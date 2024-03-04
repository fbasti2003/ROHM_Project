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

public class Entities_RFQ_BuyerInformation
{
    public Entities_RFQ_BuyerInformation()
    {
    }

    private string refId;
    private string member;
    private string section;
    private string email;
    private string mobile;
    private string addedBy;
    private string addedDate;
    private string updatedBy;
    private string updatedDate;
    private string isdisabled;



    public string RefId
    {
        get { return refId; }
        set { refId = value; }
    }

    public string Member
    {
        get { return member; }
        set { member = value; }
    }

    public string Section
    {
        get { return section; }
        set { section = value; }
    }

    public string Email
    {
        get { return email; }
        set { email = value; }
    }

    public string Mobile
    {
        get { return mobile; }
        set { mobile = value; }
    }


    public string AddedBy
    {
        get { return addedBy; }
        set { addedBy = value; }
    }

    public string AddedDate
    {
        get { return addedDate; }
        set { addedDate = value; }
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

    public string IsDisabled
    {
        get { return isdisabled; }
        set { isdisabled = value; }
    }



}
