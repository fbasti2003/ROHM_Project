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
public class Entities_SRF_RequestEntry
{

    public Entities_SRF_RequestEntry()
    {
    }

    private string no;
    private string tableName;
    private string isDisabled;
    private string dropdownRefId;
    private string dropdownName;

    private string refid;
    private string ctrlNo;
    private string category;
    private string supplier;
    private string supplierName;
    private string supplierEmail;
    private string attention;
    private string totalQuantity;
    private string pullOutServiceDate;
    private string deliveryDateToRepi;
    private string problemEncountered;
    private string purposeOfPullOut;
    private string purposeOfPullOutName;
    private string totalValueInUsd;
    private string loaNo;
    private string attachment;
    private string remarks;
    private string transactionDate;
    private string updatedDate;
    private string updatedBy;
    private string loaSuretyBond;
    private string loa8106;
    private string requester;

    private string reqIncharge;
    private string reqInchargeDOA;
    private string reqInchargeStat;
    private string reqManager;
    private string reqManagerDOA;
    private string reqManagerStat;
    private string purIncharge;
    private string purInchargeDOA;
    private string purInchargeStat;
    private string purImpex;
    private string purImpexDOA;
    private string purImpexStat;
    private string statRemarks;
    private string purDeptManager;
    private string purDeptManagerDOA;
    private string purDeptManagerStat;


    private string drFrom;
    private string drTo;

    private string department;
    private string division;
    private string section;

    private string gatePassNo;
    private string pickUpPoint;

    private string refPRPO;
    private string salesInvoice;
    private string brandMachineName;
    private string itemName;
    private string specification;
    private string quantity;
    private string unitOfMeasure;
    private string serialNo;
    private string uom_Description;


    private string reqInchargeName;
    private string reqManagerName;
    private string purInchargeName;
    private string purManagerName;
    private string purDeptManagerName;

    private string lOADescription;
    private string pOPDescription;
    private string categoryDescription;

    private string isImpex;
    private string selectAll;

    private string searchItem;
    private string statAll;

    private string document8105;

    private string uq_OriginalQuantity;
    private string uq_UpdatedQuantity;
    private string uq_Reason;
    private string uq_UpdatedBy;
    private string uq_UpdatedDate;

    




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


    private string warehouse_CtrlNo;
    private string warehouse_LOA8106;
    private string warehouse_LOANo;
    private string warehouse_RequesterName;
    private string warehouse_SupplierName;
    private string warehouse_TotalQuantity;
    private string warehouse_TotalActualQuantity;
    private string warehouse_Attachment;
    private string warehouse_AttachmentWarehouse;
    private string warehouse_DetailsRefId;
    private string warehouse_ItemName;
    private string warehouse_8105;
    private string warehouse_NewEntry;
    private string warehouse_DeliveredDate;
    private string warehouse_PezaNonPeza;
    private string warehouse_LOA8105ProcessDate;
    private string warehouse_WithDocuments;
    private string warehouse_LoaCount2;
    private string warehouse_RemainingQuantity;
    private string warehouse_PullOutServiceDate;




    private string eightOneZeroFive_RefId;
    private string eightOneZeroFive_No;
    private string eightOneZeroFive_CTRLNo;
    private string eightOneZeroFive_Quantity;
    private string eightOneZeroFive_Attachment;
    private string eightOneZeroFive_AddedBy;
    private string eightOneZeroFive_AddedDate;
    private string eightOneZeroFive_8105;
    private string eightOneZeroFive_ItemName;
    private string eightOneZeroFive_ActualQuantity;


    private string lOA_Number_From_HEAD;
    private string lOA_MaturityDate_From_HEAD;
    private string lOASuretyBond_From_HEAD;
    private string pullOutServiceDate_From_HEAD;
    private string lOAPriceValue_From_HEAD;
    private string gatePassNo_From_HEAD;
    private string inProgress;
    private string inProgressRemarks;
    private string inProgressRefId;
    private string remarksFromWarehouse;




    private string dashboardPeza_ApprovedWaiting_8106_Count;
    private string dashboardPeza_ApprovedWaiting_Pullout_Count;
    private string dashboardPeza_ApprovedWaiting_Remaining_Count;

    private string dashboardPeza_DeliveredPending_8106_Count;
    private string dashboardPeza_DeliveredPending_Pullout_Count;
    private string dashboardPeza_DeliveredPending_Remaining_Count;

    private string dashboardPeza_Delivered_8106_Count;
    private string dashboardPeza_Delivered_Pullout_Count;
    private string dashboardPeza_Delivered_Remaining_Count;

    private string old8106;

    private string report_BuyerName;
    private string warehouse_OverallTotalQty;

    private string stillWithNoDelivery;











    public string StillWithNoDelivery
    {
        get { return stillWithNoDelivery; }
        set { stillWithNoDelivery = value; }
    }

    public string Warehouse_OverallTotalQty
    {
        get { return warehouse_OverallTotalQty; }
        set { warehouse_OverallTotalQty = value; }
    }


    public string Report_BuyerName
    {
        get { return report_BuyerName; }
        set { report_BuyerName = value; }
    }


    public string Old8106
    {
        get { return old8106; }
        set { old8106 = value; }
    }

    public string DashboardPeza_ApprovedWaiting_8106_Count
    {
        get { return dashboardPeza_ApprovedWaiting_8106_Count; }
        set { dashboardPeza_ApprovedWaiting_8106_Count = value; }
    }
    public string DashboardPeza_ApprovedWaiting_Pullout_Count
    {
        get { return dashboardPeza_ApprovedWaiting_Pullout_Count; }
        set { dashboardPeza_ApprovedWaiting_Pullout_Count = value; }
    }
    public string DashboardPeza_ApprovedWaiting_Remaining_Count
    {
        get { return dashboardPeza_ApprovedWaiting_Remaining_Count; }
        set { dashboardPeza_ApprovedWaiting_Remaining_Count = value; }
    }
    public string DashboardPeza_DeliveredPending_8106_Count
    {
        get { return dashboardPeza_DeliveredPending_8106_Count; }
        set { dashboardPeza_DeliveredPending_8106_Count = value; }
    }
    public string DashboardPeza_DeliveredPending_Pullout_Count
    {
        get { return dashboardPeza_DeliveredPending_Pullout_Count; }
        set { dashboardPeza_DeliveredPending_Pullout_Count = value; }
    }
    public string DashboardPeza_DeliveredPending_Remaining_Count
    {
        get { return dashboardPeza_DeliveredPending_Remaining_Count; }
        set { dashboardPeza_DeliveredPending_Remaining_Count = value; }
    }
    public string DashboardPeza_Delivered_8106_Count
    {
        get { return dashboardPeza_Delivered_8106_Count; }
        set { dashboardPeza_Delivered_8106_Count = value; }
    }
    public string DashboardPeza_Delivered_Pullout_Count
    {
        get { return dashboardPeza_Delivered_Pullout_Count; }
        set { dashboardPeza_Delivered_Pullout_Count = value; }
    }
    public string DashboardPeza_Delivered_Remaining_Count
    {
        get { return dashboardPeza_Delivered_Remaining_Count; }
        set { dashboardPeza_Delivered_Remaining_Count = value; }
    }

    






    public string RemarksFromWarehouse
    {
        get { return remarksFromWarehouse; }
        set { remarksFromWarehouse = value; }
    }


    public string Uq_OriginalQuantity
    {
        get { return uq_OriginalQuantity; }
        set { uq_OriginalQuantity = value; }
    }
    public string Uq_UpdatedQuantity
    {
        get { return uq_UpdatedQuantity; }
        set { uq_UpdatedQuantity = value; }
    }
    public string Uq_Reason
    {
        get { return uq_Reason; }
        set { uq_Reason = value; }
    }
    public string Uq_UpdatedBy
    {
        get { return uq_UpdatedBy; }
        set { uq_UpdatedBy = value; }
    }
    public string Uq_UpdatedDate
    {
        get { return uq_UpdatedDate; }
        set { uq_UpdatedDate = value; }
    }


    public string Document8105
    {
        get { return document8105; }
        set { document8105 = value; }
    }
    public string InProgressRefId
    {
        get { return inProgressRefId; }
        set { inProgressRefId = value; }
    }
    public string InProgressRemarks
    {
        get { return inProgressRemarks; }
        set { inProgressRemarks = value; }
    }
    public string InProgress
    {
        get { return inProgress; }
        set { inProgress = value; }
    }
    public string GatePassNo_From_HEAD
    {
        get { return gatePassNo_From_HEAD; }
        set { gatePassNo_From_HEAD = value; }
    }
    public string LOAPriceValue_From_HEAD
    {
        get { return lOAPriceValue_From_HEAD; }
        set { lOAPriceValue_From_HEAD = value; }
    }
    public string PullOutServiceDate_From_HEAD
    {
        get { return pullOutServiceDate_From_HEAD; }
        set { pullOutServiceDate_From_HEAD = value; }
    }
    public string LOA_Number_From_HEAD
    {
        get { return lOA_Number_From_HEAD; }
        set { lOA_Number_From_HEAD = value; }
    }
    public string LOA_MaturityDate_From_HEAD
    {
        get { return lOA_MaturityDate_From_HEAD; }
        set { lOA_MaturityDate_From_HEAD = value; }
    }
    public string LOASuretyBond_From_HEAD
    {
        get { return lOASuretyBond_From_HEAD; }
        set { lOASuretyBond_From_HEAD = value; }
    }




    public string Warehouse_PullOutServiceDate
    {
        get { return warehouse_PullOutServiceDate; }
        set { warehouse_PullOutServiceDate = value; }
    }
    public string Warehouse_RemainingQuantity
    {
        get { return warehouse_RemainingQuantity; }
        set { warehouse_RemainingQuantity = value; }
    }
    public string Warehouse_LoaCount2
    {
        get { return warehouse_LoaCount2; }
        set { warehouse_LoaCount2 = value; }
    }
    public string Warehouse_WithDocuments
    {
        get { return warehouse_WithDocuments; }
        set { warehouse_WithDocuments = value; }
    }

    public string Warehouse_LOA8105ProcessDate
    {
        get { return warehouse_LOA8105ProcessDate; }
        set { warehouse_LOA8105ProcessDate = value; }
    }

    public string Warehouse_PezaNonPeza
    {
        get { return warehouse_PezaNonPeza; }
        set { warehouse_PezaNonPeza = value; }
    }

    public string Warehouse_DeliveredDate
    {
        get { return warehouse_DeliveredDate; }
        set { warehouse_DeliveredDate = value; }
    }

    public string Warehouse_NewEntry
    {
        get { return warehouse_NewEntry; }
        set { warehouse_NewEntry = value; }
    }
    public string Warehouse_8105
    {
        get { return warehouse_8105; }
        set { warehouse_8105 = value; }
    }
    public string EightOneZeroFive_ActualQuantity
    {
        get { return eightOneZeroFive_ActualQuantity; }
        set { eightOneZeroFive_ActualQuantity = value; }
    }
    public string EightOneZeroFive_ItemName
    {
        get { return eightOneZeroFive_ItemName; }
        set { eightOneZeroFive_ItemName = value; }
    }
    public string Warehouse_DetailsRefId
    {
        get { return warehouse_DetailsRefId; }
        set { warehouse_DetailsRefId = value; }
    }
    public string Warehouse_ItemName
    {
        get { return warehouse_ItemName; }
        set { warehouse_ItemName = value; }
    }

    public string EightOneZeroFive_No
    {
        get { return eightOneZeroFive_No; }
        set { eightOneZeroFive_No = value; }
    }
    public string EightOneZeroFive_8105
    {
        get { return eightOneZeroFive_8105; }
        set { eightOneZeroFive_8105 = value; }
    }
    public string EightOneZeroFive_RefId
    {
        get { return eightOneZeroFive_RefId; }
        set { eightOneZeroFive_RefId = value; }
    }
    public string EightOneZeroFive_CTRLNo
    {
        get { return eightOneZeroFive_CTRLNo; }
        set { eightOneZeroFive_CTRLNo = value; }
    }
    public string EightOneZeroFive_Quantity
    {
        get { return eightOneZeroFive_Quantity; }
        set { eightOneZeroFive_Quantity = value; }
    }
    public string EightOneZeroFive_Attachment
    {
        get { return eightOneZeroFive_Attachment; }
        set { eightOneZeroFive_Attachment = value; }
    }
    public string EightOneZeroFive_AddedBy
    {
        get { return eightOneZeroFive_AddedBy; }
        set { eightOneZeroFive_AddedBy = value; }
    }
    public string EightOneZeroFive_AddedDate
    {
        get { return eightOneZeroFive_AddedDate; }
        set { eightOneZeroFive_AddedDate = value; }
    }


    public string Warehouse_Attachment
    {
        get { return warehouse_Attachment; }
        set { warehouse_Attachment = value; }
    }
    public string Warehouse_AttachmentWarehouse
    {
        get { return warehouse_AttachmentWarehouse; }
        set { warehouse_AttachmentWarehouse = value; }
    }
    public string Warehouse_TotalQuantity
    {
        get { return warehouse_TotalQuantity; }
        set { warehouse_TotalQuantity = value; }
    }
    public string Warehouse_TotalActualQuantity
    {
        get { return warehouse_TotalActualQuantity; }
        set { warehouse_TotalActualQuantity = value; }
    }
    public string Warehouse_CtrlNo
    {
        get { return warehouse_CtrlNo; }
        set { warehouse_CtrlNo = value; }
    }
    public string Warehouse_LOA8106
    {
        get { return warehouse_LOA8106; }
        set { warehouse_LOA8106 = value; }
    }
    public string Warehouse_LOANo
    {
        get { return warehouse_LOANo; }
        set { warehouse_LOANo = value; }
    }
    public string Warehouse_RequesterName
    {
        get { return warehouse_RequesterName; }
        set { warehouse_RequesterName = value; }
    }
    public string Warehouse_SupplierName
    {
        get { return warehouse_SupplierName; }
        set { warehouse_SupplierName = value; }
    }


    public string No
    {
        get { return no; }
        set { no = value; }
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
    

    public string PurDeptManagerName
    {
        get { return purDeptManagerName; }
        set { purDeptManagerName = value; }
    }
    public string PurDeptManager
    {
        get { return purDeptManager; }
        set { purDeptManager = value; }
    }
    public string PurDeptManagerDOA
    {
        get { return purDeptManagerDOA; }
        set { purDeptManagerDOA = value; }
    }
    public string PurDeptManagerStat
    {
        get { return purDeptManagerStat; }
        set { purDeptManagerStat = value; }
    }

    public string StatAll
    {
        get { return statAll; }
        set { statAll = value; }
    }
    public string SearchItem
    {
        get { return searchItem; }
        set { searchItem = value; }
    }
    public string IsImpex
    {
        get { return isImpex; }
        set { isImpex = value; }
    }
    public string SelectAll
    {
        get { return selectAll; }
        set { selectAll = value; }
    }

    public string CategoryDescription
    {
        get { return categoryDescription; }
        set { categoryDescription = value; }
    }
    public string LOADescription
    {
        get { return lOADescription; }
        set { lOADescription = value; }
    }
    public string POPDescription
    {
        get { return pOPDescription; }
        set { pOPDescription = value; }
    }

    public string UOM_Description
    {
        get { return uom_Description; }
        set { uom_Description = value; }
    }

    public string ReqInchargeName
    {
        get { return reqInchargeName; }
        set { reqInchargeName = value; }
    }
    public string ReqManagerName
    {
        get { return reqManagerName; }
        set { reqManagerName = value; }
    }
    public string PurInchargeName
    {
        get { return purInchargeName; }
        set { purInchargeName = value; }
    }
    public string PurManagerName
    {
        get { return purManagerName; }
        set { purManagerName = value; }
    }


    public string RefPRPO
    {
        get { return refPRPO; }
        set { refPRPO = value; }
    }
    public string SalesInvoice
    {
        get { return salesInvoice; }
        set { salesInvoice = value; }
    }
    public string BrandMachineName
    {
        get { return brandMachineName; }
        set { brandMachineName = value; }
    }
    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }
    public string Specification
    {
        get { return specification; }
        set { specification = value; }
    }
    public string Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }
    public string UnitOfMeasure
    {
        get { return unitOfMeasure; }
        set { unitOfMeasure = value; }
    }
    public string SerialNo
    {
        get { return serialNo; }
        set { serialNo = value; }
    }


    public string GatePassNo
    {
        get { return gatePassNo; }
        set { gatePassNo = value; }
    }
    public string PickUpPoint
    {
        get { return pickUpPoint; }
        set { pickUpPoint = value; }
    }

    public string Department
    {
        get { return department; }
        set { department = value; }
    }
    public string Division
    {
        get { return division; }
        set { division = value; }
    }
    public string Section
    {
        get { return section; }
        set { section = value; }
    }


    public string StatRemarks
    {
        get { return statRemarks; }
        set { statRemarks = value; }
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

    public string ReqIncharge
    {
        get { return reqIncharge; }
        set { reqIncharge = value; }
    }
    public string ReqInchargeDOA
    {
        get { return reqInchargeDOA; }
        set { reqInchargeDOA = value; }
    }
    public string ReqInchargeStat
    {
        get { return reqInchargeStat; }
        set { reqInchargeStat = value; }
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

    public string PurImpex
    {
        get { return purImpex; }
        set { purImpex = value; }
    }
    public string PurImpexDOA
    {
        get { return purImpexDOA; }
        set { purImpexDOA = value; }
    }
    public string PurImpexStat
    {
        get { return purImpexStat; }
        set { purImpexStat = value; }
    }


    public string Requester
    {
        get { return requester; }
        set { requester = value; }
    }
    public string LoaSuretyBond
    {
        get { return loaSuretyBond; }
        set { loaSuretyBond = value; }
    }
    public string Loa8106
    {
        get { return loa8106; }
        set { loa8106 = value; }
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

    public string RefId
    {
        get { return refid; }
        set { refid = value; }
    }
    public string CtrlNo
    {
        get { return ctrlNo; }
        set { ctrlNo = value; }
    }
    public string Category
    {
        get { return category; }
        set { category = value; }
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
    public string SupplierEmail
    {
        get { return supplierEmail; }
        set { supplierEmail = value; }
    }
    public string Attention
    {
        get { return attention; }
        set { attention = value; }
    }

    public string TotalQuantity
    {
        get { return totalQuantity; }
        set { totalQuantity = value; }
    }
    public string PullOutServiceDate
    {
        get { return pullOutServiceDate; }
        set { pullOutServiceDate = value; }
    }
    public string DeliveryDateToRepi
    {
        get { return deliveryDateToRepi; }
        set { deliveryDateToRepi = value; }
    }
    public string ProblemEncountered
    {
        get { return problemEncountered; }
        set { problemEncountered = value; }
    }
    public string PurposeOfPullOut
    {
        get { return purposeOfPullOut; }
        set { purposeOfPullOut = value; }
    }
    public string PurposeOfPullOutName
    {
        get { return purposeOfPullOutName; }
        set { purposeOfPullOutName = value; }
    }
    public string TotalValueInUsd
    {
        get { return totalValueInUsd; }
        set { totalValueInUsd = value; }
    }
    public string LoaNo
    {
        get { return loaNo; }
        set { loaNo = value; }
    }
    public string Attachment
    {
        get { return attachment; }
        set { attachment = value; }
    }
    public string Remarks
    {
        get { return remarks; }
        set { remarks = value; }
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



}