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

public class Entities_RFQ_Section
{
    public Entities_RFQ_Section()
    {
    }

    private string refId;
    private string code;
    private string description;
    private string addedby;
    private string updatedby;
    private string isdisabled;
    private string disabledBy;
    private string updatedDate;
    private string addedDate;
    private string roprosCode;






    public string DisabledBy
    {
        get { return disabledBy; }
        set { disabledBy = value; }
    }
    public string RoprosCode
    {
        get { return roprosCode; }
        set { roprosCode = value; }
    }
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
    public string Code
    {
        get { return code; }
        set { code = value; }
    }
    public string Description
    {
        get { return description; }
        set { description = value; }
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