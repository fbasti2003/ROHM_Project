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
    public partial class RFQ_PurchasingReceiving_Tester : System.Web.UI.Page
    {

        BLL_RFQ BLL = new BLL_RFQ();
        Common COMMON = new Common();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["PurchasingApprovalAccess"].ToString().Trim()))
                    {

                        txtFrom.Text = DateTime.Today.AddDays(-720).ToString("MM/dd/yyyy");
                        txtTo.Text = DateTime.Today.ToString("MM/dd/yyyy");

                        //---------------------------------------------------------------------------------------------------

                        List<Entities_RFQ_RequestEntry> listCategory = new List<Entities_RFQ_RequestEntry>();
                        listCategory = BLL.RFQ_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

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

                        if (Session["TopResponded_SupplierID"] != null)
                        {
                            //DO NOTHING
                        }
                        else
                        {

                            if (Session["CategoryAccess"] != null)
                            {
                                if (int.Parse(Session["CategoryAccess"].ToString()) > 0)
                                {

                                    if (Session["SENDING_OTHER_BUYERS"] != null)
                                    {
                                        if (!string.IsNullOrEmpty(Session["SENDING_OTHER_BUYERS"].ToString()))
                                        {
                                            ddCategory.Enabled = true;
                                            ddCategory.Items.FindByValue(Session["SENDING_OTHER_BUYERS"].ToString()).Selected = true;
                                        }
                                    }
                                    else
                                    {
                                        ddCategory.Enabled = false;
                                        ddCategory.Items.FindByValue(Session["CategoryAccess"].ToString()).Selected = true;
                                    }
                                }
                                else
                                {
                                    ddCategory.Enabled = true;
                                }
                            }

                        }

                        //---------------------------------------------------------------------------------------------------

                        if (Request.QueryString["SOR"] != null)
                        {
                            if (Request.QueryString["SOR"].ToString() == "sending")
                            {
                                ddType.Items.FindByText("FOR SENDING").Selected = true;
                            }

                            if (Request.QueryString["SOR"].ToString() == "resend")
                            {
                                ddType.Items.FindByText("FOR RESEND").Selected = true;
                            }
                        }
                        else
                        {
                            if (Session["TopResponded_SupplierID"] != null)
                            {
                                //DO NOTHING
                            }
                            else
                            {
                                ddType.Items.FindByText("FOR SENDING").Selected = true;
                            }
                        }

                        //---------------------------------------------------------------------------------------------------

                        // call submit button to load initial record
                        btnSubmit_Click(sender, e);

                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
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

                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    Session["Original_FromReceivingForm"] = txtSearch.Text;
                    Response.Redirect("RFQ_AllRequest.aspx");
                }
                else
                {

                    //---------------------------------------------------------------------------------------------------                                

                    btnSubmit.Enabled = false;

                    List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                    Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                    if (Request.QueryString["SOR"] != null)
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["SOR_RFQNo"].ToString()))
                        {
                            entity.SearchCriteria = Request.QueryString["SOR_RFQNo"].ToString();
                        }
                    }
                    else
                    {
                        entity.SearchCriteria = txtSearch.Text;
                    }

                    entity.DrFrom = txtFrom.Text.Trim();
                    entity.DrTo = txtTo.Text.Trim();
                    entity.Category = ddCategory.SelectedItem.Value;

                    if (ddCategory.SelectedItem.Text.ToLower() == "all category")
                    {
                        entity.Category = "0";
                        if (ddType.SelectedItem.Text.ToLower() == "for sending")
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange(entity);
                        }
                        if (ddType.SelectedItem.Text.ToLower() == "for resend")
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange_ForResend(entity).OrderBy(itm => itm.RdMaker).ToList();
                        }
                        if (ddType.SelectedItem.Text.ToLower() == "all")
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange(entity).Where(itm => itm.StatDivManager == "0").ToList().OrderBy(itm => itm.GroupBySupplierResponse).ToList();
                        }

                    }
                    else
                    {
                        if (ddType.SelectedItem.Text.ToLower() == "for sending")
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange(entity);
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('LIST COUNT NEW: " + list.Count.ToString() + " - " + entity.Category + " - " + entity.SearchCriteria + " - " + entity.DrFrom + " - " + entity.DrTo + "');", true);
                        }
                        if (ddType.SelectedItem.Text.ToLower() == "for resend")
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange_ForResend(entity).OrderBy(itm => itm.RdMaker).ToList();
                        }
                        if (ddType.SelectedItem.Text.ToLower() == "all")
                        {
                            list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange(entity).ToList().OrderBy(itm => itm.GroupBySupplierResponse).ToList();
                        }

                    }


                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (Session["TopResponded_SupplierID"] != null)
                    {
                        string rfqNo_top = string.Empty;
                        string supplier_id = string.Empty;

                        if (!string.IsNullOrEmpty(Session["TopResponded_SupplierID"].ToString()))
                        {
                            supplier_id = Session["TopResponded_SupplierID"].ToString();

                            List<Entities_RFQ_RequestEntry> list_TodayTopSupplierResponded = new List<Entities_RFQ_RequestEntry>();
                            list_TodayTopSupplierResponded = BLL.RFQ_TRANSACTION_GetTodayTopRespondedSupplier2().Where(itm => itm.TodayTopSupplier_SupplierId == supplier_id).ToList();

                            if (list_TodayTopSupplierResponded != null)
                            {
                                if (list_TodayTopSupplierResponded.Count > 0)
                                {
                                    foreach (Entities_RFQ_RequestEntry eTop in list_TodayTopSupplierResponded)
                                    {
                                        rfqNo_top += "'" + eTop.TodayTopSupplier_RFQNo + "',";
                                    }
                                }
                            }
                        }

                        entity.SearchCriteria = rfqNo_top.Substring(0, rfqNo_top.Length - 1).ToString();

                        Session["TopResponded_SupplierID"] = null;

                        list = BLL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange_TodayTopResponse(entity);

                        ddCategory.Items.FindByValue("ALL CATEGORY").Selected = true;
                        ddType.Items.FindByValue("ALL").Selected = true;

                        ddCategory.Enabled = false;
                        ddType.Enabled = false;

                    }
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




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
                            //if (!string.IsNullOrEmpty(txtSearch.Text))
                            //{
                            //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('NO RECORDS FOUND BASED ON YOUR SEARCH CRITERIA. PLEASE SEE BELOW RELATED SEARCH RESULT.');", true);
                            //}
                        }
                    }

                    //if (!string.IsNullOrEmpty(txtSearch.Text))
                    //{
                    //    List<Entities_RFQ_RequestEntry> listRelatedResults = new List<Entities_RFQ_RequestEntry>();
                    //    Entities_RFQ_RequestEntry entityRelatedResults = new Entities_RFQ_RequestEntry();

                    //    listRelatedResults = null;
                    //    entityRelatedResults.SearchCriteria = txtSearch.Text;

                    //    listRelatedResults = BLL.RFQ_TRANSACTION_AllRequest_ByDateRange(entityRelatedResults);

                    //    if (listRelatedResults != null)
                    //    {
                    //        if (listRelatedResults.Count > 0)
                    //        {
                    //            pRelatedResult.Style.Add("display", "block");
                    //            gvRelatedSearch.Visible = true;
                    //            gvRelatedSearch.DataSource = listRelatedResults;
                    //            gvRelatedSearch.DataBind();
                    //        }
                    //        else
                    //        {
                    //            pRelatedResult.Style.Add("display", "none");
                    //            gvRelatedSearch.Visible = false;
                    //            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('NO RECORDS FOUND IN RELATED SEARCH!');", true);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    pRelatedResult.Style.Add("display", "none");
                    //    gvRelatedSearch.Visible = false;
                    //}

                    divOpacity.Style.Add("opacity", "1");
                    divLoader.Style.Add("display", "none");

                    btnSubmit.Enabled = true;

                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + list.Count.ToString() + "');", true);

                    //---------------------------------------------------------------------------------------------------

                    //if (list != null)
                    //{
                    //    if (list.Count > 0)
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Loaded " + list.Count.ToString() + " record(s).');", true);
                    //    }
                    //}

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
                int holdCounter = 0;

                if (gvData.Rows.Count > 0)
                {
                    List<string> listRFQ_For_Sending = new List<string>();

                    for (int i = 0; i < gvData.Rows.Count; i++)
                    {
                        ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[3].FindControl("ibApproved");
                        ImageButton ibDisapproved = (ImageButton)gvData.Rows[i].Cells[3].FindControl("ibDisapproved");
                        ImageButton ibHold = (ImageButton)gvData.Rows[i].Cells[3].FindControl("ibHold");
                        Label lblRFQNo = (Label)gvData.Rows[i].Cells[0].FindControl("lblRFQNo");

                        if (ibApproved.ImageUrl == "~/images/A2.png")
                        {
                            listRFQ_For_Sending.Add(lblRFQNo.Text.Trim());
                            approvedCounter++;
                        }
                        if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                        {
                            disapprovedCounter++;
                        }

                        if (ibHold.ImageUrl == "~/images/hold2.png")
                        {
                            holdCounter++;
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
                    Response.Redirect("RFQ_SendToSuppliers.aspx", false);
                }
                if ((approvedCounter <= 0 && disapprovedCounter > 0) || (approvedCounter <= 0 && holdCounter > 0))
                {
                    string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                    string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                    string query1 = string.Empty;
                    string query2 = string.Empty;
                    int queryCounter = 0;
                    string querySuccess = string.Empty;
                    string temp_RFQNo = string.Empty;
                    string temp_RFQNo_For_Disapproved_Items = string.Empty;

                    if (gvData.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvData.Rows.Count; i++)
                        {
                            ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[8].FindControl("ibApproved");
                            ImageButton ibDisapproved = (ImageButton)gvData.Rows[i].Cells[8].FindControl("ibDisapproved");
                            ImageButton ibHold = (ImageButton)gvData.Rows[i].Cells[8].FindControl("ibHold");
                            Label lblRFQNo = (Label)gvData.Rows[i].Cells[2].FindControl("lblRFQNo");
                            TextBox txtRemarks = (TextBox)gvData.Rows[i].Cells[9].FindControl("txtRemarks");

                            Label lblCategory = (Label)gvData.Rows[i].Cells[4].FindControl("lblCategory");
                            DropDownList ddCategory = (DropDownList)gvData.Rows[i].Cells[4].FindControl("ddCategory");

                            if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                            {
                                if (lblCategory.Text.ToLower().Trim() == ddCategory.SelectedItem.Text.ToLower().Trim())
                                {
                                    query1 += "UPDATE Request_Status SET Purchasing = 2 WHERE RFQNo ='" + lblRFQNo.Text.Trim() + "' ";
                                    query2 += "INSERT INTO Request_HistoryOfDisapproval (RFQNo,Cause,TransactionName,DisapprovedBy,DisapprovedDate,IsActive) VALUES ('" + lblRFQNo.Text.Trim() +
                                                  "','" + txtRemarks.Text + "','" + ConfigurationManager.AppSettings["ApprovedDisapprovedName-PurchasingBuyer"].ToString() +
                                                  "','" + Session["LcRefId"].ToString() + "',GETDATE(),1) ";
                                    queryCounter = queryCounter + 2;
                                    temp_RFQNo += lblRFQNo.Text.Trim() + ", ";
                                }
                                else
                                {
                                    query1 += "UPDATE Request_HEAD SET Category ='" + ddCategory.SelectedValue + "' WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
                                    query2 += "INSERT INTO HistoryOfUpdates (RFQNo, DetailsRefId, TableName, UpdatedBy, UpdatedDate, TransactionName, UpdateWhat) " +
                                                            "VALUES ('" + lblRFQNo.Text.Trim() + "','NA','Request_Head','" + Session["LcRefId"].ToString() + "',GETDATE(),'Purchasing-NotMyCategory','Category from " + lblCategory.Text.ToUpper() + " to " + ddCategory.SelectedItem.Text.ToUpper() + "')";
                                    queryCounter = queryCounter + 2;
                                    temp_RFQNo += lblRFQNo.Text.Trim() + ", ";
                                }

                                temp_RFQNo_For_Disapproved_Items += lblRFQNo.Text.Trim().ToUpper() + ", ";
                            }

                            if (ibHold.ImageUrl == "~/images/hold2.png")
                            {
                                query1 += "UPDATE Request_Status SET Purchasing = 3 WHERE RFQNo = '" + lblRFQNo.Text.Trim() + "' ";
                                query2 += "INSERT INTO Request_Hold_Reason (RFQNo, Reason, CreatedBy, CreatedDate) VALUES ('" + lblRFQNo.Text.Trim() +
                                          "','" + txtRemarks.Text +
                                          "','" + Session["UserFullName"].ToString() + "',GETDATE()) ";

                                queryCounter = queryCounter + 2;
                                temp_RFQNo += lblRFQNo.Text.Trim() + ", ";

                            }
                        }

                        querySuccess = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + query2 + queryEndPart).ToString();

                        if (querySuccess == queryCounter.ToString())
                        {

                            //---------------------------------------------------------------------------------------------------------------------
                            //EMAIL NOTIFICATION FOR DISAPPROVED ITEMS
                            List<string> disapprovedItemsReadyForEmail = new List<string>(temp_RFQNo_For_Disapproved_Items.Trim().Split(',').Select(t => t.Trim()));

                            if (disapprovedItemsReadyForEmail != null)
                            {
                                if (disapprovedItemsReadyForEmail.Count > 0)
                                {
                                    foreach (string disapprovedRfqNoForEmail in disapprovedItemsReadyForEmail)
                                    {
                                        Entities_RFQ_RequestEntry eForEmail_Disapproved = new Entities_RFQ_RequestEntry();
                                        eForEmail_Disapproved.RhRfqNo = disapprovedRfqNoForEmail;

                                        List<Entities_RFQ_RequestEntry> disapprovedEmail = BLL.RFQ_TRANSACTION_GetRequesterEmailAndLocalNumber_ByRFQNo(eForEmail_Disapproved);
                                        if (disapprovedEmail != null)
                                        {
                                            if (disapprovedEmail.Count > 0)
                                            {
                                                foreach (Entities_RFQ_RequestEntry eEmailDisapproved in disapprovedEmail)
                                                {
                                                    if (!string.IsNullOrEmpty(eEmailDisapproved.RhEmailAddress))
                                                    {
                                                        string body = "RFQ - <b>" + disapprovedRfqNoForEmail + "</b> has been <b><h1 style='background-color:Red'>DISAPPROVED</h1><br/><br/>RFQS-DISAPPROVED Notification! Please do not reply!";
                                                        string emailRequester = COMMON.sendEmailToRequester(eEmailDisapproved.RhEmailAddress, ConfigurationManager.AppSettings["email-username"], "RFQ - (" + disapprovedRfqNoForEmail.Trim() + ") DISAPPROVED NOTIFICATION", body);

                                                        if (emailRequester.ToLower().Contains("success"))
                                                        {
                                                            // do nothing
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                            //---------------------------------------------------------------------------------------------------------------------

                            Session["successMessage"] = "RFQ NUMBER(S) : <b>" + temp_RFQNo + "</b> HAS BEEN SUCCESSFULLY DISAPPROVED OR TRANSFERRED.";
                            Session["successTransactionName"] = "RFQ_PURCHASINGRECEIVING";
                            Session["successReturnPage"] = "RFQ_PurchasingReceiving.aspx";

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

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                LinkButton lblCtrl = row.FindControl("lblCtrl") as LinkButton;
                ImageButton ibApproved = row.FindControl("ibApproved") as ImageButton;
                ImageButton ibDisapproved = row.FindControl("ibDisapproved") as ImageButton;
                ImageButton ibPreview = row.FindControl("ibPreview") as ImageButton;
                ImageButton ibHold = row.FindControl("ibHold") as ImageButton;
                TextBox txtRemarks = row.FindControl("txtRemarks") as TextBox;
                Label lblRFQNo = row.FindControl("lblRFQNo") as Label;
                Label lblRequester = row.FindControl("lblRequester") as Label;
                Label lblTransDate2 = row.FindControl("lblTransDate2") as Label;
                Label lblCategory = row.FindControl("lblCategory") as Label;
                DropDownList ddCategory = row.FindControl("ddCategory") as DropDownList;
                GridView gvDetails = row.FindControl("gvDetails") as GridView;
                GridView gvSupplierDetails = row.FindControl("gvSupplierDetails") as GridView;
                GridView gvStatus = row.FindControl("gvStatus") as GridView;
                GridView gvHoldReason = row.FindControl("gvHoldReason") as GridView;
                HtmlGenericControl divDetails = row.FindControl("divDetails") as HtmlGenericControl;
                HtmlGenericControl divSupplierDetails = row.FindControl("divSupplierDetails") as HtmlGenericControl;
                HtmlGenericControl divStatus = row.FindControl("divStatus") as HtmlGenericControl;
                HtmlGenericControl divHoldReason = row.FindControl("divHoldReason") as HtmlGenericControl;

                if (e.CommandName == "lblCtrl_Command")
                {
                    string URL = "~/PIPL_InvoiceDetails.aspx?ControlNo_From_Inquiry=" + CryptorEngine.Encrypt(lblCtrl.Text.Trim(), true);

                    URL = Page.ResolveClientUrl(URL);
                    lblCtrl.OnClientClick = "window.open('" + URL + "'); return true;";
                }
                if (e.CommandName == "A_Command")
                {
                    if (ibDisapproved.ImageUrl == "~/images/DA2.png" || ibHold.ImageUrl == "~/images/hold2.png")
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
                                    gvDetails.DataSource = listDetails;
                                    gvDetails.DataBind();

                                    gvDetails.Visible = true;
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

                            //---------------------------------------------------------------------------------------------------

                            List<Entities_RFQ_RequestEntry> List_HoldReason = new List<Entities_RFQ_RequestEntry>();
                            List_HoldReason = BLL.RFQ_TRANSACTION_GetHoldReason_ByRFQNo(lblRFQNo.Text.Trim());

                            if (List_HoldReason != null)
                            {
                                if (List_HoldReason.Count > 0)
                                {
                                    divHoldReason.Style.Add("display", "block");
                                    gvHoldReason.DataSource = List_HoldReason;
                                    gvHoldReason.DataBind();
                                }
                            }


                            divOpacity.Style.Add("opacity", "1");
                            divLoader.Style.Add("display", "none");
                        }
                        else
                        {
                            ibApproved.ImageUrl = "~/images/A1.png";

                            gvDetails.Visible = false;
                            gvSupplierDetails.Visible = false;
                            gvStatus.Visible = false;

                            lblRFQNo.Style.Add("font-size", "11px");

                            divDetails.Style.Add("margin-top", "0px");
                            divSupplierDetails.Style.Add("margin-top", "0px");
                            divSupplierDetails.Style.Add("margin-bottom", "0px");
                            divStatus.Style.Add("margin-top", "0px");
                            divStatus.Style.Add("margin-bottom", "0px");
                            divHoldReason.Style.Add("display", "none");
                        }
                    }
                }
                if (e.CommandName == "DA_Command")
                {
                    if (ibApproved.ImageUrl == "~/images/A2.png" || ibHold.ImageUrl == "~/images/hold2.png")
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

                            // -------------------------------------------------------------------------------------------

                            ddCategory.Visible = true;
                            lblCategory.Visible = false;
                            ddCategory.Items.Clear();

                            List<Entities_RFQ_RequestEntry> listCategory = new List<Entities_RFQ_RequestEntry>();
                            listCategory = BLL.RFQ_TRANSACTION_RequestEntry_Fill_All_DropdownList();

                            if (listCategory != null)
                            {
                                if (listCategory.Count > 0)
                                {
                                    ddCategory.Items.Add("");

                                    foreach (Entities_RFQ_RequestEntry entity in listCategory)
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

                                    ddCategory.Items.FindByText(lblCategory.Text).Selected = true;

                                }
                            }
                            // -------------------------------------------------------------------------------------------

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
                                    gvDetails.DataSource = listDetails;
                                    gvDetails.DataBind();

                                    gvDetails.Visible = true;
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


                            divOpacity.Style.Add("opacity", "1");
                            divLoader.Style.Add("display", "none");


                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('NOTE : Please enter your reason if you want to disapproved then click submit button. If this is not your item and you want it to transfer to other category, then just select the new category then click submit button.');", true);

                        }
                        else
                        {
                            ibDisapproved.ImageUrl = "~/images/DA1.png";
                            txtRemarks.Text = string.Empty;
                            txtRemarks.Enabled = false;

                            ddCategory.Visible = false;
                            lblCategory.Visible = true;

                            gvDetails.Visible = false;
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

                if (e.CommandName == "Hold_Command")
                {
                    if (ibApproved.ImageUrl == "~/images/A2.png" || ibDisapproved.ImageUrl == "~/images/DA2.png")
                    {

                    }
                    else
                    {
                        if (ibHold.ImageUrl == "~/images/hold.png")
                        {
                            ibHold.ImageUrl = "~/images/hold2.png";
                            txtRemarks.Enabled = true;
                        }
                        else
                        {
                            ibHold.ImageUrl = "~/images/hold.png";
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

        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
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
                    DropDownList ddSendDates = (DropDownList)e.Row.FindControl("ddSendDates");

                    if (!string.IsNullOrEmpty(lblHasResponse.Text))
                    {
                        if (int.Parse(lblHasResponse.Text.Trim()) >= 8)
                        {
                            //lblHasResponse.Style.Add("background-color", "#00BCD4");
                            //lblHasResponse.Style.Add("color", "white");
                            lblHasResponse.Text = "YES";
                        }
                        else
                        {
                            lblHasResponse.Text = "NO";
                        }
                    }

                    //if (!string.IsNullOrEmpty(CntSuppResp.Text.Trim()))
                    //{
                    //    // FOR SENDING
                    //    if (int.Parse(CntSuppResp.Text.Trim()) <= 0)
                    //    {                            
                    //        lblRFQNo.BackColor = System.Drawing.Color.LightBlue;
                    //        lblTransDate.BackColor = System.Drawing.Color.LightBlue;
                    //        lblCategory.BackColor = System.Drawing.Color.LightBlue;
                    //        lblRequester.BackColor = System.Drawing.Color.LightBlue;
                    //    }
                    //    // FOR RESEND
                    //    if (int.Parse(CntSuppResp.Text.Trim()) > 0)
                    //    {
                    //        lblRFQNo.BackColor = System.Drawing.Color.LightGreen;
                    //        lblTransDate.BackColor = System.Drawing.Color.LightGreen;
                    //        lblCategory.BackColor = System.Drawing.Color.LightGreen;
                    //        lblRequester.BackColor = System.Drawing.Color.LightGreen;
                    //    }
                    //}

                    //---------------------------------------------------------------------------------------------------
                    List<Entities_RFQ_RequestEntry> List_SendDates = new List<Entities_RFQ_RequestEntry>();
                    List_SendDates = BLL.RFQ_TRANSACTION_SendDate_ByRFQNo(lblRFQNo.Text.Trim()).OrderByDescending(itm => DateTime.Parse(itm.SendDate)).ToList();

                    if (List_SendDates != null)
                    {
                        if (List_SendDates.Count > 0)
                        {
                            ddSendDates.Items.Clear();

                            foreach (Entities_RFQ_RequestEntry entity in List_SendDates)
                            {
                                ddSendDates.Items.Add(entity.SendDate);
                            }
                        }
                    }

                    //---------------------------------------------------------------------------------------------------

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
            try
            {
                gvData.PageIndex = e.NewPageIndex;
                btnSubmit_Click(sender, e);
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

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                LinkButton linkDescription = row.FindControl("linkDescription") as LinkButton;
                HtmlGenericControl divPopUp = row.FindControl("divPopUp") as HtmlGenericControl;
                GridView gvPopUpDetails = gvData.Rows[8].FindControl("gvPopUpDetails") as GridView;

                if (e.CommandName == "linkDescription_Command")
                {
                    //divPopUp.Style.Add("display", "block");
                    //List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                    //Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                    //entity.SearchCriteria = linkDescription.Text;
                    //entity.DivisionManagerStatus = string.Empty;

                    //list = BLL.RFQ_TRANSACTION_AllRequest_ByDateRange(entity);

                    //if (list != null)
                    //{
                    //    if (list.Count > 0)
                    //    {
                    //        gvPopUpDetails.DataSource = list;
                    //        gvPopUpDetails.DataBind();
                    //    }
                    //}

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Label lblResponseDescription = (Label)e.Row.FindControl("lblResponseDescription");
                    //Label lblResponseSupplier = (Label)e.Row.FindControl("lblResponseSupplier");
                    //Label lblDetailsRefId = (Label)e.Row.FindControl("lblDetailsRefId");
                    //Label lblIsGranted = (Label)e.Row.FindControl("lblIsGranted");
                    //ImageButton ibApprovedResponse = (ImageButton)e.Row.FindControl("ibApprovedResponse");

                    //if (!string.IsNullOrEmpty(lblResponseDescription.Text))
                    //{
                    //    lblResponseDescription.Text = lblResponseDescription.Text.Length > 31 ? lblResponseDescription.Text.Substring(0, 31).ToString() + "..." : lblResponseDescription.Text;
                    //}
                    //if (!string.IsNullOrEmpty(lblResponseSupplier.Text))
                    //{
                    //    lblResponseSupplier.Text = lblResponseSupplier.Text.Length > 17 ? lblResponseSupplier.Text.Substring(0, 17).ToString() + "..." : lblResponseSupplier.Text;
                    //}

                    //foreach (TableCell cell in e.Row.Cells)
                    //{
                    //    if (IsOdd(long.Parse(lblDetailsRefId.Text.Trim())))
                    //    {
                    //        cell.Style.Add("background-color", "#98FB98");
                    //    }
                    //    else
                    //    {
                    //        cell.Style.Add("background-color", "#FFA07A");
                    //    }
                    //}

                    //if (lblIsGranted.Text.Trim() == "1")
                    //{
                    //    ibApprovedResponse.ImageUrl = "~/images/A2.png";
                    //}

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


        protected void gvRelatedSearch_RowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void gvRelatedSearch_OnRowDataBound(object sender, GridViewRowEventArgs e)
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

        











    }
}
