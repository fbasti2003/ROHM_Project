using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Configuration;

namespace REPI_PUR_SOFRA
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        BLL_Common BLL_COMMON = new BLL_Common();
        BLL_Common BLLCommon = new BLL_Common();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();
                list = BLL_COMMON.getLoginCredentials(username.Value, password.Value);                

                if (list.Count > 0)
                {
                    foreach (Entities_Common_SystemUsers entities in list)
                    {
                        int changePassword = BLL_COMMON.Common_ChangePassword_ByRefId(CryptorEngine.Encrypt(newpassword.Value, true), entities.LcRefId.ToString());

                        if (changePassword > 0)
                        {
                            Session["successMessage"] = "PASSWORD FOR USERNAME : <b>" + username.Value + "</b> HAS BEEN SUCCESSFULLY CHANGED.";
                            Session["successTransactionName"] = "CHANGE PASSWORD FORM";
                            Session["successReturnPage"] = "Default.aspx";

                            Response.Redirect("SuccessPage.aspx");
                        }
                    }                    

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Invalid username or password. Please enter your valid current username and password.');", true);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}
