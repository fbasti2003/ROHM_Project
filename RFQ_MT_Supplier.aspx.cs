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

namespace REPI_PUR_SOFRA
{
    public partial class RFQ_MT_Supplier : System.Web.UI.Page
    {

        BLL_RFQ BLL = new BLL_RFQ();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["SupplierAccess"].ToString().Trim()))
                {
                    LoadDefault();
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }

            }
        }

        private void LoadDefault()
        {
            try
            {
                List<Entities_RFQ_Supplier> list = new List<Entities_RFQ_Supplier>();
                list = BLL.RFQ_MT_Supplier_GetAll();

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.IsDisabled.Contains("1")).ToList();
                    gvDisabled.DataBind();

                    gvData.FooterRow.Visible = gvData.PageIndex == 0;

                    LinkButton lbSaveNew = (LinkButton)gvData.FooterRow.FindControl("lbSaveNew");

                    lbSaveNew.Style.Add("display", "none");

                    //List<Entities_RFQ_Category> allCategory = new List<Entities_RFQ_Category>();
                    //allCategory = BLL.RFQ_MT_Category_GetAll();

                    //if (allCategory.Count > 0)
                    //{
                    //    foreach (Entities_RFQ_Category entity in allCategory)
                    //    {
                    //        ListItem item = new ListItem();
                    //        item.Text = entity.Description;
                    //        item.Value = entity.RefId;
                    //        if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                    //        {
                    //            lbNewCategory.Items.Add(item);
                    //        }
                    //    }
                    //}

                    

                }
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvDisabled_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblId = (Label)e.Row.FindControl("lblId");
                    Label lblName = (Label)e.Row.FindControl("lblName");
                    Label lblAddress = (Label)e.Row.FindControl("lblAddress");
                    Label lblEmail = (Label)e.Row.FindControl("lblEmail");
                    DropDownList ddCategoryDisabled = (DropDownList)e.Row.FindControl("ddCategoryDisabled");

                    lblName.Text = lblName.Text.TrimStart().TrimEnd();
                    lblAddress.Text = lblAddress.Text.TrimStart().TrimEnd();
                    lblEmail.Text = lblEmail.Text.TrimStart().TrimEnd();

                    if (lblName.Text.Length > 20)
                    {
                        lblName.Text = lblName.Text.Substring(0, 20) + "...";
                    }

                    if (lblAddress.Text.Length > 38)
                    {
                        lblAddress.Text = lblAddress.Text.Substring(0, 38) + "...";
                    }

                    if (lblEmail.Text.Length > 30)
                    {
                        lblEmail.Text = lblEmail.Text.Substring(0, 30) + "...";
                    }


                    ddCategoryDisabled.Items.Clear();

                    List<Entities_RFQ_Category> listCategory = new List<Entities_RFQ_Category>();
                    listCategory = BLL.RFQ_MT_Supplier_GetCategoryByHeadId(lblId.Text.Trim());

                    if (listCategory.Count > 0)
                    {
                        foreach (Entities_RFQ_Category entity in listCategory)
                        {
                            ListItem item = new ListItem();
                            item.Text = entity.Description;
                            item.Value = entity.RefId;
                            if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                            {
                                ddCategoryDisabled.Items.Add(item);
                            }

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

        protected void gvDisabled_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvDisabled.PageIndex = e.NewPageIndex;
                LoadDefault();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvDisabled_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                Label lblId = row.FindControl("lblId") as Label;

                if (e.CommandName == "StartEnabling_Command")
                {
                    try
                    {
                        Entities_RFQ_Supplier entity = new Entities_RFQ_Supplier();
                        entity.RefId = lblId.Text.Trim();
                        entity.IsDisabled = "0";

                        BLL.RFQ_MT_Supplier_IsDisabled(entity);

                        LoadDefault();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Record has been enabled!');", true);
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
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
                    Label lblId = (Label)e.Row.FindControl("lblId");
                    Label lblName = (Label)e.Row.FindControl("lblName");
                    Label lblAddress = (Label)e.Row.FindControl("lblAddress");
                    Label lblEmail = (Label)e.Row.FindControl("lblEmail");
                    DropDownList ddCategory = (DropDownList)e.Row.FindControl("ddCategory");


                    LinkButton lbSave = (LinkButton)e.Row.FindControl("lbSave");
                    lbSave.Style.Add("display", "none");

                    lblName.Text = lblName.Text.TrimStart().TrimEnd();
                    lblAddress.Text = lblAddress.Text.TrimStart().TrimEnd();
                    lblEmail.Text = lblEmail.Text.TrimStart().TrimEnd();

                    if (lblName.Text.Length > 20)
                    {
                        lblName.Text = lblName.Text.Substring(0, 20) + "...";
                    }

                    if (lblAddress.Text.Length > 38)
                    {
                        lblAddress.Text = lblAddress.Text.Substring(0, 38) + "...";
                    }

                    if (lblEmail.Text.Length > 30)
                    {
                        lblEmail.Text = lblEmail.Text.Substring(0, 30) + "...";
                    }

                    ddCategory.Items.Clear();

                    List<Entities_RFQ_Category> listCategory = new List<Entities_RFQ_Category>();
                    listCategory = BLL.RFQ_MT_Supplier_GetCategoryByHeadId(lblId.Text.Trim());

                    if (listCategory.Count > 0)
                    {
                        foreach (Entities_RFQ_Category entity in listCategory)
                        {
                            ListItem item = new ListItem();
                            item.Text = entity.Description;
                            item.Value = entity.RefId;
                            if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                            {
                                ddCategory.Items.Add(item);
                            }

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

        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvData.PageIndex = e.NewPageIndex;
                LoadDefault();
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

                CheckBoxList ddCategory = row.FindControl("ddCategory") as CheckBoxList;
                Label lblId = row.FindControl("lblId") as Label;
                Label lblName = row.FindControl("lblName") as Label;
                Label lblAddress = row.FindControl("lblAddress") as Label;
                Label lblEmail = row.FindControl("lblEmail") as Label;

                LinkButton lbEdit = row.FindControl("lbEdit") as LinkButton;
                LinkButton lbSave = row.FindControl("lbSave") as LinkButton;

                LinkButton lbAddNew = gvData.FooterRow.FindControl("lbAddNew") as LinkButton;
                LinkButton lbSaveNew = gvData.FooterRow.FindControl("lbSaveNew") as LinkButton;
                LinkButton lbDisabled = row.FindControl("lbDisabled") as LinkButton;

                if (e.CommandName == "StartEditing_Command")
                {
                    if (lbEdit.Text == "CANCEL")
                    {
                        //lbSave.Style.Add("display", "none");
                        lbEdit.Text = "EDIT";                        
                    }
                    else
                    {
                        //lbEdit.Text = "CANCEL";
                        Session["RFQ_MT_SUPPLIER_ID"] = lblId.Text.Trim();
                        Session["RFQ_MT_SUPPLIER_NAME"] = lblName.Text;
                        Session["RFQ_MT_SUPPLIER_ADDRESS"] = lblAddress.Text;
                        Session["RFQ_MT_SUPPLIER_EMAIL"] = lblEmail.Text;
                        Session["RFQ_MT_SUPPLIER_DISABLED"] = "no";
                        //Response.Redirect("RFQ_MT_Supplier_Update.aspx");

                        string URL = "~/RFQ_MT_Supplier_Update.aspx";
                        URL = Page.ResolveClientUrl(URL);
                        lbEdit.OnClientClick = "window.open('" + URL + "'); return false;";

                        //lbSave.Style.Add("display", "block");
                        //lbDisabled.Style.Add("display", "none");
                    }
                }

                if (e.CommandName == "StartUpdating_Command")
                {
                    //try
                    //{
                    //    Entities_RFQ_Supplier entity = new Entities_RFQ_Supplier();
                    //    entity.RefId = lblId.Text.Trim();
                    //    entity.Name = txtName.Text.Replace("'", "''").ToUpper();
                    //    entity.Address = txtAddress.Text.Replace("'", "''").ToUpper();
                    //    entity.EmailAddress = txtEmail.Text.Replace("'", "''").ToUpper();
                    //    entity.UpdatedBy = Session["LcRefId"].ToString();

                    //    try
                    //    {

                    //        if (entity.Name.Length <= 0)
                    //        {
                    //            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Name must not be blank');", true);
                    //        }
                    //        else if (entity.EmailAddress.Length <= 0)
                    //        {
                    //            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Email must not be blank');", true);
                    //        }

                    //        else
                    //        {
                    //            BLL.RFQ_MT_Supplier_Append(entity);

                    //            lblName.Text = txtName.Text;
                    //            txtName.Style.Add("display", "none");
                    //            lblAddress.Text = txtAddress.Text;
                    //            txtAddress.Style.Add("display", "none");
                    //            lblEmail.Text = txtEmail.Text;
                    //            txtEmail.Style.Add("display", "none");

                    //            lbSave.Style.Add("display", "none");
                    //            lblName.Style.Add("display", "block");
                    //            lblAddress.Style.Add("display", "block");
                    //            lblEmail.Style.Add("display", "block");

                    //            lbSave.Style.Add("display", "none");
                    //            lbEdit.Style.Add("display", "block");
                    //            lbEdit.Text = "EDIT";
                    //            lbDisabled.Style.Add("display", "block");

                    //            LoadDefault();
                    //            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Record has been successfully updated!');", true);
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                    //    }

                    //}
                    //catch (Exception ex)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                    //}

                }

                if (e.CommandName == "AddNew_Command")
                {
                    //if (lbAddNew.Text == "ADD NEW")
                    //{

                    //    lbAddNew.Text = "CANCEL";
                    //    lbSaveNew.Style.Add("display", "block");
                    //}
                    //else
                    //{

                    //    lbAddNew.Text = "ADD NEW";
                    //    lbSaveNew.Style.Add("display", "none");
                    //}
                    Session["RFQ_MT_SUPPLIER_ID"] = null;
                    Session["RFQ_MT_SUPPLIER_NAME"] = null;
                    Session["RFQ_MT_SUPPLIER_ADDRESS"] = null;
                    Session["RFQ_MT_SUPPLIER_EMAIL"] = null;
                    Session["RFQ_MT_SUPPLIER_DISABLED"] = "no";
                    Response.Redirect("RFQ_MT_Supplier_Update.aspx");
                }

                if (e.CommandName == "SaveNew_Command")
                {

                    //try
                    //{
                    //    Entities_RFQ_Supplier entity = new Entities_RFQ_Supplier();
                    //    entity.Name = txtNewName.Text.Replace("'", "''").ToUpper();
                    //    entity.Address = txtNewAddress.Text.Replace("'", "''").ToUpper();
                    //    entity.EmailAddress = txtNewEmail.Text.Replace("'", "''").ToUpper();
                    //    entity.AddedBy = Session["LcRefId"].ToString();

                    //    try
                    //    {
                    //        if (entity.Name.Length <= 0)
                    //        {
                    //            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Name must not be blank');", true);
                    //        }
                    //        else if (entity.EmailAddress.Length <= 0)
                    //        {
                    //            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Email must not be blank');", true);
                    //        }
                    //        else if (BLL.RFQ_MT_Supplier_GetByDescription(entity.Name).Count > 0)
                    //        {
                    //            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + entity.Name + " is already exist.');", true);
                    //        }
                    //        else
                    //        {
                    //            BLL.RFQ_MT_Supplier_Insert(entity);

                    //            txtNewName.Enabled = false;
                    //            txtNewAddress.Enabled = false;
                    //            txtNewEmail.Enabled = false;

                    //            lbSaveNew.Style.Add("display", "none");
                    //            lbAddNew.Text = "ADD NEW";

                    //            LoadDefault();
                    //            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Record has been successfully added!');", true);
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                    //    }

                    //}
                    //catch (Exception ex)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                    //}


                }

                if (e.CommandName == "StartDisabling_Command")
                {
                    try
                    {
                        //Entities_RFQ_Supplier entity = new Entities_RFQ_Supplier();
                        //entity.RefId = lblId.Text.Trim();
                        //entity.IsDisabled = "1";

                        //BLL.RFQ_MT_Supplier_IsDisabled(entity);

                        //LoadDefault();

                        Session["RFQ_MT_SUPPLIER_ID"] = lblId.Text.Trim();
                        Session["RFQ_MT_SUPPLIER_NAME"] = lblName.Text;
                        Session["RFQ_MT_SUPPLIER_ADDRESS"] = lblAddress.Text;
                        Session["RFQ_MT_SUPPLIER_EMAIL"] = lblEmail.Text;
                        Session["RFQ_MT_SUPPLIER_DISABLED"] = "yes";
                        //Response.Redirect("RFQ_MT_Supplier_Update.aspx");


                        string URL = "~/RFQ_MT_Supplier_Update.aspx";
                        URL = Page.ResolveClientUrl(URL);
                        lbEdit.OnClientClick = "window.open('" + URL + "'); return false;";

                        //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Record has been disabled!');", true);

                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities_RFQ_Supplier> list = new List<Entities_RFQ_Supplier>();
                list = BLL.RFQ_MT_Supplier_GetByDescription_Like(txtSearch.Text);

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.IsDisabled.Contains("1")).ToList();
                    gvDisabled.DataBind();

                    gvData.FooterRow.Visible = gvData.PageIndex == 0;

                    LinkButton lbSaveNew = (LinkButton)gvData.FooterRow.FindControl("lbSaveNew");

                    lbSaveNew.Style.Add("display", "none");

                }
                else
                {
                    LoadDefault();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Cannot find " + txtSearch.Text + "');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


        protected void btnDownloadReport_Click(object sender, EventArgs e)
        {
            try
            {

                if (!System.IO.File.Exists(Server.MapPath("~/RFQ_XLS/" + Session["LcRefId"].ToString() + "_MTSupplierList" + ".xlsx")))
                {
                    System.IO.File.Copy(Server.MapPath("~/RFQ_XLS/RFQ_MT_SupplierList_Report.xlsx"), Server.MapPath("~/RFQ_XLS/" + Session["LcRefId"].ToString() + "_MTSupplierList" + ".xlsx"));
                }
                else
                {
                    System.IO.File.Delete(Server.MapPath("~/RFQ_XLS/" + Session["LcRefId"].ToString() + "_MTSupplierList" + ".xlsx"));
                    System.IO.File.Copy(Server.MapPath("~/RFQ_XLS/RFQ_MT_SupplierList_Report.xlsx"), Server.MapPath("~/RFQ_XLS/" + Session["LcRefId"].ToString() + "_MTSupplierList" + ".xlsx"));
                }
                
                string path = Server.MapPath("~/RFQ_XLS/" + Session["LcRefId"].ToString() + "_MTSupplierList" + ".xlsx");
                Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + path);
                FileStream fsBI = new FileStream(path, FileMode.Open);
                using (SLDocument draft = new SLDocument(fsBI, "Sheet1"))
                {

                    List<Entities_RFQ_Supplier> list = new List<Entities_RFQ_Supplier>();
                    list = BLL.RFQ_MT_Supplier_GetAll().Where(item => item.IsDisabled.Contains("0")).ToList();                    

                    if (list.Count > 0)
                    {
                        int cnt = 4;

                        foreach (Entities_RFQ_Supplier entity in list)
                        {

                            draft.SetCellValue("A" + cnt.ToString(), entity.Name.ToUpper());
                            draft.SetCellValue("B" + cnt.ToString(), entity.EmailAddress.ToUpper());
                            draft.SetCellValue("C" + cnt.ToString(), entity.Address.ToUpper());
                            draft.SetCellValue("D" + cnt.ToString(), entity.Registered == "1" ? "REGISTERED" : "NOT REGISTERED");
                            draft.SetCellValue("E" + cnt.ToString(), entity.Peza == "1" ? "PEZA" : "NON-PEZA");

                            cnt++;
                        }
                    }


                    fsBI.Close();
                    draft.SaveAs(path);

                }

                Response.Redirect("RFQ_XLS/" + Session["LcRefId"].ToString() + "_MTSupplierList.xlsx", false);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }

        }










    }
}
