using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;

namespace REPI_PUR_SOFRA
{
    public partial class PIPL_MT_Company : System.Web.UI.Page
    {
        BLL_PIPL BLL = new BLL_PIPL();
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
                List<Entities_PIPL_Company> list = new List<Entities_PIPL_Company>();
                list = BLL.PIPL_MT_Company_GetAll();                

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.IsDisabled.Contains("1")).ToList();
                    gvDisabled.DataBind();

                    gvData.FooterRow.Visible = gvData.PageIndex == 0;

                    LinkButton lbSaveNew = (LinkButton)gvData.FooterRow.FindControl("lbSaveNew");
                    TextBox txtNewName = (TextBox)gvData.FooterRow.FindControl("txtNewName");
                    TextBox txtNewAddress = (TextBox)gvData.FooterRow.FindControl("txtNewAddress");
                    TextBox txtNewPhone = (TextBox)gvData.FooterRow.FindControl("txtNewPhone");
                    TextBox txtNewFax = (TextBox)gvData.FooterRow.FindControl("txtNewFax");

                    lbSaveNew.Style.Add("display", "none");
                    txtNewName.Enabled = false;
                    txtNewAddress.Enabled = false;
                    txtNewPhone.Enabled = false;
                    txtNewFax.Enabled = false;
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
                    Label lblName = (Label)e.Row.FindControl("lblName");
                    Label lblAddress = (Label)e.Row.FindControl("lblAddress");
                    Label lblPhone = (Label)e.Row.FindControl("lblPhone");
                    Label lblFax = (Label)e.Row.FindControl("lblFax");

                    lblName.Text = lblName.Text.Length > 20 ? lblName.Text = COMMON.Concatinate(lblName.Text, 20) + "..." : lblName.Text;
                    lblAddress.Text = lblAddress.Text.Length > 75 ? lblAddress.Text = COMMON.Concatinate(lblAddress.Text, 75) + "..." : lblAddress.Text;
                    lblPhone.Text = lblPhone.Text.Length > 28 ? lblPhone.Text = COMMON.Concatinate(lblPhone.Text, 28) + "..." : lblPhone.Text;
                    lblFax.Text = lblFax.Text.Length > 13 ? lblFax.Text = COMMON.Concatinate(lblFax.Text, 13) + "..." : lblFax.Text;

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
                        Entities_PIPL_Company entity = new Entities_PIPL_Company();
                        entity.RefId = lblId.Text.Trim();
                        entity.IsDisabled = "0";

                        BLL.PIPL_MT_Company_IsDisabled(entity);

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
                    Label lblAddress = (Label)e.Row.FindControl("lblAddress");
                    Label lblPhone = (Label)e.Row.FindControl("lblPhone");
                    Label lblFax = (Label)e.Row.FindControl("lblFax");

                    TextBox txtName = (TextBox)e.Row.FindControl("txtName");
                    TextBox txtAddress = (TextBox)e.Row.FindControl("txtAddress");
                    TextBox txtPhone = (TextBox)e.Row.FindControl("txtPhone");
                    TextBox txtFax = (TextBox)e.Row.FindControl("txtFax");

                    LinkButton lbSave = (LinkButton)e.Row.FindControl("lbSave");                    

                    lblName.Text = lblName.Text.Length > 20 ? lblName.Text = COMMON.Concatinate(lblName.Text, 20) + "..." : lblName.Text;
                    lblAddress.Text = lblAddress.Text.Length > 75 ? lblAddress.Text = COMMON.Concatinate(lblAddress.Text, 75) + "..." : lblAddress.Text;
                    lblPhone.Text = lblPhone.Text.Length > 28 ? lblPhone.Text = COMMON.Concatinate(lblPhone.Text, 28) + "..." : lblPhone.Text;
                    lblFax.Text = lblFax.Text.Length > 13 ? lblFax.Text = COMMON.Concatinate(lblFax.Text, 13) + "..." : lblFax.Text;

                    txtName.Style.Add("display", "none");
                    txtAddress.Style.Add("display", "none");
                    txtPhone.Style.Add("display", "none");
                    txtFax.Style.Add("display", "none");
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
                TextBox txtAddress = row.FindControl("txtAddress") as TextBox;
                TextBox txtPhone = row.FindControl("txtPhone") as TextBox;
                TextBox txtFax = row.FindControl("txtFax") as TextBox;

                Label lblId = row.FindControl("lblId") as Label;
                Label lblName = row.FindControl("lblName") as Label;
                Label lblAddress = row.FindControl("lblAddress") as Label;
                Label lblPhone = row.FindControl("lblPhone") as Label;
                Label lblFax = row.FindControl("lblFax") as Label;

                LinkButton lbEdit = row.FindControl("lbEdit") as LinkButton;
                LinkButton lbSave = row.FindControl("lbSave") as LinkButton;

                TextBox txtNewName = gvData.FooterRow.FindControl("txtNewName") as TextBox;
                TextBox txtNewAddress = gvData.FooterRow.FindControl("txtNewAddress") as TextBox;
                TextBox txtNewPhone = gvData.FooterRow.FindControl("txtNewPhone") as TextBox;
                TextBox txtNewFax = gvData.FooterRow.FindControl("txtNewFax") as TextBox;

                LinkButton lbAddNew = gvData.FooterRow.FindControl("lbAddNew") as LinkButton;
                LinkButton lbSaveNew = gvData.FooterRow.FindControl("lbSaveNew") as LinkButton;
                LinkButton lbDisabled = row.FindControl("lbDisabled") as LinkButton;

                if (e.CommandName == "StartEditing_Command")
                {
                    if (lbEdit.Text == "CANCEL")
                    {
                        txtName.Style.Add("display", "none");
                        txtAddress.Style.Add("display", "none");
                        txtPhone.Style.Add("display", "none");
                        txtFax.Style.Add("display", "none");
                        lbSave.Style.Add("display", "none");

                        lblName.Style.Add("display", "block");
                        lblAddress.Style.Add("display", "block");
                        lblPhone.Style.Add("display", "block");
                        lblFax.Style.Add("display", "block");
                        lbDisabled.Style.Add("display", "block");

                        lbEdit.Text = "EDIT";
                    }
                    else
                    {
                        lblName.Style.Add("display", "none");
                        lblAddress.Style.Add("display", "none");
                        lblPhone.Style.Add("display", "none");
                        lblFax.Style.Add("display", "none");

                        txtName.Style.Add("display", "block");
                        txtAddress.Style.Add("display", "block");
                        txtPhone.Style.Add("display", "block");
                        txtFax.Style.Add("display", "block");

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
                        Entities_PIPL_Company entity = new Entities_PIPL_Company();
                        entity.RefId = lblId.Text.Trim();
                        entity.Name = txtName.Text.Replace("'","''").ToUpper();
                        entity.Address = txtAddress.Text.Replace("'", "''");
                        entity.Phone = txtPhone.Text.Replace("'", "''").ToUpper();
                        entity.Fax = txtFax.Text.Replace("'", "''").ToUpper();
                        entity.UpdatedBy = Session["LcRefId"].ToString(); 

                        try
                        {
                            if (entity.Name.Length > 100)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Name must not exceed in to 100 characters');", true);
                            }
                            if (entity.Name.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Name must not be blank');", true);
                            }
                            else if (entity.Address.Length > 200)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Address must not exceed in to 200 characters');", true);
                            }
                            else if (entity.Address.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Address must not be blank');", true);
                            }
                            else if (entity.Phone.Length > 50)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Phone must not exceed in to 50 characters');", true);
                            }
                            else if (entity.Fax.Length > 50)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Fax must not exceed in to 50 characters');", true);
                            }
                            else
                            {
                                BLL.PIPL_MT_Company_Append(entity);

                                lblName.Text = txtName.Text;
                                lblAddress.Text = txtAddress.Text;
                                lblPhone.Text = txtPhone.Text;
                                lblFax.Text = txtFax.Text;

                                txtName.Style.Add("display", "none");
                                txtAddress.Style.Add("display", "none");
                                txtPhone.Style.Add("display", "none");
                                txtFax.Style.Add("display", "none");
                                lbSave.Style.Add("display", "none");

                                lblName.Style.Add("display", "block");
                                lblAddress.Style.Add("display", "block");
                                lblPhone.Style.Add("display", "block");
                                lblFax.Style.Add("display", "block");

                                lbSave.Style.Add("display", "none");
                                lbEdit.Style.Add("display", "block");
                                lbEdit.Text = "EDIT";
                                lbDisabled.Style.Add("display", "block");

                                lblName.Text = lblName.Text.Length > 20 ? lblName.Text = COMMON.Concatinate(lblName.Text, 20) + "..." : lblName.Text;
                                lblAddress.Text = lblAddress.Text.Length > 77 ? lblAddress.Text = COMMON.Concatinate(lblAddress.Text, 77) + "..." : lblAddress.Text;
                                lblPhone.Text = lblPhone.Text.Length > 13 ? lblPhone.Text = COMMON.Concatinate(lblPhone.Text, 13) + "..." : lblPhone.Text;
                                lblFax.Text = lblFax.Text.Length > 13 ? lblFax.Text = COMMON.Concatinate(lblFax.Text, 13) + "..." : lblFax.Text;

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
                        txtNewAddress.Enabled = true;
                        txtNewPhone.Enabled = true;
                        txtNewFax.Enabled = true;

                        lbAddNew.Text = "CANCEL";
                        lbSaveNew.Style.Add("display", "block");
                    }
                    else
                    {
                        txtNewName.Enabled = false;
                        txtNewAddress.Enabled = false;
                        txtNewPhone.Enabled = false;
                        txtNewFax.Enabled = false;

                        lbAddNew.Text = "ADD NEW";
                        lbSaveNew.Style.Add("display", "none");
                    }
                }

                if (e.CommandName == "SaveNew_Command")
                {

                    try
                    {
                        Entities_PIPL_Company entity = new Entities_PIPL_Company();
                        entity.Name = txtNewName.Text.Replace("'", "''").ToUpper();
                        entity.Address = txtNewAddress.Text.Replace("'", "''");
                        entity.Phone = txtNewPhone.Text.Replace("'", "''").ToUpper();
                        entity.Fax = txtNewFax.Text.Replace("'", "''").ToUpper();
                        entity.AddedBy = Session["LcRefId"].ToString();

                        try
                        {
                            if (entity.Name.Length > 100)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Name must not exceed in to 100 characters');", true);
                            }
                            if (entity.Name.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Name must not be blank');", true);
                            }
                            else if (entity.Address.Length > 200)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Address must not exceed in to 200 characters');", true);
                            }
                            else if (entity.Address.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Address must not be blank');", true);
                            }
                            else if (entity.Phone.Length > 50)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Phone must not exceed in to 50 characters');", true);
                            }
                            else if (entity.Fax.Length > 50)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Fax must not exceed in to 50 characters');", true);
                            }
                            else if (BLL.PIPL_MT_Company_GetByName(entity.Name).Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + entity.Name + " is already exist.');", true);
                            }
                            else
                            {
                                BLL.PIPL_MT_Company_Insert(entity);

                                txtNewName.Enabled = false;
                                txtNewAddress.Enabled = false;
                                txtNewPhone.Enabled = false;
                                txtNewFax.Enabled = false;

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
                        Entities_PIPL_Company entity = new Entities_PIPL_Company();
                        entity.RefId = lblId.Text.Trim();
                        entity.IsDisabled = "1";

                        BLL.PIPL_MT_Company_IsDisabled(entity);

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
                List<Entities_PIPL_Company> list = new List<Entities_PIPL_Company>();
                list = BLL.PIPL_MT_Company_GetByName_Like(txtSearch.Text);

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.IsDisabled.Contains("1")).ToList();
                    gvDisabled.DataBind();

                    gvData.FooterRow.Visible = gvData.PageIndex == 0;

                    LinkButton lbSaveNew = (LinkButton)gvData.FooterRow.FindControl("lbSaveNew");
                    TextBox txtNewName = (TextBox)gvData.FooterRow.FindControl("txtNewName");
                    TextBox txtNewAddress = (TextBox)gvData.FooterRow.FindControl("txtNewAddress");
                    TextBox txtNewPhone = (TextBox)gvData.FooterRow.FindControl("txtNewPhone");
                    TextBox txtNewFax = (TextBox)gvData.FooterRow.FindControl("txtNewFax");

                    lbSaveNew.Style.Add("display", "none");
                    txtNewName.Enabled = false;
                    txtNewAddress.Enabled = false;
                    txtNewPhone.Enabled = false;
                    txtNewFax.Enabled = false;

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
