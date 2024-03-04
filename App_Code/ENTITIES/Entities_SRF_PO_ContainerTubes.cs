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

public class Entities_SRF_PO_ContainerTubes
{
    public Entities_SRF_PO_ContainerTubes()
    {
    }

    private string refId;
    private string specification;
    private string weightOfBox;
    private string multiplier;
    private string grossWeight;
    private string netWeight;
    private string addedby;
    private string updatedby;
    private string isdisabled;
    private string noOfBoxes;
    private string quantity;





    public string GrossWeight
    {
        get { return grossWeight; }
        set { grossWeight = value; }
    }
    public string NetWeight
    {
        get { return netWeight; }
        set { netWeight = value; }
    }
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
    public string WeightOfBox
    {
        get { return weightOfBox; }
        set { weightOfBox = value; }
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
