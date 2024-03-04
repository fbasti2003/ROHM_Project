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

public class Entities_PIPL_Company
{
    public Entities_PIPL_Company()
    {
    }

    private string refId;
    private string name;
    private string address;
    private string phone;
    private string fax;
    private string addedby;
    private string updatedby;
    private string isdisabled;


    public string RefId
    {
        get { return refId; }
        set { refId = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public string Address
    {
        get { return address; }
        set { address = value; }
    }
    public string Phone
    {
        get { return phone; }
        set { phone = value; }
    }
    public string Fax
    {
        get { return fax; }
        set { fax = value; }
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
