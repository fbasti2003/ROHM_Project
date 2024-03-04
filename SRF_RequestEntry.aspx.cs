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
//using REPI_PUR_SOFRA.App_Code.ENTITIES;
//using REPI_PUR_SOFRA.App_Code.BLL;
using System.Collections.Generic;
using System.IO;


namespace REPI_PUR_SOFRA
{
    public partial class SRF_RequestEntry : System.Web.UI.Page
    {

        BLL_SRF BLL = new BLL_SRF();
        Common COMMON = new Common();

        private string ddumitems;
        public string Ddumitems { get { return ddumitems; } set { ddumitems = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                

                //--------------------------------------------------------------------------------------------------
                SRF_TRANSACTION_RequestEntry_Fill_All_DropdownList();
                //--------------------------------------------------------------------------------------------------
                var temp_access = ConfigurationManager.AppSettings["SRF_REQUEST_ENTRY_LOA_ALLOWED"];
                var elements = temp_access.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

                foreach (string items in elements)
                {
                    if (items.ToLower() == Session["Username"].ToString().ToLower())
                    {
                        ddLoaNo.Enabled = true;
                        txtSuretyBond.Enabled = true;
                        txtLoa8106.Enabled = true;
                        txtTotalValueInUsd.Enabled = true;                        
                    }
                }                
                //--------------------------------------------------------------------------------------------------

                if (Session["SRF_Error_Entry"] != null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('" + Session["SRF_Error_Entry"].ToString() + "')", true);
                }

                //--------------------------------------------------------------------------------------------------
                if (Request.QueryString["SRF_ControlNo_From_Details"].ToString().Length > 0)
                {
                    fu1.Visible = false;
                    fu2.Visible = false;
                    fu3.Visible = false;

                    string controlNo_From_Details = CryptorEngine.Decrypt(Request.QueryString["SRF_ControlNo_From_Details"].ToString().Replace(" ", "+"), true);

                    Set_Existing_Item_For_Update(controlNo_From_Details);
                    SRF_CTRLNo.InnerHtml = "SERVICE REPAIR REQUEST UPDATE (" + controlNo_From_Details + ")";
                    this.Title = controlNo_From_Details;

                    if (ddLoaNo.Text.Length > 0)
                    {
                        btnCancelTransaction.Visible = true;
                    }
                }
                else
                {
                    txtRequester.Text = Session["UserFullName"].ToString().ToUpper();
                    txtDivisionName.Text = Session["DivisionName"].ToString().ToUpper();
                    txtDepartment.Text = Session["DepartmentName"].ToString().ToUpper();
                    txtLocalNumber.Text = Session["LocalNumber"].ToString().ToUpper();
                    txtRequester.Enabled = false;
                    txtDivisionName.Enabled = false;
                    txtDepartment.Enabled = false;
                    txtLocalNumber.Enabled = false;

                    Ddumitems = "<asp:ListItem Text='NO' Value='0'></asp:ListItem>";
                    FirstGridViewRow();

                    fu1.Visible = true;
                    fu2.Visible = true;
                    fu3.Visible = true;
                }

                foreach (string items in elements)
                {
                    if (items.ToLower() == Session["Username"].ToString().ToLower())
                    {
                        btnCancelTransaction.Visible = true;
                    }
                    else
                    {
                        btnCancelTransaction.Visible = false;
                    }
                }      

            }
        }


        private void Set_Existing_Item_For_Update(string ctrno)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
            list = BLL.SRF_TRANSACTION_RequestEntry_ByControlNo(ctrno);

            if (list.Count > 0)
            {
                foreach (Entities_SRF_RequestEntry entity in list)
                {
                    Session["SRF_OLD_CATEGORY_DESCRIPTION"] = entity.CategoryDescription;
                    Session["SRF_OLD_CATEGORY_VALUE"] = entity.Category;
                    ddCategory.SelectedValue = entity.Category;
                    ddSupplier.SelectedValue = entity.Supplier;                    
                    txtTotalQuantity.Text = entity.TotalQuantity;
                    txtServiceDate.Text = entity.PullOutServiceDate;
                    txtDeliveryDateToRepi.Text = entity.DeliveryDateToRepi;
                    txtProblemEncountered.Text = entity.ProblemEncountered;
                    ddPurposeOfPullOut.SelectedValue = entity.PurposeOfPullOut;
                    txtTotalValueInUsd.Text = entity.TotalValueInUsd;
                    ddLoaNo.SelectedValue = entity.LoaNo;
                    txtSuretyBond.Text = entity.LoaSuretyBond;
                    txtLoa8106.Text = entity.Loa8106;
                    txtRemarks.Text = entity.Remarks;
                    txtGatePassNo.Text = entity.GatePassNo;
                    txtPickUpPoint.Text = entity.PickUpPoint;

                    hfTotalQuantity.Value = entity.TotalQuantity;
                    hfLOANumber.Value = entity.LoaNo;
                    hfCTRLNo.Value = ctrno;

                    if (entity.Document8105 == "1")
                    {
                        chkNoNeed8105.Checked = true;
                    }

                    BLL_Common BLL_Common = new BLL_Common();
                    List<Entities_Common_SystemUsers> requester = new List<Entities_Common_SystemUsers>();
                    requester = BLL_Common.getLoginCredentialsByRefId(entity.Requester);

                    if (requester.Count > 0)
                    {
                        foreach (Entities_Common_SystemUsers user in requester)
                        {
                            txtRequester.Text = CryptorEngine.Decrypt(user.FullName, true).ToUpper();
                            txtDivisionName.Text = user.DivisionName.ToUpper();
                            txtDepartment.Text = user.DepartmentName.ToUpper();
                            txtLocalNumber.Text = user.LocalNumber;

                            txtRequester.Enabled = false;
                            txtDivisionName.Enabled = false;
                            txtDepartment.Enabled = false;
                            txtLocalNumber.Enabled = false;
                        }
                    }

                    if (!string.IsNullOrEmpty(entity.Attachment))
                    {
                        if (int.Parse(entity.Attachment) > 0)
                        {
                            if (entity.Attachment == "1")
                            {
                                linkAttachment1.Text = "1-" + ctrno;
                                linkAttachment2.Text = "&nbsp;";
                                linkAttachment3.Text = "&nbsp;";
                            }
                            if (entity.Attachment == "2")
                            {
                                linkAttachment1.Text = "1-" + ctrno;
                                linkAttachment2.Text = "2-" + ctrno;
                                linkAttachment3.Text = "&nbsp;";
                            }
                            if (entity.Attachment == "3")
                            {
                                linkAttachment1.Text = "1-" + ctrno;
                                linkAttachment2.Text = "2-" + ctrno;
                                linkAttachment3.Text = "3-" + ctrno;
                            }
                        }
                    }

                    // SET DETAILS
                    List<Entities_SRF_RequestEntry> details = new List<Entities_SRF_RequestEntry>();
                    details = BLL.SRF_TRANSACTION_RequestEntry_Details_ByControlNo(entity);

                    if (details.Count > 0)
                    {
                        gvData.Visible = false;
                        gvDataUpdate.Visible = true;
                        gvDataUpdate.DataSource = details;
                        gvDataUpdate.DataBind();
                        
                    }
                    
                    // PULLOUT

                    if (entity.ProblemEncountered.ToUpper() == "RE-USE CONTAINER TUBES" || entity.ProblemEncountered.ToUpper() == "RE-USE IC TRAYS")
                    {
                        List<Entities_SRF_PO_Entry> listPullOut = new List<Entities_SRF_PO_Entry>();
                        listPullOut = BLL.SRF_TRANSACTION_PO_TOP_10_REQUEST(entity.TransactionDate, txtProblemEncountered.Text.Replace("RE-USE","").Trim(), ctrno.Trim());

                        if (listPullOut != null)
                        {
                            if (listPullOut.Count > 0)
                            {
                                gvPullOut.Visible = true;
                                gvPullOut.DataSource = listPullOut;
                                gvPullOut.DataBind();

                                //RE-COMPUTE TOTAL QTY. & REMARKS NUMBER OF BOXES TO PREVENT COMPUTATION PROBLEM ENCOUNTERED IN SRF-PO-APPROVAL
                                int srfTotalQty = 0;
                                int srfRemarksQty = 0;

                                foreach (Entities_SRF_PO_Entry entityPullOut in listPullOut)
                                {
                                    srfTotalQty = srfTotalQty + int.Parse(entityPullOut.Head_TotalQuantity);
                                    srfRemarksQty = srfRemarksQty + int.Parse(entityPullOut.Head_TotalBoxes);
                                }

                                if (txtTotalQuantity.Text.Replace(",", "") == srfTotalQty.ToString() && txtRemarks.Text.Replace("BOXES", "").Replace(" ", "") == srfRemarksQty.ToString())
                                {
                                    //DO NOTHING
                                }
                                else
                                {

                                    string queryBeginPart_PullOut = "BEGIN TRY BEGIN TRANSACTION ";
                                    string queryEndPart_PullOut = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                                    string query1_PullOut = string.Empty;
                                    string querySuccess_PullOut = string.Empty;

                                    query1_PullOut = "UPDATE SRF_TRANSACTION_Request SET TotalQuantity = '" + srfTotalQty.ToString() + "', Remarks = '" + srfRemarksQty.ToString() + " BOXES' WHERE CTRLNo = '" + ctrno.Trim() + "'";

                                    querySuccess_PullOut = BLL.SRF_TRANSACTION_SQLTransaction(queryBeginPart_PullOut + query1_PullOut + queryEndPart_PullOut).ToString();

                                    txtTotalQuantity.Text = srfTotalQty.ToString();
                                    txtRemarks.Text = srfRemarksQty.ToString() + " BOXES";

                                }


                            }

                        }


                    }


                    //TRANSFER DETAILS
                    BLL_RFQ BLLRFQ = new BLL_RFQ();

                    List<Entities_RFQ_RequestEntry> List_TransferHistory = new List<Entities_RFQ_RequestEntry>();
                    Entities_RFQ_RequestEntry entityTransferHistory = new Entities_RFQ_RequestEntry();
                    entityTransferHistory.SearchCriteria = ctrno;
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


            }


            // CHECK IF ITEM IS ALREADY APPROVED THEN SUBMIT BUTTON MUST NOT BE ALLOWED
            List<Entities_SRF_RequestEntry> approved = new List<Entities_SRF_RequestEntry>();
            approved = BLL.SRF_TRANSACTION_RequestStatus_ByControlNo(ctrno);

            if (approved.Count > 0)
            {
                divApprover.Visible = true;

                foreach (Entities_SRF_RequestEntry entity in approved)
                {
                    int purImpex = 0;
                    int purIncharge = 0;

                    if (!string.IsNullOrEmpty(entity.PurImpexStat))
                    {
                        purImpex = int.Parse(entity.PurImpexStat);
                    }
                    if (!string.IsNullOrEmpty(entity.PurInchargeStat))
                    {
                        purIncharge = int.Parse(entity.PurInchargeStat);
                    }

                    if (purImpex > 0 || purIncharge > 0)
                    {
                        //var PIPL_Impex_Access = ConfigurationManager.AppSettings["PIPL_Temp_Sir_Renz"];
                        var PIPL_Impex_Access = "5057";

                        if (Session["Username"].ToString() == PIPL_Impex_Access.ToString() || Session["Username"].ToString() == "01086")
                        {
                            btnSubmit.Visible = true;
                        }
                        else
                        {
                            btnSubmit.Visible = false;
                        }
                    }

                    lblRequestor.Text = !string.IsNullOrEmpty(entity.ReqInchargeName) ? CryptorEngine.Decrypt(entity.ReqInchargeName, true) : "PENDING";
                    lblManager.Text = !string.IsNullOrEmpty(entity.ReqManagerName) ? CryptorEngine.Decrypt(entity.ReqManagerName, true) : "PENDING";
                    lblIncharge.Text = !string.IsNullOrEmpty(entity.PurInchargeName) ? CryptorEngine.Decrypt(entity.PurInchargeName, true) : "PENDING";
                    lblImpex.Text = !string.IsNullOrEmpty(entity.PurManagerName) ? CryptorEngine.Decrypt(entity.PurManagerName, true) : "PENDING";
                    
                    
                    lblSCDDeptManager.Text = !string.IsNullOrEmpty(entity.PurDeptManagerName) ? CryptorEngine.Decrypt(entity.PurDeptManagerName, true) : "PENDING";
                    lblDOASCDDeptManager.Text = !string.IsNullOrEmpty(entity.PurDeptManagerName) ? entity.PurDeptManagerDOA : "-";

                    lblDOARequestor.Text = !string.IsNullOrEmpty(entity.ReqInchargeName) ? entity.ReqInchargeDOA : "-";
                    lblDOAManager.Text = !string.IsNullOrEmpty(entity.ReqManagerName) ? entity.ReqManagerDOA : "-";
                    lblDOAIncharge.Text = !string.IsNullOrEmpty(entity.PurInchargeName) ? entity.PurInchargeDOA : "-";
                    lblDOAImpex.Text = !string.IsNullOrEmpty(entity.PurManagerName) ? entity.PurImpexDOA : "-";
                    

                    // REQUESTOR
                    if (entity.ReqInchargeStat == "0")
                    {
                        lblRequestor.CssClass = "label label-danger";
                    }
                    if (entity.ReqInchargeStat == "1")
                    {
                        lblRequestor.CssClass = "label label-success";
                    }
                    if (entity.ReqInchargeStat == "2")
                    {
                        lblRequestor.CssClass = "label label-warning";
                    }

                    // MANAGER
                    if (entity.ReqManagerStat == "0")
                    {
                        lblManager.CssClass = "label label-danger";
                    }
                    if (entity.ReqManagerStat == "1")
                    {
                        lblManager.CssClass = "label label-success";
                    }
                    if (entity.ReqManagerStat == "2")
                    {
                        lblManager.CssClass = "label label-warning";
                    }

                    // INCHARGE
                    if (entity.PurInchargeStat == "0")
                    {
                        lblIncharge.CssClass = "label label-danger";
                    }
                    if (entity.PurInchargeStat == "1")
                    {
                        lblIncharge.CssClass = "label label-success";
                    }
                    if (entity.PurInchargeStat == "2")
                    {
                        lblIncharge.CssClass = "label label-warning";
                    }

                    // SCD DEPT. MANAGER
                    if (entity.PurDeptManagerStat == "0")
                    {
                        lblSCDDeptManager.CssClass = "label label-danger";
                    }
                    if (entity.PurDeptManagerStat == "1")
                    {
                        lblSCDDeptManager.CssClass = "label label-success";
                    }
                    if (entity.PurDeptManagerStat == "2")
                    {
                        lblSCDDeptManager.CssClass = "label label-warning";
                    }

                    // IMPEX
                    if (entity.PurImpexStat == "0")
                    {
                        lblImpex.CssClass = "label label-danger";
                    }
                    if (entity.PurImpexStat == "1")
                    {
                        lblImpex.CssClass = "label label-success";
                    }
                    if (entity.PurImpexStat == "2")
                    {
                        lblImpex.CssClass = "label label-warning";
                    }
                    if (entity.PurImpexStat == "3")
                    {
                        lblImpex.Text = lblImpex.Text + " (CANCELED)";
                        lblImpex.Style.Add("background-color", "blue");
                    }

                }
            }

        }

        private void SRF_TRANSACTION_RequestEntry_Fill_All_DropdownList()
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
            list = BLL.SRF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(item => item.DropdownName).ToList();

            ddSupplier.Items.Add("");
            ddPurposeOfPullOut.Items.Add("");
            ddLoaNo.Items.Add("");
            ddCategory.Items.Add("");


            foreach (Entities_SRF_RequestEntry entity in list)
            {
                ListItem item = new ListItem();
                item.Text = entity.DropdownName;
                item.Value = entity.DropdownRefId;

                if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                {
                    if (entity.TableName == "MT_Supplier_Head")
                    {
                        ddSupplier.Items.Add(item);
                    }
                    if (entity.TableName == "SRF_MT_PurposeOfPullOut")
                    {
                        ddPurposeOfPullOut.Items.Add(item);
                    }
                    if (entity.TableName == "SRF_MT_LOA")
                    {
                        ddLoaNo.Items.Add(item);
                    }
                    if (entity.TableName == "MT_Category")
                    {
                        if (Request.QueryString["SRF_ControlNo_From_Details"].ToString().Length > 0)
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
                        else
                        {
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
                    }
                    
                }
            }


        }

        protected void btnCancelTransaction_Click(object sender, EventArgs e)
        {
            try
            {
                string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                string query1 = string.Empty;
                string query2 = string.Empty;
                string querySuccess = string.Empty;

                query1 = "UPDATE SRF_MT_LOA SET Balance = (Balance + " + int.Parse(hfTotalQuantity.Value) + ") WHERE RefId = '" + hfLOANumber.Value + "'";
                query2 = "UPDATE SRF_TRANSACTION_Status SET Pur_Manager = '" + Session["LcRefId"].ToString() + "', DOAPur_Manager = GETDATE(), STATPur_Manager = 3, Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + hfCTRLNo.Value.Trim() + "'";

                querySuccess = BLL.SRF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + query2 + queryEndPart).ToString();

                if (querySuccess == "2")
                {
                    Session["successMessage"] = "SRF NUMBER(S) : <b>" + this.Title.Trim().ToUpper() + "</b> HAS BEEN SUCCESSFULLY CANCELED.";
                    Session["successTransactionName"] = "SRF_REQUESTENTRY";
                    Session["successReturnPage"] = "SRF_RequestPrint.aspx?SRF_ControlNo_From_Details=" + hfCTRLNo.Value.Trim();
                    Response.Redirect("SuccessPage.aspx");
                }
                

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "'\n\n\n Tips : Try to log-out then log-in again to the system again. Maybe the error cause is session timeout. Always keep in mind that our system expires the sessions in 10 minutes idle time. Please do log-out if you have no any transations needed to update and etc. Thank You!);", true);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool isError = false;
                int attachedFiles = 0;
                int quantity = 0;

                string successEntry = string.Empty;
                string successStatus = string.Empty;
                string successUpdatingLOABalance = string.Empty;
                string successDeleteDetails = string.Empty;
                string successUpdateDetails = string.Empty;
                string ctrlno = string.Empty;                

                if (!isError)
                {
                    Session["SRF_Error_Entry"] = null;

                    Entities_SRF_RequestEntry entity = new Entities_SRF_RequestEntry();

                    entity.Attention = string.Empty;
                    entity.Category = ddCategory.SelectedValue;
                    entity.CtrlNo = !String.IsNullOrEmpty(Request.QueryString["SRF_ControlNo_From_Details"].ToString()) ? CryptorEngine.Decrypt(Request.QueryString["SRF_ControlNo_From_Details"].ToString().Replace(" ", "+"), true) : setControlNumberWithPrefix();
                    entity.DeliveryDateToRepi = txtDeliveryDateToRepi.Text.Trim();
                    entity.Loa8106 = txtLoa8106.Text;
                    entity.LoaNo = ddLoaNo.SelectedValue;
                    entity.LoaSuretyBond = txtSuretyBond.Text;
                    entity.ProblemEncountered = txtProblemEncountered.Text;
                    entity.PullOutServiceDate = txtServiceDate.Text;
                    entity.PurposeOfPullOut = ddPurposeOfPullOut.SelectedValue;
                    entity.Remarks = txtRemarks.Text;
                    entity.Requester = Session["LcRefId"].ToString();
                    entity.Supplier = ddSupplier.SelectedValue;
                    //entity.TotalQuantity = txtTotalQuantity.Text.Trim();

                    if (chkNoNeed8105.Checked)
                    {
                        entity.Document8105 = "1";
                    }
                    else
                    {
                        entity.Document8105 = "0";
                    }

                    // COMPUTE TOTAL QUANTITY
                    if (gvData.Visible)
                    {

                        if (gvData.Rows.Count > 0)
                        {

                            for (int i = 0; i < gvData.Rows.Count; i++)
                            {
                                try
                                {
                                    TextBox txtQuantity = (TextBox)gvData.Rows[i].Cells[6].FindControl("txtQuantity");
                                    quantity += int.Parse(txtQuantity.Text.Trim());
                                }
                                catch (Exception ex)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                                }
                            }

                            entity.TotalQuantity = quantity.ToString("#,###");
                            txtTotalQuantity.Text = quantity.ToString("#,###");
                        }
                        else
                        {
                            entity.TotalQuantity = "0";
                        }
                    }

                    if (gvDataUpdate.Visible)
                    {

                        if (gvDataUpdate.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvDataUpdate.Rows.Count; i++)
                            {
                                try
                                {
                                    TextBox txtQuantity = (TextBox)gvDataUpdate.Rows[i].Cells[6].FindControl("txtQuantity");
                                    quantity += int.Parse(txtQuantity.Text.Trim());
                                }
                                catch (Exception ex)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                                }
                            }

                            entity.TotalQuantity = quantity.ToString("#,###");
                            txtTotalQuantity.Text = quantity.ToString("#,###");
                        }
                        else
                        {
                            entity.TotalQuantity = "0";
                        }
                    }

                    entity.TotalValueInUsd = txtTotalValueInUsd.Text.Trim();
                    ctrlno = entity.CtrlNo;
                    entity.UpdatedBy = Session["LcRefId"].ToString();
                    entity.Attachment = string.Empty;
                    entity.GatePassNo = txtGatePassNo.Text;
                    entity.PickUpPoint = txtPickUpPoint.Text;

                    // setting status to zero for peding as default value
                    entity.ReqInchargeStat = "0";
                    entity.ReqManagerStat = "0";
                    entity.PurInchargeStat = "0";
                    entity.PurImpexStat = "0";
                    entity.PurDeptManagerStat = "0";

                    // APPEND OR UPDATE
                    if (!String.IsNullOrEmpty(Request.QueryString["SRF_ControlNo_From_Details"].ToString()))
                    {
                        successEntry = BLL.SRF_TRANSACTION_RequestEntry_Update(entity).ToString();

                        if (successEntry == "-1")
                        {

                            //DELETE DETAILS FIRST BEFORE RE-INSERT DATA
                            successDeleteDetails = BLL.SRF_TRANSACTION_RequestEntry_Details_Delete(entity).ToString();

                            if (successDeleteDetails == "-1")
                            {
                                // INSERT DETAILS

                                for (int i = 0; i < gvDataUpdate.Rows.Count; i++)
                                {
                                    Entities_SRF_RequestEntry entityDetails = new Entities_SRF_RequestEntry();

                                    TextBox txtRefPRPO = (TextBox)gvDataUpdate.Rows[i].Cells[1].FindControl("txtRefPRPO");
                                    TextBox txtSalesInvoice = (TextBox)gvDataUpdate.Rows[i].Cells[2].FindControl("txtSalesInvoice");
                                    TextBox txtBrandMachine = (TextBox)gvDataUpdate.Rows[i].Cells[3].FindControl("txtBrandMachine");
                                    TextBox txtItemName = (TextBox)gvDataUpdate.Rows[i].Cells[4].FindControl("txtItemName");
                                    TextBox txtSpecification = (TextBox)gvDataUpdate.Rows[i].Cells[5].FindControl("txtSpecification");
                                    TextBox txtQuantity = (TextBox)gvDataUpdate.Rows[i].Cells[6].FindControl("txtQuantity");
                                    DropDownList ddUOM = (DropDownList)gvDataUpdate.Rows[i].Cells[7].FindControl("ddUOM");
                                    TextBox txtSerialNo = (TextBox)gvDataUpdate.Rows[i].Cells[8].FindControl("txtSerialNo");

                                    entityDetails.CtrlNo = ctrlno;
                                    entityDetails.RefPRPO = txtRefPRPO.Text.Replace("'", "''");
                                    entityDetails.SalesInvoice = txtSalesInvoice.Text.Replace("'", "''");
                                    entityDetails.BrandMachineName = txtBrandMachine.Text.Replace("'", "''");
                                    entityDetails.ItemName = txtItemName.Text.Replace("'", "''");
                                    entityDetails.Specification = txtSpecification.Text.Replace("'", "''");
                                    entityDetails.Quantity = txtQuantity.Text.Replace("'", "''");
                                    entityDetails.UnitOfMeasure = ddUOM.SelectedValue;
                                    entityDetails.SerialNo = txtSerialNo.Text.Replace("'", "''");

                                    BLL.SRF_TRANSACTION_RequestEntry_Details_Insert(entityDetails);
                                }
                            }

                            isError = false;


                            if (ddLoaNo.Enabled)
                            {
                                //if (ddLoaNo.SelectedIndex > -1)
                                if (ddLoaNo.SelectedIndex > 0)
                                {

                                    string loaBalance = ddLoaNo.SelectedItem.Text.Substring(ddLoaNo.SelectedItem.Text.IndexOf("[") + 1, ddLoaNo.SelectedItem.Text.IndexOf("]") - (ddLoaNo.SelectedItem.Text.IndexOf("[") + 1)).ToString().Trim();
                                    int total = 0;
                                    int hfQuantity = 0;

                                    total = int.Parse(loaBalance) - int.Parse(txtTotalQuantity.Text);

                                    Entities_SRF_LOA loa = new Entities_SRF_LOA();
                                    loa.RefId = ddLoaNo.SelectedValue;

                                    if (!hfTotalQuantity.Value.Equals(txtTotalQuantity.Text.Trim()))
                                    {
                                        hfQuantity = (int.Parse(loaBalance) + int.Parse(hfTotalQuantity.Value)) - int.Parse(txtTotalQuantity.Text);
                                        loa.Balance = hfQuantity.ToString();
                                    }
                                    else
                                    {
                                        loa.Balance = total.ToString();
                                    }

                                    successUpdatingLOABalance = BLL.SRF_MT_LOA_UpdateBalance_ByRefId(loa).ToString();

                                    if (successUpdatingLOABalance == "-1")
                                    {
                                        isError = false;
                                    }
                                }

                            }

                            //UPDATE TRANSFER CATEOGORY
                            string queryBeginPart = string.Empty;
                            string queryEndPart = string.Empty;
                            string queryDetails = string.Empty;

                            queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                            queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                            queryDetails = "INSERT INTO HistoryOfUpdates (RFQNo, DetailsRefId, TableName, UpdatedBy, UpdatedDate, TransactionName, UpdateWhat) " +
                                           "VALUES ('" + ctrlno + "','NA','SRF_TRANSACTION','" + Session["LcRefId"].ToString() + "',GETDATE(),'Purchasing-NotMyCategory','Category from " + Session["SRF_OLD_CATEGORY_DESCRIPTION"].ToString().ToUpper() + " to " + ddCategory.SelectedItem.Text.ToUpper() + "') ";


                            if (Session["SRF_OLD_CATEGORY_VALUE"] != null)
                            {
                                if (Session["SRF_OLD_CATEGORY_VALUE"].ToString().Trim() == ddCategory.SelectedValue.Trim())
                                {
                                    //DO NOTHING
                                }
                                else
                                {
                                    BLL.SRF_TRANSACTION_SQLTransaction(queryBeginPart + queryDetails + queryEndPart); 
                                }
                            }

                        }

                    }
                    else // INSERT
                    {
                        // Process Attachment

                        if (fu1.HasFile || fu2.HasFile || fu3.HasFile)
                        {

                            if (!System.IO.Directory.Exists(Server.MapPath("~/SRF_Request/" + entity.CtrlNo)))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/SRF_Request/" + entity.CtrlNo));
                            }
                            if (System.IO.Directory.Exists(Server.MapPath("~/SRF_Request/" + entity.CtrlNo)))
                            {
                                //try
                                //{
                                if (fu1.HasFile)
                                {
                                    string filename1 = Path.GetFileName(fu1.FileName);
                                    string fileExtensionApplication = Path.GetExtension(filename1);
                                    fu1.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + entity.CtrlNo, filename1));
                                    fu1.Dispose();
                                    File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + entity.CtrlNo), filename1), Path.Combine(Server.MapPath("~/SRF_Request/" + entity.CtrlNo), ("1-" + entity.CtrlNo + fileExtensionApplication)), true);
                                    File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + entity.CtrlNo), filename1));
                                    attachedFiles++;
                                }
                                if (fu2.HasFile)
                                {
                                    string filename2 = Path.GetFileName(fu2.FileName);
                                    string fileExtensionApplication = Path.GetExtension(filename2);
                                    fu2.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + entity.CtrlNo, filename2));
                                    fu2.Dispose();
                                    File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + entity.CtrlNo), filename2), Path.Combine(Server.MapPath("~/SRF_Request/" + entity.CtrlNo), ("2-" + entity.CtrlNo + fileExtensionApplication)), true);
                                    File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + entity.CtrlNo), filename2));
                                    attachedFiles++;
                                }
                                if (fu3.HasFile)
                                {
                                    string filename3 = Path.GetFileName(fu3.FileName);
                                    string fileExtensionApplication = Path.GetExtension(filename3);
                                    fu3.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + entity.CtrlNo, filename3));
                                    fu3.Dispose();
                                    File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + entity.CtrlNo), filename3), Path.Combine(Server.MapPath("~/SRF_Request/" + entity.CtrlNo), ("3-" + entity.CtrlNo + fileExtensionApplication)), true);
                                    File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + entity.CtrlNo), filename3));
                                    attachedFiles++;
                                }

                                if (attachedFiles > 0)
                                {
                                    entity.Attachment = attachedFiles.ToString();
                                }
                                //}
                                //catch (Exception ex)
                                //{
                                //    isError = true;
                                //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                                //}
                            }

                        }
                        else
                        {
                            entity.Attachment = "0";
                        }

                        if (!isError)
                        {
                            successEntry = BLL.SRF_TRANSACTION_RequestEntry_Insert(entity).ToString();

                            if (successEntry == "-1")
                            {

                                // INSERT DETAILS

                                for (int i = 0; i < gvData.Rows.Count; i++)
                                {
                                    Entities_SRF_RequestEntry entityDetails = new Entities_SRF_RequestEntry();

                                    TextBox txtRefPRPO = (TextBox)gvData.Rows[i].Cells[1].FindControl("txtRefPRPO");
                                    TextBox txtSalesInvoice = (TextBox)gvData.Rows[i].Cells[2].FindControl("txtSalesInvoice");
                                    TextBox txtBrandMachine = (TextBox)gvData.Rows[i].Cells[3].FindControl("txtBrandMachine");
                                    TextBox txtItemName = (TextBox)gvData.Rows[i].Cells[4].FindControl("txtItemName");
                                    TextBox txtSpecification = (TextBox)gvData.Rows[i].Cells[5].FindControl("txtSpecification");
                                    TextBox txtQuantity = (TextBox)gvData.Rows[i].Cells[6].FindControl("txtQuantity");
                                    DropDownList ddUOM = (DropDownList)gvData.Rows[i].Cells[7].FindControl("ddUOM");
                                    TextBox txtSerialNo = (TextBox)gvData.Rows[i].Cells[8].FindControl("txtSerialNo");

                                    entityDetails.CtrlNo = ctrlno;
                                    entityDetails.RefPRPO = txtRefPRPO.Text.Replace("'", "''");
                                    entityDetails.SalesInvoice = txtSalesInvoice.Text.Replace("'", "''");
                                    entityDetails.BrandMachineName = txtBrandMachine.Text.Replace("'", "''");
                                    entityDetails.ItemName = txtItemName.Text.Replace("'", "''");
                                    entityDetails.Specification = txtSpecification.Text.Replace("'", "''");
                                    entityDetails.Quantity = txtQuantity.Text.Replace("'", "''");
                                    entityDetails.UnitOfMeasure = ddUOM.SelectedValue;
                                    entityDetails.SerialNo = txtSerialNo.Text.Replace("'", "''");

                                    BLL.SRF_TRANSACTION_RequestEntry_Details_Insert(entityDetails);
                                }

                                isError = false;
                                successStatus = BLL.SRF_TRANSACTION_Status_Insert(entity).ToString();

                                Entities_SRF_RequestEntry autoApprovedRequestorIncharge = new Entities_SRF_RequestEntry();
                                autoApprovedRequestorIncharge.ReqIncharge = Session["LcRefId"].ToString();
                                autoApprovedRequestorIncharge.ReqInchargeStat = "1";
                                autoApprovedRequestorIncharge.CtrlNo = ctrlno;
                                autoApprovedRequestorIncharge.Remarks = string.Empty;
                                BLL.SRF_TRANSACTION_ApprovedRequestorIncharge(autoApprovedRequestorIncharge);

                                if (successStatus == "-1")
                                {
                                    isError = false;

                                    if (ddLoaNo.Enabled)
                                    {
                                        string loaBalance = ddLoaNo.SelectedItem.Text.Substring(ddLoaNo.SelectedItem.Text.IndexOf("[") + 1, ddLoaNo.SelectedItem.Text.IndexOf("]") - (ddLoaNo.SelectedItem.Text.IndexOf("[") + 1)).ToString().Trim();
                                        int total = 0;
                                        total = int.Parse(loaBalance) - int.Parse(txtTotalQuantity.Text);

                                        Entities_SRF_LOA loa = new Entities_SRF_LOA();
                                        loa.RefId = ddLoaNo.SelectedValue;
                                        loa.Balance = total.ToString();

                                        successUpdatingLOABalance = BLL.SRF_MT_LOA_UpdateBalance_ByRefId(loa).ToString();

                                        if (successUpdatingLOABalance == "-1")
                                        {
                                            isError = false;
                                        }
                                    }

                                }
                            }
                        }

                    }


                }
                else
                {
                    if (Session["SRF_Error_Entry"] != null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + Session["SRF_Error_Entry"].ToString() + "'", true);
                    }
                }

                //if (!COMMON.isNumeric(txtTotalQuantity.Text.Trim(), System.Globalization.NumberStyles.Integer))
                //{
                //    isError = true;
                //    Session["SRF_Error_Entry"] = "Total Quantity must not be blank.";
                //}
                //else
                //{
                //    if (Session["SRF_Error_Entry"] != null)
                //    {
                //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + Session["SRF_Error_Entry"].ToString() + "'", true);
                //    }
                //}

                if (!isError)
                {
                    Session["successMessage"] = "CONTROL NUMBER : <b>" + ctrlno + "</b> HAS BEEN SUCCESSFULLY UPDATED.";
                    Session["successTransactionName"] = "SRF_REQUEST_ENTRY";
                    if (Request.QueryString["SRF_ControlNo_From_Details"].ToString().Length > 0)
                    {
                        Session["successReturnPage"] = "SRF_RequestEntry.aspx?SRF_ControlNo_From_Details=" + CryptorEngine.Encrypt(ctrlno.Replace(" ", "+"), true);
                    }
                    else
                    {
                        Session["successReturnPage"] = "SRF_RequestEntry.aspx?SRF_ControlNo_From_Details=";
                    }

                    Response.Redirect("SuccessPage.aspx");
                }
                

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "'\n\n\n Tips : Try to log-out then log-in again to the system again. Maybe the error cause is session timeout. Always keep in mind that our system expires the sessions in 10 minutes idle time. Please do log-out if you have no any transations needed to update and etc. Thank You!);", true);
            }
        }


        private string setControlNumberWithPrefix()
        {
            string retVal = string.Empty;

            retVal = "SRF_" + Session["DivisionCode"].ToString() + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + COMMON.controlNoZeroPrefix(setControlNumber().ToString().Length.ToString()) + setControlNumber().ToString();

            return retVal;
        }

        private Int32 setControlNumber()
        {
            return BLL.SRF_TRANSACTION_Request_Count(DateTime.Now.Year.ToString()) + 1;
        }

        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddUOM = (DropDownList)e.Row.FindControl("ddUOM");
                    

                    List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
                    list = BLL.SRF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(item => item.DropdownName).ToList();

                    if (list.Count > 0)
                    {
                        foreach (Entities_SRF_RequestEntry entity in list)
                        {
                            ListItem item = new ListItem();
                            item.Text = entity.DropdownName;
                            item.Value = entity.DropdownRefId;

                            if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                            {
                                if (entity.TableName == "MT_UnitOfMeasure")
                                {
                                    ddUOM.Items.Add(item);
                                }                                
                            }

                        }
                    }
                    

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvDataUpdate_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddUOM = (DropDownList)e.Row.FindControl("ddUOM");
                    Label lblUpdatedUOM = (Label)e.Row.FindControl("lblUpdatedUOM");
                    Label lblRefId = (Label)e.Row.FindControl("lblRefId");

                    List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
                    list = BLL.SRF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(item => item.DropdownName).ToList();

                    if (list.Count > 0)
                    {
                        foreach (Entities_SRF_RequestEntry entity in list)
                        {
                            ListItem item = new ListItem();
                            item.Text = entity.DropdownName;
                            item.Value = entity.DropdownRefId;

                            if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                            {
                                if (entity.TableName == "MT_UnitOfMeasure")
                                {
                                    ddUOM.Items.Add(item);
                                }
                            }

                        }
                    }

                    ddUOM.Items.FindByValue(lblUpdatedUOM.Text).Selected = true;
                    lblRefId.Text = (e.Row.RowIndex + 1).ToString();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int quantity = 0;

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

            for (int i = 0; i < gvData.Rows.Count; i++)
            {
                try
                {
                    TextBox txtQuantity = (TextBox)gvData.Rows[i].Cells[6].FindControl("txtQuantity");

                    quantity += int.Parse(txtQuantity.Text.Trim());

                    txtTotalQuantity.Text = quantity.ToString("#,###");
                    
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                }
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            bool isErrorQuantity = false;

            for (int i = 0; i < gvData.Rows.Count; i++)
            {
                TextBox txtQuantity = (TextBox)gvData.Rows[i].Cells[6].FindControl("txtQuantity");

                if (!COMMON.isNumeric(txtQuantity.Text.Trim(), System.Globalization.NumberStyles.Number) || String.IsNullOrEmpty(txtQuantity.Text.Trim()))
                {
                    isErrorQuantity = true;
                    txtQuantity.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    isErrorQuantity = false;
                    txtQuantity.BackColor = System.Drawing.Color.White;
                }
                
            }

            if (!isErrorQuantity)
            {
                int quantity = 0;
                for (int i = 0; i < gvData.Rows.Count; i++)
                {
                    try
                    {
                        TextBox txtQuantity = (TextBox)gvData.Rows[i].Cells[6].FindControl("txtQuantity");                        

                        quantity += int.Parse(txtQuantity.Text.Trim());

                        txtTotalQuantity.Text = quantity.ToString("#,###");
                        
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                    }
                }

                AddNewRow();

                txtTotalQuantity.ReadOnly = true;

            }
        }

        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Col1", typeof(string)));
            dt.Columns.Add(new DataColumn("Col2", typeof(string)));
            dt.Columns.Add(new DataColumn("Col3", typeof(string)));
            dt.Columns.Add(new DataColumn("Col4", typeof(string)));
            dt.Columns.Add(new DataColumn("Col5", typeof(string)));
            dt.Columns.Add(new DataColumn("Col6", typeof(string)));
            dt.Columns.Add(new DataColumn("Col7", typeof(string)));
            dt.Columns.Add(new DataColumn("Col8", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Col1"] = string.Empty;
            dr["Col2"] = string.Empty;
            dr["Col3"] = string.Empty;
            dr["Col4"] = string.Empty;
            dr["Col5"] = string.Empty;
            dr["Col6"] = string.Empty;
            dr["Col7"] = string.Empty;
            dr["Col8"] = string.Empty;


            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;

            gvData.DataSource = dt;
            gvData.DataBind();
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
                        TextBox txtRefPRPO = (TextBox)gvData.Rows[rowIndex].Cells[1].FindControl("txtRefPRPO");
                        TextBox txtSalesInvoice = (TextBox)gvData.Rows[rowIndex].Cells[2].FindControl("txtSalesInvoice");
                        TextBox txtBrandMachine = (TextBox)gvData.Rows[rowIndex].Cells[3].FindControl("txtBrandMachine");
                        TextBox txtItemName = (TextBox)gvData.Rows[rowIndex].Cells[4].FindControl("txtItemName");
                        TextBox txtSpecification = (TextBox)gvData.Rows[rowIndex].Cells[5].FindControl("txtSpecification");
                        TextBox txtQuantity = (TextBox)gvData.Rows[rowIndex].Cells[6].FindControl("txtQuantity");
                        DropDownList ddUOM = (DropDownList)gvData.Rows[rowIndex].Cells[7].FindControl("ddUOM");
                        TextBox txtSerialNo = (TextBox)gvData.Rows[rowIndex].Cells[8].FindControl("txtSerialNo");


                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Col1"] = txtRefPRPO.Text;
                        dtCurrentTable.Rows[i - 1]["Col2"] = txtSalesInvoice.Text;
                        dtCurrentTable.Rows[i - 1]["Col3"] = txtBrandMachine.Text;
                        dtCurrentTable.Rows[i - 1]["Col4"] = txtItemName.Text;
                        dtCurrentTable.Rows[i - 1]["Col5"] = txtSpecification.Text;
                        dtCurrentTable.Rows[i - 1]["Col6"] = txtQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["Col7"] = ddUOM.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Col8"] = txtSerialNo.Text;

                        rowIndex++;

                    }

                    ViewState["CurrentTable"] = dtCurrentTable;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('View State [CurrentTable] is null.');", true);
            }

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
                        TextBox txtRefPRPO = (TextBox)gvData.Rows[rowIndex].Cells[1].FindControl("txtRefPRPO");
                        TextBox txtSalesInvoice = (TextBox)gvData.Rows[rowIndex].Cells[2].FindControl("txtSalesInvoice");
                        TextBox txtBrandMachine = (TextBox)gvData.Rows[rowIndex].Cells[3].FindControl("txtBrandMachine");
                        TextBox txtItemName = (TextBox)gvData.Rows[rowIndex].Cells[4].FindControl("txtItemName");
                        TextBox txtSpecification = (TextBox)gvData.Rows[rowIndex].Cells[5].FindControl("txtSpecification");
                        TextBox txtQuantity = (TextBox)gvData.Rows[rowIndex].Cells[6].FindControl("txtQuantity");
                        DropDownList ddUOM = (DropDownList)gvData.Rows[rowIndex].Cells[7].FindControl("ddUOM");
                        TextBox txtSerialNo = (TextBox)gvData.Rows[rowIndex].Cells[8].FindControl("txtSerialNo");

                        txtRefPRPO.Text = dt.Rows[i]["Col1"].ToString();
                        txtSalesInvoice.Text = dt.Rows[i]["Col2"].ToString();
                        txtBrandMachine.Text = dt.Rows[i]["Col3"].ToString();
                        txtItemName.Text = dt.Rows[i]["Col4"].ToString();
                        txtSpecification.Text = dt.Rows[i]["Col5"].ToString();
                        txtQuantity.Text = dt.Rows[i]["Col6"].ToString();
                        ddUOM.SelectedValue = dt.Rows[i]["Col7"].ToString();
                        txtSerialNo.Text = dt.Rows[i]["Col8"].ToString();

                        rowIndex++;
                    }
                }
            }
        }


        private void AddNewRow()
        {
            int rowIndex = 0;

            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
            list = BLL.SRF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(item => item.DropdownName).ToList();

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtRefPRPO = (TextBox)gvData.Rows[rowIndex].Cells[1].FindControl("txtRefPRPO");
                        TextBox txtSalesInvoice = (TextBox)gvData.Rows[rowIndex].Cells[2].FindControl("txtSalesInvoice");
                        TextBox txtBrandMachine = (TextBox)gvData.Rows[rowIndex].Cells[3].FindControl("txtBrandMachine");
                        TextBox txtItemName = (TextBox)gvData.Rows[rowIndex].Cells[4].FindControl("txtItemName");
                        TextBox txtSpecification = (TextBox)gvData.Rows[rowIndex].Cells[5].FindControl("txtSpecification");
                        TextBox txtQuantity = (TextBox)gvData.Rows[rowIndex].Cells[6].FindControl("txtQuantity");
                        DropDownList ddUOM = (DropDownList)gvData.Rows[rowIndex].Cells[7].FindControl("ddUOM");
                        TextBox txtSerialNo = (TextBox)gvData.Rows[rowIndex].Cells[8].FindControl("txtSerialNo");

                        if (list.Count > 0)
                        {
                            foreach (Entities_SRF_RequestEntry entity in list)
                            {
                                ListItem item = new ListItem();
                                item.Text = entity.DropdownName;
                                item.Value = entity.DropdownRefId;

                                if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                                {
                                    if (entity.TableName == "MT_UnitOfMeasure")
                                    {
                                        ddUOM.Items.Add(item);
                                    }                                    
                                }

                            }
                        }

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["Col1"] = txtRefPRPO.Text;
                        dtCurrentTable.Rows[i - 1]["Col2"] = txtSalesInvoice.Text;
                        dtCurrentTable.Rows[i - 1]["Col3"] = txtBrandMachine.Text;
                        dtCurrentTable.Rows[i - 1]["Col4"] = txtItemName.Text;
                        dtCurrentTable.Rows[i - 1]["Col5"] = txtSpecification.Text;                        
                        dtCurrentTable.Rows[i - 1]["Col6"] = txtQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["Col7"] = ddUOM.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Col8"] = txtSerialNo.Text;            
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
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }


        protected void linkAttachment1_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://10.27.1.170:9292/SRF_Request/" + CryptorEngine.Decrypt(Request.QueryString["SRF_ControlNo_From_Details"].ToString().Replace(" ", "+"), true) + "/" + linkAttachment1.Text.Trim() + ".pdf", true);
        }
        protected void linkAttachment2_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://10.27.1.170:9292/SRF_Request/" + CryptorEngine.Decrypt(Request.QueryString["SRF_ControlNo_From_Details"].ToString().Replace(" ", "+"), true) + "/" + linkAttachment1.Text.Trim() + ".pdf", true);
        }
        protected void linkAttachment3_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://10.27.1.170:9292/SRF_Request/" + CryptorEngine.Decrypt(Request.QueryString["SRF_ControlNo_From_Details"].ToString().Replace(" ", "+"), true) + "/" + linkAttachment1.Text.Trim() + ".pdf", true);
        }






    }
}
