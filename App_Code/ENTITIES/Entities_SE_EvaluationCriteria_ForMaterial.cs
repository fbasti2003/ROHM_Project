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

public class Entities_SE_EvaluationCriteria_ForMaterial
{
    public Entities_SE_EvaluationCriteria_ForMaterial()
    {
    }




    private string refId;
    private string item;
    private string itemName;
    private string criteria;
    private string points;
    private string weight;
    private string score;
    private string addedby;
    private string updatedby;
    private string isdisabled;
    private string disabledBy;
    private string level;
    private string rowsPan;







    public string RowsPan
    {
        get { return rowsPan; }
        set { rowsPan = value; }
    }
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
    public string Weight
    {
        get { return weight; }
        set { weight = value; }
    }
    public string Score
    {
        get { return score; }
        set { score = value; }
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
    public string Level
    {
        get { return level; }
        set { level = value; }
    }







}
