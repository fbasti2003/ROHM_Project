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

public class Entities_SRF_PO_Others
{
    public Entities_SRF_PO_Others()
    {
    }


    private string refId;
    private string specification;
    private string addedby;
    private string updatedby;
    private string isdisabled;
    private string srfItemName;
    private string quantity;




    public string RefId
    {
        get { return refId; }
        set { refId = value; }
    }
    public string Specification
    {
        get { return specification; }
        set { specification = value; }
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
    public string SrfItemName
    {
        get { return srfItemName; }
        set { srfItemName = value; }
    }
    public string Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }




}
