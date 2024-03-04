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
    public partial class SRF_PO_Reporting : System.Web.UI.Page
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
                    btnSubmit_Click(sender, e);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                }
            }
        }


        protected void ddPullOutType_Change(object sender, EventArgs e)
        {
            try
            {

                // CONTAINER TUBES
                if (ddPullOutType.SelectedItem.Text == "CONTAINER TUBES")
                {
                    divICTrays.Style.Add("display", "none");
                    divContainerTube.Style.Add("display", "block");
                    divOthers.Style.Add("display", "none");

                    gvICTrays.DataSource = null;
                    gvICTrays.DataBind();

                    gvContainerTube.DataSource = null;
                    gvContainerTube.DataBind();

                    gvOthers.DataSource = null;
                    gvOthers.DataBind();
                }

                // IC TRAYS
                if (ddPullOutType.SelectedItem.Text == "IC TRAYS")
                {
                    divContainerTube.Style.Add("display", "none");
                    divICTrays.Style.Add("display", "block");
                    divOthers.Style.Add("display", "none");

                    gvICTrays.DataSource = null;
                    gvICTrays.DataBind();

                    gvContainerTube.DataSource = null;
                    gvContainerTube.DataBind();

                    gvOthers.DataSource = null;
                    gvOthers.DataBind();
                }

                // NONE
                if (string.IsNullOrEmpty(ddPullOutType.SelectedItem.Text))
                {
                    divContainerTube.Style.Add("display", "none");
                    divICTrays.Style.Add("display", "none");
                    divOthers.Style.Add("display", "none");

                    gvICTrays.DataSource = null;
                    gvICTrays.DataBind();

                    gvContainerTube.DataSource = null;
                    gvContainerTube.DataBind();

                    gvOthers.DataSource = null;
                    gvOthers.DataBind();
                }

                // OTHERS
                if (ddPullOutType.SelectedItem.Text == "EMPTY BLACK TRAY" || ddPullOutType.SelectedItem.Text == "DANPLA BOX" || ddPullOutType.SelectedItem.Text == "ROBUST REEL")
                {
                    divContainerTube.Style.Add("display", "none");
                    divICTrays.Style.Add("display", "none");
                    divOthers.Style.Add("display", "block");

                    gvICTrays.DataSource = null;
                    gvICTrays.DataBind();

                    gvContainerTube.DataSource = null;
                    gvContainerTube.DataBind();

                    gvOthers.DataSource = null;
                    gvOthers.DataBind();
                }


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
                if (ddPullOutType.SelectedItem.Text == "IC TRAYS")
                {
                    //--------------------------------------------------------------------------------------------------------------------------------------

                    if (gvICTrays.Rows.Count > 0)
                    {

                        int id_counter_for_excel = 4;
                        string pathNew = Server.MapPath("~/SRF_PO_XLS/IC_Tray_Report_Draft.xlsx");
                        Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew);
                        FileStream fsNew = new FileStream(pathNew, FileMode.Open);
                        using (SLDocument draftNew = new SLDocument(fsNew, "Sheet1"))
                        {

                            // CLEAN UP FIRST THE EXCEL FILE
                            for (int i = 4; i <= 5000; i++)
                            {
                                draftNew.SetCellValue("A" + i.ToString(), string.Empty);
                                draftNew.SetCellValue("B" + i.ToString(), string.Empty);
                                draftNew.SetCellValue("C" + i.ToString(), string.Empty);
                                draftNew.SetCellValue("D" + i.ToString(), string.Empty);
                                draftNew.SetCellValue("E" + i.ToString(), string.Empty);
                                draftNew.SetCellValue("F" + i.ToString(), string.Empty);
                                draftNew.SetCellValue("G" + i.ToString(), string.Empty);
                                draftNew.SetCellValue("H" + i.ToString(), string.Empty);
                                draftNew.SetCellValue("I" + i.ToString(), string.Empty);
                                draftNew.SetCellValue("J" + i.ToString(), string.Empty);

                            }

                            for (int i = 0; i < gvICTrays.Rows.Count; i++)
                            {
                                Label lblId = (Label)gvICTrays.Rows[i].Cells[0].FindControl("lblId");
                                Label lblRefId = (Label)gvICTrays.Rows[i].Cells[0].FindControl("lblRefId");
                                Label lblCTRLNo = (Label)gvICTrays.Rows[i].Cells[1].FindControl("lblCTRLNo");
                                Label lblSpecification = (Label)gvICTrays.Rows[i].Cells[2].FindControl("lblSpecification");
                                Label lblBoxType = (Label)gvICTrays.Rows[i].Cells[3].FindControl("lblBoxType");
                                Label lblSize = (Label)gvICTrays.Rows[i].Cells[4].FindControl("lblSize");
                                Label lblQuantity = (Label)gvICTrays.Rows[i].Cells[7].FindControl("lblQuantity");
                                Label lblMultiplier = (Label)gvICTrays.Rows[i].Cells[6].FindControl("lblMultiplier");
                                Label lblRequester = (Label)gvICTrays.Rows[i].Cells[8].FindControl("lblRequester");
                                Label lblNoOfBoxes = (Label)gvICTrays.Rows[i].Cells[5].FindControl("lblNoOfBoxes");
                                Label lblTransactionDate = (Label)gvICTrays.Rows[i].Cells[9].FindControl("lblTransactionDate");

                                draftNew.SetCellValue("A" + id_counter_for_excel.ToString(), lblId.Text.ToUpper());
                                draftNew.SetCellValue("B" + id_counter_for_excel.ToString(), lblCTRLNo.Text.ToUpper());
                                draftNew.SetCellValue("C" + id_counter_for_excel.ToString(), lblSpecification.Text.ToUpper());
                                draftNew.SetCellValue("D" + id_counter_for_excel.ToString(), lblBoxType.Text.ToUpper());
                                draftNew.SetCellValue("E" + id_counter_for_excel.ToString(), lblSize.Text.ToUpper());
                                draftNew.SetCellValue("F" + id_counter_for_excel.ToString(), lblNoOfBoxes.Text.ToUpper());
                                draftNew.SetCellValue("G" + id_counter_for_excel.ToString(), lblMultiplier.Text.ToUpper());
                                draftNew.SetCellValue("H" + id_counter_for_excel.ToString(), lblQuantity.Text.ToUpper());
                                draftNew.SetCellValue("I" + id_counter_for_excel.ToString(), lblRequester.Text.ToUpper());
                                draftNew.SetCellValue("J" + id_counter_for_excel.ToString(), lblTransactionDate.Text.ToUpper());

                                id_counter_for_excel++;
                            }

                            fsNew.Close();
                            draftNew.SaveAs(pathNew);
                        }

                        Response.Redirect("SRF_PO_XLS/IC_Tray_Report_Draft.xlsx", false);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Record(s) not found!');", true);
                    }

                    //--------------------------------------------------------------------------------------------------------------------------------------
                }



                if (ddPullOutType.SelectedItem.Text == "CONTAINER TUBES")
                {
                    //--------------------------------------------------------------------------------------------------------------------------------------

                    if (gvContainerTube.Rows.Count > 0)
                    {

                        int id_counter_for_excel_ForContainerTube = 4;
                        string pathNew_ForContainerTube = Server.MapPath("~/SRF_PO_XLS/Container_Tube_Report_Draft.xlsx");
                        Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew_ForContainerTube);
                        FileStream fsNew_ForContainerTube = new FileStream(pathNew_ForContainerTube, FileMode.Open);
                        using (SLDocument draftNew_ForContainerTube = new SLDocument(fsNew_ForContainerTube, "Sheet1"))
                        {

                            // CLEAN UP FIRST THE EXCEL FILE
                            for (int i = 4; i <= 5000; i++)
                            {
                                draftNew_ForContainerTube.SetCellValue("A" + i.ToString(), string.Empty);
                                draftNew_ForContainerTube.SetCellValue("B" + i.ToString(), string.Empty);
                                draftNew_ForContainerTube.SetCellValue("C" + i.ToString(), string.Empty);
                                draftNew_ForContainerTube.SetCellValue("D" + i.ToString(), string.Empty);
                                draftNew_ForContainerTube.SetCellValue("E" + i.ToString(), string.Empty);
                                draftNew_ForContainerTube.SetCellValue("F" + i.ToString(), string.Empty);
                                draftNew_ForContainerTube.SetCellValue("G" + i.ToString(), string.Empty);
                                draftNew_ForContainerTube.SetCellValue("H" + i.ToString(), string.Empty);
                                draftNew_ForContainerTube.SetCellValue("I" + i.ToString(), string.Empty);
                                draftNew_ForContainerTube.SetCellValue("J" + i.ToString(), string.Empty);
                                draftNew_ForContainerTube.SetCellValue("K" + i.ToString(), string.Empty);

                            }

                            for (int i = 0; i < gvContainerTube.Rows.Count; i++)
                            {
                                Label lblId = (Label)gvContainerTube.Rows[i].Cells[0].FindControl("lblId");
                                Label lblRefId = (Label)gvContainerTube.Rows[i].Cells[0].FindControl("lblRefId");
                                Label lblCTRLNo = (Label)gvContainerTube.Rows[i].Cells[1].FindControl("lblCTRLNo");
                                Label lblSpecification = (Label)gvContainerTube.Rows[i].Cells[2].FindControl("lblSpecification");
                                Label lblWeightOfBox = (Label)gvContainerTube.Rows[i].Cells[3].FindControl("lblWeightOfBox");
                                Label lblNowOfBoxes = (Label)gvContainerTube.Rows[i].Cells[4].FindControl("lblNowOfBoxes");
                                Label lblGrossWeight = (Label)gvContainerTube.Rows[i].Cells[5].FindControl("lblGrossWeight");
                                Label lblNetWeight = (Label)gvContainerTube.Rows[i].Cells[6].FindControl("lblNetWeight");
                                Label lblMultiplier = (Label)gvContainerTube.Rows[i].Cells[7].FindControl("lblMultiplier");
                                Label lblQuantity = (Label)gvContainerTube.Rows[i].Cells[8].FindControl("lblQuantity");
                                Label lblRequester = (Label)gvContainerTube.Rows[i].Cells[9].FindControl("lblRequester");
                                Label lblTransactionDate = (Label)gvContainerTube.Rows[i].Cells[10].FindControl("lblTransactionDate");

                                draftNew_ForContainerTube.SetCellValue("A" + id_counter_for_excel_ForContainerTube.ToString(), lblId.Text.ToUpper());
                                draftNew_ForContainerTube.SetCellValue("B" + id_counter_for_excel_ForContainerTube.ToString(), lblCTRLNo.Text.ToUpper());
                                draftNew_ForContainerTube.SetCellValue("C" + id_counter_for_excel_ForContainerTube.ToString(), lblSpecification.Text.ToUpper());
                                draftNew_ForContainerTube.SetCellValue("D" + id_counter_for_excel_ForContainerTube.ToString(), lblWeightOfBox.Text.ToUpper());
                                draftNew_ForContainerTube.SetCellValue("E" + id_counter_for_excel_ForContainerTube.ToString(), lblNowOfBoxes.Text.ToUpper());
                                draftNew_ForContainerTube.SetCellValue("F" + id_counter_for_excel_ForContainerTube.ToString(), lblGrossWeight.Text.ToUpper());
                                draftNew_ForContainerTube.SetCellValue("G" + id_counter_for_excel_ForContainerTube.ToString(), lblNetWeight.Text.ToUpper());
                                draftNew_ForContainerTube.SetCellValue("H" + id_counter_for_excel_ForContainerTube.ToString(), lblMultiplier.Text.ToUpper());
                                draftNew_ForContainerTube.SetCellValue("I" + id_counter_for_excel_ForContainerTube.ToString(), lblQuantity.Text.ToUpper());
                                draftNew_ForContainerTube.SetCellValue("J" + id_counter_for_excel_ForContainerTube.ToString(), lblRequester.Text.ToUpper());
                                draftNew_ForContainerTube.SetCellValue("K" + id_counter_for_excel_ForContainerTube.ToString(), lblTransactionDate.Text.ToUpper());

                                id_counter_for_excel_ForContainerTube++;
                            }

                            fsNew_ForContainerTube.Close();
                            draftNew_ForContainerTube.SaveAs(pathNew_ForContainerTube);
                        }

                        Response.Redirect("SRF_PO_XLS/Container_Tube_Report_Draft.xlsx", false);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Record(s) not found!');", true);
                    }

                    //--------------------------------------------------------------------------------------------------------------------------------------
                }


                if (ddPullOutType.SelectedItem.Text == "EMPTY BLACK TRAY" || ddPullOutType.SelectedItem.Text == "DANPLA BOX" || ddPullOutType.SelectedItem.Text == "ROBUST REEL")
                {
                    //--------------------------------------------------------------------------------------------------------------------------------------

                    if (gvOthers.Rows.Count > 0)
                    {

                        int id_counter_for_excel_ForOthers = 4;
                        string pathNew_ForOthers = Server.MapPath("~/SRF_PO_XLS/Others_Report_Draft.xlsx");
                        Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew_ForOthers);
                        FileStream fsNew_ForOthers = new FileStream(pathNew_ForOthers, FileMode.Open);
                        using (SLDocument draftNew_ForOthers = new SLDocument(fsNew_ForOthers, "Sheet1"))
                        {

                            // CLEAN UP FIRST THE EXCEL FILE
                            for (int i = 4; i <= 5000; i++)
                            {
                                draftNew_ForOthers.SetCellValue("A" + i.ToString(), string.Empty);
                                draftNew_ForOthers.SetCellValue("B" + i.ToString(), string.Empty);
                                draftNew_ForOthers.SetCellValue("C" + i.ToString(), string.Empty);
                                draftNew_ForOthers.SetCellValue("D" + i.ToString(), string.Empty);
                                draftNew_ForOthers.SetCellValue("E" + i.ToString(), string.Empty);
                                draftNew_ForOthers.SetCellValue("F" + i.ToString(), string.Empty);
                                draftNew_ForOthers.SetCellValue("G" + i.ToString(), string.Empty);

                            }

                            for (int i = 0; i < gvOthers.Rows.Count; i++)
                            {
                                Label lblId = (Label)gvOthers.Rows[i].Cells[0].FindControl("lblId");
                                Label lblRefId = (Label)gvOthers.Rows[i].Cells[0].FindControl("lblRefId");
                                Label lblCTRLNo = (Label)gvOthers.Rows[i].Cells[1].FindControl("lblCTRLNo");
                                Label lblSpecification = (Label)gvOthers.Rows[i].Cells[2].FindControl("lblSpecification");
                                Label lblQuantity = (Label)gvOthers.Rows[i].Cells[3].FindControl("lblQuantity");
                                Label lblSRFItemName = (Label)gvOthers.Rows[i].Cells[4].FindControl("lblSRFItemName");
                                Label lblRequester = (Label)gvOthers.Rows[i].Cells[5].FindControl("lblRequester");
                                Label lblTransactionDate = (Label)gvOthers.Rows[i].Cells[6].FindControl("lblTransactionDate");

                                

                                draftNew_ForOthers.SetCellValue("A" + id_counter_for_excel_ForOthers.ToString(), lblId.Text.ToUpper());
                                draftNew_ForOthers.SetCellValue("B" + id_counter_for_excel_ForOthers.ToString(), lblCTRLNo.Text.ToUpper());
                                draftNew_ForOthers.SetCellValue("C" + id_counter_for_excel_ForOthers.ToString(), lblSpecification.Text.ToUpper());
                                draftNew_ForOthers.SetCellValue("D" + id_counter_for_excel_ForOthers.ToString(), lblQuantity.Text.ToUpper());
                                draftNew_ForOthers.SetCellValue("E" + id_counter_for_excel_ForOthers.ToString(), lblRequester.Text.ToUpper());
                                draftNew_ForOthers.SetCellValue("F" + id_counter_for_excel_ForOthers.ToString(), lblTransactionDate.Text.ToUpper());
                                draftNew_ForOthers.SetCellValue("G" + id_counter_for_excel_ForOthers.ToString(), lblSRFItemName.Text.ToUpper());

                                draftNew_ForOthers.SetCellValue("A1", "SRF PULL OUT REPORT FOR " + lblSRFItemName.Text.ToUpper());

                                id_counter_for_excel_ForOthers++;
                            }

                            fsNew_ForOthers.Close();
                            draftNew_ForOthers.SaveAs(pathNew_ForOthers);
                        }

                        Response.Redirect("SRF_PO_XLS/Others_Report_Draft.xlsx", false);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Record(s) not found!');", true);
                    }

                    //--------------------------------------------------------------------------------------------------------------------------------------
                }





            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities_SRF_PO_Entry> list = new List<Entities_SRF_PO_Entry>();
                Entities_SRF_PO_Entry status = new Entities_SRF_PO_Entry();

                status.CrFrom = txtFrom.Text.Trim();
                status.CrTo = txtTo.Text.Trim();


                // CONTAINER TUBES
                if (ddPullOutType.SelectedItem.Text == "CONTAINER TUBES")
                {
                    list = BLL.SRF_TRANSACTION_PO_Report_ByRange(status).Where(itm => itm.Head_Type == "CONTAINER TUBES").ToList();

                    if (list.Count > 0)
                    {
                        gvContainerTube.Visible = true;
                        gvContainerTube.DataSource = list;
                        gvContainerTube.DataBind();
                    }
                    else
                    {
                        gvContainerTube.Visible = false;
                        gvContainerTube.EmptyDataText = "NO RECORD(S) FOUND!";
                    }
                }

                if (ddPullOutType.SelectedItem.Text == "IC TRAYS")
                {
                    list = BLL.SRF_TRANSACTION_PO_Report_ByRange(status).Where(itm => itm.Head_Type == "IC TRAYS").ToList();

                    if (list.Count > 0)
                    {

                        gvICTrays.Visible = true;
                        gvICTrays.DataSource = list;
                        gvICTrays.DataBind();

                    }
                    else
                    {
                        gvICTrays.Visible = false;
                        gvICTrays.EmptyDataText = "NO RECORD(S) FOUND!";
                    }
                }

                // OTHERS
                if (ddPullOutType.SelectedItem.Text == "EMPTY BLACK TRAY" || ddPullOutType.SelectedItem.Text == "DANPLA BOX" || ddPullOutType.SelectedItem.Text == "ROBUST REEL")
                {
                    list = BLL.SRF_TRANSACTION_PO_Report_ByRange(status).Where(itm => itm.Head_Type == ddPullOutType.SelectedItem.Text).ToList();

                    if (list.Count > 0)
                    {

                        gvOthers.Visible = true;
                        gvOthers.DataSource = list;
                        gvOthers.DataBind();

                    }
                    else
                    {
                        gvOthers.Visible = false;
                        gvOthers.EmptyDataText = "NO RECORD(S) FOUND!";
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
