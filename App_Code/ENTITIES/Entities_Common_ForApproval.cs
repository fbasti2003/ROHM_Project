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

public class Entities_Common_ForApproval
{
    public Entities_Common_ForApproval()
    {
    }

    private string transactionName;
    private string forApprovalCount;

    private string thisWeek_Request;
    private string thisWeek_Pending;
    private string thisWeek_Approved;
    private string thiwWeek_Disapproved;

    private string percentage_Request;
    private string percentage_Pending;
    private string percentage_Approved;
    private string percentage_Disapproved;

    private string approval_RefId;
    private string approval_Fullname;
    private string approval_EmailAddress;
    private string approval_Section;
    private string approval_Department;
    private string approval_Division;
    private string approval_HQ;
    private string approval_CRF_ForApproval;
    private string approval_DRF_ForApproval;
    private string approval_URF_ForApproval_ProdSecManager;
    private string approval_URF_ForApproval_ProdDeptManager;
    private string approval_URF_ForApproval_ProdDivManager;
    private string approval_URF_ForApproval_ProdHQManager;
    private string approval_URF_ForApproval_Buyer;
    private string approval_URF_ForApproval_PurchasingManager;

    private string num;

    private string otherBuyer_CategoryName;
    private string otherBuyer_Category;
    private string otherBuyer_ForSending;
    private string otherBuyer_ForApproval;
    private string otherBuyer_CRFSending;
    private string otherBuyer_CRFApproval;
    private string otherBuyer_DRFSending;
    private string otherBuyer_DRFApproval;
    private string otherBuyer_URFSending;
    private string otherBuyer_URFApproval;
    private string otherBuyer_SRFApproval;
    private string otherBuyer_PROFORMAApproval;
    private string otherBuyer_ERFOApproval;












    public string OtherBuyer_ERFOApproval
    {
        get { return otherBuyer_ERFOApproval; }
        set { otherBuyer_ERFOApproval = value; }
    }
    public string OtherBuyer_CRFSending
    {
        get { return otherBuyer_CRFSending; }
        set { otherBuyer_CRFSending = value; }
    }
    public string OtherBuyer_CRFApproval
    {
        get { return otherBuyer_CRFApproval; }
        set { otherBuyer_CRFApproval = value; }
    }
    public string OtherBuyer_DRFSending
    {
        get { return otherBuyer_DRFSending; }
        set { otherBuyer_DRFSending = value; }
    }
    public string OtherBuyer_DRFApproval
    {
        get { return otherBuyer_DRFApproval; }
        set { otherBuyer_DRFApproval = value; }
    }
    public string OtherBuyer_URFSending
    {
        get { return otherBuyer_URFSending; }
        set { otherBuyer_URFSending = value; }
    }
    public string OtherBuyer_URFApproval
    {
        get { return otherBuyer_URFApproval; }
        set { otherBuyer_URFApproval = value; }
    }
    public string OtherBuyer_SRFApproval
    {
        get { return otherBuyer_SRFApproval; }
        set { otherBuyer_SRFApproval = value; }
    }
    public string OtherBuyer_PROFORMAApproval
    {
        get { return otherBuyer_PROFORMAApproval; }
        set { otherBuyer_PROFORMAApproval = value; }
    }





    public string OtherBuyer_CategoryName
    {
        get { return otherBuyer_CategoryName; }
        set { otherBuyer_CategoryName = value; }
    }
    public string OtherBuyer_Category
    {
        get { return otherBuyer_Category; }
        set { otherBuyer_Category = value; }
    }
    public string OtherBuyer_ForSending
    {
        get { return otherBuyer_ForSending; }
        set { otherBuyer_ForSending = value; }
    }
    public string OtherBuyer_ForApproval
    {
        get { return otherBuyer_ForApproval; }
        set { otherBuyer_ForApproval = value; }
    }


    public string Num
    {
        get { return num; }
        set { num = value; }
    }

    public string Approval_Division
    {
        get { return approval_Division; }
        set { approval_Division = value; }
    }
    public string Approval_HQ
    {
        get { return approval_HQ; }
        set { approval_HQ = value; }
    }

    public string Approval_RefId
    {
        get { return approval_RefId; }
        set { approval_RefId = value; }
    }
    public string Approval_Fullname
    {
        get { return approval_Fullname; }
        set { approval_Fullname = value; }
    }
    public string Approval_EmailAddress
    {
        get { return approval_EmailAddress; }
        set { approval_EmailAddress = value; }
    }
    public string Approval_Section
    {
        get { return approval_Section; }
        set { approval_Section = value; }
    }
    public string Approval_Department
    {
        get { return approval_Department; }
        set { approval_Department = value; }
    }
    public string Approval_CRF_ForApproval
    {
        get { return approval_CRF_ForApproval; }
        set { approval_CRF_ForApproval = value; }
    }
    public string Approval_DRF_ForApproval
    {
        get { return approval_DRF_ForApproval; }
        set { approval_DRF_ForApproval = value; }
    }
    public string Approval_URF_ForApproval_ProdSecManager
    {
        get { return approval_URF_ForApproval_ProdSecManager; }
        set { approval_URF_ForApproval_ProdSecManager = value; }
    }
    public string Approval_URF_ForApproval_ProdDeptManager
    {
        get { return approval_URF_ForApproval_ProdDeptManager; }
        set { approval_URF_ForApproval_ProdDeptManager = value; }
    }
    public string Approval_URF_ForApproval_ProdDivManager
    {
        get { return approval_URF_ForApproval_ProdDivManager; }
        set { approval_URF_ForApproval_ProdDivManager = value; }
    }
    public string Approval_URF_ForApproval_ProdHQManager
    {
        get { return approval_URF_ForApproval_ProdHQManager; }
        set { approval_URF_ForApproval_ProdHQManager = value; }
    }
    public string Approval_URF_ForApproval_Buyer
    {
        get { return approval_URF_ForApproval_Buyer; }
        set { approval_URF_ForApproval_Buyer = value; }
    }
    public string Approval_URF_ForApproval_PurchasingManager
    {
        get { return approval_URF_ForApproval_PurchasingManager; }
        set { approval_URF_ForApproval_PurchasingManager = value; }
    }

    public string Percentage_Request
    {
        get { return percentage_Request; }
        set { percentage_Request = value; }
    }
    public string Percentage_Pending
    {
        get { return percentage_Pending; }
        set { percentage_Pending = value; }
    }
    public string Percentage_Approved
    {
        get { return percentage_Approved; }
        set { percentage_Approved = value; }
    }
    public string Percentage_Disapproved
    {
        get { return percentage_Disapproved; }
        set { percentage_Disapproved = value; }
    }

    public string ThisWeek_Request
    {
        get { return thisWeek_Request; }
        set { thisWeek_Request = value; }
    }
    public string ThisWeek_Pending
    {
        get { return thisWeek_Pending; }
        set { thisWeek_Pending = value; }
    }
    public string ThisWeek_Approved
    {
        get { return thisWeek_Approved; }
        set { thisWeek_Approved = value; }
    }
    public string ThiwWeek_Disapproved
    {
        get { return thiwWeek_Disapproved; }
        set { thiwWeek_Disapproved = value; }
    }

    public string TransactionName
    {
        get { return transactionName; }
        set { transactionName = value; }
    }
    public string ForApprovalCount
    {
        get { return forApprovalCount; }
        set { forApprovalCount = value; }
    }

}
