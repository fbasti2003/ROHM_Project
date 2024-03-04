using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;

namespace REPI_PUR_SOFRA
{
    public partial class RFQ_ExportToExcel : System.Web.UI.Page
    {

        BLL_RFQ BLL = new BLL_RFQ();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadDefault();
        }

        private void LoadDefault()
        {
            List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
            Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();
            entity.DrFrom = Session["RFQ_Export_From"].ToString();
            entity.DrTo = Session["RFQ_Export_To"].ToString();

            list = null;


            if (Session["RFQ_Export_Category"].ToString().ToLower() == "all category")
            {
                if (Session["RFQ_Export_Type"].ToString().ToLower() == "for sending")
                {
                    list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel(entity).Where(itm => itm.GroupBySupplierResponse == "FOR SENDING").ToList();
                }
                if (Session["RFQ_Export_Type"].ToString().ToLower() == "for resend")
                {
                    list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel(entity).Where(itm => itm.GroupBySupplierResponse == "FOR RESEND").ToList();
                }
                if (Session["RFQ_Export_Type"].ToString().ToLower() == "all approved")
                {
                    list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel_Approved(entity);
                }
                if (Session["RFQ_Export_Type"].ToString().ToLower() == "all")
                {
                    list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel(entity);
                }

            }
            else
            {
                if (Session["RFQ_Export_Type"].ToString().ToLower() == "for sending")
                {
                    list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel(entity).Where(itm => itm.RhCategory.ToLower() == Session["RFQ_Export_Category"].ToString().ToLower() && itm.GroupBySupplierResponse == "FOR SENDING").ToList();
                }
                if (Session["RFQ_Export_Type"].ToString().ToLower() == "for resend")
                {
                    list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel(entity).Where(itm => itm.RhCategory.ToLower() == Session["RFQ_Export_Category"].ToString().ToLower() && itm.GroupBySupplierResponse == "FOR RESEND").ToList();
                }
                if (Session["RFQ_Export_Type"].ToString().ToLower() == "all approved")
                {
                    list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel_Approved(entity).Where(itm => itm.RhCategory.ToLower() == Session["RFQ_Export_Category"].ToString().ToLower()).ToList();
                }
                if (Session["RFQ_Export_Type"].ToString().ToLower() == "all")
                {
                    list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel(entity).Where(itm => itm.RhCategory.ToLower() == Session["RFQ_Export_Category"].ToString().ToLower()).ToList();
                }

            }

            if (list != null)
            {
                if (list.Count > 0)
                {
                    gvExport.DataSource = list;

                    // EXPORT TO EXCEL

                    Response.Clear();
                    Response.Buffer = true;

                    Response.AddHeader("content-disposition",
                    "attachment;filename=ExportedRFQ.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    gvExport.AllowPaging = false;
                    gvExport.DataBind();

                    gvExport.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();


                }

            }

        }
    }
}
