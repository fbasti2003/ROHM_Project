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
    public partial class RFQ_RequestPreview : System.Web.UI.Page
    {

        BLL_RFQ BLL = new BLL_RFQ();
        Common COMMON = new Common();

        public string approver_Buyer = string.Empty;
        public string approver_Incharge = string.Empty;
        public string approver_DeptManager = string.Empty;
        public string approver_DivManager = string.Empty;
        string brCounter = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Page.Title = Session["Preview_RFQNo"].ToString().ToUpper();
                    lblRFQNo.Text = Session["Preview_RFQNo"].ToString().ToUpper();
                    lblSection.Text = Session["SectionName"].ToString().ToUpper();
                    lblDepartment.Text = Session["DepartmentName"].ToString().ToUpper();
                    lblDivision.Text = Session["DivisionName"].ToString().ToUpper();

                    lblRequester.Text = Session["Preview_Requester"].ToString().ToUpper() + " " + Session["Preview_DOARequester"].ToString().ToUpper();
                    lblManager.Text = Session["Preview_ProdManager"].ToString().ToUpper() + " " + Session["Preview_DOAProdManager"].ToString().ToUpper();

                    //---------------------------------------------------------------------------------------------------
                    List<Entities_RFQ_RequestEntry> List_EmailAndLocalNumber = new List<Entities_RFQ_RequestEntry>();
                    Entities_RFQ_RequestEntry emailLocalEntity = new Entities_RFQ_RequestEntry();
                    emailLocalEntity.RhRfqNo = Session["Preview_RFQNo"].ToString().ToUpper();

                    List_EmailAndLocalNumber = BLL.RFQ_TRANSACTION_GetRequesterEmailAndLocalNumber_ByRFQNo(emailLocalEntity);

                    if (List_EmailAndLocalNumber != null)
                    {
                        if (List_EmailAndLocalNumber.Count > 0)
                        {
                            foreach (Entities_RFQ_RequestEntry emailLocal in List_EmailAndLocalNumber)
                            {
                                lblEmailAddress.Text = emailLocal.RhEmailAddress;
                            }
                        }
                    }
                    else
                    {
                        lblEmailAddress.Text = string.Empty;
                    }

                    //---------------------------------------------------------------------------------------------------

                    List<Entities_RFQ_RequestEntry> List_TransferHistory = new List<Entities_RFQ_RequestEntry>();
                    Entities_RFQ_RequestEntry entityTransferHistory = new Entities_RFQ_RequestEntry();
                    entityTransferHistory.SearchCriteria = Session["Preview_RFQNo"].ToString().ToUpper();
                    List_TransferHistory = BLL.RFQ_TRANSACTION_HistoryOfUpdates(entityTransferHistory);

                    if (List_TransferHistory != null)
                    {
                        if (List_TransferHistory.Count > 0)
                        {
                            foreach (Entities_RFQ_RequestEntry eHistory in List_TransferHistory)
                            {
                                lblTransferDetails.Visible = true;
                                lblTransferDetails.Text = "<b>TRANSFER DETAILS : </b> " + eHistory.HistoryUpdateWhat.ToUpper() + ", <b>TRANSFER BY : </b> " + eHistory.HistoryUpdatedBy.ToUpper() + ", <b>TRANSFER DATE :</b> " + eHistory.HistoryUpdatedDate.ToUpper();
                            }
                        }
                    }

                    //---------------------------------------------------------------------------------------------------


                    //---------------------------------------------------------------------------------------------------
                    List<Entities_RFQ_RequestEntry> List_SendDates = new List<Entities_RFQ_RequestEntry>();
                    List_SendDates = BLL.RFQ_TRANSACTION_SendDate_ByRFQNo(Session["Preview_RFQNo"].ToString().ToUpper());

                    if (List_SendDates != null)
                    {
                        string sendDates = string.Empty;
                        string currentSendDate = string.Empty;

                        if (List_SendDates.Count > 0)
                        {
                            foreach (Entities_RFQ_RequestEntry entity in List_SendDates)
                            {
                                if (currentSendDate == entity.SendDate)
                                {
                                    // do nothing if the same send date
                                }
                                else
                                {
                                    sendDates += entity.SendDate + ", ";
                                }
                                currentSendDate = entity.SendDate;
                            }
                        }

                        if (!string.IsNullOrEmpty(sendDates))
                        {
                            Session["Preview_DOABuyerSend"] = "[ " + sendDates.Substring(0, (sendDates.Length - 2)).ToString() + " ]";
                        }
                    }

                    //---------------------------------------------------------------------------------------------------

                    lblSender.Text = "<b><i>" + Session["Preview_BuyerSend"].ToString().ToUpper() + "</b></i> " + Session["Preview_DOABuyerSend"].ToString().ToUpper();


                    List<Entities_RFQ_RequestEntry> listDetails = new List<Entities_RFQ_RequestEntry>();
                    listDetails = BLL.RFQ_TRANSACTION_GetRequestDetailsByRFQNoWithUnitPrice_ByRFQNo(Session["Preview_RFQNo"].ToString());

                    if (listDetails != null)
                    {
                        if (listDetails.Count > 0)
                        {
                            gvData.DataSource = listDetails;
                            gvData.DataBind();
                        }

                    }

                    if (listDetails.Count <= 5)
                    {
                        brCounter = "<br/><br/><br/><br/><br/>";
                    }
                    else
                    {
                        brCounter = "<br/>";
                    }

                    //TESTING
                    //Response.Write("TESTING : " + Session["Preview_RFQNo"].ToString() + " - COUNT = " + listDetails.Count.ToString()); 

                    approver_Buyer = brCounter + "<b>" + Session["Preview_Buyer"].ToString().ToUpper() + "</b> <br />" + Session["Preview_DOABuyer"].ToString().ToUpper();
                    approver_Incharge = brCounter + "<b>" + Session["Preview_Incharge"].ToString().ToUpper() + "</b> <br />" + Session["Preview_DOAIncharge"].ToString().ToUpper();
                    approver_DeptManager = brCounter + "<b>" + Session["Preview_DeptManager"].ToString().ToUpper() + "</b> <br />" + Session["Preview_DOADeptManager"].ToString().ToUpper();
                    approver_DivManager = brCounter + "<b>" + Session["Preview_DivManager"].ToString().ToUpper() + "</b> <br />" + Session["Preview_DOADivManager"].ToString().ToUpper();

                    Session["Preview_Requester"] = string.Empty;
                    Session["Preview_DOARequester"] = string.Empty;
                    Session["Preview_SendDate"] = string.Empty;
                    Session["Preview_ProdManager"] = string.Empty;
                    Session["Preview_DOAProdManager"] = string.Empty;
                    Session["Preview_Buyer"] = string.Empty;
                    Session["Preview_DOABuyer"] = string.Empty;
                    Session["Preview_BuyerSend"] = string.Empty;
                    Session["Preview_DOABuyerSend"] = string.Empty;
                    Session["Preview_Incharge"] = string.Empty;
                    Session["Preview_DOAIncharge"] = string.Empty;
                    Session["Preview_DeptManager"] = string.Empty;
                    Session["Preview_DOADeptManager"] = string.Empty;
                    Session["Preview_DivManager"] = string.Empty;
                    Session["Preview_DOADivManager"] = string.Empty;
                    Session["SectionName"] = string.Empty;
                    Session["DepartmentName"] = string.Empty;
                    Session["DivisionName"] = string.Empty;

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }

        }



    }
}
