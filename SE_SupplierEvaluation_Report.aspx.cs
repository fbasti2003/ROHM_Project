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
    public partial class SE_SupplierEvaluation_Report : System.Web.UI.Page
    {

        BLL_SE BLL = new BLL_SE();
        Common COMMON = new Common();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDefault();
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


                List<Entities_SE_FiscalYear> list_Monitoring = new List<Entities_SE_FiscalYear>();
                list_Monitoring = BLL.SE_TRANSACTION_Monitoring_GetAll2().Where(itm => itm.RefId == ddFiscalYear.SelectedValue).ToList();

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
                        txtChartScore.BackColor = System.Drawing.Color.Green;
                    }
                    if (lblJudgementByPerson.Text == "CONTINUE")
                    {
                        txtChartScore.BackColor = System.Drawing.Color.Purple;
                    }
                    if (lblJudgementByPerson.Text == "REDUCE")
                    {
                        txtChartScore.BackColor = System.Drawing.Color.Brown;
                    }
                    if (lblJudgementByPerson.Text == "STOP")
                    {
                        txtChartScore.BackColor = System.Drawing.Color.Red;
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
                    list_SupplierEvaluation = BLL.SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear2(ddFiscalYear.SelectedValue);

                    if (list_SupplierEvaluation != null)
                    {
                        if (list_SupplierEvaluation.Count > 0)
                        {
                            gvScores.DataSource = list_SupplierEvaluation;
                            gvScores.DataBind();

                            gvRanking.DataSource = list_SupplierEvaluation.OrderByDescending(itm => itm.EI_TotalScore).ToList();
                            gvRanking.DataBind();

                            foreach (Entities_SE_RequestEntry e_Chart in list_SupplierEvaluation)
                            {
                                Entities_SE_RequestEntry entityChart = new Entities_SE_RequestEntry();
                                entityChart.SupplierName = e_Chart.SupplierName.ToUpper() + "&nbsp;&nbsp;&nbsp;";
                                entityChart.EI_TotalScore = "&nbsp;&nbsp;" + e_Chart.EI_TotalScore;
                                entityChart.CustomerChartWidth = e_Chart.CustomerChartWidth;
                                entityChart.JudgementByPerson = e_Chart.JudgementByPerson;

                                list_ChartScores.Add(entityChart);
                            }

                            if (list_ChartScores != null)
                            {
                                if (list_ChartScores.Count > 0)
                                {
                                    gvChart.DataSource = list_ChartScores.OrderByDescending(itm => itm.EI_TotalScore).ToList();
                                    gvChart.DataBind();
                                }
                            }

                        }
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
                    List<Entities_SE_FiscalYear> list_Monitoring = new List<Entities_SE_FiscalYear>();
                    list_Monitoring = BLL.SE_TRANSACTION_Monitoring_GetAll2().Where(itm => itm.RefId == ddFiscalYear.SelectedValue).ToList();

                    if (list_Monitoring != null)
                    {
                        if (list_Monitoring.Count > 0)
                        {
                            gvData.Visible = true;
                            gvData.DataSource = list_Monitoring;
                            gvData.DataBind();
                        }
                        else
                        {
                            gvData.Visible = false;
                        }
                    }
                    else
                    {
                        gvData.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }








    }
}
