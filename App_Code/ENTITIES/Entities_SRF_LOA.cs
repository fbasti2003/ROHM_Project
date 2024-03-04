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


    public class Entities_SRF_LOA
    {

        public Entities_SRF_LOA()
        {
        }

        private string refId;
        private string name;
        private string addedby;
        private string updatedby;
        private string isdisabled;

        private string loano;
        private string balance;
        private string loapricevalue;
        private string maturitydate;
        private string remarks;
        private string itemname;
        private string totalquantity;
        private string pricevalue;
        private string transactiondate;




        public string ItemName
        {
            get { return itemname; }
            set { itemname = value; }
        }
        public string TotalQuantity
        {
            get { return totalquantity; }
            set { totalquantity = value; }
        }
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
        public string PriceValue
        {
            get { return pricevalue; }
            set { pricevalue = value; }
        }

        public string TransactionDate
        {
            get { return transactiondate; }
            set { transactiondate = value; }
        }
        public string LoaNo
        {
            get { return loano; }
            set { loano = value; }
        }
        public string Balance
        {
            get { return balance; }
            set { balance = value; }
        }
        public string LoaPriceValue
        {
            get { return loapricevalue; }
            set { loapricevalue = value; }
        }
        public string MaturityDate
        {
            get { return maturitydate; }
            set { maturitydate = value; }
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

