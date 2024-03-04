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
using System.Collections.Generic;
using System.IO;

namespace REPI_PUR_SOFRA
{
    public partial class URF_RequestInquiry : System.Web.UI.Page
    {
        BLL_URF BLL = new BLL_URF();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        txtFrom.Text = DateTime.Today.AddDays(-365).ToString("MM/dd/yyyy");
                        txtTo.Text = DateTime.Today.ToString("MM/dd/yyyy");

                        // call submit button to load initial record
                        btnSubmit_Click(sender, e);
                    }
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
                Entities_URF_RequestEntry entity = new Entities_URF_RequestEntry();

                entity.DrFrom = txtFrom.Text.Trim();
                entity.DrTo = txtTo.Text.Trim();
                entity.RhRequester = Session["LcRefId"].ToString();
                entity.RhCtrlNo = txtSearch.Text;

                list = null;

                if (!string.IsNullOrEmpty(txtSearch.Text) || txtSearch.Text.Length > 0)
                {
                    //if (ddStatus.SelectedItem.Text.ToLower() == "all")
                    //{
                    //    list = BLL.URF_TRANSACTION_Monitoring_ByDateRange(entity).Where(itm => itm.RhCtrlNo.Contains(txtSearch.Text)).ToList();
                    //}
                    //else
                    //{
                    //    list = BLL.URF_TRANSACTION_Monitoring_ByDateRange(entity).Where(itm => itm.RhCtrlNo.Contains(txtSearch.Text) && itm.CssColorCode == ddStatus.SelectedItem.Value.ToString()).ToList();
                    //}
                    Session["Search_From_URF_Inquiry"] = txtSearch.Text;
                    Response.Redirect("URF_AllRequest.aspx", false);
                }
                else
                {
                    if (ddStatus.SelectedItem.Text.ToLower() == "all")
                    {
                        list = BLL.URF_TRANSACTION_Monitoring_ByDateRange(entity);
                    }
                    else
                    {
                        list = BLL.URF_TRANSACTION_Monitoring_ByDateRange(entity).Where(itm => itm.CssColorCode == ddStatus.SelectedItem.Value.ToString()).ToList();
                    }
                }

                if (list != null || list.Count > 0)
                {
                    gvData.DataSource = list;
                    gvData.DataBind();

                    //EXPORT TO EXCEL
                    List<Entities_URF_RequestEntry> finalListExport = new List<Entities_URF_RequestEntry>();

                    foreach (Entities_URF_RequestEntry entity2 in list)
                    {
                        List<Entities_URF_RequestEntry> listExport = new List<Entities_URF_RequestEntry>();
                        listExport = BLL.URF_TRANSACTION_GetRequestDetailsByCTRLNo_ExportToExcel(entity2);

                        if (listExport != null)
                        {
                            if (listExport.Count > 0)
                            {
                                foreach (Entities_URF_RequestEntry le in listExport)
                                {
                                    Entities_URF_RequestEntry final = new Entities_URF_RequestEntry();
                                    final.RdCtrlNo = le.RdCtrlNo;

                                    final.RhRequester = le.RhRequester;
                                    final.RhTransactionDate = le.RhTransactionDate;
                                    final.LcDepartment = le.LcDepartment;
                                    final.LcDivision = le.LcDivision;
                                    final.RdPONO = le.RdPONO;
                                    final.RdPRNO = le.RdPRNO;
                                    final.RdItemName = le.RdItemName;
                                    final.RdSpecs = le.RdSpecs;
                                    final.RdQuantity = le.RdQuantity;
                                    final.RdUnitOfMeasure = le.RdUnitOfMeasure;
                                    final.RdUOMDesc = le.RdUOMDesc;
                                    final.RdDeliveryConfirmationDate = le.RdDeliveryConfirmationDate;
                                    final.RdRequestedDeliveryDate = le.RdRequestedDeliveryDate;
                                    final.RdReplyDeliveryDate = le.RdReplyDeliveryDate;
                                    final.RhSupplier = le.RhSupplier;
                                    final.RhReason = le.RhReason;
                                    final.RhOtherReason = le.RhOtherReason;
                                    //final.RhType = le.RhType == "1" ? "LOCAL";
                                    if (!string.IsNullOrEmpty(le.RhType))
                                    {
                                        final.RhType = le.RhType == "1" ? "LOCAL" : "OVERSEAS";
                                    }
                                    final.RhAttention = le.RhAttention;

                                    finalListExport.Add(final);
                                }
                            }
                        }
                    }

                    gvExport.DataSource = finalListExport;
                    gvExport.DataBind();
                }

                divOpacity.Style.Add("opacity", "1");
                divLoader.Style.Add("display", "none");

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

                LinkButton lblView = row.FindControl("lblView") as LinkButton;
                Label lblCTRLNo = row.FindControl("lblCTRLNo") as Label;
                Label lblTransDate = row.FindControl("lblTransDate") as Label;
                Label lblRequester = row.FindControl("lblRequester") as Label;
                Label lblCategory = row.FindControl("lblCategory") as Label;
                Label lblStatAll = row.FindControl("lblStatAll") as Label;
                GridView gvDataDetails = row.FindControl("gvDataDetails") as GridView;
                GridView gvDataStatus = row.FindControl("gvDataStatus") as GridView;
                Label lblType = row.FindControl("lblType") as Label;
                Label lblSupplier = row.FindControl("lblSupplier") as Label;
                Label lblReason = row.FindControl("lblReason") as Label;
                Label lblAttention = row.FindControl("lblAttention") as Label;
                Label lblAttachment1 = row.FindControl("lblAttachment1") as Label;
                Label lblAttachment2 = row.FindControl("lblAttachment2") as Label;
                Label lblStockLifeAttachment = row.FindControl("lblStockLifeAttachment") as Label;
                LinkButton lbAttachment1 = row.FindControl("lbAttachment1") as LinkButton;
                LinkButton lbAttachment2 = row.FindControl("lbAttachment2") as LinkButton;
                LinkButton lbStockLifeAttachment = row.FindControl("lbStockLifeAttachment") as LinkButton;
                Label lblReOpenRemarks = row.FindControl("lblReOpenRemarks") as Label;
                Label lblSupplierAttachment = row.FindControl("lblSupplierAttachment") as Label;
                Label lblDisapprovalCause = row.FindControl("lblDisapprovalCause") as Label;


                if (e.CommandName == "lbCTRLNo_Command")
                {
                    Response.Redirect("URF_RequestEntry_New.aspx?URFNo_From_Inquiry=" + CryptorEngine.Encrypt(lblCTRLNo.Text.Trim(), true), false);
                }

                if (e.CommandName == "lbReOpen_Command")
                {
                    Session["URF_ReqEntry_ReOpen"] = "true";
                    Response.Redirect("URF_RequestEntry_New.aspx?URFNo_From_Inquiry=" + CryptorEngine.Encrypt(lblCTRLNo.Text.Trim(), true), false);
                }

                if (e.CommandName == "lblView_Command")
                {
                    if (lblView.Text.ToUpper() == "OPEN DETAILS")
                    {
                        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();
                        Entities_URF_RequestEntry entity = new Entities_URF_RequestEntry();

                        entity.RdCtrlNo = lblCTRLNo.Text.Trim();
                        list = BLL.URF_TRANSACTION_GetRequestDetailsByCTRLNo(entity);

                        if (list != null)
                        {
                            if (list.Count > 0)
                            {                               

                                gvDataDetails.DataSource = list;
                                gvDataDetails.DataBind();
                                gvDataDetails.Visible = true;

                                List<Entities_URF_RequestEntry> listStatus = new List<Entities_URF_RequestEntry>();

                                int statCounter = 0;
                                foreach (Entities_URF_RequestEntry eStat in list)
                                {
                                    Entities_URF_RequestEntry entityStatus = new Entities_URF_RequestEntry();

                                    entityStatus.StatProdSecManager = eStat.StatProdSecManager;
                                    entityStatus.StatProdDeptManager = eStat.StatProdDeptManager;
                                    entityStatus.StatProdDivManager = eStat.StatProdDivManager;
                                    entityStatus.StatProdHQManager = eStat.StatProdHQManager;
                                    entityStatus.StatPurchasingBuyer = eStat.StatPurchasingBuyer;
                                    entityStatus.StatPurchasingManager = eStat.StatPurchasingManager;

                                    if (statCounter <= 0)
                                    {
                                        listStatus.Add(entityStatus);
                                        if (!string.IsNullOrEmpty(eStat.StatRemarks))
                                        {
                                            lblReason.Visible = true;
                                            lblReason.Text = eStat.StatRemarks;
                                        }

                                        lblAttention.Text = "ATTENTION : " + eStat.RhAttention;

                                        if (string.IsNullOrEmpty(eStat.RhOtherReason))
                                        {
                                            lblReason.Text = "REASON : " + eStat.RhReason;
                                        }
                                        else
                                        {
                                            lblReason.Text = "REASON : " + eStat.RhOtherReason;
                                        }

                                        lblSupplier.Text = "SUPPLIER : " + eStat.RhSupplier;

                                        if (eStat.RhType == "1")
                                        {
                                            lblType.Text = "TYPE : LOCAL";
                                        }
                                        if (eStat.RhType == "2")
                                        {
                                            lblType.Text = "TYPE : OVERSEAS";
                                        }

                                        lblAttention.Visible = true;
                                        lblReason.Visible = true;
                                        lblSupplier.Visible = true;
                                        lblType.Visible = true;                                        

                                        if (!string.IsNullOrEmpty(eStat.DisapprovalCause))
                                        {
                                            lblDisapprovalCause.Visible = true;
                                            lblDisapprovalCause.Text = "DISAPPROVAL CAUSE: " + eStat.DisapprovalCause;
                                        }

                                        if (!string.IsNullOrEmpty(eStat.RhAttachment1))
                                        {
                                            lblAttachment1.Visible = true;
                                            lbAttachment1.Visible = true;
                                            lbAttachment1.Text = eStat.RhAttachment1;

                                            string URL1 = "~/URF_Request/" + eStat.RdCtrlNo + "/" + eStat.RhAttachment1;

                                            URL1 = Page.ResolveClientUrl(URL1);
                                            lbAttachment1.OnClientClick = "window.open('" + URL1 + "'); return false;";

                                        }
                                        if (!string.IsNullOrEmpty(eStat.RhAttachment2))
                                        {
                                            lblAttachment2.Visible = true;
                                            lbAttachment2.Visible = true;
                                            lbAttachment2.Text = eStat.RhAttachment2;

                                            string URL2 = "~/URF_Request/" + eStat.RdCtrlNo + "/" + eStat.RhAttachment2;

                                            URL2 = Page.ResolveClientUrl(URL2);
                                            lbAttachment2.OnClientClick = "window.open('" + URL2 + "'); return false;";
                                        }
                                        if (!string.IsNullOrEmpty(eStat.RhStockLifeAttachment))
                                        {
                                            lblStockLifeAttachment.Visible = true;
                                            lbStockLifeAttachment.Visible = true;
                                            lbStockLifeAttachment.Text = eStat.RhStockLifeAttachment;

                                            string URL3 = "~/URF_Request/" + eStat.RdCtrlNo + "/" + eStat.RhStockLifeAttachment;

                                            URL3 = Page.ResolveClientUrl(URL3);
                                            lbStockLifeAttachment.OnClientClick = "window.open('" + URL3 + "'); return false;";
                                        }

                                        if (!string.IsNullOrEmpty(eStat.StatReOpenRemarks))
                                        {
                                            lblReOpenRemarks.Text = "RE-OPEN REMARKS : " + eStat.StatReOpenRemarks;
                                            lblReOpenRemarks.Visible = true;
                                        }

                                        List<Entities_URF_RequestEntry> listSupplierAttachment = new List<Entities_URF_RequestEntry>();
                                        Entities_URF_RequestEntry entitySupplierAttachment = new Entities_URF_RequestEntry();

                                        entitySupplierAttachment.RhCtrlNo = lblCTRLNo.Text.Trim();
                                        listSupplierAttachment = BLL.URF_TRANSACTION_GetSupplierAttachmentByCTRLNo(entitySupplierAttachment);

                                        if (listSupplierAttachment != null)
                                        {
                                            if (listSupplierAttachment.Count > 0)
                                            {
                                                string supplierAttachment = string.Empty;

                                                foreach (Entities_URF_RequestEntry eSupplierAttachment in listSupplierAttachment)
                                                {
                                                    string URLSupplierAttachment = "URF_Received/" + eSupplierAttachment.RhCtrlNo + "/" + eSupplierAttachment.SupplierAttachment;

                                                    supplierAttachment += "<a href='" + URLSupplierAttachment + "'>" + eSupplierAttachment.SupplierAttachment + "</a>" + ", ";
                                                }

                                                lblSupplierAttachment.Text = "SUPPLIER ATTACHMENT : " + supplierAttachment;
                                                lblSupplierAttachment.Visible = true;
                                            }
                                        }

                                    }
                                    statCounter++;
                                }

                                gvDataStatus.DataSource = listStatus;
                                gvDataStatus.DataBind();
                                gvDataStatus.Visible = true;

                            }
                        }
                        lblView.Text = "CLOSE DETAILS";
                    }
                    else
                    {
                        lblView.Text = "OPEN DETAILS";
                        gvDataDetails.Visible = false;
                        gvDataStatus.Visible = false;
                        lblAttention.Visible = false;
                        lblReason.Visible = false;
                        lblSupplier.Visible = false;
                        lblType.Visible = false;
                        lblAttachment1.Visible = false;
                        lbAttachment1.Visible = false;
                        lblAttachment2.Visible = false;
                        lbAttachment2.Visible = false;
                        lblStockLifeAttachment.Visible = false;
                        lbStockLifeAttachment.Visible = false;
                        lblSupplierAttachment.Visible = false;
                        lblReOpenRemarks.Visible = false;
                        lblDisapprovalCause.Visible = false;
                    }
                    
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
                    LinkButton lbReOpen = (LinkButton)e.Row.FindControl("lbReOpen");

                    if (lblStatAll.Text.ToUpper() == "FOR SECTION MNGR APPROVAL")
                    {
                        lbCTRLNo.Visible = true;
                        lblCTRLNo.Visible = false;
                    }
                    else
                    {
                        lbCTRLNo.Visible = false;
                        lblCTRLNo.Visible = true;
                    }

                    if (lblStatAll.Text.ToUpper() == "CLOSED")
                    {
                        lbReOpen.Visible = true;
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
            try
            {
                gvData.PageIndex = e.NewPageIndex;
                btnSubmit_Click(sender, e);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
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
                "attachment;filename=URF_Export.xls");
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
