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

public class BLL_URF
{
    public BLL_URF()
    {
    }

    #region REASON

    public List<Entities_URF_Reason> URF_MT_Reason_GetAll()
    {
        List<Entities_URF_Reason> list = new List<Entities_URF_Reason>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_MT_Reason_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int URF_MT_Reason_IsDisabled(Entities_URF_Reason entity)
    {
        int ret = 0;

        try
        {
            DAL_URF DAL = new DAL_URF();
            ret = DAL.URF_MT_Reason_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int URF_MT_Reason_Append(Entities_URF_Reason entity)
    {
        int ret = 0;

        try
        {
            DAL_URF DAL = new DAL_URF();
            ret = DAL.URF_MT_Reason_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int URF_MT_Reason_Insert(Entities_URF_Reason entity)
    {
        int ret = 0;

        try
        {
            DAL_URF DAL = new DAL_URF();
            ret = DAL.URF_MT_Reason_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_URF_Reason> URF_MT_Reason_GetByName(string name)
    {
        List<Entities_URF_Reason> list = new List<Entities_URF_Reason>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_MT_Reason_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_URF_Reason> URF_MT_Reason_GetByName_Like(string name)
    {
        List<Entities_URF_Reason> list = new List<Entities_URF_Reason>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_MT_Reason_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion

    #region Request Entry

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_RequestEntry_Fill_All_DropdownList()
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_RequestEntry_Fill_All_DropdownList();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int URF_TRANSACTION_RequestEntry_Insert_SQLTransaction(string query)
    {
        int ret = 0;

        try
        {
            DAL_URF DAL = new DAL_URF();
            ret = DAL.URF_TRANSACTION_RequestEntry_Insert_SQLTransaction(query);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public Int32 URF_TRANSACTION_CountRequestHead(string year)
    {
        int ret = 0;

        try
        {
            DAL_URF DAL = new DAL_URF();
            ret = DAL.URF_TRANSACTION_CountRequestHead(year);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_Monitoring_ByDateRange(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_Monitoring_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_GetRequestDetailsByCTRLNo(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_GetRequestDetailsByCTRLNo(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }
    public List<Entities_URF_RequestEntry> URF_TRANSACTION_GetRequestDetailsByCTRLNo_ExportToExcel(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_GetRequestDetailsByCTRLNo_ExportToExcel(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_GetRequestDetailsByCTRLNo2(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_GetRequestDetailsByCTRLNo2(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_GetRequestDetailsByCTRLNo3(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_GetRequestDetailsByCTRLNo3(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    public List<Entities_URF_RequestEntry> URF_TRANSACTION_GetSupplierAttachmentByCTRLNo(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_GetSupplierAttachmentByCTRLNo(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_GetExistingRequestByPONumber(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_GetExistingRequestByPONumber(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_GetExistingRequestByPONumber_ApprovedRequest(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_GetExistingRequestByPONumber_ApprovedRequest(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_GetRequestHeadByCTRLNo(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_GetRequestHeadByCTRLNo(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_Approval_DateRange(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_Approval_DateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_GetHirarchyAccess(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_GetHirarchyAccess(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_Receiving_DateRange(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_Receiving_DateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int URF_TRANSACTION_SQLTransaction(string query)
    {
        int ret = 0;

        try
        {
            DAL_URF DAL = new DAL_URF();
            ret = DAL.URF_TRANSACTION_SQLTransaction(query);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_AllRequest(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_AllRequest(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_AllRequest_New(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_AllRequest_New(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_AllRequest_Reporting(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_AllRequest_Reporting(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_URF_RequestEntry> vewRFQDelivery(string query, string dataIndex)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.vewRFQDelivery(query, dataIndex);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    public List<Entities_URF_RequestEntry> URF_TRANSACTION_CheckApprover(string userid)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_CheckApprover(userid);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_Reporting_ByDateRange(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_Reporting_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_Reporting_ByDateRange_ByDivision(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_Reporting_ByDateRange_ByDivision(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_Reporting_ByDateRange_ByAll(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_Reporting_ByDateRange_ByAll(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_Reporting_ByDateRange_Details(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        try
        {
            DAL_URF DAL = new DAL_URF();
            list = DAL.URF_TRANSACTION_Reporting_ByDateRange_Details(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion

}

