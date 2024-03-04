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

public class Entities_Common_SystemUsers
{
    public Entities_Common_SystemUsers()
    {
    }

    private long lcRefId;
    private string username;
    private string password;
    private string password2;
    private string fullName;
    private int section;
    private int department;
    private int division;
    private int pc;
    private int hq;
    private string isDisabled;
    private string divisionCode;
    private string category;

    private string sectionName;
    private string departmentName;
    private string divisionName;
    private string pcName;
    private string hqName;

    private string sectionCode;
    private string departmentCode;
    private string pcCode;
    private string hqCode;

    private string tableName;
    private string dropdownRefId;
    private string dropdownName;

    private string localNumber;

    private string formList_FormType;
    private string formList_FormName;
    private string formList_AccessValue;
    private string formList_OrderDisplay;
    private string formList_IsDefault;
    private string formList_RefId;
    private string formList_IsAllowed;
    private string updatedBy;
    private string transactionType;
    private string allowed;
    private string addedBy;
    private string emailAddress;

    private string refId;
    private string departmentString;
    private string divisionString;
    private string sectionString;
    private string addedByString;
    private string categoryString;
    private string pcString;
    private string hqString;
    private string addedDate;
    private string accessType;

    private string urf_Prod_SectionManager;
    private string urf_Prod_DepartmentManager;
    private string urf_Prod_DivisionManager;
    private string urf_Prod_HQManager;
    private string scd_Incharge;
    private string scd_DepartmentManager;
    private string scd_DivisionManager;
    private string proforma_PCManager;
    private string rfq_Prod_Approver;
    private string crf_Prod_Approver;
    private string drf_Prod_Approver;





    public string Urf_Prod_SectionManager
    {
        get { return urf_Prod_SectionManager; }
        set { urf_Prod_SectionManager = value; }
    }
    public string Urf_Prod_DepartmentManager
    {
        get { return urf_Prod_DepartmentManager; }
        set { urf_Prod_DepartmentManager = value; }
    }
    public string Urf_Prod_DivisionManager
    {
        get { return urf_Prod_DivisionManager; }
        set { urf_Prod_DivisionManager = value; }
    }
    public string Urf_Prod_HQManager
    {
        get { return urf_Prod_HQManager; }
        set { urf_Prod_HQManager = value; }
    }
    public string Scd_Incharge
    {
        get { return scd_Incharge; }
        set { scd_Incharge = value; }
    }
    public string Scd_DepartmentManager
    {
        get { return scd_DepartmentManager; }
        set { scd_DepartmentManager = value; }
    }
    public string Scd_DivisionManager
    {
        get { return scd_DivisionManager; }
        set { scd_DivisionManager = value; }
    }
    public string Proforma_PCManager
    {
        get { return proforma_PCManager; }
        set { proforma_PCManager = value; }
    }

    public string Rfq_Prod_Approver
    {
        get { return rfq_Prod_Approver; }
        set { rfq_Prod_Approver = value; }
    }
    public string Crf_Prod_Approver
    {
        get { return crf_Prod_Approver; }
        set { crf_Prod_Approver = value; }
    }
    public string Drf_Prod_Approver
    {
        get { return drf_Prod_Approver; }
        set { drf_Prod_Approver = value; }
    }



    public string SectionCode
    {
        get { return sectionCode; }
        set { sectionCode = value; }
    }
    public string DepartmentCode
    {
        get { return departmentCode; }
        set { departmentCode = value; }
    }
    public string PcCode
    {
        get { return pcCode; }
        set { pcCode = value; }
    }
    public string HqCode
    {
        get { return hqCode; }
        set { hqCode = value; }
    }

    public string AccessType
    {
        get { return accessType; }
        set { accessType = value; }
    }
    public string AddedDate
    {
        get { return addedDate; }
        set { addedDate = value; }
    }
    public string RefId
    {
        get { return refId; }
        set { refId = value; }
    }
    public string DepartmentString
    {
        get { return departmentString; }
        set { departmentString = value; }
    }
    public string DivisionString
    {
        get { return divisionString; }
        set { divisionString = value; }
    }
    public string SectionString
    {
        get { return sectionString; }
        set { sectionString = value; }
    }
    public string AddedByString
    {
        get { return addedByString; }
        set { addedByString = value; }
    }
    public string CategoryString
    {
        get { return categoryString; }
        set { categoryString = value; }
    }
    public string PcString
    {
        get { return pcString; }
        set { pcString = value; }
    }
    public string HqString
    {
        get { return hqString; }
        set { hqString = value; }
    }

    public string EmailAddress
    {
        get { return emailAddress; }
        set { emailAddress = value; }
    }
    public string AddedBy
    {
        get { return addedBy; }
        set { addedBy = value; }
    }
    public string Allowed
    {
        get { return allowed; }
        set { allowed = value; }
    }
    public string TransactionType
    {
        get { return transactionType; }
        set { transactionType = value; }
    }
    public string UpdatedBy
    {
        get { return updatedBy; }
        set { updatedBy = value; }
    }
    public string FormList_IsAllowed
    {
        get { return formList_IsAllowed; }
        set { formList_IsAllowed = value; }
    }
    public string FormList_RefId
    {
        get { return formList_RefId; }
        set { formList_RefId = value; }
    }
    public string FormList_FormType
    {
        get { return formList_FormType; }
        set { formList_FormType = value; }
    }
    public string FormList_FormName
    {
        get { return formList_FormName; }
        set { formList_FormName = value; }
    }
    public string FormList_AccessValue
    {
        get { return formList_AccessValue; }
        set { formList_AccessValue = value; }
    }
    public string FormList_OrderDisplay
    {
        get { return formList_OrderDisplay; }
        set { formList_OrderDisplay = value; }
    }
    public string FormList_IsDefault
    {
        get { return formList_IsDefault; }
        set { formList_IsDefault = value; }
    }

    public string LocalNumber
    {
        get { return localNumber; }
        set { localNumber = value; }
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

    public string Category
    {
        get { return category; }
        set { category = value; }
    }

    public string DivisionCode
    {
        get { return divisionCode; }
        set { divisionCode = value; }
    }

    public long LcRefId
    {
        get { return lcRefId; }
        set { lcRefId = value; }
    }

    public string Username
    {
        get { return username; }
        set { username = value; }
    }

    public string Password
    {
        get { return password; }
        set { password = value; }
    }
    public string Password2
    {
        get { return password2; }
        set { password2 = value; }
    }

    public string FullName
    {
        get { return fullName; }
        set { fullName = value; }
    }

    public int Section
    {
        get { return section; }
        set { section = value; }
    }

    public int Department
    {
        get { return department; }
        set { department = value; }
    }

    public int Division
    {
        get { return division; }
        set { division = value; }
    }

    public int PC
    {
        get { return pc; }
        set { pc = value; }
    }

    public int HQ
    {
        get { return hq; }
        set { hq = value; }
    }

    public string IsDisabled
    {
        get { return isDisabled; }
        set { isDisabled = value; }
    }
}
