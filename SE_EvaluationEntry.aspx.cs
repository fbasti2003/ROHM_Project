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
using SpreadsheetLight;
using System.IO;
using System.Data.Common;

namespace REPI_PUR_SOFRA
{
    public partial class SE_EvaluationEntry : System.Web.UI.Page
    {
        BLL_RFQ BLL_RFQ = new BLL_RFQ();
        BLL_SE BLL = new BLL_SE();
        Common COMMON = new Common();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LcRefId"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["fy_year"].ToString()) && Request.QueryString["send_evaluation"].ToString() == "false")
                    {
                        LoadDefaultForResend();
                    }
                    else if (!String.IsNullOrEmpty(Request.QueryString["fy_year"].ToString()) && Request.QueryString["send_evaluation"].ToString() == "true")
                    {
                        LoadDefaultForSendEvaluation();
                    }
                    else
                    {
                        LoadDefault();
                    }
                }
            }
        }


        private void LoadDefaultForSendEvaluation()
        {
            try
            {
                List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();
                list = BLL.SE_MT_FiscalYear_GetAll();

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        ddFiscalYear.Items.Clear();
                        ddFiscalYear.Items.Add("");

                        foreach (Entities_SE_FiscalYear entity in list)
                        {
                            ListItem item = new ListItem();
                            item.Text = entity.Description;
                            item.Value = entity.RefId;

                            if (entity.Isdisabled == string.Empty || entity.Isdisabled == "0")
                            {
                                ddFiscalYear.Items.Add(item);
                            }

                        }

                        ddFiscalYear.Items.FindByValue(Request.QueryString["fy_year"].ToString()).Selected = true;
                    }
                }

                // SETTING UP SUPPLIERS
                List<Entities_SE_FiscalYear> list_Suppliers = new List<Entities_SE_FiscalYear>();
                list_Suppliers = BLL.SE_TRANSACTION_Monitoring_GetAll_ByFiscalYear(Request.QueryString["fy_year"].ToString()).Where(itm => itm.StatDivManager == "1").ToList();

                if (list_Suppliers != null)
                {
                    if (list_Suppliers.Count > 0)
                    {
                        gvData.DataSource = list_Suppliers;
                        gvData.DataBind();
                    }
                }



            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


        private void LoadDefaultForResend()
        {
            try
            {
                List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();
                list = BLL.SE_MT_FiscalYear_GetAll();

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        ddFiscalYear.Items.Clear();
                        ddFiscalYear.Items.Add("");

                        foreach (Entities_SE_FiscalYear entity in list)
                        {
                            ListItem item = new ListItem();
                            item.Text = entity.Description;
                            item.Value = entity.RefId;

                            if (entity.Isdisabled == string.Empty || entity.Isdisabled == "0")
                            {
                                ddFiscalYear.Items.Add(item);
                            }

                        }

                        ddFiscalYear.Items.FindByValue(Request.QueryString["fy_year"].ToString()).Selected = true;
                        ddFiscalYear.Enabled = false;
                    }
                }

                // SETTING UP SUPPLIERS
                List<Entities_SE_FiscalYear> list_Suppliers = new List<Entities_SE_FiscalYear>();
                list_Suppliers = BLL.SE_TRANSACTION_Monitoring_GetAll_ByFiscalYear(Request.QueryString["fy_year"].ToString()).Where(itm => itm.StatDivManager == "0" || string.IsNullOrEmpty(itm.StatDivManager)).ToList();

                if (list_Suppliers != null)
                {
                    if (list_Suppliers.Count > 0)
                    {
                        gvData.DataSource = list_Suppliers;
                        gvData.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        private void LoadDefault()
        {
            try
            {
                List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();
                list = BLL.SE_MT_FiscalYear_GetAll();

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        //ddFiscalYear.Items.Clear();
                        //ddFiscalYear.Items.Add("");

                        foreach (Entities_SE_FiscalYear entity in list)
                        {
                            ListItem item = new ListItem();
                            item.Text = entity.Description;
                            item.Value = entity.RefId;

                            if (entity.Isdisabled == string.Empty || entity.Isdisabled == "0")
                            {
                                ddFiscalYear.Items.Add(item);
                            }

                        }
                    }
                }

                // SETTING UP SUPPLIERS
                List<Entities_RFQ_Supplier> list_Suppliers_Final = new List<Entities_RFQ_Supplier>();
                List<Entities_RFQ_Supplier> list_Suppliers = new List<Entities_RFQ_Supplier>();
                list_Suppliers = BLL_RFQ.RFQ_MT_Supplier_GetAll().Where(itm => itm.EvaluationEmail.Length > 0).ToList();

                if (list_Suppliers != null)
                {
                    if (list_Suppliers.Count > 0)
                    {
                        foreach (Entities_RFQ_Supplier entity in list_Suppliers)
                        {
                            Entities_RFQ_Supplier eSupplier = new Entities_RFQ_Supplier();
                            eSupplier.RefId = entity.RefId;
                            eSupplier.Name = entity.Name;
                            eSupplier.EmailAddress = entity.EmailAddress;
                            eSupplier.EvaluationEmail = entity.EvaluationEmail;
                            eSupplier.Address = entity.Address;
                            eSupplier.AddedBy = entity.AddedBy;
                            eSupplier.IsDisabled = entity.IsDisabled;
                            eSupplier.Registered = entity.Registered;
                            eSupplier.Receipient = entity.Receipient;
                            eSupplier.Responded = entity.Responded;
                            eSupplier.StatDivManager = entity.StatDivManager;
                            eSupplier.Fy_SupplierId = entity.Fy_SupplierId;

                            if (string.IsNullOrEmpty(BLL.SE_TRANSACTION_GetAlreadySend_By_Fy_SupplierId(ddFiscalYear.SelectedValue + "_" + entity.RefId)))
                            {
                                list_Suppliers_Final.Add(eSupplier);
                            }

                        }

                        gvData.DataSource = list_Suppliers_Final;
                        gvData.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                    Label lblReceipient = (Label)e.Row.FindControl("lblReceipient");
                    Label lblResponded = (Label)e.Row.FindControl("lblResponded");
                    ImageButton ibApproved = (ImageButton)e.Row.FindControl("ibApproved");
                    ImageButton ibReport = (ImageButton)e.Row.FindControl("ibReport");
                    ImageButton ibReport2 = (ImageButton)e.Row.FindControl("ibReport2");
                    Label lblStatDivManager = (Label)e.Row.FindControl("lblStatDivManager");

                    if (Request.QueryString["send_evaluation"].ToString() == "true")
                    {
                        if (!string.IsNullOrEmpty(lblStatDivManager.Text))
                        {
                            if (lblStatDivManager.Text == "1")
                            {
                                lblStatus.Text = "EVALUATION FOR SENDING";
                            }

                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(Request.QueryString["fy_year"].ToString()))
                        {
                            if (string.IsNullOrEmpty(lblReceipient.Text) || lblReceipient.Text == "0")
                            {
                                lblStatus.Text = "FOR SENDING";
                                ibReport.Style.Add("display", "none");
                                ibReport2.Style.Add("display", "none");
                            }
                            else if (int.Parse(lblResponded.Text.Trim()) > 0)
                            {
                                lblStatus.Text = "SUPPLIER RESPONDED";
                                ibReport.Style.Add("display", "block");
                                ibReport2.Style.Add("display", "block");
                            }
                            else
                            {
                                lblStatus.Text = "FOR RESEND / WAITING FOR RESPONSE";
                                ibReport.Style.Add("display", "none");
                                ibReport2.Style.Add("display", "none");
                            }
                        }
                        else
                        {
                            if (lblStatus.Text.ToUpper() == "FOR SENDING")
                            {
                                //ibApproved.ImageUrl = "~/images/A2.png";
                                ibReport.Style.Add("display", "none");
                                ibReport2.Style.Add("display", "none");
                            }
                            else
                            {
                                ibApproved.ImageUrl = "~/images/DA2.png";
                            }
                        }
                    }

                    if (lblStatDivManager.Text == "1" && Request.QueryString["send_evaluation"].ToString() == "false")
                    {
                        btnSubmit.Visible = false;
                    }
                    else
                    {
                        btnSubmit.Visible = true;
                    }
                    
                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                ImageButton ibApproved = row.FindControl("ibApproved") as ImageButton;
                ImageButton ibReport = row.FindControl("ibReport") as ImageButton;
                Label lblStatus = row.FindControl("lblStatus") as Label;
                Label lblFySupplierId = row.FindControl("lblFySupplierId") as Label;


                if (e.CommandName == "A_Command")
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["fy_year"].ToString()))
                    {
                        if (ibApproved.ImageUrl == "~/images/A2.png")
                        {
                            ibApproved.ImageUrl = "~/images/A1.png";
                        }
                        else
                        {
                            ibApproved.ImageUrl = "~/images/A2.png";
                        }
                    }
                    else
                    {
                        if (ibApproved.ImageUrl == "~/images/A2.png")
                        {
                            ibApproved.ImageUrl = "~/images/DA2.png";
                            lblStatus.Text = "NOT FOR SENDING";
                        }
                        else
                        {
                            ibApproved.ImageUrl = "~/images/A2.png";
                            lblStatus.Text = "FOR SENDING";
                        }
                    }

                }

                if (e.CommandName == "ibReport_Command")
                {
                    Response.Redirect("SE_Received/" + lblFySupplierId.Text.Trim() + "/SE_" + lblFySupplierId.Text.Trim() + "_Financial_Analysis.xlsx", false);  
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('3 - " + ex.StackTrace.ToString() + "');", true);
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvData.Rows.Count; i++)
                {
                    ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[0].FindControl("ibApproved");
                    ibApproved.ImageUrl = "~/images/A2.png";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnUncheck_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvData.Rows.Count; i++)
                {
                    ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[0].FindControl("ibApproved");
                    ibApproved.ImageUrl = "~/images/A1.png";
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
                if (string.IsNullOrEmpty(ddFiscalYear.SelectedItem.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select a valid Fiscal Year.');", true);
                }
                //else if (BLL.SE_TRANSACTION_IsRequestExist_By_FiscalYear(ddFiscalYear.SelectedValue).Count > 1 && String.IsNullOrEmpty(Request.QueryString["fy_year"].ToString()))
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Fiscal Year " + ddFiscalYear.SelectedItem.Text + " is already created.');", true);
                //}
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowRegistered", "showDialog();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        private void sendEvaluationToSuppliers()
        {
            if (gvData.Rows.Count > 0)
            {
                int rd_Query_Counter = 0;
                string fy_from = string.Empty;
                string fy_to = string.Empty;

                //-----------------------------------------------------------------------------------------
                List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();
                list = BLL.SE_MT_FiscalYear_GetAll();

                if (list != null)
                {
                    if (list.Count > 0)
                    {

                        foreach (Entities_SE_FiscalYear entity in list)
                        {

                            if (entity.RefId == ddFiscalYear.SelectedValue)
                            {
                                fy_from = entity.From;
                                fy_to = entity.To;
                            }

                        }
                    }
                }
                //-----------------------------------------------------------------------------------------

                for (int i = 0; i < gvData.Rows.Count; i++)
                {
                    ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[0].FindControl("ibApproved");
                    Label lblId = (Label)gvData.Rows[i].Cells[3].FindControl("lblId");
                    Label lblEmail = (Label)gvData.Rows[i].Cells[2].FindControl("lblEmail");
                    Label lblName = (Label)gvData.Rows[i].Cells[1].FindControl("lblName");
                    Label lblReceipient = (Label)gvData.Rows[i].Cells[1].FindControl("lblReceipient");

                    if (ibApproved.ImageUrl == "~/images/A2.png")
                    {
                        if (!String.IsNullOrEmpty(Request.QueryString["fy_year"].ToString()) && Request.QueryString["send_evaluation"].ToString() == "true")
                        {
                            if (!System.IO.File.Exists(Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_SupplierEvaluation.xlsx")))
                            {
                                System.IO.File.Copy(Server.MapPath("~/SE_XLS/SE_Supplier_Evaluation.xlsx"), Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_SupplierEvaluation.xlsx"));
                            }


                            List<Entities_SE_RequestEntry> list_SupplierEvaluation = new List<Entities_SE_RequestEntry>();
                            list_SupplierEvaluation = BLL.SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear3(ddFiscalYear.SelectedValue);

                            if (list_SupplierEvaluation.Count > 0)
                            {
                                foreach (Entities_SE_RequestEntry entity in list_SupplierEvaluation)
                                {
                                    string supplierName = entity.SupplierName.ToUpper();
                                    string automotiveRelated = entity.AutomotiveRelated;
                                    string classification = entity.Classification;
                                    string itemClassification = entity.ItemClassification;
                                    string subContractor = entity.SubContractor;
                                    string noteWorthyPoints = entity.NoteworthyPoints;
                                    string makerName = entity.MakerName;

                                    string ISO9001 = entity.ISO9001;
                                    string ISO14001 = entity.ISO14001;
                                    string IATF16949 = entity.IATF16949;
                                    string EI_FinancialAnalysis = entity.EI_FinancialAnalysis;
                                    string EI_Quality = entity.EI_Quality;
                                    string EI_CostResponse = entity.EI_CostResponse;
                                    string EI_Delivery = entity.EI_Delivery;
                                    string EI_Cooperation = entity.EI_Cooperation;
                                    string EI_CSR = entity.EI_CSR;
                                    string EI_TotalScore = entity.EI_TotalScore;
                                    string JudgementByPerson = entity.JudgementByPerson;
                                    string JudgementYearMonth = entity.JudgementYearMonth;
                                    string Reason = entity.Reason;
                                    string CircularComments = entity.CircularComments;
                                    string DivManInstructions = entity.DivManInstructions;
                                    string incharge = entity.Incharge;
                                    string sectionIncharge = entity.SectionIncharge;
                                    string deptManager = entity.DeptManager;
                                    string divManager = entity.DivManager;



                                    if (entity.FY_SupplierId.Trim() == (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()))
                                    {

                                        // SUPPLIER EVALUATION
                                        string pathSE_Resend = Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_SupplierEvaluation.xlsx");
                                        Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathSE_Resend);
                                        FileStream fsSE_Resend = new FileStream(pathSE_Resend, FileMode.Open);
                                        using (SLDocument supplierEvaluation_Resend = new SLDocument(fsSE_Resend, "Supplier Revaluation"))
                                        {
                                            supplierEvaluation_Resend.SetCellValue("C8", lblName.Text.ToUpper());
                                            supplierEvaluation_Resend.SetCellValue("E6", fy_from);
                                            supplierEvaluation_Resend.SetCellValue("J6", fy_to);

                                            supplierEvaluation_Resend.SetCellValue("R6", automotiveRelated.ToUpper());
                                            supplierEvaluation_Resend.SetCellValue("R7", itemClassification.ToUpper());
                                            supplierEvaluation_Resend.SetCellValue("V6", classification.ToUpper());
                                            supplierEvaluation_Resend.SetCellValue("V7", subContractor.ToUpper());

                                            supplierEvaluation_Resend.SetCellValue("G13", EI_FinancialAnalysis);
                                            supplierEvaluation_Resend.SetCellValue("G15", EI_Quality);
                                            supplierEvaluation_Resend.SetCellValue("G17", EI_CostResponse);

                                            supplierEvaluation_Resend.SetCellValue("S13", EI_Delivery);
                                            supplierEvaluation_Resend.SetCellValue("S15", EI_Cooperation);
                                            supplierEvaluation_Resend.SetCellValue("S17", EI_CSR);

                                            supplierEvaluation_Resend.SetCellValue("S21", EI_TotalScore + " / 100");
                                            supplierEvaluation_Resend.SetCellValue("T21", JudgementByPerson.ToUpper());

                                            supplierEvaluation_Resend.SetCellValue("C27", Reason.ToUpper());
                                            supplierEvaluation_Resend.SetCellValue("C32", CircularComments.ToUpper());
                                            supplierEvaluation_Resend.SetCellValue("C37", DivManInstructions.ToUpper());

                                            supplierEvaluation_Resend.SetCellValue("H43", incharge.ToUpper());
                                            supplierEvaluation_Resend.SetCellValue("J43", sectionIncharge.ToUpper());
                                            supplierEvaluation_Resend.SetCellValue("M43", deptManager.ToUpper());
                                            supplierEvaluation_Resend.SetCellValue("R43", divManager.ToUpper());

                                            fsSE_Resend.Close();
                                            supplierEvaluation_Resend.SaveAs(pathSE_Resend);
                                        }

                                        //=========== Sending Email =================
                                        string supplier_evaluation_xlsx = Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_SupplierEvaluation.xlsx");
                                        string verbiage = "Hi <b>" + lblName.Text.ToUpper() + "</b> Good day! <br/>" +
                                                          "Kindly check the attached files for your evaluation result for " + ddFiscalYear.SelectedItem.Text + " fiscal year. <br/><br/>" +
                                                          "For any questions or clarifications, please send an email to imelda.limon@adm.rohmphil.com <br />" +
                                                          "Thank you and have a great day! <br/><br/>" +
                                                          "ROHM Electronics Philippines Inc.";

                                        string emailService = COMMON.sendEmailToSuppliers(lblEmail.Text.Trim(), ConfigurationManager.AppSettings["email-username"], ("SE_EVALUATION_RESULT_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim())),
                                                                                          verbiage, supplier_evaluation_xlsx, lblName.Text.ToUpper(), string.Empty);


                                        if (emailService.ToLower().Contains("success"))
                                        {
                                            string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                                            string query1 = "INSERT INTO SE_TRANSACTION_SendReceived (FY_SupplierId, SendReceivedDate, TransactionType, SendBy, FiscalYear) VALUES ('" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "',GETDATE(),'SEND_RESULT','" + Session["UserFullName"].ToString() + "','" + ddFiscalYear.SelectedValue + "') ";
                                            string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                                            string query_Success = BLL.SE_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                                            if (query_Success == "1")
                                            {
                                                rd_Query_Counter++;
                                            }

                                        }




                                    }


                                }
                            }

                            

                        }

                    }

                }
            }


            Session["successMessage"] = "SUPPLIER EVALUATION RESULT FOR FISCAL YEAR : <b>" + ddFiscalYear.SelectedItem.Text + "</b> HAS BEEN SUCCESSFULLY SENT.";
            Session["successTransactionName"] = "SE_EvaluationEntry";
            Session["successReturnPage"] = "SE_EvaluationMonitoring.aspx";
            Response.Redirect("SuccessPage.aspx");


        }


        protected void btnProceed_Click(object sender, EventArgs e)
        {
            try
            {
                int rd_Query_Counter = 0;
                string fy_from = string.Empty;
                string fy_to = string.Empty;


                btnProceed.Enabled = false;

                if (Request.QueryString["send_evaluation"].ToString() == "true")
                {
                    sendEvaluationToSuppliers();
                }
                else
                {

                    //-----------------------------------------------------------------------------------------
                    List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();
                    list = BLL.SE_MT_FiscalYear_GetAll();

                    if (list != null)
                    {
                        if (list.Count > 0)
                        {

                            foreach (Entities_SE_FiscalYear entity in list)
                            {

                                if (entity.RefId == ddFiscalYear.SelectedValue)
                                {
                                    fy_from = entity.From;
                                    fy_to = entity.To;
                                }

                            }
                        }
                    }
                    //-----------------------------------------------------------------------------------------

                    int failedToSend = 0;

                    if (gvData.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvData.Rows.Count; i++)
                        {
                            ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[0].FindControl("ibApproved");
                            Label lblId = (Label)gvData.Rows[i].Cells[3].FindControl("lblId");
                            Label lblEmail = (Label)gvData.Rows[i].Cells[2].FindControl("lblEmail");
                            Label lblName = (Label)gvData.Rows[i].Cells[1].FindControl("lblName");
                            Label lblReceipient = (Label)gvData.Rows[i].Cells[1].FindControl("lblReceipient");


                            if (ibApproved.ImageUrl == "~/images/A2.png")
                            {
                                if (!String.IsNullOrEmpty(Request.QueryString["fy_year"].ToString()))
                                {                                    
                                    //EXISTING ENTRY
                                    //FOR SENDING
                                    if (!string.IsNullOrEmpty(lblReceipient.Text) || lblReceipient.Text == "0")
                                    {
                                        try
                                        {

                                            if (!System.IO.Directory.Exists(Server.MapPath("~/SE_Request/" + ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim())))
                                            {
                                                System.IO.Directory.CreateDirectory(Server.MapPath("~/SE_Request/" + ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()));
                                            }

                                            System.IO.File.Copy(Server.MapPath("~/SE_XLS/SE_BasicInformation.xlsx"), Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_BasicInformation.xlsx"));
                                            System.IO.File.Copy(Server.MapPath("~/SE_XLS/SE_Financial_Analysis.xlsx"), Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_Financial_Analysis.xlsx"));

                                            // BASIC INFORMATION
                                            string pathBI_Resend1 = Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_BasicInformation.xlsx");
                                            Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathBI_Resend1);
                                            FileStream fsBI_Resend1 = new FileStream(pathBI_Resend1, FileMode.Open);
                                            using (SLDocument basicInformation_Resend1 = new SLDocument(fsBI_Resend1, "Sheet1"))
                                            {
                                                basicInformation_Resend1.SetCellValue("C7", lblName.Text.ToUpper());
                                                basicInformation_Resend1.SetCellValue("E5", fy_from);
                                                basicInformation_Resend1.SetCellValue("J5", fy_to);
                                                fsBI_Resend1.Close();
                                                basicInformation_Resend1.SaveAs(pathBI_Resend1);
                                            }

                                            // FINANCIAL ANALYSIS
                                            string pathFA_Resend1 = Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_Financial_Analysis.xlsx");
                                            Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathFA_Resend1);
                                            FileStream fsFA_Resend1 = new FileStream(pathFA_Resend1, FileMode.Open);
                                            using (SLDocument financialAnalysis_Resend1 = new SLDocument(fsFA_Resend1, "Bankruptcy factors"))
                                            {
                                                financialAnalysis_Resend1.SetCellValue("C4", lblName.Text.ToUpper());
                                                fsFA_Resend1.Close();
                                                financialAnalysis_Resend1.SaveAs(pathFA_Resend1);
                                            }

                                            //=========== Sending Email =================
                                            string basic_information_Resend1 = Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_BasicInformation.xlsx");
                                            string financial_analysis_Resend1 = Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_Financial_Analysis.xlsx");
                                            string path_Resend1 = basic_information_Resend1 + "," + financial_analysis_Resend1;
                                            string verbiage_Resend1 = "Hi <b>" + lblName.Text.ToUpper() + "</b> Good day! <br/>" +
                                                              "Kindly check the attached files for your evaluation for " + ddFiscalYear.SelectedItem.Text + " fiscal year. <br/><br/>" +
                                                              "For any questions or clarifications, please send an email to imelda.limon@adm.rohmphil.com <br />" +
                                                              "Thank you and have a great day! <br/><br/>" +
                                                              "ROHM Electronics Philippines Inc.";

                                            string emailService_Resend1 = COMMON.sendEmailToSuppliers(lblEmail.Text.Trim(), ConfigurationManager.AppSettings["email-username"], ("SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim())),
                                                                                              verbiage_Resend1, path_Resend1, lblName.Text.ToUpper(), string.Empty);


                                            if (emailService_Resend1.ToLower().Contains("success"))
                                            {
                                                string queryBeginPart_Resend1 = "BEGIN TRY BEGIN TRANSACTION ";
                                                string query1_Resend1 = "INSERT INTO SE_TRANSACTION_RequestHead (FiscalYear, FY_SupplierId, AddedBy, AddedDate) " +
                                                                "VALUES ('" + ddFiscalYear.SelectedValue + "','" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "','" + Session["UserFullName"].ToString() + "',GETDATE()) ";
                                                string query2_Resend1 = "INSERT INTO SE_TRANSACTION_RequestDetails (FY_SupplierId, SupplierRef) VALUES ('" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "','" + lblId.Text.Trim() + "') ";
                                                string query3_Resend1 = "INSERT INTO SE_TRANSACTION_StatusEvaluationTable (FY_SupplierId) VALUES ('" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "') ";
                                                string query4_Resend1 = "INSERT INTO SE_TRANSACTION_StatusRevaluationSheet (FY_SupplierId) VALUES ('" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "') ";
                                                string query5_Resend1 = "INSERT INTO SE_TRANSACTION_SendReceived (FY_SupplierId, SendReceivedDate, TransactionType, SendBy, FiscalYear) VALUES ('" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "',GETDATE(),'SEND','" + Session["UserFullName"].ToString() + "','" + ddFiscalYear.SelectedValue + "') ";
                                                string queryEndPart_Resend1 = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                                                string query_Success_Resend1 = BLL.SE_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart_Resend1 + query1_Resend1 + query2_Resend1 + query3_Resend1 + query4_Resend1 + query5_Resend1 + queryEndPart_Resend1).ToString();

                                                if (query_Success_Resend1 == "5")
                                                {
                                                    rd_Query_Counter++;
                                                }

                                            }

                                        }
                                        catch (Exception exExisting)
                                        {
                                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + exExisting.Message + "');", true);
                                        }

                                    }
                                    else
                                    {
                                        try
                                        {
                                            //FOR RESEND
                                            //=========== Sending Email =================
                                            string basic_information_resend = Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_BasicInformation.xlsx");
                                            string financial_analysis_resend = Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_Financial_Analysis.xlsx");
                                            string path_resend = basic_information_resend + "," + financial_analysis_resend;
                                            string verbiage_resend = "Hi <b>" + lblName.Text.ToUpper() + "</b> Good day! <br/>" +
                                                              "Kindly check the attached files for your evaluation for " + ddFiscalYear.SelectedItem.Text + " fiscal year. <br/><br/>" +
                                                              "For any questions or clarifications, please send an email to imelda.limon@adm.rohmphil.com <br />" +
                                                              "Thank you and have a great day! <br/><br/>" +
                                                              "ROHM Electronics Philippines Inc.";

                                            string emailService = COMMON.sendEmailToSuppliers(lblEmail.Text.Trim(), ConfigurationManager.AppSettings["email-username"], ("SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim())),
                                                                                              verbiage_resend, path_resend, lblName.Text.ToUpper(), string.Empty);


                                            if (emailService.ToLower().Contains("success"))
                                            {
                                                string queryBeginPart_Resend1 = "BEGIN TRY BEGIN TRANSACTION ";
                                                string query1_Resend1 = "INSERT INTO SE_TRANSACTION_SendReceived (FY_SupplierId, SendReceivedDate, TransactionType, SendBy, FiscalYear) VALUES ('" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "',GETDATE(),'SEND','" + Session["UserFullName"].ToString() + "','" + ddFiscalYear.SelectedValue + "') ";
                                                string queryEndPart_Resend1 = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                                                string query_Success_Resend1 = BLL.SE_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart_Resend1 + query1_Resend1 + queryEndPart_Resend1).ToString();

                                                if (query_Success_Resend1 == "1")
                                                {
                                                    rd_Query_Counter++;
                                                }

                                            }

                                        }
                                        catch (Exception exResend)
                                        {
                                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + exResend.Message + "');", true);
                                        }

                                    }

                                }
                                else
                                {
                                    try
                                    {

                                        // NEW ENTRY
                                        // FOR SENDING
                                        // -------------------------------------------------------------------------------------------------------------------------------------------------------------
                                        try
                                        {
                                            if (!System.IO.Directory.Exists(Server.MapPath("~/SE_Request/" + ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim())))
                                            {
                                                System.IO.Directory.CreateDirectory(Server.MapPath("~/SE_Request/" + ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()));
                                            }
                                        }
                                        catch (Exception exSERequest)
                                        {
                                            InsertServiceLog("NEW ENTRY / FOR SENDING : " + exSERequest.InnerException.ToString());
                                        }
                                        // -------------------------------------------------------------------------------------------------------------------------------------------------------------


                                        // -------------------------------------------------------------------------------------------------------------------------------------------------------------
                                        try
                                        {
                                            // BASIC INFORMATION
                                            if (!System.IO.File.Exists(Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_BasicInformation.xlsx")))
                                            {
                                                System.IO.File.Copy(Server.MapPath("~/SE_XLS/SE_BasicInformation.xlsx"), Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_BasicInformation.xlsx"));
                                            }
                                            else
                                            {
                                                System.IO.File.Delete(Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_BasicInformation.xlsx"));
                                                System.IO.File.Copy(Server.MapPath("~/SE_XLS/SE_BasicInformation.xlsx"), Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_BasicInformation.xlsx"));
                                            }
                                        }
                                        catch (Exception exBasicInformation)
                                        {
                                            InsertServiceLog("BASIC INFORMATION : " + exBasicInformation.InnerException.ToString());
                                        }
                                        // -------------------------------------------------------------------------------------------------------------------------------------------------------------


                                        // -------------------------------------------------------------------------------------------------------------------------------------------------------------
                                        try
                                        {
                                            // FINANCIAL ANALYSIS
                                            if (!System.IO.File.Exists(Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_Financial_Analysis.xlsx")))
                                            {
                                                System.IO.File.Copy(Server.MapPath("~/SE_XLS/SE_Financial_Analysis.xlsx"), Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_Financial_Analysis.xlsx"));
                                            }
                                            else
                                            {
                                                System.IO.File.Delete(Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_Financial_Analysis.xlsx"));
                                                System.IO.File.Copy(Server.MapPath("~/SE_XLS/SE_Financial_Analysis.xlsx"), Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_Financial_Analysis.xlsx"));
                                            }
                                        }
                                        catch (Exception exFinancialAnalysis)
                                        {
                                            InsertServiceLog("FINANCIAL ANALYSIS : " + exFinancialAnalysis.InnerException.ToString());
                                        }
                                        // -------------------------------------------------------------------------------------------------------------------------------------------------------------


                                        // -------------------------------------------------------------------------------------------------------------------------------------------------------------
                                        try
                                        {
                                            // BASIC INFORMATION
                                            string pathBI = Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_BasicInformation.xlsx");
                                            Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathBI);
                                            FileStream fsBI = new FileStream(pathBI, FileMode.Open);
                                            using (SLDocument basicInformation = new SLDocument(fsBI, "Sheet1"))
                                            {
                                                basicInformation.SetCellValue("C7", lblName.Text.ToUpper());
                                                basicInformation.SetCellValue("E5", fy_from);
                                                basicInformation.SetCellValue("J5", fy_to);
                                                fsBI.Close();
                                                basicInformation.SaveAs(pathBI);
                                            }
                                        }
                                        catch (Exception exBI_SLDocument)
                                        {
                                            InsertServiceLog("BASIC INFORMATION SL DOCUMENT : " + exBI_SLDocument.InnerException.ToString());
                                        }
                                        // -------------------------------------------------------------------------------------------------------------------------------------------------------------


                                        // -------------------------------------------------------------------------------------------------------------------------------------------------------------
                                        try
                                        {
                                            // FINANCIAL ANALYSIS
                                            string pathFA = Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_Financial_Analysis.xlsx");
                                            Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathFA);
                                            FileStream fsFA = new FileStream(pathFA, FileMode.Open);
                                            using (SLDocument financialAnalysis = new SLDocument(fsFA, "Bankruptcy factors"))
                                            {
                                                financialAnalysis.SetCellValue("C4", lblName.Text.ToUpper());
                                                fsFA.Close();
                                                financialAnalysis.SaveAs(pathFA);
                                            }
                                        }
                                        catch (Exception exFA_SLDocument)
                                        {
                                            InsertServiceLog("FINANCIAL ANALYSIS SL DOCUMENT : " + exFA_SLDocument.InnerException.ToString());
                                        }
                                        // -------------------------------------------------------------------------------------------------------------------------------------------------------------


                                        //=========== Sending Email =================
                                        string basic_information = Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_BasicInformation.xlsx");
                                        string financial_analysis = Server.MapPath("~/SE_Request/" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "/SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "_Financial_Analysis.xlsx");
                                        string se_notes = Server.MapPath("~/UserManual/SupplierEvaluation_NOTES.docx");
                                        string path = basic_information + "," + financial_analysis + "," + se_notes;
                                        string verbiage = "Hi <b>" + lblName.Text.ToUpper() + "</b> Good day! <br/>" +
                                                          "Kindly check the attached files for your evaluation for " + ddFiscalYear.SelectedItem.Text + " fiscal year. <br/><br/>" +
                                                          "For any questions or clarifications, please send an email to " + Session["UserFullName"].ToString() + " (" + Session["UserEmail"].ToString() + ") and cc:imelda.limon@adm.rohmphil.com <br />" +
                                                          "Thank you and have a great day! <br/><br/>" +
                                                          "ROHM Electronics Philippines Inc.";

                                        string emailService = COMMON.sendEmailToSuppliers(lblEmail.Text.Trim(), ConfigurationManager.AppSettings["email-username"], ("SE_" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim())),
                                                                                          verbiage, path, lblName.Text.ToUpper(), string.Empty);


                                        if (emailService.ToLower().Contains("success"))
                                        {
                                            string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                                            string query1 = "INSERT INTO SE_TRANSACTION_RequestHead (FiscalYear, FY_SupplierId, AddedBy, AddedDate) " +
                                                            "VALUES ('" + ddFiscalYear.SelectedValue + "','" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "','" + Session["UserFullName"].ToString() + "',GETDATE()) ";
                                            string query2 = "INSERT INTO SE_TRANSACTION_RequestDetails (FY_SupplierId, SupplierRef) VALUES ('" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "','" + lblId.Text.Trim() + "') ";
                                            string query3 = "INSERT INTO SE_TRANSACTION_StatusEvaluationTable (FY_SupplierId) VALUES ('" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "') ";
                                            string query4 = "INSERT INTO SE_TRANSACTION_StatusRevaluationSheet (FY_SupplierId) VALUES ('" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "') ";
                                            string query5 = "INSERT INTO SE_TRANSACTION_SendReceived (FY_SupplierId, SendReceivedDate, TransactionType, SendBy, FiscalYear) VALUES ('" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "',GETDATE(),'SEND','" + Session["UserFullName"].ToString() + "','" + ddFiscalYear.SelectedValue + "') ";
                                            string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                                            string query_Success = BLL.SE_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + query2 + query3 + query4 + query5 + queryEndPart).ToString();

                                            if (query_Success == "5")
                                            {
                                                rd_Query_Counter++;
                                            }

                                        }
                                        else
                                        {
                                            failedToSend++;
                                        }
                                        //else
                                        //{
                                        //    string queryBeginPart_2 = "BEGIN TRY BEGIN TRANSACTION ";
                                        //    string query1_2 = "INSERT INTO SE_TRANSACTION_RequestHead (FiscalYear, FY_SupplierId, AddedBy, AddedDate) " +
                                        //                    "VALUES ('" + ddFiscalYear.SelectedValue + "','" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "','" + Session["UserFullName"].ToString() + "',GETDATE()) ";
                                        //    string query2_2 = "INSERT INTO SE_TRANSACTION_RequestDetails (FY_SupplierId, SupplierRef) VALUES ('" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "','" + lblId.Text.Trim() + "') ";
                                        //    string query3_2 = "INSERT INTO SE_TRANSACTION_StatusEvaluationTable (FY_SupplierId) VALUES ('" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "') ";
                                        //    string query4_2 = "INSERT INTO SE_TRANSACTION_StatusRevaluationSheet (FY_SupplierId) VALUES ('" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "') ";
                                        //    string query5_2 = "INSERT INTO SE_TRANSACTION_SendReceived (FY_SupplierId, SendReceivedDate, TransactionType, SendBy, FiscalYear) VALUES ('" + (ddFiscalYear.SelectedValue + "_" + lblId.Text.Trim()) + "',GETDATE(),'FAILED_TO_SEND','" + Session["UserFullName"].ToString() + "','" + ddFiscalYear.SelectedValue + "') ";
                                        //    string queryEndPart_2 = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                                        //    string query_Success_2 = BLL.SE_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart_2 + query1_2 + query2_2 + query3_2 + query4_2 + query5_2 + queryEndPart_2).ToString();

                                        //    if (query_Success_2 == "5")
                                        //    {
                                        //        rd_Query_Counter++;
                                        //    }
                                        //}
                                    }
                                    catch (Exception exNewEntry)
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + exNewEntry.Message + "');", true);
                                    }



                                }


                            } // End of if (ibApproved.ImageUrl == "~/images/A2.png")

                        }

                        if (failedToSend > 0)
                        {
                            Session["successMessage"] = "SOME REQUEST HAS FAILED TO SEND";
                            Session["successTransactionName"] = "SE_EvaluationEntry";
                            Session["successReturnPage"] = "SE_EvaluationMonitoring.aspx";
                            Response.Redirect("SuccessPage.aspx");
                        }
                        else
                        {
                            Session["successMessage"] = "REQUEST FOR FISCAL YEAR : <b>" + ddFiscalYear.SelectedItem.Text + "</b> HAS BEEN SUCCESSFULLY CREATED.";
                            Session["successTransactionName"] = "SE_EvaluationEntry";
                            Session["successReturnPage"] = "SE_EvaluationMonitoring.aspx";
                            Response.Redirect("SuccessPage.aspx");
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        // THIS IS FOR SERVICE LOGS -------------------------------------------------------------------

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

        // --------------------------------------------------------------------------------------------






    }
}
