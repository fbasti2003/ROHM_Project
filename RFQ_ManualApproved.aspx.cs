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
    public partial class RFQ_ManualApproved : System.Web.UI.Page
    {
        public string rfqNo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["RFQNo_From_ManualApproved"].ToString()))
                {
                    rfqNo = CryptorEngine.Decrypt(Request.QueryString["RFQNo_From_ManualApproved"].ToString().Replace(" ", "+"), true);
                }
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                rfqNo = CryptorEngine.Decrypt(Request.QueryString["RFQNo_From_ManualApproved"].ToString().Replace(" ", "+"), true);
                string manualUpload = string.Empty;

                string fileNameApplication = System.IO.Path.GetFileName(fuManualApproved.FileName);
                string fileExtensionApplication = System.IO.Path.GetExtension(fileNameApplication);
                string newFile = fileNameApplication;


                if (fuManualApproved.HasFile)
                {
                    if (!System.IO.Directory.Exists(Server.MapPath("~/IO_Request/" + rfqNo)))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/IO_Request/" + rfqNo));
                    }
                    if (!System.IO.File.Exists(Server.MapPath("~/IO_Request/" + rfqNo + "/" + newFile)))
                    {
                        fuManualApproved.SaveAs(System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + rfqNo), newFile));
                        fuManualApproved.Dispose();
                        System.IO.File.Copy(System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + rfqNo), newFile), System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + rfqNo), ("ManualApproved-" + rfqNo + fileExtensionApplication)), true);
                        System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + rfqNo), newFile));
                    }

                    InsertManualApprovedAttachment(rfqNo, "ManualApproved-" + rfqNo + fileExtensionApplication);

                    //BUYER
                    InsertHistoryOfApproval(rfqNo, ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingBuyer"].ToString(), Session["LcRefId"].ToString());
                    //INCHARGE AUTO-APPROVED
                    InsertHistoryOfApproval(rfqNo, ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingIncharge"].ToString(), ConfigurationManager.AppSettings["AUTO-APPROVED-ACCOUNT"].ToString());
                    //DEPTMAN AUTO-APPROVED
                    InsertHistoryOfApproval(rfqNo, ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingDeptManager"].ToString(), ConfigurationManager.AppSettings["AUTO-APPROVED-ACCOUNT"].ToString());
                    //DIVMAN AUTO-APPROVED
                    InsertHistoryOfApproval(rfqNo, ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingDivManager"].ToString(), ConfigurationManager.AppSettings["AUTO-APPROVED-ACCOUNT"].ToString());

                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('MANUAL APPROVED FOR " + rfqNo + " HAS BEEN SUCCESSFULLY COMPLETED');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }



        private static void InsertManualApprovedAttachment(string rfqNo, string attachment)
        {
            try
            {
                DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
                DbConnection conn = fact.CreateConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_SOFRA"].ConnectionString;

                DbCommand cmd = fact.CreateCommand();
                cmd.CommandType = CommandType.Text;
                int result = 0;

                cmd.CommandText = "UPDATE Request_Status SET Buyer = 1, PurchasingInCharge = 1, DepartmentManager = 1, DivisionManager = 1, ManualApprovedAtt = '" + attachment + "' WHERE RFQNo = '" + rfqNo + "'";                                  

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



        private static void InsertHistoryOfApproval(string rfqNo, string transactionName, string approvedBy)
        {
            try
            {
                DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
                DbConnection conn = fact.CreateConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_SOFRA"].ConnectionString;

                DbCommand cmd = fact.CreateCommand();
                cmd.CommandType = CommandType.Text;
                int result = 0;

                cmd.CommandText = "INSERT INTO Request_HistoryOfApproval (RFQNo,TransactionName,ApprovedBy,ApprovedDate,IsActive) VALUES ('" + rfqNo.Trim() +
                                  "','" + transactionName + "','" + approvedBy + "',GETDATE(),1) ";
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







    }
}
