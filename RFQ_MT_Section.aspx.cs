﻿using System;
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
using System.IO;

namespace REPI_PUR_SOFRA
{
    public partial class RFQ_MT_Section : System.Web.UI.Page
    {

        BLL_RFQ BLL = new BLL_RFQ();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["SectionAccess"].ToString().Trim()))
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
                List<Entities_RFQ_Section> list = new List<Entities_RFQ_Section>();
                list = BLL.RFQ_MT_Section_GetAll();

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.IsDisabled.Contains("1")).ToList();
                    gvDisabled.DataBind();

                    gvData.FooterRow.Visible = gvData.PageIndex == 0;

                    LinkButton lbSaveNew = (LinkButton)gvData.FooterRow.FindControl("lbSaveNew");
                    TextBox txtNewCode = (TextBox)gvData.FooterRow.FindControl("txtNewCode");
                    TextBox txtNewRoprosCode = (TextBox)gvData.FooterRow.FindControl("txtNewRoprosCode");
                    TextBox txtNewDescription = (TextBox)gvData.FooterRow.FindControl("txtNewDescription");

                    lbSaveNew.Style.Add("display", "none");
                    txtNewCode.Enabled = false;
                    txtNewDescription.Enabled = false;
                    txtNewRoprosCode.Enabled = false;

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
                        Entities_RFQ_Section entity = new Entities_RFQ_Section();
                        entity.RefId = lblId.Text.Trim();
                        entity.IsDisabled = "0";
                        entity.DisabledBy = string.Empty;

                        BLL.RFQ_MT_Section_IsDisabled(entity);

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
                    Label lblRoprosCode = (Label)e.Row.FindControl("lblRoprosCode");
                    TextBox txtRoprosCode = (TextBox)e.Row.FindControl("txtRoprosCode");
                    Label lblDescription = (Label)e.Row.FindControl("lblDescription");
                    TextBox txtDescription = (TextBox)e.Row.FindControl("txtDescription");
                    LinkButton lbSave = (LinkButton)e.Row.FindControl("lbSave");

                    txtCode.Style.Add("display", "none");
                    txtDescription.Style.Add("display", "none");
                    txtRoprosCode.Style.Add("display", "none");
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

                TextBox txtCode = row.FindControl("txtCode") as TextBox;
                TextBox txtRoprosCode = row.FindControl("txtRoprosCode") as TextBox;
                TextBox txtDescription = row.FindControl("txtDescription") as TextBox;
                Label lblId = row.FindControl("lblId") as Label;
                Label lblCode = row.FindControl("lblCode") as Label;
                Label lblRoprosCode = row.FindControl("lblRoprosCode") as Label;
                Label lblDescription = row.FindControl("lblDescription") as Label;

                LinkButton lbEdit = row.FindControl("lbEdit") as LinkButton;
                LinkButton lbSave = row.FindControl("lbSave") as LinkButton;

                TextBox txtNewCode = gvData.FooterRow.FindControl("txtNewCode") as TextBox;
                TextBox txtNewRoprosCode = gvData.FooterRow.FindControl("txtNewRoprosCode") as TextBox;
                TextBox txtNewDescription = gvData.FooterRow.FindControl("txtNewDescription") as TextBox;

                LinkButton lbAddNew = gvData.FooterRow.FindControl("lbAddNew") as LinkButton;
                LinkButton lbSaveNew = gvData.FooterRow.FindControl("lbSaveNew") as LinkButton;
                LinkButton lbDisabled = row.FindControl("lbDisabled") as LinkButton;

                if (e.CommandName == "StartEditing_Command")
                {
                    if (lbEdit.Text == "CANCEL")
                    {
                        txtCode.Style.Add("display", "none");
                        txtRoprosCode.Style.Add("display", "none");
                        txtDescription.Style.Add("display", "none");
                        lbSave.Style.Add("display", "none");

                        lblCode.Style.Add("display", "block");
                        lblRoprosCode.Style.Add("display", "block");
                        lblDescription.Style.Add("display", "block");
                        lbDisabled.Style.Add("display", "block");

                        lbEdit.Text = "EDIT";
                    }
                    else
                    {
                        lblCode.Style.Add("display", "block");
                        lblRoprosCode.Style.Add("display", "block");
                        //lblCode.Style.Add("display", "none");
                        //txtCode.Style.Add("display", "block");

                        if (Session["Username"].ToString() == "10771-1")
                        {
                            lblCode.Style.Add("display", "none");
                            txtCode.Style.Add("display", "block");
                            lblRoprosCode.Style.Add("display", "none");
                            txtRoprosCode.Style.Add("display", "block");
                        }


                        lblDescription.Style.Add("display", "none");
                        txtDescription.Style.Add("display", "block");

                        lbEdit.Text = "CANCEL";
                        lbSave.Style.Add("display", "block");
                        lbDisabled.Style.Add("display", "none");

                        txtCode.Focus();
                    }
                }

                if (e.CommandName == "StartUpdating_Command")
                {
                    try
                    {
                        Entities_RFQ_Section entity = new Entities_RFQ_Section();
                        entity.RefId = lblId.Text.Trim();
                        entity.Code = txtCode.Text.Replace("'", "''").ToUpper();
                        entity.RoprosCode = txtRoprosCode.Text.Replace("'", "''").ToUpper();
                        entity.Description = txtDescription.Text.Replace("'", "''").ToUpper();
                        entity.UpdatedBy = Session["LcRefId"].ToString();

                        try
                        {

                            if (entity.Code.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Code must not be blank');", true);
                            }
                            else if (entity.Description.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Description must not be blank');", true);
                            }

                            else
                            {
                                BLL.RFQ_MT_Section_Append(entity);

                                lblCode.Text = txtCode.Text;
                                txtCode.Style.Add("display", "none");
                                lblRoprosCode.Text = txtCode.Text;
                                txtRoprosCode.Style.Add("display", "none");
                                lblDescription.Text = txtDescription.Text;
                                txtDescription.Style.Add("display", "none");

                                lbSave.Style.Add("display", "none");
                                lblCode.Style.Add("display", "block");
                                lblRoprosCode.Style.Add("display", "block");
                                lblDescription.Style.Add("display", "block");

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
                        txtNewCode.Enabled = true;
                        txtNewRoprosCode.Enabled = true;
                        txtNewDescription.Enabled = true;

                        lbAddNew.Text = "CANCEL";
                        lbSaveNew.Style.Add("display", "block");
                    }
                    else
                    {
                        txtNewCode.Enabled = false;
                        txtNewRoprosCode.Enabled = false;
                        txtNewDescription.Enabled = false;

                        lbAddNew.Text = "ADD NEW";
                        lbSaveNew.Style.Add("display", "none");
                    }
                }

                if (e.CommandName == "SaveNew_Command")
                {

                    try
                    {
                        Entities_RFQ_Section entity = new Entities_RFQ_Section();
                        entity.Code = txtNewCode.Text.Replace("'", "''").ToUpper();
                        entity.RoprosCode = txtNewRoprosCode.Text.Replace("'", "''").ToUpper();
                        entity.Description = txtNewDescription.Text.Replace("'", "''").ToUpper();
                        entity.AddedBy = Session["LcRefId"].ToString();

                        try
                        {
                            if (entity.Code.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Code must not be blank');", true);
                            }
                            else if (entity.Description.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Description must not be blank');", true);
                            }
                            else if (BLL.RFQ_MT_Section_GetByDescription(entity.Description).Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + entity.Description + " is already exist.');", true);
                            }
                            else
                            {
                                BLL.RFQ_MT_Section_Insert(entity);

                                txtNewCode.Enabled = false;
                                txtNewRoprosCode.Enabled = false;
                                txtNewDescription.Enabled = false;

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
                        Entities_RFQ_Section entity = new Entities_RFQ_Section();
                        entity.RefId = lblId.Text.Trim();
                        entity.IsDisabled = "1";
                        entity.DisabledBy = DateTime.Now.ToLongDateString() + " - " + Session["UserFullName"].ToString();

                        BLL.RFQ_MT_Section_IsDisabled(entity);

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
                List<Entities_RFQ_Section> list = new List<Entities_RFQ_Section>();
                list = BLL.RFQ_MT_Section_GetByDescription_Like(txtSearch.Text);

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.IsDisabled.Contains("1")).ToList();
                    gvDisabled.DataBind();

                    gvData.FooterRow.Visible = gvData.PageIndex == 0;

                    LinkButton lbSaveNew = (LinkButton)gvData.FooterRow.FindControl("lbSaveNew");
                    TextBox txtNewCode = (TextBox)gvData.FooterRow.FindControl("txtNewCode");
                    TextBox txtNewRoprosCode = (TextBox)gvData.FooterRow.FindControl("txtNewRoprosCode");
                    TextBox txtNewDescription = (TextBox)gvData.FooterRow.FindControl("txtNewDescription");

                    lbSaveNew.Style.Add("display", "none");
                    txtNewCode.Enabled = false;
                    txtNewRoprosCode.Enabled = false;
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


        private void LoadExportData()
        {
            try
            {
                List<Entities_RFQ_Section> list = new List<Entities_RFQ_Section>();
                list = BLL.RFQ_MT_Section_GetAll_Export();

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
            "attachment;filename=SectionTable.xls");
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