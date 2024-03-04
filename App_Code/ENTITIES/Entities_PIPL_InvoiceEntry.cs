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

[Serializable]
public class Entities_PIPL_InvoiceEntry
{
    public Entities_PIPL_InvoiceEntry()
    {
    }

    private string tableName;
    private string isDisabled;
    private string dropdownRefId;
    private string dropdownName;

    private string refIdHead;
    private string ctrlNo;
    private string invoiceNo;
    private string consignee;
    private string modeOfShipment;
    private string tradeTerms;
    private string pickUpLocation;
    private string purpose;
    private string packing;
    private string natureOfGoods;
    private string countryOfOrigin;
    private string category;
    private string requester;
    private string portOfDestination;
    private string etd;
    private string valueInYen;
    private string valueInUsd;
    private string poNo;
    private string commercialValue;
    private string bdn;
    private string bdnValue;
    private string transactionDate;
    private string updatedDate;
    private string updatedBy;


    private string headCTRLNo;
    private string description;
    private string quantity;
    private string uom;
    private string currency;
    private string uPrice;
    private string netWeight;
    private string grossWeight;
    private string measurement;
    private string attachment;
    private string auditTrailDate;
    private string specification;
    private string caseUnit;
    private string caseNumber;    

    private string attention1;
    private string attention2;
    private string secdept1;
    private string secdept2;
    private string remarks;
    private string referenceNo;

    private string manager;
    private string doaManager;
    private string pcManager;
    private string doaPCManager;
    private string incharge;
    private string doaIncharge;
    private string impex;
    private string doaImpex;
    private string accounting;
    private string doaAccounting;

    private string statManager;
    private string statPCManager;
    private string statIncharge;
    private string statImpex;
    private string statAccounting;

    private string drFrom;
    private string drTo;
    private string detailsRefId;
    private string approverName;
    private string department;
    private string statRemarks;
    private string division;

    private string purposeOthers;

    private string sectionName;
    private string departmentName;
    private string divisionName;
    private string pcName;
    private string hqName;

    private string pc;
    private string hq;

    private string salesType;
    private string businessUnit;
    private string accountCode;

    private string requesterLocalNumber;
    private string requesterEmailAddress;

    private string report_Department_Name;
    private string report_Department_Total_Request;
    private string report_Department_Buyer_Approved;
    private string report_Department_Buyer_Disapproved;
    private string report_Department_Impex_Approved;
    private string report_Department_Impex_Disapproved;
    private string report_Department_Pending_Approval;

    private string report_Division_Name;
    private string report_Division_Total_Request;
    private string report_Division_Buyer_Approved;
    private string report_Division_Buyer_Disapproved;
    private string report_Division_Impex_Approved;
    private string report_Division_Impex_Disapproved;
    private string report_Division_Pending_Approval;

    private string report_All_Total_Request;
    private string report_All_Buyer_Approved;
    private string report_All_Buyer_Disapproved;
    private string report_All_Impex_Approved;
    private string report_All_Impex_Disapproved;
    private string report_All_Pending_Approval;
    private string report_All_Total_Approval;
    private string report_All_Total_Disapproval;


    private string report_BuyerName;








    public string Report_BuyerName
    {
        get { return report_BuyerName; }
        set { report_BuyerName = value; }
    }



    public string Report_All_Total_Approval
    {
        get { return report_All_Total_Approval; }
        set { report_All_Total_Approval = value; }
    }
    public string Report_All_Total_Disapproval
    {
        get { return report_All_Total_Disapproval; }
        set { report_All_Total_Disapproval = value; }
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
    public string Report_Department_Impex_Approved
    {
        get { return report_Department_Impex_Approved; }
        set { report_Department_Impex_Approved = value; }
    }
    public string Report_Department_Impex_Disapproved
    {
        get { return report_Department_Impex_Disapproved; }
        set { report_Department_Impex_Disapproved = value; }
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
    public string Report_Division_Impex_Approved
    {
        get { return report_Division_Impex_Approved; }
        set { report_Division_Impex_Approved = value; }
    }
    public string Report_Division_Impex_Disapproved
    {
        get { return report_Division_Impex_Disapproved; }
        set { report_Division_Impex_Disapproved = value; }
    }
    public string Report_Division_Pending_Approval
    {
        get { return report_Division_Pending_Approval; }
        set { report_Division_Pending_Approval = value; }
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
    public string Report_All_Impex_Approved
    {
        get { return report_All_Impex_Approved; }
        set { report_All_Impex_Approved = value; }
    }
    public string Report_All_Impex_Disapproved
    {
        get { return report_All_Impex_Disapproved; }
        set { report_All_Impex_Disapproved = value; }
    }
    public string Report_All_Pending_Approval
    {
        get { return report_All_Pending_Approval; }
        set { report_All_Pending_Approval = value; }
    }



    public string RequesterLocalNumber
    {
        get { return requesterLocalNumber; }
        set { requesterLocalNumber = value; }
    }
    public string RequesterEmailAddress
    {
        get { return requesterEmailAddress; }
        set { requesterEmailAddress = value; }
    }

    public string SalesType
    {
        get { return salesType; }
        set { salesType = value; }
    }
    public string BusinessUnit
    {
        get { return businessUnit; }
        set { businessUnit = value; }
    }
    public string AccountCode
    {
        get { return accountCode; }
        set { accountCode = value; }
    }

    public string Pc
    {
        get { return pc; }
        set { pc = value; }
    }
    public string Hq
    {
        get { return hq; }
        set { hq = value; }
    }

    public string SectionName
    {
        get { return sectionName; }
        set { sectionName = value; }
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

    public string PcName
    {
        get { return pcName; }
        set { pcName = value; }
    }

    public string HqName
    {
        get { return hqName; }
        set { hqName = value; }
    }




    public string Division
    {
        get { return division; }
        set { division = value; }
    }

    public string PurposeOthers
    {
        get { return purposeOthers; }
        set { purposeOthers = value; }
    }

    public string StatRemarks
    {
        get { return statRemarks; }
        set { statRemarks = value; }
    }

    public string Department
    {
        get { return department; }
        set { department = value; }
    }

    public string CaseUnit
    {
        get { return caseUnit; }
        set { caseUnit = value; }
    }
    public string CaseNumber
    {
        get { return caseNumber; }
        set { caseNumber = value; }
    }

    public string ApproverName
    {
        get { return approverName; }
        set { approverName = value; }
    }

    public string DetailsRefId
    {
        get { return detailsRefId; }
        set { detailsRefId = value; }
    }
    public string StatManager
    {
        get { return statManager; }
        set { statManager = value; }
    }
    public string StatPCManager
    {
        get { return statPCManager; }
        set { statPCManager = value; }
    }
    public string StatIncharge
    {
        get { return statIncharge; }
        set { statIncharge = value; }
    }
    public string StatImpex
    {
        get { return statImpex; }
        set { statImpex = value; }
    }
    public string StatAccounting
    {
        get { return statAccounting; }
        set { statAccounting = value; }
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

    public string Manager
    {
        get { return manager; }
        set { manager = value; }
    }
    public string DoaManager
    {
        get { return doaManager; }
        set { doaManager = value; }
    }
    public string PcManager
    {
        get { return pcManager; }
        set { pcManager = value; }
    }
    public string DoaPCManager
    {
        get { return doaPCManager; }
        set { doaPCManager = value; }
    }
    public string Incharge
    {
        get { return incharge; }
        set { incharge = value; }
    }
    public string DoaIncharge
    {
        get { return doaIncharge; }
        set { doaIncharge = value; }
    }
    public string Impex
    {
        get { return impex; }
        set { impex = value; }
    }
    public string DoaImpex
    {
        get { return doaImpex; }
        set { doaImpex = value; }
    }
    public string Accounting
    {
        get { return accounting; }
        set { accounting = value; }
    }
    public string DoaAccounting
    {
        get { return doaAccounting; }
        set { doaAccounting = value; }
    }

    public string ReferenceNo
    {
        get { return referenceNo; }
        set { referenceNo = value; }
    }
    public string Remarks
    {
        get { return remarks; }
        set { remarks = value; }
    }
    public string Attention1
    {
        get { return attention1; }
        set { attention1 = value; }
    }
    public string Attention2
    {
        get { return attention2; }
        set { attention2 = value; }
    }
    public string Secdept1
    {
        get { return secdept1; }
        set { secdept1 = value; }
    }
    public string Secdept2
    {
        get { return secdept2; }
        set { secdept2 = value; }
    }

    public string Specification
    {
        get { return specification; }
        set { specification = value; }
    }

    public string Attachment
    {
        get { return attachment; }
        set { attachment = value; }
    }
    public string AuditTrailDate
    {
        get { return auditTrailDate; }
        set { auditTrailDate = value; }
    }

    public string NetWeight
    {
        get { return netWeight; }
        set { netWeight = value; }
    }
    public string GrossWeight
    {
        get { return grossWeight; }
        set { grossWeight = value; }
    }
    public string Measurement
    {
        get { return measurement; }
        set { measurement = value; }
    }

    public string Uom
    {
        get { return uom; }
        set { uom = value; }
    }
    public string Currency
    {
        get { return currency; }
        set { currency = value; }
    }
    public string UPrice
    {
        get { return uPrice; }
        set { uPrice = value; }
    }

    public string HeadCTRLNo
    {
        get { return headCTRLNo; }
        set { headCTRLNo = value; }
    }
    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    public string Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }


    public string TransactionDate
    {
        get { return transactionDate; }
        set { transactionDate = value; }
    }

    public string UpdatedBy
    {
        get { return updatedBy; }
        set { updatedBy = value; }
    }
    public string UpdatedDate
    {
        get { return updatedDate; }
        set { updatedDate = value; }
    }

    public string CommercialValue
    {
        get { return commercialValue; }
        set { commercialValue = value; }
    }
    public string Bdn
    {
        get { return bdn; }
        set { bdn = value; }
    }
    public string BdnValue
    {
        get { return bdnValue; }
        set { bdnValue = value; }
    }

    public string ValueInYen
    {
        get { return valueInYen; }
        set { valueInYen = value; }
    }
    public string ValueInUsd
    {
        get { return valueInUsd; }
        set { valueInUsd = value; }
    }
    public string PoNo
    {
        get { return poNo; }
        set { poNo = value; }
    }

    public string Requester
    {
        get { return requester; }
        set { requester = value; }
    }
    public string PortOfDestination
    {
        get { return portOfDestination; }
        set { portOfDestination = value; }
    }
    public string Etd
    {
        get { return etd; }
        set { etd = value; }
    }

    public string RefIdHead
    {
        get { return refIdHead; }
        set { refIdHead = value; }
    }
    public string CtrlNo
    {
        get { return ctrlNo; }
        set { ctrlNo = value; }
    }
    public string InvoiceNo
    {
        get { return invoiceNo; }
        set { invoiceNo = value; }
    }
    public string Consignee
    {
        get { return consignee; }
        set { consignee = value; }
    }

    public string ModeOfShipment
    {
        get { return modeOfShipment; }
        set { modeOfShipment = value; }
    }

    public string TradeTerms
    {
        get { return tradeTerms; }
        set { tradeTerms = value; }
    }

    public string PickUpLocation
    {
        get { return pickUpLocation; }
        set { pickUpLocation = value; }
    }

    public string Purpose
    {
        get { return purpose; }
        set { purpose = value; }
    }

    public string Packing
    {
        get { return packing; }
        set { packing = value; }
    }

    public string NatureOfGoods
    {
        get { return natureOfGoods; }
        set { natureOfGoods = value; }
    }

    public string CountryOfOrigin
    {
        get { return countryOfOrigin; }
        set { countryOfOrigin = value; }
    }

    public string Category
    {
        get { return category; }
        set { category = value; }
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
