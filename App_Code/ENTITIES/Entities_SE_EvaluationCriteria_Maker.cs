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


public class Entities_SE_EvaluationCriteria_Maker
{
    public Entities_SE_EvaluationCriteria_Maker()
    {
    }



    private string refId;
    private string item;
    private string itemName;
    private string criteria;
    private string points;
    private string judgement;
    private string addedby;
    private string updatedby;
    private string isdisabled;
    private string disabledBy;






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
    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }
    public string Criteria
    {
        get { return criteria; }
        set { criteria = value; }
    }
    public string Points
    {
        get { return points; }
        set { points = value; }
    }
    public string Judgement
    {
        get { return judgement; }
        set { judgement = value; }
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



}
