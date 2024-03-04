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

public class DAL_RFQ
{
    public DAL_RFQ()
    {
    }

    #region Section

    public List<Entities_RFQ_Section> RFQ_MT_Section_GetAll_Export()
    {
        List<Entities_RFQ_Section> list = new List<Entities_RFQ_Section>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT MT.RefId, MT.SectionCode, MT.RoprosCode, MT.Description, MT.AddedDate, MT.UpdatedDate, AddedBy.FullName as AddedBy, UpdatedBy.FullName as UpdatedBy, MT.IsDisabled " +
                              "FROM MT_Section MT WITH (NOLOCK) " +
                              "LEFT JOIN Login_Credentials AddedBy WITH (NOLOCK) ON MT.AddedBy = AddedBy.RefId " +
                              "LEFT JOIN Login_Credentials UpdatedBy WITH (NOLOCK) ON MT.UpdatedBy = UpdatedBy.RefId WHERE MT.IsDisabled = '0' ORDER BY MT.SectionCode, MT.Description ASC;";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Section entities = new Entities_RFQ_Section();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["SectionCode"].ToString().ToUpper();
                entities.RoprosCode = reader["RoprosCode"] != DBNull.Value ? reader["RoprosCode"].ToString() : string.Empty;
                entities.Description = reader["Description"].ToString().ToUpper();
                entities.AddedBy = CryptorEngine.Decrypt(reader["AddedBy"].ToString(), true).ToUpper();
                entities.AddedDate = reader["AddedDate"].ToString();
                if (reader["UpdatedBy"].ToString().Length > 0)
                {
                    entities.UpdatedBy = CryptorEngine.Decrypt(reader["UpdatedBy"].ToString(), true).ToUpper();
                }
                else
                {
                    entities.UpdatedBy = "";
                }
                entities.UpdatedDate = reader["UpdatedDate"].ToString();
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

    public List<Entities_RFQ_Section> RFQ_MT_Section_GetAll()
    {
        List<Entities_RFQ_Section> list = new List<Entities_RFQ_Section>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_Section_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Section entities = new Entities_RFQ_Section();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["SectionCode"].ToString();
                entities.Description = reader["Description"].ToString();
                entities.AddedBy = reader["AddedBy"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();
                entities.RoprosCode = reader["RoprosCode"] != DBNull.Value ? reader["RoprosCode"].ToString() : string.Empty;

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

    public int RFQ_MT_Section_IsDisabled(Entities_RFQ_Section entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_Section_IsDisabled";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@isdisabled", entity.IsDisabled));
        cmd.Parameters.Add(Factory.CreateParameter("@disabledby", entity.DisabledBy));

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

    public int RFQ_MT_Section_Append(Entities_RFQ_Section entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_Section_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@code", entity.Code));
        cmd.Parameters.Add(Factory.CreateParameter("@roproscode", entity.RoprosCode));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
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

    public int RFQ_MT_Section_Insert(Entities_RFQ_Section entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_Section_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@code", entity.Code));
        cmd.Parameters.Add(Factory.CreateParameter("@roproscode", entity.RoprosCode));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
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

    public List<Entities_RFQ_Section> RFQ_MT_Section_GetByDescription(string description)
    {
        List<Entities_RFQ_Section> list = new List<Entities_RFQ_Section>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_Section_GetByDescription";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Section entities = new Entities_RFQ_Section();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["SectionCode"].ToString();
                entities.Description = reader["Description"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();
                entities.RoprosCode = reader["RoprosCode"] != DBNull.Value ? reader["RoprosCode"].ToString() : string.Empty;

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

    public List<Entities_RFQ_Section> RFQ_MT_Section_GetByDescription_Like(string description)
    {
        List<Entities_RFQ_Section> list = new List<Entities_RFQ_Section>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_Section_GetByDescription_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Section entities = new Entities_RFQ_Section();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["SectionCode"].ToString();
                entities.Description = reader["Description"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();
                entities.RoprosCode = reader["RoprosCode"] != DBNull.Value ? reader["RoprosCode"].ToString() : string.Empty;

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

    #region Department

    public List<Entities_RFQ_Department> RFQ_MT_Department_GetAll()
    {
        List<Entities_RFQ_Department> list = new List<Entities_RFQ_Department>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_Department_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Department entities = new Entities_RFQ_Department();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["DepartmentCode"].ToString();
                entities.Description = reader["Description"].ToString();
                entities.AddedBy = reader["AddedBy"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();
                entities.RoprosCode = reader["RoprosCode"] != DBNull.Value ? reader["RoprosCode"].ToString() : string.Empty;

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

    public List<Entities_RFQ_Department> RFQ_MT_Department_GetAll_Export()
    {
        List<Entities_RFQ_Department> list = new List<Entities_RFQ_Department>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT MT.RefId, MT.DepartmentCode, MT.RoprosCode, MT.Description, MT.AddedDate, MT.UpdatedDate, AddedBy.FullName as AddedBy, UpdatedBy.FullName as UpdatedBy, MT.isDisabled " +
                              "FROM MT_Department MT WITH (NOLOCK) " +
                              "LEFT JOIN Login_Credentials AddedBy WITH (NOLOCK) ON MT.AddedBy = AddedBy.RefId " +
                              "LEFT JOIN Login_Credentials UpdatedBy WITH (NOLOCK) ON MT.UpdatedBy = UpdatedBy.RefId WHERE MT.IsDisabled = '0' OR MT.isDisabled IS NULL ORDER BY MT.DepartmentCode, MT.Description ASC;";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Department entities = new Entities_RFQ_Department();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["DepartmentCode"].ToString().ToUpper();
                entities.Description = reader["Description"].ToString().ToUpper();
                entities.AddedBy = CryptorEngine.Decrypt(reader["AddedBy"].ToString(), true);
                entities.AddedDate = reader["AddedDate"].ToString();
                if (reader["UpdatedBy"].ToString().Length > 0)
                {
                    entities.UpdatedBy = CryptorEngine.Decrypt(reader["UpdatedBy"].ToString(), true);
                }
                else
                {
                    entities.UpdatedBy = "";
                }
                entities.UpdatedDate = reader["UpdatedDate"].ToString();
                entities.IsDisabled = reader["isDisabled"].ToString();
                entities.RoprosCode = reader["RoprosCode"] != DBNull.Value ? reader["RoprosCode"].ToString() : string.Empty;

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

    public int RFQ_MT_Department_IsDisabled(Entities_RFQ_Department entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_Department_IsDisabled";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@isdisabled", entity.IsDisabled));
        cmd.Parameters.Add(Factory.CreateParameter("@disabledBy", entity.DisabledBy));

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

    public int RFQ_MT_Department_Append(Entities_RFQ_Department entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_Department_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@code", entity.Code));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.UpdatedBy));
        cmd.Parameters.Add(Factory.CreateParameter("@roproscode", entity.RoprosCode));

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

    public int RFQ_MT_Department_Insert(Entities_RFQ_Department entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_Department_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@code", entity.Code));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
        cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.AddedBy));
        cmd.Parameters.Add(Factory.CreateParameter("@roproscode", entity.RoprosCode));

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

    public List<Entities_RFQ_Department> RFQ_MT_Department_GetByDescription(string description)
    {
        List<Entities_RFQ_Department> list = new List<Entities_RFQ_Department>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_Department_GetByDescription";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Department entities = new Entities_RFQ_Department();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["DepartmentCode"].ToString();
                entities.Description = reader["Description"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();
                entities.RoprosCode = reader["RoprosCode"] != DBNull.Value ? reader["RoprosCode"].ToString() : string.Empty;

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

    public List<Entities_RFQ_Department> RFQ_MT_Department_GetByDescription_Like(string description)
    {
        List<Entities_RFQ_Department> list = new List<Entities_RFQ_Department>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_Department_GetByDescription_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Department entities = new Entities_RFQ_Department();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["DepartmentCode"].ToString();
                entities.Description = reader["Description"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();
                entities.RoprosCode = reader["RoprosCode"] != DBNull.Value ? reader["RoprosCode"].ToString() : string.Empty;

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

    #region Division

    public List<Entities_RFQ_Division> RFQ_MT_Division_GetAll()
    {
        List<Entities_RFQ_Division> list = new List<Entities_RFQ_Division>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_Division_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Division entities = new Entities_RFQ_Division();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["DivisionCode"].ToString();
                entities.Description = reader["Description"].ToString();
                entities.AddedBy = reader["AddedBy"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();
                entities.RoprosCode = reader["RoprosCode"] != DBNull.Value ? reader["RoprosCode"].ToString() : string.Empty;

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

    public List<Entities_RFQ_Division> RFQ_MT_Division_GetAll_Export()
    {
        List<Entities_RFQ_Division> list = new List<Entities_RFQ_Division>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT MT.RefId, MT.DivisionCode, MT.Description, MT.AddedDate, MT.UpdatedDate, AddedBy.FullName as AddedBy, UpdatedBy.FullName as UpdatedBy, MT.isDisabled " + 
                              "FROM MT_Division MT WITH (NOLOCK) " + 
                              "LEFT JOIN Login_Credentials AddedBy WITH (NOLOCK) ON MT.AddedBy = AddedBy.RefId " +
                              "LEFT JOIN Login_Credentials UpdatedBy WITH (NOLOCK) ON MT.UpdatedBy = UpdatedBy.RefId WHERE MT.IsDisabled = '0' OR MT.isDisabled IS NULL ORDER BY MT.DivisionCode, MT.Description ASC;";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Division entities = new Entities_RFQ_Division();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["DivisionCode"].ToString().ToUpper();
                entities.Description = reader["Description"].ToString().ToUpper();
                entities.AddedBy = CryptorEngine.Decrypt(reader["AddedBy"].ToString(), true);
                entities.AddedDate = reader["AddedDate"].ToString();
                if (reader["UpdatedBy"].ToString().Length > 0)
                {
                    entities.UpdatedBy = CryptorEngine.Decrypt(reader["UpdatedBy"].ToString(), true);
                }
                else
                {
                    entities.UpdatedBy = "";
                }
                entities.UpdatedDate = reader["UpdatedDate"].ToString();
                entities.IsDisabled = reader["isDisabled"].ToString();

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


    public int RFQ_MT_Division_IsDisabled(Entities_RFQ_Division entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_Division_IsDisabled";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@isdisabled", entity.IsDisabled));
        cmd.Parameters.Add(Factory.CreateParameter("@disabledBy", entity.DisabledBy));

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

    public int RFQ_MT_Division_Append(Entities_RFQ_Division entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_Division_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@code", entity.Code));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.UpdatedBy));
        cmd.Parameters.Add(Factory.CreateParameter("@roproscode", entity.RoprosCode));

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

    public int RFQ_MT_Division_Insert(Entities_RFQ_Division entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_Division_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@code", entity.Code));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
        cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.AddedBy));
        cmd.Parameters.Add(Factory.CreateParameter("@roproscode", entity.RoprosCode));

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

    public List<Entities_RFQ_Division> RFQ_MT_Division_GetByDescription(string description)
    {
        List<Entities_RFQ_Division> list = new List<Entities_RFQ_Division>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_Division_GetByDescription";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Division entities = new Entities_RFQ_Division();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["DivisionCode"].ToString();
                entities.Description = reader["Description"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();
                entities.RoprosCode = reader["RoprosCode"] != DBNull.Value ? reader["RoprosCode"].ToString() : string.Empty;

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

    public List<Entities_RFQ_Division> RFQ_MT_Division_GetByDescription_Like(string description)
    {
        List<Entities_RFQ_Division> list = new List<Entities_RFQ_Division>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_Division_GetByDescription_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Division entities = new Entities_RFQ_Division();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["DivisionCode"].ToString();
                entities.Description = reader["Description"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();
                entities.RoprosCode = reader["RoprosCode"] != DBNull.Value ? reader["RoprosCode"].ToString() : string.Empty;

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

    #region PC

    public List<Entities_RFQ_PC> RFQ_MT_PC_GetAll()
    {
        List<Entities_RFQ_PC> list = new List<Entities_RFQ_PC>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_PC_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_PC entities = new Entities_RFQ_PC();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["PCCode"].ToString();
                entities.Description = reader["Description"].ToString();
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

    public int RFQ_MT_PC_IsDisabled(Entities_RFQ_PC entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_PC_IsDisabled";

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

    public int RFQ_MT_PC_Append(Entities_RFQ_PC entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_PC_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@code", entity.Code));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
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

    public int RFQ_MT_PC_Insert(Entities_RFQ_PC entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_PC_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@code", entity.Code));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
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

    public List<Entities_RFQ_PC> RFQ_MT_PC_GetByDescription(string description)
    {
        List<Entities_RFQ_PC> list = new List<Entities_RFQ_PC>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_PC_GetByDescription";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_PC entities = new Entities_RFQ_PC();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["PCCode"].ToString();
                entities.Description = reader["Description"].ToString();
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

    public List<Entities_RFQ_PC> RFQ_MT_PC_GetByDescription_Like(string description)
    {
        List<Entities_RFQ_PC> list = new List<Entities_RFQ_PC>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_PC_GetByDescription_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_PC entities = new Entities_RFQ_PC();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["PCCode"].ToString();
                entities.Description = reader["Description"].ToString();
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

    #region HQ

    public List<Entities_RFQ_HQ> RFQ_MT_HQ_GetAll()
    {
        List<Entities_RFQ_HQ> list = new List<Entities_RFQ_HQ>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_HQ_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_HQ entities = new Entities_RFQ_HQ();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["HQCode"].ToString();
                entities.Description = reader["Description"].ToString();
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

    public int RFQ_MT_HQ_IsDisabled(Entities_RFQ_HQ entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_HQ_IsDisabled";

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

    public int RFQ_MT_HQ_Append(Entities_RFQ_HQ entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_HQ_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@code", entity.Code));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
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

    public int RFQ_MT_HQ_Insert(Entities_RFQ_HQ entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_HQ_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@code", entity.Code));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
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

    public List<Entities_RFQ_HQ> RFQ_MT_HQ_GetByDescription(string description)
    {
        List<Entities_RFQ_HQ> list = new List<Entities_RFQ_HQ>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_HQ_GetByDescription";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_HQ entities = new Entities_RFQ_HQ();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["HQCode"].ToString();
                entities.Description = reader["Description"].ToString();
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

    public List<Entities_RFQ_HQ> RFQ_MT_HQ_GetByDescription_Like(string description)
    {
        List<Entities_RFQ_HQ> list = new List<Entities_RFQ_HQ>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_HQ_GetByDescription_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_HQ entities = new Entities_RFQ_HQ();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["HQCode"].ToString();
                entities.Description = reader["Description"].ToString();
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

    #region Category

    public List<Entities_RFQ_Category> RFQ_MT_Category_GetAll()
    {
        List<Entities_RFQ_Category> list = new List<Entities_RFQ_Category>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_Category_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Category entities = new Entities_RFQ_Category();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["CategoryCode"].ToString();
                entities.Description = reader["Description"].ToString();
                entities.AddedBy = reader["AddedBy"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.IsDisabled = "0";
                }
                else
                {
                    entities.IsDisabled = reader["IsDisabled"].ToString();
                }
                entities.Aging = reader["Aging"].ToString();

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

    public int RFQ_MT_Category_IsDisabled(Entities_RFQ_Category entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_Category_IsDisabled";

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

    public int RFQ_MT_Category_Append(Entities_RFQ_Category entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_Category_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@code", entity.Code));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.UpdatedBy));
        cmd.Parameters.Add(Factory.CreateParameter("@aging", entity.Aging));

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

    public int RFQ_MT_Category_Insert(Entities_RFQ_Category entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_Category_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@code", entity.Code));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
        cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.AddedBy));
        cmd.Parameters.Add(Factory.CreateParameter("@aging", entity.Aging));

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

    public List<Entities_RFQ_Category> RFQ_MT_Category_GetByDescription(string description)
    {
        List<Entities_RFQ_Category> list = new List<Entities_RFQ_Category>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_Category_GetByDescription";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Category entities = new Entities_RFQ_Category();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["CategoryCode"].ToString();
                entities.Description = reader["Description"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.IsDisabled = "0";
                }
                else
                {
                    entities.IsDisabled = reader["IsDisabled"].ToString();
                }
                entities.Aging = reader["Aging"].ToString();

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

    public List<Entities_RFQ_Category> RFQ_MT_Category_GetByDescription_Like(string description)
    {
        List<Entities_RFQ_Category> list = new List<Entities_RFQ_Category>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_Category_GetByDescription_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Category entities = new Entities_RFQ_Category();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["CategoryCode"].ToString();
                entities.Description = reader["Description"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.IsDisabled = "0";
                }
                else
                {
                    entities.IsDisabled = reader["IsDisabled"].ToString();
                }
                entities.Aging = reader["Aging"].ToString();

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

    #region UnitOfMeasure

    public List<Entities_RFQ_UnitOfMeasure> RFQ_MT_UnitOfMeasure_GetAll()
    {
        List<Entities_RFQ_UnitOfMeasure> list = new List<Entities_RFQ_UnitOfMeasure>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_UnitOfMeasure_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_UnitOfMeasure entities = new Entities_RFQ_UnitOfMeasure();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["UOMCode"].ToString();
                entities.Description = reader["Description"].ToString();
                entities.AddedBy = reader["AddedBy"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.IsDisabled = "0";
                }
                else
                {
                    entities.IsDisabled = reader["IsDisabled"].ToString();
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

    public int RFQ_MT_UnitOfMeasure_IsDisabled(Entities_RFQ_UnitOfMeasure entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_UnitOfMeasure_IsDisabled";

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

    public int RFQ_MT_UnitOfMeasure_Append(Entities_RFQ_UnitOfMeasure entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_UnitOfMeasure_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@code", entity.Code));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
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

    public int RFQ_MT_UnitOfMeasure_Insert(Entities_RFQ_UnitOfMeasure entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_UnitOfMeasure_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@code", entity.Code));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
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

    public List<Entities_RFQ_UnitOfMeasure> RFQ_MT_UnitOfMeasure_GetByDescription(string description)
    {
        List<Entities_RFQ_UnitOfMeasure> list = new List<Entities_RFQ_UnitOfMeasure>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_UnitOfMeasure_GetByDescription";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_UnitOfMeasure entities = new Entities_RFQ_UnitOfMeasure();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["UOMCode"].ToString();
                entities.Description = reader["Description"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.IsDisabled = "0";
                }
                else
                {
                    entities.IsDisabled = reader["IsDisabled"].ToString();
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

    public List<Entities_RFQ_UnitOfMeasure> RFQ_MT_UnitOfMeasure_GetByDescription_Like(string description)
    {
        List<Entities_RFQ_UnitOfMeasure> list = new List<Entities_RFQ_UnitOfMeasure>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_UnitOfMeasure_GetByDescription_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_UnitOfMeasure entities = new Entities_RFQ_UnitOfMeasure();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["UOMCode"].ToString();
                entities.Description = reader["Description"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.IsDisabled = "0";
                }
                else
                {
                    entities.IsDisabled = reader["IsDisabled"].ToString();
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


    #endregion

    #region Currency

    public List<Entities_RFQ_Currency> RFQ_MT_Currency_GetAll()
    {
        List<Entities_RFQ_Currency> list = new List<Entities_RFQ_Currency>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_Currency_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Currency entities = new Entities_RFQ_Currency();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["CurrencyCode"].ToString();
                entities.Description = reader["Description"].ToString();
                entities.AddedBy = reader["AddedBy"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.IsDisabled = "0";
                }
                else
                {
                    entities.IsDisabled = reader["IsDisabled"].ToString();
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

    public int RFQ_MT_Currency_IsDisabled(Entities_RFQ_Currency entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_Currency_IsDisabled";

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

    public int RFQ_MT_Currency_Append(Entities_RFQ_Currency entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_Currency_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@code", entity.Code));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
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

    public int RFQ_MT_Currency_Insert(Entities_RFQ_Currency entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_Currency_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@code", entity.Code));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
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

    public List<Entities_RFQ_Currency> RFQ_MT_Currency_GetByDescription(string description)
    {
        List<Entities_RFQ_Currency> list = new List<Entities_RFQ_Currency>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_Currency_GetByDescription";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Currency entities = new Entities_RFQ_Currency();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["CurrencyCode"].ToString();
                entities.Description = reader["Description"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.IsDisabled = "0";
                }
                else
                {
                    entities.IsDisabled = reader["IsDisabled"].ToString();
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

    public List<Entities_RFQ_Currency> RFQ_MT_Currency_GetByDescription_Like(string description)
    {
        List<Entities_RFQ_Currency> list = new List<Entities_RFQ_Currency>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_Currency_GetByDescription_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Currency entities = new Entities_RFQ_Currency();

                entities.RefId = reader["RefId"].ToString();
                entities.Code = reader["CurrencyCode"].ToString();
                entities.Description = reader["Description"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.IsDisabled = "0";
                }
                else
                {
                    entities.IsDisabled = reader["IsDisabled"].ToString();
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

    #endregion

    #region Supplier

    public List<Entities_RFQ_Supplier> RFQ_MT_Supplier_GetAll()
    {
        List<Entities_RFQ_Supplier> list = new List<Entities_RFQ_Supplier>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_Supplier_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Supplier entities = new Entities_RFQ_Supplier();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.EmailAddress = reader["EmailAddress"].ToString();
                entities.EvaluationEmail = reader["EvaluationEmail"].ToString();
                entities.Address = reader["Address"].ToString();
                entities.AddedBy = reader["AddedBy"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.IsDisabled = "0";
                }
                else
                {
                    entities.IsDisabled = reader["IsDisabled"].ToString();
                }

                entities.Registered = reader["Registered"] != DBNull.Value ? reader["Registered"].ToString() : "0";
                entities.Peza = reader["Peza"] != DBNull.Value ? reader["Peza"].ToString() : "0";

                //if (reader["Registered"] != DBNull.Value)
                //{
                //    if (reader["Registered"].ToString() == "1")
                //    {
                //        entities.RegisteredName = "REGISTERED";
                //    }
                //    if (reader["Registered"].ToString() == "2")
                //    {
                //        entities.RegisteredName = "NOT REGISTERED";
                //    }
                //}
                //else
                //{
                //    entities.RegisteredName = "";
                //}

                entities.Receipient = "0";
                entities.Responded = "0";
                entities.StatDivManager = "0";
                entities.Fy_SupplierId = string.Empty;

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

    public int RFQ_MT_Supplier_IsDisabled(Entities_RFQ_Supplier entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_Supplier_IsDisabled";

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

    public int RFQ_MT_Supplier_Append(Entities_RFQ_Supplier entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_Supplier_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@address", entity.Address));
        cmd.Parameters.Add(Factory.CreateParameter("@emailaddress", entity.EmailAddress));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.UpdatedBy));
        cmd.Parameters.Add(Factory.CreateParameter("@registered", entity.Registered));
        cmd.Parameters.Add(Factory.CreateParameter("@evaluationemail", entity.EvaluationEmail));
        cmd.Parameters.Add(Factory.CreateParameter("@peza", entity.Peza));

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
    

    public int RFQ_MT_Supplier_Insert(Entities_RFQ_Supplier entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_Supplier_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@name", entity.Name));
        cmd.Parameters.Add(Factory.CreateParameter("@address", entity.Address));
        cmd.Parameters.Add(Factory.CreateParameter("@emailaddress", entity.EmailAddress));
        cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.AddedBy));
        cmd.Parameters.Add(Factory.CreateParameter("@registered", entity.Registered));
        cmd.Parameters.Add(Factory.CreateParameter("@evaluationemail", entity.EvaluationEmail));
        cmd.Parameters.Add(Factory.CreateParameter("@peza", entity.Peza));

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

    public int RFQ_MT_Supplier_InsertDetails(Entities_RFQ_Supplier entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_Supplier_InsertDetails";

        cmd.Parameters.Add(Factory.CreateParameter("@headrefid", entity.HeadRefId));
        cmd.Parameters.Add(Factory.CreateParameter("@categoryid", entity.CategoryId));

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

    public List<Entities_RFQ_Supplier> RFQ_MT_Supplier_GetByDescription(string description)
    {
        List<Entities_RFQ_Supplier> list = new List<Entities_RFQ_Supplier>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_Supplier_GetByDescription";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Supplier entities = new Entities_RFQ_Supplier();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.EmailAddress = reader["EmailAddress"].ToString();
                entities.Address = reader["Address"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.IsDisabled = "0";
                }
                else
                {
                    entities.IsDisabled = reader["IsDisabled"].ToString();
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

    public List<Entities_RFQ_Supplier> RFQ_MT_Supplier_GetByDescription_Like(string name)
    {
        List<Entities_RFQ_Supplier> list = new List<Entities_RFQ_Supplier>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_Supplier_GetByDescription_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Supplier entities = new Entities_RFQ_Supplier();

                entities.RefId = reader["RefId"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.EmailAddress = reader["EmailAddress"].ToString();
                entities.Address = reader["Address"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.IsDisabled = "0";
                }
                else
                {
                    entities.IsDisabled = reader["IsDisabled"].ToString();
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


    public List<Entities_RFQ_Category> RFQ_MT_Supplier_GetCategoryByHeadId(string headrefid)
    {
        List<Entities_RFQ_Category> list = new List<Entities_RFQ_Category>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_Supplier_GetCategoryByHeadId";

            cmd.Parameters.Add(Factory.CreateParameter("@headrefid", headrefid));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_Category entities = new Entities_RFQ_Category();

                entities.RefId = reader["RefId"].ToString();
                entities.Description = reader["Description"].ToString();
                
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.IsDisabled = "0";
                }
                else
                {
                    entities.IsDisabled = reader["IsDisabled"].ToString();
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

    #endregion

    #region BuyerInformation

    public List<Entities_RFQ_BuyerInformation> RFQ_MT_BuyerInformation_GetAll()
    {
        List<Entities_RFQ_BuyerInformation> list = new List<Entities_RFQ_BuyerInformation>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_BuyerInformation_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_BuyerInformation entities = new Entities_RFQ_BuyerInformation();

                entities.RefId = reader["RefId"].ToString();
                entities.Member = reader["Member"].ToString();
                entities.Section = reader["Section"].ToString();
                entities.Email = reader["Email"].ToString();
                entities.Mobile = reader["MobileNumber"].ToString();
                entities.AddedBy = reader["AddedBy"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.IsDisabled = "0";
                }
                else
                {
                    entities.IsDisabled = reader["IsDisabled"].ToString();
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

    public int RFQ_MT_BuyerInformation_IsDisabled(Entities_RFQ_BuyerInformation entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_BuyerInformation_IsDisabled";

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

    public int RFQ_MT_BuyerInformation_Append(Entities_RFQ_BuyerInformation entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_BuyerInformation_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@member", entity.Member));
        cmd.Parameters.Add(Factory.CreateParameter("@section", entity.Section));
        cmd.Parameters.Add(Factory.CreateParameter("@email", entity.Email));
        cmd.Parameters.Add(Factory.CreateParameter("@mobilenumber", entity.Mobile));
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

    public int RFQ_MT_BuyerInformation_Insert(Entities_RFQ_BuyerInformation entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_MT_BuyerInformation_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@member", entity.Member));
        cmd.Parameters.Add(Factory.CreateParameter("@section", entity.Section));
        cmd.Parameters.Add(Factory.CreateParameter("@email", entity.Email));
        cmd.Parameters.Add(Factory.CreateParameter("@mobilenumber", entity.Mobile));
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

    public List<Entities_RFQ_BuyerInformation> RFQ_MT_BuyerInformation_GetByMember(string member)
    {
        List<Entities_RFQ_BuyerInformation> list = new List<Entities_RFQ_BuyerInformation>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_BuyerInformation_GetByMember";

            cmd.Parameters.Add(Factory.CreateParameter("@member", member));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_BuyerInformation entities = new Entities_RFQ_BuyerInformation();

                entities.RefId = reader["RefId"].ToString();
                entities.Member = reader["Member"].ToString();
                entities.Section = reader["Section"].ToString();
                entities.Email = reader["Email"].ToString();
                entities.Mobile = reader["MobileNumber"].ToString();
                entities.AddedBy = reader["AddedBy"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.IsDisabled = "0";
                }
                else
                {
                    entities.IsDisabled = reader["IsDisabled"].ToString();
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

    public List<Entities_RFQ_BuyerInformation> RFQ_MT_BuyerInformation_GetByMember_Like(string description)
    {
        List<Entities_RFQ_BuyerInformation> list = new List<Entities_RFQ_BuyerInformation>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_MT_BuyerInformation_GetByDescription_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_BuyerInformation entities = new Entities_RFQ_BuyerInformation();

                entities.RefId = reader["RefId"].ToString();
                entities.Member = reader["Member"].ToString();
                entities.Section = reader["Section"].ToString();
                entities.Email = reader["Email"].ToString();
                entities.Mobile = reader["MobileNumber"].ToString();
                entities.AddedBy = reader["AddedBy"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.IsDisabled = "0";
                }
                else
                {
                    entities.IsDisabled = reader["IsDisabled"].ToString();
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

    #endregion

    #region TRANSACTIONS

    public string IsAlreadySentToSupplierToday(string rfqno, string supplierid)
    {
        string retVal = string.Empty;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TOP 1 RFQNo FROM Supplier_Rewarded WITH (NOLOCK) WHERE RFQNo = '" + rfqno + "' AND SupplierID = '" + supplierid + "' AND CONVERT(VARCHAR, SendDate, 112) = CONVERT(VARCHAR, GETDATE(), 112)";

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                retVal = reader["RFQNo"].ToString().ToUpper();
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
        return retVal;
    }


    public string getPurchasingRemarksByRFQNo(string rfqno)
    {
        string purchasingRemarks = string.Empty;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT PurchasingRemarks FROM Request_Head WITH (NOLOCK) WHERE RFQNo = '" + rfqno + "'";

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                purchasingRemarks = reader["PurchasingRemarks"].ToString().ToUpper();
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
        return purchasingRemarks;
    }

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Monitoring_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_Monitoring_ByDateRange";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.Parameters.Add(Factory.CreateParameter("@requester", entity.Requester = entity.Requester.Length <= 0 ? string.Empty : entity.Requester));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                string prodManager = (reader["ProdManager"] != DBNull.Value ? reader["ProdManager"] : string.Empty).ToString();
                string purchasing = (reader["Purchasing"] != DBNull.Value ? reader["Purchasing"] : string.Empty).ToString();
                string buyer = (reader["Buyer"] != DBNull.Value ? reader["Buyer"] : string.Empty).ToString();
                string purchasingIncharge = (reader["PurchasingIncharge"] != DBNull.Value ? reader["PurchasingIncharge"] : string.Empty).ToString();
                string deptManager = (reader["DepartmentManager"] != DBNull.Value ? reader["DepartmentManager"] : string.Empty).ToString();
                string divManager = (reader["DivisionManager"] != DBNull.Value ? reader["DivisionManager"] : string.Empty).ToString();
                string supplier = (reader["Supplier"] != DBNull.Value ? reader["Supplier"] : string.Empty).ToString();

                entities.Rfqno = reader["RFQNo"].ToString().ToUpper();
                entities.CategoryName = reader["CategoryName"].ToString().ToUpper();
                entities.TransactionDate = reader["TransactionDate"].ToString();
                entities.ProdManagerStatus = reader["ProdManager"].ToString();
                //entities.PurchasingRemarks = reader["PurchasingRemarks"].ToString();
                entities.PurchasingRemarks = reader["BuyerNotes"].ToString();
                entities.RhDepartmentCode = reader["DepartmentCode"].ToString().ToUpper();
                entities.ApprovedDate = reader["ApprovedDate"].ToString().ToUpper();
                
                
                if (prodManager == "0")
                {
                    entities.StatAll = "FOR PRODUCTION MANAGER APPROVAL";
                    entities.CssColorCode = "#f44336";
                }
                if (prodManager == "3")
                {
                    entities.StatAll = "HOLD BY PRODUCTION MANAGER";
                    entities.CssColorCode = "#3C4C54";
                }
                if (prodManager == "1" && purchasing == "0")
                {
                    entities.StatAll = "FOR BUYER APPROVAL";
                    entities.CssColorCode = "#9C27B0";
                }
                if (prodManager == "1" && purchasing == "3")
                {
                    entities.StatAll = "HOLD BY SC BUYER";
                    entities.CssColorCode = "#3C4C54";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "3")
                {
                    entities.StatAll = "QUOTATION SENT TO SUPPLIER(S)";
                    entities.CssColorCode = "#673AB7";
                }
                if (prodManager == "1" && purchasing == "1" && int.Parse(reader["NumberOfResponded"].ToString()) > 0)
                {
                    entities.StatAll = "SUPPLIER RESPONDED / FOR BUYER APPROVAL";
                    entities.CssColorCode = "#009688";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1")
                {
                    entities.StatAll = "FOR PURCHASING INCHARGE APPROVAL";
                    entities.CssColorCode = "#8BC34A";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1")
                {
                    entities.StatAll = "FOR PURCHASING DEPARTMENT MANAGER APPROVAL";
                    entities.CssColorCode = "#CDDC39";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1")
                {
                    entities.StatAll = "FOR PURCHASING DIVISION MANAGER APPROVAL";
                    entities.CssColorCode = "#E91E63";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1" && divManager == "1")
                {
                    entities.StatAll = "APPROVED";
                    entities.CssColorCode = "#00C851";
                }
                if (prodManager == "2" || purchasing == "2" || buyer == "2" || purchasingIncharge == "2" || deptManager == "2" || divManager == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
                }

                entities.Requester = CryptorEngine.Decrypt(reader["FullName"].ToString(), true);


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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_MyRequestersItem_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_MyRequestersItem_ByDateRange";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.Parameters.Add(Factory.CreateParameter("@department", entity.RhDepartment = entity.RhDepartment.Length <= 0 ? string.Empty : entity.RhDepartment));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                string prodManager = (reader["ProdManager"] != DBNull.Value ? reader["ProdManager"] : string.Empty).ToString();
                string purchasing = (reader["Purchasing"] != DBNull.Value ? reader["Purchasing"] : string.Empty).ToString();
                string buyer = (reader["Buyer"] != DBNull.Value ? reader["Buyer"] : string.Empty).ToString();
                string purchasingIncharge = (reader["PurchasingIncharge"] != DBNull.Value ? reader["PurchasingIncharge"] : string.Empty).ToString();
                string deptManager = (reader["DepartmentManager"] != DBNull.Value ? reader["DepartmentManager"] : string.Empty).ToString();
                string divManager = (reader["DivisionManager"] != DBNull.Value ? reader["DivisionManager"] : string.Empty).ToString();
                string supplier = (reader["Supplier"] != DBNull.Value ? reader["Supplier"] : string.Empty).ToString();

                entities.Rfqno = reader["RFQNo"].ToString().ToUpper();
                entities.CategoryName = reader["CategoryName"].ToString().ToUpper();
                entities.TransactionDate = reader["TransactionDate"].ToString();
                entities.ProdManagerStatus = reader["ProdManager"].ToString();
                if (prodManager == "0")
                {
                    entities.StatAll = "FOR PRODUCTION MANAGER APPROVAL";
                    entities.CssColorCode = "#f44336";
                }
                if (prodManager == "1" && purchasing == "0")
                {
                    entities.StatAll = "FOR BUYER APPROVAL";
                    entities.CssColorCode = "#9C27B0";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "3")
                {
                    entities.StatAll = "QUOTATION SENT TO SUPPLIER(S)";
                    entities.CssColorCode = "#673AB7";
                }
                if (prodManager == "1" && purchasing == "1" && int.Parse(reader["NumberOfResponded"].ToString()) > 0)
                {
                    entities.StatAll = "SUPPLIER RESPONDED / FOR BUYER APPROVAL";
                    entities.CssColorCode = "#009688";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1")
                {
                    entities.StatAll = "FOR PURCHASING INCHARGE APPROVAL";
                    entities.CssColorCode = "#8BC34A";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1")
                {
                    entities.StatAll = "FOR PURCHASING DEPARTMENT MANAGER APPROVAL";
                    entities.CssColorCode = "#CDDC39";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1")
                {
                    entities.StatAll = "FOR PURCHASING DIVISION MANAGER APPROVAL";
                    entities.CssColorCode = "#E91E63";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1" && divManager == "1")
                {
                    entities.StatAll = "APPROVED";
                    entities.CssColorCode = "#00C851";
                }
                if (prodManager == "2" || purchasing == "2" || buyer == "2" || purchasingIncharge == "2" || deptManager == "2" || divManager == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
                }

                entities.Requester = CryptorEngine.Decrypt(reader["FullName"].ToString(), true);


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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_AllRequest_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_AllRequest_ByDateRange";
            cmd.Parameters.Add(Factory.CreateParameter("@criteria", entity.SearchCriteria = entity.SearchCriteria.Length <= 0 ? string.Empty : entity.SearchCriteria));           

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                string prodManager = (reader["ProdManager"] != DBNull.Value ? reader["ProdManager"] : string.Empty).ToString();
                string purchasing = (reader["Purchasing"] != DBNull.Value ? reader["Purchasing"] : string.Empty).ToString();
                string buyer = (reader["Buyer"] != DBNull.Value ? reader["Buyer"] : string.Empty).ToString();
                string purchasingIncharge = (reader["PurchasingIncharge"] != DBNull.Value ? reader["PurchasingIncharge"] : string.Empty).ToString();
                string deptManager = (reader["DepartmentManager"] != DBNull.Value ? reader["DepartmentManager"] : string.Empty).ToString();
                string divManager = (reader["DivisionManager"] != DBNull.Value ? reader["DivisionManager"] : string.Empty).ToString();
                string supplier = (reader["Supplier"] != DBNull.Value ? reader["Supplier"] : string.Empty).ToString();

                entities.CategoryName = reader["CategoryName"].ToString().ToUpper();

                entities.StatProdManager = prodManager;
                entities.StatPurchasing = purchasing;
                entities.StatPurchasingIncharge = purchasingIncharge;
                entities.StatDeptManager = deptManager;
                entities.StatDivManager = divManager;

                entities.RdRfqNo = reader["RFQNo"].ToString().ToUpper().Trim();
                entities.RhBuyerNotes = reader["BuyerNotes"].ToString().ToUpper().Trim();
                entities.RdDescription = reader["Description"].ToString().ToUpper().Trim();
                entities.RdSpecs = reader["Specs"].ToString().ToUpper().Trim();
                entities.RdMaker = reader["Maker"].ToString().ToUpper().Trim();
                entities.TransactionDate = reader["TransactionDate"].ToString().ToUpper();
                entities.CategoryName = reader["CategoryName"].ToString().ToUpper();
                entities.RhCategory = reader["Category"].ToString().ToUpper();
                entities.Requester = CryptorEngine.Decrypt(reader["Requester"].ToString().Replace(" ", "+"), true);

                entities.RhSection = reader["SectionName"].ToString().ToUpper();
                entities.RhDepartment = reader["DepartmentName"].ToString().ToUpper();
                entities.RhDepartmentCode = reader["DepartmentCode"].ToString().ToUpper();
                entities.RhDivision = reader["DivisionName"].ToString().ToUpper();                

                if (prodManager == "0")
                {
                    entities.StatAll = "FOR PRODUCTION MANAGER APPROVAL";
                    entities.CssColorCode = "#f44336";
                }
                if (prodManager == "3")
                {
                    entities.StatAll = "HOLD BY PRODUCTION MANAGER";
                    entities.CssColorCode = "#3C4C54";
                }
                if (prodManager == "1" && purchasing == "0")
                {
                    entities.StatAll = "FOR BUYER APPROVAL";
                    entities.CssColorCode = "#9C27B0";
                }
                if (prodManager == "1" && purchasing == "3")
                {
                    entities.StatAll = "HOLD BY SC BUYER";
                    entities.CssColorCode = "#3C4C54";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "3")
                {
                    entities.StatAll = "QUOTATION SENT TO SUPPLIER(S)";
                    entities.CssColorCode = "#673AB7";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "8")
                {
                    entities.StatAll = "SUPPLIER RESPONDED / FOR BUYER APPROVAL";
                    entities.CssColorCode = "#009688";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1")
                {
                    entities.StatAll = "FOR PURCHASING INCHARGE APPROVAL";
                    entities.CssColorCode = "#8BC34A";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1")
                {
                    entities.StatAll = "FOR PURCHASING DEPARTMENT MANAGER APPROVAL";
                    entities.CssColorCode = "#CDDC39";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1")
                {
                    entities.StatAll = "FOR PURCHASING DIVISION MANAGER APPROVAL";
                    entities.CssColorCode = "#E91E63";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1" && divManager == "1")
                {
                    entities.StatAll = "APPROVED";
                    entities.CssColorCode = "#00C851";
                }
                if (prodManager == "2" || purchasing == "2" || buyer == "2" || purchasingIncharge == "2" || deptManager == "2" || divManager == "2")
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_AllRequest_ByDateRange_Reporting(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_AllRequest_ByDateRange_Reporting";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.CommandTimeout = 1000;
            

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                string prodManager = (reader["ProdManager"] != DBNull.Value ? reader["ProdManager"] : string.Empty).ToString();
                string purchasing = (reader["Purchasing"] != DBNull.Value ? reader["Purchasing"] : string.Empty).ToString();
                string buyer = (reader["Buyer"] != DBNull.Value ? reader["Buyer"] : string.Empty).ToString();
                string purchasingIncharge = (reader["PurchasingIncharge"] != DBNull.Value ? reader["PurchasingIncharge"] : string.Empty).ToString();
                string deptManager = (reader["DepartmentManager"] != DBNull.Value ? reader["DepartmentManager"] : string.Empty).ToString();
                string divManager = (reader["DivisionManager"] != DBNull.Value ? reader["DivisionManager"] : string.Empty).ToString();
                string supplier = (reader["Supplier"] != DBNull.Value ? reader["Supplier"] : string.Empty).ToString();

                entities.CategoryName = reader["CategoryName"].ToString().ToUpper();

                entities.StatProdManager = prodManager;
                entities.StatPurchasing = purchasing;
                entities.StatPurchasingIncharge = purchasingIncharge;
                entities.StatDeptManager = deptManager;
                entities.StatDivManager = divManager;

                entities.RdRfqNo = reader["RFQNo"].ToString().ToUpper().Trim();
                entities.RhBuyerNotes = reader["BuyerNotes"] != DBNull.Value ? reader["BuyerNotes"].ToString().ToUpper().Trim() : string.Empty;
                entities.RdDescription = reader["Description"] != DBNull.Value ? reader["Description"].ToString().ToUpper().Trim() : string.Empty;
                entities.RdSpecs = reader["Specs"] != DBNull.Value ? reader["Specs"].ToString().ToUpper() : string.Empty;
                entities.RdMaker = reader["Maker"] != DBNull.Value ? reader["Maker"].ToString().ToUpper() : string.Empty;
                entities.RdQuantity = reader["Quantity"] != DBNull.Value ? reader["Quantity"].ToString().ToUpper().Trim() : string.Empty;
                entities.RdUnitOfMeasure = reader["UnitOfMeasure"].ToString().ToUpper();
                entities.RdRemarks = reader["Remarks"] != DBNull.Value ? reader["Remarks"].ToString().ToUpper() : string.Empty;
                entities.TransactionDate = reader["TransactionDate"].ToString().ToUpper();
                entities.CategoryName = reader["CategoryName"].ToString().ToUpper();
                entities.RhCategory = reader["Category"].ToString().ToUpper();
                entities.Requester = CryptorEngine.Decrypt(reader["Requester"].ToString().Replace(" ", "+"), true);

                entities.RhSection = reader["SectionName"].ToString().ToUpper();
                entities.RhDepartment = reader["DepartmentName"].ToString().ToUpper();
                entities.RhDepartmentCode = reader["DepartmentCode"].ToString().ToUpper();
                entities.RhDivision = reader["DivisionName"].ToString().ToUpper();
                entities.Report_BuyerName = reader["BuyerName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["BuyerName"].ToString().Replace(" ", "+"), true) : string.Empty;

                entities.RdSupplierName = reader["ApprovedSupplier"] != DBNull.Value ? reader["ApprovedSupplier"].ToString().ToUpper() : string.Empty;
                entities.RdResponsePrice = reader["ResponsePrice"] != DBNull.Value ? reader["ResponsePrice"].ToString().ToUpper() : string.Empty;
                entities.RdResponseLead = reader["ResponseLead"] != DBNull.Value ? reader["ResponseLead"].ToString().ToUpper() : string.Empty;
                entities.ApprovedDate = reader["DivManApproveDate"] != DBNull.Value ? reader["DivManApproveDate"].ToString().ToUpper() : string.Empty;
                entities.PurchasingRemarks = reader["PurchasingRemarks"] != DBNull.Value ? reader["PurchasingRemarks"].ToString().ToUpper() : string.Empty;
                entities.RdResponseRemarks = reader["SupplierRemarks"] != DBNull.Value ? reader["SupplierRemarks"].ToString().ToUpper() : string.Empty;
                

                if (prodManager == "0")
                {
                    entities.StatAll = "FOR PRODUCTION MANAGER APPROVAL";
                    entities.CssColorCode = "#f44336";
                }
                if (prodManager == "3")
                {
                    entities.StatAll = "HOLD BY PRODUCTION MANAGER";
                    entities.CssColorCode = "#3C4C54";
                }
                if (prodManager == "1" && purchasing == "0")
                {
                    entities.StatAll = "FOR BUYER APPROVAL";
                    entities.CssColorCode = "#9C27B0";
                }
                if (prodManager == "1" && purchasing == "3")
                {
                    entities.StatAll = "HOLD BY SC BUYER";
                    entities.CssColorCode = "#3C4C54";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "3")
                {
                    entities.StatAll = "QUOTATION SENT TO SUPPLIER(S)";
                    entities.CssColorCode = "#673AB7";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "8")
                {
                    entities.StatAll = "SUPPLIER RESPONDED / FOR BUYER APPROVAL";
                    entities.CssColorCode = "#009688";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1")
                {
                    entities.StatAll = "FOR PURCHASING INCHARGE APPROVAL";
                    entities.CssColorCode = "#8BC34A";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1")
                {
                    entities.StatAll = "FOR PURCHASING DEPARTMENT MANAGER APPROVAL";
                    entities.CssColorCode = "#CDDC39";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1")
                {
                    entities.StatAll = "FOR PURCHASING DIVISION MANAGER APPROVAL";
                    entities.CssColorCode = "#E91E63";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1" && divManager == "1")
                {
                    entities.StatAll = "APPROVED";
                    entities.CssColorCode = "#00C851";
                }
                if (prodManager == "2" || purchasing == "2" || buyer == "2" || purchasingIncharge == "2" || deptManager == "2" || divManager == "2")
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_HistoryOfUpdates(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_HistoryOfUpdates";
            cmd.Parameters.Add(Factory.CreateParameter("@criteria", entity.SearchCriteria = entity.SearchCriteria.Length <= 0 ? string.Empty : entity.SearchCriteria));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.HistoryRFQ = reader["RFQNo"].ToString().ToUpper().Trim();
                entities.HistoryUpdatedBy = CryptorEngine.Decrypt(reader["UpdatedBy"].ToString(), true);
                entities.HistoryUpdatedDate = reader["UpdatedDate"].ToString().ToUpper().Trim();
                entities.HistoryUpdateWhat = reader["UpdateWhat"].ToString().ToUpper().Trim();
                entities.HistoryUpdateWholeDetails = reader["UpdateWhat"].ToString().ToUpper().Trim() + " - TRANSFER BY : " + entities.HistoryUpdatedBy.ToUpper() + " - TRANSFER DATE : " + entities.HistoryUpdatedDate.ToUpper();  

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

    public List<Entities_RFQ_RequestEntry> RFQ_HistoryOfUpdates_GetAll_UpdatedBy(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_HistoryOfUpdates_GetAll_UpdatedBy";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.HistoryUpdatedByID = reader["UpdatedBy"].ToString().ToUpper().Trim();
                entities.HistoryUpdatedBy = CryptorEngine.Decrypt(reader["FullName"].ToString(), true);
                entities.HistoryUpdatedByUsername = CryptorEngine.Decrypt(reader["Username"].ToString(), true);

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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetTodayTopRespondedSupplier()
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        Common COMMON = new Common();

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_GetTodayTopRespondedSupplier";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.TodayTopSupplier_SupplierId = reader["SupplierID"].ToString().ToUpper();
                entities.TodayTopSupplier_SupplierName = reader["SupplierName"].ToString().ToUpper();
                entities.TodayTopSupplier_SupplierResponseCount = COMMON.formatNumber(decimal.Parse(reader["ResponseCount"].ToString()), 0);


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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetTodayTopRespondedSupplier2()
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        Common COMMON = new Common();

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT LTRIM(RTRIM(SUBSTRING(REPLACE(TransactionLog,'Reference Number :',''), 0,CHARINDEX('_', REPLACE(TransactionLog,'Reference Number :',''))))) AS SupplierID, " +
                              //"REPLACE(LTRIM(RTRIM(SUBSTRING(TransactionLog,CHARINDEX('_',TransactionLog) + 1,CHARINDEX('x', (LTRIM(RTRIM(SUBSTRING(TransactionLog,CHARINDEX('_',TransactionLog) + 1,CHARINDEX('x',TransactionLog) ))))) ))),'.x','') AS RFQNo " +
                              "LTRIM(RTRIM(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(SUBSTRING(REPLACE(TransactionLog,'Reference Number :',''), CHARINDEX('_', REPLACE(TransactionLog,'Reference Number :','')) + 1,CHARINDEX('s', REPLACE(TransactionLog,'Reference Number :',''))))), 'succe',''), 'succes',''), 's',''))) AS RFQNo " +
                              "FROM Service_Logs WITH (NOLOCK) WHERE CONVERT(VARCHAR(10),TransactionDate ,103) = CONVERT(VARCHAR(10),GETDATE(),103) ";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.TodayTopSupplier_SupplierId = reader["SupplierID"].ToString().ToUpper();
                entities.TodayTopSupplier_RFQNo = reader["RFQNo"].ToString().ToUpper();

                if (COMMON.isNumeric(reader["SupplierID"].ToString(), System.Globalization.NumberStyles.Any))
                {
                    list.Add(entities);
                }
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_AllRequest_ByDateRange_AllApproved(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_AllRequest_ByDateRange_AllApproved";
            cmd.Parameters.Add(Factory.CreateParameter("@criteria", entity.SearchCriteria = entity.SearchCriteria.Length <= 0 ? string.Empty : entity.SearchCriteria));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                string prodManager = (reader["ProdManager"] != DBNull.Value ? reader["ProdManager"] : string.Empty).ToString();
                string purchasing = (reader["Purchasing"] != DBNull.Value ? reader["Purchasing"] : string.Empty).ToString();
                string buyer = (reader["Buyer"] != DBNull.Value ? reader["Buyer"] : string.Empty).ToString();
                string purchasingIncharge = (reader["PurchasingIncharge"] != DBNull.Value ? reader["PurchasingIncharge"] : string.Empty).ToString();
                string deptManager = (reader["DepartmentManager"] != DBNull.Value ? reader["DepartmentManager"] : string.Empty).ToString();
                string divManager = (reader["DivisionManager"] != DBNull.Value ? reader["DivisionManager"] : string.Empty).ToString();
                string supplier = (reader["Supplier"] != DBNull.Value ? reader["Supplier"] : string.Empty).ToString();

                entities.CategoryName = reader["CategoryName"].ToString().ToUpper();

                entities.StatProdManager = prodManager;
                entities.StatPurchasing = purchasing;
                entities.StatPurchasingIncharge = purchasingIncharge;
                entities.StatDeptManager = deptManager;
                entities.StatDivManager = divManager;

                entities.RdRfqNo = reader["RFQNo"].ToString().ToUpper().Trim();
                entities.RdDescription = reader["Description"].ToString().ToUpper().Trim();
                entities.RdSpecs = reader["Specs"].ToString().ToUpper().Trim();
                entities.RdMaker = reader["Maker"].ToString().ToUpper().Trim();
                entities.TransactionDate = reader["TransactionDate"].ToString().ToUpper();
                entities.CategoryName = reader["CategoryName"].ToString().ToUpper();
                entities.Requester = CryptorEngine.Decrypt(reader["Requester"].ToString().Replace(" ", "+"), true);

                if (prodManager == "0")
                {
                    entities.StatAll = "FOR PRODUCTION MANAGER APPROVAL";
                    entities.CssColorCode = "#f44336";
                }
                if (prodManager == "1" && purchasing == "0")
                {
                    entities.StatAll = "FOR BUYER APPROVAL";
                    entities.CssColorCode = "#9C27B0";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "3")
                {
                    entities.StatAll = "QUOTATION SENT TO SUPPLIER(S)";
                    entities.CssColorCode = "#673AB7";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "8")
                {
                    entities.StatAll = "SUPPLIER RESPONDED / FOR BUYER APPROVAL";
                    entities.CssColorCode = "#009688";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1")
                {
                    entities.StatAll = "FOR PURCHASING INCHARGE APPROVAL";
                    entities.CssColorCode = "#8BC34A";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1")
                {
                    entities.StatAll = "FOR PURCHASING DEPARTMENT MANAGER APPROVAL";
                    entities.CssColorCode = "#CDDC39";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1")
                {
                    entities.StatAll = "FOR PURCHASING DIVISION MANAGER APPROVAL";
                    entities.CssColorCode = "#E91E63";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1" && divManager == "1")
                {
                    entities.StatAll = "APPROVED";
                    entities.CssColorCode = "#00C851";
                }
                if (prodManager == "2" || purchasing == "2" || buyer == "2" || purchasingIncharge == "2" || deptManager == "2" || divManager == "2")
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_SendDate_ByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_SendDate_ByRFQNo";
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", rfqNo = rfqNo.Length <= 0 ? string.Empty : rfqNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.SendDate = reader["SendDate"].ToString().ToUpper().Replace("12:00:00 AM", "");
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_HistoryOfApproval_ByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_HistoryOfApproval_ByRFQNo";
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", rfqNo = rfqNo.Length <= 0 ? string.Empty : rfqNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.Rfqno = reader["RFQNo"].ToString();
                entities.TransactionName = reader["TransactionName"].ToString();
                entities.ApprovedBy = CryptorEngine.Decrypt(reader["FullName"].ToString(), true);
                entities.ApprovedDate = reader["ApprovedDate"].ToString();
                entities.StatProdManager = reader["ProdManager"].ToString();
                entities.StatPurchasing = reader["Purchasing"].ToString();
                entities.StatPurchasingIncharge = reader["PurchasingInCharge"].ToString();
                entities.StatDeptManager = reader["DepartmentManager"].ToString();
                entities.StatBuyer = reader["Buyer"].ToString();
                entities.StatDivManager = reader["DivisionManager"].ToString();
                entities.StatSupplier = reader["Supplier"].ToString();
                entities.Cause = string.Empty;
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetHoldReason_ByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Request_Hold_Reason WITH (NOLOCK) WHERE RFQNo = '" + rfqNo + "' ORDER BY CreatedDate DESC";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.Rfqno = reader["RFQNo"].ToString();
                entities.Hold_Reason = reader["Reason"].ToString();
                entities.Hold_By = reader["CreatedBy"].ToString();
                entities.Hold_Date = reader["CreatedDate"].ToString();

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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_IsAlready_Approved_ByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_IsAlready_Approved_ByRFQNo";
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", rfqNo = rfqNo.Length <= 0 ? string.Empty : rfqNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.Rfqno = reader["RFQNo"].ToString();
                entities.TransactionName = reader["TransactionName"].ToString();
                entities.ApprovedBy = CryptorEngine.Decrypt(reader["FullName"].ToString(), true);
                entities.ApprovedDate = reader["ApprovedDate"].ToString();
                entities.StatProdManager = reader["ProdManager"].ToString();
                entities.StatPurchasing = reader["Purchasing"].ToString();
                entities.StatPurchasingIncharge = reader["PurchasingInCharge"].ToString();
                entities.StatDeptManager = reader["DepartmentManager"].ToString();
                entities.StatBuyer = reader["Buyer"].ToString();
                entities.StatDivManager = reader["DivisionManager"].ToString();
                entities.StatSupplier = reader["Supplier"].ToString();
                entities.Cause = string.Empty;
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_HistoryOfDisapproval_ByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_HistoryOfDisapproval_ByRFQNo";
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", rfqNo = rfqNo.Length <= 0 ? string.Empty : rfqNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.Rfqno = reader["RFQNo"].ToString();
                entities.TransactionName = reader["TransactionName"].ToString();
                entities.ApprovedBy = CryptorEngine.Decrypt(reader["FullName"].ToString(), true);
                entities.ApprovedDate = reader["DisapprovedDate"].ToString();
                entities.StatProdManager = reader["ProdManager"].ToString();
                entities.StatPurchasing = reader["Purchasing"].ToString();
                entities.StatPurchasingIncharge = reader["PurchasingInCharge"].ToString();
                entities.StatDeptManager = reader["DepartmentManager"].ToString();
                entities.StatBuyer = reader["Buyer"].ToString();
                entities.StatDivManager = reader["DivisionManager"].ToString();
                entities.StatSupplier = reader["Supplier"].ToString();
                entities.Cause = reader["Cause"].ToString();
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Monitoring_GetDetailsByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_Monitoring_GetDetailsByRFQNo";
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", rfqNo = rfqNo.Length <= 0 ? string.Empty : rfqNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry rdEntities = new Entities_RFQ_RequestEntry();

                rdEntities.RdRefId = reader["RefId"].ToString();
                rdEntities.RdRfqNo = reader["RFQNo"].ToString();
                rdEntities.RdDescription = reader["Description"].ToString();
                rdEntities.RdSpecs = reader["Specs"].ToString();
                rdEntities.RdMaker = reader["Maker"].ToString();
                rdEntities.RdQuantity = reader["Quantity"].ToString();
                rdEntities.RdUnitOfMeasure = reader["UnitOfMeasure"].ToString();
                if (reader["Remarks"].ToString().Contains("["))
                {
                    rdEntities.RdRemarks = reader["Remarks"].ToString().Substring(0, reader["Remarks"].ToString().IndexOf("["));
                    rdEntities.RdPurchasingRemarks = reader["Remarks"].ToString().Remove(0, reader["Remarks"].ToString().IndexOf("[")).Replace("[", "").Replace("]", "");
                }
                else
                {
                    rdEntities.RdRemarks = reader["Remarks"].ToString();
                    rdEntities.RdPurchasingRemarks = string.Empty;
                }

                rdEntities.RdProcess = reader["Process"] != DBNull.Value ? reader["Process"].ToString() : string.Empty;
                rdEntities.RdPurpose = reader["Purpose"] != DBNull.Value ? reader["Purpose"].ToString() : string.Empty;

                rdEntities.RdAttachment = reader["Attachment"].ToString();
                rdEntities.RdAttachmentLink = reader["Attachment"].ToString();

                list.Add(rdEntities);
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Monitoring_GetDetailsByRFQNo_ForReApply(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_Monitoring_GetDetailsByRFQNo_ForReApply";
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", rfqNo = rfqNo.Length <= 0 ? string.Empty : rfqNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry rdEntities = new Entities_RFQ_RequestEntry();

                rdEntities.RdRefId = reader["RefId"].ToString();
                rdEntities.RdRfqNo = reader["RFQNo"].ToString();
                rdEntities.RdDescription = reader["Description"].ToString();
                rdEntities.RdSpecs = reader["Specs"].ToString();
                rdEntities.RdMaker = reader["Maker"].ToString();
                rdEntities.RdQuantity = reader["Quantity"].ToString();
                rdEntities.RdUnitOfMeasure = reader["UnitOfMeasure"].ToString();
                if (reader["Remarks"].ToString().Contains("["))
                {
                    rdEntities.RdRemarks = reader["SupplierRemarks"] != DBNull.Value ? reader["SupplierRemarks"].ToString() : string.Empty;
                    rdEntities.RdPurchasingRemarks = reader["Remarks"].ToString().Remove(0, reader["Remarks"].ToString().IndexOf("[")).Replace("[", "").Replace("]", "");
                }
                else
                {
                    rdEntities.RdRemarks = reader["SupplierRemarks"] != DBNull.Value ? reader["SupplierRemarks"].ToString() : string.Empty;
                    rdEntities.RdPurchasingRemarks = string.Empty;
                }


                //rdEntities.ResponseSupplierRemarks = reader["SupplierRemarks"] != DBNull.Value ? reader["SupplierRemarks"].ToString() : string.Empty;
                rdEntities.RdProcess = reader["Process"] != DBNull.Value ? reader["Process"].ToString() : string.Empty;
                rdEntities.RdPurpose = reader["Purpose"] != DBNull.Value ? reader["Purpose"].ToString() : string.Empty;

                rdEntities.RdAttachment = reader["Attachment"].ToString();
                rdEntities.RdAttachmentLink = reader["Attachment"].ToString();

                list.Add(rdEntities);
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


    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetAttachment_ByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        int counter = 0;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_GetAttachment_ByRFQNo";
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", rfqNo = rfqNo.Length <= 0 ? string.Empty : rfqNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                entity.RdRfqNo = reader["RFQNo"].ToString();
                entity.RdDescription = reader["Description"].ToString();
                entity.RdSpecs = reader["Specs"].ToString();
                entity.RdAttachment = reader["Attachment"].ToString();
                entity.RdAttachmentLink = "<a href='/IO_Request/" + reader["RFQNo"].ToString().ToString() + "/" + reader["Attachment"].ToString() + "' runat='server' id='" + counter.ToString() + "' target='_blank', 'height=500,width=500,modal=yes,alwaysRaised=yes'>" + reader["Attachment"].ToString() + "</a>";

                list.Add(entity);
                counter++;
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetRequestDetailsByRFQNoWithUnitPrice_ByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_GetRequestDetailsByRFQNoWithUnitPrice_ByRFQNo";
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", rfqNo = rfqNo.Length <= 0 ? string.Empty : rfqNo));
            cmd.CommandTimeout = 500;

            reader = cmd.ExecuteReader();

            int ctr = 1;

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                entity.RdNo = ctr.ToString();
                entity.RdRefId = reader["RefId"] != DBNull.Value ? reader["RefId"].ToString() : string.Empty;
                entity.RdRfqNo = reader["RFQNo"] != DBNull.Value ? reader["RFQNo"].ToString() : string.Empty;
                entity.RdDescription = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty;
                entity.RdSpecs = reader["Specs"] != DBNull.Value ? reader["Specs"].ToString() : string.Empty;
                entity.RdMaker = reader["Maker"] != DBNull.Value ? reader["Maker"].ToString() : string.Empty;
                entity.RdQuantity = reader["Quantity"] != DBNull.Value ? reader["Quantity"].ToString() : string.Empty;
                entity.RdRemarks = reader["Remarks"] != DBNull.Value ? reader["Remarks"].ToString() : string.Empty;
                entity.RdAttachment = reader["Attachment"] != DBNull.Value ? reader["Attachment"].ToString() : string.Empty;
                //entity.RdAttachmentYN = reader["Attachment"] != DBNull.Value ? "Y" : "N";

                if (reader["Attachment"] != DBNull.Value)
                {
                    if (!string.IsNullOrEmpty(reader["Attachment"].ToString()))
                    {
                        entity.RdAttachmentYN = "Y";
                    }
                    else
                    {
                        entity.RdAttachmentYN = "N";
                    }
                }
                else
                {
                    entity.RdAttachmentYN = "N";
                }

                entity.RdUnitOfMeasure = reader["UnitOfMeasure"] != DBNull.Value ? reader["UnitOfMeasure"].ToString() : string.Empty;
                entity.RdResponsePrice = reader["RCurrency"].ToString() + " " + reader["ResponsePrice"].ToString();
                entity.RdResponseRemarks = reader["ResponseRemarks"] != DBNull.Value ? reader["ResponseRemarks"].ToString() : string.Empty;
                entity.RdCurrency = reader["RCurrency"] != DBNull.Value ? reader["RCurrency"].ToString() : string.Empty;
                entity.RdSupplierName = reader["SupplierName"].ToString() + " | " + reader["ResponseDate"].ToString();
                entity.RdResponseLead = reader["ResponseLead"] != DBNull.Value ? reader["ResponseLead"].ToString() : string.Empty;
                entity.RdPurchasingRemarks = reader["PurchasingRemarks"] != DBNull.Value ? reader["PurchasingRemarks"].ToString() : string.Empty;
                entity.RdPurpose = reader["Purpose"] != DBNull.Value ? reader["Purpose"].ToString() : string.Empty;
                entity.RdProcess = reader["Process"] != DBNull.Value ? reader["Process"].ToString() : string.Empty;

                entity.Registered = reader["Registered"] != DBNull.Value ? reader["Registered"].ToString() : "";

                if (reader["Registered"] != DBNull.Value)
                {
                    if (reader["Registered"].ToString() == "1")
                    {
                        entity.Registered = "REGISTERED";
                    }
                    if (reader["Registered"].ToString() == "2")
                    {
                        entity.Registered = "NOT REGISTERED";
                    }
                }
                else
                {
                    entity.Registered = string.Empty;
                }

                ctr++;

                list.Add(entity);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            Common com = new Common();
            com.sendEmailToRequester("fbasti2003@yahoo.com", "repi-rfqs@adm.rohmphil.com", "ERROR FROM RFQ_TRANSACTION_GetRequestDetailsByRFQNoWithUnitPrice_ByRFQNo", ex.Message);
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetSupplierRespondedOutOfByRFQNo_ByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_GetSupplierRespondedOutOfByRFQNo_ByRFQNo";
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", rfqNo = rfqNo.Length <= 0 ? string.Empty : rfqNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                entity.SupplierRewarded = reader["Rewarded"].ToString();
                entity.SupplierResponded = reader["Responded"].ToString();

                list.Add(entity);
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_RequestEntry_Fill_All_DropdownList()
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_RequestEntry_Fill_All_DropdownList";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                entity.DropdownRefId = reader["RefId"].ToString();
                entity.DropdownName = reader["Name"].ToString();
                entity.TableName = reader["TableName"].ToString();
                entity.IsDisabled = reader["IsDisabled"].ToString();

                list.Add(entity);
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

    public Int32 RFQ_TRANSACTION_CountRequestHead(string year)
    {
        Int32 result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_TRANSACTION_CountRequestHead";

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

    public int RFQ_TRANSACTION_RequestEntry_Insert(Entities_RFQ_RequestEntry entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "RFQ_TRANSACTION_RequestEntry_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@rfqno", entity.RhRfqNo));
        cmd.Parameters.Add(Factory.CreateParameter("@requester", entity.RhRequester));
        cmd.Parameters.Add(Factory.CreateParameter("@category", entity.RhCategory));
        cmd.Parameters.Add(Factory.CreateParameter("@division", entity.RhDivision));
        cmd.Parameters.Add(Factory.CreateParameter("@department", entity.RhDepartment));
        cmd.Parameters.Add(Factory.CreateParameter("@section", entity.RhSection));

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

    public int RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(string query)
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetRequestDetailsByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_GetRequestDetailsByRFQNo";
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", rfqNo = rfqNo.Length <= 0 ? string.Empty : rfqNo));

            reader = cmd.ExecuteReader();

            int ctr = 1;

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                entity.RowNumber = ctr.ToString();
                entity.RdNo = ctr.ToString();
                entity.RdRefId = reader["RefId"].ToString();
                entity.RdRfqNo = reader["RFQNo"].ToString();
                entity.RdDescription = reader["Description"].ToString();
                entity.RdSpecs = reader["Specs"].ToString();
                entity.RdMaker = reader["Maker"].ToString();
                entity.RdQuantity = reader["Quantity"].ToString();
                entity.RdPurpose = reader["Purpose"].ToString();
                entity.RdProcess = reader["Process"].ToString();

                if (reader["Remarks"].ToString().Contains("["))
                {
                    entity.RdRemarks = reader["Remarks"].ToString().Substring(0, reader["Remarks"].ToString().IndexOf("["));
                    entity.RdPurchasingRemarks = reader["Remarks"].ToString().Remove(0, reader["Remarks"].ToString().IndexOf("[")).Replace("[", "").Replace("]", "");
                }
                else
                {
                    entity.RdRemarks = reader["Remarks"].ToString();
                    entity.RdPurchasingRemarks = string.Empty;
                }

                entity.RdAttachment = reader["Attachment"].ToString();
                entity.RdUnitOfMeasure = reader["UOM"].ToString();
                entity.RdUOMDesc = reader["UOMDesc"].ToString();

                ctr++;

                list.Add(entity);
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetRequestDetailsByExistingDescriptionSpecs(string descriptionSpecs)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT DETAILS.RefId, DETAILS.RFQNo, DETAILS.Description, DETAILS.Specs, DETAILS.Maker, DETAILS.Quantity, DETAILS.UOM, DETAILS.Remarks, " +
                              "(SELECT FullName FROM Login_Credentials WITH (NOLOCK) WHERE RefId = HEAD.Requester) AS Requester, " +   
                              "(SELECT Description FROM MT_Category WITH (NOLOCK) WHERE RefId = HEAD.Category) AS CategoryName, " +
                              "HEAD.TransactionDate " +
                              "FROM Request_Details DETAILS WITH (NOLOCK) " +
                              "INNER JOIN Request_Status STATUS WITH (NOLOCK) ON DETAILS.RFQNo = STATUS.RFQNo " +
                              "INNER JOIN Request_Head HEAD WITH (NOLOCK) ON DETAILS.RFQNo = HEAD.RFQNo " +
                              "WHERE UPPER(REPLACE((LTRIM(RTRIM(description)) + LTRIM(RTRIM(specs))), ' ','')) IN (" + descriptionSpecs + ") " +
                              "AND (STATUS.DivisionManager = 1 OR STATUS.Purchasing = 1 AND STATUS.Buyer <> 2 AND STATUS.PurchasingInCharge <> 2 AND STATUS.DepartmentManager <> 2 AND STATUS.DivisionManager <> 2)";

            reader = cmd.ExecuteReader();

            int ctr = 1;

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                entity.RowNumber = ctr.ToString();
                entity.RdNo = ctr.ToString();
                entity.RdRefId = reader["RefId"].ToString();
                entity.RdRfqNo = reader["RFQNo"].ToString();
                entity.RdDescription = reader["Description"].ToString();
                entity.RdSpecs = reader["Specs"] != DBNull.Value ? reader["Specs"].ToString() : string.Empty;
                entity.RdMaker = reader["Maker"] != DBNull.Value ? reader["Maker"].ToString() : string.Empty;
                entity.RdQuantity = reader["Quantity"].ToString();
                entity.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true);
                entity.RhTransactionDate = reader["TransactionDate"].ToString();
                entity.RhCategory = reader["CategoryName"].ToString();

                if (reader["Remarks"].ToString().Contains("["))
                {
                    entity.RdRemarks = reader["Remarks"].ToString().Substring(0, reader["Remarks"].ToString().IndexOf("["));
                    entity.RdPurchasingRemarks = reader["Remarks"].ToString().Remove(0, reader["Remarks"].ToString().IndexOf("[")).Replace("[", "").Replace("]", "");
                }
                else
                {
                    entity.RdRemarks = reader["Remarks"] != DBNull.Value ? reader["Remarks"].ToString() : string.Empty;
                    entity.RdPurchasingRemarks = string.Empty;
                }

                entity.RdUnitOfMeasure = reader["UOM"].ToString();

                ctr++;

                list.Add(entity);
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetStatusByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();
        Common COMMON = new Common();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_GetStatusByRFQNo";
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", rfqNo = rfqNo.Length <= 0 ? string.Empty : rfqNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                string DOAProdManager = reader["DOAProdManager"] != DBNull.Value ? reader["DOAProdManager"].ToString() : string.Empty;
                string ABProdManager = reader["ABProdManager"] != DBNull.Value ? reader["ABProdManager"].ToString() : string.Empty;

                string DOABuyer = reader["DOABuyer"] != DBNull.Value ? reader["DOABuyer"].ToString() : string.Empty;
                string ABBuyer = reader["ABBuyer"] != DBNull.Value ? reader["ABBuyer"].ToString() : string.Empty;

                string DOAIncharge = reader["DOAIncharge"] != DBNull.Value ? reader["DOAIncharge"].ToString() : string.Empty;
                string ABIncharge = reader["ABIncharge"] != DBNull.Value ? reader["ABIncharge"].ToString() : string.Empty;

                string DOADepartmentManager = reader["DOADepartmentManager"] != DBNull.Value ? reader["DOADepartmentManager"].ToString() : string.Empty;
                string ABDepartmentManager = reader["ABDepartmentManager"] != DBNull.Value ? reader["ABDepartmentManager"].ToString() : string.Empty;

                string DOADivisionManager = reader["DOADivisionManager"] != DBNull.Value ? reader["DOADivisionManager"].ToString() : string.Empty;
                string ABDivisionManager = reader["ABDivisionManager"] != DBNull.Value ? reader["ABDivisionManager"].ToString() : string.Empty;

                string prodManager = string.Empty;
                string buyer = string.Empty;
                string incharge = string.Empty;
                string deptManager = string.Empty;
                string divManager = string.Empty;

                if (!string.IsNullOrEmpty(DOAProdManager) && !string.IsNullOrEmpty(ABProdManager))
                {
                    prodManager = "<br/>" + CryptorEngine.Decrypt(ABProdManager.Replace(" ", "+"), true) + "<br/>" + DOAProdManager;
                }
                if (!string.IsNullOrEmpty(DOABuyer) && !string.IsNullOrEmpty(ABBuyer))
                {
                    buyer = "<br/>" + CryptorEngine.Decrypt(ABBuyer.Replace(" ", "+"), true) + "<br/>" + DOABuyer;
                }
                if (!string.IsNullOrEmpty(DOAIncharge) && !string.IsNullOrEmpty(ABIncharge))
                {
                    incharge = "<br/>" + CryptorEngine.Decrypt(ABIncharge.Replace(" ", "+"), true) + "<br/>" + DOAIncharge;
                }
                if (!string.IsNullOrEmpty(DOADepartmentManager) && !string.IsNullOrEmpty(ABDepartmentManager))
                {
                    deptManager = "<br/>" + CryptorEngine.Decrypt(ABDepartmentManager.Replace(" ", "+"), true) + "<br/>" + DOADepartmentManager;
                }
                if (!string.IsNullOrEmpty(DOADivisionManager) && !string.IsNullOrEmpty(ABDivisionManager))
                {
                    divManager = "<br/>" + CryptorEngine.Decrypt(ABDivisionManager.Replace(" ", "+"), true) + "<br/>" + DOADivisionManager;
                }

                entity.StatProdManager = COMMON.setStatus(reader["ProdManager"].ToString());
                entity.StatPurchasing = COMMON.setStatus(reader["Purchasing"].ToString());
                entity.StatBuyer = COMMON.setStatus(reader["Buyer"].ToString());
                entity.StatPurchasingIncharge = COMMON.setStatus(reader["PurchasingInCharge"].ToString());
                entity.StatDeptManager = COMMON.setStatus(reader["DepartmentManager"].ToString());
                entity.StatDivManager = COMMON.setStatus(reader["DivisionManager"].ToString());
                entity.ProdManagerStatus = prodManager;
                entity.BuyerStatus = buyer;
                entity.PurchasingInchargeStatus = incharge;
                entity.DepartmentManagerStatus = deptManager;
                entity.DivisionManagerStatus = divManager;

                if (reader["SendDate"] != DBNull.Value)
                {
                    entity.RhLeadTime = reader["Aging"].ToString() + " day(s) until <b>" + DateTime.Parse(reader["SendDate"].ToString()).AddDays(double.Parse(reader["Aging"].ToString())) + "</b>";
                }
                else
                {
                    entity.RhLeadTime = string.Empty;
                }



                list.Add(entity);
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_PurchasingReceiving_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        string prevRFQ = string.Empty;
        string prevRequesterAttachment = string.Empty;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 500;
            cmd.CommandText = "RFQ_TRANSACTION_PurchasingReceiving_ByDateRange";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.Parameters.Add(Factory.CreateParameter("@category", entity.Category = entity.Category.Length <= 0 ? string.Empty : entity.Category));
            cmd.Parameters.Add(Factory.CreateParameter("@search_criteria", entity.SearchCriteria = entity.SearchCriteria.Length <= 0 ? string.Empty : entity.SearchCriteria));
            

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.RhRfqNo = reader["RFQNo"].ToString().ToUpper();
                entities.RhCategory = reader["CategoryName"].ToString().ToUpper();
                entities.RhTransactionDate = reader["TransactionDate"].ToString();

                entities.StatBuyer = reader["Buyer"] == DBNull.Value ? "0" : reader["Buyer"].ToString();
                entities.StatPurchasingIncharge = reader["PurchasingIncharge"] == DBNull.Value ? "0" : reader["PurchasingIncharge"].ToString();
                entities.StatDeptManager = reader["DepartmentManager"] == DBNull.Value ? "0" : reader["DepartmentManager"].ToString();
                entities.StatDivManager = reader["DivisionManager"] == DBNull.Value ? "0" : reader["DivisionManager"].ToString();

                if (reader["LocalNumber"] == DBNull.Value)
                {
                    entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString().Replace(" ", "+"), true);
                }
                else
                {
                    entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString().Replace(" ", "+"), true) +" (<b>" + reader["LocalNumber"].ToString() + "</b>)";
                }

                if (reader["cntSuppResp"] == DBNull.Value)
                {
                    entities.CntSuppResp = "0";
                }
                else
                {
                    entities.CntSuppResp = reader["cntSuppResp"].ToString().ToUpper();                    
                }

                if (reader["ProdManagerApprovedDate"] == DBNull.Value)
                {
                    entities.RhProdManagerApprovedDate = "";
                }
                else
                {
                    entities.RhProdManagerApprovedDate = reader["ProdManagerApprovedDate"].ToString();
                }
                entities.ResponseNumberOfResponded = reader["NumberOfResponded"].ToString();
                entities.GroupBySupplierResponse = int.Parse(reader["NumberOfResponded"].ToString()) > 0 ? "YES" : "NO";

                entities.RdDescription = reader["Description"].ToString().ToUpper();
                entities.RdSpecs = reader["Specs"].ToString().ToUpper();
                entities.RdAttachment = reader["HasAttachment"].ToString().ToUpper();
                //entities.RequesterAttachment = reader["RequesterAttachment"].ToString().ToUpper();
                //if (string.IsNullOrEmpty(reader["RequesterAttachment"].ToString().ToUpper()))
                if (string.IsNullOrEmpty(reader["HasAttachment"].ToString().ToUpper()) || reader["HasAttachment"].ToString() == "0")
                {
                    entities.RequesterAttachment = "NO";
                }
                else
                {
                    entities.RequesterAttachment = "YES";
                }

                if (prevRFQ.ToUpper() != reader["RFQNo"].ToString().ToUpper())
                {                    
                    list.Add(entities);
                }

                prevRFQ = entities.RhRfqNo;
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_PurchasingReceiving_ByDateRange_Tester(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        string prevRFQ = string.Empty;
        string prevRequesterAttachment = string.Empty;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_PurchasingReceiving_ByDateRange";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.Parameters.Add(Factory.CreateParameter("@category", entity.Category = entity.Category.Length <= 0 ? string.Empty : entity.Category));
            cmd.Parameters.Add(Factory.CreateParameter("@search_criteria", entity.SearchCriteria = entity.SearchCriteria.Length <= 0 ? string.Empty : entity.SearchCriteria));
            cmd.CommandTimeout = 500;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.RhRfqNo = reader["RFQNo"].ToString().ToUpper();
                entities.RhCategory = reader["CategoryName"].ToString().ToUpper();
                entities.RhTransactionDate = reader["TransactionDate"].ToString();

                entities.StatBuyer = reader["Buyer"] == DBNull.Value ? "0" : reader["Buyer"].ToString();
                entities.StatPurchasingIncharge = reader["PurchasingIncharge"] == DBNull.Value ? "0" : reader["PurchasingIncharge"].ToString();
                entities.StatDeptManager = reader["DepartmentManager"] == DBNull.Value ? "0" : reader["DepartmentManager"].ToString();
                entities.StatDivManager = reader["DivisionManager"] == DBNull.Value ? "0" : reader["DivisionManager"].ToString();

                entities.RhRequester = "TEST";

                entities.CntSuppResp = "0";

                entities.RhProdManagerApprovedDate = "";
                entities.ResponseNumberOfResponded = "0";
                entities.GroupBySupplierResponse = "NO";

                entities.RdDescription = reader["Description"].ToString().ToUpper();
                entities.RdSpecs = reader["Specs"].ToString().ToUpper();
                entities.RdAttachment = reader["HasAttachment"].ToString().ToUpper();
                //entities.RequesterAttachment = reader["RequesterAttachment"].ToString().ToUpper();
                //if (string.IsNullOrEmpty(reader["RequesterAttachment"].ToString().ToUpper()))
                entities.RequesterAttachment = "NO";

                //if (prevRFQ.ToUpper() != reader["RFQNo"].ToString().ToUpper())
                //{
                //    list.Add(entities);
                //}

                //prevRFQ = entities.RhRfqNo;

                list.Add(entities);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            Common cm = new Common();
            cm.sendEmailToRequester("fbasti2003@gmail.com", ConfigurationManager.AppSettings["email-username"], "ERROR", ex.Message + "-" + ex.InnerException.ToString());
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_PurchasingReceiving_ByDateRange_TodayTopResponse(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        string prevRFQ = string.Empty;
        string prevRequesterAttachment = string.Empty;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT HEAD.RFQNo, CATEGORY.[Description] AS CategoryName, RHOA.ApprovedDate AS ProdManagerApprovedDate, " +
                                "HEAD.TransactionDate, LC.FullName AS Requester, SRC.cntSuppResp, LC.LocalNumber, " +
                                "(SELECT COUNT(RFQNo) FROM Supplier_Response WITH (NOLOCK) WHERE RFQNo = HEAD.RFQNo) AS NumberOfResponded, " +
                                "(SELECT COUNT(RFQNo) FROM Request_Details WITH (NOLOCK) WHERE LEN([Attachment]) > 0 AND RFQNo = HEAD.RFQNo) AS HasAttachment, " +
                                "DETAILS.Attachment AS RequesterAttachment, " +
                                "DETAILS.[Description], DETAILS.Specs, [STATUS].Buyer, [STATUS].PurchasingInCharge, [STATUS].DepartmentManager, [STATUS].DivisionManager " +
                                "FROM Request_Head HEAD WITH (NOLOCK) " +
                                "INNER JOIN Request_Details DETAILS WITH (NOLOCK) ON HEAD.RFQNo = DETAILS.RFQNo " +
                                "INNER JOIN MT_Category CATEGORY WITH (NOLOCK) ON HEAD.Category = CATEGORY.RefId " +
                                "INNER JOIN vew_RequestHistOfApproval RHOA with (nolock) ON HEAD.RFQNo  = RHOA.RFQNo " +
                                "INNER JOIN Request_Status [STATUS] WITH (NOLOCK) ON HEAD.RFQNo = [STATUS].RFQNo " +
                                "INNER JOIN Login_Credentials LC WITH (NOLOCK) ON HEAD.Requester = LC.RefId " +
                                "LEFT JOIN vew_SupplierResponseCount SRC WITH (NOLOCK) ON HEAD.RFQNo = SRC.RFQNo " +
                                "WHERE HEAD.RFQNo IN (" + entity.SearchCriteria + ") " +
                                "ORDER BY RHOA.ApprovedDate DESC ";


            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.RhRfqNo = reader["RFQNo"].ToString().ToUpper();
                entities.RhCategory = reader["CategoryName"].ToString().ToUpper();
                entities.RhTransactionDate = reader["TransactionDate"].ToString();

                entities.StatBuyer = reader["Buyer"] == DBNull.Value ? "0" : reader["Buyer"].ToString();
                entities.StatPurchasingIncharge = reader["PurchasingIncharge"] == DBNull.Value ? "0" : reader["PurchasingIncharge"].ToString();
                entities.StatDeptManager = reader["DepartmentManager"] == DBNull.Value ? "0" : reader["DepartmentManager"].ToString();
                entities.StatDivManager = reader["DivisionManager"] == DBNull.Value ? "0" : reader["DivisionManager"].ToString();

                if (reader["LocalNumber"] == DBNull.Value)
                {
                    entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString().Replace(" ", "+"), true);
                }
                else
                {
                    entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString().Replace(" ", "+"), true) + " (<b>" + reader["LocalNumber"].ToString() + "</b>)";
                }

                if (reader["cntSuppResp"] == DBNull.Value)
                {
                    entities.CntSuppResp = "0";
                }
                else
                {
                    entities.CntSuppResp = reader["cntSuppResp"].ToString().ToUpper();
                }

                if (reader["ProdManagerApprovedDate"] == DBNull.Value)
                {
                    entities.RhProdManagerApprovedDate = "";
                }
                else
                {
                    entities.RhProdManagerApprovedDate = reader["ProdManagerApprovedDate"].ToString();
                }
                //if (reader["NumberOfResponded"].ToString() == "0")
                //{
                //    //entities.ResponseNumberOfResponded = reader["NumberOfResponded"].ToString();
                //    entities.ResponseNumberOfResponded = "NO";
                //}
                //else
                //{
                //    entities.ResponseNumberOfResponded = "YES";
                //}
                entities.ResponseNumberOfResponded = reader["NumberOfResponded"].ToString();
                entities.GroupBySupplierResponse = "YES";
                //entities.GroupBySupplierResponse = int.Parse(reader["NumberOfResponded"].ToString()) > 0 ? "YES" : "NO";

                entities.RdDescription = reader["Description"].ToString().ToUpper();
                entities.RdSpecs = reader["Specs"].ToString().ToUpper();
                entities.RdAttachment = reader["HasAttachment"].ToString().ToUpper();
                //entities.RequesterAttachment = reader["RequesterAttachment"].ToString().ToUpper();
                //if (string.IsNullOrEmpty(reader["RequesterAttachment"].ToString().ToUpper()))
                if (string.IsNullOrEmpty(reader["HasAttachment"].ToString().ToUpper()) || reader["HasAttachment"].ToString() == "0")
                {
                    entities.RequesterAttachment = "NO";
                }
                else
                {
                    entities.RequesterAttachment = "YES";
                }

                if (prevRFQ.ToUpper() != reader["RFQNo"].ToString().ToUpper())
                {
                    list.Add(entities);
                }

                prevRFQ = entities.RhRfqNo;
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_PurchasingReceiving_ByDateRange_ForResend(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        string prevRFQ = string.Empty;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 500;
            cmd.CommandText = "RFQ_TRANSACTION_PurchasingReceiving_ByDateRange_ForResend";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.Parameters.Add(Factory.CreateParameter("@category", entity.Category = entity.Category.Length <= 0 ? string.Empty : entity.Category));
            cmd.Parameters.Add(Factory.CreateParameter("@search_criteria", entity.SearchCriteria = entity.SearchCriteria.Length <= 0 ? string.Empty : entity.SearchCriteria));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.RhRfqNo = reader["RFQNo"].ToString().ToUpper();
                entities.RhCategory = reader["CategoryName"].ToString().ToUpper();
                entities.RhTransactionDate = reader["TransactionDate"].ToString();

                entities.StatBuyer = reader["Buyer"] == DBNull.Value ? "0" : reader["Buyer"].ToString();
                entities.StatPurchasingIncharge = reader["PurchasingIncharge"] == DBNull.Value ? "0" : reader["PurchasingIncharge"].ToString();
                entities.StatDeptManager = reader["DepartmentManager"] == DBNull.Value ? "0" : reader["DepartmentManager"].ToString();
                entities.StatDivManager = reader["DivisionManager"] == DBNull.Value ? "0" : reader["DivisionManager"].ToString();
                entities.StatSupplier = reader["Supplier"] == DBNull.Value ? "0" : reader["Supplier"].ToString();

                if (reader["LocalNumber"] == DBNull.Value)
                {
                    entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString().Replace(" ", "+"), true);
                }
                else
                {
                    entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString().Replace(" ", "+"), true) +" (<b>" + reader["LocalNumber"].ToString() + "</b>)";
                }

                if (reader["cntSuppResp"] == DBNull.Value)
                {
                    entities.CntSuppResp = "0";
                }
                else
                {
                    entities.CntSuppResp = reader["cntSuppResp"].ToString().ToUpper();
                }

                if (reader["ProdManagerApprovedDate"] == DBNull.Value)
                {
                    entities.RhProdManagerApprovedDate = "";
                }
                else
                {
                    entities.RhProdManagerApprovedDate = reader["ProdManagerApprovedDate"].ToString();
                }
                //entities.ResponseNumberOfResponded = reader["NumberOfResponded"].ToString();
                entities.ResponseNumberOfResponded = reader["Supplier"].ToString();
                entities.GroupBySupplierResponse = int.Parse(reader["NumberOfResponded"].ToString()) > 0 ? "YES" : "NO";

                entities.RdDescription = reader["Description"].ToString().ToUpper();
                entities.RdSpecs = reader["Specs"].ToString().ToUpper();
                entities.RdAttachment = reader["HasAttachment"].ToString().ToUpper();
                //entities.RdMaker = reader["Maker"] == DBNull.Value ? "" : reader["Maker"].ToString();

                if (string.IsNullOrEmpty(reader["Maker"].ToString()))
                {
                    entities.RdMaker = "-";
                }
                else
                {
                    entities.RdMaker = reader["Maker"].ToString();
                }

                //if (string.IsNullOrEmpty(reader["RequesterAttachment"].ToString().ToUpper()))
                if (string.IsNullOrEmpty(reader["HasAttachment"].ToString().ToUpper()) || reader["HasAttachment"].ToString() == "0")
                {
                    entities.RequesterAttachment = "NO";
                }
                else
                {
                    entities.RequesterAttachment = "YES";
                }


                if (prevRFQ.ToUpper() != reader["RFQNo"].ToString().ToUpper())
                {
                    list.Add(entities);
                }

                prevRFQ = entities.RhRfqNo;
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            //Common cm = new Common();
            //cm.sendEmailToRequester("fbasti2003@gmail.com", "repi-rfqs@adm.rohmphil.com", "Error in RESEND 06/14/2021", ex.Message + " - " + ex.StackTrace.ToString());
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetRequesterEmailAndLocalNumber_ByRFQNo(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_GetRequesterEmailAndLocalNumber_ByRFQNo";
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", entity.RhRfqNo = entity.RhRfqNo.Length <= 0 ? string.Empty : entity.RhRfqNo));
            
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.RhRfqNo = reader["RFQNo"].ToString().ToUpper();
                entities.RhEmailAddress = reader["EmailAddress"] != DBNull.Value ? reader["EmailAddress"].ToString().ToUpper() : string.Empty;
                entities.RhLocalNumber = reader["LocalNumber"] != DBNull.Value ? reader["LocalNumber"].ToString() : string.Empty;
                entities.RhBuyerNotes = reader["BuyerNotes"] != DBNull.Value ? reader["BuyerNotes"].ToString() : string.Empty;

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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.RhRfqNo = reader["RFQNo"].ToString().ToUpper();
                entities.RhCategory = reader["CategoryName"].ToString().ToUpper();
                entities.RhTransactionDate = reader["TransactionDate"].ToString();

                entities.StatBuyer = reader["Buyer"] == DBNull.Value ? "0" : reader["Buyer"].ToString();
                entities.StatPurchasingIncharge = reader["PurchasingIncharge"] == DBNull.Value ? "0" : reader["PurchasingIncharge"].ToString();
                entities.StatDeptManager = reader["DepartmentManager"] == DBNull.Value ? "0" : reader["DepartmentManager"].ToString();
                entities.StatDivManager = reader["DivisionManager"] == DBNull.Value ? "0" : reader["DivisionManager"].ToString();
                entities.SendDate = reader["SendDate"] == DBNull.Value ? string.Empty : reader["SendDate"].ToString();

                if (reader["LocalNumber"] == DBNull.Value)
                {
                    entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true);
                }
                else
                {
                    entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true) + " (<b>" + reader["LocalNumber"].ToString() + "</b>)";
                }

                if (reader["cntSuppResp"] == DBNull.Value)
                {
                    entities.CntSuppResp = "0";
                }
                else
                {
                    entities.CntSuppResp = reader["cntSuppResp"].ToString().ToUpper();
                }

                if (reader["ProdManagerApprovedDate"] == DBNull.Value)
                {
                    entities.RhProdManagerApprovedDate = "";
                }
                else
                {
                    entities.RhProdManagerApprovedDate = reader["ProdManagerApprovedDate"].ToString();
                }
                entities.ResponseNumberOfResponded = reader["NumberOfResponded"].ToString();
                entities.GroupBySupplierResponse = int.Parse(entities.CntSuppResp) > 0 ? "FOR RESEND" : "FOR SENDING";
                entities.SupplierResponded = int.Parse(reader["NumberOfResponded"].ToString()) > 0 ? "YES" : "NO";

                entities.RdDescription = reader["Description"].ToString().ToUpper();
                entities.RdSpecs = reader["Specs"].ToString().ToUpper();
                entities.RdMaker = reader["Maker"].ToString().ToUpper();
                entities.RdAttachment = reader["HasAttachment"].ToString().ToUpper();

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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel_Resend(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel_Resend";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.RhRfqNo = reader["RFQNo"].ToString().ToUpper();
                entities.RhCategory = reader["CategoryName"].ToString().ToUpper();
                entities.RhTransactionDate = reader["TransactionDate"].ToString();

                entities.StatBuyer = reader["Buyer"] == DBNull.Value ? "0" : reader["Buyer"].ToString();
                entities.StatPurchasingIncharge = reader["PurchasingIncharge"] == DBNull.Value ? "0" : reader["PurchasingIncharge"].ToString();
                entities.StatDeptManager = reader["DepartmentManager"] == DBNull.Value ? "0" : reader["DepartmentManager"].ToString();
                entities.StatDivManager = reader["DivisionManager"] == DBNull.Value ? "0" : reader["DivisionManager"].ToString();
                entities.SendDate = reader["SendDate"] == DBNull.Value ? string.Empty : reader["SendDate"].ToString();

                if (reader["LocalNumber"] == DBNull.Value)
                {
                    entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true);
                }
                else
                {
                    entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true) + " (<b>" + reader["LocalNumber"].ToString() + "</b>)";
                }

                if (reader["cntSuppResp"] == DBNull.Value)
                {
                    entities.CntSuppResp = "0";
                }
                else
                {
                    entities.CntSuppResp = reader["cntSuppResp"].ToString().ToUpper();
                }

                if (reader["ProdManagerApprovedDate"] == DBNull.Value)
                {
                    entities.RhProdManagerApprovedDate = "";
                }
                else
                {
                    entities.RhProdManagerApprovedDate = reader["ProdManagerApprovedDate"].ToString();
                }
                entities.ResponseNumberOfResponded = reader["NumberOfResponded"].ToString();
                entities.GroupBySupplierResponse = int.Parse(entities.CntSuppResp) > 0 ? "FOR RESEND" : "FOR SENDING";
                entities.SupplierResponded = int.Parse(reader["NumberOfResponded"].ToString()) > 0 ? "YES" : "NO";

                entities.RdDescription = reader["Description"].ToString().ToUpper();
                entities.RdSpecs = reader["Specs"].ToString().ToUpper();
                entities.RdMaker = reader["Maker"].ToString().ToUpper();
                entities.RdAttachment = reader["HasAttachment"].ToString().ToUpper();

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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel_Approved(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_PurchasingReceiving_ExportToExcel_Approved";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.RhRfqNo = reader["RFQNo"].ToString().ToUpper();
                entities.RhCategory = reader["CategoryName"].ToString().ToUpper();
                entities.RhTransactionDate = reader["TransactionDate"].ToString();

                entities.StatBuyer = reader["Buyer"] == DBNull.Value ? "0" : reader["Buyer"].ToString();
                entities.StatPurchasingIncharge = reader["PurchasingIncharge"] == DBNull.Value ? "0" : reader["PurchasingIncharge"].ToString();
                entities.StatDeptManager = reader["DepartmentManager"] == DBNull.Value ? "0" : reader["DepartmentManager"].ToString();
                entities.StatDivManager = reader["DivisionManager"] == DBNull.Value ? "0" : reader["DivisionManager"].ToString();
                entities.SendDate = reader["SendDate"] == DBNull.Value ? string.Empty : reader["SendDate"].ToString();

                if (reader["LocalNumber"] == DBNull.Value)
                {
                    entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true);
                }
                else
                {
                    entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true) + " (<b>" + reader["LocalNumber"].ToString() + "</b>)";
                }

                if (reader["cntSuppResp"] == DBNull.Value)
                {
                    entities.CntSuppResp = "0";
                }
                else
                {
                    entities.CntSuppResp = reader["cntSuppResp"].ToString().ToUpper();
                }

                if (reader["ProdManagerApprovedDate"] == DBNull.Value)
                {
                    entities.RhProdManagerApprovedDate = "";
                }
                else
                {
                    entities.RhProdManagerApprovedDate = reader["ProdManagerApprovedDate"].ToString();
                }
                entities.ResponseNumberOfResponded = reader["NumberOfResponded"].ToString();
                //entities.GroupBySupplierResponse = int.Parse(entities.CntSuppResp) > 0 ? "FOR RESEND" : "FOR SENDING";
                if (entities.StatDivManager == "1")
                {
                    entities.GroupBySupplierResponse = "APPROVED";
                }
                else
                {
                    if (int.Parse(entities.CntSuppResp) > 0)
                    {
                        entities.GroupBySupplierResponse = "FOR RESEND";
                    }
                    else
                    {
                        entities.GroupBySupplierResponse = "FOR SENDING";
                    }
                }
                entities.SupplierResponded = int.Parse(reader["NumberOfResponded"].ToString()) > 0 ? "YES" : "NO";

                entities.RdDescription = reader["Description"].ToString().ToUpper();
                entities.RdSpecs = reader["Specs"].ToString().ToUpper();
                entities.RdMaker = reader["Maker"].ToString().ToUpper();
                entities.RdAttachment = reader["HasAttachment"].ToString().ToUpper();

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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_PurchasingReceiving(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        string prevRFQ = string.Empty;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_PurchasingReceiving";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.Parameters.Add(Factory.CreateParameter("@search_criteria", entity.SearchCriteria = entity.SearchCriteria.Length <= 0 ? string.Empty : entity.SearchCriteria));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.RhRfqNo = reader["RFQNo"].ToString().ToUpper();
                entities.RhCategory = reader["CategoryName"].ToString().ToUpper();
                entities.RhTransactionDate = reader["TransactionDate"].ToString();

                entities.StatBuyer = reader["Buyer"] == DBNull.Value ? "0" : reader["Buyer"].ToString();
                entities.StatPurchasingIncharge = reader["PurchasingIncharge"] == DBNull.Value ? "0" : reader["PurchasingIncharge"].ToString();
                entities.StatDeptManager = reader["DepartmentManager"] == DBNull.Value ? "0" : reader["DepartmentManager"].ToString();
                entities.StatDivManager = reader["DivisionManager"] == DBNull.Value ? "0" : reader["DivisionManager"].ToString();

                if (reader["LocalNumber"] == DBNull.Value)
                {
                    entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true);
                }
                else
                {
                    entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true) + " (<b>" + reader["LocalNumber"].ToString() + "</b>)";
                }

                if (reader["cntSuppResp"] == DBNull.Value)
                {
                    entities.CntSuppResp = "0";
                }
                else
                {
                    entities.CntSuppResp = reader["cntSuppResp"].ToString().ToUpper();
                }

                if (reader["ProdManagerApprovedDate"] == DBNull.Value)
                {
                    entities.RhProdManagerApprovedDate = "";
                }
                else
                {
                    entities.RhProdManagerApprovedDate = reader["ProdManagerApprovedDate"].ToString();
                }
                entities.ResponseNumberOfResponded = reader["NumberOfResponded"].ToString();
                entities.GroupBySupplierResponse = int.Parse(reader["NumberOfResponded"].ToString()) > 0 ? "YES" : "NO";

                entities.RdDescription = reader["Description"].ToString().ToUpper();
                entities.RdSpecs = reader["Specs"].ToString().ToUpper();
                entities.RdAttachment = reader["HasAttachment"].ToString().ToUpper();

                if (prevRFQ.ToUpper() != reader["RFQNo"].ToString().ToUpper())
                {
                    list.Add(entities);
                }

                prevRFQ = entities.RhRfqNo;
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetRespondedSupplierByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_GetRespondedSupplierByRFQNo";
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", rfqNo = rfqNo.Length <= 0 ? string.Empty : rfqNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                entity.ResponseSupplierName = reader["SupplierName"].ToString();
                entity.ResponseResponseDate = reader["ResponseDate"].ToString();
                entity.ResponseSupplierID = reader["SupplierID"].ToString();
                entity.ResponseCount = reader["ResponseCount"].ToString();

                list.Add(entity);
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetRespondedSupplierByRFQNo_ForReceiving(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_GetRespondedSupplierByRFQNo_ForReceiving";
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", rfqNo = rfqNo.Length <= 0 ? string.Empty : rfqNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                entity.ResponseSupplierName = reader["SupplierName"].ToString();
                entity.ResponseResponseDate = reader["ResponseDate"].ToString();
                entity.ResponseSupplierID = reader["SupplierID"].ToString();
                entity.ResponseCount = reader["ResponseCount"].ToString();

                list.Add(entity);
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetSupplierResponseByRFQNoAndSupplierID(string rfqNo, string refid)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_GetSupplierResponseByRFQNoAndSupplierID";
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", rfqNo = rfqNo.Length <= 0 ? string.Empty : rfqNo));
            cmd.Parameters.Add(Factory.CreateParameter("@supplierid", refid = refid.Length <= 0 ? string.Empty : refid));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                entity.ResponseDescription = reader["Description"].ToString();
                entity.ResponseSpecs = reader["Specs"].ToString();
                entity.ResponseMaker = reader["Maker"].ToString();
                entity.ResponsePrice = reader["ResponsePrice"].ToString();
                entity.ResponseLead = reader["ResponseLead"].ToString();
                entity.ResponseRemarks = reader["Remarks"].ToString();

                list.Add(entity);
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetSupplierResponseByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        Common COMMON = new Common();
        int inum = 0;
        string oldRefid = string.Empty;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_GetSupplierResponseByRFQNo";
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", rfqNo = rfqNo.Length <= 0 ? string.Empty : rfqNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                entity.ResponseRefId = reader["RefId"].ToString();
                entity.ResponsePurchasingRemarks = reader["PurchasingRemarks"] == DBNull.Value ? string.Empty : reader["PurchasingRemarks"].ToString();
                entity.ResponseIsGranted = reader["isGranted"].ToString();
                entity.ResponseRFQNo = reader["RFQNo"].ToString();
                entity.ResponseSupplierName = reader["Name"].ToString();
                entity.ResponseSupplierID = reader["SupplierID"].ToString();
                entity.ResponseSupplierRemarks = reader["Remarks"] == DBNull.Value ? string.Empty : reader["Remarks"].ToString();
                entity.ResponseSupplierCurrency = reader["RCurrency"] == DBNull.Value ? string.Empty : reader["RCurrency"].ToString();
                entity.ResponseDescription = reader["Description"].ToString();
                entity.ResponseSpecs = reader["Specs"] == DBNull.Value ? string.Empty : reader["Specs"].ToString();
                entity.ResponseMaker = reader["Maker"] == DBNull.Value ? string.Empty : reader["Maker"].ToString();
                entity.ResponsePrice = reader["ResponsePrice"] == DBNull.Value ? string.Empty : COMMON.formatNumber(decimal.Parse(reader["ResponsePrice"].ToString()), 4);
                entity.ResponseLead = reader["ResponseLead"] == DBNull.Value ? string.Empty : reader["ResponseLead"].ToString();
                entity.RdRefId = reader["DetailsRefId"].ToString();
                entity.RdRemarks = reader["RequesterRemarks"] == DBNull.Value ? string.Empty : reader["RequesterRemarks"].ToString();

                if (oldRefid == reader["DetailsRefId"].ToString())
                {
                    entity.ItemNumber = inum.ToString();
                }
                else
                {
                    inum++;
                    entity.ItemNumber = inum.ToString();
                }

                oldRefid = reader["DetailsRefId"].ToString();
                list.Add(entity);
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetSupplierResponseByRFQNoSupplierNameOnly(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        Common COMMON = new Common();

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_GetSupplierResponseByRFQNoSupplierNameOnly";
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", rfqNo = rfqNo.Length <= 0 ? string.Empty : rfqNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                entity.ResponseSupplierName = reader["Name"].ToString();                

                list.Add(entity);
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


    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetSupplierForSendAndWithResponseByRFQNo(string rfqNo)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_GetSupplierForSendAndWithResponseByRFQNo";
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", rfqNo = rfqNo.Length <= 0 ? string.Empty : rfqNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entity = new Entities_RFQ_RequestEntry();

                entity.ResponseSupplierName = reader["Name"].ToString();

                if (reader["WithResponseSupplier"] == DBNull.Value)
                {
                    entity.ReponseWithResponse = "0";
                }
                else
                {
                    entity.ReponseWithResponse = reader["WithResponseSupplier"].ToString();
                }

                if (reader["HasResponse"] == DBNull.Value)
                {
                    entity.ResponseCount = "0";
                }
                else
                {
                    entity.ResponseCount = reader["HasResponse"].ToString();
                }

                entity.ResponseSupplierID = reader["RefId"].ToString();
                entity.ResponseSupplierEmail = reader["EmailAddress"].ToString();

                entity.Registered = reader["Registered"] != DBNull.Value ? reader["Registered"].ToString() : "";

                list.Add(entity);
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


    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Approval_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_Approval_ByDateRange";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                string prodManager = (reader["ProdManager"] != DBNull.Value ? reader["ProdManager"] : string.Empty).ToString();
                string purchasing = (reader["Purchasing"] != DBNull.Value ? reader["Purchasing"] : string.Empty).ToString();
                string buyer = (reader["Buyer"] != DBNull.Value ? reader["Buyer"] : string.Empty).ToString();
                string purchasingIncharge = (reader["PurchasingIncharge"] != DBNull.Value ? reader["PurchasingIncharge"] : string.Empty).ToString();
                string deptManager = (reader["DepartmentManager"] != DBNull.Value ? reader["DepartmentManager"] : string.Empty).ToString();
                string divManager = (reader["DivisionManager"] != DBNull.Value ? reader["DivisionManager"] : string.Empty).ToString();
                string supplier = (reader["Supplier"] != DBNull.Value ? reader["Supplier"] : string.Empty).ToString();

                entities.Rfqno = reader["RFQNo"].ToString().ToUpper();
                entities.CategoryName = reader["CategoryName"].ToString().ToUpper();
                entities.RhCategory = reader["Category"].ToString();
                entities.TransactionDate = reader["TransactionDate"].ToString();
                entities.ProdManagerStatus = reader["ProdManager"].ToString();
                entities.PurchasingStatus = purchasing;
                entities.BuyerStatus = buyer;
                entities.PurchasingInchargeStatus = purchasingIncharge;
                entities.DepartmentManagerStatus = deptManager;
                entities.DivisionManagerStatus = divManager;
                entities.RhDepartment = reader["Department"].ToString();
                entities.RhRequester = CryptorEngine.Decrypt(reader["FullName"].ToString(), true).ToUpper();
                entities.StatSupplier = supplier;

                if (prodManager == "0")
                {
                    entities.StatAll = "FOR PRODUCTION MANAGER APPROVAL";
                    entities.CssColorCode = "#f44336";
                }
                if (prodManager == "1" && purchasing == "0")
                {
                    entities.StatAll = "FOR BUYER APPROVAL";
                    entities.CssColorCode = "#9C27B0";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "3")
                {
                    entities.StatAll = "QUOTATION SENT TO SUPPLIER(S)";
                    entities.CssColorCode = "#673AB7";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "8")
                {
                    entities.StatAll = "SUPPLIER RESPONDED / FOR BUYER APPROVAL";
                    entities.CssColorCode = "#009688";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1")
                {
                    entities.StatAll = "FOR PURCHASING INCHARGE APPROVAL";
                    entities.CssColorCode = "#8BC34A";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1")
                {
                    entities.StatAll = "FOR PURCHASING DEPARTMENT MANAGER APPROVAL";
                    entities.CssColorCode = "#CDDC39";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1")
                {
                    entities.StatAll = "FOR PURCHASING DIVISION MANAGER APPROVAL";
                    entities.CssColorCode = "#E91E63";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1" && divManager == "1")
                {
                    entities.StatAll = "APPROVED";
                    entities.CssColorCode = "#00C851";
                }
                if (prodManager == "2" || purchasing == "2" || buyer == "2" || purchasingIncharge == "2" || deptManager == "2" || divManager == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
                }

                entities.Requester = CryptorEngine.Decrypt(reader["FullName"].ToString(), true);
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Approval(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_Approval";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                string prodManager = (reader["ProdManager"] != DBNull.Value ? reader["ProdManager"] : string.Empty).ToString();
                string purchasing = (reader["Purchasing"] != DBNull.Value ? reader["Purchasing"] : string.Empty).ToString();
                string buyer = (reader["Buyer"] != DBNull.Value ? reader["Buyer"] : string.Empty).ToString();
                string purchasingIncharge = (reader["PurchasingIncharge"] != DBNull.Value ? reader["PurchasingIncharge"] : string.Empty).ToString();
                string deptManager = (reader["DepartmentManager"] != DBNull.Value ? reader["DepartmentManager"] : string.Empty).ToString();
                string divManager = (reader["DivisionManager"] != DBNull.Value ? reader["DivisionManager"] : string.Empty).ToString();
                string supplier = (reader["Supplier"] != DBNull.Value ? reader["Supplier"] : string.Empty).ToString();

                entities.Rfqno = reader["RFQNo"].ToString().ToUpper();
                entities.CategoryName = reader["CategoryName"].ToString().ToUpper();
                entities.RhCategory = reader["Category"].ToString();
                entities.TransactionDate = reader["TransactionDate"].ToString();
                entities.ProdManagerStatus = reader["ProdManager"].ToString();
                entities.PurchasingStatus = purchasing;
                entities.BuyerStatus = buyer;
                entities.PurchasingInchargeStatus = purchasingIncharge;
                entities.DepartmentManagerStatus = deptManager;
                entities.DivisionManagerStatus = divManager;
                entities.RhDepartment = reader["Department"].ToString();
                entities.RhDivision = reader["Division"].ToString();
                entities.RhRequester = CryptorEngine.Decrypt(reader["FullName"].ToString(), true).ToUpper();
                entities.RhRequesterId = reader["Requester"].ToString();
                entities.StatSupplier = supplier;                

                if (prodManager == "0")
                {
                    entities.StatAll = "FOR PRODUCTION MANAGER APPROVAL";
                    entities.CssColorCode = "#f44336";
                }
                if (prodManager == "1" && purchasing == "0")
                {
                    entities.StatAll = "FOR BUYER APPROVAL";
                    entities.CssColorCode = "#9C27B0";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "3")
                {
                    entities.StatAll = "QUOTATION SENT TO SUPPLIER(S)";
                    entities.CssColorCode = "#673AB7";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "8")
                {
                    entities.StatAll = "SUPPLIER RESPONDED / FOR BUYER APPROVAL";
                    entities.CssColorCode = "#009688";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1")
                {
                    entities.StatAll = "FOR PURCHASING INCHARGE APPROVAL";
                    entities.CssColorCode = "#8BC34A";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1")
                {
                    entities.StatAll = "FOR PURCHASING DEPARTMENT MANAGER APPROVAL";
                    entities.CssColorCode = "#CDDC39";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1")
                {
                    entities.StatAll = "FOR PURCHASING DIVISION MANAGER APPROVAL";
                    entities.CssColorCode = "#E91E63";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1" && divManager == "1")
                {
                    entities.StatAll = "APPROVED";
                    entities.CssColorCode = "#00C851";
                }
                if (prodManager == "2" || purchasing == "2" || buyer == "2" || purchasingIncharge == "2" || deptManager == "2" || divManager == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
                }

                entities.Requester = CryptorEngine.Decrypt(reader["FullName"].ToString(), true);
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Approval_Purchasing_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_Approval_Purchasing_ByDateRange";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                string prodManager = (reader["ProdManager"] != DBNull.Value ? reader["ProdManager"] : string.Empty).ToString();
                string purchasing = (reader["Purchasing"] != DBNull.Value ? reader["Purchasing"] : string.Empty).ToString();
                string buyer = (reader["Buyer"] != DBNull.Value ? reader["Buyer"] : string.Empty).ToString();
                string purchasingIncharge = (reader["PurchasingIncharge"] != DBNull.Value ? reader["PurchasingIncharge"] : string.Empty).ToString();
                string deptManager = (reader["DepartmentManager"] != DBNull.Value ? reader["DepartmentManager"] : string.Empty).ToString();
                string divManager = (reader["DivisionManager"] != DBNull.Value ? reader["DivisionManager"] : string.Empty).ToString();
                string supplier = (reader["Supplier"] != DBNull.Value ? reader["Supplier"] : string.Empty).ToString();

                entities.Rfqno = reader["RFQNo"].ToString().ToUpper();
                entities.CategoryName = reader["CategoryName"].ToString().ToUpper();
                entities.RhCategory = reader["Category"].ToString();
                entities.TransactionDate = reader["TransactionDate"].ToString();
                entities.ProdManagerStatus = reader["ProdManager"].ToString();
                entities.PurchasingStatus = string.IsNullOrEmpty(purchasing) ? "0" : purchasing;
                entities.BuyerStatus = string.IsNullOrEmpty(buyer) ? "0" : buyer;
                entities.PurchasingInchargeStatus = string.IsNullOrEmpty(purchasingIncharge) ? "0" : purchasingIncharge;
                entities.DepartmentManagerStatus = string.IsNullOrEmpty(deptManager) ? "0" : deptManager;
                entities.DivisionManagerStatus = string.IsNullOrEmpty(divManager) ? "0" : divManager;
                entities.RhDepartment = reader["Department"].ToString();
                entities.RhRequester = CryptorEngine.Decrypt(reader["FullName"].ToString(), true).ToUpper();
                entities.StatSupplier = supplier;

                if (prodManager == "0")
                {
                    entities.StatAll = "FOR PRODUCTION MANAGER APPROVAL";
                    entities.CssColorCode = "#f44336";
                }
                if (prodManager == "1" && purchasing == "0")
                {
                    entities.StatAll = "FOR BUYER APPROVAL";
                    entities.CssColorCode = "#9C27B0";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "3")
                {
                    entities.StatAll = "QUOTATION SENT TO SUPPLIER(S)";
                    entities.CssColorCode = "#673AB7";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "8")
                {
                    entities.StatAll = "SUPPLIER RESPONDED / FOR BUYER APPROVAL";
                    entities.CssColorCode = "#009688";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1")
                {
                    entities.StatAll = "FOR PURCHASING INCHARGE APPROVAL";
                    entities.CssColorCode = "#8BC34A";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1")
                {
                    entities.StatAll = "FOR PURCHASING DEPARTMENT MANAGER APPROVAL";
                    entities.CssColorCode = "#CDDC39";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1")
                {
                    entities.StatAll = "FOR PURCHASING DIVISION MANAGER APPROVAL";
                    entities.CssColorCode = "#E91E63";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1" && divManager == "1")
                {
                    entities.StatAll = "APPROVED";
                    entities.CssColorCode = "#00C851";
                }
                if (prodManager == "2" || purchasing == "2" || buyer == "2" || purchasingIncharge == "2" || deptManager == "2" || divManager == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
                }

                entities.Requester = CryptorEngine.Decrypt(reader["FullName"].ToString(), true);
                entities.GroupBySupplierResponse = int.Parse(reader["NumberOfResponded"].ToString()) > 0 ? "YES" : "NO";
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Approval_Purchasing(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        string prevRFQ = string.Empty;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_Approval_Purchasing";
            cmd.Parameters.Add(Factory.CreateParameter("@search_criteria", entity.SearchCriteria = entity.SearchCriteria.Length <= 0 ? string.Empty : entity.SearchCriteria));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                string prodManager = (reader["ProdManager"] != DBNull.Value ? reader["ProdManager"] : string.Empty).ToString();
                string purchasing = (reader["Purchasing"] != DBNull.Value ? reader["Purchasing"] : string.Empty).ToString();
                string buyer = (reader["Buyer"] != DBNull.Value ? reader["Buyer"] : string.Empty).ToString();
                string purchasingIncharge = (reader["PurchasingIncharge"] != DBNull.Value ? reader["PurchasingIncharge"] : string.Empty).ToString();
                string deptManager = (reader["DepartmentManager"] != DBNull.Value ? reader["DepartmentManager"] : string.Empty).ToString();
                string divManager = (reader["DivisionManager"] != DBNull.Value ? reader["DivisionManager"] : string.Empty).ToString();
                string supplier = (reader["Supplier"] != DBNull.Value ? reader["Supplier"] : string.Empty).ToString();

                entities.Rfqno = reader["RFQNo"].ToString().ToUpper();
                entities.CategoryName = reader["CategoryName"].ToString().ToUpper();
                entities.DateReceivedFromSupplier = reader["DateReceivedFromSupplier"].ToString().ToUpper();
                entities.RhCategory = reader["Category"].ToString();
                entities.TransactionDate = reader["TransactionDate"].ToString();
                entities.ProdManagerStatus = reader["ProdManager"].ToString();
                entities.PurchasingStatus = string.IsNullOrEmpty(purchasing) ? "0" : purchasing;
                entities.BuyerStatus = string.IsNullOrEmpty(buyer) ? "0" : buyer;
                entities.PurchasingInchargeStatus = string.IsNullOrEmpty(purchasingIncharge) ? "0" : purchasingIncharge;
                entities.DepartmentManagerStatus = string.IsNullOrEmpty(deptManager) ? "0" : deptManager;
                entities.DivisionManagerStatus = string.IsNullOrEmpty(divManager) ? "0" : divManager;
                entities.RhDepartment = reader["Department"].ToString();
                entities.RhRequester = CryptorEngine.Decrypt(reader["FullName"].ToString(), true).ToUpper();
                entities.StatSupplier = supplier;
                entities.RdDescription = reader["Description"].ToString().ToUpper();
                entities.RdSpecs = reader["Specs"].ToString().ToUpper();
                entities.NumberOfSuppliers_WithResponse = reader["NumberOfSupplierWithResponse"].ToString().ToUpper() + " / " + reader["RewardedSupplier"].ToString().ToUpper();

                entities.RhProdManagerApprovedDate = (reader["ProdManagerApprovedDate"] != DBNull.Value ? reader["ProdManagerApprovedDate"] : string.Empty).ToString();

                if (prodManager == "0")
                {
                    entities.StatAll = "FOR PRODUCTION MANAGER APPROVAL";
                    entities.CssColorCode = "#f44336";
                }
                if (prodManager == "1" && purchasing == "0")
                {
                    entities.StatAll = "FOR BUYER APPROVAL";
                    entities.CssColorCode = "#9C27B0";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "3")
                {
                    entities.StatAll = "QUOTATION SENT TO SUPPLIER(S)";
                    entities.CssColorCode = "#673AB7";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "8")
                {
                    entities.StatAll = "SUPPLIER RESPONDED / FOR BUYER APPROVAL";
                    entities.CssColorCode = "#009688";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1")
                {
                    entities.StatAll = "FOR PURCHASING INCHARGE APPROVAL";
                    entities.CssColorCode = "#8BC34A";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1")
                {
                    entities.StatAll = "FOR PURCHASING DEPARTMENT MANAGER APPROVAL";
                    entities.CssColorCode = "#CDDC39";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1")
                {
                    entities.StatAll = "FOR PURCHASING DIVISION MANAGER APPROVAL";
                    entities.CssColorCode = "#E91E63";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1" && divManager == "1")
                {
                    entities.StatAll = "APPROVED";
                    entities.CssColorCode = "#00C851";
                }
                if (prodManager == "2" || purchasing == "2" || buyer == "2" || purchasingIncharge == "2" || deptManager == "2" || divManager == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
                }

                entities.Requester = CryptorEngine.Decrypt(reader["FullName"].ToString(), true);
                entities.GroupBySupplierResponse = int.Parse(reader["NumberOfResponded"].ToString()) > 0 ? "YES" : "NO";


                if (prevRFQ.ToUpper() != reader["RFQNo"].ToString().ToUpper())
                {
                    list.Add(entities);
                }

                prevRFQ = entities.Rfqno;                
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        string prevRFQ = string.Empty;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_Approval_Purchasing_RFQWithNoResponse";
            cmd.Parameters.Add(Factory.CreateParameter("@search_criteria", entity.SearchCriteria = entity.SearchCriteria.Length <= 0 ? string.Empty : entity.SearchCriteria));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                string prodManager = (reader["ProdManager"] != DBNull.Value ? reader["ProdManager"] : string.Empty).ToString();
                string purchasing = (reader["Purchasing"] != DBNull.Value ? reader["Purchasing"] : string.Empty).ToString();
                string buyer = (reader["Buyer"] != DBNull.Value ? reader["Buyer"] : string.Empty).ToString();
                string purchasingIncharge = (reader["PurchasingIncharge"] != DBNull.Value ? reader["PurchasingIncharge"] : string.Empty).ToString();
                string deptManager = (reader["DepartmentManager"] != DBNull.Value ? reader["DepartmentManager"] : string.Empty).ToString();
                string divManager = (reader["DivisionManager"] != DBNull.Value ? reader["DivisionManager"] : string.Empty).ToString();
                string supplier = (reader["Supplier"] != DBNull.Value ? reader["Supplier"] : string.Empty).ToString();

                entities.Rfqno = reader["RFQNo"].ToString().ToUpper();
                entities.CategoryName = reader["CategoryName"].ToString().ToUpper();
                entities.RhCategory = reader["Category"].ToString();
                entities.TransactionDate = reader["TransactionDate"].ToString();
                entities.ProdManagerStatus = reader["ProdManager"].ToString();
                entities.PurchasingStatus = string.IsNullOrEmpty(purchasing) ? "0" : purchasing;
                entities.BuyerStatus = string.IsNullOrEmpty(buyer) ? "0" : buyer;
                entities.PurchasingInchargeStatus = string.IsNullOrEmpty(purchasingIncharge) ? "0" : purchasingIncharge;
                entities.DepartmentManagerStatus = string.IsNullOrEmpty(deptManager) ? "0" : deptManager;
                entities.DivisionManagerStatus = string.IsNullOrEmpty(divManager) ? "0" : divManager;
                entities.RhDepartment = reader["Department"].ToString();
                entities.RhRequester = CryptorEngine.Decrypt(reader["FullName"].ToString(), true).ToUpper();
                entities.StatSupplier = supplier;
                entities.RdDescription = reader["Description"].ToString().ToUpper();
                entities.RdSpecs = reader["Specs"].ToString().ToUpper();
                entities.NumberOfSuppliers_WithResponse = reader["NumberOfSupplierWithResponse"].ToString().ToUpper();
                entities.RdRemarks = reader["Remarks"] != DBNull.Value ? reader["Remarks"].ToString().ToUpper() : string.Empty;
                entities.RdMaker = reader["Maker"] != DBNull.Value ? reader["Maker"].ToString().ToUpper() : string.Empty;
                entities.RhProdManagerApprovedDate = reader["ProdManagerDOA"] != DBNull.Value ? reader["ProdManagerDOA"].ToString().ToUpper() : string.Empty;

                if (prodManager == "0")
                {
                    entities.StatAll = "FOR PRODUCTION MANAGER APPROVAL";
                    entities.CssColorCode = "#f44336";
                }
                if (prodManager == "1" && purchasing == "0")
                {
                    entities.StatAll = "FOR BUYER APPROVAL";
                    entities.CssColorCode = "#9C27B0";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "3")
                {
                    entities.StatAll = "QUOTATION SENT TO SUPPLIER(S)";
                    entities.CssColorCode = "#673AB7";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "8")
                {
                    entities.StatAll = "SUPPLIER RESPONDED / FOR BUYER APPROVAL";
                    entities.CssColorCode = "#009688";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1")
                {
                    entities.StatAll = "FOR PURCHASING INCHARGE APPROVAL";
                    entities.CssColorCode = "#8BC34A";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1")
                {
                    entities.StatAll = "FOR PURCHASING DEPARTMENT MANAGER APPROVAL";
                    entities.CssColorCode = "#CDDC39";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1")
                {
                    entities.StatAll = "FOR PURCHASING DIVISION MANAGER APPROVAL";
                    entities.CssColorCode = "#E91E63";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1" && divManager == "1")
                {
                    entities.StatAll = "APPROVED";
                    entities.CssColorCode = "#00C851";
                }
                if (prodManager == "2" || purchasing == "2" || buyer == "2" || purchasingIncharge == "2" || deptManager == "2" || divManager == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
                }

                entities.Requester = CryptorEngine.Decrypt(reader["FullName"].ToString(), true);
                entities.GroupBySupplierResponse = int.Parse(reader["NumberOfResponded"].ToString()) > 0 ? "YES" : "NO";


                if (prevRFQ.ToUpper() != reader["RFQNo"].ToString().ToUpper())
                {
                    list.Add(entities);
                }

                prevRFQ = entities.Rfqno;
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetSupplierAttachmentBySupplierIdAndRFQNo(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_GetSupplierAttachmentBySupplierIdAndRFQNo";
            cmd.Parameters.Add(Factory.CreateParameter("@supplierid", entity.ResponseSupplierID = entity.ResponseSupplierID.Length <= 0 ? string.Empty : entity.ResponseSupplierID));
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", entity.ResponseRFQNo = entity.ResponseRFQNo.Length <= 0 ? string.Empty : entity.ResponseRFQNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry attachment = new Entities_RFQ_RequestEntry();

                attachment.ResponseRFQNo = reader["RFQNo"].ToString();
                attachment.ResponseSupplierID = reader["SupplierID"].ToString();
                attachment.ResponseSupplierAttachment = reader["Attachment"].ToString();

                list.Add(attachment);
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetSupplierAttachmentByRFQNo(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_GetSupplierAttachmentByRFQNo";            
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", entity.ResponseRFQNo = entity.ResponseRFQNo.Length <= 0 ? string.Empty : entity.ResponseRFQNo));

            reader = cmd.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry attachment = new Entities_RFQ_RequestEntry();

                attachment.ResponseRFQNo = reader["RFQNo"].ToString();
                attachment.ResponseSupplierID = reader["SupplierID"].ToString();                
                attachment.ResponseSupplierName = reader["SupplierName"].ToString();
                attachment.ResponseSupplierAttachment = "<a href='/IO_Received/" + reader["SupplierID"].ToString() + "_" + reader["RFQNo"].ToString().ToString() + "/" + reader["Attachment"].ToString() + "' runat='server' id='" + counter.ToString() + "' target='_blank', 'height=500,width=500,modal=yes,alwaysRaised=yes'>" + reader["Attachment"].ToString() + "</a>";
                //attachment.ResponseSupplierAttachment = reader["Attachment"].ToString();
                //attachment.ResponseSupplierAttachment = "javascript:window.open('/IO_Received/" + reader["RFQNo"].ToString().ToString() + "/" + reader["Attachment"].ToString() + "', '_blank', 'height=500,width=500,modal=yes,alwaysRaised=yes')";
                list.Add(attachment);
                counter++;
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_GetApprovedSupplierAttachmentBySupplierIdAndRFQNo(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_GetApprovedSupplierAttachmentBySupplierIdAndRFQNo";            
            cmd.Parameters.Add(Factory.CreateParameter("@rfqno", entity.ResponseRFQNo = entity.ResponseRFQNo.Length <= 0 ? string.Empty : entity.ResponseRFQNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry attachment = new Entities_RFQ_RequestEntry();

                attachment.ResponseRFQNo = reader["RFQNo"].ToString();
                attachment.ResponseSupplierID = reader["SupplierID"].ToString();
                attachment.ResponseSupplierAttachment = reader["Attachment"].ToString();
                attachment.ResponseSupplierName = reader["SupplierName"].ToString();

                list.Add(attachment);
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_OnePage_Head(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_OnePage_Head";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                string prodManager = (reader["ProdManager"] != DBNull.Value ? reader["ProdManager"] : string.Empty).ToString();
                string purchasing = (reader["Purchasing"] != DBNull.Value ? reader["Purchasing"] : string.Empty).ToString();
                string buyer = (reader["Buyer"] != DBNull.Value ? reader["Buyer"] : string.Empty).ToString();
                string purchasingIncharge = (reader["PurchasingIncharge"] != DBNull.Value ? reader["PurchasingIncharge"] : string.Empty).ToString();
                string deptManager = (reader["DepartmentManager"] != DBNull.Value ? reader["DepartmentManager"] : string.Empty).ToString();
                string divManager = (reader["DivisionManager"] != DBNull.Value ? reader["DivisionManager"] : string.Empty).ToString();
                string supplier = (reader["Supplier"] != DBNull.Value ? reader["Supplier"] : string.Empty).ToString();

                if (prodManager == "0")
                {
                    entities.StatAll = "FOR PRODUCTION MANAGER APPROVAL";
                    entities.CssColorCode = "#f44336";
                }
                if (prodManager == "1" && purchasing == "0")
                {
                    entities.StatAll = "FOR BUYER APPROVAL";
                    entities.CssColorCode = "#9C27B0";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "3")
                {
                    entities.StatAll = "QUOTATION SENT TO SUPPLIER(S)";
                    entities.CssColorCode = "#673AB7";
                }
                if (prodManager == "1" && purchasing == "1" && supplier == "8")
                {
                    entities.StatAll = "SUPPLIER RESPONDED / FOR BUYER APPROVAL";
                    entities.CssColorCode = "#009688";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1")
                {
                    entities.StatAll = "FOR PURCHASING INCHARGE APPROVAL";
                    entities.CssColorCode = "#8BC34A";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1")
                {
                    entities.StatAll = "FOR PURCHASING DEPARTMENT MANAGER APPROVAL";
                    entities.CssColorCode = "#CDDC39";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1")
                {
                    entities.StatAll = "FOR PURCHASING DIVISION MANAGER APPROVAL";
                    entities.CssColorCode = "#E91E63";
                }
                if (prodManager == "1" && purchasing == "1" && buyer == "1" && purchasingIncharge == "1" && deptManager == "1" && divManager == "1")
                {
                    entities.StatAll = "APPROVED";
                    entities.CssColorCode = "#00C851";
                }
                if (prodManager == "2" || purchasing == "2" || buyer == "2" || purchasingIncharge == "2" || deptManager == "2" || divManager == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
                }


                entities.RhRfqNo = reader["RFQNo"].ToString().ToUpper().Trim();
                entities.RhCategory = reader["CategoryName"].ToString().ToUpper().Trim();
                entities.RhSection = reader["SectionName"].ToString().ToUpper().Trim();
                entities.RhDepartment = reader["DepartmentName"].ToString().ToUpper().Trim();
                entities.RhDivision = reader["DivisionName"].ToString().ToUpper().Trim();
                entities.RhRequester = CryptorEngine.Decrypt(reader["RequesterName"].ToString(), true).ToUpper().Trim();
                entities.RhTransactionDate = reader["TransactionDate"].ToString().ToUpper().Trim();


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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_OnePage_Head_Description(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_OnePage_Head_Description";
            cmd.Parameters.Add(Factory.CreateParameter("@criteria", entity.SearchCriteria = entity.SearchCriteria.Length <= 0 ? string.Empty : entity.SearchCriteria)); 

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.RdRfqNo = reader["RFQNo"].ToString().ToUpper().Trim();
                entities.RdDescription = reader["Description"].ToString().ToUpper().Trim();

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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_OnePage_Head_Specification(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_OnePage_Head_Specification";
            cmd.Parameters.Add(Factory.CreateParameter("@criteria", entity.SearchCriteria = entity.SearchCriteria.Length <= 0 ? string.Empty : entity.SearchCriteria));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.RdRfqNo = reader["RFQNo"].ToString().ToUpper().Trim();
                entities.RdSpecs = reader["Specs"].ToString().ToUpper().Trim();

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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_OnePage_Head_Maker(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_OnePage_Head_Maker";
            cmd.Parameters.Add(Factory.CreateParameter("@criteria", entity.SearchCriteria = entity.SearchCriteria.Length <= 0 ? string.Empty : entity.SearchCriteria));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.RdRfqNo = reader["RFQNo"].ToString().ToUpper().Trim();
                entities.RdMaker = reader["Maker"].ToString().ToUpper().Trim();

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

    public int RFQ_TRANSACTION_UpdateBuyerNotes(string buyerNotes, string rfqno)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "UPDATE Request_Head SET BuyerNotes = '" + buyerNotes + "' WHERE RFQNo = '" + rfqno + "'";

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


    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Reporting_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_Reporting_ByDateRange";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.CommandTimeout = 500;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.Report_Department_Name = reader["Department_Name"].ToString().ToUpper();
                entities.Report_Department_Total_Request = reader["Total_Request_Counts_BY_DEPARTMENT"].ToString();
                entities.Report_Department_Buyer_Approved = reader["Buyer_Approved_Counts_BY_DEPARTMENT"].ToString();
                entities.Report_Department_Buyer_Disapproved = reader["Buyer_Disapproved_Counts_BY_DEPARTMENT"].ToString();
                entities.Report_Department_PurManager_Approved = reader["Pur_Manager_Approved_Counts_BY_DEPARTMENT"].ToString();
                entities.Report_Department_PurManager_Disapproved = reader["Pur_Manager_Disapproved_Counts_BY_DEPARTMENT"].ToString();
                entities.Report_Department_Posted_Counts = reader["Posted_Counts_BY_DEPARTMENT"].ToString();
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Reporting_Sending_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT HEAD.RFQNo, CATEGORY.[Description] AS CategoryName, RHOA.ApprovedDate AS ProdManagerApprovedDate, " +
                                "HEAD.TransactionDate, LC.FullName AS Requester, LC.LocalNumber, " +
                                "DETAILS.[Description], DETAILS.Specs, DETAILS.Maker, [STATUS].ProdManager, [STATUS].Purchasing, [STATUS].Buyer, " +
                                "[STATUS].PurchasingInCharge, [STATUS].DepartmentManager, [STATUS].DivisionManager " +
                                "FROM Request_Head HEAD WITH (NOLOCK) " +
                                "INNER JOIN Request_Details DETAILS WITH (NOLOCK) ON HEAD.RFQNo = DETAILS.RFQNo " +
                                "INNER JOIN MT_Category CATEGORY WITH (NOLOCK) ON HEAD.Category = CATEGORY.RefId " +
                                "INNER JOIN vew_RequestHistOfApproval RHOA with (nolock) ON HEAD.RFQNo  = RHOA.RFQNo " +
                                "INNER JOIN Request_Status [STATUS] WITH (NOLOCK) ON HEAD.RFQNo = [STATUS].RFQNo " +
                                "INNER JOIN Login_Credentials LC WITH (NOLOCK) ON HEAD.Requester = LC.RefId " +
                                "WHERE [STATUS].Purchasing = 0 AND [STATUS].ProdManager = 1 " +
                                "AND CONVERT(DATE, HEAD.TransactionDate) >= '" + entity.DrFrom + "' AND CONVERT(DATE, HEAD.TransactionDate) <= '" + entity.DrTo + "'" +
                                "ORDER BY RHOA.ApprovedDate DESC";		



            cmd.CommandTimeout = 500;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.RdRfqNo = reader["RFQNo"] != DBNull.Value ? reader["RFQNo"].ToString().ToUpper() : string.Empty;
                entities.RdDescription = reader["Description"] != DBNull.Value ? reader["Description"].ToString().ToUpper() : string.Empty;
                entities.RdSpecs = reader["Specs"] != DBNull.Value ? reader["Specs"].ToString().ToUpper() : string.Empty;
                entities.RdMaker = reader["Maker"] != DBNull.Value ? reader["Maker"].ToString().ToUpper() : string.Empty;
                entities.RhCategory = reader["CategoryName"].ToString().ToUpper();
                entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString().Replace(" ", "+"), true);
                entities.RhTransactionDate = reader["TransactionDate"].ToString().ToUpper();
                entities.StatAll = "FOR SENDING";

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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Reporting_Resend_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT HEAD.RFQNo, CATEGORY.[Description] AS CategoryName, RHOA.ApprovedDate AS ProdManagerApprovedDate, " +
                                "HEAD.TransactionDate, LC.FullName AS Requester, LC.LocalNumber, " +
                                "DETAILS.Attachment AS RequesterAttachment, " +
                                "DETAILS.[Description], DETAILS.Specs, DETAILS.Maker, [STATUS].ProdManager, [STATUS].Purchasing, " +
                                "[STATUS].Buyer, [STATUS].PurchasingInCharge, [STATUS].DepartmentManager, [STATUS].DivisionManager " +
                                "FROM Request_Head HEAD WITH (NOLOCK) " +
                                "INNER JOIN Request_Details DETAILS WITH (NOLOCK) ON HEAD.RFQNo = DETAILS.RFQNo " +
                                "INNER JOIN MT_Category CATEGORY WITH (NOLOCK) ON HEAD.Category = CATEGORY.RefId " +
                                "INNER JOIN vew_RequestHistOfApproval RHOA with (nolock) ON HEAD.RFQNo  = RHOA.RFQNo " +
                                "INNER JOIN Request_Status [STATUS] WITH (NOLOCK) ON HEAD.RFQNo = [STATUS].RFQNo " +
                                "INNER JOIN Login_Credentials LC WITH (NOLOCK) ON HEAD.Requester = LC.RefId " +
                                "WHERE [STATUS].Purchasing = 1 " +
                                "AND [STATUS].Buyer = 0 " +
                                "AND ([STATUS].PurchasingInCharge = 0 OR [STATUS].PurchasingInCharge = 2) " +
                                "AND ([STATUS].DepartmentManager = 0 OR [STATUS].DepartmentManager = 2) " +
                                "AND ([STATUS].DivisionManager = 0 OR [STATUS].DivisionManager = 2) " +
                                "AND CONVERT(DATE, HEAD.TransactionDate) >= '" + entity.DrFrom + "' AND CONVERT(DATE, HEAD.TransactionDate) <= '" + entity.DrTo + "' " +
                                "ORDER BY RHOA.ApprovedDate DESC";



            cmd.CommandTimeout = 500;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.RdRfqNo = reader["RFQNo"] != DBNull.Value ? reader["RFQNo"].ToString().ToUpper() : string.Empty;
                entities.RdDescription = reader["Description"] != DBNull.Value ? reader["Description"].ToString().ToUpper() : string.Empty;
                entities.RdSpecs = reader["Specs"] != DBNull.Value ? reader["Specs"].ToString().ToUpper() : string.Empty;
                entities.RdMaker = reader["Maker"] != DBNull.Value ? reader["Maker"].ToString().ToUpper() : string.Empty;
                entities.RhCategory = reader["CategoryName"].ToString().ToUpper();
                entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString().Replace(" ", "+"), true);
                entities.RhTransactionDate = reader["TransactionDate"].ToString().ToUpper();
                entities.StatAll = "FOR RESEND";

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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Reporting_ALL_ByDateRange(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT HEAD.BuyerNotes, DETAILS.RFQNo, DETAILS.[Description], DETAILS.Specs, DETAILS.Maker, DETAILS.Remarks, STAT.ProdManager, " +
                                "STAT.Purchasing, STAT.Buyer, STAT.PurchasingInCharge, STAT.DepartmentManager, STAT.DivisionManager, STAT.Supplier, " +
                                "LC.FullName AS Requester, CATEGORY.[Description] AS CategoryName, HEAD.Category, HEAD.TransactionDate " +
                                "FROM Request_Details DETAILS WITH (NOLOCK) " +
                                "INNER JOIN Request_Head HEAD ON DETAILS.RFQNo = HEAD.RFQNo " +
                                "INNER JOIN MT_Category CATEGORY ON HEAD.Category = CATEGORY.RefId " +
                                "INNER JOIN Request_Status STAT ON HEAD.RFQNo = STAT.RFQNo " +
                                "INNER JOIN Login_Credentials LC ON HEAD.Requester = LC.RefId " +
                                "AND CONVERT(DATE, HEAD.TransactionDate) >= '" + entity.DrFrom + "' AND CONVERT(DATE, HEAD.TransactionDate) <= '" + entity.DrTo + "' " +
                                "ORDER BY HEAD.TransactionDate DESC";




            cmd.CommandTimeout = 500;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.RdRfqNo = reader["RFQNo"] != DBNull.Value ? reader["RFQNo"].ToString().ToUpper() : string.Empty;
                entities.RdDescription = reader["Description"] != DBNull.Value ? reader["Description"].ToString().ToUpper() : string.Empty;
                entities.RdSpecs = reader["Specs"] != DBNull.Value ? reader["Specs"].ToString().ToUpper() : string.Empty;
                entities.RdMaker = reader["Maker"] != DBNull.Value ? reader["Maker"].ToString().ToUpper() : string.Empty;
                entities.RhCategory = reader["CategoryName"].ToString().ToUpper();
                entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString().Replace(" ", "+"), true);
                entities.RhTransactionDate = reader["TransactionDate"].ToString().ToUpper();


                if (reader["DivisionManager"].ToString() == "0")
                {
                    entities.StatAll = "PENDING APPROVAL";
                }
                if (reader["DivisionManager"].ToString() == "2" || reader["DepartmentManager"].ToString() == "2" || reader["PurchasingInCharge"].ToString() == "2" || reader["Purchasing"].ToString() == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                }
                if (reader["DivisionManager"].ToString() == "1")
                {
                    entities.StatAll = "APPROVED";
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


    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Reporting_ByDateRange_ByDivision(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_Reporting_ByDateRange_ByDivision";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.CommandTimeout = 500;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.Report_Division_Name = reader["Division_Name"].ToString().ToUpper();
                entities.Report_Division_Total_Request = reader["Total_Request_Counts_BY_DIVISION"].ToString();
                entities.Report_Division_Buyer_Approved = reader["Buyer_Approved_Counts_BY_DIVISION"].ToString();
                entities.Report_Division_Buyer_Disapproved = reader["Buyer_Disapproved_Counts_BY_DIVISION"].ToString();
                entities.Report_Division_PurManager_Approved = reader["Pur_Manager_Approved_Counts_BY_DIVISION"].ToString();
                entities.Report_Division_PurManager_Disapproved = reader["Pur_Manager_Disapproved_Counts_BY_DIVISION"].ToString();
                entities.Report_Division_Posted_Counts = reader["Posted_Counts_BY_DIVISION"].ToString();
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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Reporting_ByDateRange_ByAll(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_Reporting_ByDateRange_ByAll";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.CommandTimeout = 500;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.Report_All_Total_Request = reader["Total_Request_Counts_BY_ALL"].ToString();
                entities.Report_All_Buyer_Approved = reader["Buyer_Approved_Counts_BY_ALL"].ToString();
                entities.Report_All_Buyer_Disapproved = reader["Buyer_Disapproved_Counts_BY_ALL"].ToString();
                entities.Report_All_PurManager_Approved = reader["Pur_Manager_Approved_Counts_BY_ALL"].ToString();
                entities.Report_All_PurManager_Disapproved = reader["Pur_Manager_Disapproved_Counts_BY_ALL"].ToString();
                entities.Report_All_Posted_Counts = reader["Posted_Counts_BY_ALL"].ToString();
                entities.Report_All_Pending_Approval = reader["Pending_Approval_Counts_BY_ALL"].ToString();
                entities.Report_All_Total_Approved = reader["Total_Posted_Counts_BY_ALL"].ToString();
                entities.Report_All_Total_Disapproved = reader["Total_Disapproved_Counts_BY_ALL"].ToString();

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

    public List<Entities_RFQ_RequestEntry> RFQ_TRANSACTION_Reporting_ByDateRange_Details(Entities_RFQ_RequestEntry entity)
    {
        List<Entities_RFQ_RequestEntry> list = new List<Entities_RFQ_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT HEAD.BuyerNotes, DETAILS.RFQNo, DETAILS.[Description], DETAILS.Specs, DETAILS.Maker, DETAILS.Remarks, STAT.ProdManager, " +
                                "STAT.Purchasing, STAT.Buyer, STAT.PurchasingInCharge, STAT.DepartmentManager, STAT.DivisionManager, STAT.Supplier, " +
                                "LC.FullName AS Requester, CATEGORY.[Description] AS CategoryName, HEAD.Category, HEAD.TransactionDate, " +
                                "(SELECT Description FROM MT_Section WITH (NOLOCK) WHERE RefId = LC.Section) AS SectionName, " +
                                "(SELECT Description FROM MT_Department WITH (NOLOCK) WHERE RefId = LC.Department) AS DepartmentName, " +
                                "(SELECT Description FROM MT_Division WITH (NOLOCK) WHERE RefId = LC.Division) AS DivisionName, " +
                                "(SELECT DISTINCT(SendDate) FROM Supplier_Rewarded WHERE RFQNo = DETAILS.RFQNo FOR XML PATH('')) AS SendDate, " +
                                "(SELECT TOP 1 ApprovedDate FROM Request_HistoryOfApproval WITH (NOLOCK) WHERE RFQNo = DETAILS.RFQNo AND TransactionName = 'PurchasingDivManager') AS ApprovedDate, " +
                                "(SELECT TOP 1 ApprovedDate FROM Request_HistoryOfApproval WITH (NOLOCK) WHERE RFQNo = DETAILS.RFQNo AND TransactionName = 'Purchasing') AS ApprovedDatePurchasing, " +
                                "(SELECT TOP 1 ApprovedDate FROM Request_HistoryOfApproval WITH (NOLOCK) WHERE RFQNo = DETAILS.RFQNo AND TransactionName = 'PurchasingBuyer') AS ApprovedDatePurchasingBuyer, " +
                                "(SELECT TOP 1 ApprovedDate FROM Request_HistoryOfApproval WITH (NOLOCK) WHERE RFQNo = DETAILS.RFQNo AND TransactionName = 'PurchasingIncharge') AS ApprovedDatePurchasingIncharge, " +
                                "(SELECT TOP 1 ApprovedDate FROM Request_HistoryOfApproval WITH (NOLOCK) WHERE RFQNo = DETAILS.RFQNo AND TransactionName = 'PurchasingDeptManager') AS ApprovedDatePurchasingDeptManager, " +
                                "(SELECT TOP 1 SendDate FROM Supplier_Rewarded WITH (NOLOCK) WHERE RFQNo = DETAILS.RFQNo ORDER BY RefId ASC) AS SendDateSingle, " +
                                "(SELECT TOP 1 DisapprovedDate FROM Request_HistoryOfDisapproval WITH (NOLOCK) WHERE RFQNo = DETAILS.RFQNo) AS DisapprovedDate " +
                                "FROM Request_Details DETAILS WITH (NOLOCK) " +
                                "LEFT JOIN Request_Head HEAD ON DETAILS.RFQNo = HEAD.RFQNo " +
                                "LEFT JOIN MT_Category CATEGORY ON HEAD.Category = CATEGORY.RefId " +
                                "LEFT JOIN Request_Status STAT ON HEAD.RFQNo = STAT.RFQNo " +
                                "LEFT JOIN Login_Credentials LC ON HEAD.Requester = LC.RefId " +
                                "WHERE CONVERT(DATE, HEAD.TransactionDate) >= '" + entity.DrFrom + "' AND CONVERT(DATE, HEAD.TransactionDate) <= '" + entity.DrTo + "' " +
                                "ORDER BY HEAD.RefId DESC";
            cmd.CommandTimeout = 500;


            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_RFQ_RequestEntry entities = new Entities_RFQ_RequestEntry();

                entities.Rfqno = reader["Rfqno"].ToString();
                entities.RdDescription = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty;
                entities.RdSpecs = reader["Specs"] != DBNull.Value ? reader["Specs"].ToString() : string.Empty;
                entities.RdMaker = reader["Maker"] != DBNull.Value ? reader["Maker"].ToString() : string.Empty;
                entities.Category = reader["CategoryName"] != DBNull.Value ? reader["CategoryName"].ToString() : string.Empty;
                entities.Requester = CryptorEngine.Decrypt(reader["Requester"].ToString().Replace(" ", "+"), true);
                entities.TransactionDate = reader["TransactionDate"].ToString();
                entities.DivisionManagerStatus = reader["DivisionManager"] != DBNull.Value ? reader["DivisionManager"].ToString() : "0";
                entities.PurchasingInchargeStatus = reader["PurchasingInCharge"] != DBNull.Value ? reader["PurchasingInCharge"].ToString() : "0";


                if (entities.DivisionManagerStatus == "1")
                {
                    entities.StatAll = "APPROVED";
                }
                if (string.IsNullOrEmpty(entities.DivisionManagerStatus) || entities.DivisionManagerStatus == "0")
                {
                    entities.StatAll = "PENDING APPROVAL";
                }
                if (entities.PurchasingInchargeStatus == "2" || entities.DivisionManagerStatus == "2")
                {
                    entities.StatAll = "DISAPPROVED";
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





    #endregion

}
