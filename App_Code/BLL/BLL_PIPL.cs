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

public class BLL_PIPL
{
    public BLL_PIPL()
    {
    }

    #region COMPANY

    public List<Entities_PIPL_Company> PIPL_MT_Company_GetAll()
    {
        List<Entities_PIPL_Company> list = new List<Entities_PIPL_Company>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_Company_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_Company> PIPL_MT_Company_GetByName(string name)
    {
        List<Entities_PIPL_Company> list = new List<Entities_PIPL_Company>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_Company_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_Company> PIPL_MT_Company_GetByName_Like(string name)
    {
        List<Entities_PIPL_Company> list = new List<Entities_PIPL_Company>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_Company_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    public int PIPL_MT_Company_Append(Entities_PIPL_Company entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_Company_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_Company_Insert(Entities_PIPL_Company entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_Company_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_Company_IsDisabled(Entities_PIPL_Company entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_Company_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    #endregion

    #region PURPOSE

    public List<Entities_PIPL_Purpose> PIPL_MT_Purpose_GetAll()
    {
        List<Entities_PIPL_Purpose> list = new List<Entities_PIPL_Purpose>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_Purpose_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int PIPL_MT_Purpose_IsDisabled(Entities_PIPL_Purpose entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_Purpose_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_Purpose_Append(Entities_PIPL_Purpose entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_Purpose_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_Purpose_Insert(Entities_PIPL_Purpose entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_Purpose_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_PIPL_Purpose> PIPL_MT_Purpose_GetByName(string name)
    {
        List<Entities_PIPL_Purpose> list = new List<Entities_PIPL_Purpose>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_Purpose_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_Purpose> PIPL_MT_Purpose_GetByName_Like(string name)
    {
        List<Entities_PIPL_Purpose> list = new List<Entities_PIPL_Purpose>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_Purpose_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion

    #region MODE OF SHIPMENT

    public List<Entities_PIPL_ModeOfShipment> PIPL_MT_ModeOfShipment_GetAll()
    {
        List<Entities_PIPL_ModeOfShipment> list = new List<Entities_PIPL_ModeOfShipment>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_ModeOfShipment_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int PIPL_MT_ModeOfShipment_IsDisabled(Entities_PIPL_ModeOfShipment entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_ModeOfShipment_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_ModeOfShipment_Append(Entities_PIPL_ModeOfShipment entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_ModeOfShipment_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_ModeOfShipment_Insert(Entities_PIPL_ModeOfShipment entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_ModeOfShipment_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_PIPL_ModeOfShipment> PIPL_MT_ModeOfShipment_GetByName(string name)
    {
        List<Entities_PIPL_ModeOfShipment> list = new List<Entities_PIPL_ModeOfShipment>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_ModeOfShipment_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_ModeOfShipment> PIPL_MT_ModeOfShipment_GetByName_Like(string name)
    {
        List<Entities_PIPL_ModeOfShipment> list = new List<Entities_PIPL_ModeOfShipment>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_ModeOfShipment_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion

    #region COMMERCIAL VALUE

    public List<Entities_PIPL_CommercialValue> PIPL_MT_CommercialValue_GetAll()
    {
        List<Entities_PIPL_CommercialValue> list = new List<Entities_PIPL_CommercialValue>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_CommercialValue_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int PIPL_MT_CommercialValue_IsDisabled(Entities_PIPL_CommercialValue entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_CommercialValue_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_CommercialValue_Append(Entities_PIPL_CommercialValue entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_CommercialValue_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_CommercialValue_Insert(Entities_PIPL_CommercialValue entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_CommercialValue_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_PIPL_CommercialValue> PIPL_MT_CommercialValue_GetByName(string name)
    {
        List<Entities_PIPL_CommercialValue> list = new List<Entities_PIPL_CommercialValue>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_CommercialValue_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_CommercialValue> PIPL_MT_CommercialValue_GetByName_Like(string name)
    {
        List<Entities_PIPL_CommercialValue> list = new List<Entities_PIPL_CommercialValue>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_CommercialValue_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion

    #region TRADE TERMS

    public List<Entities_PIPL_TradeTerms> PIPL_MT_TradeTerms_GetAll()
    {
        List<Entities_PIPL_TradeTerms> list = new List<Entities_PIPL_TradeTerms>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_TradeTerms_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int PIPL_MT_TradeTerms_IsDisabled(Entities_PIPL_TradeTerms entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_TradeTerms_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_TradeTerms_Append(Entities_PIPL_TradeTerms entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_TradeTerms_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_TradeTerms_Insert(Entities_PIPL_TradeTerms entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_TradeTerms_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_PIPL_TradeTerms> PIPL_MT_TradeTerms_GetByName(string name)
    {
        List<Entities_PIPL_TradeTerms> list = new List<Entities_PIPL_TradeTerms>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_TradeTerms_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_TradeTerms> PIPL_MT_TradeTerms_GetByName_Like(string name)
    {
        List<Entities_PIPL_TradeTerms> list = new List<Entities_PIPL_TradeTerms>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_TradeTerms_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion

    #region PICKUP LOCATION

    public List<Entities_PIPL_PickupLocation> PIPL_MT_PickupLocation_GetAll()
    {
        List<Entities_PIPL_PickupLocation> list = new List<Entities_PIPL_PickupLocation>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_PickupLocation_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int PIPL_MT_PickupLocation_IsDisabled(Entities_PIPL_PickupLocation entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_PickupLocation_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_PickupLocation_Append(Entities_PIPL_PickupLocation entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_PickupLocation_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_PickupLocation_Insert(Entities_PIPL_PickupLocation entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_PickupLocation_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_PIPL_PickupLocation> PIPL_MT_PickupLocation_GetByName(string name)
    {
        List<Entities_PIPL_PickupLocation> list = new List<Entities_PIPL_PickupLocation>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_PickupLocation_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_PickupLocation> PIPL_MT_PickupLocation_GetByName_Like(string name)
    {
        List<Entities_PIPL_PickupLocation> list = new List<Entities_PIPL_PickupLocation>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_PickupLocation_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion

    #region PACKING

    public List<Entities_PIPL_Packing> PIPL_MT_Packing_GetAll()
    {
        List<Entities_PIPL_Packing> list = new List<Entities_PIPL_Packing>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_Packing_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int PIPL_MT_Packing_IsDisabled(Entities_PIPL_Packing entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_Packing_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_Packing_Append(Entities_PIPL_Packing entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_Packing_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_Packing_Insert(Entities_PIPL_Packing entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_Packing_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_PIPL_Packing> PIPL_MT_Packing_GetByName(string name)
    {
        List<Entities_PIPL_Packing> list = new List<Entities_PIPL_Packing>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_Packing_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_Packing> PIPL_MT_Packing_GetByName_Like(string name)
    {
        List<Entities_PIPL_Packing> list = new List<Entities_PIPL_Packing>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_Packing_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion

    #region NATURE OF GOODS

    public List<Entities_PIPL_NatureOfGoods> PIPL_MT_NatureOfGoods_GetAll()
    {
        List<Entities_PIPL_NatureOfGoods> list = new List<Entities_PIPL_NatureOfGoods>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_NatureOfGoods_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int PIPL_MT_NatureOfGoods_IsDisabled(Entities_PIPL_NatureOfGoods entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_NatureOfGoods_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_NatureOfGoods_Append(Entities_PIPL_NatureOfGoods entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_NatureOfGoods_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_NatureOfGoods_Insert(Entities_PIPL_NatureOfGoods entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_NatureOfGoods_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_PIPL_NatureOfGoods> PIPL_MT_NatureOfGoods_GetByName(string name)
    {
        List<Entities_PIPL_NatureOfGoods> list = new List<Entities_PIPL_NatureOfGoods>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_NatureOfGoods_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_NatureOfGoods> PIPL_MT_NatureOfGoods_GetByName_Like(string name)
    {
        List<Entities_PIPL_NatureOfGoods> list = new List<Entities_PIPL_NatureOfGoods>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_NatureOfGoods_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion

    #region COUNTRY OF ORIGIN

    public List<Entities_PIPL_CountryOfOrigin> PIPL_MT_CountryOfOrigin_GetAll()
    {
        List<Entities_PIPL_CountryOfOrigin> list = new List<Entities_PIPL_CountryOfOrigin>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_CountryOfOrigin_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int PIPL_MT_CountryOfOrigin_IsDisabled(Entities_PIPL_CountryOfOrigin entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_CountryOfOrigin_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_CountryOfOrigin_Append(Entities_PIPL_CountryOfOrigin entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_CountryOfOrigin_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_CountryOfOrigin_Insert(Entities_PIPL_CountryOfOrigin entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_CountryOfOrigin_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_PIPL_CountryOfOrigin> PIPL_MT_CountryOfOrigin_GetByName(string name)
    {
        List<Entities_PIPL_CountryOfOrigin> list = new List<Entities_PIPL_CountryOfOrigin>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_CountryOfOrigin_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_CountryOfOrigin> PIPL_MT_CountryOfOrigin_GetByName_Like(string name)
    {
        List<Entities_PIPL_CountryOfOrigin> list = new List<Entities_PIPL_CountryOfOrigin>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_CountryOfOrigin_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion

    #region CASE UNIT

    public List<Entities_PIPL_CaseUnit> PIPL_MT_CaseUnit_GetAll()
    {
        List<Entities_PIPL_CaseUnit> list = new List<Entities_PIPL_CaseUnit>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_CaseUnit_GetAll();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int PIPL_MT_CaseUnit_IsDisabled(Entities_PIPL_CaseUnit entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_CaseUnit_IsDisabled(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_CaseUnit_Append(Entities_PIPL_CaseUnit entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_CaseUnit_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_MT_CaseUnit_Insert(Entities_PIPL_CaseUnit entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_MT_CaseUnit_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_PIPL_CaseUnit> PIPL_MT_CaseUnit_GetByName(string name)
    {
        List<Entities_PIPL_CaseUnit> list = new List<Entities_PIPL_CaseUnit>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_CaseUnit_GetByName(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_CaseUnit> PIPL_MT_CaseUnit_GetByName_Like(string name)
    {
        List<Entities_PIPL_CaseUnit> list = new List<Entities_PIPL_CaseUnit>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_MT_CaseUnit_GetByName_Like(name);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    #endregion

    #region PIPL TRANSACTION INVOICE ENTRY

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_InvoiceEntry_Fill_All_DropdownList()
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_TRANSACTION_InvoiceEntry_Fill_All_DropdownList();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int PIPL_TRANSACTION_RequestHead_Insert(Entities_PIPL_InvoiceEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_TRANSACTION_RequestHead_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_TRANSACTION_RequestHead_Append(Entities_PIPL_InvoiceEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_TRANSACTION_RequestHead_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_TRANSACTION_RequestStatus_Insert(Entities_PIPL_InvoiceEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_TRANSACTION_RequestStatus_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_TRANSACTION_RequestDetails_Insert(Entities_PIPL_InvoiceEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_TRANSACTION_RequestDetails_Insert(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_TRANSACTION_RequestDetails_Append(Entities_PIPL_InvoiceEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_TRANSACTION_RequestDetails_Append(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public Int32 PIPL_TRANSACTION_RequestHead_Count(string year)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_TRANSACTION_RequestHead_Count(year);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }



    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_RequestStatus_ByDateRange(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_TRANSACTION_RequestStatus_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_RequestStatus_ByDateRange_All(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_TRANSACTION_RequestStatus_ByDateRange_All(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_RequestStatus_ByDateRange_All_Reporting(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_TRANSACTION_RequestStatus_ByDateRange_All_Reporting(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_Reporting_ByDateRange(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_TRANSACTION_Reporting_ByDateRange(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_Reporting_ByDateRange_ByDivision(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_TRANSACTION_Reporting_ByDateRange_ByDivision(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_Reporting_ByDateRange_ByAll(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_TRANSACTION_Reporting_ByDateRange_ByAll(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_Reporting_ByDateRange_Details(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_TRANSACTION_Reporting_ByDateRange_Details(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like_All(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like_All(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_RequestDetails_GetByControlNo(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_TRANSACTION_RequestDetails_GetByControlNo(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_ForApproval(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_TRANSACTION_ForApproval(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }

    public int PIPL_TRANSACTION_ApprovedManager(Entities_PIPL_InvoiceEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_TRANSACTION_ApprovedManager(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_TRANSACTION_ApprovedPCManager(Entities_PIPL_InvoiceEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_TRANSACTION_ApprovedPCManager(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_TRANSACTION_ApprovedPCManager2(Entities_PIPL_InvoiceEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_TRANSACTION_ApprovedPCManager2(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_TRANSACTION_ApprovedAccounting(Entities_PIPL_InvoiceEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_TRANSACTION_ApprovedAccounting(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_TRANSACTION_ApprovedAccounting_AUTOAPPROVED(Entities_PIPL_InvoiceEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_TRANSACTION_ApprovedAccounting_AUTOAPPROVED(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_TRANSACTION_ApprovedIncharge(Entities_PIPL_InvoiceEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_TRANSACTION_ApprovedIncharge(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public int PIPL_TRANSACTION_ApprovedImpex(Entities_PIPL_InvoiceEntry entity)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_TRANSACTION_ApprovedImpex(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_GetRequesterEmailAndLocalNumber_ByCTRLNo(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            list = DAL.PIPL_TRANSACTION_GetRequesterEmailAndLocalNumber_ByCTRLNo(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return list;
    }


    public int PIPL_TRANSACTION_SQLTransaction(string query)
    {
        int ret = 0;

        try
        {
            DAL_PIPL DAL = new DAL_PIPL();
            ret = DAL.PIPL_TRANSACTION_SQLTransaction(query);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return ret;
    }




    #endregion


}


