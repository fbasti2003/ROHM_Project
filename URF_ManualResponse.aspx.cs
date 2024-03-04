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
    public partial class URF_ManualResponse : System.Web.UI.Page
    {
        BLL_URF BLL = new BLL_URF();
        public string urf_number;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["URFNo_From_ManualResponse"].ToString()))
                {
                    urf_number = CryptorEngine.Decrypt(Request.QueryString["URFNo_From_ManualResponse"].ToString().Replace(" ", "+"), true);
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string validSubject = string.Empty;
                string validCSV = string.Empty;
                urf_number = CryptorEngine.Decrypt(Request.QueryString["URFNo_From_ManualResponse"].ToString().Replace(" ", "+"), true);

                if (fuManualUpload.HasFile)
                {
                    validSubject = urf_number.Trim();
                    validCSV = fuManualUpload.FileName.Replace(".csv", "");

                    if (validSubject.Trim() == validCSV.Trim())
                    {
                        string filePathURF = Server.MapPath("~/URF_CSV_DUMP/" + validSubject);
                        string filePathURF_PDF = Server.MapPath("~/URF_Received/" + validSubject);
                        string DateSentURF = "555";

                        //-----------------------------------------------------------------------------------------
                        if (!System.IO.Directory.Exists(filePathURF))
                        {
                            System.IO.Directory.CreateDirectory(filePathURF);
                        }

                        if (!System.IO.File.Exists(Server.MapPath("~/URF_CSV_DUMP/" + validSubject + "/" + System.IO.Path.GetFileName(fuManualUpload.FileName))))
                        {
                            fuManualUpload.SaveAs(System.IO.Path.Combine(Server.MapPath("~/URF_CSV_DUMP/" + validSubject), System.IO.Path.GetFileName(fuManualUpload.FileName)));
                            fuManualUpload.Dispose();
                        }
                        //-----------------------------------------------------------------------------------------

                        if (fuManualUploadPDF.HasFile)
                        {
                            if (!System.IO.Directory.Exists(filePathURF_PDF))
                            {
                                System.IO.Directory.CreateDirectory(filePathURF_PDF);
                            }

                            if (!System.IO.File.Exists(Server.MapPath("~/URF_Received/" + validSubject + "/" + System.IO.Path.GetFileName(fuManualUploadPDF.FileName))))
                            {
                                fuManualUploadPDF.SaveAs(System.IO.Path.Combine(Server.MapPath("~/URF_Received/" + validSubject), System.IO.Path.GetFileName(fuManualUploadPDF.FileName)));
                                fuManualUploadPDF.Dispose();
                            }
                        }

                        try
                        {
                            foreach (string file in System.IO.Directory.GetFiles(filePathURF.Replace(".csv", "")))
                            {
                                if (file.ToLower().Contains(".csv"))
                                {
                                    //---------------------------------------------------------------------------------------
                                    string queryBeginPartURF = "BEGIN TRY BEGIN TRANSACTION ";
                                    string queryEndPartURF = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                                    string queryURF = string.Empty;
                                    string query2URF = string.Empty;
                                    string query3URF = string.Empty;
                                    int queryCounterURF = 0;
                                    string querySuccessURF = string.Empty;

                                    //var contentsURF = File.ReadAllText(filePathURF.Replace(".csv", "") + "/" + attachment.FileName).Split('\n');
                                    var contentsURF = File.ReadAllText(file).Split('\n');
                                    var csvURF = from line in contentsURF
                                                 select line.Split(',').ToArray();


                                    int headerRowsURF = 1;
                                    foreach (var row in csvURF.Skip(headerRowsURF).TakeWhile(r => r.Length > 1 && r.Last().Trim().Length > 0))
                                    {
                                        String refIdURF = row[0];
                                        var replyDeliveryDateURF = row[9];

                                        queryURF += "UPDATE URF_TRANSACTION_RequestDetails SET ReplyDeliveryDate = '" + replyDeliveryDateURF + "' WHERE RefId ='" + refIdURF + "' ";
                                        query3URF += "INSERT INTO URF_TRANSACTION_SupplierResponse (CTRLNo, DetailsRefId, DateSent, DateReceived, ReplyDeliveryDate) VALUES ('" + validSubject.Trim() + "','" + refIdURF.Trim() + "','" + DateSentURF.Trim() + "', GETDATE(), '" + replyDeliveryDateURF + "') ";
                                        queryCounterURF++;
                                    }

                                    query2URF = "INSERT INTO URF_TRANSACTION_SendReceived (CTRLNo, SendReceivedDate, TransactionType) VALUES ('" + validSubject.Trim() + "', GETDATE(), 'RECEIVED') ";


                                    if (queryCounterURF > 0)
                                    {
                                        if (checkIfStatusClosed(validSubject.Trim()) == "1")
                                        {
                                            // if transaction (StaClosed=1) is already closed
                                        }
                                        else
                                        {
                                            querySuccessURF = BLL.URF_TRANSACTION_SQLTransaction(queryBeginPartURF + queryURF + query3URF + query2URF + queryEndPartURF).ToString();

                                            if (querySuccessURF == ((queryCounterURF * 2) + 1).ToString())
                                            {
                                                InsertServiceLog("URF (" + validSubject.ToUpper() + ") Successfully Received (MANUAL RESPONSE)");

                                                if (fuManualUploadPDF.HasFile)
                                                {
                                                    InsertSupplierAttachment(validSubject, fuManualUploadPDF.FileName, DateSentURF);
                                                }

                                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Manual response successfully completed!');", true);
                                            }

                                        }
                                    }
                                    //---------------------------------------------------------------------------------------
                                }
                            }
                        }
                        catch (Exception exURF)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + exURF.Message + "');", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('INVALID CSV FILE. Please check you csv file by comparing it to the correct URF Number');", true);
                    }

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
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

        private static string checkIfStatusClosed(string ctrlno)
        {

            string result = string.Empty;

            DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection conn = fact.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_SOFRA"].ConnectionString;

            DbDataReader reader = null;
            DbCommand cmd = fact.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT STATClosed FROM URF_TRANSACTION_RequestStatus WHERE (STATClosed IS NULL OR STATClosed = 1) AND CTRLNo = '" + ctrlno + "'";

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

        private static void InsertSupplierAttachment(string ctrlno, string attachmentFile, string dateSent)
        {
            try
            {
                DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
                DbConnection conn = fact.CreateConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_SOFRA"].ConnectionString;

                DbCommand cmd = fact.CreateCommand();
                cmd.CommandType = CommandType.Text;
                int result = 0;

                cmd.CommandText = "INSERT INTO URF_TRANSACTION_SupplierAttachment (CTRLNo, AttachmentFile, DateSent) VALUES ('" + ctrlno + "','" + attachmentFile + "','" + dateSent + "') ";

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
