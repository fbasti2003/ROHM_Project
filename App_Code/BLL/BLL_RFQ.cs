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

public class BLL_RFQ
{
    public BLL_RFQ()
    {
    }

    #region SECTION

    public List<Entities_RFQ_Section> RFQ_MT_Section_GetAll_Export()
    {
        List<Entities_RFQ_Section> list = new List<Entities_RFQ_Section>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Section_GetAll_Export();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_Section> RFQ_MT_Section_GetAll()
    {
        List<Entities_RFQ_Section> list = new List<Entities_RFQ_Section>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Section_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    public int RFQ_MT_Section_IsDisabled(Entities_RFQ_Section entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_Section_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_Section_Append(Entities_RFQ_Section entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_Section_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_Section_Insert(Entities_RFQ_Section entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_Section_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_RFQ_Section> RFQ_MT_Section_GetByDescription(string description)
    {
        List<Entities_RFQ_Section> list = new List<Entities_RFQ_Section>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Section_GetByDescription(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_Section> RFQ_MT_Section_GetByDescription_Like(string description)
    {
        List<Entities_RFQ_Section> list = new List<Entities_RFQ_Section>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Section_GetByDescription_Like(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion

    #region Department

    public List<Entities_RFQ_Department> RFQ_MT_Department_GetAll()
    {
        List<Entities_RFQ_Department> list = new List<Entities_RFQ_Department>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Department_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_Department> RFQ_MT_Department_GetAll_Export()
    {
        List<Entities_RFQ_Department> list = new List<Entities_RFQ_Department>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Department_GetAll_Export();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int RFQ_MT_Department_IsDisabled(Entities_RFQ_Department entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_Department_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_Department_Append(Entities_RFQ_Department entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_Department_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_Department_Insert(Entities_RFQ_Department entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_Department_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_RFQ_Department> RFQ_MT_Department_GetByDescription(string description)
    {
        List<Entities_RFQ_Department> list = new List<Entities_RFQ_Department>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Department_GetByDescription(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_Department> RFQ_MT_Department_GetByDescription_Like(string description)
    {
        List<Entities_RFQ_Department> list = new List<Entities_RFQ_Department>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Department_GetByDescription_Like(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    #endregion

    #region Division

    public List<Entities_RFQ_Division> RFQ_MT_Division_GetAll()
    {
        List<Entities_RFQ_Division> list = new List<Entities_RFQ_Division>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Division_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_Division> RFQ_MT_Division_GetAll_Export()
    {
        List<Entities_RFQ_Division> list = new List<Entities_RFQ_Division>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Division_GetAll_Export();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int RFQ_MT_Division_IsDisabled(Entities_RFQ_Division entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_Division_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_Division_Append(Entities_RFQ_Division entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_Division_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_Division_Insert(Entities_RFQ_Division entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_Division_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_RFQ_Division> RFQ_MT_Division_GetByDescription(string description)
    {
        List<Entities_RFQ_Division> list = new List<Entities_RFQ_Division>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Division_GetByDescription(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_Division> RFQ_MT_Division_GetByDescription_Like(string description)
    {
        List<Entities_RFQ_Division> list = new List<Entities_RFQ_Division>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Division_GetByDescription_Like(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    #endregion

    #region PC

    public List<Entities_RFQ_PC> RFQ_MT_PC_GetAll()
    {
        List<Entities_RFQ_PC> list = new List<Entities_RFQ_PC>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_PC_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int RFQ_MT_PC_IsDisabled(Entities_RFQ_PC entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_PC_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_PC_Append(Entities_RFQ_PC entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_PC_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_PC_Insert(Entities_RFQ_PC entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_PC_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_RFQ_PC> RFQ_MT_PC_GetByDescription(string description)
    {
        List<Entities_RFQ_PC> list = new List<Entities_RFQ_PC>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_PC_GetByDescription(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_PC> RFQ_MT_PC_GetByDescription_Like(string description)
    {
        List<Entities_RFQ_PC> list = new List<Entities_RFQ_PC>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_PC_GetByDescription_Like(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    #endregion

    #region HQ

    public List<Entities_RFQ_HQ> RFQ_MT_HQ_GetAll()
    {
        List<Entities_RFQ_HQ> list = new List<Entities_RFQ_HQ>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_HQ_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int RFQ_MT_HQ_IsDisabled(Entities_RFQ_HQ entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_HQ_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_HQ_Append(Entities_RFQ_HQ entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_HQ_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_HQ_Insert(Entities_RFQ_HQ entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_HQ_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_RFQ_HQ> RFQ_MT_HQ_GetByDescription(string description)
    {
        List<Entities_RFQ_HQ> list = new List<Entities_RFQ_HQ>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_HQ_GetByDescription(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_HQ> RFQ_MT_HQ_GetByDescription_Like(string description)
    {
        List<Entities_RFQ_HQ> list = new List<Entities_RFQ_HQ>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_HQ_GetByDescription_Like(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    #endregion

    #region Category

    public List<Entities_RFQ_Category> RFQ_MT_Category_GetAll()
    {
        List<Entities_RFQ_Category> list = new List<Entities_RFQ_Category>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Category_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int RFQ_MT_Category_IsDisabled(Entities_RFQ_Category entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_Category_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_Category_Append(Entities_RFQ_Category entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_Category_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_Category_Insert(Entities_RFQ_Category entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_Category_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_RFQ_Category> RFQ_MT_Category_GetByDescription(string description)
    {
        List<Entities_RFQ_Category> list = new List<Entities_RFQ_Category>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Category_GetByDescription(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_Category> RFQ_MT_Category_GetByDescription_Like(string description)
    {
        List<Entities_RFQ_Category> list = new List<Entities_RFQ_Category>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Category_GetByDescription_Like(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    #endregion

    #region UnitOfMeasure

    public List<Entities_RFQ_UnitOfMeasure> RFQ_MT_UnitOfMeasure_GetAll()
    {
        List<Entities_RFQ_UnitOfMeasure> list = new List<Entities_RFQ_UnitOfMeasure>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_UnitOfMeasure_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int RFQ_MT_UnitOfMeasure_IsDisabled(Entities_RFQ_UnitOfMeasure entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_UnitOfMeasure_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_UnitOfMeasure_Append(Entities_RFQ_UnitOfMeasure entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_UnitOfMeasure_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_UnitOfMeasure_Insert(Entities_RFQ_UnitOfMeasure entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_UnitOfMeasure_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_RFQ_UnitOfMeasure> RFQ_MT_UnitOfMeasure_GetByDescription(string description)
    {
        List<Entities_RFQ_UnitOfMeasure> list = new List<Entities_RFQ_UnitOfMeasure>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_UnitOfMeasure_GetByDescription(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_UnitOfMeasure> RFQ_MT_UnitOfMeasure_GetByDescription_Like(string description)
    {
        List<Entities_RFQ_UnitOfMeasure> list = new List<Entities_RFQ_UnitOfMeasure>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_UnitOfMeasure_GetByDescription_Like(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    #endregion

    #region Currency

    public List<Entities_RFQ_Currency> RFQ_MT_Currency_GetAll()
    {
        List<Entities_RFQ_Currency> list = new List<Entities_RFQ_Currency>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Currency_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int RFQ_MT_Currency_IsDisabled(Entities_RFQ_Currency entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_Currency_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_Currency_Append(Entities_RFQ_Currency entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_Currency_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_Currency_Insert(Entities_RFQ_Currency entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_Currency_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_RFQ_Currency> RFQ_MT_Currency_GetByDescription(string description)
    {
        List<Entities_RFQ_Currency> list = new List<Entities_RFQ_Currency>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Currency_GetByDescription(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_Currency> RFQ_MT_Currency_GetByDescription_Like(string description)
    {
        List<Entities_RFQ_Currency> list = new List<Entities_RFQ_Currency>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Currency_GetByDescription_Like(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    #endregion

    #region Supplier

    public List<Entities_RFQ_Supplier> RFQ_MT_Supplier_GetAll()
    {
        List<Entities_RFQ_Supplier> list = new List<Entities_RFQ_Supplier>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Supplier_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int RFQ_MT_Supplier_IsDisabled(Entities_RFQ_Supplier entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_Supplier_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_Supplier_Append(Entities_RFQ_Supplier entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_Supplier_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_Supplier_Insert(Entities_RFQ_Supplier entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_Supplier_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_Supplier_InsertDetails(Entities_RFQ_Supplier entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_Supplier_InsertDetails(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_RFQ_Supplier> RFQ_MT_Supplier_GetByDescription(string description)
    {
        List<Entities_RFQ_Supplier> list = new List<Entities_RFQ_Supplier>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Supplier_GetByDescription(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_Supplier> RFQ_MT_Supplier_GetByDescription_Like(string description)
    {
        List<Entities_RFQ_Supplier> list = new List<Entities_RFQ_Supplier>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Supplier_GetByDescription_Like(description);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_Category> RFQ_MT_Supplier_GetCategoryByHeadId(string headrefid)
    {
        List<Entities_RFQ_Category> list = new List<Entities_RFQ_Category>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_Supplier_GetCategoryByHeadId(headrefid);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    #endregion

    #region BuyerInformation

    public List<Entities_RFQ_BuyerInformation> RFQ_MT_BuyerInformation_GetAll()
    {
        List<Entities_RFQ_BuyerInformation> list = new List<Entities_RFQ_BuyerInformation>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_BuyerInformation_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int RFQ_MT_BuyerInformation_IsDisabled(Entities_RFQ_BuyerInformation entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_BuyerInformation_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_BuyerInformation_Append(Entities_RFQ_BuyerInformation entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_BuyerInformation_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_MT_BuyerInformation_Insert(Entities_RFQ_BuyerInformation entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_MT_BuyerInformation_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_RFQ_BuyerInformation> RFQ_MT_BuyerInformation_GetByMember(string member)
    {
        List<Entities_RFQ_BuyerInformation> list = new List<Entities_RFQ_BuyerInformation>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_BuyerInformation_GetByMember(member);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_BuyerInformation> RFQ_MT_BuyerInformation_GetByMember_Like(string member)
    {
        List<Entities_RFQ_BuyerInformation> list = new List<Entities_RFQ_BuyerInformation>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_MT_BuyerInformation_GetByMember_Like(member);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    #endregion

    #region TRANSACTIONS

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Monitoring_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_Monitoring_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_MyRequestersItem_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_MyRequestersItem_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_SendDate_ByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_SendDate_ByRFQNo(rfqNo);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_HistoryOfApproval_ByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_HistoryOfApproval_ByRFQNo(rfqNo);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetHoldReason_ByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_GetHoldReason_ByRFQNo(rfqNo);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_IsAlready_Approved_ByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_IsAlready_Approved_ByRFQNo(rfqNo);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_HistoryOfDisapproval_ByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_HistoryOfDisapproval_ByRFQNo(rfqNo);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Monitoring_GetDetailsByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_Monitoring_GetDetailsByRFQNo(rfqNo);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Monitoring_GetDetailsByRFQNo_ForReApply(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_Monitoring_GetDetailsByRFQNo_ForReApply(rfqNo);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetAttachment_ByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_GetAttachment_ByRFQNo(rfqNo);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetRequestDetailsByRFQNoWithUnitPrice_ByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_GetRequestDetailsByRFQNoWithUnitPrice_ByRFQNo(rfqNo);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetRequestDetailsByExistingDescriptionSpecs(string descriptionSpecs)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_GetRequestDetailsByExistingDescriptionSpecs(descriptionSpecs);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetSupplierRespondedOutOfByRFQNo_ByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_GetSupplierRespondedOutOfByRFQNo_ByRFQNo(rfqNo);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_RequestEntry_Fill_All_DropdownList()
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_RequestEntry_Fill_All_DropdownList();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public Int32 RFQ_TRANSACTION_CountRequestHead(string year)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_TRANSACTION_CountRequestHead(year);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_TRANSACTION_RequestEntry_Insert(Entities_RFQ_RequestEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_TRANSACTION_RequestEntry_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(string query)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(query);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetRequestDetailsByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_GetRequestDetailsByRFQNo(rfqNo);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }    

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_PurchasingReceiving_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_PurchasingReceiving_ByDateRange_Tester(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange_Tester(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_PurchasingReceiving_ByDateRange_TodayTopResponse(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange_TodayTopResponse(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_PurchasingReceiving_ByDateRange_ForResend(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_PurchasingReceiving_ByDateRange_ForResend(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel_Resend(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel_Resend(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel_Approved(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel_Approved(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_PurchasingReceiving(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_PurchasingReceiving(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_AllRequest_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_AllRequest_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_AllRequest_ByDateRange_Reporting(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_AllRequest_ByDateRange_Reporting(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_HistoryOfUpdates(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_HistoryOfUpdates(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_HistoryOfUpdates_GetAll_UpdatedBy(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_HistoryOfUpdates_GetAll_UpdatedBy(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_AllRequest_ByDateRange_AllApproved(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_AllRequest_ByDateRange_AllApproved(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetRespondedSupplierByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_GetRespondedSupplierByRFQNo(rfqNo);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetRespondedSupplierByRFQNo_ForReceiving(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_GetRespondedSupplierByRFQNo_ForReceiving(rfqNo);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetSupplierResponseByRFQNoAndSupplierID(string rfqNo, string refid)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_GetSupplierResponseByRFQNoAndSupplierID(rfqNo, refid);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetSupplierResponseByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_GetSupplierResponseByRFQNo(rfqNo);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetSupplierResponseByRFQNoSupplierNameOnly(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_GetSupplierResponseByRFQNoSupplierNameOnly(rfqNo);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetStatusByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_GetStatusByRFQNo(rfqNo);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetSupplierForSendAndWithResponseByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_GetSupplierForSendAndWithResponseByRFQNo(rfqNo);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Approval_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_Approval_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Approval(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_Approval(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Approval_Purchasing_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_Approval_Purchasing_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Approval_Purchasing(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_Approval_Purchasing(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetRequesterEmailAndLocalNumber_ByRFQNo(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_GetRequesterEmailAndLocalNumber_ByRFQNo(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetSupplierAttachmentBySupplierIdAndRFQNo(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_GetSupplierAttachmentBySupplierIdAndRFQNo(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetSupplierAttachmentByRFQNo(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_GetSupplierAttachmentByRFQNo(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetApprovedSupplierAttachmentBySupplierIdAndRFQNo(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_GetApprovedSupplierAttachmentBySupplierIdAndRFQNo(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetTodayTopRespondedSupplier()
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_GetTodayTopRespondedSupplier();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetTodayTopRespondedSupplier2()
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_GetTodayTopRespondedSupplier2();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_OnePage_Head(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_OnePage_Head(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_OnePage_Head_Description(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_OnePage_Head_Description(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_OnePage_Head_Specification(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_OnePage_Head_Specification(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_OnePage_Head_Maker(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_OnePage_Head_Maker(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    public int RFQ_TRANSACTION_UpdateBuyerNotes(string buyerNotes, string rfqno)
    {
        int ret = 0;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            ret = DAL.RFQ_TRANSACTION_UpdateBuyerNotes(buyerNotes, rfqno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public string getPurchasingRemarksByRFQNo(string rfqno)
    {
        string purchasingRemarks = string.Empty;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            purchasingRemarks = DAL.getPurchasingRemarksByRFQNo(rfqno);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return purchasingRemarks;
    }

    public string IsAlreadySentToSupplierToday(string rfqno, string supplierid)
    {
        string retVal = string.Empty;

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            retVal = DAL.IsAlreadySentToSupplierToday(rfqno, supplierid);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return retVal;
    }


    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Reporting_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_Reporting_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Reporting_Sending_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_Reporting_Sending_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Reporting_Resend_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_Reporting_Resend_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Reporting_ALL_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_Reporting_ALL_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Reporting_ByDateRange_ByDivision(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_Reporting_ByDateRange_ByDivision(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Reporting_ByDateRange_ByAll(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_Reporting_ByDateRange_ByAll(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Reporting_ByDateRange_Details(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        try
        {
            DAL_RFQ DAL = new DAL_RFQ();
            list = DAL.RFQ_TRANSACTION_Reporting_ByDateRange_Details(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }



    #endregion

}
