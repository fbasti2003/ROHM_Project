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
using SpreadsheetLight;
using System.IO;

namespace REPI_PUR_SOFRA
{
    public partial class SRF_AllRequest : System.Web.UI.Page
    {
        BLL_SRF BLL = new BLL_SRF();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    txtFrom.Text = DateTime.Today.AddDays(-7).ToString("MM/dd/yyyy");
                    txtTo.Text = DateTime.Today.AddDays(7).ToString("MM/dd/yyyy");

                    SRF_TRANSACTION_RequestEntry_Fill_All_DropdownList();

                    // call submit button to load initial record
                    btnSubmit_Click(sender, e);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                }
            }
        }

        private void SRF_TRANSACTION_RequestEntry_Fill_All_DropdownList()
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
            list = BLL.SRF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(item => item.DropdownName).ToList();

            ListItem item2 = new ListItem();
            item2.Text = "ALL CATEGORIES";
            item2.Value = "0";
            ddCategory.Items.Add(item2);

            foreach (Entities_SRF_RequestEntry entity in list)
            {
                ListItem item = new ListItem();
                item.Text = entity.DropdownName;
                item.Value = entity.DropdownRefId;

                if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                {
                    if (entity.TableName == "MT_Category")
                    {
                        ddCategory.Items.Add(item);
                    }
                }
            }


        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
                Entities_SRF_RequestEntry status = new Entities_SRF_RequestEntry();

                status.DrFrom = txtFrom.Text.Trim();
                status.DrTo = txtTo.Text.Trim();
                status.Requester = Session["LcRefId"].ToString();
                status.CtrlNo = txtSearch.Text;
                status.Category = ddCategory.SelectedValue;

                var PIPL_Impex_Access = ConfigurationManager.AppSettings["PIPL_Temp_Sir_Renz"];
                status.IsImpex = (Session["Username"].ToString() == PIPL_Impex_Access.ToString() || Session["Username"].ToString() == "057") ? "1" : "0";
                status.SelectAll = ddCategory.SelectedItem.Text.ToLower() == "all categories" ? "1" : "0";

                list = null;

                if (txtSearch.Text.Length > 0)
                {
                    if (ddItemStatus.SelectedValue.ToLower() == "pending")
                    {
                        list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_All(status).Where(itm => (itm.PurInchargeStat != "2" || itm.PurInchargeStat == null || itm.PurInchargeStat == "0" || itm.PurInchargeStat == "1") && (itm.PurImpexStat != "2" && (itm.PurImpexStat == null || itm.PurImpexStat == "0")) && (itm.PurDeptManagerStat != "2" || itm.PurDeptManagerStat == null || itm.PurDeptManagerStat == "0" || itm.PurDeptManagerStat == "1")).ToList();
                        //list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like_All(status).Where(itm => itm.PurInchargeStat == null || itm.PurInchargeStat == "0" || itm.PurImpexStat == null || itm.PurImpexStat == "0" && (itm.PurInchargeStat != "2" || itm.PurImpexStat != "2")).ToList();
                        //list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like_All(status).Where(itm => itm.ReqInchargeStat == "0" || itm.ReqManagerStat == "0" || itm.PurInchargeStat == "0" || itm.PurImpexStat == "0").ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "disapproved")
                    {
                        //list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like_All(status).Where(itm => itm.ReqInchargeStat == "2" || itm.ReqManagerStat == "2" || itm.PurInchargeStat == "2" || itm.PurImpexStat == "2").ToList();
                        list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like_All(status).Where(itm => itm.PurInchargeStat == "2" || itm.PurImpexStat == "2").ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "approved")
                    {
                        //list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like_All(status).Where(itm => itm.PurImpexStat == "1").ToList();
                        list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like_All(status).Where(itm => itm.PurImpexStat == "1").ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "all")
                    {
                        //list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like_All(status);
                        list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like_All(status);
                    }
                }
                else
                {
                    if (ddItemStatus.SelectedValue.ToLower() == "pending")
                    {
                        list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_All(status).Where(itm => (itm.PurInchargeStat != "2" || itm.PurInchargeStat == null || itm.PurInchargeStat == "0" || itm.PurInchargeStat == "1") && (itm.PurImpexStat != "2" && (itm.PurImpexStat == null || itm.PurImpexStat == "0")) && (itm.PurDeptManagerStat != "2" || itm.PurDeptManagerStat == null || itm.PurDeptManagerStat == "0" || itm.PurDeptManagerStat == "1")).ToList();
                        //list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_All(status).Where(itm => itm.PurInchargeStat == null || itm.PurInchargeStat == "0" || itm.PurImpexStat == null || itm.PurImpexStat == "0" && (itm.PurInchargeStat != "2" || itm.PurImpexStat != "2")).ToList();
                        //list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_All(status).Where(itm => itm.ReqInchargeStat == "0" || itm.ReqManagerStat == "0" || itm.PurInchargeStat == "0" || itm.PurImpexStat == "0").ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "disapproved")
                    {
                        //list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_All(status).Where(itm => itm.ReqInchargeStat == "2" || itm.ReqManagerStat == "2" || itm.PurInchargeStat == "2" || itm.PurImpexStat == "2").ToList();
                        list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_All(status).Where(itm => itm.PurInchargeStat == "2" || itm.PurImpexStat == "2").ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "approved")
                    {
                        //list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_All(status).Where(itm => itm.PurImpexStat == "1").ToList();
                        list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_All(status).Where(itm => itm.PurImpexStat == "1").ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "all")
                    {
                        //list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_All(status);
                        list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_All(status);
                    }

                }

                if (list.Count > 0)
                {
                    gvData.Visible = true;
                    gvData.DataSource = list;
                    gvData.DataBind();
                }
                else
                {
                    gvData.Visible = false;
                    gvData.EmptyDataText = "NO RECORD(S) FOUND!";
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
                list = BLL.SRF_TRANSACTION_AllRequest_Reports_New(txtFrom.Text.Trim(), txtTo.Text.Trim());

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
                    Label lblReqIncharge = (Label)e.Row.FindControl("lblReqIncharge");
                    Label lblReqInchargeStat = (Label)e.Row.FindControl("lblReqInchargeStat");
                    Label lblReqManager = (Label)e.Row.FindControl("lblReqManager");
                    Label lblReqManagerStat = (Label)e.Row.FindControl("lblReqManagerStat");
                    Label lblPurIncharge = (Label)e.Row.FindControl("lblPurIncharge");
                    Label lblPurInchargeStat = (Label)e.Row.FindControl("lblPurInchargeStat");
                    Label lblImpex = (Label)e.Row.FindControl("lblImpex");
                    Label lblImpexStat = (Label)e.Row.FindControl("lblImpexStat");
                    Label lblSupplier = (Label)e.Row.FindControl("lblSupplier");
                    Label lblPurDeptManager = (Label)e.Row.FindControl("lblPurDeptManager");
                    Label lblPurDeptManagerStat = (Label)e.Row.FindControl("lblPurDeptManagerStat");


                    //-------------- REQ INCHARGE ----------------------------------
                    if (lblReqInchargeStat.Text == "0")
                    {
                        lblReqIncharge.Text = "PENDING";
                        lblReqIncharge.Style.Add("background-color", "#f44336");
                    }
                    if (lblReqInchargeStat.Text == "1")
                    {
                        lblReqIncharge.Text = "APPROVED";
                        lblReqIncharge.Style.Add("background-color", "#00C851");
                    }
                    if (lblReqInchargeStat.Text == "2")
                    {
                        lblReqIncharge.Text = "DISAPPROVED";
                        lblReqIncharge.Style.Add("background-color", "#ffbb33");
                    }
                    //---------------------------------------------------------


                    //-------------- REQ MANAGER -------------------------------
                    if (lblReqManagerStat.Text == "0")
                    {
                        lblReqManager.Text = "PENDING";
                        lblReqManager.Style.Add("background-color", "#f44336");
                    }
                    if (lblReqManagerStat.Text == "1")
                    {
                        lblReqManager.Text = "APPROVED";
                        lblReqManager.Style.Add("background-color", "#00C851");
                    }
                    if (lblReqManagerStat.Text == "2")
                    {
                        lblReqManager.Text = "DISAPPROVED";
                        lblReqManager.Style.Add("background-color", "#ffbb33");
                    }
                    //---------------------------------------------------------


                    //-------------- PUR INCHARGE ---------------------------------
                    if (lblPurInchargeStat.Text == "0")
                    {
                        lblPurIncharge.Text = "PENDING";
                        lblPurIncharge.Style.Add("background-color", "#f44336");
                    }
                    if (lblPurInchargeStat.Text == "1")
                    {
                        lblPurIncharge.Text = "APPROVED";
                        lblPurIncharge.Style.Add("background-color", "#00C851");
                    }
                    if (lblPurInchargeStat.Text == "2")
                    {
                        lblPurIncharge.Text = "DISAPPROVED";
                        lblPurIncharge.Style.Add("background-color", "#ffbb33");
                    }
                    //---------------------------------------------------------

                    //-------------- PUR DEPT. MANAGER ---------------------------------
                    if (lblPurDeptManagerStat.Text == "0")
                    {
                        lblPurDeptManager.Text = "PENDING";
                        lblPurDeptManager.Style.Add("background-color", "#f44336");
                    }
                    if (lblPurDeptManagerStat.Text == "1")
                    {
                        lblPurDeptManager.Text = "APPROVED";
                        lblPurDeptManager.Style.Add("background-color", "#00C851");
                    }
                    if (lblPurDeptManagerStat.Text == "2")
                    {
                        lblPurDeptManager.Text = "DISAPPROVED";
                        lblPurDeptManager.Style.Add("background-color", "#ffbb33");
                    }
                    //---------------------------------------------------------

                    //-------------- IMPEX ------------------------------------
                    if (lblImpexStat.Text == "0")
                    {
                        lblImpex.Text = "PENDING";
                        lblImpex.Style.Add("background-color", "#f44336");
                    }
                    if (lblImpexStat.Text == "1")
                    {
                        lblImpex.Text = "APPROVED";
                        lblImpex.Style.Add("background-color", "#00C851");
                    }
                    if (lblImpexStat.Text == "2")
                    {
                        lblImpex.Text = "DISAPPROVED";
                        lblImpex.Style.Add("background-color", "#ffbb33");
                    }
                    if (lblImpexStat.Text == "3")
                    {
                        lblImpex.Text = "CANCELED";
                        lblImpex.Style.Add("background-color", "BLUE");
                    }
                    //---------------------------------------------------------                    

                    lblSupplier.Text = lblSupplier.Text.Length >= 31 ? lblSupplier.Text.Substring(0, 31).ToString() + "..." : lblSupplier.Text;

                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;
            btnSubmit_Click(sender, e);
        }



    }
}
