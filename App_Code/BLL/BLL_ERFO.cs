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
using System.Data.Common;
using System.Collections.Generic;

public class BLL_ERFO
{
    public BLL_ERFO()
    {
    }


    #region PURPOSE OF OPERATION

    public List<Entities_ERFO_PurposeOfOperation> ERFO_MT_PurposeOfOperation_GetAll()
    {
        List<Entities_ERFO_PurposeOfOperation> list = new List<Entities_ERFO_PurposeOfOperation>();

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            list = DAL.ERFO_MT_PurposeOfOperation_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int ERFO_MT_PurposeOfOperation_IsDisabled(Entities_ERFO_PurposeOfOperation entity)
    {
        int ret = 0;

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            ret = DAL.ERFO_MT_PurposeOfOperation_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int ERFO_MT_PurposeOfOperation_Append(Entities_ERFO_PurposeOfOperation entity)
    {
        int ret = 0;

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            ret = DAL.ERFO_MT_PurposeOfOperation_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int ERFO_MT_PurposeOfOperation_Insert(Entities_ERFO_PurposeOfOperation entity)
    {
        int ret = 0;

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            ret = DAL.ERFO_MT_PurposeOfOperation_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_ERFO_PurposeOfOperation> ERFO_MT_PurposeOfOperation_GetByName(string name)
    {
        List<Entities_ERFO_PurposeOfOperation> list = new List<Entities_ERFO_PurposeOfOperation>();

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            list = DAL.ERFO_MT_PurposeOfOperation_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_ERFO_PurposeOfOperation> ERFO_MT_PurposeOfOperation_GetByName_Like(string name)
    {
        List<Entities_ERFO_PurposeOfOperation> list = new List<Entities_ERFO_PurposeOfOperation>();

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            list = DAL.ERFO_MT_PurposeOfOperation_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion

    #region EQUIPMENTS REQUIREMENT

    public List<Entities_ERFO_EquipmentRequirement> ERFO_MT_EquipmentRequirement_GetAll()
    {
        List<Entities_ERFO_EquipmentRequirement> list = new List<Entities_ERFO_EquipmentRequirement>();

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            list = DAL.ERFO_MT_EquipmentRequirement_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int ERFO_MT_EquipmentRequirement_IsDisabled(Entities_ERFO_EquipmentRequirement entity)
    {
        int ret = 0;

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            ret = DAL.ERFO_MT_EquipmentRequirement_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int ERFO_MT_EquipmentRequirement_Append(Entities_ERFO_EquipmentRequirement entity)
    {
        int ret = 0;

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            ret = DAL.ERFO_MT_EquipmentRequirement_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int ERFO_MT_EquipmentRequirement_Insert(Entities_ERFO_EquipmentRequirement entity)
    {
        int ret = 0;

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            ret = DAL.ERFO_MT_EquipmentRequirement_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_ERFO_EquipmentRequirement> ERFO_MT_EquipmentRequirement_GetByName(string name)
    {
        List<Entities_ERFO_EquipmentRequirement> list = new List<Entities_ERFO_EquipmentRequirement>();

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            list = DAL.ERFO_MT_EquipmentRequirement_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_ERFO_EquipmentRequirement> ERFO_MT_EquipmentRequirement_GetByName_Like(string name)
    {
        List<Entities_ERFO_EquipmentRequirement> list = new List<Entities_ERFO_EquipmentRequirement>();

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            list = DAL.ERFO_MT_EquipmentRequirement_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion

    #region ATTACHED DOCUMENT

    public List<Entities_ERFO_AttachedDocument> ERFO_MT_AttachedDocument_GetAll()
    {
        List<Entities_ERFO_AttachedDocument> list = new List<Entities_ERFO_AttachedDocument>();

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            list = DAL.ERFO_MT_AttachedDocument_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int ERFO_MT_AttachedDocument_IsDisabled(Entities_ERFO_AttachedDocument entity)
    {
        int ret = 0;

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            ret = DAL.ERFO_MT_AttachedDocument_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int ERFO_MT_AttachedDocument_Append(Entities_ERFO_AttachedDocument entity)
    {
        int ret = 0;

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            ret = DAL.ERFO_MT_AttachedDocument_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int ERFO_MT_AttachedDocument_Insert(Entities_ERFO_AttachedDocument entity)
    {
        int ret = 0;

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            ret = DAL.ERFO_MT_AttachedDocument_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_ERFO_AttachedDocument> ERFO_MT_AttachedDocument_GetByName(string name)
    {
        List<Entities_ERFO_AttachedDocument> list = new List<Entities_ERFO_AttachedDocument>();

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            list = DAL.ERFO_MT_AttachedDocument_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_ERFO_AttachedDocument> ERFO_MT_AttachedDocument_GetByName_Like(string name)
    {
        List<Entities_ERFO_AttachedDocument> list = new List<Entities_ERFO_AttachedDocument>();

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            list = DAL.ERFO_MT_AttachedDocument_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion

    #region RECOMMENDED CONTRACTOR

    public List<Entities_ERFO_RecommendedContractor> ERFO_MT_RecommendedContractor_GetAll()
    {
        List<Entities_ERFO_RecommendedContractor> list = new List<Entities_ERFO_RecommendedContractor>();

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            list = DAL.ERFO_MT_RecommendedContractor_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int ERFO_MT_RecommendedContractor_IsDisabled(Entities_ERFO_RecommendedContractor entity)
    {
        int ret = 0;

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            ret = DAL.ERFO_MT_RecommendedContractor_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int ERFO_MT_RecommendedContractor_Append(Entities_ERFO_RecommendedContractor entity)
    {
        int ret = 0;

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            ret = DAL.ERFO_MT_RecommendedContractor_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int ERFO_MT_RecommendedContractor_Insert(Entities_ERFO_RecommendedContractor entity)
    {
        int ret = 0;

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            ret = DAL.ERFO_MT_RecommendedContractor_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_ERFO_RecommendedContractor> ERFO_MT_RecommendedContractor_GetByName(string name)
    {
        List<Entities_ERFO_RecommendedContractor> list = new List<Entities_ERFO_RecommendedContractor>();

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            list = DAL.ERFO_MT_RecommendedContractor_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_ERFO_RecommendedContractor> ERFO_MT_RecommendedContractor_GetByName_Like(string name)
    {
        List<Entities_ERFO_RecommendedContractor> list = new List<Entities_ERFO_RecommendedContractor>();

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            list = DAL.ERFO_MT_RecommendedContractor_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion

    #region ERFO TRANSACTION ENTRY

    public List<Entities_ERFO_RequestEntry> ERFO_TRANSACTION_RequestEntry_Fill_All_DropdownList()
    {
        List<Entities_ERFO_RequestEntry> list = new List<Entities_ERFO_RequestEntry>();

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            list = DAL.ERFO_TRANSACTION_RequestEntry_Fill_All_DropdownList();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public Int32 ERFO_TRANSACTION_CountRequestHead(string year)
    {
        int ret = 0;

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            ret = DAL.ERFO_TRANSACTION_CountRequestHead(year);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public string ERFO_TRANSACTION_Request_Insert(Entities_ERFO_RequestEntry entity)
    {
        string result = string.Empty;

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            result = DAL.ERFO_TRANSACTION_Request_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return result;
    }

    public int ERFO_TRANSACTION_Request_Append(Entities_ERFO_RequestEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            ret = DAL.ERFO_TRANSACTION_Request_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_ERFO_RequestEntry> ERFO_TRANSACTION_RequestStatus_ByDateRange(Entities_ERFO_RequestEntry entity)
    {
        List<Entities_ERFO_RequestEntry> list = new List<Entities_ERFO_RequestEntry>();

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            list = DAL.ERFO_TRANSACTION_RequestStatus_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_ERFO_RequestEntry> ERFO_TRANSACTION_AllRequest_ByDateRange(Entities_ERFO_RequestEntry entity)
    {
        List<Entities_ERFO_RequestEntry> list = new List<Entities_ERFO_RequestEntry>();

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            list = DAL.ERFO_TRANSACTION_AllRequest_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_ERFO_RequestEntry> ERFO_TRANSACTION_Approval_DateRange(Entities_ERFO_RequestEntry entity)
    {
        List<Entities_ERFO_RequestEntry> list = new List<Entities_ERFO_RequestEntry>();

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            list = DAL.ERFO_TRANSACTION_Approval_DateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_ERFO_RequestEntry> ERFO_TRANSACTION_GetRequestByCTRLNo(Entities_ERFO_RequestEntry entity)
    {
        List<Entities_ERFO_RequestEntry> list = new List<Entities_ERFO_RequestEntry>();

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            list = DAL.ERFO_TRANSACTION_GetRequestByCTRLNo(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_ERFO_RequestEntry> ERFO_TRANSACTION_ContractorResponse(string ctrlno)
    {
        List<Entities_ERFO_RequestEntry> list = new List<Entities_ERFO_RequestEntry>();

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            list = DAL.ERFO_TRANSACTION_ContractorResponse(ctrlno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int ERFO_TRANSACTION_SQLTransaction(string query)
    {
        int ret = 0;

        try
        {
            DAL_ERFO DAL = new DAL_ERFO();
            ret = DAL.ERFO_TRANSACTION_SQLTransaction(query);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    #endregion

}
