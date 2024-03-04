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

public class Entities_SRF_PO_ICTrays
{
    public Entities_SRF_PO_ICTrays()
    {
    }

    private string refId;
    private string specification;
    private string boxType;
    private string size;
    private string multiplier;
    private string addedby;
    private string updatedby;
    private string isdisabled;
    private string noOfBoxes;
    private string quantity;


    public string NoOfBoxes
    {
        get { return noOfBoxes; }
        set { noOfBoxes = value; }
    }
    public string Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }

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
    public string BoxType
    {
        get { return boxType; }
        set { boxType = value; }
    }
    public string Size
    {
        get { return size; }
        set { size = value; }
    }
    public string Multiplier
    {
        get { return multiplier; }
        set { multiplier = value; }
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
