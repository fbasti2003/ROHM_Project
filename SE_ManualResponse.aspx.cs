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
using SpreadsheetLight;

namespace REPI_PUR_SOFRA
{
    public partial class SE_ManualResponse : System.Web.UI.Page
    {
        Common COMMON = new Common();
        public string supplier;
        public string ficalYear;
        public string fy_supplierid;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["fy_supplierid"].ToString()))
                {
                    //rfqNo = CryptorEngine.Decrypt(Request.QueryString["Fy_SupplierId_From_ManualResponse"].ToString().Replace(" ", "+"), true);
                    supplier = Request.QueryString["supplier_name"].ToString();
                    ficalYear = Request.QueryString["fiscal_year"].ToString();
                    fy_supplierid = Request.QueryString["fy_supplierid"].ToString();

                    btnBasicInformation.Text = fy_supplierid.Trim() + "/SE_" + fy_supplierid.Trim() + "_BasicInformation.xlsx";
                    btnFinancialAnalysis.Text = fy_supplierid.Trim() + "/SE_" + fy_supplierid.Trim() + "_Financial_Analysis.xlsx";
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            supplier = Request.QueryString["supplier_name"].ToString();
            ficalYear = Request.QueryString["fiscal_year"].ToString();
            fy_supplierid = Request.QueryString["fy_supplierid"].ToString();

            string filePath = Server.MapPath("~/SE_Received/" + fy_supplierid.Trim()); 

            if (!System.IO.Directory.Exists(filePath))
            {
                System.IO.Directory.CreateDirectory(filePath); 
            }

            if (System.IO.Directory.Exists(filePath))
            {
                if (fuBasicInformation.HasFile)
                {
                    fuBasicInformation.SaveAs(System.IO.Path.Combine(filePath, System.IO.Path.GetFileName(fuBasicInformation.FileName)));
                    fuBasicInformation.Dispose();
                }

                if (fuFinancialAnalysis.HasFile)
                {
                    fuFinancialAnalysis.SaveAs(System.IO.Path.Combine(filePath, System.IO.Path.GetFileName(fuFinancialAnalysis.FileName)));
                    fuFinancialAnalysis.Dispose();
                }
            }

            try
            {
                //InsertServiceLog("FILE PATH: " + filePath);
                foreach (string file in System.IO.Directory.GetFiles(filePath))
                {
                    if (file.ToLower().Contains("basicinformation"))
                    {
                        //InsertServiceLog("BASIC INFORMATION");

                        string queryBeginPartBI = "BEGIN TRY BEGIN TRANSACTION ";
                        string query1BI = string.Empty;
                        string query2BI = string.Empty;
                        string querySuccessBI = string.Empty;
                        string queryEndPartBI = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";

                        try
                        {
                            using (SLDocument sl = new SLDocument())
                            {
                                //InsertServiceLog("START SLDOCUMENT");

                                FileStream fs = new FileStream(file, FileMode.Open);
                                SLDocument sheet = new SLDocument(fs, "Sheet1");

                                SLWorksheetStatistics stats = sheet.GetWorksheetStatistics();

                                //InsertServiceLog("START SLWorksheetStatistics");

                                string fy_SupplierId = Path.GetFileNameWithoutExtension(file).Remove(0, 3).Replace("_BasicInformation", string.Empty);
                                string fiscalYear = fy_SupplierId.Substring(0, fy_SupplierId.IndexOf("_"));
                                string companyName = sheet.GetCellValueAsString(7, 3);
                                string noteWorthy = sheet.GetCellValueAsString(9, 3);
                                string fyFrom = sheet.GetCellValueAsString(5, 5);
                                string fyTo = sheet.GetCellValueAsString(5, 10);
                                string automotiveRelated = sheet.GetCellValueAsString(5, 18);
                                string classification = sheet.GetCellValueAsString(5, 22);
                                string itemClassification = sheet.GetCellValueAsString(6, 18);
                                string subContractor = sheet.GetCellValueAsString(6, 22);

                                query1BI = "UPDATE SE_TRANSACTION_RequestDetails SET AutomotiveRelated = '" + automotiveRelated + "', Classification = '" + classification + "', ItemClassification = '" + itemClassification + "', SubContractor = '" + subContractor + "', NoteworthyPoints = '" + noteWorthy + "', JudgementYearMonth ='" + (fyFrom + "~" + fyTo) + "' WHERE FY_SupplierId = '" + fy_SupplierId + "' ";
                                query2BI = "INSERT INTO SE_TRANSACTION_SendReceived (FY_SupplierId, SendReceivedDate, TransactionType, SendBy, FiscalYear) VALUES ('" + fy_SupplierId + "',GETDATE(),'RECEIVED','SYSTEM','" + fiscalYear + "') ";

                                querySuccessBI = SE_TRANSACTION_SQLTransaction(queryBeginPartBI + query1BI + query2BI + queryEndPartBI).ToString();

                                if (querySuccessBI == "2")
                                {
                                    InsertServiceLog("SUPPLIER EVALUATION Successfully received from " + companyName + " for fiscal year " + fyFrom + "~" + fyTo + " - (MANUAL_UPLOAD)");

                                    Session["successMessage"] = "RESPONSE HAS BEEN SUCCESSFULLY UPLOADED!";
                                    Session["successTransactionName"] = "SE_ManualResponse";
                                    Session["successReturnPage"] = "SE_SupplierEvaluation.aspx";
                                    Response.Redirect("SuccessPage.aspx");
                                }

                                fs.Close();
                                sheet.CloseWithoutSaving();

                            }
                        }
                        catch (Exception ex)
                        {
                            InsertServiceLog("ERROR 3 : SE Error Receiving - " + ex.Message);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                InsertServiceLog("ERROR 4 : SE Error Receiving - " + ex.Message);
            }

            
        }

        protected void btnBasicInformation_Click(object sender, EventArgs e)
        {
            supplier = Request.QueryString["supplier_name"].ToString();
            ficalYear = Request.QueryString["fiscal_year"].ToString();
            fy_supplierid = Request.QueryString["fy_supplierid"].ToString();

            Response.Redirect("SE_Request/" + fy_supplierid.Trim() + "/SE_" + fy_supplierid.Trim() + "_BasicInformation.xlsx", false);
        }

        protected void btnFinancialAnalysis_Click(object sender, EventArgs e)
        {
            supplier = Request.QueryString["supplier_name"].ToString();
            ficalYear = Request.QueryString["fiscal_year"].ToString();
            fy_supplierid = Request.QueryString["fy_supplierid"].ToString();

            Response.Redirect("SE_Request/" + fy_supplierid.Trim() + "/SE_" + fy_supplierid.Trim() + "_Financial_Analysis.xlsx", false);
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
            }
        }

        private static int SE_TRANSACTION_SQLTransaction(string query)
        {
            int result = 0;

            DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection conn = fact.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_SOFRA"].ConnectionString;

            DbCommand cmd = fact.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;

            try
            {
                conn.Open();
                cmd.Connection = conn;

                result = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                conn.Close();
                conn.Dispose();
                conn = null;
            }

            return result;

        }



    }
}
