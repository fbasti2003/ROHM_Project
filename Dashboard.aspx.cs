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

namespace REPI_PUR_SOFRA
{
    public partial class Dashboard : System.Web.UI.Page
    {
        BLL_PIPL BLL = new BLL_PIPL();
        BLL_Common BLLCommon = new BLL_Common();
        Common COMMON = new Common();

        BLL_RFQ BLL_RFQ = new BLL_RFQ();
        BLL_SRF BLL_SRF = new BLL_SRF();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["Username"].ToString() == "FERDIE")
                    {
                        //DO NOT DISPLAY DASHBOARD
                    }
                    else
                    {


                        // PROFORMA NOTIFICATION ----------------------------------------------------
                        Entities_PIPL_InvoiceEntry status = new Entities_PIPL_InvoiceEntry();

                        status.DrFrom = DateTime.Today.AddDays(-30).ToString("MM/dd/yyyy");
                        status.DrTo = DateTime.Today.AddDays(30).ToString("MM/dd/yyyy");
                        status.Requester = Session["LcRefId"].ToString();

                        // FOR IMPEX (Sir Renz)
                        var PIPL_Impex_Access = ConfigurationManager.AppSettings["PIPL_Temp_Sir_Renz"];
                        if (Session["Username"].ToString() == PIPL_Impex_Access.ToString())
                        {
                            List<Entities_PIPL_InvoiceEntry> listImpex = new List<Entities_PIPL_InvoiceEntry>();
                            listImpex = BLL.PIPL_TRANSACTION_ForApproval(status).Where(item => item.StatIncharge == "1" && (item.StatImpex == "0" || item.StatImpex == null)).ToList();

                            if (listImpex.Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('PROFORMA NOTIFICATION! You have " + listImpex.Count.ToString() + " item(s) for approval.');", true);
                            }
                        }

                        // FOR PURCHASING INCHARGE (BY CATEGORY)
                        var PIPL_Incharge_Access = Session["CategoryAccess"].ToString();
                        if (int.Parse(PIPL_Incharge_Access.ToString()) > 0)
                        {
                            List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();
                            list = BLL.PIPL_TRANSACTION_ForApproval(status).Where(item => item.Category == PIPL_Incharge_Access
                                                                               && item.StatManager == "1" && item.StatPCManager == "1" && (item.StatAccounting == "1" || item.StatAccounting == "3")
                                                                               && item.StatIncharge == "0").ToList();
                            if (list.Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('PROFORMA NOTIFICATION! You have " + list.Count.ToString() + " item(s) for approval.');", true);
                            }
                        }

                        if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()) || Session["Username"].ToString() == PIPL_Impex_Access.ToString() || Session["Username"].ToString() == "057")
                        {

                            //---------------------------------------------------------------------------

                            List<Entities_RFQ_RequestEntry> list_TodayTopSupplierResponded = new List<Entities_RFQ_RequestEntry>();
                            list_TodayTopSupplierResponded = BLL_RFQ.RFQ_TRANSACTION_GetTodayTopRespondedSupplier();

                            if (list_TodayTopSupplierResponded != null)
                            {
                                if (list_TodayTopSupplierResponded.Count > 0)
                                {
                                    gvData.DataSource = list_TodayTopSupplierResponded;
                                    gvData.DataBind();
                                }
                            }

                            //---------------------------------------------------------------------------

                            //if (!COMMON.isUserAllowed(Session["LcRefId"].ToString(), "601")) //Disabled dashboard MY FOR APPROVALS
                            //{
                            List<Entities_Common_ForApproval> listForApproval = new List<Entities_Common_ForApproval>();
                            listForApproval = BLLCommon.Common_GetForApprovalByCategoryId(Session["CategoryAccess"].ToString(), Session["Username"].ToString(), Session["Department"].ToString(), Session["Division"].ToString(), Session["HQ"].ToString(), string.Empty, string.Empty);

                            if (listForApproval != null)
                            {
                                if (listForApproval.Count > 0)
                                {
                                    gvForApproval.DataSource = listForApproval;
                                    gvForApproval.DataBind();

                                    int hasSRFApproval = 0;

                                    foreach (Entities_Common_ForApproval srfApproval in listForApproval)
                                    {
                                        if (srfApproval.TransactionName.ToUpper() == "SERVICE REPAIR FORM")
                                        {
                                            hasSRFApproval++;
                                        }
                                    }

                                    if (hasSRFApproval > 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('SERVICE REPAIR NOTIFICATION! You have SRF items for approval that needs to be settled until 11am today for Impex to process 8106/8112.');", true);
                                    }

                                }
                            }
                            //}


                            //---------------------------------------------------------------------------

                            List<Entities_Common_ForApproval> listThisWeekStatus = new List<Entities_Common_ForApproval>();
                            listThisWeekStatus = BLLCommon.Common_GetThisWeekRequestStatus().OrderBy(itm => itm.Num).ToList();

                            if (listThisWeekStatus != null)
                            {
                                if (listThisWeekStatus.Count > 0)
                                {
                                    //gvThisWeekStatus.DataSource = listThisWeekStatus;
                                    //gvThisWeekStatus.DataBind();
                                    //gvThisWeekStatus.Visible = false;

                                    gvGraphicalPresentation.DataSource = listThisWeekStatus.Skip(0).Take(3).ToList();
                                    gvGraphicalPresentation.DataBind();

                                    gvGraphicalPresentation2.DataSource = listThisWeekStatus.Skip(3).Take(3).ToList();
                                    gvGraphicalPresentation2.DataBind();

                                    gvGraphicalPresentation3.DataSource = listThisWeekStatus.Skip(6).Take(3).ToList();
                                    gvGraphicalPresentation3.DataBind();
                                }
                            }

                            //---------------------------------------------------------------------------

                            List<Entities_Common_ForApproval> listOtherBuyersRFQ = new List<Entities_Common_ForApproval>();
                            listOtherBuyersRFQ = BLLCommon.Common_RFQ_GetOtherBuyerItems().Where(itm => itm.OtherBuyer_Category != Session["CategoryAccess"].ToString()).ToList();

                            if (listOtherBuyersRFQ != null)
                            {
                                if (listOtherBuyersRFQ.Count > 0)
                                {
                                    gvForOtherBuyers.DataSource = listOtherBuyersRFQ;
                                    gvForOtherBuyers.DataBind();
                                }

                            }

                            //---------------------------------------------------------------------------

                            divThisWeekRequest.Style.Add("display", "block");
                            divMyApproval.Style.Add("display", "block");
                            divTopResponded.Style.Add("display", "block");
                            divOtherBuyersRFQ.Style.Add("display", "block");
                            divWarehouse.Style.Add("display", "block");

                        }
                        else
                        {
                            if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["ProductionApprovalAccess"].ToString().Trim()))
                            {
                                // PRODUCTION SIDE
                                divMyApproval.Style.Add("display", "block");

                                string isDivision = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "403").ToString().ToLower();
                                string isHQ = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "404").ToString().ToLower();

                                List<Entities_Common_ForApproval> listForApproval = new List<Entities_Common_ForApproval>();
                                listForApproval = BLLCommon.Common_GetForApprovalByCategoryId(Session["CategoryAccess"].ToString(), Session["Username"].ToString(), Session["Department"].ToString(), Session["Division"].ToString(), Session["HQ"].ToString(), isDivision, isHQ);

                                if (listForApproval != null)
                                {
                                    if (listForApproval.Count > 0)
                                    {

                                        List<Entities_Common_ForApproval> listForApprovalFinal = new List<Entities_Common_ForApproval>();
                                        foreach (Entities_Common_ForApproval eFinal in listForApproval)
                                        {
                                            Entities_Common_ForApproval entFinal = new Entities_Common_ForApproval();
                                            entFinal.TransactionName = eFinal.TransactionName;
                                            entFinal.ForApprovalCount = eFinal.ForApprovalCount;

                                            listForApprovalFinal.Add(entFinal);

                                            // PROFORMA
                                            if (eFinal.TransactionName == "PROFORMA - PC MANAGER" && !COMMON.isUserAllowed(Session["LcRefId"].ToString(), "101"))
                                            {
                                                listForApprovalFinal.Remove(entFinal);
                                            }
                                            if (eFinal.TransactionName == "PROFORMA - ACCOUNTING" && !COMMON.isUserAllowed(Session["LcRefId"].ToString(), "215"))
                                            {
                                                listForApprovalFinal.Remove(entFinal);
                                            }

                                            //URF
                                            //SECTION MANAGER
                                            if (eFinal.TransactionName == "URGENT REQUEST FORM (SEC. MANAGER)" && !COMMON.isUserAllowed(Session["LcRefId"].ToString(), "401"))
                                            {
                                                listForApprovalFinal.Remove(entFinal);
                                            }
                                            //DEPARTMENT MANAGER
                                            if (eFinal.TransactionName == "URGENT REQUEST FORM (DEPT. MANAGER)" && !COMMON.isUserAllowed(Session["LcRefId"].ToString(), "402"))
                                            {
                                                listForApprovalFinal.Remove(entFinal);
                                            }
                                            //DIVISION MANAGER
                                            if (eFinal.TransactionName == "URGENT REQUEST FORM (DIVISION MANAGER)" && !COMMON.isUserAllowed(Session["LcRefId"].ToString(), "403"))
                                            {
                                                listForApprovalFinal.Remove(entFinal);
                                            }
                                            //HQ MANAGER
                                            if (eFinal.TransactionName == "URGENT REQUEST FORM (HQ. MANAGER)" && !COMMON.isUserAllowed(Session["LcRefId"].ToString(), "404"))
                                            {
                                                listForApprovalFinal.Remove(entFinal);
                                            }

                                        }

                                        if (listForApprovalFinal.Count > 0)
                                        {
                                            gvForApproval.DataSource = listForApprovalFinal;
                                            gvForApproval.DataBind();
                                        }

                                    }
                                }
                            }
                            else
                            {
                                // REQUESTER
                                List<Entities_Common_ForApproval> listForApprovalRequester = new List<Entities_Common_ForApproval>();
                                listForApprovalRequester = BLLCommon.Common_GetForApprovalByCategoryId(Session["CategoryAccess"].ToString(), Session["Username"].ToString(), Session["Department"].ToString(), Session["Division"].ToString(), Session["HQ"].ToString(), string.Empty, string.Empty);

                                if (listForApprovalRequester != null)
                                {
                                    if (listForApprovalRequester.Count > 0)
                                    {

                                        divMyApproval.Style.Add("display", "block");

                                        gvForApproval.DataSource = listForApprovalRequester;
                                        gvForApproval.DataBind();

                                        gvForApproval.Visible = true;

                                    }
                                }

                            }

                        }


                        // SRF WAREHOUSE WITHOU DOCUMENTS
                        if (ConfigurationManager.AppSettings["PIPL_Temp_MT_Access"].ToString().Contains(Session["Username"].ToString()) || BLLCommon.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString()).Count > 0 || BLLCommon.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), "311").Count > 0)
                        {

                            divWarehouse.Style.Add("display", "block");

                            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                            long dashboardPeza_ApprovedWaiting_8106_Count = 0;
                            long dashboardPeza_ApprovedWaiting_Pullout_Count = 0;
                            long dashboardPeza_ApprovedWaiting_Remaining_Count = 0;

                            long dashboardPeza_DeliveredPending_8106_Count = 0;
                            long dashboardPeza_DeliveredPending_Pullout_Count = 0;
                            long dashboardPeza_DeliveredPending_Remaining_Count = 0;

                            long dashboardPeza_Delivered_8106_Count = 0;
                            long dashboardPeza_Delivered_Pullout_Count = 0;
                            long dashboardPeza_Delivered_Remaining_Count = 0;

                            long dashboardNonPeza_ApprovedWaiting_8106_Count = 0;
                            long dashboardNonPeza_ApprovedWaiting_Pullout_Count = 0;
                            long dashboardNonPeza_ApprovedWaiting_Remaining_Count = 0;

                            long dashboardNonPeza_DeliveredPending_8106_Count = 0;
                            long dashboardNonPeza_DeliveredPending_Pullout_Count = 0;
                            long dashboardNonPeza_DeliveredPending_Remaining_Count = 0;

                            long dashboardNonPeza_Delivered_8106_Count = 0;
                            long dashboardNonPeza_Delivered_Pullout_Count = 0;
                            long dashboardNonPeza_Delivered_Remaining_Count = 0;

                            string oldLOA8106 = string.Empty;

                            long totalPeza = 0;
                            long totalNonPeza = 0;

                            long totalPezaAll = 0;
                            long totalNonPezaAll = 0;


                            //PAST 1 MONTH ==========================================================================================================================================================================
                            List<Entities_SRF_RequestEntry> listWarehouse = new List<Entities_SRF_RequestEntry>();
                            listWarehouse = BLL_SRF.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange("", DateTime.Today.AddDays(-60).ToString("MM/dd/yyyy"), DateTime.Today.AddDays(-30).ToString("MM/dd/yyyy")).Where(itm2 => !string.IsNullOrEmpty(itm2.Warehouse_LOA8106)).OrderBy(itm => itm.Warehouse_LOA8106).ToList();


                            if (listWarehouse != null)
                            {
                                if (listWarehouse.Count > 0)
                                {

                                    foreach (Entities_SRF_RequestEntry entity in listWarehouse)
                                    {

                                        //PEZA
                                        if (entity.Warehouse_PezaNonPeza == "1")
                                        {
                                            if (entity.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY")
                                            {
                                                if (oldLOA8106 != entity.Old8106.Trim())
                                                {
                                                    dashboardPeza_ApprovedWaiting_8106_Count++;
                                                }
                                                //dashboardPeza_ApprovedWaiting_8106_Count++;
                                                dashboardPeza_ApprovedWaiting_Pullout_Count += long.Parse(entity.Warehouse_OverallTotalQty.Trim());
                                                dashboardPeza_ApprovedWaiting_Remaining_Count += long.Parse(entity.Warehouse_TotalActualQuantity.Trim());
                                            }

                                            if (entity.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS")
                                            {
                                                if (oldLOA8106 != entity.Old8106.Trim())
                                                {
                                                    dashboardPeza_DeliveredPending_8106_Count++;
                                                }
                                                //dashboardPeza_DeliveredPending_8106_Count++;
                                                dashboardPeza_DeliveredPending_Pullout_Count += long.Parse(entity.Warehouse_OverallTotalQty.Trim());
                                                dashboardPeza_DeliveredPending_Remaining_Count += long.Parse(entity.Warehouse_TotalActualQuantity.Trim());
                                            }

                                            if (entity.StatAll.ToUpper() == "DELIVERED")
                                            {
                                                if (oldLOA8106 != entity.Old8106.Trim())
                                                {
                                                    dashboardPeza_Delivered_8106_Count++;
                                                }
                                                //dashboardPeza_Delivered_8106_Count++;
                                                dashboardPeza_Delivered_Pullout_Count += long.Parse(entity.Warehouse_OverallTotalQty.Trim());
                                                dashboardPeza_Delivered_Remaining_Count += long.Parse(entity.Warehouse_TotalActualQuantity.Trim());
                                            }

                                            if (oldLOA8106 != entity.Warehouse_LOA8106.Trim() && entity.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" || entity.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" || entity.StatAll.ToUpper() == "DELIVERED" || entity.StatAll.ToUpper() == "CLOSED")
                                            {
                                                totalPezaAll++;
                                            }

                                        }

                                        //NON PEZA
                                        if (entity.Warehouse_PezaNonPeza == "2" && (entity.Warehouse_LOANo == "40" || entity.Warehouse_LOANo == "41"))
                                        {

                                            if (entity.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY")
                                            {
                                                if (oldLOA8106 != entity.Old8106.Trim())
                                                {
                                                    dashboardNonPeza_ApprovedWaiting_8106_Count++;
                                                }
                                                //dashboardNonPeza_ApprovedWaiting_8106_Count++;
                                                dashboardNonPeza_ApprovedWaiting_Pullout_Count += long.Parse(entity.Warehouse_OverallTotalQty.Trim());
                                                dashboardNonPeza_ApprovedWaiting_Remaining_Count += long.Parse(entity.Warehouse_TotalActualQuantity.Trim());
                                            }

                                            if (entity.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS")
                                            {
                                                if (oldLOA8106 != entity.Old8106.Trim())
                                                {
                                                    dashboardNonPeza_DeliveredPending_8106_Count++;
                                                }
                                                //dashboardNonPeza_DeliveredPending_8106_Count++;
                                                dashboardNonPeza_DeliveredPending_Pullout_Count += long.Parse(entity.Warehouse_OverallTotalQty.Trim());
                                                dashboardNonPeza_DeliveredPending_Remaining_Count += long.Parse(entity.Warehouse_TotalActualQuantity.Trim());
                                            }

                                            if (entity.StatAll.ToUpper() == "DELIVERED")
                                            {
                                                if (oldLOA8106 != entity.Old8106.Trim())
                                                {
                                                    dashboardNonPeza_Delivered_8106_Count++;
                                                }
                                                //dashboardNonPeza_Delivered_8106_Count++;
                                                dashboardNonPeza_Delivered_Pullout_Count += long.Parse(entity.Warehouse_OverallTotalQty.Trim());
                                                dashboardNonPeza_Delivered_Remaining_Count += long.Parse(entity.Warehouse_TotalActualQuantity.Trim());
                                            }

                                            if (oldLOA8106 != entity.Warehouse_LOA8106.Trim() && entity.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" || entity.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" || entity.StatAll.ToUpper() == "DELIVERED" || entity.StatAll.ToUpper() == "CLOSED")
                                            {
                                                totalNonPezaAll++;
                                            }

                                        }

                                        oldLOA8106 = entity.Old8106.Trim();

                                    }


                                    //PAST 1 MONTH PEZA

                                    //lb1.Text = dashboardPeza_ApprovedWaiting_8106_Count.ToString("#,###");
                                    //dashboardPeza_ApprovedWaiting_8106_Count = listWarehouse.Where(itm => itm.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" && itm.Warehouse_PezaNonPeza == "1").ToList().Select(itm2 => itm2.Warehouse_LOA8106).Distinct().Count();
                                    dashboardPeza_ApprovedWaiting_8106_Count = listWarehouse.Where(itm => itm.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" && itm.Warehouse_PezaNonPeza == "1").ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count();
                                    lb1.Text = dashboardPeza_ApprovedWaiting_8106_Count.ToString("#,###");
                                    lb2.Text = dashboardPeza_ApprovedWaiting_Pullout_Count.ToString("#,###");
                                    lb3.Text = (dashboardPeza_ApprovedWaiting_Pullout_Count - dashboardPeza_ApprovedWaiting_Remaining_Count).ToString("#,###");

                                    //dashboardPeza_DeliveredPending_8106_Count = listWarehouse.Where(itm => itm.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" && itm.Warehouse_PezaNonPeza == "1").ToList().Select(itm2 => itm2.Warehouse_LOA8106).Distinct().Count();
                                    dashboardPeza_DeliveredPending_8106_Count = listWarehouse.Where(itm => itm.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" && itm.Warehouse_PezaNonPeza == "1").ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count();
                                    lb10.Text = dashboardPeza_DeliveredPending_8106_Count.ToString("#,###");
                                    lb11.Text = dashboardPeza_DeliveredPending_Pullout_Count.ToString("#,###");
                                    lb12.Text = (dashboardPeza_DeliveredPending_Pullout_Count - dashboardPeza_DeliveredPending_Remaining_Count).ToString("#,###");

                                    //dashboardPeza_Delivered_8106_Count = listWarehouse.Where(itm => itm.StatAll.ToUpper() == "DELIVERED" && itm.Warehouse_PezaNonPeza == "1").ToList().Select(itm2 => itm2.Warehouse_LOA8106).Distinct().Count();
                                    dashboardPeza_Delivered_8106_Count = listWarehouse.Where(itm => itm.StatAll.ToUpper() == "DELIVERED" && itm.Warehouse_PezaNonPeza == "1").ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count();
                                    lb19.Text = dashboardPeza_Delivered_8106_Count.ToString("#,###");
                                    lb20.Text = dashboardPeza_Delivered_Pullout_Count.ToString("#,###");
                                    lb21.Text = (dashboardPeza_Delivered_Pullout_Count - dashboardPeza_Delivered_Remaining_Count).ToString("#,###");



                                    //PAST 1 MONTH NON PEZA

                                    //dashboardNonPeza_ApprovedWaiting_8106_Count = listWarehouse.Where(itm => itm.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" && itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList().Select(itm2 => itm2.Warehouse_LOA8106).Distinct().Count();
                                    dashboardNonPeza_ApprovedWaiting_8106_Count = listWarehouse.Where(itm => itm.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" && itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count();
                                    lb37.Text = dashboardNonPeza_ApprovedWaiting_8106_Count.ToString("#,###");
                                    lb38.Text = dashboardNonPeza_ApprovedWaiting_Pullout_Count.ToString("#,###");
                                    lb39.Text = (dashboardNonPeza_ApprovedWaiting_Pullout_Count - dashboardNonPeza_ApprovedWaiting_Remaining_Count).ToString("#,###");

                                    //dashboardNonPeza_DeliveredPending_8106_Count = listWarehouse.Where(itm => itm.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" && itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList().Select(itm2 => itm2.Warehouse_LOA8106).Distinct().Count();
                                    dashboardNonPeza_DeliveredPending_8106_Count = listWarehouse.Where(itm => itm.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" && itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count();
                                    lb46.Text = dashboardNonPeza_DeliveredPending_8106_Count.ToString("#,###");
                                    lb47.Text = dashboardNonPeza_DeliveredPending_Pullout_Count.ToString("#,###");
                                    lb48.Text = (dashboardNonPeza_DeliveredPending_Pullout_Count - dashboardNonPeza_DeliveredPending_Remaining_Count).ToString("#,###");

                                    //dashboardNonPeza_Delivered_8106_Count = listWarehouse.Where(itm => itm.StatAll.ToUpper() == "DELIVERED" && itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList().Select(itm2 => itm2.Warehouse_LOA8106).Distinct().Count();
                                    dashboardNonPeza_Delivered_8106_Count = listWarehouse.Where(itm => itm.StatAll.ToUpper() == "DELIVERED" && itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count();
                                    lb55.Text = dashboardNonPeza_Delivered_8106_Count.ToString("#,###");
                                    lb56.Text = dashboardNonPeza_Delivered_Pullout_Count.ToString("#,###");
                                    lb57.Text = (dashboardNonPeza_Delivered_Pullout_Count - dashboardNonPeza_Delivered_Remaining_Count).ToString("#,###");




                                }
                            }

                            //TOTALS PEZA
                            lb28.Text = (dashboardPeza_ApprovedWaiting_8106_Count + dashboardPeza_DeliveredPending_8106_Count + dashboardPeza_Delivered_8106_Count).ToString("#,###");
                            lb29.Text = (dashboardPeza_ApprovedWaiting_Pullout_Count + dashboardPeza_DeliveredPending_Pullout_Count + dashboardPeza_Delivered_Pullout_Count).ToString("#,###");
                            lb30.Text = ((dashboardPeza_ApprovedWaiting_Pullout_Count - dashboardPeza_ApprovedWaiting_Remaining_Count) + (dashboardPeza_DeliveredPending_Pullout_Count - dashboardPeza_DeliveredPending_Remaining_Count) + (dashboardPeza_Delivered_Pullout_Count - dashboardPeza_Delivered_Remaining_Count)).ToString("#,###");
                            totalPeza += (dashboardPeza_ApprovedWaiting_8106_Count + dashboardPeza_DeliveredPending_8106_Count + dashboardPeza_Delivered_8106_Count);

                            //TOTAL NON PEZA
                            lb64.Text = (dashboardNonPeza_ApprovedWaiting_8106_Count + dashboardNonPeza_DeliveredPending_8106_Count + dashboardNonPeza_Delivered_8106_Count).ToString("#,###");
                            lb65.Text = (dashboardNonPeza_ApprovedWaiting_Pullout_Count + dashboardNonPeza_DeliveredPending_Pullout_Count + dashboardNonPeza_Delivered_Pullout_Count).ToString("#,###");
                            lb66.Text = ((dashboardNonPeza_ApprovedWaiting_Pullout_Count - dashboardNonPeza_ApprovedWaiting_Remaining_Count) + (dashboardNonPeza_DeliveredPending_Pullout_Count - dashboardNonPeza_DeliveredPending_Remaining_Count) + (dashboardNonPeza_Delivered_Pullout_Count - dashboardNonPeza_Delivered_Remaining_Count)).ToString("#,###");
                            totalNonPeza += (dashboardNonPeza_ApprovedWaiting_8106_Count + dashboardNonPeza_DeliveredPending_8106_Count + dashboardNonPeza_Delivered_8106_Count);


                            dashboardPeza_ApprovedWaiting_8106_Count = 0;
                            dashboardPeza_ApprovedWaiting_Pullout_Count = 0;
                            dashboardPeza_ApprovedWaiting_Remaining_Count = 0;

                            dashboardPeza_DeliveredPending_8106_Count = 0;
                            dashboardPeza_DeliveredPending_Pullout_Count = 0;
                            dashboardPeza_DeliveredPending_Remaining_Count = 0;

                            dashboardPeza_Delivered_8106_Count = 0;
                            dashboardPeza_Delivered_Pullout_Count = 0;
                            dashboardPeza_Delivered_Remaining_Count = 0;

                            dashboardNonPeza_ApprovedWaiting_8106_Count = 0;
                            dashboardNonPeza_ApprovedWaiting_Pullout_Count = 0;
                            dashboardNonPeza_ApprovedWaiting_Remaining_Count = 0;

                            dashboardNonPeza_DeliveredPending_8106_Count = 0;
                            dashboardNonPeza_DeliveredPending_Pullout_Count = 0;
                            dashboardNonPeza_DeliveredPending_Remaining_Count = 0;

                            dashboardNonPeza_Delivered_8106_Count = 0;
                            dashboardNonPeza_Delivered_Pullout_Count = 0;
                            dashboardNonPeza_Delivered_Remaining_Count = 0;

                            oldLOA8106 = string.Empty;

                            //PAST 2 MONTH ==========================================================================================================================================================================
                            List<Entities_SRF_RequestEntry> listWarehouse2Months = new List<Entities_SRF_RequestEntry>();
                            listWarehouse2Months = BLL_SRF.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange("", DateTime.Today.AddDays(-120).ToString("MM/dd/yyyy"), DateTime.Today.AddDays(-60).ToString("MM/dd/yyyy")).Where(itm2 => !string.IsNullOrEmpty(itm2.Warehouse_LOA8106)).OrderBy(itm => itm.Warehouse_LOA8106).ToList();
                            //listWarehouse2Months = BLL_SRF.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange_DashboardCount(DateTime.Today.AddDays(-365).ToString("MM/dd/yyyy"), DateTime.Today.AddDays(-120).ToString("MM/dd/yyyy"));


                            if (listWarehouse2Months != null)
                            {
                                if (listWarehouse2Months.Count > 0)
                                {

                                    foreach (Entities_SRF_RequestEntry entity in listWarehouse2Months)
                                    {

                                        //PEZA
                                        if (entity.Warehouse_PezaNonPeza == "1")
                                        {
                                            if (entity.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY")
                                            {
                                                if (oldLOA8106 != entity.Old8106.Trim())
                                                {
                                                    dashboardPeza_ApprovedWaiting_8106_Count++;
                                                }
                                                //dashboardPeza_ApprovedWaiting_8106_Count++;
                                                dashboardPeza_ApprovedWaiting_Pullout_Count += long.Parse(entity.Warehouse_OverallTotalQty.Trim());
                                                dashboardPeza_ApprovedWaiting_Remaining_Count += long.Parse(entity.Warehouse_TotalActualQuantity.Trim());
                                            }

                                            if (entity.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS")
                                            {
                                                if (oldLOA8106 != entity.Old8106.Trim())
                                                {
                                                    dashboardPeza_DeliveredPending_8106_Count++;
                                                }
                                                //dashboardPeza_DeliveredPending_8106_Count++;
                                                dashboardPeza_DeliveredPending_Pullout_Count += long.Parse(entity.Warehouse_OverallTotalQty.Trim());
                                                dashboardPeza_DeliveredPending_Remaining_Count += long.Parse(entity.Warehouse_TotalActualQuantity.Trim());
                                            }

                                            if (entity.StatAll.ToUpper() == "DELIVERED")
                                            {
                                                if (oldLOA8106 != entity.Old8106.Trim())
                                                {
                                                    dashboardPeza_Delivered_8106_Count++;
                                                }
                                                //dashboardPeza_Delivered_8106_Count++;
                                                dashboardPeza_Delivered_Pullout_Count += long.Parse(entity.Warehouse_OverallTotalQty.Trim());
                                                dashboardPeza_Delivered_Remaining_Count += long.Parse(entity.Warehouse_TotalActualQuantity.Trim());
                                            }

                                            if (oldLOA8106 != entity.Warehouse_LOA8106.Trim() && entity.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" || entity.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" || entity.StatAll.ToUpper() == "DELIVERED" || entity.StatAll.ToUpper() == "CLOSED")
                                            {
                                                totalPezaAll++;
                                            }

                                        }

                                        //NON PEZA
                                        if (entity.Warehouse_PezaNonPeza == "2" && (entity.Warehouse_LOANo == "40" || entity.Warehouse_LOANo == "41"))
                                        {

                                            if (entity.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY")
                                            {
                                                if (oldLOA8106 != entity.Old8106.Trim())
                                                {
                                                    dashboardNonPeza_ApprovedWaiting_8106_Count++;
                                                }
                                                //dashboardNonPeza_ApprovedWaiting_8106_Count++;
                                                dashboardNonPeza_ApprovedWaiting_Pullout_Count += long.Parse(entity.Warehouse_OverallTotalQty.Trim());
                                                dashboardNonPeza_ApprovedWaiting_Remaining_Count += long.Parse(entity.Warehouse_TotalActualQuantity.Trim());
                                            }

                                            if (entity.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS")
                                            {
                                                if (oldLOA8106 != entity.Old8106.Trim())
                                                {
                                                    dashboardNonPeza_DeliveredPending_8106_Count++;
                                                }
                                                //dashboardNonPeza_DeliveredPending_8106_Count++;
                                                dashboardNonPeza_DeliveredPending_Pullout_Count += long.Parse(entity.Warehouse_OverallTotalQty.Trim());
                                                dashboardNonPeza_DeliveredPending_Remaining_Count += long.Parse(entity.Warehouse_TotalActualQuantity.Trim());
                                            }

                                            if (entity.StatAll.ToUpper() == "DELIVERED")
                                            {
                                                if (oldLOA8106 != entity.Old8106.Trim())
                                                {
                                                    dashboardNonPeza_Delivered_8106_Count++;
                                                }
                                                //dashboardNonPeza_Delivered_8106_Count++;
                                                dashboardNonPeza_Delivered_Pullout_Count += long.Parse(entity.Warehouse_OverallTotalQty.Trim());
                                                dashboardNonPeza_Delivered_Remaining_Count += long.Parse(entity.Warehouse_TotalActualQuantity.Trim());
                                            }

                                            if (oldLOA8106 != entity.Old8106 && entity.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" || entity.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" || entity.StatAll.ToUpper() == "DELIVERED" || entity.StatAll.ToUpper() == "CLOSED")
                                            {
                                                totalNonPezaAll++;
                                            }

                                        }

                                        oldLOA8106 = entity.Old8106.Trim();

                                    }


                                    //PAST 2 MONTH PEZA

                                    //dashboardPeza_ApprovedWaiting_8106_Count = listWarehouse2Months.Where(itm => itm.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" && itm.Warehouse_PezaNonPeza == "1").ToList().Select(itm2 => itm2.Warehouse_LOA8106).Distinct().Count();
                                    dashboardPeza_ApprovedWaiting_8106_Count = listWarehouse2Months.Where(itm => itm.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" && itm.Warehouse_PezaNonPeza == "1").ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count();
                                    lb4.Text = dashboardPeza_ApprovedWaiting_8106_Count.ToString("#,###");
                                    lb5.Text = dashboardPeza_ApprovedWaiting_Pullout_Count.ToString("#,###");
                                    lb6.Text = (dashboardPeza_ApprovedWaiting_Pullout_Count - dashboardPeza_ApprovedWaiting_Remaining_Count).ToString("#,###");

                                    //dashboardPeza_DeliveredPending_8106_Count = listWarehouse2Months.Where(itm => itm.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" && itm.Warehouse_PezaNonPeza == "1").ToList().Select(itm2 => itm2.Warehouse_LOA8106).Distinct().Count();
                                    dashboardPeza_DeliveredPending_8106_Count = listWarehouse2Months.Where(itm => itm.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" && itm.Warehouse_PezaNonPeza == "1").ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count();
                                    lb13.Text = dashboardPeza_DeliveredPending_8106_Count.ToString("#,###");
                                    lb14.Text = dashboardPeza_DeliveredPending_Pullout_Count.ToString("#,###");
                                    lb15.Text = (dashboardPeza_DeliveredPending_Pullout_Count - dashboardPeza_DeliveredPending_Remaining_Count).ToString("#,###");

                                    //dashboardPeza_Delivered_8106_Count = listWarehouse2Months.Where(itm => itm.StatAll.ToUpper() == "DELIVERED" && itm.Warehouse_PezaNonPeza == "1").ToList().Select(itm2 => itm2.Warehouse_LOA8106).Distinct().Count();
                                    dashboardPeza_Delivered_8106_Count = listWarehouse2Months.Where(itm => itm.StatAll.ToUpper() == "DELIVERED" && itm.Warehouse_PezaNonPeza == "1").ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count();
                                    lb22.Text = dashboardPeza_Delivered_8106_Count.ToString("#,###");
                                    lb23.Text = dashboardPeza_Delivered_Pullout_Count.ToString("#,###");
                                    lb24.Text = (dashboardPeza_Delivered_Pullout_Count - dashboardPeza_Delivered_Remaining_Count).ToString("#,###");


                                    //PAST 2 MONTH NON PEZA

                                    //dashboardNonPeza_ApprovedWaiting_8106_Count = listWarehouse2Months.Where(itm => itm.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" && itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList().Select(itm2 => itm2.Warehouse_LOA8106).Distinct().Count();
                                    dashboardNonPeza_ApprovedWaiting_8106_Count = listWarehouse2Months.Where(itm => itm.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" && itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count();
                                    lb40.Text = dashboardNonPeza_ApprovedWaiting_8106_Count.ToString("#,###");
                                    lb41.Text = dashboardNonPeza_ApprovedWaiting_Pullout_Count.ToString("#,###");
                                    lb42.Text = (dashboardNonPeza_ApprovedWaiting_Pullout_Count - dashboardNonPeza_ApprovedWaiting_Remaining_Count).ToString("#,###");

                                    //dashboardNonPeza_DeliveredPending_8106_Count = listWarehouse2Months.Where(itm => itm.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" && itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList().Select(itm2 => itm2.Warehouse_LOA8106).Distinct().Count();
                                    dashboardNonPeza_DeliveredPending_8106_Count = listWarehouse2Months.Where(itm => itm.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" && itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count();
                                    lb49.Text = dashboardNonPeza_DeliveredPending_8106_Count.ToString("#,###");
                                    lb50.Text = dashboardNonPeza_DeliveredPending_Pullout_Count.ToString("#,###");
                                    lb51.Text = (dashboardNonPeza_DeliveredPending_Pullout_Count - dashboardNonPeza_DeliveredPending_Remaining_Count).ToString("#,###");

                                    //dashboardNonPeza_Delivered_8106_Count = listWarehouse2Months.Where(itm => itm.StatAll.ToUpper() == "DELIVERED" && itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList().Select(itm2 => itm2.Warehouse_LOA8106).Distinct().Count();
                                    dashboardNonPeza_Delivered_8106_Count = listWarehouse2Months.Where(itm => itm.StatAll.ToUpper() == "DELIVERED" && itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count();
                                    lb58.Text = dashboardNonPeza_Delivered_8106_Count.ToString("#,###");
                                    lb59.Text = dashboardNonPeza_Delivered_Pullout_Count.ToString("#,###");
                                    lb60.Text = (dashboardNonPeza_Delivered_Pullout_Count - dashboardNonPeza_Delivered_Remaining_Count).ToString("#,###");

                                }
                            }

                            //TOTALS PEZA
                            lb31.Text = (dashboardPeza_ApprovedWaiting_8106_Count + dashboardPeza_DeliveredPending_8106_Count + dashboardPeza_Delivered_8106_Count).ToString("#,###");
                            lb32.Text = (dashboardPeza_ApprovedWaiting_Pullout_Count + dashboardPeza_DeliveredPending_Pullout_Count + dashboardPeza_Delivered_Pullout_Count).ToString("#,###");
                            lb33.Text = ((dashboardPeza_ApprovedWaiting_Pullout_Count - dashboardPeza_ApprovedWaiting_Remaining_Count) + (dashboardPeza_DeliveredPending_Pullout_Count - dashboardPeza_DeliveredPending_Remaining_Count) + (dashboardPeza_Delivered_Pullout_Count - dashboardPeza_Delivered_Remaining_Count)).ToString("#,###");
                            totalPeza += (dashboardPeza_ApprovedWaiting_8106_Count + dashboardPeza_DeliveredPending_8106_Count + dashboardPeza_Delivered_8106_Count);

                            //TOTAL NON PEZA
                            lb67.Text = (dashboardNonPeza_ApprovedWaiting_8106_Count + dashboardNonPeza_DeliveredPending_8106_Count + dashboardNonPeza_Delivered_8106_Count).ToString("#,###");
                            lb68.Text = (dashboardNonPeza_ApprovedWaiting_Pullout_Count + dashboardNonPeza_DeliveredPending_Pullout_Count + dashboardNonPeza_Delivered_Pullout_Count).ToString("#,###");
                            lb69.Text = ((dashboardNonPeza_ApprovedWaiting_Pullout_Count - dashboardNonPeza_ApprovedWaiting_Remaining_Count) + (dashboardNonPeza_DeliveredPending_Pullout_Count - dashboardNonPeza_DeliveredPending_Remaining_Count) + (dashboardNonPeza_Delivered_Pullout_Count - dashboardNonPeza_Delivered_Remaining_Count)).ToString("#,###");
                            totalNonPeza += (dashboardNonPeza_ApprovedWaiting_8106_Count + dashboardNonPeza_DeliveredPending_8106_Count + dashboardNonPeza_Delivered_8106_Count);


                            dashboardPeza_ApprovedWaiting_8106_Count = 0;
                            dashboardPeza_ApprovedWaiting_Pullout_Count = 0;
                            dashboardPeza_ApprovedWaiting_Remaining_Count = 0;

                            dashboardPeza_DeliveredPending_8106_Count = 0;
                            dashboardPeza_DeliveredPending_Pullout_Count = 0;
                            dashboardPeza_DeliveredPending_Remaining_Count = 0;

                            dashboardPeza_Delivered_8106_Count = 0;
                            dashboardPeza_Delivered_Pullout_Count = 0;
                            dashboardPeza_Delivered_Remaining_Count = 0;

                            dashboardNonPeza_ApprovedWaiting_8106_Count = 0;
                            dashboardNonPeza_ApprovedWaiting_Pullout_Count = 0;
                            dashboardNonPeza_ApprovedWaiting_Remaining_Count = 0;

                            dashboardNonPeza_DeliveredPending_8106_Count = 0;
                            dashboardNonPeza_DeliveredPending_Pullout_Count = 0;
                            dashboardNonPeza_DeliveredPending_Remaining_Count = 0;

                            dashboardNonPeza_Delivered_8106_Count = 0;
                            dashboardNonPeza_Delivered_Pullout_Count = 0;
                            dashboardNonPeza_Delivered_Remaining_Count = 0;

                            oldLOA8106 = string.Empty;

                            string test = string.Empty;

                            //PAST 3 MONTHS & MORE ==========================================================================================================================================================================
                            List<Entities_SRF_RequestEntry> listWarehouse3Months = new List<Entities_SRF_RequestEntry>();
                            //listWarehouse3Months = BLL_SRF.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange("", DateTime.Today.AddDays(-365).ToString("MM/dd/yyyy"), DateTime.Today.AddDays(-90).ToString("MM/dd/yyyy")).Where(itm2 => !string.IsNullOrEmpty(itm2.Warehouse_LOA8106)).OrderBy(itm => itm.Warehouse_LOA8106).ToList();
                            listWarehouse3Months = BLL_SRF.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange("", "07/01/2023", DateTime.Today.AddDays(-120).ToString("MM/dd/yyyy")).Where(itm2 => !string.IsNullOrEmpty(itm2.Warehouse_LOA8106)).OrderBy(itm => itm.Warehouse_LOA8106).ToList();

                            if (listWarehouse3Months != null)
                            {
                                if (listWarehouse3Months.Count > 0)
                                {

                                    foreach (Entities_SRF_RequestEntry entity in listWarehouse3Months)
                                    {

                                        //PEZA
                                        if (entity.Warehouse_PezaNonPeza == "1")
                                        {
                                            if (entity.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY")
                                            {

                                                if (oldLOA8106 != entity.Old8106.Trim())
                                                {
                                                    dashboardPeza_ApprovedWaiting_8106_Count++;
                                                }
                                                //dashboardPeza_ApprovedWaiting_8106_Count++;
                                                dashboardPeza_ApprovedWaiting_Pullout_Count += long.Parse(entity.Warehouse_OverallTotalQty.Trim());
                                                dashboardPeza_ApprovedWaiting_Remaining_Count += long.Parse(entity.Warehouse_TotalActualQuantity.Trim());
                                            }

                                            if (entity.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS")
                                            {
                                                if (oldLOA8106 != entity.Old8106.Trim())
                                                {
                                                    dashboardPeza_DeliveredPending_8106_Count++;

                                                }
                                                //dashboardPeza_DeliveredPending_8106_Count++;
                                                dashboardPeza_DeliveredPending_Pullout_Count += long.Parse(entity.Warehouse_OverallTotalQty.Trim());
                                                dashboardPeza_DeliveredPending_Remaining_Count += long.Parse(entity.Warehouse_TotalActualQuantity.Trim());

                                                //test += entity.Warehouse_CtrlNo + ",";
                                            }

                                            if (entity.StatAll.ToUpper() == "DELIVERED")
                                            {
                                                if (oldLOA8106 != entity.Old8106.Trim())
                                                {
                                                    dashboardPeza_Delivered_8106_Count++;
                                                }
                                                //dashboardPeza_Delivered_8106_Count++;
                                                dashboardPeza_Delivered_Pullout_Count += long.Parse(entity.Warehouse_OverallTotalQty.Trim());
                                                dashboardPeza_Delivered_Remaining_Count += long.Parse(entity.Warehouse_TotalActualQuantity.Trim());
                                            }

                                            if (oldLOA8106 != entity.Warehouse_LOA8106.Trim() && entity.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" || entity.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" || entity.StatAll.ToUpper() == "DELIVERED" || entity.StatAll.ToUpper() == "CLOSED")
                                            {
                                                totalPezaAll++;
                                            }

                                        }

                                        //NON PEZA
                                        if (entity.Warehouse_PezaNonPeza == "2" && (entity.Warehouse_LOANo == "40" || entity.Warehouse_LOANo == "41"))
                                        {

                                            if (entity.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY")
                                            {

                                                if (entity.Old8106.Trim() != oldLOA8106)
                                                {
                                                    dashboardNonPeza_ApprovedWaiting_8106_Count++;
                                                }
                                                //dashboardNonPeza_ApprovedWaiting_8106_Count++;
                                                dashboardNonPeza_ApprovedWaiting_Pullout_Count += long.Parse(entity.Warehouse_OverallTotalQty.Trim());
                                                dashboardNonPeza_ApprovedWaiting_Remaining_Count += long.Parse(entity.Warehouse_TotalActualQuantity.Trim());
                                            }

                                            if (entity.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS")
                                            {
                                                if (entity.Old8106.Trim() != oldLOA8106)
                                                {
                                                    dashboardNonPeza_DeliveredPending_8106_Count++;
                                                    test += entity.Warehouse_CtrlNo + ",";
                                                }
                                                //dashboardNonPeza_DeliveredPending_8106_Count++;
                                                dashboardNonPeza_DeliveredPending_Pullout_Count += long.Parse(entity.Warehouse_OverallTotalQty.Trim());
                                                dashboardNonPeza_DeliveredPending_Remaining_Count += long.Parse(entity.Warehouse_TotalActualQuantity.Trim());


                                            }

                                            if (entity.StatAll.ToUpper() == "DELIVERED")
                                            {
                                                if (entity.Old8106.Trim() != oldLOA8106)
                                                {
                                                    dashboardNonPeza_Delivered_8106_Count++;
                                                }
                                                //dashboardNonPeza_Delivered_8106_Count++;
                                                dashboardNonPeza_Delivered_Pullout_Count += long.Parse(entity.Warehouse_OverallTotalQty.Trim());
                                                dashboardNonPeza_Delivered_Remaining_Count += long.Parse(entity.Warehouse_TotalActualQuantity.Trim());
                                            }

                                            if (oldLOA8106 != entity.Warehouse_LOA8106.Trim() && entity.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" || entity.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" || entity.StatAll.ToUpper() == "DELIVERED" || entity.StatAll.ToUpper() == "CLOSED")
                                            {
                                                totalNonPezaAll++;
                                            }


                                        }


                                        oldLOA8106 = entity.Old8106.Trim();


                                    }


                                    //PAST 3 MONTH PEZA

                                    //dashboardPeza_ApprovedWaiting_8106_Count = listWarehouse3Months.Where(itm => itm.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" && itm.Warehouse_PezaNonPeza == "1").ToList().Select(itm2 => itm2.Warehouse_LOA8106).Distinct().Count();
                                    dashboardPeza_ApprovedWaiting_8106_Count = listWarehouse3Months.Where(itm => itm.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" && itm.Warehouse_PezaNonPeza == "1").ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count();
                                    lb7.Text = dashboardPeza_ApprovedWaiting_8106_Count.ToString("#,###");
                                    lb8.Text = dashboardPeza_ApprovedWaiting_Pullout_Count.ToString("#,###");
                                    lb9.Text = (dashboardPeza_ApprovedWaiting_Pullout_Count - dashboardPeza_ApprovedWaiting_Remaining_Count).ToString("#,###");

                                    //dashboardPeza_DeliveredPending_8106_Count = listWarehouse3Months.Where(itm => itm.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" && itm.Warehouse_PezaNonPeza == "1").ToList().Select(itm2 => itm2.Warehouse_LOA8106).Distinct().Count();
                                    dashboardPeza_DeliveredPending_8106_Count = listWarehouse3Months.Where(itm => itm.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" && itm.Warehouse_PezaNonPeza == "1").ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count();
                                    lb16.Text = dashboardPeza_DeliveredPending_8106_Count.ToString("#,###");
                                    lb17.Text = dashboardPeza_DeliveredPending_Pullout_Count.ToString("#,###");
                                    lb18.Text = (dashboardPeza_DeliveredPending_Pullout_Count - dashboardPeza_DeliveredPending_Remaining_Count).ToString("#,###");

                                    //dashboardPeza_Delivered_8106_Count = listWarehouse3Months.Where(itm => itm.StatAll.ToUpper() == "DELIVERED" && itm.Warehouse_PezaNonPeza == "1").ToList().Select(itm2 => itm2.Warehouse_LOA8106).Distinct().Count();
                                    dashboardPeza_Delivered_8106_Count = listWarehouse3Months.Where(itm => itm.StatAll.ToUpper() == "DELIVERED" && itm.Warehouse_PezaNonPeza == "1").ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count();
                                    lb25.Text = dashboardPeza_Delivered_8106_Count.ToString("#,###");
                                    lb26.Text = dashboardPeza_Delivered_Pullout_Count.ToString("#,###");
                                    lb27.Text = (dashboardPeza_Delivered_Pullout_Count - dashboardPeza_Delivered_Remaining_Count).ToString("#,###");


                                    //PAST 3 MONTH NON PEZA

                                    //dashboardNonPeza_ApprovedWaiting_8106_Count = listWarehouse3Months.Where(itm => itm.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" && itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList().Select(itm2 => itm2.Warehouse_LOA8106).Distinct().Count();
                                    dashboardNonPeza_ApprovedWaiting_8106_Count = listWarehouse3Months.Where(itm => itm.StatAll.ToUpper() == "APPROVED / WAITING FOR DELIVERY" && itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count();
                                    lb43.Text = dashboardNonPeza_ApprovedWaiting_8106_Count.ToString("#,###");
                                    lb44.Text = dashboardNonPeza_ApprovedWaiting_Pullout_Count.ToString("#,###");
                                    lb45.Text = (dashboardNonPeza_ApprovedWaiting_Pullout_Count - dashboardNonPeza_ApprovedWaiting_Remaining_Count).ToString("#,###");

                                    //dashboardNonPeza_DeliveredPending_8106_Count = listWarehouse3Months.Where(itm => itm.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" && itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList().Select(itm2 => itm2.Warehouse_LOA8106).Distinct().Count();
                                    dashboardNonPeza_DeliveredPending_8106_Count = listWarehouse3Months.Where(itm => itm.StatAll.ToUpper() == "DELIVERED WITH PENDING ITEMS" && itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count();
                                    lb52.Text = dashboardNonPeza_DeliveredPending_8106_Count.ToString("#,###");
                                    lb53.Text = dashboardNonPeza_DeliveredPending_Pullout_Count.ToString("#,###");
                                    lb54.Text = (dashboardNonPeza_DeliveredPending_Pullout_Count - dashboardNonPeza_DeliveredPending_Remaining_Count).ToString("#,###");

                                    //dashboardNonPeza_Delivered_8106_Count = listWarehouse3Months.Where(itm => itm.StatAll.ToUpper() == "DELIVERED" && itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList().Select(itm2 => itm2.Warehouse_LOA8106).Distinct().Count();
                                    dashboardNonPeza_Delivered_8106_Count = listWarehouse3Months.Where(itm => itm.StatAll.ToUpper() == "DELIVERED" && itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count();
                                    lb61.Text = dashboardNonPeza_Delivered_8106_Count.ToString("#,###");
                                    lb62.Text = dashboardNonPeza_Delivered_Pullout_Count.ToString("#,###");
                                    lb63.Text = (dashboardNonPeza_Delivered_Pullout_Count - dashboardNonPeza_Delivered_Remaining_Count).ToString("#,###");

                                }
                            }


                            //TOTALS PEZA
                            lb34.Text = (dashboardPeza_ApprovedWaiting_8106_Count + dashboardPeza_DeliveredPending_8106_Count + dashboardPeza_Delivered_8106_Count).ToString("#,###");
                            lb35.Text = (dashboardPeza_ApprovedWaiting_Pullout_Count + dashboardPeza_DeliveredPending_Pullout_Count + dashboardPeza_Delivered_Pullout_Count).ToString("#,###");
                            lb36.Text = ((dashboardPeza_ApprovedWaiting_Pullout_Count - dashboardPeza_ApprovedWaiting_Remaining_Count) + (dashboardPeza_DeliveredPending_Pullout_Count - dashboardPeza_DeliveredPending_Remaining_Count) + (dashboardPeza_Delivered_Pullout_Count - dashboardPeza_Delivered_Remaining_Count)).ToString("#,###");
                            totalPeza += (dashboardPeza_ApprovedWaiting_8106_Count + dashboardPeza_DeliveredPending_8106_Count + dashboardPeza_Delivered_8106_Count);

                            //TOTAL NON PEZA
                            lb70.Text = (dashboardNonPeza_ApprovedWaiting_8106_Count + dashboardNonPeza_DeliveredPending_8106_Count + dashboardNonPeza_Delivered_8106_Count).ToString("#,###");
                            lb71.Text = (dashboardNonPeza_ApprovedWaiting_Pullout_Count + dashboardNonPeza_DeliveredPending_Pullout_Count + dashboardNonPeza_Delivered_Pullout_Count).ToString("#,###");
                            lb72.Text = ((dashboardNonPeza_ApprovedWaiting_Pullout_Count - dashboardNonPeza_ApprovedWaiting_Remaining_Count) + (dashboardNonPeza_DeliveredPending_Pullout_Count - dashboardNonPeza_DeliveredPending_Remaining_Count) + (dashboardNonPeza_Delivered_Pullout_Count - dashboardNonPeza_Delivered_Remaining_Count)).ToString("#,###");
                            totalNonPeza += (dashboardNonPeza_ApprovedWaiting_8106_Count + dashboardNonPeza_DeliveredPending_8106_Count + dashboardNonPeza_Delivered_8106_Count);

                            ///////////////////////////////////////////////////////////////////////////////////////////////////
                            lbNonPezaTotal1.Text = totalNonPeza.ToString("#,###");

                            List<Entities_SRF_RequestEntry> listTotalNonPezaAll = new List<Entities_SRF_RequestEntry>();
                            listTotalNonPezaAll = BLL_SRF.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange("", "07/01/2023", DateTime.Today.AddDays(-30).ToString("MM/dd/yyyy")).Where(itm => itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList();
                            lbNonPezaTotal2.Text = listTotalNonPezaAll.Where(itm => itm.Warehouse_PezaNonPeza == "2" && (itm.Warehouse_LOANo == "40" || itm.Warehouse_LOANo == "41")).ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count().ToString("#,###");
                            ///////////////////////////////////////////////////////////////////////////////////////////////////


                            ///////////////////////////////////////////////////////////////////////////////////////////////////
                            lbPezaTotal1.Text = totalPeza.ToString("#,###");

                            List<Entities_SRF_RequestEntry> listTotalPezaAll = new List<Entities_SRF_RequestEntry>();
                            listTotalPezaAll = BLL_SRF.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange("", "07/01/2023", DateTime.Today.AddDays(-30).ToString("MM/dd/yyyy")).Where(itm => itm.Warehouse_PezaNonPeza == "1").ToList();
                            lbPezaTotal2.Text = listTotalPezaAll.Where(itm => itm.Warehouse_PezaNonPeza == "1").ToList().Select(itm2 => itm2.Warehouse_CtrlNo).Distinct().Count().ToString("#,###");
                            ///////////////////////////////////////////////////////////////////////////////////////////////////




                            Session["PezaPast3Total"] = lb34.Text;
                            Session["PezaPast2Total"] = lb31.Text;
                            Session["PezaPast1Total"] = lb28.Text;

                            Session["NonPezaPast3Total"] = lb70.Text;
                            Session["NonPezaPast2Total"] = lb67.Text;
                            Session["NonPezaPast1Total"] = lb64.Text;


                            Session["TotalPeza"] = totalPeza.ToString("#,###");
                            Session["TotalNonPeza"] = totalNonPeza.ToString("#,###");

                            //Session["TotalPezaQuantity"] = ((dashboardPeza_ApprovedWaiting_Pullout_Count + dashboardPeza_DeliveredPending_Pullout_Count + dashboardPeza_Delivered_Pullout_Count) + (dashboardPeza_ApprovedWaiting_Pullout_Count + dashboardPeza_DeliveredPending_Pullout_Count + dashboardPeza_Delivered_Pullout_Count) + (dashboardPeza_ApprovedWaiting_Pullout_Count + dashboardPeza_DeliveredPending_Pullout_Count + dashboardPeza_Delivered_Pullout_Count)).ToString("#,###");
                            //Session["TotalNonPezaQuantity"] = ((dashboardNonPeza_ApprovedWaiting_Pullout_Count + dashboardNonPeza_DeliveredPending_Pullout_Count + dashboardNonPeza_Delivered_Pullout_Count) + (dashboardNonPeza_ApprovedWaiting_Pullout_Count + dashboardNonPeza_DeliveredPending_Pullout_Count + dashboardNonPeza_Delivered_Pullout_Count) + (dashboardNonPeza_ApprovedWaiting_Pullout_Count + dashboardNonPeza_DeliveredPending_Pullout_Count + dashboardNonPeza_Delivered_Pullout_Count)).ToString("#,###");

                            long totalPeza1 = !string.IsNullOrEmpty(lb29.Text) ? long.Parse(lb29.Text.Replace(",", "")) : 0;
                            long totalPeza2 = !string.IsNullOrEmpty(lb32.Text) ? long.Parse(lb32.Text.Replace(",", "")) : 0;
                            long totalPeza3 = !string.IsNullOrEmpty(lb35.Text) ? long.Parse(lb35.Text.Replace(",", "")) : 0;

                            long totalNonPeza1 = !string.IsNullOrEmpty(lb65.Text) ? long.Parse(lb65.Text.Replace(",", "")) : 0;
                            long totalNonPeza2 = !string.IsNullOrEmpty(lb68.Text) ? long.Parse(lb68.Text.Replace(",", "")) : 0;
                            long totalNonPeza3 = !string.IsNullOrEmpty(lb71.Text) ? long.Parse(lb71.Text.Replace(",", "")) : 0;


                            Session["TotalPezaQuantity"] = (totalPeza1 + totalPeza2 + totalPeza3).ToString();
                            Session["TotalNonPezaQuantity"] = (totalNonPeza1 + totalNonPeza2 + totalNonPeza3).ToString();


                            Session["OverallRequestPeza"] = lbPezaTotal2.Text.Replace(",", "");
                            Session["OverallRequestNonPeza"] = lbNonPezaTotal2.Text.Replace(",", "");



                            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                        }


                        // FERDIE TESTING FOR WAREHOUSE NO DOCUMENTS
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        if (Session["Username"].ToString().ToUpper() == "TEST_ACCT")
                        {
                            int testcount = BLLCommon.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString()).Count;

                            ScriptManager.RegisterStartupScript(this, GetType(), "ShowWithoutDocuments", "javascript:alert('" + testcount.ToString() + " - " + ConfigurationManager.AppSettings["PIPL_Temp_MT_Access"].ToString() + "')", true);
                        }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                        if (ConfigurationManager.AppSettings["PIPL_Temp_MT_Access"].ToString().Contains(Session["Username"].ToString()) || BLLCommon.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString()).Count > 0)
                        {

                            List<Entities_SRF_RequestEntry> warehouseWithoutDocuments = new List<Entities_SRF_RequestEntry>();
                            warehouseWithoutDocuments = BLL_SRF.SRF_TRANSACTION_WarehouseItemsWithoutDocuments();

                            if (warehouseWithoutDocuments != null)
                            {
                                if (warehouseWithoutDocuments.Count > 0)
                                {

                                    if (warehouseWithoutDocuments.Count > 10)
                                    {
                                        btnMoreItems.Visible = true;
                                    }

                                    gvActualDelivery.DataSource = warehouseWithoutDocuments.Take(10).ToList();
                                    gvActualDelivery.DataBind();


                                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowWithoutDocuments", "showDialog2();", true);
                                }
                            }

                        }


                        if (Session["APPROVAL_OTHER_BUYERS"] != null)
                        {
                            Session["APPROVAL_OTHER_BUYERS"] = null;
                        }

                        if (Session["SENDING_OTHER_BUYERS"] != null)
                        {
                            Session["SENDING_OTHER_BUYERS"] = null;
                        }




                    }

                    // SYSTEM MAINTENANCE MESSAGE
                    //ScriptManager.RegisterStartupScript(this, GetType(), "ShowRegistered", "showDialog();", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                }

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

        protected void gvForApproval_OnRowDataBound(object sender, GridViewRowEventArgs e)
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

        //protected void gvThisWeekStatus_OnRowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
        //            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
        //    }
        //}

        protected void gvData_Command(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                LinkButton lbDetails = row.FindControl("lbDetails") as LinkButton;
                Label lblSupplierId = row.FindControl("lblSupplierId") as Label;

                if (e.CommandName == "lbDetails_Command")
                {
                    Session["TopResponded_SupplierID"] = lblSupplierId.Text.Trim();
                    Response.Redirect("RFQ_PurchasingReceiving.aspx", false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvForApproval_Command(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                LinkButton linkNoOfForApproval = row.FindControl("linkNoOfForApproval") as LinkButton;
                Label lblTransactionName = row.FindControl("lblTransactionName") as Label;

                if (e.CommandName == "linkNoOfForApproval_Command")
                {
                    if (lblTransactionName.Text.ToUpper() == "RFQ HOLD REQUEST")
                    {
                        Response.Redirect("RFQ_Monitoring.aspx");
                    }
                    if (lblTransactionName.Text.ToUpper() == "REQUEST FOR QUOTATION FOR SENDING")
                    {
                        Response.Redirect("RFQ_PurchasingReceiving.aspx");
                    }
                    //if (lblTransactionName.Text.ToUpper() == "RFQ PENDING FOR THE PAST 1 MONTH")
                    //{
                    //    Session["RFQ_PAST_1_MONTH"] = "true";
                    //    Response.Redirect("RFQ_PurchasingReceiving.aspx");
                    //}
                    if (lblTransactionName.Text.ToUpper() == "REQUEST FOR QUOTATION FOR APPROVAL" || lblTransactionName.Text.ToUpper() == "RFQ FOR PROD. MANAGER APPROVAL")
                    {
                        Response.Redirect("RFQ_ApprovalForm.aspx");
                    }
                    if (lblTransactionName.Text.ToUpper() == "SERVICE REPAIR FORM")
                    {
                        Response.Redirect("SRF_ApprovalForm.aspx");
                    }
                    if (lblTransactionName.Text.ToUpper() == "PROFORMA - ACCOUNTING" || lblTransactionName.Text.ToUpper() == "PROFORMA - PC MANAGER" || lblTransactionName.Text.ToUpper() == "PROFORMA - PROD. MANAGER" || lblTransactionName.Text.ToUpper() == "PROFORMA INVOICE AND PACKING LIST")
                    {
                        Response.Redirect("PIPL_ApprovalForm.aspx");
                    }
                    if (lblTransactionName.Text.ToUpper() == "DISCREPANCY REPORT FORM")
                    {
                        Response.Redirect("DRF_ApprovalForm.aspx");
                    }
                    if (lblTransactionName.Text.ToUpper() == "DISCREPANCY REPORT FORM FOR SENDING")
                    {
                        Response.Redirect("DRF_ReceivingEntry.aspx");
                    }
                    if (lblTransactionName.Text.ToUpper() == "CANCEL REQUEST FORM")
                    {
                        Response.Redirect("CRF_ApprovalForm.aspx");
                    }
                    if (lblTransactionName.Text.ToUpper() == "CANCEL REQUEST FORM FOR SENDING")
                    {
                        Response.Redirect("CRF_ReceivingEntry.aspx");
                    }
                    if (lblTransactionName.Text.ToUpper() == "CANCEL REQUEST FORM (RESPONDED)")
                    {
                        Response.Redirect("CRF_ReceivingEntry.aspx?crf_stat=supplier_responded");
                    }
                    if (lblTransactionName.Text.ToUpper() == "URGENT REQUEST FORM")
                    {
                        Response.Redirect("URF_ApprovalForm_New2.aspx");
                    }
                    if (lblTransactionName.Text.ToUpper() == "URGENT REQUEST FORM FOR SENDING")
                    {
                        Response.Redirect("URF_ReceivingEntry.aspx");
                    }
                    if (lblTransactionName.Text.ToUpper() == "URGENT REQUEST FORM (SEC. MANAGER)")
                    {
                        Response.Redirect("URF_ApprovalForm_New2.aspx");
                    }
                    if (lblTransactionName.Text.ToUpper() == "URGENT REQUEST FORM (DEPT. MANAGER)")
                    {
                        Response.Redirect("URF_ApprovalForm_New2.aspx");
                    }
                    if (lblTransactionName.Text.ToUpper() == "URGENT REQUEST FORM (DIVISION MANAGER)")
                    {
                        Response.Redirect("URF_ApprovalForm_New2.aspx");
                    }
                    if (lblTransactionName.Text.ToUpper() == "URGENT REQUEST FORM (HQ. MANAGER)")
                    {
                        Response.Redirect("URF_ApprovalForm_New2.aspx");
                    }

                    if (lblTransactionName.Text.ToUpper() == "EQUIPMENT REQUEST FOR OPERATION (INCHARGE)")
                    {
                        Response.Redirect("ERFO_ApprovalForm.aspx");
                    }
                    if (lblTransactionName.Text.ToUpper() == "EQUIPMENT REQUEST FOR OPERATION (INCHARGE-CONFIRMED)")
                    {
                        Response.Redirect("ERFO_ApprovalForm.aspx");
                    }
                    if (lblTransactionName.Text.ToUpper() == "EQUIPMENT REQUEST FOR OPERATION (DEPT. MANAGER)")
                    {
                        Response.Redirect("ERFO_ApprovalForm.aspx");
                    }
                    if (lblTransactionName.Text.ToUpper() == "EQUIPMENT REQUEST FOR OPERATION (DIV. MANAGER)")
                    {
                        Response.Redirect("ERFO_ApprovalForm.aspx");
                    }
                    if (lblTransactionName.Text.ToUpper() == "EQUIPMENT REQUEST FOR OPERATION")
                    {
                        Response.Redirect("ERFO_ApprovalForm.aspx");
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


        protected void gvForOtherBuyers_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                Label lblCategoryId = row.FindControl("lblCategoryId") as Label;
                LinkButton lbForApproval = row.FindControl("lbForApproval") as LinkButton;
                LinkButton lbForSending = row.FindControl("lbForSending") as LinkButton;

                //RFQ
                if (e.CommandName == "lbForApproval_Command")
                {
                    Session["APPROVAL_OTHER_BUYERS"] = lblCategoryId.Text.Trim();
                    Response.Redirect("RFQ_ApprovalForm.aspx", false);
                }
                if (e.CommandName == "lbForSending_Command")
                {
                    Session["SENDING_OTHER_BUYERS"] = lblCategoryId.Text.Trim();
                    Response.Redirect("RFQ_PurchasingReceiving.aspx", false);
                }

                //CRF
                if (e.CommandName == "lbCRFApproval_Command")
                {
                    Session["APPROVAL_OTHER_BUYERS"] = lblCategoryId.Text.Trim();
                    Response.Redirect("CRF_ApprovalForm.aspx", false);
                }
                if (e.CommandName == "lbCRFSending_Command")
                {
                    Session["SENDING_OTHER_BUYERS"] = lblCategoryId.Text.Trim();
                    Response.Redirect("CRF_ReceivingEntry.aspx", false);
                }

                //DRF
                if (e.CommandName == "lbDRFApproval_Command")
                {
                    Session["APPROVAL_OTHER_BUYERS"] = lblCategoryId.Text.Trim();
                    Response.Redirect("DRF_ApprovalForm.aspx", false);
                }
                if (e.CommandName == "lbDRFSending_Command")
                {
                    Session["SENDING_OTHER_BUYERS"] = lblCategoryId.Text.Trim();
                    Response.Redirect("DRF_ReceivingEntry.aspx", false);
                }

                //URF
                if (e.CommandName == "lbURFApproval_Command")
                {
                    Session["APPROVAL_OTHER_BUYERS"] = lblCategoryId.Text.Trim();
                    Response.Redirect("URF_ApprovalForm.aspx", false);
                }
                if (e.CommandName == "lbURFSending_Command")
                {
                    Session["SENDING_OTHER_BUYERS"] = lblCategoryId.Text.Trim();
                    Response.Redirect("URF_ReceivingEntry.aspx", false);
                }

                //SRF
                if (e.CommandName == "lbSRFApproval_Command")
                {
                    Session["APPROVAL_OTHER_BUYERS"] = lblCategoryId.Text.Trim();
                    Response.Redirect("SRF_ApprovalForm.aspx", false);
                }

                //PROFORMA
                if (e.CommandName == "lbProformaApproval_Command")
                {
                    Session["APPROVAL_OTHER_BUYERS"] = lblCategoryId.Text.Trim();
                    Response.Redirect("PIPL_ApprovalForm.aspx", false);
                }

                //ERFO
                if (e.CommandName == "lbERFOApproval_Command")
                {
                    Session["APPROVAL_OTHER_BUYERS"] = lblCategoryId.Text.Trim();
                    Response.Redirect("ERFO_ApprovalForm.aspx", false);
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
                    Label lblAttachment = (Label)e.Row.FindControl("lblAttachment");
                    LinkButton lbAttachment = (LinkButton)e.Row.FindControl("lbAttachment");
                    FileUpload fuAttachment = (FileUpload)e.Row.FindControl("fuAttachment");

                    Label lblDetailsRefId = (Label)e.Row.FindControl("lblDetailsRefId");
                    TextBox txtLOA8105Number = (TextBox)e.Row.FindControl("txtLOA8105Number");
                    TextBox txt8105ProcessDate = (TextBox)e.Row.FindControl("txt8105ProcessDate");

                    if (!string.IsNullOrEmpty(lblAttachment.Text))
                    {
                        lbAttachment.Visible = true;
                        fuAttachment.Visible = false;
                    }
                    else
                    {
                        lbAttachment.Visible = false;
                        fuAttachment.Visible = true;
                        lblDetailsRefId.Style.Add("color", "Red");
                        txtLOA8105Number.Style.Add("background-color", "#FFCCCB");
                        txt8105ProcessDate.Style.Add("background-color", "#FFCCCB");
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

        protected void btnMoreItems_Click(object sender, EventArgs e)
        {
            Response.Redirect("SRF_Warehouse_WithoutDocuments.aspx", false);
        }




    }
}
