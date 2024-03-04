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
    public partial class SRF_MT_PurposeOfPullOut : System.Web.UI.Page
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
                List<Entities_SRF_PurposeOfPullOut> list = new List<Entities_SRF_PurposeOfPullOut>();
                list = BLL.SRF_MT_PurposeOfPullOut_GetAll();

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
                        Entities_SRF_PurposeOfPullOut entity = new Entities_SRF_PurposeOfPullOut();
                        entity.RefId = lblId.Text.Trim();
                        entity.IsDisabled = "0";

                        BLL.SRF_MT_PurposeOfPullOut_IsDisabled(entity);

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
                    Label lblName = (Label)e.Row.FindControl("lblName");
                    TextBox txtName = (TextBox)e.Row.FindControl("txtName");
                    LinkButton lbSave = (LinkButton)e.Row.FindControl("lbSave");

                    txtName.Style.Add("display", "none");
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

                TextBox txtName = row.FindControl("txtName") as TextBox;
                Label lblId = row.FindControl("lblId") as Label;
                Label lblName = row.FindControl("lblName") as Label;

                LinkButton lbEdit = row.FindControl("lbEdit") as LinkButton;
                LinkButton lbSave = row.FindControl("lbSave") as LinkButton;

                TextBox txtNewName = gvData.FooterRow.FindControl("txtNewName") as TextBox;

                LinkButton lbAddNew = gvData.FooterRow.FindControl("lbAddNew") as LinkButton;
                LinkButton lbSaveNew = gvData.FooterRow.FindControl("lbSaveNew") as LinkButton;
                LinkButton lbDisabled = row.FindControl("lbDisabled") as LinkButton;

                if (e.CommandName == "StartEditing_Command")
                {
                    if (lbEdit.Text == "CANCEL")
                    {
                        txtName.Style.Add("display", "none");
                        lbSave.Style.Add("display", "none");

                        lblName.Style.Add("display", "block");
                        lbDisabled.Style.Add("display", "block");

                        lbEdit.Text = "EDIT";
                    }
                    else
                    {
                        lblName.Style.Add("display", "none");
                        txtName.Style.Add("display", "block");

                        lbEdit.Text = "CANCEL";
                        lbSave.Style.Add("display", "block");
                        lbDisabled.Style.Add("display", "none");

                        txtName.Focus();
                    }
                }

                if (e.CommandName == "StartUpdating_Command")
                {
                    try
                    {
                        Entities_SRF_PurposeOfPullOut entity = new Entities_SRF_PurposeOfPullOut();
                        entity.RefId = lblId.Text.Trim();
                        entity.Name = txtName.Text.Replace("'", "''").ToUpper();
                        entity.UpdatedBy = Session["LcRefId"].ToString();

                        try
                        {

                            if (entity.Name.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Name must not be blank');", true);
                            }

                            else
                            {
                                BLL.SRF_MT_PurposeOfPullOut_Append(entity);

                                lblName.Text = txtName.Text;
                                txtName.Style.Add("display", "none");

                                lbSave.Style.Add("display", "none");
                                lblName.Style.Add("display", "block");

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
                        txtNewName.Enabled = true;

                        lbAddNew.Text = "CANCEL";
                        lbSaveNew.Style.Add("display", "block");
                    }
                    else
                    {
                        txtNewName.Enabled = false;

                        lbAddNew.Text = "ADD NEW";
                        lbSaveNew.Style.Add("display", "none");
                    }
                }

                if (e.CommandName == "SaveNew_Command")
                {

                    try
                    {
                        Entities_SRF_PurposeOfPullOut entity = new Entities_SRF_PurposeOfPullOut();
                        entity.Name = txtNewName.Text.Replace("'", "''").ToUpper();
                        entity.AddedBy = Session["LcRefId"].ToString();

                        try
                        {
                            if (entity.Name.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Name must not be blank');", true);
                            }
                            else if (BLL.SRF_MT_PurposeOfPullOut_GetByName(entity.Name).Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + entity.Name + " is already exist.');", true);
                            }
                            else
                            {
                                BLL.SRF_MT_PurposeOfPullOut_Insert(entity);

                                txtNewName.Enabled = false;

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
                        Entities_SRF_PurposeOfPullOut entity = new Entities_SRF_PurposeOfPullOut();
                        entity.RefId = lblId.Text.Trim();
                        entity.IsDisabled = "1";

                        BLL.SRF_MT_PurposeOfPullOut_IsDisabled(entity);

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
                List<Entities_SRF_PurposeOfPullOut> list = new List<Entities_SRF_PurposeOfPullOut>();
                list = BLL.SRF_MT_PurposeOfPullOut_GetByName_Like(txtSearch.Text);

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
