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

public class Entities_CRF_RequestEntry
{
    public Entities_CRF_RequestEntry()
    {
    }

    private string refid;
    private string supplier;
    private string supplierName;
    private string attention;
    private string poDate;
    private string poNO;
    private string prNO;
    private string description;
    private string typeDrawingNo;
    private string orderQuantity;
    private string unitOfMeasure;
    private string category;
    private string categoryId;
    private string attachment1;
    private string attachment2;
    private string attachment3;
    private string attachment4;
    private string attachment5;
    private string attachment6;
    private string attachment7;
    private string attachment8;
    private string requester;
    private string ctrlNo;
    private string transactionDate;
    private string updatedDate;
    private string updatedBy;
    private string requesterEmail;
    private string requesterLocalNumber;
    private string buyerRemarks;

    private string requesterS;
    private string requesterSDOA;
    private string requesterSStat;
    private string reqManager;
    private string reqManagerDOA;
    private string reqManagerStat;    
    private string purIncharge;
    private string purInchargeDOA;
    private string purInchargeStat;
    private string purManager;
    private string purManagerDOA;
    private string purManagerStat;
    private string statRemarks;
    private string supplierResponded;
    private string supplierResponseDate;
    private string buyerSend;
    private string buyerSendDOA;
    private string buyerSendStat;
    private string posted;
    private string postedBy;
    private string postedDate;
    private string postingRemarks;
    private string reason;
    private string reasonName;

    private string tableName;
    private string isDisabled;
    private string dropdownRefId;
    private string dropdownName;

    private string crFrom;
    private string crTo;

    private string statAll;
    private string cssColorCode;
    private string lcDepartment;
    private string lcDivision;
    private string supplierEmail;

    private string searchCriteria;

    private string responseConfirmedBy;
    private string responseDateConfirmed;
    private string responseNotes;
    private string responseDateReceived;
    private string supplierAttachment;
    private string responseResponseDetailsId;

    private string reqManagerStatColor;
    private string purInchargeStatColor;
    private string purManagerStatColor;
    private string supplierStatColor;

    private string departmentName;
    private string divisionName;
    private string division;

    private string sendDates;

    private string dateInformedSupplier;
    private string dateInformedRequestor;


    private string rowNumber;
    private string rdRefId;
    private string rdCtrlNo;
    private string rdPRNO;
    private string rdPONO;
    private string rdItemName;
    private string rdSpecs;
    private string rdQuantity;
    private string rdUnitOfMeasure;
    private string rdUOMDesc;
    private string rdReason;
    private string rdDateInformedSupplier;
    private string rdDateInformedRequester;
    private string rdPODate;
    private string dataIndex;
    private string rdAttachment;
    private string rdReasonName;
    private string rdSupplierAttachment;

    private string reqManagerStatDisplay;
    private string buyerStatDisplay;
    private string scInchargeStatDisplay;
    private string supplierStatDisplay;

    private string forSending;

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


    private string report_BuyerName;










    public string Report_BuyerName
    {
        get { return report_BuyerName; }
        set { report_BuyerName = value; }
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



    public string ForSending
    {
        get { return forSending; }
        set { forSending = value; }
    }

    public string ResponseResponseDetailsId
    {
        get { return responseResponseDetailsId; }
        set { responseResponseDetailsId = value; }
    }


    public string ReqManagerStatDisplay
    {
        get { return reqManagerStatDisplay; }
        set { reqManagerStatDisplay = value; }
    }
    public string BuyerStatDisplay
    {
        get { return buyerStatDisplay; }
        set { buyerStatDisplay = value; }
    }
    public string ScInchargeStatDisplay
    {
        get { return scInchargeStatDisplay; }
        set { scInchargeStatDisplay = value; }
    }
    public string SupplierStatDisplay
    {
        get { return supplierStatDisplay; }
        set { supplierStatDisplay = value; }
    }



    
    public string RdReasonName
    {
        get { return rdReasonName; }
        set { rdReasonName = value; }
    }
    public string RdSupplierAttachment
    {
        get { return rdSupplierAttachment; }
        set { rdSupplierAttachment = value; }
    }
    public string RowNumber
    {
        get { return rowNumber; }
        set { rowNumber = value; }
    }
    public string RdRefId
    {
        get { return rdRefId; }
        set { rdRefId = value; }
    }
    public string RdCtrlNo
    {
        get { return rdCtrlNo; }
        set { rdCtrlNo = value; }
    }
    public string RdPRNO
    {
        get { return rdPRNO; }
        set { rdPRNO = value; }
    }
    public string RdPONO
    {
        get { return rdPONO; }
        set { rdPONO = value; }
    }
    public string RdItemName
    {
        get { return rdItemName; }
        set { rdItemName = value; }
    }
    public string RdSpecs
    {
        get { return rdSpecs; }
        set { rdSpecs = value; }
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
    public string RdUOMDesc
    {
        get { return rdUOMDesc; }
        set { rdUOMDesc = value; }
    }
    public string RdReason
    {
        get { return rdReason; }
        set { rdReason = value; }
    }
    public string RdDateInformedSupplier
    {
        get { return rdDateInformedSupplier; }
        set { rdDateInformedSupplier = value; }
    }
    public string RdDateInformedRequester
    {
        get { return rdDateInformedRequester; }
        set { rdDateInformedRequester = value; }
    }
    public string RdPODate
    {
        get { return rdPODate; }
        set { rdPODate = value; }
    }
    public string DataIndex
    {
        get { return dataIndex; }
        set { dataIndex = value; }
    }
    public string RdAttachment
    {
        get { return rdAttachment; }
        set { rdAttachment = value; }
    }





    public string DateInformedSupplier
    {
        get { return dateInformedSupplier; }
        set { dateInformedSupplier = value; }
    }
    public string DateInformedRequestor
    {
        get { return dateInformedRequestor; }
        set { dateInformedRequestor = value; }
    }

    public string SendDates
    {
        get { return sendDates; }
        set { sendDates = value; }
    }
    public string DepartmentName
    {
        get { return departmentName; }
        set { departmentName = value; }
    }
    public string DivisionName
    {
        get { return divisionName; }
        set { divisionName = value; }
    }
    public string Division
    {
        get { return division; }
        set { division = value; }
    }

    public string ReqManagerStatColor
    {
        get { return reqManagerStatColor; }
        set { reqManagerStatColor = value; }
    }
    public string PurInchargeStatColor
    {
        get { return purInchargeStatColor; }
        set { purInchargeStatColor = value; }
    }
    public string PurManagerStatColor
    {
        get { return purManagerStatColor; }
        set { purManagerStatColor = value; }
    }
    public string SupplierStatColor
    {
        get { return supplierStatColor; }
        set { supplierStatColor = value; }
    }

    public string ResponseConfirmedBy
    {
        get { return responseConfirmedBy; }
        set { responseConfirmedBy = value; }
    }
    public string ResponseDateConfirmed
    {
        get { return responseDateConfirmed; }
        set { responseDateConfirmed = value; }
    }
    public string ResponseNotes
    {
        get { return responseNotes; }
        set { responseNotes = value; }
    }
    public string ResponseDateReceived
    {
        get { return responseDateReceived; }
        set { responseDateReceived = value; }
    }

    public string SupplierAttachment
    {
        get { return supplierAttachment; }
        set { supplierAttachment = value; }
    }
    public string SearchCriteria
    {
        get { return searchCriteria; }
        set { searchCriteria = value; }
    }
    public string SupplierEmail
    {
        get { return supplierEmail; }
        set { supplierEmail = value; }
    }
    public string RefId
    {
        get { return refid; }
        set { refid = value; }
    }
    public string Supplier
    {
        get { return supplier; }
        set { supplier = value; }
    }
    public string SupplierName
    {
        get { return supplierName; }
        set { supplierName = value; }
    }
    public string Attention
    {
        get { return attention; }
        set { attention = value; }
    }
    public string PoDate
    {
        get { return poDate; }
        set { poDate = value; }
    }
    public string PoNO
    {
        get { return poNO; }
        set { poNO = value; }
    }
    public string PrNO
    {
        get { return prNO; }
        set { prNO = value; }
    }
    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    public string TypeDrawingNo
    {
        get { return typeDrawingNo; }
        set { typeDrawingNo = value; }
    }
    public string OrderQuantity
    {
        get { return orderQuantity; }
        set { orderQuantity = value; }
    }
    public string UnitOfMeasure
    {
        get { return unitOfMeasure; }
        set { unitOfMeasure = value; }
    }
    public string Category
    {
        get { return category; }
        set { category = value; }
    }
    public string CategoryId
    {
        get { return categoryId; }
        set { categoryId = value; }
    }
    public string Attachment1
    {
        get { return attachment1; }
        set { attachment1 = value; }
    }
    public string Attachment2
    {
        get { return attachment2; }
        set { attachment2 = value; }
    }
    public string Attachment3
    {
        get { return attachment3; }
        set { attachment3 = value; }
    }
    public string Attachment4
    {
        get { return attachment4; }
        set { attachment4 = value; }
    }
    public string Attachment5
    {
        get { return attachment5; }
        set { attachment5 = value; }
    }
    public string Attachment6
    {
        get { return attachment6; }
        set { attachment6 = value; }
    }
    public string Attachment7
    {
        get { return attachment7; }
        set { attachment7 = value; }
    }
    public string Attachment8
    {
        get { return attachment8; }
        set { attachment8 = value; }
    }
    public string Requester
    {
        get { return requester; }
        set { requester = value; }
    }
    public string CtrlNo
    {
        get { return ctrlNo; }
        set { ctrlNo = value; }
    }
    public string TransactionDate
    {
        get { return transactionDate; }
        set { transactionDate = value; }
    }
    public string UpdatedDate
    {
        get { return updatedDate; }
        set { updatedDate = value; }
    }
    public string UpdatedBy
    {
        get { return updatedBy; }
        set { updatedBy = value; }
    }
    public string RequesterLocalNumber
    {
        get { return requesterLocalNumber; }
        set { requesterLocalNumber = value; }
    }
    public string BuyerRemarks
    {
        get { return buyerRemarks; }
        set { buyerRemarks = value; }
    }
    public string RequesterEmail
    {
        get { return requesterEmail; }
        set { requesterEmail = value; }
    }




    public string RequesterS
    {
        get { return requesterS; }
        set { requesterS = value; }
    }
    public string RequesterSDOA
    {
        get { return requesterSDOA; }
        set { requesterSDOA = value; }
    }
    public string RequesterSStat
    {
        get { return requesterSStat; }
        set { requesterSStat = value; }
    }

    public string ReqManager
    {
        get { return reqManager; }
        set { reqManager = value; }
    }
    public string ReqManagerDOA
    {
        get { return reqManagerDOA; }
        set { reqManagerDOA = value; }
    }
    public string ReqManagerStat
    {
        get { return reqManagerStat; }
        set { reqManagerStat = value; }
    }

    public string PurIncharge
    {
        get { return purIncharge; }
        set { purIncharge = value; }
    }
    public string PurInchargeDOA
    {
        get { return purInchargeDOA; }
        set { purInchargeDOA = value; }
    }
    public string PurInchargeStat
    {
        get { return purInchargeStat; }
        set { purInchargeStat = value; }
    }

    public string PurManager
    {
        get { return purManager; }
        set { purManager = value; }
    }
    public string PurManagerDOA
    {
        get { return purManagerDOA; }
        set { purManagerDOA = value; }
    }
    public string PurManagerStat
    {
        get { return purManagerStat; }
        set { purManagerStat = value; }
    }

    public string StatRemarks
    {
        get { return statRemarks; }
        set { statRemarks = value; }
    }

    public string SupplierResponded
    {
        get { return supplierResponded; }
        set { supplierResponded = value; }
    }
    public string SupplierResponseDate
    {
        get { return supplierResponseDate; }
        set { supplierResponseDate = value; }
    }
    public string BuyerSend
    {
        get { return buyerSend; }
        set { buyerSend = value; }
    }

    public string BuyerSendDOA
    {
        get { return buyerSendDOA; }
        set { buyerSendDOA = value; }
    }

    public string BuyerSendStat
    {
        get { return buyerSendStat; }
        set { buyerSendStat = value; }
    }

    public string Posted
    {
        get { return posted; }
        set { posted = value; }
    }

    public string PostedBy
    {
        get { return postedBy; }
        set { postedBy = value; }
    }

    public string PostedDate
    {
        get { return postedDate; }
        set { postedDate = value; }
    }

    public string PostingRemarks
    {
        get { return postingRemarks; }
        set { postingRemarks = value; }
    }

    public string Reason
    {
        get { return reason; }
        set { reason = value; }
    }

    public string ReasonName
    {
        get { return reasonName; }
        set { reasonName = value; }
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


    public string CrFrom
    {
        get { return crFrom; }
        set { crFrom = value; }
    }
    public string CrTo
    {
        get { return crTo; }
        set { crTo = value; }
    }

    public string StatAll
    {
        get { return statAll; }
        set { statAll = value; }
    }
    public string CssColorCode
    {
        get { return cssColorCode; }
        set { cssColorCode = value; }
    }
    public string LcDepartment
    {
        get { return lcDepartment; }
        set { lcDepartment = value; }
    }

    public string LcDivision
    {
        get { return lcDivision; }
        set { lcDivision = value; }
    }




}
