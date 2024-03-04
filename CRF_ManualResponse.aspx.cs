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
using System.Data.Common;

namespace REPI_PUR_SOFRA
{
    public partial class CRF_ManualResponse : System.Web.UI.Page
    {

        public string crf_number;
        BLL_CRF BLL = new BLL_CRF();
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["CRFNo_From_ManualResponse"].ToString()))
                {
                    crf_number = CryptorEngine.Decrypt(Request.QueryString["CRFNo_From_ManualResponse"].ToString().Replace(" ", "+"), true);                    
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string validSubject = string.Empty;
                string validCSV = string.Empty;
                crf_number = CryptorEngine.Decrypt(Request.QueryString["CRFNo_From_ManualResponse"].ToString().Replace(" ", "+"), true);
                bool isSuccess = false;

                if (fuManualUpload.HasFile)
                {
                    validSubject = crf_number.Trim();
                    validCSV = fuManualUpload.FileName.Replace(".csv", "");

                    if (validSubject.Trim() == validCSV.Trim())
                    {
                        string filePathCRF = Server.MapPath("~/CRF_CSV_DUMP/" + validSubject);
                        string filePathCRF_PDF = Server.MapPath("~/CRF_Received/" + validSubject);
                        string DateSentCRF = "555";

                        //-----------------------------------------------------------------------------------------
                        if (!System.IO.Directory.Exists(filePathCRF))
                        {
                            System.IO.Directory.CreateDirectory(filePathCRF);
                        }

                        if (!System.IO.File.Exists(Server.MapPath("~/CRF_CSV_DUMP/" + validSubject + "/" + System.IO.Path.GetFileName(fuManualUpload.FileName))))
                        {
                            fuManualUpload.SaveAs(System.IO.Path.Combine(Server.MapPath("~/CRF_CSV_DUMP/" + validSubject), System.IO.Path.GetFileName(fuManualUpload.FileName)));
                            fuManualUpload.Dispose();
                        }
                        else
                        {
                            System.IO.File.Delete(Server.MapPath("~/CRF_CSV_DUMP/" + validSubject + "/" + System.IO.Path.GetFileName(fuManualUpload.FileName)));

                            fuManualUpload.SaveAs(System.IO.Path.Combine(Server.MapPath("~/CRF_CSV_DUMP/" + validSubject), System.IO.Path.GetFileName(fuManualUpload.FileName)));
                            fuManualUpload.Dispose();
                        }
                        //-----------------------------------------------------------------------------------------                        

                        try
                        {
                            foreach (string file in System.IO.Directory.GetFiles(filePathCRF.Replace(".csv", "")))
                            {
                                if (file.ToLower().Contains(".csv"))
                                {
                                    //---------------------------------------------------------------------------------------
                                    string queryBeginPartCRF = "BEGIN TRY BEGIN TRANSACTION ";
                                    string queryEndPartCRF = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                                    string queryCRF = string.Empty;
                                    string query2CRF = string.Empty;
                                    string query3CRF = string.Empty;
                                    int queryCounterCRF = 0;
                                    string querySuccessCRF = string.Empty;

                                    var contentsCRF = File.ReadAllText(file).Split('\n');
                                    var csvCRF = from line in contentsCRF
                                                 select line.Split(',').ToArray();

                                    //int headerRowsCRF = 0;
                                    //foreach (var row in csvCRF.Skip(headerRowsCRF).TakeWhile(r => r.Length > 1 && r.Last().Trim().Length > 0))
                                    //{
                                    //    String confirmedByCRF = row[0];
                                    //    String dateConfirmedCRF = row[1];
                                    //    String notesCRF = row[2];

                                    //    queryCRF += "UPDATE CRF_TRANSACTION_Status SET SupplierResponded = '1', SupplierResponseDate = GETDATE() WHERE CTRLNo ='" + validSubject.Trim() + "' ";
                                    //    query3CRF += "INSERT INTO CRF_TRANSACTION_SupplierResponse (CTRLNo, DateSent, DateReceived, ConfirmedBy, DateConfirmed, Notes) VALUES ('" + validSubject.Trim() + "','" + DateSentCRF.Trim() + "', GETDATE(), '" + confirmedByCRF + "','" + dateConfirmedCRF + "','" + notesCRF + "') ";
                                    //    queryCounterCRF++;
                                    //}

                                    //query2CRF = "INSERT INTO CRF_TRANSACTION_SendReceived (CTRLNo, SendReceivedDate, TransactionType) VALUES ('" + validSubject.Trim() + "', GETDATE(), 'RECEIVED') ";

                                    int headerRowsCRF = 0;
                                    int counter = 0;

                                    foreach (var row in csvCRF.Skip(headerRowsCRF).TakeWhile(r => r.Length > 1 && r.Last().Trim().Length > 0))
                                    {
                                        String confirmedByCRF = string.Empty;
                                        String dateConfirmedCRF = string.Empty;
                                        String notesCRF = string.Empty;
                                        String refid = string.Empty;

                                        if (row[0].ToString().Trim().ToUpper() == "REFID")
                                        {
                                            counter++;
                                        }

                                        // MULTIPLE RECORDS
                                        if (counter > 0)
                                        {
                                            if (row[0].ToString().Trim().ToUpper() != "REFID")
                                            {
                                                confirmedByCRF = row[8];
                                                dateConfirmedCRF = row[9];
                                                notesCRF = row[10];
                                                refid = row[0];

                                                queryCRF += "UPDATE CRF_TRANSACTION_Status SET SupplierResponded = '1', SupplierResponseDate = GETDATE() WHERE CTRLNo ='" + validSubject.Trim() + "' ";
                                                query3CRF += "INSERT INTO CRF_TRANSACTION_SupplierResponse (CTRLNo, DateSent, DateReceived, ConfirmedBy, DateConfirmed, Notes, DetailsRefId) VALUES ('" + validSubject.Trim() + "','" + DateSentCRF.Trim() + "', GETDATE(), '" + confirmedByCRF + "','" + dateConfirmedCRF + "','" + notesCRF + "','" + refid + "') ";
                                                queryCounterCRF++;
                                            }
                                        }
                                        else // SINGLE RECORD (THE OLD CSV)
                                        {
                                            confirmedByCRF = row[0];
                                            dateConfirmedCRF = row[1];
                                            notesCRF = row[2];

                                            queryCRF += "UPDATE CRF_TRANSACTION_Status SET SupplierResponded = '1', SupplierResponseDate = GETDATE() WHERE CTRLNo ='" + validSubject.Trim() + "' ";
                                            query3CRF += "INSERT INTO CRF_TRANSACTION_SupplierResponse (CTRLNo, DateSent, DateReceived, ConfirmedBy, DateConfirmed, Notes, DetailsRefId) VALUES ('" + validSubject.Trim() + "','" + DateSentCRF.Trim() + "', GETDATE(), '" + confirmedByCRF + "','" + dateConfirmedCRF + "','" + notesCRF + "','" + CRF_GetDetailsRefId_From_RequestDetails_ForOldRecord(validSubject.Trim()) + "') ";
                                            queryCounterCRF++;
                                        }

                                    }

                                    query2CRF = "INSERT INTO CRF_TRANSACTION_SendReceived (CTRLNo, SendReceivedDate, TransactionType) VALUES ('" + validSubject.Trim() + "', GETDATE(), 'RECEIVED') ";

                                    if (queryCounterCRF > 0)
                                    {


                                        if (checkIfStatusClosed_CRF(validSubject) == "1")
                                        {
                                            // if transaction (StaClosed=1) is already closed
                                        }
                                        else
                                        {
                                            querySuccessCRF = BLL.CRF_TRANSACTION_SQLTransaction(queryBeginPartCRF + queryCRF + query3CRF + query2CRF + queryEndPartCRF).ToString();

                                            if (querySuccessCRF == ((queryCounterCRF * 2) + 1).ToString())
                                            {
                                                InsertServiceLog("CRF (" + validSubject.ToUpper() + ") Successfully Received (MANUAL RESPONSE)");

                                                isSuccess = true;
                                                //if (fuManualUploadPDF.HasFile)
                                                //{
                                                //    InsertSupplierAttachment_CRF(validSubject, fuManualUploadPDF.FileName, DateSentCRF);
                                                //}

                                                //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Manual response successfully completed!');", true);
                                            }
                                        }

                                    }
                                    //---------------------------------------------------------------------------------------
                                }
                            }
                        }
                        catch (Exception exCRF)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + exCRF.Message + "');", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('INVALID CSV FILE. Please check you csv file by comparing it to the correct CRF Number');", true);
                    }

                }

                if (fuManualUploadPDF.HasFile)
                {
                    validSubject = crf_number.Trim();
                    validCSV = fuManualUploadPDF.FileName.Replace(".pdf", "");
                    string filePathCRF_PDF2 = Server.MapPath("~/CRF_Received/" + validSubject);                    

                    if (!System.IO.Directory.Exists(filePathCRF_PDF2))
                    {
                        System.IO.Directory.CreateDirectory(filePathCRF_PDF2);
                    }

                    if (!System.IO.File.Exists(Server.MapPath("~/CRF_Received/" + validSubject + "/" + System.IO.Path.GetFileName(fuManualUploadPDF.FileName))))
                    {
                        fuManualUploadPDF.SaveAs(System.IO.Path.Combine(Server.MapPath("~/CRF_Received/" + validSubject), System.IO.Path.GetFileName(fuManualUploadPDF.FileName)));
                        fuManualUploadPDF.Dispose();
                    }

                    if (checkIfStatusClosed_CRF(validSubject) == "1")
                    {
                        // if transaction (StaClosed=1) is already closed
                    }
                    else
                    {
                        InsertSupplierAttachment_CRF(validSubject, fuManualUploadPDF.FileName, "555");
                        isSuccess = true;
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Manual response successfully completed!');", true);
                    }
                }

                if (isSuccess)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Manual response successfully completed!');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }



        private static string CRF_GetDetailsRefId_From_RequestDetails_ForOldRecord(string ctrlno)
        {

            string result = string.Empty;

            DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection conn = fact.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_SOFRA"].ConnectionString;

            DbDataReader reader = null;
            DbCommand cmd = fact.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TOP 1 RefId FROM CRF_TRANSACTION_RequestDetails WITH (NOLOCK) WHERE CTRLNo = '" + ctrlno + "'";

            conn.Open();
            cmd.Connection = conn;

            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result = reader["RefId"].ToString();
                }

            }

            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;

            return result;
        }


        private static string checkIfStatusClosed_CRF(string ctrlno)
        {

            string result = string.Empty;

            DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection conn = fact.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_SOFRA"].ConnectionString;

            DbDataReader reader = null;
            DbCommand cmd = fact.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Posted FROM CRF_TRANSACTION_Status WHERE Posted = 1 AND CTRLNo = '" + ctrlno + "'";

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



        private static string checkSupplierResponse_CRF(string ctrlno)
        {

            string result = string.Empty;

            DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection conn = fact.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_SOFRA"].ConnectionString;

            DbDataReader reader = null;
            DbCommand cmd = fact.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TOP 1 DateSent FROM CRF_TRANSACTION_SupplierResponse WITH (NOLOCK) WHERE CTRLNo = '" + ctrlno + "' ORDER BY DateSent DESC";
            //cmd.Parameters.Add(Factory.CreateParameter("@category", category = string.IsNullOrEmpty(category) ? string.Empty : category));

            conn.Open();
            cmd.Connection = conn;

            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result = reader["DateSent"].ToString();
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

        private static void InsertSupplierAttachment_CRF(string ctrlno, string attachmentFile, string dateSent)
        {
            try
            {
                DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
                DbConnection conn = fact.CreateConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_SOFRA"].ConnectionString;

                DbCommand cmd = fact.CreateCommand();
                cmd.CommandType = CommandType.Text;
                int result = 0;

                cmd.CommandText = "INSERT INTO CRF_TRANSACTION_SupplierAttachment (CTRLNo, AttachmentFile, DateSent) VALUES ('" + ctrlno + "','" + attachmentFile + "','" + dateSent + "') ";

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
