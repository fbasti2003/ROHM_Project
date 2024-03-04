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
using System.Text;

namespace REPI_PUR_SOFRA
{
    public partial class RFQ_ApprovalForm : System.Web.UI.Page
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
                        listCategory = BLL.RFQ_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

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

                                if (Session["APPROVAL_OTHER_BUYERS"] != null)
                                {
                                    if (!string.IsNullOrEmpty(Session["APPROVAL_OTHER_BUYERS"].ToString()))
                                    {
                                        ddCategory.Enabled = true;
                                        ddCategory.Items.FindByValue(Session["APPROVAL_OTHER_BUYERS"].ToString()).Selected = true;
                                    }
                                }
                                else
                                {
                                    ddCategory.Items.FindByValue(category).Selected = true;
                                    ddCategory.Enabled = false;
                                }
                            }
                            else
                            {
                                ddCategory.Items.FindByText("ALL CATEGORY").Selected = true;
                                ddCategory.Enabled = true;
                            }
                        }

                        //---------------------------------------------------------------------------------------------------
                        
                        if (Session["Username"].ToString() == "3844")
                        {
                            // Incharge Approval SIR VAL
                            //11/21/2022 - Updated Sir Val account from SecMan to DeptMan
                            //ddApprover.Items.FindByValue("2").Selected = true;
                            ddApprover.Items.FindByValue("3").Selected = true;
                        }
                        else if (Session["Username"].ToString() == "04370-2" || Session["Username"].ToString() == "06986-3")
                        {
                            ddApprover.Items.FindByValue("2").Selected = true;
                            //ddApprover.Enabled = false;
                        }
                        else if (Session["Username"].ToString() == "6985")
                        {
                            // Incharge Approval SIR RUDY
                            ddApprover.Items.FindByValue("1").Selected = true;
                        }
                        else if (Session["Username"].ToString() == "7505")
                        {
                            // Incharge Approval SIR RENZ
                            ddApprover.Items.FindByValue("1").Selected = true;
                        }
                        else if (Session["Username"].ToString() == "0286")
                        {
                            // Incharge Approval MAM SYLVIA CARAVANA
                            ddApprover.Items.FindByValue("1").Selected = true;
                            ddApprover.Enabled = true;
                        }
                        else if (Session["Username"].ToString() == "1402")
                        {
                            //Department Manager Approval
                            ddApprover.Items.FindByValue("3").Selected = true;
                        }
                        else if (Session["Username"].ToString() == "1152")
                        {
                            // Department Manager Approval 
                            // 11/21/2022 - Updated Mam Sally Account from DeptMan to DivMan
                            //ddApprover.Items.FindByValue("3").Selected = true;
                            ddApprover.Items.FindByValue("4").Selected = true;
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

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("RFQ_ApprovalForm.aspx");
            }
            catch (Exception ex)
            {
                //throw ex;
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtRFQNo.Text))
                {
                    Session["Original_FromApprovalForm"] = txtRFQNo.Text;
                    Response.Redirect("RFQ_AllRequest.aspx");
                }
                else
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
                        btnRefresh.Visible = false;

                        if (!string.IsNullOrEmpty(txtRFQNo.Text))
                        {
                            list = BLL.RFQ_TRANSACTION_Approval(entity).Where(itm => itm.RhDepartment == Session["Department"].ToString() && itm.ProdManagerStatus == "0" && itm.Rfqno.Contains(txtRFQNo.Text)).ToList();
                        }
                        else
                        {
                            if (Session["Username"].ToString() == "09076")
                            {
                                // IF DEPARTMENT MANAGER
                                if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), "402").ToString().ToLower() == "true")
                                {
                                    list = BLL.RFQ_TRANSACTION_Approval(entity).Where(itm => itm.RhDepartment == Session["Department"].ToString() && itm.ProdManagerStatus == "0").ToList();
                                }
                                // IF DIVISION MANAGER
                                if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), "403").ToString().ToLower() == "true")
                                {
                                    list = BLL.RFQ_TRANSACTION_Approval(entity).Where(itm => itm.RhDivision == Session["Division"].ToString() && itm.ProdManagerStatus == "0").ToList();
                                }
                            }
                            else
                            {
                                list = BLL.RFQ_TRANSACTION_Approval(entity).Where(itm => itm.RhDepartment == Session["Department"].ToString() && itm.ProdManagerStatus == "0").ToList();
                            }
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
                        btnRefresh.Visible = true;

                        if (Request.QueryString["AF_RFQNo"] != null)
                        {
                            if (!string.IsNullOrEmpty(Request.QueryString["AF_RFQNo"].ToString()))
                            {
                                entity.SearchCriteria = Request.QueryString["AF_RFQNo"].ToString();
                            }
                        }
                        else
                        {
                            entity.SearchCriteria = txtRFQNo.Text;
                        }


                        // FOR PRODUCTION MANAGER APPROVAL - If requester is from purchasing so need to approvad by purchasing manager as prod manager
                        if (ddApprover.SelectedItem.Text == "FOR PROD. MANAGER APPROVAL")
                        {
                            Session["ProdManagerIsApprovingRequest"] = "true";
                            Session["PurchasingIsApprovingRequest"] = "false";

                            if (ddCategory.SelectedItem.Text != "ALL CATEGORY")
                            {
                                list = BLL.RFQ_TRANSACTION_Approval(entity).Where(itm => itm.RhDepartment == Session["Department"].ToString() && itm.ProdManagerStatus == "0" && itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim()).ToList();
                                //list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.ProdManagerStatus == "0" && itm.RhDepartment == Session["Department"].ToString()).ToList();
                            }
                            else
                            {
                                list = BLL.RFQ_TRANSACTION_Approval(entity).Where(itm => itm.RhDepartment == Session["Department"].ToString() && itm.ProdManagerStatus == "0").ToList();
                                //list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.ProdManagerStatus == "0" && itm.RhDepartment == Session["Department"].ToString()).ToList();
                            }
                        }
                        // FOR BUYER APPROVAL
                        if (ddApprover.SelectedItem.Text == "FOR BUYER APPROVAL")
                        {
                            if (ddCategory.SelectedItem.Text != "ALL CATEGORY")
                            {
                                list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.PurchasingStatus == "1" && itm.BuyerStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList().OrderByDescending(itm => int.Parse(itm.DateReceivedFromSupplier)).ToList();
                            }
                            else
                            {
                                list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.PurchasingStatus == "1" && itm.BuyerStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList().OrderByDescending(itm => int.Parse(itm.DateReceivedFromSupplier)).ToList();
                            }
                        }
                        // FOR INCHARGE APPROVAL
                        if (ddApprover.SelectedItem.Text == "FOR INCHARGE APPROVAL")
                        {
                            if (ddCategory.SelectedItem.Text != "ALL CATEGORY")
                            {
                                list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                            }
                            else
                            {
                                //list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.GroupBySupplierResponse == "YES" && itm.RhCategory.Trim().Equals(incharge_category) ).ToList();

                                if (Session["Username"].ToString() == "3844" || Session["Username"].ToString() == "6985" || Session["Username"].ToString() == "0286" || Session["Username"].ToString() == "4370")
                                {
                                    //SIR VAL
                                    if (Session["Username"].ToString() == "3844")
                                    {
                                        list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.GroupBySupplierResponse == "YES" && (itm.RhCategory.Trim() == "1009" || itm.RhCategory.Trim() == "1" || itm.RhCategory.Trim() == "1007" || itm.RhCategory.Trim() == "1014" || itm.RhCategory == "1013" || itm.RhCategory.Trim() == "1006" || itm.RhCategory.Trim() == "1010" || itm.RhCategory.Trim() == "7" || itm.RhCategory.Trim() == "2" || itm.RhCategory.Trim() == "1008" || itm.RhCategory.Trim() == "1017" || itm.RhCategory.Trim() == "1018")).ToList();
                                    }
                                    //SIR RUDY
                                    if (Session["Username"].ToString() == "6985")
                                    {
                                        list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.GroupBySupplierResponse == "YES" && itm.RhCategory.Trim() == "3").ToList();
                                    }
                                    //MAM SYLVIA CARAVANA
                                    if (Session["Username"].ToString() == "0286")
                                    {
                                        list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.GroupBySupplierResponse == "YES" && itm.RhCategory.Trim() == "1015").ToList();
                                    }
                                    //MAM IMELDA LIMON
                                    if (Session["Username"].ToString() == "4370")
                                    {
                                        list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.GroupBySupplierResponse == "YES" && itm.RhCategory.Trim() == "9").ToList();
                                    }
                                }
                                else
                                {
                                    list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                                }
                            }
                        }
                        // FOR DEPARTMENT MANAGER APPROVAL
                        if (ddApprover.SelectedItem.Text == "FOR DEPARTMENT MANAGER APPROVAL")
                        {
                            if (ddCategory.SelectedItem.Text != "ALL CATEGORY")
                            {
                                list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.PurchasingInchargeStatus == "1" && itm.DepartmentManagerStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                            }
                            else
                            {
                                list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.PurchasingInchargeStatus == "1" && itm.DepartmentManagerStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                            }
                        }
                        // FOR DIVISION MANAGER APPROVAL
                        if (ddApprover.SelectedItem.Text == "FOR DIVISION MANAGER APPROVAL")
                        {
                            if (ddCategory.SelectedItem.Text != "ALL CATEGORY")
                            {
                                list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.DepartmentManagerStatus == "1" && itm.DivisionManagerStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                            }
                            else
                            {
                                list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.DepartmentManagerStatus == "1" && itm.DivisionManagerStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                            }
                        }

                        if (list != null)
                        {
                            if (list.Count > 0)
                            {
                                if (ddApprover.SelectedItem.Text == "FOR PROD. MANAGER APPROVAL")
                                {
                                    gvData.Visible = true;
                                    gvData.DataSource = list;
                                    gvData.DataBind();

                                    gvPurchasing.Visible = false;
                                    gvRelatedSearch.Visible = false;
                                }
                                else
                                {
                                    gvPurchasing.Visible = true;
                                    gvRelatedSearch.Visible = false;
                                    gvPurchasing.DataSource = list;
                                    gvPurchasing.DataBind();

                                    gvData.Visible = false;
                                }
                            }
                            else
                            {
                                gvPurchasing.Visible = false;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('NO RECORDS FOUND BASED ON YOUR SEARCH CRITERIA. PLEASE SEE BELOW RELATED SEARCH RESULT.');", true);

                                List<Entities_RFQ_RequestEntry> listRelatedResults = new List<Entities_RFQ_RequestEntry>();
                                Entities_RFQ_RequestEntry entityRelatedResults = new Entities_RFQ_RequestEntry();

                                listRelatedResults = null;
                                entityRelatedResults.SearchCriteria = txtRFQNo.Text;

                                listRelatedResults = BLL.RFQ_TRANSACTION_AllRequest_ByDateRange(entityRelatedResults);

                                if (listRelatedResults != null)
                                {
                                    if (listRelatedResults.Count > 0)
                                    {
                                        gvRelatedSearch.Visible = true;
                                        gvRelatedSearch.DataSource = listRelatedResults;
                                        gvRelatedSearch.DataBind();
                                    }
                                    else
                                    {
                                        gvRelatedSearch.Visible = false;
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('NO RECORDS FOUND IN RELATED SEARCH!');", true);
                                    }
                                }
                            }
                        }

                    }

                    // ----------------------------------------------------------------------------------------------------------

                }

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
                string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                string query1 = string.Empty;
                string query2 = string.Empty;
                string query3 = string.Empty;
                string query4 = string.Empty;
                int queryStatusCounter = 0;
                string querySuccess = string.Empty;
                string temp_RFQNo = string.Empty;
                string temp_RFQNo_For_Disapproved_Items = string.Empty;

                //-------------------------------------------------------------------------------------------------------------
                if (Session["ProdManagerIsApprovingRequest"] != null)
                {

                    if (bool.Parse(Session["ProdManagerIsApprovingRequest"].ToString()))
                    {
                        if (gvData.Rows.Count > 0)
                        {
                            string requesterIsApprover = string.Empty;

                            for (int i = 0; i < gvData.Rows.Count; i++)
                            {
                                Label lblRFQNo = (Label)gvData.Rows[i].Cells[0].FindControl("lblRFQNo");
                                ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[3].FindControl("ibApproved");
                                ImageButton ibDisapproved = (ImageButton)gvData.Rows[i].Cells[3].FindControl("ibDisapproved");
                                ImageButton ibHold = (ImageButton)gvData.Rows[i].Cells[3].FindControl("ibHold");
                                TextBox txtRemarks = (TextBox)gvData.Rows[i].Cells[4].FindControl("txtRemarks");
                                Label lblRequesterId = (Label)gvData.Rows[i].Cells[0].FindControl("lblRequesterId");

                                if (ibApproved.ImageUrl == "~/images/A2.png")
                                {
                                    query1 += "UPDATE Request_Status SET ProdManager = 1 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
                                    query2 += "INSERT INTO Request_HistoryOfApproval (RFQNo,TransactionName,ApprovedBy,ApprovedDate,IsActive) VALUES ('" + lblRFQNo.Text.Trim() + 
                                              "','" + ConfigurationManager.AppSettings["ApprovedDisapprovedName-ProdManager"].ToString() + 
                                              "','" + Session["LcRefId"].ToString() + "',GETDATE(),1) ";

                                    queryStatusCounter = queryStatusCounter + 2;
                                    temp_RFQNo += lblRFQNo.Text.Trim().ToUpper() + ", ";

                                    // CHECK IF USER IS ALLOWED TO APPROVED HIS/HER OWN REQUEST 
                                    if (lblRequesterId.Text.Trim() == Session["LcRefId"].ToString().Trim())
                                    {
                                        if (COMMON.isUserAllowed(lblRequesterId.Text.Trim(), "45").ToString().ToLower() == "false")
                                        {
                                            requesterIsApprover += lblRFQNo.Text + ", ";
                                        }
                                    }
                                    
                                }

                                if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                                {
                                    query1 += "UPDATE Request_Status SET ProdManager = 2 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
                                    query2 += "INSERT INTO Request_HistoryOfDisapproval (RFQNo,Cause,TransactionName,DisapprovedBy,DisapprovedDate,IsActive) VALUES ('" + lblRFQNo.Text.Trim() +
                                              "','" + txtRemarks.Text + "','" + ConfigurationManager.AppSettings["ApprovedDisapprovedName-ProdManager"].ToString() +
                                              "','" + Session["LcRefId"].ToString() + "',GETDATE(),1) ";

                                    queryStatusCounter = queryStatusCounter + 2;
                                    temp_RFQNo += lblRFQNo.Text.Trim().ToUpper() + ", ";
                                }

                                if (ibHold.ImageUrl == "~/images/hold2.png")
                                {
                                    query1 += "UPDATE Request_Status SET ProdManager = 3 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
                                    query2 += "INSERT INTO Request_Hold_Reason (RFQNo, Reason, CreatedBy, CreatedDate) VALUES ('" + lblRFQNo.Text.Trim() +
                                              "','" + txtRemarks.Text + 
                                              "','" + Session["UserFullName"].ToString() + "',GETDATE()) ";

                                    queryStatusCounter = queryStatusCounter + 2;
                                    temp_RFQNo += lblRFQNo.Text.Trim().ToUpper() + ", ";
                                }


                            }

                            if (!string.IsNullOrEmpty(requesterIsApprover))
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ReminderMessage('Please check RFQ (" + requesterIsApprover + ") because you are not allowed to approve your own request.');", true);
                            }
                            else
                            {

                                querySuccess = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + query2 + queryEndPart).ToString();

                                if (querySuccess == queryStatusCounter.ToString())
                                {
                                    Session["successMessage"] = "RFQ NUMBER(S) : <b>" + temp_RFQNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
                                    Session["successTransactionName"] = "RFQ_APPROVALFORM";
                                    Session["successReturnPage"] = "RFQ_ApprovalForm.aspx";

                                    Response.Redirect("SuccessPage.aspx");
                                }
                            }

                        }
                    }

                }
                //-------------------------------------------------------------------------------------------------------------

                if (Session["PurchasingIsApprovingRequest"] != null)
                {
                    if (bool.Parse(Session["PurchasingIsApprovingRequest"].ToString()))
                    {
                        if (gvPurchasing.Rows.Count > 0)
                        {
                            int incharge = int.Parse(Session["listIncharge"].ToString());
                            int deptManager = int.Parse(Session["listDeptManager"].ToString());
                            int divManager = int.Parse(Session["listDivManager"].ToString());
                            string position = string.Empty;
                            string transactionName = string.Empty;
                            bool isSuccess = false;
                            string failedMessage = string.Empty;

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

                                Label lblRFQNo = (Label)gvPurchasing.Rows[i].Cells[0].FindControl("lblRFQNo");
                                ImageButton ibApproved = (ImageButton)gvPurchasing.Rows[i].Cells[3].FindControl("ibApproved");
                                ImageButton ibDisapproved = (ImageButton)gvPurchasing.Rows[i].Cells[3].FindControl("ibDisapproved");
                                TextBox txtRemarks = (TextBox)gvPurchasing.Rows[i].Cells[4].FindControl("txtRemarks");
                                GridView gvResponse = (GridView)gvPurchasing.Rows[i].Cells[5].FindControl("gvResponse");


                                if (ibApproved.ImageUrl == "~/images/A2.png")
                                {
                                    //if (position == "BUYER")
                                    //{
                                    //    query1 += "UPDATE Request_Status SET " + position + " = 1, PURCHASINGINCHARGE = 1, DEPARTMENTMANAGER = 0, DIVISIONMANAGER = 0 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";

                                    //    // THIS IS FOR AUTO-APPROVED PURCHASING INCHARGE
                                    //    query2 += "INSERT INTO Request_HistoryOfApproval (RFQNo,TransactionName,ApprovedBy,ApprovedDate,IsActive) VALUES ('" + lblRFQNo.Text.Trim() +
                                    //          "','PurchasingIncharge','" + ConfigurationManager.AppSettings["AUTO-APPROVED-ACCOUNT"].ToString() + "',GETDATE(),1) ";
                                    //}
                                    //else
                                    //{
                                    //    query1 += "UPDATE Request_Status SET " + position + " = 1 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
                                    //}

                                    //query1 += "UPDATE Request_Status SET " + position + " = 1 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";

                                    //query2 += "INSERT INTO Request_HistoryOfApproval (RFQNo,TransactionName,ApprovedBy,ApprovedDate,IsActive) VALUES ('" + lblRFQNo.Text.Trim() +
                                    //          "','" + transactionName + "','" + Session["LcRefId"].ToString() + "',GETDATE(),1) ";

                                    //if (position == "BUYER")
                                    //{
                                    //    queryStatusCounter = queryStatusCounter + 3;
                                    //}
                                    //else
                                    //{
                                    //    queryStatusCounter = queryStatusCounter + 2;
                                    //}

                                    //queryStatusCounter = queryStatusCounter + 2;



                                    //DEPARTMENT MANAGER AUTO-APPROVED
                                    if (position == "PURCHASINGINCHARGE")
                                    {
                                        query1 += "UPDATE Request_Status SET " + position + " = 1, DEPARTMENTMANAGER = 1, DIVISIONMANAGER = 0 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";

                                        query2 += "INSERT INTO Request_HistoryOfApproval (RFQNo,TransactionName,ApprovedBy,ApprovedDate,IsActive) VALUES ('" + lblRFQNo.Text.Trim() +
                                                  "','" + transactionName + "','" + Session["LcRefId"].ToString() + "',GETDATE(),1) ";

                                        query2 += "INSERT INTO Request_HistoryOfApproval (RFQNo,TransactionName,ApprovedBy,ApprovedDate,IsActive) VALUES ('" + lblRFQNo.Text.Trim() +
                                                   "','PurchasingDeptManager','" + ConfigurationManager.AppSettings["AUTO-APPROVED-ACCOUNT"].ToString() + "',GETDATE(),1) ";
                                    }
                                    else
                                    {
                                        query1 += "UPDATE Request_Status SET " + position + " = 1 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
                                        query2 += "INSERT INTO Request_HistoryOfApproval (RFQNo,TransactionName,ApprovedBy,ApprovedDate,IsActive) VALUES ('" + lblRFQNo.Text.Trim() +
                                                  "','" + transactionName + "','" + Session["LcRefId"].ToString() + "',GETDATE(),1) ";
                                    }

                                    if (position == "PURCHASINGINCHARGE")
                                    {
                                        queryStatusCounter = queryStatusCounter + 3;
                                    }
                                    else
                                    {
                                        queryStatusCounter = queryStatusCounter + 2;
                                    }

                                    
                                    temp_RFQNo += lblRFQNo.Text.Trim().ToUpper() + ", ";

                                    if (gvResponse.Rows.Count > 0)
                                    {
                                        int checkCounter = 0;
                                        int nullResponsePriceCounter = 0;
                                        string itemDescription = string.Empty;
                                        string itemDetailsRefId = string.Empty;
                                        string itemDetailsRefIdForNotSelected = string.Empty;
                                        int numberOfChecks = 0;

                                        foreach (GridViewRow rowResponse in gvResponse.Rows)
                                        {
                                            Label lblResponseRefId = (Label)rowResponse.FindControl("lblResponseRefId");
                                            Label lblIsGranted = (Label)rowResponse.FindControl("lblIsGranted");
                                            TextBox txtResponseCurrencyPrice = (TextBox)rowResponse.FindControl("txtResponseCurrencyPrice");
                                            TextBox txtResponseLeadTime = (TextBox)rowResponse.FindControl("txtResponseLeadTime");
                                            TextBox txtResponseSupplierRemarks = (TextBox)rowResponse.FindControl("txtResponseSupplierRemarks");
                                            TextBox txtResponsePurchasingRemarks = (TextBox)rowResponse.FindControl("txtResponsePurchasingRemarks");
                                            ImageButton ibApprovedResponse = (ImageButton)rowResponse.FindControl("ibApprovedResponse");
                                            Label lblResponseDescription = (Label)rowResponse.FindControl("lblResponseDescription");
                                            Label lblResponseSpecs = (Label)rowResponse.FindControl("lblResponseSpecs");
                                            Label lblDetailsRefId = (Label)rowResponse.FindControl("lblDetailsRefId");
                                            Label lblResponseSupplier = (Label)rowResponse.FindControl("lblResponseSupplier");
                                            DropDownList ddResponseCurrency = (DropDownList)rowResponse.FindControl("ddResponseCurrency");
                                            

                                            if (ibApprovedResponse.ImageUrl == "~/images/A2.png")
                                            {
                                                query3 += "UPDATE Supplier_Response SET IsGranted = 1, ResponsePrice = '" + txtResponseCurrencyPrice.Text.Replace("'", "''").ToString() +
                                                          "', ResponseLead = '" + txtResponseLeadTime.Text.Replace("'", "''").ToString() + "', Remarks = '" + txtResponseSupplierRemarks.Text.Replace("'", "''").ToString() +
                                                          "', RCurrency = '" + ddResponseCurrency.SelectedValue + "' WHERE RefId = '" + lblResponseRefId.Text.Trim() + "' ";

                                                if (!string.IsNullOrEmpty(txtResponsePurchasingRemarks.Text))
                                                {
                                                    query4 += "INSERT INTO Supplier_Remarks (ResponseRefId, Remarks) VALUES ('" + lblResponseRefId.Text.Trim() + "','" + txtResponsePurchasingRemarks.Text + "') ";
                                                    queryStatusCounter++;
                                                }

                                                itemDescription += lblResponseDescription.Text.Replace(",", "").Trim() + lblResponseSpecs.Text.Replace(",", "").Trim() + ",";                                                
                                                checkCounter++;
                                                numberOfChecks++;
                                            }

                                            if (ibApprovedResponse.ImageUrl == "~/images/A1.png")
                                            {
                                                query3 += "UPDATE Supplier_Response SET IsGranted = 0, ResponsePrice = '" + txtResponseCurrencyPrice.Text.Replace("'", "''").ToString() +
                                                          "', ResponseLead = '" + txtResponseLeadTime.Text.Replace("'", "''").ToString() + "', Remarks = '" + txtResponseSupplierRemarks.Text.Replace("'", "''").ToString() +
                                                          "', RCurrency = '" + ddResponseCurrency.SelectedValue + "' WHERE RefId = '" + lblResponseRefId.Text.Trim() + "' ";

                                                if (!string.IsNullOrEmpty(txtResponsePurchasingRemarks.Text))
                                                {
                                                    query4 += "INSERT INTO Supplier_Remarks (ResponseRefId, Remarks) VALUES ('" + lblResponseRefId.Text.Trim() + "','" + txtResponsePurchasingRemarks.Text + "') ";
                                                    queryStatusCounter++;
                                                }

                                                //itemDescription += lblResponseDescription.Text.Replace(",", "").Trim() + lblResponseSpecs.Text.Replace(",", "").Trim() + ",";
                                                //itemDetailsRefIdForNotSelected += lblDetailsRefId.Text.Replace(",", "").Trim() + lblResponseSupplier.Text.Replace(",", "").Trim() + ",";
                                            }

                                            if (string.IsNullOrEmpty(txtResponseCurrencyPrice.Text))
                                            {
                                                nullResponsePriceCounter++;
                                            }

                                            itemDetailsRefId += lblDetailsRefId.Text.Replace(",", "").Trim() + ",";
                                            queryStatusCounter++;

                                        }

                                        if (checkCounter > 0)
                                        {
                                            List<string> completeItems = new List<string>(itemDescription.Trim().Split(',').Select(t => t.Trim()));
                                            bool isUnique = completeItems.Count == completeItems.Distinct().Count();

                                            List<string> completeDetailsRefId = new List<string>(itemDetailsRefId.Trim().Split(',').Select(t => t.Trim()));
                                            //bool isUniqueDetailsRefId = completeDetailsRefId.Count == completeDetailsRefId.Distinct().Count();

                                            if (isUnique)
                                            {
                                                isSuccess = true;
                                            }
                                            else // has duplicate
                                            {
                                                isSuccess = false;
                                                failedMessage += "Please check your items. System detected duplicate selection in " + lblRFQNo.Text.Trim() + "<br/>";
                                            }
                                            //------------------------------------------------------------------------------------------------------------------------

                                            if (numberOfChecks < completeDetailsRefId.Distinct().Count() - 1)
                                            {
                                                isSuccess = false;
                                                //failedMessage += "numberOfChecks = " + numberOfChecks + " / completeDetailsRefId = " + completeDetailsRefId.Distinct().Count() + " / itemDetailsRefId = " + itemDetailsRefId;
                                                failedMessage += "Please check your items. System detected empty selection in " + lblRFQNo.Text.Trim() + "<br/>";
                                            }
                                            else
                                            {
                                                isSuccess = true;
                                            }
                                            //if (isUniqueDetailsRefId)
                                            //{
                                            //    isSuccess = true;
                                            //}
                                            //else // has duplicate
                                            //{
                                            //    isSuccess = false;
                                            //    failedMessage += "Please check your items. System detected empty selection in " + lblRFQNo.Text.Trim() + "<br/>";
                                            //}
                                        }
                                        else
                                        {
                                            isSuccess = false;
                                            failedMessage += "You cannot approved empty items. Please select items you want to approve in RFQNo " + lblRFQNo.Text.Trim();
                                        }

                                        if (nullResponsePriceCounter > 0)
                                        {
                                            isSuccess = false;
                                            failedMessage += "Please check " + lblRFQNo.Text.Trim().ToUpper() + " - You have empty or null price in the list!";
                                        }

                                    }
                                }

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
                                    temp_RFQNo_For_Disapproved_Items += lblRFQNo.Text.Trim().ToUpper() + ", ";

                                    if (string.IsNullOrEmpty(txtRemarks.Text) || COMMON.IsNullOrWhiteSpace(txtRemarks.Text))
                                    {
                                        isSuccess = false;
                                        failedMessage += "Please enter a valid REMARKS/CAUSE/NOTE for RFQ (" + lblRFQNo.Text.Trim() + ")"; 
                                    }
                                }


                            }
                            
                            if (string.IsNullOrEmpty(failedMessage))
                            {
                                if (!string.IsNullOrEmpty(query4))
                                {
                                    querySuccess = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + query2 + query3 + query4 + queryEndPart).ToString();
                                }
                                else
                                {
                                    querySuccess = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + query2 + query3 + queryEndPart).ToString();
                                }

                                if (querySuccess == queryStatusCounter.ToString())
                                {
                                    //-------------------------------------------------------------------------------------------------------------------------
                                    // If quotation if approved by Division Manager then send email notification to Requester
                                    if (ddApprover.SelectedItem.Text == "FOR DIVISION MANAGER APPROVAL")
                                    {
                                        List<string> completeItemsReadyForEmail = new List<string>(temp_RFQNo.Trim().Split(',').Select(t => t.Trim()));

                                        foreach (string rfqNoForEmail in completeItemsReadyForEmail)
                                        {

                                            List<Entities_RFQ_RequestEntry> isAlreadyApproved = new List<Entities_RFQ_RequestEntry>();
                                            isAlreadyApproved = BLL.RFQ_TRANSACTION_IsAlready_Approved_ByRFQNo(rfqNoForEmail.Trim());

                                            if (isAlreadyApproved != null)
                                            {
                                                if (isAlreadyApproved.Count > 0)
                                                {
                                                    Entities_RFQ_RequestEntry eForEmail = new Entities_RFQ_RequestEntry();
                                                    eForEmail.RhRfqNo = rfqNoForEmail;

                                                    List<Entities_RFQ_RequestEntry> email = BLL.RFQ_TRANSACTION_GetRequesterEmailAndLocalNumber_ByRFQNo(eForEmail);
                                                    if (email != null)
                                                    {
                                                        if (email.Count > 0)
                                                        {
                                                            foreach (Entities_RFQ_RequestEntry eEmail in email)
                                                            {
                                                                if (!string.IsNullOrEmpty(eEmail.RhEmailAddress))
                                                                {
                                                                    string body = "RFQ - <b>" + rfqNoForEmail + "</b> has been successfully approved.<br/><br/>RFQS Notification! Please do not reply!";
                                                                    string emailRequester = COMMON.sendEmailToRequester(eEmail.RhEmailAddress, ConfigurationManager.AppSettings["email-username"], "RFQ - (" + rfqNoForEmail.Trim() + ") APPROVED NOTIFICATION", body);

                                                                    if (emailRequester.ToLower().Contains("success"))
                                                                    {
                                                                        // do nothing
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                            }

                                        }
                                    }

                                    if (ddApprover.SelectedItem.Text == "FOR BUYER APPROVAL")
                                    {
                                        //---------------------------------------------------------------------------------------------------------------------
                                        //EMAIL NOTIFICATION FOR DISAPPROVED ITEMS
                                        List<string> disapprovedItemsReadyForEmail = new List<string>(temp_RFQNo_For_Disapproved_Items.Trim().Split(',').Select(t => t.Trim()));

                                        if (disapprovedItemsReadyForEmail != null)
                                        {
                                            if (disapprovedItemsReadyForEmail.Count > 0)
                                            {
                                                foreach (string disapprovedRfqNoForEmail in disapprovedItemsReadyForEmail)
                                                {
                                                    Entities_RFQ_RequestEntry eForEmail_Disapproved = new Entities_RFQ_RequestEntry();
                                                    eForEmail_Disapproved.RhRfqNo = disapprovedRfqNoForEmail;

                                                    List<Entities_RFQ_RequestEntry> disapprovedEmail = BLL.RFQ_TRANSACTION_GetRequesterEmailAndLocalNumber_ByRFQNo(eForEmail_Disapproved);
                                                    if (disapprovedEmail != null)
                                                    {
                                                        if (disapprovedEmail.Count > 0)
                                                        {
                                                            foreach (Entities_RFQ_RequestEntry eEmailDisapproved in disapprovedEmail)
                                                            {
                                                                if (!string.IsNullOrEmpty(eEmailDisapproved.RhEmailAddress))
                                                                {
                                                                    string body = "RFQ - <b>" + disapprovedRfqNoForEmail + "</b> has been <b><h1 style='background-color:Red'>DISAPPROVED</h1><br/><br/>RFQS-DISAPPROVED Notification! Please do not reply!";
                                                                    string emailRequester = COMMON.sendEmailToRequester(eEmailDisapproved.RhEmailAddress, ConfigurationManager.AppSettings["email-username"], "RFQ - (" + disapprovedRfqNoForEmail.Trim() + ") DISAPPROVED NOTIFICATION", body);

                                                                    if (emailRequester.ToLower().Contains("success"))
                                                                    {
                                                                        // do nothing
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }

                                                }
                                            }
                                        }

                                        //---------------------------------------------------------------------------------------------------------------------

                                    }

                                    Session["successMessage"] = "RFQ NUMBER(S) : <b>" + temp_RFQNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
                                    Session["successTransactionName"] = "RFQ_APPROVALFORM";
                                    Session["successReturnPage"] = "RFQ_ApprovalForm.aspx";

                                    Response.Redirect("SuccessPage.aspx");
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + failedMessage + "');", true);
                            }

                            ////FOR TESTING (TATANGGALING KO RIN)------------------------------------------

                            //if (ddApprover.SelectedItem.Text == "FOR INCHARGE APPROVAL")
                            //{
                            //    string msg = "failedMessage : " + failedMessage + "<br/> querySuccess : " + querySuccess + "<br /> queryStatusCounter : " + queryStatusCounter + "<br /> query1 : " + query1 + "<br/> query2 : " + query2 + "<br/> query3 : " + query3 + "<br/> query4 : " + query4;
                            //    txtRFQNo.Text = msg;
                            //    //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + msg + "');", true);
                            //}

                            ////FOR TESTING (TATANGGALING KO RIN)------------------------------------------


                        }
                    }                                        


                }

                //-------------------------------------------------------------------------------------------------------------

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

                Label lblRFQNo = row.FindControl("lblRFQNo") as Label;
                Label lblStatAll = row.FindControl("lblStatAll") as Label;
                Label lblCategory = row.FindControl("lblCategory") as Label;
                Label lblRequester = row.FindControl("lblRequester") as Label;
                Label lblTransactionDate = row.FindControl("lblTransactionDate") as Label;
                ImageButton ibPreview = row.FindControl("ibPreview") as ImageButton;
                ImageButton ibApproved = row.FindControl("ibApproved") as ImageButton;
                ImageButton ibDisapproved = row.FindControl("ibDisapproved") as ImageButton;
                ImageButton ibHold = row.FindControl("ibHold") as ImageButton;
                TextBox txtRemarks = row.FindControl("txtRemarks") as TextBox;                
                GridView gvResponseProd = row.FindControl("gvResponseProd") as GridView;


                if (e.CommandName == "Preview_Command")
                {
                    //string URL = "~/RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(lblRFQNo.Text.Trim(), true);

                    //Session["Requester_From_Inquiry"] = lblRequester.Text;
                    //Session["TransDate_From_Inquiry"] = lblTransactionDate.Text;
                    //Session["Category_From_Inquiry"] = lblCategory.Text.Trim().ToUpper();
                    //Session["btnPreview_Visibility"] = lblStatAll.Text == "APPROVED" ? "true" : "false";
                    //Session["btnUpdate_Visibility"] = "true";

                    ////URL = Page.ResolveClientUrl(URL);
                    ////ibPreview.OnClientClick = "window.open('" + URL + "'); return false;";

                    //Response.Redirect("RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(lblRFQNo.Text.Trim(), true), false);

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

                if (e.CommandName == "A_Command")
                {
                    if (ibDisapproved.ImageUrl == "~/images/DA2.png" || ibHold.ImageUrl == "~/images/hold2.png")
                    {
                    }
                    else
                    {
                        if (ibApproved.ImageUrl == "~/images/A1.png")
                        {
                            ibApproved.ImageUrl = "~/images/A2.png";

                            gvResponseProd.Visible = true;

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
                            gvResponseProd.Visible = false;
                            ibApproved.ImageUrl = "~/images/A1.png";
                        }
                    }
                }

                if (e.CommandName == "DA_Command")
                {
                    if (ibApproved.ImageUrl == "~/images/A2.png" || ibHold.ImageUrl == "~/images/hold2.png")
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

                if (e.CommandName == "Hold_Command")
                {
                    if (ibApproved.ImageUrl == "~/images/A2.png" || ibDisapproved.ImageUrl == "~/images/DA2.png")
                    {
                        
                    }
                    else
                    {
                        if (ibHold.ImageUrl == "~/images/hold.png")
                        {
                            ibHold.ImageUrl = "~/images/hold2.png";
                            txtRemarks.Enabled = true;
                        }
                        else
                        {
                            ibHold.ImageUrl = "~/images/hold.png";
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
                    Label lblItem = (Label)e.Row.FindControl("lblItem");                    
                    ImageButton ibApprovedResponse = (ImageButton)e.Row.FindControl("ibApprovedResponse");
                    DropDownList ddResponseCurrency = (DropDownList)e.Row.FindControl("ddResponseCurrency");
                    Label lblResponseCurrency = (Label)e.Row.FindControl("lblResponseCurrency");
                    TextBox txtResponseSupplierRemarks = (TextBox)e.Row.FindControl("txtResponseSupplierRemarks");
                    TextBox txtResponsePurchasingRemarks = (TextBox)e.Row.FindControl("txtResponsePurchasingRemarks");
                    Label lblResponseMaker = (Label)e.Row.FindControl("lblResponseMaker");

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


                    if (!string.IsNullOrEmpty(lblResponseMaker.Text))
                    {
                        if (lblResponseMaker.Text.Length > 6)
                        {
                            int supRemLen2 = lblResponseMaker.Text.Length;
                            int divisor2 = 6;
                            int height2 = ((supRemLen2 / divisor2) * 20) + 10;

                            lblResponseMaker.Style.Add("Height", height2.ToString() + "px");
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
                Label lblSupplierResponse = row.FindControl("lblSupplierResponse") as Label;


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
                    string ifOnlyOne = lblSupplierResponse.Text.Trim().Substring(0, 1).ToString();

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

                            if (ifOnlyOne == "1") // this will check if supplier response is only 1 then automatically select all items
                            {
                                for (int i = 0; i < gvResponse.Rows.Count; i++)
                                {
                                    ImageButton ibApprovedResponse = (ImageButton)gvResponse.Rows[i].Cells[2].FindControl("ibApprovedResponse");
                                    ibApprovedResponse.ImageUrl = "~/images/A2.png";
                                }
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

                            gvResponse.Visible = true;

                            List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                            list = BLL.RFQ_TRANSACTION_GetSupplierResponseByRFQNo(lblRFQNo.Text.Trim());

                            if (list != null)
                            {
                                if (list.Count > 0)
                                {
                                    gvResponse.DataSource = list;
                                    gvResponse.DataBind();

                                    for (int i = 0; i < gvResponse.Rows.Count; i++)
                                    {
                                        ImageButton ibApprovedResponse = (ImageButton)gvResponse.Rows[i].Cells[2].FindControl("ibApprovedResponse");
                                        ibApprovedResponse.Visible = false;
                                    }
                                }
                            }
                            else
                            {
                                gvResponse.EmptyDataText = "NO SUPPLIER RESPONSE";
                            }

                        }
                        else
                        {
                            ibDisapproved.ImageUrl = "~/images/DA1.png";
                            txtRemarks.Text = string.Empty;
                            txtRemarks.Enabled = false;
                            gvResponse.Visible = false;
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


    }
}
