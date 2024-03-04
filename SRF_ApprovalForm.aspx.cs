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
//using REPI_PUR_SOFRA.App_Code.ENTITIES;
//using REPI_PUR_SOFRA.App_Code.BLL;
using System.Collections.Generic;

namespace REPI_PUR_SOFRA
{
    public partial class SRF_ApprovalForm : System.Web.UI.Page
    {

        BLL_SRF BLL = new BLL_SRF();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //try
                //{
                    //txtFrom.Text = DateTime.Today.AddDays(-7).ToString("MM/dd/yyyy");
                    //txtTo.Text = DateTime.Today.AddDays(7).ToString("MM/dd/yyyy");

                    // call submit button to load initial record
                    //btnSubmit_Click(sender, e);
                SRF_TRANSACTION_RequestEntry_Fill_All_DropdownList();
                LoadDefault();

                //}
                //catch (Exception ex)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                //}
            }
        }


        private bool isUserAllowed(string loginId, string transaction)
        {
            bool isAllowed = false;
            BLL_Common BLLCommon = new BLL_Common();

            List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();
            list = BLLCommon.Common_checkIfUserHasTransactionsByUserId(loginId);

            if (list.Count > 0)
            {
                foreach (Entities_Common_SystemUsers entity in list)
                {
                    if (transaction.Trim().Equals(entity.TransactionType.Trim()))
                    {
                        isAllowed = true;
                    }
                }
            }

            return isAllowed;
        }

        private void LoadDefault()
        {
            try
            {
                List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
                Entities_SRF_RequestEntry status = new Entities_SRF_RequestEntry();
                var PIPL_Impex_Access = ConfigurationManager.AppSettings["PIPL_Temp_Sir_Renz"];

                //status.DrFrom = txtFrom.Text.Trim();
                //status.DrTo = txtTo.Text.Trim();
                status.DrFrom = string.Empty;
                status.DrTo = string.Empty;
                status.Requester = Session["LcRefId"].ToString();

                list = null;
                Session["aType"] = "";

                //REQUESTOR INCHARGE
                if (isUserAllowed(status.Requester, "307") && int.Parse(Session["CategoryAccess"].ToString()) <= 0)
                {
                    list = BLL.SRF_TRANSACTION_ForApproval(status).Where(item => item.ReqInchargeStat.Length <= 0 || item.ReqInchargeStat == "0"
                                                                       && item.Department == Session["Department"].ToString()).ToList();
                    Session["aType"] = "requestor_incharge";
                }

                //REQUESTOR MANAGER
                //if (isUserAllowed(status.Requester, "308") && int.Parse(Session["CategoryAccess"].ToString()) <= 0)
                if (int.Parse(Session["CategoryAccess"].ToString()) <= 0)
                {
                    list = BLL.SRF_TRANSACTION_ForApproval(status).Where(item => item.ReqManagerStat.Length <= 0 || item.ReqManagerStat == "0" && item.ReqInchargeStat == "1"
                                                                       && item.Department == Session["Department"].ToString()).ToList();
                    Session["aType"] = "requestor_manager";
                }                

                //PURCHASING INCHARGE
                if (int.Parse(Session["CategoryAccess"].ToString()) > 0)
                {
                    if (Session["APPROVAL_OTHER_BUYERS"] != null)
                    {
                        string otherBuyer = Session["APPROVAL_OTHER_BUYERS"].ToString();
                        ddCategory.Items.FindByValue(otherBuyer).Selected = true;
                    }
                    else
                    {
                        ddCategory.Items.FindByText(Session["CategoryName"].ToString()).Selected = true;
                    }

                    if (ddCategory.SelectedItem.Text.ToLower() == "all")
                    {
                        list = BLL.SRF_TRANSACTION_ForApproval(status).Where(item => item.ReqInchargeStat == "1" && item.ReqManagerStat == "1" && item.PurInchargeStat == "0").ToList();
                    }
                    else
                    {
                        list = BLL.SRF_TRANSACTION_ForApproval(status).Where(item => item.CategoryDescription.ToUpper() == ddCategory.SelectedItem.Text.ToUpper() && item.ReqInchargeStat == "1" && item.ReqManagerStat == "1" && item.PurInchargeStat == "0").ToList();
                    }

                    Session["aType"] = "purchasing_incharge";
                }

                //PURCHASING DEPARTMENT MANAGER
                if (isUserAllowed(Session["LcRefId"].ToString(), "16"))
                {
                    list = BLL.SRF_TRANSACTION_ForApproval(status).Where(item => item.ReqInchargeStat == "1" && item.ReqManagerStat == "1" && item.PurInchargeStat == "1" && item.PurDeptManagerStat == "0" && item.PurImpexStat == "0").ToList();
                    Session["aType"] = "purchasing_dept_manager";

                    // THIS IS FOR SCD/PURCHASING OWN REQUEST
                    List<Entities_SRF_RequestEntry> listRequestFromSCD = new List<Entities_SRF_RequestEntry>();
                    listRequestFromSCD = BLL.SRF_TRANSACTION_ForApproval(status).Where(item => item.ReqManagerStat.Length <= 0 || item.ReqManagerStat == "0" && item.ReqInchargeStat == "1"
                                                                       && item.Division == Session["Division"].ToString()).ToList();

                    if (listRequestFromSCD != null)
                    {
                        if (listRequestFromSCD.Count > 0)
                        {
                            foreach (Entities_SRF_RequestEntry scdRequest in listRequestFromSCD)
                            {
                                Entities_SRF_RequestEntry eSCD = new Entities_SRF_RequestEntry();
                                eSCD.Attachment = scdRequest.Attachment;
                                eSCD.Attention = scdRequest.Attention;
                                eSCD.BrandMachineName = scdRequest.BrandMachineName;
                                eSCD.Category = scdRequest.Category;
                                eSCD.CategoryDescription = scdRequest.CategoryDescription;
                                eSCD.CtrlNo = scdRequest.CtrlNo;
                                eSCD.DeliveryDateToRepi = scdRequest.DeliveryDateToRepi;
                                eSCD.Department = scdRequest.Department;
                                eSCD.Division = scdRequest.Division;
                                eSCD.DrFrom = scdRequest.DrFrom;
                                eSCD.DropdownName = scdRequest.DropdownName;
                                eSCD.DropdownRefId = scdRequest.DropdownRefId;
                                eSCD.DrTo = scdRequest.DrTo;
                                eSCD.GatePassNo = scdRequest.GatePassNo;
                                eSCD.IsDisabled = scdRequest.IsDisabled;
                                eSCD.IsImpex = scdRequest.IsImpex;
                                eSCD.ItemName = scdRequest.ItemName;
                                eSCD.Loa8106 = scdRequest.Loa8106;
                                eSCD.LOADescription = scdRequest.LOADescription;
                                eSCD.LoaNo = scdRequest.LoaNo;
                                eSCD.LoaSuretyBond = scdRequest.LoaSuretyBond;
                                eSCD.PickUpPoint = scdRequest.PickUpPoint;
                                eSCD.POPDescription = scdRequest.POPDescription;
                                eSCD.ProblemEncountered = scdRequest.ProblemEncountered;
                                eSCD.PullOutServiceDate = scdRequest.PullOutServiceDate;
                                eSCD.PurDeptManager = scdRequest.PurDeptManager;
                                eSCD.PurDeptManagerDOA = scdRequest.PurDeptManagerDOA;
                                eSCD.PurDeptManagerName = scdRequest.PurDeptManagerName;
                                eSCD.PurDeptManagerStat = scdRequest.PurDeptManagerStat;
                                eSCD.PurImpex = scdRequest.PurImpex;
                                eSCD.PurImpexDOA = scdRequest.PurImpexDOA;
                                eSCD.PurImpexStat = scdRequest.PurImpexStat;
                                eSCD.PurIncharge = scdRequest.PurIncharge;
                                eSCD.PurInchargeDOA = scdRequest.PurInchargeDOA;
                                eSCD.PurInchargeName = scdRequest.PurInchargeName;
                                eSCD.PurInchargeStat = scdRequest.PurInchargeStat;
                                eSCD.PurManagerName = scdRequest.PurManagerName;
                                eSCD.PurposeOfPullOut = scdRequest.PurposeOfPullOut;
                                eSCD.Quantity = scdRequest.Quantity;
                                eSCD.RefId = scdRequest.RefId;
                                eSCD.RefPRPO = scdRequest.RefPRPO;
                                eSCD.Remarks = scdRequest.Remarks;
                                eSCD.ReqIncharge = scdRequest.ReqIncharge;
                                eSCD.ReqInchargeDOA = scdRequest.ReqInchargeDOA;
                                eSCD.ReqInchargeName = scdRequest.ReqInchargeName;
                                eSCD.ReqInchargeStat = scdRequest.ReqInchargeStat;
                                eSCD.ReqManager = scdRequest.ReqManager;
                                eSCD.ReqManagerDOA = scdRequest.ReqManagerDOA;
                                eSCD.ReqManagerName = scdRequest.ReqManagerName;
                                eSCD.ReqManagerStat = scdRequest.ReqManagerStat;
                                eSCD.Requester = scdRequest.Requester;
                                eSCD.SalesInvoice = scdRequest.SalesInvoice;
                                eSCD.SearchItem = scdRequest.SearchItem;
                                eSCD.Section = scdRequest.Section;
                                eSCD.SelectAll = scdRequest.SelectAll;
                                eSCD.SerialNo = scdRequest.SerialNo;
                                eSCD.Specification = scdRequest.Specification;
                                eSCD.StatAll = scdRequest.StatAll;
                                eSCD.StatRemarks = scdRequest.StatRemarks;
                                eSCD.Supplier = scdRequest.Supplier;
                                eSCD.TableName = scdRequest.TableName;
                                eSCD.TotalQuantity = scdRequest.TotalQuantity;
                                eSCD.TotalValueInUsd = scdRequest.TotalValueInUsd;
                                eSCD.TransactionDate = scdRequest.TransactionDate;
                                eSCD.UnitOfMeasure = scdRequest.UnitOfMeasure;
                                eSCD.UOM_Description = scdRequest.UOM_Description;
                                eSCD.UpdatedBy = scdRequest.UpdatedBy;
                                eSCD.UpdatedDate = scdRequest.UpdatedDate;

                                list.Add(eSCD);
                            }

                        }
                    }



                }

                //PURCHASING MANAGER (057 - MARILYN BONTIGAO)
                //if (Session["Username"].ToString() == PIPL_Impex_Access.ToString() || Session["Username"].ToString() == "057")
                //if (Session["Username"].ToString() == PIPL_Impex_Access.ToString())
                if (Session["Username"].ToString() == "5057" || Session["Username"].ToString() == PIPL_Impex_Access.ToString() || Session["Username"].ToString() == "01086" || Session["Username"].ToString() == "13292")
                {
                    list = BLL.SRF_TRANSACTION_ForApproval(status).Where(item => item.ReqInchargeStat == "1" && item.ReqManagerStat == "1" && item.PurInchargeStat == "1" && item.PurDeptManagerStat == "1" && item.PurImpexStat == "0" && string.IsNullOrEmpty(item.GatePassNo)).ToList();
                    Session["aType"] = "purchasing_manager";
                }

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        btnOk.Visible = true;
                        gvData.Visible = true;
                        gvData.DataSource = list;
                        gvData.DataBind();
                    }
                    else
                    {
                        btnOk.Visible = false;
                        gvData.Visible = true;
                        gvData.EmptyDataText = "NO RECORD(S) FOUND!";
                    }
                }


            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.InnerException + " - " + ex.Source + " - " + ex.StackTrace + " - " + ex.TargetSite + "');", true);
                //throw ex;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
                Entities_SRF_RequestEntry status = new Entities_SRF_RequestEntry();
                var PIPL_Impex_Access = ConfigurationManager.AppSettings["PIPL_Temp_Sir_Renz"];

                //status.DrFrom = txtFrom.Text.Trim();
                //status.DrTo = txtTo.Text.Trim();
                status.DrFrom = string.Empty;
                status.DrTo = string.Empty;
                status.Requester = Session["LcRefId"].ToString();

                list = null;
                Session["aType"] = "";

                //REQUESTOR INCHARGE
                if (isUserAllowed(status.Requester, "307") && int.Parse(Session["CategoryAccess"].ToString()) <= 0)
                {
                    list = BLL.SRF_TRANSACTION_ForApproval(status).Where(item => item.ReqInchargeStat.Length <= 0 || item.ReqInchargeStat == "0"
                                                                       && item.Department == Session["Department"].ToString()).ToList();
                    Session["aType"] = "requestor_incharge";
                }

                //REQUESTOR MANAGER
                //if (isUserAllowed(status.Requester, "308") && int.Parse(Session["CategoryAccess"].ToString()) <= 0)
                if (int.Parse(Session["CategoryAccess"].ToString()) <= 0)
                {
                    if (ddCategory.SelectedItem.Text.ToLower() == "all")
                    {
                        list = BLL.SRF_TRANSACTION_ForApproval(status).Where(item => item.ReqManagerStat.Length <= 0 || item.ReqManagerStat == "0" && item.ReqInchargeStat == "1"
                                                                           && item.Department == Session["Department"].ToString()).ToList();
                    }
                    else
                    {
                        list = BLL.SRF_TRANSACTION_ForApproval(status).Where(item => item.ReqManagerStat.Length <= 0 || item.ReqManagerStat == "0" && item.ReqInchargeStat == "1"
                                                                           && item.Department == Session["Department"].ToString() && item.CategoryDescription.ToUpper() == ddCategory.SelectedItem.Text.ToUpper()).ToList();
                    }
                    Session["aType"] = "requestor_manager";
                }

                //PURCHASING INCHARGE
                if (int.Parse(Session["CategoryAccess"].ToString()) > 0)
                {
                    if (ddCategory.SelectedItem.Text.ToLower() == "all")
                    {
                        list = BLL.SRF_TRANSACTION_ForApproval(status).Where(item => item.ReqInchargeStat == "1" && item.ReqManagerStat == "1" && item.PurInchargeStat == "0").ToList();
                    }
                    else
                    {
                        list = BLL.SRF_TRANSACTION_ForApproval(status).Where(item => item.CategoryDescription.ToUpper() == ddCategory.SelectedItem.Text.ToUpper() && item.ReqInchargeStat == "1" && item.ReqManagerStat == "1" && item.PurInchargeStat == "0").ToList();
                    }
                    Session["aType"] = "purchasing_incharge";
                }

                //PURCHASING DEPARTMENT MANAGER
                if (isUserAllowed(Session["LcRefId"].ToString(), "16"))
                {
                    if (ddCategory.SelectedItem.Text.ToLower() == "all")
                    {
                        list = BLL.SRF_TRANSACTION_ForApproval(status).Where(item => item.ReqInchargeStat == "1" && item.ReqManagerStat == "1" && item.PurInchargeStat == "1" && item.PurDeptManagerStat == "0" && item.PurImpexStat == "0").ToList();
                    }
                    else
                    {
                        list = BLL.SRF_TRANSACTION_ForApproval(status).Where(item => item.ReqInchargeStat == "1" && item.ReqManagerStat == "1" && item.PurInchargeStat == "1" && item.PurDeptManagerStat == "0" && item.PurImpexStat == "0" && item.CategoryDescription.ToUpper() == ddCategory.SelectedItem.Text.ToUpper()).ToList();                        
                    }
                    Session["aType"] = "purchasing_dept_manager";

                    // THIS IS FOR SCD/PURCHASING OWN REQUEST
                    List<Entities_SRF_RequestEntry> listRequestFromSCD = new List<Entities_SRF_RequestEntry>();

                    if (ddCategory.SelectedItem.Text.ToLower() == "all")
                    {
                        listRequestFromSCD = BLL.SRF_TRANSACTION_ForApproval(status).Where(item => item.ReqManagerStat.Length <= 0 || item.ReqManagerStat == "0" && item.ReqInchargeStat == "1"
                                                                           && item.Division == Session["Division"].ToString()).ToList();
                    }
                    else
                    {
                        listRequestFromSCD = BLL.SRF_TRANSACTION_ForApproval(status).Where(item => item.ReqManagerStat.Length <= 0 || item.ReqManagerStat == "0" && item.ReqInchargeStat == "1"
                                                                           && item.Division == Session["Division"].ToString() && item.CategoryDescription.ToUpper() == ddCategory.SelectedItem.Text.ToUpper()).ToList();
                    }

                    if (listRequestFromSCD != null)
                    {
                        if (listRequestFromSCD.Count > 0)
                        {
                            foreach (Entities_SRF_RequestEntry scdRequest in listRequestFromSCD)
                            {
                                Entities_SRF_RequestEntry eSCD = new Entities_SRF_RequestEntry();
                                eSCD.Attachment = scdRequest.Attachment;
                                eSCD.Attention = scdRequest.Attention;
                                eSCD.BrandMachineName = scdRequest.BrandMachineName;
                                eSCD.Category = scdRequest.Category;
                                eSCD.CategoryDescription = scdRequest.CategoryDescription;
                                eSCD.CtrlNo = scdRequest.CtrlNo;
                                eSCD.DeliveryDateToRepi = scdRequest.DeliveryDateToRepi;
                                eSCD.Department = scdRequest.Department;
                                eSCD.Division = scdRequest.Division;
                                eSCD.DrFrom = scdRequest.DrFrom;
                                eSCD.DropdownName = scdRequest.DropdownName;
                                eSCD.DropdownRefId = scdRequest.DropdownRefId;
                                eSCD.DrTo = scdRequest.DrTo;
                                eSCD.GatePassNo = scdRequest.GatePassNo;
                                eSCD.IsDisabled = scdRequest.IsDisabled;
                                eSCD.IsImpex = scdRequest.IsImpex;
                                eSCD.ItemName = scdRequest.ItemName;
                                eSCD.Loa8106 = scdRequest.Loa8106;
                                eSCD.LOADescription = scdRequest.LOADescription;
                                eSCD.LoaNo = scdRequest.LoaNo;
                                eSCD.LoaSuretyBond = scdRequest.LoaSuretyBond;
                                eSCD.PickUpPoint = scdRequest.PickUpPoint;
                                eSCD.POPDescription = scdRequest.POPDescription;
                                eSCD.ProblemEncountered = scdRequest.ProblemEncountered;
                                eSCD.PullOutServiceDate = scdRequest.PullOutServiceDate;
                                eSCD.PurDeptManager = scdRequest.PurDeptManager;
                                eSCD.PurDeptManagerDOA = scdRequest.PurDeptManagerDOA;
                                eSCD.PurDeptManagerName = scdRequest.PurDeptManagerName;
                                eSCD.PurDeptManagerStat = scdRequest.PurDeptManagerStat;
                                eSCD.PurImpex = scdRequest.PurImpex;
                                eSCD.PurImpexDOA = scdRequest.PurImpexDOA;
                                eSCD.PurImpexStat = scdRequest.PurImpexStat;
                                eSCD.PurIncharge = scdRequest.PurIncharge;
                                eSCD.PurInchargeDOA = scdRequest.PurInchargeDOA;
                                eSCD.PurInchargeName = scdRequest.PurInchargeName;
                                eSCD.PurInchargeStat = scdRequest.PurInchargeStat;
                                eSCD.PurManagerName = scdRequest.PurManagerName;
                                eSCD.PurposeOfPullOut = scdRequest.PurposeOfPullOut;
                                eSCD.Quantity = scdRequest.Quantity;
                                eSCD.RefId = scdRequest.RefId;
                                eSCD.RefPRPO = scdRequest.RefPRPO;
                                eSCD.Remarks = scdRequest.Remarks;
                                eSCD.ReqIncharge = scdRequest.ReqIncharge;
                                eSCD.ReqInchargeDOA = scdRequest.ReqInchargeDOA;
                                eSCD.ReqInchargeName = scdRequest.ReqInchargeName;
                                eSCD.ReqInchargeStat = scdRequest.ReqInchargeStat;
                                eSCD.ReqManager = scdRequest.ReqManager;
                                eSCD.ReqManagerDOA = scdRequest.ReqManagerDOA;
                                eSCD.ReqManagerName = scdRequest.ReqManagerName;
                                eSCD.ReqManagerStat = scdRequest.ReqManagerStat;
                                eSCD.Requester = scdRequest.Requester;
                                eSCD.SalesInvoice = scdRequest.SalesInvoice;
                                eSCD.SearchItem = scdRequest.SearchItem;
                                eSCD.Section = scdRequest.Section;
                                eSCD.SelectAll = scdRequest.SelectAll;
                                eSCD.SerialNo = scdRequest.SerialNo;
                                eSCD.Specification = scdRequest.Specification;
                                eSCD.StatAll = scdRequest.StatAll;
                                eSCD.StatRemarks = scdRequest.StatRemarks;
                                eSCD.Supplier = scdRequest.Supplier;
                                eSCD.TableName = scdRequest.TableName;
                                eSCD.TotalQuantity = scdRequest.TotalQuantity;
                                eSCD.TotalValueInUsd = scdRequest.TotalValueInUsd;
                                eSCD.TransactionDate = scdRequest.TransactionDate;
                                eSCD.UnitOfMeasure = scdRequest.UnitOfMeasure;
                                eSCD.UOM_Description = scdRequest.UOM_Description;
                                eSCD.UpdatedBy = scdRequest.UpdatedBy;
                                eSCD.UpdatedDate = scdRequest.UpdatedDate;

                                list.Add(eSCD);
                            }

                        }
                    }


                }

                //PURCHASING MANAGER (057 - MARILYN BONTIGAO)
                //if (Session["Username"].ToString() == PIPL_Impex_Access.ToString() || Session["Username"].ToString() == "057")
                if (Session["Username"].ToString() == "5057" || Session["Username"].ToString() == PIPL_Impex_Access.ToString() || Session["Username"].ToString() == "01086")
                {
                    if (ddCategory.SelectedItem.Text.ToLower() == "all")
                    {
                        list = BLL.SRF_TRANSACTION_ForApproval(status).Where(item => item.ReqInchargeStat == "1" && item.ReqManagerStat == "1" && item.PurInchargeStat == "1" && item.PurDeptManagerStat == "1" && item.PurImpexStat == "0").ToList();
                    }
                    else
                    {
                        list = BLL.SRF_TRANSACTION_ForApproval(status).Where(item => item.ReqInchargeStat == "1" && item.ReqManagerStat == "1" && item.PurInchargeStat == "1" && item.PurDeptManagerStat == "1" && item.PurImpexStat == "0" && item.CategoryDescription.ToUpper() == ddCategory.SelectedItem.Text.ToUpper()).ToList();
                    }
                    Session["aType"] = "purchasing_manager";
                }

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        btnOk.Visible = true;
                        gvData.Visible = true;
                        gvData.DataSource = list;
                        gvData.DataBind();
                        gvData.Visible = true;
                    }
                    else
                    {
                        btnOk.Visible = false;
                        gvData.Visible = true;
                        gvData.EmptyDataText = "NO RECORD(S) FOUND!";
                        gvData.Visible = false;
                    }
                }


            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.InnerException + " - " + ex.Source + " - " + ex.StackTrace + " - " + ex.TargetSite + "');", true);
                //throw ex;
            }
        }


        protected void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                bool isError = false;
                string approvedItems = string.Empty;

                if (gvData.Rows.Count > 0)
                {
                    for (int i = 0; i < gvData.Rows.Count; i++)
                    {
                        Entities_SRF_RequestEntry details = new Entities_SRF_RequestEntry();
                        LinkButton lblCtrl = (LinkButton)gvData.Rows[i].Cells[1].FindControl("lblCtrl");
                        ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[2].FindControl("ibApproved");
                        ImageButton ibDisapproved = (ImageButton)gvData.Rows[i].Cells[2].FindControl("ibDisapproved");
                        TextBox txtRemarks = (TextBox)gvData.Rows[i].Cells[10].FindControl("txtRemarks");
                        TextBox txtGPNumber = (TextBox)gvData.Rows[i].Cells[8].FindControl("txtGPNumber");
                        TextBox txtLOA8106No = (TextBox)gvData.Rows[i].Cells[9].FindControl("txtLOA8106No");
                        Label lblSupplier = (Label)gvData.Rows[i].Cells[2].FindControl("lblSupplier");
                        Label lblSupplierEmail = (Label)gvData.Rows[i].Cells[2].FindControl("lblSupplierEmail");
                        Label lblPezaNonPeza = (Label)gvData.Rows[i].Cells[2].FindControl("lblPezaNonPeza");

                        if (ibApproved.ImageUrl == "~/images/A2.png" || ibDisapproved.ImageUrl == "~/images/DA2.png")
                        {
                            // REQUESTOR INCHARGE
                            if (Session["aType"].ToString().ToLower().Trim() == "requestor_incharge".ToLower())
                            {
                                try
                                {
                                    details.CtrlNo = lblCtrl.Text.Trim();
                                    details.ReqIncharge = Session["LcRefId"].ToString();
                                    if (ibApproved.ImageUrl == "~/images/A2.png")
                                    {
                                        details.ReqInchargeStat = "1";                                        
                                    }
                                    if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                                    {
                                        details.ReqInchargeStat = "2";                                        
                                    }

                                    details.Remarks = txtRemarks.Text.Length > 0 ? txtRemarks.Text : "";
                                    BLL.SRF_TRANSACTION_ApprovedRequestorIncharge(details);
                                    approvedItems += lblCtrl.Text.Trim() + ", ";
                                }
                                catch (Exception ex)
                                {
                                    isError = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                                }
                            }

                            // REQUESTOR MANAGER
                            if (Session["aType"].ToString().ToLower().Trim() == "requestor_manager".ToLower())
                            {
                                try
                                {
                                    details.CtrlNo = lblCtrl.Text.Trim();
                                    details.ReqManager = Session["LcRefId"].ToString();
                                    if (ibApproved.ImageUrl == "~/images/A2.png")
                                    {
                                        details.ReqManagerStat = "1";
                                    }
                                    if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                                    {
                                        details.ReqManagerStat = "2";
                                    }

                                    details.Remarks = txtRemarks.Text.Length > 0 ? txtRemarks.Text : "";
                                    BLL.SRF_TRANSACTION_ApprovedRequestorManager(details);
                                    approvedItems += lblCtrl.Text.Trim() + ", ";
                                }
                                catch (Exception ex)
                                {
                                    isError = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                                }
                            }

                            // PURCHASING INCHARGE
                            if (Session["aType"].ToString().ToLower().Trim() == "purchasing_incharge".ToLower())
                            {
                                try
                                {
                                    if (string.IsNullOrEmpty(lblPezaNonPeza.Text))
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('Unable to approved! Kindly update Peza/NonPeza field of supplier " + lblSupplier.Text.ToUpper() + " to proceed!');", true);
                                    }
                                    else
                                    {

                                        details.CtrlNo = lblCtrl.Text.Trim();
                                        details.PurIncharge = Session["LcRefId"].ToString();
                                        if (ibApproved.ImageUrl == "~/images/A2.png")
                                        {
                                            details.PurInchargeStat = "1";

                                            //// SET PURCHASING DEPT. MANAGER TO NULL & APPROVED = 0
                                            //Entities_SRF_RequestEntry purchasingDeptApproved = new Entities_SRF_RequestEntry();
                                            //purchasingDeptApproved.CtrlNo = lblCtrl.Text.Trim();
                                            //purchasingDeptApproved.PurDeptManager = DBNull.Value.ToString();
                                            //purchasingDeptApproved.PurDeptManagerStat = "0";
                                            //purchasingDeptApproved.Remarks = txtRemarks.Text.Length > 0 ? txtRemarks.Text : "";
                                            //BLL.SRF_TRANSACTION_ApprovedPurchasingDeptManager(purchasingDeptApproved);

                                            // SET PURCHASING DEPT. MANAGER TO AUTO-APPROVED (10/31/2023 Discussed with Sir Ghing)
                                            Entities_SRF_RequestEntry purchasingDeptApproved = new Entities_SRF_RequestEntry();
                                            purchasingDeptApproved.CtrlNo = lblCtrl.Text.Trim();
                                            //purchasingDeptApproved.PurDeptManager = "3590"; (LOCAL MACHINE)
                                            purchasingDeptApproved.PurDeptManager = "1997"; //SERVER (AUTO-APPROVED ACCOUNT)
                                            purchasingDeptApproved.PurDeptManagerStat = "1";
                                            purchasingDeptApproved.Remarks = txtRemarks.Text.Length > 0 ? txtRemarks.Text : "";
                                            BLL.SRF_TRANSACTION_ApprovedPurchasingDeptManager(purchasingDeptApproved);
                                        }
                                        if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                                        {
                                            details.PurInchargeStat = "2";
                                        }

                                        details.Remarks = txtRemarks.Text.Length > 0 ? txtRemarks.Text : "";
                                        BLL.SRF_TRANSACTION_ApprovedPurchasingIncharge(details);
                                        approvedItems += lblCtrl.Text.Trim() + ", ";

                                        // CHECK IF HAS GP-NO
                                        if (!string.IsNullOrEmpty(txtGPNumber.Text) || !String.IsNullOrEmpty(txtGPNumber.Text))
                                        {

                                            // UPDATE GP-NO
                                            Entities_SRF_RequestEntry updateGPNo = new Entities_SRF_RequestEntry();
                                            updateGPNo.CtrlNo = lblCtrl.Text.Trim();
                                            updateGPNo.GatePassNo = txtGPNumber.Text;

                                            if (BLL.SRF_TRANSACTION_RequestEntry_Update_GPNO_ByControlNo(updateGPNo).ToString() == "-1")
                                            {
                                                details.CtrlNo = lblCtrl.Text.Trim();
                                                details.PurImpex = "1997"; //SERVER (AUTO-APPROVED ACCOUNT)
                                                details.PurImpexStat = "1";
                                                details.Remarks = "AUTO-APPROVED WITH GPNO";
                                                // Auto-approved 
                                                BLL.SRF_TRANSACTION_ApprovedPurchasingManager(details);
                                            }

                                        }

                                        //CHECK IF PULL-OUT REQUEST THEN SEND PULLOUT REQUEST TO SUPPLIER
                                        if (ibApproved.ImageUrl == "~/images/A2.png")
                                        {
                                            List<Entities_SRF_PO_Entry> listSRF_HEAD = new List<Entities_SRF_PO_Entry>();
                                            listSRF_HEAD = BLL.SRF_TRANSACTION_PO_WITH_SRF_HEAD(lblCtrl.Text.Trim());

                                            if (listSRF_HEAD != null)
                                            {
                                                if (listSRF_HEAD.Count > 0)
                                                {
                                                    string attachSRF_PO = string.Empty;
                                                    string email_content = string.Empty;
                                                    email_content = "Hi.<b>" + lblSupplier.Text.ToUpper() + "</b>, Please see attached pullout-request related to SRF# (<b>" + lblCtrl.Text.Trim() + "</b>)." +
                                                        "<br/><br/>" +
                                                        "<p style='color:black; font-size:12px;'>Please do not reply. This is an auto generated email.</p>" +
                                                        "<br/><br/>" +
                                                        "<p style='color:black; font-size:12px;'>If you have any questions or clarifications, kindly send a separate email to <b>" + ConfigurationManager.AppSettings["MamAndhieMapanoo_EmailAddress"].ToString() + "</b></p>" +
                                                        "<br/><br/>" +
                                                        "Thank you," +
                                                        "<br/><br/>" +
                                                        "<b>ROHM Electronics Phils.</b>" +
                                                        "<br/>" +
                                                        "Supply Chain Management";


                                                    foreach (Entities_SRF_PO_Entry srf_head in listSRF_HEAD)
                                                    {
                                                        attachSRF_PO += Server.MapPath("~/SRF_PO_Request/" + srf_head.Ctrlno.Trim() + "/" + srf_head.Ctrlno.Trim() + "_SRF_PO_Request.xlsx") + ",";
                                                    }

                                                    COMMON.sendEmailToSRF_PullOut(lblSupplierEmail.Text.Trim(), ConfigurationManager.AppSettings["email-username"], "SRF PULL-OUT REQUEST", email_content, attachSRF_PO.Substring(0, attachSRF_PO.Length - 1), lblSupplier.Text.ToUpper(), string.Empty);
                                                }
                                            }

                                        }




                                    }

                                    //// CHECK IF ITEM HAS GATEPASS NUMBER (IF YES THEN AUTO APPROVED purchasing_manager plus update the GP Number Field in Head Table)
                                    //if (!string.IsNullOrEmpty(txtGPNumber.Text) || !String.IsNullOrEmpty(txtGPNumber.Text))
                                    //{
                                    //    // Update GPNo
                                    //    Entities_SRF_RequestEntry updateGPNo = new Entities_SRF_RequestEntry();
                                    //    updateGPNo.CtrlNo = lblCtrl.Text.Trim();
                                    //    updateGPNo.GatePassNo = txtGPNumber.Text;

                                    //    if (BLL.SRF_TRANSACTION_RequestEntry_Update_GPNO_ByControlNo(updateGPNo).ToString() == "-1")
                                    //    {
                                    //        details.CtrlNo = lblCtrl.Text.Trim();
                                    //        details.PurImpex = Session["LcRefId"].ToString();
                                    //        details.PurImpexStat = "1";
                                    //        details.Remarks = "AUTO-APPROVED WITH GPNO";
                                    //        // Auto-approved 
                                    //        BLL.SRF_TRANSACTION_ApprovedPurchasingManager(details);
                                            
                                    //        // Auto-approved PUR DEPT. MANAGER
                                    //        details.PurDeptManager = Session["LcRefId"].ToString();
                                    //        details.PurDeptManagerStat = "1";
                                    //        BLL.SRF_TRANSACTION_ApprovedPurchasingDeptManager(details);

                                    //    }
                                    //}                                    

                                }
                                catch (Exception ex)
                                {
                                    isError = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                                }
                            }

                            // PURCHASING DEPARTMENT MANAGER
                            if (Session["aType"].ToString().ToLower().Trim() == "purchasing_dept_manager".ToLower())
                            {
                                try
                                {
                                    
                                    if (ibApproved.ImageUrl == "~/images/A2.png")
                                    {
                                        if (!string.IsNullOrEmpty(BLL.SRF_TRANSACTION_IsApprovedByProdManager(lblCtrl.Text.Trim())))
                                        {
                                            details.CtrlNo = lblCtrl.Text.Trim();
                                            details.ReqManagerStat = "1";
                                            details.ReqManager = Session["LcRefId"].ToString();
                                            details.Remarks = txtRemarks.Text.Length > 0 ? txtRemarks.Text : "";
                                            BLL.SRF_TRANSACTION_ApprovedRequestorManager(details);
                                            approvedItems += lblCtrl.Text.Trim() + ", ";
                                        }
                                        else
                                        {
                                            details.PurDeptManagerStat = "1";
                                            details.CtrlNo = lblCtrl.Text.Trim();
                                            details.PurDeptManager = Session["LcRefId"].ToString();
                                            details.Remarks = txtRemarks.Text.Length > 0 ? txtRemarks.Text : "";
                                            BLL.SRF_TRANSACTION_ApprovedPurchasingDeptManager(details);
                                            approvedItems += lblCtrl.Text.Trim() + ", ";
                                        }

                                        // CHECK IF HAS GP-NUMBER THEN AUTO APPROVED IN IMPEX SIDE
                                        if (!string.IsNullOrEmpty(txtGPNumber.Text) || !String.IsNullOrEmpty(txtGPNumber.Text))
                                        {

                                            details.CtrlNo = lblCtrl.Text.Trim();
                                            details.PurImpex = Session["LcRefId"].ToString();
                                            details.PurImpexStat = "1";

                                            details.Remarks = txtRemarks.Text.Length > 0 ? txtRemarks.Text : "";
                                            BLL.SRF_TRANSACTION_ApprovedPurchasingManager(details);

                                        }

                                    }
                                    if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                                    {
                                        if (!string.IsNullOrEmpty(BLL.SRF_TRANSACTION_IsApprovedByProdManager(lblCtrl.Text.Trim())))
                                        {
                                            details.CtrlNo = lblCtrl.Text.Trim();
                                            details.ReqManagerStat = "2";
                                            details.ReqManager = Session["LcRefId"].ToString();
                                            details.Remarks = txtRemarks.Text.Length > 0 ? txtRemarks.Text : "";
                                            BLL.SRF_TRANSACTION_ApprovedRequestorManager(details);
                                            approvedItems += lblCtrl.Text.Trim() + ", ";
                                        }
                                        else
                                        {
                                            details.PurDeptManagerStat = "2";
                                            details.CtrlNo = lblCtrl.Text.Trim();
                                            details.PurDeptManager = Session["LcRefId"].ToString();
                                            details.Remarks = txtRemarks.Text.Length > 0 ? txtRemarks.Text : "";
                                            BLL.SRF_TRANSACTION_ApprovedPurchasingDeptManager(details);
                                            approvedItems += lblCtrl.Text.Trim() + ", ";

                                            // SET PURCHASING INCHARGE TO NULL & APPROVED = 0
                                            Entities_SRF_RequestEntry buyerApproved = new Entities_SRF_RequestEntry();
                                            buyerApproved.CtrlNo = lblCtrl.Text.Trim();
                                            buyerApproved.PurIncharge = DBNull.Value.ToString();
                                            buyerApproved.PurInchargeStat = "0";
                                            buyerApproved.Remarks = txtRemarks.Text.Length > 0 ? txtRemarks.Text : "";
                                            BLL.SRF_TRANSACTION_ApprovedPurchasingIncharge(buyerApproved);
                                        }

                                        
                                    }

                                    
                                }
                                catch (Exception ex)
                                {
                                    isError = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                                }
                            }

                            // IMPEX
                            if (Session["aType"].ToString().ToLower().Trim() == "purchasing_manager".ToLower())
                            {
                                try
                                {
                                    //if (string.IsNullOrEmpty(lblPezaNonPeza.Text))
                                    //{
                                    //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('Unable to approved! Kindly ask the assigned buyer of this request to update Peza/NonPeza field of supplier " + lblSupplier.Text.ToUpper() + "');", true);
                                    //}
                                    //else
                                    //{

                                        //List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
                                        //list = BLL.SRF_TRANSACTION_RequestEntry_ByControlNo(lblCtrl.Text.Trim());

                                        //if (list.Count > 0)
                                        //{
                                        //    foreach (Entities_SRF_RequestEntry entity in list)
                                        //    {
                                        //        if (string.IsNullOrEmpty(entity.Loa8106))
                                        //        {
                                        //            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('LOA 8106 NO. NOTIFICATION! Please make sure that LOA 8106 Number is entered before approving items. Click the SRF Number to open the record then update accordingly!');", true);
                                        //        }
                                        //        else
                                        //        {
                                        //            details.CtrlNo = lblCtrl.Text.Trim();
                                        //            details.PurImpex = Session["LcRefId"].ToString();
                                        //            if (ibApproved.ImageUrl == "~/images/A2.png")
                                        //            {
                                        //                details.PurImpexStat = "1";
                                        //            }
                                        //            if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                                        //            {
                                        //                details.PurImpexStat = "2";
                                        //            }

                                        //            details.Remarks = txtRemarks.Text.Length > 0 ? txtRemarks.Text : "";
                                        //            BLL.SRF_TRANSACTION_ApprovedPurchasingManager(details);


                                        //            approvedItems += lblCtrl.Text.Trim() + ", ";
                                        //        }
                                        //    }
                                        //}



                                        if ((string.IsNullOrEmpty(txtLOA8106No.Text) || COMMON.IsNullOrWhiteSpace(txtLOA8106No.Text)) && ibDisapproved.ImageUrl != "~/images/DA2.png")
                                        {
                                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('LOA 8106 NO. NOTIFICATION! Please make sure that LOA 8106 Number is entered before approving items!');", true);
                                        }
                                        else
                                        {

                                            details.CtrlNo = lblCtrl.Text.Trim();
                                            details.PurImpex = Session["LcRefId"].ToString();
                                            if (ibApproved.ImageUrl == "~/images/A2.png")
                                            {
                                                details.PurImpexStat = "1";
                                            }
                                            if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                                            {
                                                details.PurImpexStat = "2";
                                            }

                                            details.Remarks = txtRemarks.Text.Length > 0 ? txtRemarks.Text : "";
                                            BLL.SRF_TRANSACTION_ApprovedPurchasingManager(details);


                                            approvedItems += lblCtrl.Text.Trim() + ", ";


                                            ///////////////////////////////////////////////////////////////////////////////////////////////////////
                                            // UPDATE LOA8106
                                            string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                                            string query1 = string.Empty;
                                            string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                                            string query_Success = string.Empty;

                                            query1 = "UPDATE SRF_TRANSACTION_Request SET LOA8106 = '" + txtLOA8106No.Text + "' WHERE CTRLNo = '" + lblCtrl.Text.Trim() + "'";

                                            query_Success = BLL.SRF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                                            if (int.Parse(query_Success) > 0)
                                            {
                                                // DO NOTHING
                                            }

                                            ///////////////////////////////////////////////////////////////////////////////////////////////////////



                                        }

                                       


                                    //}
                                    
                                }
                                catch (Exception ex)
                                {
                                    isError = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                                }
                            }


                        }

                        //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + lblCtrl.Text + " - " + ibApproved.ImageUrl + " - " + ibDisapproved.ImageUrl + "');", true);
                    }

                    if (!isError)
                    {
                        Session["aType"] = "";
                        Session["successMessage"] = "THE FOLLOWING ITEMS HAS BEEN SUCCESSFULLY APPROVED: <b>" + approvedItems.Remove(approvedItems.Length - 2, 2) + "</b>";
                        Session["successTransactionName"] = "SRF_APPROVALFORM";
                        Session["successReturnPage"] = "SRF_ApprovalForm.aspx";

                        Response.Redirect("SuccessPage.aspx");
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
                ImageButton ibApproved = row.FindControl("ibApproved") as ImageButton;
                ImageButton ibDisapproved = row.FindControl("ibDisapproved") as ImageButton;
                TextBox txtRemarks = row.FindControl("txtRemarks") as TextBox;
                TextBox txtGPNumber = row.FindControl("txtGPNumber") as TextBox;
                TextBox txtLOA8106No = row.FindControl("txtLOA8106No") as TextBox;
                GridView gvDataDetails = row.FindControl("gvDataDetails") as GridView;
                GridView gvDetails1 = row.FindControl("gvDetails1") as GridView;
                GridView gvDetails2 = row.FindControl("gvDetails2") as GridView;
                GridView gvDetails3 = row.FindControl("gvDetails3") as GridView;
                GridView gvDetails4 = row.FindControl("gvDetails4") as GridView;
                GridView gvPullOut = row.FindControl("gvPullOut") as GridView;
                Label lblPezaNonPeza = row.FindControl("lblPezaNonPeza") as Label;
                Label lblSupplier = row.FindControl("lblSupplier") as Label;


                if (e.CommandName == "lblCtrl_Command")
                {
                    string URL = "~/SRF_RequestEntry.aspx?SRF_ControlNo_From_Details=" + CryptorEngine.Encrypt(lblCtrl.Text.Trim(), true);

                    URL = Page.ResolveClientUrl(URL);
                    lblCtrl.OnClientClick = "window.open('" + URL + "'); return true;";
                }
                if (e.CommandName == "A_Command")
                {
                    if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                    {
                        ibApproved.ImageUrl = "~/images/A1.png";
                        txtGPNumber.Text = string.Empty;
                        txtGPNumber.Enabled = false;
                    }
                    else
                    {
                        if (ibApproved.ImageUrl == "~/images/A1.png")
                        {
                            ibApproved.ImageUrl = "~/images/A2.png";


                            Entities_SRF_RequestEntry entity = new Entities_SRF_RequestEntry();
                            entity.CtrlNo = lblCtrl.Text.Trim();

                            // POPULATE MAIN
                            List<Entities_SRF_RequestEntry> approved = new List<Entities_SRF_RequestEntry>();
                            approved = BLL.SRF_TRANSACTION_RequestStatus_ByControlNo(lblCtrl.Text.Trim());

                            if (approved.Count > 0)
                            {
                                List<Entities_SRF_RequestEntry> approvedFinal = new List<Entities_SRF_RequestEntry>();
                                foreach (Entities_SRF_RequestEntry entityFinal in approved)
                                {
                                    Entities_SRF_RequestEntry eFinal = new Entities_SRF_RequestEntry();
                                    eFinal.ReqInchargeName = !string.IsNullOrEmpty(entityFinal.ReqInchargeName) ? CryptorEngine.Decrypt(entityFinal.ReqInchargeName, true) + "<br/>" + entityFinal.ReqInchargeDOA : "PENDING";
                                    eFinal.ReqManagerName = !string.IsNullOrEmpty(entityFinal.ReqManagerName) ? CryptorEngine.Decrypt(entityFinal.ReqManagerName, true) + "<br/>" + entityFinal.ReqManagerDOA : "PENDING";
                                    eFinal.PurInchargeName = !string.IsNullOrEmpty(entityFinal.PurInchargeName) ? CryptorEngine.Decrypt(entityFinal.PurInchargeName, true) + "<br/>" + entityFinal.PurInchargeDOA : "PENDING";
                                    eFinal.PurManagerName = !string.IsNullOrEmpty(entityFinal.PurManagerName) ? CryptorEngine.Decrypt(entityFinal.PurManagerName, true) + "<br/>" + entityFinal.PurImpexDOA : "PENDING";
                                    eFinal.PurDeptManagerName = !string.IsNullOrEmpty(entityFinal.PurDeptManagerName) ? CryptorEngine.Decrypt(entityFinal.PurDeptManagerName, true) + "<br/>" + entityFinal.PurDeptManagerDOA : "PENDING";

                                    approvedFinal.Add(eFinal);

                                }

                                if (approvedFinal.Count > 0)
                                {
                                    gvDetails1.Visible = true;
                                    gvDetails1.DataSource = approvedFinal;
                                    gvDetails1.DataBind();
                                }
                            }

                            string problemEncountered = string.Empty;

                            // POPULATE ITEM DETAILS                            
                            List<Entities_SRF_RequestEntry> details = new List<Entities_SRF_RequestEntry>();
                            details = BLL.SRF_TRANSACTION_RequestEntry_Details_ByControlNo(entity);

                            if (details.Count > 0)
                            {
                                gvDataDetails.Visible = true;
                                gvDataDetails.DataSource = details;
                                gvDataDetails.DataBind();

                                foreach (Entities_SRF_RequestEntry PO_Specs in details)
                                {
                                    problemEncountered = PO_Specs.Specification;
                                }

                            }

                            // DETAILS 2,3
                            List<Entities_SRF_RequestEntry> listDetails2 = new List<Entities_SRF_RequestEntry>();
                            listDetails2 = BLL.SRF_TRANSACTION_RequestEntry_ByControlNo(lblCtrl.Text.Trim());

                            if (listDetails2.Count > 0)
                            {
                                gvDetails2.Visible = true;
                                gvDetails2.DataSource = listDetails2;
                                gvDetails2.DataBind();

                                gvDetails3.Visible = true;
                                gvDetails3.DataSource = listDetails2;
                                gvDetails3.DataBind();

                                gvDetails4.Visible = true;
                                gvDetails4.DataSource = listDetails2;
                                gvDetails4.DataBind();

                                foreach (Entities_SRF_RequestEntry ePullOut in listDetails2)
                                {
                                    // PULLOUT
                                    if (ePullOut.ProblemEncountered.ToUpper() == "RE-USE CONTAINER TUBES" || ePullOut.ProblemEncountered.ToUpper() == "RE-USE IC TRAYS")
                                    {
                                        List<Entities_SRF_PO_Entry> listPullOut = new List<Entities_SRF_PO_Entry>();
                                        listPullOut = BLL.SRF_TRANSACTION_PO_TOP_10_REQUEST(ePullOut.TransactionDate, problemEncountered, lblCtrl.Text.Trim());

                                        if (listPullOut != null)
                                        {
                                            if (listPullOut.Count > 0)
                                            {
                                                gvPullOut.Visible = true;
                                                gvPullOut.DataSource = listPullOut;
                                                gvPullOut.DataBind();
                                            }

                                        }

                                    }

                                }


                            }

                            

                            if (Session["aType"].ToString().ToLower().Trim() == "purchasing_incharge".ToLower())
                            {
                                if (string.IsNullOrEmpty(lblPezaNonPeza.Text))
                                {
                                    txtGPNumber.Enabled = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('Unable to approved! Kindly update Peza/NonPeza field of supplier " + lblSupplier.Text.ToUpper() + " to proceed!');", true);
                                }
                                else
                                {
                                    txtGPNumber.Enabled = true;
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('GATE PASS NO. NOTIFICATION! Please make sure to check GPNO before submitting items.');", true);
                                }
                            }

                            if (Session["aType"].ToString().ToLower().Trim() == "purchasing_manager".ToLower())
                            {
                                txtLOA8106No.Enabled = true;
                                //if (string.IsNullOrEmpty(lblPezaNonPeza.Text))
                                //{
                                //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('Unable to approved! Kindly ask the assigned buyer of this request to update Peza/NonPeza field of supplier " + lblSupplier.Text.ToUpper() + "');", true);
                                //}
                                //else
                                //{
                                //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('LOA 8106 NO. NOTIFICATION! Please make sure that LOA 8106 Number is entered before approving items. Click the SRF Number to open the record then update accordingly!');", true);
                                //}

                                //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('LOA 8106 NO. NOTIFICATION! Please make sure that LOA 8106 Number is entered before approving items. Click the SRF Number to open the record then update accordingly!');", true);
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('LOA 8106 NO. NOTIFICATION! Please make sure that LOA 8106 Number is entered before approving items!');", true);
                            }

                        }
                        else
                        {
                            ibApproved.ImageUrl = "~/images/A1.png";
                            txtGPNumber.Text = string.Empty;
                            txtGPNumber.Enabled = false;

                            gvDataDetails.Visible = false;
                            gvDetails1.Visible = false;
                            gvDetails2.Visible = false;
                            gvDetails3.Visible = false;
                            gvDetails4.Visible = false;
                            gvPullOut.Visible = false;

                        }
                    }
                }
                if (e.CommandName == "DA_Command")
                {
                    if (ibApproved.ImageUrl == "~/images/A2.png")
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
                        }
                        else
                        {
                            ibDisapproved.ImageUrl = "~/images/DA1.png";
                            txtRemarks.Text = string.Empty;
                            txtRemarks.Enabled = false;
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
                    Label lblSupplier = (Label)e.Row.FindControl("lblSupplier");

                    lblSupplier.Text = lblSupplier.Text.Length >= 23 ? lblSupplier.Text.Substring(0, 23).ToString() + "..." : lblSupplier.Text;

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
        }


        private void SRF_TRANSACTION_RequestEntry_Fill_All_DropdownList()
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
            list = BLL.SRF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(item => item.DropdownName).ToList();

            ddCategory.Items.Add("ALL");

            foreach (Entities_SRF_RequestEntry entity in list)
            {
                ListItem item = new ListItem();
                item.Text = entity.DropdownName;
                item.Value = entity.DropdownRefId;

                if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                {
                    if (entity.TableName == "MT_Category")
                    {
                        ddCategory.Items.Add(item);
                    }

                }
            }

            //---------------------------------------------------------------------------------------------------
            string category = Session["CategoryAccess"].ToString();
            if (!string.IsNullOrEmpty(category))
            {
                if (int.Parse(category) > 0)
                {

                    if (Session["APPROVAL_OTHER_BUYERS"] != null)
                    {
                        if (!string.IsNullOrEmpty(Session["APPROVAL_OTHER_BUYERS"].ToString()))
                        {
                            ddCategory.Enabled = true;
                            ddCategory.Items.FindByValue(Session["APPROVAL_OTHER_BUYERS"].ToString()).Selected = true;
                        }
                    }
                    else
                    {
                        ddCategory.Items.FindByValue(category).Selected = true;
                        ddCategory.Enabled = false;
                    }
                }
                else
                {
                    ddCategory.Items.FindByText("ALL").Selected = true;
                    ddCategory.Enabled = true;
                }
            }

            //---------------------------------------------------------------------------------------------------


        }



    }
}
