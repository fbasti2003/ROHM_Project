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
using System.Data.Common;
using System.IO;

namespace REPI_PUR_SOFRA
{
    public partial class DRF_ManualResponse : System.Web.UI.Page
    {
        public string drf_number;
        BLL_DRF BLL = new BLL_DRF();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["DRFNo_From_ManualResponse"].ToString()))
                {
                    drf_number = CryptorEngine.Decrypt(Request.QueryString["DRFNo_From_ManualResponse"].ToString().Replace(" ", "+"), true);
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string validSubject = string.Empty;
                string validCSV = string.Empty;
                drf_number = CryptorEngine.Decrypt(Request.QueryString["DRFNo_From_ManualResponse"].ToString().Replace(" ", "+"), true);


                if (fuManualUpload.HasFile)
                {
                    validSubject = drf_number.Trim();
                    validCSV = fuManualUpload.FileName.Replace(".csv", "");

                    if (validSubject.Trim() == validCSV.Trim())
                    {
                        string filePathDRF = Server.MapPath("~/DRF_CSV_DUMP/" + validSubject);
                        string filePathDRF_PDF = Server.MapPath("~/DRF_Received/" + validSubject);
                        string filePathDRF_XLSX = Server.MapPath("~/DRF_Received/" + validSubject);
                        string DateSentDRF = "555";

                        //-----------------------------------------------------------------------------------------
                        if (!System.IO.Directory.Exists(filePathDRF))
                        {
                            System.IO.Directory.CreateDirectory(filePathDRF);
                        }

                        if (!System.IO.File.Exists(Server.MapPath("~/DRF_CSV_DUMP/" + validSubject + "/" + System.IO.Path.GetFileName(fuManualUpload.FileName))))
                        {
                            fuManualUpload.SaveAs(System.IO.Path.Combine(Server.MapPath("~/DRF_CSV_DUMP/" + validSubject), System.IO.Path.GetFileName(fuManualUpload.FileName)));
                            fuManualUpload.Dispose();
                        }
                        //-----------------------------------------------------------------------------------------

                        if (fuManualUploadPDF.HasFile)
                        {
                            if (!System.IO.Directory.Exists(filePathDRF_PDF))
                            {
                                System.IO.Directory.CreateDirectory(filePathDRF_PDF);
                            }

                            if (!System.IO.File.Exists(Server.MapPath("~/DRF_Received/" + validSubject + "/" + System.IO.Path.GetFileName(fuManualUploadPDF.FileName))))
                            {
                                fuManualUploadPDF.SaveAs(System.IO.Path.Combine(Server.MapPath("~/DRF_Received/" + validSubject), System.IO.Path.GetFileName(fuManualUploadPDF.FileName)));
                                fuManualUploadPDF.Dispose();
                            }
                        }

                        if (fuManualUploadExcel.HasFile)
                        {
                            if (!System.IO.Directory.Exists(filePathDRF_XLSX))
                            {
                                System.IO.Directory.CreateDirectory(filePathDRF_XLSX);
                            }

                            if (!System.IO.File.Exists(Server.MapPath("~/DRF_Received/" + validSubject + "/" + System.IO.Path.GetFileName(fuManualUploadExcel.FileName))))
                            {
                                fuManualUploadExcel.SaveAs(System.IO.Path.Combine(Server.MapPath("~/DRF_Received/" + validSubject), System.IO.Path.GetFileName(fuManualUploadExcel.FileName)));
                                fuManualUploadExcel.Dispose();
                            }
                        }

                        try
                        {
                            foreach (string file in System.IO.Directory.GetFiles(filePathDRF.Replace(".csv", "")))
                            {
                                if (file.ToLower().Contains(".csv"))
                                {
                                    //---------------------------------------------------------------------------------------

                                    string queryBeginPartDRF = "BEGIN TRY BEGIN TRANSACTION ";
                                    string queryEndPartDRF = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                                    string queryDRF = string.Empty;
                                    string query2DRF = string.Empty;
                                    string query3DRF = string.Empty;
                                    int queryCounterDRF = 0;
                                    string querySuccessDRF = string.Empty;

                                    var contentsDRF = File.ReadAllText(file).Split('\n');
                                    var csvDRF = from line in contentsDRF
                                                 select line.Split(',').ToArray();

                                    int headerRowsDRF = 0;
                                    foreach (var row in csvDRF.Skip(headerRowsDRF).TakeWhile(r => r.Length > 1 && r.Last().Trim().Length > 0))
                                    {
                                        String receivedTypeDRF = row[0];
                                        String answerTypeDRF = row[1];

                                        queryDRF += "UPDATE DRF_TRANSACTION_Status SET SupplierResponded = '1', SupplierResponseDate = GETDATE() WHERE CTRLNo ='" + validSubject.Trim() + "' ";
                                        query3DRF += "INSERT INTO DRF_TRANSACTION_SupplierResponse (CTRLNo, DateSent, DateReceived, ReceivedType, ReceivedAnswer) VALUES ('" + validSubject.Trim() + "','" + DateSentDRF.Trim() + "', GETDATE(), '" + receivedTypeDRF + "','" + answerTypeDRF + "') ";
                                        queryCounterDRF++;
                                    }

                                    query2DRF = "INSERT INTO DRF_TRANSACTION_SendReceived (CTRLNo, SendReceivedDate, TransactionType) VALUES ('" + validSubject.Trim() + "', GETDATE(), 'RECEIVED') ";

                                    if (queryCounterDRF > 0)
                                    {
                                        if (checkIfStatusClosed_DRF(validSubject.Trim()) == "1")
                                        {
                                            // if transaction (StaClosed=1) is already closed
                                        }
                                        else
                                        {
                                            querySuccessDRF = BLL.DRF_TRANSACTION_SQLTransaction(queryBeginPartDRF + queryDRF + query3DRF + query2DRF + queryEndPartDRF).ToString();

                                            if (querySuccessDRF == ((queryCounterDRF * 2) + 1).ToString())
                                            {
                                                InsertServiceLog("DRF (" + validSubject.ToUpper() + ") Successfully Received (MANUAL RESPONSE)");

                                                if (fuManualUploadPDF.HasFile)
                                                {
                                                    InsertSupplierAttachment_DRF(validSubject, fuManualUploadPDF.FileName, DateSentDRF);
                                                }

                                                if (fuManualUploadExcel.HasFile)
                                                {
                                                    InsertSupplierAttachment_DRF(validSubject, fuManualUploadExcel.FileName, DateSentDRF);
                                                }

                                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Manual response successfully completed!');", true);
                                            }
                                        }
                                    }

                                    //---------------------------------------------------------------------------------------
                                }
                            }
                        }
                        catch (Exception exDRF)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + exDRF.Message + "');", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('INVALID CSV FILE. Please check you csv file by comparing it to the correct DRF Number');", true);
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


        private static string checkIfStatusClosed_DRF(string ctrlno)
        {

            string result = string.Empty;

            DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection conn = fact.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_SOFRA"].ConnectionString;

            DbDataReader reader = null;
            DbCommand cmd = fact.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Posted FROM DRF_TRANSACTION_Status WHERE Posted = 1 AND CTRLNo = '" + ctrlno + "'";

            conn.Open();
            cmd.Connection = conn;

            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result = reader["STATClosed"].ToString();
                }

            }

            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;

            return result;
        }

        private static void InsertServiceLog(string log)
        {
            try
            {
                DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
                DbConnection conn = fact.CreateConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_SOFRA"].ConnectionString;

                DbCommand cmd = fact.CreateCommand();
                cmd.CommandType = CommandType.Text;
                int result = 0;

                cmd.CommandText = "INSERT INTO Service_Logs (TransactionLog,TransactionDate) VALUES ('" + log.Replace("'", "''") + "',getdate())";

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
                //InsertServiceLog("InsertSupplierAttachment : " + ex.Message + "\n\n" + ex.StackTrace + "\n\n" + ex.Source);
            }
        }

        private static void InsertSupplierAttachment_DRF(string ctrlno, string attachmentFile, string dateSent)
        {
            try
            {
                DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
                DbConnection conn = fact.CreateConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_SOFRA"].ConnectionString;

                DbCommand cmd = fact.CreateCommand();
                cmd.CommandType = CommandType.Text;
                int result = 0;

                cmd.CommandText = "INSERT INTO DRF_TRANSACTION_SupplierAttachment (CTRLNo, AttachmentFile, DateSent) VALUES ('" + ctrlno + "','" + attachmentFile + "','" + dateSent + "') ";

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
                InsertServiceLog("URF FAILED : Failed to received attachment from " + ctrlno.ToUpper());
            }
        }


    }
}
