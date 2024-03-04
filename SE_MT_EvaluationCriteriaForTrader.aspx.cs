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
    public partial class SE_MT_EvaluationCriteriaForTrader : System.Web.UI.Page
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
                List<Entities_SE_EvaluationCriteria_Trader> list = new List<Entities_SE_EvaluationCriteria_Trader>();
                list = BLL.SE_MT_EvaluationCriteria_Trader_GetAll();

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
                    TextBox txtNewJudgement = (TextBox)gvData.FooterRow.FindControl("txtNewJudgement");
                    DropDownList ddItemNew = (DropDownList)gvData.FooterRow.FindControl("ddItemNew");
                    //DropDownList ddItem = (DropDownList)gvData.FooterRow.FindControl("ddItem");

                    lbSaveNew.Style.Add("display", "none");
                    txtNewEvaluationCriteria.Enabled = false;
                    txtNewPoints.Enabled = false;
                    txtNewJudgement.Enabled = false;
                    ddItemNew.Enabled = false;


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
                        Entities_SE_EvaluationCriteria_Trader entity = new Entities_SE_EvaluationCriteria_Trader();
                        entity.RefId = lblId.Text.Trim();
                        entity.Isdisabled = "0";
                        entity.DisabledBy = Session["UserFullName"].ToString() + " - " + DateTime.Now.ToLongDateString();

                        BLL.SE_MT_EvaluationCriteria_Trader_IsDisabled(entity);

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
                    Label lblJudgement = (Label)e.Row.FindControl("lblJudgement");
                    TextBox txtJudgement = (TextBox)e.Row.FindControl("txtJudgement");
                    LinkButton lbSave = (LinkButton)e.Row.FindControl("lbSave");
                    DropDownList ddItem = (DropDownList)e.Row.FindControl("ddItem");

                    txtEvaluationCriteria.Style.Add("display", "none");
                    txtPoints.Style.Add("display", "none");
                    txtJudgement.Style.Add("display", "none");
                    ddItem.Style.Add("display", "none");
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
                TextBox txtEvaluationCriteria = row.FindControl("txtEvaluationCriteria") as TextBox;
                TextBox txtPoints = row.FindControl("txtPoints") as TextBox;
                TextBox txtJudgement = row.FindControl("txtJudgement") as TextBox;
                Label lblId = row.FindControl("lblId") as Label;
                Label lblEvaluationCriteria = row.FindControl("lblEvaluationCriteria") as Label;
                Label lblPoints = row.FindControl("lblPoints") as Label;
                Label lblJudgement = row.FindControl("lblJudgement") as Label;
                Label lblItem = row.FindControl("lblItem") as Label;

                LinkButton lbEdit = row.FindControl("lbEdit") as LinkButton;
                LinkButton lbSave = row.FindControl("lbSave") as LinkButton;

                TextBox txtNewEvaluationCriteria = row.FindControl("txtNewEvaluationCriteria") as TextBox;
                TextBox txtNewPoints = row.FindControl("txtNewPoints") as TextBox;
                TextBox txtNewJudgement = row.FindControl("txtNewJudgement") as TextBox;
                DropDownList ddItemNew = row.FindControl("ddItemNew") as DropDownList;

                LinkButton lbAddNew = gvData.FooterRow.FindControl("lbAddNew") as LinkButton;
                LinkButton lbSaveNew = gvData.FooterRow.FindControl("lbSaveNew") as LinkButton;
                LinkButton lbDisabled = row.FindControl("lbDisabled") as LinkButton;

                if (e.CommandName == "StartEditing_Command")
                {
                    if (lbEdit.Text == "CANCEL")
                    {
                        txtEvaluationCriteria.Style.Add("display", "none");
                        txtPoints.Style.Add("display", "none");
                        txtJudgement.Style.Add("display", "none");
                        ddItem.Style.Add("display", "none");

                        lblEvaluationCriteria.Style.Add("display", "block");
                        lblPoints.Style.Add("display", "block");
                        lblJudgement.Style.Add("display", "block");
                        lblItem.Style.Add("display", "block");
                        lbDisabled.Style.Add("display", "block");

                        lbSave.Style.Add("display", "none");
                        lbEdit.Text = "EDIT";
                    }
                    else
                    {
                        lblEvaluationCriteria.Style.Add("display", "none");
                        lblPoints.Style.Add("display", "none");
                        lblJudgement.Style.Add("display", "none");
                        lblItem.Style.Add("display", "none");

                        txtEvaluationCriteria.Style.Add("display", "block");
                        txtPoints.Style.Add("display", "block");
                        txtJudgement.Style.Add("display", "block");
                        ddItem.Style.Add("display", "block");

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

                                    if (itm.Type == "TRADER")
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
                        Entities_SE_EvaluationCriteria_Trader entity = new Entities_SE_EvaluationCriteria_Trader();
                        entity.RefId = lblId.Text.Trim();
                        entity.Item = ddItem.SelectedValue;
                        entity.Criteria = txtEvaluationCriteria.Text.Replace("'", "''");
                        entity.Points = txtPoints.Text;
                        entity.Judgement = txtJudgement.Text;
                        entity.Updatedby = Session["LcRefId"].ToString();

                        try
                        {

                            if (string.IsNullOrEmpty(ddItem.SelectedItem.Text))
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select a valid item');", true);
                            }
                            else if (entity.Criteria.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Criteria field must not be blank');", true);
                            }
                            else if (entity.Points.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Points field must not be blank');", true);
                            }

                            else
                            {
                                BLL.SE_MT_EvaluationCriteria_Trader_Append(entity);

                                lblItem.Text = ddItem.SelectedItem.Text;
                                lblEvaluationCriteria.Text = txtEvaluationCriteria.Text;
                                lblPoints.Text = txtPoints.Text;
                                lblJudgement.Text = txtJudgement.Text;

                                ddItem.Style.Add("display", "none");
                                txtEvaluationCriteria.Style.Add("display", "none");
                                txtPoints.Style.Add("display", "none");
                                txtJudgement.Style.Add("display", "none");

                                lblItem.Style.Add("display", "block");
                                lblEvaluationCriteria.Style.Add("display", "block");
                                lblPoints.Style.Add("display", "block");
                                lblJudgement.Style.Add("display", "block");

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
                        txtNewJudgement.Enabled = true;
                        txtNewPoints.Enabled = true;
                        ddItemNew.Enabled = true;

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

                                    if (itm.Type == "TRADER")
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
                        txtNewJudgement.Enabled = false;
                        txtNewPoints.Enabled = false;
                        ddItemNew.Enabled = false;

                        lbAddNew.Text = "ADD NEW";
                        lbSaveNew.Style.Add("display", "none");
                    }
                }

                if (e.CommandName == "SaveNew_Command")
                {

                    try
                    {
                        Entities_SE_EvaluationCriteria_Trader entity = new Entities_SE_EvaluationCriteria_Trader();
                        entity.Item = ddItemNew.SelectedValue;
                        entity.Criteria = txtNewEvaluationCriteria.Text.Replace("'", "''");
                        entity.Points = txtNewPoints.Text;
                        entity.Judgement = txtNewJudgement.Text;
                        entity.Addedby = Session["LcRefId"].ToString();

                        try
                        {
                            if (string.IsNullOrEmpty(ddItemNew.SelectedItem.Text))
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Please select a valid item');", true);
                            }
                            else if (entity.Criteria.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Criteria field must not be blank');", true);
                            }
                            else if (entity.Points.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Points field must not be blank');", true);
                            }
                            else
                            {
                                BLL.SE_MT_EvaluationCriteria_Trader_Insert(entity);

                                txtNewEvaluationCriteria.Enabled = false;
                                txtNewPoints.Enabled = false;
                                txtNewJudgement.Enabled = false;
                                ddItemNew.Enabled = false;

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
                        Entities_SE_EvaluationCriteria_Trader entity = new Entities_SE_EvaluationCriteria_Trader();
                        entity.RefId = lblId.Text.Trim();
                        entity.Isdisabled = "1";
                        entity.DisabledBy = Session["UserFullName"].ToString() + " - " + DateTime.Now.ToLongDateString();

                        BLL.SE_MT_EvaluationCriteria_Trader_IsDisabled(entity);

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
                List<Entities_SE_EvaluationCriteria_Trader> list = new List<Entities_SE_EvaluationCriteria_Trader>();
                list = BLL.SE_MT_EvaluationCriteria_Trader_GetAll().Where(itm => itm.Criteria.Contains(txtSearch.Text)).ToList();

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
