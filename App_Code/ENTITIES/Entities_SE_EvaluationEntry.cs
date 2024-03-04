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

public class Entities_SE_EvaluationEntry
{
    public Entities_SE_EvaluationEntry()
    {
    }

    private string refId;
    private string fiscalYear;



    public string RefId
    {
        get { return refId; }
        set { refId = value; }
    }
    public string FiscalYear
    {
        get { return fiscalYear; }
        set { fiscalYear = value; }
    }




}
