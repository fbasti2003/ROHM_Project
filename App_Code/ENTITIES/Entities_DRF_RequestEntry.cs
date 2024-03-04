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

public class Entities_DRF_RequestEntry
{
    public Entities_DRF_RequestEntry()
    {
    }

    private string refid;
    private string supplier;
    private string attention;
    private string invoiceDRNO;
    private string poDate;
    private string receivedDate;
    private string poNO;
    private string prNO;
    private string description;
    private string typeDrawingNo;
    private string orderQuantity;
    private string abnormalQuantity;
    private string typesOfAbnormality;
    private string detailedReport;
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

    private string division;
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

    private string tableName;
    private string isDisabled;
    private string dropdownRefId;
    private string dropdownName;

    private string drFrom;
    private string drTo;

    private string statAll;
    private string cssColorCode;
    private string lcDepartment;
    private string supplierEmail;

    private string searchCriteria;

    private string responseType;
    private string responseAnswer;
    private string supplierAttachment;

    private string reqManagerStatColor;
    private string purInchargeStatColor;
    private string purManagerStatColor;

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

    private string proofAttachment;
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

    public string ProofAttachment
    {
        get { return proofAttachment; }
        set { proofAttachment = value; }
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

    public string SupplierAttachment
    {
        get { return supplierAttachment; }
        set { supplierAttachment = value; }
    }
    public string ResponseType
    {
        get { return responseType; }
        set { responseType = value; }
    }
    public string ResponseAnswer
    {
        get { return responseAnswer; }
        set { responseAnswer = value; }
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
    public string Attention
    {
        get { return attention; }
        set { attention = value; }
    }
    public string InvoiceDRNO
    {
        get { return invoiceDRNO; }
        set { invoiceDRNO = value; }
    }
    public string PoDate
    {
        get { return poDate; }
        set { poDate = value; }
    }
    public string ReceivedDate
    {
        get { return receivedDate; }
        set { receivedDate = value; }
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
    public string AbnormalQuantity
    {
        get { return abnormalQuantity; }
        set { abnormalQuantity = value; }
    }
    public string TypesOfAbnormality
    {
        get { return typesOfAbnormality; }
        set { typesOfAbnormality = value; }
    }
    public string DetailedReport
    {
        get { return detailedReport; }
        set { detailedReport = value; }
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
    public string RequesterEmail
    {
        get { return requesterEmail; }
        set { requesterEmail = value; }
    }




    public string Division
    {
        get { return division; }
        set { division = value; }
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

}
