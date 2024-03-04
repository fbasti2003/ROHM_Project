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
    public partial class SE_MT_EvaluationCriteriaForMaterial : System.Web.UI.Page
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
            try
            {
                List<Entities_SE_EvaluationCriteria_ForMaterial> list = new List<Entities_SE_EvaluationCriteria_ForMaterial>();
                list = BLL.SE_MT_EvaluationCriteria_ForMaterial_GetAll();

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.Isdisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.Isdisabled.Contains("1")).ToList();
                    gvDisabled.DataBind();

                    gvData.FooterRow.Visible = gvData.PageIndex == 0;

                    LinkButton lbSaveNew = (LinkButton)gvData.FooterRow.FindControl("lbSaveNew");
                    TextBox txtNewEvaluationCriteria = (TextBox)gvData.FooterRow.FindControl("txtNewEvaluationCriteria");
                    TextBox txtNewPoints = (TextBox)gvData.FooterRow.FindControl("txtNewPoints");
                    TextBox txtNewWeight = (TextBox)gvData.FooterRow.FindControl("txtNewWeight");
                    //TextBox txtNewScore = (TextBox)gvData.FooterRow.FindControl("txtNewScore");
                    DropDownList ddItemNew = (DropDownList)gvData.FooterRow.FindControl("ddItemNew");
                    DropDownList ddLevelNew = (DropDownList)gvData.FooterRow.FindControl("ddLevelNew");
                    //DropDownList ddItem = (DropDownList)gvData.FooterRow.FindControl("ddItem");

                    lbSaveNew.Style.Add("display", "none");
                    txtNewEvaluationCriteria.Enabled = false;
                    txtNewPoints.Enabled = false;
                    txtNewWeight.Enabled = false;
                    //txtNewScore.Enabled = false;
                    ddItemNew.Enabled = false;
                    ddLevelNew.Enabled = false;


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
                        Entities_SE_EvaluationCriteria_ForMaterial entity = new Entities_SE_EvaluationCriteria_ForMaterial();
                        entity.RefId = lblId.Text.Trim();
                        entity.Isdisabled = "0";
                        entity.DisabledBy = Session["UserFullName"].ToString() + " - " + DateTime.Now.ToLongDateString();

                        BLL.SE_MT_EvaluationCriteria_ForMaterial_IsDisabled(entity);

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
                    Label lblEvaluationCriteria = (Label)e.Row.FindControl("lblEvaluationCriteria");
                    TextBox txtEvaluationCriteria = (TextBox)e.Row.FindControl("txtEvaluationCriteria");
                    Label lblPoints = (Label)e.Row.FindControl("lblPoints");
                    TextBox txtPoints = (TextBox)e.Row.FindControl("txtPoints");
                    Label lblWeight = (Label)e.Row.FindControl("lblWeight");
                    TextBox txtWeight = (TextBox)e.Row.FindControl("txtWeight");
                    //Label lblScore = (Label)e.Row.FindControl("lblScore");
                    //TextBox txtScore = (TextBox)e.Row.FindControl("txtScore");
                    LinkButton lbSave = (LinkButton)e.Row.FindControl("lbSave");
                    DropDownList ddItem = (DropDownList)e.Row.FindControl("ddItem");
                    DropDownList ddLevel = (DropDownList)e.Row.FindControl("ddLevel");

                    txtEvaluationCriteria.Style.Add("display", "none");
                    txtPoints.Style.Add("display", "none");
                    txtWeight.Style.Add("display", "none");
                    //txtScore.Style.Add("display", "none");
                    ddItem.Style.Add("display", "none");
                    ddLevel.Style.Add("display", "none");
                    lbSave.Style.Add("display", "none");

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

                DropDownList ddItem = row.FindControl("ddItem") as DropDownList;
                DropDownList ddLevel = row.FindControl("ddLevel") as DropDownList;
                TextBox txtEvaluationCriteria = row.FindControl("txtEvaluationCriteria") as TextBox;
                TextBox txtPoints = row.FindControl("txtPoints") as TextBox;
                TextBox txtWeight = row.FindControl("txtWeight") as TextBox;
                //TextBox txtScore = row.FindControl("txtScore") as TextBox;
                Label lblId = row.FindControl("lblId") as Label;
                Label lblEvaluationCriteria = row.FindControl("lblEvaluationCriteria") as Label;
                Label lblPoints = row.FindControl("lblPoints") as Label;
                Label lblWeight = row.FindControl("lblWeight") as Label;
                //Label lblScore = row.FindControl("lblScore") as Label;
                Label lblItem = row.FindControl("lblItem") as Label;
                Label lblLevel = row.FindControl("lblLevel") as Label;

                LinkButton lbEdit = row.FindControl("lbEdit") as LinkButton;
                LinkButton lbSave = row.FindControl("lbSave") as LinkButton;

                TextBox txtNewEvaluationCriteria = row.FindControl("txtNewEvaluationCriteria") as TextBox;
                TextBox txtNewPoints = row.FindControl("txtNewPoints") as TextBox;
                TextBox txtNewWeight = row.FindControl("txtNewWeight") as TextBox;
                //TextBox txtNewScore = row.FindControl("txtNewScore") as TextBox;
                DropDownList ddItemNew = row.FindControl("ddItemNew") as DropDownList;
                DropDownList ddLevelNew = row.FindControl("ddLevelNew") as DropDownList;

                LinkButton lbAddNew = gvData.FooterRow.FindControl("lbAddNew") as LinkButton;
                LinkButton lbSaveNew = gvData.FooterRow.FindControl("lbSaveNew") as LinkButton;
                LinkButton lbDisabled = row.FindControl("lbDisabled") as LinkButton;

                if (e.CommandName == "StartEditing_Command")
                {
                    if (lbEdit.Text == "CANCEL")
                    {
                        txtEvaluationCriteria.Style.Add("display", "none");
                        txtPoints.Style.Add("display", "none");
                        txtWeight.Style.Add("display", "none");
                        //txtScore.Style.Add("display", "none");
                        ddItem.Style.Add("display", "none");
                        ddLevel.Style.Add("display", "none");

                        lblEvaluationCriteria.Style.Add("display", "block");
                        lblPoints.Style.Add("display", "block");
                        lblWeight.Style.Add("display", "block");
                        //lblScore.Style.Add("display", "block");
                        lblItem.Style.Add("display", "block");
                        lblLevel.Style.Add("display", "block");
                        lbDisabled.Style.Add("display", "block");

                        lbSave.Style.Add("display", "none");
                        lbEdit.Text = "EDIT";
                    }
                    else
                    {
                        lblEvaluationCriteria.Style.Add("display", "none");
                        lblPoints.Style.Add("display", "none");
                        lblWeight.Style.Add("display", "none");
                        //lblScore.Style.Add("display", "none");
                        lblItem.Style.Add("display", "none");
                        lblLevel.Style.Add("display", "none");

                        txtEvaluationCriteria.Style.Add("display", "block");
                        txtPoints.Style.Add("display", "block");
                        txtWeight.Style.Add("display", "block");
                        //txtScore.Style.Add("display", "block");
                        ddItem.Style.Add("display", "block");
                        ddLevel.Style.Add("display", "block");

                        lbEdit.Text = "CANCEL";
                        lbSave.Style.Add("display", "block");
                        lbDisabled.Style.Add("display", "none");

                        //POPULATE EVALUATION ITEM
                        List<Entities_SE_EvaluationItem> listItem = new List<Entities_SE_EvaluationItem>();
                        listItem = BLL.SE_MT_EvaluationItem_GetAll();

                        if (listItem != null)
                        {
                            if (listItem.Count > 0)
                            {
                                ddItem.Items.Clear();
                                ddItem.Items.Add("");
                                foreach (Entities_SE_EvaluationItem itm in listItem)
                                {
                                    ListItem item = new ListItem();
                                    item.Text = itm.Item;
                                    item.Value = itm.RefId;

                                    if (itm.Type == "FOR_MATERIAL")
                                    {
                                        ddItem.Items.Add(item);
                                    }
                                }

                            }
                        }
                        // END OF POPULATE EVALUATION ITEM

                        ddItem.Focus();
                    }
                }

                if (e.CommandName == "StartUpdating_Command")
                {
                    try
                    {
                        Entities_SE_EvaluationCriteria_ForMaterial entity = new Entities_SE_EvaluationCriteria_ForMaterial();
                        entity.RefId = lblId.Text.Trim();
                        entity.Item = ddItem.SelectedValue;
                        entity.Criteria = txtEvaluationCriteria.Text.Replace("'", "''");
                        entity.Points = txtPoints.Text;
                        entity.Weight = txtWeight.Text;
                        entity.Score = "0"; // SET TO ZERO BECAUSE I REMOVE THE SCORE FIELD
                        entity.Updatedby = Session["LcRefId"].ToString();
                        entity.Level = ddLevel.SelectedValue;

                        try
                        {
                            if (string.IsNullOrEmpty(ddItem.SelectedItem.Text))
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select a valid item');", true);
                            }
                            else if (string.IsNullOrEmpty(ddLevel.SelectedItem.Text))
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select a valid type');", true);
                            }
                            else if (entity.Criteria.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Criteria field must not be blank');", true);
                            }
                            else if (entity.Points.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Points field must not be blank');", true);
                            }
                            else if (entity.Weight.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Weight field must not be blank');", true);
                            }
                            //else if (entity.Score.Length <= 0)
                            //{
                            //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Score field must not be blank');", true);
                            //}
                            else
                            {
                                BLL.SE_MT_EvaluationCriteria_ForMaterial_Append(entity);

                                lblItem.Text = ddItem.SelectedItem.Text;
                                lblEvaluationCriteria.Text = txtEvaluationCriteria.Text;
                                lblPoints.Text = txtPoints.Text;
                                lblWeight.Text = txtWeight.Text;
                                //lblScore.Text = txtScore.Text;
                                lblLevel.Text = ddLevel.SelectedItem.Text;

                                ddItem.Style.Add("display", "none");
                                ddLevel.Style.Add("display", "none");
                                txtEvaluationCriteria.Style.Add("display", "none");
                                txtPoints.Style.Add("display", "none");
                                txtWeight.Style.Add("display", "none");
                                //txtScore.Style.Add("display", "none");

                                lblItem.Style.Add("display", "block");
                                lblLevel.Style.Add("display", "block");
                                lblEvaluationCriteria.Style.Add("display", "block");
                                lblPoints.Style.Add("display", "block");
                                lblWeight.Style.Add("display", "block");
                                //lblScore.Style.Add("display", "block");

                                lbSave.Style.Add("display", "none");
                                lbEdit.Style.Add("display", "block");
                                lbEdit.Text = "EDIT";
                                lbDisabled.Style.Add("display", "block");

                                LoadDefault();
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Record has been successfully updated!');", true);
                            }
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                        }

                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                    }

                }

                if (e.CommandName == "AddNew_Command")
                {
                    if (lbAddNew.Text == "ADD NEW")
                    {
                        txtNewEvaluationCriteria.Enabled = true;
                        txtNewWeight.Enabled = true;
                        //txtNewScore.Enabled = true;
                        txtNewPoints.Enabled = true;
                        ddItemNew.Enabled = true;
                        ddLevelNew.Enabled = true;

                        lbAddNew.Text = "CANCEL";
                        lbSaveNew.Style.Add("display", "block");

                        //POPULATE EVALUATION ITEM
                        List<Entities_SE_EvaluationItem> listItem = new List<Entities_SE_EvaluationItem>();
                        listItem = BLL.SE_MT_EvaluationItem_GetAll();

                        if (listItem != null)
                        {
                            if (listItem.Count > 0)
                            {
                                ddItemNew.Items.Clear();
                                ddItemNew.Items.Add("");
                                foreach (Entities_SE_EvaluationItem itm in listItem)
                                {
                                    ListItem item = new ListItem();
                                    item.Text = itm.Item;
                                    item.Value = itm.RefId;

                                    if (itm.Type == "FOR_MATERIAL")
                                    {
                                        ddItemNew.Items.Add(item);
                                    }
                                }

                            }
                        }
                        // END OF POPULATE EVALUATION ITEM
                    }
                    else
                    {
                        txtNewEvaluationCriteria.Enabled = false;
                        txtNewWeight.Enabled = false;
                        //txtNewScore.Enabled = false;
                        txtNewPoints.Enabled = false;
                        ddItemNew.Enabled = false;
                        ddLevelNew.Enabled = false;

                        lbAddNew.Text = "ADD NEW";
                        lbSaveNew.Style.Add("display", "none");
                    }
                }

                if (e.CommandName == "SaveNew_Command")
                {

                    try
                    {
                        Entities_SE_EvaluationCriteria_ForMaterial entity = new Entities_SE_EvaluationCriteria_ForMaterial();
                        entity.Item = ddItemNew.SelectedValue;
                        entity.Criteria = txtNewEvaluationCriteria.Text.Replace("'", "''");
                        entity.Points = txtNewPoints.Text;
                        entity.Score = "0"; // SET TO ZERO BECAUSE I REMOVE THE FIELD SCORE
                        entity.Weight = txtNewWeight.Text;
                        entity.Addedby = Session["LcRefId"].ToString();
                        entity.Level = ddLevelNew.SelectedValue;

                        try
                        {
                            if (string.IsNullOrEmpty(ddItemNew.SelectedItem.Text))
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select a valid item');", true);
                            }
                            else if (string.IsNullOrEmpty(ddLevelNew.SelectedItem.Text))
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select a valid type');", true);
                            }
                            else if (entity.Criteria.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Criteria field must not be blank');", true);
                            }
                            else if (entity.Points.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Points field must not be blank');", true);
                            }
                            else if (entity.Weight.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Weight field must not be blank');", true);
                            }
                            //else if (entity.Score.Length <= 0)
                            //{
                            //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Score field must not be blank');", true);
                            //}
                            else
                            {
                                BLL.SE_MT_EvaluationCriteria_ForMaterial_Insert(entity);

                                txtNewEvaluationCriteria.Enabled = false;
                                txtNewPoints.Enabled = false;
                                txtNewWeight.Enabled = false;
                                //txtNewScore.Enabled = false;
                                ddItemNew.Enabled = false;
                                ddLevelNew.Enabled = false;

                                lbSaveNew.Style.Add("display", "none");
                                lbAddNew.Text = "ADD NEW";

                                LoadDefault();
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Record has been successfully added!');", true);
                            }
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                        }

                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                    }


                }

                if (e.CommandName == "StartDisabling_Command")
                {
                    try
                    {
                        Entities_SE_EvaluationCriteria_ForMaterial entity = new Entities_SE_EvaluationCriteria_ForMaterial();
                        entity.RefId = lblId.Text.Trim();
                        entity.Isdisabled = "1";
                        entity.DisabledBy = Session["UserFullName"].ToString() + " - " + DateTime.Now.ToLongDateString();

                        BLL.SE_MT_EvaluationCriteria_ForMaterial_IsDisabled(entity);

                        LoadDefault();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Record has been disabled!');", true);
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
                List<Entities_SE_EvaluationCriteria_ForMaterial> list = new List<Entities_SE_EvaluationCriteria_ForMaterial>();
                list = BLL.SE_MT_EvaluationCriteria_ForMaterial_GetAll().Where(itm => itm.Criteria.Contains(txtSearch.Text)).ToList();

                if (list.Count > 0)
                {
                    gvData.DataSource = list.Where(item => item.Isdisabled.Contains("0")).ToList();
                    gvData.DataBind();

                    gvDisabled.DataSource = list.Where(item => item.Isdisabled.Contains("1")).ToList();
                    gvDisabled.DataBind();

                    gvData.FooterRow.Visible = gvData.PageIndex == 0;

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





    }
}
