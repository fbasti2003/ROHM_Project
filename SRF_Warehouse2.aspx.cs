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
    public partial class SRF_Warehouse2 : System.Web.UI.Page
    {
        BLL_SRF BLL = new BLL_SRF();
        Common COMMON = new Common();
        BLL_Common BLL_COMMON = new BLL_Common();


        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Form.Enctype = "multipart/form-data";

            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["type"] != null && Request.QueryString["pastMonth"] != null && Request.QueryString["pezaNonPeza"] != null)
                    {
                        //PEZA
                        //Approved/Waiting for delivery (Type=1)
                        //Delivered w/ pending items (Type=2)
                        //DELIVERED (Type=3)

                        //Past 3Months & More
                        //PEZA
                        //DELIVERED

                        if (Request.QueryString["pastMonth"].ToString() == "3")
                        {
                            //PAST 3 MONTHS & MORE
                            //txtFrom2.Text = DateTime.Today.AddDays(-365).ToString("MM/dd/yyyy");
                            txtFrom2.Text = "07/01/2023";
                            txtTo2.Text = DateTime.Today.AddDays(-120).ToString("MM/dd/yyyy");

                            //txtFrom.Text = DateTime.Today.AddDays(-365).ToString("MM/dd/yyyy");
                            txtFrom.Text = "07/01/2023";
                            txtTo.Text = DateTime.Today.AddDays(-90).ToString("MM/dd/yyyy");
                        }
                        else if (Request.QueryString["pastMonth"].ToString() == "2")
                        {
                            //PAST 2 MONTHS
                            txtFrom2.Text = DateTime.Today.AddDays(-120).ToString("MM/dd/yyyy");
                            txtTo2.Text = DateTime.Today.AddDays(-60).ToString("MM/dd/yyyy");

                            txtFrom.Text = DateTime.Today.AddDays(-120).ToString("MM/dd/yyyy");
                            txtTo.Text = DateTime.Today.AddDays(-60).ToString("MM/dd/yyyy");
                        }
                        else if (Request.QueryString["pastMonth"].ToString() == "1")
                        {
                            //PAST 1 MONTH
                            txtFrom2.Text = DateTime.Today.AddDays(-60).ToString("MM/dd/yyyy");
                            txtTo2.Text = DateTime.Today.AddDays(-30).ToString("MM/dd/yyyy");

                            txtFrom.Text = DateTime.Today.AddDays(-60).ToString("MM/dd/yyyy");
                            txtTo.Text = DateTime.Today.AddDays(-30).ToString("MM/dd/yyyy");
                        }
                        else if (Request.QueryString["pastMonth"].ToString() == "44" || Request.QueryString["pastMonth"].ToString() == "55")
                        {
                            //PAST 1 MONTH
                            //txtFrom2.Text = DateTime.Today.AddDays(-1825).ToString("MM/dd/yyyy");
                            txtFrom2.Text = "07/01/2023";
                            txtTo2.Text = DateTime.Today.AddDays(-30).ToString("MM/dd/yyyy");

                            //txtFrom.Text = DateTime.Today.AddDays(-1825).ToString("MM/dd/yyyy");
                            txtFrom.Text = "07/01/2023";
                            txtTo.Text = DateTime.Today.AddDays(-30).ToString("MM/dd/yyyy");
                        }
                        else
                        {                            
                            //txtFrom2.Text = DateTime.Today.AddDays(-365).ToString("MM/dd/yyyy");
                            txtFrom2.Text = "07/01/2023";
                            txtTo2.Text = DateTime.Today.AddDays(-30).ToString("MM/dd/yyyy");

                            //txtFrom.Text = DateTime.Today.AddDays(-365).ToString("MM/dd/yyyy");
                            txtFrom.Text = "07/01/2023";
                            txtTo.Text = DateTime.Today.AddDays(-30).ToString("MM/dd/yyyy");
                        }

                        if (Request.QueryString["type"].ToString() == "1")
                        {
                            ddStatus.SelectedValue = "APPROVED / WAITING FOR DELIVERY";
                            ddReportStatus.SelectedValue = "APPROVED / WAITING FOR DELIVERY";
                        }
                        if (Request.QueryString["type"].ToString() == "2")
                        {
                            ddStatus.SelectedValue = "DELIVERED WITH PENDING ITEMS";
                            ddReportStatus.SelectedValue = "DELIVERED WITH PENDING ITEMS";
                        }
                        if (Request.QueryString["type"].ToString() == "3")
                        {
                            ddStatus.SelectedValue = "DELIVERED";
                            ddReportStatus.SelectedValue = "DELIVERED";
                        }
                        if (Request.QueryString["type"].ToString() == "4" || Request.QueryString["type"].ToString() == "5")
                        {
                            ddStatus.SelectedValue = "";
                            ddReportStatus.SelectedValue = "ALL";
                        }

                        ddReportType.SelectedValue = "ALL ITEMS";

                        if (Request.QueryString["pezaNonPeza"].ToString() == "1")
                        {
                            ddPezaNonPezaReport.SelectedValue = "PEZA";
                        }
                        if (Request.QueryString["pezaNonPeza"].ToString() == "2")
                        {
                            ddPezaNonPezaReport.SelectedValue = "NON PEZA";
                        }


                        

                    }
                    else
                    {
                        txtFrom2.Text = DateTime.Today.AddMonths(-12).ToString("MM/dd/yyyy");
                        txtTo2.Text = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");

                        txtFrom.Text = DateTime.Today.AddMonths(-12).ToString("MM/dd/yyyy");
                        txtTo.Text = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");

                    }
                    // call submit button to load initial record
                    btnSubmit_Click(sender, e);
                    
                    //-----------------------------------------------------------------------------------------------------------------------------------------
                    if (Session["AddToCurrentDelivery"] != null)
                    {
                        if (!string.IsNullOrEmpty(Session["AddToCurrentDelivery"].ToString()))
                        {

                            lblCtrlNo2.Text = Session["AddToCurrentDelivery"].ToString();
                            buyerCategory.Value = string.Empty;

                            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
                            list = BLL.SRF_TRANSACTION_RequestEntry_ByControlNo(Session["AddToCurrentDelivery"].ToString().Trim());

                            if (list.Count > 0)
                            {
                                foreach (Entities_SRF_RequestEntry entity in list)
                                {
                                    txtCategory.Text = entity.CategoryDescription;
                                    txtTotalQuantity.Text = entity.TotalQuantity;
                                    txtServiceDate.Text = entity.PullOutServiceDate;
                                    txtDeliveryDateToRepi.Text = entity.DeliveryDateToRepi;
                                    txtProblemEncountered.Text = entity.ProblemEncountered;
                                    txtPurposeOfPullOut.Text = entity.POPDescription;
                                    txtTotalValueInUsd.Text = entity.TotalValueInUsd;
                                    txtLoaNumber.Text = entity.LOADescription;
                                    txtSuretyBond.Text = entity.LoaSuretyBond;
                                    txtLoa8106.Text = entity.Loa8106;
                                    txtPickUpPoint.Text = entity.PickUpPoint;

                                    buyerCategory.Value = entity.Category;

                                    BLL_Common BLL_Common = new BLL_Common();

                                    List<Entities_Common_MTSupplier> supplier = new List<Entities_Common_MTSupplier>();
                                    supplier = BLL_Common.Common_getSupplier_ByRefId(entity.Supplier);

                                    if (supplier.Count > 0)
                                    {
                                        foreach (Entities_Common_MTSupplier sup in supplier)
                                        {
                                            lblSupplier.Text = "<U>" + sup.Name + "</U>" + " " + sup.Address;
                                        }
                                    }

                                    List<Entities_Common_SystemUsers> requester = new List<Entities_Common_SystemUsers>();
                                    requester = BLL_Common.getLoginCredentialsByRefId(entity.Requester);

                                    if (requester.Count > 0)
                                    {
                                        foreach (Entities_Common_SystemUsers user in requester)
                                        {
                                            txtRequester.Text = CryptorEngine.Decrypt(user.FullName, true).ToUpper();
                                            txtDivisionName.Text = user.DivisionName.ToUpper();
                                            txtDepartment.Text = user.DepartmentName.ToUpper();
                                        }
                                    }

                                    
                                    // SET DETAILS
                                    List<Entities_SRF_RequestEntry> details = new List<Entities_SRF_RequestEntry>();
                                    details = BLL.SRF_TRANSACTION_RequestEntry_Details_ByControlNo_ForWarehouse(Session["AddToCurrentDelivery"].ToString().Trim());

                                    if (details.Count > 0)
                                    {
                                        gvDetails.DataSource = details;
                                        gvDetails.DataBind();
                                        gvDetails.Visible = true;

                                    }

                                    // SET DISABLED THE CHECKBOX
                                    for (int i = 0; i < gvDetails.Rows.Count; i++)
                                    {
                                        CheckBox cbAITD = (CheckBox)gvDetails.Rows[i].Cells[11].FindControl("cbAITD");
                                        cbAITD.Enabled = false;
                                    }

                                    // SET ACTUAL DELIVERY
                                    List<Entities_SRF_RequestEntry> actualDelivery = new List<Entities_SRF_RequestEntry>();
                                    actualDelivery = BLL.SRF_TRANSACTION_RequestEntry_Details_ByControlNo_ForWarehouse_ActualProgress(Session["AddToCurrentDelivery"].ToString());

                                    if (actualDelivery.Count > 0)
                                    {
                                        gvActualDelivery.DataSource = actualDelivery;
                                        gvActualDelivery.DataBind();
                                        gvActualDelivery.Visible = true;

                                    }


                                    List<Entities_SRF_RequestEntry> approved = new List<Entities_SRF_RequestEntry>();
                                    approved = BLL.SRF_TRANSACTION_RequestStatus_ByControlNo(Session["AddToCurrentDelivery"].ToString());

                                    if (approved.Count > 0)
                                    {

                                        foreach (Entities_SRF_RequestEntry entityApproved in approved)
                                        {
                                            lblRequestor.Text = !string.IsNullOrEmpty(entityApproved.ReqInchargeName) ? CryptorEngine.Decrypt(entityApproved.ReqInchargeName, true) : "PENDING";
                                            lblManager.Text = !string.IsNullOrEmpty(entityApproved.ReqManagerName) ? CryptorEngine.Decrypt(entityApproved.ReqManagerName, true) : "PENDING";
                                            lblIncharge.Text = !string.IsNullOrEmpty(entityApproved.PurInchargeName) ? CryptorEngine.Decrypt(entityApproved.PurInchargeName, true) : "PENDING";
                                            lblImpex.Text = !string.IsNullOrEmpty(entityApproved.PurManagerName) ? CryptorEngine.Decrypt(entityApproved.PurManagerName, true) : "PENDING";

                                            lblSCDDeptManager.Text = !string.IsNullOrEmpty(entityApproved.PurDeptManagerName) ? CryptorEngine.Decrypt(entityApproved.PurDeptManagerName, true) : "PENDING";
                                            lblDOASCDDeptManager.Text = !string.IsNullOrEmpty(entityApproved.PurDeptManagerName) ? entityApproved.PurDeptManagerDOA : "-";

                                            lblDOARequestor.Text = !string.IsNullOrEmpty(entityApproved.ReqInchargeName) ? entityApproved.ReqInchargeDOA : "-";
                                            lblDOAManager.Text = !string.IsNullOrEmpty(entityApproved.ReqManagerName) ? entityApproved.ReqManagerDOA : "-";
                                            lblDOAIncharge.Text = !string.IsNullOrEmpty(entityApproved.PurInchargeName) ? entityApproved.PurInchargeDOA : "-";
                                            lblDOAImpex.Text = !string.IsNullOrEmpty(entityApproved.PurManagerName) ? entityApproved.PurImpexDOA : "-";

                                            // REQUESTOR
                                            if (entityApproved.ReqInchargeStat == "0")
                                            {
                                                lblRequestor.CssClass = "label label-danger";
                                            }
                                            if (entityApproved.ReqInchargeStat == "1")
                                            {
                                                lblRequestor.CssClass = "label label-success";
                                            }
                                            if (entityApproved.ReqInchargeStat == "2")
                                            {
                                                lblRequestor.CssClass = "label label-warning";
                                            }

                                            // MANAGER
                                            if (entityApproved.ReqManagerStat == "0")
                                            {
                                                lblManager.CssClass = "label label-danger";
                                            }
                                            if (entityApproved.ReqManagerStat == "1")
                                            {
                                                lblManager.CssClass = "label label-success";
                                            }
                                            if (entityApproved.ReqManagerStat == "2")
                                            {
                                                lblManager.CssClass = "label label-warning";
                                            }

                                            // INCHARGE
                                            if (entityApproved.PurInchargeStat == "0")
                                            {
                                                lblIncharge.CssClass = "label label-danger";
                                            }
                                            if (entityApproved.PurInchargeStat == "1")
                                            {
                                                lblIncharge.CssClass = "label label-success";
                                            }
                                            if (entityApproved.PurInchargeStat == "2")
                                            {
                                                lblIncharge.CssClass = "label label-warning";
                                            }

                                            // SCD DEPT. MANAGER
                                            if (entityApproved.PurDeptManagerStat == "0")
                                            {
                                                lblSCDDeptManager.CssClass = "label label-danger";
                                            }
                                            if (entityApproved.PurDeptManagerStat == "1")
                                            {
                                                lblSCDDeptManager.CssClass = "label label-success";
                                            }
                                            if (entityApproved.PurDeptManagerStat == "2")
                                            {
                                                lblSCDDeptManager.CssClass = "label label-warning";
                                            }

                                            // IMPEX
                                            if (entityApproved.PurImpexStat == "0")
                                            {
                                                lblImpex.CssClass = "label label-danger";
                                            }
                                            if (entityApproved.PurImpexStat == "1")
                                            {
                                                lblImpex.CssClass = "label label-success";
                                            }
                                            if (entityApproved.PurImpexStat == "2")
                                            {
                                                lblImpex.CssClass = "label label-warning";
                                            }
                                            if (entityApproved.PurImpexStat == "3")
                                            {
                                                lblImpex.Text = lblImpex.Text + " (CANCELED)";
                                                lblImpex.Style.Add("background-color", "blue");
                                            }

                                        }
                                    }



                                    Session["AddToCurrentDelivery"] = null;

                                    btnInProgress.Visible = false;
                                    btnConfirmDelivery.Visible = true;                                    
                                    divAttachment.Visible = true;

                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessageForCurrentDelivery('Current delivery has been updated!');", true);


                                }



                            }


                            

                        }
                    }
                    //-----------------------------------------------------------------------------------------------------------------------------------------

                    

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                }


            }

            
        }

        protected void btnLoad8106_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
                list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => string.IsNullOrEmpty(itm.Warehouse_LOA8106) || COMMON.IsNullOrWhiteSpace(itm.Warehouse_LOA8106)).ToList();

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        gvData.Visible = true;
                        gvData.DataSource = list;
                        gvData.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
                if (!string.IsNullOrEmpty(ddStatus.SelectedItem.Text))
                {
                    if (ddStatus.SelectedItem.Text.ToUpper() == "DELIVERED")
                    {
                        list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper()).ToList();
                    }
                    else
                    {
                        list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => itm.StatAll.ToUpper().Contains(ddStatus.SelectedItem.Text.ToUpper())).ToList();
                    }
                }
                else
                {
                    if (chkIncludeDelivered.Checked)
                    {
                        list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim());
                    }
                    else
                    {
                        list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => itm.StatAll.ToUpper() != "DELIVERED").ToList().Where(itm => itm.StatAll.ToUpper() != "CLOSED").ToList();
                    }
                }

                if (Request.QueryString["pezaNonPeza"] != null)
                {
                    if (Request.QueryString["pezaNonPeza"].ToString() == "1")
                    {
                        //PEZA
                        //list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper() && itm.Warehouse_PezaNonPeza == "1" && !string.IsNullOrEmpty(itm.Warehouse_LOA8106) && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList();
                        list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper() && itm.Warehouse_PezaNonPeza == "1" && !string.IsNullOrEmpty(itm.Warehouse_LOA8106)).ToList();
                        //if (string.IsNullOrEmpty(ddStatus.SelectedItem.Text))
                        //{
                        //    list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => (itm.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" || itm.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" || itm.StatAll.ToUpper() == "DELIVERED") && itm.Warehouse_PezaNonPeza == "1").ToList();
                        //}
                        //else
                        //{
                        //    list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper() && itm.Warehouse_PezaNonPeza == "1").ToList();
                        //}

                    }
                    if (Request.QueryString["pezaNonPeza"].ToString() == "2")
                    {
                        //NON PEZA
                        list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper() && itm.Warehouse_PezaNonPeza == "2" && !string.IsNullOrEmpty(itm.Warehouse_LOA8106) && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList();
                        //list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper() && itm.Warehouse_PezaNonPeza == "2" && !string.IsNullOrEmpty(itm.Warehouse_LOA8106)).ToList();
                        //if (string.IsNullOrEmpty(ddStatus.SelectedItem.Text))
                        //{
                        //    list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => (itm.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" || itm.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" || itm.StatAll.ToUpper() == "DELIVERED") && itm.Warehouse_PezaNonPeza == "2").ToList();
                        //}
                        //else
                        //{
                        //    list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper() && itm.Warehouse_PezaNonPeza == "2").ToList();
                        //}

                    }
                    if (Request.QueryString["pezaNonPeza"].ToString() == "0")
                    {

                        //PEZA
                        if (Request.QueryString["totalPezaNonPeza"].ToString() == "1")
                        {
                            list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => (itm.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" || itm.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" || itm.StatAll.ToUpper() == "DELIVERED") && itm.Warehouse_PezaNonPeza == "1" && !string.IsNullOrEmpty(itm.Warehouse_LOA8106)).ToList();
                            //list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => (itm.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" || itm.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" || itm.StatAll.ToUpper() == "DELIVERED") && itm.Warehouse_PezaNonPeza == "1" && !string.IsNullOrEmpty(itm.Warehouse_LOA8106)).ToList();

                        }

                        //NON PEZA
                        if (Request.QueryString["totalPezaNonPeza"].ToString() == "2")
                        {
                            list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => (itm.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" || itm.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" || itm.StatAll.ToUpper() == "DELIVERED") && itm.Warehouse_PezaNonPeza == "2" && !string.IsNullOrEmpty(itm.Warehouse_LOA8106) && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList();
                            //list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => (itm.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" || itm.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" || itm.StatAll.ToUpper() == "DELIVERED") && itm.Warehouse_PezaNonPeza == "2" && !string.IsNullOrEmpty(itm.Warehouse_LOA8106)).ToList();

                        }
                    }                    

                }

                if (Request.QueryString["pastMonth"] != null)
                {
                    //PEZA
                    if (Request.QueryString["pastMonth"].ToString() == "44")
                    {
                        //list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "1").ToList();
                        list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "1").ToList();
                    }

                    //NON PEZA
                    if (Request.QueryString["pastMonth"].ToString() == "55")
                    {
                        //list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "2").ToList();
                        list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList();
                    }
                }



                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        gvData.Visible = true;
                        gvData.DataSource = list;
                        gvData.DataBind();

                        
                        // SET OVERALL TOTAL QUANTITY
                        long totalOverallQty = 0;
                        string old8106 = string.Empty;
                        int totalUnique8106 = 0;


                        foreach (Entities_SRF_RequestEntry entityTotalOverall in list.Where(itm2 => !string.IsNullOrEmpty(itm2.Warehouse_LOA8106)).OrderBy(itm => itm.Warehouse_LOA8106).ToList())
                        {
                            totalOverallQty += long.Parse(entityTotalOverall.Warehouse_OverallTotalQty);

                            if (old8106 != entityTotalOverall.Warehouse_LOA8106)
                            {
                                totalUnique8106++;
                            }

                            old8106 = entityTotalOverall.Warehouse_LOA8106;

                            ctrlnoList.Value += "'" + entityTotalOverall.Warehouse_CtrlNo + "'" + ",";
                        }
                        
                        lblOverallTotalQuantity.Text = totalOverallQty.ToString("#,###");
                        lblTotalUnique8106.Text = totalUnique8106.ToString("#,###");
                        //lblTotalUnique8106.Text = list.Where(itm => !string.IsNullOrEmpty(itm.Old8106)).ToList().Select(itm2 => itm2.Old8106).Distinct().Count().ToString();

                        if (Request.QueryString["type"] != null && Request.QueryString["pastMonth"] != null && Request.QueryString["pezaNonPeza"] != null && Request.QueryString["totalPezaNonPeza"] != null)
                        {

                            if (Request.QueryString["type"].ToString() == "0")
                            {
                                if (Request.QueryString["pezaNonPeza"].ToString() == "0")
                                {
                                    if (Request.QueryString["totalPezaNonPeza"].ToString() == "1")
                                    {
                                        if (Request.QueryString["pastMonth"].ToString() == "1")
                                        {
                                            if (Session["PezaPast1Total"] != null)
                                            {
                                                lblTotalUnique8106.Text = Session["PezaPast1Total"].ToString();
                                            }
                                        }
                                        if (Request.QueryString["pastMonth"].ToString() == "2")
                                        {
                                            if (Session["PezaPast2Total"] != null)
                                            {
                                                lblTotalUnique8106.Text = Session["PezaPast2Total"].ToString();
                                            }
                                        }
                                        if (Request.QueryString["pastMonth"].ToString() == "3")
                                        {
                                            if (Session["PezaPast3Total"] != null)
                                            {
                                                lblTotalUnique8106.Text = Session["PezaPast3Total"].ToString();
                                            }
                                        }
                                    }

                                    if (Request.QueryString["totalPezaNonPeza"].ToString() == "2")
                                    {
                                        if (Request.QueryString["pastMonth"].ToString() == "1")
                                        {
                                            if (Session["NonPezaPast1Total"] != null)
                                            {
                                                lblTotalUnique8106.Text = Session["NonPezaPast1Total"].ToString();
                                            }
                                        }
                                        if (Request.QueryString["pastMonth"].ToString() == "2")
                                        {
                                            if (Session["NonPezaPast2Total"] != null)
                                            {
                                                lblTotalUnique8106.Text = Session["NonPezaPast2Total"].ToString();
                                            }
                                        }
                                        if (Request.QueryString["pastMonth"].ToString() == "3")
                                        {
                                            if (Session["NonPezaPast3Total"] != null)
                                            {
                                                lblTotalUnique8106.Text = Session["NonPezaPast3Total"].ToString();
                                            }
                                        }
                                    }

                                }
                            }

                            //PEZA OVERALL COUNT
                            if (Request.QueryString["type"].ToString() == "4")
                            {
                                if (Request.QueryString["pastMonth"].ToString() == "4")
                                {
                                    if (Request.QueryString["pezaNonPeza"].ToString() == "0")
                                    {
                                        if (Request.QueryString["totalPezaNonPeza"].ToString() == "1")
                                        {
                                            if (Session["TotalPeza"] != null)
                                            {
                                                lblTotalUnique8106.Text = Session["TotalPeza"].ToString();
                                            }
                                            if (Session["TotalPezaQuantity"] != null)
                                            {
                                                long overAllTotalQuantityPeza = long.Parse(Session["TotalPezaQuantity"].ToString());
                                                lblOverallTotalQuantity.Text = overAllTotalQuantityPeza.ToString("#,###");
                                            }
                                        }
                                    }
                                }
                            }

                            if (Request.QueryString["type"].ToString() == "44")
                            {
                                if (Request.QueryString["pastMonth"].ToString() == "44")
                                {
                                    if (Request.QueryString["pezaNonPeza"].ToString() == "0")
                                    {
                                        if (Request.QueryString["totalPezaNonPeza"].ToString() == "1")
                                        {
                                            if (Session["OverallRequestPeza"] != null)
                                            {
                                                lblTotalUnique8106.Text = Session["OverallRequestPeza"].ToString();
                                            }
                                        }
                                    }
                                }
                            }

                            //NON PEZA OVERALL COUNT
                            if (Request.QueryString["type"].ToString() == "5")
                            {
                                if (Request.QueryString["pastMonth"].ToString() == "5")
                                {
                                    if (Request.QueryString["pezaNonPeza"].ToString() == "0")
                                    {
                                        if (Request.QueryString["totalPezaNonPeza"].ToString() == "2")
                                        {
                                            if (Session["TotalNonPeza"] != null)
                                            {
                                                lblTotalUnique8106.Text = Session["TotalNonPeza"].ToString();
                                            }

                                            if (Session["TotalNonPezaQuantity"] != null)
                                            {
                                                long overAllTotalQuantityNonPeza = long.Parse(Session["TotalNonPezaQuantity"].ToString());
                                                lblOverallTotalQuantity.Text = overAllTotalQuantityNonPeza.ToString("#,###");
                                            }
                                        }
                                    }
                                }
                            }

                            if (Request.QueryString["type"].ToString() == "55")
                            {
                                if (Request.QueryString["pastMonth"].ToString() == "55")
                                {
                                    if (Request.QueryString["pezaNonPeza"].ToString() == "0")
                                    {
                                        if (Request.QueryString["totalPezaNonPeza"].ToString() == "2")
                                        {
                                            if (Session["OverallRequestNonPeza"] != null)
                                            {
                                                lblTotalUnique8106.Text = Session["OverallRequestNonPeza"].ToString();
                                            }
                                        }
                                    }
                                }
                            }

                        }

                    }
                    else
                    {
                        lblOverallTotalQuantity.Text = "0";
                        lblTotalUnique8106.Text = "0";

                        gvData.Visible = false;
                        gvData.EmptyDataText = "NO RECORD(S) FOUND!";
                    }
                }

                if (BLL_COMMON.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), "309").Count > 0)
                {
                    ddReportType.SelectedValue = "ALL ITEMS";
                    ddReportType.Enabled = false;

                    ddPezaNonPezaReport.SelectedValue = "PEZA";
                    ddPezaNonPezaReport.Enabled = false;
                }

                if (int.Parse(Session["CategoryAccess"].ToString()) > 0 || ConfigurationManager.AppSettings["PIPL_Temp_MT_Access"].ToString().Contains(Session["Username"].ToString()))
                {
                    ddReportType.Enabled = true;
                    ddPezaNonPezaReport.Enabled = true;
                }




                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        lblTotalUnique8106.Text = list.Count.ToString("#,###");
                    }
                }
                



            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvActualPrevious_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                LinkButton lbAttachment = row.FindControl("lbAttachment") as LinkButton;

                if (e.CommandName == "lbAttachment_Command")
                {

                    string URL = "http://10.27.1.170:9292/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + lbAttachment.Text.Trim();

                    URL = Page.ResolveClientUrl(URL);
                    lbAttachment.OnClientClick = "showDialog(); window.open('" + URL + "'); return false;";

                }



            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }

        }

        protected void gvDetailsQuantityCorrections_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                Button btnCorrection = row.FindControl("btnCorrection") as Button;
                Label lblRefId = row.FindControl("lblRefId") as Label;
                Label txtQuantity = row.FindControl("txtQuantity") as Label;
                TextBox txtQuantityCorrection = row.FindControl("txtQuantityCorrection") as TextBox;
                TextBox txtReasonOfCorrection = row.FindControl("txtReasonOfCorrection") as TextBox;


                if (e.CommandName == "btnCorrection_Command")
                {
                    string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                    string query1 = string.Empty;
                    string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                    string query_Success = string.Empty;


                    query1 += "INSERT INTO SRF_TRANSACTION_UpdatedQuantity (DetailsRefId,OriginalQuantity,UpdatedQuantity,Reason,UpdatedBy,UpdatedDate) " +
                              "VALUES ('" + lblRefId.Text.Trim() + "','" + txtQuantity.Text + "','" + txtQuantityCorrection.Text + "','" + txtReasonOfCorrection.Text + "','" + Session["UserFullName"].ToString() + "',GETDATE()) ";

                    query1 += "UPDATE SRF_TRANSACTION_Request_Details SET TotalQuantity ='" + txtQuantityCorrection.Text.Trim() + "' WHERE RefId ='" + lblRefId.Text.Trim() + "' ";

                    query1 += "INSERT INTO Service_Logs (TransactionLog, TransactionDate) VALUES ('" + lblCtrlNo2.Text + "-" + lblRefId.Text + " quantity has been updated by " + Session["UserFullName"].ToString() + " from " + txtQuantity.Text + " to " + txtQuantityCorrection.Text +  " | Reason: " + txtReasonOfCorrection.Text.Replace("'","''") + "',GETDATE()) ";


                    if (string.IsNullOrEmpty(txtQuantityCorrection.Text) || txtQuantityCorrection.Text.Length <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('NEW QTY. MUST NOT BE BLANK!');", true);
                    }
                    else if (!COMMON.isNumeric(txtQuantityCorrection.Text.Trim(), System.Globalization.NumberStyles.Integer))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('PLEASE ENTER A VALID NEW QTY.!');", true);
                    }
                    else if (string.IsNullOrEmpty(txtReasonOfCorrection.Text) || txtReasonOfCorrection.Text.Length <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('REASON OF CORRECTION MUST NOT BE BLANK!');", true);
                    }
                    else
                    {

                        query_Success = BLL.SRF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                        if (int.Parse(query_Success) > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('NEW QUANTITY HAS BEEN SUCCESSFULLY UPDATED!');", true);

                        }

                    }


                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            } 

        }

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                LinkButton lbAddFromCurrentDelivery = row.FindControl("lbAddFromCurrentDelivery") as LinkButton;
                Label lblRefId = row.FindControl("lblRefId") as Label;
                Label txtItemName = row.FindControl("txtItemName") as Label;


                if (e.CommandName == "lbAddFromCurrentDelivery_Command")
                {
                    string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                    string query1 = string.Empty;
                    string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                    string query_Success = string.Empty;
                    string emailSuccess = string.Empty;
                    int hasCheck = 0;


                    query1 += "INSERT INTO SRF_TRANSACTION_Warehouse_InProgress (CTRLNo,ItemName,AddedBy,AddedDate,InProgress,RDRefId,Remarks) " +
                              "VALUES ('" + lblCtrlNo2.Text.Trim() + "','" + txtItemName.Text + "','" + Session["UserFullName"].ToString() + "',GETDATE(),'1', '" + lblRefId.Text.Trim() + "', '" + Session["UserFullName"].ToString() + " added this to current delivery [" + DateTime.Now.ToShortDateString() + "]') ";


                    query_Success = BLL.SRF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                    if (int.Parse(query_Success) > 0)
                    {
                        Session["AddToCurrentDelivery"] = lblCtrlNo2.Text.Trim();
                        Response.Redirect("SRF_Warehouse2.aspx", false);
                        
                    }


                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            } 
        }

        protected void gvActualDelivery_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                Label lblInProgressRefId = row.FindControl("lblInProgressRefId") as Label;
                Button btnRemoveFromCurrentDelivery = row.FindControl("btnRemoveFromCurrentDelivery") as Button;


                if (e.CommandName == "btnRemoveFromCurrentDelivery_Command")
                {
                    string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                    string query1 = string.Empty;
                    string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                    string query_Success = string.Empty;
                    string emailSuccess = string.Empty;
                    int hasCheck = 0;


                    query1 += "DELETE FROM SRF_TRANSACTION_Warehouse_InProgress WHERE RefId = '" + lblInProgressRefId.Text.Trim() + "' ";


                    query_Success = BLL.SRF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                    if (int.Parse(query_Success) > 0)
                    {
                        Session["AddToCurrentDelivery"] = lblCtrlNo2.Text.Trim();
                        Response.Redirect("SRF_Warehouse2.aspx", false);

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
                LinkButton lbPendingDocuments = row.FindControl("lbPendingDocuments") as LinkButton;
                LinkButton lbQuantityCorrections = row.FindControl("lbQuantityCorrections") as LinkButton;

                Label lblCTRLNo = row.FindControl("lblCTRLNo") as Label;
                Label lblStatus = row.FindControl("lblStatus") as Label;

                //if (e.CommandName == "lblCtrl_Command")
                //{
                //    string URL = "~/SRF_RequestEntry.aspx?SRF_ControlNo_From_Details=" + CryptorEngine.Encrypt(lblCtrl.Text.Trim(), true);

                //    URL = Page.ResolveClientUrl(URL);
                //    lblCtrl.OnClientClick = "window.open('" + URL + "'); return false;";
                //}

                if (e.CommandName == "lbPrint_Command" || e.CommandName == "lbQuantityCorrections_Command")
                {
                    //string URL = "~/SRF_RequestPrint.aspx?SRF_ControlNo_From_Details=" + CryptorEngine.Encrypt(lblCtrl.Text.Trim(), true);

                    //URL = Page.ResolveClientUrl(URL);
                    //lbPrint.OnClientClick = "window.open('" + URL + "'); return false;";

                    lblCtrlNo2.Text = lblCTRLNo.Text.Trim();
                    buyerCategory.Value = string.Empty;

                    List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
                    list = BLL.SRF_TRANSACTION_RequestEntry_ByControlNo(lblCTRLNo.Text.Trim());

                    if (list.Count > 0)
                    {
                        foreach (Entities_SRF_RequestEntry entity in list)
                        {
                            txtCategory.Text = entity.CategoryDescription;
                            txtTotalQuantity.Text = entity.TotalQuantity;
                            txtServiceDate.Text = entity.PullOutServiceDate;
                            txtDeliveryDateToRepi.Text = entity.DeliveryDateToRepi;
                            txtProblemEncountered.Text = entity.ProblemEncountered;
                            txtPurposeOfPullOut.Text = entity.POPDescription;
                            txtTotalValueInUsd.Text = entity.TotalValueInUsd;
                            txtLoaNumber.Text = entity.LOADescription;
                            txtSuretyBond.Text = entity.LoaSuretyBond;
                            txtLoa8106.Text = entity.Loa8106;
                            //txtRemarks.Text = entity.Remarks;
                            //txtGatePassNo.Text = entity.GatePassNo;
                            txtPickUpPoint.Text = entity.PickUpPoint;

                            buyerCategory.Value = entity.Category;

                            BLL_Common BLL_Common = new BLL_Common();

                            List<Entities_Common_MTSupplier> supplier = new List<Entities_Common_MTSupplier>();
                            supplier = BLL_Common.Common_getSupplier_ByRefId(entity.Supplier);

                            if (supplier.Count > 0)
                            {
                                foreach (Entities_Common_MTSupplier sup in supplier)
                                {
                                    lblSupplier.Text = "<U>" + sup.Name + "</U>" + " " + sup.Address;
                                }
                            }

                            List<Entities_Common_SystemUsers> requester = new List<Entities_Common_SystemUsers>();
                            requester = BLL_Common.getLoginCredentialsByRefId(entity.Requester);

                            if (requester.Count > 0)
                            {
                                foreach (Entities_Common_SystemUsers user in requester)
                                {
                                    txtRequester.Text = CryptorEngine.Decrypt(user.FullName, true).ToUpper();
                                    txtDivisionName.Text = user.DivisionName.ToUpper();
                                    txtDepartment.Text = user.DepartmentName.ToUpper();
                                }
                            }

                            // SET DETAILS
                            List<Entities_SRF_RequestEntry> details = new List<Entities_SRF_RequestEntry>();
                            details = BLL.SRF_TRANSACTION_RequestEntry_Details_ByControlNo_ForWarehouse(lblCTRLNo.Text.Trim());

                            if (details.Count > 0)
                            {
                                gvDetails.DataSource = details;
                                gvDetails.DataBind();
                                gvDetails.Visible = true;

                                gvDetailsQuantityCorrections.DataSource = details;
                                gvDetailsQuantityCorrections.DataBind();

                            }

                            // ATTACHMENT
                            List<Entities_SRF_RequestEntry> eAttachment = new List<Entities_SRF_RequestEntry>();
                            eAttachment = BLL.SRF_TRANSACTION_Warehouse_GetAttachment(lblCTRLNo.Text.Trim());

                            if (eAttachment != null)
                            {
                                if (eAttachment.Count > 0)
                                {
                                    foreach (Entities_SRF_RequestEntry entityAtt in eAttachment)
                                    {


                                    }
                                }
                            }

                        }



                    }




                    List<Entities_SRF_RequestEntry> approved = new List<Entities_SRF_RequestEntry>();
                    approved = BLL.SRF_TRANSACTION_RequestStatus_ByControlNo(lblCTRLNo.Text.Trim());

                    if (approved.Count > 0)
                    {

                        foreach (Entities_SRF_RequestEntry entity in approved)
                        {
                            lblRequestor.Text = !string.IsNullOrEmpty(entity.ReqInchargeName) ? CryptorEngine.Decrypt(entity.ReqInchargeName, true) : "PENDING";
                            lblManager.Text = !string.IsNullOrEmpty(entity.ReqManagerName) ? CryptorEngine.Decrypt(entity.ReqManagerName, true) : "PENDING";
                            lblIncharge.Text = !string.IsNullOrEmpty(entity.PurInchargeName) ? CryptorEngine.Decrypt(entity.PurInchargeName, true) : "PENDING";
                            lblImpex.Text = !string.IsNullOrEmpty(entity.PurManagerName) ? CryptorEngine.Decrypt(entity.PurManagerName, true) : "PENDING";

                            lblSCDDeptManager.Text = !string.IsNullOrEmpty(entity.PurDeptManagerName) ? CryptorEngine.Decrypt(entity.PurDeptManagerName, true) : "PENDING";
                            lblDOASCDDeptManager.Text = !string.IsNullOrEmpty(entity.PurDeptManagerName) ? entity.PurDeptManagerDOA : "-";

                            lblDOARequestor.Text = !string.IsNullOrEmpty(entity.ReqInchargeName) ? entity.ReqInchargeDOA : "-";
                            lblDOAManager.Text = !string.IsNullOrEmpty(entity.ReqManagerName) ? entity.ReqManagerDOA : "-";
                            lblDOAIncharge.Text = !string.IsNullOrEmpty(entity.PurInchargeName) ? entity.PurInchargeDOA : "-";
                            lblDOAImpex.Text = !string.IsNullOrEmpty(entity.PurManagerName) ? entity.PurImpexDOA : "-";

                            // REQUESTOR
                            if (entity.ReqInchargeStat == "0")
                            {
                                lblRequestor.CssClass = "label label-danger";
                            }
                            if (entity.ReqInchargeStat == "1")
                            {
                                lblRequestor.CssClass = "label label-success";
                            }
                            if (entity.ReqInchargeStat == "2")
                            {
                                lblRequestor.CssClass = "label label-warning";
                            }

                            // MANAGER
                            if (entity.ReqManagerStat == "0")
                            {
                                lblManager.CssClass = "label label-danger";
                            }
                            if (entity.ReqManagerStat == "1")
                            {
                                lblManager.CssClass = "label label-success";
                            }
                            if (entity.ReqManagerStat == "2")
                            {
                                lblManager.CssClass = "label label-warning";
                            }

                            // INCHARGE
                            if (entity.PurInchargeStat == "0")
                            {
                                lblIncharge.CssClass = "label label-danger";
                            }
                            if (entity.PurInchargeStat == "1")
                            {
                                lblIncharge.CssClass = "label label-success";
                            }
                            if (entity.PurInchargeStat == "2")
                            {
                                lblIncharge.CssClass = "label label-warning";
                            }

                            // SCD DEPT. MANAGER
                            if (entity.PurDeptManagerStat == "0")
                            {
                                lblSCDDeptManager.CssClass = "label label-danger";
                            }
                            if (entity.PurDeptManagerStat == "1")
                            {
                                lblSCDDeptManager.CssClass = "label label-success";
                            }
                            if (entity.PurDeptManagerStat == "2")
                            {
                                lblSCDDeptManager.CssClass = "label label-warning";
                            }

                            // IMPEX
                            if (entity.PurImpexStat == "0")
                            {
                                lblImpex.CssClass = "label label-danger";
                            }
                            if (entity.PurImpexStat == "1")
                            {
                                lblImpex.CssClass = "label label-success";
                            }
                            if (entity.PurImpexStat == "2")
                            {
                                lblImpex.CssClass = "label label-warning";
                            }
                            if (entity.PurImpexStat == "3")
                            {
                                lblImpex.Text = lblImpex.Text + " (CANCELED)";
                                lblImpex.Style.Add("background-color", "blue");
                            }

                        }
                    }

                    // SET lbAddFromCurrentDelivery VISIBLE FALSE
                    if (lblStatus.Text == "APPROVED / WAITING FOR DELIVERY")
                    {
                        for (int i = 0; i < gvDetails.Rows.Count; i++)
                        {
                            LinkButton lbAddFromCurrentDelivery = (LinkButton)gvDetails.Rows[i].Cells[12].FindControl("lbAddFromCurrentDelivery");
                            lbAddFromCurrentDelivery.Visible = false;
                        }

                    } 


                    if (lblStatus.Text.Contains("DELIVERY IN-PROGRESS") || lblStatus.Text == "DELIVERED WITH PENDING ITEMS")
                    {
                        btnClose3.Visible = false;
                        btnInProgress.Visible = false;
                        btnConfirmDelivery.Visible = true;
                        divAttachment.Visible = true;
                        //divActualDelivery.Visible = true;
                        

                        // SET DISABLED THE CHECKBOX
                        if (lblStatus.Text.Contains("DELIVERY IN-PROGRESS"))
                        {
                            for (int i = 0; i < gvDetails.Rows.Count; i++)
                            {
                                CheckBox cbAITD = (CheckBox)gvDetails.Rows[i].Cells[11].FindControl("cbAITD");
                                cbAITD.Enabled = false;
                            }

                        }

                        // SET lbAddFromCurrentDelivery VISIBLE FALSE
                        if (lblStatus.Text.Contains("DELIVERED WITH PENDING ITEMS"))
                        {
                            for (int i = 0; i < gvDetails.Rows.Count; i++)
                            {
                                LinkButton lbAddFromCurrentDelivery = (LinkButton)gvDetails.Rows[i].Cells[12].FindControl("lbAddFromCurrentDelivery");
                                lbAddFromCurrentDelivery.Visible = false;
                            }

                        }                        


                        // SET ACTUAL DELIVERY
                        List<Entities_SRF_RequestEntry> actualDelivery = new List<Entities_SRF_RequestEntry>();
                        actualDelivery = BLL.SRF_TRANSACTION_RequestEntry_Details_ByControlNo_ForWarehouse_ActualProgress(lblCTRLNo.Text.Trim());

                        if (actualDelivery.Count > 0)
                        {
                            gvActualDelivery.DataSource = actualDelivery;
                            gvActualDelivery.DataBind();
                            gvActualDelivery.Visible = true;

                        }


                    }
                    else
                    {
                        btnClose3.Visible = true;
                        btnInProgress.Visible = true;
                        btnConfirmDelivery.Visible = false;
                        divAttachment.Visible = false;
                        //divActualDelivery.Visible = false;
                        //divNote.Visible = true;
                    }


                    if (lblStatus.Text == "DELIVERED WITH PENDING ITEMS")
                    {
                        btnClose3.Visible = true;
                        btnInProgress.Visible = true;
                        btnConfirmDelivery.Visible = false;
                        divAttachment.Visible = false;
                        //divActualDelivery.Visible = false;
                        //divNote.Visible = true;


                    }

                    if (lblStatus.Text == "DELIVERED" || lblStatus.Text == "CLOSED")
                    {
                        btnClose3.Visible = false;
                        btnInProgress.Visible = false;
                        btnConfirmDelivery.Visible = false;
                        divAttachment.Visible = true;
                        fu.Visible = false;
                        Reminder1.Visible = false;
                        Reminder2.Visible = false;


                        // SET ACTUAL DELIVERY
                        List<Entities_SRF_RequestEntry> actualDelivery2 = new List<Entities_SRF_RequestEntry>();
                        actualDelivery2 = BLL.SRF_TRANSACTION_RequestEntry_Details_ByControlNo_ForWarehouse(lblCTRLNo.Text.Trim());

                        if (actualDelivery2.Count > 0)
                        {
                            gvActualDelivery.DataSource = actualDelivery2;
                            gvActualDelivery.DataBind();
                            gvActualDelivery.Visible = true;

                        }

                        for (int i = 0; i < gvActualDelivery.Rows.Count; i++)
                        {
                            HtmlTable tblActualEntry = (HtmlTable)gvActualDelivery.Rows[i].Cells[11].FindControl("tblActualEntry");
                            tblActualEntry.Style.Add("display", "none");
                        }

                        //lbAddNewActual.Enabled = false;
                    }

                    hiddenUserId.Value = Session["LcRefId"].ToString();


                    //CHECK IF NEED TO REVERT
                    if (BLL.SRF_TRANSACTION_WAREHOUSE_CheckIfMoreThanOneDelivery(lblCtrlNo2.Text.Trim()) <= 1)
                    {
                        btnRevert.Visible = true;
                    }
                    else
                    {
                        btnRevert.Visible = false;
                    }


                    if (e.CommandName == "lbPrint_Command")
                    {
                        gvDetails.Visible = true;
                        gvDetailsQuantityCorrections.Visible = false;
                        lbPrint.OnClientClick = "showDialog(); return false;";
                        divCorrectionRequired.Style.Add("display", "none");
                    }

                    if (e.CommandName == "lbQuantityCorrections_Command")
                    {
                        divCorrectionRequired.Style.Add("display", "block");
                        gvDetailsQuantityCorrections.Visible = true;

                        gvDetails.Visible = false;
                        btnInProgress.Visible = false;
                        lbQuantityCorrections.OnClientClick = "showDialog(); return false;";
                    }

                    


                }

                if (e.CommandName == "lbPendingDocuments_Command")
                {
                    if (lbPendingDocuments.Text.ToUpper().Trim() == "YES")
                    {
                        lblCTRLNoWithoutDocument.Text = lblCTRLNo.Text.Trim();

                        List<Entities_SRF_RequestEntry> detailsActualQuantity2 = new List<Entities_SRF_RequestEntry>();
                        detailsActualQuantity2 = BLL.SRF_TRANSACTION_GetActualDeliveryByCTRLNoWithoutDocuments(lblCTRLNo.Text.Trim());

                        if (detailsActualQuantity2 != null)
                        {
                            if (detailsActualQuantity2.Count > 0)
                            {
                                gvWithoutDocuments.DataSource = detailsActualQuantity2;
                                gvWithoutDocuments.DataBind();

                                hiddenUserId.Value = Session["LcRefId"].ToString();

                                lbPendingDocuments.OnClientClick = "showDialog2(); return false;";

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


        protected void gvActualDelivery_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblRefId = (Label)e.Row.FindControl("lblRefId");
                    GridView gvActualPrevious = (GridView)e.Row.FindControl("gvActualPrevious");

                    TextBox txtActualQuantity2 = (TextBox)e.Row.FindControl("txtActualQuantity2");
                    DropDownList ddWithDocuments = (DropDownList)e.Row.FindControl("ddWithDocuments");
                    FileUpload fuAttachment = (FileUpload)e.Row.FindControl("fuAttachment");



                    List<Entities_SRF_RequestEntry> detailsActualPrevious = new List<Entities_SRF_RequestEntry>();
                    detailsActualPrevious = BLL.SRF_TRANSACTION_GetActualDeliveryByCTRLNo(lblCtrlNo2.Text.Trim()).Where(itm => itm.Warehouse_DetailsRefId.Trim() == lblRefId.Text.Trim()).ToList();

                    if (detailsActualPrevious != null)
                    {
                        if (detailsActualPrevious.Count > 0)
                        {
                            gvActualPrevious.DataSource = detailsActualPrevious;
                            gvActualPrevious.DataBind();
                        }
                    }


                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }

        }

        protected void gvActualPrevious_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    
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
                    Label lblCTRLNo = (Label)e.Row.FindControl("lblCTRLNo");
                    Label lblLOACount2 = (Label)e.Row.FindControl("lblLOACount2");
                    LinkButton lbPendingDocuments = (LinkButton)e.Row.FindControl("lbPendingDocuments");
                    LinkButton lbQuantityCorrections = (LinkButton)e.Row.FindControl("lbQuantityCorrections");



                    lblStatus.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");

                    if (ConfigurationManager.AppSettings["PIPL_Temp_MT_Access"].ToString().Contains(Session["Username"].ToString()))
                    {
                        lbQuantityCorrections.Visible = true;
                    }

                    if (int.Parse(lblLOACount2.Text.Trim()) > 0)
                    {
                        lbPendingDocuments.Text = "YES";
                        lbPendingDocuments.Style.Add("font-weight", "bold");
                    }
                    else
                    {
                        lbPendingDocuments.Text = string.Empty;
                    }


                    if (lblStatus.Text == "FOR PROD.MNGR. APPROVAL")
                    {
                        lblStatus.Style.Add("background-color", "#f44336");
                    }

                    if (lblStatus.Text == "FOR PUR.INCHARGE APPROVAL")
                    {
                        lblStatus.Style.Add("background-color", "#9C27B0");
                    }

                    if (lblStatus.Text == "FOR PUR.IMPEX PROCESSING")
                    {
                        lblStatus.Style.Add("background-color", "#673AB7");
                    }

                    if (lblStatus.Text == "APPROVED / WAITING FOR DELIVERY")
                    {
                        //lblStatus.Style.Add("height", "30px");
                        lblStatus.Style.Add("font-size", "13px");
                        lblStatus.Style.Add("background-color", "#00C851");
                    }

                    if (lblStatus.Text.Contains("DELIVERY IN-PROGRESS"))
                    {
                        //lblStatus.Style.Add("height", "30px");
                        lblStatus.Style.Add("font-size", "13px");
                        lblStatus.Style.Add("background-color", "#6F6B02");

                    }

                    if (lblStatus.Text == "DELIVERED")
                    {
                        lblStatus.Style.Add("height", "40px");
                        lblStatus.Style.Add("font-size", "25px");
                        lblStatus.Style.Add("background-color", "#3DB7F9");
                    }

                    if (lblStatus.Text == "DELIVERED WITH PENDING ITEMS")
                    {
                        lblStatus.Style.Add("height", "40px");
                        lblStatus.Style.Add("font-size", "13px");
                        lblStatus.Style.Add("background-color", "#5499C7");
                    }

                    if (lblStatus.Text == "DISAPPROVED")
                    {
                        lblStatus.Style.Add("height", "40px");
                        lblStatus.Style.Add("font-size", "25px");
                        lblStatus.Style.Add("background-color", "#ffbb33");
                    }

                    if (lblStatus.Text == "CANCELED")
                    {
                        lblStatus.Style.Add("height", "40px");
                        lblStatus.Style.Add("font-size", "25px");
                        lblStatus.Style.Add("background-color", "blue");
                    }

                    if (lblStatus.Text == "CLOSED")
                    {
                        lblStatus.Style.Add("height", "40px");
                        lblStatus.Style.Add("font-size", "25px");
                        lblStatus.Style.Add("background-color", "#f44336");
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


        protected void gvDetailsQuantityCorrections_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
        }


        protected void gvDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label txtBrandMachine = (Label)e.Row.FindControl("txtBrandMachine");
                Label txtItemName = (Label)e.Row.FindControl("txtItemName");
                Label txtSpecification = (Label)e.Row.FindControl("txtSpecification");
                Label txtSerialNo = (Label)e.Row.FindControl("txtSerialNo");
                CheckBox cbAITD = (CheckBox)e.Row.FindControl("cbAITD");
                Label txtQuantity = (Label)e.Row.FindControl("txtQuantity");
                Label txtActualQty = (Label)e.Row.FindControl("txtActualQty");
                Label lblInProgress = (Label)e.Row.FindControl("lblInProgress");
                LinkButton lbAddFromCurrentDelivery = (LinkButton)e.Row.FindControl("lbAddFromCurrentDelivery");


                if (txtQuantity.Text.Trim() == txtActualQty.Text.Trim())
                {
                    cbAITD.Enabled = false;
                }

                if (string.IsNullOrEmpty(lblInProgress.Text))
                {
                    if (txtQuantity.Text.Trim() == txtActualQty.Text.Trim())
                    {
                        // DO NOTHING
                    }
                    else
                    {
                        lbAddFromCurrentDelivery.Visible = true;
                    }
                }

                txtBrandMachine.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                txtItemName.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                txtSpecification.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                txtSerialNo.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");


            }
        }

        protected void btnInProgress_Click(object sender, EventArgs e)
        {
            try
            {
                string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                string query1 = string.Empty;
                string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                string query_Success = string.Empty;
                string emailSuccess = string.Empty;
                int hasCheck = 0;

                for (int i = 0; i < gvDetails.Rows.Count; i++)
                {
                    Label lblRefId = (Label)gvDetails.Rows[i].Cells[0].FindControl("lblRefId");
                    TextBox txtBuyerNotes = (TextBox)gvDetails.Rows[i].Cells[13].FindControl("txtBuyerNotes");

                    if (!string.IsNullOrEmpty(txtBuyerNotes.Text) || txtBuyerNotes.Text.Length > 0)
                    {
                        query1 += "UPDATE SRF_TRANSACTION_Request_Details SET RemarksFromWarehouse = '" + txtBuyerNotes.Text.Replace("'", "''") + "' WHERE RefId = '" + lblRefId.Text.Trim() + "' ";
                    }
                }

                for (int i = 0; i < gvDetails.Rows.Count; i++)
                {
                    CheckBox cbAITD = (CheckBox)gvDetails.Rows[i].Cells[11].FindControl("cbAITD");
                    Label txtItemName = (Label)gvDetails.Rows[i].Cells[4].FindControl("txtItemName");
                    Label lblRefId = (Label)gvDetails.Rows[i].Cells[0].FindControl("lblRefId");

                    if (cbAITD.Checked)
                    {
                        hasCheck++;
                        query1 += "INSERT INTO SRF_TRANSACTION_Warehouse_InProgress (CTRLNo,ItemName,AddedBy,AddedDate,InProgress,RDRefId) " +
                                  "VALUES ('" + lblCtrlNo2.Text.Trim() + "','" + txtItemName.Text + "','" + Session["UserFullName"].ToString() + "',GETDATE(),'1', '" + lblRefId.Text.Trim() + "') ";
                    }
                }

                if (hasCheck > 0)
                {

                    query1 += "INSERT INTO SRF_TRANSACTION_Warehouse (CTRLNo,AddedBy,AddedDate,TransType,Attachment) " +
                              "VALUES ('" + lblCtrlNo2.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE(), '1', '') ";

                    query_Success = BLL.SRF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                    if (int.Parse(query_Success) > 0)
                    {
                        // SEND EMAIL
                        string verbiage = string.Empty;

                        if (!string.IsNullOrEmpty(txtLoa8106.Text))
                        {
                            verbiage = "Please know that delivery for <b>" + lblCtrlNo2.Text + "</b> with LOA8106 Number <b>" + txtLoa8106.Text.ToUpper().Trim() + "</b> has arrived. <br/>" +
                                       "Kindly coordinate with Warehouse if needed. <br/><br/>" +
                                       "Thank you and have a great day! <br/><br/>" +
                                       "ROHM Electronics Philippines Inc.";
                        }
                        else
                        {
                            verbiage = "Please know that delivery for <b>" + lblCtrlNo2.Text + "</b> has arrived. <br/>" +
                                       "Kindly coordinate with Warehouse if needed. <br/><br/>" +
                                       "Thank you and have a great day! <br/><br/>" +
                                       "ROHM Electronics Philippines Inc.";
                        }

                        //string emailTo = BLL_COMMON.GetBuyerEmailAddressByHandledCategory(buyerCategory.Value);

                        string emailService = COMMON.sendEmailToSuppliersForSRFWarehouse(ConfigurationManager.AppSettings["SRF_IMPEX_EMAIL"], ConfigurationManager.AppSettings["email-username"], "SRF DELIVERY NOTIFICATION",
                                                                                              verbiage, string.Empty, string.Empty, string.Empty);

                        if (emailService.ToLower().Contains("success"))
                        {
                            emailSuccess = "SRF NUMBER : <b>" + lblCtrlNo2.Text.ToUpper() + "</b> HAS BEEN SUCCESSFULLY MOVE TO DELIVERY IN-PROGRESS. IMPEX HAS BEEN NOTIFIED.";
                        }
                        else
                        {
                            emailSuccess = "SRF NUMBER : <b>" + lblCtrlNo2.Text.ToUpper() + "</b> HAS BEEN SUCCESSFULLY MOVE TO DELIVERY IN-PROGRESS. FAILED TO NOTIFY IMPEX DUE TO CONNECTION ISSUE.";
                        }

                        // REDIRECT TO SUCCESS PAGE
                        Session["successMessage"] = emailSuccess;
                        Session["successTransactionName"] = "SRF_WAREHOUSE";
                        Session["successReturnPage"] = "SRF_Warehouse2.aspx";
                        Response.Redirect("SuccessPage.aspx");
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please check AITD or ACTUAL ITEMS TO DELIVER.'); showDialog();", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {

                Response.Redirect("SRF_Warehouse2.aspx");
                //for (int i = 0; i < gvActualDelivery.Rows.Count; i++)
                //{
                //    string refid = gvActualDelivery.Rows[i].Cells[0].Text;

                //}

                //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ManipulateGrid()", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnRevert_Click(object sender, EventArgs e)
        {
            try
            {

                string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                string query1 = string.Empty;
                string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                string query_Success = string.Empty;
                string emailSuccess = string.Empty;
                string emailService = string.Empty;


                query1 += "DELETE FROM SRF_TRANSACTION_Warehouse WHERE CTRLNo ='" + lblCtrlNo2.Text.Trim() + "' ";
                query1 += "DELETE FROM SRF_TRANSACTION_Warehouse_InProgress WHERE CTRLNo ='" + lblCtrlNo2.Text.Trim() + "' ";

                query_Success = BLL.SRF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                if (int.Parse(query_Success) > 0)
                {
                    string verbiage = "FYI. Please know that <b>" + lblCtrlNo2.Text + "</b> with LOA8106 Number <b>" + txtLoa8106.Text.ToUpper().Trim() + "</b> has been <b>reverted</b> to <b>APPROVED / WAITING FOR DELIVERY</b> by <b>" + Session["UserFullName"].ToString().ToUpper() + "</b> <br/>" +
                                       "Thank you and have a great day! <br/><br/>" +
                                       "ROHM Electronics Philippines Inc.";

                    emailService = COMMON.sendEmailToSuppliersForSRFWarehouse(ConfigurationManager.AppSettings["SRF_IMPEX_EMAIL"], ConfigurationManager.AppSettings["email-username"], "SRF REVERTED TO WAITING FOR DELIVERY",
                                   verbiage, string.Empty, string.Empty, string.Empty);
                }

                if (emailService.ToLower().Contains("success"))
                {
                    emailSuccess = "SRF NUMBER : <b>" + lblCtrlNo2.Text.ToUpper() + "</b> HAS BEEN SUCCESSFULLY REVERTED TO APPROVED / WAITING FOR DELIVERY. IMPEX HAS BEEN NOTIFIED.";
                }
                else
                {
                    emailSuccess = "SRF NUMBER : <b>" + lblCtrlNo2.Text.ToUpper() + "</b> HAS BEEN SUCCESSFULLY REVERTED TO APPROVED / WAITING FOR DELIVERY. FAILED TO NOTIFY IMPEX DUE TO CONNECTION ISSUE.";
                }

                // REDIRECT TO SUCCESS PAGE
                Session["successMessage"] = emailSuccess;
                Session["successTransactionName"] = "SRF_WAREHOUSE";
                Session["successReturnPage"] = "SRF_Warehouse2.aspx";
                Response.Redirect("SuccessPage.aspx");


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnConfirmDelivery_Click(object sender, EventArgs e)
        {
            try
            {

                string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                string query1 = string.Empty;
                string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                string query_Success = string.Empty;
                string emailSuccess = string.Empty;


                string error = string.Empty;
                string isDiscrepancy = string.Empty;
                string verbiage = string.Empty;
                string attachedFiles = string.Empty;
                string attachmentFilesForEmail = string.Empty;

                for (int i = 0; i < gvDetails.Rows.Count; i++)
                {
                    Label lblRefId = (Label)gvDetails.Rows[i].Cells[0].FindControl("lblRefId");
                    TextBox txtBuyerNotes = (TextBox)gvDetails.Rows[i].Cells[13].FindControl("txtBuyerNotes");

                    if (!string.IsNullOrEmpty(txtBuyerNotes.Text) || txtBuyerNotes.Text.Length > 0)
                    {
                        query1 += "UPDATE SRF_TRANSACTION_Request_Details SET RemarksFromWarehouse = '" + txtBuyerNotes.Text.Replace("'", "''") + "' WHERE RefId = '" + lblRefId.Text.Trim() + "' ";
                    }
                }


                for (int i = 0; i < gvActualDelivery.Rows.Count; i++)
                {
                    Label lblRefId = (Label)gvActualDelivery.Rows[i].Cells[0].FindControl("lblRefId");
                    TextBox txtActualQuantity2 = (TextBox)gvActualDelivery.Rows[i].Cells[11].FindControl("txtActualQuantity2");
                    DropDownList ddWithDocuments = (DropDownList)gvActualDelivery.Rows[i].Cells[11].FindControl("ddWithDocuments");
                    Label txtRemainingQty = (Label)gvActualDelivery.Rows[i].Cells[10].FindControl("txtRemainingQty");
                    FileUpload fuAttachment = (FileUpload)gvActualDelivery.Rows[i].Cells[6].FindControl("fuAttachment");
                    

                    if (string.IsNullOrEmpty(txtActualQuantity2.Text))
                    {
                        error += "Empty quantity found in " + lblRefId.Text + " | ";
                    }

                    if (int.Parse(txtActualQuantity2.Text) > int.Parse(txtRemainingQty.Text))
                    {
                        txtActualQuantity2.Style.Add("background-color", "Red");
                        error += "Actual quantity is greater than remaining quantity is found in " + lblRefId.Text + " | ";
                    }
                    else
                    {
                        txtActualQuantity2.Style.Add("background-color", "White");
                    }


                    if (ddWithDocuments.SelectedItem.Text.ToUpper() == "YES" && !fuAttachment.HasFile)
                    {
                        error += "Please attach supporting file for " + lblRefId.Text + " | ";
                    }
                            
                }


                query1 += "UPDATE SRF_TRANSACTION_Warehouse_InProgress SET InProgress = '0' WHERE CTRLNo ='" + lblCtrlNo2.Text.Trim() + "' ";

                if (!string.IsNullOrEmpty(error))
                {    

                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + error + "'); showDialog();", true);
                }
                else
                {
                    // JUST INCASE DIRECTORY FOR CTRLNO IS NOT YET CREATED
                    if (!System.IO.Directory.Exists(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim())))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()));
                    }

                    bool isFuHasFile = false;
                    string fileNameApplicationFU = System.IO.Path.GetFileName(fu.FileName);
                    string fileExtensionApplicationFU = System.IO.Path.GetExtension(fileNameApplicationFU);
                    string newFileFU = fileNameApplicationFU;

                    int numberOfAttachment = BLL.SRF_TRANSACTION_Warehouse_ActualDelivery_Attachment_Count_ForWarehouse(lblCtrlNo2.Text.Trim());
                    numberOfAttachment++;


                    //CHECK FIRST iF FU HAS ATTACHMENT
                    if (fu.HasFile)
                    {
                        isFuHasFile = true;

                        if (!System.IO.File.Exists(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + newFileFU)))
                        {
                            fu.SaveAs(System.IO.Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), newFileFU));
                            fu.Dispose();
                            System.IO.File.Copy(System.IO.Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), newFileFU), System.IO.Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), (numberOfAttachment.ToString() + "-Warehouse" + fileExtensionApplicationFU)), true);
                            System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), newFileFU));
                        }

                    }

                    for (int i = 0; i < gvActualDelivery.Rows.Count; i++)
                    {
                        Label lblRefId = (Label)gvActualDelivery.Rows[i].Cells[0].FindControl("lblRefId");
                        TextBox txtActualQuantity2 = (TextBox)gvActualDelivery.Rows[i].Cells[11].FindControl("txtActualQuantity2");
                        DropDownList ddWithDocuments = (DropDownList)gvActualDelivery.Rows[i].Cells[11].FindControl("ddWithDocuments");
                        FileUpload fuAttachment = (FileUpload)gvActualDelivery.Rows[i].Cells[11].FindControl("fuAttachment");

                        string fileNameApplication = System.IO.Path.GetFileName(fuAttachment.FileName);
                        string fileExtensionApplication = System.IO.Path.GetExtension(fileNameApplication);
                        string newFile = fileNameApplication;
                        string attachmentFiles = string.Empty;
                        


                        if (!isFuHasFile)
                        {
                            if (fuAttachment.HasFile)
                            {

                                if (!System.IO.Directory.Exists(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim())))
                                {
                                    System.IO.Directory.CreateDirectory(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()));
                                }
                                if (!System.IO.File.Exists(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + newFile)))
                                {
                                    fuAttachment.SaveAs(System.IO.Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), newFile));
                                    fuAttachment.Dispose();
                                    System.IO.File.Copy(System.IO.Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), newFile), System.IO.Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), (numberOfAttachment.ToString() + "-Warehouse" + fileExtensionApplication)), true);
                                    System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), newFile));
                                }

                                attachmentFiles = numberOfAttachment.ToString() + "-Warehouse" + fileExtensionApplication;
                                numberOfAttachment++;

                                attachmentFilesForEmail += Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + attachmentFiles) + ",";
                            }
                        }
                        else
                        {
                            attachmentFiles = numberOfAttachment.ToString() + "-Warehouse" + fileExtensionApplicationFU;
                            attachmentFilesForEmail += Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + attachmentFiles) + ",";
                        }

                        string withDocs = string.Empty;

                        if (ddWithDocuments.SelectedItem.Text.ToUpper() == "YES")
                        {
                            withDocs = "1";
                        }

                        if (ddWithDocuments.SelectedItem.Text.ToUpper() == "NO")
                        {
                            withDocs = "0";
                        }

                        if (!isFuHasFile)
                        {
                            query1 += "INSERT INTO SRF_TRANSACTION_Warehouse_Actual_Delivery (DetailsRefId,SRFNumber,Quantity,ActualQuantity,AddedBy,AddedDate,WithDocuments,AttachmentWarehouse) ";
                            query1 += "VALUES ('" + lblRefId.Text.Trim() + "','" + lblCtrlNo2.Text.Trim() + "','0','" + txtActualQuantity2.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE(),'" + withDocs + "','" + attachmentFiles + "') ";
                        }
                        else
                        {
                            //SET THE WithDocuments to 1
                            query1 += "INSERT INTO SRF_TRANSACTION_Warehouse_Actual_Delivery (DetailsRefId,SRFNumber,Quantity,ActualQuantity,AddedBy,AddedDate,WithDocuments,AttachmentWarehouse) ";
                            query1 += "VALUES ('" + lblRefId.Text.Trim() + "','" + lblCtrlNo2.Text.Trim() + "','0','" + txtActualQuantity2.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE(),'1','" + attachmentFiles + "') ";
                        }



                    }

                    query_Success = BLL.SRF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                    if (int.Parse(query_Success) > 0)
                    {

                        if (!string.IsNullOrEmpty(isDiscrepancy))
                        {
                            //WITH DISCREPANCY
                            if (!string.IsNullOrEmpty(txtLoa8106.Text))
                            {
                                verbiage = "Please know that delivery for <b>" + lblCtrlNo2.Text + "</b> with LOA8106 Number <b>" + txtLoa8106.Text.ToUpper().Trim() + "</b> has been delivered but with pending items. <br/>" +
                                           "Expect a hard copy of documents from Warehouse and do coordinate with them if needed. <br/>" +
                                           "Check the attached documents. <br/><br/>" +
                                           "Thank you and have a great day! <br/><br/>" +
                                           "ROHM Electronics Philippines Inc.";
                            }
                            else
                            {
                                verbiage = "Please know that delivery for <b>" + lblCtrlNo2.Text + "</b> has been delivered but with pending items. <br/>" +
                                           "Expect a hard copy of documents from Warehouse and do coordinate with them if needed. <br/>" +
                                           "Check the attached documents. <br/><br/>" +
                                           "Thank you and have a great day! <br/><br/>" +
                                           "ROHM Electronics Philippines Inc.";
                            }


                        }
                        else
                        {
                            //WITHOUT DISCREPANCY
                            if (!string.IsNullOrEmpty(txtLoa8106.Text))
                            {
                                verbiage = "Please know that delivery for <b>" + lblCtrlNo2.Text + "</b> with LOA8106 Number <b>" + txtLoa8106.Text.ToUpper().Trim() + "</b> has been successfully delivered. <br/>" +
                                           "Expect a hard copy of documents from Warehouse and do coordinate with them if needed. <br/>" +
                                           "Check the attached documents. <br/><br/>" +
                                           "Thank you and have a great day! <br/><br/>" +
                                           "ROHM Electronics Philippines Inc.";
                            }
                            else
                            {
                                verbiage = "Please know that delivery for <b>" + lblCtrlNo2.Text + "</b> has been successfully delivered. <br/>" +
                                           "Expect a hard copy of documents from Warehouse and do coordinate with them if needed. <br/>" +
                                           "Check the attached documents. <br/><br/>" +
                                           "Thank you and have a great day! <br/><br/>" +
                                           "ROHM Electronics Philippines Inc.";
                            }
                            ;

                        }

                        //BLL_Common BLLCOMMON = new BLL_Common();
                        //string buyerEmail = BLLCOMMON.GetBuyerEmailAddressByHandledCategory(buyerCategory.Value);

                        //string emailService = COMMON.sendEmailToSuppliers(ConfigurationManager.AppSettings["SRF_IMPEX_EMAIL"], ConfigurationManager.AppSettings["email-username"], "SRF DELIVERED CONFIRMATION",
                        //                                              verbiage, attachedFiles.Substring(0, attachedFiles.Length - 1).ToString(), string.Empty, string.Empty);

                        string emailService = COMMON.sendEmailToSuppliersForSRFWarehouse(ConfigurationManager.AppSettings["SRF_IMPEX_EMAIL"], ConfigurationManager.AppSettings["email-username"], "SRF DELIVERED CONFIRMATION",
                                                                      verbiage, attachmentFilesForEmail.Substring(0, attachmentFilesForEmail.Length - 1).ToString(), string.Empty, string.Empty);

                        if (emailService.ToLower().Contains("success"))
                        {
                            emailSuccess = "SRF NUMBER : <b>" + lblCtrlNo2.Text.ToUpper() + "</b> HAS BEEN SUCCESSFULLY PROCESSED. IMPEX HAS BEEN NOTIFIED.";
                        }
                        else
                        {
                            emailSuccess = "SRF NUMBER : <b>" + lblCtrlNo2.Text.ToUpper() + "</b> HAS BEEN SUCCESSFULLY PROCESSED. FAILED TO NOTIFY IMPEX DUE TO CONNECTION ISSUE.";
                        }


                        // REDIRECT TO SUCCESS PAGE
                        Session["successMessage"] = emailSuccess;
                        Session["successTransactionName"] = "SRF_WAREHOUSE";
                        Session["successReturnPage"] = "SRF_Warehouse2.aspx";
                        Response.Redirect("SuccessPage.aspx");


                    }



                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }



        protected void btnConfirmDocuments_Click(object sender, EventArgs e)
        {
            try
            {

                string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                string query1 = string.Empty;
                string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                string query_Success = string.Empty;
                string emailSuccess = string.Empty;


                string error = string.Empty;
                string isDiscrepancy = string.Empty;
                string verbiage = string.Empty;
                string attachedFiles = string.Empty;
                int hasFile = 0;

                for (int i = 0; i < gvWithoutDocuments.Rows.Count; i++)
                {
                    FileUpload fuWithoutDocuments = (FileUpload)gvWithoutDocuments.Rows[i].Cells[4].FindControl("fuWithoutDocuments");

                    if (fuWithoutDocuments.HasFile)
                    {
                        hasFile++;
                    }
                }


                if (hasFile <= 0)
                {
                    error += "Cannot process empty item! Please upload atleast 1 document!";
                }

                if (!string.IsNullOrEmpty(error))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + error + "'); showDialog2();", true);
                }
                else
                {
                    // JUST INCASE DIRECTORY FOR CTRLNO IS NOT YET CREATED
                    if (!System.IO.Directory.Exists(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim())))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()));
                    }

                    int numberOfAttachmentForWarehouse = BLL.SRF_TRANSACTION_Warehouse_ActualDelivery_Attachment_Count_ForWarehouse(lblCTRLNoWithoutDocument.Text.Trim());
                    numberOfAttachmentForWarehouse++;

                    for (int i = 0; i < gvWithoutDocuments.Rows.Count; i++)
                    {
                        Label lblRefId = (Label)gvWithoutDocuments.Rows[i].Cells[0].FindControl("lblRefId");
                        FileUpload fuWithoutDocuments = (FileUpload)gvWithoutDocuments.Rows[i].Cells[4].FindControl("fuWithoutDocuments");

                        // ATTACHMENT 
                        if (fuWithoutDocuments.HasFile)
                        {
                            //query1 += "INSERT INTO SRF_TRANSACTION_Warehouse (CTRLNo,AddedBy,AddedDate,TransType,Attachment) " +
                            //          "VALUES ('" + lblCTRLNoWithoutDocument.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE(), '2', '" + numberOfAttachment.ToString() + "-Warehouse.pdf" + "') ";

                            query1 += "UPDATE SRF_TRANSACTION_Warehouse_Actual_Delivery SET WithDocuments = '1', AttachmentWarehouse = '" + numberOfAttachmentForWarehouse.ToString() + "-Warehouse.pdf" + "' WHERE RefId ='" + lblRefId.Text.Trim() + "' ";

                            string filename1 = Path.GetFileName(fuWithoutDocuments.FileName);
                            fuWithoutDocuments.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + lblCTRLNoWithoutDocument.Text.Trim(), filename1));
                            fuWithoutDocuments.Dispose();
                            File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCTRLNoWithoutDocument.Text.Trim()), filename1), Path.Combine(Server.MapPath("~/SRF_Request/" + lblCTRLNoWithoutDocument.Text.Trim()), numberOfAttachmentForWarehouse.ToString() + "-Warehouse.pdf"), true);
                            File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCTRLNoWithoutDocument.Text.Trim()), filename1));

                            attachedFiles += Path.Combine(Server.MapPath("~/SRF_Request/" + lblCTRLNoWithoutDocument.Text.Trim()), numberOfAttachmentForWarehouse.ToString() + "-Warehouse.pdf") + ",";
                            numberOfAttachmentForWarehouse++;

                        }

                    }



                    query_Success = BLL.SRF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                    if (int.Parse(query_Success) > 0)
                    {

                        verbiage = "Please know that pending documents for <b>" + lblCtrlNo2.Text + "</b> has been successfully updated and ready for liquidation! <br/>" +
                                   "Expect a hard copy of documents from Warehouse and do coordinate with them if needed. <br/>" +
                                   "Check the attached documents. <br/><br/>" +
                                   "Thank you and have a great day! <br/><br/>" +
                                   "ROHM Electronics Philippines Inc.";


                        string emailService = COMMON.sendEmailToSuppliersForSRFWarehouse(ConfigurationManager.AppSettings["SRF_IMPEX_EMAIL"], ConfigurationManager.AppSettings["email-username"], "SRF UPDATED DOCUMENTS",
                                                                      verbiage, attachedFiles.Substring(0, attachedFiles.Length - 1), string.Empty, string.Empty);

                        if (emailService.ToLower().Contains("success"))
                        {
                            emailSuccess = "SRF NUMBER : <b>" + lblCTRLNoWithoutDocument.Text.ToUpper() + "</b> HAS BEEN SUCCESSFULLY PROCESSED. IMPEX HAS BEEN NOTIFIED.";
                        }
                        else
                        {
                            emailSuccess = "SRF NUMBER : <b>" + lblCTRLNoWithoutDocument.Text.ToUpper() + "</b> HAS BEEN SUCCESSFULLY PROCESSED. FAILED TO NOTIFY IMPEX DUE TO CONNECTION ISSUE.";
                        }


                        // REDIRECT TO SUCCESS PAGE
                        Session["successMessage"] = emailSuccess;
                        Session["successTransactionName"] = "SRF_WAREHOUSE";
                        Session["successReturnPage"] = "SRF_Warehouse2.aspx";
                        Response.Redirect("SuccessPage.aspx");


                    }



                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


        protected void btnDownloadReport2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFrom.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select a valid FROM (IMPEX APPROVED DATE)'); showDialog3();", true);
                }
                else if (string.IsNullOrEmpty(txtTo.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select a valid TO (IMPEX APPROVED DATE)'); showDialog3();", true);
                }
                else
                {

                    ////----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    ////FOR DASHBOARD FUNCTIONALITY

                    //if (Request.QueryString["type"] != null && Request.QueryString["pastMonth"] != null && Request.QueryString["pezaNonPeza"] != null)
                    //{
                    //    //PEZA
                    //    //Approved/Waiting for delivery (Type=1)
                    //    //Delivered w/ pending items (Type=2)
                    //    //DELIVERED (Type=3)

                    //    //Past 3Months & More
                    //    //PEZA
                    //    //DELIVERED

                    //    if (Request.QueryString["pastMonth"].ToString() == "3")
                    //    {
                    //        //PAST 3 MONTHS & MORE
                    //        txtFrom2.Text = DateTime.Today.AddDays(-365).ToString("MM/dd/yyyy");
                    //        txtTo2.Text = DateTime.Today.AddDays(-90).ToString("MM/dd/yyyy");
                    //    }
                    //    else if (Request.QueryString["pastMonth"].ToString() == "2")
                    //    {
                    //        //PAST 2 MONTHS
                    //        txtFrom2.Text = DateTime.Today.AddDays(-120).ToString("MM/dd/yyyy");
                    //        txtTo2.Text = DateTime.Today.AddDays(-60).ToString("MM/dd/yyyy");
                    //    }
                    //    else if (Request.QueryString["pastMonth"].ToString() == "1")
                    //    {
                    //        //PAST 1 MONTH
                    //        txtFrom2.Text = DateTime.Today.AddDays(-60).ToString("MM/dd/yyyy");
                    //        txtTo2.Text = DateTime.Today.AddDays(-30).ToString("MM/dd/yyyy");
                    //    }
                    //    else if (Request.QueryString["pastMonth"].ToString() == "44" || Request.QueryString["pastMonth"].ToString() == "55")
                    //    {
                    //        //PAST 1 MONTH
                    //        txtFrom2.Text = DateTime.Today.AddDays(-1825).ToString("MM/dd/yyyy");
                    //        txtTo2.Text = DateTime.Today.AddDays(-30).ToString("MM/dd/yyyy");
                    //    }
                    //    else
                    //    {
                    //        txtFrom2.Text = DateTime.Today.AddDays(-365).ToString("MM/dd/yyyy");
                    //        txtTo2.Text = DateTime.Today.AddDays(-30).ToString("MM/dd/yyyy");
                    //    }

                    //    if (Request.QueryString["type"].ToString() == "1")
                    //    {
                    //        ddStatus.SelectedValue = "APPROVED / WAITING FOR DELIVERY";
                    //        ddReportStatus.SelectedValue = "APPROVED / WAITING FOR DELIVERY";
                    //    }
                    //    if (Request.QueryString["type"].ToString() == "2")
                    //    {
                    //        ddStatus.SelectedValue = "DELIVERED WITH PENDING ITEMS";
                    //        ddReportStatus.SelectedValue = "DELIVERED WITH PENDING ITEMS";
                    //    }
                    //    if (Request.QueryString["type"].ToString() == "3")
                    //    {
                    //        ddStatus.SelectedValue = "DELIVERED";
                    //        ddReportStatus.SelectedValue = "DELIVERED";
                    //    }
                    //    if (Request.QueryString["type"].ToString() == "4" || Request.QueryString["type"].ToString() == "5")
                    //    {
                    //        ddStatus.SelectedValue = "";
                    //        ddReportStatus.SelectedValue = "ALL";
                    //    }

                    //    ddReportType.SelectedValue = "ALL ITEMS";

                    //    if (Request.QueryString["pezaNonPeza"].ToString() == "1")
                    //    {
                    //        ddPezaNonPezaReport.SelectedValue = "PEZA";
                    //    }
                    //    if (Request.QueryString["pezaNonPeza"].ToString() == "2")
                    //    {
                    //        ddPezaNonPezaReport.SelectedValue = "NON PEZA";
                    //    }

                    //}


                    ////----------------------------------------------------------------------------------------------------------------------------------------------------------------------


                    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    if (ddReportType.SelectedItem.Text.ToUpper() == "ALL ITEMS")
                    {

                        if (!System.IO.File.Exists(Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LOA" + ".xlsx")))
                        {
                            System.IO.File.Copy(Server.MapPath("~/SRF_XLS/Template/LOA_Template.xlsx"), Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LOA" + ".xlsx"));
                        }
                        else
                        {
                            System.IO.File.Delete(Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LOA" + ".xlsx"));
                            System.IO.File.Copy(Server.MapPath("~/SRF_XLS/Template/LOA_Template.xlsx"), Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LOA" + ".xlsx"));
                        }


                        string path = Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LOA" + ".xlsx");
                        Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + path);
                        FileStream fsBI = new FileStream(path, FileMode.Open);
                        using (SLDocument draft = new SLDocument(fsBI, "LOA"))
                        {
                            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

                            if (!string.IsNullOrEmpty(ctrlnoList.Value))
                            {                                

                                //if (ddPezaNonPezaReport.SelectedItem.Text.ToUpper() == "PEZA") //WAREHOUSE USERS
                                //{
                                //    if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                                //    {
                                //        list = BLL.SRF_TRANSACTION_Warehouse_Reporting_FromDashboard(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim(), ctrlnoList.Value.Substring(0, ctrlnoList.Value.Length - 1)).Where(itm => itm.Warehouse_PezaNonPeza == "1").ToList();
                                //    }
                                //    else
                                //    {
                                //        list = BLL.SRF_TRANSACTION_Warehouse_Reporting_FromDashboard(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim(), ctrlnoList.Value.Substring(0, ctrlnoList.Value.Length - 1)).Where(itm => itm.Warehouse_PezaNonPeza == "1" && itm.StatAll == ddReportStatus.SelectedItem.Text.ToUpper()).ToList();
                                //    }
                                //}
                                //else if (ddPezaNonPezaReport.SelectedItem.Text.ToUpper() == "NON PEZA") //MAM JARED (IMPEX)
                                //{
                                //    if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                                //    {
                                //        list = BLL.SRF_TRANSACTION_Warehouse_Reporting_FromDashboard(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim(), ctrlnoList.Value.Substring(0, ctrlnoList.Value.Length - 1)).Where(itm => itm.Warehouse_PezaNonPeza == "2").ToList();
                                //    }
                                //    else
                                //    {
                                //        list = BLL.SRF_TRANSACTION_Warehouse_Reporting_FromDashboard(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim(), ctrlnoList.Value.Substring(0, ctrlnoList.Value.Length - 1)).Where(itm => itm.Warehouse_PezaNonPeza == "2" && itm.StatAll == ddReportStatus.SelectedItem.Text.ToUpper()).ToList();
                                //    }
                                //}
                                //else
                                //{
                                //    if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                                //    {
                                //        list = BLL.SRF_TRANSACTION_Warehouse_Reporting_FromDashboard(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim(), ctrlnoList.Value.Substring(0, ctrlnoList.Value.Length - 1));
                                //    }
                                //    else
                                //    {
                                //        list = BLL.SRF_TRANSACTION_Warehouse_Reporting_FromDashboard(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim(), ctrlnoList.Value.Substring(0, ctrlnoList.Value.Length - 1)).Where(itm => itm.StatAll == ddReportStatus.SelectedItem.Text.ToUpper()).ToList();
                                //    }
                                //}

                                list = BLL.SRF_TRANSACTION_Warehouse_Reporting_FromDashboard(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim(), ctrlnoList.Value.Substring(0, ctrlnoList.Value.Length - 1));

                                ctrlnoList.Value = string.Empty;

                            }
                            else
                            {

                                if (ddPezaNonPezaReport.SelectedItem.Text.ToUpper() == "PEZA") //WAREHOUSE USERS
                                {
                                    if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                                    {
                                        list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "1").ToList();
                                    }
                                    else
                                    {
                                        list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "1" && itm.StatAll == ddReportStatus.SelectedItem.Text.ToUpper()).ToList();
                                    }
                                }
                                else if (ddPezaNonPezaReport.SelectedItem.Text.ToUpper() == "NON PEZA") //MAM JARED (IMPEX)
                                {
                                    if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                                    {
                                        list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "2").ToList();
                                    }
                                    else
                                    {
                                        list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "2" && itm.StatAll == ddReportStatus.SelectedItem.Text.ToUpper()).ToList();
                                    }
                                }
                                else
                                {
                                    if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                                    {
                                        list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim());
                                    }
                                    else
                                    {
                                        list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.StatAll == ddReportStatus.SelectedItem.Text.ToUpper()).ToList();
                                    }
                                }

                            }

                            if (list != null)
                            {
                                if (list.Count > 0)
                                {
                                    int cnt = 2;
                                    int itemNoCnt = 0;
                                    string prevCTRLNo = string.Empty;


                                    foreach (Entities_SRF_RequestEntry elist in list)
                                    {
                                        if (elist.Warehouse_CtrlNo != prevCTRLNo)
                                        {
                                            itemNoCnt++;
                                        }

                                        draft.SetCellValue("A" + cnt.ToString(), itemNoCnt.ToString());
                                        draft.SetCellValue("B" + cnt.ToString(), elist.Warehouse_CtrlNo);
                                        draft.SetCellValue("C" + cnt.ToString(), elist.CategoryDescription);
                                        draft.SetCellValue("D" + cnt.ToString(), elist.Warehouse_LOA8106);
                                        draft.SetCellValue("E" + cnt.ToString(), elist.Warehouse_8105);
                                        draft.SetCellValue("F" + cnt.ToString(), elist.Warehouse_ItemName);
                                        draft.SetCellValue("G" + cnt.ToString(), elist.Warehouse_TotalQuantity);
                                        draft.SetCellValue("H" + cnt.ToString(), elist.Warehouse_TotalActualQuantity);
                                        draft.SetCellValue("I" + cnt.ToString(), elist.Warehouse_RemainingQuantity);
                                        draft.SetCellValue("J" + cnt.ToString(), elist.Warehouse_DeliveredDate);
                                        draft.SetCellValue("K" + cnt.ToString(), elist.Warehouse_RequesterName);
                                        draft.SetCellValue("L" + cnt.ToString(), elist.Warehouse_SupplierName);
                                        draft.SetCellValue("M" + cnt.ToString(), elist.PullOutServiceDate_From_HEAD);
                                        draft.SetCellValue("N" + cnt.ToString(), elist.StatAll);

                                        cnt++;
                                        prevCTRLNo = elist.Warehouse_CtrlNo;
                                    }
                                }
                            }


                            fsBI.Close();
                            draft.SaveAs(path);

                        }


                        Response.Redirect("SRF_XLS/" + Session["LcRefId"].ToString() + "_LOA.xlsx", false);



                    }

                    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------


                    if (ddReportType.SelectedItem.Text.ToUpper() == "LIQUIDATION LEDGER")
                    {

                        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

                        if (ddPezaNonPezaReport.SelectedItem.Text.ToUpper() == "PEZA") //WAREHOUSE USERS
                        {
                            if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                            {
                                list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "1" && string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                            }
                            else
                            {
                                list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "1" && string.IsNullOrEmpty(itm.GatePassNo_From_HEAD) && itm.StatAll == ddReportStatus.SelectedItem.Text.ToUpper()).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                            }
                        }
                        else if (ddPezaNonPezaReport.SelectedItem.Text.ToUpper() == "NON PEZA") //MAM JARED (IMPEX)
                        {
                            if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                            {
                                list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "2" && string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                            }
                            else
                            {
                                list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "2" && string.IsNullOrEmpty(itm.GatePassNo_From_HEAD) && itm.StatAll == ddReportStatus.SelectedItem.Text.ToUpper()).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                            }
                        }
                        else
                        {
                            if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                            {
                                list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                            }
                            else
                            {
                                list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => string.IsNullOrEmpty(itm.GatePassNo_From_HEAD) && itm.StatAll == ddReportStatus.SelectedItem.Text.ToUpper()).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                            }
                        }


                        if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()))
                        {
                            Decimal listCount = list.Count;
                            int templateNumber = 0;
                            Decimal ans = Decimal.Divide(listCount, 20);
                            int ans2 = (int)ans;

                            if (ans > ans2)
                            {
                                templateNumber = ans2 + 1;
                            }
                            else
                            {
                                templateNumber = ans2;
                            }



                            if (!System.IO.File.Exists(Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LIQUIDATION" + ".xlsx")))
                            {
                                System.IO.File.Copy(Server.MapPath("~/SRF_XLS/Template/SRF_Liquidation_" + templateNumber + ".xlsx"), Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LIQUIDATION" + ".xlsx"));
                            }
                            else
                            {
                                System.IO.File.Delete(Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LIQUIDATION" + ".xlsx"));
                                System.IO.File.Copy(Server.MapPath("~/SRF_XLS/Template/SRF_Liquidation_" + templateNumber + ".xlsx"), Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LIQUIDATION" + ".xlsx"));
                            }


                            
                            int tempCnt = 0;

                            //for (int tempCnt = 1; tempCnt <= templateNumber; tempCnt++)
                            //{

                                
                                    if (list != null)
                                    {
                                        if (list.Count > 0)
                                        {
                                            int cnt = 10;
                                            string previousItemName = string.Empty;
                                            string previousSupplier = string.Empty;
                                            int recCount = 0;

                                            List<Entities_SRF_RequestEntry> listDistinctSupplierName = new List<Entities_SRF_RequestEntry>();
                                            if (ddPezaNonPezaReport.SelectedItem.Text.ToUpper() == "PEZA") //WAREHOUSE USERS
                                            {
                                                if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                                                {
                                                    listDistinctSupplierName = BLL.SRF_TRANSACTION_Warehouse_Reporting_Distinct_SupplierName(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "1" && string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                                                }
                                                else
                                                {
                                                    listDistinctSupplierName = BLL.SRF_TRANSACTION_Warehouse_Reporting_Distinct_SupplierName(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "1" && string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                                                }
                                            }
                                            else if (ddPezaNonPezaReport.SelectedItem.Text.ToUpper() == "NON PEZA") //MAM JARED (IMPEX)
                                            {
                                                if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                                                {
                                                    listDistinctSupplierName = BLL.SRF_TRANSACTION_Warehouse_Reporting_Distinct_SupplierName(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "2" && string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                                                }
                                                else
                                                {
                                                    listDistinctSupplierName = BLL.SRF_TRANSACTION_Warehouse_Reporting_Distinct_SupplierName(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "2" && string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                                                }
                                            }
                                            else
                                            {
                                                if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                                                {
                                                    listDistinctSupplierName = BLL.SRF_TRANSACTION_Warehouse_Reporting_Distinct_SupplierName(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                                                }
                                                else
                                                {
                                                    listDistinctSupplierName = BLL.SRF_TRANSACTION_Warehouse_Reporting_Distinct_SupplierName(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                                                }
                                            }

                                            if (listDistinctSupplierName != null)
                                            {

                                                foreach (Entities_SRF_RequestEntry eDistinct in listDistinctSupplierName.GroupBy(itm => itm.Warehouse_SupplierName).Select(g => g.First()))
                                                {

                                                    int lessThan20 = list.Where(itm => int.Parse(itm.Warehouse_TotalActualQuantity) > 0 && itm.Warehouse_SupplierName.Replace(" ", "").Trim() == eDistinct.Warehouse_SupplierName.Replace(" ", "").Trim()).ToList().Count();
                                                    string sheetName = eDistinct.Warehouse_SupplierName.Length > 10 ? eDistinct.Warehouse_SupplierName.Substring(0, 10).ToString() + "-" : eDistinct.Warehouse_SupplierName + "-";

                                                    int skip = 0;
                                                    int skipCount = 0;
                                                    int take = 20;
                                                    int counterForActualRecords = 1;

                                                    Decimal listCount3 = lessThan20;
                                                    int templateNumber3 = 0;
                                                    Decimal ans3 = Decimal.Divide(listCount3, 20);
                                                    int ans33 = (int)ans3;

                                                    if (ans3 > ans33)
                                                    {
                                                        templateNumber3 = ans33 + 1;
                                                    }
                                                    else
                                                    {
                                                        templateNumber3 = ans33;
                                                    }

                                                    for (int tempCnt2 = 1; tempCnt2 <= templateNumber3; tempCnt2++)
                                                    {

                                                        foreach (Entities_SRF_RequestEntry elist in list.Where(itm => int.Parse(itm.Warehouse_TotalActualQuantity) > 0 && itm.Warehouse_SupplierName.Replace(" ", "").Trim() == eDistinct.Warehouse_SupplierName.Replace(" ", "").Trim()).ToList().Skip(skipCount).Take(lessThan20 < 20 ? lessThan20 : take).ToList())
                                                        {

                                                            string path_LIQUIDATION = Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LIQUIDATION" + ".xlsx");
                                                            Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + path_LIQUIDATION);
                                                            FileStream fsBI_LIQUIDATION = new FileStream(path_LIQUIDATION, FileMode.Open);
                                                            using (SLDocument draft_LIQUIDATION = new SLDocument(fsBI_LIQUIDATION, "LT" + (tempCnt + tempCnt2).ToString()))
                                                            {

                                                                if (cnt <= 29)
                                                                {


                                                                    draft_LIQUIDATION.SetCellValue("E4", "RECIPIENT/CONSIGNEE: " + elist.Warehouse_SupplierName);
                                                                    draft_LIQUIDATION.SetCellValue("B6", elist.LOA_Number_From_HEAD);
                                                                    draft_LIQUIDATION.SetCellValue("G6", elist.LOA_MaturityDate_From_HEAD);
                                                                    draft_LIQUIDATION.SetCellValue("M6", elist.LOASuretyBond_From_HEAD);

                                                                    draft_LIQUIDATION.SetCellValue("A10", elist.Warehouse_SupplierName);

                                                                    if (!string.IsNullOrEmpty(elist.Warehouse_ItemName.Trim()))
                                                                    {
                                                                        if ((elist.Warehouse_ItemName.Trim() + elist.Warehouse_LOA8106.Trim()) != previousItemName)
                                                                        {
                                                                            draft_LIQUIDATION.SetCellValue("B" + cnt.ToString(), elist.Warehouse_ItemName);
                                                                            draft_LIQUIDATION.SetCellValue("C" + cnt.ToString(), elist.Warehouse_LOA8106);
                                                                            draft_LIQUIDATION.SetCellValue("D" + cnt.ToString(), elist.PullOutServiceDate_From_HEAD);
                                                                            draft_LIQUIDATION.SetCellValue("E" + cnt.ToString(), elist.Warehouse_TotalQuantity);
                                                                            draft_LIQUIDATION.SetCellValueNumeric("F" + cnt.ToString(), (double.Parse(elist.Warehouse_TotalQuantity) * double.Parse(elist.LOAPriceValue_From_HEAD)).ToString());
                                                                        }
                                                                        //draft_LIQUIDATION.SetCellValueNumeric("F" + cnt.ToString(), (double.Parse(elist.Warehouse_TotalQuantity) * double.Parse(elist.LOAPriceValue_From_HEAD)).ToString());
                                                                        draft_LIQUIDATION.SetCellValueNumeric("M" + cnt.ToString(), (double.Parse(elist.Warehouse_TotalActualQuantity) * double.Parse(elist.LOAPriceValue_From_HEAD)).ToString());

                                                                        draft_LIQUIDATION.SetCellValue("I" + cnt.ToString(), elist.Warehouse_ItemName);

                                                                    }

                                                                    draft_LIQUIDATION.SetCellValue("J" + cnt.ToString(), elist.Warehouse_8105);
                                                                    draft_LIQUIDATION.SetCellValue("K" + cnt.ToString(), elist.Warehouse_LOA8105ProcessDate);

                                                                    if (string.IsNullOrEmpty(elist.Warehouse_8105))
                                                                    {
                                                                        // DO NOTHING
                                                                    }
                                                                    else
                                                                    {
                                                                        draft_LIQUIDATION.SetCellValue("L" + cnt.ToString(), elist.Warehouse_TotalActualQuantity);
                                                                    }
                                                                    

                                                                    if (lessThan20 <= 20 && lessThan20.ToString() == counterForActualRecords.ToString())
                                                                    {
                                                                        
                                                                        draft_LIQUIDATION.RenameWorksheet("LT" + (tempCnt + tempCnt2).ToString(), sheetName + tempCnt2.ToString());
                                                                        
                                                                    }

                                                                    if (counterForActualRecords.ToString() == "20")
                                                                    {
                                                                        skip++;
                                                                        skipCount = skip * 20;

                                                                        draft_LIQUIDATION.RenameWorksheet("LT" + (tempCnt + tempCnt2).ToString(), sheetName + tempCnt2.ToString());

                                                                        counterForActualRecords = 1;
                                                                        cnt = 10;

                                                                    }


                                                                }

                                                                previousItemName = elist.Warehouse_ItemName.Trim() + elist.Warehouse_LOA8106.Trim();
                                                                previousSupplier = elist.Warehouse_SupplierName.Replace(" ", "").Trim();
                                                                cnt++;
                                                                counterForActualRecords++;

                                                                fsBI_LIQUIDATION.Close();
                                                                draft_LIQUIDATION.SaveAs(path_LIQUIDATION);

                                                            }



                                                        }

                                                        

                                                    }

                                                    tempCnt++;    

                                                    //}




                                                    cnt = 10;

                                                }

                                            }


                                        }
                                    }


                                    


                                

                            //}


                            Response.Redirect("SRF_XLS/" + Session["LcRefId"].ToString() + "_LIQUIDATION.xlsx", false);


                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You dont have access rights for this report!'); showDialog3();", true);
                        }


                    }







                }





            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }



        protected void btnDownloadReport3_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtFrom.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select a valid FROM (IMPEX APPROVED DATE)'); showDialog3();", true);
                }
                else if (string.IsNullOrEmpty(txtTo.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select a valid TO (IMPEX APPROVED DATE)'); showDialog3();", true);
                }
                else
                {

                    if (ddReportType.SelectedItem.Text.ToUpper() == "ALL ITEMS")
                    {

                        if (!System.IO.File.Exists(Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LOA_Summary" + ".xlsx")))
                        {
                            System.IO.File.Copy(Server.MapPath("~/SRF_XLS/Template/LOA_Template2.xlsx"), Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LOA_Summary" + ".xlsx"));
                        }
                        else
                        {
                            System.IO.File.Delete(Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LOA_Summary" + ".xlsx"));
                            System.IO.File.Copy(Server.MapPath("~/SRF_XLS/Template/LOA_Template2.xlsx"), Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LOA_Summary" + ".xlsx"));
                        }


                        string path = Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LOA_Summary" + ".xlsx");
                        Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + path);
                        FileStream fsBI = new FileStream(path, FileMode.Open);
                        using (SLDocument draft = new SLDocument(fsBI, "LOA"))
                        {
                            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

                            if (!string.IsNullOrEmpty(ctrlnoList.Value))
                            {

                                list = BLL.SRF_TRANSACTION_Warehouse_Reporting_FromDashboard_Summarize(ctrlnoList.Value.Substring(0, ctrlnoList.Value.Length - 1), txtFrom.Text.Trim(), txtTo.Text.Trim());
                                ctrlnoList.Value = string.Empty;

                            }


                            if (list != null)
                            {
                                if (list.Count > 0)
                                {
                                    int cnt = 2;
                                    int itemNoCnt = 0;
                                    string prevCTRLNo = string.Empty;


                                    foreach (Entities_SRF_RequestEntry elist in list)
                                    {
                                        if (elist.Warehouse_CtrlNo != prevCTRLNo)
                                        {
                                            itemNoCnt++;
                                        }

                                        draft.SetCellValue("A" + cnt.ToString(), itemNoCnt.ToString());
                                        draft.SetCellValue("B" + cnt.ToString(), elist.Warehouse_CtrlNo);
                                        draft.SetCellValue("C" + cnt.ToString(), elist.CategoryDescription);
                                        draft.SetCellValue("D" + cnt.ToString(), elist.Warehouse_LOA8106);
                                        draft.SetCellValue("E" + cnt.ToString(), elist.Warehouse_8105);

                                        draft.SetCellValue("F" + cnt.ToString(), elist.Warehouse_OverallTotalQty);
                                        draft.SetCellValue("G" + cnt.ToString(), elist.Warehouse_TotalActualQuantity);
                                        draft.SetCellValue("H" + cnt.ToString(), long.Parse(elist.Warehouse_OverallTotalQty) - long.Parse(elist.Warehouse_TotalActualQuantity));
                                        draft.SetCellValue("I" + cnt.ToString(), elist.Warehouse_DeliveredDate);
                                        draft.SetCellValue("J" + cnt.ToString(), elist.Warehouse_RequesterName);
                                        draft.SetCellValue("K" + cnt.ToString(), elist.Warehouse_SupplierName);
                                        draft.SetCellValue("L" + cnt.ToString(), elist.Warehouse_PullOutServiceDate);
                                        draft.SetCellValue("M" + cnt.ToString(), elist.StatAll);

                                        cnt++;
                                        prevCTRLNo = elist.Warehouse_CtrlNo;
                                    }
                                }
                            }


                            fsBI.Close();
                            draft.SaveAs(path);



                        }


                        Response.Redirect("SRF_XLS/" + Session["LcRefId"].ToString() + "_LOA_Summary.xlsx", false);

                    }

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }





        //protected void linkAttachment1_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("http://10.27.1.170:9292/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + linkAttachment1.Text.Trim(), true);
        //}
        //protected void linkAttachment2_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("http://10.27.1.170:9292/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + linkAttachment1.Text.Trim(), true);
        //}
        //protected void linkAttachment3_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("http://10.27.1.170:9292/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + linkAttachment1.Text.Trim(), true);
        //}
        //protected void linkAttachment4_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("http://10.27.1.170:9292/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + linkAttachment1.Text.Trim(), true);
        //}
        //protected void linkAttachment5_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("http://10.27.1.170:9292/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + linkAttachment1.Text.Trim(), true);
        //}





    }
}
