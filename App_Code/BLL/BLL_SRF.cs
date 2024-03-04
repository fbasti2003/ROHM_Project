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

public class BLL_SRF
{

    public BLL_SRF()
    {
    }

    #region PURPOSE OF PULLOUT

    public List<Entities_SRF_PurposeOfPullOut> SRF_MT_PurposeOfPullOut_GetAll()
    {
        List<Entities_SRF_PurposeOfPullOut> list = new List<Entities_SRF_PurposeOfPullOut>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_MT_PurposeOfPullOut_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int SRF_MT_PurposeOfPullOut_IsDisabled(Entities_SRF_PurposeOfPullOut entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_MT_PurposeOfPullOut_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_MT_PurposeOfPullOut_Append(Entities_SRF_PurposeOfPullOut entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_MT_PurposeOfPullOut_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_MT_PurposeOfPullOut_Insert(Entities_SRF_PurposeOfPullOut entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_MT_PurposeOfPullOut_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_SRF_PurposeOfPullOut> SRF_MT_PurposeOfPullOut_GetByName(string name)
    {
        List<Entities_SRF_PurposeOfPullOut> list = new List<Entities_SRF_PurposeOfPullOut>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_MT_PurposeOfPullOut_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_PurposeOfPullOut> SRF_MT_PurposeOfPullOut_GetByName_Like(string name)
    {
        List<Entities_SRF_PurposeOfPullOut> list = new List<Entities_SRF_PurposeOfPullOut>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_MT_PurposeOfPullOut_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    #endregion

    #region LOA

    public List<Entities_SRF_LOA> SRF_MT_LOA_GetAll()
    {
        List<Entities_SRF_LOA> list = new List<Entities_SRF_LOA>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_MT_LOA_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int SRF_MT_LOA_IsDisabled(Entities_SRF_LOA entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_MT_LOA_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_MT_LOA_Append(Entities_SRF_LOA entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_MT_LOA_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_MT_LOA_Insert(Entities_SRF_LOA entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_MT_LOA_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_MT_LOA_UpdateBalance_ByRefId(Entities_SRF_LOA entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_MT_LOA_UpdateBalance_ByRefId(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_SRF_LOA> SRF_MT_LOA_GetByName(string name)
    {
        List<Entities_SRF_LOA> list = new List<Entities_SRF_LOA>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_MT_LOA_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_LOA> SRF_MT_LOA_GetByName_Like(string name)
    {
        List<Entities_SRF_LOA> list = new List<Entities_SRF_LOA>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_MT_LOA_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    #endregion

    #region PULL OUT OF CONTAINER TUBES

    public List<Entities_SRF_PO_ContainerTubes> SRF_MT_PullOutOfContainerTubes_GetAll()
    {
        List<Entities_SRF_PO_ContainerTubes> list = new List<Entities_SRF_PO_ContainerTubes>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_MT_PullOutOfContainerTubes_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int SRF_MT_PullOutOfContainerTubes_IsDisabled(Entities_SRF_PO_ContainerTubes entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_MT_PullOutOfContainerTubes_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_MT_PullOutOfContainerTubes_Append(Entities_SRF_PO_ContainerTubes entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_MT_PullOutOfContainerTubes_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_MT_PullOutOfContainerTubes_Insert(Entities_SRF_PO_ContainerTubes entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_MT_PullOutOfContainerTubes_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    #endregion

    #region PULL OUT OF IC TRAYS

    public List<Entities_SRF_PO_ICTrays> SRF_MT_PullOutICTrays_GetAll()
    {
        List<Entities_SRF_PO_ICTrays> list = new List<Entities_SRF_PO_ICTrays>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_MT_PullOutICTrays_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int SRF_MT_PullOutICTrays_IsDisabled(Entities_SRF_PO_ICTrays entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_MT_PullOutICTrays_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_MT_PullOutICTrays_Append(Entities_SRF_PO_ICTrays entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_MT_PullOutICTrays_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_MT_PullOutICTrays_Insert(Entities_SRF_PO_ICTrays entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_MT_PullOutICTrays_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    #endregion

    #region PULL OUT OF OTHERS

    public List<Entities_SRF_PO_Others> SRF_MT_PullOutOthers_GetAll()
    {
        List<Entities_SRF_PO_Others> list = new List<Entities_SRF_PO_Others>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_MT_PullOutOthers_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int SRF_MT_PullOutOthers_IsDisabled(Entities_SRF_PO_Others entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_MT_PullOutOthers_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_MT_PullOutOthers_Append(Entities_SRF_PO_Others entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_MT_PullOutOthers_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_MT_PullOutOthers_Insert(Entities_SRF_PO_Others entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_MT_PullOutOthers_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    #endregion


    #region PULL OUT TRANSACTION

    public Int32 SRF_TRANSACTION_PO_Request_Count(string year)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_TRANSACTION_PO_Request_Count(year);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_TRANSACTION_PO_SQLTransaction(string query)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_TRANSACTION_PO_SQLTransaction(query);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_SRF_PO_Entry> SRF_TRANSACTION_PO_TOP_10_REQUEST(string date, string problemEncountered, string srf_head)
    {
        List<Entities_SRF_PO_Entry> list = new List<Entities_SRF_PO_Entry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_PO_TOP_10_REQUEST(date, problemEncountered, srf_head);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_PO_Entry> SRF_TRANSACTION_PO_WITH_SRF_HEAD(string SRF_HEAD)
    {
        List<Entities_SRF_PO_Entry> list = new List<Entities_SRF_PO_Entry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_PO_WITH_SRF_HEAD(SRF_HEAD);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_PO_Entry> SRF_TRANSACTION_PO_AllRequest_ByDateRange(Entities_SRF_PO_Entry entity)
    {
        List<Entities_SRF_PO_Entry> list = new List<Entities_SRF_PO_Entry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_PO_AllRequest_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_PO_Entry> SRF_TRANSACTION_PO_Report_ByRange(Entities_SRF_PO_Entry entity)
    {
        List<Entities_SRF_PO_Entry> list = new List<Entities_SRF_PO_Entry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_PO_Report_ByRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_PO_Entry> SRF_TRANSACTION_PO_AllRequest_ByCTRLNo(string ctrlno)
    {
        List<Entities_SRF_PO_Entry> list = new List<Entities_SRF_PO_Entry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_PO_AllRequest_ByCTRLNo(ctrlno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    #endregion




    #region SRF TRANSACTION ENTRY

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestEntry_Fill_All_DropdownList()
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_RequestEntry_Fill_All_DropdownList();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestEntry_ByControlNo(string controlno)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_RequestEntry_ByControlNo(controlno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int SRF_TRANSACTION_RequestEntry_Insert(Entities_SRF_RequestEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_TRANSACTION_RequestEntry_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_TRANSACTION_RequestEntry_Details_Insert(Entities_SRF_RequestEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_TRANSACTION_RequestEntry_Details_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_TRANSACTION_RequestEntry_Update(Entities_SRF_RequestEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_TRANSACTION_RequestEntry_Update(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_TRANSACTION_RequestEntry_Update_GPNO_ByControlNo(Entities_SRF_RequestEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_TRANSACTION_RequestEntry_Update_GPNO_ByControlNo(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public Int32 SRF_TRANSACTION_Request_Count(string year)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_TRANSACTION_Request_Count(year);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }
    public int SRF_TRANSACTION_Status_Insert(Entities_SRF_RequestEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_TRANSACTION_Status_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_TRANSACTION_RequestEntry_Details_Delete(Entities_SRF_RequestEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_TRANSACTION_RequestEntry_Details_Delete(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like(Entities_SRF_RequestEntry entity)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like2(Entities_SRF_RequestEntry entity)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like2(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestStatus_ByControlNo(string ctrlno)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_RequestStatus_ByControlNo(ctrlno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestEntry_Details_ByControlNo(Entities_SRF_RequestEntry entity)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_RequestEntry_Details_ByControlNo(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public Int32 SRF_TRANSACTION_Warehouse_ActualDelivery_Attachment_Count(string srfNumber)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_TRANSACTION_Warehouse_ActualDelivery_Attachment_Count(srfNumber);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public Int32 SRF_TRANSACTION_Warehouse_ActualDelivery_Attachment_Count_ForWarehouse(string srfNumber)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_TRANSACTION_Warehouse_ActualDelivery_Attachment_Count_ForWarehouse(srfNumber);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public Int32 SRF_TRANSACTION_Warehouse_Attachment_Count(string srfNumber)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_TRANSACTION_Warehouse_Attachment_Count(srfNumber);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestEntry_Details_ByControlNo_ForWarehouse(string ctrlno)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_RequestEntry_Details_ByControlNo_ForWarehouse(ctrlno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_QuantityAdjustmentHistory(Entities_SRF_RequestEntry entity)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_QuantityAdjustmentHistory(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestEntry_Details_ByControlNo_ForWarehouse_ActualProgress(string ctrlno)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_RequestEntry_Details_ByControlNo_ForWarehouse_ActualProgress(ctrlno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_WarehouseItemsWithoutDocuments()
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_WarehouseItemsWithoutDocuments();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestEntry_Details_ByControlNo_ForWarehouse2(string ctrlno)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_RequestEntry_Details_ByControlNo_ForWarehouse2(ctrlno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_GetActualDeliveryByCTRLNo(string ctrlno)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_GetActualDeliveryByCTRLNo(ctrlno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_GetActualDeliveryByCTRLNoWithoutDocuments(string ctrlno)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_GetActualDeliveryByCTRLNoWithoutDocuments(ctrlno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_GetInProgressByCTRLNo(string ctrlno)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_GetInProgressByCTRLNo(ctrlno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Get_8105_By_CTRLNo(string ctrlno)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_Get_8105_By_CTRLNo(ctrlno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestStatus_ByDateRange(Entities_SRF_RequestEntry entity)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_RequestStatus_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestStatus_ByDateRange2(Entities_SRF_RequestEntry entity)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_RequestStatus_ByDateRange2(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestStatus_ByDateRange_All(Entities_SRF_RequestEntry entity)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_RequestStatus_ByDateRange_All(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_AllRequest_Reports(string from, string to)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_AllRequest_Reports(from, to);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_AllRequest_Reports_New(string from, string to)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_AllRequest_Reports_New(from, to);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_AllRequest_Reports_New2(string ctrlno)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_AllRequest_Reports_New2(ctrlno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like_All(Entities_SRF_RequestEntry entity)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like_All(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_ForApproval(Entities_SRF_RequestEntry entity)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_ForApproval(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public string SRF_TRANSACTION_IsApprovedByProdManager(string ctrlno)
    {
        string retVal = string.Empty;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            retVal = DAL.SRF_TRANSACTION_IsApprovedByProdManager(ctrlno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return retVal;
    }

    public string SRF_TRANSACTION_IsApprovedByPURIncharge(string ctrlno)
    {
        string retVal = string.Empty;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            retVal = DAL.SRF_TRANSACTION_IsApprovedByPURIncharge(ctrlno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return retVal;
    }

    public int SRF_TRANSACTION_ApprovedRequestorIncharge(Entities_SRF_RequestEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_TRANSACTION_ApprovedRequestorIncharge(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_TRANSACTION_ApprovedRequestorManager(Entities_SRF_RequestEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_TRANSACTION_ApprovedRequestorManager(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_TRANSACTION_ApprovedPurchasingIncharge(Entities_SRF_RequestEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_TRANSACTION_ApprovedPurchasingIncharge(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_TRANSACTION_ApprovedPurchasingManager(Entities_SRF_RequestEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_TRANSACTION_ApprovedPurchasingManager(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int SRF_TRANSACTION_ApprovedPurchasingDeptManager(Entities_SRF_RequestEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_TRANSACTION_ApprovedPurchasingDeptManager(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_SRF_LOA> SRF_TRANSACTION_LOA_DISTRIBUTION(string from, string to)
    {
        List<Entities_SRF_LOA> list = new List<Entities_SRF_LOA>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_LOA_DISTRIBUTION(from, to);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_AllRequest_FreeForm(Entities_SRF_RequestEntry entity)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_AllRequest_FreeForm(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_ReceivingEntry(string search)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_Warehouse_ReceivingEntry(search);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(string search, string from, string to)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(search, from, to);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_Reporting_FromDashboard_Summarize(string search, string from, string to)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_Warehouse_Reporting_FromDashboard_Summarize(search, from, to);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_ReceivingEntryTotalOverallQuantity(string from, string to)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_Warehouse_ReceivingEntryTotalOverallQuantity(from, to);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange_DashboardCount(string from, string to)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange_DashboardCount(from, to);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_8105_Entry(string search)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_8105_Entry(search);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_Reporting(string search, string from, string to)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_Warehouse_Reporting(search, from, to);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_Reporting_FromDashboard(string search, string from, string to, string ctrlnoList)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_Warehouse_Reporting_FromDashboard(search, from, to, ctrlnoList);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_Reporting_FromDashboard_2(string search, string ctrlnoList)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_Warehouse_Reporting_FromDashboard_2(search, ctrlnoList);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_Reporting_Distinct_SupplierName(string search, string from, string to)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_Warehouse_Reporting_Distinct_SupplierName(search, from, to);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_8105_Entry2(string search)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_8105_Entry2(search);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_8105_Entry2WithDateRange(string search, string from, string to, string isChecked)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_8105_Entry2WithDateRange(search, from, to, isChecked);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_GetAttachment(string ctrlno)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_Warehouse_GetAttachment(ctrlno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int SRF_TRANSACTION_SQLTransaction(string query)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_TRANSACTION_SQLTransaction(query);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Reporting_ByDateRange(Entities_SRF_RequestEntry entity)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_Reporting_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Reporting_ByDateRange_ByDivision(Entities_SRF_RequestEntry entity)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_Reporting_ByDateRange_ByDivision(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Reporting_ByDateRange_ByAll(Entities_SRF_RequestEntry entity)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_Reporting_ByDateRange_ByAll(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Reporting_ByDateRange_Details(Entities_SRF_RequestEntry entity)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_Reporting_ByDateRange_Details(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_IsPullOutRequestExistingInSRF(Entities_SRF_RequestEntry entity)
    {
        List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            list = DAL.SRF_TRANSACTION_IsPullOutRequestExistingInSRF(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    public Int32 SRF_TRANSACTION_WAREHOUSE_CheckIfMoreThanOneDelivery(string ctrlno)
    {
        int ret = 0;

        try
        {
            DAL_SRF DAL = new DAL_SRF();
            ret = DAL.SRF_TRANSACTION_WAREHOUSE_CheckIfMoreThanOneDelivery(ctrlno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }


    #endregion

}