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
using CrystalDecisions.CrystalReports.Engine;
using System.Collections.Generic; 

namespace REPI_PUR_SOFRA.Reporting.DRF
{
    public partial class RPT_DRF : System.Web.UI.Page
    {

        BLL_DRF BLL = new BLL_DRF();
        Common COMMON = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    if (!String.IsNullOrEmpty(Request.QueryString["CTRLNo"].ToString()))
                    {

                        string ctrlno = CryptorEngine.Decrypt(Request.QueryString["CTRLNo"].ToString().Replace(" ", "+"), true);
                        string supplier = string.Empty;
                        string attention = string.Empty;
                        string invoicedrno = string.Empty;
                        string pono = string.Empty;
                        string prno = string.Empty;
                        string description = string.Empty;
                        string typedrawingno = string.Empty;
                        string orderquantity = string.Empty;
                        string abnormalquantity = string.Empty;
                        string typesofabnormality = string.Empty;
                        string detailedreport = string.Empty;
                        string preparedby = string.Empty;
                        string notedby = string.Empty;
                        string incharge = string.Empty;
                        string manager = string.Empty;
                        string conforme = string.Empty;
                        string type = string.Empty;
                        string answercountermeasure = string.Empty;
                        string podate = string.Empty;
                        string receiveddate = string.Empty;

                        string preparedby_mark = string.Empty;
                        string notedby_mark = string.Empty;
                        string incharge_mark = string.Empty;
                        string conforme_mark = string.Empty;
                        string manager_mark = string.Empty;

                        List<Entities_DRF_RequestEntry> listRequest = new List<Entities_DRF_RequestEntry>();
                        Entities_DRF_RequestEntry entityRequest = new Entities_DRF_RequestEntry();
                        entityRequest.CtrlNo = ctrlno;

                        listRequest = BLL.DRF_TRANSACTION_AllRequest_ByCTRLNo(entityRequest);

                        if (listRequest != null)
                        {
                            if (listRequest.Count > 0)
                            {
                                foreach (Entities_DRF_RequestEntry eRequest in listRequest)
                                {
                                    supplier = eRequest.Supplier;
                                    attention = eRequest.Attention;
                                    invoicedrno = eRequest.InvoiceDRNO;
                                    prno = eRequest.PrNO;
                                    pono = eRequest.PoNO;
                                    description = eRequest.Description;
                                    typedrawingno = eRequest.TypeDrawingNo;
                                    orderquantity = eRequest.OrderQuantity;
                                    abnormalquantity = eRequest.AbnormalQuantity;
                                    typesofabnormality = eRequest.TypesOfAbnormality;
                                    detailedreport = eRequest.DetailedReport;
                                    preparedby = eRequest.RequesterS;
                                    notedby = eRequest.ReqManager;
                                    incharge = eRequest.PurIncharge;
                                    manager = eRequest.PurManager;
                                    podate = eRequest.PoDate;
                                    receiveddate = eRequest.ReceivedDate;
                                    notedby_mark = eRequest.ReqManagerStat;
                                    incharge_mark = eRequest.PurInchargeStat;
                                    manager_mark = eRequest.PurManagerStat;
                                    conforme_mark = eRequest.SupplierResponded;
                                    conforme = eRequest.Attention;
                                }
                            }
                        }

                        CrystalDecisions.Shared.ParameterFields PAF = new CrystalDecisions.Shared.ParameterFields();

                        // CTRLNo -----------------------------------------------------------
                        CrystalDecisions.Shared.ParameterField CTRLNo_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue CTRLNo_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        CTRLNo_PF.ParameterFieldName = "CTRLNo";
                        CTRLNo_PV.Value = ctrlno;
                        CTRLNo_PF.CurrentValues.Add(CTRLNo_PV);
                        PAF.Add(CTRLNo_PF);


                        // SUPPLIER ---------------------------------------------------------
                        CrystalDecisions.Shared.ParameterField SUPPLIER_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue SUPPLIER_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        SUPPLIER_PF.ParameterFieldName = "Supplier";
                        SUPPLIER_PV.Value = supplier;
                        SUPPLIER_PF.CurrentValues.Add(SUPPLIER_PV);
                        PAF.Add(SUPPLIER_PF);

                        // ATTENTION
                        CrystalDecisions.Shared.ParameterField ATTENTION_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue ATTENTION_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        ATTENTION_PF.ParameterFieldName = "Attention";
                        ATTENTION_PV.Value = attention;
                        ATTENTION_PF.CurrentValues.Add(ATTENTION_PV);
                        PAF.Add(ATTENTION_PF);

                        // INVOICE DRNO
                        CrystalDecisions.Shared.ParameterField INVOICEDRNO_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue INVOICEDRNO_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        INVOICEDRNO_PF.ParameterFieldName = "InvoiceDRNo";
                        INVOICEDRNO_PV.Value = invoicedrno;
                        INVOICEDRNO_PF.CurrentValues.Add(INVOICEDRNO_PV);
                        PAF.Add(INVOICEDRNO_PF);

                        // PRNO
                        CrystalDecisions.Shared.ParameterField PRPONO_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue PRPONO_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        PRPONO_PF.ParameterFieldName = "PRPONo";
                        PRPONO_PV.Value = prno;
                        PRPONO_PF.CurrentValues.Add(PRPONO_PV);
                        PAF.Add(PRPONO_PF);

                        // PONO
                        CrystalDecisions.Shared.ParameterField PONO_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue PONO_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        PONO_PF.ParameterFieldName = "PONo";
                        PONO_PV.Value = pono;
                        PONO_PF.CurrentValues.Add(PONO_PV);
                        PAF.Add(PONO_PF);

                        // DESCRIPTION
                        CrystalDecisions.Shared.ParameterField DESCRIPTION_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue DESCRIPTION_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        DESCRIPTION_PF.ParameterFieldName = "Description";
                        DESCRIPTION_PV.Value = description;
                        DESCRIPTION_PF.CurrentValues.Add(DESCRIPTION_PV);
                        PAF.Add(DESCRIPTION_PF);

                        // TYPE DRAWING NO
                        CrystalDecisions.Shared.ParameterField TYPEDRAWINGNO_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue TYPEDRAWINGNO_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        TYPEDRAWINGNO_PF.ParameterFieldName = "TypeDrawingNo";
                        TYPEDRAWINGNO_PV.Value = typedrawingno;
                        TYPEDRAWINGNO_PF.CurrentValues.Add(TYPEDRAWINGNO_PV);
                        PAF.Add(TYPEDRAWINGNO_PF);

                        // ORDER QUANTITY
                        CrystalDecisions.Shared.ParameterField ORDERQUANTITY_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue ORDERQUANTITY_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        ORDERQUANTITY_PF.ParameterFieldName = "OrderQuantity";
                        ORDERQUANTITY_PV.Value = orderquantity;
                        ORDERQUANTITY_PF.CurrentValues.Add(ORDERQUANTITY_PV);
                        PAF.Add(ORDERQUANTITY_PF);

                        // ABNORMAL QUANTITY
                        CrystalDecisions.Shared.ParameterField ABNORMALQUANTITY_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue ABNORMALQUANTITY_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        ABNORMALQUANTITY_PF.ParameterFieldName = "AbnormalQuantity";
                        ABNORMALQUANTITY_PV.Value = abnormalquantity;
                        ABNORMALQUANTITY_PF.CurrentValues.Add(ABNORMALQUANTITY_PV);
                        PAF.Add(ABNORMALQUANTITY_PF);

                        // TYPES OF ABNORMALITY
                        CrystalDecisions.Shared.ParameterField TYPESOFABNORMALITY_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue TYPESOFABNORMALITY_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        TYPESOFABNORMALITY_PF.ParameterFieldName = "TypesOfAbnormality";
                        TYPESOFABNORMALITY_PV.Value = typesofabnormality;
                        TYPESOFABNORMALITY_PF.CurrentValues.Add(TYPESOFABNORMALITY_PV);
                        PAF.Add(TYPESOFABNORMALITY_PF);

                        // DETAILED REPORT
                        CrystalDecisions.Shared.ParameterField DETAILEDREPORT_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue DETAILEDREPORT_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        DETAILEDREPORT_PF.ParameterFieldName = "DetailedReport";
                        DETAILEDREPORT_PV.Value = detailedreport;
                        DETAILEDREPORT_PF.CurrentValues.Add(DETAILEDREPORT_PV);
                        PAF.Add(DETAILEDREPORT_PF);

                        // PREPARED BY
                        CrystalDecisions.Shared.ParameterField PREPAREDBY_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue PREPAREDBY_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        PREPAREDBY_PF.ParameterFieldName = "PreparedBy";
                        PREPAREDBY_PV.Value = preparedby;
                        PREPAREDBY_PF.CurrentValues.Add(PREPAREDBY_PV);
                        PAF.Add(PREPAREDBY_PF);

                        // PREPARED BY MARK
                        CrystalDecisions.Shared.ParameterField PREPAREDBYMARK_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue PREPAREDBYMARK_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        PREPAREDBYMARK_PF.ParameterFieldName = "PreparedBy_Mark";
                        PREPAREDBYMARK_PV.Value = !string.IsNullOrEmpty(preparedby) ? "APPROVED" : string.Empty;
                        PREPAREDBYMARK_PF.CurrentValues.Add(PREPAREDBYMARK_PV);
                        PAF.Add(PREPAREDBYMARK_PF);

                        // NOTED BY
                        CrystalDecisions.Shared.ParameterField NOTEDBY_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue NOTEDBY_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        NOTEDBY_PF.ParameterFieldName = "NotedBy";
                        NOTEDBY_PV.Value = notedby;
                        NOTEDBY_PF.CurrentValues.Add(NOTEDBY_PV);
                        PAF.Add(NOTEDBY_PF);

                        // NOTED BY MARK
                        CrystalDecisions.Shared.ParameterField NOTEDBYMARK_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue NOTEDBYMARK_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        NOTEDBYMARK_PF.ParameterFieldName = "NotedBy_Mark";
                        NOTEDBYMARK_PV.Value = setStatus(notedby_mark);
                        NOTEDBYMARK_PF.CurrentValues.Add(NOTEDBYMARK_PV);
                        PAF.Add(NOTEDBYMARK_PF);

                        // INCHARGE BY
                        CrystalDecisions.Shared.ParameterField INCHARGEBY_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue INCHARGEBY_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        INCHARGEBY_PF.ParameterFieldName = "Incharge";
                        INCHARGEBY_PV.Value = incharge;
                        INCHARGEBY_PF.CurrentValues.Add(INCHARGEBY_PV);
                        PAF.Add(INCHARGEBY_PF);

                        // INCHARGE BY MARK
                        CrystalDecisions.Shared.ParameterField INCHARGEBYMARK_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue INCHARGEBYMARK_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        INCHARGEBYMARK_PF.ParameterFieldName = "Incharge_Mark";
                        INCHARGEBYMARK_PV.Value = setStatus(incharge_mark);
                        INCHARGEBYMARK_PF.CurrentValues.Add(INCHARGEBYMARK_PV);
                        PAF.Add(INCHARGEBYMARK_PF);

                        // MANAGER BY
                        CrystalDecisions.Shared.ParameterField MANAGERBY_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue MANAGERBY_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        MANAGERBY_PF.ParameterFieldName = "Manager";
                        MANAGERBY_PV.Value = manager;
                        MANAGERBY_PF.CurrentValues.Add(MANAGERBY_PV);
                        PAF.Add(MANAGERBY_PF);

                        // MANAGER BY MARK
                        CrystalDecisions.Shared.ParameterField MANAGERBYMARK_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue MANAGERBYMARK_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        MANAGERBYMARK_PF.ParameterFieldName = "Manager_Mark";
                        MANAGERBYMARK_PV.Value = setStatus(manager_mark);
                        MANAGERBYMARK_PF.CurrentValues.Add(MANAGERBYMARK_PV);
                        PAF.Add(MANAGERBYMARK_PF);

                        // CONFORME 
                        CrystalDecisions.Shared.ParameterField CONFORME_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue CONFORME_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        CONFORME_PF.ParameterFieldName = "SupplierConforme";
                        CONFORME_PV.Value = conforme;
                        CONFORME_PF.CurrentValues.Add(CONFORME_PV);
                        PAF.Add(CONFORME_PF);

                        // CONFORME BY MARK
                        CrystalDecisions.Shared.ParameterField CONFORMEBYMARK_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue CONFORMEBYMARK_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        CONFORMEBYMARK_PF.ParameterFieldName = "SupplierConforme_Mark";
                        CONFORMEBYMARK_PV.Value = setStatus(conforme_mark);
                        CONFORMEBYMARK_PF.CurrentValues.Add(CONFORMEBYMARK_PV);
                        PAF.Add(CONFORMEBYMARK_PF);

                        // TYPE 
                        CrystalDecisions.Shared.ParameterField TYPE_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue TYPE_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        TYPE_PF.ParameterFieldName = "Type";
                        TYPE_PV.Value = type;
                        TYPE_PF.CurrentValues.Add(TYPE_PV);
                        PAF.Add(TYPE_PF);

                        // ANSWER COUNTER MEASURE 
                        CrystalDecisions.Shared.ParameterField ANSWERCOUNTERMEASURE_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue ANSWERCOUNTERMEASURE_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        ANSWERCOUNTERMEASURE_PF.ParameterFieldName = "AnswerCountermeasure";
                        ANSWERCOUNTERMEASURE_PV.Value = answercountermeasure;
                        ANSWERCOUNTERMEASURE_PF.CurrentValues.Add(ANSWERCOUNTERMEASURE_PV);
                        PAF.Add(ANSWERCOUNTERMEASURE_PF);

                        // PO DATE
                        CrystalDecisions.Shared.ParameterField PODATE_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue PODATE_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        PODATE_PF.ParameterFieldName = "PODate";
                        PODATE_PV.Value = podate;
                        PODATE_PF.CurrentValues.Add(PODATE_PV);
                        PAF.Add(PODATE_PF);

                        // RECEIVED DATE
                        CrystalDecisions.Shared.ParameterField RECEIVEDDATE_PF = new CrystalDecisions.Shared.ParameterField();
                        CrystalDecisions.Shared.ParameterDiscreteValue RECEIVEDDATE_PV = new CrystalDecisions.Shared.ParameterDiscreteValue();

                        RECEIVEDDATE_PF.ParameterFieldName = "ReceivedDate";
                        RECEIVEDDATE_PV.Value = receiveddate;
                        RECEIVEDDATE_PF.CurrentValues.Add(RECEIVEDDATE_PV);
                        PAF.Add(RECEIVEDDATE_PF);

                        ReportDocument cryRpt = new ReportDocument();
                        cryRpt.Load(Server.MapPath(@"~/Reporting/DRF/DRF_Print.rpt"));
                        CrystalReportViewer1.ParameterFieldInfo = PAF;
                        CrystalReportViewer1.BestFitPage = true;
                        CrystalReportViewer1.Width = 1160;
                        CrystalReportViewer1.Height = 1000;
                        CrystalReportViewer1.RefreshReport();
                        CrystalReportViewer1.ReportSource = cryRpt;

                    }

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.Message + "');", true);
                }

            }

        }

        private string setStatus(string stat)
        {
            string s = string.Empty;

            if (stat == "0")
            {
                s = "PENDING";
            }
            if (stat == "1")
            {
                s = "APPROVED";
            }
            if (stat == "2")
            {
                s = "DISAPPROVED";
            }

            return s;
        }



    }
}
