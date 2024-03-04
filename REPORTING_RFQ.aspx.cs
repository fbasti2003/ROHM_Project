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
    public partial class REPORTING_RFQ : System.Web.UI.Page
    {
        BLL_RFQ BLL = new BLL_RFQ();
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

                List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                List<Entities_RFQ_RequestEntry> listDivision = new List<Entities_RFQ_RequestEntry>();
                List<Entities_RFQ_RequestEntry> listAll = new List<Entities_RFQ_RequestEntry>();
                Entities_RFQ_RequestEntry status = new Entities_RFQ_RequestEntry();

                status.DrFrom = txtFrom.Text.Trim();
                status.DrTo = txtTo.Text.Trim();

                if (ddItemStatus.SelectedItem.Text == "APPROVED/DISAPPROVED")
                {
                    gvDataDetails.Visible = false;
                    gvData.Visible = true;
                    gvDataDivision.Visible = true;
                    gvDataAll.Visible = true;

                    //DEPARTMENT
                    list = BLL.RFQ_TRANSACTION_Reporting_ByDateRange(status);

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
                    listDivision = BLL.RFQ_TRANSACTION_Reporting_ByDateRange_ByDivision(status);

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
                    listAll = BLL.RFQ_TRANSACTION_Reporting_ByDateRange_ByAll(status);

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

                if (ddItemStatus.SelectedItem.Text == "FOR SENDING")
                {
                    gvData.Visible = false;
                    gvDataDivision.Visible = false;
                    gvDataAll.Visible = false;
                    gvDataDetails.Visible = true;

                    list = BLL.RFQ_TRANSACTION_Reporting_Sending_ByDateRange(status);

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

                if (ddItemStatus.SelectedItem.Text == "FOR RESEND")
                {
                    gvData.Visible = false;
                    gvDataDivision.Visible = false;
                    gvDataAll.Visible = false;
                    gvDataDetails.Visible = true;

                    list = BLL.RFQ_TRANSACTION_Reporting_Resend_ByDateRange(status);

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

                if (ddItemStatus.SelectedItem.Text == "ALL")
                {
                    gvData.Visible = false;
                    gvDataDivision.Visible = false;
                    gvDataAll.Visible = false;
                    gvDataDetails.Visible = true;

                    list = BLL.RFQ_TRANSACTION_Reporting_ALL_ByDateRange(status);

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

                if (ddItemStatus.SelectedItem.Text == "APPROVED/DISAPPROVED")
                {


                    int id_counter_for_excel_RFQ = 4;
                    string pathNew_RFQ = Server.MapPath("~/Reporting/RFQ.xlsx");
                    string pathNew_RFQ_FileName = Session["LcRefId"].ToString() + "_RFQ_" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Replace("/", "").Replace(":", "").Replace(" ", "").Trim() + ".xlsx";
                    string pathNew_RFQ_Destination = System.IO.Path.Combine(Server.MapPath("~/Reporting/USER_REPORTS_DUMP/"), pathNew_RFQ_FileName);

                    //System.IO.File.Create(pathNew_RFQ_Destination);
                    System.IO.File.Copy(pathNew_RFQ, pathNew_RFQ_Destination, true);

                    Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew_RFQ_Destination);
                    FileStream fsNew_RFQ = new FileStream(pathNew_RFQ_Destination, FileMode.Open);
                    using (SLDocument draftNew_RFQ = new SLDocument(fsNew_RFQ, "SUMMARY"))
                    {

                        // CLEAN UP FIRST THE EXCEL FILE
                        for (int i = 1; i <= 5000; i++)
                        {
                            draftNew_RFQ.SetCellValue("A" + i.ToString(), string.Empty);
                            draftNew_RFQ.SetCellValue("B" + i.ToString(), string.Empty);
                            draftNew_RFQ.SetCellValue("C" + i.ToString(), string.Empty);
                            draftNew_RFQ.SetCellValue("D" + i.ToString(), string.Empty);
                            draftNew_RFQ.SetCellValue("E" + i.ToString(), string.Empty);
                            draftNew_RFQ.SetCellValue("F" + i.ToString(), string.Empty);
                            draftNew_RFQ.SetCellValue("G" + i.ToString(), string.Empty);
                            draftNew_RFQ.SetCellValue("H" + i.ToString(), string.Empty);
                            draftNew_RFQ.SetCellValue("I" + i.ToString(), string.Empty);
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

                            draftNew_RFQ.SetCellValue("A1", "COUNTS BY DEPARTMENT");
                            draftNew_RFQ.SetCellValue("A3", "NO");
                            draftNew_RFQ.SetCellValue("B3", "DEPARTMENT NAME");
                            draftNew_RFQ.SetCellValue("C3", "NUMBER OF REQUEST");
                            draftNew_RFQ.SetCellValue("D3", "BUYER-APPROVED");
                            draftNew_RFQ.SetCellValue("E3", "BUYER-DISAPPROVED");
                            draftNew_RFQ.SetCellValue("F3", "SC-MNGR-APPROVED");
                            draftNew_RFQ.SetCellValue("G3", "SC-MNGR-DISAPPROVED");
                            draftNew_RFQ.SetCellValue("H3", "POSTED/CLOSED");
                            draftNew_RFQ.SetCellValue("I3", "PENDING APPROVAL");
                            draftNew_RFQ.SetCellValue("A" + id_counter_for_excel_RFQ.ToString(), lblCounter.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("B" + id_counter_for_excel_RFQ.ToString(), lblDepartmentName.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("C" + id_counter_for_excel_RFQ.ToString(), lblNumberOfRequest.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("D" + id_counter_for_excel_RFQ.ToString(), lblBuyerApproved.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("E" + id_counter_for_excel_RFQ.ToString(), lblBuyerDisapproved.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("F" + id_counter_for_excel_RFQ.ToString(), lblSCManagerApproved.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("G" + id_counter_for_excel_RFQ.ToString(), lblSCManagerDisapproved.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("H" + id_counter_for_excel_RFQ.ToString(), lblPosted.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("I" + id_counter_for_excel_RFQ.ToString(), lblPendingApproval.Text.ToUpper());

                            id_counter_for_excel_RFQ++;
                        }

                        // DIVISION
                        id_counter_for_excel_RFQ++;
                        id_counter_for_excel_RFQ++;

                        draftNew_RFQ.SetCellValue("A" + id_counter_for_excel_RFQ.ToString(), "COUNTS BY DIVISION");

                        id_counter_for_excel_RFQ++;
                        id_counter_for_excel_RFQ++;

                        draftNew_RFQ.SetCellValue("A" + id_counter_for_excel_RFQ.ToString(), "COUNTS BY DEPARTMENT");
                        draftNew_RFQ.SetCellValue("A" + id_counter_for_excel_RFQ.ToString(), "NO");
                        draftNew_RFQ.SetCellValue("B" + id_counter_for_excel_RFQ.ToString(), "DEPARTMENT NAME");
                        draftNew_RFQ.SetCellValue("C" + id_counter_for_excel_RFQ.ToString(), "NUMBER OF REQUEST");
                        draftNew_RFQ.SetCellValue("D" + id_counter_for_excel_RFQ.ToString(), "BUYER-APPROVED");
                        draftNew_RFQ.SetCellValue("E" + id_counter_for_excel_RFQ.ToString(), "BUYER-DISAPPROVED");
                        draftNew_RFQ.SetCellValue("F" + id_counter_for_excel_RFQ.ToString(), "SC-MNGR-APPROVED");
                        draftNew_RFQ.SetCellValue("G" + id_counter_for_excel_RFQ.ToString(), "SC-MNGR-DISAPPROVED");
                        draftNew_RFQ.SetCellValue("H" + id_counter_for_excel_RFQ.ToString(), "POSTED/CLOSED");
                        draftNew_RFQ.SetCellValue("I" + id_counter_for_excel_RFQ.ToString(), "PENDING APPROVAL");

                        id_counter_for_excel_RFQ++;

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

                            draftNew_RFQ.SetCellValue("A" + id_counter_for_excel_RFQ.ToString(), lblCounter.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("B" + id_counter_for_excel_RFQ.ToString(), lblDivisionName.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("C" + id_counter_for_excel_RFQ.ToString(), lblNumberOfRequest.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("D" + id_counter_for_excel_RFQ.ToString(), lblBuyerApproved.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("E" + id_counter_for_excel_RFQ.ToString(), lblBuyerDisapproved.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("F" + id_counter_for_excel_RFQ.ToString(), lblSCManagerApproved.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("G" + id_counter_for_excel_RFQ.ToString(), lblSCManagerDisapproved.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("H" + id_counter_for_excel_RFQ.ToString(), lblPosted.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("I" + id_counter_for_excel_RFQ.ToString(), lblPendingApproval.Text.ToUpper());


                            id_counter_for_excel_RFQ++;
                        }

                        // ALL
                        id_counter_for_excel_RFQ++;
                        id_counter_for_excel_RFQ++;

                        draftNew_RFQ.SetCellValue("A" + id_counter_for_excel_RFQ.ToString(), "TOTAL COUNTS");

                        id_counter_for_excel_RFQ++;
                        id_counter_for_excel_RFQ++;

                        draftNew_RFQ.SetCellValue("A" + id_counter_for_excel_RFQ.ToString(), "NO");
                        draftNew_RFQ.SetCellValue("B" + id_counter_for_excel_RFQ.ToString(), "NUMBER OF REQUEST");
                        draftNew_RFQ.SetCellValue("C" + id_counter_for_excel_RFQ.ToString(), "BUYER-APPROVED");
                        draftNew_RFQ.SetCellValue("D" + id_counter_for_excel_RFQ.ToString(), "BUYER-DISAPPROVED");
                        draftNew_RFQ.SetCellValue("E" + id_counter_for_excel_RFQ.ToString(), "SC-MNGR-APPROVED");
                        draftNew_RFQ.SetCellValue("F" + id_counter_for_excel_RFQ.ToString(), "SC-MNGR-DISAPPROVED");
                        draftNew_RFQ.SetCellValue("G" + id_counter_for_excel_RFQ.ToString(), "PENDING APPROVAL");
                        draftNew_RFQ.SetCellValue("H" + id_counter_for_excel_RFQ.ToString(), "TOTAL POSTED/CLOSED");
                        draftNew_RFQ.SetCellValue("I" + id_counter_for_excel_RFQ.ToString(), "TOTAL DISAPPROVED");

                        id_counter_for_excel_RFQ++;

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

                            draftNew_RFQ.SetCellValue("A" + id_counter_for_excel_RFQ.ToString(), lblCounter.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("B" + id_counter_for_excel_RFQ.ToString(), lblNumberOfRequest.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("C" + id_counter_for_excel_RFQ.ToString(), lblBuyerApproved.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("D" + id_counter_for_excel_RFQ.ToString(), lblBuyerDisapproved.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("E" + id_counter_for_excel_RFQ.ToString(), lblSCManagerApproved.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("F" + id_counter_for_excel_RFQ.ToString(), lblSCManagerDisapproved.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("G" + id_counter_for_excel_RFQ.ToString(), lblPendingApproval.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("H" + id_counter_for_excel_RFQ.ToString(), lblTotalPosted.Text.ToUpper());
                            draftNew_RFQ.SetCellValue("I" + id_counter_for_excel_RFQ.ToString(), lblTotalDisapproved.Text.ToUpper());


                            id_counter_for_excel_RFQ++;
                        }


                        fsNew_RFQ.Close();
                        draftNew_RFQ.SaveAs(pathNew_RFQ);
                    }


                    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    //DETAILS

                    List<Entities_RFQ_RequestEntry> listDetails = new List<Entities_RFQ_RequestEntry>();
                    Entities_RFQ_RequestEntry status = new Entities_RFQ_RequestEntry();

                    status.DrFrom = txtFrom.Text.Trim();
                    status.DrTo = txtTo.Text.Trim();

                    //DETAILS
                    listDetails = BLL.RFQ_TRANSACTION_Reporting_ByDateRange_Details(status);

                    int id_counter_for_excel_RFQ_Details = 2;
                    string pathNew_RFQ_Details = Server.MapPath("~/Reporting/RFQ.xlsx");
                    Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew_RFQ_Destination);
                    FileStream fsNew_RFQ_Details = new FileStream(pathNew_RFQ_Destination, FileMode.Open);
                    using (SLDocument draftNew_RFQ_Details = new SLDocument(fsNew_RFQ_Details, "DETAILS"))
                    {

                        // CLEAN UP FIRST THE EXCEL FILE
                        for (int i = 1; i <= 10000; i++)
                        {
                            draftNew_RFQ_Details.SetCellValue("A" + i.ToString(), string.Empty);
                            draftNew_RFQ_Details.SetCellValue("B" + i.ToString(), string.Empty);
                            draftNew_RFQ_Details.SetCellValue("C" + i.ToString(), string.Empty);
                            draftNew_RFQ_Details.SetCellValue("D" + i.ToString(), string.Empty);
                            draftNew_RFQ_Details.SetCellValue("E" + i.ToString(), string.Empty);
                            draftNew_RFQ_Details.SetCellValue("F" + i.ToString(), string.Empty);
                            draftNew_RFQ_Details.SetCellValue("G" + i.ToString(), string.Empty);
                            draftNew_RFQ_Details.SetCellValue("H" + i.ToString(), string.Empty);


                        }

                        if (listDetails != null)
                        {
                            if (listDetails.Count > 0)
                            {
                                draftNew_RFQ_Details.SetCellValue("A1", "RFQNO");
                                draftNew_RFQ_Details.SetCellValue("B1", "DESCRIPTION");
                                draftNew_RFQ_Details.SetCellValue("C1", "SPECIFICATION");
                                draftNew_RFQ_Details.SetCellValue("D1", "MAKER");
                                draftNew_RFQ_Details.SetCellValue("E1", "CATEGORY");
                                draftNew_RFQ_Details.SetCellValue("F1", "REQUESTER");
                                draftNew_RFQ_Details.SetCellValue("G1", "TRANSACTION DATE");
                                draftNew_RFQ_Details.SetCellValue("H1", "STATUS");


                                foreach (Entities_RFQ_RequestEntry entity in listDetails)
                                {

                                    draftNew_RFQ_Details.SetCellValue("A" + id_counter_for_excel_RFQ_Details.ToString(), entity.Rfqno);
                                    draftNew_RFQ_Details.SetCellValue("B" + id_counter_for_excel_RFQ_Details.ToString(), entity.RdDescription);
                                    draftNew_RFQ_Details.SetCellValue("C" + id_counter_for_excel_RFQ_Details.ToString(), entity.RdSpecs);
                                    draftNew_RFQ_Details.SetCellValue("D" + id_counter_for_excel_RFQ_Details.ToString(), entity.RdMaker);
                                    draftNew_RFQ_Details.SetCellValue("E" + id_counter_for_excel_RFQ_Details.ToString(), entity.Category);
                                    draftNew_RFQ_Details.SetCellValue("F" + id_counter_for_excel_RFQ_Details.ToString(), entity.Requester);
                                    draftNew_RFQ_Details.SetCellValue("G" + id_counter_for_excel_RFQ_Details.ToString(), entity.TransactionDate);
                                    draftNew_RFQ_Details.SetCellValue("H" + id_counter_for_excel_RFQ_Details.ToString(), entity.StatAll);


                                    id_counter_for_excel_RFQ_Details++;
                                }
                            }
                        }


                        fsNew_RFQ_Details.Close();
                        draftNew_RFQ_Details.SaveAs(pathNew_RFQ_Destination);
                    }


                    Response.Redirect("Reporting/USER_REPORTS_DUMP/" + pathNew_RFQ_FileName, false);

                }

                if (ddItemStatus.SelectedItem.Text == "FOR SENDING")
                {


                    int id_counter_for_excel_RFQ_SENDING = 4;
                    string pathNew_RFQ_SENDING = Server.MapPath("~/Reporting/RFQ_SENDING.xlsx");
                    string pathNew_RFQ_SENDING_FileName = Session["LcRefId"].ToString() + "_RFQ_SENDING_" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Replace("/", "").Replace(":", "").Replace(" ", "").Trim() + ".xlsx";
                    string pathNew_RFQ_SENDING_Destination = System.IO.Path.Combine(Server.MapPath("~/Reporting/USER_REPORTS_DUMP/"), pathNew_RFQ_SENDING_FileName);

                    //System.IO.File.Create(pathNew_RFQ_SENDING_Destination);
                    System.IO.File.Copy(pathNew_RFQ_SENDING, pathNew_RFQ_SENDING_Destination, true);                    


                    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    //DETAILS

                    List<Entities_RFQ_RequestEntry> listDetails = new List<Entities_RFQ_RequestEntry>();
                    Entities_RFQ_RequestEntry status = new Entities_RFQ_RequestEntry();

                    status.DrFrom = txtFrom.Text.Trim();
                    status.DrTo = txtTo.Text.Trim();

                    //DETAILS
                    listDetails = BLL.RFQ_TRANSACTION_Reporting_ByDateRange_Details(status);

                    int id_counter_for_excel_RFQ_SENDING_Details = 2;
                    string pathNew_RFQ_SENDING_Details = Server.MapPath("~/Reporting/RFQ.xlsx");
                    Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew_RFQ_SENDING_Destination);
                    FileStream fsNew_RFQ_SENDING_Details = new FileStream(pathNew_RFQ_SENDING_Destination, FileMode.Open);
                    using (SLDocument draftNew_RFQ_SENDING_Details = new SLDocument(fsNew_RFQ_SENDING_Details, "DETAILS"))
                    {

                        // CLEAN UP FIRST THE EXCEL FILE
                        for (int i = 2; i <= 10000; i++)
                        {
                            draftNew_RFQ_SENDING_Details.SetCellValue("A" + i.ToString(), string.Empty);
                            draftNew_RFQ_SENDING_Details.SetCellValue("B" + i.ToString(), string.Empty);
                            draftNew_RFQ_SENDING_Details.SetCellValue("C" + i.ToString(), string.Empty);
                            draftNew_RFQ_SENDING_Details.SetCellValue("D" + i.ToString(), string.Empty);
                            draftNew_RFQ_SENDING_Details.SetCellValue("E" + i.ToString(), string.Empty);
                            draftNew_RFQ_SENDING_Details.SetCellValue("F" + i.ToString(), string.Empty);
                            draftNew_RFQ_SENDING_Details.SetCellValue("G" + i.ToString(), string.Empty);
                            draftNew_RFQ_SENDING_Details.SetCellValue("H" + i.ToString(), string.Empty);


                        }

                        if (gvDataDetails.Rows.Count > 0)
                        {

                            for (int i = 0; i < gvDataDetails.Rows.Count; i++)
                            {
                                Label lblRFQNo = (Label)gvDataDetails.Rows[i].Cells[0].FindControl("lblRFQNo");
                                Label lblDescription = (Label)gvDataDetails.Rows[i].Cells[1].FindControl("lblDescription");
                                Label lblSpecification = (Label)gvDataDetails.Rows[i].Cells[2].FindControl("lblSpecification");
                                Label lblMaker = (Label)gvDataDetails.Rows[i].Cells[3].FindControl("lblMaker");
                                Label lblCategory = (Label)gvDataDetails.Rows[i].Cells[4].FindControl("lblCategory");
                                Label lblRequester = (Label)gvDataDetails.Rows[i].Cells[5].FindControl("lblRequester");
                                Label lblTransactionDate = (Label)gvDataDetails.Rows[i].Cells[6].FindControl("lblTransactionDate");
                                Label lblStatus = (Label)gvDataDetails.Rows[i].Cells[7].FindControl("lblStatus");

                                draftNew_RFQ_SENDING_Details.SetCellValue("A" + id_counter_for_excel_RFQ_SENDING_Details.ToString(), lblRFQNo.Text.ToUpper());
                                draftNew_RFQ_SENDING_Details.SetCellValue("B" + id_counter_for_excel_RFQ_SENDING_Details.ToString(), lblDescription.Text);
                                draftNew_RFQ_SENDING_Details.SetCellValue("C" + id_counter_for_excel_RFQ_SENDING_Details.ToString(), lblSpecification.Text);
                                draftNew_RFQ_SENDING_Details.SetCellValue("D" + id_counter_for_excel_RFQ_SENDING_Details.ToString(), lblMaker.Text);
                                draftNew_RFQ_SENDING_Details.SetCellValue("E" + id_counter_for_excel_RFQ_SENDING_Details.ToString(), lblCategory.Text.ToUpper());
                                draftNew_RFQ_SENDING_Details.SetCellValue("F" + id_counter_for_excel_RFQ_SENDING_Details.ToString(), lblRequester.Text.ToUpper());
                                draftNew_RFQ_SENDING_Details.SetCellValue("G" + id_counter_for_excel_RFQ_SENDING_Details.ToString(), lblTransactionDate.Text);
                                draftNew_RFQ_SENDING_Details.SetCellValue("H" + id_counter_for_excel_RFQ_SENDING_Details.ToString(), lblStatus.Text.ToUpper());


                                id_counter_for_excel_RFQ_SENDING_Details++;
                            }
                        }


                        fsNew_RFQ_SENDING_Details.Close();
                        draftNew_RFQ_SENDING_Details.SaveAs(pathNew_RFQ_SENDING_Destination);
                    }


                    Response.Redirect("Reporting/USER_REPORTS_DUMP/" + pathNew_RFQ_SENDING_FileName, false);

                }


                if (ddItemStatus.SelectedItem.Text == "FOR RESEND")
                {


                    int id_counter_for_excel_RFQ_RESEND = 4;
                    string pathNew_RFQ_RESEND = Server.MapPath("~/Reporting/RFQ_RESEND.xlsx");
                    string pathNew_RFQ_RESEND_FileName = Session["LcRefId"].ToString() + "_RFQ_RESEND_" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Replace("/", "").Replace(":", "").Replace(" ", "").Trim() + ".xlsx";
                    string pathNew_RFQ_RESEND_Destination = System.IO.Path.Combine(Server.MapPath("~/Reporting/USER_REPORTS_DUMP/"), pathNew_RFQ_RESEND_FileName);

                    //System.IO.File.Create(pathNew_RFQ_RESEND_Destination);
                    System.IO.File.Copy(pathNew_RFQ_RESEND, pathNew_RFQ_RESEND_Destination, true);


                    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    //DETAILS

                    List<Entities_RFQ_RequestEntry> listDetails = new List<Entities_RFQ_RequestEntry>();
                    Entities_RFQ_RequestEntry status = new Entities_RFQ_RequestEntry();

                    status.DrFrom = txtFrom.Text.Trim();
                    status.DrTo = txtTo.Text.Trim();

                    //DETAILS
                    listDetails = BLL.RFQ_TRANSACTION_Reporting_ByDateRange_Details(status);

                    int id_counter_for_excel_RFQ_RESEND_Details = 2;
                    string pathNew_RFQ_RESEND_Details = Server.MapPath("~/Reporting/RFQ.xlsx");
                    Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew_RFQ_RESEND_Destination);
                    FileStream fsNew_RFQ_RESEND_Details = new FileStream(pathNew_RFQ_RESEND_Destination, FileMode.Open);
                    using (SLDocument draftNew_RFQ_RESEND_Details = new SLDocument(fsNew_RFQ_RESEND_Details, "DETAILS"))
                    {

                        // CLEAN UP FIRST THE EXCEL FILE
                        for (int i = 2; i <= 10000; i++)
                        {
                            draftNew_RFQ_RESEND_Details.SetCellValue("A" + i.ToString(), string.Empty);
                            draftNew_RFQ_RESEND_Details.SetCellValue("B" + i.ToString(), string.Empty);
                            draftNew_RFQ_RESEND_Details.SetCellValue("C" + i.ToString(), string.Empty);
                            draftNew_RFQ_RESEND_Details.SetCellValue("D" + i.ToString(), string.Empty);
                            draftNew_RFQ_RESEND_Details.SetCellValue("E" + i.ToString(), string.Empty);
                            draftNew_RFQ_RESEND_Details.SetCellValue("F" + i.ToString(), string.Empty);
                            draftNew_RFQ_RESEND_Details.SetCellValue("G" + i.ToString(), string.Empty);
                            draftNew_RFQ_RESEND_Details.SetCellValue("H" + i.ToString(), string.Empty);


                        }

                        if (gvDataDetails.Rows.Count > 0)
                        {

                            for (int i = 0; i < gvDataDetails.Rows.Count; i++)
                            {
                                Label lblRFQNo = (Label)gvDataDetails.Rows[i].Cells[0].FindControl("lblRFQNo");
                                Label lblDescription = (Label)gvDataDetails.Rows[i].Cells[1].FindControl("lblDescription");
                                Label lblSpecification = (Label)gvDataDetails.Rows[i].Cells[2].FindControl("lblSpecification");
                                Label lblMaker = (Label)gvDataDetails.Rows[i].Cells[3].FindControl("lblMaker");
                                Label lblCategory = (Label)gvDataDetails.Rows[i].Cells[4].FindControl("lblCategory");
                                Label lblRequester = (Label)gvDataDetails.Rows[i].Cells[5].FindControl("lblRequester");
                                Label lblTransactionDate = (Label)gvDataDetails.Rows[i].Cells[6].FindControl("lblTransactionDate");
                                Label lblStatus = (Label)gvDataDetails.Rows[i].Cells[7].FindControl("lblStatus");

                                draftNew_RFQ_RESEND_Details.SetCellValue("A" + id_counter_for_excel_RFQ_RESEND_Details.ToString(), lblRFQNo.Text.ToUpper());
                                draftNew_RFQ_RESEND_Details.SetCellValue("B" + id_counter_for_excel_RFQ_RESEND_Details.ToString(), lblDescription.Text);
                                draftNew_RFQ_RESEND_Details.SetCellValue("C" + id_counter_for_excel_RFQ_RESEND_Details.ToString(), lblSpecification.Text);
                                draftNew_RFQ_RESEND_Details.SetCellValue("D" + id_counter_for_excel_RFQ_RESEND_Details.ToString(), lblMaker.Text);
                                draftNew_RFQ_RESEND_Details.SetCellValue("E" + id_counter_for_excel_RFQ_RESEND_Details.ToString(), lblCategory.Text.ToUpper());
                                draftNew_RFQ_RESEND_Details.SetCellValue("F" + id_counter_for_excel_RFQ_RESEND_Details.ToString(), lblRequester.Text.ToUpper());
                                draftNew_RFQ_RESEND_Details.SetCellValue("G" + id_counter_for_excel_RFQ_RESEND_Details.ToString(), lblTransactionDate.Text);
                                draftNew_RFQ_RESEND_Details.SetCellValue("H" + id_counter_for_excel_RFQ_RESEND_Details.ToString(), lblStatus.Text.ToUpper());


                                id_counter_for_excel_RFQ_RESEND_Details++;
                            }
                        }


                        fsNew_RFQ_RESEND_Details.Close();
                        draftNew_RFQ_RESEND_Details.SaveAs(pathNew_RFQ_RESEND_Destination);
                    }


                    Response.Redirect("Reporting/USER_REPORTS_DUMP/" + pathNew_RFQ_RESEND_FileName, false);

                }

                if (ddItemStatus.SelectedItem.Text == "ALL")
                {


                    int id_counter_for_excel_RFQ_ALL = 4;
                    string pathNew_RFQ_ALL = Server.MapPath("~/Reporting/RFQ_ALL.xlsx");
                    string pathNew_RFQ_ALL_FileName = Session["LcRefId"].ToString() + "_RFQ_ALL_" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Replace("/", "").Replace(":", "").Replace(" ", "").Trim() + ".xlsx";
                    string pathNew_RFQ_ALL_Destination = System.IO.Path.Combine(Server.MapPath("~/Reporting/USER_REPORTS_DUMP/"), pathNew_RFQ_ALL_FileName);

                    //System.IO.File.Create(pathNew_RFQ_ALL_Destination);
                    System.IO.File.Copy(pathNew_RFQ_ALL, pathNew_RFQ_ALL_Destination, true);


                    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    //DETAILS

                    List<Entities_RFQ_RequestEntry> listDetails = new List<Entities_RFQ_RequestEntry>();
                    Entities_RFQ_RequestEntry status = new Entities_RFQ_RequestEntry();

                    status.DrFrom = txtFrom.Text.Trim();
                    status.DrTo = txtTo.Text.Trim();

                    //DETAILS
                    listDetails = BLL.RFQ_TRANSACTION_Reporting_ByDateRange_Details(status);

                    int id_counter_for_excel_RFQ_ALL_Details = 2;
                    string pathNew_RFQ_ALL_Details = Server.MapPath("~/Reporting/RFQ.xlsx");
                    Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew_RFQ_ALL_Destination);
                    FileStream fsNew_RFQ_ALL_Details = new FileStream(pathNew_RFQ_ALL_Destination, FileMode.Open);
                    using (SLDocument draftNew_RFQ_ALL_Details = new SLDocument(fsNew_RFQ_ALL_Details, "DETAILS"))
                    {

                        // CLEAN UP FIRST THE EXCEL FILE
                        for (int i = 2; i <= 10000; i++)
                        {
                            draftNew_RFQ_ALL_Details.SetCellValue("A" + i.ToString(), string.Empty);
                            draftNew_RFQ_ALL_Details.SetCellValue("B" + i.ToString(), string.Empty);
                            draftNew_RFQ_ALL_Details.SetCellValue("C" + i.ToString(), string.Empty);
                            draftNew_RFQ_ALL_Details.SetCellValue("D" + i.ToString(), string.Empty);
                            draftNew_RFQ_ALL_Details.SetCellValue("E" + i.ToString(), string.Empty);
                            draftNew_RFQ_ALL_Details.SetCellValue("F" + i.ToString(), string.Empty);
                            draftNew_RFQ_ALL_Details.SetCellValue("G" + i.ToString(), string.Empty);
                            draftNew_RFQ_ALL_Details.SetCellValue("H" + i.ToString(), string.Empty);


                        }

                        if (gvDataDetails.Rows.Count > 0)
                        {

                            for (int i = 0; i < gvDataDetails.Rows.Count; i++)
                            {
                                Label lblRFQNo = (Label)gvDataDetails.Rows[i].Cells[0].FindControl("lblRFQNo");
                                Label lblDescription = (Label)gvDataDetails.Rows[i].Cells[1].FindControl("lblDescription");
                                Label lblSpecification = (Label)gvDataDetails.Rows[i].Cells[2].FindControl("lblSpecification");
                                Label lblMaker = (Label)gvDataDetails.Rows[i].Cells[3].FindControl("lblMaker");
                                Label lblCategory = (Label)gvDataDetails.Rows[i].Cells[4].FindControl("lblCategory");
                                Label lblRequester = (Label)gvDataDetails.Rows[i].Cells[5].FindControl("lblRequester");
                                Label lblTransactionDate = (Label)gvDataDetails.Rows[i].Cells[6].FindControl("lblTransactionDate");
                                Label lblStatus = (Label)gvDataDetails.Rows[i].Cells[7].FindControl("lblStatus");

                                draftNew_RFQ_ALL_Details.SetCellValue("A" + id_counter_for_excel_RFQ_ALL_Details.ToString(), lblRFQNo.Text.ToUpper());
                                draftNew_RFQ_ALL_Details.SetCellValue("B" + id_counter_for_excel_RFQ_ALL_Details.ToString(), lblDescription.Text);
                                draftNew_RFQ_ALL_Details.SetCellValue("C" + id_counter_for_excel_RFQ_ALL_Details.ToString(), lblSpecification.Text);
                                draftNew_RFQ_ALL_Details.SetCellValue("D" + id_counter_for_excel_RFQ_ALL_Details.ToString(), lblMaker.Text);
                                draftNew_RFQ_ALL_Details.SetCellValue("E" + id_counter_for_excel_RFQ_ALL_Details.ToString(), lblCategory.Text.ToUpper());
                                draftNew_RFQ_ALL_Details.SetCellValue("F" + id_counter_for_excel_RFQ_ALL_Details.ToString(), lblRequester.Text.ToUpper());
                                draftNew_RFQ_ALL_Details.SetCellValue("G" + id_counter_for_excel_RFQ_ALL_Details.ToString(), lblTransactionDate.Text);
                                draftNew_RFQ_ALL_Details.SetCellValue("H" + id_counter_for_excel_RFQ_ALL_Details.ToString(), lblStatus.Text.ToUpper());


                                id_counter_for_excel_RFQ_ALL_Details++;
                            }
                        }


                        fsNew_RFQ_ALL_Details.Close();
                        draftNew_RFQ_ALL_Details.SaveAs(pathNew_RFQ_ALL_Destination);
                    }


                    Response.Redirect("Reporting/USER_REPORTS_DUMP/" + pathNew_RFQ_ALL_FileName, false);

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
