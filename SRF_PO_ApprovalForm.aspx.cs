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
using SpreadsheetLight;

namespace REPI_PUR_SOFRA
{
    public partial class SRF_PO_ApprovalForm : System.Web.UI.Page
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
                    // PRODUCTION MANAGER
                    if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["ProductionApprovalAccess"].ToString()).ToString().ToLower() == "true")
                    {
                        list = BLL.SRF_TRANSACTION_PO_AllRequest_ByDateRange(status).Where(itm => itm.Division == Session["Division"].ToString() && itm.Head_StatProdManager == "0").ToList();
                    }
                    // SCD BUYER
                    if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString()).ToString().ToLower() == "true")
                    {
                        list = BLL.SRF_TRANSACTION_PO_AllRequest_ByDateRange(status).Where(itm => itm.Head_StatProdManager == "1" && itm.Head_StatBuyer == "0" && (itm.Head_StatSCIncharge == "0" || itm.Head_StatSCIncharge == "2")).ToList().OrderBy(itm => itm.Head_Type).ToList();
                    }
                    // SCD INCHARGE
                    if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), "15").ToString().ToLower() == "true")
                    {

                        list = BLL.SRF_TRANSACTION_PO_AllRequest_ByDateRange(status).Where(itm => itm.Head_StatProdManager == "1" && itm.Head_StatBuyer == "1" && itm.Head_StatSCIncharge == "0").ToList();

                        // FOR PURCHASING REQUEST                        
                        List<Entities_SRF_PO_Entry> listForPuchasingRequest = new List<Entities_SRF_PO_Entry>();
                        listForPuchasingRequest = BLL.SRF_TRANSACTION_PO_AllRequest_ByDateRange(status).Where(itm => itm.Division == Session["Division"].ToString() && itm.Head_StatProdManager == "0").ToList();

                        if (listForPuchasingRequest != null)
                        {
                            if (listForPuchasingRequest.Count > 0)
                            {
                                foreach (Entities_SRF_PO_Entry entity in listForPuchasingRequest)
                                {
                                    Entities_SRF_PO_Entry final = new Entities_SRF_PO_Entry();
                                    final.BoxType = entity.BoxType;
                                    final.CrFrom = entity.CrFrom;
                                    final.CrTo = entity.CrTo;
                                    final.CssColorCode = entity.CssColorCode;
                                    final.Ctrlno = entity.Ctrlno;
                                    final.DisapprovalRemarks = entity.DisapprovalRemarks;
                                    final.Division = entity.Division;
                                    final.DivisionDisplay = entity.DivisionDisplay;
                                    final.GrossWeight = entity.GrossWeight;
                                    final.Head_Buyer = entity.Head_Buyer;
                                    final.Head_Ctrlno = entity.Head_Ctrlno;
                                    final.Head_DOABuyer = entity.Head_DOABuyer;
                                    final.Head_DOAProdManager = entity.Head_DOAProdManager;
                                    final.Head_DOASCIncharge = entity.Head_DOASCIncharge;
                                    final.Head_ProdManager = entity.Head_ProdManager;
                                    final.Head_RefId = entity.Head_RefId;
                                    final.Head_Requester = entity.Head_Requester;
                                    final.Head_RequesterId = entity.Head_RequesterId;
                                    final.Head_SCIncharge = entity.Head_SCIncharge;
                                    final.Head_StatBuyer = entity.Head_StatBuyer;
                                    final.Head_StatProdManager = entity.Head_StatProdManager;
                                    final.Head_StatSCIncharge = entity.Head_StatSCIncharge;
                                    final.Head_TotalBoxes = entity.Head_TotalBoxes;
                                    final.Head_TotalQuantity = entity.Head_TotalQuantity;
                                    final.Head_TransactionDate = entity.Head_TransactionDate;
                                    final.Head_Type = entity.Head_Type;
                                    final.Multiplier = entity.Multiplier;
                                    final.NetWeight = entity.NetWeight;
                                    final.NoOfBoxes = entity.NoOfBoxes;
                                    final.Quantity = entity.Quantity;
                                    final.Refid = entity.Refid;
                                    final.Size = entity.Size;
                                    final.Specification = entity.Specification;
                                    final.SrfItemName = entity.SrfItemName;
                                    final.StatAll = entity.StatAll;
                                    final.Supplier = entity.Supplier;
                                    final.WeightOfBox = entity.WeightOfBox;

                                    list.Add(final);
                                }
                            }
                        }

                        
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

                ImageButton ibApproved = row.FindControl("ibApproved") as ImageButton;
                ImageButton ibDisapproved = row.FindControl("ibDisapproved") as ImageButton;
                TextBox txtRemarks = row.FindControl("txtRemarks") as TextBox;

                LinkButton lbCTRLNo = row.FindControl("lbCTRLNo") as LinkButton;

                if (e.CommandName == "lbCTRLNo_Command")
                {
                    string URL = "~/SRF_PO_RequestEntry.aspx?SRFNo_PO_From_Inquiry=" + CryptorEngine.Encrypt(lbCTRLNo.Text.Trim(), true);

                    Response.Redirect(URL, false);
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

        private string setControlNumberWithPrefix_For_SRF()
        {
            string retVal = string.Empty;

            retVal = "SRF_" + Session["DivisionCode"].ToString() + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + COMMON.controlNoZeroPrefix(setControlNumber_For_SRF().ToString().Length.ToString()) + setControlNumber_For_SRF().ToString();

            return retVal;
        }

        private Int32 setControlNumber_For_SRF()
        {
            return BLL.SRF_TRANSACTION_Request_Count(DateTime.Now.Year.ToString()) + 1;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string mamSallyAccount = ConfigurationManager.AppSettings["MamSallyAccount"].ToString();
                string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                string query1 = string.Empty;
                string query1_For_SRF = string.Empty;
                int queryStatusCounter = 0;
                int queryStatusCounter_For_SRF = 0;
                string querySuccess = string.Empty;
                string querySuccess_For_SRF = string.Empty;
                string tempCtrlNo = string.Empty;
                string approvedBy = Session["LcRefId"].ToString();
                int disApprovalCause = 0;
                string emptyDI = string.Empty;
                string emptyForSending = string.Empty;

                if (gvData.Rows.Count > 0)
                {

                    for (int i = 0; i < gvData.Rows.Count; i++)
                    {
                        Label lblCTRLNo = (Label)gvData.Rows[i].Cells[1].FindControl("lblCTRLNo");
                        Label lblSupplier = (Label)gvData.Rows[i].Cells[1].FindControl("lblSupplier");
                        Label lblType = (Label)gvData.Rows[i].Cells[3].FindControl("lblType");
                        Label lblTotalQuantity = (Label)gvData.Rows[i].Cells[1].FindControl("lblTotalQuantity");
                        Label lblTotalBoxes = (Label)gvData.Rows[i].Cells[1].FindControl("lblTotalBoxes");
                        Label lblStatAll = (Label)gvData.Rows[i].Cells[4].FindControl("lblStatAll");
                        ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[5].FindControl("ibApproved");
                        ImageButton ibDisapproved = (ImageButton)gvData.Rows[i].Cells[5].FindControl("ibDisapproved");
                        TextBox txtRemarks = (TextBox)gvData.Rows[i].Cells[6].FindControl("txtRemarks");


                        if (ibApproved.ImageUrl == "~/images/A2.png")
                        {
                            if (lblStatAll.Text.ToUpper() == "FOR PROD.MNGR. APPROVAL")
                            {
                                query1 += "UPDATE SRF_TRANSACTION_PO_Head SET ProdManager = '" + approvedBy + "', DOAProdManager = CONVERT(VARCHAR, GETDATE(), 22), StatProdManager = '1' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR SCD BUYER APPROVAL" || lblStatAll.Text.ToUpper() == "DISAPPROVED")
                            {
                                if (lblStatAll.Text.ToUpper() == "FOR SCD BUYER APPROVAL")
                                {
                                    query1 += "UPDATE SRF_TRANSACTION_PO_Head SET Buyer = '" + approvedBy + "', DOABuyer = CONVERT(VARCHAR, GETDATE(), 22), StatBuyer = '1', SCIncharge ='" + mamSallyAccount + "', DOASCIncharge = CONVERT(VARCHAR, GETDATE(), 22), StatSCIncharge = '1', DisapprovalRemarks = '' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";                                    
                                } 
                                else 
                                {    
                                    query1 += "UPDATE SRF_TRANSACTION_PO_Head SET Buyer = '" + approvedBy + "', DOABuyer = CONVERT(VARCHAR, GETDATE(), 22), StatBuyer = '1', StatSCIncharge = '0', DisapprovalRemarks = '' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                                }

                                if (lblStatAll.Text.ToUpper() == "FOR SCD BUYER APPROVAL")
                                {
                                    List<Entities_SRF_RequestEntry> listRequestExistInCRF = new List<Entities_SRF_RequestEntry>();
                                    Entities_SRF_RequestEntry entityRequestExistInCRF = new Entities_SRF_RequestEntry();
                                    entityRequestExistInCRF.ProblemEncountered = "RE-USE " + lblType.Text.ToUpper();

                                    listRequestExistInCRF = BLL.SRF_TRANSACTION_IsPullOutRequestExistingInSRF(entityRequestExistInCRF);

                                    if (listRequestExistInCRF.Count <= 0)
                                    {
                                        query1_For_SRF += "INSERT INTO SRF_TRANSACTION_Request (CTRLNo,Requester,Category,TotalQuantity,PullOutServiceDate,ProblemEncountered,Remarks,PurposeOfPullOut,TransactionDate,Supplier) " +
                                                  "VALUES ('" + setControlNumberWithPrefix_For_SRF() + "','" + approvedBy + "','" + ConfigurationManager.AppSettings["SRF_PO_FIXED_CATEGORY"].ToString() + "','" + lblTotalQuantity.Text.Trim() + "','" + DateTime.Now.ToShortDateString() + "','RE-USE " + lblType.Text.ToUpper() + "','" + lblTotalBoxes.Text + " BOXES','" + ConfigurationManager.AppSettings["SRF_PO_FIXED_PURPOSE_OF_PULLOUT"].ToString() + "',GETDATE(),'" + lblSupplier.Text.Trim() + "') ";

                                        query1_For_SRF += "INSERT INTO SRF_TRANSACTION_Request_Details (CTRLNo,ItemName,ItemSpecification,TotalQuantity,UnitOfMeasure) " +
                                                  "VALUES ('" + setControlNumberWithPrefix_For_SRF() + "','RE-USE EMPTY','" + lblType.Text.ToUpper() + "','" + lblTotalQuantity.Text.Trim() + "','" + ConfigurationManager.AppSettings["SRF_PO_FIXED_UNIT_OF_MEASURE"].ToString() + "') ";

                                        query1_For_SRF += "INSERT INTO SRF_TRANSACTION_Status (CTRLNo,Req_Incharge,DOAReq_Incharge,STATReq_Incharge,STATReq_Manager,DOAReq_Manager,Req_Manager,STATPur_Incharge,STATPur_Manager,STATPur_DeptManager) " +
                                                  "VALUES ('" + setControlNumberWithPrefix_For_SRF() + "','" + approvedBy + "',GETDATE(),'1','1',GETDATE()," + mamSallyAccount + ",'0','0','0') ";

                                        query1_For_SRF += "UPDATE SRF_TRANSACTION_PO_Head SET SRF_Head = '" + setControlNumberWithPrefix_For_SRF() + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";

                                        queryStatusCounter_For_SRF++; // FOR SRF_TRANSACTION_Request
                                        queryStatusCounter_For_SRF++; // FOR SRF_TRANSACTION_Request_Details
                                        queryStatusCounter_For_SRF++; // FOR SRF_TRANSACTION_Status
                                        queryStatusCounter_For_SRF++;

                                    }
                                    else
                                    {
                                        foreach (Entities_SRF_RequestEntry eExist in listRequestExistInCRF)
                                        {
                                            string numberOfBoxes = eExist.Remarks.Replace("BOXES", string.Empty).Replace(" ", "");
                                            string totalNoOfBoxes = Decimal.Truncate(Decimal.Parse((double.Parse(numberOfBoxes) + double.Parse(lblTotalBoxes.Text)).ToString())).ToString();
                                            string totalQuantity = Decimal.Truncate(Decimal.Parse((double.Parse(lblTotalQuantity.Text.Trim()) + double.Parse(eExist.TotalQuantity.Trim())).ToString())).ToString();

                                            query1_For_SRF += "UPDATE SRF_TRANSACTION_Request SET TotalQuantity = '" + totalQuantity + "', Remarks = '" + totalNoOfBoxes + " BOXES' WHERE CTRLNo = '" + eExist.CtrlNo + "' ";
                                            query1_For_SRF += "UPDATE SRF_TRANSACTION_Request_Details SET  TotalQuantity = '" + totalQuantity + "' WHERE CTRLNo = '" + eExist.CtrlNo + "' ";

                                            query1_For_SRF += "UPDATE SRF_TRANSACTION_PO_Head SET SRF_Head = '" + eExist.CtrlNo + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                                            //query1_For_SRF += "UPDATE SRF_TRANSACTION_Status SET Req_Manager = '" + mamSallyAccount + "', DOAReq_Manager = GETDATE(), STATReq_Manager = '1' WHERE CTRLNo = '" + eExist.CtrlNo + "' ";
                                        }

                                    }

                                    //query1_For_SRF += "INSERT INTO SRF_TRANSACTION_Request (CTRLNo,Requester,Category,TotalQuantity,PullOutServiceDate,ProblemEncountered,Remarks,PurposeOfPullOut,TransactionDate,Supplier) " +
                                    //              "VALUES ('" + setControlNumberWithPrefix_For_SRF() + "','" + approvedBy + "','" + ConfigurationManager.AppSettings["SRF_PO_FIXED_CATEGORY"].ToString() + "','" + lblTotalQuantity.Text.Trim() + "','" + DateTime.Now.ToShortDateString() + "','RE-USE " + lblType.Text.ToUpper() + "','" + lblTotalBoxes.Text + " BOXES','" + ConfigurationManager.AppSettings["SRF_PO_FIXED_PURPOSE_OF_PULLOUT"].ToString() + "',GETDATE(),'" + lblSupplier.Text.Trim() + "') ";

                                    //query1_For_SRF += "INSERT INTO SRF_TRANSACTION_Request_Details (CTRLNo,ItemName,ItemSpecification,TotalQuantity,UnitOfMeasure) " +
                                    //          "VALUES ('" + setControlNumberWithPrefix_For_SRF() + "','RE-USE EMPTY','" + lblType.Text.ToUpper() + "','" + lblTotalQuantity.Text.Trim() + "','" + ConfigurationManager.AppSettings["SRF_PO_FIXED_UNIT_OF_MEASURE"].ToString() + "') ";

                                    //query1_For_SRF += "INSERT INTO SRF_TRANSACTION_Status (CTRLNo,Req_Incharge,DOAReq_Incharge,STATReq_Incharge,STATReq_Manager,DOAReq_Manager,Req_Manager,STATPur_Incharge,STATPur_Manager,STATPur_DeptManager) " +
                                    //          "VALUES ('" + setControlNumberWithPrefix_For_SRF() + "','" + approvedBy + "',GETDATE(),'1','1',GETDATE()," + mamSallyAccount + ",'0','0','0') ";

                                    //queryStatusCounter_For_SRF++; // FOR SRF_TRANSACTION_Request
                                    //queryStatusCounter_For_SRF++; // FOR SRF_TRANSACTION_Request_Details
                                    //queryStatusCounter_For_SRF++; // FOR SRF_TRANSACTION_Status

                                    // CHECK FIRST IF THERE IS FOR DISAPPROVAL WITH EMPTY REMARKS
                                    for (int x = 0; x < gvData.Rows.Count; x++)
                                    {
                                        TextBox txtRemarksPO = (TextBox)gvData.Rows[x].Cells[6].FindControl("txtRemarks");
                                        ImageButton ibDisapprovedPO = (ImageButton)gvData.Rows[x].Cells[5].FindControl("ibDisapproved");

                                        if (ibDisapprovedPO.ImageUrl == "~/images/DA2.png")
                                        {
                                            if (string.IsNullOrEmpty(txtRemarksPO.Text))
                                            {
                                                disApprovalCause++;
                                            }
                                        }
                                    }

                                    if (disApprovalCause <= 0)
                                    {
                                        // UPDATE SRF
                                        querySuccess_For_SRF = BLL.SRF_TRANSACTION_PO_SQLTransaction(queryBeginPart + query1_For_SRF + queryEndPart).ToString();

                                        query1_For_SRF = string.Empty;
                                    }

                                }

                            }
                            if (lblStatAll.Text.ToUpper() == "FOR SCD INCHARGE APPROVAL")
                            {
                                query1 += "UPDATE SRF_TRANSACTION_PO_Head SET SCIncharge = '" + approvedBy + "', DOASCIncharge = CONVERT(VARCHAR, GETDATE(), 22), StatSCIncharge = '1' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }

                            queryStatusCounter++;
                            tempCtrlNo += lblCTRLNo.Text.Trim().ToUpper() + ", ";

                        }

                        if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                        {
                            if (lblStatAll.Text.ToUpper() == "FOR PROD.MNGR. APPROVAL")
                            {
                                query1 += "UPDATE SRF_TRANSACTION_PO_Head SET ProdManager = '" + approvedBy + "', DOAProdManager = CONVERT(VARCHAR, GETDATE(), 22), StatProdManager = '2', DisapprovalRemarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR SCD BUYER APPROVAL" || lblStatAll.Text.ToUpper() == "DISAPPROVED")
                            {
                                query1 += "UPDATE SRF_TRANSACTION_PO_Head SET Buyer = '" + approvedBy + "', DOABuyer = CONVERT(VARCHAR, GETDATE(), 22), StatBuyer = '2', DisapprovalRemarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR SCD INCHARGE APPROVAL")
                            {
                                query1 += "UPDATE SRF_TRANSACTION_PO_Head SET SCIncharge = '" + approvedBy + "', DOASCIncharge = CONVERT(VARCHAR, GETDATE(), 22), StatSCIncharge = '2', StatBuyer = '0', DisapprovalRemarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }

                            if (string.IsNullOrEmpty(txtRemarks.Text))
                            {
                                disApprovalCause++;
                            }
                            queryStatusCounter++;
                            tempCtrlNo += lblCTRLNo.Text.Trim().ToUpper() + ", ";
                        }                        

                    }

                    if (disApprovalCause > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please provide a valid disapproval cause.');", true);
                    }                                        
                    else
                    {

                        if (queryStatusCounter > 0)
                        {
                            querySuccess = BLL.SRF_TRANSACTION_PO_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                            if (querySuccess == queryStatusCounter.ToString())
                            {
                                //-------------------------------------------------------------------------------------------------------------                                

                                for (int i = 0; i < gvData.Rows.Count; i++)
                                {
                                    Label lblCTRLNo = (Label)gvData.Rows[i].Cells[1].FindControl("lblCTRLNo");
                                    Label lblStatAll = (Label)gvData.Rows[i].Cells[4].FindControl("lblStatAll");
                                    ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[5].FindControl("ibApproved");
                                    ImageButton ibDisapproved = (ImageButton)gvData.Rows[i].Cells[5].FindControl("ibDisapproved");
                                    Label lblSupplierEmail = (Label)gvData.Rows[i].Cells[1].FindControl("lblSupplierEmail");
                                    Label lblSupplierName = (Label)gvData.Rows[i].Cells[1].FindControl("lblSupplierName");

                                    string xlsx_column = string.Empty;

                                    if (ibApproved.ImageUrl == "~/images/A2.png" || ibDisapproved.ImageUrl == "~/images/DA2.png")
                                    {
                                        if (lblStatAll.Text.ToUpper() == "FOR PROD.MNGR. APPROVAL")
                                        {
                                            xlsx_column = "C25";
                                        }
                                        if (lblStatAll.Text.ToUpper() == "FOR SCD BUYER APPROVAL" || lblStatAll.Text.ToUpper() == "DISAPPROVED")
                                        {
                                            xlsx_column = "D25";
                                        }
                                        if (lblStatAll.Text.ToUpper() == "FOR SCD INCHARGE APPROVAL")
                                        {
                                            xlsx_column = "E25";
                                        }

                                        string pathNew = Server.MapPath("~/SRF_PO_Request/" + lblCTRLNo.Text.Trim() + "/" + lblCTRLNo.Text.Trim() + "_SRF_PO_Request.xlsx");
                                        //Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew);
                                        FileStream fsNew = new FileStream(pathNew, FileMode.Open);
                                        using (SLDocument draftNew = new SLDocument(fsNew, "Sheet1"))
                                        {
                                            if (lblStatAll.Text.ToUpper() == "FOR SCD BUYER APPROVAL")
                                            {
                                                draftNew.SetCellValue(xlsx_column, Session["UserFullName"].ToString().ToUpper() + "\n" + DateTime.Now.ToShortDateString()); //APPROVER
                                                //draftNew.SetCellValue("E25", "BOREBOR, SALLY \n" + DateTime.Now.ToShortDateString()); //SCD INCHARGE
                                                draftNew.SetCellValue("E25", "Dearoz, Cherrylou \n" + DateTime.Now.ToShortDateString()); //SCD INCHARGE
                                            }
                                            else
                                            {
                                                draftNew.SetCellValue(xlsx_column, Session["UserFullName"].ToString().ToUpper() + "\n" + DateTime.Now.ToShortDateString()); //APPROVER
                                            }

                                            fsNew.Close();
                                            draftNew.SaveAs(pathNew);
                                        }

                                    }

                                    //// SEND EMAIL FUNCTIONALITY --------------------------------------------------------------------------------------------------
                                    //if (ibApproved.ImageUrl == "~/images/A2.png")
                                    //{
                                    //    if (lblStatAll.Text.ToUpper() == "FOR SCD BUYER APPROVAL")
                                    //    {
                                    //        string email_content = string.Empty;

                                    //        email_content = "Hi.<b>" + lblSupplierName.Text.ToUpper() + "</b>, Please see attached (<b>" + lblCTRLNo.Text.Trim() + "_SRF_PO_Request.xlsx</b>) excel file for your reference. " +
                                    //            "<br/><br/>" +
                                    //            "<p style='color:black; font-size:12px;'>Please do not reply. This is an auto generated email.</p>" +
                                    //            "<br/><br/>" +
                                    //            "<p style='color:black; font-size:12px;'>If you have any questions or clarifications, kindly send a separate email to <b>" + ConfigurationManager.AppSettings["MamAndhieMapanoo_EmailAddress"].ToString() + "</b></p>" +
                                    //            "<br/><br/>" +
                                    //            "Thank you," +
                                    //            "<br/><br/>" +
                                    //            "<b>ROHM Electronics Phils.</b>" +
                                    //            "<br/>" +
                                    //            "Supply Chain Management";

                                    //        COMMON.sendEmailToSRF_PullOut(lblSupplierEmail.Text.Trim(), ConfigurationManager.AppSettings["email-username"], "SRF PULL-OUT REQUEST", email_content, Server.MapPath("~/SRF_PO_Request/" + lblCTRLNo.Text.Trim() + "/" + lblCTRLNo.Text.Trim() + "_SRF_PO_Request.xlsx"), lblSupplierName.Text.ToUpper(), string.Empty);
                                    //    }
                                    //}
                                    ////----------------------------------------------------------------------------------------------------------------------------




                                }

                                Session["successMessage"] = "SRF_PO NUMBER(S) : <b>" + tempCtrlNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
                                Session["successTransactionName"] = "SRF_PO_APPROVALFORM";
                                Session["successReturnPage"] = "SRF_PO_ApprovalForm.aspx";
                                Response.Redirect("SuccessPage.aspx",false);
                                //-------------------------------------------------------------------------------------------------------------                                

                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select atleast 1 item to approved. No selected items for approval.');", true);
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
