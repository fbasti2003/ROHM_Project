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
//using REPI_PUR_SOFRA.App_Code.BLL;
//using REPI_PUR_SOFRA.App_Code.ENTITIES;
using System.Collections.Generic;
using System.IO;

namespace REPI_PUR_SOFRA
{
    public partial class SRF_MT_LOA_Distribution : System.Web.UI.Page
    {
        BLL_SRF BLL = new BLL_SRF();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    txtFrom.Text = DateTime.Today.AddDays(-7).ToString("MM/dd/yyyy");
                    txtTo.Text = DateTime.Today.AddDays(7).ToString("MM/dd/yyyy");

                    // call submit button to load initial record
                    btnSubmit_Click(sender, e);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                }
            }
        }
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities_SRF_LOA> list = new List<Entities_SRF_LOA>();
                list = BLL.SRF_TRANSACTION_LOA_DISTRIBUTION(txtFrom.Text.Trim(), txtTo.Text.Trim());

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        gvData.Visible = true;
                        gvData.DataSource = list;
                        gvData.DataBind();
                    }
                    else
                    {
                        gvData.Visible = false;
                        gvData.EmptyDataText = "NO RECORD(S) FOUND!";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvData.Rows.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Unable to export empty data.');", true);
                }
                else
                {
                    Response.Clear();
                    Response.Buffer = true;

                    Response.AddHeader("content-disposition",
                    "attachment;filename=LOA_Distribution_" + DateTime.Now.ToShortDateString() + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //Change the Header Row back to white color
                    gvData.HeaderRow.Style.Add("background-color", "#FFFFFF");
                    gvData.HeaderRow.Style.Add("color", "#000000");


                    gvData.RenderControl(hw);

                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;
            btnSubmit_Click(sender, e);
        }


        protected void gvData_OnRowDataBound(object sender, GridViewRowEventArgs e)
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



    }
}
