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
    public partial class SRF_Quantity_Adjustment_History : System.Web.UI.Page
    {

        BLL_SRF BLL = new BLL_SRF();
        Common COMMON = new Common();
        BLL_Common BLL_COMMON = new BLL_Common();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    txtFrom.Text = DateTime.Today.AddMonths(-12).ToString("MM/dd/yyyy");
                    txtTo.Text = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");
                    // call submit button to load initial record
                    btnSubmit_Click(sender, e);

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                }
            }
        }


        protected void gvDetailsQuantityCorrections_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void gvDetailsQuantityCorrections_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label txtBrandMachine = (Label)e.Row.FindControl("txtBrandMachine");
                    Label txtItemName = (Label)e.Row.FindControl("txtItemName");
                    Label txtSpecification = (Label)e.Row.FindControl("txtSpecification");
                    Label lblReasonOfCorrection = (Label)e.Row.FindControl("lblReasonOfCorrection");
                    Label txtSalesInvoice = (Label)e.Row.FindControl("txtSalesInvoice");
                    Label txtRefPRPO = (Label)e.Row.FindControl("txtRefPRPO");


                    txtBrandMachine.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    txtItemName.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    txtSpecification.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    lblReasonOfCorrection.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    txtSalesInvoice.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    txtRefPRPO.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");

                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");


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

                // SET DETAILS
                List<Entities_SRF_RequestEntry> details = new List<Entities_SRF_RequestEntry>();
                Entities_SRF_RequestEntry entity = new Entities_SRF_RequestEntry();
                entity.DrFrom = txtFrom.Text;
                entity.DrTo = txtTo.Text;

                if (string.IsNullOrEmpty(txtSearch.Text) || txtSearch.Text.Length <= 0)
                {
                    details = BLL.SRF_TRANSACTION_QuantityAdjustmentHistory(entity);
                }
                else
                {
                    details = BLL.SRF_TRANSACTION_QuantityAdjustmentHistory(entity).Where(itm => itm.CtrlNo.Contains(txtSearch.Text)).ToList();
                }

                if (details != null)
                {
                    if (details.Count > 0)
                    {
                        gvDetailsQuantityCorrections.DataSource = details;
                        gvDetailsQuantityCorrections.DataBind();

                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }

        }










    }
}
