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
    public partial class CRF_AllRequest_New : System.Web.UI.Page
    {
        BLL_CRF BLL = new BLL_CRF();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    txtFrom.Text = DateTime.Today.AddDays(-360).ToString("MM/dd/yyyy");
                    txtTo.Text = DateTime.Today.AddDays(7).ToString("MM/dd/yyyy");

                    if (Session["Search_From_CRF_Inquiry"] != null)
                    {
                        if (!string.IsNullOrEmpty(Session["Search_From_CRF_Inquiry"].ToString()))
                        {
                            txtFrom.Text = DateTime.Today.AddDays(-360).ToString("MM/dd/yyyy");
                            txtTo.Text = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");

                            txtSearch.Text = Session["Search_From_CRF_Inquiry"].ToString().TrimStart().TrimEnd();
                            Session["Search_From_CRF_Inquiry"] = null;
                        }
                    }

                    //---------------------------------------------------------------------------------------------------

                    List<Entities_CRF_RequestEntry> listDropDown = new List<Entities_CRF_RequestEntry>();
                    listDropDown = BLL.CRF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                    if (listDropDown != null)
                    {
                        if (listDropDown.Count > 0)
                        {
                            //ddReason.Items.Add("");
                            ddCategory.Items.Add("ALL");

                            foreach (Entities_CRF_RequestEntry entity in listDropDown)
                            {
                                ListItem item = new ListItem();
                                item.Text = entity.DropdownName.ToUpper();
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
                    }

                    //---------------------------------------------------------------------------------------------------

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
                bool supplyChain = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim());
                List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();
                Entities_CRF_RequestEntry status = new Entities_CRF_RequestEntry();

                status.CrFrom = txtFrom.Text.Trim();
                status.CrTo = txtTo.Text.Trim();

                //list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status);

                if (supplyChain)
                {

                    if (txtSearch.Text.Length > 0)
                    {
                        list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status).Where(itm => itm.SearchCriteria.Contains(txtSearch.Text.ToLower())).ToList();
                    }
                    else
                    {

                        if (ddItemStatus.SelectedValue.ToLower() == "all")
                        {
                            if (ddCategory.SelectedItem.Text == "ALL")
                            {
                                list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status);
                            }
                            else
                            {
                                list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status).Where(itm => itm.Category == ddCategory.SelectedItem.Text).ToList();
                            }
                        }
                        else
                        {
                            if (ddCategory.SelectedItem.Text == "ALL")
                            {
                                list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status).Where(itm => itm.StatAll == ddItemStatus.SelectedValue).ToList();
                            }
                            else
                            {
                                list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status).Where(itm => itm.StatAll == ddItemStatus.SelectedValue && itm.Category == ddCategory.SelectedItem.Text).ToList();
                            }
                        }

                    }
                }
                else
                {
                    if (txtSearch.Text.Length > 0)
                    {
                        list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status).Where(itm => itm.Division == Session["Division"].ToString() && itm.SearchCriteria.Contains(txtSearch.Text.ToLower())).ToList();
                    }
                    else
                    {
                        if (ddItemStatus.SelectedValue.ToLower() == "all")
                        {
                            if (ddCategory.SelectedItem.Text == "ALL")
                            {
                                list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status).Where(itm => itm.Division == Session["Division"].ToString()).ToList();
                            }
                            else
                            {
                                list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status).Where(itm => itm.Division == Session["Division"].ToString() && itm.Category == ddCategory.SelectedItem.Text).ToList();
                            }                            
                        }
                        else
                        {
                            if (ddCategory.SelectedItem.Text == "ALL")
                            {
                                list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status).Where(itm => itm.Division == Session["Division"].ToString() && itm.StatAll == ddItemStatus.SelectedItem.Text).ToList();
                            }
                            else
                            {
                                list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status).Where(itm => itm.Division == Session["Division"].ToString() && itm.StatAll == ddItemStatus.SelectedItem.Text && itm.Category == ddCategory.SelectedItem.Text).ToList();
                            }
                            
                        }
                        
                    }
                }

                if (list.Count > 0)
                {
                    gvData.Visible = true;
                    gvData.DataSource = list;
                    gvData.DataBind();


                    //EXPORT TO EXCEL
                    List<Entities_CRF_RequestEntry> finalListExport = new List<Entities_CRF_RequestEntry>();

                    foreach (Entities_CRF_RequestEntry entity in list)
                    {
                        List<Entities_CRF_RequestEntry> listExport = new List<Entities_CRF_RequestEntry>();
                        listExport = BLL.CRF_TRANSACTION_AllRequest_ByCTRLNo2(entity);

                        if (listExport != null)
                        {
                            if (listExport.Count > 0)
                            {
                                foreach (Entities_CRF_RequestEntry le in listExport)
                                {
                                    Entities_CRF_RequestEntry final = new Entities_CRF_RequestEntry();
                                    final.RdCtrlNo = le.RdCtrlNo;
                                    final.Attention = le.Attention;
                                    final.Supplier = le.Supplier;
                                    final.Requester = le.Requester;

                                    final.RdPRNO = le.RdPRNO;
                                    final.RdPONO = le.RdPONO;
                                    final.RdPODate = le.RdPODate;
                                    final.Category = le.Category;
                                    final.RdItemName = le.RdItemName;
                                    final.RdSpecs = le.RdSpecs;
                                    final.RdQuantity = le.RdQuantity;
                                    final.ResponseConfirmedBy = le.ResponseConfirmedBy;
                                    final.ResponseDateConfirmed = le.ResponseDateConfirmed;
                                    final.ResponseNotes = le.ResponseNotes;
                                    final.DateInformedSupplier = le.DateInformedSupplier;
                                    final.DateInformedRequestor = le.DateInformedRequestor;

                                    finalListExport.Add(final);
                                }
                            }
                        }
                    }

                    gvExport.DataSource = finalListExport;
                    gvExport.DataBind();
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


        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                    Label lblStatColor = (Label)e.Row.FindControl("lblStatColor");

                    lblStatus.Style.Add("background-color", lblStatColor.Text.Trim());

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

                LinkButton linkCtrlNo = row.FindControl("linkCtrlNo") as LinkButton;

                if (e.CommandName == "linkCtrlNo_Command")
                {
                    Response.Redirect("CRF_RequestEntry.aspx?CRFNo_From_Inquiry=" + CryptorEngine.Encrypt(linkCtrlNo.Text.Trim(), true), false);
                }

                if (e.CommandName == "linkPreview_Command")
                {

                    string pathLocation = Server.MapPath("~/CRF_Request/" + linkCtrlNo.Text.Trim() + "/REPORT_" + linkCtrlNo.Text.Trim() + ".html");
                    string htmlTemplate = Server.MapPath("~/HTML_Report/CRF/CRF.txt");
                    string htmlTemplate_Multiple = Server.MapPath("~/HTML_Report/CRF/CRF_Multiple.txt");
                    string templateValue = string.Empty;

                    if (System.IO.File.Exists(htmlTemplate))
                    {

                        //DETAILS
                        List<Entities_CRF_RequestEntry> listRequestDetails = new List<Entities_CRF_RequestEntry>();
                        Entities_CRF_RequestEntry entityRequestDetails = new Entities_CRF_RequestEntry();
                        entityRequestDetails.RdCtrlNo = linkCtrlNo.Text.Trim();

                        listRequestDetails = BLL.CRF_TRANSACTION_GetRequestDetailsByCTRLNo(entityRequestDetails);

                        if (listRequestDetails != null)
                        {
                            if (listRequestDetails.Count > 0)
                            {
                                if (listRequestDetails.Count.ToString() == "1")
                                {
                                    // SINGLE RECORD
                                    foreach (Entities_CRF_RequestEntry eRequest in listRequestDetails)
                                    {
                                        templateValue = System.IO.File.ReadAllText(htmlTemplate)
                                                           .Replace("val_ctrlno", eRequest.RdCtrlNo)
                                                           .Replace("val_attention", eRequest.Attention.ToUpper())
                                                           .Replace("val_division", eRequest.DivisionName)
                                                           .Replace("val_department", eRequest.DepartmentName)
                                                           .Replace("val_prno", eRequest.RdPRNO)
                                                           .Replace("val_pono", eRequest.RdPONO)
                                                           .Replace("val_description", eRequest.RdItemName)
                                                           .Replace("val_type", eRequest.RdSpecs)
                                                           .Replace("val_quantity", eRequest.RdQuantity)
                                                           .Replace("val_reason", eRequest.RdReasonName)
                                                           .Replace("val_preparedby", eRequest.RequesterS)
                                                           .Replace("val_notedby", eRequest.ReqManager)
                                                           .Replace("val_incharge", eRequest.PurIncharge)
                                                           .Replace("val_manager", eRequest.PurManager)
                                                           .Replace("val_doa_preparedby", eRequest.RequesterSDOA)
                                                           .Replace("val_doa_notedby", eRequest.ReqManagerDOA)
                                                           .Replace("val_doa_incharge", eRequest.PurInchargeDOA)
                                                           .Replace("val_doa_manager", eRequest.PurManagerDOA)
                                                           .Replace("val_confirmedby", eRequest.ResponseConfirmedBy)
                                                           .Replace("val_dateconfirmed", eRequest.ResponseDateConfirmed)
                                                           .Replace("val_notes", eRequest.ResponseNotes)
                                                           .Replace("bg_v_nb", "background-color:" + eRequest.ReqManagerStatColor + ";")
                                                           .Replace("bg_v_i", "background-color:" + eRequest.PurInchargeStatColor + ";")
                                                           .Replace("bg_v_m", "background-color:" + eRequest.PurManagerStatColor + ";")
                                                           .Replace("valreason_supplierchange", setReason(eRequest.RdReasonName, "valreason_supplierchange"))
                                                           .Replace("valreason_changepacking", setReason(eRequest.RdReasonName, "valreason_changepacking"))
                                                           .Replace("valreason_changespecification", setReason(eRequest.RdReasonName, "valreason_changespecification"))
                                                           .Replace("valreason_pricechange", setReason(eRequest.RdReasonName, "valreason_pricechange"))
                                                           .Replace("valreason_changemaker", setReason(eRequest.RdReasonName, "valreason_changemaker"))
                                                           .Replace("valreason_stopproduction", setReason(eRequest.RdReasonName, "valreason_stopproduction"))
                                                           .Replace("valreason_quantitychange", setReason(eRequest.RdReasonName, "valreason_quantitychange"))
                                                           .Replace("valreason_outofstock", setReason(eRequest.RdReasonName, "valreason_outofstock"))
                                                           .Replace("valreason_excessorder", setReason(eRequest.RdReasonName, "valreason_excessorder"))
                                                           .Replace("val_closed", eRequest.PurIncharge + "<br />" + eRequest.PostedDate)
                                                           .Replace("val_supplier", eRequest.SupplierName);

                                    }
                                }
                                else
                                {
                                    //MULTIPLE RECORDS
                                    string tableHeader = string.Empty;
                                    string tableDetails = string.Empty;
                                    string table = string.Empty;
                                    int counter = 0;

                                    tableHeader = "<tr><th>PONO</th><th>PRNO</th><th>DESCRIPTION</th><th>TYPE/DRAWING</th><th>QUANTITY</th><th>UNIT OF MEASURE</th><th>REASON</th><th>CONFIRMED BY</th><th>DATE CONFIRMED</th><th>NOTES</th></tr>";
                                    //LOOP & GET THE SINGLE RECORD
                                    foreach (Entities_CRF_RequestEntry entity in listRequestDetails)
                                    {
                                        // TABLE CREATION
                                        tableDetails += "<tr><td>" + entity.RdPONO + "</td><td>" + entity.RdPRNO + "</td><td>" + entity.RdItemName + "</td><td>" + entity.RdSpecs + "</td><td>" + entity.RdQuantity + "</td><td>" + entity.RdUOMDesc + "</td><td>" + entity.RdReasonName + "</td><td>" + entity.ResponseConfirmedBy + "</td><td>" + entity.ResponseDateConfirmed + "</td><td>" + entity.ResponseNotes + "</td></tr>";
                                    }

                                    table = "<table border='1' style='width: 100%; border-collapse: collapse; margin-left: auto; margin-right: auto; height: 94px; font-size:10px;'><tbody>" + tableHeader + tableDetails + "</tbody></table>";

                                    foreach (Entities_CRF_RequestEntry eRequest in listRequestDetails)
                                    {
                                        if (counter <= 0)
                                        {
                                            templateValue = System.IO.File.ReadAllText(htmlTemplate_Multiple)
                                                               .Replace("val_ctrlno", eRequest.RdCtrlNo)
                                                               .Replace("val_attention", eRequest.Attention.ToUpper())
                                                               .Replace("val_division", eRequest.DivisionName)
                                                               .Replace("val_department", eRequest.DepartmentName)
                                                               .Replace("val_prno", "PLS SEE ATTACHED")
                                                               .Replace("val_pono", "PLS SEE ATTACHED")
                                                               .Replace("val_description", "PLS SEE ATTACHED")
                                                               .Replace("val_type", "PLS SEE ATTACHED")
                                                               .Replace("val_quantity", "PLS SEE ATTACHED")
                                                               .Replace("val_reason", "PLS SEE ATTACHED")
                                                               .Replace("val_preparedby", eRequest.RequesterS)
                                                               .Replace("val_notedby", eRequest.ReqManager)
                                                               .Replace("val_incharge", eRequest.PurIncharge)
                                                               .Replace("val_manager", eRequest.PurManager)
                                                               .Replace("val_doa_preparedby", eRequest.RequesterSDOA)
                                                               .Replace("val_doa_notedby", eRequest.ReqManagerDOA)
                                                               .Replace("val_doa_incharge", eRequest.PurInchargeDOA)
                                                               .Replace("val_doa_manager", eRequest.PurManagerDOA)
                                                               .Replace("val_confirmedby", eRequest.ResponseConfirmedBy)
                                                               .Replace("val_dateconfirmed", eRequest.ResponseDateConfirmed)
                                                               .Replace("val_notes", eRequest.ResponseNotes)
                                                               .Replace("bg_v_nb", "background-color:" + eRequest.ReqManagerStatColor + ";")
                                                               .Replace("bg_v_i", "background-color:" + eRequest.PurInchargeStatColor + ";")
                                                               .Replace("bg_v_m", "background-color:" + eRequest.PurManagerStatColor + ";")
                                                               .Replace("valreason_supplierchange", setReason(eRequest.RdReason, "valreason_supplierchange"))
                                                               .Replace("valreason_changepacking", setReason(eRequest.RdReason, "valreason_changepacking"))
                                                               .Replace("valreason_changespecification", setReason(eRequest.RdReason, "valreason_changespecification"))
                                                               .Replace("valreason_pricechange", setReason(eRequest.RdReason, "valreason_pricechange"))
                                                               .Replace("valreason_changemaker", setReason(eRequest.RdReason, "valreason_changemaker"))
                                                               .Replace("valreason_stopproduction", setReason(eRequest.RdReason, "valreason_stopproduction"))
                                                               .Replace("valreason_quantitychange", setReason(eRequest.RdReason, "valreason_quantitychange"))
                                                               .Replace("valreason_outofstock", setReason(eRequest.RdReason, "valreason_outofstock"))
                                                               .Replace("valreason_excessorder", setReason(eRequest.RdReason, "valreason_excessorder"))
                                                               .Replace("val_details", table)
                                                               .Replace("val_closed", eRequest.PurIncharge + "<br />" + eRequest.PostedDate)
                                                               .Replace("val_supplier", eRequest.SupplierName);
                                            counter++;
                                        }

                                    }
                                }
                            }
                        }


                        if (!System.IO.File.Exists(pathLocation))
                        {
                            using (StreamWriter writer = new StreamWriter(new FileStream(pathLocation, FileMode.Create, FileAccess.Write)))
                            {
                                writer.WriteLine(templateValue);
                            }
                        }
                        else
                        {
                            System.IO.File.Delete(pathLocation);

                            if (!System.IO.File.Exists(pathLocation))
                            {
                                using (StreamWriter writer = new StreamWriter(new FileStream(pathLocation, FileMode.Create, FileAccess.Write)))
                                {
                                    writer.WriteLine(templateValue);
                                }
                            }
                        }
                    }

                    string URL = "CRF_Request/" + linkCtrlNo.Text.Trim() + "/REPORT_" + linkCtrlNo.Text.Trim() + ".html";

                    Response.Redirect(URL, false);
                    //URL = Page.ResolveClientUrl(URL);
                    //lbPreview.OnClientClick = "window.open('" + URL + "'); return false;";

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }



        private string setReason(string reason, string value)
        {
            string ret = "white;";

            if (reason == "SUPPLIER CHANGE" && value == "valreason_supplierchange")
            {
                ret = "black;";
            }

            else if (reason == "CHANGE PACKING" && value == "valreason_changepacking")
            {
                ret = "black;";
            }

            else if (reason == "CHANGE SPECIFICATION" && value == "valreason_changespecification")
            {
                ret = "black;";
            }

            else if (reason == "PRICE CHANGE" && value == "valreason_pricechange")
            {
                ret = "black;";
            }

            else if (reason == "CHANGE MAKER" && value == "valreason_changemaker")
            {
                ret = "black;";
            }

            else if (reason == "STOP PRODUCTION" && value == "valreason_stopproduction")
            {
                ret = "black;";
            }

            else if (reason == "QUANTITY CHANGE" && value == "valreason_quantitychange")
            {
                ret = "black;";
            }

            else if (reason == "OUT OF STOCK" && value == "valreason_outofstock")
            {
                ret = "black;";
            }

            else if (reason == "EXCESS ORDER" && value == "valreason_excessorder")
            {
                ret = "black;";
            }

            return ret;
        }


        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!System.IO.File.Exists(Server.MapPath("~/CRF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx")))
                {
                    System.IO.File.Copy(Server.MapPath("~/CRF_XLS/CRF_AllRequest_Report.xlsx"), Server.MapPath("~/CRF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx"));
                }
                else
                {
                    System.IO.File.Delete(Server.MapPath("~/CRF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx"));
                    System.IO.File.Copy(Server.MapPath("~/CRF_XLS/CRF_AllRequest_Report.xlsx"), Server.MapPath("~/CRF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx"));
                }

                List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();
                Entities_CRF_RequestEntry status = new Entities_CRF_RequestEntry();

                status.CrFrom = txtFrom.Text.Trim();
                status.CrTo = txtTo.Text.Trim();

                bool supplyChain = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim());

                if (supplyChain)
                {
                    if (ddItemStatus.SelectedItem.Text.ToUpper() == "ALL")
                    {
                        list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status);
                    }
                    else
                    {
                        list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status).Where(itm => itm.StatAll == ddItemStatus.SelectedItem.Text.ToUpper()).ToList();
                    }

                }
                else
                {
                    if (ddItemStatus.SelectedItem.Text.ToUpper() == "ALL")
                    {
                        list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status).Where(itm => itm.Division == Session["Division"].ToString()).ToList();
                    }
                    else
                    {
                        list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status).Where(itm => itm.Division == Session["Division"].ToString() && itm.StatAll == ddItemStatus.SelectedItem.Text.ToUpper()).ToList();
                    }
                }

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        int cnt = 4;
                        string path = Server.MapPath("~/CRF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx");
                        Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + path);
                        FileStream fsBI = new FileStream(path, FileMode.Open);
                        using (SLDocument draft = new SLDocument(fsBI, "Sheet1"))
                        {

                            foreach (Entities_CRF_RequestEntry entity in list)
                            {
                                draft.SetCellValue("A" + cnt.ToString(), entity.RdCtrlNo);
                                draft.SetCellValue("B" + cnt.ToString(), entity.Supplier);
                                draft.SetCellValue("C" + cnt.ToString(), entity.RdPODate);
                                draft.SetCellValue("D" + cnt.ToString(), entity.RdPRNO);
                                draft.SetCellValue("E" + cnt.ToString(), entity.RdPONO);
                                draft.SetCellValue("F" + cnt.ToString(), entity.RdItemName);
                                draft.SetCellValue("G" + cnt.ToString(), entity.RdSpecs);
                                draft.SetCellValue("H" + cnt.ToString(), entity.RdQuantity);
                                draft.SetCellValue("I" + cnt.ToString(), entity.RdUOMDesc);
                                draft.SetCellValue("J" + cnt.ToString(), entity.RdReasonName);
                                draft.SetCellValue("K" + cnt.ToString(), entity.Attention);
                                draft.SetCellValue("L" + cnt.ToString(), entity.Category);
                                draft.SetCellValue("M" + cnt.ToString(), entity.TransactionDate);
                                draft.SetCellValue("N" + cnt.ToString(), entity.StatAll);
                                draft.SetCellValue("O" + cnt.ToString(), entity.Report_BuyerName);
                                draft.SetCellValue("P" + cnt.ToString(), entity.PostedDate);

                                cnt++;
                            }

                            fsBI.Close();
                            draft.SaveAs(path);

                        }

                        Response.Redirect("CRF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx", false);

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
