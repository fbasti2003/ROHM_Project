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
    public partial class DRF_AllRequest : System.Web.UI.Page
    {
        BLL_DRF BLL = new BLL_DRF();
        Common COMMON = new Common();        
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    txtFrom.Text = DateTime.Today.AddDays(-360).ToString("MM/dd/yyyy");
                    txtTo.Text = DateTime.Today.AddDays(7).ToString("MM/dd/yyyy");

                    if (Session["Search_From_DRF_Inquiry"] != null)
                    {
                        if (!string.IsNullOrEmpty(Session["Search_From_DRF_Inquiry"].ToString()))
                        {
                            txtFrom.Text = DateTime.Today.AddDays(-360).ToString("MM/dd/yyyy");
                            txtTo.Text = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");

                            txtSearch.Text = Session["Search_From_DRF_Inquiry"].ToString().TrimStart().TrimEnd();
                            Session["Search_From_DRF_Inquiry"] = null;
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

                List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();
                Entities_DRF_RequestEntry request = new Entities_DRF_RequestEntry();

                request.DrFrom = txtFrom.Text;
                request.DrTo = txtTo.Text;
                request.SearchCriteria = txtSearch.Text;

                list = null;

                if (supplyChain)
                {

                    if (txtSearch.Text.Length > 0)
                    {
                        if (ddItemStatus.SelectedValue.ToLower() == "pending")
                        {
                            list = BLL.DRF_TRANSACTION_AllRequest_Like(request).Where(itm => itm.ReqManagerStat == "0" || itm.PurInchargeStat == "0" || itm.PurManagerStat == "0" || itm.ReqManagerStat == null || itm.PurInchargeStat == null || itm.PurManagerStat == null).ToList();
                        }
                        if (ddItemStatus.SelectedValue.ToLower() == "disapproved")
                        {
                            list = BLL.DRF_TRANSACTION_AllRequest_Like(request).Where(itm => itm.ReqManagerStat == "2" || itm.PurInchargeStat == "2" || itm.PurManagerStat == "2").ToList();
                        }
                        if (ddItemStatus.SelectedValue.ToLower() == "approved")
                        {
                            list = BLL.DRF_TRANSACTION_AllRequest_Like(request).Where(itm => itm.PurManagerStat == "1").ToList();
                        }
                        if (ddItemStatus.SelectedValue.ToLower() == "closed")
                        {
                            list = BLL.DRF_TRANSACTION_AllRequest_Like(request).Where(itm => itm.Posted == "1").ToList();
                        }
                        if (ddItemStatus.SelectedValue.ToLower() == "all")
                        {
                            list = BLL.DRF_TRANSACTION_AllRequest_Like(request);
                        }
                    }
                    else
                    {

                        if (ddItemStatus.SelectedValue.ToLower() == "pending")
                        {
                            list = BLL.DRF_TRANSACTION_AllRequest(request).Where(itm => itm.ReqManagerStat == "0" || itm.PurInchargeStat == "0" || itm.PurManagerStat == "0" || itm.ReqManagerStat == null || itm.PurInchargeStat == null || itm.PurManagerStat == null).ToList();
                        }
                        if (ddItemStatus.SelectedValue.ToLower() == "disapproved")
                        {
                            list = BLL.DRF_TRANSACTION_AllRequest(request).Where(itm => itm.ReqManagerStat == "2" || itm.PurInchargeStat == "2" || itm.PurManagerStat == "2").ToList();
                        }
                        if (ddItemStatus.SelectedValue.ToLower() == "approved")
                        {
                            list = BLL.DRF_TRANSACTION_AllRequest(request).Where(itm => itm.PurManagerStat == "1").ToList();
                        }
                        if (ddItemStatus.SelectedValue.ToLower() == "closed")
                        {
                            list = BLL.DRF_TRANSACTION_AllRequest(request).Where(itm => itm.Posted == "1").ToList();
                        }
                        if (ddItemStatus.SelectedValue.ToLower() == "all")
                        {
                            list = BLL.DRF_TRANSACTION_AllRequest(request);
                        }
                    }

                }
                else
                {
                    if (txtSearch.Text.Length > 0)
                    {
                        list = BLL.DRF_TRANSACTION_AllRequest_Like(request).Where(itm => itm.Division == Session["Division"].ToString()).ToList();
                    }
                    else
                    {
                        list = BLL.DRF_TRANSACTION_AllRequest(request).Where(itm => itm.Division == Session["Division"].ToString()).ToList();
                    }
                }

                if (list != null)
                {
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

                 LinkButton lblCtrl = row.FindControl("lblCtrl") as LinkButton;
                 LinkButton lbPrint = row.FindControl("lbPrint") as LinkButton;
                 Button btnStatus = row.FindControl("btnStatus") as Button;

                 TextBox txtPRNo = row.FindControl("txtPRNo") as TextBox;
                 TextBox txtPONo = row.FindControl("txtPONo") as TextBox;
                 TextBox txtPODate = row.FindControl("txtPODate") as TextBox;

                 Label lblSupplier = row.FindControl("lblSupplier") as Label;
                 Label lblAttention = row.FindControl("lblAttention") as Label;

                 TextBox txtDescription = row.FindControl("txtDescription") as TextBox;
                 TextBox txtInvoiceDRNo = row.FindControl("txtInvoiceDRNo") as TextBox;
                 TextBox txtReceivedDate = row.FindControl("txtReceivedDate") as TextBox;
                 TextBox txtTypeDrawing = row.FindControl("txtTypeDrawing") as TextBox;
                 TextBox txtOrderQuantity = row.FindControl("txtOrderQuantity") as TextBox;
                 TextBox txtAbnormalQuantity = row.FindControl("txtAbnormalQuantity") as TextBox;
                 TextBox txtTypesOfAbnormality = row.FindControl("txtTypesOfAbnormality") as TextBox;
                 TextBox txtDetailedReport = row.FindControl("txtDetailedReport") as TextBox;
                 TextBox txtResponseType = row.FindControl("txtResponseType") as TextBox;
                 TextBox txtResponseAnswer = row.FindControl("txtResponseAnswer") as TextBox;

                 Label lblReqManagerStatColor = row.FindControl("lblReqManagerStatColor") as Label;
                 Label lblPurInchargeStatColor = row.FindControl("lblPurInchargeStatColor") as Label;
                 Label lblPurManagerStatColor = row.FindControl("lblPurManagerStatColor") as Label;

                 Label lblRequester = row.FindControl("lblRequester") as Label;
                 Label lblReqManager = row.FindControl("lblReqManager") as Label;
                 Label lblPurIncharge = row.FindControl("lblPurIncharge") as Label;
                 Label lblPurManager = row.FindControl("lblPurManager") as Label;

                 Label lblReqManagerDOAStat = row.FindControl("lblReqManagerDOAStat") as Label;
                 Label lblPurInchargeDOAStat = row.FindControl("lblPurInchargeDOAStat") as Label;
                 Label lblPurManagerDOAStat = row.FindControl("lblPurManagerDOAStat") as Label;
                 Label lblRequesterDOAStat = row.FindControl("lblRequesterDOAStat") as Label;


                 if (e.CommandName == "lblCtrl_Command")
                 {
                     string URL = "~/DRF_RequestEntry.aspx?DRFNo_From_Inquiry=" + CryptorEngine.Encrypt(lblCtrl.Text.Trim(), true);

                     URL = Page.ResolveClientUrl(URL);
                     lblCtrl.OnClientClick = "window.open('" + URL + "'); return false;";
                 }

                 if (e.CommandName == "lbPrint_Command")
                 {
                     // REPORT

                     string URL = "~/Reporting/DRF/RPT_DRF.aspx?CTRLNo=" + CryptorEngine.Encrypt(lblCtrl.Text.Trim(), true);

                     URL = Page.ResolveClientUrl(URL);
                     lbPrint.OnClientClick = "window.open('" + URL + "'); return false;";

                     
                 }

                 if (e.CommandName == "lbPreview_Command")
                 {
                     string pathLocation = Server.MapPath("~/DRF_Request/" + lblCtrl.Text.Trim() + "/REPORT_" + lblCtrl.Text.Trim() + ".html");
                     string htmlTemplate = Server.MapPath("~/HTML_Report/DRF/DRF.txt");

                     if (System.IO.File.Exists(htmlTemplate))
                     {
                         string templateValue = System.IO.File.ReadAllText(htmlTemplate).Replace("val_ctrlno", lblCtrl.Text.Trim())
                                                                                        .Replace("val_attention", lblAttention.Text)
                                                                                        .Replace("val_prno", txtPRNo.Text)
                                                                                        .Replace("val_pono", txtPONo.Text)
                                                                                        .Replace("val_podate", txtPODate.Text)
                                                                                        .Replace("val_invoicedrno", txtInvoiceDRNo.Text)
                                                                                        .Replace("val_description", txtDescription.Text)
                                                                                        .Replace("val_orderquantity", txtOrderQuantity.Text)
                                                                                        .Replace("val_abnormalquantity", txtAbnormalQuantity.Text)
                                                                                        .Replace("val_receiveddate", txtReceivedDate.Text)
                                                                                        .Replace("val_typesofabnormality", txtTypesOfAbnormality.Text)
                                                                                        .Replace("val_detailedreport", txtDetailedReport.Text)
                                                                                        .Replace("val_type", txtTypeDrawing.Text)
                                                                                        .Replace("val_responsetype", txtResponseType.Text)
                                                                                        .Replace("val_responseanswer", txtResponseAnswer.Text)
                                                                                        .Replace("val_supplier_conforme", !string.IsNullOrEmpty(txtResponseAnswer.Text) ? lblAttention.Text : string.Empty)
                                                                                        .Replace("val_preparedby", lblRequester.Text)
                                                                                        .Replace("val_notedby", lblReqManager.Text)
                                                                                        .Replace("val_incharge", lblPurIncharge.Text)
                                                                                        .Replace("val_manager", lblPurManager.Text)
                                                                                        .Replace("val_doa_preparedby", lblRequesterDOAStat.Text)
                                                                                        .Replace("val_doa_notedby", lblReqManagerDOAStat.Text)
                                                                                        .Replace("val_doa_incharge", lblPurInchargeDOAStat.Text)
                                                                                        .Replace("val_doa_manager", lblPurManagerDOAStat.Text)
                                                                                        .Replace("bg_v_nb", "background-color:" + lblReqManagerStatColor.Text + ";")
                                                                                        .Replace("bg_v_i", "background-color:" + lblPurInchargeStatColor.Text + ";")
                                                                                        .Replace("bg_v_m", "background-color:" + lblPurManagerStatColor.Text + ";")
                                                                                        .Replace("valtoa_wrongtype", setTypesOfAbnormality(txtTypesOfAbnormality.Text, "valtoa_wrongtype"))
                                                                                        .Replace("valtoa_wrongmeasurement", setTypesOfAbnormality(txtTypesOfAbnormality.Text, "valtoa_wrongmeasurement"))
                                                                                        .Replace("valtoa_excessquantity", setTypesOfAbnormality(txtTypesOfAbnormality.Text, "valtoa_excessquantity"))
                                                                                        .Replace("valtoa_lackingquantity", setTypesOfAbnormality(txtTypesOfAbnormality.Text, "valtoa_lackingquantity"))
                                                                                        .Replace("valtoa_incompleteprocessing", setTypesOfAbnormality(txtTypesOfAbnormality.Text, "valtoa_incompleteprocessing"))
                                                                                        .Replace("valtoa_misinterpretation", setTypesOfAbnormality(txtTypesOfAbnormality.Text, "valtoa_misinterpretation"))
                                                                                        .Replace("valtoa_doubledelivery", setTypesOfAbnormality(txtTypesOfAbnormality.Text, "valtoa_doubledelivery"))
                                                                                        .Replace("valtoa_differentmaterial", setTypesOfAbnormality(txtTypesOfAbnormality.Text, "valtoa_differentmaterial"))
                                                                                        .Replace("val_supplier", lblSupplier.Text);


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

                     Response.Redirect("DRF_Request/" + lblCtrl.Text.Trim() + "/REPORT_" + lblCtrl.Text.Trim() + ".html", false);

                 }

                 if (e.CommandName == "btnStatus_Command")
                 {
                     if (btnStatus.Text == "FOR BUYER APPROVAL")
                     {
                         Session["Search_From_DRF_AllRequest"] = lblCtrl.Text.Trim();
                         Response.Redirect("DRF_ApprovalForm.aspx");
                     }

                     if (btnStatus.Text == "APPROVED / FOR SENDING" || btnStatus.Text == "SENT TO SUPPLIER / FOR RESEND")
                     {
                         Session["Search_From_DRF_AllRequest"] = lblCtrl.Text.Trim();
                         Response.Redirect("DRF_ReceivingEntry.aspx");
                     }
                 }


             }
             catch (Exception ex)
             {
                 ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.StackTrace + "');", true);
             }
         }


         protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
         {
             try
             {
                 if (e.Row.RowType == DataControlRowType.DataRow)
                 {
                     Label lblReqManagerStat = (Label)e.Row.FindControl("lblReqManagerStat");
                     Label lblPurInchargeStat = (Label)e.Row.FindControl("lblPurInchargeStat");
                     Label lblPurManagerStat = (Label)e.Row.FindControl("lblPurManagerStat");
                     Label lblBuyerSendStat = (Label)e.Row.FindControl("lblBuyerSendStat");
                     Label lblSupplierResponded = (Label)e.Row.FindControl("lblSupplierResponded");
                     Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                     Label lblPosted = (Label)e.Row.FindControl("lblPosted");
                     Button btnStatus = (Button)e.Row.FindControl("btnStatus");

                     Label lblSendDates = (Label)e.Row.FindControl("lblSendDates");
                     DropDownList ddSendDates = (DropDownList)e.Row.FindControl("ddSendDates");

                     HtmlControl tableSupplierResponseType = (HtmlControl)e.Row.FindControl("tableSupplierResponseType");
                     HtmlControl tableSupplierResponseAnswer = (HtmlControl)e.Row.FindControl("tableSupplierResponseAnswer");

                     //-------------- REQ MANAGER -------------------------------
                     if (lblReqManagerStat.Text == "0" && lblPurInchargeStat.Text == "0" && lblPurManagerStat.Text == "0")
                     {
                         lblStatus.Visible = true;
                         lblStatus.Text = "FOR PROD. MANAGER APPROVAL";
                         lblStatus.Style.Add("background-color", "#f44336");
                         btnStatus.Visible = false;
                     }
                     //---------------------------------------------------------


                     //-------------- BUYERR -------------------------------
                     if (lblReqManagerStat.Text == "1" && lblPurInchargeStat.Text == "0" && lblPurManagerStat.Text == "0")
                     {
                         if (Session["DRF_PurchasingBuyer"].ToString().ToLower() == "true")
                         {
                             lblStatus.Visible = false;
                             btnStatus.Visible = true;
                             btnStatus.Style.Add("background-color", "#9C27B0");
                             btnStatus.Text = "FOR BUYER APPROVAL";
                         }
                         else
                         {
                             lblStatus.Visible = true;
                             lblStatus.Text = "FOR BUYER APPROVAL";
                             lblStatus.Style.Add("background-color", "#9C27B0");
                             btnStatus.Visible = false;
                         }
                     }
                     //---------------------------------------------------------     

                     //-------------- PUR MANAGER -------------------------------
                     if (lblReqManagerStat.Text == "1" && lblPurInchargeStat.Text == "1" && lblPurManagerStat.Text == "0")
                     {
                         if (Session["DRF_PurchasingIncharge"].ToString().ToLower() == "true" || Session["DRF_PurchasingDeptManager"].ToString().ToLower() == "true" || Session["DRF_PurchasingDivManager"].ToString().ToLower() == "true")
                         {
                             lblStatus.Visible = false;
                             btnStatus.Visible = true;
                             btnStatus.Style.Add("background-color", "#673AB7");
                             btnStatus.Text = "FOR PUR. MANAGER APPROVAL";
                         }
                         else
                         {
                             lblStatus.Visible = true;
                             lblStatus.Text = "FOR PUR. MANAGER APPROVAL";
                             lblStatus.Style.Add("background-color", "#673AB7");
                             btnStatus.Visible = false;
                         }
                     }
                     //---------------------------------------------------------

                     //-------------- APPROVED / FOR SENDING -------------------------------
                     if (lblReqManagerStat.Text == "1" && lblPurInchargeStat.Text == "1" && lblPurManagerStat.Text == "1" && lblBuyerSendStat.Text == "0")
                     {
                         if (Session["DRF_PurchasingBuyer"].ToString().ToLower() == "true" || Session["DRF_PurchasingIncharge"].ToString().ToLower() == "true" || Session["DRF_PurchasingDeptManager"].ToString().ToLower() == "true" || Session["DRF_PurchasingDivManager"].ToString().ToLower() == "true")
                         {
                             lblStatus.Visible = false;
                             btnStatus.Visible = true;
                             btnStatus.Style.Add("background-color", "#009688");
                             btnStatus.Text = "APPROVED / FOR SENDING";
                         }
                         else
                         {
                             lblStatus.Visible = true;
                             lblStatus.Text = "APPROVED / FOR SENDING";
                             lblStatus.Style.Add("background-color", "#009688");
                             btnStatus.Visible = false;
                         }
                     }

                     //-------------- SENT TO SUPPLIER / FOR RESEND -------------------------------
                     if (lblReqManagerStat.Text == "1" && lblPurInchargeStat.Text == "1" && lblPurManagerStat.Text == "1" && lblBuyerSendStat.Text == "1")
                     {
                         if (Session["DRF_PurchasingBuyer"].ToString().ToLower() == "true" || Session["DRF_PurchasingIncharge"].ToString().ToLower() == "true" || Session["DRF_PurchasingDeptManager"].ToString().ToLower() == "true" || Session["DRF_PurchasingDivManager"].ToString().ToLower() == "true")
                         {
                             lblStatus.Visible = false;
                             btnStatus.Visible = true;
                             btnStatus.Style.Add("background-color", "#8BC34A");
                             btnStatus.Text = "SENT TO SUPPLIER / FOR RESEND";
                         }
                         else
                         {
                             lblStatus.Visible = true;
                             lblStatus.Text = "SENT TO SUPPLIER / FOR RESEND";
                             lblStatus.Style.Add("background-color", "#8BC34A");
                             btnStatus.Visible = false;
                         }
                     }
                     //---------------------------------------------------------

                     //-------------- SUPPLIER RESPONDED -------------------------------
                     if (lblReqManagerStat.Text == "1" && lblPurInchargeStat.Text == "1" && lblPurManagerStat.Text == "1" && lblBuyerSendStat.Text == "1" && lblSupplierResponded.Text == "1")
                     {
                         lblStatus.Visible = true;
                         lblStatus.Text = "SUPPLIER RESPONDED";
                         lblStatus.Style.Add("background-color", "#673AB7");
                         btnStatus.Visible = false;

                         tableSupplierResponseType.Style.Add("display", "block");
                         tableSupplierResponseAnswer.Style.Add("display", "block");
                     }
                     //---------------------------------------------------------

                     //-------------- DISAPPROVED -------------------------------
                     if (lblReqManagerStat.Text == "2" || lblPurInchargeStat.Text == "2" || lblPurManagerStat.Text == "2")
                     {
                         lblStatus.Visible = true;
                         lblStatus.Text = "DISAPPROVED";
                         lblStatus.Style.Add("background-color", "#ffbb33");
                         btnStatus.Visible = false;
                     }
                     //---------------------------------------------------------

                     //-------------- CLOSED -------------------------------
                     if (lblPosted.Text == "1")
                     {
                         lblStatus.Visible = true;
                         lblStatus.Text = "CLOSED TRANSACTION";
                         lblStatus.Style.Add("background-color", "RED");
                         btnStatus.Visible = false;
                     }
                     //---------------------------------------------------------

                     // SEND DATES
                     if (!string.IsNullOrEmpty(lblSendDates.Text))
                     {
                         var query = from val in lblSendDates.Text.Split(',')
                                     select val;

                         ddSendDates.Items.Clear();

                         foreach (string str in query)
                         {
                             ddSendDates.Items.Add(str);
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

         private string setTypesOfAbnormality(string reason, string value)
         {
             string ret = "white;";

             if (reason == "WRONG TYPE" && value == "valtoa_wrongtype")
             {
                 ret = "black;";
             }

             else if (reason == "WRONG MEASUREMENT" && value == "valtoa_wrongmeasurement")
             {
                 ret = "black;";
             }

             else if (reason == "EXCESS QUANTITY RECEIVED" && value == "valtoa_excessquantity")
             {
                 ret = "black;";
             }

             else if (reason == "LACKING QUANTITY" && value == "valtoa_lackingquantity")
             {
                 ret = "black;";
             }

             else if (reason == "INCOMPLETE PROCESSING" && value == "valtoa_incompleteprocessing")
             {
                 ret = "black;";
             }

             else if (reason == "MISINTERPRETATION OF DRAWING" && value == "valtoa_misinterpretation")
             {
                 ret = "black;";
             }

             else if (reason == "DOUBLE DELIVERY" && value == "valtoa_doubledelivery")
             {
                 ret = "black;";
             }

             else if (reason == "DIFFERENT MATERIALS USED" && value == "valtoa_differentmaterial")
             {
                 ret = "black;";
             }

             else if (reason == "" && value == "valtoa_others")
             {
                 ret = "black;";
             }


             return ret;
         }



    }
}
