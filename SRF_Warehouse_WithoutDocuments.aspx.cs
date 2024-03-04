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
    public partial class SRF_Warehouse_WithoutDocuments : System.Web.UI.Page
    {

        BLL_SRF BLL_SRF = new BLL_SRF();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                // SRF WAREHOUSE WITHOU DOCUMENTS
                //if (ConfigurationManager.AppSettings["PIPL_Temp_MT_Access"].ToString().Contains(Session["Username"].ToString()))
                //{

                //    List<Entities_SRF_RequestEntry> warehouseWithoutDocuments = new List<Entities_SRF_RequestEntry>();
                //    warehouseWithoutDocuments = BLL_SRF.SRF_TRANSACTION_WarehouseItemsWithoutDocuments();

                //    if (warehouseWithoutDocuments != null)
                //    {
                //        if (warehouseWithoutDocuments.Count > 0)
                //        {

                //            gvActualDelivery.DataSource = warehouseWithoutDocuments;
                //            gvActualDelivery.DataBind();

                //        }
                //    }


                //}

                List<Entities_SRF_RequestEntry> warehouseWithoutDocuments = new List<Entities_SRF_RequestEntry>();
                warehouseWithoutDocuments = BLL_SRF.SRF_TRANSACTION_WarehouseItemsWithoutDocuments();

                if (warehouseWithoutDocuments != null)
                {
                    if (warehouseWithoutDocuments.Count > 0)
                    {

                        gvActualDelivery.DataSource = warehouseWithoutDocuments;
                        gvActualDelivery.DataBind();

                    }
                }

            }


        }


        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (!System.IO.File.Exists(Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_SRF_Warehouse_NoDocs.xlsx")))
                {
                    System.IO.File.Copy(Server.MapPath("~/SRF_XLS/SRF_Warehouse_NoDocs.xlsx"), Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_SRF_Warehouse_NoDocs.xlsx"));
                }
                else
                {
                    System.IO.File.Delete(Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_SRF_Warehouse_NoDocs.xlsx"));
                    System.IO.File.Copy(Server.MapPath("~/SRF_XLS/SRF_Warehouse_NoDocs.xlsx"), Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_SRF_Warehouse_NoDocs.xlsx"));

                }

                int id_counter_for_excel = 5;
                string pathNew = Server.MapPath("~/SRF_XLS/" + Session["LcRefId"].ToString() + "_SRF_Warehouse_NoDocs.xlsx");
                Response.ContentType = "application/xlsx"; Response.AddHeader("content-disposition", "attachment; filename=" + pathNew);
                FileStream fsNew = new FileStream(pathNew, FileMode.Open);
                using (SLDocument draftNew = new SLDocument(fsNew, "Sheet1"))
                {
                    for (int i = 0; i < gvActualDelivery.Rows.Count; i++)
                    {

                        Label lblItemNo = (Label)gvActualDelivery.Rows[i].Cells[0].FindControl("lblItemNo");
                        Label lblRefId = (Label)gvActualDelivery.Rows[i].Cells[1].FindControl("lblRefId");
                        Label txtCTRLNo = (Label)gvActualDelivery.Rows[i].Cells[2].FindControl("txtCTRLNo");
                        Label txtItemName = (Label)gvActualDelivery.Rows[i].Cells[3].FindControl("txtItemName");
                        Label txtActualQty = (Label)gvActualDelivery.Rows[i].Cells[4].FindControl("txtActualQty");
                        Label txtDateDelivered = (Label)gvActualDelivery.Rows[i].Cells[5].FindControl("txtDateDelivered");
                        Label txtAddedBy = (Label)gvActualDelivery.Rows[i].Cells[6].FindControl("txtAddedBy");
                        Label txt8105 = (Label)gvActualDelivery.Rows[i].Cells[7].FindControl("txt8105");
                        Label txt8105ProcessDate = (Label)gvActualDelivery.Rows[i].Cells[8].FindControl("txt8105ProcessDate");


                        draftNew.SetCellValue("A" + id_counter_for_excel.ToString(), lblItemNo.Text.ToUpper()); //ITEMNO
                        draftNew.SetCellValue("B" + id_counter_for_excel.ToString(), lblRefId.Text.ToUpper()); //REFID
                        draftNew.SetCellValue("C" + id_counter_for_excel.ToString(), txtCTRLNo.Text.ToUpper());
                        draftNew.SetCellValue("D" + id_counter_for_excel.ToString(), txtItemName.Text.ToUpper());
                        draftNew.SetCellValue("E" + id_counter_for_excel.ToString(), txtActualQty.Text.ToUpper());
                        draftNew.SetCellValue("F" + id_counter_for_excel.ToString(), txtDateDelivered.Text.ToUpper());
                        draftNew.SetCellValue("G" + id_counter_for_excel.ToString(), txtAddedBy.Text.ToUpper());
                        draftNew.SetCellValue("H" + id_counter_for_excel.ToString(), txt8105.Text.ToUpper());
                        draftNew.SetCellValue("I" + id_counter_for_excel.ToString(), txt8105ProcessDate.Text.ToUpper());

                        id_counter_for_excel++;

                    }

                    fsNew.Close();
                    draftNew.SaveAs(pathNew);
                }


                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "hideDialog();", true);

                Response.Redirect("SRF_XLS/" + Session["LcRefId"].ToString() + "_SRF_Warehouse_NoDocs.xlsx", false);

                
                
               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }

        }





        protected void gvActualDelivery_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblAttachment = (Label)e.Row.FindControl("lblAttachment");
                    LinkButton lbAttachment = (LinkButton)e.Row.FindControl("lbAttachment");
                    FileUpload fuAttachment = (FileUpload)e.Row.FindControl("fuAttachment");

                    Label lblDetailsRefId = (Label)e.Row.FindControl("lblDetailsRefId");
                    TextBox txtLOA8105Number = (TextBox)e.Row.FindControl("txtLOA8105Number");
                    TextBox txt8105ProcessDate = (TextBox)e.Row.FindControl("txt8105ProcessDate");

                    if (!string.IsNullOrEmpty(lblAttachment.Text))
                    {
                        lbAttachment.Visible = true;
                        fuAttachment.Visible = false;
                    }
                    else
                    {
                        lbAttachment.Visible = false;
                        fuAttachment.Visible = true;
                        lblDetailsRefId.Style.Add("color", "Red");
                        txtLOA8105Number.Style.Add("background-color", "#FFCCCB");
                        txt8105ProcessDate.Style.Add("background-color", "#FFCCCB");
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



    }
}
