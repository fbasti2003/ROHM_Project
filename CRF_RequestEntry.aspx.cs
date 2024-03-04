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
    public partial class CRF_RequestEntry : System.Web.UI.Page
    {

        BLL_CRF BLL = new BLL_CRF();
        Common COMMON = new Common();
        public string displayAttachment = string.Empty;
        public string displayCTRLNo = string.Empty;

        private string ddumitems;
        public string Ddumitems { get { return ddumitems; } set { ddumitems = value; } }

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
                    if (!String.IsNullOrEmpty(Request.QueryString["CRFNo_From_Inquiry"].ToString()))
                    {
                        string ctrlno = CryptorEngine.Decrypt(Request.QueryString["CRFNo_From_Inquiry"].ToString().Replace(" ", "+"), true);
                        divApprover.Style.Add("display", "block");
                        LoadDefaultForUpdate(ctrlno);
                        Page.Title = ctrlno;
                        displayCTRLNo = "<b style='color:red'>(" + ctrlno + ")</>";

                        // TRANSFER DETAILS
                        BLL_RFQ BLLRFQ = new BLL_RFQ();

                        List<Entities_RFQ_RequestEntry> List_TransferHistory = new List<Entities_RFQ_RequestEntry>();
                        Entities_RFQ_RequestEntry entityTransferHistory = new Entities_RFQ_RequestEntry();
                        entityTransferHistory.SearchCriteria = ctrlno;
                        List_TransferHistory = BLLRFQ.RFQ_TRANSACTION_HistoryOfUpdates(entityTransferHistory);

                        if (List_TransferHistory != null)
                        {
                            if (List_TransferHistory.Count > 0)
                            {
                                gvTransferDetails.DataSource = List_TransferHistory;
                                gvTransferDetails.DataBind();

                                tableTransferDetails.Style.Add("display", "block");
                            }
                        }
                    }
                    else
                    {
                        //tableAttachment.Style.Add("display", "block");
                        LoadDefault();
                        Ddumitems = "<asp:ListItem Text='NO' Value='0'></asp:ListItem>";
                        FirstGridViewRow();
                        displayCTRLNo = string.Empty;
                        divApprover.Style.Add("display", "none");

                        ////---------------------------------------------------------------------------------------------------------
                        //int t1 = int.Parse(DateTime.Now.ToString("HHmm").ToString());
                        //int t2 = int.Parse(ConfigurationManager.AppSettings["RFQ-CUT-OFF-TIME"].ToString());
                        //string notif = string.Empty;

                        //if (t1 > t2)
                        //{
                        //    notif = "Please be informed that this request is going be processed on the next day. Cut-Off time is 2PM.";
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessageCutOff('" + notif + "');", true);
                        //}

                        ////---------------------------------------------------------------------------------------------------------

                        string notif = string.Empty;
                        notif = "Please be informed that the request cut-off time is 2PM.";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessageCutOff('" + notif + "');", true);

                    }
                }

            }
        }

        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("RdRefId", typeof(string)));
            dt.Columns.Add(new DataColumn("RdPONO", typeof(string)));
            dt.Columns.Add(new DataColumn("RdPRNO", typeof(string)));
            dt.Columns.Add(new DataColumn("RdItemName", typeof(string)));
            dt.Columns.Add(new DataColumn("RdSpecs", typeof(string)));
            dt.Columns.Add(new DataColumn("RdQuantity", typeof(string)));
            dt.Columns.Add(new DataColumn("RdUnitOfMeasure", typeof(string)));
            dt.Columns.Add(new DataColumn("RdPODate", typeof(string)));
            dt.Columns.Add(new DataColumn("RdReason", typeof(string)));
            dt.Columns.Add(new DataColumn("RdDateInformedSupplier", typeof(string)));
            dt.Columns.Add(new DataColumn("RdDateInformedRequester", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["RdRefId"] = string.Empty;
            dr["RdPONO"] = string.Empty;
            dr["RdPRNO"] = string.Empty;
            dr["RdItemName"] = string.Empty;
            dr["RdSpecs"] = string.Empty;
            dr["RdQuantity"] = string.Empty;
            dr["RdUnitOfMeasure"] = string.Empty;
            dr["RdPODate"] = string.Empty;
            dr["RdReason"] = string.Empty;
            dr["RdDateInformedSupplier"] = string.Empty;
            dr["RdDateInformedRequester"] = string.Empty;

            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;

            gvData.DataSource = dt;
            gvData.DataBind();


        }

        private void LoadDefault()
        {
            try
            {

                //---------------------------------------------------------------------------------------------------

                List<Entities_CRF_RequestEntry> listDropDown = new List<Entities_CRF_RequestEntry>();
                listDropDown = BLL.CRF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                if (listDropDown != null)
                {
                    if (listDropDown.Count > 0)
                    {
                        //ddReason.Items.Add("");
                        ddCategory.Items.Add("");
                        ddSupplier.Items.Add("");

                        foreach (Entities_CRF_RequestEntry entity in listDropDown)
                        {
                            ListItem item = new ListItem();
                            item.Text = entity.DropdownName.ToUpper();
                            item.Value = entity.DropdownRefId;

                            if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                            {
                                //if (entity.TableName == "CRF_MT_Reason")
                                //{
                                //    ddReason.Items.Add(item);
                                //}
                                if (entity.TableName == "MT_Category")
                                {
                                    //if (entity.DropdownRefId == "1013" || entity.DropdownRefId == "1006")
                                    //{
                                    //    //DO NOT ADD THIS CATEGORY
                                    //}
                                    //else
                                    //{
                                    //    ddCategory.Items.Add(item);
                                    //}
                                    if (entity.DropdownRefId == "1013" || entity.DropdownRefId == "1006"
                                        || entity.DropdownRefId == "1015" || entity.DropdownRefId == "3" || entity.DropdownRefId == "1"
                                        || entity.DropdownRefId == "7" || entity.DropdownRefId == "1014" || entity.DropdownRefId == "1019")
                                    {
                                        //DO NOT ADD THIS CATEGORY
                                    }
                                    else
                                    {
                                        ddCategory.Items.Add(item);
                                    }
                                }
                                if (entity.TableName == "MT_Supplier_Head")
                                {
                                    ddSupplier.Items.Add(item);
                                }
                            }

                        }

                    }
                }

                //---------------------------------------------------------------------------------------------------


                txtRequesterName.Text = Session["UserFullName"].ToString();
                txtRequesterEmail.Text = Session["UserEmail"].ToString();
                txtRequesterLocalNumber.Text = Session["LocalNumber"].ToString();

                txtRequesterName.Enabled = false;
                txtRequesterEmail.Enabled = false;
                txtRequesterLocalNumber.Enabled = false;

                btnSubmit.Visible = true;
                btnSubmit.Text = "SUBMIT";

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        private void LoadDefaultForUpdate(string ctrlno)
        {
            try
            {
                string attachmentLiteralInside = string.Empty;
                string pdfSource = string.Empty;

                //---------------------------------------------------------------------------------------------------

                List<Entities_CRF_RequestEntry> listDropDown = new List<Entities_CRF_RequestEntry>();
                listDropDown = BLL.CRF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                if (listDropDown != null)
                {
                    if (listDropDown.Count > 0)
                    {
                        ddCategory.Items.Add("");
                        ddSupplier.Items.Add("");

                        foreach (Entities_CRF_RequestEntry entity in listDropDown)
                        {
                            ListItem item = new ListItem();
                            item.Text = entity.DropdownName.ToUpper();
                            item.Value = entity.DropdownRefId;

                            if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                            {
                                if (entity.TableName == "MT_Category")
                                {
                                    if (entity.DropdownRefId == "1013" || entity.DropdownRefId == "1006")
                                    {
                                        //DO NOT ADD THIS CATEGORY
                                    }
                                    else
                                    {
                                        ddCategory.Items.Add(item);
                                    }
                                }
                                if (entity.TableName == "MT_Supplier_Head")
                                {
                                    ddSupplier.Items.Add(item);
                                }
                            }

                        }

                    }
                }


                //---------------------------------------------------------------------------------------------------

                List<Entities_CRF_RequestEntry> listRequest = new List<Entities_CRF_RequestEntry>();

                Entities_CRF_RequestEntry entityRequest = new Entities_CRF_RequestEntry();
                entityRequest.CtrlNo = ctrlno;

                listRequest = BLL.CRF_TRANSACTION_GetRequestByCTRLNo(entityRequest);

                if (listRequest != null)
                {
                    if (listRequest.Count > 0)
                    {
                        foreach (Entities_CRF_RequestEntry eRequest in listRequest)
                        {
                            ddCategory.Items.FindByValue(eRequest.CategoryId).Selected = true;
                            ddSupplier.Items.FindByValue(eRequest.Supplier).Selected = true;

                            txtAttention.Text = eRequest.Attention;
                            txtRequesterName.Text = eRequest.Requester;
                            txtRequesterEmail.Text = eRequest.RequesterEmail;
                            txtRequesterLocalNumber.Text = eRequest.RequesterLocalNumber;

                            txtRequesterName.Enabled = false;
                            txtRequesterEmail.Enabled = false;
                            txtRequesterLocalNumber.Enabled = false;

                            lblRequester.Text = eRequest.RequesterS;
                            lblRequesterDOA.Text = eRequest.RequesterSDOA;
                            lblProdManager.Text = eRequest.ReqManager;
                            lblProdManagerDOA.Text = eRequest.ReqManagerDOA;
                            lblBuyer.Text = eRequest.PurIncharge;
                            lblBuyerDOA.Text = eRequest.PurInchargeDOA;
                            lblPurManager.Text = eRequest.PurManager;
                            lblPurManagerDOA.Text = eRequest.PurManagerDOA;

                            // GET DETAILS --------------------------------------------------------------------------------------
                            List<Entities_CRF_RequestEntry> listDetails = new List<Entities_CRF_RequestEntry>();
                            Entities_CRF_RequestEntry entityDetails = new Entities_CRF_RequestEntry();
                            entityDetails.RdCtrlNo = ctrlno;
                            
                            listDetails = BLL.CRF_TRANSACTION_GetRequestDetailsByCTRLNo(entityDetails);

                            if (listDetails != null)
                            {
                                if (listDetails.Count > 0)
                                {
                                    gvData.DataSource = listDetails;
                                    gvData.DataBind();

                                    foreach (Entities_CRF_RequestEntry entityAttachment in listDetails)
                                    {
                                        if (!string.IsNullOrEmpty(entityAttachment.RdSupplierAttachment))
                                        {
                                            tableSupplierAttachment.Style.Add("display", "block");
                                            lbSupplierAttachment.Text = entityAttachment.RdSupplierAttachment;
                                            
                                        }
                                    }
                                }


                                
                            }
                            //----------------------------------------------------------------------------------------------------

                            // SEND DATES
                            if (!string.IsNullOrEmpty(eRequest.SendDates))
                            {
                                var query = from val in eRequest.SendDates.Split(',')
                                            select val;

                                ddSendDates.Items.Clear();

                                foreach (string str in query)
                                {
                                    ddSendDates.Items.Add(str);
                                }
                            }


                            txtRemarks.Text = eRequest.StatRemarks;

                            divRequester.Style.Add("background-color", "#00C851");

                            lblRequesterDOA.Text = Convert.ToDateTime(lblRequesterDOA.Text).ToString("MM'/'dd'/'yyyy h:mm tt");

                            // STATUS REQ MANAGER
                            if (eRequest.ReqManagerStat == "1")
                            {
                                divProdManager.Style.Add("background-color", "#00C851");
                                lblProdManagerDOA.Text = lblProdManagerDOA.Text;
                            }
                            if (eRequest.ReqManagerStat == "2")
                            {
                                divProdManager.Style.Add("background-color", "#ffbb33");
                                lblProdManagerDOA.Text = lblProdManagerDOA.Text;
                            }
                            if (eRequest.ReqManagerStat == "0")
                            {
                                divProdManager.Style.Add("background-color", "#f44336");
                                lblProdManager.Text = "PENDING APPROVAL";
                            }
                            // STATUS BUYER
                            if (eRequest.PurInchargeStat == "1")
                            {
                                divBuyer.Style.Add("background-color", "#00C851");
                                lblBuyerDOA.Text = lblBuyerDOA.Text;
                            }
                            if (eRequest.PurInchargeStat == "2")
                            {
                                divBuyer.Style.Add("background-color", "#ffbb33");
                                lblBuyerDOA.Text = lblBuyerDOA.Text;
                            }
                            if (eRequest.PurInchargeStat == "0")
                            {
                                divBuyer.Style.Add("background-color", "#f44336");
                                lblBuyer.Text = "PENDING APPROVAL";
                            }
                            // PUR MANAGER
                            if (eRequest.PurManagerStat == "1")
                            {
                                divPurManager.Style.Add("background-color", "#00C851");
                                lblPurManagerDOA.Text = lblPurManagerDOA.Text;
                            }
                            if (eRequest.PurManagerStat == "2")
                            {
                                divPurManager.Style.Add("background-color", "#ffbb33");
                                lblPurManagerDOA.Text = lblPurManagerDOA.Text;
                            }
                            if (eRequest.PurManagerStat == "0")
                            {
                                divPurManager.Style.Add("background-color", "#f44336");
                                lblPurManager.Text = "PENDING APPROVAL";
                            }
                            // POSTING REMARKS
                            if (!string.IsNullOrEmpty(eRequest.PostingRemarks))
                            {
                                txtBuyerCloseRemarks.Text = eRequest.PostingRemarks;
                                tableCloseRemarks.Style.Add("display", "block");
                            }

                            // PURCHASING DISAPPROVAL REMARKS
                            if (!string.IsNullOrEmpty(eRequest.StatRemarks))
                            {
                                tableRemarks.Style.Add("display", "block");
                            }
                            else
                            {
                                tableRemarks.Style.Add("display", "none");
                            }
                            // BUYER SEND
                            if (eRequest.BuyerSendStat == "1")
                            {
                                divBuyerSend.Style.Add("background-color", "#8BC34A");
                                lblBuyerSend.Text = "SENT TO SUPPLIER";
                                lblBuyerSendDOA.Text = eRequest.BuyerSendDOA;
                            }
                            if (eRequest.BuyerSendStat == "0")
                            {
                                divBuyerSend.Style.Add("background-color", "#f44336");
                                lblBuyerSend.Text = "PENDING APPROVAL";
                            }
                            // SUPPLIER
                            if (eRequest.SupplierResponded == "1")
                            {
                                divSupplier.Style.Add("background-color", "#673AB7");
                                lblSupplier.Text = "SUPPLIER RESPONDED";
                                lblSupplierDOA.Text = eRequest.SupplierResponseDate;
                                //tableSupplierResponseConfirmedBy.Style.Add("display", "block");
                                //tableSupplierResponseDateConfirmed.Style.Add("display", "block");
                                //tableSupplierResponseNotes.Style.Add("display", "block");
                                //txtResponseConfirmedBy.Text = eRequest.ResponseConfirmedBy;
                                //txtResponseDateConfirmed.Text = eRequest.ResponseDateConfirmed;
                                //txtResponseNotes.Text = eRequest.ResponseNotes;

                                //tableSupplierResponseAttachment.Style.Add("display", "block");
                                List<Entities_CRF_RequestEntry> listSupplierAttachment = new List<Entities_CRF_RequestEntry>();
                                listSupplierAttachment = BLL.CRF_TRANSACTION_GetSupplierAttachmentByCTRLNo(eRequest);

                                if (listSupplierAttachment != null)
                                {
                                    if (listSupplierAttachment.Count > 0)
                                    {
                                        //ddSupplierAttachment.Items.Clear();
                                        //ddSupplierAttachment.Items.Add("");
                                        //foreach (Entities_CRF_RequestEntry listAttachment in listSupplierAttachment)
                                        //{
                                        //    ddSupplierAttachment.Items.Add(listAttachment.SupplierAttachment);
                                        //}
                                    }
                                }

                            }
                            if (eRequest.SupplierResponded == "0")
                            {
                                divSupplier.Style.Add("background-color", "#f44336");
                                lblSupplier.Text = "PENDING APPROVAL";
                            }
                            // ITEM STATUS
                            if (eRequest.Posted == "1")
                            {
                                divItemStatus.Style.Add("background-color", "#00C851");
                                lblItemStatus.Text = "TRANSACTION CLOSED";
                                lblItemStatusDOA.Text = eRequest.PostedDate;
                            }
                            if (eRequest.Posted == "0")
                            {
                                divItemStatus.Style.Add("background-color", "#f44336");
                                lblItemStatus.Text = "TRANSACTION OPEN";
                            }


                            //tableAttachment.Style.Add("display", "none");

                            string catAccess = Session["CategoryAccess"].ToString();

                            if (!string.IsNullOrEmpty(catAccess))
                            {
                                if (int.Parse(catAccess) > 0)
                                {
                                    btnSubmit.Visible = true;
                                    btnSubmit.Text = "SUBMIT";
                                }
                                else
                                {
                                    if (eRequest.RequesterSStat == "1" && eRequest.ReqManagerStat == "1")
                                    {
                                        btnSubmit.Visible = false;
                                    }
                                    if (eRequest.RequesterSStat == "1" && eRequest.ReqManagerStat == "0" && eRequest.Requester.Trim().ToUpper() == Session["UserFullName"].ToString().ToUpper())
                                    {
                                        btnSubmit.Visible = true;
                                        btnSubmit.Text = "UPDATE";
                                    }
                                }
                            }
                            //------------------------------------------------------------------------------------

                            pdfSource = ConfigurationManager.AppSettings["CRF_DL_REQUEST_ATTACHMENT_URL"] + eRequest.CtrlNo + "/";

                            if (listDetails != null)
                            {
                                if (listDetails.Count > 0)
                                {

                                    foreach (Entities_CRF_RequestEntry eAtt in listDetails)
                                    {

                                        if (!string.IsNullOrEmpty(eAtt.Attachment1) || !string.IsNullOrEmpty(eAtt.Attachment2) || !string.IsNullOrEmpty(eAtt.Attachment3) || !string.IsNullOrEmpty(eAtt.Attachment4))
                                        {
                                            if (!string.IsNullOrEmpty(eAtt.Attachment1))
                                            {
                                                attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + eAtt.Attachment1 + "' height='500px' width='1213px'></iframe></td></tr></table>";
                                            }                                            
                                        }


                                    }

                                }
                            }
                            
                            
                            if (!string.IsNullOrEmpty(attachmentLiteralInside))
                            {
                                divAttachment.Style.Add("display", "block");
                                displayAttachment = attachmentLiteralInside;
                            }
                            else
                            {
                                divAttachment.Style.Add("display", "none");
                                displayAttachment = string.Empty;
                            }

                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------


                // check first if login user is from purchasing peep
                if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()) ||
                    Session["Username"].ToString() == "6985" || Session["Username"].ToString() == "3844" || Session["Username"].ToString() == "1152" || Session["Username"].ToString() == "1402" || Session["Username"].ToString() == "002")
                {
                    ddCategory.Enabled = true;
                }
                else
                {
                    ddCategory.Enabled = false;
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvDataLinkToPR_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                Label lblDataIndex = row.FindControl("lblDataIndex") as Label;
                HtmlGenericControl divDataLinkToPR = gvData.Rows[int.Parse(lblDataIndex.Text.Trim())].FindControl("divDataLinkToPR") as HtmlGenericControl;
                Label lblLinkPONO = row.FindControl("lblLinkPONO") as Label;
                Label lblLinkPRNO = row.FindControl("lblLinkPRNO") as Label;
                Label lblLinkItemName = row.FindControl("lblLinkItemName") as Label;
                Label lblLinkSpecs = row.FindControl("lblLinkSpecs") as Label;
                Label lblLinkQuantity = row.FindControl("lblLinkQuantity") as Label;
                Label lblLinkUnitOfMeasure = row.FindControl("lblLinkUnitOfMeasure") as Label;


                Label lblDescription = row.FindControl("lblDescription") as Label;
                Label lblTypeDrawingNo = row.FindControl("lblTypeDrawingNo") as Label;
                Label lblOrderQuantity = row.FindControl("lblOrderQuantity") as Label;
                Label lblUnitOfMeasure = row.FindControl("lblUnitOfMeasure") as Label;
                Label lblLinkPODate = row.FindControl("lblLinkPODate") as Label;

                TextBox txtPONO = gvData.Rows[int.Parse(lblDataIndex.Text.Trim())].FindControl("txtPONO") as TextBox;
                TextBox txtPRNO = gvData.Rows[int.Parse(lblDataIndex.Text.Trim())].FindControl("txtPRNO") as TextBox;
                TextBox txtItemName = gvData.Rows[int.Parse(lblDataIndex.Text.Trim())].FindControl("txtItemName") as TextBox;
                TextBox txtSpecification = gvData.Rows[int.Parse(lblDataIndex.Text.Trim())].FindControl("txtSpecification") as TextBox;
                TextBox txtQuantity = gvData.Rows[int.Parse(lblDataIndex.Text.Trim())].FindControl("txtQuantity") as TextBox;
                TextBox txtPODate = gvData.Rows[int.Parse(lblDataIndex.Text.Trim())].FindControl("txtPODate") as TextBox;

                DropDownList ddUOM = gvData.Rows[int.Parse(lblDataIndex.Text.Trim())].FindControl("ddUOM") as DropDownList;

                if (e.CommandName == "btnLinkSelectPR_Command")
                {
                    txtPONO.Text = lblLinkPONO.Text;
                    txtPRNO.Text = lblLinkPRNO.Text;
                    txtItemName.Text = lblLinkItemName.Text;
                    txtSpecification.Text = lblLinkSpecs.Text;
                    txtQuantity.Text = lblLinkQuantity.Text;
                    txtPODate.Text = lblLinkPODate.Text.Replace("12:00:00 AM", "").Trim();

                    //---------------------------------------------------------------------------------------------------

                    List<Entities_CRF_RequestEntry> listDropDown = new List<Entities_CRF_RequestEntry>();
                    listDropDown = BLL.CRF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                    if (listDropDown != null)
                    {
                        if (listDropDown.Count > 0)
                        {
                            ddUOM.Items.Clear();
                            ddUOM.Items.Add("");

                            foreach (Entities_CRF_RequestEntry entity in listDropDown)
                            {
                                ListItem item = new ListItem();
                                item.Text = entity.DropdownName.ToUpper();
                                item.Value = entity.DropdownRefId;

                                if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                                {                                    
                                    if (entity.TableName == "MT_UnitOfMeasure")
                                    {
                                        ddUOM.Items.Add(item);
                                    }
                                }

                            }

                            ddUOM.Items.FindByText(lblLinkUnitOfMeasure.Text.ToUpper().Trim()).Selected = true;

                        }
                    }

                    //---------------------------------------------------------------------------------------------------

                    divDataLinkToPR.Style.Add("display", "none");
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void lbSupplierAttachment_Click(object sender, EventArgs e)
        {
            try
            {
                string URL = "~/CRF_Received/" + CryptorEngine.Decrypt(Request.QueryString["CRFNo_From_Inquiry"].ToString().Replace(" ", "+"), true) + "/" + lbSupplierAttachment.Text.Replace("%20", " ");

                URL = Page.ResolveClientUrl(URL);
                lbSupplierAttachment.OnClientClick = "window.open('" + URL + "'); return false;";

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //tableDataLinkToPR.Style.Add("display", "none");

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnPONO_Click(object sender, EventArgs e)
        {
            try
            {
                //List<Entities_CRF_RequestEntry> listDataLink = new List<Entities_CRF_RequestEntry>();
                //listDataLink = BLL.CRF_TRANSACTION_DataLinkToPR_ByPONO(txtPONO.Text);

                //if (listDataLink != null)
                //{
                //    if (listDataLink.Count > 0)
                //    {
                //        gvDataLinkToPR.DataSource = listDataLink;
                //        gvDataLinkToPR.DataBind();
                //        tableDataLinkToPR.Style.Add("display", "block");
                //    }
                //    else
                //    {
                //        tableDataLinkToPR.Style.Add("display", "none");
                //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('No record found!');", true);
                //    }
                //}

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnPRNO_Click(object sender, EventArgs e)
        {
            try
            {
                //List<Entities_CRF_RequestEntry> listDataLink = new List<Entities_CRF_RequestEntry>();
                //listDataLink = BLL.CRF_TRANSACTION_DataLinkToPR_ByPRNO(txtPRNO.Text);

                //if (listDataLink != null)
                //{
                //    if (listDataLink.Count > 0)
                //    {
                //        gvDataLinkToPR.DataSource = listDataLink;
                //        gvDataLinkToPR.DataBind();
                //        tableDataLinkToPR.Style.Add("display", "block");
                //    }
                //    else
                //    {
                //        tableDataLinkToPR.Style.Add("display", "none");
                //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('No record found!');", true);
                //    }
                //}

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int isNoError = 0;
                string success = string.Empty;
                int emptyReason = 0;
                int emtpyUnitOfMeasure = 0;

                if (string.IsNullOrEmpty(ddSupplier.SelectedValue))
                {
                    ddSupplier.Style.Add("background-color", "Red");
                    pRequired.Style.Add("display", "block");
                    isNoError++;
                }
                else
                {
                    ddSupplier.Style.Add("background-color", "White");
                    pRequired.Style.Add("display", "none");
                }
                //--------------------------------------------------------------------
                if (string.IsNullOrEmpty(txtAttention.Text))
                {
                    txtAttention.Style.Add("background-color", "Red");
                    pRequired.Style.Add("display", "block");
                    isNoError++;
                }
                else
                {
                    txtAttention.Style.Add("background-color", "White");
                    pRequired.Style.Add("display", "none");
                }
                //--------------------------------------------------------------------
                //if (string.IsNullOrEmpty(txtPRNO.Text))
                //{
                //    txtPRNO.Style.Add("background-color", "Red");
                //    pRequired.Style.Add("display", "block");
                //    isNoError = false;
                //}
                //else
                //{
                //    txtPRNO.Style.Add("background-color", "White");
                //    pRequired.Style.Add("display", "none");
                //    isNoError = true;
                //}
                //--------------------------------------------------------------------

                //if (string.IsNullOrEmpty(txtPONO.Text))
                //{
                //    txtPONO.Style.Add("background-color", "Red");
                //    pRequired.Style.Add("display", "block");
                //    isNoError = false;
                //}
                //else
                //{
                //    txtPONO.Style.Add("background-color", "White");
                //    pRequired.Style.Add("display", "none");
                //    isNoError = true;
                //}
                //--------------------------------------------------------------------

                //if (string.IsNullOrEmpty(txtPODate.Text))
                //{
                //    txtPODate.Style.Add("background-color", "Red");
                //    pRequired.Style.Add("display", "block");
                //    isNoError = false;
                //}
                //else
                //{
                //    txtPODate.Style.Add("background-color", "White");
                //    pRequired.Style.Add("display", "none");
                //    isNoError = true;
                //}
                //--------------------------------------------------------------------
                if (string.IsNullOrEmpty(ddCategory.SelectedValue))
                {
                    ddCategory.Style.Add("background-color", "Red");
                    pRequired.Style.Add("display", "block");
                    isNoError++;
                }
                else
                {
                    ddCategory.Style.Add("background-color", "White");
                    pRequired.Style.Add("display", "none");
                }

                // VALIDATE REASON -------------------------------------------------------------------------------------
                for (int i = 0; i < gvData.Rows.Count; i++)
                {
                    DropDownList ddReason = (DropDownList)gvData.Rows[i].Cells[8].FindControl("ddReason");

                    if (string.IsNullOrEmpty(ddReason.SelectedValue))
                    {
                        emptyReason++;
                        ddReason.Style.Add("background-color", "Red");
                    }
                    else
                    {
                        ddReason.Style.Add("background-color", "White");
                    }
                }

                if (emptyReason > 0)
                {
                    isNoError++;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('Reason must not be blank. Please check reason that is highlighted in RED.');", true);
                }

                // VALIDATE UNIT OF MEASURE -------------------------------------------------------------------------------------
                for (int i = 0; i < gvData.Rows.Count; i++)
                {
                    DropDownList ddUOM = (DropDownList)gvData.Rows[i].Cells[6].FindControl("ddUOM");

                    if (string.IsNullOrEmpty(ddUOM.SelectedValue))
                    {
                        emtpyUnitOfMeasure++;
                        ddUOM.Style.Add("background-color", "Red");
                    }
                    else
                    {
                        ddUOM.Style.Add("background-color", "White");
                    }
                }

                if (emtpyUnitOfMeasure > 0)
                {
                    isNoError++;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('Unit Of Measure must not be blank. Please check UOM that is highlighted in RED.');", true);
                }
                // -----------------------------------------------------------------------------------------------------
                //--------------------------------------------------------------------
                //if (string.IsNullOrEmpty(txtDescription.Text))
                //{
                //    txtDescription.Style.Add("background-color", "Red");
                //    pRequired.Style.Add("display", "block");
                //    isNoError = false;
                //}
                //else
                //{
                //    txtDescription.Style.Add("background-color", "White");
                //    pRequired.Style.Add("display", "none");
                //    isNoError = true;
                //}
                ////--------------------------------------------------------------------
                //if (string.IsNullOrEmpty(txtTypeDrawing.Text))
                //{
                //    txtTypeDrawing.Style.Add("background-color", "Red");
                //    pRequired.Style.Add("display", "block");
                //    isNoError = false;
                //}
                //else
                //{
                //    txtTypeDrawing.Style.Add("background-color", "White");
                //    pRequired.Style.Add("display", "none");
                //    isNoError = true;
                //}
                //--------------------------------------------------------------------
                //if (string.IsNullOrEmpty(txtOrderQuantity.Text))
                //{
                //    txtOrderQuantity.Style.Add("background-color", "Red");
                //    pRequired.Style.Add("display", "block");
                //    isNoError = false;
                //}
                //else
                //{
                //    txtOrderQuantity.Style.Add("background-color", "White");
                //    pRequired.Style.Add("display", "none");
                //    isNoError = true;
                //}
                ////--------------------------------------------------------------------
                //if (string.IsNullOrEmpty(ddReason.SelectedValue))
                //{
                //    ddReason.Style.Add("background-color", "Red");
                //    pRequired.Style.Add("display", "block");
                //    isNoError = false;
                //}
                //else
                //{
                //    ddReason.Style.Add("background-color", "White");
                //    pRequired.Style.Add("display", "none");
                //    isNoError = true;
                //}
                //--------------------------------------------------------------------

                //if (string.IsNullOrEmpty(ddSupplier.SelectedValue) || string.IsNullOrEmpty(txtAttention.Text) || string.IsNullOrEmpty(txtPRNO.Text) || string.IsNullOrEmpty(txtPODate.Text) || string.IsNullOrEmpty(ddCategory.SelectedValue) || string.IsNullOrEmpty(txtDescription.Text) || string.IsNullOrEmpty(txtTypeDrawing.Text) || string.IsNullOrEmpty(txtOrderQuantity.Text) || string.IsNullOrEmpty(ddReason.SelectedValue))
                //{
                //    isNoError = false;
                //}

                if (isNoError <= 0)
                {

                    string queryDetails = string.Empty;
                    string queryHead = string.Empty;
                    string queryStatus = string.Empty;
                    string queryApprover = string.Empty;
                    string attachmentFiles = string.Empty;
                    int rd_Query_Counter = 0;
                    string query_Success = string.Empty;
                    string temp_CTRLNo = string.Empty;

                    string queryBeginPart = string.Empty;
                    string queryEndPart = string.Empty;
                    int queryHeadCounter = 0;
                    int queryStatusCounter = 0;
                    int queryHistoryUpdatesCounter = 0;
                    string queryHistoryOfUpdates = string.Empty;


                    queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";

                    if (!String.IsNullOrEmpty(Request.QueryString["CRFNo_From_Inquiry"].ToString()))
                    {
                        temp_CTRLNo = CryptorEngine.Decrypt(Request.QueryString["CRFNo_From_Inquiry"].ToString().Replace(" ", "+"), true);

                        //UPDATE HEAD
                        queryHead = "UPDATE CRF_TRANSACTION_RequestHead SET Supplier ='" + ddSupplier.SelectedValue + "', " +
                                                                           "Attention ='" + txtAttention.Text.Replace("'", "''") + "', " +
                                                                           "Category ='" + ddCategory.SelectedValue + "', " +
                                                                           "UpdatedDate = GETDATE(), " +
                                                                           "UpdatedBy ='" + Session["LcRefId"].ToString() + "' " +
                                    "WHERE CTRLNo = '" + temp_CTRLNo.Trim() + "'";

                        queryHeadCounter = 1;

                        //UPDATE HISTORY OF UPDATES
                        if (Session["CategoryName"].ToString().ToUpper() == ddCategory.SelectedItem.Text.ToUpper())
                        {
                            // do nothing if the same
                        }
                        else
                        {
                            queryHistoryUpdatesCounter = 1;
                            queryHistoryOfUpdates = "INSERT INTO HistoryOfUpdates (RFQNo, DetailsRefId, TableName, UpdatedBy, UpdatedDate, TransactionName, UpdateWhat) " +
                                                    "VALUES ('" + temp_CTRLNo + "','NA','CRF_TRANSACTION','" + Session["LcRefId"].ToString() + "',GETDATE(),'Purchasing-NotMyCategory','Category from " + Session["CategoryName"].ToString().ToUpper() + " to " + ddCategory.SelectedItem.Text.ToUpper() + "')";
                        }

                    }
                    else
                    {
                        temp_CTRLNo = setCRFNumberWithPrefix();

                        if (ddCategory.SelectedValue == "1013") //FOR MAM JEWEL OLD CATEGORY AUTOMATIC TRANSFER TO NEW CATEGORY
                        {
                            queryHead = "INSERT INTO CRF_TRANSACTION_RequestHead (CTRLNo,Supplier,Attention,Category,TransactionDate,Requester) " +
                                    "VALUES ('" + temp_CTRLNo.Trim() + "','" +
                                                  ddSupplier.SelectedValue + "','" +
                                                  txtAttention.Text.Replace("'", "''") + "','1018'," +
                                                  "GETDATE(), '" +
                                                  Session["LcRefId"].ToString() + "') ";
                        }
                        else if (ddCategory.SelectedValue == "1006") //FOR MAM JUDY OLD CATEGORY AUTOMATIC TRANSFER TO NEW CATEGORY
                        {
                            queryHead = "INSERT INTO CRF_TRANSACTION_RequestHead (CTRLNo,Supplier,Attention,Category,TransactionDate,Requester) " +
                                    "VALUES ('" + temp_CTRLNo.Trim() + "','" +
                                                  ddSupplier.SelectedValue + "','" +
                                                  txtAttention.Text.Replace("'", "''") + "','1017'," +
                                                  "GETDATE(), '" +
                                                  Session["LcRefId"].ToString() + "') ";
                        }
                        else
                        {

                            queryHead = "INSERT INTO CRF_TRANSACTION_RequestHead (CTRLNo,Supplier,Attention,Category,TransactionDate,Requester) " +
                                        "VALUES ('" + temp_CTRLNo.Trim() + "','" +
                                                      ddSupplier.SelectedValue + "','" +
                                                      txtAttention.Text.Replace("'", "''") + "','" +
                                                      ddCategory.SelectedValue + "'," +
                                                      "GETDATE(), '" +
                                                      Session["LcRefId"].ToString() + "') ";
                        }

                        queryStatus = "INSERT INTO CRF_TRANSACTION_Status (CTRLNo,Requester,DOARequester,STATRequester) " +
                                      "VALUES ('" + temp_CTRLNo.Trim() + "','" +
                                                    Session["LcRefId"].ToString() + "'," +
                                                    "GETDATE(), 1) ";

                        queryHeadCounter = 1;
                        queryStatusCounter = 1;                        

                    }

                    //UPDATE DETAILS
                    if (gvData.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvData.Rows.Count; i++)
                        {
                            Label lblRefId = (Label)gvData.Rows[i].Cells[1].FindControl("lblRefId");
                            TextBox txtPONO = (TextBox)gvData.Rows[i].Cells[1].FindControl("txtPONO");
                            TextBox txtPRNO = (TextBox)gvData.Rows[i].Cells[2].FindControl("txtPRNO");
                            TextBox txtItemName = (TextBox)gvData.Rows[i].Cells[3].FindControl("txtItemName");
                            TextBox txtSpecification = (TextBox)gvData.Rows[i].Cells[4].FindControl("txtSpecification");
                            TextBox txtQuantity = (TextBox)gvData.Rows[i].Cells[5].FindControl("txtQuantity");
                            DropDownList ddUOM = (DropDownList)gvData.Rows[i].Cells[6].FindControl("ddUOM");
                            TextBox txtPODate = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtPODate");
                            DropDownList ddReason = (DropDownList)gvData.Rows[i].Cells[8].FindControl("ddReason");
                            TextBox txtDISupplier = (TextBox)gvData.Rows[i].Cells[9].FindControl("txtDISupplier");
                            TextBox txtDIRequester = (TextBox)gvData.Rows[i].Cells[10].FindControl("txtDIRequester");
                            FileUpload fuAttachment = (FileUpload)gvData.Rows[i].Cells[11].FindControl("fuAttachment");

                            string prNO = string.Empty;
                            string poNO = string.Empty;

                            prNO = string.IsNullOrEmpty(txtPRNO.Text) ? "-" : txtPRNO.Text.Replace("'", "''");
                            poNO = string.IsNullOrEmpty(txtPONO.Text) ? "-" : txtPONO.Text.Replace("'", "''");

                            if (!string.IsNullOrEmpty(Request.QueryString["CRFNo_From_Inquiry"].ToString()))
                            {
                                

                                queryDetails += "UPDATE CRF_TRANSACTION_RequestDetails SET PODate ='" + txtPODate.Text.Replace("'", "''") + "', " +
                                                                                          "PRNo ='" + prNO + "', " +
                                                                                          "PONo ='" + poNO + "', " +
                                                                                          "Description = '" + txtItemName.Text.Replace("'", "''") + "', " +
                                                                                          "TypeDrawingNo = '" + txtSpecification.Text.Replace("'", "''") + "', " +
                                                                                          "OrderQuantity = '" + txtQuantity.Text.Replace("'", "''") + "', " +
                                                                                          "UnitOfMeasure = '" + ddUOM.SelectedValue + "', " +
                                                                                          "Reason = '" + ddReason.SelectedValue + "', " +
                                                                                          "DateInformedSupplier = '" + txtDISupplier.Text.Replace("'", "''") + "', " +
                                                                                          "DateInformedRequester = '" + txtDIRequester.Text.Replace("'", "''") + "' " +
                                                "WHERE CTRLNo = '" + temp_CTRLNo.Trim() + "' AND Refid = '" + lblRefId.Text.Trim() + "' ";
                            }
                            else
                            {
                                string fileNameApplication = System.IO.Path.GetFileName(fuAttachment.FileName);
                                string fileExtensionApplication = System.IO.Path.GetExtension(fileNameApplication);
                                string newFile = fileNameApplication;

                                if (fuAttachment.HasFile)
                                {

                                    if (!System.IO.Directory.Exists(Server.MapPath("~/CRF_Request/" + temp_CTRLNo.Trim())))
                                    {
                                        System.IO.Directory.CreateDirectory(Server.MapPath("~/CRF_Request/" + temp_CTRLNo.Trim()));
                                    }
                                    if (!System.IO.File.Exists(Server.MapPath("~/CRF_Request/" + temp_CTRLNo.Trim() + "/" + newFile)))
                                    {
                                        fuAttachment.SaveAs(System.IO.Path.Combine(Server.MapPath("~/CRF_Request/" + temp_CTRLNo.Trim()), newFile));
                                        fuAttachment.Dispose();
                                        System.IO.File.Copy(System.IO.Path.Combine(Server.MapPath("~/CRF_Request/" + temp_CTRLNo.Trim()), newFile), System.IO.Path.Combine(Server.MapPath("~/CRF_Request/" + temp_CTRLNo.Trim()), (i.ToString() + "-" + temp_CTRLNo.Trim() + fileExtensionApplication)), true);
                                        System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath("~/CRF_Request/" + temp_CTRLNo.Trim()), newFile));
                                    }

                                    attachmentFiles = i.ToString() + "-" + temp_CTRLNo.Trim() + fileExtensionApplication;
                                }
                                else
                                {
                                    if (!System.IO.Directory.Exists(Server.MapPath("~/CRF_Request/" + temp_CTRLNo.Trim())))
                                    {
                                        System.IO.Directory.CreateDirectory(Server.MapPath("~/CRF_Request/" + temp_CTRLNo.Trim()));
                                    }
                                }

                                queryDetails += "INSERT INTO CRF_TRANSACTION_RequestDetails (CTRLNo,PODate,PRNo,PONo,Description,TypeDrawingNo,OrderQuantity,UnitOfMeasure,Reason,DateInformedSupplier,DateInformedRequester,Attachment1) " +
                                                "VALUES ('" + temp_CTRLNo.Trim() + "','" +
                                                              txtPODate.Text.Replace("'", "''") + "','" +
                                                              prNO + "','" +
                                                              poNO + "','" +
                                                              txtItemName.Text.Replace("'", "''") + "','" +
                                                              txtSpecification.Text.Replace("'", "''") + "','" +
                                                              txtQuantity.Text.Replace("'", "''") + "','" +
                                                              ddUOM.SelectedValue + "','" +
                                                              ddReason.SelectedValue + "','" +
                                                              txtDISupplier.Text.Replace("'", "''") + "','" +
                                                              txtDIRequester.Text.Replace("'", "''") + "','" +
                                                              attachmentFiles + "') ";

                            }

                            rd_Query_Counter++;

                        }
                    }

                    queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";

                    query_Success = BLL.CRF_TRANSACTION_SQLTransaction(queryBeginPart + queryHead + queryStatus + queryHistoryOfUpdates + queryDetails + queryEndPart).ToString();

                    if (query_Success == (rd_Query_Counter + queryHeadCounter + queryHistoryUpdatesCounter + queryStatusCounter).ToString())
                    {
                        Session["successMessage"] = "CTRL NUMBER : <b>" + temp_CTRLNo + "</b> HAS BEEN SUCCESSFULLY SAVED.";
                        Session["successTransactionName"] = "CRF_REQUESTENTRY";
                        Session["successReturnPage"] = "CRF_RequestEntry.aspx?CRFNo_From_Inquiry=";

                        Response.Redirect("SuccessPage.aspx");
                    }


                        //// CREATE THE INITIAL DIRECTORY
                        //if (!System.IO.Directory.Exists(Server.MapPath("~/CRF_Request/" + temp_CTRLNo)))
                        //{
                        //    System.IO.Directory.CreateDirectory(Server.MapPath("~/CRF_Request/" + temp_CTRLNo));
                        //}

                        
                        //if (!string.IsNullOrEmpty(success))
                        //{
                        //    if (int.Parse(success) >= 2)
                        //    {
                        //        Session["successMessage"] = "CTRL NUMBER : <b>" + temp_CTRLNo + "</b> HAS BEEN SUCCESSFULLY SAVED.";
                        //        Session["successTransactionName"] = "CRF_REQUESTENTRY";
                        //        Session["successReturnPage"] = "CRF_RequestEntry.aspx?CRFNo_From_Inquiry=";

                        //        Response.Redirect("SuccessPage.aspx");
                        //    }


                        //}



                    
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }

        }

        private string setCRFNumberWithPrefix()
        {
            string retVal = string.Empty;

            retVal = "CRF_" + Session["DivisionCode"].ToString() + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + COMMON.controlNoZeroPrefix(setControlNumber().ToString().Length.ToString()) + setControlNumber().ToString();

            return retVal;
        }

        private Int32 setControlNumber()
        {
            return BLL.CRF_TRANSACTION_CountRequestHead(DateTime.Now.Year.ToString()) + 1;
        }

        protected void btnSupplierAttachment_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = string.Empty;
                string filenameExtension = string.Empty;
                string extension = string.Empty;

                //if (string.IsNullOrEmpty(ddSupplierAttachment.SelectedItem.Text))
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select a valid file to download.');", true);
                //}
                //else
                //{
                //    filename = ddSupplierAttachment.SelectedItem.Text;
                //    filenameExtension = ddSupplierAttachment.SelectedItem.Text.Remove(0, (ddSupplierAttachment.SelectedItem.Text.IndexOf(".") + 1));

                //    if (filenameExtension.ToLower() == "xlsx")
                //    {
                //        extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //    }
                //    if (filenameExtension.ToLower() == "pdf")
                //    {
                //        extension = "application/pdf";
                //    }

                //    Response.ContentType = extension;
                //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                //    Response.TransmitFile(Server.MapPath("~/CRF_Received/" + displayCTRLNo + "/" + filename));
                //    Response.End();
                //}

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        private void AddNewRow()
        {
            try
            {

                int rowIndex = 0;

                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    DataRow drCurrentRow = null;
                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            TextBox txtPONO =
                              (TextBox)gvData.Rows[rowIndex].Cells[1].FindControl("txtPONO");
                            TextBox txtPRNO =
                              (TextBox)gvData.Rows[rowIndex].Cells[2].FindControl("txtPRNO");
                            TextBox txtItemName =
                              (TextBox)gvData.Rows[rowIndex].Cells[3].FindControl("txtItemName");
                            TextBox txtSpecification =
                              (TextBox)gvData.Rows[rowIndex].Cells[4].FindControl("txtSpecification");
                            TextBox txtQuantity =
                              (TextBox)gvData.Rows[rowIndex].Cells[5].FindControl("txtQuantity");
                            DropDownList ddUOM =
                              (DropDownList)gvData.Rows[rowIndex].Cells[6].FindControl("ddUOM");
                            TextBox txtPODate =
                              (TextBox)gvData.Rows[rowIndex].Cells[7].FindControl("txtPODate");
                            DropDownList ddReason =
                              (DropDownList)gvData.Rows[rowIndex].Cells[8].FindControl("ddReason");
                            TextBox txtDISupplier =
                              (TextBox)gvData.Rows[rowIndex].Cells[9].FindControl("txtDISupplier");
                            TextBox txtDIRequester =
                              (TextBox)gvData.Rows[rowIndex].Cells[10].FindControl("txtDIRequester");

                            //---------------------------------------------------------------------------------------------------

                            List<Entities_CRF_RequestEntry> listDropDown = new List<Entities_CRF_RequestEntry>();
                            listDropDown = BLL.CRF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                            if (listDropDown != null)
                            {
                                if (listDropDown.Count > 0)
                                {
                                    ddReason.Items.Add("");
                                    ddUOM.Items.Add("");

                                    foreach (Entities_CRF_RequestEntry entity in listDropDown)
                                    {
                                        ListItem item = new ListItem();
                                        item.Text = entity.DropdownName.ToUpper();
                                        item.Value = entity.DropdownRefId;

                                        if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                                        {
                                            if (entity.TableName == "CRF_MT_Reason")
                                            {
                                                ddReason.Items.Add(item);
                                            }
                                            if (entity.TableName == "MT_UnitOfMeasure")
                                            {
                                                ddUOM.Items.Add(item);
                                            }
                                        }

                                    }

                                }
                            }

                            //---------------------------------------------------------------------------------------------------

                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["RowNumber"] = i + 1;

                            dtCurrentTable.Rows[i - 1]["RdRefId"] = string.Empty;
                            dtCurrentTable.Rows[i - 1]["RdPONO"] = txtPONO.Text;
                            dtCurrentTable.Rows[i - 1]["RdPRNO"] = txtPRNO.Text;
                            dtCurrentTable.Rows[i - 1]["RdItemName"] = txtItemName.Text;
                            dtCurrentTable.Rows[i - 1]["RdSpecs"] = txtSpecification.Text;
                            dtCurrentTable.Rows[i - 1]["RdQuantity"] = txtQuantity.Text;
                            dtCurrentTable.Rows[i - 1]["RdUnitOfMeasure"] = ddUOM.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["RdPODate"] = txtPODate.Text;
                            dtCurrentTable.Rows[i - 1]["RdReason"] = ddReason.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["RdDateInformedSupplier"] = txtDISupplier.Text;
                            dtCurrentTable.Rows[i - 1]["RdDateInformedRequester"] = txtDIRequester.Text;
                            rowIndex++;
                        }

                        dtCurrentTable.Rows.Add(drCurrentRow);
                        ViewState["CurrentTable"] = dtCurrentTable;

                        gvData.DataSource = dtCurrentTable;
                        gvData.DataBind();

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('ViewState is null');", true);
                }
                SetPreviousData();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.StackTrace.ToString() + "');", true);
            }
        }

        private void SetRowData()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtPONO =
                              (TextBox)gvData.Rows[rowIndex].Cells[1].FindControl("txtPONO");
                        TextBox txtPRNO =
                          (TextBox)gvData.Rows[rowIndex].Cells[2].FindControl("txtPRNO");
                        TextBox txtItemName =
                          (TextBox)gvData.Rows[rowIndex].Cells[3].FindControl("txtItemName");
                        TextBox txtSpecification =
                          (TextBox)gvData.Rows[rowIndex].Cells[4].FindControl("txtSpecification");
                        TextBox txtQuantity =
                          (TextBox)gvData.Rows[rowIndex].Cells[5].FindControl("txtQuantity");
                        DropDownList ddUOM =
                          (DropDownList)gvData.Rows[rowIndex].Cells[6].FindControl("ddUOM");
                        TextBox txtPODate =
                          (TextBox)gvData.Rows[rowIndex].Cells[7].FindControl("txtPODate");
                        DropDownList ddReason =
                              (DropDownList)gvData.Rows[rowIndex].Cells[8].FindControl("ddReason");
                        TextBox txtDISupplier =
                          (TextBox)gvData.Rows[rowIndex].Cells[9].FindControl("txtDISupplier");
                        TextBox txtDIRequester =
                          (TextBox)gvData.Rows[rowIndex].Cells[10].FindControl("txtDIRequester");


                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["RdRefId"] = string.Empty;
                        dtCurrentTable.Rows[i - 1]["RdPONO"] = txtPONO.Text;
                        dtCurrentTable.Rows[i - 1]["RdPRNO"] = txtPRNO.Text;
                        dtCurrentTable.Rows[i - 1]["RdItemName"] = txtItemName.Text;
                        dtCurrentTable.Rows[i - 1]["RdSpecs"] = txtSpecification.Text;
                        dtCurrentTable.Rows[i - 1]["RdQuantity"] = txtQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["RdUnitOfMeasure"] = ddUOM.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["RdPODate"] = txtPODate.Text;
                        dtCurrentTable.Rows[i - 1]["RdReason"] = ddReason.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["RdDateInformedSupplier"] = txtDISupplier.Text;
                        dtCurrentTable.Rows[i - 1]["RdDateInformedRequester"] = txtDIRequester.Text;
                        rowIndex++;

                    }

                    ViewState["CurrentTable"] = dtCurrentTable;
                    //gvData.DataSource = dtCurrentTable;
                    //gvData.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            //SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtPONO =
                              (TextBox)gvData.Rows[rowIndex].Cells[1].FindControl("txtPONO");
                        TextBox txtPRNO =
                          (TextBox)gvData.Rows[rowIndex].Cells[2].FindControl("txtPRNO");
                        TextBox txtItemName =
                          (TextBox)gvData.Rows[rowIndex].Cells[3].FindControl("txtItemName");
                        TextBox txtSpecification =
                          (TextBox)gvData.Rows[rowIndex].Cells[4].FindControl("txtSpecification");
                        TextBox txtQuantity =
                          (TextBox)gvData.Rows[rowIndex].Cells[5].FindControl("txtQuantity");
                        DropDownList ddUOM =
                          (DropDownList)gvData.Rows[rowIndex].Cells[6].FindControl("ddUOM");
                        TextBox txtPODate =
                          (TextBox)gvData.Rows[rowIndex].Cells[7].FindControl("txtPODate");
                        DropDownList ddReason =
                          (DropDownList)gvData.Rows[rowIndex].Cells[8].FindControl("ddReason");
                        TextBox txtDISupplier =
                          (TextBox)gvData.Rows[rowIndex].Cells[9].FindControl("txtDISupplier");
                        TextBox txtDIRequester =
                          (TextBox)gvData.Rows[rowIndex].Cells[10].FindControl("txtDIRequester");


                        txtPONO.Text = dt.Rows[i]["RdPONO"].ToString();
                        txtPRNO.Text = dt.Rows[i]["RdPRNO"].ToString();
                        txtItemName.Text = dt.Rows[i]["RdItemName"].ToString();
                        txtSpecification.Text = dt.Rows[i]["RdSpecs"].ToString();
                        txtQuantity.Text = dt.Rows[i]["RdQuantity"].ToString();
                        ddUOM.Items.FindByValue(dt.Rows[i]["RdUnitOfMeasure"].ToString()).Selected = true;
                        txtPODate.Text = dt.Rows[i]["RdPODate"].ToString();
                        ddReason.Items.FindByValue(dt.Rows[i]["RdReason"].ToString()).Selected = true;
                        txtDISupplier.Text = dt.Rows[i]["RdDateInformedSupplier"].ToString();
                        txtDIRequester.Text = dt.Rows[i]["RdDateInformedRequester"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (gvData.Rows.Count <= 9)
            {
                AddNewRow();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('You can only add up to 10 items.');", true);
            }

        }

        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblUOM = (Label)e.Row.FindControl("lblUOM");
                    Label lblReason = (Label)e.Row.FindControl("lblReason");
                    DropDownList ddUOM = (DropDownList)e.Row.FindControl("ddUOM");
                    DropDownList ddReason = (DropDownList)e.Row.FindControl("ddReason");


                    //---------------------------------------------------------------------------------------------------

                    List<Entities_CRF_RequestEntry> listDropDown = new List<Entities_CRF_RequestEntry>();
                    listDropDown = BLL.CRF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                    if (listDropDown != null)
                    {
                        if (listDropDown.Count > 0)
                        {
                            ddReason.Items.Add("");
                            ddUOM.Items.Add("");

                            foreach (Entities_CRF_RequestEntry entity in listDropDown)
                            {
                                ListItem item = new ListItem();
                                item.Text = entity.DropdownName.ToUpper();
                                item.Value = entity.DropdownRefId;

                                if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                                {
                                    if (entity.TableName == "CRF_MT_Reason")
                                    {
                                        ddReason.Items.Add(item);
                                    }
                                    if (entity.TableName == "MT_UnitOfMeasure")
                                    {
                                        ddUOM.Items.Add(item);
                                    }
                                }

                            }

                            if (!String.IsNullOrEmpty(Request.QueryString["CRFNo_From_Inquiry"].ToString()))
                            {
                                ddUOM.Items.FindByValue(lblUOM.Text.Trim()).Selected = true;
                                ddReason.Items.FindByValue(lblReason.Text.Trim()).Selected = true;
                            }

                        }
                    }

                    //---------------------------------------------------------------------------------------------------

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                SetRowData();
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    DataRow drCurrentRow = null;
                    int rowIndex = Convert.ToInt32(e.RowIndex);
                    if (dt.Rows.Count > 1)
                    {
                        dt.Rows.Remove(dt.Rows[rowIndex]);
                        drCurrentRow = dt.NewRow();
                        ViewState["CurrentTable"] = dt;
                        gvData.DataSource = dt;
                        gvData.DataBind();

                        for (int i = 0; i < gvData.Rows.Count - 1; i++)
                        {
                            gvData.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                        }
                        SetPreviousData();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.StackTrace.ToString() + "');", true);
            }
        }

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                TextBox txtPONO = row.FindControl("txtPONO") as TextBox;
                TextBox txtPRNO = row.FindControl("txtPRNO") as TextBox;
                TextBox txtItemName = row.FindControl("txtItemName") as TextBox;
                TextBox txtSpecification = row.FindControl("txtSpecification") as TextBox;

                GridView gvDataLinkToPR = row.FindControl("gvDataLinkToPR") as GridView;
                HtmlGenericControl divDataLinkToPR = row.FindControl("divDataLinkToPR") as HtmlGenericControl;


                if (e.CommandName == "btnPONO_Command")
                {
                    if (string.IsNullOrEmpty(txtPONO.Text))
                    {
                        divDataLinkToPR.Style.Add("display", "none");
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Search item must not be blank.');", true);
                    }
                    else
                    {
                        List<Entities_CRF_RequestEntry> listDataLink = new List<Entities_CRF_RequestEntry>();
                        listDataLink = BLL.vewRFQDelivery("SELECT TOP 10 * FROM [ROPROS].[dbo].[vewRFQDelivery] WITH (NOLOCK) WHERE PONO LIKE '%" + txtPONO.Text + "%'", index.ToString());
                        //listDataLink = BLL.vewRFQDelivery("SELECT TOP 10 * FROM v_vewRFQDelivery WITH (NOLOCK) WHERE PONO LIKE '%" + txtPONO.Text + "%'", index.ToString());


                        if (listDataLink != null)
                        {
                            if (listDataLink.Count > 0)
                            {
                                gvDataLinkToPR.DataSource = listDataLink;
                                gvDataLinkToPR.DataBind();

                                divDataLinkToPR.Style.Add("display", "block");
                            }
                            else
                            {
                                divDataLinkToPR.Style.Add("display", "none");
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('No PONO (" + txtPONO.Text + ") found in the data collection.');", true);
                            }
                        }
                        else
                        {
                            divDataLinkToPR.Style.Add("display", "none");
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('No PONO (" + txtPONO.Text + ") found in the data collection.');", true);
                        }                        

                    }
                }

                if (e.CommandName == "btnPRNO_Command")
                {
                    if (string.IsNullOrEmpty(txtPRNO.Text))
                    {
                        divDataLinkToPR.Style.Add("display", "none");
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Search item must not be blank.');", true);
                    }
                    else
                    {
                        List<Entities_CRF_RequestEntry> listDataLink = new List<Entities_CRF_RequestEntry>();
                        listDataLink = BLL.vewRFQDelivery("SELECT TOP 10 * FROM [ROPROS].[dbo].[vewRFQDelivery] WITH (NOLOCK) WHERE PRNO LIKE '%" + txtPRNO.Text + "%'", index.ToString());

                        if (listDataLink != null)
                        {
                            if (listDataLink.Count > 0)
                            {
                                gvDataLinkToPR.DataSource = listDataLink;
                                gvDataLinkToPR.DataBind();

                                divDataLinkToPR.Style.Add("display", "block");
                            }
                            else
                            {
                                divDataLinkToPR.Style.Add("display", "none");
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('No PRNO (" + txtPRNO.Text + ") found in the data collection.');", true);
                            }
                        }
                        else
                        {
                            divDataLinkToPR.Style.Add("display", "none");
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('No PRNO (" + txtPRNO.Text + ") found in the data collection.');", true);
                        }
                        


                    }
                }

                if (e.CommandName == "btnItemName_Command")
                {
                    if (string.IsNullOrEmpty(txtItemName.Text))
                    {
                        divDataLinkToPR.Style.Add("display", "none");
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Search item must not be blank.');", true);
                    }
                    else
                    {
                        List<Entities_CRF_RequestEntry> listDataLink = new List<Entities_CRF_RequestEntry>();
                        listDataLink = BLL.vewRFQDelivery("SELECT TOP 10 * FROM [ROPROS].[dbo].[vewRFQDelivery] WITH (NOLOCK) WHERE ITEM LIKE '%" + txtItemName.Text + "%'", index.ToString());

                        if (listDataLink != null)
                        {
                            if (listDataLink.Count > 0)
                            {
                                gvDataLinkToPR.DataSource = listDataLink;
                                gvDataLinkToPR.DataBind();

                                divDataLinkToPR.Style.Add("display", "block");
                            }
                            else
                            {
                                divDataLinkToPR.Style.Add("display", "none");
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('No ItemName (" + txtItemName.Text + ") found in the data collection.');", true);
                            }
                        }
                        else
                        {
                            divDataLinkToPR.Style.Add("display", "none");
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('No ItemName (" + txtItemName.Text + ") found in the data collection.');", true);
                        }
                       

                    }
                }

                if (e.CommandName == "btnSpecification_Command")
                {
                    if (string.IsNullOrEmpty(txtSpecification.Text))
                    {
                        divDataLinkToPR.Style.Add("display", "none");
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Search item must not be blank.');", true);
                    }
                    else
                    {
                        List<Entities_CRF_RequestEntry> listDataLink = new List<Entities_CRF_RequestEntry>();
                        listDataLink = BLL.vewRFQDelivery("SELECT TOP 10 * FROM [ROPROS].[dbo].[vewRFQDelivery] WITH (NOLOCK) WHERE Specification LIKE '%" + txtSpecification.Text + "%'", index.ToString());

                        if (listDataLink != null)
                        {
                            if (listDataLink.Count > 0)
                            {
                                gvDataLinkToPR.DataSource = listDataLink;
                                gvDataLinkToPR.DataBind();

                                divDataLinkToPR.Style.Add("display", "block");
                            }
                            else
                            {
                                divDataLinkToPR.Style.Add("display", "none");
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('No Specification (" + txtSpecification.Text + ") found in the data collection.');", true);
                            }
                        }
                        else
                        {
                            divDataLinkToPR.Style.Add("display", "none");
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('No Specification (" + txtSpecification.Text + ") found in the data collection.');", true);
                        }
                        

                    }
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        


    }
}
