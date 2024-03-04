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
using System.Collections.Generic;

namespace REPI_PUR_SOFRA
{
    public partial class RFQ_MT_BuyerInformation : System.Web.UI.Page
    {

        BLL_RFQ BLL = new BLL_RFQ();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["CategoryAccess"].ToString().Trim()))
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
            try
            {
                List<Entities_RFQ_BuyerInformation> list = new List<Entities_RFQ_BuyerInformation>();
                list = BLL.RFQ_MT_BuyerInformation_GetAll();

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.IsDisabled.Contains("1")).ToList();
                    gvDisabled.DataBind();

                    gvData.FooterRow.Visible = gvData.PageIndex == 0;

                    LinkButton lbSaveNew = (LinkButton)gvData.FooterRow.FindControl("lbSaveNew");
                    TextBox txtNewMember = (TextBox)gvData.FooterRow.FindControl("txtNewMember");
                    TextBox txtNewSection = (TextBox)gvData.FooterRow.FindControl("txtNewSection");
                    TextBox txtNewEmail = (TextBox)gvData.FooterRow.FindControl("txtNewEmail");
                    TextBox txtNewMobile = (TextBox)gvData.FooterRow.FindControl("txtNewMobile");

                    lbSaveNew.Style.Add("display", "none");
                    txtNewMember.Enabled = false;
                    txtNewSection.Enabled = false;
                    txtNewEmail.Enabled = false;
                    txtNewMobile.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvDisabled_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvDisabled_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvDisabled.PageIndex = e.NewPageIndex;
                LoadDefault();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvDisabled_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                Label lblId = row.FindControl("lblId") as Label;

                if (e.CommandName == "StartEnabling_Command")
                {
                    try
                    {
                        Entities_RFQ_BuyerInformation entity = new Entities_RFQ_BuyerInformation();
                        entity.RefId = lblId.Text.Trim();
                        entity.IsDisabled = "0";

                        BLL.RFQ_MT_BuyerInformation_IsDisabled(entity);

                        LoadDefault();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Record has been enabled!');", true);
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblMember = (Label)e.Row.FindControl("lblMember");
                    TextBox txtMember = (TextBox)e.Row.FindControl("txtMember");
                    Label lblSection = (Label)e.Row.FindControl("lblSection");
                    TextBox txtSection = (TextBox)e.Row.FindControl("txtSection");
                    Label lblEmail = (Label)e.Row.FindControl("lblEmail");
                    TextBox txtEmail = (TextBox)e.Row.FindControl("txtEmail");
                    Label lblMobile = (Label)e.Row.FindControl("lblMobile");
                    TextBox txtMobile = (TextBox)e.Row.FindControl("txtMobile");
                    LinkButton lbSave = (LinkButton)e.Row.FindControl("lbSave");

                    txtMember.Style.Add("display", "none");
                    txtSection.Style.Add("display", "none");
                    txtEmail.Style.Add("display", "none");
                    txtMobile.Style.Add("display", "none");
                    lbSave.Style.Add("display", "none");

                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvData.PageIndex = e.NewPageIndex;
                LoadDefault();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                TextBox txtMember = row.FindControl("txtMember") as TextBox;
                TextBox txtSection = row.FindControl("txtSection") as TextBox;
                TextBox txtEmail = row.FindControl("txtEmail") as TextBox;
                TextBox txtMobile = row.FindControl("txtMobile") as TextBox;
                Label lblId = row.FindControl("lblId") as Label;
                Label lblMember = row.FindControl("lblMember") as Label;
                Label lblSection = row.FindControl("lblSection") as Label;
                Label lblEmail = row.FindControl("lblEmail") as Label;
                Label lblMobile = row.FindControl("lblMobile") as Label;

                LinkButton lbEdit = row.FindControl("lbEdit") as LinkButton;
                LinkButton lbSave = row.FindControl("lbSave") as LinkButton;

                TextBox txtNewMember = gvData.FooterRow.FindControl("txtNewMember") as TextBox;
                TextBox txtNewSection = gvData.FooterRow.FindControl("txtNewSection") as TextBox;
                TextBox txtNewEmail = gvData.FooterRow.FindControl("txtNewEmail") as TextBox;
                TextBox txtNewMobile = gvData.FooterRow.FindControl("txtNewMobile") as TextBox;

                LinkButton lbAddNew = gvData.FooterRow.FindControl("lbAddNew") as LinkButton;
                LinkButton lbSaveNew = gvData.FooterRow.FindControl("lbSaveNew") as LinkButton;
                LinkButton lbDisabled = row.FindControl("lbDisabled") as LinkButton;

                if (e.CommandName == "StartEditing_Command")
                {
                    if (lbEdit.Text == "CANCEL")
                    {
                        txtMember.Style.Add("display", "none");
                        txtSection.Style.Add("display", "none");
                        txtEmail.Style.Add("display", "none");
                        txtMobile.Style.Add("display", "none");
                        lbSave.Style.Add("display", "none");

                        lblMember.Style.Add("display", "none");
                        lblSection.Style.Add("display", "none");
                        lblEmail.Style.Add("display", "none");
                        lblMobile.Style.Add("display", "none");
                        lbDisabled.Style.Add("display", "block");

                        lbEdit.Text = "EDIT";
                    }
                    else
                    {
                        lblMember.Style.Add("display", "none");
                        txtMember.Style.Add("display", "block");
                        lblSection.Style.Add("display", "none");
                        txtSection.Style.Add("display", "block");
                        lblEmail.Style.Add("display", "none");
                        txtEmail.Style.Add("display", "block");
                        lblMobile.Style.Add("display", "none");
                        txtMobile.Style.Add("display", "block");

                        lbEdit.Text = "CANCEL";
                        lbSave.Style.Add("display", "block");
                        lbDisabled.Style.Add("display", "none");

                        txtMember.Focus();
                    }
                }

                if (e.CommandName == "StartUpdating_Command")
                {
                    try
                    {
                        Entities_RFQ_BuyerInformation entity = new Entities_RFQ_BuyerInformation();
                        entity.RefId = lblId.Text.Trim();
                        entity.Member = txtMember.Text.Replace("'", "''").ToUpper();
                        entity.Section = txtSection.Text.Replace("'", "''").ToUpper();
                        entity.Email = txtEmail.Text.Replace("'", "''");
                        entity.Mobile = txtMobile.Text.Replace("'", "''").ToUpper();
                        entity.UpdatedBy = Session["LcRefId"].ToString();

                        try
                        {

                            if (entity.Member.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Member must not be blank');", true);
                            }
                            else if (entity.Section.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Section must not be blank');", true);
                            }

                            else
                            {
                                BLL.RFQ_MT_BuyerInformation_Append(entity);

                                lblMember.Text = txtMember.Text;
                                txtMember.Style.Add("display", "none");
                                lblSection.Text = txtSection.Text;
                                txtSection.Style.Add("display", "none");
                                lblEmail.Text = txtEmail.Text;
                                txtEmail.Style.Add("display", "none");
                                lblMobile.Text = txtMobile.Text;
                                txtMobile.Style.Add("display", "none");

                                lblMember.Style.Add("display", "none");
                                lblSection.Style.Add("display", "block");
                                lblEmail.Style.Add("display", "block");
                                lblMobile.Style.Add("display", "block");

                                lbSave.Style.Add("display", "none");
                                lbEdit.Style.Add("display", "block");
                                lbEdit.Text = "EDIT";
                                lbDisabled.Style.Add("display", "block");

                                LoadDefault();
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Record has been successfully updated!');", true);
                            }
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                        }

                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                    }

                }

                if (e.CommandName == "AddNew_Command")
                {
                    if (lbAddNew.Text == "ADD NEW")
                    {
                        txtNewMember.Enabled = true;
                        txtNewSection.Enabled = true;
                        txtNewEmail.Enabled = true;
                        txtNewMobile.Enabled = true;

                        lbAddNew.Text = "CANCEL";
                        lbSaveNew.Style.Add("display", "block");
                    }
                    else
                    {
                        txtNewMember.Enabled = false;
                        txtNewSection.Enabled = false;
                        txtNewEmail.Enabled = false;
                        txtNewMobile.Enabled = false;

                        lbAddNew.Text = "ADD NEW";
                        lbSaveNew.Style.Add("display", "none");
                    }
                }

                if (e.CommandName == "SaveNew_Command")
                {

                    try
                    {
                        Entities_RFQ_BuyerInformation entity = new Entities_RFQ_BuyerInformation();
                        entity.Member = txtNewMember.Text.Replace("'", "''").ToUpper();
                        entity.Section = txtNewSection.Text.Replace("'", "''").ToUpper();
                        entity.Email = txtNewEmail.Text.Replace("'", "''");
                        entity.Mobile = txtNewMobile.Text.Replace("'", "''").ToUpper();
                        entity.AddedBy = Session["LcRefId"].ToString();

                        try
                        {
                            if (entity.Member.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Member must not be blank');", true);
                            }
                            else if (entity.Section.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Section must not be blank');", true);
                            }
                            else if (BLL.RFQ_MT_BuyerInformation_GetByMember(entity.Member).Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + entity.Member + " is already exist.');", true);
                            }
                            else
                            {
                                BLL.RFQ_MT_BuyerInformation_Insert(entity);

                                txtNewMember.Enabled = false;
                                txtNewSection.Enabled = false;
                                txtNewEmail.Enabled = false;
                                txtNewMobile.Enabled = false;

                                lbSaveNew.Style.Add("display", "none");
                                lbAddNew.Text = "ADD NEW";

                                LoadDefault();
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Record has been successfully added!');", true);
                            }
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                        }

                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                    }


                }

                if (e.CommandName == "StartDisabling_Command")
                {
                    try
                    {
                        Entities_RFQ_BuyerInformation entity = new Entities_RFQ_BuyerInformation();
                        entity.RefId = lblId.Text.Trim();
                        entity.IsDisabled = "1";

                        BLL.RFQ_MT_BuyerInformation_IsDisabled(entity);

                        LoadDefault();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Record has been disabled!');", true);
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
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
            try
            {
                List<Entities_RFQ_BuyerInformation> list = new List<Entities_RFQ_BuyerInformation>();
                list = BLL.RFQ_MT_BuyerInformation_GetByMember_Like(txtSearch.Text);

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.IsDisabled.Contains("1")).ToList();
                    gvDisabled.DataBind();

                    gvData.FooterRow.Visible = gvData.PageIndex == 0;

                    LinkButton lbSaveNew = (LinkButton)gvData.FooterRow.FindControl("lbSaveNew");
                    TextBox txtNewMember = (TextBox)gvData.FooterRow.FindControl("txtNewMember");
                    TextBox txtNewSection = (TextBox)gvData.FooterRow.FindControl("txtNewSection");
                    TextBox txtNewEmail = (TextBox)gvData.FooterRow.FindControl("txtNewEmail");
                    TextBox txtNewMobile = (TextBox)gvData.FooterRow.FindControl("txtNewMobile");

                    lbSaveNew.Style.Add("display", "none");
                    txtNewMember.Enabled = false;
                    txtNewSection.Enabled = false;
                    txtNewEmail.Enabled = false;
                    txtNewMobile.Enabled = false;

                }
                else
                {
                    LoadDefault();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Cannot find " + txtSearch.Text + "');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


    }
}
