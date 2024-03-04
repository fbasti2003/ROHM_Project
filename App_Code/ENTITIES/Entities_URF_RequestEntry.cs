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

public class Entities_URF_RequestEntry
{
    public Entities_URF_RequestEntry()
    {
    }

    private string tableName;
    private string isDisabled;
    private string dropdownRefId;
    private string dropdownName;

    private string rhCtrlNo;
    private string rhRequester;
    private string rhRequesterEmail;
    private string rhSupplier;
    private string rhAttention;
    private string rhReason;
    private string rhReasonName;
    private string rhOtherReason;
    private string rhSupplierComments;
    private string rhPurchasingRemarks;
    private string rhTransactionDate;
    private string rhUpdatedDate;
    private string rhUpdatedBy;
    private string rhType;
    private string rhCategory;
    private string rhCategoryName;
    private string rhAttachment1;
    private string rhAttachment2;
    private string rhSupplierEmail;
    private string rhSupplierId;
    private string rhSupplierName;
    private string rhSendReceived;
    private string rhStockLifeAttachment;
    private string rhDivision;
    private string rhRemarks;

    private string rdRefId;
    private string rdCtrlNo;
    private string rdPRNO;
    private string rdPONO;
    private string rdItemName;
    private string rdSpecs;
    private string rdQuantity;
    private string rdUnitOfMeasure;
    private string rdUOMDesc;
    private string rdDeliveryConfirmationDate;
    private string rdRequestedDeliveryDate;
    private string rdReplyDeliveryDate;
    private string rdPODate;

    private string statCtrlNo;
    private string statProdSecManager;
    private string statDOAProdSecManager;
    private string statSTATProdSecManager;
    private string statProdDeptManager;
    private string statDOAProdDeptManager;
    private string statSTATProdDeptManager;
    private string statProdDivManager;
    private string statDOAProdDivManager;
    private string statSTATProdDivManager;
    private string statProdHQManager;
    private string statDOAProdHQManager;
    private string statSTATProdHQManager;
    private string statPurchasingBuyer;
    private string statDOAPurchasingBuyer;
    private string statSTATPurchasingBuyer;
    private string statPurchasingManager;
    private string statDOAPurchasingManager;
    private string statSTATPurchasingManager;
    private string statRemarks;
    private string statClosed;
    private string statReOpenRemarks;
    private string statSend;
    private string statReceived;

    private string drFrom;
    private string drTo;

    private string statAll;
    private string cssColorCode;

    private string prodArrayApprovalField;
    private string lcDepartment;
    private string lcDivision;
    private string lcHQ;
    private string rowNumber;
    private string supplierAttachment;

    private string criteria;
    private string dataIndex;

    private string hirarchyDepartment;
    private string hirarchyAccess;

    private string fontColor;

    private string secManagerApprover;
    private string deptManagerApprover;
    private string divManagerApprover;
    private string hqManagerApprover;

    private string buyerSend_SendBy;
    private string buyerSend_SendReceivedDate;
    private string supplierResponse_Date;

    private string disapprovalCause;

    private string repiStock;
    private string dailyUsage;
    private string stockLife;

    private string sendDates;

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

    private string postingRemarks;
    private string emptyPO;
    private string buyerRemarks;

    private string report_BuyerName;






    public string Report_BuyerName
    {
        get { return report_BuyerName; }
        set { report_BuyerName = value; }
    }

    public string BuyerRemarks
    {
        get { return buyerRemarks; }
        set { buyerRemarks = value; }
    }

    public string EmptyPO
    {
        get { return emptyPO; }
        set { emptyPO = value; }
    }

    public string PostingRemarks
    {
        get { return postingRemarks; }
        set { postingRemarks = value; }
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
    



    public string SendDates
    {
        get { return sendDates; }
        set { sendDates = value; }
    }

    public string RepiStock
    {
        get { return repiStock; }
        set { repiStock = value; }
    }
    public string DailyUsage
    {
        get { return dailyUsage; }
        set { dailyUsage = value; }
    }
    public string StockLife
    {
        get { return stockLife; }
        set { stockLife = value; }
    }

    public string DisapprovalCause
    {
        get { return disapprovalCause; }
        set { disapprovalCause = value; }
    }

    public string BuyerSend_SendBy
    {
        get { return buyerSend_SendBy; }
        set { buyerSend_SendBy = value; }
    }
    public string BuyerSend_SendReceivedDate
    {
        get { return buyerSend_SendReceivedDate; }
        set { buyerSend_SendReceivedDate = value; }
    }
    public string SupplierResponse_Date
    {
        get { return supplierResponse_Date; }
        set { supplierResponse_Date = value; }
    }


    public string SecManagerApprover
    {
        get { return secManagerApprover; }
        set { secManagerApprover = value; }
    }
    public string DeptManagerApprover
    {
        get { return deptManagerApprover; }
        set { deptManagerApprover = value; }
    }
    public string DivManagerApprover
    {
        get { return divManagerApprover; }
        set { divManagerApprover = value; }
    }
    public string HqManagerApprover
    {
        get { return hqManagerApprover; }
        set { hqManagerApprover = value; }
    }

    public string FontColor
    {
        get { return fontColor; }
        set { fontColor = value; }
    }

    public string HirarchyDepartment
    {
        get { return hirarchyDepartment; }
        set { hirarchyDepartment = value; }
    }
    public string HirarchyAccess
    {
        get { return hirarchyAccess; }
        set { hirarchyAccess = value; }
    }


    public string DataIndex
    {
        get { return dataIndex; }
        set { dataIndex = value; }
    }
    public string Criteria
    {
        get { return criteria; }
        set { criteria = value; }
    }

    public string SupplierAttachment
    {
        get { return supplierAttachment; }
        set { supplierAttachment = value; }
    }

    public string StatReOpenRemarks
    {
        get { return statReOpenRemarks; }
        set { statReOpenRemarks = value; }
    }

    public string StatSend
    {
        get { return statSend; }
        set { statSend = value; }
    }

    public string StatReceived
    {
        get { return statReceived; }
        set { statReceived = value; }
    }

    public string StatClosed
    {
        get { return statClosed; }
        set { statClosed = value; }
    }

    public string RhStockLifeAttachment
    {
        get { return rhStockLifeAttachment; }
        set { rhStockLifeAttachment = value; }
    }

    public string RhDivision
    {
        get { return rhDivision; }
        set { rhDivision = value; }
    }

    public string RhRemarks
    {
        get { return rhRemarks; }
        set { rhRemarks = value; }
    }

    public string RhSendReceived
    {
        get { return rhSendReceived; }
        set { rhSendReceived = value; }
    }

    public string RhSupplierId
    {
        get { return rhSupplierId; }
        set { rhSupplierId = value; }
    }

    public string RhSupplierName
    {
        get { return rhSupplierName; }
        set { rhSupplierName = value; }
    }

    public string RhSupplierEmail
    {
        get { return rhSupplierEmail; }
        set { rhSupplierEmail = value; }
    }

    public string StatRemarks
    {
        get { return statRemarks; }
        set { statRemarks = value; }
    }

    public string RowNumber
    {
        get { return rowNumber; }
        set { rowNumber = value; }
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
    public string LcHQ
    {
        get { return lcHQ; }
        set { lcHQ = value; }
    }
    public string ProdArrayApprovalField
    {
        get { return prodArrayApprovalField; }
        set { prodArrayApprovalField = value; }
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


    public string RhCtrlNo
    {
        get { return rhCtrlNo; }
        set { rhCtrlNo = value; }
    }
    public string RhRequester
    {
        get { return rhRequester; }
        set { rhRequester = value; }
    }
    public string RhRequesterEmail
    {
        get { return rhRequesterEmail; }
        set { rhRequesterEmail = value; }
    }
    public string RhSupplier
    {
        get { return rhSupplier; }
        set { rhSupplier = value; }
    }
    public string RhAttention
    {
        get { return rhAttention; }
        set { rhAttention = value; }
    }
    public string RhReason
    {
        get { return rhReason; }
        set { rhReason = value; }
    }
    public string RhReasonName
    {
        get { return rhReasonName; }
        set { rhReasonName = value; }
    }
    public string RhOtherReason
    {
        get { return rhOtherReason; }
        set { rhOtherReason = value; }
    }
    public string RhSupplierComments
    {
        get { return rhSupplierComments; }
        set { rhSupplierComments = value; }
    }
    public string RhPurchasingRemarks
    {
        get { return rhPurchasingRemarks; }
        set { rhPurchasingRemarks = value; }
    }
    public string RhTransactionDate
    {
        get { return rhTransactionDate; }
        set { rhTransactionDate = value; }
    }
    public string RhUpdatedDate
    {
        get { return rhUpdatedDate; }
        set { rhUpdatedDate = value; }
    }
    public string RhUpdatedBy
    {
        get { return rhUpdatedBy; }
        set { rhUpdatedBy = value; }
    }
    public string RhType
    {
        get { return rhType; }
        set { rhType = value; }
    }
    public string RhCategory
    {
        get { return rhCategory; }
        set { rhCategory = value; }
    }
    public string RhCategoryName
    {
        get { return rhCategoryName; }
        set { rhCategoryName = value; }
    }
    public string RhAttachment1
    {
        get { return rhAttachment1; }
        set { rhAttachment1 = value; }
    }
    public string RhAttachment2
    {
        get { return rhAttachment2; }
        set { rhAttachment2 = value; }
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
    public string RdDeliveryConfirmationDate
    {
        get { return rdDeliveryConfirmationDate; }
        set { rdDeliveryConfirmationDate = value; }
    }
    public string RdRequestedDeliveryDate
    {
        get { return rdRequestedDeliveryDate; }
        set { rdRequestedDeliveryDate = value; }
    }
    public string RdReplyDeliveryDate
    {
        get { return rdReplyDeliveryDate; }
        set { rdReplyDeliveryDate = value; }
    }
    public string RdPODate
    {
        get { return rdPODate; }
        set { rdPODate = value; }
    }



    public string StatCtrlNo
    {
        get { return statCtrlNo; }
        set { statCtrlNo = value; }
    }
    public string StatProdSecManager
    {
        get { return statProdSecManager; }
        set { statProdSecManager = value; }
    }
    public string StatDOAProdSecManager
    {
        get { return statDOAProdSecManager; }
        set { statDOAProdSecManager = value; }
    }
    public string StatSTATProdSecManager
    {
        get { return statSTATProdSecManager; }
        set { statSTATProdSecManager = value; }
    }
    public string StatProdDeptManager
    {
        get { return statProdDeptManager; }
        set { statProdDeptManager = value; }
    }
    public string StatDOAProdDeptManager
    {
        get { return statDOAProdDeptManager; }
        set { statDOAProdDeptManager = value; }
    }
    public string StatSTATProdDeptManager
    {
        get { return statSTATProdDeptManager; }
        set { statSTATProdDeptManager = value; }
    }
    public string StatProdDivManager
    {
        get { return statProdDivManager; }
        set { statProdDivManager = value; }
    }
    public string StatDOAProdDivManager
    {
        get { return statDOAProdDivManager; }
        set { statDOAProdDivManager = value; }
    }
    public string StatSTATProdDivManager
    {
        get { return statSTATProdDivManager; }
        set { statSTATProdDivManager = value; }
    }
    public string StatProdHQManager
    {
        get { return statProdHQManager; }
        set { statProdHQManager = value; }
    }
    public string StatDOAProdHQManager
    {
        get { return statDOAProdHQManager; }
        set { statDOAProdHQManager = value; }
    }
    public string StatSTATProdHQManager
    {
        get { return statSTATProdHQManager; }
        set { statSTATProdHQManager = value; }
    }
    public string StatPurchasingBuyer
    {
        get { return statPurchasingBuyer; }
        set { statPurchasingBuyer = value; }
    }
    public string StatDOAPurchasingBuyer
    {
        get { return statDOAPurchasingBuyer; }
        set { statDOAPurchasingBuyer = value; }
    }
    public string StatSTATPurchasingBuyer
    {
        get { return statSTATPurchasingBuyer; }
        set { statSTATPurchasingBuyer = value; }
    }
    public string StatPurchasingManager
    {
        get { return statPurchasingManager; }
        set { statPurchasingManager = value; }
    }
    public string StatDOAPurchasingManager
    {
        get { return statDOAPurchasingManager; }
        set { statDOAPurchasingManager = value; }
    }
    public string StatSTATPurchasingManager
    {
        get { return statSTATPurchasingManager; }
        set { statSTATPurchasingManager = value; }
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




}
