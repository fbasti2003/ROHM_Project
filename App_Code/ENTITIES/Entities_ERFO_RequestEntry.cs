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

public class Entities_ERFO_RequestEntry
{
    public Entities_ERFO_RequestEntry()
    {
    }



    private string refId;
    private string ctrlno;
    private string dateOfOperationFrom;
    private string dateOfOperationTo;
    private string daysRequired;
    private string numberOfManpower;
    private string attachedDocument;
    private string purposeOfOperation;
    private string equipmentRequirements;
    private string specialInstructionsRequester;
    private string specialInstructionsPurchasing;
    private string recommendedContractor;
    private string contractorName;
    private string recommendedContractorEmailAddress;
    private string requester;
    private string transactionDate;
    private string updatedBy;
    private string updatedDate;
    private string attachment1;
    private string attachment2;
    private string category;
    private string categoryName;
    private string attachmentConfirmed;

    private string prodDeptManager;
    private string doaProdDeptManager;
    private string statProdDeptManager;
    private string prodDivManager;
    private string doaProdDivManager;
    private string statProdDivManager;
    private string purIncharge;
    private string doaPurIncharge;
    private string statPurIncharge;
    private string purDeptManager;
    private string doaPurDeptManager;
    private string statPurDeptManager;
    private string reqReceived;
    private string purReceived;
    private string doaReqReceived;
    private string doaPurReceived;

    private string sectionDepartment;
    private string local;
    private string building;
    private string floor;
    private string productionSupervisor;
    private string contactNumber;


    private string tableName;
    private string isDisabled;
    private string dropdownRefId;
    private string dropdownName;

    private string drFrom;
    private string drTo;

    private string statAll;
    private string cssColorCode;

    private string disapprovalRemarks;

    private string department;
    private string division;

    private string prodIncharge_Name;
    private string prodDeptManager_Name;
    private string prodDivManager_Name;
    private string scdIncharge_Name;
    private string scdDeptManager_Name;
    private string reqReceived_Name;
    private string purReceived_Name;

    private string supplierResponse;
    private string doaSupplierResponse;

    private string supplier_SendRecievedDate;
    private string supplier_ReceivedType;
    private string supplier_Answer;
    private string contrator_Attachment;

    private string contractorResponseType;
    private string contractorResponse;





    public string ContractorResponseType
    {
        get { return contractorResponseType; }
        set { contractorResponseType = value; }
    }
    public string ContractorResponse
    {
        get { return contractorResponse; }
        set { contractorResponse = value; }
    }

    public string Contrator_Attachment
    {
        get { return contrator_Attachment; }
        set { contrator_Attachment = value; }
    }
    public string Supplier_SendRecievedDate
    {
        get { return supplier_SendRecievedDate; }
        set { supplier_SendRecievedDate = value; }
    }
    public string Supplier_ReceivedType
    {
        get { return supplier_ReceivedType; }
        set { supplier_ReceivedType = value; }
    }
    public string Supplier_Answer
    {
        get { return supplier_Answer; }
        set { supplier_Answer = value; }
    }


    public string SupplierResponse
    {
        get { return supplierResponse; }
        set { supplierResponse = value; }
    }
    public string DoaSupplierResponse
    {
        get { return doaSupplierResponse; }
        set { doaSupplierResponse = value; }
    }


    public string ReqReceived_Name
    {
        get { return reqReceived_Name; }
        set { reqReceived_Name = value; }
    }
    public string PurReceived_Name
    {
        get { return purReceived_Name; }
        set { purReceived_Name = value; }
    }
    public string ProdIncharge_Name
    {
        get { return prodIncharge_Name; }
        set { prodIncharge_Name = value; }
    }
    public string ProdDeptManager_Name
    {
        get { return prodDeptManager_Name; }
        set { prodDeptManager_Name = value; }
    }
    public string ProdDivManager_Name
    {
        get { return prodDivManager_Name; }
        set { prodDivManager_Name = value; }
    }
    public string ScdIncharge_Name
    {
        get { return scdIncharge_Name; }
        set { scdIncharge_Name = value; }
    }
    public string ScdDeptManager_Name
    {
        get { return scdDeptManager_Name; }
        set { scdDeptManager_Name = value; }
    }

    public string RefId
    {
        get { return refId; }
        set { refId = value; }
    }
    public string Ctrlno
    {
        get { return ctrlno; }
        set { ctrlno = value; }
    }
    public string DateOfOperationFrom
    {
        get { return dateOfOperationFrom; }
        set { dateOfOperationFrom = value; }
    }
    public string DateOfOperationTo
    {
        get { return dateOfOperationTo; }
        set { dateOfOperationTo = value; }
    }
    public string DaysRequired
    {
        get { return daysRequired; }
        set { daysRequired = value; }
    }
    public string NumberOfManpower
    {
        get { return numberOfManpower; }
        set { numberOfManpower = value; }
    }
    public string AttachedDocument
    {
        get { return attachedDocument; }
        set { attachedDocument = value; }
    }
    public string PurposeOfOperation
    {
        get { return purposeOfOperation; }
        set { purposeOfOperation = value; }
    }
    public string EquipmentRequirements
    {
        get { return equipmentRequirements; }
        set { equipmentRequirements = value; }
    }
    public string SpecialInstructionsRequester
    {
        get { return specialInstructionsRequester; }
        set { specialInstructionsRequester = value; }
    }
    public string SpecialInstructionsPurchasing
    {
        get { return specialInstructionsPurchasing; }
        set { specialInstructionsPurchasing = value; }
    }
    public string RecommendedContractor
    {
        get { return recommendedContractor; }
        set { recommendedContractor = value; }
    }
    public string ContractorName
    {
        get { return contractorName; }
        set { contractorName = value; }
    }
    public string RecommendedContractorEmailAddress
    {
        get { return recommendedContractorEmailAddress; }
        set { recommendedContractorEmailAddress = value; }
    }
    public string Requester
    {
        get { return requester; }
        set { requester = value; }
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
    public string AttachmentConfirmed
    {
        get { return attachmentConfirmed; }
        set { attachmentConfirmed = value; }
    }
    public string ProdDeptManager
    {
        get { return prodDeptManager; }
        set { prodDeptManager = value; }
    }
    public string DoaProdDeptManager
    {
        get { return doaProdDeptManager; }
        set { doaProdDeptManager = value; }
    }
    public string StatProdDeptManager
    {
        get { return statProdDeptManager; }
        set { statProdDeptManager = value; }
    }
    public string ProdDivManager
    {
        get { return prodDivManager; }
        set { prodDivManager = value; }
    }
    public string DoaProdDivManager
    {
        get { return doaProdDivManager; }
        set { doaProdDivManager = value; }
    }
    public string StatProdDivManager
    {
        get { return statProdDivManager; }
        set { statProdDivManager = value; }
    }

    public string PurIncharge
    {
        get { return purIncharge; }
        set { purIncharge = value; }
    }
    public string DoaPurIncharge
    {
        get { return doaPurIncharge; }
        set { doaPurIncharge = value; }
    }
    public string StatPurIncharge
    {
        get { return statPurIncharge; }
        set { statPurIncharge = value; }
    }

    public string PurDeptManager
    {
        get { return purDeptManager; }
        set { purDeptManager = value; }
    }
    public string DoaPurDeptManager
    {
        get { return doaPurDeptManager; }
        set { doaPurDeptManager = value; }
    }
    public string StatPurDeptManager
    {
        get { return statPurDeptManager; }
        set { statPurDeptManager = value; }
    }


    public string ReqReceived
    {
        get { return reqReceived; }
        set { reqReceived = value; }
    }
    public string DoaReqReceived
    {
        get { return doaReqReceived; }
        set { doaReqReceived = value; }
    }
    public string PurReceived
    {
        get { return purReceived; }
        set { purReceived = value; }
    }
    public string DoaPurReceived
    {
        get { return doaPurReceived; }
        set { doaPurReceived = value; }
    }

    public string SectionDepartment
    {
        get { return sectionDepartment; }
        set { sectionDepartment = value; }
    }
    public string Local
    {
        get { return local; }
        set { local = value; }
    }

    public string Building
    {
        get { return building; }
        set { building = value; }
    }
    public string Floor
    {
        get { return floor; }
        set { floor = value; }
    }
    public string ProductionSupervisor
    {
        get { return productionSupervisor; }
        set { productionSupervisor = value; }
    }
    public string ContactNumber
    {
        get { return contactNumber; }
        set { contactNumber = value; }
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

    public string DisapprovalRemarks
    {
        get { return disapprovalRemarks; }
        set { disapprovalRemarks = value; }
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




}
