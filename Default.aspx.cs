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
    public partial class _Default : System.Web.UI.Page
    {
        BLL_Common BLL_COMMON = new BLL_Common();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            //username.Value = CryptorEngine.Decrypt("GFp2Vr8adcRNrHrCxG8A+AO+KOSEkQ+r", true);
        }        

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();
                list = BLL_COMMON.getLoginCredentials(username.Value, password.Value);

                if (list.Count > 0)
                {
                    foreach (Entities_Common_SystemUsers entities in list)
                    {
                        Session["LcRefId"] = entities.LcRefId.ToString();
                        Session["Username"] = username.Value;
                        Session["UserFullName"] = CryptorEngine.Decrypt(entities.FullName, true);
                        Session["Section"] = entities.Section.ToString();
                        Session["Department"] = entities.Department.ToString();
                        Session["Division"] = entities.Division.ToString();
                        Session["PC"] = entities.PC.ToString();
                        Session["HQ"] = entities.HQ.ToString();
                        Session["DivisionCode"] = entities.DivisionCode.ToString();
                        Session["isDisabled"] = entities.IsDisabled.ToString();
                        Session["CategoryAccess"] = entities.Category.ToString();
                        Session["CategoryName"] = entities.CategoryString.ToString();

                        Session["SectionName"] = entities.SectionName.ToString();
                        Session["DepartmentName"] = entities.DepartmentName.ToString();
                        Session["DivisionName"] = entities.DivisionName.ToString();
                        Session["PcName"] = entities.PcName.ToString();
                        Session["HqName"] = entities.HqName.ToString();
                        Session["LocalNumber"] = entities.LocalNumber.ToString();
                        Session["UserEmail"] = entities.EmailAddress.ToString();

                        Session["URF_Prod_SecManager"] = COMMON.isUserAllowed(entities.LcRefId.ToString(), "401").ToString();
                        Session["URF_Prod_DeptManager"] = COMMON.isUserAllowed(entities.LcRefId.ToString(), "402").ToString();
                        Session["URF_Prod_DivManager"] = COMMON.isUserAllowed(entities.LcRefId.ToString(), "403").ToString();
                        Session["URF_Prod_HQManager"] = COMMON.isUserAllowed(entities.LcRefId.ToString(), "404").ToString();

                        Session["DRF_PurchasingBuyer"] = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim());
                        Session["DRF_PurchasingIncharge"] = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "15");
                        Session["DRF_PurchasingDeptManager"] = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "16");
                        Session["DRF_PurchasingDivManager"] = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "17");

                        Session["CRF_PurchasingBuyer"] = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim());
                        Session["CRF_PurchasingIncharge"] = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "15");
                        Session["CRF_PurchasingDeptManager"] = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "16");
                        Session["CRF_PurchasingDivManager"] = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "17");

                    }
                    if (Session["isDisabled"].ToString() == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Your account is disabled!');", true);
                    }
                    else
                    {
                        Response.Redirect("Dashboard.aspx");
                        //Response.Redirect("RFQ_AllRequest.aspx");

                        //if (username.Value == "FERDIE" || username.Value == "6983" || username.Value == "3844")
                        //{

                        //    Response.Redirect("Dashboard.aspx");
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('MAINTENANCE STILL ON GOING. THANK YOU!');", true);
                        //}
                        //if (BLLCommon.Common_checkIfUserHasTransactionsByUserId(Session["LcRefId"].ToString()).Count > 0)                        
                        //{
                        //    Response.Redirect("Dashboard.aspx");
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('Your account has no access in all transactions!');", true);
                        //}
                    }
                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "InvalidCredentials();", true);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }
    }
}
