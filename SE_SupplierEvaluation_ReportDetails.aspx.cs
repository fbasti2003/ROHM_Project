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
using SpreadsheetLight;

namespace REPI_PUR_SOFRA
{
    public partial class SE_SupplierEvaluation_ReportDetails : System.Web.UI.Page
    {
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
                    if (!String.IsNullOrEmpty(Request.QueryString["fy_year"].ToString()))
                    {
                        LoadDefault();
                    }                    
                }
            }
        }

        private void LoadDefault()
        {
            try
            {
                //List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();
                //list = BLL.SE_MT_FiscalYear_GetAll();

                //if (list != null)
                //{
                //    if (list.Count > 0)
                //    {
                //        ddFiscalYear.Items.Clear();
                //        ddFiscalYear.Items.Add("");

                //        foreach (Entities_SE_FiscalYear entity in list)
                //        {
                //            ListItem item = new ListItem();
                //            item.Text = entity.Description;
                //            item.Value = entity.RefId;

                //            if (entity.Isdisabled == string.Empty || entity.Isdisabled == "0")
                //            {
                //                ddFiscalYear.Items.Add(item);
                //            }

                //        }
                //    }
                //}


                List<Entities_SE_FiscalYear> list_Monitoring = new List<Entities_SE_FiscalYear>();
                list_Monitoring = BLL.SE_TRANSACTION_Monitoring_GetAll2().Where(itm => itm.RefId == Request.QueryString["fy_year"].ToString()).ToList();

                if (list_Monitoring != null)
                {
                    if (list_Monitoring.Count > 0)
                    {
                        gvData.DataSource = list_Monitoring;
                        gvData.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvChart_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtChartScore = (TextBox)e.Row.FindControl("txtChartScore");
                    Label lblChartScore = (Label)e.Row.FindControl("lblChartScore");
                    Label lblJudgementByPerson = (Label)e.Row.FindControl("lblJudgementByPerson");

                    txtChartScore.Style.Add("Width", lblChartScore.Text.Trim());

                    if (lblJudgementByPerson.Text == "INCREASE")
                    {
                        //txtChartScore.BackColor = System.Drawing.Color.Green;
                        txtChartScore.Style.Add("background-color", "#80A459");
                    }
                    if (lblJudgementByPerson.Text == "CONTINUE")
                    {
                        //txtChartScore.BackColor = System.Drawing.Color.Purple;
                        txtChartScore.Style.Add("background-color", "#F9CA3D");
                    }
                    if (lblJudgementByPerson.Text == "REDUCE")
                    {
                        //txtChartScore.BackColor = System.Drawing.Color.Brown;
                        txtChartScore.Style.Add("background-color", "#F9CA3D");
                    }
                    if (lblJudgementByPerson.Text == "STOP")
                    {
                        //txtChartScore.BackColor = System.Drawing.Color.Red;
                        txtChartScore.Style.Add("background-color", "#F9CA3D");
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
                    GridView gvScores = (GridView)e.Row.FindControl("gvScores");
                    GridView gvRanking = (GridView)e.Row.FindControl("gvRanking");

                    List<Entities_SE_RequestEntry> list_ChartScores = new List<Entities_SE_RequestEntry>();
                    List<Entities_SE_RequestEntry> list_SupplierEvaluation = new List<Entities_SE_RequestEntry>();
                    list_SupplierEvaluation = BLL.SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear2(Request.QueryString["fy_year"].ToString());

                    if (list_SupplierEvaluation != null)
                    {
                        if (list_SupplierEvaluation.Count > 0)
                        {
                            gvScores.DataSource = list_SupplierEvaluation;
                            gvScores.DataBind();

                            gvRanking.DataSource = list_SupplierEvaluation.OrderByDescending(itm => double.Parse(itm.EI_TotalScore)).ToList();
                            gvRanking.DataBind();

                            foreach (Entities_SE_RequestEntry e_Chart in list_SupplierEvaluation.OrderByDescending(itm => double.Parse(itm.EI_TotalScore)).ToList())
                            {
                                Entities_SE_RequestEntry entityChart = new Entities_SE_RequestEntry();
                                entityChart.SupplierName = e_Chart.SupplierName.ToUpper() + "&nbsp;&nbsp;&nbsp;";
                                entityChart.EI_TotalScore = "&nbsp;&nbsp;" + e_Chart.EI_TotalScore;
                                entityChart.CustomerChartWidthExtended = e_Chart.CustomerChartWidthExtended;
                                entityChart.JudgementByPerson = e_Chart.JudgementByPerson;

                                list_ChartScores.Add(entityChart);
                            }

                            if (list_ChartScores != null)
                            {
                                if (list_ChartScores.Count > 0)
                                {
                                    gvChart.DataSource = list_ChartScores;
                                    gvChart.DataBind();
                                }
                            }

                        }
                    }

                    List<Entities_SE_RequestEntry> list_details = new List<Entities_SE_RequestEntry>();
                    list_details = BLL.SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear2(Request.QueryString["fy_year"].ToString());

                    if (list_details != null)
                    {
                        if (list_details.Count > 0)
                        {
                            gvDetails.Visible = true;
                            gvDetails.DataSource = list_details;
                            gvDetails.DataBind();
                        }
                        else
                        {
                            gvDetails.Visible = false;
                        }
                    }
                    else
                    {
                        gvDetails.Visible = false;
                    }


                    //e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
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



            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('3 - " + ex.StackTrace.ToString() + "');", true);
            }
        }

        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(ddFiscalYear.SelectedItem.Text))
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select a valid Fiscal Year.');", true);
        //        }
        //        else
        //        {
        //            List<Entities_SE_FiscalYear> list_Monitoring = new List<Entities_SE_FiscalYear>();
        //            list_Monitoring = BLL.SE_TRANSACTION_Monitoring_GetAll().Where(itm => itm.RefId == ddFiscalYear.SelectedValue).ToList();

        //            if (list_Monitoring != null)
        //            {
        //                if (list_Monitoring.Count > 0)
        //                {
        //                    gvData.Visible = true;
        //                    gvData.DataSource = list_Monitoring;
        //                    gvData.DataBind();
        //                }
        //                else
        //                {
        //                    gvData.Visible = false;
        //                }
        //            }
        //            else
        //            {
        //                gvData.Visible = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
        //    }
        //}

        protected void btnDownloadReport_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities_SE_RequestEntry> list_details = new List<Entities_SE_RequestEntry>();
                list_details = BLL.SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear2(Request.QueryString["fy_year"].ToString());

                List<Entities_SE_RequestEntry> list_SupplierEvaluation = new List<Entities_SE_RequestEntry>();
                list_SupplierEvaluation = BLL.SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear2(Request.QueryString["fy_year"].ToString());

                string fiscalYear = Request.QueryString["fy_year"].ToString();
                string reportName = fiscalYear + "_SE_Evaluation_For_FiscaYear";
                string evaluationReportName = fiscalYear + "_SE_Evaluation";
                int counter = 1;
                int counterForEvaluation = 1;
                int counterForRanking = 1;
                int startingRow = 6;
                int startingRowFoEvaluation = 7;
                int startingRowForRanking = 7;


                if (!String.IsNullOrEmpty(Request.QueryString["fy_year"].ToString()))
                {
                    if (list_details != null)
                    {
                        if (list_details.Count > 0)
                        {
                            if (!System.IO.Directory.Exists(Server.MapPath("~/SE_REPORTS/" + reportName)))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/SE_REPORTS/" + reportName));
                            }

                            // SUPPLIER TABLE
                            if (!System.IO.File.Exists(Server.MapPath("~/SE_REPORTS/" + reportName + "/" + reportName + ".xlsx")))
                            {
                                System.IO.File.Copy(Server.MapPath("~/SE_XLS/SE_Evaluation_For_FiscaYear.xlsx"), Server.MapPath("~/SE_REPORTS/" + reportName + "/" + reportName + ".xlsx"));                                
                            }
                            else
                            {
                                System.IO.File.Delete(Server.MapPath("~/SE_REPORTS/" + reportName + "/" + reportName + ".xlsx"));
                                System.IO.File.Copy(Server.MapPath("~/SE_XLS/SE_Evaluation_For_FiscaYear.xlsx"), Server.MapPath("~/SE_REPORTS/" + reportName + "/" + reportName + ".xlsx"));                                
                            }

                            // EVALUATION TABLE
                            if (!System.IO.File.Exists(Server.MapPath("~/SE_REPORTS/" + reportName + "/" + evaluationReportName + ".xlsx")))
                            {
                                System.IO.File.Copy(Server.MapPath("~/SE_XLS/SE_Evaluation.xlsx"), Server.MapPath("~/SE_REPORTS/" + reportName + "/" + evaluationReportName + ".xlsx"));
                            }
                            else
                            {
                                System.IO.File.Delete(Server.MapPath("~/SE_REPORTS/" + reportName + "/" + evaluationReportName + ".xlsx"));
                                System.IO.File.Copy(Server.MapPath("~/SE_XLS/SE_Evaluation.xlsx"), Server.MapPath("~/SE_REPORTS/" + reportName + "/" + evaluationReportName + ".xlsx"));
                            }


                            //SUPPLIER TABLE
                            string path_SupplierTable = Server.MapPath("~/SE_REPORTS/" + reportName + "/" + reportName + ".xlsx");
                            Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + path_SupplierTable);
                            FileStream fs_SupplierTable = new FileStream(path_SupplierTable, FileMode.Open);
                            using (SLDocument sl_SupplierTable = new SLDocument(fs_SupplierTable, "Supplier Eval Table"))
                            {

                                foreach (Entities_SE_RequestEntry entity in list_details)
                                {

                                    sl_SupplierTable.SetCellValue("B" + startingRow.ToString(), counter.ToString()); // NO
                                    sl_SupplierTable.SetCellValue("C" + startingRow.ToString(), entity.SupplierName.ToUpper()); // Supplier Name
                                    sl_SupplierTable.SetCellValue("D" + startingRow.ToString(), entity.Classification.ToUpper()); // Trader/Maker
                                    sl_SupplierTable.SetCellValue("E" + startingRow.ToString(), entity.MakerName.ToUpper()); // Maker Name

                                    sl_SupplierTable.SetCellValue("F" + startingRow.ToString(), entity.AutomotiveRelated.ToUpper()); // AutomotiveRelated
                                    sl_SupplierTable.SetCellValue("G" + startingRow.ToString(), entity.ISO9001.ToUpper()); // ISO9001
                                    sl_SupplierTable.SetCellValue("H" + startingRow.ToString(), entity.ISO14001.ToUpper()); // ISO14001
                                    sl_SupplierTable.SetCellValue("I" + startingRow.ToString(), entity.IATF16949.ToUpper()); // IATF16949
                                    sl_SupplierTable.SetCellValue("J" + startingRow.ToString(), entity.ItemClassification.ToUpper()); // ItemClassification

                                    sl_SupplierTable.SetCellValue("K" + startingRow.ToString(), entity.EI_FinancialAnalysis.ToUpper()); // EI_FinancialAnalysis
                                    sl_SupplierTable.SetCellValue("L" + startingRow.ToString(), entity.EI_Quality.ToUpper()); // EI_Quality
                                    sl_SupplierTable.SetCellValue("M" + startingRow.ToString(), entity.EI_CostResponse.ToUpper()); // EI_CostResponse
                                    sl_SupplierTable.SetCellValue("N" + startingRow.ToString(), entity.EI_Delivery.ToUpper()); // EI_Delivery
                                    sl_SupplierTable.SetCellValue("O" + startingRow.ToString(), entity.EI_Cooperation.ToUpper()); // EI_Cooperation
                                    sl_SupplierTable.SetCellValue("P" + startingRow.ToString(), entity.EI_CSR.ToUpper()); // EI_CSR
                                    sl_SupplierTable.SetCellValue("Q" + startingRow.ToString(), entity.EI_TotalScore.ToUpper()); // EI_TotalScore

                                    sl_SupplierTable.SetCellValue("R" + startingRow.ToString(), entity.JudgementByPerson.ToUpper()); // JudgementByPerson
                                    sl_SupplierTable.SetCellValue("S" + startingRow.ToString(), entity.JudgementYearMonth.ToUpper()); // JudgementByPerson

                                    if (entity.JudgementByPerson.ToUpper() == "INCREASE")
                                    {
                                        sl_SupplierTable.SetCellValue("T" + startingRow.ToString(), "●"); // EXPAND
                                    }
                                    if (entity.JudgementByPerson.ToUpper() == "CONTINUE")
                                    {
                                        sl_SupplierTable.SetCellValue("U" + startingRow.ToString(), "●"); // CONTINUE
                                    }
                                    if (entity.JudgementByPerson.ToUpper() == "REDUCE")
                                    {
                                        sl_SupplierTable.SetCellValue("V" + startingRow.ToString(), "●"); // REDUCE
                                    }
                                    if (entity.JudgementByPerson.ToUpper() == "STOP")
                                    {
                                        sl_SupplierTable.SetCellValue("W" + startingRow.ToString(), "●"); // STOP
                                    }

                                    sl_SupplierTable.SetCellValue("X" + startingRow.ToString(), entity.SubContractor.ToUpper()); // SubContractor
                                    sl_SupplierTable.SetCellValue("Y" + startingRow.ToString(), entity.NoteworthyPoints.ToUpper()); // NoteworthyPoints
                                    sl_SupplierTable.SetCellValue("Z" + startingRow.ToString(), entity.Reason.ToUpper()); // Reason
                                    sl_SupplierTable.SetCellValue("AA" + startingRow.ToString(), entity.CircularComments.ToUpper()); // CircularComments
                                    sl_SupplierTable.SetCellValue("AB" + startingRow.ToString(), entity.DivManInstructions.ToUpper()); // DivManInstructions


                                    if (counter <= 1)
                                    {
                                        sl_SupplierTable.SetCellValue("B2", "[" + entity.JudgementYearMonth + "] SUPPLIER EVALUATION TABLE"); // FISCAL YEAR

                                        sl_SupplierTable.SetCellValue("K2", entity.Incharge.ToUpper()); // Incharge
                                        sl_SupplierTable.SetCellValue("M2", entity.SectionIncharge.ToUpper()); // SectionIncharge
                                        sl_SupplierTable.SetCellValue("O2", entity.DeptManager.ToUpper()); // DeptManager
                                        sl_SupplierTable.SetCellValue("Q2", entity.DivManager.ToUpper()); // DivManager
                                    }

                                    counter++;
                                    startingRow++;
                                    

                                }                                

                                fs_SupplierTable.Close();
                                sl_SupplierTable.SaveAs(path_SupplierTable);
                                

                            }

                            //EVALUATION TABLE
                            string path_EvalutionTable = Server.MapPath("~/SE_REPORTS/" + reportName + "/" + evaluationReportName + ".xlsx");
                            Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + path_EvalutionTable);
                            FileStream fs_EvalutionTable = new FileStream(path_EvalutionTable, FileMode.Open);
                            using (SLDocument sl_EvalutionTable = new SLDocument(fs_EvalutionTable, "EVALUATION"))
                            {
                                foreach (Entities_SE_RequestEntry entityEvaluation in list_SupplierEvaluation)
                                {
                                    sl_EvalutionTable.SetCellValue("A" + startingRowFoEvaluation.ToString(), counterForEvaluation.ToString()); // NO
                                    sl_EvalutionTable.SetCellValue("B" + startingRowFoEvaluation.ToString(), entityEvaluation.SupplierName.ToUpper()); // SUPPLIER NAME
                                    sl_EvalutionTable.SetCellValue("C" + startingRowFoEvaluation.ToString(), entityEvaluation.EI_TotalScore.ToUpper()); // POINTS
                                    sl_EvalutionTable.SetCellValue("D" + startingRowFoEvaluation.ToString(), entityEvaluation.EI_EvaluationPoints.ToUpper()); // CLASS

                                    if (counterForEvaluation <= 1)
                                    {
                                        sl_EvalutionTable.SetCellValue("A3", "Period Covered: " + entityEvaluation.JudgementYearMonth); // FISCAL YEAR
                                        sl_EvalutionTable.SetCellValue("J3", entityEvaluation.Incharge.ToUpper()); // Incharge
                                        sl_EvalutionTable.SetCellValue("K3", entityEvaluation.SectionIncharge.ToUpper()); // SectionIncharge
                                        sl_EvalutionTable.SetCellValue("L3", entityEvaluation.DeptManager.ToUpper()); // DeptManager
                                        sl_EvalutionTable.SetCellValue("M3", entityEvaluation.DivManager.ToUpper()); // DivManager
                                    }

                                    counterForEvaluation++;
                                    startingRowFoEvaluation++;
                                }

                                foreach (Entities_SE_RequestEntry entityEvaluationRanking in list_SupplierEvaluation.OrderByDescending(itm => double.Parse(itm.EI_TotalScore)).ToList())
                                {
                                    sl_EvalutionTable.SetCellValue("F" + startingRowForRanking.ToString(), entityEvaluationRanking.EI_Ranking); // RANKING
                                    sl_EvalutionTable.SetCellValue("G" + startingRowForRanking.ToString(), entityEvaluationRanking.SupplierName.ToUpper()); // SUPPLIER NAME
                                    sl_EvalutionTable.SetCellValue("H" + startingRowForRanking.ToString(), entityEvaluationRanking.EI_TotalScore.ToUpper()); // SUPPLIER NAME

                                    counterForRanking++;
                                    startingRowForRanking++;
                                }

                                fs_EvalutionTable.Close();
                                sl_EvalutionTable.SaveAs(path_EvalutionTable);

                            }

                            //ScriptManager.RegisterStartupScript(this, GetType(), "ShowRegistered", "showDialog();", true);

                            //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Reports successfully generated.');", true);

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnViewReports_Click(object sender, EventArgs e)
        {
            try
            {
                string fiscalYear = "SE_REPORTS/" + Request.QueryString["fy_year"].ToString();
                string reportName = fiscalYear + "_SE_Evaluation_For_FiscaYear/" + Request.QueryString["fy_year"].ToString() + "_SE_Evaluation.xlsx";

                string query_Success = string.Empty;
                string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                string query1 = string.Empty;
                string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";



                List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();
                list = BLL.SE_MT_FiscalYear_GetAll().Where(itm => itm.RefId == Request.QueryString["fy_year"].ToString()).ToList();

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        foreach (Entities_SE_FiscalYear elist in list)
                        {
                            query1 = "INSERT INTO SE_TRANSACTION_DownloadAuditTrail (FiscalYear,FormName,DownloadedBy,DownloadedDate) " +
                                     "VALUES ('" + elist.Description + "','PERFORMANCE EVALUATION REPORT','" + Session["UserFullName"].ToString().ToUpper() + "', GETDATE())";
                            query_Success = BLL.SE_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();
                        }
                    }
                }

                

                Response.Redirect(reportName, false);
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnSupplierEvaluationTable_Click(object sender, EventArgs e)
        {
            try
            {
                string fiscalYear = "SE_REPORTS/" + Request.QueryString["fy_year"].ToString();
                string reportName = fiscalYear + "_SE_Evaluation_For_FiscaYear/" + Request.QueryString["fy_year"].ToString() + "_SE_Evaluation_For_FiscaYear.xlsx";


                string query_Success = string.Empty;
                string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                string query1 = string.Empty;
                string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";



                List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();
                list = BLL.SE_MT_FiscalYear_GetAll().Where(itm => itm.RefId == Request.QueryString["fy_year"].ToString()).ToList();

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        foreach (Entities_SE_FiscalYear elist in list)
                        {
                            query1 = "INSERT INTO SE_TRANSACTION_DownloadAuditTrail (FiscalYear,FormName,DownloadedBy,DownloadedDate) " +
                                     "VALUES ('" + elist.Description + "','SUPPLIER EVALUATION TABLE REPORT','" + Session["UserFullName"].ToString().ToUpper() + "', GETDATE())";
                            query_Success = BLL.SE_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();
                        }
                    }
                }

                Response.Redirect(reportName, false);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        //protected void lbEvaluation_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string fiscalYear = Request.QueryString["fy_year"].ToString();
        //        string reportName = fiscalYear + "_SE_Evaluation_For_FiscaYear";
        //        string path_SupplierTable = Server.MapPath("~/SE_REPORTS/" + reportName + "/" + reportName + ".xlsx");

        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AddHeader("content-disposition", "attachment; filename=" + path_SupplierTable);
        //        Response.TransmitFile(path_SupplierTable);
        //        Response.End();

        //        //Response.Redirect(path_SupplierTable, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
        //    }
        //}

        //protected void lbFiscalYearEvaluation_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string fiscalYear = Request.QueryString["fy_year"].ToString();
        //        string reportName = fiscalYear + "_SE_Evaluation_For_FiscaYear";
        //        string evaluationReportName = fiscalYear + "_SE_Evaluation";
        //        string path_EvalutionTable = Server.MapPath("~/SE_REPORTS/" + reportName + "/" + evaluationReportName + ".xlsx");

        //        Response.ContentType = "application/xlsx";
        //        Response.AddHeader("content-disposition", "attachment; filename=" + path_EvalutionTable);
        //        Response.TransmitFile(path_EvalutionTable);
        //        Response.End();
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
        //    }
        //}







    }
}
