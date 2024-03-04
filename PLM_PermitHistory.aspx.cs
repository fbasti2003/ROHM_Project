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
using System.Globalization;


namespace REPI_PUR_SOFRA
{
    public partial class PLM_PermitHistory : System.Web.UI.Page
    {

        BLL_PLM BLL = new BLL_PLM();
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
            try
            {
                List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();
                list = BLL.PLM_TRANSACTION_Request_GetAll();

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    divOpacity.Style.Add("opacity", "1");
                    divLoader.Style.Add("display", "none");

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();
                list = BLL.PLM_TRANSACTION_Request_GetAll_Like(txtSearch.Text);

                if (list.Count > 0)
                {
                    gvData.DataSource = list;
                    gvData.DataBind();

                    divOpacity.Style.Add("opacity", "1");
                    divLoader.Style.Add("display", "none");

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("PLM_PermitEntry.aspx?RefId=", false);
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

                Label lblStatus = row.FindControl("lblStatus") as Label;
                LinkButton lblView = row.FindControl("lblView") as LinkButton;
                LinkButton lbEditRecord = row.FindControl("lbEditRecord") as LinkButton;
                LinkButton lblHistory = row.FindControl("lblHistory") as LinkButton;
                HtmlControl divDetails = row.FindControl("divDetails") as HtmlControl;
                HtmlControl divHistory = row.FindControl("divHistory") as HtmlControl;
                Label lblGovernmentAgency = row.FindControl("lblGovernmentAgency") as Label;
                TextBox txtPermitName = row.FindControl("txtPermitName") as TextBox;
                TextBox txtChemicalContent = row.FindControl("txtChemicalContent") as TextBox;
                TextBox txtItemName = row.FindControl("txtItemName") as TextBox;
                TextBox txtSpecification = row.FindControl("txtSpecification") as TextBox;
                TextBox txtSpecifiedRequirements = row.FindControl("txtSpecifiedRequirements") as TextBox;
                TextBox txtFrequency = row.FindControl("txtFrequency") as TextBox;
                TextBox txtLeadTime = row.FindControl("txtLeadTime") as TextBox;
                TextBox txtProcessorName = row.FindControl("txtProcessorName") as TextBox;
                TextBox txtSafety = row.FindControl("txtSafety") as TextBox;
                TextBox txtIssuedDate = row.FindControl("txtIssuedDate") as TextBox;
                TextBox txtExpirationDate = row.FindControl("txtExpirationDate") as TextBox;
                //DropDownList ddSupplier = row.FindControl("ddSupplier") as DropDownList;
                DropDownList ddGovernmentAgencies = row.FindControl("ddGovernmentAgencies") as DropDownList;
                //Label lblSupplierName = row.FindControl("lblSupplierName") as Label;
                Label lblRefId = row.FindControl("lblRefId") as Label;
                FileUpload fu1 = row.FindControl("fu1") as FileUpload;
                LinkButton lbAttachment1 = row.FindControl("lbAttachment1") as LinkButton;
                LinkButton lbAttachment2 = row.FindControl("lbAttachment2") as LinkButton;
                LinkButton lbAttachment3 = row.FindControl("lbAttachment3") as LinkButton;
                Label lblPermitId = row.FindControl("lblPermitId") as Label;

                GridView gvHistory = row.FindControl("gvHistory") as GridView;
                GridView gvSuppliers = row.FindControl("gvSuppliers") as GridView;


                if (e.CommandName == "lblView_Command")
                {
                    if (lblView.Text.ToUpper() == "DETAILS")
                    {
                        divDetails.Style.Add("display", "block");
                        lblView.Text = "CLOSE";

                        txtPermitName.Enabled = false;
                        txtChemicalContent.Enabled = false;
                        txtItemName.Enabled = false;
                        txtSpecification.Enabled = false;
                        txtSpecifiedRequirements.Enabled = false;
                        txtFrequency.Enabled = false;
                        txtLeadTime.Enabled = false;
                        txtProcessorName.Enabled = false;
                        txtSafety.Enabled = false;
                        //ddSupplier.Enabled = false;
                        txtIssuedDate.Enabled = false;
                        txtExpirationDate.Enabled = false;

                        //---------------------------------------------------------------------------------------------------

                        List<Entities_PLM_PermitCertificates> listDropDown = new List<Entities_PLM_PermitCertificates>();
                        listDropDown = BLL.PLM_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                        if (listDropDown != null)
                        {
                            if (listDropDown.Count > 0)
                            {
                                ddGovernmentAgencies.Items.Clear();
                                ddGovernmentAgencies.Items.Add("");

                                foreach (Entities_PLM_PermitCertificates entityGA in listDropDown)
                                {
                                    ListItem item = new ListItem();
                                    item.Text = entityGA.DropdownName.ToUpper();
                                    item.Value = entityGA.DropdownRefId;

                                    if (entityGA.IsDisabled == string.Empty || entityGA.IsDisabled == "0")
                                    {
                                        if (entityGA.TableName == "PLM_MT_GovernmentAgencies")
                                        {
                                            ddGovernmentAgencies.Items.Add(item);
                                        }
                                    }

                                }

                                if (!string.IsNullOrEmpty(lblGovernmentAgency.Text))
                                {
                                    ddGovernmentAgencies.Items.FindByValue(lblGovernmentAgency.Text).Selected = true;
                                    ddGovernmentAgencies.Enabled = false;
                                }

                            }
                        }
                        //---------------------------------------------------------------------------------------------------

                        List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();
                        Entities_PLM_PermitCertificates entity = new Entities_PLM_PermitCertificates();
                        entity.PermitId = lblPermitId.Text.Trim();
                        entity.IssuedDate = txtIssuedDate.Text.Substring(6, 4) + "/" + txtIssuedDate.Text.Substring(0, 2) + "/" + txtIssuedDate.Text.Substring(3, 2);
                        entity.ExpirationDate = txtExpirationDate.Text.Substring(6, 4) + "/" + txtExpirationDate.Text.Substring(0, 2) + "/" + txtExpirationDate.Text.Substring(3, 2);

                        list = BLL.PLM_TRANSACTION_NotificationReceiver_GetByRefIdAndSupplierIdAndIssuedDateAndExpiration(entity);

                        if (list != null)
                        {
                            if (list.Count > 0)
                            {
                                gvSuppliers.DataSource = list;
                                gvSuppliers.DataBind();
                            }
                        }

                    }
                    else
                    {
                        divDetails.Style.Add("display", "none");
                        divHistory.Style.Add("display", "none");
                        lblView.Text = "DETAILS";
                        lbEditRecord.Text = "EDIT RECORD";
                    }
                }

                if (e.CommandName == "lbRenew_Command")
                {
                    Response.Redirect("PLM_PermitEntry.aspx?RefId=" + CryptorEngine.Encrypt(lblRefId.Text.Trim(), true) + "&renew=true&status=" + lblStatus.Text.Trim().ToLower(), false);
                }

                if (e.CommandName == "EditRecord_Command")
                {
                    try
                    {
                        // UPDATE REOCRD
                        if (lbEditRecord.Text.ToUpper() == "UPDATE RECORD")
                        {
                            try
                            {

                                DateTime parsedIssuedDate;
                                DateTime parsedExpirationDate;


                                if (!DateTime.TryParseExact(txtIssuedDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedIssuedDate))
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('INVALID Issued Date');", true);
                                }
                                else if (!DateTime.TryParseExact(txtExpirationDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedExpirationDate))
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('INVALID Expiration Date');", true);
                                }
                                else
                                {
                                    //string filename1 = Path.GetFileName(fu1.FileName);
                                    //string fileExtensionApplication = Path.GetExtension(filename1);
                                    //string newName = txtPermitName.Text.Trim() + "-" + txtExpirationDate.Text.Replace("/", "").Trim() + fileExtensionApplication;

                                    string success = string.Empty;
                                    Entities_PLM_PermitCertificates entity = new Entities_PLM_PermitCertificates();
                                    entity.RefId = lblRefId.Text;
                                    entity.PermitName = txtPermitName.Text.Replace("'", "''");
                                    entity.ChemicalContent = txtChemicalContent.Text.Replace("'", "''");
                                    entity.ItemName = txtItemName.Text.Replace("'", "''");
                                    entity.Specification = txtSpecification.Text.Replace("'", "''");
                                    entity.GovernmentAgency = ddGovernmentAgencies.SelectedValue;
                                    entity.Frequency = txtFrequency.Text.Replace("'", "''");
                                    entity.SpecifiedRequirements = txtSpecifiedRequirements.Text.Replace("'", "''");
                                    entity.LeadTime = txtLeadTime.Text.Replace("'", "''");
                                    entity.Safety = txtSafety.Text.Replace("'", "''");
                                    entity.ProcessorName = txtProcessorName.Text.Replace("'", "''");
                                    entity.IssuedDate = txtIssuedDate.Text.Substring(6, 4) + "/" + txtIssuedDate.Text.Substring(0, 2) + "/" + txtIssuedDate.Text.Substring(3, 2);
                                    entity.ExpirationDate = txtExpirationDate.Text.Substring(6, 4) + "/" + txtExpirationDate.Text.Substring(0, 2) + "/" + txtExpirationDate.Text.Substring(3, 2);
                                    //entity.Supplier = ddSupplier.SelectedValue;
                                    entity.Status = "0";
                                    entity.UpdatedBy = Session["LcRefId"].ToString();
                                    entity.Attachment1 = "";

                                    //if (fu1.HasFile)
                                    //{
                                    //    entity.Attachment1 = newName;
                                    //}

                                    success = BLL.PLM_TRANSACTION_Request_Append(entity).ToString();

                                    if (!string.IsNullOrEmpty(success))
                                    {
                                        if (success == "-1")
                                        {

                                            //// SAVE ATTACHMENT IF THERE IS ANY
                                            //if (fu1.HasFile)
                                            //{
                                            //    fu1.SaveAs(Path.Combine(Server.MapPath("~/PLM_Request/"), filename1));
                                            //    fu1.Dispose();
                                            //    File.Copy(Path.Combine(Server.MapPath("~/PLM_Request/"), filename1), Path.Combine(Server.MapPath("~/PLM_Request/"), (newName)), true);
                                            //    File.Delete(Path.Combine(Server.MapPath("~/PLM_Request/"), filename1));
                                            //}

                                            Session["successMessage"] = "CERTIFICATE NAME : <b>" + txtPermitName.Text.ToUpper() + "</b> HAS BEEN SUCCESSFULLY SAVED.";
                                            Session["successTransactionName"] = "PLM_Permit";
                                            Session["successReturnPage"] = "PLM_Permit.aspx";

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

                        if (lbEditRecord.Text.ToUpper() == "EDIT RECORD")
                        {

                            Response.Redirect("PLM_PermitEntry.aspx?RefId=" + CryptorEngine.Encrypt(lblRefId.Text.Trim(), true) + "&renew=false&status=" + lblStatus.Text.Trim().ToLower(), false);

                            //txtPermitName.Enabled = true;
                            //txtChemicalContent.Enabled = true;
                            //txtItemName.Enabled = true;
                            //txtSpecification.Enabled = true;
                            //txtSpecifiedRequirements.Enabled = true;
                            //ddGovernmentAgencies.Enabled = true;
                            //txtFrequency.Enabled = true;
                            //txtLeadTime.Enabled = true;
                            //txtProcessorName.Enabled = true;
                            //txtSafety.Enabled = true;
                            ////ddSupplier.Enabled = true;
                            //txtIssuedDate.Enabled = true;
                            //txtExpirationDate.Enabled = true;
                            //gvSuppliers.Enabled = true;

                            //---------------------------------------------------------------------------------------------------

                            //List<Entities_PLM_PermitCertificates> listDropDown = new List<Entities_PLM_PermitCertificates>();
                            //listDropDown = BLL.PLM_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                            //if (listDropDown != null)
                            //{
                            //    if (listDropDown.Count > 0)
                            //    {
                            //        ddSupplier.Items.Add("");

                            //        foreach (Entities_PLM_PermitCertificates entity in listDropDown)
                            //        {
                            //            ListItem item = new ListItem();
                            //            item.Text = entity.DropdownName.ToUpper();
                            //            item.Value = entity.DropdownRefId;

                            //            if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                            //            {
                            //                if (entity.TableName == "PLM_MT_Supplier")
                            //                {
                            //                    ddSupplier.Items.Add(item);
                            //                }
                            //            }

                            //        }

                            //        if (!string.IsNullOrEmpty(lblSupplierName.Text))
                            //        {
                            //            ddSupplier.Items.FindByText(lblSupplierName.Text).Selected = true;
                            //        }

                            //    }
                            //}


                            //lbEditRecord.Text = "UPDATE RECORD";
                        }

                        //---------------------------------------------------------------------------------------------------




                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                    }
                }

                if (e.CommandName == "lblHistory_Command")
                {
                    if (lblHistory.Text.ToUpper() == "HISTORY")
                    {
                        lblHistory.Text = "HIDE HISTORY";
                        divHistory.Style.Add("display", "block");

                        List<Entities_PLM_PermitCertificates> history = new List<Entities_PLM_PermitCertificates>();
                        history = BLL.PLM_TRANSACTION_Request_History_GetByRefId(lblRefId.Text.Trim());

                        if (history != null)
                        {
                            if (history.Count > 0)
                            {
                                gvHistory.DataSource = history;
                                gvHistory.DataBind();
                            }
                        }
                    }
                    else
                    {
                        lblHistory.Text = "HISTORY";
                        divHistory.Style.Add("display", "none");
                    }


                }

                if (e.CommandName == "DLA1_Command")
                {
                    try
                    {
                        string strURL = "~/PLM_Request/" + lbAttachment1.Text;
                        Response.Redirect(strURL, false);
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                    }
                }

                if (e.CommandName == "DLA2_Command")
                {
                    try
                    {
                        string strURL = "~/PLM_Request/" + lbAttachment2.Text;
                        Response.Redirect(strURL, false);
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                    }
                }

                if (e.CommandName == "DLA3_Command")
                {
                    try
                    {
                        string strURL = "~/PLM_Request/" + lbAttachment3.Text;
                        Response.Redirect(strURL, false);
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                    }
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.StackTrace.ToString() + "');", true);
            }
        }

        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                    Label lblColorCode = (Label)e.Row.FindControl("lblColorCode");

                    //if (lblStatus.Text.ToUpper() == "TAKE ACTION")
                    //{
                    //    lblStatus.Style.Add("color", "Green");
                    //}

                    //if (lblStatus.Text.ToUpper() == "EXPIRED")
                    //{
                    //    lblStatus.Style.Add("color", "Red");
                    //}
                    lblStatus.Style.Add("color", lblColorCode.Text.Trim());
                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

                }
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }
        

    }
}
