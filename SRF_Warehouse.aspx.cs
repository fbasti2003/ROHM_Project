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
    public partial class SRF_Warehouse : System.Web.UI.Page
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
                //list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntry(hidSearch.Value);
                //list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntry(txtSearch.Text).Where(itm => itm.Category == Session["CategoryAccess"].ToString()).ToList();
                if (!string.IsNullOrEmpty(ddStatus.SelectedItem.Text))
                {
                    if (ddStatus.SelectedItem.Text.ToUpper() == "DELIVERED")
                    {
                        list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntry(txtSearch.Text).Where(itm => itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper()).ToList();
                    }
                    else
                    {
                        list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntry(txtSearch.Text).Where(itm => itm.StatAll.ToUpper().Contains(ddStatus.SelectedItem.Text.ToUpper())).ToList();
                    }
                }
                else
                {
                    if (chkIncludeDelivered.Checked)
                    {
                        list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntry(txtSearch.Text);
                    }
                    else
                    {
                        list = BLL.SRF_TRANSACTION_Warehouse_ReceivingEntry(txtSearch.Text).Where(itm => itm.StatAll.ToUpper() != "DELIVERED").ToList().Where(itm => itm.StatAll.ToUpper() != "CLOSED").ToList();
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
                        gvData.EmptyDataText = "NO RECORD(S) FOUND!";
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

                Label lblCTRLNo = row.FindControl("lblCTRLNo") as Label;
                Label lblStatus = row.FindControl("lblStatus") as Label;

                //if (e.CommandName == "lblCtrl_Command")
                //{
                //    string URL = "~/SRF_RequestEntry.aspx?SRF_ControlNo_From_Details=" + CryptorEngine.Encrypt(lblCtrl.Text.Trim(), true);

                //    URL = Page.ResolveClientUrl(URL);
                //    lblCtrl.OnClientClick = "window.open('" + URL + "'); return false;";
                //}

                if (e.CommandName == "lbPrint_Command")
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
                                        ////ATTACHMENT 1
                                        //if (entityAtt.Warehouse_Attachment.Contains("1"))
                                        //{
                                        //    fu1.Visible = false;
                                        //    linkAttachment1.Visible = true;
                                        //    linkAttachment1.Text = entityAtt.Warehouse_Attachment;

                                        //    //fu2.Visible = true;
                                        //    //fu3.Visible = true;
                                        //    //fu4.Visible = true;
                                        //    //fu5.Visible = true;
                                        //}

                                        ////ATTACHMENT 2
                                        //if (entityAtt.Warehouse_Attachment.Contains("2"))
                                        //{
                                        //    fu2.Visible = false;
                                        //    linkAttachment2.Visible = true;
                                        //    linkAttachment2.Text = entityAtt.Warehouse_Attachment;

                                        //}

                                        ////ATTACHMENT 3
                                        //if (entityAtt.Warehouse_Attachment.Contains("3"))
                                        //{
                                        //    fu3.Visible = false;
                                        //    linkAttachment3.Visible = true;
                                        //    linkAttachment3.Text = entityAtt.Warehouse_Attachment;
                                        //}


                                        ////ATTACHMENT 4
                                        //if (entityAtt.Warehouse_Attachment.Contains("4"))
                                        //{
                                        //    fu4.Visible = false;
                                        //    linkAttachment4.Visible = true;
                                        //    linkAttachment4.Text = entityAtt.Warehouse_Attachment;
                                        //}

                                        ////ATTACHMENT 5
                                        //if (entityAtt.Warehouse_Attachment.Contains("5"))
                                        //{
                                        //    fu5.Visible = false;
                                        //    linkAttachment5.Visible = true;
                                        //    linkAttachment5.Text = entityAtt.Warehouse_Attachment;
                                        //}


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

                    if (lblStatus.Text.Contains("DELIVERY IN-PROGRESS") || lblStatus.Text == "DELIVERED WITH PENDING ITEMS")
                    {
                        btnInProgress.Visible = false;
                        btnConfirmDelivery.Visible = true;
                        divAttachment.Visible = true;
                        divActualDelivery.Visible = true;

                        List<Entities_SRF_RequestEntry> detailsActualItemName = new List<Entities_SRF_RequestEntry>();
                        detailsActualItemName = BLL.SRF_TRANSACTION_RequestEntry_Details_ByControlNo_ForWarehouse2(lblCTRLNo.Text.Trim());

                        if (detailsActualItemName != null)
                        {
                            if (detailsActualItemName.Count > 0)
                            {
                                ddActualItemName.Items.Add("");

                                int totalQty = 0;
                                int actualQty = 0;
                                int neededQuantity = 0;

                                foreach (Entities_SRF_RequestEntry eActualItemName in detailsActualItemName)
                                {
                                    totalQty = int.Parse(eActualItemName.TotalQuantity);
                                    actualQty = int.Parse(!string.IsNullOrEmpty(eActualItemName.Warehouse_TotalActualQuantity) ? eActualItemName.Warehouse_TotalActualQuantity : "0");
                                    neededQuantity = totalQty - actualQty;

                                    ListItem item = new ListItem();
                                    item.Text = "[" + eActualItemName.RefId + "] " + eActualItemName.ItemName;
                                    item.Value = eActualItemName.RefId + "(" + neededQuantity.ToString() + ")";

                                    ddActualItemName.Items.Add(item);
                                }
                            }
                        }

                        // SET DISABLED THE CHECKBOX
                        if (lblStatus.Text.Contains("DELIVERY IN-PROGRESS"))
                        {
                            for (int i = 0; i < gvDetails.Rows.Count; i++)
                            {
                                CheckBox cbAITD = (CheckBox)gvDetails.Rows[i].Cells[11].FindControl("cbAITD");
                                cbAITD.Enabled = false;
                            }

                        }

                        List<Entities_SRF_RequestEntry> detailsActualQuantity = new List<Entities_SRF_RequestEntry>();
                        detailsActualQuantity = BLL.SRF_TRANSACTION_GetActualDeliveryByCTRLNo(lblCTRLNo.Text.Trim());

                        if (detailsActualQuantity != null)
                        {
                            if (detailsActualQuantity.Count > 0)
                            {
                                gvActualDelivery.DataSource = detailsActualQuantity;
                                gvActualDelivery.DataBind();
                                
                            }
                            else
                            {
                                DataTable dt2 = new DataTable();
                                DataRow dr2 = null;
                                dt2.Columns.Add(new DataColumn("Warehouse_DetailsRefId", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Warehouse_ItemName", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Warehouse_TotalActualQuantity", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Warehouse_NewEntry", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Warehouse_DeliveredDate", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Warehouse_WithDocuments", typeof(string)));

                                dr2 = dt2.NewRow();
                                dr2["Warehouse_DetailsRefId"] = string.Empty;
                                dr2["Warehouse_ItemName"] = string.Empty;
                                dr2["Warehouse_TotalActualQuantity"] = string.Empty;
                                dr2["Warehouse_NewEntry"] = string.Empty;
                                dr2["Warehouse_DeliveredDate"] = string.Empty;
                                dr2["Warehouse_WithDocuments"] = string.Empty;

                                dt2.Rows.Add(dr2);

                                gvActualDelivery.DataSource = dt2;
                                gvActualDelivery.DataBind();
                            }

                        }
                        else
                        {
                            DataTable dt = new DataTable();
                            DataRow dr = null;
                            dt.Columns.Add(new DataColumn("Warehouse_DetailsRefId", typeof(string)));
                            dt.Columns.Add(new DataColumn("Warehouse_ItemName", typeof(string)));
                            dt.Columns.Add(new DataColumn("Warehouse_TotalActualQuantity", typeof(string)));
                            dt.Columns.Add(new DataColumn("Warehouse_NewEntry", typeof(string)));
                            dt.Columns.Add(new DataColumn("Warehouse_DeliveredDate", typeof(string)));
                            dt.Columns.Add(new DataColumn("Warehouse_WithDocuments", typeof(string)));

                            dr = dt.NewRow();
                            dr["Warehouse_DetailsRefId"] = string.Empty;
                            dr["Warehouse_ItemName"] = string.Empty;
                            dr["Warehouse_TotalActualQuantity"] = string.Empty;
                            dr["Warehouse_NewEntry"] = string.Empty;
                            dr["Warehouse_DeliveredDate"] = string.Empty;
                            dr["Warehouse_WithDocuments"] = string.Empty;

                            dt.Rows.Add(dr);


                            gvActualDelivery.DataSource = dt;
                            gvActualDelivery.DataBind();
                        }


                    }
                    else
                    {
                        btnInProgress.Visible = true;
                        btnConfirmDelivery.Visible = false;
                        divAttachment.Visible = false;
                        divActualDelivery.Visible = false;
                        divNote.Visible = true;
                    }


                    if (lblStatus.Text == "DELIVERED WITH PENDING ITEMS")
                    {
                        btnInProgress.Visible = true;
                        btnConfirmDelivery.Visible = false;
                        divAttachment.Visible = false;
                        divActualDelivery.Visible = false;
                        divNote.Visible = true;


                    }

                    if (lblStatus.Text == "DELIVERED")
                    {
                        btnInProgress.Visible = false;
                        btnConfirmDelivery.Visible = false;
                        divAttachment.Visible = true;
                        divActualDelivery.Visible = true;

                        List<Entities_SRF_RequestEntry> detailsActualQuantity2 = new List<Entities_SRF_RequestEntry>();
                        detailsActualQuantity2 = BLL.SRF_TRANSACTION_GetActualDeliveryByCTRLNo(lblCTRLNo.Text.Trim());

                        if (detailsActualQuantity2 != null)
                        {
                            if (detailsActualQuantity2.Count > 0)
                            {
                                gvActualDelivery.DataSource = detailsActualQuantity2;
                                gvActualDelivery.DataBind();
                            }

                        }

                        lbAddNewActual.Enabled = false;
                    }

                    hiddenUserId.Value = Session["LcRefId"].ToString();

                    lbPrint.OnClientClick = "showDialog(); return false;";


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
                        lblStatus.Style.Add("height", "30px");
                        lblStatus.Style.Add("background-color", "#00C851");
                    }

                    if (lblStatus.Text.Contains("DELIVERY IN-PROGRESS"))
                    {
                        lblStatus.Style.Add("background-color", "#6F6B02");
                        //lblStatus.Text += "<table><tr>DELIVERY IN-PROGRESS</tr>";

                        //List<Entities_SRF_RequestEntry> inProgress = new List<Entities_SRF_RequestEntry>();
                        //inProgress = BLL.SRF_TRANSACTION_GetInProgressByCTRLNo(lblCTRLNo.Text.Trim());

                        //if (inProgress != null)
                        //{
                        //    if (inProgress.Count > 0)
                        //    {
                        //        foreach (Entities_SRF_RequestEntry eProgress in inProgress)
                        //        {
                        //            lblStatus.Text += "<tr><td>" + eProgress.Warehouse_ItemName + "</td></tr>";
                        //        }
                        //    }
                        //}

                        //lblStatus.Text += "</table>";

                    }

                    if (lblStatus.Text == "DELIVERED")
                    {
                        lblStatus.Style.Add("height", "30px");
                        lblStatus.Style.Add("background-color", "#3DB7F9");
                    }

                    if (lblStatus.Text == "DELIVERED WITH PENDING ITEMS")
                    {
                        lblStatus.Style.Add("height", "30px");
                        lblStatus.Style.Add("background-color", "#5499C7");
                    }

                    if (lblStatus.Text == "DISAPPROVED")
                    {
                        lblStatus.Style.Add("background-color", "#ffbb33");
                    }

                    if (lblStatus.Text == "CANCELED")
                    {
                        lblStatus.Style.Add("background-color", "blue");
                    }

                    if (lblStatus.Text == "CLOSED")
                    {
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
                

                if (txtQuantity.Text.Trim() == txtActualQty.Text.Trim())
                {
                    cbAITD.Enabled = false;
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
                        Session["successReturnPage"] = "SRF_Warehouse.aspx";
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

                Response.Redirect("SRF_Warehouse.aspx");
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



                //if (!fu1.HasFile && !fu2.HasFile && !fu3.HasFile && !fu4.HasFile && !fu5.HasFile)
                //{
                //    error += "ATTACHMENT REQUIRED! Please attached required attachment file (8106, DR WITH 8106 and other supporting documents)";
                //}

                //// DELETE THE OLD RECORDS
                //query1 += "DELETE FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WHERE SRFNumber = '" + lblCtrlNo2.Text.Trim() + "' ";

                //for (int i = 0; i < gvDetails.Rows.Count; i++)
                //{
                //    Label lblRefId = (Label)gvDetails.Rows[i].Cells[0].FindControl("lblRefId");
                //    Label txtQuantity = (Label)gvDetails.Rows[i].Cells[5].FindControl("txtQuantity");
                //    Label txtActualQty = (Label)gvDetails.Rows[i].Cells[8].FindControl("txtActualQty");

                //    if (txtQuantity.Text == txtActualQty.Text)
                //    {
                //        //DO NOTHING
                //    }
                //    else
                //    {
                //        isDiscrepancy += "[" + txtQuantity.Text + "-" + txtActualQty.Text + "]";
                //    }

                    
                //    query1 += "INSERT INTO SRF_TRANSACTION_Warehouse_Actual_Delivery (DetailsRefId,SRFNumber,Quantity,ActualQuantity,AddedBy,AddedDate) " +
                //              "VALUES ('" + lblRefId.Text.Trim() + "','" + lblCtrlNo2.Text.Trim() + "','" + txtQuantity.Text.Trim() + "','" + txtActualQty.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE()) ";

                //}

                query1 += "UPDATE SRF_TRANSACTION_Warehouse_InProgress SET InProgress = '0' WHERE CTRLNo ='" + lblCtrlNo2.Text.Trim() + "' ";

                query1 += csvActualCollections.Value;

                if (string.IsNullOrEmpty(csvActualCollections.Value))
                {
                    error += "Cannot proceed empty delivery item!";
                }
                
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

                    //// ATTACHMENT 1
                    //if (fu1.HasFile)
                    //{
                    //    query1 += "INSERT INTO SRF_TRANSACTION_Warehouse (CTRLNo,AddedBy,AddedDate,TransType,Attachment) " +
                    //             "VALUES ('" + lblCtrlNo2.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE(), '2', '1-Warehouse.pdf') ";

                    //    string filename1 = Path.GetFileName(fu1.FileName);
                    //    fu1.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + lblCtrlNo2.Text.Trim(), filename1));
                    //    fu1.Dispose();
                    //    File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename1), Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "1-Warehouse.pdf"), true);
                    //    File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename1));

                    //    attachedFiles += Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "1-Warehouse.pdf") + ",";

                    //}

                    //// ATTACHMENT 2
                    //if (fu2.HasFile)
                    //{
                    //    query1 += "INSERT INTO SRF_TRANSACTION_Warehouse (CTRLNo,AddedBy,AddedDate,TransType,Attachment) " +
                    //             "VALUES ('" + lblCtrlNo2.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE(), '2', '2-Warehouse.pdf') ";

                    //    string filename2 = Path.GetFileName(fu2.FileName);
                    //    fu2.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + lblCtrlNo2.Text.Trim(), filename2));
                    //    fu2.Dispose();
                    //    File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename2), Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "2-Warehouse.pdf"), true);
                    //    File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename2));

                    //    attachedFiles += Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "2-Warehouse.pdf") + ",";

                    //}

                    //// ATTACHMENT 3
                    //if (fu3.HasFile)
                    //{
                    //    query1 += "INSERT INTO SRF_TRANSACTION_Warehouse (CTRLNo,AddedBy,AddedDate,TransType,Attachment) " +
                    //             "VALUES ('" + lblCtrlNo2.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE(), '2', '3-Warehouse.pdf') ";

                    //    string filename3 = Path.GetFileName(fu3.FileName);
                    //    fu3.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + lblCtrlNo2.Text.Trim(), filename3));
                    //    fu3.Dispose();
                    //    File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename3), Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "3-Warehouse.pdf"), true);
                    //    File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename3));

                    //    attachedFiles += Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "3-Warehouse.pdf") + ",";

                    //}

                    //// ATTACHMENT 4
                    //if (fu4.HasFile)
                    //{
                    //    query1 += "INSERT INTO SRF_TRANSACTION_Warehouse (CTRLNo,AddedBy,AddedDate,TransType,Attachment) " +
                    //             "VALUES ('" + lblCtrlNo2.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE(), '2', '4-Warehouse.pdf') ";

                    //    string filename4 = Path.GetFileName(fu4.FileName);
                    //    fu4.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + lblCtrlNo2.Text.Trim(), filename4));
                    //    fu4.Dispose();
                    //    File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename4), Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "4-Warehouse.pdf"), true);
                    //    File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename4));

                    //    attachedFiles += Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "4-Warehouse.pdf") + ",";

                    //}

                    //// ATTACHMENT 5
                    //if (fu5.HasFile)
                    //{
                    //    query1 += "INSERT INTO SRF_TRANSACTION_Warehouse (CTRLNo,AddedBy,AddedDate,TransType,Attachment) " +
                    //             "VALUES ('" + lblCtrlNo2.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE(), '2', '5-Warehouse.pdf') ";

                    //    string filename5 = Path.GetFileName(fu5.FileName);
                    //    fu5.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + lblCtrlNo2.Text.Trim(), filename5));
                    //    fu5.Dispose();
                    //    File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename5), Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "5-Warehouse.pdf"), true);
                    //    File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename5));

                    //    attachedFiles += Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "5-Warehouse.pdf") + ",";

                    //}

                    //string attachmentFiles2 = string.Empty;
                    //int numberOfAttachment = BLL.SRF_TRANSACTION_Warehouse_Attachment_Count(lblCtrlNo2.Text.Trim());
                    //numberOfAttachment++;

                    //for (int i = 0; i < gvActualDelivery.Rows.Count; i++)
                    //{
                    //    FileUpload fuAttachment = (FileUpload)gvActualDelivery.Rows[i].Cells[5].FindControl("fuAttachment");

                    //    string fileNameApplication = System.IO.Path.GetFileName(fuAttachment.FileName);
                    //    string fileExtensionApplication = System.IO.Path.GetExtension(fileNameApplication);
                    //    string newFile = fileNameApplication;
                    //    string attachmentFiles = string.Empty;

                    //    if (fuAttachment.Visible)
                    //    {
                    //        if (fuAttachment.HasFile)
                    //        {

                    //            if (!System.IO.Directory.Exists(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim())))
                    //            {
                    //                System.IO.Directory.CreateDirectory(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()));
                    //            }
                    //            if (!System.IO.File.Exists(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + newFile)))
                    //            {
                    //                fuAttachment.SaveAs(System.IO.Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), newFile));
                    //                fuAttachment.Dispose();
                    //                System.IO.File.Copy(System.IO.Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), newFile), System.IO.Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), (numberOfAttachment.ToString() + "-Warehouse" + fileExtensionApplication)), true);
                    //                System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), newFile));
                    //            }

                    //            attachmentFiles = numberOfAttachment.ToString() + "-Warehouse" + fileExtensionApplication;
                    //            attachmentFiles2 = numberOfAttachment.ToString() + "-Warehouse" + fileExtensionApplication + ",";
                    //            numberOfAttachment++;

                    //            query1 += "INSERT INTO SRF_TRANSACTION_Warehouse (CTRLNo,AddedBy,AddedDate,TransType,Attachment) " +
                    //                      "VALUES ('" + lblCtrlNo2.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE(), '2', '" + (!string.IsNullOrEmpty(attachedFiles) ? attachedFiles : string.Empty).ToString() + "') ";

                    //        }



                    //    }


                    //}

                    int numberOfAttachment = BLL.SRF_TRANSACTION_Warehouse_Attachment_Count(lblCtrlNo2.Text.Trim());
                    numberOfAttachment++;

                    // ATTACHMENT 
                    if (fu.HasFile)
                    {
                        query1 += "INSERT INTO SRF_TRANSACTION_Warehouse (CTRLNo,AddedBy,AddedDate,TransType,Attachment) " +
                                 "VALUES ('" + lblCtrlNo2.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE(), '2', '" + numberOfAttachment.ToString() + "-Warehouse.pdf" + "') ";

                        string filename1 = Path.GetFileName(fu.FileName);
                        fu.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + lblCtrlNo2.Text.Trim(), filename1));
                        fu.Dispose();
                        File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename1), Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), numberOfAttachment.ToString() + "-Warehouse.pdf"), true);
                        File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename1));

                        attachedFiles += Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), numberOfAttachment.ToString() + "-Warehouse.pdf");

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
                                                                      verbiage, attachedFiles, string.Empty, string.Empty);

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
                        Session["successReturnPage"] = "SRF_Warehouse.aspx";
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


                    int numberOfAttachment = BLL.SRF_TRANSACTION_Warehouse_Attachment_Count(lblCTRLNoWithoutDocument.Text.Trim());
                    numberOfAttachment++;

                    for (int i = 0; i < gvWithoutDocuments.Rows.Count; i++)
                    {
                        Label lblRefId = (Label)gvWithoutDocuments.Rows[i].Cells[0].FindControl("lblRefId");
                        FileUpload fuWithoutDocuments = (FileUpload)gvWithoutDocuments.Rows[i].Cells[4].FindControl("fuWithoutDocuments");

                        // ATTACHMENT 
                        if (fuWithoutDocuments.HasFile)
                        {
                            query1 += "INSERT INTO SRF_TRANSACTION_Warehouse (CTRLNo,AddedBy,AddedDate,TransType,Attachment) " +
                                      "VALUES ('" + lblCTRLNoWithoutDocument.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE(), '2', '" + numberOfAttachment.ToString() + "-Warehouse.pdf" + "') ";

                            query1 += "UPDATE SRF_TRANSACTION_Warehouse_Actual_Delivery SET WithDocuments = '1' WHERE RefId ='" + lblRefId.Text.Trim() + "' ";

                            string filename1 = Path.GetFileName(fuWithoutDocuments.FileName);
                            fuWithoutDocuments.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + lblCTRLNoWithoutDocument.Text.Trim(), filename1));
                            fuWithoutDocuments.Dispose();
                            File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCTRLNoWithoutDocument.Text.Trim()), filename1), Path.Combine(Server.MapPath("~/SRF_Request/" + lblCTRLNoWithoutDocument.Text.Trim()), numberOfAttachment.ToString() + "-Warehouse.pdf"), true);
                            File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCTRLNoWithoutDocument.Text.Trim()), filename1));

                            attachedFiles += Path.Combine(Server.MapPath("~/SRF_Request/" + lblCTRLNoWithoutDocument.Text.Trim()), numberOfAttachment.ToString() + "-Warehouse.pdf") + ",";
                            numberOfAttachment++;

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
                                                                      verbiage, attachedFiles.Substring(0, attachedFiles.Length-1), string.Empty, string.Empty);

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
                        Session["successReturnPage"] = "SRF_Warehouse.aspx";
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



                    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    if (ddReportType.SelectedItem.Text.ToUpper() == "ALL ITEMS")
                    {

                        if (!System.IO.File.Exists(Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LOA" + ".xlsx")))
                        {
                            System.IO.File.Copy(Server.MapPath("~/SRF_XLS/LOA_Template.xlsx"), Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LOA" + ".xlsx"));
                        }
                        else
                        {
                            System.IO.File.Delete(Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LOA" + ".xlsx"));
                            System.IO.File.Copy(Server.MapPath("~/SRF_XLS/LOA_Template.xlsx"), Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LOA" + ".xlsx"));
                        }


                        string path = Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LOA" + ".xlsx");
                        Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + path);
                        FileStream fsBI = new FileStream(path, FileMode.Open);
                        using (SLDocument draft = new SLDocument(fsBI, "LOA"))
                        {
                            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

                            if (ddPezaNonPezaReport.SelectedItem.Text.ToUpper() == "PEZA")
                            {
                                list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "1").ToList();
                            }
                            else if (ddPezaNonPezaReport.SelectedItem.Text.ToUpper() == "NON PEZA")
                            {
                                list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "2").ToList();
                            }
                            else
                            {
                                list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim());
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
                                        draft.SetCellValue("C" + cnt.ToString(), elist.Warehouse_LOA8106);
                                        draft.SetCellValue("D" + cnt.ToString(), elist.Warehouse_8105);
                                        draft.SetCellValue("E" + cnt.ToString(), elist.Warehouse_ItemName);
                                        draft.SetCellValue("F" + cnt.ToString(), elist.Warehouse_TotalQuantity);
                                        draft.SetCellValue("G" + cnt.ToString(), elist.Warehouse_TotalActualQuantity);
                                        draft.SetCellValue("H" + cnt.ToString(), elist.Warehouse_RemainingQuantity);
                                        draft.SetCellValue("I" + cnt.ToString(), elist.Warehouse_DeliveredDate);
                                        draft.SetCellValue("J" + cnt.ToString(), elist.Warehouse_RequesterName);
                                        draft.SetCellValue("K" + cnt.ToString(), elist.Warehouse_SupplierName);
                                        draft.SetCellValue("L" + cnt.ToString(), elist.StatAll);

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

                        if (Session["LcRefId"].ToString() == ConfigurationManager.AppSettings["MAM_SYLVIA_ACCOUNT"].ToString())
                        {

                            if (!System.IO.File.Exists(Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LIQUIDATION" + ".xlsx")))
                            {
                                System.IO.File.Copy(Server.MapPath("~/SRF_XLS/SRF_Liquidation_Template.xlsx"), Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LIQUIDATION" + ".xlsx"));
                            }
                            else
                            {
                                System.IO.File.Delete(Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LIQUIDATION" + ".xlsx"));
                                System.IO.File.Copy(Server.MapPath("~/SRF_XLS/SRF_Liquidation_Template.xlsx"), Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LIQUIDATION" + ".xlsx"));
                            }


                            string path_LIQUIDATION = Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LIQUIDATION" + ".xlsx");
                            Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + path_LIQUIDATION);
                            FileStream fsBI_LIQUIDATION = new FileStream(path_LIQUIDATION, FileMode.Open);
                            using (SLDocument draft_LIQUIDATION = new SLDocument(fsBI_LIQUIDATION, "LOA"))
                            {

                                List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

                                if (ddPezaNonPezaReport.SelectedItem.Text.ToUpper() == "PEZA")
                                {
                                    list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "1" && string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                                }
                                else if (ddPezaNonPezaReport.SelectedItem.Text.ToUpper() == "NON PEZA")
                                {
                                    list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "2" && string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                                }
                                else
                                {
                                    list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                                }

                                if (list != null)
                                {
                                    if (list.Count > 0)
                                    {
                                        int cnt = 10;
                                        string previousItemName = string.Empty;

                                        foreach (Entities_SRF_RequestEntry elist in list)
                                        {
                                            if (cnt <= 29)
                                            {

                                                draft_LIQUIDATION.SetCellValue("E4", "RECIPIENT/CONSIGNEE: " + elist.Warehouse_SupplierName);
                                                draft_LIQUIDATION.SetCellValue("B6", elist.LOA_Number_From_HEAD);
                                                draft_LIQUIDATION.SetCellValue("G6", elist.LOA_MaturityDate_From_HEAD);
                                                draft_LIQUIDATION.SetCellValue("M6", elist.LOASuretyBond_From_HEAD);

                                                draft_LIQUIDATION.SetCellValue("A10", elist.Warehouse_SupplierName);

                                                if ((elist.Warehouse_ItemName.Trim() + elist.Warehouse_LOA8106.Trim()) != previousItemName)
                                                {
                                                    draft_LIQUIDATION.SetCellValue("B" + cnt.ToString(), elist.Warehouse_ItemName);
                                                    draft_LIQUIDATION.SetCellValue("C" + cnt.ToString(), elist.Warehouse_LOA8106);
                                                    draft_LIQUIDATION.SetCellValue("D" + cnt.ToString(), elist.PullOutServiceDate_From_HEAD);
                                                    draft_LIQUIDATION.SetCellValue("E" + cnt.ToString(), elist.Warehouse_TotalQuantity);
                                                    draft_LIQUIDATION.SetCellValueNumeric("F" + cnt.ToString(), (double.Parse(elist.Warehouse_TotalQuantity) * double.Parse(elist.LOAPriceValue_From_HEAD)).ToString());
                                                    draft_LIQUIDATION.SetCellValueNumeric("M" + cnt.ToString(), (double.Parse(elist.Warehouse_TotalQuantity) * double.Parse(elist.LOAPriceValue_From_HEAD)).ToString());

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

                                            }

                                            previousItemName = elist.Warehouse_ItemName.Trim() + elist.Warehouse_LOA8106.Trim();
                                            cnt++;
                                        }

                                    }
                                }


                                fsBI_LIQUIDATION.Close();
                                draft_LIQUIDATION.SaveAs(path_LIQUIDATION);

                            }


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
