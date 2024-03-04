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
    public partial class REPORTING_URF : System.Web.UI.Page
    {
        BLL_URF BLL = new BLL_URF();
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
                List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();
                List<Entities_URF_RequestEntry> listDivision = new List<Entities_URF_RequestEntry>();
                List<Entities_URF_RequestEntry> listAll = new List<Entities_URF_RequestEntry>();
                Entities_URF_RequestEntry status = new Entities_URF_RequestEntry();

                status.DrFrom = txtFrom.Text.Trim();
                status.DrTo = txtTo.Text.Trim();


                if (ddItemStatus.SelectedItem.Text == "APPROVED/DISAPPROVED" || ddItemStatus.SelectedItem.Text == "ALL")
                {
                    gvDataDetails.Visible = false;
                    gvData.Visible = true;
                    gvDataDivision.Visible = true;
                    gvDataAll.Visible = true;

                    //DEPARTMENT
                    list = BLL.URF_TRANSACTION_Reporting_ByDateRange(status);

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
                    listDivision = BLL.URF_TRANSACTION_Reporting_ByDateRange_ByDivision(status);

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
                    listAll = BLL.URF_TRANSACTION_Reporting_ByDateRange_ByAll(status);

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


                    list = BLL.URF_TRANSACTION_Reporting_ByDateRange_Details(status).Where(itm => itm.StatAll == "PENDING APPROVAL").ToList();

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

                    int id_counter_for_excel_URF = 4;
                    string pathNew_URF = Server.MapPath("~/Reporting/URF.xlsx");
                    string pathNew_URF_FileName = Session["LcRefId"].ToString() + "_URF_" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Replace("/", "").Replace(":", "").Replace(" ", "").Trim() + ".xlsx";
                    string pathNew_URF_Destination = System.IO.Path.Combine(Server.MapPath("~/Reporting/USER_REPORTS_DUMP/"), pathNew_URF_FileName);

                    //System.IO.File.Create(pathNew_URF_Destination);
                    System.IO.File.Copy(pathNew_URF, pathNew_URF_Destination, true);

                    Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew_URF_Destination);
                    FileStream fsNew_URF = new FileStream(pathNew_URF_Destination, FileMode.Open);
                    using (SLDocument draftNew_URF = new SLDocument(fsNew_URF, "SUMMARY"))
                    {

                        // CLEAN UP FIRST THE EXCEL FILE
                        for (int i = 1; i <= 5000; i++)
                        {
                            draftNew_URF.SetCellValue("A" + i.ToString(), string.Empty);
                            draftNew_URF.SetCellValue("B" + i.ToString(), string.Empty);
                            draftNew_URF.SetCellValue("C" + i.ToString(), string.Empty);
                            draftNew_URF.SetCellValue("D" + i.ToString(), string.Empty);
                            draftNew_URF.SetCellValue("E" + i.ToString(), string.Empty);
                            draftNew_URF.SetCellValue("F" + i.ToString(), string.Empty);
                            draftNew_URF.SetCellValue("G" + i.ToString(), string.Empty);
                            draftNew_URF.SetCellValue("H" + i.ToString(), string.Empty);
                            draftNew_URF.SetCellValue("I" + i.ToString(), string.Empty);
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

                            draftNew_URF.SetCellValue("A1", "COUNTS BY DEPARTMENT");
                            draftNew_URF.SetCellValue("A3", "NO");
                            draftNew_URF.SetCellValue("B3", "DEPARTMENT NAME");
                            draftNew_URF.SetCellValue("C3", "NUMBER OF REQUEST");
                            draftNew_URF.SetCellValue("D3", "BUYER-APPROVED");
                            draftNew_URF.SetCellValue("E3", "BUYER-DISAPPROVED");
                            draftNew_URF.SetCellValue("F3", "SC-MNGR-APPROVED");
                            draftNew_URF.SetCellValue("G3", "SC-MNGR-DISAPPROVED");
                            draftNew_URF.SetCellValue("H3", "POSTED/CLOSED");
                            draftNew_URF.SetCellValue("I3", "PENDING APPROVAL");
                            draftNew_URF.SetCellValue("A" + id_counter_for_excel_URF.ToString(), lblCounter.Text.ToUpper());
                            draftNew_URF.SetCellValue("B" + id_counter_for_excel_URF.ToString(), lblDepartmentName.Text.ToUpper());
                            draftNew_URF.SetCellValue("C" + id_counter_for_excel_URF.ToString(), lblNumberOfRequest.Text.ToUpper());
                            draftNew_URF.SetCellValue("D" + id_counter_for_excel_URF.ToString(), lblBuyerApproved.Text.ToUpper());
                            draftNew_URF.SetCellValue("E" + id_counter_for_excel_URF.ToString(), lblBuyerDisapproved.Text.ToUpper());
                            draftNew_URF.SetCellValue("F" + id_counter_for_excel_URF.ToString(), lblSCManagerApproved.Text.ToUpper());
                            draftNew_URF.SetCellValue("G" + id_counter_for_excel_URF.ToString(), lblSCManagerDisapproved.Text.ToUpper());
                            draftNew_URF.SetCellValue("H" + id_counter_for_excel_URF.ToString(), lblPosted.Text.ToUpper());
                            draftNew_URF.SetCellValue("I" + id_counter_for_excel_URF.ToString(), lblPendingApproval.Text.ToUpper());

                            id_counter_for_excel_URF++;
                        }

                        // DIVISION
                        id_counter_for_excel_URF++;
                        id_counter_for_excel_URF++;

                        draftNew_URF.SetCellValue("A" + id_counter_for_excel_URF.ToString(), "COUNTS BY DIVISION");

                        id_counter_for_excel_URF++;
                        id_counter_for_excel_URF++;

                        draftNew_URF.SetCellValue("A" + id_counter_for_excel_URF.ToString(), "COUNTS BY DEPARTMENT");
                        draftNew_URF.SetCellValue("A" + id_counter_for_excel_URF.ToString(), "NO");
                        draftNew_URF.SetCellValue("B" + id_counter_for_excel_URF.ToString(), "DEPARTMENT NAME");
                        draftNew_URF.SetCellValue("C" + id_counter_for_excel_URF.ToString(), "NUMBER OF REQUEST");
                        draftNew_URF.SetCellValue("D" + id_counter_for_excel_URF.ToString(), "BUYER-APPROVED");
                        draftNew_URF.SetCellValue("E" + id_counter_for_excel_URF.ToString(), "BUYER-DISAPPROVED");
                        draftNew_URF.SetCellValue("F" + id_counter_for_excel_URF.ToString(), "SC-MNGR-APPROVED");
                        draftNew_URF.SetCellValue("G" + id_counter_for_excel_URF.ToString(), "SC-MNGR-DISAPPROVED");
                        draftNew_URF.SetCellValue("H" + id_counter_for_excel_URF.ToString(), "POSTED/CLOSED");
                        draftNew_URF.SetCellValue("I" + id_counter_for_excel_URF.ToString(), "PENDING APPROVAL");

                        id_counter_for_excel_URF++;

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

                            draftNew_URF.SetCellValue("A" + id_counter_for_excel_URF.ToString(), lblCounter.Text.ToUpper());
                            draftNew_URF.SetCellValue("B" + id_counter_for_excel_URF.ToString(), lblDivisionName.Text.ToUpper());
                            draftNew_URF.SetCellValue("C" + id_counter_for_excel_URF.ToString(), lblNumberOfRequest.Text.ToUpper());
                            draftNew_URF.SetCellValue("D" + id_counter_for_excel_URF.ToString(), lblBuyerApproved.Text.ToUpper());
                            draftNew_URF.SetCellValue("E" + id_counter_for_excel_URF.ToString(), lblBuyerDisapproved.Text.ToUpper());
                            draftNew_URF.SetCellValue("F" + id_counter_for_excel_URF.ToString(), lblSCManagerApproved.Text.ToUpper());
                            draftNew_URF.SetCellValue("G" + id_counter_for_excel_URF.ToString(), lblSCManagerDisapproved.Text.ToUpper());
                            draftNew_URF.SetCellValue("H" + id_counter_for_excel_URF.ToString(), lblPosted.Text.ToUpper());
                            draftNew_URF.SetCellValue("I" + id_counter_for_excel_URF.ToString(), lblPendingApproval.Text.ToUpper());


                            id_counter_for_excel_URF++;
                        }

                        // ALL
                        id_counter_for_excel_URF++;
                        id_counter_for_excel_URF++;

                        draftNew_URF.SetCellValue("A" + id_counter_for_excel_URF.ToString(), "TOTAL COUNTS");

                        id_counter_for_excel_URF++;
                        id_counter_for_excel_URF++;

                        draftNew_URF.SetCellValue("A" + id_counter_for_excel_URF.ToString(), "NO");
                        draftNew_URF.SetCellValue("B" + id_counter_for_excel_URF.ToString(), "NUMBER OF REQUEST");
                        draftNew_URF.SetCellValue("C" + id_counter_for_excel_URF.ToString(), "BUYER-APPROVED");
                        draftNew_URF.SetCellValue("D" + id_counter_for_excel_URF.ToString(), "BUYER-DISAPPROVED");
                        draftNew_URF.SetCellValue("E" + id_counter_for_excel_URF.ToString(), "SC-MNGR-APPROVED");
                        draftNew_URF.SetCellValue("F" + id_counter_for_excel_URF.ToString(), "SC-MNGR-DISAPPROVED");
                        draftNew_URF.SetCellValue("G" + id_counter_for_excel_URF.ToString(), "PENDING APPROVAL");
                        draftNew_URF.SetCellValue("H" + id_counter_for_excel_URF.ToString(), "TOTAL POSTED/CLOSED");
                        draftNew_URF.SetCellValue("I" + id_counter_for_excel_URF.ToString(), "TOTAL DISAPPROVED");

                        id_counter_for_excel_URF++;

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

                            draftNew_URF.SetCellValue("A" + id_counter_for_excel_URF.ToString(), lblCounter.Text.ToUpper());
                            draftNew_URF.SetCellValue("B" + id_counter_for_excel_URF.ToString(), lblNumberOfRequest.Text.ToUpper());
                            draftNew_URF.SetCellValue("C" + id_counter_for_excel_URF.ToString(), lblBuyerApproved.Text.ToUpper());
                            draftNew_URF.SetCellValue("D" + id_counter_for_excel_URF.ToString(), lblBuyerDisapproved.Text.ToUpper());
                            draftNew_URF.SetCellValue("E" + id_counter_for_excel_URF.ToString(), lblSCManagerApproved.Text.ToUpper());
                            draftNew_URF.SetCellValue("F" + id_counter_for_excel_URF.ToString(), lblSCManagerDisapproved.Text.ToUpper());
                            draftNew_URF.SetCellValue("G" + id_counter_for_excel_URF.ToString(), lblPendingApproval.Text.ToUpper());
                            draftNew_URF.SetCellValue("H" + id_counter_for_excel_URF.ToString(), lblTotalPosted.Text.ToUpper());
                            draftNew_URF.SetCellValue("I" + id_counter_for_excel_URF.ToString(), lblTotalDisapproved.Text.ToUpper());


                            id_counter_for_excel_URF++;
                        }


                        fsNew_URF.Close();
                        draftNew_URF.SaveAs(pathNew_URF);
                    }


                    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    //DETAILS

                    List<Entities_URF_RequestEntry> listDetails = new List<Entities_URF_RequestEntry>();
                    Entities_URF_RequestEntry status = new Entities_URF_RequestEntry();

                    status.DrFrom = txtFrom.Text.Trim();
                    status.DrTo = txtTo.Text.Trim();

                    //DETAILS
                    listDetails = BLL.URF_TRANSACTION_Reporting_ByDateRange_Details(status);

                    int id_counter_for_excel_URF_Details = 2;
                    string pathNew_URF_Details = Server.MapPath("~/Reporting/URF.xlsx");
                    Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew_URF_Destination);
                    FileStream fsNew_URF_Details = new FileStream(pathNew_URF_Details, FileMode.Open);
                    using (SLDocument draftNew_URF_Details = new SLDocument(pathNew_URF_Destination, "DETAILS"))
                    {

                        // CLEAN UP FIRST THE EXCEL FILE
                        for (int i = 1; i <= 10000; i++)
                        {
                            draftNew_URF_Details.SetCellValue("A" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("B" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("C" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("D" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("E" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("F" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("G" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("H" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("I" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("J" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("K" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("L" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("M" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("N" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("O" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("P" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("Q" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("R" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("S" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("T" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("U" + i.ToString(), string.Empty);
                            draftNew_URF_Details.SetCellValue("V" + i.ToString(), string.Empty);

                        }

                        if (listDetails != null)
                        {
                            if (listDetails.Count > 0)
                            {
                                draftNew_URF_Details.SetCellValue("A1", "CTRLNO");
                                draftNew_URF_Details.SetCellValue("B1", "REQUESTER");
                                draftNew_URF_Details.SetCellValue("C1", "CATEGORY");
                                draftNew_URF_Details.SetCellValue("D1", "TYPE");
                                draftNew_URF_Details.SetCellValue("E1", "SUPPLIER NAME");
                                draftNew_URF_Details.SetCellValue("F1", "ATTENTION");
                                draftNew_URF_Details.SetCellValue("G1", "REASON");
                                draftNew_URF_Details.SetCellValue("H1", "OTHER REASON");
                                draftNew_URF_Details.SetCellValue("I1", "TRANSACTION DATE");
                                draftNew_URF_Details.SetCellValue("J1", "REPI STOCK");
                                draftNew_URF_Details.SetCellValue("K1", "DAILY USAGE");
                                draftNew_URF_Details.SetCellValue("L1", "STOCK LIFE");
                                draftNew_URF_Details.SetCellValue("M1", "PO NO");
                                draftNew_URF_Details.SetCellValue("N1", "PR NO");
                                draftNew_URF_Details.SetCellValue("O1", "ITEM NAME");
                                draftNew_URF_Details.SetCellValue("P1", "SPECIFICATION");
                                draftNew_URF_Details.SetCellValue("Q1", "QUANTIY");
                                draftNew_URF_Details.SetCellValue("R1", "UNIT MEASURE");
                                draftNew_URF_Details.SetCellValue("S1", "DELIVERY CONFIRMATION DATE");
                                draftNew_URF_Details.SetCellValue("T1", "REQUEST DELIVERY DATE");
                                draftNew_URF_Details.SetCellValue("U1", "REPLY DELIVERY DATE");
                                draftNew_URF_Details.SetCellValue("V1", "STATUS");


                                foreach (Entities_URF_RequestEntry entity in listDetails)
                                {

                                    draftNew_URF_Details.SetCellValue("A" + id_counter_for_excel_URF_Details.ToString(), entity.RdCtrlNo);
                                    draftNew_URF_Details.SetCellValue("B" + id_counter_for_excel_URF_Details.ToString(), entity.RhRequester);
                                    draftNew_URF_Details.SetCellValue("C" + id_counter_for_excel_URF_Details.ToString(), entity.RhCategory);
                                    draftNew_URF_Details.SetCellValue("D" + id_counter_for_excel_URF_Details.ToString(), entity.RhType);
                                    draftNew_URF_Details.SetCellValue("E" + id_counter_for_excel_URF_Details.ToString(), entity.RhSupplier);
                                    draftNew_URF_Details.SetCellValue("F" + id_counter_for_excel_URF_Details.ToString(), entity.RhAttention);
                                    draftNew_URF_Details.SetCellValue("G" + id_counter_for_excel_URF_Details.ToString(), entity.RhReason);
                                    draftNew_URF_Details.SetCellValue("H" + id_counter_for_excel_URF_Details.ToString(), entity.RhOtherReason);
                                    draftNew_URF_Details.SetCellValue("I" + id_counter_for_excel_URF_Details.ToString(), entity.RhTransactionDate);
                                    draftNew_URF_Details.SetCellValue("J" + id_counter_for_excel_URF_Details.ToString(), entity.RepiStock);
                                    draftNew_URF_Details.SetCellValue("K" + id_counter_for_excel_URF_Details.ToString(), entity.DailyUsage);
                                    draftNew_URF_Details.SetCellValue("L" + id_counter_for_excel_URF_Details.ToString(), entity.StockLife);
                                    draftNew_URF_Details.SetCellValue("M" + id_counter_for_excel_URF_Details.ToString(), entity.RdPONO);
                                    draftNew_URF_Details.SetCellValue("N" + id_counter_for_excel_URF_Details.ToString(), entity.RdPRNO);
                                    draftNew_URF_Details.SetCellValue("O" + id_counter_for_excel_URF_Details.ToString(), entity.RdItemName);
                                    draftNew_URF_Details.SetCellValue("P" + id_counter_for_excel_URF_Details.ToString(), entity.RdSpecs);
                                    draftNew_URF_Details.SetCellValue("Q" + id_counter_for_excel_URF_Details.ToString(), entity.RdQuantity);
                                    draftNew_URF_Details.SetCellValue("R" + id_counter_for_excel_URF_Details.ToString(), entity.RdUnitOfMeasure);
                                    draftNew_URF_Details.SetCellValue("S" + id_counter_for_excel_URF_Details.ToString(), entity.RdDeliveryConfirmationDate);
                                    draftNew_URF_Details.SetCellValue("T" + id_counter_for_excel_URF_Details.ToString(), entity.RdRequestedDeliveryDate);
                                    draftNew_URF_Details.SetCellValue("U" + id_counter_for_excel_URF_Details.ToString(), entity.RdReplyDeliveryDate);
                                    draftNew_URF_Details.SetCellValue("V" + id_counter_for_excel_URF_Details.ToString(), entity.StatAll);


                                    id_counter_for_excel_URF_Details++;
                                }
                            }
                        }


                        fsNew_URF_Details.Close();
                        draftNew_URF_Details.SaveAs(pathNew_URF_Destination);
                    }



                    Response.Redirect("Reporting/USER_REPORTS_DUMP/" + pathNew_URF_FileName, false);

                }


                if (ddItemStatus.SelectedItem.Text == "PENDING")
                {
                    int id_counter_for_excel_URF_PENDING = 4;
                    string pathNew_URF_PENDING = Server.MapPath("~/Reporting/URF_PENDING.xlsx");
                    string pathNew_URF_PENDING_FileName = Session["LcRefId"].ToString() + "_URF_PENDING_" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Replace("/", "").Replace(":", "").Replace(" ", "").Trim() + ".xlsx";
                    string pathNew_URF_PENDING_Destination = System.IO.Path.Combine(Server.MapPath("~/Reporting/USER_REPORTS_DUMP/"), pathNew_URF_PENDING_FileName);

                    System.IO.File.Copy(pathNew_URF_PENDING, pathNew_URF_PENDING_Destination, true);


                    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    //DETAILS

                    List<Entities_URF_RequestEntry> listDetails = new List<Entities_URF_RequestEntry>();
                    Entities_URF_RequestEntry status = new Entities_URF_RequestEntry();

                    status.DrFrom = txtFrom.Text.Trim();
                    status.DrTo = txtTo.Text.Trim();

                    //DETAILS
                    listDetails = BLL.URF_TRANSACTION_Reporting_ByDateRange_Details(status).Where(itm => itm.StatAll == "PENDING APPROVAL").ToList();

                    int id_counter_for_excel_URF_PENDING_Details = 2;
                    //string pathNew_URF_PENDING_Details = Server.MapPath("~/Reporting/URF.xlsx");
                    Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew_URF_PENDING_Destination);
                    FileStream fsNew_URF_PENDING_Details = new FileStream(pathNew_URF_PENDING_Destination, FileMode.Open);
                    using (SLDocument draftNew_URF_PENDING_Details = new SLDocument(fsNew_URF_PENDING_Details, "DETAILS"))
                    {

                        // CLEAN UP FIRST THE EXCEL FILE
                        for (int i = 2; i <= 10000; i++)
                        {
                            draftNew_URF_PENDING_Details.SetCellValue("A" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("B" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("C" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("D" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("E" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("F" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("G" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("H" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("I" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("J" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("K" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("L" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("M" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("N" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("O" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("P" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("Q" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("R" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("S" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("T" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("U" + i.ToString(), string.Empty);
                            draftNew_URF_PENDING_Details.SetCellValue("V" + i.ToString(), string.Empty);

                        }

                        if (listDetails != null)
                        {
                            if (listDetails.Count > 0)
                            {

                                foreach (Entities_URF_RequestEntry entity in listDetails)
                                {

                                    draftNew_URF_PENDING_Details.SetCellValue("A" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.RdCtrlNo);
                                    draftNew_URF_PENDING_Details.SetCellValue("B" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.RhRequester);
                                    draftNew_URF_PENDING_Details.SetCellValue("C" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.RhCategory);
                                    draftNew_URF_PENDING_Details.SetCellValue("D" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.RhType);
                                    draftNew_URF_PENDING_Details.SetCellValue("E" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.RhSupplier);
                                    draftNew_URF_PENDING_Details.SetCellValue("F" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.RhAttention);
                                    draftNew_URF_PENDING_Details.SetCellValue("G" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.RhReason);
                                    draftNew_URF_PENDING_Details.SetCellValue("H" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.RhOtherReason);
                                    draftNew_URF_PENDING_Details.SetCellValue("I" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.RhTransactionDate);
                                    draftNew_URF_PENDING_Details.SetCellValue("J" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.RepiStock);
                                    draftNew_URF_PENDING_Details.SetCellValue("K" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.DailyUsage);
                                    draftNew_URF_PENDING_Details.SetCellValue("L" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.StockLife);
                                    draftNew_URF_PENDING_Details.SetCellValue("M" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.RdPONO);
                                    draftNew_URF_PENDING_Details.SetCellValue("N" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.RdPRNO);
                                    draftNew_URF_PENDING_Details.SetCellValue("O" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.RdItemName);
                                    draftNew_URF_PENDING_Details.SetCellValue("P" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.RdSpecs);
                                    draftNew_URF_PENDING_Details.SetCellValue("Q" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.RdQuantity);
                                    draftNew_URF_PENDING_Details.SetCellValue("R" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.RdUnitOfMeasure);
                                    draftNew_URF_PENDING_Details.SetCellValue("S" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.RdDeliveryConfirmationDate);
                                    draftNew_URF_PENDING_Details.SetCellValue("T" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.RdRequestedDeliveryDate);
                                    draftNew_URF_PENDING_Details.SetCellValue("U" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.RdReplyDeliveryDate);
                                    draftNew_URF_PENDING_Details.SetCellValue("V" + id_counter_for_excel_URF_PENDING_Details.ToString(), entity.StatAll);


                                    id_counter_for_excel_URF_PENDING_Details++;
                                }
                            }
                        }


                        fsNew_URF_PENDING_Details.Close();
                        draftNew_URF_PENDING_Details.SaveAs(pathNew_URF_PENDING_Destination);
                    }



                    Response.Redirect("Reporting/USER_REPORTS_DUMP/" + pathNew_URF_PENDING_FileName, false);

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
