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

namespace REPI_PUR_SOFRA
{
    public partial class SE_SupplierEvaluation : System.Web.UI.Page
    {
        BLL_SE BLL = new BLL_SE();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDefault();
                btnSubmit_Click(sender, e);
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
                else
                {
                    List<Entities_SE_RequestEntry> list_SupplierEvaluation = new List<Entities_SE_RequestEntry>();
                    list_SupplierEvaluation = BLL.SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear(ddFiscalYear.SelectedValue);
                    string haveScore = string.Empty;
                    string forApproval = string.Empty;

                    if (list_SupplierEvaluation != null)
                    {
                        if (list_SupplierEvaluation.Count > 0)
                        {
                            gvData.Visible = true;
                            gvData.DataSource = list_SupplierEvaluation;
                            gvData.DataBind();

                            gridViewDummy.Visible = true;
                            gridViewDummy.DataSource = list_SupplierEvaluation;
                            gridViewDummy.DataBind();

                            btnUpdate.Visible = true;

                            foreach (Entities_SE_RequestEntry entity in list_SupplierEvaluation)
                            {
                                if (string.IsNullOrEmpty(entity.EI_TotalScore))
                                {
                                    haveScore += entity.DetailsRefId;
                                }

                                forApproval = entity.ForApproval;
                            }
                        }
                        else
                        {
                            gvData.Visible = false;
                            gridViewDummy.Visible = false;
                        }

                        //if (string.IsNullOrEmpty(haveScore))
                        //{
                        //    btnForApproval.Visible = true;
                        //}

                        //if (forApproval == "1")
                        //{
                        //    btnForApproval.Visible = false;
                        //}
                    }
                    else
                    {
                        gvData.Visible = false;
                        gridViewDummy.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvData.Rows.Count > 0)
                {
                    string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                    string query1 = string.Empty;
                    string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                    int queryCounter = 0;
                    string query_Success = string.Empty;
                    


                    for (int i = 0; i < gvData.Rows.Count; i++)
                    {
                        string quality = string.Empty;
                        double score = 0;
                        double financialAnalysis = 0;
                        double quality_d = 0;
                        double costResponse = 0;
                        double delivery = 0;
                        double cooperation = 0;
                        double csr = 0;
                        double totalScore = 0;
                        string judgementByPerson = string.Empty;
                        string divManExpand = string.Empty;
                        string divManContinue = string.Empty;
                        string divManReduce = string.Empty;
                        string divManStop = string.Empty;


                        Label lblRefId = (Label)gvData.Rows[i].Cells[0].FindControl("lblRefId");
                        Label lblTraderMaker = (Label)gvData.Rows[i].Cells[2].FindControl("lblTraderMaker");
                        TextBox txtMakerName = (TextBox)gvData.Rows[i].Cells[3].FindControl("txtMakerName");
                        DropDownList ddISO9001 = (DropDownList)gvData.Rows[i].Cells[5].FindControl("ddISO9001");
                        DropDownList ddISO14001 = (DropDownList)gvData.Rows[i].Cells[6].FindControl("ddISO14001");
                        DropDownList ddIATF16949 = (DropDownList)gvData.Rows[i].Cells[7].FindControl("ddIATF16949");
                        DropDownList ddFinancialAnalysis = (DropDownList)gvData.Rows[i].Cells[9].FindControl("ddFinancialAnalysis");
                        DropDownList ddQuality = (DropDownList)gvData.Rows[i].Cells[10].FindControl("ddQuality");
                        DropDownList ddCostResponse = (DropDownList)gvData.Rows[i].Cells[11].FindControl("ddCostResponse");
                        DropDownList ddDelivery = (DropDownList)gvData.Rows[i].Cells[12].FindControl("ddDelivery");
                        DropDownList ddCooperation = (DropDownList)gvData.Rows[i].Cells[13].FindControl("ddCooperation");
                        DropDownList ddCSR = (DropDownList)gvData.Rows[i].Cells[14].FindControl("ddCSR");
                        TextBox txtTotalScore = (TextBox)gvData.Rows[i].Cells[15].FindControl("txtTotalScore");
                        Label lblJudgementYearMonth = (Label)gvData.Rows[i].Cells[17].FindControl("lblJudgementYearMonth");
                        //DropDownList ddDivisionManagerEvaluationResult = (DropDownList)gvData.Rows[i].Cells[18].FindControl("ddDivisionManagerEvaluationResult");
                        TextBox txtReason = (TextBox)gvData.Rows[i].Cells[21].FindControl("txtReason");
                        TextBox txtCircularComments = (TextBox)gvData.Rows[i].Cells[22].FindControl("txtCircularComments");
                        TextBox txtDivisionManagerInstructions = (TextBox)gvData.Rows[i].Cells[23].FindControl("txtDivisionManagerInstructions");
                        Label lblNoteworthy = (Label)gvData.Rows[i].Cells[20].FindControl("lblNoteworthy");
                        Label lblItemClassification = (Label)gvData.Rows[i].Cells[8].FindControl("lblItemClassification");
                        LinkButton lbQuality = (LinkButton)gvData.Rows[i].Cells[10].FindControl("lbQuality");

                        if (!string.IsNullOrEmpty(lblTraderMaker.Text))
                        {

                            if (lblItemClassification.Text.ToUpper() == "SUB-MATERIALS")
                            {
                                if (lbQuality.Text.ToUpper() != "SET")
                                {
                                    quality = lbQuality.Text.Trim();
                                    quality_d = double.Parse(quality);
                                }
                            }
                            else
                            {
                                quality = ddQuality.SelectedItem.Text.Substring(0, ddQuality.SelectedItem.Text.IndexOf("-")).Trim();
                                quality_d = double.Parse(quality);
                                quality = ddQuality.SelectedValue;
                            }


                            if (!string.IsNullOrEmpty(ddFinancialAnalysis.SelectedItem.Text))
                            {
                                financialAnalysis = double.Parse(ddFinancialAnalysis.SelectedItem.Text.Substring(0, ddFinancialAnalysis.SelectedItem.Text.IndexOf("-")).Trim());
                            }
                            if (!string.IsNullOrEmpty(ddCostResponse.SelectedItem.Text))
                            {
                                costResponse = double.Parse(ddCostResponse.SelectedItem.Text.Substring(0, ddCostResponse.SelectedItem.Text.IndexOf("-")).Trim());
                            }
                            if (!string.IsNullOrEmpty(ddDelivery.SelectedItem.Text))
                            {
                                delivery = double.Parse(ddDelivery.SelectedItem.Text.Substring(0, ddDelivery.SelectedItem.Text.IndexOf("-")).Trim());
                            }
                            if (!string.IsNullOrEmpty(ddCooperation.SelectedItem.Text))
                            {
                                cooperation = double.Parse(ddCooperation.SelectedItem.Text.Substring(0, ddCooperation.SelectedItem.Text.IndexOf("-")).Trim());
                            }
                            if (!string.IsNullOrEmpty(ddCSR.SelectedItem.Text))
                            {
                                csr = double.Parse(ddCSR.SelectedItem.Text.Substring(0, ddCSR.SelectedItem.Text.IndexOf("-")).Trim());
                            }

                            totalScore = quality_d + financialAnalysis + costResponse + delivery + cooperation + csr;

                            if (financialAnalysis <= 0 || quality_d <= 0 || csr <= 0)
                            {
                                judgementByPerson = "STOP";
                                divManStop = "STOP";
                            }
                            else if (totalScore >= 40 && totalScore <= 59)
                            {
                                judgementByPerson = "REDUCE";
                                divManReduce = "REDUCE";
                            }
                            else if (totalScore >= 60 && totalScore <= 79)
                            {
                                judgementByPerson = "CONTINUE";
                                divManContinue = "CONTINUE";
                            }
                            else if (totalScore >= 80)
                            {
                                judgementByPerson = "INCREASE";
                                divManContinue = "EXPAND";
                            }
                            else
                            {
                                judgementByPerson = "STOP";
                                divManStop = "STOP";
                            }

                            query1 += "UPDATE SE_TRANSACTION_RequestDetails SET MakerName ='" + txtMakerName.Text.Replace("'", "''") + "', " +
                                      "NoteworthyPoints = '" + lblNoteworthy.Text.Replace("'", "''") + "', " +
                                      "ISO9001 = '" + ddISO9001.SelectedValue + "', " +
                                      "ISO14001 = '" + ddISO14001.SelectedValue + "', " +
                                      "IATF16949 = '" + ddIATF16949.SelectedValue + "', " +
                                      "EI_FinancialAnalysis = '" + financialAnalysis + "', " +
                                      "EI_Quality = '" + quality_d + "', " +
                                      "EI_CostResponse = '" + costResponse + "', " +
                                      "EI_Delivery = '" + delivery + "', " +
                                      "EI_Cooperation = '" + cooperation + "', " +
                                      "EI_CSR = '" + csr + "', " +
                                      "EI_FinancialAnalysis_Value = '" + ddFinancialAnalysis.SelectedValue + "', " +
                                      "EI_Quality_Value  = '" + quality + "', " +
                                      "EI_CostResponse_Value  = '" + ddCostResponse.SelectedValue + "', " +
                                      "EI_Delivery_Value  = '" + ddDelivery.SelectedValue + "', " +
                                      "EI_Cooperation_Value  = '" + ddCooperation.SelectedValue + "', " +
                                      "EI_CSR_Value  = '" + ddCSR.SelectedValue + "', " +
                                      "EI_TotalScore = '" + totalScore + "', " +
                                      "JudgementByPerson = '" + judgementByPerson + "', " +
                                      "DivManExpand = '" + divManExpand + "', " +
                                      "DivManContinue = '" + divManContinue + "', " +
                                      "DivManReduce = '" + divManReduce + "', " +
                                      "DivManStop = '" + divManStop + "', " +
                                      "Reason = '" + txtReason.Text.Replace("'", "''") + "', " +
                                      "CircularComments = '" + txtCircularComments.Text.Replace("'", "''") + "', " +
                                      "DivManInstructions = '" + txtDivisionManagerInstructions.Text.Replace("'", "''") + "' WHERE RefId = '" + lblRefId.Text.Trim() + "' ";

                            queryCounter++;

                        } // End of if (!string.IsNullOrEmpty(lblTraderMaker.Text))


                    }

                    query_Success = BLL.SE_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                    if (query_Success == queryCounter.ToString())
                    {
                        Session["successMessage"] = "REQUEST FOR FISCAL YEAR : <b>" + ddFiscalYear.SelectedItem.Text + "</b> HAS BEEN SUCCESSFULLY UPDATED.";
                        Session["successTransactionName"] = "SE_SupplierEvaluation";
                        Session["successReturnPage"] = "SE_SupplierEvaluation.aspx";
                        Response.Redirect("SuccessPage.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvScore.Rows.Count > 0)
                {
                    string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                    string query1 = string.Empty;
                    string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                    string query_Success = string.Empty;
                    int queryCounter = 0;
                    double totalScore = 0;
                    LinkButton lbQuality = (LinkButton)gvData.Rows[int.Parse(lblRow.Text.Trim()) - 1].Cells[10].FindControl("lbQuality");


                    for (int i = 0; i < gvScore.Rows.Count; i++)
                    {
                        Label lblLevel = (Label)gvScore.Rows[i].Cells[0].FindControl("lblLevel");
                        Label lblWeight = (Label)gvScore.Rows[i].Cells[1].FindControl("lblWeight");
                        TextBox txtScore = (TextBox)gvScore.Rows[i].Cells[2].FindControl("txtScore");
                        Label lblTotalScore = (Label)gvScore.Rows[i].Cells[3].FindControl("lblTotalScore");

                        lblTotalScore.Text = (double.Parse(lblWeight.Text) * double.Parse(txtScore.Text.Trim())).ToString();

                        if (lbQuality.Text.ToUpper() == "SET" || lbQuality.Text.ToUpper() == "0")
                        {
                            query1 += "INSERT INTO SE_TRANSACTION_Score (DetailsRefId,Type,Score,TotalScore) VALUES ('" + lblDetailsRefId.Text.Trim() + "','" + lblLevel.Text.ToUpper() + "','" + txtScore.Text.Trim() + "','" + lblTotalScore.Text + "') ";
                        }
                        else
                        {
                            query1 += "UPDATE SE_TRANSACTION_Score SET Score = '" + txtScore.Text.Trim() + "', TotalScore = '" + lblTotalScore.Text + "' WHERE DetailsRefId ='" + lblDetailsRefId.Text.Trim() + "' AND Type ='" + lblLevel.Text.ToUpper() + "' ";
                        }

                        queryCounter++;

                        totalScore = totalScore + double.Parse(lblTotalScore.Text);
                    }

                    query_Success = BLL.SE_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                    if (query_Success == queryCounter.ToString())
                    {
                        lbQuality.Text = totalScore.ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowRegistered", "hideDialog();", true);
                    }
                    
                    
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnForApproval_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvData.Rows.Count > 0)
                {
                    string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                    string query1 = string.Empty;
                    string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                    string query_Success = string.Empty;

                    if (gvData.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvData.Rows.Count; i++)
                        {
                            Label lblFY_SupplierId = (Label)gvData.Rows[i].Cells[1].FindControl("lblFY_SupplierId");
                            ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[0].FindControl("ibApproved");
                            if (ibApproved.ImageUrl == "~/images/A2.png")
                            {
                                //query1 += "UPDATE SE_TRANSACTION_StatusEvaluationTable SET ForApproval = 1 WHERE SUBSTRING(FY_SupplierId,0,CHARINDEX('_',FY_SupplierId)) = '" + ddFiscalYear.SelectedValue + "' ";
                                query1 += "UPDATE SE_TRANSACTION_StatusEvaluationTable SET ForApproval = 1 WHERE FY_SupplierId = '" + lblFY_SupplierId.Text.Trim() + "' ";
                            }
                        }

                        if (!string.IsNullOrEmpty(query1))
                        {
                            query_Success = BLL.SE_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                            Session["successMessage"] = "FISCAL YEAR : <b>" + ddFiscalYear.SelectedItem.Text + "</b> HAS BEEN SENT TO SECTION INCHARGE FOR APPROVAL.";
                            Session["successTransactionName"] = "SE_SupplierEvaluation";
                            Session["successReturnPage"] = "SE_SupplierEvaluation.aspx";
                            Response.Redirect("SuccessPage.aspx");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select atlease 1 item to submit!');", true);
                        }
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
                    Label lblTraderMaker = (Label)e.Row.FindControl("lblTraderMaker");
                    DropDownList ddISO9001 = (DropDownList)e.Row.FindControl("ddISO9001");
                    DropDownList ddISO14001 = (DropDownList)e.Row.FindControl("ddISO14001");
                    DropDownList ddIATF16949 = (DropDownList)e.Row.FindControl("ddIATF16949");

                    DropDownList ddFinancialAnalysis = (DropDownList)e.Row.FindControl("ddFinancialAnalysis");
                    DropDownList ddQuality = (DropDownList)e.Row.FindControl("ddQuality");
                    DropDownList ddCostResponse = (DropDownList)e.Row.FindControl("ddCostResponse");
                    DropDownList ddDelivery = (DropDownList)e.Row.FindControl("ddDelivery");
                    DropDownList ddCooperation = (DropDownList)e.Row.FindControl("ddCooperation");
                    DropDownList ddCSR = (DropDownList)e.Row.FindControl("ddCSR");

                    Label lblFinancialAnalysis = (Label)e.Row.FindControl("lblFinancialAnalysis");
                    Label lblQuality = (Label)e.Row.FindControl("lblQuality");
                    Label lblCostResponse = (Label)e.Row.FindControl("lblCostResponse");
                    Label lblDelivery = (Label)e.Row.FindControl("lblDelivery");
                    Label lblCooperation = (Label)e.Row.FindControl("lblCooperation");
                    Label lblCSR = (Label)e.Row.FindControl("lblCSR");

                    Label lblFinancialAnalysisValue = (Label)e.Row.FindControl("lblFinancialAnalysisValue");
                    Label lblQualityValue = (Label)e.Row.FindControl("lblQualityValue");
                    Label lblCostResponseValue = (Label)e.Row.FindControl("lblCostResponseValue");
                    Label lblDeliveryValue = (Label)e.Row.FindControl("lblDeliveryValue");
                    Label lblCooperationValue = (Label)e.Row.FindControl("lblCooperationValue");
                    Label lblCSRValue = (Label)e.Row.FindControl("lblCSRValue");

                    Label lblTotalScore = (Label)e.Row.FindControl("lblTotalScore");
                    TextBox txtTotalScore = (TextBox)e.Row.FindControl("txtTotalScore");


                    Label lblItemClassification = (Label)e.Row.FindControl("lblItemClassification");
                    LinkButton lbQuality = (LinkButton)e.Row.FindControl("lbQuality");
                    Label lblRefId = (Label)e.Row.FindControl("lblRefId");
                    Label lblISO9001 = (Label)e.Row.FindControl("lblISO9001");
                    Label lblISO14001 = (Label)e.Row.FindControl("lblISO14001");
                    Label lblATF16949 = (Label)e.Row.FindControl("lblATF16949");
                    Label lblMakerName = (Label)e.Row.FindControl("lblMakerName");
                    TextBox txtMakerName = (TextBox)e.Row.FindControl("txtMakerName");

                    Label lblDivisionManagerEvaluationResult = (Label)e.Row.FindControl("lblDivisionManagerEvaluationResult");                    



                    if (!string.IsNullOrEmpty(lblTraderMaker.Text))
                    {
                        
                        // MAKER
                        if (lblTraderMaker.Text.ToUpper() == "MAKER")
                        {
                            List<Entities_SE_EvaluationCriteria_Maker> list = new List<Entities_SE_EvaluationCriteria_Maker>();
                            list = BLL.SE_MT_EvaluationCriteria_Maker_GetAll();

                            if (list != null)
                            {
                                if (list.Count > 0)
                                {
                                    ddFinancialAnalysis.Items.Clear();
                                    ddFinancialAnalysis.Items.Add("");
                                    
                                    //ddQuality.Items.Clear();
                                    //ddQuality.Items.Add("");

                                    ddCostResponse.Items.Clear();
                                    ddCostResponse.Items.Add("");

                                    ddDelivery.Items.Clear();
                                    ddDelivery.Items.Add("");

                                    ddCooperation.Items.Clear();
                                    ddCooperation.Items.Add("");

                                    ddCSR.Items.Clear();
                                    ddCSR.Items.Add("");

                                    foreach (Entities_SE_EvaluationCriteria_Maker entity in list)
                                    {
                                        ListItem item = new ListItem();
                                        item.Text = entity.Points + " - " + entity.Criteria;
                                        item.Value = entity.RefId;

                                        if (entity.Item == "1" && (entity.Isdisabled == string.Empty || entity.Isdisabled == "0"))
                                        {
                                            ddFinancialAnalysis.Items.Add(item);
                                        }
                                        if (entity.Item == "2" && (entity.Isdisabled == string.Empty || entity.Isdisabled == "0"))
                                        {
                                            ddQuality.Items.Add(item);
                                        }
                                        if (entity.Item == "3" && (entity.Isdisabled == string.Empty || entity.Isdisabled == "0"))
                                        {
                                            ddCostResponse.Items.Add(item);
                                        }
                                        if (entity.Item == "4" && (entity.Isdisabled == string.Empty || entity.Isdisabled == "0"))
                                        {
                                            ddDelivery.Items.Add(item);
                                        }
                                        if (entity.Item == "5" && (entity.Isdisabled == string.Empty || entity.Isdisabled == "0"))
                                        {
                                            ddCooperation.Items.Add(item);
                                        }
                                        if (entity.Item == "6" && (entity.Isdisabled == string.Empty || entity.Isdisabled == "0"))
                                        {
                                            ddCSR.Items.Add(item);
                                        }

                                    }
                                }
                            }

                        }

                        // TRADER
                        if (lblTraderMaker.Text.ToUpper() == "TRADER")
                        {
                            List<Entities_SE_EvaluationCriteria_Trader> list = new List<Entities_SE_EvaluationCriteria_Trader>();
                            list = BLL.SE_MT_EvaluationCriteria_Trader_GetAll();

                            if (list != null)
                            {
                                if (list.Count > 0)
                                {
                                    ddFinancialAnalysis.Items.Clear();
                                    ddFinancialAnalysis.Items.Add("");

                                    ddQuality.Items.Clear();
                                    ddQuality.Items.Add("");

                                    ddCostResponse.Items.Clear();
                                    ddCostResponse.Items.Add("");

                                    ddDelivery.Items.Clear();
                                    ddDelivery.Items.Add("");

                                    ddCooperation.Items.Clear();
                                    ddCooperation.Items.Add("");

                                    ddCSR.Items.Clear();
                                    ddCSR.Items.Add("");

                                    foreach (Entities_SE_EvaluationCriteria_Trader entity in list)
                                    {
                                        ListItem item = new ListItem();
                                        item.Text = entity.Points + " - " + entity.Criteria;
                                        item.Value = entity.RefId;

                                        if (entity.Item == "7" && (entity.Isdisabled == string.Empty || entity.Isdisabled == "0"))
                                        {
                                            ddFinancialAnalysis.Items.Add(item);
                                        }
                                        if (entity.Item == "8" && (entity.Isdisabled == string.Empty || entity.Isdisabled == "0"))
                                        {
                                            ddQuality.Items.Add(item);
                                        }
                                        if (entity.Item == "9" && (entity.Isdisabled == string.Empty || entity.Isdisabled == "0"))
                                        {
                                            ddCostResponse.Items.Add(item);
                                        }
                                        if (entity.Item == "10" && (entity.Isdisabled == string.Empty || entity.Isdisabled == "0"))
                                        {
                                            ddDelivery.Items.Add(item);
                                        }
                                        if (entity.Item == "11" && (entity.Isdisabled == string.Empty || entity.Isdisabled == "0"))
                                        {
                                            ddCooperation.Items.Add(item);
                                        }
                                        if (entity.Item == "12" && (entity.Isdisabled == string.Empty || entity.Isdisabled == "0"))
                                        {
                                            ddCSR.Items.Add(item);
                                        }

                                    }
                                }
                            }
                        }


                        if (lblItemClassification.Text.ToUpper() == "SUB-MATERIALS")
                        {
                            ddQuality.Visible = false;
                            lbQuality.Visible = true;

                            List<Entities_SE_RequestEntry> list_Score = new List<Entities_SE_RequestEntry>();
                            list_Score = BLL.SE_TRANSACTION_IsScoreExist_In_TransactionScore(lblRefId.Text.Trim());
                            double score = 0;

                            if (list_Score != null)
                            {
                                if (list_Score.Count > 0)
                                {
                                    foreach (Entities_SE_RequestEntry entity_score in list_Score)
                                    {
                                        score = score + double.Parse(entity_score.TotalScore);
                                    }

                                    lbQuality.Text = score.ToString();
                                }
                            }

                            if (string.IsNullOrEmpty(lbQuality.Text))
                            {
                                lbQuality.Text = "SET";
                            }

                        }
                        else
                        {
                            ddQuality.Visible = true;
                            lbQuality.Visible = false;

                            if (!string.IsNullOrEmpty(lblQualityValue.Text))
                            {
                                if (lblQualityValue.Text.Trim() != "0")
                                {
                                    ddQuality.Items.FindByValue(lblQualityValue.Text.Trim()).Selected = true;
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(lblTraderMaker.Text))
                        {
                            ddISO9001.Items.FindByValue(lblISO9001.Text.Trim()).Selected = true;
                            ddISO14001.Items.FindByValue(lblISO14001.Text.Trim()).Selected = true;
                            ddIATF16949.Items.FindByValue(lblATF16949.Text.Trim()).Selected = true;

                            if (lblFinancialAnalysisValue.Text.Trim() != "0")
                            {
                                ddFinancialAnalysis.Items.FindByValue(lblFinancialAnalysisValue.Text.Trim()).Selected = true;
                            }
                            if (lblCostResponseValue.Text.Trim() != "0")
                            {
                                ddCostResponse.Items.FindByValue(lblCostResponseValue.Text.Trim()).Selected = true;
                            }
                            if (lblDeliveryValue.Text.Trim() != "0")
                            {
                                ddDelivery.Items.FindByValue(lblDeliveryValue.Text.Trim()).Selected = true;
                            }
                            if (lblCooperationValue.Text.Trim() != "0")
                            {
                                ddCooperation.Items.FindByValue(lblCooperationValue.Text.Trim()).Selected = true;
                            }
                            if (lblCSRValue.Text.Trim() != "0")
                            {
                                ddCSR.Items.FindByValue(lblCSRValue.Text.Trim()).Selected = true;
                            }

                            txtMakerName.Text = lblMakerName.Text;
                            txtTotalScore.Text = lblTotalScore.Text;
                        }

                    } // End of if (!string.IsNullOrEmpty(lblTraderMaker.Text))


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
                Label lblRefId = row.FindControl("lblRefId") as Label;
                LinkButton lbQuality = row.FindControl("lbQuality") as LinkButton;
                Label lblSupplierName = row.FindControl("lblSupplierName") as Label;
                Label lblCounter = row.FindControl("lblCounter") as Label;
                ImageButton ibApproved = row.FindControl("ibApproved") as ImageButton;

                TextBox txtMakerName = row.FindControl("txtMakerName") as TextBox;
                DropDownList ddISO9001 = row.FindControl("ddISO9001") as DropDownList;
                DropDownList ddISO14001 = row.FindControl("ddISO14001") as DropDownList;
                DropDownList ddIATF16949 = row.FindControl("ddIATF16949") as DropDownList;

                Label lblFY_SupplierId = row.FindControl("lblFY_SupplierId") as Label;


                //if (e.CommandName == "lbFiscalYear_Command")
                //{
                //    Response.Redirect("SE_EvaluationEntry.aspx?fy_year=" + lblId.Text.Trim(), false);
                //}

                if (e.CommandName == "ibManual_Command")
                {
                    Response.Redirect("SE_ManualResponse.aspx?fy_supplierid=" + lblFY_SupplierId.Text.Trim() + "&supplier_name=" + lblSupplierName.Text + "&fiscal_year=" + ddFiscalYear.SelectedItem.Text, false);
                }

                if (e.CommandName == "A_Command")
                {
                    if (ibApproved.ImageUrl == "~/images/A2.png")
                    {
                        ibApproved.ImageUrl = "~/images/A1.png";
                        btnForApproval.Visible = false;
                    }
                    else
                    {
                        ibApproved.ImageUrl = "~/images/A2.png";

                        if (string.IsNullOrEmpty(txtMakerName.Text) || string.IsNullOrEmpty(ddISO9001.SelectedItem.Text) || string.IsNullOrEmpty(ddISO14001.SelectedItem.Text) || string.IsNullOrEmpty(ddIATF16949.SelectedItem.Text))
                        {
                            ibApproved.ImageUrl = "~/images/A1.png";
                            btnForApproval.Visible = false;
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please make sure that entry is updated before ticking check icon for approval!');", true);
                        }
                        else
                        {
                            btnForApproval.Visible = true;
                        }
                    }

                }

                if (e.CommandName == "EI_Quality_Command")
                {
                    lblSupplierName2.Text = lblSupplierName.Text.ToUpper();
                    lblRow.Text = lblCounter.Text.Trim();
                    lblDetailsRefId.Text = lblRefId.Text.Trim();
                    LoadQuality();
                    

                    List<Entities_SE_RequestEntry> list_Score = new List<Entities_SE_RequestEntry>();
                    list_Score = BLL.SE_TRANSACTION_IsScoreExist_In_TransactionScore(lblRefId.Text.Trim());

                    if (list_Score != null)
                    {
                        if (list_Score.Count > 0)
                        {
                            gvScore.DataSource = list_Score;
                            gvScore.DataBind();
                        }
                        else
                        {
                            LoadScore();
                        }
                    }
                    else
                    {
                        LoadScore();
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowRegistered", "showDialog();", true);
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('3 - " + ex.StackTrace.ToString() + "');", true);
            }
        }

        protected void gvQuality_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('3 - " + ex.StackTrace.ToString() + "');", true);
            }
        }


        private void LoadQuality()
        {
            try
            {
                List<Entities_SE_EvaluationCriteria_ForMaterial> list = new List<Entities_SE_EvaluationCriteria_ForMaterial>();
                list = BLL.SE_MT_EvaluationCriteria_ForMaterial_GetAll();

                if (list.Count > 0)
                {
                    gvQuality.DataSource = list.Where(item => item.Isdisabled.Contains("0")).ToList();
                    gvQuality.DataBind();                    
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        private void LoadScore()
        {
            try
            {
                List<Entities_SE_RequestEntry> list = new List<Entities_SE_RequestEntry>();
                list = BLL.SE_TRANSACTION_GetLevel_From_CriteriaForMaterial();

                if (list.Count > 0)
                {
                    gvScore.DataSource = list;
                    gvScore.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        //protected void txtScore_TextChanged(object sender, EventArgs e)
        //{
            
        //    GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        //    TextBox txtScore = (TextBox)row.FindControl("txtScore");
        //    Label lblWeight = (Label)row.FindControl("lblWeight");
        //    Label lblTotalScore = (Label)row.FindControl("lblTotalScore");

        //    if (!string.IsNullOrEmpty(txtScore.Text))
        //    {
        //        double score = Convert.ToDouble(txtScore.Text);
        //        double weight = Convert.ToDouble(lblWeight.Text);
        //        double total = score * weight;
        //        lblTotalScore.Text = total.ToString();
        //    }
        //}




    }
}
