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
    public partial class SE_EvaluationMonitoring : System.Web.UI.Page
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
                    //if (list.Count > 0)
                    //{
                    //    ddFiscalYear.Items.Clear();
                    //    ddFiscalYear.Items.Add("");

                    //    foreach (Entities_SE_FiscalYear entity in list)
                    //    {
                    //        ListItem item = new ListItem();
                    //        item.Text = entity.Description;
                    //        item.Value = entity.RefId;

                    //        if (entity.Isdisabled == string.Empty || entity.Isdisabled == "0")
                    //        {
                    //            ddFiscalYear.Items.Add(item);
                    //        }

                    //    }
                    //}
                }

                
                List<Entities_SE_FiscalYear> list_Monitoring = new List<Entities_SE_FiscalYear>();
                //list_Monitoring = BLL.SE_TRANSACTION_Monitoring_GetAll().Where(itm => itm.Description == ddFiscalYear.SelectedItem.Text).ToList();
                list_Monitoring = BLL.SE_TRANSACTION_Monitoring_GetAll();

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


        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblFiscalYear = (Label)e.Row.FindControl("lblFiscalYear");
                    Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                    Button btnSendEvaluation = (Button)e.Row.FindControl("btnSendEvaluation");

                    if (lblStatus.Text.ToUpper() == "APPROVED")
                    {
                        btnSendEvaluation.Visible = true;
                    }
                    else
                    {
                        btnSendEvaluation.Visible = false;
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
                Label lblId = row.FindControl("lblId") as Label;
                Label lblFySupplierId = row.FindControl("lblFySupplierId") as Label;

                if (e.CommandName == "lbFiscalYear_Command")
                {
                    Response.Redirect("SE_EvaluationEntry.aspx?fy_year=" + lblId.Text.Trim() + "&send_evaluation=false", false);
                }

                if (e.CommandName == "btnSendEvaluation_Command")
                {
                    Response.Redirect("SE_EvaluationEntry.aspx?fy_year=" + lblId.Text.Trim() + "&send_evaluation=true", false);
                }

                if (e.CommandName == "btnViewReport_Command")
                {
                    Response.Redirect("SE_SupplierEvaluation_ReportDetails.aspx?fy_year=" + lblId.Text.Trim(), false);
                }

                if (e.CommandName == "ibReport_Command")
                {
                    if (System.IO.File.Exists(Server.MapPath("~/SE_Received/" + lblFySupplierId.Text.Trim() + "/SE_" + lblFySupplierId.Text.Trim() + "_Financial_Analysis.xlsx")))
                    {
                        Response.Redirect("SE_Received/" + lblFySupplierId.Text.Trim() + "/SE_" + lblFySupplierId.Text.Trim() + "_Financial_Analysis.xlsx", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Report is not available!');", true);
                    }
                }

                if (e.CommandName == "ibReport2_Command")
                {
                    if (System.IO.File.Exists(Server.MapPath("~/SE_Request/" + lblFySupplierId.Text.Trim() + "/SE_" + lblFySupplierId.Text.Trim() + "_SupplierEvaluation.xlsx")))
                    {
                        Response.Redirect("SE_Request/" + lblFySupplierId.Text.Trim() + "/SE_" + lblFySupplierId.Text.Trim() + "_SupplierEvaluation.xlsx", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Report is not available!');", true);
                    }
                }

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





    }
}
