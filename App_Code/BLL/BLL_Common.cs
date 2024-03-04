using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class BLL_Common
{
    public BLL_Common()
    {
    }


    public List<Entities_Common_SystemUsers> getFormList()
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.getFormList();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_Common_ForApproval> Common_GetForApprovalByCategoryId(string category, string position, string department, string division, string hq, string isDivision, string isHQ)
    {
        List<Entities_Common_ForApproval> list = new List<Entities_Common_ForApproval>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.Common_GetForApprovalByCategoryId(category, position, department, division, hq, isDivision, isHQ);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_Common_ForApproval> Common_GetForApprovals()
    {
        List<Entities_Common_ForApproval> list = new List<Entities_Common_ForApproval>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.Common_GetForApprovals();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_Common_ForApproval> Common_GetForApprovals2(string category)
    {
        List<Entities_Common_ForApproval> list = new List<Entities_Common_ForApproval>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.Common_GetForApprovals2(category);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_Common_ForApproval> Common_GetThisWeekRequestStatus()
    {
        List<Entities_Common_ForApproval> list = new List<Entities_Common_ForApproval>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.Common_GetThisWeekRequestStatus();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_Common_ForApproval> Common_RFQ_GetOtherBuyerItems()
    {
        List<Entities_Common_ForApproval> list = new List<Entities_Common_ForApproval>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.Common_RFQ_GetOtherBuyerItems();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    public List<Entities_Common_SystemUsers> getFormListByRefId(string refid)
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.getFormListByRefId(refid);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_Common_SystemUsers> getPossible_RFQ_Approver_ByTransactionAndDepartment(string department, string transaction)
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.getPossible_RFQ_Approver_ByTransactionAndDepartment(department, transaction);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_Common_SystemUsers> getLoginCredentials(string username, string password)
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.getLoginCredentials(username, password);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_Common_SystemUsers> getLoginCredentialsByRefId(string refid)
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.getLoginCredentialsByRefId(refid);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_Common_SystemUsers> getLoginCredentials_ByUserName(string username)
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.getLoginCredentials_ByUserName(username);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_Common_SystemUsers> getLoginCredentials_All()
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.getLoginCredentials_All();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_Common_SystemUsers> getLoginCredentials_All_Export()
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.getLoginCredentials_All_Export();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_Common_SystemUsers> Common_checkIfUserAccessExistByUserId(string loginid, string transaction)
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.Common_checkIfUserAccessExistByUserId(loginid, transaction);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_Common_SystemUsers> Common_UserAccess_Fill_All_DropdownList()
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.Common_UserAccess_Fill_All_DropdownList();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int Common_UpdateLoginCredentials_ByRefId(Entities_Common_SystemUsers entity)
    {
        int ret = 0;

        try
        {
            DAL_Common DAL = new DAL_Common();
            ret = DAL.Common_UpdateLoginCredentials_ByRefId(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int Common_ChangePassword_ByRefId(string password, string refid)
    {
        int ret = 0;

        try
        {
            DAL_Common DAL = new DAL_Common();
            ret = DAL.Common_ChangePassword_ByRefId(password, refid);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int Common_InserUserAccess_ByLoginId(Entities_Common_SystemUsers entity)
    {
        int ret = 0;

        try
        {
            DAL_Common DAL = new DAL_Common();
            ret = DAL.Common_InserUserAccess_ByLoginId(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int Common_DeleteUserAccess_ByLoginId(Entities_Common_SystemUsers entity)
    {
        int ret = 0;

        try
        {
            DAL_Common DAL = new DAL_Common();
            ret = DAL.Common_DeleteUserAccess_ByLoginId(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_Common_SystemUsers> Common_checkIfUserHasTransactionsByUserId(string loginid)
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.Common_checkIfUserHasTransactionsByUserId(loginid);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int Common_DisabledLoginCredentials_ByRefId(Entities_Common_SystemUsers entity)
    {
        int ret = 0;

        try
        {
            DAL_Common DAL = new DAL_Common();
            ret = DAL.Common_DisabledLoginCredentials_ByRefId(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int Common_EnabledLoginCredentials_ByRefId(Entities_Common_SystemUsers entity)
    {
        int ret = 0;

        try
        {
            DAL_Common DAL = new DAL_Common();
            ret = DAL.Common_EnabledLoginCredentials_ByRefId(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int Common_InsertLoginCredentials(Entities_Common_SystemUsers entity)
    {
        int ret = 0;

        try
        {
            DAL_Common DAL = new DAL_Common();
            ret = DAL.Common_InsertLoginCredentials(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int Common_ResetPassword(Entities_Common_SystemUsers entity)
    {
        int ret = 0;

        try
        {
            DAL_Common DAL = new DAL_Common();
            ret = DAL.Common_ResetPassword(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_Common_MTSupplier> Common_getSupplier_ByRefId(string refid)
    {
        List<Entities_Common_MTSupplier> list = new List<Entities_Common_MTSupplier>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.Common_getSupplier_ByRefId(refid);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_Common_IntegrationFromOtherDatabase> Common_getBusinessUnit()
    {
        List<Entities_Common_IntegrationFromOtherDatabase> list = new List<Entities_Common_IntegrationFromOtherDatabase>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.Common_getBusinessUnit();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_Common_IntegrationFromOtherDatabase> Common_getAccountCode()
    {
        List<Entities_Common_IntegrationFromOtherDatabase> list = new List<Entities_Common_IntegrationFromOtherDatabase>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.Common_getAccountCode();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_Common_TransactionLogs> RFQ_TRANSACTION_TransactionLogs_ByDateRange(Entities_Common_TransactionLogs entity)
    {
        List<Entities_Common_TransactionLogs> list = new List<Entities_Common_TransactionLogs>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.RFQ_TRANSACTION_TransactionLogs_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public string GetBuyerEmailAddressByHandledCategory(string category)
    {
        string retVal = string.Empty;

        try
        {
            DAL_Common DAL = new DAL_Common();
            retVal = DAL.GetBuyerEmailAddressByHandledCategory(category);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return retVal;
    }


    #region SYSTEM INFORMATION

    public List<Entities_Common_SystemInformation> Common_SystemInformation_GetAll()
    {
        List<Entities_Common_SystemInformation> list = new List<Entities_Common_SystemInformation>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.Common_SystemInformation_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int Common_SystemInformation_IsDisabled(Entities_Common_SystemInformation entity)
    {
        int ret = 0;

        try
        {
            DAL_Common DAL = new DAL_Common();
            ret = DAL.Common_SystemInformation_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int Common_SystemInformation_Append(Entities_Common_SystemInformation entity)
    {
        int ret = 0;

        try
        {
            DAL_Common DAL = new DAL_Common();
            ret = DAL.Common_SystemInformation_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int Common_SystemInformation_Insert(Entities_Common_SystemInformation entity)
    {
        int ret = 0;

        try
        {
            DAL_Common DAL = new DAL_Common();
            ret = DAL.Common_SystemInformation_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_Common_SystemInformation> Common_SystemInformation_GetByName(string name)
    {
        List<Entities_Common_SystemInformation> list = new List<Entities_Common_SystemInformation>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.Common_SystemInformation_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_Common_SystemInformation> Common_SystemInformation_GetByName_Like(string name)
    {
        List<Entities_Common_SystemInformation> list = new List<Entities_Common_SystemInformation>();

        try
        {
            DAL_Common DAL = new DAL_Common();
            list = DAL.Common_SystemInformation_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion




}
