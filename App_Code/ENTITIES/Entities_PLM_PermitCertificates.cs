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

public class Entities_PLM_PermitCertificates
{
    public Entities_PLM_PermitCertificates()
    {
    }

    private string refId;
    private string permitName;
    private string permitId;
    private string chemicalContent;
    private string itemName;
    private string specification;
    private string governmentAgency;
    private string frequency;
    private string specifiedRequirements;
    private string leadTime;
    private string safety;
    private string processorName;
    private string supplier;
    private string status;
    private string issuedDate;
    private string expirationDate;
    private string daysLeft;

    private string addedby;
    private string updatedby;
    private string isdisabled;
    private string addedDate;


    private string supplierCode;
    private string supplierName;
    private string supplierEmailAddress;
    private string supplierSelected;

    private string tableName;
    private string isDisabled;
    private string dropdownRefId;
    private string dropdownName;

    private string attachment1;
    private string attachment2;
    private string attachment3;

    private string takeAction;
    private string expired;
    private string renewed;

    private string colorCode;

    private string notifiedAlready;




    public string NotifiedAlready
    {
        get { return notifiedAlready; }
        set { notifiedAlready = value; }
    }

    public string RefId
    {
        get { return refId; }
        set { refId = value; }
    }
    public string PermitName
    {
        get { return permitName; }
        set { permitName = value; }
    }
    public string PermitId
    {
        get { return permitId; }
        set { permitId = value; }
    }
    public string ChemicalContent
    {
        get { return chemicalContent; }
        set { chemicalContent = value; }
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
    public string GovernmentAgency
    {
        get { return governmentAgency; }
        set { governmentAgency = value; }
    }
    public string Frequency
    {
        get { return frequency; }
        set { frequency = value; }
    }
    public string SpecifiedRequirements
    {
        get { return specifiedRequirements; }
        set { specifiedRequirements = value; }
    }
    public string LeadTime
    {
        get { return leadTime; }
        set { leadTime = value; }
    }
    public string Safety
    {
        get { return safety; }
        set { safety = value; }
    }
    public string ProcessorName
    {
        get { return processorName; }
        set { processorName = value; }
    }
    public string Supplier
    {
        get { return supplier; }
        set { supplier = value; }
    }
    public string Status
    {
        get { return status; }
        set { status = value; }
    }
    public string IssuedDate
    {
        get { return issuedDate; }
        set { issuedDate = value; }
    }
    public string ExpirationDate
    {
        get { return expirationDate; }
        set { expirationDate = value; }
    }
    public string DaysLeft
    {
        get { return daysLeft; }
        set { daysLeft = value; }
    }
    public string AddedBy
    {
        get { return addedby; }
        set { addedby = value; }
    }
    public string UpdatedBy
    {
        get { return updatedby; }
        set { updatedby = value; }
    }
    public string IsDisabled
    {
        get { return isdisabled; }
        set { isdisabled = value; }
    }
    public string AddedDate
    {
        get { return addedDate; }
        set { addedDate = value; }
    }



    public string SupplierCode
    {
        get { return supplierCode; }
        set { supplierCode = value; }
    }
    public string SupplierName
    {
        get { return supplierName; }
        set { supplierName = value; }
    }
    public string SupplierEmailAddress
    {
        get { return supplierEmailAddress; }
        set { supplierEmailAddress = value; }
    }
    public string SupplierSelected
    {
        get { return supplierSelected; }
        set { supplierSelected = value; }
    }


    public string TableName
    {
        get { return tableName; }
        set { tableName = value; }
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

    public string TakeAction
    {
        get { return takeAction; }
        set { takeAction = value; }
    }
    public string Expired
    {
        get { return expired; }
        set { expired = value; }
    }
    public string Renewed
    {
        get { return renewed; }
        set { renewed = value; }
    }

    public string ColorCode
    {
        get { return colorCode; }
        set { colorCode = value; }
    }


}
