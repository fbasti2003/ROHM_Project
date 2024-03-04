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
    public partial class RFQ_MyRequesterItems : System.Web.UI.Page
    {

        BLL_RFQ BLL = new BLL_RFQ();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["ProductionApprovalAccess"].ToString().Trim()))
                        {
                            txtFrom.Text = DateTime.Today.AddDays(-7).ToString("MM/dd/yyyy");
                            txtTo.Text = DateTime.Today.AddDays(7).ToString("MM/dd/yyyy");

                            // call submit button to load initial record
                            btnSubmit_Click(sender, e);
                        }
                        else
                        {
                            Response.Redirect("Default.aspx");
                        }
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

                List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                entity.DrFrom = txtFrom.Text.Trim();
                entity.DrTo = txtTo.Text.Trim();
                entity.RhDepartment = Session["Department"].ToString();
                entity.Rfqno = txtSearch.Text;

                list = null;

                if (!string.IsNullOrEmpty(txtSearch.Text) || txtSearch.Text.Length > 0)
                {
                    list = BLL.RFQ_TRANSACTION_MyRequestersItem_ByDateRange(entity).Where(itm => itm.Rfqno.Contains(txtSearch.Text)).ToList();
                }
                else
                {
                    list = BLL.RFQ_TRANSACTION_MyRequestersItem_ByDateRange(entity);
                }

                if (list != null || list.Count > 0)
                {
                    gvData.DataSource = list;
                    gvData.DataBind();
                }

                divOpacity.Style.Add("opacity", "1");
                divLoader.Style.Add("display", "none");

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvResponseProd_Command(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                LinkButton lbAttachment = row.FindControl("lbAttachment") as LinkButton;
                Label lblRFQNoProd = row.FindControl("lblRFQNoProd") as Label;


                if (e.CommandName == "ATT_Command")
                {
                    Response.Redirect("/IO_Request/" + lblRFQNoProd.Text.Trim() + "/" + lbAttachment.Text, false);
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

                LinkButton lblView = row.FindControl("lblView") as LinkButton;
                Label lblRFQNo = row.FindControl("lblRFQNo") as Label;
                Label lblTransDate = row.FindControl("lblTransDate") as Label;
                Label lblRequester = row.FindControl("lblRequester") as Label;
                Label lblCategory = row.FindControl("lblCategory") as Label;
                Label lblStatAll = row.FindControl("lblStatAll") as Label;
                GridView gvResponseProd = row.FindControl("gvResponseProd") as GridView;


                if (e.CommandName == "lblView_Command")
                {
                    //string URL = "~/RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(lblRFQNo.Text.Trim(), true);                    

                    //Session["Requester_From_Inquiry"] = lblRequester.Text;
                    //Session["TransDate_From_Inquiry"] = lblTransDate.Text;
                    //Session["Category_From_Inquiry"] = lblCategory.Text.Trim().ToUpper();
                    //Session["btnPreview_Visibility"] = lblStatAll.Text == "APPROVED" ? "true" : "false";
                    //Session["btnUpdate_Visibility"] = lblStatAll.Text == "FOR PRODUCTION MANAGER APPROVAL" ? "true" : "false";

                    ////URL = Page.ResolveClientUrl(URL);
                    ////lblView.OnClientClick = "window.open('" + URL + "'); return false;";

                    //Response.Redirect("RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(lblRFQNo.Text.Trim(), true), false);

                    if (lblView.Text.ToUpper() == "DETAILS")
                    {
                        lblView.Text = "CLOSE DETAILS";

                        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                        list = BLL.RFQ_TRANSACTION_GetRequestDetailsByRFQNo(lblRFQNo.Text.Trim());

                        if (list != null)
                        {
                            if (list.Count > 0)
                            {
                                gvResponseProd.DataSource = list;
                                gvResponseProd.DataBind();
                                gvResponseProd.Visible = true;
                            }
                        }
                        else
                        {
                            gvResponseProd.Visible = true;
                            gvResponseProd.EmptyDataText = "NO SUPPLIER RESPONSE";
                        }
                    }
                    else
                    {
                        lblView.Text = "DETAILS";
                        gvResponseProd.Visible = false;
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
            try
            {
                gvData.PageIndex = e.NewPageIndex;
                btnSubmit_Click(sender, e);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


        private string ProductionManagerStatus(string statusNumber)
        {
            string ret = string.Empty;

            if (statusNumber == "1")
            {
                ret = "APPROVED";
            }
            if (statusNumber == "2")
            {
                ret = "DISAPPROVED";
            }
            if (statusNumber == "0" || String.IsNullOrEmpty(statusNumber) || statusNumber.Length <= 0)
            {
                ret = "PENDING";
            }

            return ret;

        }

        private string StatusColorCoding(string statusNumber)
        {
            string ret = string.Empty;

            if (statusNumber == "1")
            {
                ret = "#00C851";
            }
            if (statusNumber == "2")
            {
                ret = "#ffbb33";
            }
            if (statusNumber == "0" || String.IsNullOrEmpty(statusNumber) || statusNumber.Length <= 0)
            {
                ret = "#f44336";
            }

            return ret;

        }

    }
}
