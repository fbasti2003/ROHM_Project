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
    public partial class SRF_8105Entry2 : System.Web.UI.Page
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
                    txtFrom2.Text = DateTime.Today.AddMonths(-12).ToString("MM/dd/yyyy");
                    txtTo2.Text = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");

                    txtFrom.Text = DateTime.Today.AddMonths(-12).ToString("MM/dd/yyyy");
                    txtTo.Text = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");

                    if (Session["DeleteRecord"] != null)
                    {
                        if (!string.IsNullOrEmpty(Session["DeleteRecord"].ToString()))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('RECORD DELETED. Please open " + Session["DeleteRecord"].ToString() + " again and continue your transactions.');", true);
                            Session["DeleteRecord"] = null;
                        }
                    }

                    chkIncludeDelivered.Checked = true;

                    //Populate category
                    List<Entities_SRF_RequestEntry> listCategory = new List<Entities_SRF_RequestEntry>();
                    listCategory = BLL.SRF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(item => item.DropdownName).ToList();

                    if (listCategory != null)
                    {
                        if (listCategory.Count > 0)
                        {
                            ddCategory.Items.Add("");

                            foreach (Entities_SRF_RequestEntry entity in listCategory)
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

                            if (ConfigurationManager.AppSettings["PIPL_Temp_MT_Access"].ToString().Contains(Session["Username"].ToString()))
                            {
                                //DO NOTHING
                            }
                            else
                            {
                                ddCategory.SelectedValue = Session["CategoryAccess"].ToString();
                            }


                        }
                    }



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

                //PRODUCTION (WAREHOUSE USERS)
                if (BLL_COMMON.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), "310").Count > 0)
                {
                    // FOR CHYNG AND LORNA ONLY (DEDICATED ACCOUNTS
                    if (Session["Username"].ToString() == "15-048" || Session["Username"].ToString() == "22-079")
                    {

                        if (!string.IsNullOrEmpty(ddStatus.SelectedItem.Text))
                        {
                            if (chkIncludeDelivered.Checked)
                            {
                                if (ddCategory.SelectedValue == string.Empty)
                                {
                                    list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "yes").Where(itm => itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper()).ToList();
                                }
                                else
                                {
                                    list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "yes").Where(itm => itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper() && itm.Category == ddCategory.SelectedValue).ToList();
                                }
                            }
                            else
                            {
                                if (ddCategory.SelectedValue == string.Empty)
                                {
                                    list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "no").Where(itm => itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper()).ToList();
                                }
                                else
                                {
                                    list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "no").Where(itm => itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper() && itm.Category == ddCategory.SelectedValue).ToList();
                                }
                            }
                        }
                        else
                        {
                            if (chkIncludeDelivered.Checked)
                            {
                                if (ddCategory.SelectedValue == string.Empty)
                                {
                                    list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "yes");
                                }
                                else
                                {
                                    list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "yes").Where(itm => itm.Category == ddCategory.SelectedValue).ToList();
                                }
                            }
                            else
                            {
                                if (ddCategory.SelectedValue == string.Empty)
                                {
                                    list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "no");
                                }
                                else
                                {
                                    list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "no").Where(itm => itm.Category == ddCategory.SelectedValue).ToList();
                                }
                            }
                        }



                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(ddStatus.SelectedItem.Text))
                        {
                            if (chkIncludeDelivered.Checked)
                            {
                                if (ddCategory.SelectedValue == string.Empty)
                                {
                                    list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "yes").Where(itm => itm.Warehouse_PezaNonPeza == "1" && itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper()).ToList();
                                }
                                else
                                {
                                    list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "yes").Where(itm => itm.Warehouse_PezaNonPeza == "1" && itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper() && itm.Category == ddCategory.SelectedValue).ToList();
                                }
                            }
                            else
                            {
                                if (ddCategory.SelectedValue == string.Empty)
                                {
                                    list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "no").Where(itm => itm.Warehouse_PezaNonPeza == "1" && itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper()).ToList();
                                }
                                else
                                {
                                    list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "no").Where(itm => itm.Warehouse_PezaNonPeza == "1" && itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper() && itm.Category == ddCategory.SelectedValue).ToList();
                                }
                            }
                        }
                        else
                        {
                            if (chkIncludeDelivered.Checked)
                            {
                                if (ddCategory.SelectedValue == string.Empty)
                                {
                                    list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "yes").Where(itm => itm.Warehouse_PezaNonPeza == "1").ToList();
                                }
                                else
                                {
                                    list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "yes").Where(itm => itm.Warehouse_PezaNonPeza == "1" && itm.Category == ddCategory.SelectedValue).ToList();
                                }
                            }
                            else
                            {
                                if (ddCategory.SelectedValue == string.Empty)
                                {
                                    list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "no").Where(itm => itm.Warehouse_PezaNonPeza == "1").ToList();
                                }
                                else
                                {
                                    list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "no").Where(itm => itm.Warehouse_PezaNonPeza == "1" && itm.Category == ddCategory.SelectedValue).ToList();
                                }
                            }
                        }


                    }

                    ddReportType.SelectedValue = "ALL ITEMS";
                    ddReportType.Enabled = false;

                    ddPezaNonPezaReport.SelectedValue = "PEZA";
                    ddPezaNonPezaReport.Enabled = false;
                }
                //IMPEX PERSONNEL
                //if (BLL_COMMON.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString()).Count > 0)
                if (ConfigurationManager.AppSettings["PIPL_Temp_MT_Access"].ToString().Contains(Session["Username"].ToString()) || BLL_COMMON.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString()).Count > 0)
                {
                    if (!string.IsNullOrEmpty(ddStatus.SelectedItem.Text))
                    {
                        if (chkIncludeDelivered.Checked)
                        {
                            if (ddCategory.SelectedValue == string.Empty)
                            {
                                list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "yes").Where(itm => itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper()).ToList().OrderByDescending(itm => itm.StatAll).ToList();
                            }
                            else
                            {
                                list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "yes").Where(itm => itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper() && itm.Category == ddCategory.SelectedValue).ToList().OrderByDescending(itm => itm.StatAll).ToList();
                            }
                        }
                        else
                        {
                            if (ddCategory.SelectedValue == string.Empty)
                            {
                                //list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper() && itm.Warehouse_PezaNonPeza == "2").ToList();
                                list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "no").Where(itm => itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper()).ToList().OrderByDescending(itm => itm.StatAll).ToList();
                            }
                            else
                            {
                                list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "no").Where(itm => itm.StatAll.ToUpper() == ddStatus.SelectedItem.Text.ToUpper() && itm.Category == ddCategory.SelectedValue).ToList().OrderByDescending(itm => itm.StatAll).ToList();
                            }
                        }
                    }
                    else
                    {
                        if (chkIncludeDelivered.Checked)
                        {
                            if (ddCategory.SelectedValue == string.Empty)
                            {
                                list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "yes").OrderByDescending(itm => itm.StatAll).ToList();
                            }
                            else
                            {
                                list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "yes").Where(itm => itm.Category == ddCategory.SelectedValue).ToList().OrderByDescending(itm => itm.StatAll).ToList();
                            }
                        }
                        else
                        {
                            if (ddCategory.SelectedValue == string.Empty)
                            {
                                //list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "2").ToList();
                                list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "no").OrderByDescending(itm => itm.StatAll).ToList();
                            }
                            else
                            {
                                list = BLL.SRF_TRANSACTION_8105_Entry2WithDateRange(txtSearch.Text, txtFrom2.Text.Trim(), txtTo2.Text.Trim(), "no").Where(itm => itm.Category == ddCategory.SelectedValue).ToList().OrderByDescending(itm => itm.StatAll).ToList();
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

        protected void gvActualDelivery_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                LinkButton lbAttachmentFromWarehouse = row.FindControl("lbAttachmentFromWarehouse") as LinkButton;
                LinkButton lbAttachment = row.FindControl("lbAttachment") as LinkButton;
                Label lblRefid = row.FindControl("lblRefid") as Label;


                if (e.CommandName == "lbAttachmentFromWarehouse_Command")
                {

                    string URL = "http://10.27.1.170:9292/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + lbAttachmentFromWarehouse.Text.Trim();

                    URL = Page.ResolveClientUrl(URL);
                    lbAttachmentFromWarehouse.OnClientClick = "showDialog(); window.open('" + URL + "'); return false;";
                    
                }

                if (e.CommandName == "lbAttachment_Command")
                {

                    string URL2 = "http://10.27.1.170:9292/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + lbAttachment.Text.Trim();

                    URL2 = Page.ResolveClientUrl(URL2);
                    lbAttachment.OnClientClick = "showDialog(); window.open('" + URL2 + "'); return false;";

                }

                if (e.CommandName == "btnRemove_Command")
                {
                    string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                    string query1 = string.Empty;
                    string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                    string query_Success = string.Empty;


                    query1 = "DELETE FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WHERE RefId = '" + lblRefid.Text.Trim() + "'";
                    query_Success = BLL.SRF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                    Session["DeleteRecord"] = lblCtrlNo2.Text.Trim();
                    Response.Redirect("SRF_8105Entry2.aspx");
                    

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

                    if (lblStatus.Text == "CLOSED")
                    {
                        divAttachment.Visible = true;
                        btnClose.Visible = true;
                        btnInProgress.Visible = false;
                        btnConfirmDelivery.Visible = false;
                        btnSubmit.Visible = false;
                        btnSubmit2.Visible = false;
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
                    Button btnRemove = (Button)e.Row.FindControl("btnRemove");


                    if (!string.IsNullOrEmpty(lblAttachment.Text))
                    {
                        lbAttachment.Visible = true;
                        fuAttachment.Visible = false;
                        btnRemove.Visible = false;
                    }
                    else
                    {
                        lbAttachment.Visible = false;
                        fuAttachment.Visible = true;
                        btnRemove.Visible = true;
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

                    if (lblStatus.Text == "CLOSED")
                    {
                        lblStatus.Style.Add("background-color", "red");
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
                Response.Redirect("SRF_8105Entry2.aspx");

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
                string attachmentFilesForEmail = string.Empty;
                string str8105NumberForEmail = string.Empty;
                bool isFuHasFile = false;
                string hasError = string.Empty;


                // JUST INCASE DIRECTORY FOR CTRLNO IS NOT YET CREATED
                if (!System.IO.Directory.Exists(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim())))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()));
                }


                // DELETE RECORD FOR INITIAL ITERATION
                //query1 += "DELETE FROM SRF_TRANSACTION_8105 WHERE CTRLNo = '" + lblCtrlNo2.Text.Trim() + "' ";

                int numberOfAttachment = BLL.SRF_TRANSACTION_Warehouse_ActualDelivery_Attachment_Count(lblCtrlNo2.Text.Trim());
                numberOfAttachment++;

                string oneTime_FileNameApplication = string.Empty;
                string oneTime_FileExtensionApplication = string.Empty;
                string oneTime_NewFile = string.Empty;

                // CHECK ONE TIME ATTACHMENT
                if (fu.HasFile)
                {
                    oneTime_FileNameApplication = System.IO.Path.GetFileName(fu.FileName);
                    oneTime_FileExtensionApplication = System.IO.Path.GetExtension(oneTime_FileNameApplication);
                    oneTime_NewFile = oneTime_FileNameApplication;

                    if (!System.IO.Directory.Exists(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim())))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()));
                    }
                    if (!System.IO.File.Exists(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + oneTime_NewFile)))
                    {
                        fu.SaveAs(System.IO.Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), oneTime_NewFile));
                        fu.Dispose();
                        System.IO.File.Copy(System.IO.Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), oneTime_NewFile), System.IO.Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), (numberOfAttachment.ToString() + "-8105" + oneTime_FileExtensionApplication)), true);
                        System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim()), oneTime_NewFile));
                    }

                }

                
                for (int i = 0; i < gvActualDelivery.Rows.Count; i++)
                {
                    Label lblRefid = (Label)gvActualDelivery.Rows[i].Cells[0].FindControl("lblRefid");
                    Label lblActualQuantity = (Label)gvActualDelivery.Rows[i].Cells[2].FindControl("lblActualQuantity");
                    TextBox txtLOA8105Number = (TextBox)gvActualDelivery.Rows[i].Cells[4].FindControl("txtLOA8105Number");
                    TextBox txt8105ProcessDate = (TextBox)gvActualDelivery.Rows[i].Cells[5].FindControl("txt8105ProcessDate");
                    FileUpload fuAttachment = (FileUpload)gvActualDelivery.Rows[i].Cells[6].FindControl("fuAttachment");
                    LinkButton lbAttachment = (LinkButton)gvActualDelivery.Rows[i].Cells[6].FindControl("lbAttachment");

                    string fileNameApplication = System.IO.Path.GetFileName(fuAttachment.FileName);
                    string fileExtensionApplication = System.IO.Path.GetExtension(fileNameApplication);
                    string newFile = fileNameApplication;
                    string attachmentFiles = string.Empty;

                    
                    //long totalQty_8105 = totalQty_8105 + long.Parse(lblActualQuantity.Text);

                    if (fu.HasFile)
                    {
                        if (!string.IsNullOrEmpty(txtLOA8105Number.Text) && !string.IsNullOrEmpty(txt8105ProcessDate.Text) && fu.HasFile && !lbAttachment.Visible)
                        {
                            query1 += "UPDATE SRF_TRANSACTION_Warehouse_Actual_Delivery SET LOA8105 = '" + txtLOA8105Number.Text + "', Attachment = '" + (numberOfAttachment.ToString() + "-8105" + oneTime_FileExtensionApplication) + "', LOA8105_AddedBy = '" + Session["LcRefId"].ToString() + "', LOA8105_AddedDate = GETDATE(), LOA8105ProcessDate ='" + txt8105ProcessDate.Text + "' WHERE RefId = '" + lblRefid.Text.Trim() + "' ";
                            attachmentFilesForEmail += Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + (numberOfAttachment.ToString() + "-8105" + oneTime_FileExtensionApplication)) + ",";
                            str8105NumberForEmail = !string.IsNullOrEmpty(txtLOA8105Number.Text) ? txtLOA8105Number.Text : string.Empty;
                        }
                        else
                        {
                            if (!lbAttachment.Visible)
                            {
                                hasError += lblRefid.Text + ", ";
                            }
                        }
                    }
                    else
                    {

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

                            if (!string.IsNullOrEmpty(txtLOA8105Number.Text) && !string.IsNullOrEmpty(txt8105ProcessDate.Text) && fuAttachment.HasFile)
                            {
                                query1 += "UPDATE SRF_TRANSACTION_Warehouse_Actual_Delivery SET LOA8105 = '" + txtLOA8105Number.Text + "', Attachment = '" + attachmentFiles + "', LOA8105_AddedBy = '" + Session["LcRefId"].ToString() + "', LOA8105_AddedDate = GETDATE(), LOA8105ProcessDate ='" + txt8105ProcessDate.Text + "' WHERE RefId = '" + lblRefid.Text.Trim() + "' ";
                                attachmentFilesForEmail += Server.MapPath("~/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + attachmentFiles) + ",";
                                str8105NumberForEmail = !string.IsNullOrEmpty(txtLOA8105Number.Text) ? txtLOA8105Number.Text : string.Empty;
                            }
                            else
                            {
                                if (!lbAttachment.Visible)
                                {
                                    hasError += lblRefid.Text + ", ";
                                }
                            }


                        }

                    }



                }



                if (string.IsNullOrEmpty(hasError))
                {
                    query_Success = BLL.SRF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                    if (int.Parse(query_Success) > 0)
                    {
                        //SEND EMAIL

                        string verbiage = "Please know that delivery for <b>" + lblCtrlNo2.Text + "</b> with 8105 / 8112 NUMBER <b>" + str8105NumberForEmail.ToUpper() + "</b> has been successfully delivered. <br/>" +
                                           "Expect a hard copy of documents from Warehouse and do coordinate with them if needed. <br/>" +
                                           "Check the attached documents. <br/><br/>" +
                                           "Thank you and have a great day! <br/><br/>" +
                                           "ROHM Electronics Philippines Inc.";

                        string emailSuccess = string.Empty;

                        string emailService = COMMON.sendEmailToSuppliersForSRFWarehouse(ConfigurationManager.AppSettings["SRF_IMPEX_EMAIL"], ConfigurationManager.AppSettings["email-username"], "SRF 8105 / 8112 LIQUIDATION",
                                                                          verbiage, attachmentFilesForEmail.Substring(0, attachmentFilesForEmail.Length - 1).ToString(), string.Empty, string.Empty);

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
                        Session["successTransactionName"] = "SRF_8105Entry";
                        Session["successReturnPage"] = "SRF_8105Entry2.aspx";
                        Response.Redirect("SuccessPage.aspx");
                    }

                }
                else
                {
                    // DO NOTHING
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "javascript:return false;", true);
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please check " + hasError + " RefId Maybe attachment or 8105 / 8112 NUMBER OR Process Date is empty or check the one-time attachment if you are attaching 1 file for all items.');", true);
                }

                





            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        //protected void lbAttachment_Click(object sender, EventArgs e)
        //{
        //    //Response.Redirect("http://10.27.1.170:9292/SRF_Request/" + lblCtrlNo2.Text.Trim() + "/" + lb8105_1.Text.Trim(), true);
        //}

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

                            if (ddPezaNonPezaReport.SelectedItem.Text.ToUpper() == "PEZA") //WAREHOUSE USERS
                            {
                                if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                                {
                                    list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "1").ToList();
                                }
                                else
                                {
                                    list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "1" && itm.StatAll == ddReportStatus.SelectedItem.Text.ToUpper()).ToList();
                                }
                            }
                            else if (ddPezaNonPezaReport.SelectedItem.Text.ToUpper() == "NON PEZA") //MAM JARED (IMPEX)
                            {
                                if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                                {
                                    list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "2").ToList();
                                }
                                else
                                {
                                    list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "2" && itm.StatAll == ddReportStatus.SelectedItem.Text.ToUpper()).ToList();
                                }
                            }
                            else
                            {
                                if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                                {
                                    list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim());
                                }
                                else
                                {
                                    list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.StatAll == ddReportStatus.SelectedItem.Text.ToUpper()).ToList();
                                }
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
                                        draft.SetCellValue("L" + cnt.ToString(), elist.PullOutServiceDate_From_HEAD);
                                        draft.SetCellValue("M" + cnt.ToString(), elist.StatAll);

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

                        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

                        if (ddPezaNonPezaReport.SelectedItem.Text.ToUpper() == "PEZA") //WAREHOUSE USERS
                        {
                            if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                            {
                                list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "1" && string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                            }
                            else
                            {
                                list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "1" && string.IsNullOrEmpty(itm.GatePassNo_From_HEAD) && itm.StatAll == ddReportStatus.SelectedItem.Text.ToUpper()).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                            }
                        }
                        else if (ddPezaNonPezaReport.SelectedItem.Text.ToUpper() == "NON PEZA") //MAM JARED (IMPEX)
                        {
                            if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                            {
                                list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "2" && string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                            }
                            else
                            {
                                list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "2" && string.IsNullOrEmpty(itm.GatePassNo_From_HEAD) && itm.StatAll == ddReportStatus.SelectedItem.Text.ToUpper()).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                            }
                        }
                        else
                        {
                            if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                            {
                                list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                            }
                            else
                            {
                                list = BLL.SRF_TRANSACTION_Warehouse_Reporting(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => string.IsNullOrEmpty(itm.GatePassNo_From_HEAD) && itm.StatAll == ddReportStatus.SelectedItem.Text.ToUpper()).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                            }
                        }


                        if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()))
                        {
                            Decimal listCount = list.Count;
                            int templateNumber = 0;
                            Decimal ans = Decimal.Divide(listCount, 20);
                            int ans2 = (int)ans;

                            if (ans > ans2)
                            {
                                templateNumber = ans2 + 1;
                            }
                            else
                            {
                                templateNumber = ans2;
                            }



                            if (!System.IO.File.Exists(Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LIQUIDATION" + ".xlsx")))
                            {
                                System.IO.File.Copy(Server.MapPath("~/SRF_XLS/Template/SRF_Liquidation_" + templateNumber + ".xlsx"), Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LIQUIDATION" + ".xlsx"));
                            }
                            else
                            {
                                System.IO.File.Delete(Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LIQUIDATION" + ".xlsx"));
                                System.IO.File.Copy(Server.MapPath("~/SRF_XLS/Template/SRF_Liquidation_" + templateNumber + ".xlsx"), Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LIQUIDATION" + ".xlsx"));
                            }



                            int tempCnt = 0;

                            //for (int tempCnt = 1; tempCnt <= templateNumber; tempCnt++)
                            //{


                            if (list != null)
                            {
                                if (list.Count > 0)
                                {
                                    int cnt = 10;
                                    string previousItemName = string.Empty;
                                    string previousSupplier = string.Empty;
                                    int recCount = 0;

                                    List<Entities_SRF_RequestEntry> listDistinctSupplierName = new List<Entities_SRF_RequestEntry>();
                                    if (ddPezaNonPezaReport.SelectedItem.Text.ToUpper() == "PEZA") //WAREHOUSE USERS
                                    {
                                        if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                                        {
                                            listDistinctSupplierName = BLL.SRF_TRANSACTION_Warehouse_Reporting_Distinct_SupplierName(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "1" && string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                                        }
                                        else
                                        {
                                            listDistinctSupplierName = BLL.SRF_TRANSACTION_Warehouse_Reporting_Distinct_SupplierName(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "1" && string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                                        }
                                    }
                                    else if (ddPezaNonPezaReport.SelectedItem.Text.ToUpper() == "NON PEZA") //MAM JARED (IMPEX)
                                    {
                                        if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                                        {
                                            listDistinctSupplierName = BLL.SRF_TRANSACTION_Warehouse_Reporting_Distinct_SupplierName(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "2" && string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                                        }
                                        else
                                        {
                                            listDistinctSupplierName = BLL.SRF_TRANSACTION_Warehouse_Reporting_Distinct_SupplierName(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => itm.Warehouse_PezaNonPeza == "2" && string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                                        }
                                    }
                                    else
                                    {
                                        if (ddReportStatus.SelectedItem.Text.ToUpper() == "ALL")
                                        {
                                            listDistinctSupplierName = BLL.SRF_TRANSACTION_Warehouse_Reporting_Distinct_SupplierName(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                                        }
                                        else
                                        {
                                            listDistinctSupplierName = BLL.SRF_TRANSACTION_Warehouse_Reporting_Distinct_SupplierName(txtSearch.Text, txtFrom.Text.Trim(), txtTo.Text.Trim()).Where(itm => string.IsNullOrEmpty(itm.GatePassNo_From_HEAD)).ToList().OrderBy(itm => itm.Warehouse_SupplierName).ToList();
                                        }
                                    }

                                    if (listDistinctSupplierName != null)
                                    {

                                        foreach (Entities_SRF_RequestEntry eDistinct in listDistinctSupplierName.GroupBy(itm => itm.Warehouse_SupplierName).Select(g => g.First()))
                                        {

                                            int lessThan20 = list.Where(itm => int.Parse(itm.Warehouse_TotalActualQuantity) > 0 && itm.Warehouse_SupplierName.Replace(" ", "").Trim() == eDistinct.Warehouse_SupplierName.Replace(" ", "").Trim()).ToList().Count();
                                            string sheetName = eDistinct.Warehouse_SupplierName.Length > 10 ? eDistinct.Warehouse_SupplierName.Substring(0, 10).ToString() + "-" : eDistinct.Warehouse_SupplierName + "-";

                                            int skip = 0;
                                            int skipCount = 0;
                                            int take = 20;
                                            int counterForActualRecords = 1;

                                            Decimal listCount3 = lessThan20;
                                            int templateNumber3 = 0;
                                            Decimal ans3 = Decimal.Divide(listCount3, 20);
                                            int ans33 = (int)ans3;

                                            if (ans3 > ans33)
                                            {
                                                templateNumber3 = ans33 + 1;
                                            }
                                            else
                                            {
                                                templateNumber3 = ans33;
                                            }

                                            for (int tempCnt2 = 1; tempCnt2 <= templateNumber3; tempCnt2++)
                                            {

                                                foreach (Entities_SRF_RequestEntry elist in list.Where(itm => int.Parse(itm.Warehouse_TotalActualQuantity) > 0 && itm.Warehouse_SupplierName.Replace(" ", "").Trim() == eDistinct.Warehouse_SupplierName.Replace(" ", "").Trim()).ToList().Skip(skipCount).Take(lessThan20 < 20 ? lessThan20 : take).ToList())
                                                {

                                                    string path_LIQUIDATION = Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_LIQUIDATION" + ".xlsx");
                                                    Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + path_LIQUIDATION);
                                                    FileStream fsBI_LIQUIDATION = new FileStream(path_LIQUIDATION, FileMode.Open);
                                                    using (SLDocument draft_LIQUIDATION = new SLDocument(fsBI_LIQUIDATION, "LT" + (tempCnt + tempCnt2).ToString()))
                                                    {

                                                        if (cnt <= 29)
                                                        {


                                                            draft_LIQUIDATION.SetCellValue("E4", "RECIPIENT/CONSIGNEE: " + elist.Warehouse_SupplierName);
                                                            draft_LIQUIDATION.SetCellValue("B6", elist.LOA_Number_From_HEAD);
                                                            draft_LIQUIDATION.SetCellValue("G6", elist.LOA_MaturityDate_From_HEAD);
                                                            draft_LIQUIDATION.SetCellValue("M6", elist.LOASuretyBond_From_HEAD);

                                                            draft_LIQUIDATION.SetCellValue("A10", elist.Warehouse_SupplierName);

                                                            if (!string.IsNullOrEmpty(elist.Warehouse_ItemName.Trim()))
                                                            {
                                                                if ((elist.Warehouse_ItemName.Trim() + elist.Warehouse_LOA8106.Trim()) != previousItemName)
                                                                {
                                                                    draft_LIQUIDATION.SetCellValue("B" + cnt.ToString(), elist.Warehouse_ItemName);
                                                                    draft_LIQUIDATION.SetCellValue("C" + cnt.ToString(), elist.Warehouse_LOA8106);
                                                                    draft_LIQUIDATION.SetCellValue("D" + cnt.ToString(), elist.PullOutServiceDate_From_HEAD);
                                                                    draft_LIQUIDATION.SetCellValue("E" + cnt.ToString(), elist.Warehouse_TotalQuantity);
                                                                    draft_LIQUIDATION.SetCellValueNumeric("F" + cnt.ToString(), (double.Parse(elist.Warehouse_TotalQuantity) * double.Parse(elist.LOAPriceValue_From_HEAD)).ToString());
                                                                }
                                                                //draft_LIQUIDATION.SetCellValueNumeric("F" + cnt.ToString(), (double.Parse(elist.Warehouse_TotalQuantity) * double.Parse(elist.LOAPriceValue_From_HEAD)).ToString());
                                                                draft_LIQUIDATION.SetCellValueNumeric("M" + cnt.ToString(), (double.Parse(elist.Warehouse_TotalActualQuantity) * double.Parse(elist.LOAPriceValue_From_HEAD)).ToString());

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


                                                            if (lessThan20 <= 20 && lessThan20.ToString() == counterForActualRecords.ToString())
                                                            {

                                                                draft_LIQUIDATION.RenameWorksheet("LT" + (tempCnt + tempCnt2).ToString(), sheetName + tempCnt2.ToString());

                                                            }

                                                            if (counterForActualRecords.ToString() == "20")
                                                            {
                                                                skip++;
                                                                skipCount = skip * 20;

                                                                draft_LIQUIDATION.RenameWorksheet("LT" + (tempCnt + tempCnt2).ToString(), sheetName + tempCnt2.ToString());

                                                                counterForActualRecords = 1;
                                                                cnt = 10;

                                                            }


                                                        }

                                                        previousItemName = elist.Warehouse_ItemName.Trim() + elist.Warehouse_LOA8106.Trim();
                                                        previousSupplier = elist.Warehouse_SupplierName.Replace(" ", "").Trim();
                                                        cnt++;
                                                        counterForActualRecords++;

                                                        fsBI_LIQUIDATION.Close();
                                                        draft_LIQUIDATION.SaveAs(path_LIQUIDATION);

                                                    }



                                                }



                                            }

                                            tempCnt++;

                                            //}




                                            cnt = 10;

                                        }

                                    }


                                }
                            }







                            //}


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












    }
}
