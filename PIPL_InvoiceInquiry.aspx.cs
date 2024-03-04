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
    public partial class PIPL_InvoiceInquiry : System.Web.UI.Page
    {

        BLL_PIPL BLL = new BLL_PIPL();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    txtFrom.Text = DateTime.Today.AddDays(-7).ToString("MM/dd/yyyy");
                    txtTo.Text = DateTime.Today.AddDays(7).ToString("MM/dd/yyyy");

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
                List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();
                Entities_PIPL_InvoiceEntry status = new Entities_PIPL_InvoiceEntry();

                status.DrFrom = txtFrom.Text.Trim();
                status.DrTo = txtTo.Text.Trim();
                status.Requester = Session["LcRefId"].ToString();
                status.CtrlNo = txtSearch.Text;

                list = null;

                if (txtSearch.Text.Length > 0)
                {
                    if (ddItemStatus.SelectedValue.ToLower() == "pending")
                    {
                        list = BLL.PIPL_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like(status).Where(itm => itm.StatManager == "0" || itm.StatPCManager == "0" || itm.StatIncharge == "0" || itm.StatImpex == "0" || itm.StatManager == null || itm.StatPCManager == null || itm.StatIncharge == null || itm.StatImpex == null).ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "disapproved")
                    {
                        list = BLL.PIPL_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like(status).Where(itm => itm.StatManager == "2" || itm.StatPCManager == "2" || itm.StatIncharge == "2" || itm.StatImpex == "2").ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "approved")
                    {
                        list = BLL.PIPL_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like(status).Where(itm => itm.StatImpex == "1").ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "all")
                    {
                        list = BLL.PIPL_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like(status);
                    }
                }
                else
                {
                    if (ddItemStatus.SelectedValue.ToLower() == "pending")
                    {
                        list = BLL.PIPL_TRANSACTION_RequestStatus_ByDateRange(status).Where(itm => itm.StatManager == "0" || itm.StatPCManager == "0" || itm.StatIncharge == "0" || itm.StatImpex == "0" || itm.StatManager == null || itm.StatPCManager == null || itm.StatIncharge == null || itm.StatImpex == null).ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "disapproved")
                    {
                        list = BLL.PIPL_TRANSACTION_RequestStatus_ByDateRange(status).Where(itm => itm.StatManager == "2" || itm.StatPCManager == "2" || itm.StatIncharge == "2" || itm.StatImpex == "2").ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "approved")
                    {
                        list = BLL.PIPL_TRANSACTION_RequestStatus_ByDateRange(status).Where(itm => itm.StatImpex == "1").ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "all")
                    {
                        list = BLL.PIPL_TRANSACTION_RequestStatus_ByDateRange(status);
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

                LinkButton lblCtrl = row.FindControl("lblCtrl") as LinkButton;

                if (e.CommandName == "lblCtrl_Command")
                {
                    string URL = "~/PIPL_InvoiceDetails.aspx?ControlNo_From_Inquiry=" + CryptorEngine.Encrypt(lblCtrl.Text.Trim(), true);
                    
                    URL = Page.ResolveClientUrl(URL);
                    lblCtrl.OnClientClick = "window.open('" + URL + "'); return false;";
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
                    Label lblManager = (Label)e.Row.FindControl("lblManager");
                    Label lblManagerStat = (Label)e.Row.FindControl("lblManagerStat");
                    Label lblPCManager = (Label)e.Row.FindControl("lblPCManager");
                    Label lblPCManagerStat = (Label)e.Row.FindControl("lblPCManagerStat");
                    Label lblIncharge = (Label)e.Row.FindControl("lblIncharge");
                    Label lblInchargeStat = (Label)e.Row.FindControl("lblInchargeStat");
                    Label lblImpex = (Label)e.Row.FindControl("lblImpex");
                    Label lblImpexStat = (Label)e.Row.FindControl("lblImpexStat");
                    Label lblAccounting = (Label)e.Row.FindControl("lblAccounting");
                    Label lblAccountingStat = (Label)e.Row.FindControl("lblAccountingStat");


                    //-------------- MANAGER ----------------------------------
                    if (lblManagerStat.Text == "0")
                    {
                        lblManager.Text = "PENDING";
                        lblManager.Style.Add("background-color", "#f44336");
                    }
                    if (lblManagerStat.Text == "1")
                    {
                        lblManager.Text = "APPROVED";
                        lblManager.Style.Add("background-color", "#00C851");
                    }
                    if (lblManagerStat.Text == "2")
                    {
                        lblManager.Text = "DISAPPROVED";
                        lblManager.Style.Add("background-color","#ffbb33");
                    }
                    //---------------------------------------------------------


                    //-------------- PC MANAGER -------------------------------
                    if (lblPCManagerStat.Text == "0")
                    {
                        lblPCManager.Text = "PENDING";
                        lblPCManager.Style.Add("background-color", "#f44336");
                    }
                    if (lblPCManagerStat.Text == "1")
                    {
                        lblPCManager.Text = "APPROVED";
                        lblPCManager.Style.Add("background-color", "#00C851");
                    }
                    if (lblPCManagerStat.Text == "2")
                    {
                        lblPCManager.Text = "DISAPPROVED";
                        lblPCManager.Style.Add("background-color", "#ffbb33");
                    }
                    if (lblPCManagerStat.Text == "3")
                    {
                        lblPCManager.Text = "AUTO-APPROVED";
                        lblPCManager.Style.Add("background-color", "#00C851");
                    }
                    //---------------------------------------------------------

                    //-------------- ACCOUNTING -------------------------------
                    if (lblAccountingStat.Text == "0")
                    {
                        lblAccounting.Text = "PENDING";
                        lblAccounting.Style.Add("background-color", "#f44336");
                    }
                    if (lblAccountingStat.Text == "1")
                    {
                        lblAccounting.Text = "APPROVED";
                        lblAccounting.Style.Add("background-color", "#00C851");
                    }
                    if (lblAccountingStat.Text == "2")
                    {
                        lblAccounting.Text = "DISAPPROVED";
                        lblAccounting.Style.Add("background-color", "#ffbb33");
                    }
                    if (lblAccountingStat.Text == "3")
                    {
                        lblAccounting.Text = "AUTO-APPROVED";
                        lblAccounting.Style.Add("background-color", "#00C851");
                    }
                    //---------------------------------------------------------

                    //-------------- INCHARGE ---------------------------------
                    if (lblInchargeStat.Text == "0")
                    {
                        lblIncharge.Text = "PENDING";
                        lblIncharge.Style.Add("background-color", "#f44336");
                    }
                    if (lblInchargeStat.Text == "1")
                    {
                        lblIncharge.Text = "APPROVED";
                        lblIncharge.Style.Add("background-color", "#00C851");
                    }
                    if (lblInchargeStat.Text == "2")
                    {
                        lblIncharge.Text = "DISAPPROVED";
                        lblIncharge.Style.Add("background-color", "#ffbb33");
                    }
                    //---------------------------------------------------------


                    //-------------- IMPEX ------------------------------------
                    if (lblImpexStat.Text == "0")
                    {
                        lblImpex.Text = "PENDING";
                        lblImpex.Style.Add("background-color", "#f44336");
                    }
                    if (lblImpexStat.Text == "1")
                    {
                        lblImpex.Text = "APPROVED";
                        lblImpex.Style.Add("background-color", "#00C851");
                    }
                    if (lblImpexStat.Text == "2")
                    {
                        lblImpex.Text = "DISAPPROVED";
                        lblImpex.Style.Add("background-color", "#ffbb33");
                    }
                    //---------------------------------------------------------


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
        }

    }
}
