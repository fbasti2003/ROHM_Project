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
    public partial class PLM_MT_Supplier : System.Web.UI.Page
    {
        BLL_PLM BLL = new BLL_PLM();
        Common COMMON = new Common();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), "501"))
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
                List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();
                list = BLL.PLM_MT_Supplier_GetAll();

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.IsDisabled.Contains("1")).ToList();
                    gvDisabled.DataBind();

                    gvData.FooterRow.Visible = gvData.PageIndex == 0;

                    LinkButton lbSaveNew = (LinkButton)gvData.FooterRow.FindControl("lbSaveNew");
                    TextBox txtNewCode = (TextBox)gvData.FooterRow.FindControl("txtNewCode");
                    TextBox txtNewDescription = (TextBox)gvData.FooterRow.FindControl("txtNewDescription");
                    TextBox txtNewEmailAddress = (TextBox)gvData.FooterRow.FindControl("txtNewEmailAddress");
                    CheckBox chkNewTakeAction = (CheckBox)gvData.FooterRow.FindControl("chkNewTakeAction");
                    CheckBox chkNewExpired = (CheckBox)gvData.FooterRow.FindControl("chkNewTakeAction");
                    CheckBox chkNewRenewed = (CheckBox)gvData.FooterRow.FindControl("chkNewTakeAction");

                    lbSaveNew.Style.Add("display", "none");
                    txtNewCode.Enabled = false;
                    txtNewDescription.Enabled = false;
                    txtNewEmailAddress.Enabled = false;
                    chkNewTakeAction.Enabled = false;
                    chkNewExpired.Enabled = false;
                    chkNewRenewed.Enabled = false;
                    

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
                        Entities_PLM_PermitCertificates entity = new Entities_PLM_PermitCertificates();
                        entity.RefId = lblId.Text.Trim();
                        entity.IsDisabled = "0";

                        BLL.PLM_MT_Supplier_IsDisabled(entity);

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
                    Label lblCode = (Label)e.Row.FindControl("lblCode");
                    TextBox txtCode = (TextBox)e.Row.FindControl("txtCode");
                    Label lblDescription = (Label)e.Row.FindControl("lblDescription");
                    TextBox txtDescription = (TextBox)e.Row.FindControl("txtDescription");
                    Label lblEmailAddress = (Label)e.Row.FindControl("lblEmailAddress");
                    TextBox txtEmaiilAddress = (TextBox)e.Row.FindControl("txtEmaiilAddress");

                    CheckBox chkTakeAction = (CheckBox)e.Row.FindControl("chkTakeAction");
                    CheckBox chkExpired = (CheckBox)e.Row.FindControl("chkTakeAction");
                    CheckBox chkRenewed = (CheckBox)e.Row.FindControl("chkTakeAction");

                    LinkButton lbSave = (LinkButton)e.Row.FindControl("lbSave");

                    txtCode.Style.Add("display", "none");
                    txtDescription.Style.Add("display", "none");
                    txtEmaiilAddress.Style.Add("display", "none");
                    lbSave.Style.Add("display", "none");

                    chkTakeAction.Enabled = false;
                    chkExpired.Enabled = false;
                    chkRenewed.Enabled = false;

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

                TextBox txtCode = row.FindControl("txtCode") as TextBox;
                TextBox txtDescription = row.FindControl("txtDescription") as TextBox;
                TextBox txtEmaiilAddress = row.FindControl("txtEmaiilAddress") as TextBox;
                Label lblId = row.FindControl("lblId") as Label;
                Label lblCode = row.FindControl("lblCode") as Label;
                Label lblDescription = row.FindControl("lblDescription") as Label;
                Label lblEmailAddress = row.FindControl("lblEmailAddress") as Label;

                LinkButton lbEdit = row.FindControl("lbEdit") as LinkButton;
                LinkButton lbSave = row.FindControl("lbSave") as LinkButton;

                TextBox txtNewCode = gvData.FooterRow.FindControl("txtNewCode") as TextBox;
                TextBox txtNewDescription = gvData.FooterRow.FindControl("txtNewDescription") as TextBox;
                TextBox txtNewEmailAddress = gvData.FooterRow.FindControl("txtNewEmailAddress") as TextBox;

                LinkButton lbAddNew = gvData.FooterRow.FindControl("lbAddNew") as LinkButton;
                LinkButton lbSaveNew = gvData.FooterRow.FindControl("lbSaveNew") as LinkButton;
                LinkButton lbDisabled = row.FindControl("lbDisabled") as LinkButton;

                CheckBox chkTakeAction = row.FindControl("chkTakeAction") as CheckBox;
                CheckBox chkExpired = row.FindControl("chkExpired") as CheckBox;
                CheckBox chkRenewed = row.FindControl("chkRenewed") as CheckBox;

                CheckBox chkNewTakeAction = gvData.FooterRow.FindControl("chkNewTakeAction") as CheckBox;
                CheckBox chkNewExpired = gvData.FooterRow.FindControl("chkNewExpired") as CheckBox;
                CheckBox chkNewRenewed = gvData.FooterRow.FindControl("chkNewRenewed") as CheckBox;

                if (e.CommandName == "StartEditing_Command")
                {
                    if (lbEdit.Text == "CANCEL")
                    {
                        txtCode.Style.Add("display", "none");
                        txtDescription.Style.Add("display", "none");
                        txtEmaiilAddress.Style.Add("display", "none");
                        lbSave.Style.Add("display", "none");

                        lblCode.Style.Add("display", "block");
                        lblDescription.Style.Add("display", "block");
                        lblEmailAddress.Style.Add("display", "block");
                        lbDisabled.Style.Add("display", "block");

                        chkRenewed.Enabled = false;
                        chkTakeAction.Enabled = false;
                        chkExpired.Enabled = false;

                        chkNewTakeAction.Enabled = true;
                        chkNewExpired.Enabled = true;
                        chkNewRenewed.Enabled = true;

                        lbEdit.Text = "EDIT";
                    }
                    else
                    {
                        lblCode.Style.Add("display", "none");
                        txtCode.Style.Add("display", "block");
                        lblDescription.Style.Add("display", "none");
                        txtDescription.Style.Add("display", "block");
                        lblEmailAddress.Style.Add("display", "none");
                        txtEmaiilAddress.Style.Add("display", "block");

                        lbEdit.Text = "CANCEL";
                        lbSave.Style.Add("display", "block");
                        lbDisabled.Style.Add("display", "none");

                        chkRenewed.Enabled = true;
                        chkTakeAction.Enabled = true;
                        chkExpired.Enabled = true;

                        chkNewTakeAction.Enabled = false;
                        chkNewExpired.Enabled = false;
                        chkNewRenewed.Enabled = false;

                        txtCode.Focus();
                    }
                }

                if (e.CommandName == "StartUpdating_Command")
                {
                    try
                    {
                        Entities_PLM_PermitCertificates entity = new Entities_PLM_PermitCertificates();
                        entity.RefId = lblId.Text.Trim();
                        entity.SupplierCode = txtCode.Text.Replace("'", "''").ToUpper();
                        entity.SupplierName = txtDescription.Text.Replace("'", "''").ToUpper();
                        entity.SupplierEmailAddress = txtEmaiilAddress.Text.Replace("'", "''").ToUpper();
                        entity.TakeAction = chkTakeAction.Checked ? "1" : "0";
                        entity.Expired = chkExpired.Checked ? "1" : "0";
                        entity.Renewed = chkRenewed.Checked ? "1" : "0";
                        entity.UpdatedBy = Session["LcRefId"].ToString();

                        try
                        {

                            if (entity.SupplierCode.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Code must not be blank');", true);
                            }
                            else if (entity.SupplierName.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Description must not be blank');", true);
                            }

                            else
                            {
                                BLL.PLM_MT_Supplier_Append(entity);

                                lblCode.Text = txtCode.Text;
                                txtCode.Style.Add("display", "none");
                                lblDescription.Text = txtDescription.Text;
                                txtDescription.Style.Add("display", "none");
                                lblEmailAddress.Text = txtEmaiilAddress.Text;
                                txtEmaiilAddress.Style.Add("display", "none");

                                lbSave.Style.Add("display", "none");
                                lblCode.Style.Add("display", "block");
                                lblDescription.Style.Add("display", "block");
                                lblEmailAddress.Style.Add("display", "block");

                                lbSave.Style.Add("display", "none");
                                lbEdit.Style.Add("display", "block");
                                lbEdit.Text = "EDIT";
                                lbDisabled.Style.Add("display", "block");

                                chkRenewed.Enabled = false;
                                chkTakeAction.Enabled = false;
                                chkExpired.Enabled = false;

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
                        txtNewCode.Enabled = true;
                        txtNewDescription.Enabled = true;
                        txtNewEmailAddress.Enabled = true;

                        chkNewTakeAction.Enabled = true;
                        chkNewExpired.Enabled = true;
                        chkNewRenewed.Enabled = true;

                        lbAddNew.Text = "CANCEL";
                        lbSaveNew.Style.Add("display", "block");
                    }
                    else
                    {
                        txtNewCode.Enabled = false;
                        txtNewDescription.Enabled = false;
                        txtNewEmailAddress.Enabled = false;

                        chkNewTakeAction.Enabled = false;
                        chkNewExpired.Enabled = false;
                        chkNewRenewed.Enabled = false;

                        lbAddNew.Text = "ADD NEW";
                        lbSaveNew.Style.Add("display", "none");
                    }
                }

                if (e.CommandName == "SaveNew_Command")
                {

                    try
                    {
                        Entities_PLM_PermitCertificates entity = new Entities_PLM_PermitCertificates();
                        entity.SupplierCode = txtNewCode.Text.Replace("'", "''").ToUpper();
                        entity.SupplierName = txtNewDescription.Text.Replace("'", "''").ToUpper();
                        entity.SupplierEmailAddress = txtNewEmailAddress.Text.Replace("'", "''").ToUpper();
                        entity.TakeAction = chkNewTakeAction.Checked ? "1" : "0";
                        entity.Expired = chkNewExpired.Checked ? "1" : "0";
                        entity.Renewed = chkNewRenewed.Checked ? "1" : "0";
                        entity.AddedBy = Session["LcRefId"].ToString();

                        try
                        {
                            if (entity.SupplierCode.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Code must not be blank');", true);
                            }
                            else if (entity.SupplierName.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Description must not be blank');", true);
                            }
                            else if (BLL.PLM_MT_Supplier_GetBySupplierName(entity.SupplierName).Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + entity.SupplierName + " is already exist.');", true);
                            }
                            else
                            {
                                BLL.PLM_MT_Supplier_Insert(entity);

                                txtNewCode.Enabled = false;
                                txtNewDescription.Enabled = false;
                                txtNewEmailAddress.Enabled = false;

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
                        Entities_PLM_PermitCertificates entity = new Entities_PLM_PermitCertificates();
                        entity.RefId = lblId.Text.Trim();
                        entity.IsDisabled = "1";

                        BLL.PLM_MT_Supplier_IsDisabled(entity);

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
                List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();
                list = BLL.PLM_MT_Supplier_GetBySupplierName_Like(txtSearch.Text);

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.IsDisabled.Contains("1")).ToList();
                    gvDisabled.DataBind();

                    gvData.FooterRow.Visible = gvData.PageIndex == 0;

                    LinkButton lbSaveNew = (LinkButton)gvData.FooterRow.FindControl("lbSaveNew");
                    TextBox txtNewCode = (TextBox)gvData.FooterRow.FindControl("txtNewCode");
                    TextBox txtNewDescription = (TextBox)gvData.FooterRow.FindControl("txtNewDescription");

                    lbSaveNew.Style.Add("display", "none");
                    txtNewCode.Enabled = false;
                    txtNewDescription.Enabled = false;

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
