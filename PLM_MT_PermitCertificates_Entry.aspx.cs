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
    public partial class PLM_MT_PermitCertificates_Entry : System.Web.UI.Page
    {
        BLL_PLM BLL = new BLL_PLM();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LcRefId"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["RefId"].ToString()))
                    {
                        string refId = CryptorEngine.Decrypt(Request.QueryString["RefId"].ToString().Replace(" ", "+"), true);
                        LoadDefaultForUpdate(refId);
                    }
                    else
                    {
                        //---------------------------------------------------------------------------------------------------
                        List<Entities_PLM_PermitCertificates> listDropDown = new List<Entities_PLM_PermitCertificates>();
                        listDropDown = BLL.PLM_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                        if (listDropDown != null)
                        {
                            if (listDropDown.Count > 0)
                            {
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

                            }
                        }
                        //---------------------------------------------------------------------------------------------------

                    }
                    
                }
            }
        }

        private void LoadDefaultForUpdate(string refid)
        {
            List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();
            list = BLL.PLM_MT_PermitCertificates_GetByRefId(refid);

            if (list != null)
            {
                if (list.Count > 0)
                {
                    foreach (Entities_PLM_PermitCertificates entity in list)
                    {
                        txtPermitName.Text = entity.PermitName;
                        txtChemicalContent.Text = entity.ChemicalContent;
                        txtItemName.Text = entity.ItemName;
                        txtSpecification.Text = entity.Specification;                        
                        txtFrequency.Text = entity.Frequency;
                        txtSpecifiedRequirements.Text = entity.SpecifiedRequirements;
                        txtLeadTime.Text = entity.LeadTime;

                         //---------------------------------------------------------------------------------------------------

                            List<Entities_PLM_PermitCertificates> listDropDown = new List<Entities_PLM_PermitCertificates>();
                            listDropDown = BLL.PLM_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                            if (listDropDown != null)
                            {
                                if (listDropDown.Count > 0)
                                {
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

                                    if (!string.IsNullOrEmpty(entity.GovernmentAgency))
                                    {
                                        ddGovernmentAgencies.Items.FindByValue(entity.GovernmentAgency).Selected = true;
                                    }

                                }
                            }
                        //---------------------------------------------------------------------------------------------------
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string success = string.Empty;
                Entities_PLM_PermitCertificates entity = new Entities_PLM_PermitCertificates();
                entity.PermitName = txtPermitName.Text.Replace("'", "''");
                entity.ChemicalContent = txtChemicalContent.Text.Replace("'", "''");
                entity.ItemName = txtItemName.Text.Replace("'", "''");
                entity.Specification = txtSpecification.Text.Replace("'", "''");
                entity.GovernmentAgency = ddGovernmentAgencies.SelectedValue;
                entity.Frequency = txtFrequency.Text.Replace("'", "''");
                entity.SpecifiedRequirements = txtSpecifiedRequirements.Text.Replace("'", "''");
                entity.LeadTime = txtLeadTime.Text.Replace("'", "''");
                entity.AddedBy = Session["LcRefId"].ToString();
                entity.UpdatedBy = Session["LcRefId"].ToString();                

                if (!COMMON.isNumeric(txtLeadTime.Text.Trim(), System.Globalization.NumberStyles.AllowThousands) || String.IsNullOrEmpty(txtLeadTime.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Lead time must be numeric.');", true);
                }
                else
                {

                    if (!String.IsNullOrEmpty(Request.QueryString["RefId"].ToString()))
                    {
                        //UPDATE RECORD
                        entity.RefId = CryptorEngine.Decrypt(Request.QueryString["RefId"].ToString().Replace(" ", "+"), true);
                        success = BLL.PLM_MT_PermitCertificates_Append(entity).ToString(); 
                    }
                    else
                    {
                        //INSERT NEW RECORD
                        success = BLL.PLM_MT_PermitCertificates_Insert(entity).ToString();                        
                    }

                    if (!string.IsNullOrEmpty(success))
                    {
                        if (success == "-1")
                        {
                            Session["successMessage"] = "CERTIFICATE NAME : <b>" + txtPermitName.Text.ToUpper() + "</b> HAS BEEN SUCCESSFULLY SAVED.";
                            Session["successTransactionName"] = "PLM_MT_PermitCertificateEntry";
                            Session["successReturnPage"] = "PLM_MT_PermitCertificates.aspx";

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





    }
}
