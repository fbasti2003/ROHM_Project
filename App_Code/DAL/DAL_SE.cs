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

public class DAL_SE
{
    public DAL_SE()
    {
    }


    #region FiscalYear

    public List<Entities_SE_FiscalYear> SE_MT_FiscalYear_GetAll()
    {
        List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SE_MT_FiscalYear_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_SE_FiscalYear entities = new Entities_SE_FiscalYear();

                entities.RefId = reader["RefId"].ToString();
                entities.From = reader["FY_From"].ToString();
                entities.To = reader["FY_To"].ToString();
                entities.Description = reader["FY_Description"].ToString();
                entities.Addedby = reader["AddedBy"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.Isdisabled = "0";
                }
                else
                {
                    entities.Isdisabled = reader["IsDisabled"].ToString();
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

    public int SE_MT_FiscalYear_IsDisabled(Entities_SE_FiscalYear entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SE_MT_FiscalYear_IsDisabled";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@isdisabled", entity.Isdisabled));
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

    public int SE_MT_FiscalYear_Insert(Entities_SE_FiscalYear entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SE_MT_FiscalYear_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@fyfrom", entity.From));
        cmd.Parameters.Add(Factory.CreateParameter("@fyto", entity.To));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
        cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.Addedby));

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

    public int SE_MT_FiscalYear_Append(Entities_SE_FiscalYear entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SE_MT_FiscalYear_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@fyfrom", entity.From));
        cmd.Parameters.Add(Factory.CreateParameter("@fyto", entity.To));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.Updatedby));

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

    public List<Entities_SE_FiscalYear> SE_MT_FiscalYear_GetByDescription(string description)
    {
        List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SE_MT_FiscalYear_GetByDescription";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_SE_FiscalYear entities = new Entities_SE_FiscalYear();

                entities.RefId = reader["RefId"].ToString();
                entities.From = reader["FY_From"].ToString();
                entities.To = reader["FY_To"].ToString();
                entities.Description = reader["FY_Description"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.Isdisabled = "0";
                }
                else
                {
                    entities.Isdisabled = reader["IsDisabled"].ToString();
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

    public List<Entities_SE_FiscalYear> SE_MT_FiscalYear_GetByDescription_Like(string description)
    {
        List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SE_MT_FiscalYear_GetByDescription_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@description", description));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_SE_FiscalYear entities = new Entities_SE_FiscalYear();

                entities.RefId = reader["RefId"].ToString();
                entities.From = reader["FY_From"].ToString();
                entities.To = reader["FY_To"].ToString();
                entities.Description = reader["FY_Description"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.Isdisabled = "0";
                }
                else
                {
                    entities.Isdisabled = reader["IsDisabled"].ToString();
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


    #region Evaluation Item

    public List<Entities_SE_EvaluationItem> SE_MT_EvaluationItem_GetAll()
    {
        List<Entities_SE_EvaluationItem> list = new List<Entities_SE_EvaluationItem>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SE_MT_EvaluationItem_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_SE_EvaluationItem entities = new Entities_SE_EvaluationItem();

                entities.RefId = reader["RefId"].ToString();
                entities.Item = reader["Item"].ToString();
                entities.Type = reader["Type"].ToString();              

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


    #region Evaluation Criteria For Maker

    public List<Entities_SE_EvaluationCriteria_Maker> SE_MT_EvaluationCriteria_Maker_GetAll()
    {
        List<Entities_SE_EvaluationCriteria_Maker> list = new List<Entities_SE_EvaluationCriteria_Maker>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SE_MT_EvaluationCriteria_Maker_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_SE_EvaluationCriteria_Maker entities = new Entities_SE_EvaluationCriteria_Maker();

                entities.RefId = reader["RefId"].ToString();
                entities.Item = reader["Item"].ToString();
                entities.ItemName = reader["ItemName"].ToString();
                entities.Criteria = reader["Criteria"].ToString();
                entities.Points = reader["Points"].ToString();
                entities.Judgement = reader["Judgement"].ToString();
                entities.Addedby = reader["AddedBy"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.Isdisabled = "0";
                }
                else
                {
                    entities.Isdisabled = reader["IsDisabled"].ToString();
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


    public int SE_MT_EvaluationCriteria_Maker_Insert(Entities_SE_EvaluationCriteria_Maker entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SE_MT_EvaluationCriteria_Maker_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@item", entity.Item));
        cmd.Parameters.Add(Factory.CreateParameter("@criteria", entity.Criteria));
        cmd.Parameters.Add(Factory.CreateParameter("@points", entity.Points));
        cmd.Parameters.Add(Factory.CreateParameter("@judgement", entity.Judgement));
        cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.Addedby));

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

    public int SE_MT_EvaluationCriteria_Maker_Append(Entities_SE_EvaluationCriteria_Maker entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SE_MT_EvaluationCriteria_Maker_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@item", entity.Item));
        cmd.Parameters.Add(Factory.CreateParameter("@criteria", entity.Criteria));
        cmd.Parameters.Add(Factory.CreateParameter("@points", entity.Points));
        cmd.Parameters.Add(Factory.CreateParameter("@judgement", entity.Judgement));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.Updatedby));

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

    public int SE_MT_EvaluationCriteria_Maker_IsDisabled(Entities_SE_EvaluationCriteria_Maker entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SE_MT_EvaluationCriteria_Maker_IsDisabled";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@isdisabled", entity.Isdisabled));
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


    #endregion


    #region Evaluation Criteria For Trader

    public List<Entities_SE_EvaluationCriteria_Trader> SE_MT_EvaluationCriteria_Trader_GetAll()
    {
        List<Entities_SE_EvaluationCriteria_Trader> list = new List<Entities_SE_EvaluationCriteria_Trader>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SE_MT_EvaluationCriteria_Trader_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_SE_EvaluationCriteria_Trader entities = new Entities_SE_EvaluationCriteria_Trader();

                entities.RefId = reader["RefId"].ToString();
                entities.Item = reader["Item"].ToString();
                entities.ItemName = reader["ItemName"].ToString();
                entities.Criteria = reader["Criteria"].ToString();
                entities.Points = reader["Points"].ToString();
                entities.Judgement = reader["Judgement"].ToString();
                entities.Addedby = reader["AddedBy"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.Isdisabled = "0";
                }
                else
                {
                    entities.Isdisabled = reader["IsDisabled"].ToString();
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


    public int SE_MT_EvaluationCriteria_Trader_Insert(Entities_SE_EvaluationCriteria_Trader entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SE_MT_EvaluationCriteria_Trader_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@item", entity.Item));
        cmd.Parameters.Add(Factory.CreateParameter("@criteria", entity.Criteria));
        cmd.Parameters.Add(Factory.CreateParameter("@points", entity.Points));
        cmd.Parameters.Add(Factory.CreateParameter("@judgement", entity.Judgement));
        cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.Addedby));

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

    public int SE_MT_EvaluationCriteria_Trader_Append(Entities_SE_EvaluationCriteria_Trader entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SE_MT_EvaluationCriteria_Trader_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@item", entity.Item));
        cmd.Parameters.Add(Factory.CreateParameter("@criteria", entity.Criteria));
        cmd.Parameters.Add(Factory.CreateParameter("@points", entity.Points));
        cmd.Parameters.Add(Factory.CreateParameter("@judgement", entity.Judgement));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.Updatedby));

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

    public int SE_MT_EvaluationCriteria_Trader_IsDisabled(Entities_SE_EvaluationCriteria_Trader entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SE_MT_EvaluationCriteria_Trader_IsDisabled";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@isdisabled", entity.Isdisabled));
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


    #endregion


    #region Evaluation Criteria For Material

    public List<Entities_SE_EvaluationCriteria_ForMaterial> SE_MT_EvaluationCriteria_ForMaterial_GetAll()
    {
        List<Entities_SE_EvaluationCriteria_ForMaterial> list = new List<Entities_SE_EvaluationCriteria_ForMaterial>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SE_MT_EvaluationCriteria_ForMaterial_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_SE_EvaluationCriteria_ForMaterial entities = new Entities_SE_EvaluationCriteria_ForMaterial();

                entities.RefId = reader["RefId"].ToString();
                entities.Item = reader["Item"].ToString();
                entities.ItemName = reader["ItemName"].ToString();
                entities.Criteria = reader["Criteria"].ToString();
                entities.Points = reader["Points"].ToString();
                entities.Weight = reader["Weight"].ToString();
                entities.Score = reader["Score"].ToString();
                entities.Addedby = reader["AddedBy"].ToString();
                entities.Level = reader["Lvl"].ToString();
                if (reader["IsDisabled"] == DBNull.Value)
                {
                    entities.Isdisabled = "0";
                }
                else
                {
                    entities.Isdisabled = reader["IsDisabled"].ToString();
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


    public int SE_MT_EvaluationCriteria_ForMaterial_Insert(Entities_SE_EvaluationCriteria_ForMaterial entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SE_MT_EvaluationCriteria_ForMaterial_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@item", entity.Item));
        cmd.Parameters.Add(Factory.CreateParameter("@criteria", entity.Criteria));
        cmd.Parameters.Add(Factory.CreateParameter("@points", entity.Points));
        cmd.Parameters.Add(Factory.CreateParameter("@weight", entity.Weight));
        cmd.Parameters.Add(Factory.CreateParameter("@score", entity.Score));
        cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.Addedby));
        cmd.Parameters.Add(Factory.CreateParameter("@level", entity.Level));

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

    public int SE_MT_EvaluationCriteria_ForMaterial_Append(Entities_SE_EvaluationCriteria_ForMaterial entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SE_MT_EvaluationCriteria_ForMaterial_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@item", entity.Item));
        cmd.Parameters.Add(Factory.CreateParameter("@criteria", entity.Criteria));
        cmd.Parameters.Add(Factory.CreateParameter("@points", entity.Points));
        cmd.Parameters.Add(Factory.CreateParameter("@weight", entity.Weight));
        cmd.Parameters.Add(Factory.CreateParameter("@score", entity.Score));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.Updatedby));
        cmd.Parameters.Add(Factory.CreateParameter("@level", entity.Level));

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

    public int SE_MT_EvaluationCriteria_ForMaterial_IsDisabled(Entities_SE_EvaluationCriteria_ForMaterial entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SE_MT_EvaluationCriteria_ForMaterial_IsDisabled";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
        cmd.Parameters.Add(Factory.CreateParameter("@isdisabled", entity.Isdisabled));
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


    #endregion


    #region Supplier Evaluation Request Entry

    public int SE_TRANSACTION_RequestHead_Insert(Entities_SE_RequestEntry entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SE_TRANSACTION_RequestHead_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@item", entity.FiscalYear));
        cmd.Parameters.Add(Factory.CreateParameter("@criteria", entity.AddedBy));


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

    public List<Entities_SE_RequestEntry> SE_TRANSACTION_IsRequestExist_By_FiscalYear(string fiscalYear)
    {
        List<Entities_SE_RequestEntry> list = new List<Entities_SE_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SE_TRANSACTION_IsRequestExist_By_FiscalYear";

            cmd.Parameters.Add(Factory.CreateParameter("@fiscalyear", fiscalYear));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_SE_RequestEntry entities = new Entities_SE_RequestEntry();

                entities.FiscalYear = reader["FiscalYear"].ToString();

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

    public int SE_TRANSACTION_RequestEntry_Insert_SQLTransaction(string query)
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

    public List<Entities_SE_FiscalYear> SE_TRANSACTION_Monitoring_GetAll()
    {
        List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        string old_rec = string.Empty;
        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SE_TRANSACTION_Monitoring_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_SE_FiscalYear entities = new Entities_SE_FiscalYear();
                
                entities.RefId = reader["RefId"].ToString();
                entities.Description = reader["FY_Description"].ToString();
                entities.Receipient = reader["Receipient"].ToString();
                entities.Responded = reader["Responded"].ToString();
                entities.SupplierName = reader["SupplierName"].ToString();
                entities.SupplierId = reader["SupplierId"].ToString();
                entities.Fy_SupplierId = reader["Fy_SupplierId"].ToString();

                entities.Incharge = reader["Incharge"].ToString() + "<br/>" + reader["DoaIncharge"].ToString();
                entities.DoaIncharge = DateTime.Parse(reader["DoaIncharge"].ToString()).ToLongDateString();
                entities.SectionIncharge = reader["SectionIncharge"] != DBNull.Value ? reader["SectionIncharge"].ToString() + "<br/>" + reader["DoaSectionIncharge"].ToString() : "PENDING APPROVAL";
                entities.DoaSectionIncharge = reader["DoaSectionIncharge"] != DBNull.Value ? DateTime.Parse(reader["DoaSectionIncharge"].ToString()).ToLongDateString() : string.Empty;
                entities.DeptManager = reader["DeptManager"] != DBNull.Value ? reader["DeptManager"].ToString() + "<br/>" + reader["DoaDeptManager"].ToString() : "PENDING APPROVAL";
                entities.DoaDeptManager = reader["DoaDeptManager"] != DBNull.Value ? DateTime.Parse(reader["DoaDeptManager"].ToString()).ToLongDateString() : string.Empty;
                entities.DivManager = reader["DivManager"] != DBNull.Value ? reader["DivManager"].ToString() + "<br/>" + reader["DoaDivManager"].ToString() : "PENDING APPROVAL";
                entities.DoaDivManager = reader["DoaDivManager"] != DBNull.Value ? DateTime.Parse(reader["DoaDivManager"].ToString()).ToLongDateString() : string.Empty;
                entities.ForApproval = reader["ForApproval"] != DBNull.Value ? reader["ForApproval"].ToString() : "0";
                entities.StatSectionIncharge = reader["STATSectionIncharge"] != DBNull.Value ? reader["STATSectionIncharge"].ToString() : "0";
                entities.StatDeptManager = reader["STATDeptManager"] != DBNull.Value ? reader["STATDeptManager"].ToString() : "0";
                entities.StatDivManager = reader["STATDivManager"] != DBNull.Value ? reader["STATDivManager"].ToString() : "0";
                entities.Posted = reader["Posted"] != DBNull.Value ? reader["Posted"].ToString() : "0";
                entities.PostedBy = reader["PostedBy"] != DBNull.Value ? reader["PostedBy"].ToString() : string.Empty;
                entities.PostedDate = reader["PostedDate"] != DBNull.Value ? DateTime.Parse(reader["PostedDate"].ToString()).ToLongDateString() : string.Empty;
                entities.DisapprovalRemarks = reader["DisapprovedRemarks"] != DBNull.Value ? reader["DisapprovedRemarks"].ToString() : string.Empty;

                if (entities.SectionIncharge.ToUpper() == "PENDING APPROVAL")
                {
                    entities.Status = "FOR SECTION INCHARGE APPROVAL";
                }
                if (entities.DeptManager.ToUpper() == "PENDING APPROVAL" && entities.SectionIncharge.ToUpper() != "PENDING APPROVAL")
                {
                    entities.Status = "FOR DEPT. MANAGER APPROVAL";
                }
                if (entities.DivManager.ToUpper() == "PENDING APPROVAL" && entities.SectionIncharge.ToUpper() != "PENDING APPROVAL" && entities.DeptManager.ToUpper() != "PENDING APPROVAL")
                {
                    entities.Status = "FOR DIV. MANAGER APPROVAL";
                }
                if (entities.DivManager.ToUpper() != "PENDING APPROVAL" && entities.SectionIncharge.ToUpper() != "PENDING APPROVAL" && entities.DeptManager.ToUpper() != "PENDING APPROVAL")
                {
                    entities.Status = "APPROVED";
                }

                //if (old_rec != reader["FY_Description"].ToString())
                //{
                //    list.Add(entities);
                //}

                //old_rec = reader["FY_Description"].ToString();
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

    public List<Entities_SE_FiscalYear> SE_TRANSACTION_Monitoring_GetAll2()
    {
        List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        string old_rec = string.Empty;
        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SE_TRANSACTION_Monitoring_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_SE_FiscalYear entities = new Entities_SE_FiscalYear();

                entities.RefId = reader["RefId"].ToString();
                entities.Description = reader["FY_Description"].ToString();
                entities.Receipient = reader["Receipient"].ToString();
                entities.Responded = reader["Responded"].ToString();
                entities.SupplierName = reader["SupplierName"].ToString();
                entities.SupplierId = reader["SupplierId"].ToString();
                entities.Fy_SupplierId = reader["Fy_SupplierId"].ToString();

                entities.Incharge = reader["Incharge"].ToString() + "<br/>" + reader["DoaIncharge"].ToString();
                entities.DoaIncharge = DateTime.Parse(reader["DoaIncharge"].ToString()).ToLongDateString();
                entities.SectionIncharge = reader["SectionIncharge"] != DBNull.Value ? reader["SectionIncharge"].ToString() + "<br/>" + reader["DoaSectionIncharge"].ToString() : "PENDING APPROVAL";
                entities.DoaSectionIncharge = reader["DoaSectionIncharge"] != DBNull.Value ? DateTime.Parse(reader["DoaSectionIncharge"].ToString()).ToLongDateString() : string.Empty;
                entities.DeptManager = reader["DeptManager"] != DBNull.Value ? reader["DeptManager"].ToString() + "<br/>" + reader["DoaDeptManager"].ToString() : "PENDING APPROVAL";
                entities.DoaDeptManager = reader["DoaDeptManager"] != DBNull.Value ? DateTime.Parse(reader["DoaDeptManager"].ToString()).ToLongDateString() : string.Empty;
                entities.DivManager = reader["DivManager"] != DBNull.Value ? reader["DivManager"].ToString() + "<br/>" + reader["DoaDivManager"].ToString() : "PENDING APPROVAL";
                entities.DoaDivManager = reader["DoaDivManager"] != DBNull.Value ? DateTime.Parse(reader["DoaDivManager"].ToString()).ToLongDateString() : string.Empty;
                entities.ForApproval = reader["ForApproval"] != DBNull.Value ? reader["ForApproval"].ToString() : "0";
                entities.StatSectionIncharge = reader["STATSectionIncharge"] != DBNull.Value ? reader["STATSectionIncharge"].ToString() : "0";
                entities.StatDeptManager = reader["STATDeptManager"] != DBNull.Value ? reader["STATDeptManager"].ToString() : "0";
                entities.StatDivManager = reader["STATDivManager"] != DBNull.Value ? reader["STATDivManager"].ToString() : "0";
                entities.Posted = reader["Posted"] != DBNull.Value ? reader["Posted"].ToString() : "0";
                entities.PostedBy = reader["PostedBy"] != DBNull.Value ? reader["PostedBy"].ToString() : string.Empty;
                entities.PostedDate = reader["PostedDate"] != DBNull.Value ? DateTime.Parse(reader["PostedDate"].ToString()).ToLongDateString() : string.Empty;
                entities.DisapprovalRemarks = reader["DisapprovedRemarks"] != DBNull.Value ? reader["DisapprovedRemarks"].ToString() : string.Empty;

                if (entities.SectionIncharge.ToUpper() == "PENDING APPROVAL")
                {
                    entities.Status = "FOR SECTION INCHARGE APPROVAL";
                }
                if (entities.DeptManager.ToUpper() == "PENDING APPROVAL" && entities.SectionIncharge.ToUpper() != "PENDING APPROVAL")
                {
                    entities.Status = "FOR DEPT. MANAGER APPROVAL";
                }
                if (entities.DivManager.ToUpper() == "PENDING APPROVAL" && entities.SectionIncharge.ToUpper() != "PENDING APPROVAL" && entities.DeptManager.ToUpper() != "PENDING APPROVAL")
                {
                    entities.Status = "FOR DIV. MANAGER APPROVAL";
                }

                if (old_rec != reader["RefId"].ToString())
                {
                    list.Add(entities);
                }

                old_rec = reader["RefId"].ToString();
                //list.Add(entities);
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

    public List<Entities_SE_FiscalYear> SE_TRANSACTION_Monitoring_GetAll_ByFiscalYear(string fy)
    {
        List<Entities_SE_FiscalYear> list = new List<Entities_SE_FiscalYear>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SE_TRANSACTION_Monitoring_GetAll_ByFiscalYear";
            cmd.Parameters.Add(Factory.CreateParameter("@fy_year", fy));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_SE_FiscalYear entities = new Entities_SE_FiscalYear();

                entities.RefId = reader["RefId"].ToString();
                entities.Description = reader["FY_Description"].ToString();
                entities.Receipient = reader["Receipient"].ToString();
                entities.Responded = reader["Responded"].ToString();
                entities.Name = reader["Name"].ToString();
                entities.EvaluationEmail = reader["EvaluationEmail"].ToString();
                entities.StatDivManager = reader["STATDivManager"] != DBNull.Value ? reader["STATDivManager"].ToString() : string.Empty;
                entities.Fy_SupplierId = reader["Fy_SupplierId"].ToString();

                if (reader["FiscalYear"].ToString().Trim() == fy.Trim())
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

    public string SE_TRANSACTION_GetAlreadySend_By_Fy_SupplierId(string fy_supplierid)
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
            cmd.CommandText = "SELECT * FROM SE_TRANSACTION_SendReceived WITH (NOLOCK) WHERE FY_SupplierId ='" + fy_supplierid + "' AND TransactionType = 'SEND'";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                retVal = reader["FY_SupplierId"].ToString();
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

    public List<Entities_SE_RequestEntry> SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear(string fy)
    {
        List<Entities_SE_RequestEntry> list = new List<Entities_SE_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        double totalScore = 0;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear";
            cmd.Parameters.Add(Factory.CreateParameter("@fy_year", fy));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_SE_RequestEntry entities = new Entities_SE_RequestEntry();

                entities.DetailsRefId = reader["RefId"] != DBNull.Value ? reader["RefId"].ToString() : string.Empty;
                entities.FY_SupplierId = reader["FY_SupplierId"] != DBNull.Value ? reader["FY_SupplierId"].ToString() : string.Empty;
                entities.SupplierName = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : string.Empty;
                entities.AutomotiveRelated = reader["AutomotiveRelated"] != DBNull.Value ? reader["AutomotiveRelated"].ToString() : string.Empty;
                entities.Classification = reader["Classification"] != DBNull.Value ? reader["Classification"].ToString() : string.Empty;
                entities.ItemClassification = reader["ItemClassification"] != DBNull.Value ? reader["ItemClassification"].ToString() : string.Empty;
                entities.JudgementYearMonth = reader["JudgementYearMonth"] != DBNull.Value ? reader["JudgementYearMonth"].ToString() : string.Empty;
                entities.SubContractor = reader["SubContractor"] != DBNull.Value ? reader["SubContractor"].ToString() : string.Empty;
                entities.NoteworthyPoints = reader["NoteworthyPoints"] != DBNull.Value ? reader["NoteworthyPoints"].ToString() : string.Empty;
                entities.MakerName = reader["MakerName"] != DBNull.Value ? reader["MakerName"].ToString() : string.Empty;
                entities.ISO9001 = reader["ISO9001"] != DBNull.Value ? reader["ISO9001"].ToString() : string.Empty;
                entities.ISO14001 = reader["ISO14001"] != DBNull.Value ? reader["ISO14001"].ToString() : string.Empty;
                entities.IATF16949 = reader["IATF16949"] != DBNull.Value ? reader["IATF16949"].ToString() : string.Empty;
                entities.EI_FinancialAnalysis = reader["EI_FinancialAnalysis"] != DBNull.Value ? reader["EI_FinancialAnalysis"].ToString() : string.Empty;
                entities.EI_Quality = reader["EI_Quality"] != DBNull.Value ? reader["EI_Quality"].ToString() : string.Empty;
                entities.EI_CostResponse = reader["EI_CostResponse"] != DBNull.Value ? reader["EI_CostResponse"].ToString() : string.Empty;
                entities.EI_Delivery = reader["EI_Delivery"] != DBNull.Value ? reader["EI_Delivery"].ToString() : string.Empty;
                entities.EI_Cooperation = reader["EI_Cooperation"] != DBNull.Value ? reader["EI_Cooperation"].ToString() : string.Empty;
                entities.EI_CSR = reader["EI_CSR"] != DBNull.Value ? reader["EI_CSR"].ToString() : string.Empty;

                entities.EI_FinancialAnalysis_Value = reader["EI_FinancialAnalysis_Value"] != DBNull.Value ? reader["EI_FinancialAnalysis_Value"].ToString() : "0";
                entities.EI_Quality_Value = reader["EI_Quality_Value"] != DBNull.Value ? reader["EI_Quality_Value"].ToString() : "0";
                entities.EI_CostResponse_Value = reader["EI_CostResponse_Value"] != DBNull.Value ? reader["EI_CostResponse_Value"].ToString() : "0";
                entities.EI_Delivery_Value = reader["EI_Delivery_Value"] != DBNull.Value ? reader["EI_Delivery_Value"].ToString() : "0";
                entities.EI_Cooperation_Value = reader["EI_Cooperation_Value"] != DBNull.Value ? reader["EI_Cooperation_Value"].ToString() : "0";
                entities.EI_CSR_Value = reader["EI_CSR_Value"] != DBNull.Value ? reader["EI_CSR_Value"].ToString() : "0";

                entities.EI_TotalScore = reader["EI_TotalScore"] != DBNull.Value ? reader["EI_TotalScore"].ToString() : "0";
                entities.JudgementByPerson = reader["JudgementByPerson"] != DBNull.Value ? reader["JudgementByPerson"].ToString() : string.Empty;
                entities.Reason = reader["Reason"] != DBNull.Value ? reader["Reason"].ToString() : string.Empty;
                entities.CircularComments = reader["CircularComments"] != DBNull.Value ? reader["CircularComments"].ToString() : string.Empty;
                entities.DivManInstructions = reader["DivManInstructions"] != DBNull.Value ? reader["DivManInstructions"].ToString() : string.Empty;
                entities.ForApproval = reader["ForApproval"] != DBNull.Value ? reader["ForApproval"].ToString() : "0";
                entities.CustomerChartWidth = reader["EI_TotalScore"] != DBNull.Value ? ((int.Parse(reader["EI_TotalScore"].ToString()) * 2) + 200).ToString() + "px" : "0px";
                entities.CustomerChartWidthExtended = reader["EI_TotalScore"] != DBNull.Value ? ((int.Parse(reader["EI_TotalScore"].ToString()) * 2) + 400).ToString() + "px" : "0px";

                entities.Incharge = reader["AddedBy"] != DBNull.Value ? reader["AddedBy"].ToString() + "(" + reader["AddedDate"].ToString() + ")" : string.Empty;
                entities.SectionIncharge = reader["SectionIncharge"] != DBNull.Value ? reader["SectionIncharge"].ToString() + "(" + reader["DOASectionIncharge"].ToString() + ")" : string.Empty;
                entities.DeptManager = reader["DepMan"] != DBNull.Value ? reader["DepMan"].ToString() + "(" + reader["DOADepMan"].ToString() + ")" : string.Empty;
                entities.DivManager = reader["DivMan"] != DBNull.Value ? reader["DivMan"].ToString() + "(" + reader["DOADivMan"].ToString() + ")" : string.Empty;

                totalScore = double.Parse(entities.EI_TotalScore);

                if (totalScore >= 80)
                {
                    entities.EI_EvaluationPoints = "A";
                }
                if (totalScore <= 79 && totalScore >= 60)
                {
                    entities.EI_EvaluationPoints = "B";
                }
                if (totalScore <= 59 && totalScore >= 40)
                {
                    entities.EI_EvaluationPoints = "C";
                }
                if (totalScore <= 39)
                {
                    entities.EI_EvaluationPoints = "D";
                }

                //if (reader["DivManExpand"] != DBNull.Value)
                //{
                //    if (!string.IsNullOrEmpty(reader["DivManExpand"].ToString()))
                //    {
                //        entities.DivManEvaluationResult = reader["DivManExpand"].ToString();
                //    }
                //} else if (reader["DivManContinue"] != DBNull.Value)
                //{
                //    if (!string.IsNullOrEmpty(reader["DivManContinue"].ToString()))
                //    {
                //        entities.DivManEvaluationResult = reader["DivManContinue"].ToString();
                //    }
                //} else if (reader["DivManReduce"] != DBNull.Value)
                //{
                //    if (!string.IsNullOrEmpty(reader["DivManReduce"].ToString()))
                //    {
                //        entities.DivManEvaluationResult = reader["DivManReduce"].ToString();
                //    }
                //}
                //else if (reader["DivManStop"] != DBNull.Value)
                //{
                //    if (!string.IsNullOrEmpty(reader["DivManStop"].ToString()))
                //    {
                //        entities.DivManEvaluationResult = reader["DivManStop"].ToString();
                //    }
                //}
                //else
                //{
                //    entities.DivManEvaluationResult = string.Empty;
                //}

                entities.DivManEvaluationResult = reader["DivManExpand"].ToString() + "" + reader["DivManContinue"].ToString() + "" + reader["DivManReduce"].ToString() + "" + reader["DivManStop"].ToString();

                

                //entities.EI_Quality = reader["EI_Quality"].ToString().ToUpper();

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

    public List<Entities_SE_RequestEntry> SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear3(string fy)
    {
        List<Entities_SE_RequestEntry> list = new List<Entities_SE_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        double totalScore = 0;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear3";
            cmd.Parameters.Add(Factory.CreateParameter("@fy_year", fy));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_SE_RequestEntry entities = new Entities_SE_RequestEntry();

                entities.DetailsRefId = reader["RefId"] != DBNull.Value ? reader["RefId"].ToString() : string.Empty;
                entities.FY_SupplierId = reader["FY_SupplierId"] != DBNull.Value ? reader["FY_SupplierId"].ToString() : string.Empty;
                entities.SupplierName = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : string.Empty;
                entities.AutomotiveRelated = reader["AutomotiveRelated"] != DBNull.Value ? reader["AutomotiveRelated"].ToString() : string.Empty;
                entities.Classification = reader["Classification"] != DBNull.Value ? reader["Classification"].ToString() : string.Empty;
                entities.ItemClassification = reader["ItemClassification"] != DBNull.Value ? reader["ItemClassification"].ToString() : string.Empty;
                entities.JudgementYearMonth = reader["JudgementYearMonth"] != DBNull.Value ? reader["JudgementYearMonth"].ToString() : string.Empty;
                entities.SubContractor = reader["SubContractor"] != DBNull.Value ? reader["SubContractor"].ToString() : string.Empty;
                entities.NoteworthyPoints = reader["NoteworthyPoints"] != DBNull.Value ? reader["NoteworthyPoints"].ToString() : string.Empty;
                entities.MakerName = reader["MakerName"] != DBNull.Value ? reader["MakerName"].ToString() : string.Empty;
                entities.ISO9001 = reader["ISO9001"] != DBNull.Value ? reader["ISO9001"].ToString() : string.Empty;
                entities.ISO14001 = reader["ISO14001"] != DBNull.Value ? reader["ISO14001"].ToString() : string.Empty;
                entities.IATF16949 = reader["IATF16949"] != DBNull.Value ? reader["IATF16949"].ToString() : string.Empty;
                entities.EI_FinancialAnalysis = reader["EI_FinancialAnalysis"] != DBNull.Value ? reader["EI_FinancialAnalysis"].ToString() : string.Empty;
                entities.EI_Quality = reader["EI_Quality"] != DBNull.Value ? reader["EI_Quality"].ToString() : string.Empty;
                entities.EI_CostResponse = reader["EI_CostResponse"] != DBNull.Value ? reader["EI_CostResponse"].ToString() : string.Empty;
                entities.EI_Delivery = reader["EI_Delivery"] != DBNull.Value ? reader["EI_Delivery"].ToString() : string.Empty;
                entities.EI_Cooperation = reader["EI_Cooperation"] != DBNull.Value ? reader["EI_Cooperation"].ToString() : string.Empty;
                entities.EI_CSR = reader["EI_CSR"] != DBNull.Value ? reader["EI_CSR"].ToString() : string.Empty;

                entities.EI_FinancialAnalysis_Value = reader["EI_FinancialAnalysis_Value"] != DBNull.Value ? reader["EI_FinancialAnalysis_Value"].ToString() : "0";
                entities.EI_Quality_Value = reader["EI_Quality_Value"] != DBNull.Value ? reader["EI_Quality_Value"].ToString() : "0";
                entities.EI_CostResponse_Value = reader["EI_CostResponse_Value"] != DBNull.Value ? reader["EI_CostResponse_Value"].ToString() : "0";
                entities.EI_Delivery_Value = reader["EI_Delivery_Value"] != DBNull.Value ? reader["EI_Delivery_Value"].ToString() : "0";
                entities.EI_Cooperation_Value = reader["EI_Cooperation_Value"] != DBNull.Value ? reader["EI_Cooperation_Value"].ToString() : "0";
                entities.EI_CSR_Value = reader["EI_CSR_Value"] != DBNull.Value ? reader["EI_CSR_Value"].ToString() : "0";

                entities.EI_TotalScore = reader["EI_TotalScore"] != DBNull.Value ? reader["EI_TotalScore"].ToString() : "0";
                entities.JudgementByPerson = reader["JudgementByPerson"] != DBNull.Value ? reader["JudgementByPerson"].ToString() : string.Empty;
                entities.Reason = reader["Reason"] != DBNull.Value ? reader["Reason"].ToString() : string.Empty;
                entities.CircularComments = reader["CircularComments"] != DBNull.Value ? reader["CircularComments"].ToString() : string.Empty;
                entities.DivManInstructions = reader["DivManInstructions"] != DBNull.Value ? reader["DivManInstructions"].ToString() : string.Empty;
                entities.ForApproval = reader["ForApproval"] != DBNull.Value ? reader["ForApproval"].ToString() : "0";
                entities.CustomerChartWidth = reader["EI_TotalScore"] != DBNull.Value ? ((int.Parse(reader["EI_TotalScore"].ToString()) * 2) + 200).ToString() + "px" : "0px";
                entities.CustomerChartWidthExtended = reader["EI_TotalScore"] != DBNull.Value ? ((int.Parse(reader["EI_TotalScore"].ToString()) * 2) + 400).ToString() + "px" : "0px";

                entities.Incharge = reader["AddedBy"] != DBNull.Value ? reader["AddedBy"].ToString() + "(" + reader["AddedDate"].ToString() + ")" : string.Empty;
                entities.SectionIncharge = reader["SectionIncharge"] != DBNull.Value ? reader["SectionIncharge"].ToString() + "(" + reader["DOASectionIncharge"].ToString() + ")" : string.Empty;
                entities.DeptManager = reader["DepMan"] != DBNull.Value ? reader["DepMan"].ToString() + "(" + reader["DOADepMan"].ToString() + ")" : string.Empty;
                entities.DivManager = reader["DivMan"] != DBNull.Value ? reader["DivMan"].ToString() + "(" + reader["DOADivMan"].ToString() + ")" : string.Empty;

                totalScore = double.Parse(entities.EI_TotalScore);

                if (totalScore >= 80)
                {
                    entities.EI_EvaluationPoints = "A";
                }
                if (totalScore <= 79 && totalScore >= 60)
                {
                    entities.EI_EvaluationPoints = "B";
                }
                if (totalScore <= 59 && totalScore >= 40)
                {
                    entities.EI_EvaluationPoints = "C";
                }
                if (totalScore <= 39)
                {
                    entities.EI_EvaluationPoints = "D";
                }

                //if (reader["DivManExpand"] != DBNull.Value)
                //{
                //    if (!string.IsNullOrEmpty(reader["DivManExpand"].ToString()))
                //    {
                //        entities.DivManEvaluationResult = reader["DivManExpand"].ToString();
                //    }
                //} else if (reader["DivManContinue"] != DBNull.Value)
                //{
                //    if (!string.IsNullOrEmpty(reader["DivManContinue"].ToString()))
                //    {
                //        entities.DivManEvaluationResult = reader["DivManContinue"].ToString();
                //    }
                //} else if (reader["DivManReduce"] != DBNull.Value)
                //{
                //    if (!string.IsNullOrEmpty(reader["DivManReduce"].ToString()))
                //    {
                //        entities.DivManEvaluationResult = reader["DivManReduce"].ToString();
                //    }
                //}
                //else if (reader["DivManStop"] != DBNull.Value)
                //{
                //    if (!string.IsNullOrEmpty(reader["DivManStop"].ToString()))
                //    {
                //        entities.DivManEvaluationResult = reader["DivManStop"].ToString();
                //    }
                //}
                //else
                //{
                //    entities.DivManEvaluationResult = string.Empty;
                //}

                entities.DivManEvaluationResult = reader["DivManExpand"].ToString() + "" + reader["DivManContinue"].ToString() + "" + reader["DivManReduce"].ToString() + "" + reader["DivManStop"].ToString();



                //entities.EI_Quality = reader["EI_Quality"].ToString().ToUpper();

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

    public List<Entities_SE_RequestEntry> SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear2(string fy)
    {
        List<Entities_SE_RequestEntry> list = new List<Entities_SE_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        double totalScore = 0;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SE_TRANSACTION_SupplierTable_GetAll_ByFiscalYear2";
            cmd.Parameters.Add(Factory.CreateParameter("@fy_year", fy));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_SE_RequestEntry entities = new Entities_SE_RequestEntry();

                entities.DetailsRefId = reader["RefId"] != DBNull.Value ? reader["RefId"].ToString() : string.Empty;
                entities.FY_SupplierId = reader["FY_SupplierId"] != DBNull.Value ? reader["FY_SupplierId"].ToString() : string.Empty;
                entities.SupplierName = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : string.Empty;
                entities.AutomotiveRelated = reader["AutomotiveRelated"] != DBNull.Value ? reader["AutomotiveRelated"].ToString() : string.Empty;
                entities.Classification = reader["Classification"] != DBNull.Value ? reader["Classification"].ToString() : string.Empty;
                entities.ItemClassification = reader["ItemClassification"] != DBNull.Value ? reader["ItemClassification"].ToString() : string.Empty;
                entities.JudgementYearMonth = reader["JudgementYearMonth"] != DBNull.Value ? reader["JudgementYearMonth"].ToString() : string.Empty;
                entities.SubContractor = reader["SubContractor"] != DBNull.Value ? reader["SubContractor"].ToString() : string.Empty;
                entities.NoteworthyPoints = reader["NoteworthyPoints"] != DBNull.Value ? reader["NoteworthyPoints"].ToString() : string.Empty;
                entities.MakerName = reader["MakerName"] != DBNull.Value ? reader["MakerName"].ToString() : string.Empty;
                entities.ISO9001 = reader["ISO9001"] != DBNull.Value ? reader["ISO9001"].ToString() : string.Empty;
                entities.ISO14001 = reader["ISO14001"] != DBNull.Value ? reader["ISO14001"].ToString() : string.Empty;
                entities.IATF16949 = reader["IATF16949"] != DBNull.Value ? reader["IATF16949"].ToString() : string.Empty;
                entities.EI_FinancialAnalysis = reader["EI_FinancialAnalysis"] != DBNull.Value ? reader["EI_FinancialAnalysis"].ToString() : string.Empty;
                entities.EI_Quality = reader["EI_Quality"] != DBNull.Value ? reader["EI_Quality"].ToString() : string.Empty;
                entities.EI_CostResponse = reader["EI_CostResponse"] != DBNull.Value ? reader["EI_CostResponse"].ToString() : string.Empty;
                entities.EI_Delivery = reader["EI_Delivery"] != DBNull.Value ? reader["EI_Delivery"].ToString() : string.Empty;
                entities.EI_Cooperation = reader["EI_Cooperation"] != DBNull.Value ? reader["EI_Cooperation"].ToString() : string.Empty;
                entities.EI_CSR = reader["EI_CSR"] != DBNull.Value ? reader["EI_CSR"].ToString() : string.Empty;

                entities.EI_FinancialAnalysis_Value = reader["EI_FinancialAnalysis_Value"] != DBNull.Value ? reader["EI_FinancialAnalysis_Value"].ToString() : "0";
                entities.EI_Quality_Value = reader["EI_Quality_Value"] != DBNull.Value ? reader["EI_Quality_Value"].ToString() : "0";
                entities.EI_CostResponse_Value = reader["EI_CostResponse_Value"] != DBNull.Value ? reader["EI_CostResponse_Value"].ToString() : "0";
                entities.EI_Delivery_Value = reader["EI_Delivery_Value"] != DBNull.Value ? reader["EI_Delivery_Value"].ToString() : "0";
                entities.EI_Cooperation_Value = reader["EI_Cooperation_Value"] != DBNull.Value ? reader["EI_Cooperation_Value"].ToString() : "0";
                entities.EI_CSR_Value = reader["EI_CSR_Value"] != DBNull.Value ? reader["EI_CSR_Value"].ToString() : "0";

                entities.EI_TotalScore = reader["EI_TotalScore"] != DBNull.Value ? reader["EI_TotalScore"].ToString() : "0";
                entities.EI_Ranking = reader["Ranking"] != DBNull.Value ? reader["Ranking"].ToString() : string.Empty;
                entities.JudgementByPerson = reader["JudgementByPerson"] != DBNull.Value ? reader["JudgementByPerson"].ToString() : string.Empty;
                entities.Reason = reader["Reason"] != DBNull.Value ? reader["Reason"].ToString() : string.Empty;
                entities.CircularComments = reader["CircularComments"] != DBNull.Value ? reader["CircularComments"].ToString() : string.Empty;
                entities.DivManInstructions = reader["DivManInstructions"] != DBNull.Value ? reader["DivManInstructions"].ToString() : string.Empty;
                entities.ForApproval = reader["ForApproval"] != DBNull.Value ? reader["ForApproval"].ToString() : "0";
                entities.CustomerChartWidth = reader["EI_TotalScore"] != DBNull.Value ? ((int.Parse(reader["EI_TotalScore"].ToString()) * 2) + 200).ToString() + "px" : "0px";
                entities.CustomerChartWidthExtended = reader["EI_TotalScore"] != DBNull.Value ? ((int.Parse(reader["EI_TotalScore"].ToString()) * 2) + 400).ToString() + "px" : "0px";

                entities.Incharge = reader["AddedBy"] != DBNull.Value ? reader["AddedBy"].ToString() + "(" + reader["AddedDate"].ToString() + ")" : string.Empty;
                entities.SectionIncharge = reader["SectionIncharge"] != DBNull.Value ? reader["SectionIncharge"].ToString() + "(" + reader["DOASectionIncharge"].ToString() + ")" : string.Empty;
                entities.DeptManager = reader["DepMan"] != DBNull.Value ? reader["DepMan"].ToString() + "(" + reader["DOADepMan"].ToString() + ")" : string.Empty;
                entities.DivManager = reader["DivMan"] != DBNull.Value ? reader["DivMan"].ToString() + "(" + reader["DOADivMan"].ToString() + ")" : string.Empty;

                totalScore = double.Parse(entities.EI_TotalScore);

                if (totalScore >= 80)
                {
                    entities.EI_EvaluationPoints = "A";
                }
                if (totalScore <= 79 && totalScore >= 60)
                {
                    entities.EI_EvaluationPoints = "B";
                }
                if (totalScore <= 59 && totalScore >= 40)
                {
                    entities.EI_EvaluationPoints = "C";
                }
                if (totalScore <= 39)
                {
                    entities.EI_EvaluationPoints = "D";
                }

                if (totalScore >= 80)
                {
                    entities.ScoreClass = "A";
                }
                if (totalScore <= 79 && totalScore >= 60)
                {
                    entities.ScoreClass = "B";
                }
                if (totalScore <= 59 && totalScore >= 40)
                {
                    entities.ScoreClass = "C";
                }
                if (totalScore <= 39)
                {
                    entities.ScoreClass = "D";
                }

                //if (reader["DivManExpand"] != DBNull.Value)
                //{
                //    if (!string.IsNullOrEmpty(reader["DivManExpand"].ToString()))
                //    {
                //        entities.DivManEvaluationResult = reader["DivManExpand"].ToString();
                //    }
                //} else if (reader["DivManContinue"] != DBNull.Value)
                //{
                //    if (!string.IsNullOrEmpty(reader["DivManContinue"].ToString()))
                //    {
                //        entities.DivManEvaluationResult = reader["DivManContinue"].ToString();
                //    }
                //} else if (reader["DivManReduce"] != DBNull.Value)
                //{
                //    if (!string.IsNullOrEmpty(reader["DivManReduce"].ToString()))
                //    {
                //        entities.DivManEvaluationResult = reader["DivManReduce"].ToString();
                //    }
                //}
                //else if (reader["DivManStop"] != DBNull.Value)
                //{
                //    if (!string.IsNullOrEmpty(reader["DivManStop"].ToString()))
                //    {
                //        entities.DivManEvaluationResult = reader["DivManStop"].ToString();
                //    }
                //}
                //else
                //{
                //    entities.DivManEvaluationResult = string.Empty;
                //}

                entities.DivManEvaluationResult = reader["DivManExpand"].ToString() + "" + reader["DivManContinue"].ToString() + "" + reader["DivManReduce"].ToString() + "" + reader["DivManStop"].ToString();



                //entities.EI_Quality = reader["EI_Quality"].ToString().ToUpper();

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


    public List<Entities_SE_RequestEntry> SE_TRANSACTION_GetLevel_From_CriteriaForMaterial()
    {
        List<Entities_SE_RequestEntry> list = new List<Entities_SE_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SE_TRANSACTION_GetLevel_From_CriteriaForMaterial";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_SE_RequestEntry entities = new Entities_SE_RequestEntry();

                entities.Level = reader["Lvl"].ToString().ToUpper();
                entities.Weight = reader["Weight"].ToString().ToUpper();
                entities.Score = string.Empty;
                entities.TotalScore = string.Empty;

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

    public List<Entities_SE_RequestEntry> SE_TRANSACTION_IsScoreExist_In_TransactionScore(string detailsrefid)
    {
        List<Entities_SE_RequestEntry> list = new List<Entities_SE_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SE_TRANSACTION_IsScoreExist_In_TransactionScore";
            cmd.Parameters.Add(Factory.CreateParameter("@detailsrefid", detailsrefid));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_SE_RequestEntry entities = new Entities_SE_RequestEntry();

                entities.DetailsRefId = reader["DetailsRefId"].ToString().ToUpper();
                entities.Level = reader["Type"].ToString().ToUpper();
                entities.Score = reader["Score"] != DBNull.Value ? reader["Score"].ToString() : string.Empty;
                entities.Weight = reader["Weight"] != DBNull.Value ? reader["Weight"].ToString() : string.Empty;
                entities.TotalScore = reader["TotalScore"] != DBNull.Value ? reader["TotalScore"].ToString() : string.Empty;


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

    public List<Entities_SE_RequestEntry> SE_TRANSACTION_ReportAuditTrail_GetAll()
    {
        List<Entities_SE_RequestEntry> list = new List<Entities_SE_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TOP 500 * FROM SE_TRANSACTION_DownloadAuditTrail WITH (NOLOCK) ORDER BY RefId DESC";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_SE_RequestEntry entities = new Entities_SE_RequestEntry();

                entities.ReportAudit_RefId = reader["RefId"].ToString().ToUpper();
                entities.ReportAudit_FiscalYear = reader["FiscalYear"].ToString().ToUpper();
                entities.ReportAudit_FormName = reader["FormName"].ToString().ToUpper();
                entities.ReportAudit_DownloadedBy = reader["DownloadedBy"].ToString().ToUpper();
                entities.ReportAudit_DownloadedDate = reader["DownloadedDate"].ToString().ToUpper();


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
