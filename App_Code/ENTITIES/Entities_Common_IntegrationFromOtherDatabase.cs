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

public class Entities_Common_IntegrationFromOtherDatabase
{
    public Entities_Common_IntegrationFromOtherDatabase()
    {
    }


    private string refid;
    private string description;

    public string Refid
    {
        get { return refid; }
        set { refid = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

}
