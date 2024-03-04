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
//using REPI_PUR_SOFRA.App_Code.BLL;
//using REPI_PUR_SOFRA.App_Code.ENTITIES;

namespace REPI_PUR_SOFRA
{
    public partial class SRF_MT_ICTrays : System.Web.UI.Page
    {
        BLL_SRF BLL = new BLL_SRF();
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
                List<Entities_SRF_PO_ICTrays> list = new List<Entities_SRF_PO_ICTrays>();
                list = BLL.SRF_MT_PullOutICTrays_GetAll();

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.IsDisabled.Contains("1")).ToList();
                    gvDisabled.DataBind();

                    gvData.FooterRow.Visible = gvData.PageIndex == 0;

                    LinkButton lbSaveNew = (LinkButton)gvData.FooterRow.FindControl("lbSaveNew");
                    TextBox txtNewSpecification = (TextBox)gvData.FooterRow.FindControl("txtNewSpecification");
                    TextBox txtNewBoxType = (TextBox)gvData.FooterRow.FindControl("txtNewBoxType");
                    TextBox txtNewSize = (TextBox)gvData.FooterRow.FindControl("txtNewSize");
                    TextBox txtNewMultiplier = (TextBox)gvData.FooterRow.FindControl("txtNewMultiplier");

                    lbSaveNew.Style.Add("display", "none");
                    txtNewSpecification.Enabled = false;
                    txtNewBoxType.Enabled = false;
                    txtNewSize.Enabled = false;
                    txtNewMultiplier.Enabled = false;

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
                List<Entities_SRF_PO_ICTrays> list = new List<Entities_SRF_PO_ICTrays>();
                list = BLL.SRF_MT_PullOutICTrays_GetAll().Where(itm => itm.Specification.Contains(txtSearch.Text)).ToList();

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.IsDisabled.Contains("1")).ToList();
                    gvDisabled.DataBind();

                    gvData.FooterRow.Visible = gvData.PageIndex == 0;

                    LinkButton lbSaveNew = (LinkButton)gvData.FooterRow.FindControl("lbSaveNew");
                    TextBox txtNewSpecification = (TextBox)gvData.FooterRow.FindControl("txtNewSpecification");
                    TextBox txtNewBoxType = (TextBox)gvData.FooterRow.FindControl("txtNewBoxType");
                    TextBox txtNewSize = (TextBox)gvData.FooterRow.FindControl("txtNewSize");
                    TextBox txtNewMultiplier = (TextBox)gvData.FooterRow.FindControl("txtNewMultiplier");

                    lbSaveNew.Style.Add("display", "none");
                    txtNewSpecification.Enabled = false;
                    txtNewBoxType.Enabled = false;
                    txtNewSize.Enabled = false;
                    txtNewMultiplier.Enabled = false;

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

                TextBox txtSpecification = row.FindControl("txtSpecification") as TextBox;
                TextBox txtBoxType = row.FindControl("txtBoxType") as TextBox;
                TextBox txtSize = row.FindControl("txtSize") as TextBox;
                TextBox txtMultiplier = row.FindControl("txtMultiplier") as TextBox;

                Label lblId = row.FindControl("lblId") as Label;
                Label lblSpecification = row.FindControl("lblSpecification") as Label;
                Label lblBoxType = row.FindControl("lblBoxType") as Label;
                Label lblSize = row.FindControl("lblSize") as Label;
                Label lblMultiplier = row.FindControl("lblMultiplier") as Label;

                LinkButton lbEdit = row.FindControl("lbEdit") as LinkButton;
                LinkButton lbSave = row.FindControl("lbSave") as LinkButton;

                TextBox txtNewSpecification = gvData.FooterRow.FindControl("txtNewSpecification") as TextBox;
                TextBox txtNewBoxType = gvData.FooterRow.FindControl("txtNewBoxType") as TextBox;
                TextBox txtNewSize = gvData.FooterRow.FindControl("txtNewSize") as TextBox;
                TextBox txtNewMultiplier = gvData.FooterRow.FindControl("txtNewMultiplier") as TextBox;

                LinkButton lbAddNew = gvData.FooterRow.FindControl("lbAddNew") as LinkButton;
                LinkButton lbSaveNew = gvData.FooterRow.FindControl("lbSaveNew") as LinkButton;
                LinkButton lbDisabled = row.FindControl("lbDisabled") as LinkButton;

                if (e.CommandName == "StartEditing_Command")
                {
                    if (lbEdit.Text == "CANCEL")
                    {
                        txtSpecification.Style.Add("display", "none");
                        txtBoxType.Style.Add("display", "none");
                        txtSize.Style.Add("display", "none");
                        txtMultiplier.Style.Add("display", "none");
                        lbSave.Style.Add("display", "none");

                        lblSpecification.Style.Add("display", "none");
                        lblBoxType.Style.Add("display", "none");
                        lblSize.Style.Add("display", "none");
                        lblMultiplier.Style.Add("display", "none");
                        lbDisabled.Style.Add("display", "block");

                        lbEdit.Text = "EDIT";
                    }
                    else
                    {
                        lblSpecification.Style.Add("display", "none");
                        txtSpecification.Style.Add("display", "block");
                        lblBoxType.Style.Add("display", "none");
                        txtBoxType.Style.Add("display", "block");
                        lblSize.Style.Add("display", "none");
                        txtSize.Style.Add("display", "block");
                        lblMultiplier.Style.Add("display", "none");
                        txtMultiplier.Style.Add("display", "block");

                        lbEdit.Text = "CANCEL";
                        lbSave.Style.Add("display", "block");
                        lbDisabled.Style.Add("display", "none");

                        txtSpecification.Focus();
                    }
                }

                if (e.CommandName == "StartUpdating_Command")
                {
                    try
                    {
                        Entities_SRF_PO_ICTrays entity = new Entities_SRF_PO_ICTrays();
                        entity.RefId = lblId.Text.Trim();
                        entity.Specification = txtSpecification.Text.Replace("'", "''").ToUpper();
                        entity.BoxType = txtBoxType.Text.Replace("'", "''").ToUpper();
                        entity.Size = txtSize.Text.Replace("'", "''").ToUpper();
                        entity.Multiplier = txtMultiplier.Text.Replace("'", "''");
                        entity.UpdatedBy = Session["LcRefId"].ToString();

                        try
                        {

                            if (entity.Specification.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Specification must not be blank');", true);
                            }
                            else if (entity.BoxType.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('BoxType of box must not be blank');", true);
                            }
                            else if (entity.Size.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Size of box must not be blank');", true);
                            }
                            else if (entity.Multiplier.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Multiplier of box must not be blank');", true);
                            }

                            else
                            {
                                BLL.SRF_MT_PullOutICTrays_Append(entity);

                                lblSpecification.Text = txtSpecification.Text;
                                txtSpecification.Style.Add("display", "none");
                                lblBoxType.Text = txtBoxType.Text;
                                txtBoxType.Style.Add("display", "none");
                                lblSize.Text = txtSize.Text;
                                txtSize.Style.Add("display", "none");
                                lblMultiplier.Text = txtMultiplier.Text;
                                txtMultiplier.Style.Add("display", "none");

                                lblSpecification.Style.Add("display", "none");
                                lblBoxType.Style.Add("display", "block");
                                lblSize.Style.Add("display", "block");
                                lblMultiplier.Style.Add("display", "block");

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
                        txtNewSpecification.Enabled = true;
                        txtNewBoxType.Enabled = true;
                        txtNewSize.Enabled = true;
                        txtNewMultiplier.Enabled = true;

                        lbAddNew.Text = "CANCEL";
                        lbSaveNew.Style.Add("display", "block");
                    }
                    else
                    {
                        txtNewSpecification.Enabled = false;
                        txtNewBoxType.Enabled = false;
                        txtNewSize.Enabled = false;
                        txtNewMultiplier.Enabled = false;

                        lbAddNew.Text = "ADD NEW";
                        lbSaveNew.Style.Add("display", "none");
                    }
                }

                if (e.CommandName == "SaveNew_Command")
                {

                    try
                    {
                        Entities_SRF_PO_ICTrays entity = new Entities_SRF_PO_ICTrays();
                        entity.Specification = txtNewSpecification.Text.Replace("'", "''").ToUpper();
                        entity.BoxType = txtNewBoxType.Text.Replace("'", "''").ToUpper();
                        entity.Size= txtNewSize.Text.Replace("'", "''").ToUpper();
                        entity.Multiplier = txtNewMultiplier.Text.Replace("'", "''");
                        entity.AddedBy = Session["LcRefId"].ToString();

                        try
                        {
                            if (entity.Specification.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Specification must not be blank');", true);
                            }
                            else if (entity.BoxType.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('BoxType of box must not be blank');", true);
                            }
                            else if (entity.Size.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Size of box must not be blank');", true);
                            }
                            else if (entity.Multiplier.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Multiplier of box must not be blank');", true);
                            }

                            else
                            {
                                BLL.SRF_MT_PullOutICTrays_Insert(entity);

                                txtNewSpecification.Enabled = false;
                                txtNewBoxType.Enabled = false;
                                txtNewSize.Enabled = false;
                                txtNewMultiplier.Enabled = false;

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
                        Entities_SRF_PO_ICTrays entity = new Entities_SRF_PO_ICTrays();
                        entity.RefId = lblId.Text.Trim();
                        entity.IsDisabled = "1";

                        BLL.SRF_MT_PullOutICTrays_IsDisabled(entity);

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

        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblSpecification = (Label)e.Row.FindControl("lblSpecification");
                    TextBox txtSpecification = (TextBox)e.Row.FindControl("txtSpecification");
                    Label lblBoxType = (Label)e.Row.FindControl("lblBoxType");
                    TextBox txtBoxType = (TextBox)e.Row.FindControl("txtBoxType");
                    Label lblSize = (Label)e.Row.FindControl("lblSize");
                    TextBox txtSize = (TextBox)e.Row.FindControl("txtSize");
                    Label lblMultiplier = (Label)e.Row.FindControl("lblMultiplier");
                    TextBox txtMultiplier = (TextBox)e.Row.FindControl("txtMultiplier");
                    LinkButton lbSave = (LinkButton)e.Row.FindControl("lbSave");

                    txtSpecification.Style.Add("display", "none");
                    txtBoxType.Style.Add("display", "none");
                    txtSize.Style.Add("display", "none");
                    txtMultiplier.Style.Add("display", "none");
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
                        Entities_SRF_PO_ICTrays entity = new Entities_SRF_PO_ICTrays();
                        entity.RefId = lblId.Text.Trim();
                        entity.IsDisabled = "0";

                        BLL.SRF_MT_PullOutICTrays_IsDisabled(entity);

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





    }
}
