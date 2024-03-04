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
    public partial class SE_SupplierEvaluationForApproval : System.Web.UI.Page
    {
        BLL_SE BLL = new BLL_SE();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDefault();
            }
        }

        private void LoadDefault()
        {
            //try
            //{
            //    List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();
            //    list = BLL.SE_MT_FiscalYear_GetAll();

            //    if (list != null)
            //    {
            //        if (list.Count > 0)
            //        {
            //            ddFiscalYear.Items.Clear();
            //            ddFiscalYear.Items.Add("");

            //            foreach (Entities_SE_FiscalYear entity in list)
            //            {
            //                ListItem item = new ListItem();
            //                item.Text = entity.Description;
            //                item.Value = entity.RefId;

            //                if (entity.Isdisabled == string.Empty || entity.Isdisabled == "0")
            //                {
            //                    ddFiscalYear.Items.Add(item);
            //                }

            //            }
            //        }
            //    }
                

            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            //}

            try
            {
                string isSectionManager = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "15").ToString();
                string isDepartmentManager = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "16").ToString();
                string isDivisionManager = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "17").ToString();

                List<Entities_SE_FiscalYear> list_Monitoring = new List<Entities_SE_FiscalYear>();

                //// Is Purchasing Incharge
                //if (isSectionManager.ToLower() == "true")
                //{
                //    Session["ForApproval"] = "SECTION_INCHARGE";
                //    list_Monitoring = BLL.SE_TRANSACTION_Monitoring_GetAll().Where(itm => itm.ForApproval == "1" && itm.SectionIncharge.ToUpper() == "PENDING APPROVAL").ToList();
                //    //list_Monitoring = BLL.SE_TRANSACTION_Monitoring_GetAll().Where(itm => itm.ForApproval == "1" && itm.Status.ToUpper() == "FOR SECTION INCHARGE APPROVAL").ToList();
                //}
                //// Is Purchasiing Department Manager
                //if (isDepartmentManager.ToLower() == "true")
                //{
                //    Session["ForApproval"] = "DEPARTMENT_MANAGER";
                //    list_Monitoring = BLL.SE_TRANSACTION_Monitoring_GetAll().Where(itm => itm.ForApproval == "1" && ((itm.SectionIncharge.ToUpper() == "PENDING APPROVAL" && itm.DeptManager == "PENDING APPROVAL") || (itm.SectionIncharge.ToUpper() != "PENDING APPROVAL" && itm.DeptManager == "PENDING APPROVAL"))).ToList();
                //    //list_Monitoring = BLL.SE_TRANSACTION_Monitoring_GetAll().Where(itm => itm.ForApproval == "1" && itm.Status.ToUpper() == "FOR DEPT. MANAGER APPROVAL").ToList();
                //}
                //// Is Purchasing Division Manager
                //if (isDivisionManager.ToLower() == "true")
                //{
                //    Session["ForApproval"] = "DIVISION_MANAGER";
                //    list_Monitoring = BLL.SE_TRANSACTION_Monitoring_GetAll().Where(itm => itm.ForApproval == "1" && itm.SectionIncharge.ToUpper() != "PENDING APPROVAL" && itm.DeptManager != "PENDING APPROVAL" && itm.DivManager == "PENDING APPROVAL").ToList();
                //    //list_Monitoring = BLL.SE_TRANSACTION_Monitoring_GetAll().Where(itm => itm.ForApproval == "1" && itm.Status.ToUpper() == "FOR DIV. MANAGER APPROVAL").ToList();
                //}

                if (isSectionManager.ToLower() == "true" || isDepartmentManager.ToLower() == "true" || isDivisionManager.ToLower() == "true")
                {
                    list_Monitoring = BLL.SE_TRANSACTION_Monitoring_GetAll().Where(itm => itm.ForApproval == "1" && itm.DivManager.ToUpper() == "PENDING APPROVAL").ToList();
                }

                if (list_Monitoring != null)
                {
                    if (list_Monitoring.Count > 0)
                    {
                        gvData.Visible = true;
                        gvData.DataSource = list_Monitoring;
                        gvData.DataBind();
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

        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
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
                Label lblId = row.FindControl("lblId") as Label;
                Label lblStatus = row.FindControl("lblStatus") as Label;

                ImageButton ibApproved = row.FindControl("ibApproved") as ImageButton;
                ImageButton ibDisapproved = row.FindControl("ibDisapproved") as ImageButton;
                ImageButton ibPreview = row.FindControl("ibPreview") as ImageButton;
                TextBox txtRemarks = row.FindControl("txtRemarks") as TextBox;
                TextBox txtDivManagerInstructions = row.FindControl("txtDivManagerInstructions") as TextBox;


                if (e.CommandName == "lbFiscalYear_Command")
                {
                    Response.Redirect("SE_SupplierEvaluation_ReportDetails.aspx?fy_year=" + lblId.Text.Trim(), false);

                    //List<Entities_SE_RequestEntry> list_SupplierEvaluation = new List<Entities_SE_RequestEntry>();
                    //list_SupplierEvaluation = BLL.SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear(lblId.Text.Trim());

                    //if (list_SupplierEvaluation != null)
                    //{
                    //    if (list_SupplierEvaluation.Count > 0)
                    //    {
                    //        gvDetails.Visible = true;
                    //        gvDetails.DataSource = list_SupplierEvaluation;
                    //        gvDetails.DataBind();                            
                    //    }
                    //    else
                    //    {
                    //        gvDetails.Visible = false;
                    //    }                        
                    //}
                    //else
                    //{
                    //    gvDetails.Visible = false;
                    //}

                    //ScriptManager.RegisterStartupScript(this, GetType(), "ShowRegistered", "showDialog();", true);
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
                            if (lblStatus.Text.ToUpper() == "FOR DIV. MANAGER APPROVAL")
                            {
                                ibApproved.ImageUrl = "~/images/A2.png";
                                txtDivManagerInstructions.Enabled = true;
                            }
                            else
                            {
                                ibApproved.ImageUrl = "~/images/A2.png";
                                txtDivManagerInstructions.Enabled = false;
                            }
                        }
                        else
                        {                            
                            ibApproved.ImageUrl = "~/images/A1.png";
                            txtDivManagerInstructions.Enabled = false;
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

                if (e.CommandName == "ibPreview_Command")
                {
                    Response.Redirect("SE_EvaluationEntry.aspx?fy_year=" + lblId.Text.Trim() + "&send_evaluation=false", false);
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('3 - " + ex.StackTrace.ToString() + "');", true);
            }
        }

        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string isSectionManager = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "15").ToString();
        //        string isDepartmentManager = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "16").ToString();
        //        string isDivisionManager = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "17").ToString();

        //        if (string.IsNullOrEmpty(ddFiscalYear.SelectedItem.Text))
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select a valid Fiscal Year.');", true);
        //        }
        //        else
        //        {
        //            List<Entities_SE_FiscalYear> list_Monitoring = new List<Entities_SE_FiscalYear>();

        //            // Is Purchasing Incharge
        //            if (isSectionManager.ToLower() == "true")
        //            {
        //                Session["ForApproval"] = "SECTION_INCHARGE";
        //                list_Monitoring = BLL.SE_TRANSACTION_Monitoring_GetAll().Where(itm => itm.Description == ddFiscalYear.SelectedItem.Text && itm.ForApproval == "1" && itm.SectionIncharge.ToUpper() == "PENDING APPROVAL").ToList();
        //            }
        //            // Is Purchasiing Department Manager
        //            if (isDepartmentManager.ToLower() == "true")
        //            {
        //                Session["ForApproval"] = "DEPARTMENT_MANAGER";
        //                list_Monitoring = BLL.SE_TRANSACTION_Monitoring_GetAll().Where(itm => itm.Description == ddFiscalYear.SelectedItem.Text && itm.ForApproval == "1" && itm.SectionIncharge.ToUpper() != "PENDING APPROVAL" && itm.DeptManager == "PENDING APPROVAL").ToList();
        //            }
        //            // Is Purchasing Division Manager
        //            if (isDivisionManager.ToLower() == "true")
        //            {
        //                Session["ForApproval"] = "DIVISION_MANAGER";
        //                list_Monitoring = BLL.SE_TRANSACTION_Monitoring_GetAll().Where(itm => itm.Description == ddFiscalYear.SelectedItem.Text && itm.ForApproval == "1" && itm.SectionIncharge.ToUpper() != "PENDING APPROVAL" && itm.DeptManager != "PENDING APPROVAL" && itm.DivManager == "PENDING APPROVAL").ToList();
        //            }

        //            if (list_Monitoring != null)
        //            {
        //                if (list_Monitoring.Count > 0)
        //                {
        //                    gvData.Visible = true;
        //                    gvData.DataSource = list_Monitoring;
        //                    gvData.DataBind();
        //                }
        //                else
        //                {
        //                    gvData.Visible = false;
        //                }
        //            }
        //            else
        //            {
        //                gvData.Visible = false;
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
        //    }
        //}

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvData.Rows.Count > 0)
                {
                    //if (Session["ForApproval"] != null)
                    //{
                    //    if (!string.IsNullOrEmpty(Session["ForApproval"].ToString()))
                    //    {

                            string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                            string query1 = string.Empty;
                            string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                            string query_Success = string.Empty;
                            int disapproved = 0;
                            string fiscalYear = string.Empty;

                            if (gvData.Rows.Count > 0)
                            {
                                for (int i = 0; i < gvData.Rows.Count; i++)
                                {
                                    Label lblId = (Label)gvData.Rows[i].Cells[0].FindControl("lblId");
                                    Label lblFYISupplierId = (Label)gvData.Rows[i].Cells[2].FindControl("lblFYISupplierId");
                                    ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[4].FindControl("ibApproved");
                                    ImageButton ibDisapproved = (ImageButton)gvData.Rows[i].Cells[4].FindControl("ibDisapproved");
                                    TextBox txtRemarks = (TextBox)gvData.Rows[i].Cells[5].FindControl("txtRemarks");
                                    TextBox txtDivManagerInstructions = (TextBox)gvData.Rows[i].Cells[6].FindControl("txtDivManagerInstructions");
                                    Label lblStatus = (Label)gvData.Rows[i].Cells[3].FindControl("lblStatus");

                                    if (ibApproved.ImageUrl == "~/images/A2.png")
                                    {

                                        //SECTION INCHARGE
                                        //if (Session["ForApproval"].ToString() == "SECTION_INCHARGE")
                                        if (lblStatus.Text.ToUpper() == "FOR SECTION INCHARGE APPROVAL")
                                        {
                                            //query1 = "UPDATE SE_TRANSACTION_StatusEvaluationTable SET STATSectionIncharge = 1, SectionIncharge ='" + Session["UserFullName"].ToString() + "', DOASectionIncharge = GETDATE() WHERE SUBSTRING(FY_SupplierId,0,CHARINDEX('_',FY_SupplierId)) = '" + lblId.Text.Trim() + "' ";
                                            query1 += "UPDATE SE_TRANSACTION_StatusEvaluationTable SET STATSectionIncharge = 1, SectionIncharge ='" + Session["UserFullName"].ToString() + "', DOASectionIncharge = GETDATE() WHERE FY_SupplierId = '" + lblFYISupplierId.Text.Trim() + "' ";
                                        }
                                        // DEPARTMENT MANAGER
                                        //if (Session["ForApproval"].ToString() == "DEPARTMENT_MANAGER")
                                        if (lblStatus.Text.ToUpper() == "FOR DEPT. MANAGER APPROVAL")
                                        {
                                            //query1 = "UPDATE SE_TRANSACTION_StatusEvaluationTable SET STATDepMan = 1, DepMan ='" + Session["UserFullName"].ToString() + "', DOADepMan = GETDATE() WHERE SUBSTRING(FY_SupplierId,0,CHARINDEX('_',FY_SupplierId)) = '" + lblId.Text.Trim() + "' ";
                                            query1 += "UPDATE SE_TRANSACTION_StatusEvaluationTable SET STATDepMan = 1, DepMan ='" + Session["UserFullName"].ToString() + "', DOADepMan = GETDATE() WHERE FY_SupplierId = '" + lblFYISupplierId.Text.Trim() + "' ";
                                        }
                                        // DIVISION MANAGER
                                        //if (Session["ForApproval"].ToString() == "DIVISION_MANAGER")
                                        if (lblStatus.Text.ToUpper() == "FOR DIV. MANAGER APPROVAL")
                                        {
                                            if (!string.IsNullOrEmpty(txtDivManagerInstructions.Text))
                                            {
                                                query1 += "UPDATE SE_TRANSACTION_StatusEvaluationTable SET STATDivMan = 1, DivMan ='" + Session["UserFullName"].ToString() + "', DOADivMan = GETDATE() WHERE FY_SupplierId = '" + lblFYISupplierId.Text.Trim() + "' ";
                                                query1 += "UPDATE SE_TRANSACTION_RequestDetails SET DivManInstructions = '" + txtDivManagerInstructions.Text.Replace("'","''") + "' WHERE FY_SupplierId = '" + lblFYISupplierId.Text.Trim() + "' ";
                                            }
                                            else
                                            {
                                                //query1 = "UPDATE SE_TRANSACTION_StatusEvaluationTable SET STATDivMan = 1, DivMan ='" + Session["UserFullName"].ToString() + "', DOADivMan = GETDATE() WHERE SUBSTRING(FY_SupplierId,0,CHARINDEX('_',FY_SupplierId)) = '" + lblId.Text.Trim() + "' ";
                                                query1 += "UPDATE SE_TRANSACTION_StatusEvaluationTable SET STATDivMan = 1, DivMan ='" + Session["UserFullName"].ToString() + "', DOADivMan = GETDATE() WHERE FY_SupplierId = '" + lblFYISupplierId.Text.Trim() + "' ";
                                            }
                                        }
                                    }

                                    if (ibDisapproved.ImageUrl == "~/images/DA2.png")
                                    {
                                        if (string.IsNullOrEmpty(txtRemarks.Text))
                                        {
                                            disapproved++;
                                        }

                                        //SECTION INCHARGE
                                        if (lblStatus.Text.ToUpper() == "FOR SECTION INCHARGE APPROVAL")
                                        {
                                            //query1 = "UPDATE SE_TRANSACTION_StatusEvaluationTable SET STATSectionIncharge = 2, DisapprovedRemarks ='" + txtRemarks.Text.Replace("'", "''") + "', SectionIncharge ='" + Session["UserFullName"].ToString() + "', DOASectionIncharge = GETDATE() WHERE SUBSTRING(FY_SupplierId,0,CHARINDEX('_',FY_SupplierId)) = '" + lblId.Text.Trim() + "' ";
                                            query1 += "UPDATE SE_TRANSACTION_StatusEvaluationTable SET STATSectionIncharge = 2, DisapprovedRemarks ='" + txtRemarks.Text.Replace("'", "''") + "', SectionIncharge ='" + Session["UserFullName"].ToString() + "', DOASectionIncharge = GETDATE() WHERE FY_SupplierId = '" + lblFYISupplierId.Text.Trim() + "' ";
                                        }
                                        // DEPARTMENT MANAGER
                                        if (lblStatus.Text.ToUpper() == "FOR DEPT. MANAGER APPROVAL")
                                        {
                                            //query1 = "UPDATE SE_TRANSACTION_StatusEvaluationTable SET STATDepMan = 2, DisapprovedRemarks ='" + txtRemarks.Text.Replace("'", "''") + "', DepMan ='" + Session["UserFullName"].ToString() + "' DOADepMan = GETDATE() WHERE SUBSTRING(FY_SupplierId,0,CHARINDEX('_',FY_SupplierId)) = '" + lblId.Text.Trim() + "' ";
                                            query1 += "UPDATE SE_TRANSACTION_StatusEvaluationTable SET STATDepMan = 2, DisapprovedRemarks ='" + txtRemarks.Text.Replace("'", "''") + "', DepMan ='" + Session["UserFullName"].ToString() + "' DOADepMan = GETDATE() WHERE FY_SupplierId = '" + lblFYISupplierId.Text.Trim() + "' ";
                                        }
                                        // DIVISION MANAGER
                                        if (lblStatus.Text.ToUpper() == "FOR DIV. MANAGER APPROVAL")
                                        {
                                            //query1 = "UPDATE SE_TRANSACTION_StatusEvaluationTable SET STATDivMan = 2, DisapprovedRemarks ='" + txtRemarks.Text.Replace("'", "''") + "', DivMan ='" + Session["UserFullName"].ToString() + "' DOADivMan = GETDATE() WHERE SUBSTRING(FY_SupplierId,0,CHARINDEX('_',FY_SupplierId)) = '" + lblId.Text.Trim() + "' ";
                                            query1 += "UPDATE SE_TRANSACTION_StatusEvaluationTable SET STATDivMan = 2, DisapprovedRemarks ='" + txtRemarks.Text.Replace("'", "''") + "', DivMan ='" + Session["UserFullName"].ToString() + "' DOADivMan = GETDATE() WHERE FY_SupplierId = '" + lblFYISupplierId.Text.Trim() + "' ";
                                        }                                        

                                    }

                                    fiscalYear += lblId.Text.Trim() + ", ";

                                }

                                if (!string.IsNullOrEmpty(query1))
                                {
                                    if (disapproved <= 0)
                                    {
                                        query_Success = BLL.SE_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                                        if (int.Parse(query_Success) > 0)
                                        {
                                            Session["successMessage"] = "FISCAL YEAR : <b>" + fiscalYear + "</b> HAS BEEN SUCCESSFULLY APPROVED.";
                                            Session["successTransactionName"] = "SE_SupplierEvaluation";
                                            Session["successReturnPage"] = "SE_SupplierEvaluation.aspx";
                                            Response.Redirect("SuccessPage.aspx");
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('REMARKS MUST NOT BE BLANK!');", true);
                                    }
                                }
                                
                                
                            }

                        //}
                    //}
                    
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + " - " + ex.StackTrace.ToString() + "');", true);
            }
        }











    }
}
