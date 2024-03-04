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

public class Entities_RFQ_Supplier
{
    public Entities_RFQ_Supplier()
    {
    }


    private string refId;
    private string name;
    private string address;
    private string emailaddress;
    private string addedby;
    private string updatedby;
    private string isdisabled;
    private string disabledby;
    private string headrefid;
    private string categoryid;
    private string registered;
    private string evaluationEmail;
    private string receipient;
    private string responded;
    private string statDivManager;
    private string fy_SupplierId;
    private string peza;




    public string Peza
    {
        get { return peza; }
        set { peza = value; }
    }
    public string Fy_SupplierId
    {
        get { return fy_SupplierId; }
        set { fy_SupplierId = value; }
    }

    public string StatDivManager
    {
        get { return statDivManager; }
        set { statDivManager = value; }
    }

    public string EvaluationEmail
    {
        get { return evaluationEmail; }
        set { evaluationEmail = value; }
    }

    public string Registered
    {
        get { return registered; }
        set { registered = value; }
    }

    public string HeadRefId
    {
        get { return headrefid; }
        set { headrefid = value; }
    }
    public string CategoryId
    {
        get { return categoryid; }
        set { categoryid = value; }
    }

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
    public string EmailAddress
    {
        get { return emailaddress; }
        set { emailaddress = value; }
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
    public string DisabledBy
    {
        get { return disabledby; }
        set { disabledby = value; }
    }
    public string Receipient
    {
        get { return receipient; }
        set { receipient = value; }
    }
    public string Responded
    {
        get { return responded; }
        set { responded = value; }
    }



}
