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

public class Entities_ERFO_RecommendedContractor
{
    public Entities_ERFO_RecommendedContractor()
    {
    }

    private string refId;
    private string contractorName;
    private string emailAddress;
    private string addedby;
    private string updatedby;
    private string isdisabled;
    private string updatedDate;
    private string addedDate;


    public string AddedDate
    {
        get { return addedDate; }
        set { addedDate = value; }
    }

    public string UpdatedDate
    {
        get { return updatedDate; }
        set { updatedDate = value; }
    }

    public string RefId
    {
        get { return refId; }
        set { refId = value; }
    }
    public string ContractorName
    {
        get { return contractorName; }
        set { contractorName = value; }
    }
    public string EmailAddress
    {
        get { return emailAddress; }
        set { emailAddress = value; }
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




}
