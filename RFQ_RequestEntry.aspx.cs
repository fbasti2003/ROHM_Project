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
    public partial class RFQ_RequestEntry : System.Web.UI.Page
    {

        BLL_RFQ BLL = new BLL_RFQ();
        Common COMMON = new Common();

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
                    if (!String.IsNullOrEmpty(Request.QueryString["RFQNo_From_Inquiry"].ToString()))
                    {
                        string rfqNo = CryptorEngine.Decrypt(Request.QueryString["RFQNo_From_Inquiry"].ToString().Replace(" ", "+"), true);
                        LoadDefaultForUpdate(rfqNo);
                        Page.Title = rfqNo;

                        // check first if login user is from purchasing peep
                        if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()) ||
                            Session["Username"].ToString() == "6985" || Session["Username"].ToString() == "3844" || Session["Username"].ToString() == "1152" || Session["Username"].ToString() == "1402" || Session["Username"].ToString() == "002")
                        {
                            txtPurchasingRemarks.Enabled = true;
                            txtPurchasingRemarks.Text = BLL.getPurchasingRemarksByRFQNo(rfqNo);
                        }
                    }
                    else
                    {
                        LoadDefault();
                        Ddumitems = "<asp:ListItem Text='NO' Value='0'></asp:ListItem>";
                        FirstGridViewRow();

                        string notif = string.Empty;
                        notif = "Please be informed that the request cut-off time is 2PM.";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessageCutOff('" + notif + "');", true);

                        ////---------------------------------------------------------------------------------------------------------
                        //int t1 = int.Parse(DateTime.Now.ToString("HHmm").ToString());
                        //int t2 = int.Parse(ConfigurationManager.AppSettings["RFQ-CUT-OFF-TIME"].ToString());
                        //string notif = string.Empty;

                        //if (t1 > t2)
                        //{
                        //    notif = "Please be informed that this request is going be processed on the next day. Cut-Off time is 2PM.";
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessageCutOff('" + notif + "');", true);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('SUBMIT BUTTON WILL AUTOMATICALLY DISABLED IF ATTACHMENT FILE(S) EXCEED TO 4MB.');", true);
                        //}
                        ////----------------------------------------------------------------------------------------------------------
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "WarningMessage('" + notif + "SUBMIT BUTTON WILL AUTOMATICALLY DISABLED IF ATTACHMENT FILE(S) EXCEED TO 4MB." + "');", true);
                    }
                }
            }
        }

        private void LoadDefaultForUpdate(string rfqNo)
        {
            try
            {
                List<Entities_RFQ_RequestEntry> listDetals = new List<Entities_RFQ_RequestEntry>();
                listDetals = BLL.RFQ_TRANSACTION_GetRequestDetailsByRFQNo(rfqNo);

                if (listDetals != null)
                {
                    if (listDetals.Count > 0)
                    {
                        gvData.ShowFooter = false;                        
                        gvData.DataSource = listDetals;
                        gvData.DataBind();

                    }
                }

                //---------------------------------------------------------------------------------------------------

                List<Entities_RFQ_RequestEntry> listCategory = new List<Entities_RFQ_RequestEntry>();
                listCategory = BLL.RFQ_TRANSACTION_RequestEntry_Fill_All_DropdownList();

                if (listCategory != null)
                {
                    if (listCategory.Count > 0)
                    {
                        ddCategory.Items.Clear();

                        foreach (Entities_RFQ_RequestEntry entity in listCategory)
                        {
                            ListItem item = new ListItem();
                            item.Text = entity.DropdownName;
                            item.Value = entity.DropdownRefId;

                            if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                            {
                                if (entity.TableName == "MT_Category")
                                {
                                    if (item.Value == "1013" || item.Value == "1006"
                                        || entity.DropdownRefId == "1015" || entity.DropdownRefId == "3" || entity.DropdownRefId == "1"
                                        || entity.DropdownRefId == "7" || entity.DropdownRefId == "1014" || entity.DropdownRefId == "1019") 
                                    {
                                        //DO NOT ADD THIS CATEGORY
                                    }
                                    else
                                    {
                                        ddCategory.Items.Add(item);
                                    }
                                }

                            }

                        }

                    }
                }

                //---------------------------------------------------------------------------------------------------

                List<Entities_RFQ_RequestEntry> List_HoldReason = new List<Entities_RFQ_RequestEntry>();
                List_HoldReason = BLL.RFQ_TRANSACTION_GetHoldReason_ByRFQNo(rfqNo);

                if (List_HoldReason != null)
                {
                    if (List_HoldReason.Count > 0)
                    {
                        divHoldReason.Style.Add("display", "block");
                        gvHoldReason.DataSource = List_HoldReason;
                        gvHoldReason.DataBind();
                    }
                }

                //---------------------------------------------------------------------------------------------------

                if (Session["CategoryFromDetails"] != null)
                {
                    ddCategory.Items.FindByText(Session["CategoryFromDetails"].ToString()).Selected = true;
                }

                // check first if login user is from purchasing peep
                if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()) ||
                    Session["Username"].ToString() == "6985" || Session["Username"].ToString() == "3844" || Session["Username"].ToString() == "1152" || Session["Username"].ToString() == "1402" || Session["Username"].ToString() == "002")
                {
                    ddCategory.Enabled = true;
                }
                else
                {
                    ddCategory.Enabled = false;
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

                List<Entities_RFQ_RequestEntry> listCategory = new List<Entities_RFQ_RequestEntry>();
                listCategory = BLL.RFQ_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(itm => itm.DropdownName).ToList();

                if (listCategory != null)
                {
                    if (listCategory.Count > 0)
                    {
                        ddCategory.Items.Add("");

                        foreach (Entities_RFQ_RequestEntry entity in listCategory)
                        {
                            ListItem item = new ListItem();
                            item.Text = entity.DropdownName;
                            item.Value = entity.DropdownRefId;

                            if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                            {
                                if (entity.TableName == "MT_Category")
                                {
                                    if (entity.DropdownRefId == "1012" || entity.DropdownRefId == "1013" || entity.DropdownRefId == "1006"
                                        || entity.DropdownRefId == "1015" || entity.DropdownRefId == "3" || entity.DropdownRefId == "1"
                                        || entity.DropdownRefId == "7" || entity.DropdownRefId == "1014" || entity.DropdownRefId == "1019"
                                        || entity.DropdownRefId == "1018" || entity.DropdownRefId == "1020") 
                                    {
                                        //DO NOTHING (DISABLE MAIN MATERIALS AS PER SIR GHING BECAUSE THIS IS DIRECTLY FROM JAPAN
                                        //12/22/2022

                                        //1013 - MAM JEWEL OLD CATEGORY
                                        //1006 - MAM JUDY OLD CATEGORY
                                    }
                                    else
                                    {
                                        ddCategory.Items.Add(item);
                                    }
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

        private void FirstGridViewRow()
        {
            //string remarks = string.Empty;
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("RdNo", typeof(string)));
            dt.Columns.Add(new DataColumn("RdDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("RdRefId", typeof(string)));
            dt.Columns.Add(new DataColumn("RdUnitOfMeasure", typeof(string)));
            dt.Columns.Add(new DataColumn("RdSpecs", typeof(string)));
            dt.Columns.Add(new DataColumn("RdMaker", typeof(string)));
            dt.Columns.Add(new DataColumn("RdQuantity", typeof(string)));
            dt.Columns.Add(new DataColumn("Col5", typeof(string)));
            dt.Columns.Add(new DataColumn("RdPurpose", typeof(string)));
            dt.Columns.Add(new DataColumn("RdProcess", typeof(string)));
            dt.Columns.Add(new DataColumn("RdRemarks", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["RdRefId"] = string.Empty;
            dr["RdUnitOfMeasure"] = string.Empty;
            dr["RdDescription"] = string.Empty;
            dr["RdSpecs"] = string.Empty;
            dr["RdMaker"] = string.Empty;
            dr["RdQuantity"] = string.Empty;
            dr["Col5"] = string.Empty;

            //remarks = "Purpose / Use of item: " + Environment.NewLine +
            //          "Process / Machine: " + Environment.NewLine +
            //          "Remarks: ";
            //dr["RdRemarks"] = remarks;

            dr["RdPurpose"] = string.Empty;
            dr["RdProcess"] = string.Empty;
            dr["RdRemarks"] = string.Empty;

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
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('REQUEST ENTRY LIMIT NOTIFICATION : You can add request up to 10 items only!');", true);
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
                            TextBox txtDescription =
                              (TextBox)gvData.Rows[rowIndex].Cells[1].FindControl("txtDescription");
                            TextBox txtSpecsDrawing =
                              (TextBox)gvData.Rows[rowIndex].Cells[2].FindControl("txtSpecsDrawing");
                            TextBox txtMakerBrand =
                              (TextBox)gvData.Rows[rowIndex].Cells[3].FindControl("txtMakerBrand");
                            TextBox txtQuantity =
                              (TextBox)gvData.Rows[rowIndex].Cells[4].FindControl("txtQuantity");
                            DropDownList ddUOM =
                              (DropDownList)gvData.Rows[rowIndex].Cells[5].FindControl("ddUOM");                            
                            TextBox txtFileSize =
                              (TextBox)gvData.Rows[rowIndex].Cells[6].FindControl("txtFileSize");
                            TextBox txtPurpose =
                              (TextBox)gvData.Rows[rowIndex].Cells[7].FindControl("txtPurpose");
                            TextBox txtProcess =
                              (TextBox)gvData.Rows[rowIndex].Cells[8].FindControl("txtProcess");
                            TextBox txtRemarks =
                              (TextBox)gvData.Rows[rowIndex].Cells[9].FindControl("txtRemarks");
                            FileUpload fuAttachment =
                              (FileUpload)gvData.Rows[rowIndex].Cells[10].FindControl("fuAttachment");
                            TextBox txtFileName =
                              (TextBox)gvData.Rows[rowIndex].Cells[11].FindControl("txtFileName");
                            

                            List<Entities_RFQ_RequestEntry> listCategory = new List<Entities_RFQ_RequestEntry>();
                            listCategory = BLL.RFQ_TRANSACTION_RequestEntry_Fill_All_DropdownList();

                            if (listCategory != null)
                            {
                                if (listCategory.Count > 0)
                                {
                                    ddCategory.Items.Add("");

                                    foreach (Entities_RFQ_RequestEntry entity in listCategory)
                                    {
                                        ListItem item = new ListItem();
                                        item.Text = entity.DropdownName;
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

                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["RowNumber"] = i + 1;

                            dtCurrentTable.Rows[i - 1]["RdRefId"] = string.Empty;
                            dtCurrentTable.Rows[i - 1]["RdUnitOfMeasure"] = ddUOM.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["RdDescription"] = txtDescription.Text;
                            dtCurrentTable.Rows[i - 1]["RdSpecs"] = txtSpecsDrawing.Text;
                            dtCurrentTable.Rows[i - 1]["RdMaker"] = txtMakerBrand.Text;
                            dtCurrentTable.Rows[i - 1]["RdQuantity"] = txtQuantity.Text;
                            dtCurrentTable.Rows[i - 1]["Col5"] = ddUOM.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["RdPurpose"] = txtPurpose.Text;
                            dtCurrentTable.Rows[i - 1]["RdProcess"] = txtProcess.Text;
                            dtCurrentTable.Rows[i - 1]["RdRemarks"] = txtRemarks.Text;

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
                        TextBox txtDescription = (TextBox)gvData.Rows[rowIndex].Cells[1].FindControl("txtDescription");
                        TextBox txtSpecsDrawing = (TextBox)gvData.Rows[rowIndex].Cells[2].FindControl("txtSpecsDrawing");
                        TextBox txtMakerBrand =
                          (TextBox)gvData.Rows[rowIndex].Cells[3].FindControl("txtMakerBrand");
                        TextBox txtQuantity =
                          (TextBox)gvData.Rows[rowIndex].Cells[4].FindControl("txtQuantity");
                        DropDownList ddUOM =
                          (DropDownList)gvData.Rows[rowIndex].Cells[5].FindControl("ddUOM");
                        TextBox txtFileSize =
                              (TextBox)gvData.Rows[rowIndex].Cells[6].FindControl("txtFileSize");
                        TextBox txtPurpose =
                          (TextBox)gvData.Rows[rowIndex].Cells[7].FindControl("txtPurpose");
                        TextBox txtProcess =
                          (TextBox)gvData.Rows[rowIndex].Cells[8].FindControl("txtProcess");
                        TextBox txtRemarks =
                          (TextBox)gvData.Rows[rowIndex].Cells[9].FindControl("txtRemarks");
                        FileUpload fuAttachment =
                          (FileUpload)gvData.Rows[rowIndex].Cells[10].FindControl("fuAttachment");
                        TextBox txtFileName =
                          (TextBox)gvData.Rows[rowIndex].Cells[11].FindControl("txtFileName");

                        ddUOM.Items.FindByValue(dt.Rows[i]["RdUnitOfMeasure"].ToString()).Selected = true;
                        txtDescription.Text = dt.Rows[i]["RdDescription"].ToString();
                        txtSpecsDrawing.Text = dt.Rows[i]["RdSpecs"].ToString();
                        txtMakerBrand.Text = dt.Rows[i]["RdMaker"].ToString();
                        txtQuantity.Text = dt.Rows[i]["RdQuantity"].ToString();
                        ddUOM.SelectedValue = dt.Rows[i]["Col5"].ToString();
                        txtPurpose.Text = dt.Rows[i]["RdPurpose"].ToString();
                        txtProcess.Text = dt.Rows[i]["RdProcess"].ToString();
                        txtRemarks.Text = dt.Rows[i]["RdRemarks"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

        private Int32 setControlNumber()
        {
            return BLL.RFQ_TRANSACTION_CountRequestHead(DateTime.Now.Year.ToString()) + 1;
        }

        private string setRFQNumberWithPrefix()
        {
            string retVal = string.Empty;

            retVal = Session["DivisionCode"].ToString() + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + COMMON.controlNoZeroPrefix(setControlNumber().ToString().Length.ToString()) + setControlNumber().ToString();

            return retVal;
        }


        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddUOM = (DropDownList)e.Row.FindControl("ddUOM");
                    Label lblUnitOfMeasure = (Label)e.Row.FindControl("lblUnitOfMeasure");
                    FileUpload fuAttachment = (FileUpload)e.Row.FindControl("fuAttachment");
                    TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");

                    List<Entities_RFQ_RequestEntry> listCategory = new List<Entities_RFQ_RequestEntry>();
                    listCategory = BLL.RFQ_TRANSACTION_RequestEntry_Fill_All_DropdownList();

                    if (listCategory != null)
                    {
                        if (listCategory.Count > 0)
                        {
                            ddCategory.Items.Add("");

                            foreach (Entities_RFQ_RequestEntry entity in listCategory)
                            {
                                ListItem item = new ListItem();
                                item.Text = entity.DropdownName;
                                item.Value = entity.DropdownRefId;

                                if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                                {
                                    if (entity.TableName == "MT_UnitOfMeasure")
                                    {
                                        ddUOM.Items.Add(item);
                                    }

                                }

                            }
                            if (!String.IsNullOrEmpty(Request.QueryString["RFQNo_From_Inquiry"].ToString()))
                            {
                                ddUOM.Items.FindByValue(lblUnitOfMeasure.Text).Selected = true;
                                fuAttachment.Visible = false;
                                txtRemarks.Style.Add("width", "240px");                                                                
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
                        TextBox txtDescription = (TextBox)gvData.Rows[rowIndex].Cells[1].FindControl("txtDescription");
                        TextBox txtSpecsDrawing = (TextBox)gvData.Rows[rowIndex].Cells[2].FindControl("txtSpecsDrawing");
                        TextBox txtMakerBrand = (TextBox)gvData.Rows[rowIndex].Cells[3].FindControl("txtMakerBrand");
                        TextBox txtQuantity =
                          (TextBox)gvData.Rows[rowIndex].Cells[4].FindControl("txtQuantity");
                        DropDownList ddUOM =
                          (DropDownList)gvData.Rows[rowIndex].Cells[5].FindControl("ddUOM");
                        TextBox txtFileSize =
                              (TextBox)gvData.Rows[rowIndex].Cells[6].FindControl("txtFileSize");
                        TextBox txtPurpose =
                          (TextBox)gvData.Rows[rowIndex].Cells[7].FindControl("txtPurpose");
                        TextBox txtProcess =
                          (TextBox)gvData.Rows[rowIndex].Cells[8].FindControl("txtProcess");
                        TextBox txtRemarks =
                          (TextBox)gvData.Rows[rowIndex].Cells[9].FindControl("txtRemarks");
                        FileUpload fuAttachment =
                          (FileUpload)gvData.Rows[rowIndex].Cells[10].FindControl("fuAttachment");
                        TextBox txtFileName =
                          (TextBox)gvData.Rows[rowIndex].Cells[11].FindControl("txtFileName");


                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["RdRefId"] = string.Empty;
                        dtCurrentTable.Rows[i - 1]["RdUnitOfMeasure"] = ddUOM.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["RdDescription"] = txtDescription.Text;
                        dtCurrentTable.Rows[i - 1]["RdSpecs"] = txtSpecsDrawing.Text;
                        dtCurrentTable.Rows[i - 1]["RdMaker"] = txtMakerBrand.Text;
                        dtCurrentTable.Rows[i - 1]["RdQuantity"] = txtQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["Col5"] = ddUOM.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["RdPurpose"] = txtRemarks.Text;
                        dtCurrentTable.Rows[i - 1]["RdProcess"] = txtRemarks.Text;
                        dtCurrentTable.Rows[i - 1]["RdRemarks"] = txtRemarks.Text;
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

        protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool isNoError = false;
                string successHead = string.Empty;
                string successDetails = string.Empty;
                string successStatus = string.Empty;
                int errorCounter = 0;
                string itemDescriptionAndSpecs = string.Empty;
                string errorDescription = string.Empty;
                string descriptionSpecsExistInTable = string.Empty;
                int errorCounterExisting = 0;

                if (ddCategory.Text.Length <= 0)
                {
                    errorCounter++;
                    ddCategory.Style.Add("background-color", "#f44336");
                    errorDescription = "CATEGORY field must not be blank";
                }
                else
                {
                    ddCategory.Style.Add("background-color", "White");

                    for (int i = 0; i < gvData.Rows.Count; i++)
                    {
                        TextBox txtDescription = (TextBox)gvData.Rows[i].Cells[1].FindControl("txtDescription");
                        TextBox txtSpecsDrawing = (TextBox)gvData.Rows[i].Cells[2].FindControl("txtSpecsDrawing");
                        TextBox txtMakerBrand = (TextBox)gvData.Rows[i].Cells[3].FindControl("txtMakerBrand");
                        TextBox txtQuantity = (TextBox)gvData.Rows[i].Cells[4].FindControl("txtQuantity");
                        DropDownList ddUOM = (DropDownList)gvData.Rows[i].Cells[5].FindControl("ddUOM");
                        TextBox txtPurpose = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtPurpose");
                        TextBox txtProcess = (TextBox)gvData.Rows[i].Cells[8].FindControl("txtProcess");                        
                        TextBox txtRemarks = (TextBox)gvData.Rows[i].Cells[9].FindControl("txtRemarks");

                        txtDescription.Style.Add("background-color", "White");
                        txtSpecsDrawing.Style.Add("background-color", "White");
                        txtMakerBrand.Style.Add("background-color", "White");
                        txtQuantity.Style.Add("background-color", "White");
                        txtPurpose.Style.Add("background-color", "White");
                        txtProcess.Style.Add("background-color", "White");
                        txtRemarks.Style.Add("background-color", "White");

                        if (string.IsNullOrEmpty(txtDescription.Text))
                        {
                            txtDescription.Style.Add("background-color", "#f44336");
                            errorCounter++;
                            errorDescription = "DESCRIPTION field must not be blank";
                        }
                        if (string.IsNullOrEmpty(txtSpecsDrawing.Text))
                        {
                            txtSpecsDrawing.Style.Add("background-color", "#f44336");
                            errorCounter++;
                            errorDescription = "DRAWING/SPECIFICATION field must not be blank";
                        }
                        if (!COMMON.isNumeric(txtQuantity.Text.Trim(), System.Globalization.NumberStyles.AllowThousands) || String.IsNullOrEmpty(txtQuantity.Text.Trim()))
                        {
                            txtQuantity.Style.Add("background-color", "#f44336");
                            errorCounter++;
                            errorDescription = "PLEASE ENTER A VALID QUANTITY";
                        }
                        if (string.IsNullOrEmpty(txtProcess.Text))
                        {
                            txtProcess.Style.Add("background-color", "#f44336");
                            errorCounter++;
                            errorDescription = "PROCESS/MACHINE field must not be blank";
                        }
                        if (string.IsNullOrEmpty(txtPurpose.Text))
                        {
                            txtPurpose.Style.Add("background-color", "#f44336");
                            errorCounter++;
                            errorDescription = "PURPOSE/USE & PICTURE OF ITEM field must not be blank";
                        }                        

                        itemDescriptionAndSpecs += (txtDescription.Text.Trim().ToUpper().Replace("'", "''") + txtSpecsDrawing.Text.Trim().ToUpper().Replace("'", "''")) + ",";
                        descriptionSpecsExistInTable += "'" + (txtDescription.Text.Trim().ToUpper().Replace("'", "''") + txtSpecsDrawing.Text.Trim().ToUpper().Replace("'", "''")) + "',";                        
                        
                    }

                    //--------------------------------------------------------------------------------------------------------------------------------------------
                    List<string> completeItems = new List<string>(itemDescriptionAndSpecs.Trim().Split(',').Select(t => t.Trim()));
                    bool isUnique = completeItems.Count == completeItems.Distinct().Count();

                    if (!isUnique)
                    {
                        errorCounter++;
                        errorDescription = "System detected duplicate entry. <br/> Please check your items! <br/> Check description and specification.";
                    }

                    //if (gvData.Rows.Count > 10)
                    //{
                    //    errorCounter++;
                    //    errorDescription = "REQUEST ENTRY LIMIT NOTIFICATION : You can add request up to 10 items only!";
                    //}

                    if (Session["errorCounterExisting"] != null)
                    {
                        if (Session["errorCounterExisting"].ToString() == "yes")
                        {
                            errorCounterExisting = 0;
                            Session["errorCounterExisting"] = null;
                        }
                    }
                    else
                    {
                        
                        //--------------------------------------------------------------------------------------------------------------------------------------------
                        List<Entities_RFQ_RequestEntry> listDescSpecsExisting = new List<Entities_RFQ_RequestEntry>();
                        string finalDescSpecs = descriptionSpecsExistInTable.Trim().ToUpper().Replace(" ", "");
                        listDescSpecsExisting = BLL.RFQ_TRANSACTION_GetRequestDetailsByExistingDescriptionSpecs(finalDescSpecs.Substring(0, (finalDescSpecs.Trim().Length - 1)));

                        if (listDescSpecsExisting != null)
                        {
                            if (listDescSpecsExisting.Count > 0)
                            {
                                errorDescription = "System detected existing request listed in your collection. Please check existing items below";
                                divExistingDetails.Style.Add("display", "block");
                                divExistingMessage.Style.Add("display", "block");
                                Session["errorCounterExisting"] = "yes";
                                errorCounterExisting++;
                                gvExisting.DataSource = listDescSpecsExisting;
                                gvExisting.DataBind();

                            }
                            else
                            {
                                divExistingDetails.Style.Add("display", "none");
                                divExistingMessage.Style.Add("display", "none");
                            }
                        }
                        else
                        {
                            divExistingDetails.Style.Add("display", "none");
                            divExistingMessage.Style.Add("display", "none");
                        }
                        //--------------------------------------------------------------------------------------------------------------------------------------------

                    }

                }

                if (errorCounter <= 0 && errorCounterExisting <= 0)
                {
                    string queryDetails = string.Empty;
                    string queryHead = string.Empty;
                    string queryStatus = string.Empty;
                    string queryHistoryOfUpdates = string.Empty;
                    string attachmentFiles = string.Empty;
                    int rd_Query_Counter = 0;
                    string query_Success = string.Empty;
                    string temp_RFQNo = string.Empty;

                    string queryBeginPart = string.Empty;
                    string queryEndPart = string.Empty;
                    int queryHeadCounter = 1;
                    int queryStatusCounter = 0;
                    int queryHistoryUpdatesCounter = 0;


                    queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";

                    if (!String.IsNullOrEmpty(Request.QueryString["RFQNo_From_Inquiry"].ToString()))
                    {
                        temp_RFQNo = CryptorEngine.Decrypt(Request.QueryString["RFQNo_From_Inquiry"].ToString().Replace(" ", "+"), true);
                        queryHead = "UPDATE Request_HEAD SET Category ='" + ddCategory.SelectedValue + "' WHERE RFQNo = '" + temp_RFQNo + "' ";

                        if (Session["holdState"] != null)
                        {
                            if (Session["holdState"].ToString().ToUpper() == "HOLD BY PRODUCTION MANAGER")
                            {
                                queryStatus = "UPDATE Request_Status SET ProdManager = '0' WHERE RFQNo = '" + temp_RFQNo + "' ";
                            }
                            if (Session["holdState"].ToString().ToUpper() == "HOLD BY SC BUYER")
                            {
                                queryStatus = "UPDATE Request_Status SET Purchasing = '0' WHERE RFQNo = '" + temp_RFQNo + "' ";
                            }
                            queryStatus += "INSERT INTO Request_Hold_Reason (RFQNo, Reason, CreatedBy, CreatedDate) VALUES ('" + temp_RFQNo +
                                           "','" + txtHoldReasonResponse.Text + "','" + Session["UserFullName"].ToString() + "',GETDATE()) ";
                            queryStatusCounter++;
                        }
                        //else
                        //{
                        //    queryStatus = "UPDATE Request_Status SET ProdManager = '0' WHERE RFQNo = '" + temp_RFQNo + "' ";
                        //}
                        
                        //queryStatusCounter++;

                        if (!string.IsNullOrEmpty(txtPurchasingRemarks.Text))
                        {
                            queryHead = "UPDATE Request_HEAD SET Category ='" + ddCategory.SelectedValue + "', PurchasingRemarks ='" + txtPurchasingRemarks.Text.Replace("'", "''") + "' WHERE RFQNo = '" + temp_RFQNo + "' ";
                        }

                        if (Session["CategoryFromDetails"] != null)
                        {
                            // check first if login user is from purchasing peep
                            if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()) ||
                                Session["Username"].ToString() == "6985" || Session["Username"].ToString() == "3844" || Session["Username"].ToString() == "1152" || Session["Username"].ToString() == "1402" || Session["Username"].ToString() == "002")
                            {

                                if (Session["CategoryFromDetails"].ToString().ToUpper() == ddCategory.SelectedItem.Text.ToUpper())
                                {
                                    // do nothing if the same
                                }
                                else
                                {
                                    queryHistoryUpdatesCounter = 1;
                                    queryHistoryOfUpdates = "INSERT INTO HistoryOfUpdates (RFQNo, DetailsRefId, TableName, UpdatedBy, UpdatedDate, TransactionName, UpdateWhat) " +
                                                            "VALUES ('" + temp_RFQNo + "','NA','Request_Head','" + Session["LcRefId"].ToString() + "',GETDATE(),'Purchasing-NotMyCategory','Category from " + Session["CategoryFromDetails"].ToString().ToUpper() + " to " + ddCategory.SelectedItem.Text.ToUpper() + "')";
                                }

                            }
                        }
                    }
                    else
                    {
                        temp_RFQNo = setRFQNumberWithPrefix();
                        queryStatusCounter = 1;

                        if (ddCategory.SelectedValue == "1015" || ddCategory.SelectedValue == "3")
                        {
                            // If selected category is EQUIPMENT & SERVICES or EQUIPMENT / SERVICES then assigned it to category "EQUIPMENT AND SERVICES (RENZ)"
                            queryHead = "INSERT INTO Request_HEAD (RFQNO, Division, Department, Section, Requester, TransactionDate, Category) " +
                                    "VALUES ('" + temp_RFQNo + "','" + Session["Division"].ToString() + "','" + Session["Department"].ToString() +
                                    "','" + Session["Section"].ToString() + "','" + Session["LcRefId"].ToString() + "',GETDATE(),'1016') ";
                        }
                        //I COMMENTED THIS ONE BECAUSE MAM JEWEL IS ON LEAVE (12/19/2022)
                        //else if (ddCategory.SelectedValue == "1013") //FOR MAM JEWEL OLD CATEGORY AUTOMATIC TRANSFER TO NEW CATEGORY
                        //{
                        //    queryHead = "INSERT INTO Request_HEAD (RFQNO, Division, Department, Section, Requester, TransactionDate, Category) " +
                        //            "VALUES ('" + temp_RFQNo + "','" + Session["Division"].ToString() + "','" + Session["Department"].ToString() +
                        //            "','" + Session["Section"].ToString() + "','" + Session["LcRefId"].ToString() + "',GETDATE(),'1018') ";
                        //}
                        else if (ddCategory.SelectedValue == "1006") //FOR MAM JUDY OLD CATEGORY AUTOMATIC TRANSFER TO NEW CATEGORY
                        {
                            queryHead = "INSERT INTO Request_HEAD (RFQNO, Division, Department, Section, Requester, TransactionDate, Category) " +
                                    "VALUES ('" + temp_RFQNo + "','" + Session["Division"].ToString() + "','" + Session["Department"].ToString() +
                                    "','" + Session["Section"].ToString() + "','" + Session["LcRefId"].ToString() + "',GETDATE(),'1017') ";
                        }
                        else if (ddCategory.SelectedValue == "1018") //FOR MAM JEWEL OLD CATEGORY AUTOMATIC TRANSFER TO NEW CATEGORY OF MAM SYLVIA
                        {
                            queryHead = "INSERT INTO Request_HEAD (RFQNO, Division, Department, Section, Requester, TransactionDate, Category) " +
                                    "VALUES ('" + temp_RFQNo + "','" + Session["Division"].ToString() + "','" + Session["Department"].ToString() +
                                    "','" + Session["Section"].ToString() + "','" + Session["LcRefId"].ToString() + "',GETDATE(),'1019') ";
                        }
                        else if (ddCategory.SelectedValue == "1020") // SPAREPARTS (DISCRETE) SYLVIA TO SPAREPARTS (MCR/TR) BAMBIE
                        {
                            queryHead = "INSERT INTO Request_HEAD (RFQNO, Division, Department, Section, Requester, TransactionDate, Category) " +
                                    "VALUES ('" + temp_RFQNo + "','" + Session["Division"].ToString() + "','" + Session["Department"].ToString() +
                                    "','" + Session["Section"].ToString() + "','" + Session["LcRefId"].ToString() + "',GETDATE(),'1023') ";
                        }
                        else
                        {
                            queryHead = "INSERT INTO Request_HEAD (RFQNO, Division, Department, Section, Requester, TransactionDate, Category) " +
                                    "VALUES ('" + temp_RFQNo + "','" + Session["Division"].ToString() + "','" + Session["Department"].ToString() +
                                    "','" + Session["Section"].ToString() + "','" + Session["LcRefId"].ToString() + "',GETDATE(),'" + ddCategory.SelectedValue + "') ";
                        }

                        

                        queryStatus = "INSERT INTO Request_Status (RFQNO, Requester, ProdManager, Purchasing, Supplier, PurchasingIncharge, DepartmentManager, DivisionManager) " +
                                  "VALUES ('" + temp_RFQNo + "','" + Session["LcRefId"].ToString() + "','0','0','0','0','0','0') ";
                    }


                    for (int i = 0; i < gvData.Rows.Count; i++)
                    {
                        TextBox txtDescription = (TextBox)gvData.Rows[i].Cells[1].FindControl("txtDescription");
                        TextBox txtSpecsDrawing = (TextBox)gvData.Rows[i].Cells[2].FindControl("txtSpecsDrawing");
                        TextBox txtMakerBrand = (TextBox)gvData.Rows[i].Cells[3].FindControl("txtMakerBrand");
                        TextBox txtQuantity = (TextBox)gvData.Rows[i].Cells[4].FindControl("txtQuantity");
                        DropDownList ddUOM = (DropDownList)gvData.Rows[i].Cells[5].FindControl("ddUOM");
                        TextBox txtPurpose = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtPurpose");
                        TextBox txtProcess = (TextBox)gvData.Rows[i].Cells[8].FindControl("txtProcess");
                        TextBox txtRemarks = (TextBox)gvData.Rows[i].Cells[9].FindControl("txtRemarks");
                        FileUpload fuAttachment = (FileUpload)gvData.Rows[i].Cells[10].FindControl("fuAttachment");
                        Label lblRefId = (Label)gvData.Rows[i].Cells[1].FindControl("lblRefId");

                        string fileNameApplication = System.IO.Path.GetFileName(fuAttachment.FileName);
                        string fileExtensionApplication = System.IO.Path.GetExtension(fileNameApplication);
                        string newFile = fileNameApplication;

                        if (fuAttachment.HasFile)
                        {

                            if (!System.IO.Directory.Exists(Server.MapPath("~/IO_Request/" + temp_RFQNo)))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/IO_Request/" + temp_RFQNo));
                            }
                            if (!System.IO.File.Exists(Server.MapPath("~/IO_Request/" + temp_RFQNo + "/" + newFile)))
                            {
                                fuAttachment.SaveAs(System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + temp_RFQNo), newFile));
                                fuAttachment.Dispose();
                                System.IO.File.Copy(System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + temp_RFQNo), newFile), System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + temp_RFQNo), (i.ToString() + "-" + temp_RFQNo + fileExtensionApplication)), true);
                                System.IO.File.Delete(System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + temp_RFQNo), newFile));
                            }

                            attachmentFiles = i.ToString() + "-" + temp_RFQNo + fileExtensionApplication;
                        }

                        if (!String.IsNullOrEmpty(Request.QueryString["RFQNo_From_Inquiry"].ToString()))
                        {
                            queryDetails += "UPDATE Request_Details SET Description ='" + txtDescription.Text.Replace("'", "''") + "', Specs = '" + txtSpecsDrawing.Text.Replace("'", "''") +
                                            "', Maker = '" + txtMakerBrand.Text.Replace("'", "''") + "', Quantity = '" + txtQuantity.Text.Trim() + "', UOM = '" + ddUOM.SelectedValue +
                                            "', Purpose = '" + txtPurpose.Text.Replace("'", "''") + "', Process = '" + txtProcess.Text.Replace("'", "''") + 
                                            "', Remarks = '" + txtRemarks.Text.Replace("'", "''").Replace("[", "").Replace("]", "") + "' WHERE RFQNo = '" + temp_RFQNo + "' AND RefId ='" + lblRefId.Text.Trim() + "' ";
                        }
                        else
                        {
                            queryDetails += "INSERT INTO Request_Details (RFQNO, Description, Specs, Maker, Quantity, UOM, Remarks, Attachment, Purpose, Process) " +
                                            "VALUES ('" + temp_RFQNo + "','" + txtDescription.Text.Replace("'", "''") + "','" + txtSpecsDrawing.Text.Replace("'", "''") +
                                            "','" + txtMakerBrand.Text.Replace("'", "''") + "','" + txtQuantity.Text.Trim() + "','" + ddUOM.SelectedValue + "','" + txtRemarks.Text.Replace("'", "''").Replace("[", "").Replace("]", "") + "','" + attachmentFiles + "','" + txtPurpose.Text.Replace("'", "''") + "','" + txtProcess.Text.Replace("'", "''") + "') ";
                        }

                        rd_Query_Counter++;
                    }

                    // IF NO ATTACHMENT
                    if (string.IsNullOrEmpty(attachmentFiles))
                    {
                        if (!System.IO.Directory.Exists(Server.MapPath("~/IO_Request/" + temp_RFQNo)))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath("~/IO_Request/" + temp_RFQNo));
                        }
                    }

                    queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";

                    if (!string.IsNullOrEmpty(queryHistoryOfUpdates))
                    {
                        query_Success = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + queryHead + queryStatus + queryDetails + queryHistoryOfUpdates + queryEndPart).ToString();
                    }
                    else
                    {
                        query_Success = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + queryHead + queryStatus + queryDetails + queryEndPart).ToString();
                    }

                    if (query_Success == (rd_Query_Counter + queryHeadCounter + queryStatusCounter + queryHistoryUpdatesCounter).ToString())
                    {
                        Session["successMessage"] = "RFQ NUMBER : <b>" + temp_RFQNo + "</b> HAS BEEN SUCCESSFULLY SAVED.";
                        Session["successTransactionName"] = "RFQ_REQUESTENTRY";
                        Session["successReturnPage"] = "RFQ_RequestEntry.aspx?RFQNo_From_Inquiry=";

                        if (Session["From_OnePage"] != null)
                        {
                            if (Session["From_OnePage"].ToString() == "true")
                            {
                                Session["From_OnePage"] = "true";
                                Session["RFQ_FromOnePage"] = temp_RFQNo;
                                Session["successTransactionName"] = "RFQ_REQUEST_UPDATE";
                                Session["successReturnPage"] = "RFQ_OnePage.aspx";
                            }
                        }

                        if (!String.IsNullOrEmpty(Request.QueryString["RFQNo_From_Inquiry"].ToString()))
                        {
                            Session["UPDATE_From_Inquiry"] = "true";
                            Session["Original_FromUpdatingDetails"] = temp_RFQNo;
                        }

                        Response.Redirect("SuccessPage.aspx");
                    }


                }
                else
                {
                    // errorCounter is greater than 0
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + errorDescription.ToUpper() + "');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.StackTrace.ToString() + "');", true);
            }
        }


        protected void gvExisting_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                LinkButton linkRFQNo = row.FindControl("linkRFQNo") as LinkButton;
                Label lblRequester = row.FindControl("lblRequester") as Label;
                Label lblTransactionDate = row.FindControl("lblTransactionDate") as Label;
                Label lblCategoryName = row.FindControl("lblCategoryName") as Label;

                if (e.CommandName == "linkRFQNo_Command")
                {
                    Session["Requester_From_Inquiry"] = lblRequester.Text;
                    Session["TransDate_From_Inquiry"] = lblTransactionDate.Text;
                    Session["Category_From_Inquiry"] = lblCategoryName.Text.Trim().ToUpper();
                    Session["btnPreview_Visibility"] = "true";
                    Session["btnUpdate_Visibility"] = "false";

                    Response.Redirect("RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(linkRFQNo.Text.Trim(), true), false);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


    }
}
