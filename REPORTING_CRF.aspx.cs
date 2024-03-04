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
    public partial class REPORTING_CRF : System.Web.UI.Page
    {

        BLL_CRF BLL = new BLL_CRF();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    txtFrom.Text = DateTime.Today.AddDays(-365).ToString("MM/dd/yyyy");
                    txtTo.Text = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");

                    // call submit button to load initial record
                    //btnSubmit_Click(sender, e);
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
                List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();
                List<Entities_CRF_RequestEntry> listDivision = new List<Entities_CRF_RequestEntry>();
                List<Entities_CRF_RequestEntry> listAll = new List<Entities_CRF_RequestEntry>();
                Entities_CRF_RequestEntry status = new Entities_CRF_RequestEntry();

                status.CrFrom = txtFrom.Text.Trim();
                status.CrTo = txtTo.Text.Trim();


                if (ddItemStatus.SelectedItem.Text == "APPROVED/DISAPPROVED" || ddItemStatus.SelectedItem.Text == "ALL")
                {

                    gvDataDetails.Visible = false;
                    gvData.Visible = true;
                    gvDataDivision.Visible = true;
                    gvDataAll.Visible = true;

                    //DEPARTMENT
                    list = BLL.CRF_TRANSACTION_Reporting_ByDateRange(status);

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


                    //DIVISION
                    listDivision = BLL.CRF_TRANSACTION_Reporting_ByDateRange_ByDivision(status);

                    if (listDivision.Count > 0)
                    {
                        gvDataDivision.Visible = true;
                        gvDataDivision.DataSource = listDivision;
                        gvDataDivision.DataBind();
                    }
                    else
                    {
                        gvDataDivision.Visible = false;
                        gvDataDivision.EmptyDataText = "NO RECORD(S) FOUND!";
                    }

                    //ALL
                    listAll = BLL.CRF_TRANSACTION_Reporting_ByDateRange_ByAll(status);

                    if (listAll.Count > 0)
                    {
                        gvDataAll.Visible = true;
                        gvDataAll.DataSource = listAll;
                        gvDataAll.DataBind();
                    }
                    else
                    {
                        gvDataAll.Visible = false;
                        gvDataAll.EmptyDataText = "NO RECORD(S) FOUND!";
                    }

                }

                if (ddItemStatus.SelectedItem.Text == "PENDING")
                {
                    gvData.Visible = false;
                    gvDataDivision.Visible = false;
                    gvDataAll.Visible = false;
                    gvDataDetails.Visible = true;


                    list = BLL.CRF_TRANSACTION_Reporting_ByDateRange_Details(status).Where(itm => itm.StatAll == "PENDING APPROVAL").ToList();

                    if (list.Count > 0)
                    {
                        gvDataDetails.Visible = true;
                        gvDataDetails.DataSource = list;
                        gvDataDetails.DataBind();
                    }
                    else
                    {
                        gvDataDetails.Visible = false;
                        gvDataDetails.EmptyDataText = "NO RECORD(S) FOUND!";
                    }

                }



                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('RECORD SUCCESSFULLY LOADED!');", true);



            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {

            try
            {

                if (ddItemStatus.SelectedItem.Text == "APPROVED/DISAPPROVED" || ddItemStatus.SelectedItem.Text == "ALL")
                {

                    int id_counter_for_excel_CRF = 4;
                    string pathNew_CRF = Server.MapPath("~/Reporting/CRF.xlsx");
                    string pathNew_CRF_FileName = Session["LcRefId"].ToString() + "_CRF_" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Replace("/", "").Replace(":", "").Replace(" ", "").Trim() + ".xlsx";
                    string pathNew_CRF_Destination = System.IO.Path.Combine(Server.MapPath("~/Reporting/USER_REPORTS_DUMP/"), pathNew_CRF_FileName);

                    //System.IO.File.Create(pathNew_CRF_Destination);
                    System.IO.File.Copy(pathNew_CRF, pathNew_CRF_Destination, true);

                    Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew_CRF_Destination);
                    FileStream fsNew_CRF = new FileStream(pathNew_CRF_Destination, FileMode.Open);
                    using (SLDocument draftNew_CRF = new SLDocument(fsNew_CRF, "SUMMARY"))
                    {

                        // CLEAN UP FIRST THE EXCEL FILE
                        for (int i = 1; i <= 5000; i++)
                        {
                            draftNew_CRF.SetCellValue("A" + i.ToString(), string.Empty);
                            draftNew_CRF.SetCellValue("B" + i.ToString(), string.Empty);
                            draftNew_CRF.SetCellValue("C" + i.ToString(), string.Empty);
                            draftNew_CRF.SetCellValue("D" + i.ToString(), string.Empty);
                            draftNew_CRF.SetCellValue("E" + i.ToString(), string.Empty);
                            draftNew_CRF.SetCellValue("F" + i.ToString(), string.Empty);
                            draftNew_CRF.SetCellValue("G" + i.ToString(), string.Empty);
                            draftNew_CRF.SetCellValue("H" + i.ToString(), string.Empty);
                            draftNew_CRF.SetCellValue("I" + i.ToString(), string.Empty);
                        }

                        // DEPARTMENT
                        for (int i = 0; i < gvData.Rows.Count; i++)
                        {
                            Label lblCounter = (Label)gvData.Rows[i].Cells[0].FindControl("lblCounter");
                            Label lblDepartmentName = (Label)gvData.Rows[i].Cells[1].FindControl("lblDepartmentName");
                            Label lblNumberOfRequest = (Label)gvData.Rows[i].Cells[2].FindControl("lblNumberOfRequest");
                            Label lblBuyerApproved = (Label)gvData.Rows[i].Cells[3].FindControl("lblBuyerApproved");
                            Label lblBuyerDisapproved = (Label)gvData.Rows[i].Cells[4].FindControl("lblBuyerDisapproved");
                            Label lblSCManagerApproved = (Label)gvData.Rows[i].Cells[5].FindControl("lblSCManagerApproved");
                            Label lblSCManagerDisapproved = (Label)gvData.Rows[i].Cells[6].FindControl("lblSCManagerDisapproved");
                            Label lblPosted = (Label)gvData.Rows[i].Cells[7].FindControl("lblPosted");
                            Label lblPendingApproval = (Label)gvData.Rows[i].Cells[8].FindControl("lblPendingApproval");

                            draftNew_CRF.SetCellValue("A1", "COUNTS BY DEPARTMENT");
                            draftNew_CRF.SetCellValue("A3", "NO");
                            draftNew_CRF.SetCellValue("B3", "DEPARTMENT NAME");
                            draftNew_CRF.SetCellValue("C3", "NUMBER OF REQUEST");
                            draftNew_CRF.SetCellValue("D3", "BUYER-APPROVED");
                            draftNew_CRF.SetCellValue("E3", "BUYER-DISAPPROVED");
                            draftNew_CRF.SetCellValue("F3", "SC-MNGR-APPROVED");
                            draftNew_CRF.SetCellValue("G3", "SC-MNGR-DISAPPROVED");
                            draftNew_CRF.SetCellValue("H3", "POSTED/CLOSED");
                            draftNew_CRF.SetCellValue("I3", "PENDING APPROVAL");
                            draftNew_CRF.SetCellValue("A" + id_counter_for_excel_CRF.ToString(), lblCounter.Text.ToUpper());
                            draftNew_CRF.SetCellValue("B" + id_counter_for_excel_CRF.ToString(), lblDepartmentName.Text.ToUpper());
                            draftNew_CRF.SetCellValue("C" + id_counter_for_excel_CRF.ToString(), lblNumberOfRequest.Text.ToUpper());
                            draftNew_CRF.SetCellValue("D" + id_counter_for_excel_CRF.ToString(), lblBuyerApproved.Text.ToUpper());
                            draftNew_CRF.SetCellValue("E" + id_counter_for_excel_CRF.ToString(), lblBuyerDisapproved.Text.ToUpper());
                            draftNew_CRF.SetCellValue("F" + id_counter_for_excel_CRF.ToString(), lblSCManagerApproved.Text.ToUpper());
                            draftNew_CRF.SetCellValue("G" + id_counter_for_excel_CRF.ToString(), lblSCManagerDisapproved.Text.ToUpper());
                            draftNew_CRF.SetCellValue("H" + id_counter_for_excel_CRF.ToString(), lblPosted.Text.ToUpper());
                            draftNew_CRF.SetCellValue("I" + id_counter_for_excel_CRF.ToString(), lblPendingApproval.Text.ToUpper());

                            id_counter_for_excel_CRF++;
                        }

                        // DIVISION
                        id_counter_for_excel_CRF++;
                        id_counter_for_excel_CRF++;

                        draftNew_CRF.SetCellValue("A" + id_counter_for_excel_CRF.ToString(), "COUNTS BY DIVISION");

                        id_counter_for_excel_CRF++;
                        id_counter_for_excel_CRF++;

                        draftNew_CRF.SetCellValue("A" + id_counter_for_excel_CRF.ToString(), "COUNTS BY DEPARTMENT");
                        draftNew_CRF.SetCellValue("A" + id_counter_for_excel_CRF.ToString(), "NO");
                        draftNew_CRF.SetCellValue("B" + id_counter_for_excel_CRF.ToString(), "DEPARTMENT NAME");
                        draftNew_CRF.SetCellValue("C" + id_counter_for_excel_CRF.ToString(), "NUMBER OF REQUEST");
                        draftNew_CRF.SetCellValue("D" + id_counter_for_excel_CRF.ToString(), "BUYER-APPROVED");
                        draftNew_CRF.SetCellValue("E" + id_counter_for_excel_CRF.ToString(), "BUYER-DISAPPROVED");
                        draftNew_CRF.SetCellValue("F" + id_counter_for_excel_CRF.ToString(), "SC-MNGR-APPROVED");
                        draftNew_CRF.SetCellValue("G" + id_counter_for_excel_CRF.ToString(), "SC-MNGR-DISAPPROVED");
                        draftNew_CRF.SetCellValue("H" + id_counter_for_excel_CRF.ToString(), "POSTED/CLOSED");
                        draftNew_CRF.SetCellValue("I" + id_counter_for_excel_CRF.ToString(), "PENDING APPROVAL");

                        id_counter_for_excel_CRF++;

                        for (int i = 0; i < gvDataDivision.Rows.Count; i++)
                        {
                            Label lblCounter = (Label)gvDataDivision.Rows[i].Cells[0].FindControl("lblCounter");
                            Label lblDivisionName = (Label)gvDataDivision.Rows[i].Cells[1].FindControl("lblDivisionName");
                            Label lblNumberOfRequest = (Label)gvDataDivision.Rows[i].Cells[2].FindControl("lblNumberOfRequest");
                            Label lblBuyerApproved = (Label)gvDataDivision.Rows[i].Cells[3].FindControl("lblBuyerApproved");
                            Label lblBuyerDisapproved = (Label)gvDataDivision.Rows[i].Cells[4].FindControl("lblBuyerDisapproved");
                            Label lblSCManagerApproved = (Label)gvDataDivision.Rows[i].Cells[5].FindControl("lblSCManagerApproved");
                            Label lblSCManagerDisapproved = (Label)gvDataDivision.Rows[i].Cells[6].FindControl("lblSCManagerDisapproved");
                            Label lblPosted = (Label)gvDataDivision.Rows[i].Cells[7].FindControl("lblPosted");
                            Label lblPendingApproval = (Label)gvDataDivision.Rows[i].Cells[8].FindControl("lblPendingApproval");

                            draftNew_CRF.SetCellValue("A" + id_counter_for_excel_CRF.ToString(), lblCounter.Text.ToUpper());
                            draftNew_CRF.SetCellValue("B" + id_counter_for_excel_CRF.ToString(), lblDivisionName.Text.ToUpper());
                            draftNew_CRF.SetCellValue("C" + id_counter_for_excel_CRF.ToString(), lblNumberOfRequest.Text.ToUpper());
                            draftNew_CRF.SetCellValue("D" + id_counter_for_excel_CRF.ToString(), lblBuyerApproved.Text.ToUpper());
                            draftNew_CRF.SetCellValue("E" + id_counter_for_excel_CRF.ToString(), lblBuyerDisapproved.Text.ToUpper());
                            draftNew_CRF.SetCellValue("F" + id_counter_for_excel_CRF.ToString(), lblSCManagerApproved.Text.ToUpper());
                            draftNew_CRF.SetCellValue("G" + id_counter_for_excel_CRF.ToString(), lblSCManagerDisapproved.Text.ToUpper());
                            draftNew_CRF.SetCellValue("H" + id_counter_for_excel_CRF.ToString(), lblPosted.Text.ToUpper());
                            draftNew_CRF.SetCellValue("I" + id_counter_for_excel_CRF.ToString(), lblPendingApproval.Text.ToUpper());


                            id_counter_for_excel_CRF++;
                        }

                        // ALL
                        id_counter_for_excel_CRF++;
                        id_counter_for_excel_CRF++;

                        draftNew_CRF.SetCellValue("A" + id_counter_for_excel_CRF.ToString(), "TOTAL COUNTS");

                        id_counter_for_excel_CRF++;
                        id_counter_for_excel_CRF++;

                        draftNew_CRF.SetCellValue("A" + id_counter_for_excel_CRF.ToString(), "NO");
                        draftNew_CRF.SetCellValue("B" + id_counter_for_excel_CRF.ToString(), "NUMBER OF REQUEST");
                        draftNew_CRF.SetCellValue("C" + id_counter_for_excel_CRF.ToString(), "BUYER-APPROVED");
                        draftNew_CRF.SetCellValue("D" + id_counter_for_excel_CRF.ToString(), "BUYER-DISAPPROVED");
                        draftNew_CRF.SetCellValue("E" + id_counter_for_excel_CRF.ToString(), "SC-MNGR-APPROVED");
                        draftNew_CRF.SetCellValue("F" + id_counter_for_excel_CRF.ToString(), "SC-MNGR-DISAPPROVED");
                        draftNew_CRF.SetCellValue("G" + id_counter_for_excel_CRF.ToString(), "PENDING APPROVAL");
                        draftNew_CRF.SetCellValue("H" + id_counter_for_excel_CRF.ToString(), "TOTAL POSTED/CLOSED");
                        draftNew_CRF.SetCellValue("I" + id_counter_for_excel_CRF.ToString(), "TOTAL DISAPPROVED");

                        id_counter_for_excel_CRF++;

                        for (int i = 0; i < gvDataAll.Rows.Count; i++)
                        {
                            Label lblCounter = (Label)gvDataAll.Rows[i].Cells[0].FindControl("lblCounter");
                            Label lblNumberOfRequest = (Label)gvDataAll.Rows[i].Cells[1].FindControl("lblNumberOfRequest");
                            Label lblBuyerApproved = (Label)gvDataAll.Rows[i].Cells[2].FindControl("lblBuyerApproved");
                            Label lblBuyerDisapproved = (Label)gvDataAll.Rows[i].Cells[3].FindControl("lblBuyerDisapproved");
                            Label lblSCManagerApproved = (Label)gvDataAll.Rows[i].Cells[4].FindControl("lblSCManagerApproved");
                            Label lblSCManagerDisapproved = (Label)gvDataAll.Rows[i].Cells[5].FindControl("lblSCManagerDisapproved");
                            Label lblPosted = (Label)gvDataAll.Rows[i].Cells[6].FindControl("lblPosted");
                            Label lblPendingApproval = (Label)gvDataAll.Rows[i].Cells[7].FindControl("lblPendingApproval");
                            Label lblTotalPosted = (Label)gvDataAll.Rows[i].Cells[8].FindControl("lblTotalPosted");
                            Label lblTotalDisapproved = (Label)gvDataAll.Rows[i].Cells[8].FindControl("lblTotalDisapproved");

                            draftNew_CRF.SetCellValue("A" + id_counter_for_excel_CRF.ToString(), lblCounter.Text.ToUpper());
                            draftNew_CRF.SetCellValue("B" + id_counter_for_excel_CRF.ToString(), lblNumberOfRequest.Text.ToUpper());
                            draftNew_CRF.SetCellValue("C" + id_counter_for_excel_CRF.ToString(), lblBuyerApproved.Text.ToUpper());
                            draftNew_CRF.SetCellValue("D" + id_counter_for_excel_CRF.ToString(), lblBuyerDisapproved.Text.ToUpper());
                            draftNew_CRF.SetCellValue("E" + id_counter_for_excel_CRF.ToString(), lblSCManagerApproved.Text.ToUpper());
                            draftNew_CRF.SetCellValue("F" + id_counter_for_excel_CRF.ToString(), lblSCManagerDisapproved.Text.ToUpper());
                            draftNew_CRF.SetCellValue("G" + id_counter_for_excel_CRF.ToString(), lblPendingApproval.Text.ToUpper());
                            draftNew_CRF.SetCellValue("H" + id_counter_for_excel_CRF.ToString(), lblTotalPosted.Text.ToUpper());
                            draftNew_CRF.SetCellValue("I" + id_counter_for_excel_CRF.ToString(), lblTotalDisapproved.Text.ToUpper());


                            id_counter_for_excel_CRF++;
                        }


                        fsNew_CRF.Close();
                        draftNew_CRF.SaveAs(pathNew_CRF);
                    }


                    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    //DETAILS

                    List<Entities_CRF_RequestEntry> listDetails = new List<Entities_CRF_RequestEntry>();
                    Entities_CRF_RequestEntry status = new Entities_CRF_RequestEntry();

                    status.CrFrom = txtFrom.Text.Trim();
                    status.CrTo = txtTo.Text.Trim();

                    //DETAILS
                    listDetails = BLL.CRF_TRANSACTION_Reporting_ByDateRange_Details(status);

                    int id_counter_for_excel_CRF_Details = 2;
                    //string pathNew_CRF_Details = Server.MapPath("~/Reporting/CRF.xlsx");
                    Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew_CRF_Destination);
                    FileStream fsNew_CRF_Details = new FileStream(pathNew_CRF_Destination, FileMode.Open);
                    using (SLDocument draftNew_CRF_Details = new SLDocument(fsNew_CRF_Details, "DETAILS"))
                    {

                        // CLEAN UP FIRST THE EXCEL FILE
                        for (int i = 1; i <= 10000; i++)
                        {
                            draftNew_CRF_Details.SetCellValue("A" + i.ToString(), string.Empty);
                            draftNew_CRF_Details.SetCellValue("B" + i.ToString(), string.Empty);
                            draftNew_CRF_Details.SetCellValue("C" + i.ToString(), string.Empty);
                            draftNew_CRF_Details.SetCellValue("D" + i.ToString(), string.Empty);
                            draftNew_CRF_Details.SetCellValue("E" + i.ToString(), string.Empty);
                            draftNew_CRF_Details.SetCellValue("F" + i.ToString(), string.Empty);
                            draftNew_CRF_Details.SetCellValue("G" + i.ToString(), string.Empty);
                            draftNew_CRF_Details.SetCellValue("H" + i.ToString(), string.Empty);
                            draftNew_CRF_Details.SetCellValue("I" + i.ToString(), string.Empty);
                            draftNew_CRF_Details.SetCellValue("J" + i.ToString(), string.Empty);
                            draftNew_CRF_Details.SetCellValue("K" + i.ToString(), string.Empty);
                            draftNew_CRF_Details.SetCellValue("L" + i.ToString(), string.Empty);
                            draftNew_CRF_Details.SetCellValue("M" + i.ToString(), string.Empty);
                            draftNew_CRF_Details.SetCellValue("N" + i.ToString(), string.Empty);
                            draftNew_CRF_Details.SetCellValue("O" + i.ToString(), string.Empty);
                            draftNew_CRF_Details.SetCellValue("P" + i.ToString(), string.Empty);
                            draftNew_CRF_Details.SetCellValue("Q" + i.ToString(), string.Empty);
                            draftNew_CRF_Details.SetCellValue("R" + i.ToString(), string.Empty);
                            draftNew_CRF_Details.SetCellValue("S" + i.ToString(), string.Empty);
                            draftNew_CRF_Details.SetCellValue("T" + i.ToString(), string.Empty);

                        }

                        if (listDetails != null)
                        {
                            if (listDetails.Count > 0)
                            {
                                draftNew_CRF_Details.SetCellValue("A1", "CTRLNO");
                                draftNew_CRF_Details.SetCellValue("B1", "REQUESTER");
                                draftNew_CRF_Details.SetCellValue("C1", "CATEGORY");
                                draftNew_CRF_Details.SetCellValue("D1", "SUPPLIER");
                                draftNew_CRF_Details.SetCellValue("E1", "ATTENTION");
                                draftNew_CRF_Details.SetCellValue("F1", "PO DATE");
                                draftNew_CRF_Details.SetCellValue("G1", "PO. NO.");
                                draftNew_CRF_Details.SetCellValue("H1", "PR. NO.");
                                draftNew_CRF_Details.SetCellValue("I1", "DESCRIPTION");
                                draftNew_CRF_Details.SetCellValue("J1", "TYPE/DRAWING");
                                draftNew_CRF_Details.SetCellValue("K1", "ORDER QUANTITY");
                                draftNew_CRF_Details.SetCellValue("L1", "REASON");
                                draftNew_CRF_Details.SetCellValue("M1", "TRANSACTION DATE");
                                draftNew_CRF_Details.SetCellValue("N1", "DATE CONFIRMED");
                                draftNew_CRF_Details.SetCellValue("O1", "STATUS");


                                foreach (Entities_CRF_RequestEntry entity in listDetails)
                                {

                                    draftNew_CRF_Details.SetCellValue("A" + id_counter_for_excel_CRF_Details.ToString(), entity.CtrlNo);
                                    draftNew_CRF_Details.SetCellValue("B" + id_counter_for_excel_CRF_Details.ToString(), entity.Requester);
                                    draftNew_CRF_Details.SetCellValue("C" + id_counter_for_excel_CRF_Details.ToString(), entity.Category);
                                    draftNew_CRF_Details.SetCellValue("D" + id_counter_for_excel_CRF_Details.ToString(), entity.Supplier);
                                    draftNew_CRF_Details.SetCellValue("E" + id_counter_for_excel_CRF_Details.ToString(), entity.Attention);
                                    draftNew_CRF_Details.SetCellValue("F" + id_counter_for_excel_CRF_Details.ToString(), entity.PoDate);
                                    draftNew_CRF_Details.SetCellValue("G" + id_counter_for_excel_CRF_Details.ToString(), entity.PoNO);
                                    draftNew_CRF_Details.SetCellValue("H" + id_counter_for_excel_CRF_Details.ToString(), entity.PrNO);
                                    draftNew_CRF_Details.SetCellValue("I" + id_counter_for_excel_CRF_Details.ToString(), entity.Description);
                                    draftNew_CRF_Details.SetCellValue("J" + id_counter_for_excel_CRF_Details.ToString(), entity.TypeDrawingNo);
                                    draftNew_CRF_Details.SetCellValue("K" + id_counter_for_excel_CRF_Details.ToString(), entity.OrderQuantity);
                                    draftNew_CRF_Details.SetCellValue("L" + id_counter_for_excel_CRF_Details.ToString(), entity.Reason);
                                    draftNew_CRF_Details.SetCellValue("M" + id_counter_for_excel_CRF_Details.ToString(), entity.TransactionDate);
                                    draftNew_CRF_Details.SetCellValue("N" + id_counter_for_excel_CRF_Details.ToString(), entity.SupplierResponseDate);
                                    draftNew_CRF_Details.SetCellValue("O" + id_counter_for_excel_CRF_Details.ToString(), entity.StatAll);


                                    id_counter_for_excel_CRF_Details++;
                                }
                            }
                        }


                        fsNew_CRF_Details.Close();
                        draftNew_CRF_Details.SaveAs(pathNew_CRF_Destination);
                    }



                    Response.Redirect("Reporting/USER_REPORTS_DUMP/" + pathNew_CRF_FileName, false);

                }


                if (ddItemStatus.SelectedItem.Text == "PENDING")
                {

                    int id_counter_for_excel_CRF_PENDING = 4;
                    string pathNew_CRF_PENDING = Server.MapPath("~/Reporting/CRF_PENDING.xlsx");
                    string pathNew_CRF_PENDING_FileName = Session["LcRefId"].ToString() + "_CRF_PENDING_" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Replace("/", "").Replace(":", "").Replace(" ", "").Trim() + ".xlsx";
                    string pathNew_CRF_PENDING_Destination = System.IO.Path.Combine(Server.MapPath("~/Reporting/USER_REPORTS_DUMP/"), pathNew_CRF_PENDING_FileName);

                    System.IO.File.Copy(pathNew_CRF_PENDING, pathNew_CRF_PENDING_Destination, true);


                    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    //DETAILS

                    List<Entities_CRF_RequestEntry> listDetails = new List<Entities_CRF_RequestEntry>();
                    Entities_CRF_RequestEntry status = new Entities_CRF_RequestEntry();

                    status.CrFrom = txtFrom.Text.Trim();
                    status.CrTo = txtTo.Text.Trim();

                    //DETAILS
                    listDetails = BLL.CRF_TRANSACTION_Reporting_ByDateRange_Details(status).Where(itm => itm.StatAll == "PENDING APPROVAL").ToList();

                    int id_counter_for_excel_CRF_PENDING_Details = 2;
                    //string pathNew_CRF_PENDING_Details = Server.MapPath("~/Reporting/CRF.xlsx");
                    Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew_CRF_PENDING_Destination);
                    FileStream fsNew_CRF_PENDING_Details = new FileStream(pathNew_CRF_PENDING_Destination, FileMode.Open);
                    using (SLDocument draftNew_CRF_PENDING_Details = new SLDocument(fsNew_CRF_PENDING_Details, "DETAILS"))
                    {

                        // CLEAN UP FIRST THE EXCEL FILE
                        for (int i = 2; i <= 10000; i++)
                        {
                            draftNew_CRF_PENDING_Details.SetCellValue("A" + i.ToString(), string.Empty);
                            draftNew_CRF_PENDING_Details.SetCellValue("B" + i.ToString(), string.Empty);
                            draftNew_CRF_PENDING_Details.SetCellValue("C" + i.ToString(), string.Empty);
                            draftNew_CRF_PENDING_Details.SetCellValue("D" + i.ToString(), string.Empty);
                            draftNew_CRF_PENDING_Details.SetCellValue("E" + i.ToString(), string.Empty);
                            draftNew_CRF_PENDING_Details.SetCellValue("F" + i.ToString(), string.Empty);
                            draftNew_CRF_PENDING_Details.SetCellValue("G" + i.ToString(), string.Empty);
                            draftNew_CRF_PENDING_Details.SetCellValue("H" + i.ToString(), string.Empty);
                            draftNew_CRF_PENDING_Details.SetCellValue("I" + i.ToString(), string.Empty);
                            draftNew_CRF_PENDING_Details.SetCellValue("J" + i.ToString(), string.Empty);
                            draftNew_CRF_PENDING_Details.SetCellValue("K" + i.ToString(), string.Empty);
                            draftNew_CRF_PENDING_Details.SetCellValue("L" + i.ToString(), string.Empty);
                            draftNew_CRF_PENDING_Details.SetCellValue("M" + i.ToString(), string.Empty);
                            draftNew_CRF_PENDING_Details.SetCellValue("N" + i.ToString(), string.Empty);
                            draftNew_CRF_PENDING_Details.SetCellValue("O" + i.ToString(), string.Empty);

                        }

                        if (listDetails != null)
                        {
                            if (listDetails.Count > 0)
                            {
                                
                                foreach (Entities_CRF_RequestEntry entity in listDetails)
                                {

                                    draftNew_CRF_PENDING_Details.SetCellValue("A" + id_counter_for_excel_CRF_PENDING_Details.ToString(), entity.CtrlNo);
                                    draftNew_CRF_PENDING_Details.SetCellValue("B" + id_counter_for_excel_CRF_PENDING_Details.ToString(), entity.Requester);
                                    draftNew_CRF_PENDING_Details.SetCellValue("C" + id_counter_for_excel_CRF_PENDING_Details.ToString(), entity.Category);
                                    draftNew_CRF_PENDING_Details.SetCellValue("D" + id_counter_for_excel_CRF_PENDING_Details.ToString(), entity.Supplier);
                                    draftNew_CRF_PENDING_Details.SetCellValue("E" + id_counter_for_excel_CRF_PENDING_Details.ToString(), entity.Attention);
                                    draftNew_CRF_PENDING_Details.SetCellValue("F" + id_counter_for_excel_CRF_PENDING_Details.ToString(), entity.PoDate);
                                    draftNew_CRF_PENDING_Details.SetCellValue("G" + id_counter_for_excel_CRF_PENDING_Details.ToString(), entity.PoNO);
                                    draftNew_CRF_PENDING_Details.SetCellValue("H" + id_counter_for_excel_CRF_PENDING_Details.ToString(), entity.PrNO);
                                    draftNew_CRF_PENDING_Details.SetCellValue("I" + id_counter_for_excel_CRF_PENDING_Details.ToString(), entity.Description);
                                    draftNew_CRF_PENDING_Details.SetCellValue("J" + id_counter_for_excel_CRF_PENDING_Details.ToString(), entity.TypeDrawingNo);
                                    draftNew_CRF_PENDING_Details.SetCellValue("K" + id_counter_for_excel_CRF_PENDING_Details.ToString(), entity.OrderQuantity);
                                    draftNew_CRF_PENDING_Details.SetCellValue("L" + id_counter_for_excel_CRF_PENDING_Details.ToString(), entity.Reason);
                                    draftNew_CRF_PENDING_Details.SetCellValue("M" + id_counter_for_excel_CRF_PENDING_Details.ToString(), entity.TransactionDate);
                                    draftNew_CRF_PENDING_Details.SetCellValue("N" + id_counter_for_excel_CRF_PENDING_Details.ToString(), entity.SupplierResponseDate);
                                    draftNew_CRF_PENDING_Details.SetCellValue("O" + id_counter_for_excel_CRF_PENDING_Details.ToString(), entity.StatAll);


                                    id_counter_for_excel_CRF_PENDING_Details++;
                                }
                            }
                        }


                        fsNew_CRF_PENDING_Details.Close();
                        draftNew_CRF_PENDING_Details.SaveAs(pathNew_CRF_PENDING_Destination);
                    }



                    Response.Redirect("Reporting/USER_REPORTS_DUMP/" + pathNew_CRF_PENDING_FileName, false);

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

                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvDataDivision_OnRowDataBound(object sender, GridViewRowEventArgs e)
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
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }








    }
}
