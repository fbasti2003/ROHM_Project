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
    public partial class URF_ApprovalForm_New : System.Web.UI.Page
    {


        BLL_URF BLL = new BLL_URF();
        BLL_RFQ BLL_RFQ = new BLL_RFQ();
        Common COMMON = new Common();

        BLL_Common BLL_COMMON = new BLL_Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Session["URF_Prod_SecManager"].ToString()) || !string.IsNullOrEmpty(Session["URF_Prod_DeptManager"].ToString())
                    || !string.IsNullOrEmpty(Session["URF_Prod_DivManager"].ToString()) || !string.IsNullOrEmpty(Session["URF_Prod_HQManager"].ToString()))
                {
                    if (Session["URF_Prod_SecManager"].ToString().ToLower() == "true" || Session["URF_Prod_DeptManager"].ToString().ToLower() == "true"
                        || Session["URF_Prod_DivManager"].ToString().ToLower() == "true" || Session["URF_Prod_HQManager"].ToString().ToLower() == "true"
                        || COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()))
                    {

                        //---------------------------------------------------------------------------------------------------

                        List<Entities_URF_RequestEntry> listDropDown = new List<Entities_URF_RequestEntry>();
                        listDropDown = BLL.URF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                        if (listDropDown != null)
                        {
                            if (listDropDown.Count > 0)
                            {
                                ddCategory.Items.Add("");

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

                                if (!string.IsNullOrEmpty(Session["CategoryAccess"].ToString()))
                                {
                                    if (int.Parse(Session["CategoryAccess"].ToString()) > 0)
                                    {
                                        ddCategory.Items.FindByValue(Session["CategoryAccess"].ToString()).Selected = true;
                                        ddCategory.Enabled = false;
                                    }
                                }

                            }
                        }

                        //---------------------------------------------------------------------------------------------------

                        txtFrom.Text = DateTime.Today.AddDays(-365).ToString("MM/dd/yyyy");
                        txtTo.Text = DateTime.Today.ToString("MM/dd/yyyy");

                        btnSubmit_Click(sender, e);
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool isPurchasing = false;
                string username = Session["Username"].ToString();
                string categoryAccess = Session["CategoryAccess"].ToString();


                if (!string.IsNullOrEmpty(categoryAccess))
                {
                    if (int.Parse(categoryAccess) > 0)
                    {
                        isPurchasing = true;
                    }
                }
                if (username == "6985" || username == "3844" || username == "1152" || username == "1402" || username == "002")
                {
                    isPurchasing = true;
                }


                //------------------------------------------------------------------------------------------------------

                List<Entities_URF_RequestEntry> listField = new List<Entities_URF_RequestEntry>();
                Entities_URF_RequestEntry prodSecManager = new Entities_URF_RequestEntry();
                Entities_URF_RequestEntry prodDeptManager = new Entities_URF_RequestEntry();
                Entities_URF_RequestEntry prodDivManager = new Entities_URF_RequestEntry();
                Entities_URF_RequestEntry prodHQManager = new Entities_URF_RequestEntry();
                string whereClause = string.Empty;

                //if (Session["URF_Prod_SecManager"].ToString().ToLower() == "true")
                //{
                //    prodSecManager.ProdArrayApprovalField = "STATProdSecManager = 0";
                //    listField.Add(prodSecManager);                    
                //}
                //if (Session["URF_Prod_DeptManager"].ToString().ToLower() == "true")
                //{
                //    prodDeptManager.ProdArrayApprovalField = "STATProdDeptManager = 0 AND STATProdSecManager = 1";
                //    listField.Add(prodDeptManager);
                //}
                //if (Session["URF_Prod_DivManager"].ToString().ToLower() == "true")
                //{
                //    prodDivManager.ProdArrayApprovalField = "STATProdDivManager = 0 AND STATProdDeptManager = 1";
                //    listField.Add(prodDivManager);
                //}
                //if (Session["URF_Prod_HQManager"].ToString().ToLower() == "true")
                //{
                //    prodHQManager.ProdArrayApprovalField = "STATProdHQManager = 0 AND STATProdDivManager = 1";
                //    listField.Add(prodHQManager);
                //}

                //foreach (Entities_URF_RequestEntry field in listField)
                //{
                //    whereClause += field.ProdArrayApprovalField + " OR ";
                //}

                //------------------------------------------------------------------------------------------------------

                List<Entities_URF_RequestEntry> listFinalPurchasing = new List<Entities_URF_RequestEntry>();
                List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();
                List<Entities_URF_RequestEntry> listDivisionManager = new List<Entities_URF_RequestEntry>();
                List<Entities_URF_RequestEntry> listHQManager = new List<Entities_URF_RequestEntry>();
                List<Entities_URF_RequestEntry> listDepartmentManager = new List<Entities_URF_RequestEntry>();
                List<Entities_URF_RequestEntry> listSectionManager = new List<Entities_URF_RequestEntry>();
                Entities_URF_RequestEntry entity = new Entities_URF_RequestEntry();

                entity.RhRequester = Session["LcRefId"].ToString();
                entity.RhCtrlNo = txtURFNo.Text;
                entity.DrFrom = txtFrom.Text;
                entity.DrTo = txtTo.Text;

                //if (listField.Count > 0)
                //{
                //    entity.ProdArrayApprovalField = whereClause.Remove((whereClause.Length - 4), 4).ToString();
                //}

                list = null;

                // FOR PRODUCTION APPROVAL
                if (!isPurchasing)
                {
                    if (!string.IsNullOrEmpty(txtURFNo.Text) || txtURFNo.Text.Length > 0)
                    {
                        Session["Search_From_URF_Inquiry"] = txtURFNo.Text;
                        Response.Redirect("URF_AllRequest.aspx");
                    }
                    else
                    {
                        if (ddCategory.SelectedItem.Text.ToLower() == "")
                        {
                            //SECTION MANAGER
                            if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), "401").ToString().ToLower() == "true")
                            {
                                list = new List<Entities_URF_RequestEntry>();
                                entity.ProdArrayApprovalField = "STATProdSecManager = 0";
                                //listSectionManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.LcDepartment == Session["Department"].ToString()).ToList();

                                if (ddStatus.SelectedValue == "ALL")
                                {
                                    listSectionManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.LcDepartment == Session["Department"].ToString()).ToList();
                                }
                                else
                                {
                                    listSectionManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.LcDepartment == Session["Department"].ToString() && itm.StatAll == ddStatus.SelectedValue).ToList();
                                }

                                if (listSectionManager != null || listSectionManager.Count > 0)
                                {
                                    foreach (Entities_URF_RequestEntry entitySec in listSectionManager)
                                    {
                                        Entities_URF_RequestEntry entitySection = new Entities_URF_RequestEntry();
                                        entitySection.Criteria = entitySec.Criteria;
                                        entitySection.CssColorCode = entitySec.CssColorCode;
                                        entitySection.DataIndex = entitySec.DataIndex;
                                        entitySection.DrFrom = entitySec.DrFrom;
                                        entitySection.DropdownName = entitySec.DropdownName;
                                        entitySection.DropdownRefId = entitySec.DropdownRefId;
                                        entitySection.IsDisabled = entitySec.IsDisabled;
                                        entitySection.LcDepartment = entitySec.LcDepartment;
                                        entitySection.ProdArrayApprovalField = entitySec.ProdArrayApprovalField;
                                        entitySection.RdCtrlNo = entitySec.RdCtrlNo;
                                        entitySection.RdDeliveryConfirmationDate = entitySec.RdDeliveryConfirmationDate;
                                        entitySection.RdItemName = entitySec.RdItemName;
                                        entitySection.RdPONO = entitySec.RdPONO;
                                        entitySection.RdPRNO = entitySec.RdPRNO;
                                        entitySection.RdQuantity = entitySec.RdQuantity;
                                        entitySection.RdRefId = entitySec.RdRefId;
                                        entitySection.RdReplyDeliveryDate = entitySec.RdReplyDeliveryDate;
                                        entitySection.RdRequestedDeliveryDate = entitySec.RdRequestedDeliveryDate;
                                        entitySection.RdSpecs = entitySec.RdSpecs;
                                        entitySection.RdUnitOfMeasure = entitySec.RdUnitOfMeasure;
                                        entitySection.RhAttachment1 = entitySec.RhAttachment1;
                                        entitySection.RhAttachment2 = entitySec.RhAttachment2;
                                        entitySection.RhAttention = entitySec.RhAttention;
                                        entitySection.RhCategory = entitySec.RhCategory;
                                        entitySection.RhCategoryName = entitySec.RhCategoryName;
                                        entitySection.RhCtrlNo = entitySec.RhCtrlNo;
                                        entitySection.RhOtherReason = entitySec.RhOtherReason;
                                        entitySection.RhPurchasingRemarks = entitySec.RhPurchasingRemarks;
                                        entitySection.RhReason = entitySec.RhReason;
                                        entitySection.RhRequester = entitySec.RhRequester;
                                        entitySection.RhSendReceived = entitySec.RhSendReceived;
                                        entitySection.RhStockLifeAttachment = entitySec.RhStockLifeAttachment;
                                        entitySection.RhSupplier = entitySec.RhSupplier;
                                        entitySection.RhSupplierComments = entitySec.RhSupplierComments;
                                        entitySection.RhSupplierEmail = entitySec.RhSupplierEmail;
                                        entitySection.RhSupplierId = entitySec.RhSupplierId;
                                        entitySection.RhTransactionDate = entitySec.RhTransactionDate;
                                        entitySection.RhType = entitySec.RhType;
                                        entitySection.RhUpdatedBy = entitySec.RhUpdatedBy;
                                        entitySection.RhUpdatedDate = entitySec.RhUpdatedDate;
                                        entitySection.RowNumber = entitySec.RowNumber;
                                        entitySection.StatAll = entitySec.StatAll;
                                        entitySection.StatClosed = entitySec.StatClosed;
                                        entitySection.StatCtrlNo = entitySec.StatCtrlNo;
                                        entitySection.StatDOAProdDeptManager = entitySec.StatDOAProdDeptManager;
                                        entitySection.StatDOAProdDivManager = entitySec.StatDOAProdDivManager;
                                        entitySection.StatDOAProdHQManager = entitySec.StatDOAProdHQManager;
                                        entitySection.StatDOAProdSecManager = entitySec.StatDOAProdSecManager;
                                        entitySection.StatDOAPurchasingBuyer = entitySec.StatDOAPurchasingBuyer;
                                        entitySection.StatDOAPurchasingManager = entitySec.StatDOAPurchasingManager;
                                        entitySection.StatProdDeptManager = entitySec.StatProdDeptManager;
                                        entitySection.StatProdDivManager = entitySec.StatProdDivManager;
                                        entitySection.StatProdHQManager = entitySec.StatProdHQManager;
                                        entitySection.StatProdSecManager = entitySec.StatProdSecManager;
                                        entitySection.StatPurchasingBuyer = entitySec.StatPurchasingBuyer;
                                        entitySection.StatPurchasingManager = entitySec.StatPurchasingManager;
                                        entitySection.StatRemarks = entitySec.StatRemarks;
                                        entitySection.StatReOpenRemarks = entitySec.StatReOpenRemarks;
                                        entitySection.StatSTATProdDeptManager = entitySec.StatSTATProdDeptManager;
                                        entitySection.StatSTATProdDivManager = entitySec.StatSTATProdDivManager;
                                        entitySection.StatSTATProdHQManager = entitySec.StatSTATProdHQManager;
                                        entitySection.StatSTATProdSecManager = entitySec.StatSTATProdSecManager;
                                        entitySection.StatSTATPurchasingBuyer = entitySec.StatSTATPurchasingBuyer;
                                        entitySection.StatSTATPurchasingManager = entitySec.StatSTATPurchasingManager;
                                        entitySection.SupplierAttachment = entitySec.SupplierAttachment;
                                        entitySection.TableName = entitySec.TableName;
                                        entitySection.RhRequesterEmail = entitySec.RhRequesterEmail;
                                        list.Add(entitySection);
                                    }
                                }
                            }
                            //DEPARTMENT MANAGER
                            if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), "402").ToString().ToLower() == "true")
                            {
                                if (list == null || list.Count <= 0)
                                {
                                    list = new List<Entities_URF_RequestEntry>();
                                }
                                entity.ProdArrayApprovalField = "STATProdDeptManager = 0 AND (STATProdSecManager = 1 OR STATProdSecManager = -1)";
                                //listDepartmentManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.LcDepartment == Session["Department"].ToString()).ToList();

                                if (ddStatus.SelectedValue == "ALL")
                                {
                                    listDepartmentManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.LcDepartment == Session["Department"].ToString()).ToList();
                                }
                                else
                                {
                                    listDepartmentManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.LcDepartment == Session["Department"].ToString() && itm.StatAll == ddStatus.SelectedValue).ToList();
                                }

                                if (listDepartmentManager != null || listDepartmentManager.Count > 0)
                                {
                                    foreach (Entities_URF_RequestEntry entityDept in listDepartmentManager)
                                    {
                                        Entities_URF_RequestEntry entityDepartment = new Entities_URF_RequestEntry();
                                        entityDepartment.Criteria = entityDept.Criteria;
                                        entityDepartment.CssColorCode = entityDept.CssColorCode;
                                        entityDepartment.DataIndex = entityDept.DataIndex;
                                        entityDepartment.DrFrom = entityDept.DrFrom;
                                        entityDepartment.DropdownName = entityDept.DropdownName;
                                        entityDepartment.DropdownRefId = entityDept.DropdownRefId;
                                        entityDepartment.IsDisabled = entityDept.IsDisabled;
                                        entityDepartment.LcDepartment = entityDept.LcDepartment;
                                        entityDepartment.ProdArrayApprovalField = entityDept.ProdArrayApprovalField;
                                        entityDepartment.RdCtrlNo = entityDept.RdCtrlNo;
                                        entityDepartment.RdDeliveryConfirmationDate = entityDept.RdDeliveryConfirmationDate;
                                        entityDepartment.RdItemName = entityDept.RdItemName;
                                        entityDepartment.RdPONO = entityDept.RdPONO;
                                        entityDepartment.RdPRNO = entityDept.RdPRNO;
                                        entityDepartment.RdQuantity = entityDept.RdQuantity;
                                        entityDepartment.RdRefId = entityDept.RdRefId;
                                        entityDepartment.RdReplyDeliveryDate = entityDept.RdReplyDeliveryDate;
                                        entityDepartment.RdRequestedDeliveryDate = entityDept.RdRequestedDeliveryDate;
                                        entityDepartment.RdSpecs = entityDept.RdSpecs;
                                        entityDepartment.RdUnitOfMeasure = entityDept.RdUnitOfMeasure;
                                        entityDepartment.RhAttachment1 = entityDept.RhAttachment1;
                                        entityDepartment.RhAttachment2 = entityDept.RhAttachment2;
                                        entityDepartment.RhAttention = entityDept.RhAttention;
                                        entityDepartment.RhCategory = entityDept.RhCategory;
                                        entityDepartment.RhCategoryName = entityDept.RhCategoryName;
                                        entityDepartment.RhCtrlNo = entityDept.RhCtrlNo;
                                        entityDepartment.RhOtherReason = entityDept.RhOtherReason;
                                        entityDepartment.RhPurchasingRemarks = entityDept.RhPurchasingRemarks;
                                        entityDepartment.RhReason = entityDept.RhReason;
                                        entityDepartment.RhRequester = entityDept.RhRequester;
                                        entityDepartment.RhSendReceived = entityDept.RhSendReceived;
                                        entityDepartment.RhStockLifeAttachment = entityDept.RhStockLifeAttachment;
                                        entityDepartment.RhSupplier = entityDept.RhSupplier;
                                        entityDepartment.RhSupplierComments = entityDept.RhSupplierComments;
                                        entityDepartment.RhSupplierEmail = entityDept.RhSupplierEmail;
                                        entityDepartment.RhSupplierId = entityDept.RhSupplierId;
                                        entityDepartment.RhTransactionDate = entityDept.RhTransactionDate;
                                        entityDepartment.RhType = entityDept.RhType;
                                        entityDepartment.RhUpdatedBy = entityDept.RhUpdatedBy;
                                        entityDepartment.RhUpdatedDate = entityDept.RhUpdatedDate;
                                        entityDepartment.RowNumber = entityDept.RowNumber;
                                        entityDepartment.StatAll = entityDept.StatAll;
                                        entityDepartment.StatClosed = entityDept.StatClosed;
                                        entityDepartment.StatCtrlNo = entityDept.StatCtrlNo;
                                        entityDepartment.StatDOAProdDeptManager = entityDept.StatDOAProdDeptManager;
                                        entityDepartment.StatDOAProdDivManager = entityDept.StatDOAProdDivManager;
                                        entityDepartment.StatDOAProdHQManager = entityDept.StatDOAProdHQManager;
                                        entityDepartment.StatDOAProdSecManager = entityDept.StatDOAProdSecManager;
                                        entityDepartment.StatDOAPurchasingBuyer = entityDept.StatDOAPurchasingBuyer;
                                        entityDepartment.StatDOAPurchasingManager = entityDept.StatDOAPurchasingManager;
                                        entityDepartment.StatProdDeptManager = entityDept.StatProdDeptManager;
                                        entityDepartment.StatProdDivManager = entityDept.StatProdDivManager;
                                        entityDepartment.StatProdHQManager = entityDept.StatProdHQManager;
                                        entityDepartment.StatProdSecManager = entityDept.StatProdSecManager;
                                        entityDepartment.StatPurchasingBuyer = entityDept.StatPurchasingBuyer;
                                        entityDepartment.StatPurchasingManager = entityDept.StatPurchasingManager;
                                        entityDepartment.StatRemarks = entityDept.StatRemarks;
                                        entityDepartment.StatReOpenRemarks = entityDept.StatReOpenRemarks;
                                        entityDepartment.StatSTATProdDeptManager = entityDept.StatSTATProdDeptManager;
                                        entityDepartment.StatSTATProdDivManager = entityDept.StatSTATProdDivManager;
                                        entityDepartment.StatSTATProdHQManager = entityDept.StatSTATProdHQManager;
                                        entityDepartment.StatSTATProdSecManager = entityDept.StatSTATProdSecManager;
                                        entityDepartment.StatSTATPurchasingBuyer = entityDept.StatSTATPurchasingBuyer;
                                        entityDepartment.StatSTATPurchasingManager = entityDept.StatSTATPurchasingManager;
                                        entityDepartment.SupplierAttachment = entityDept.SupplierAttachment;
                                        entityDepartment.TableName = entityDept.TableName;
                                        entityDepartment.RhRequesterEmail = entityDept.RhRequesterEmail;
                                        list.Add(entityDepartment);
                                    }
                                }
                            }
                            // DIVISION MANAGER
                            if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), "403").ToString().ToLower() == "true")
                            {
                                if (list == null || list.Count <= 0)
                                {
                                    list = new List<Entities_URF_RequestEntry>();
                                }
                                entity.ProdArrayApprovalField = "STATProdDivManager = 0 AND (STATProdDeptManager = 1 OR STATProdDeptManager = -1) AND STATProdSecManager != 2";
                                //listDivisionManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.LcDivision == Session["Division"].ToString()).ToList();

                                if (ddStatus.SelectedValue == "ALL")
                                {
                                    listDivisionManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.LcDivision == Session["Division"].ToString()).ToList();
                                }
                                else
                                {
                                    listDivisionManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.LcDivision == Session["Division"].ToString() && itm.StatAll == ddStatus.SelectedValue).ToList();
                                }

                                if (listDivisionManager != null || listDivisionManager.Count > 0)
                                {
                                    foreach (Entities_URF_RequestEntry entityDiv in listDivisionManager)
                                    {
                                        Entities_URF_RequestEntry entityDivision = new Entities_URF_RequestEntry();
                                        entityDivision.Criteria = entityDiv.Criteria;
                                        entityDivision.CssColorCode = entityDiv.CssColorCode;
                                        entityDivision.DataIndex = entityDiv.DataIndex;
                                        entityDivision.DrFrom = entityDiv.DrFrom;
                                        entityDivision.DropdownName = entityDiv.DropdownName;
                                        entityDivision.DropdownRefId = entityDiv.DropdownRefId;
                                        entityDivision.IsDisabled = entityDiv.IsDisabled;
                                        entityDivision.LcDepartment = entityDiv.LcDepartment;
                                        entityDivision.ProdArrayApprovalField = entityDiv.ProdArrayApprovalField;
                                        entityDivision.RdCtrlNo = entityDiv.RdCtrlNo;
                                        entityDivision.RdDeliveryConfirmationDate = entityDiv.RdDeliveryConfirmationDate;
                                        entityDivision.RdItemName = entityDiv.RdItemName;
                                        entityDivision.RdPONO = entityDiv.RdPONO;
                                        entityDivision.RdPRNO = entityDiv.RdPRNO;
                                        entityDivision.RdQuantity = entityDiv.RdQuantity;
                                        entityDivision.RdRefId = entityDiv.RdRefId;
                                        entityDivision.RdReplyDeliveryDate = entityDiv.RdReplyDeliveryDate;
                                        entityDivision.RdRequestedDeliveryDate = entityDiv.RdRequestedDeliveryDate;
                                        entityDivision.RdSpecs = entityDiv.RdSpecs;
                                        entityDivision.RdUnitOfMeasure = entityDiv.RdUnitOfMeasure;
                                        entityDivision.RhAttachment1 = entityDiv.RhAttachment1;
                                        entityDivision.RhAttachment2 = entityDiv.RhAttachment2;
                                        entityDivision.RhAttention = entityDiv.RhAttention;
                                        entityDivision.RhCategory = entityDiv.RhCategory;
                                        entityDivision.RhCategoryName = entityDiv.RhCategoryName;
                                        entityDivision.RhCtrlNo = entityDiv.RhCtrlNo;
                                        entityDivision.RhOtherReason = entityDiv.RhOtherReason;
                                        entityDivision.RhPurchasingRemarks = entityDiv.RhPurchasingRemarks;
                                        entityDivision.RhReason = entityDiv.RhReason;
                                        entityDivision.RhRequester = entityDiv.RhRequester;
                                        entityDivision.RhSendReceived = entityDiv.RhSendReceived;
                                        entityDivision.RhStockLifeAttachment = entityDiv.RhStockLifeAttachment;
                                        entityDivision.RhSupplier = entityDiv.RhSupplier;
                                        entityDivision.RhSupplierComments = entityDiv.RhSupplierComments;
                                        entityDivision.RhSupplierEmail = entityDiv.RhSupplierEmail;
                                        entityDivision.RhSupplierId = entityDiv.RhSupplierId;
                                        entityDivision.RhTransactionDate = entityDiv.RhTransactionDate;
                                        entityDivision.RhType = entityDiv.RhType;
                                        entityDivision.RhUpdatedBy = entityDiv.RhUpdatedBy;
                                        entityDivision.RhUpdatedDate = entityDiv.RhUpdatedDate;
                                        entityDivision.RowNumber = entityDiv.RowNumber;
                                        entityDivision.StatAll = entityDiv.StatAll;
                                        entityDivision.StatClosed = entityDiv.StatClosed;
                                        entityDivision.StatCtrlNo = entityDiv.StatCtrlNo;
                                        entityDivision.StatDOAProdDeptManager = entityDiv.StatDOAProdDeptManager;
                                        entityDivision.StatDOAProdDivManager = entityDiv.StatDOAProdDivManager;
                                        entityDivision.StatDOAProdHQManager = entityDiv.StatDOAProdHQManager;
                                        entityDivision.StatDOAProdSecManager = entityDiv.StatDOAProdSecManager;
                                        entityDivision.StatDOAPurchasingBuyer = entityDiv.StatDOAPurchasingBuyer;
                                        entityDivision.StatDOAPurchasingManager = entityDiv.StatDOAPurchasingManager;
                                        entityDivision.StatProdDeptManager = entityDiv.StatProdDeptManager;
                                        entityDivision.StatProdDivManager = entityDiv.StatProdDivManager;
                                        entityDivision.StatProdHQManager = entityDiv.StatProdHQManager;
                                        entityDivision.StatProdSecManager = entityDiv.StatProdSecManager;
                                        entityDivision.StatPurchasingBuyer = entityDiv.StatPurchasingBuyer;
                                        entityDivision.StatPurchasingManager = entityDiv.StatPurchasingManager;
                                        entityDivision.StatRemarks = entityDiv.StatRemarks;
                                        entityDivision.StatReOpenRemarks = entityDiv.StatReOpenRemarks;
                                        entityDivision.StatSTATProdDeptManager = entityDiv.StatSTATProdDeptManager;
                                        entityDivision.StatSTATProdDivManager = entityDiv.StatSTATProdDivManager;
                                        entityDivision.StatSTATProdHQManager = entityDiv.StatSTATProdHQManager;
                                        entityDivision.StatSTATProdSecManager = entityDiv.StatSTATProdSecManager;
                                        entityDivision.StatSTATPurchasingBuyer = entityDiv.StatSTATPurchasingBuyer;
                                        entityDivision.StatSTATPurchasingManager = entityDiv.StatSTATPurchasingManager;
                                        entityDivision.SupplierAttachment = entityDiv.SupplierAttachment;
                                        entityDivision.TableName = entityDiv.TableName;
                                        entityDivision.RhRequesterEmail = entityDiv.RhRequesterEmail;
                                        list.Add(entityDivision);
                                    }
                                }
                            }

                            // HQ MANAGER
                            if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), "404").ToString().ToLower() == "true")
                            {
                                if (list == null || list.Count <= 0)
                                {
                                    list = new List<Entities_URF_RequestEntry>();
                                }
                                entity.ProdArrayApprovalField = "STATProdHQManager = 0 AND (STATProdDivManager = 1 OR STATProdDivManager = -1) AND STATProdSecManager != 2";
                                //listHQManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.LcHQ == Session["HQ"].ToString()).ToList();

                                if (ddStatus.SelectedValue == "ALL")
                                {
                                    listHQManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.LcHQ == Session["HQ"].ToString()).ToList();
                                }
                                else
                                {
                                    listHQManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.LcHQ == Session["HQ"].ToString() && itm.StatAll == ddStatus.SelectedValue).ToList();
                                }

                                if (listHQManager != null || listHQManager.Count > 0)
                                {
                                    foreach (Entities_URF_RequestEntry entityH in listHQManager)
                                    {
                                        Entities_URF_RequestEntry entityHQ = new Entities_URF_RequestEntry();
                                        entityHQ.Criteria = entityH.Criteria;
                                        entityHQ.CssColorCode = entityH.CssColorCode;
                                        entityHQ.DataIndex = entityH.DataIndex;
                                        entityHQ.DrFrom = entityH.DrFrom;
                                        entityHQ.DropdownName = entityH.DropdownName;
                                        entityHQ.DropdownRefId = entityH.DropdownRefId;
                                        entityHQ.IsDisabled = entityH.IsDisabled;
                                        entityHQ.LcDepartment = entityH.LcDepartment;
                                        entityHQ.ProdArrayApprovalField = entityH.ProdArrayApprovalField;
                                        entityHQ.RdCtrlNo = entityH.RdCtrlNo;
                                        entityHQ.RdDeliveryConfirmationDate = entityH.RdDeliveryConfirmationDate;
                                        entityHQ.RdItemName = entityH.RdItemName;
                                        entityHQ.RdPONO = entityH.RdPONO;
                                        entityHQ.RdPRNO = entityH.RdPRNO;
                                        entityHQ.RdQuantity = entityH.RdQuantity;
                                        entityHQ.RdRefId = entityH.RdRefId;
                                        entityHQ.RdReplyDeliveryDate = entityH.RdReplyDeliveryDate;
                                        entityHQ.RdRequestedDeliveryDate = entityH.RdRequestedDeliveryDate;
                                        entityHQ.RdSpecs = entityH.RdSpecs;
                                        entityHQ.RdUnitOfMeasure = entityH.RdUnitOfMeasure;
                                        entityHQ.RhAttachment1 = entityH.RhAttachment1;
                                        entityHQ.RhAttachment2 = entityH.RhAttachment2;
                                        entityHQ.RhAttention = entityH.RhAttention;
                                        entityHQ.RhCategory = entityH.RhCategory;
                                        entityHQ.RhCategoryName = entityH.RhCategoryName;
                                        entityHQ.RhCtrlNo = entityH.RhCtrlNo;
                                        entityHQ.RhOtherReason = entityH.RhOtherReason;
                                        entityHQ.RhPurchasingRemarks = entityH.RhPurchasingRemarks;
                                        entityHQ.RhReason = entityH.RhReason;
                                        entityHQ.RhRequester = entityH.RhRequester;
                                        entityHQ.RhSendReceived = entityH.RhSendReceived;
                                        entityHQ.RhStockLifeAttachment = entityH.RhStockLifeAttachment;
                                        entityHQ.RhSupplier = entityH.RhSupplier;
                                        entityHQ.RhSupplierComments = entityH.RhSupplierComments;
                                        entityHQ.RhSupplierEmail = entityH.RhSupplierEmail;
                                        entityHQ.RhSupplierId = entityH.RhSupplierId;
                                        entityHQ.RhTransactionDate = entityH.RhTransactionDate;
                                        entityHQ.RhType = entityH.RhType;
                                        entityHQ.RhUpdatedBy = entityH.RhUpdatedBy;
                                        entityHQ.RhUpdatedDate = entityH.RhUpdatedDate;
                                        entityHQ.RowNumber = entityH.RowNumber;
                                        entityHQ.StatAll = entityH.StatAll;
                                        entityHQ.StatClosed = entityH.StatClosed;
                                        entityHQ.StatCtrlNo = entityH.StatCtrlNo;
                                        entityHQ.StatDOAProdDeptManager = entityH.StatDOAProdDeptManager;
                                        entityHQ.StatDOAProdDivManager = entityH.StatDOAProdDivManager;
                                        entityHQ.StatDOAProdHQManager = entityH.StatDOAProdHQManager;
                                        entityHQ.StatDOAProdSecManager = entityH.StatDOAProdSecManager;
                                        entityHQ.StatDOAPurchasingBuyer = entityH.StatDOAPurchasingBuyer;
                                        entityHQ.StatDOAPurchasingManager = entityH.StatDOAPurchasingManager;
                                        entityHQ.StatProdDeptManager = entityH.StatProdDeptManager;
                                        entityHQ.StatProdDivManager = entityH.StatProdDivManager;
                                        entityHQ.StatProdHQManager = entityH.StatProdHQManager;
                                        entityHQ.StatProdSecManager = entityH.StatProdSecManager;
                                        entityHQ.StatPurchasingBuyer = entityH.StatPurchasingBuyer;
                                        entityHQ.StatPurchasingManager = entityH.StatPurchasingManager;
                                        entityHQ.StatRemarks = entityH.StatRemarks;
                                        entityHQ.StatReOpenRemarks = entityH.StatReOpenRemarks;
                                        entityHQ.StatSTATProdDeptManager = entityH.StatSTATProdDeptManager;
                                        entityHQ.StatSTATProdDivManager = entityH.StatSTATProdDivManager;
                                        entityHQ.StatSTATProdHQManager = entityH.StatSTATProdHQManager;
                                        entityHQ.StatSTATProdSecManager = entityH.StatSTATProdSecManager;
                                        entityHQ.StatSTATPurchasingBuyer = entityH.StatSTATPurchasingBuyer;
                                        entityHQ.StatSTATPurchasingManager = entityH.StatSTATPurchasingManager;
                                        entityHQ.SupplierAttachment = entityH.SupplierAttachment;
                                        entityHQ.TableName = entityH.TableName;
                                        entityHQ.RhRequesterEmail = entityH.RhRequesterEmail;
                                        list.Add(entityHQ);
                                    }
                                }
                            }

                            //// HQ MANAGER
                            //if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), "404").ToString() == "true")
                            //{
                            //    list = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.LcHQ == Session["HQ"].ToString()).ToList();
                            //}
                            //else if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), "403").ToString() == "true") // Division Manager
                            //{
                            //    list = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.LcDivision == Session["Division"].ToString()).ToList();
                            //}
                            //else
                            //{
                            //    list = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.LcDepartment == Session["Department"].ToString()).ToList();
                            //}
                        }
                        else
                        {
                            //SECTION MANAGER
                            if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), "401").ToString().ToLower() == "true")
                            {
                                list = new List<Entities_URF_RequestEntry>();
                                entity.ProdArrayApprovalField = "STATProdSecManager = 0";
                                //listSectionManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim() && itm.LcDepartment == Session["Department"].ToString()).ToList();

                                if (ddStatus.SelectedValue == "ALL")
                                {
                                    listSectionManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim() && itm.LcDepartment == Session["Department"].ToString()).ToList();
                                }
                                else
                                {
                                    listSectionManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim() && itm.LcDepartment == Session["Department"].ToString() && itm.StatAll == ddStatus.SelectedValue).ToList();
                                }

                                if (listSectionManager != null || listSectionManager.Count > 0)
                                {
                                    foreach (Entities_URF_RequestEntry entitySec in listDepartmentManager)
                                    {
                                        Entities_URF_RequestEntry entitySection = new Entities_URF_RequestEntry();
                                        entitySection.Criteria = entitySec.Criteria;
                                        entitySection.CssColorCode = entitySec.CssColorCode;
                                        entitySection.DataIndex = entitySec.DataIndex;
                                        entitySection.DrFrom = entitySec.DrFrom;
                                        entitySection.DropdownName = entitySec.DropdownName;
                                        entitySection.DropdownRefId = entitySec.DropdownRefId;
                                        entitySection.IsDisabled = entitySec.IsDisabled;
                                        entitySection.LcDepartment = entitySec.LcDepartment;
                                        entitySection.ProdArrayApprovalField = entitySec.ProdArrayApprovalField;
                                        entitySection.RdCtrlNo = entitySec.RdCtrlNo;
                                        entitySection.RdDeliveryConfirmationDate = entitySec.RdDeliveryConfirmationDate;
                                        entitySection.RdItemName = entitySec.RdItemName;
                                        entitySection.RdPONO = entitySec.RdPONO;
                                        entitySection.RdPRNO = entitySec.RdPRNO;
                                        entitySection.RdQuantity = entitySec.RdQuantity;
                                        entitySection.RdRefId = entitySec.RdRefId;
                                        entitySection.RdReplyDeliveryDate = entitySec.RdReplyDeliveryDate;
                                        entitySection.RdRequestedDeliveryDate = entitySec.RdRequestedDeliveryDate;
                                        entitySection.RdSpecs = entitySec.RdSpecs;
                                        entitySection.RdUnitOfMeasure = entitySec.RdUnitOfMeasure;
                                        entitySection.RhAttachment1 = entitySec.RhAttachment1;
                                        entitySection.RhAttachment2 = entitySec.RhAttachment2;
                                        entitySection.RhAttention = entitySec.RhAttention;
                                        entitySection.RhCategory = entitySec.RhCategory;
                                        entitySection.RhCategoryName = entitySec.RhCategoryName;
                                        entitySection.RhCtrlNo = entitySec.RhCtrlNo;
                                        entitySection.RhOtherReason = entitySec.RhOtherReason;
                                        entitySection.RhPurchasingRemarks = entitySec.RhPurchasingRemarks;
                                        entitySection.RhReason = entitySec.RhReason;
                                        entitySection.RhRequester = entitySec.RhRequester;
                                        entitySection.RhSendReceived = entitySec.RhSendReceived;
                                        entitySection.RhStockLifeAttachment = entitySec.RhStockLifeAttachment;
                                        entitySection.RhSupplier = entitySec.RhSupplier;
                                        entitySection.RhSupplierComments = entitySec.RhSupplierComments;
                                        entitySection.RhSupplierEmail = entitySec.RhSupplierEmail;
                                        entitySection.RhSupplierId = entitySec.RhSupplierId;
                                        entitySection.RhTransactionDate = entitySec.RhTransactionDate;
                                        entitySection.RhType = entitySec.RhType;
                                        entitySection.RhUpdatedBy = entitySec.RhUpdatedBy;
                                        entitySection.RhUpdatedDate = entitySec.RhUpdatedDate;
                                        entitySection.RowNumber = entitySec.RowNumber;
                                        entitySection.StatAll = entitySec.StatAll;
                                        entitySection.StatClosed = entitySec.StatClosed;
                                        entitySection.StatCtrlNo = entitySec.StatCtrlNo;
                                        entitySection.StatDOAProdDeptManager = entitySec.StatDOAProdDeptManager;
                                        entitySection.StatDOAProdDivManager = entitySec.StatDOAProdDivManager;
                                        entitySection.StatDOAProdHQManager = entitySec.StatDOAProdHQManager;
                                        entitySection.StatDOAProdSecManager = entitySec.StatDOAProdSecManager;
                                        entitySection.StatDOAPurchasingBuyer = entitySec.StatDOAPurchasingBuyer;
                                        entitySection.StatDOAPurchasingManager = entitySec.StatDOAPurchasingManager;
                                        entitySection.StatProdDeptManager = entitySec.StatProdDeptManager;
                                        entitySection.StatProdDivManager = entitySec.StatProdDivManager;
                                        entitySection.StatProdHQManager = entitySec.StatProdHQManager;
                                        entitySection.StatProdSecManager = entitySec.StatProdSecManager;
                                        entitySection.StatPurchasingBuyer = entitySec.StatPurchasingBuyer;
                                        entitySection.StatPurchasingManager = entitySec.StatPurchasingManager;
                                        entitySection.StatRemarks = entitySec.StatRemarks;
                                        entitySection.StatReOpenRemarks = entitySec.StatReOpenRemarks;
                                        entitySection.StatSTATProdDeptManager = entitySec.StatSTATProdDeptManager;
                                        entitySection.StatSTATProdDivManager = entitySec.StatSTATProdDivManager;
                                        entitySection.StatSTATProdHQManager = entitySec.StatSTATProdHQManager;
                                        entitySection.StatSTATProdSecManager = entitySec.StatSTATProdSecManager;
                                        entitySection.StatSTATPurchasingBuyer = entitySec.StatSTATPurchasingBuyer;
                                        entitySection.StatSTATPurchasingManager = entitySec.StatSTATPurchasingManager;
                                        entitySection.SupplierAttachment = entitySec.SupplierAttachment;
                                        entitySection.TableName = entitySec.TableName;
                                        entitySection.RhRequesterEmail = entitySec.RhRequesterEmail;
                                        list.Add(entitySection);
                                    }
                                }
                            }
                            //DEPARTMENT MANAGER
                            if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), "402").ToString().ToLower() == "true")
                            {
                                if (list == null || list.Count <= 0)
                                {
                                    list = new List<Entities_URF_RequestEntry>();
                                }
                                entity.ProdArrayApprovalField = "STATProdDeptManager = 0 AND (STATProdSecManager = 1 OR STATProdSecManager = -1) AND STATProdSecManager != 2";
                                //listDepartmentManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim() && itm.LcDepartment == Session["Department"].ToString()).ToList();


                                if (ddStatus.SelectedValue == "ALL")
                                {
                                    listDepartmentManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim() && itm.LcDepartment == Session["Department"].ToString()).ToList();
                                }
                                else
                                {
                                    listDepartmentManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim() && itm.LcDepartment == Session["Department"].ToString() && itm.StatAll == ddStatus.SelectedValue).ToList();
                                }

                                if (listDepartmentManager != null || listDepartmentManager.Count > 0)
                                {
                                    foreach (Entities_URF_RequestEntry entityDept in listDepartmentManager)
                                    {
                                        Entities_URF_RequestEntry entityDepartment = new Entities_URF_RequestEntry();
                                        entityDepartment.Criteria = entityDept.Criteria;
                                        entityDepartment.CssColorCode = entityDept.CssColorCode;
                                        entityDepartment.DataIndex = entityDept.DataIndex;
                                        entityDepartment.DrFrom = entityDept.DrFrom;
                                        entityDepartment.DropdownName = entityDept.DropdownName;
                                        entityDepartment.DropdownRefId = entityDept.DropdownRefId;
                                        entityDepartment.IsDisabled = entityDept.IsDisabled;
                                        entityDepartment.LcDepartment = entityDept.LcDepartment;
                                        entityDepartment.ProdArrayApprovalField = entityDept.ProdArrayApprovalField;
                                        entityDepartment.RdCtrlNo = entityDept.RdCtrlNo;
                                        entityDepartment.RdDeliveryConfirmationDate = entityDept.RdDeliveryConfirmationDate;
                                        entityDepartment.RdItemName = entityDept.RdItemName;
                                        entityDepartment.RdPONO = entityDept.RdPONO;
                                        entityDepartment.RdPRNO = entityDept.RdPRNO;
                                        entityDepartment.RdQuantity = entityDept.RdQuantity;
                                        entityDepartment.RdRefId = entityDept.RdRefId;
                                        entityDepartment.RdReplyDeliveryDate = entityDept.RdReplyDeliveryDate;
                                        entityDepartment.RdRequestedDeliveryDate = entityDept.RdRequestedDeliveryDate;
                                        entityDepartment.RdSpecs = entityDept.RdSpecs;
                                        entityDepartment.RdUnitOfMeasure = entityDept.RdUnitOfMeasure;
                                        entityDepartment.RhAttachment1 = entityDept.RhAttachment1;
                                        entityDepartment.RhAttachment2 = entityDept.RhAttachment2;
                                        entityDepartment.RhAttention = entityDept.RhAttention;
                                        entityDepartment.RhCategory = entityDept.RhCategory;
                                        entityDepartment.RhCategoryName = entityDept.RhCategoryName;
                                        entityDepartment.RhCtrlNo = entityDept.RhCtrlNo;
                                        entityDepartment.RhOtherReason = entityDept.RhOtherReason;
                                        entityDepartment.RhPurchasingRemarks = entityDept.RhPurchasingRemarks;
                                        entityDepartment.RhReason = entityDept.RhReason;
                                        entityDepartment.RhRequester = entityDept.RhRequester;
                                        entityDepartment.RhSendReceived = entityDept.RhSendReceived;
                                        entityDepartment.RhStockLifeAttachment = entityDept.RhStockLifeAttachment;
                                        entityDepartment.RhSupplier = entityDept.RhSupplier;
                                        entityDepartment.RhSupplierComments = entityDept.RhSupplierComments;
                                        entityDepartment.RhSupplierEmail = entityDept.RhSupplierEmail;
                                        entityDepartment.RhSupplierId = entityDept.RhSupplierId;
                                        entityDepartment.RhTransactionDate = entityDept.RhTransactionDate;
                                        entityDepartment.RhType = entityDept.RhType;
                                        entityDepartment.RhUpdatedBy = entityDept.RhUpdatedBy;
                                        entityDepartment.RhUpdatedDate = entityDept.RhUpdatedDate;
                                        entityDepartment.RowNumber = entityDept.RowNumber;
                                        entityDepartment.StatAll = entityDept.StatAll;
                                        entityDepartment.StatClosed = entityDept.StatClosed;
                                        entityDepartment.StatCtrlNo = entityDept.StatCtrlNo;
                                        entityDepartment.StatDOAProdDeptManager = entityDept.StatDOAProdDeptManager;
                                        entityDepartment.StatDOAProdDivManager = entityDept.StatDOAProdDivManager;
                                        entityDepartment.StatDOAProdHQManager = entityDept.StatDOAProdHQManager;
                                        entityDepartment.StatDOAProdSecManager = entityDept.StatDOAProdSecManager;
                                        entityDepartment.StatDOAPurchasingBuyer = entityDept.StatDOAPurchasingBuyer;
                                        entityDepartment.StatDOAPurchasingManager = entityDept.StatDOAPurchasingManager;
                                        entityDepartment.StatProdDeptManager = entityDept.StatProdDeptManager;
                                        entityDepartment.StatProdDivManager = entityDept.StatProdDivManager;
                                        entityDepartment.StatProdHQManager = entityDept.StatProdHQManager;
                                        entityDepartment.StatProdSecManager = entityDept.StatProdSecManager;
                                        entityDepartment.StatPurchasingBuyer = entityDept.StatPurchasingBuyer;
                                        entityDepartment.StatPurchasingManager = entityDept.StatPurchasingManager;
                                        entityDepartment.StatRemarks = entityDept.StatRemarks;
                                        entityDepartment.StatReOpenRemarks = entityDept.StatReOpenRemarks;
                                        entityDepartment.StatSTATProdDeptManager = entityDept.StatSTATProdDeptManager;
                                        entityDepartment.StatSTATProdDivManager = entityDept.StatSTATProdDivManager;
                                        entityDepartment.StatSTATProdHQManager = entityDept.StatSTATProdHQManager;
                                        entityDepartment.StatSTATProdSecManager = entityDept.StatSTATProdSecManager;
                                        entityDepartment.StatSTATPurchasingBuyer = entityDept.StatSTATPurchasingBuyer;
                                        entityDepartment.StatSTATPurchasingManager = entityDept.StatSTATPurchasingManager;
                                        entityDepartment.SupplierAttachment = entityDept.SupplierAttachment;
                                        entityDepartment.TableName = entityDept.TableName;
                                        entityDepartment.RhRequesterEmail = entityDept.RhRequesterEmail;
                                        list.Add(entityDepartment);
                                    }
                                }
                            }
                            // DIVISION MANAGER
                            if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), "403").ToString().ToLower() == "true")
                            {
                                if (list == null || list.Count <= 0)
                                {
                                    list = new List<Entities_URF_RequestEntry>();
                                }
                                entity.ProdArrayApprovalField = "STATProdDivManager = 0 AND (STATProdDeptManager = 1 OR STATProdDeptManager = -1) AND STATProdSecManager != 2";
                                //listDivisionManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim() && itm.LcDivision == Session["Division"].ToString()).ToList();

                                if (ddStatus.SelectedValue == "ALL")
                                {
                                    listDivisionManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim() && itm.LcDivision == Session["Division"].ToString()).ToList();
                                }
                                else
                                {
                                    listDivisionManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim() && itm.LcDivision == Session["Division"].ToString() && itm.StatAll == ddStatus.SelectedValue).ToList();
                                }

                                if (listDivisionManager != null || listDivisionManager.Count > 0)
                                {
                                    foreach (Entities_URF_RequestEntry entityDiv in listDivisionManager)
                                    {
                                        Entities_URF_RequestEntry entityDivision = new Entities_URF_RequestEntry();
                                        entityDivision.Criteria = entityDiv.Criteria;
                                        entityDivision.CssColorCode = entityDiv.CssColorCode;
                                        entityDivision.DataIndex = entityDiv.DataIndex;
                                        entityDivision.DrFrom = entityDiv.DrFrom;
                                        entityDivision.DropdownName = entityDiv.DropdownName;
                                        entityDivision.DropdownRefId = entityDiv.DropdownRefId;
                                        entityDivision.IsDisabled = entityDiv.IsDisabled;
                                        entityDivision.LcDepartment = entityDiv.LcDepartment;
                                        entityDivision.ProdArrayApprovalField = entityDiv.ProdArrayApprovalField;
                                        entityDivision.RdCtrlNo = entityDiv.RdCtrlNo;
                                        entityDivision.RdDeliveryConfirmationDate = entityDiv.RdDeliveryConfirmationDate;
                                        entityDivision.RdItemName = entityDiv.RdItemName;
                                        entityDivision.RdPONO = entityDiv.RdPONO;
                                        entityDivision.RdPRNO = entityDiv.RdPRNO;
                                        entityDivision.RdQuantity = entityDiv.RdQuantity;
                                        entityDivision.RdRefId = entityDiv.RdRefId;
                                        entityDivision.RdReplyDeliveryDate = entityDiv.RdReplyDeliveryDate;
                                        entityDivision.RdRequestedDeliveryDate = entityDiv.RdRequestedDeliveryDate;
                                        entityDivision.RdSpecs = entityDiv.RdSpecs;
                                        entityDivision.RdUnitOfMeasure = entityDiv.RdUnitOfMeasure;
                                        entityDivision.RhAttachment1 = entityDiv.RhAttachment1;
                                        entityDivision.RhAttachment2 = entityDiv.RhAttachment2;
                                        entityDivision.RhAttention = entityDiv.RhAttention;
                                        entityDivision.RhCategory = entityDiv.RhCategory;
                                        entityDivision.RhCategoryName = entityDiv.RhCategoryName;
                                        entityDivision.RhCtrlNo = entityDiv.RhCtrlNo;
                                        entityDivision.RhOtherReason = entityDiv.RhOtherReason;
                                        entityDivision.RhPurchasingRemarks = entityDiv.RhPurchasingRemarks;
                                        entityDivision.RhReason = entityDiv.RhReason;
                                        entityDivision.RhRequester = entityDiv.RhRequester;
                                        entityDivision.RhSendReceived = entityDiv.RhSendReceived;
                                        entityDivision.RhStockLifeAttachment = entityDiv.RhStockLifeAttachment;
                                        entityDivision.RhSupplier = entityDiv.RhSupplier;
                                        entityDivision.RhSupplierComments = entityDiv.RhSupplierComments;
                                        entityDivision.RhSupplierEmail = entityDiv.RhSupplierEmail;
                                        entityDivision.RhSupplierId = entityDiv.RhSupplierId;
                                        entityDivision.RhTransactionDate = entityDiv.RhTransactionDate;
                                        entityDivision.RhType = entityDiv.RhType;
                                        entityDivision.RhUpdatedBy = entityDiv.RhUpdatedBy;
                                        entityDivision.RhUpdatedDate = entityDiv.RhUpdatedDate;
                                        entityDivision.RowNumber = entityDiv.RowNumber;
                                        entityDivision.StatAll = entityDiv.StatAll;
                                        entityDivision.StatClosed = entityDiv.StatClosed;
                                        entityDivision.StatCtrlNo = entityDiv.StatCtrlNo;
                                        entityDivision.StatDOAProdDeptManager = entityDiv.StatDOAProdDeptManager;
                                        entityDivision.StatDOAProdDivManager = entityDiv.StatDOAProdDivManager;
                                        entityDivision.StatDOAProdHQManager = entityDiv.StatDOAProdHQManager;
                                        entityDivision.StatDOAProdSecManager = entityDiv.StatDOAProdSecManager;
                                        entityDivision.StatDOAPurchasingBuyer = entityDiv.StatDOAPurchasingBuyer;
                                        entityDivision.StatDOAPurchasingManager = entityDiv.StatDOAPurchasingManager;
                                        entityDivision.StatProdDeptManager = entityDiv.StatProdDeptManager;
                                        entityDivision.StatProdDivManager = entityDiv.StatProdDivManager;
                                        entityDivision.StatProdHQManager = entityDiv.StatProdHQManager;
                                        entityDivision.StatProdSecManager = entityDiv.StatProdSecManager;
                                        entityDivision.StatPurchasingBuyer = entityDiv.StatPurchasingBuyer;
                                        entityDivision.StatPurchasingManager = entityDiv.StatPurchasingManager;
                                        entityDivision.StatRemarks = entityDiv.StatRemarks;
                                        entityDivision.StatReOpenRemarks = entityDiv.StatReOpenRemarks;
                                        entityDivision.StatSTATProdDeptManager = entityDiv.StatSTATProdDeptManager;
                                        entityDivision.StatSTATProdDivManager = entityDiv.StatSTATProdDivManager;
                                        entityDivision.StatSTATProdHQManager = entityDiv.StatSTATProdHQManager;
                                        entityDivision.StatSTATProdSecManager = entityDiv.StatSTATProdSecManager;
                                        entityDivision.StatSTATPurchasingBuyer = entityDiv.StatSTATPurchasingBuyer;
                                        entityDivision.StatSTATPurchasingManager = entityDiv.StatSTATPurchasingManager;
                                        entityDivision.SupplierAttachment = entityDiv.SupplierAttachment;
                                        entityDivision.TableName = entityDiv.TableName;
                                        entityDivision.RhRequesterEmail = entityDiv.RhRequesterEmail;
                                        list.Add(entityDivision);
                                    }
                                }
                            }

                            // HQ MANAGER
                            if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), "404").ToString().ToLower() == "true")
                            {
                                if (list == null || list.Count <= 0)
                                {
                                    list = new List<Entities_URF_RequestEntry>();
                                }
                                entity.ProdArrayApprovalField = "STATProdHQManager = 0 AND (STATProdDivManager = 1 OR STATProdDivManager = -1) AND STATProdSecManager != 2";
                                //listHQManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim() && itm.LcHQ == Session["HQ"].ToString()).ToList();

                                if (ddStatus.SelectedValue == "ALL")
                                {
                                    listHQManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim() && itm.LcHQ == Session["HQ"].ToString()).ToList();
                                }
                                else
                                {
                                    listHQManager = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim() && itm.LcHQ == Session["HQ"].ToString() && itm.StatAll == ddStatus.SelectedValue).ToList();
                                }

                                if (listHQManager != null || listHQManager.Count > 0)
                                {
                                    foreach (Entities_URF_RequestEntry entityH in listHQManager)
                                    {
                                        Entities_URF_RequestEntry entityHQ = new Entities_URF_RequestEntry();
                                        entityHQ.Criteria = entityH.Criteria;
                                        entityHQ.CssColorCode = entityH.CssColorCode;
                                        entityHQ.DataIndex = entityH.DataIndex;
                                        entityHQ.DrFrom = entityH.DrFrom;
                                        entityHQ.DropdownName = entityH.DropdownName;
                                        entityHQ.DropdownRefId = entityH.DropdownRefId;
                                        entityHQ.IsDisabled = entityH.IsDisabled;
                                        entityHQ.LcDepartment = entityH.LcDepartment;
                                        entityHQ.ProdArrayApprovalField = entityH.ProdArrayApprovalField;
                                        entityHQ.RdCtrlNo = entityH.RdCtrlNo;
                                        entityHQ.RdDeliveryConfirmationDate = entityH.RdDeliveryConfirmationDate;
                                        entityHQ.RdItemName = entityH.RdItemName;
                                        entityHQ.RdPONO = entityH.RdPONO;
                                        entityHQ.RdPRNO = entityH.RdPRNO;
                                        entityHQ.RdQuantity = entityH.RdQuantity;
                                        entityHQ.RdRefId = entityH.RdRefId;
                                        entityHQ.RdReplyDeliveryDate = entityH.RdReplyDeliveryDate;
                                        entityHQ.RdRequestedDeliveryDate = entityH.RdRequestedDeliveryDate;
                                        entityHQ.RdSpecs = entityH.RdSpecs;
                                        entityHQ.RdUnitOfMeasure = entityH.RdUnitOfMeasure;
                                        entityHQ.RhAttachment1 = entityH.RhAttachment1;
                                        entityHQ.RhAttachment2 = entityH.RhAttachment2;
                                        entityHQ.RhAttention = entityH.RhAttention;
                                        entityHQ.RhCategory = entityH.RhCategory;
                                        entityHQ.RhCategoryName = entityH.RhCategoryName;
                                        entityHQ.RhCtrlNo = entityH.RhCtrlNo;
                                        entityHQ.RhOtherReason = entityH.RhOtherReason;
                                        entityHQ.RhPurchasingRemarks = entityH.RhPurchasingRemarks;
                                        entityHQ.RhReason = entityH.RhReason;
                                        entityHQ.RhRequester = entityH.RhRequester;
                                        entityHQ.RhSendReceived = entityH.RhSendReceived;
                                        entityHQ.RhStockLifeAttachment = entityH.RhStockLifeAttachment;
                                        entityHQ.RhSupplier = entityH.RhSupplier;
                                        entityHQ.RhSupplierComments = entityH.RhSupplierComments;
                                        entityHQ.RhSupplierEmail = entityH.RhSupplierEmail;
                                        entityHQ.RhSupplierId = entityH.RhSupplierId;
                                        entityHQ.RhTransactionDate = entityH.RhTransactionDate;
                                        entityHQ.RhType = entityH.RhType;
                                        entityHQ.RhUpdatedBy = entityH.RhUpdatedBy;
                                        entityHQ.RhUpdatedDate = entityH.RhUpdatedDate;
                                        entityHQ.RowNumber = entityH.RowNumber;
                                        entityHQ.StatAll = entityH.StatAll;
                                        entityHQ.StatClosed = entityH.StatClosed;
                                        entityHQ.StatCtrlNo = entityH.StatCtrlNo;
                                        entityHQ.StatDOAProdDeptManager = entityH.StatDOAProdDeptManager;
                                        entityHQ.StatDOAProdDivManager = entityH.StatDOAProdDivManager;
                                        entityHQ.StatDOAProdHQManager = entityH.StatDOAProdHQManager;
                                        entityHQ.StatDOAProdSecManager = entityH.StatDOAProdSecManager;
                                        entityHQ.StatDOAPurchasingBuyer = entityH.StatDOAPurchasingBuyer;
                                        entityHQ.StatDOAPurchasingManager = entityH.StatDOAPurchasingManager;
                                        entityHQ.StatProdDeptManager = entityH.StatProdDeptManager;
                                        entityHQ.StatProdDivManager = entityH.StatProdDivManager;
                                        entityHQ.StatProdHQManager = entityH.StatProdHQManager;
                                        entityHQ.StatProdSecManager = entityH.StatProdSecManager;
                                        entityHQ.StatPurchasingBuyer = entityH.StatPurchasingBuyer;
                                        entityHQ.StatPurchasingManager = entityH.StatPurchasingManager;
                                        entityHQ.StatRemarks = entityH.StatRemarks;
                                        entityHQ.StatReOpenRemarks = entityH.StatReOpenRemarks;
                                        entityHQ.StatSTATProdDeptManager = entityH.StatSTATProdDeptManager;
                                        entityHQ.StatSTATProdDivManager = entityH.StatSTATProdDivManager;
                                        entityHQ.StatSTATProdHQManager = entityH.StatSTATProdHQManager;
                                        entityHQ.StatSTATProdSecManager = entityH.StatSTATProdSecManager;
                                        entityHQ.StatSTATPurchasingBuyer = entityH.StatSTATPurchasingBuyer;
                                        entityHQ.StatSTATPurchasingManager = entityH.StatSTATPurchasingManager;
                                        entityHQ.SupplierAttachment = entityH.SupplierAttachment;
                                        entityHQ.TableName = entityH.TableName;
                                        entityHQ.RhRequesterEmail = entityH.RhRequesterEmail;
                                        list.Add(entityHQ);
                                    }
                                }
                            }

                            //// HQ MANAGER
                            //if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), "404").ToString() == "true")
                            //{
                            //    list = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim() && itm.LcHQ == Session["HQ"].ToString()).ToList();
                            //}
                            //else if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), "403").ToString() == "true") // DIVISION Manager
                            //{
                            //    list = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim() && itm.LcDivision == Session["Division"].ToString()).ToList();
                            //}
                            //else
                            //{
                            //    list = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim() && itm.LcDepartment == Session["Department"].ToString()).ToList();
                            //}
                            //list = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim() && itm.LcDepartment == Session["Department"].ToString()).ToList();
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
                        }
                    }

                }

                // FOR PURCHASING APPROVAL
                if (isPurchasing)
                {

                    if (!string.IsNullOrEmpty(txtURFNo.Text) || txtURFNo.Text.Length > 0)
                    {
                        Session["Search_From_URF_Inquiry"] = txtURFNo.Text;
                        Response.Redirect("URF_AllRequest.aspx");
                    }
                    else
                    {
                        if (Session["URF_Prod_SecManager"].ToString().ToLower() == "true")
                        {
                            prodSecManager.ProdArrayApprovalField = "STATProdSecManager = 0 AND STATProdSecManager != 2";
                            listField.Add(prodSecManager);
                        }
                        if (Session["URF_Prod_DeptManager"].ToString().ToLower() == "true")
                        {
                            prodDeptManager.ProdArrayApprovalField = "STATProdDeptManager = 0 AND (STATProdSecManager = 1 OR STATProdSecManager = -1)";
                            listField.Add(prodDeptManager);
                        }
                        if (Session["URF_Prod_DivManager"].ToString().ToLower() == "true")
                        {
                            prodDivManager.ProdArrayApprovalField = "STATProdDivManager = 0 AND (STATProdDeptManager = 1 OR STATProdDeptManager = -1) AND STATProdSecManager != 2";
                            listField.Add(prodDivManager);
                        }
                        if (Session["URF_Prod_HQManager"].ToString().ToLower() == "true")
                        {
                            prodHQManager.ProdArrayApprovalField = "STATProdHQManager = 0 AND (STATProdDivManager = 1 OR STATProdDivManager = -1) AND STATProdSecManager != 2";
                            listField.Add(prodHQManager);
                        }

                        foreach (Entities_URF_RequestEntry field in listField)
                        {
                            whereClause += field.ProdArrayApprovalField + " OR ";
                        }

                        if (listField.Count > 0)
                        {
                            entity.ProdArrayApprovalField = whereClause.Remove((whereClause.Length - 4), 4).ToString();
                        }

                        List<Entities_URF_RequestEntry> listPuchasingRequest = new List<Entities_URF_RequestEntry>();
                        //listPuchasingRequest = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.LcDepartment == Session["Department"].ToString()).ToList();

                        if (ddStatus.SelectedValue == "ALL")
                        {
                            listPuchasingRequest = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.LcDepartment == Session["Department"].ToString()).ToList();
                        }
                        else
                        {
                            listPuchasingRequest = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.LcDepartment == Session["Department"].ToString() && itm.StatAll == ddStatus.SelectedValue).ToList();
                        }

                        if (listPuchasingRequest != null || listPuchasingRequest.Count > 0)
                        {
                            foreach (Entities_URF_RequestEntry entityPR in listPuchasingRequest)
                            {
                                Entities_URF_RequestEntry entityPurchasingRequest = new Entities_URF_RequestEntry();
                                entityPurchasingRequest.Criteria = entityPR.Criteria;
                                entityPurchasingRequest.CssColorCode = entityPR.CssColorCode;
                                entityPurchasingRequest.DataIndex = entityPR.DataIndex;
                                entityPurchasingRequest.DrFrom = entityPR.DrFrom;
                                entityPurchasingRequest.DropdownName = entityPR.DropdownName;
                                entityPurchasingRequest.DropdownRefId = entityPR.DropdownRefId;
                                entityPurchasingRequest.IsDisabled = entityPR.IsDisabled;
                                entityPurchasingRequest.LcDepartment = entityPR.LcDepartment;
                                entityPurchasingRequest.ProdArrayApprovalField = entityPR.ProdArrayApprovalField;
                                entityPurchasingRequest.RdCtrlNo = entityPR.RdCtrlNo;
                                entityPurchasingRequest.RdDeliveryConfirmationDate = entityPR.RdDeliveryConfirmationDate;
                                entityPurchasingRequest.RdItemName = entityPR.RdItemName;
                                entityPurchasingRequest.RdPONO = entityPR.RdPONO;
                                entityPurchasingRequest.RdPRNO = entityPR.RdPRNO;
                                entityPurchasingRequest.RdQuantity = entityPR.RdQuantity;
                                entityPurchasingRequest.RdRefId = entityPR.RdRefId;
                                entityPurchasingRequest.RdReplyDeliveryDate = entityPR.RdReplyDeliveryDate;
                                entityPurchasingRequest.RdRequestedDeliveryDate = entityPR.RdRequestedDeliveryDate;
                                entityPurchasingRequest.RdSpecs = entityPR.RdSpecs;
                                entityPurchasingRequest.RdUnitOfMeasure = entityPR.RdUnitOfMeasure;
                                entityPurchasingRequest.RhAttachment1 = entityPR.RhAttachment1;
                                entityPurchasingRequest.RhAttachment2 = entityPR.RhAttachment2;
                                entityPurchasingRequest.RhAttention = entityPR.RhAttention;
                                entityPurchasingRequest.RhCategory = entityPR.RhCategory;
                                entityPurchasingRequest.RhCategoryName = entityPR.RhCategoryName;
                                entityPurchasingRequest.RhCtrlNo = entityPR.RhCtrlNo;
                                entityPurchasingRequest.RhOtherReason = entityPR.RhOtherReason;
                                entityPurchasingRequest.RhPurchasingRemarks = entityPR.RhPurchasingRemarks;
                                entityPurchasingRequest.RhReason = entityPR.RhReason;
                                entityPurchasingRequest.RhRequester = entityPR.RhRequester;
                                entityPurchasingRequest.RhSendReceived = entityPR.RhSendReceived;
                                entityPurchasingRequest.RhStockLifeAttachment = entityPR.RhStockLifeAttachment;
                                entityPurchasingRequest.RhSupplier = entityPR.RhSupplier;
                                entityPurchasingRequest.RhSupplierComments = entityPR.RhSupplierComments;
                                entityPurchasingRequest.RhSupplierEmail = entityPR.RhSupplierEmail;
                                entityPurchasingRequest.RhSupplierId = entityPR.RhSupplierId;
                                entityPurchasingRequest.RhTransactionDate = entityPR.RhTransactionDate;
                                entityPurchasingRequest.RhType = entityPR.RhType;
                                entityPurchasingRequest.RhUpdatedBy = entityPR.RhUpdatedBy;
                                entityPurchasingRequest.RhUpdatedDate = entityPR.RhUpdatedDate;
                                entityPurchasingRequest.RowNumber = entityPR.RowNumber;
                                entityPurchasingRequest.StatAll = entityPR.StatAll;
                                entityPurchasingRequest.StatClosed = entityPR.StatClosed;
                                entityPurchasingRequest.StatCtrlNo = entityPR.StatCtrlNo;
                                entityPurchasingRequest.StatDOAProdDeptManager = entityPR.StatDOAProdDeptManager;
                                entityPurchasingRequest.StatDOAProdDivManager = entityPR.StatDOAProdDivManager;
                                entityPurchasingRequest.StatDOAProdHQManager = entityPR.StatDOAProdHQManager;
                                entityPurchasingRequest.StatDOAProdSecManager = entityPR.StatDOAProdSecManager;
                                entityPurchasingRequest.StatDOAPurchasingBuyer = entityPR.StatDOAPurchasingBuyer;
                                entityPurchasingRequest.StatDOAPurchasingManager = entityPR.StatDOAPurchasingManager;
                                entityPurchasingRequest.StatProdDeptManager = entityPR.StatProdDeptManager;
                                entityPurchasingRequest.StatProdDivManager = entityPR.StatProdDivManager;
                                entityPurchasingRequest.StatProdHQManager = entityPR.StatProdHQManager;
                                entityPurchasingRequest.StatProdSecManager = entityPR.StatProdSecManager;
                                entityPurchasingRequest.StatPurchasingBuyer = entityPR.StatPurchasingBuyer;
                                entityPurchasingRequest.StatPurchasingManager = entityPR.StatPurchasingManager;
                                entityPurchasingRequest.StatRemarks = entityPR.StatRemarks;
                                entityPurchasingRequest.StatReOpenRemarks = entityPR.StatReOpenRemarks;
                                entityPurchasingRequest.StatSTATProdDeptManager = entityPR.StatSTATProdDeptManager;
                                entityPurchasingRequest.StatSTATProdDivManager = entityPR.StatSTATProdDivManager;
                                entityPurchasingRequest.StatSTATProdHQManager = entityPR.StatSTATProdHQManager;
                                entityPurchasingRequest.StatSTATProdSecManager = entityPR.StatSTATProdSecManager;
                                entityPurchasingRequest.StatSTATPurchasingBuyer = entityPR.StatSTATPurchasingBuyer;
                                entityPurchasingRequest.StatSTATPurchasingManager = entityPR.StatSTATPurchasingManager;
                                entityPurchasingRequest.SupplierAttachment = entityPR.SupplierAttachment;
                                entityPurchasingRequest.TableName = entityPR.TableName;
                                listFinalPurchasing.Add(entityPurchasingRequest);
                            }
                        }

                        if (ddCategory.SelectedItem.Text.ToLower() == "")
                        {
                            if (username == "6985" || username == "3844" || username == "1152" || username == "1402" || username == "002" || username == "0286")
                            {
                                entity.ProdArrayApprovalField = "STATPurchasingBuyer = 1 AND STATPurchasingManager = 0 AND STATProdSecManager != 2";
                                //list = BLL.URF_TRANSACTION_Approval_DateRange(entity);

                                if (ddStatus.SelectedValue == "ALL")
                                {
                                    list = BLL.URF_TRANSACTION_Approval_DateRange(entity);
                                }
                                else
                                {
                                    list = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.StatAll == ddStatus.SelectedValue).ToList();
                                }
                            }
                            //if (int.Parse(categoryAccess) > 0)
                            //{
                            //    entity.ProdArrayApprovalField = "(STATProdHQManager = 1 OR STATProdHQManager = -1) AND STATPurchasingBuyer = 0";
                            //    list = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim()).ToList();
                            //}

                        }
                        else
                        {
                            if (int.Parse(categoryAccess) > 0)
                            {
                                entity.ProdArrayApprovalField = "(STATProdHQManager = 1 OR STATProdHQManager = -1) AND STATPurchasingBuyer = 0 AND STATProdSecManager != 2";
                                //list = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim()).ToList();

                                if (ddStatus.SelectedValue == "ALL")
                                {
                                    list = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim() && int.Parse(itm.EmptyPO) <= 0).ToList();
                                }
                                else
                                {
                                    list = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedItem.Value.Trim() && itm.StatAll == ddStatus.SelectedValue && int.Parse(itm.EmptyPO) <= 0).ToList();
                                }
                            }
                            if (username == "6985" || username == "3844" || username == "1152" || username == "1402" || username == "002" || username == "0286")
                            {
                                //special for 0286 and 6985 because she is using one account
                                if (username == "0286" || username == "6985")
                                {
                                    entity.ProdArrayApprovalField = "(STATProdHQManager = 1 OR STATProdHQManager = -1) AND (STATPurchasingBuyer = 1 OR STATPurchasingBuyer = 0) AND STATPurchasingManager = 0 AND STATProdSecManager != 2";
                                    //list = BLL.URF_TRANSACTION_Approval_DateRange(entity);
                                }
                                else
                                {
                                    entity.ProdArrayApprovalField = "STATPurchasingBuyer = 1 AND STATPurchasingManager = 0 AND STATProdSecManager != 2";
                                    //list = BLL.URF_TRANSACTION_Approval_DateRange(entity);
                                }

                                if (ddCategory.SelectedItem.Text.ToLower() == "")
                                {
                                    if (ddStatus.SelectedValue == "ALL")
                                    {
                                        list = BLL.URF_TRANSACTION_Approval_DateRange(entity);
                                    }
                                    else
                                    {
                                        list = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.StatAll == ddStatus.SelectedValue).ToList();
                                    }
                                }
                                else
                                {
                                    if (ddStatus.SelectedValue == "ALL")
                                    {
                                        list = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.RhCategory == ddCategory.SelectedValue.Trim()).ToList();
                                    }
                                    else
                                    {
                                        list = BLL.URF_TRANSACTION_Approval_DateRange(entity).Where(itm => itm.StatAll == ddStatus.SelectedValue && itm.RhCategory == ddCategory.SelectedValue.Trim()).ToList();
                                    }
                                }

                            }

                        }

                        if (list != null)
                        {
                            if (list.Count > 0)
                            {
                                foreach (Entities_URF_RequestEntry entityFinalList in list)
                                {
                                    Entities_URF_RequestEntry entityPurchasingRequest2 = new Entities_URF_RequestEntry();
                                    entityPurchasingRequest2.Criteria = entityFinalList.Criteria;
                                    entityPurchasingRequest2.CssColorCode = entityFinalList.CssColorCode;
                                    entityPurchasingRequest2.DataIndex = entityFinalList.DataIndex;
                                    entityPurchasingRequest2.DrFrom = entityFinalList.DrFrom;
                                    entityPurchasingRequest2.DropdownName = entityFinalList.DropdownName;
                                    entityPurchasingRequest2.DropdownRefId = entityFinalList.DropdownRefId;
                                    entityPurchasingRequest2.IsDisabled = entityFinalList.IsDisabled;
                                    entityPurchasingRequest2.LcDepartment = entityFinalList.LcDepartment;
                                    entityPurchasingRequest2.ProdArrayApprovalField = entityFinalList.ProdArrayApprovalField;
                                    entityPurchasingRequest2.RdCtrlNo = entityFinalList.RdCtrlNo;
                                    entityPurchasingRequest2.RdDeliveryConfirmationDate = entityFinalList.RdDeliveryConfirmationDate;
                                    entityPurchasingRequest2.RdItemName = entityFinalList.RdItemName;
                                    entityPurchasingRequest2.RdPONO = entityFinalList.RdPONO;
                                    entityPurchasingRequest2.RdPRNO = entityFinalList.RdPRNO;
                                    entityPurchasingRequest2.RdQuantity = entityFinalList.RdQuantity;
                                    entityPurchasingRequest2.RdRefId = entityFinalList.RdRefId;
                                    entityPurchasingRequest2.RdReplyDeliveryDate = entityFinalList.RdReplyDeliveryDate;
                                    entityPurchasingRequest2.RdRequestedDeliveryDate = entityFinalList.RdRequestedDeliveryDate;
                                    entityPurchasingRequest2.RdSpecs = entityFinalList.RdSpecs;
                                    entityPurchasingRequest2.RdUnitOfMeasure = entityFinalList.RdUnitOfMeasure;
                                    entityPurchasingRequest2.RhAttachment1 = entityFinalList.RhAttachment1;
                                    entityPurchasingRequest2.RhAttachment2 = entityFinalList.RhAttachment2;
                                    entityPurchasingRequest2.RhAttention = entityFinalList.RhAttention;
                                    entityPurchasingRequest2.RhCategory = entityFinalList.RhCategory;
                                    entityPurchasingRequest2.RhCategoryName = entityFinalList.RhCategoryName;
                                    entityPurchasingRequest2.RhCtrlNo = entityFinalList.RhCtrlNo;
                                    entityPurchasingRequest2.RhOtherReason = entityFinalList.RhOtherReason;
                                    entityPurchasingRequest2.RhPurchasingRemarks = entityFinalList.RhPurchasingRemarks;
                                    entityPurchasingRequest2.RhReason = entityFinalList.RhReason;
                                    entityPurchasingRequest2.RhRequester = entityFinalList.RhRequester;
                                    entityPurchasingRequest2.RhSendReceived = entityFinalList.RhSendReceived;
                                    entityPurchasingRequest2.RhStockLifeAttachment = entityFinalList.RhStockLifeAttachment;
                                    entityPurchasingRequest2.RhSupplier = entityFinalList.RhSupplier;
                                    entityPurchasingRequest2.RhSupplierComments = entityFinalList.RhSupplierComments;
                                    entityPurchasingRequest2.RhSupplierEmail = entityFinalList.RhSupplierEmail;
                                    entityPurchasingRequest2.RhSupplierId = entityFinalList.RhSupplierId;
                                    entityPurchasingRequest2.RhTransactionDate = entityFinalList.RhTransactionDate;
                                    entityPurchasingRequest2.RhType = entityFinalList.RhType;
                                    entityPurchasingRequest2.RhUpdatedBy = entityFinalList.RhUpdatedBy;
                                    entityPurchasingRequest2.RhUpdatedDate = entityFinalList.RhUpdatedDate;
                                    entityPurchasingRequest2.RowNumber = entityFinalList.RowNumber;
                                    entityPurchasingRequest2.StatAll = entityFinalList.StatAll;
                                    entityPurchasingRequest2.StatClosed = entityFinalList.StatClosed;
                                    entityPurchasingRequest2.StatCtrlNo = entityFinalList.StatCtrlNo;
                                    entityPurchasingRequest2.StatDOAProdDeptManager = entityFinalList.StatDOAProdDeptManager;
                                    entityPurchasingRequest2.StatDOAProdDivManager = entityFinalList.StatDOAProdDivManager;
                                    entityPurchasingRequest2.StatDOAProdHQManager = entityFinalList.StatDOAProdHQManager;
                                    entityPurchasingRequest2.StatDOAProdSecManager = entityFinalList.StatDOAProdSecManager;
                                    entityPurchasingRequest2.StatDOAPurchasingBuyer = entityFinalList.StatDOAPurchasingBuyer;
                                    entityPurchasingRequest2.StatDOAPurchasingManager = entityFinalList.StatDOAPurchasingManager;
                                    entityPurchasingRequest2.StatProdDeptManager = entityFinalList.StatProdDeptManager;
                                    entityPurchasingRequest2.StatProdDivManager = entityFinalList.StatProdDivManager;
                                    entityPurchasingRequest2.StatProdHQManager = entityFinalList.StatProdHQManager;
                                    entityPurchasingRequest2.StatProdSecManager = entityFinalList.StatProdSecManager;
                                    entityPurchasingRequest2.StatPurchasingBuyer = entityFinalList.StatPurchasingBuyer;
                                    entityPurchasingRequest2.StatPurchasingManager = entityFinalList.StatPurchasingManager;
                                    entityPurchasingRequest2.StatRemarks = entityFinalList.StatRemarks;
                                    entityPurchasingRequest2.StatReOpenRemarks = entityFinalList.StatReOpenRemarks;
                                    entityPurchasingRequest2.StatSTATProdDeptManager = entityFinalList.StatSTATProdDeptManager;
                                    entityPurchasingRequest2.StatSTATProdDivManager = entityFinalList.StatSTATProdDivManager;
                                    entityPurchasingRequest2.StatSTATProdHQManager = entityFinalList.StatSTATProdHQManager;
                                    entityPurchasingRequest2.StatSTATProdSecManager = entityFinalList.StatSTATProdSecManager;
                                    entityPurchasingRequest2.StatSTATPurchasingBuyer = entityFinalList.StatSTATPurchasingBuyer;
                                    entityPurchasingRequest2.StatSTATPurchasingManager = entityFinalList.StatSTATPurchasingManager;
                                    entityPurchasingRequest2.SupplierAttachment = entityFinalList.SupplierAttachment;
                                    entityPurchasingRequest2.TableName = entityFinalList.TableName;
                                    listFinalPurchasing.Add(entityPurchasingRequest2);
                                }
                            }
                        }
                    }

                    //list = BLL.URF_TRANSACTION_Approval_DateRange(entity);

                    if (listFinalPurchasing != null)
                    {
                        if (listFinalPurchasing.Count > 0)
                        {
                            gvData.Visible = true;
                            gvData.DataSource = listFinalPurchasing;
                            gvData.DataBind();

                            //EXPORT TO EXCEL
                            List<Entities_URF_RequestEntry> finalListExport = new List<Entities_URF_RequestEntry>();

                            foreach (Entities_URF_RequestEntry entity2 in listFinalPurchasing)
                            {
                                List<Entities_URF_RequestEntry> listExport = new List<Entities_URF_RequestEntry>();
                                listExport = BLL.URF_TRANSACTION_GetRequestDetailsByCTRLNo_ExportToExcel(entity2);

                                if (listExport != null)
                                {
                                    if (listExport.Count > 0)
                                    {
                                        foreach (Entities_URF_RequestEntry le in listExport)
                                        {
                                            Entities_URF_RequestEntry final = new Entities_URF_RequestEntry();
                                            final.RdCtrlNo = le.RdCtrlNo;

                                            final.RhRequester = le.RhRequester;
                                            final.RhTransactionDate = le.RhTransactionDate;
                                            final.LcDepartment = le.LcDepartment;
                                            final.LcDivision = le.LcDivision;
                                            final.RdPONO = le.RdPONO;
                                            final.RdPRNO = le.RdPRNO;
                                            final.RdItemName = le.RdItemName;
                                            final.RdSpecs = le.RdSpecs;
                                            final.RdQuantity = le.RdQuantity;
                                            final.RdUnitOfMeasure = le.RdUnitOfMeasure;
                                            final.RdUOMDesc = le.RdUOMDesc;
                                            final.RdDeliveryConfirmationDate = le.RdDeliveryConfirmationDate;
                                            final.RdRequestedDeliveryDate = le.RdRequestedDeliveryDate;
                                            final.RdReplyDeliveryDate = le.RdReplyDeliveryDate;
                                            final.RhSupplier = le.RhSupplier;
                                            final.RhReason = le.RhReason;
                                            final.RhOtherReason = le.RhOtherReason;
                                            //final.RhType = le.RhType == "1" ? "LOCAL";
                                            if (!string.IsNullOrEmpty(le.RhType))
                                            {
                                                final.RhType = le.RhType == "1" ? "LOCAL" : "OVERSEAS";
                                            }
                                            final.RhAttention = le.RhAttention;

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
                }

                divOpacity.Style.Add("opacity", "1");
                divLoader.Style.Add("display", "none");

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + " - " + ex.StackTrace + "');", true);
            }
        }

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                LinkButton lblView = row.FindControl("lblView") as LinkButton;
                Label lblCTRLNo = row.FindControl("lblCTRLNo") as Label;
                Label lblTransDate = row.FindControl("lblTransDate") as Label;
                Label lblRequester = row.FindControl("lblRequester") as Label;
                Label lblCategory = row.FindControl("lblCategory") as Label;
                Label lblStatAll = row.FindControl("lblStatAll") as Label;
                GridView gvDataDetails = row.FindControl("gvDataDetails") as GridView;
                GridView gvDataStatus = row.FindControl("gvDataStatus") as GridView;
                ImageButton ibApproved = row.FindControl("ibApproved") as ImageButton;
                ImageButton ibDisapproved = row.FindControl("ibDisapproved") as ImageButton;
                TextBox txtRemarks = row.FindControl("txtRemarks") as TextBox;
                Label lblType = row.FindControl("lblType") as Label;
                Label lblSupplier = row.FindControl("lblSupplier") as Label;
                Label lblReason = row.FindControl("lblReason") as Label;
                Label lblAttention = row.FindControl("lblAttention") as Label;
                Label lblAttachment1 = row.FindControl("lblAttachment1") as Label;
                Label lblAttachment2 = row.FindControl("lblAttachment2") as Label;
                Label lblStockLifeAttachment = row.FindControl("lblStockLifeAttachment") as Label;
                LinkButton lbAttachment1 = row.FindControl("lbAttachment1") as LinkButton;
                LinkButton lbAttachment2 = row.FindControl("lbAttachment2") as LinkButton;
                LinkButton lbStockLifeAttachment = row.FindControl("lbStockLifeAttachment") as LinkButton;
                Label lblDisapprovalCause = row.FindControl("lblDisapprovalCause") as Label;


                if (e.CommandName == "lbCTRLNo_Command")
                {
                    Response.Redirect("URF_RequestEntry_New.aspx?URFNo_From_Inquiry=" + CryptorEngine.Encrypt(lblCTRLNo.Text.Trim(), true), false);
                }

                if (e.CommandName == "lblPreview_Command")
                {
                    Response.Redirect("URF_RequestEntry_New.aspx?URFNo_From_Inquiry=" + CryptorEngine.Encrypt(lblCTRLNo.Text.Trim(), true) + "&previewrep=true", false);
                }

                if (e.CommandName == "lblView_Command")
                {
                    if (lblView.Text.ToUpper() == "OPEN DETAILS")
                    {
                        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();
                        Entities_URF_RequestEntry entity = new Entities_URF_RequestEntry();

                        entity.RdCtrlNo = lblCTRLNo.Text.Trim();
                        list = BLL.URF_TRANSACTION_GetRequestDetailsByCTRLNo(entity);

                        if (list != null)
                        {
                            if (list.Count > 0)
                            {

                                gvDataDetails.DataSource = list;
                                gvDataDetails.DataBind();
                                gvDataDetails.Visible = true;

                                List<Entities_URF_RequestEntry> listStatus = new List<Entities_URF_RequestEntry>();

                                int statCounter = 0;
                                foreach (Entities_URF_RequestEntry eStat in list)
                                {
                                    Entities_URF_RequestEntry entityStatus = new Entities_URF_RequestEntry();

                                    // PROD SEC MANAGER
                                    if (eStat.StatSTATProdSecManager == "-1")
                                    {
                                        entityStatus.StatProdSecManager = "AUTO-APPROVED";
                                    }
                                    else
                                    {
                                        entityStatus.StatProdSecManager = eStat.StatProdSecManager;
                                    }
                                    // PROD DEPT MANAGER
                                    if (eStat.StatSTATProdDeptManager == "-1")
                                    {
                                        entityStatus.StatProdDeptManager = "AUTO-APPROVED";
                                    }
                                    else
                                    {
                                        entityStatus.StatProdDeptManager = eStat.StatProdDeptManager;
                                    }
                                    // PROD DIV MANAGER
                                    if (eStat.StatSTATProdDivManager == "-1")
                                    {
                                        entityStatus.StatProdDivManager = "AUTO-APPROVED";
                                    }
                                    else
                                    {
                                        entityStatus.StatProdDivManager = eStat.StatProdDivManager;
                                    }
                                    // PROD HQ MANAGER
                                    if (eStat.StatSTATProdHQManager == "-1")
                                    {
                                        entityStatus.StatProdHQManager = "AUTO-APPROVED";
                                    }
                                    else
                                    {
                                        entityStatus.StatProdHQManager = eStat.StatProdHQManager;
                                    }

                                    if (!string.IsNullOrEmpty(eStat.DisapprovalCause))
                                    {
                                        lblDisapprovalCause.Visible = true;
                                        lblDisapprovalCause.Text += "DISAPPROVAL CAUSE: " + eStat.DisapprovalCause;
                                    }


                                    entityStatus.StatPurchasingBuyer = eStat.StatPurchasingBuyer;
                                    entityStatus.StatPurchasingManager = eStat.StatPurchasingManager;

                                    if (statCounter <= 0)
                                    {
                                        listStatus.Add(entityStatus);

                                        lblAttention.Text = "ATTENTION : " + eStat.RhAttention;

                                        if (string.IsNullOrEmpty(eStat.RhOtherReason))
                                        {
                                            lblReason.Text = "REASON : " + eStat.RhReason;
                                        }
                                        else
                                        {
                                            lblReason.Text = "REASON : " + eStat.RhOtherReason;
                                        }

                                        lblSupplier.Text = "SUPPLIER : " + eStat.RhSupplier;

                                        if (eStat.RhType == "1")
                                        {
                                            lblType.Text = "TYPE : LOCAL";
                                        }
                                        if (eStat.RhType == "2")
                                        {
                                            lblType.Text = "TYPE : OVERSEAS";
                                        }

                                        lblAttention.Visible = true;
                                        lblReason.Visible = true;
                                        lblSupplier.Visible = true;
                                        lblType.Visible = true;

                                        if (!string.IsNullOrEmpty(eStat.RhAttachment1))
                                        {
                                            lblAttachment1.Visible = true;
                                            lbAttachment1.Visible = true;
                                            lbAttachment1.Text = eStat.RhAttachment1;

                                            string URL1 = "~/URF_Request/" + eStat.RdCtrlNo + "/" + eStat.RhAttachment1;

                                            URL1 = Page.ResolveClientUrl(URL1);
                                            lbAttachment1.OnClientClick = "window.open('" + URL1 + "'); return false;";

                                        }
                                        if (!string.IsNullOrEmpty(eStat.RhAttachment2))
                                        {
                                            lblAttachment2.Visible = true;
                                            lbAttachment2.Visible = true;
                                            lbAttachment2.Text = eStat.RhAttachment2;

                                            string URL2 = "~/URF_Request/" + eStat.RdCtrlNo + "/" + eStat.RhAttachment2;

                                            URL2 = Page.ResolveClientUrl(URL2);
                                            lbAttachment2.OnClientClick = "window.open('" + URL2 + "'); return false;";
                                        }
                                        if (!string.IsNullOrEmpty(eStat.RhStockLifeAttachment))
                                        {
                                            lblStockLifeAttachment.Visible = true;
                                            lbStockLifeAttachment.Visible = true;
                                            lbStockLifeAttachment.Text = eStat.RhStockLifeAttachment;

                                            string URL3 = "~/URF_Request/" + eStat.RdCtrlNo + "/" + eStat.RhStockLifeAttachment;

                                            URL3 = Page.ResolveClientUrl(URL3);
                                            lbStockLifeAttachment.OnClientClick = "window.open('" + URL3 + "'); return false;";
                                        }

                                    }
                                    statCounter++;
                                }

                                gvDataStatus.DataSource = listStatus;
                                gvDataStatus.DataBind();
                                gvDataStatus.Visible = true;

                            }
                        }
                        lblView.Text = "CLOSE DETAILS";
                    }
                    else
                    {
                        lblView.Text = "OPEN DETAILS";
                        gvDataDetails.Visible = false;
                        gvDataStatus.Visible = false;
                        lblAttention.Visible = false;
                        lblReason.Visible = false;
                        lblSupplier.Visible = false;
                        lblType.Visible = false;
                        lblAttachment1.Visible = false;
                        lbAttachment1.Visible = false;
                        lblAttachment2.Visible = false;
                        lbAttachment2.Visible = false;
                        lblStockLifeAttachment.Visible = false;
                        lbStockLifeAttachment.Visible = false;
                        lblDisapprovalCause.Visible = false;
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

                            List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();
                            Entities_URF_RequestEntry entity = new Entities_URF_RequestEntry();

                            entity.RdCtrlNo = lblCTRLNo.Text.Trim();
                            list = BLL.URF_TRANSACTION_GetRequestDetailsByCTRLNo(entity);

                            if (list != null)
                            {
                                if (list.Count > 0)
                                {

                                    gvDataDetails.DataSource = list;
                                    gvDataDetails.DataBind();
                                    gvDataDetails.Visible = true;

                                    List<Entities_URF_RequestEntry> listStatus = new List<Entities_URF_RequestEntry>();

                                    int statCounter = 0;
                                    foreach (Entities_URF_RequestEntry eStat in list)
                                    {
                                        Entities_URF_RequestEntry entityStatus = new Entities_URF_RequestEntry();

                                        // PROD SEC MANAGER
                                        if (eStat.StatSTATProdSecManager == "-1")
                                        {
                                            entityStatus.StatProdSecManager = "AUTO-APPROVED";
                                        }
                                        else
                                        {
                                            entityStatus.StatProdSecManager = eStat.StatProdSecManager;
                                        }
                                        // PROD DEPT MANAGER
                                        if (eStat.StatSTATProdDeptManager == "-1")
                                        {
                                            entityStatus.StatProdDeptManager = "AUTO-APPROVED";
                                        }
                                        else
                                        {
                                            entityStatus.StatProdDeptManager = eStat.StatProdDeptManager;
                                        }
                                        // PROD DIV MANAGER
                                        if (eStat.StatSTATProdDivManager == "-1")
                                        {
                                            entityStatus.StatProdDivManager = "AUTO-APPROVED";
                                        }
                                        else
                                        {
                                            entityStatus.StatProdDivManager = eStat.StatProdDivManager;
                                        }
                                        // PROD HQ MANAGER
                                        if (eStat.StatSTATProdHQManager == "-1")
                                        {
                                            entityStatus.StatProdHQManager = "AUTO-APPROVED";
                                        }
                                        else
                                        {
                                            entityStatus.StatProdHQManager = eStat.StatProdHQManager;
                                        }

                                        if (!string.IsNullOrEmpty(eStat.DisapprovalCause))
                                        {
                                            lblDisapprovalCause.Visible = true;
                                            lblDisapprovalCause.Text += "DISAPPROVAL CAUSE: " + eStat.DisapprovalCause;
                                        }


                                        entityStatus.StatPurchasingBuyer = eStat.StatPurchasingBuyer;
                                        entityStatus.StatPurchasingManager = eStat.StatPurchasingManager;

                                        if (statCounter <= 0)
                                        {
                                            listStatus.Add(entityStatus);

                                            lblAttention.Text = "ATTENTION : " + eStat.RhAttention;

                                            if (string.IsNullOrEmpty(eStat.RhOtherReason))
                                            {
                                                lblReason.Text = "REASON : " + eStat.RhReason;
                                            }
                                            else
                                            {
                                                lblReason.Text = "REASON : " + eStat.RhOtherReason;
                                            }

                                            lblSupplier.Text = "SUPPLIER : " + eStat.RhSupplier;

                                            if (eStat.RhType == "1")
                                            {
                                                lblType.Text = "TYPE : LOCAL";
                                            }
                                            if (eStat.RhType == "2")
                                            {
                                                lblType.Text = "TYPE : OVERSEAS";
                                            }

                                            lblAttention.Visible = true;
                                            lblReason.Visible = true;
                                            lblSupplier.Visible = true;
                                            lblType.Visible = true;

                                            if (!string.IsNullOrEmpty(eStat.RhAttachment1))
                                            {
                                                lblAttachment1.Visible = true;
                                                lbAttachment1.Visible = true;
                                                lbAttachment1.Text = eStat.RhAttachment1;

                                                string URL1 = "~/URF_Request/" + eStat.RdCtrlNo + "/" + eStat.RhAttachment1;

                                                URL1 = Page.ResolveClientUrl(URL1);
                                                lbAttachment1.OnClientClick = "window.open('" + URL1 + "'); return false;";

                                            }
                                            if (!string.IsNullOrEmpty(eStat.RhAttachment2))
                                            {
                                                lblAttachment2.Visible = true;
                                                lbAttachment2.Visible = true;
                                                lbAttachment2.Text = eStat.RhAttachment2;

                                                string URL2 = "~/URF_Request/" + eStat.RdCtrlNo + "/" + eStat.RhAttachment2;

                                                URL2 = Page.ResolveClientUrl(URL2);
                                                lbAttachment2.OnClientClick = "window.open('" + URL2 + "'); return false;";
                                            }
                                            if (!string.IsNullOrEmpty(eStat.RhStockLifeAttachment))
                                            {
                                                lblStockLifeAttachment.Visible = true;
                                                lbStockLifeAttachment.Visible = true;
                                                lbStockLifeAttachment.Text = eStat.RhStockLifeAttachment;

                                                string URL3 = "~/URF_Request/" + eStat.RdCtrlNo + "/" + eStat.RhStockLifeAttachment;

                                                URL3 = Page.ResolveClientUrl(URL3);
                                                lbStockLifeAttachment.OnClientClick = "window.open('" + URL3 + "'); return false;";
                                            }

                                        }
                                        statCounter++;
                                    }

                                    gvDataStatus.DataSource = listStatus;
                                    gvDataStatus.DataBind();
                                    gvDataStatus.Visible = true;

                                }
                            }
                        }
                        else
                        {
                            ibApproved.ImageUrl = "~/images/A1.png";

                            gvDataDetails.Visible = false;
                            gvDataStatus.Visible = false;
                            lblAttention.Visible = false;
                            lblReason.Visible = false;
                            lblSupplier.Visible = false;
                            lblType.Visible = false;
                            lblAttachment1.Visible = false;
                            lbAttachment1.Visible = false;
                            lblAttachment2.Visible = false;
                            lbAttachment2.Visible = false;
                            lblStockLifeAttachment.Visible = false;
                            lbStockLifeAttachment.Visible = false;
                            lblDisapprovalCause.Visible = false;
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

                            List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();
                            Entities_URF_RequestEntry entity = new Entities_URF_RequestEntry();

                            entity.RdCtrlNo = lblCTRLNo.Text.Trim();
                            list = BLL.URF_TRANSACTION_GetRequestDetailsByCTRLNo(entity);

                            if (list != null)
                            {
                                if (list.Count > 0)
                                {

                                    gvDataDetails.DataSource = list;
                                    gvDataDetails.DataBind();
                                    gvDataDetails.Visible = true;

                                    List<Entities_URF_RequestEntry> listStatus = new List<Entities_URF_RequestEntry>();

                                    int statCounter = 0;
                                    foreach (Entities_URF_RequestEntry eStat in list)
                                    {
                                        Entities_URF_RequestEntry entityStatus = new Entities_URF_RequestEntry();

                                        // PROD SEC MANAGER
                                        if (eStat.StatSTATProdSecManager == "-1")
                                        {
                                            entityStatus.StatProdSecManager = "AUTO-APPROVED";
                                        }
                                        else
                                        {
                                            entityStatus.StatProdSecManager = eStat.StatProdSecManager;
                                        }
                                        // PROD DEPT MANAGER
                                        if (eStat.StatSTATProdDeptManager == "-1")
                                        {
                                            entityStatus.StatProdDeptManager = "AUTO-APPROVED";
                                        }
                                        else
                                        {
                                            entityStatus.StatProdDeptManager = eStat.StatProdDeptManager;
                                        }
                                        // PROD DIV MANAGER
                                        if (eStat.StatSTATProdDivManager == "-1")
                                        {
                                            entityStatus.StatProdDivManager = "AUTO-APPROVED";
                                        }
                                        else
                                        {
                                            entityStatus.StatProdDivManager = eStat.StatProdDivManager;
                                        }
                                        // PROD HQ MANAGER
                                        if (eStat.StatSTATProdHQManager == "-1")
                                        {
                                            entityStatus.StatProdHQManager = "AUTO-APPROVED";
                                        }
                                        else
                                        {
                                            entityStatus.StatProdHQManager = eStat.StatProdHQManager;
                                        }

                                        if (!string.IsNullOrEmpty(eStat.DisapprovalCause))
                                        {
                                            lblDisapprovalCause.Visible = true;
                                            lblDisapprovalCause.Text += "DISAPPROVAL CAUSE: " + eStat.DisapprovalCause;
                                        }


                                        entityStatus.StatPurchasingBuyer = eStat.StatPurchasingBuyer;
                                        entityStatus.StatPurchasingManager = eStat.StatPurchasingManager;

                                        if (statCounter <= 0)
                                        {
                                            listStatus.Add(entityStatus);

                                            lblAttention.Text = "ATTENTION : " + eStat.RhAttention;

                                            if (string.IsNullOrEmpty(eStat.RhOtherReason))
                                            {
                                                lblReason.Text = "REASON : " + eStat.RhReason;
                                            }
                                            else
                                            {
                                                lblReason.Text = "REASON : " + eStat.RhOtherReason;
                                            }

                                            lblSupplier.Text = "SUPPLIER : " + eStat.RhSupplier;

                                            if (eStat.RhType == "1")
                                            {
                                                lblType.Text = "TYPE : LOCAL";
                                            }
                                            if (eStat.RhType == "2")
                                            {
                                                lblType.Text = "TYPE : OVERSEAS";
                                            }

                                            lblAttention.Visible = true;
                                            lblReason.Visible = true;
                                            lblSupplier.Visible = true;
                                            lblType.Visible = true;

                                            if (!string.IsNullOrEmpty(eStat.RhAttachment1))
                                            {
                                                lblAttachment1.Visible = true;
                                                lbAttachment1.Visible = true;
                                                lbAttachment1.Text = eStat.RhAttachment1;

                                                string URL1 = "~/URF_Request/" + eStat.RdCtrlNo + "/" + eStat.RhAttachment1;

                                                URL1 = Page.ResolveClientUrl(URL1);
                                                lbAttachment1.OnClientClick = "window.open('" + URL1 + "'); return false;";

                                            }
                                            if (!string.IsNullOrEmpty(eStat.RhAttachment2))
                                            {
                                                lblAttachment2.Visible = true;
                                                lbAttachment2.Visible = true;
                                                lbAttachment2.Text = eStat.RhAttachment2;

                                                string URL2 = "~/URF_Request/" + eStat.RdCtrlNo + "/" + eStat.RhAttachment2;

                                                URL2 = Page.ResolveClientUrl(URL2);
                                                lbAttachment2.OnClientClick = "window.open('" + URL2 + "'); return false;";
                                            }
                                            if (!string.IsNullOrEmpty(eStat.RhStockLifeAttachment))
                                            {
                                                lblStockLifeAttachment.Visible = true;
                                                lbStockLifeAttachment.Visible = true;
                                                lbStockLifeAttachment.Text = eStat.RhStockLifeAttachment;

                                                string URL3 = "~/URF_Request/" + eStat.RdCtrlNo + "/" + eStat.RhStockLifeAttachment;

                                                URL3 = Page.ResolveClientUrl(URL3);
                                                lbStockLifeAttachment.OnClientClick = "window.open('" + URL3 + "'); return false;";
                                            }

                                        }
                                        statCounter++;
                                    }

                                    gvDataStatus.DataSource = listStatus;
                                    gvDataStatus.DataBind();
                                    gvDataStatus.Visible = true;

                                }
                            }

                        }
                        else
                        {
                            ibDisapproved.ImageUrl = "~/images/DA1.png";
                            txtRemarks.Text = string.Empty;
                            txtRemarks.Enabled = false;

                            gvDataDetails.Visible = false;
                            gvDataStatus.Visible = false;
                            lblAttention.Visible = false;
                            lblReason.Visible = false;
                            lblSupplier.Visible = false;
                            lblType.Visible = false;
                            lblAttachment1.Visible = false;
                            lbAttachment1.Visible = false;
                            lblAttachment2.Visible = false;
                            lbAttachment2.Visible = false;
                            lblStockLifeAttachment.Visible = false;
                            lbStockLifeAttachment.Visible = false;
                            lblDisapprovalCause.Visible = false;
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
                    Label lblStatAll = (Label)e.Row.FindControl("lblStatAll");
                    Label lblStatColor = (Label)e.Row.FindControl("lblStatColor");

                    lblStatAll.Style.Add("background-color", lblStatColor.Text.Trim());

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
                        GridView gvDataDetails = (GridView)gvData.Rows[i].Cells[7].FindControl("gvDataDetails");


                        if (ibApproved.ImageUrl == "~/images/A2.png")
                        {
                            if (lblStatAll.Text.ToUpper() == "FOR SEC. MNGR APPROVAL")
                            {
                                query1 += "UPDATE URF_TRANSACTION_RequestStatus SET ProdSecManager = '" + approvedBy + "', DOAProdSecManager = CONVERT(VARCHAR, GETDATE(), 22), STATProdSecManager = '1' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR DEPT. MNGR APPROVAL")
                            {
                                query1 += "UPDATE URF_TRANSACTION_RequestStatus SET ProdDeptManager = '" + approvedBy + "', DOAProdDeptManager = CONVERT(VARCHAR, GETDATE(), 22), STATProdDeptManager = '1' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR DIV. MNGR APPROVAL")
                            {
                                query1 += "UPDATE URF_TRANSACTION_RequestStatus SET ProdDivManager = '" + approvedBy + "', DOAProdDivManager = CONVERT(VARCHAR, GETDATE(), 22), STATProdDivManager = '1' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR HQ MNGR APPROVAL")
                            {
                                query1 += "UPDATE URF_TRANSACTION_RequestStatus SET ProdHQManager = '" + approvedBy + "', DOAProdHQManager = CONVERT(VARCHAR, GETDATE(), 22), STATProdHQManager = '1' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR PUR BUYER APPROVAL" || lblStatAll.Text.ToUpper() == "DISAPPROVED")
                            {
                                query1 += "UPDATE URF_TRANSACTION_RequestStatus SET PurchasingBuyer = '" + approvedBy + "', DOAPurchasingBuyer = CONVERT(VARCHAR, GETDATE(), 22), STATPurchasingBuyer = '1', STATPurchasingManager = '0', Remarks = '' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";

                                for (int x = 0; x < gvDataDetails.Rows.Count; x++)
                                {
                                    Label lblReplyDeliveryDate = (Label)gvDataDetails.Rows[x].Cells[9].FindControl("lblReplyDeliveryDate");
                                    Label lblDetailsRefId = (Label)gvDataDetails.Rows[x].Cells[0].FindControl("lblDetailsRefId");

                                    if (!string.IsNullOrEmpty(lblReplyDeliveryDate.Text))
                                    {
                                        query1 += "UPDATE URF_TRANSACTION_RequestDetails SET ReplyDeliveryDate = '" + lblReplyDeliveryDate.Text.Replace("<p style='color:red;'>", "").Replace("</p>", "") + "' WHERE RefId ='" + lblDetailsRefId.Text.Trim() + "' ";
                                        queryStatusCounter++;
                                    }

                                }

                            }
                            if (lblStatAll.Text.ToUpper() == "FOR PUR MANAGER APPROVAL")
                            {
                                query1 += "UPDATE URF_TRANSACTION_RequestStatus SET PurchasingManager = '" + approvedBy + "', DOAPurchasingManager = CONVERT(VARCHAR, GETDATE(), 22), STATPurchasingManager = '1' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }

                            queryStatusCounter++;
                            tempCtrlNo += lblCTRLNo.Text.Trim().ToUpper() + ", ";
                        }

                        if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                        {
                            if (lblStatAll.Text.ToUpper() == "FOR SEC. MNGR APPROVAL")
                            {
                                query1 += "UPDATE URF_TRANSACTION_RequestStatus SET ProdSecManager = '" + approvedBy + "', DOAProdSecManager = CONVERT(VARCHAR, GETDATE(), 22), STATProdSecManager = '2', Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR DEPT. MNGR APPROVAL")
                            {
                                query1 += "UPDATE URF_TRANSACTION_RequestStatus SET ProdDeptManager = '" + approvedBy + "', DOAProdDeptManager = CONVERT(VARCHAR, GETDATE(), 22), STATProdDeptManager = '2', Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR DIV. MNGR APPROVAL")
                            {
                                query1 += "UPDATE URF_TRANSACTION_RequestStatus SET ProdDivManager = '" + approvedBy + "', DOAProdDivManager = CONVERT(VARCHAR, GETDATE(), 22), STATProdDivManager = '2', Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR HQ MNGR APPROVAL")
                            {
                                query1 += "UPDATE URF_TRANSACTION_RequestStatus SET ProdHQManager = '" + approvedBy + "', DOAProdHQManager = CONVERT(VARCHAR, GETDATE(), 22), STATProdHQManager = '2', Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR PUR BUYER APPROVAL")
                            {
                                query1 += "UPDATE URF_TRANSACTION_RequestStatus SET PurchasingBuyer = '" + approvedBy + "', DOAPurchasingBuyer = CONVERT(VARCHAR, GETDATE(), 22), STATPurchasingBuyer = '2', Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                            }
                            if (lblStatAll.Text.ToUpper() == "FOR PUR MANAGER APPROVAL")
                            {
                                query1 += "UPDATE URF_TRANSACTION_RequestStatus SET PurchasingManager = '" + approvedBy + "', DOAPurchasingManager = CONVERT(VARCHAR, GETDATE(), 22), STATPurchasingManager = '2', Remarks = '" + txtRemarks.Text + "' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                                query1 += "UPDATE URF_TRANSACTION_RequestStatus SET STATPurchasingBuyer = '0' WHERE CTRLNo ='" + lblCTRLNo.Text.Trim() + "' ";
                                queryStatusCounter++; // THIS IS FOR STATPurchasingBuyer = '0'
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
                            querySuccess = BLL.URF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                            if (querySuccess == queryStatusCounter.ToString())
                            {
                                Session["successMessage"] = "URF NUMBER(S) : <b>" + tempCtrlNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
                                Session["successTransactionName"] = "URF_APPROVALFORM";
                                Session["successReturnPage"] = "URF_ApprovalForm.aspx";

                                //SEND NOTIFICATION
                                if (gvData.Rows.Count > 0)
                                {
                                    for (int i = 0; i < gvData.Rows.Count; i++)
                                    {
                                        ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[4].FindControl("ibApproved");
                                        Label lblStatAll = (Label)gvData.Rows[i].Cells[3].FindControl("lblStatAll");
                                        Label lblCategoryId = (Label)gvData.Rows[i].Cells[1].FindControl("lblCategoryId");
                                        LinkButton lbCTRLNo = (LinkButton)gvData.Rows[i].Cells[1].FindControl("lbCTRLNo");
                                        Label lblRequesterEmail = (Label)gvData.Rows[i].Cells[1].FindControl("lblRequesterEmail");
                                        Label lblRequester = (Label)gvData.Rows[i].Cells[4].FindControl("lblRequester");

                                        if (ibApproved.ImageUrl == "~/images/A2.png")
                                        {
                                            string approvedBy2 = string.Empty;

                                            // SEND NOTIFICATION TO PRODUCTION SECTION MANAGER -----------------------------------------------------------------------------------------
                                            if (lblStatAll.Text.ToUpper() == "FOR SEC. MNGR APPROVAL")
                                            {

                                                approvedBy2 = "APPROVED BY SECTION MANAGER";

                                                List<Entities_Common_ForApproval> listSecManagerNotification = new List<Entities_Common_ForApproval>();
                                                listSecManagerNotification = BLL_COMMON.Common_GetForApprovals().Where(itm => itm.Approval_Department == Session["Department"].ToString() && !string.IsNullOrEmpty(itm.Approval_URF_ForApproval_ProdDeptManager)).ToList();

                                                string name = string.Empty;
                                                string noti = string.Empty;
                                                string items = string.Empty;
                                                string autogenerated = string.Empty;

                                                if (listSecManagerNotification != null)
                                                {
                                                    if (listSecManagerNotification.Count > 0)
                                                    {
                                                        foreach (Entities_Common_ForApproval sceManager in listSecManagerNotification)
                                                        {
                                                            if (int.Parse(sceManager.Approval_URF_ForApproval_ProdDeptManager) > 0)
                                                            {
                                                                // SEND NOTIFICATION
                                                                if (!string.IsNullOrEmpty(sceManager.Approval_EmailAddress))
                                                                {
                                                                    //--------------------------------------------------------------------------------------------

                                                                    autogenerated = "<br/>To view your for approval(s), go to <a href='http://10.27.1.170:9292/Default.aspx'>http://10.27.1.170:9292/Default.aspx</a><br/><br/> Thank you! <br/><br/> *** This is an automatically generated email. Please do not reply ***";
                                                                    noti = "<p style='font-size:22px;'><b>NOTIFICATION APPROVAL</b></p><br/><br/>";
                                                                    name = "Hi <b>" + CryptorEngine.Decrypt(sceManager.Approval_Fullname, true) + "</b> Good Day! <br/> Please check below request item(s) for your approval. <br/><br/>";

                                                                    items = "URGENT REQUEST FORM (URF) FOR PROD. DEPARTMENT MANAGER APPROVAL - <a href='http://10.27.1.170:9292/URF_ApprovalForm.aspx'>" + sceManager.Approval_URF_ForApproval_ProdDeptManager + "</a><br/>";

                                                                    COMMON.sendEmailTo_CRF_DRF_URF_Approvers(sceManager.Approval_EmailAddress, ConfigurationManager.AppSettings["email-username"].ToString(), "PUR_SOFRA_Notifications", noti + name + items + autogenerated);

                                                                    // Clear variables for next iteration
                                                                    name = string.Empty;
                                                                    noti = string.Empty;
                                                                    items = string.Empty;
                                                                    autogenerated = string.Empty;

                                                                    //--------------------------------------------------------------------------------------------
                                                                }

                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                            // SEND NOTIFICATION TO PRODUCTION DEPARTMENT MANAGER -----------------------------------------------------------------------------------------
                                            if (lblStatAll.Text.ToUpper() == "FOR DEPT. MNGR APPROVAL")
                                            {

                                                approvedBy2 = "APPROVED BY DEPARTMENT MANAGER";

                                                List<Entities_Common_ForApproval> listDeptManagerNotification = new List<Entities_Common_ForApproval>();
                                                listDeptManagerNotification = BLL_COMMON.Common_GetForApprovals().Where(itm => itm.Approval_Division == Session["Division"].ToString() && !string.IsNullOrEmpty(itm.Approval_URF_ForApproval_ProdDivManager)).ToList();

                                                string name = string.Empty;
                                                string noti = string.Empty;
                                                string items = string.Empty;
                                                string autogenerated = string.Empty;

                                                if (listDeptManagerNotification != null)
                                                {
                                                    if (listDeptManagerNotification.Count > 0)
                                                    {
                                                        foreach (Entities_Common_ForApproval deptManager in listDeptManagerNotification)
                                                        {
                                                            if (int.Parse(deptManager.Approval_URF_ForApproval_ProdDivManager) > 0)
                                                            {
                                                                // SEND NOTIFICATION
                                                                if (!string.IsNullOrEmpty(deptManager.Approval_EmailAddress))
                                                                {
                                                                    //--------------------------------------------------------------------------------------------

                                                                    autogenerated = "<br/>To view your for approval(s), go to <a href='http://10.27.1.170:9292/Default.aspx'>http://10.27.1.170:9292/Default.aspx</a><br/><br/> Thank you! <br/><br/> *** This is an automatically generated email. Please do not reply ***";
                                                                    noti = "<p style='font-size:22px;'><b>NOTIFICATION APPROVAL</b></p><br/><br/>";
                                                                    name = "Hi <b>" + CryptorEngine.Decrypt(deptManager.Approval_Fullname, true) + "</b> Good Day! <br/> Please check below request item(s) for your approval. <br/><br/>";

                                                                    items = "URGENT REQUEST FORM (URF) FOR PROD. DIVISION MANAGER APPROVAL - <a href='http://10.27.1.170:9292/URF_ApprovalForm.aspx'>" + deptManager.Approval_URF_ForApproval_ProdDivManager + "</a><br/>";

                                                                    COMMON.sendEmailTo_CRF_DRF_URF_Approvers(deptManager.Approval_EmailAddress, ConfigurationManager.AppSettings["email-username"].ToString(), "PUR_SOFRA_Notifications", noti + name + items + autogenerated);

                                                                    // Clear variables for next iteration
                                                                    name = string.Empty;
                                                                    noti = string.Empty;
                                                                    items = string.Empty;
                                                                    autogenerated = string.Empty;

                                                                    //--------------------------------------------------------------------------------------------
                                                                }

                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                            // SEND NOTIFICATION TO PRODUCTION DIVISION MANAGER -----------------------------------------------------------------------------------------
                                            if (lblStatAll.Text.ToUpper() == "FOR DIV. MNGR APPROVAL")
                                            {

                                                approvedBy2 = "APPROVED BY DIVISION MANAGER";

                                                List<Entities_Common_ForApproval> listDivManagerNotification = new List<Entities_Common_ForApproval>();
                                                listDivManagerNotification = BLL_COMMON.Common_GetForApprovals().Where(itm => itm.Approval_HQ == Session["HQ"].ToString() && !string.IsNullOrEmpty(itm.Approval_URF_ForApproval_ProdHQManager)).ToList();

                                                string name = string.Empty;
                                                string noti = string.Empty;
                                                string items = string.Empty;
                                                string autogenerated = string.Empty;

                                                if (listDivManagerNotification != null)
                                                {
                                                    if (listDivManagerNotification.Count > 0)
                                                    {
                                                        foreach (Entities_Common_ForApproval divManager in listDivManagerNotification)
                                                        {
                                                            if (int.Parse(divManager.Approval_URF_ForApproval_ProdHQManager) > 0)
                                                            {
                                                                // SEND NOTIFICATION
                                                                if (!string.IsNullOrEmpty(divManager.Approval_EmailAddress))
                                                                {
                                                                    //--------------------------------------------------------------------------------------------

                                                                    autogenerated = "<br/>To view your for approval(s), go to <a href='http://10.27.1.170:9292/Default.aspx'>http://10.27.1.170:9292/Default.aspx</a><br/><br/> Thank you! <br/><br/> *** This is an automatically generated email. Please do not reply ***";
                                                                    noti = "<p style='font-size:22px;'><b>NOTIFICATION APPROVAL</b></p><br/><br/>";
                                                                    name = "Hi <b>" + CryptorEngine.Decrypt(divManager.Approval_Fullname, true) + "</b> Good Day! <br/> Please check below request item(s) for your approval. <br/><br/>";

                                                                    items = "URGENT REQUEST FORM (URF) FOR PROD. HQ MANAGER APPROVAL - <a href='http://10.27.1.170:9292/URF_ApprovalForm.aspx'>" + divManager.Approval_URF_ForApproval_ProdHQManager + "</a><br/>";

                                                                    COMMON.sendEmailTo_CRF_DRF_URF_Approvers(divManager.Approval_EmailAddress, ConfigurationManager.AppSettings["email-username"].ToString(), "PUR_SOFRA_Notifications", noti + name + items + autogenerated);

                                                                    // Clear variables for next iteration
                                                                    name = string.Empty;
                                                                    noti = string.Empty;
                                                                    items = string.Empty;
                                                                    autogenerated = string.Empty;

                                                                    //--------------------------------------------------------------------------------------------
                                                                }

                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                            // SEND NOTIFICATION TO PRODUCTION HQ MANAGER -----------------------------------------------------------------------------------------
                                            if (lblStatAll.Text.ToUpper() == "FOR HQ MNGR APPROVAL")
                                            {

                                                approvedBy2 = "APPROVED BY HQ MANAGER";

                                                List<Entities_Common_ForApproval> listHQManagerNotification = new List<Entities_Common_ForApproval>();
                                                //listHQManagerNotification = BLL_COMMON.Common_GetForApprovals().Where(itm => itm.Approval_Department == Session["HQ"].ToString() && !string.IsNullOrEmpty(itm.Approval_URF_ForApproval_ProdHQManager)).ToList();
                                                listHQManagerNotification = BLL_COMMON.Common_GetForApprovals2(lblCategoryId.Text.Trim());
                                                string name = string.Empty;
                                                string noti = string.Empty;
                                                string items = string.Empty;
                                                string autogenerated = string.Empty;

                                                if (listHQManagerNotification != null)
                                                {
                                                    if (listHQManagerNotification.Count > 0)
                                                    {
                                                        foreach (Entities_Common_ForApproval hqManager in listHQManagerNotification)
                                                        {
                                                            if (!string.IsNullOrEmpty(hqManager.Approval_URF_ForApproval_Buyer))
                                                            {
                                                                if (int.Parse(hqManager.Approval_URF_ForApproval_Buyer) > 0)
                                                                {
                                                                    // SEND NOTIFICATION
                                                                    if (!string.IsNullOrEmpty(hqManager.Approval_EmailAddress))
                                                                    {
                                                                        //--------------------------------------------------------------------------------------------

                                                                        autogenerated = "<br/>To view your for approval(s), go to <a href='http://10.27.1.170:9292/Default.aspx'>http://10.27.1.170:9292/Default.aspx</a><br/><br/> Thank you! <br/><br/> *** This is an automatically generated email. Please do not reply ***";
                                                                        noti = "<p style='font-size:22px;'><b>NOTIFICATION APPROVAL</b></p><br/><br/>";
                                                                        name = "Hi <b>" + CryptorEngine.Decrypt(hqManager.Approval_Fullname, true) + "</b> Good Day! <br/> Please check below request item(s) for your approval. <br/><br/>";

                                                                        items = "URGENT REQUEST FORM (URF) FOR PURCHASING BUYER APPROVAL - <a href='http://10.27.1.170:9292/URF_ApprovalForm.aspx'>" + hqManager.Approval_URF_ForApproval_Buyer + "</a><br/>";

                                                                        //CHECK FIRST IF THERE IS EMPTY PO NUMBERS
                                                                        int emptyPOForBuyer = 0;
                                                                        List<Entities_URF_RequestEntry> listDetailsForBuyer = new List<Entities_URF_RequestEntry>();
                                                                        Entities_URF_RequestEntry entityDetailsForBuyer = new Entities_URF_RequestEntry();
                                                                        entityDetailsForBuyer.RdCtrlNo = lbCTRLNo.Text.Trim();

                                                                        listDetailsForBuyer = BLL.URF_TRANSACTION_GetRequestDetailsByCTRLNo3(entityDetailsForBuyer);

                                                                        if (listDetailsForBuyer != null)
                                                                        {
                                                                            if (listDetailsForBuyer.Count > 0)
                                                                            {
                                                                                foreach (Entities_URF_RequestEntry eDetailsForBuyer in listDetailsForBuyer)
                                                                                {

                                                                                    if (string.IsNullOrEmpty(eDetailsForBuyer.RdPONO))
                                                                                    {
                                                                                        emptyPOForBuyer++;
                                                                                    }

                                                                                }
                                                                            }

                                                                        }

                                                                        if (emptyPOForBuyer <= 0)
                                                                        {
                                                                            COMMON.sendEmailTo_CRF_DRF_URF_Approvers(hqManager.Approval_EmailAddress, ConfigurationManager.AppSettings["email-username"].ToString(), "PUR_SOFRA_Notifications", noti + name + items + autogenerated);
                                                                        }

                                                                        // Clear variables for next iteration
                                                                        name = string.Empty;
                                                                        noti = string.Empty;
                                                                        items = string.Empty;
                                                                        autogenerated = string.Empty;

                                                                        //--------------------------------------------------------------------------------------------
                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                            // SEND NOTIFICATION TO PURCHASING BUYER -----------------------------------------------------------------------------------------
                                            if (lblStatAll.Text.ToUpper() == "FOR PUR BUYER APPROVAL")
                                            {
                                                List<Entities_Common_ForApproval> listBuyerNotification = new List<Entities_Common_ForApproval>();
                                                listBuyerNotification = BLL_COMMON.Common_GetForApprovals2(string.Empty);

                                                string name = string.Empty;
                                                string noti = string.Empty;
                                                string items = string.Empty;
                                                string autogenerated = string.Empty;

                                                if (listBuyerNotification != null)
                                                {
                                                    if (listBuyerNotification.Count > 0)
                                                    {
                                                        foreach (Entities_Common_ForApproval buyer in listBuyerNotification)
                                                        {
                                                            if (!string.IsNullOrEmpty(buyer.Approval_URF_ForApproval_PurchasingManager))
                                                            {
                                                                if (int.Parse(buyer.Approval_URF_ForApproval_PurchasingManager) > 0)
                                                                {
                                                                    // SEND NOTIFICATION
                                                                    if (!string.IsNullOrEmpty(buyer.Approval_EmailAddress))
                                                                    {
                                                                        //--------------------------------------------------------------------------------------------

                                                                        autogenerated = "<br/>To view your for approval(s), go to <a href='http://10.27.1.170:9292/Default.aspx'>http://10.27.1.170:9292/Default.aspx</a><br/><br/> Thank you! <br/><br/> *** This is an automatically generated email. Please do not reply ***";
                                                                        noti = "<p style='font-size:22px;'><b>NOTIFICATION APPROVAL</b></p><br/><br/>";
                                                                        name = "Hi <b>" + CryptorEngine.Decrypt(buyer.Approval_Fullname, true) + "</b> Good Day! <br/> Please check below request item(s) for your approval. <br/><br/>";

                                                                        items = "URGENT REQUEST FORM (URF) FOR PURCHASING MANAGER APPROVAL - <a href='http://10.27.1.170:9292/URF_ApprovalForm.aspx'>" + buyer.Approval_URF_ForApproval_PurchasingManager + "</a><br/>";

                                                                        COMMON.sendEmailTo_CRF_DRF_URF_Approvers(buyer.Approval_EmailAddress, ConfigurationManager.AppSettings["email-username"].ToString(), "PUR_SOFRA_Notifications", noti + name + items + autogenerated);

                                                                        // Clear variables for next iteration
                                                                        name = string.Empty;
                                                                        noti = string.Empty;
                                                                        items = string.Empty;
                                                                        autogenerated = string.Empty;

                                                                        //--------------------------------------------------------------------------------------------
                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                            }

                                            //if (lblStatAll.Text.ToUpper() == "FOR PUR MANAGER APPROVAL")
                                            //{
                                            //    sendToSuppliers();
                                            //}

                                            // SEND NOTIFICATION TO PURCHASING MANAGERS -----------------------------------------------------------------------------------------
                                            //if (lblStatAll.Text.ToUpper() == "FOR PUR MANAGER APPROVAL")
                                            //{
                                            //    List<Entities_Common_ForApproval> listPurchasingManagerNotification = new List<Entities_Common_ForApproval>();
                                            //    listPurchasingManagerNotification = BLL_COMMON.Common_GetForApprovals2(string.Empty);

                                            //    string name = string.Empty;
                                            //    string noti = string.Empty;
                                            //    string items = string.Empty;
                                            //    string autogenerated = string.Empty;

                                            //    if (listPurchasingManagerNotification != null)
                                            //    {
                                            //        if (listPurchasingManagerNotification.Count > 0)
                                            //        {
                                            //            foreach (Entities_Common_ForApproval purchasingManager in listPurchasingManagerNotification)
                                            //            {
                                            //                if (int.Parse(purchasingManager.Approval_URF_ForApproval_PurchasingManager) > 0)
                                            //                {
                                            //                    // SEND NOTIFICATION
                                            //                    if (!string.IsNullOrEmpty(purchasingManager.Approval_EmailAddress))
                                            //                    {
                                            //                        //--------------------------------------------------------------------------------------------

                                            //                        autogenerated = "<br/>To view your for approval(s), go to <a href='http://10.27.1.170:9292/Default.aspx'>http://10.27.1.170:9292/Default.aspx</a><br/><br/> Thank you! <br/><br/> *** This is an automatically generated email. Please do not reply ***";
                                            //                        noti = "<p style='font-size:22px;'><b>NOTIFICATION APPROVAL</b></p><br/><br/>";
                                            //                        name = "Hi <b>" + CryptorEngine.Decrypt(purchasingManager.Approval_Fullname, true) + "</b> Good Day! <br/> Please check below request item(s) for your approval. <br/><br/>";

                                            //                        items = "URGENT REQUEST FORM (URF) FOR PURCHASING MANAGER APPROVAL - <a href='http://10.27.1.170:9292/URF_ApprovalForm.aspx'>" + purchasingManager.Approval_URF_ForApproval_PurchasingManager + "</a><br/>";

                                            //                        COMMON.sendEmailTo_CRF_DRF_URF_Approvers(purchasingManager.Approval_EmailAddress, ConfigurationManager.AppSettings["email-username"].ToString(), "PUR_SOFRA_Notifications", noti + name + items + autogenerated);

                                            //                        // Clear variables for next iteration
                                            //                        name = string.Empty;
                                            //                        noti = string.Empty;
                                            //                        items = string.Empty;
                                            //                        autogenerated = string.Empty;

                                            //                        //--------------------------------------------------------------------------------------------
                                            //                    }

                                            //                }
                                            //            }
                                            //        }
                                            //    }

                                            //}


                                            // SEND EMAIL TO REQUESTER IF REQUEST HAS EMPTY PONO
                                            List<Entities_URF_RequestEntry> listDetails = new List<Entities_URF_RequestEntry>();
                                            Entities_URF_RequestEntry entityDetails = new Entities_URF_RequestEntry();
                                            entityDetails.RdCtrlNo = lbCTRLNo.Text.Trim();

                                            listDetails = BLL.URF_TRANSACTION_GetRequestDetailsByCTRLNo3(entityDetails);

                                            if (listDetails != null)
                                            {
                                                if (listDetails.Count > 0)
                                                {

                                                    string userEmail = lblRequesterEmail.Text.Trim();
                                                    int emptyPO = 0;
                                                    string tableStart = "<table style='width:100%;' border='1'>" +
                                                                        "<tr>" +
                                                                        "<th align='left'>PO.NO.</th>" +
                                                                        "<th align='left'>PR.NO.</th>" +
                                                                        "<th align='left'>ITEM NAME</th>" +
                                                                        "<th align='left'>SPECS</th>" +
                                                                        "<th align='left'>QTY.</th>" +
                                                                        "<th align='left'>UOM</th>" +
                                                                        "<th align='left'>DEL.CONF.DATE</th>" +
                                                                        "<th align='left'>REQ.DEL.DATE</th>" +
                                                                        "<th align='left'>REPLY.DEL.DATE</th>" +
                                                                        "</tr>";
                                                    string tableEnd = "</table>";
                                                    string information = string.Empty;
                                                    string genericInfo = "Hi <b>" + lblRequester.Text.ToUpper() + "</b>, " +
                                                                         "System detected <b>EMPTY PO NUMBER(S)</b> in your URF Request (<b>" + lbCTRLNo.Text.Trim() + "</b>). <BR />" +
                                                                         "Please know that approval of this item will go through until HQ Manager Approval but will not proceed to SC-BUYER up until you update the <b>EMPTY PO NUMBER(S)</b> <BR />" +
                                                                         "To check your item go to <a href='http://10.27.1.170:9292/Default.aspx'>http://10.27.1.170:9292/Default.aspx</a> and update accordingly. <BR /><BR />" +
                                                                         "ITEM STATUS: <b>" + approvedBy2 + "</b><BR /><BR />";
                                                    string thankYou = "<BR /><BR />Thank you, <BR /> PUR_SOFRA_Notifications";

                                                    foreach (Entities_URF_RequestEntry eDetails in listDetails)
                                                    {
                                                        information += "<tr>" +
                                                                       "<td>" + eDetails.RdPONO + "</td>" +
                                                                       "<td>" + eDetails.RdPRNO + "</td>" +
                                                                       "<td>" + eDetails.RdItemName + "</td>" +
                                                                       "<td>" + eDetails.RdSpecs + "</td>" +
                                                                       "<td>" + eDetails.RdQuantity + "</td>" +
                                                                       "<td>" + eDetails.RdUnitOfMeasure + "</td>" +
                                                                       "<td>" + eDetails.RdDeliveryConfirmationDate + "</td>" +
                                                                       "<td>" + eDetails.RdRequestedDeliveryDate + "</td>" +
                                                                       "<td>" + eDetails.RdReplyDeliveryDate + "</td>" +
                                                                       "</tr>";

                                                        if (string.IsNullOrEmpty(eDetails.RdPONO))
                                                        {
                                                            emptyPO++;
                                                        }
                                                    }

                                                    information = tableStart + information + tableEnd;

                                                    if (emptyPO > 0)
                                                    {
                                                        if (!string.IsNullOrEmpty(userEmail))
                                                        {
                                                            COMMON.sendEmailTo_CRF_DRF_URF_Approvers(userEmail, ConfigurationManager.AppSettings["email-username"].ToString(), "PUR_SOFRA_Notifications_(URF-EMPTY-PO-NUMBERS)", genericInfo + information + thankYou);
                                                        }
                                                    }

                                                }
                                            }



                                        }

                                    }
                                }

                                sendToSuppliers();

                                Response.Redirect("SuccessPage.aspx", false);
                            }
                        }// end of if (queryStatusCounter > 0)
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select atleast 1 item to approved. No selected items for approval.');", true);
                        }

                    } //end of if (disApprovalCause > 0)

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
                bool hasDisapproved = false;
                bool hasApproved = false;
                bool hasClosed = false;
                int hasForPURManagerApproval = 0;


                if (gvData.Rows.Count > 0)
                {
                    for (int i = 0; i < gvData.Rows.Count; i++)
                    {
                        Label lblCTRLNo = (Label)gvData.Rows[i].Cells[0].FindControl("lblCTRLNo");
                        Label lblStatAll = (Label)gvData.Rows[i].Cells[3].FindControl("lblStatAll");
                        ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[4].FindControl("ibApproved");
                        ImageButton ibDisapproved = (ImageButton)gvData.Rows[i].Cells[4].FindControl("ibDisapproved");
                        ImageButton ibClosed = (ImageButton)gvData.Rows[i].Cells[4].FindControl("ibClosed");
                        TextBox txtRemarks = (TextBox)gvData.Rows[i].Cells[6].FindControl("txtRemarks");
                        Label lblCategory = (Label)gvData.Rows[i].Cells[1].FindControl("lblCategory");


                        if (ibApproved.ImageUrl == "~/images/A2.png")
                        {
                            if (lblStatAll.Text.ToUpper() == "FOR PUR MANAGER APPROVAL")
                            {
                                hasForPURManagerApproval++;

                                string path = Server.MapPath("~/URF_Request/" + lblCTRLNo.Text.Trim() + "/" + lblCTRLNo.Text.Trim() + ".html");
                                //string path = Server.MapPath("~/URF_Request/" + lblCTRLNo.Text.Trim() + "/" + lblCTRLNo.Text.Trim() + ".csv");
                                string userManualPath = Server.MapPath("~/UserManual/URF NOTES.docx");
                                string htmlTemplate = Server.MapPath("~/UserManual/URF_Template.txt");
                                string csvHeader = string.Empty;
                                string csvDetails = string.Empty;
                                string csvNewLine = string.Empty;
                                string attachment1 = string.Empty;
                                string attachment2 = string.Empty;
                                string attachmentStockLife = string.Empty;
                                string attachmentPath1 = string.Empty;
                                string attachmentPath2 = string.Empty;
                                string attachmentPathStockLife = string.Empty;
                                string attachmentPath = string.Empty;
                                string emailService = string.Empty;
                                string supplierEmail = string.Empty;
                                string supplierId = string.Empty;
                                string supplierName = string.Empty;
                                string reason = string.Empty;
                                int recCounter = 0;
                                string reOpenRemarks = string.Empty;
                                string tableHeader = string.Empty;
                                string tableDetails = string.Empty;
                                string table = string.Empty;


                                List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();
                                Entities_URF_RequestEntry entity = new Entities_URF_RequestEntry();

                                entity.RdCtrlNo = lblCTRLNo.Text.Trim();
                                list = BLL.URF_TRANSACTION_GetRequestDetailsByCTRLNo(entity);

                                if (list != null)
                                {
                                    if (list.Count > 0)
                                    {

                                        tableHeader = "<tr><th>REFID</th><th>PONO</th><th>PRNO</th><th>ITEM NAME</th><th>SPECIFICATION</th><th>QUANTITY</th><th>UNIT OF MEASURE</th><th>DEL.CONF.DATE</th><th>REQ.DEL.DATE</th><th>REPLY DELIVERY DATE</th></tr>";
                                        foreach (Entities_URF_RequestEntry eCSV in list)
                                        {
                                            if (recCounter <= 0)
                                            {
                                                attachment1 = eCSV.RhAttachment1;
                                                attachment2 = eCSV.RhAttachment2;
                                                attachmentStockLife = eCSV.RhStockLifeAttachment;
                                                supplierEmail = eCSV.RhSupplierEmail;
                                                supplierId = eCSV.RhSupplierId;
                                                supplierName = eCSV.RhSupplier;
                                                reason = eCSV.RhReason;
                                            }
                                            recCounter++;

                                            // TABLE CREATION
                                            tableDetails += "<tr><td>" + eCSV.RdRefId + "</td><td>" + eCSV.RdPONO + "</td><td>" + eCSV.RdPRNO + "</td><td>" + eCSV.RdItemName + "</td><td>" + eCSV.RdSpecs + "</td><td>" + eCSV.RdQuantity + "</td><td>" + eCSV.RdUOMDesc + "</td><td>" + eCSV.RdDeliveryConfirmationDate + "</td><td>" + eCSV.RdRequestedDeliveryDate + "</td><td><input type='text' id='rdd" + recCounter + "' name='rdd" + recCounter + "'></td></tr>";
                                        }

                                        table = "<table style='width:100%;'>" + tableHeader + tableDetails + "</table>";
                                    }

                                    if (System.IO.File.Exists(htmlTemplate))
                                    {
                                        string templateValue = System.IO.File.ReadAllText(htmlTemplate).Replace("filename.csv", lblCTRLNo.Text.Trim() + ".csv")
                                                                                                       .Replace("val_ctrlno", lblCTRLNo.Text.Trim())
                                                                                                       .Replace("val_supplier", supplierName)
                                                                                                       .Replace("val_reason", reason)
                                                                                                       .Replace("val_table", table)
                                                                                                       .Replace("val_title", lblCTRLNo.Text.Trim());


                                        if (!System.IO.File.Exists(path))
                                        {
                                            using (StreamWriter writer = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write)))
                                            {
                                                writer.WriteLine(templateValue);
                                            }
                                        }
                                    }

                                }


                                if (!string.IsNullOrEmpty(attachment1))
                                {
                                    attachmentPath1 = Server.MapPath("~/URF_Request/" + lblCTRLNo.Text.Trim() + "/" + attachment1);
                                }
                                if (!string.IsNullOrEmpty(attachment2))
                                {
                                    attachmentPath2 = Server.MapPath("~/URF_Request/" + lblCTRLNo.Text.Trim() + "/" + attachment2);
                                }
                                if (!string.IsNullOrEmpty(attachmentStockLife))
                                {
                                    attachmentPathStockLife = Server.MapPath("~/URF_Request/" + lblCTRLNo.Text.Trim() + "/" + attachmentStockLife);
                                }

                                if (!string.IsNullOrEmpty(attachmentPath1) && string.IsNullOrEmpty(attachmentPath2))
                                {
                                    attachmentPath = path + "," + userManualPath + "," + attachmentPath1;
                                    if (!string.IsNullOrEmpty(attachmentPathStockLife))
                                    {
                                        attachmentPath = path + "," + userManualPath + "," + attachmentPath1 + "," + attachmentPathStockLife;
                                    }
                                }
                                if (string.IsNullOrEmpty(attachmentPath1) && !string.IsNullOrEmpty(attachmentPath2))
                                {
                                    attachmentPath = path + "," + userManualPath + "," + attachmentPath2;
                                    if (!string.IsNullOrEmpty(attachmentPathStockLife))
                                    {
                                        attachmentPath = path + "," + userManualPath + "," + attachmentPath2 + "," + attachmentPathStockLife;
                                    }
                                }
                                if (!string.IsNullOrEmpty(attachmentPath1) && !string.IsNullOrEmpty(attachmentPath2))
                                {
                                    attachmentPath = path + "," + userManualPath + "," + attachmentPath1 + "," + attachmentPath2;
                                    if (!string.IsNullOrEmpty(attachmentPathStockLife))
                                    {
                                        attachmentPath = path + "," + userManualPath + "," + attachmentPath2 + "," + attachmentPath2 + "," + attachmentPathStockLife;
                                    }
                                }
                                if (string.IsNullOrEmpty(attachmentPath1) && string.IsNullOrEmpty(attachmentPath2))
                                {
                                    attachmentPath = path + "," + userManualPath;
                                }
                                if (string.IsNullOrEmpty(attachmentPath1) && string.IsNullOrEmpty(attachmentPath2) && !string.IsNullOrEmpty(attachmentPathStockLife))
                                {
                                    attachmentPath = path + "," + userManualPath + "," + attachmentPathStockLife;
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


                                emailService = COMMON.sendEmailToSuppliersURF(supplierEmail.Trim(), ConfigurationManager.AppSettings["email-username"], lblCTRLNo.Text.Trim(),
                                                "Hi <b>" + supplierName + "</b> Good Day!" + "<br /><br /> Kindly check the attached csv file (" + lblCTRLNo.Text.Trim() + ".csv) for our URF Request" + "<br /><br /> NOTE : Please refer to the attached document file (URF NOTES.docx) on how to update or put your response accordingly. DO NOT PUT special character comma (,) in your response." +
                                                "<br /><br />For this URF, please contact <b>" + lblCategory.Text.ToUpper() + "</b> Section <br /> <b>PLEASE DO REPLY WITHIN 48 HOURS</b> <br /><br />" +
                                                "<br /><br /><br />Thank You!<br /><br /><br />" +
                                                "*** This is an automatically generated email, Please reply accordingly *** <br /> <br />" +
                                                "You have received a new URF Request from ROHM Electronics Philippines Inc. <br /> <br />" +
                                                "<br /><br /><br /> For inquries, kindly see below contact details : <br />" + fixedBuyerInfo, attachmentPath, supplierName, lblCTRLNo.Text.Trim());


                                if (emailService.ToLower().Contains("success"))
                                {
                                    query1 += "INSERT INTO URF_TRANSACTION_SendReceived (CTRLNo, SendReceivedDate, TransactionType, SendBy) VALUES ('" + lblCTRLNo.Text.Trim() + "', GETDATE(), 'SEND','" + Session["UserFullName"].ToString() + "') ";
                                    tempCtrlNo += "(" + lblCTRLNo.Text.Trim().ToUpper() + " - SUCCESSFULLY SENT) , ";
                                }
                                else
                                {
                                    tempCtrlNo += "(" + lblCTRLNo.Text.Trim().ToUpper() + " - FAILED TO SEND) , ";
                                }

                                hasApproved = true;
                                queryStatusCounter++;

                            }

                        }


                    }


                    if (hasForPURManagerApproval > 0)
                    {
                        querySuccess = BLL.URF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                        if (querySuccess == queryStatusCounter.ToString() || tempCtrlNo.Contains("FAILED"))
                        {
                            Session["successMessage"] = "URF NUMBER(S) : <b>" + tempCtrlNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
                            Session["successTransactionName"] = "URF_APPROVALFORM";
                            Session["successReturnPage"] = "URF_ApprovalForm.aspx";

                            Response.Redirect("SuccessPage.aspx", false);
                        }
                    }
                    else
                    {
                    }

                }

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
                "attachment;filename=URF_Export.xls");
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
