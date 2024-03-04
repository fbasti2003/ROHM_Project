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
    public partial class DRF_ApprovalForm : System.Web.UI.Page
    {
        BLL_DRF BLL = new BLL_DRF();
        BLL_RFQ BLL_RFQ = new BLL_RFQ();
        Common COMMON = new Common();
        public string displayAttachment = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    txtFrom.Text = DateTime.Today.AddDays(-1825).ToString("MM/dd/yyyy");
                    txtTo.Text = DateTime.Today.ToString("MM/dd/yyyy");

                    //---------------------------------------------------------------------------------------------------
                    List<Entities_DRF_RequestEntry> listDropDown = new List<Entities_DRF_RequestEntry>();
                    listDropDown = BLL.DRF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                    if (listDropDown != null)
                    {
                        if (listDropDown.Count > 0)
                        {
                            ddCategory.Items.Clear();
                            ddCategory.Items.Add("");

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

                            //if (!string.IsNullOrEmpty(Session["CategoryAccess"].ToString()))
                            //{
                            //    if (int.Parse(Session["CategoryAccess"].ToString()) > 0)
                            //    {
                            //        ddCategory.Items.FindByValue(Session["CategoryAccess"].ToString()).Selected = true;
                            //        ddCategory.Enabled = false;
                            //    }
                            //}

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
                                    //ddCategory.Items.FindByText("ALL CATEGORY").Selected = true;
                                    ddCategory.Enabled = true;
                                }
                            }

                            //---------------------------------------------------------------------------------------------------


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
                bool productionManager = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["ProductionApprovalAccess"].ToString().Trim());
                bool purchasingBuyer = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim());
                bool purchasingIncharge= COMMON.isUserAllowed(Session["LcRefId"].ToString(), "15");
                bool purchasingDeptManager= COMMON.isUserAllowed(Session["LcRefId"].ToString(), "16");
                bool purchasingDivManager = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "17");                


                List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();
                Entities_DRF_RequestEntry request = new Entities_DRF_RequestEntry();

                request.DrFrom = txtFrom.Text;
                request.DrTo = txtTo.Text;

                // PRODUCTION MANAGER
                if (productionManager && !purchasingBuyer)
                {
                    if (!string.IsNullOrEmpty(txtDRFNo.Text) || txtDRFNo.Text.Length > 0)
                    {
                        Session["Search_From_DRF_Inquiry"] = txtDRFNo.Text;
                        Response.Redirect("DRF_AllRequest.aspx");
                    }
                    else
                    {
                        if (ddCategory.SelectedItem.Text.ToLower() == "")
                        {
                            if (ddStatus.SelectedValue == "ALL")
                            {
                                list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "0" && itm.LcDepartment == Session["Department"].ToString()).ToList();
                            }
                            else
                            {
                                list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "0" && itm.LcDepartment == Session["Department"].ToString() && itm.StatAll == ddStatus.SelectedValue).ToList();
                            }
                        }
                        else
                        {
                            if (ddStatus.SelectedValue == "ALL")
                            {
                                list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "0" && itm.LcDepartment == Session["Department"].ToString() && itm.CategoryId == ddCategory.SelectedValue.Trim()).ToList();
                            }
                            else
                            {
                                list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "0" && itm.LcDepartment == Session["Department"].ToString() && itm.CategoryId == ddCategory.SelectedValue.Trim() && itm.StatAll == ddStatus.SelectedValue).ToList();
                            }
                        }
                    }
                }

                // PUCHASING BUYER OR INCHARGE
                if (purchasingBuyer && !purchasingIncharge && !purchasingDeptManager && !purchasingDivManager)
                {
                    if (!string.IsNullOrEmpty(txtDRFNo.Text) || txtDRFNo.Text.Length > 0)
                    {
                        Session["Search_From_DRF_Inquiry"] = txtDRFNo.Text;
                        Response.Redirect("DRF_AllRequest.aspx");
                    }
                    else
                    {
                        if (ddCategory.SelectedItem.Text.ToLower() == "")
                        {
                            if (Session["Search_From_DRF_AllRequest"] != null)
                            {
                                if (!string.IsNullOrEmpty(Session["Search_From_DRF_AllRequest"].ToString()))
                                {
                                    list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "0" && (itm.PurManagerStat == "0" || itm.PurManagerStat == "2") && itm.CtrlNo == Session["Search_From_DRF_AllRequest"].ToString().Trim()).ToList();
                                }
                            }
                            else
                            {
                                if (ddStatus.SelectedValue == "ALL")
                                {
                                    list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "0" && (itm.PurManagerStat == "0" || itm.PurManagerStat == "2")).ToList();
                                }
                                else
                                {
                                    list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "0" && (itm.PurManagerStat == "0" || itm.PurManagerStat == "2") && itm.StatAll == ddStatus.SelectedValue).ToList();
                                }
                            }
                        }
                        else
                        {
                            if (Session["Search_From_DRF_AllRequest"] != null)
                            {
                                if (!string.IsNullOrEmpty(Session["Search_From_DRF_AllRequest"].ToString()))
                                {
                                    list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "0" && (itm.PurManagerStat == "0" || itm.PurManagerStat == "2") && itm.CtrlNo == Session["Search_From_DRF_AllRequest"].ToString().Trim() && itm.CategoryId == ddCategory.SelectedValue.Trim()).ToList();
                                }
                            }
                            else
                            {
                                if (ddStatus.SelectedValue == "ALL")
                                {
                                    list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "0" && (itm.PurManagerStat == "0" || itm.PurManagerStat == "2") && itm.CategoryId == ddCategory.SelectedValue.Trim()).ToList();
                                }
                                else
                                {
                                    list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "0" && (itm.PurManagerStat == "0" || itm.PurManagerStat == "2") && itm.CategoryId == ddCategory.SelectedValue.Trim() && itm.StatAll == ddStatus.SelectedValue).ToList();
                                }
                            }
                        }


                        //// FOR BUYER REQUEST THAT NEEDS TO APPROVED BY HIS/HER MANAGER
                        //List<Entities_DRF_RequestEntry> listBuyerExclusive2 = new List<Entities_DRF_RequestEntry>();
                        //listBuyerExclusive2 = BLL.DRF_TRANSACTION_Approval_DateRange().Where(itm => itm.ReqManagerStat == "0" && (itm.PurInchargeStat == "0" || itm.PurManagerStat == "2") && itm.LcDepartment == Session["Department"].ToString()).ToList();

                        //if (listBuyerExclusive2 != null)
                        //{
                        //    if (listBuyerExclusive2.Count > 0)
                        //    {

                        //        foreach (Entities_DRF_RequestEntry eb in listBuyerExclusive2)
                        //        {
                        //            Entities_DRF_RequestEntry eBuyer2 = new Entities_DRF_RequestEntry();
                        //            eBuyer2.AbnormalQuantity = eb.AbnormalQuantity;
                        //            eBuyer2.Attachment1 = eb.Attachment1;
                        //            eBuyer2.Attachment2 = eb.Attachment2;
                        //            eBuyer2.Attachment3 = eb.Attachment3;
                        //            eBuyer2.Attachment4 = eb.Attachment4;
                        //            eBuyer2.Attachment5 = eb.Attachment5;
                        //            eBuyer2.Attachment6 = eb.Attachment6;
                        //            eBuyer2.Attachment7 = eb.Attachment7;
                        //            eBuyer2.Attachment8 = eb.Attachment8;
                        //            eBuyer2.Attention = eb.Attention;
                        //            eBuyer2.Category = eb.Category;
                        //            eBuyer2.CategoryId = eb.CategoryId;
                        //            eBuyer2.CssColorCode = eb.CssColorCode;
                        //            eBuyer2.CtrlNo = eb.CtrlNo;
                        //            eBuyer2.Description = eb.Description;
                        //            eBuyer2.DetailedReport = eb.DetailedReport;
                        //            eBuyer2.DrFrom = eb.DrFrom;
                        //            eBuyer2.DropdownName = eb.DropdownName;
                        //            eBuyer2.DropdownRefId = eb.DropdownRefId;
                        //            eBuyer2.DrTo = eb.DrTo;
                        //            eBuyer2.InvoiceDRNO = eb.InvoiceDRNO;
                        //            eBuyer2.IsDisabled = eb.IsDisabled;
                        //            eBuyer2.LcDepartment = eb.LcDepartment;
                        //            eBuyer2.OrderQuantity = eb.OrderQuantity;
                        //            eBuyer2.PoDate = eb.PoDate;
                        //            eBuyer2.PrNO = eb.PrNO;
                        //            eBuyer2.PoNO = eb.PoNO;
                        //            eBuyer2.PurIncharge = eb.PurIncharge;
                        //            eBuyer2.PurInchargeDOA = eb.PurInchargeDOA;
                        //            eBuyer2.PurInchargeStat = eb.PurInchargeStat;
                        //            eBuyer2.PurManager = eb.PurManager;
                        //            eBuyer2.PurManagerDOA = eb.PurManagerDOA;
                        //            eBuyer2.PurManagerStat = eb.PurManagerStat;
                        //            eBuyer2.ReceivedDate = eb.ReceivedDate;
                        //            eBuyer2.RefId = eb.RefId;
                        //            eBuyer2.ReqManager = eb.ReqManager;
                        //            eBuyer2.ReqManagerDOA = eb.ReqManagerDOA;
                        //            eBuyer2.ReqManagerStat = eb.ReqManagerStat;
                        //            eBuyer2.Requester = eb.Requester;
                        //            eBuyer2.RequesterEmail = eb.RequesterEmail;
                        //            eBuyer2.RequesterLocalNumber = eb.RequesterLocalNumber;
                        //            eBuyer2.RequesterS = eb.RequesterS;
                        //            eBuyer2.RequesterSDOA = eb.RequesterSDOA;
                        //            eBuyer2.RequesterSStat = eb.RequesterSStat;
                        //            eBuyer2.StatAll = eb.StatAll;
                        //            eBuyer2.StatRemarks = eb.StatRemarks;
                        //            eBuyer2.Supplier = eb.Supplier;
                        //            eBuyer2.SupplierResponded = eb.SupplierResponded;
                        //            eBuyer2.TableName = eb.TableName;
                        //            eBuyer2.TransactionDate = eb.TransactionDate;
                        //            eBuyer2.TypeDrawingNo = eb.TypeDrawingNo;
                        //            eBuyer2.TypesOfAbnormality = eb.TypesOfAbnormality;
                        //            eBuyer2.UpdatedBy = eb.UpdatedBy;
                        //            eBuyer2.UpdatedDate = eb.UpdatedDate;

                        //            list.Add(eBuyer2);
                        //        }
                        //    }
                        //}


                    }
                }

                // PURCHASING MANAGER
                if ((purchasingIncharge || purchasingDeptManager || purchasingDivManager) && (list == null || list.Count <= 0))
                {
                    if (!string.IsNullOrEmpty(txtDRFNo.Text) || txtDRFNo.Text.Length > 0)
                    {
                        Session["Search_From_DRF_Inquiry"] = txtDRFNo.Text;
                        Response.Redirect("DRF_AllRequest.aspx");
                    }
                    else
                    {
                        if (ddCategory.SelectedItem.Text.ToLower() == "")
                        {
                            if (Session["Search_From_DRF_AllRequest"] != null)
                            {
                                if (!string.IsNullOrEmpty(Session["Search_From_DRF_AllRequest"].ToString()))
                                {
                                    list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "1" && itm.PurManagerStat == "0" && itm.CtrlNo == Session["Search_From_DRF_AllRequest"].ToString().Trim()).ToList();
                                }
                            }
                            else
                            {
                                if (Session["Username"].ToString() == "1402" || Session["Username"].ToString() == "0286") //SIR GEORGE & MAM SYLVIA CARAVANA
                                {
                                    if (ddStatus.SelectedValue == "ALL")
                                    {
                                        list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "0" || itm.PurManagerStat == "0").ToList().Where(abc => abc.PurInchargeStat != "2" && abc.ReqManagerStat != "2" && abc.PurManagerStat != "2").ToList();
                                    }
                                    else
                                    {
                                        list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "0" || itm.PurManagerStat == "0" && itm.StatAll == ddStatus.SelectedValue).ToList().Where(abc => abc.PurInchargeStat != "2" && abc.ReqManagerStat != "2" && abc.PurManagerStat != "2" && abc.StatAll == ddStatus.SelectedValue).ToList();
                                    }
                                }
                                else
                                {
                                    if (ddStatus.SelectedValue == "ALL")
                                    {
                                        list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "1" && itm.PurManagerStat == "0").ToList();
                                    }
                                    else
                                    {
                                        list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "1" && itm.PurManagerStat == "0" && itm.StatAll == ddStatus.SelectedValue).ToList();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Session["Search_From_DRF_AllRequest"] != null)
                            {
                                if (!string.IsNullOrEmpty(Session["Search_From_DRF_AllRequest"].ToString()))
                                {
                                    list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "1" && itm.PurManagerStat == "0" && itm.CtrlNo == Session["Search_From_DRF_AllRequest"].ToString().Trim() && itm.CategoryId == ddCategory.SelectedValue.Trim()).ToList();
                                }
                            }
                            else
                            {
                                if (Session["Username"].ToString() == "1402" || Session["Username"].ToString() == "0286") //SIR GEORGE & MAM SYLVIA CARAVANA
                                {
                                    if (ddStatus.SelectedValue == "ALL")
                                    {
                                        list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "0" || itm.PurManagerStat == "0" && itm.CategoryId.Trim() == ddCategory.SelectedValue.Trim()).ToList().Where(abc => abc.PurInchargeStat != "2" && abc.ReqManagerStat != "2" && abc.PurManagerStat != "2" && abc.CategoryId.Trim() == ddCategory.SelectedValue.Trim()).ToList();
                                    }
                                    else
                                    {
                                        list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "0" || itm.PurManagerStat == "0" && itm.CategoryId.Trim() == ddCategory.SelectedValue.Trim() && itm.StatAll == ddStatus.SelectedValue).ToList().Where(abc => abc.PurInchargeStat != "2" && abc.ReqManagerStat != "2" && abc.PurManagerStat != "2" && abc.CategoryId.Trim() == ddCategory.SelectedValue.Trim() && abc.StatAll == ddStatus.SelectedValue).ToList();
                                    }
                                }
                                else
                                {
                                    if (ddStatus.SelectedValue == "ALL")
                                    {
                                        list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "1" && itm.PurManagerStat == "0" && itm.CategoryId.Trim() == ddCategory.SelectedValue.Trim()).ToList();
                                    }
                                    else
                                    {
                                        list = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "1" && itm.PurManagerStat == "0" && itm.CategoryId.Trim() == ddCategory.SelectedValue.Trim() && itm.StatAll == ddStatus.SelectedValue).ToList();
                                    }
                                }
                            }
                        }

                        // FOR BUYER REQUEST THAT NEEDS TO APPROVED BY HIS/HER MANAGER
                        List<Entities_DRF_RequestEntry> listBuyerExclusive = new List<Entities_DRF_RequestEntry>();
                        if (ddStatus.SelectedValue == "ALL")
                        {
                            listBuyerExclusive = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.PurManagerStat == "0" && itm.LcDepartment == Session["Department"].ToString()).ToList();
                        }
                        else
                        {
                            listBuyerExclusive = BLL.DRF_TRANSACTION_Approval_DateRange(request).Where(itm => itm.PurManagerStat == "0" && itm.LcDepartment == Session["Department"].ToString() && itm.StatAll == ddStatus.SelectedValue).ToList();
                        }

                        if (listBuyerExclusive != null)
                        {
                            if (listBuyerExclusive.Count > 0)
                            {

                                foreach (Entities_DRF_RequestEntry eb in listBuyerExclusive)
                                {
                                    Entities_DRF_RequestEntry eBuyer = new Entities_DRF_RequestEntry();
                                    eBuyer.AbnormalQuantity = eb.AbnormalQuantity;
                                    eBuyer.Attachment1 = eb.Attachment1;
                                    eBuyer.Attachment2 = eb.Attachment2;
                                    eBuyer.Attachment3 = eb.Attachment3;
                                    eBuyer.Attachment4 = eb.Attachment4;
                                    eBuyer.Attachment5 = eb.Attachment5;
                                    eBuyer.Attachment6 = eb.Attachment6;
                                    eBuyer.Attachment7 = eb.Attachment7;
                                    eBuyer.Attachment8 = eb.Attachment8;
                                    eBuyer.Attention = eb.Attention;
                                    eBuyer.Category = eb.Category;
                                    eBuyer.CategoryId = eb.CategoryId;
                                    eBuyer.CssColorCode = eb.CssColorCode;
                                    eBuyer.CtrlNo = eb.CtrlNo;
                                    eBuyer.Description = eb.Description;
                                    eBuyer.DetailedReport = eb.DetailedReport;
                                    eBuyer.DrFrom = eb.DrFrom;
                                    eBuyer.DropdownName = eb.DropdownName;
                                    eBuyer.DropdownRefId = eb.DropdownRefId;
                                    eBuyer.DrTo = eb.DrTo;
                                    eBuyer.InvoiceDRNO = eb.InvoiceDRNO;
                                    eBuyer.IsDisabled = eb.IsDisabled;
                                    eBuyer.LcDepartment = eb.LcDepartment;
                                    eBuyer.OrderQuantity = eb.OrderQuantity;
                                    eBuyer.PoDate = eb.PoDate;
                                    eBuyer.PrNO = eb.PrNO;
                                    eBuyer.PoNO = eb.PoNO;
                                    eBuyer.PurIncharge = eb.PurIncharge;
                                    eBuyer.PurInchargeDOA = eb.PurInchargeDOA;
                                    eBuyer.PurInchargeStat = eb.PurInchargeStat;
                                    eBuyer.PurManager = eb.PurManager;
                                    eBuyer.PurManagerDOA = eb.PurManagerDOA;
                                    eBuyer.PurManagerStat = eb.PurManagerStat;
                                    eBuyer.ReceivedDate = eb.ReceivedDate;
                                    eBuyer.RefId = eb.RefId;
                                    eBuyer.ReqManager = eb.ReqManager;
                                    eBuyer.ReqManagerDOA = eb.ReqManagerDOA;
                                    eBuyer.ReqManagerStat = eb.ReqManagerStat;
                                    eBuyer.Requester = eb.Requester;
                                    eBuyer.RequesterEmail = eb.RequesterEmail;
                                    eBuyer.RequesterLocalNumber = eb.RequesterLocalNumber;
                                    eBuyer.RequesterS = eb.RequesterS;
                                    eBuyer.RequesterSDOA = eb.RequesterSDOA;
                                    eBuyer.RequesterSStat = eb.RequesterSStat;
                                    eBuyer.StatAll = eb.StatAll;
                                    eBuyer.StatRemarks = eb.StatRemarks;
                                    eBuyer.Supplier = eb.Supplier;
                                    eBuyer.SupplierResponded = eb.SupplierResponded;
                                    eBuyer.TableName = eb.TableName;
                                    eBuyer.TransactionDate = eb.TransactionDate;
                                    eBuyer.TypeDrawingNo = eb.TypeDrawingNo;
                                    eBuyer.TypesOfAbnormality = eb.TypesOfAbnormality;
                                    eBuyer.UpdatedBy = eb.UpdatedBy;
                                    eBuyer.UpdatedDate = eb.UpdatedDate;

                                    if (string.IsNullOrEmpty(eb.ReqManager))
                                    {
                                        list.Add(eBuyer);
                                    }
                                }
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

                        //EXPORT TO EXCEL
                        List<Entities_DRF_RequestEntry> finalListExport = new List<Entities_DRF_RequestEntry>();

                        foreach (Entities_DRF_RequestEntry entity in list)
                        {
                            List<Entities_DRF_RequestEntry> listExport = new List<Entities_DRF_RequestEntry>();
                            listExport = BLL.DRF_TRANSACTION_AllRequest_ByCTRLNo(entity);

                            if (listExport != null)
                            {
                                if (listExport.Count > 0)
                                {
                                    foreach (Entities_DRF_RequestEntry le in listExport)
                                    {
                                        Entities_DRF_RequestEntry final = new Entities_DRF_RequestEntry();
                                        final.CtrlNo = le.CtrlNo;
                                        final.Attention = le.Attention;
                                        final.Supplier = le.Supplier;
                                        final.Requester = le.Requester;
                                        final.InvoiceDRNO = le.InvoiceDRNO;
                                        final.PrNO = le.PrNO;
                                        final.PoNO = le.PoNO;
                                        final.PoDate = le.PoDate;
                                        final.ReceivedDate = le.ReceivedDate;
                                        final.Category = le.Category;
                                        final.Description = le.Description;
                                        final.TypeDrawingNo = le.TypeDrawingNo;
                                        final.OrderQuantity = le.OrderQuantity;
                                        final.AbnormalQuantity = le.AbnormalQuantity;
                                        final.TypesOfAbnormality = le.TypesOfAbnormality;
                                        final.DetailedReport = le.DetailedReport;

                                        finalListExport.Add(final);
                                    }
                                }
                            }
                        }

                        gvExport.DataSource = finalListExport;
                        gvExport.DataBind();
                    }
                    else
                    {
                        gvData.Visible = false;
                    }
                }

                divOpacity.Style.Add("opacity", "1");
                divLoader.Style.Add("display", "none");

                Session["Search_From_DRF_AllRequest"] = null;
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

                LinkButton lbCTRLNo = row.FindControl("lbCTRLNo") as LinkButton;
                LinkButton lblPreview = row.FindControl("lblPreview") as LinkButton;
                LinkButton lblView = row.FindControl("lblView") as LinkButton;
                Label lblCTRLNo = row.FindControl("lblCTRLNo") as Label;
                ImageButton ibApproved = row.FindControl("ibApproved") as ImageButton;
                ImageButton ibDisapproved = row.FindControl("ibDisapproved") as ImageButton;
                TextBox txtRemarks = row.FindControl("txtRemarks") as TextBox;
                HtmlControl divDetails = row.FindControl("divDetails") as HtmlControl;
                Label lblAttachment1 = row.FindControl("lblAttachment1") as Label;
                Label lblAttachment2 = row.FindControl("lblAttachment2") as Label;
                Label lblAttachment3 = row.FindControl("lblAttachment3") as Label;
                Label lblAttachment4 = row.FindControl("lblAttachment4") as Label;
                HtmlControl divAttachment = row.FindControl("divAttachment") as HtmlControl;

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

                string attachmentLiteralInside = string.Empty;
                string pdfSource = string.Empty;

                string responseType = string.Empty;
                string responseAnswer = string.Empty;


                if (e.CommandName == "lbCTRLNo_Command")
                {
                    //Response.Redirect("DRF_RequestEntry.aspx?DRFNo_From_Inquiry=" + CryptorEngine.Encrypt(lblCTRLNo.Text.Trim(), true), false);

                    string URL = "~/DRF_RequestEntry.aspx?DRFNo_From_Inquiry=" + CryptorEngine.Encrypt(lblCTRLNo.Text.Trim(), true);
                    URL = Page.ResolveClientUrl(URL);
                    lbCTRLNo.OnClientClick = "window.open('" + URL + "'); return false;";
                }

                //if (e.CommandName == "lblPreview_Command")
                //{
                //    Session["Search_From_DRF_Inquiry"] = lblCTRLNo.Text.Trim();
                //    Response.Redirect("DRF_AllRequest.aspx", false);
                //}

                if (e.CommandName == "lblPreview_Command")
                {
                    string pathLocation = Server.MapPath("~/DRF_Request/" + lblCTRLNo.Text.Trim() + "/REPORT_" + lblCTRLNo.Text.Trim() + ".html");
                    string htmlTemplate = Server.MapPath("~/HTML_Report/DRF/DRF.txt");

                    if (System.IO.File.Exists(htmlTemplate))
                    {

                        List<Entities_DRF_RequestEntry> listRequest = new List<Entities_DRF_RequestEntry>();
                        Entities_DRF_RequestEntry entityRequest = new Entities_DRF_RequestEntry();
                        entityRequest.CtrlNo = lblCTRLNo.Text;

                        listRequest = BLL.DRF_TRANSACTION_GetRequestByCTRLNo(entityRequest);

                        if (listRequest != null)
                        {
                            if (listRequest.Count > 0)
                            {
                                foreach (Entities_DRF_RequestEntry eRequest in listRequest)
                                {
                                    if (eRequest.SupplierResponded == "1")
                                    {
                                        responseType = eRequest.ResponseType;
                                        responseAnswer = eRequest.ResponseAnswer;
                                    }
                                }
                            }
                        }

                        string templateValue = System.IO.File.ReadAllText(htmlTemplate).Replace("val_ctrlno", lblCTRLNo.Text.Trim())
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
                                                                                       .Replace("val_responsetype", responseType)
                                                                                       .Replace("val_responseanswer", responseAnswer)
                                                                                       .Replace("val_supplier_conforme", !string.IsNullOrEmpty(responseAnswer) ? lblAttention.Text : string.Empty)
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

                    //Response.Redirect("DRF_Request/" + lblCTRLNo.Text.Trim() + "/REPORT_" + lblCTRLNo.Text.Trim() + ".html", false);

                    string URL = "~/DRF_Request/" + lblCTRLNo.Text.Trim() + "/REPORT_" + lblCTRLNo.Text.Trim() + ".html";
                    URL = Page.ResolveClientUrl(URL);
                    lblPreview.OnClientClick = "window.open('" + URL + "'); return false;";

                }

                if (e.CommandName == "lblView_Command")
                {
                    if (lblView.Text.ToUpper() == "OPEN DETAILS")
                    {
                        divDetails.Style.Add("display", "block");
                        lblView.Text = "CLOSE DETAILS";

                        if (!string.IsNullOrEmpty(lblAttachment1.Text) || !string.IsNullOrEmpty(lblAttachment2.Text) || !string.IsNullOrEmpty(lblAttachment3.Text) || !string.IsNullOrEmpty(lblAttachment4.Text))
                        {
                            pdfSource = ConfigurationManager.AppSettings["DRF_DL_REQUEST_ATTACHMENT_URL"] + lblCTRLNo.Text + "/";

                            if (!string.IsNullOrEmpty(lblAttachment1.Text))
                            {
                                attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment1.Text + "' height='500px' width='980px'></iframe></td></tr></table>";
                            }
                            if (!string.IsNullOrEmpty(lblAttachment2.Text))
                            {
                                attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment2.Text + "' height='500px' width='980px'></iframe></td></tr></table>";
                            }
                            if (!string.IsNullOrEmpty(lblAttachment3.Text))
                            {
                                attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment3.Text + "' height='500px' width='980px'></iframe></td></tr></table>";
                            }
                            if (!string.IsNullOrEmpty(lblAttachment4.Text))
                            {
                                attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment4.Text + "' height='500px' width='980px'></iframe></td></tr></table>";
                            }

                            divAttachment.Style.Add("display", "block");
                            displayAttachment = attachmentLiteralInside;
                        }
                    }
                    else
                    {
                        divDetails.Style.Add("display", "none");
                        lblView.Text = "OPEN DETAILS";

                        divAttachment.Style.Add("display", "none");
                        displayAttachment = string.Empty;
                    }
                }

                if (e.CommandName == "A_Command")
                {
                    if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                    {
                    }
                    else
                    {
                        if (ibApproved.ImageUrl == "~/images/A1.png")
                        {
                            ibApproved.ImageUrl = "~/images/A2.png";

                            divDetails.Style.Add("display", "block");

                            if (!string.IsNullOrEmpty(lblAttachment1.Text) || !string.IsNullOrEmpty(lblAttachment2.Text) || !string.IsNullOrEmpty(lblAttachment3.Text) || !string.IsNullOrEmpty(lblAttachment4.Text))
                            {
                                pdfSource = ConfigurationManager.AppSettings["DRF_DL_REQUEST_ATTACHMENT_URL"] + lblCTRLNo.Text + "/";

                                if (!string.IsNullOrEmpty(lblAttachment1.Text))
                                {
                                    attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment1.Text + "' height='500px' width='980px'></iframe></td></tr></table>";
                                }
                                if (!string.IsNullOrEmpty(lblAttachment2.Text))
                                {
                                    attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment2.Text + "' height='500px' width='980px'></iframe></td></tr></table>";
                                }
                                if (!string.IsNullOrEmpty(lblAttachment3.Text))
                                {
                                    attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment3.Text + "' height='500px' width='980px'></iframe></td></tr></table>";
                                }
                                if (!string.IsNullOrEmpty(lblAttachment4.Text))
                                {
                                    attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment4.Text + "' height='500px' width='980px'></iframe></td></tr></table>";
                                }

                                divAttachment.Style.Add("display", "block");
                                displayAttachment = attachmentLiteralInside;
                            }

                        }
                        else
                        {
                            ibApproved.ImageUrl = "~/images/A1.png";

                            divDetails.Style.Add("display", "none");
                            divAttachment.Style.Add("display", "none");
                            displayAttachment = string.Empty;
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
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessageClose('" + lblCTRLNo.Text.Trim() + "');", true);
                            ibDisapproved.ImageUrl = "~/images/DA2.png";
                            txtRemarks.Enabled = true;

                            divDetails.Style.Add("display", "block");

                            if (!string.IsNullOrEmpty(lblAttachment1.Text) || !string.IsNullOrEmpty(lblAttachment2.Text) || !string.IsNullOrEmpty(lblAttachment3.Text) || !string.IsNullOrEmpty(lblAttachment4.Text))
                            {
                                pdfSource = ConfigurationManager.AppSettings["DRF_DL_REQUEST_ATTACHMENT_URL"] + lblCTRLNo.Text + "/";

                                if (!string.IsNullOrEmpty(lblAttachment1.Text))
                                {
                                    attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment1.Text + "' height='500px' width='980px'></iframe></td></tr></table>";
                                }
                                if (!string.IsNullOrEmpty(lblAttachment2.Text))
                                {
                                    attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment2.Text + "' height='500px' width='980px'></iframe></td></tr></table>";
                                }
                                if (!string.IsNullOrEmpty(lblAttachment3.Text))
                                {
                                    attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment3.Text + "' height='500px' width='980px'></iframe></td></tr></table>";
                                }
                                if (!string.IsNullOrEmpty(lblAttachment4.Text))
                                {
                                    attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + lblAttachment4.Text + "' height='500px' width='980px'></iframe></td></tr></table>";
                                }

                                divAttachment.Style.Add("display", "block");
                                displayAttachment = attachmentLiteralInside;
                            }

                        }
                        else
                        {
                            ibDisapproved.ImageUrl = "~/images/DA1.png";
                            txtRemarks.Text = string.Empty;
                            txtRemarks.Enabled = false;

                            divDetails.Style.Add("display", "none");
                            divAttachment.Style.Add("display", "none");
                            displayAttachment = string.Empty;
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
                    HtmlControl tblDisapprovalCause = (HtmlControl)e.Row.FindControl("tblDisapprovalCause");
                    TextBox txtDisapprovalCause = (TextBox)e.Row.FindControl("txtDisapprovalCause");
                    Label lblStatAll = (Label)e.Row.FindControl("lblStatAll");
                    Label lblStatColor = (Label)e.Row.FindControl("lblStatColor");

                    lblStatAll.Style.Add("background-color", lblStatColor.Text.Trim());

                    if (string.IsNullOrEmpty(txtDisapprovalCause.Text))
                    {
                        tblDisapprovalCause.Style.Add("display", "none");
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

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                string query1 = string.Empty;
                string query2 = string.Empty;
                string query3 = string.Empty;
                string query4 = string.Empty;
                int queryStatusCounter = 0;
                string querySuccess = string.Empty;
                string tempCtrlNo = string.Empty;
                string approvedBy = Session["LcRefId"].ToString();
                int disApprovalCause = 0;

                if (gvData.Rows.Count > 0)
                {
                    for (int i = 0; i < gvData.Rows.Count; i++)
                    {
                        Label lblCTRLNo = (Label)gvData.Rows[i].Cells[0].FindControl("lblCTRLNo");
                        Label lblStatAll = (Label)gvData.Rows[i].Cells[3].FindControl("lblStatAll");
                        ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[4].FindControl("ibApproved");
                        ImageButton ibDisapproved = (ImageButton)gvData.Rows[i].Cells[4].FindControl("ibDisapproved");
                        TextBox txtRemarks = (TextBox)gvData.Rows[i].Cells[6].FindControl("txtRemarks");


                        if (ibApproved.ImageUrl == "~/images/A2.png")
                        {
                            if (lblStatAll.Text.ToUpper() == "FOR PROD. MNGR APPROVAL")
                            {
                                query1 += "UPDATE DRF_TRANSACTION_Status SET Req_Manager = '" + approvedBy + "', DOAReq_Manager = CONVERT(VARCHAR, GETDATE(), 22), STATReq_Manager = '1' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR BUYER APPROVAL" || lblStatAll.Text.ToUpper() == "DISAPPROVED")
                            {
                                query1 += "UPDATE DRF_TRANSACTION_Status SET PurIncharge = '" + approvedBy + "', DOAPur_Incharge = CONVERT(VARCHAR, GETDATE(), 22), STATPur_Incharge = '1', STATPur_Manager = '0', Remarks = '' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR PUR. MNGR APPROVAL")
                            {
                                query1 += "UPDATE DRF_TRANSACTION_Status SET Pur_Manager = '" + approvedBy + "', DOAPur_Manager = CONVERT(VARCHAR, GETDATE(), 22), STATPur_Manager = '1' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }

                            queryStatusCounter++;
                            tempCtrlNo += lblCTRLNo.Text.Trim().ToUpper() + ", ";
                        }

                        if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                        {
                            if (lblStatAll.Text.ToUpper() == "FOR PROD. MNGR APPROVAL")
                            {
                                query1 += "UPDATE DRF_TRANSACTION_Status SET Req_Manager = '" + approvedBy + "', DOAReq_Manager = CONVERT(VARCHAR, GETDATE(), 22), STATReq_Manager = '2', Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR BUYER APPROVAL")
                            {
                                query1 += "UPDATE DRF_TRANSACTION_Status SET PurIncharge = '" + approvedBy + "', DOAPur_Incharge = CONVERT(VARCHAR, GETDATE(), 22), STATPur_Incharge = '2', Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR PUR. MNGR APPROVAL")
                            {
                                query1 += "UPDATE DRF_TRANSACTION_Status SET Pur_Manager = '" + approvedBy + "', DOAPur_Manager = CONVERT(VARCHAR, GETDATE(), 22), STATPur_Manager = '2', STATPur_Incharge = '0', Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }

                            if (string.IsNullOrEmpty(txtRemarks.Text))
                            {
                                disApprovalCause++;
                            }

                            queryStatusCounter++;
                            tempCtrlNo += lblCTRLNo.Text.Trim().ToUpper() + ", ";
                        }

                    }

                    if (disApprovalCause > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please provide a valid disapproval cause.');", true);
                    }
                    else
                    {
                        if (queryStatusCounter > 0)
                        {
                            querySuccess = BLL.DRF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                            if (querySuccess == queryStatusCounter.ToString())
                            {
                                //Session["successMessage"] = "DRF NUMBER(S) : <b>" + tempCtrlNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
                                //Session["successTransactionName"] = "DRF_APPROVALFORM";
                                //Session["successReturnPage"] = "DRF_ApprovalForm.aspx";

                                //Response.Redirect("SuccessPage.aspx");
                                sendToSuppliers();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select atleast 1 item to approved. No selected items for approval.');", true);
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        private void sendToSuppliers()
        {
            try
            {
                string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                string query1 = string.Empty;
                string query2 = string.Empty;
                string query3 = string.Empty;
                string query4 = string.Empty;
                int queryStatusCounter = 0;
                string querySuccess = string.Empty;
                string tempCtrlNo = string.Empty;
                string approvedBy = Session["LcRefId"].ToString();
                int hasForPURManagerApproval = 0;


                if (gvData.Rows.Count > 0)
                {
                    for (int i = 0; i < gvData.Rows.Count; i++)
                    {
                        Label lblCTRLNo = (Label)gvData.Rows[i].Cells[0].FindControl("lblCTRLNo");
                        Label lblStatAll = (Label)gvData.Rows[i].Cells[3].FindControl("lblStatAll");
                        ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[4].FindControl("ibApproved");
                        ImageButton ibClosed = (ImageButton)gvData.Rows[i].Cells[4].FindControl("ibClosed");
                        TextBox txtRemarks = (TextBox)gvData.Rows[i].Cells[6].FindControl("txtRemarks");

                        TextBox txtInvoiceDRNo = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtInvoiceDRNo");
                        TextBox txtPRNo = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtPRNo");
                        TextBox txtPONo = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtPONo");
                        TextBox txtPODate = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtPODate");
                        TextBox txtReceivedDate = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtReceivedDate");
                        TextBox txtCategory = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtCategory");
                        TextBox txtDescription = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtDescription");
                        TextBox txtTypeDrawing = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtTypeDrawing");
                        TextBox txtOrderQuantity = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtOrderQuantity");
                        TextBox txtAbnormalQuantity = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtAbnormalQuantity");
                        TextBox txtTypesOfAbnormality = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtTypesOfAbnormality");
                        TextBox txtDetailedReport = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtDetailedReport");

                        Label lblSupplier = (Label)gvData.Rows[i].Cells[0].FindControl("lblSupplier");
                        Label lblAttention = (Label)gvData.Rows[i].Cells[0].FindControl("lblAttention");
                        Label lblAttachment1 = (Label)gvData.Rows[i].Cells[0].FindControl("lblAttachment1");
                        Label lblAttachment2 = (Label)gvData.Rows[i].Cells[0].FindControl("lblAttachment2");
                        Label lblAttachment3 = (Label)gvData.Rows[i].Cells[0].FindControl("lblAttachment3");
                        Label lblAttachment4 = (Label)gvData.Rows[i].Cells[0].FindControl("lblAttachment4");
                        Label lblSupplierEmail = (Label)gvData.Rows[i].Cells[0].FindControl("lblSupplierEmail");


                        if (ibApproved.ImageUrl == "~/images/A2.png")
                        {
                            if (lblStatAll.Text.ToUpper() == "FOR PUR. MNGR APPROVAL")
                            {
                                hasForPURManagerApproval++;

                                string path = Server.MapPath("~/DRF_Request/" + lblCTRLNo.Text.Trim() + "/" + lblCTRLNo.Text.Trim() + ".html");
                                string htmlTemplate = Server.MapPath("~/UserManual/DRF_Template.txt");
                                string userManualPath = Server.MapPath("~/UserManual/DRF NOTES.docx");
                                string eightDFormat = Server.MapPath("~/UserManual/8D FORMAT.xlsx");
                                string attachmentPath1 = string.Empty;
                                string attachmentPath2 = string.Empty;
                                string attachmentPath3 = string.Empty;
                                string attachmentPath4 = string.Empty;
                                string htmlAttachment = string.Empty;
                                string attachmentPath = string.Empty;
                                string emailService = string.Empty;


                                if (System.IO.File.Exists(htmlTemplate))
                                {
                                    string templateValue = System.IO.File.ReadAllText(htmlTemplate).Replace("filename.csv", lblCTRLNo.Text.Trim() + ".csv")
                                                                                                   .Replace("val_ctrlno", lblCTRLNo.Text.Trim())
                                                                                                   .Replace("val_supplier", lblSupplier.Text)
                                                                                                   .Replace("val_attn", lblAttention.Text)
                                                                                                   .Replace("val_invoicedrno", txtInvoiceDRNo.Text)
                                                                                                   .Replace("val_prpono", txtPRNo.Text)
                                                                                                   .Replace("val_pono", txtPONo.Text)
                                                                                                   .Replace("val_description", txtDescription.Text)
                                                                                                   .Replace("val_typedrawingno", txtTypeDrawing.Text)
                                                                                                   .Replace("val_orderquantity", txtOrderQuantity.Text)
                                                                                                   .Replace("val_abnormalquantity", txtAbnormalQuantity.Text)
                                                                                                   .Replace("val_typesofabnormality", txtTypesOfAbnormality.Text)
                                                                                                   .Replace("val_detailedreport", txtDetailedReport.Text)
                                                                                                   .Replace("val_title", lblCTRLNo.Text.Trim());


                                    if (!System.IO.File.Exists(path))
                                    {
                                        using (StreamWriter writer = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write)))
                                        {
                                            writer.WriteLine(templateValue);
                                        }
                                    }
                                }

                                if (!string.IsNullOrEmpty(lblAttachment1.Text))
                                {
                                    attachmentPath1 = Server.MapPath("~/DRF_Request/" + lblCTRLNo.Text.Trim() + "/" + lblAttachment1.Text);
                                    attachmentPath += attachmentPath1 + ",";
                                }
                                if (!string.IsNullOrEmpty(lblAttachment2.Text))
                                {
                                    attachmentPath2 = Server.MapPath("~/DRF_Request/" + lblCTRLNo.Text.Trim() + "/" + lblAttachment2.Text);
                                    attachmentPath += attachmentPath2 + ",";
                                }
                                if (!string.IsNullOrEmpty(lblAttachment3.Text))
                                {
                                    attachmentPath3 = Server.MapPath("~/DRF_Request/" + lblCTRLNo.Text.Trim() + "/" + lblAttachment3.Text);
                                    attachmentPath += attachmentPath3 + ",";
                                }
                                if (!string.IsNullOrEmpty(lblAttachment4.Text))
                                {
                                    attachmentPath4 = Server.MapPath("~/DRF_Request/" + lblCTRLNo.Text.Trim() + "/" + lblAttachment4.Text);
                                    attachmentPath += attachmentPath4 + ",";
                                }
                                if (System.IO.File.Exists(path))
                                {
                                    htmlAttachment = path;
                                }

                                //-------------------------------------------------------------------------------------------------------------------
                                // SET BUYER INFORMATION

                                List<Entities_RFQ_BuyerInformation> listBuyerInformation = new List<Entities_RFQ_BuyerInformation>();
                                listBuyerInformation = BLL_RFQ.RFQ_MT_BuyerInformation_GetAll();
                                string buyerInformation = string.Empty;

                                if (listBuyerInformation != null)
                                {
                                    if (listBuyerInformation.Count > 0)
                                    {
                                        string tableStart = "<table style='width:100%;'><tr><th align='left'>Purchasing Members</th><th align='left'>Section</th><th align='left'>Personal Email</th><th align='left'>Mobile Number</th></tr>";
                                        string tableEnd = "</table>";
                                        string information = string.Empty;
                                        foreach (Entities_RFQ_BuyerInformation eBI in listBuyerInformation)
                                        {
                                            information += "<tr><td>" + eBI.Member + "</td><td>" + eBI.Section + "</td><td>" + eBI.Email + "</td><td>" + eBI.Mobile + "</td></tr>";
                                        }

                                        buyerInformation = tableStart + information + tableEnd;
                                    }
                                }

                                //-------------------------------------------------------------------------------------------------------------------

                                string fixedBuyerInfo = buyerInformation;


                                if (System.IO.File.Exists(path))
                                {
                                    if (string.IsNullOrEmpty(lblAttachment1.Text) && string.IsNullOrEmpty(lblAttachment2.Text) && string.IsNullOrEmpty(lblAttachment3.Text) && string.IsNullOrEmpty(lblAttachment4.Text))
                                    {
                                        attachmentPath = path + "," + userManualPath + "," + eightDFormat;
                                    }
                                    else
                                    {
                                        attachmentPath = attachmentPath + path + "," + userManualPath + "," + eightDFormat;
                                    }

                                    emailService = COMMON.sendEmailToSuppliersDRF(lblSupplierEmail.Text.Trim(), ConfigurationManager.AppSettings["email-username"], lblCTRLNo.Text.Trim(),
                                                "Hi <b>" + lblSupplier.Text.ToUpper() + "</b> Good Day!" + "<br /><br /> Kindly check the attached html file (" + lblCTRLNo.Text.Trim() + ".html) for our DRF Request" + "<br /><br /><p style='font-size:22px; color:red;'><b>NOTE : </b></p><p style='font-size:18px;'>Please refer to the attached document file (DRF NOTES.docx) on how to update or put your response accordingly.</p>" +
                                                "<br /><br />For this DRF, please contact <b>" + txtCategory.Text.ToUpper() + "</b> Section <br /> <b>PLEASE DO REPLY WITHIN 48 HOURS</b> <br /><br />" +
                                                "<br /><br /><br />Thank You!<br /><br /><br />" +
                                                "*** This is an automatically generated email, Please do reply accordingly *** <br /> <br />" +
                                                "You have received a new DRF Request from ROHM Electronics Philippines Inc." +
                                                "<br /> <br /><br /><br /> For inquries, kindly see below contact details : <br />" + fixedBuyerInfo, attachmentPath, lblSupplier.Text.ToUpper(), lblCTRLNo.Text.Trim());

                                }

                                if (emailService.ToLower().Contains("success"))
                                {
                                    tempCtrlNo += "(" + lblCTRLNo.Text.Trim().ToUpper() + " - SUCCESSFULLY SENT) <br />";

                                    query1 += "UPDATE DRF_TRANSACTION_Status SET BuyerSend ='" + approvedBy + "', DOABuyerSend = CONVERT(VARCHAR, GETDATE(), 22), STATBuyerSend = 1 WHERE CTRLNo = '" + lblCTRLNo.Text.Trim() + "' ";
                                    queryStatusCounter++;
                                    query2 += "INSERT INTO DRF_TRANSACTION_SendReceived (CTRLNo, SendReceivedDate, TransactionType) VALUES ('" + lblCTRLNo.Text.Trim() + "', GETDATE(), 'SEND') ";
                                    queryStatusCounter++;
                                }
                                else
                                {
                                    tempCtrlNo += "(" + lblCTRLNo.Text.Trim().ToUpper() + " - FAILED TO SEND) <br />";
                                }

                            }

                        }

                        //if (ibClosed.ImageUrl == "~/images/Close4.png")
                        //{
                        //    query3 += "UPDATE DRF_TRANSACTION_Status SET PostedBy ='" + approvedBy + "', PostingRemarks = '" + txtRemarks.Text + "', PostedDate = CONVERT(VARCHAR, GETDATE(), 22), Posted = 1 WHERE CTRLNo = '" + lblCTRLNo.Text.Trim() + "' ";
                        //    queryStatusCounter++;
                        //    tempCtrlNo += "(" + lblCTRLNo.Text.Trim().ToUpper() + " - CLOSED <br />";

                        //}


                    } // ending of [for (int i = 0; i < gvData.Rows.Count; i++)]

                }


                if (hasForPURManagerApproval > 0)
                {

                    querySuccess = BLL.DRF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + query2 + query3 + queryEndPart).ToString();

                    if (querySuccess == queryStatusCounter.ToString())
                    {

                        Session["successMessage"] = "DRF NUMBER(S) : <b>" + tempCtrlNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
                        Session["successTransactionName"] = "DRF_APPROVALFORM";
                        Session["successReturnPage"] = "DRF_ApprovalForm.aspx";
                        Response.Redirect("SuccessPage.aspx", false);
                    }
                }
                else
                {
                    Session["successMessage"] = "DRF NUMBER(S) : <b>" + tempCtrlNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
                    Session["successTransactionName"] = "DRF_APPROVALFORM";
                    Session["successReturnPage"] = "DRF_ApprovalForm.aspx";
                    Response.Redirect("SuccessPage.aspx", false);
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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvData.Rows.Count > 0)
            {
                //btnSubmit_Click(sender, e);
                gvExport.Visible = true;
                Response.Clear();
                Response.Buffer = true;

                Response.AddHeader("content-disposition",
                "attachment;filename=DRF_Export.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

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
