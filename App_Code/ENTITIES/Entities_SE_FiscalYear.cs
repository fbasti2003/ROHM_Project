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

public class Entities_SE_FiscalYear
{
    public Entities_SE_FiscalYear()
    {
    }

    private string refId;
    private string from;
    private string to;
    private string description;
    private string addedby;
    private string updatedby;
    private string isdisabled;
    private string disabledBy;
    private string receipient;
    private string responded;
    private string name;
    private string evaluationEmail;

    private string incharge;
    private string doaIncharge;
    private string sectionIncharge;
    private string doaSectionIncharge;
    private string deptManager;
    private string doaDeptManager;
    private string divManager;
    private string doaDivManager;
    private string forApproval;

    private string statSectionIncharge;
    private string statDeptManager;
    private string statDivManager;
    private string posted;
    private string postedBy;
    private string disapprovalRemarks;
    private string postedDate;
    private string fy_SupplierId;
    private string supplierName;
    private string supplierId;
    private string status;








    public string Status
    {
        get { return status; }
        set { status = value; }
    }

    public string SupplierName
    {
        get { return supplierName; }
        set { supplierName = value; }
    }

    public string SupplierId
    {
        get { return supplierId; }
        set { supplierId = value; }
    }

    public string Fy_SupplierId
    {
        get { return fy_SupplierId; }
        set { fy_SupplierId = value; }
    }

    public string PostedDate
    {
        get { return postedDate; }
        set { postedDate = value; }
    }
    public string StatSectionIncharge
    {
        get { return statSectionIncharge; }
        set { statSectionIncharge = value; }
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
    public string DisapprovalRemarks
    {
        get { return disapprovalRemarks; }
        set { disapprovalRemarks = value; }
    }

    public string ForApproval
    {
        get { return forApproval; }
        set { forApproval = value; }
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
    public string SectionIncharge
    {
        get { return sectionIncharge; }
        set { sectionIncharge = value; }
    }
    public string DoaSectionIncharge
    {
        get { return doaSectionIncharge; }
        set { doaSectionIncharge = value; }
    }
    public string DeptManager
    {
        get { return deptManager; }
        set { deptManager = value; }
    }
    public string DoaDeptManager
    {
        get { return doaDeptManager; }
        set { doaDeptManager = value; }
    }
    public string DoaDivManager
    {
        get { return doaDivManager; }
        set { doaDivManager = value; }
    }
    public string DivManager
    {
        get { return divManager; }
        set { divManager = value; }
    }


    public string RefId
    {
        get { return refId; }
        set { refId = value; }
    }

    public string From
    {
        get { return from; }
        set { from = value; }
    }

    public string To
    {
        get { return to; }
        set { to = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public string Addedby
    {
        get { return addedby; }
        set { addedby = value; }
    }

    public string Updatedby
    {
        get { return updatedby; }
        set { updatedby = value; }
    }

    public string Isdisabled
    {
        get { return isdisabled; }
        set { isdisabled = value; }
    }

    public string DisabledBy
    {
        get { return disabledBy; }
        set { disabledBy = value; }
    }

    public string Receipient
    {
        get { return receipient; }
        set { receipient = value; }
    }

    public string Responded
    {
        get { return responded; }
        set { responded = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string EvaluationEmail
    {
        get { return evaluationEmail; }
        set { evaluationEmail = value; }
    }

}
