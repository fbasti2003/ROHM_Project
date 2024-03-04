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
    public partial class CRF_ApprovalForm : System.Web.UI.Page
    {
        BLL_CRF BLL = new BLL_CRF();
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
                    List<Entities_CRF_RequestEntry> listDropDown = new List<Entities_CRF_RequestEntry>();
                    listDropDown = BLL.CRF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                    if (listDropDown != null)
                    {
                        if (listDropDown.Count > 0)
                        {
                            ddCategory.Items.Clear();
                            ddCategory.Items.Add("");

                            foreach (Entities_CRF_RequestEntry entity in listDropDown)
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
                bool purchasingIncharge = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "15");
                bool purchasingDeptManager = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "16");
                bool purchasingDivManager = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "17");               


                List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();
                Entities_CRF_RequestEntry request = new Entities_CRF_RequestEntry();

                request.CrFrom = txtFrom.Text;
                request.CrTo = txtTo.Text;

                // PRODUCTION MANAGER
                if (productionManager && !purchasingBuyer)
                {
                    if (!string.IsNullOrEmpty(txtCRFNo.Text) || txtCRFNo.Text.Length > 0)
                    {
                        Session["Search_From_CRF_Inquiry"] = txtCRFNo.Text;
                        Response.Redirect("CRF_AllRequest.aspx");
                    }
                    else
                    {
                        if (ddCategory.SelectedItem.Text.ToLower() == "")
                        {
                            if (ddStatus.SelectedValue == "ALL")
                            {
                                list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "0" && itm.LcDepartment == Session["Department"].ToString()).ToList();
                            }
                            else
                            {
                                list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "0" && itm.LcDepartment == Session["Department"].ToString() && itm.StatAll == ddStatus.SelectedValue).ToList();
                            }
                        }
                        else
                        {
                            if (ddStatus.SelectedValue == "ALL")
                            {
                                list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "0" && itm.LcDepartment == Session["Department"].ToString() && itm.CategoryId == ddCategory.SelectedValue.Trim()).ToList();
                            }
                            else
                            {
                                list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "0" && itm.LcDepartment == Session["Department"].ToString() && itm.CategoryId == ddCategory.SelectedValue.Trim() && itm.StatAll == ddStatus.SelectedValue).ToList();
                            }
                        }
                    }
                }

                // PUCHASING BUYER OR INCHARGE
                if (purchasingBuyer && !purchasingIncharge && !purchasingDeptManager && !purchasingDivManager)
                {
                    if (!string.IsNullOrEmpty(txtCRFNo.Text) || txtCRFNo.Text.Length > 0)
                    {
                        Session["Search_From_CRF_Inquiry"] = txtCRFNo.Text;
                        Response.Redirect("CRF_AllRequest.aspx");
                    }
                    else
                    {
                        if (ddCategory.SelectedItem.Text.ToLower() == "")
                        {
                            if (Session["Search_From_CRF_AllRequest"] != null)
                            {
                                if (!string.IsNullOrEmpty(Session["Search_From_CRF_AllRequest"].ToString()))
                                {
                                    list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "0" && (itm.PurManagerStat == "0" || itm.PurManagerStat == "2") && itm.CtrlNo == Session["Search_From_CRF_AllRequest"].ToString().Trim()).ToList();
                                }
                            }
                            else
                            {
                                if (ddStatus.SelectedValue == "ALL")
                                {
                                    list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "0" && (itm.PurManagerStat == "0" || itm.PurManagerStat == "2")).ToList();
                                }
                                else
                                {
                                    list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "0" && (itm.PurManagerStat == "0" || itm.PurManagerStat == "2") && itm.StatAll == ddStatus.SelectedValue).ToList();
                                }
                            }
                        }
                        else
                        {
                            if (Session["Search_From_CRF_AllRequest"] != null)
                            {
                                if (!string.IsNullOrEmpty(Session["Search_From_CRF_AllRequest"].ToString()))
                                {
                                    list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "0" && (itm.PurManagerStat == "0" || itm.PurManagerStat == "2") && itm.CtrlNo == Session["Search_From_CRF_AllRequest"].ToString().Trim() && itm.CategoryId == ddCategory.SelectedValue.Trim()).ToList();
                                }
                            }
                            else
                            {
                                if (ddStatus.SelectedValue == "ALL")
                                {
                                    list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "0" && (itm.PurManagerStat == "0" || itm.PurManagerStat == "2") && itm.CategoryId == ddCategory.SelectedValue.Trim()).ToList();
                                }
                                else
                                {
                                    list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "0" && (itm.PurManagerStat == "0" || itm.PurManagerStat == "2") && itm.CategoryId == ddCategory.SelectedValue.Trim() && itm.StatAll == ddStatus.SelectedValue).ToList();
                                }
                            }
                        }

                        //// FOR BUYER REQUEST THAT NEEDS TO APPROVED BY HIS/HER MANAGER
                        //List<Entities_CRF_RequestEntry> listBuyerExclusive2 = new List<Entities_CRF_RequestEntry>();
                        //listBuyerExclusive2 = BLL.CRF_TRANSACTION_Approval_DateRange().Where(itm => itm.ReqManagerStat == "0" && (itm.PurInchargeStat == "0" || itm.PurManagerStat == "2") && itm.LcDepartment == Session["Department"].ToString()).ToList();

                        //if (listBuyerExclusive2 != null)
                        //{
                        //    if (listBuyerExclusive2.Count > 0)
                        //    {

                        //        foreach (Entities_CRF_RequestEntry eb in listBuyerExclusive2)
                        //        {
                        //            Entities_CRF_RequestEntry eBuyer = new Entities_CRF_RequestEntry();
                        //            eBuyer.Attachment1 = eb.Attachment1;
                        //            eBuyer.Attachment2 = eb.Attachment2;
                        //            eBuyer.Attachment3 = eb.Attachment3;
                        //            eBuyer.Attachment4 = eb.Attachment4;
                        //            eBuyer.Attachment5 = eb.Attachment5;
                        //            eBuyer.Attachment6 = eb.Attachment6;
                        //            eBuyer.Attachment7 = eb.Attachment7;
                        //            eBuyer.Attachment8 = eb.Attachment8;
                        //            eBuyer.Attention = eb.Attention;
                        //            eBuyer.Category = eb.Category;
                        //            eBuyer.CategoryId = eb.CategoryId;
                        //            eBuyer.CssColorCode = eb.CssColorCode;
                        //            eBuyer.CtrlNo = eb.CtrlNo;
                        //            eBuyer.Description = eb.Description;
                        //            eBuyer.CrFrom = eb.CrFrom;
                        //            eBuyer.DropdownName = eb.DropdownName;
                        //            eBuyer.DropdownRefId = eb.DropdownRefId;
                        //            eBuyer.CrTo = eb.CrTo;
                        //            eBuyer.IsDisabled = eb.IsDisabled;
                        //            eBuyer.LcDepartment = eb.LcDepartment;
                        //            eBuyer.OrderQuantity = eb.OrderQuantity;
                        //            eBuyer.PoDate = eb.PoDate;
                        //            eBuyer.PrNO = eb.PrNO;
                        //            eBuyer.PoNO = eb.PoNO;
                        //            eBuyer.PurIncharge = eb.PurIncharge;
                        //            eBuyer.PurInchargeDOA = eb.PurInchargeDOA;
                        //            eBuyer.PurInchargeStat = eb.PurInchargeStat;
                        //            eBuyer.PurManager = eb.PurManager;
                        //            eBuyer.PurManagerDOA = eb.PurManagerDOA;
                        //            eBuyer.PurManagerStat = eb.PurManagerStat;
                        //            eBuyer.RefId = eb.RefId;
                        //            eBuyer.ReqManager = eb.ReqManager;
                        //            eBuyer.ReqManagerDOA = eb.ReqManagerDOA;
                        //            eBuyer.ReqManagerStat = eb.ReqManagerStat;
                        //            eBuyer.Requester = eb.Requester;
                        //            eBuyer.RequesterEmail = eb.RequesterEmail;
                        //            eBuyer.RequesterLocalNumber = eb.RequesterLocalNumber;
                        //            eBuyer.RequesterS = eb.RequesterS;
                        //            eBuyer.RequesterSDOA = eb.RequesterSDOA;
                        //            eBuyer.RequesterSStat = eb.RequesterSStat;
                        //            eBuyer.StatAll = eb.StatAll;
                        //            eBuyer.StatRemarks = eb.StatRemarks;
                        //            eBuyer.Supplier = eb.Supplier;
                        //            eBuyer.SupplierResponded = eb.SupplierResponded;
                        //            eBuyer.TableName = eb.TableName;
                        //            eBuyer.TransactionDate = eb.TransactionDate;
                        //            eBuyer.TypeDrawingNo = eb.TypeDrawingNo;
                        //            eBuyer.UpdatedBy = eb.UpdatedBy;
                        //            eBuyer.UpdatedDate = eb.UpdatedDate;
                        //            eBuyer.Reason = eb.Reason;
                        //            eBuyer.ReasonName = eb.ReasonName;

                        //            list.Add(eBuyer);
                        //        }
                        //    }
                        //}


                    }
                }

                // PURCHASING MANAGER
                if ((purchasingIncharge || purchasingDeptManager || purchasingDivManager) && (list == null || list.Count <= 0))
                {
                    if (!string.IsNullOrEmpty(txtCRFNo.Text) || txtCRFNo.Text.Length > 0)
                    {
                        Session["Search_From_CRF_Inquiry"] = txtCRFNo.Text;
                        Response.Redirect("CRF_AllRequest.aspx");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(ddCategory.SelectedItem.Text))
                        {
                            if (Session["Search_From_CRF_AllRequest"] != null)
                            {
                                if (!string.IsNullOrEmpty(Session["Search_From_CRF_AllRequest"].ToString()))
                                {
                                    list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "1" && itm.PurManagerStat == "0" && itm.CtrlNo == Session["Search_From_CRF_AllRequest"].ToString().Trim()).ToList();
                                }
                            }
                            else
                            {
                                if (ddStatus.SelectedValue == "ALL")
                                {
                                    list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "1" && itm.PurManagerStat == "0").ToList();
                                }
                                else
                                {
                                    list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "1" && itm.PurManagerStat == "0" && itm.StatAll == ddStatus.SelectedValue).ToList();
                                }

                                // This is special for Sir Rudy and Mam Sylvia because he is using 1 account for buyer and manager
                                if (Session["Username"].ToString() == "6985" || Session["Username"].ToString() == "0286")
                                {
                                    if (ddStatus.SelectedValue == "ALL")
                                    {
                                        list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "1" && (itm.PurInchargeStat == "1" || itm.PurInchargeStat == "0") && itm.PurManagerStat == "0").ToList();
                                    }
                                    else
                                    {
                                        list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "1" && (itm.PurInchargeStat == "1" || itm.PurInchargeStat == "0") && itm.PurManagerStat == "0" && itm.StatAll == ddStatus.SelectedValue).ToList();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Session["Search_From_CRF_AllRequest"] != null)
                            {
                                if (!string.IsNullOrEmpty(Session["Search_From_CRF_AllRequest"].ToString()))
                                {
                                    list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "1" && itm.PurManagerStat == "0" && itm.CtrlNo == Session["Search_From_CRF_AllRequest"].ToString().Trim() && itm.CategoryId == ddCategory.SelectedValue.Trim()).ToList();
                                }
                            }
                            else
                            {
                                if (ddStatus.SelectedValue == "ALL")
                                {
                                    list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "1" && itm.PurManagerStat == "0" && itm.CategoryId == ddCategory.SelectedValue.Trim()).ToList();
                                }
                                else
                                {
                                    list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "1" && itm.PurInchargeStat == "1" && itm.PurManagerStat == "0" && itm.CategoryId == ddCategory.SelectedValue.Trim() && itm.StatAll == ddStatus.SelectedValue).ToList();
                                }

                                // This is special for Sir Rudy and Mam Sylvia because he is using 1 account for buyer and manager
                                if (Session["Username"].ToString() == "6985" || Session["Username"].ToString() == "0286")
                                {
                                    if (ddStatus.SelectedValue == "ALL")
                                    {
                                        list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "1" && (itm.PurInchargeStat == "1" || itm.PurInchargeStat == "0") && itm.PurManagerStat == "0" && itm.CategoryId == ddCategory.SelectedValue.Trim()).ToList();
                                    }
                                    else
                                    {
                                        list = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.ReqManagerStat == "1" && (itm.PurInchargeStat == "1" || itm.PurInchargeStat == "0") && itm.PurManagerStat == "0" && itm.CategoryId == ddCategory.SelectedValue.Trim() && itm.StatAll == ddStatus.SelectedValue).ToList();
                                    }
                                }
                            }
                        }

                        // FOR BUYER REQUEST THAT NEEDS TO APPROVED BY HIS/HER MANAGER
                        List<Entities_CRF_RequestEntry> listBuyerExclusive = new List<Entities_CRF_RequestEntry>();

                        if (ddStatus.SelectedValue == "ALL")
                        {
                            listBuyerExclusive = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.PurManagerStat == "0" && itm.LcDivision == Session["Division"].ToString()).ToList();
                        }
                        else
                        {
                            listBuyerExclusive = BLL.CRF_TRANSACTION_Approval_DateRange_New(request).Where(itm => itm.PurManagerStat == "0" && itm.LcDivision == Session["Division"].ToString() && itm.StatAll == ddStatus.SelectedValue).ToList();
                        }

                        if (listBuyerExclusive != null)
                        {
                            if (listBuyerExclusive.Count > 0)
                            {

                                foreach (Entities_CRF_RequestEntry eb in listBuyerExclusive)
                                {
                                    Entities_CRF_RequestEntry eBuyer = new Entities_CRF_RequestEntry();
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
                                    eBuyer.CrFrom = eb.CrFrom;
                                    eBuyer.DropdownName = eb.DropdownName;
                                    eBuyer.DropdownRefId = eb.DropdownRefId;
                                    eBuyer.CrTo = eb.CrTo;
                                    eBuyer.IsDisabled = eb.IsDisabled;
                                    eBuyer.LcDepartment = eb.LcDepartment;
                                    eBuyer.LcDivision = eb.LcDivision;
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
                                    eBuyer.UpdatedBy = eb.UpdatedBy;
                                    eBuyer.UpdatedDate = eb.UpdatedDate;
                                    eBuyer.Reason = eb.Reason;
                                    eBuyer.ReasonName = eb.ReasonName;

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
                        List<Entities_CRF_RequestEntry> finalListExport = new List<Entities_CRF_RequestEntry>();

                        foreach (Entities_CRF_RequestEntry entity in list)
                        {
                            List<Entities_CRF_RequestEntry> listExport = new List<Entities_CRF_RequestEntry>();
                            listExport = BLL.CRF_TRANSACTION_AllRequest_ByCTRLNo2(entity);

                            if (listExport != null)
                            {
                                if (listExport.Count > 0)
                                {
                                    foreach (Entities_CRF_RequestEntry le in listExport)
                                    {
                                        Entities_CRF_RequestEntry final = new Entities_CRF_RequestEntry();
                                        final.RdCtrlNo = le.RdCtrlNo;
                                        final.Attention = le.Attention;
                                        final.Supplier = le.Supplier;
                                        final.Requester = le.Requester;

                                        final.RdPRNO = le.RdPRNO;
                                        final.RdPONO = le.RdPONO;
                                        final.RdPODate = le.RdPODate;
                                        final.Category = le.Category;
                                        final.RdItemName = le.RdItemName;
                                        final.RdSpecs = le.RdSpecs;
                                        final.RdQuantity = le.RdQuantity;
                                        final.DateInformedSupplier = le.DateInformedSupplier;
                                        final.DateInformedRequestor = le.DateInformedRequestor;
                                        final.ResponseConfirmedBy = le.ResponseConfirmedBy;
                                        final.ResponseDateConfirmed = le.ResponseDateConfirmed;
                                        final.ResponseNotes = le.ResponseNotes;
                                        final.DateInformedSupplier = le.DateInformedSupplier;
                                        final.DateInformedRequestor = le.DateInformedRequestor;

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
                //divLoader.Style.Add("display", "none");

                Session["Search_From_CRF_AllRequest"] = null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
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
                "attachment;filename=CRF_Export.xls");
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

        protected void gvDataStatus_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblProdManagerStat = (Label)e.Row.FindControl("lblProdManagerStat");
                    Label lblProdManagerStatColor = (Label)e.Row.FindControl("lblProdManagerStatColor");

                    Label lblBuyerStat = (Label)e.Row.FindControl("lblBuyerStat");
                    Label lblBuyerStatColor = (Label)e.Row.FindControl("lblBuyerStatColor");

                    Label lblSCDManagerStat = (Label)e.Row.FindControl("lblSCDManagerStat");
                    Label lblSCDManagerStatColor = (Label)e.Row.FindControl("lblSCDManagerStatColor");



                    lblProdManagerStat.Style.Add("background-color", lblProdManagerStatColor.Text.Trim());
                    lblBuyerStat.Style.Add("background-color", lblBuyerStatColor.Text.Trim());
                    lblSCDManagerStat.Style.Add("background-color", lblSCDManagerStatColor.Text.Trim());


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

        protected void gvDataDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                LinkButton lbSupplierAttachment = row.FindControl("lbSupplierAttachment") as LinkButton;
                LinkButton lbRequesterAttachment = row.FindControl("lbRequesterAttachment") as LinkButton;
                Label lblCTRLNoLabel = row.FindControl("lblCTRLNoLabel") as Label;

                if (e.CommandName == "lbSupplierAttachment_Command")
                {
                    string URL = "~/CRF_Received/" + lblCTRLNoLabel.Text.Trim() + "/" + lbSupplierAttachment.Text.Replace("%20", " ");

                    URL = Page.ResolveClientUrl(URL);
                    lbSupplierAttachment.OnClientClick = "window.open('" + URL + "'); return false;";
                }

                if (e.CommandName == "lbRequesterAttachment_Command")
                {
                    string URL = "~/CRF_Request/" + lblCTRLNoLabel.Text.Trim() + "/" + lbRequesterAttachment.Text.Replace("%20", " ");

                    URL = Page.ResolveClientUrl(URL);
                    lbRequesterAttachment.OnClientClick = "window.open('" + URL + "'); return false;";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvDataDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblDisapprovalCause2 = (Label)e.Row.FindControl("lblDisapprovalCause2");
                    HtmlControl trDisapprovalCause = (HtmlControl)e.Row.FindControl("trDisapprovalCause");

                    if (!string.IsNullOrEmpty(lblDisapprovalCause2.Text))
                    {
                        trDisapprovalCause.Style.Add("display", "block");
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

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                HtmlControl divRequesterAttachment = row.FindControl("divRequesterAttachment") as HtmlControl;
                GridView gvDataStatus = row.FindControl("gvDataStatus") as GridView;
                GridView gvDataHead = row.FindControl("gvDataHead") as GridView;
                GridView gvDataDetails = row.FindControl("gvDataDetails") as GridView;
                LinkButton lbCTRLNo = row.FindControl("lbCTRLNo") as LinkButton;
                LinkButton lblView = row.FindControl("lblView") as LinkButton;

                ImageButton ibApproved = row.FindControl("ibApproved") as ImageButton;
                ImageButton ibDisapproved = row.FindControl("ibDisapproved") as ImageButton;
                ImageButton ibPreview = row.FindControl("ibPreview") as ImageButton;
                TextBox txtRemarks = row.FindControl("txtRemarks") as TextBox;

                Label lblSupplierName = row.FindControl("lblSupplierName") as Label;
                Label lblAttentionName = row.FindControl("lblAttentionName") as Label;
                Label lblRequester2 = row.FindControl("lblRequester2") as Label;

                if (e.CommandName == "ibManualResponse_Command")
                {
                    bool purchasingBuyer = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim());

                    if (purchasingBuyer)
                    {
                        string URL = "~/CRF_ManualResponse.aspx?CRFNo_From_ManualResponse=" + CryptorEngine.Encrypt(lbCTRLNo.Text.Trim(), true);
                        Response.Redirect(URL, false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('ACCESS DENIED!');", true);
                    }
                }

                if (e.CommandName == "lbCTRLNo_Command")
                {
                    string URL = "~/CRF_RequestEntry.aspx?CRFNo_From_Inquiry=" + CryptorEngine.Encrypt(lbCTRLNo.Text.Trim(), true);

                    URL = Page.ResolveClientUrl(URL);
                    lbCTRLNo.OnClientClick = "window.open('" + URL + "'); return false;";
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

                            //HEAD
                            List<Entities_CRF_RequestEntry> listRequest = new List<Entities_CRF_RequestEntry>();
                            Entities_CRF_RequestEntry entityRequest = new Entities_CRF_RequestEntry();
                            entityRequest.CtrlNo = lbCTRLNo.Text.Trim();

                            listRequest = BLL.CRF_TRANSACTION_GetRequestByCTRLNo(entityRequest);

                            if (listRequest != null)
                            {
                                if (listRequest.Count > 0)
                                {
                                    gvDataHead.Visible = true;
                                    gvDataHead.DataSource = listRequest;
                                    gvDataHead.DataBind();

                                    gvDataStatus.Visible = true;
                                    gvDataStatus.DataSource = listRequest;
                                    gvDataStatus.DataBind();

                                }
                            }

                            //DETAILS
                            List<Entities_CRF_RequestEntry> listRequestDetails = new List<Entities_CRF_RequestEntry>();
                            Entities_CRF_RequestEntry entityRequestDetails = new Entities_CRF_RequestEntry();
                            entityRequestDetails.RdCtrlNo = lbCTRLNo.Text.Trim();

                            listRequestDetails = BLL.CRF_TRANSACTION_GetRequestDetailsByCTRLNo(entityRequestDetails);

                            if (listRequestDetails != null)
                            {
                                if (listRequestDetails.Count > 0)
                                {
                                    gvDataDetails.Visible = true;
                                    gvDataDetails.DataSource = listRequestDetails;
                                    gvDataDetails.DataBind();

                                }
                            }

                        }
                        else
                        {
                            gvDataStatus.Visible = false;
                            gvDataDetails.Visible = false;
                            gvDataHead.Visible = false;

                            ibApproved.ImageUrl = "~/images/A1.png";
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

                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessageClose('" + lbCTRLNo.Text.Trim() + "');", true);

                            ibDisapproved.ImageUrl = "~/images/DA2.png";
                            txtRemarks.Enabled = true;

                            //HEAD
                            List<Entities_CRF_RequestEntry> listRequest = new List<Entities_CRF_RequestEntry>();
                            Entities_CRF_RequestEntry entityRequest = new Entities_CRF_RequestEntry();
                            entityRequest.CtrlNo = lbCTRLNo.Text.Trim();

                            listRequest = BLL.CRF_TRANSACTION_GetRequestByCTRLNo(entityRequest);

                            if (listRequest != null)
                            {
                                if (listRequest.Count > 0)
                                {
                                    gvDataHead.Visible = true;
                                    gvDataHead.DataSource = listRequest;
                                    gvDataHead.DataBind();

                                    gvDataStatus.Visible = true;
                                    gvDataStatus.DataSource = listRequest;
                                    gvDataStatus.DataBind();

                                }
                            }

                            //DETAILS
                            List<Entities_CRF_RequestEntry> listRequestDetails = new List<Entities_CRF_RequestEntry>();
                            Entities_CRF_RequestEntry entityRequestDetails = new Entities_CRF_RequestEntry();
                            entityRequestDetails.RdCtrlNo = lbCTRLNo.Text.Trim();

                            listRequestDetails = BLL.CRF_TRANSACTION_GetRequestDetailsByCTRLNo(entityRequestDetails);

                            if (listRequestDetails != null)
                            {
                                if (listRequestDetails.Count > 0)
                                {
                                    gvDataDetails.Visible = true;
                                    gvDataDetails.DataSource = listRequestDetails;
                                    gvDataDetails.DataBind();

                                }
                            }

                        }
                        else
                        {
                            ibDisapproved.ImageUrl = "~/images/DA1.png";
                            txtRemarks.Text = string.Empty;
                            txtRemarks.Enabled = false;

                            gvDataStatus.Visible = false;
                            gvDataDetails.Visible = false;
                            gvDataHead.Visible = false;
                        }
                    }
                }

                if (e.CommandName == "ibPreview_Command")
                {

                    string pathLocation = Server.MapPath("~/CRF_Request/" + lbCTRLNo.Text.Trim() + "/REPORT_" + lbCTRLNo.Text.Trim() + ".html");
                    string htmlTemplate = Server.MapPath("~/HTML_Report/CRF/CRF.txt");
                    string htmlTemplate_Multiple = Server.MapPath("~/HTML_Report/CRF/CRF_Multiple.txt");
                    string templateValue = string.Empty;

                    if (System.IO.File.Exists(htmlTemplate))
                    {

                        //DETAILS
                        List<Entities_CRF_RequestEntry> listRequestDetails = new List<Entities_CRF_RequestEntry>();
                        Entities_CRF_RequestEntry entityRequestDetails = new Entities_CRF_RequestEntry();
                        entityRequestDetails.RdCtrlNo = lbCTRLNo.Text.Trim();

                        listRequestDetails = BLL.CRF_TRANSACTION_GetRequestDetailsByCTRLNo(entityRequestDetails);

                        if (listRequestDetails != null)
                        {
                            if (listRequestDetails.Count > 0)
                            {
                                if (listRequestDetails.Count.ToString() == "1")
                                {
                                    // SINGLE RECORD
                                    foreach (Entities_CRF_RequestEntry eRequest in listRequestDetails)
                                    {
                                        templateValue = System.IO.File.ReadAllText(htmlTemplate)
                                                           .Replace("val_ctrlno", eRequest.RdCtrlNo)
                                                           .Replace("val_attention", lblAttentionName.Text.ToUpper())
                                                           .Replace("val_division", eRequest.DivisionName)
                                                           .Replace("val_department", eRequest.DepartmentName)
                                                           .Replace("val_prno", eRequest.RdPRNO)
                                                           .Replace("val_pono", eRequest.RdPONO)
                                                           .Replace("val_description", eRequest.RdItemName)
                                                           .Replace("val_type", eRequest.RdSpecs)
                                                           .Replace("val_quantity", eRequest.RdQuantity)
                                                           .Replace("val_reason", eRequest.RdReasonName)
                                                           .Replace("val_preparedby", eRequest.RequesterS)
                                                           .Replace("val_notedby", eRequest.ReqManager)
                                                           .Replace("val_incharge", eRequest.PurIncharge)
                                                           .Replace("val_manager", eRequest.PurManager)
                                                           .Replace("val_doa_preparedby", eRequest.RequesterSDOA)
                                                           .Replace("val_doa_notedby", eRequest.ReqManagerDOA)
                                                           .Replace("val_doa_incharge", eRequest.PurInchargeDOA)
                                                           .Replace("val_doa_manager", eRequest.PurManagerDOA)
                                                           .Replace("val_confirmedby", eRequest.ResponseConfirmedBy)
                                                           .Replace("val_dateconfirmed", eRequest.ResponseDateConfirmed)
                                                           .Replace("val_notes", eRequest.ResponseNotes)
                                                           .Replace("bg_v_nb", "background-color:" + eRequest.ReqManagerStatColor + ";")
                                                           .Replace("bg_v_i", "background-color:" + eRequest.PurInchargeStatColor + ";")
                                                           .Replace("bg_v_m", "background-color:" + eRequest.PurManagerStatColor + ";")
                                                           .Replace("valreason_supplierchange", setReason(eRequest.RdReasonName, "valreason_supplierchange"))
                                                           .Replace("valreason_changepacking", setReason(eRequest.RdReasonName, "valreason_changepacking"))
                                                           .Replace("valreason_changespecification", setReason(eRequest.RdReasonName, "valreason_changespecification"))
                                                           .Replace("valreason_pricechange", setReason(eRequest.RdReasonName, "valreason_pricechange"))
                                                           .Replace("valreason_changemaker", setReason(eRequest.RdReasonName, "valreason_changemaker"))
                                                           .Replace("valreason_stopproduction", setReason(eRequest.RdReasonName, "valreason_stopproduction"))
                                                           .Replace("valreason_quantitychange", setReason(eRequest.RdReasonName, "valreason_quantitychange"))
                                                           .Replace("valreason_outofstock", setReason(eRequest.RdReasonName, "valreason_outofstock"))
                                                           .Replace("valreason_excessorder", setReason(eRequest.RdReasonName, "valreason_excessorder"))
                                                           .Replace("val_supplier", eRequest.SupplierName);

                                    }
                                }
                                else
                                {
                                    //MULTIPLE RECORDS
                                    string tableHeader = string.Empty;
                                    string tableDetails = string.Empty;
                                    string table = string.Empty;
                                    int counter = 0;

                                    tableHeader = "<tr><th>PONO</th><th>PRNO</th><th>DESCRIPTION</th><th>TYPE/DRAWING</th><th>QUANTITY</th><th>UNIT OF MEASURE</th><th>REASON</th><th>CONFIRMED BY</th><th>DATE CONFIRMED</th><th>NOTES</th></tr>";
                                    //LOOP & GET THE SINGLE RECORD
                                    foreach (Entities_CRF_RequestEntry entity in listRequestDetails)
                                    {
                                        // TABLE CREATION
                                        tableDetails += "<tr><td>" + entity.RdPONO + "</td><td>" + entity.RdPRNO + "</td><td>" + entity.RdItemName + "</td><td>" + entity.RdSpecs + "</td><td>" + entity.RdQuantity + "</td><td>" + entity.RdUOMDesc + "</td><td>" + entity.RdReasonName + "</td><td>" + entity.ResponseConfirmedBy + "</td><td>" + entity.ResponseDateConfirmed + "</td><td>" + entity.ResponseNotes + "</td></tr>";
                                    }

                                    table = "<table border='1' style='width: 100%; border-collapse: collapse; margin-left: auto; margin-right: auto; height: 94px; font-size:10px;'><tbody>" + tableHeader + tableDetails + "</tbody></table>";

                                    foreach (Entities_CRF_RequestEntry eRequest in listRequestDetails)
                                    {
                                        if (counter <= 0)
                                        {
                                            templateValue = System.IO.File.ReadAllText(htmlTemplate_Multiple)
                                                               .Replace("val_ctrlno", eRequest.RdCtrlNo)
                                                               .Replace("val_attention", lblAttentionName.Text.ToUpper())
                                                               .Replace("val_division", eRequest.DivisionName)
                                                               .Replace("val_department", eRequest.DepartmentName)
                                                               .Replace("val_prno", "PLS SEE ATTACHED")
                                                               .Replace("val_pono", "PLS SEE ATTACHED")
                                                               .Replace("val_description", "PLS SEE ATTACHED")
                                                               .Replace("val_type", "PLS SEE ATTACHED")
                                                               .Replace("val_quantity", "PLS SEE ATTACHED")
                                                               .Replace("val_reason", "PLS SEE ATTACHED")
                                                               .Replace("val_preparedby", eRequest.RequesterS)
                                                               .Replace("val_notedby", eRequest.ReqManager)
                                                               .Replace("val_incharge", eRequest.PurIncharge)
                                                               .Replace("val_manager", eRequest.PurManager)
                                                               .Replace("val_doa_preparedby", eRequest.RequesterSDOA)
                                                               .Replace("val_doa_notedby", eRequest.ReqManagerDOA)
                                                               .Replace("val_doa_incharge", eRequest.PurInchargeDOA)
                                                               .Replace("val_doa_manager", eRequest.PurManagerDOA)
                                                               .Replace("val_confirmedby", eRequest.ResponseConfirmedBy)
                                                               .Replace("val_dateconfirmed", eRequest.ResponseDateConfirmed)
                                                               .Replace("val_notes", eRequest.ResponseNotes)
                                                               .Replace("bg_v_nb", "background-color:" + eRequest.ReqManagerStatColor + ";")
                                                               .Replace("bg_v_i", "background-color:" + eRequest.PurInchargeStatColor + ";")
                                                               .Replace("bg_v_m", "background-color:" + eRequest.PurManagerStatColor + ";")
                                                               .Replace("valreason_supplierchange", setReason(eRequest.RdReason, "valreason_supplierchange"))
                                                               .Replace("valreason_changepacking", setReason(eRequest.RdReason, "valreason_changepacking"))
                                                               .Replace("valreason_changespecification", setReason(eRequest.RdReason, "valreason_changespecification"))
                                                               .Replace("valreason_pricechange", setReason(eRequest.RdReason, "valreason_pricechange"))
                                                               .Replace("valreason_changemaker", setReason(eRequest.RdReason, "valreason_changemaker"))
                                                               .Replace("valreason_stopproduction", setReason(eRequest.RdReason, "valreason_stopproduction"))
                                                               .Replace("valreason_quantitychange", setReason(eRequest.RdReason, "valreason_quantitychange"))
                                                               .Replace("valreason_outofstock", setReason(eRequest.RdReason, "valreason_outofstock"))
                                                               .Replace("valreason_excessorder", setReason(eRequest.RdReason, "valreason_excessorder"))
                                                               .Replace("val_details", table)
                                                               .Replace("val_supplier", eRequest.SupplierName);
                                            counter++;
                                        }

                                    }
                                }
                            }
                        }


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

                    string URL = "CRF_Request/" + lbCTRLNo.Text.Trim() + "/REPORT_" + lbCTRLNo.Text.Trim() + ".html";

                    URL = Page.ResolveClientUrl(URL);
                    ibPreview.OnClientClick = "window.open('" + URL + "'); return false;";

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
                    DropDownList ddForSending = (DropDownList)e.Row.FindControl("ddForSending");
                    Label lblForSending = (Label)e.Row.FindControl("lblForSending");

                    lblStatAll.Style.Add("background-color", lblStatColor.Text.Trim());

                    if (lblStatAll.Text.ToUpper() == "FOR SUPPLY CHAIN BUYER APPROVAL" || lblStatAll.Text.ToUpper() == "FOR SUPPLY CHAIN INCHARGE APPROVAL")
                    {
                        ddForSending.Enabled = true;

                        if (lblForSending.Text == "1")
                        {
                            ddForSending.Items.FindByText("YES").Selected = true;
                        }
                        if (lblForSending.Text == "2")
                        {
                            ddForSending.Items.FindByText("NO").Selected = true;
                        }
                    }
                    else
                    {
                        ddForSending.Enabled = false;
                    }

                    //if (lblStatAll.Text.ToUpper() == "FOR SUPPLY CHAIN INCHARGE APPROVAL")
                    //{
                    //    ddForSending.Enabled = true;

                    //    if (lblForSending.Text == "1")
                    //    {
                    //        ddForSending.Items.FindByText("YES").Selected = true;
                    //    }
                    //    if (lblForSending.Text == "2")
                    //    {
                    //        ddForSending.Items.FindByText("NO").Selected = true;
                    //    }

                    //    ddForSending.Enabled = false;
                    //}

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
                string emptyDI = string.Empty;
                string emptyForSending = string.Empty;

                if (gvData.Rows.Count > 0)
                {
                    for (int i = 0; i < gvData.Rows.Count; i++)
                    {
                        Label lblCTRLNo = (Label)gvData.Rows[i].Cells[0].FindControl("lblCTRLNo");
                        Label lblStatAll = (Label)gvData.Rows[i].Cells[5].FindControl("lblStatAll");
                        ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[7].FindControl("ibApproved");
                        ImageButton ibDisapproved = (ImageButton)gvData.Rows[i].Cells[7].FindControl("ibDisapproved");
                        TextBox txtRemarks = (TextBox)gvData.Rows[i].Cells[8].FindControl("txtRemarks");
                        DropDownList ddForSending = (DropDownList)gvData.Rows[i].Cells[6].FindControl("ddForSending");


                        if (ibApproved.ImageUrl == "~/images/A2.png")
                        {
                            if (lblStatAll.Text.ToUpper() == "FOR PROD. MNGR APPROVAL")
                            {
                                query1 += "UPDATE CRF_TRANSACTION_Status SET Req_Manager = '" + approvedBy + "', DOAReq_Manager = CONVERT(VARCHAR, GETDATE(), 22), STATReq_Manager = '1' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR SUPPLY CHAIN BUYER APPROVAL" || lblStatAll.Text.ToUpper() == "DISAPPROVED")
                            {
                                query1 += "UPDATE CRF_TRANSACTION_Status SET PurIncharge = '" + approvedBy + "', DOAPur_Incharge = CONVERT(VARCHAR, GETDATE(), 22), STATPur_Incharge = '1', STATPur_Manager = '0', Remarks = '' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR SUPPLY CHAIN INCHARGE APPROVAL")
                            {
                                query1 += "UPDATE CRF_TRANSACTION_Status SET Pur_Manager = '" + approvedBy + "', DOAPur_Manager = CONVERT(VARCHAR, GETDATE(), 22), STATPur_Manager = '1' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }

                            queryStatusCounter++;
                            tempCtrlNo += lblCTRLNo.Text.Trim().ToUpper() + ", ";

                            if (lblStatAll.Text.ToUpper() == "FOR SUPPLY CHAIN BUYER APPROVAL")
                            {
                                //VALIDATE DETAILS
                                List<Entities_CRF_RequestEntry> listRequestDetails = new List<Entities_CRF_RequestEntry>();
                                Entities_CRF_RequestEntry entityRequestDetails = new Entities_CRF_RequestEntry();
                                entityRequestDetails.RdCtrlNo = lblCTRLNo.Text;

                                listRequestDetails = BLL.CRF_TRANSACTION_GetRequestDetailsByCTRLNo(entityRequestDetails);

                                if (listRequestDetails != null)
                                {
                                    if (listRequestDetails.Count > 0)
                                    {
                                        foreach (Entities_CRF_RequestEntry entity in listRequestDetails)
                                        {
                                            if (string.IsNullOrEmpty(entity.RdDateInformedRequester) || string.IsNullOrEmpty(entity.RdDateInformedSupplier))
                                            {
                                                emptyDI += lblCTRLNo.Text + ", ";
                                            }
                                        }

                                    }
                                }

                                //VALIDATE FOR SENDING
                                if (string.IsNullOrEmpty(ddForSending.SelectedItem.Text))
                                {
                                    emptyForSending += lblCTRLNo.Text + ", ";
                                }
                            }

                            // FOR HISTORICAL REQUEST
                            if (lblStatAll.Text.ToUpper() == "FOR SUPPLY CHAIN INCHARGE APPROVAL")
                            {
                                //VALIDATE FOR SENDING
                                if (string.IsNullOrEmpty(ddForSending.SelectedItem.Text))
                                {
                                    emptyForSending += lblCTRLNo.Text + ", ";
                                }
                            }

                        }

                        if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                        {
                            if (lblStatAll.Text.ToUpper() == "FOR PROD. MNGR APPROVAL")
                            {
                                query1 += "UPDATE CRF_TRANSACTION_Status SET Req_Manager = '" + approvedBy + "', DOAReq_Manager = CONVERT(VARCHAR, GETDATE(), 22), STATReq_Manager = '2', Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR SUPPLY CHAIN BUYER APPROVAL" || lblStatAll.Text.ToUpper() == "DISAPPROVED")
                            {
                                query1 += "UPDATE CRF_TRANSACTION_Status SET PurIncharge = '" + approvedBy + "', DOAPur_Incharge = CONVERT(VARCHAR, GETDATE(), 22), STATPur_Incharge = '2', Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR SUPPLY CHAIN INCHARGE APPROVAL")
                            {
                                query1 += "UPDATE CRF_TRANSACTION_Status SET Pur_Manager = '" + approvedBy + "', DOAPur_Manager = CONVERT(VARCHAR, GETDATE(), 22), STATPur_Manager = '2', STATPur_Incharge = '0', Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
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
                    else if (!string.IsNullOrEmpty(emptyDI))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + emptyDI + " - DATE INFORMED REQUESTER AND DATE INFORMED SUPPLIER MUST NOT BE BLANK. CLICK CTRLNO AND UPDATE ACCORDINGLY.');", true);
                    }
                    else if (!string.IsNullOrEmpty(emptyForSending))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select a valid FOR SENDING field for CTRLNO " + emptyForSending + "');", true);
                    }
                    else
                    {

                        if (queryStatusCounter > 0)
                        {
                            querySuccess = BLL.CRF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                            if (querySuccess == queryStatusCounter.ToString())
                            {
                                //sendToSuppliers_New(lblCTRLNo.Text.Trim().ToUpper(), lblStatAll.Text.ToUpper(), ibApproved.ImageUrl);
                                sendToSuppliers();
                                //-------------------------------------------------------------------------------------------------------------
                                //Session["successMessage"] = "CRF NUMBER(S) : <b>" + tempCtrlNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
                                //Session["successTransactionName"] = "CRF_APPROVALFORM";
                                //Session["successReturnPage"] = "CRF_ApprovalForm.aspx";

                                //Response.Redirect("SuccessPage.aspx");
                                //-------------------------------------------------------------------------------------------------------------                                

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

        private void sendToSuppliers_New(string ctrlno, string transType, string imageUrl)
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
                

                if (imageUrl == "~/images/A2.png")
                {
                    if (transType == "FOR PUR. MNGR APPROVAL")
                    {
                        hasForPURManagerApproval++;
                        string path = Server.MapPath("~/CRF_Request/" + ctrlno + "/" + ctrlno + ".html");
                        string htmlTemplate = Server.MapPath("~/UserManual/CRF_Template.txt");
                        string userManualPath = Server.MapPath("~/UserManual/CRF NOTES.docx");

                        //-----------------------------------------------------------------------
                        //DETAILS
                        List<Entities_CRF_RequestEntry> listRequestDetails = new List<Entities_CRF_RequestEntry>();
                        Entities_CRF_RequestEntry entityRequestDetails = new Entities_CRF_RequestEntry();
                        entityRequestDetails.RdCtrlNo = ctrlno;

                        listRequestDetails = BLL.CRF_TRANSACTION_GetRequestDetailsByCTRLNo(entityRequestDetails);

                        if (listRequestDetails != null)
                        {
                            if (listRequestDetails.Count > 0)
                            {
                                foreach (Entities_CRF_RequestEntry entity in listRequestDetails)
                                {

                                }

                            }
                        }
                        //-----------------------------------------------------------------------
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


                if (gvData.Rows.Count > 0)
                {
                    for (int i = 0; i < gvData.Rows.Count; i++)
                    {
                        Label lblCTRLNo = (Label)gvData.Rows[i].Cells[1].FindControl("lblCTRLNo");
                        Label lblStatAll = (Label)gvData.Rows[i].Cells[5].FindControl("lblStatAll");
                        ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[7].FindControl("ibApproved");
                        ImageButton ibClosed = (ImageButton)gvData.Rows[i].Cells[7].FindControl("ibClosed");
                        TextBox txtRemarks = (TextBox)gvData.Rows[i].Cells[8].FindControl("txtRemarks");

                        Label lblSupplierName = (Label)gvData.Rows[i].Cells[1].FindControl("lblSupplierName");
                        Label lblAttentionName = (Label)gvData.Rows[i].Cells[1].FindControl("lblAttentionName");
                        Label lblSupplierEmail = (Label)gvData.Rows[i].Cells[1].FindControl("lblSupplierEmail");
                        Label lblCategory = (Label)gvData.Rows[i].Cells[2].FindControl("lblCategory");

                        DropDownList ddForSending = (DropDownList)gvData.Rows[i].Cells[6].FindControl("ddForSending");

                        string path = Server.MapPath("~/CRF_Request/" + lblCTRLNo.Text.Trim() + "/" + lblCTRLNo.Text.Trim() + ".html");
                        string htmlTemplateSingle = Server.MapPath("~/UserManual/CRF_Template.txt");
                        string userManualPath = Server.MapPath("~/UserManual/CRF NOTES.docx");
                        string attachmentPath = string.Empty;
                        string emailService = string.Empty;

                        int emptyPOPR = 0;

                        if (ibApproved.ImageUrl == "~/images/A2.png")
                        {
                            if (lblStatAll.Text.ToUpper() == "FOR SUPPLY CHAIN BUYER APPROVAL")
                            {
                                // VALIDATE IF FOR SENDING OR NOT FOR SENDING
                                if (ddForSending.SelectedItem.Text.ToUpper() == "YES") // FOR SENDING
                                {
                                    query1 += "UPDATE CRF_TRANSACTION_RequestHead SET ForSending = 1 WHERE CTRLNO = '" + lblCTRLNo.Text.Trim() + "' ";
                                }
                                if (ddForSending.SelectedItem.Text.ToUpper() == "NO") // NOT FOR SENDING
                                {
                                    // NOT FOR SENDING
                                    query1 += "UPDATE CRF_TRANSACTION_RequestHead SET ForSending = 2 WHERE CTRLNO = '" + lblCTRLNo.Text.Trim() + "' ";
                                }

                                if (!string.IsNullOrEmpty(query1))
                                {
                                    // UPDATE DATABASE RECORD
                                    querySuccess = BLL.CRF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();
                                    query1 = string.Empty;
                                }
                            }

                            if (lblStatAll.Text.ToUpper() == "FOR SUPPLY CHAIN INCHARGE APPROVAL")
                            {
                                string tableHeader = string.Empty;
                                string tableDetails = string.Empty;
                                string table = string.Empty;

                                //DETAILS
                                List<Entities_CRF_RequestEntry> listRequestDetails = new List<Entities_CRF_RequestEntry>();
                                Entities_CRF_RequestEntry entityRequestDetails = new Entities_CRF_RequestEntry();
                                entityRequestDetails.RdCtrlNo = lblCTRLNo.Text.Trim();

                                listRequestDetails = BLL.CRF_TRANSACTION_GetRequestDetailsByCTRLNo(entityRequestDetails);

                                if (listRequestDetails != null)
                                {
                                    if (listRequestDetails.Count > 0)
                                    {
                                        foreach (Entities_CRF_RequestEntry entity in listRequestDetails)
                                        {

                                            if (string.IsNullOrEmpty(entity.RdPONO) || string.IsNullOrEmpty(entity.RdPRNO))
                                            {
                                                emptyPOPR++;
                                            }

                                        }

                                        if (emptyPOPR <= 0)
                                        {
                                            // NO EMPTY PO OR PR
                                            hasForPURManagerApproval++;

                                            string prno = string.Empty;
                                            string pono = string.Empty;
                                            string description = string.Empty;
                                            string typeDrawing = string.Empty;
                                            string reason = string.Empty;
                                            string quantity = string.Empty;
                                            string refid = string.Empty;
                                            string uomDesc = string.Empty;
                                            int recCounter = 0;

                                            //SINGLE REQUEST
                                            if (System.IO.File.Exists(htmlTemplateSingle))
                                            {
                                                tableHeader = "<tr><th>REFID</th><th>PONO</th><th>PRNO</th><th>DESCRIPTION</th><th>TYPE/DRAWING</th><th>QUANTITY</th><th>UNIT OF MEASURE</th><th>REASON</th><th>CONFIRMED BY</th><th>DATE CONFIRMED</th><th>NOTES</th></tr>";
                                                //LOOP & GET THE SINGLE RECORD
                                                foreach (Entities_CRF_RequestEntry entity in listRequestDetails)                                                
                                                {
                                                    tempCtrlNo = entity.RdCtrlNo;
                                                    prno = entity.RdPRNO;
                                                    pono = entity.RdPONO;
                                                    description = entity.RdItemName;
                                                    typeDrawing = entity.RdSpecs;
                                                    reason = entity.RdReason;
                                                    quantity = entity.RdQuantity;
                                                    uomDesc = entity.RdUOMDesc;

                                                    // TABLE CREATION
                                                    recCounter++;                                                    
                                                    tableDetails += "<tr><td>" + entity.RdRefId + "</td><td>" + entity.RdPONO + "</td><td>" + entity.RdPRNO + "</td><td>" + entity.RdItemName + "</td><td>" + entity.RdSpecs + "</td><td>" + entity.RdQuantity + "</td><td>" + entity.RdUOMDesc + "</td><td>" + entity.RdReasonName + "</td><td><input type='text' id='cb" + recCounter + "' name='cb" + recCounter + "'></td><td><input type='text' id='dc" + recCounter + "' name='dc" + recCounter + "'></td><td><input type='text' id='nt" + recCounter + "' name='nt" + recCounter + "'></td></tr>";                                                                                                        

                                                    if (!string.IsNullOrEmpty(entity.RdAttachment))
                                                    {
                                                        attachmentPath += Server.MapPath("~/CRF_Request/" + lblCTRLNo.Text.Trim() + "/" + entity.RdAttachment) + ",";
                                                    }                                                    

                                                }

                                                attachmentPath = attachmentPath + path + "," + userManualPath;
                                                table = "<table style='width:100%;'>" + tableHeader + tableDetails + "</table>";

                                                string templateValueSingle = System.IO.File.ReadAllText(htmlTemplateSingle).Replace("filename.csv", lblCTRLNo.Text.Trim() + ".csv")
                                                       .Replace("val_ctrlno", lblCTRLNo.Text.Trim())
                                                       .Replace("val_supplier", lblSupplierName.Text.ToUpper())
                                                       .Replace("val_attn", lblAttentionName.Text.ToUpper())
                                                       .Replace("val_div", Session["DivisionName"].ToString().ToUpper())
                                                       .Replace("val_dept", Session["DepartmentName"].ToString().ToUpper())
                                                       .Replace("val_table", table)
                                                       .Replace("val_title", lblCTRLNo.Text.Trim());


                                                if (!System.IO.File.Exists(path))
                                                {
                                                    using (StreamWriter writer = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write)))
                                                    {
                                                        writer.WriteLine(templateValueSingle);
                                                    }
                                                }

                                            }

                                            // VALIDATE IF FOR SENDING OR NOT FOR SENDING
                                            if (ddForSending.SelectedItem.Text.ToUpper() == "YES") // FOR SENDING
                                            {

                                                // SEND EMAIL NOTIFICATION
                                                emailService = COMMON.sendEmailToSuppliersCRF(lblSupplierEmail.Text.Trim(), ConfigurationManager.AppSettings["email-username"], lblCTRLNo.Text.Trim(),
                                                            "Hi <b>" + lblSupplierName.Text.ToUpper() + "</b> Good Day!" + "<br /><br /> Kindly check the attached html file (" + lblCTRLNo.Text.Trim() + ".html) for our CRF Request" + "<br /><br /><p style='font-size:22px; color:red;'><b>NOTE : </b></p><p style='font-size:18px;'>Please refer to the attached document file (CRF NOTES.docx) on how to update or put your response accordingly.</p>" +
                                                            "<br /><br />For this CRF, please contact <b>" + lblCategory.Text.ToUpper() + "</b> Section <br /><b>PLEASE DO REPLY WITHIN 48 HOURS</b> <br /><br />" +
                                                            "<br /><br /><br />Thank You!<br /><br /><br />" +
                                                            "*** This is an automatically generated email, Please do reply accordingly *** <br /> <br />" +
                                                            "You have received a new CRF Request from ROHM Electronics Philippines Inc." +
                                                            "<br /> <br /><br /><br /> For inquries, kindly see below contact details : <br />" + buyerInformation, attachmentPath, lblSupplierName.Text.ToUpper(), lblCTRLNo.Text.Trim());


                                                // CHECK IF EMAIL SUCCESSFULLY SENT
                                                if (emailService.ToLower().Contains("success"))
                                                {
                                                    tempCtrlNo += "(" + lblCTRLNo.Text.Trim().ToUpper() + " - SUCCESSFULLY SENT) <br />";

                                                    query1 += "UPDATE CRF_TRANSACTION_Status SET BuyerSend ='" + approvedBy + "', DOABuyerSend = CONVERT(VARCHAR, GETDATE(), 22), STATBuyerSend = 1 WHERE CTRLNo = '" + lblCTRLNo.Text.Trim() + "' ";
                                                    queryStatusCounter++;
                                                    query2 += "INSERT INTO CRF_TRANSACTION_SendReceived (CTRLNo, SendReceivedDate, TransactionType) VALUES ('" + lblCTRLNo.Text.Trim() + "', GETDATE(), 'SEND') ";
                                                    queryStatusCounter++;
                                                    query3 += "UPDATE CRF_TRANSACTION_RequestHead SET ForSending = 1 WHERE CTRLNO = '" + lblCTRLNo.Text.Trim() + "' ";
                                                    queryStatusCounter++;
                                                }
                                                else
                                                {
                                                    tempCtrlNo += "(" + lblCTRLNo.Text.Trim().ToUpper() + " - FAILED TO SEND) <br />";
                                                }

                                            }
                                            else
                                            {
                                                // NOT FOR SENDING
                                                tempCtrlNo += "(" + lblCTRLNo.Text.Trim().ToUpper() + " - SUCCESSFULLY APPROVED) <br />";
                                                query3 += "UPDATE CRF_TRANSACTION_RequestHead SET ForSending = 2 WHERE CTRLNO = '" + lblCTRLNo.Text.Trim() + "' ";
                                                queryStatusCounter++;
                                            }

                                            if (!string.IsNullOrEmpty(query1) || !string.IsNullOrEmpty(query2) || !string.IsNullOrEmpty(query3))
                                            {
                                                // UPDATE DATABASE RECORD
                                                querySuccess = BLL.CRF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + query2 + query3 + queryEndPart).ToString();
                                                query1 = string.Empty;
                                                query2 = string.Empty;
                                                query3 = string.Empty;
                                            }

                                            // IF SUCCESSFULLY THEN REDIRECT TO SUCCESS PAGE
                                            if (querySuccess == queryStatusCounter.ToString())
                                            {

                                                Session["successMessage"] = "CRF NUMBER(S) : <br /> <b>" + tempCtrlNo + "</b> <br /> HAS BEEN SUCCESSFULLY PROCESSED.";
                                                Session["successTransactionName"] = "CRF_APPROVALFORM";
                                                Session["successReturnPage"] = "CRF_ApprovalForm.aspx";

                                                Response.Redirect("SuccessPage.aspx", false);
                                            }
                                            else
                                            {
                                                Session["successMessage"] = "CRF NUMBER(S) : <br /> <b>" + tempCtrlNo + "</b> <br /> HAS BEEN SUCCESSFULLY PROCESSED.";
                                                Session["successTransactionName"] = "CRF_APPROVALFORM";
                                                Session["successReturnPage"] = "CRF_ApprovalForm.aspx";

                                                Response.Redirect("SuccessPage.aspx", false);
                                            }

                                        }
                                        else // EMPTY PR OR PO, JUST APPROVED BUT DO NOT SEND TO SUPPLIER
                                        {
                                            Session["successMessage"] = "CRF NUMBER(S) : <b>" + tempCtrlNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
                                            Session["successTransactionName"] = "CRF_APPROVALFORM";
                                            Session["successReturnPage"] = "CRF_ApprovalForm.aspx";

                                            Response.Redirect("SuccessPage.aspx", false);
                                        }

                                    }
                                }



                            } //End of if (lblStatAll.Text.ToUpper() == "FOR PUR. MNGR APPROVAL")
                            else // IF NOT SUPPLY CHAIN INCHARGE
                            {
                                tempCtrlNo += lblCTRLNo.Text.Trim() + ", ";
                                Session["successMessage"] = "CRF NUMBER(S) : <b>" + tempCtrlNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
                                Session["successTransactionName"] = "CRF_APPROVALFORM";
                                Session["successReturnPage"] = "CRF_ApprovalForm.aspx";

                                Response.Redirect("SuccessPage.aspx", false);
                            }

                        } // ending of  if (ibApproved.ImageUrl == "~/images/A2.png")

                    } // ending of [for (int i = 0; i < gvData.Rows.Count; i++)]                    

                }

                Session["successMessage"] = "CRF NUMBER(S) : <b>" + tempCtrlNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
                Session["successTransactionName"] = "CRF_APPROVALFORM";
                Session["successReturnPage"] = "CRF_ApprovalForm.aspx";

                Response.Redirect("SuccessPage.aspx", false);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }

        }

        private string setReason(string reason, string value)
        {
            string ret = "white;";

            if (reason == "SUPPLIER CHANGE" && value == "valreason_supplierchange")
            {
                ret = "black;";
            }

            else if (reason == "CHANGE PACKING" && value == "valreason_changepacking")
            {
                ret = "black;";
            }

            else if (reason == "CHANGE SPECIFICATION" && value == "valreason_changespecification")
            {
                ret = "black;";
            }

            else if (reason == "PRICE CHANGE" && value == "valreason_pricechange")
            {
                ret = "black;";
            }

            else if (reason == "CHANGE MAKER" && value == "valreason_changemaker")
            {
                ret = "black;";
            }

            else if (reason == "STOP PRODUCTION" && value == "valreason_stopproduction")
            {
                ret = "black;";
            }

            else if (reason == "QUANTITY CHANGE" && value == "valreason_quantitychange")
            {
                ret = "black;";
            }

            else if (reason == "OUT OF STOCK" && value == "valreason_outofstock")
            {
                ret = "black;";
            }

            else if (reason == "EXCESS ORDER" && value == "valreason_excessorder")
            {
                ret = "black;";
            }

            return ret;
        }





    }
}
