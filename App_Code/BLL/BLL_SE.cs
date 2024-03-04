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
using System.Collections.Generic;

public class BLL_SE
{
    public BLL_SE()
    {
    }


    #region FiscalYear

    public List<Entities_SE_FiscalYear> SE_MT_FiscalYear_GetAll()
    {
        List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();

        try
        {
            DAL_SE DAL = new DAL_SE();
            list = DAL.SE_MT_FiscalYear_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int SE_MT_FiscalYear_IsDisabled(Entities_SE_FiscalYear entity)
    {
        int ret = 0;

        try
        {
            DAL_SE DAL = new DAL_SE();
            ret = DAL.SE_MT_FiscalYear_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SE_MT_FiscalYear_Insert(Entities_SE_FiscalYear entity)
    {
        int ret = 0;

        try
        {
            DAL_SE DAL = new DAL_SE();
            ret = DAL.SE_MT_FiscalYear_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SE_MT_FiscalYear_Append(Entities_SE_FiscalYear entity)
    {
        int ret = 0;

        try
        {
            DAL_SE DAL = new DAL_SE();
            ret = DAL.SE_MT_FiscalYear_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_SE_FiscalYear> SE_MT_FiscalYear_GetByDescription(string description)
    {
        List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();

        try
        {
            DAL_SE DAL = new DAL_SE();
            list = DAL.SE_MT_FiscalYear_GetByDescription(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SE_FiscalYear> SE_MT_FiscalYear_GetByDescription_Like(string description)
    {
        List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();

        try
        {
            DAL_SE DAL = new DAL_SE();
            list = DAL.SE_MT_FiscalYear_GetByDescription_Like(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    #endregion


    #region Evaluation Item

    public List<Entities_SE_EvaluationItem> SE_MT_EvaluationItem_GetAll()
    {
        List<Entities_SE_EvaluationItem> list = new List<Entities_SE_EvaluationItem>();

        try
        {
            DAL_SE DAL = new DAL_SE();
            list = DAL.SE_MT_EvaluationItem_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    #endregion


    #region Evaluation Criteria For Maker

    public List<Entities_SE_EvaluationCriteria_Maker> SE_MT_EvaluationCriteria_Maker_GetAll()
    {
        List<Entities_SE_EvaluationCriteria_Maker> list = new List<Entities_SE_EvaluationCriteria_Maker>();

        try
        {
            DAL_SE DAL = new DAL_SE();
            list = DAL.SE_MT_EvaluationCriteria_Maker_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int SE_MT_EvaluationCriteria_Maker_Insert(Entities_SE_EvaluationCriteria_Maker entity)
    {
        int ret = 0;

        try
        {
            DAL_SE DAL = new DAL_SE();
            ret = DAL.SE_MT_EvaluationCriteria_Maker_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SE_MT_EvaluationCriteria_Maker_Append(Entities_SE_EvaluationCriteria_Maker entity)
    {
        int ret = 0;

        try
        {
            DAL_SE DAL = new DAL_SE();
            ret = DAL.SE_MT_EvaluationCriteria_Maker_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SE_MT_EvaluationCriteria_Maker_IsDisabled(Entities_SE_EvaluationCriteria_Maker entity)
    {
        int ret = 0;

        try
        {
            DAL_SE DAL = new DAL_SE();
            ret = DAL.SE_MT_EvaluationCriteria_Maker_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    #endregion


    #region Evaluation Criteria For Trader

    public List<Entities_SE_EvaluationCriteria_Trader> SE_MT_EvaluationCriteria_Trader_GetAll()
    {
        List<Entities_SE_EvaluationCriteria_Trader> list = new List<Entities_SE_EvaluationCriteria_Trader>();

        try
        {
            DAL_SE DAL = new DAL_SE();
            list = DAL.SE_MT_EvaluationCriteria_Trader_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int SE_MT_EvaluationCriteria_Trader_Insert(Entities_SE_EvaluationCriteria_Trader entity)
    {
        int ret = 0;

        try
        {
            DAL_SE DAL = new DAL_SE();
            ret = DAL.SE_MT_EvaluationCriteria_Trader_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SE_MT_EvaluationCriteria_Trader_Append(Entities_SE_EvaluationCriteria_Trader entity)
    {
        int ret = 0;

        try
        {
            DAL_SE DAL = new DAL_SE();
            ret = DAL.SE_MT_EvaluationCriteria_Trader_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SE_MT_EvaluationCriteria_Trader_IsDisabled(Entities_SE_EvaluationCriteria_Trader entity)
    {
        int ret = 0;

        try
        {
            DAL_SE DAL = new DAL_SE();
            ret = DAL.SE_MT_EvaluationCriteria_Trader_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    #endregion


    #region Evaluation Criteria For Material

    public List<Entities_SE_EvaluationCriteria_ForMaterial> SE_MT_EvaluationCriteria_ForMaterial_GetAll()
    {
        List<Entities_SE_EvaluationCriteria_ForMaterial> list = new List<Entities_SE_EvaluationCriteria_ForMaterial>();

        try
        {
            DAL_SE DAL = new DAL_SE();
            list = DAL.SE_MT_EvaluationCriteria_ForMaterial_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int SE_MT_EvaluationCriteria_ForMaterial_Insert(Entities_SE_EvaluationCriteria_ForMaterial entity)
    {
        int ret = 0;

        try
        {
            DAL_SE DAL = new DAL_SE();
            ret = DAL.SE_MT_EvaluationCriteria_ForMaterial_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SE_MT_EvaluationCriteria_ForMaterial_Append(Entities_SE_EvaluationCriteria_ForMaterial entity)
    {
        int ret = 0;

        try
        {
            DAL_SE DAL = new DAL_SE();
            ret = DAL.SE_MT_EvaluationCriteria_ForMaterial_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SE_MT_EvaluationCriteria_ForMaterial_IsDisabled(Entities_SE_EvaluationCriteria_ForMaterial entity)
    {
        int ret = 0;

        try
        {
            DAL_SE DAL = new DAL_SE();
            ret = DAL.SE_MT_EvaluationCriteria_ForMaterial_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    #endregion


    #region Supplier Evaluation Request Entry

    public int SE_TRANSACTION_RequestHead_Insert(Entities_SE_RequestEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_SE DAL = new DAL_SE();
            ret = DAL.SE_TRANSACTION_RequestHead_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public string SE_TRANSACTION_GetAlreadySend_By_Fy_SupplierId(string fy_supplierid)
    {
        string retVal = string.Empty;

        try
        {
            DAL_SE DAL = new DAL_SE();
            retVal = DAL.SE_TRANSACTION_GetAlreadySend_By_Fy_SupplierId(fy_supplierid);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return retVal;
    }

    public List<Entities_SE_RequestEntry> SE_TRANSACTION_IsRequestExist_By_FiscalYear(string fiscalYear)
    {
        List<Entities_SE_RequestEntry> list = new List<Entities_SE_RequestEntry>();

        try
        {
            DAL_SE DAL = new DAL_SE();
            list = DAL.SE_TRANSACTION_IsRequestExist_By_FiscalYear(fiscalYear);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int SE_TRANSACTION_RequestEntry_Insert_SQLTransaction(string query)
    {
        int ret = 0;

        try
        {
            DAL_SE DAL = new DAL_SE();
            ret = DAL.SE_TRANSACTION_RequestEntry_Insert_SQLTransaction(query);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_SE_FiscalYear> SE_TRANSACTION_Monitoring_GetAll()
    {
        List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();

        try
        {
            DAL_SE DAL = new DAL_SE();
            list = DAL.SE_TRANSACTION_Monitoring_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SE_FiscalYear> SE_TRANSACTION_Monitoring_GetAll2()
    {
        List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();

        try
        {
            DAL_SE DAL = new DAL_SE();
            list = DAL.SE_TRANSACTION_Monitoring_GetAll2();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SE_FiscalYear> SE_TRANSACTION_Monitoring_GetAll_ByFiscalYear(string fy)
    {
        List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();

        try
        {
            DAL_SE DAL = new DAL_SE();
            list = DAL.SE_TRANSACTION_Monitoring_GetAll_ByFiscalYear(fy);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SE_RequestEntry> SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear(string fy)
    {
        List<Entities_SE_RequestEntry> list = new List<Entities_SE_RequestEntry>();

        try
        {
            DAL_SE DAL = new DAL_SE();
            list = DAL.SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear(fy);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SE_RequestEntry> SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear3(string fy)
    {
        List<Entities_SE_RequestEntry> list = new List<Entities_SE_RequestEntry>();

        try
        {
            DAL_SE DAL = new DAL_SE();
            list = DAL.SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear3(fy);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SE_RequestEntry> SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear2(string fy)
    {
        List<Entities_SE_RequestEntry> list = new List<Entities_SE_RequestEntry>();

        try
        {
            DAL_SE DAL = new DAL_SE();
            list = DAL.SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear2(fy);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    public List<Entities_SE_RequestEntry> SE_TRANSACTION_GetLevel_From_CriteriaForMaterial()
    {
        List<Entities_SE_RequestEntry> list = new List<Entities_SE_RequestEntry>();

        try
        {
            DAL_SE DAL = new DAL_SE();
            list = DAL.SE_TRANSACTION_GetLevel_From_CriteriaForMaterial();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SE_RequestEntry> SE_TRANSACTION_IsScoreExist_In_TransactionScore(string detailsrefid)
    {
        List<Entities_SE_RequestEntry> list = new List<Entities_SE_RequestEntry>();

        try
        {
            DAL_SE DAL = new DAL_SE();
            list = DAL.SE_TRANSACTION_IsScoreExist_In_TransactionScore(detailsrefid);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SE_RequestEntry> SE_TRANSACTION_ReportAuditTrail_GetAll()
    {
        List<Entities_SE_RequestEntry> list = new List<Entities_SE_RequestEntry>();

        try
        {
            DAL_SE DAL = new DAL_SE();
            list = DAL.SE_TRANSACTION_ReportAuditTrail_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion


}
