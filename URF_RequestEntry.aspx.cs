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


namespace REPI_PUR_SOFRA
{
    public partial class URF_RequestEntry : System.Web.UI.Page
    {

        BLL_URF BLL = new BLL_URF();
        Common COMMON = new Common();
        BLL_Common BLL_COMMON = new BLL_Common();
        public string displayCTRLNo = string.Empty;
        public string reportCTRLNo = string.Empty;
        public string displayAttachment = string.Empty;

        private string ddumitems;
        public string Ddumitems { get { return ddumitems; } set { ddumitems = value; } }

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
                    if (!String.IsNullOrEmpty(Request.QueryString["URFNo_From_Inquiry"].ToString()))
                    {
                        string rfqNo = CryptorEngine.Decrypt(Request.QueryString["URFNo_From_Inquiry"].ToString().Replace(" ", "+"), true);
                        LoadDefaultForUpdate(rfqNo);
                        Page.Title = rfqNo;
                        lblCtrlNo.Text = rfqNo;
                        displayCTRLNo = "<b style='color:red'>(" + rfqNo + ")</b>";

                        if (Request.QueryString["previewrep"] != null)
                        {
                            if (!string.IsNullOrEmpty(Request.QueryString["previewrep"].ToString()))
                            {
                                if (Request.QueryString["previewrep"].ToString() == "true")
                                {
                                    btnPreview_Click(sender, e);
                                }
                            }
                        }
                    }
                    else
                    {
                        LoadDefault();
                        Ddumitems = "<asp:ListItem Text='NO' Value='0'></asp:ListItem>";
                        FirstGridViewRow();
                        displayCTRLNo = string.Empty;
                        btnPreview.Visible = false;
                        txtOtherReason.Enabled = false;
                    }
                }                

            }
        }

        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("RdRefId", typeof(string)));
            dt.Columns.Add(new DataColumn("RdPONO", typeof(string)));
            dt.Columns.Add(new DataColumn("RdPRNO", typeof(string)));
            dt.Columns.Add(new DataColumn("RdItemName", typeof(string)));
            dt.Columns.Add(new DataColumn("RdSpecs", typeof(string)));
            dt.Columns.Add(new DataColumn("RdQuantity", typeof(string)));
            dt.Columns.Add(new DataColumn("RdUnitOfMeasure", typeof(string)));
            dt.Columns.Add(new DataColumn("RdDeliveryConfirmationDate", typeof(string)));
            dt.Columns.Add(new DataColumn("RdRequestedDeliveryDate", typeof(string)));
            dt.Columns.Add(new DataColumn("RdReplyDeliveryDate", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["RdRefId"] = string.Empty;
            dr["RdPONO"] = string.Empty;
            dr["RdPRNO"] = string.Empty;
            dr["RdItemName"] = string.Empty;
            dr["RdSpecs"] = string.Empty;
            dr["RdQuantity"] = string.Empty;
            dr["RdUnitOfMeasure"] = string.Empty;
            dr["RdDeliveryConfirmationDate"] = string.Empty;
            dr["RdRequestedDeliveryDate"] = string.Empty;
            dr["RdReplyDeliveryDate"] = string.Empty;

            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;

            gvData.DataSource = dt;
            gvData.DataBind();


        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (gvData.Rows.Count <= 9)
            {
                AddNewRow();
                if (ddCategory.SelectedItem.Text.Trim() == "SUB-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "MAIN-MATERIALS")
                {
                    tblStockLife.Style.Add("display", "block");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('You can only add up to 10 items.');", true);
            }
        }

        private void AddNewRow()
        {
            try
            {

                int rowIndex = 0;

                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    DataRow drCurrentRow = null;
                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            TextBox txtPONO =
                              (TextBox)gvData.Rows[rowIndex].Cells[1].FindControl("txtPONO");
                            TextBox txtPRNO =
                              (TextBox)gvData.Rows[rowIndex].Cells[2].FindControl("txtPRNO");
                            TextBox txtItemName =
                              (TextBox)gvData.Rows[rowIndex].Cells[3].FindControl("txtItemName");
                            TextBox txtSpecification =
                              (TextBox)gvData.Rows[rowIndex].Cells[4].FindControl("txtSpecification");
                            TextBox txtQuantity =
                              (TextBox)gvData.Rows[rowIndex].Cells[5].FindControl("txtQuantity");
                            DropDownList ddUOM =
                              (DropDownList)gvData.Rows[rowIndex].Cells[6].FindControl("ddUOM");
                            TextBox txtDeliveryConfirmationDate =
                              (TextBox)gvData.Rows[rowIndex].Cells[7].FindControl("txtDeliveryConfirmationDate");
                            TextBox txtRequestedDeliveryDate =
                              (TextBox)gvData.Rows[rowIndex].Cells[8].FindControl("txtRequestedDeliveryDate");
                            TextBox txtReplyDeliveryDate =
                              (TextBox)gvData.Rows[rowIndex].Cells[9].FindControl("txtReplyDeliveryDate");


                            //---------------------------------------------------------------------------------------------------

                            List<Entities_URF_RequestEntry> listDropDown = new List<Entities_URF_RequestEntry>();
                            listDropDown = BLL.URF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                            if (listDropDown != null)
                            {
                                if (listDropDown.Count > 0)
                                {
                                    ddUOM.Items.Add("");

                                    foreach (Entities_URF_RequestEntry entity in listDropDown)
                                    {
                                        ListItem item = new ListItem();
                                        item.Text = entity.DropdownName.ToUpper();
                                        item.Value = entity.DropdownRefId;

                                        if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                                        {
                                            if (entity.TableName == "MT_UnitOfMeasure")
                                            {
                                                ddUOM.Items.Add(item);
                                            }                                            
                                        }

                                    }

                                }
                            }

                            //---------------------------------------------------------------------------------------------------

                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["RowNumber"] = i + 1;

                            dtCurrentTable.Rows[i - 1]["RdRefId"] = string.Empty;
                            dtCurrentTable.Rows[i - 1]["RdPONO"] = txtPONO.Text;
                            dtCurrentTable.Rows[i - 1]["RdPRNO"] = txtPRNO.Text;
                            dtCurrentTable.Rows[i - 1]["RdItemName"] = txtItemName.Text;
                            dtCurrentTable.Rows[i - 1]["RdSpecs"] = txtSpecification.Text;
                            dtCurrentTable.Rows[i - 1]["RdQuantity"] = txtQuantity.Text;
                            dtCurrentTable.Rows[i - 1]["RdUnitOfMeasure"] = ddUOM.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["RdDeliveryConfirmationDate"] = txtDeliveryConfirmationDate.Text;
                            dtCurrentTable.Rows[i - 1]["RdRequestedDeliveryDate"] = txtRequestedDeliveryDate.Text;
                            dtCurrentTable.Rows[i - 1]["RdReplyDeliveryDate"] = txtReplyDeliveryDate.Text;
                            rowIndex++;

                        }
                        dtCurrentTable.Rows.Add(drCurrentRow);
                        ViewState["CurrentTable"] = dtCurrentTable;

                        gvData.DataSource = dtCurrentTable;
                        gvData.DataBind();

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('ViewState is null');", true);
                }
                SetPreviousData();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.StackTrace.ToString() + "');", true);
            }
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtPONO =
                              (TextBox)gvData.Rows[rowIndex].Cells[1].FindControl("txtPONO");
                        TextBox txtPRNO =
                          (TextBox)gvData.Rows[rowIndex].Cells[2].FindControl("txtPRNO");
                        TextBox txtItemName =
                          (TextBox)gvData.Rows[rowIndex].Cells[3].FindControl("txtItemName");
                        TextBox txtSpecification =
                          (TextBox)gvData.Rows[rowIndex].Cells[4].FindControl("txtSpecification");
                        TextBox txtQuantity =
                          (TextBox)gvData.Rows[rowIndex].Cells[5].FindControl("txtQuantity");
                        DropDownList ddUOM =
                          (DropDownList)gvData.Rows[rowIndex].Cells[6].FindControl("ddUOM");
                        TextBox txtDeliveryConfirmationDate =
                          (TextBox)gvData.Rows[rowIndex].Cells[7].FindControl("txtDeliveryConfirmationDate");
                        TextBox txtRequestedDeliveryDate =
                          (TextBox)gvData.Rows[rowIndex].Cells[8].FindControl("txtRequestedDeliveryDate");
                        TextBox txtReplyDeliveryDate =
                          (TextBox)gvData.Rows[rowIndex].Cells[9].FindControl("txtReplyDeliveryDate");

                        txtPONO.Text = dt.Rows[i]["RdPONO"].ToString();
                        txtPRNO.Text = dt.Rows[i]["RdPRNO"].ToString();
                        txtItemName.Text = dt.Rows[i]["RdItemName"].ToString();
                        txtSpecification.Text = dt.Rows[i]["RdSpecs"].ToString();
                        txtQuantity.Text = dt.Rows[i]["RdQuantity"].ToString();
                        ddUOM.Items.FindByValue(dt.Rows[i]["RdUnitOfMeasure"].ToString()).Selected = true;
                        txtDeliveryConfirmationDate.Text = dt.Rows[i]["RdDeliveryConfirmationDate"].ToString();
                        txtRequestedDeliveryDate.Text = dt.Rows[i]["RdRequestedDeliveryDate"].ToString();
                        txtReplyDeliveryDate.Text = dt.Rows[i]["RdReplyDeliveryDate"].ToString();


                        rowIndex++;

                    }
                }
            }
        }

        private void SetRowData()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtPONO =
                              (TextBox)gvData.Rows[rowIndex].Cells[1].FindControl("txtPONO");
                        TextBox txtPRNO =
                          (TextBox)gvData.Rows[rowIndex].Cells[2].FindControl("txtPRNO");
                        TextBox txtItemName =
                          (TextBox)gvData.Rows[rowIndex].Cells[3].FindControl("txtItemName");
                        TextBox txtSpecification =
                          (TextBox)gvData.Rows[rowIndex].Cells[4].FindControl("txtSpecification");
                        TextBox txtQuantity =
                          (TextBox)gvData.Rows[rowIndex].Cells[5].FindControl("txtQuantity");
                        DropDownList ddUOM =
                          (DropDownList)gvData.Rows[rowIndex].Cells[6].FindControl("ddUOM");
                        TextBox txtDeliveryConfirmationDate =
                          (TextBox)gvData.Rows[rowIndex].Cells[7].FindControl("txtDeliveryConfirmationDate");
                        TextBox txtRequestedDeliveryDate =
                          (TextBox)gvData.Rows[rowIndex].Cells[8].FindControl("txtRequestedDeliveryDate");
                        TextBox txtReplyDeliveryDate =
                          (TextBox)gvData.Rows[rowIndex].Cells[9].FindControl("txtReplyDeliveryDate");


                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["RdRefId"] = string.Empty;
                        dtCurrentTable.Rows[i - 1]["RdPONO"] = txtPONO.Text;
                        dtCurrentTable.Rows[i - 1]["RdPRNO"] = txtPRNO.Text;
                        dtCurrentTable.Rows[i - 1]["RdItemName"] = txtItemName.Text;
                        dtCurrentTable.Rows[i - 1]["RdSpecs"] = txtSpecification.Text;
                        dtCurrentTable.Rows[i - 1]["RdQuantity"] = txtQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["RdUnitOfMeasure"] = ddUOM.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["RdDeliveryConfirmationDate"] = txtDeliveryConfirmationDate.Text;
                        dtCurrentTable.Rows[i - 1]["RdRequestedDeliveryDate"] = txtRequestedDeliveryDate.Text;
                        dtCurrentTable.Rows[i - 1]["RdReplyDeliveryDate"] = txtReplyDeliveryDate.Text;
                        rowIndex++;

                    }

                    ViewState["CurrentTable"] = dtCurrentTable;
                    //gvData.DataSource = dtCurrentTable;
                    //gvData.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            //SetPreviousData();
        }

        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddUOM = (DropDownList)e.Row.FindControl("ddUOM");
                    Label lblUOM = (Label)e.Row.FindControl("lblUOM");
                    //HtmlGenericControl divDataLink = (HtmlGenericControl)e.Row.FindControl("divDataLink");
                    TextBox txtPONO = (TextBox)e.Row.FindControl("txtPONO");
                    //GridView gvDataLink = (GridView)e.Row.FindControl("gvDataLink");

                    TextBox txtReplyDeliveryDate = (TextBox)e.Row.FindControl("txtReplyDeliveryDate");
                    TextBox txtItemName = (TextBox)e.Row.FindControl("txtItemName");
                    TextBox txtSpecification = (TextBox)e.Row.FindControl("txtSpecification");
                    TextBox txtQuantity = (TextBox)e.Row.FindControl("txtQuantity");
                    TextBox txtPRNO = (TextBox)e.Row.FindControl("txtPRNO");
                    TextBox txtDeliveryConfirmationDate = (TextBox)e.Row.FindControl("txtDeliveryConfirmationDate");

                    List<Entities_URF_RequestEntry> listDropDown = new List<Entities_URF_RequestEntry>();
                    listDropDown = BLL.URF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                    if (listDropDown != null)
                    {
                        if (listDropDown.Count > 0)
                        {
                            ddUOM.Items.Add("");

                            foreach (Entities_URF_RequestEntry entity in listDropDown)
                            {
                                ListItem item = new ListItem();
                                item.Text = entity.DropdownName.ToUpper();
                                item.Value = entity.DropdownRefId;

                                if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                                {
                                    if (entity.TableName == "MT_UnitOfMeasure")
                                    {
                                        ddUOM.Items.Add(item);
                                    }                                    
                                }

                            }

                            if (!String.IsNullOrEmpty(Request.QueryString["URFNo_From_Inquiry"].ToString()))
                            {
                                ddUOM.Items.FindByValue(lblUOM.Text.Trim()).Selected = true;
                            }

                        }
                    }

                    //----------------------------------------------------------------------------------------------------

                    
                    if (txtReplyDeliveryDate.Text.ToUpper() == "CLOSED")
                    {
                        if (!string.IsNullOrEmpty(Session["CategoryAccess"].ToString()))
                        {
                            if (!string.IsNullOrEmpty(txtPONO.Text))
                            {
                                txtReplyDeliveryDate.Style.Add("Color", "Red");
                                txtItemName.Style.Add("Color", "Red");
                                txtSpecification.Style.Add("Color", "Red");
                                txtQuantity.Style.Add("Color", "Red");
                                txtPONO.Style.Add("Color", "Red");
                                txtDeliveryConfirmationDate.Style.Add("Color", "Red");
                                txtPRNO.Style.Add("Color", "Red");
                                ddUOM.Style.Add("Color", "Red");
                                lblUOM.Style.Add("Color", "Red");
                            }
                            else
                            {
                                txtReplyDeliveryDate.Text = string.Empty;
                            }

                            //lblUOM.Enabled = false;
                            //ddUOM.Enabled = false;
                            //txtItemName.Enabled = false;
                            //txtSpecification.Enabled = false;
                            //txtQuantity.Enabled = false;
                            //txtPONO.Enabled = false;
                            //txtDeliveryConfirmationDate.Enabled = false;
                            //txtPRNO.Enabled = false;
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }
        

        protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                SetRowData();
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    DataRow drCurrentRow = null;
                    int rowIndex = Convert.ToInt32(e.RowIndex);
                    if (dt.Rows.Count > 1)
                    {
                        dt.Rows.Remove(dt.Rows[rowIndex]);
                        drCurrentRow = dt.NewRow();
                        ViewState["CurrentTable"] = dt;
                        gvData.DataSource = dt;
                        gvData.DataBind();

                        for (int i = 0; i < gvData.Rows.Count - 1; i++)
                        {
                            gvData.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                        }
                        SetPreviousData();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.StackTrace.ToString() + "');", true);
            }
        }
        
        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                
                TextBox txtPONO = row.FindControl("txtPONO") as TextBox;
                TextBox txtPRNO = row.FindControl("txtPRNO") as TextBox;
                TextBox txtItemName = row.FindControl("txtItemName") as TextBox;
                TextBox txtSpecification = row.FindControl("txtSpecification") as TextBox;
                TextBox txtQuantity = row.FindControl("txtQuantity") as TextBox;
                DropDownList ddUOM = row.FindControl("ddUOM") as DropDownList;
                TextBox txtDeliveryConfirmationDate = row.FindControl("txtDeliveryConfirmationDate") as TextBox;
                

                GridView gvDataLinkToPR = row.FindControl("gvDataLinkToPR") as GridView;
                HtmlGenericControl divDataLinkToPR = row.FindControl("divDataLinkToPR") as HtmlGenericControl;


                if (e.CommandName == "btnPONO_Command")
                {
                    if (string.IsNullOrEmpty(txtPONO.Text))
                    {
                        divDataLinkToPR.Style.Add("display", "none");
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Search item must not be blank.');", true);
                        
                    }                    
                    else
                    {
                        List<Entities_URF_RequestEntry> listDataLink = new List<Entities_URF_RequestEntry>();
                        listDataLink = BLL.vewRFQDelivery("SELECT TOP 10 * FROM [ROPROS].[dbo].[vewRFQDelivery] WITH (NOLOCK) WHERE PONO LIKE '%" + txtPONO.Text + "%'", index.ToString());
                        //listDataLink = BLL.vewRFQDelivery("SELECT TOP 10 * FROM v_vewRFQDelivery WITH (NOLOCK) WHERE PONO LIKE '%" + txtPONO.Text + "%'", index.ToString());

                        if (listDataLink != null)
                        {
                            if (listDataLink.Count > 0)
                            {
                                gvDataLinkToPR.DataSource = listDataLink;
                                gvDataLinkToPR.DataBind();

                                divDataLinkToPR.Style.Add("display", "block");
                               
                            }
                            else
                            {
                                divDataLinkToPR.Style.Add("display", "none");
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('No PONO (" + txtPONO.Text + ") found in the data collection.');", true);
                                
                            }

                        }
                        else
                        {
                            divDataLinkToPR.Style.Add("display", "none");
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('No PONO (" + txtPONO.Text + ") found in the data collection.');", true);                            

                        }

                        if (ddCategory.SelectedItem.Text.Trim() == "SUB-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "MAIN-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "CHEMICALS")
                        {
                            tblStockLife.Style.Add("display", "block");
                        }
                        else
                        {
                            tblStockLife.Style.Add("display", "none");
                        }

                    }

                }

                if (e.CommandName == "btnPRNO_Command")
                {
                    if (string.IsNullOrEmpty(txtPRNO.Text))
                    {
                        divDataLinkToPR.Style.Add("display", "none");
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Search item must not be blank.');", true);
                    }
                    else
                    {
                        List<Entities_URF_RequestEntry> listDataLink = new List<Entities_URF_RequestEntry>();
                        listDataLink = BLL.vewRFQDelivery("SELECT TOP 10 * FROM [ROPROS].[dbo].[vewRFQDelivery] WITH (NOLOCK) WHERE PRNO LIKE '%" + txtPRNO.Text + "%'", index.ToString());
                        //listDataLink = BLL.vewRFQDelivery("SELECT TOP 10 * FROM v_vewRFQDelivery WITH (NOLOCK) WHERE PRNO LIKE '%" + txtPRNO.Text + "%'", index.ToString());

                        if (listDataLink != null)
                        {
                            if (listDataLink.Count > 0)
                            {
                                gvDataLinkToPR.DataSource = listDataLink;
                                gvDataLinkToPR.DataBind();

                                divDataLinkToPR.Style.Add("display", "block");
                                

                            }
                            else
                            {
                                divDataLinkToPR.Style.Add("display", "none");
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('No PRNO (" + txtPRNO.Text + ") found in the data collection.');", true);

                            }

                        }
                        else
                        {
                            divDataLinkToPR.Style.Add("display", "none");
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('No PRNO (" + txtPRNO.Text + ") found in the data collection.');", true);
                            
                        }

                        if (ddCategory.SelectedItem.Text.Trim() == "SUB-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "MAIN-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "CHEMICALS")
                        {
                            tblStockLife.Style.Add("display", "block");
                        }
                        else
                        {
                            tblStockLife.Style.Add("display", "none");
                        }


                    }

                }

                if (e.CommandName == "btnItemName_Command")
                {
                    if (string.IsNullOrEmpty(txtItemName.Text))
                    {
                        divDataLinkToPR.Style.Add("display", "none");
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Search item must not be blank.');", true);
                    }
                    else
                    {
                        List<Entities_URF_RequestEntry> listDataLink = new List<Entities_URF_RequestEntry>();
                        listDataLink = BLL.vewRFQDelivery("SELECT TOP 10 * FROM [ROPROS].[dbo].[vewRFQDelivery] WITH (NOLOCK) WHERE ITEM LIKE '%" + txtItemName.Text + "%'", index.ToString());

                        if (listDataLink != null)
                        {
                            if (listDataLink.Count > 0)
                            {
                                gvDataLinkToPR.DataSource = listDataLink;
                                gvDataLinkToPR.DataBind();

                                divDataLinkToPR.Style.Add("display", "block");
                            }
                            else
                            {
                                divDataLinkToPR.Style.Add("display", "none");
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('No ItemName (" + txtItemName.Text + ") found in the data collection.');", true);
                            }
                        }
                        else
                        {
                            divDataLinkToPR.Style.Add("display", "none");
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('No ItemName (" + txtItemName.Text + ") found in the data collection.');", true);
                        }

                        if (ddCategory.SelectedItem.Text.Trim() == "SUB-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "MAIN-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "CHEMICALS")
                        {
                            tblStockLife.Style.Add("display", "block");
                        }
                        else
                        {
                            tblStockLife.Style.Add("display", "none");
                        }


                    }
                }                

                if (e.CommandName == "btnSpecification_Command")
                {
                    if (string.IsNullOrEmpty(txtSpecification.Text))
                    {
                        divDataLinkToPR.Style.Add("display", "none");
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Search item must not be blank.');", true);
                    }
                    else
                    {
                        List<Entities_URF_RequestEntry> listDataLink = new List<Entities_URF_RequestEntry>();
                        listDataLink = BLL.vewRFQDelivery("SELECT TOP 10 * FROM [ROPROS].[dbo].[vewRFQDelivery] WITH (NOLOCK) WHERE Specification LIKE '%" + txtSpecification.Text + "%'", index.ToString());

                        if (listDataLink != null)
                        {
                            if (listDataLink.Count > 0)
                            {
                                gvDataLinkToPR.DataSource = listDataLink;
                                gvDataLinkToPR.DataBind();

                                divDataLinkToPR.Style.Add("display", "block");
                            }
                            else
                            {
                                divDataLinkToPR.Style.Add("display", "none");
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('No Specification (" + txtSpecification.Text + ") found in the data collection.');", true);
                            }
                        }
                        else
                        {
                            divDataLinkToPR.Style.Add("display", "none");
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('No Specification (" + txtSpecification.Text + ") found in the data collection.');", true);
                        }

                        if (ddCategory.SelectedItem.Text.Trim() == "SUB-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "MAIN-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "CHEMICALS")
                        {
                            tblStockLife.Style.Add("display", "block");
                        }
                        else
                        {
                            tblStockLife.Style.Add("display", "none");
                        }

                    }
                }
                


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvDataLinkToPR_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                Label lblDataIndex = row.FindControl("lblDataIndex") as Label;

                HtmlGenericControl divDataLinkToPR = gvData.Rows[int.Parse(lblDataIndex.Text.Trim())].FindControl("divDataLinkToPR") as HtmlGenericControl;
                TextBox txtPONO = gvData.Rows[int.Parse(lblDataIndex.Text.Trim())].FindControl("txtPONO") as TextBox;
                TextBox txtPRNO = gvData.Rows[int.Parse(lblDataIndex.Text.Trim())].FindControl("txtPRNO") as TextBox;
                TextBox txtItemName = gvData.Rows[int.Parse(lblDataIndex.Text.Trim())].FindControl("txtItemName") as TextBox;
                TextBox txtSpecification = gvData.Rows[int.Parse(lblDataIndex.Text.Trim())].FindControl("txtSpecification") as TextBox;
                TextBox txtQuantity = gvData.Rows[int.Parse(lblDataIndex.Text.Trim())].FindControl("txtQuantity") as TextBox;
                TextBox txtDeliveryConfirmationDate = gvData.Rows[int.Parse(lblDataIndex.Text.Trim())].FindControl("txtDeliveryConfirmationDate") as TextBox;
                DropDownList ddUOM = gvData.Rows[int.Parse(lblDataIndex.Text.Trim())].FindControl("ddUOM") as DropDownList;

                Label lblLinkPONO = row.FindControl("lblLinkPONO") as Label;
                Label lblLinkPRNO = row.FindControl("lblLinkPRNO") as Label;
                Label lblLinkItemName = row.FindControl("lblLinkItemName") as Label;
                Label lblLinkSpecs = row.FindControl("lblLinkSpecs") as Label;
                Label lblLinkQuantity = row.FindControl("lblLinkQuantity") as Label;
                Label lblLinkReplyDeliveryDate = row.FindControl("lblLinkReplyDeliveryDate") as Label;
                Label lblLinkUnitOfMeasure = row.FindControl("lblLinkUnitOfMeasure") as Label;
                Label lblLinkPODate = row.FindControl("lblLinkPODate") as Label;

                if (e.CommandName == "btnLinkSelectPR_Command")
                {
                    txtPONO.Text = lblLinkPONO.Text.Trim();
                    txtPRNO.Text = lblLinkPRNO.Text.Trim();
                    txtItemName.Text = lblLinkItemName.Text;
                    txtSpecification.Text = lblLinkSpecs.Text;
                    txtQuantity.Text = lblLinkQuantity.Text.Trim();
                    //txtDeliveryConfirmationDate.Text = lblLinkReplyDeliveryDate.Text;
                    txtDeliveryConfirmationDate.Text = lblLinkPODate.Text;

                    
                    //---------------------------------------------------------------------------------------------
                    List<Entities_URF_RequestEntry> listDropDown = new List<Entities_URF_RequestEntry>();
                    listDropDown = BLL.URF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                    if (listDropDown != null)
                    {
                        if (listDropDown.Count > 0)
                        {
                            ddUOM.Items.Clear();
                            ddUOM.Items.Add("");

                            foreach (Entities_URF_RequestEntry entity in listDropDown)
                            {
                                ListItem item = new ListItem();
                                item.Text = entity.DropdownName.ToUpper();
                                item.Value = entity.DropdownRefId;

                                if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                                {
                                    if (entity.TableName == "MT_UnitOfMeasure")
                                    {
                                        ddUOM.Items.Add(item);
                                    }
                                }

                            }

                            ddUOM.Items.FindByText(lblLinkUnitOfMeasure.Text.ToUpper().Trim()).Selected = true;

                            ddUOM.Enabled = false;

                        }
                    }
                    //---------------------------------------------------------------------------------------------                    

                    divDataLinkToPR.Style.Add("display", "none");

                    if (ddCategory.SelectedItem.Text.Trim() == "SUB-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "MAIN-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "CHEMICALS")
                    {
                        tblStockLife.Style.Add("display", "block");
                    }
                    else
                    {
                        tblStockLife.Style.Add("display", "none");
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        private void LoadDefaultForUpdate(string rfqno)
        {
            try
            {
                string attachmentLiteralInside = string.Empty;
                string pdfSource = string.Empty;
                int attachmentCounter = 1;                



                //---------------------------------------------------------------------------------------------------

                List<Entities_URF_RequestEntry> listDropDown = new List<Entities_URF_RequestEntry>();
                listDropDown = BLL.URF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                if (listDropDown != null)
                {
                    if (listDropDown.Count > 0)
                    {
                        ddReason.Items.Add("");
                        ddCategory.Items.Add("");
                        ddSupplier.Items.Add("");

                        foreach (Entities_URF_RequestEntry entity in listDropDown)
                        {
                            ListItem item = new ListItem();
                            item.Text = entity.DropdownName.ToUpper();
                            item.Value = entity.DropdownRefId;

                            if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                            {
                                if (entity.TableName == "URF_MT_Reason")
                                {
                                    ddReason.Items.Add(item);
                                }
                                if (entity.TableName == "MT_Category")
                                {
                                    if (entity.DropdownRefId == "1013" || entity.DropdownRefId == "1006")
                                    {
                                        //DO NOT ADD THIS CATEGORY
                                    }
                                    else
                                    {
                                        ddCategory.Items.Add(item);
                                    }
                                }
                                if (entity.TableName == "MT_Supplier_Head")
                                {
                                    ddSupplier.Items.Add(item);
                                }
                            }

                        }

                    }
                }

                //---------------------------------------------------------------------------------------------------

                List<Entities_URF_RequestEntry> listHead = new List<Entities_URF_RequestEntry>();
                
                Entities_URF_RequestEntry entityHead = new Entities_URF_RequestEntry();
                entityHead.RhCtrlNo = rfqno;

                listHead = BLL.URF_TRANSACTION_GetRequestHeadByCTRLNo(entityHead);

                if (listHead != null)
                {
                    if (listHead.Count > 0)
                    {
                        foreach (Entities_URF_RequestEntry eHead in listHead)
                        {
                            ddCategory.Items.FindByValue(eHead.RhCategory).Selected = true;
                            ddSupplier.Items.FindByValue(eHead.RhSupplier).Selected = true;
                            if (eHead.RhReason == "5" || eHead.RhReason == "10")
                            {
                                ddReason.Items.FindByValue(eHead.RhReason).Selected = true;
                                txtOtherReason.Text = eHead.RhOtherReason; 
                            }
                            else
                            {
                                if (eHead.RhReason == "0")
                                {
                                    txtOtherReason.Text = eHead.RhOtherReason; 
                                }
                                else
                                {
                                    ddReason.Items.FindByValue(eHead.RhReason).Selected = true;
                                }
                            }
                            ddType.Items.FindByValue(eHead.RhType).Selected = true;
                            txtAttention.Text = eHead.RhAttention;    
                        
                            // REPISTOCK, DAILY USAGE, STOCKLIFE
                            if (ddCategory.SelectedItem.Text.Trim() == "SUB-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "MAIN-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "CHEMICALS")
                            {
                                txtRepiStock.Text = eHead.RepiStock;
                                txtDailyUsage.Text = eHead.DailyUsage;
                                txtStockLife.Text = eHead.StockLife;

                                tblStockLife.Style.Add("display", "block");
                            }

                            // PURCHASING REMARKS
                            if (!string.IsNullOrEmpty(eHead.RhRemarks))
                            {
                                tableRemarks.Style.Add("display", "block");
                                txtRemarks.Text = eHead.RhRemarks;
                            }

                        }
                    }
                }

                //---------------------------------------------------------------------------------------------------

                List<Entities_URF_RequestEntry> listDetails = new List<Entities_URF_RequestEntry>();
                Entities_URF_RequestEntry entityDetails = new Entities_URF_RequestEntry();
                entityDetails.RdCtrlNo = rfqno;

                listDetails = BLL.URF_TRANSACTION_GetRequestDetailsByCTRLNo2(entityDetails);

                if (listDetails != null)
                {
                    if (listDetails.Count > 0)
                    {
                        gvData.DataSource = listDetails;
                        gvData.DataBind();

                        //// DISABLED ALL 3 DOTS BUTTONS
                        //if (gvData.Rows.Count > 0)
                        //{
                        //    for (int i = 0; i < gvData.Rows.Count; i++)
                        //    {
                        //        TextBox txtPONO = gvData.Rows[i].Cells[1].FindControl("txtPONO") as TextBox;
                        //        TextBox txtPRNO = gvData.Rows[i].Cells[2].FindControl("txtPRNO") as TextBox;
                        //        TextBox txtItemName = gvData.Rows[i].Cells[3].FindControl("txtItemName") as TextBox;
                        //        TextBox txtSpecification = gvData.Rows[i].Cells[4].FindControl("txtSpecification") as TextBox;
                        //        TextBox txtQuantity = gvData.Rows[i].Cells[5].FindControl("txtQuantity") as TextBox;
                        //        DropDownList ddUOM = gvData.Rows[i].Cells[6].FindControl("ddUOM") as DropDownList;
                        //        TextBox txtDeliveryConfirmationDate = gvData.Rows[i].Cells[7].FindControl("txtDeliveryConfirmationDate") as TextBox;

                        //        Button btnPONO = gvData.Rows[i].Cells[1].FindControl("btnPONO") as Button;
                        //        Button btnPRNO = gvData.Rows[i].Cells[2].FindControl("btnPRNO") as Button;
                        //        Button btnItemName = gvData.Rows[i].Cells[3].FindControl("btnItemName") as Button;
                        //        Button btnSpecification = gvData.Rows[i].Cells[4].FindControl("btnSpecification") as Button;


                        //        txtPONO.Enabled = false;
                        //        txtPRNO.Enabled = false;
                        //        txtItemName.Enabled = false;
                        //        txtSpecification.Enabled = false;
                        //        txtQuantity.Enabled = false;
                        //        txtDeliveryConfirmationDate.Enabled = false;
                        //        ddUOM.Enabled = false;

                        //        btnPONO.Enabled = false;
                        //        btnPRNO.Enabled = false;
                        //        btnItemName.Enabled = false;
                        //        btnSpecification.Enabled = false;
                        //    }
                        //}

                    }
                }

                if (Session["URF_ReqEntry_ReOpen"] != null)
                {
                    if (!string.IsNullOrEmpty(Session["URF_ReqEntry_ReOpen"].ToString()))
                    {
                        btnSubmit.Visible = false;
                        btnReOpen.Visible = true;

                        tblReOpenRemarks.Visible = true;
                        tblPurchasingRemarks.Style.Add("display", "none");
                        tblSupplierRemarks.Style.Add("display", "none");

                        Session["URF_ReqEntry_ReOpen"] = null;
                    }
                }

                // APPROVERS
                divApprover.Style.Add("display", "block");

                List<Entities_URF_RequestEntry> listApprover = new List<Entities_URF_RequestEntry>();
                Entities_URF_RequestEntry eApprover = new Entities_URF_RequestEntry();
                eApprover.RdCtrlNo = rfqno;
                listApprover = BLL.URF_TRANSACTION_GetRequestDetailsByCTRLNo(eApprover);

                if (listApprover != null)
                {
                    if (listApprover.Count > 0)
                    {
                        foreach (Entities_URF_RequestEntry eA in listApprover)
                        {
                            // PROD SEC MANAGER
                            if (eA.StatSTATProdSecManager == "-1")
                            {
                                lblSecManager.Text = "AUTO-APPROVED";
                            }
                            else
                            {
                                lblSecManager.Text = eA.StatProdSecManager;
                            }
                            // PROD DEPT MANAGER
                            if (eA.StatSTATProdDeptManager == "-1")
                            {
                                lblDeptManager.Text = "AUTO-APPROVED";
                            }
                            else
                            {
                                lblDeptManager.Text = eA.StatProdDeptManager;
                            }
                            // PROD DIV MANAGER
                            if (eA.StatSTATProdDivManager == "-1")
                            {
                                lblDivManager.Text = "AUTO-APPROVED";
                            }
                            else
                            {
                                lblDivManager.Text = eA.StatProdDivManager;
                            }
                            // PROD HQ MANAGER
                            if (eA.StatSTATProdHQManager == "-1")
                            {
                                lblHQManager.Text = "AUTO-APPROVED";
                            }
                            else
                            {
                                lblHQManager.Text = eA.StatProdHQManager;
                            }
                            
                                                        
                            lblBuyer.Text = eA.StatPurchasingBuyer;
                            lblPurchasingManager.Text = eA.StatPurchasingManager;

                            lblSecManagerStat.Text = eA.StatSTATProdSecManager;
                            lblDeptManagerStat.Text = eA.StatSTATProdDeptManager;
                            lblDivManagerStat.Text = eA.StatSTATProdDivManager;
                            lblHQManagerStat.Text = eA.StatSTATProdHQManager;
                            lblPurchasingBuyerStat.Text = eA.StatSTATPurchasingBuyer;
                            lblPurchasingManagerStat.Text = eA.StatSTATPurchasingManager;

                            lblRequester.Text = eA.RhRequester + "<br/>" + eA.RhTransactionDate;
                            lblDepartmentDivision.Text = eA.LcDepartment + "/" + eA.LcDivision;

                            lblBuyerSend.Text = eA.BuyerSend_SendBy + "<br/>" + eA.BuyerSend_SendReceivedDate;
                            lblSupplier.Text = eA.SupplierResponse_Date;

                            // SEND DATES
                            if (!string.IsNullOrEmpty(eA.SendDates))
                            {
                                var query = from val in eA.SendDates.Split(',')
                                            select val;

                                ddSendDates.Items.Clear();

                                foreach (string str in query)
                                {
                                    ddSendDates.Items.Add(str);
                                }
                            }


                            if (eA.StatSTATPurchasingManager == "1")
                            {
                                btnSubmit.Visible = false;
                                //if (!string.IsNullOrEmpty(Session["CategoryAccess"].ToString()) ||
                                //    Session["Username"].ToString() == "3844" ||
                                //    Session["Username"].ToString() == "6985" ||
                                //    Session["Username"].ToString() == "0286" ||
                                //    Session["Username"].ToString() == "1152" ||
                                //    Session["Username"].ToString() == "1402" ||
                                //    Session["Username"].ToString() == "002")
                                //{
                                //    btnSubmit.Visible = true;
                                //}
                                //else
                                //{
                                //    btnSubmit.Visible = false;
                                //}
                            }
                            else
                            {
                                btnSubmit.Visible = true;
                            }


                            // STATUS REQ MANAGER
                            if (eA.StatSTATProdSecManager == "1" || eA.StatSTATProdSecManager == "-1")
                            {
                                divSecManager.Style.Add("background-color", "#00C851");
                            }
                            if (eA.StatSTATProdSecManager == "2")
                            {
                                divSecManager.Style.Add("background-color", "#ffbb33");
                            }
                            if (eA.StatSTATProdSecManager == "0")
                            {
                                divSecManager.Style.Add("background-color", "#f44336");
                            }

                            // STATUS DEPT MANAGER
                            if (eA.StatSTATProdDeptManager == "1" || eA.StatSTATProdDeptManager == "-1")
                            {
                                divDeptManager.Style.Add("background-color", "#00C851");
                            }
                            if (eA.StatSTATProdDeptManager == "2")
                            {
                                divDeptManager.Style.Add("background-color", "#ffbb33");
                            }
                            if (eA.StatSTATProdDeptManager == "0")
                            {
                                divDeptManager.Style.Add("background-color", "#f44336");
                            }

                            // STATUS DIV MANAGER
                            if (eA.StatSTATProdDivManager == "1" || eA.StatSTATProdDivManager == "-1")
                            {
                                divDivManager.Style.Add("background-color", "#00C851");
                            }
                            if (eA.StatSTATProdDivManager == "2")
                            {
                                divDivManager.Style.Add("background-color", "#ffbb33");
                            }
                            if (eA.StatSTATProdDivManager == "0")
                            {
                                divDivManager.Style.Add("background-color", "#f44336");
                            }

                            // STATUS HQ MANAGER
                            if (eA.StatSTATProdHQManager == "1" || eA.StatSTATProdHQManager == "-1")
                            {
                                divHQManager.Style.Add("background-color", "#00C851");                                

                            }
                            if (eA.StatSTATProdHQManager == "2")
                            {
                                divHQManager.Style.Add("background-color", "#ffbb33");
                            }
                            if (eA.StatSTATProdHQManager == "0")
                            {
                                divHQManager.Style.Add("background-color", "#f44336");
                            }

                            // STATUS BUYER
                            if (eA.StatSTATPurchasingBuyer == "1")
                            {
                                divBuyer.Style.Add("background-color", "#00C851");
                            }
                            if (eA.StatSTATPurchasingBuyer == "2")
                            {
                                divBuyer.Style.Add("background-color", "#ffbb33");
                            }
                            if (eA.StatSTATPurchasingBuyer == "0")
                            {
                                divBuyer.Style.Add("background-color", "#f44336");
                            }

                            // STATUS PURCHASING MANAGER
                            if (eA.StatSTATPurchasingManager == "1")
                            {
                                divPurchasingManager.Style.Add("background-color", "#00C851");
                            }
                            if (eA.StatSTATPurchasingManager == "2")
                            {
                                divPurchasingManager.Style.Add("background-color", "#ffbb33");
                            }
                            if (eA.StatSTATPurchasingManager == "0")
                            {
                                divPurchasingManager.Style.Add("background-color", "#f44336");
                            }

                            // STATUS BUYER SEND
                            if (!string.IsNullOrEmpty(eA.BuyerSend_SendBy) || !string.IsNullOrEmpty(eA.BuyerSend_SendReceivedDate))
                            {
                                divBuyerSend.Style.Add("background-color", "#00C851");
                            }

                            // SUPPLIER RESPONSE
                            if (!string.IsNullOrEmpty(eA.SupplierResponse_Date))
                            {
                                divSupplier.Style.Add("background-color", "#00C851");
                            }

                            // POSTING REMARKS
                            if (!string.IsNullOrEmpty(eA.PostingRemarks))
                            {
                                txtBuyerCloseRemarks.Text = eA.PostingRemarks;
                                tableCloseRemarks.Style.Add("display", "block");
                            }

                            // REQUESTER ATTACHMENT
                            pdfSource = ConfigurationManager.AppSettings["URF_DL_REQUEST_ATTACHMENT_URL"] + rfqno + "/";

                            if (!string.IsNullOrEmpty(eA.RhAttachment1) || !string.IsNullOrEmpty(eA.RhAttachment2))
                            {
                                if (!string.IsNullOrEmpty(eA.RhAttachment1) && attachmentCounter.ToString() == "1")
                                {
                                    attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + eA.RhAttachment1 + "' height='500px' width='1213px'></iframe></td></tr></table>";
                                }
                                if (!string.IsNullOrEmpty(eA.RhAttachment2) && attachmentCounter.ToString() == "2")
                                {
                                    attachmentLiteralInside += "<table style='margin-bottom:3px; border:none;'><tr><td><iframe src='" + pdfSource + eA.RhAttachment2 + "' height='500px' width='1213px'></iframe></td></tr></table>";
                                }
                                
                            }

                            if (!string.IsNullOrEmpty(attachmentLiteralInside))
                            {
                                divAttachment.Style.Add("display", "block");
                                displayAttachment = attachmentLiteralInside;
                            }
                            else
                            {
                                divAttachment.Style.Add("display", "none");
                                displayAttachment = string.Empty;
                            }
                           

                            attachmentCounter++;

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

                List<Entities_URF_RequestEntry> listDropDown = new List<Entities_URF_RequestEntry>();
                listDropDown = BLL.URF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                if (listDropDown != null)
                {
                    if (listDropDown.Count > 0)
                    {
                        ddReason.Items.Add("");
                        ddCategory.Items.Add("");
                        ddSupplier.Items.Add("");

                        foreach (Entities_URF_RequestEntry entity in listDropDown)
                        {
                            ListItem item = new ListItem();
                            item.Text = entity.DropdownName.ToUpper();
                            item.Value = entity.DropdownRefId;

                            if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                            {
                                if (entity.TableName == "URF_MT_Reason")
                                {
                                    ddReason.Items.Add(item);
                                }
                                if (entity.TableName == "MT_Category")
                                {
                                    if (entity.DropdownRefId == "1013" || entity.DropdownRefId == "1006")
                                    {
                                        //DO NOT ADD THIS CATEGORY
                                    }
                                    else
                                    {
                                        ddCategory.Items.Add(item);
                                    }
                                }
                                if (entity.TableName == "MT_Supplier_Head")
                                {
                                    ddSupplier.Items.Add(item);
                                }
                            }

                        }

                    }
                }

                //---------------------------------------------------------------------------------------------------


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnReOpen_Click(object sender, EventArgs e)
        {
            try
            {
                bool isNoError = false;
                string successStatus = string.Empty;
                string queryStatus = string.Empty;
                string queryBeginPart = string.Empty;
                string queryEndPart = string.Empty;
                string urfno = CryptorEngine.Decrypt(Request.QueryString["URFNo_From_Inquiry"].ToString().Replace(" ", "+"), true);

                queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                queryStatus = "UPDATE URF_TRANSACTION_RequestStatus SET STATClosed = '2', ReOpenRemarks ='" + txtReOpenRemarks.Text.Replace("'", "''") + "' WHERE CTRLNo ='" + urfno.Trim() + "' ";

                successStatus = BLL.URF_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + queryStatus + queryEndPart).ToString();

                if (successStatus == "1")
                {
                    Session["successMessage"] = "CTRL NUMBER : <b>" + Page.Title.Trim() +"</b> HAS BEEN SUCCESSFULLY RE-OPEN.";
                    Session["successTransactionName"] = "URF_RequestInquiry";
                    Session["successReturnPage"] = "URF_RequestInquiry.aspx";

                    Response.Redirect("SuccessPage.aspx");
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
                string existingPO = string.Empty;
                string existingPOWithinTheRequest = string.Empty;
                string oldPO = string.Empty;

                if (String.IsNullOrEmpty(Request.QueryString["URFNo_From_Inquiry"].ToString()))
                {
                    // CHECK IF THERE IS EXISTING REQUEST (PER PO Number)

                    for (int i = 0; i < gvData.Rows.Count; i++)
                    {
                        TextBox txtPONO = (TextBox)gvData.Rows[i].Cells[1].FindControl("txtPONO");

                        if (!string.IsNullOrEmpty(txtPONO.Text))
                        {
                            List<Entities_URF_RequestEntry> listExistingRequestByPONumber = new List<Entities_URF_RequestEntry>();
                            Entities_URF_RequestEntry entityExisting = new Entities_URF_RequestEntry();
                            entityExisting.RdPONO = txtPONO.Text.Trim();

                            listExistingRequestByPONumber = BLL.URF_TRANSACTION_GetExistingRequestByPONumber_ApprovedRequest(entityExisting);

                            if (listExistingRequestByPONumber != null)
                            {
                                if (listExistingRequestByPONumber.Count > 0)
                                {
                                    foreach (Entities_URF_RequestEntry eExisting in listExistingRequestByPONumber)
                                    {
                                        if (eExisting.StatSTATProdDeptManager == "2" ||
                                            eExisting.StatSTATProdDivManager == "2" ||
                                            eExisting.StatSTATProdHQManager == "2" ||
                                            eExisting.StatSTATProdSecManager == "2" ||
                                            eExisting.StatSTATPurchasingBuyer == "2" ||
                                            eExisting.StatSTATPurchasingManager == "2")
                                        {
                                            // IF DISAPPROVED THEN DO NOTHING AND PROCEED THE TRANSACTION
                                        }
                                        else
                                        {
                                            existingPO += "CTRLNo (" + eExisting.RdCtrlNo + ") - PONumber(" + eExisting.RdPONO + ")<br />";

                                            txtPONO.BackColor = System.Drawing.Color.Red;
                                            txtPONO.ForeColor = System.Drawing.Color.White;
                                        }
                                    }
                                }
                            }
                        }

                    }

                }

                // GET ALL PO NUMBER
                for (int i = 0; i < gvData.Rows.Count; i++)
                {
                    TextBox txtPONO = (TextBox)gvData.Rows[i].Cells[1].FindControl("txtPONO");
                    if (!string.IsNullOrEmpty(txtPONO.Text))
                    {
                        oldPO += txtPONO.Text.ToUpper().Trim() + ",";
                    }
                }

                // CHECK IF THERE IS DUPLICATE PO
                List<string> completeItems = new List<string>(oldPO.Trim().Split(',').Select(t => t.Trim()));
                bool isUnique = completeItems.Count == completeItems.Distinct().Count();

                if (!isUnique)
                {
                    string duplicatePO = string.Empty;
                    string allPO = string.Empty;

                    for (int i = 0; i < gvData.Rows.Count; i++)
                    {
                        TextBox txtPONO = (TextBox)gvData.Rows[i].Cells[1].FindControl("txtPONO");
                        allPO += txtPONO.Text.Trim() + ",";
                    }

                    string[] poList = allPO.Split(',');
                    Array.Sort(poList);
                    List<string> duplicates = new List<string>();

                    for (int i = 1; i < poList.Length; i++)
                    {
                        if (poList[i] == poList[i - 1])
                        {

                            for (int x = 0; x < gvData.Rows.Count; x++)
                            {
                                TextBox txtPONO = (TextBox)gvData.Rows[x].Cells[1].FindControl("txtPONO");

                                if (txtPONO.Text.Trim() == poList[i].Trim())
                                {
                                    txtPONO.BackColor = System.Drawing.Color.Red;
                                    txtPONO.ForeColor = System.Drawing.Color.White;
                                }
                            }

                        }
                    }



                    lblSystemDetected.Text = "SYSTEM DETECTED EXISTING RECORD";
                    lblSystemCheck.Text = "*** System detected duplicate PO Number in your request. <br /> Please check items that is highlighted in RED color.";
                    lblExistingPONumber.Text = "";
                    btnSend.Visible = false;
                    btnCancel2.Text = "OK";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowRegistered", "showDialog();", true);

                } else if (!string.IsNullOrEmpty(existingPO))
                {
                    lblSystemDetected.Text = "SYSTEM DETECTED EXISTING RECORD";
                    lblSystemCheck.Text = "*** Please check below PO# with corresponding CTRL# OR items that is highlighted in RED color in all request form.";
                    lblExistingPONumber.Text = existingPO;
                    btnSend.Visible = false;
                    btnCancel2.Text = "OK";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowRegistered", "showDialog();", true);                    
                }
                else
                {
                    btnSend_Click(sender, e);
                }

            }            
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                bool isNoError = false;
                string successHead = string.Empty;
                string successDetails = string.Empty;
                string successStatus = string.Empty;
                int errorCounter = 0;

                for (int i = 0; i < gvData.Rows.Count; i++)
                {
                    TextBox txtPONO = (TextBox)gvData.Rows[i].Cells[1].FindControl("txtPONO");
                    TextBox txtPRNO = (TextBox)gvData.Rows[i].Cells[2].FindControl("txtPRNO");
                    TextBox txtItemName = (TextBox)gvData.Rows[i].Cells[3].FindControl("txtItemName");
                    TextBox txtSpecification = (TextBox)gvData.Rows[i].Cells[4].FindControl("txtSpecification");
                    TextBox txtQuantity = (TextBox)gvData.Rows[i].Cells[5].FindControl("txtQuantity");
                    DropDownList ddUOM = (DropDownList)gvData.Rows[i].Cells[6].FindControl("ddUOM");
                    TextBox txtDeliveryConfirmationDate = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtDeliveryConfirmationDate");
                    TextBox txtRequestedDeliveryDate = (TextBox)gvData.Rows[i].Cells[8].FindControl("txtRequestedDeliveryDate");

                    //if (string.IsNullOrEmpty(txtPONO.Text))
                    //{
                    //    txtPONO.Style.Add("background-color", "#f44336");
                    //    txtPRNO.Style.Add("background-color", "White");
                    //    txtItemName.Style.Add("background-color", "White");
                    //    txtSpecification.Style.Add("background-color", "White");
                    //    txtQuantity.Style.Add("background-color", "White");
                    //    ddUOM.Style.Add("background-color", "White");
                    //    txtDeliveryConfirmationDate.Style.Add("background-color", "White");
                    //    txtRequestedDeliveryDate.Style.Add("background-color", "White");
                    //    errorCounter++;
                    //}
                    if (string.IsNullOrEmpty(txtPRNO.Text))
                    {
                        txtPONO.Style.Add("background-color", "White");
                        txtPRNO.Style.Add("background-color", "#f44336");
                        txtItemName.Style.Add("background-color", "White");
                        txtSpecification.Style.Add("background-color", "White");
                        txtQuantity.Style.Add("background-color", "White");
                        ddUOM.Style.Add("background-color", "White");
                        txtDeliveryConfirmationDate.Style.Add("background-color", "White");
                        txtRequestedDeliveryDate.Style.Add("background-color", "White");
                        errorCounter++;
                    }
                    if (string.IsNullOrEmpty(txtItemName.Text))
                    {
                        txtPONO.Style.Add("background-color", "White");
                        txtPRNO.Style.Add("background-color", "White");
                        txtItemName.Style.Add("background-color", "#f44336");
                        txtSpecification.Style.Add("background-color", "White");
                        txtQuantity.Style.Add("background-color", "White");
                        ddUOM.Style.Add("background-color", "White");
                        txtDeliveryConfirmationDate.Style.Add("background-color", "White");
                        txtRequestedDeliveryDate.Style.Add("background-color", "White");
                        errorCounter++;
                    }
                    if (string.IsNullOrEmpty(txtSpecification.Text))
                    {
                        txtPONO.Style.Add("background-color", "White");
                        txtPRNO.Style.Add("background-color", "White");
                        txtItemName.Style.Add("background-color", "White");
                        txtSpecification.Style.Add("background-color", "#f44336");
                        txtQuantity.Style.Add("background-color", "White");
                        ddUOM.Style.Add("background-color", "White");
                        txtDeliveryConfirmationDate.Style.Add("background-color", "White");
                        txtRequestedDeliveryDate.Style.Add("background-color", "White");
                        errorCounter++;
                    }
                    if (String.IsNullOrEmpty(txtQuantity.Text.Trim()))
                    {
                        txtPONO.Style.Add("background-color", "White");
                        txtPRNO.Style.Add("background-color", "White");
                        txtItemName.Style.Add("background-color", "White");
                        txtSpecification.Style.Add("background-color", "White");
                        ddUOM.Style.Add("background-color", "White");
                        txtQuantity.Style.Add("background-color", "#f44336");
                        txtDeliveryConfirmationDate.Style.Add("background-color", "White");
                        txtRequestedDeliveryDate.Style.Add("background-color", "White");
                        errorCounter++;
                    }
                    if (String.IsNullOrEmpty(ddUOM.SelectedItem.Text))
                    {
                        txtPONO.Style.Add("background-color", "White");
                        txtPRNO.Style.Add("background-color", "White");
                        txtItemName.Style.Add("background-color", "White");
                        txtSpecification.Style.Add("background-color", "White");
                        txtQuantity.Style.Add("background-color", "White");
                        ddUOM.Style.Add("background-color", "#f44336");
                        txtDeliveryConfirmationDate.Style.Add("background-color", "White");
                        txtRequestedDeliveryDate.Style.Add("background-color", "White");
                        errorCounter++;
                    }
                    if (String.IsNullOrEmpty(txtDeliveryConfirmationDate.Text.Trim()))
                    {
                        txtPONO.Style.Add("background-color", "White");
                        txtPRNO.Style.Add("background-color", "White");
                        txtItemName.Style.Add("background-color", "White");
                        txtSpecification.Style.Add("background-color", "White");
                        ddUOM.Style.Add("background-color", "White");
                        txtQuantity.Style.Add("background-color", "White");
                        txtDeliveryConfirmationDate.Style.Add("background-color", "#f44336");
                        txtRequestedDeliveryDate.Style.Add("background-color", "White");
                        errorCounter++;
                    }
                    if (String.IsNullOrEmpty(txtRequestedDeliveryDate.Text.Trim()))
                    {
                        txtPONO.Style.Add("background-color", "White");
                        txtPRNO.Style.Add("background-color", "White");
                        txtItemName.Style.Add("background-color", "White");
                        txtSpecification.Style.Add("background-color", "White");
                        ddUOM.Style.Add("background-color", "White");
                        txtQuantity.Style.Add("background-color", "White");
                        txtDeliveryConfirmationDate.Style.Add("background-color", "White");
                        txtRequestedDeliveryDate.Style.Add("background-color", "#f44336");
                        errorCounter++;
                    }


                }

                if (ddReason.SelectedValue == "SPECIALREASON:")
                {
                    if (string.IsNullOrEmpty(txtOtherReason.Text))
                    {
                        errorCounter++;
                        txtOtherReason.Style.Add("background-color", "#f44336");
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('Please enter a valid Reason.');", true);
                    }
                }

                if (string.IsNullOrEmpty(Request.QueryString["URFNo_From_Inquiry"].ToString()))
                {
                    if (ddCategory.SelectedItem.Text.Trim() == "SUB-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "MAIN-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "CHEMICALS")
                    {
                        tblStockLife.Style.Add("display", "block");

                        if (string.IsNullOrEmpty(txtRepiStock.Text) || string.IsNullOrEmpty(txtDailyUsage.Text) || string.IsNullOrEmpty(txtStockLife.Text))
                        {
                            errorCounter++;
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('REPI STOCK, DAILY USAGE & STOCKLIFE (days) ARE REQUIRED FOR THIS CATEGORY.');", true);
                        }

                        //if (!fuStock.HasFile)
                        //{
                        //    errorCounter++;
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('STOCK LIFE IS REQUIRED FOR THIS CATEGORY. Please select a valid stock life (pdf file).');", true);
                        //}
                    }

                }

                if (errorCounter <= 0)
                {
                    string queryDetails = string.Empty;
                    string queryHead = string.Empty;
                    string queryStatus = string.Empty;
                    string queryApprover = string.Empty;
                    string attachmentFiles = string.Empty;
                    int rd_Query_Counter = 0;
                    string query_Success = string.Empty;
                    string temp_CTRLNo = string.Empty;

                    string queryBeginPart = string.Empty;
                    string queryEndPart = string.Empty;
                    int queryHeadCounter = 1;
                    int queryStatusCounter = 0;

                    queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";

                    if (!String.IsNullOrEmpty(Request.QueryString["URFNo_From_Inquiry"].ToString()))
                    {
                        temp_CTRLNo = CryptorEngine.Decrypt(Request.QueryString["URFNo_From_Inquiry"].ToString().Replace(" ", "+"), true);
                        if (string.IsNullOrEmpty(ddReason.SelectedValue))
                        {
                            if (ddCategory.SelectedItem.Text.Trim() == "SUB-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "MAIN-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "CHEMICALS")
                            {
                                queryHead = "UPDATE URF_TRANSACTION_RequestHead SET Category ='" + ddCategory.SelectedValue + "', Type ='" + ddType.SelectedValue +
                                            "', Supplier ='" + ddSupplier.SelectedValue + "', Attention ='" + txtAttention.Text + "', OtherReason ='" + txtOtherReason.Text +
                                            "', RepiStock='" + txtRepiStock.Text + "', DailyUsage='" + txtDailyUsage.Text + "', StockLife ='" + txtStockLife.Text +
                                            "' WHERE CTRLNo = '" + temp_CTRLNo + "' ";
                            }
                            else
                            {
                                queryHead = "UPDATE URF_TRANSACTION_RequestHead SET Category ='" + ddCategory.SelectedValue + "', Type ='" + ddType.SelectedValue +
                                            "', Supplier ='" + ddSupplier.SelectedValue + "', Attention ='" + txtAttention.Text + "', OtherReason ='" + txtOtherReason.Text +
                                            "' WHERE CTRLNo = '" + temp_CTRLNo + "' ";
                            }
                        }
                        else
                        {
                            if (ddCategory.SelectedItem.Text.Trim() == "SUB-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "MAIN-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "CHEMICALS")
                            {
                                if (ddReason.SelectedItem.Text.Trim() == "SPECIAL REASON:")
                                {
                                    queryHead = "UPDATE URF_TRANSACTION_RequestHead SET Category ='" + ddCategory.SelectedValue + "', Type ='" + ddType.SelectedValue +
                                                "', Supplier ='" + ddSupplier.SelectedValue + "', Attention ='" + txtAttention.Text + "', Reason ='" + ddReason.SelectedValue +
                                                "', OtherReason = '" + txtOtherReason.Text +
                                                "', RepiStock='" + txtRepiStock.Text + "', DailyUsage='" + txtDailyUsage.Text + "', StockLife ='" + txtStockLife.Text +
                                                "' WHERE CTRLNo = '" + temp_CTRLNo + "' ";
                                }
                                else
                                {
                                    queryHead = "UPDATE URF_TRANSACTION_RequestHead SET Category ='" + ddCategory.SelectedValue + "', Type ='" + ddType.SelectedValue +
                                                "', Supplier ='" + ddSupplier.SelectedValue + "', Attention ='" + txtAttention.Text + "', Reason ='" + ddReason.SelectedValue +
                                                "', RepiStock='" + txtRepiStock.Text + "', DailyUsage='" + txtDailyUsage.Text + "', StockLife ='" + txtStockLife.Text +
                                                "' WHERE CTRLNo = '" + temp_CTRLNo + "' ";
                                }
                            }
                            else
                            {
                                if (ddReason.SelectedItem.Text.Trim() == "SPECIAL REASON:")
                                {
                                    queryHead = "UPDATE URF_TRANSACTION_RequestHead SET Category ='" + ddCategory.SelectedValue + "', Type ='" + ddType.SelectedValue +
                                                "', Supplier ='" + ddSupplier.SelectedValue + "', Attention ='" + txtAttention.Text + "', Reason ='" + ddReason.SelectedValue +
                                                "', OtherReason = '" + txtOtherReason.Text +
                                                "' WHERE CTRLNo = '" + temp_CTRLNo + "' ";
                                }
                                else
                                {
                                    queryHead = "UPDATE URF_TRANSACTION_RequestHead SET Category ='" + ddCategory.SelectedValue + "', Type ='" + ddType.SelectedValue +
                                                "', Supplier ='" + ddSupplier.SelectedValue + "', Attention ='" + txtAttention.Text + "', Reason ='" + ddReason.SelectedValue +
                                                "' WHERE CTRLNo = '" + temp_CTRLNo + "' ";
                                }
                            }
                        }
                    }
                    else
                    {
                        temp_CTRLNo = setURFNumberWithPrefix();
                        queryStatusCounter = 1;

                        string attachment1 = string.Empty;
                        string attachment2 = string.Empty;
                        string attachmentStockLife = string.Empty;

                        string fileNameApplication1 = System.IO.Path.GetFileName(fu1.FileName);
                        string fileExtensionApplication1 = System.IO.Path.GetExtension(fileNameApplication1);
                        string newFile1 = fileNameApplication1;

                        string fileNameApplication2 = System.IO.Path.GetFileName(fu2.FileName);
                        string fileExtensionApplication2 = System.IO.Path.GetExtension(fileNameApplication2);
                        string newFile2 = fileNameApplication2;

                        string fileNameApplicationStockLife = System.IO.Path.GetFileName(fuStock.FileName);
                        string fileExtensionApplicationStockLife = System.IO.Path.GetExtension(fileNameApplicationStockLife);
                        string stockLife = fileNameApplicationStockLife;

                        if (fu1.HasFile)
                        {
                            if (!System.IO.Directory.Exists(Server.MapPath("~/URF_Request/" + temp_CTRLNo)))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/URF_Request/" + temp_CTRLNo));
                            }
                            if (!System.IO.File.Exists(Server.MapPath("~/URF_Request/" + temp_CTRLNo + "/" + newFile1)))
                            {
                                fu1.SaveAs(System.IO.Path.Combine(Server.MapPath("~/URF_Request/" + temp_CTRLNo), newFile1));
                                fu1.Dispose();
                                System.IO.File.Copy(System.IO.Path.Combine(Server.MapPath("~/URF_Request/" + temp_CTRLNo), newFile1), System.IO.Path.Combine(Server.MapPath("~/URF_Request/" + temp_CTRLNo), ("1-" + temp_CTRLNo + fileExtensionApplication1)), true);
                                System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath("~/URF_Request/" + temp_CTRLNo), newFile1));
                            }
                            attachment1 = "1-" + temp_CTRLNo + fileExtensionApplication1;
                        }
                        if (fu2.HasFile)
                        {
                            if (!System.IO.Directory.Exists(Server.MapPath("~/URF_Request/" + temp_CTRLNo)))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/URF_Request/" + temp_CTRLNo));
                            }
                            if (!System.IO.File.Exists(Server.MapPath("~/URF_Request/" + temp_CTRLNo + "/" + newFile2)))
                            {
                                fu2.SaveAs(System.IO.Path.Combine(Server.MapPath("~/URF_Request/" + temp_CTRLNo), newFile2));
                                fu2.Dispose();
                                System.IO.File.Copy(System.IO.Path.Combine(Server.MapPath("~/URF_Request/" + temp_CTRLNo), newFile2), System.IO.Path.Combine(Server.MapPath("~/URF_Request/" + temp_CTRLNo), ("2-" + temp_CTRLNo + fileExtensionApplication2)), true);
                                System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath("~/URF_Request/" + temp_CTRLNo), newFile2));
                            }
                            attachment2 = "2-" + temp_CTRLNo + fileExtensionApplication2;
                        }
                        // NO ATTACHMENT
                        if (!fu1.HasFile && !fu2.HasFile)
                        {
                            if (!System.IO.Directory.Exists(Server.MapPath("~/URF_Request/" + temp_CTRLNo)))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/URF_Request/" + temp_CTRLNo));
                            }
                        }

                        // STOCK LIFE
                        if (fuStock.HasFile)
                        {
                            if (!System.IO.Directory.Exists(Server.MapPath("~/URF_Request/" + temp_CTRLNo)))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/URF_Request/" + temp_CTRLNo));
                            }
                            if (!System.IO.File.Exists(Server.MapPath("~/URF_Request/" + temp_CTRLNo + "/" + stockLife)))
                            {
                                fu1.SaveAs(System.IO.Path.Combine(Server.MapPath("~/URF_Request/" + temp_CTRLNo), stockLife));
                                fu1.Dispose();
                                System.IO.File.Copy(System.IO.Path.Combine(Server.MapPath("~/URF_Request/" + temp_CTRLNo), stockLife), System.IO.Path.Combine(Server.MapPath("~/URF_Request/" + temp_CTRLNo), ("SL-" + temp_CTRLNo + fileExtensionApplicationStockLife)), true);
                                System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath("~/URF_Request/" + temp_CTRLNo), stockLife));
                            }
                            attachmentStockLife = "SL-" + temp_CTRLNo + fileExtensionApplicationStockLife;
                        }

                        if (string.IsNullOrEmpty(ddReason.SelectedValue))
                        {
                            if (ddCategory.SelectedItem.Text.Trim() == "SUB-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "MAIN-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "CHEMICALS")
                            {
                                queryHead = "INSERT INTO URF_TRANSACTION_RequestHead (CTRLNo,Requester,Category,Type,Supplier,Attention,Reason,OtherReason,Attachment1,Attachment2,TransactionDate,StockLifeAttachment,RepiStock,DailyUsage,StockLife) " +
                                            "VALUES ('" + temp_CTRLNo + "','" + Session["LcRefId"].ToString() + "','" + ddCategory.SelectedValue +
                                            "','" + ddType.SelectedValue + "','" + ddSupplier.SelectedValue + "','" + txtAttention.Text + "','0','" + txtOtherReason.Text + "','" + attachment1 + "','" + attachment2 + "',GETDATE(),'" + attachmentStockLife + "','" + txtRepiStock.Text + "','" + txtDailyUsage.Text + "','" + txtStockLife.Text + "') ";

                            }
                            else
                            {
                                queryHead = "INSERT INTO URF_TRANSACTION_RequestHead (CTRLNo,Requester,Category,Type,Supplier,Attention,Reason,OtherReason,Attachment1,Attachment2,TransactionDate,StockLifeAttachment) " +
                                            "VALUES ('" + temp_CTRLNo + "','" + Session["LcRefId"].ToString() + "','" + ddCategory.SelectedValue +
                                            "','" + ddType.SelectedValue + "','" + ddSupplier.SelectedValue + "','" + txtAttention.Text + "','0','" + txtOtherReason.Text + "','" + attachment1 + "','" + attachment2 + "',GETDATE(),'" + attachmentStockLife + "') ";
                            }
                        }
                        else
                        {
                            if (ddCategory.SelectedItem.Text.Trim() == "SUB-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "MAIN-MATERIALS" || ddCategory.SelectedItem.Text.Trim() == "CHEMICALS")
                            {
                                if (ddReason.SelectedItem.Text.Trim() == "SPECIAL REASON:")
                                {
                                    queryHead = "INSERT INTO URF_TRANSACTION_RequestHead (CTRLNo,Requester,Category,Type,Supplier,Attention,Reason,OtherReason,Attachment1,Attachment2,TransactionDate,StockLifeAttachment,RepiStock,DailyUsage,StockLife) " +
                                                "VALUES ('" + temp_CTRLNo + "','" + Session["LcRefId"].ToString() + "','" + ddCategory.SelectedValue +
                                                "','" + ddType.SelectedValue + "','" + ddSupplier.SelectedValue + "','" + txtAttention.Text + "','" + ddReason.SelectedValue + "','" + txtOtherReason.Text + "','" + attachment1 + "','" + attachment2 + "',GETDATE(),'" + attachmentStockLife + "','" + txtRepiStock.Text + "','" + txtDailyUsage.Text + "','" + txtStockLife.Text + "') ";
                                }
                                else
                                {
                                    queryHead = "INSERT INTO URF_TRANSACTION_RequestHead (CTRLNo,Requester,Category,Type,Supplier,Attention,Reason,Attachment1,Attachment2,TransactionDate,StockLifeAttachment,RepiStock,DailyUsage,StockLife) " +
                                                "VALUES ('" + temp_CTRLNo + "','" + Session["LcRefId"].ToString() + "','" + ddCategory.SelectedValue +
                                                "','" + ddType.SelectedValue + "','" + ddSupplier.SelectedValue + "','" + txtAttention.Text + "','" + ddReason.SelectedValue + "','" + attachment1 + "','" + attachment2 + "',GETDATE(),'" + attachmentStockLife + "','" + txtRepiStock.Text + "','" + txtDailyUsage.Text + "','" + txtStockLife.Text + "') ";
                                }
                            }
                            else
                            {
                                if (ddReason.SelectedItem.Text.Trim() == "SPECIAL REASON:")
                                {
                                    queryHead = "INSERT INTO URF_TRANSACTION_RequestHead (CTRLNo,Requester,Category,Type,Supplier,Attention,Reason,OtherReason,Attachment1,Attachment2,TransactionDate,StockLifeAttachment) " +
                                                "VALUES ('" + temp_CTRLNo + "','" + Session["LcRefId"].ToString() + "','" + ddCategory.SelectedValue +
                                                "','" + ddType.SelectedValue + "','" + ddSupplier.SelectedValue + "','" + txtAttention.Text + "','" + ddReason.SelectedValue + "','" + txtOtherReason.Text + "','" + attachment1 + "','" + attachment2 + "',GETDATE(),'" + attachmentStockLife + "') ";
                                }
                                else
                                {
                                    queryHead = "INSERT INTO URF_TRANSACTION_RequestHead (CTRLNo,Requester,Category,Type,Supplier,Attention,Reason,Attachment1,Attachment2,TransactionDate,StockLifeAttachment) " +
                                                "VALUES ('" + temp_CTRLNo + "','" + Session["LcRefId"].ToString() + "','" + ddCategory.SelectedValue +
                                                "','" + ddType.SelectedValue + "','" + ddSupplier.SelectedValue + "','" + txtAttention.Text + "','" + ddReason.SelectedValue + "','" + attachment1 + "','" + attachment2 + "',GETDATE(),'" + attachmentStockLife + "') ";
                                }
                            }
                        }

                        //queryStatus = "INSERT INTO URF_TRANSACTION_RequestStatus (CTRLNo) " +
                        //              "VALUES ('" + temp_CTRLNo + "') ";


                        // UPDATE APPROVER
                        List<Entities_URF_RequestEntry> listApprover = new List<Entities_URF_RequestEntry>();
                        listApprover = BLL.URF_TRANSACTION_CheckApprover(Session["LcRefId"].ToString());

                        if (listApprover != null)
                        {
                            if (listApprover.Count > 0)
                            {
                                foreach (Entities_URF_RequestEntry entityApprover in listApprover)
                                {
                                    // STATUS TABLE
                                    string sectionManager = string.IsNullOrEmpty(entityApprover.SecManagerApprover) ? "-1" : string.Empty;
                                    string departmetManager = string.IsNullOrEmpty(entityApprover.DeptManagerApprover) ? "-1" : string.Empty;
                                    string divisionManager = string.IsNullOrEmpty(entityApprover.DivManagerApprover) ? "-1" : string.Empty;
                                    string hqManager = string.IsNullOrEmpty(entityApprover.HqManagerApprover) ? "-1" : string.Empty;
                                    queryStatus = "INSERT INTO URF_TRANSACTION_RequestStatus (CTRLNo,STATProdSecManager,STATProdDeptManager,STATProdDivManager,STATProdHQManager) " +
                                                  "VALUES ('" + temp_CTRLNo + "','" + sectionManager + "','" + departmetManager + "','" + divisionManager + "','" + hqManager + "') ";
                                }
                            }
                        }


                    }

                    for (int i = 0; i < gvData.Rows.Count; i++)
                    {
                        TextBox txtPONO = (TextBox)gvData.Rows[i].Cells[1].FindControl("txtPONO");
                        TextBox txtPRNO = (TextBox)gvData.Rows[i].Cells[2].FindControl("txtPRNO");
                        TextBox txtItemName = (TextBox)gvData.Rows[i].Cells[3].FindControl("txtItemName");
                        TextBox txtSpecification = (TextBox)gvData.Rows[i].Cells[4].FindControl("txtSpecification");
                        TextBox txtQuantity = (TextBox)gvData.Rows[i].Cells[5].FindControl("txtQuantity");
                        DropDownList ddUOM = (DropDownList)gvData.Rows[i].Cells[6].FindControl("ddUOM");
                        TextBox txtDeliveryConfirmationDate = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtDeliveryConfirmationDate");
                        TextBox txtRequestedDeliveryDate = (TextBox)gvData.Rows[i].Cells[8].FindControl("txtRequestedDeliveryDate");
                        TextBox txtReplyDeliveryDate = (TextBox)gvData.Rows[i].Cells[9].FindControl("txtReplyDeliveryDate");
                        Label lblRefId = (Label)gvData.Rows[i].Cells[1].FindControl("lblRefId");

                        if (!String.IsNullOrEmpty(Request.QueryString["URFNo_From_Inquiry"].ToString()))
                        {
                            queryDetails += "UPDATE URF_TRANSACTION_RequestDetails SET PONO ='" + txtPONO.Text.Replace("'", "''") + "', PRNO = '" + txtPRNO.Text.Replace("'", "''") +
                                            "', ItemName = '" + txtItemName.Text.Replace("'", "''") + "', Specs = '" + txtSpecification.Text.Trim() + "', UnitOfMeasure = '" + ddUOM.SelectedValue +
                                            "', Quantity = '" + txtQuantity.Text.Replace("'", "''") + "', DeliveryConfirmationDate = '" + txtDeliveryConfirmationDate.Text + "', RequestedDeliveryDate = '" + txtRequestedDeliveryDate.Text + "' WHERE CTRLNo = '" + temp_CTRLNo + "' AND RefId ='" + lblRefId.Text.Trim() + "' ";
                        }
                        else
                        {
                            queryDetails += "INSERT INTO URF_TRANSACTION_RequestDetails (CTRLNo,PONO,PRNO,ItemName,Specs,Quantity,UnitOfMeasure,DeliveryConfirmationDate,RequestedDeliveryDate) " +
                                            "VALUES ('" + temp_CTRLNo + "','" + txtPONO.Text.Replace("'", "''") + "','" + txtPRNO.Text.Replace("'", "''") +
                                            "','" + txtItemName.Text.Replace("'", "''") + "','" + txtSpecification.Text.Trim() + "','" + txtQuantity.Text.Replace("'", "''").Trim() + "','" + ddUOM.SelectedValue + "','" + txtDeliveryConfirmationDate.Text.Replace("'", "''") + "','" + txtRequestedDeliveryDate.Text.Replace("'", "''") + "') ";
                        }

                        rd_Query_Counter++;
                    }


                    queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";

                    query_Success = BLL.URF_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + queryHead + queryStatus + queryDetails + queryEndPart).ToString();

                    if (query_Success == (rd_Query_Counter + queryHeadCounter + queryStatusCounter).ToString())
                    {
                        Session["successMessage"] = "CTRL NUMBER : <b>" + temp_CTRLNo + "</b> HAS BEEN SUCCESSFULLY SAVED.";
                        Session["successTransactionName"] = "URF_REQUESTENTRY";
                        Session["successReturnPage"] = "URF_RequestEntry.aspx?URFNo_From_Inquiry=";


                        // SEND EMAIL NOTIFICATION TO SECTION MANAGER FOR APPROVAL
                        List<Entities_Common_ForApproval> listSecManagerNotification = new List<Entities_Common_ForApproval>();
                        listSecManagerNotification = BLL_COMMON.Common_GetForApprovals().Where(itm => itm.Approval_Department == Session["Department"].ToString() && !string.IsNullOrEmpty(itm.Approval_URF_ForApproval_ProdSecManager)).ToList();

                        string name = string.Empty;
                        string noti = string.Empty;
                        string items = string.Empty;
                        string autogenerated = string.Empty;

                        if (listSecManagerNotification != null)
                        {
                            if (listSecManagerNotification.Count > 0)
                            {
                                foreach (Entities_Common_ForApproval secManager in listSecManagerNotification)
                                {
                                    if (int.Parse(secManager.Approval_URF_ForApproval_ProdSecManager) > 0)
                                    {
                                        // SEND NOTIFICATION
                                        if (!string.IsNullOrEmpty(secManager.Approval_EmailAddress))
                                        {
                                            //--------------------------------------------------------------------------------------------

                                            autogenerated = "<br/>To view your for approval(s), go to <a href='http://10.27.1.170:9292/Default.aspx'>http://10.27.1.170:9292/Default.aspx</a><br/><br/> Thank you! <br/><br/> *** This is an automatically generated email. Please do not reply ***";
                                            noti = "<p style='font-size:22px;'><b>NOTIFICATION APPROVAL</b></p><br/><br/>";
                                            name = "Hi <b>" + CryptorEngine.Decrypt(secManager.Approval_Fullname, true) + "</b> Good Day! <br/> Please check below request item(s) for your approval. <br/><br/>";

                                            items = "URGENT REQUEST FORM (URF) FOR PROD. SECTION MANAGER APPROVAL - <a href='http://10.27.1.170:9292/URF_ApprovalForm.aspx'>" + secManager.Approval_URF_ForApproval_ProdSecManager + "</a><br/>";

                                            COMMON.sendEmailTo_CRF_DRF_URF_Approvers(secManager.Approval_EmailAddress, ConfigurationManager.AppSettings["email-username"].ToString(), "PUR_SOFRA_Notifications", noti + name + items + autogenerated);

                                            // Clear variables for next iteration
                                            name = string.Empty;
                                            noti = string.Empty;
                                            items = string.Empty;
                                            autogenerated = string.Empty;

                                            //--------------------------------------------------------------------------------------------
                                        }

                                    }
                                }
                            }
                        }


                        Response.Redirect("SuccessPage.aspx", false);
                    }




                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        private string setURFNumberWithPrefix()
        {
            string retVal = string.Empty;

            retVal = "URF_" + Session["DivisionCode"].ToString() + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + COMMON.controlNoZeroPrefix(setControlNumber().ToString().Length.ToString()) + setControlNumber().ToString();

            return retVal;
        }
        private Int32 setControlNumber()
        {
            return BLL.URF_TRANSACTION_CountRequestHead(DateTime.Now.Year.ToString()) + 1;
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (gvData.Rows.Count > 0)
                {
                    if (gvData.Rows.Count > 1)
                    {
                        URF_REPORT_MULTIPLE();
                    }
                    else
                    {
                        URF_REPORT_SINGLE();
                    }
                }
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        private void URF_REPORT_SINGLE()
        {
            string pathLocation = Server.MapPath("~/URF_Request/" + lblCtrlNo.Text.Trim() + "/REPORT_" + lblCtrlNo.Text.Trim() + ".html");
            string htmlTemplate = Server.MapPath("~/HTML_Report/URF/URF_Single.txt");
            string tableHeader = string.Empty;
            string tableDetails = string.Empty;
            string table = string.Empty;
            int recCounter = 0;
            string stat_SecManager = string.Empty;
            string stat_DeptManager = string.Empty;
            string stat_DivManager = string.Empty;
            string stat_HqManager = string.Empty;
            string stat_PurchasingBuyer = string.Empty;
            string stat_PurchasingManager = string.Empty;

            string po_no = string.Empty;
            string pr_no = string.Empty;
            string description = string.Empty;
            string specification = string.Empty;
            string quantity = string.Empty;
            string division_department = string.Empty;
            string reason = string.Empty;
            string delivery_confirmation = string.Empty;
            string request_deliverydate = string.Empty;
            string reply_deliverydate = string.Empty;


            List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();
            Entities_URF_RequestEntry entity = new Entities_URF_RequestEntry();

            entity.RdCtrlNo = lblCtrlNo.Text.ToString();
            list = BLL.URF_TRANSACTION_GetRequestDetailsByCTRLNo(entity);

            if (list != null)
            {
                if (list.Count > 0)
                {
                    //tableHeader = "<tr><td>REFID</td><td>PONO</td><td>PRNO</td><td>ITEM NAME</td><td>SPECIFICATION</td><td>QUANTITY</td><td>UNIT OF MEASURE</td><td>DEL.CONF.DATE</td><td>REQ.DEL.DATE</td><td>REPLY DELIVERY DATE</td></tr>";
                    foreach (Entities_URF_RequestEntry eCSV in list)
                    {
                        po_no = eCSV.RdPONO;
                        pr_no = eCSV.RdPRNO;
                        description = eCSV.RdItemName;
                        specification = eCSV.RdSpecs;
                        quantity = eCSV.RdQuantity + " " + eCSV.RdUOMDesc;
                        delivery_confirmation = eCSV.RdDeliveryConfirmationDate;
                        request_deliverydate = eCSV.RdRequestedDeliveryDate;
                        reply_deliverydate = eCSV.RdReplyDeliveryDate;

                        //recCounter++;
                        //// TABLE CREATION
                        //tableDetails += "<tr><td>" + eCSV.RdRefId + "</td><td>" + eCSV.RdPONO + "</td><td>" + eCSV.RdPRNO + "</td><td>" + eCSV.RdItemName + "</td><td>" + eCSV.RdSpecs + "</td><td>" + eCSV.RdQuantity + "</td><td>" + eCSV.RdUOMDesc + "</td><td>" + eCSV.RdDeliveryConfirmationDate + "</td><td>" + eCSV.RdRequestedDeliveryDate + "</td><td>" + eCSV.RdReplyDeliveryDate + "</td></tr>";
                    }

                    //table = "<table border='1' style='width: 90%; border-collapse: collapse; margin-left: auto; margin-right: auto; height: 94px;'><tbody>" + tableHeader + tableDetails + "</tbody></table>";
                }
            }

            //-------------------------------------------------------------
            if (lblSecManagerStat.Text == "1")
            {
                stat_SecManager = "#D5FBC5";
            }
            else if (lblSecManagerStat.Text == "2")
            {
                stat_SecManager = "#FCBBC1";
            }
            else
            {
                stat_SecManager = "white";
            }
            //-------------------------------------------------------------
            if (lblDeptManagerStat.Text == "1")
            {
                stat_DeptManager = "#D5FBC5";
            }
            else if (lblDeptManagerStat.Text == "2")
            {
                stat_DeptManager = "#FCBBC1";
            }
            else
            {
                stat_DeptManager = "white";
            }
            //-------------------------------------------------------------
            if (lblDivManagerStat.Text == "1")
            {
                stat_DivManager = "#D5FBC5";
            }
            else if (lblDivManagerStat.Text == "2")
            {
                stat_DivManager = "#FCBBC1";
            }
            else
            {
                stat_DivManager = "white";
            }
            //-------------------------------------------------------------
            if (lblHQManagerStat.Text == "1")
            {
                stat_HqManager = "#D5FBC5";
            }
            else if (lblHQManagerStat.Text == "2")
            {
                stat_HqManager = "#FCBBC1";
            }
            else
            {
                stat_HqManager = "white";
            }
            //-------------------------------------------------------------
            if (lblPurchasingBuyerStat.Text == "1")
            {
                stat_PurchasingBuyer = "#D5FBC5";
            }
            else if (lblPurchasingBuyerStat.Text == "2")
            {
                stat_PurchasingBuyer = "#FCBBC1";
            }
            else
            {
                stat_PurchasingBuyer = "white";
            }
            //-------------------------------------------------------------
            if (lblPurchasingManagerStat.Text == "1")
            {
                stat_PurchasingManager = "#D5FBC5";
            }
            else if (lblPurchasingManagerStat.Text == "2")
            {
                stat_PurchasingManager = "#FCBBC1";
            }
            else
            {
                stat_PurchasingManager = "white";
            }
            //-------------------------------------------------------------

            if (!string.IsNullOrEmpty(txtOtherReason.Text))
            {
                reason = txtOtherReason.Text;
            }
            else
            {
                reason = ddReason.SelectedItem.Text;
            }

            if (System.IO.File.Exists(htmlTemplate))
            {
                string templateValue = System.IO.File.ReadAllText(htmlTemplate).Replace("val_ctrlno", lblCtrlNo.Text.ToString())
                                                                               .Replace("val_attention", txtAttention.Text)
                                                                               .Replace("val_supplier", ddSupplier.SelectedItem.Text)
                                                                               .Replace("val_reason", reason)
                                                                               .Replace("val_otherreason", txtOtherReason.Text)
                                                                               .Replace("val_category", ddCategory.SelectedItem.Text)
                                                                               .Replace("val_type", ddType.SelectedItem.Text)
                                                                               .Replace("val_preparedby", lblRequester.Text)
                                                                               .Replace("val_secmanager", lblSecManager.Text)
                                                                               .Replace("val_deptmanager", lblDeptManager.Text)
                                                                               .Replace("val_divmanager", lblDivManager.Text)
                                                                               .Replace("val_hqmanager", lblHQManager.Text)

                                                                               .Replace("bg_preparedby", "background-color:white;")
                                                                               .Replace("bg_secmanager", "background-color:" + stat_SecManager + ";")
                                                                               .Replace("bg_deptmanager", "background-color:" + stat_DeptManager + ";")
                                                                               .Replace("bg_divmanager", "background-color:" + stat_DivManager + ";")
                                                                               .Replace("bg_hqmanager", "background-color:" + stat_DivManager + ";")
                                                                               .Replace("bg_buyer", "background-color:" + stat_PurchasingBuyer + ";")
                                                                               .Replace("bg_purmanager", "background-color:" + stat_PurchasingManager + ";")

                                                                               .Replace("val_pono", po_no)
                                                                               .Replace("val_prno", pr_no)
                                                                               .Replace("val_description", description)
                                                                               .Replace("val_specification", specification)
                                                                               .Replace("val_quantity", quantity)
                                                                               .Replace("val_reason", ddReason.SelectedItem.Text)
                                                                               .Replace("val_deliveryconfirmationdate", delivery_confirmation)
                                                                               .Replace("val_urgentrequestdeliverydate", request_deliverydate)
                                                                               .Replace("val_divisiondept", lblDepartmentDivision.Text)

                                                                               .Replace("val_incharge", string.Empty)
                                                                               .Replace("val_delsched", string.Empty)
                                                                               .Replace("val_etdorigin", string.Empty)
                                                                               .Replace("val_etarepi", string.Empty)

                                                                               .Replace("val_dailyusage", txtDailyUsage.Text)
                                                                               .Replace("val_stocklife", txtStockLife.Text)
                                                                               .Replace("val_repistock", txtRepiStock.Text)

                                                                               .Replace("val_buyer", lblBuyer.Text)
                                                                               .Replace("val_purmanager", lblPurchasingManager.Text)
                                                                               .Replace("val_replydeliverydate", reply_deliverydate)
                                                                               .Replace("val_details", table);


                if (!System.IO.File.Exists(pathLocation))
                {
                    using (StreamWriter writer = new StreamWriter(new FileStream(pathLocation, FileMode.Create, FileAccess.Write)))
                    {
                        writer.WriteLine(templateValue);
                    }
                }
                else
                {
                    System.IO.File.Delete(pathLocation);

                    if (!System.IO.File.Exists(pathLocation))
                    {
                        using (StreamWriter writer = new StreamWriter(new FileStream(pathLocation, FileMode.Create, FileAccess.Write)))
                        {
                            writer.WriteLine(templateValue);
                        }
                    }
                }
            }

            Response.Redirect("URF_Request/" + lblCtrlNo.Text.Trim() + "/REPORT_" + lblCtrlNo.Text.Trim() + ".html", false);
        }

        private void URF_REPORT_MULTIPLE()
        {
            string pathLocation = Server.MapPath("~/URF_Request/" + lblCtrlNo.Text.Trim() + "/REPORT_" + lblCtrlNo.Text.Trim() + ".html");
            string htmlTemplate = Server.MapPath("~/HTML_Report/URF/URF_Multiple.txt");
            string tableHeader = string.Empty;
            string tableDetails = string.Empty;
            string table = string.Empty;
            int recCounter = 0;
            string stat_SecManager = string.Empty;
            string stat_DeptManager = string.Empty;
            string stat_DivManager = string.Empty;
            string stat_HqManager = string.Empty;
            string stat_PurchasingBuyer = string.Empty;
            string stat_PurchasingManager = string.Empty;
            string reason = string.Empty;

            List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();
            Entities_URF_RequestEntry entity = new Entities_URF_RequestEntry();

            entity.RdCtrlNo = lblCtrlNo.Text.ToString();
            list = BLL.URF_TRANSACTION_GetRequestDetailsByCTRLNo(entity);

            if (list != null)
            {
                if (list.Count > 0)
                {
                    tableHeader = "<tr><td>REFID</td><td>PONO</td><td>PRNO</td><td>ITEM NAME</td><td>SPECIFICATION</td><td>QUANTITY</td><td>UNIT OF MEASURE</td><td>PO DATE</td><td>REQ.DEL.DATE</td><td>REPLY DELIVERY DATE</td></tr>";
                    foreach (Entities_URF_RequestEntry eCSV in list)
                    {
                        recCounter++;
                        // TABLE CREATION
                        tableDetails += "<tr><td>" + recCounter+ "</td><td>" + eCSV.RdPONO + "</td><td>" + eCSV.RdPRNO + "</td><td>" + eCSV.RdItemName + "</td><td>" + eCSV.RdSpecs + "</td><td>" + eCSV.RdQuantity + "</td><td>" + eCSV.RdUOMDesc + "</td><td>" + eCSV.RdDeliveryConfirmationDate + "</td><td>" + eCSV.RdRequestedDeliveryDate + "</td><td>" + eCSV.RdReplyDeliveryDate + "</td></tr>";
                    }

                    table = "<table border='1' style='width: 100%; border-collapse: collapse; margin-left: auto; margin-right: auto; height: 94px; font-size:10px;'><tbody>" + tableHeader + tableDetails + "</tbody></table>";
                }
            }

            //-------------------------------------------------------------
            if (lblSecManagerStat.Text == "1")
            {
                stat_SecManager = "#D5FBC5";
            }
            else if (lblSecManagerStat.Text == "2")
            {
                stat_SecManager = "#FCBBC1";
            }
            else
            {
                stat_SecManager = "white";
            }
            //-------------------------------------------------------------
            if (lblDeptManagerStat.Text == "1")
            {
                stat_DeptManager = "#D5FBC5";
            }
            else if (lblDeptManagerStat.Text == "2")
            {
                stat_DeptManager = "#FCBBC1";
            }
            else
            {
                stat_DeptManager = "white";
            }
            //-------------------------------------------------------------
            if (lblDivManagerStat.Text == "1")
            {
                stat_DivManager = "#D5FBC5";
            }
            else if (lblDivManagerStat.Text == "2")
            {
                stat_DivManager = "#FCBBC1";
            }
            else
            {
                stat_DivManager = "white";
            }
            //-------------------------------------------------------------
            if (lblHQManagerStat.Text == "1")
            {
                stat_HqManager = "#D5FBC5";
            }
            else if (lblHQManagerStat.Text == "2")
            {
                stat_HqManager = "#FCBBC1";
            }
            else
            {
                stat_HqManager = "white";
            }
            //-------------------------------------------------------------
            if (lblPurchasingBuyerStat.Text == "1")
            {
                stat_PurchasingBuyer = "#D5FBC5";
            }
            else if (lblPurchasingBuyerStat.Text == "2")
            {
                stat_PurchasingBuyer = "#FCBBC1";
            }
            else
            {
                stat_PurchasingBuyer = "white";
            }
            //-------------------------------------------------------------
            if (lblPurchasingManagerStat.Text == "1")
            {
                stat_PurchasingManager = "#D5FBC5";
            }
            else if (lblPurchasingManagerStat.Text == "2")
            {
                stat_PurchasingManager = "#FCBBC1";
            }
            else
            {
                stat_PurchasingManager = "white";
            }
            //-------------------------------------------------------------

            if (!string.IsNullOrEmpty(txtOtherReason.Text))
            {
                reason = txtOtherReason.Text;
            }
            else
            {
                reason = ddReason.SelectedItem.Text;
            }

            if (System.IO.File.Exists(htmlTemplate))
            {
                string templateValue = System.IO.File.ReadAllText(htmlTemplate).Replace("val_ctrlno", lblCtrlNo.Text.ToString())
                                                                               .Replace("val_attention", txtAttention.Text)
                                                                               .Replace("val_supplier", ddSupplier.SelectedItem.Text)
                                                                               .Replace("val_reason", reason)
                                                                               .Replace("val_otherreason", txtOtherReason.Text)
                                                                               .Replace("val_category", ddCategory.SelectedItem.Text)
                                                                               .Replace("val_type", ddType.SelectedItem.Text)
                                                                               .Replace("val_preparedby", lblSecManager.Text)
                                                                               .Replace("val_secmanager", lblSecManager.Text)
                                                                               .Replace("val_deptmanager", lblDeptManager.Text)
                                                                               .Replace("val_divmanager", lblDivManager.Text)
                                                                               .Replace("val_hqmanager", lblHQManager.Text)

                                                                               .Replace("bg_preparedby", "background-color:white;")
                                                                               .Replace("bg_secmanager", "background-color:" + stat_SecManager + ";")
                                                                               .Replace("bg_deptmanager", "background-color:" + stat_DeptManager + ";")
                                                                               .Replace("bg_divmanager", "background-color:" + stat_DivManager + ";")
                                                                               .Replace("bg_hqmanager", "background-color:" + stat_DivManager + ";")
                                                                               .Replace("bg_buyer", "background-color:" + stat_PurchasingBuyer + ";")
                                                                               .Replace("bg_purmanager", "background-color:" + stat_PurchasingManager + ";")

                                                                               .Replace("val_incharge", string.Empty)
                                                                               .Replace("val_delsched", string.Empty)
                                                                               .Replace("val_etdorigin", string.Empty)
                                                                               .Replace("val_etarepi", string.Empty)

                                                                               .Replace("val_dailyusage", txtDailyUsage.Text)
                                                                               .Replace("val_stocklife", txtStockLife.Text)
                                                                               .Replace("val_repistock", txtRepiStock.Text)

                                                                               .Replace("val_buyer", lblBuyer.Text)
                                                                               .Replace("val_purmanager", lblPurchasingManager.Text)
                                                                               .Replace("val_details", table);


                if (!System.IO.File.Exists(pathLocation))
                {
                    using (StreamWriter writer = new StreamWriter(new FileStream(pathLocation, FileMode.Create, FileAccess.Write)))
                    {
                        writer.WriteLine(templateValue);
                    }
                }
                else
                {
                    System.IO.File.Delete(pathLocation);

                    if (!System.IO.File.Exists(pathLocation))
                    {
                        using (StreamWriter writer = new StreamWriter(new FileStream(pathLocation, FileMode.Create, FileAccess.Write)))
                        {
                            writer.WriteLine(templateValue);
                        }
                    }
                }
            }

            Response.Redirect("URF_Request/" + lblCtrlNo.Text.Trim() + "/REPORT_" + lblCtrlNo.Text.Trim() + ".html", false);
        }




    }
}
