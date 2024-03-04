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
using System.Xml;

namespace REPI_PUR_SOFRA
{
    public partial class RFQ_OnePage : System.Web.UI.Page
    {

        BLL_RFQ BLL = new BLL_RFQ();
        Common COMMON = new Common();

        BLL_Common BLL_COMMON = new BLL_Common();

        public string tabAttachmentSupplier = string.Empty;
        public string tabAttachmentContentsSupplier = string.Empty;

        public string errorSendingDetails = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        lblUser.Text = Session["UserFullName"].ToString().ToUpper() + " ";

                        //---------------------------------------------------------------------------------------------------

                        //btnSubmit_Click(sender, e);

                        txtFrom.Text = DateTime.Today.AddDays(-500).ToString("MM/dd/yyyy");
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

                                ddCategoryForApproval.Items.Clear();
                                ddCategoryForApproval.Items.Add("ALL CATEGORY");

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
                                            ddCategoryForApproval.Items.Add(item);
                                        }

                                    }

                                }

                            }
                        }

                        //---------------------------------------------------------------------------------------------------


                        if (Session["CategoryAccess"] != null)
                        {
                            if (int.Parse(Session["CategoryAccess"].ToString()) > 0)
                            {
                                ddCategory.Enabled = false;
                                ddCategory.Items.FindByValue(Session["CategoryAccess"].ToString()).Selected = true;
                            }
                            else
                            {
                                ddCategory.Enabled = true;
                            }
                        }

                        //---------------------------------------------------------------------------------------------------

                        //ddType.Items.FindByText("FOR SENDING").Selected = true;
                        //lblType.Text = "FOR SENDING".Trim();
                        //---------------------------------------------------------------------------------------------------

                        //btnSubmitReceiving_Click(sender, e);


                        //---------------------------------------------------------------------------------------------------
                        // INITIAL LOAD FOR APPROVAL

                        string categoryApproval = Session["CategoryAccess"].ToString();

                        if (Session["Username"].ToString() == "3844")
                        {
                            // Incharge Approval SIR VAL
                            ddApprover.Items.FindByValue("2").Selected = true;
                        }
                        //else if (Session["Username"].ToString() == "6985")
                        //{
                        //    // Incharge Approval SIR RUDY
                        //    ddApprover.Items.FindByValue("1").Selected = true;
                        //}
                        else if (Session["Username"].ToString() == "1152" || Session["Username"].ToString() == "1402")
                        {
                            // Department Manager Approval 
                            ddApprover.Items.FindByValue("3").Selected = true;
                        }
                        else if (Session["Username"].ToString() == "002")
                        {
                            // Division Manager Approval
                            ddApprover.Items.FindByValue("4").Selected = true;
                        }
                        else
                        {
                            // Buyer Approval 
                            ddApprover.Items.FindByValue("1").Selected = true;
                            ddApprover.Enabled = false;
                        }

                        if (!string.IsNullOrEmpty(categoryApproval))
                        {
                            if (int.Parse(categoryApproval) > 0)
                            {
                                ddCategoryForApproval.Items.FindByValue(categoryApproval).Selected = true;
                                ddCategoryForApproval.Enabled = false;
                            }
                            else
                            {
                                ddCategoryForApproval.Items.FindByText("ALL CATEGORY").Selected = true;
                                ddCategoryForApproval.Enabled = true;
                            }
                        }

                        Session["PurchasingIsApprovingRequest"] = "true";

                        // call submit button to load initial record
                        //btnSubmitForApproval_Click(sender, e);

                        //---------------------------------------------------------------------------------------------------
                        if (Session["UpdatesComingFromOnePage"] != null)
                        {
                            if (Session["UpdatesComingFromOnePage"].ToString() == "true")
                            {
                                txtSearch.Text = Session["RFQ_FromOnePageValue"].ToString();
                                btnAllRequest_Click(sender, e);
                            }
                        }
                        else
                        {
                            btnSubmit_Click(sender, e);
                            btnReceivingSending_Click(sender, e);
                        }

                        //---------------------------------------------------------------------------------------------------

                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                }
            }
        }
        protected void btnAllRequest_Click(object sender, EventArgs e)
        {
            tabAllRequest.Attributes.CssStyle.Add("display", "block");
            tabReceivingEntry.Attributes.CssStyle.Add("display", "none");
            divSendToSupplier.Attributes.CssStyle.Add("display", "none");
            tabForApproval.Attributes.CssStyle.Add("display", "none");
            tabSuccess.Attributes.CssStyle.Add("display", "none");

            btnAllRequest.Attributes.CssStyle.Add("color", "black");
            btnReceivingResend.Attributes.CssStyle.Add("color", "white");
            btnReceivingSending.Attributes.CssStyle.Add("color", "white");
            btnApproval.Attributes.CssStyle.Add("color", "white");
            btnHomePage.Attributes.CssStyle.Add("color", "white");

            LoadDefaultAllRequest();
            
        }
        protected void btnReceivingSending_Click(object sender, EventArgs e)
        {
            tabAllRequest.Attributes.CssStyle.Add("display", "none");
            tabReceivingEntry.Attributes.CssStyle.Add("display", "block");
            divSendToSupplier.Attributes.CssStyle.Add("display", "none");
            tabForApproval.Attributes.CssStyle.Add("display", "none");
            tabSuccess.Attributes.CssStyle.Add("display", "none");
            //ddType.Items.FindByText("FOR SENDING").Selected = true;

            btnAllRequest.Attributes.CssStyle.Add("color", "white");
            btnReceivingResend.Attributes.CssStyle.Add("color", "white");
            btnReceivingSending.Attributes.CssStyle.Add("color", "black");
            btnApproval.Attributes.CssStyle.Add("color", "white");
            btnHomePage.Attributes.CssStyle.Add("color", "white");

            lblType.Text = "FOR SENDING".Trim();
            txtSearchReceiving.Text = string.Empty;
            LoadDefaultReceiving();
        }
        protected void btnReceivingResend_Click(object sender, EventArgs e)
        {
            tabAllRequest.Attributes.CssStyle.Add("display", "none");
            tabReceivingEntry.Attributes.CssStyle.Add("display", "block");
            divSendToSupplier.Attributes.CssStyle.Add("display", "none");
            tabForApproval.Attributes.CssStyle.Add("display", "none");
            tabSuccess.Attributes.CssStyle.Add("display", "none");

            btnAllRequest.Attributes.CssStyle.Add("color", "white");
            btnReceivingResend.Attributes.CssStyle.Add("color", "black");
            btnReceivingSending.Attributes.CssStyle.Add("color", "white");
            btnApproval.Attributes.CssStyle.Add("color", "white");
            btnHomePage.Attributes.CssStyle.Add("color", "white");

            //ddType.Items.FindByText("FOR RESEND").Selected = true;
            lblType.Text = "FOR RESEND".Trim();
            txtSearchReceiving.Text = string.Empty;
            LoadDefaultReceiving();
        }
        protected void btnApproval_Click(object sender, EventArgs e)
        {
            tabAllRequest.Attributes.CssStyle.Add("display", "none");
            tabReceivingEntry.Attributes.CssStyle.Add("display", "none");
            divSendToSupplier.Attributes.CssStyle.Add("display", "none");
            tabForApproval.Attributes.CssStyle.Add("display", "block");
            tabSuccess.Attributes.CssStyle.Add("display", "none");

            btnAllRequest.Attributes.CssStyle.Add("color", "white");
            btnReceivingResend.Attributes.CssStyle.Add("color", "white");
            btnReceivingSending.Attributes.CssStyle.Add("color", "white");
            btnApproval.Attributes.CssStyle.Add("color", "black");
            btnHomePage.Attributes.CssStyle.Add("color", "white");
            LoadDefaultForApproval();

        }

        protected void btnHomePage_Click(object sender, EventArgs e)
        {
            btnAllRequest.Attributes.CssStyle.Add("color", "white");
            btnReceivingResend.Attributes.CssStyle.Add("color", "white");
            btnReceivingSending.Attributes.CssStyle.Add("color", "white");
            btnApproval.Attributes.CssStyle.Add("color", "white");
            btnHomePage.Attributes.CssStyle.Add("color", "black");

            Response.Redirect("Dashboard.aspx");
        }

        protected void btnSubmitSuccess_Click(object sender, EventArgs e)
        {
            btnApproval_Click(sender, e);
            btnSubmitForApproval_Click(sender, e);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                list = null;

                entity.SearchCriteria = txtSearch.Text;

                if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()))
                {
                    list = BLL.RFQ_TRANSACTION_AllRequest_ByDateRange(entity).Take(500).ToList();
                }
                else
                {
                    list = BLL.RFQ_TRANSACTION_AllRequest_ByDateRange_AllApproved(entity);
                }

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        tabAllRequest.Attributes.CssStyle.Add("display", "block");
                        tabReceivingEntry.Attributes.CssStyle.Add("display", "none");
                        divSendToSupplier.Attributes.CssStyle.Add("display", "none");
                        tabForApproval.Attributes.CssStyle.Add("display", "none");
                        tabSuccess.Attributes.CssStyle.Add("display", "none");

                        btnAllRequest.Attributes.CssStyle.Add("color", "black");
                        btnReceivingResend.Attributes.CssStyle.Add("color", "white");
                        btnReceivingSending.Attributes.CssStyle.Add("color", "white");
                        btnApproval.Attributes.CssStyle.Add("color", "white");
                        btnHomePage.Attributes.CssStyle.Add("color", "white");

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

                divOpacity.Style.Add("opacity", "1");


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        private void LoadDefaultAllRequest()
        {
            try
            {

                List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                list = null;

                entity.SearchCriteria = txtSearch.Text;

                if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()))
                {
                    list = BLL.RFQ_TRANSACTION_AllRequest_ByDateRange(entity).Take(500).ToList();
                }
                else
                {
                    list = BLL.RFQ_TRANSACTION_AllRequest_ByDateRange_AllApproved(entity);
                }

                if (list != null)
                {
                    if (list.Count > 0)
                    {

                        tabAllRequest.Attributes.CssStyle.Add("display", "block");
                        tabReceivingEntry.Attributes.CssStyle.Add("display", "none");
                        divSendToSupplier.Attributes.CssStyle.Add("display", "none");
                        tabForApproval.Attributes.CssStyle.Add("display", "none");
                        tabSuccess.Attributes.CssStyle.Add("display", "none");

                        btnAllRequest.Attributes.CssStyle.Add("color", "black");
                        btnReceivingResend.Attributes.CssStyle.Add("color", "white");
                        btnReceivingSending.Attributes.CssStyle.Add("color", "white");
                        btnApproval.Attributes.CssStyle.Add("color", "white");
                        btnHomePage.Attributes.CssStyle.Add("color", "white");

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

                divOpacity.Style.Add("opacity", "1");


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

                LinkButton linkRFQNo = row.FindControl("linkRFQNo") as LinkButton;
                Label lblRequester = row.FindControl("lblRequester") as Label;
                Label lblTransactionDate = row.FindControl("lblTransactionDate") as Label;
                Label lblCategoryName = row.FindControl("lblCategoryName") as Label;
                Label lblStatDivManager = row.FindControl("lblStatDivManager") as Label;
                Button btnReceiving = row.FindControl("btnReceiving") as Button;
                Button btnApproval = row.FindControl("btnApproval") as Button;
                Button btnPreview = row.FindControl("btnPreview") as Button;
                Label lblCategory = row.FindControl("lblCategory") as Label;
                GridView gvResponseAllRequest = row.FindControl("gvResponseAllRequest") as GridView;
                GridView gvSuppliers = row.FindControl("gvSuppliers") as GridView;
                GridView gvSupplierAttachment = row.FindControl("gvSupplierAttachment") as GridView;
                Label lblAllRequestSupplierHistory = row.FindControl("lblAllRequestSupplierHistory") as Label;
                Label lblResponseAllRequest = row.FindControl("lblResponseAllRequest") as Label;
                Label lblSendDates = row.FindControl("lblSendDates") as Label;
                Label lblSupplierAttachment = row.FindControl("lblSupplierAttachment") as Label;
                DropDownList ddSendDates = row.FindControl("ddSendDates") as DropDownList;
                Label lblSection = row.FindControl("lblSection") as Label;
                Label lblDepartment = row.FindControl("lblDepartment") as Label;
                Label lblDivision = row.FindControl("lblDivision") as Label;
                Label lblRequesterAttachment = row.FindControl("lblRequesterAttachment") as Label;
                GridView gvDataRequesterAttachment = row.FindControl("gvDataRequesterAttachment") as GridView;
                Label lblRequestDetails = row.FindControl("lblRequestDetails") as Label;


                if (e.CommandName == "linkRFQNo_Command")
                {
                    lblRequestDetails.Visible = true;
                    lblRequestDetails.Text = "REQUESTER : " + lblRequester.Text.ToUpper() + "<br/>" +
                                             "SECTION : " + lblSection.Text.ToUpper() + "<br/>" +
                                             "DEPARTMENT : " + lblDepartment.Text.ToUpper() + "<br/>" +
                                             "DIVISION : " + lblDivision.Text.ToUpper();


                    List<Entities_RFQ_RequestEntry> listRequesterAttachment = new List<Entities_RFQ_RequestEntry>();
                    listRequesterAttachment = BLL.RFQ_TRANSACTION_GetAttachment_ByRFQNo(linkRFQNo.Text.Trim());

                    if (listRequesterAttachment != null)
                    {
                        if (listRequesterAttachment.Count > 0)
                        {
                            lblRequesterAttachment.Visible = true;
                            gvDataRequesterAttachment.Visible = true;
                            gvDataRequesterAttachment.DataSource = listRequesterAttachment;
                            gvDataRequesterAttachment.DataBind();
                        }
                    }


                    List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                    list = BLL.RFQ_TRANSACTION_GetSupplierResponseByRFQNo(linkRFQNo.Text.Trim());

                    if (list != null)
                    {
                        if (list.Count > 0)
                        {
                            gvResponseAllRequest.Visible = true;
                            lblResponseAllRequest.Visible = true;                            
                            gvResponseAllRequest.DataSource = list;
                            gvResponseAllRequest.DataBind();
                        }
                    }
                    else
                    {
                        gvResponseAllRequest.EmptyDataText = "NO SUPPLIER RESPONSE";
                    }

                    //---------------------------------------------------------------------------------------------------

                    List<Entities_RFQ_RequestEntry> listRespondedSupplier = new List<Entities_RFQ_RequestEntry>();
                    listRespondedSupplier = BLL.RFQ_TRANSACTION_GetRespondedSupplierByRFQNo(linkRFQNo.Text.Trim());

                    if (listRespondedSupplier != null)
                    {
                        if (listRespondedSupplier.Count > 0)
                        {
                            gvSuppliers.Visible = true;
                            lblAllRequestSupplierHistory.Visible = true;
                            gvSuppliers.DataSource = listRespondedSupplier;
                            gvSuppliers.DataBind();
                        }
                    }

                    //---------------------------------------------------------------------------------------------------

                    //---------------------------------------------------------------------------------------------------
                    List<Entities_RFQ_RequestEntry> List_SendDates = new List<Entities_RFQ_RequestEntry>();
                    List_SendDates = BLL.RFQ_TRANSACTION_SendDate_ByRFQNo(linkRFQNo.Text.Trim());

                    if (List_SendDates != null)
                    {
                        if (List_SendDates.Count > 0)
                        {
                            lblSendDates.Visible = true;
                            ddSendDates.Visible = true;
                            ddSendDates.Items.Clear();

                            foreach (Entities_RFQ_RequestEntry entity in List_SendDates)
                            {
                                ddSendDates.Items.Add(entity.SendDate);
                            }
                        }
                    }

                    //--------------------------------------------------------------------------------------------------- 

                    // SUPPLIER ATTACHMENT

                    List<Entities_RFQ_RequestEntry> listSupplierAttachment = new List<Entities_RFQ_RequestEntry>();
                    Entities_RFQ_RequestEntry entitySupplierAttachment = new Entities_RFQ_RequestEntry();

                    entitySupplierAttachment.ResponseRFQNo = linkRFQNo.Text.Trim();

                    listSupplierAttachment = BLL.RFQ_TRANSACTION_GetSupplierAttachmentByRFQNo(entitySupplierAttachment);

                    if (listSupplierAttachment != null)
                    {
                        if (listSupplierAttachment.Count > 0)
                        {

                            gvSupplierAttachment.DataSource = listSupplierAttachment;
                            gvSupplierAttachment.DataBind();
                            gvSupplierAttachment.Visible = true;
                            lblSupplierAttachment.Visible = true;
                        }
                    }
                    //--------------------------------------------------------------------------------------------------- 

                    divOpacity.Style.Add("opacity", "1");

                }
                if (e.CommandName == "btnReceiving_Command")
                {
                    if (btnReceiving.Text == "FOR SENDING")
                    {
                        if (!string.IsNullOrEmpty(Session["CategoryAccess"].ToString()))
                        {
                            if (lblCategory.Text.Trim() == Session["CategoryAccess"].ToString().Trim())
                            {
                                tabAllRequest.Attributes.CssStyle.Add("display", "none");
                                tabReceivingEntry.Attributes.CssStyle.Add("display", "block");

                                btnAllRequest.Attributes.CssStyle.Add("color", "white");
                                btnReceivingResend.Attributes.CssStyle.Add("color", "white");
                                btnReceivingSending.Attributes.CssStyle.Add("color", "black");
                                btnApproval.Attributes.CssStyle.Add("color", "white");
                                btnHomePage.Attributes.CssStyle.Add("color", "white");

                                lblType.Text = "FOR SENDING".Trim();
                                txtSearchReceiving.Text = linkRFQNo.Text.Trim();
                                LoadDefaultReceiving();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('ACCESS DENIED! This is not your category access.');", true);
                            }
                        }
                    }
                    if (btnReceiving.Text == "FOR RESEND")
                    {
                        if (!string.IsNullOrEmpty(Session["CategoryAccess"].ToString()))
                        {
                            if (lblCategory.Text.Trim() == Session["CategoryAccess"].ToString().Trim())
                            {
                                tabAllRequest.Attributes.CssStyle.Add("display", "none");
                                tabReceivingEntry.Attributes.CssStyle.Add("display", "block");

                                btnAllRequest.Attributes.CssStyle.Add("color", "white");
                                btnReceivingResend.Attributes.CssStyle.Add("color", "black");
                                btnReceivingSending.Attributes.CssStyle.Add("color", "white");
                                btnApproval.Attributes.CssStyle.Add("color", "white");
                                btnHomePage.Attributes.CssStyle.Add("color", "white");

                                lblType.Text = "FOR RESEND".Trim();
                                txtSearchReceiving.Text = linkRFQNo.Text.Trim();
                                LoadDefaultReceiving();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('ACCESS DENIED! This is not your category access.');", true);
                            }
                        }
                        
                    }
                }

                if (e.CommandName == "btnApproval_Command")
                {
                    if (!string.IsNullOrEmpty(Session["CategoryAccess"].ToString()))
                    {
                        if (int.Parse(Session["CategoryAccess"].ToString().Trim()) > 0)
                        {
                            if (lblCategory.Text.Trim() == Session["CategoryAccess"].ToString().Trim())
                            {
                                tabAllRequest.Attributes.CssStyle.Add("display", "none");
                                tabReceivingEntry.Attributes.CssStyle.Add("display", "none");
                                divSendToSupplier.Attributes.CssStyle.Add("display", "none");
                                tabForApproval.Attributes.CssStyle.Add("display", "block");
                                tabSuccess.Attributes.CssStyle.Add("display", "none");

                                btnAllRequest.Attributes.CssStyle.Add("color", "white");
                                btnReceivingResend.Attributes.CssStyle.Add("color", "white");
                                btnReceivingSending.Attributes.CssStyle.Add("color", "white");
                                btnApproval.Attributes.CssStyle.Add("color", "black");
                                btnHomePage.Attributes.CssStyle.Add("color", "white");

                                txtRFQNo.Text = linkRFQNo.Text.Trim();
                                LoadDefaultForApproval();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('ACCESS DENIED! This is not your category access.');", true);
                            }
                        }
                        else
                        {
                            tabAllRequest.Attributes.CssStyle.Add("display", "none");
                            tabReceivingEntry.Attributes.CssStyle.Add("display", "none");
                            divSendToSupplier.Attributes.CssStyle.Add("display", "none");
                            tabForApproval.Attributes.CssStyle.Add("display", "block");
                            tabSuccess.Attributes.CssStyle.Add("display", "none");

                            btnAllRequest.Attributes.CssStyle.Add("color", "white");
                            btnReceivingResend.Attributes.CssStyle.Add("color", "white");
                            btnReceivingSending.Attributes.CssStyle.Add("color", "white");
                            btnApproval.Attributes.CssStyle.Add("color", "black");
                            btnHomePage.Attributes.CssStyle.Add("color", "white");

                            txtRFQNo.Text = linkRFQNo.Text.Trim();
                            LoadDefaultForApproval();
                        }
                    }
                }

                if (e.CommandName == "btnPreview_Command")
                {
                    Session["Preview_RFQNo"] = linkRFQNo.Text.Trim().ToUpper();
                    Session["Preview_Requester"] = lblRequester.Text.ToUpper();
                    Session["Preview_DOARequester"] = lblTransactionDate.Text.ToUpper();

                    Session["SectionName"] = lblSection.Text.ToUpper();
                    Session["DepartmentName"] = lblDepartment.Text.ToUpper();
                    Session["DivisionName"] = lblDivision.Text.ToUpper();

                    //---------------------------------------------------------------------------------------------------
                    List<Entities_RFQ_RequestEntry> List_Approval = new List<Entities_RFQ_RequestEntry>();
                    List<Entities_RFQ_RequestEntry> List_Disapproval = new List<Entities_RFQ_RequestEntry>();
                    List<Entities_RFQ_RequestEntry> List_AD_Consolidated = new List<Entities_RFQ_RequestEntry>();

                    List_Disapproval = BLL.RFQ_TRANSACTION_HistoryOfDisapproval_ByRFQNo(linkRFQNo.Text.Trim());
                    List_Approval = BLL.RFQ_TRANSACTION_HistoryOfApproval_ByRFQNo(linkRFQNo.Text.Trim());

                    if (List_Approval != null)
                    {
                        if (List_Approval.Count > 0)
                        {
                            foreach (Entities_RFQ_RequestEntry entity in List_Approval)
                            {
                                List_AD_Consolidated.Add(entity);
                            }
                        }
                    }
                    if (List_Disapproval != null)
                    {
                        if (List_Disapproval.Count > 0)
                        {
                            foreach (Entities_RFQ_RequestEntry entity in List_Disapproval)
                            {
                                List_AD_Consolidated.Add(entity);
                            }
                        }
                    }

                    if (List_AD_Consolidated != null)
                    {
                        if (List_AD_Consolidated.Count > 0)
                        {
                            foreach (Entities_RFQ_RequestEntry entity in List_AD_Consolidated)
                            {
                                
                                if (entity.TransactionName == ConfigurationManager.AppSettings["ApprovedDisapprovedName-ProdManager"])
                                {
                                    Session["Preview_ProdManager"] = entity.ApprovedBy.ToUpper();
                                    Session["Preview_DOAProdManager"] = entity.ApprovedDate.ToUpper();
                                }
                                if (entity.TransactionName == ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingBuyer"])
                                {
                                    Session["Preview_Buyer"] = entity.ApprovedBy.ToUpper();
                                    Session["Preview_DOABuyer"] = entity.ApprovedDate.ToUpper();
                                }
                                if (entity.TransactionName == ConfigurationManager.AppSettings["ApprovedDisapprovedName-Purchasing"])
                                {
                                    Session["Preview_BuyerSend"] = entity.ApprovedBy.ToUpper();
                                    Session["Preview_DOABuyerSend"] = entity.ApprovedDate.ToUpper();

                                }
                                if (entity.TransactionName == ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingIncharge"])
                                {
                                    Session["Preview_Incharge"] = entity.ApprovedBy.ToUpper();
                                    Session["Preview_DOAIncharge"] = entity.ApprovedDate.ToUpper();
                                }
                                if (entity.TransactionName == ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingDeptManager"])
                                {
                                    Session["Preview_DeptManager"] = entity.ApprovedBy.ToUpper();
                                    Session["Preview_DOADeptManager"] = entity.ApprovedDate.ToUpper();
                                }
                                if (entity.TransactionName == ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingDivManager"])
                                {
                                    Session["Preview_DivManager"] = entity.ApprovedBy.ToUpper();
                                    Session["Preview_DOADivManager"] = entity.ApprovedDate.ToUpper();
                                }
                            }
                        }
                    }

                    //---------------------------------------------------------------------------------------------------

                    string URL = "~/RFQ_RequestPreview.aspx";

                    Session["Requester_From_Inquiry"] = lblRequester.Text;
                    Session["TransDate_From_Inquiry"] = lblTransactionDate.Text;
                    Session["Category_From_Inquiry"] = lblCategory.Text.Trim().ToUpper();
                    Session["btnPreview_Visibility"] = "false";
                    Session["btnUpdate_Visibility"] = "true";
                    Session["divSupplier_Visibility"] = "true";
                    Session["From_OnePage"] = "true";

                    URL = Page.ResolveClientUrl(URL);
                    btnPreview.OnClientClick = "window.open('" + URL + "'); return false;";
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
                    Button btnReceiving = (Button)e.Row.FindControl("btnReceiving");
                    Button btnApproval = (Button)e.Row.FindControl("btnApproval");
                    Button btnPreview = (Button)e.Row.FindControl("btnPreview");
                    DropDownList ddRespondedSupplier = (DropDownList)e.Row.FindControl("ddRespondedSupplier");
                    LinkButton linkRFQNo = (LinkButton)e.Row.FindControl("linkRFQNo");


                    List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                    list = BLL.RFQ_TRANSACTION_GetSupplierResponseByRFQNoSupplierNameOnly(linkRFQNo.Text.Trim());

                    if (list != null)
                    {
                        ddRespondedSupplier.Items.Clear();
                        if (list.Count > 0)
                        {
                            foreach (Entities_RFQ_RequestEntry entity in list)
                            {
                                ddRespondedSupplier.Items.Add(entity.ResponseSupplierName);
                            }
                        }
                    }

                    lblStatAll.Style.Add("background-color", lblStatColor.Text.Trim());

                    if (lblStatAll.Text.Trim() == "FOR PRODUCTION MANAGER APPROVAL")
                    {
                        btnReceiving.Visible = false;
                        btnApproval.Visible = false;
                        btnPreview.Visible = false;
                    }
                    if (lblStatAll.Text.Trim() == "FOR BUYER APPROVAL")
                    {
                        btnReceiving.Text = "FOR SENDING";
                        btnApproval.Text = "";
                        btnApproval.Visible = false;
                        btnPreview.Visible = false;
                    }
                    if (lblStatAll.Text.Trim() == "QUOTATION SENT TO SUPPLIER(S)")
                    {
                        btnReceiving.Text = "FOR RESEND";
                        btnApproval.Text = "";
                        btnApproval.Visible = false;
                        btnPreview.Visible = false;
                    }
                    if (lblStatAll.Text.Trim() == "SUPPLIER RESPONDED / FOR BUYER APPROVAL")
                    {
                        btnReceiving.Text = "FOR RESEND";
                        btnApproval.Text = "FOR APPROVAL";
                        btnPreview.Visible = false;
                    }
                    if (lblStatAll.Text.Trim() == "FOR PURCHASING INCHARGE APPROVAL")
                    {
                        btnReceiving.Text = "";
                        btnApproval.Text = "FOR APPROVAL";
                        btnReceiving.Visible = false;
                        btnPreview.Visible = false;
                    }
                    if (lblStatAll.Text.Trim() == "FOR PURCHASING DEPARTMENT MANAGER APPROVAL")
                    {
                        btnReceiving.Text = "";
                        btnApproval.Text = "FOR APPROVAL";
                        btnReceiving.Visible = false;
                        btnPreview.Visible = false;
                    }
                    if (lblStatAll.Text.Trim() == "FOR PURCHASING DIVISION MANAGER APPROVAL")
                    {
                        btnReceiving.Text = "";
                        btnApproval.Text = "FOR APPROVAL";
                        btnReceiving.Visible = false;
                        btnPreview.Visible = false;
                    }
                    if (lblStatAll.Text.Trim() == "APPROVED")
                    {
                        btnReceiving.Visible = false;
                        btnApproval.Visible = false;
                        btnPreview.Visible = true;
                    }
                    if (lblStatAll.Text.Trim() == "DISAPPROVED")
                    {
                        btnReceiving.Visible = false;
                        btnApproval.Visible = false;
                        btnPreview.Visible = false;
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

        //protected void gvResponseAllRequest_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //     try
        //    {
        //        int index = int.Parse(e.CommandArgument.ToString());
        //        GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

        //        Label lblResponseSupplierID = row.FindControl("lblResponseSupplierID") as Label;
        //        Label lblResponseRFQNo = row.FindControl("lblResponseRFQNo") as Label;

        //        if (e.CommandName == "Attachment_Command")
        //        {
        //            List<Entities_RFQ_RequestEntry> listAttachment = new List<Entities_RFQ_RequestEntry>();
        //            Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

        //            entity.ResponseSupplierID = lblResponseSupplierID.Text.Trim();
        //            entity.ResponseRFQNo = lblResponseRFQNo.Text.Trim();

        //            listAttachment = BLL.RFQ_TRANSACTION_GetSupplierAttachmentBySupplierIdAndRFQNo(entity);

        //                if (listAttachment != null)
        //                {
        //                    if (listAttachment.Count > 0)
        //                    {
        //                        int attachCounter = 1;
        //                        string cssTabPane = string.Empty;

        //                        string temp_cssTabPane = string.Empty;
        //                        string temp_tabAttachment = string.Empty;
        //                        string temp_tabAttachmentContents = string.Empty;

        //                        foreach (Entities_RFQ_RequestEntry attachment in listAttachment)
        //                        {

        //                            temp_tabAttachmentContents += "<iframe runat='server' id='iframepdf" + attachCounter.ToString() + "' height='800' width='980' frameborder='0' src='/IO_Received/" + (lblResponseSupplierID.Text.Trim() + "_" + lblResponseRFQNo.Text.Trim()) + "/" + attachment.ResponseSupplierAttachment.Replace("#", "%23").ToString() + "'> </iframe>";
        //                            attachCounter++;                                        

        //                        }

        //                        if (!string.IsNullOrEmpty(temp_tabAttachment))
        //                        {
        //                            tabAttachmentSupplier = temp_tabAttachment;
        //                            tabAttachmentContentsSupplier = temp_tabAttachmentContents;
        //                        }
                                
        //                    }
        //                }


        //            }
                    
        //        }
            
        //     catch (Exception ex)
        //     {
        //         ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
        //     }
        //}
        protected void gvResponseAllRequest_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblResponseDescription = (Label)e.Row.FindControl("lblResponseDescription");
                    Label lblResponseSupplier = (Label)e.Row.FindControl("lblResponseSupplier");
                    Label lblDetailsRefId = (Label)e.Row.FindControl("lblDetailsRefId");
                    Label lblIsGranted = (Label)e.Row.FindControl("lblIsGranted");
                    ImageButton ibApprovedResponse = (ImageButton)e.Row.FindControl("ibApprovedResponse");
                    DropDownList ddResponseCurrency = (DropDownList)e.Row.FindControl("ddResponseCurrency");
                    Label lblResponseCurrency = (Label)e.Row.FindControl("lblResponseCurrency");
                    TextBox txtResponseSupplierRemarks = (TextBox)e.Row.FindControl("txtResponseSupplierRemarks");
                    TextBox txtResponsePurchasingRemarks = (TextBox)e.Row.FindControl("txtResponsePurchasingRemarks");
                    Label lblResponseSupplierID = (Label)e.Row.FindControl("lblResponseSupplierID");
                    Label lblResponseRFQNo = (Label)e.Row.FindControl("lblResponseRFQNo");
                    //LinkButton lbSupplierAttachment = (LinkButton)e.Row.FindControl("lbSupplierAttachment");

                    List<Entities_RFQ_Currency> listCurrency = new List<Entities_RFQ_Currency>();
                    listCurrency = BLL.RFQ_MT_Currency_GetAll();

                    if (listCurrency != null)
                    {
                        if (listCurrency.Count > 0)
                        {
                            ddResponseCurrency.Items.Add("");
                            foreach (Entities_RFQ_Currency entity in listCurrency)
                            {
                                ListItem item = new ListItem();
                                item.Text = entity.Code;
                                item.Value = entity.Code;

                                if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                                {
                                    ddResponseCurrency.Items.Add(item);
                                }
                            }

                            if (!string.IsNullOrEmpty(lblResponseCurrency.Text.Trim()))
                            {
                                ddResponseCurrency.Items.FindByText(lblResponseCurrency.Text.Trim()).Selected = true;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(lblResponseDescription.Text))
                    {
                        if (lblResponseDescription.Text.Length > 36)
                        {
                            int supRemLen = lblResponseDescription.Text.Length;
                            int divisor = 36;
                            int height = ((supRemLen / divisor) * 20) + 10;

                            lblResponseDescription.Style.Add("Height", height.ToString() + "px");
                        }
                    }
                    //if (!string.IsNullOrEmpty(lblResponseSupplier.Text))
                    //{
                    //    lblResponseSupplier.Text = lblResponseSupplier.Text.Length > 17 ? lblResponseSupplier.Text.Substring(0, 17).ToString() + "..." : lblResponseSupplier.Text;
                    //}

                    foreach (TableCell cell in e.Row.Cells)
                    {
                        if (IsOdd(long.Parse(lblDetailsRefId.Text.Trim())))
                        {
                            cell.Style.Add("background-color", "#98FB98");
                        }
                        else
                        {
                            cell.Style.Add("background-color", "#FFA07A");
                        }
                    }

                    if (lblIsGranted.Text.Trim() == "1")
                    {
                        ibApprovedResponse.ImageUrl = "~/images/A2.png";
                    }

                    if (!string.IsNullOrEmpty(txtResponseSupplierRemarks.Text))
                    {
                        if (txtResponseSupplierRemarks.Text.Length > 30)
                        {
                            int supRemLen = txtResponseSupplierRemarks.Text.Length;
                            int divisor = 30;
                            int height = ((supRemLen / divisor) * 20) + 30;

                            txtResponseSupplierRemarks.Style.Add("Height", height.ToString() + "px");
                        }
                    }

                    if (!string.IsNullOrEmpty(txtResponsePurchasingRemarks.Text))
                    {
                        if (txtResponsePurchasingRemarks.Text.Length > 30)
                        {
                            int supRemLen2 = txtResponsePurchasingRemarks.Text.Length;
                            int divisor2 = 30;
                            int height2 = ((supRemLen2 / divisor2) * 20) + 30;

                            txtResponsePurchasingRemarks.Style.Add("Height", height2.ToString() + "px");
                        }
                    }


                    //// SUPPLIER ATTACHMENT 
                    //List<Entities_RFQ_RequestEntry> listAttachment = new List<Entities_RFQ_RequestEntry>();
                    //Entities_RFQ_RequestEntry entityAttachment = new Entities_RFQ_RequestEntry();

                    //entityAttachment.ResponseSupplierID = lblResponseSupplierID.Text.Trim();
                    //entityAttachment.ResponseRFQNo = lblResponseRFQNo.Text.Trim();

                    //listAttachment = BLL.RFQ_TRANSACTION_GetSupplierAttachmentBySupplierIdAndRFQNo(entityAttachment);

                    //if (listAttachment != null)
                    //{
                    //    if (listAttachment.Count > 0)
                    //    {
                    //        lbSupplierAttachment.Visible = true;
                    //    }
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


        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        tabAllRequest.Attributes.Add("display", "block");
        //        tabReceivingEntry.Attributes.Add("display", "none");

        //        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
        //        Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

        //        if (ddCriteria.SelectedValue == "1")
        //        {
        //            list = BLL.RFQ_TRANSACTION_OnePage_Head(entity).Where(itm => itm.RhRfqNo.Contains(txtSearchItem.Text.ToUpper())).ToList().Take(500).ToList();
        //        }
        //        if (ddCriteria.SelectedValue == "2")
        //        {
        //            list = BLL.RFQ_TRANSACTION_OnePage_Head(entity).Where(itm => itm.RhSection.Contains(txtSearchItem.Text.ToUpper())).ToList().Take(500).ToList();
        //        }
        //        if (ddCriteria.SelectedValue == "3")
        //        {
        //            list = BLL.RFQ_TRANSACTION_OnePage_Head(entity).Where(itm => itm.RhDepartment.Contains(txtSearchItem.Text.ToUpper())).ToList().Take(500).ToList();
        //        }
        //        if (ddCriteria.SelectedValue == "4")
        //        {
        //            list = BLL.RFQ_TRANSACTION_OnePage_Head(entity).Where(itm => itm.RhDivision.Contains(txtSearchItem.Text.ToUpper())).ToList().Take(500).ToList();
        //        }
        //        if (ddCriteria.SelectedValue == "5")
        //        {
        //            list = BLL.RFQ_TRANSACTION_OnePage_Head(entity).Where(itm => itm.RhRequester.Contains(txtSearchItem.Text.ToUpper())).ToList().Take(500).ToList();
        //        }
        //        if (ddCriteria.SelectedValue == "6")
        //        {
        //            List<Entities_RFQ_RequestEntry> listDescriptionFinal = new List<Entities_RFQ_RequestEntry>();
        //            List<Entities_RFQ_RequestEntry> listDescription = new List<Entities_RFQ_RequestEntry>();
        //            Entities_RFQ_RequestEntry entityDescription = new Entities_RFQ_RequestEntry();

        //            listDescription = null;

        //            entityDescription.SearchCriteria = txtSearchItem.Text;
        //            listDescription = BLL.RFQ_TRANSACTION_OnePage_Head_Description(entityDescription).Take(500).ToList();

        //            if (listDescription != null)
        //            {
        //                if (listDescription.Count > 0)
        //                {
        //                    foreach (Entities_RFQ_RequestEntry eDescription in listDescription)
        //                    {
        //                        listDescriptionFinal = BLL.RFQ_TRANSACTION_OnePage_Head(entity).Where(itm => itm.RhRfqNo.Trim() == eDescription.RdRfqNo.Trim()).ToList();

        //                        if (listDescriptionFinal.Count > 0)
        //                        {
        //                            foreach (Entities_RFQ_RequestEntry eDescFinal in listDescriptionFinal)
        //                            {
        //                                list.Add(eDescFinal);
        //                            }
        //                        }

        //                    }
        //                }
        //            }

        //        }

        //        if (ddCriteria.SelectedValue == "7")
        //        {
        //            List<Entities_RFQ_RequestEntry> listSpecsFinal = new List<Entities_RFQ_RequestEntry>();
        //            List<Entities_RFQ_RequestEntry> listSpecs = new List<Entities_RFQ_RequestEntry>();
        //            Entities_RFQ_RequestEntry entitySpecs = new Entities_RFQ_RequestEntry();

        //            listSpecs = null;

        //            entitySpecs.SearchCriteria = txtSearchItem.Text;
        //            listSpecs = BLL.RFQ_TRANSACTION_OnePage_Head_Specification(entitySpecs).Take(500).ToList();

        //            if (listSpecs != null)
        //            {
        //                if (listSpecs.Count > 0)
        //                {
        //                    foreach (Entities_RFQ_RequestEntry eSpecs in listSpecs)
        //                    {
        //                        listSpecsFinal = BLL.RFQ_TRANSACTION_OnePage_Head(entity).Where(itm => itm.RhRfqNo.Trim() == eSpecs.RdRfqNo.Trim()).ToList();

        //                        if (listSpecsFinal.Count > 0)
        //                        {
        //                            foreach (Entities_RFQ_RequestEntry eSpecsFinal in listSpecsFinal)
        //                            {
        //                                list.Add(eSpecsFinal);
        //                            }
        //                        }

        //                    }
        //                }
        //            }

        //        }

        //        if (ddCriteria.SelectedValue == "8")
        //        {
        //            List<Entities_RFQ_RequestEntry> listMakerFinal = new List<Entities_RFQ_RequestEntry>();
        //            List<Entities_RFQ_RequestEntry> listMaker = new List<Entities_RFQ_RequestEntry>();
        //            Entities_RFQ_RequestEntry entityMaker = new Entities_RFQ_RequestEntry();

        //            listMaker = null;

        //            entityMaker.SearchCriteria = txtSearchItem.Text;
        //            listMaker = BLL.RFQ_TRANSACTION_OnePage_Head_Maker(entityMaker).Take(500).ToList();

        //            if (listMaker != null)
        //            {
        //                if (listMaker.Count > 0)
        //                {
        //                    foreach (Entities_RFQ_RequestEntry eMaker in listMaker)
        //                    {
        //                        listMakerFinal = BLL.RFQ_TRANSACTION_OnePage_Head(entity).Where(itm => itm.RhRfqNo.Trim() == eMaker.RdRfqNo.Trim()).ToList();

        //                        if (listMakerFinal.Count > 0)
        //                        {
        //                            foreach (Entities_RFQ_RequestEntry eMakerFinal in listMakerFinal)
        //                            {
        //                                list.Add(eMakerFinal);
        //                            }
        //                        }

        //                    }
        //                }
        //            }

        //        }


        //        if (list != null)
        //        {
        //            if (list.Count > 0)
        //            {
        //                gvData.DataSource = list.Take(500).ToList();
        //                gvData.DataBind();
        //                gvData.Visible = true;
        //            }
        //            else
        //            {
        //                gvData.Visible = false;
        //            }
        //        }
        //        else
        //        {
        //            gvData.Visible = false;
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
        //    }
        //}


        

        //protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        //int index = int.Parse(e.CommandArgument.ToString());
        //        //GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

        //        //LinkButton linkRFQNo = row.FindControl("linkRFQNo") as LinkButton;
        //        //Label lblRequester = row.FindControl("lblRequester") as Label;
        //        //Label lblTransactionDate = row.FindControl("lblTransactionDate") as Label;
        //        //Label lblCategoryName = row.FindControl("lblCategoryName") as Label;
        //        //Label lblStatDivManager = row.FindControl("lblStatDivManager") as Label;
        //        ////Label lblStatAll = row.FindControl("lblStatAll") as Label;


        //        //if (e.CommandName == "linkRFQNo_Command")
        //        //{
        //        //    //string URL = "~/RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(linkRFQNo.Text.Trim(), true);

        //        //    Session["Requester_From_Inquiry"] = lblRequester.Text;
        //        //    Session["TransDate_From_Inquiry"] = lblTransactionDate.Text;
        //        //    Session["Category_From_Inquiry"] = lblCategoryName.Text.Trim().ToUpper();
        //        //    Session["btnPreview_Visibility"] = lblStatDivManager.Text == "1" ? "true" : "false";
        //        //    Session["btnUpdate_Visibility"] = "false";
        //        //    if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()))
        //        //    {
        //        //        Session["divSupplier_Visibility"] = "true";
        //        //    }
        //        //    else
        //        //    {
        //        //        Session["divSupplier_Visibility"] = "false";
        //        //    }

        //        //    Response.Redirect("RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(linkRFQNo.Text.Trim(), true), false);

        //        //    //URL = Page.ResolveClientUrl(URL);
        //        //    //linkRFQNo.OnClientClick = "window.open('" + URL + "'); return false;";
        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
        //    }
        //}

        //protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {

        //            Label lblRFQNo = (Label)e.Row.FindControl("lblRFQNo");
        //            GridView gvDetails = (GridView)e.Row.FindControl("gvDetails");
        //            Label lblStatAll = (Label)e.Row.FindControl("lblStatAll");
        //            Label lblStatColor = (Label)e.Row.FindControl("lblStatColor");

        //            List<Entities_RFQ_RequestEntry> listDetails = new List<Entities_RFQ_RequestEntry>();
        //            listDetails = BLL.RFQ_TRANSACTION_Monitoring_GetDetailsByRFQNo(lblRFQNo.Text.Trim());

        //            if (listDetails != null)
        //            {
        //                if (listDetails.Count > 0)
        //                {
        //                    gvDetails.DataSource = listDetails;
        //                    gvDetails.DataBind();
        //                }
        //            }                    

        //            lblStatAll.Style.Add("background-color", lblStatColor.Text.Trim());
        //            lblStatAll.Text = " " + lblStatAll.Text;

        //            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
        //            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
        //    }
        //}

        protected void lbLogOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }


        //--------------------------------------------------------------------------------------------------------------------------------------------------------------
        // RECEIVING ENTRY


        private void LoadDefaultReceiving()
        {
            try
            {

                List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                entity.DrFrom = txtFrom.Text.Trim();
                entity.DrTo = txtTo.Text.Trim();
                entity.SearchCriteria = txtSearchReceiving.Text;

                if (!string.IsNullOrEmpty(txtSearchReceiving.Text))
                {
                    if (lblType.Text == "FOR SENDING".Trim())
                    {
                        list = BLL.RFQ_TRANSACTION_PurchasingReceiving(entity).Where(itm => itm.CntSuppResp == "0" && itm.StatDivManager == "0").ToList();
                    }
                    if (lblType.Text == "FOR RESEND".Trim())
                    {
                        list = BLL.RFQ_TRANSACTION_PurchasingReceiving(entity).Where(itm => int.Parse(itm.CntSuppResp) > 0 && itm.StatDivManager == "0").ToList();
                    }
                }
                else
                {
                    if (ddCategory.SelectedItem.Text.ToLower() == "all category")
                    {
                        if (lblType.Text.ToLower().Trim() == "for sending".Trim())
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange(entity).Where(itm => itm.CntSuppResp == "0" && itm.StatDivManager == "0").ToList();
                        }
                        if (lblType.Text.ToLower().Trim() == "for resend".Trim())
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange(entity).Where(itm => int.Parse(itm.CntSuppResp) > 0 && itm.StatDivManager == "0").ToList();
                        }
                        if (lblType.Text.ToLower().Trim() == "all".Trim())
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange(entity).Where(itm => itm.StatDivManager == "0").ToList().OrderBy(itm => itm.GroupBySupplierResponse).ToList();
                        }

                    }
                    else
                    {
                        if (lblType.Text.ToLower().Trim() == "for sending".Trim())
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange(entity).Where(itm => itm.RhCategory == ddCategory.SelectedItem.Text && itm.CntSuppResp == "0" && itm.StatBuyer == "0").ToList();
                        }
                        if (lblType.Text.ToLower().Trim() == "for resend".Trim())
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange(entity).Where(itm => itm.RhCategory == ddCategory.SelectedItem.Text && int.Parse(itm.CntSuppResp) > 0 && itm.StatBuyer == "0").ToList();
                        }
                        if (lblType.Text.ToLower().Trim() == "all".Trim())
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange(entity).Where(itm => itm.RhCategory == ddCategory.SelectedItem.Text && itm.StatBuyer == "0").ToList().OrderBy(itm => itm.GroupBySupplierResponse).ToList();
                        }

                    }

                }


                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        gvDataReceiving.Visible = true;
                        gvDataReceiving.DataSource = list;
                        gvDataReceiving.DataBind();
                    }
                    else
                    {
                        gvDataReceiving.Visible = false;
                    }
                }
                else
                {
                    gvDataReceiving.Visible = false;
                }

                divLoader.Style.Add("display", "none");

                divOpacity.Style.Add("opacity", "1");

                //---------------------------------------------------------------------------------------------------

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


        protected void btnSubmitReceiving_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = txtSearchReceiving.Text.Trim();
                btnSubmit_Click(sender, e);
                txtSearchReceiving.Text = string.Empty;

                //---------------------------------------------------------------------------------------------------                                
                ////// TEMPORARY DISABLED KO ITONG MAY 6 NA SLASH PARA MATEST UNG SA ALL REQUEST LAHAT BABAGSAK ANG SEARCH
                //////btnSubmitReceiving.Enabled = false;

                //////List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                //////Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                //////entity.DrFrom = txtFrom.Text.Trim();
                //////entity.DrTo = txtTo.Text.Trim();
                //////entity.SearchCriteria = txtSearchReceiving.Text;

                //////if (!string.IsNullOrEmpty(txtSearchReceiving.Text))
                //////{
                //////    if (lblType.Text == "FOR SENDING".Trim())
                //////    {
                //////        list = BLL.RFQ_TRANSACTION_PurchasingReceiving(entity).Where(itm => itm.CntSuppResp == "0" && itm.StatDivManager == "0").ToList();
                //////    }
                //////    if (lblType.Text == "FOR RESEND".Trim())
                //////    {
                //////        list = BLL.RFQ_TRANSACTION_PurchasingReceiving(entity).Where(itm => int.Parse(itm.CntSuppResp) > 0 && itm.StatDivManager == "0").ToList();
                //////    }
                //////}
                //////else
                //////{
                //////    if (ddCategory.SelectedItem.Text.ToLower() == "all category")
                //////    {
                //////        if (lblType.Text.ToLower().Trim() == "for sending".Trim())
                //////        {
                //////            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange(entity).Where(itm => itm.CntSuppResp == "0" && itm.StatDivManager == "0").ToList();
                //////        }
                //////        if (lblType.Text.ToLower().Trim() == "for resend".Trim())
                //////        {
                //////            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange(entity).Where(itm => int.Parse(itm.CntSuppResp) > 0 && itm.StatDivManager == "0").ToList();
                //////        }
                //////        if (lblType.Text.ToLower().Trim() == "all".Trim())
                //////        {
                //////            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange(entity).Where(itm => itm.StatDivManager == "0").ToList().OrderBy(itm => itm.GroupBySupplierResponse).ToList();
                //////        }

                //////    }
                //////    else
                //////    {
                //////        if (lblType.Text.ToLower().Trim() == "for sending".Trim())
                //////        {
                //////            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange(entity).Where(itm => itm.RhCategory == ddCategory.SelectedItem.Text && itm.CntSuppResp == "0" && itm.StatBuyer == "0").ToList();
                //////        }
                //////        if (lblType.Text.ToLower().Trim() == "for resend".Trim())
                //////        {
                //////            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange(entity).Where(itm => itm.RhCategory == ddCategory.SelectedItem.Text && int.Parse(itm.CntSuppResp) > 0 && itm.StatBuyer == "0").ToList();
                //////        }
                //////        if (lblType.Text.ToLower().Trim() == "all".Trim())
                //////        {
                //////            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange(entity).Where(itm => itm.RhCategory == ddCategory.SelectedItem.Text && itm.StatBuyer == "0").ToList().OrderBy(itm => itm.GroupBySupplierResponse).ToList();
                //////        }

                //////    }

                //////}


                //////if (list != null)
                //////{
                //////    if (list.Count > 0)
                //////    {
                //////        gvDataReceiving.Visible = true;
                //////        gvDataReceiving.DataSource = list;
                //////        gvDataReceiving.DataBind();
                //////    }
                //////    else
                //////    {
                //////        gvDataReceiving.Visible = false;
                //////    }
                //////}
                //////else
                //////{
                //////    gvDataReceiving.Visible = false;
                //////}

                //////divLoader.Style.Add("display", "none");

                //////btnSubmitReceiving.Enabled = true;

                //////divOpacity.Style.Add("opacity", "1");

                //---------------------------------------------------------------------------------------------------


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvDataReceiving_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                LinkButton lblCtrl = row.FindControl("lblCtrl") as LinkButton;
                ImageButton ibApproved = row.FindControl("ibApproved") as ImageButton;
                ImageButton ibDisapproved = row.FindControl("ibDisapproved") as ImageButton;
                ImageButton ibPreview = row.FindControl("ibPreview") as ImageButton;
                TextBox txtRemarks = row.FindControl("txtRemarks") as TextBox;
                Label lblRFQNo = row.FindControl("lblRFQNo") as Label;
                Label lblRequester = row.FindControl("lblRequester") as Label;
                Label lblTransDate2 = row.FindControl("lblTransDate2") as Label;
                Label lblCategory = row.FindControl("lblCategory") as Label;
                GridView gvDetailsReceiving = row.FindControl("gvDetailsReceiving") as GridView;
                GridView gvSupplierDetails = row.FindControl("gvSupplierDetails") as GridView;
                GridView gvStatus = row.FindControl("gvStatus") as GridView;
                HtmlGenericControl divDetails = row.FindControl("divDetails") as HtmlGenericControl;
                HtmlGenericControl divSupplierDetails = row.FindControl("divSupplierDetails") as HtmlGenericControl;
                HtmlGenericControl divStatus = row.FindControl("divStatus") as HtmlGenericControl;

                if (e.CommandName == "lblCtrl_Command")
                {
                    string URL = "~/PIPL_InvoiceDetails.aspx?ControlNo_From_Inquiry=" + CryptorEngine.Encrypt(lblCtrl.Text.Trim(), true);

                    URL = Page.ResolveClientUrl(URL);
                    lblCtrl.OnClientClick = "window.open('" + URL + "'); return true;";
                }
                if (e.CommandName == "A_Command")
                {
                    if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                    {
                        ibApproved.ImageUrl = "~/images/A1.png";
                    }
                    else
                    {
                        if (ibApproved.ImageUrl == "~/images/A1.png")
                        {
                            ibApproved.ImageUrl = "~/images/A2.png";

                            lblRFQNo.Style.Add("font-size", "14px");

                            divDetails.Style.Add("margin-top", "15px");
                            divSupplierDetails.Style.Add("margin-top", "5px");
                            divSupplierDetails.Style.Add("margin-bottom", "5px");
                            divStatus.Style.Add("margin-top", "5px");
                            divStatus.Style.Add("margin-bottom", "15px");

                            // -------------------------------------------------------------------------------------------

                            List<Entities_RFQ_RequestEntry> listDetails = new List<Entities_RFQ_RequestEntry>();
                            listDetails = BLL.RFQ_TRANSACTION_Monitoring_GetDetailsByRFQNo(lblRFQNo.Text.Trim());

                            if (listDetails != null)
                            {
                                if (listDetails.Count > 0)
                                {
                                    gvDetailsReceiving.DataSource = listDetails;
                                    gvDetailsReceiving.DataBind();

                                    gvDetailsReceiving.Visible = true;
                                }
                            }

                            List<Entities_RFQ_RequestEntry> listRespondedSupplier = new List<Entities_RFQ_RequestEntry>();
                            listRespondedSupplier = BLL.RFQ_TRANSACTION_GetRespondedSupplierByRFQNo(lblRFQNo.Text.Trim());

                            if (listRespondedSupplier != null)
                            {
                                if (listRespondedSupplier.Count > 0)
                                {
                                    gvSupplierDetails.DataSource = listRespondedSupplier;
                                    gvSupplierDetails.DataBind();

                                    gvSupplierDetails.Visible = true;
                                }
                            }

                            List<Entities_RFQ_RequestEntry> listStatus = new List<Entities_RFQ_RequestEntry>();
                            listStatus = BLL.RFQ_TRANSACTION_GetStatusByRFQNo(lblRFQNo.Text.Trim());

                            if (listStatus != null)
                            {
                                if (listStatus.Count > 0)
                                {
                                    gvStatus.DataSource = listStatus;
                                    gvStatus.DataBind();

                                    gvStatus.Visible = true;
                                }
                            }

                            divLoader.Style.Add("display", "none");

                            // -------------------------------------------------------------------------------------------
                        }
                        else
                        {
                            ibApproved.ImageUrl = "~/images/A1.png";

                            gvDetailsReceiving.Visible = false;
                            gvSupplierDetails.Visible = false;
                            gvStatus.Visible = false;

                            lblRFQNo.Style.Add("font-size", "11px");

                            divDetails.Style.Add("margin-top", "0px");
                            divSupplierDetails.Style.Add("margin-top", "0px");
                            divSupplierDetails.Style.Add("margin-bottom", "0px");
                            divStatus.Style.Add("margin-top", "0px");
                            divStatus.Style.Add("margin-bottom", "0px");

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
                if (e.CommandName == "Preview_Command")
                {
                    //string URL = "~/RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(lblRFQNo.Text.Trim(), true);

                    Session["Requester_From_Inquiry"] = lblRequester.Text;
                    Session["TransDate_From_Inquiry"] = lblTransDate2.Text;
                    Session["Category_From_Inquiry"] = lblCategory.Text.Trim().ToUpper();
                    Session["btnPreview_Visibility"] = "false";
                    Session["btnUpdate_Visibility"] = "true";
                    Session["divSupplier_Visibility"] = "true";
                    Session["From_OnePage"] = "true";

                    //URL = Page.ResolveClientUrl(URL);
                    //ibPreview.OnClientClick = "window.open('" + URL + "'); return false;";

                    Response.Redirect("RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(lblRFQNo.Text.Trim(), true), false);

                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvDataReceiving_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label lblHasResponse = (Label)e.Row.FindControl("lblHasResponse");
                    Label lblRFQNo = (Label)e.Row.FindControl("lblRFQNo");
                    Label lblTransDate = (Label)e.Row.FindControl("lblTransDate");
                    Label lblCategory = (Label)e.Row.FindControl("lblCategory");
                    Label lblRequester = (Label)e.Row.FindControl("lblRequester");
                    Label CntSuppResp = (Label)e.Row.FindControl("CntSuppResp");
                    Label lblRequesterAttachment = (Label)e.Row.FindControl("lblRequesterAttachment");

                    if (!string.IsNullOrEmpty(lblHasResponse.Text))
                    {
                        if (int.Parse(lblHasResponse.Text.Trim()) > 0)
                        {
                            lblHasResponse.Text = "YES";
                        }
                        else
                        {
                            lblHasResponse.Text = "NO";
                        }
                    }

                    if (!string.IsNullOrEmpty(lblRequesterAttachment.Text))
                    {
                        if (int.Parse(lblRequesterAttachment.Text.Trim()) > 0)
                        {
                            lblRequesterAttachment.Text = "YES";
                        }
                        else
                        {
                            lblRequesterAttachment.Text = string.Empty;
                        }
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

        protected void gvDataReceiving_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvDataReceiving.PageIndex = e.NewPageIndex;
                btnSubmitReceiving_Click(sender, e);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvPopUpDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                LinkButton linkRFQNo = row.FindControl("linkRFQNo") as LinkButton;
                Label lblRequester = row.FindControl("lblRequester") as Label;
                Label lblTransactionDate = row.FindControl("lblTransactionDate") as Label;
                Label lblCategoryName = row.FindControl("lblCategoryName") as Label;
                Label lblStatDivManager = row.FindControl("lblStatDivManager") as Label;

                if (e.CommandName == "linkRFQNo_Command")
                {
                    Session["Requester_From_Inquiry"] = lblRequester.Text;
                    Session["TransDate_From_Inquiry"] = lblTransactionDate.Text;
                    Session["Category_From_Inquiry"] = lblCategoryName.Text.Trim().ToUpper();
                    Session["btnPreview_Visibility"] = lblStatDivManager.Text == "1" ? "true" : "false";
                    Session["btnUpdate_Visibility"] = "false";

                    Response.Redirect("RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(linkRFQNo.Text.Trim(), true), false);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvPopUpDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#98FB98'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvDetailsReceiving_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {                

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvDetailsReceiving_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#98FB98'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvSupplierDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblSupplier = (Label)e.Row.FindControl("lblSupplier");
                    Label lblResponseDate = (Label)e.Row.FindControl("lblResponseDate");

                    if (!string.IsNullOrEmpty(lblResponseDate.Text))
                    {
                        lblSupplier.Style.Add("font-weight", "bold");
                        lblResponseDate.Style.Add("font-weight", "bold");
                    }

                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#98FB98'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvStatus_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblProdManager = (Label)e.Row.FindControl("lblProdManager");
                    Label lblBuyer = (Label)e.Row.FindControl("lblBuyer");
                    Label lblIncharge = (Label)e.Row.FindControl("lblIncharge");
                    Label lblDepartmentManager = (Label)e.Row.FindControl("lblDepartmentManager");
                    Label lblDivisionManager = (Label)e.Row.FindControl("lblDivisionManager");
                    Label lblLeadTime = (Label)e.Row.FindControl("lblLeadTime");

                    Label lblProdManagerStatus = (Label)e.Row.FindControl("lblProdManagerStatus");
                    Label lblBuyerStatus = (Label)e.Row.FindControl("lblBuyerStatus");
                    Label lblInchargeStatus = (Label)e.Row.FindControl("lblInchargeStatus");
                    Label lblDepartmentManagerStatus = (Label)e.Row.FindControl("lblDepartmentManagerStatus");
                    Label lblDivisionManagerStatus = (Label)e.Row.FindControl("lblDivisionManagerStatus");


                    lblProdManager.Style.Add("color", COMMON.setStatusColor(lblProdManager.Text.Trim()));
                    lblBuyer.Style.Add("color", COMMON.setStatusColor(lblBuyer.Text.Trim()));
                    lblIncharge.Style.Add("color", COMMON.setStatusColor(lblIncharge.Text.Trim()));
                    lblDepartmentManager.Style.Add("color", COMMON.setStatusColor(lblDepartmentManager.Text.Trim()));
                    lblDivisionManager.Style.Add("color", COMMON.setStatusColor(lblDivisionManager.Text.Trim()));

                    lblProdManager.Text = lblProdManager.Text + lblProdManagerStatus.Text;
                    lblBuyer.Text = lblBuyer.Text + lblBuyerStatus.Text;
                    lblIncharge.Text = lblIncharge.Text + lblInchargeStatus.Text;
                    lblDepartmentManager.Text = lblDepartmentManager.Text + lblDepartmentManagerStatus.Text;
                    lblDivisionManager.Text = lblDivisionManager.Text + lblDivisionManagerStatus.Text;

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
                int approvedCounter = 0;
                int disapprovedCounter = 0;

                if (gvDataReceiving.Rows.Count > 0)
                {
                    List<string> listRFQ_For_Sending = new List<string>();

                    for (int i = 0; i < gvDataReceiving.Rows.Count; i++)
                    {
                        ImageButton ibApproved = (ImageButton)gvDataReceiving.Rows[i].Cells[3].FindControl("ibApproved");
                        ImageButton ibDisapproved = (ImageButton)gvDataReceiving.Rows[i].Cells[3].FindControl("ibDisapproved");
                        Label lblRFQNo = (Label)gvDataReceiving.Rows[i].Cells[0].FindControl("lblRFQNo");

                        if (ibApproved.ImageUrl == "~/images/A2.png")
                        {
                            listRFQ_For_Sending.Add(lblRFQNo.Text.Trim());
                            approvedCounter++;
                        }
                        if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                        {
                            disapprovedCounter++;
                        }
                    }

                    if (listRFQ_For_Sending != null)
                    {
                        if (listRFQ_For_Sending.Count > 0)
                        {
                            Session["RFQListForSending"] = listRFQ_For_Sending;
                        }
                    }

                }

                if (approvedCounter > 0 && disapprovedCounter > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('You cannot approved and disapproved items at the same time.');", true);
                }
                if (approvedCounter > 0 && disapprovedCounter <= 0)
                {
                    gvSentDetailsSendToSupplier.Visible = false;

                    tabAllRequest.Attributes.CssStyle.Add("display", "none");
                    tabReceivingEntry.Attributes.CssStyle.Add("display", "none");
                    divSendToSupplier.Attributes.CssStyle.Add("display", "block");

                    if (Session["RFQListForSending"] != null)
                    {

                        List<string> listRFQNo = (List<string>)Session["RFQListForSending"];

                        ddRFQNo.Items.Clear();
                        foreach (string rfq in listRFQNo)
                        {
                            ddRFQNo.Items.Add(rfq);
                        }

                    }
                    LoadDefaultSendToSupplier();

                }
                if (approvedCounter <= 0 && disapprovedCounter > 0)
                {
                    string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                    string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                    string query1 = string.Empty;
                    string query2 = string.Empty;
                    int queryCounter = 0;
                    string querySuccess = string.Empty;
                    string temp_RFQNo = string.Empty;

                    if (gvDataReceiving.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvDataReceiving.Rows.Count; i++)
                        {
                            ImageButton ibApproved = (ImageButton)gvDataReceiving.Rows[i].Cells[5].FindControl("ibApproved");
                            ImageButton ibDisapproved = (ImageButton)gvDataReceiving.Rows[i].Cells[5].FindControl("ibDisapproved");
                            Label lblRFQNo = (Label)gvDataReceiving.Rows[i].Cells[0].FindControl("lblRFQNo");
                            TextBox txtRemarks = (TextBox)gvDataReceiving.Rows[i].Cells[6].FindControl("txtRemarks");

                            if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                            {
                                query1 += "UPDATE Request_Status SET Purchasing = 2 WHERE RFQNo ='" + lblRFQNo.Text.Trim() + "' ";
                                query2 += "INSERT INTO Request_HistoryOfDisapproval (RFQNo,Cause,TransactionName,DisapprovedBy,DisapprovedDate,IsActive) VALUES ('" + lblRFQNo.Text.Trim() +
                                              "','" + txtRemarks.Text + "','" + ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingBuyer"].ToString() +
                                              "','" + Session["LcRefId"].ToString() + "',GETDATE(),1) ";
                                queryCounter = queryCounter + 2;
                                temp_RFQNo += lblRFQNo.Text.Trim() + ", ";
                            }
                        }

                        querySuccess = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + query2 + queryEndPart).ToString();

                        if (querySuccess == queryCounter.ToString())
                        {

                            Session["From_OnePage"] = "true";

                            Session["successMessage"] = "RFQ NUMBER(S) : <b>" + temp_RFQNo + "</b> HAS BEEN SUCCESSFULLY DISAPPROVED.";
                            Session["successTransactionName"] = "RFQ_PURCHASINGRECEIVING";
                            Session["successReturnPage"] = "RFQ_OnePage.aspx";

                            Response.Redirect("SuccessPage.aspx");
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        // END RECEIVING ENTRY
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // SEND TO SUPPLIER

        private void LoadDefaultSendToSupplier()
        {
            try
            {

                List<Entities_RFQ_RequestEntry> listSuppliersSendToSupplier = new List<Entities_RFQ_RequestEntry>();
                listSuppliersSendToSupplier = BLL.RFQ_TRANSACTION_GetSupplierForSendAndWithResponseByRFQNo(ddRFQNo.SelectedItem.Text.Trim());

                if (listSuppliersSendToSupplier != null)
                {
                    if (listSuppliersSendToSupplier.Count > 0)
                    {
                        gvSuppliersSendToSupplier.DataSource = listSuppliersSendToSupplier;
                        gvSuppliersSendToSupplier.DataBind();
                    }
                }

                //-------------------------------------------------------------------------------------------------------------------

                List<Entities_RFQ_RequestEntry> listDetailsSendToSupplier = new List<Entities_RFQ_RequestEntry>();
                listDetailsSendToSupplier = BLL.RFQ_TRANSACTION_Monitoring_GetDetailsByRFQNo(ddRFQNo.SelectedItem.Text.Trim());

                if (listDetailsSendToSupplier != null)
                {
                    if (listDetailsSendToSupplier.Count > 0)
                    {
                        gvDataSendToSupplier.DataSource = listDetailsSendToSupplier;
                        gvDataSendToSupplier.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('1 - " + ex.StackTrace.ToString() + "');", true);
            }
        }

        protected void ddRFQNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDefaultSendToSupplier();
        }

        protected void gvSuppliersSendToSupplier_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblSupplierID = (Label)e.Row.FindControl("lblSupplierID");
                    Label lblWithResponse = (Label)e.Row.FindControl("lblWithResponse");
                    Label lblSupplierName = (Label)e.Row.FindControl("lblSupplierName");
                    Label lblResponseCount = (Label)e.Row.FindControl("lblResponseCount");

                    if (!string.IsNullOrEmpty(lblWithResponse.Text))
                    {
                        if (int.Parse(lblWithResponse.Text.Trim()) > 0)
                        {
                            lblSupplierName.Style.Add("background-color", "#4CAF50");
                            lblSupplierName.Style.Add("color", "White");
                        }
                    }

                    if (!string.IsNullOrEmpty(lblResponseCount.Text))
                    {
                        if (int.Parse(lblWithResponse.Text.Trim()) > 0 && int.Parse(lblResponseCount.Text.Trim()) > 0)
                        {
                            lblSupplierName.Style.Add("background-color", "#009688");
                            lblSupplierName.Style.Add("color", "White");
                        }
                    }


                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('2 - " + ex.StackTrace.ToString() + "');", true);
            }
        }

        protected void gvSuppliersSendToSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                ImageButton ibApproved = row.FindControl("ibApproved") as ImageButton;

                if (e.CommandName == "A_Command")
                {
                    if (ibApproved.ImageUrl == "~/images/A1.png")
                    {
                        ibApproved.ImageUrl = "~/images/A2.png";
                    }
                    else
                    {
                        ibApproved.ImageUrl = "~/images/A1.png";
                    }
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('3 - " + ex.StackTrace.ToString() + "');", true);
            }
        }

        protected void btnUpdateRemarks_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvDataSendToSupplier.Rows.Count > 0)
                {
                    string queryPurchasingRemarks = string.Empty;
                    string qBPart = "BEGIN TRY BEGIN TRANSACTION ";
                    string qEPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                    int remarksCounter = 0;
                    string success = string.Empty;

                    for (int i = 0; i < gvDataSendToSupplier.Rows.Count; i++)
                    {
                        Label lblRefId = (Label)gvDataSendToSupplier.Rows[i].Cells[0].FindControl("lblRefId");
                        Label lblRemarks = (Label)gvDataSendToSupplier.Rows[i].Cells[6].FindControl("lblRemarks");
                        TextBox txtPurchasingRemarks = (TextBox)gvDataSendToSupplier.Rows[i].Cells[7].FindControl("txtPurchasingRemarks");

                        if (!string.IsNullOrEmpty(txtPurchasingRemarks.Text))
                        {
                            queryPurchasingRemarks += "UPDATE Request_Details SET Remarks = '" + lblRemarks.Text + " [" + txtPurchasingRemarks.Text + "]' WHERE RefId = '" + lblRefId.Text.Trim() + "' ";
                            remarksCounter++;
                        }
                    }

                    success = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(qBPart + queryPurchasingRemarks + qEPart).ToString();

                    if (success == remarksCounter.ToString())
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Updating remarks for RFQNo " + ddRFQNo.SelectedItem.Text + " has been successfully updated.');", true);
                    }


                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.StackTrace.ToString() + "');", true);
            }
        }

        protected void btnSendToSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvSuppliersSendToSupplier.Rows.Count > 0)
                {

                    List<Entities_RFQ_RequestEntry> listSendStatus = new List<Entities_RFQ_RequestEntry>();

                    List<Entities_RFQ_Currency> currencyL = new List<Entities_RFQ_Currency>();
                    string currencyList = string.Empty;

                    currencyL = BLL.RFQ_MT_Currency_GetAll();
                    if (currencyL != null)
                    {
                        if (currencyL.Count > 0)
                        {
                            foreach (Entities_RFQ_Currency currency in currencyL)
                            {
                                if (currency.IsDisabled == "0" || currency.IsDisabled.Length <= 0)
                                {
                                    currencyList += currency.Code + ",";
                                }
                            }
                        }
                    }

                    foreach (ListItem item in ddRFQNo.Items)
                    {

                        if (System.IO.Directory.Exists(Server.MapPath("~/IO_Request/" + item.Text.Trim().ToString())))
                        {

                            // Start of for (int i = 0; i < gvSuppliers.Rows.Count; i++)
                            for (int i = 0; i < gvSuppliersSendToSupplier.Rows.Count; i++)
                            {
                                ImageButton ibApproved = (ImageButton)gvSuppliersSendToSupplier.Rows[i].Cells[1].FindControl("ibApproved");
                                Label lblSupplierID = (Label)gvSuppliersSendToSupplier.Rows[i].Cells[0].FindControl("lblSupplierID");
                                Label lblSupplierEmail = (Label)gvSuppliersSendToSupplier.Rows[i].Cells[0].FindControl("lblSupplierEmail");
                                Label lblSupplierName = (Label)gvSuppliersSendToSupplier.Rows[i].Cells[0].FindControl("lblSupplierName");

                                if (ibApproved.ImageUrl == "~/images/A2.png")
                                {
                                    List<Entities_RFQ_RequestEntry> listRequestDetails = new List<Entities_RFQ_RequestEntry>();
                                    listRequestDetails = BLL.RFQ_TRANSACTION_GetRequestDetailsByRFQNo(item.Text.Trim().ToString());

                                    if (listRequestDetails != null)
                                    {
                                        if (listRequestDetails.Count > 0)
                                        {
                                            string strAttachment = string.Empty;
                                            if (!System.IO.File.Exists(System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + item.Text.Trim().ToString()), lblSupplierID.Text.Trim() + "_" + item.Text.Trim().ToString() + ".xml")))
                                            {
                                                string pathToXML = System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + item.Text.Trim().ToString()), lblSupplierID.Text.Trim() + "_" + item.Text.Trim().ToString() + ".xml");

                                                XmlWriterSettings xmlSetting = new XmlWriterSettings();
                                                xmlSetting.CloseOutput = true;

                                                XmlWriter writer = XmlWriter.Create(pathToXML, xmlSetting);
                                                writer.WriteStartDocument(true);
                                                writer.WriteStartElement("TABLE");

                                                foreach (Entities_RFQ_RequestEntry entity in listRequestDetails)
                                                {
                                                    string remarks = string.Empty;
                                                    if (entity.RdPurchasingRemarks.Length > 0)
                                                    {
                                                        remarks = entity.RdPurchasingRemarks;
                                                    }
                                                    else
                                                    {
                                                        remarks = string.Empty;
                                                    }

                                                    COMMON.createXMLNodeForRequestDetails(entity.RdRefId.ToString(), entity.RdDescription,
                                                                          entity.RdSpecs, entity.RdMaker, entity.RdQuantity.ToString(),
                                                                          entity.RdUOMDesc, remarks, writer, currencyList, DateTime.Now.AddDays(double.Parse(txtAging.Text.Trim())).ToLongDateString());

                                                    if (!string.IsNullOrEmpty(entity.RdAttachment))
                                                    {
                                                        strAttachment += System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + item.Text.Trim().ToString()), entity.RdAttachment) + ",";
                                                    }
                                                }

                                                writer.WriteEndElement();
                                                writer.WriteEndDocument();
                                                writer.Flush();
                                                writer.Close();

                                            }
                                            else
                                            {
                                                foreach (Entities_RFQ_RequestEntry entity in listRequestDetails)
                                                {
                                                    if (!string.IsNullOrEmpty(entity.RdAttachment))
                                                    {
                                                        strAttachment += System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + item.Text.Trim().ToString()), entity.RdAttachment) + ",";
                                                    }
                                                }
                                            }

                                            //=========== Sending Email =================
                                            string path = System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + item.Text.Trim().ToString()), lblSupplierID.Text.Trim() + "_" + item.Text.Trim().ToString() + ".xml");

                                            if (!string.IsNullOrEmpty(strAttachment))
                                            {
                                                path += "," + strAttachment;
                                            }
                                            string fixedBuyerInfo = "<table style='width:100%;'><tr><th align='left'>Purchasing Members</th><th align='left'>Section</th><th align='left'>Personal Email</th><th align='left'>Mobile Number</th></tr>" +
                                                                    "<tr><td>Dearoz, Cherrylou</td><td>MATERIALS - Main Materials</td><td>cherrylou.dearoz@adm.rohmphil.com</td><td>09266641532</td></tr>" +
                                                                    "<tr><td>Limon, Imelda</td><td>MATERIALS - Sub Materials</td><td>imelda.limon@adm.rohmphil.com</td><td>09178182391</td></tr>" +
                                                                    "<tr><td>Maniego, Pops</td><td>MATERIALS - Sub Materials</td><td>pops.maniego@adm.rohmphil.com</td><td>09271145260</td></tr>" +
                                                                    "<tr><td>Mapanoo, Anne Jasmine</td><td>MATERIALS - Sub Materials</td><td>anne.mapanoo@adm.rohmphil.com</td><td>09054147800</td></tr>" +
                                                                    "<tr><td>Puzo, Marielle</td><td>MATERIALS - Sub Materials</td><td>marielle.puzo@adm.rohmphil.com</td><td>09662566461</td></tr>" +
                                                                    "<tr><td>Jevulan, Sheryll Anne</td><td>MATERIALS - Documentation</td><td>sherylle.jevulan@adm.rohmphil.com</td><td>09661946253</td></tr>" +
                                                                    "<tr><td>Coloma, Val Davis</td><td>SPSU - Section Manager</td><td>val.coloma@adm.rohmphil.com</td><td>09178215282</td></tr>" +
                                                                    "<tr><td>Bernal, Jewelyn</td><td>SPSU - Factory Supplies</td><td>jewelyn.bernal@adm.rohmphil.com</td><td>09279075759</td></tr>" +
                                                                    "<tr><td>Olavario, Judy</td><td>SPSU - Office Supplies</td><td>judy.olavario@adm.rohmphil.com</td><td>09174391447</td></tr>" +
                                                                    "<tr><td>Bontigao, Marilyn</td><td>SPSU - Fabrication</td><td>marilyn.bontigao@adm.rohmphil.com</td><td>09162303531</td></tr>" +
                                                                    "<tr><td>Dayug, Sergio</td><td>SPSU - Spareparts - LSI</td><td>sergio.dayug@adm.rohmphil.com</td><td>09063107769</td></tr>" +
                                                                    "<tr><td>Perez, Baby Lyne</td><td>SPSU - Spareparts - Discrete</td><td>babylyne.perez@adm.rohmphil.com</td><td>09352659126</td></tr>" +
                                                                    "<tr><td>Negrite, Rodolfo</td><td>IMPEX - Equipment / COJO</td><td>rodolfo.negrite@adm.rohmphil.com</td><td>09260321369</td></tr>" +
                                                                    "<tr><td>Mercado, Renz</td><td>IMPEX - Import / Export</td><td>renz.mercado@adm.rohmphil.com</td><td>09276809692</td></tr>" +
                                                                    "<tr><td>Patulot, Jarrellyn</td><td>IMPEX - Import / Export</td><td>jarrellyn.patulot@adm.rohmphil.com</td><td>09174553619</td></tr>" +
                                                                    "</table>";

                                            string emailService = COMMON.sendEmailToSuppliers(lblSupplierEmail.Text.Trim(), ConfigurationManager.AppSettings["email-username"], lblSupplierID.Text.Trim() + "_" + item.Text.Trim().ToString(),
                                                        "Hi <b>" + lblSupplierName.Text + "</b> Good Day!" + "<br /><br /> Kindly check the attached xml file (" + lblSupplierID.Text.Trim() + "_" + item.Text.Trim().ToString() + ".xml) for our quotation request" + "<br /><br /><br />Thank You!<br /><br /><br />" +
                                                        "*** This is an automatically generated email, Please reply accordingly *** <br /> <br />" +
                                                        "You have received a new request quotation from ROHM Electronics Philippines Inc. <br /> <br />" +
                                                        "Please answer this quotation on or before <b>" + DateTime.Now.AddDays(double.Parse(txtAging.Text.Trim())).ToLongDateString().ToUpper() + "</b><br /> <br />For this RFQ, please contact <b>" + Session["UserFullName"].ToString() + "</b><br /> <br /><br /><br /> For inquries, kindly see below contact details : <br />" + fixedBuyerInfo, path, lblSupplierName.Text, lblSupplierID.Text.Trim() + "_" + item.Text.ToUpper().Trim().ToString());



                                            if (emailService.ToLower().Contains("success"))
                                            {
                                                string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                                                string query1 = "UPDATE Request_Status SET Purchasing = 1 WHERE RFQNo ='" + item.Text.Trim().ToString() + "' ";
                                                string query2 = "UPDATE Request_Status SET Buyer = 0, PurchasingIncharge = 0, DepartmentManager = 0, DivisionManager = 0 WHERE RFQNo = '" + item.Text.Trim().ToString() + "' ";
                                                string query3 = "UPDATE Request_Status SET Supplier = 3 WHERE RFQNo = '" + item.Text.Trim().ToString() + "' ";
                                                string query4 = "INSERT INTO Request_HistoryOfApproval (RFQNo,TransactionName,ApprovedBy,ApprovedDate,IsActive) VALUES ('" + item.Text.Trim().ToString() + "','" + ConfigurationManager.AppSettings["ApprovedDisapprovedName-Purchasing"].ToString() + "','" + Session["LcRefId"].ToString() + "', GETDATE(),1) ";
                                                string query5 = "INSERT INTO Supplier_Rewarded (RFQNo, SupplierID, SendDate, SendBy, Aging) VALUES ('" + item.Text.Trim().ToString() + "','" + lblSupplierID.Text.Trim() + "', GETDATE(),'" + Session["LcRefId"].ToString() + "','" + txtAging.Text.Trim() + "') ";
                                                string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                                                string query_Success = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + query2 + query3 + query4 + query5 + queryEndPart).ToString();

                                                if (query_Success == "5")
                                                {
                                                    Entities_RFQ_RequestEntry entitySendStatus = new Entities_RFQ_RequestEntry();

                                                    entitySendStatus.ResponseRFQNo = item.Text.Trim().ToString();
                                                    entitySendStatus.ResponseSendStatus = "SUCCESSFULLY SENT";
                                                    entitySendStatus.ResponseSupplierName = lblSupplierName.Text;

                                                    listSendStatus.Add(entitySendStatus);
                                                }

                                            }
                                            else
                                            {
                                                Entities_RFQ_RequestEntry entitySendStatus = new Entities_RFQ_RequestEntry();

                                                emailService = string.IsNullOrEmpty(emailService) ? "ERROR SENDING" : emailService;

                                                entitySendStatus.ResponseRFQNo = item.Text.Trim().ToString();
                                                entitySendStatus.ResponseSendStatus = emailService.Length > 70 ? emailService.ToUpper().Substring(0, 70).ToString() + "..." : emailService.ToUpper().ToString();
                                                entitySendStatus.ResponseSupplierName = lblSupplierName.Text;

                                                listSendStatus.Add(entitySendStatus);
                                            }

                                        }
                                    }

                                }


                            }
                            // End of for (int i = 0; i < gvSuppliers.Rows.Count; i++)

                        } // if (System.IO.Directory.Exists(Server.MapPath("~/IO_Request/" + item.Text.Trim().ToString())))
                    } // End of foreach (ListItem item in ddRFQNo.Items)


                    if (listSendStatus != null)
                    {
                        if (listSendStatus.Count > 0)
                        {
                            gvSentDetailsSendToSupplier.Visible = true;
                            gvSentDetailsSendToSupplier.DataSource = listSendStatus;
                            gvSentDetailsSendToSupplier.DataBind();
                        }
                    }

                    divOpacity.Style.Add("opacity", "1");

                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Sending Process has been completed. Please see below details.');", true);
                }

            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('4 - " + ex.StackTrace.ToString() + "');", true);
                errorSendingDetails = "ERROR DETAILS : <br/> MESSAGE : " + ex.Message + "<br/> STACKTRACE :" + ex.StackTrace.ToString();
            }
        }


        // END SEND TO SUPPLIER
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // FOR APPROVAL

        private void LoadDefaultForApproval()
        {
            try
            {
                 
                string approver = Session["LcRefId"].ToString();

                List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                List<Entities_Common_SystemUsers> listProdManagerApproval = new List<Entities_Common_SystemUsers>();
                List<Entities_Common_SystemUsers> listPurchasingApproval = new List<Entities_Common_SystemUsers>();

                //entity.DrFrom = txtFrom.Text.Trim();
                //entity.DrTo = txtTo.Text.Trim();

                list = null;

                listProdManagerApproval = BLL_COMMON.Common_checkIfUserAccessExistByUserId(approver, ConfigurationManager.AppSettings["ProductionApprovalAccess"].ToString());
                listPurchasingApproval = BLL_COMMON.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString());

                // PRODUCTION MANAGER --------------------------------------------------------------------------------------

                if (listProdManagerApproval.Count > 0 && listPurchasingApproval.Count <= 0)
                {
                    Session["ProdManagerIsApprovingRequest"] = "true";

                    if (!string.IsNullOrEmpty(txtRFQNo.Text))
                    {
                        list = BLL.RFQ_TRANSACTION_Approval(entity).Where(itm => itm.RhDepartment == Session["Department"].ToString() && itm.ProdManagerStatus == "0" && itm.Rfqno.Contains(txtRFQNo.Text)).ToList();
                    }
                    else
                    {
                        list = BLL.RFQ_TRANSACTION_Approval(entity).Where(itm => itm.RhDepartment == Session["Department"].ToString() && itm.ProdManagerStatus == "0").ToList();
                    }

                    if (list != null)
                    {
                        if (list.Count > 0)
                        {
                            gvDataForApproval.Visible = true;
                            gvDataForApproval.DataSource = list;
                            gvDataForApproval.DataBind();
                        }
                        else
                        {
                            gvDataForApproval.Visible = false;
                        }
                    }
                }

                // ----------------------------------------------------------------------------------------------------------


                // PURCHASING APPROVER --------------------------------------------------------------------------------------

                if (listPurchasingApproval.Count > 0)
                {
                    string username = Session["Username"].ToString();

                    Session["PurchasingIsApprovingRequest"] = "true";

                    entity.SearchCriteria = txtRFQNo.Text;


                    // FOR BUYER APPROVAL
                    if (username != "3844" || username != "1152" || username != "1402" || username != "002")
                    {
                        if (ddCategoryForApproval.SelectedItem.Text != "ALL CATEGORY")
                        {
                            list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.PurchasingStatus == "1" && itm.BuyerStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                        }
                        else
                        {
                            list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.PurchasingStatus == "1" && itm.BuyerStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                        }
                    }
                    // FOR INCHARGE APPROVAL
                    if (username == "3844")
                    {
                        ddApprover.Items.FindByValue("2").Selected = true;

                        if (ddCategoryForApproval.SelectedItem.Text != "ALL CATEGORY")
                        {
                            list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                        }
                        else
                        {
                            //list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.GroupBySupplierResponse == "YES" && itm.RhCategory.Trim().Equals(incharge_category) ).ToList();

                            if (username == "3844")
                            {
                                //SIR VAL
                                if (username == "3844")
                                {
                                    list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.GroupBySupplierResponse == "YES" && (itm.RhCategory.Trim() == "1009" || itm.RhCategory.Trim() == "1" || itm.RhCategory.Trim() == "1007" || itm.RhCategory.Trim() == "1014" || itm.RhCategory == "1013" || itm.RhCategory.Trim() == "1006" || itm.RhCategory.Trim() == "1010" || itm.RhCategory.Trim() == "7" || itm.RhCategory.Trim() == "2" || itm.RhCategory.Trim() == "1008")).ToList();
                                }
                                ////SIR RUDY
                                //if (username == "6985")
                                //{
                                //    list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.GroupBySupplierResponse == "YES" && itm.RhCategory.Trim() == "3").ToList();
                                //}
                            }
                            else
                            {
                                list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                            }
                        }
                    }
                    // FOR DEPARTMENT MANAGER APPROVAL
                    if (username == "1152" || username == "1402")
                    {
                        ddApprover.Items.FindByValue("3").Selected = true;

                        if (ddCategoryForApproval.SelectedItem.Text != "ALL CATEGORY")
                        {
                            list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.PurchasingInchargeStatus == "1" && itm.DepartmentManagerStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                        }
                        else
                        {
                            list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.PurchasingInchargeStatus == "1" && itm.DepartmentManagerStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                        }
                    }
                    // FOR DIVISION MANAGER APPROVAL
                    if (username == "002")
                    {
                        ddApprover.Items.FindByValue("4").Selected = true;

                        if (ddCategoryForApproval.SelectedItem.Text != "ALL CATEGORY")
                        {
                            list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.DepartmentManagerStatus == "1" && itm.DivisionManagerStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                        }
                        else
                        {
                            list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.DepartmentManagerStatus == "1" && itm.DivisionManagerStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                        }
                    }

                    if (list != null)
                    {
                        if (list.Count > 0)
                        {
                            gvPurchasingForApproval.Visible = true;
                            gvRelatedSearchForApproval.Visible = false;
                            gvPurchasingForApproval.DataSource = list;
                            gvPurchasingForApproval.DataBind();
                        }
                        else
                        {
                            gvPurchasingForApproval.Visible = false;
                        }
                    }
                    else
                    {
                        gvPurchasingForApproval.Visible = false;
                    }

                }

                // ----------------------------------------------------------------------------------------------------------


            }
            catch (Exception ex)
            {
                //throw ex;
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnSubmitForSendingQuotation_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = txtSearchSending.Text.Trim();
                btnSubmit_Click(sender, e);
                txtSearchSending.Text = string.Empty;
            }
            catch (Exception ex)
            {
                //throw ex;
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnSubmitForApproval_Click(object sender, EventArgs e)
        {
            try
            {

                txtSearch.Text = txtRFQNo.Text.Trim();
                btnSubmit_Click(sender, e);
                txtRFQNo.Text = string.Empty;

                             
                ////// TEMPORARY DISABLED KO ITONG MAY 6 NA SLASH PARA MATEST UNG SA ALL REQUEST LAHAT BABAGSAK ANG SEARCH

                //////string approver = Session["LcRefId"].ToString();

                //////List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                //////Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                //////List<Entities_Common_SystemUsers> listProdManagerApproval = new List<Entities_Common_SystemUsers>();
                //////List<Entities_Common_SystemUsers> listPurchasingApproval = new List<Entities_Common_SystemUsers>();

                ////////entity.DrFrom = txtFrom.Text.Trim();
                ////////entity.DrTo = txtTo.Text.Trim();

                //////list = null;

                //////listProdManagerApproval = BLL_COMMON.Common_checkIfUserAccessExistByUserId(approver, ConfigurationManager.AppSettings["ProductionApprovalAccess"].ToString());
                //////listPurchasingApproval = BLL_COMMON.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString());

                //////// PRODUCTION MANAGER --------------------------------------------------------------------------------------

                //////if (listProdManagerApproval.Count > 0 && listPurchasingApproval.Count <= 0)
                //////{
                //////    Session["ProdManagerIsApprovingRequest"] = "true";

                //////    if (!string.IsNullOrEmpty(txtRFQNo.Text))
                //////    {
                //////        list = BLL.RFQ_TRANSACTION_Approval(entity).Where(itm => itm.RhDepartment == Session["Department"].ToString() && itm.ProdManagerStatus == "0" && itm.Rfqno.Contains(txtRFQNo.Text)).ToList();
                //////    }
                //////    else
                //////    {
                //////        list = BLL.RFQ_TRANSACTION_Approval(entity).Where(itm => itm.RhDepartment == Session["Department"].ToString() && itm.ProdManagerStatus == "0").ToList();
                //////    }

                //////    if (list != null)
                //////    {
                //////        if (list.Count > 0)
                //////        {
                //////            gvDataForApproval.Visible = true;
                //////            gvDataForApproval.DataSource = list;
                //////            gvDataForApproval.DataBind();
                //////        }
                //////        else
                //////        {
                //////            gvDataForApproval.Visible = false;
                //////        }
                //////    }
                //////}

                //////// ----------------------------------------------------------------------------------------------------------


                //////// PURCHASING APPROVER --------------------------------------------------------------------------------------

                //////if (listPurchasingApproval.Count > 0)
                //////{
                //////    string username = Session["Username"].ToString();

                //////    Session["PurchasingIsApprovingRequest"] = "true";

                //////    entity.SearchCriteria = txtRFQNo.Text;


                //////    // FOR BUYER APPROVAL
                //////    if (username != "6985" || username != "3844" || username != "1152" || username != "1402" || username != "002")
                //////    {
                //////        if (ddCategoryForApproval.SelectedItem.Text != "ALL CATEGORY")
                //////        {
                //////            list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.PurchasingStatus == "1" && itm.BuyerStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                //////        }
                //////        else
                //////        {
                //////            list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.PurchasingStatus == "1" && itm.BuyerStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                //////        }
                //////    }
                //////    // FOR INCHARGE APPROVAL
                //////    if (username == "6985" || username == "3844")
                //////    {
                //////        ddApprover.Items.FindByValue("2").Selected = true;

                //////        if (ddCategoryForApproval.SelectedItem.Text != "ALL CATEGORY")
                //////        {
                //////            list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                //////        }
                //////        else
                //////        {
                //////            //list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.GroupBySupplierResponse == "YES" && itm.RhCategory.Trim().Equals(incharge_category) ).ToList();

                //////            if (username == "3844" || username == "6985")
                //////            {
                //////                //SIR VAL
                //////                if (username == "3844")
                //////                {
                //////                    list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.GroupBySupplierResponse == "YES" && (itm.RhCategory.Trim() == "1009" || itm.RhCategory.Trim() == "1" || itm.RhCategory.Trim() == "1007" || itm.RhCategory.Trim() == "1006" || itm.RhCategory.Trim() == "1010" || itm.RhCategory.Trim() == "7" || itm.RhCategory.Trim() == "2" || itm.RhCategory.Trim() == "1008")).ToList();
                //////                }
                //////                //SIR RUDY
                //////                if (username == "6985")
                //////                {
                //////                    list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.GroupBySupplierResponse == "YES" && itm.RhCategory.Trim() == "3").ToList();
                //////                }
                //////            }
                //////            else
                //////            {
                //////                list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.BuyerStatus == "1" && itm.PurchasingInchargeStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                //////            }
                //////        }
                //////    }
                //////    // FOR DEPARTMENT MANAGER APPROVAL
                //////    if (username == "1152" || username == "1402")
                //////    {
                //////        ddApprover.Items.FindByValue("3").Selected = true;

                //////        if (ddCategoryForApproval.SelectedItem.Text != "ALL CATEGORY")
                //////        {
                //////            list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.PurchasingInchargeStatus == "1" && itm.DepartmentManagerStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                //////        }
                //////        else
                //////        {
                //////            list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.PurchasingInchargeStatus == "1" && itm.DepartmentManagerStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                //////        }
                //////    }
                //////    // FOR DIVISION MANAGER APPROVAL
                //////    if (username == "002")
                //////    {
                //////        ddApprover.Items.FindByValue("4").Selected = true;

                //////        if (ddCategoryForApproval.SelectedItem.Text != "ALL CATEGORY")
                //////        {
                //////            list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.RhCategory.Trim() == ddCategory.SelectedValue.Trim() && itm.DepartmentManagerStatus == "1" && itm.DivisionManagerStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                //////        }
                //////        else
                //////        {
                //////            list = BLL.RFQ_TRANSACTION_Approval_Purchasing(entity).Where(itm => itm.DepartmentManagerStatus == "1" && itm.DivisionManagerStatus == "0" && itm.GroupBySupplierResponse == "YES").ToList();
                //////        }
                //////    }

                //////    if (list != null)
                //////    {
                //////        if (list.Count > 0)
                //////        {
                //////            gvPurchasingForApproval.Visible = true;
                //////            gvRelatedSearchForApproval.Visible = false;
                //////            gvPurchasingForApproval.DataSource = list;
                //////            gvPurchasingForApproval.DataBind();
                //////        }
                //////        else
                //////        {
                //////            gvPurchasingForApproval.Visible = false;
                //////        }
                //////        //else
                //////        //{
                //////        //    gvPurchasingForApproval.Visible = false;
                //////        //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('NO RECORDS FOUND BASED ON YOUR SEARCH CRITERIA. PLEASE SEE BELOW RELATED SEARCH RESULT.');", true);

                //////        //    List<Entities_RFQ_RequestEntry> listRelatedResults = new List<Entities_RFQ_RequestEntry>();
                //////        //    Entities_RFQ_RequestEntry entityRelatedResults = new Entities_RFQ_RequestEntry();

                //////        //    listRelatedResults = null;
                //////        //    entityRelatedResults.SearchCriteria = txtRFQNo.Text;

                //////        //    listRelatedResults = BLL.RFQ_TRANSACTION_AllRequest_ByDateRange(entityRelatedResults);

                //////        //    if (listRelatedResults != null)
                //////        //    {
                //////        //        if (listRelatedResults.Count > 0)
                //////        //        {
                //////        //            gvRelatedSearchForApproval.Visible = true;
                //////        //            gvRelatedSearchForApproval.DataSource = listRelatedResults;
                //////        //            gvRelatedSearchForApproval.DataBind();
                //////        //        }
                //////        //        else
                //////        //        {
                //////        //            gvRelatedSearchForApproval.Visible = false;
                //////        //            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('NO RECORDS FOUND IN RELATED SEARCH!');", true);
                //////        //        }
                //////        //    }
                //////        //}
                //////    }
                //////    else
                //////    {
                //////        gvPurchasingForApproval.Visible = false;
                //////    }

                //////}

                // ----------------------------------------------------------------------------------------------------------


            }
            catch (Exception ex)
            {
                //throw ex;
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnSendForApproval_Click(object sender, EventArgs e)
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
                string temp_RFQNo = string.Empty;

                //-------------------------------------------------------------------------------------------------------------
                if (Session["ProdManagerIsApprovingRequest"] != null)
                {

                    if (bool.Parse(Session["ProdManagerIsApprovingRequest"].ToString()))
                    {
                        if (gvDataForApproval.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvDataForApproval.Rows.Count; i++)
                            {
                                Label lblRFQNo = (Label)gvDataForApproval.Rows[i].Cells[0].FindControl("lblRFQNo");
                                ImageButton ibApproved = (ImageButton)gvDataForApproval.Rows[i].Cells[3].FindControl("ibApproved");
                                ImageButton ibDisapproved = (ImageButton)gvDataForApproval.Rows[i].Cells[3].FindControl("ibDisapproved");
                                TextBox txtRemarks = (TextBox)gvDataForApproval.Rows[i].Cells[4].FindControl("txtRemarks");

                                if (ibApproved.ImageUrl == "~/images/A2.png")
                                {
                                    query1 += "UPDATE Request_Status SET ProdManager = 1 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
                                    query2 += "INSERT INTO Request_HistoryOfApproval (RFQNo,TransactionName,ApprovedBy,ApprovedDate,IsActive) VALUES ('" + lblRFQNo.Text.Trim() +
                                              "','" + ConfigurationManager.AppSettings["ApprovedDisapprovedName-ProdManager"].ToString() +
                                              "','" + Session["LcRefId"].ToString() + "',GETDATE(),1) ";

                                    queryStatusCounter = queryStatusCounter + 2;
                                    temp_RFQNo += lblRFQNo.Text.Trim().ToUpper() + ", ";
                                }

                                if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                                {
                                    query1 += "UPDATE Request_Status SET ProdManager = 2 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
                                    query2 += "INSERT INTO Request_HistoryOfDisapproval (RFQNo,Cause,TransactionName,DisapprovedBy,DisapprovedDate,IsActive) VALUES ('" + lblRFQNo.Text.Trim() +
                                              "','" + txtRemarks.Text + "','" + ConfigurationManager.AppSettings["ApprovedDisapprovedName-ProdManager"].ToString() +
                                              "','" + Session["LcRefId"].ToString() + "',GETDATE(),1) ";

                                    queryStatusCounter = queryStatusCounter + 2;
                                    temp_RFQNo += lblRFQNo.Text.Trim().ToUpper() + ", ";
                                }

                            }

                            querySuccess = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + query2 + queryEndPart).ToString();

                            if (querySuccess == queryStatusCounter.ToString())
                            {
                                lblMessage.Text = "RFQ NUMBER(S) : <b>" + temp_RFQNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
                                lblTransactionName.Text = "RFQ_APPROVALFORM";

                                tabAllRequest.Attributes.CssStyle.Add("display", "none");
                                tabReceivingEntry.Attributes.CssStyle.Add("display", "none");
                                divSendToSupplier.Attributes.CssStyle.Add("display", "none");
                                tabForApproval.Attributes.CssStyle.Add("display", "none");
                                tabSuccess.Attributes.CssStyle.Add("display", "block");
                            }

                        }
                    }

                }
                //-------------------------------------------------------------------------------------------------------------

                if (Session["PurchasingIsApprovingRequest"] != null)
                {
                    if (bool.Parse(Session["PurchasingIsApprovingRequest"].ToString()))
                    {
                        if (gvPurchasingForApproval.Rows.Count > 0)
                        {
                            //int incharge = int.Parse(Session["listIncharge"].ToString());
                            //int deptManager = int.Parse(Session["listDeptManager"].ToString());
                            //int divManager = int.Parse(Session["listDivManager"].ToString());
                            string position = string.Empty;
                            string transactionName = string.Empty;
                            bool isSuccess = false;
                            string failedMessage = string.Empty;

                            if (ddApprover.SelectedItem.Text == "FOR BUYER APPROVAL")
                            {
                                // Buyer Approval 
                                position = "BUYER";
                                transactionName = ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingBuyer"].ToString();
                            }
                            if (ddApprover.SelectedItem.Text == "FOR INCHARGE APPROVAL")
                            {
                                // Incharge Approval 
                                position = "PURCHASINGINCHARGE";
                                transactionName = ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingIncharge"].ToString();
                            }
                            if (ddApprover.SelectedItem.Text == "FOR DEPARTMENT MANAGER APPROVAL")
                            {
                                // Department Manager Approval 
                                position = "DEPARTMENTMANAGER";
                                transactionName = ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingDeptManager"].ToString();
                            }
                            if (ddApprover.SelectedItem.Text == "FOR DIVISION MANAGER APPROVAL")
                            {
                                // Division Manager Approval
                                position = "DIVISIONMANAGER";
                                transactionName = ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingDivManager"].ToString();
                            }

                            for (int i = 0; i < gvPurchasingForApproval.Rows.Count; i++)
                            {

                                Label lblRFQNo = (Label)gvPurchasingForApproval.Rows[i].Cells[0].FindControl("lblRFQNo");
                                ImageButton ibApproved = (ImageButton)gvPurchasingForApproval.Rows[i].Cells[3].FindControl("ibApproved");
                                ImageButton ibDisapproved = (ImageButton)gvPurchasingForApproval.Rows[i].Cells[3].FindControl("ibDisapproved");
                                TextBox txtRemarks = (TextBox)gvPurchasingForApproval.Rows[i].Cells[4].FindControl("txtRemarks");
                                GridView gvResponseForApproval = (GridView)gvPurchasingForApproval.Rows[i].Cells[5].FindControl("gvResponseForApproval");


                                if (ibApproved.ImageUrl == "~/images/A2.png")
                                {
                                    if (position == "BUYER")
                                    {
                                        query1 += "UPDATE Request_Status SET " + position + " = 1, PURCHASINGINCHARGE = 0, DEPARTMENTMANAGER = 0, DIVISIONMANAGER = 0 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        query1 += "UPDATE Request_Status SET " + position + " = 1 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
                                    }

                                    query2 += "INSERT INTO Request_HistoryOfApproval (RFQNo,TransactionName,ApprovedBy,ApprovedDate,IsActive) VALUES ('" + lblRFQNo.Text.Trim() +
                                              "','" + transactionName + "','" + Session["LcRefId"].ToString() + "',GETDATE(),1) ";

                                    queryStatusCounter = queryStatusCounter + 2;
                                    temp_RFQNo += lblRFQNo.Text.Trim().ToUpper() + ", ";

                                    if (gvResponseForApproval.Rows.Count > 0)
                                    {
                                        int checkCounter = 0;
                                        int nullResponsePriceCounter = 0;
                                        string itemDescription = string.Empty;
                                        string itemDetailsRefId = string.Empty;

                                        foreach (GridViewRow rowResponse in gvResponseForApproval.Rows)
                                        {
                                            Label lblResponseRefId = (Label)rowResponse.FindControl("lblResponseRefId");
                                            Label lblIsGranted = (Label)rowResponse.FindControl("lblIsGranted");
                                            TextBox txtResponseCurrencyPrice = (TextBox)rowResponse.FindControl("txtResponseCurrencyPrice");
                                            TextBox txtResponseLeadTime = (TextBox)rowResponse.FindControl("txtResponseLeadTime");
                                            TextBox txtResponseSupplierRemarks = (TextBox)rowResponse.FindControl("txtResponseSupplierRemarks");
                                            TextBox txtResponsePurchasingRemarks = (TextBox)rowResponse.FindControl("txtResponsePurchasingRemarks");
                                            ImageButton ibApprovedResponse = (ImageButton)rowResponse.FindControl("ibApprovedResponse");
                                            Label lblResponseDescription = (Label)rowResponse.FindControl("lblResponseDescription");
                                            Label lblResponseSpecs = (Label)rowResponse.FindControl("lblResponseSpecs");
                                            Label lblDetailsRefId = (Label)rowResponse.FindControl("lblDetailsRefId");
                                            DropDownList ddResponseCurrency = (DropDownList)rowResponse.FindControl("ddResponseCurrency");


                                            if (ibApprovedResponse.ImageUrl == "~/images/A2.png")
                                            {
                                                query3 += "UPDATE Supplier_Response SET IsGranted = 1, ResponsePrice = '" + txtResponseCurrencyPrice.Text.Replace("'", "''").ToString() +
                                                          "', ResponseLead = '" + txtResponseLeadTime.Text.Replace("'", "''").ToString() + "', Remarks = '" + txtResponseSupplierRemarks.Text.Replace("'", "''").ToString() +
                                                          "', RCurrency = '" + ddResponseCurrency.SelectedValue + "' WHERE RefId = '" + lblResponseRefId.Text.Trim() + "' ";

                                                if (!string.IsNullOrEmpty(txtResponsePurchasingRemarks.Text))
                                                {
                                                    query4 += "INSERT INTO Supplier_Remarks (ResponseRefId, Remarks) VALUES ('" + lblResponseRefId.Text.Trim() + "','" + txtResponsePurchasingRemarks.Text + "') ";
                                                    queryStatusCounter++;
                                                }

                                                itemDescription += lblResponseDescription.Text.Replace(",", "").Trim() + lblResponseSpecs.Text.Replace(",", "").Trim() + ",";
                                                itemDetailsRefId += lblDetailsRefId.Text.Replace(",", "").Trim() + ",";
                                                checkCounter++;
                                            }

                                            if (ibApprovedResponse.ImageUrl == "~/images/A1.png")
                                            {
                                                query3 += "UPDATE Supplier_Response SET IsGranted = 0, ResponsePrice = '" + txtResponseCurrencyPrice.Text.Replace("'", "''").ToString() +
                                                          "', ResponseLead = '" + txtResponseLeadTime.Text.Replace("'", "''").ToString() + "', Remarks = '" + txtResponseSupplierRemarks.Text.Replace("'", "''").ToString() +
                                                          "', RCurrency = '" + ddResponseCurrency.SelectedValue + "' WHERE RefId = '" + lblResponseRefId.Text.Trim() + "' ";

                                                if (!string.IsNullOrEmpty(txtResponsePurchasingRemarks.Text))
                                                {
                                                    query4 += "INSERT INTO Supplier_Remarks (ResponseRefId, Remarks) VALUES ('" + lblResponseRefId.Text.Trim() + "','" + txtResponsePurchasingRemarks.Text + "') ";
                                                    queryStatusCounter++;
                                                }
                                            }

                                            if (string.IsNullOrEmpty(txtResponseCurrencyPrice.Text))
                                            {
                                                nullResponsePriceCounter++;
                                            }

                                            queryStatusCounter++;

                                        }

                                        if (checkCounter > 0)
                                        {
                                            List<string> completeItems = new List<string>(itemDescription.Trim().Split(',').Select(t => t.Trim()));
                                            bool isUnique = completeItems.Count == completeItems.Distinct().Count();

                                            List<string> completeDetailsRefId = new List<string>(itemDetailsRefId.Trim().Split(',').Select(t => t.Trim()));

                                            if (isUnique)
                                            {
                                                isSuccess = true;
                                            }
                                            else // has duplicate
                                            {
                                                isSuccess = false;
                                                failedMessage += "Please check your items. System detected duplicate selection in " + lblRFQNo.Text.Trim() + "<br/>";
                                            }
                                        }
                                        else
                                        {
                                            isSuccess = false;
                                            failedMessage += "You cannot approved empty items. Please select items you want to approve in RFQNo " + lblRFQNo.Text.Trim();
                                        }

                                        if (nullResponsePriceCounter > 0)
                                        {
                                            isSuccess = false;
                                            failedMessage += "Please check " + lblRFQNo.Text.Trim().ToUpper() + " - You have empty or null price in the list!";
                                        }

                                    }
                                }

                                if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                                {
                                    if (position == "BUYER")
                                    {
                                        query1 += "UPDATE Request_Status SET " + position + " = 2 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
                                    }
                                    else
                                    {
                                        query1 += "UPDATE Request_Status SET " + position + " = 2, BUYER = 0 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
                                    }
                                    query2 += "INSERT INTO Request_HistoryOfDisapproval (RFQNo,Cause,TransactionName,DisapprovedBy,DisapprovedDate,IsActive) VALUES ('" + lblRFQNo.Text.Trim() +
                                              "','" + txtRemarks.Text + "','" + transactionName + "','" + Session["LcRefId"].ToString() + "',GETDATE(),1) ";

                                    queryStatusCounter = queryStatusCounter + 2;
                                    temp_RFQNo += lblRFQNo.Text.Trim().ToUpper() + ", ";
                                }


                            }

                            if (string.IsNullOrEmpty(failedMessage))
                            {
                                if (!string.IsNullOrEmpty(query4))
                                {
                                    querySuccess = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + query2 + query3 + query4 + queryEndPart).ToString();
                                }
                                else
                                {
                                    querySuccess = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + query2 + query3 + queryEndPart).ToString();
                                }

                                if (querySuccess == queryStatusCounter.ToString())
                                {
                                    lblMessage.Text = "RFQ NUMBER(S) : <b>" + temp_RFQNo + "</b> HAS BEEN SUCCESSFULLY PROCESSED.";
                                    lblTransactionName.Text = "RFQ_APPROVALFORM";

                                    tabAllRequest.Attributes.CssStyle.Add("display", "none");
                                    tabReceivingEntry.Attributes.CssStyle.Add("display", "none");
                                    divSendToSupplier.Attributes.CssStyle.Add("display", "none");
                                    tabForApproval.Attributes.CssStyle.Add("display", "none");
                                    tabSuccess.Attributes.CssStyle.Add("display", "block");
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + failedMessage + "');", true);
                            }


                        }
                    }


                }

                //-------------------------------------------------------------------------------------------------------------

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvDataForApproval_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                Label lblRFQNo = row.FindControl("lblRFQNo") as Label;
                Label lblStatAll = row.FindControl("lblStatAll") as Label;
                Label lblCategory = row.FindControl("lblCategory") as Label;
                Label lblRequester = row.FindControl("lblRequester") as Label;
                Label lblTransactionDate = row.FindControl("lblTransactionDate") as Label;
                ImageButton ibPreview = row.FindControl("ibPreview") as ImageButton;
                ImageButton ibApproved = row.FindControl("ibApproved") as ImageButton;
                ImageButton ibDisapproved = row.FindControl("ibDisapproved") as ImageButton;
                TextBox txtRemarks = row.FindControl("txtRemarks") as TextBox;

                if (e.CommandName == "Preview_Command")
                {
                    //string URL = "~/RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(lblRFQNo.Text.Trim(), true);

                    Session["Requester_From_Inquiry"] = lblRequester.Text;
                    Session["TransDate_From_Inquiry"] = lblTransactionDate.Text;
                    Session["Category_From_Inquiry"] = lblCategory.Text.Trim().ToUpper();
                    Session["btnPreview_Visibility"] = lblStatAll.Text == "APPROVED" ? "true" : "false";
                    Session["btnUpdate_Visibility"] = "true";
                    Session["From_OnePage"] = "true";

                    //URL = Page.ResolveClientUrl(URL);
                    //ibPreview.OnClientClick = "window.open('" + URL + "'); return false;";

                    Response.Redirect("RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(lblRFQNo.Text.Trim(), true), false);
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
                        }
                        else
                        {
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

        protected void gvDataForApproval_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Label lblRFQNo = (Label)e.Row.FindControl("lblRFQNo");
                    //GridView gvResponse = e.Row.FindControl("gvResponse") as GridView;

                    //gvResponse.DataSource = BLL.RFQ_TRANSACTION_GetSupplierResponseByRFQNo(lblRFQNo.Text.Trim());
                    //gvResponse.DataBind();

                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvDataForApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvDataForApproval.PageIndex = e.NewPageIndex;
                btnSubmitForApproval_Click(sender, e);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvResponseForApproval_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblResponseDescription = (Label)e.Row.FindControl("lblResponseDescription");
                    Label lblResponseSupplier = (Label)e.Row.FindControl("lblResponseSupplier");
                    Label lblDetailsRefId = (Label)e.Row.FindControl("lblDetailsRefId");
                    Label lblIsGranted = (Label)e.Row.FindControl("lblIsGranted");
                    ImageButton ibApprovedResponse = (ImageButton)e.Row.FindControl("ibApprovedResponse");
                    DropDownList ddResponseCurrency = (DropDownList)e.Row.FindControl("ddResponseCurrency");
                    Label lblResponseCurrency = (Label)e.Row.FindControl("lblResponseCurrency");
                    TextBox txtResponseSupplierRemarks = (TextBox)e.Row.FindControl("txtResponseSupplierRemarks");
                    TextBox txtResponsePurchasingRemarks = (TextBox)e.Row.FindControl("txtResponsePurchasingRemarks");

                    List<Entities_RFQ_Currency> listCurrency = new List<Entities_RFQ_Currency>();
                    listCurrency = BLL.RFQ_MT_Currency_GetAll();

                    if (listCurrency != null)
                    {
                        if (listCurrency.Count > 0)
                        {
                            ddResponseCurrency.Items.Add("");
                            foreach (Entities_RFQ_Currency entity in listCurrency)
                            {
                                ListItem item = new ListItem();
                                item.Text = entity.Code;
                                item.Value = entity.Code;

                                if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                                {
                                    ddResponseCurrency.Items.Add(item);
                                }
                            }

                            if (!string.IsNullOrEmpty(lblResponseCurrency.Text.Trim()))
                            {
                                ddResponseCurrency.Items.FindByText(lblResponseCurrency.Text.Trim()).Selected = true;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(lblResponseDescription.Text))
                    {
                        if (lblResponseDescription.Text.Length > 36)
                        {
                            int supRemLen = lblResponseDescription.Text.Length;
                            int divisor = 36;
                            int height = ((supRemLen / divisor) * 20) + 10;

                            lblResponseDescription.Style.Add("Height", height.ToString() + "px");
                        }
                    }
                    if (!string.IsNullOrEmpty(lblResponseSupplier.Text))
                    {
                        lblResponseSupplier.Text = lblResponseSupplier.Text.Length > 17 ? lblResponseSupplier.Text.Substring(0, 17).ToString() + "..." : lblResponseSupplier.Text;
                    }

                    foreach (TableCell cell in e.Row.Cells)
                    {
                        if (IsOdd(long.Parse(lblDetailsRefId.Text.Trim())))
                        {
                            cell.Style.Add("background-color", "#98FB98");
                        }
                        else
                        {
                            cell.Style.Add("background-color", "#FFA07A");
                        }
                    }

                    if (lblIsGranted.Text.Trim() == "1")
                    {
                        ibApprovedResponse.ImageUrl = "~/images/A2.png";
                    }

                    if (!string.IsNullOrEmpty(txtResponseSupplierRemarks.Text))
                    {
                        if (txtResponseSupplierRemarks.Text.Length > 30)
                        {
                            int supRemLen = txtResponseSupplierRemarks.Text.Length;
                            int divisor = 30;
                            int height = ((supRemLen / divisor) * 20) + 30;

                            txtResponseSupplierRemarks.Style.Add("Height", height.ToString() + "px");
                        }
                    }

                    if (!string.IsNullOrEmpty(txtResponsePurchasingRemarks.Text))
                    {
                        if (txtResponsePurchasingRemarks.Text.Length > 30)
                        {
                            int supRemLen2 = txtResponsePurchasingRemarks.Text.Length;
                            int divisor2 = 30;
                            int height2 = ((supRemLen2 / divisor2) * 20) + 30;

                            txtResponsePurchasingRemarks.Style.Add("Height", height2.ToString() + "px");
                        }
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

        protected void gvResponseForApproval_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                ImageButton ibApprovedResponse = row.FindControl("ibApprovedResponse") as ImageButton;

                if (e.CommandName == "AResponse_Command")
                {
                    if (ibApprovedResponse.ImageUrl == "~/images/A1.png")
                    {
                        ibApprovedResponse.ImageUrl = "~/images/A2.png";
                    }
                    else
                    {
                        ibApprovedResponse.ImageUrl = "~/images/A1.png";
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvPurchasingForApproval_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Label lblRFQNo = (Label)e.Row.FindControl("lblRFQNo");                    
                    //GridView gvResponse = e.Row.FindControl("gvResponse") as GridView;                    

                    //gvResponse.DataSource = BLL.RFQ_TRANSACTION_GetSupplierResponseByRFQNo(lblRFQNo.Text.Trim());
                    //gvResponse.DataBind();

                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvPurchasingForApproval_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                Label lblRFQNo = row.FindControl("lblRFQNo") as Label;
                ImageButton ibPreview = row.FindControl("ibPreview") as ImageButton;
                ImageButton ibApproved = row.FindControl("ibApproved") as ImageButton;
                ImageButton ibDisapproved = row.FindControl("ibDisapproved") as ImageButton;
                TextBox txtRemarks = row.FindControl("txtRemarks") as TextBox;
                GridView gvResponseForApproval = row.FindControl("gvResponseForApproval") as GridView;
                Label lblStatAll = row.FindControl("lblStatAll") as Label;
                Label lblCategory = row.FindControl("lblCategory") as Label;
                Label lblRequester = row.FindControl("lblRequester") as Label;
                Label lblTransactionDate = row.FindControl("lblTransactionDate") as Label;


                if (e.CommandName == "Preview_Command")
                {
                    //string URL = "~/RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(lblRFQNo.Text.Trim(), true);

                    Session["Requester_From_Inquiry"] = lblRequester.Text;
                    Session["TransDate_From_Inquiry"] = lblTransactionDate.Text;
                    Session["Category_From_Inquiry"] = lblCategory.Text.Trim().ToUpper();
                    Session["btnPreview_Visibility"] = lblStatAll.Text == "APPROVED" ? "true" : "false";
                    Session["btnUpdate_Visibility"] = "true";
                    Session["divSupplier_Visibility"] = "true";
                    Session["From_OnePage"] = "true";

                    //URL = Page.ResolveClientUrl(URL);
                    //ibPreview.OnClientClick = "window.open('" + URL + "'); return false;";

                    Response.Redirect("RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(lblRFQNo.Text.Trim(), true), false);
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
                            gvResponseForApproval.Visible = true;
                            ibApproved.ImageUrl = "~/images/A2.png";

                            List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                            list = BLL.RFQ_TRANSACTION_GetSupplierResponseByRFQNo(lblRFQNo.Text.Trim());

                            if (list != null)
                            {
                                if (list.Count > 0)
                                {
                                    gvResponseForApproval.DataSource = list;
                                    gvResponseForApproval.DataBind();
                                }
                            }
                            else
                            {
                                gvResponseForApproval.EmptyDataText = "NO SUPPLIER RESPONSE";
                            }

                        }
                        else
                        {
                            gvResponseForApproval.Visible = false;
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

        protected void gvRelatedSearchForApproval_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                LinkButton linkRFQNo = row.FindControl("linkRFQNo") as LinkButton;
                Label lblRequester = row.FindControl("lblRequester") as Label;
                Label lblTransactionDate = row.FindControl("lblTransactionDate") as Label;
                Label lblCategoryName = row.FindControl("lblCategoryName") as Label;
                Label lblStatDivManager = row.FindControl("lblStatDivManager") as Label;


                if (e.CommandName == "linkRFQNo_Command")
                {

                    Session["Requester_From_Inquiry"] = lblRequester.Text;
                    Session["TransDate_From_Inquiry"] = lblTransactionDate.Text;
                    Session["Category_From_Inquiry"] = lblCategoryName.Text.Trim().ToUpper();
                    Session["btnPreview_Visibility"] = lblStatDivManager.Text == "1" ? "true" : "false";
                    Session["btnUpdate_Visibility"] = "false";
                    Session["divSupplier_Visibility"] = "true";

                    Response.Redirect("RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(linkRFQNo.Text.Trim(), true), false);

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvRelatedSearchForApproval_OnRowDataBound(object sender, GridViewRowEventArgs e)
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


        // END FOR APPROVAL
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // MONITORING DETAILS





        // END MONITORING DETAILS
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------






        private static bool IsOdd(long value)
        {
            return value % 2 != 0;
        }












    }
}
