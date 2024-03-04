using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
//using REPI_PUR_SOFRA.App_Code.ENTITIES;
//using REPI_PUR_SOFRA.App_Code.BLL;
using System.Collections.Generic;


namespace REPI_PUR_SOFRA
{
    public partial class SA_Common_UserAccess_Details : System.Web.UI.Page
    {
        BLL_Common BLL = new BLL_Common();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["LoginCredentialsAccess"].ToString().Trim()))
                {
                    Common_UserAccess_Fill_All_DropdownList();
                    loadFormList();

                    if (Request.QueryString["SA_UserAccess_RefId"] != null)
                    {
                        loadDefaultDetails();
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //try
            //{
                bool isSuccessAll = false;
                string successLoginUser = string.Empty;
                string successDeleteLoginUser = string.Empty;
                bool isUpdate = false;

                Entities_Common_SystemUsers entity = new Entities_Common_SystemUsers();

                //entity.LcRefId = Request.QueryString["SA_UserAccess_RefId"] != null ? int.Parse(CryptorEngine.Decrypt(Request.QueryString["SA_UserAccess_RefId"].ToString(), true)) : 0;
                entity.LcRefId = Request.QueryString["SA_UserAccess_RefId"] != null ? int.Parse(Request.QueryString["SA_UserAccess_RefId"].ToString()) : 0;
                entity.Username = CryptorEngine.Encrypt(txtUserName.Text.ToUpper(), true);
                entity.FullName = CryptorEngine.Encrypt(txtFullName.Text.ToUpper(), true);
                entity.Section = int.Parse(ddSection.SelectedValue);
                entity.Department = int.Parse(ddDepartment.SelectedValue);
                entity.Division = int.Parse(ddDivision.SelectedValue);
                entity.PC = int.Parse(ddPC.SelectedValue);
                entity.HQ = int.Parse(ddHQ.SelectedValue);
                entity.Category = ddCategory.SelectedValue;
                entity.LocalNumber = txtLocalNo.Text;
                entity.UpdatedBy = Session["LcRefId"].ToString();
                entity.AddedBy = Session["LcRefId"].ToString();
                entity.EmailAddress = txtEmailAddress.Text;
                
                
                if (Request.QueryString["SA_UserAccess_RefId"] != null)
                {
                    // UPDATE LOGIN CREDENTIALS
                    isUpdate = true;
                    successLoginUser = BLL.Common_UpdateLoginCredentials_ByRefId(entity).ToString();
                    if (successLoginUser == "-1")
                    {
                        successDeleteLoginUser = BLL.Common_DeleteUserAccess_ByLoginId(entity).ToString();
                    }

                    if (successDeleteLoginUser == "-1")
                    {

                        if (gvData.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvData.Rows.Count; i++)
                            {
                                Label lblAccessValue = (Label)gvData.Rows[i].Cells[3].FindControl("lblAccessValue");
                                ImageButton ibAllowed = (ImageButton)gvData.Rows[i].Cells[3].FindControl("ibAllowed");

                                Entities_Common_SystemUsers useraccess = new Entities_Common_SystemUsers();
                                useraccess.LcRefId = entity.LcRefId;
                                useraccess.TransactionType = lblAccessValue.Text.Trim();
                                useraccess.Allowed = "1";
                                useraccess.AddedBy = entity.UpdatedBy;

                                if (ibAllowed.ImageUrl == "~/images/A2.png")
                                {
                                    BLL.Common_InserUserAccess_ByLoginId(useraccess);
                                }
                            }

                            if (cbResetPassword.Checked)
                            {
                                BLL.Common_ResetPassword(entity);
                            }

                            isSuccessAll = true;
                        }

                    }

                }
                else
                {
                    if (BLL.getLoginCredentials_ByUserName(CryptorEngine.Encrypt(txtUserName.Text.ToUpper(), true)).Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + txtUserName.Text.ToUpper() + " is already exist.');", true);
                    }
                    else
                    {
                        // INSERT NEW LOGIN CREDENTIALS
                        isUpdate = false;
                        successLoginUser = BLL.Common_InsertLoginCredentials(entity).ToString();

                        if (successLoginUser == "-1")
                        {
                            List<Entities_Common_SystemUsers> username = new List<Entities_Common_SystemUsers>();
                            username = BLL.getLoginCredentials_ByUserName(CryptorEngine.Encrypt(txtUserName.Text.ToUpper(), true));

                            if (username.Count > 0)
                            {
                                foreach (Entities_Common_SystemUsers user in username)
                                {
                                    entity.LcRefId = user.LcRefId;
                                }

                                if (gvData.Rows.Count > 0)
                                {
                                    for (int i = 0; i < gvData.Rows.Count; i++)
                                    {
                                        Label lblAccessValue = (Label)gvData.Rows[i].Cells[3].FindControl("lblAccessValue");
                                        ImageButton ibAllowed = (ImageButton)gvData.Rows[i].Cells[3].FindControl("ibAllowed");

                                        Entities_Common_SystemUsers useraccess = new Entities_Common_SystemUsers();
                                        useraccess.LcRefId = entity.LcRefId;
                                        useraccess.TransactionType = lblAccessValue.Text.Trim();
                                        useraccess.Allowed = "1";
                                        useraccess.AddedBy = entity.AddedBy;

                                        if (ibAllowed.ImageUrl == "~/images/A2.png")
                                        {
                                            BLL.Common_InserUserAccess_ByLoginId(useraccess);
                                        }
                                    }

                                    isSuccessAll = true;
                                }

                            }

                        }


                    }

                }                    



                if (isSuccessAll)
                {
                    string updateOrNew = string.Empty;

                    if (isUpdate)
                    {
                        updateOrNew = "UPDATED";
                    }
                    else
                    {
                        updateOrNew = "ADDED";
                    }

                    Session["successMessage"] = "USER ACCESS : <b>" + entity.LcRefId + " - " + txtUserName.Text + "</b> HAS BEEN SUCCESSFULLY '" + updateOrNew +".";
                    Session["successTransactionName"] = "SA_COMMON_USERACCESS_DETAILS";
                    Session["successReturnPage"] = "SA_Common_UserAccess.aspx";

                    Response.Redirect("SuccessPage.aspx");
                }

            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            //}
        }

        private void loadFormList()
        {
            try
            {
                List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();
                if (Request.QueryString["SA_UserAccess_RefId"] != null)
                {
                    //list = BLL.getFormListByRefId(CryptorEngine.Decrypt(Request.QueryString["SA_UserAccess_RefId"].ToString(), true));
                    list = BLL.getFormListByRefId(Request.QueryString["SA_UserAccess_RefId"].ToString());
                }
                else
                {
                    list = BLL.getFormList();
                }
                

                if (list.Count > 0)
                {
                    gvData.DataSource = list;
                    gvData.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void loadDefaultDetails()
        {
            try
            {
                //string refid = CryptorEngine.Decrypt(Request.QueryString["SA_UserAccess_RefId"].ToString().Replace(" ", "+"), true);
                string refid = Request.QueryString["SA_UserAccess_RefId"].ToString();
                List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();
                list = BLL.getLoginCredentialsByRefId(refid);

                ddCategory.ClearSelection();
                ddDepartment.ClearSelection();
                ddDivision.ClearSelection();
                ddHQ.ClearSelection();
                ddPC.ClearSelection();
                ddSection.ClearSelection();

                foreach (Entities_Common_SystemUsers entity in list)
                {
                    txtUserName.Text = CryptorEngine.Decrypt(entity.Username, true);
                    txtFullName.Text = CryptorEngine.Decrypt(entity.FullName, true);
                    txtLocalNo.Text = entity.LocalNumber;
                    txtEmailAddress.Text = entity.EmailAddress;

                    if (int.Parse(entity.Category) > 0)
                    {
                        ddCategory.Items.FindByValue(entity.Category).Selected = true;
                    }
                    if (entity.Department > 0)
                    {
                        ddDepartment.Items.FindByText(entity.DepartmentName.ToString().ToUpper()).Selected = true;
                    }
                    if (entity.Division > 0)
                    {
                        ddDivision.Items.FindByText(entity.DivisionName.ToString().ToUpper()).Selected = true;
                    }
                    if (entity.HQ > 0)
                    {
                        ddHQ.Items.FindByText(entity.HqName.ToString().ToUpper()).Selected = true;
                    }
                    if (entity.PC > 0)
                    {
                        ddPC.Items.FindByText(entity.PcName.ToString().ToUpper()).Selected = true;
                    }
                    if (entity.Section > 0)
                    {
                        ddSection.Items.FindByText(entity.SectionName.ToString().ToUpper()).Selected = true;
                    }

                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
               
        }

        private void Common_UserAccess_Fill_All_DropdownList()
        {
            List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();
            list = BLL.Common_UserAccess_Fill_All_DropdownList().OrderBy(item => item.DropdownName).ToList();

            ddCategory.Items.Add("");
            ddDepartment.Items.Add("");
            ddDivision.Items.Add("");
            ddSection.Items.Add("");
            ddPC.Items.Add("");
            ddHQ.Items.Add("");

            foreach (Entities_Common_SystemUsers entity in list)
            {
                ListItem item = new ListItem();
                item.Text = entity.DropdownName.ToUpper();
                item.Value = entity.DropdownRefId;

                if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                {
                    if (entity.TableName == "MT_Category")
                    {
                        ddCategory.Items.Add(item);
                    }
                    if (entity.TableName == "MT_Department")
                    {
                        ddDepartment.Items.Add(item);
                    }
                    if (entity.TableName == "MT_Division")
                    {
                        ddDivision.Items.Add(item);
                    }
                    if (entity.TableName == "MT_Section")
                    {
                        ddSection.Items.Add(item);
                    }
                    if (entity.TableName == "MT_PC")
                    {
                        ddPC.Items.Add(item);
                    }
                    if (entity.TableName == "MT_HQ")
                    {
                        ddHQ.Items.Add(item);
                    }

                }
            }


        }

        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblIsAllowed = (Label)e.Row.FindControl("lblIsAllowed");
                    CheckBox cbAllowed = (CheckBox)e.Row.FindControl("cbAllowed");
                    ImageButton ibAllowed = (ImageButton)e.Row.FindControl("ibAllowed");


                    if (!string.IsNullOrEmpty(lblIsAllowed.Text) || int.Parse(lblIsAllowed.Text.Trim()) > 0)
                    {
                        ibAllowed.ImageUrl = "~/images/A2.png";
                    }
                    if (string.IsNullOrEmpty(lblIsAllowed.Text) || int.Parse(lblIsAllowed.Text.Trim()) <= 0)
                    {
                        ibAllowed.ImageUrl = "~/images/DA2.png";
                    }

                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                }
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            //}
        }

        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //try
            //{
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                ImageButton ibAllowed = row.FindControl("ibAllowed") as ImageButton;

                if (e.CommandName == "ibaCommand")
                {
                    if (ibAllowed.ImageUrl == "~/images/A2.png")
                    {
                        ibAllowed.ImageUrl = "~/images/DA2.png";
                    }
                    else
                    {
                        ibAllowed.ImageUrl = "~/images/A2.png";
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            //}
        }







    }
}
