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
//using REPI_PUR_SOFRA.App_Code.BLL;
//using REPI_PUR_SOFRA.App_Code.ENTITIES;
using System.Collections.Generic;
using System.IO;


namespace REPI_PUR_SOFRA
{
    public partial class SA_Common_UserAccess : System.Web.UI.Page
    {
        BLL_Common BLL = new BLL_Common();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["LoginCredentialsAccess"].ToString().Trim()))
                {
                    LoadDefault();
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        private void LoadDefault()
        {
            //try
            //{
                List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();
                list = BLL.getLoginCredentials_All();

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.IsDisabled.Contains("0")).OrderBy(item => item.FullName).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.IsDisabled.Contains("1")).OrderBy(item => item.FullName).ToList();
                    gvDisabled.DataBind();
                }
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            //}
        }

        protected void gvDisabled_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblUserName = (Label)e.Row.FindControl("lblUserName");
                    TextBox txtUserName = (TextBox)e.Row.FindControl("txtUserName");
                    LinkButton lblFullName = (LinkButton)e.Row.FindControl("lblFullName");
                    TextBox txtFullName = (TextBox)e.Row.FindControl("txtFullName");


                    txtUserName.Style.Add("display", "none");
                    txtFullName.Style.Add("display", "none");

                    //lblUserName.Text = CryptorEngine.Decrypt(lblUserName.Text, true);
                    //lblFullName.Text = CryptorEngine.Decrypt(lblFullName.Text, true);

                    //txtUserName.Text = CryptorEngine.Decrypt(txtUserName.Text, true);
                    //txtFullName.Text = CryptorEngine.Decrypt(txtFullName.Text, true);

                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                }
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            //}
        }

        protected void gvDisabled_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //try
            //{
                gvDisabled.PageIndex = e.NewPageIndex;
                LoadDefault();
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            //}
        }

        protected void gvDisabled_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = int.Parse(e.CommandArgument.ToString());
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

            Label lblId = row.FindControl("lblId") as Label;
            LinkButton lblFullName = row.FindControl("lblFullName") as LinkButton;


            if (e.CommandName == "StartEnabling_Command")
            {

                Entities_Common_SystemUsers entity = new Entities_Common_SystemUsers();
                entity.LcRefId = int.Parse(lblId.Text.Trim());

                string successDisabled = BLL.Common_EnabledLoginCredentials_ByRefId(entity).ToString();

                if (successDisabled == "-1")
                {
                    Session["successMessage"] = "USER ACCESS : <b>" + entity.LcRefId + " - " + lblFullName.Text + "</b> HAS BEEN ENABLED.";
                    Session["successTransactionName"] = "SA_COMMON_USERACCESS";
                    Session["successReturnPage"] = "SA_Common_UserAccess.aspx";

                    Response.Redirect("SuccessPage.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ConfigurationManager.AppSettings["GENERIC_ERROR_MESSAGE"] + "');", true);
                }
            }

        }

        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblUserName = (Label)e.Row.FindControl("lblUserName");
                    TextBox txtUserName = (TextBox)e.Row.FindControl("txtUserName");
                    LinkButton lblFullName = (LinkButton)e.Row.FindControl("lblFullName");
                    TextBox txtFullName = (TextBox)e.Row.FindControl("txtFullName");

                    Label lblPassword2 = (Label)e.Row.FindControl("lblPassword2");

                    if (Session["Username"].ToString().ToUpper() == "FERDIE" || Session["Username"].ToString().ToUpper() == "10771")
                    {
                        if (Session["Username"].ToString().ToUpper() == "10771")
                        {
                            if (lblUserName.Text.ToUpper() == "FERDIE" || lblUserName.Text.ToUpper() == "FERDIE_TEST" || lblUserName.Text.ToUpper() == "FERDIE_TEST")
                            {
                                lblPassword2.Visible = false;
                            }
                            else
                            {
                                lblPassword2.Visible = true;
                            }
                        }
                        else
                        {
                            lblPassword2.Visible = true;
                        }
                        
                    }
                    else
                    {
                        lblPassword2.Visible = false;
                    }

                    txtUserName.Style.Add("display", "none");
                    txtFullName.Style.Add("display", "none");

                    //lblUserName.Text = CryptorEngine.Decrypt(lblUserName.Text, true);
                    //lblFullName.Text = CryptorEngine.Decrypt(lblFullName.Text, true);

                    //txtUserName.Text = CryptorEngine.Decrypt(txtUserName.Text, true);
                    //txtFullName.Text = CryptorEngine.Decrypt(txtFullName.Text, true);

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
            //try
            //{
                gvData.PageIndex = e.NewPageIndex;
                LoadDefault();
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            //}
        }

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);


                LinkButton lblFullName = row.FindControl("lblFullName") as LinkButton;
                Label lblId = row.FindControl("lblId") as Label;

                if (e.CommandName == "lblFullName_Command")
                {
                    //string URL = "~/SA_Common_UserAccess_Details.aspx?SA_UserAccess_RefId=" + CryptorEngine.Encrypt(lblId.Text.Trim(), true);
                    //string URL = "~/SA_Common_UserAccess_Details.aspx?SA_UserAccess_RefId=" + lblId.Text.Trim();

                    //URL = Page.ResolveClientUrl(URL);
                    //lblFullName.OnClientClick = "window.open('" + URL + "'); return false;";
                    Response.Redirect("SA_Common_UserAccess_Details.aspx?SA_UserAccess_RefId=" + lblId.Text.Trim(), false);

                }

                if (e.CommandName == "StartEditing_Command")
                {
                    //string URL = "~/SA_Common_UserAccess_Details.aspx?SA_UserAccess_RefId=" + CryptorEngine.Encrypt(lblId.Text.Trim(), true);
                    string URL = "~/SA_Common_UserAccess_Details.aspx?SA_UserAccess_RefId=" + lblId.Text.Trim();

                    URL = Page.ResolveClientUrl(URL);
                    lblFullName.OnClientClick = "window.open('" + URL + "'); return false;";
                }

                if (e.CommandName == "StartDisabling_Command")
                {

                    Entities_Common_SystemUsers entity = new Entities_Common_SystemUsers();
                    entity.LcRefId = int.Parse(lblId.Text.Trim());

                    string successDisabled = BLL.Common_DisabledLoginCredentials_ByRefId(entity).ToString();

                    if (successDisabled == "-1")
                    {
                        Session["successMessage"] = "USER ACCESS : <b>" + entity.LcRefId + " - " + lblFullName.Text + "</b> HAS BEEN DISABLED.";
                        Session["successTransactionName"] = "SA_COMMON_USERACCESS";
                        Session["successReturnPage"] = "SA_Common_UserAccess.aspx";

                        Response.Redirect("SuccessPage.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ConfigurationManager.AppSettings["GENERIC_ERROR_MESSAGE"] + "');", true);
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //try
            //{
                if (txtSearch.Text.Length <= 4)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please enter atleast 5 characters to search.');", true);
                }
                else
                {

                    List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();
                    list = BLL.getLoginCredentials_All();
                    List<Entities_Common_SystemUsers> list_final = new List<Entities_Common_SystemUsers>();

                    foreach (Entities_Common_SystemUsers final in list)
                    {
                        Entities_Common_SystemUsers f = new Entities_Common_SystemUsers();

                        f.LcRefId = final.LcRefId;
                        f.Username = final.Username;
                        f.Password = final.Password;
                        f.FullName = final.FullName;
                        f.IsDisabled = final.IsDisabled;
                        f.DivisionCode = final.DivisionCode;
                        f.Division = final.Division;
                        f.Department = final.Department;
                        f.Category = final.Category;
                        f.SectionName = final.SectionName;
                        f.DepartmentName = final.DepartmentName;
                        f.DivisionName = final.DivisionName;
                        f.PcName = final.PcName;
                        f.HqName = final.HqName;

                        list_final.Add(f);
                    }


                    if (list_final.Count > 0)
                    {
                        gvData.DataSource = list_final.Where(item => item.IsDisabled.Contains("0") && item.FullName.Contains(txtSearch.Text.ToUpper())).OrderBy(item => item.FullName).ToList();
                        gvData.DataBind();

                        gvDisabled.DataSource = list_final.Where(item => item.IsDisabled.Contains("1") && item.FullName.Contains(txtSearch.Text.ToUpper())).OrderBy(item => item.FullName).ToList();
                        gvDisabled.DataBind();
                    }

                }
            //}
            //catch (Exception ex)
            //{
            //    LoadDefault();
            //    txtSearch.Text = string.Empty;
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            //}
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("SA_Common_UserAccess_Details.aspx");
        }

        private void LoadExportData()
        {
            try
            {
                List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();
                list = BLL.getLoginCredentials_All_Export();

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        gvExport.DataSource = list;
                        gvExport.DataBind();

                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            LoadExportData();
            Response.Clear();
            Response.Buffer = true;

            Response.AddHeader("content-disposition",
            "attachment;filename=LoginCredentials.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            gvExport.AllowPaging = false;
            gvExport.DataBind();

            //Change the Header Row back to white color
            gvExport.HeaderRow.Style.Add("background-color", "#FFFFFF");
            gvExport.HeaderRow.Style.Add("color", "#000000");


            gvExport.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }


    }
}
