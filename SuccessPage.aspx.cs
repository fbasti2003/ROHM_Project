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

namespace REPI_PUR_SOFRA
{
    public partial class SuccessPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((Session["successMessage"] != null || Session["successMessage"].ToString().Length > 0) && (Session["successTransactionName"] != null || Session["successTransactionName"].ToString().Length > 0))
                {
                    lblMessage.Text = Session["successMessage"].ToString();
                    lblTransactionName.Text = Session["successTransactionName"].ToString();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string redirect = Session["successReturnPage"].ToString();

            if (Session["From_OnePage"] != null)
            {
                if (Session["From_OnePage"].ToString() == "true")
                {
                    redirect = "RFQ_OnePage.aspx";
                    if (Session["RFQ_FromOnePage"] != null)
                    {
                        Session["RFQ_FromOnePageValue"] = Session["RFQ_FromOnePage"].ToString();
                        Session["UpdatesComingFromOnePage"] = "true";
                    }
                    Session["From_OnePage"] = "";
                }
            }

            if (Session["UPDATE_From_Inquiry"] != null)
            {
                if (Session["UPDATE_From_Inquiry"].ToString() == "true")
                {
                    redirect = "RFQ_AllRequest.aspx";
                }
            }

            Session["successReturnPage"] = "";
            Session["successTransactionName"] = "";
            Session["successMessage"] = "";

            Response.Redirect(redirect);
        }
    }
}
