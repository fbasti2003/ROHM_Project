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
    public partial class Sofra : System.Web.UI.MasterPage
    {
        BLL_Common BLL = new BLL_Common();
        Common COMMON = new Common();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserFullName"] == null && Session["LcRefId"] == null)
                {
                    lblUser.Text = string.Empty;
                }
                else
                {
                    
                        var PIPL_Impex_Access = ConfigurationManager.AppSettings["PIPL_Temp_Sir_Renz"];
                        if (Session["Username"].ToString() == PIPL_Impex_Access.ToString() || Session["Username"].ToString() == "057" || Session["Username"].ToString() == "5057" || Session["Username"].ToString() == "01086")
                        {
                            li_pipl_AllRequest.Visible = true;
                            li_mt_company.Visible = true;
                            li_mt_modeofshipment.Visible = true;
                            li_mt_commercialvalue.Visible = true;
                            li_mt_tradeterms.Visible = true;
                            li_mt_pickuplocation.Visible = true;
                            li_mt_packng.Visible = true;
                            li_mt_natureofgoods.Visible = true;
                            li_mt_countryoforigin.Visible = true;
                            li_mt_purpose.Visible = true;
                            li_mt_caseunit.Visible = true;
                            proforma_mt.Visible = true;
                        }
                        else
                        {
                            li_pipl_AllRequest.Visible = false;
                            li_mt_company.Visible = false;
                            li_mt_modeofshipment.Visible = false;
                            li_mt_commercialvalue.Visible = false;
                            li_mt_tradeterms.Visible = false;
                            li_mt_pickuplocation.Visible = false;
                            li_mt_packng.Visible = false;
                            li_mt_natureofgoods.Visible = false;
                            li_mt_countryoforigin.Visible = false;
                            li_mt_purpose.Visible = false;
                            li_mt_caseunit.Visible = false;
                            proforma_mt.Visible = false;
                        }


                        if (Session["Username"].ToString() == PIPL_Impex_Access.ToString() || Session["Username"].ToString() == "13292" || BLL.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString()).Count > 0)
                        {
                            li_srf_purposeofpullout.Visible = true;
                            li_srf_loa.Visible = true;
                            li_srf_loa_distribution.Visible = true;
                            srf_mt.Visible = true;
                            li_srf_pullout_container_tubes.Visible = true;
                            li_srf_pullout_ic_trays.Visible = true;
                            li_srf_pullout_others.Visible = true;
                            li_srf_quantity_adjustment_history.Visible = true;
                        }
                        else
                        {
                            li_srf_purposeofpullout.Visible = false;
                            li_srf_loa.Visible = false;
                            li_srf_loa_distribution.Visible = false;
                            srf_mt.Visible = false;
                            li_srf_pullout_container_tubes.Visible = false;
                            li_srf_pullout_ic_trays.Visible = false;
                            li_srf_pullout_others.Visible = false;
                            li_srf_quantity_adjustment_history.Visible = false;
                        }

                        li_srf_entry.Visible = true;
                        li_srf_monitoring.Visible = true;
                        li_srf_poentry.Visible = true;
                        li_srf_pomonitoring.Visible = true;


                        if (BLL.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), "309").Count > 0 || BLL.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString()).Count > 0)
                        {
                            li_srf_warehouse.Visible = true;
                        }
                        else
                        {
                            li_srf_warehouse.Visible = false;
                        }

                        if (BLL.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), "215").Count > 0 || BLL.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), "305").Count > 0 || BLL.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["ProductionApprovalAccess"].ToString()).Count > 0 || BLL.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString()).Count > 0 || Session["Username"].ToString() == PIPL_Impex_Access.ToString() || Session["Username"].ToString() == "057")
                        {
                            li_pipl_approvalform.Visible = true;
                            li_srf_approval.Visible = true;
                            li_srf_poapproval.Visible = true;
                            
                        }
                        else
                        {
                            li_pipl_approvalform.Visible = false;
                            li_srf_approval.Visible = false;
                            li_srf_poapproval.Visible = false;

                        }

                        if (BLL.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), "306").Count > 0 || Session["Username"].ToString() == PIPL_Impex_Access.ToString() || BLL.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString()).Count > 0 || Session["Username"].ToString() == "057")
                        {
                            li_srf_allrequest.Visible = true;
                            li_srf_allrequestfreeform.Visible = true;
                            li_pipl_AllRequest.Visible = true;
                        }
                        else
                        {
                            li_srf_allrequest.Visible = false;
                            li_srf_allrequestfreeform.Visible = false;
                        }

                        if (BLL.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), "310").Count > 0 || BLL.Common_checkIfUserAccessExistByUserId(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString()).Count > 0)
                        {
                            li_srf_8105_entry.Visible = true;
                            li_srf_received_without_documents.Visible = true;
                        }
                        else
                        {
                            li_srf_8105_entry.Visible = false;
                            li_srf_received_without_documents.Visible = false;
                        }


                        ////SYSTEM ADMINISTRATION
                        if (Session["Username"].ToString() == "FERDIE")
                        {
                            //li_sa_common_useraccess.Visible = true;
                        }
                        else
                        {
                            //li_sa_common_useraccess.Visible = false;
                        }

                        // RFQS ACCESS ENTRY ------------------------------------------------------------------------------------------------------------
                        li_rfq_section.Visible = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["SectionAccess"].ToString().Trim());
                        li_rfq_department.Visible = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["DepartmentAccess"].ToString().Trim());
                        li_rfq_division.Visible = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["DivisionAccess"].ToString().Trim());
                        li_rfq_pc.Visible = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["PCAccess"].ToString().Trim());
                        li_rfq_hq.Visible = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["HQAccess"].ToString().Trim());
                        li_rfq_category.Visible = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["CategoryAccess"].ToString().Trim());
                        li_rfq_unitofmeasure.Visible = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["UnitOfMeasureAccess"].ToString().Trim());
                        li_rfq_supplier.Visible = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["SupplierAccess"].ToString().Trim());
                        li_rfq_currency.Visible = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["CurrencyAccess"].ToString().Trim());
                        li_rfq_purchasingreceiving.Visible = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["PurchasingApprovalAccess"].ToString().Trim());
                        li_rfq_myrequesterrequest.Visible = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["ProductionApprovalAccess"].ToString().Trim());
                        
                        //ERFO RECEIVING
                        li_erfo_receivingentry.Visible = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["PurchasingApprovalAccess"].ToString().Trim());

                        if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["ProductionApprovalAccess"].ToString().Trim()) || COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()))
                        {
                            li_rfq_approvalform.Visible = true;
                        }
                        else
                        {
                            li_rfq_approvalform.Visible = false;
                        }


                        if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()) ||
                            Session["Username"].ToString() == "6985" || Session["Username"].ToString() == "3844" || Session["Username"].ToString() == "1152" || Session["Username"].ToString() == "1402" || Session["Username"].ToString() == "002")
                        {
                            li_rfq_allrequest_exporttoexcel.Visible = true;
                            li_rfq_allrequest_exporttoexcel_resend.Visible = true;
                            li_rfq_allrequest_exporttoexcel_approved.Visible = true;
                            //li_rfq_onepage.Visible = true;
                            li_rfq_withnoresponse.Visible = true;
                            li_rfq_notmycategory.Visible = true;
                            li_rfq_buyer_information.Visible = true;
                            rfq_mt.Visible = true;
                            rfq_reporting.Visible = true;
                            menu_supplier_evaluation.Visible = true;
                            li_srf_poreporting.Visible = true;
                            rpt_proforma.Visible = true;
                            rpt_drf.Visible = true;
                            rpt_crf.Visible = true;
                            rpt_srf.Visible = true;
                            rpt_rfq.Visible = true;
                            rpt_urf.Visible = true;
                        }
                        else
                        {
                            li_rfq_allrequest_exporttoexcel.Visible = false;
                            li_rfq_allrequest_exporttoexcel_resend.Visible = false;
                            li_rfq_allrequest_exporttoexcel_approved.Visible = false;
                            //li_rfq_onepage.Visible = false;
                            li_rfq_withnoresponse.Visible = false;
                            li_rfq_notmycategory.Visible = false;
                            li_rfq_buyer_information.Visible = false;

                            if (li_rfq_section.Visible == true || li_rfq_department.Visible == true || li_rfq_division.Visible == true || li_rfq_pc.Visible == true || li_rfq_hq.Visible == true || li_rfq_category.Visible == true || li_rfq_unitofmeasure.Visible == true || li_rfq_supplier.Visible == true || li_rfq_currency.Visible == true)
                            {
                                rfq_mt.Visible = true;
                            }
                            else
                            {
                                rfq_mt.Visible = false;
                            }

                            rfq_reporting.Visible = false;
                            menu_supplier_evaluation.Visible = false;
                            li_srf_poreporting.Visible = false;
                            rpt_proforma.Visible = false;
                            rpt_drf.Visible = false;
                            rpt_crf.Visible = false;
                            rpt_srf.Visible = false;
                            rpt_rfq.Visible = false;
                            rpt_urf.Visible = false;
                        }


                        li_sa_common_useraccess.Visible = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["LoginCredentialsAccess"].ToString().Trim());
                        li_sa_servicelogs.Visible = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["PurchasingApprovalAccess"].ToString().Trim());
                        li_sa_systeminformation.Visible = COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["PurchasingApprovalAccess"].ToString().Trim());

                        //if (li_sa_common_useraccess.Visible == true || li_sa_servicelogs.Visible == true)
                        //{
                        //    divAdmin.Style.Add("display", "block");
                        //}
                        //else
                        //{
                        //    divAdmin.Style.Add("display", "none");
                        //}

                        // Default Access
                        li_rfq_requestentry.Visible = true;
                        li_rfq_allapprovedrequest.Visible = true;
                        li_rfq_requestinquiry.Visible = true;
                        li_rfq_statusreport.Visible = false;


                        urf_trans.Visible = true;
                        drf_trans.Visible = true;
                        crf_trans.Visible = true;

                        // URF -----------------------------------------------------------------------------
                        if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["ProductionApprovalAccess"].ToString().Trim()) || COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()))
                        {                            
                            li_urf_approvalform.Visible = true;                            
                            li_drf_approvalform.Visible = true;
                            li_crf_approvalform.Visible = true;
                            //li_erfo_requestapproval.Visible = true;                               
                        }
                        else
                        {
                            li_urf_approvalform.Visible = false;
                            li_drf_approvalform.Visible = false;
                            li_crf_approvalform.Visible = false;
                            //li_erfo_requestapproval.Visible = false;
                        }

                        if (COMMON.isUserAllowed(Session["LcRefId"].ToString(), ConfigurationManager.AppSettings["RFQToSuppliersAccess"].ToString().Trim()))
                        {
                            urf_mt.Visible = true;
                            li_urf_reason.Visible = true;
                            li_urf_receivingentry.Visible = true;

                            drf_mt.Visible = true;
                            li_drf_typesofabnormality.Visible = true;
                            li_drf_receivingentry.Visible = true;

                            crf_mt.Visible = true;
                            li_crf_mt_reason.Visible = true;
                            li_crf_receivingentry.Visible = true;

                            li_crf_allrequest.Visible = true;
                            li_drf_allrequest.Visible = true;
                            li_urf_allrequest.Visible = true;

                            li_erfo_poo.Visible = true;
                            li_erfo_er.Visible = true;
                            li_erfo_ad.Visible = true;
                            li_erfo_rc.Visible = true;
                        }
                        else
                        {
                            urf_mt.Visible = false;
                            li_urf_reason.Visible = false;
                            li_urf_receivingentry.Visible = false;

                            drf_mt.Visible = false;
                            li_drf_typesofabnormality.Visible = false;
                            li_drf_receivingentry.Visible = false;

                            crf_mt.Visible = false;
                            li_crf_mt_reason.Visible = false;
                            li_crf_receivingentry.Visible = false;

                            li_crf_allrequest.Visible = true;
                            li_drf_allrequest.Visible = true;
                            li_urf_allrequest.Visible = true;

                            li_erfo_poo.Visible = false;
                            li_erfo_er.Visible = false;
                            li_erfo_ad.Visible = false;
                            li_erfo_rc.Visible = false;
                        }
                                                

                        // ------------------------------------------------------------------------------------------------------------------------------


                        // PERMIT LICENSE MONITORING
                        li_plm_mt_permitcertificates.Visible = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "502");
                        li_plm_mt_supplier.Visible = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "501");
                        li_plm_permitentry.Visible = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "503");
                        li_plm_mt_governmentagencies.Visible = COMMON.isUserAllowed(Session["LcRefId"].ToString(), "504");
                        permits_trans.Visible = true;    

                        if (li_plm_mt_permitcertificates.Visible == true || li_plm_mt_supplier.Visible == true || li_plm_mt_governmentagencies.Visible == true)
                        {
                            permits_mt.Visible = true;
                        }
                        else
                        {
                            permits_mt.Visible = false;
                        }

                        lblUser.Text = Session["Username"].ToString().ToUpper() + " (" + Session["UserFullName"].ToString().ToUpper() + ") ";
                        lbLogOut.Visible = true;
                    }
                }
        }

        

        protected void lbLogOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }



    }
}
