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
    public partial class PIPL_InvoicePrint : System.Web.UI.Page
    {
        BLL_PIPL BLL = new BLL_PIPL();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["ControlNo_From_Inquiry"].ToString()))
                {
                    LoadDefault();
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

                details.CtrlNo = CryptorEngine.Decrypt(Request.QueryString["ControlNo_From_Inquiry"].ToString().Replace(" ", "+"), true);
                list = BLL.PIPL_TRANSACTION_RequestDetails_GetByControlNo(details);

                int counter = 0;

                if (list.Count > 0)
                {
                    foreach (Entities_PIPL_InvoiceEntry entity in list)
                    {
                        lblConsignee.Text = entity.Consignee;
                        lblAttention1.Text = entity.Attention1;
                        lblSectionDept1.Text = entity.Secdept1;
                        lblModeOfShipment.Text = entity.ModeOfShipment;
                        lblPurpose.Text = entity.Purpose;
                        lblPacking.Text = entity.Packing;
                        lblNatureOfGoods.Text = entity.NatureOfGoods;
                        lblCountryOfOrigin.Text = entity.CountryOfOrigin;
                        lblCategory.Text = entity.Category;
                        lblRequester.Text = entity.Requester;
                        lblPortOfDestination.Text = entity.PortOfDestination;
                        lblETDManila.Text = entity.Etd;
                        lblEstimatedYen.Text = entity.ValueInYen;
                        lblEstimatedUsd.Text = entity.ValueInUsd;
                        lblPONumber.Text = entity.PoNo;
                        lblInvoiceNumber.Text = entity.InvoiceNo;
                        lblCommercialValue.Text = entity.CommercialValue;
                        lblTradeTerms.Text = entity.TradeTerms;
                        lblPickupLocation.Text = entity.PickUpLocation;
                        lblBDN.Text = bdnValue(entity.Bdn);
                        lblBDNValue.Text = entity.BdnValue;
                        lblAttention2.Text = entity.Attention2;
                        lblReferenceNo.Text = entity.ReferenceNo;
                        txtRemarks.Text = entity.Remarks;
                        lblPurposeOthers.Text = entity.PurposeOthers;
                        txtBusinessUnit.Text = entity.BusinessUnit;
                        txtAccountCode.Text = entity.AccountCode;

                        if (entity.SalesType.Trim().ToLower() == "1")
                        {
                            txtSalesType.Text = "NON-SALES";
                        }
                        if (entity.SalesType.Trim().ToLower() == "2")
                        {
                            txtSalesType.Text = "SALES";
                        }

                        if (entity.TradeTerms == "CIF")
                        {
                            txtInsurance.Text = "ALL COVERED BY SHIPPER";
                        }
                        if (entity.TradeTerms == "FOB")
                        {
                            txtInsurance.Text = "ALL COVERED BY BUYER";
                        }
                        if (entity.TradeTerms == "DDP (FREEHOUSE)")
                        {
                            txtInsurance.Text = "ALL COVERED BY SHIPPER";
                        }
                        if (entity.TradeTerms == "EX-WORKS")
                        {
                            txtInsurance.Text = "ALL COVERED BY BUYER";
                        }
                        if (entity.TradeTerms == "DAP")
                        {
                            txtInsurance.Text = "ALL COVERED BY SHIPPER";
                        }

                        lblFu1.Text = int.Parse(entity.Attachment) > 0 ? lblFu1.Text = "1-" + entity.CtrlNo : string.Empty;
                        lblFu2.Text = int.Parse(entity.Attachment) > 1 ? lblFu2.Text = "2-" + entity.CtrlNo : string.Empty;
                        lblFu3.Text = int.Parse(entity.Attachment) > 2 ? lblFu3.Text = "3-" + entity.CtrlNo : string.Empty;
                        lblFu4.Text = int.Parse(entity.Attachment) > 3 ? lblFu4.Text = "4-" + entity.CtrlNo : string.Empty;

                        //----------------------------------------------------------------------------
                        //Commercial Value Approver Display if WITH or WITHOUT
                        if (entity.CommercialValue.ToLower() == "with")
                        {
                            divWith.Style.Add("display", "block");
                            divWithout.Style.Add("display", "none");
                            lblRequesterLocalEmailWith.Text = entity.RequesterLocalNumber + " / " + entity.RequesterEmailAddress;
                        }
                        else
                        {
                            divWith.Style.Add("display", "none");
                            divWithout.Style.Add("display", "block");
                            lblRequesterLocalEmail.Text = entity.RequesterLocalNumber + " / " + entity.RequesterEmailAddress;
                        }
                        //----------------------------------------------------------------------------
                        

                        if (counter <= 0)
                        {
                            if (entity.CommercialValue.ToLower() == "with")
                            {
                                //-------------- MANAGER WITH ----------------------------------
                                if (entity.StatManager == "0")
                                {
                                    lblManagerWith.Text = entity.Manager.Length > 0 ? entity.Manager : "PENDING";
                                    lblManagerWith.CssClass = "label label-danger";
                                    lblDOAManagerWith.Text = entity.DoaManager.Length > 0 ? entity.DoaManager : "-";
                                }
                                if (entity.StatManager == "1")
                                {
                                    lblManagerWith.Text = entity.Manager.Length > 0 ? entity.Manager : "APPROVED";
                                    lblManagerWith.CssClass = "label label-success";
                                    lblDOAManagerWith.Text = entity.DoaManager.Length > 0 ? entity.DoaManager : "-";
                                }
                                if (entity.StatManager == "2")
                                {
                                    lblManagerWith.Text = entity.Manager.Length > 0 ? entity.Manager : "DISAPPROVED";
                                    lblManagerWith.CssClass = "label label-warning";
                                    lblDOAManagerWith.Text = entity.DoaManager.Length > 0 ? entity.DoaManager : "-";
                                }
                                //---------------------------------------------------------
                            }
                            else
                            {
                                //-------------- MANAGER ----------------------------------
                                if (entity.StatManager == "0")
                                {
                                    lblManager.Text = entity.Manager.Length > 0 ? entity.Manager : "PENDING";
                                    lblManager.CssClass = "label label-danger";
                                    lblDOAManager.Text = entity.DoaManager.Length > 0 ? entity.DoaManager : "-";
                                }
                                if (entity.StatManager == "1")
                                {
                                    lblManager.Text = entity.Manager.Length > 0 ? entity.Manager : "APPROVED";
                                    lblManager.CssClass = "label label-success";
                                    lblDOAManager.Text = entity.DoaManager.Length > 0 ? entity.DoaManager : "-";
                                }
                                if (entity.StatManager == "2")
                                {
                                    lblManager.Text = entity.Manager.Length > 0 ? entity.Manager : "DISAPPROVED";
                                    lblManager.CssClass = "label label-warning";
                                    lblDOAManager.Text = entity.DoaManager.Length > 0 ? entity.DoaManager : "-";
                                }
                                //---------------------------------------------------------
                            }

                            if (entity.CommercialValue.ToLower() == "with")
                            {
                                //-------------- PC MANAGER WITH-------------------------------
                                if (entity.StatPCManager == "0")
                                {
                                    lblPCManagerWith.Text = entity.PcManager.Length > 0 ? entity.PcManager : "PENDING";
                                    lblPCManagerWith.CssClass = "label label-danger";
                                    lblDOAPCManagerWith.Text = entity.DoaPCManager.Length > 0 ? entity.DoaPCManager : "-";
                                }
                                if (entity.StatPCManager == "1")
                                {
                                    lblPCManagerWith.Text = entity.PcManager.Length > 0 ? entity.PcManager : "APPROVED";
                                    lblPCManagerWith.CssClass = "label label-success";
                                    lblDOAPCManagerWith.Text = entity.DoaPCManager.Length > 0 ? entity.DoaPCManager : "-";

                                }
                                if (entity.StatPCManager == "2")
                                {
                                    lblPCManagerWith.Text = entity.PcManager.Length > 0 ? entity.PcManager : "DISAPPROVED";
                                    lblPCManagerWith.CssClass = "label label-warning";
                                    lblDOAPCManagerWith.Text = entity.DoaPCManager.Length > 0 ? entity.DoaPCManager : "-";
                                }
                                //---------------------------------------------------------
                            }
                            else
                            {
                                //-------------- PC MANAGER -------------------------------
                                if (entity.StatPCManager == "0")
                                {
                                    lblPCManager.Text = entity.PcManager.Length > 0 ? entity.PcManager : "PENDING";
                                    lblPCManager.CssClass = "label label-danger";
                                    lblDOAPCManager.Text = entity.DoaPCManager.Length > 0 ? entity.DoaPCManager : "-";
                                }
                                if (entity.StatPCManager == "1")
                                {
                                    lblPCManager.Text = entity.PcManager.Length > 0 ? entity.PcManager : "APPROVED";
                                    lblPCManager.CssClass = "label label-success";
                                    lblDOAPCManager.Text = entity.DoaPCManager.Length > 0 ? entity.DoaPCManager : "-";

                                }
                                if (entity.StatPCManager == "2")
                                {
                                    lblPCManager.Text = entity.PcManager.Length > 0 ? entity.PcManager : "DISAPPROVED";
                                    lblPCManager.CssClass = "label label-warning";
                                    lblDOAPCManager.Text = entity.DoaPCManager.Length > 0 ? entity.DoaPCManager : "-";
                                }
                                //---------------------------------------------------------
                            }


                            if (entity.CommercialValue.ToLower() == "with")
                            {
                                //-------------- INCHARGE WITH ---------------------------------
                                if (entity.StatIncharge == "0")
                                {
                                    lblInchargeWith.Text = entity.Incharge.Length > 0 ? entity.Incharge : "PENDING";
                                    lblInchargeWith.CssClass = "label label-danger";
                                    lblDOAInchargeWith.Text = entity.DoaIncharge.Length > 0 ? entity.DoaIncharge : "-";
                                }
                                if (entity.StatIncharge == "1")
                                {
                                    lblInchargeWith.Text = entity.Incharge.Length > 0 ? entity.Incharge : "APPROVED";
                                    lblInchargeWith.CssClass = "label label-success";
                                    lblDOAInchargeWith.Text = entity.DoaIncharge.Length > 0 ? entity.DoaIncharge : "-";
                                }
                                if (entity.StatIncharge == "2")
                                {
                                    lblInchargeWith.Text = entity.Incharge.Length > 0 ? entity.Incharge : "DISAPPROVED";
                                    lblInchargeWith.CssClass = "label label-warning";
                                    lblDOAInchargeWith.Text = entity.DoaIncharge.Length > 0 ? entity.DoaIncharge : "-";
                                }
                                //---------------------------------------------------------
                            }
                            else
                            {
                                //-------------- INCHARGE ---------------------------------
                                if (entity.StatIncharge == "0")
                                {
                                    lblIncharge.Text = entity.Incharge.Length > 0 ? entity.Incharge : "PENDING";
                                    lblIncharge.CssClass = "label label-danger";
                                    lblDOAIncharge.Text = entity.DoaIncharge.Length > 0 ? entity.DoaIncharge : "-";
                                }
                                if (entity.StatIncharge == "1")
                                {
                                    lblIncharge.Text = entity.Incharge.Length > 0 ? entity.Incharge : "APPROVED";
                                    lblIncharge.CssClass = "label label-success";
                                    lblDOAIncharge.Text = entity.DoaIncharge.Length > 0 ? entity.DoaIncharge : "-";
                                }
                                if (entity.StatIncharge == "2")
                                {
                                    lblIncharge.Text = entity.Incharge.Length > 0 ? entity.Incharge : "DISAPPROVED";
                                    lblIncharge.CssClass = "label label-warning";
                                    lblDOAIncharge.Text = entity.DoaIncharge.Length > 0 ? entity.DoaIncharge : "-";
                                }
                                //---------------------------------------------------------
                            }

                            if (entity.CommercialValue.ToLower() == "with")
                            {
                                //-------------- IMPEX WITH------------------------------------
                                if (entity.StatImpex == "0")
                                {
                                    lblImpexWith.Text = entity.Impex.Length > 0 ? entity.Impex : "PENDING";
                                    lblImpexWith.CssClass = "label label-danger";
                                    lblDOAImpexWith.Text = entity.DoaImpex.Length > 0 ? entity.DoaImpex : "-";
                                }
                                if (entity.StatImpex == "1")
                                {
                                    lblImpexWith.Text = entity.Impex.Length > 0 ? entity.Impex : "APPROVED";
                                    lblImpexWith.CssClass = "label label-success";
                                    lblDOAImpexWith.Text = entity.DoaImpex.Length > 0 ? entity.DoaImpex : "-";
                                }
                                if (entity.StatImpex == "2")
                                {
                                    lblImpexWith.Text = entity.Impex.Length > 0 ? entity.Impex : "DISAPPROVED";
                                    lblImpexWith.CssClass = "label label-warning";
                                    lblDOAImpexWith.Text = entity.DoaImpex.Length > 0 ? entity.DoaImpex : "-";
                                }
                                //---------------------------------------------------------
                            }
                            else
                            {
                                //-------------- IMPEX ------------------------------------
                                if (entity.StatImpex == "0")
                                {
                                    lblImpex.Text = entity.Impex.Length > 0 ? entity.Impex : "PENDING";
                                    lblImpex.CssClass = "label label-danger";
                                    lblDOAImpex.Text = entity.DoaImpex.Length > 0 ? entity.DoaImpex : "-";
                                }
                                if (entity.StatImpex == "1")
                                {
                                    lblImpex.Text = entity.Impex.Length > 0 ? entity.Impex : "APPROVED";
                                    lblImpex.CssClass = "label label-success";
                                    lblDOAImpex.Text = entity.DoaImpex.Length > 0 ? entity.DoaImpex : "-";
                                }
                                if (entity.StatImpex == "2")
                                {
                                    lblImpex.Text = entity.Impex.Length > 0 ? entity.Impex : "DISAPPROVED";
                                    lblImpex.CssClass = "label label-warning";
                                    lblDOAImpex.Text = entity.DoaImpex.Length > 0 ? entity.DoaImpex : "-";
                                }
                                //---------------------------------------------------------
                            }

                            if (entity.CommercialValue.ToLower() == "with")
                            {
                                //-------------- ACCOUNTING WITH------------------------------------
                                if (entity.StatAccounting == "0")
                                {
                                    lblAccountingWith.Text = entity.Accounting.Length > 0 ? entity.Accounting : "PENDING";
                                    lblAccountingWith.CssClass = "label label-danger";
                                    lblDOAAccountingWith.Text = entity.DoaAccounting.Length > 0 ? entity.DoaAccounting : "-";
                                }
                                if (entity.StatAccounting == "1")
                                {
                                    lblAccountingWith.Text = entity.Accounting.Length > 0 ? entity.Accounting : "APPROVED";
                                    lblAccountingWith.CssClass = "label label-success";
                                    lblDOAAccountingWith.Text = entity.DoaAccounting.Length > 0 ? entity.DoaAccounting : "-";
                                }
                                if (entity.StatAccounting == "3")
                                {
                                    lblAccountingWith.Text = entity.Accounting.Length > 0 ? entity.Accounting : "AUTO-APPROVED";
                                    lblAccountingWith.CssClass = "label label-success";
                                    lblDOAAccountingWith.Text = entity.DoaAccounting.Length > 0 ? entity.DoaAccounting : "-";
                                }
                                if (entity.StatAccounting == "2")
                                {
                                    lblAccountingWith.Text = entity.Accounting.Length > 0 ? entity.Accounting : "DISAPPROVED";
                                    lblAccountingWith.CssClass = "label label-warning";
                                    lblDOAAccountingWith.Text = entity.DoaAccounting.Length > 0 ? entity.DoaAccounting : "-";
                                }
                                //---------------------------------------------------------
                            }


                        }

                        counter++;
                    }

                    gvData.DataSource = list;
                    gvData.DataBind();
                }

                lblControlNo.Text = details.CtrlNo;

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "/n/n" + ex.InnerException + "/n/n" + ex.Source + "/n/n" + ex.StackTrace + "');", true);
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






    }
}
