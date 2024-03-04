using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public class Entities_SE_RequestEntry
{
    public Entities_SE_RequestEntry()
    {
    }

    private string refId;
    private string fiscalYear;
    private string fY_SupplierId;
    private string addedBy;
    private string updatedBy;


    private string detailsRefId;
    private string supplierRef;
    private string supplierName;
    private string automotiveRelated;
    private string classification;
    private string itemClassification;
    private string subContractor;
    private string noteworthyPoints;
    private string makerName;
    private string iSO9001;
    private string iSO14001;
    private string iATF16949;
    private string eI_FinancialAnalysis;
    private string eI_Quality;
    private string eI_CostResponse;
    private string eI_Delivery;
    private string eI_Cooperation;
    private string eI_CSR;
    private string eI_FinancialAnalysis_Value;
    private string eI_Quality_Value;
    private string eI_CostResponse_Value;
    private string eI_Delivery_Value;
    private string eI_Cooperation_Value;
    private string eI_CSR_Value;
    private string eI_TotalScore;
    private string eI_Ranking;
    private string eI_EvaluationPoints;
    private string judgementByPerson;
    private string judgementYearMonth;
    private string divManExpand;
    private string divManContinue;
    private string divManReduce;
    private string divManStop;
    private string reason;
    private string circularComments;
    private string divManInstructions;
    private string divManEvaluationResult;

    private string level;
    private string score;
    private string weight;
    private string totalScore;
    private string forApproval;

    private string incharge;
    private string sectionIncharge;
    private string deptManager;
    private string divManager;

    private string customerChartWidth;
    private string customerChartWidthExtended;


    private string report_Department_Name;
    private string report_Department_Total_Request;
    private string report_Department_Buyer_Approved;
    private string report_Department_Buyer_Disapproved;
    private string report_Department_PurManager_Approved;
    private string report_Department_PurManager_Disapproved;
    private string report_Department_Posted_Counts;
    private string report_Department_Pending_Approval;

    private string report_Division_Name;
    private string report_Division_Total_Request;
    private string report_Division_Buyer_Approved;
    private string report_Division_Buyer_Disapproved;
    private string report_Division_PurManager_Approved;
    private string report_Division_PurManager_Disapproved;
    private string report_Division_Posted_Counts;
    private string report_Division_Pending_Approval;

    private string report_All_Total_Request;
    private string report_All_Buyer_Approved;
    private string report_All_Buyer_Disapproved;
    private string report_All_PurManager_Approved;
    private string report_All_PurManager_Disapproved;
    private string report_All_Posted_Counts;
    private string report_All_Pending_Approval;
    private string report_All_Total_Approved;
    private string report_All_Total_Disapproved;

    private string scoreClass;


    private string reportAudit_RefId;
    private string reportAudit_FiscalYear;
    private string reportAudit_FormName;
    private string reportAudit_DownloadedBy;
    private string reportAudit_DownloadedDate;







    public string ReportAudit_RefId
    {
        get { return reportAudit_RefId; }
        set { reportAudit_RefId = value; }
    }
    public string ReportAudit_FiscalYear
    {
        get { return reportAudit_FiscalYear; }
        set { reportAudit_FiscalYear = value; }
    }
    public string ReportAudit_FormName
    {
        get { return reportAudit_FormName; }
        set { reportAudit_FormName = value; }
    }
    public string ReportAudit_DownloadedBy
    {
        get { return reportAudit_DownloadedBy; }
        set { reportAudit_DownloadedBy = value; }
    }
    public string ReportAudit_DownloadedDate
    {
        get { return reportAudit_DownloadedDate; }
        set { reportAudit_DownloadedDate = value; }
    }





    public string ScoreClass
    {
        get { return scoreClass; }
        set { scoreClass = value; }
    }

    public string Report_All_Total_Request
    {
        get { return report_All_Total_Request; }
        set { report_All_Total_Request = value; }
    }
    public string Report_All_Buyer_Approved
    {
        get { return report_All_Buyer_Approved; }
        set { report_All_Buyer_Approved = value; }
    }
    public string Report_All_Buyer_Disapproved
    {
        get { return report_All_Buyer_Disapproved; }
        set { report_All_Buyer_Disapproved = value; }
    }
    public string Report_All_PurManager_Approved
    {
        get { return report_All_PurManager_Approved; }
        set { report_All_PurManager_Approved = value; }
    }
    public string Report_All_PurManager_Disapproved
    {
        get { return report_All_PurManager_Disapproved; }
        set { report_All_PurManager_Disapproved = value; }
    }
    public string Report_All_Posted_Counts
    {
        get { return report_All_Posted_Counts; }
        set { report_All_Posted_Counts = value; }
    }
    public string Report_All_Pending_Approval
    {
        get { return report_All_Pending_Approval; }
        set { report_All_Pending_Approval = value; }
    }
    public string Report_All_Total_Approved
    {
        get { return report_All_Total_Approved; }
        set { report_All_Total_Approved = value; }
    }
    public string Report_All_Total_Disapproved
    {
        get { return report_All_Total_Disapproved; }
        set { report_All_Total_Disapproved = value; }
    }




    public string Report_Department_Name
    {
        get { return report_Department_Name; }
        set { report_Department_Name = value; }
    }
    public string Report_Department_Total_Request
    {
        get { return report_Department_Total_Request; }
        set { report_Department_Total_Request = value; }
    }
    public string Report_Department_Buyer_Approved
    {
        get { return report_Department_Buyer_Approved; }
        set { report_Department_Buyer_Approved = value; }
    }
    public string Report_Department_Buyer_Disapproved
    {
        get { return report_Department_Buyer_Disapproved; }
        set { report_Department_Buyer_Disapproved = value; }
    }
    public string Report_Department_PurManager_Approved
    {
        get { return report_Department_PurManager_Approved; }
        set { report_Department_PurManager_Approved = value; }
    }
    public string Report_Department_PurManager_Disapproved
    {
        get { return report_Department_PurManager_Disapproved; }
        set { report_Department_PurManager_Disapproved = value; }
    }
    public string Report_Department_Posted_Counts
    {
        get { return report_Department_Posted_Counts; }
        set { report_Department_Posted_Counts = value; }
    }
    public string Report_Department_Pending_Approval
    {
        get { return report_Department_Pending_Approval; }
        set { report_Department_Pending_Approval = value; }
    }

    public string Report_Division_Name
    {
        get { return report_Division_Name; }
        set { report_Division_Name = value; }
    }
    public string Report_Division_Total_Request
    {
        get { return report_Division_Total_Request; }
        set { report_Division_Total_Request = value; }
    }
    public string Report_Division_Buyer_Approved
    {
        get { return report_Division_Buyer_Approved; }
        set { report_Division_Buyer_Approved = value; }
    }
    public string Report_Division_Buyer_Disapproved
    {
        get { return report_Division_Buyer_Disapproved; }
        set { report_Division_Buyer_Disapproved = value; }
    }
    public string Report_Division_PurManager_Approved
    {
        get { return report_Division_PurManager_Approved; }
        set { report_Division_PurManager_Approved = value; }
    }
    public string Report_Division_PurManager_Disapproved
    {
        get { return report_Division_PurManager_Disapproved; }
        set { report_Division_PurManager_Disapproved = value; }
    }
    public string Report_Division_Posted_Counts
    {
        get { return report_Division_Posted_Counts; }
        set { report_Division_Posted_Counts = value; }
    }
    public string Report_Division_Pending_Approval
    {
        get { return report_Division_Pending_Approval; }
        set { report_Division_Pending_Approval = value; }
    }






    public string CustomerChartWidthExtended
    {
        get { return customerChartWidthExtended; }
        set { customerChartWidthExtended = value; }
    }
    public string CustomerChartWidth
    {
        get { return customerChartWidth; }
        set { customerChartWidth = value; }
    }
    public string Incharge
    {
        get { return incharge; }
        set { incharge = value; }
    }
    public string SectionIncharge
    {
        get { return sectionIncharge; }
        set { sectionIncharge = value; }
    }
    public string DeptManager
    {
        get { return deptManager; }
        set { deptManager = value; }
    }
    public string DivManager
    {
        get { return divManager; }
        set { divManager = value; }
    }

    public string ForApproval
    {
        get { return forApproval; }
        set { forApproval = value; }
    }
    public string TotalScore
    {
        get { return totalScore; }
        set { totalScore = value; }
    }
    public string DivManEvaluationResult
    {
        get { return divManEvaluationResult; }
        set { divManEvaluationResult = value; }
    }
    public string Level
    {
        get { return level; }
        set { level = value; }
    }
    public string Score
    {
        get { return score; }
        set { score = value; }
    }
    public string Weight
    {
        get { return weight; }
        set { weight = value; }
    }

    public string RefId
    {
        get { return refId; }
        set { refId = value; }
    }
    public string FiscalYear
    {
        get { return fiscalYear; }
        set { fiscalYear = value; }
    }
    public string FY_SupplierId
    {
        get { return fY_SupplierId; }
        set { fY_SupplierId = value; }
    }
    public string AddedBy
    {
        get { return addedBy; }
        set { addedBy = value; }
    }
    public string UpdatedBy
    {
        get { return updatedBy; }
        set { updatedBy = value; }
    }


    public string DetailsRefId
    {
        get { return detailsRefId; }
        set { detailsRefId = value; }
    }
    public string SupplierRef
    {
        get { return supplierRef; }
        set { supplierRef = value; }
    }
    public string SupplierName
    {
        get { return supplierName; }
        set { supplierName = value; }
    }
    public string AutomotiveRelated
    {
        get { return automotiveRelated; }
        set { automotiveRelated = value; }
    }
    public string Classification
    {
        get { return classification; }
        set { classification = value; }
    }
    public string ItemClassification
    {
        get { return itemClassification; }
        set { itemClassification = value; }
    }
    public string SubContractor
    {
        get { return subContractor; }
        set { subContractor = value; }
    }
    public string NoteworthyPoints
    {
        get { return noteworthyPoints; }
        set { noteworthyPoints = value; }
    }
    public string MakerName
    {
        get { return makerName; }
        set { makerName = value; }
    }
    public string ISO9001
    {
        get { return iSO9001; }
        set { iSO9001 = value; }
    }
    public string ISO14001
    {
        get { return iSO14001; }
        set { iSO14001 = value; }
    }
    public string IATF16949
    {
        get { return iATF16949; }
        set { iATF16949 = value; }
    }
    public string EI_FinancialAnalysis
    {
        get { return eI_FinancialAnalysis; }
        set { eI_FinancialAnalysis = value; }
    }
    public string EI_Quality
    {
        get { return eI_Quality; }
        set { eI_Quality = value; }
    }
    public string EI_CostResponse
    {
        get { return eI_CostResponse; }
        set { eI_CostResponse = value; }
    }
    public string EI_Delivery
    {
        get { return eI_Delivery; }
        set { eI_Delivery = value; }
    }
    public string EI_Cooperation
    {
        get { return eI_Cooperation; }
        set { eI_Cooperation = value; }
    }
    public string EI_CSR
    {
        get { return eI_CSR; }
        set { eI_CSR = value; }
    }

    public string EI_FinancialAnalysis_Value
    {
        get { return eI_FinancialAnalysis_Value; }
        set { eI_FinancialAnalysis_Value = value; }
    }
    public string EI_Quality_Value
    {
        get { return eI_Quality_Value; }
        set { eI_Quality_Value = value; }
    }
    public string EI_CostResponse_Value
    {
        get { return eI_CostResponse_Value; }
        set { eI_CostResponse_Value = value; }
    }
    public string EI_Delivery_Value
    {
        get { return eI_Delivery_Value; }
        set { eI_Delivery_Value = value; }
    }
    public string EI_Cooperation_Value
    {
        get { return eI_Cooperation_Value; }
        set { eI_Cooperation_Value = value; }
    }
    public string EI_CSR_Value
    {
        get { return eI_CSR_Value; }
        set { eI_CSR_Value = value; }
    }

    public string EI_TotalScore
    {
        get { return eI_TotalScore; }
        set { eI_TotalScore = value; }
    }
    public string EI_Ranking
    {
        get { return eI_Ranking; }
        set { eI_Ranking = value; }
    }
    public string EI_EvaluationPoints
    {
        get { return eI_EvaluationPoints; }
        set { eI_EvaluationPoints = value; }
    }
    public string JudgementByPerson
    {
        get { return judgementByPerson; }
        set { judgementByPerson = value; }
    }
    public string JudgementYearMonth
    {
        get { return judgementYearMonth; }
        set { judgementYearMonth = value; }
    }
    public string DivManExpand
    {
        get { return divManExpand; }
        set { divManExpand = value; }
    }
    public string DivManContinue
    {
        get { return divManContinue; }
        set { divManContinue = value; }
    }
    public string DivManReduce
    {
        get { return divManReduce; }
        set { divManReduce = value; }
    }
    public string DivManStop
    {
        get { return divManStop; }
        set { divManStop = value; }
    }
    public string Reason
    {
        get { return reason; }
        set { reason = value; }
    }
    public string CircularComments
    {
        get { return circularComments; }
        set { circularComments = value; }
    }
    public string DivManInstructions
    {
        get { return divManInstructions; }
        set { divManInstructions = value; }
    }









}
