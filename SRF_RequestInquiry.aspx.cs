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
    public partial class SRF_RequestInquiry : System.Web.UI.Page
    {
        BLL_SRF BLL = new BLL_SRF();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    txtFrom.Text = DateTime.Today.AddDays(-7).ToString("MM/dd/yyyy");
                    txtTo.Text = DateTime.Today.AddDays(7).ToString("MM/dd/yyyy");

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
                List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
                Entities_SRF_RequestEntry status = new Entities_SRF_RequestEntry();

                status.DrFrom = txtFrom.Text.Trim();
                status.DrTo = txtTo.Text.Trim();
                status.Requester = Session["LcRefId"].ToString();
                status.CtrlNo = txtSearch.Text;

                list = null;

                if (txtSearch.Text.Length > 0)
                {
                    if (ddItemStatus.SelectedValue.ToLower() == "pending")
                    {
                        list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like2(status).Where(itm => itm.ReqInchargeStat == "0" || itm.ReqManagerStat == "0" || itm.PurInchargeStat == "0" || itm.PurImpexStat == "0" || itm.ReqInchargeStat == null || itm.ReqManagerStat == null || itm.PurInchargeStat == null || itm.PurImpexStat == null).ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "disapproved")
                    {
                        list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like2(status).Where(itm => itm.ReqInchargeStat == "2" || itm.ReqManagerStat == "2" || itm.PurInchargeStat == "2" || itm.PurImpexStat == "2").ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "approved")
                    {
                        list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like2(status).Where(itm => itm.PurImpexStat == "1").ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "all")
                    {
                        list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like2(status);
                    }
                }
                else
                {
                    if (ddItemStatus.SelectedValue.ToLower() == "pending")
                    {
                        list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange2(status).Where(itm => itm.ReqInchargeStat == "0" || itm.ReqManagerStat == "0" || itm.PurInchargeStat == "0" || itm.PurImpexStat == "0" || itm.ReqInchargeStat == null || itm.ReqManagerStat == null || itm.PurInchargeStat == null || itm.PurImpexStat == null).ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "disapproved")
                    {
                        list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange2(status).Where(itm => itm.ReqInchargeStat == "2" || itm.ReqManagerStat == "2" || itm.PurInchargeStat == "2" || itm.PurImpexStat == "2").ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "approved")
                    {
                        list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange2(status).Where(itm => itm.PurImpexStat == "1").ToList();
                    }
                    if (ddItemStatus.SelectedValue.ToLower() == "all")
                    {
                        list = BLL.SRF_TRANSACTION_RequestStatus_ByDateRange2(status);
                    }

                }

                if (list.Count > 0)
                {
                    gvData.Visible = true;
                    gvData.DataSource = list;
                    gvData.DataBind();

                    //SET GRIDVIEW FOR EXPORT

                    List<Entities_SRF_RequestEntry> listExportedData = new List<Entities_SRF_RequestEntry>();

                    foreach (Entities_SRF_RequestEntry entity in list)
                    {
                        List<Entities_SRF_RequestEntry> listExport = new List<Entities_SRF_RequestEntry>();
                        listExport = BLL.SRF_TRANSACTION_RequestEntry_Details_ByControlNo(entity);

                        if (listExport.Count > 0)
                        {
                            foreach (Entities_SRF_RequestEntry entityDetails in listExport)
                            {                                
                                Entities_SRF_RequestEntry entityData = new Entities_SRF_RequestEntry();
                                entityData.CtrlNo = entityDetails.CtrlNo;
                                entityData.RefPRPO = entityDetails.RefPRPO;
                                entityData.SalesInvoice = entityDetails.SalesInvoice;
                                entityData.BrandMachineName = entityDetails.BrandMachineName;
                                entityData.ItemName = entityDetails.ItemName;
                                entityData.Specification = entityDetails.Specification;
                                entityData.TotalQuantity = entityDetails.TotalQuantity;
                                entityData.UOM_Description = entityDetails.UOM_Description;
                                entityData.SerialNo = entityDetails.SerialNo;
                                entityData.Supplier = entityDetails.Supplier;
                                listExportedData.Add(entityData);
                            }
                        }
                    }

                    if (listExportedData.Count > 0)
                    {
                        gvExport.DataSource = listExportedData;
                        gvExport.DataBind();
                    }
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

                LinkButton lblCtrl = row.FindControl("lblCtrl") as LinkButton;

                if (e.CommandName == "lblCtrl_Command")
                {
                    string URL = "~/SRF_RequestEntry.aspx?SRF_ControlNo_From_Details=" + CryptorEngine.Encrypt(lblCtrl.Text.Trim(), true);

                    URL = Page.ResolveClientUrl(URL);
                    lblCtrl.OnClientClick = "window.open('" + URL + "'); return false;";
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
                    Label lblReqIncharge = (Label)e.Row.FindControl("lblReqIncharge");
                    Label lblReqInchargeStat = (Label)e.Row.FindControl("lblReqInchargeStat");
                    Label lblReqManager = (Label)e.Row.FindControl("lblReqManager");
                    Label lblReqManagerStat = (Label)e.Row.FindControl("lblReqManagerStat");
                    Label lblPurIncharge = (Label)e.Row.FindControl("lblPurIncharge");
                    Label lblPurInchargeStat = (Label)e.Row.FindControl("lblPurInchargeStat");
                    Label lblImpex = (Label)e.Row.FindControl("lblImpex");
                    Label lblImpexStat = (Label)e.Row.FindControl("lblImpexStat");
                    Label lblPurDeptManager = (Label)e.Row.FindControl("lblPurDeptManager");
                    Label lblPurDeptManagerStat = (Label)e.Row.FindControl("lblPurDeptManagerStat");


                    //-------------- REQ INCHARGE ----------------------------------
                    if (lblReqInchargeStat.Text == "0")
                    {
                        lblReqIncharge.Text = "PENDING";
                        lblReqIncharge.Style.Add("background-color", "#f44336");
                    }
                    if (lblReqInchargeStat.Text == "1")
                    {
                        lblReqIncharge.Text = "APPROVED";
                        lblReqIncharge.Style.Add("background-color", "#00C851");
                    }
                    if (lblReqInchargeStat.Text == "2")
                    {
                        lblReqIncharge.Text = "DISAPPROVED";
                        lblReqIncharge.Style.Add("background-color", "#ffbb33");
                    }
                    //---------------------------------------------------------


                    //-------------- REQ MANAGER -------------------------------
                    if (lblReqManagerStat.Text == "0")
                    {
                        lblReqManager.Text = "PENDING";
                        lblReqManager.Style.Add("background-color", "#f44336");
                    }
                    if (lblReqManagerStat.Text == "1")
                    {
                        lblReqManager.Text = "APPROVED";
                        lblReqManager.Style.Add("background-color", "#00C851");
                    }
                    if (lblReqManagerStat.Text == "2")
                    {
                        lblReqManager.Text = "DISAPPROVED";
                        lblReqManager.Style.Add("background-color", "#ffbb33");
                    }
                    //---------------------------------------------------------


                    //-------------- PUR INCHARGE ---------------------------------
                    if (lblPurInchargeStat.Text == "0")
                    {
                        lblPurIncharge.Text = "PENDING";
                        lblPurIncharge.Style.Add("background-color", "#f44336");
                    }
                    if (lblPurInchargeStat.Text == "1")
                    {
                        lblPurIncharge.Text = "APPROVED";
                        lblPurIncharge.Style.Add("background-color", "#00C851");
                    }
                    if (lblPurInchargeStat.Text == "2")
                    {
                        lblPurIncharge.Text = "DISAPPROVED";
                        lblPurIncharge.Style.Add("background-color", "#ffbb33");
                    }
                    //---------------------------------------------------------

                    //-------------- PUR DEPT. MANAGER ---------------------------------
                    if (lblPurDeptManagerStat.Text == "0")
                    {
                        lblPurDeptManager.Text = "PENDING";
                        lblPurDeptManager.Style.Add("background-color", "#f44336");
                    }
                    if (lblPurDeptManagerStat.Text == "1")
                    {
                        lblPurDeptManager.Text = "APPROVED";
                        lblPurDeptManager.Style.Add("background-color", "#00C851");
                    }
                    if (lblPurDeptManagerStat.Text == "2")
                    {
                        lblPurDeptManager.Text = "DISAPPROVED";
                        lblPurDeptManager.Style.Add("background-color", "#ffbb33");
                    }
                    //---------------------------------------------------------

                    //-------------- IMPEX ------------------------------------
                    if (lblImpexStat.Text == "0")
                    {
                        lblImpex.Text = "PENDING";
                        lblImpex.Style.Add("background-color", "#f44336");
                    }
                    if (lblImpexStat.Text == "1")
                    {
                        lblImpex.Text = "APPROVED";
                        lblImpex.Style.Add("background-color", "#00C851");
                    }
                    if (lblImpexStat.Text == "2")
                    {
                        lblImpex.Text = "DISAPPROVED";
                        lblImpex.Style.Add("background-color", "#ffbb33");
                    }
                    if (lblImpexStat.Text == "3")
                    {
                        lblImpex.Text = "CANCELED";
                        lblImpex.Style.Add("background-color", "blue");
                    }
                    //---------------------------------------------------------


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
                "attachment;filename=SRF_Report.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //gvExport.AllowPaging = false;
                //gvExport.DataBind();

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
