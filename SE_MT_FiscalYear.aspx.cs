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
    public partial class SE_MT_FiscalYear : System.Web.UI.Page
    {
        BLL_SE BLL = new BLL_SE();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDefault();
            }

        }

        private void LoadDefault()
        {
            try
            {
                List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();
                list = BLL.SE_MT_FiscalYear_GetAll();

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.Isdisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.Isdisabled.Contains("1")).ToList();
                    gvDisabled.DataBind();

                    gvData.FooterRow.Visible = gvData.PageIndex == 0;

                    LinkButton lbSaveNew = (LinkButton)gvData.FooterRow.FindControl("lbSaveNew");
                    TextBox txtNewFrom = (TextBox)gvData.FooterRow.FindControl("txtNewFrom");
                    TextBox txtNewTo = (TextBox)gvData.FooterRow.FindControl("txtNewTo");

                    lbSaveNew.Style.Add("display", "none");
                    txtNewFrom.Enabled = false;
                    txtNewTo.Enabled = false;

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
                        Entities_SE_FiscalYear entity = new Entities_SE_FiscalYear();
                        entity.RefId = lblId.Text.Trim();
                        entity.Isdisabled = "0";
                        entity.DisabledBy = Session["UserFullName"].ToString() + " - " + DateTime.Now.ToLongDateString();

                        BLL.SE_MT_FiscalYear_IsDisabled(entity);

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
                    Label lblFrom = (Label)e.Row.FindControl("lblFrom");
                    TextBox txtFrom = (TextBox)e.Row.FindControl("txtFrom");
                    Label lblTo = (Label)e.Row.FindControl("lblTo");
                    TextBox txtTo = (TextBox)e.Row.FindControl("txtTo");
                    LinkButton lbSave = (LinkButton)e.Row.FindControl("lbSave");

                    txtFrom.Style.Add("display", "none");
                    txtTo.Style.Add("display", "none");
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

                TextBox txtTo = row.FindControl("txtTo") as TextBox;
                TextBox txtFrom = row.FindControl("txtFrom") as TextBox;
                Label lblId = row.FindControl("lblId") as Label;
                Label lblTo = row.FindControl("lblTo") as Label;
                Label lblFrom = row.FindControl("lblFrom") as Label;

                LinkButton lbEdit = row.FindControl("lbEdit") as LinkButton;
                LinkButton lbSave = row.FindControl("lbSave") as LinkButton;

                TextBox txtNewTo = gvData.FooterRow.FindControl("txtNewTo") as TextBox;
                TextBox txtNewFrom = gvData.FooterRow.FindControl("txtNewFrom") as TextBox;

                LinkButton lbAddNew = gvData.FooterRow.FindControl("lbAddNew") as LinkButton;
                LinkButton lbSaveNew = gvData.FooterRow.FindControl("lbSaveNew") as LinkButton;
                LinkButton lbDisabled = row.FindControl("lbDisabled") as LinkButton;

                if (e.CommandName == "StartEditing_Command")
                {
                    if (lbEdit.Text == "CANCEL")
                    {
                        txtTo.Style.Add("display", "none");
                        txtFrom.Style.Add("display", "none");
                        lbSave.Style.Add("display", "none");

                        lblTo.Style.Add("display", "block");
                        lblFrom.Style.Add("display", "block");
                        lbDisabled.Style.Add("display", "block");

                        lbEdit.Text = "EDIT";
                    }
                    else
                    {
                        lblTo.Style.Add("display", "none");
                        txtTo.Style.Add("display", "block");
                        lblFrom.Style.Add("display", "none");
                        txtFrom.Style.Add("display", "block");

                        lbEdit.Text = "CANCEL";
                        lbSave.Style.Add("display", "block");
                        lbDisabled.Style.Add("display", "none");

                        txtTo.Focus();
                    }
                }

                if (e.CommandName == "StartUpdating_Command")
                {
                    try
                    {
                        Entities_SE_FiscalYear entity = new Entities_SE_FiscalYear();
                        entity.RefId = lblId.Text.Trim();
                        entity.From = txtFrom.Text.Replace("'", "''").ToUpper();
                        entity.To = txtTo.Text.Replace("'", "''").ToUpper();
                        entity.Description = entity.From + " ~ " + entity.To;
                        entity.Updatedby = Session["LcRefId"].ToString();

                        try
                        {

                            if (entity.From.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('From field must not be blank');", true);
                            }
                            else if (entity.To.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('To field must not be blank');", true);
                            }

                            else
                            {
                                BLL.SE_MT_FiscalYear_Append(entity);

                                lblTo.Text = txtTo.Text;
                                txtTo.Style.Add("display", "none");
                                lblFrom.Text = txtFrom.Text;
                                txtFrom.Style.Add("display", "none");

                                lbSave.Style.Add("display", "none");
                                lblFrom.Style.Add("display", "block");
                                lblTo.Style.Add("display", "block");

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
                        txtNewFrom.Enabled = true;
                        txtNewTo.Enabled = true;

                        lbAddNew.Text = "CANCEL";
                        lbSaveNew.Style.Add("display", "block");
                    }
                    else
                    {
                        txtNewFrom.Enabled = false;
                        txtNewTo.Enabled = false;

                        lbAddNew.Text = "ADD NEW";
                        lbSaveNew.Style.Add("display", "none");
                    }
                }

                if (e.CommandName == "SaveNew_Command")
                {

                    try
                    {
                        Entities_SE_FiscalYear entity = new Entities_SE_FiscalYear();
                        entity.From = txtNewFrom.Text.Replace("'", "''").ToUpper();
                        entity.To = txtNewTo.Text.Replace("'", "''").ToUpper();
                        entity.Description = entity.From + " ~ " + entity.To;
                        entity.Addedby = Session["LcRefId"].ToString();

                        try
                        {
                            if (entity.From.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('From field must not be blank');", true);
                            }
                            else if (entity.To.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('To field must not be blank');", true);
                            }
                            else if (BLL.SE_MT_FiscalYear_GetByDescription(entity.Description).Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + entity.Description + " is already exist.');", true);
                            }
                            else
                            {
                                BLL.SE_MT_FiscalYear_Insert(entity);

                                txtNewFrom.Enabled = false;
                                txtNewTo.Enabled = false;

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
                        Entities_SE_FiscalYear entity = new Entities_SE_FiscalYear();
                        entity.RefId = lblId.Text.Trim();
                        entity.Isdisabled = "1";
                        entity.DisabledBy = Session["UserFullName"].ToString() + " - " + DateTime.Now.ToLongDateString();

                        BLL.SE_MT_FiscalYear_IsDisabled(entity);

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
                List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();
                list = BLL.SE_MT_FiscalYear_GetByDescription_Like(txtSearch.Text);

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.Isdisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.Isdisabled.Contains("1")).ToList();
                    gvDisabled.DataBind();

                    gvData.FooterRow.Visible = gvData.PageIndex == 0;

                    LinkButton lbSaveNew = (LinkButton)gvData.FooterRow.FindControl("lbSaveNew");
                    TextBox txtNewFrom = (TextBox)gvData.FooterRow.FindControl("txtNewFrom");
                    TextBox txtNewTo = (TextBox)gvData.FooterRow.FindControl("txtNewTo");

                    lbSaveNew.Style.Add("display", "none");
                    txtNewFrom.Enabled = false;
                    txtNewTo.Enabled = false;

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
