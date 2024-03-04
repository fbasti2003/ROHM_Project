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
    public partial class RFQ_MT_Supplier_Update : System.Web.UI.Page
    {

        BLL_RFQ BLL = new BLL_RFQ();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["RFQ_MT_SUPPLIER_ID"] != null)
                {                    
                    List<Entities_RFQ_Supplier> supplier = new List<Entities_RFQ_Supplier>();
                    supplier = BLL.RFQ_MT_Supplier_GetAll().Where(item => item.RefId == Session["RFQ_MT_SUPPLIER_ID"].ToString()).ToList();

                    if (supplier.Count > 0)
                    {
                        foreach (Entities_RFQ_Supplier entitySup in supplier)
                        {
                            txtName.Text = entitySup.Name.ToUpper();
                            txtAddress.Text = entitySup.Address;
                            txtEmail.Text = entitySup.EmailAddress;
                            txtSupplierEvaluationEmail.Text = entitySup.EvaluationEmail;
                            ddRegistered.Items.FindByValue(entitySup.Registered).Selected = true;
                            ddPeza.Items.FindByValue(entitySup.Peza).Selected = true;
                        }
                    }
                }

                List<Entities_RFQ_Category> allCategory = new List<Entities_RFQ_Category>();
                allCategory = BLL.RFQ_MT_Category_GetAll();

                if (allCategory.Count > 0)
                {

                    foreach (Entities_RFQ_Category entity in allCategory)
                    {                        
                        ListItem item = new ListItem();
                        item.Text = entity.Description;
                        item.Value = entity.RefId;
                        if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                        {
                            cblCategory.Items.Add(item);

                            if (Session["RFQ_MT_SUPPLIER_ID"] != null)
                            {
                                List<Entities_RFQ_Category> listCategory = new List<Entities_RFQ_Category>();
                                listCategory = BLL.RFQ_MT_Supplier_GetCategoryByHeadId(Session["RFQ_MT_SUPPLIER_ID"].ToString().Trim());

                                if (listCategory.Count > 0)
                                {
                                    foreach (Entities_RFQ_Category entityCat in listCategory)
                                    {
                                        if (entityCat.RefId.Trim() == entity.RefId.Trim())
                                        {
                                            cblCategory.Items.FindByValue(item.Value).Selected = true;
                                        }
                                    }
                                }


                            }

                        }

                        
                    }
                }

                if (Session["RFQ_MT_SUPPLIER_DISABLED"].ToString() == "yes")
                {
                    btnDisable.Visible = true;
                    btnSubmit.Visible = false;
                    pTitle.InnerHtml = "ARE YOU SURE YOU WANT TO DISABLE THIS RECORD?";
                    pTitle.Style.Add("color", "red");
                }
                else
                {
                    pTitle.InnerHtml = "ADD/UPDATE SUPPLIER";
                    pTitle.Style.Add("color", "black");
                    btnDisable.Visible = false;
                    btnSubmit.Visible = true;
                }

            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("RFQ_MT_Supplier.aspx", false);
        }

        protected void btnDisable2_Click(object sender, EventArgs e)
        {
            Entities_RFQ_Supplier entity = new Entities_RFQ_Supplier();
            entity.RefId = Session["RFQ_MT_SUPPLIER_ID"].ToString();
            entity.IsDisabled = "1";

            BLL.RFQ_MT_Supplier_IsDisabled(entity);

            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Supplier " + txtName.Text.ToUpper() + " has been successfully disabled.');", true);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool isError = false;
            string successHead = string.Empty;
            string successDetails = string.Empty;

            if (String.IsNullOrEmpty(txtName.Text))
            {
                isError = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('Supplier Name must not be blank.');", true);
            }

            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                isError = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('Supplier Email Address must not be blank.');", true);
            }

            if (String.IsNullOrEmpty(ddRegistered.SelectedItem.Text))
            {
                isError = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('Please select a valid Registered / Not Registered field.');", true);
            }


            if (!isError)
            {
                Entities_RFQ_Supplier supplier = new Entities_RFQ_Supplier();
                supplier.Name = txtName.Text;
                supplier.Address = txtAddress.Text;
                supplier.EmailAddress = txtEmail.Text;
                supplier.AddedBy = Session["LcRefId"].ToString();
                supplier.UpdatedBy = Session["LcRefId"].ToString();
                supplier.Registered = ddRegistered.SelectedValue;
                supplier.EvaluationEmail = txtSupplierEvaluationEmail.Text;
                supplier.Peza = ddPeza.SelectedValue;

                if (Session["RFQ_MT_SUPPLIER_ID"] != null)
                {
                    //UPDATE
                    supplier.RefId = Session["RFQ_MT_SUPPLIER_ID"].ToString();
                    successHead = BLL.RFQ_MT_Supplier_Append(supplier).ToString();
                    if (successHead == "-1")
                    {
                        isError = false;

                        foreach (ListItem item in cblCategory.Items)
                        {
                            if (item.Selected)
                            {
                                Entities_RFQ_Supplier entityDetails = new Entities_RFQ_Supplier();
                                entityDetails.HeadRefId = Session["RFQ_MT_SUPPLIER_ID"].ToString();
                                entityDetails.CategoryId = item.Value;
                                BLL.RFQ_MT_Supplier_InsertDetails(entityDetails);
                            }
                        }
                    }
                }
                else
                {
                    //INSERT NEW
                    successHead = BLL.RFQ_MT_Supplier_Insert(supplier).ToString();
                    if (successHead == "-1")
                    {
                        isError = false;
                        // Get SupplierHeadId
                        List<Entities_RFQ_Supplier> supplierId = new List<Entities_RFQ_Supplier>();
                        supplierId = BLL.RFQ_MT_Supplier_GetAll().Where(item => item.Name.ToUpper() == txtName.Text.ToUpper()).ToList();

                        if (supplierId.Count > 0)
                        {
                            foreach (Entities_RFQ_Supplier sID in supplierId)
                            {
                                foreach (ListItem item in cblCategory.Items)
                                {
                                    if (item.Selected)
                                    {
                                        Entities_RFQ_Supplier entityDetails = new Entities_RFQ_Supplier();
                                        entityDetails.HeadRefId = sID.RefId;
                                        entityDetails.CategoryId = item.Value;
                                        BLL.RFQ_MT_Supplier_InsertDetails(entityDetails);
                                    }
                                }
                            }
                        }



                    }

                }

                if (!isError)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Supplier " + txtName.Text.ToUpper() + " has been successfully saved.');", true);
                }


            }

        }


    }
}
