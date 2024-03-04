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
    public partial class SRF_8105Entry : System.Web.UI.Page
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
                //list = BLL.SRF_TRANSACTION_8105_Entry(txtSearch.Text).Where(itm => itm.Category == Session["CategoryAccess"].ToString()).ToList().GroupBy(x => x.Warehouse_CtrlNo).Select(y => y.First()).ToList();

                //PRODUCTION
                if (BLL_COMMON.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), "310").Count > 0)
                {
                    if (!string.IsNullOrEmpty(ddStatus.SelectedItem.Text))
                    {
                        list = BLL.SRF_TRANSACTION_8105_Entry2(txtSearch.Text).Where(itm => itm.Warehouse_PezaNonPeza == "1" && itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper()).ToList();
                    }
                    else
                    {
                        list = BLL.SRF_TRANSACTION_8105_Entry2(txtSearch.Text).Where(itm => itm.Warehouse_PezaNonPeza == "1").ToList();
                    }
                }
                //IMPEX PERSONNEL
                //if (BLL_COMMON.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString()).Count > 0)
                if (ConfigurationManager.AppSettings["PIPL_Temp_MT_Access"].ToString().Contains(Session["Username"].ToString()))
                {
                    if (!string.IsNullOrEmpty(ddStatus.SelectedItem.Text))
                    {
                        list = BLL.SRF_TRANSACTION_8105_Entry2(txtSearch.Text).Where(itm => itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper() && itm.Warehouse_PezaNonPeza == "2").ToList();
                    }
                    else
                    {
                        list = BLL.SRF_TRANSACTION_8105_Entry2(txtSearch.Text).Where(itm => itm.Warehouse_PezaNonPeza == "2").ToList();
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

        protected void btnDownloadExcel_Click(object sender, EventArgs e)
        {
            try
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
                    list = BLL.SRF_TRANSACTION_8105_Entry(txtSearch.Text).Where(itm => itm.Category == Session["CategoryAccess"].ToString()).ToList();
                    if (list != null)
                    {
                        if (list.Count > 0)
                        {
                            int cnt = 2;
                            foreach (Entities_SRF_RequestEntry elist in list)
                            {
                                draft.SetCellValue("A" + cnt.ToString(), elist.Warehouse_CtrlNo);
                                draft.SetCellValue("B" + cnt.ToString(), elist.Warehouse_LOA8106);
                                draft.SetCellValue("C" + cnt.ToString(), elist.Warehouse_8105);
                                draft.SetCellValue("D" + cnt.ToString(), elist.Warehouse_ItemName);
                                draft.SetCellValue("E" + cnt.ToString(), elist.Warehouse_TotalQuantity);
                                draft.SetCellValue("F" + cnt.ToString(), elist.Warehouse_TotalActualQuantity);
                                draft.SetCellValue("G" + cnt.ToString(), elist.Warehouse_DeliveredDate);
                                draft.SetCellValue("H" + cnt.ToString(), elist.Warehouse_RequesterName);
                                draft.SetCellValue("I" + cnt.ToString(), elist.Warehouse_SupplierName);
                                

                                cnt++;
                            }
                        }
                    }


                    fsBI.Close();
                    draft.SaveAs(path);
                    
                }


                Response.Redirect("SRF_XLS/" + Session["LcRefId"].ToString() + "_LOA.xlsx", false);

                



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

                Label lblCTRLNo = row.FindControl("lblCTRLNo") as Label;
                Label lblStatus = row.FindControl("lblStatus") as Label;


                if (e.CommandName == "lbPrint_Command")
                {

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
                                    gvAttachmentFromWarehouse.DataSource = eAttachment;
                                    gvAttachmentFromWarehouse.DataBind();
                                    gvAttachmentFromWarehouse.Visible = true;
                                    //foreach (Entities_SRF_RequestEntry entityAtt in eAttachment)
                                    //{
                                    //    //ATTACHMENT 1
                                    //    if (entityAtt.Warehouse_Attachment.Contains("1"))
                                    //    {
                                    //        fu1.Visible = false;
                                    //        linkAttachment1.Visible = true;
                                    //        linkAttachment1.Text = entityAtt.Warehouse_Attachment;
                                    //    }
                                    //    //ATTACHMENT 2
                                    //    if (entityAtt.Warehouse_Attachment.Contains("2"))
                                    //    {
                                    //        fu2.Visible = false;
                                    //        linkAttachment2.Visible = true;
                                    //        linkAttachment2.Text = entityAtt.Warehouse_Attachment;
                                    //    }
                                    //    //ATTACHMENT 3
                                    //    if (entityAtt.Warehouse_Attachment.Contains("3"))
                                    //    {
                                    //        fu3.Visible = false;
                                    //        linkAttachment3.Visible = true;
                                    //        linkAttachment3.Text = entityAtt.Warehouse_Attachment;
                                    //    }
                                    //    //ATTACHMENT 4
                                    //    if (entityAtt.Warehouse_Attachment.Contains("4"))
                                    //    {
                                    //        fu4.Visible = false;
                                    //        linkAttachment4.Visible = true;
                                    //        linkAttachment4.Text = entityAtt.Warehouse_Attachment;
                                    //    }
                                    //    //ATTACHMENT 5
                                    //    if (entityAtt.Warehouse_Attachment.Contains("5"))
                                    //    {
                                    //        fu5.Visible = false;
                                    //        linkAttachment5.Visible = true;
                                    //        linkAttachment5.Text = entityAtt.Warehouse_Attachment;
                                    //    }
                                    //}
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

                    if (lblStatus.Text == "DELIVERY IN-PROGRESS")
                    {
                        btnInProgress.Visible = false;
                        btnConfirmDelivery.Visible = false;
                        divAttachment.Visible = true;
                    }
                    else
                    {
                        btnInProgress.Visible = true;
                        btnConfirmDelivery.Visible = false;
                        divAttachment.Visible = false;
                    }


                    if (lblStatus.Text == "DELIVERED WITH PENDING ITEMS")
                    {
                        btnInProgress.Visible = false;
                        btnConfirmDelivery.Visible = false;
                        divAttachment.Visible = true;
                        //div8105.Visible = true;


                    }

                    if (lblStatus.Text == "DELIVERED")
                    {
                        btnInProgress.Visible = false;
                        btnConfirmDelivery.Visible = false;
                        divAttachment.Visible = true;
                        //div8105.Visible = true;
                    }

                    lbPrint.OnClientClick = "showDialog(); return false;";


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

        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label lblStatus = (Label)e.Row.FindControl("lblStatus");


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

                    if (lblStatus.Text == "DELIVERY IN-PROGRESS")
                    {
                        lblStatus.Style.Add("height", "30px");
                        lblStatus.Style.Add("background-color", "#f44336");
                    }

                    if (lblStatus.Text == "DELIVERED")
                    {
                        lblStatus.Style.Add("height", "30px");
                        lblStatus.Style.Add("background-color", "#00C851");
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


                query1 = "INSERT INTO SRF_TRANSACTION_Warehouse (CTRLNo,AddedBy,AddedDate,TransType,Attachment) " +
                         "VALUES ('" + lblCtrlNo2.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE(), '1', '')";

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

                    string emailTo = BLL_COMMON.GetBuyerEmailAddressByHandledCategory(buyerCategory.Value);

                    string emailService = COMMON.sendEmailToSuppliersForSRFWarehouse(emailTo, ConfigurationManager.AppSettings["email-username"], "SRF DELIVERY NOTIFICATION",
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
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("SRF_8105Entry.aspx");

                //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Close8105(); return false;", true);

                //btnClose.OnClientClick = "showDialog2(); return false;";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


        protected void btnConfirmDelivery_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
            //    string query1 = string.Empty;
            //    string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
            //    string query_Success = string.Empty;
            //    string emailSuccess = string.Empty;


            //    string error = string.Empty;
            //    string isDiscrepancy = string.Empty;
            //    string verbiage = string.Empty;
            //    string attachedFiles = string.Empty;



            //    if (!fu1.HasFile && !fu2.HasFile && !fu3.HasFile && !fu4.HasFile && !fu5.HasFile)
            //    {
            //        error += "ATTACHMENT REQUIRED! Please attached required attachment file (8106, DR WITH 8106 and other supporting documents)";
            //    }

            //    // DELETE THE OLD RECORDS
            //    query1 += "DELETE FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WHERE SRFNumber = '" + lblCtrlNo2.Text.Trim() + "' ";

            //    for (int i = 0; i < gvDetails.Rows.Count; i++)
            //    {
            //        Label lblRefId = (Label)gvDetails.Rows[i].Cells[0].FindControl("lblRefId");
            //        Label txtQuantity = (Label)gvDetails.Rows[i].Cells[5].FindControl("txtQuantity");
            //        TextBox txtActualQty = (TextBox)gvDetails.Rows[i].Cells[8].FindControl("txtActualQty");

            //        if (txtQuantity.Text == txtActualQty.Text)
            //        {
            //            //DO NOTHING
            //        }
            //        else
            //        {
            //            isDiscrepancy += "[" + txtQuantity.Text + "-" + txtActualQty.Text + "]";
            //        }


            //        query1 += "INSERT INTO SRF_TRANSACTION_Warehouse_Actual_Delivery (DetailsRefId,SRFNumber,Quantity,ActualQuantity,AddedBy,AddedDate) " +
            //                  "VALUES ('" + lblRefId.Text.Trim() + "','" + lblCtrlNo2.Text.Trim() + "','" + txtQuantity.Text.Trim() + "','" + txtActualQty.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE()) ";

            //    }

            //    if (!string.IsNullOrEmpty(error))
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + error + "');", true);
            //    }
            //    else
            //    {
            //        // ATTACHMENT 1
            //        if (fu1.HasFile)
            //        {
            //            query1 += "INSERT INTO SRF_TRANSACTION_Warehouse (CTRLNo,AddedBy,AddedDate,TransType,Attachment) " +
            //                     "VALUES ('" + lblCtrlNo2.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE(), '2', '1-Warehouse.pdf') ";

            //            string filename1 = Path.GetFileName(fu1.FileName);
            //            fu1.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + lblCtrlNo2.Text.Trim(), filename1));
            //            fu1.Dispose();
            //            File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename1), Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "1-Warehouse.pdf"), true);
            //            File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename1));

            //            attachedFiles += Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "1-Warehouse.pdf") + ",";

            //        }

            //        // ATTACHMENT 2
            //        if (fu2.HasFile)
            //        {
            //            query1 += "INSERT INTO SRF_TRANSACTION_Warehouse (CTRLNo,AddedBy,AddedDate,TransType,Attachment) " +
            //                     "VALUES ('" + lblCtrlNo2.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE(), '2', '2-Warehouse.pdf') ";

            //            string filename2 = Path.GetFileName(fu2.FileName);
            //            fu2.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + lblCtrlNo2.Text.Trim(), filename2));
            //            fu2.Dispose();
            //            File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename2), Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "2-Warehouse.pdf"), true);
            //            File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename2));

            //            attachedFiles += Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "2-Warehouse.pdf") + ",";

            //        }

            //        // ATTACHMENT 3
            //        if (fu3.HasFile)
            //        {
            //            query1 += "INSERT INTO SRF_TRANSACTION_Warehouse (CTRLNo,AddedBy,AddedDate,TransType,Attachment) " +
            //                     "VALUES ('" + lblCtrlNo2.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE(), '2', '3-Warehouse.pdf') ";

            //            string filename3 = Path.GetFileName(fu3.FileName);
            //            fu3.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + lblCtrlNo2.Text.Trim(), filename3));
            //            fu3.Dispose();
            //            File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename3), Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "3-Warehouse.pdf"), true);
            //            File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename3));

            //            attachedFiles += Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "3-Warehouse.pdf") + ",";

            //        }

            //        // ATTACHMENT 4
            //        if (fu4.HasFile)
            //        {
            //            query1 += "INSERT INTO SRF_TRANSACTION_Warehouse (CTRLNo,AddedBy,AddedDate,TransType,Attachment) " +
            //                     "VALUES ('" + lblCtrlNo2.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE(), '2', '4-Warehouse.pdf') ";

            //            string filename4 = Path.GetFileName(fu4.FileName);
            //            fu4.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + lblCtrlNo2.Text.Trim(), filename4));
            //            fu4.Dispose();
            //            File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename4), Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "4-Warehouse.pdf"), true);
            //            File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename4));

            //            attachedFiles += Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "4-Warehouse.pdf") + ",";

            //        }

            //        // ATTACHMENT 5
            //        if (fu5.HasFile)
            //        {
            //            query1 += "INSERT INTO SRF_TRANSACTION_Warehouse (CTRLNo,AddedBy,AddedDate,TransType,Attachment) " +
            //                     "VALUES ('" + lblCtrlNo2.Text.Trim() + "','" + Session["LcRefId"].ToString() + "', GETDATE(), '2', '5-Warehouse.pdf') ";

            //            string filename5 = Path.GetFileName(fu5.FileName);
            //            fu5.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + lblCtrlNo2.Text.Trim(), filename5));
            //            fu5.Dispose();
            //            File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename5), Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "5-Warehouse.pdf"), true);
            //            File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename5));

            //            attachedFiles += Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "5-Warehouse.pdf") + ",";

            //        }



            //        query_Success = BLL.SRF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

            //        if (int.Parse(query_Success) > 0)
            //        {

            //            if (!string.IsNullOrEmpty(isDiscrepancy))
            //            {
            //                //WITH DISCREPANCY
            //                if (!string.IsNullOrEmpty(txtLoa8106.Text))
            //                {
            //                    verbiage = "Please know that delivery for <b>" + lblCtrlNo2.Text + "</b> with LOA8106 Number <b>" + txtLoa8106.Text.ToUpper().Trim() + "</b> has been delivered but with pending items. <br/>" +
            //                               "Expect a hard copy of documents from Warehouse and do coordinate with them if needed. <br/>" +
            //                               "Check the attached documents. <br/><br/>" +
            //                               "Thank you and have a great day! <br/><br/>" +
            //                               "ROHM Electronics Philippines Inc.";
            //                }
            //                else
            //                {
            //                    verbiage = "Please know that delivery for <b>" + lblCtrlNo2.Text + "</b> has been delivered but with pending items. <br/>" +
            //                               "Expect a hard copy of documents from Warehouse and do coordinate with them if needed. <br/>" +
            //                               "Check the attached documents. <br/><br/>" +
            //                               "Thank you and have a great day! <br/><br/>" +
            //                               "ROHM Electronics Philippines Inc.";
            //                }

            //            }
            //            else
            //            {
            //                //WITHOUT DISCREPANCY
            //                if (!string.IsNullOrEmpty(txtLoa8106.Text))
            //                {
            //                    verbiage = "Please know that delivery for <b>" + lblCtrlNo2.Text + "</b> with LOA8106 Number <b>" + txtLoa8106.Text.ToUpper().Trim() + "</b> has been successfully delivered. <br/>" +
            //                               "Expect a hard copy of documents from Warehouse and do coordinate with them if needed. <br/>" +
            //                               "Check the attached documents. <br/><br/>" +
            //                               "Thank you and have a great day! <br/><br/>" +
            //                               "ROHM Electronics Philippines Inc.";
            //                }
            //                else
            //                {
            //                    verbiage = "Please know that delivery for <b>" + lblCtrlNo2.Text + "</b> has been successfully delivered. <br/>" +
            //                               "Expect a hard copy of documents from Warehouse and do coordinate with them if needed. <br/>" +
            //                               "Check the attached documents. <br/><br/>" +
            //                               "Thank you and have a great day! <br/><br/>" +
            //                               "ROHM Electronics Philippines Inc.";
            //                }

            //            }


            //            string emailService = COMMON.sendEmailToSuppliers(ConfigurationManager.AppSettings["SRF_IMPEX_EMAIL"], ConfigurationManager.AppSettings["email-username"], "SRF DELIVERED CONFIRMATION",
            //                                                          verbiage, attachedFiles.Substring(0, attachedFiles.Length - 1).ToString(), string.Empty, string.Empty);

            //            if (emailService.ToLower().Contains("success"))
            //            {
            //                emailSuccess = "SRF NUMBER : <b>" + lblCtrlNo2.Text.ToUpper() + "</b> HAS BEEN SUCCESSFULLY PROCESSED. IMPEX HAS BEEN NOTIFIED.";
            //            }
            //            else
            //            {
            //                emailSuccess = "SRF NUMBER : <b>" + lblCtrlNo2.Text.ToUpper() + "</b> HAS BEEN SUCCESSFULLY PROCESSED. FAILED TO NOTIFY IMPEX DUE TO CONNECTION ISSUE.";
            //            }


            //            // REDIRECT TO SUCCESS PAGE
            //            Session["successMessage"] = emailSuccess;
            //            Session["successTransactionName"] = "SRF_WAREHOUSE";
            //            Session["successReturnPage"] = "SRF_Warehouse.aspx";
            //            Response.Redirect("SuccessPage.aspx");


            //        }



            //    }

            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            //}
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            btnSubmitTemporary_Click(sender, e);
        }

        protected void btnSubmitTemporary_Click(object sender, EventArgs e)
        {
            try
            {
                string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                string query1 = string.Empty;
                string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                string query_Success = string.Empty;
                int hasEntry = 0;
                


                // JUST INCASE DIRECTORY FOR CTRLNO IS NOT YET CREATED
                if (!System.IO.Directory.Exists(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim())))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()));
                }


                // DELETE RECORD FOR INITIAL ITERATION
                //query1 += "DELETE FROM SRF_TRANSACTION_8105 WHERE CTRLNo = '" + lblCtrlNo2.Text.Trim() + "' ";

                int numberOfAttachment = BLL.SRF_TRANSACTION_Warehouse_ActualDelivery_Attachment_Count(lblCtrlNo2.Text.Trim());
                numberOfAttachment++;

                for (int i = 0; i < gvActualDelivery.Rows.Count; i++)
                {
                    Label lblRefid = (Label)gvActualDelivery.Rows[i].Cells[0].FindControl("lblRefid");
                    Label lblActualQuantity = (Label)gvActualDelivery.Rows[i].Cells[2].FindControl("lblActualQuantity");
                    TextBox txtLOA8105Number = (TextBox)gvActualDelivery.Rows[i].Cells[4].FindControl("txtLOA8105Number");
                    TextBox txt8105ProcessDate = (TextBox)gvActualDelivery.Rows[i].Cells[5].FindControl("txt8105ProcessDate");
                    FileUpload fuAttachment = (FileUpload)gvActualDelivery.Rows[i].Cells[6].FindControl("fuAttachment");

                    string fileNameApplication = System.IO.Path.GetFileName(fuAttachment.FileName);
                    string fileExtensionApplication = System.IO.Path.GetExtension(fileNameApplication);
                    string newFile = fileNameApplication;
                    string attachmentFiles = string.Empty;

                    //long totalQty_8105 = totalQty_8105 + long.Parse(lblActualQuantity.Text);

                    if (fuAttachment.Visible)
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
                                System.IO.File.Copy(System.IO.Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), newFile), System.IO.Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), (numberOfAttachment.ToString() + "-8105" + fileExtensionApplication)), true);
                                System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), newFile));
                            }

                            attachmentFiles = numberOfAttachment.ToString() + "-8105" + fileExtensionApplication;
                            numberOfAttachment++;
                        }

                        if (!string.IsNullOrEmpty(txtLOA8105Number.Text) && fuAttachment.HasFile)
                        {
                            query1 += "UPDATE SRF_TRANSACTION_Warehouse_Actual_Delivery SET LOA8105 = '" + txtLOA8105Number.Text + "', Attachment = '" + attachmentFiles + "', LOA8105_AddedBy = '" + Session["LcRefId"].ToString() + "', LOA8105_AddedDate = GETDATE(), LOA8105ProcessDate ='" + txt8105ProcessDate.Text + "' WHERE RefId = '" + lblRefid.Text.Trim() + "' ";                            
                        }
                        

                    }

                    //if (!string.IsNullOrEmpty(txtLOA8105Number.Text))
                    //{
                    //    query1 += "UPDATE SRF_TRANSACTION_Warehouse_Actual_Delivery SET LOA8105 = '" + txtLOA8105Number.Text + "', Attachment = '" + attachmentFiles + "', LOA8105_AddedBy = '" + Session["LcRefId"].ToString() + "', LOA8105_AddedDate = GETDATE() WHERE RefId = '" + lblRefid.Text.Trim() + "' ";
                    //}


                }

 

                ////NO. 1
                //if (!string.IsNullOrEmpty(txt8105_1.Text) && !string.IsNullOrEmpty(txtQty8105_1.Text))
                //{
                //    query1 += "INSERT INTO SRF_TRANSACTION_8105 (No,CTRLNo,LOA8105,Quantity,Attachment,AddedBy,AddedDate) " +
                //              "VALUES ('1','" + lblCtrlNo2.Text.Trim() + "','" + txt8105_1.Text.Replace("'", "''") + "','" + txtQty8105_1.Text + "','1-8105.pdf','" + Session["LcRefId"].ToString() + "', GETDATE()) ";

                //    // ATTACHMENT 1
                //    if (fu8105_1.HasFile)
                //    {
                //        string filename1 = Path.GetFileName(fu8105_1.FileName);
                //        fu8105_1.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + lblCtrlNo2.Text.Trim(), filename1));
                //        fu8105_1.Dispose();
                //        File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename1), Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "1-8105.pdf"), true);
                //        File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename1));

                //    }

                //    hasEntry++;
                //}

                ////NO. 2
                //if (!string.IsNullOrEmpty(txt8105_2.Text) && !string.IsNullOrEmpty(txtQty8105_2.Text))
                //{
                //    query1 += "INSERT INTO SRF_TRANSACTION_8105 (No,CTRLNo,LOA8105,Quantity,Attachment,AddedBy,AddedDate) " +
                //              "VALUES ('2','" + lblCtrlNo2.Text.Trim() + "','" + txt8105_2.Text.Replace("'", "''") + "','" + txtQty8105_2.Text + "','2-8105.pdf','" + Session["LcRefId"].ToString() + "', GETDATE()) ";

                //    // ATTACHMENT 2
                //    if (fu8105_2.HasFile)
                //    {
                //        string filename2 = Path.GetFileName(fu8105_2.FileName);
                //        fu8105_2.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + lblCtrlNo2.Text.Trim(), filename2));
                //        fu8105_2.Dispose();
                //        File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename2), Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "2-8105.pdf"), true);
                //        File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename2));

                //    }

                //    hasEntry++;
                //}

                ////NO. 3
                //if (!string.IsNullOrEmpty(txt8105_3.Text) && !string.IsNullOrEmpty(txtQty8105_3.Text))
                //{
                //    query1 += "INSERT INTO SRF_TRANSACTION_8105 (No,CTRLNo,LOA8105,Quantity,Attachment,AddedBy,AddedDate) " +
                //              "VALUES ('3','" + lblCtrlNo2.Text.Trim() + "','" + txt8105_3.Text.Replace("'", "''") + "','" + txtQty8105_3.Text + "','3-8105.pdf','" + Session["LcRefId"].ToString() + "', GETDATE()) ";

                //    // ATTACHMENT 3
                //    if (fu8105_3.HasFile)
                //    {
                //        string filename3 = Path.GetFileName(fu8105_3.FileName);
                //        fu8105_3.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + lblCtrlNo2.Text.Trim(), filename3));
                //        fu8105_3.Dispose();
                //        File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename3), Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "3-8105.pdf"), true);
                //        File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename3));

                //    }

                //    hasEntry++;
                //}

                ////NO. 4
                //if (!string.IsNullOrEmpty(txt8105_4.Text) && !string.IsNullOrEmpty(txtQty8105_4.Text))
                //{
                //    query1 += "INSERT INTO SRF_TRANSACTION_8105 (No,CTRLNo,LOA8105,Quantity,Attachment,AddedBy,AddedDate) " +
                //              "VALUES ('4','" + lblCtrlNo2.Text.Trim() + "','" + txt8105_4.Text.Replace("'", "''") + "','" + txtQty8105_4.Text + "','4-8105.pdf','" + Session["LcRefId"].ToString() + "', GETDATE()) ";


                //    // ATTACHMENT 4
                //    if (fu8105_4.HasFile)
                //    {
                //        string filename4 = Path.GetFileName(fu8105_4.FileName);
                //        fu8105_4.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + lblCtrlNo2.Text.Trim(), filename4));
                //        fu8105_4.Dispose();
                //        File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename4), Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "4-8105.pdf"), true);
                //        File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename4));

                //    }

                //    hasEntry++;
                //}

                ////NO. 5
                //if (!string.IsNullOrEmpty(txt8105_5.Text) && !string.IsNullOrEmpty(txtQty8105_5.Text))
                //{
                //    query1 += "INSERT INTO SRF_TRANSACTION_8105 (No,CTRLNo,LOA8105,Quantity,Attachment,AddedBy,AddedDate) " +
                //              "VALUES ('5','" + lblCtrlNo2.Text.Trim() + "','" + txt8105_5.Text.Replace("'", "''") + "','" + txtQty8105_5.Text + "','5-8105.pdf','" + Session["LcRefId"].ToString() + "', GETDATE()) ";


                //    // ATTACHMENT 5
                //    if (fu8105_5.HasFile)
                //    {
                //        string filename5 = Path.GetFileName(fu8105_5.FileName);
                //        fu8105_5.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + lblCtrlNo2.Text.Trim(), filename5));
                //        fu8105_5.Dispose();
                //        File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename5), Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "5-8105.pdf"), true);
                //        File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename5));

                //    }

                //    hasEntry++;
                //}

                ////NO. 6
                //if (!string.IsNullOrEmpty(txt8105_6.Text) && !string.IsNullOrEmpty(txtQty8105_6.Text))
                //{
                //    query1 += "INSERT INTO SRF_TRANSACTION_8105 (No,CTRLNo,LOA8105,Quantity,Attachment,AddedBy,AddedDate) " +
                //              "VALUES ('6','" + lblCtrlNo2.Text.Trim() + "','" + txt8105_6.Text.Replace("'", "''") + "','" + txtQty8105_6.Text + "','6-8105.pdf','" + Session["LcRefId"].ToString() + "', GETDATE()) ";


                //    // ATTACHMENT 6
                //    if (fu8105_6.HasFile)
                //    {
                //        string filename6 = Path.GetFileName(fu8105_6.FileName);
                //        fu8105_6.SaveAs(Path.Combine(Server.MapPath("~/SRF_Request/") + lblCtrlNo2.Text.Trim(), filename6));
                //        fu8105_6.Dispose();
                //        File.Copy(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename6), Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), "6-8105.pdf"), true);
                //        File.Delete(Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), filename6));

                //    }

                //    hasEntry++;
                //}



                query_Success = BLL.SRF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                if (int.Parse(query_Success) > 0)
                {
                    // REDIRECT TO SUCCESS PAGE
                    Session["successMessage"] = "SRF 8105 has been successfully added to <b>" + lblCtrlNo2.Text.ToUpper() + "</b>";
                    Session["successTransactionName"] = "SRF_8105Entry";
                    Session["successReturnPage"] = "SRF_8105Entry.aspx";
                    Response.Redirect("SuccessPage.aspx");
                }





            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void lbAttachment_Click(object sender, EventArgs e)
        {
            //Response.Redirect("http://10.27.1.170:9292/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + lb8105_1.Text.Trim(), true);
        }
        //protected void lb8105_2_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("http://10.27.1.170:9292/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + lb8105_2.Text.Trim(), true);
        //}
        //protected void lb8105_3_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("http://10.27.1.170:9292/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + lb8105_3.Text.Trim(), true);
        //}
        //protected void lb8105_4_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("http://10.27.1.170:9292/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + lb8105_4.Text.Trim(), true);
        //}
        //protected void lb8105_5_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("http://10.27.1.170:9292/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + lb8105_5.Text.Trim(), true);
        //}
        //protected void lb8105_6_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("http://10.27.1.170:9292/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + lb8105_6.Text.Trim(), true);
        //}



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
