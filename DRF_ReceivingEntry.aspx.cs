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
using System.IO;

namespace REPI_PUR_SOFRA
{
    public partial class DRF_ReceivingEntry : System.Web.UI.Page
    {

        BLL_DRF BLL = new BLL_DRF();
        BLL_RFQ BLL_RFQ = new BLL_RFQ();
        Common COMMON = new Common();
        public string displayAttachment = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    txtFrom.Text = DateTime.Today.AddDays(-1825).ToString("MM/dd/yyyy");
                    txtTo.Text = DateTime.Today.ToString("MM/dd/yyyy");

                    //---------------------------------------------------------------------------------------------------
                    List<Entities_DRF_RequestEntry> listDropDown = new List<Entities_DRF_RequestEntry>();
                    listDropDown = BLL.DRF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                    if (listDropDown != null)
                    {
                        if (listDropDown.Count > 0)
                        {
                            ddCategory.Items.Clear();
                            ddCategory.Items.Add("");

                            foreach (Entities_DRF_RequestEntry entity in listDropDown)
                            {
                                ListItem item = new ListItem();
                                item.Text = entity.DropdownName.ToUpper();
                                item.Value = entity.DropdownRefId;

                                if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                                {
                                    if (entity.TableName == "MT_Category")
                                    {
                                        ddCategory.Items.Add(item);
                                    }
                                }

                            }

                            //if (!string.IsNullOrEmpty(Session["CategoryAccess"].ToString()))
                            //{
                            //    if (int.Parse(Session["CategoryAccess"].ToString()) > 0)
                            //    {
                            //        ddCategory.Items.FindByValue(Session["CategoryAccess"].ToString()).Selected = true;
                            //        ddCategory.Enabled = false;
                            //    }
                            //}

                            //---------------------------------------------------------------------------------------------------


                            if (Session["CategoryAccess"] != null)
                            {
                                if (int.Parse(Session["CategoryAccess"].ToString()) > 0)
                                {

                                    if (Session["SENDING_OTHER_BUYERS"] != null)
                                    {
                                        if (!string.IsNullOrEmpty(Session["SENDING_OTHER_BUYERS"].ToString()))
                                        {
                                            ddCategory.Enabled = true;
                                            ddCategory.Items.FindByValue(Session["SENDING_OTHER_BUYERS"].ToString()).Selected = true;
                                        }
                                    }
                                    else
                                    {
                                        ddCategory.Enabled = false;
                                        ddCategory.Items.FindByValue(Session["CategoryAccess"].ToString()).Selected = true;
                                    }
                                }
                                else
                                {
                                    ddCategory.Enabled = true;
                                }
                            }

                            //---------------------------------------------------------------------------------------------------



                        }
                    }
                    //---------------------------------------------------------------------------------------------------

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


                List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();
                Entities_DRF_RequestEntry request = new Entities_DRF_RequestEntry();

                request.DrFrom = txtFrom.Text;
                request.DrTo = txtTo.Text;

                if (!string.IsNullOrEmpty(txtDRFNo.Text) || txtDRFNo.Text.Length > 0)
                {
                    Session["Search_From_DRF_Inquiry"] = txtDRFNo.Text;
                    Response.Redirect("DRF_AllRequest_New.aspx");
                }
                else
                {
                    if (ddCategory.SelectedItem.Text.ToLower() == "")
                    {
                        if (Session["Search_From_DRF_AllRequest"] != null)
                        {
                            if (!string.IsNullOrEmpty(Session["Search_From_DRF_AllRequest"].ToString()))
                            {
                                list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "1" && itm.PurManagerStat == "1" && itm.Posted == "0" && itm.CtrlNo == Session["Search_From_DRF_AllRequest"].ToString().Trim()).ToList();
                            }
                        }
                        else
                        {
                            if (ddStatus.SelectedValue == "ALL")
                            {
                                list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "1" && itm.PurManagerStat == "1" && itm.Posted == "0").ToList(); ;
                            }
                            else
                            {
                                list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "1" && itm.PurManagerStat == "1" && itm.Posted == "0" && itm.StatAll == ddStatus.SelectedValue).ToList(); ;
                            }
                        }
                    }
                    else
                    {
                        if (Session["Search_From_DRF_AllRequest"] != null)
                        {
                            if (!string.IsNullOrEmpty(Session["Search_From_DRF_AllRequest"].ToString()))
                            {
                                list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "1" && itm.PurManagerStat == "1" && itm.Posted == "0" && itm.CtrlNo == Session["Search_From_DRF_AllRequest"].ToString().Trim() && itm.CategoryId == ddCategory.SelectedValue.Trim()).ToList();
                            }
                        }
                        else
                        {
                            if (ddStatus.SelectedValue == "ALL")
                            {
                                list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "1" && itm.PurManagerStat == "1" && itm.Posted == "0" && itm.CategoryId == ddCategory.SelectedValue.Trim()).ToList();
                            }
                            else
                            {
                                list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "1" && itm.PurManagerStat == "1" && itm.Posted == "0" && itm.CategoryId == ddCategory.SelectedValue.Trim() && itm.StatAll == ddStatus.SelectedValue).ToList();
                            }
                        }
                    }
                }


                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        gvData.Visible = true;
                        gvData.DataSource = list;
                        gvData.DataBind();

                        //EXPORT TO EXCEL
                        List<Entities_DRF_RequestEntry> finalListExport = new List<Entities_DRF_RequestEntry>();

                        foreach (Entities_DRF_RequestEntry entity in list)
                        {
                            List<Entities_DRF_RequestEntry> listExport = new List<Entities_DRF_RequestEntry>();
                            listExport = BLL.DRF_TRANSACTION_AllRequest_ByCTRLNo(entity);

                            if (listExport != null)
                            {
                                if (listExport.Count > 0)
                                {
                                    foreach (Entities_DRF_RequestEntry le in listExport)
                                    {
                                        Entities_DRF_RequestEntry final = new Entities_DRF_RequestEntry();
                                        final.CtrlNo = le.CtrlNo;
                                        final.Attention = le.Attention;
                                        final.Supplier = le.Supplier;
                                        final.Requester = le.Requester;
                                        final.TransactionDate = le.TransactionDate;
                                        final.InvoiceDRNO = le.InvoiceDRNO;
                                        final.PrNO = le.PrNO;
                                        final.PoNO = le.PoNO;
                                        final.PoDate = le.PoDate;
                                        final.ReceivedDate = le.ReceivedDate;
                                        final.Category = le.Category;
                                        final.Description = le.Description;
                                        final.TypeDrawingNo = le.TypeDrawingNo;
                                        final.OrderQuantity = le.OrderQuantity;
                                        final.AbnormalQuantity = le.AbnormalQuantity;
                                        final.TypesOfAbnormality = le.TypesOfAbnormality;
                                        final.DetailedReport = le.DetailedReport;
                                        final.StatAll = le.StatAll;

                                        finalListExport.Add(final);
                                    }
                                }
                            }
                        }

                        gvExport.DataSource = finalListExport;
                        gvExport.DataBind();
                    }
                    else
                    {
                        gvData.Visible = false;
                    }
                }

                divOpacity.Style.Add("opacity", "1");
                divLoader.Style.Add("display", "none");

                Session["Search_From_DRF_AllRequest"] = null;
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
                LinkButton lbCTRLNo = row.FindControl("lbCTRLNo") as LinkButton;
                LinkButton lbPreview = row.FindControl("lbPreview") as LinkButton;
                Label lblCTRLNo = row.FindControl("lblCTRLNo") as Label;
                ImageButton ibApproved = row.FindControl("ibApproved") as ImageButton;
                ImageButton ibClosed = row.FindControl("ibClosed") as ImageButton;
                TextBox txtRemarks = row.FindControl("txtRemarks") as TextBox;
                HtmlControl divDetails = row.FindControl("divDetails") as HtmlControl;
                Label lblAttachment1 = row.FindControl("lblAttachment1") as Label;
                Label lblAttachment2 = row.FindControl("lblAttachment2") as Label;
                Label lblAttachment3 = row.FindControl("lblAttachment3") as Label;
                Label lblAttachment4 = row.FindControl("lblAttachment4") as Label;
                HtmlControl divAttachment = row.FindControl("divAttachment") as HtmlControl;

                HtmlControl tableSupplierResponseType = row.FindControl("tableSupplierResponseType") as HtmlControl;
                HtmlControl tableSupplierResponseAnswer = row.FindControl("tableSupplierResponseAnswer") as HtmlControl;
                Label lblStatAll = row.FindControl("lblStatAll") as Label;
                TextBox txtResponseType = row.FindControl("txtResponseType") as TextBox;
                TextBox txtResponseAnswer = row.FindControl("txtResponseAnswer") as TextBox;

                TextBox txtPRNo = row.FindControl("txtPRNo") as TextBox;
                TextBox txtPONo = row.FindControl("txtPONo") as TextBox;
                TextBox txtPODate = row.FindControl("txtPODate") as TextBox;

                Label lblSupplier = row.FindControl("lblSupplier") as Label;
                Label lblAttention = row.FindControl("lblAttention") as Label;

                TextBox txtDescription = row.FindControl("txtDescription") as TextBox;
                TextBox txtInvoiceDRNo = row.FindControl("txtInvoiceDRNo") as TextBox;
                TextBox txtReceivedDate = row.FindControl("txtReceivedDate") as TextBox;
                TextBox txtTypeDrawing = row.FindControl("txtTypeDrawing") as TextBox;
                TextBox txtOrderQuantity = row.FindControl("txtOrderQuantity") as TextBox;
                TextBox txtAbnormalQuantity = row.FindControl("txtAbnormalQuantity") as TextBox;
                TextBox txtTypesOfAbnormality = row.FindControl("txtTypesOfAbnormality") as TextBox;
                TextBox txtDetailedReport = row.FindControl("txtDetailedReport") as TextBox;

                Label lblReqManagerStatColor = row.FindControl("lblReqManagerStatColor") as Label;
                Label lblPurInchargeStatColor = row.FindControl("lblPurInchargeStatColor") as Label;
                Label lblPurManagerStatColor = row.FindControl("lblPurManagerStatColor") as Label;

                Label lblRequester = row.FindControl("lblRequester") as Label;
                Label lblReqManager = row.FindControl("lblReqManager") as Label;
                Label lblPurIncharge = row.FindControl("lblPurIncharge") as Label;
                Label lblPurManager = row.FindControl("lblPurManager") as Label;

                Label lblReqManagerDOAStat = row.FindControl("lblReqManagerDOAStat") as Label;
                Label lblPurInchargeDOAStat = row.FindControl("lblPurInchargeDOAStat") as Label;
                Label lblPurManagerDOAStat = row.FindControl("lblPurManagerDOAStat") as Label;
                Label lblRequesterDOAStat = row.FindControl("lblRequesterDOAStat") as Label;

                TextBox txtBuyerRemarksNew = row.FindControl("txtBuyerRemarksNew") as TextBox;

                string attachmentLiteralInside = string.Empty;
                string pdfSource = string.Empty;



                if (e.CommandName == "UpdateBuyerRemarks_Command")
                {
                    string queryResult = BLL.DRF_TRANSACTION_SQLTransaction("UPDATE DRF_TRANSACTION_Request SET BuyerRemarks = '" + txtBuyerRemarksNew.Text.Replace("'", "''") + "' WHERE CTRLNO = '" + lblCTRLNo.Text.Trim() + "'").ToString();
                    if (queryResult == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Buyer Remarks for " + lblCTRLNo.Text + " has been successfully updated.');", true);
                    }
                }

                if (e.CommandName == "lbCTRLNo_Command")
                {
                    //Response.Redirect("DRF_RequestEntry.aspx?DRFNo_From_Inquiry=" + CryptorEngine.Encrypt(lblCTRLNo.Text.Trim(), true), false);
                    string URL = "~/DRF_RequestEntry.aspx?DRFNo_From_Inquiry=" + CryptorEngine.Encrypt(lblCTRLNo.Text.Trim(), true);
                    URL = Page.ResolveClientUrl(URL);
                    lbCTRLNo.OnClientClick = "window.open('" + URL + "'); return false;";
                }

                if (e.CommandName == "lbManualResponse_Command")
                {
                    Response.Redirect("DRF_ManualResponse.aspx?DRFNo_From_ManualResponse=" + CryptorEngine.Encrypt(lblCTRLNo.Text.Trim(), true), false);
                }

                if (e.CommandName == "lblView_Command")
                {
                    if (lblView.Text.ToUpper() == "OPEN DETAILS")
                    {
                        divDetails.Style.Add("display", "block");
                        lblView.Text = "CLOSE DETAILS";

                        if (!string.IsNullOrEmpty(lblAttachment1.Text) || !string.IsNullOrEmpty(lblAttachment2.Text) || !string.IsNullOrEmpty(lblAttachment3.Text) || !string.IsNullOrEmpty(lblAttachment4.Text))
                        {
                            pdfSource = ConfigurationManager.AppSettings["DRF_DL_REQUEST_ATTACHMENT_URL"] + lblCTRLNo.Text + "/";

                            if (!string.IsNullOrEmpty(lblAttachment1.Text))
                            {
                                attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment1.Text + "' height='500px' width='1177px'></iframe></td></tr></table>";
                            }
                            if (!string.IsNullOrEmpty(lblAttachment2.Text))
                            {
                                attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment2.Text + "' height='500px' width='1177px'></iframe></td></tr></table>";
                            }
                            if (!string.IsNullOrEmpty(lblAttachment3.Text))
                            {
                                attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment3.Text + "' height='500px' width='1177px'></iframe></td></tr></table>";
                            }
                            if (!string.IsNullOrEmpty(lblAttachment4.Text))
                            {
                                attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment4.Text + "' height='500px' width='1177px'></iframe></td></tr></table>";
                            }

                            divAttachment.Style.Add("display", "block");
                            displayAttachment = attachmentLiteralInside;
                        }

                        if (lblStatAll.Text == "SUPPLIER RESPONDED")
                        {
                            List<Entities_DRF_RequestEntry> listRequest = new List<Entities_DRF_RequestEntry>();
                            Entities_DRF_RequestEntry entityRequest = new Entities_DRF_RequestEntry();
                            entityRequest.CtrlNo = lblCTRLNo.Text;

                            listRequest = BLL.DRF_TRANSACTION_GetRequestByCTRLNo(entityRequest);

                            if (listRequest != null)
                            {
                                if (listRequest.Count > 0)
                                {
                                    foreach (Entities_DRF_RequestEntry eRequest in listRequest)
                                    {
                                        if (eRequest.SupplierResponded == "1")
                                        {
                                            txtResponseType.Text = eRequest.ResponseType;
                                            txtResponseAnswer.Text = eRequest.ResponseAnswer;
                                            tableSupplierResponseType.Style.Add("display", "block");
                                            tableSupplierResponseAnswer.Style.Add("display", "block"); 
                                        }
                                    }
                                }
                            }
                                                                                   
                        }

                    }
                    else
                    {
                        divDetails.Style.Add("display", "none");
                        lblView.Text = "OPEN DETAILS";

                        divAttachment.Style.Add("display", "none");
                        displayAttachment = string.Empty;
                    }
                }

                if (e.CommandName == "lbPreview_Command")
                {
                    ViewReport(lblCTRLNo, txtResponseType, txtResponseAnswer, txtPRNo, txtPONo, txtPODate, lblSupplier, lblAttention, txtDescription, txtInvoiceDRNo, txtReceivedDate, txtTypeDrawing, txtOrderQuantity, txtAbnormalQuantity, txtTypesOfAbnormality, txtDetailedReport, lblReqManagerStatColor, lblPurInchargeStatColor, lblPurManagerStatColor, lblRequester, lblReqManager, lblPurIncharge, lblPurManager, lblReqManagerDOAStat, lblPurInchargeDOAStat, lblPurManagerDOAStat, lblRequesterDOAStat);

                    //Response.Redirect("DRF_Request/" + lblCTRLNo.Text.Trim() + "/REPORT_" + lblCTRLNo.Text.Trim() + ".html", false);
                    string URL = "~/DRF_Request/" + lblCTRLNo.Text.Trim() + "/REPORT_" + lblCTRLNo.Text.Trim() + ".html";
                    URL = Page.ResolveClientUrl(URL);
                    lbPreview.OnClientClick = "window.open('" + URL + "'); return false;";

                }

                if (e.CommandName == "A_Command")
                {
                    if (ibClosed.ImageUrl == "~/images/Close4.png")
                    {
                    }
                    else
                    {
                        if (ibApproved.ImageUrl == "~/images/A1.png")
                        {
                            divDetails.Style.Add("display", "block");
                            ibApproved.ImageUrl = "~/images/A2.png";

                            if (!string.IsNullOrEmpty(lblAttachment1.Text) || !string.IsNullOrEmpty(lblAttachment2.Text) || !string.IsNullOrEmpty(lblAttachment3.Text) || !string.IsNullOrEmpty(lblAttachment4.Text))
                            {
                                pdfSource = ConfigurationManager.AppSettings["DRF_DL_REQUEST_ATTACHMENT_URL"] + lblCTRLNo.Text + "/";

                                if (!string.IsNullOrEmpty(lblAttachment1.Text))
                                {
                                    attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment1.Text + "' height='500px' width='1177px'></iframe></td></tr></table>";
                                }
                                if (!string.IsNullOrEmpty(lblAttachment2.Text))
                                {
                                    attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment2.Text + "' height='500px' width='1177px'></iframe></td></tr></table>";
                                }
                                if (!string.IsNullOrEmpty(lblAttachment3.Text))
                                {
                                    attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment3.Text + "' height='500px' width='1177px'></iframe></td></tr></table>";
                                }
                                if (!string.IsNullOrEmpty(lblAttachment4.Text))
                                {
                                    attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment4.Text + "' height='500px' width='1177px'></iframe></td></tr></table>";
                                }

                                divAttachment.Style.Add("display", "block");
                                displayAttachment = attachmentLiteralInside;
                            }

                            if (lblStatAll.Text == "SUPPLIER RESPONDED")
                            {
                                List<Entities_DRF_RequestEntry> listRequest = new List<Entities_DRF_RequestEntry>();
                                Entities_DRF_RequestEntry entityRequest = new Entities_DRF_RequestEntry();
                                entityRequest.CtrlNo = lblCTRLNo.Text;

                                listRequest = BLL.DRF_TRANSACTION_GetRequestByCTRLNo(entityRequest);

                                if (listRequest != null)
                                {
                                    if (listRequest.Count > 0)
                                    {
                                        foreach (Entities_DRF_RequestEntry eRequest in listRequest)
                                        {
                                            if (eRequest.SupplierResponded == "1")
                                            {
                                                txtResponseType.Text = eRequest.ResponseType;
                                                txtResponseAnswer.Text = eRequest.ResponseAnswer;
                                                tableSupplierResponseType.Style.Add("display", "block");
                                                tableSupplierResponseAnswer.Style.Add("display", "block");
                                            }
                                        }
                                    }
                                }

                            }


                            //CALL VIEWREPORT TO GENERATE REPORT TO ATTACHED FOR SUPPLIER COPY
                            ViewReport(lblCTRLNo, txtResponseType, txtResponseAnswer, txtPRNo, txtPONo, txtPODate, lblSupplier, lblAttention, txtDescription, txtInvoiceDRNo, txtReceivedDate, txtTypeDrawing, txtOrderQuantity, txtAbnormalQuantity, txtTypesOfAbnormality, txtDetailedReport, lblReqManagerStatColor, lblPurInchargeStatColor, lblPurManagerStatColor, lblRequester, lblReqManager, lblPurIncharge, lblPurManager, lblReqManagerDOAStat, lblPurInchargeDOAStat, lblPurManagerDOAStat, lblRequesterDOAStat);


                        }
                        else
                        {
                            ibApproved.ImageUrl = "~/images/A1.png";

                            divDetails.Style.Add("display", "none");
                            divAttachment.Style.Add("display", "none");
                            displayAttachment = string.Empty;
                        }
                    }
                }

                if (e.CommandName == "Closed_Command")
                {
                    if (ibApproved.ImageUrl == "~/images/A2.png")
                    {
                    }
                    else
                    {
                        if (ibClosed.ImageUrl == "~/images/Close3.png")
                        {

                            List<Entities_DRF_RequestEntry> listProofAttachment1 = new List<Entities_DRF_RequestEntry>();
                            Entities_DRF_RequestEntry entityProofAttachment = new Entities_DRF_RequestEntry();
                            entityProofAttachment.CtrlNo = lblCTRLNo.Text.Trim();

                            listProofAttachment1 = BLL.DRF_TRANSACTION_ProofAttachmentList(entityProofAttachment);

                            if (listProofAttachment1.Count > 0)
                            {

                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessageClose('" + lblCTRLNo.Text.Trim() + "');", true);
                                ibClosed.ImageUrl = "~/images/Close4.png";
                                txtRemarks.Enabled = true;

                                divDetails.Style.Add("display", "block");

                                if (!string.IsNullOrEmpty(lblAttachment1.Text) || !string.IsNullOrEmpty(lblAttachment2.Text) || !string.IsNullOrEmpty(lblAttachment3.Text) || !string.IsNullOrEmpty(lblAttachment4.Text))
                                {
                                    pdfSource = ConfigurationManager.AppSettings["DRF_DL_REQUEST_ATTACHMENT_URL"] + lblCTRLNo.Text + "/";

                                    if (!string.IsNullOrEmpty(lblAttachment1.Text))
                                    {
                                        attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment1.Text + "' height='500px' width='1177px'></iframe></td></tr></table>";
                                    }
                                    if (!string.IsNullOrEmpty(lblAttachment2.Text))
                                    {
                                        attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment2.Text + "' height='500px' width='1177px'></iframe></td></tr></table>";
                                    }
                                    if (!string.IsNullOrEmpty(lblAttachment3.Text))
                                    {
                                        attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment3.Text + "' height='500px' width='1177px'></iframe></td></tr></table>";
                                    }
                                    if (!string.IsNullOrEmpty(lblAttachment4.Text))
                                    {
                                        attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment4.Text + "' height='500px' width='1177px'></iframe></td></tr></table>";
                                    }

                                    divAttachment.Style.Add("display", "block");
                                    displayAttachment = attachmentLiteralInside;
                                }

                                if (lblStatAll.Text == "SUPPLIER RESPONDED")
                                {
                                    List<Entities_DRF_RequestEntry> listRequest = new List<Entities_DRF_RequestEntry>();
                                    Entities_DRF_RequestEntry entityRequest = new Entities_DRF_RequestEntry();
                                    entityRequest.CtrlNo = lblCTRLNo.Text;

                                    listRequest = BLL.DRF_TRANSACTION_GetRequestByCTRLNo(entityRequest);

                                    if (listRequest != null)
                                    {
                                        if (listRequest.Count > 0)
                                        {
                                            foreach (Entities_DRF_RequestEntry eRequest in listRequest)
                                            {
                                                if (eRequest.SupplierResponded == "1")
                                                {
                                                    txtResponseType.Text = eRequest.ResponseType;
                                                    txtResponseAnswer.Text = eRequest.ResponseAnswer;
                                                    tableSupplierResponseType.Style.Add("display", "block");
                                                    tableSupplierResponseAnswer.Style.Add("display", "block");
                                                }
                                            }
                                        }
                                    }

                                }

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Unable to close. Attachment or related equivalent document is required! For file attachment please use PDF file only!');", true);
                            }

                        }
                        else
                        {
                            txtRemarks.Enabled = false;
                            ibClosed.ImageUrl = "~/images/Close3.png";

                            divDetails.Style.Add("display", "none");
                            divAttachment.Style.Add("display", "none");
                            displayAttachment = string.Empty;

                        }
                    }
                }

                if (e.CommandName == "lbAttachment_Command")
                {
                    listProofAttachment.Items.Clear();
                    lblAttachment_CtrlNo.Text = lblCTRLNo.Text.ToUpper().Trim();

                    List<Entities_DRF_RequestEntry> listProofAttachment1 = new List<Entities_DRF_RequestEntry>();
                    Entities_DRF_RequestEntry entityProofAttachment = new Entities_DRF_RequestEntry();
                    entityProofAttachment.CtrlNo = lblCTRLNo.Text.ToUpper().Trim();

                    listProofAttachment1 = BLL.DRF_TRANSACTION_ProofAttachmentList(entityProofAttachment);

                    if (listProofAttachment1 != null)
                    {
                        if (listProofAttachment1.Count > 0)
                        {
                            foreach (Entities_DRF_RequestEntry eProof in listProofAttachment1)
                            {
                                listProofAttachment.Items.Add(eProof.ProofAttachment);
                            }

                            btnPreview2.Visible = true;
                        }
                        else
                        {
                            btnPreview2.Visible = false;
                        }
                    }


                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowAttachment", "showDialog();", true);
                }





            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        private void ViewReport(Label lblCTRLNo, TextBox txtResponseType, TextBox txtResponseAnswer, TextBox txtPRNo, TextBox txtPONo, TextBox txtPODate, Label lblSupplier, Label lblAttention, TextBox txtDescription, TextBox txtInvoiceDRNo, TextBox txtReceivedDate, TextBox txtTypeDrawing, TextBox txtOrderQuantity, TextBox txtAbnormalQuantity, TextBox txtTypesOfAbnormality, TextBox txtDetailedReport, Label lblReqManagerStatColor, Label lblPurInchargeStatColor, Label lblPurManagerStatColor, Label lblRequester, Label lblReqManager, Label lblPurIncharge, Label lblPurManager, Label lblReqManagerDOAStat, Label lblPurInchargeDOAStat, Label lblPurManagerDOAStat, Label lblRequesterDOAStat)
        {
            string pathLocation = Server.MapPath("~/DRF_Request/" + lblCTRLNo.Text.Trim() + "/REPORT_" + lblCTRLNo.Text.Trim() + ".html");
            string htmlTemplate = Server.MapPath("~/HTML_Report/DRF/DRF.txt");

            if (System.IO.File.Exists(htmlTemplate))
            {

                List<Entities_DRF_RequestEntry> listRequest = new List<Entities_DRF_RequestEntry>();
                Entities_DRF_RequestEntry entityRequest = new Entities_DRF_RequestEntry();
                entityRequest.CtrlNo = lblCTRLNo.Text;

                listRequest = BLL.DRF_TRANSACTION_GetRequestByCTRLNo(entityRequest);

                if (listRequest != null)
                {
                    if (listRequest.Count > 0)
                    {
                        foreach (Entities_DRF_RequestEntry eRequest in listRequest)
                        {
                            if (eRequest.SupplierResponded == "1")
                            {
                                txtResponseType.Text = eRequest.ResponseType;
                                txtResponseAnswer.Text = eRequest.ResponseAnswer;
                            }
                        }
                    }
                }

                string templateValue = System.IO.File.ReadAllText(htmlTemplate).Replace("val_ctrlno", lblCTRLNo.Text.Trim())
                                                                               .Replace("val_attention", lblAttention.Text)
                                                                               .Replace("val_prno", txtPRNo.Text)
                                                                               .Replace("val_pono", txtPONo.Text)
                                                                               .Replace("val_podate", txtPODate.Text)
                                                                               .Replace("val_invoicedrno", txtInvoiceDRNo.Text)
                                                                               .Replace("val_description", txtDescription.Text)
                                                                               .Replace("val_orderquantity", txtOrderQuantity.Text)
                                                                               .Replace("val_abnormalquantity", txtAbnormalQuantity.Text)
                                                                               .Replace("val_receiveddate", txtReceivedDate.Text)
                                                                               .Replace("val_typesofabnormality", txtTypesOfAbnormality.Text)
                                                                               .Replace("val_detailedreport", txtDetailedReport.Text)
                                                                               .Replace("val_type", txtTypeDrawing.Text)
                                                                               .Replace("val_responsetype", txtResponseType.Text)
                                                                               .Replace("val_responseanswer", txtResponseAnswer.Text)
                                                                               .Replace("val_supplier_conforme", !string.IsNullOrEmpty(txtResponseAnswer.Text) ? lblAttention.Text : string.Empty)
                                                                               .Replace("val_preparedby", lblRequester.Text)
                                                                               .Replace("val_notedby", lblReqManager.Text)
                                                                               .Replace("val_incharge", lblPurIncharge.Text)
                                                                               .Replace("val_manager", lblPurManager.Text)
                                                                               .Replace("val_doa_preparedby", lblRequesterDOAStat.Text)
                                                                               .Replace("val_doa_notedby", lblReqManagerDOAStat.Text)
                                                                               .Replace("val_doa_incharge", lblPurInchargeDOAStat.Text)
                                                                               .Replace("val_doa_manager", lblPurManagerDOAStat.Text)
                                                                               .Replace("bg_v_nb", "background-color:" + lblReqManagerStatColor.Text + ";")
                                                                               .Replace("bg_v_i", "background-color:" + lblPurInchargeStatColor.Text + ";")
                                                                               .Replace("bg_v_m", "background-color:" + lblPurManagerStatColor.Text + ";")
                                                                               .Replace("valtoa_wrongtype", setTypesOfAbnormality(txtTypesOfAbnormality.Text, "valtoa_wrongtype"))
                                                                               .Replace("valtoa_wrongmeasurement", setTypesOfAbnormality(txtTypesOfAbnormality.Text, "valtoa_wrongmeasurement"))
                                                                               .Replace("valtoa_excessquantity", setTypesOfAbnormality(txtTypesOfAbnormality.Text, "valtoa_excessquantity"))
                                                                               .Replace("valtoa_lackingquantity", setTypesOfAbnormality(txtTypesOfAbnormality.Text, "valtoa_lackingquantity"))
                                                                               .Replace("valtoa_incompleteprocessing", setTypesOfAbnormality(txtTypesOfAbnormality.Text, "valtoa_incompleteprocessing"))
                                                                               .Replace("valtoa_misinterpretation", setTypesOfAbnormality(txtTypesOfAbnormality.Text, "valtoa_misinterpretation"))
                                                                               .Replace("valtoa_doubledelivery", setTypesOfAbnormality(txtTypesOfAbnormality.Text, "valtoa_doubledelivery"))
                                                                               .Replace("valtoa_differentmaterial", setTypesOfAbnormality(txtTypesOfAbnormality.Text, "valtoa_differentmaterial"))
                                                                               .Replace("val_supplier", lblSupplier.Text);



                if (!System.IO.File.Exists(pathLocation))
                {
                    using (StreamWriter writer = new StreamWriter(new FileStream(pathLocation, FileMode.Create, FileAccess.Write)))
                    {
                        writer.WriteLine(templateValue);
                    }
                }
                else
                {
                    System.IO.File.Delete(pathLocation);

                    if (!System.IO.File.Exists(pathLocation))
                    {
                        using (StreamWriter writer = new StreamWriter(new FileStream(pathLocation, FileMode.Create, FileAccess.Write)))
                        {
                            writer.WriteLine(templateValue);
                        }
                    }
                }
            }
        }


        protected void btnPreview2_Click(object sender, EventArgs e)
        {
            if (listProofAttachment.Items.Count > 0)
            {

                if (listProofAttachment.SelectedIndex == -1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select a valid attachment in the list before clicking PREVIEW button.');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowAttachment", "showDialog();", true);
                }
                else
                {
                    if (listProofAttachment.SelectedItem.Text.ToLower().Contains(".pdf"))
                    {
                        string URL = "~/DRF_Request/" + lblAttachment_CtrlNo.Text + "/" + listProofAttachment.SelectedItem.Text;

                        URL = Page.ResolveClientUrl(URL);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + URL + "','_blank')", true);

                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowAttachment", "showDialog();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You selected a pretext related document and not a valid pdf file.');", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowAttachment", "showDialog();", true);
                    }
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Unable to PREVIEW empty attachment list.');", true);
            }
        }

        protected void btnProofAttachment_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowAttachment", "showDialog();", true);            

            if (fuProofAttachment.HasFile)
            {                
                string fileNameApplication = System.IO.Path.GetFileName(fuProofAttachment.FileName);
                string fileExtensionApplication = System.IO.Path.GetExtension(fileNameApplication);
                string newFile = fileNameApplication;

                if (fileExtensionApplication.ToLower().Contains(".pdf"))
                {

                    if (!System.IO.File.Exists(Server.MapPath("~/DRF_Request/" + lblAttachment_CtrlNo.Text + "/" + newFile)))
                    {
                        fuProofAttachment.SaveAs(System.IO.Path.Combine(Server.MapPath("~/DRF_Request/" + lblAttachment_CtrlNo.Text), newFile));
                        fuProofAttachment.Dispose();

                        BLL.DRF_TRANSACTION_Insert_ProofAttachment(newFile, Session["LcRefId"].ToString(), lblAttachment_CtrlNo.Text);

                        List<Entities_DRF_RequestEntry> listProofAttachment1 = new List<Entities_DRF_RequestEntry>();
                        Entities_DRF_RequestEntry entityProofAttachment = new Entities_DRF_RequestEntry();
                        entityProofAttachment.CtrlNo = lblAttachment_CtrlNo.Text.ToUpper().Trim();

                        listProofAttachment1 = BLL.DRF_TRANSACTION_ProofAttachmentList(entityProofAttachment);

                        listProofAttachment.Items.Clear();
                        foreach (Entities_DRF_RequestEntry eProof in listProofAttachment1)
                        {
                            listProofAttachment.Items.Add(eProof.ProofAttachment);
                        }

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Attachment file must be PDF only!');", true);
                }

            }

            if (!string.IsNullOrEmpty(txtRelatedDocument.Text))
            {
                BLL.DRF_TRANSACTION_Insert_ProofAttachment(txtRelatedDocument.Text, Session["LcRefId"].ToString(), lblAttachment_CtrlNo.Text);

                List<Entities_DRF_RequestEntry> listProofAttachment1 = new List<Entities_DRF_RequestEntry>();
                Entities_DRF_RequestEntry entityProofAttachment = new Entities_DRF_RequestEntry();
                entityProofAttachment.CtrlNo = lblAttachment_CtrlNo.Text.ToUpper().Trim();

                listProofAttachment1 = BLL.DRF_TRANSACTION_ProofAttachmentList(entityProofAttachment);

                listProofAttachment.Items.Clear();
                foreach (Entities_DRF_RequestEntry eProof in listProofAttachment1)
                {
                    listProofAttachment.Items.Add(eProof.ProofAttachment);
                }
            }



        }

        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblCTRLNo = (Label)e.Row.FindControl("lblCTRLNo");
                    Label lblStatAll = (Label)e.Row.FindControl("lblStatAll");
                    Label lblStatColor = (Label)e.Row.FindControl("lblStatColor");
                    LinkButton lbManualResponse = (LinkButton)e.Row.FindControl("lbManualResponse");
                    DropDownList ddSendDates = (DropDownList)e.Row.FindControl("ddSendDates");

                    lblStatAll.Style.Add("background-color", lblStatColor.Text.Trim());

                    if (lblStatAll.Text == "FOR RESEND")
                    {
                        lbManualResponse.Visible = true;
                    }

                    List<Entities_DRF_RequestEntry> listRequest = new List<Entities_DRF_RequestEntry>();
                    Entities_DRF_RequestEntry entityRequest = new Entities_DRF_RequestEntry();
                    entityRequest.CtrlNo = lblCTRLNo.Text.Trim();

                    listRequest = BLL.DRF_TRANSACTION_GetRequestByCTRLNo(entityRequest);

                    if (listRequest != null)
                    {
                        if (listRequest.Count > 0)
                        {

                            foreach (Entities_DRF_RequestEntry eA in listRequest)
                            {

                                if (!string.IsNullOrEmpty(eA.SendDates))
                                {
                                    ddSendDates.Items.Clear();

                                    string[] values = eA.SendDates.Split(',');
                                    Array.Sort<string>(values);

                                    for (int i = 0; i < values.Length; i++)
                                    {
                                        if (!string.IsNullOrEmpty(values[i].ToString()))
                                        {
                                            ddSendDates.Items.Add(values[i].ToString());
                                        }
                                    }

                                    ddSendDates.SelectedIndex = 1;

                                }
                            }


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

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                string query1 = string.Empty;
                string query2 = string.Empty;
                string query3 = string.Empty;
                string query4 = string.Empty;
                int queryStatusCounter = 0;
                string querySuccess = string.Empty;
                string tempCtrlNo = string.Empty;
                string approvedBy = Session["LcRefId"].ToString();
                string hasForClose = string.Empty;


                if (gvData.Rows.Count > 0)
                {
                    for (int i = 0; i < gvData.Rows.Count; i++)
                    {
                        Label lblCTRLNo = (Label)gvData.Rows[i].Cells[0].FindControl("lblCTRLNo");
                        Label lblStatAll = (Label)gvData.Rows[i].Cells[3].FindControl("lblStatAll");
                        ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[4].FindControl("ibApproved");
                        ImageButton ibClosed = (ImageButton)gvData.Rows[i].Cells[4].FindControl("ibClosed");
                        TextBox txtRemarks = (TextBox)gvData.Rows[i].Cells[6].FindControl("txtRemarks");

                        TextBox txtInvoiceDRNo = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtInvoiceDRNo");
                        TextBox txtPRNo = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtPRNo");
                        TextBox txtPONo = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtPONo");
                        TextBox txtPODate = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtPODate");
                        TextBox txtReceivedDate = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtReceivedDate");
                        TextBox txtCategory = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtCategory");
                        TextBox txtDescription = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtDescription");
                        TextBox txtTypeDrawing = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtTypeDrawing");
                        TextBox txtOrderQuantity = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtOrderQuantity");
                        TextBox txtAbnormalQuantity = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtAbnormalQuantity");
                        TextBox txtTypesOfAbnormality = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtTypesOfAbnormality");
                        TextBox txtDetailedReport = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtDetailedReport");

                        Label lblSupplier = (Label)gvData.Rows[i].Cells[0].FindControl("lblSupplier");
                        Label lblAttention = (Label)gvData.Rows[i].Cells[0].FindControl("lblAttention");
                        Label lblAttachment1 = (Label)gvData.Rows[i].Cells[0].FindControl("lblAttachment1");
                        Label lblAttachment2 = (Label)gvData.Rows[i].Cells[0].FindControl("lblAttachment2");
                        Label lblAttachment3 = (Label)gvData.Rows[i].Cells[0].FindControl("lblAttachment3");
                        Label lblAttachment4 = (Label)gvData.Rows[i].Cells[0].FindControl("lblAttachment4");
                        Label lblSupplierEmail = (Label)gvData.Rows[i].Cells[0].FindControl("lblSupplierEmail");


                        if (ibApproved.ImageUrl == "~/images/A2.png")
                        {
                            string path = Server.MapPath("~/DRF_Request/" + lblCTRLNo.Text.Trim() + "/" + lblCTRLNo.Text.Trim() + ".html");
                            string pathPreview = Server.MapPath("~/DRF_Request/" + lblCTRLNo.Text.Trim() + "/REPORT_" + lblCTRLNo.Text.Trim() + ".html");
                            string htmlTemplate = Server.MapPath("~/UserManual/DRF_Template.txt");
                            string userManualPath = Server.MapPath("~/UserManual/DRF NOTES.docx");
                            string eightDFormat = Server.MapPath("~/UserManual/8D FORMAT.xlsx");
                            string attachmentPath1 = string.Empty;
                            string attachmentPath2 = string.Empty;
                            string attachmentPath3 = string.Empty;
                            string attachmentPath4 = string.Empty;
                            string htmlAttachment = string.Empty;
                            string attachmentPath = string.Empty;
                            string emailService = string.Empty;


                            if (System.IO.File.Exists(htmlTemplate))
                            {
                                string templateValue = System.IO.File.ReadAllText(htmlTemplate).Replace("filename.csv", lblCTRLNo.Text.Trim() + ".csv")
                                                                                               .Replace("val_ctrlno", lblCTRLNo.Text.Trim())
                                                                                               .Replace("val_supplier", lblSupplier.Text)
                                                                                               .Replace("val_attn", lblAttention.Text)
                                                                                               .Replace("val_invoicedrno", txtInvoiceDRNo.Text)
                                                                                               .Replace("val_prpono", txtPRNo.Text)
                                                                                               .Replace("val_pono", txtPONo.Text)
                                                                                               .Replace("val_description", txtDescription.Text)
                                                                                               .Replace("val_typedrawingno", txtTypeDrawing.Text)
                                                                                               .Replace("val_orderquantity", txtOrderQuantity.Text)
                                                                                               .Replace("val_abnormalquantity", txtAbnormalQuantity.Text)
                                                                                               .Replace("val_typesofabnormality", txtTypesOfAbnormality.Text)
                                                                                               .Replace("val_detailedreport", txtDetailedReport.Text)
                                                                                               .Replace("val_title", lblCTRLNo.Text.Trim());


                                if (!System.IO.File.Exists(path))
                                {
                                    using (StreamWriter writer = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write)))
                                    {
                                        writer.WriteLine(templateValue);
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(lblAttachment1.Text))
                            {
                                attachmentPath1 = Server.MapPath("~/DRF_Request/" + lblCTRLNo.Text.Trim() + "/" + lblAttachment1.Text);
                                attachmentPath += attachmentPath1 + ",";
                            }
                            if (!string.IsNullOrEmpty(lblAttachment2.Text))
                            {
                                attachmentPath2 = Server.MapPath("~/DRF_Request/" + lblCTRLNo.Text.Trim() + "/" + lblAttachment2.Text);
                                attachmentPath += attachmentPath2 + ",";
                            }
                            if (!string.IsNullOrEmpty(lblAttachment3.Text))
                            {
                                attachmentPath3 = Server.MapPath("~/DRF_Request/" + lblCTRLNo.Text.Trim() + "/" + lblAttachment3.Text);
                                attachmentPath += attachmentPath3 + ",";
                            }
                            if (!string.IsNullOrEmpty(lblAttachment4.Text))
                            {
                                attachmentPath4 = Server.MapPath("~/DRF_Request/" + lblCTRLNo.Text.Trim() + "/" + lblAttachment4.Text);
                                attachmentPath += attachmentPath4 + ",";
                            }
                            if (System.IO.File.Exists(path))
                            {
                                htmlAttachment = path;
                            }                            

                            //-------------------------------------------------------------------------------------------------------------------
                            // SET BUYER INFORMATION

                            List<Entities_RFQ_BuyerInformation> listBuyerInformation = new List<Entities_RFQ_BuyerInformation>();
                            listBuyerInformation = BLL_RFQ.RFQ_MT_BuyerInformation_GetAll();
                            string buyerInformation = string.Empty;

                            if (listBuyerInformation != null)
                            {
                                if (listBuyerInformation.Count > 0)
                                {
                                    string tableStart = "<table style='width:100%;'><tr><th align='left'>Purchasing Members</th><th align='left'>Section</th><th align='left'>Personal Email</th><th align='left'>Mobile Number</th></tr>";
                                    string tableEnd = "</table>";
                                    string information = string.Empty;
                                    foreach (Entities_RFQ_BuyerInformation eBI in listBuyerInformation)
                                    {
                                        information += "<tr><td>" + eBI.Member + "</td><td>" + eBI.Section + "</td><td>" + eBI.Email + "</td><td>" + eBI.Mobile + "</td></tr>";
                                    }

                                    buyerInformation = tableStart + information + tableEnd;
                                }
                            }

                            //-------------------------------------------------------------------------------------------------------------------

                            string fixedBuyerInfo = buyerInformation;


                            if (System.IO.File.Exists(path))
                            {
                                if (string.IsNullOrEmpty(lblAttachment1.Text) && string.IsNullOrEmpty(lblAttachment2.Text) && string.IsNullOrEmpty(lblAttachment3.Text) && string.IsNullOrEmpty(lblAttachment4.Text))
                                {
                                    attachmentPath = path + "," + userManualPath + "," + eightDFormat + "," + pathPreview;
                                }
                                else
                                {
                                    attachmentPath = attachmentPath + path + "," + userManualPath + "," + eightDFormat + "," + pathPreview;
                                }

                                emailService = COMMON.sendEmailToSuppliersDRF(lblSupplierEmail.Text.Trim(), ConfigurationManager.AppSettings["email-username"], lblCTRLNo.Text.Trim(),
                                            "Hi <b>" + lblSupplier.Text.ToUpper() + "</b> Good Day!" + "<br /><br /> Kindly check the attached html file (" + lblCTRLNo.Text.Trim() + ".html) for our DRF Request" + "<br /><br /><p style='font-size:22px; color:red;'><b>NOTE : </b></p><p style='font-size:18px;'>Please refer to the attached document file (DRF NOTES.docx) on how to update or put your response accordingly.</p>" +
                                            "<br /><br />For this DRF, please contact <b>" + txtCategory.Text.ToUpper() + "</b> Section <br /> <b>PLEASE DO REPLY WITHIN 48 HOURS</b> <br /><br />" +
                                            "<br /><br /><br />Thank You!<br /><br /><br />" +
                                            "*** This is an automatically generated email, Please do reply accordingly *** <br /> <br />" +
                                            "You have received a new DRF Request from ROHM Electronics Philippines Inc." +
                                            "<br /> <br /><br /><br /> For inquries, kindly see below contact details : <br />" + fixedBuyerInfo, attachmentPath, lblSupplier.Text.ToUpper(), lblCTRLNo.Text.Trim());

                            }

                            if (emailService.ToLower().Contains("success"))
                            {
                                tempCtrlNo += "(" + lblCTRLNo.Text.Trim().ToUpper() + " - SUCCESSFULLY SENT) <br />";
                                
                                query1 += "UPDATE DRF_TRANSACTION_Status SET BuyerSend ='" + approvedBy + "', DOABuyerSend = CONVERT(VARCHAR, GETDATE(), 22), STATBuyerSend = 1 WHERE CTRLNo = '" + lblCTRLNo.Text.Trim() + "' ";
                                queryStatusCounter++;
                                query2 += "INSERT INTO DRF_TRANSACTION_SendReceived (CTRLNo, SendReceivedDate, TransactionType) VALUES ('" + lblCTRLNo.Text.Trim() + "', GETDATE(), 'SEND') ";
                                queryStatusCounter++;                                
                            }
                            else
                            {
                                tempCtrlNo += "(" + lblCTRLNo.Text.Trim().ToUpper() + " - FAILED TO SEND) <br />";
                            }                            

                        }

                        if (ibClosed.ImageUrl == "~/images/Close4.png")
                        {
                            if (string.IsNullOrEmpty(txtRemarks.Text))
                            {
                                hasForClose += lblCTRLNo.Text + ", ";                                
                            }
                            else
                            {
                                query3 += "UPDATE DRF_TRANSACTION_Status SET PostedBy ='" + approvedBy + "', PostingRemarks = '" + txtRemarks.Text + "', PostedDate = CONVERT(VARCHAR, GETDATE(), 22), Posted = 1 WHERE CTRLNo = '" + lblCTRLNo.Text.Trim() + "' ";
                                queryStatusCounter++;
                                tempCtrlNo += "(" + lblCTRLNo.Text.Trim().ToUpper() + " - CLOSED <br />";
                            }

                        }                        


                    }                    

                }

                if (string.IsNullOrEmpty(hasForClose))
                {

                    querySuccess = BLL.DRF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + query2 + query3 + queryEndPart).ToString();

                    if (querySuccess == queryStatusCounter.ToString())
                    {

                        Session["successMessage"] = "DRF NUMBER(S) : <br /> <b>" + tempCtrlNo + "</b> <br /> HAS BEEN SUCCESSFULLY PROCESSED.";
                        Session["successTransactionName"] = "DRF_RECEIVINGENTRY";
                        Session["successReturnPage"] = "DRF_ReceivingEntry.aspx";

                        Response.Redirect("SuccessPage.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('ERROR SENDING REQUEST');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('CLOSE REMARKS FOR " + hasForClose + " MUST NOT BE BLANK!');", true);
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


        private string setTypesOfAbnormality(string reason, string value)
        {
            string ret = "white;";

            if (reason == "WRONG TYPE" && value == "valtoa_wrongtype")
            {
                ret = "black;";
            }

            else if (reason == "WRONG MEASUREMENT" && value == "valtoa_wrongmeasurement")
            {
                ret = "black;";
            }

            else if (reason == "EXCESS QUANTITY RECEIVED" && value == "valtoa_excessquantity")
            {
                ret = "black;";
            }

            else if (reason == "LACKING QUANTITY" && value == "valtoa_lackingquantity")
            {
                ret = "black;";
            }

            else if (reason == "INCOMPLETE PROCESSING" && value == "valtoa_incompleteprocessing")
            {
                ret = "black;";
            }

            else if (reason == "MISINTERPRETATION OF DRAWING" && value == "valtoa_misinterpretation")
            {
                ret = "black;";
            }

            else if (reason == "DOUBLE DELIVERY" && value == "valtoa_doubledelivery")
            {
                ret = "black;";
            }

            else if (reason == "DIFFERENT MATERIALS USED" && value == "valtoa_differentmaterial")
            {
                ret = "black;";
            }

            else if (reason == "" && value == "valtoa_others")
            {
                ret = "black;";
            }


            return ret;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvData.Rows.Count > 0)
            {
                //btnSubmit_Click(sender, e);
                gvExport.Visible = true;
                Response.Clear();
                Response.Buffer = true;

                Response.AddHeader("content-disposition",
                "attachment;filename=DRF_Export.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //Change the Header Row back to white color
                gvExport.HeaderRow.Style.Add("background-color", "#FFFFFF");
                gvExport.HeaderRow.Style.Add("color", "#000000");


                gvExport.RenderControl(hw);

                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
                gvExport.Visible = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('CANNOT EXPORT EMPTY RECORDS');", true);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }

        








    }
}
