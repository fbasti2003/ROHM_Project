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
//using REPI_PUR_SOFRA.App_Code.BLL;
//using REPI_PUR_SOFRA.App_Code.ENTITIES;
using System.Collections.Generic;
using System.IO;
using SpreadsheetLight;
namespace REPI_PUR_SOFRA
{
    public partial class SRF_AllRequestFreeForm : System.Web.UI.Page
    {

        BLL_SRF BLL = new BLL_SRF();
        Common COMMON = new Common();
       


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    // call submit button to load initial record
                    btnSubmit_Click(sender, e);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                }
            }
        }        

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string loopCtrlNo = string.Empty;

                List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
                Entities_SRF_RequestEntry status = new Entities_SRF_RequestEntry();

                status.SearchItem = txtSearch.Text;

                list = BLL.SRF_TRANSACTION_AllRequest_FreeForm(status);

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        gvData.Visible = true;
                        gvData.DataSource = list;
                        gvData.DataBind();

                        foreach (Entities_SRF_RequestEntry eCtrlNo in list)
                        {
                            loopCtrlNo += "'" + eCtrlNo.CtrlNo + "',";
                        }

                        if (!string.IsNullOrEmpty(loopCtrlNo))
                        {
                            Session["eCTRLNO"] = loopCtrlNo.Substring(0, loopCtrlNo.Length - 1).ToString();
                        }
                    }
                    else
                    {
                        gvData.Visible = false;
                        gvData.EmptyDataText = "NO RECORD(S) FOUND!";
                    }
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

                LinkButton lblCtrl = row.FindControl("lblCtrl") as LinkButton;
                LinkButton lbPrint = row.FindControl("lbPrint") as LinkButton;

                if (e.CommandName == "lblCtrl_Command")
                {
                    string URL = "~/SRF_RequestEntry.aspx?SRF_ControlNo_From_Details=" + CryptorEngine.Encrypt(lblCtrl.Text.Trim(), true);

                    URL = Page.ResolveClientUrl(URL);
                    lblCtrl.OnClientClick = "window.open('" + URL + "'); return false;";
                }

                if (e.CommandName == "lbPrint_Command")
                {
                    string URL = "~/SRF_RequestPrint.aspx?SRF_ControlNo_From_Details=" + CryptorEngine.Encrypt(lblCtrl.Text.Trim(), true);

                    URL = Page.ResolveClientUrl(URL);
                    lbPrint.OnClientClick = "window.open('" + URL + "'); return false;";
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

                    
                    if (lblStatus.Text == "FOR PROD.MNGR. APPROVAL")
                    {
                        lblStatus.Style.Add("background-color", "#f44336");
                    }

                    if (lblStatus.Text == "FOR PUR.INCHARGE APPROVAL")
                    {
                        lblStatus.Style.Add("background-color", "#9C27B0");
                    }

                    if (lblStatus.Text == "FOR PUR.IMPEX PROCESSING")
                    {
                        lblStatus.Style.Add("background-color", "#673AB7");
                    }

                    if (lblStatus.Text == "APPROVED")
                    {
                        lblStatus.Style.Add("background-color", "#00C851");
                    }

                    if (lblStatus.Text == "DISAPPROVED")
                    {
                        lblStatus.Style.Add("background-color", "#ffbb33");
                    }

                    if (lblStatus.Text == "CANCELED")
                    {
                        lblStatus.Style.Add("background-color", "blue");
                    }

                    if (lblStatus.Text == "CLOSED")
                    {
                        lblStatus.Style.Add("background-color", "#f44336");
                    }

                    if (lblStatus.Text == "DELIVERED WITH PENDING ITEMS")
                    {
                        lblStatus.Style.Add("background-color", "#5499C7");
                    }

                    if (lblStatus.Text.Contains("DELIVERY IN-PROGRESS"))
                    {
                        lblStatus.Style.Add("background-color", "#6F6B02");
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




        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!System.IO.File.Exists(Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx")))
                {
                    System.IO.File.Copy(Server.MapPath("~/SRF_XLS/SRF_AllRequest_Report_New.xlsx"), Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx"));
                }
                else
                {
                    System.IO.File.Delete(Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx"));
                    System.IO.File.Copy(Server.MapPath("~/SRF_XLS/SRF_AllRequest_Report_New.xlsx"), Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx"));
                }

                List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
                list = BLL.SRF_TRANSACTION_AllRequest_Reports_New2(Session["eCTRLNO"].ToString());

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        int cnt = 4;
                        string path = Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx");
                        Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + path);
                        FileStream fsBI = new FileStream(path, FileMode.Open);
                        using (SLDocument draft = new SLDocument(fsBI, "Sheet1"))
                        {

                            foreach (Entities_SRF_RequestEntry entity in list)
                            {
                                draft.SetCellValue("A" + cnt.ToString(), entity.CtrlNo);
                                draft.SetCellValue("B" + cnt.ToString(), entity.Requester);
                                draft.SetCellValue("C" + cnt.ToString(), entity.SupplierName);
                                draft.SetCellValue("D" + cnt.ToString(), entity.TransactionDate);
                                draft.SetCellValue("E" + cnt.ToString(), entity.ItemName);
                                draft.SetCellValue("F" + cnt.ToString(), entity.Specification);
                                draft.SetCellValue("G" + cnt.ToString(), entity.TotalQuantity);
                                draft.SetCellValue("H" + cnt.ToString(), entity.UnitOfMeasure);
                                draft.SetCellValue("I" + cnt.ToString(), entity.CategoryDescription);
                                draft.SetCellValue("J" + cnt.ToString(), entity.Report_BuyerName);

                                draft.SetCellValue("K" + cnt.ToString(), entity.Warehouse_TotalActualQuantity);
                                draft.SetCellValue("L" + cnt.ToString(), entity.Warehouse_RemainingQuantity);

                                //draft.SetCellValue("L" + cnt.ToString(), DateTime.Parse(entity.PullOutServiceDate.Replace("12:00:00 AM", string.Empty).Trim()).ToShortDateString());
                                //draft.SetCellValue("M" + cnt.ToString(), DateTime.Parse(entity.Warehouse_DeliveredDate.Replace("12:00:00 AM", string.Empty).Trim()).ToShortDateString());

                                //DateTime poDate = DateTime.Parse(entity.PullOutServiceDate.Replace("12:00:00 AM", string.Empty).Trim());
                                //SLStyle poDateStyle = draft.CreateStyle();
                                //poDateStyle.FormatCode = "yyyy-mm-dd";

                                //draft.SetCellStyle("L" + cnt.ToString(), poDateStyle);
                                //draft.SetCellValue("L" + cnt.ToString(), poDate.ToShortDateString());
                                draft.SetCellValue("M" + cnt.ToString(), entity.PullOutServiceDate.Replace("12:00:00 AM", string.Empty).Trim());
                                draft.SetCellValue("N" + cnt.ToString(), entity.Loa8106);
                                draft.SetCellValue("O" + cnt.ToString(), entity.Warehouse_DeliveredDate.Replace("12:00:00 AM", string.Empty).Trim());
                                draft.SetCellValue("P" + cnt.ToString(), entity.Warehouse_LOA8105ProcessDate.Replace(".", "/"));
                                draft.SetCellValue("Q" + cnt.ToString(), entity.Warehouse_8105);
                                draft.SetCellValue("R" + cnt.ToString(), entity.PurposeOfPullOut);
                                draft.SetCellValue("S" + cnt.ToString(), entity.StatAll);

                                cnt++;
                            }

                            fsBI.Close();
                            draft.SaveAs(path);

                        }

                        Response.Redirect("SRF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx", false);

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
