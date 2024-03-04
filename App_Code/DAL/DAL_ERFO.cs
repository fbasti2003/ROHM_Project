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

public class DAL_ERFO
{
    public DAL_ERFO()
    {
    }

    #region PURPOSE OF OPERATION

    public List<Entities_ERFO_PurposeOfOperation> ERFO_MT_PurposeOfOperation_GetAll()
    {
        List<Entities_ERFO_PurposeOfOperation> list = new List<Entities_ERFO_PurposeOfOperation>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ERFO_MT_PurposeOfOperation_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_ERFO_PurposeOfOperation entities = new Entities_ERFO_PurposeOfOperation();

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

    public int ERFO_MT_PurposeOfOperation_IsDisabled(Entities_ERFO_PurposeOfOperation entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ERFO_MT_PurposeOfOperation_IsDisabled";

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

    public int ERFO_MT_PurposeOfOperation_Append(Entities_ERFO_PurposeOfOperation entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ERFO_MT_PurposeOfOperation_Append";

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

    public int ERFO_MT_PurposeOfOperation_Insert(Entities_ERFO_PurposeOfOperation entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ERFO_MT_PurposeOfOperation_Insert";

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

    public List<Entities_ERFO_PurposeOfOperation> ERFO_MT_PurposeOfOperation_GetByName(string name)
    {
        List<Entities_ERFO_PurposeOfOperation> list = new List<Entities_ERFO_PurposeOfOperation>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ERFO_MT_PurposeOfOperation_GetByName";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_ERFO_PurposeOfOperation entities = new Entities_ERFO_PurposeOfOperation();

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

    public List<Entities_ERFO_PurposeOfOperation> ERFO_MT_PurposeOfOperation_GetByName_Like(string name)
    {
        List<Entities_ERFO_PurposeOfOperation> list = new List<Entities_ERFO_PurposeOfOperation>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ERFO_MT_PurposeOfOperation_GetByName_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_ERFO_PurposeOfOperation entities = new Entities_ERFO_PurposeOfOperation();

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

    #region EQUIPMENT REQUIREMENTS

    public List<Entities_ERFO_EquipmentRequirement> ERFO_MT_EquipmentRequirement_GetAll()
    {
        List<Entities_ERFO_EquipmentRequirement> list = new List<Entities_ERFO_EquipmentRequirement>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ERFO_MT_EquipmentRequirement_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_ERFO_EquipmentRequirement entities = new Entities_ERFO_EquipmentRequirement();

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

    public int ERFO_MT_EquipmentRequirement_IsDisabled(Entities_ERFO_EquipmentRequirement entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ERFO_MT_EquipmentRequirement_IsDisabled";

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

    public int ERFO_MT_EquipmentRequirement_Append(Entities_ERFO_EquipmentRequirement entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ERFO_MT_EquipmentRequirement_Append";

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

    public int ERFO_MT_EquipmentRequirement_Insert(Entities_ERFO_EquipmentRequirement entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ERFO_MT_EquipmentRequirement_Insert";

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

    public List<Entities_ERFO_EquipmentRequirement> ERFO_MT_EquipmentRequirement_GetByName(string name)
    {
        List<Entities_ERFO_EquipmentRequirement> list = new List<Entities_ERFO_EquipmentRequirement>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ERFO_MT_EquipmentRequirement_GetByName";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_ERFO_EquipmentRequirement entities = new Entities_ERFO_EquipmentRequirement();

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

    public List<Entities_ERFO_EquipmentRequirement> ERFO_MT_EquipmentRequirement_GetByName_Like(string name)
    {
        List<Entities_ERFO_EquipmentRequirement> list = new List<Entities_ERFO_EquipmentRequirement>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ERFO_MT_EquipmentRequirement_GetByName_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_ERFO_EquipmentRequirement entities = new Entities_ERFO_EquipmentRequirement();

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

    #region ATTACHED DOCUMENT

    public List<Entities_ERFO_AttachedDocument> ERFO_MT_AttachedDocument_GetAll()
    {
        List<Entities_ERFO_AttachedDocument> list = new List<Entities_ERFO_AttachedDocument>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ERFO_MT_AttachedDocument_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_ERFO_AttachedDocument entities = new Entities_ERFO_AttachedDocument();

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

    public int ERFO_MT_AttachedDocument_IsDisabled(Entities_ERFO_AttachedDocument entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ERFO_MT_AttachedDocument_IsDisabled";

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

    public int ERFO_MT_AttachedDocument_Append(Entities_ERFO_AttachedDocument entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ERFO_MT_AttachedDocument_Append";

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

    public int ERFO_MT_AttachedDocument_Insert(Entities_ERFO_AttachedDocument entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ERFO_MT_AttachedDocument_Insert";

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

    public List<Entities_ERFO_AttachedDocument> ERFO_MT_AttachedDocument_GetByName(string name)
    {
        List<Entities_ERFO_AttachedDocument> list = new List<Entities_ERFO_AttachedDocument>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ERFO_MT_AttachedDocument_GetByName";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_ERFO_AttachedDocument entities = new Entities_ERFO_AttachedDocument();

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

    public List<Entities_ERFO_AttachedDocument> ERFO_MT_AttachedDocument_GetByName_Like(string name)
    {
        List<Entities_ERFO_AttachedDocument> list = new List<Entities_ERFO_AttachedDocument>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ERFO_MT_AttachedDocument_GetByName_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_ERFO_AttachedDocument entities = new Entities_ERFO_AttachedDocument();

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

    #region RECOMMENDED CONTRACTOR

    public List<Entities_ERFO_RecommendedContractor> ERFO_MT_RecommendedContractor_GetAll()
    {
        List<Entities_ERFO_RecommendedContractor> list = new List<Entities_ERFO_RecommendedContractor>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ERFO_MT_RecommendedContractor_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_ERFO_RecommendedContractor entities = new Entities_ERFO_RecommendedContractor();

                entities.RefId = reader["RefId"].ToString();
                entities.ContractorName = reader["ContractorName"].ToString();
                entities.EmailAddress = reader["EmailAddress"].ToString();
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

    public int ERFO_MT_RecommendedContractor_IsDisabled(Entities_ERFO_RecommendedContractor entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ERFO_MT_RecommendedContractor_IsDisabled";

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

    public int ERFO_MT_RecommendedContractor_Append(Entities_ERFO_RecommendedContractor entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ERFO_MT_RecommendedContractor_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@contractorname", entity.ContractorName));
        cmd.Parameters.Add(Factory.CreateParameter("@emailaddress", entity.EmailAddress));
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

    public int ERFO_MT_RecommendedContractor_Insert(Entities_ERFO_RecommendedContractor entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ERFO_MT_RecommendedContractor_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@contractorname", entity.ContractorName));
        cmd.Parameters.Add(Factory.CreateParameter("@emailaddress", entity.EmailAddress));
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

    public List<Entities_ERFO_RecommendedContractor> ERFO_MT_RecommendedContractor_GetByName(string name)
    {
        List<Entities_ERFO_RecommendedContractor> list = new List<Entities_ERFO_RecommendedContractor>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ERFO_MT_RecommendedContractor_GetByName";

            cmd.Parameters.Add(Factory.CreateParameter("@contractorname", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_ERFO_RecommendedContractor entities = new Entities_ERFO_RecommendedContractor();

                entities.RefId = reader["RefId"].ToString();
                entities.ContractorName = reader["ContractorName"].ToString();
                entities.EmailAddress = reader["EmailAddress"].ToString();
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

    public List<Entities_ERFO_RecommendedContractor> ERFO_MT_RecommendedContractor_GetByName_Like(string name)
    {
        List<Entities_ERFO_RecommendedContractor> list = new List<Entities_ERFO_RecommendedContractor>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ERFO_MT_RecommendedContractor_GetByName_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@contractorname", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_ERFO_RecommendedContractor entities = new Entities_ERFO_RecommendedContractor();

                entities.RefId = reader["RefId"].ToString();
                entities.ContractorName = reader["ContractorName"].ToString();
                entities.EmailAddress = reader["EmailAddress"].ToString();
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

    #region ERFO TRANSACTION ENTRY

    public List<Entities_ERFO_RequestEntry> ERFO_TRANSACTION_RequestEntry_Fill_All_DropdownList()
    {
        List<Entities_ERFO_RequestEntry> list = new List<Entities_ERFO_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ERFO_TRANSACTION_RequestEntry_Fill_All_DropdownList";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_ERFO_RequestEntry entities = new Entities_ERFO_RequestEntry();

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

    public List<Entities_ERFO_RequestEntry> ERFO_TRANSACTION_ContractorResponse(string ctrlno)
    {
        List<Entities_ERFO_RequestEntry> list = new List<Entities_ERFO_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ERFO_TRANSACTION_ContractorResponse";
            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", ctrlno = ctrlno.Length <= 0 ? string.Empty : ctrlno));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_ERFO_RequestEntry entities = new Entities_ERFO_RequestEntry();

                entities.ContractorResponseType = reader["TransactionType"].ToString() + " - " + reader["SendReceivedDate"].ToString();
                entities.ContractorResponse = reader["Answer"].ToString();


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

    public Int32 ERFO_TRANSACTION_CountRequestHead(string year)
    {
        Int32 result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ERFO_TRANSACTION_CountRequestHead";

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

    public string ERFO_TRANSACTION_Request_Insert(Entities_ERFO_RequestEntry entity)
    {
        string result = string.Empty;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ERFO_TRANSACTION_Request_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.Ctrlno));
        cmd.Parameters.Add(Factory.CreateParameter("@category", entity.Category));
        cmd.Parameters.Add(Factory.CreateParameter("@dateofoperationfrom", entity.DateOfOperationFrom));
        cmd.Parameters.Add(Factory.CreateParameter("@dateofoperationto", entity.DateOfOperationTo));
        cmd.Parameters.Add(Factory.CreateParameter("@daysrequired", entity.DaysRequired));
        cmd.Parameters.Add(Factory.CreateParameter("@numberofmanpower", entity.NumberOfManpower));
        cmd.Parameters.Add(Factory.CreateParameter("@attacheddocument", entity.AttachedDocument.Substring(1, entity.AttachedDocument.Length - 1)));
        cmd.Parameters.Add(Factory.CreateParameter("@purposeofoperation", entity.PurposeOfOperation.Substring(1, entity.PurposeOfOperation.Length - 1)));
        cmd.Parameters.Add(Factory.CreateParameter("@equipmentrequirements", entity.EquipmentRequirements.Substring(1, entity.EquipmentRequirements.Length - 1)));
        cmd.Parameters.Add(Factory.CreateParameter("@specialinstructionsrequester", entity.SpecialInstructionsRequester));
        cmd.Parameters.Add(Factory.CreateParameter("@recommendedcontractor", entity.RecommendedContractor));
        cmd.Parameters.Add(Factory.CreateParameter("@specialinstructionspurchasing", entity.SpecialInstructionsPurchasing));
        cmd.Parameters.Add(Factory.CreateParameter("@attachment1", entity.Attachment1));
        cmd.Parameters.Add(Factory.CreateParameter("@attachment2", entity.Attachment2));
        cmd.Parameters.Add(Factory.CreateParameter("@requester", entity.Requester));
        cmd.Parameters.Add(Factory.CreateParameter("@sectiondepartment", entity.SectionDepartment));
        cmd.Parameters.Add(Factory.CreateParameter("@local", entity.Local));
        cmd.Parameters.Add(Factory.CreateParameter("@building", entity.Building));
        cmd.Parameters.Add(Factory.CreateParameter("@floor", entity.Floor));
        cmd.Parameters.Add(Factory.CreateParameter("@productionsupervisor", entity.ProductionSupervisor));
        cmd.Parameters.Add(Factory.CreateParameter("@contactnumber", entity.ContactNumber));
        

        try
        {
            conn.Open();
            cmd.Connection = conn;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result = reader["Row_Count"].ToString();
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
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

        return result;

    }

    public int ERFO_TRANSACTION_Request_Append(Entities_ERFO_RequestEntry entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ERFO_TRANSACTION_Request_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.Ctrlno));
        cmd.Parameters.Add(Factory.CreateParameter("@category", entity.Category));
        cmd.Parameters.Add(Factory.CreateParameter("@dateofoperationfrom", entity.DateOfOperationFrom));
        cmd.Parameters.Add(Factory.CreateParameter("@dateofoperationto", entity.DateOfOperationTo));
        cmd.Parameters.Add(Factory.CreateParameter("@daysrequired", entity.DaysRequired));
        cmd.Parameters.Add(Factory.CreateParameter("@numberofmanpower", entity.NumberOfManpower));

        //cmd.Parameters.Add(Factory.CreateParameter("@attacheddocument", entity.AttachedDocument));
        //cmd.Parameters.Add(Factory.CreateParameter("@purposeofoperation", entity.PurposeOfOperation));
        //cmd.Parameters.Add(Factory.CreateParameter("@equipmentrequirements", entity.EquipmentRequirements));

        cmd.Parameters.Add(Factory.CreateParameter("@attacheddocument", entity.AttachedDocument.Substring(1, entity.AttachedDocument.Length - 1)));
        cmd.Parameters.Add(Factory.CreateParameter("@purposeofoperation", entity.PurposeOfOperation.Substring(1, entity.PurposeOfOperation.Length - 1)));
        cmd.Parameters.Add(Factory.CreateParameter("@equipmentrequirements", entity.EquipmentRequirements.Substring(1, entity.EquipmentRequirements.Length - 1)));

        cmd.Parameters.Add(Factory.CreateParameter("@specialinstructionsrequester", entity.SpecialInstructionsRequester));
        cmd.Parameters.Add(Factory.CreateParameter("@recommendedcontractor", entity.RecommendedContractor));
        cmd.Parameters.Add(Factory.CreateParameter("@specialinstructionspurchasing", entity.SpecialInstructionsPurchasing));
        //cmd.Parameters.Add(Factory.CreateParameter("@attachment1", entity.Attachment1));
        //cmd.Parameters.Add(Factory.CreateParameter("@attachment2", entity.Attachment2));
        cmd.Parameters.Add(Factory.CreateParameter("@building", entity.Building));
        cmd.Parameters.Add(Factory.CreateParameter("@floor", entity.Floor));
        cmd.Parameters.Add(Factory.CreateParameter("@productionsupervisor", entity.ProductionSupervisor));
        cmd.Parameters.Add(Factory.CreateParameter("@contactnumber", entity.ContactNumber));

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

    public List<Entities_ERFO_RequestEntry> ERFO_TRANSACTION_RequestStatus_ByDateRange(Entities_ERFO_RequestEntry entity)
    {
        List<Entities_ERFO_RequestEntry> list = new List<Entities_ERFO_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ERFO_TRANSACTION_RequestStatus_ByDateRange";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.Parameters.Add(Factory.CreateParameter("@requester", entity.Requester = entity.Requester.Length <= 0 ? string.Empty : entity.Requester));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_ERFO_RequestEntry entities = new Entities_ERFO_RequestEntry();

                entities.RefId = reader["RefId"].ToString();
                entities.Ctrlno = reader["CtrlNo"].ToString().ToUpper();
                entities.Requester = reader["Requester"].ToString();
                entities.TransactionDate = reader["TransactionDate"].ToString();
                entities.CategoryName = reader["CategoryName"].ToString();
                entities.ProdDeptManager = reader["Req_DeptManager"] != DBNull.Value ? reader["Req_DeptManager"].ToString() : string.Empty;
                entities.DoaProdDeptManager = reader["DOAReq_DeptManager"] != DBNull.Value ? reader["DOAReq_DeptManager"].ToString() : string.Empty;
                entities.StatProdDeptManager = reader["STATReq_DeptManager"] != DBNull.Value ? reader["STATReq_DeptManager"].ToString() : "0";
                entities.ProdDivManager = reader["Req_DivManager"] != DBNull.Value ? reader["Req_DivManager"].ToString() : string.Empty;
                entities.DoaProdDivManager = reader["DOAReq_DivManager"] != DBNull.Value ? reader["DOAReq_DivManager"].ToString() : string.Empty;
                entities.StatProdDivManager = reader["STATReq_DivManager"] != DBNull.Value ? reader["STATReq_DivManager"].ToString() : "0";

                entities.PurIncharge = reader["Pur_Incharge"] != DBNull.Value ? reader["Pur_Incharge"].ToString() : string.Empty;
                entities.DoaPurIncharge = reader["DOAPur_Incharge"] != DBNull.Value ? reader["DOAPur_Incharge"].ToString() : string.Empty;
                entities.StatPurIncharge = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
                entities.PurDeptManager = reader["Pur_DeptManager"] != DBNull.Value ? reader["Pur_DeptManager"].ToString() : string.Empty;
                entities.DoaPurDeptManager = reader["DOAPur_DeptManager"] != DBNull.Value ? reader["DOAPur_DeptManager"].ToString() : string.Empty;
                entities.StatPurDeptManager = reader["STATPur_DeptManager"] != DBNull.Value ? reader["STATPur_DeptManager"].ToString() : "0";

                entities.ReqReceived = reader["Req_Received"] != DBNull.Value ? reader["Req_Received"].ToString() : string.Empty;
                entities.DoaReqReceived = reader["DOAReq_Received"] != DBNull.Value ? reader["DOAReq_Received"].ToString() : string.Empty;
                entities.PurReceived = reader["Pur_Received"] != DBNull.Value ? reader["Pur_Received"].ToString() : string.Empty;
                entities.DoaPurReceived = reader["DOAPur_Received"] != DBNull.Value ? reader["DOAPur_Received"].ToString() : string.Empty;

                entities.SupplierResponse = reader["SupplierResponse"] != DBNull.Value ? reader["SupplierResponse"].ToString() : "0";
                entities.DoaSupplierResponse = reader["DOASupplierResponse"] != DBNull.Value ? reader["DOASupplierResponse"].ToString() : string.Empty;

                entities.Supplier_ReceivedType = reader["ReceivedType"] != DBNull.Value ? reader["ReceivedType"].ToString() : string.Empty;
                entities.Supplier_SendRecievedDate = reader["ReceivedDate"] != DBNull.Value ? reader["ReceivedDate"].ToString() : string.Empty;

                if (entities.StatProdDeptManager == "0")
                {
                    entities.StatAll = "FOR PRODUCTION DEPARTMENT MANAGER APPROVAL";
                    entities.CssColorCode = "#f44336";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "0")
                {
                    entities.StatAll = "FOR PRODUCTION DIVISION MANAGER APPROVAL";
                    entities.CssColorCode = "#28B463";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "0")
                {
                    entities.StatAll = "FOR SUPPLY CHAIN INCHARGE APPROVAL";
                    entities.CssColorCode = "#5499C7";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "1" && entities.StatPurDeptManager == "0")
                {
                    entities.StatAll = "FOR SUPPLY CHAIN DEPARTMENT MANAGER APPROVAL";
                    entities.CssColorCode = "#000000";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "1" && entities.StatPurDeptManager == "1")
                {
                    if (entities.Supplier_ReceivedType.ToUpper() == "SEND")
                    {
                        entities.StatAll = "SENT TO CONTRACTOR / WAITING FOR RESPONSE";
                        entities.CssColorCode = "#039168";
                    }
                    else if (entities.Supplier_ReceivedType.ToUpper() == "RECEIVED")
                    {
                        entities.StatAll = "CONTRACTOR RESPONDED";
                        entities.CssColorCode = "#910375";
                    }
                    else
                    {
                        entities.StatAll = "FOR SENDING TO CONTRACTOR";
                        entities.CssColorCode = "#B09207";
                    }

                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "1" && entities.StatPurDeptManager == "1" && entities.SupplierResponse == "1" && string.IsNullOrEmpty(entities.ReqReceived))
                {
                    entities.StatAll = "FOR PRODUCTION INCHARGE RESPONSE CONFIRMATION";
                    entities.CssColorCode = "#009688";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "1" && entities.StatPurDeptManager == "1" && entities.SupplierResponse == "1" && !string.IsNullOrEmpty(entities.ReqReceived) && string.IsNullOrEmpty(entities.PurReceived))
                {
                    entities.StatAll = "FOR SUPPLY CHAIN INCHARGE RESPONSE CONFIRMATION";
                    entities.CssColorCode = "#009688";
                }

                if (entities.StatProdDeptManager == "2" || entities.StatProdDivManager == "2" || entities.StatPurIncharge == "2" || entities.StatPurDeptManager == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "1" && entities.StatPurDeptManager == "1" && entities.SupplierResponse == "1" && !string.IsNullOrEmpty(entities.ReqReceived) && !string.IsNullOrEmpty(entities.PurReceived))
                {
                    entities.StatAll = "CLOSED";
                    entities.CssColorCode = "#4D0385";
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

    public List<Entities_ERFO_RequestEntry> ERFO_TRANSACTION_AllRequest_ByDateRange(Entities_ERFO_RequestEntry entity)
    {
        List<Entities_ERFO_RequestEntry> list = new List<Entities_ERFO_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ERFO_TRANSACTION_AllRequest_ByDateRange";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_ERFO_RequestEntry entities = new Entities_ERFO_RequestEntry();

                entities.RefId = reader["RefId"].ToString();
                entities.Ctrlno = reader["CtrlNo"].ToString().ToUpper();
                entities.Requester = reader["Requester"].ToString();
                entities.TransactionDate = reader["TransactionDate"].ToString();
                entities.CategoryName = reader["CategoryName"].ToString();
                entities.ProdDeptManager = reader["Req_DeptManager"] != DBNull.Value ? reader["Req_DeptManager"].ToString() : string.Empty;
                entities.DoaProdDeptManager = reader["DOAReq_DeptManager"] != DBNull.Value ? reader["DOAReq_DeptManager"].ToString() : string.Empty;
                entities.StatProdDeptManager = reader["STATReq_DeptManager"] != DBNull.Value ? reader["STATReq_DeptManager"].ToString() : "0";
                entities.ProdDivManager = reader["Req_DivManager"] != DBNull.Value ? reader["Req_DivManager"].ToString() : string.Empty;
                entities.DoaProdDivManager = reader["DOAReq_DivManager"] != DBNull.Value ? reader["DOAReq_DivManager"].ToString() : string.Empty;
                entities.StatProdDivManager = reader["STATReq_DivManager"] != DBNull.Value ? reader["STATReq_DivManager"].ToString() : "0";

                entities.PurIncharge = reader["Pur_Incharge"] != DBNull.Value ? reader["Pur_Incharge"].ToString() : string.Empty;
                entities.DoaPurIncharge = reader["DOAPur_Incharge"] != DBNull.Value ? reader["DOAPur_Incharge"].ToString() : string.Empty;
                entities.StatPurIncharge = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
                entities.PurDeptManager = reader["Pur_DeptManager"] != DBNull.Value ? reader["Pur_DeptManager"].ToString() : string.Empty;
                entities.DoaPurDeptManager = reader["DOAPur_DeptManager"] != DBNull.Value ? reader["DOAPur_DeptManager"].ToString() : string.Empty;
                entities.StatPurDeptManager = reader["STATPur_DeptManager"] != DBNull.Value ? reader["STATPur_DeptManager"].ToString() : "0";

                entities.ReqReceived = reader["Req_Received"] != DBNull.Value ? reader["Req_Received"].ToString() : string.Empty;
                entities.DoaReqReceived = reader["DOAReq_Received"] != DBNull.Value ? reader["DOAReq_Received"].ToString() : string.Empty;
                entities.PurReceived = reader["Pur_Received"] != DBNull.Value ? reader["Pur_Received"].ToString() : string.Empty;
                entities.DoaPurReceived = reader["DOAPur_Received"] != DBNull.Value ? reader["DOAPur_Received"].ToString() : string.Empty;

                entities.SupplierResponse = reader["SupplierResponse"] != DBNull.Value ? reader["SupplierResponse"].ToString() : "0";
                entities.DoaSupplierResponse = reader["DOASupplierResponse"] != DBNull.Value ? reader["DOASupplierResponse"].ToString() : string.Empty;

                entities.Supplier_ReceivedType = reader["ReceivedType"] != DBNull.Value ? reader["ReceivedType"].ToString() : string.Empty;
                entities.Supplier_SendRecievedDate = reader["ReceivedDate"] != DBNull.Value ? reader["ReceivedDate"].ToString() : string.Empty;

                if (entities.StatProdDeptManager == "0")
                {
                    entities.StatAll = "FOR PRODUCTION DEPARTMENT MANAGER APPROVAL";
                    entities.CssColorCode = "#f44336";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "0")
                {
                    entities.StatAll = "FOR PRODUCTION DIVISION MANAGER APPROVAL";
                    entities.CssColorCode = "#28B463";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "0")
                {
                    entities.StatAll = "FOR SUPPLY CHAIN INCHARGE APPROVAL";
                    entities.CssColorCode = "#5499C7";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "1" && entities.StatPurDeptManager == "0")
                {
                    entities.StatAll = "FOR SUPPLY CHAIN DEPARTMENT MANAGER APPROVAL";
                    entities.CssColorCode = "#000000";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "1" && entities.StatPurDeptManager == "1")
                {
                    if (entities.Supplier_ReceivedType.ToUpper() == "SEND")
                    {
                        entities.StatAll = "SENT TO CONTRACTOR / WAITING FOR RESPONSE";
                        entities.CssColorCode = "#039168";
                    }
                    else if (entities.Supplier_ReceivedType.ToUpper() == "RECEIVED")
                    {
                        entities.StatAll = "CONTRACTOR RESPONDED";
                        entities.CssColorCode = "#910375";
                    }
                    else
                    {
                        entities.StatAll = "FOR SENDING TO CONTRACTOR";
                        entities.CssColorCode = "#B09207";
                    }

                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "1" && entities.StatPurDeptManager == "1" && entities.SupplierResponse == "1" && string.IsNullOrEmpty(entities.ReqReceived))
                {
                    entities.StatAll = "FOR PRODUCTION INCHARGE RESPONSE CONFIRMATION";
                    entities.CssColorCode = "#009688";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "1" && entities.StatPurDeptManager == "1" && entities.SupplierResponse == "1" && !string.IsNullOrEmpty(entities.ReqReceived) && string.IsNullOrEmpty(entities.PurReceived))
                {
                    entities.StatAll = "FOR SUPPLY CHAIN INCHARGE RESPONSE CONFIRMATION";
                    entities.CssColorCode = "#009688";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "1" && entities.StatPurDeptManager == "1" && entities.SupplierResponse == "1" && !string.IsNullOrEmpty(entities.ReqReceived) && !string.IsNullOrEmpty(entities.PurReceived))
                {
                    entities.StatAll = "CLOSED";
                    entities.CssColorCode = "#4D0385";
                }

                if (entities.StatProdDeptManager == "2" || entities.StatProdDivManager == "2" || entities.StatPurIncharge == "2" || entities.StatPurDeptManager == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
                }

                entities.Requester = CryptorEngine.Decrypt(reader["FullName"].ToString(), true);
                entities.Building = reader["Building"].ToString();
                entities.Floor = reader["Floor"].ToString();
                entities.ProductionSupervisor = reader["ProductionSupervisor"].ToString();
                entities.ContactNumber = reader["ContactNumber"].ToString();
                entities.SpecialInstructionsRequester = reader["SpecialInstructionsRequester"].ToString();
                entities.RecommendedContractor = reader["ContractorName"].ToString();
                entities.SpecialInstructionsPurchasing = reader["SpecialInstructionsPurchasing"].ToString();
                entities.DateOfOperationFrom = reader["DateOfOperationFrom"].ToString();
                entities.DateOfOperationTo = reader["DateOfOperationTo"].ToString();
                entities.DaysRequired = reader["DaysRequired"].ToString();
                entities.NumberOfManpower = reader["NumberOfManpower"].ToString();
                entities.AttachedDocument = reader["AttachedDocument"].ToString();
                entities.PurposeOfOperation = reader["PurposeOfOperation"].ToString();
                entities.EquipmentRequirements = reader["EquipmentRequirements"].ToString();
                entities.ContractorName = reader["ContractorName"].ToString();

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

    public List<Entities_ERFO_RequestEntry> ERFO_TRANSACTION_GetRequestByCTRLNo(Entities_ERFO_RequestEntry entity)
    {
        List<Entities_ERFO_RequestEntry> list = new List<Entities_ERFO_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ERFO_TRANSACTION_GetRequestByCTRLNo";
            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.Ctrlno = entity.Ctrlno.Length <= 0 ? string.Empty : entity.Ctrlno));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_ERFO_RequestEntry entities = new Entities_ERFO_RequestEntry();

                entities.RefId = reader["RefId"].ToString();
                entities.Ctrlno = reader["CtrlNo"].ToString().ToUpper();
                entities.Requester = CryptorEngine.Decrypt(reader["Requester_Name"].ToString(), true);
                entities.TransactionDate = reader["TransactionDate"].ToString();
                entities.DateOfOperationFrom = reader["DateOfOperationFrom"].ToString();
                entities.DateOfOperationTo = reader["DateOfOperationTo"].ToString();
                entities.DaysRequired = reader["DaysRequired"].ToString();
                entities.NumberOfManpower = reader["NumberOfManpower"].ToString();
                entities.AttachedDocument = reader["AttachedDocument"].ToString();
                entities.PurposeOfOperation = reader["PurposeOfOperation"].ToString();
                entities.EquipmentRequirements = reader["EquipmentRequirements"].ToString();

                entities.SpecialInstructionsRequester = reader["SpecialInstructionsRequester"].ToString();
                entities.RecommendedContractor = reader["RecommendedContractor"].ToString();
                entities.SpecialInstructionsPurchasing = reader["SpecialInstructionsPurchasing"].ToString();
                entities.SectionDepartment = reader["SectionDepartment"].ToString();
                entities.Local = reader["Local"].ToString();

                entities.Building = reader["Building"].ToString();
                entities.Floor = reader["Floor"].ToString();
                entities.ProductionSupervisor = reader["ProductionSupervisor"].ToString();
                entities.ContactNumber = reader["ContactNumber"].ToString();

                entities.Attachment1 = reader["Attachment1"].ToString();
                entities.Attachment2 = reader["Attachment2"].ToString();
                entities.AttachmentConfirmed = reader["AttachmentConfirmed"].ToString();
                entities.Contrator_Attachment = reader["ContractorAttachment"].ToString();

                entities.Category = reader["Category"].ToString();
                entities.ProdDeptManager = reader["Req_DeptManager"] != DBNull.Value ? reader["Req_DeptManager"].ToString() : string.Empty;
                entities.DoaProdDeptManager = reader["DOAReq_DeptManager"] != DBNull.Value ? reader["DOAReq_DeptManager"].ToString() : string.Empty;
                entities.StatProdDeptManager = reader["STATReq_DeptManager"] != DBNull.Value ? reader["STATReq_DeptManager"].ToString() : "0";
                entities.ProdDivManager = reader["Req_DivManager"] != DBNull.Value ? reader["Req_DivManager"].ToString() : string.Empty;
                entities.DoaProdDivManager = reader["DOAReq_DivManager"] != DBNull.Value ? reader["DOAReq_DivManager"].ToString() : string.Empty;
                entities.StatProdDivManager = reader["STATReq_DivManager"] != DBNull.Value ? reader["STATReq_DivManager"].ToString() : "0";

                entities.PurIncharge = reader["Pur_Incharge"] != DBNull.Value ? reader["Pur_Incharge"].ToString() : string.Empty;
                entities.DoaPurIncharge = reader["DOAPur_Incharge"] != DBNull.Value ? reader["DOAPur_Incharge"].ToString() : string.Empty;
                entities.StatPurIncharge = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
                entities.PurDeptManager = reader["Pur_DeptManager"] != DBNull.Value ? reader["Pur_DeptManager"].ToString() : string.Empty;
                entities.DoaPurDeptManager = reader["DOAPur_DeptManager"] != DBNull.Value ? reader["DOAPur_DeptManager"].ToString() : string.Empty;
                entities.StatPurDeptManager = reader["STATPur_DeptManager"] != DBNull.Value ? reader["STATPur_DeptManager"].ToString() : "0";

                entities.ReqReceived = reader["Req_Received"] != DBNull.Value ? reader["Req_Received"].ToString() : string.Empty;
                entities.DoaReqReceived = reader["DOAReq_Received"] != DBNull.Value ? reader["DOAReq_Received"].ToString() : string.Empty;
                entities.PurReceived = reader["Pur_Received"] != DBNull.Value ? reader["Pur_Received"].ToString() : string.Empty;
                entities.DoaPurReceived = reader["DOAPur_Received"] != DBNull.Value ? reader["DOAPur_Received"].ToString() : string.Empty;

                entities.ProdIncharge_Name = reader["Incharge_Name"] != DBNull.Value ? CryptorEngine.Decrypt(reader["Incharge_Name"].ToString(), true) : string.Empty;
                entities.ProdDeptManager_Name = reader["ReqDeptManager_Name"] != DBNull.Value ? CryptorEngine.Decrypt(reader["ReqDeptManager_Name"].ToString(), true) : string.Empty;
                entities.ProdDivManager_Name = reader["ReqDivManager_Name"] != DBNull.Value ? CryptorEngine.Decrypt(reader["ReqDivManager_Name"].ToString(), true) : string.Empty;
                entities.ScdIncharge_Name = reader["PurIncharge_Name"] != DBNull.Value ? CryptorEngine.Decrypt(reader["PurIncharge_Name"].ToString(), true) : string.Empty;
                entities.ScdDeptManager_Name = reader["PurDeptManager_Name"] != DBNull.Value ? CryptorEngine.Decrypt(reader["PurDeptManager_Name"].ToString(), true) : string.Empty;
                entities.ReqReceived_Name = reader["ReqReceived_Name"] != DBNull.Value ? CryptorEngine.Decrypt(reader["ReqReceived_Name"].ToString(), true) : string.Empty;
                entities.PurReceived_Name = reader["PurReceived_Name"] != DBNull.Value ? CryptorEngine.Decrypt(reader["PurReceived_Name"].ToString(), true) : string.Empty;


                entities.SupplierResponse = reader["SupplierResponse"] != DBNull.Value ? reader["SupplierResponse"].ToString() : "0";
                entities.DoaSupplierResponse = reader["DOASupplierResponse"] != DBNull.Value ? reader["DOASupplierResponse"].ToString() : string.Empty;

                entities.Supplier_ReceivedType = reader["ReceivedType"] != DBNull.Value ? reader["ReceivedType"].ToString() : string.Empty;
                entities.Supplier_SendRecievedDate = reader["ReceivedDate"] != DBNull.Value ? reader["ReceivedDate"].ToString() : string.Empty;

                if (entities.StatProdDeptManager == "0")
                {
                    entities.StatAll = "FOR PRODUCTION DEPARTMENT MANAGER APPROVAL";
                    entities.CssColorCode = "#f44336";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "0")
                {
                    entities.StatAll = "FOR PRODUCTION DIVISION MANAGER APPROVAL";
                    entities.CssColorCode = "#28B463";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "0")
                {
                    entities.StatAll = "FOR SUPPLY CHAIN INCHARGE APPROVAL";
                    entities.CssColorCode = "#5499C7";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "1" && entities.StatPurDeptManager == "0")
                {
                    entities.StatAll = "FOR SUPPLY CHAIN DEPARTMENT MANAGER APPROVAL";
                    entities.CssColorCode = "#000000";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "1" && entities.StatPurDeptManager == "1")
                {
                    if (entities.Supplier_ReceivedType.ToUpper() == "SEND")
                    {
                        entities.StatAll = "SENT TO CONTRACTOR / WAITING FOR RESPONSE";
                        entities.CssColorCode = "#039168";
                    }
                    else if (entities.Supplier_ReceivedType.ToUpper() == "RECEIVED")
                    {
                        entities.StatAll = "CONTRACTOR RESPONDED";
                        entities.CssColorCode = "#910375";
                    }
                    else
                    {
                        entities.StatAll = "FOR SENDING TO CONTRACTOR";
                        entities.CssColorCode = "#B09207";
                    }

                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "1" && entities.StatPurDeptManager == "1" && entities.SupplierResponse == "1" && string.IsNullOrEmpty(entities.ReqReceived))
                {
                    entities.StatAll = "FOR PRODUCTION INCHARGE RESPONSE CONFIRMATION";
                    entities.CssColorCode = "#009688";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "1" && entities.StatPurDeptManager == "1" && entities.SupplierResponse == "1" && !string.IsNullOrEmpty(entities.ReqReceived) && string.IsNullOrEmpty(entities.PurReceived))
                {
                    entities.StatAll = "FOR SUPPLY CHAIN INCHARGE RESPONSE CONFIRMATION";
                    entities.CssColorCode = "#009688";
                }

                if (entities.StatProdDeptManager == "2" || entities.StatProdDivManager == "2" || entities.StatPurIncharge == "2" || entities.StatPurDeptManager == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
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

    public List<Entities_ERFO_RequestEntry> ERFO_TRANSACTION_Approval_DateRange(Entities_ERFO_RequestEntry entity)
    {
        List<Entities_ERFO_RequestEntry> list = new List<Entities_ERFO_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ERFO_TRANSACTION_Approval_DateRange";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_ERFO_RequestEntry entities = new Entities_ERFO_RequestEntry();

                entities.RefId = reader["RefId"].ToString();
                entities.Ctrlno = reader["CtrlNo"].ToString().ToUpper();
                entities.Requester = reader["Requester"].ToString();
                entities.TransactionDate = reader["TransactionDate"].ToString();
                entities.Category = reader["Category"].ToString();
                entities.CategoryName = reader["CategoryName"].ToString();
                entities.ProdDeptManager = reader["Req_DeptManager"] != DBNull.Value ? reader["Req_DeptManager"].ToString() : string.Empty;
                entities.DoaProdDeptManager = reader["DOAReq_DeptManager"] != DBNull.Value ? reader["DOAReq_DeptManager"].ToString() : string.Empty;
                entities.StatProdDeptManager = reader["STATReq_DeptManager"] != DBNull.Value ? reader["STATReq_DeptManager"].ToString() : "0";
                entities.ProdDivManager = reader["Req_DivManager"] != DBNull.Value ? reader["Req_DivManager"].ToString() : string.Empty;
                entities.DoaProdDivManager = reader["DOAReq_DivManager"] != DBNull.Value ? reader["DOAReq_DivManager"].ToString() : string.Empty;
                entities.StatProdDivManager = reader["STATReq_DivManager"] != DBNull.Value ? reader["STATReq_DivManager"].ToString() : "0";

                entities.PurIncharge = reader["Pur_Incharge"] != DBNull.Value ? reader["Pur_Incharge"].ToString() : string.Empty;
                entities.DoaPurIncharge = reader["DOAPur_Incharge"] != DBNull.Value ? reader["DOAPur_Incharge"].ToString() : string.Empty;
                entities.StatPurIncharge = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
                entities.PurDeptManager = reader["Pur_DeptManager"] != DBNull.Value ? reader["Pur_DeptManager"].ToString() : string.Empty;
                entities.DoaPurDeptManager = reader["DOAPur_DeptManager"] != DBNull.Value ? reader["DOAPur_DeptManager"].ToString() : string.Empty;
                entities.StatPurDeptManager = reader["STATPur_DeptManager"] != DBNull.Value ? reader["STATPur_DeptManager"].ToString() : "0";

                entities.ReqReceived = reader["Req_Received"] != DBNull.Value ? reader["Req_Received"].ToString() : string.Empty;
                entities.DoaReqReceived = reader["DOAReq_Received"] != DBNull.Value ? reader["DOAReq_Received"].ToString() : string.Empty;
                entities.PurReceived = reader["Pur_Received"] != DBNull.Value ? reader["Pur_Received"].ToString() : string.Empty;
                entities.DoaPurReceived = reader["DOAPur_Received"] != DBNull.Value ? reader["DOAPur_Received"].ToString() : string.Empty;

                entities.SupplierResponse = reader["SupplierResponse"] != DBNull.Value ? reader["SupplierResponse"].ToString() : "0";
                entities.DoaSupplierResponse = reader["DOASupplierResponse"] != DBNull.Value ? reader["DOASupplierResponse"].ToString() : string.Empty;

                entities.Supplier_ReceivedType = reader["ReceivedType"] != DBNull.Value ? reader["ReceivedType"].ToString() : string.Empty;
                entities.Supplier_SendRecievedDate = reader["ReceivedDate"] != DBNull.Value ? reader["ReceivedDate"].ToString() : string.Empty;

                entities.Department = reader["Department"].ToString();
                entities.Division = reader["Division"].ToString();

                entities.RecommendedContractor = reader["RecommendedContractor"].ToString();
                entities.RecommendedContractorEmailAddress = reader["ContractorEmail"].ToString();
                entities.ContractorName = reader["ContractorName"].ToString();
                entities.Attachment1 = reader["Attachment1"].ToString();
                entities.Attachment2 = reader["Attachment2"].ToString();

                if (entities.StatProdDeptManager == "0")
                {
                    entities.StatAll = "FOR PRODUCTION DEPARTMENT MANAGER APPROVAL";
                    entities.CssColorCode = "#f44336";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "0")
                {
                    entities.StatAll = "FOR PRODUCTION DIVISION MANAGER APPROVAL";
                    entities.CssColorCode = "#28B463";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "0")
                {
                    entities.StatAll = "FOR SUPPLY CHAIN INCHARGE APPROVAL";
                    entities.CssColorCode = "#5499C7";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "1" && entities.StatPurDeptManager == "0")
                {
                    entities.StatAll = "FOR SUPPLY CHAIN DEPARTMENT MANAGER APPROVAL";
                    entities.CssColorCode = "#000000";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "1" && entities.StatPurDeptManager == "1")
                {
                    if (entities.Supplier_ReceivedType.ToUpper() == "SEND")
                    {
                        entities.StatAll = "SENT TO CONTRACTOR / WAITING FOR RESPONSE";
                        entities.CssColorCode = "#039168";
                    }
                    else if (entities.Supplier_ReceivedType.ToUpper() == "RECEIVED")
                    {
                        entities.StatAll = "CONTRACTOR RESPONDED";
                        entities.CssColorCode = "#910375";
                    }
                    else
                    {
                        entities.StatAll = "FOR SENDING TO CONTRACTOR";
                        entities.CssColorCode = "#B09207";
                    }

                }                

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "1" && entities.StatPurDeptManager == "1" && entities.SupplierResponse == "1" && string.IsNullOrEmpty(entities.ReqReceived))
                {
                    entities.StatAll = "FOR PRODUCTION INCHARGE RESPONSE CONFIRMATION";
                    entities.CssColorCode = "#009688";
                }

                if (entities.StatProdDeptManager == "1" && entities.StatProdDivManager == "1" && entities.StatPurIncharge == "1" && entities.StatPurDeptManager == "1" && entities.SupplierResponse == "1" && !string.IsNullOrEmpty(entities.ReqReceived) && string.IsNullOrEmpty(entities.PurReceived))
                {
                    entities.StatAll = "FOR SUPPLY CHAIN INCHARGE RESPONSE CONFIRMATION";
                    entities.CssColorCode = "#009688";
                }

                if (entities.StatProdDeptManager == "2" || entities.StatProdDivManager == "2" || entities.StatPurIncharge == "2" || entities.StatPurDeptManager == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
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


    public int ERFO_TRANSACTION_SQLTransaction(string query)
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
