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
    public partial class CRF_RequestInquiry : System.Web.UI.Page
    {
        BLL_CRF BLL = new BLL_CRF();
        Common COMMON = new Common();

        public string tabAttachmentContents = string.Empty;
        public string tabAttachmentRequester = string.Empty;
        public string tabAttachmentContentsRequester = string.Empty;

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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();
                Entities_CRF_RequestEntry status = new Entities_CRF_RequestEntry();

                status.CrFrom = txtFrom.Text.Trim();
                status.CrTo = txtTo.Text.Trim();
                status.Requester = Session["LcRefId"].ToString();
                status.CtrlNo = txtSearch.Text;


                if (txtSearch.Text.Length > 0)
                {
                    Session["Search_From_CRF_Inquiry"] = txtSearch.Text;
                    Response.Redirect("CRF_AllRequest.aspx", false);
                }
                else
                {
                    if (ddItemStatus.SelectedValue.ToLower() == "pending")
                    {
                        list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange(status).Where(itm => itm.ReqManagerStat == "0" || itm.PurInchargeStat == "0" || itm.PurManagerStat == "0" || itm.ReqManagerStat == null || itm.PurInchargeStat == null || itm.PurManagerStat == null).ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "disapproved")
                    {
                        list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange(status).Where(itm => itm.ReqManagerStat == "2" || itm.PurInchargeStat == "2" || itm.PurManagerStat == "2").ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "approved")
                    {
                        list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange(status).Where(itm => itm.PurManagerStat == "1").ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "all")
                    {
                        list = BLL.CRF_TRANSACTION_RequestStatus_ByDateRange(status);
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


        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                HtmlControl divRequesterAttachment = row.FindControl("divRequesterAttachment") as HtmlControl;
                GridView gvDataStatus = row.FindControl("gvDataStatus") as GridView;
                GridView gvDataHead = row.FindControl("gvDataHead") as GridView;
                GridView gvDataDetails = row.FindControl("gvDataDetails") as GridView;
                LinkButton lbCTRLNo = row.FindControl("lbCTRLNo") as LinkButton;
                LinkButton lblView = row.FindControl("lblView") as LinkButton;
                

                if (e.CommandName == "lbCTRLNo_Command")
                {
                    string URL = "~/CRF_RequestEntry.aspx?CRFNo_From_Inquiry=" + CryptorEngine.Encrypt(lbCTRLNo.Text.Trim(), true);

                    URL = Page.ResolveClientUrl(URL);
                    lbCTRLNo.OnClientClick = "window.open('" + URL + "'); return false;";
                }

                if (e.CommandName == "lblView_Command")
                {
                    if (lblView.Text.ToUpper() == "OPEN DETAILS")
                    {
                        //HEAD
                        List<Entities_CRF_RequestEntry> listRequest = new List<Entities_CRF_RequestEntry>();
                        Entities_CRF_RequestEntry entityRequest = new Entities_CRF_RequestEntry();
                        entityRequest.CtrlNo = lbCTRLNo.Text.Trim();

                        listRequest = BLL.CRF_TRANSACTION_GetRequestByCTRLNo(entityRequest);

                        if (listRequest != null)
                        {
                            if (listRequest.Count > 0)
                            {
                                gvDataHead.Visible = true;
                                gvDataHead.DataSource = listRequest;
                                gvDataHead.DataBind();

                                gvDataStatus.Visible = true;
                                gvDataStatus.DataSource = listRequest;
                                gvDataStatus.DataBind();
                                
                            }
                        }

                        //DETAILS
                        List<Entities_CRF_RequestEntry> listRequestDetails = new List<Entities_CRF_RequestEntry>();
                        Entities_CRF_RequestEntry entityRequestDetails = new Entities_CRF_RequestEntry();
                        entityRequestDetails.RdCtrlNo = lbCTRLNo.Text.Trim();

                        listRequestDetails = BLL.CRF_TRANSACTION_GetRequestDetailsByCTRLNo(entityRequestDetails);

                        if (listRequestDetails != null)
                        {
                            if (listRequestDetails.Count > 0)
                            {
                                gvDataDetails.Visible = true;
                                gvDataDetails.DataSource = listRequestDetails;
                                gvDataDetails.DataBind();
                                

                                //REQUESTER ATTACHMENT
                                
                                //int attachCounter = 1;
                                //string cssTabPane = string.Empty;

                                //string temp_cssTabPane = string.Empty;
                                //string temp_tabAttachment = string.Empty;
                                //string temp_tabAttachmentContents = string.Empty;

                                //foreach (Entities_CRF_RequestEntry attachment in listRequestDetails)
                                //{
                                //    if (!string.IsNullOrEmpty(attachment.RdAttachment))
                                //    {
                                //        temp_cssTabPane = attachCounter.ToString() == "1" ? "tab-pane fade in active" : "tab-pane fade";
                                //        temp_tabAttachment += "<li role='presentation'><a href='#" + attachment.RdAttachment.Replace(".pdf", "").Replace(".PDF", "") + "'data-toggle='tab'>" + attachCounter.ToString() + "</a></li> ";
                                //        temp_tabAttachmentContents += "<div role='tabpanel' class='" + temp_cssTabPane + "' id='" + attachment.RdAttachment.Replace(".pdf", "").Replace(".PDF", "") + "'> <b>" + attachment.RdItemName + " - " + attachment.RdSpecs + "</b>" +
                                //                                 "<p><iframe runat='server' id='iframepdf" + attachCounter.ToString() + "' height='1045' width='1185' frameborder='0' src='/CRF_Request/" + lbCTRLNo.Text.Trim() + "/" + attachment.RdAttachment + "'> </iframe></p>" + "<b>" + attachment.RdItemName + " - " + attachment.RdSpecs + "</b>" + "</div>";
                                //        attachCounter++;
                                //    }
                                //}

                                //if (attachCounter > 1)
                                //{
                                //    divRequesterAttachment.Style.Add("display", "block");
                                //    tabAttachmentRequester = temp_tabAttachment;
                                //    tabAttachmentContentsRequester = temp_tabAttachmentContents;
                                //}
                                //else
                                //{
                                //    divRequesterAttachment.Style.Add("display", "none");
                                //}

                            }
                        }


                        
                        lblView.Text = "CLOSE DETAILS";
                    }
                    else
                    {
                        //divRequesterAttachment.Style.Add("display", "none");
                        gvDataStatus.Visible = false;
                        gvDataDetails.Visible = false;
                        gvDataHead.Visible = false;
                        lblView.Text = "OPEN DETAILS";
                    }
                    
                }
                

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvDataDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                LinkButton lbSupplierAttachment = row.FindControl("lbSupplierAttachment") as LinkButton;
                LinkButton lbRequesterAttachment = row.FindControl("lbRequesterAttachment") as LinkButton;
                Label lblCTRLNoLabel = row.FindControl("lblCTRLNoLabel") as Label;                

                if (e.CommandName == "lbSupplierAttachment_Command")
                {
                    string URL = "~/CRF_Received/" + lblCTRLNoLabel.Text.Trim() + "/" + lbSupplierAttachment.Text.Replace("%20"," ");

                    URL = Page.ResolveClientUrl(URL);
                    lbSupplierAttachment.OnClientClick = "window.open('" + URL + "'); return false;";
                }

                if (e.CommandName == "lbRequesterAttachment_Command")
                {
                    string URL = "~/CRF_Request/" + lblCTRLNoLabel.Text.Trim() + "/" + lbRequesterAttachment.Text.Replace("%20", " ");

                    URL = Page.ResolveClientUrl(URL);
                    lbRequesterAttachment.OnClientClick = "window.open('" + URL + "'); return false;";
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
                    Label lblDisapprovalCause2 = (Label)e.Row.FindControl("lblDisapprovalCause2");
                    HtmlControl trDisapprovalCause = (HtmlControl)e.Row.FindControl("trDisapprovalCause");

                    if (!string.IsNullOrEmpty(lblDisapprovalCause2.Text))
                    {
                        trDisapprovalCause.Style.Add("display", "block");
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

        //protected void gvDataSupplierResponse_OnRowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            Label lblResponseDetailsRefId = (Label)e.Row.FindControl("lblResponseDetailsRefId");
        //            Label lblCTRLNoHead = (Label)e.Row.FindControl("lblCTRLNoHead");
        //            GridView gvDataSupplierResponse = (GridView)e.Row.FindControl("gvDataSupplierResponse");

        //            //DETAILS
        //            List<Entities_CRF_RequestEntry> listRequestDetails = new List<Entities_CRF_RequestEntry>();
        //            Entities_CRF_RequestEntry entityRequestDetails = new Entities_CRF_RequestEntry();
        //            entityRequestDetails.RdCtrlNo = lblCTRLNoHead.Text.Trim();

        //            listRequestDetails = BLL.CRF_TRANSACTION_GetRequestDetailsByCTRLNo(entityRequestDetails).Where(itm => itm.ResponseResponseDetailsId.Trim() == lblResponseDetailsRefId.Text.Trim()).ToList();

        //            if (listRequestDetails != null)
        //            {
        //                if (listRequestDetails.Count > 0)
        //                {
        //                    // SUPPLIER RESPONSE
        //                    gvDataSupplierResponse.Visible = true;
        //                    gvDataSupplierResponse.DataSource = listRequestDetails;
        //                    gvDataSupplierResponse.DataBind();
        //                }
        //            }

        //            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
        //            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
        //    }
        //}

        protected void gvDataStatus_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblProdManagerStat = (Label)e.Row.FindControl("lblProdManagerStat");
                    Label lblProdManagerStatColor = (Label)e.Row.FindControl("lblProdManagerStatColor");

                    Label lblBuyerStat = (Label)e.Row.FindControl("lblBuyerStat");
                    Label lblBuyerStatColor = (Label)e.Row.FindControl("lblBuyerStatColor");

                    Label lblSCDManagerStat = (Label)e.Row.FindControl("lblSCDManagerStat");
                    Label lblSCDManagerStatColor = (Label)e.Row.FindControl("lblSCDManagerStatColor");



                    lblProdManagerStat.Style.Add("background-color", lblProdManagerStatColor.Text.Trim());
                    lblBuyerStat.Style.Add("background-color", lblBuyerStatColor.Text.Trim());
                    lblSCDManagerStat.Style.Add("background-color", lblSCDManagerStatColor.Text.Trim());


                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

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
                    Label lblStatAll = (Label)e.Row.FindControl("lblStatAll");
                    Label lblStatColor = (Label)e.Row.FindControl("lblStatColor");
                    Label lblCTRLNo = (Label)e.Row.FindControl("lblCTRLNo");
                    LinkButton lbCTRLNo = (LinkButton)e.Row.FindControl("lbCTRLNo");

                    if (lblStatAll.Text.ToUpper() == "FOR PROD. MNGR APPROVAL")
                    {
                        lbCTRLNo.Visible = true;
                        lblCTRLNo.Visible = false;
                    }
                    else
                    {
                        lbCTRLNo.Visible = false;
                        lblCTRLNo.Visible = true;
                    }

                    lblStatAll.Style.Add("background-color", lblStatColor.Text.Trim());

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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvData.Rows.Count > 0)
            {
                //btnSubmit_Click(sender, e);
                gvExport.Visible = true;
                Response.Clear();
                Response.Buffer = true;

                Response.AddHeader("content-disposition",
                "attachment;filename=CRF_Export.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //Change the Header Row back to white color
                gvExport.HeaderRow.Style.Add("background-color", "#FFFFFF");
                gvExport.HeaderRow.Style.Add("color", "#000000");


                gvExport.RenderControl(hw);

                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
                gvExport.Visible = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('CANNOT EXPORT EMPTY RECORDS');", true);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }





    }
}
