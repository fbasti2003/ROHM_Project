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


namespace REPI_PUR_SOFRA
{
    public partial class CRF_AllRequest : System.Web.UI.Page
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
                        if (ddItemStatus.SelectedValue.ToLower() == "pending")
                        {
                            list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status).Where(itm => itm.ReqManagerStat == "0" || itm.PurInchargeStat == "0" || itm.PurManagerStat == "0" || itm.ReqManagerStat == null || itm.PurInchargeStat == null || itm.PurManagerStat == null).ToList();
                        }
                        if (ddItemStatus.SelectedValue.ToLower() == "disapproved")
                        {
                            list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status).Where(itm => itm.ReqManagerStat == "2" || itm.PurInchargeStat == "2" || itm.PurManagerStat == "2").ToList();
                        }
                        if (ddItemStatus.SelectedValue.ToLower() == "approved")
                        {
                            list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status).Where(itm => itm.PurManagerStat == "1").ToList();
                        }
                        if (ddItemStatus.SelectedValue.ToLower() == "all")
                        {
                            list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status);
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
                        list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(status).Where(itm => itm.Division == Session["Division"].ToString()).ToList();
                    }
                }

                if (list.Count > 0)
                {
                    gvDataDetails.Visible = true;
                    gvDataDetails.DataSource = list;
                    gvDataDetails.DataBind();


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
                    gvDataDetails.Visible = false;
                    gvDataDetails.EmptyDataText = "NO RECORD(S) FOUND!";
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
                
        }


        //protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        int index = int.Parse(e.CommandArgument.ToString());
        //        GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

        //        HtmlControl divRequesterAttachment = row.FindControl("divRequesterAttachment") as HtmlControl;
        //        GridView gvDataStatus = row.FindControl("gvDataStatus") as GridView;
        //        GridView gvDataHead = row.FindControl("gvDataHead") as GridView;
        //        GridView gvDataDetails = row.FindControl("gvDataDetails") as GridView;
        //        GridView gvDataSupplierResponse = row.FindControl("gvDataSupplierResponse") as GridView;
        //        LinkButton lbCTRLNo = row.FindControl("lbCTRLNo") as LinkButton;
        //        LinkButton lblView = row.FindControl("lblView") as LinkButton;

        //        if (e.CommandName == "lbCTRLNo_Command")
        //        {
        //            string URL = "~/CRF_RequestEntry.aspx?CRFNo_From_Inquiry=" + CryptorEngine.Encrypt(lbCTRLNo.Text.Trim(), true);

        //            URL = Page.ResolveClientUrl(URL);
        //            lbCTRLNo.OnClientClick = "window.open('" + URL + "'); return false;";
        //        }

        //        //if (e.CommandName == "lblView_Command")
        //        //{
        //        //    if (lblView.Text.ToUpper() == "OPEN DETAILS")
        //        //    {
        //        //        //HEAD
        //        //        List<Entities_CRF_RequestEntry> listRequest = new List<Entities_CRF_RequestEntry>();
        //        //        Entities_CRF_RequestEntry entityRequest = new Entities_CRF_RequestEntry();
        //        //        entityRequest.CtrlNo = lbCTRLNo.Text.Trim();

        //        //        listRequest = BLL.CRF_TRANSACTION_GetRequestByCTRLNo(entityRequest);

        //        //        if (listRequest != null)
        //        //        {
        //        //            if (listRequest.Count > 0)
        //        //            {
        //        //                gvDataHead.Visible = true;
        //        //                gvDataHead.DataSource = listRequest;
        //        //                gvDataHead.DataBind();

        //        //                gvDataStatus.Visible = true;
        //        //                gvDataStatus.DataSource = listRequest;
        //        //                gvDataStatus.DataBind();

        //        //                gvDataSupplierResponse.Visible = true;
        //        //                gvDataSupplierResponse.DataSource = listRequest;
        //        //                gvDataSupplierResponse.DataBind();
        //        //            }
        //        //        }

        //        //        //DETAILS
        //        //        List<Entities_CRF_RequestEntry> listRequestDetails = new List<Entities_CRF_RequestEntry>();
        //        //        Entities_CRF_RequestEntry entityRequestDetails = new Entities_CRF_RequestEntry();
        //        //        entityRequestDetails.RdCtrlNo = lbCTRLNo.Text.Trim();

        //        //        listRequestDetails = BLL.CRF_TRANSACTION_GetRequestDetailsByCTRLNo(entityRequestDetails);

        //        //        if (listRequestDetails != null)
        //        //        {
        //        //            if (listRequestDetails.Count > 0)
        //        //            {
        //        //                gvDataDetails.Visible = true;
        //        //                gvDataDetails.DataSource = listRequestDetails;
        //        //                gvDataDetails.DataBind();                                
        //        //            }
        //        //        }



        //        //        lblView.Text = "CLOSE DETAILS";
        //        //    }
        //        //    else
        //        //    {
        //        //        gvDataStatus.Visible = false;
        //        //        gvDataDetails.Visible = false;
        //        //        gvDataHead.Visible = false;
        //        //        gvDataSupplierResponse.Visible = false;
        //        //        lblView.Text = "OPEN DETAILS";
        //        //    }

        //        //}


        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
        //    }
        //}


        //protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            Label lblStatAll = (Label)e.Row.FindControl("lblStatAll");
        //            Label lblStatColor = (Label)e.Row.FindControl("lblStatColor");
        //            Label lblCTRLNo = (Label)e.Row.FindControl("lblCTRLNo");
        //            LinkButton lbCTRLNo = (LinkButton)e.Row.FindControl("lbCTRLNo");

        //            GridView gvDataStatus = (GridView)e.Row.FindControl("gvDataStatus");
        //            GridView gvDataHead = (GridView)e.Row.FindControl("gvDataHead");
        //            GridView gvDataDetails = (GridView)e.Row.FindControl("gvDataDetails");

        //            gvDataStatus.Visible = false;
        //            gvDataDetails.Visible = false;
        //            gvDataHead.Visible = false;

        //            //HEAD
        //            List<Entities_CRF_RequestEntry> listRequest = new List<Entities_CRF_RequestEntry>();
        //            Entities_CRF_RequestEntry entityRequest = new Entities_CRF_RequestEntry();
        //            entityRequest.CtrlNo = lbCTRLNo.Text.Trim();

        //            listRequest = BLL.CRF_TRANSACTION_GetRequestByCTRLNo(entityRequest);

        //            if (listRequest != null)
        //            {
        //                if (listRequest.Count > 0)
        //                {
        //                    gvDataHead.Visible = true;
        //                    gvDataHead.DataSource = listRequest;
        //                    gvDataHead.DataBind();

        //                    gvDataStatus.Visible = true;
        //                    gvDataStatus.DataSource = listRequest;
        //                    gvDataStatus.DataBind();

        //                }
        //            }

        //            //DETAILS
        //            List<Entities_CRF_RequestEntry> listRequestDetails = new List<Entities_CRF_RequestEntry>();
        //            Entities_CRF_RequestEntry entityRequestDetails = new Entities_CRF_RequestEntry();
        //            entityRequestDetails.RdCtrlNo = lbCTRLNo.Text.Trim();

        //            listRequestDetails = BLL.CRF_TRANSACTION_GetRequestDetailsByCTRLNo(entityRequestDetails);

        //            if (listRequestDetails != null)
        //            {
        //                if (listRequestDetails.Count > 0)
        //                {
        //                    gvDataDetails.Visible = true;
        //                    gvDataDetails.DataSource = listRequestDetails;
        //                    gvDataDetails.DataBind();
        //                }
        //            }



        //            lblStatAll.Style.Add("background-color", lblStatColor.Text.Trim());

        //            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
        //            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
        //    }
        //}

        protected void gvDataDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                LinkButton lbSupplierAttachment = row.FindControl("lbSupplierAttachment") as LinkButton;
                LinkButton lbRequesterAttachment = row.FindControl("lbRequesterAttachment") as LinkButton;
                LinkButton lbCTRLNo2 = row.FindControl("lbCTRLNo2") as LinkButton;
                LinkButton lbPreview = row.FindControl("lbCTRLNo2") as LinkButton;
                Label lblCTRLNoLabel = row.FindControl("lbPreview") as Label;
                TextBox txtAttention2 = row.FindControl("txtAttention2") as TextBox;

                if (e.CommandName == "lbSupplierAttachment_Command")
                {
                    string URL = "~/CRF_Received/" + lblCTRLNoLabel.Text.Trim() + "/" + lbSupplierAttachment.Text.Replace("%20", " ");

                    URL = Page.ResolveClientUrl(URL);
                    lbSupplierAttachment.OnClientClick = "window.open('" + URL + "'); return false;";
                }

                if (e.CommandName == "lbRequesterAttachment_Command")
                {
                    string URL = "~/CRF_Request/" + lblCTRLNoLabel.Text.Trim() + "/" + lbRequesterAttachment.Text.Replace("%20", " ");
                    Response.Redirect(URL, false);
                    //URL = Page.ResolveClientUrl(URL);
                    //lbRequesterAttachment.OnClientClick = "window.open('" + URL + "'); return false;";
                }

                if (e.CommandName == "lbCTRLNo2_Command")
                {
                    string URL = "~/CRF_RequestEntry.aspx?CRFNo_From_Inquiry=" + CryptorEngine.Encrypt(lbCTRLNo2.Text.Trim(), true);
                    Response.Redirect(URL, false);
                    //URL = Page.ResolveClientUrl(URL);
                    //lbCTRLNo2.OnClientClick = "window.open('" + URL + "'); return false;";
                }

                if (e.CommandName == "lbPreview_Command")
                {

                    string pathLocation = Server.MapPath("~/CRF_Request/" + lbCTRLNo2.Text.Trim() + "/REPORT_" + lbCTRLNo2.Text.Trim() + ".html");
                    string htmlTemplate = Server.MapPath("~/HTML_Report/CRF/CRF.txt");
                    string htmlTemplate_Multiple = Server.MapPath("~/HTML_Report/CRF/CRF_Multiple.txt");
                    string templateValue = string.Empty;

                    if (System.IO.File.Exists(htmlTemplate))
                    {

                        //DETAILS
                        List<Entities_CRF_RequestEntry> listRequestDetails = new List<Entities_CRF_RequestEntry>();
                        Entities_CRF_RequestEntry entityRequestDetails = new Entities_CRF_RequestEntry();
                        entityRequestDetails.RdCtrlNo = lbCTRLNo2.Text.Trim();

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
                                                           .Replace("val_attention", txtAttention2.Text.ToUpper())
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
                                                               .Replace("val_attention", txtAttention2.Text.ToUpper())
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

                    string URL = "CRF_Request/" + lbCTRLNo2.Text.Trim() + "/REPORT_" + lbCTRLNo2.Text.Trim() + ".html";

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

        protected void gvDataDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtStatus2 = (TextBox)e.Row.FindControl("txtStatus2");
                    Label lblStatColor = (Label)e.Row.FindControl("lblStatColor");
                    Label lblDisapprovalCause2 = (Label)e.Row.FindControl("lblDisapprovalCause2");
                    HtmlControl trDisapprovalCause = (HtmlControl)e.Row.FindControl("trDisapprovalCause");

                    if (!string.IsNullOrEmpty(lblDisapprovalCause2.Text))
                    {
                        trDisapprovalCause.Style.Add("display", "block");
                    }


                    txtStatus2.Style.Add("background-color", lblStatColor.Text.Trim());

                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvDataDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDataDetails.PageIndex = e.NewPageIndex;
            btnSubmit_Click(sender, e);
        }

        //protected void gvDataStatus_OnRowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            Label lblProdManagerStat = (Label)e.Row.FindControl("lblProdManagerStat");
        //            Label lblProdManagerStatColor = (Label)e.Row.FindControl("lblProdManagerStatColor");

        //            Label lblBuyerStat = (Label)e.Row.FindControl("lblBuyerStat");
        //            Label lblBuyerStatColor = (Label)e.Row.FindControl("lblBuyerStatColor");

        //            Label lblSCDManagerStat = (Label)e.Row.FindControl("lblSCDManagerStat");
        //            Label lblSCDManagerStatColor = (Label)e.Row.FindControl("lblSCDManagerStatColor");



        //            lblProdManagerStat.Style.Add("background-color", lblProdManagerStatColor.Text.Trim());
        //            lblBuyerStat.Style.Add("background-color", lblBuyerStatColor.Text.Trim());
        //            lblSCDManagerStat.Style.Add("background-color", lblSCDManagerStatColor.Text.Trim());


        //            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
        //            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
        //    }
        //}

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



    }
}
