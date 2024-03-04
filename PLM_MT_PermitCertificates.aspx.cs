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
    public partial class PLM_MT_PermitCertificates : System.Web.UI.Page
    {

        BLL_PLM BLL = new BLL_PLM();
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
                List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();
                list = BLL.PLM_MT_PermitCertificates_GetAll();

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.IsDisabled.Contains("1")).ToList();
                    gvDisabled.DataBind();

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
                        //Entities_CRF_Reason entity = new Entities_CRF_Reason();
                        //entity.RefId = lblId.Text.Trim();
                        //entity.IsDisabled = "0";

                        //BLL.CRF_MT_Reason_IsDisabled(entity);

                        //LoadDefault();
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Record has been enabled!');", true);
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
                    //Label lblName = (Label)e.Row.FindControl("lblName");
                    //TextBox txtName = (TextBox)e.Row.FindControl("txtName");
                    //LinkButton lbSave = (LinkButton)e.Row.FindControl("lbSave");

                    //txtName.Style.Add("display", "none");
                    //lbSave.Style.Add("display", "none");

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

                Label lblRefId = row.FindControl("lblRefId") as Label;
                TextBox txtPermitName = row.FindControl("txtPermitName") as TextBox;

                if (e.CommandName == "EditRecord_Command")
                {
                    try
                    {
                        Response.Redirect("PLM_MT_PermitCertificates_Entry.aspx?RefId=" + CryptorEngine.Encrypt(lblRefId.Text.Trim(), true), false);
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                    }
                }

                if (e.CommandName == "DisableRecord_Command")
                {
                    try
                    {
                        Entities_PLM_PermitCertificates entity = new Entities_PLM_PermitCertificates();
                        entity.RefId = lblRefId.Text.Trim();
                        entity.UpdatedBy = Session["LcRefId"].ToString();

                        if (BLL.PLM_MT_PermitCertificates_DisableByRefId(entity).ToString() == "-1")
                        {
                            Session["successMessage"] = "CERTIFICATE NAME : <b>" + txtPermitName.Text.ToUpper() + "</b> HAS BEEN SUCCESSFULLY DISABLED.";
                            Session["successTransactionName"] = "PLM_MT_PermitCertificates";
                            Session["successReturnPage"] = "PLM_MT_PermitCertificates.aspx";

                            Response.Redirect("SuccessPage.aspx");
                        }
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
                //List<Entities_CRF_Reason> list = new List<Entities_CRF_Reason>();
                //list = BLL.CRF_MT_Reason_GetByName_Like(txtSearch.Text);

                //if (list.Count > 0)
                //{
                //    gvData.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                //    gvData.DataBind();

                //    gvDisabled.DataSource = list.Where(item => item.IsDisabled.Contains("1")).ToList();
                //    gvDisabled.DataBind();

                //    gvData.FooterRow.Visible = gvData.PageIndex == 0;

                //    LinkButton lbSaveNew = (LinkButton)gvData.FooterRow.FindControl("lbSaveNew");
                //    TextBox txtNewName = (TextBox)gvData.FooterRow.FindControl("txtNewName");

                //    lbSaveNew.Style.Add("display", "none");
                //    txtNewName.Enabled = false;

                //}
                //else
                //{
                //    LoadDefault();
                //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Cannot find " + txtSearch.Text + "');", true);
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("PLM_MT_PermitCertificates_Entry.aspx?RefId=");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }



    }
}
