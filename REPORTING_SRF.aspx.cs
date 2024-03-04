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
    public partial class REPORTING_SRF : System.Web.UI.Page
    {
        BLL_SRF BLL = new BLL_SRF();
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
                List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
                List<Entities_SRF_RequestEntry> listDivision = new List<Entities_SRF_RequestEntry>();
                List<Entities_SRF_RequestEntry> listAll = new List<Entities_SRF_RequestEntry>();
                Entities_SRF_RequestEntry status = new Entities_SRF_RequestEntry();

                status.DrFrom = txtFrom.Text.Trim();
                status.DrTo = txtTo.Text.Trim();

                //DEPARTMENT
                list = BLL.SRF_TRANSACTION_Reporting_ByDateRange(status);

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
                listDivision = BLL.SRF_TRANSACTION_Reporting_ByDateRange_ByDivision(status);

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
                listAll = BLL.SRF_TRANSACTION_Reporting_ByDateRange_ByAll(status);

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

                int id_counter_for_excel_SRF = 4;
                string pathNew_SRF = Server.MapPath("~/Reporting/SRF.xlsx");
                string pathNew_SRF_FileName = Session["LcRefId"].ToString() + "_SRF_" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Replace("/", "").Replace(":", "").Replace(" ", "").Trim() + ".xlsx";
                string pathNew_SRF_Destination = System.IO.Path.Combine(Server.MapPath("~/Reporting/USER_REPORTS_DUMP/"), pathNew_SRF_FileName);

                //System.IO.File.Create(pathNew_SRF_Destination);
                System.IO.File.Copy(pathNew_SRF, pathNew_SRF_Destination, true);

                Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew_SRF_Destination);
                FileStream fsNew_SRF = new FileStream(pathNew_SRF_Destination, FileMode.Open);
                using (SLDocument draftNew_SRF = new SLDocument(fsNew_SRF, "SUMMARY"))
                {

                    // CLEAN UP FIRST THE EXCEL FILE
                    for (int i = 1; i <= 5000; i++)
                    {
                        draftNew_SRF.SetCellValue("A" + i.ToString(), string.Empty);
                        draftNew_SRF.SetCellValue("B" + i.ToString(), string.Empty);
                        draftNew_SRF.SetCellValue("C" + i.ToString(), string.Empty);
                        draftNew_SRF.SetCellValue("D" + i.ToString(), string.Empty);
                        draftNew_SRF.SetCellValue("E" + i.ToString(), string.Empty);
                        draftNew_SRF.SetCellValue("F" + i.ToString(), string.Empty);
                        draftNew_SRF.SetCellValue("G" + i.ToString(), string.Empty);
                        draftNew_SRF.SetCellValue("H" + i.ToString(), string.Empty);
                        draftNew_SRF.SetCellValue("I" + i.ToString(), string.Empty);
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

                        draftNew_SRF.SetCellValue("A1", "COUNTS BY DEPARTMENT");
                        draftNew_SRF.SetCellValue("A3", "NO");
                        draftNew_SRF.SetCellValue("B3", "DEPARTMENT NAME");
                        draftNew_SRF.SetCellValue("C3", "NUMBER OF REQUEST");
                        draftNew_SRF.SetCellValue("D3", "BUYER-APPROVED");
                        draftNew_SRF.SetCellValue("E3", "BUYER-DISAPPROVED");
                        draftNew_SRF.SetCellValue("F3", "SC-MNGR-APPROVED");
                        draftNew_SRF.SetCellValue("G3", "SC-MNGR-DISAPPROVED");
                        draftNew_SRF.SetCellValue("H3", "POSTED/CLOSED");
                        draftNew_SRF.SetCellValue("I3", "PENDING APPROVAL");
                        draftNew_SRF.SetCellValue("A" + id_counter_for_excel_SRF.ToString(), lblCounter.Text.ToUpper());
                        draftNew_SRF.SetCellValue("B" + id_counter_for_excel_SRF.ToString(), lblDepartmentName.Text.ToUpper());
                        draftNew_SRF.SetCellValue("C" + id_counter_for_excel_SRF.ToString(), lblNumberOfRequest.Text.ToUpper());
                        draftNew_SRF.SetCellValue("D" + id_counter_for_excel_SRF.ToString(), lblBuyerApproved.Text.ToUpper());
                        draftNew_SRF.SetCellValue("E" + id_counter_for_excel_SRF.ToString(), lblBuyerDisapproved.Text.ToUpper());
                        draftNew_SRF.SetCellValue("F" + id_counter_for_excel_SRF.ToString(), lblSCManagerApproved.Text.ToUpper());
                        draftNew_SRF.SetCellValue("G" + id_counter_for_excel_SRF.ToString(), lblSCManagerDisapproved.Text.ToUpper());
                        draftNew_SRF.SetCellValue("H" + id_counter_for_excel_SRF.ToString(), lblPosted.Text.ToUpper());
                        draftNew_SRF.SetCellValue("I" + id_counter_for_excel_SRF.ToString(), lblPendingApproval.Text.ToUpper());

                        id_counter_for_excel_SRF++;
                    }

                    // DIVISION
                    id_counter_for_excel_SRF++;
                    id_counter_for_excel_SRF++;

                    draftNew_SRF.SetCellValue("A" + id_counter_for_excel_SRF.ToString(), "COUNTS BY DIVISION");

                    id_counter_for_excel_SRF++;
                    id_counter_for_excel_SRF++;

                    draftNew_SRF.SetCellValue("A" + id_counter_for_excel_SRF.ToString(), "COUNTS BY DEPARTMENT");
                    draftNew_SRF.SetCellValue("A" + id_counter_for_excel_SRF.ToString(), "NO");
                    draftNew_SRF.SetCellValue("B" + id_counter_for_excel_SRF.ToString(), "DEPARTMENT NAME");
                    draftNew_SRF.SetCellValue("C" + id_counter_for_excel_SRF.ToString(), "NUMBER OF REQUEST");
                    draftNew_SRF.SetCellValue("D" + id_counter_for_excel_SRF.ToString(), "BUYER-APPROVED");
                    draftNew_SRF.SetCellValue("E" + id_counter_for_excel_SRF.ToString(), "BUYER-DISAPPROVED");
                    draftNew_SRF.SetCellValue("F" + id_counter_for_excel_SRF.ToString(), "SC-MNGR-APPROVED");
                    draftNew_SRF.SetCellValue("G" + id_counter_for_excel_SRF.ToString(), "SC-MNGR-DISAPPROVED");
                    draftNew_SRF.SetCellValue("H" + id_counter_for_excel_SRF.ToString(), "POSTED/CLOSED");
                    draftNew_SRF.SetCellValue("I" + id_counter_for_excel_SRF.ToString(), "PENDING APPROVAL");

                    id_counter_for_excel_SRF++;

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

                        draftNew_SRF.SetCellValue("A" + id_counter_for_excel_SRF.ToString(), lblCounter.Text.ToUpper());
                        draftNew_SRF.SetCellValue("B" + id_counter_for_excel_SRF.ToString(), lblDivisionName.Text.ToUpper());
                        draftNew_SRF.SetCellValue("C" + id_counter_for_excel_SRF.ToString(), lblNumberOfRequest.Text.ToUpper());
                        draftNew_SRF.SetCellValue("D" + id_counter_for_excel_SRF.ToString(), lblBuyerApproved.Text.ToUpper());
                        draftNew_SRF.SetCellValue("E" + id_counter_for_excel_SRF.ToString(), lblBuyerDisapproved.Text.ToUpper());
                        draftNew_SRF.SetCellValue("F" + id_counter_for_excel_SRF.ToString(), lblSCManagerApproved.Text.ToUpper());
                        draftNew_SRF.SetCellValue("G" + id_counter_for_excel_SRF.ToString(), lblSCManagerDisapproved.Text.ToUpper());
                        draftNew_SRF.SetCellValue("H" + id_counter_for_excel_SRF.ToString(), lblPosted.Text.ToUpper());
                        draftNew_SRF.SetCellValue("I" + id_counter_for_excel_SRF.ToString(), lblPendingApproval.Text.ToUpper());


                        id_counter_for_excel_SRF++;
                    }

                    // ALL
                    id_counter_for_excel_SRF++;
                    id_counter_for_excel_SRF++;

                    draftNew_SRF.SetCellValue("A" + id_counter_for_excel_SRF.ToString(), "TOTAL COUNTS");

                    id_counter_for_excel_SRF++;
                    id_counter_for_excel_SRF++;

                    draftNew_SRF.SetCellValue("A" + id_counter_for_excel_SRF.ToString(), "NO");
                    draftNew_SRF.SetCellValue("B" + id_counter_for_excel_SRF.ToString(), "NUMBER OF REQUEST");
                    draftNew_SRF.SetCellValue("C" + id_counter_for_excel_SRF.ToString(), "BUYER-APPROVED");
                    draftNew_SRF.SetCellValue("D" + id_counter_for_excel_SRF.ToString(), "BUYER-DISAPPROVED");
                    draftNew_SRF.SetCellValue("E" + id_counter_for_excel_SRF.ToString(), "SC-MNGR-APPROVED");
                    draftNew_SRF.SetCellValue("F" + id_counter_for_excel_SRF.ToString(), "SC-MNGR-DISAPPROVED");
                    draftNew_SRF.SetCellValue("G" + id_counter_for_excel_SRF.ToString(), "PENDING APPROVAL");
                    draftNew_SRF.SetCellValue("H" + id_counter_for_excel_SRF.ToString(), "TOTAL POSTED/CLOSED");
                    draftNew_SRF.SetCellValue("I" + id_counter_for_excel_SRF.ToString(), "TOTAL DISAPPROVED");

                    id_counter_for_excel_SRF++;

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

                        draftNew_SRF.SetCellValue("A" + id_counter_for_excel_SRF.ToString(), lblCounter.Text.ToUpper());
                        draftNew_SRF.SetCellValue("B" + id_counter_for_excel_SRF.ToString(), lblNumberOfRequest.Text.ToUpper());
                        draftNew_SRF.SetCellValue("C" + id_counter_for_excel_SRF.ToString(), lblBuyerApproved.Text.ToUpper());
                        draftNew_SRF.SetCellValue("D" + id_counter_for_excel_SRF.ToString(), lblBuyerDisapproved.Text.ToUpper());
                        draftNew_SRF.SetCellValue("E" + id_counter_for_excel_SRF.ToString(), lblSCManagerApproved.Text.ToUpper());
                        draftNew_SRF.SetCellValue("F" + id_counter_for_excel_SRF.ToString(), lblSCManagerDisapproved.Text.ToUpper());
                        draftNew_SRF.SetCellValue("G" + id_counter_for_excel_SRF.ToString(), lblPendingApproval.Text.ToUpper());
                        draftNew_SRF.SetCellValue("H" + id_counter_for_excel_SRF.ToString(), lblTotalPosted.Text.ToUpper());
                        draftNew_SRF.SetCellValue("I" + id_counter_for_excel_SRF.ToString(), lblTotalDisapproved.Text.ToUpper());


                        id_counter_for_excel_SRF++;
                    }


                    fsNew_SRF.Close();
                    draftNew_SRF.SaveAs(pathNew_SRF);
                }


                //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //DETAILS

                List<Entities_SRF_RequestEntry> listDetails = new List<Entities_SRF_RequestEntry>();
                Entities_SRF_RequestEntry status = new Entities_SRF_RequestEntry();

                status.DrFrom = txtFrom.Text.Trim();
                status.DrTo = txtTo.Text.Trim();

                //DETAILS
                listDetails = BLL.SRF_TRANSACTION_Reporting_ByDateRange_Details(status);

                int id_counter_for_excel_SRF_Details = 2;
                string pathNew_SRF_Details = Server.MapPath("~/Reporting/SRF.xlsx");
                Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew_SRF_Destination);
                FileStream fsNew_SRF_Details = new FileStream(pathNew_SRF_Destination, FileMode.Open);
                using (SLDocument draftNew_SRF_Details = new SLDocument(fsNew_SRF_Details, "DETAILS"))
                {

                    // CLEAN UP FIRST THE EXCEL FILE
                    for (int i = 1; i <= 10000; i++)
                    {
                        draftNew_SRF_Details.SetCellValue("A" + i.ToString(), string.Empty);
                        draftNew_SRF_Details.SetCellValue("B" + i.ToString(), string.Empty);
                        draftNew_SRF_Details.SetCellValue("C" + i.ToString(), string.Empty);
                        draftNew_SRF_Details.SetCellValue("D" + i.ToString(), string.Empty);
                        draftNew_SRF_Details.SetCellValue("E" + i.ToString(), string.Empty);
                        draftNew_SRF_Details.SetCellValue("F" + i.ToString(), string.Empty);
                        draftNew_SRF_Details.SetCellValue("G" + i.ToString(), string.Empty);
                        draftNew_SRF_Details.SetCellValue("H" + i.ToString(), string.Empty);
                        draftNew_SRF_Details.SetCellValue("I" + i.ToString(), string.Empty);
                        draftNew_SRF_Details.SetCellValue("J" + i.ToString(), string.Empty);
                        draftNew_SRF_Details.SetCellValue("K" + i.ToString(), string.Empty);

                    }

                    if (listDetails != null)
                    {
                        if (listDetails.Count > 0)
                        {
                            draftNew_SRF_Details.SetCellValue("A1", "CTRLNO");
                            draftNew_SRF_Details.SetCellValue("B1", "CATEGORY");
                            draftNew_SRF_Details.SetCellValue("C1", "REFERENCE PR/PO");
                            draftNew_SRF_Details.SetCellValue("D1", "SALES INVOICE");
                            draftNew_SRF_Details.SetCellValue("E1", "BRAND MACHINE NAME");
                            draftNew_SRF_Details.SetCellValue("F1", "ITEM NAME");
                            draftNew_SRF_Details.SetCellValue("G1", "ITEM SPECIFICATION");
                            draftNew_SRF_Details.SetCellValue("H1", "REQUESTER");
                            draftNew_SRF_Details.SetCellValue("I1", "REQUESTED DATE");
                            draftNew_SRF_Details.SetCellValue("J1", "STATUS");
                            draftNew_SRF_Details.SetCellValue("K1", "TRANSACTION DATE");


                            foreach (Entities_SRF_RequestEntry entity in listDetails)
                            {

                                draftNew_SRF_Details.SetCellValue("A" + id_counter_for_excel_SRF_Details.ToString(), entity.CtrlNo);
                                draftNew_SRF_Details.SetCellValue("B" + id_counter_for_excel_SRF_Details.ToString(), entity.Category);
                                draftNew_SRF_Details.SetCellValue("C" + id_counter_for_excel_SRF_Details.ToString(), entity.RefPRPO);
                                draftNew_SRF_Details.SetCellValue("D" + id_counter_for_excel_SRF_Details.ToString(), entity.SalesInvoice);
                                draftNew_SRF_Details.SetCellValue("E" + id_counter_for_excel_SRF_Details.ToString(), entity.BrandMachineName);
                                draftNew_SRF_Details.SetCellValue("F" + id_counter_for_excel_SRF_Details.ToString(), entity.ItemName);
                                draftNew_SRF_Details.SetCellValue("G" + id_counter_for_excel_SRF_Details.ToString(), entity.Specification);
                                draftNew_SRF_Details.SetCellValue("H" + id_counter_for_excel_SRF_Details.ToString(), entity.Requester);
                                draftNew_SRF_Details.SetCellValue("I" + id_counter_for_excel_SRF_Details.ToString(), entity.TransactionDate);
                                draftNew_SRF_Details.SetCellValue("J" + id_counter_for_excel_SRF_Details.ToString(), entity.StatAll);
                                draftNew_SRF_Details.SetCellValue("K" + id_counter_for_excel_SRF_Details.ToString(), entity.TransactionDate);


                                id_counter_for_excel_SRF_Details++;
                            }
                        }
                    }


                    fsNew_SRF_Details.Close();
                    draftNew_SRF_Details.SaveAs(pathNew_SRF_Destination);
                }



                Response.Redirect("Reporting/USER_REPORTS_DUMP/" + pathNew_SRF_FileName, false);

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
