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
using SpreadsheetLight;
using System.Data.Common;

namespace REPI_PUR_SOFRA
{
    public partial class RFQ_AllRequest : System.Web.UI.Page
    {

        BLL_RFQ BLL = new BLL_RFQ();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {

            this.Page.Form.Enctype = "multipart/form-data";

            if (!IsPostBack)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        if (Session["Original_FromApprovalForm"] != null)
                        {
                            if (!string.IsNullOrEmpty(Session["Original_FromApprovalForm"].ToString()))
                            {
                                txtSearch.Text = Session["Original_FromApprovalForm"].ToString();
                                Session["Original_FromApprovalForm"] = null;
                            }
                        }

                        if (Session["Original_FromReceivingForm"] != null)
                        {
                            if (!string.IsNullOrEmpty(Session["Original_FromReceivingForm"].ToString()))
                            {
                                txtSearch.Text = Session["Original_FromReceivingForm"].ToString();
                                Session["Original_FromReceivingForm"] = null;
                            }
                        }

                        if (Session["Original_FromUpdatingDetails"] != null)
                        {
                            if (!string.IsNullOrEmpty(Session["Original_FromUpdatingDetails"].ToString()))
                            {
                                txtSearch.Text = Session["Original_FromUpdatingDetails"].ToString();
                                Session["Original_FromUpdatingDetails"] = null;
                            }
                        }

                        txtFrom.Text = DateTime.Today.AddDays(-100).ToString("MM/dd/yyyy");
                        txtTo.Text = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");

                        LoadDefaultForExport();

                        btnSubmit_Click(sender, e);                     
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

                List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();                

                list = null;

                entity.SearchCriteria = txtSearch.Text;

                //if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()))
                //{
                //    list = BLL.RFQ_TRANSACTION_AllRequest_ByDateRange(entity);
                //}
                //else
                //{
                //    list = BLL.RFQ_TRANSACTION_AllRequest_ByDateRange(entity);
                //    //Disable this part kasi gusto nila lahat ng request kita regardless of status
                //    //list = BLL.RFQ_TRANSACTION_AllRequest_ByDateRange_AllApproved(entity);
                //}                

                if (ViewState["CurrentTable"] != null)
                {
                    list = BLL.RFQ_TRANSACTION_AllRequest_ByDateRange(entity).Take(20).ToList();
                }
                else
                {
                    list = BLL.RFQ_TRANSACTION_AllRequest_ByDateRange(entity);
                }

                
                if (list != null)
                {
                    if (list.Count > 0)
                    {
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
                Label lblSection = row.FindControl("lblSection") as Label;
                Label lblDepartment = row.FindControl("lblDepartment") as Label;
                Label lblDepartmentCode = row.FindControl("lblDepartmentCode") as Label;
                Label lblDivision = row.FindControl("lblDivision") as Label;
                Label lblCategory = row.FindControl("lblCategory") as Label;
                TextBox txtBuyerNotes = row.FindControl("txtBuyerNotes") as TextBox;
                LinkButton linkBuyerNotes = row.FindControl("linkBuyerNotes") as LinkButton;
                Label lblSpecsDrawing = row.FindControl("lblSpecsDrawing") as Label;

                Button btnReapply = row.FindControl("btnReapply") as Button;


                if (e.CommandName == "btnReapply_Command")
                {

                    if (gvReapply.Rows.Count <= 10)
                    {

                        LoadDefault();

                        if (gvReapply.Rows.Count <= 0)
                        {
                            //FIRST ENTRY

                            List<Entities_RFQ_RequestEntry> listReapply = new List<Entities_RFQ_RequestEntry>();
                            listReapply = BLL.RFQ_TRANSACTION_Monitoring_GetDetailsByRFQNo_ForReApply(linkRFQNo.Text.Trim());

                            DataTable dt = new DataTable();
                            DataRow dr = null;
                            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                            dt.Columns.Add(new DataColumn("RdNo", typeof(string)));
                            dt.Columns.Add(new DataColumn("RdDescription", typeof(string)));
                            dt.Columns.Add(new DataColumn("RdRefId", typeof(string)));
                            dt.Columns.Add(new DataColumn("RdUnitOfMeasure", typeof(string)));
                            dt.Columns.Add(new DataColumn("RdSpecs", typeof(string)));
                            dt.Columns.Add(new DataColumn("RdMaker", typeof(string)));
                            dt.Columns.Add(new DataColumn("RdQuantity", typeof(string)));
                            dt.Columns.Add(new DataColumn("Col5", typeof(string)));
                            dt.Columns.Add(new DataColumn("RdPurpose", typeof(string)));
                            dt.Columns.Add(new DataColumn("RdProcess", typeof(string)));
                            dt.Columns.Add(new DataColumn("RdRemarks", typeof(string)));
                            dr = dt.NewRow();

                            if (listReapply.Count > 0)
                            {
                                foreach (Entities_RFQ_RequestEntry entity in listReapply)
                                {
                                    if (lblSpecsDrawing.Text.ToUpper() == entity.RdSpecs.ToUpper())
                                    {
                                        dr["RowNumber"] = 1;
                                        dr["RdRefId"] = string.Empty;
                                        dr["RdUnitOfMeasure"] = entity.RdUnitOfMeasure;
                                        dr["RdDescription"] = entity.RdDescription;
                                        dr["RdSpecs"] = entity.RdSpecs;
                                        dr["RdMaker"] = entity.RdMaker;
                                        dr["RdQuantity"] = entity.RdQuantity;
                                        dr["Col5"] = string.Empty;

                                        dr["RdPurpose"] = entity.RdPurpose;
                                        dr["RdProcess"] = entity.RdProcess;
                                        dr["RdRemarks"] = entity.RdRemarks;

                                        dt.Rows.Add(dr);
                                    }
                                }
                            }

                            
                            ViewState["CurrentTable"] = dt;

                            gvReapply.DataSource = dt;
                            gvReapply.DataBind();


                        }
                        else
                        {
                            // SUCCEEDING ENTRY


                            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                            DataRow drCurrentRow = null;

                            
                            int rowIndex = 0;

                            if (dtCurrentTable.Rows.Count <= 1)
                            {

                                TextBox txtDescription = (TextBox)gvReapply.Rows[rowIndex].Cells[1].FindControl("txtDescription");
                                TextBox txtSpecsDrawing = (TextBox)gvReapply.Rows[rowIndex].Cells[2].FindControl("txtSpecsDrawing");
                                TextBox txtMakerBrand = (TextBox)gvReapply.Rows[rowIndex].Cells[3].FindControl("txtMakerBrand");
                                TextBox txtQuantity = (TextBox)gvReapply.Rows[rowIndex].Cells[4].FindControl("txtQuantity");
                                DropDownList ddUOM = (DropDownList)gvReapply.Rows[rowIndex].Cells[5].FindControl("ddUOM");
                                TextBox txtFileSize = (TextBox)gvReapply.Rows[rowIndex].Cells[6].FindControl("txtFileSize");
                                TextBox txtPurpose = (TextBox)gvReapply.Rows[rowIndex].Cells[7].FindControl("txtPurpose");
                                TextBox txtProcess = (TextBox)gvReapply.Rows[rowIndex].Cells[8].FindControl("txtProcess");
                                TextBox txtRemarks = (TextBox)gvReapply.Rows[rowIndex].Cells[9].FindControl("txtRemarks");
                                FileUpload fuAttachment = (FileUpload)gvReapply.Rows[rowIndex].Cells[10].FindControl("fuAttachment");
                                TextBox txtFileName = (TextBox)gvReapply.Rows[rowIndex].Cells[11].FindControl("txtFileName");

                                drCurrentRow = dtCurrentTable.NewRow();
                                drCurrentRow["RowNumber"] = 1;

                                dtCurrentTable.Rows[0]["RdRefId"] = string.Empty;
                                dtCurrentTable.Rows[0]["RdUnitOfMeasure"] = ddUOM.SelectedValue;
                                dtCurrentTable.Rows[0]["RdDescription"] = txtDescription.Text;
                                dtCurrentTable.Rows[0]["RdSpecs"] = txtSpecsDrawing.Text;
                                dtCurrentTable.Rows[0]["RdMaker"] = txtMakerBrand.Text;
                                dtCurrentTable.Rows[0]["RdQuantity"] = txtQuantity.Text;
                                dtCurrentTable.Rows[0]["Col5"] = ddUOM.SelectedValue;
                                dtCurrentTable.Rows[0]["RdPurpose"] = txtPurpose.Text;
                                dtCurrentTable.Rows[0]["RdProcess"] = txtProcess.Text;
                                dtCurrentTable.Rows[0]["RdRemarks"] = txtRemarks.Text;

                                if (!string.IsNullOrEmpty(txtSpecsDrawing.Text))
                                {
                                    dtCurrentTable.Rows.Add(drCurrentRow);
                                    //ViewState["CurrentTable"] = dtCurrentTable;
                                }


                            }

                            List<Entities_RFQ_RequestEntry> listReapply2 = new List<Entities_RFQ_RequestEntry>();
                            listReapply2 = BLL.RFQ_TRANSACTION_Monitoring_GetDetailsByRFQNo(linkRFQNo.Text.Trim());

                            int i = dtCurrentTable.Rows.Count;
                            
                            if (listReapply2.Count > 0)
                            {
                                foreach (Entities_RFQ_RequestEntry entity2 in listReapply2)
                                {
                                    if (lblSpecsDrawing.Text.ToUpper() == entity2.RdSpecs.ToUpper())
                                    {
                                        drCurrentRow = dtCurrentTable.NewRow();
                                        drCurrentRow["RowNumber"] = i + 1;

                                        dtCurrentTable.Rows[i - 1]["RdRefId"] = string.Empty;
                                        dtCurrentTable.Rows[i - 1]["RdUnitOfMeasure"] = entity2.RdUnitOfMeasure;
                                        dtCurrentTable.Rows[i - 1]["RdDescription"] = entity2.RdDescription;
                                        dtCurrentTable.Rows[i - 1]["RdSpecs"] = entity2.RdSpecs;
                                        dtCurrentTable.Rows[i - 1]["RdMaker"] = entity2.RdMaker;
                                        dtCurrentTable.Rows[i - 1]["RdQuantity"] = entity2.RdQuantity;
                                        //dtCurrentTable.Rows[i - 1]["Col5"] = ddUOM.SelectedValue;
                                        dtCurrentTable.Rows[i - 1]["RdPurpose"] = entity2.RdPurpose;
                                        dtCurrentTable.Rows[i - 1]["RdProcess"] = entity2.RdProcess;
                                        dtCurrentTable.Rows[i - 1]["RdRemarks"] = entity2.RdRemarks;


                                        dtCurrentTable.Rows.Add(drCurrentRow);
                                        //ViewState["CurrentTable"] = dtCurrentTable;

                                    }
                                }
                            }
                            //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + dtCurrentTable.Rows.Count.ToString() + "');", true);

                            ////CLEAN UP TABLE
                            //if (gvReapply.Rows.Count > 0)
                            //{

                            //    ViewState["CurrentTable"] = null;

                            //    int ri = 0;
                            //    int n = gvReapply.Rows.Count;
                            //    for (int b = 0; b <= gvReapply.Rows.Count; b++)
                            //    {
                            //        TextBox txtDescription = (TextBox)gvReapply.Rows[b].Cells[1].FindControl("txtDescription");
                            //        TextBox txtSpecsDrawing = (TextBox)gvReapply.Rows[b].Cells[2].FindControl("txtSpecsDrawing");
                            //        TextBox txtMakerBrand = (TextBox)gvReapply.Rows[b].Cells[3].FindControl("txtMakerBrand");
                            //        TextBox txtQuantity = (TextBox)gvReapply.Rows[b].Cells[4].FindControl("txtQuantity");
                            //        DropDownList ddUOM = (DropDownList)gvReapply.Rows[b].Cells[5].FindControl("ddUOM");
                            //        TextBox txtFileSize = (TextBox)gvReapply.Rows[b].Cells[6].FindControl("txtFileSize");
                            //        TextBox txtPurpose = (TextBox)gvReapply.Rows[b].Cells[7].FindControl("txtPurpose");
                            //        TextBox txtProcess = (TextBox)gvReapply.Rows[b].Cells[8].FindControl("txtProcess");
                            //        TextBox txtRemarks = (TextBox)gvReapply.Rows[b].Cells[9].FindControl("txtRemarks");
                            //        FileUpload fuAttachment = (FileUpload)gvReapply.Rows[b].Cells[10].FindControl("fuAttachment");
                            //        TextBox txtFileName = (TextBox)gvReapply.Rows[b].Cells[11].FindControl("txtFileName");

                            //        drCurrentRow = dtCurrentTable.NewRow();
                            //        drCurrentRow["RowNumber"] = b + 1;

                            //        dtCurrentTable.Rows[n-1]["RdRefId"] = string.Empty;
                            //        dtCurrentTable.Rows[n - 1]["RdUnitOfMeasure"] = ddUOM.SelectedValue;
                            //        dtCurrentTable.Rows[n - 1]["RdDescription"] = txtDescription.Text;
                            //        dtCurrentTable.Rows[n - 1]["RdSpecs"] = txtSpecsDrawing.Text;
                            //        dtCurrentTable.Rows[n - 1]["RdMaker"] = txtMakerBrand.Text;
                            //        dtCurrentTable.Rows[n - 1]["RdQuantity"] = txtQuantity.Text;
                            //        dtCurrentTable.Rows[n - 1]["Col5"] = ddUOM.SelectedValue;
                            //        dtCurrentTable.Rows[n - 1]["RdPurpose"] = txtPurpose.Text;
                            //        dtCurrentTable.Rows[n - 1]["RdProcess"] = txtProcess.Text;
                            //        dtCurrentTable.Rows[n - 1]["RdRemarks"] = txtRemarks.Text;

                            //        if (!string.IsNullOrEmpty(txtSpecsDrawing.Text))
                            //        {
                            //            dtCurrentTable.Rows.Add(drCurrentRow);                                        
                            //        }

                            //        ri++;
                            //    }
                            //}

                            ViewState["CurrentTable"] = dtCurrentTable;
                            gvReapply.DataSource = (DataTable)ViewState["CurrentTable"];
                            gvReapply.DataBind();

                            //SetPreviousData();


                        }


                        divOpacity.Style.Add("opacity", "1");
                        divReapply.Style.Add("display", "block");
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Item successfully added in Re-apply item(s). Please scroll down below.');", true);


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('RE-APPLY LIMIT NOTIFICATION : You can add request up to 10 items only!');", true);
                    }

                }



                if (e.CommandName == "linkBuyerNotes_Command")
                {
                    int updateNotes = BLL.RFQ_TRANSACTION_UpdateBuyerNotes(txtBuyerNotes.Text, linkRFQNo.Text.Trim());

                    if (updateNotes > 0)
                    {
                        divOpacity.Style.Add("opacity", "1");
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Your notes has been successfully updated.');", true);
                    }
                }

                if (e.CommandName == "linkRFQNo_Command")
                {
                    //string URL = "~/RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(linkRFQNo.Text.Trim(), true);
                    Session["DepartmentCode_From_Inquiry"] = lblDepartmentCode.Text;
                    Session["Requester_From_Inquiry"] = lblRequester.Text;
                    Session["TransDate_From_Inquiry"] = lblTransactionDate.Text;
                    Session["Category_From_Inquiry"] = lblCategoryName.Text.Trim().ToUpper();
                    Session["btnPreview_Visibility"] = lblStatDivManager.Text == "1" ? "true" : "false";
                    Session["btnUpdate_Visibility"] = "false";
                    if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()))
                    {
                        Session["divSupplier_Visibility"] = "true";
                    }
                    else
                    {
                        Session["divSupplier_Visibility"] = "false";
                    }

                    Response.Redirect("RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(linkRFQNo.Text.Trim(), true), false);

                    //URL = Page.ResolveClientUrl(URL);
                    //linkRFQNo.OnClientClick = "window.open('" + URL + "'); return false;";
                }

                if (e.CommandName == "btnReceiving_Command")
                {
                    if (btnReceiving.Text == "FOR SENDING")
                    {
                        Response.Redirect("RFQ_PurchasingReceiving.aspx?SOR=sending&SOR_RFQNo=" + linkRFQNo.Text);
                    }
                    if (btnReceiving.Text == "FOR RESEND")
                    {
                        Response.Redirect("RFQ_PurchasingReceiving.aspx?SOR=resend&SOR_RFQNo=" + linkRFQNo.Text);
                    }
                }

                if (e.CommandName == "btnApproval_Command")
                {
                    Response.Redirect("RFQ_ApprovalForm.aspx?AF_RFQNo=" + linkRFQNo.Text);
                }

                if (e.CommandName == "btnManualApproved_Command")
                {                    
                    Response.Redirect("RFQ_ManualApproved.aspx?RFQNo_From_ManualApproved=" + CryptorEngine.Encrypt(linkRFQNo.Text.Trim(), true));
                }

                if (e.CommandName == "btnPreview_Command")
                {
                    if (isItemManualApproved(linkRFQNo.Text.Trim().ToUpper()))
                    {
                        

                        string URL2 = "http://10.27.1.170:9292/IO_Request/" + linkRFQNo.Text.Trim() + "/ManualApproved-" + linkRFQNo.Text.Trim() + ".pdf";

                        URL2 = Page.ResolveClientUrl(URL2);
                        btnPreview.OnClientClick = "window.open('" + URL2 + "'); return false;";
                    }
                    else
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
                    //Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");
                    
                    //Label lblSpecsDrawing = (Label)e.Row.FindControl("lblSpecsDrawing");
                    //Label lblDescription = (Label)e.Row.FindControl("lblDescription");
                    //Label lblMaker = (Label)e.Row.FindControl("lblMaker");
                    
                    //Label lblRequester = (Label)e.Row.FindControl("lblRequester");

                    //if (!string.IsNullOrEmpty(lblRequester.Text))
                    //{
                    //    lblRequester.Text = lblRequester.Text.Length > 20 ? lblRequester.Text.Substring(0, 20).ToString() + "..." : lblRequester.Text;
                    //}
                    //if (!string.IsNullOrEmpty(lblRemarks.Text))
                    //{
                    //    lblRemarks.Text = lblRemarks.Text.Length > 25 ? lblRemarks.Text.Substring(0, 25).ToString() : lblRemarks.Text + "...";
                    //}

                    //if (!string.IsNullOrEmpty(lblSpecsDrawing.Text))
                    //{
                    //    lblSpecsDrawing.Text = lblSpecsDrawing.Text.Length > 40 ? lblSpecsDrawing.Text.Substring(0, 40).ToString() + "..." : lblSpecsDrawing.Text;
                    //}
                    //if (!string.IsNullOrEmpty(lblDescription.Text))
                    //{
                    //    lblDescription.Text = lblDescription.Text.Length > 40 ? lblDescription.Text.Substring(0, 40).ToString() + "..." : lblDescription.Text;
                    //}
                    //if (!string.IsNullOrEmpty(lblMaker.Text))
                    //{
                    //    lblMaker.Text = lblMaker.Text.Length > 15 ? lblMaker.Text.Substring(0, 15).ToString() + "..." : lblMaker.Text;
                    //}

                    Label lblCategory = (Label)e.Row.FindControl("lblCategory");
                    Label lblStatAll = (Label)e.Row.FindControl("lblStatAll");
                    Label lblStatColor = (Label)e.Row.FindControl("lblStatColor");
                    Button btnReceiving = (Button)e.Row.FindControl("btnReceiving");
                    Button btnApproval = (Button)e.Row.FindControl("btnApproval");
                    Button btnPreview = (Button)e.Row.FindControl("btnPreview");
                    HtmlControl trBuyerNotes = (HtmlControl)e.Row.FindControl("trBuyerNotes");

                    Button btnReapply = (Button)e.Row.FindControl("btnReapply");
                    Button btnManualApproved = (Button)e.Row.FindControl("btnManualApproved");
                    

                    lblStatAll.Style.Add("background-color", lblStatColor.Text.Trim());


                    string category = Session["CategoryAccess"].ToString();


                    if (!string.IsNullOrEmpty(category))
                    {
                        if (int.Parse(category) > 0 || Session["Username"].ToString() == "3844" || Session["Username"].ToString() == "6985" || Session["Username"].ToString() == "1152" || Session["Username"].ToString() == "1402" || Session["Username"].ToString() == "002")
                        {

                            if (category == lblCategory.Text.Trim())
                            {
                                trBuyerNotes.Visible = true;
                            }
                            else
                            {
                                trBuyerNotes.Visible = false;
                            }

                            btnReceiving.Visible = true;
                            btnApproval.Visible = true;
                            btnPreview.Visible = true;
                            lblStatAll.Visible = false;
                            //-------------------------------------------------------------------------------------------------
                            if (lblStatAll.Text.Trim() == "FOR PRODUCTION MANAGER APPROVAL")
                            {
                                btnReceiving.Visible = false;
                                btnApproval.Visible = false;
                                btnPreview.Visible = false;
                                lblStatAll.Visible = true;
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
                                btnManualApproved.Visible = true;
                            }
                            if (lblStatAll.Text.Trim() == "SUPPLIER RESPONDED / FOR BUYER APPROVAL")
                            {
                                btnReceiving.Text = "FOR RESEND";
                                btnApproval.Text = "SUPPLIER RESPONDED / FOR BUYER APPROVAL";
                                btnPreview.Visible = false;
                                btnManualApproved.Visible = true;
                            }
                            if (lblStatAll.Text.Trim() == "FOR PURCHASING INCHARGE APPROVAL")
                            {
                                btnReceiving.Text = "";
                                btnApproval.Text = "FOR INCHARGE APPROVAL";
                                btnReceiving.Visible = false;
                                btnPreview.Visible = false;
                            }
                            if (lblStatAll.Text.Trim() == "FOR PURCHASING DEPARTMENT MANAGER APPROVAL")
                            {
                                btnReceiving.Text = "";
                                btnApproval.Text = "FOR DEPARTMENT MANAGER APPROVAL";
                                btnReceiving.Visible = false;
                                btnPreview.Visible = false;
                            }
                            if (lblStatAll.Text.Trim() == "FOR PURCHASING DIVISION MANAGER APPROVAL")
                            {
                                btnReceiving.Text = "";
                                btnApproval.Text = "FOR DIVISION MANAGER APPROVAL";
                                btnReceiving.Visible = false;
                                btnPreview.Visible = false;
                            }
                            if (lblStatAll.Text.Trim() == "APPROVED")
                            {
                                btnReceiving.Visible = false;
                                btnApproval.Visible = false;
                                btnPreview.Visible = true;
                                btnReapply.Visible = true;
                            }
                            if (lblStatAll.Text.Trim() == "DISAPPROVED")
                            {
                                btnReceiving.Visible = false;
                                btnApproval.Visible = false;
                                btnPreview.Visible = false;
                                lblStatAll.Visible = true;
                                btnReapply.Visible = true;
                            }
                            //-------------------------------------------------------------------------------------------------
                        }
                        else
                        {
                            btnReceiving.Visible = false;
                            btnApproval.Visible = false;
                            btnPreview.Visible = false;
                            lblStatAll.Visible = true;
                            if (lblStatAll.Text.Trim() == "APPROVED" || lblStatAll.Text.Trim() == "DISAPPROVED")
                            {
                                btnReapply.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        if (lblStatAll.Text.Trim() == "APPROVED" || lblStatAll.Text.Trim() == "DISAPPROVED")
                        {
                            btnReapply.Visible = true;
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



        private void LoadDefault()
        {
            try
            {

                //---------------------------------------------------------------------------------------------------

                List<Entities_RFQ_RequestEntry> listCategory = new List<Entities_RFQ_RequestEntry>();
                listCategory = BLL.RFQ_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                if (listCategory != null)
                {
                    if (listCategory.Count > 0)
                    {
                        ddCategory.Items.Clear();
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
                                    if (item.Value == "1013" || item.Value == "1006"
                                        || entity.DropdownRefId == "1015" || entity.DropdownRefId == "3" || entity.DropdownRefId == "1"
                                        || entity.DropdownRefId == "7" || entity.DropdownRefId == "1014" || entity.DropdownRefId == "1019")
                                    {
                                        //DO NOT ADD THIS CATEGORY
                                    }
                                    else
                                    {
                                        ddCategory.Items.Add(item);
                                    }
                                }

                            }

                        }

                    }
                }

                //---------------------------------------------------------------------------------------------------


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        private void LoadDefaultForExport()
        {
            try
            {

                //---------------------------------------------------------------------------------------------------

                List<Entities_RFQ_RequestEntry> listCategory = new List<Entities_RFQ_RequestEntry>();
                listCategory = BLL.RFQ_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                if (listCategory != null)
                {
                    if (listCategory.Count > 0)
                    {

                        ddCategoryExport.Items.Clear();
                        ddCategoryExport.Items.Add("");

                        foreach (Entities_RFQ_RequestEntry entity in listCategory)
                        {
                            ListItem item = new ListItem();
                            item.Text = entity.DropdownName;
                            item.Value = entity.DropdownRefId;

                            if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                            {
                                if (entity.TableName == "MT_Category")
                                {
                                    if (item.Value == "1013" || item.Value == "1006"
                                        || entity.DropdownRefId == "1015" || entity.DropdownRefId == "3" || entity.DropdownRefId == "1"
                                        || entity.DropdownRefId == "7" || entity.DropdownRefId == "1014" || entity.DropdownRefId == "1019")
                                    {
                                        //DO NOT ADD THIS CATEGORY
                                    }
                                    else
                                    {
                                        ddCategoryExport.Items.Add(item);
                                    }
                                }

                            }

                        }

                    }
                }

                //---------------------------------------------------------------------------------------------------


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvReapply_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddUOM = (DropDownList)e.Row.FindControl("ddUOM");
                    Label lblUnitOfMeasure = (Label)e.Row.FindControl("lblUnitOfMeasure");
                    FileUpload fuAttachment = (FileUpload)e.Row.FindControl("fuAttachment");
                    TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");
                    TextBox txtSpecsDrawing = (TextBox)e.Row.FindControl("txtSpecsDrawing");

                    List<Entities_RFQ_RequestEntry> listCategory = new List<Entities_RFQ_RequestEntry>();
                    listCategory = BLL.RFQ_TRANSACTION_RequestEntry_Fill_All_DropdownList();

                    if (listCategory != null)
                    {
                        if (listCategory.Count > 0)
                        {

                            foreach (Entities_RFQ_RequestEntry entity in listCategory)
                            {
                                ListItem item = new ListItem();
                                item.Text = entity.DropdownName;
                                item.Value = entity.DropdownRefId;

                                if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                                {
                                    if (entity.TableName == "MT_UnitOfMeasure")
                                    {
                                        ddUOM.Items.Add(item);
                                    }

                                }

                            }
                            
                        }
                    }

                    if (string.IsNullOrEmpty(txtSpecsDrawing.Text))
                    {
                        e.Row.Visible = false;                        
                    }
                   


                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvReapply_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void gvReapply_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetRowData();
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable"] = dt;
                    gvReapply.DataSource = dt;
                    gvReapply.DataBind();

                    //for (int i = 0; i < gvReapply.Rows.Count - 1; i++)
                    //{
                    //    gvReapply.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    //}
                    SetPreviousData();
                }
            }
        }


        private void SetRowData()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtDescription = (TextBox)gvReapply.Rows[rowIndex].Cells[1].FindControl("txtDescription");
                        TextBox txtSpecsDrawing = (TextBox)gvReapply.Rows[rowIndex].Cells[2].FindControl("txtSpecsDrawing");
                        TextBox txtMakerBrand = (TextBox)gvReapply.Rows[rowIndex].Cells[3].FindControl("txtMakerBrand");
                        TextBox txtQuantity =
                          (TextBox)gvReapply.Rows[rowIndex].Cells[4].FindControl("txtQuantity");
                        DropDownList ddUOM =
                          (DropDownList)gvReapply.Rows[rowIndex].Cells[5].FindControl("ddUOM");
                        TextBox txtFileSize =
                              (TextBox)gvReapply.Rows[rowIndex].Cells[6].FindControl("txtFileSize");
                        TextBox txtPurpose =
                          (TextBox)gvReapply.Rows[rowIndex].Cells[7].FindControl("txtPurpose");
                        TextBox txtProcess =
                          (TextBox)gvReapply.Rows[rowIndex].Cells[8].FindControl("txtProcess");
                        TextBox txtRemarks =
                          (TextBox)gvReapply.Rows[rowIndex].Cells[9].FindControl("txtRemarks");
                        FileUpload fuAttachment =
                          (FileUpload)gvReapply.Rows[rowIndex].Cells[10].FindControl("fuAttachment");
                        TextBox txtFileName =
                          (TextBox)gvReapply.Rows[rowIndex].Cells[11].FindControl("txtFileName");


                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["RdRefId"] = string.Empty;
                        dtCurrentTable.Rows[i - 1]["RdUnitOfMeasure"] = ddUOM.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["RdDescription"] = txtDescription.Text;
                        dtCurrentTable.Rows[i - 1]["RdSpecs"] = txtSpecsDrawing.Text;
                        dtCurrentTable.Rows[i - 1]["RdMaker"] = txtMakerBrand.Text;
                        dtCurrentTable.Rows[i - 1]["RdQuantity"] = txtQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["Col5"] = ddUOM.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["RdPurpose"] = txtRemarks.Text;
                        dtCurrentTable.Rows[i - 1]["RdProcess"] = txtRemarks.Text;
                        dtCurrentTable.Rows[i - 1]["RdRemarks"] = txtRemarks.Text;
                        rowIndex++;
                    }

                    ViewState["CurrentTable"] = dtCurrentTable;

                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
  
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtDescription = (TextBox)gvReapply.Rows[rowIndex].Cells[1].FindControl("txtDescription");
                        TextBox txtSpecsDrawing = (TextBox)gvReapply.Rows[rowIndex].Cells[2].FindControl("txtSpecsDrawing");
                        TextBox txtMakerBrand =
                          (TextBox)gvReapply.Rows[rowIndex].Cells[3].FindControl("txtMakerBrand");
                        TextBox txtQuantity =
                          (TextBox)gvReapply.Rows[rowIndex].Cells[4].FindControl("txtQuantity");
                        DropDownList ddUOM =
                          (DropDownList)gvReapply.Rows[rowIndex].Cells[5].FindControl("ddUOM");
                        TextBox txtFileSize =
                              (TextBox)gvReapply.Rows[rowIndex].Cells[6].FindControl("txtFileSize");
                        TextBox txtPurpose =
                          (TextBox)gvReapply.Rows[rowIndex].Cells[7].FindControl("txtPurpose");
                        TextBox txtProcess =
                          (TextBox)gvReapply.Rows[rowIndex].Cells[8].FindControl("txtProcess");
                        TextBox txtRemarks =
                          (TextBox)gvReapply.Rows[rowIndex].Cells[9].FindControl("txtRemarks");
                        FileUpload fuAttachment =
                          (FileUpload)gvReapply.Rows[rowIndex].Cells[10].FindControl("fuAttachment");
                        TextBox txtFileName =
                          (TextBox)gvReapply.Rows[rowIndex].Cells[11].FindControl("txtFileName");

                        //ddUOM.Items.FindByValue(dt.Rows[i]["RdUnitOfMeasure"].ToString()).Selected = true;
                        txtDescription.Text = dt.Rows[i]["RdDescription"].ToString();
                        txtSpecsDrawing.Text = dt.Rows[i]["RdSpecs"].ToString();
                        txtMakerBrand.Text = dt.Rows[i]["RdMaker"].ToString();
                        txtQuantity.Text = dt.Rows[i]["RdQuantity"].ToString();
                        ddUOM.SelectedValue = dt.Rows[i]["Col5"].ToString();
                        txtPurpose.Text = dt.Rows[i]["RdPurpose"].ToString();
                        txtProcess.Text = dt.Rows[i]["RdProcess"].ToString();
                        txtRemarks.Text = dt.Rows[i]["RdRemarks"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

        private void AddNewRow()
        {
            try
            {

                int rowIndex = 0;

                if (ViewState["CurrentTable"] != null)
                {

                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    DataRow drCurrentRow = null;
                    if (dtCurrentTable.Rows.Count > 0)
                    {

                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            TextBox txtDescription =
                              (TextBox)gvReapply.Rows[rowIndex].Cells[1].FindControl("txtDescription");
                            TextBox txtSpecsDrawing =
                              (TextBox)gvReapply.Rows[rowIndex].Cells[2].FindControl("txtSpecsDrawing");
                            TextBox txtMakerBrand =
                              (TextBox)gvReapply.Rows[rowIndex].Cells[3].FindControl("txtMakerBrand");
                            TextBox txtQuantity =
                              (TextBox)gvReapply.Rows[rowIndex].Cells[4].FindControl("txtQuantity");
                            DropDownList ddUOM =
                              (DropDownList)gvReapply.Rows[rowIndex].Cells[5].FindControl("ddUOM");
                            TextBox txtFileSize =
                              (TextBox)gvReapply.Rows[rowIndex].Cells[6].FindControl("txtFileSize");
                            TextBox txtPurpose =
                              (TextBox)gvReapply.Rows[rowIndex].Cells[7].FindControl("txtPurpose");
                            TextBox txtProcess =
                              (TextBox)gvReapply.Rows[rowIndex].Cells[8].FindControl("txtProcess");
                            TextBox txtRemarks =
                              (TextBox)gvReapply.Rows[rowIndex].Cells[9].FindControl("txtRemarks");
                            FileUpload fuAttachment =
                              (FileUpload)gvReapply.Rows[rowIndex].Cells[10].FindControl("fuAttachment");
                            TextBox txtFileName =
                              (TextBox)gvReapply.Rows[rowIndex].Cells[11].FindControl("txtFileName");


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
                                            if (entity.TableName == "MT_UnitOfMeasure")
                                            {
                                                ddUOM.Items.Add(item);
                                            }

                                        }

                                    }

                                }
                            }

                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["RowNumber"] = i + 1;

                            dtCurrentTable.Rows[i - 1]["RdRefId"] = string.Empty;
                            dtCurrentTable.Rows[i - 1]["RdUnitOfMeasure"] = ddUOM.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["RdDescription"] = txtDescription.Text;
                            dtCurrentTable.Rows[i - 1]["RdSpecs"] = txtSpecsDrawing.Text;
                            dtCurrentTable.Rows[i - 1]["RdMaker"] = txtMakerBrand.Text;
                            dtCurrentTable.Rows[i - 1]["RdQuantity"] = txtQuantity.Text;
                            dtCurrentTable.Rows[i - 1]["Col5"] = ddUOM.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["RdPurpose"] = txtPurpose.Text;
                            dtCurrentTable.Rows[i - 1]["RdProcess"] = txtProcess.Text;
                            dtCurrentTable.Rows[i - 1]["RdRemarks"] = txtRemarks.Text;

                            rowIndex++;
                        }
                        dtCurrentTable.Rows.Add(drCurrentRow);
                        ViewState["CurrentTable"] = dtCurrentTable;

                        gvReapply.DataSource = dtCurrentTable;
                        gvReapply.DataBind();

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('ViewState is null');", true);
                }
                SetPreviousData();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.StackTrace.ToString() + "');", true);
            }
        }


        private Int32 setControlNumber()
        {
            return BLL.RFQ_TRANSACTION_CountRequestHead(DateTime.Now.Year.ToString()) + 1;
        }

        private string setRFQNumberWithPrefix()
        {
            string retVal = string.Empty;

            retVal = Session["DivisionCode"].ToString() + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + COMMON.controlNoZeroPrefix(setControlNumber().ToString().Length.ToString()) + setControlNumber().ToString();

            return retVal;
        }

        protected void btnSubmit2_Click(object sender, EventArgs e)
        {
            try
            {

                try
                {
                    bool isNoError = false;
                    string successHead = string.Empty;
                    string successDetails = string.Empty;
                    string successStatus = string.Empty;
                    int errorCounter = 0;
                    string itemDescriptionAndSpecs = string.Empty;
                    string errorDescription = string.Empty;
                    string descriptionSpecsExistInTable = string.Empty;
                    int errorCounterExisting = 0;

                    if (ddCategory.Text.Length <= 0)
                    {
                        errorCounter++;
                        ddCategory.Style.Add("background-color", "#f44336");
                        errorDescription = "CATEGORY field must not be blank";
                    }
                    else
                    {
                        ddCategory.Style.Add("background-color", "White");

                        for (int i = 0; i < gvReapply.Rows.Count; i++)
                        {
                            TextBox txtDescription = (TextBox)gvReapply.Rows[i].Cells[1].FindControl("txtDescription");
                            TextBox txtSpecsDrawing = (TextBox)gvReapply.Rows[i].Cells[2].FindControl("txtSpecsDrawing");
                            TextBox txtMakerBrand = (TextBox)gvReapply.Rows[i].Cells[3].FindControl("txtMakerBrand");
                            TextBox txtQuantity = (TextBox)gvReapply.Rows[i].Cells[4].FindControl("txtQuantity");
                            DropDownList ddUOM = (DropDownList)gvReapply.Rows[i].Cells[5].FindControl("ddUOM");
                            TextBox txtPurpose = (TextBox)gvReapply.Rows[i].Cells[7].FindControl("txtPurpose");
                            TextBox txtProcess = (TextBox)gvReapply.Rows[i].Cells[8].FindControl("txtProcess");
                            TextBox txtRemarks = (TextBox)gvReapply.Rows[i].Cells[9].FindControl("txtRemarks");

                            txtDescription.Style.Add("background-color", "White");
                            txtSpecsDrawing.Style.Add("background-color", "White");
                            txtMakerBrand.Style.Add("background-color", "White");
                            txtQuantity.Style.Add("background-color", "White");
                            txtPurpose.Style.Add("background-color", "White");
                            txtProcess.Style.Add("background-color", "White");
                            txtRemarks.Style.Add("background-color", "White");


                            if (!string.IsNullOrEmpty(txtSpecsDrawing.Text))
                            {

                                if (string.IsNullOrEmpty(txtDescription.Text))
                                {
                                    txtDescription.Style.Add("background-color", "#f44336");
                                    errorCounter++;
                                    errorDescription = "DESCRIPTION field must not be blank";
                                }
                                if (string.IsNullOrEmpty(txtSpecsDrawing.Text))
                                {
                                    txtSpecsDrawing.Style.Add("background-color", "#f44336");
                                    errorCounter++;
                                    errorDescription = "DRAWING/SPECIFICATION field must not be blank";
                                }
                                if (!COMMON.isNumeric(txtQuantity.Text.Trim(), System.Globalization.NumberStyles.AllowThousands) || String.IsNullOrEmpty(txtQuantity.Text.Trim()))
                                {
                                    txtQuantity.Style.Add("background-color", "#f44336");
                                    errorCounter++;
                                    errorDescription = "PLEASE ENTER A VALID QUANTITY";
                                }
                                if (string.IsNullOrEmpty(txtProcess.Text))
                                {
                                    txtProcess.Style.Add("background-color", "#f44336");
                                    errorCounter++;
                                    errorDescription = "PROCESS/MACHINE field must not be blank";
                                }
                                if (string.IsNullOrEmpty(txtPurpose.Text))
                                {
                                    txtPurpose.Style.Add("background-color", "#f44336");
                                    errorCounter++;
                                    errorDescription = "PURPOSE/USE & PICTURE OF ITEM field must not be blank";
                                }

                            
                                itemDescriptionAndSpecs += (txtDescription.Text.Trim().ToUpper().Replace("'", "''") + txtSpecsDrawing.Text.Trim().ToUpper().Replace("'", "''")) + ",";
                                descriptionSpecsExistInTable += "'" + (txtDescription.Text.Trim().ToUpper().Replace("'", "''") + txtSpecsDrawing.Text.Trim().ToUpper().Replace("'", "''")) + "',";

                            }


                        }

                        //--------------------------------------------------------------------------------------------------------------------------------------------
                        List<string> completeItems = new List<string>(itemDescriptionAndSpecs.Trim().Split(',').Select(t => t.Trim()));
                        bool isUnique = completeItems.Count == completeItems.Distinct().Count();

                        if (!isUnique)
                        {
                            errorCounter++;
                            errorDescription = "System detected duplicate entry. <br/> Please check your items! <br/> Check description and specification.";
                        }
                        

                    }

                    if (errorCounter <= 0 && errorCounterExisting <= 0)
                    {
                        string queryDetails = string.Empty;
                        string queryHead = string.Empty;
                        string queryStatus = string.Empty;
                        string queryHistoryOfUpdates = string.Empty;
                        string attachmentFiles = string.Empty;
                        int rd_Query_Counter = 0;
                        string query_Success = string.Empty;
                        string temp_RFQNo = string.Empty;

                        string queryBeginPart = string.Empty;
                        string queryEndPart = string.Empty;
                        int queryHeadCounter = 1;
                        int queryStatusCounter = 0;
                        int queryHistoryUpdatesCounter = 0;


                        queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";

                        temp_RFQNo = setRFQNumberWithPrefix();
                        queryStatusCounter = 1;

                        if (ddCategory.SelectedValue == "1015" || ddCategory.SelectedValue == "3")
                        {
                            // If selected category is EQUIPMENT & SERVICES or EQUIPMENT / SERVICES then assigned it to category "EQUIPMENT AND SERVICES (RENZ)"
                            queryHead = "INSERT INTO Request_HEAD (RFQNO, Division, Department, Section, Requester, TransactionDate, Category) " +
                                    "VALUES ('" + temp_RFQNo + "','" + Session["Division"].ToString() + "','" + Session["Department"].ToString() +
                                    "','" + Session["Section"].ToString() + "','" + Session["LcRefId"].ToString() + "',GETDATE(),'1016') ";
                        }
                        else if (ddCategory.SelectedValue == "1013") //FOR MAM JEWEL OLD CATEGORY AUTOMATIC TRANSFER TO NEW CATEGORY
                        {
                            queryHead = "INSERT INTO Request_HEAD (RFQNO, Division, Department, Section, Requester, TransactionDate, Category) " +
                                    "VALUES ('" + temp_RFQNo + "','" + Session["Division"].ToString() + "','" + Session["Department"].ToString() +
                                    "','" + Session["Section"].ToString() + "','" + Session["LcRefId"].ToString() + "',GETDATE(),'1018') ";
                        }
                        else if (ddCategory.SelectedValue == "1006") //FOR MAM JUDY OLD CATEGORY AUTOMATIC TRANSFER TO NEW CATEGORY
                        {
                            queryHead = "INSERT INTO Request_HEAD (RFQNO, Division, Department, Section, Requester, TransactionDate, Category) " +
                                    "VALUES ('" + temp_RFQNo + "','" + Session["Division"].ToString() + "','" + Session["Department"].ToString() +
                                    "','" + Session["Section"].ToString() + "','" + Session["LcRefId"].ToString() + "',GETDATE(),'1017') ";
                        }
                        else
                        {
                            queryHead = "INSERT INTO Request_HEAD (RFQNO, Division, Department, Section, Requester, TransactionDate, Category) " +
                                    "VALUES ('" + temp_RFQNo + "','" + Session["Division"].ToString() + "','" + Session["Department"].ToString() +
                                    "','" + Session["Section"].ToString() + "','" + Session["LcRefId"].ToString() + "',GETDATE(),'" + ddCategory.SelectedValue + "') ";
                        }



                        queryStatus = "INSERT INTO Request_Status (RFQNO, Requester, ProdManager, Purchasing, Supplier, PurchasingIncharge, DepartmentManager, DivisionManager) " +
                                  "VALUES ('" + temp_RFQNo + "','" + Session["LcRefId"].ToString() + "','0','0','0','0','0','0') ";


                        for (int i = 0; i < gvReapply.Rows.Count; i++)
                        {
                            TextBox txtDescription = (TextBox)gvReapply.Rows[i].Cells[1].FindControl("txtDescription");
                            TextBox txtSpecsDrawing = (TextBox)gvReapply.Rows[i].Cells[2].FindControl("txtSpecsDrawing");
                            TextBox txtMakerBrand = (TextBox)gvReapply.Rows[i].Cells[3].FindControl("txtMakerBrand");
                            TextBox txtQuantity = (TextBox)gvReapply.Rows[i].Cells[4].FindControl("txtQuantity");
                            DropDownList ddUOM = (DropDownList)gvReapply.Rows[i].Cells[5].FindControl("ddUOM");
                            TextBox txtPurpose = (TextBox)gvReapply.Rows[i].Cells[7].FindControl("txtPurpose");
                            TextBox txtProcess = (TextBox)gvReapply.Rows[i].Cells[8].FindControl("txtProcess");
                            TextBox txtRemarks = (TextBox)gvReapply.Rows[i].Cells[9].FindControl("txtRemarks");
                            FileUpload fuAttachment = (FileUpload)gvReapply.Rows[i].Cells[10].FindControl("fuAttachment");
                            Label lblRefId = (Label)gvReapply.Rows[i].Cells[1].FindControl("lblRefId");


                            if (!string.IsNullOrEmpty(txtSpecsDrawing.Text))
                            {

                                if (fuAttachment.HasFile)
                                {
                                    string fileNameApplication = System.IO.Path.GetFileName(fuAttachment.FileName);
                                    string fileExtensionApplication = System.IO.Path.GetExtension(fileNameApplication);
                                    string newFile = fileNameApplication;

                                    if (!System.IO.Directory.Exists(Server.MapPath("~/IO_Request/" + temp_RFQNo)))
                                    {
                                        System.IO.Directory.CreateDirectory(Server.MapPath("~/IO_Request/" + temp_RFQNo));
                                    }
                                    if (!System.IO.File.Exists(Server.MapPath("~/IO_Request/" + temp_RFQNo + "/" + newFile)))
                                    {
                                        fuAttachment.SaveAs(System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + temp_RFQNo), newFile));
                                        fuAttachment.Dispose();
                                        System.IO.File.Copy(System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + temp_RFQNo), newFile), System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + temp_RFQNo), (i.ToString() + "-" + temp_RFQNo + fileExtensionApplication)), true);
                                        System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + temp_RFQNo), newFile));
                                    }

                                    attachmentFiles = i.ToString() + "-" + temp_RFQNo + fileExtensionApplication;
                                }

                                queryDetails += "INSERT INTO Request_Details (RFQNO, Description, Specs, Maker, Quantity, UOM, Remarks, Attachment, Purpose, Process) " +
                                                    "VALUES ('" + temp_RFQNo + "','" + txtDescription.Text.Replace("'", "''") + "','" + txtSpecsDrawing.Text.Replace("'", "''") +
                                                    "','" + txtMakerBrand.Text.Replace("'", "''") + "','" + txtQuantity.Text.Trim() + "','" + ddUOM.SelectedValue + "','" + txtRemarks.Text.Replace("'", "''").Replace("[", "").Replace("]", "") + "','" + attachmentFiles + "','" + txtPurpose.Text.Replace("'", "''") + "','" + txtProcess.Text.Replace("'", "''") + "') ";


                                rd_Query_Counter++;

                            }

                        }

                        // IF NO ATTACHMENT
                        if (string.IsNullOrEmpty(attachmentFiles))
                        {
                            if (!System.IO.Directory.Exists(Server.MapPath("~/IO_Request/" + temp_RFQNo)))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/IO_Request/" + temp_RFQNo));
                            }
                        }

                        queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";

                        if (!string.IsNullOrEmpty(queryHistoryOfUpdates))
                        {
                            query_Success = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + queryHead + queryStatus + queryDetails + queryHistoryOfUpdates + queryEndPart).ToString();
                        }
                        else
                        {
                            query_Success = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + queryHead + queryStatus + queryDetails + queryEndPart).ToString();
                        }

                        if (query_Success == (rd_Query_Counter + queryHeadCounter + queryStatusCounter + queryHistoryUpdatesCounter).ToString())
                        {
                            Session["successMessage"] = "RFQ NUMBER : <b>" + temp_RFQNo + "</b> HAS BEEN SUCCESSFULLY SAVED.";
                            Session["successTransactionName"] = "RFQ_ALLREQUEST";
                            Session["successReturnPage"] = "RFQ_AllRequest.aspx";

                            Response.Redirect("SuccessPage.aspx");
                        }


                    }
                    else
                    {
                        // errorCounter is greater than 0
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + errorDescription.ToUpper() + "');", true);
                    }

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.StackTrace.ToString() + "');", true);
                }
                

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!System.IO.File.Exists(Server.MapPath("~/RFQ_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx")))
                {
                    System.IO.File.Copy(Server.MapPath("~/RFQ_XLS/RFQ_AllRequest_Report.xlsx"), Server.MapPath("~/RFQ_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx"));
                }
                else
                {
                    System.IO.File.Delete(Server.MapPath("~/RFQ_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx"));
                    System.IO.File.Copy(Server.MapPath("~/RFQ_XLS/RFQ_AllRequest_Report.xlsx"), Server.MapPath("~/RFQ_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx"));
                }

                List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                Entities_RFQ_RequestEntry status = new Entities_RFQ_RequestEntry();

                status.DrFrom = txtFrom.Text.Trim();
                status.DrTo = txtTo.Text.Trim();

                if (ddItemStatus.SelectedItem.Text.ToUpper() == "ALL")
                {
                    list = BLL.RFQ_TRANSACTION_AllRequest_ByDateRange_Reporting(status);
                }
                else if (ddItemStatus.SelectedItem.Text.ToUpper() == "ALL" && !string.IsNullOrEmpty(ddCategoryExport.SelectedItem.Text))
                {
                    list = BLL.RFQ_TRANSACTION_AllRequest_ByDateRange_Reporting(status).Where(itm => itm.CategoryName.ToUpper() == ddCategoryExport.SelectedItem.Text.ToUpper()).ToList();
                }
                else if (ddItemStatus.SelectedItem.Text.ToUpper() != "ALL" && !string.IsNullOrEmpty(ddCategoryExport.SelectedItem.Text))
                {
                    list = BLL.RFQ_TRANSACTION_AllRequest_ByDateRange_Reporting(status).Where(itm => itm.CategoryName.ToUpper() == ddCategoryExport.SelectedItem.Text.ToUpper() && itm.StatAll == ddItemStatus.SelectedItem.Text.ToUpper()).ToList();
                }
                else
                {
                    list = BLL.RFQ_TRANSACTION_AllRequest_ByDateRange_Reporting(status).Where(itm => itm.StatAll == ddItemStatus.SelectedItem.Text.ToUpper()).ToList();
                }

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        string category = Session["CategoryAccess"].ToString();
                        int cnt = 4;
                        string path = Server.MapPath("~/RFQ_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx");
                        Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + path);
                        FileStream fsBI = new FileStream(path, FileMode.Open);
                        using (SLDocument draft = new SLDocument(fsBI, "Sheet1"))
                        {

                            foreach (Entities_RFQ_RequestEntry entity in list)
                            {
                                draft.SetCellValue("A" + cnt.ToString(), entity.RdRfqNo);
                                draft.SetCellValue("B" + cnt.ToString(), entity.RdDescription);
                                draft.SetCellValue("C" + cnt.ToString(), entity.RdSpecs);
                                draft.SetCellValue("D" + cnt.ToString(), entity.RdMaker);
                                draft.SetCellValue("E" + cnt.ToString(), entity.RdQuantity);
                                draft.SetCellValue("F" + cnt.ToString(), entity.RdUnitOfMeasure);
                                draft.SetCellValue("G" + cnt.ToString(), entity.RdRemarks);
                                draft.SetCellValue("H" + cnt.ToString(), entity.CategoryName);
                                draft.SetCellValue("I" + cnt.ToString(), entity.StatAll);
                                draft.SetCellValue("J" + cnt.ToString(), entity.TransactionDate);
                                draft.SetCellValue("K" + cnt.ToString(), entity.Report_BuyerName);

                                draft.SetCellValue("M" + cnt.ToString(), entity.Requester);

                                if (!string.IsNullOrEmpty(entity.ApprovedDate))
                                {
                                    draft.SetCellValue("N" + cnt.ToString(), entity.ApprovedDate);
                                    draft.SetCellValue("O" + cnt.ToString(), entity.RdResponsePrice);
                                    draft.SetCellValue("P" + cnt.ToString(), entity.RdResponseLead);
                                    draft.SetCellValue("Q" + cnt.ToString(), entity.RdResponseRemarks);
                                    draft.SetCellValue("R" + cnt.ToString(), entity.PurchasingRemarks);
                                }


                                if (!string.IsNullOrEmpty(category))
                                {
                                    if (int.Parse(category) > 0)
                                    {
                                        draft.SetCellValue("L" + cnt.ToString(), entity.RdSupplierName);
                                    }
                                }

                                cnt++;
                            }

                            fsBI.Close();
                            draft.SaveAs(path);

                        }

                        Response.Redirect("RFQ_XLS/" + Session["LcRefId"].ToString() + "_AllRequest.xlsx", false);

                    }
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


        private static bool isItemManualApproved(string rfqNo)
        {
            bool isExist = false;

            DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection conn = fact.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_SOFRA"].ConnectionString;

            DbDataReader reader = null;
            DbCommand cmd = fact.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TOP 1 * FROM Request_HistoryOfApproval WITH (NOLOCK) WHERE TransactionName = 'PurchasingDivManager' AND ApprovedBy = '" + ConfigurationManager.AppSettings["AUTO-APPROVED-ACCOUNT"].ToString() + "' AND RFQNo = '" + rfqNo + "'";

            conn.Open();
            cmd.Connection = conn;

            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                isExist = true;
            }

            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;

            return isExist;
        }

















        

    }
}
