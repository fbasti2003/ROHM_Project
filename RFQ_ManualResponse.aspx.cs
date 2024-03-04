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
using System.IO;
using System.Xml;
using System.Data.Common;
using System.Collections.Generic;

namespace REPI_PUR_SOFRA
{
    public partial class RFQ_ManualResponse : System.Web.UI.Page
    {
        Common COMMON = new Common();
        public string supplier;
        public string rfqNo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["RFQNo_From_ManualResponse"].ToString()))
                {
                    rfqNo = CryptorEngine.Decrypt(Request.QueryString["RFQNo_From_ManualResponse"].ToString().Replace(" ", "+"), true);
                    supplier = Request.QueryString["RFQNo_From_ManualResponse_Supplier"].ToString();
                }
            }
        }

        protected void btnManualPull_Click(object sender, EventArgs e)
        {
            try
            {
                OpenPop.Pop3.Pop3Client PopClient = new OpenPop.Pop3.Pop3Client();
                PopClient.Connect("smtp.office365.com", 995, true);
                PopClient.Authenticate(ConfigurationManager.AppSettings["email-username"], ConfigurationManager.AppSettings["email-password"], OpenPop.Pop3.AuthenticationMethod.UsernameAndPassword);

                if (PopClient.Connected)
                {
                    int msgCount = PopClient.GetMessageCount();

                    for (int x = 1; x <= msgCount; x++)
                    {
                        var Messages = new List<OpenPop.Mime.Message>(msgCount);
                        OpenPop.Mime.Message getMessage = PopClient.GetMessage(x);
                        Messages.Add(getMessage);

                        foreach (OpenPop.Mime.Message msg in Messages)
                        {
                            foreach (var attachment in msg.FindAllAttachments())
                            {
                                string validXML = string.Empty;
                                string validSubject = string.Empty;

                                if (attachment.FileName.ToLower().Contains(".xml"))
                                {
                                    validSubject = msg.Headers.Subject.Remove(0, msg.Headers.Subject.IndexOf("_") + 1);
                                    validXML = attachment.FileName.Remove(0, attachment.FileName.IndexOf("_") + 1).Replace(".xml", "");                                    

                                    if (validSubject.ToLower().Trim() == validXML.ToLower().Trim())
                                    {
                                        string supplierId = attachment.FileName.Substring(0, attachment.FileName.IndexOf("_")).ToString();

                                        if (validSubject.Trim().ToLower() == txtRFQNo.Text.ToLower())
                                        {
                                            //-----------------------------------------------------------------------------------------------------------------
                                            if (!System.IO.Directory.Exists(Server.MapPath("~/RFQ_XML_DUMP/" + validXML)))
                                            {
                                                System.IO.Directory.CreateDirectory(Server.MapPath("~/RFQ_XML_DUMP/" + validXML));
                                            }

                                            if (!System.IO.File.Exists(Server.MapPath("~/RFQ_XML_DUMP/" + validXML + "/" + attachment.FileName)))
                                            {
                                                FileStream Stream = new FileStream("~/RFQ_XML_DUMP/" + validXML + "/" + attachment.FileName, FileMode.Create);
                                                BinaryWriter BinaryStream = new BinaryWriter(Stream);
                                                BinaryStream.Write(attachment.Body);
                                                BinaryStream.Close();
                                                Stream.Close();
                                                Stream.Dispose();


                                                XmlDocument mainXml = new XmlDocument();
                                                mainXml.Load(Server.MapPath("~/RFQ_XML_DUMP/" + validXML + "/" + attachment.FileName));
                                                XmlNodeList xmlNodeList = mainXml.SelectNodes("TABLE/REQUEST");

                                                try
                                                {
                                                    foreach (XmlNode xn in xmlNodeList)
                                                    {
                                                        string detailsRefId = xn["RefId"].InnerText;
                                                        string responsePrice = string.Empty;
                                                        if (COMMON.isNumeric(xn["PricePerUnit"].InnerText, System.Globalization.NumberStyles.Currency))
                                                        {
                                                            responsePrice = xn["PricePerUnit"].InnerText;
                                                        }
                                                        else
                                                        {
                                                            responsePrice = "0";
                                                        }
                                                        string responseLead = setValidLeadTime(xn["LeadTime"].InnerText.ToString().Trim());
                                                        string supplierRemarks = xn["SupplierRemarks"].InnerText.Replace("'", "''");
                                                        string rCurrency = xn["Currency"].InnerText;

                                                        if (!isThisResponseExist(rfqNo.Trim(), supplierId, "555", detailsRefId))
                                                        {
                                                            InsertSupplierResponse(rfqNo.Trim(), supplierId, detailsRefId, responsePrice, responseLead, supplierRemarks, rCurrency, "555");
                                                        }
                                                    }

                                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('SUPPLIER RESPONSE SUCCESSFULLY SAVE');", true);
                                                }
                                                catch (Exception exXML)
                                                {
                                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('exXML - " + exXML.Message + "');", true);
                                                }

                                            }
                                            //------------------------------------------------------------------------------------------------------------------
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string validXML = string.Empty;
                string validSubject = string.Empty;
                string supplierId = string.Empty;
                string supplierId2 = string.Empty;
                rfqNo = CryptorEngine.Decrypt(Request.QueryString["RFQNo_From_ManualResponse"].ToString().Replace(" ", "+"), true);
                supplier = Request.QueryString["RFQNo_From_ManualResponse_Supplier"].ToString();
                int manualUploadXML = 0;
                int manualUploadPDF= 0;
                string manualUpload = string.Empty;


                if (fuManualUpload.HasFile || fuManualUpload_PDF.HasFile)
                {

                    if (fuManualUpload.HasFile)
                    {
                        validSubject = rfqNo.Trim();
                        validXML = fuManualUpload.FileName.Remove(0, fuManualUpload.FileName.IndexOf("_") + 1).Replace(".xml", "");
                        supplierId = Path.GetFileName(fuManualUpload.FileName).Substring(0, Path.GetFileName(fuManualUpload.FileName).IndexOf("_")).ToString();
                        supplierId2 = Request.QueryString["RFQNo_From_ManualResponse_SupplierID"].ToString().Trim();

                        if (validSubject.Trim() == validXML.Trim() && supplierId.Trim() == supplierId2.Trim())
                        {

                            if (!System.IO.Directory.Exists(Server.MapPath("~/RFQ_XML_DUMP/" + rfqNo)))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/RFQ_XML_DUMP/" + rfqNo));
                            }

                            if (!System.IO.File.Exists(Server.MapPath("~/RFQ_XML_DUMP/" + rfqNo + "/" + System.IO.Path.GetFileName(fuManualUpload.FileName))))
                            {
                                fuManualUpload.SaveAs(System.IO.Path.Combine(Server.MapPath("~/RFQ_XML_DUMP/" + rfqNo), System.IO.Path.GetFileName(fuManualUpload.FileName)));
                                fuManualUpload.Dispose();
                            }
                            else
                            {
                                File.Delete(Server.MapPath("~/RFQ_XML_DUMP/" + rfqNo + "/" + System.IO.Path.GetFileName(fuManualUpload.FileName)));
                                fuManualUpload.SaveAs(System.IO.Path.Combine(Server.MapPath("~/RFQ_XML_DUMP/" + rfqNo), System.IO.Path.GetFileName(fuManualUpload.FileName)));
                                fuManualUpload.Dispose();
                            }

                            XmlDocument mainXml = new XmlDocument();
                            mainXml.Load(Server.MapPath("~/RFQ_XML_DUMP/" + rfqNo + "/" + System.IO.Path.GetFileName(fuManualUpload.FileName)));
                            XmlNodeList xmlNodeList = mainXml.SelectNodes("TABLE/REQUEST");

                            try
                            {
                                foreach (XmlNode xn in xmlNodeList)
                                {
                                    string detailsRefId = xn["RefId"].InnerText;
                                    string responsePrice = string.Empty;
                                    if (COMMON.isNumeric(xn["PricePerUnit"].InnerText, System.Globalization.NumberStyles.Currency))
                                    {
                                        responsePrice = xn["PricePerUnit"].InnerText;
                                    }
                                    else
                                    {
                                        responsePrice = "0";
                                    }
                                    string responseLead = setValidLeadTime(xn["LeadTime"].InnerText.ToString().Trim());
                                    string supplierRemarks = xn["SupplierRemarks"].InnerText.Replace("'", "''");
                                    string rCurrency = xn["Currency"].InnerText;

                                    if (!isThisResponseExist(rfqNo.Trim(), supplierId2, "555", detailsRefId))
                                    {
                                        InsertSupplierResponse(rfqNo.Trim(), supplierId2, detailsRefId, responsePrice, responseLead, supplierRemarks, rCurrency, "555");
                                    }
                                    else
                                    {
                                        deletePreviousResponse(rfqNo.Trim(), supplierId2, "555", detailsRefId);
                                        InsertSupplierResponse(rfqNo.Trim(), supplierId2, detailsRefId, responsePrice, responseLead, supplierRemarks, rCurrency, "555");
                                    }
                                }

                                manualUploadXML++;
                                //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('SUPPLIER RESPONSE SUCCESSFULLY SAVE');", true);
                            }
                            catch (Exception exXML)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('exXML - " + exXML.Message + "');", true);
                            }

                        } // end of if (validSubject.Trim() == validXML.Trim() && supplierId.Trim() == supplierId2.Trim())
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('INVALID XML File');", true);
                        }

                    } // endo of if hasfile

                    //PDF ATTACHMENT
                    if (fuManualUpload_PDF.HasFile)
                    {
                        supplierId2 = Request.QueryString["RFQNo_From_ManualResponse_SupplierID"].ToString().Trim();

                        if (!System.IO.Directory.Exists(Server.MapPath("~/IO_Received/" + supplierId2 + "_" + rfqNo)))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath("~/IO_Received/" + supplierId2 + "_" + rfqNo));
                        }

                        if (!System.IO.File.Exists(Server.MapPath("~/IO_Received/" + rfqNo + "/" + System.IO.Path.GetFileName(fuManualUpload_PDF.FileName))))
                        {
                            fuManualUpload_PDF.SaveAs(System.IO.Path.Combine(Server.MapPath("~/IO_Received/" + supplierId2 + "_" + rfqNo), System.IO.Path.GetFileName(fuManualUpload_PDF.FileName.Replace("'", ""))));
                            fuManualUpload_PDF.Dispose();

                            InsertSupplierAttachment(rfqNo, supplierId2, fuManualUpload_PDF.FileName.Replace("'", ""));

                            manualUploadPDF++;
                        }
                    }

                    if (manualUploadXML > 0)
                    {
                        manualUpload = "XML";                        
                    }
                    if (manualUploadPDF > 0)
                    {
                        manualUpload = "PDF";                        
                    }
                    if (manualUploadXML > 0 && manualUploadPDF > 0)
                    {
                        manualUpload = "XML & PDF";   
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('SUPPLIER " + manualUpload + " RESPONSE SUCCESSFULLY SAVE');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select a valid XML or PDF File');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


        private static string setValidLeadTime(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, @"[^0-9]+", "");
        }

        private static bool isThisResponseExist(string rfqNo, string supplierId, string DateSent, string detailsRefId)
        {
            bool isExist = false;

            DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection conn = fact.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_SOFRA"].ConnectionString;

            DbDataReader reader = null;
            DbCommand cmd = fact.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TOP 1 * FROM Supplier_Response WITH (NOLOCK) WHERE RFQNo = '" + rfqNo + "' AND SupplierId = '" + supplierId + "' AND DateSent = '" + DateSent + "' AND DetailsRefId = '" + detailsRefId + "'";

            conn.Open();
            cmd.Connection = conn;

            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                isExist = true;
            }

            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;

            return isExist;
        }

        private static void deletePreviousResponse(string rfqNo, string supplierId, string DateSent, string detailsRefId)
        {
            try
            {
                DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
                DbConnection conn = fact.CreateConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_SOFRA"].ConnectionString;

                DbCommand cmd = fact.CreateCommand();
                cmd.CommandType = CommandType.Text;
                int result = 0;

                cmd.CommandText = "DELETE FROM Supplier_Response WHERE RFQNo = '" + rfqNo + "' AND DetailsRefId = '" + detailsRefId + "' AND SupplierID = '" + supplierId + "' AND DateSent = '" + DateSent + "'";

                conn.Open();
                cmd.Connection = conn;

                result = cmd.ExecuteNonQuery();

                cmd.Dispose();
                cmd = null;
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Line 485 : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n" + ex.Source);
                //Console.ReadKey();
                //InsertServiceLog("InsertSupplierResponse : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n" + ex.Source);
            }

        }

        private static void InsertSupplierResponse(string rfqNo, string supplierId, string detailsRefId, string responsePrice, string responseLead, string supplierRemarks, string currency, string DateSent)
        {
            try
            {
                DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
                DbConnection conn = fact.CreateConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_SOFRA"].ConnectionString;

                DbCommand cmd = fact.CreateCommand();
                cmd.CommandType = CommandType.Text;
                int result = 0;

                cmd.CommandText = "INSERT INTO Supplier_Response (RFQNo, DetailsRefId, SupplierId, ResponsePrice, ResponseLead, Remarks, RCurrency, DateSent) " +
                                  "VALUES ('" + rfqNo + "','" + detailsRefId + "','" + supplierId + "','" + responsePrice + "','" + responseLead + "','" + supplierRemarks.Replace("'", "''") + "','" + currency + "','" + DateSent + "')";

                conn.Open();
                cmd.Connection = conn;

                result = cmd.ExecuteNonQuery();

                cmd.Dispose();
                cmd = null;
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Line 485 : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n" + ex.Source);
                //Console.ReadKey();
                //InsertServiceLog("InsertSupplierResponse : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n" + ex.Source);
            }

        }

        private static void InsertSupplierAttachment(string rfqno, string supplierid, string attachment)
        {
            try
            {
                DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
                DbConnection conn = fact.CreateConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_SOFRA"].ConnectionString;

                DbCommand cmd = fact.CreateCommand();
                cmd.CommandType = CommandType.Text;
                int result = 0;

                cmd.CommandText = "INSERT INTO Supplier_Attachment (RFQNo,SupplierId,Attachment) VALUES ('" + rfqno + "','" + supplierid + "','" + attachment + "')";

                conn.Open();
                cmd.Connection = conn;

                result = cmd.ExecuteNonQuery();

                cmd.Dispose();
                cmd = null;
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Line 453 : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n" + ex.Source);
                //Console.ReadKey();
                //InsertServiceLog("InsertSupplierAttachment : " + rfqno + "\n\n" + ex.Message + "\n\n" + ex.StackTrace + "\n\n" + ex.Source);
            }
        }



    }


}
