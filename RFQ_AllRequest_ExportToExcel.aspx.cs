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
    public partial class RFQ_AllRequest_ExportToExcel : System.Web.UI.Page
    {

        BLL_RFQ BLL = new BLL_RFQ();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (!IsPostBack)
                    {

                        txtFrom.Text = DateTime.Today.AddDays(-30).ToString("MM/dd/yyyy");
                        txtTo.Text = DateTime.Today.ToString("MM/dd/yyyy");

                        //---------------------------------------------------------------------------------------------------

                        List<Entities_RFQ_RequestEntry> listCategory = new List<Entities_RFQ_RequestEntry>();
                        listCategory = BLL.RFQ_TRANSACTION_RequestEntry_Fill_All_DropdownList();

                        if (listCategory != null)
                        {
                            if (listCategory.Count > 0)
                            {
                                ddCategory.Items.Clear();
                                ddCategory.Items.Add("ALL CATEGORY");

                                foreach (Entities_RFQ_RequestEntry category in listCategory)
                                {
                                    ListItem item = new ListItem();
                                    item.Text = category.DropdownName.ToUpper();
                                    item.Value = category.DropdownRefId;

                                    if (category.IsDisabled == string.Empty || category.IsDisabled == "0")
                                    {
                                        if (category.TableName == "MT_Category")
                                        {
                                            ddCategory.Items.Add(item);
                                        }

                                    }

                                }

                            }
                        }

                        //---------------------------------------------------------------------------------------------------
                        

                        //---------------------------------------------------------------------------------------------------

                        //if (Session["ExportCategory"] == null)
                        //{
                        //    ddType.Items.FindByText("FOR SENDING").Selected = true;

                        //    if (Session["CategoryAccess"] != null)
                        //    {
                        //        if (int.Parse(Session["CategoryAccess"].ToString()) > 0)
                        //        {
                        //            ddCategory.Enabled = false;
                        //            ddCategory.Items.FindByValue(Session["CategoryAccess"].ToString()).Selected = true;
                        //        }
                        //        else
                        //        {
                        //            ddCategory.Enabled = true;
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    ddType.Items.FindByText(Session["ExportType"].ToString()).Selected = true;
                        //    ddCategory.Items.FindByText(Session["ExportCategory"].ToString()).Selected = true;
                        //}

                        if (!String.IsNullOrEmpty(Request.QueryString["trans"].ToString()))
                        {
                            string transType = Request.QueryString["trans"].ToString();

                            if (transType == "sending")
                            {
                                ddType.Items.FindByText("FOR SENDING").Selected = true;
                                ddType.Enabled = false;
                            }
                            if (transType == "resend")
                            {
                                ddType.Items.FindByText("FOR RESEND").Selected = true;
                                ddType.Enabled = false;
                            }
                            if (transType == "approved")
                            {
                                ddType.Items.FindByText("ALL APPROVED").Selected = true;
                                ddType.Enabled = false;
                            }
                        }


                        string username = Session["Username"].ToString();

                        if (int.Parse(Session["CategoryAccess"].ToString()) > 0)
                        {
                            ddCategory.Enabled = false;
                            ddCategory.Items.FindByValue(Session["CategoryAccess"].ToString()).Selected = true;
                        }
                        else
                        {

                            if (username == "6985" || username == "3844" || username == "1152" || username == "1402" || username == "002")
                            {
                                ddCategory.Enabled = true;
                            }
                            else
                            {
                                ddCategory.Enabled = false;
                            }

                        }

                        //---------------------------------------------------------------------------------------------------

                        //btnSubmit_Click(sender, e);

                        //if (Session["ExportCategory"] != null)
                        //{
                        //    gvData.Visible = false;
                        //    divCriteria.Visible = false;
                        //    divTitle.Visible = false;

                        //    Response.Clear();
                        //    Response.Buffer = true;

                        //    Response.AddHeader("content-disposition",
                        //    "attachment;filename=ExportedRFQ.xls");
                        //    Response.Charset = "";
                        //    Response.ContentType = "application/vnd.ms-excel";
                        //    StringWriter sw = new StringWriter();
                        //    HtmlTextWriter hw = new HtmlTextWriter(sw);

                        //    gvExport.AllowPaging = false;
                        //    gvExport.DataBind();

                        //    gvExport.RenderControl(hw);
                        //    Response.Output.Write(sw.ToString());
                        //    Response.Flush();
                        //    Response.End();

                        //    Session["ExportCategory"] = null;
                        //    Session["ExportType"] = null;

                        //    divCriteria.Visible = true;
                        //    divTitle.Visible = true;
                        //}
                        //else
                        //{
                        //    ddType.Items.FindByText("FOR SENDING").Selected = true;
                        //}

                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                }
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvData.Rows.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Cannot export empty record');", true);
            }
            else
            {
                Session["ExportCategory"] = ddCategory.SelectedItem.Text;
                Session["ExportType"] = ddType.SelectedItem.Text;

                Response.Redirect("RFQ_AllRequest_ExportToExcel.aspx");
                
            }
        }
        

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                //string username = Session["Username"].ToString();

                //if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()) ||
                //    username == "6985" || username == "3844" || username == "1152" || username == "1402" || username == "002")
                //{
                //    Session["RFQ_Export_From"] = txtFrom.Text.Trim();
                //    Session["RFQ_Export_To"] = txtTo.Text.Trim();
                //    Session["RFQ_Export_Category"] = ddCategory.SelectedItem.Text.ToLower();
                //    Session["RFQ_Export_Type"] = ddType.SelectedItem.Text.ToLower();
                //    Response.Redirect("RFQ_ExportToExcel.aspx");
                //}

                List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();
                entity.DrFrom = txtFrom.Text.Trim();
                entity.DrTo = txtTo.Text.Trim();

                list = null;

                string username = Session["Username"].ToString();

                //if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()) ||
                //    username == "6985" || username == "3844" || username == "1152" || username == "1402" || username == "002")
                //{
                //    list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel(entity);
                //}
                //else
                //{
                //    list = BLL.RFQ_TRANSACTION_AllRequest_ByDateRange_AllApproved(entity);
                //}

                if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()) ||
                    username == "6985" || username == "3844" || username == "1152" || username == "1402" || username == "002")
                {

                    if (ddCategory.SelectedItem.Text.ToLower() == "all category")
                    {
                        if (ddType.SelectedItem.Text.ToLower() == "for sending")
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel(entity).Where(itm => int.Parse(itm.CntSuppResp) <= 0).ToList();
                        }
                        if (ddType.SelectedItem.Text.ToLower() == "for resend")
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel_Resend(entity);
                        }
                        if (ddType.SelectedItem.Text.ToLower() == "all approved")
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel_Approved(entity);
                        }
                        if (ddType.SelectedItem.Text.ToLower() == "all")
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel(entity);
                        }

                    }
                    else
                    {
                        if (ddType.SelectedItem.Text.ToLower() == "for sending")
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel(entity).Where(itm => itm.RhCategory == ddCategory.SelectedItem.Text && int.Parse(itm.CntSuppResp) <= 0).ToList();
                        }
                        if (ddType.SelectedItem.Text.ToLower() == "for resend")
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel_Resend(entity).Where(itm => itm.RhCategory == ddCategory.SelectedItem.Text).ToList();
                        }
                        if (ddType.SelectedItem.Text.ToLower() == "all approved")
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel_Approved(entity).Where(itm => itm.RhCategory == ddCategory.SelectedItem.Text).ToList();
                        }
                        if (ddType.SelectedItem.Text.ToLower() == "all")
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel(entity).Where(itm => itm.RhCategory == ddCategory.SelectedItem.Text).ToList();
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

                        gvExport.DataSource = list;


                        // EXPORT TO EXCEL

                        gvData.Visible = false;
                        divCriteria.Visible = false;
                        divTitle.Visible = false;

                        Response.Clear();
                        Response.Buffer = true;

                        Response.AddHeader("content-disposition",
                        "attachment;filename=ExportedRFQ.xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        StringWriter sw = new StringWriter();
                        HtmlTextWriter hw = new HtmlTextWriter(sw);

                        gvExport.AllowPaging = false;
                        gvExport.DataBind();

                        gvExport.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();

                        //Session["ExportCategory"] = null;
                        //Session["ExportType"] = null;

                        divCriteria.Visible = true;
                        divTitle.Visible = true;

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

        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label lblStatAll = (Label)e.Row.FindControl("lblStatAll");
                    Label lblStatColor = (Label)e.Row.FindControl("lblStatColor");

                    if (lblStatAll.Text == "FOR SENDING")
                    {
                        lblStatAll.Style.Add("background-color", "#9C27B0");
                    }
                    if (lblStatAll.Text == "FOR RESEND")
                    {
                        lblStatAll.Style.Add("background-color", "#009688");
                    }
                    if (lblStatAll.Text == "APPROVED")
                    {
                        lblStatAll.Style.Add("background-color", "#00C851");
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



    }
}
