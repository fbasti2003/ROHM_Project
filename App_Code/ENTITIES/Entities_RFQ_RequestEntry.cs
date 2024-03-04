using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public class Entities_RFQ_RequestEntry
{
    public Entities_RFQ_RequestEntry()
    {
    }

    private string headRefId;
    private string rfqno;
    private string category;
    private string categoryName;

    private string requester;
    private string drFrom;
    private string drTo;
    private string transactionDate;

    private string prodManagerStatus;
    private string purchasingStatus;
    private string supplierStatus;
    private string buyerStatus;
    private string purchasingInchargeStatus;
    private string departmentManagerStatus;
    private string divisionManagerStatus;

    private string sendDate;
    private string transactionName;
    private string approvedBy;
    private string approvedDate;


    private string statProdManager;
    private string statPurchasing;
    private string statBuyer;
    private string statPurchasingIncharge;
    private string statDeptManager;
    private string statDivManager;
    private string statSupplier;
    private string statAll;

    private string cause;

    private string cssColorCode;


    private string rdRefId;
    private string rdRfqNo;
    private string rdDescription;
    private string rdSpecs;
    private string rdMaker;
    private string rdQuantity;
    private string rdUnitOfMeasure;
    private string rdRemarks;
    private string rdPurchasingRemarks;
    private string rdAttachment;
    private string rdAttachmentYN;
    private string rdAttachmentLink;
    private string rdResponsePrice;
    private string rdResponseRemarks;
    private string rdCurrency;
    private string rdSupplierName;
    private string rdResponseLead;
    private string rdNo;
    private string rowNumber;
    private string rdUOMDesc;
    private string rdPurpose;
    private string rdProcess;
    

    private string supplierRewarded;
    private string supplierResponded;

    private string dropdownRefId;
    private string dropdownName;
    private string tableName;
    private string isDisabled;

    private string rhBuyerNotes;
    private string rhRfqNo;
    private string rhRefId;
    private string rhDivision;
    private string rhDepartment;
    private string rhDepartmentCode;
    private string rhSection;
    private string rhRequester;
    private string rhRequesterId;
    private string rhCategory;
    private string rhTransactionDate;
    private string cntSuppResp;
    private string groupBySupplierResponse;
    private string rhProdManagerApprovedDate;
    private string rhLeadTime;
    private string rhEmailAddress;
    private string rhLocalNumber;

    private string responseSupplierName;
    private string responseResponseDate;
    private string responseCount;
    private string responseRFQNo;
    private string responseSupplierID;
    private string responseSupplierEmail;
    private string responseSupplierAttachment;

    private string responseDescription;
    private string responseSpecs;
    private string responseMaker;
    private string responsePrice;
    private string responseLead;
    private string reponseWithResponse;
    private string responseSendStatus;
    private string responseNumberOfResponded;
    private string responseRefId;
    private string responsePurchasingRemarks;
    private string responseIsGranted;
    private string responseSupplierRemarks;
    private string responseSupplierCurrency;
    private string responseRemarks;

    private string searchCriteria;

    private string todayTopSupplier_SupplierName;
    private string todayTopSupplier_SupplierResponseCount;
    private string todayTopSupplier_SupplierId;
    private string todayTopSupplier_RFQNo;

    private string textboxHeight_SupplierRemarks;

    private string numberOfSuppliers_WithResponse;


    private string historyRFQ;
    private string historyUpdatedBy;
    private string historyUpdatedByID;
    private string historyUpdatedByUsername;
    private string historyUpdatedDate;
    private string historyUpdateWhat;
    private string historyUpdateWholeDetails;

    private string counter;
    private string dateReceivedFromSupplier;
    private string dateApprovedByBuyer;
    private string dateApprovedByIncharge;
    private string dateApprovedByDepartmentManager;

    private string requesterAttachment;

    private string purchasingRemarks;

    private string itemNumber;

    private string registered;



    private string report_Department_Name;
    private string report_Department_Total_Request;
    private string report_Department_Buyer_Approved;
    private string report_Department_Buyer_Disapproved;
    private string report_Department_PurManager_Approved;
    private string report_Department_PurManager_Disapproved;
    private string report_Department_Posted_Counts;
    private string report_Department_Pending_Approval;

    private string report_Division_Name;
    private string report_Division_Total_Request;
    private string report_Division_Buyer_Approved;
    private string report_Division_Buyer_Disapproved;
    private string report_Division_PurManager_Approved;
    private string report_Division_PurManager_Disapproved;
    private string report_Division_Posted_Counts;
    private string report_Division_Pending_Approval;

    private string report_All_Total_Request;
    private string report_All_Buyer_Approved;
    private string report_All_Buyer_Disapproved;
    private string report_All_PurManager_Approved;
    private string report_All_PurManager_Disapproved;
    private string report_All_Posted_Counts;
    private string report_All_Pending_Approval;
    private string report_All_Total_Approved;
    private string report_All_Total_Disapproved;

    private string hold_Reason;
    private string hold_By;
    private string hold_Date;

    private string report_BuyerName;




    public string Report_BuyerName
    {
        get { return report_BuyerName; }
        set { report_BuyerName = value; }
    }



    public string Hold_Reason
    {
        get { return hold_Reason; }
        set { hold_Reason = value; }
    }
    public string Hold_By
    {
        get { return hold_By; }
        set { hold_By = value; }
    }
    public string Hold_Date
    {
        get { return hold_Date; }
        set { hold_Date = value; }
    }

    public string Report_All_Total_Request
    {
        get { return report_All_Total_Request; }
        set { report_All_Total_Request = value; }
    }
    public string Report_All_Buyer_Approved
    {
        get { return report_All_Buyer_Approved; }
        set { report_All_Buyer_Approved = value; }
    }
    public string Report_All_Buyer_Disapproved
    {
        get { return report_All_Buyer_Disapproved; }
        set { report_All_Buyer_Disapproved = value; }
    }
    public string Report_All_PurManager_Approved
    {
        get { return report_All_PurManager_Approved; }
        set { report_All_PurManager_Approved = value; }
    }
    public string Report_All_PurManager_Disapproved
    {
        get { return report_All_PurManager_Disapproved; }
        set { report_All_PurManager_Disapproved = value; }
    }
    public string Report_All_Posted_Counts
    {
        get { return report_All_Posted_Counts; }
        set { report_All_Posted_Counts = value; }
    }
    public string Report_All_Pending_Approval
    {
        get { return report_All_Pending_Approval; }
        set { report_All_Pending_Approval = value; }
    }
    public string Report_All_Total_Approved
    {
        get { return report_All_Total_Approved; }
        set { report_All_Total_Approved = value; }
    }
    public string Report_All_Total_Disapproved
    {
        get { return report_All_Total_Disapproved; }
        set { report_All_Total_Disapproved = value; }
    }




    public string Report_Department_Name
    {
        get { return report_Department_Name; }
        set { report_Department_Name = value; }
    }
    public string Report_Department_Total_Request
    {
        get { return report_Department_Total_Request; }
        set { report_Department_Total_Request = value; }
    }
    public string Report_Department_Buyer_Approved
    {
        get { return report_Department_Buyer_Approved; }
        set { report_Department_Buyer_Approved = value; }
    }
    public string Report_Department_Buyer_Disapproved
    {
        get { return report_Department_Buyer_Disapproved; }
        set { report_Department_Buyer_Disapproved = value; }
    }
    public string Report_Department_PurManager_Approved
    {
        get { return report_Department_PurManager_Approved; }
        set { report_Department_PurManager_Approved = value; }
    }
    public string Report_Department_PurManager_Disapproved
    {
        get { return report_Department_PurManager_Disapproved; }
        set { report_Department_PurManager_Disapproved = value; }
    }
    public string Report_Department_Posted_Counts
    {
        get { return report_Department_Posted_Counts; }
        set { report_Department_Posted_Counts = value; }
    }
    public string Report_Department_Pending_Approval
    {
        get { return report_Department_Pending_Approval; }
        set { report_Department_Pending_Approval = value; }
    }

    public string Report_Division_Name
    {
        get { return report_Division_Name; }
        set { report_Division_Name = value; }
    }
    public string Report_Division_Total_Request
    {
        get { return report_Division_Total_Request; }
        set { report_Division_Total_Request = value; }
    }
    public string Report_Division_Buyer_Approved
    {
        get { return report_Division_Buyer_Approved; }
        set { report_Division_Buyer_Approved = value; }
    }
    public string Report_Division_Buyer_Disapproved
    {
        get { return report_Division_Buyer_Disapproved; }
        set { report_Division_Buyer_Disapproved = value; }
    }
    public string Report_Division_PurManager_Approved
    {
        get { return report_Division_PurManager_Approved; }
        set { report_Division_PurManager_Approved = value; }
    }
    public string Report_Division_PurManager_Disapproved
    {
        get { return report_Division_PurManager_Disapproved; }
        set { report_Division_PurManager_Disapproved = value; }
    }
    public string Report_Division_Posted_Counts
    {
        get { return report_Division_Posted_Counts; }
        set { report_Division_Posted_Counts = value; }
    }
    public string Report_Division_Pending_Approval
    {
        get { return report_Division_Pending_Approval; }
        set { report_Division_Pending_Approval = value; }
    }


    public string RhDepartmentCode
    {
        get { return rhDepartmentCode; }
        set { rhDepartmentCode = value; }
    }


    public string Registered
    {
        get { return registered; }
        set { registered = value; }
    }

    public string ItemNumber
    {
        get { return itemNumber; }
        set { itemNumber = value; }
    }
    public string RhEmailAddress
    {
        get { return rhEmailAddress; }
        set { rhEmailAddress = value; }
    }
    public string RhLocalNumber
    {
        get { return rhLocalNumber; }
        set { rhLocalNumber = value; }
    }


    public string PurchasingRemarks
    {
        get { return purchasingRemarks; }
        set { purchasingRemarks = value; }
    }

    public string RequesterAttachment
    {
        get { return requesterAttachment; }
        set { requesterAttachment = value; }
    }

    public string DateApprovedByBuyer
    {
        get { return dateApprovedByBuyer; }
        set { dateApprovedByBuyer = value; }
    }
    public string DateApprovedByIncharge
    {
        get { return dateApprovedByIncharge; }
        set { dateApprovedByIncharge = value; }
    }
    public string DateApprovedByDepartmentManager
    {
        get { return dateApprovedByDepartmentManager; }
        set { dateApprovedByDepartmentManager = value; }
    }

    public string DateReceivedFromSupplier
    {
        get { return dateReceivedFromSupplier; }
        set { dateReceivedFromSupplier = value; }
    }

    public string RhBuyerNotes
    {
        get { return rhBuyerNotes; }
        set { rhBuyerNotes = value; }
    }
    public string Counter
    {
        get { return counter; }
        set { counter = value; }
    }

    public string HistoryRFQ
    {
        get { return historyRFQ; }
        set { historyRFQ = value; }
    }
    public string HistoryUpdatedBy
    {
        get { return historyUpdatedBy; }
        set { historyUpdatedBy = value; }
    }
    public string HistoryUpdatedByID
    {
        get { return historyUpdatedByID; }
        set { historyUpdatedByID = value; }
    }
    public string HistoryUpdatedByUsername
    {
        get { return historyUpdatedByUsername; }
        set { historyUpdatedByUsername = value; }
    }
    public string HistoryUpdatedDate
    {
        get { return historyUpdatedDate; }
        set { historyUpdatedDate = value; }
    }
    public string HistoryUpdateWhat
    {
        get { return historyUpdateWhat; }
        set { historyUpdateWhat = value; }
    }
    public string HistoryUpdateWholeDetails
    {
        get { return historyUpdateWholeDetails; }
        set { historyUpdateWholeDetails = value; }
    }


    public string ResponseRemarks
    {
        get { return responseRemarks; }
        set { responseRemarks = value; }
    }
    public string NumberOfSuppliers_WithResponse
    {
        get { return numberOfSuppliers_WithResponse; }
        set { numberOfSuppliers_WithResponse = value; }
    }
    public string RdUOMDesc
    {
        get { return rdUOMDesc; }
        set { rdUOMDesc = value; }
    }
    public string RdProcess
    {
        get { return rdProcess; }
        set { rdProcess = value; }
    }
    public string RdPurpose
    {
        get { return rdPurpose; }
        set { rdPurpose = value; }
    }
    public string TextboxHeight_SupplierRemarks
    {
        get { return textboxHeight_SupplierRemarks; }
        set { textboxHeight_SupplierRemarks = value; }
    }
    public string TodayTopSupplier_SupplierName
    {
        get { return todayTopSupplier_SupplierName; }
        set { todayTopSupplier_SupplierName = value; }
    }
    public string TodayTopSupplier_SupplierId
    {
        get { return todayTopSupplier_SupplierId; }
        set { todayTopSupplier_SupplierId = value; }
    }
    public string TodayTopSupplier_RFQNo
    {
        get { return todayTopSupplier_RFQNo; }
        set { todayTopSupplier_RFQNo = value; }
    }
    public string TodayTopSupplier_SupplierResponseCount
    {
        get { return todayTopSupplier_SupplierResponseCount; }
        set { todayTopSupplier_SupplierResponseCount = value; }
    }
    public string SearchCriteria
    {
        get { return searchCriteria; }
        set { searchCriteria = value; }
    }
    public string RhLeadTime
    {
        get { return rhLeadTime; }
        set { rhLeadTime = value; }
    }
    public string ResponseSupplierCurrency
    {
        get { return responseSupplierCurrency; }
        set { responseSupplierCurrency = value; }
    }
    public string ResponseSupplierRemarks
    {
        get { return responseSupplierRemarks; }
        set { responseSupplierRemarks = value; }
    }
    public string ResponseIsGranted
    {
        get { return responseIsGranted; }
        set { responseIsGranted = value; }
    }
    public string ResponsePurchasingRemarks
    {
        get { return responsePurchasingRemarks; }
        set { responsePurchasingRemarks = value; }
    }
    public string ResponseRefId
    {
        get { return responseRefId; }
        set { responseRefId = value; }
    }
    public string DivisionManagerStatus
    {
        get { return divisionManagerStatus; }
        set { divisionManagerStatus = value; }
    }
    public string DepartmentManagerStatus
    {
        get { return departmentManagerStatus; }
        set { departmentManagerStatus = value; }
    }
    public string PurchasingInchargeStatus
    {
        get { return purchasingInchargeStatus; }
        set { purchasingInchargeStatus = value; }
    }
    public string BuyerStatus
    {
        get { return buyerStatus; }
        set { buyerStatus = value; }
    }
    public string ResponseNumberOfResponded
    {
        get { return responseNumberOfResponded; }
        set { responseNumberOfResponded = value; }
    }
    public string ResponseSendStatus
    {
        get { return responseSendStatus; }
        set { responseSendStatus = value; }
    }
    public string ResponseSupplierEmail
    {
        get { return responseSupplierEmail; }
        set { responseSupplierEmail = value; }
    }
    public string ReponseWithResponse
    {
        get { return reponseWithResponse; }
        set { reponseWithResponse = value; }
    }
    public string ResponseDescription
    {
        get { return responseDescription; }
        set { responseDescription = value; }
    }
    public string ResponseSpecs
    {
        get { return responseSpecs; }
        set { responseSpecs = value; }
    }
    public string ResponseMaker
    {
        get { return responseMaker; }
        set { responseMaker = value; }
    }
    public string ResponsePrice
    {
        get { return responsePrice; }
        set { responsePrice = value; }
    }
    public string ResponseLead
    {
        get { return responseLead; }
        set { responseLead = value; }
    }

    public string ResponseSupplierID
    {
        get { return responseSupplierID; }
        set { responseSupplierID = value; }
    }
    public string ResponseCount
    {
        get { return responseCount; }
        set { responseCount = value; }
    }
    public string ResponseRFQNo
    {
        get { return responseRFQNo; }
        set { responseRFQNo = value; }
    }
    public string ResponseSupplierAttachment
    {
        get { return responseSupplierAttachment; }
        set { responseSupplierAttachment = value; }
    }
    public string ResponseSupplierName
    {
        get { return responseSupplierName; }
        set { responseSupplierName = value; }
    }
    public string ResponseResponseDate
    {
        get { return responseResponseDate; }
        set { responseResponseDate = value; }
    }

    public string RhProdManagerApprovedDate
    {
        get { return rhProdManagerApprovedDate; }
        set { rhProdManagerApprovedDate = value; }
    }
    public string CntSuppResp
    {
        get { return cntSuppResp; }
        set { cntSuppResp = value; }
    }
    public string GroupBySupplierResponse
    {
        get { return groupBySupplierResponse; }
        set { groupBySupplierResponse = value; }
    }
    public string RhTransactionDate
    {
        get { return rhTransactionDate; }
        set { rhTransactionDate = value; }
    }
    public string RowNumber
    {
        get { return rowNumber; }
        set { rowNumber = value; }
    }

    public string RhRfqNo
    {
        get { return rhRfqNo; }
        set { rhRfqNo = value; }
    }
    public string RhRefId
    {
        get { return rhRefId; }
        set { rhRefId = value; }
    }
    public string RhDivision
    {
        get { return rhDivision; }
        set { rhDivision = value; }
    }
    public string RhDepartment
    {
        get { return rhDepartment; }
        set { rhDepartment = value; }
    }
    public string RhSection
    {
        get { return rhSection; }
        set { rhSection = value; }
    }
    public string RhRequester
    {
        get { return rhRequester; }
        set { rhRequester = value; }
    }
    public string RhRequesterId
    {
        get { return rhRequesterId; }
        set { rhRequesterId = value; }
    }
    public string RhCategory
    {
        get { return rhCategory; }
        set { rhCategory = value; }
    }


    public string TableName
    {
        get { return tableName; }
        set { tableName = value; }
    }
    public string IsDisabled
    {
        get { return isDisabled; }
        set { isDisabled = value; }
    }
    public string DropdownRefId
    {
        get { return dropdownRefId; }
        set { dropdownRefId = value; }
    }
    public string DropdownName
    {
        get { return dropdownName; }
        set { dropdownName = value; }
    }

    public string SupplierRewarded
    {
        get { return supplierRewarded; }
        set { supplierRewarded = value; }
    }
    public string SupplierResponded
    {
        get { return supplierResponded; }
        set { supplierResponded = value; }
    }

    public string RdNo
    {
        get { return rdNo; }
        set { rdNo = value; }
    }
    public string RdResponsePrice
    {
        get { return rdResponsePrice; }
        set { rdResponsePrice = value; }
    }
    public string RdResponseRemarks
    {
        get { return rdResponseRemarks; }
        set { rdResponseRemarks = value; }
    }
    public string RdCurrency
    {
        get { return rdCurrency; }
        set { rdCurrency = value; }
    }
    public string RdSupplierName
    {
        get { return rdSupplierName; }
        set { rdSupplierName = value; }
    }
    public string RdResponseLead
    {
        get { return rdResponseLead; }
        set { rdResponseLead = value; }
    }
    public string RdPurchasingRemarks
    {
        get { return rdPurchasingRemarks; }
        set { rdPurchasingRemarks = value; }
    }

    public string RdRefId
    {
        get { return rdRefId; }
        set { rdRefId = value; }
    }
    public string RdRfqNo
    {
        get { return rdRfqNo; }
        set { rdRfqNo = value; }
    }
    public string RdDescription
    {
        get { return rdDescription; }
        set { rdDescription = value; }
    }
    public string RdSpecs
    {
        get { return rdSpecs; }
        set { rdSpecs = value; }
    }
    public string RdMaker
    {
        get { return rdMaker; }
        set { rdMaker = value; }
    }
    public string RdQuantity
    {
        get { return rdQuantity; }
        set { rdQuantity = value; }
    }
    public string RdUnitOfMeasure
    {
        get { return rdUnitOfMeasure; }
        set { rdUnitOfMeasure = value; }
    }
    public string RdRemarks
    {
        get { return rdRemarks; }
        set { rdRemarks = value; }
    }
    public string RdAttachment
    {
        get { return rdAttachment; }
        set { rdAttachment = value; }
    }
    public string RdAttachmentYN
    {
        get { return rdAttachmentYN; }
        set { rdAttachmentYN = value; }
    }
    public string RdAttachmentLink
    {
        get { return rdAttachmentLink; }
        set { rdAttachmentLink = value; }
    }



    public string CssColorCode
    {
        get { return cssColorCode; }
        set { cssColorCode = value; }
    }

    public string Cause
    {
        get { return cause; }
        set { cause = value; }
    }

    public string StatAll
    {
        get { return statAll; }
        set { statAll = value; }
    }
    public string StatSupplier
    {
        get { return statSupplier; }
        set { statSupplier = value; }
    }
    public string StatProdManager
    {
        get { return statProdManager; }
        set { statProdManager = value; }
    }
    public string StatPurchasing
    {
        get { return statPurchasing; }
        set { statPurchasing = value; }
    }
    public string StatBuyer
    {
        get { return statBuyer; }
        set { statBuyer = value; }
    }
    public string StatPurchasingIncharge
    {
        get { return statPurchasingIncharge; }
        set { statPurchasingIncharge = value; }
    }
    public string StatDeptManager
    {
        get { return statDeptManager; }
        set { statDeptManager = value; }
    }
    public string StatDivManager
    {
        get { return statDivManager; }
        set { statDivManager = value; }
    }


    public string ApprovedBy
    {
        get { return approvedBy; }
        set { approvedBy = value; }
    }
    public string ApprovedDate
    {
        get { return approvedDate; }
        set { approvedDate = value; }
    }
    public string TransactionName
    {
        get { return transactionName; }
        set { transactionName = value; }
    }
    public string SendDate
    {
        get { return sendDate; }
        set { sendDate = value; }
    }

    public string ProdManagerStatus
    {
        get { return prodManagerStatus; }
        set { prodManagerStatus = value; }
    }
    public string PurchasingStatus
    {
        get { return purchasingStatus; }
        set { purchasingStatus = value; }
    }
    public string SupplierStatus
    {
        get { return supplierStatus; }
        set { supplierStatus = value; }
    }


    public string TransactionDate
    {
        get { return transactionDate; }
        set { transactionDate = value; }
    }
    public string HeadRefId
    {
        get { return headRefId; }
        set { headRefId = value; }
    }
    public string Rfqno
    {
        get { return rfqno; }
        set { rfqno = value; }
    }
    public string Category
    {
        get { return category; }
        set { category = value; }
    }
    public string CategoryName
    {
        get { return categoryName; }
        set { categoryName = value; }
    }


    public string Requester
    {
        get { return requester; }
        set { requester = value; }
    }
    public string DrFrom
    {
        get { return drFrom; }
        set { drFrom = value; }
    }
    public string DrTo
    {
        get { return drTo; }
        set { drTo = value; }
    }



}
