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

public class BLL_DRF
{
    public BLL_DRF()
    {
    }

    #region TYPE OF ABNORMALITY

    public List<Entities_DRF_TypesOfAbnormality> DRF_MT_TypesOfAbnormality_GetAll()
    {
        List<Entities_DRF_TypesOfAbnormality> list = new List<Entities_DRF_TypesOfAbnormality>();

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            list = DAL.DRF_MT_TypesOfAbnormality_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int DRF_MT_TypesOfAbnormality_IsDisabled(Entities_DRF_TypesOfAbnormality entity)
    {
        int ret = 0;

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            ret = DAL.DRF_MT_TypesOfAbnormality_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int DRF_MT_TypesOfAbnormality_Append(Entities_DRF_TypesOfAbnormality entity)
    {
        int ret = 0;

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            ret = DAL.DRF_MT_TypesOfAbnormality_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int DRF_MT_TypesOfAbnormality_Insert(Entities_DRF_TypesOfAbnormality entity)
    {
        int ret = 0;

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            ret = DAL.DRF_MT_TypesOfAbnormality_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_DRF_TypesOfAbnormality> DRF_MT_TypesOfAbnormality_GetByName(string name)
    {
        List<Entities_DRF_TypesOfAbnormality> list = new List<Entities_DRF_TypesOfAbnormality>();

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            list = DAL.DRF_MT_TypesOfAbnormality_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_DRF_TypesOfAbnormality> DRF_MT_TypesOfAbnormality_GetByName_Like(string name)
    {
        List<Entities_DRF_TypesOfAbnormality> list = new List<Entities_DRF_TypesOfAbnormality>();

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            list = DAL.DRF_MT_TypesOfAbnormality_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion

    #region DRF TRANSACTION ENTRY

    public List<Entities_DRF_RequestEntry> DRF_TRANSACTION_RequestEntry_Fill_All_DropdownList()
    {
        List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            list = DAL.DRF_TRANSACTION_RequestEntry_Fill_All_DropdownList();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public Int32 DRF_TRANSACTION_CountRequestHead(string year)
    {
        int ret = 0;

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            ret = DAL.DRF_TRANSACTION_CountRequestHead(year);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public string DRF_TRANSACTION_Request_Insert(Entities_DRF_RequestEntry entity)
    {
        string result = string.Empty;

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            result = DAL.DRF_TRANSACTION_Request_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return result;
    }

    public int DRF_TRANSACTION_Request_Append(Entities_DRF_RequestEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            ret = DAL.DRF_TRANSACTION_Request_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int DRF_TRANSACTION_Insert_ProofAttachment(string filename, string addedby, string ctrlno)
    {
        int ret = 0;

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            ret = DAL.DRF_TRANSACTION_Insert_ProofAttachment(filename, addedby, ctrlno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_DRF_RequestEntry> DRF_TRANSACTION_RequestStatus_ByDateRange(Entities_DRF_RequestEntry entity)
    {
        List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            list = DAL.DRF_TRANSACTION_RequestStatus_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_DRF_RequestEntry> DRF_TRANSACTION_ProofAttachmentList(Entities_DRF_RequestEntry entity)
    {
        List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            list = DAL.DRF_TRANSACTION_ProofAttachmentList(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_DRF_RequestEntry> DRF_TRANSACTION_Reporting_ByDateRange(Entities_DRF_RequestEntry entity)
    {
        List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            list = DAL.DRF_TRANSACTION_Reporting_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_DRF_RequestEntry> DRF_TRANSACTION_Reporting_ByDateRange_ByDivision(Entities_DRF_RequestEntry entity)
    {
        List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            list = DAL.DRF_TRANSACTION_Reporting_ByDateRange_ByDivision(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_DRF_RequestEntry> DRF_TRANSACTION_Reporting_ByDateRange_ByAll(Entities_DRF_RequestEntry entity)
    {
        List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            list = DAL.DRF_TRANSACTION_Reporting_ByDateRange_ByAll(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_DRF_RequestEntry> DRF_TRANSACTION_Reporting_ByDateRange_Details(Entities_DRF_RequestEntry entity)
    {
        List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            list = DAL.DRF_TRANSACTION_Reporting_ByDateRange_Details(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_DRF_RequestEntry> DRF_TRANSACTION_GetRequestByCTRLNo(Entities_DRF_RequestEntry entity)
    {
        List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            list = DAL.DRF_TRANSACTION_GetRequestByCTRLNo(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_DRF_RequestEntry> DRF_TRANSACTION_AllRequest(Entities_DRF_RequestEntry entity)
    {
        List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            list = DAL.DRF_TRANSACTION_AllRequest(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_DRF_RequestEntry> DRF_TRANSACTION_AllRequest_New(Entities_DRF_RequestEntry entity)
    {
        List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            list = DAL.DRF_TRANSACTION_AllRequest_New(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_DRF_RequestEntry> DRF_TRANSACTION_AllRequest_Like(Entities_DRF_RequestEntry entity)
    {
        List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            list = DAL.DRF_TRANSACTION_AllRequest_Like(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_DRF_RequestEntry> DRF_TRANSACTION_DataLinkToPR_ByPONO(string pono)
    {
        List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            list = DAL.DRF_TRANSACTION_DataLinkToPR_ByPONO(pono);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_DRF_RequestEntry> DRF_TRANSACTION_DataLinkToPR_ByPRNO(string prno)
    {
        List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            list = DAL.DRF_TRANSACTION_DataLinkToPR_ByPRNO(prno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_DRF_RequestEntry> DRF_TRANSACTION_AllRequest_ByCTRLNo(Entities_DRF_RequestEntry entity)
    {
        List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            list = DAL.DRF_TRANSACTION_AllRequest_ByCTRLNo(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_DRF_RequestEntry> DRF_TRANSACTION_Approval_DateRange(Entities_DRF_RequestEntry entity)
    {
        List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            list = DAL.DRF_TRANSACTION_Approval_DateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int DRF_TRANSACTION_SQLTransaction(string query)
    {
        int ret = 0;

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            ret = DAL.DRF_TRANSACTION_SQLTransaction(query);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_DRF_RequestEntry> DRF_TRANSACTION_GetSupplierAttachmentByCTRLNo(Entities_DRF_RequestEntry entity)
    {
        List<Entities_DRF_RequestEntry> list = new List<Entities_DRF_RequestEntry>();

        try
        {
            DAL_DRF DAL = new DAL_DRF();
            list = DAL.DRF_TRANSACTION_GetSupplierAttachmentByCTRLNo(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    #endregion

}
