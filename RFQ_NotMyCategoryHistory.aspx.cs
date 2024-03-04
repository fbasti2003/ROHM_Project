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
    public partial class RFQ_NotMyCategoryHistory : System.Web.UI.Page
    {

        BLL_RFQ BLL = new BLL_RFQ();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        List<Entities_RFQ_RequestEntry> listUpdatedBy = new List<Entities_RFQ_RequestEntry>();
                        Entities_RFQ_RequestEntry entityUpdatedBy = new Entities_RFQ_RequestEntry();
                        listUpdatedBy = BLL.RFQ_HistoryOfUpdates_GetAll_UpdatedBy(entityUpdatedBy).ToList().OrderBy(itm => itm.HistoryUpdatedBy).ToList();

                        if (listUpdatedBy != null)
                        {
                            if (listUpdatedBy.Count > 0)
                            {
                                ddUpdatedBy.Items.Add("");

                                foreach (Entities_RFQ_RequestEntry entity in listUpdatedBy)
                                {
                                    ListItem item = new ListItem();
                                    item.Text = entity.HistoryUpdatedBy + " - " + entity.HistoryUpdatedByUsername;
                                    item.Value = entity.HistoryUpdatedByID;

                                    ddUpdatedBy.Items.Add(item);
                                }
                            }
                        }

                        btnSubmit_Click(sender, e);
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

                List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                list = null;

                entity.SearchCriteria = txtSearch.Text;
                list = BLL.RFQ_TRANSACTION_HistoryOfUpdates(entity);                

                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        gvData.DataSource = list;
                        gvData.DataBind();
                        gvData.Visible = true;
                    }
                    else
                    {
                        gvData.Visible = false;
                    }
                }
                else
                {
                    gvData.Visible = false;
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
            }
        }

    }
}
