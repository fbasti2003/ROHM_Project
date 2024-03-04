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
using System.Text;

namespace REPI_PUR_SOFRA
{
    public partial class URF_ReceivingEntry : System.Web.UI.Page
    {

        BLL_URF BLL = new BLL_URF();
        BLL_RFQ BLL_RFQ = new BLL_RFQ();
        Common COMMON = new Common();

        BLL_Common BLL_COMMON = new BLL_Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()))
                {

                    //---------------------------------------------------------------------------------------------------

                    List<Entities_URF_RequestEntry> listDropDown = new List<Entities_URF_RequestEntry>();
                    listDropDown = BLL.URF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                    if (listDropDown != null)
                    {
                        if (listDropDown.Count > 0)
                        {
                            ddCategory.Items.Add("");

                            foreach (Entities_URF_RequestEntry entity in listDropDown)
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

                            //if (!string.IsNullOrEmpty(Session["CategoryAccess"].ToString()))
                            //{
                            //    if (int.Parse(Session["CategoryAccess"].ToString()) > 0)
                            //    {
                            //        ddCategory.Items.FindByValue(Session["CategoryAccess"].ToString()).Selected = true;
                            //        ddCategory.Enabled = false;
                            //    }
                            //}

                            //---------------------------------------------------------------------------------------------------


                            if (Session["CategoryAccess"] != null)
                            {
                                if (int.Parse(Session["CategoryAccess"].ToString()) > 0)
                                {

                                    if (Session["SENDING_OTHER_BUYERS"] != null)
                                    {
                                        if (!string.IsNullOrEmpty(Session["SENDING_OTHER_BUYERS"].ToString()))
                                        {
                                            ddCategory.Enabled = true;
                                            ddCategory.Items.FindByValue(Session["SENDING_OTHER_BUYERS"].ToString()).Selected = true;
                                        }
                                    }
                                    else
                                    {
                                        ddCategory.Enabled = false;
                                        ddCategory.Items.FindByValue(Session["CategoryAccess"].ToString()).Selected = true;
                                    }
                                }
                                else
                                {
                                    ddCategory.Enabled = true;
                                }
                            }

                            //---------------------------------------------------------------------------------------------------

                        }
                    }

                    //---------------------------------------------------------------------------------------------------

                    txtFrom.Text = DateTime.Today.AddDays(-1825).ToString("MM/dd/yyyy");
                    txtTo.Text = DateTime.Today.ToString("MM/dd/yyyy");

                    btnSubmit_Click(sender, e);
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                
                List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();
                Entities_URF_RequestEntry entity = new Entities_URF_RequestEntry();
                entity.DrFrom = txtFrom.Text;
                entity.DrTo = txtTo.Text;

                list = null;

                if (!string.IsNullOrEmpty(txtURFNo.Text) || txtURFNo.Text.Length > 0)
                {
                    Session["Search_From_URF_Inquiry"] = txtURFNo.Text;
                    Response.Redirect("URF_AllRequest.aspx");
                }
                else
                {
                    if (ddCategory.SelectedItem.Text.ToLower() == "")
                    {
                        list = BLL.URF_TRANSACTION_Receiving_DateRange(entity);
                    }
                    else
                    {
                        if (ddStatus.SelectedValue == "ALL")
                        {
                            list = BLL.URF_TRANSACTION_Receiving_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim()).ToList();
                        }
                        else
                        {
                            list = BLL.URF_TRANSACTION_Receiving_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim() && itm.StatAll == ddStatus.SelectedValue).ToList();
                        }
                    }
                }

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        gvData.Visible = true;
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
                    else
                    {
                        gvData.Visible = false;
                    }
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
                LinkButton lblPreview = row.FindControl("lblPreview") as LinkButton;
                Label lblCTRLNo = row.FindControl("lblCTRLNo") as Label;
                Label lblTransDate = row.FindControl("lblTransDate") as Label;
                Label lblRequester = row.FindControl("lblRequester") as Label;
                Label lblCategory = row.FindControl("lblCategory") as Label;
                Label lblStatAll = row.FindControl("lblStatAll") as Label;
                GridView gvDataDetails = row.FindControl("gvDataDetails") as GridView;
                GridView gvDataStatus = row.FindControl("gvDataStatus") as GridView;
                ImageButton ibApproved = row.FindControl("ibApproved") as ImageButton;
                ImageButton ibDisapproved = row.FindControl("ibDisapproved") as ImageButton;
                ImageButton ibClosed = row.FindControl("ibClosed") as ImageButton;
                TextBox txtRemarks = row.FindControl("txtRemarks") as TextBox;
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
                LinkButton lbCTRLNo = row.FindControl("lbCTRLNo") as LinkButton;

                TextBox txtBuyerRemarksNew = row.FindControl("txtBuyerRemarksNew") as TextBox;



                if (e.CommandName == "UpdateBuyerRemarks_Command")
                {
                    string queryResult = BLL.URF_TRANSACTION_SQLTransaction("UPDATE URF_TRANSACTION_RequestHead SET BuyerRemarks = '" + txtBuyerRemarksNew.Text.Replace("'", "''") + "' WHERE CTRLNO = '" + lblCTRLNo.Text.Trim() + "'").ToString();
                    if (queryResult == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Buyer Remarks for " + lblCTRLNo.Text + " has been successfully updated.');", true);
                    }
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

                                    //entityStatus.StatProdSecManager = eStat.StatProdSecManager;
                                    //entityStatus.StatProdDeptManager = eStat.StatProdDeptManager;
                                    //entityStatus.StatProdDivManager = eStat.StatProdDivManager;
                                    //entityStatus.StatProdHQManager = eStat.StatProdHQManager;
                                    entityStatus.StatPurchasingBuyer = eStat.StatPurchasingBuyer;
                                    entityStatus.StatPurchasingManager = eStat.StatPurchasingManager;

                                    // PROD SEC MANAGER
                                    if (eStat.StatSTATProdSecManager == "-1")
                                    {
                                        entityStatus.StatProdSecManager = "AUTO-APPROVED";
                                    }
                                    else
                                    {
                                        entityStatus.StatProdSecManager = eStat.StatProdSecManager;
                                    }
                                    // PROD DEPT MANAGER
                                    if (eStat.StatSTATProdDeptManager == "-1")
                                    {
                                        entityStatus.StatProdDeptManager = "AUTO-APPROVED";
                                    }
                                    else
                                    {
                                        entityStatus.StatProdDeptManager = eStat.StatProdDeptManager;
                                    }
                                    // PROD DIV MANAGER
                                    if (eStat.StatSTATProdDivManager == "-1")
                                    {
                                        entityStatus.StatProdDivManager = "AUTO-APPROVED";
                                    }
                                    else
                                    {
                                        entityStatus.StatProdDivManager = eStat.StatProdDivManager;
                                    }
                                    //// PROD HQ MANAGER
                                    //if (eStat.StatSTATProdHQManager == "-1")
                                    //{
                                    //    entityStatus.StatProdHQManager = "AUTO-APPROVED";
                                    //}
                                    //else
                                    //{
                                    //    entityStatus.StatProdHQManager = eStat.StatProdHQManager;
                                    //}               

                                    if (statCounter <= 0)
                                    {
                                        listStatus.Add(entityStatus);

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
                        lblReOpenRemarks.Visible = false;
                        lblSupplierAttachment.Visible = false;
                    }

                }

                if (e.CommandName == "PDF_Command")
                {
                    Response.Redirect("URF_RequestEntry_New.aspx?URFNo_From_Inquiry=" + CryptorEngine.Encrypt(lblCTRLNo.Text.Trim(), true) + "&previewrep=true&savePDF=true", false);
                }

                if (e.CommandName == "lbCTRLNo_Command")
                {
                    Response.Redirect("URF_RequestEntry_New.aspx?URFNo_From_Inquiry=" + CryptorEngine.Encrypt(lbCTRLNo.Text.Trim(), true), false);
                }

                if (e.CommandName == "lblPreview_Command")
                {
                    //Response.Redirect("URF_RequestEntry_New.aspx?URFNo_From_Inquiry=" + CryptorEngine.Encrypt(lblCTRLNo.Text.Trim(), true) + "&previewrep=true", false);
                    string URL = "URF_RequestEntry_New.aspx?URFNo_From_Inquiry=" + CryptorEngine.Encrypt(lblCTRLNo.Text.Trim(), true) + "&previewrep=true";

                    URL = Page.ResolveClientUrl(URL);
                    lblPreview.OnClientClick = "window.open('" + URL + "'); return false;";
                }

                if (e.CommandName == "lbManualResponse_Command")
                {
                    Response.Redirect("URF_ManualResponse.aspx?URFNo_From_ManualResponse=" + CryptorEngine.Encrypt(lblCTRLNo.Text.Trim(), true), false);
                }

                if (e.CommandName == "Closed_Command")
                {
                    if (ibDisapproved.ImageUrl == "~/images/DA2.png" || ibApproved.ImageUrl == "~/images/A2.png")
                    {
                    }
                    else
                    {
                        if (ibClosed.ImageUrl == "~/images/Close3.png")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessageClose('" + lblCTRLNo.Text.Trim() + "');", true);
                            ibClosed.ImageUrl = "~/images/Close4.png";
                            txtRemarks.Enabled = true;

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

                                        //entityStatus.StatProdSecManager = eStat.StatProdSecManager;
                                        //entityStatus.StatProdDeptManager = eStat.StatProdDeptManager;
                                        //entityStatus.StatProdDivManager = eStat.StatProdDivManager;
                                        //entityStatus.StatProdHQManager = eStat.StatProdHQManager;
                                        entityStatus.StatPurchasingBuyer = eStat.StatPurchasingBuyer;
                                        entityStatus.StatPurchasingManager = eStat.StatPurchasingManager;

                                        // PROD SEC MANAGER
                                        if (eStat.StatSTATProdSecManager == "-1")
                                        {
                                            entityStatus.StatProdSecManager = "AUTO-APPROVED";
                                        }
                                        else
                                        {
                                            entityStatus.StatProdSecManager = eStat.StatProdSecManager;
                                        }
                                        // PROD DEPT MANAGER
                                        if (eStat.StatSTATProdDeptManager == "-1")
                                        {
                                            entityStatus.StatProdDeptManager = "AUTO-APPROVED";
                                        }
                                        else
                                        {
                                            entityStatus.StatProdDeptManager = eStat.StatProdDeptManager;
                                        }
                                        // PROD DIV MANAGER
                                        if (eStat.StatSTATProdDivManager == "-1")
                                        {
                                            entityStatus.StatProdDivManager = "AUTO-APPROVED";
                                        }
                                        else
                                        {
                                            entityStatus.StatProdDivManager = eStat.StatProdDivManager;
                                        }
                                        //// PROD HQ MANAGER
                                        //if (eStat.StatSTATProdHQManager == "-1")
                                        //{
                                        //    entityStatus.StatProdHQManager = "AUTO-APPROVED";
                                        //}
                                        //else
                                        //{
                                        //    entityStatus.StatProdHQManager = eStat.StatProdHQManager;
                                        //}

                                        if (statCounter <= 0)
                                        {
                                            listStatus.Add(entityStatus);

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

                        }
                        else
                        {
                            ibClosed.ImageUrl = "~/images/Close3.png";

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
                            lblReOpenRemarks.Visible = false;
                            lblSupplierAttachment.Visible = false;
                        }
                    }
                }

                if (e.CommandName == "A_Command")
                {
                    if (ibDisapproved.ImageUrl == "~/images/DA2.png" || ibClosed.ImageUrl == "~/images/Close4.png")
                    {                        
                    }
                    else
                    {
                        if (ibApproved.ImageUrl == "~/images/A1.png")
                        {                            

                            ibApproved.ImageUrl = "~/images/A2.png";

                            ////OPEN NEW TAB TO CREATE REPORT FOR SUPPLIERS COPY
                            //string URL = "URF_RequestEntry_New.aspx?URFNo_From_Inquiry=" + CryptorEngine.Encrypt(lblCTRLNo.Text.Trim(), true) + "&previewrep=true";

                            //URL = Page.ResolveClientUrl(URL);
                            //ibApproved.OnClientClick = "window.open('" + URL + "'); return false;";

                            ////////////////////////////////////////////////////

                            
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

                                        //entityStatus.StatProdSecManager = eStat.StatProdSecManager;
                                        //entityStatus.StatProdDeptManager = eStat.StatProdDeptManager;
                                        //entityStatus.StatProdDivManager = eStat.StatProdDivManager;
                                        //entityStatus.StatProdHQManager = eStat.StatProdHQManager;
                                        entityStatus.StatPurchasingBuyer = eStat.StatPurchasingBuyer;
                                        entityStatus.StatPurchasingManager = eStat.StatPurchasingManager;

                                        // PROD SEC MANAGER
                                        if (eStat.StatSTATProdSecManager == "-1")
                                        {
                                            entityStatus.StatProdSecManager = "AUTO-APPROVED";
                                        }
                                        else
                                        {
                                            entityStatus.StatProdSecManager = eStat.StatProdSecManager;
                                        }
                                        // PROD DEPT MANAGER
                                        if (eStat.StatSTATProdDeptManager == "-1")
                                        {
                                            entityStatus.StatProdDeptManager = "AUTO-APPROVED";
                                        }
                                        else
                                        {
                                            entityStatus.StatProdDeptManager = eStat.StatProdDeptManager;
                                        }
                                        // PROD DIV MANAGER
                                        if (eStat.StatSTATProdDivManager == "-1")
                                        {
                                            entityStatus.StatProdDivManager = "AUTO-APPROVED";
                                        }
                                        else
                                        {
                                            entityStatus.StatProdDivManager = eStat.StatProdDivManager;
                                        }
                                        //// PROD HQ MANAGER
                                        //if (eStat.StatSTATProdHQManager == "-1")
                                        //{
                                        //    entityStatus.StatProdHQManager = "AUTO-APPROVED";
                                        //}
                                        //else
                                        //{
                                        //    entityStatus.StatProdHQManager = eStat.StatProdHQManager;
                                        //}

                                        if (statCounter <= 0)
                                        {
                                            listStatus.Add(entityStatus);

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
                        }
                        else
                        {
                            ibApproved.ImageUrl = "~/images/A1.png";

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
                            lblReOpenRemarks.Visible = false;
                            lblSupplierAttachment.Visible = false;

                            
                        }
                    }
                }

                if (e.CommandName == "DA_Command")
                {
                    if (ibApproved.ImageUrl == "~/images/A2.png" || ibClosed.ImageUrl == "~/images/Close4.png")
                    {
                        ibDisapproved.ImageUrl = "~/images/DA1.png";
                        txtRemarks.Text = string.Empty;
                        txtRemarks.Enabled = false;
                    }
                    else
                    {
                        if (ibDisapproved.ImageUrl == "~/images/DA1.png")
                        {
                            ibDisapproved.ImageUrl = "~/images/DA2.png";
                            txtRemarks.Enabled = true;

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

                                        //entityStatus.StatProdSecManager = eStat.StatProdSecManager;
                                        //entityStatus.StatProdDeptManager = eStat.StatProdDeptManager;
                                        //entityStatus.StatProdDivManager = eStat.StatProdDivManager;
                                        //entityStatus.StatProdHQManager = eStat.StatProdHQManager;
                                        entityStatus.StatPurchasingBuyer = eStat.StatPurchasingBuyer;
                                        entityStatus.StatPurchasingManager = eStat.StatPurchasingManager;

                                        // PROD SEC MANAGER
                                        if (eStat.StatSTATProdSecManager == "-1")
                                        {
                                            entityStatus.StatProdSecManager = "AUTO-APPROVED";
                                        }
                                        else
                                        {
                                            entityStatus.StatProdSecManager = eStat.StatProdSecManager;
                                        }
                                        // PROD DEPT MANAGER
                                        if (eStat.StatSTATProdDeptManager == "-1")
                                        {
                                            entityStatus.StatProdDeptManager = "AUTO-APPROVED";
                                        }
                                        else
                                        {
                                            entityStatus.StatProdDeptManager = eStat.StatProdDeptManager;
                                        }
                                        // PROD DIV MANAGER
                                        if (eStat.StatSTATProdDivManager == "-1")
                                        {
                                            entityStatus.StatProdDivManager = "AUTO-APPROVED";
                                        }
                                        else
                                        {
                                            entityStatus.StatProdDivManager = eStat.StatProdDivManager;
                                        }
                                        //// PROD HQ MANAGER
                                        //if (eStat.StatSTATProdHQManager == "-1")
                                        //{
                                        //    entityStatus.StatProdHQManager = "AUTO-APPROVED";
                                        //}
                                        //else
                                        //{
                                        //    entityStatus.StatProdHQManager = eStat.StatProdHQManager;
                                        //}

                                        if (statCounter <= 0)
                                        {
                                            listStatus.Add(entityStatus);

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

                        }
                        else
                        {
                            ibDisapproved.ImageUrl = "~/images/DA1.png";
                            txtRemarks.Text = string.Empty;
                            txtRemarks.Enabled = false;

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
                            lblReOpenRemarks.Visible = false;
                            lblSupplierAttachment.Visible = false;
                        }
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
                    LinkButton lbManualResponse = (LinkButton)e.Row.FindControl("lbManualResponse");
                    DropDownList ddSendDates = (DropDownList)e.Row.FindControl("ddSendDates");

                    lblStatAll.Style.Add("background-color", lblStatColor.Text.Trim());

                    if (lblStatAll.Text == "FOR RESEND" || lblStatAll.Text == "SUPPLIER RESPONDED")
                    {
                        lbManualResponse.Visible = true;
                    }


                    List<Entities_URF_RequestEntry> listApprover = new List<Entities_URF_RequestEntry>();
                    Entities_URF_RequestEntry eApprover = new Entities_URF_RequestEntry();
                    eApprover.RdCtrlNo = lblCTRLNo.Text.Trim();
                    listApprover = BLL.URF_TRANSACTION_GetRequestDetailsByCTRLNo(eApprover);

                    if (listApprover != null)
                    {
                        if (listApprover.Count > 0)
                        {
                            foreach (Entities_URF_RequestEntry eA in listApprover)
                            {

                                if (!string.IsNullOrEmpty(eA.SendDates))
                                {
                                    ddSendDates.Items.Clear();

                                    string[] values = eA.SendDates.Split(',');
                                    //Array.Sort<string>(values);

                                    for (int i = 0; i < values.Length; i++)
                                    {
                                        if (!string.IsNullOrEmpty(values[i].ToString()))
                                        {
                                            ddSendDates.Items.Add(values[i].ToString());
                                        }
                                    }

                                    ddSendDates.SelectedIndex = 0;

                                }
                            }                    
                            
                            //List<Entities_URF_RequestEntry> listSendDates = new List<Entities_URF_RequestEntry>();

                            //foreach (Entities_URF_RequestEntry eRequest in listApprover)
                            //{

                            //    // SEND DATES
                            //    if (!string.IsNullOrEmpty(eRequest.SendDates))
                            //    {
                            //        ddSendDates.Items.Clear();

                            //        var query = from val in eRequest.SendDates.Split(',')
                            //                    select val;

                            //        foreach (string str in query)
                            //        {
                            //            Entities_URF_RequestEntry entitySendDates = new Entities_URF_RequestEntry();
                            //            entitySendDates.SendDates = str;
                            //            listSendDates.Add(entitySendDates);
                            //        }

                            //    }

                            //}

                            //if (listSendDates.Count > 0)
                            //{                                

                            //    foreach (Entities_URF_RequestEntry eSendDates in listSendDates.OrderBy(itm => itm.SendDates).ToList())
                            //    {
                            //        ddSendDates.Items.Add(eSendDates.SendDates);
                            //    }

                            //    ddSendDates.SelectedIndex = 1;

                            //}
                        }
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


        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                string query1 = string.Empty;
                string query2 = string.Empty;
                string query3 = string.Empty;
                string query4 = string.Empty;
                int queryStatusCounter = 0;
                string querySuccess = string.Empty;
                string tempCtrlNo = string.Empty;
                string approvedBy = Session["LcRefId"].ToString();
                bool hasDisapproved = false;
                bool hasApproved = false;
                bool hasClosed = false;


                if (gvData.Rows.Count > 0)
                {
                    for (int i = 0; i < gvData.Rows.Count; i++)
                    {
                        Label lblCTRLNo = (Label)gvData.Rows[i].Cells[0].FindControl("lblCTRLNo");
                        Label lblReOpenRemarks2 = (Label)gvData.Rows[i].Cells[0].FindControl("lblReOpenRemarks2");
                        Label lblSupplierName = (Label)gvData.Rows[i].Cells[0].FindControl("lblSupplierName");
                        Label lblReasonName = (Label)gvData.Rows[i].Cells[0].FindControl("lblReasonName");
                        Label lblStatAll = (Label)gvData.Rows[i].Cells[3].FindControl("lblStatAll");
                        ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[4].FindControl("ibApproved");
                        ImageButton ibDisapproved = (ImageButton)gvData.Rows[i].Cells[4].FindControl("ibDisapproved");
                        ImageButton ibClosed = (ImageButton)gvData.Rows[i].Cells[4].FindControl("ibClosed");
                        TextBox txtRemarks = (TextBox)gvData.Rows[i].Cells[6].FindControl("txtRemarks");
                        Label lblCategory = (Label)gvData.Rows[i].Cells[1].FindControl("lblCategory");


                        if (ibApproved.ImageUrl == "~/images/A2.png")
                        {
                            string path = Server.MapPath("~/URF_Request/" + lblCTRLNo.Text.Trim() + "/" + lblCTRLNo.Text.Trim() + ".html");
                            //string pathPreview = Server.MapPath("~/URF_Request/" + lblCTRLNo.Text.Trim() + "/REPORT_" + lblCTRLNo.Text.Trim() + ".html");

                            //string path = Server.MapPath("~/URF_Request/" + lblCTRLNo.Text.Trim() + "/" + lblCTRLNo.Text.Trim() + ".csv");
                            string userManualPath = Server.MapPath("~/UserManual/URF NOTES.docx");
                            string htmlTemplate = Server.MapPath("~/UserManual/URF_Template.txt");
                            string csvHeader = string.Empty;
                            string csvDetails = string.Empty;
                            string csvNewLine = string.Empty;
                            string attachment1 = string.Empty;
                            string attachment2 = string.Empty;
                            string attachmentStockLife = string.Empty;
                            string attachmentPath1 = string.Empty;
                            string attachmentPath2 = string.Empty;
                            string attachmentPathStockLife = string.Empty;
                            string attachmentPath = string.Empty;
                            string emailService = string.Empty;
                            string supplierEmail = string.Empty;
                            string supplierId = string.Empty;
                            string supplierName = string.Empty;
                            string reason = string.Empty;
                            int recCounter = 0;
                            string reOpenRemarks = string.Empty;
                            string tableHeader = string.Empty;
                            string tableDetails = string.Empty;
                            string table = string.Empty;

                            
                            List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();
                            Entities_URF_RequestEntry entity = new Entities_URF_RequestEntry();

                            entity.RdCtrlNo = lblCTRLNo.Text.Trim();
                            list = BLL.URF_TRANSACTION_GetRequestDetailsByCTRLNo(entity);

                            if (list != null)
                            {
                                if (list.Count > 0)
                                {
                                    //csvHeader = "RefId,PONO,PRNO,ItemName,Specification,Quantity,UnitOfMeasure,DeliveryConfirmationDate,RequestedDeliveryDate,ReplyDeliveryDate";
                                    //csvNewLine = "\r\n";
                                    //foreach (Entities_URF_RequestEntry eCSV in list)
                                    //{
                                    //    csvDetails += eCSV.RdRefId + "," + eCSV.RdPONO + "," + eCSV.RdPRNO + "," + eCSV.RdItemName + "," +
                                    //                  eCSV.RdSpecs + "," + eCSV.RdQuantity + "," + eCSV.RdUnitOfMeasure + "," + eCSV.RdDeliveryConfirmationDate + "," +
                                    //                  eCSV.RdRequestedDeliveryDate + "," + eCSV.RdReplyDeliveryDate + csvNewLine;

                                    //    if (recCounter <= 0)
                                    //    {
                                    //        attachment1 = eCSV.RhAttachment1;
                                    //        attachment2 = eCSV.RhAttachment2;
                                    //        attachmentStockLife = eCSV.RhStockLifeAttachment;
                                    //        supplierEmail = eCSV.RhSupplierEmail;
                                    //        supplierId = eCSV.RhSupplierId;
                                    //        supplierName = eCSV.RhSupplier;
                                    //    }
                                    //    recCounter++;
                                    //}

                                    //if (!System.IO.File.Exists(path))
                                    //{
                                    //    using (StreamWriter writer = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write)))
                                    //    {
                                    //        writer.WriteLine(csvHeader + csvNewLine + csvDetails);
                                    //    }
                                    //}

                                    tableHeader = "<tr><th>REFID</th><th>PONO</th><th>PRNO</th><th>ITEM NAME</th><th>SPECIFICATION</th><th>QUANTITY</th><th>UNIT OF MEASURE</th><th>DEL.CONF.DATE</th><th>REQ.DEL.DATE</th><th>REPLY DELIVERY DATE</th></tr>";
                                    foreach (Entities_URF_RequestEntry eCSV in list)
                                    {
                                        if (recCounter <= 0)
                                        {
                                            attachment1 = eCSV.RhAttachment1;
                                            attachment2 = eCSV.RhAttachment2;
                                            attachmentStockLife = eCSV.RhStockLifeAttachment;
                                            supplierEmail = eCSV.RhSupplierEmail;
                                            supplierId = eCSV.RhSupplierId;
                                            supplierName = eCSV.RhSupplier;
                                            reason = eCSV.RhReason;
                                        }
                                        recCounter++;

                                        // TABLE CREATION
                                        tableDetails += "<tr><td>" + eCSV.RdRefId + "</td><td>" + eCSV.RdPONO + "</td><td>" + eCSV.RdPRNO + "</td><td>" + eCSV.RdItemName + "</td><td>" + eCSV.RdSpecs + "</td><td>" + eCSV.RdQuantity + "</td><td>" + eCSV.RdUOMDesc + "</td><td>" + eCSV.RdDeliveryConfirmationDate + "</td><td>" + eCSV.RdRequestedDeliveryDate + "</td><td><input type='text' id='rdd" + recCounter + "' name='rdd" + recCounter + "'></td></tr>";
                                    }

                                    table = "<table style='width:100%;'>" + tableHeader + tableDetails + "</table>";
                                }

                                if (System.IO.File.Exists(htmlTemplate))
                                {
                                    string templateValue = System.IO.File.ReadAllText(htmlTemplate).Replace("filename.csv", lblCTRLNo.Text.Trim() + ".csv")
                                                                                                   .Replace("val_ctrlno", lblCTRLNo.Text.Trim())
                                                                                                   .Replace("val_supplier", supplierName)
                                                                                                   .Replace("val_reason", reason)
                                                                                                   .Replace("val_table", table)
                                                                                                   .Replace("val_title", lblCTRLNo.Text.Trim());


                                    if (!System.IO.File.Exists(path))
                                    {
                                        using (StreamWriter writer = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write)))
                                        {
                                            writer.WriteLine(templateValue);
                                        }
                                    }
                                }

                            }
                                                        

                            if (!string.IsNullOrEmpty(attachment1))
                            {
                                attachmentPath1 = Server.MapPath("~/URF_Request/" + lblCTRLNo.Text.Trim() + "/" + attachment1);
                            }
                            if (!string.IsNullOrEmpty(attachment2))
                            {
                                attachmentPath2 = Server.MapPath("~/URF_Request/" + lblCTRLNo.Text.Trim() + "/" + attachment2);
                            }
                            if (!string.IsNullOrEmpty(attachmentStockLife))
                            {
                                attachmentPathStockLife = Server.MapPath("~/URF_Request/" + lblCTRLNo.Text.Trim() + "/" + attachmentStockLife);
                            }

                            if (!string.IsNullOrEmpty(attachmentPath1) && string.IsNullOrEmpty(attachmentPath2))
                            {
                                attachmentPath = path + "," + userManualPath + "," + attachmentPath1;
                                if (!string.IsNullOrEmpty(attachmentPathStockLife))
                                {
                                    attachmentPath = path + "," + userManualPath + "," + attachmentPath1 + "," + attachmentPathStockLife;
                                }
                            }
                            if (string.IsNullOrEmpty(attachmentPath1) && !string.IsNullOrEmpty(attachmentPath2))
                            {
                                attachmentPath = path + "," + userManualPath + "," + attachmentPath2;
                                if (!string.IsNullOrEmpty(attachmentPathStockLife))
                                {
                                    attachmentPath = path + "," + userManualPath + "," + attachmentPath2 + "," + attachmentPathStockLife;
                                }
                            }
                            if (!string.IsNullOrEmpty(attachmentPath1) && !string.IsNullOrEmpty(attachmentPath2))
                            {
                                attachmentPath = path + "," + userManualPath + "," + attachmentPath1 + "," + attachmentPath2;
                                if (!string.IsNullOrEmpty(attachmentPathStockLife))
                                {
                                    attachmentPath = path + "," + userManualPath + "," + attachmentPath1 + "," + attachmentPathStockLife;
                                }
                            }
                            if (string.IsNullOrEmpty(attachmentPath1) && string.IsNullOrEmpty(attachmentPath2))
                            {
                                attachmentPath = path + "," + userManualPath;
                            }
                            if (string.IsNullOrEmpty(attachmentPath1) && string.IsNullOrEmpty(attachmentPath2) && !string.IsNullOrEmpty(attachmentPathStockLife))
                            {
                                attachmentPath = path + "," + userManualPath + "," + attachmentPathStockLife;
                            }

                            //-------------------------------------------------------------------------------------------------------------------
                            // SET BUYER INFORMATION

                            List<Entities_RFQ_BuyerInformation> listBuyerInformation = new List<Entities_RFQ_BuyerInformation>();
                            listBuyerInformation = BLL_RFQ.RFQ_MT_BuyerInformation_GetAll();
                            string buyerInformation = string.Empty;

                            if (listBuyerInformation != null)
                            {
                                if (listBuyerInformation.Count > 0)
                                {
                                    string tableStart = "<table style='width:100%;'><tr><th align='left'>Purchasing Members</th><th align='left'>Section</th><th align='left'>Personal Email</th><th align='left'>Mobile Number</th></tr>";
                                    string tableEnd = "</table>";
                                    string information = string.Empty;
                                    foreach (Entities_RFQ_BuyerInformation eBI in listBuyerInformation)
                                    {
                                        information += "<tr><td>" + eBI.Member + "</td><td>" + eBI.Section + "</td><td>" + eBI.Email + "</td><td>" + eBI.Mobile + "</td></tr>";
                                    }

                                    buyerInformation = tableStart + information + tableEnd;
                                }
                            }

                            //-------------------------------------------------------------------------------------------------------------------

                            string fixedBuyerInfo = buyerInformation;


                            if (!string.IsNullOrEmpty(lblReOpenRemarks2.Text))
                            {
                                reOpenRemarks = lblReOpenRemarks2.Text;

                                emailService = COMMON.sendEmailToSuppliersURF(supplierEmail.Trim(), ConfigurationManager.AppSettings["email-username"], lblCTRLNo.Text.Trim(),
                                            "Hi <b>" + supplierName + "</b> Good Day!" + "<br /><br /> Kindly check the attached html file (" + lblCTRLNo.Text.Trim() + ".html) for our URF Request <b>RESENDING</b> (PLEASE READ THE RESENDING NOTE BELOW)" + "<br /><br /> NOTE : Please refer to the attached document file (URF NOTES.docx) on how to update or put your response accordingly. DO NOT PUT special character comma (,) in your response." +
                                            "<br /><br /> <b>RESENDING NOTE :</b> " + reOpenRemarks + "<br /><br />" +
                                            "<br /><br />For this URF, please contact <b>" + lblCategory.Text.ToUpper() + "</b> Section <br /> <b>PLEASE DO REPLY WITHIN 48 HOURS</b> <br /><br />" +
                                            "<br /><br /><br />Thank You!<br /><br /><br />" +
                                            "*** This is an automatically generated email, Please reply accordingly *** <br /> <br />" +
                                            "You have received a new URF Request from ROHM Electronics Philippines Inc. <br /> <br />" +
                                            "<br /> <br /><br /><br /> For inquries, kindly see below contact details : <br />" + fixedBuyerInfo, attachmentPath, supplierName, lblCTRLNo.Text.Trim());

                            }
                            else
                            {
                                emailService = COMMON.sendEmailToSuppliersURF(supplierEmail.Trim(), ConfigurationManager.AppSettings["email-username"], lblCTRLNo.Text.Trim(),
                                            "Hi <b>" + supplierName + "</b> Good Day!" + "<br /><br /> Kindly check the attached html file (" + lblCTRLNo.Text.Trim() + ".html) for our URF Request" + "<br /><br /> NOTE : Please refer to the attached document file (URF NOTES.docx) on how to update or put your response accordingly. DO NOT PUT special character comma (,) in your response." +
                                            "<br /><br />For this URF, please contact <b>" + lblCategory.Text.ToUpper() + "</b> Section <br /> <b>PLEASE DO REPLY WITHIN 48 HOURS</b> <br /><br />" +
                                            "<br /><br /><br />Thank You!<br /><br /><br />" +
                                            "*** This is an automatically generated email, Please reply accordingly *** <br /> <br />" +
                                            "You have received a new URF Request from ROHM Electronics Philippines Inc. <br /> <br />" +
                                            "<br /> <br /><br /><br /> For inquries, kindly see below contact details : <br />" + fixedBuyerInfo, attachmentPath, supplierName, lblCTRLNo.Text.Trim());

                            }
                            
                            if (emailService.ToLower().Contains("success"))
                            {
                                query1 += "INSERT INTO URF_TRANSACTION_SendReceived (CTRLNo, SendReceivedDate, TransactionType, SendBy) VALUES ('" + lblCTRLNo.Text.Trim() + "', GETDATE(), 'SEND','" + Session["UserFullName"].ToString() + "') ";
                                tempCtrlNo += "(" + lblCTRLNo.Text.Trim().ToUpper() + " - SUCCESSFULLY SENT) , ";                                
                            }
                            else
                            {
                                tempCtrlNo += "(" + lblCTRLNo.Text.Trim().ToUpper() + " - FAILED TO SEND) , ";
                            }

                            hasApproved = true;
                            queryStatusCounter++;
                        }

                        if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                        {
                            if (lblStatAll.Text.ToUpper() == "FOR SEC. MNGR APPROVAL")
                            {
                                query1 += "UPDATE URF_TRANSACTION_RequestStatus SET ProdSecManager = '" + approvedBy + "', DOAProdSecManager = CONVERT(VARCHAR, GETDATE(), 22), STATProdSecManager = '2', Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR DEPT. MNGR APPROVAL")
                            {
                                query1 += "UPDATE URF_TRANSACTION_RequestStatus SET ProdDeptManager = '" + approvedBy + "', DOAProdDeptManager = CONVERT(VARCHAR, GETDATE(), 22), STATProdDeptManager = '2', Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR DIV. MNGR APPROVAL")
                            {
                                query1 += "UPDATE URF_TRANSACTION_RequestStatus SET ProdDivManager = '" + approvedBy + "', DOAProdDivManager = CONVERT(VARCHAR, GETDATE(), 22), STATProdDivManager = '2', Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            //if (lblStatAll.Text.ToUpper() == "FOR HQ MNGR APPROVAL")
                            //{
                            //    query1 += "UPDATE URF_TRANSACTION_RequestStatus SET ProdHQManager = '" + approvedBy + "', DOAProdHQManager = CONVERT(VARCHAR, GETDATE(), 22), STATProdHQManager = '2', Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            //}
                            if (lblStatAll.Text.ToUpper() == "FOR PUR BUYER APPROVAL")
                            {
                                query1 += "UPDATE URF_TRANSACTION_RequestStatus SET PurchasingBuyer = '" + approvedBy + "', DOAPurchasingBuyer = CONVERT(VARCHAR, GETDATE(), 22), STATPurchasingBuyer = '2', Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR PUR MANAGER APPROVAL")
                            {
                                query1 += "UPDATE URF_TRANSACTION_RequestStatus SET PurchasingManager = '" + approvedBy + "', DOAPurchasingManager = CONVERT(VARCHAR, GETDATE(), 22), STATPurchasingManager = '2', Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR SENDING")
                            {
                                query1 += "UPDATE URF_TRANSACTION_RequestStatus SET PurchasingBuyer = '" + approvedBy + "', DOAPurchasingBuyer = CONVERT(VARCHAR, GETDATE(), 22), STATPurchasingBuyer = '2', DOAPurchasingManager = NULL, STATPurchasingManager = '0', PurchasingManager = NULL, Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }

                            queryStatusCounter++;
                            tempCtrlNo += lblCTRLNo.Text.Trim().ToUpper() + ", ";
                            hasDisapproved = true;
                        }

                        if (ibClosed.ImageUrl == "~/images/Close4.png")
                        {
                            query1 += "UPDATE URF_TRANSACTION_RequestStatus SET STATClosed = '1', PostingRemarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            
                            queryStatusCounter++;
                            tempCtrlNo += lblCTRLNo.Text.Trim().ToUpper() + ", ";
                            hasClosed = true;
                        }

                    }

                    if ((hasApproved && hasDisapproved) || (hasApproved && hasClosed) || (hasDisapproved && hasClosed) || (hasApproved && hasDisapproved && hasClosed))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You cannot approved, disapproved and closed items at the same time!');", true);
                    }                    
                    else
                    {
                        if (queryStatusCounter > 0)
                        {
                            string ctrlnoForClosed = string.Empty;

                            for (int i = 0; i < gvData.Rows.Count; i++)
                            {
                                Label lblCTRLNo = (Label)gvData.Rows[i].Cells[0].FindControl("lblCTRLNo");
                                TextBox txtRemarks = (TextBox)gvData.Rows[i].Cells[6].FindControl("txtRemarks");
                                ImageButton ibClosed = (ImageButton)gvData.Rows[i].Cells[4].FindControl("ibClosed");

                                if (hasClosed)
                                {
                                    if (string.IsNullOrEmpty(txtRemarks.Text) && ibClosed.ImageUrl == "~/images/Close4.png")
                                    {
                                        ctrlnoForClosed += lblCTRLNo.Text + ", ";
                                    }
                                }
                            }

                            if (string.IsNullOrEmpty(ctrlnoForClosed))
                            {

                                if (hasDisapproved)
                                {
                                    querySuccess = BLL.URF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                                    if (querySuccess == queryStatusCounter.ToString())
                                    {
                                        Session["successMessage"] = "URF NUMBER(S) : <b>" + tempCtrlNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
                                        Session["successTransactionName"] = "URF_RECEIVINGENTRY";
                                        Session["successReturnPage"] = "URF_ReceivingEntry.aspx";

                                        Response.Redirect("SuccessPage.aspx");
                                    }
                                }
                                if (hasApproved)
                                {
                                    querySuccess = BLL.URF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                                    if (querySuccess == queryStatusCounter.ToString() || tempCtrlNo.Contains("FAILED"))
                                    {
                                        Session["successMessage"] = "URF NUMBER(S) : <b>" + tempCtrlNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
                                        Session["successTransactionName"] = "URF_RECEIVINGENTRY";
                                        Session["successReturnPage"] = "URF_ReceivingEntry.aspx";

                                        Response.Redirect("SuccessPage.aspx");
                                    }
                                }
                                if (hasClosed)
                                {
                                    querySuccess = BLL.URF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                                    if (querySuccess == queryStatusCounter.ToString())
                                    {
                                        Session["successMessage"] = "URF NUMBER(S) : <b>" + tempCtrlNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
                                        Session["successTransactionName"] = "URF_RECEIVINGENTRY";
                                        Session["successReturnPage"] = "URF_ReceivingEntry.aspx";

                                        Response.Redirect("SuccessPage.aspx");
                                    }
                                }

                            }
                            else
                            {                                
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('CLOSE REMARKS FOR " + ctrlnoForClosed + " MUST NOT BE BLANK!');", true);
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select atleast 1 item to approved. No selected items for approval.');", true);
                        }
                    }

                }

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
