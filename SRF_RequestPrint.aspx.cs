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
//using REPI_PUR_SOFRA.App_Code.ENTITIES;
//using REPI_PUR_SOFRA.App_Code.BLL;
using System.Collections.Generic;

namespace REPI_PUR_SOFRA
{
    public partial class SRF_RequestPrint : System.Web.UI.Page
    {

        BLL_SRF BLL = new BLL_SRF();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["SRF_ControlNo_From_Details"].ToString().Length > 0)
                {
                    Set_Existing_Item_For_Print(CryptorEngine.Decrypt(Request.QueryString["SRF_ControlNo_From_Details"].ToString().Replace(" ", "+"), true));
                    SRF_CTRLNo.InnerHtml = "SERVICE REPAIR PRINT FORM ( <b>" + CryptorEngine.Decrypt(Request.QueryString["SRF_ControlNo_From_Details"].ToString().Replace(" ", "+"), true) + "</b> )";
                }
            }
        }


        private void Set_Existing_Item_For_Print(string ctrno)
        {
            try
            {

                List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
                list = BLL.SRF_TRANSACTION_RequestEntry_ByControlNo(ctrno);

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
                        details = BLL.SRF_TRANSACTION_RequestEntry_Details_ByControlNo(entity);

                        if (details.Count > 0)
                        {
                            gvData.DataSource = details;
                            gvData.DataBind();
                            gvData.Visible = true;

                        }

                    }



                }


                // CHECK IF ITEM IS ALREADY APPROVED THEN SUBMIT BUTTON MUST NOT BE ALLOWED
                List<Entities_SRF_RequestEntry> approved = new List<Entities_SRF_RequestEntry>();
                approved = BLL.SRF_TRANSACTION_RequestStatus_ByControlNo(ctrno);

                if (approved.Count > 0)
                {
                    divApprover.Visible = true;

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

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
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


    }
}
