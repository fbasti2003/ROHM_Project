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
using System.IO;

namespace REPI_PUR_SOFRA
{
    public partial class PLM_PermitEntry : System.Web.UI.Page
    {

        BLL_PLM BLL = new BLL_PLM();
        Common COMMON = new Common();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PLM_Dates_Empty"] != null)
                {
                    if (!string.IsNullOrEmpty(Session["PLM_Dates_Empty"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + Session["PLM_Dates_Empty"].ToString() + "');", true);
                    }
                }

                //if (!string.IsNullOrEmpty(Request.QueryString["RefId"].ToString()))
                //{
                //    txtIssuedDate.Enabled = false;
                //    txtExpirationDate.Enabled = false;
                //}

                LoadDefault();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime parsedIssuedDate;
                DateTime parsedExpirationDate;
                string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                string query1 = string.Empty;
                string query2 = string.Empty;
                int queryStatusCounter = 0;


                if (!DateTime.TryParseExact(txtIssuedDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedIssuedDate))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('INVALID Issued Date');", true);
                }
                else if (!DateTime.TryParseExact(txtExpirationDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedExpirationDate))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('INVALID Expiration Date');", true);
                }
                else
                { //START OF ELSE

                    string filename1 = Path.GetFileName(fu1.FileName);
                    string fileExtensionApplication1 = Path.GetExtension(filename1);
                    string newName1 = "1-" + ddPermitName.SelectedItem.Text.Trim() + "-" + txtExpirationDate.Text.Replace("/", "").Trim() + fileExtensionApplication1;

                    string filename2 = Path.GetFileName(fu2.FileName);
                    string fileExtensionApplication2 = Path.GetExtension(filename2);
                    string newName2 = "2-" + ddPermitName.SelectedItem.Text.Trim() + "-" + txtExpirationDate.Text.Replace("/", "").Trim() + fileExtensionApplication2;

                    string filename3 = Path.GetFileName(fu3.FileName);
                    string fileExtensionApplication3 = Path.GetExtension(filename3);
                    string newName3 = "3-" + ddPermitName.SelectedItem.Text.Trim() + "-" + txtExpirationDate.Text.Replace("/", "").Trim() + fileExtensionApplication3;

                    string success = string.Empty;
                    Entities_PLM_PermitCertificates entity = new Entities_PLM_PermitCertificates();                    
                    entity.PermitName = ddPermitName.SelectedItem.Text;
                    entity.PermitId = ddPermitName.SelectedValue;
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
                    entity.Supplier = "0"; 
                    entity.Status = "0";
                    entity.AddedBy = Session["LcRefId"].ToString();
                    entity.Attachment1 = "";

                    entity.Attachment1 = fu1.HasFile ? newName1 : lbAttachment1.Text;
                    entity.Attachment2 = fu2.HasFile ? newName2 : lbAttachment2.Text;
                    entity.Attachment3 = fu3.HasFile ? newName3 : lbAttachment3.Text;

                    // INSERT RECORD
                    if (String.IsNullOrEmpty(Request.QueryString["RefId"].ToString()))
                    {

                        query1 = "INSERT INTO PLM_TRANSACTION_Request (PermitName,PermitId, ChemicalContent,ItemName,Specification,GovernmentAgency,Frequency,SpecifiedRequirements,LeadTime,[Safety],ProcessorName,Supplier,[status],IssuedDate,ExpirationDate,AddedBy,AddedDate,Attachment1,Attachment2,Attachment3) " +
                                 "VALUES ('" + entity.PermitName + "','" + entity.PermitId + "','" + entity.ChemicalContent + "','" + entity.ItemName + "','" + entity.Specification + "','" + entity.GovernmentAgency + "','" + entity.Frequency + "','" + entity.SpecifiedRequirements + "','" + entity.LeadTime + "','" + entity.Safety + "','" + entity.ProcessorName + "','0','" + entity.Status + "','" + entity.IssuedDate + "','" + entity.ExpirationDate + "','" + entity.AddedBy + "',GETDATE(),'" + entity.Attachment1 + "','" + entity.Attachment2 + "','" + entity.Attachment3 + "')";
                        queryStatusCounter++;

                        if (gvData.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvData.Rows.Count; i++)
                            {
                                Label lblRefId = (Label)gvData.Rows[i].Cells[0].FindControl("lblRefId");
                                Label lblSupplier = (Label)gvData.Rows[i].Cells[0].FindControl("lblSupplier");
                                ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[2].FindControl("ibApproved");

                                if (ibApproved.ImageUrl == "~/images/A2.png")
                                {
                                    query2 += "INSERT INTO PLM_TRANSACTION_NotificationReceiver(PermitRefId,SupplierName,AddedBy,AddedDate,LatestIssuedDate,LatestExpirationDate,SupplierName2) " +
                                              "VALUES ('" + entity.PermitId + "','" + lblRefId.Text.Trim() + "','" + entity.AddedBy + "',GETDATE(), '" + entity.IssuedDate + "','" + entity.ExpirationDate + "','" + lblSupplier.Text + "') ";
                                    queryStatusCounter++;
                                }
                            }
                        }

                        success = BLL.PLM_TRANSACTION_SQLTransaction(queryBeginPart + query1 + query2 + queryEndPart).ToString();
                    }
                    else
                    {
                        // UPDATE RECORD
                        string refId = CryptorEngine.Decrypt(Request.QueryString["RefId"].ToString().Replace(" ", "+"), true);
                        entity.RefId = refId;
                        entity.UpdatedBy = Session["LcRefId"].ToString();

                        if (!String.IsNullOrEmpty(Request.QueryString["renew"].ToString()))
                        {
                            if (Request.QueryString["renew"].ToString().Trim() == "true".Trim())
                            {
                                query1 += "INSERT INTO PLM_TRANSACTION_Request_History (RequestId, " +
                                                         "PermitName, " +
                                                         "ChemicalContent, " +
                                                         "ItemName, " +
                                                         "Specification, " +
                                                         "GovernmentAgency, " +
                                                         "Frequency, " +
                                                         "SpecifiedRequirements, " +
                                                         "LeadTime, " +
                                                         "[Safety], " +
                                                         "ProcessorName, " +
                                                         "Supplier, " +
                                                         "[Status], " +
                                                         "IssuedDate, " +
                                                         "ExpirationDate, " +
                                                         "AddedBy, " +
                                                         "AddedDate, " +
                                                         "Attachment1, " +
                                                         "Attachment2, " +
                                                         "Attachment3) " +
                               "SELECT RefId, PermitName, ChemicalContent, ItemName, Specification, GovernmentAgency, Frequency, SpecifiedRequirements, LeadTime, " +
                               "[Safety], ProcessorName, Supplier, [Status], IssuedDate, ExpirationDate,  " +
                               "CASE WHEN UpdatedBy IS NULL THEN AddedBy ELSE UpdatedBy END, " +
                               "CASE WHEN UpdatedDate IS NULL THEN AddedDate ELSE UpdatedDate END, " +
                               "Attachment1, Attachment2, Attachment3 " +
                               "FROM PLM_TRANSACTION_Request " +
                               "WHERE RefId = '" + entity.RefId + "' ";

                                queryStatusCounter++;
                            }

                        }

                       query1+= "UPDATE PLM_TRANSACTION_Request SET PermitName = '" + entity.PermitName + "'," +
                                         "ChemicalContent = '" + entity.ChemicalContent + "'," +
                                         "ItemName = '" + entity.ItemName + "'," +
                                         "Specification = '" + entity.Specification + "'," +
                                         "GovernmentAgency = '" + entity.GovernmentAgency + "'," +
                                         "Frequency = '" + entity.Frequency + "'," +
                                         "SpecifiedRequirements = '" + entity.SpecifiedRequirements + "'," +
                                         "LeadTime = '" + entity.LeadTime + "'," +
                                         "[Safety] = '" + entity.Safety + "'," +
                                         "ProcessorName = '" + entity.ProcessorName + "'," +
                                         "Supplier = '" + entity.Supplier + "'," +
                                         "[Status] = '" + entity.Status + "'," +
                                         "IssuedDate = '" + entity.IssuedDate + "'," +
                                         "ExpirationDate = '" + entity.ExpirationDate + "'," +
                                         "updatedby = '" + entity.UpdatedBy + "'," +
										 "updateddate = GETDATE(), " +
                                         "Attachment1 = '" + entity.Attachment1 + "'," +
                                         "Attachment2 = '" + entity.Attachment2 + "'," +
                                         "Attachment3 = '" + entity.Attachment3 + "'" +
	                    "WHERE refid = '" + entity.RefId + "'";

                        queryStatusCounter++;

                        if (gvData.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvData.Rows.Count; i++)
                            {
                                Label lblRefId = (Label)gvData.Rows[i].Cells[0].FindControl("lblRefId");
                                Label lblSupplier = (Label)gvData.Rows[i].Cells[0].FindControl("lblSupplier");
                                ImageButton ibApproved = (ImageButton)gvData.Rows[i].Cells[2].FindControl("ibApproved");

                                if (ibApproved.ImageUrl == "~/images/A2.png")
                                {
                                    query2 += "INSERT INTO PLM_TRANSACTION_NotificationReceiver(PermitRefId,SupplierName,AddedBy,AddedDate,LatestIssuedDate,LatestExpirationDate,SupplierName2) " +
                                              "VALUES ('" + entity.PermitId + "','" + lblRefId.Text.Trim() + "','" + entity.AddedBy + "',GETDATE(),'" + entity.IssuedDate + "','" + entity.ExpirationDate + "','" + lblSupplier.Text + "')";
                                    queryStatusCounter++;
                                }
                            }
                        }

                        
                        success = BLL.PLM_TRANSACTION_SQLTransaction(queryBeginPart + query1 + query2 + queryEndPart).ToString();

                    }

                    //success = BLL.PLM_TRANSACTION_Request_Insert(entity).ToString();

                    // SAVE ATTACHMENT IF THERE IS ANY
                    if (fu1.HasFile) // ATTACHMENT 1
                    {
                        fu1.SaveAs(Path.Combine(Server.MapPath("~/PLM_Request/"), filename1));
                        fu1.Dispose();
                        File.Copy(Path.Combine(Server.MapPath("~/PLM_Request/"), filename1), Path.Combine(Server.MapPath("~/PLM_Request/"), (newName1)), true);
                        File.Delete(Path.Combine(Server.MapPath("~/PLM_Request/"), filename1));
                    }

                    if (fu2.HasFile) // ATTACHMENT 2
                    {
                        fu2.SaveAs(Path.Combine(Server.MapPath("~/PLM_Request/"), filename2));
                        fu2.Dispose();
                        File.Copy(Path.Combine(Server.MapPath("~/PLM_Request/"), filename2), Path.Combine(Server.MapPath("~/PLM_Request/"), (newName2)), true);
                        File.Delete(Path.Combine(Server.MapPath("~/PLM_Request/"), filename2));
                    }

                    if (fu3.HasFile) // ATTACHMENT 3
                    {
                        fu3.SaveAs(Path.Combine(Server.MapPath("~/PLM_Request/"), filename3));
                        fu3.Dispose();
                        File.Copy(Path.Combine(Server.MapPath("~/PLM_Request/"), filename3), Path.Combine(Server.MapPath("~/PLM_Request/"), (newName3)), true);
                        File.Delete(Path.Combine(Server.MapPath("~/PLM_Request/"), filename3));
                    }

                    if (!string.IsNullOrEmpty(success))
                    {
                        if (success == queryStatusCounter.ToString())
                        {
                            Session["successMessage"] = "CERTIFICATE NAME : <b>" + ddPermitName.SelectedItem.Text.ToUpper() + "</b> HAS BEEN SUCCESSFULLY SAVED.";
                            Session["successTransactionName"] = "PLM_Permit";
                            Session["successReturnPage"] = "PLM_Permit.aspx";

                            Response.Redirect("SuccessPage.aspx", false);
                        }
                    }



                } //END OF ELSE

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


        protected void ddPermitName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtIssuedDate.Text) || string.IsNullOrEmpty(txtExpirationDate.Text))
                {
                    Session["PLM_Dates_Empty"] = "Please make sure that Issued Date and Expiration Date is selected first.";
                    Response.Redirect("PLM_PermitEntry.aspx?RefId=", false);                   
                }
                else
                {
                    // Populate record
                    List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();
                    list = BLL.PLM_MT_PermitCertificates_GetByRefId(ddPermitName.SelectedValue);

                    if (list != null)
                    {
                        if (list.Count > 0)
                        {
                            foreach (Entities_PLM_PermitCertificates entity in list)
                            {
                                txtChemicalContent.Text = entity.ChemicalContent;
                                txtItemName.Text = entity.ItemName;
                                txtSpecification.Text = entity.Specification;
                                txtSpecifiedRequirements.Text = entity.SpecifiedRequirements;
                                //ddGovernmentAgencies.Items.FindByValue(entity.GovernmentAgency).Selected = true;
                                txtFrequency.Text = entity.Frequency;
                                txtLeadTime.Text = entity.LeadTime;
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

        private void LoadDefault()
        {
            try
            {

                //---------------------------------------------------------------------------------------------------

                List<Entities_PLM_PermitCertificates> listDropDown = new List<Entities_PLM_PermitCertificates>();
                listDropDown = BLL.PLM_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                if (listDropDown != null)
                {
                    if (listDropDown.Count > 0)
                    {
                        ddPermitName.Items.Clear();
                        ddGovernmentAgencies.Items.Clear();

                        ddPermitName.Items.Add("");
                        ddGovernmentAgencies.Items.Add("");
                        //ddSupplier.Items.Add("");

                        foreach (Entities_PLM_PermitCertificates entity in listDropDown)
                        {
                            ListItem item = new ListItem();
                            item.Text = entity.DropdownName.ToUpper();
                            item.Value = entity.DropdownRefId;

                            if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                            {
                                //if (entity.TableName == "PLM_MT_Supplier")
                                //{
                                //    ddSupplier.Items.Add(item);
                                //}
                                if (entity.TableName == "PLM_MT_PermitCertificates")
                                {
                                    ddPermitName.Items.Add(item);
                                }
                                if (entity.TableName == "PLM_MT_GovernmentAgencies")
                                {
                                    ddGovernmentAgencies.Items.Add(item);
                                }
                            }

                        }                        

                    }
                }

                //---------------------------------------------------------------------------------------------------                

                if (!String.IsNullOrEmpty(Request.QueryString["RefId"].ToString()))
                {
                    string refId = CryptorEngine.Decrypt(Request.QueryString["RefId"].ToString().Replace(" ", "+"), true);

                    //Load DefaultForUpdate
                    LoadDefaultForUpdate(refId, Request.QueryString["status"].ToString());
                }
                else
                {
                    List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();
                    list = BLL.PLM_MT_Supplier_GetAll();

                    if (list.Count > 0)
                    {
                        gvData.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                        gvData.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        private void LoadDefaultForUpdate(string refid, string status)
        {
            try
            {
                List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();
                list = BLL.PLM_TRANSACTION_Request_GetByRefid(refid);
                string permitId = string.Empty;

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        foreach (Entities_PLM_PermitCertificates entity in list)
                        {
                            ddPermitName.Items.FindByValue(entity.PermitId).Selected = true;
                            txtChemicalContent.Text = entity.ChemicalContent;
                            txtItemName.Text = entity.ItemName;
                            txtSpecification.Text = entity.Specification;
                            txtSpecifiedRequirements.Text = entity.SpecifiedRequirements;
                            txtFrequency.Text = entity.Frequency;
                            txtLeadTime.Text = entity.LeadTime;
                            ddGovernmentAgencies.Items.FindByValue(entity.GovernmentAgency).Selected = true;
                            txtProcessorName.Text = entity.ProcessorName;
                            txtSafety.Text = entity.Safety;
                            txtIssuedDate.Text = entity.IssuedDate;
                            txtExpirationDate.Text = entity.ExpirationDate;
                            lbAttachment1.Text = entity.Attachment1;
                            lbAttachment2.Text = entity.Attachment2;
                            lbAttachment3.Text = entity.Attachment3;
                            permitId = entity.PermitId;
                        }
                    }
                }

                List<Entities_PLM_PermitCertificates> listSuppliers = new List<Entities_PLM_PermitCertificates>();

                // TAKE ACTION
                if (status == "takeaction")
                {
                    listSuppliers = BLL.PLM_TRANSACTION_NotificationReceiver_GetByRefId_AndSupplierCode(permitId, "").Where(itm => itm.TakeAction == "1").ToList();
                }

                //EXPIRED
                if (status == "expired")
                {
                    listSuppliers = BLL.PLM_TRANSACTION_NotificationReceiver_GetByRefId_AndSupplierCode(permitId, "").Where(itm => itm.Expired == "1").ToList();
                }

                // RENEWED OR NEW
                if (status == "renewed" || status == "new")
                {
                    listSuppliers = BLL.PLM_TRANSACTION_NotificationReceiver_GetByRefId_AndSupplierCode(permitId, "").Where(itm => itm.Renewed == "1").ToList();
                }

                if (listSuppliers.Count > 0)
                {
                    gvData.DataSource = listSuppliers;
                    gvData.DataBind();
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
                    Label lblSelected = (Label)e.Row.FindControl("lblSelected");
                    ImageButton ibApproved = (ImageButton)e.Row.FindControl("ibApproved");

                    if (lblSelected.Text.ToLower() == "selected")
                    {
                        ibApproved.ImageUrl = "~/images/A2.png";
                    }
                    //ImageButton ibApproved = (ImageButton)e.Row.FindControl("ibApproved");

                    //List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();
                    //list = BLL.PLM_TRANSACTION_NotificationReceiver_GetByRefId_AndSupplierCode(ddPermitName.SelectedValue.ToString(), lblRefId.Text.Trim());

                    //if (list != null)
                    //{
                    //    if (list.Count > 0)
                    //    {
                    //        ibApproved.ImageUrl = "~/images/A2.png";
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

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
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
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }

        }

        protected void lbAttachment1_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (lbAttachment1.Text.Length > 0)
                {
                    string strURL = "~/PLM_Request/" + lbAttachment1.Text;
                    Response.Redirect(strURL, false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void lbAttachment2_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (lbAttachment2.Text.Length > 0)
                {
                    string strURL = "~/PLM_Request/" + lbAttachment2.Text;
                    Response.Redirect(strURL, false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void lbAttachment3_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (lbAttachment3.Text.Length > 0)
                {
                    string strURL = "~/PLM_Request/" + lbAttachment3.Text;
                    Response.Redirect(strURL, false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


    }
}
