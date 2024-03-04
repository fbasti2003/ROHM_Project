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

public class BLL_PLM
{
    public BLL_PLM()
    {
    }

    #region PERMIT CERTIFICATES

    public List<Entities_PLM_PermitCertificates> PLM_MT_PermitCertificates_GetAll()
    {
        List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            list = DAL.PLM_MT_PermitCertificates_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PLM_PermitCertificates> PLM_MT_PermitCertificates_GetByRefId(string refId)
    {
        List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            list = DAL.PLM_MT_PermitCertificates_GetByRefId(refId);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PLM_PermitCertificates> PLM_TRANSACTION_NotificationReceiver_GetByRefId(string refId)
    {
        List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            list = DAL.PLM_TRANSACTION_NotificationReceiver_GetByRefId(refId);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PLM_PermitCertificates> PLM_TRANSACTION_NotificationReceiver_GetByRefIdAndSupplierIdAndIssuedDateAndExpiration(Entities_PLM_PermitCertificates entity)
    {
        List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            list = DAL.PLM_TRANSACTION_NotificationReceiver_GetByRefIdAndSupplierIdAndIssuedDateAndExpiration(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int PLM_MT_PermitCertificates_Insert(Entities_PLM_PermitCertificates entity)
    {
        int ret = 0;

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            ret = DAL.PLM_MT_PermitCertificates_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PLM_MT_PermitCertificates_Append(Entities_PLM_PermitCertificates entity)
    {
        int ret = 0;

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            ret = DAL.PLM_MT_PermitCertificates_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PLM_MT_PermitCertificates_DisableByRefId(Entities_PLM_PermitCertificates entity)
    {
        int ret = 0;

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            ret = DAL.PLM_MT_PermitCertificates_DisableByRefId(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    #endregion

    #region Supplier

    public List<Entities_PLM_PermitCertificates> PLM_MT_Supplier_GetAll()
    {
        List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            list = DAL.PLM_MT_Supplier_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int PLM_MT_Supplier_IsDisabled(Entities_PLM_PermitCertificates entity)
    {
        int ret = 0;

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            ret = DAL.PLM_MT_Supplier_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PLM_MT_Supplier_Append(Entities_PLM_PermitCertificates entity)
    {
        int ret = 0;

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            ret = DAL.PLM_MT_Supplier_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PLM_MT_Supplier_Insert(Entities_PLM_PermitCertificates entity)
    {
        int ret = 0;

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            ret = DAL.PLM_MT_Supplier_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_PLM_PermitCertificates> PLM_MT_Supplier_GetBySupplierName(string description)
    {
        List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            list = DAL.PLM_MT_Supplier_GetBySupplierName(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PLM_PermitCertificates> PLM_MT_Supplier_GetBySupplierName_Like(string description)
    {
        List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            list = DAL.PLM_MT_Supplier_GetBySupplierName_Like(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    #endregion


    #region Transactions

    public List<Entities_PLM_PermitCertificates> PLM_TRANSACTION_Request_GetAll()
    {
        List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            list = DAL.PLM_TRANSACTION_Request_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PLM_PermitCertificates> PLM_TRANSACTION_Request_GetByRefid(string refid)
    {
        List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            list = DAL.PLM_TRANSACTION_Request_GetByRefid(refid);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PLM_PermitCertificates> PLM_TRANSACTION_NotificationReceiver_GetByRefId_AndSupplierCode(string refid, string code)
    {
        List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            list = DAL.PLM_TRANSACTION_NotificationReceiver_GetByRefId_AndSupplierCode(refid, code);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PLM_PermitCertificates> PLM_TRANSACTION_Request_History_GetAll()
    {
        List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            list = DAL.PLM_TRANSACTION_Request_History_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PLM_PermitCertificates> PLM_TRANSACTION_Request_History_GetByRefId(string refid)
    {
        List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            list = DAL.PLM_TRANSACTION_Request_History_GetByRefId(refid);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PLM_PermitCertificates> PLM_TRANSACTION_Request_GetAll_Like(string item)
    {
        List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            list = DAL.PLM_TRANSACTION_Request_GetAll_Like(item);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PLM_PermitCertificates> PLM_TRANSACTION_RequestEntry_Fill_All_DropdownList()
    {
        List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            list = DAL.PLM_TRANSACTION_RequestEntry_Fill_All_DropdownList();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int PLM_TRANSACTION_Request_Insert(Entities_PLM_PermitCertificates entity)
    {
        int ret = 0;

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            ret = DAL.PLM_TRANSACTION_Request_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PLM_TRANSACTION_Request_Append(Entities_PLM_PermitCertificates entity)
    {
        int ret = 0;

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            ret = DAL.PLM_TRANSACTION_Request_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PLM_TRANSACTION_SQLTransaction(string query)
    {
        int ret = 0;

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            ret = DAL.PLM_TRANSACTION_SQLTransaction(query);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    #endregion

    #region GOVERNMENT AGENCIES

    public List<Entities_PLM_GovernmentAgencies> PLM_MT_GovernmentAgencies_GetAll()
    {
        List<Entities_PLM_GovernmentAgencies> list = new List<Entities_PLM_GovernmentAgencies>();

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            list = DAL.PLM_MT_GovernmentAgencies_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int PLM_MT_GovernmentAgencies_IsDisabled(Entities_PLM_GovernmentAgencies entity)
    {
        int ret = 0;

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            ret = DAL.PLM_MT_GovernmentAgencies_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PLM_MT_GovernmentAgencies_Append(Entities_PLM_GovernmentAgencies entity)
    {
        int ret = 0;

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            ret = DAL.PLM_MT_GovernmentAgencies_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PLM_MT_GovernmentAgencies_Insert(Entities_PLM_GovernmentAgencies entity)
    {
        int ret = 0;

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            ret = DAL.PLM_MT_GovernmentAgencies_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_PLM_GovernmentAgencies> PLM_MT_GovernmentAgencies_GetByName(string name)
    {
        List<Entities_PLM_GovernmentAgencies> list = new List<Entities_PLM_GovernmentAgencies>();

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            list = DAL.PLM_MT_GovernmentAgencies_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PLM_GovernmentAgencies> PLM_MT_GovernmentAgencies_GetByName_Like(string name)
    {
        List<Entities_PLM_GovernmentAgencies> list = new List<Entities_PLM_GovernmentAgencies>();

        try
        {
            DAL_PLM DAL = new DAL_PLM();
            list = DAL.PLM_MT_GovernmentAgencies_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion

}