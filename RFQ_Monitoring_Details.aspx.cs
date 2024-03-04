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
using System.Data.Common;


namespace REPI_PUR_SOFRA
{
    public partial class RFQ_Monitoring_Details : System.Web.UI.Page
    {
        BLL_RFQ BLL = new BLL_RFQ();
        BLL_Common BLL_COMMON = new BLL_Common();

        Common COMMON = new Common();


        public string tabAttachment = string.Empty;
        public string tabAttachmentContents = string.Empty;
        public string tabAttachmentSupplier = string.Empty;
        public string tabAttachmentContentsSupplier = string.Empty;
        public string supplierResponseDetails = string.Empty;

        public string supplierAttachment = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LcRefId"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["RFQNo_From_Inquiry"].ToString()))
                    {
                        string rfqNo = CryptorEngine.Decrypt(Request.QueryString["RFQNo_From_Inquiry"].ToString().Replace(" ", "+"), true);

                        lblRFQNo.Text = rfqNo;
                        lblRFQNoForDetails.Text = rfqNo;
                        lblSupplierResponseRFQNo.Text = rfqNo;
                        Page.Title = rfqNo;

                        LoadDefault(rfqNo);                        
                    }
                }
            }


        }

        protected void imgWho_Click(object sender, EventArgs e)
        {
            try
            {
                string possibleApprovers = string.Empty;

                //---------------------------------------------------------------------------------------------------
                List<Entities_Common_SystemUsers> list_PossibleApprover = new List<Entities_Common_SystemUsers>();
                list_PossibleApprover = BLL_COMMON.getPossible_RFQ_Approver_ByTransactionAndDepartment(Session["DepartmentCode_From_Inquiry"].ToString(), ConfigurationManager.AppSettings["ProductionApprovalAccess"].ToString());

                if (list_PossibleApprover != null)
                {
                    if (list_PossibleApprover.Count > 0)
                    {
                        foreach (Entities_Common_SystemUsers pUser in list_PossibleApprover)
                        {
                            possibleApprovers += "[" + pUser.FullName + "], ";
                        }
                    }
                }
                //---------------------------------------------------------------------------------------------------

                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ApproverMessage('" + possibleApprovers + "');", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        private void LoadDefault(string rfqno)
        {
            try
            {
                string buyerSending = string.Empty;

                //---------------------------------------------------------------------------------------------------
                if (Session["Requester_From_Inquiry"] != null)
                {
                    lblRequester.Text = Session["Requester_From_Inquiry"].ToString();
                    Session["Preview_Requester"] = lblRequester.Text;
                }
                if (Session["TransDate_From_Inquiry"] != null)
                {
                    lblDOARequester.Text = Session["TransDate_From_Inquiry"].ToString();
                    Session["Preview_DOARequester"] = lblDOARequester.Text;
                }

                Session["Preview_RFQNo"] = rfqno;
                

                //---------------------------------------------------------------------------------------------------
                List<Entities_RFQ_RequestEntry> List_EmailAndLocalNumber = new List<Entities_RFQ_RequestEntry>();
                Entities_RFQ_RequestEntry emailLocalEntity = new Entities_RFQ_RequestEntry();
                emailLocalEntity.RhRfqNo = rfqno;

                List_EmailAndLocalNumber = BLL.RFQ_TRANSACTION_GetRequesterEmailAndLocalNumber_ByRFQNo(emailLocalEntity);

                if (List_EmailAndLocalNumber != null)
                {
                    if (List_EmailAndLocalNumber.Count > 0)
                    {
                        foreach (Entities_RFQ_RequestEntry emailLocal in List_EmailAndLocalNumber)
                        {
                            lblEmailAddress.Text = emailLocal.RhEmailAddress;
                            txtPurchasingRemarks.Text = emailLocal.RhBuyerNotes;
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------
                List<Entities_RFQ_RequestEntry> List_SendDates = new List<Entities_RFQ_RequestEntry>();
                List_SendDates = BLL.RFQ_TRANSACTION_SendDate_ByRFQNo(rfqno);

                if (List_SendDates != null)
                {
                    if (List_SendDates.Count > 0)
                    {
                        ddSendDates.Items.Clear();

                        foreach (Entities_RFQ_RequestEntry entity in List_SendDates)
                        {
                            ddSendDates.Items.Add(entity.SendDate);
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------

                List<Entities_RFQ_RequestEntry> List_TransferHistory = new List<Entities_RFQ_RequestEntry>();
                Entities_RFQ_RequestEntry entityTransferHistory = new Entities_RFQ_RequestEntry();
                entityTransferHistory.SearchCriteria = rfqno;
                List_TransferHistory = BLL.RFQ_TRANSACTION_HistoryOfUpdates(entityTransferHistory);

                if (List_TransferHistory != null)
                {
                    if (List_TransferHistory.Count > 0)
                    {
                        gvTransferDetails.DataSource = List_TransferHistory;
                        gvTransferDetails.DataBind();
                    }
                }

                //---------------------------------------------------------------------------------------------------

                List<Entities_RFQ_RequestEntry> List_HoldReason = new List<Entities_RFQ_RequestEntry>();
                List_HoldReason = BLL.RFQ_TRANSACTION_GetHoldReason_ByRFQNo(rfqno);

                if (List_HoldReason != null)
                {
                    if (List_HoldReason.Count > 0)
                    {
                        gvHoldReason.DataSource = List_HoldReason;
                        gvHoldReason.DataBind();
                    }
                }

                //---------------------------------------------------------------------------------------------------
                List<Entities_RFQ_RequestEntry> List_Approval = new List<Entities_RFQ_RequestEntry>();
                List<Entities_RFQ_RequestEntry> List_Disapproval = new List<Entities_RFQ_RequestEntry>();
                List<Entities_RFQ_RequestEntry> List_AD_Consolidated = new List<Entities_RFQ_RequestEntry>();

                List_Disapproval = BLL.RFQ_TRANSACTION_HistoryOfDisapproval_ByRFQNo(rfqno);
                List_Approval = BLL.RFQ_TRANSACTION_HistoryOfApproval_ByRFQNo(rfqno);

                if (List_Approval != null)
                {
                    if (List_Approval.Count > 0)
                    {
                        foreach (Entities_RFQ_RequestEntry entity in List_Approval)
                        {
                            List_AD_Consolidated.Add(entity);
                        }
                    }
                }
                if (List_Disapproval != null)
                {
                    if (List_Disapproval.Count > 0)
                    {
                        foreach (Entities_RFQ_RequestEntry entity in List_Disapproval)
                        {
                            List_AD_Consolidated.Add(entity);
                        }
                    }
                }

                if (List_AD_Consolidated != null)
                {
                    if (List_AD_Consolidated.Count > 0)
                    {
                        foreach (Entities_RFQ_RequestEntry entity in List_AD_Consolidated)
                        {
                            if (!string.IsNullOrEmpty(entity.Cause) || entity.Cause.Length > 0)
                            {
                                divCause.Style.Add("display", "block");
                                txtDisapprovalCause.Text = entity.Cause.ToUpper();
                            }

                            if (entity.TransactionName == ConfigurationManager.AppSettings["ApprovedDisapprovedName-ProdManager"])
                            {
                                lblProdManager.Text = entity.ApprovedBy;
                                lblDOAProdManager.Text = entity.ApprovedDate;
                                Session["Preview_ProdManager"] = entity.ApprovedBy;
                                Session["Preview_DOAProdManager"] = entity.ApprovedDate;
                                lblProdManager.Style.Add("background-color", statusColor(entity.StatProdManager));
                            }
                            if (entity.TransactionName == ConfigurationManager.AppSettings["ApprovedDisapprovedName-Purchasing"])
                            {
                                lblBuyerSend.Text = entity.ApprovedBy;
                                lblDOABuyerSend.Text = entity.ApprovedDate;
                                Session["Preview_BuyerSend"] = entity.ApprovedBy;
                                Session["Preview_DOABuyerSend"] = entity.ApprovedDate;
                                lblBuyerSend.Style.Add("background-color", statusColor(entity.StatPurchasing));
                                buyerSending = entity.StatPurchasing;
                            }
                            if (entity.TransactionName == ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingBuyer"])
                            {
                                if (buyerSending == "2" && string.IsNullOrEmpty(entity.StatBuyer))
                                {
                                }
                                else
                                {
                                    lblBuyer.Text = entity.ApprovedBy;
                                    lblDOABuyer.Text = entity.ApprovedDate;
                                    Session["Preview_Buyer"] = entity.ApprovedBy;
                                    Session["Preview_DOABuyer"] = entity.ApprovedDate;
                                    lblBuyer.Style.Add("background-color", statusColor(entity.StatBuyer));
                                }
                            }                            
                            if (entity.TransactionName == ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingIncharge"])
                            {
                                lblIncharge.Text = entity.ApprovedBy;
                                lblDOAIncharge.Text = entity.ApprovedDate;
                                Session["Preview_Incharge"] = entity.ApprovedBy;
                                Session["Preview_DOAIncharge"] = entity.ApprovedDate;
                                lblIncharge.Style.Add("background-color", statusColor(entity.StatPurchasingIncharge));
                            }
                            if (entity.TransactionName == ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingDeptManager"])
                            {
                                lblDeptManager.Text = entity.ApprovedBy;
                                lblDOADeptManager.Text = entity.ApprovedDate;
                                Session["Preview_DeptManager"] = entity.ApprovedBy;
                                Session["Preview_DOADeptManager"] = entity.ApprovedDate;
                                lblDeptManager.Style.Add("background-color", statusColor(entity.StatDeptManager));
                            }
                            if (entity.TransactionName == ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingDivManager"])
                            {
                                lblDivisionManager.Text = entity.ApprovedBy;
                                lblDOADivisionManager.Text = entity.ApprovedDate;
                                Session["Preview_DivManager"] = entity.ApprovedBy;
                                Session["Preview_DOADivManager"] = entity.ApprovedDate;
                                lblDivisionManager.Style.Add("background-color", statusColor(entity.StatDivManager));                                
                            }
                        }
                    }
                }

                List<Entities_RFQ_Category> List_Category = new List<Entities_RFQ_Category>();
                List_Category = BLL.RFQ_MT_Category_GetAll();

                if (List_Category != null)
                {
                    if (List_Category.Count > 0)
                    {
                        int isCategoryExist = 0;

                        ddCategory.Items.Clear();
                        foreach (Entities_RFQ_Category entity in List_Category)
                        {
                            if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                            {
                                if (entity.RefId == "1013" || entity.RefId == "1006")
                                {
                                    //DO NOT ADD THIS CATEGORY
                                }
                                else
                                {
                                    if (Session["Category_From_Inquiry"].ToString() == entity.Description.Trim().ToUpper())
                                    {
                                        isCategoryExist++;
                                    }
                                    ddCategory.Items.Add(entity.Description.ToUpper());
                                }
                            }
                        }

                        if (Session["Category_From_Inquiry"] != null)
                        {
                            if (isCategoryExist > 0)
                            {
                                ddCategory.Items.FindByText(Session["Category_From_Inquiry"].ToString().Trim().ToUpper()).Selected = true;
                            }
                            else
                            {
                                //DO NOTHING
                            }
                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------

                List<Entities_RFQ_RequestEntry> listDetails = new List<Entities_RFQ_RequestEntry>();
                listDetails = BLL.RFQ_TRANSACTION_Monitoring_GetDetailsByRFQNo(rfqno);

                if (listDetails != null)
                {
                    if (listDetails.Count > 0)
                    {
                        gvData.DataSource = listDetails;
                        gvData.DataBind();
                    }
                }


                //---------------------------------------------------------------------------------------------------

                List<Entities_RFQ_RequestEntry> listSupplierResponse = new List<Entities_RFQ_RequestEntry>();
                listSupplierResponse = BLL.RFQ_TRANSACTION_GetSupplierResponseByRFQNo(rfqno);

                if (listSupplierResponse != null)
                {
                    if (listSupplierResponse.Count > 0)
                    {
                        gvResponse.Visible = true;
                        gvResponse.DataSource = listSupplierResponse;
                        gvResponse.DataBind();
                    }
                }

                //---------------------------------------------------------------------------------------------------

                List<Entities_RFQ_RequestEntry> listAttachment = new List<Entities_RFQ_RequestEntry>();
                listAttachment = BLL.RFQ_TRANSACTION_GetAttachment_ByRFQNo(rfqno);

                if (listAttachment != null)
                {
                    if (listAttachment.Count > 0)
                    {
                        divAttachment.Style.Add("display", "block");
                        int attachCounter = 1;
                        string cssTabPane = string.Empty;

                        string temp_cssTabPane = string.Empty;
                        string temp_tabAttachment = string.Empty;
                        string temp_tabAttachmentContents = string.Empty;

                        foreach (Entities_RFQ_RequestEntry attachment in listAttachment)
                        {
                            temp_cssTabPane = attachCounter.ToString() == "1" ? "tab-pane fade in active" : "tab-pane fade";
                            temp_tabAttachment += "<li role='presentation'><a href='#" + attachment.RdAttachment.Replace(".pdf", "").Replace(".PDF", "") + "'data-toggle='tab'>" + attachCounter.ToString() + "</a></li> ";
                            temp_tabAttachmentContents += "<div role='tabpanel' class='" + temp_cssTabPane + "' id='" + attachment.RdAttachment.Replace(".pdf", "").Replace(".PDF", "") + "'> <b>" + attachment.RdDescription + " - " + attachment.RdSpecs + "</b>" +
                                                     "<p><iframe runat='server' id='iframepdf" + attachCounter.ToString() + "' height='800' width='980' frameborder='0' src='/IO_Request/" + rfqno + "/" + attachment.RdAttachment + "'> </iframe></p>" + "<b>" + attachment.RdDescription + " - " + attachment.RdSpecs + "</b>" + "</div>";                            
                            attachCounter++;
                        }

                        Session["tabAttachment"] = temp_tabAttachment;
                        Session["tabAttachmentContents"] = temp_tabAttachmentContents;

                    }

                }

                if (Session["tabAttachment"] != null)
                {
                    tabAttachment = Session["tabAttachment"].ToString();
                    tabAttachmentContents = Session["tabAttachmentContents"].ToString();
                }


                //---------------------------------------------------------------------------------------------------                

                btnPreview.Visible = bool.Parse(Session["btnPreview_Visibility"].ToString());
                btnUpdate.Visible = bool.Parse(Session["btnUpdate_Visibility"].ToString());


                if (Session["btnPreview_Visibility"].ToString() == "true" || Session["btnUpdate_Visibility"].ToString() == "true")
                {
                    divActionButtons.Style.Add("display", "block");
                    //ddCategory.Enabled = true;
                    ddCategory.Enabled = false;
                }
                else
                {
                    divActionButtons.Style.Add("display", "none");
                    ddCategory.Enabled = false;
                }


                //---------------------------------------------------------------------------------------------------

                List<Entities_RFQ_RequestEntry> listRespondedSupplier = new List<Entities_RFQ_RequestEntry>();
                listRespondedSupplier = BLL.RFQ_TRANSACTION_GetRespondedSupplierByRFQNo(rfqno);
                int responded = 0;

                if (Session["divSupplier_Visibility"] != null)
                {
                    if (Session["divSupplier_Visibility"].ToString() == "true")
                    {                        

                        if (listRespondedSupplier != null)
                        {
                            if (listRespondedSupplier.Count > 0)
                            {
                                divSupplierResponse.Style.Add("display", "block");

                                gvSuppliers.DataSource = listRespondedSupplier;
                                gvSuppliers.DataBind();                                                              
                            }
                        }                        
                    }
                }

                //---------------------------------------------------------------------------------------------------

                if (listRespondedSupplier != null)
                {
                    if (listRespondedSupplier.Count > 0)
                    {
                        foreach (Entities_RFQ_RequestEntry entity in listRespondedSupplier)
                        {
                            if (!string.IsNullOrEmpty(entity.ResponseCount))
                            {
                                if (int.Parse(entity.ResponseCount) > 0)
                                {
                                    responded++;
                                }
                            }
                        }  

                        lblSuppliers.Text = "Responded " + responded.ToString() + " supplier(s) out of " + listRespondedSupplier.Count.ToString();
                    }
                }

                //---------------------------------------------------------------------------------------------------

                //---------------------------------------------------------------------------------------------------

                List<Entities_RFQ_RequestEntry> listIsAlreadyApproved = new List<Entities_RFQ_RequestEntry>();
                listIsAlreadyApproved = BLL.RFQ_TRANSACTION_IsAlready_Approved_ByRFQNo(rfqno);

                if (listIsAlreadyApproved != null)
                {
                    if (listIsAlreadyApproved.Count > 0 && BLL_COMMON.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString()).Count <= 0)
                    {                        

                        Entities_RFQ_RequestEntry entityAttachment = new Entities_RFQ_RequestEntry();
                        entityAttachment.ResponseRFQNo = rfqno.Trim();

                        List<Entities_RFQ_RequestEntry> listApprovedSupplierAttachment = new List<Entities_RFQ_RequestEntry>();
                        listApprovedSupplierAttachment = BLL.RFQ_TRANSACTION_GetApprovedSupplierAttachmentBySupplierIdAndRFQNo(entityAttachment);

                        if (listApprovedSupplierAttachment != null)
                        {

                            if (listApprovedSupplierAttachment.Count > 0)
                            {
                                divSupplierAttachment.Style.Add("display", "block");

                                int attachCounter = 1;
                                string cssTabPane = string.Empty;

                                string temp_cssTabPane = string.Empty;
                                string temp_tabAttachment = string.Empty;
                                string temp_tabAttachmentContents = string.Empty;

                                foreach (Entities_RFQ_RequestEntry list in listApprovedSupplierAttachment)
                                {
                                    temp_cssTabPane = attachCounter.ToString() == "1" ? "tab-pane fade in active" : "tab-pane fade";
                                    temp_tabAttachment += "<li role='presentation'><a href='#" + list.ResponseSupplierAttachment.Replace(".pdf", "").Replace(".PDF", "") + "'data-toggle='tab'>" + list.ToString() + "</a></li> ";
                                    temp_tabAttachmentContents += "<div role='tabpanel' class='" + temp_cssTabPane + "' id='" + list.ResponseSupplierAttachment.Replace(".pdf", "").Replace(".PDF", "") + "'> <b>" + list.ResponseSupplierAttachment + "</b>" +
                                                             "<p><iframe runat='server' id='iframepdf" + attachCounter.ToString() + "' height='800' width='980' frameborder='0' src='/IO_Received/" + (list.ResponseSupplierID.Trim() + "_" + lblRFQNo.Text.Trim()) + "/" + list.ResponseSupplierAttachment.Replace("#", "%23").ToString() + "'> </iframe></p>" + "</div>";
                                    attachCounter++;
                                }

                                Session["tabAttachmentSupplier"] = temp_tabAttachment;
                                Session["tabAttachmentContentsSupplier"] = temp_tabAttachmentContents;

                                if (Session["tabAttachmentSupplier"] != null)
                                {
                                    tabAttachmentSupplier = Session["tabAttachmentSupplier"].ToString();
                                    tabAttachmentContentsSupplier = Session["tabAttachmentContentsSupplier"].ToString();
                                }

                            }

                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------

                if (string.IsNullOrEmpty(lblProdManager.Text))
                {
                    imgWho.Visible = true;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (isItemManualApproved(lblRFQNo.Text.Trim().ToUpper()))
                {
                    string URL2 = "http://10.27.1.170:9292/IO_Request/" + lblRFQNo.Text.Trim() + "/ManualApproved-" + lblRFQNo.Text.Trim() + ".pdf";

                    URL2 = Page.ResolveClientUrl(URL2);
                    btnPreview.OnClientClick = "window.open('" + URL2 + "'); return false;";
                }
                else
                {
                    Response.Redirect("RFQ_RequestPreview.aspx", false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Session["CategoryFromDetails"] = ddCategory.SelectedItem.Text;
                Response.Redirect("RFQ_RequestEntry.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(lblRFQNo.Text.Trim(), true), false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }
        

        private string statusColor(string stat)
        {
            string retVal = string.Empty;

            if (stat == "0" || string.IsNullOrEmpty(stat) || stat.Length <= 0)
            {
                retVal = "#f44336";
            }
            if (stat == "1")
            {
                retVal = "#00C851";
            }
            if (stat == "2")
            {
                retVal = "#ffbb33";
            }

            return retVal;
        }

        protected void gvSuppliers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                Label lblSupplierID = row.FindControl("lblSupplierID") as Label;
                Label lblSupplier = row.FindControl("lblSupplier") as Label;

                if (e.CommandName == "lblView_Command")
                {
                    List<Entities_RFQ_RequestEntry> listSupplierResponse = new List<Entities_RFQ_RequestEntry>();
                    listSupplierResponse = BLL.RFQ_TRANSACTION_GetSupplierResponseByRFQNoAndSupplierID(lblRFQNo.Text.Trim(), lblSupplierID.Text.Trim());

                    if (listSupplierResponse != null)
                    {
                        if (listSupplierResponse.Count > 0)
                        {
                            gvSupplierResponse.Visible = true;
                            gvSupplierResponse.DataSource = listSupplierResponse;
                            gvSupplierResponse.DataBind();

                            supplierResponseDetails = lblSupplier.Text;

                            List<Entities_RFQ_RequestEntry> listAttachment = new List<Entities_RFQ_RequestEntry>();
                            Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                            entity.ResponseSupplierID = lblSupplierID.Text.Trim();
                            entity.ResponseRFQNo = lblRFQNo.Text.Trim();

                            listAttachment = BLL.RFQ_TRANSACTION_GetSupplierAttachmentBySupplierIdAndRFQNo(entity);

                            if (listAttachment != null)
                            {
                                if (listAttachment.Count > 0)
                                {
                                    divSupplierAttachment.Style.Add("display", "block");
                                    int attachCounter = 1;
                                    string cssTabPane = string.Empty;

                                    string temp_cssTabPane = string.Empty;
                                    string temp_tabAttachment = string.Empty;
                                    string temp_tabAttachmentContents = string.Empty;

                                    foreach (Entities_RFQ_RequestEntry attachment in listAttachment)
                                    {                                        

                                        temp_cssTabPane = attachCounter.ToString() == "1" ? "tab-pane fade in active" : "tab-pane fade";
                                        temp_tabAttachment += "<li role='presentation'><a href='#" + attachment.ResponseSupplierAttachment.Replace(".pdf", "").Replace(".PDF", "") + "'data-toggle='tab'>" + attachCounter.ToString() + "</a></li> ";
                                        temp_tabAttachmentContents += "<div role='tabpanel' class='" + temp_cssTabPane + "' id='" + attachment.ResponseSupplierAttachment.Replace(".pdf", "").Replace(".PDF", "") + "'> <b>" + attachment.ResponseSupplierAttachment + "</b>" +
                                                                 "<p><iframe runat='server' id='iframepdf" + attachCounter.ToString() + "' height='800' width='980' frameborder='0' src='/IO_Received/" + (lblSupplierID.Text.Trim() + "_" + lblRFQNo.Text.Trim()) + "/" + attachment.ResponseSupplierAttachment.Replace("#","%23").ToString() + "'> </iframe></p>" + "</div>";
                                        attachCounter++;                                        

                                    }

                                    Session["tabAttachmentSupplier"] = temp_tabAttachment;
                                    Session["tabAttachmentContentsSupplier"] = temp_tabAttachmentContents;

                                    if (Session["tabAttachmentSupplier"] != null)
                                    {
                                        tabAttachmentSupplier = Session["tabAttachmentSupplier"].ToString();
                                        tabAttachmentContentsSupplier = Session["tabAttachmentContentsSupplier"].ToString();
                                    }

                                }
                            }


                        }
                        else
                        {
                            gvSupplierResponse.Visible = false;
                        }
                    }

                }

                if (e.CommandName == "lblManualResponse_Command")
                {
                    Response.Redirect("RFQ_ManualResponse.aspx?RFQNo_From_ManualResponse=" + CryptorEngine.Encrypt(lblRFQNo.Text.Trim(), true) + "&RFQNo_From_ManualResponse_Supplier=" + lblSupplier.Text + "&RFQNo_From_ManualResponse_SupplierID=" + lblSupplierID.Text.Trim());
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


        protected void gvSuppliers_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label lblResponseCount = (Label)e.Row.FindControl("lblResponseCount");
                    Label lblResponseDate = (Label)e.Row.FindControl("lblResponseDate");

                    if (!string.IsNullOrEmpty(lblResponseCount.Text))
                    {
                        if (int.Parse(lblResponseCount.Text) <= 0)
                        {
                            lblResponseDate.Visible = false;
                        }
                    }

                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvResponse_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblResponseDescription = (Label)e.Row.FindControl("lblResponseDescription");
                    Label lblResponseSupplier = (Label)e.Row.FindControl("lblResponseSupplier");
                    Label lblDetailsRefId = (Label)e.Row.FindControl("lblDetailsRefId");
                    Label lblIsGranted = (Label)e.Row.FindControl("lblIsGranted");
                    ImageButton ibApprovedResponse = (ImageButton)e.Row.FindControl("ibApprovedResponse");
                    DropDownList ddResponseCurrency = (DropDownList)e.Row.FindControl("ddResponseCurrency");
                    Label lblResponseCurrency = (Label)e.Row.FindControl("lblResponseCurrency");
                    TextBox txtResponseSupplierRemarks = (TextBox)e.Row.FindControl("txtResponseSupplierRemarks");
                    TextBox txtResponsePurchasingRemarks = (TextBox)e.Row.FindControl("txtResponsePurchasingRemarks");

                    List<Entities_RFQ_Currency> listCurrency = new List<Entities_RFQ_Currency>();
                    listCurrency = BLL.RFQ_MT_Currency_GetAll();

                    if (listCurrency != null)
                    {
                        if (listCurrency.Count > 0)
                        {
                            ddResponseCurrency.Items.Add("");
                            foreach (Entities_RFQ_Currency entity in listCurrency)
                            {
                                ListItem item = new ListItem();
                                item.Text = entity.Code;
                                item.Value = entity.Code;

                                if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                                {
                                    ddResponseCurrency.Items.Add(item);
                                }
                            }

                            if (!string.IsNullOrEmpty(lblResponseCurrency.Text.Trim()))
                            {
                                ddResponseCurrency.Items.FindByText(lblResponseCurrency.Text.Trim()).Selected = true;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(lblResponseDescription.Text))
                    {
                        if (lblResponseDescription.Text.Length > 36)
                        {
                            int supRemLen = lblResponseDescription.Text.Length;
                            int divisor = 36;
                            int height = ((supRemLen / divisor) * 20) + 10;

                            lblResponseDescription.Style.Add("Height", height.ToString() + "px");
                        }
                    }
                    if (!string.IsNullOrEmpty(lblResponseSupplier.Text))
                    {
                        lblResponseSupplier.Text = lblResponseSupplier.Text.Length > 17 ? lblResponseSupplier.Text.Substring(0, 17).ToString() + "..." : lblResponseSupplier.Text;
                    }

                    foreach (TableCell cell in e.Row.Cells)
                    {
                        if (IsOdd(long.Parse(lblDetailsRefId.Text.Trim())))
                        {
                            cell.Style.Add("background-color", "#98FB98");
                        }
                        else
                        {
                            cell.Style.Add("background-color", "#FFA07A");
                        }
                    }

                    if (lblIsGranted.Text.Trim() == "1")
                    {
                        ibApprovedResponse.ImageUrl = "~/images/A2.png";
                    }

                    if (!string.IsNullOrEmpty(txtResponseSupplierRemarks.Text))
                    {
                        if (txtResponseSupplierRemarks.Text.Length > 30)
                        {
                            int supRemLen = txtResponseSupplierRemarks.Text.Length;
                            int divisor = 30;
                            int height = ((supRemLen / divisor) * 20) + 30;

                            txtResponseSupplierRemarks.Style.Add("Height", height.ToString() + "px");
                        }
                    }

                    if (!string.IsNullOrEmpty(txtResponsePurchasingRemarks.Text))
                    {
                        if (txtResponsePurchasingRemarks.Text.Length > 30)
                        {
                            int supRemLen2 = txtResponsePurchasingRemarks.Text.Length;
                            int divisor2 = 30;
                            int height2 = ((supRemLen2 / divisor2) * 20) + 30;

                            txtResponsePurchasingRemarks.Style.Add("Height", height2.ToString() + "px");
                        }
                    }

                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        private static bool IsOdd(long value)
        {
            return value % 2 != 0;
        }


        private static bool isItemManualApproved(string rfqNo)
        {
            bool isExist = false;

            DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection conn = fact.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_SOFRA"].ConnectionString;

            DbDataReader reader = null;
            DbCommand cmd = fact.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TOP 1 * FROM Request_HistoryOfApproval WITH (NOLOCK) WHERE TransactionName = 'PurchasingDivManager' AND ApprovedBy = '" + ConfigurationManager.AppSettings["AUTO-APPROVED-ACCOUNT"].ToString() + "' AND RFQNo = '" + rfqNo + "'";

            conn.Open();
            cmd.Connection = conn;

            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                isExist = true;
            }

            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;

            return isExist;
        }




    }
}
