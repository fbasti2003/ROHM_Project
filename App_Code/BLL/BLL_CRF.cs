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


public class BLL_CRF
{
    public BLL_CRF()
    {
    }

    #region REASON

    public List<Entities_CRF_Reason> CRF_MT_Reason_GetAll()
    {
        List<Entities_CRF_Reason> list = new List<Entities_CRF_Reason>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_MT_Reason_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int CRF_MT_Reason_IsDisabled(Entities_CRF_Reason entity)
    {
        int ret = 0;

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            ret = DAL.CRF_MT_Reason_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int CRF_MT_Reason_Append(Entities_CRF_Reason entity)
    {
        int ret = 0;

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            ret = DAL.CRF_MT_Reason_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int CRF_MT_Reason_Insert(Entities_CRF_Reason entity)
    {
        int ret = 0;

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            ret = DAL.CRF_MT_Reason_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_CRF_Reason> CRF_MT_Reason_GetByName(string name)
    {
        List<Entities_CRF_Reason> list = new List<Entities_CRF_Reason>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_MT_Reason_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_CRF_Reason> CRF_MT_Reason_GetByName_Like(string name)
    {
        List<Entities_CRF_Reason> list = new List<Entities_CRF_Reason>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_MT_Reason_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion

    #region CRF TRANSACTION ENTRY

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_RequestEntry_Fill_All_DropdownList()
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_TRANSACTION_RequestEntry_Fill_All_DropdownList();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public Int32 CRF_TRANSACTION_CountRequestHead(string year)
    {
        int ret = 0;

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            ret = DAL.CRF_TRANSACTION_CountRequestHead(year);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public string CRF_TRANSACTION_Request_Insert(Entities_CRF_RequestEntry entity)
    {
        string result = string.Empty;

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            result = DAL.CRF_TRANSACTION_Request_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return result;
    }

    public int CRF_TRANSACTION_Request_Append(Entities_CRF_RequestEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            ret = DAL.CRF_TRANSACTION_Request_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_RequestStatus_ByDateRange(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_TRANSACTION_RequestStatus_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_Reporting_ByDateRange(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_TRANSACTION_Reporting_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_Reporting_ByDateRange_ByDivision(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_TRANSACTION_Reporting_ByDateRange_ByDivision(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_Reporting_ByDateRange_ByAll(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_TRANSACTION_Reporting_ByDateRange_ByAll(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_Reporting_ByDateRange_Details(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_TRANSACTION_Reporting_ByDateRange_Details(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_RequestStatus_ByDateRange_All(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_TRANSACTION_RequestStatus_ByDateRange_All(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_GetRequestByCTRLNo(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_TRANSACTION_GetRequestByCTRLNo(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_GetRequestDetailsByCTRLNo(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_TRANSACTION_GetRequestDetailsByCTRLNo(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_AllRequest(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_TRANSACTION_AllRequest(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_AllRequest_Like(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_TRANSACTION_AllRequest_Like(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_DataLinkToPR_ByPONO(string pono)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_TRANSACTION_DataLinkToPR_ByPONO(pono);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_CRF_RequestEntry> vewRFQDelivery(string query, string dataIndex)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.vewRFQDelivery(query, dataIndex);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_DataLinkToPR_ByPRNO(string prno)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_TRANSACTION_DataLinkToPR_ByPRNO(prno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_AllRequest_ByCTRLNo(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_TRANSACTION_AllRequest_ByCTRLNo(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_AllRequest_ByCTRLNo2(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_TRANSACTION_AllRequest_ByCTRLNo2(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_Approval_DateRange(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_TRANSACTION_Approval_DateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_Approval_DateRange_New(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_TRANSACTION_Approval_DateRange_New(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int CRF_TRANSACTION_SQLTransaction(string query)
    {
        int ret = 0;

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            ret = DAL.CRF_TRANSACTION_SQLTransaction(query);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_GetSupplierAttachmentByCTRLNo(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        try
        {
            DAL_CRF DAL = new DAL_CRF();
            list = DAL.CRF_TRANSACTION_GetSupplierAttachmentByCTRLNo(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    #endregion


}
