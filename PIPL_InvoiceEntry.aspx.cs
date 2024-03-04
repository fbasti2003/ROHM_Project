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
using SpreadsheetLight;
//using DocumentFormat.OpenXml.Packaging;


namespace REPI_PUR_SOFRA
{    
    public partial class PIPL_InvoiceEntry : System.Web.UI.Page
    {

        BLL_PIPL BLL = new BLL_PIPL();
        Common COMMON = new Common();
        BLL_Common BLLCommon = new BLL_Common();
        
        private string ddumitems;
        public string Ddumitems { get { return ddumitems; } set { ddumitems = value; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LcRefId"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
                else
                {

                    PIPL_TRANSACTION_InvoiceEntry_Fill_All_DropdownList();

                    if (!String.IsNullOrEmpty(Request.QueryString["ControlNo_From_Details"].ToString()))
                    {
                        LoadDefault();
                        fuExcelData.Visible = false;

                        Page.Title = CryptorEngine.Decrypt(Request.QueryString["ControlNo_From_Details"].ToString().Replace(" ", "+"), true);
                        //FirstGridViewRowForUpdate();
                    }
                    else
                    {
                        txtETDManila.Text = DateTime.Now.ToString("MM/dd/yyyy");
                        Ddumitems = "<asp:ListItem Text='NO' Value='0'></asp:ListItem>";
                        FirstGridViewRow();
                    }


                    int t1 = int.Parse(DateTime.Now.ToString("HHmm").ToString());
                    int t2 = int.Parse(ConfigurationManager.AppSettings["CUT-OFF-TIME"].ToString());

                    if (t1 > t2 && String.IsNullOrEmpty(Request.QueryString["ControlNo_From_Details"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('CUT-OFF TIME NOTIFICATION! Please be informed that this request is going be processed on the next day.');", true);
                    }

                }
            }
        }

        private string bdnValue(string val)
        {
            string ret = string.Empty;

            if (val == "0")
            {
                ret = "BUYER";
            }
            if (val == "1")
            {
                ret = "DELIVERED TO";
            }
            if (val == "2")
            {
                ret = "NOTIFY PARTY";
            }

            return ret;
        }

        private void LoadDefault()
        {
            try
            {
                List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();
                Entities_PIPL_InvoiceEntry details = new Entities_PIPL_InvoiceEntry();

                details.CtrlNo = CryptorEngine.Decrypt(Request.QueryString["ControlNo_From_Details"].ToString().Replace(" ", "+"), true);
                list = BLL.PIPL_TRANSACTION_RequestDetails_GetByControlNo(details);

                int counter = 0;

                ddCommercialValue.ClearSelection();
                ddConsignee.ClearSelection();
                ddCountryOfOrigin.ClearSelection();
                ddModeOfShipment.ClearSelection();
                ddNatureOfGoods.ClearSelection();
                ddPacking.ClearSelection();
                ddPickupLocation.ClearSelection();
                ddPurpose.ClearSelection();
                ddTradeTerms.ClearSelection();
                ddCategory.ClearSelection();
                ddBDNValue.ClearSelection();
                ddSalesType.ClearSelection();
                ddBusinessUnit.ClearSelection();
                ddAccountCode.ClearSelection();

                if (list.Count > 0)
                {

                    //TRANSFER DETAILS
                    BLL_RFQ BLLRFQ = new BLL_RFQ();

                    List<Entities_RFQ_RequestEntry> List_TransferHistory = new List<Entities_RFQ_RequestEntry>();
                    Entities_RFQ_RequestEntry entityTransferHistory = new Entities_RFQ_RequestEntry();
                    entityTransferHistory.SearchCriteria = details.CtrlNo;
                    List_TransferHistory = BLLRFQ.RFQ_TRANSACTION_HistoryOfUpdates(entityTransferHistory);

                    if (List_TransferHistory != null)
                    {
                        if (List_TransferHistory.Count > 0)
                        {
                            gvTransferDetails.DataSource = List_TransferHistory;
                            gvTransferDetails.DataBind();

                            tableTransferDetails.Style.Add("display", "block");
                        }
                    }

                    foreach (Entities_PIPL_InvoiceEntry entity in list)
                    {
                        Session["PIPL_OLD_CATEGORY_DESCRIPTION"] = entity.Category;

                        ddConsignee.Items.FindByText(entity.Consignee).Selected = true;
                        ddModeOfShipment.Items.FindByText(entity.ModeOfShipment).Selected = true;
                        ddPurpose.Items.FindByText(entity.Purpose).Selected = true;
                        ddPacking.Items.FindByText(entity.Packing).Selected = true;

                        ddNatureOfGoods.Items.FindByText(entity.NatureOfGoods).Selected = true;
                        ddCountryOfOrigin.Items.FindByText(entity.CountryOfOrigin).Selected = true;
                        ddCategory.Items.FindByText(entity.Category).Selected = true;
                        ddCommercialValue.Items.FindByText(entity.CommercialValue).Selected = true;                        

                        ddTradeTerms.Items.FindByText(entity.TradeTerms).Selected = true;

                        ddPickupLocation.Items.FindByText(entity.PickUpLocation).Selected = true;
                        ddBDN.Items.FindByText(bdnValue(entity.Bdn)).Selected = true;
                        ddBDNValue.Items.FindByText(entity.BdnValue).Selected = true;

                        txtAttention1.Text = entity.Attention1;
                        txtSectionDept1.Text = entity.Secdept1;
                        lblRequester.Text = entity.Requester;
                        txtPortOfDestination.Text = entity.PortOfDestination;
                        txtETDManila.Text = entity.Etd;
                        txtEstimatedYen.Text = entity.ValueInYen;
                        txtEstimatedUsd.Text = entity.ValueInUsd;
                        txtPONumber.Text = entity.PoNo;
                        txtInvoiceNumber.Text = entity.InvoiceNo;
                        txtAttention2.Text = entity.Attention2;
                        txtReferenceNo.Text = entity.ReferenceNo;
                        txtRemarks.Text = entity.Remarks;
                        txtPurposeOthers.Text = entity.PurposeOthers;
                        txtPurposeOthers.Enabled = txtPurposeOthers.Text.Length <= 0 ? false : true;

                        if (!string.IsNullOrEmpty(entity.BusinessUnit))
                        {                            
                            ddBusinessUnit.Items.FindByText(entity.BusinessUnit).Selected = true;
                        }
                        if (!string.IsNullOrEmpty(entity.AccountCode))
                        {
                            ddAccountCode.Items.FindByText(entity.AccountCode).Selected = true;
                        }
                        if (!string.IsNullOrEmpty(entity.SalesType))
                        {
                            ddSalesType.Items.FindByValue(entity.SalesType).Selected = true;
                        }

                        if (entity.TradeTerms == "CIF")
                        {
                            lblInsurance.Text = "ALL COVERED BY SHIPPER";
                        }
                        if (entity.TradeTerms == "FOB")
                        {
                            lblInsurance.Text = "ALL COVERED BY BUYER/CONSIGNEE";
                        }
                        if (entity.TradeTerms == "DDP (FREEHOUSE)")
                        {
                            lblInsurance.Text = "ALL COVERED BY SHIPPER";
                        }
                        if (entity.TradeTerms == "EX-WORKS")
                        {
                            lblInsurance.Text = "ALL COVERED BY BUYER/CONSIGNEE";
                        }
                        if (entity.TradeTerms == "DAP")
                        {
                            lblInsurance.Text = "ALL COVERED BY SHIPPER";
                        }

                        //--------------------------------------------------------------------------------------------
                        if (ddCommercialValue.SelectedItem.Text.Trim() == "WITH")
                        {
                            ddSalesType.Enabled = true;
                            ddAccountCode.Enabled = true;
                            ddBusinessUnit.Enabled = true;
                        }
                        else
                        {
                            ddSalesType.SelectedValue = "1";
                            ddAccountCode.SelectedValue = "";
                            ddBusinessUnit.SelectedValue = "";
                            ddSalesType.Enabled = false;
                            ddAccountCode.Enabled = false;
                            ddBusinessUnit.Enabled = false;                            
                        }

                        if (ddBDNValue.SelectedItem.Text.Length > 0)
                        {
                            ddBDNValue.Enabled = true;
                        }
                        //--------------------------------------------------------------------------------------------

                        counter++;
                    }

                    //Entities_PIPL_InvoiceEntry dummy = new Entities_PIPL_InvoiceEntry();
                    //dummy.DetailsRefId = string.Empty;
                    //dummy.CaseUnit = string.Empty;
                    //dummy.CaseNumber = string.Empty;
                    //dummy.Description = string.Empty;
                    //dummy.Specification = string.Empty;
                    //dummy.Quantity = string.Empty;
                    //dummy.Uom = string.Empty;
                    //dummy.Currency = string.Empty;
                    //dummy.UPrice = string.Empty;
                    //dummy.NetWeight = string.Empty;
                    //dummy.GrossWeight = string.Empty;
                    //dummy.Measurement = string.Empty;                    

                    //list.Add(dummy);

                    //ViewState["CurrentTable"] = dummy;

                    ViewState["CurrentTable"] = list;

                    gvData.Visible = false;
                    gvDataUpdate.Visible = true;
                    gvDataUpdate.DataSource = list;
                    gvDataUpdate.DataBind();
                    

                    if (gvDataUpdate.Visible)
                    {
                        Session["BiggerHeight"] = "22";
                        Session["BiggerFontSize"] = "9";
                        Session["TextMode"] = "SINGLE";

                        for (int i = 0; i < gvDataUpdate.Rows.Count; i++)
                        {

                            Label lblUpdatedRefId = (Label)gvDataUpdate.Rows[i].Cells[0].FindControl("lblUpdatedRefId");
                            TextBox txtUpdatedCaseNumber = (TextBox)gvDataUpdate.Rows[i].Cells[2].FindControl("txtUpdatedCaseNumber");
                            TextBox txtUpdatedDescription = (TextBox)gvDataUpdate.Rows[i].Cells[3].FindControl("txtUpdatedDescription");
                            TextBox txtUpdatedSpecification = (TextBox)gvDataUpdate.Rows[i].Cells[4].FindControl("txtUpdatedSpecification");
                            TextBox txtUpdatedQuantity = (TextBox)gvDataUpdate.Rows[i].Cells[5].FindControl("txtUpdatedQuantity");
                            TextBox txtUpdatedUnitPrice = (TextBox)gvDataUpdate.Rows[i].Cells[8].FindControl("txtUpdatedUnitPrice");
                            TextBox txtUpdatedNetWeight = (TextBox)gvDataUpdate.Rows[i].Cells[9].FindControl("txtUpdatedNetWeight");
                            TextBox txtUpdatedGrossWeight = (TextBox)gvDataUpdate.Rows[i].Cells[10].FindControl("txtUpdatedGrossWeight");
                            TextBox txtUpdatedMeasurement = (TextBox)gvDataUpdate.Rows[i].Cells[11].FindControl("txtUpdatedMeasurement");


                            DropDownList ddUpdatedUOM = (DropDownList)gvDataUpdate.Rows[i].Cells[6].FindControl("ddUpdatedUOM");
                            DropDownList ddUpdatedCurrency = (DropDownList)gvDataUpdate.Rows[i].Cells[7].FindControl("ddUpdatedCurrency");
                            DropDownList ddUpdatedCaseUnit = (DropDownList)gvDataUpdate.Rows[i].Cells[1].FindControl("ddUpdatedCaseUnit");

                            ddUpdatedCaseUnit.Height = int.Parse(Session["BiggerHeight"].ToString());
                            ddUpdatedCaseUnit.Font.Size = int.Parse(Session["BiggerFontSize"].ToString());
                            ddUpdatedUOM.Height = int.Parse(Session["BiggerHeight"].ToString());
                            ddUpdatedUOM.Font.Size = int.Parse(Session["BiggerFontSize"].ToString());
                            ddUpdatedCurrency.Height = int.Parse(Session["BiggerHeight"].ToString());
                            ddUpdatedCurrency.Font.Size = int.Parse(Session["BiggerFontSize"].ToString());

                            txtUpdatedCaseNumber.Height = int.Parse(Session["BiggerHeight"].ToString());
                            if (Session["TextMode"].ToString() == "MULTILINE")
                            {
                                txtUpdatedCaseNumber.TextMode = TextBoxMode.MultiLine;
                                txtUpdatedDescription.TextMode = TextBoxMode.MultiLine;
                                txtUpdatedSpecification.TextMode = TextBoxMode.MultiLine;
                                txtUpdatedNetWeight.TextMode = TextBoxMode.MultiLine;
                                txtUpdatedGrossWeight.TextMode = TextBoxMode.MultiLine;
                                txtUpdatedMeasurement.TextMode = TextBoxMode.MultiLine;
                            }
                            else
                            {
                                txtUpdatedCaseNumber.TextMode = TextBoxMode.SingleLine;
                                txtUpdatedDescription.TextMode = TextBoxMode.SingleLine;
                                txtUpdatedSpecification.TextMode = TextBoxMode.SingleLine;
                                txtUpdatedNetWeight.TextMode = TextBoxMode.SingleLine;
                                txtUpdatedGrossWeight.TextMode = TextBoxMode.SingleLine;
                                txtUpdatedMeasurement.TextMode = TextBoxMode.SingleLine;
                            }


                            txtUpdatedDescription.Height = int.Parse(Session["BiggerHeight"].ToString());


                            txtUpdatedSpecification.Height = int.Parse(Session["BiggerHeight"].ToString());

                            txtUpdatedQuantity.Height = int.Parse(Session["BiggerHeight"].ToString());
                            txtUpdatedQuantity.Font.Size = int.Parse(Session["BiggerFontSize"].ToString());
                            txtUpdatedUnitPrice.Height = int.Parse(Session["BiggerHeight"].ToString());
                            txtUpdatedUnitPrice.Font.Size = int.Parse(Session["BiggerFontSize"].ToString());


                            txtUpdatedNetWeight.Height = int.Parse(Session["BiggerHeight"].ToString());
                            txtUpdatedGrossWeight.Height = int.Parse(Session["BiggerHeight"].ToString());
                            txtUpdatedMeasurement.Height = int.Parse(Session["BiggerHeight"].ToString());

                        }

                    }

                }

                //------------------------------------------------------------------------------------------------

                // ACCOUNTING
                if (BLLCommon.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), "215").Count > 0)
                {
                    // SET OTHER TO DISABLED. ONLY SALES TYPE; BUSINESS UNIT; ACCOUNT CODE ARE ENABLED

                    ddCommercialValue.Enabled = false;
                    ddConsignee.Enabled = false;
                    ddCountryOfOrigin.Enabled = false;
                    ddModeOfShipment.Enabled = false;
                    ddNatureOfGoods.Enabled = false;
                    ddPacking.Enabled = false;
                    ddPickupLocation.Enabled = false;
                    ddPurpose.Enabled = false;
                    ddTradeTerms.Enabled = false;
                    ddCategory.Enabled = false;
                    ddBDNValue.Enabled = false;
                    ddSalesType.Enabled = true;
                    ddBusinessUnit.Enabled = true;
                    ddAccountCode.Enabled = true;

                    txtAttention1.Enabled = false;
                    txtSectionDept1.Enabled = false;
                    lblRequester.Enabled = false;
                    txtPortOfDestination.Enabled = false;
                    txtETDManila.Enabled = false;
                    txtEstimatedYen.Enabled = false;
                    txtEstimatedUsd.Enabled = false;
                    txtPONumber.Enabled = false;
                    txtInvoiceNumber.Enabled = false;
                    txtAttention2.Enabled = false;
                    txtReferenceNo.Enabled = false;
                    txtRemarks.Enabled = false;
                    txtPurposeOthers.Enabled = false;
                    gvDataUpdate.Enabled = false;
                    ddBiggerView.Enabled = false;
                }
                

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "/n/n" + ex.InnerException + "/n/n" + ex.Source + "/n/n" + ex.StackTrace + "');", true);
            }
        }

        private void PIPL_TRANSACTION_InvoiceEntry_Fill_All_DropdownList()
        {
            List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();
            list = BLL.PIPL_TRANSACTION_InvoiceEntry_Fill_All_DropdownList().OrderBy(x => x.DropdownName).ToList();

            ddCommercialValue.Items.Add("");
            ddConsignee.Items.Add("");
            ddCountryOfOrigin.Items.Add("");
            ddModeOfShipment.Items.Add("");
            ddNatureOfGoods.Items.Add("");
            ddPacking.Items.Add("");
            ddPickupLocation.Items.Add("");
            ddPurpose.Items.Add("");
            ddTradeTerms.Items.Add("");
            ddCategory.Items.Add("");
            ddBDNValue.Items.Add("");

            ddBusinessUnit.Items.Add("");
            ddAccountCode.Items.Add("");

            BLL_Common BLLCommon = new BLL_Common();

            List<Entities_Common_IntegrationFromOtherDatabase> listBusinessUnit = new List<Entities_Common_IntegrationFromOtherDatabase>();
            listBusinessUnit = BLLCommon.Common_getBusinessUnit();

            if (listBusinessUnit.Count > 0)
            {
                foreach (Entities_Common_IntegrationFromOtherDatabase entity in listBusinessUnit)
                {
                    ListItem item = new ListItem();
                    item.Text = entity.Description;
                    item.Value = entity.Refid;
                    ddBusinessUnit.Items.Add(item);
                }
            }

            List<Entities_Common_IntegrationFromOtherDatabase> listAccountCode = new List<Entities_Common_IntegrationFromOtherDatabase>();
            listAccountCode = BLLCommon.Common_getAccountCode();

            if (listAccountCode.Count > 0)
            {
                foreach (Entities_Common_IntegrationFromOtherDatabase entity in listAccountCode)
                {
                    ListItem item = new ListItem();
                    item.Text = entity.Description;
                    item.Value = entity.Refid;
                    ddAccountCode.Items.Add(item);
                }
            }

            foreach (Entities_PIPL_InvoiceEntry entity in list)
            {
                ListItem item = new ListItem();
                item.Text = entity.DropdownName;
                item.Value = entity.DropdownRefId;

                if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                {
                    if (entity.TableName == "PIPL_MT_Company")
                    {
                        ddConsignee.Items.Add(item);
                    }
                    if (entity.TableName == "PIPL_MT_ModeOfShipment")
                    {
                        ddModeOfShipment.Items.Add(item);
                    }
                    if (entity.TableName == "PIPL_MT_CommercialValue")
                    {
                        ddCommercialValue.Items.Add(item);
                    }
                    if (entity.TableName == "PIPL_MT_TradeTerms")
                    {
                        ddTradeTerms.Items.Add(item);
                    }
                    if (entity.TableName == "PIPL_MT_PickUpLocation")
                    {
                        ddPickupLocation.Items.Add(item);
                    }
                    if (entity.TableName == "PIPL_MT_Purpose")
                    {
                        ddPurpose.Items.Add(item);
                    }
                    if (entity.TableName == "PIPL_MT_Packing")
                    {
                        ddPacking.Items.Add(item);
                    }
                    if (entity.TableName == "PIPL_MT_NatureOfGoods")
                    {
                        ddNatureOfGoods.Items.Add(item);
                    }
                    if (entity.TableName == "PIPL_MT_CountryOfOrigin")
                    {
                        ddCountryOfOrigin.Items.Add(item);
                    }
                    if (entity.TableName == "MT_Category")
                    {
                        if (!String.IsNullOrEmpty(Request.QueryString["ControlNo_From_Details"].ToString()))
                        {
                            if (entity.DropdownRefId == "1013" || entity.DropdownRefId == "1006")
                            {
                                //DO NOT ADD THIS CATEGORY
                            }
                            else
                            {
                                ddCategory.Items.Add(item);
                            }
                        }
                        else
                        {
                            if (entity.DropdownRefId == "1013" || entity.DropdownRefId == "1006"
                                        || entity.DropdownRefId == "1015" || entity.DropdownRefId == "3" || entity.DropdownRefId == "1"
                                        || entity.DropdownRefId == "7" || entity.DropdownRefId == "1014" || entity.DropdownRefId == "1019")
                            {
                                //DO NOT ADD THIS CATEGORY
                            }
                            else
                            {
                                ddCategory.Items.Add(item);
                            }
                        }
                    }
                    if (entity.TableName == "BDNValue")
                    {
                        ddBDNValue.Items.Add(item);
                    }
                }
            }

            lblRequester.Text = Session["UserFullName"].ToString().ToUpper();

        }


        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddUOM = (DropDownList)e.Row.FindControl("ddUOM");
                    DropDownList ddCurrency = (DropDownList)e.Row.FindControl("ddCurrency");
                    DropDownList ddCaseUnit = (DropDownList)e.Row.FindControl("ddCaseUnit");

                    List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();
                    list = BLL.PIPL_TRANSACTION_InvoiceEntry_Fill_All_DropdownList();

                    if (list.Count > 0)
                    {
                        foreach (Entities_PIPL_InvoiceEntry entity in list)
                        {
                            ListItem item = new ListItem();
                            item.Text = entity.DropdownName;
                            item.Value = entity.DropdownRefId;

                            if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                            {
                                if (entity.TableName == "MT_UnitOfMeasure")
                                {
                                    ddUOM.Items.Add(item);
                                }
                                if (entity.TableName == "MT_Currency")
                                {
                                    ddCurrency.Items.Add(item);
                                }
                                if (entity.TableName == "PIPL_MT_CaseUnit")
                                {
                                    ddCaseUnit.Items.Add(item);
                                }
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvDataUpdate_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblUpdatedUOM = (Label)e.Row.FindControl("lblUpdatedUOM");
                    Label lblUpdatedCurrency = (Label)e.Row.FindControl("lblUpdatedCurrency");
                    Label lblUpdatedCaseUnit = (Label)e.Row.FindControl("lblUpdatedCaseUnit");

                    DropDownList ddUpdatedUOM = (DropDownList)e.Row.FindControl("ddUpdatedUOM");
                    DropDownList ddUpdatedCurrency = (DropDownList)e.Row.FindControl("ddUpdatedCurrency");
                    DropDownList ddUpdatedCaseUnit = (DropDownList)e.Row.FindControl("ddUpdatedCaseUnit");

                    List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();
                    list = BLL.PIPL_TRANSACTION_InvoiceEntry_Fill_All_DropdownList();

                    if (list.Count > 0)
                    {
                        foreach (Entities_PIPL_InvoiceEntry entity in list)
                        {
                            ListItem item = new ListItem();
                            item.Text = entity.DropdownName;
                            item.Value = entity.DropdownRefId;

                            if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                            {
                                if (entity.TableName == "MT_UnitOfMeasure")
                                {
                                    ddUpdatedUOM.Items.Add(item);
                                }
                                if (entity.TableName == "MT_Currency")
                                {
                                    ddUpdatedCurrency.Items.Add(item);
                                }
                                if (entity.TableName == "PIPL_MT_CaseUnit")
                                {
                                    ddUpdatedCaseUnit.Items.Add(item);
                                }
                            }

                        }
                    }

                    ddUpdatedUOM.Items.FindByText(lblUpdatedUOM.Text).Selected = true;
                    ddUpdatedCurrency.Items.FindByText(lblUpdatedCurrency.Text).Selected = true;
                    ddUpdatedCaseUnit.Items.FindByText(lblUpdatedCaseUnit.Text).Selected = true;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvDataUpload_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblUploadUOM = (Label)e.Row.FindControl("lblUploadUOM");
                    Label lblUploadCurrency = (Label)e.Row.FindControl("lblUploadCurrency");
                    Label lblUploadCaseUnit = (Label)e.Row.FindControl("lblUploadCaseUnit");

                    DropDownList ddUploadUOM = (DropDownList)e.Row.FindControl("ddUploadUOM");
                    DropDownList ddUploadCurrency = (DropDownList)e.Row.FindControl("ddUploadCurrency");
                    DropDownList ddUploadCaseUnit = (DropDownList)e.Row.FindControl("ddUploadCaseUnit");

                    List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();
                    list = BLL.PIPL_TRANSACTION_InvoiceEntry_Fill_All_DropdownList();

                    if (list.Count > 0)
                    {
                        foreach (Entities_PIPL_InvoiceEntry entity in list)
                        {
                            ListItem item = new ListItem();
                            item.Text = entity.DropdownName;
                            item.Value = entity.DropdownRefId;

                            if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                            {
                                if (entity.TableName == "MT_UnitOfMeasure")
                                {
                                    ddUploadUOM.Items.Add(item);
                                }
                                if (entity.TableName == "MT_Currency")
                                {
                                    ddUploadCurrency.Items.Add(item);
                                }
                                if (entity.TableName == "PIPL_MT_CaseUnit")
                                {
                                    ddUploadCaseUnit.Items.Add(item);
                                }
                            }

                        }
                    }

                    ddUploadUOM.Items.FindByText(lblUploadUOM.Text).Selected = true;
                    ddUploadCurrency.Items.FindByText(lblUploadCurrency.Text).Selected = true;
                    ddUploadCaseUnit.Items.FindByText(lblUploadCaseUnit.Text).Selected = true;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Col1", typeof(string)));
            dt.Columns.Add(new DataColumn("Col2", typeof(string)));
            dt.Columns.Add(new DataColumn("Col3", typeof(string)));
            dt.Columns.Add(new DataColumn("Col4", typeof(string)));
            dt.Columns.Add(new DataColumn("Col5", typeof(string)));
            dt.Columns.Add(new DataColumn("Col6", typeof(string)));
            dt.Columns.Add(new DataColumn("Col7", typeof(string)));
            dt.Columns.Add(new DataColumn("Col8", typeof(string)));
            dt.Columns.Add(new DataColumn("Col9", typeof(string)));
            dt.Columns.Add(new DataColumn("Col10", typeof(string)));
            dt.Columns.Add(new DataColumn("Col11", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Col1"] = string.Empty;
            dr["Col2"] = string.Empty;
            dr["Col3"] = string.Empty;
            dr["Col4"] = string.Empty;
            dr["Col5"] = string.Empty;
            dr["Col6"] = string.Empty;
            dr["Col7"] = string.Empty;
            dr["Col8"] = string.Empty;
            dr["Col9"] = string.Empty;
            dr["Col10"] = string.Empty;
            dr["Col11"] = string.Empty;


            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;

            gvData.DataSource = dt;
            gvData.DataBind();

            if (ViewState["CurrentTable"] != null)
            {
                int rowIndex = 0;
                Session["BiggerHeight"] = "22";
                Session["BiggerFontSize"] = "9";

                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList ddCaseUnit =
                          (DropDownList)gvData.Rows[rowIndex].Cells[1].FindControl("ddCaseUnit");
                        TextBox txtCaseNumber =
                          (TextBox)gvData.Rows[rowIndex].Cells[2].FindControl("txtCaseNumber");
                        TextBox txtDescription =
                          (TextBox)gvData.Rows[rowIndex].Cells[3].FindControl("txtDescription");
                        TextBox txtSpecification =
                          (TextBox)gvData.Rows[rowIndex].Cells[4].FindControl("txtSpecification");
                        TextBox txtQuantity =
                          (TextBox)gvData.Rows[rowIndex].Cells[5].FindControl("txtQuantity");
                        DropDownList ddUOM =
                          (DropDownList)gvData.Rows[rowIndex].Cells[6].FindControl("ddUOM");
                        DropDownList ddCurrency =
                          (DropDownList)gvData.Rows[rowIndex].Cells[7].FindControl("ddCurrency");
                        TextBox txtUnitPrice =
                          (TextBox)gvData.Rows[rowIndex].Cells[8].FindControl("txtUnitPrice");
                        TextBox txtNetWeight =
                          (TextBox)gvData.Rows[rowIndex].Cells[9].FindControl("txtNetWeight");
                        TextBox txtGrossWeight =
                          (TextBox)gvData.Rows[rowIndex].Cells[10].FindControl("txtGrossWeight");
                        TextBox txtMeasurement =
                          (TextBox)gvData.Rows[rowIndex].Cells[11].FindControl("txtMeasurement");
                        TextBox txtFileSize =
                          (TextBox)gvData.Rows[rowIndex].Cells[12].FindControl("txtFileSize");
                        TextBox txtFileName =
                          (TextBox)gvData.Rows[rowIndex].Cells[13].FindControl("txtFileName");


                        ddCaseUnit.Height = int.Parse(Session["BiggerHeight"].ToString());
                        ddCaseUnit.Font.Size = int.Parse(Session["BiggerFontSize"].ToString());
                        ddUOM.Height = int.Parse(Session["BiggerHeight"].ToString());
                        ddUOM.Font.Size = int.Parse(Session["BiggerFontSize"].ToString()); 
                        ddCurrency.Height = int.Parse(Session["BiggerHeight"].ToString());
                        ddCurrency.Font.Size = int.Parse(Session["BiggerFontSize"].ToString());
                        
                        txtCaseNumber.Height = int.Parse(Session["BiggerHeight"].ToString());
                        txtDescription.Height = int.Parse(Session["BiggerHeight"].ToString());
                        txtSpecification.Height =int.Parse(Session["BiggerHeight"].ToString());

                        txtQuantity.Height = int.Parse(Session["BiggerHeight"].ToString());
                        txtQuantity.Font.Size = int.Parse(Session["BiggerFontSize"].ToString());
                        txtUnitPrice.Height = int.Parse(Session["BiggerHeight"].ToString());
                        txtUnitPrice.Font.Size = int.Parse(Session["BiggerFontSize"].ToString());

                        txtNetWeight.Height = int.Parse(Session["BiggerHeight"].ToString());
                        txtGrossWeight.Height = int.Parse(Session["BiggerHeight"].ToString());
                        txtMeasurement.Height = int.Parse(Session["BiggerHeight"].ToString());                        
                        

                    }

                }
            }

        }

        private void FirstGridViewRowForUpdate()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Col1", typeof(string)));
            dt.Columns.Add(new DataColumn("Col2", typeof(string)));
            dt.Columns.Add(new DataColumn("Col3", typeof(string)));
            dt.Columns.Add(new DataColumn("Col4", typeof(string)));
            dt.Columns.Add(new DataColumn("Col5", typeof(string)));
            dt.Columns.Add(new DataColumn("Col6", typeof(string)));
            dt.Columns.Add(new DataColumn("Col7", typeof(string)));
            dt.Columns.Add(new DataColumn("Col8", typeof(string)));
            dt.Columns.Add(new DataColumn("Col9", typeof(string)));
            dt.Columns.Add(new DataColumn("Col10", typeof(string)));
            dt.Columns.Add(new DataColumn("Col11", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Col1"] = string.Empty;
            dr["Col2"] = string.Empty;
            dr["Col3"] = string.Empty;
            dr["Col4"] = string.Empty;
            dr["Col5"] = string.Empty;
            dr["Col6"] = string.Empty;
            dr["Col7"] = string.Empty;
            dr["Col8"] = string.Empty;
            dr["Col9"] = string.Empty;
            dr["Col10"] = string.Empty;
            dr["Col11"] = string.Empty;


            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;

            gvDataUpdate.DataSource = dt;
            gvDataUpdate.DataBind();

            
            

        }

        private void AddNewRow()
        {
            int rowIndex = 0;

            List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();
            list = BLL.PIPL_TRANSACTION_InvoiceEntry_Fill_All_DropdownList();

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList ddCaseUnit =
                          (DropDownList)gvData.Rows[rowIndex].Cells[1].FindControl("ddCaseUnit");
                        TextBox txtCaseNumber =
                          (TextBox)gvData.Rows[rowIndex].Cells[2].FindControl("txtCaseNumber");
                        TextBox txtDescription =
                          (TextBox)gvData.Rows[rowIndex].Cells[3].FindControl("txtDescription");
                        TextBox txtSpecification =
                          (TextBox)gvData.Rows[rowIndex].Cells[4].FindControl("txtSpecification");
                        TextBox txtQuantity =
                          (TextBox)gvData.Rows[rowIndex].Cells[5].FindControl("txtQuantity");
                        DropDownList ddUOM =
                          (DropDownList)gvData.Rows[rowIndex].Cells[6].FindControl("ddUOM");
                        DropDownList ddCurrency =
                          (DropDownList)gvData.Rows[rowIndex].Cells[7].FindControl("ddCurrency");
                        TextBox txtUnitPrice =
                          (TextBox)gvData.Rows[rowIndex].Cells[8].FindControl("txtUnitPrice");
                        TextBox txtNetWeight =
                          (TextBox)gvData.Rows[rowIndex].Cells[9].FindControl("txtNetWeight");
                        TextBox txtGrossWeight =
                          (TextBox)gvData.Rows[rowIndex].Cells[10].FindControl("txtGrossWeight");
                        TextBox txtMeasurement =
                          (TextBox)gvData.Rows[rowIndex].Cells[11].FindControl("txtMeasurement");
                        TextBox txtFileSize =
                          (TextBox)gvData.Rows[rowIndex].Cells[12].FindControl("txtFileSize");
                        TextBox txtFileName =
                          (TextBox)gvData.Rows[rowIndex].Cells[13].FindControl("txtFileName");

                        if (list.Count > 0)
                        {
                            foreach (Entities_PIPL_InvoiceEntry entity in list)
                            {
                                ListItem item = new ListItem();
                                item.Text = entity.DropdownName;
                                item.Value = entity.DropdownRefId;

                                if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                                {
                                    if (entity.TableName == "MT_UnitOfMeasure")
                                    {
                                        ddUOM.Items.Add(item);
                                    }
                                    if (entity.TableName == "MT_Currency")
                                    {
                                        ddCurrency.Items.Add(item);
                                    }
                                    if (entity.TableName == "PIPL_MT_CaseUnit")
                                    {
                                        ddCaseUnit.Items.Add(item);
                                    }
                                }

                            }
                        }

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["Col1"] = ddCaseUnit.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Col2"] = txtCaseNumber.Text;
                        dtCurrentTable.Rows[i - 1]["Col3"] = txtDescription.Text;
                        dtCurrentTable.Rows[i - 1]["Col4"] = txtSpecification.Text;
                        dtCurrentTable.Rows[i - 1]["Col5"] = txtQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["Col6"] = ddUOM.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Col7"] = ddCurrency.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Col8"] = txtUnitPrice.Text;
                        dtCurrentTable.Rows[i - 1]["Col9"] = txtNetWeight.Text;
                        dtCurrentTable.Rows[i - 1]["Col10"] = txtGrossWeight.Text;
                        dtCurrentTable.Rows[i - 1]["Col11"] = txtMeasurement.Text; ;
                        rowIndex++;

                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;


                    gvData.DataSource = dtCurrentTable;
                    gvData.DataBind();

                }
            }
            else
            {
                Response.Write("ViewState is null");
            }


            SetPreviousData();
        }

        private void AddNewRowForUpdate()
        {
            int rowIndex = 0;

            List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();
            list = BLL.PIPL_TRANSACTION_InvoiceEntry_Fill_All_DropdownList();

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList ddCaseUnit =
                          (DropDownList)gvDataUpdate.Rows[rowIndex].Cells[1].FindControl("ddCaseUnit");
                        TextBox txtCaseNumber =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[2].FindControl("txtCaseNumber");
                        TextBox txtDescription =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[3].FindControl("txtDescription");
                        TextBox txtSpecification =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[4].FindControl("txtSpecification");
                        TextBox txtQuantity =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[5].FindControl("txtQuantity");
                        DropDownList ddUOM =
                          (DropDownList)gvDataUpdate.Rows[rowIndex].Cells[6].FindControl("ddUOM");
                        DropDownList ddCurrency =
                          (DropDownList)gvDataUpdate.Rows[rowIndex].Cells[7].FindControl("ddCurrency");
                        TextBox txtUnitPrice =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[8].FindControl("txtUnitPrice");
                        TextBox txtNetWeight =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[9].FindControl("txtNetWeight");
                        TextBox txtGrossWeight =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[10].FindControl("txtGrossWeight");
                        TextBox txtMeasurement =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[11].FindControl("txtMeasurement");
                        TextBox txtFileSize =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[12].FindControl("txtFileSize");
                        TextBox txtFileName =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[13].FindControl("txtFileName");

                        if (list.Count > 0)
                        {
                            foreach (Entities_PIPL_InvoiceEntry entity in list)
                            {
                                ListItem item = new ListItem();
                                item.Text = entity.DropdownName;
                                item.Value = entity.DropdownRefId;

                                if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                                {
                                    if (entity.TableName == "MT_UnitOfMeasure")
                                    {
                                        ddUOM.Items.Add(item);
                                    }
                                    if (entity.TableName == "MT_Currency")
                                    {
                                        ddCurrency.Items.Add(item);
                                    }
                                    if (entity.TableName == "PIPL_MT_CaseUnit")
                                    {
                                        ddCaseUnit.Items.Add(item);
                                    }
                                }

                            }
                        }

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["Col1"] = ddCaseUnit.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Col2"] = txtCaseNumber.Text;
                        dtCurrentTable.Rows[i - 1]["Col3"] = txtDescription.Text;
                        dtCurrentTable.Rows[i - 1]["Col4"] = txtSpecification.Text;
                        dtCurrentTable.Rows[i - 1]["Col5"] = txtQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["Col6"] = ddUOM.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Col7"] = ddCurrency.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Col8"] = txtUnitPrice.Text;
                        dtCurrentTable.Rows[i - 1]["Col9"] = txtNetWeight.Text;
                        dtCurrentTable.Rows[i - 1]["Col10"] = txtGrossWeight.Text;
                        dtCurrentTable.Rows[i - 1]["Col11"] = txtMeasurement.Text; ;
                        rowIndex++;

                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    gvDataUpdate.DataSource = dtCurrentTable;
                    gvDataUpdate.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousDataForUpdate();
        }

        private void AddNewRowFromUploadedExcelFile()
        {
            int rowIndex = 0;

            List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();
            list = BLL.PIPL_TRANSACTION_InvoiceEntry_Fill_All_DropdownList();

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList ddCaseUnit =
                          (DropDownList)gvData.Rows[rowIndex].Cells[1].FindControl("ddCaseUnit");
                        TextBox txtCaseNumber =
                          (TextBox)gvData.Rows[rowIndex].Cells[2].FindControl("txtCaseNumber");
                        TextBox txtDescription =
                          (TextBox)gvData.Rows[rowIndex].Cells[3].FindControl("txtDescription");
                        TextBox txtSpecification =
                          (TextBox)gvData.Rows[rowIndex].Cells[4].FindControl("txtSpecification");
                        TextBox txtQuantity =
                          (TextBox)gvData.Rows[rowIndex].Cells[5].FindControl("txtQuantity");
                        DropDownList ddUOM =
                          (DropDownList)gvData.Rows[rowIndex].Cells[6].FindControl("ddUOM");
                        DropDownList ddCurrency =
                          (DropDownList)gvData.Rows[rowIndex].Cells[7].FindControl("ddCurrency");
                        TextBox txtUnitPrice =
                          (TextBox)gvData.Rows[rowIndex].Cells[8].FindControl("txtUnitPrice");
                        TextBox txtNetWeight =
                          (TextBox)gvData.Rows[rowIndex].Cells[9].FindControl("txtNetWeight");
                        TextBox txtGrossWeight =
                          (TextBox)gvData.Rows[rowIndex].Cells[10].FindControl("txtGrossWeight");
                        TextBox txtMeasurement =
                          (TextBox)gvData.Rows[rowIndex].Cells[11].FindControl("txtMeasurement");
                        TextBox txtFileSize =
                          (TextBox)gvData.Rows[rowIndex].Cells[12].FindControl("txtFileSize");
                        TextBox txtFileName =
                          (TextBox)gvData.Rows[rowIndex].Cells[13].FindControl("txtFileName");

                        if (list.Count > 0)
                        {
                            ddUOM.Items.Clear();
                            ddCurrency.Items.Clear();
                            ddCaseUnit.Items.Clear();

                            foreach (Entities_PIPL_InvoiceEntry entity in list)
                            {
                                ListItem item = new ListItem();
                                item.Text = entity.DropdownName;
                                item.Value = entity.DropdownRefId;

                                if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                                {
                                    if (entity.TableName == "MT_UnitOfMeasure")
                                    {
                                        ddUOM.Items.Add(item);
                                    }
                                    if (entity.TableName == "MT_Currency")
                                    {
                                        ddCurrency.Items.Add(item);
                                    }
                                    if (entity.TableName == "PIPL_MT_CaseUnit")
                                    {
                                        ddCaseUnit.Items.Add(item);
                                    }
                                }

                            }
                        }

                        //drCurrentRow = dtCurrentTable.NewRow();
                        //drCurrentRow["RowNumber"] = i + 1;

                        //dtCurrentTable.Rows[i - 1]["Col1"] = entities.CaseUnit;
                        //dtCurrentTable.Rows[i - 1]["Col2"] = entities.CaseNumber;
                        //dtCurrentTable.Rows[i - 1]["Col3"] = entities.Description;
                        //dtCurrentTable.Rows[i - 1]["Col4"] = entities.Specification;
                        //dtCurrentTable.Rows[i - 1]["Col5"] = entities.Quantity;
                        //dtCurrentTable.Rows[i - 1]["Col6"] = entities.Uom;
                        //dtCurrentTable.Rows[i - 1]["Col7"] = entities.Currency;
                        //dtCurrentTable.Rows[i - 1]["Col8"] = entities.UPrice;
                        //dtCurrentTable.Rows[i - 1]["Col9"] = entities.NetWeight;
                        //dtCurrentTable.Rows[i - 1]["Col10"] = entities.GrossWeight;
                        //dtCurrentTable.Rows[i - 1]["Col11"] = entities.Measurement;
                        //rowIndex++;

                    }
                    //dtCurrentTable.Rows.Add(drCurrentRow);
                    //ViewState["CurrentTable"] = dtCurrentTable;

                    //SetPreviousDataFromUploadedExcelFile();

                    gvData.DataSource = dtCurrentTable;
                    gvData.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            
        }

        private string setControlNumberWithPrefix()
        {
            string retVal = string.Empty;

            retVal = "PIPL_" + Session["DivisionCode"].ToString() + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + COMMON.controlNoZeroPrefix(setControlNumber().ToString().Length.ToString()) + setControlNumber().ToString();

            return retVal;
        }

        private Int32 setControlNumber()
        {
            return BLL.PIPL_TRANSACTION_RequestHead_Count(DateTime.Now.Year.ToString()) + 1;
        }

        private void SetPreviousDataFromUploadedExcelFile()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddCaseUnit =
                          (DropDownList)gvData.Rows[rowIndex].Cells[1].FindControl("ddCaseUnit");
                        TextBox txtCaseNumber =
                          (TextBox)gvData.Rows[rowIndex].Cells[2].FindControl("txtCaseNumber");
                        TextBox txtDescription =
                          (TextBox)gvData.Rows[rowIndex].Cells[3].FindControl("txtDescription");
                        TextBox txtSpecification =
                          (TextBox)gvData.Rows[rowIndex].Cells[4].FindControl("txtSpecification");
                        TextBox txtQuantity =
                          (TextBox)gvData.Rows[rowIndex].Cells[5].FindControl("txtQuantity");
                        DropDownList ddUOM =
                          (DropDownList)gvData.Rows[rowIndex].Cells[6].FindControl("ddUOM");
                        DropDownList ddCurrency =
                          (DropDownList)gvData.Rows[rowIndex].Cells[7].FindControl("ddCurrency");
                        TextBox txtUnitPrice =
                          (TextBox)gvData.Rows[rowIndex].Cells[8].FindControl("txtUnitPrice");
                        TextBox txtNetWeight =
                          (TextBox)gvData.Rows[rowIndex].Cells[9].FindControl("txtNetWeight");
                        TextBox txtGrossWeight =
                          (TextBox)gvData.Rows[rowIndex].Cells[10].FindControl("txtGrossWeight");
                        TextBox txtMeasurement =
                          (TextBox)gvData.Rows[rowIndex].Cells[11].FindControl("txtMeasurement");

                        //ddCaseUnit.SelectedValue = ddCaseUnit.Items.FindByText(dt.Rows[i]["Col1"].ToString()).Value;
                        txtCaseNumber.Text = dt.Rows[i]["Col2"].ToString();
                        txtDescription.Text = dt.Rows[i]["Col3"].ToString();
                        txtSpecification.Text = dt.Rows[i]["Col4"].ToString();
                        txtQuantity.Text = dt.Rows[i]["Col5"].ToString();
                        //ddUOM.SelectedValue = ddUOM.Items.FindByText(dt.Rows[i]["Col6"].ToString()).Value;
                        //ddCurrency.SelectedValue = ddCurrency.Items.FindByText(dt.Rows[i]["Col7"].ToString()).Value;
                        txtUnitPrice.Text = dt.Rows[i]["Col8"].ToString();
                        txtNetWeight.Text = dt.Rows[i]["Col9"].ToString();
                        txtGrossWeight.Text = dt.Rows[i]["Col10"].ToString();
                        txtMeasurement.Text = dt.Rows[i]["Col11"].ToString();

                        rowIndex++;
                    }
                }
            }
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddCaseUnit =
                          (DropDownList)gvData.Rows[rowIndex].Cells[1].FindControl("ddCaseUnit");
                        TextBox txtCaseNumber =
                          (TextBox)gvData.Rows[rowIndex].Cells[2].FindControl("txtCaseNumber");
                        TextBox txtDescription =
                          (TextBox)gvData.Rows[rowIndex].Cells[3].FindControl("txtDescription");
                        TextBox txtSpecification =
                          (TextBox)gvData.Rows[rowIndex].Cells[4].FindControl("txtSpecification");
                        TextBox txtQuantity =
                          (TextBox)gvData.Rows[rowIndex].Cells[5].FindControl("txtQuantity");                        
                        DropDownList ddUOM =
                          (DropDownList)gvData.Rows[rowIndex].Cells[6].FindControl("ddUOM");
                        DropDownList ddCurrency =
                          (DropDownList)gvData.Rows[rowIndex].Cells[7].FindControl("ddCurrency");
                        TextBox txtUnitPrice =
                          (TextBox)gvData.Rows[rowIndex].Cells[8].FindControl("txtUnitPrice");
                        TextBox txtNetWeight =
                          (TextBox)gvData.Rows[rowIndex].Cells[9].FindControl("txtNetWeight");
                        TextBox txtGrossWeight =
                          (TextBox)gvData.Rows[rowIndex].Cells[10].FindControl("txtGrossWeight");
                        TextBox txtMeasurement =
                          (TextBox)gvData.Rows[rowIndex].Cells[11].FindControl("txtMeasurement");

                        ddCaseUnit.SelectedValue = dt.Rows[i]["Col1"].ToString();
                        txtCaseNumber.Text = dt.Rows[i]["Col2"].ToString();
                        txtDescription.Text = dt.Rows[i]["Col3"].ToString();
                        txtSpecification.Text = dt.Rows[i]["Col4"].ToString();
                        txtQuantity.Text = dt.Rows[i]["Col5"].ToString();                        
                        ddUOM.SelectedValue = dt.Rows[i]["Col6"].ToString();
                        ddCurrency.SelectedValue = dt.Rows[i]["Col7"].ToString();
                        txtUnitPrice.Text = dt.Rows[i]["Col8"].ToString();
                        txtNetWeight.Text = dt.Rows[i]["Col9"].ToString();
                        txtGrossWeight.Text = dt.Rows[i]["Col10"].ToString();
                        txtMeasurement.Text = dt.Rows[i]["Col11"].ToString();                        

                        rowIndex++;
                    }
                }
            }
        }

        private void SetPreviousDataForUpdate()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddCaseUnit =
                          (DropDownList)gvDataUpdate.Rows[rowIndex].Cells[1].FindControl("ddCaseUnit");
                        TextBox txtCaseNumber =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[2].FindControl("txtCaseNumber");
                        TextBox txtDescription =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[3].FindControl("txtDescription");
                        TextBox txtSpecification =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[4].FindControl("txtSpecification");
                        TextBox txtQuantity =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[5].FindControl("txtQuantity");
                        DropDownList ddUOM =
                          (DropDownList)gvDataUpdate.Rows[rowIndex].Cells[6].FindControl("ddUOM");
                        DropDownList ddCurrency =
                          (DropDownList)gvDataUpdate.Rows[rowIndex].Cells[7].FindControl("ddCurrency");
                        TextBox txtUnitPrice =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[8].FindControl("txtUnitPrice");
                        TextBox txtNetWeight =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[9].FindControl("txtNetWeight");
                        TextBox txtGrossWeight =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[10].FindControl("txtGrossWeight");
                        TextBox txtMeasurement =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[11].FindControl("txtMeasurement");

                        ddCaseUnit.SelectedValue = dt.Rows[i]["Col1"].ToString();
                        txtCaseNumber.Text = dt.Rows[i]["Col2"].ToString();
                        txtDescription.Text = dt.Rows[i]["Col3"].ToString();
                        txtSpecification.Text = dt.Rows[i]["Col4"].ToString();
                        txtQuantity.Text = dt.Rows[i]["Col5"].ToString();
                        ddUOM.SelectedValue = dt.Rows[i]["Col6"].ToString();
                        ddCurrency.SelectedValue = dt.Rows[i]["Col7"].ToString();
                        txtUnitPrice.Text = dt.Rows[i]["Col8"].ToString();
                        txtNetWeight.Text = dt.Rows[i]["Col9"].ToString();
                        txtGrossWeight.Text = dt.Rows[i]["Col10"].ToString();
                        txtMeasurement.Text = dt.Rows[i]["Col11"].ToString();

                        rowIndex++;
                    }
                }
            }
        }

        protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetRowData();
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable"] = dt;
                    gvData.DataSource = dt;
                    gvData.DataBind();

                    for (int i = 0; i < gvData.Rows.Count - 1; i++)
                    {
                        gvData.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }
                    SetPreviousData();
                }
            }
            computeEstimatedValues();
        }

        protected void gvDataUpdate_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetRowDataForUpdate();
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable"] = dt;
                    gvDataUpdate.DataSource = dt;
                    gvDataUpdate.DataBind();

                    for (int i = 0; i < gvDataUpdate.Rows.Count - 1; i++)
                    {
                        gvDataUpdate.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }
                    SetPreviousDataForUpdate();
                }
            }
            computeEstimatedValuesForUpdate();
        }

        private void SetRowData()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList ddCaseUnit =
                          (DropDownList)gvData.Rows[rowIndex].Cells[1].FindControl("ddCaseUnit");
                        TextBox txtCaseNumber =
                          (TextBox)gvData.Rows[rowIndex].Cells[2].FindControl("txtCaseNumber");
                        TextBox txtDescription =
                          (TextBox)gvData.Rows[rowIndex].Cells[3].FindControl("txtDescription");
                        TextBox txtSpecification =
                          (TextBox)gvData.Rows[rowIndex].Cells[4].FindControl("txtSpecification");
                        TextBox txtQuantity =
                          (TextBox)gvData.Rows[rowIndex].Cells[5].FindControl("txtQuantity");                        
                        DropDownList ddUOM =
                          (DropDownList)gvData.Rows[rowIndex].Cells[6].FindControl("ddUOM");
                        DropDownList ddCurrency =
                          (DropDownList)gvData.Rows[rowIndex].Cells[7].FindControl("ddCurrency");
                        TextBox txtUnitPrice =
                          (TextBox)gvData.Rows[rowIndex].Cells[8].FindControl("txtUnitPrice");
                        TextBox txtNetWeight =
                          (TextBox)gvData.Rows[rowIndex].Cells[9].FindControl("txtNetWeight");
                        TextBox txtGrossWeight =
                          (TextBox)gvData.Rows[rowIndex].Cells[10].FindControl("txtGrossWeight");
                        TextBox txtMeasurement =
                          (TextBox)gvData.Rows[rowIndex].Cells[11].FindControl("txtMeasurement");


                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Col1"] = ddCaseUnit.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Col2"] = txtCaseNumber.Text;
                        dtCurrentTable.Rows[i - 1]["Col3"] = txtDescription.Text;
                        dtCurrentTable.Rows[i - 1]["Col4"] = txtSpecification.Text;
                        dtCurrentTable.Rows[i - 1]["Col5"] = txtQuantity.Text;                        
                        dtCurrentTable.Rows[i - 1]["Col6"] = ddUOM.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Col7"] = ddCurrency.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Col8"] = txtUnitPrice.Text;
                        dtCurrentTable.Rows[i - 1]["Col9"] = txtNetWeight.Text;
                        dtCurrentTable.Rows[i - 1]["Col10"] = txtGrossWeight.Text;
                        dtCurrentTable.Rows[i - 1]["Col11"] = txtMeasurement.Text;
                        
                        rowIndex++;                        

                    }
                    
                    ViewState["CurrentTable"] = dtCurrentTable;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('View State [CurrentTable] is null.');", true);
            }

        }

        private void SetRowDataForUpdate()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList ddCaseUnit =
                          (DropDownList)gvDataUpdate.Rows[rowIndex].Cells[1].FindControl("ddCaseUnit");
                        TextBox txtCaseNumber =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[2].FindControl("txtCaseNumber");
                        TextBox txtDescription =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[3].FindControl("txtDescription");
                        TextBox txtSpecification =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[4].FindControl("txtSpecification");
                        TextBox txtQuantity =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[5].FindControl("txtQuantity");
                        DropDownList ddUOM =
                          (DropDownList)gvDataUpdate.Rows[rowIndex].Cells[6].FindControl("ddUOM");
                        DropDownList ddCurrency =
                          (DropDownList)gvDataUpdate.Rows[rowIndex].Cells[7].FindControl("ddCurrency");
                        TextBox txtUnitPrice =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[8].FindControl("txtUnitPrice");
                        TextBox txtNetWeight =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[9].FindControl("txtNetWeight");
                        TextBox txtGrossWeight =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[10].FindControl("txtGrossWeight");
                        TextBox txtMeasurement =
                          (TextBox)gvDataUpdate.Rows[rowIndex].Cells[11].FindControl("txtMeasurement");


                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Col1"] = ddCaseUnit.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Col2"] = txtCaseNumber.Text;
                        dtCurrentTable.Rows[i - 1]["Col3"] = txtDescription.Text;
                        dtCurrentTable.Rows[i - 1]["Col4"] = txtSpecification.Text;
                        dtCurrentTable.Rows[i - 1]["Col5"] = txtQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["Col6"] = ddUOM.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Col7"] = ddCurrency.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Col8"] = txtUnitPrice.Text;
                        dtCurrentTable.Rows[i - 1]["Col9"] = txtNetWeight.Text;
                        dtCurrentTable.Rows[i - 1]["Col10"] = txtGrossWeight.Text;
                        dtCurrentTable.Rows[i - 1]["Col11"] = txtMeasurement.Text;

                        rowIndex++;

                    }

                    ViewState["CurrentTable"] = dtCurrentTable;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('View State [CurrentTable] is null.');", true);
            }

        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            bool isErrorQuantity = false;
            bool isErrorPrice = false;

            for (int i = 0; i < gvData.Rows.Count; i++)
            {
                TextBox txtQuantity = (TextBox)gvData.Rows[i].Cells[2].FindControl("txtQuantity");
                TextBox txtUnitPrice = (TextBox)gvData.Rows[i].Cells[5].FindControl("txtUnitPrice");

                if (!COMMON.isNumeric(txtQuantity.Text.Trim(), System.Globalization.NumberStyles.Number) || String.IsNullOrEmpty(txtQuantity.Text.Trim()))
                {
                    isErrorQuantity = true;
                    txtQuantity.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    isErrorQuantity = false;
                    txtQuantity.BackColor = System.Drawing.Color.White;
                }

                if (!COMMON.isNumeric(txtUnitPrice.Text.Trim(), System.Globalization.NumberStyles.Currency) || String.IsNullOrEmpty(txtUnitPrice.Text.Trim()))
                {
                    isErrorPrice = true;
                    txtUnitPrice.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    isErrorPrice = false;
                    txtUnitPrice.BackColor = System.Drawing.Color.White;
                }
            }

            if (!isErrorPrice && !isErrorQuantity)
            {
                string dt = Request.Form[txtETDManila.UniqueID];
                txtETDManila.Text = dt;

                txtEstimatedYen.ReadOnly = false;
                txtEstimatedUsd.ReadOnly = false;

                computeEstimatedValues();
                AddNewRow();

                txtEstimatedYen.ReadOnly = true;
                txtEstimatedUsd.ReadOnly = true;

                fuExcelData.Visible = false;
            }

            if (ddCommercialValue.SelectedItem.Text.Trim() == "WITH")
            {
                ddSalesType.Enabled = true;
                ddAccountCode.Enabled = true;
                ddBusinessUnit.Enabled = true;
            }
            else
            {
                ddSalesType.SelectedValue = "1";
                ddSalesType.Enabled = false;
                ddAccountCode.Enabled = false;
                ddBusinessUnit.Enabled = false;
            }
        }

        protected void ButtonAdd2_Click(object sender, EventArgs e)
        {
            bool isErrorQuantity = false;
            bool isErrorPrice = false;

            for (int i = 0; i < gvDataUpdate.Rows.Count; i++)
            {
                TextBox txtQuantity = (TextBox)gvDataUpdate.Rows[i].Cells[2].FindControl("txtQuantity");
                TextBox txtUnitPrice = (TextBox)gvDataUpdate.Rows[i].Cells[5].FindControl("txtUnitPrice");

                if (!COMMON.isNumeric(txtQuantity.Text.Trim(), System.Globalization.NumberStyles.Number) || String.IsNullOrEmpty(txtQuantity.Text.Trim()))
                {
                    isErrorQuantity = true;
                    txtQuantity.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    isErrorQuantity = false;
                    txtQuantity.BackColor = System.Drawing.Color.White;
                }

                if (!COMMON.isNumeric(txtUnitPrice.Text.Trim(), System.Globalization.NumberStyles.Currency) || String.IsNullOrEmpty(txtUnitPrice.Text.Trim()))
                {
                    isErrorPrice = true;
                    txtUnitPrice.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    isErrorPrice = false;
                    txtUnitPrice.BackColor = System.Drawing.Color.White;
                }
            }

            if (!isErrorPrice && !isErrorQuantity)
            {
                string dt = Request.Form[txtETDManila.UniqueID];
                txtETDManila.Text = dt;

                txtEstimatedYen.ReadOnly = false;
                txtEstimatedUsd.ReadOnly = false;

                computeEstimatedValuesForUpdate();
                AddNewRowForUpdate();

                txtEstimatedYen.ReadOnly = true;
                txtEstimatedUsd.ReadOnly = true;

                fuExcelData.Visible = false;
            }
        }

        private void computeEstimatedValues()
        {
            try
            {
                txtEstimatedYen.Text = string.Empty;
                txtEstimatedUsd.Text = string.Empty;

                for (int i = 0; i <= gvData.Rows.Count; i++)
                {
                    TextBox txtQuantity = (TextBox)gvData.Rows[i].Cells[3].FindControl("txtQuantity");
                    DropDownList ddCurrency = (DropDownList)gvData.Rows[i].Cells[4].FindControl("ddCurrency");
                    TextBox txtUnitPrice = (TextBox)gvData.Rows[i].Cells[6].FindControl("txtUnitPrice");

                    double quantity;
                    double price;
                    double estimatedYEN;
                    double estimatedUSD;

                    estimatedYEN = double.Parse(txtEstimatedYen.Text = txtEstimatedYen.Text.Length <= 0 ? "0" : txtEstimatedYen.Text.Trim());
                    estimatedUSD = double.Parse(txtEstimatedUsd.Text = txtEstimatedUsd.Text.Length <= 0 ? "0" : txtEstimatedUsd.Text.Trim());
                    price = double.Parse(txtUnitPrice.Text.Trim());
                    quantity = double.Parse(txtQuantity.Text.Trim());

                    // if YEN (March 04, 2019 - If YEN is selected then it must be whole number and rounded-off)
                    if (ddCurrency.SelectedValue == "1")
                    {
                        txtEstimatedYen.Text = (estimatedYEN + (quantity * price)).ToString("#,##0");
                    }
                    // if USD
                    if (ddCurrency.SelectedValue == "2")
                    {
                        txtEstimatedUsd.Text = (estimatedUSD + (quantity * price)).ToString("#,###.#0");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        private void computeEstimatedValuesForUpdate()
        {
            try
            {
                txtEstimatedYen.Text = string.Empty;
                txtEstimatedUsd.Text = string.Empty;

                for (int i = 0; i <= gvDataUpdate.Rows.Count; i++)
                {
                    TextBox txtUpdatedQuantity = (TextBox)gvDataUpdate.Rows[i].Cells[5].FindControl("txtUpdatedQuantity");
                    DropDownList ddUpdatedCurrency = (DropDownList)gvDataUpdate.Rows[i].Cells[7].FindControl("ddUpdatedCurrency");
                    TextBox txtUpdatedUnitPrice = (TextBox)gvDataUpdate.Rows[i].Cells[8].FindControl("txtUpdatedUnitPrice");

                    double quantity;
                    double price;
                    double estimatedYEN;
                    double estimatedUSD;

                    estimatedYEN = double.Parse(txtEstimatedYen.Text = txtEstimatedYen.Text.Length <= 0 ? "0" : txtEstimatedYen.Text.Trim());
                    estimatedUSD = double.Parse(txtEstimatedUsd.Text = txtEstimatedUsd.Text.Length <= 0 ? "0" : txtEstimatedUsd.Text.Trim());
                    price = double.Parse(txtUpdatedUnitPrice.Text.Trim());
                    quantity = double.Parse(txtUpdatedQuantity.Text.Trim());

                    // if YEN (March 04, 2019 - If YEN is selected then it must be whole number and rounded-off)
                    if (ddUpdatedCurrency.SelectedValue == "1")
                    {
                        txtEstimatedYen.Text = (estimatedYEN + (quantity * price)).ToString("#,##0");
                    }
                    // if USD
                    if (ddUpdatedCurrency.SelectedValue == "2")
                    {
                        txtEstimatedUsd.Text = (estimatedUSD + (quantity * price)).ToString("#,###.#0");
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        private void computeEstimatedValuesForUpload()
        {
            try
            {
                txtEstimatedYen.Text = string.Empty;
                txtEstimatedUsd.Text = string.Empty;

                for (int i = 0; i <= gvDataUpload.Rows.Count; i++)
                {
                    TextBox txtUploadQuantity = (TextBox)gvDataUpload.Rows[i].Cells[5].FindControl("txtUploadQuantity");
                    DropDownList ddUploadCurrency = (DropDownList)gvDataUpload.Rows[i].Cells[7].FindControl("ddUploadCurrency");
                    TextBox txtUploadUnitPrice = (TextBox)gvDataUpload.Rows[i].Cells[8].FindControl("txtUploadUnitPrice");

                    double quantity;
                    double price;
                    double estimatedYEN;
                    double estimatedUSD;

                    estimatedYEN = double.Parse(txtEstimatedYen.Text = txtEstimatedYen.Text.Length <= 0 ? "0" : txtEstimatedYen.Text.Trim());
                    estimatedUSD = double.Parse(txtEstimatedUsd.Text = txtEstimatedUsd.Text.Length <= 0 ? "0" : txtEstimatedUsd.Text.Trim());
                    price = double.Parse(txtUploadUnitPrice.Text.Trim());
                    quantity = double.Parse(txtUploadQuantity.Text.Trim());

                    // if YEN
                    if (ddUploadCurrency.SelectedValue == "1")
                    {
                        txtEstimatedYen.Text = (estimatedYEN + (quantity * price)).ToString("#,###.#0");
                    }
                    // if USD
                    if (ddUploadCurrency.SelectedValue == "2")
                    {
                        txtEstimatedUsd.Text = (estimatedUSD + (quantity * price)).ToString("#,###.#0");
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = System.IO.Path.GetFileName(fuExcelData.FileName);
                string fileExtensionApplication = System.IO.Path.GetExtension(fileName);

                if (fileName.Length > 0 && fileExtensionApplication.ToLower().Trim() == ".xlsx")
                {
                    //if (!System.IO.Directory.Exists(Server.MapPath("~/PIPL_Request/" + ctrlNo)))
                    //if (!System.IO.Directory.Exists(@"C:\SOFRA_PIPL_FORUPLOAD_TEMP"))
                    //{
                    //    System.IO.Directory.CreateDirectory(@"C:\SOFRA_PIPL_FORUPLOAD_TEMP");
                    //}

                    if (System.IO.Directory.Exists(Server.MapPath("~/SOFRA_PIPL_FORUPLOAD_TEMP")))
                    {
                        string timeStamp = DateTime.Now.ToString("MMddyyyyHHmmss");
                        string fullPath = Server.MapPath("~/SOFRA_PIPL_FORUPLOAD_TEMP/" + timeStamp + "_" + fileName);
                        fuExcelData.SaveAs(fullPath);
                        fuExcelData.Dispose();
                        fuExcelData.FileContent.Dispose();

                        string fsPath = "http://10.27.1.170:9292/SOFRA_PIPL_FORUPLOAD_TEMP/" + timeStamp + "_" + fileName;


                        using (SLDocument sl = new SLDocument())
                        {
                            try
                            {
                                FileStream fs = new FileStream(fsPath, FileMode.Open);
                                SLDocument sheet = new SLDocument(fs, "PIPL_FU_DATA");

                                SLWorksheetStatistics stats = sheet.GetWorksheetStatistics();

                                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                                DataRow drCurrentRow = null;

                                List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

                                for (int j = 2; j <= stats.EndRowIndex; j++)
                                {
                                    //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + sheet.GetCellValueAsString(j, 1).Trim() + "');", true);
                                    Entities_PIPL_InvoiceEntry entity = new Entities_PIPL_InvoiceEntry();
                                    entity.CaseUnit = sheet.GetCellValueAsString(j, 1);
                                    entity.CaseNumber = sheet.GetCellValueAsString(j, 2);
                                    entity.Description = sheet.GetCellValueAsString(j, 3);
                                    entity.Specification = sheet.GetCellValueAsString(j, 4);
                                    entity.Quantity = sheet.GetCellValueAsString(j, 5);
                                    entity.Uom = sheet.GetCellValueAsString(j, 6);
                                    entity.Currency = sheet.GetCellValueAsString(j, 7);
                                    entity.UPrice = sheet.GetCellValueAsString(j, 8);
                                    entity.NetWeight = sheet.GetCellValueAsString(j, 9);
                                    entity.GrossWeight = sheet.GetCellValueAsString(j, 10);
                                    entity.Measurement = sheet.GetCellValueAsString(j, 11);
                                    entity.DetailsRefId = (j - 1).ToString();

                                    //if (sheet.GetCellValueAsString(j, 3).Length > 0 || sheet.GetCellValueAsString(j, 4).Length > 0)
                                    //{
                                    //    AddNewRowFromUploadedExcelFile(entity);
                                    //    computeEstimatedValues();
                                    //}
                                    

                                    drCurrentRow = dtCurrentTable.NewRow();
                                    drCurrentRow["RowNumber"] = j - 1;

                                    dtCurrentTable.Rows[j - 2]["Col1"] = entity.CaseUnit;
                                    dtCurrentTable.Rows[j - 2]["Col2"] = entity.CaseNumber;
                                    dtCurrentTable.Rows[j - 2]["Col3"] = entity.Description;
                                    dtCurrentTable.Rows[j - 2]["Col4"] = entity.Specification;
                                    dtCurrentTable.Rows[j - 2]["Col5"] = entity.Quantity;
                                    dtCurrentTable.Rows[j - 2]["Col6"] = entity.Uom;
                                    dtCurrentTable.Rows[j - 2]["Col7"] = entity.Currency;
                                    dtCurrentTable.Rows[j - 2]["Col8"] = entity.UPrice;
                                    dtCurrentTable.Rows[j - 2]["Col9"] = entity.NetWeight;
                                    dtCurrentTable.Rows[j - 2]["Col10"] = entity.GrossWeight;
                                    dtCurrentTable.Rows[j - 2]["Col11"] = entity.Measurement;

                                    dtCurrentTable.Rows.Add(drCurrentRow);

                                    list.Add(entity);

                                }
                                
                                gvData.Visible = false;
                                gvDataUpload.Visible = true;

                                gvDataUpload.DataSource = list;
                                gvDataUpload.DataBind();

                                fs.Dispose();

                                btnUpload.Visible = false;
                                fuExcelData.Visible = false;

                                computeEstimatedValuesForUpload();

                                txtEstimatedUsd.ReadOnly = true;
                                txtEstimatedYen.ReadOnly = true;


                            }
                            catch (Exception ex)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                            }
                        }

                        System.IO.File.Delete(fullPath);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select a valid excel file to upload.');", true);
                }
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnBiggerView_Click(object sender, EventArgs e)
        {
            lbBiggerView_Click(sender, e);
        }

        protected void lbBiggerView_Click(object sender, EventArgs e)
        {
            try
            {                

                if (ViewState["CurrentTable"] != null)
                {

                    if (lbBiggerView.Text == "BIGGER VIEW")
                    {
                        Session["BiggerHeight"] = "62";
                        Session["BiggerFontSize"] = "16";
                        Session["TextMode"] = "MULTILINE";
                        lbBiggerView.Text = "NORMAL VIEW";
                    }
                    else
                    {
                        Session["BiggerHeight"] = "22";
                        Session["BiggerFontSize"] = "9";
                        Session["TextMode"] = "SINGLE";
                        lbBiggerView.Text = "BIGGER VIEW";
                    }

                    if (gvData.Visible)
                    {

                        for (int i = 0; i < gvData.Rows.Count; i++)
                        {

                            DropDownList ddCaseUnit = (DropDownList)gvData.Rows[i].Cells[3].FindControl("ddCaseUnit");
                            TextBox txtCaseNumber = (TextBox)gvData.Rows[i].Cells[1].FindControl("txtCaseNumber");
                            TextBox txtDescription = (TextBox)gvData.Rows[i].Cells[1].FindControl("txtDescription");
                            TextBox txtSpecification = (TextBox)gvData.Rows[i].Cells[1].FindControl("txtSpecification");
                            TextBox txtQuantity = (TextBox)gvData.Rows[i].Cells[2].FindControl("txtQuantity");
                            DropDownList ddUOM = (DropDownList)gvData.Rows[i].Cells[3].FindControl("ddUOM");
                            DropDownList ddCurrency = (DropDownList)gvData.Rows[i].Cells[4].FindControl("ddCurrency");
                            TextBox txtUnitPrice = (TextBox)gvData.Rows[i].Cells[5].FindControl("txtUnitPrice");
                            TextBox txtNetWeight = (TextBox)gvData.Rows[i].Cells[6].FindControl("txtNetWeight");
                            TextBox txtGrossWeight = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtGrossWeight");
                            TextBox txtMeasurement = (TextBox)gvData.Rows[i].Cells[8].FindControl("txtMeasurement");

                            ddCaseUnit.Height = int.Parse(Session["BiggerHeight"].ToString());
                            ddCaseUnit.Font.Size = int.Parse(Session["BiggerFontSize"].ToString());
                            ddUOM.Height = int.Parse(Session["BiggerHeight"].ToString());
                            ddUOM.Font.Size = int.Parse(Session["BiggerFontSize"].ToString());
                            ddCurrency.Height = int.Parse(Session["BiggerHeight"].ToString());
                            ddCurrency.Font.Size = int.Parse(Session["BiggerFontSize"].ToString());

                            txtCaseNumber.Height = int.Parse(Session["BiggerHeight"].ToString());
                            if (Session["TextMode"].ToString() == "MULTILINE")
                            {
                                txtCaseNumber.TextMode = TextBoxMode.MultiLine;
                                txtDescription.TextMode = TextBoxMode.MultiLine;
                                txtSpecification.TextMode = TextBoxMode.MultiLine;
                                txtNetWeight.TextMode = TextBoxMode.MultiLine;
                                txtGrossWeight.TextMode = TextBoxMode.MultiLine;
                                txtMeasurement.TextMode = TextBoxMode.MultiLine;
                            }
                            else
                            {
                                txtCaseNumber.TextMode = TextBoxMode.SingleLine;
                                txtDescription.TextMode = TextBoxMode.SingleLine;
                                txtSpecification.TextMode = TextBoxMode.SingleLine;
                                txtNetWeight.TextMode = TextBoxMode.SingleLine;
                                txtGrossWeight.TextMode = TextBoxMode.SingleLine;
                                txtMeasurement.TextMode = TextBoxMode.SingleLine;
                            }


                            txtDescription.Height = int.Parse(Session["BiggerHeight"].ToString());


                            txtSpecification.Height = int.Parse(Session["BiggerHeight"].ToString());

                            txtQuantity.Height = int.Parse(Session["BiggerHeight"].ToString());
                            txtQuantity.Font.Size = int.Parse(Session["BiggerFontSize"].ToString());
                            txtUnitPrice.Height = int.Parse(Session["BiggerHeight"].ToString());
                            txtUnitPrice.Font.Size = int.Parse(Session["BiggerFontSize"].ToString());


                            txtNetWeight.Height = int.Parse(Session["BiggerHeight"].ToString());
                            txtGrossWeight.Height = int.Parse(Session["BiggerHeight"].ToString());
                            txtMeasurement.Height = int.Parse(Session["BiggerHeight"].ToString());

                        }

                    }

                    if (gvDataUpdate.Visible)
                    {

                        for (int i = 0; i < gvDataUpdate.Rows.Count; i++)
                        {

                            Label lblUpdatedRefId = (Label)gvDataUpdate.Rows[i].Cells[0].FindControl("lblUpdatedRefId");
                            TextBox txtUpdatedCaseNumber = (TextBox)gvDataUpdate.Rows[i].Cells[2].FindControl("txtUpdatedCaseNumber");
                            TextBox txtUpdatedDescription = (TextBox)gvDataUpdate.Rows[i].Cells[3].FindControl("txtUpdatedDescription");
                            TextBox txtUpdatedSpecification = (TextBox)gvDataUpdate.Rows[i].Cells[4].FindControl("txtUpdatedSpecification");
                            TextBox txtUpdatedQuantity = (TextBox)gvDataUpdate.Rows[i].Cells[5].FindControl("txtUpdatedQuantity");
                            TextBox txtUpdatedUnitPrice = (TextBox)gvDataUpdate.Rows[i].Cells[8].FindControl("txtUpdatedUnitPrice");
                            TextBox txtUpdatedNetWeight = (TextBox)gvDataUpdate.Rows[i].Cells[9].FindControl("txtUpdatedNetWeight");
                            TextBox txtUpdatedGrossWeight = (TextBox)gvDataUpdate.Rows[i].Cells[10].FindControl("txtUpdatedGrossWeight");
                            TextBox txtUpdatedMeasurement = (TextBox)gvDataUpdate.Rows[i].Cells[11].FindControl("txtUpdatedMeasurement");


                            DropDownList ddUpdatedUOM = (DropDownList)gvDataUpdate.Rows[i].Cells[6].FindControl("ddUpdatedUOM");
                            DropDownList ddUpdatedCurrency = (DropDownList)gvDataUpdate.Rows[i].Cells[7].FindControl("ddUpdatedCurrency");
                            DropDownList ddUpdatedCaseUnit = (DropDownList)gvDataUpdate.Rows[i].Cells[1].FindControl("ddUpdatedCaseUnit");

                            ddUpdatedCaseUnit.Height = int.Parse(Session["BiggerHeight"].ToString());
                            ddUpdatedCaseUnit.Font.Size = int.Parse(Session["BiggerFontSize"].ToString());
                            ddUpdatedUOM.Height = int.Parse(Session["BiggerHeight"].ToString());
                            ddUpdatedUOM.Font.Size = int.Parse(Session["BiggerFontSize"].ToString());
                            ddUpdatedCurrency.Height = int.Parse(Session["BiggerHeight"].ToString());
                            ddUpdatedCurrency.Font.Size = int.Parse(Session["BiggerFontSize"].ToString());

                            txtUpdatedCaseNumber.Height = int.Parse(Session["BiggerHeight"].ToString());
                            if (Session["TextMode"].ToString() == "MULTILINE")
                            {
                                txtUpdatedCaseNumber.TextMode = TextBoxMode.MultiLine;
                                txtUpdatedDescription.TextMode = TextBoxMode.MultiLine;
                                txtUpdatedSpecification.TextMode = TextBoxMode.MultiLine;
                                txtUpdatedNetWeight.TextMode = TextBoxMode.MultiLine;
                                txtUpdatedGrossWeight.TextMode = TextBoxMode.MultiLine;
                                txtUpdatedMeasurement.TextMode = TextBoxMode.MultiLine;
                            }
                            else
                            {
                                txtUpdatedCaseNumber.TextMode = TextBoxMode.SingleLine;
                                txtUpdatedDescription.TextMode = TextBoxMode.SingleLine;
                                txtUpdatedSpecification.TextMode = TextBoxMode.SingleLine;
                                txtUpdatedNetWeight.TextMode = TextBoxMode.SingleLine;
                                txtUpdatedGrossWeight.TextMode = TextBoxMode.SingleLine;
                                txtUpdatedMeasurement.TextMode = TextBoxMode.SingleLine;
                            }


                            txtUpdatedDescription.Height = int.Parse(Session["BiggerHeight"].ToString());


                            txtUpdatedSpecification.Height = int.Parse(Session["BiggerHeight"].ToString());

                            txtUpdatedQuantity.Height = int.Parse(Session["BiggerHeight"].ToString());
                            txtUpdatedQuantity.Font.Size = int.Parse(Session["BiggerFontSize"].ToString());
                            txtUpdatedUnitPrice.Height = int.Parse(Session["BiggerHeight"].ToString());
                            txtUpdatedUnitPrice.Font.Size = int.Parse(Session["BiggerFontSize"].ToString());


                            txtUpdatedNetWeight.Height = int.Parse(Session["BiggerHeight"].ToString());
                            txtUpdatedGrossWeight.Height = int.Parse(Session["BiggerHeight"].ToString());
                            txtUpdatedMeasurement.Height = int.Parse(Session["BiggerHeight"].ToString());

                        }

                    }



                    
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool isError = false;
                string successHead = string.Empty;
                string successStatus = string.Empty;


                if (ddBDN.SelectedItem.Text.Length > 0)
                {
                    if (ddBDNValue.SelectedItem.Text.Length <= 0)
                    {
                        isError = true;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select a valid BDNValue');", true);
                    }
                }
                if (ddBDN.SelectedItem.Text.Length > 0 && ddBDNValue.SelectedItem.Text.Length > 0 && String.IsNullOrEmpty(txtAttention2.Text))
                {
                    isError = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please enter a valid attention name for BDNValue.');", true);
                }
                if (ddPurpose.SelectedItem.Text.Trim() == "OTHERS")
                {
                    if (txtPurposeOthers.Text.Length <= 0)
                    {
                        isError = true;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You selected purpose [OTHERS]. You must provide the Purpose(Others) field.');", true);
                    }
                }

                if (ddCommercialValue.SelectedItem.Text.Trim() == "WITH")
                {
                    if (ddSalesType.SelectedItem.Text.Length <= 0 || ddBusinessUnit.SelectedItem.Text.Length <= 0 || ddAccountCode.SelectedItem.Text.Length <= 0)
                    {
                        isError = true;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You selected WITH Commercial Value. You must select a valid Sales Type, Business Unit and Account Code.');", true);
                    }
                    if (ddBDN.SelectedItem.Text.Length <= 0 || ddBDNValue.SelectedItem.Text.Length <= 0)
                    {
                        isError = true;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You selected WITH Commercial Value. You must select a valid BDN and BDNValue.');", true);
                    }
                }

                if (String.IsNullOrEmpty(Request.QueryString["ControlNo_From_Details"].ToString()))
                {
                    if (fu1.FileName.Length <= 0 && fu2.FileName.Length <= 0 && fu3.FileName.Length <= 0 && fu4.FileName.Length <= 0)
                    {
                        isError = true;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Need ATTACHMENT of reference document such as P.O. (with Commercial Value), Shipping Invoice (Import), Instruction Sheet, or any other documents that will suffice with your request.');", true);
                    }
                }

                if (fu1.HasFile)
                {
                    if (fu1.FileName.Substring(fu1.FileName.IndexOf(".") + 1, 3).ToString().ToLower() != "pdf")
                    {
                        isError = true;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('ATTACHMENT1 (Invalid Attachment) : Attachment file must be pdf file only!');", true);
                    }                    
                }
                if (fu2.HasFile)
                {
                    if (fu2.FileName.Substring(fu2.FileName.IndexOf(".") + 1, 3).ToString().ToLower() != "pdf")
                    {
                        isError = true;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('ATTACHMENT2 (Invalid Attachment) : Attachment file must be pdf file only!');", true);
                    }
                }
                if (fu3.HasFile)
                {
                    if (fu3.FileName.Substring(fu3.FileName.IndexOf(".") + 1, 3).ToString().ToLower() != "pdf")
                    {
                        isError = true;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('ATTACHMENT3 (Invalid Attachment) : Attachment file must be pdf file only!');", true);
                    }
                }
                if (fu4.HasFile)
                {
                    if (fu4.FileName.Substring(fu4.FileName.IndexOf(".") + 1, 3).ToString().ToLower() != "pdf")
                    {
                        isError = true;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('ATTACHMENT4 (Invalid Attachment) : Attachment file must be pdf file only!');", true);
                    }
                }

                if (!String.IsNullOrEmpty(Request.QueryString["ControlNo_From_Details"].ToString()))
                {
                    if (gvDataUpdate.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvDataUpdate.Rows.Count; i++)
                        {
                            TextBox txtUpdatedMeasurement = (TextBox)gvDataUpdate.Rows[i].Cells[11].FindControl("txtUpdatedMeasurement");

                            TextBox txtUpdatedCaseNumber = (TextBox)gvDataUpdate.Rows[i].Cells[2].FindControl("txtUpdatedCaseNumber");
                            TextBox txtUpdatedDescription = (TextBox)gvDataUpdate.Rows[i].Cells[3].FindControl("txtUpdatedDescription");
                            TextBox txtUpdatedSpecification = (TextBox)gvDataUpdate.Rows[i].Cells[4].FindControl("txtUpdatedSpecification");
                            TextBox txtUpdatedQuantity = (TextBox)gvDataUpdate.Rows[i].Cells[5].FindControl("txtUpdatedQuantity");
                            TextBox txtUpdatedUnitPrice = (TextBox)gvDataUpdate.Rows[i].Cells[8].FindControl("txtUpdatedUnitPrice");
                            TextBox txtUpdatedNetWeight = (TextBox)gvDataUpdate.Rows[i].Cells[9].FindControl("txtUpdatedNetWeight");
                            TextBox txtUpdatedGrossWeight = (TextBox)gvDataUpdate.Rows[i].Cells[10].FindControl("txtUpdatedGrossWeight");


                            if (!COMMON.isNumeric(txtUpdatedQuantity.Text.Trim(), System.Globalization.NumberStyles.AllowThousands) || String.IsNullOrEmpty(txtUpdatedQuantity.Text.Trim()))
                            {
                                isError = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please enter a valid quantity');", true);
                            }
                            if (!COMMON.isNumeric(txtUpdatedUnitPrice.Text.Trim(), System.Globalization.NumberStyles.AllowDecimalPoint) || String.IsNullOrEmpty(txtUpdatedUnitPrice.Text.Trim()))
                            {
                                isError = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please enter a valid unit price');", true);
                            }


                            if (string.IsNullOrEmpty(txtUpdatedCaseNumber.Text))
                            {
                                isError = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please enter a valid casenumber');", true);
                            }
                            if (string.IsNullOrEmpty(txtUpdatedDescription.Text))
                            {
                                isError = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please enter a valid description');", true);
                            }
                            if (string.IsNullOrEmpty(txtUpdatedSpecification.Text))
                            {
                                isError = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please enter a valid specification');", true);
                            }

                            if (ddPacking.SelectedItem.Text == "NO NEED")
                            {
                                if (string.IsNullOrEmpty(txtUpdatedQuantity.Text))
                                {
                                    isError = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Quantity is required.');", true);
                                }
                                if (string.IsNullOrEmpty(txtUpdatedUnitPrice.Text))
                                {
                                    isError = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Unit Price is required.');", true);
                                }
                                if (string.IsNullOrEmpty(txtUpdatedMeasurement.Text))
                                {
                                    isError = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Measurement is required.');", true);
                                }
                                if (string.IsNullOrEmpty(txtUpdatedCaseNumber.Text))
                                {
                                    isError = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). CaseNumber is required.');", true);
                                }
                                if (string.IsNullOrEmpty(txtUpdatedDescription.Text))
                                {
                                    isError = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Description is required.');", true);
                                }
                                if (string.IsNullOrEmpty(txtUpdatedSpecification.Text))
                                {
                                    isError = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Specification is required.');", true);
                                }
                                if (string.IsNullOrEmpty(txtUpdatedNetWeight.Text))
                                {
                                    isError = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Net Weight is required.');", true);
                                }
                                if (string.IsNullOrEmpty(txtUpdatedGrossWeight.Text))
                                {
                                    isError = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Gross Weight is required.');", true);
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(txtUpdatedQuantity.Text))
                                {
                                    isError = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Quantity is required.');", true);
                                }
                                if (string.IsNullOrEmpty(txtUpdatedUnitPrice.Text))
                                {
                                    isError = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Unit Price is required.');", true);
                                }
                                if (string.IsNullOrEmpty(txtUpdatedCaseNumber.Text))
                                {
                                    isError = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). CaseNumber is required.');", true);
                                }
                                if (string.IsNullOrEmpty(txtUpdatedDescription.Text))
                                {
                                    isError = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Description is required.');", true);
                                }
                                if (string.IsNullOrEmpty(txtUpdatedSpecification.Text))
                                {
                                    isError = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Specification is required.');", true);
                                }
                                if (string.IsNullOrEmpty(txtUpdatedNetWeight.Text))
                                {
                                    isError = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Net Weight is required.');", true);
                                }
                            }


                        }
                    }
                }
                else
                {
                    for (int i = 0; i < gvData.Rows.Count; i++)
                    {
                        TextBox txtQuantity = (TextBox)gvData.Rows[i].Cells[2].FindControl("txtQuantity");
                        TextBox txtUnitPrice = (TextBox)gvData.Rows[i].Cells[5].FindControl("txtUnitPrice");
                        TextBox txtMeasurement = (TextBox)gvData.Rows[i].Cells[8].FindControl("txtMeasurement");

                        TextBox txtCaseNumber = (TextBox)gvData.Rows[i].Cells[1].FindControl("txtCaseNumber");
                        TextBox txtDescription = (TextBox)gvData.Rows[i].Cells[1].FindControl("txtDescription");
                        TextBox txtSpecification = (TextBox)gvData.Rows[i].Cells[1].FindControl("txtSpecification");
                        TextBox txtNetWeight = (TextBox)gvData.Rows[i].Cells[6].FindControl("txtNetWeight");
                        TextBox txtGrossWeight = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtGrossWeight");

                        if (!COMMON.isNumeric(txtQuantity.Text.Trim(), System.Globalization.NumberStyles.AllowThousands) || String.IsNullOrEmpty(txtQuantity.Text.Trim()))
                        {
                            isError = true;
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please enter a valid quantity');", true);
                        }
                        if (!COMMON.isNumeric(txtUnitPrice.Text.Trim(), System.Globalization.NumberStyles.AllowDecimalPoint) || String.IsNullOrEmpty(txtUnitPrice.Text.Trim()))
                        {
                            isError = true;
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please enter a valid unit price');", true);
                        }
                        if (string.IsNullOrEmpty(txtCaseNumber.Text))
                        {
                            isError = true;
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please enter a valid casenumber');", true);
                        }
                        if (string.IsNullOrEmpty(txtDescription.Text))
                        {
                            isError = true;
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please enter a valid description');", true);
                        }
                        if (string.IsNullOrEmpty(txtSpecification.Text))
                        {
                            isError = true;
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please enter a valid specification');", true);
                        }                        


                        if (ddPacking.SelectedItem.Text == "NO NEED")
                        {
                            if (string.IsNullOrEmpty(txtQuantity.Text))
                            {
                                isError = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Quantity is required.');", true);
                            }
                            if (string.IsNullOrEmpty(txtUnitPrice.Text))
                            {
                                isError = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Unit Price is required.');", true);
                            }
                            if (string.IsNullOrEmpty(txtMeasurement.Text))
                            {
                                isError = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Measurement is required.');", true);
                            }
                            if (string.IsNullOrEmpty(txtCaseNumber.Text))
                            {
                                isError = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). CaseNumber is required.');", true);
                            }
                            if (string.IsNullOrEmpty(txtDescription.Text))
                            {
                                isError = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Description is required.');", true);
                            }
                            if (string.IsNullOrEmpty(txtSpecification.Text))
                            {
                                isError = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Specification is required.');", true);
                            }
                            if (string.IsNullOrEmpty(txtNetWeight.Text))
                            {
                                isError = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Net Weight is required.');", true);
                            }
                            if (string.IsNullOrEmpty(txtGrossWeight.Text))
                            {
                                isError = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Gross Weight is required.');", true);
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(txtQuantity.Text))
                            {
                                isError = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Quantity is required.');", true);
                            }
                            if (string.IsNullOrEmpty(txtUnitPrice.Text))
                            {
                                isError = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Unit Price is required.');", true);
                            }
                            if (string.IsNullOrEmpty(txtCaseNumber.Text))
                            {
                                isError = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). CaseNumber is required.');", true);
                            }
                            if (string.IsNullOrEmpty(txtDescription.Text))
                            {
                                isError = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Description is required.');", true);
                            }
                            if (string.IsNullOrEmpty(txtSpecification.Text))
                            {
                                isError = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Specification is required.');", true);
                            }
                            if (string.IsNullOrEmpty(txtNetWeight.Text))
                            {
                                isError = true;
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You have selected PACKING (NO NEED). Net Weight is required.');", true);
                            }
                        }



                    }
                }

                if (!isError)
                {

                    string ctrlNo = string.Empty;                    
                    int attachedFiles = 0;

                    Entities_PIPL_InvoiceEntry head = new Entities_PIPL_InvoiceEntry();    
                
                    // APPEND OR UPDATE
                    if (!String.IsNullOrEmpty(Request.QueryString["ControlNo_From_Details"].ToString()))
                    {
                        computeEstimatedValuesForUpdate();
                    }
                    else // INSERT
                    {
                        if (gvDataUpload.Visible)
                        {
                            computeEstimatedValuesForUpload();
                        }
                        else
                        {
                            computeEstimatedValues();
                        }
                    }

                    head.Bdn = String.IsNullOrEmpty(ddBDN.SelectedValue.ToString()) ? "-1" : ddBDN.SelectedValue;
                    head.BdnValue = String.IsNullOrEmpty(ddBDNValue.SelectedValue.ToString()) ? "-1" : ddBDNValue.SelectedValue;
                    head.Category = ddCategory.SelectedValue;
                    head.CommercialValue = ddCommercialValue.SelectedValue;
                    head.Consignee = ddConsignee.SelectedValue;
                    head.CountryOfOrigin = ddCountryOfOrigin.SelectedValue;
                    head.CtrlNo = !String.IsNullOrEmpty(Request.QueryString["ControlNo_From_Details"].ToString()) ? CryptorEngine.Decrypt(Request.QueryString["ControlNo_From_Details"].ToString().Replace(" ", "+"), true) : setControlNumberWithPrefix();
                    head.Etd = txtETDManila.Text.Trim();
                    head.InvoiceNo = txtInvoiceNumber.Text.Replace("'", "''");
                    head.ModeOfShipment = ddModeOfShipment.SelectedValue;
                    head.NatureOfGoods = ddNatureOfGoods.SelectedValue;
                    head.Packing = ddPacking.SelectedValue;
                    head.PickUpLocation = ddPickupLocation.SelectedValue;
                    head.PoNo = txtPONumber.Text.Replace("'", "''");
                    head.PortOfDestination = txtPortOfDestination.Text;
                    head.Purpose = ddPurpose.SelectedValue;                                        
                    head.TradeTerms = ddTradeTerms.SelectedValue;
                    head.ValueInUsd = txtEstimatedUsd.Text.Replace("'", "''");
                    head.ValueInYen = txtEstimatedYen.Text.Replace("'", "''");
                    head.Attention1 = txtAttention1.Text.Replace("'", "''");
                    head.Secdept1 = txtSectionDept1.Text.Replace("'", "''");
                    head.Secdept2 = string.Empty;
                    head.Attention2 = txtAttention2.Text.Replace("'", "''");
                    head.Remarks = txtRemarks.Text.Replace("'", "''");
                    head.ReferenceNo = txtReferenceNo.Text.Replace("'", "''");
                    head.PurposeOthers = String.IsNullOrEmpty(txtPurposeOthers.Text) ? string.Empty : txtPurposeOthers.Text;
                    head.SalesType = ddSalesType.SelectedValue;
                    head.BusinessUnit = ddBusinessUnit.SelectedValue;
                    head.AccountCode = ddAccountCode.SelectedValue;


                    ctrlNo = head.CtrlNo;

                    // APPEND OR UPDATE
                    if (!String.IsNullOrEmpty(Request.QueryString["ControlNo_From_Details"].ToString()))
                    {
                        head.UpdatedBy = Session["LcRefId"].ToString();
                        successHead = BLL.PIPL_TRANSACTION_RequestHead_Append(head).ToString();

                        if (successHead == "-1") // APPEND IS SUCCESS
                        {
                            if (gvDataUpdate.Rows.Count > 0)
                            {
                                for (int i = 0; i < gvDataUpdate.Rows.Count; i++)
                                {
                                    Entities_PIPL_InvoiceEntry details = new Entities_PIPL_InvoiceEntry();

                                    Label lblUpdatedRefId = (Label)gvDataUpdate.Rows[i].Cells[0].FindControl("lblUpdatedRefId");
                                    TextBox txtUpdatedCaseNumber = (TextBox)gvDataUpdate.Rows[i].Cells[2].FindControl("txtUpdatedCaseNumber");
                                    TextBox txtUpdatedDescription = (TextBox)gvDataUpdate.Rows[i].Cells[3].FindControl("txtUpdatedDescription");
                                    TextBox txtUpdatedSpecification = (TextBox)gvDataUpdate.Rows[i].Cells[4].FindControl("txtUpdatedSpecification");
                                    TextBox txtUpdatedQuantity = (TextBox)gvDataUpdate.Rows[i].Cells[5].FindControl("txtUpdatedQuantity");
                                    TextBox txtUpdatedUnitPrice = (TextBox)gvDataUpdate.Rows[i].Cells[8].FindControl("txtUpdatedUnitPrice");
                                    TextBox txtUpdatedNetWeight = (TextBox)gvDataUpdate.Rows[i].Cells[9].FindControl("txtUpdatedNetWeight");
                                    TextBox txtUpdatedGrossWeight = (TextBox)gvDataUpdate.Rows[i].Cells[10].FindControl("txtUpdatedGrossWeight");
                                    TextBox txtUpdatedMeasurement = (TextBox)gvDataUpdate.Rows[i].Cells[11].FindControl("txtUpdatedMeasurement");


                                    DropDownList ddUpdatedUOM = (DropDownList)gvDataUpdate.Rows[i].Cells[6].FindControl("ddUpdatedUOM");
                                    DropDownList ddUpdatedCurrency = (DropDownList)gvDataUpdate.Rows[i].Cells[7].FindControl("ddUpdatedCurrency");
                                    DropDownList ddUpdatedCaseUnit = (DropDownList)gvDataUpdate.Rows[i].Cells[1].FindControl("ddUpdatedCaseUnit");

                                    details.DetailsRefId = lblUpdatedRefId.Text.Trim();
                                    details.Description = txtUpdatedDescription.Text.Replace("'", "''");
                                    details.Specification = txtUpdatedSpecification.Text.Replace("'", "''");
                                    details.Quantity = txtUpdatedQuantity.Text.Replace("'", "''");
                                    details.Uom = ddUpdatedUOM.SelectedValue;
                                    details.Currency = ddUpdatedCurrency.SelectedValue;
                                    details.UPrice = txtUpdatedUnitPrice.Text.Replace("'", "''");
                                    details.NetWeight = txtUpdatedNetWeight.Text.Replace("'", "''");
                                    details.GrossWeight = txtUpdatedGrossWeight.Text.Replace("'", "''");
                                    details.Measurement = txtUpdatedMeasurement.Text.Replace("'", "''");
                                    details.CaseUnit = ddUpdatedCaseUnit.SelectedValue;
                                    details.CaseNumber = txtUpdatedCaseNumber.Text.Replace("'", "''");

                                    try
                                    {
                                        BLL.PIPL_TRANSACTION_RequestDetails_Append(details);
                                    }
                                    catch (Exception ex)
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                                    }
                                }

                            }


                            //UPDATE TRANSFER CATEOGORY
                            string queryBeginPart = string.Empty;
                            string queryEndPart = string.Empty;
                            string queryDetails = string.Empty;

                            queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                            queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                            queryDetails = "INSERT INTO HistoryOfUpdates (RFQNo, DetailsRefId, TableName, UpdatedBy, UpdatedDate, TransactionName, UpdateWhat) " +
                                           "VALUES ('" + ctrlNo + "','NA','PIPL_TRANSACTION','" + Session["LcRefId"].ToString() + "',GETDATE(),'Purchasing-NotMyCategory','Category from " + Session["PIPL_OLD_CATEGORY_DESCRIPTION"].ToString().ToUpper() + " to " + ddCategory.SelectedItem.Text.ToUpper() + "') ";


                            if (Session["PIPL_OLD_CATEGORY_DESCRIPTION"] != null)
                            {
                                if (Session["PIPL_OLD_CATEGORY_DESCRIPTION"].ToString().Trim().ToLower() == ddCategory.SelectedValue.Trim().ToLower())
                                {
                                    //DO NOTHING
                                }
                                else
                                {
                                    BLL.PIPL_TRANSACTION_SQLTransaction(queryBeginPart + queryDetails + queryEndPart);
                                }
                            }

                            //-------------------------------------------------------------------------------------------------------------


                            Session["successMessage"] = "CONTROL NUMBER : <b>" + ctrlNo + "</b> HAS BEEN SUCCESSFULLY UPDATED.";
                            Session["successTransactionName"] = "PIPL_INVOICE_UPDATE";
                            Session["successReturnPage"] = "PIPL_InvoiceEntry.aspx?ControlNo_From_Details=";

                            Response.Redirect("SuccessPage.aspx");

                        }
                    }
                    else // INSERT
                    {
                        try
                        {
                            if (fu1.HasFile || fu2.HasFile || fu3.HasFile || fu4.HasFile)
                            {

                                if (!System.IO.Directory.Exists(Server.MapPath("~/PIPL_Request/" + ctrlNo)))
                                {
                                    System.IO.Directory.CreateDirectory(Server.MapPath("~/PIPL_Request/" + ctrlNo));
                                }
                                if (System.IO.Directory.Exists(Server.MapPath("~/PIPL_Request/" + ctrlNo)))
                                {
                                    try
                                    {
                                        if (fu1.HasFile)
                                        {
                                            string filename1 = Path.GetFileName(fu1.FileName);
                                            string fileExtensionApplication = Path.GetExtension(filename1);
                                            fu1.SaveAs(Path.Combine(Server.MapPath("~/PIPL_Request/") + ctrlNo, filename1));
                                            fu1.Dispose();
                                            File.Copy(Path.Combine(Server.MapPath("~/PIPL_Request/" + ctrlNo), filename1), Path.Combine(Server.MapPath("~/PIPL_Request/" + ctrlNo), ("1-" + ctrlNo + fileExtensionApplication)), true);
                                            File.Delete(Path.Combine(Server.MapPath("~/PIPL_Request/" + ctrlNo), filename1));
                                            attachedFiles++;
                                        }
                                        if (fu2.HasFile)
                                        {
                                            string filename2 = Path.GetFileName(fu2.FileName);
                                            string fileExtensionApplication = Path.GetExtension(filename2);
                                            fu2.SaveAs(Path.Combine(Server.MapPath("~/PIPL_Request/") + ctrlNo, filename2));
                                            fu2.Dispose();
                                            File.Copy(Path.Combine(Server.MapPath("~/PIPL_Request/" + ctrlNo), filename2), Path.Combine(Server.MapPath("~/PIPL_Request/" + ctrlNo), ("2-" + ctrlNo + fileExtensionApplication)), true);
                                            File.Delete(Path.Combine(Server.MapPath("~/PIPL_Request/" + ctrlNo), filename2));
                                            attachedFiles++;
                                        }
                                        if (fu3.HasFile)
                                        {
                                            string filename3 = Path.GetFileName(fu3.FileName);
                                            string fileExtensionApplication = Path.GetExtension(filename3);
                                            fu3.SaveAs(Path.Combine(Server.MapPath("~/PIPL_Request/") + ctrlNo, filename3));
                                            fu3.Dispose();
                                            File.Copy(Path.Combine(Server.MapPath("~/PIPL_Request/" + ctrlNo), filename3), Path.Combine(Server.MapPath("~/PIPL_Request/" + ctrlNo), ("3-" + ctrlNo + fileExtensionApplication)), true);
                                            File.Delete(Path.Combine(Server.MapPath("~/PIPL_Request/" + ctrlNo), filename3));
                                            attachedFiles++;
                                        }
                                        if (fu4.HasFile)
                                        {
                                            string filename4 = Path.GetFileName(fu4.FileName);
                                            string fileExtensionApplication = Path.GetExtension(filename4);
                                            fu4.SaveAs(Path.Combine(Server.MapPath("~/PIPL_Request/") + ctrlNo, filename4));
                                            fu4.Dispose();
                                            File.Copy(Path.Combine(Server.MapPath("~/PIPL_Request/" + ctrlNo), filename4), Path.Combine(Server.MapPath("~/PIPL_Request/" + ctrlNo), ("4-" + ctrlNo + fileExtensionApplication)), true);
                                            File.Delete(Path.Combine(Server.MapPath("~/PIPL_Request/" + ctrlNo), filename4));
                                            head.Attachment += "4-" + ctrlNo + fileExtensionApplication + ", ";
                                            attachedFiles++;
                                        }

                                        if (attachedFiles > 0)
                                        {
                                            head.Attachment = attachedFiles.ToString();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                                    }
                                }

                            }
                            else
                            {
                                head.Attachment = "0";
                            }

                            head.Requester = Session["LcRefId"].ToString();

                            successHead = BLL.PIPL_TRANSACTION_RequestHead_Insert(head).ToString();
                            successStatus = BLL.PIPL_TRANSACTION_RequestStatus_Insert(head).ToString();
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                        }

                        if (successHead == "-1" && successStatus == "-1")
                        {
                            if (gvDataUpload.Visible)
                            {
                                for (int i = 0; i < gvDataUpload.Rows.Count; i++)
                                {
                                    Entities_PIPL_InvoiceEntry details = new Entities_PIPL_InvoiceEntry();

                                    DropDownList ddUploadCaseUnit = (DropDownList)gvDataUpload.Rows[i].Cells[3].FindControl("ddUploadCaseUnit");
                                    TextBox txtUploadCaseNumber = (TextBox)gvDataUpload.Rows[i].Cells[1].FindControl("txtUploadCaseNumber");
                                    TextBox txtUploadDescription = (TextBox)gvDataUpload.Rows[i].Cells[1].FindControl("txtUploadDescription");
                                    TextBox txtUploadSpecification = (TextBox)gvDataUpload.Rows[i].Cells[1].FindControl("txtUploadSpecification");
                                    TextBox txtUploadQuantity = (TextBox)gvDataUpload.Rows[i].Cells[2].FindControl("txtUploadQuantity");
                                    DropDownList ddUploadUOM = (DropDownList)gvDataUpload.Rows[i].Cells[3].FindControl("ddUploadUOM");
                                    DropDownList ddUploadCurrency = (DropDownList)gvDataUpload.Rows[i].Cells[4].FindControl("ddUploadCurrency");
                                    TextBox txtUploadUnitPrice = (TextBox)gvDataUpload.Rows[i].Cells[5].FindControl("txtUploadUnitPrice");
                                    TextBox txtUploadNetWeight = (TextBox)gvDataUpload.Rows[i].Cells[6].FindControl("txtUploadNetWeight");
                                    TextBox txtUploadGrossWeight = (TextBox)gvDataUpload.Rows[i].Cells[7].FindControl("txtUploadGrossWeight");
                                    TextBox txtUploadMeasurement = (TextBox)gvDataUpload.Rows[i].Cells[8].FindControl("txtUploadMeasurement");


                                    details.HeadCTRLNo = ctrlNo;
                                    details.Description = txtUploadDescription.Text.Replace("'", "''");
                                    details.Specification = txtUploadSpecification.Text.Replace("'", "''");
                                    details.Quantity = txtUploadQuantity.Text.Replace("'", "''");
                                    details.Uom = ddUploadUOM.SelectedValue;
                                    details.Currency = ddUploadCurrency.SelectedValue;
                                    details.UPrice = txtUploadUnitPrice.Text.Replace("'", "''");
                                    details.NetWeight = txtUploadNetWeight.Text.Replace("'", "''");
                                    details.GrossWeight = txtUploadGrossWeight.Text.Replace("'", "''");
                                    details.Measurement = txtUploadMeasurement.Text.Replace("'", "''");
                                    details.CaseUnit = ddUploadCaseUnit.SelectedValue;
                                    details.CaseNumber = txtUploadCaseNumber.Text.Replace("'", "''");

                                    try
                                    {
                                        BLL.PIPL_TRANSACTION_RequestDetails_Insert(details);
                                    }
                                    catch (Exception ex)
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                                    }
                                }

                            }
                            else
                            {

                                for (int i = 0; i < gvData.Rows.Count; i++)
                                {
                                    Entities_PIPL_InvoiceEntry details = new Entities_PIPL_InvoiceEntry();

                                    DropDownList ddCaseUnit = (DropDownList)gvData.Rows[i].Cells[3].FindControl("ddCaseUnit");
                                    TextBox txtCaseNumber = (TextBox)gvData.Rows[i].Cells[1].FindControl("txtCaseNumber");
                                    TextBox txtDescription = (TextBox)gvData.Rows[i].Cells[1].FindControl("txtDescription");
                                    TextBox txtSpecification = (TextBox)gvData.Rows[i].Cells[1].FindControl("txtSpecification");
                                    TextBox txtQuantity = (TextBox)gvData.Rows[i].Cells[2].FindControl("txtQuantity");
                                    DropDownList ddUOM = (DropDownList)gvData.Rows[i].Cells[3].FindControl("ddUOM");
                                    DropDownList ddCurrency = (DropDownList)gvData.Rows[i].Cells[4].FindControl("ddCurrency");
                                    TextBox txtUnitPrice = (TextBox)gvData.Rows[i].Cells[5].FindControl("txtUnitPrice");
                                    TextBox txtNetWeight = (TextBox)gvData.Rows[i].Cells[6].FindControl("txtNetWeight");
                                    TextBox txtGrossWeight = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtGrossWeight");
                                    TextBox txtMeasurement = (TextBox)gvData.Rows[i].Cells[8].FindControl("txtMeasurement");


                                    details.HeadCTRLNo = ctrlNo;
                                    details.Description = txtDescription.Text.Replace("'", "''");
                                    details.Specification = txtSpecification.Text.Replace("'", "''");
                                    details.Quantity = txtQuantity.Text.Replace("'", "''");
                                    details.Uom = ddUOM.SelectedValue;
                                    details.Currency = ddCurrency.SelectedValue;
                                    details.UPrice = txtUnitPrice.Text.Replace("'", "''");
                                    details.NetWeight = txtNetWeight.Text.Replace("'", "''");
                                    details.GrossWeight = txtGrossWeight.Text.Replace("'", "''");
                                    details.Measurement = txtMeasurement.Text.Replace("'", "''");
                                    details.CaseUnit = ddCaseUnit.SelectedValue;
                                    details.CaseNumber = txtCaseNumber.Text.Replace("'", "''");

                                    try
                                    {
                                        BLL.PIPL_TRANSACTION_RequestDetails_Insert(details);
                                    }
                                    catch (Exception ex)
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                                    }
                                }

                            }

                        }

                        Session["successMessage"] = "YOUR REQUEST HAS BEEN SUCCESSFULY CREATED. YOUR CONTROL NUMBER IS <b>" + ctrlNo + "</b>";
                        Session["successTransactionName"] = "PIPL_INVOICE_ENTRY";
                        Session["successReturnPage"] = "PIPL_InvoiceEntry.aspx?ControlNo_From_Details=";

                        Response.Redirect("SuccessPage.aspx");

                    } // end of else INSERT

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "'\n\n\n Tips : Try to log-out then log-in again to the system again. Maybe the error cause is session timeout. Always keep in mind that our system expires the sessions in 10 minutes idle time. Please do log-out if you have no any transations needed to update and etc. Thank You!);", true);
            }


        
        }



    }
}
