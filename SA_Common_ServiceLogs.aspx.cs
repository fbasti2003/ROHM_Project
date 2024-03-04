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
    public partial class SA_Common_ServiceLogs : System.Web.UI.Page
    {

        BLL_Common BLLCommon = new BLL_Common();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (!IsPostBack)
                    {

                        if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["PurchasingApprovalAccess"].ToString().Trim()))
                        {
                            txtFrom.Text = DateTime.Today.AddDays(-7).ToString("MM/dd/yyyy");
                            txtTo.Text = DateTime.Today.AddDays(7).ToString("MM/dd/yyyy");

                            // call submit button to load initial record
                            btnSubmit_Click(sender, e);
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }

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

                List<Entities_Common_TransactionLogs> list = new List<Entities_Common_TransactionLogs>();
                Entities_Common_TransactionLogs entity = new Entities_Common_TransactionLogs();

                entity.TransactionDateFrom = txtFrom.Text;
                entity.TransactionDateTo = txtTo.Text;

                list = BLLCommon.RFQ_TRANSACTION_TransactionLogs_ByDateRange(entity);

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        gvData.DataSource = list;
                        gvData.DataBind();
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
