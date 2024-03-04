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

public class Entities_SRF_PO_Entry
{
    public Entities_SRF_PO_Entry()
    {
    }

    private string head_RefId;
    private string head_Ctrlno;
    private string head_TotalBoxes;
    private string head_TotalQuantity;
    private string head_Requester;
    private string head_TransactionDate;
    private string head_ProdManager;
    private string head_DOAProdManager;
    private string head_Buyer;
    private string head_DOABuyer;
    private string head_SCIncharge;
    private string head_DOASCIncharge;
    private string head_Type;
    private string head_StatProdManager;
    private string head_StatBuyer;
    private string head_StatSCIncharge;

    private string head_RequesterId;


    private string refid;
    private string ctrlno;
    private string specification;
    private string boxType;
    private string size;
    private string weightOfBox;
    private string grossWeight;
    private string netWeight;
    private string multiplier;
    private string noOfBoxes;
    private string quantity;
    private string srfItemName;

    private string crFrom;
    private string crTo;

    private string statAll;
    private string cssColorCode;

    private string division;
    private string disapprovalRemarks;

    private string divisionDisplay;
    private string supplier;
    private string supplierEmail;
    private string supplierName;



    public string SupplierName
    {
        get { return supplierName; }
        set { supplierName = value; }
    }
    public string SupplierEmail
    {
        get { return supplierEmail; }
        set { supplierEmail = value; }
    }
    public string SrfItemName
    {
        get { return srfItemName; }
        set { srfItemName = value; }
    }
    public string DivisionDisplay
    {
        get { return divisionDisplay; }
        set { divisionDisplay = value; }
    }
    public string Supplier
    {
        get { return supplier; }
        set { supplier = value; }
    }

    public string Head_RequesterId
    {
        get { return head_RequesterId; }
        set { head_RequesterId = value; }
    }

    public string DisapprovalRemarks
    {
        get { return disapprovalRemarks; }
        set { disapprovalRemarks = value; }
    }
    public string Division
    {
        get { return division; }
        set { division = value; }
    }

    public string Head_StatProdManager
    {
        get { return head_StatProdManager; }
        set { head_StatProdManager = value; }
    }
    public string Head_StatBuyer
    {
        get { return head_StatBuyer; }
        set { head_StatBuyer = value; }
    }
    public string Head_StatSCIncharge
    {
        get { return head_StatSCIncharge; }
        set { head_StatSCIncharge = value; }
    }

    public string StatAll
    {
        get { return statAll; }
        set { statAll = value; }
    }
    public string CssColorCode
    {
        get { return cssColorCode; }
        set { cssColorCode = value; }
    }

    public string CrFrom
    {
        get { return crFrom; }
        set { crFrom = value; }
    }

    public string CrTo
    {
        get { return crTo; }
        set { crTo = value; }
    }


    public string Head_Type
    {
        get { return head_Type; }
        set { head_Type = value; }
    }
    public string Head_RefId
    {
        get { return head_RefId; }
        set { head_RefId = value; }
    }
    public string Head_Ctrlno
    {
        get { return head_Ctrlno; }
        set { head_Ctrlno = value; }
    }
    public string Head_TotalBoxes
    {
        get { return head_TotalBoxes; }
        set { head_TotalBoxes = value; }
    }
    public string Head_TotalQuantity
    {
        get { return head_TotalQuantity; }
        set { head_TotalQuantity = value; }
    }
    public string Head_Requester
    {
        get { return head_Requester; }
        set { head_Requester = value; }
    }
    public string Head_TransactionDate
    {
        get { return head_TransactionDate; }
        set { head_TransactionDate = value; }
    }
    public string Head_ProdManager
    {
        get { return head_ProdManager; }
        set { head_ProdManager = value; }
    }
    public string Head_DOAProdManager
    {
        get { return head_DOAProdManager; }
        set { head_DOAProdManager = value; }
    }
    public string Head_Buyer
    {
        get { return head_Buyer; }
        set { head_Buyer = value; }
    }
    public string Head_DOABuyer
    {
        get { return head_DOABuyer; }
        set { head_DOABuyer = value; }
    }
    public string Head_SCIncharge
    {
        get { return head_SCIncharge; }
        set { head_SCIncharge = value; }
    }
    public string Head_DOASCIncharge
    {
        get { return head_DOASCIncharge; }
        set { head_DOASCIncharge = value; }
    }


    public string Refid
    {
        get { return refid; }
        set { refid = value; }
    }
    public string Ctrlno
    {
        get { return ctrlno; }
        set { ctrlno = value; }
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
    public string WeightOfBox
    {
        get { return weightOfBox; }
        set { weightOfBox = value; }
    }
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
    public string Multiplier
    {
        get { return multiplier; }
        set { multiplier = value; }
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





}
