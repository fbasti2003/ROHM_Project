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
    public partial class REPORTING_PROFORMA : System.Web.UI.Page
    {
        BLL_PIPL BLL = new BLL_PIPL();
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
                List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();
                List<Entities_PIPL_InvoiceEntry> listDivision = new List<Entities_PIPL_InvoiceEntry>();
                List<Entities_PIPL_InvoiceEntry> listAll = new List<Entities_PIPL_InvoiceEntry>();
                Entities_PIPL_InvoiceEntry status = new Entities_PIPL_InvoiceEntry();

                status.DrFrom = txtFrom.Text.Trim();
                status.DrTo = txtTo.Text.Trim();


                if (ddItemStatus.SelectedItem.Text == "APPROVED/DISAPPROVED" || ddItemStatus.SelectedItem.Text == "ALL")
                {

                    gvDataDetails.Visible = false;
                    gvData.Visible = true;
                    gvDataDivision.Visible = true;
                    gvDataAll.Visible = true;

                    //DEPARTMENT
                    list = BLL.PIPL_TRANSACTION_Reporting_ByDateRange(status);

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
                    listDivision = BLL.PIPL_TRANSACTION_Reporting_ByDateRange_ByDivision(status);

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
                    listAll = BLL.PIPL_TRANSACTION_Reporting_ByDateRange_ByAll(status);

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


                    list = BLL.PIPL_TRANSACTION_Reporting_ByDateRange_Details(status).Where(itm => itm.StatImpex == "PENDING").ToList();

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

                    int id_counter_for_excel_Proforma = 4;
                    string pathNew_PROFORMA = Server.MapPath("~/Reporting/PROFORMA.xlsx");
                    string pathNew_PROFORMA_FileName = Session["LcRefId"].ToString() + "_PROFORMA_" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Replace("/", "").Replace(":", "").Replace(" ", "").Trim() + ".xlsx";
                    string pathNew_PROFORMA_Destination = System.IO.Path.Combine(Server.MapPath("~/Reporting/USER_REPORTS_DUMP/"), pathNew_PROFORMA_FileName);

                    //System.IO.File.Create(pathNew_PROFORMA_Destination);
                    System.IO.File.Copy(pathNew_PROFORMA, pathNew_PROFORMA_Destination, true);

                    Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew_PROFORMA_Destination);
                    FileStream fsNew_PROFORMA = new FileStream(pathNew_PROFORMA_Destination, FileMode.Open);
                    using (SLDocument draftNew_Proforma = new SLDocument(fsNew_PROFORMA, "SUMMARY"))
                    {

                        // CLEAN UP FIRST THE EXCEL FILE
                        for (int i = 1; i <= 5000; i++)
                        {
                            draftNew_Proforma.SetCellValue("A" + i.ToString(), string.Empty);
                            draftNew_Proforma.SetCellValue("B" + i.ToString(), string.Empty);
                            draftNew_Proforma.SetCellValue("C" + i.ToString(), string.Empty);
                            draftNew_Proforma.SetCellValue("D" + i.ToString(), string.Empty);
                            draftNew_Proforma.SetCellValue("E" + i.ToString(), string.Empty);
                            draftNew_Proforma.SetCellValue("F" + i.ToString(), string.Empty);
                            draftNew_Proforma.SetCellValue("G" + i.ToString(), string.Empty);
                            draftNew_Proforma.SetCellValue("H" + i.ToString(), string.Empty);
                            draftNew_Proforma.SetCellValue("I" + i.ToString(), string.Empty);

                        }

                        // DEPARTMENT
                        for (int i = 0; i < gvData.Rows.Count; i++)
                        {
                            Label lblCounter = (Label)gvData.Rows[i].Cells[0].FindControl("lblCounter");
                            Label lblDepartmentName = (Label)gvData.Rows[i].Cells[0].FindControl("lblDepartmentName");
                            Label lblNumberOfRequest = (Label)gvData.Rows[i].Cells[0].FindControl("lblNumberOfRequest");
                            Label lblBuyerApproved = (Label)gvData.Rows[i].Cells[1].FindControl("lblBuyerApproved");
                            Label lblBuyerDisapproved = (Label)gvData.Rows[i].Cells[2].FindControl("lblBuyerDisapproved");
                            Label lblImpexApproved = (Label)gvData.Rows[i].Cells[3].FindControl("lblImpexApproved");
                            Label lblImpexDisapproved = (Label)gvData.Rows[i].Cells[4].FindControl("lblImpexDisapproved");
                            Label lblPendingApproval = (Label)gvData.Rows[i].Cells[5].FindControl("lblPendingApproval");

                            draftNew_Proforma.SetCellValue("A1", "COUNTS BY DEPARTMENT");
                            draftNew_Proforma.SetCellValue("A3", "NO");
                            draftNew_Proforma.SetCellValue("B3", "DEPARTMENT NAME");
                            draftNew_Proforma.SetCellValue("C3", "NUMBER OF REQUEST");
                            draftNew_Proforma.SetCellValue("D3", "BUYER-APPROVED");
                            draftNew_Proforma.SetCellValue("E3", "BUYER-DISAPPROVED");
                            draftNew_Proforma.SetCellValue("F3", "IMPEX-APPROVED");
                            draftNew_Proforma.SetCellValue("G3", "IMPEX-DISAPPROVED");
                            draftNew_Proforma.SetCellValue("H3", "PENDING APPROVAL");
                            draftNew_Proforma.SetCellValue("A" + id_counter_for_excel_Proforma.ToString(), lblCounter.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("B" + id_counter_for_excel_Proforma.ToString(), lblDepartmentName.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("C" + id_counter_for_excel_Proforma.ToString(), lblNumberOfRequest.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("D" + id_counter_for_excel_Proforma.ToString(), lblBuyerApproved.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("E" + id_counter_for_excel_Proforma.ToString(), lblBuyerDisapproved.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("F" + id_counter_for_excel_Proforma.ToString(), lblImpexApproved.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("G" + id_counter_for_excel_Proforma.ToString(), lblImpexDisapproved.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("H" + id_counter_for_excel_Proforma.ToString(), lblPendingApproval.Text.ToUpper());


                            id_counter_for_excel_Proforma++;
                        }

                        // DIVISION
                        id_counter_for_excel_Proforma++;
                        id_counter_for_excel_Proforma++;

                        draftNew_Proforma.SetCellValue("A" + id_counter_for_excel_Proforma.ToString(), "COUNTS BY DIVISION");

                        id_counter_for_excel_Proforma++;
                        id_counter_for_excel_Proforma++;

                        draftNew_Proforma.SetCellValue("A" + id_counter_for_excel_Proforma.ToString(), "NO");
                        draftNew_Proforma.SetCellValue("B" + id_counter_for_excel_Proforma.ToString(), "DIVISION NAME");
                        draftNew_Proforma.SetCellValue("C" + id_counter_for_excel_Proforma.ToString(), "NUMBER OF REQUEST");
                        draftNew_Proforma.SetCellValue("D" + id_counter_for_excel_Proforma.ToString(), "BUYER-APPROVED");
                        draftNew_Proforma.SetCellValue("E" + id_counter_for_excel_Proforma.ToString(), "BUYER-DISAPPROVED");
                        draftNew_Proforma.SetCellValue("F" + id_counter_for_excel_Proforma.ToString(), "IMPEX-APPROVED");
                        draftNew_Proforma.SetCellValue("G" + id_counter_for_excel_Proforma.ToString(), "IMPEX-DISAPPROVED");
                        draftNew_Proforma.SetCellValue("H" + id_counter_for_excel_Proforma.ToString(), "PENDING APPROVAL");

                        id_counter_for_excel_Proforma++;

                        for (int i = 0; i < gvDataDivision.Rows.Count; i++)
                        {
                            Label lblCounter = (Label)gvDataDivision.Rows[i].Cells[0].FindControl("lblCounter");
                            Label lblDivisionName = (Label)gvDataDivision.Rows[i].Cells[0].FindControl("lblDivisionName");
                            Label lblNumberOfRequest = (Label)gvDataDivision.Rows[i].Cells[0].FindControl("lblNumberOfRequest");
                            Label lblBuyerApproved = (Label)gvDataDivision.Rows[i].Cells[1].FindControl("lblBuyerApproved");
                            Label lblBuyerDisapproved = (Label)gvDataDivision.Rows[i].Cells[2].FindControl("lblBuyerDisapproved");
                            Label lblImpexApproved = (Label)gvDataDivision.Rows[i].Cells[3].FindControl("lblImpexApproved");
                            Label lblImpexDisapproved = (Label)gvDataDivision.Rows[i].Cells[4].FindControl("lblImpexDisapproved");
                            Label lblPendingApproval = (Label)gvDataDivision.Rows[i].Cells[5].FindControl("lblPendingApproval");

                            draftNew_Proforma.SetCellValue("A" + id_counter_for_excel_Proforma.ToString(), lblCounter.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("B" + id_counter_for_excel_Proforma.ToString(), lblDivisionName.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("C" + id_counter_for_excel_Proforma.ToString(), lblNumberOfRequest.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("D" + id_counter_for_excel_Proforma.ToString(), lblBuyerApproved.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("E" + id_counter_for_excel_Proforma.ToString(), lblBuyerDisapproved.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("F" + id_counter_for_excel_Proforma.ToString(), lblImpexApproved.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("G" + id_counter_for_excel_Proforma.ToString(), lblImpexDisapproved.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("H" + id_counter_for_excel_Proforma.ToString(), lblPendingApproval.Text.ToUpper());


                            id_counter_for_excel_Proforma++;
                        }

                        // ALL
                        id_counter_for_excel_Proforma++;
                        id_counter_for_excel_Proforma++;

                        draftNew_Proforma.SetCellValue("A" + id_counter_for_excel_Proforma.ToString(), "TOTAL COUNTS");

                        id_counter_for_excel_Proforma++;
                        id_counter_for_excel_Proforma++;

                        draftNew_Proforma.SetCellValue("A" + id_counter_for_excel_Proforma.ToString(), "NO");
                        draftNew_Proforma.SetCellValue("B" + id_counter_for_excel_Proforma.ToString(), "NUMBER OF REQUEST");
                        draftNew_Proforma.SetCellValue("C" + id_counter_for_excel_Proforma.ToString(), "BUYER-APPROVED");
                        draftNew_Proforma.SetCellValue("D" + id_counter_for_excel_Proforma.ToString(), "BUYER-DISAPPROVED");
                        draftNew_Proforma.SetCellValue("E" + id_counter_for_excel_Proforma.ToString(), "IMPEX-APPROVED");
                        draftNew_Proforma.SetCellValue("F" + id_counter_for_excel_Proforma.ToString(), "IMPEX-DISAPPROVED");
                        draftNew_Proforma.SetCellValue("G" + id_counter_for_excel_Proforma.ToString(), "PENDING APPROVAL");
                        draftNew_Proforma.SetCellValue("H" + id_counter_for_excel_Proforma.ToString(), "TOTAL APPROVED");
                        draftNew_Proforma.SetCellValue("I" + id_counter_for_excel_Proforma.ToString(), "TOTAL DISAPPROVED");

                        id_counter_for_excel_Proforma++;

                        for (int i = 0; i < gvDataAll.Rows.Count; i++)
                        {
                            Label lblCounter = (Label)gvDataAll.Rows[i].Cells[0].FindControl("lblCounter");
                            Label lblNumberOfRequest = (Label)gvDataAll.Rows[i].Cells[1].FindControl("lblNumberOfRequest");
                            Label lblBuyerApproved = (Label)gvDataAll.Rows[i].Cells[2].FindControl("lblBuyerApproved");
                            Label lblBuyerDisapproved = (Label)gvDataAll.Rows[i].Cells[3].FindControl("lblBuyerDisapproved");
                            Label lblImpexApproved = (Label)gvDataAll.Rows[i].Cells[4].FindControl("lblImpexApproved");
                            Label lblImpexDisapproved = (Label)gvDataAll.Rows[i].Cells[5].FindControl("lblImpexDisapproved");
                            Label lblPendingApproval = (Label)gvDataAll.Rows[i].Cells[6].FindControl("lblPendingApproval");
                            Label lblTotalApproved = (Label)gvDataAll.Rows[i].Cells[7].FindControl("lblTotalApproved");
                            Label lblTotalDisapproved = (Label)gvDataAll.Rows[i].Cells[8].FindControl("lblTotalDisapproved");

                            draftNew_Proforma.SetCellValue("A" + id_counter_for_excel_Proforma.ToString(), lblCounter.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("B" + id_counter_for_excel_Proforma.ToString(), lblNumberOfRequest.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("C" + id_counter_for_excel_Proforma.ToString(), lblBuyerApproved.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("D" + id_counter_for_excel_Proforma.ToString(), lblBuyerDisapproved.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("E" + id_counter_for_excel_Proforma.ToString(), lblImpexApproved.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("F" + id_counter_for_excel_Proforma.ToString(), lblImpexDisapproved.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("G" + id_counter_for_excel_Proforma.ToString(), lblPendingApproval.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("H" + id_counter_for_excel_Proforma.ToString(), lblTotalApproved.Text.ToUpper());
                            draftNew_Proforma.SetCellValue("I" + id_counter_for_excel_Proforma.ToString(), lblTotalDisapproved.Text.ToUpper());


                            id_counter_for_excel_Proforma++;
                        }


                        fsNew_PROFORMA.Close();
                        draftNew_Proforma.SaveAs(pathNew_PROFORMA);
                    }

                    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    //DETAILS

                    List<Entities_PIPL_InvoiceEntry> listDetails = new List<Entities_PIPL_InvoiceEntry>();
                    Entities_PIPL_InvoiceEntry status = new Entities_PIPL_InvoiceEntry();

                    status.DrFrom = txtFrom.Text.Trim();
                    status.DrTo = txtTo.Text.Trim();

                    //DETAILS
                    listDetails = BLL.PIPL_TRANSACTION_Reporting_ByDateRange_Details(status);

                    int id_counter_for_excel_Proforma_Details = 2;
                    string pathNew_Proforma_Details = Server.MapPath("~/Reporting/PROFORMA.xlsx");
                    Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew_PROFORMA_Destination);
                    FileStream fsNew_Proforma_Details = new FileStream(pathNew_Proforma_Details, FileMode.Open);
                    using (SLDocument draftNew_Proforma_Details = new SLDocument(pathNew_PROFORMA_Destination, "DETAILS"))
                    {

                        // CLEAN UP FIRST THE EXCEL FILE
                        for (int i = 1; i <= 10000; i++)
                        {
                            draftNew_Proforma_Details.SetCellValue("A" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("B" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("C" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("D" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("E" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("F" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("G" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("H" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("I" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("J" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("K" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("L" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("M" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("N" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("O" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("P" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("Q" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("R" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("S" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("T" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("U" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("V" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("W" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("X" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("Y" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("Z" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("AA" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("AB" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("AC" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("AD" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("AE" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("AF" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("AG" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details.SetCellValue("AH" + i.ToString(), string.Empty);

                        }

                        if (listDetails != null)
                        {
                            if (listDetails.Count > 0)
                            {
                                draftNew_Proforma_Details.SetCellValue("A1", "CTRLNO");
                                draftNew_Proforma_Details.SetCellValue("B1", "DESCRIPTION");
                                draftNew_Proforma_Details.SetCellValue("C1", "CASE UNIT");
                                draftNew_Proforma_Details.SetCellValue("D1", "CASE NO.");
                                draftNew_Proforma_Details.SetCellValue("E1", "UOM");
                                draftNew_Proforma_Details.SetCellValue("F1", "SPECIFICATION");
                                draftNew_Proforma_Details.SetCellValue("G1", "PO NUMBER");
                                draftNew_Proforma_Details.SetCellValue("H1", "CURRENCY");
                                draftNew_Proforma_Details.SetCellValue("I1", "QUANTITY");
                                draftNew_Proforma_Details.SetCellValue("J1", "UNIT PRICE");
                                draftNew_Proforma_Details.SetCellValue("K1", "VALUE IN USD");
                                draftNew_Proforma_Details.SetCellValue("L1", "COMMERCIAL VALUE");
                                draftNew_Proforma_Details.SetCellValue("M1", "REFERENCE NO.");
                                draftNew_Proforma_Details.SetCellValue("N1", "INVOICE NO.");
                                draftNew_Proforma_Details.SetCellValue("O1", "BDN");
                                draftNew_Proforma_Details.SetCellValue("P1", "BDN VALUE");
                                draftNew_Proforma_Details.SetCellValue("Q1", "BDN ATTENTION");
                                draftNew_Proforma_Details.SetCellValue("R1", "MODE OF SHIPMENT");
                                draftNew_Proforma_Details.SetCellValue("S1", "EXPECTED TIME OF DELIVERY");
                                draftNew_Proforma_Details.SetCellValue("T1", "TRADE TERMS");
                                draftNew_Proforma_Details.SetCellValue("U1", "PURPOSE");
                                draftNew_Proforma_Details.SetCellValue("V1", "CONSIGNEE");
                                draftNew_Proforma_Details.SetCellValue("W1", "CONSIGNEE DEPT/SEC.");
                                draftNew_Proforma_Details.SetCellValue("X1", "REQUESTER");
                                draftNew_Proforma_Details.SetCellValue("Y1", "DEPARTMENT");
                                draftNew_Proforma_Details.SetCellValue("Z1", "NET WEIGHT");
                                draftNew_Proforma_Details.SetCellValue("AA1", "GROSS WEIGHT");
                                draftNew_Proforma_Details.SetCellValue("AB1", "MEASUREMENT");
                                draftNew_Proforma_Details.SetCellValue("AC1", "SALES TYPE");
                                draftNew_Proforma_Details.SetCellValue("AD1", "BUSINESS UNIT");
                                draftNew_Proforma_Details.SetCellValue("AE1", "ACCOUNT CODE");
                                draftNew_Proforma_Details.SetCellValue("AF1", "TRANSACTION DATE");
                                draftNew_Proforma_Details.SetCellValue("AG1", "CATEGORY");
                                draftNew_Proforma_Details.SetCellValue("AH1", "STATUS");

                                foreach (Entities_PIPL_InvoiceEntry entity in listDetails)
                                {

                                    draftNew_Proforma_Details.SetCellValue("A" + id_counter_for_excel_Proforma_Details.ToString(), entity.CtrlNo);
                                    draftNew_Proforma_Details.SetCellValue("B" + id_counter_for_excel_Proforma_Details.ToString(), entity.Description);
                                    draftNew_Proforma_Details.SetCellValue("C" + id_counter_for_excel_Proforma_Details.ToString(), entity.CaseUnit);
                                    draftNew_Proforma_Details.SetCellValue("D" + id_counter_for_excel_Proforma_Details.ToString(), entity.CaseNumber);
                                    draftNew_Proforma_Details.SetCellValue("E" + id_counter_for_excel_Proforma_Details.ToString(), entity.Uom);
                                    draftNew_Proforma_Details.SetCellValue("F" + id_counter_for_excel_Proforma_Details.ToString(), entity.Specification);
                                    draftNew_Proforma_Details.SetCellValue("G" + id_counter_for_excel_Proforma_Details.ToString(), entity.PoNo);
                                    draftNew_Proforma_Details.SetCellValue("H" + id_counter_for_excel_Proforma_Details.ToString(), entity.Currency);
                                    draftNew_Proforma_Details.SetCellValue("I" + id_counter_for_excel_Proforma_Details.ToString(), entity.Quantity);
                                    draftNew_Proforma_Details.SetCellValue("J" + id_counter_for_excel_Proforma_Details.ToString(), entity.UPrice);
                                    draftNew_Proforma_Details.SetCellValue("K" + id_counter_for_excel_Proforma_Details.ToString(), entity.ValueInUsd);
                                    draftNew_Proforma_Details.SetCellValue("L" + id_counter_for_excel_Proforma_Details.ToString(), entity.CommercialValue);
                                    draftNew_Proforma_Details.SetCellValue("M" + id_counter_for_excel_Proforma_Details.ToString(), entity.ReferenceNo);
                                    draftNew_Proforma_Details.SetCellValue("N" + id_counter_for_excel_Proforma_Details.ToString(), entity.InvoiceNo);
                                    draftNew_Proforma_Details.SetCellValue("O" + id_counter_for_excel_Proforma_Details.ToString(), entity.Bdn);
                                    draftNew_Proforma_Details.SetCellValue("P" + id_counter_for_excel_Proforma_Details.ToString(), entity.BdnValue);
                                    draftNew_Proforma_Details.SetCellValue("Q" + id_counter_for_excel_Proforma_Details.ToString(), entity.Attention2);
                                    draftNew_Proforma_Details.SetCellValue("R" + id_counter_for_excel_Proforma_Details.ToString(), entity.ModeOfShipment);
                                    draftNew_Proforma_Details.SetCellValue("S" + id_counter_for_excel_Proforma_Details.ToString(), entity.Etd);
                                    draftNew_Proforma_Details.SetCellValue("T" + id_counter_for_excel_Proforma_Details.ToString(), entity.TradeTerms);
                                    draftNew_Proforma_Details.SetCellValue("U" + id_counter_for_excel_Proforma_Details.ToString(), entity.Purpose);
                                    draftNew_Proforma_Details.SetCellValue("V" + id_counter_for_excel_Proforma_Details.ToString(), entity.Consignee);
                                    draftNew_Proforma_Details.SetCellValue("W" + id_counter_for_excel_Proforma_Details.ToString(), entity.Secdept1);
                                    draftNew_Proforma_Details.SetCellValue("X" + id_counter_for_excel_Proforma_Details.ToString(), entity.Requester);
                                    draftNew_Proforma_Details.SetCellValue("Y" + id_counter_for_excel_Proforma_Details.ToString(), entity.Department);
                                    draftNew_Proforma_Details.SetCellValue("Z" + id_counter_for_excel_Proforma_Details.ToString(), entity.NetWeight);
                                    draftNew_Proforma_Details.SetCellValue("AA" + id_counter_for_excel_Proforma_Details.ToString(), entity.GrossWeight);
                                    draftNew_Proforma_Details.SetCellValue("AB" + id_counter_for_excel_Proforma_Details.ToString(), entity.Measurement);
                                    draftNew_Proforma_Details.SetCellValue("AC" + id_counter_for_excel_Proforma_Details.ToString(), entity.SalesType);
                                    draftNew_Proforma_Details.SetCellValue("AD" + id_counter_for_excel_Proforma_Details.ToString(), entity.BusinessUnit);
                                    draftNew_Proforma_Details.SetCellValue("AE" + id_counter_for_excel_Proforma_Details.ToString(), entity.AccountCode);
                                    draftNew_Proforma_Details.SetCellValue("AF" + id_counter_for_excel_Proforma_Details.ToString(), entity.TransactionDate);
                                    draftNew_Proforma_Details.SetCellValue("AG" + id_counter_for_excel_Proforma_Details.ToString(), entity.Category);
                                    draftNew_Proforma_Details.SetCellValue("AH" + id_counter_for_excel_Proforma_Details.ToString(), entity.StatImpex);

                                    id_counter_for_excel_Proforma_Details++;
                                }
                            }
                        }


                        fsNew_Proforma_Details.Close();
                        draftNew_Proforma_Details.SaveAs(pathNew_PROFORMA_Destination);
                    }


                    Response.Redirect("Reporting/USER_REPORTS_DUMP/" + pathNew_PROFORMA_FileName, false);

                }


                if (ddItemStatus.SelectedItem.Text == "PENDING")
                {

                    string pathNew_PROFORMA_PENDING = Server.MapPath("~/Reporting/PROFORMA_PENDING.xlsx");
                    string pathNew_PROFORMA_FileName_PENDING = Session["LcRefId"].ToString() + "_PROFORMA_PENDING_" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Replace("/", "").Replace(":", "").Replace(" ", "").Trim() + ".xlsx";
                    string pathNew_PROFORMA_Destination_PENDING = System.IO.Path.Combine(Server.MapPath("~/Reporting/USER_REPORTS_DUMP/"), pathNew_PROFORMA_FileName_PENDING);

                    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    //DETAILS

                    List<Entities_PIPL_InvoiceEntry> listDetails = new List<Entities_PIPL_InvoiceEntry>();
                    Entities_PIPL_InvoiceEntry status = new Entities_PIPL_InvoiceEntry();

                    status.DrFrom = txtFrom.Text.Trim();
                    status.DrTo = txtTo.Text.Trim();

                    //DETAILS
                    listDetails = BLL.PIPL_TRANSACTION_Reporting_ByDateRange_Details(status).Where(itm => itm.StatImpex == "PENDING").ToList();

                    System.IO.File.Copy(pathNew_PROFORMA_PENDING, pathNew_PROFORMA_Destination_PENDING, true);

                    int id_counter_for_excel_Proforma_Details_Pending = 2;
                    Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew_PROFORMA_Destination_PENDING);
                    FileStream fsNew_Proforma_Details_Pending = new FileStream(pathNew_PROFORMA_PENDING, FileMode.Open);
                    using (SLDocument draftNew_Proforma_Details_Pending = new SLDocument(pathNew_PROFORMA_Destination_PENDING, "DETAILS"))
                    {

                        // CLEAN UP FIRST THE EXCEL FILE
                        for (int i = 1; i <= 10000; i++)
                        {
                            draftNew_Proforma_Details_Pending.SetCellValue("A" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("B" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("C" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("D" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("E" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("F" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("G" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("H" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("I" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("J" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("K" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("L" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("M" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("N" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("O" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("P" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("Q" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("R" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("S" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("T" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("U" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("V" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("W" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("X" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("Y" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("Z" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("AA" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("AB" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("AC" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("AD" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("AE" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("AF" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("AG" + i.ToString(), string.Empty);
                            draftNew_Proforma_Details_Pending.SetCellValue("AH" + i.ToString(), string.Empty);

                        }

                        if (listDetails != null)
                        {
                            if (listDetails.Count > 0)
                            {
                                draftNew_Proforma_Details_Pending.SetCellValue("A1", "CTRLNO");
                                draftNew_Proforma_Details_Pending.SetCellValue("B1", "DESCRIPTION");
                                draftNew_Proforma_Details_Pending.SetCellValue("C1", "CASE UNIT");
                                draftNew_Proforma_Details_Pending.SetCellValue("D1", "CASE NO.");
                                draftNew_Proforma_Details_Pending.SetCellValue("E1", "UOM");
                                draftNew_Proforma_Details_Pending.SetCellValue("F1", "SPECIFICATION");
                                draftNew_Proforma_Details_Pending.SetCellValue("G1", "PO NUMBER");
                                draftNew_Proforma_Details_Pending.SetCellValue("H1", "CURRENCY");
                                draftNew_Proforma_Details_Pending.SetCellValue("I1", "QUANTITY");
                                draftNew_Proforma_Details_Pending.SetCellValue("J1", "UNIT PRICE");
                                draftNew_Proforma_Details_Pending.SetCellValue("K1", "VALUE IN USD");
                                draftNew_Proforma_Details_Pending.SetCellValue("L1", "COMMERCIAL VALUE");
                                draftNew_Proforma_Details_Pending.SetCellValue("M1", "REFERENCE NO.");
                                draftNew_Proforma_Details_Pending.SetCellValue("N1", "INVOICE NO.");
                                draftNew_Proforma_Details_Pending.SetCellValue("O1", "BDN");
                                draftNew_Proforma_Details_Pending.SetCellValue("P1", "BDN VALUE");
                                draftNew_Proforma_Details_Pending.SetCellValue("Q1", "BDN ATTENTION");
                                draftNew_Proforma_Details_Pending.SetCellValue("R1", "MODE OF SHIPMENT");
                                draftNew_Proforma_Details_Pending.SetCellValue("S1", "EXPECTED TIME OF DELIVERY");
                                draftNew_Proforma_Details_Pending.SetCellValue("T1", "TRADE TERMS");
                                draftNew_Proforma_Details_Pending.SetCellValue("U1", "PURPOSE");
                                draftNew_Proforma_Details_Pending.SetCellValue("V1", "CONSIGNEE");
                                draftNew_Proforma_Details_Pending.SetCellValue("W1", "CONSIGNEE DEPT/SEC.");
                                draftNew_Proforma_Details_Pending.SetCellValue("X1", "REQUESTER");
                                draftNew_Proforma_Details_Pending.SetCellValue("Y1", "DEPARTMENT");
                                draftNew_Proforma_Details_Pending.SetCellValue("Z1", "NET WEIGHT");
                                draftNew_Proforma_Details_Pending.SetCellValue("AA1", "GROSS WEIGHT");
                                draftNew_Proforma_Details_Pending.SetCellValue("AB1", "MEASUREMENT");
                                draftNew_Proforma_Details_Pending.SetCellValue("AC1", "SALES TYPE");
                                draftNew_Proforma_Details_Pending.SetCellValue("AD1", "BUSINESS UNIT");
                                draftNew_Proforma_Details_Pending.SetCellValue("AE1", "ACCOUNT CODE");
                                draftNew_Proforma_Details_Pending.SetCellValue("AF1", "TRANSACTION DATE");
                                draftNew_Proforma_Details_Pending.SetCellValue("AG1", "CATEGORY");
                                draftNew_Proforma_Details_Pending.SetCellValue("AH1", "STATUS");

                                foreach (Entities_PIPL_InvoiceEntry entity in listDetails)
                                {

                                    draftNew_Proforma_Details_Pending.SetCellValue("A" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.CtrlNo);
                                    draftNew_Proforma_Details_Pending.SetCellValue("B" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.Description);
                                    draftNew_Proforma_Details_Pending.SetCellValue("C" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.CaseUnit);
                                    draftNew_Proforma_Details_Pending.SetCellValue("D" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.CaseNumber);
                                    draftNew_Proforma_Details_Pending.SetCellValue("E" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.Uom);
                                    draftNew_Proforma_Details_Pending.SetCellValue("F" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.Specification);
                                    draftNew_Proforma_Details_Pending.SetCellValue("G" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.PoNo);
                                    draftNew_Proforma_Details_Pending.SetCellValue("H" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.Currency);
                                    draftNew_Proforma_Details_Pending.SetCellValue("I" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.Quantity);
                                    draftNew_Proforma_Details_Pending.SetCellValue("J" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.UPrice);
                                    draftNew_Proforma_Details_Pending.SetCellValue("K" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.ValueInUsd);
                                    draftNew_Proforma_Details_Pending.SetCellValue("L" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.CommercialValue);
                                    draftNew_Proforma_Details_Pending.SetCellValue("M" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.ReferenceNo);
                                    draftNew_Proforma_Details_Pending.SetCellValue("N" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.InvoiceNo);
                                    draftNew_Proforma_Details_Pending.SetCellValue("O" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.Bdn);
                                    draftNew_Proforma_Details_Pending.SetCellValue("P" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.BdnValue);
                                    draftNew_Proforma_Details_Pending.SetCellValue("Q" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.Attention2);
                                    draftNew_Proforma_Details_Pending.SetCellValue("R" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.ModeOfShipment);
                                    draftNew_Proforma_Details_Pending.SetCellValue("S" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.Etd);
                                    draftNew_Proforma_Details_Pending.SetCellValue("T" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.TradeTerms);
                                    draftNew_Proforma_Details_Pending.SetCellValue("U" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.Purpose);
                                    draftNew_Proforma_Details_Pending.SetCellValue("V" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.Consignee);
                                    draftNew_Proforma_Details_Pending.SetCellValue("W" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.Secdept1);
                                    draftNew_Proforma_Details_Pending.SetCellValue("X" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.Requester);
                                    draftNew_Proforma_Details_Pending.SetCellValue("Y" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.Department);
                                    draftNew_Proforma_Details_Pending.SetCellValue("Z" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.NetWeight);
                                    draftNew_Proforma_Details_Pending.SetCellValue("AA" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.GrossWeight);
                                    draftNew_Proforma_Details_Pending.SetCellValue("AB" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.Measurement);
                                    draftNew_Proforma_Details_Pending.SetCellValue("AC" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.SalesType);
                                    draftNew_Proforma_Details_Pending.SetCellValue("AD" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.BusinessUnit);
                                    draftNew_Proforma_Details_Pending.SetCellValue("AE" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.AccountCode);
                                    draftNew_Proforma_Details_Pending.SetCellValue("AF" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.TransactionDate);
                                    draftNew_Proforma_Details_Pending.SetCellValue("AG" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.Category);
                                    draftNew_Proforma_Details_Pending.SetCellValue("AH" + id_counter_for_excel_Proforma_Details_Pending.ToString(), entity.StatImpex);

                                    id_counter_for_excel_Proforma_Details_Pending++;
                                }
                            }
                        }


                        fsNew_Proforma_Details_Pending.Close();
                        draftNew_Proforma_Details_Pending.SaveAs(pathNew_PROFORMA_Destination_PENDING);

                    }


                    Response.Redirect("Reporting/USER_REPORTS_DUMP/" + pathNew_PROFORMA_FileName_PENDING, false);

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

        protected void gvDataAll_OnRowDataBound(object sender, GridViewRowEventArgs e)
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
