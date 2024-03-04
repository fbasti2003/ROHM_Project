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
    public partial class URF_AllRequest : System.Web.UI.Page
    {

        BLL_URF BLL = new BLL_URF();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (!IsPostBack)
                    {

                        if (Session["Search_From_URF_Inquiry"] != null)
                        {
                            if (!string.IsNullOrEmpty(Session["Search_From_URF_Inquiry"].ToString()))
                            {

                                txtSearch.Text = Session["Search_From_URF_Inquiry"].ToString().TrimStart().TrimEnd();
                                Session["Search_From_URF_Inquiry"] = null;
                            }
                        }

                        btnSubmit_Click(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool supplyChain = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim());

                List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();
                Entities_URF_RequestEntry entity = new Entities_URF_RequestEntry();

                list = null;
                entity.Criteria = txtSearch.Text;

                if (supplyChain)
                {
                    list = BLL.URF_TRANSACTION_AllRequest(entity);
                }
                else
                {
                    list = BLL.URF_TRANSACTION_AllRequest(entity).Where(itm => itm.RhDivision == Session["Division"].ToString()).ToList();
                }

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        gvData.DataSource = list;
                        gvData.DataBind();
                        gvData.Visible = true;
                    }
                    else
                    {
                        gvData.Visible = false;
                    }
                }
                else
                {
                    gvData.Visible = false;
                }


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

                LinkButton linkCTRLNO = row.FindControl("linkCTRLNO") as LinkButton;

                if (e.CommandName == "linkCTRLNO_Command")
                {
                    Response.Redirect("URF_RequestEntry_New.aspx?URFNo_From_Inquiry=" + CryptorEngine.Encrypt(linkCTRLNO.Text.Trim(), true), false);
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
