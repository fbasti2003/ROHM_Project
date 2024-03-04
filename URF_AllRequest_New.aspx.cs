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

namespace REPI_PUR_SOFRA
{
    public partial class URF_AllRequest_New : System.Web.UI.Page
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
                        txtFrom.Text = DateTime.Today.AddDays(-360).ToString("MM/dd/yyyy");
                        txtTo.Text = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");

                        if (Session["Search_From_URF_Inquiry"] != null)
                        {
                            if (!string.IsNullOrEmpty(Session["Search_From_URF_Inquiry"].ToString()))
                            {
                                

                                txtSearch.Text = Session["Search_From_URF_Inquiry"].ToString().TrimStart().TrimEnd();
                                Session["Search_From_URF_Inquiry"] = null;
                            }
                        }

                        //---------------------------------------------------------------------------------------------------

                        List<Entities_URF_RequestEntry> listDropDown = new List<Entities_URF_RequestEntry>();
                        listDropDown = BLL.URF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                        if (listDropDown != null)
                        {
                            if (listDropDown.Count > 0)
                            {
                                ddCategory.Items.Add("ALL");

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

                            }
                        }

                        //---------------------------------------------------------------------------------------------------

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
                bool supplyChain = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim());

                List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();
                Entities_URF_RequestEntry entity = new Entities_URF_RequestEntry();

                list = null;
                entity.DrFrom = txtFrom.Text.Trim();
                entity.DrTo = txtTo.Text.Trim();
                entity.Criteria = txtSearch.Text;

                if (supplyChain)
                {

                    if (txtSearch.Text.Length > 0)
                    {
                        list = BLL.URF_TRANSACTION_AllRequest_New(entity);
                    }
                    else
                    {
                        if (ddItemStatus.SelectedValue.ToLower() == "all")
                        {
                            if (ddCategory.SelectedItem.Text == "ALL")
                            {
                                list = BLL.URF_TRANSACTION_AllRequest_New(entity);
                            }
                            else
                            {
                                list = BLL.URF_TRANSACTION_AllRequest_New(entity).Where(itm => itm.RhCategoryName == ddCategory.SelectedItem.Text).ToList();
                            }
                        }
                        else
                        {
                            if (ddCategory.SelectedItem.Text == "ALL")
                            {
                                list = BLL.URF_TRANSACTION_AllRequest_New(entity).Where(itm => itm.StatAll == ddItemStatus.SelectedItem.Text).ToList();
                            }
                            else
                            {
                                list = BLL.URF_TRANSACTION_AllRequest_New(entity).Where(itm => itm.StatAll == ddItemStatus.SelectedItem.Text && itm.RhCategoryName == ddCategory.SelectedItem.Text).ToList();
                            }
                        }
                    }
                }
                else
                {
                    if (txtSearch.Text.Length > 0)
                    {
                        list = BLL.URF_TRANSACTION_AllRequest_New(entity).Where(itm => itm.Criteria.Contains(txtSearch.Text.ToLower())).ToList();
                    }
                    else
                    {
                        if (ddItemStatus.SelectedValue.ToLower() == "all")
                        {
                            if (ddCategory.SelectedItem.Text == "ALL")
                            {
                                list = BLL.URF_TRANSACTION_AllRequest_New(entity).Where(itm => itm.RhDivision == Session["Division"].ToString()).ToList();
                            }
                            else
                            {
                                list = BLL.URF_TRANSACTION_AllRequest_New(entity).Where(itm => itm.RhCategoryName == ddCategory.SelectedItem.Text && itm.RhDivision == Session["Division"].ToString()).ToList();
                            }
                        }
                        else
                        {
                            if (ddCategory.SelectedItem.Text == "ALL")
                            {
                                list = BLL.URF_TRANSACTION_AllRequest_New(entity).Where(itm => itm.StatAll == ddItemStatus.SelectedItem.Text && itm.RhDivision == Session["Division"].ToString()).ToList();
                            }
                            else
                            {
                                list = BLL.URF_TRANSACTION_AllRequest_New(entity).Where(itm => itm.StatAll == ddItemStatus.SelectedItem.Text && itm.RhCategoryName == ddCategory.SelectedItem.Text && itm.RhDivision == Session["Division"].ToString()).ToList();
                            }
                        }
                    }
                }

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        gvData.DataSource = list;
                        gvData.DataBind();
                        gvData.Visible = true;
                    }
                    else
                    {
                        gvData.Visible = false;
                    }
                }
                else
                {
                    gvData.Visible = false;
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

                LinkButton linkCTRLNO = row.FindControl("linkCTRLNO") as LinkButton;

                if (e.CommandName == "linkCTRLNO_Command")
                {
                    Response.Redirect("URF_RequestEntry_New.aspx?URFNo_From_Inquiry=" + CryptorEngine.Encrypt(linkCTRLNO.Text.Trim(), true), false);
                }

                if (e.CommandName == "linkPreview_Command")
                {
                    Response.Redirect("URF_RequestEntry_New.aspx?URFNo_From_Inquiry=" + CryptorEngine.Encrypt(linkCTRLNO.Text.Trim(), true) + "&previewrep=true", false);
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

                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!System.IO.File.Exists(Server.MapPath("~/URF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx")))
                {
                    System.IO.File.Copy(Server.MapPath("~/URF_XLS/URF_AllRequest_Report.xlsx"), Server.MapPath("~/URF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx"));
                }
                else
                {
                    System.IO.File.Delete(Server.MapPath("~/URF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx"));
                    System.IO.File.Copy(Server.MapPath("~/URF_XLS/URF_AllRequest_Report.xlsx"), Server.MapPath("~/URF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx"));
                }

                List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();
                Entities_URF_RequestEntry status = new Entities_URF_RequestEntry();

                status.DrFrom = txtFrom.Text.Trim();
                status.DrTo = txtTo.Text.Trim();

                bool supplyChain = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim());


                if (supplyChain)
                {
                    if (ddItemStatus.SelectedItem.Text.ToUpper() == "ALL")
                    {
                        list = BLL.URF_TRANSACTION_AllRequest_Reporting(status);
                    }
                    else
                    {
                        list = BLL.URF_TRANSACTION_AllRequest_Reporting(status).Where(itm => itm.StatAll == ddItemStatus.SelectedItem.Text.ToUpper()).ToList();
                    }

                }
                else
                {
                    if (ddItemStatus.SelectedItem.Text.ToUpper() == "ALL")
                    {
                        list = BLL.URF_TRANSACTION_AllRequest_Reporting(status).Where(itm => itm.RhDivision == Session["Division"].ToString()).ToList();
                    }
                    else
                    {
                        list = BLL.URF_TRANSACTION_AllRequest_Reporting(status).Where(itm => itm.RhDivision == Session["Division"].ToString() && itm.StatAll == ddItemStatus.SelectedItem.Text.ToUpper()).ToList();
                    }
                }

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        int cnt = 4;
                        string path = Server.MapPath("~/URF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx");
                        Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + path);
                        FileStream fsBI = new FileStream(path, FileMode.Open);
                        using (SLDocument draft = new SLDocument(fsBI, "Sheet1"))
                        {

                            foreach (Entities_URF_RequestEntry entity in list)
                            {
                                draft.SetCellValue("A" + cnt.ToString(), entity.RdCtrlNo);
                                draft.SetCellValue("B" + cnt.ToString(), entity.RdPONO);
                                draft.SetCellValue("C" + cnt.ToString(), entity.RdPRNO);
                                draft.SetCellValue("D" + cnt.ToString(), entity.RdItemName);
                                draft.SetCellValue("E" + cnt.ToString(), entity.RdSpecs);
                                draft.SetCellValue("F" + cnt.ToString(), entity.RdQuantity);
                                draft.SetCellValue("G" + cnt.ToString(), entity.RdUnitOfMeasure);
                                draft.SetCellValue("H" + cnt.ToString(), entity.RdDeliveryConfirmationDate);
                                draft.SetCellValue("I" + cnt.ToString(), entity.RdRequestedDeliveryDate);
                                draft.SetCellValue("J" + cnt.ToString(), entity.RdReplyDeliveryDate);
                                draft.SetCellValue("K" + cnt.ToString(), entity.RhCategoryName);
                                draft.SetCellValue("L" + cnt.ToString(), entity.RhTransactionDate);
                                draft.SetCellValue("M" + cnt.ToString(), entity.StatAll);

                                cnt++;
                            }

                            fsBI.Close();
                            draft.SaveAs(path);

                        }

                        Response.Redirect("URF_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx", false);

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
