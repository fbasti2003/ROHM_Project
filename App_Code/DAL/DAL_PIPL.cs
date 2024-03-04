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

public class DAL_PIPL
{
    public DAL_PIPL()
    {
    }

    #region COMPANY

    public List<Entities_PIPL_Company> PIPL_MT_Company_GetAll()
    {
        List<Entities_PIPL_Company> list = new List<Entities_PIPL_Company>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_Company_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_Company entities = new Entities_PIPL_Company();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.Address = reader["Address"].ToString();
                entities.Phone = reader["Phone"].ToString();
                entities.Fax = reader["Fax"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_PIPL_Company> PIPL_MT_Company_GetByName(string name)
    {
        List<Entities_PIPL_Company> list = new List<Entities_PIPL_Company>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_Company_GetByName";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_Company entities = new Entities_PIPL_Company();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.Address = reader["Address"].ToString();
                entities.Phone = reader["Phone"].ToString();
                entities.Fax = reader["Fax"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_PIPL_Company> PIPL_MT_Company_GetByName_Like(string name)
    {
        List<Entities_PIPL_Company> list = new List<Entities_PIPL_Company>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_Company_GetByName_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_Company entities = new Entities_PIPL_Company();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.Address = reader["Address"].ToString();
                entities.Phone = reader["Phone"].ToString();
                entities.Fax = reader["Fax"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }


    public int PIPL_MT_Company_Append(Entities_PIPL_Company entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_Company_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@address", entity.Address));
        cmd.Parameters.Add(Factory.CreateParameter("@phone", entity.Phone));
        cmd.Parameters.Add(Factory.CreateParameter("@fax", entity.Fax));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.UpdatedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_Company_Insert(Entities_PIPL_Company entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_Company_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@address", entity.Address));
        cmd.Parameters.Add(Factory.CreateParameter("@phone", entity.Phone));
        cmd.Parameters.Add(Factory.CreateParameter("@fax", entity.Fax));
        cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.AddedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_Company_IsDisabled(Entities_PIPL_Company entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_Company_IsDisabled";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@isdisabled", entity.IsDisabled));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    #endregion

    #region PURPOSE

    public List<Entities_PIPL_Purpose> PIPL_MT_Purpose_GetAll()
    {
        List<Entities_PIPL_Purpose> list = new List<Entities_PIPL_Purpose>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_Purpose_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_Purpose entities = new Entities_PIPL_Purpose();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.AddedBy = reader["AddedBy"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public int PIPL_MT_Purpose_IsDisabled(Entities_PIPL_Purpose entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_Purpose_IsDisabled";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@isdisabled", entity.IsDisabled));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_Purpose_Append(Entities_PIPL_Purpose entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_Purpose_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.UpdatedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_Purpose_Insert(Entities_PIPL_Purpose entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_Purpose_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.AddedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public List<Entities_PIPL_Purpose> PIPL_MT_Purpose_GetByName(string name)
    {
        List<Entities_PIPL_Purpose> list = new List<Entities_PIPL_Purpose>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_Purpose_GetByName";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_Purpose entities = new Entities_PIPL_Purpose();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_PIPL_Purpose> PIPL_MT_Purpose_GetByName_Like(string name)
    {
        List<Entities_PIPL_Purpose> list = new List<Entities_PIPL_Purpose>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_Purpose_GetByName_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_Purpose entities = new Entities_PIPL_Purpose();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    #endregion

    #region MODE OF SHIPMENT

    public List<Entities_PIPL_ModeOfShipment> PIPL_MT_ModeOfShipment_GetAll()
    {
        List<Entities_PIPL_ModeOfShipment> list = new List<Entities_PIPL_ModeOfShipment>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_ModeOfShipment_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_ModeOfShipment entities = new Entities_PIPL_ModeOfShipment();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.AddedBy = reader["AddedBy"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public int PIPL_MT_ModeOfShipment_IsDisabled(Entities_PIPL_ModeOfShipment entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_ModeOfShipment_IsDisabled";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@isdisabled", entity.IsDisabled));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_ModeOfShipment_Append(Entities_PIPL_ModeOfShipment entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_ModeOfShipment_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.UpdatedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_ModeOfShipment_Insert(Entities_PIPL_ModeOfShipment entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_ModeOfShipment_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.AddedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public List<Entities_PIPL_ModeOfShipment> PIPL_MT_ModeOfShipment_GetByName(string name)
    {
        List<Entities_PIPL_ModeOfShipment> list = new List<Entities_PIPL_ModeOfShipment>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_ModeOfShipment_GetByName";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_ModeOfShipment entities = new Entities_PIPL_ModeOfShipment();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_PIPL_ModeOfShipment> PIPL_MT_ModeOfShipment_GetByName_Like(string name)
    {
        List<Entities_PIPL_ModeOfShipment> list = new List<Entities_PIPL_ModeOfShipment>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_ModeOfShipment_GetByName_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_ModeOfShipment entities = new Entities_PIPL_ModeOfShipment();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    #endregion

    #region COMMERCIAL VALUE

    public List<Entities_PIPL_CommercialValue> PIPL_MT_CommercialValue_GetAll()
    {
        List<Entities_PIPL_CommercialValue> list = new List<Entities_PIPL_CommercialValue>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_CommercialValue_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_CommercialValue entities = new Entities_PIPL_CommercialValue();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.AddedBy = reader["AddedBy"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public int PIPL_MT_CommercialValue_IsDisabled(Entities_PIPL_CommercialValue entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_CommercialValue_IsDisabled";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@isdisabled", entity.IsDisabled));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_CommercialValue_Append(Entities_PIPL_CommercialValue entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_CommercialValue_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.UpdatedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_CommercialValue_Insert(Entities_PIPL_CommercialValue entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_CommercialValue_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.AddedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public List<Entities_PIPL_CommercialValue> PIPL_MT_CommercialValue_GetByName(string name)
    {
        List<Entities_PIPL_CommercialValue> list = new List<Entities_PIPL_CommercialValue>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_CommercialValue_GetByName";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_CommercialValue entities = new Entities_PIPL_CommercialValue();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_PIPL_CommercialValue> PIPL_MT_CommercialValue_GetByName_Like(string name)
    {
        List<Entities_PIPL_CommercialValue> list = new List<Entities_PIPL_CommercialValue>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_CommercialValue_GetByName_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_CommercialValue entities = new Entities_PIPL_CommercialValue();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    #endregion

    #region TRADE TERMS

    public List<Entities_PIPL_TradeTerms> PIPL_MT_TradeTerms_GetAll()
    {
        List<Entities_PIPL_TradeTerms> list = new List<Entities_PIPL_TradeTerms>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_TradeTerms_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_TradeTerms entities = new Entities_PIPL_TradeTerms();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.AddedBy = reader["AddedBy"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public int PIPL_MT_TradeTerms_IsDisabled(Entities_PIPL_TradeTerms entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_TradeTerms_IsDisabled";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@isdisabled", entity.IsDisabled));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_TradeTerms_Append(Entities_PIPL_TradeTerms entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_TradeTerms_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.UpdatedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_TradeTerms_Insert(Entities_PIPL_TradeTerms entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_TradeTerms_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.AddedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public List<Entities_PIPL_TradeTerms> PIPL_MT_TradeTerms_GetByName(string name)
    {
        List<Entities_PIPL_TradeTerms> list = new List<Entities_PIPL_TradeTerms>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_TradeTerms_GetByName";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_TradeTerms entities = new Entities_PIPL_TradeTerms();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_PIPL_TradeTerms> PIPL_MT_TradeTerms_GetByName_Like(string name)
    {
        List<Entities_PIPL_TradeTerms> list = new List<Entities_PIPL_TradeTerms>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_TradeTerms_GetByName_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_TradeTerms entities = new Entities_PIPL_TradeTerms();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    #endregion

    #region PICKUP LOCATION

    public List<Entities_PIPL_PickupLocation> PIPL_MT_PickupLocation_GetAll()
    {
        List<Entities_PIPL_PickupLocation> list = new List<Entities_PIPL_PickupLocation>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_PickupLocation_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_PickupLocation entities = new Entities_PIPL_PickupLocation();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.AddedBy = reader["AddedBy"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public int PIPL_MT_PickupLocation_IsDisabled(Entities_PIPL_PickupLocation entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_PickupLocation_IsDisabled";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@isdisabled", entity.IsDisabled));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_PickupLocation_Append(Entities_PIPL_PickupLocation entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_PickupLocation_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.UpdatedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_PickupLocation_Insert(Entities_PIPL_PickupLocation entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_PickupLocation_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.AddedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public List<Entities_PIPL_PickupLocation> PIPL_MT_PickupLocation_GetByName(string name)
    {
        List<Entities_PIPL_PickupLocation> list = new List<Entities_PIPL_PickupLocation>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_PickupLocation_GetByName";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_PickupLocation entities = new Entities_PIPL_PickupLocation();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_PIPL_PickupLocation> PIPL_MT_PickupLocation_GetByName_Like(string name)
    {
        List<Entities_PIPL_PickupLocation> list = new List<Entities_PIPL_PickupLocation>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_PickupLocation_GetByName_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_PickupLocation entities = new Entities_PIPL_PickupLocation();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    #endregion

    #region PACKING

    public List<Entities_PIPL_Packing> PIPL_MT_Packing_GetAll()
    {
        List<Entities_PIPL_Packing> list = new List<Entities_PIPL_Packing>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_Packing_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_Packing entities = new Entities_PIPL_Packing();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.AddedBy = reader["AddedBy"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public int PIPL_MT_Packing_IsDisabled(Entities_PIPL_Packing entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_Packing_IsDisabled";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@isdisabled", entity.IsDisabled));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_Packing_Append(Entities_PIPL_Packing entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_Packing_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.UpdatedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_Packing_Insert(Entities_PIPL_Packing entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_Packing_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.AddedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public List<Entities_PIPL_Packing> PIPL_MT_Packing_GetByName(string name)
    {
        List<Entities_PIPL_Packing> list = new List<Entities_PIPL_Packing>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_Packing_GetByName";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_Packing entities = new Entities_PIPL_Packing();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_PIPL_Packing> PIPL_MT_Packing_GetByName_Like(string name)
    {
        List<Entities_PIPL_Packing> list = new List<Entities_PIPL_Packing>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_Packing_GetByName_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_Packing entities = new Entities_PIPL_Packing();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    #endregion

    #region NATURE OF GOODS

    public List<Entities_PIPL_NatureOfGoods> PIPL_MT_NatureOfGoods_GetAll()
    {
        List<Entities_PIPL_NatureOfGoods> list = new List<Entities_PIPL_NatureOfGoods>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_NatureOfGoods_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_NatureOfGoods entities = new Entities_PIPL_NatureOfGoods();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.AddedBy = reader["AddedBy"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public int PIPL_MT_NatureOfGoods_IsDisabled(Entities_PIPL_NatureOfGoods entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_NatureOfGoods_IsDisabled";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@isdisabled", entity.IsDisabled));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_NatureOfGoods_Append(Entities_PIPL_NatureOfGoods entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_NatureOfGoods_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.UpdatedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_NatureOfGoods_Insert(Entities_PIPL_NatureOfGoods entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_NatureOfGoods_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.AddedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public List<Entities_PIPL_NatureOfGoods> PIPL_MT_NatureOfGoods_GetByName(string name)
    {
        List<Entities_PIPL_NatureOfGoods> list = new List<Entities_PIPL_NatureOfGoods>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_NatureOfGoods_GetByName";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_NatureOfGoods entities = new Entities_PIPL_NatureOfGoods();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_PIPL_NatureOfGoods> PIPL_MT_NatureOfGoods_GetByName_Like(string name)
    {
        List<Entities_PIPL_NatureOfGoods> list = new List<Entities_PIPL_NatureOfGoods>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_NatureOfGoods_GetByName_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_NatureOfGoods entities = new Entities_PIPL_NatureOfGoods();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    #endregion

    #region COUNTRY OF ORIGIN

    public List<Entities_PIPL_CountryOfOrigin> PIPL_MT_CountryOfOrigin_GetAll()
    {
        List<Entities_PIPL_CountryOfOrigin> list = new List<Entities_PIPL_CountryOfOrigin>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_CountryOfOrigin_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_CountryOfOrigin entities = new Entities_PIPL_CountryOfOrigin();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.AddedBy = reader["AddedBy"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public int PIPL_MT_CountryOfOrigin_IsDisabled(Entities_PIPL_CountryOfOrigin entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_CountryOfOrigin_IsDisabled";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@isdisabled", entity.IsDisabled));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_CountryOfOrigin_Append(Entities_PIPL_CountryOfOrigin entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_CountryOfOrigin_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.UpdatedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_CountryOfOrigin_Insert(Entities_PIPL_CountryOfOrigin entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_CountryOfOrigin_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.AddedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public List<Entities_PIPL_CountryOfOrigin> PIPL_MT_CountryOfOrigin_GetByName(string name)
    {
        List<Entities_PIPL_CountryOfOrigin> list = new List<Entities_PIPL_CountryOfOrigin>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_CountryOfOrigin_GetByName";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_CountryOfOrigin entities = new Entities_PIPL_CountryOfOrigin();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_PIPL_CountryOfOrigin> PIPL_MT_CountryOfOrigin_GetByName_Like(string name)
    {
        List<Entities_PIPL_CountryOfOrigin> list = new List<Entities_PIPL_CountryOfOrigin>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_CountryOfOrigin_GetByName_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_CountryOfOrigin entities = new Entities_PIPL_CountryOfOrigin();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    #endregion

    #region CASE UNIT

    public List<Entities_PIPL_CaseUnit> PIPL_MT_CaseUnit_GetAll()
    {
        List<Entities_PIPL_CaseUnit> list = new List<Entities_PIPL_CaseUnit>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_CaseUnit_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_CaseUnit entities = new Entities_PIPL_CaseUnit();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.AddedBy = reader["AddedBy"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public int PIPL_MT_CaseUnit_IsDisabled(Entities_PIPL_CaseUnit entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_CaseUnit_IsDisabled";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@isdisabled", entity.IsDisabled));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_CaseUnit_Append(Entities_PIPL_CaseUnit entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_CaseUnit_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.UpdatedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_MT_CaseUnit_Insert(Entities_PIPL_CaseUnit entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_MT_CaseUnit_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.AddedBy));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public List<Entities_PIPL_CaseUnit> PIPL_MT_CaseUnit_GetByName(string name)
    {
        List<Entities_PIPL_CaseUnit> list = new List<Entities_PIPL_CaseUnit>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_CaseUnit_GetByName";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_CaseUnit entities = new Entities_PIPL_CaseUnit();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_PIPL_CaseUnit> PIPL_MT_CaseUnit_GetByName_Like(string name)
    {
        List<Entities_PIPL_CaseUnit> list = new List<Entities_PIPL_CaseUnit>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_MT_CaseUnit_GetByName_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_CaseUnit entities = new Entities_PIPL_CaseUnit();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    #endregion

    #region PIPL TRANSACTION INVOICE ENTRY

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_InvoiceEntry_Fill_All_DropdownList()
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_TRANSACTION_InvoiceEntry_Fill_All_DropdownList";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_InvoiceEntry entities = new Entities_PIPL_InvoiceEntry();

                entities.DropdownRefId = reader["RefId"].ToString();
                entities.DropdownName = reader["Name"].ToString();
                entities.TableName = reader["TableName"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public int PIPL_TRANSACTION_RequestHead_Insert(Entities_PIPL_InvoiceEntry entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_TRANSACTION_RequestHead_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
        cmd.Parameters.Add(Factory.CreateParameter("@invoiceno", entity.InvoiceNo = entity.InvoiceNo.Length <= 0 ? string.Empty : entity.InvoiceNo));
        cmd.Parameters.Add(Factory.CreateParameter("@consignee", entity.Consignee = entity.Consignee.Length <= 0 ? string.Empty : entity.Consignee));
        cmd.Parameters.Add(Factory.CreateParameter("@modeofshipment", entity.ModeOfShipment = entity.ModeOfShipment.Length <= 0 ? string.Empty : entity.ModeOfShipment));
        cmd.Parameters.Add(Factory.CreateParameter("@tradeterms", entity.TradeTerms = entity.TradeTerms.Length <= 0 ? string.Empty : entity.TradeTerms));
        cmd.Parameters.Add(Factory.CreateParameter("@pickuplocation", entity.PickUpLocation = entity.PickUpLocation.Length <= 0 ? string.Empty : entity.PickUpLocation));
        cmd.Parameters.Add(Factory.CreateParameter("@purpose", entity.Purpose = entity.Purpose.Length <= 0 ? string.Empty : entity.Purpose));
        cmd.Parameters.Add(Factory.CreateParameter("@packing", entity.Packing = entity.Packing.Length <= 0 ? string.Empty : entity.Packing));
        cmd.Parameters.Add(Factory.CreateParameter("@natureofgoods", entity.NatureOfGoods = entity.NatureOfGoods.Length <= 0 ? string.Empty : entity.NatureOfGoods));
        cmd.Parameters.Add(Factory.CreateParameter("@countryoforigin", entity.CountryOfOrigin = entity.CountryOfOrigin.Length <= 0 ? string.Empty : entity.CountryOfOrigin));
        cmd.Parameters.Add(Factory.CreateParameter("@category", entity.Category = entity.Category.Length <= 0 ? string.Empty : entity.Category));
        cmd.Parameters.Add(Factory.CreateParameter("@requester", entity.Requester = entity.Requester.Length <= 0 ? string.Empty : entity.Requester));
        cmd.Parameters.Add(Factory.CreateParameter("@portofdestination", entity.PortOfDestination = entity.PortOfDestination.Length <= 0 ? string.Empty : entity.PortOfDestination));
        cmd.Parameters.Add(Factory.CreateParameter("@etd", entity.Etd = entity.Etd.Length <= 0 ? string.Empty : entity.Etd));
        cmd.Parameters.Add(Factory.CreateParameter("@valueinyen", entity.ValueInYen = entity.ValueInYen.Length <= 0 ? string.Empty : entity.ValueInYen));
        cmd.Parameters.Add(Factory.CreateParameter("@valueinusd", entity.ValueInUsd = entity.ValueInUsd.Length <= 0 ? string.Empty : entity.ValueInUsd));
        cmd.Parameters.Add(Factory.CreateParameter("@pono", entity.PoNo = entity.PoNo.Length <= 0 ? string.Empty : entity.PoNo));
        cmd.Parameters.Add(Factory.CreateParameter("@commercialvalue", entity.CommercialValue = entity.CommercialValue.Length <= 0 ? string.Empty : entity.CommercialValue));
        cmd.Parameters.Add(Factory.CreateParameter("@bdn", entity.Bdn = entity.Bdn.Length <= 0 ? string.Empty : entity.Bdn));
        cmd.Parameters.Add(Factory.CreateParameter("@bdnvalue", entity.BdnValue = entity.BdnValue.Length <= 0 ? string.Empty : entity.BdnValue));
        cmd.Parameters.Add(Factory.CreateParameter("@attention1", entity.Attention1 = entity.Attention1.Length <= 0 ? string.Empty : entity.Attention1));
        cmd.Parameters.Add(Factory.CreateParameter("@attention2", entity.Attention2 = entity.Attention2.Length <= 0 ? string.Empty : entity.Attention2));
        cmd.Parameters.Add(Factory.CreateParameter("@secdept1", entity.Secdept1 = entity.Secdept1.Length <= 0 ? string.Empty : entity.Secdept1));
        cmd.Parameters.Add(Factory.CreateParameter("@secdept2", entity.Secdept2 = entity.Secdept2.Length <= 0 ? string.Empty : entity.Secdept2));
        cmd.Parameters.Add(Factory.CreateParameter("@remarks", entity.Remarks = entity.Remarks.Length <= 0 ? string.Empty : entity.Remarks));
        cmd.Parameters.Add(Factory.CreateParameter("@referenceno", entity.ReferenceNo = entity.ReferenceNo.Length <= 0 ? string.Empty : entity.ReferenceNo));
        cmd.Parameters.Add(Factory.CreateParameter("@attachment", entity.Attachment = entity.Attachment.Length <= 0 ? string.Empty : entity.Attachment));
        cmd.Parameters.Add(Factory.CreateParameter("@purposeothers", entity.PurposeOthers = entity.PurposeOthers.Length <= 0 ? string.Empty : entity.PurposeOthers));
        cmd.Parameters.Add(Factory.CreateParameter("@salestype", entity.SalesType = entity.SalesType.Length <= 0 ? string.Empty : entity.SalesType));
        cmd.Parameters.Add(Factory.CreateParameter("@businessunit", entity.BusinessUnit = entity.BusinessUnit.Length <= 0 ? string.Empty : entity.BusinessUnit));
        cmd.Parameters.Add(Factory.CreateParameter("@accountcode", entity.AccountCode = entity.AccountCode.Length <= 0 ? string.Empty : entity.AccountCode));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_TRANSACTION_RequestHead_Append(Entities_PIPL_InvoiceEntry entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_TRANSACTION_RequestHead_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
        cmd.Parameters.Add(Factory.CreateParameter("@invoiceno", entity.InvoiceNo = entity.InvoiceNo.Length <= 0 ? string.Empty : entity.InvoiceNo));
        cmd.Parameters.Add(Factory.CreateParameter("@consignee", entity.Consignee = entity.Consignee.Length <= 0 ? string.Empty : entity.Consignee));
        cmd.Parameters.Add(Factory.CreateParameter("@modeofshipment", entity.ModeOfShipment = entity.ModeOfShipment.Length <= 0 ? string.Empty : entity.ModeOfShipment));
        cmd.Parameters.Add(Factory.CreateParameter("@tradeterms", entity.TradeTerms = entity.TradeTerms.Length <= 0 ? string.Empty : entity.TradeTerms));
        cmd.Parameters.Add(Factory.CreateParameter("@pickuplocation", entity.PickUpLocation = entity.PickUpLocation.Length <= 0 ? string.Empty : entity.PickUpLocation));
        cmd.Parameters.Add(Factory.CreateParameter("@purpose", entity.Purpose = entity.Purpose.Length <= 0 ? string.Empty : entity.Purpose));
        cmd.Parameters.Add(Factory.CreateParameter("@packing", entity.Packing = entity.Packing.Length <= 0 ? string.Empty : entity.Packing));
        cmd.Parameters.Add(Factory.CreateParameter("@natureofgoods", entity.NatureOfGoods = entity.NatureOfGoods.Length <= 0 ? string.Empty : entity.NatureOfGoods));
        cmd.Parameters.Add(Factory.CreateParameter("@countryoforigin", entity.CountryOfOrigin = entity.CountryOfOrigin.Length <= 0 ? string.Empty : entity.CountryOfOrigin));
        cmd.Parameters.Add(Factory.CreateParameter("@category", entity.Category = entity.Category.Length <= 0 ? string.Empty : entity.Category));
        //cmd.Parameters.Add(Factory.CreateParameter("@requester", entity.Requester = entity.Requester.Length <= 0 ? string.Empty : entity.Requester));
        cmd.Parameters.Add(Factory.CreateParameter("@portofdestination", entity.PortOfDestination = entity.PortOfDestination.Length <= 0 ? string.Empty : entity.PortOfDestination));
        cmd.Parameters.Add(Factory.CreateParameter("@etd", entity.Etd = entity.Etd.Length <= 0 ? string.Empty : entity.Etd));
        cmd.Parameters.Add(Factory.CreateParameter("@valueinyen", entity.ValueInYen = entity.ValueInYen.Length <= 0 ? string.Empty : entity.ValueInYen));
        cmd.Parameters.Add(Factory.CreateParameter("@valueinusd", entity.ValueInUsd = entity.ValueInUsd.Length <= 0 ? string.Empty : entity.ValueInUsd));
        cmd.Parameters.Add(Factory.CreateParameter("@pono", entity.PoNo = entity.PoNo.Length <= 0 ? string.Empty : entity.PoNo));
        cmd.Parameters.Add(Factory.CreateParameter("@commercialvalue", entity.CommercialValue = entity.CommercialValue.Length <= 0 ? string.Empty : entity.CommercialValue));
        cmd.Parameters.Add(Factory.CreateParameter("@bdn", entity.Bdn = entity.Bdn.Length <= 0 ? string.Empty : entity.Bdn));
        cmd.Parameters.Add(Factory.CreateParameter("@bdnvalue", entity.BdnValue = entity.BdnValue.Length <= 0 ? string.Empty : entity.BdnValue));
        cmd.Parameters.Add(Factory.CreateParameter("@attention1", entity.Attention1 = entity.Attention1.Length <= 0 ? string.Empty : entity.Attention1));
        cmd.Parameters.Add(Factory.CreateParameter("@attention2", entity.Attention2 = entity.Attention2.Length <= 0 ? string.Empty : entity.Attention2));
        cmd.Parameters.Add(Factory.CreateParameter("@secdept1", entity.Secdept1 = entity.Secdept1.Length <= 0 ? string.Empty : entity.Secdept1));
        cmd.Parameters.Add(Factory.CreateParameter("@secdept2", entity.Secdept2 = entity.Secdept2.Length <= 0 ? string.Empty : entity.Secdept2));
        cmd.Parameters.Add(Factory.CreateParameter("@remarks", entity.Remarks = entity.Remarks.Length <= 0 ? string.Empty : entity.Remarks));
        cmd.Parameters.Add(Factory.CreateParameter("@referenceno", entity.ReferenceNo = entity.ReferenceNo.Length <= 0 ? string.Empty : entity.ReferenceNo));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.UpdatedBy = entity.UpdatedBy.Length <= 0 ? string.Empty : entity.UpdatedBy));
        cmd.Parameters.Add(Factory.CreateParameter("@purposeothers", entity.PurposeOthers = entity.PurposeOthers.Length <= 0 ? string.Empty : entity.PurposeOthers));
        cmd.Parameters.Add(Factory.CreateParameter("@salestype", entity.SalesType = entity.SalesType.Length <= 0 ? string.Empty : entity.SalesType));
        cmd.Parameters.Add(Factory.CreateParameter("@businessunit", entity.BusinessUnit = entity.BusinessUnit.Length <= 0 ? string.Empty : entity.BusinessUnit));
        cmd.Parameters.Add(Factory.CreateParameter("@accountcode", entity.AccountCode = entity.AccountCode.Length <= 0 ? string.Empty : entity.AccountCode));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_TRANSACTION_RequestDetails_Insert(Entities_PIPL_InvoiceEntry entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_TRANSACTION_RequestDetails_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.HeadCTRLNo = entity.HeadCTRLNo.Length <= 0 ? string.Empty : entity.HeadCTRLNo));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description = entity.Description.Length <= 0 ? string.Empty : entity.Description));
        cmd.Parameters.Add(Factory.CreateParameter("@specification", entity.Specification = entity.Specification.Length <= 0 ? string.Empty : entity.Specification));
        cmd.Parameters.Add(Factory.CreateParameter("@quantity", entity.Quantity = entity.Quantity.Length <= 0 ? string.Empty : entity.Quantity));
        cmd.Parameters.Add(Factory.CreateParameter("@uom", entity.Uom = entity.Uom.Length <= 0 ? string.Empty : entity.Uom));
        cmd.Parameters.Add(Factory.CreateParameter("@currency", entity.Currency = entity.Currency.Length <= 0 ? string.Empty : entity.Currency));
        cmd.Parameters.Add(Factory.CreateParameter("@uprice", entity.UPrice = entity.UPrice.Length <= 0 ? string.Empty : entity.UPrice));
        cmd.Parameters.Add(Factory.CreateParameter("@netweight", entity.NetWeight = entity.NetWeight.Length <= 0 ? string.Empty : entity.NetWeight));
        cmd.Parameters.Add(Factory.CreateParameter("@grossweight", entity.GrossWeight = entity.GrossWeight.Length <= 0 ? string.Empty : entity.GrossWeight));
        cmd.Parameters.Add(Factory.CreateParameter("@measurement", entity.Measurement = entity.Measurement.Length <= 0 ? string.Empty : entity.Measurement));
        cmd.Parameters.Add(Factory.CreateParameter("@caseunit", entity.CaseUnit = entity.CaseUnit.Length <= 0 ? string.Empty : entity.CaseUnit));
        cmd.Parameters.Add(Factory.CreateParameter("@casenumber", entity.CaseNumber = entity.CaseNumber.Length <= 0 ? string.Empty : entity.CaseNumber));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_TRANSACTION_RequestDetails_Append(Entities_PIPL_InvoiceEntry entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_TRANSACTION_RequestDetails_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.DetailsRefId = entity.DetailsRefId.Length <= 0 ? string.Empty : entity.DetailsRefId));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description = entity.Description.Length <= 0 ? string.Empty : entity.Description));
        cmd.Parameters.Add(Factory.CreateParameter("@specification", entity.Specification = entity.Specification.Length <= 0 ? string.Empty : entity.Specification));
        cmd.Parameters.Add(Factory.CreateParameter("@quantity", entity.Quantity = entity.Quantity.Length <= 0 ? string.Empty : entity.Quantity));
        cmd.Parameters.Add(Factory.CreateParameter("@uom", entity.Uom = entity.Uom.Length <= 0 ? string.Empty : entity.Uom));
        cmd.Parameters.Add(Factory.CreateParameter("@currency", entity.Currency = entity.Currency.Length <= 0 ? string.Empty : entity.Currency));
        cmd.Parameters.Add(Factory.CreateParameter("@uprice", entity.UPrice = entity.UPrice.Length <= 0 ? string.Empty : entity.UPrice));
        cmd.Parameters.Add(Factory.CreateParameter("@netweight", entity.NetWeight = entity.NetWeight.Length <= 0 ? string.Empty : entity.NetWeight));
        cmd.Parameters.Add(Factory.CreateParameter("@grossweight", entity.GrossWeight = entity.GrossWeight.Length <= 0 ? string.Empty : entity.GrossWeight));
        cmd.Parameters.Add(Factory.CreateParameter("@measurement", entity.Measurement = entity.Measurement.Length <= 0 ? string.Empty : entity.Measurement));
        cmd.Parameters.Add(Factory.CreateParameter("@caseunit", entity.CaseUnit = entity.CaseUnit.Length <= 0 ? string.Empty : entity.CaseUnit));
        cmd.Parameters.Add(Factory.CreateParameter("@casenumber", entity.CaseNumber = entity.CaseNumber.Length <= 0 ? string.Empty : entity.CaseNumber));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_TRANSACTION_RequestStatus_Insert(Entities_PIPL_InvoiceEntry entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_TRANSACTION_RequestStatus_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public Int32 PIPL_TRANSACTION_RequestHead_Count(string year)
    {
        Int32 result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_TRANSACTION_RequestHead_Count";

        cmd.Parameters.Add(Factory.CreateParameter("@year", year));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = (Int32)cmd.ExecuteScalar();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_RequestStatus_ByDateRange(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_TRANSACTION_RequestStatus_ByDateRange";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.Parameters.Add(Factory.CreateParameter("@requester", entity.Requester = entity.Requester.Length <= 0 ? string.Empty : entity.Requester));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_InvoiceEntry entities = new Entities_PIPL_InvoiceEntry();

                entities.RefIdHead = reader["RefId"].ToString();
                entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.Manager = reader["Manager"].ToString();
                entities.DoaManager = reader["DoaManager"].ToString();
                entities.PcManager = reader["PcManager"].ToString();
                entities.DoaPCManager = reader["DoaPCManager"].ToString();
                entities.Impex = reader["Impex"].ToString();
                entities.DoaImpex = reader["DoaImpex"].ToString();
                entities.StatManager = reader["StatManager"].ToString();
                entities.StatPCManager = reader["StatPCManager"].ToString();
                entities.StatIncharge = reader["StatIncharge"].ToString();
                entities.StatImpex = reader["StatImpex"].ToString();
                entities.StatRemarks = reader["StatRemarks"].ToString();
                entities.StatAccounting = reader["StatAccounting"].ToString();
                entities.Accounting = reader["Accounting"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_RequestStatus_ByDateRange_All(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_TRANSACTION_RequestStatus_ByDateRange_All";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.Parameters.Add(Factory.CreateParameter("@requester", entity.Requester = entity.Requester.Length <= 0 ? string.Empty : entity.Requester));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_InvoiceEntry entities = new Entities_PIPL_InvoiceEntry();

                entities.RefIdHead = reader["RefId"].ToString();
                entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.Manager = reader["Manager"].ToString();
                entities.DoaManager = reader["DoaManager"].ToString();
                entities.PcManager = reader["PcManager"].ToString();
                entities.DoaPCManager = reader["DoaPCManager"].ToString();
                entities.Impex = reader["Impex"].ToString();
                entities.DoaImpex = reader["DoaImpex"].ToString();
                entities.StatManager = reader["StatManager"].ToString();
                entities.StatPCManager = reader["StatPCManager"].ToString();
                entities.StatIncharge = reader["StatIncharge"].ToString();
                entities.StatImpex = reader["StatImpex"].ToString();
                entities.StatRemarks = reader["StatRemarks"].ToString();
                entities.Accounting = reader["Accounting"].ToString();
                entities.StatAccounting = reader["StatAccounting"].ToString();
                entities.Consignee = reader["SupplierName"].ToString().ToUpper();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_RequestStatus_ByDateRange_All_Reporting(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_TRANSACTION_RequestStatus_ByDateRange_All_Reporting";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.Parameters.Add(Factory.CreateParameter("@requester", entity.Requester = entity.Requester.Length <= 0 ? string.Empty : entity.Requester));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_InvoiceEntry entities = new Entities_PIPL_InvoiceEntry();

                entities.RefIdHead = reader["RefId"].ToString();
                entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.Manager = reader["Manager"].ToString();
                entities.DoaManager = reader["DoaManager"].ToString();
                entities.PcManager = reader["PcManager"].ToString();
                entities.DoaPCManager = reader["DoaPCManager"].ToString();
                entities.Impex = reader["Impex"].ToString();
                entities.DoaImpex = reader["DoaImpex"].ToString();
                entities.StatManager = reader["StatManager"].ToString();
                entities.StatPCManager = reader["StatPCManager"].ToString();
                entities.StatIncharge = reader["StatIncharge"].ToString();
                entities.StatImpex = reader["StatImpex"].ToString();
                entities.StatRemarks = reader["StatRemarks"].ToString();
                entities.Accounting = reader["Accounting"].ToString();
                entities.StatAccounting = reader["StatAccounting"].ToString();
                entities.Consignee = reader["SupplierName"].ToString().ToUpper();

                entities.Description = reader["Description"].ToString().ToUpper();
                entities.Specification = reader["Specification"].ToString().ToUpper();
                entities.Quantity = reader["Quantity"].ToString().ToUpper();
                entities.Currency = reader["CurrencyCode"].ToString().ToUpper();
                entities.UPrice = reader["UPrice"].ToString().ToUpper();
                entities.NetWeight = reader["NetWeight"].ToString().ToUpper();
                entities.GrossWeight = reader["GrossWeight"].ToString().ToUpper();
                entities.Measurement = reader["Measurement"].ToString().ToUpper();
                entities.CaseUnit = reader["CaseUnit"].ToString().ToUpper();
                entities.Uom = reader["UnitOfMeasure"].ToString().ToUpper();
                entities.CaseNumber = reader["CaseNumber"].ToString().ToUpper();

                entities.Attention1 = reader["Attention1"].ToString().ToUpper();
                entities.Purpose = reader["Purpose"].ToString().ToUpper();
                entities.Packing = reader["Packing"].ToString().ToUpper();
                entities.NatureOfGoods = reader["NatureOfGoods"].ToString().ToUpper();
                entities.Category = reader["Category"].ToString().ToUpper();
                entities.Report_BuyerName = reader["BuyerName"] != null ? CryptorEngine.Decrypt(reader["BuyerName"].ToString().Replace(" ", "+"), true) : string.Empty;
                //CryptorEngine.Decrypt(reader["Requester"].ToString().Replace(" ", "+"), true);


                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_Reporting_ByDateRange(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_TRANSACTION_Reporting_ByDateRange";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.CommandTimeout = 500;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_InvoiceEntry entities = new Entities_PIPL_InvoiceEntry();

                entities.Report_Department_Name = reader["Department_Name"].ToString().ToUpper();
                entities.Report_Department_Total_Request = reader["Total_Request_Counts_BY_DEPARTMENT"].ToString();
                entities.Report_Department_Buyer_Approved = reader["Buyer_Approved_Counts_BY_DEPARTMENT"].ToString();
                entities.Report_Department_Buyer_Disapproved = reader["Buyer_Disapproved_Counts_BY_DEPARTMENT"].ToString();
                entities.Report_Department_Impex_Approved = reader["Impex_Approved_Counts_BY_DEPARTMENT"].ToString();
                entities.Report_Department_Impex_Disapproved = reader["Impex_Disapproved_Counts_BY_DEPARTMENT"].ToString();
                entities.Report_Department_Pending_Approval = reader["Pending_Approval_Counts_BY_DEPARTMENT"].ToString();                


                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_Reporting_ByDateRange_ByDivision(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_TRANSACTION_Reporting_ByDateRange_ByDivision";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.CommandTimeout = 500;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_InvoiceEntry entities = new Entities_PIPL_InvoiceEntry();

                entities.Report_Division_Name = reader["Division_Name"].ToString().ToUpper();
                entities.Report_Division_Total_Request = reader["Total_Request_Counts_BY_DIVISION"].ToString();
                entities.Report_Division_Buyer_Approved = reader["Buyer_Approved_Counts_BY_DIVISION"].ToString();
                entities.Report_Division_Buyer_Disapproved = reader["Buyer_Disapproved_Counts_BY_DIVISION"].ToString();
                entities.Report_Division_Impex_Approved = reader["Impex_Approved_Counts_BY_DIVISION"].ToString();
                entities.Report_Division_Impex_Disapproved = reader["Impex_Disapproved_Counts_BY_DIVISION"].ToString();
                entities.Report_Division_Pending_Approval = reader["Pending_Approval_Counts_BY_DIVISION"].ToString();
                


                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_Reporting_ByDateRange_ByAll(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_TRANSACTION_Reporting_ByDateRange_ByAll";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.CommandTimeout = 500;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_InvoiceEntry entities = new Entities_PIPL_InvoiceEntry();

                entities.Report_All_Total_Request = reader["Total_Request_Counts_BY_ALL"].ToString();
                entities.Report_All_Buyer_Approved = reader["Buyer_Approved_Counts_BY_ALL"].ToString();
                entities.Report_All_Buyer_Disapproved = reader["Buyer_Disapproved_Counts_BY_ALL"].ToString();
                entities.Report_All_Impex_Approved = reader["Impex_Approved_Counts_BY_ALL"].ToString();
                entities.Report_All_Impex_Disapproved = reader["Impex_Disapproved_Counts_BY_ALL"].ToString();
                entities.Report_All_Pending_Approval = reader["Pending_Approval_Counts_BY_ALL"].ToString();
                entities.Report_All_Total_Approval = reader["Total_Approved_Counts_BY_ALL"].ToString();
                entities.Report_All_Total_Disapproval = reader["Total_Disapproved_Counts_BY_ALL"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_Reporting_ByDateRange_Details(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT HEAD.CTRLNo, HEAD.TransactionDate, CATEGORY.Description AS Category, DETAILS.Description, CASEUNIT.Name AS CaseUnit, DETAILS.CaseNumber, " +
            "UNITOFMEASURE.UOMCode AS UnitQuantity, DETAILS.Specification, HEAD.PONo, CURRENCY.Description AS Currency, " +
            "DETAILS.Quantity, DETAILS.UPrice AS UnitPrice, " +
            "CASE WHEN HEAD.ValueInUSD IS NOT NULL OR HEAD.ValueInUSD > 0 THEN HEAD.ValueInUSD ELSE HEAD.ValueInYen END AS Amount, " +
            "HEAD.CommercialValue AS CommClass, HEAD.ReferenceNo, HEAD.InvoiceNo, HEAD.BDN, HEAD.BDNValue, " +
            "HEAD.Attention2 AS BDNAttention, MOS.Name AS ModeOfShipment, ORIGIN.Name AS Origin, HEAD.ETD AS ETDManila, TRADE.Name AS TradeTerms, " +
            "PURPOSE.Name AS Purpose, CONSIGNEE.Name AS Consignee, HEAD.SecDept1 ConssigneeDeptSec, LC.FullName AS Requester, DEPARTMENT.Description AS Department, " +
            "DETAILS.NetWeight, DETAILS.GrossWeight, DETAILS.Measurement AS Dimension, HEAD.SalesType, HEAD.BusinessUnit, HEAD.AccountCode, " +
            "STAT.STATPCManager, STAT.STATIncharge, STAT.STATImpex " +
            "FROM PIPL_TRANSACTION_RequestHead HEAD " +
            "LEFT JOIN PIPL_TRANSACTION_RequestDetails DETAILS ON HEAD.CTRLNo = DETAILS.CTRLNo " +
            "LEFT JOIN PIPL_MT_CaseUnit CASEUNIT ON DETAILS.CaseUnit = CASEUNIT.RefId " +
            "LEFT JOIN MT_Currency CURRENCY ON DETAILS.Currency = CURRENCY.RefId " +
            "LEFT JOIN MT_UnitOfMeasure UNITOFMEASURE ON DETAILS.UOM = UNITOFMEASURE.RefId " +
            "LEFT JOIN PIPL_MT_ModeOfShipment MOS ON HEAD.ModeOfShipment = MOS.RefId " +
            "LEFT JOIN PIPL_MT_CountryOfOrigin ORIGIN ON HEAD.CountryOfOrigin = ORIGIN.RefId " +
            "LEFT JOIN PIPL_MT_TradeTerms TRADE ON HEAD.TradeTerms = TRADE.RefId " +
            "LEFT JOIN PIPL_MT_Purpose PURPOSE ON HEAD.Purpose = PURPOSE.RefId " +
            "LEFT JOIN PIPL_MT_Company CONSIGNEE ON HEAD.Consignee = CONSIGNEE.RefId  " +
            "LEFT JOIN Login_Credentials LC ON HEAD.Requester = LC.RefId " +
            "LEFT JOIN MT_Department DEPARTMENT ON LC.Department = DEPARTMENT.RefId " +
            "LEFT JOIN MT_Category CATEGORY ON HEAD.Category = CATEGORY.RefId  " +
            "LEFT JOIN PIPL_TRANSACTION_RequestStatus STAT ON HEAD.CTRLNo = STAT.CTRLNo " +
            "WHERE CONVERT(DATE, HEAD.TransactionDate) >= '" + entity.DrFrom + "' AND CONVERT(DATE, HEAD.TransactionDate) <= '" + entity.DrTo + "' " +
            "ORDER BY HEAD.RefId DESC";
            cmd.CommandTimeout = 500;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_InvoiceEntry entities = new Entities_PIPL_InvoiceEntry();

                entities.CtrlNo = reader["ctrlNo"] != DBNull.Value ? reader["ctrlNo"].ToString().ToUpper() : string.Empty;
                entities.Description = reader["description"] != DBNull.Value ? reader["description"].ToString().ToUpper() : string.Empty;
                entities.CaseUnit = reader["caseUnit"] != DBNull.Value ? reader["caseUnit"].ToString().ToUpper() : string.Empty;
                entities.CaseNumber = reader["caseNumber"] != DBNull.Value ? reader["caseNumber"].ToString().ToUpper() : string.Empty;
                entities.Uom = reader["unitQuantity"] != DBNull.Value ? reader["unitQuantity"].ToString().ToUpper() : string.Empty;
                entities.Specification = reader["specification"] != DBNull.Value ? reader["specification"].ToString().ToUpper() : string.Empty;
                entities.PoNo = reader["pono"] != DBNull.Value ? reader["pono"].ToString().ToUpper() : string.Empty;
                entities.Currency = reader["currency"] != DBNull.Value ? reader["currency"].ToString().ToUpper() : string.Empty;
                entities.Quantity = reader["quantity"] != DBNull.Value ? reader["quantity"].ToString().ToUpper() : string.Empty;
                entities.UPrice = reader["unitPrice"] != DBNull.Value ? reader["unitPrice"].ToString().ToUpper() : string.Empty;
                entities.ValueInUsd = reader["amount"] != DBNull.Value ? reader["amount"].ToString().ToUpper() : string.Empty;
                entities.CommercialValue = reader["commClass"] != DBNull.Value ? reader["commClass"].ToString().ToUpper() : string.Empty;
                entities.ReferenceNo = reader["referenceNo"] != DBNull.Value ? reader["referenceNo"].ToString().ToUpper() : string.Empty;
                entities.InvoiceNo = reader["invoiceNo"] != DBNull.Value ? reader["invoiceNo"].ToString().ToUpper() : string.Empty;
                entities.Bdn = reader["bdn"] != DBNull.Value ? reader["bdn"].ToString().ToUpper() : string.Empty;
                entities.BdnValue = reader["bdnValue"] != DBNull.Value ? reader["bdnValue"].ToString().ToUpper() : string.Empty;
                entities.Attention2 = reader["bdnAttention"] != DBNull.Value ? reader["bdnAttention"].ToString().ToUpper() : string.Empty;
                entities.ModeOfShipment = reader["modeOfShipment"] != DBNull.Value ? reader["modeOfShipment"].ToString().ToUpper() : string.Empty;
                //entities. = reader["origin"] != DBNull.Value ? reader["origin"].ToString().ToUpper() : string.Empty;
                entities.Etd = reader["etdManila"] != DBNull.Value ? reader["etdManila"].ToString().ToUpper() : string.Empty;
                entities.TradeTerms = reader["tradeTerms"] != DBNull.Value ? reader["tradeTerms"].ToString().ToUpper() : string.Empty;
                entities.Purpose = reader["purpose"] != DBNull.Value ? reader["purpose"].ToString().ToUpper() : string.Empty;
                entities.Consignee = reader["consignee"] != DBNull.Value ? reader["consignee"].ToString().ToUpper() : string.Empty;
                entities.Secdept1 = reader["conssigneeDeptSec"] != DBNull.Value ? reader["conssigneeDeptSec"].ToString().ToUpper() : string.Empty;
                entities.Requester = CryptorEngine.Decrypt(reader["Requester"].ToString().Replace(" ", "+"), true);
                entities.Department = reader["department"] != DBNull.Value ? reader["department"].ToString().ToUpper() : string.Empty;
                entities.NetWeight = reader["netWeight"] != DBNull.Value ? reader["netWeight"].ToString().ToUpper() : string.Empty;
                entities.GrossWeight = reader["GrossWeight"] != DBNull.Value ? reader["GrossWeight"].ToString().ToUpper() : string.Empty;
                entities.Measurement = reader["dimension"] != DBNull.Value ? reader["dimension"].ToString().ToUpper() : string.Empty;
                entities.SalesType = reader["salesType"] != DBNull.Value ? reader["salesType"].ToString().ToUpper() : string.Empty;
                entities.BusinessUnit = reader["businessUnit"] != DBNull.Value ? reader["businessUnit"].ToString().ToUpper() : string.Empty;
                entities.AccountCode = reader["accountCode"] != DBNull.Value ? reader["accountCode"].ToString().ToUpper() : string.Empty;
                entities.TransactionDate = reader["TransactionDate"] != DBNull.Value ? reader["TransactionDate"].ToString().ToUpper() : string.Empty;
                entities.Category = reader["category"] != DBNull.Value ? reader["category"].ToString().ToUpper() : string.Empty;

                if (reader["STATImpex"].ToString() == "1")
                {
                    entities.StatImpex = "APPROVED";
                }

                if (reader["STATImpex"] == DBNull.Value || reader["STATImpex"].ToString() == "0")
                {
                    entities.StatImpex = "PENDING";
                }

                if (reader["STATIncharge"].ToString() == "2" || reader["STATImpex"].ToString() == "2")
                {
                    entities.StatImpex = "DISAPPROVED";
                }
                

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }


    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.Parameters.Add(Factory.CreateParameter("@requester", entity.Requester = entity.Requester.Length <= 0 ? string.Empty : entity.Requester));
            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_InvoiceEntry entities = new Entities_PIPL_InvoiceEntry();

                entities.RefIdHead = reader["RefId"].ToString();
                entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.Manager = reader["Manager"].ToString();
                entities.DoaManager = reader["DoaManager"].ToString();
                entities.PcManager = reader["PcManager"].ToString();
                entities.DoaPCManager = reader["DoaPCManager"].ToString();
                entities.Impex = reader["Impex"].ToString();
                entities.DoaImpex = reader["DoaImpex"].ToString();
                entities.StatManager = reader["StatManager"].ToString();
                entities.StatPCManager = reader["StatPCManager"].ToString();
                entities.StatIncharge = reader["StatIncharge"].ToString();
                entities.StatImpex = reader["StatImpex"].ToString();
                entities.StatRemarks = reader["StatRemarks"].ToString();

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like_All(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like_All";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.Parameters.Add(Factory.CreateParameter("@requester", entity.Requester = entity.Requester.Length <= 0 ? string.Empty : entity.Requester));
            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_InvoiceEntry entities = new Entities_PIPL_InvoiceEntry();

                entities.RefIdHead = reader["RefId"].ToString();
                entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.Manager = reader["Manager"].ToString();
                entities.DoaManager = reader["DoaManager"].ToString();
                entities.PcManager = reader["PcManager"].ToString();
                entities.DoaPCManager = reader["DoaPCManager"].ToString();
                entities.Impex = reader["Impex"].ToString();
                entities.DoaImpex = reader["DoaImpex"].ToString();
                entities.StatManager = reader["StatManager"].ToString();
                entities.StatPCManager = reader["StatPCManager"].ToString();
                entities.StatIncharge = reader["StatIncharge"].ToString();
                entities.StatImpex = reader["StatImpex"].ToString();
                entities.StatRemarks = reader["StatRemarks"].ToString();
                entities.Accounting = reader["Accounting"].ToString();
                entities.StatAccounting = reader["StatAccounting"].ToString();
                entities.Consignee = reader["SupplierName"].ToString().ToUpper();
                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_RequestDetails_GetByControlNo(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_TRANSACTION_RequestDetails_GetByControlNo";
            cmd.Parameters.Add(Factory.CreateParameter("@controlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_InvoiceEntry entities = new Entities_PIPL_InvoiceEntry();

                entities.CtrlNo = reader["CtrlNo"].ToString();
                entities.InvoiceNo = reader["InvoiceNo"].ToString();
                entities.ReferenceNo = reader["ReferenceNo"].ToString();
                entities.Consignee = reader["CompanyConsignee"].ToString();
                entities.ModeOfShipment = reader["ModeOfShipment"].ToString();
                entities.TradeTerms = reader["TradeTerms"].ToString();
                entities.PickUpLocation = reader["PickUpLocation"].ToString();
                entities.Purpose = reader["Purpose"].ToString();
                entities.Packing = reader["Packing"].ToString();
                entities.NatureOfGoods = reader["NatureOfGoods"].ToString();
                entities.CountryOfOrigin = reader["CountryOfOrigin"].ToString();
                entities.Requester = reader["Requester"].ToString().Length > 0 ? CryptorEngine.Decrypt(reader["Requester"].ToString(), true) : string.Empty;
                entities.PortOfDestination = reader["PortOfDestination"].ToString();
                entities.Etd = reader["ETD"].ToString();
                entities.ValueInYen = reader["ValueInYen"].ToString();
                entities.ValueInUsd = reader["ValueInUSD"].ToString();
                entities.PoNo = reader["PONo"].ToString();
                entities.CommercialValue = reader["CommercialValue"].ToString();
                entities.Bdn = reader["BDN"].ToString();
                entities.BdnValue = reader["BDNValue"].ToString();
                entities.Attention1 = reader["Attention1"].ToString();
                entities.Attention2 = reader["Attention2"].ToString();
                entities.Secdept1 = reader["SecDept1"].ToString();
                entities.Secdept2 = reader["SecDept2"].ToString();
                entities.Category = reader["Category"].ToString();
                if (reader["Attachment"] == DBNull.Value)
                {
                    entities.Attachment = "0";
                }
                else
                {
                    entities.Attachment = reader["Attachment"].ToString();
                }
                entities.Remarks = reader["Remarks"].ToString();
                entities.StatRemarks = reader["StatRemarks"].ToString();
                entities.TransactionDate = reader["TransactionDate"].ToString();
                entities.DetailsRefId = reader["RefId"].ToString();
                entities.Description = reader["Description"].ToString();
                entities.Specification = reader["Specification"].ToString();
                entities.Quantity = reader["Quantity"].ToString();
                entities.Uom = reader["UomName"].ToString();
                entities.Currency = reader["CurrencyName"].ToString();
                entities.UPrice = reader["UPrice"].ToString();
                entities.NetWeight = reader["NetWeight"].ToString();
                entities.GrossWeight = reader["GrossWeight"].ToString();
                entities.Measurement = reader["Measurement"].ToString();
                entities.AuditTrailDate = reader["AuditTrailDate"].ToString();

                entities.Manager = reader["ManagerName"].ToString().Length > 0 ? CryptorEngine.Decrypt(reader["ManagerName"].ToString(), true) : string.Empty;
                entities.DoaManager = reader["DoaManager"].ToString();
                entities.PcManager = reader["PcManagerName"].ToString().Length > 0 ? CryptorEngine.Decrypt(reader["PcManagerName"].ToString(), true) : string.Empty;
                entities.DoaPCManager = reader["DoaPCManager"].ToString();
                entities.Incharge = reader["InchargeName"].ToString().Length > 0 ? CryptorEngine.Decrypt(reader["InchargeName"].ToString(), true) : string.Empty;
                entities.DoaIncharge = reader["DoaIncharge"].ToString();
                entities.Impex = reader["ImpexName"].ToString().Length > 0 ? CryptorEngine.Decrypt(reader["ImpexName"].ToString(), true) : string.Empty;
                entities.DoaImpex = reader["DoaImpex"].ToString();
                entities.Accounting = reader["Accounting"].ToString().Length > 0 ? CryptorEngine.Decrypt(reader["AccountingName"].ToString(), true) : string.Empty;
                entities.DoaAccounting = reader["DoaAccounting"].ToString();

                entities.StatManager = reader["StatManager"].ToString();
                entities.StatPCManager = reader["StatPCManager"].ToString();
                entities.StatIncharge = reader["StatIncharge"].ToString();
                entities.StatImpex = reader["StatImpex"].ToString();
                entities.StatAccounting = reader["StatAccounting"].ToString();

                entities.CaseUnit = reader["CaseUnitName"].ToString();
                entities.CaseNumber = reader["CaseNumber"].ToString();

                if (reader["PurposeOthers"] == DBNull.Value)
                {
                    entities.PurposeOthers = string.Empty;
                }
                else
                {
                    entities.PurposeOthers = reader["PurposeOthers"].ToString();
                }

                entities.SectionName = reader["SectionName"].ToString();
                entities.DepartmentName = reader["DepartmentName"].ToString();
                entities.DivisionName = reader["DivisionName"].ToString();
                entities.PcName = reader["PcName"].ToString();
                entities.HqName = reader["HqName"].ToString();

                entities.SalesType = reader["SalesType"].ToString();
                entities.BusinessUnit = reader["BusinessUnit"].ToString();
                entities.AccountCode = reader["AccountCode"].ToString();

                if (reader["RequesterLocalNumber"] == DBNull.Value)
                {
                    entities.RequesterLocalNumber = string.Empty;
                }
                else
                {
                    entities.RequesterLocalNumber = reader["RequesterLocalNumber"].ToString();
                }

                if (reader["RequesterEmailAddress"] == DBNull.Value)
                {
                    entities.RequesterEmailAddress = string.Empty;
                }
                else
                {
                    entities.RequesterEmailAddress = reader["RequesterEmailAddress"].ToString();
                }

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }


    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_ForApproval(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_TRANSACTION_ForApproval";
            //cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            //cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_InvoiceEntry entities = new Entities_PIPL_InvoiceEntry();

                entities.RefIdHead = reader["RefId"].ToString();
                entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.Manager = reader["Manager"].ToString();
                entities.DoaManager = reader["DoaManager"].ToString();
                entities.PcManager = reader["PcManager"].ToString();
                entities.DoaPCManager = reader["DoaPCManager"].ToString();
                entities.Impex = reader["Impex"].ToString();
                entities.DoaImpex = reader["DoaImpex"].ToString();
                entities.StatManager = reader["StatManager"].ToString();
                entities.StatPCManager = reader["StatPCManager"].ToString();
                entities.StatIncharge = reader["StatIncharge"].ToString();
                entities.StatImpex = reader["StatImpex"].ToString();
                entities.Remarks = reader["Remarks"].ToString();
                entities.Requester = reader["Requester"].ToString();
                entities.Department = reader["Department"].ToString();
                entities.Category = reader["Category"].ToString();
                entities.Division = reader["Division"].ToString();
                entities.Pc = reader["PC"].ToString();
                entities.Hq = reader["HQ"].ToString();
                entities.Accounting = reader["Accounting"].ToString();
                entities.DoaAccounting = reader["DoaAccounting"].ToString();
                entities.StatAccounting = reader["StatAccounting"].ToString();
                entities.CommercialValue = reader["CommercialValue"].ToString();
                

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public int PIPL_TRANSACTION_ApprovedManager(Entities_PIPL_InvoiceEntry entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_TRANSACTION_ApprovedManager";

        cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
        cmd.Parameters.Add(Factory.CreateParameter("@approver", entity.Manager = entity.Manager.Length <= 0 ? string.Empty : entity.Manager));
        cmd.Parameters.Add(Factory.CreateParameter("@status", entity.StatManager = entity.StatManager.Length <= 0 ? string.Empty : entity.StatManager));
        cmd.Parameters.Add(Factory.CreateParameter("@remarks", entity.Remarks = entity.Remarks.Length <= 0 ? string.Empty : entity.Remarks));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_TRANSACTION_ApprovedPCManager(Entities_PIPL_InvoiceEntry entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_TRANSACTION_ApprovedPCManager";

        cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
        cmd.Parameters.Add(Factory.CreateParameter("@approver", entity.PcManager = entity.PcManager.Length <= 0 ? string.Empty : entity.PcManager));
        cmd.Parameters.Add(Factory.CreateParameter("@status", entity.StatPCManager = entity.StatPCManager.Length <= 0 ? string.Empty : entity.StatPCManager));
        cmd.Parameters.Add(Factory.CreateParameter("@remarks", entity.Remarks = entity.Remarks.Length <= 0 ? string.Empty : entity.Remarks));
        cmd.Parameters.Add(Factory.CreateParameter("@withCommercial", entity.CommercialValue = entity.CommercialValue.Length <= 0 ? string.Empty : entity.CommercialValue));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_TRANSACTION_ApprovedPCManager2(Entities_PIPL_InvoiceEntry entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_TRANSACTION_ApprovedPCManager2";

        cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
        cmd.Parameters.Add(Factory.CreateParameter("@approver", entity.PcManager = entity.PcManager.Length <= 0 ? string.Empty : entity.PcManager));
        cmd.Parameters.Add(Factory.CreateParameter("@status", entity.StatPCManager = entity.StatPCManager.Length <= 0 ? string.Empty : entity.StatPCManager));
        cmd.Parameters.Add(Factory.CreateParameter("@remarks", entity.Remarks = entity.Remarks.Length <= 0 ? string.Empty : entity.Remarks));
        cmd.Parameters.Add(Factory.CreateParameter("@withCommercial", entity.CommercialValue = entity.CommercialValue.Length <= 0 ? string.Empty : entity.CommercialValue));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_TRANSACTION_ApprovedAccounting(Entities_PIPL_InvoiceEntry entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_TRANSACTION_ApprovedAccounting";

        cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
        cmd.Parameters.Add(Factory.CreateParameter("@approver", entity.Accounting = entity.Accounting.Length <= 0 ? string.Empty : entity.Accounting));
        cmd.Parameters.Add(Factory.CreateParameter("@status", entity.StatAccounting = entity.StatAccounting.Length <= 0 ? string.Empty : entity.StatAccounting));
        cmd.Parameters.Add(Factory.CreateParameter("@remarks", entity.Remarks = entity.Remarks.Length <= 0 ? string.Empty : entity.Remarks));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_TRANSACTION_ApprovedAccounting_AUTOAPPROVED(Entities_PIPL_InvoiceEntry entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_TRANSACTION_ApprovedAccounting_AUTOAPPROVED";

        cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
        cmd.Parameters.Add(Factory.CreateParameter("@approver", entity.Accounting = entity.Accounting.Length <= 0 ? string.Empty : entity.Accounting));
        cmd.Parameters.Add(Factory.CreateParameter("@status", entity.StatAccounting = entity.StatAccounting.Length <= 0 ? string.Empty : entity.StatAccounting));
        cmd.Parameters.Add(Factory.CreateParameter("@remarks", entity.Remarks = entity.Remarks.Length <= 0 ? string.Empty : entity.Remarks));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_TRANSACTION_ApprovedIncharge(Entities_PIPL_InvoiceEntry entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_TRANSACTION_ApprovedIncharge";

        cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
        cmd.Parameters.Add(Factory.CreateParameter("@approver", entity.Incharge = entity.Incharge.Length <= 0 ? string.Empty : entity.Incharge));
        cmd.Parameters.Add(Factory.CreateParameter("@status", entity.StatIncharge = entity.StatIncharge.Length <= 0 ? string.Empty : entity.StatIncharge));
        cmd.Parameters.Add(Factory.CreateParameter("@remarks", entity.Remarks = entity.Remarks.Length <= 0 ? string.Empty : entity.Remarks));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }

    public int PIPL_TRANSACTION_ApprovedImpex(Entities_PIPL_InvoiceEntry entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PIPL_TRANSACTION_ApprovedImpex";

        cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
        cmd.Parameters.Add(Factory.CreateParameter("@approver", entity.Impex = entity.Impex.Length <= 0 ? string.Empty : entity.Impex));
        cmd.Parameters.Add(Factory.CreateParameter("@status", entity.StatImpex = entity.StatImpex.Length <= 0 ? string.Empty : entity.StatImpex));
        cmd.Parameters.Add(Factory.CreateParameter("@remarks", entity.Remarks = entity.Remarks.Length <= 0 ? string.Empty : entity.Remarks));

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }


    public List<Entities_PIPL_InvoiceEntry> PIPL_TRANSACTION_GetRequesterEmailAndLocalNumber_ByCTRLNo(Entities_PIPL_InvoiceEntry entity)
    {
        List<Entities_PIPL_InvoiceEntry> list = new List<Entities_PIPL_InvoiceEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PIPL_TRANSACTION_GetRequesterEmailAndLocalNumber_ByCTRLNo";
            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_PIPL_InvoiceEntry entities = new Entities_PIPL_InvoiceEntry();

                entities.CtrlNo = reader["CTRLNo"].ToString().ToUpper();
                entities.RequesterEmailAddress = reader["EmailAddress"] != DBNull.Value ? reader["EmailAddress"].ToString().ToUpper() : string.Empty;
                entities.RequesterLocalNumber = reader["LocalNumber"] != DBNull.Value ? reader["LocalNumber"].ToString() : string.Empty;

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }



    public int PIPL_TRANSACTION_SQLTransaction(string query)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.Text;
        cmd.CommandText = query;

        try
        {
            conn.Open();
            cmd.Connection = conn;

            result = cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        return result;

    }





    #endregion



}
