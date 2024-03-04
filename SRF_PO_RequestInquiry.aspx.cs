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
using System.IO;

namespace REPI_PUR_SOFRA
{
    public partial class SRF_PO_RequestInquiry : System.Web.UI.Page
    {
        BLL_SRF BLL = new BLL_SRF();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    txtFrom.Text = DateTime.Today.AddDays(-365).ToString("MM/dd/yyyy");
                    txtTo.Text = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");

                    // call submit button to load initial record
                    btnSubmit_Click(sender, e);
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
                List<Entities_SRF_PO_Entry> list = new List<Entities_SRF_PO_Entry>();
                Entities_SRF_PO_Entry status = new Entities_SRF_PO_Entry();

                status.CrFrom = txtFrom.Text.Trim();
                status.CrTo = txtTo.Text.Trim();
                status.Head_Requester = Session["LcRefId"].ToString();
                status.Head_Ctrlno = txtSearch.Text;


                if (txtSearch.Text.Length > 0)
                {
                    Session["Search_From_SRF_PO_Inquiry"] = txtSearch.Text;
                    Response.Redirect("SRF_PO_AllRequest.aspx", false);
                }
                else
                {
                    if (ddItemStatus.SelectedValue.ToLower() == "pending")
                    {
                        list = BLL.SRF_TRANSACTION_PO_AllRequest_ByDateRange(status).Where(itm => (itm.Head_StatProdManager == "0" || itm.Head_StatBuyer == "0" || itm.Head_StatSCIncharge == "0") && itm.Head_Requester == Session["LcRefId"].ToString()).ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "disapproved")
                    {
                        list = BLL.SRF_TRANSACTION_PO_AllRequest_ByDateRange(status).Where(itm => (itm.Head_StatProdManager == "2" || itm.Head_StatBuyer == "2" || itm.Head_StatSCIncharge == "2") && itm.Head_Requester == Session["LcRefId"].ToString()).ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "approved")
                    {
                        list = BLL.SRF_TRANSACTION_PO_AllRequest_ByDateRange(status).Where(itm => (itm.Head_StatSCIncharge == "1" || itm.Head_StatSCIncharge == "-1") && itm.Head_Requester == Session["LcRefId"].ToString()).ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "all")
                    {
                        list = BLL.SRF_TRANSACTION_PO_AllRequest_ByDateRange(status).Where(itm => itm.Head_Requester == Session["LcRefId"].ToString()).ToList();
                    }

                }

                if (list.Count > 0)
                {
                    gvData.Visible = true;
                    gvData.DataSource = list;
                    gvData.DataBind();
                }
                else
                {
                    gvData.Visible = false;
                    gvData.EmptyDataText = "NO RECORD(S) FOUND!";
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

                LinkButton lbCTRLNo = row.FindControl("lbCTRLNo") as LinkButton;

                if (e.CommandName == "lbCTRLNo_Command")
                {
                    string URL = "~/SRF_PO_RequestEntry.aspx?SRFNo_PO_From_Inquiry=" + CryptorEngine.Encrypt(lbCTRLNo.Text.Trim(), true);

                    Response.Redirect(URL, false);
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
                    Label lblStatAll = (Label)e.Row.FindControl("lblStatAll");
                    Label lblStatColor = (Label)e.Row.FindControl("lblStatColor");
                    

                    lblStatAll.Style.Add("background-color", lblStatColor.Text.Trim());

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
            gvData.PageIndex = e.NewPageIndex;
            btnSubmit_Click(sender, e);
        }



    }
}
