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
using SpreadsheetLight;

namespace REPI_PUR_SOFRA
{
    public partial class RFQ_WithNoResponse : System.Web.UI.Page
    {

        BLL_RFQ BLL = new BLL_RFQ();
        Common COMMON = new Common();

        BLL_Common BLL_COMMON = new BLL_Common();

        

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["ProductionApprovalAccess"].ToString().Trim()) || COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()))
                    {

                        //txtFrom.Text = DateTime.Today.AddDays(-7).ToString("MM/dd/yyyy");
                        //txtTo.Text = DateTime.Today.AddDays(7).ToString("MM/dd/yyyy");

                        //---------------------------------------------------------------------------------------------------

                        List<Entities_Common_SystemUsers> listBuyer = new List<Entities_Common_SystemUsers>();
                        List<Entities_Common_SystemUsers> listIncharge = new List<Entities_Common_SystemUsers>();
                        List<Entities_Common_SystemUsers> listDeptManager = new List<Entities_Common_SystemUsers>();
                        List<Entities_Common_SystemUsers> listDivManager = new List<Entities_Common_SystemUsers>();

                        listIncharge = BLL_COMMON.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), "15");
                        listDeptManager = BLL_COMMON.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), "16");
                        listDivManager = BLL_COMMON.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), "17");

                        // INCHARGE
                        if (listIncharge != null)
                        {
                            if (listIncharge.Count > 0)
                            {
                                Session["listIncharge"] = listIncharge.Count.ToString();
                            }
                            else
                            {
                                Session["listIncharge"] = "0";
                            }
                        }
                        else
                        {
                            Session["listIncharge"] = "0";
                        }

                        // DEPARTMENT MANAGER
                        if (listDeptManager != null)
                        {
                            if (listDeptManager.Count > 0)
                            {
                                Session["listDeptManager"] = listDeptManager.Count.ToString();
                            }
                            else
                            {
                                Session["listDeptManager"] = "0";
                            }
                        }
                        else
                        {
                            Session["listDeptManager"] = "0";
                        }

                        // DIVISION MANAGER
                        if (listDivManager != null)
                        {
                            if (listDivManager.Count > 0)
                            {
                                Session["listDivManager"] = listDivManager.Count.ToString();
                            }
                            else
                            {
                                Session["listDivManager"] = "0";
                            }
                        }
                        else
                        {
                            Session["listDivManager"] = "0";
                        }

                        if (BLL_COMMON.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString()).Count <= 0)
                        {
                            ddCategory.Visible = false;
                            ddApprover.Visible = false;
                            thCategory.Style.Add("display", "none");
                            thApprover.Style.Add("display", "none");
                            tblApproval.Style.Add("width", "420px");
                        }

                        //---------------------------------------------------------------------------------------------------

                        List<Entities_RFQ_RequestEntry> listCategory = new List<Entities_RFQ_RequestEntry>();
                        listCategory = BLL.RFQ_TRANSACTION_RequestEntry_Fill_All_DropdownList();

                        if (listCategory != null)
                        {
                            if (listCategory.Count > 0)
                            {
                                ddCategory.Items.Clear();
                                ddCategory.Items.Add("ALL CATEGORY");

                                foreach (Entities_RFQ_RequestEntry entity in listCategory)
                                {
                                    ListItem item = new ListItem();
                                    item.Text = entity.DropdownName;
                                    item.Value = entity.DropdownRefId;

                                    if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                                    {
                                        if (entity.TableName == "MT_Category")
                                        {
                                            ddCategory.Items.Add(item);
                                        }

                                    }

                                }

                            }
                        }

                        //---------------------------------------------------------------------------------------------------
                        string category = Session["CategoryAccess"].ToString();
                        if (!string.IsNullOrEmpty(category))
                        {
                            if (int.Parse(category) > 0)
                            {
                                ddCategory.Items.FindByValue(category).Selected = true;
                                ddCategory.Enabled = false;
                            }
                            else
                            {
                                ddCategory.Items.FindByText("ALL CATEGORY").Selected = true;
                                ddCategory.Enabled = true;
                            }
                        }

                        //---------------------------------------------------------------------------------------------------

                        if (Session["Username"].ToString() == "6985" || Session["Username"].ToString() == "3844")
                        {
                            // Incharge Approval 
                            ddApprover.Items.FindByValue("2").Selected = true;
                        }
                        else if (Session["Username"].ToString() == "1152" || Session["Username"].ToString() == "1402")
                        {
                            // Department Manager Approval 
                            ddApprover.Items.FindByValue("3").Selected = true;
                        }
                        else if (Session["Username"].ToString() == "002")
                        {
                            // Division Manager Approval
                            ddApprover.Items.FindByValue("4").Selected = true;
                        }
                        else
                        {
                            // Buyer Approval 
                            ddApprover.Items.FindByValue("1").Selected = true;
                            ddApprover.Enabled = false;
                        }

                        //---------------------------------------------------------------------------------------------------

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
                //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Javascript:alert('" + ex.StackTrace.ToString() + "\n\n" + ex.InnerException.ToString() + "')", true);
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string approver = Session["LcRefId"].ToString();

                List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                List<Entities_Common_SystemUsers> listProdManagerApproval = new List<Entities_Common_SystemUsers>();
                List<Entities_Common_SystemUsers> listPurchasingApproval = new List<Entities_Common_SystemUsers>();

                //entity.DrFrom = txtFrom.Text.Trim();
                //entity.DrTo = txtTo.Text.Trim();

                list = null;

                listProdManagerApproval = BLL_COMMON.Common_checkIfUserAccessExistByUserId(approver, ConfigurationManager.AppSettings["ProductionApprovalAccess"].ToString());
                listPurchasingApproval = BLL_COMMON.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString());

                // PRODUCTION MANAGER --------------------------------------------------------------------------------------

                if (listProdManagerApproval.Count > 0 && listPurchasingApproval.Count <= 0)
                {
                    Session["ProdManagerIsApprovingRequest"] = "true";

                    if (!string.IsNullOrEmpty(txtRFQNo.Text))
                    {
                        list = BLL.RFQ_TRANSACTION_Approval(entity).Where(itm => itm.RhDepartment == Session["Department"].ToString() && itm.ProdManagerStatus == "0" && itm.Rfqno.Contains(txtRFQNo.Text)).ToList();
                    }
                    else
                    {
                        list = BLL.RFQ_TRANSACTION_Approval(entity).Where(itm => itm.RhDepartment == Session["Department"].ToString() && itm.ProdManagerStatus == "0").ToList();
                    }

                    if (list != null)
                    {
                        if (list.Count > 0)
                        {
                            gvData.Visible = true;
                            gvData.DataSource = list;
                            gvData.DataBind();
                        }
                        else
                        {
                            gvData.Visible = false;
                        }
                    }
                }

                // ----------------------------------------------------------------------------------------------------------


                // PURCHASING APPROVER --------------------------------------------------------------------------------------

                if (listPurchasingApproval.Count > 0)
                {
                    Session["PurchasingIsApprovingRequest"] = "true";

                    entity.SearchCriteria = txtRFQNo.Text;


                    btnExportToExcel.Visible = true;


                    // FOR PRODUCTION MANAGER APPROVAL - If requester is from purchasing so need to approvad by purchasing manager as prod manager
                    if (ddApprover.SelectedItem.Text == "FOR PROD. MANAGER APPROVAL")
                    {
                        if (ddCategory.SelectedItem.Text != "ALL CATEGORY")
                        {
                            list = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.ProdManagerStatus == "0" && itm.RhDepartment == Session["Department"].ToString() && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                        }
                        else
                        {
                            list = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.ProdManagerStatus == "0" && itm.RhDepartment == Session["Department"].ToString() && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                        }
                    }
                    // FOR BUYER APPROVAL
                    if (ddApprover.SelectedItem.Text == "FOR BUYER APPROVAL")
                    {
                        if (ddCategory.SelectedItem.Text != "ALL CATEGORY")
                        {
                            list = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.PurchasingStatus == "1" && itm.BuyerStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                        }
                        else
                        {
                            list = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.PurchasingStatus == "1" && itm.BuyerStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                        }
                    }
                    // FOR INCHARGE APPROVAL
                    if (ddApprover.SelectedItem.Text == "FOR INCHARGE APPROVAL")
                    {
                        if (ddCategory.SelectedItem.Text != "ALL CATEGORY")
                        {
                            list = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                        }
                        else
                        {
                            //list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.GroupBySupplierResponse == "YES" && itm.RhCategory.Trim().Equals(incharge_category) ).ToList();

                            if (Session["Username"].ToString() == "3844" || Session["Username"].ToString() == "6985")
                            {
                                //SIR VAL
                                if (Session["Username"].ToString() == "3844")
                                {
                                    list = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0" && (itm.RhCategory.Trim() == "1009" || itm.RhCategory.Trim() == "1" || itm.RhCategory.Trim() == "1007" || itm.RhCategory.Trim() == "1014" || itm.RhCategory == "1013" || itm.RhCategory.Trim() == "1006" || itm.RhCategory.Trim() == "1010" || itm.RhCategory.Trim() == "7" || itm.RhCategory.Trim() == "2" || itm.RhCategory.Trim() == "1008")).ToList();
                                }
                                //SIR RUDY
                                if (Session["Username"].ToString() == "6985")
                                {
                                    list = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0" && itm.RhCategory.Trim() == "3").ToList();
                                }
                            }
                            else
                            {
                                list = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                            }
                        }
                    }
                    // FOR DEPARTMENT MANAGER APPROVAL
                    if (ddApprover.SelectedItem.Text == "FOR DEPARTMENT MANAGER APPROVAL")
                    {
                        if (ddCategory.SelectedItem.Text != "ALL CATEGORY")
                        {
                            list = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.PurchasingInchargeStatus == "1" && itm.DepartmentManagerStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                        }
                        else
                        {
                            list = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.PurchasingInchargeStatus == "1" && itm.DepartmentManagerStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                        }
                    }
                    // FOR DIVISION MANAGER APPROVAL
                    if (ddApprover.SelectedItem.Text == "FOR DIVISION MANAGER APPROVAL")
                    {
                        if (ddCategory.SelectedItem.Text != "ALL CATEGORY")
                        {
                            list = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.DepartmentManagerStatus == "1" && itm.DivisionManagerStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                        }
                        else
                        {
                            list = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.DepartmentManagerStatus == "1" && itm.DivisionManagerStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                        }
                    }

                    if (list != null)
                    {
                        if (list.Count > 0)
                        {
                            gvPurchasing.Visible = true;
                            gvRelatedSearch.Visible = false;
                            gvPurchasing.DataSource = list;
                            gvPurchasing.DataBind();
                            
                        }
                        //else
                        //{
                        //    gvPurchasing.Visible = false;
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('NO RECORDS FOUND BASED ON YOUR SEARCH CRITERIA. PLEASE SEE BELOW RELATED SEARCH RESULT.');", true);

                        //    List<Entities_RFQ_RequestEntry> listRelatedResults = new List<Entities_RFQ_RequestEntry>();
                        //    Entities_RFQ_RequestEntry entityRelatedResults = new Entities_RFQ_RequestEntry();

                        //    listRelatedResults = null;
                        //    entityRelatedResults.SearchCriteria = txtRFQNo.Text;

                        //    listRelatedResults = BLL.RFQ_TRANSACTION_AllRequest_ByDateRange(entityRelatedResults);

                        //    if (listRelatedResults != null)
                        //    {
                        //        if (listRelatedResults.Count > 0)
                        //        {
                        //            gvRelatedSearch.Visible = true;
                        //            gvRelatedSearch.DataSource = listRelatedResults;
                        //            gvRelatedSearch.DataBind();
                        //        }
                        //        else
                        //        {
                        //            gvRelatedSearch.Visible = false;
                        //            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('NO RECORDS FOUND IN RELATED SEARCH!');", true);
                        //        }
                        //    }
                        //}
                    }

                }

                // ----------------------------------------------------------------------------------------------------------


            }
            catch (Exception ex)
            {
                //throw ex;
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                int approvedCounter = 0;
                int disapprovedCounter = 0;
                string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                string query1 = string.Empty;
                string query2 = string.Empty;
                string query3 = string.Empty;
                string query4 = string.Empty;
                int queryStatusCounter = 0;
                string querySuccess = string.Empty;
                string temp_RFQNo = string.Empty;

                if (gvPurchasing.Rows.Count > 0)
                {
                    List<string> listRFQ_For_Sending = new List<string>();
                    string position = string.Empty;
                    string transactionName = string.Empty;
                    bool isSuccess = false;
                    string failedMessage = string.Empty;
                    int hasNoRemarks = 0;

                    if (ddApprover.SelectedItem.Text == "FOR PROD. MANAGER APPROVAL")
                    {
                        position = "ProdManager";
                        transactionName = ConfigurationManager.AppSettings["ApprovedDisapprovedName-ProdManager"].ToString();
                    }

                    if (ddApprover.SelectedItem.Text == "FOR BUYER APPROVAL")
                    {
                        // Buyer Approval 
                        position = "BUYER";
                        transactionName = ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingBuyer"].ToString();
                    }
                    if (ddApprover.SelectedItem.Text == "FOR INCHARGE APPROVAL")
                    {
                        // Incharge Approval 
                        position = "PURCHASINGINCHARGE";
                        transactionName = ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingIncharge"].ToString();
                    }
                    if (ddApprover.SelectedItem.Text == "FOR DEPARTMENT MANAGER APPROVAL")
                    {
                        // Department Manager Approval 
                        position = "DEPARTMENTMANAGER";
                        transactionName = ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingDeptManager"].ToString();
                    }
                    if (ddApprover.SelectedItem.Text == "FOR DIVISION MANAGER APPROVAL")
                    {
                        // Division Manager Approval
                        position = "DIVISIONMANAGER";
                        transactionName = ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingDivManager"].ToString();
                    }

                    for (int i = 0; i < gvPurchasing.Rows.Count; i++)
                    {
                        ImageButton ibApproved = (ImageButton)gvPurchasing.Rows[i].Cells[4].FindControl("ibApproved");
                        ImageButton ibDisapproved = (ImageButton)gvPurchasing.Rows[i].Cells[4].FindControl("ibDisapproved");
                        Label lblRFQNo = (Label)gvPurchasing.Rows[i].Cells[0].FindControl("lblRFQNo");
                        TextBox txtRemarks = (TextBox)gvPurchasing.Rows[i].Cells[5].FindControl("txtRemarks");

                        if (ibApproved.ImageUrl == "~/images/A2.png")
                        {
                            listRFQ_For_Sending.Add(lblRFQNo.Text.Trim());
                            approvedCounter++;
                        }
                        if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                        {
                            disapprovedCounter++;

                            if (string.IsNullOrEmpty(txtRemarks.Text))
                            {
                                hasNoRemarks++;
                            }
                        }
                    }

                    if (listRFQ_For_Sending != null)
                    {
                        if (listRFQ_For_Sending.Count > 0)
                        {
                            Session["RFQListForSending"] = listRFQ_For_Sending;
                        }
                    }

                    if (approvedCounter > 0 && disapprovedCounter > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You cannot approved and disapproved items at the same time.');", true);
                    }
                    if (approvedCounter > 0 && disapprovedCounter <= 0)
                    {
                        Response.Redirect("RFQ_SendToSuppliers.aspx", false);
                    }
                    if (approvedCounter <= 0 && disapprovedCounter > 0)
                    {
                        if (hasNoRemarks > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Remarks must not be blank.');", true);
                        }
                        else
                        {

                            for (int i = 0; i < gvPurchasing.Rows.Count; i++)
                            {
                                ImageButton ibApproved = (ImageButton)gvPurchasing.Rows[i].Cells[4].FindControl("ibApproved");
                                ImageButton ibDisapproved = (ImageButton)gvPurchasing.Rows[i].Cells[4].FindControl("ibDisapproved");
                                Label lblRFQNo = (Label)gvPurchasing.Rows[i].Cells[0].FindControl("lblRFQNo");
                                TextBox txtRemarks = (TextBox)gvPurchasing.Rows[i].Cells[5].FindControl("txtRemarks");

                                if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                                {
                                    if (position == "BUYER")
                                    {
                                        query1 += "UPDATE Request_Status SET " + position + " = 2 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        query1 += "UPDATE Request_Status SET " + position + " = 2, BUYER = 0 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
                                    }
                                    query2 += "INSERT INTO Request_HistoryOfDisapproval (RFQNo,Cause,TransactionName,DisapprovedBy,DisapprovedDate,IsActive) VALUES ('" + lblRFQNo.Text.Trim() +
                                              "','" + txtRemarks.Text + "','" + transactionName + "','" + Session["LcRefId"].ToString() + "',GETDATE(),1) ";

                                    queryStatusCounter = queryStatusCounter + 2;
                                    temp_RFQNo += lblRFQNo.Text.Trim().ToUpper() + ", ";
                                }
                            }

                            querySuccess = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + query2 + queryEndPart).ToString();

                            if (querySuccess == queryStatusCounter.ToString())
                            {
                                Session["successMessage"] = "RFQ NUMBER(S) : <b>" + temp_RFQNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
                                Session["successTransactionName"] = "RFQ_WITHNORESPONSE";
                                Session["successReturnPage"] = "RFQ_WithNoResponse.aspx";

                                Response.Redirect("SuccessPage.aspx");
                            }
                        }


                    }

                }

                

                

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }

            //try
            //{
            //    string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
            //    string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
            //    string query1 = string.Empty;
            //    string query2 = string.Empty;
            //    string query3 = string.Empty;
            //    string query4 = string.Empty;
            //    int queryStatusCounter = 0;
            //    string querySuccess = string.Empty;
            //    string temp_RFQNo = string.Empty;

            //    //-------------------------------------------------------------------------------------------------------------
            //    if (Session["ProdManagerIsApprovingRequest"] != null)
            //    {

            //        if (bool.Parse(Session["ProdManagerIsApprovingRequest"].ToString()))
            //        {
            //            if (gvData.Rows.Count > 0)
            //            {
            //                for (int i = 0; i < gvData.Rows.Count; i++)
            //                {
            //                    Label lblRFQNo = (Label)gvData.Rows[i].Cells[0].FindControl("lblRFQNo");
            //                    ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[3].FindControl("ibApproved");
            //                    ImageButton ibDisapproved = (ImageButton)gvData.Rows[i].Cells[3].FindControl("ibDisapproved");
            //                    TextBox txtRemarks = (TextBox)gvData.Rows[i].Cells[4].FindControl("txtRemarks");

            //                    if (ibApproved.ImageUrl == "~/images/A2.png")
            //                    {
            //                        query1 += "UPDATE Request_Status SET ProdManager = 1 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
            //                        query2 += "INSERT INTO Request_HistoryOfApproval (RFQNo,TransactionName,ApprovedBy,ApprovedDate,IsActive) VALUES ('" + lblRFQNo.Text.Trim() +
            //                                  "','" + ConfigurationManager.AppSettings["ApprovedDisapprovedName-ProdManager"].ToString() +
            //                                  "','" + Session["LcRefId"].ToString() + "',GETDATE(),1) ";

            //                        queryStatusCounter = queryStatusCounter + 2;
            //                        temp_RFQNo += lblRFQNo.Text.Trim().ToUpper() + ", ";
            //                    }

            //                    if (ibDisapproved.ImageUrl == "~/images/DA2.png")
            //                    {
            //                        query1 += "UPDATE Request_Status SET ProdManager = 2 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
            //                        query2 += "INSERT INTO Request_HistoryOfDisapproval (RFQNo,Cause,TransactionName,DisapprovedBy,DisapprovedDate,IsActive) VALUES ('" + lblRFQNo.Text.Trim() +
            //                                  "','" + txtRemarks.Text + "','" + ConfigurationManager.AppSettings["ApprovedDisapprovedName-ProdManager"].ToString() +
            //                                  "','" + Session["LcRefId"].ToString() + "',GETDATE(),1) ";

            //                        queryStatusCounter = queryStatusCounter + 2;
            //                        temp_RFQNo += lblRFQNo.Text.Trim().ToUpper() + ", ";
            //                    }

            //                }

            //                querySuccess = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + query2 + queryEndPart).ToString();

            //                if (querySuccess == queryStatusCounter.ToString())
            //                {
            //                    Session["successMessage"] = "RFQ NUMBER(S) : <b>" + temp_RFQNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
            //                    Session["successTransactionName"] = "RFQ_APPROVALFORM";
            //                    Session["successReturnPage"] = "RFQ_ApprovalForm.aspx";

            //                    Response.Redirect("SuccessPage.aspx");
            //                }

            //            }
            //        }

            //    }
            //    //-------------------------------------------------------------------------------------------------------------

            //    if (Session["PurchasingIsApprovingRequest"] != null)
            //    {
            //        if (bool.Parse(Session["PurchasingIsApprovingRequest"].ToString()))
            //        {
            //            if (gvPurchasing.Rows.Count > 0)
            //            {
            //                int incharge = int.Parse(Session["listIncharge"].ToString());
            //                int deptManager = int.Parse(Session["listDeptManager"].ToString());
            //                int divManager = int.Parse(Session["listDivManager"].ToString());
            //                string position = string.Empty;
            //                string transactionName = string.Empty;
            //                bool isSuccess = false;
            //                string failedMessage = string.Empty;

            //                if (ddApprover.SelectedItem.Text == "FOR PROD. MANAGER APPROVAL")
            //                {
            //                    position = "ProdManager";
            //                    transactionName = ConfigurationManager.AppSettings["ApprovedDisapprovedName-ProdManager"].ToString();
            //                }

            //                if (ddApprover.SelectedItem.Text == "FOR BUYER APPROVAL")
            //                {
            //                    // Buyer Approval 
            //                    position = "BUYER";
            //                    transactionName = ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingBuyer"].ToString();
            //                }
            //                if (ddApprover.SelectedItem.Text == "FOR INCHARGE APPROVAL")
            //                {
            //                    // Incharge Approval 
            //                    position = "PURCHASINGINCHARGE";
            //                    transactionName = ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingIncharge"].ToString();
            //                }
            //                if (ddApprover.SelectedItem.Text == "FOR DEPARTMENT MANAGER APPROVAL")
            //                {
            //                    // Department Manager Approval 
            //                    position = "DEPARTMENTMANAGER";
            //                    transactionName = ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingDeptManager"].ToString();
            //                }
            //                if (ddApprover.SelectedItem.Text == "FOR DIVISION MANAGER APPROVAL")
            //                {
            //                    // Division Manager Approval
            //                    position = "DIVISIONMANAGER";
            //                    transactionName = ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingDivManager"].ToString();
            //                }

            //                for (int i = 0; i < gvPurchasing.Rows.Count; i++)
            //                {

            //                    Label lblRFQNo = (Label)gvPurchasing.Rows[i].Cells[0].FindControl("lblRFQNo");
            //                    ImageButton ibApproved = (ImageButton)gvPurchasing.Rows[i].Cells[3].FindControl("ibApproved");
            //                    ImageButton ibDisapproved = (ImageButton)gvPurchasing.Rows[i].Cells[3].FindControl("ibDisapproved");
            //                    TextBox txtRemarks = (TextBox)gvPurchasing.Rows[i].Cells[4].FindControl("txtRemarks");
            //                    GridView gvResponse = (GridView)gvPurchasing.Rows[i].Cells[5].FindControl("gvResponse");


            //                    if (ibApproved.ImageUrl == "~/images/A2.png")
            //                    {
            //                        if (position == "BUYER")
            //                        {
            //                            query1 += "UPDATE Request_Status SET " + position + " = 1, PURCHASINGINCHARGE = 0, DEPARTMENTMANAGER = 0, DIVISIONMANAGER = 0 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
            //                        }
            //                        else
            //                        {
            //                            query1 += "UPDATE Request_Status SET " + position + " = 1 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
            //                        }

            //                        query2 += "INSERT INTO Request_HistoryOfApproval (RFQNo,TransactionName,ApprovedBy,ApprovedDate,IsActive) VALUES ('" + lblRFQNo.Text.Trim() +
            //                                  "','" + transactionName + "','" + Session["LcRefId"].ToString() + "',GETDATE(),1) ";

            //                        queryStatusCounter = queryStatusCounter + 2;
            //                        temp_RFQNo += lblRFQNo.Text.Trim().ToUpper() + ", ";

            //                        if (gvResponse.Rows.Count > 0)
            //                        {
            //                            int checkCounter = 0;
            //                            int nullResponsePriceCounter = 0;
            //                            string itemDescription = string.Empty;
            //                            string itemDetailsRefId = string.Empty;

            //                            foreach (GridViewRow rowResponse in gvResponse.Rows)
            //                            {
            //                                Label lblResponseRefId = (Label)rowResponse.FindControl("lblResponseRefId");
            //                                Label lblIsGranted = (Label)rowResponse.FindControl("lblIsGranted");
            //                                TextBox txtResponseCurrencyPrice = (TextBox)rowResponse.FindControl("txtResponseCurrencyPrice");
            //                                TextBox txtResponseLeadTime = (TextBox)rowResponse.FindControl("txtResponseLeadTime");
            //                                TextBox txtResponseSupplierRemarks = (TextBox)rowResponse.FindControl("txtResponseSupplierRemarks");
            //                                TextBox txtResponsePurchasingRemarks = (TextBox)rowResponse.FindControl("txtResponsePurchasingRemarks");
            //                                ImageButton ibApprovedResponse = (ImageButton)rowResponse.FindControl("ibApprovedResponse");
            //                                Label lblResponseDescription = (Label)rowResponse.FindControl("lblResponseDescription");
            //                                Label lblResponseSpecs = (Label)rowResponse.FindControl("lblResponseSpecs");
            //                                Label lblDetailsRefId = (Label)rowResponse.FindControl("lblDetailsRefId");
            //                                DropDownList ddResponseCurrency = (DropDownList)rowResponse.FindControl("ddResponseCurrency");


            //                                if (ibApprovedResponse.ImageUrl == "~/images/A2.png")
            //                                {
            //                                    query3 += "UPDATE Supplier_Response SET IsGranted = 1, ResponsePrice = '" + txtResponseCurrencyPrice.Text.Replace("'", "''").ToString() +
            //                                              "', ResponseLead = '" + txtResponseLeadTime.Text.Replace("'", "''").ToString() + "', Remarks = '" + txtResponseSupplierRemarks.Text.Replace("'", "''").ToString() +
            //                                              "', RCurrency = '" + ddResponseCurrency.SelectedValue + "' WHERE RefId = '" + lblResponseRefId.Text.Trim() + "' ";

            //                                    if (!string.IsNullOrEmpty(txtResponsePurchasingRemarks.Text))
            //                                    {
            //                                        query4 += "INSERT INTO Supplier_Remarks (ResponseRefId, Remarks) VALUES ('" + lblResponseRefId.Text.Trim() + "','" + txtResponsePurchasingRemarks.Text + "') ";
            //                                        queryStatusCounter++;
            //                                    }

            //                                    itemDescription += lblResponseDescription.Text.Replace(",", "").Trim() + lblResponseSpecs.Text.Replace(",", "").Trim() + ",";
            //                                    itemDetailsRefId += lblDetailsRefId.Text.Replace(",", "").Trim() + ",";
            //                                    checkCounter++;
            //                                }

            //                                if (ibApprovedResponse.ImageUrl == "~/images/A1.png")
            //                                {
            //                                    query3 += "UPDATE Supplier_Response SET IsGranted = 0, ResponsePrice = '" + txtResponseCurrencyPrice.Text.Replace("'", "''").ToString() +
            //                                              "', ResponseLead = '" + txtResponseLeadTime.Text.Replace("'", "''").ToString() + "', Remarks = '" + txtResponseSupplierRemarks.Text.Replace("'", "''").ToString() +
            //                                              "', RCurrency = '" + ddResponseCurrency.SelectedValue + "' WHERE RefId = '" + lblResponseRefId.Text.Trim() + "' ";

            //                                    if (!string.IsNullOrEmpty(txtResponsePurchasingRemarks.Text))
            //                                    {
            //                                        query4 += "INSERT INTO Supplier_Remarks (ResponseRefId, Remarks) VALUES ('" + lblResponseRefId.Text.Trim() + "','" + txtResponsePurchasingRemarks.Text + "') ";
            //                                        queryStatusCounter++;
            //                                    }
            //                                }

            //                                if (string.IsNullOrEmpty(txtResponseCurrencyPrice.Text))
            //                                {
            //                                    nullResponsePriceCounter++;
            //                                }

            //                                queryStatusCounter++;

            //                            }

            //                            if (checkCounter > 0)
            //                            {
            //                                List<string> completeItems = new List<string>(itemDescription.Trim().Split(',').Select(t => t.Trim()));
            //                                bool isUnique = completeItems.Count == completeItems.Distinct().Count();

            //                                List<string> completeDetailsRefId = new List<string>(itemDetailsRefId.Trim().Split(',').Select(t => t.Trim()));

            //                                if (isUnique)
            //                                {
            //                                    isSuccess = true;
            //                                }
            //                                else // has duplicate
            //                                {
            //                                    isSuccess = false;
            //                                    failedMessage += "Please check your items. System detected duplicate selection in " + lblRFQNo.Text.Trim() + "<br/>";
            //                                }
            //                            }
            //                            else
            //                            {
            //                                isSuccess = false;
            //                                failedMessage += "You cannot approved empty items. Please select items you want to approve in RFQNo " + lblRFQNo.Text.Trim();
            //                            }

            //                            if (nullResponsePriceCounter > 0)
            //                            {
            //                                isSuccess = false;
            //                                failedMessage += "Please check " + lblRFQNo.Text.Trim().ToUpper() + " - You have empty or null price in the list!";
            //                            }

            //                        }
            //                    }

            //                    if (ibDisapproved.ImageUrl == "~/images/DA2.png")
            //                    {
            //                        if (position == "BUYER")
            //                        {
            //                            query1 += "UPDATE Request_Status SET " + position + " = 2 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
            //                        }
            //                        else
            //                        {
            //                            query1 += "UPDATE Request_Status SET " + position + " = 2, BUYER = 0 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
            //                        }
            //                        query2 += "INSERT INTO Request_HistoryOfDisapproval (RFQNo,Cause,TransactionName,DisapprovedBy,DisapprovedDate,IsActive) VALUES ('" + lblRFQNo.Text.Trim() +
            //                                  "','" + txtRemarks.Text + "','" + transactionName + "','" + Session["LcRefId"].ToString() + "',GETDATE(),1) ";

            //                        queryStatusCounter = queryStatusCounter + 2;
            //                        temp_RFQNo += lblRFQNo.Text.Trim().ToUpper() + ", ";
            //                    }


            //                }

            //                if (string.IsNullOrEmpty(failedMessage))
            //                {
            //                    if (!string.IsNullOrEmpty(query4))
            //                    {
            //                        querySuccess = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + query2 + query3 + query4 + queryEndPart).ToString();
            //                    }
            //                    else
            //                    {
            //                        querySuccess = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + query2 + query3 + queryEndPart).ToString();
            //                    }

            //                    if (querySuccess == queryStatusCounter.ToString())
            //                    {
            //                        Session["successMessage"] = "RFQ NUMBER(S) : <b>" + temp_RFQNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
            //                        Session["successTransactionName"] = "RFQ_APPROVALFORM";
            //                        Session["successReturnPage"] = "RFQ_ApprovalForm.aspx";

            //                        Response.Redirect("SuccessPage.aspx");
            //                    }
            //                }
            //                else
            //                {
            //                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + failedMessage + "');", true);
            //                }


            //            }
            //        }


            //    }

            //    //-------------------------------------------------------------------------------------------------------------

            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            //}
        }


        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                Label lblRFQNo = row.FindControl("lblRFQNo") as Label;
                Label lblStatAll = row.FindControl("lblStatAll") as Label;
                Label lblCategory = row.FindControl("lblCategory") as Label;
                Label lblRequester = row.FindControl("lblRequester") as Label;
                Label lblTransactionDate = row.FindControl("lblTransactionDate") as Label;
                ImageButton ibPreview = row.FindControl("ibPreview") as ImageButton;
                ImageButton ibApproved = row.FindControl("ibApproved") as ImageButton;
                ImageButton ibDisapproved = row.FindControl("ibDisapproved") as ImageButton;
                TextBox txtRemarks = row.FindControl("txtRemarks") as TextBox;

                if (e.CommandName == "Preview_Command")
                {
                    //string URL = "~/RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(lblRFQNo.Text.Trim(), true);

                    Session["Requester_From_Inquiry"] = lblRequester.Text;
                    Session["TransDate_From_Inquiry"] = lblTransactionDate.Text;
                    Session["Category_From_Inquiry"] = lblCategory.Text.Trim().ToUpper();
                    Session["btnPreview_Visibility"] = lblStatAll.Text == "APPROVED" ? "true" : "false";
                    Session["btnUpdate_Visibility"] = "true";

                    //URL = Page.ResolveClientUrl(URL);
                    //ibPreview.OnClientClick = "window.open('" + URL + "'); return false;";

                    Response.Redirect("RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(lblRFQNo.Text.Trim(), true), false);
                }

                if (e.CommandName == "A_Command")
                {
                    if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                    {
                    }
                    else
                    {
                        if (ibApproved.ImageUrl == "~/images/A1.png")
                        {
                            ibApproved.ImageUrl = "~/images/A2.png";
                        }
                        else
                        {
                            ibApproved.ImageUrl = "~/images/A1.png";
                        }
                    }
                }

                if (e.CommandName == "DA_Command")
                {
                    if (ibApproved.ImageUrl == "~/images/A2.png")
                    {
                        ibDisapproved.ImageUrl = "~/images/DA1.png";
                        txtRemarks.Text = string.Empty;
                        txtRemarks.Enabled = false;
                    }
                    else
                    {
                        if (ibDisapproved.ImageUrl == "~/images/DA1.png")
                        {
                            ibDisapproved.ImageUrl = "~/images/DA2.png";
                            txtRemarks.Enabled = true;
                        }
                        else
                        {
                            ibDisapproved.ImageUrl = "~/images/DA1.png";
                            txtRemarks.Text = string.Empty;
                            txtRemarks.Enabled = false;
                        }
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
                    //Label lblRFQNo = (Label)e.Row.FindControl("lblRFQNo");
                    //GridView gvResponse = e.Row.FindControl("gvResponse") as GridView;

                    //gvResponse.DataSource = BLL.RFQ_TRANSACTION_GetSupplierResponseByRFQNo(lblRFQNo.Text.Trim());
                    //gvResponse.DataBind();

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

        protected void gvResponse_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                ImageButton ibApprovedResponse = row.FindControl("ibApprovedResponse") as ImageButton;

                if (e.CommandName == "AResponse_Command")
                {
                    if (ibApprovedResponse.ImageUrl == "~/images/A1.png")
                    {
                        ibApprovedResponse.ImageUrl = "~/images/A2.png";
                    }
                    else
                    {
                        ibApprovedResponse.ImageUrl = "~/images/A1.png";
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvPurchasing_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblRFQNo = (Label)e.Row.FindControl("lblRFQNo");
                    DropDownList ddSendDates = (DropDownList)e.Row.FindControl("ddSendDates");
                    //GridView gvResponse = e.Row.FindControl("gvResponse") as GridView;                    

                    //gvResponse.DataSource = BLL.RFQ_TRANSACTION_GetSupplierResponseByRFQNo(lblRFQNo.Text.Trim());
                    //gvResponse.DataBind();

                    //---------------------------------------------------------------------------------------------------
                    List<Entities_RFQ_RequestEntry> List_SendDates = new List<Entities_RFQ_RequestEntry>();
                    List_SendDates = BLL.RFQ_TRANSACTION_SendDate_ByRFQNo(lblRFQNo.Text.Trim());

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

                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvPurchasing_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                Label lblRFQNo = row.FindControl("lblRFQNo") as Label;
                ImageButton ibPreview = row.FindControl("ibPreview") as ImageButton;
                ImageButton ibApproved = row.FindControl("ibApproved") as ImageButton;
                ImageButton ibDisapproved = row.FindControl("ibDisapproved") as ImageButton;
                TextBox txtRemarks = row.FindControl("txtRemarks") as TextBox;
                GridView gvResponse = row.FindControl("gvResponse") as GridView;
                Label lblStatAll = row.FindControl("lblStatAll") as Label;
                Label lblCategory = row.FindControl("lblCategory") as Label;
                Label lblRequester = row.FindControl("lblRequester") as Label;
                Label lblTransactionDate = row.FindControl("lblTransactionDate") as Label;


                if (e.CommandName == "Preview_Command")
                {
                    //string URL = "~/RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(lblRFQNo.Text.Trim(), true);

                    Session["Requester_From_Inquiry"] = lblRequester.Text;
                    Session["TransDate_From_Inquiry"] = lblTransactionDate.Text;
                    Session["Category_From_Inquiry"] = lblCategory.Text.Trim().ToUpper();
                    Session["btnPreview_Visibility"] = lblStatAll.Text == "APPROVED" ? "true" : "false";
                    Session["btnUpdate_Visibility"] = "true";
                    Session["divSupplier_Visibility"] = "true";

                    //URL = Page.ResolveClientUrl(URL);
                    //ibPreview.OnClientClick = "window.open('" + URL + "'); return false;";

                    Response.Redirect("RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(lblRFQNo.Text.Trim(), true), false);
                }

                if (e.CommandName == "A_Command")
                {
                    if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                    {
                    }
                    else
                    {

                        if (ibApproved.ImageUrl == "~/images/A1.png")
                        {
                            gvResponse.Visible = true;
                            ibApproved.ImageUrl = "~/images/A2.png";

                            List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                            list = BLL.RFQ_TRANSACTION_GetSupplierResponseByRFQNo(lblRFQNo.Text.Trim());

                            if (list != null)
                            {
                                if (list.Count > 0)
                                {
                                    gvResponse.DataSource = list;
                                    gvResponse.DataBind();
                                }
                            }
                            else
                            {
                                gvResponse.EmptyDataText = "NO SUPPLIER RESPONSE";
                            }

                        }
                        else
                        {
                            gvResponse.Visible = false;
                            ibApproved.ImageUrl = "~/images/A1.png";
                        }
                    }
                }

                if (e.CommandName == "DA_Command")
                {
                    if (ibApproved.ImageUrl == "~/images/A2.png")
                    {
                        ibDisapproved.ImageUrl = "~/images/DA1.png";
                        txtRemarks.Text = string.Empty;
                        txtRemarks.Enabled = false;
                    }
                    else
                    {
                        if (ibDisapproved.ImageUrl == "~/images/DA1.png")
                        {
                            ibDisapproved.ImageUrl = "~/images/DA2.png";
                            txtRemarks.Enabled = true;
                        }
                        else
                        {
                            ibDisapproved.ImageUrl = "~/images/DA1.png";
                            txtRemarks.Text = string.Empty;
                            txtRemarks.Enabled = false;
                        }
                    }
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


        protected void gvRelatedSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                LinkButton linkRFQNo = row.FindControl("linkRFQNo") as LinkButton;
                Label lblRequester = row.FindControl("lblRequester") as Label;
                Label lblTransactionDate = row.FindControl("lblTransactionDate") as Label;
                Label lblCategoryName = row.FindControl("lblCategoryName") as Label;
                Label lblStatDivManager = row.FindControl("lblStatDivManager") as Label;


                if (e.CommandName == "linkRFQNo_Command")
                {

                    Session["Requester_From_Inquiry"] = lblRequester.Text;
                    Session["TransDate_From_Inquiry"] = lblTransactionDate.Text;
                    Session["Category_From_Inquiry"] = lblCategoryName.Text.Trim().ToUpper();
                    Session["btnPreview_Visibility"] = lblStatDivManager.Text == "1" ? "true" : "false";
                    Session["btnUpdate_Visibility"] = "false";
                    Session["divSupplier_Visibility"] = "true";

                    Response.Redirect("RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(linkRFQNo.Text.Trim(), true), false);

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvRelatedSearch_OnRowDataBound(object sender, GridViewRowEventArgs e)
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



        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {

            try
            {
                if (!System.IO.File.Exists(Server.MapPath("~/RFQ_XLS/" + Session["LcRefId"].ToString() + "_NoResponse_Report.xlsx")))
                {
                    System.IO.File.Copy(Server.MapPath("~/RFQ_XLS/RFQ_NoResponse_Report.xlsx"), Server.MapPath("~/RFQ_XLS/" + Session["LcRefId"].ToString() + "_NoResponse_Report.xlsx"));
                }
                else
                {
                    System.IO.File.Delete(Server.MapPath("~/RFQ_XLS/" + Session["LcRefId"].ToString() + "_NoResponse_Report.xlsx"));
                    System.IO.File.Copy(Server.MapPath("~/RFQ_XLS/RFQ_NoResponse_Report.xlsx"), Server.MapPath("~/RFQ_XLS/" + Session["LcRefId"].ToString() + "_NoResponse_Report.xlsx"));
                }


                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                List<Entities_RFQ_RequestEntry> listExport = new List<Entities_RFQ_RequestEntry>();
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();
                entity.SearchCriteria = txtRFQNo.Text;

                // FOR PRODUCTION MANAGER APPROVAL - If requester is from purchasing so need to approvad by purchasing manager as prod manager
                if (ddApprover.SelectedItem.Text == "FOR PROD. MANAGER APPROVAL")
                {
                    if (ddCategory.SelectedItem.Text != "ALL CATEGORY")
                    {
                        listExport = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.ProdManagerStatus == "0" && itm.RhDepartment == Session["Department"].ToString() && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                    }
                    else
                    {
                        listExport = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.ProdManagerStatus == "0" && itm.RhDepartment == Session["Department"].ToString() && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                    }
                }
                // FOR BUYER APPROVAL
                if (ddApprover.SelectedItem.Text == "FOR BUYER APPROVAL")
                {
                    if (ddCategory.SelectedItem.Text != "ALL CATEGORY")
                    {
                        listExport = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.PurchasingStatus == "1" && itm.BuyerStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                    }
                    else
                    {
                        listExport = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.PurchasingStatus == "1" && itm.BuyerStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                    }
                }
                // FOR INCHARGE APPROVAL
                if (ddApprover.SelectedItem.Text == "FOR INCHARGE APPROVAL")
                {
                    if (ddCategory.SelectedItem.Text != "ALL CATEGORY")
                    {
                        listExport = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                    }
                    else
                    {
                        //list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.GroupBySupplierResponse == "YES" && itm.RhCategory.Trim().Equals(incharge_category) ).ToList();

                        if (Session["Username"].ToString() == "3844" || Session["Username"].ToString() == "6985")
                        {
                            //SIR VAL
                            if (Session["Username"].ToString() == "3844")
                            {
                                listExport = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0" && (itm.RhCategory.Trim() == "1009" || itm.RhCategory.Trim() == "1" || itm.RhCategory.Trim() == "1007" || itm.RhCategory.Trim() == "1014" || itm.RhCategory == "1013" || itm.RhCategory.Trim() == "1006" || itm.RhCategory.Trim() == "1010" || itm.RhCategory.Trim() == "7" || itm.RhCategory.Trim() == "2" || itm.RhCategory.Trim() == "1008")).ToList();
                            }
                            //SIR RUDY
                            if (Session["Username"].ToString() == "6985")
                            {
                                listExport = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0" && itm.RhCategory.Trim() == "3").ToList();
                            }
                        }
                        else
                        {
                            listExport = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                        }
                    }
                }
                // FOR DEPARTMENT MANAGER APPROVAL
                if (ddApprover.SelectedItem.Text == "FOR DEPARTMENT MANAGER APPROVAL")
                {
                    if (ddCategory.SelectedItem.Text != "ALL CATEGORY")
                    {
                        listExport = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.PurchasingInchargeStatus == "1" && itm.DepartmentManagerStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                    }
                    else
                    {
                        listExport = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.PurchasingInchargeStatus == "1" && itm.DepartmentManagerStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                    }
                }
                // FOR DIVISION MANAGER APPROVAL
                if (ddApprover.SelectedItem.Text == "FOR DIVISION MANAGER APPROVAL")
                {
                    if (ddCategory.SelectedItem.Text != "ALL CATEGORY")
                    {
                        listExport = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.DepartmentManagerStatus == "1" && itm.DivisionManagerStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                    }
                    else
                    {
                        listExport = BLL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity).Where(itm => itm.DepartmentManagerStatus == "1" && itm.DivisionManagerStatus == "0" && itm.NumberOfSuppliers_WithResponse == "0").ToList();
                    }
                }

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



                if (listExport != null)
                {
                    if (listExport.Count > 0)
                    {
                        //string category = Session["CategoryAccess"].ToString();
                        int cnt = 4;
                        string path = Server.MapPath("~/RFQ_XLS/" + Session["LcRefId"].ToString() + "_NoResponse_Report.xlsx");
                        Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + path);
                        FileStream fsBI = new FileStream(path, FileMode.Open);
                        using (SLDocument draft = new SLDocument(fsBI, "Sheet1"))
                        {

                            foreach (Entities_RFQ_RequestEntry entityExport in listExport)
                            {
                                draft.SetCellValue("A" + cnt.ToString(), entityExport.Rfqno);
                                draft.SetCellValue("B" + cnt.ToString(), entityExport.CategoryName);
                                draft.SetCellValue("C" + cnt.ToString(), entityExport.RhRequester);
                                draft.SetCellValue("D" + cnt.ToString(), entityExport.RdRemarks);
                                draft.SetCellValue("E" + cnt.ToString(), entityExport.RdMaker);
                                draft.SetCellValue("F" + cnt.ToString(), entityExport.RhProdManagerApprovedDate);  

                                cnt++;
                            }

                            fsBI.Close();
                            draft.SaveAs(path);

                        }

                        Response.Redirect("RFQ_XLS/" + Session["LcRefId"].ToString() + "_NoResponse_Report.xlsx", false);

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
