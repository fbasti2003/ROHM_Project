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
//using REPI_PUR_SOFRA.App_Code.BLL;
//using REPI_PUR_SOFRA.App_Code.ENTITIES;
using SpreadsheetLight;
using System.IO;

namespace REPI_PUR_SOFRA
{
    public partial class SRF_PO_RequestEntry : System.Web.UI.Page
    {

        BLL_SRF BLL = new BLL_SRF();
        Common COMMON = new Common();
        public string displayCTRLNo = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // POPULATE SUPPLIER
                List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();
                list = BLL.SRF_TRANSACTION_RequestEntry_Fill_All_DropdownList().OrderBy(item => item.DropdownName).ToList();

                ddSupplier.Items.Add("");

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        foreach (Entities_SRF_RequestEntry entity in list)
                        {
                            ListItem item = new ListItem();
                            item.Text = entity.DropdownName.ToUpper();
                            item.Value = entity.DropdownRefId;

                            if (entity.IsDisabled == string.Empty || entity.IsDisabled == "0")
                            {
                                if (entity.TableName == "MT_Supplier_Head")
                                {
                                    // FILTER REQUIRED SUPPLIER ONLY 
                                    if (entity.DropdownRefId == "442" || entity.DropdownRefId == "137" || entity.DropdownRefId == "445"
                                                                      || entity.DropdownRefId == "451" || entity.DropdownRefId == "565")
                                    {
                                        ddSupplier.Items.Add(item);
                                    }
                                }                                
                            }

                        }

                    }
                }


                if (!String.IsNullOrEmpty(Request.QueryString["SRFNo_PO_From_Inquiry"].ToString()))
                {
                    //UPDATE
                    string ctrlno = CryptorEngine.Decrypt(Request.QueryString["SRFNo_PO_From_Inquiry"].ToString().Replace(" ", "+"), true);
                    loadDefaultForUpdate(ctrlno);
                    displayCTRLNo = ctrlno.ToUpper();
                }
                else
                {
                    //NEW ENTRY
                }
            }
        }

        private void loadDefaultForUpdate(string ctrlno)
        {
            try
            {
                string type = string.Empty;

                List<Entities_SRF_PO_Entry> list = new List<Entities_SRF_PO_Entry>();
                list = BLL.SRF_TRANSACTION_PO_AllRequest_ByCTRLNo(ctrlno);

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        foreach (Entities_SRF_PO_Entry entity in list)
                        {
                            ddPullOutType.Items.FindByValue(entity.Head_Type).Selected = true;
                            ddSupplier.Items.FindByValue(entity.Supplier).Selected = true;
                            ddDivision.Items.FindByValue(entity.DivisionDisplay).Selected = true;
                            type = entity.Head_Type.ToUpper();

                            lblRequester.Text = entity.Head_Requester;
                            lblProdManager.Text = entity.Head_ProdManager;
                            lblBuyer.Text = entity.Head_Buyer;
                            lblSCIncharge.Text = entity.Head_SCIncharge;

                            if (entity.Head_RequesterId != Session["LcRefId"].ToString())
                            {
                                btnSubmit.Visible = false;
                                btnBack.Visible = false;
                            }

                            // PROD MANAGER
                            if (entity.Head_StatProdManager == "1")
                            {
                                divProdManager.Style.Add("background-color", "#00C851");

                                btnSubmit.Visible = false;
                                btnBack.Visible = false;
                            }
                            if (entity.Head_StatProdManager == "2")
                            {
                                divProdManager.Style.Add("background-color", "#ffbb33");
                                Session["prodManagerStat"] = "2";
                                Session["buyerStat"] = "";
                            }
                            if (entity.Head_StatProdManager == "0")
                            {
                                divProdManager.Style.Add("background-color", "#f44336");
                            }
                            if (entity.Head_StatProdManager == "-1") //AUTO-APPROVED
                            {
                                divProdManager.Style.Add("background-color", "#00C851");
                                lblProdManager.Text = "AUTO-APPROVED";
                            }

                            // BUYER
                            if (entity.Head_StatBuyer == "1")
                            {
                                divBuyer.Style.Add("background-color", "#00C851");
                            }
                            if (entity.Head_StatBuyer == "2")
                            {
                                divBuyer.Style.Add("background-color", "#ffbb33");
                                Session["prodManagerStat"] = "";
                                Session["buyerStat"] = "2";
                            }
                            if (entity.Head_StatBuyer == "0")
                            {
                                divBuyer.Style.Add("background-color", "#f44336");
                            }
                            if (entity.Head_StatBuyer == "-1") //AUTO-APPROVED
                            {
                                divBuyer.Style.Add("background-color", "#00C851");
                                lblBuyer.Text = "AUTO-APPROVED";
                            }

                            // SC INCHARGE
                            if (entity.Head_StatSCIncharge == "1")
                            {
                                divSCIncharge.Style.Add("background-color", "#00C851");
                            }
                            if (entity.Head_StatSCIncharge == "2")
                            {
                                divSCIncharge.Style.Add("background-color", "#ffbb33");
                            }
                            if (entity.Head_StatSCIncharge == "0")
                            {
                                divSCIncharge.Style.Add("background-color", "#f44336");
                            }
                            if (entity.Head_StatSCIncharge == "-1") //AUTO-APPROVED
                            {
                                divSCIncharge.Style.Add("background-color", "#00C851");
                                lblSCIncharge.Text = "AUTO-APPROVED";
                            }
                        }

                        // CONTAINER TUBES
                        if (type == "CONTAINER TUBES")
                        {
                            divICTrays.Style.Add("display", "none");
                            divContainerTube.Style.Add("display", "block");

                            gvContainerTube.DataSource = list;
                            gvContainerTube.DataBind();                            

                        }

                        // IC TRAYS
                        if (type == "IC TRAYS")
                        {
                            divContainerTube.Style.Add("display", "none");
                            divICTrays.Style.Add("display", "block");

                            gvICTrays.DataSource = list;
                            gvICTrays.DataBind();                            

                        }

                        // OTHERS
                        if (type == "EMPTY BLACK TRAY" || type == "DANPLA BOX" || type == "ROBUST REEL")
                        {
                            divContainerTube.Style.Add("display", "none");
                            divICTrays.Style.Add("display", "none");
                            divOthers.Style.Add("display", "block");

                            gvOthers.DataSource = list;
                            gvOthers.DataBind();

                        }

                        // APPROVAL
                        divApproval.Style.Add("display", "block");

                        // ATTACHMENT
                        divAttachment.Style.Add("display", "block");

                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void ibAttachment1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["SRFNo_PO_From_Inquiry"].ToString()))
            {
                string ctrlno = CryptorEngine.Decrypt(Request.QueryString["SRFNo_PO_From_Inquiry"].ToString().Replace(" ", "+"), true);
                Response.Redirect("SRF_PO_Request/" + ctrlno + "/" + ctrlno + "_SRF_PO_Request.xlsx", false);
            }
        }

        protected void ddPullOutType_Change(object sender, EventArgs e)
        {
            try
            {

                if (String.IsNullOrEmpty(Request.QueryString["SRFNo_PO_From_Inquiry"].ToString()))
                {

                    // CONTAINER TUBES
                    if (ddPullOutType.SelectedItem.Text == "CONTAINER TUBES")
                    {
                        divICTrays.Style.Add("display", "none");
                        divContainerTube.Style.Add("display", "block");

                        List<Entities_SRF_PO_ContainerTubes> list = new List<Entities_SRF_PO_ContainerTubes>();
                        list = BLL.SRF_MT_PullOutOfContainerTubes_GetAll();

                        if (list != null)
                        {
                            if (list.Count > 0)
                            {
                                gvContainerTube.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                                gvContainerTube.DataBind();
                            }
                        }

                    }

                    // IC TRAYS
                    if (ddPullOutType.SelectedItem.Text == "IC TRAYS")
                    {
                        divContainerTube.Style.Add("display", "none");
                        divICTrays.Style.Add("display", "block");

                        List<Entities_SRF_PO_ICTrays> list = new List<Entities_SRF_PO_ICTrays>();
                        list = BLL.SRF_MT_PullOutICTrays_GetAll();

                        if (list != null)
                        {
                            if (list.Count > 0)
                            {
                                gvICTrays.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                                gvICTrays.DataBind();
                            }
                        }
                    }

                    // OTHERS
                    if (ddPullOutType.SelectedItem.Text == "EMPTY BLACK TRAY" || ddPullOutType.SelectedItem.Text == "DANPLA BOX" || ddPullOutType.SelectedItem.Text == "ROBUST REEL")
                    {
                        divContainerTube.Style.Add("display", "none");
                        divICTrays.Style.Add("display", "none");
                        divOthers.Style.Add("display", "block");

                        List<Entities_SRF_PO_Others> list = new List<Entities_SRF_PO_Others>();
                        list = BLL.SRF_MT_PullOutOthers_GetAll();

                        if (list != null)
                        {
                            if (list.Count > 0)
                            {
                                gvOthers.DataSource = list.Where(item => item.IsDisabled.Contains("0")).ToList();
                                gvOthers.DataBind();
                            }
                        }
                    }

                    // NONE
                    if (string.IsNullOrEmpty(ddPullOutType.SelectedItem.Text))
                    {
                        divContainerTube.Style.Add("display", "none");
                        divICTrays.Style.Add("display", "none");
                    }
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
                string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                string query1 = string.Empty;
                string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                string query_Success = string.Empty;
                string totalQuantity = "0";
                string totalNoOfBoxes = "0";
                int queryCounter = 0;


                if (btnSubmit.Text == "PREVIEW")
                {
                    btnBack.Visible = true;

                    // IC TRAYS
                    if (ddPullOutType.SelectedItem.Text == "IC TRAYS")
                    {
                        if (gvICTrays.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvICTrays.Rows.Count; i++)
                            {
                                Label lblQuantity = (Label)gvICTrays.Rows[i].Cells[6].FindControl("lblQuantity");
                                Label lblMultiplier = (Label)gvICTrays.Rows[i].Cells[5].FindControl("lblMultiplier");
                                TextBox txtNoOfBoxes = (TextBox)gvICTrays.Rows[i].Cells[4].FindControl("txtNoOfBoxes");

                                if (!string.IsNullOrEmpty(txtNoOfBoxes.Text))
                                {
                                    if (COMMON.isNumeric(txtNoOfBoxes.Text, System.Globalization.NumberStyles.Number))
                                    {
                                        lblQuantity.Text = (int.Parse(txtNoOfBoxes.Text) * int.Parse(lblMultiplier.Text)).ToString();
                                        txtNoOfBoxes.Enabled = false;

                                        totalQuantity = (int.Parse(totalQuantity) + int.Parse(lblQuantity.Text)).ToString();
                                        totalNoOfBoxes = (int.Parse(totalNoOfBoxes) + int.Parse(txtNoOfBoxes.Text)).ToString();
                                    }
                                }
                                else
                                {
                                    txtNoOfBoxes.Text = string.Empty;
                                    txtNoOfBoxes.Enabled = false;
                                    lblQuantity.Text = string.Empty;
                                }

                                
                            }

                            Session["PO_Quantity"] = totalQuantity;
                            Session["PO_Boxes"] = totalNoOfBoxes;
                            

                            btnSubmit.Text = "SUBMIT";
                        }

                    }

                    // CONTAINER TUBES
                    if (ddPullOutType.SelectedItem.Text == "CONTAINER TUBES")
                    {
                        if (gvContainerTube.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvContainerTube.Rows.Count; i++)
                            {
                                Label lblQuantity = (Label)gvContainerTube.Rows[i].Cells[7].FindControl("lblQuantity");
                                Label lblMultiplier = (Label)gvContainerTube.Rows[i].Cells[6].FindControl("lblMultiplier");
                                Label lblNetWeight = (Label)gvContainerTube.Rows[i].Cells[5].FindControl("lblNetWeight");
                                Label lblWeightOfBox = (Label)gvContainerTube.Rows[i].Cells[2].FindControl("lblWeightOfBox");
                                TextBox txtNoOfBoxes = (TextBox)gvContainerTube.Rows[i].Cells[3].FindControl("txtNoOfBoxes");
                                TextBox txtGrossWeight = (TextBox)gvContainerTube.Rows[i].Cells[4].FindControl("txtGrossWeight");

                                double netWeight = 0;

                                if (!string.IsNullOrEmpty(txtNoOfBoxes.Text) && !string.IsNullOrEmpty(txtGrossWeight.Text))
                                {
                                    if (COMMON.isNumeric(txtNoOfBoxes.Text, System.Globalization.NumberStyles.Number) && COMMON.isNumeric(txtGrossWeight.Text, System.Globalization.NumberStyles.Number))
                                    {
                                        lblNetWeight.Text = Decimal.Truncate(Decimal.Parse((double.Parse(txtGrossWeight.Text) - (double.Parse(txtNoOfBoxes.Text) * double.Parse(lblWeightOfBox.Text))).ToString())).ToString();
                                        netWeight = double.Parse(txtGrossWeight.Text) - (double.Parse(txtNoOfBoxes.Text) * double.Parse(lblWeightOfBox.Text));
                                        lblQuantity.Text = Decimal.Truncate(Decimal.Parse((double.Parse(netWeight.ToString()) * double.Parse(lblMultiplier.Text)).ToString())).ToString();

                                        totalQuantity = Decimal.Truncate(Decimal.Parse((double.Parse(totalQuantity) + double.Parse(lblQuantity.Text)).ToString())).ToString();
                                        totalNoOfBoxes = Decimal.Truncate(Decimal.Parse((double.Parse(totalNoOfBoxes) + double.Parse(txtNoOfBoxes.Text)).ToString())).ToString();

                                        txtNoOfBoxes.Enabled = false;
                                        txtGrossWeight.Enabled = false;
                                    }
                                }
                                else
                                {
                                    txtNoOfBoxes.Text = string.Empty;
                                    txtGrossWeight.Text = string.Empty;
                                    txtNoOfBoxes.Enabled = false;
                                    txtGrossWeight.Enabled = false;
                                    lblNetWeight.Text = string.Empty;
                                    lblQuantity.Text = string.Empty;
                                }
                                
                            }

                            Session["PO_Quantity"] = totalQuantity;
                            Session["PO_Boxes"] = totalNoOfBoxes;


                            btnSubmit.Text = "SUBMIT";

                        }
                    }

                    // OTHERS
                    if (ddPullOutType.SelectedItem.Text == "EMPTY BLACK TRAY" || ddPullOutType.SelectedItem.Text == "DANPLA BOX" || ddPullOutType.SelectedItem.Text == "ROBUST REEL")
                    {
                        if (gvOthers.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvOthers.Rows.Count; i++)
                            {
                                Label lblRefId = (Label)gvOthers.Rows[i].Cells[0].FindControl("lblRefId");
                                Label lblSpecification = (Label)gvOthers.Rows[i].Cells[1].FindControl("lblSpecification");
                                Label lblSRFItemName = (Label)gvOthers.Rows[i].Cells[3].FindControl("lblSRFItemName");
                                TextBox txtQuantity = (TextBox)gvOthers.Rows[i].Cells[2].FindControl("txtQuantity");

                                double netWeight = 0;

                                if (!string.IsNullOrEmpty(txtQuantity.Text))
                                {
                                    if (COMMON.isNumeric(txtQuantity.Text, System.Globalization.NumberStyles.Number))
                                    {
                                        totalQuantity = Decimal.Truncate(Decimal.Parse((double.Parse(totalQuantity) + double.Parse(txtQuantity.Text)).ToString())).ToString();

                                        txtQuantity.Enabled = false;
                                    }
                                }
                                else
                                {
                                    txtQuantity.Text = string.Empty;
                                }

                            }

                            Session["PO_Quantity"] = totalQuantity;



                            btnSubmit.Text = "SUBMIT";

                        }
                    }



                }
                else // SUBMIT TRANSACTION
                {
                    string ctrno = string.Empty;

                    if (!String.IsNullOrEmpty(Request.QueryString["SRFNo_PO_From_Inquiry"].ToString()))
                    {
                        ctrno = CryptorEngine.Decrypt(Request.QueryString["SRFNo_PO_From_Inquiry"].ToString().Replace(" ", "+"), true);
                    }
                    else
                    {
                        ctrno = setSRF_PO_NumberWithPrefix();
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    // IC TRAYS
                    if (ddPullOutType.SelectedItem.Text == "IC TRAYS")
                    {
                        
                        if (gvICTrays.Rows.Count > 0)
                        {
                            int id_counter_for_excel = 6;

                            //HEAD
                            if (!String.IsNullOrEmpty(Request.QueryString["SRFNo_PO_From_Inquiry"].ToString()))
                            {
                                
                                //UPDATE
                                if (Session["prodManagerStat"].ToString() == "2")
                                {
                                    query1 += "UPDATE SRF_TRANSACTION_PO_Head SET ProdManager = NULL, StatProdManager = NULL, DOAProdManager = NULL, TotalBoxes = '" + Session["PO_Boxes"].ToString() + "',TotaQuantity = '" + Session["PO_Quantity"].ToString() + "', UpdatedBy = '" + Session["LcRefId"].ToString() + "', UpdatedDate = GETDATE() WHERE CTRLNo = '" + ctrno + "' ";
                                }
                                else if (Session["buyerStat"].ToString() == "2")
                                {
                                    query1 += "UPDATE SRF_TRANSACTION_PO_Head SET Buyer = NULL, StatBuyer = NULL, DOABuyer = NULL, TotalBoxes = '" + Session["PO_Boxes"].ToString() + "',TotaQuantity = '" + Session["PO_Quantity"].ToString() + "', UpdatedBy = '" + Session["LcRefId"].ToString() + "', UpdatedDate = GETDATE() WHERE CTRLNo = '" + ctrno + "' ";
                                }
                                else
                                {
                                    query1 += "UPDATE SRF_TRANSACTION_PO_Head SET TotalBoxes = '" + Session["PO_Boxes"].ToString() + "',TotaQuantity = '" + Session["PO_Quantity"].ToString() + "', UpdatedBy = '" + Session["LcRefId"].ToString() + "', UpdatedDate = GETDATE() WHERE CTRLNo = '" + ctrno + "' ";
                                }
                            }
                            else
                            {
                                //INSERT
                                query1 += "INSERT INTO SRF_TRANSACTION_PO_Head (CTRLNo,TotalBoxes,TotaQuantity,Requester,TransactionDate,Type,DivisionDisplay,Supplier) " +
                                          "VALUES ('" + ctrno + "','" + Session["PO_Boxes"].ToString() + "','" + Session["PO_Quantity"].ToString() + "','" + Session["LcRefId"].ToString() + "',GETDATE(),'IC TRAYS','" + ddDivision.SelectedItem.Text.ToUpper() + "','" + ddSupplier.SelectedValue + "') ";
                            }
                            queryCounter++;

                            for (int i = 0; i < gvICTrays.Rows.Count; i++)
                            {
                                Label lblRefId = (Label)gvICTrays.Rows[i].Cells[0].FindControl("lblRefId");
                                Label lblSpecification = (Label)gvICTrays.Rows[i].Cells[1].FindControl("lblSpecification");
                                Label lblBoxType = (Label)gvICTrays.Rows[i].Cells[2].FindControl("lblBoxType");
                                Label lblSize = (Label)gvICTrays.Rows[i].Cells[3].FindControl("lblSize");
                                Label lblQuantity = (Label)gvICTrays.Rows[i].Cells[6].FindControl("lblQuantity");
                                Label lblMultiplier = (Label)gvICTrays.Rows[i].Cells[5].FindControl("lblMultiplier");
                                TextBox txtNoOfBoxes = (TextBox)gvICTrays.Rows[i].Cells[4].FindControl("txtNoOfBoxes");                                

                                //DETAILS
                                if (!String.IsNullOrEmpty(Request.QueryString["SRFNo_PO_From_Inquiry"].ToString()))
                                {
                                    //UPDATE
                                    query1 += "UPDATE SRF_TRANSACTION_PO_Details SET NoOfBoxes = '" + txtNoOfBoxes.Text + "', Quantity = '" + lblQuantity.Text + "' WHERE RefId = '" + lblRefId.Text.Trim() + "' ";
                                    queryCounter++;
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(txtNoOfBoxes.Text))
                                    {
                                        //INSERT
                                        query1 += "INSERT INTO SRF_TRANSACTION_PO_Details (CTRLNo, Specification, BoxType, Size, Multiplier, NoOfBoxes, Quantity) " +
                                                  "VALUES ('" + ctrno + "','" + lblSpecification.Text + "','" + lblBoxType.Text + "','" + lblSize.Text + "','" + lblMultiplier.Text + "','" + txtNoOfBoxes.Text + "','" + lblQuantity.Text + "') ";

                                        queryCounter++;
                                    }
                                                                        
                                }
                                
                            }

                            //--------------------------------------------------------------------------------------------------------------------------------------
                            // CREATE THE INITIAL DIRECTORY
                            if (!System.IO.Directory.Exists(Server.MapPath("~/SRF_PO_Request/" + ctrno)))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/SRF_PO_Request/" + ctrno));
                            }

                            // SRF_PO DRAFT XLS
                            if (!System.IO.File.Exists(Server.MapPath("~/SRF_PO_Request/" + ctrno + "/" + ctrno + "_SRF_PO_Request.xlsx")))
                            {
                                System.IO.File.Copy(Server.MapPath("~/SRF_PO_XLS/IC_Tray_Draft.xlsx"), Server.MapPath("~/SRF_PO_Request/" + ctrno + "/" + ctrno + "_SRF_PO_Request.xlsx"));
                            }
                            else
                            {
                                System.IO.File.Delete(Server.MapPath("~/SRF_PO_Request/" + ctrno + "/" + ctrno + "_SRF_PO_Request.xlsx"));
                                System.IO.File.Copy(Server.MapPath("~/SRF_PO_XLS/IC_Tray_Draft.xlsx"), Server.MapPath("~/SRF_PO_Request/" + ctrno + "/" + ctrno + "_SRF_PO_Request.xlsx"));

                            }

                            string pathNew = Server.MapPath("~/SRF_PO_Request/" + ctrno + "/" + ctrno + "_SRF_PO_Request.xlsx");
                            Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew);
                            FileStream fsNew = new FileStream(pathNew, FileMode.Open);
                            using (SLDocument draftNew = new SLDocument(fsNew, "Sheet1"))
                            {
                                for (int i = 0; i < gvICTrays.Rows.Count; i++)
                                {
                                    Label lblRefId = (Label)gvICTrays.Rows[i].Cells[0].FindControl("lblRefId");
                                    Label lblSpecification = (Label)gvICTrays.Rows[i].Cells[1].FindControl("lblSpecification");
                                    Label lblBoxType = (Label)gvICTrays.Rows[i].Cells[2].FindControl("lblBoxType");
                                    Label lblSize = (Label)gvICTrays.Rows[i].Cells[3].FindControl("lblSize");
                                    Label lblQuantity = (Label)gvICTrays.Rows[i].Cells[6].FindControl("lblQuantity");
                                    Label lblMultiplier = (Label)gvICTrays.Rows[i].Cells[5].FindControl("lblMultiplier");
                                    TextBox txtNoOfBoxes = (TextBox)gvICTrays.Rows[i].Cells[4].FindControl("txtNoOfBoxes");

                                    if (!string.IsNullOrEmpty(txtNoOfBoxes.Text))
                                    {
                                        draftNew.SetCellValue("B" + id_counter_for_excel.ToString(), lblSpecification.Text.ToUpper()); //SPECS
                                        draftNew.SetCellValue("C" + id_counter_for_excel.ToString(), lblBoxType.Text.ToUpper()); //BOXT TYPE
                                        draftNew.SetCellValue("D" + id_counter_for_excel.ToString(), lblSize.Text.ToUpper()); //SIZE
                                        draftNew.SetCellValue("E" + id_counter_for_excel.ToString(), txtNoOfBoxes.Text.ToUpper()); //NO. OF BOXES
                                        draftNew.SetCellValue("F" + id_counter_for_excel.ToString(), lblMultiplier.Text.ToUpper()); //MULTIPLIER
                                        draftNew.SetCellValue("G" + id_counter_for_excel.ToString(), lblQuantity.Text.ToUpper()); //QUANTITY

                                        draftNew.SetCellValue("A3", ddDivision.SelectedItem.Text.ToUpper()); //DIVISION 
                                        draftNew.SetCellValue("E20", Session["PO_Boxes"].ToString()); //TOTAL NUMBER OF BOXES
                                        draftNew.SetCellValue("G20", Session["PO_Quantity"].ToString()); //TOTAL QUANTITY
                                        draftNew.SetCellValue("B25", Session["UserFullName"].ToString().ToUpper() + "\n" + DateTime.Now.ToShortDateString()); //REQUESTER

                                        id_counter_for_excel++;
                                    }

                                }

                                fsNew.Close();
                                draftNew.SaveAs(pathNew);
                            }

                            //--------------------------------------------------------------------------------------------------------------------------------------

                            query_Success = BLL.SRF_TRANSACTION_PO_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                            if (query_Success == queryCounter.ToString())
                            {

                                Session["successMessage"] = "CTRL NUMBER : <b>" + ctrno + "</b> HAS BEEN SUCCESSFULLY SAVED.";
                                Session["successTransactionName"] = "SRF_PO_REQUESTENTRY";
                                Session["successReturnPage"] = "SRF_PO_RequestEntry.aspx?SRFNo_PO_From_Inquiry=";
                                Response.Redirect("SuccessPage.aspx", false);
                            }

                        }
                    }


                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    // CONTAINER TUBES
                    if (ddPullOutType.SelectedItem.Text == "CONTAINER TUBES")
                    {
                        if (gvContainerTube.Rows.Count > 0)
                        {
                            int id_counter_for_excel = 6;

                            //HEAD
                            if (!String.IsNullOrEmpty(Request.QueryString["SRFNo_PO_From_Inquiry"].ToString()))
                            {
                                
                                //UPDATE                                 
                                if (Session["prodManagerStat"].ToString() == "2")
                                {                                    
                                    query1 += "UPDATE SRF_TRANSACTION_PO_Head SET ProdManager = NULL, StatProdManager = NULL, DOAProdManager = NULL, TotalBoxes = '" + Session["PO_Boxes"].ToString() + "',TotaQuantity = '" + Session["PO_Quantity"].ToString() + "', UpdatedBy = '" + Session["LcRefId"].ToString() + "', UpdatedDate = GETDATE() WHERE CTRLNo = '" + ctrno + "' ";
                                }
                                else if (Session["buyerStat"].ToString() == "2")
                                {
                                    query1 += "UPDATE SRF_TRANSACTION_PO_Head SET Buyer = NULL, StatBuyer = NULL, DOABuyer = NULL, TotalBoxes = '" + Session["PO_Boxes"].ToString() + "',TotaQuantity = '" + Session["PO_Quantity"].ToString() + "', UpdatedBy = '" + Session["LcRefId"].ToString() + "', UpdatedDate = GETDATE() WHERE CTRLNo = '" + ctrno + "' ";                                    
                                }
                                else
                                {
                                    query1 += "UPDATE SRF_TRANSACTION_PO_Head SET TotalBoxes = '" + Session["PO_Boxes"].ToString() + "',TotaQuantity = '" + Session["PO_Quantity"].ToString() + "', UpdatedBy = '" + Session["LcRefId"].ToString() + "', UpdatedDate = GETDATE() WHERE CTRLNo = '" + ctrno + "' ";
                                }
                            }
                            else
                            {
                                //INSERT
                                query1 += "INSERT INTO SRF_TRANSACTION_PO_Head (CTRLNo,TotalBoxes,TotaQuantity,Requester,TransactionDate,Type,DivisionDisplay,Supplier) " +
                                          "VALUES ('" + ctrno + "','" + Session["PO_Boxes"].ToString() + "','" + Session["PO_Quantity"].ToString() + "','" + Session["LcRefId"].ToString() + "',GETDATE(),'CONTAINER TUBES','" + ddDivision.SelectedItem.Text.ToUpper() + "','" + ddSupplier.SelectedValue + "') ";
                            }
                            queryCounter++;

                            for (int i = 0; i < gvContainerTube.Rows.Count; i++)
                            {
                                Label lblRefId = (Label)gvContainerTube.Rows[i].Cells[0].FindControl("lblRefId");
                                Label lblSpecification = (Label)gvContainerTube.Rows[i].Cells[1].FindControl("lblSpecification");
                                Label lblWeightOfBox = (Label)gvContainerTube.Rows[i].Cells[2].FindControl("lblWeightOfBox");
                                Label lblNetWeight = (Label)gvContainerTube.Rows[i].Cells[5].FindControl("lblNetWeight");
                                Label lblMultiplier = (Label)gvContainerTube.Rows[i].Cells[6].FindControl("lblMultiplier");
                                Label lblQuantity = (Label)gvContainerTube.Rows[i].Cells[7].FindControl("lblQuantity");
                                TextBox txtNoOfBoxes = (TextBox)gvContainerTube.Rows[i].Cells[3].FindControl("txtNoOfBoxes");
                                TextBox txtGrossWeight = (TextBox)gvContainerTube.Rows[i].Cells[4].FindControl("txtGrossWeight");

                                //DETAILS
                                if (!String.IsNullOrEmpty(Request.QueryString["SRFNo_PO_From_Inquiry"].ToString()))
                                {
                                    //UPDATE
                                    query1 += "UPDATE SRF_TRANSACTION_PO_Details SET NoOfBoxes = '" + txtNoOfBoxes.Text + "', Quantity = '" + lblQuantity.Text + "', GrossWeight = '" + txtGrossWeight.Text + "', NetWeight = '" + lblNetWeight.Text + "' WHERE RefId = '" + lblRefId.Text.Trim() + "' ";
                                    queryCounter++;
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(txtNoOfBoxes.Text))
                                    {
                                        //INSERT
                                        query1 += "INSERT INTO SRF_TRANSACTION_PO_Details (CTRLNo, Specification, WeightOfBox, NoOfBoxes, GrossWeight, NetWeight, Multiplier, Quantity) " +
                                                  "VALUES ('" + ctrno + "','" + lblSpecification.Text + "','" + lblWeightOfBox.Text + "','" + txtNoOfBoxes.Text + "','" + txtGrossWeight.Text + "','" + lblNetWeight.Text + "','" + lblMultiplier.Text + "','" + lblQuantity.Text + "') ";

                                        queryCounter++;
                                    }

                                }

                                
                            }

                            //--------------------------------------------------------------------------------------------------------------------------------------
                            // CREATE THE INITIAL DIRECTORY
                            if (!System.IO.Directory.Exists(Server.MapPath("~/SRF_PO_Request/" + ctrno)))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/SRF_PO_Request/" + ctrno));
                            }

                            // SRF_PO DRAFT XLS
                            if (!System.IO.File.Exists(Server.MapPath("~/SRF_PO_Request/" + ctrno + "/" + ctrno + "_SRF_PO_Request.xlsx")))
                            {
                                System.IO.File.Copy(Server.MapPath("~/SRF_PO_XLS/Container_Tube_Draft.xlsx"), Server.MapPath("~/SRF_PO_Request/" + ctrno + "/" + ctrno + "_SRF_PO_Request.xlsx"));
                            }
                            else
                            {
                                System.IO.File.Delete(Server.MapPath("~/SRF_PO_Request/" + ctrno + "/" + ctrno + "_SRF_PO_Request.xlsx"));
                                System.IO.File.Copy(Server.MapPath("~/SRF_PO_XLS/Container_Tube_Draft.xlsx"), Server.MapPath("~/SRF_PO_Request/" + ctrno + "/" + ctrno + "_SRF_PO_Request.xlsx"));

                            }

                            string pathNew = Server.MapPath("~/SRF_PO_Request/" + ctrno + "/" + ctrno + "_SRF_PO_Request.xlsx");
                            Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew);
                            FileStream fsNew = new FileStream(pathNew, FileMode.Open);
                            using (SLDocument draftNew = new SLDocument(fsNew, "Sheet1"))
                            {
                                for (int i = 0; i < gvContainerTube.Rows.Count; i++)
                                {
                                    Label lblRefId = (Label)gvContainerTube.Rows[i].Cells[0].FindControl("lblRefId");
                                    Label lblSpecification = (Label)gvContainerTube.Rows[i].Cells[1].FindControl("lblSpecification");
                                    Label lblWeightOfBox = (Label)gvContainerTube.Rows[i].Cells[2].FindControl("lblWeightOfBox");
                                    Label lblNetWeight = (Label)gvContainerTube.Rows[i].Cells[5].FindControl("lblNetWeight");
                                    Label lblMultiplier = (Label)gvContainerTube.Rows[i].Cells[6].FindControl("lblMultiplier");
                                    Label lblQuantity = (Label)gvContainerTube.Rows[i].Cells[7].FindControl("lblQuantity");
                                    TextBox txtNoOfBoxes = (TextBox)gvContainerTube.Rows[i].Cells[3].FindControl("txtNoOfBoxes");
                                    TextBox txtGrossWeight = (TextBox)gvContainerTube.Rows[i].Cells[4].FindControl("txtGrossWeight");

                                    if (!string.IsNullOrEmpty(txtNoOfBoxes.Text))
                                    {
                                        draftNew.SetCellValue("B" + id_counter_for_excel.ToString(), lblSpecification.Text.ToUpper()); //SPECS
                                        draftNew.SetCellValue("C" + id_counter_for_excel.ToString(), lblWeightOfBox.Text.ToUpper()); //WEIGHT OF BOX
                                        draftNew.SetCellValue("D" + id_counter_for_excel.ToString(), txtNoOfBoxes.Text.ToUpper()); //NO OF BOXES
                                        draftNew.SetCellValue("E" + id_counter_for_excel.ToString(), txtGrossWeight.Text.ToUpper()); //GROSS WEIGHT
                                        draftNew.SetCellValue("F" + id_counter_for_excel.ToString(), lblNetWeight.Text.ToUpper()); //NET WEIGHT
                                        draftNew.SetCellValue("G" + id_counter_for_excel.ToString(), lblMultiplier.Text.ToUpper()); //MULTIPLIER
                                        draftNew.SetCellValue("H" + id_counter_for_excel.ToString(), lblQuantity.Text.ToUpper()); //QUANTITY

                                        draftNew.SetCellValue("A3", ddDivision.SelectedItem.Text.ToUpper()); //DIVISION 
                                        draftNew.SetCellValue("D20", Session["PO_Boxes"].ToString()); //TOTAL NUMBER OF BOXES
                                        draftNew.SetCellValue("H20", Session["PO_Quantity"].ToString()); //TOTAL QUANTITY
                                        draftNew.SetCellValue("B25", Session["UserFullName"].ToString().ToUpper() + "\n" + DateTime.Now.ToShortDateString()); //REQUESTER

                                        id_counter_for_excel++;

                                    }

                                }

                                fsNew.Close();
                                draftNew.SaveAs(pathNew);
                            }

                            //--------------------------------------------------------------------------------------------------------------------------------------

                            query_Success = BLL.SRF_TRANSACTION_PO_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                            if (query_Success == queryCounter.ToString())
                            {

                                Session["successMessage"] = "CTRL NUMBER : <b>" + ctrno + "</b> HAS BEEN SUCCESSFULLY SAVED.";
                                Session["successTransactionName"] = "SRF_PO_REQUESTENTRY";
                                Session["successReturnPage"] = "SRF_PO_RequestEntry.aspx?SRFNo_PO_From_Inquiry=";
                                Response.Redirect("SuccessPage.aspx", false);
                            }

                        }

                    }

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    // OTHERS
                    if (ddPullOutType.SelectedItem.Text == "EMPTY BLACK TRAY" || ddPullOutType.SelectedItem.Text == "DANPLA BOX" || ddPullOutType.SelectedItem.Text == "ROBUST REEL")
                    {
                        if (gvOthers.Rows.Count > 0)
                        {
                            int id_counter_for_excel = 6;

                            //HEAD
                            if (!String.IsNullOrEmpty(Request.QueryString["SRFNo_PO_From_Inquiry"].ToString()))
                            {
                                
                                //UPDATE
                                query1 += "UPDATE SRF_TRANSACTION_PO_Head SET TotaQuantity = '" + Session["PO_Quantity"].ToString() + "', UpdatedBy = '" + Session["LcRefId"].ToString() + "', UpdatedDate = GETDATE() WHERE CTRLNo = '" + ctrno + "' ";

                                if (Session["prodManagerStat"].ToString() == "2")
                                {
                                    query1 += "UPDATE SRF_TRANSACTION_PO_Head SET ProdManager = NULL, StatProdManager = NULL, DOAProdManager = NULL, TotaQuantity = '" + Session["PO_Quantity"].ToString() + "', UpdatedBy = '" + Session["LcRefId"].ToString() + "', UpdatedDate = GETDATE() WHERE CTRLNo = '" + ctrno + "' ";                                    
                                }
                                else if (Session["buyerStat"].ToString() == "2")
                                {
                                    query1 += "UPDATE SRF_TRANSACTION_PO_Head SET Buyer = NULL, StatBuyer = NULL, DOABuyer = NULL, TotaQuantity = '" + Session["PO_Quantity"].ToString() + "', UpdatedBy = '" + Session["LcRefId"].ToString() + "', UpdatedDate = GETDATE() WHERE CTRLNo = '" + ctrno + "' ";                                    
                                }
                                else
                                {
                                    query1 += "UPDATE SRF_TRANSACTION_PO_Head SET TotaQuantity = '" + Session["PO_Quantity"].ToString() + "', UpdatedBy = '" + Session["LcRefId"].ToString() + "', UpdatedDate = GETDATE() WHERE CTRLNo = '" + ctrno + "' ";
                                }
                            }
                            else
                            {
                                //INSERT
                                query1 += "INSERT INTO SRF_TRANSACTION_PO_Head (CTRLNo,TotaQuantity,Requester,TransactionDate,Type,DivisionDisplay,Supplier,StatProdManager,StatBuyer,StatSCIncharge) " +
                                          "VALUES ('" + ctrno + "','" + Session["PO_Quantity"].ToString() + "','" + Session["LcRefId"].ToString() + "',GETDATE(),'" + ddPullOutType.SelectedItem.Text.ToUpper() + "','" + ddDivision.SelectedItem.Text.ToUpper() + "','" + ddSupplier.SelectedValue + "','-1','-1','-1') ";
                               
                            }
                            queryCounter++;

                            for (int i = 0; i < gvOthers.Rows.Count; i++)
                            {
                                Label lblRefId = (Label)gvOthers.Rows[i].Cells[0].FindControl("lblRefId");
                                Label lblSpecification = (Label)gvOthers.Rows[i].Cells[1].FindControl("lblSpecification");
                                Label lblSRFItemName = (Label)gvOthers.Rows[i].Cells[3].FindControl("lblSRFItemName");
                                TextBox txtQuantity = (TextBox)gvOthers.Rows[i].Cells[2].FindControl("txtQuantity");

                                //DETAILS
                                if (!String.IsNullOrEmpty(Request.QueryString["SRFNo_PO_From_Inquiry"].ToString()))
                                {
                                    //UPDATE
                                    query1 += "UPDATE SRF_TRANSACTION_PO_Details SET Quantity = '" + txtQuantity.Text + "' WHERE RefId = '" + lblRefId.Text.Trim() + "' ";
                                    queryCounter++;
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(txtQuantity.Text))
                                    {
                                        //INSERT
                                        query1 += "INSERT INTO SRF_TRANSACTION_PO_Details (CTRLNo, Specification, Quantity) " +
                                                  "VALUES ('" + ctrno + "','" + lblSpecification.Text + "','" + txtQuantity.Text + "') ";

                                        queryCounter++;

                                        query1 += "INSERT INTO SRF_TRANSACTION_Request (CTRLNo,Requester,Category,TotalQuantity,PullOutServiceDate,ProblemEncountered,PurposeOfPullOut,TransactionDate,Supplier) " +
                                            "VALUES ('" + setControlNumberWithPrefix_For_SRF() + "','" + Session["LcRefId"].ToString() + "','" + ConfigurationManager.AppSettings["SRF_PO_FIXED_CATEGORY"].ToString() + "','" + Session["PO_Quantity"].ToString() + "','" + DateTime.Now.ToShortDateString() + "','" + lblSRFItemName.Text.ToUpper() + "','" + ConfigurationManager.AppSettings["SRF_PO_FIXED_PURPOSE_OF_PULLOUT"].ToString() + "',GETDATE(),'" + ddSupplier.SelectedValue + "') ";

                                        query1 += "INSERT INTO SRF_TRANSACTION_Request_Details (CTRLNo,ItemName,ItemSpecification,TotalQuantity,UnitOfMeasure) " +
                                              "VALUES ('" + setControlNumberWithPrefix_For_SRF() + "','" + lblSRFItemName.Text.ToUpper() + "','" + lblSpecification.Text + "','" + Session["PO_Quantity"].ToString() + "','" + ConfigurationManager.AppSettings["SRF_PO_FIXED_UNIT_OF_MEASURE"].ToString() + "') ";

                                        query1 += "INSERT INTO SRF_TRANSACTION_Status (CTRLNo,Req_Incharge,DOAReq_Incharge,STATReq_Incharge,STATReq_Manager,STATPur_Incharge,STATPur_Manager,STATPur_DeptManager) " +
                                                  "VALUES ('" + setControlNumberWithPrefix_For_SRF() + "','" + Session["LcRefId"].ToString() + "',GETDATE(),'1','0','0','0','0') ";

                                        queryCounter++; // FOR SRF_TRANSACTION_Request
                                        queryCounter++; // FOR SRF_TRANSACTION_Request_Details
                                        queryCounter++; // FOR SRF_TRANSACTION_Status
                                    }

                                }


                            }

                            //--------------------------------------------------------------------------------------------------------------------------------------
                            // CREATE THE INITIAL DIRECTORY
                            if (!System.IO.Directory.Exists(Server.MapPath("~/SRF_PO_Request/" + ctrno)))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/SRF_PO_Request/" + ctrno));
                            }

                            // SRF_PO DRAFT XLS
                            if (!System.IO.File.Exists(Server.MapPath("~/SRF_PO_Request/" + ctrno + "/" + ctrno + "_SRF_PO_Request.xlsx")))
                            {
                                System.IO.File.Copy(Server.MapPath("~/SRF_PO_XLS/Others_Draft.xlsx"), Server.MapPath("~/SRF_PO_Request/" + ctrno + "/" + ctrno + "_SRF_PO_Request.xlsx"));
                            }
                            else
                            {
                                System.IO.File.Delete(Server.MapPath("~/SRF_PO_Request/" + ctrno + "/" + ctrno + "_SRF_PO_Request.xlsx"));
                                System.IO.File.Copy(Server.MapPath("~/SRF_PO_XLS/Others_Draft.xlsx"), Server.MapPath("~/SRF_PO_Request/" + ctrno + "/" + ctrno + "_SRF_PO_Request.xlsx"));

                            }

                            string pathNew = Server.MapPath("~/SRF_PO_Request/" + ctrno + "/" + ctrno + "_SRF_PO_Request.xlsx");
                            Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew);
                            FileStream fsNew = new FileStream(pathNew, FileMode.Open);
                            using (SLDocument draftNew = new SLDocument(fsNew, "Sheet1"))
                            {
                                for (int i = 0; i < gvOthers.Rows.Count; i++)
                                {

                                    Label lblRefId = (Label)gvOthers.Rows[i].Cells[0].FindControl("lblRefId");
                                    Label lblSpecification = (Label)gvOthers.Rows[i].Cells[1].FindControl("lblSpecification");
                                    TextBox txtQuantity = (TextBox)gvOthers.Rows[i].Cells[2].FindControl("txtQuantity");

                                    if (!string.IsNullOrEmpty(txtQuantity.Text))
                                    {
                                        draftNew.SetCellValue("B" + id_counter_for_excel.ToString(), lblSpecification.Text.ToUpper()); //SPECS
                                        draftNew.SetCellValue("C" + id_counter_for_excel.ToString(), txtQuantity.Text.ToUpper()); //QUANTITY

                                        draftNew.SetCellValue("A3", ddDivision.SelectedItem.Text.ToUpper()); //DIVISION 
                                        draftNew.SetCellValue("C20", Session["PO_Quantity"].ToString()); //TOTAL QUANTITY
                                        draftNew.SetCellValue("B24", Session["UserFullName"].ToString().ToUpper() + "\n" + DateTime.Now.ToShortDateString()); //REQUESTER
                                        draftNew.SetCellValue("A2", "PULL OUT OF " + ddPullOutType.SelectedItem.Text.ToUpper()); //DIVISION 

                                        id_counter_for_excel++;

                                    }

                                }

                                fsNew.Close();
                                draftNew.SaveAs(pathNew);
                            }

                            //--------------------------------------------------------------------------------------------------------------------------------------

                            query_Success = BLL.SRF_TRANSACTION_PO_SQLTransaction(queryBeginPart + query1 + queryEndPart).ToString();

                            if (query_Success == queryCounter.ToString())
                            {

                                Session["successMessage"] = "CTRL NUMBER : <b>" + ctrno + "</b> HAS BEEN SUCCESSFULLY SAVED.";
                                Session["successTransactionName"] = "SRF_PO_REQUESTENTRY";
                                Session["successReturnPage"] = "SRF_PO_RequestEntry.aspx?SRFNo_PO_From_Inquiry=";
                                Response.Redirect("SuccessPage.aspx", false);
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


        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                btnBack.Visible = false;
                // IC TRAYS
                if (ddPullOutType.SelectedItem.Text == "IC TRAYS")
                {
                    for (int i = 0; i < gvICTrays.Rows.Count; i++)
                    {
                        TextBox txtNoOfBoxes = (TextBox)gvICTrays.Rows[i].Cells[4].FindControl("txtNoOfBoxes");

                        txtNoOfBoxes.Enabled = true;
                    }
                }

                if (ddPullOutType.SelectedItem.Text == "CONTAINER TUBES")
                {
                    for (int i = 0; i < gvContainerTube.Rows.Count; i++)
                    {
                        TextBox txtNoOfBoxes = (TextBox)gvContainerTube.Rows[i].Cells[4].FindControl("txtNoOfBoxes");
                        TextBox txtGrossWeight = (TextBox)gvContainerTube.Rows[i].Cells[5].FindControl("txtGrossWeight");

                        txtNoOfBoxes.Enabled = true;
                        txtGrossWeight.Enabled = true;
                    }
                }

                // OTHERS
                if (ddPullOutType.SelectedItem.Text == "EMPTY BLACK TRAY" || ddPullOutType.SelectedItem.Text == "DANPLA BOX" || ddPullOutType.SelectedItem.Text == "ROBUST REEL")
                {
                    for (int i = 0; i < gvOthers.Rows.Count; i++)
                    {
                        TextBox txtQuantity = (TextBox)gvOthers.Rows[i].Cells[2].FindControl("txtQuantity");

                        txtQuantity.Enabled = true;
                    }
                }

                btnSubmit.Text = "PREVIEW";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }


        private Int32 setControlNumber()
        {
            return BLL.SRF_TRANSACTION_PO_Request_Count(DateTime.Now.Year.ToString()) + 1;
        }

        private string setSRF_PO_NumberWithPrefix()
        {
            string retVal = string.Empty;

            retVal = "SRF_PO_" + Session["DivisionCode"].ToString() + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + COMMON.controlNoZeroPrefix(setControlNumber().ToString().Length.ToString()) + setControlNumber().ToString();

            return retVal;
        }


        private string setControlNumberWithPrefix_For_SRF()
        {
            string retVal = string.Empty;

            retVal = "SRF_" + Session["DivisionCode"].ToString() + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + COMMON.controlNoZeroPrefix(setControlNumber_For_SRF().ToString().Length.ToString()) + setControlNumber_For_SRF().ToString();

            return retVal;
        }

        private Int32 setControlNumber_For_SRF()
        {
            return BLL.SRF_TRANSACTION_Request_Count(DateTime.Now.Year.ToString()) + 1;
        }



    }
}
