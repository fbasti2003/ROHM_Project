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
    public partial class DRF_AllRequest_New : System.Web.UI.Page
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

                    //---------------------------------------------------------------------------------------------------

                    List<Entities_DRF_RequestEntry> listDropDown = new List<Entities_DRF_RequestEntry>();
                    listDropDown = BLL.DRF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                    if (listDropDown != null)
                    {
                        if (listDropDown.Count > 0)
                        {
                            ddCategory.Items.Add("ALL");

                            foreach (Entities_DRF_RequestEntry entity in listDropDown)
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

                        }
                    }

                    //---------------------------------------------------------------------------------------------------

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
                        list = BLL.DRF_TRANSACTION_AllRequest_New(request).Where(itm => itm.SearchCriteria.Contains(txtSearch.Text.ToLower())).ToList();
                    }
                    else
                    {
                        if (ddItemStatus.SelectedValue.ToLower() == "all")
                        {
                            if (ddCategory.SelectedItem.Text == "ALL")
                            {
                                list = BLL.DRF_TRANSACTION_AllRequest_New(request);
                            }
                            else
                            {
                                list = BLL.DRF_TRANSACTION_AllRequest_New(request).Where(itm => itm.Category == ddCategory.SelectedItem.Text).ToList();
                            }
                        }
                        else
                        {
                            if (ddCategory.SelectedItem.Text == "ALL")
                            {
                                list = BLL.DRF_TRANSACTION_AllRequest_New(request).Where(itm => itm.StatAll == ddItemStatus.SelectedValue).ToList();
                            }
                            else
                            {
                                list = BLL.DRF_TRANSACTION_AllRequest_New(request).Where(itm => itm.StatAll == ddItemStatus.SelectedValue && itm.Category == ddCategory.SelectedItem.Text).ToList();
                            }
                        }
                    }

                }
                else
                {
                    if (txtSearch.Text.Length > 0)
                    {
                        list = BLL.DRF_TRANSACTION_AllRequest_New(request).Where(itm => itm.SearchCriteria.Contains(txtSearch.Text.ToLower()) && itm.Division == Session["Division"].ToString()).ToList();
                    }
                    else
                    {

                        if (ddItemStatus.SelectedValue.ToLower() == "all")
                        {
                            if (ddCategory.SelectedItem.Text == "ALL")
                            {
                                list = BLL.DRF_TRANSACTION_AllRequest_New(request).Where(itm => itm.Division == Session["Division"].ToString()).ToList();
                            }
                            else
                            {
                                list = BLL.DRF_TRANSACTION_AllRequest_New(request).Where(itm => itm.Category == ddCategory.SelectedItem.Text && itm.Division == Session["Division"].ToString()).ToList();
                            }
                        }
                        else
                        {
                            if (ddCategory.SelectedItem.Text == "ALL")
                            {
                                list = BLL.DRF_TRANSACTION_AllRequest_New(request).Where(itm => itm.StatAll == ddItemStatus.SelectedValue && itm.Division == Session["Division"].ToString()).ToList();
                            }
                            else
                            {
                                list = BLL.DRF_TRANSACTION_AllRequest_New(request).Where(itm => itm.StatAll == ddItemStatus.SelectedValue && itm.Category == ddCategory.SelectedItem.Text && itm.Division == Session["Division"].ToString()).ToList();
                            }
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

        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                    Label lblStatColor = (Label)e.Row.FindControl("lblStatColor");

                    lblStatus.Style.Add("background-color", lblStatColor.Text.Trim());

                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

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

                LinkButton linkCtrlNo = row.FindControl("linkCtrlNo") as LinkButton;

                if (e.CommandName == "linkCtrlNo_Command")
                {
                    Response.Redirect("DRF_RequestEntry.aspx?DRFNo_From_Inquiry=" + CryptorEngine.Encrypt(linkCtrlNo.Text.Trim(), true), false);
                }

                if (e.CommandName == "linkPreview_Command")
                {
                    List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();
                    Entities_DRF_RequestEntry request = new Entities_DRF_RequestEntry();

                    request.DrFrom = txtFrom.Text;
                    request.DrTo = txtTo.Text;
                    request.SearchCriteria = linkCtrlNo.Text.Trim();

                    list = null;

                    list = BLL.DRF_TRANSACTION_AllRequest_New(request).Where(itm => itm.CtrlNo == linkCtrlNo.Text.Trim()).ToList();

                    if (list != null)
                    {
                        if (list.Count > 0)
                        {
                            string pathLocation = Server.MapPath("~/DRF_Request/" + linkCtrlNo.Text.Trim() + "/REPORT_" + linkCtrlNo.Text.Trim() + ".html");
                            string htmlTemplate = Server.MapPath("~/HTML_Report/DRF/DRF.txt");

                            foreach (Entities_DRF_RequestEntry entity in list)
                            {
                                if (System.IO.File.Exists(htmlTemplate))
                                {
                                    string templateValue = System.IO.File.ReadAllText(htmlTemplate).Replace("val_ctrlno", linkCtrlNo.Text.Trim())
                                                                                                   .Replace("val_attention", entity.Attention)
                                                                                                   .Replace("val_prno", entity.PrNO)
                                                                                                   .Replace("val_pono", entity.PoNO)
                                                                                                   .Replace("val_podate", entity.PoDate)
                                                                                                   .Replace("val_invoicedrno", entity.InvoiceDRNO)
                                                                                                   .Replace("val_description", entity.Description)
                                                                                                   .Replace("val_orderquantity", entity.OrderQuantity)
                                                                                                   .Replace("val_abnormalquantity", entity.AbnormalQuantity)
                                                                                                   .Replace("val_receiveddate", entity.ReceivedDate)
                                                                                                   .Replace("val_typesofabnormality", entity.TypesOfAbnormality)
                                                                                                   .Replace("val_detailedreport", entity.DetailedReport)
                                                                                                   .Replace("val_type", entity.TypeDrawingNo)
                                                                                                   .Replace("val_responsetype", entity.ResponseType)
                                                                                                   .Replace("val_responseanswer", entity.ResponseAnswer)
                                                                                                   .Replace("val_supplier_conforme", !string.IsNullOrEmpty(entity.ResponseAnswer) ? entity.Attention : string.Empty)
                                                                                                   .Replace("val_preparedby", entity.Requester)
                                                                                                   .Replace("val_notedby", entity.ReqManager)
                                                                                                   .Replace("val_incharge", entity.PurIncharge)
                                                                                                   .Replace("val_manager", entity.PurManager)
                                                                                                   .Replace("val_doa_preparedby", entity.RequesterSDOA)
                                                                                                   .Replace("val_doa_notedby", entity.ReqManagerDOA)
                                                                                                   .Replace("val_doa_incharge", entity.PurInchargeDOA)
                                                                                                   .Replace("val_doa_manager", entity.PurManagerDOA)
                                                                                                   .Replace("bg_v_nb", "background-color:" + entity.ReqManagerStatColor + ";")
                                                                                                   .Replace("bg_v_i", "background-color:" + entity.PurInchargeStatColor + ";")
                                                                                                   .Replace("bg_v_m", "background-color:" + entity.PurManagerStatColor + ";")
                                                                                                   .Replace("valtoa_wrongtype", setTypesOfAbnormality(entity.TypesOfAbnormality, "valtoa_wrongtype"))
                                                                                                   .Replace("valtoa_wrongmeasurement", setTypesOfAbnormality(entity.TypesOfAbnormality, "valtoa_wrongmeasurement"))
                                                                                                   .Replace("valtoa_excessquantity", setTypesOfAbnormality(entity.TypesOfAbnormality, "valtoa_excessquantity"))
                                                                                                   .Replace("valtoa_lackingquantity", setTypesOfAbnormality(entity.TypesOfAbnormality, "valtoa_lackingquantity"))
                                                                                                   .Replace("valtoa_incompleteprocessing", setTypesOfAbnormality(entity.TypesOfAbnormality, "valtoa_incompleteprocessing"))
                                                                                                   .Replace("valtoa_misinterpretation", setTypesOfAbnormality(entity.TypesOfAbnormality, "valtoa_misinterpretation"))
                                                                                                   .Replace("valtoa_doubledelivery", setTypesOfAbnormality(entity.TypesOfAbnormality, "valtoa_doubledelivery"))
                                                                                                   .Replace("valtoa_differentmaterial", setTypesOfAbnormality(entity.TypesOfAbnormality, "valtoa_differentmaterial"))
                                                                                                   .Replace("val_supplier", entity.Supplier);


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

                                Response.Redirect("DRF_Request/" + linkCtrlNo.Text.Trim() + "/REPORT_" + linkCtrlNo.Text.Trim() + ".html", false);
                            }
                        }
                    }




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



        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!System.IO.File.Exists(Server.MapPath("~/DRF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx")))
                {
                    System.IO.File.Copy(Server.MapPath("~/DRF_XLS/DRF_AllRequest_Report.xlsx"), Server.MapPath("~/DRF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx"));
                }
                else
                {
                    System.IO.File.Delete(Server.MapPath("~/DRF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx"));
                    System.IO.File.Copy(Server.MapPath("~/DRF_XLS/DRF_AllRequest_Report.xlsx"), Server.MapPath("~/DRF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx"));
                }

                List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();
                Entities_DRF_RequestEntry status = new Entities_DRF_RequestEntry();

                status.DrFrom = txtFrom.Text.Trim();
                status.DrTo = txtTo.Text.Trim();

                bool supplyChain = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim());


                if (supplyChain)
                {
                    if (ddItemStatus.SelectedItem.Text.ToUpper() == "ALL")
                    {
                        list = BLL.DRF_TRANSACTION_AllRequest_New(status);
                    }
                    else
                    {
                        list = BLL.DRF_TRANSACTION_AllRequest_New(status).Where(itm => itm.StatAll == ddItemStatus.SelectedItem.Text.ToUpper()).ToList();
                    }

                }
                else
                {
                    if (ddItemStatus.SelectedItem.Text.ToUpper() == "ALL")
                    {
                        list = BLL.DRF_TRANSACTION_AllRequest_New(status).Where(itm => itm.Division == Session["Division"].ToString()).ToList();
                    }
                    else
                    {
                        list = BLL.DRF_TRANSACTION_AllRequest_New(status).Where(itm => itm.Division == Session["Division"].ToString() && itm.StatAll == ddItemStatus.SelectedItem.Text.ToUpper()).ToList();
                    }
                }

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        int cnt = 4;
                        string path = Server.MapPath("~/DRF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx");
                        Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + path);
                        FileStream fsBI = new FileStream(path, FileMode.Open);
                        using (SLDocument draft = new SLDocument(fsBI, "Sheet1"))
                        {

                            foreach (Entities_DRF_RequestEntry entity in list)
                            {
                                draft.SetCellValue("A" + cnt.ToString(), entity.CtrlNo);
                                draft.SetCellValue("B" + cnt.ToString(), entity.Supplier);
                                draft.SetCellValue("C" + cnt.ToString(), entity.Attention);
                                draft.SetCellValue("D" + cnt.ToString(), entity.InvoiceDRNO);
                                draft.SetCellValue("E" + cnt.ToString(), entity.PrNO);
                                draft.SetCellValue("F" + cnt.ToString(), entity.PoNO);
                                draft.SetCellValue("G" + cnt.ToString(), entity.PoDate);
                                draft.SetCellValue("H" + cnt.ToString(), entity.ReceivedDate);
                                draft.SetCellValue("I" + cnt.ToString(), entity.Category);
                                draft.SetCellValue("J" + cnt.ToString(), entity.Description);
                                draft.SetCellValue("K" + cnt.ToString(), entity.TypeDrawingNo);
                                draft.SetCellValue("L" + cnt.ToString(), entity.OrderQuantity);
                                draft.SetCellValue("M" + cnt.ToString(), entity.AbnormalQuantity);
                                draft.SetCellValue("N" + cnt.ToString(), entity.TypesOfAbnormality);
                                draft.SetCellValue("O" + cnt.ToString(), entity.DetailedReport);
                                draft.SetCellValue("P" + cnt.ToString(), entity.TransactionDate);
                                draft.SetCellValue("Q" + cnt.ToString(), entity.StatAll);
                                draft.SetCellValue("R" + cnt.ToString(), entity.Report_BuyerName);

                                cnt++;
                            }

                            fsBI.Close();
                            draft.SaveAs(path);

                        }

                        Response.Redirect("DRF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx", false);

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
