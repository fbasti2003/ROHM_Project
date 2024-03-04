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

public class Entities_SE_EvaluationItem
{
    public Entities_SE_EvaluationItem()
    {
    }

    private string refId;
    private string item;
    private string type;




    public string RefId
    {
        get { return refId; }
        set { refId = value; }
    }
    public string Item
    {
        get { return item; }
        set { item = value; }
    }
    public string Type
    {
        get { return type; }
        set { type = value; }
    }




}
