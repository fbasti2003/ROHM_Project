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
using System.Drawing.Printing;
using SelectPdf;
using System.IO;

namespace REPI_PUR_SOFRA
{
    public partial class DownloadPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //HtmlToPdf converter = new HtmlToPdf();
                //PdfDocument doc = converter.ConvertHtmlString(File.ReadAllText("D:/test2.html"));
                //doc.Save("D:/test2.pdf");
                //doc.Close();

                //HtmlToPdf converter2 = new HtmlToPdf();
                //PdfDocument doc2 = converter2.ConvertHtmlString(File.ReadAllText("D:/test2_2.html"));
                //doc2.Save("D:/test2_2.pdf");
                //doc2.Close();

                HtmlToPdf converter2 = new HtmlToPdf();
                PdfDocument doc2 = converter2.ConvertHtmlString(File.ReadAllText("D:/URF_Single.html"));
                doc2.Save("D:/URF_Single.pdf");
                doc2.Close();



                //// load the pdf document
                //PdfDocument doc1 = converter.ConvertHtmlString(File.ReadAllText("D:/test2.txt"));

                //// create a new pdf document
                //PdfDocument doc = new PdfDocument();

                //// add 2 pages to the new document
                //if (doc1.Pages.Count > 2)
                //{
                //    doc.AddPage(doc1.Pages[1]);
                //    doc.AddPage(doc1.Pages[2]);
                //}

                //// save pdf document
                //doc.Save(Response, false, "D:/testNew.pdf");

                //// close pdf document
                //doc.Close();

                //// close the original pdf document
                //doc1.Close();




            }
        }
    }
}
