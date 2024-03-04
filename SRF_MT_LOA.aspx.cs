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


namespace REPI_PUR_SOFRA
{
    public partial class SRF_MT_LOA : System.Web.UI.Page
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
                List<Entities_SRF_LOA> list = new List<Entities_SRF_LOA>();
                list = BLL.SRF_MT_LOA_GetAll();

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.IsDisabled.Contains("1")).ToList();
                    gvDisabled.DataBind();

                    gvData.FooterRow.Visible = gvData.PageIndex == 0;

                    LinkButton lbSaveNew = (LinkButton)gvData.FooterRow.FindControl("lbSaveNew");
                    TextBox txtNewLoaNo = (TextBox)gvData.FooterRow.FindControl("txtNewLoaNo");
                    TextBox txtNewLoaBalance = (TextBox)gvData.FooterRow.FindControl("txtNewLoaBalance");
                    TextBox txtNewLoaPriceValue = (TextBox)gvData.FooterRow.FindControl("txtNewLoaPriceValue");
                    TextBox txtNewMaturityDate = (TextBox)gvData.FooterRow.FindControl("txtNewMaturityDate");
                    TextBox txtNewRemarks = (TextBox)gvData.FooterRow.FindControl("txtNewRemarks");

                    lbSaveNew.Style.Add("display", "none");
                    txtNewLoaNo.Enabled = false;
                    txtNewLoaBalance.Enabled = false;
                    txtNewLoaPriceValue.Enabled = false;
                    txtNewMaturityDate.Enabled = false;
                    txtNewRemarks.Enabled = false;

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
                        Entities_SRF_LOA entity = new Entities_SRF_LOA();
                        entity.RefId = lblId.Text.Trim();
                        entity.IsDisabled = "0";

                        BLL.SRF_MT_LOA_IsDisabled(entity);

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
                    Label lblLoaNo = (Label)e.Row.FindControl("lblLoaNo");
                    TextBox txtLoaNo = (TextBox)e.Row.FindControl("txtLoaNo");

                    Label lblLoaBalance = (Label)e.Row.FindControl("lblLoaBalance");
                    TextBox txtLoaBalance = (TextBox)e.Row.FindControl("txtLoaBalance");

                    Label lblLoaPriceValue = (Label)e.Row.FindControl("lblLoaPriceValue");
                    TextBox txtLoaPriceValue = (TextBox)e.Row.FindControl("txtLoaPriceValue");

                    Label lblMaturityDate = (Label)e.Row.FindControl("lblMaturityDate");
                    TextBox txtMaturityDate = (TextBox)e.Row.FindControl("txtMaturityDate");

                    Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
                    TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");

                    LinkButton lbSave = (LinkButton)e.Row.FindControl("lbSave");

                    txtLoaNo.Style.Add("display", "none");
                    txtLoaBalance.Style.Add("display", "none");
                    txtLoaPriceValue.Style.Add("display", "none");
                    txtMaturityDate.Style.Add("display", "none");
                    txtRemarks.Style.Add("display", "none");

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

                TextBox txtLoaNo = row.FindControl("txtLoaNo") as TextBox;
                Label lblLoaNo = row.FindControl("lblLoaNo") as Label;
                TextBox txtNewLoaNo = gvData.FooterRow.FindControl("txtNewLoaNo") as TextBox;

                TextBox txtLoaBalance = row.FindControl("txtLoaBalance") as TextBox;
                Label lblLoaBalance = row.FindControl("lblLoaBalance") as Label;
                TextBox txtNewLoaBalance = gvData.FooterRow.FindControl("txtNewLoaBalance") as TextBox;

                TextBox txtLoaPriceValue = row.FindControl("txtLoaPriceValue") as TextBox;
                Label lblLoaPriceValue = row.FindControl("lblLoaPriceValue") as Label;
                TextBox txtNewLoaPriceValue = gvData.FooterRow.FindControl("txtNewLoaPriceValue") as TextBox;

                TextBox txtMaturityDate = row.FindControl("txtMaturityDate") as TextBox;
                Label lblMaturityDate = row.FindControl("lblMaturityDate") as Label;
                TextBox txtNewMaturityDate = gvData.FooterRow.FindControl("txtNewMaturityDate") as TextBox;

                TextBox txtRemarks = row.FindControl("txtRemarks") as TextBox;
                Label lblRemarks = row.FindControl("lblRemarks") as Label;
                TextBox txtNewRemarks = gvData.FooterRow.FindControl("txtNewRemarks") as TextBox;

                Label lblId = row.FindControl("lblId") as Label;                

                LinkButton lbEdit = row.FindControl("lbEdit") as LinkButton;
                LinkButton lbSave = row.FindControl("lbSave") as LinkButton;                

                LinkButton lbAddNew = gvData.FooterRow.FindControl("lbAddNew") as LinkButton;
                LinkButton lbSaveNew = gvData.FooterRow.FindControl("lbSaveNew") as LinkButton;
                LinkButton lbDisabled = row.FindControl("lbDisabled") as LinkButton;

                if (e.CommandName == "StartEditing_Command")
                {
                    if (lbEdit.Text == "CANCEL")
                    {
                        lbSave.Style.Add("display", "none");
                        lbDisabled.Style.Add("display", "block");

                        lblLoaNo.Style.Add("display", "block");
                        txtLoaNo.Style.Add("display", "none");
                        lblLoaBalance.Style.Add("display", "block");
                        txtLoaBalance.Style.Add("display", "none");
                        lblLoaPriceValue.Style.Add("display", "block");
                        txtLoaPriceValue.Style.Add("display", "none");
                        lblMaturityDate.Style.Add("display", "block");
                        txtMaturityDate.Style.Add("display", "none");
                        lblRemarks.Style.Add("display", "block");
                        txtRemarks.Style.Add("display", "none");

                        lbEdit.Text = "EDIT";
                    }
                    else
                    {
                        lblLoaNo.Style.Add("display", "none");
                        txtLoaNo.Style.Add("display", "block");
                        lblLoaBalance.Style.Add("display", "none");
                        txtLoaBalance.Style.Add("display", "block");
                        lblLoaPriceValue.Style.Add("display", "none");
                        txtLoaPriceValue.Style.Add("display", "block");
                        lblMaturityDate.Style.Add("display", "none");
                        txtMaturityDate.Style.Add("display", "block");
                        lblRemarks.Style.Add("display", "none");
                        txtRemarks.Style.Add("display", "block");

                        lbEdit.Text = "CANCEL";
                        lbSave.Style.Add("display", "block");
                        lbDisabled.Style.Add("display", "none");

                        lblLoaNo.Focus();
                    }
                }

                if (e.CommandName == "StartUpdating_Command")
                {
                    try
                    {
                        Entities_SRF_LOA entity = new Entities_SRF_LOA();
                        entity.RefId = lblId.Text.Trim();
                        entity.LoaNo = txtLoaNo.Text.Replace("'", "''").ToUpper();
                        entity.Balance = txtLoaBalance.Text.Replace("'", "''").ToUpper();
                        entity.LoaPriceValue = txtLoaPriceValue.Text.Replace("'", "''").ToUpper();
                        entity.MaturityDate = txtMaturityDate.Text.Replace("'", "''").ToUpper();
                        entity.Remarks = txtRemarks.Text.Replace("'", "''").ToUpper();
                        entity.UpdatedBy = Session["LcRefId"].ToString();

                        try
                        {

                            if (entity.LoaNo.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('LOANo must not be blank');", true);
                            }

                            else
                            {
                                BLL.SRF_MT_LOA_Append(entity);

                                lblLoaNo.Text = txtLoaNo.Text;
                                txtLoaNo.Style.Add("display", "none");
                                lblLoaNo.Style.Add("display", "block");

                                lblLoaBalance.Text = txtLoaBalance.Text;
                                txtLoaBalance.Style.Add("display", "none");
                                lblLoaBalance.Style.Add("display", "block");

                                lblLoaPriceValue.Text = txtLoaPriceValue.Text;
                                txtLoaPriceValue.Style.Add("display", "none");
                                lblLoaPriceValue.Style.Add("display", "block");

                                lblMaturityDate.Text = txtMaturityDate.Text;
                                txtMaturityDate.Style.Add("display", "none");
                                lblMaturityDate.Style.Add("display", "block");

                                lblRemarks.Text = txtRemarks.Text;
                                txtRemarks.Style.Add("display", "none");
                                lblRemarks.Style.Add("display", "block");

                                lbSave.Style.Add("display", "none");                                

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
                        txtNewLoaNo.Enabled = true;
                        txtNewLoaBalance.Enabled = true;
                        txtNewLoaPriceValue.Enabled = true;
                        txtNewMaturityDate.Enabled = true;
                        txtNewRemarks.Enabled = true;

                        lbAddNew.Text = "CANCEL";
                        lbSaveNew.Style.Add("display", "block");
                    }
                    else
                    {
                        txtNewLoaNo.Enabled = false;
                        txtNewLoaBalance.Enabled = false;
                        txtNewLoaPriceValue.Enabled = false;
                        txtNewMaturityDate.Enabled = false;
                        txtNewRemarks.Enabled = false;

                        lbAddNew.Text = "ADD NEW";
                        lbSaveNew.Style.Add("display", "none");
                    }
                }

                if (e.CommandName == "SaveNew_Command")
                {

                    try
                    {
                        Entities_SRF_LOA entity = new Entities_SRF_LOA();
                        entity.LoaNo = txtNewLoaNo.Text.Replace("'", "''").ToUpper();
                        entity.Balance = txtNewLoaBalance.Text.Replace("'", "''").ToUpper();
                        entity.LoaPriceValue = txtNewLoaPriceValue.Text.Replace("'", "''").ToUpper();
                        entity.MaturityDate = txtNewMaturityDate.Text.Replace("'", "''").ToUpper();
                        entity.Remarks = txtNewRemarks.Text.Replace("'", "''").ToUpper();
                        entity.AddedBy = Session["LcRefId"].ToString();

                        try
                        {
                            if (entity.LoaNo.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('LOANo must not be blank');", true);
                            }
                            else if (BLL.SRF_MT_LOA_GetByName(entity.LoaNo).Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + entity.Name + " is already exist.');", true);
                            }
                            else
                            {
                                BLL.SRF_MT_LOA_Insert(entity);

                                txtNewLoaNo.Enabled = false;
                                txtNewLoaBalance.Enabled = false;
                                txtNewLoaPriceValue.Enabled = false;
                                txtNewMaturityDate.Enabled = false;
                                txtNewRemarks.Enabled = false;

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
                        Entities_SRF_LOA entity = new Entities_SRF_LOA();
                        entity.RefId = lblId.Text.Trim();
                        entity.IsDisabled = "1";

                        BLL.SRF_MT_LOA_IsDisabled(entity);

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
                List<Entities_SRF_LOA> list = new List<Entities_SRF_LOA>();
                list = BLL.SRF_MT_LOA_GetByName_Like(txtSearch.Text);

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.IsDisabled.Contains("1")).ToList();
                    gvDisabled.DataBind();

                    gvData.FooterRow.Visible = gvData.PageIndex == 0;

                    LinkButton lbSaveNew = (LinkButton)gvData.FooterRow.FindControl("lbSaveNew");
                    TextBox txtNewName = (TextBox)gvData.FooterRow.FindControl("txtNewName");

                    lbSaveNew.Style.Add("display", "none");
                    txtNewName.Enabled = false;

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
