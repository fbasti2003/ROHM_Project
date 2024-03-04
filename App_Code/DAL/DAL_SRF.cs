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
//using REPI_PUR_SOFRA.App_Code.ENTITIES;


    public class DAL_SRF
    {
        public DAL_SRF()
        {
        }

        #region PURPOSE OF PULLOUT

        public List<Entities_SRF_PurposeOfPullOut> SRF_MT_PurposeOfPullOut_GetAll()
        {
            List<Entities_SRF_PurposeOfPullOut> list = new List<Entities_SRF_PurposeOfPullOut>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_MT_PurposeOfPullOut_GetAll";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_PurposeOfPullOut entities = new Entities_SRF_PurposeOfPullOut();

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

        public int SRF_MT_PurposeOfPullOut_IsDisabled(Entities_SRF_PurposeOfPullOut entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_MT_PurposeOfPullOut_IsDisabled";

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

        public int SRF_MT_PurposeOfPullOut_Append(Entities_SRF_PurposeOfPullOut entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_MT_PurposeOfPullOut_Append";

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

        public int SRF_MT_PurposeOfPullOut_Insert(Entities_SRF_PurposeOfPullOut entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_MT_PurposeOfPullOut_Insert";

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

        public List<Entities_SRF_PurposeOfPullOut> SRF_MT_PurposeOfPullOut_GetByName(string name)
        {
            List<Entities_SRF_PurposeOfPullOut> list = new List<Entities_SRF_PurposeOfPullOut>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_MT_PurposeOfPullOut_GetByName";

                cmd.Parameters.Add(Factory.CreateParameter("@name", name));

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_PurposeOfPullOut entities = new Entities_SRF_PurposeOfPullOut();

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

        public List<Entities_SRF_PurposeOfPullOut> SRF_MT_PurposeOfPullOut_GetByName_Like(string name)
        {
            List<Entities_SRF_PurposeOfPullOut> list = new List<Entities_SRF_PurposeOfPullOut>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_MT_PurposeOfPullOut_GetByName_Like";

                cmd.Parameters.Add(Factory.CreateParameter("@name", name));

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_PurposeOfPullOut entities = new Entities_SRF_PurposeOfPullOut();

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

        #region LOA

        public List<Entities_SRF_LOA> SRF_MT_LOA_GetAll()
        {
            List<Entities_SRF_LOA> list = new List<Entities_SRF_LOA>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_MT_LOA_GetAll";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_LOA entities = new Entities_SRF_LOA();

                    entities.RefId = reader["RefId"].ToString();
                    entities.LoaNo = reader["LoaNo"].ToString();
                    entities.Balance = reader["Balance"].ToString();
                    entities.LoaPriceValue = reader["LoaPriceValue"].ToString();
                    entities.MaturityDate = reader["MaturityDate"].ToString();
                    entities.Remarks = reader["Remarks"].ToString();
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

        public int SRF_MT_LOA_IsDisabled(Entities_SRF_LOA entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_MT_LOA_IsDisabled";

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

        public int SRF_MT_LOA_Append(Entities_SRF_LOA entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_MT_LOA_Append";

            cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
            cmd.Parameters.Add(Factory.CreateParameter("@loano", entity.LoaNo));
            cmd.Parameters.Add(Factory.CreateParameter("@balance", entity.Balance));
            cmd.Parameters.Add(Factory.CreateParameter("@loapricevalue", entity.LoaPriceValue));
            cmd.Parameters.Add(Factory.CreateParameter("@maturitydate", entity.MaturityDate));
            cmd.Parameters.Add(Factory.CreateParameter("@remarks", entity.Remarks));
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

        public int SRF_MT_LOA_Insert(Entities_SRF_LOA entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_MT_LOA_Insert";

            cmd.Parameters.Add(Factory.CreateParameter("@loano", entity.LoaNo));
            cmd.Parameters.Add(Factory.CreateParameter("@balance", entity.Balance));
            cmd.Parameters.Add(Factory.CreateParameter("@loapricevalue", entity.LoaPriceValue));
            cmd.Parameters.Add(Factory.CreateParameter("@maturitydate", entity.MaturityDate));
            cmd.Parameters.Add(Factory.CreateParameter("@remarks", entity.Remarks));
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

        public int SRF_MT_LOA_UpdateBalance_ByRefId(Entities_SRF_LOA entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_MT_LOA_UpdateBalance_ByRefId";

            cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
            cmd.Parameters.Add(Factory.CreateParameter("@balance", entity.Balance));

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

        public List<Entities_SRF_LOA> SRF_MT_LOA_GetByName(string name)
        {
            List<Entities_SRF_LOA> list = new List<Entities_SRF_LOA>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_MT_LOA_GetByName";

                cmd.Parameters.Add(Factory.CreateParameter("@loano", name));

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_LOA entities = new Entities_SRF_LOA();

                    entities.RefId = reader["RefId"].ToString();
                    entities.LoaNo = reader["LOANo"].ToString();
                    entities.Balance = reader["Balance"].ToString();
                    entities.LoaPriceValue = reader["LoaPriceValue"].ToString();
                    entities.MaturityDate = reader["MaturityDate"].ToString();
                    entities.Remarks = reader["Remarks"].ToString();
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

        public List<Entities_SRF_LOA> SRF_MT_LOA_GetByName_Like(string name)
        {
            List<Entities_SRF_LOA> list = new List<Entities_SRF_LOA>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_MT_LOA_GetByName_Like";

                cmd.Parameters.Add(Factory.CreateParameter("@name", name));

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_LOA entities = new Entities_SRF_LOA();

                    entities.RefId = reader["RefId"].ToString();
                    entities.LoaNo = reader["LOANo"].ToString();
                    entities.Balance = reader["Balance"].ToString();
                    entities.LoaPriceValue = reader["LoaPriceValue"].ToString();
                    entities.MaturityDate = reader["MaturityDate"].ToString();
                    entities.Remarks = reader["Remarks"].ToString();
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

        #region PULL OUT OF CONTAINER TUBES

        public List<Entities_SRF_PO_ContainerTubes> SRF_MT_PullOutOfContainerTubes_GetAll()
        {
            List<Entities_SRF_PO_ContainerTubes> list = new List<Entities_SRF_PO_ContainerTubes>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_MT_PullOutContainerTubes_GetAll";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_PO_ContainerTubes entities = new Entities_SRF_PO_ContainerTubes();

                    entities.RefId = reader["RefId"].ToString();
                    entities.Specification = reader["Specification"].ToString();
                    entities.WeightOfBox = reader["WeightOfBox"].ToString();
                    entities.Multiplier = reader["Multiplier"].ToString();
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

        public int SRF_MT_PullOutOfContainerTubes_IsDisabled(Entities_SRF_PO_ContainerTubes entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_MT_PullOutContainerTubes_IsDisabled";

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

        public int SRF_MT_PullOutOfContainerTubes_Append(Entities_SRF_PO_ContainerTubes entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_MT_PullOutContainerTubes_Append";

            cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
            cmd.Parameters.Add(Factory.CreateParameter("@specification", entity.Specification));
            cmd.Parameters.Add(Factory.CreateParameter("@weightofbox", entity.WeightOfBox));
            cmd.Parameters.Add(Factory.CreateParameter("@multiplier", entity.Multiplier));
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

        public int SRF_MT_PullOutOfContainerTubes_Insert(Entities_SRF_PO_ContainerTubes entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_MT_PullOutContainerTubes_Insert";

            cmd.Parameters.Add(Factory.CreateParameter("@specification", entity.Specification));
            cmd.Parameters.Add(Factory.CreateParameter("@weightofbox", entity.WeightOfBox));
            cmd.Parameters.Add(Factory.CreateParameter("@multiplier", entity.Multiplier));
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

        #endregion

        #region PULL OUT OF IC TRAYS

        public List<Entities_SRF_PO_ICTrays> SRF_MT_PullOutICTrays_GetAll()
        {
            List<Entities_SRF_PO_ICTrays> list = new List<Entities_SRF_PO_ICTrays>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_MT_PullOutICTrays_GetAll";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_PO_ICTrays entities = new Entities_SRF_PO_ICTrays();

                    entities.RefId = reader["RefId"].ToString();
                    entities.Specification = reader["Specification"].ToString();
                    entities.BoxType = reader["BoxType"].ToString();
                    entities.Size = reader["Size"].ToString();
                    entities.Multiplier = reader["Multiplier"].ToString();
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

        public int SRF_MT_PullOutICTrays_IsDisabled(Entities_SRF_PO_ICTrays entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_MT_PullOutICTrays_IsDisabled";

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

        public int SRF_MT_PullOutICTrays_Append(Entities_SRF_PO_ICTrays entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_MT_PullOutICTrays_Append";

            cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
            cmd.Parameters.Add(Factory.CreateParameter("@specification", entity.Specification));
            cmd.Parameters.Add(Factory.CreateParameter("@boxtype", entity.BoxType));
            cmd.Parameters.Add(Factory.CreateParameter("@size", entity.Size));
            cmd.Parameters.Add(Factory.CreateParameter("@multiplier", entity.Multiplier));
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

        public int SRF_MT_PullOutICTrays_Insert(Entities_SRF_PO_ICTrays entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_MT_PullOutICTrays_Insert";

            cmd.Parameters.Add(Factory.CreateParameter("@specification", entity.Specification));
            cmd.Parameters.Add(Factory.CreateParameter("@boxtype", entity.BoxType));
            cmd.Parameters.Add(Factory.CreateParameter("@size", entity.Size));
            cmd.Parameters.Add(Factory.CreateParameter("@multiplier", entity.Multiplier));
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

        #endregion

        #region PULL OUT OTHERS

        public List<Entities_SRF_PO_Others> SRF_MT_PullOutOthers_GetAll()
        {
            List<Entities_SRF_PO_Others> list = new List<Entities_SRF_PO_Others>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_MT_PullOutOthers_GetAll";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_PO_Others entities = new Entities_SRF_PO_Others();

                    entities.RefId = reader["RefId"].ToString();
                    entities.Specification = reader["Specification"].ToString();
                    entities.SrfItemName = reader["SrfItemName"].ToString();
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

        public int SRF_MT_PullOutOthers_IsDisabled(Entities_SRF_PO_Others entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_MT_PullOutOthers_IsDisabled";

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

        public int SRF_MT_PullOutOthers_Append(Entities_SRF_PO_Others entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_MT_PullOutOthers_Append";

            cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
            cmd.Parameters.Add(Factory.CreateParameter("@specification", entity.Specification));
            cmd.Parameters.Add(Factory.CreateParameter("@srfitemname", entity.SrfItemName));
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

        public int SRF_MT_PullOutOthers_Insert(Entities_SRF_PO_Others entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_MT_PullOutOthers_Insert";

            cmd.Parameters.Add(Factory.CreateParameter("@specification", entity.Specification));
            cmd.Parameters.Add(Factory.CreateParameter("@srfitemname", entity.SrfItemName));
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

        #endregion

        #region PULL OUT TRANSACTION

        public Int32 SRF_TRANSACTION_PO_Request_Count(string year)
        {
            Int32 result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_TRANSACTION_PO_Request_Count";

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


        public int SRF_TRANSACTION_PO_SQLTransaction(string query)
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

        public List<Entities_SRF_PO_Entry> SRF_TRANSACTION_PO_AllRequest_ByDateRange(Entities_SRF_PO_Entry entity)
        {
            List<Entities_SRF_PO_Entry> list = new List<Entities_SRF_PO_Entry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_TRANSACTION_PO_AllRequest_ByDateRange";
                cmd.Parameters.Add(Factory.CreateParameter("@from", entity.CrFrom = entity.CrFrom.Length <= 0 ? string.Empty : entity.CrFrom));
                cmd.Parameters.Add(Factory.CreateParameter("@to", entity.CrTo = entity.CrTo.Length <= 0 ? string.Empty : entity.CrTo));

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_PO_Entry entities = new Entities_SRF_PO_Entry();

                    entities.DisapprovalRemarks = reader["DisapprovalRemarks"].ToString();
                    entities.Division = reader["Division"].ToString();
                    entities.DivisionDisplay = reader["DivisionDisplay"].ToString();
                    entities.Supplier = reader["Supplier"].ToString();
                    entities.Head_Type = reader["Type"].ToString();
                    entities.Head_TotalQuantity = reader["TotaQuantity"].ToString();
                    entities.Head_TotalBoxes = reader["TotalBoxes"].ToString();
                    entities.Head_RefId = reader["RefId"].ToString();
                    entities.Head_Ctrlno = reader["Ctrlno"].ToString();
                    entities.Head_Requester = reader["Requester"].ToString();
                    entities.Head_TransactionDate = reader["TransactionDate"].ToString().ToUpper();
                    entities.Head_Type = reader["Type"].ToString().ToUpper();
                    entities.Head_StatProdManager = reader["StatProdManager"] != DBNull.Value ? reader["StatProdManager"].ToString() : "0";
                    entities.Head_StatBuyer = reader["StatBuyer"] != DBNull.Value ? reader["StatBuyer"].ToString() : "0";
                    entities.Head_StatSCIncharge = reader["StatSCIncharge"] != DBNull.Value ? reader["StatSCIncharge"].ToString() : "0";
                    entities.SupplierEmail = reader["SupplierEmail"] != DBNull.Value ? reader["SupplierEmail"].ToString() : string.Empty;
                    entities.SupplierName = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : string.Empty;


                    if (entities.Head_StatProdManager == "0" && entities.Head_StatBuyer == "0" && entities.Head_StatSCIncharge == "0")
                    {
                        entities.StatAll = "FOR PROD.MNGR. APPROVAL";
                        entities.CssColorCode = "#f44336";
                    }
                    if (entities.Head_StatProdManager == "1" && entities.Head_StatBuyer == "0" && entities.Head_StatSCIncharge == "0")
                    {
                        entities.StatAll = "FOR SCD BUYER APPROVAL";
                        entities.CssColorCode = "#28B463";
                    }
                    if (entities.Head_StatProdManager == "1" && entities.Head_StatBuyer == "1" && entities.Head_StatSCIncharge == "0")
                    {
                        entities.StatAll = "FOR SCD INCHARGE APPROVAL";
                        entities.CssColorCode = "#5499C7";
                    }
                    if ((entities.Head_StatProdManager == "1" || entities.Head_StatProdManager == "-1") && (entities.Head_StatBuyer == "1" || entities.Head_StatBuyer == "-1") && (entities.Head_StatSCIncharge == "1" || entities.Head_StatSCIncharge == "-1"))
                    {
                        entities.StatAll = "APPROVED";
                        entities.CssColorCode = "#673AB7";
                    }
                    if (entities.Head_StatProdManager == "2" || entities.Head_StatBuyer == "2" || entities.Head_StatSCIncharge == "2")
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

        public List<Entities_SRF_PO_Entry> SRF_TRANSACTION_PO_Report_ByRange(Entities_SRF_PO_Entry entity)
        {
            List<Entities_SRF_PO_Entry> list = new List<Entities_SRF_PO_Entry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_TRANSACTION_PO_Report_ByRange";
                cmd.Parameters.Add(Factory.CreateParameter("@from", entity.CrFrom = entity.CrFrom.Length <= 0 ? string.Empty : entity.CrFrom));
                cmd.Parameters.Add(Factory.CreateParameter("@to", entity.CrTo = entity.CrTo.Length <= 0 ? string.Empty : entity.CrTo));

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_PO_Entry entities = new Entities_SRF_PO_Entry();

                    entities.Ctrlno = reader["CTRLNo"].ToString();
                    entities.Specification = reader["Specification"].ToString();
                    entities.BoxType = reader["BoxType"].ToString();
                    entities.Size = reader["Size"].ToString();
                    entities.WeightOfBox = reader["WeightOfBox"].ToString();
                    entities.GrossWeight = reader["GrossWeight"].ToString();
                    entities.NetWeight = reader["NetWeight"].ToString();
                    entities.Multiplier = reader["Multiplier"].ToString();
                    entities.NoOfBoxes = reader["NoOfBoxes"].ToString();
                    entities.Quantity = reader["Quantity"].ToString();
                    entities.Head_Type = reader["Type"].ToString();
                    entities.Head_Requester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true).ToUpper();
                    entities.Head_TransactionDate = reader["TransactionDate"].ToString();

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

        public List<Entities_SRF_PO_Entry> SRF_TRANSACTION_PO_AllRequest_ByCTRLNo(string ctrlno)
        {
            List<Entities_SRF_PO_Entry> list = new List<Entities_SRF_PO_Entry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_TRANSACTION_PO_AllRequest_ByCTRLNo";
                cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", ctrlno = ctrlno.Length <= 0 ? string.Empty : ctrlno));

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_PO_Entry entities = new Entities_SRF_PO_Entry();

                    entities.Refid = reader["RefId"].ToString();
                    entities.Ctrlno = reader["Ctrlno"].ToString();
                    entities.Supplier = reader["Supplier"].ToString();
                    entities.DivisionDisplay = reader["DivisionDisplay"].ToString();
                    entities.Head_Type = reader["Type"].ToString();
                    entities.Specification = reader["Specification"] != DBNull.Value ? reader["Specification"].ToString() : string.Empty;
                    entities.Size = reader["Size"] != DBNull.Value ? reader["Size"].ToString() : string.Empty;
                    entities.BoxType = reader["BoxType"] != DBNull.Value ? reader["BoxType"].ToString() : string.Empty;
                    entities.WeightOfBox = reader["WeightOfBox"] != DBNull.Value ? reader["WeightOfBox"].ToString() : string.Empty;
                    entities.GrossWeight = reader["GrossWeight"] != DBNull.Value ? reader["GrossWeight"].ToString() : string.Empty;
                    entities.NetWeight = reader["NetWeight"] != DBNull.Value ? reader["NetWeight"].ToString() : string.Empty;
                    entities.Multiplier = reader["Multiplier"] != DBNull.Value ? reader["Multiplier"].ToString() : string.Empty;
                    entities.NoOfBoxes = reader["NoOfBoxes"] != DBNull.Value ? reader["NoOfBoxes"].ToString() : string.Empty;
                    entities.Quantity = reader["Quantity"] != DBNull.Value ? reader["Quantity"].ToString() : string.Empty;
                    entities.SrfItemName = reader["SrfItemName"] != DBNull.Value ? reader["SrfItemName"].ToString() : string.Empty;

                    entities.Head_RequesterId = reader["Requester"].ToString();
                    entities.Head_Requester = reader["RequesterName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["RequesterName"].ToString(), true) + "<BR />" + reader["TransactionDate"].ToString() : "PENDING APPROVAL";
                    entities.Head_ProdManager = reader["ProdManagerName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["ProdManagerName"].ToString(), true) + "<BR />" + reader["DOAProdManager"].ToString() : "PENDING APPROVAL";
                    entities.Head_Buyer = reader["BuyerName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["BuyerName"].ToString(), true) + "<BR />" + reader["DOABuyer"].ToString() : "PENDING APPROVAL";
                    entities.Head_SCIncharge = reader["SCDInchargeName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["SCDInchargeName"].ToString(), true) + "<BR />" + reader["DOASCIncharge"].ToString() : "PENDING APPROVAL";

                    entities.Head_StatProdManager = reader["StatProdManager"] != DBNull.Value ? reader["StatProdManager"].ToString() : "0";
                    entities.Head_StatBuyer = reader["StatBuyer"] != DBNull.Value ? reader["StatBuyer"].ToString() : "0";
                    entities.Head_StatSCIncharge = reader["StatSCIncharge"] != DBNull.Value ? reader["StatSCIncharge"].ToString() : "0";


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

        public List<Entities_SRF_PO_Entry> SRF_TRANSACTION_PO_TOP_10_REQUEST(string date, string problemEncountered, string srf_head)
        {
            List<Entities_SRF_PO_Entry> list = new List<Entities_SRF_PO_Entry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT TOP 10 HEAD.CTRLNo, HEAD.TotalBoxes, HEAD.TotaQuantity, TransactionDate, LC.FullName AS Requester, HEAD.Type " +
                                    "FROM SRF_TRANSACTION_PO_Head HEAD WITH (NOLOCK) " +
                                    "INNER JOIN Login_Credentials LC WITH (NOLOCK) ON HEAD.Requester = LC.RefId " +
                                    "WHERE (HEAD.StatBuyer != 2 OR HEAD.StatProdManager != 2 OR HEAD.StatSCIncharge != 2) " + 
                                    "AND HEAD.Type = '" + problemEncountered + "' " +
                                    "AND CONVERT(DATE, HEAD.TransactionDate) = '" + date + "' AND HEAD.SRF_HEAD = '" + srf_head + "'";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_PO_Entry entities = new Entities_SRF_PO_Entry();

                    entities.Head_Ctrlno = reader["CTRLNo"].ToString();
                    entities.Head_TotalQuantity = reader["TotaQuantity"].ToString();
                    entities.Head_TotalBoxes = reader["TotalBoxes"].ToString();
                    entities.Head_Requester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true).ToUpper();
                    entities.Head_TransactionDate = reader["TransactionDate"].ToString().ToUpper();
                    entities.Head_Type = reader["Type"].ToString();


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

        public List<Entities_SRF_PO_Entry> SRF_TRANSACTION_PO_WITH_SRF_HEAD(string SRF_HEAD)
        {
            List<Entities_SRF_PO_Entry> list = new List<Entities_SRF_PO_Entry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CTRLNO, SRF_HEAD FROM SRF_TRANSACTION_PO_Head WHERE SRF_HEAD = '" + SRF_HEAD + "'";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_PO_Entry entities = new Entities_SRF_PO_Entry();

                    entities.Ctrlno = reader["CTRLNo"].ToString();
                    entities.Head_Ctrlno = reader["SRF_HEAD"].ToString();


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

        #region SRF TRANSACTION ENTRY

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestEntry_Fill_All_DropdownList()
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_TRANSACTION_RequestEntry_Fill_All_DropdownList";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestEntry_ByControlNo(string controlno)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_TRANSACTION_RequestEntry_ByControlNo";
                cmd.Parameters.Add(Factory.CreateParameter("@controlno", controlno = controlno.Length <= 0 ? string.Empty : controlno));

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.RefId = reader["RefId"].ToString();
                    entities.CtrlNo = reader["CtrlNo"].ToString();
                    entities.Requester = reader["Requester"].ToString();
                    entities.Category = reader["Category"].ToString();
                    entities.Supplier = reader["Supplier"].ToString();
                    entities.Attention = reader["Attention"].ToString();                    
                    entities.TotalQuantity = reader["TotalQuantity"].ToString();
                    entities.PullOutServiceDate = reader["PullOutServiceDate"].ToString();
                    entities.DeliveryDateToRepi = reader["DeliveryDateToRepi"].ToString();
                    entities.ProblemEncountered = reader["ProblemEncountered"].ToString();
                    entities.PurposeOfPullOut = reader["PurposeOfPullOut"].ToString();
                    entities.TotalValueInUsd = reader["TotalValueInUsd"].ToString();
                    entities.LoaNo = reader["LoaNo"].ToString();
                    entities.LoaSuretyBond = reader["LoaSuretyBond"].ToString();
                    entities.Loa8106 = reader["Loa8106"].ToString();
                    entities.Attachment = reader["Attachment"].ToString();
                    entities.Remarks = reader["Remarks"].ToString();
                    entities.GatePassNo = reader["GatePassNo"].ToString();
                    entities.PickUpPoint = reader["PickUpPoint"].ToString();
                    entities.LOADescription = reader["LOADescription"].ToString();
                    entities.POPDescription = reader["POPDescription"].ToString();
                    entities.CategoryDescription = reader["CategoryDescription"].ToString();
                    entities.TransactionDate = reader["TransactionDate"].ToString();
                    entities.SupplierName = reader["SupplierName"].ToString();
                    entities.PurposeOfPullOutName = reader["PurposeOfPullOutName"].ToString();
                    entities.Document8105 = reader["Document8105"].ToString();

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

        public int SRF_TRANSACTION_RequestEntry_Insert(Entities_SRF_RequestEntry entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_TRANSACTION_RequestEntry_Insert";

            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
            cmd.Parameters.Add(Factory.CreateParameter("@requester", entity.Requester = entity.Requester.Length <= 0 ? string.Empty : entity.Requester));
            cmd.Parameters.Add(Factory.CreateParameter("@category", entity.Category = entity.Category.Length <= 0 ? string.Empty : entity.Category));
            cmd.Parameters.Add(Factory.CreateParameter("@supplier", entity.Supplier = entity.Supplier.Length <= 0 ? string.Empty : entity.Supplier));
            cmd.Parameters.Add(Factory.CreateParameter("@attention", entity.Attention = entity.Attention.Length <= 0 ? string.Empty : entity.Attention));           
            cmd.Parameters.Add(Factory.CreateParameter("@totalquantity", entity.TotalQuantity = entity.TotalQuantity.Length <= 0 ? string.Empty : entity.TotalQuantity));
            cmd.Parameters.Add(Factory.CreateParameter("@pulloutservicedate", entity.PullOutServiceDate = entity.PullOutServiceDate.Length <= 0 ? string.Empty : entity.PullOutServiceDate));
            cmd.Parameters.Add(Factory.CreateParameter("@deliverydatetorepi", entity.DeliveryDateToRepi = entity.DeliveryDateToRepi.Length <= 0 ? string.Empty : entity.DeliveryDateToRepi));
            cmd.Parameters.Add(Factory.CreateParameter("@problemencountered", entity.ProblemEncountered = entity.ProblemEncountered.Length <= 0 ? string.Empty : entity.ProblemEncountered));
            cmd.Parameters.Add(Factory.CreateParameter("@purposeofpullout", entity.PurposeOfPullOut = entity.PurposeOfPullOut.Length <= 0 ? string.Empty : entity.PurposeOfPullOut));
            cmd.Parameters.Add(Factory.CreateParameter("@totalvalueinusd", entity.TotalValueInUsd = entity.TotalValueInUsd.Length <= 0 ? string.Empty : entity.TotalValueInUsd));
            cmd.Parameters.Add(Factory.CreateParameter("@loano", entity.LoaNo = entity.LoaNo.Length <= 0 ? string.Empty : entity.LoaNo));
            cmd.Parameters.Add(Factory.CreateParameter("@loasuretybond", entity.LoaSuretyBond = entity.LoaSuretyBond.Length <= 0 ? string.Empty : entity.LoaSuretyBond));
            cmd.Parameters.Add(Factory.CreateParameter("@loa8106", entity.Loa8106 = entity.Loa8106.Length <= 0 ? string.Empty : entity.Loa8106));
            cmd.Parameters.Add(Factory.CreateParameter("@attachment", entity.Attachment = entity.Attachment.Length <= 0 ? string.Empty : entity.Attachment));
            cmd.Parameters.Add(Factory.CreateParameter("@remarks", entity.Remarks = entity.Remarks.Length <= 0 ? string.Empty : entity.Remarks));
            cmd.Parameters.Add(Factory.CreateParameter("@getpassno", entity.GatePassNo = entity.GatePassNo.Length <= 0 ? string.Empty : entity.GatePassNo));
            cmd.Parameters.Add(Factory.CreateParameter("@pickuppoint", entity.PickUpPoint = entity.PickUpPoint.Length <= 0 ? string.Empty : entity.PickUpPoint));
            cmd.Parameters.Add(Factory.CreateParameter("@document8105", entity.Document8105 = entity.Document8105.Length <= 0 ? string.Empty : entity.Document8105));
            
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

        public int SRF_TRANSACTION_RequestEntry_Details_Insert(Entities_SRF_RequestEntry entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_TRANSACTION_RequestEntry_Details_Insert";

            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
            cmd.Parameters.Add(Factory.CreateParameter("@refprpo", entity.RefPRPO = entity.RefPRPO.Length <= 0 ? string.Empty : entity.RefPRPO));
            cmd.Parameters.Add(Factory.CreateParameter("@salesinvoice", entity.SalesInvoice = entity.SalesInvoice.Length <= 0 ? string.Empty : entity.SalesInvoice));
            cmd.Parameters.Add(Factory.CreateParameter("@brandmachinename", entity.BrandMachineName = entity.BrandMachineName.Length <= 0 ? string.Empty : entity.BrandMachineName));
            cmd.Parameters.Add(Factory.CreateParameter("@itemname", entity.ItemName = entity.ItemName.Length <= 0 ? string.Empty : entity.ItemName));
            cmd.Parameters.Add(Factory.CreateParameter("@specification", entity.Specification = entity.Specification.Length <= 0 ? string.Empty : entity.Specification));
            cmd.Parameters.Add(Factory.CreateParameter("@quantity", entity.Quantity = entity.Quantity.Length <= 0 ? string.Empty : entity.Quantity));
            cmd.Parameters.Add(Factory.CreateParameter("@unitofmeasure", entity.UnitOfMeasure = entity.UnitOfMeasure.Length <= 0 ? string.Empty : entity.UnitOfMeasure));
            cmd.Parameters.Add(Factory.CreateParameter("@serialno", entity.SerialNo = entity.SerialNo.Length <= 0 ? string.Empty : entity.SerialNo));
            

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

        public int SRF_TRANSACTION_RequestEntry_Details_Delete(Entities_SRF_RequestEntry entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_TRANSACTION_RequestEntry_Details_Delete";

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

        public int SRF_TRANSACTION_RequestEntry_Update(Entities_SRF_RequestEntry entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_TRANSACTION_RequestEntry_Update";

            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
            cmd.Parameters.Add(Factory.CreateParameter("@category", entity.Category = entity.Category.Length <= 0 ? string.Empty : entity.Category));
            cmd.Parameters.Add(Factory.CreateParameter("@supplier", entity.Supplier = entity.Supplier.Length <= 0 ? string.Empty : entity.Supplier));
            cmd.Parameters.Add(Factory.CreateParameter("@attention", entity.Attention = entity.Attention.Length <= 0 ? string.Empty : entity.Attention));
            cmd.Parameters.Add(Factory.CreateParameter("@totalquantity", entity.TotalQuantity = entity.TotalQuantity.Length <= 0 ? string.Empty : entity.TotalQuantity));
            cmd.Parameters.Add(Factory.CreateParameter("@pulloutservicedate", entity.PullOutServiceDate = entity.PullOutServiceDate.Length <= 0 ? string.Empty : entity.PullOutServiceDate));
            cmd.Parameters.Add(Factory.CreateParameter("@deliverydatetorepi", entity.DeliveryDateToRepi = entity.DeliveryDateToRepi.Length <= 0 ? string.Empty : entity.DeliveryDateToRepi));
            cmd.Parameters.Add(Factory.CreateParameter("@problemencountered", entity.ProblemEncountered = entity.ProblemEncountered.Length <= 0 ? string.Empty : entity.ProblemEncountered));
            cmd.Parameters.Add(Factory.CreateParameter("@purposeofpullout", entity.PurposeOfPullOut = entity.PurposeOfPullOut.Length <= 0 ? string.Empty : entity.PurposeOfPullOut));
            cmd.Parameters.Add(Factory.CreateParameter("@totalvalueinusd", entity.TotalValueInUsd = entity.TotalValueInUsd.Length <= 0 ? string.Empty : entity.TotalValueInUsd));
            cmd.Parameters.Add(Factory.CreateParameter("@loano", entity.LoaNo = entity.LoaNo.Length <= 0 ? string.Empty : entity.LoaNo));
            cmd.Parameters.Add(Factory.CreateParameter("@loasuretybond", entity.LoaSuretyBond = entity.LoaSuretyBond.Length <= 0 ? string.Empty : entity.LoaSuretyBond));
            cmd.Parameters.Add(Factory.CreateParameter("@loa8106", entity.Loa8106 = entity.Loa8106.Length <= 0 ? string.Empty : entity.Loa8106));
            cmd.Parameters.Add(Factory.CreateParameter("@attachment", entity.Attachment = entity.Attachment.Length <= 0 ? string.Empty : entity.Attachment));
            cmd.Parameters.Add(Factory.CreateParameter("@remarks", entity.Remarks = entity.Remarks.Length <= 0 ? string.Empty : entity.Remarks));
            cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.UpdatedBy = entity.UpdatedBy.Length <= 0 ? string.Empty : entity.UpdatedBy));
            cmd.Parameters.Add(Factory.CreateParameter("@getpassno", entity.GatePassNo = entity.GatePassNo.Length <= 0 ? string.Empty : entity.GatePassNo));
            cmd.Parameters.Add(Factory.CreateParameter("@pickuppoint", entity.PickUpPoint = entity.PickUpPoint.Length <= 0 ? string.Empty : entity.PickUpPoint));
            cmd.Parameters.Add(Factory.CreateParameter("@document8105", entity.Document8105 = entity.Document8105.Length <= 0 ? string.Empty : entity.Document8105));

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


        public int SRF_TRANSACTION_RequestEntry_Update_GPNO_ByControlNo(Entities_SRF_RequestEntry entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_TRANSACTION_RequestEntry_Update_GPNO_ByControlNo";

            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
            cmd.Parameters.Add(Factory.CreateParameter("@getpassno", entity.GatePassNo = entity.GatePassNo.Length <= 0 ? string.Empty : entity.GatePassNo));

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

        public Int32 SRF_TRANSACTION_Request_Count(string year)
        {
            Int32 result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_TRANSACTION_Request_Count";

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

        public int SRF_TRANSACTION_Status_Insert(Entities_SRF_RequestEntry entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_TRANSACTION_Status_Insert";

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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestEntry_Details_ByControlNo(Entities_SRF_RequestEntry entity)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            int number = 1;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_TRANSACTION_RequestEntry_Details_ByControlNo";
                cmd.Parameters.Add(Factory.CreateParameter("@controlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.RefId = reader["RefId"].ToString();
                    entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.RefPRPO = reader["RefPRPO"].ToString().ToUpper();
                    entities.SalesInvoice = reader["SalesInvoice"].ToString().ToUpper();
                    entities.BrandMachineName = reader["BrandMachineName"].ToString().ToUpper();
                    entities.ItemName = reader["ItemName"].ToString().ToUpper();
                    entities.Specification = reader["ItemSpecification"].ToString().ToUpper();
                    entities.TotalQuantity = reader["TotalQuantity"].ToString().ToUpper();
                    entities.UnitOfMeasure = reader["UnitOfMeasure"].ToString().ToUpper();
                    entities.SerialNo = reader["SerialNo"].ToString().ToUpper();
                    entities.UOM_Description = reader["UOM_Description"].ToString().ToUpper();
                    entities.Supplier = reader["SupplierName"].ToString().ToUpper();
                    entities.No = number.ToString();

                    list.Add(entities);

                    number++;
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

        public Int32 SRF_TRANSACTION_Warehouse_ActualDelivery_Attachment_Count(string srfNumber)
        {
            Int32 result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT COUNT(*) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber ='" + srfNumber + "' AND LEN(Attachment) > 0";

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

        public Int32 SRF_TRANSACTION_Warehouse_ActualDelivery_Attachment_Count_ForWarehouse(string srfNumber)
        {
            Int32 result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT COUNT(*) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber ='" + srfNumber + "' AND LEN(AttachmentWarehouse) > 0";

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

        public Int32 SRF_TRANSACTION_Warehouse_Attachment_Count(string srfNumber)
        {
            Int32 result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT COUNT(*) FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo ='" + srfNumber + "' AND LEN(Attachment) > 0 AND TransType = '2'";

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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestEntry_Details_ByControlNo_ForWarehouse(string ctrlno)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            int number = 1;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT RD.*, UOM.[Description] AS UOM_Description, (SELECT Name FROM MT_Supplier_Head WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
                                  "(SELECT SUM(CONVERT(BIGINT, ActualQuantity)) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE DetailsRefId = RD.RefId) AS ActualQuantity, " +
                                  "(SELECT TOP 1 InProgress FROM SRF_TRANSACTION_Warehouse_InProgress WITH (NOLOCK) WHERE RDRefId = RD.RefId AND InProgress = '1' ORDER BY AddedDate DESC) AS InProgress " +
                                  "FROM SRF_TRANSACTION_Request_Details RD WITH (NOLOCK) " +
                                  "INNER JOIN MT_UnitOfMeasure UOM on RD.UnitOfMeasure = UOM.RefId " +
                                  "INNER JOIN SRF_TRANSACTION_Request HEAD WITH (NOLOCK) ON RD.CTRLNo = HEAD.CTRLNo " +
                                  "WHERE RD.CTRLNo = '" + ctrlno + "'";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.RefId = reader["RefId"].ToString();
                    entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.RefPRPO = reader["RefPRPO"].ToString().ToUpper();
                    entities.SalesInvoice = reader["SalesInvoice"].ToString().ToUpper();
                    entities.BrandMachineName = reader["BrandMachineName"].ToString().ToUpper();
                    entities.ItemName = reader["ItemName"].ToString().ToUpper();
                    entities.Specification = reader["ItemSpecification"].ToString().ToUpper();
                    entities.TotalQuantity = reader["TotalQuantity"].ToString().ToUpper();
                    entities.UnitOfMeasure = reader["UnitOfMeasure"].ToString().ToUpper();
                    entities.SerialNo = reader["SerialNo"].ToString().ToUpper();
                    entities.UOM_Description = reader["UOM_Description"].ToString().ToUpper();
                    entities.Supplier = reader["SupplierName"].ToString().ToUpper();
                    entities.No = number.ToString();
                    entities.Warehouse_TotalActualQuantity = reader["ActualQuantity"] != DBNull.Value ? reader["ActualQuantity"].ToString() : string.Empty;
                    entities.InProgress = reader["InProgress"] != DBNull.Value ? reader["InProgress"].ToString() : string.Empty;
                    entities.RemarksFromWarehouse = reader["RemarksFromWarehouse"] != DBNull.Value ? reader["RemarksFromWarehouse"].ToString() : string.Empty;

                    int orderedQty = int.Parse(entities.TotalQuantity);
                    int actualQty = int.Parse(!string.IsNullOrEmpty(entities.Warehouse_TotalActualQuantity) ? entities.Warehouse_TotalActualQuantity : "0");
                    int remainingQty = orderedQty - actualQty;

                    entities.Warehouse_RemainingQuantity = remainingQty.ToString();

                    list.Add(entities);

                    number++;
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_QuantityAdjustmentHistory(Entities_SRF_RequestEntry entity)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            int number = 1;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT RD.*, UOM.[Description] AS UOM_Description, (SELECT Name FROM MT_Supplier_Head WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
                                  "QTY.OriginalQuantity, QTY.UpdatedQuantity, QTY.Reason, QTY.UpdatedBy, QTY.UpdatedDate " +
                                  "FROM SRF_TRANSACTION_Request_Details RD WITH (NOLOCK) " +
                                  "INNER JOIN MT_UnitOfMeasure UOM on RD.UnitOfMeasure = UOM.RefId " +
                                  "INNER JOIN SRF_TRANSACTION_Request HEAD WITH (NOLOCK) ON RD.CTRLNo = HEAD.CTRLNo " +
                                  "INNER JOIN SRF_TRANSACTION_UpdatedQuantity QTY WITH (NOLOCK) ON RD.RefId = QTY.DetailsRefId " +
                                  "WHERE CONVERT(DATE, QTY.UpdatedDate) >= '" + entity.DrFrom + "' AND CONVERT(DATE, QTY.UpdatedDate) <= '" + entity.DrTo + "' " +
                                  "ORDER BY QTY.UpdatedDate DESC";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.RefId = reader["RefId"].ToString();
                    entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.RefPRPO = reader["RefPRPO"].ToString().ToUpper();
                    entities.SalesInvoice = reader["SalesInvoice"].ToString().ToUpper();
                    entities.BrandMachineName = reader["BrandMachineName"].ToString().ToUpper();
                    entities.ItemName = reader["ItemName"].ToString().ToUpper();
                    entities.Specification = reader["ItemSpecification"].ToString().ToUpper();
                    entities.TotalQuantity = reader["TotalQuantity"].ToString().ToUpper();
                    entities.UnitOfMeasure = reader["UnitOfMeasure"].ToString().ToUpper();
                    entities.SerialNo = reader["SerialNo"].ToString().ToUpper();
                    entities.UOM_Description = reader["UOM_Description"].ToString().ToUpper();
                    entities.Supplier = reader["SupplierName"].ToString().ToUpper();
                    entities.No = number.ToString();


                    entities.Uq_OriginalQuantity = reader["OriginalQuantity"].ToString().ToUpper();
                    entities.Uq_UpdatedQuantity = reader["UpdatedQuantity"].ToString().ToUpper();
                    entities.Uq_Reason = reader["Reason"].ToString().ToUpper();
                    entities.Uq_UpdatedBy = reader["UpdatedBy"].ToString().ToUpper();
                    entities.Uq_UpdatedDate = reader["UpdatedDate"].ToString().ToUpper();


                    list.Add(entities);

                    number++;
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestEntry_Details_ByControlNo_ForWarehouse_ActualProgress(string ctrlno)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            int number = 1;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT RD.*, UOM.[Description] AS UOM_Description, (SELECT Name FROM MT_Supplier_Head WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
                                  "(SELECT SUM(CONVERT(BIGINT, ActualQuantity)) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE DetailsRefId = RD.RefId) AS ActualQuantity, " +
                                  "(SELECT RefId FROM SRF_TRANSACTION_Warehouse_InProgress WITH (NOLOCK) WHERE RDRefId = RD.RefId AND InProgress = '1') AS InProgressRefId " +
                                  "FROM SRF_TRANSACTION_Request_Details RD WITH (NOLOCK) " +
                                  "INNER JOIN MT_UnitOfMeasure UOM on RD.UnitOfMeasure = UOM.RefId " +
                                  "INNER JOIN SRF_TRANSACTION_Request HEAD WITH (NOLOCK) ON RD.CTRLNo = HEAD.CTRLNo " +
                                  "WHERE RD.CTRLNo = '" + ctrlno + "' " +
                                  "AND RD.RefId IN (SELECT CONVERT(VARCHAR(50), RDRefId) FROM SRF_TRANSACTION_Warehouse_InProgress where CTRLNo = '" + ctrlno + "' AND InProgress = '1')";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.RefId = reader["RefId"].ToString();
                    entities.InProgressRefId = reader["InProgressRefId"].ToString();
                    entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.RefPRPO = reader["RefPRPO"].ToString().ToUpper();
                    entities.SalesInvoice = reader["SalesInvoice"].ToString().ToUpper();
                    entities.BrandMachineName = reader["BrandMachineName"].ToString().ToUpper();
                    entities.ItemName = reader["ItemName"].ToString().ToUpper();
                    entities.Specification = reader["ItemSpecification"].ToString().ToUpper();
                    entities.TotalQuantity = reader["TotalQuantity"].ToString().ToUpper();
                    entities.UnitOfMeasure = reader["UnitOfMeasure"].ToString().ToUpper();
                    entities.SerialNo = reader["SerialNo"].ToString().ToUpper();
                    entities.UOM_Description = reader["UOM_Description"].ToString().ToUpper();
                    entities.Supplier = reader["SupplierName"].ToString().ToUpper();
                    entities.No = number.ToString();
                    entities.Warehouse_TotalActualQuantity = reader["ActualQuantity"] != DBNull.Value ? reader["ActualQuantity"].ToString() : string.Empty;

                    int orderedQty = int.Parse(entities.TotalQuantity);
                    int actualQty = int.Parse(!string.IsNullOrEmpty(entities.Warehouse_TotalActualQuantity) ? entities.Warehouse_TotalActualQuantity : "0");
                    int remainingQty = orderedQty - actualQty;

                    entities.Warehouse_RemainingQuantity = remainingQty.ToString();

                    list.Add(entities);

                    number++;
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_WarehouseItemsWithoutDocuments()
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            int number = 1;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ACTUAL.DetailsRefId, ACTUAL.SRFNumber, ACTUAL.ActualQuantity, ACTUAL.AddedDate AS DeliveredDate, " +
                                  "(SELECT ItemName FROM SRF_TRANSACTION_Request_Details WITH (NOLOCK) WHERE Refid = ACTUAL.DetailsRefId) AS ItemName, " +
                                  "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = ACTUAL.AddedBy) AS RequesterName, " +
                                  "ACTUAL.WithDocuments, ACTUAL.LOA8105, ACTUAL.LOA8105ProcessDate " +
                                  "FROM SRF_TRANSACTION_Warehouse_Actual_Delivery ACTUAL WITH (NOLOCK) " +
                                  "WHERE WithDocuments IS NULL OR WithDocuments = '0' AND DATEADD(dd, 0, DATEDIFF(dd, 0, CONVERT(DATE, AddedDate))) < DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) " +
                                  "AND ACTUAL.SRFNumber IN ((SELECT CTRLNo FROM SRF_TRANSACTION_Status WITH (NOLOCK) WHERE STATPur_Manager = 1 AND CONVERT(DATE, DOAPur_Manager) >= '12/19/2022')) ";


                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.No = number.ToString();
                    entities.Warehouse_DetailsRefId = reader["DetailsRefId"].ToString();
                    entities.Warehouse_CtrlNo = reader["SRFNumber"].ToString();
                    entities.Warehouse_TotalActualQuantity = reader["ActualQuantity"].ToString();
                    entities.Warehouse_DeliveredDate = reader["DeliveredDate"].ToString();
                    entities.Warehouse_ItemName = reader["ItemName"].ToString();
                    entities.Warehouse_RequesterName = CryptorEngine.Decrypt(reader["RequesterName"].ToString(), true).ToUpper();
                    entities.Warehouse_8105 = reader["LOA8105"].ToString();
                    entities.Warehouse_LOA8105ProcessDate = reader["LOA8105ProcessDate"].ToString();
                    

                    list.Add(entities);

                    number++;
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestEntry_Details_ByControlNo_ForWarehouse2(string ctrlno)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            int number = 1;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT RD.*, UOM.[Description] AS UOM_Description, (SELECT Name FROM MT_Supplier_Head WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
                                  "(SELECT SUM(CONVERT(BIGINT, ActualQuantity)) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE DetailsRefId = RD.RefId) AS ActualQuantity " +
                                  "FROM SRF_TRANSACTION_Request_Details RD WITH (NOLOCK) " +
                                  "INNER JOIN MT_UnitOfMeasure UOM on RD.UnitOfMeasure = UOM.RefId " +
                                  "INNER JOIN SRF_TRANSACTION_Request HEAD WITH (NOLOCK) ON RD.CTRLNo = HEAD.CTRLNo " +
                                  "WHERE RD.CTRLNo = '" + ctrlno + "' " +
                                  "AND RD.RefId IN (SELECT RDRefId FROM SRF_TRANSACTION_Warehouse_InProgress WITH (NOLOCK) WHERE CTRLNo = '" + ctrlno + "' AND InProgress = '1')";


                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.RefId = reader["RefId"].ToString();
                    entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.RefPRPO = reader["RefPRPO"].ToString().ToUpper();
                    entities.SalesInvoice = reader["SalesInvoice"].ToString().ToUpper();
                    entities.BrandMachineName = reader["BrandMachineName"].ToString().ToUpper();
                    entities.ItemName = reader["ItemName"].ToString().ToUpper();
                    entities.Specification = reader["ItemSpecification"].ToString().ToUpper();
                    entities.TotalQuantity = reader["TotalQuantity"].ToString().ToUpper();
                    entities.UnitOfMeasure = reader["UnitOfMeasure"].ToString().ToUpper();
                    entities.SerialNo = reader["SerialNo"].ToString().ToUpper();
                    entities.UOM_Description = reader["UOM_Description"].ToString().ToUpper();
                    entities.Supplier = reader["SupplierName"].ToString().ToUpper();
                    entities.No = number.ToString();
                    entities.Warehouse_TotalActualQuantity = reader["ActualQuantity"] != DBNull.Value ? reader["ActualQuantity"].ToString() : string.Empty;

                    list.Add(entities);

                    number++;
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_GetActualDeliveryByCTRLNo(string ctrlno)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            int number = 1;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ACTUAL.*, DETAILS.ItemName " +
                                  "FROM SRF_TRANSACTION_Warehouse_Actual_Delivery ACTUAL WITH (NOLOCK) " +
                                  "INNER JOIN SRF_TRANSACTION_Request_Details DETAILS WITH (NOLOCK) ON ACTUAL.DetailsRefId = DETAILS.RefId " +
                                  "WHERE ACTUAL.SRFNumber = '" + ctrlno + "' AND ACTUAL.WithDocuments = '1' ORDER BY DETAILS.ItemName ASC";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    string withDocs = reader["WithDocuments"] != DBNull.Value ? reader["WithDocuments"].ToString() : string.Empty;


                    entities.RefId = reader["RefId"].ToString();
                    entities.Warehouse_CtrlNo = reader["SRFNumber"].ToString().ToUpper();
                    entities.Warehouse_DetailsRefId = reader["DetailsRefId"].ToString().ToUpper();
                    entities.Warehouse_ItemName = reader["ItemName"].ToString().ToUpper();
                    entities.Warehouse_TotalActualQuantity = reader["ActualQuantity"].ToString().ToUpper();
                    entities.Warehouse_8105 = reader["LOA8105"] != DBNull.Value ? reader["LOA8105"].ToString() : string.Empty;
                    entities.Warehouse_Attachment = reader["Attachment"] != DBNull.Value ? reader["Attachment"].ToString() : string.Empty;
                    entities.Warehouse_AttachmentWarehouse = reader["AttachmentWarehouse"] != DBNull.Value ? reader["AttachmentWarehouse"].ToString() : string.Empty;
                    entities.Warehouse_DeliveredDate = reader["AddedDate"] != DBNull.Value ? reader["AddedDate"].ToString() : string.Empty;
                    entities.Warehouse_LOA8105ProcessDate = reader["LOA8105ProcessDate"] != DBNull.Value ? reader["LOA8105ProcessDate"].ToString() : string.Empty;
                    entities.Warehouse_WithDocuments = withDocs == "1" ? "YES" : "NO";


                    list.Add(entities);

                    number++;
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_GetActualDeliveryByCTRLNoWithoutDocuments(string ctrlno)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            int number = 1;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ACTUAL.*, DETAILS.ItemName " +
                                  "FROM SRF_TRANSACTION_Warehouse_Actual_Delivery ACTUAL WITH (NOLOCK) " +
                                  "INNER JOIN SRF_TRANSACTION_Request_Details DETAILS WITH (NOLOCK) ON ACTUAL.DetailsRefId = DETAILS.RefId " +
                                  "WHERE ACTUAL.SRFNumber = '" + ctrlno + "' AND ACTUAL.WithDocuments = '0' ORDER BY DETAILS.ItemName ASC";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    string withDocs = reader["WithDocuments"] != DBNull.Value ? reader["WithDocuments"].ToString() : string.Empty;


                    entities.RefId = reader["RefId"].ToString();
                    entities.Warehouse_CtrlNo = reader["SRFNumber"].ToString().ToUpper();
                    entities.Warehouse_DetailsRefId = reader["DetailsRefId"].ToString().ToUpper();
                    entities.Warehouse_ItemName = reader["ItemName"].ToString().ToUpper();
                    entities.Warehouse_TotalActualQuantity = reader["ActualQuantity"].ToString().ToUpper();
                    entities.Warehouse_8105 = reader["LOA8105"] != DBNull.Value ? reader["LOA8105"].ToString() : string.Empty;
                    entities.Warehouse_Attachment = reader["Attachment"] != DBNull.Value ? reader["Attachment"].ToString() : string.Empty;
                    entities.Warehouse_DeliveredDate = reader["AddedDate"] != DBNull.Value ? reader["AddedDate"].ToString() : string.Empty;
                    entities.Warehouse_LOA8105ProcessDate = reader["LOA8105ProcessDate"] != DBNull.Value ? reader["LOA8105ProcessDate"].ToString() : string.Empty;
                    entities.Warehouse_WithDocuments = withDocs == "1" ? "YES" : "NO";


                    list.Add(entities);

                    number++;
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_GetInProgressByCTRLNo(string ctrlno)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            int number = 1;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * " +
                                  "FROM SRF_TRANSACTION_Warehouse_InProgress WITH (NOLOCK) " +
                                  "WHERE CTRLNo = '" + ctrlno + "' AND InProgress = '1' ORDER BY ItemName ASC";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.RefId = reader["RDRefId"].ToString();
                    entities.Warehouse_CtrlNo = reader["CTRLNo"].ToString().ToUpper();
                    entities.Warehouse_ItemName = reader["ItemName"].ToString().ToUpper();


                    list.Add(entities);

                    number++;
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Get_8105_By_CTRLNo(string ctrlno)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            int number = 1;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM SRF_TRANSACTION_8105 WITH (NOLOCK) " +
                                  "WHERE CTRLNo = '" + ctrlno + "'";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.EightOneZeroFive_RefId = reader["RefId"].ToString();
                    entities.EightOneZeroFive_No = reader["No"].ToString();
                    entities.EightOneZeroFive_CTRLNo = reader["CTRLNo"].ToString();
                    entities.EightOneZeroFive_8105 = reader["LOA8105"].ToString();
                    entities.EightOneZeroFive_Quantity = reader["Quantity"].ToString();
                    entities.EightOneZeroFive_Attachment = reader["Attachment"].ToString();
                    entities.EightOneZeroFive_AddedBy = reader["AddedBy"].ToString();
                    entities.EightOneZeroFive_AddedDate = reader["AddedDate"].ToString();

                    list.Add(entities);

                    number++;
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like(Entities_SRF_RequestEntry entity)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like";
                cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
                cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
                cmd.Parameters.Add(Factory.CreateParameter("@requester", entity.Requester = entity.Requester.Length <= 0 ? string.Empty : entity.Requester));
                cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
                cmd.Parameters.Add(Factory.CreateParameter("@category", entity.Category = entity.Category.Length <= 0 ? string.Empty : entity.Category));

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.RefId = reader["RefId"].ToString();
                    entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.ReqIncharge = reader["Req_Incharge"].ToString();
                    entities.ReqManager = reader["Req_Manager"].ToString();
                    entities.PurIncharge = reader["Pur_Incharge"].ToString();
                    entities.PurImpex = reader["Pur_Manager"].ToString();
                    entities.ReqInchargeDOA = reader["DOAReq_Incharge"].ToString();
                    entities.ReqManagerDOA = reader["DOAReq_Manager"].ToString();
                    entities.PurInchargeDOA = reader["DOAPur_Incharge"].ToString();
                    entities.PurImpexDOA = reader["DOAPur_Manager"].ToString();
                    entities.ReqInchargeStat = reader["STATReq_Incharge"].ToString();
                    entities.ReqManagerStat = reader["STATReq_Manager"].ToString();
                    entities.PurInchargeStat = reader["STATPur_Incharge"].ToString();
                    entities.PurImpexStat = reader["STATPur_Manager"].ToString();
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like2(Entities_SRF_RequestEntry entity)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like2";
                cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
                cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
                cmd.Parameters.Add(Factory.CreateParameter("@requester", entity.Requester = entity.Requester.Length <= 0 ? string.Empty : entity.Requester));
                cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.RefId = reader["RefId"].ToString();
                    entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.ReqIncharge = reader["Req_Incharge"].ToString();
                    entities.ReqManager = reader["Req_Manager"].ToString();
                    entities.PurIncharge = reader["Pur_Incharge"].ToString();
                    entities.PurImpex = reader["Pur_Manager"].ToString();
                    entities.ReqInchargeDOA = reader["DOAReq_Incharge"].ToString();
                    entities.ReqManagerDOA = reader["DOAReq_Manager"].ToString();
                    entities.PurInchargeDOA = reader["DOAPur_Incharge"].ToString();
                    entities.PurImpexDOA = reader["DOAPur_Manager"].ToString();
                    entities.ReqInchargeStat = reader["STATReq_Incharge"].ToString();
                    entities.ReqManagerStat = reader["STATReq_Manager"].ToString();
                    entities.PurInchargeStat = reader["STATPur_Incharge"].ToString();
                    entities.PurImpexStat = reader["STATPur_Manager"].ToString();
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestStatus_ByDateRange(Entities_SRF_RequestEntry entity)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_TRANSACTION_RequestStatus_ByDateRange";
                cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
                cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
                cmd.Parameters.Add(Factory.CreateParameter("@requester", entity.Requester = entity.Requester.Length <= 0 ? string.Empty : entity.Requester));
                cmd.Parameters.Add(Factory.CreateParameter("@category", entity.Category = entity.Category.Length <= 0 ? string.Empty : entity.Category));

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.RefId = reader["RefId"].ToString();
                    entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.ReqIncharge = reader["Req_Incharge"].ToString();
                    entities.ReqManager = reader["Req_Manager"].ToString();
                    entities.PurIncharge = reader["Pur_Incharge"].ToString();
                    entities.PurImpex = reader["Pur_Manager"].ToString();
                    entities.ReqInchargeDOA = reader["DOAReq_Incharge"].ToString();
                    entities.ReqManagerDOA = reader["DOAReq_Manager"].ToString();
                    entities.PurInchargeDOA = reader["DOAPur_Incharge"].ToString();
                    entities.PurImpexDOA = reader["DOAPur_Manager"].ToString();
                    entities.ReqInchargeStat = reader["STATReq_Incharge"].ToString();
                    entities.ReqManagerStat = reader["STATReq_Manager"].ToString();
                    entities.PurInchargeStat = reader["STATPur_Incharge"].ToString();
                    entities.PurImpexStat = reader["STATPur_Manager"].ToString();
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestStatus_ByDateRange2(Entities_SRF_RequestEntry entity)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_TRANSACTION_RequestStatus_ByDateRange2";
                cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
                cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
                cmd.Parameters.Add(Factory.CreateParameter("@requester", entity.Requester = entity.Requester.Length <= 0 ? string.Empty : entity.Requester));

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.RefId = reader["RefId"].ToString();
                    entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.ReqIncharge = reader["Req_Incharge"].ToString();
                    entities.ReqManager = reader["Req_Manager"].ToString();
                    entities.PurIncharge = reader["Pur_Incharge"].ToString();
                    entities.PurImpex = reader["Pur_Manager"].ToString();
                    entities.ReqInchargeDOA = reader["DOAReq_Incharge"].ToString();
                    entities.ReqManagerDOA = reader["DOAReq_Manager"].ToString();
                    entities.PurInchargeDOA = reader["DOAPur_Incharge"].ToString();
                    entities.PurImpexDOA = reader["DOAPur_Manager"].ToString();
                    entities.ReqInchargeStat = reader["STATReq_Incharge"].ToString();
                    entities.ReqManagerStat = reader["STATReq_Manager"].ToString();
                    entities.PurInchargeStat = reader["STATPur_Incharge"].ToString();
                    entities.PurImpexStat = reader["STATPur_Manager"].ToString();
                    entities.PurDeptManager = reader["Pur_DeptManager"].ToString();
                    entities.PurDeptManagerDOA = reader["DOAPur_DeptManager"].ToString();
                    entities.PurDeptManagerStat = reader["STATPur_DeptManager"].ToString();
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestStatus_ByControlNo(string ctrlno)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_TRANSACTION_RequestStatus_ByControlNo";
                cmd.Parameters.Add(Factory.CreateParameter("@controlno", ctrlno = ctrlno.Length <= 0 ? string.Empty : ctrlno));

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.RefId = reader["RefId"].ToString();
                    entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.ReqIncharge = reader["Req_Incharge"].ToString();
                    entities.ReqManager = reader["Req_Manager"].ToString();
                    entities.PurIncharge = reader["Pur_Incharge"].ToString();
                    entities.PurImpex = reader["Pur_Manager"].ToString();
                    entities.ReqInchargeDOA = reader["DOAReq_Incharge"].ToString();
                    entities.ReqManagerDOA = reader["DOAReq_Manager"].ToString();
                    entities.PurInchargeDOA = reader["DOAPur_Incharge"].ToString();
                    entities.PurImpexDOA = reader["DOAPur_Manager"].ToString();
                    entities.ReqInchargeStat = reader["STATReq_Incharge"].ToString();
                    entities.ReqManagerStat = reader["STATReq_Manager"].ToString();
                    entities.PurInchargeStat = reader["STATPur_Incharge"].ToString();
                    entities.PurImpexStat = reader["STATPur_Manager"].ToString();
                    entities.StatRemarks = reader["Remarks"].ToString();

                    entities.PurDeptManager = reader["Pur_DeptManager"].ToString();
                    entities.PurDeptManagerStat = reader["STATPur_DeptManager"].ToString();
                    entities.PurDeptManagerDOA = reader["DOAPur_DeptManager"].ToString();

                    entities.ReqInchargeName = reader["ReqInchargeName"].ToString();
                    entities.ReqManagerName = reader["ReqManagerName"].ToString();
                    entities.PurInchargeName = reader["PurInchargeName"].ToString();
                    entities.PurManagerName = reader["PurManagerName"].ToString();
                    entities.PurDeptManagerName = reader["PurDeptManagerName"].ToString();

                    entities.GatePassNo = reader["GatePassNo"] != DBNull.Value ? reader["GatePassNo"].ToString() : string.Empty;

                    list.Add(entities);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_AllRequest_Reports(string from, string to)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT REQUEST.CTRLNo, REQUEST.TotalQuantity, REQUEST.PullOutServiceDate, REQUEST.DeliveryDateToRepi, REQUEST.ProblemEncountered, REQUEST.Attention, " +
                                  "REQUEST.TotalValueInUsd, REQUEST.LOANo, REQUEST.LOASuretyBond, REQUEST.LOA8106, REQUEST.TransactionDate, REQUEST.GatePassNo, REQUEST.PickUpPoint, REQUEST.Remarks, " +
                                  "LC.FullName AS Requester, CATEGORY.[Description] AS Category, SUPPLIER.Name AS Supplier, PULLOUT.Name AS PurposeOfPullOut " +
                                  "FROM SRF_TRANSACTION_Request REQUEST WITH (NOLOCK) " +
                                  "INNER JOIN Login_Credentials LC WITH (NOLOCK) ON REQUEST.Requester = LC.RefId " +
                                  "INNER JOIN MT_Category CATEGORY WITH (NOLOCK) ON REQUEST.Category = CATEGORY.RefId " +
                                  "INNER JOIN MT_Supplier_Head SUPPLIER WITH (NOLOCK) ON REQUEST.Supplier = SUPPLIER.RefId " +
                                  "INNER JOIN SRF_MT_PurposeOfPullOut PULLOUT WITH (NOLOCK) ON REQUEST.PurposeOfPullOut = PULLOUT.RefId " +
                                  "WHERE CONVERT(DATE, TransactionDate) >= '" + from + "' AND CONVERT(DATE, TransactionDate) <= '" + to + "' ";	

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.Requester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true).ToUpper();
                    entities.TotalQuantity = reader["TotalQuantity"] != DBNull.Value ? reader["TotalQuantity"].ToString() : string.Empty;
                    entities.PullOutServiceDate = reader["PullOutServiceDate"] != DBNull.Value ? reader["PullOutServiceDate"].ToString() : string.Empty;
                    entities.DeliveryDateToRepi = reader["DeliveryDateToRepi"] != DBNull.Value ? reader["DeliveryDateToRepi"].ToString() : string.Empty;
                    entities.ProblemEncountered = reader["ProblemEncountered"] != DBNull.Value ? reader["ProblemEncountered"].ToString() : string.Empty;
                    entities.TotalValueInUsd = reader["TotalValueInUsd"] != DBNull.Value ? reader["TotalValueInUsd"].ToString() : string.Empty;
                    entities.LoaNo = reader["LoaNo"] != DBNull.Value ? reader["LoaNo"].ToString() : string.Empty;
                    entities.LoaSuretyBond = reader["LoaSuretyBond"] != DBNull.Value ? reader["LoaSuretyBond"].ToString() : string.Empty;
                    entities.Loa8106 = reader["Loa8106"] != DBNull.Value ? reader["Loa8106"].ToString() : string.Empty;
                    entities.TransactionDate = reader["TransactionDate"] != DBNull.Value ? reader["TransactionDate"].ToString() : string.Empty;
                    entities.GatePassNo = reader["GatePassNo"] != DBNull.Value ? reader["GatePassNo"].ToString() : string.Empty;
                    entities.PickUpPoint = reader["PickUpPoint"] != DBNull.Value ? reader["PickUpPoint"].ToString() : string.Empty;
                    entities.Remarks = reader["Remarks"] != DBNull.Value ? reader["Remarks"].ToString() : string.Empty;
                    entities.Category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : string.Empty;
                    entities.Supplier = reader["Supplier"] != DBNull.Value ? reader["Supplier"].ToString() : string.Empty;
                    entities.PurposeOfPullOut = reader["PurposeOfPullOut"] != DBNull.Value ? reader["PurposeOfPullOut"].ToString() : string.Empty;
                    entities.Attention = reader["Attention"] != DBNull.Value ? reader["Attention"].ToString() : string.Empty;

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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_AllRequest_Reports_New(string from, string to)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT DETAILS.CTRLNo, DETAILS.ItemName, DETAILS.ItemSpecification, DETAILS.TotalQuantity, UOM.[Description] AS UnitOfMeasure, " +
                                  "(SELECT Name FROM MT_Supplier_Head WITH (NOLOCK) WHERE RefId = (SELECT TOP 1 Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAILS.CTRLNo)) AS SupplierName, " +
                                  "(SELECT TOP 1 TransactionDate FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAILS.CTRLNo) AS RequestDate, " +
                                  "(SELECT Description FROM MT_Category WITH (NOLOCK) WHERE RefId = (SELECT TOP 1 Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAILS.CTRLNo)) AS CategoryDesc, " +
                                  "(SELECT FullName FROM Login_Credentials WITH (NOLOCK) WHERE RefId = (SELECT TOP 1 Pur_Incharge FROM SRF_TRANSACTION_Status WITH (NOLOCK) WHERE CTRLNo = DETAILS.CTRLNo)) AS BuyerName, " +
                                  "(SELECT TOP 1 FullName FROM Login_Credentials WITH (NOLOCK) WHERE RefId = (SELECT TOP 1 Req_Incharge FROM SRF_TRANSACTION_Status WITH (NOLOCK) WHERE CTRLNo = DETAILS.CTRLNo)) AS Requester, " +

                                  "(SELECT SUM(CONVERT(BIGINT, ActualQuantity)) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE DetailsRefId = DETAILS.RefId) AS DeliveredQty, " +
                                  "(SELECT TOP 1 CASE WHEN ISDATE(PullOutServiceDate) <= 0 THEN NULL ELSE CONVERT(DATE,PullOutServiceDate) END FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAILS.CTRLNo AND PullOutServiceDate IS NOT NULL) AS PullOutDate, " +
                                  "(SELECT TOP 1 CONVERT(DATE, AddedDate) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE DetailsRefId = DETAILS.RefId ORDER BY CONVERT(DATE, AddedDate) DESC) AS DeliveredDate, " +
                                  "(SELECT TOP 1 LOA8105 FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE DetailsRefId = DETAILS.RefId) AS LOA8105, " +
                                  "(SELECT TOP 1 LOA8106 FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAILS.CTRLNo) AS LOA8106, " +
                                  "(SELECT TOP 1 REPLACE(REPLACE(LOA8105ProcessDate,'.','/'),'-','/') FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE DetailsRefId = DETAILS.RefId) AS LOA8105ProcessDate,  " + 
                                  "(SELECT TOP 1 Name FROM SRF_MT_PurposeOfPullOut WITH (NOLOCK) WHERE RefId = (SELECT PurposeOfPullOut FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAILS.CTRLNo)) AS PurposeOfPullOut, " +
                                  "STAT.STATReq_Manager, STAT.STATReq_Incharge, STAT.STATPur_DeptManager, STAT.STATPur_Incharge, STAT.STATPur_Manager, " +
                                  "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = DETAILS.CTRLNo AND LOA8105 IS NULL) AS LOACount " +

                                  "FROM SRF_TRANSACTION_Request_Details DETAILS WITH (NOLOCK) " +
                                  "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON DETAILS.CTRLNo = STAT.CTRLNo " +
                                  "INNER JOIN MT_UnitOfMeasure UOM WITH (NOLOCK) ON DETAILS.UnitOfMeasure = UOM.RefId " +
                                  "WHERE DETAILS.CTRLNo IN (SELECT CTRLNo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CONVERT(DATE, TransactionDate) >= '" + from + "' AND CONVERT(DATE, TransactionDate) <= '" + to + "') " +
                                  "ORDER BY RequestDate DESC";


                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    string LOACount = reader["LOACount"] != DBNull.Value ? reader["LOACount"].ToString() : "0";
                    entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.TotalQuantity = reader["TotalQuantity"] != DBNull.Value ? reader["TotalQuantity"].ToString() : "0";
                    entities.Warehouse_TotalActualQuantity = reader["DeliveredQty"] != DBNull.Value ? reader["DeliveredQty"].ToString() : "0";
                    entities.UnitOfMeasure = reader["UnitOfMeasure"] != DBNull.Value ? reader["UnitOfMeasure"].ToString() : string.Empty;
                    entities.ItemName = reader["ItemName"] != DBNull.Value ? reader["ItemName"].ToString() : string.Empty;
                    entities.Specification = reader["ItemSpecification"] != DBNull.Value ? reader["ItemSpecification"].ToString() : string.Empty;
                    entities.SupplierName = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : string.Empty;
                    entities.TransactionDate = reader["RequestDate"] != DBNull.Value ? reader["RequestDate"].ToString() : string.Empty;
                    entities.CategoryDescription = reader["CategoryDesc"] != DBNull.Value ? reader["CategoryDesc"].ToString() : string.Empty;
                    entities.Report_BuyerName = reader["BuyerName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["BuyerName"].ToString().Replace(" ", "+"), true) : string.Empty;
                    entities.Requester = reader["Requester"] != DBNull.Value ? CryptorEngine.Decrypt(reader["Requester"].ToString().Replace(" ", "+"), true) : string.Empty;
                    
                    entities.Warehouse_RemainingQuantity = (long.Parse(entities.TotalQuantity) - long.Parse(entities.Warehouse_TotalActualQuantity)).ToString();
                    entities.PullOutServiceDate = reader["PullOutDate"] != DBNull.Value ? reader["PullOutDate"].ToString() : string.Empty;
                    entities.Warehouse_DeliveredDate = reader["DeliveredDate"] != DBNull.Value ? reader["DeliveredDate"].ToString() : string.Empty;
                    entities.Warehouse_8105 = reader["LOA8105"] != DBNull.Value ? reader["LOA8105"].ToString() : string.Empty;
                    entities.Warehouse_LOA8105ProcessDate = reader["LOA8105ProcessDate"] != DBNull.Value ? reader["LOA8105ProcessDate"].ToString() : string.Empty;
                    entities.Loa8106 = reader["LOA8106"] != DBNull.Value ? reader["LOA8106"].ToString() : string.Empty;
                    entities.PurposeOfPullOut = reader["PurposeOfPullOut"] != DBNull.Value ? reader["PurposeOfPullOut"].ToString() : string.Empty;


                    entities.ReqInchargeStat = reader["STATReq_Incharge"] != DBNull.Value ? reader["STATReq_Incharge"].ToString() : "0";
                    entities.ReqManagerStat = reader["STATReq_Manager"] != DBNull.Value ? reader["STATReq_Manager"].ToString() : "0";
                    entities.PurInchargeStat = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
                    entities.PurImpexStat = reader["STATPur_Manager"] != DBNull.Value ? reader["STATPur_Manager"].ToString() : "0";



                    //if (entities.ReqManagerStat == "0" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    //{
                    //    entities.StatAll = "FOR PROD.MNGR. APPROVAL";
                    //}
                    //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    //{
                    //    entities.StatAll = "FOR PUR.INCHARGE APPROVAL";
                    //}
                    //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "0")
                    //{
                    //    entities.StatAll = "FOR PUR.IMPEX PROCESSING";
                    //}


                    //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1")
                    //{

                    //    string stat = string.Empty;

                    //    List<Entities_SRF_RequestEntry> inProgress = new List<Entities_SRF_RequestEntry>();
                    //    inProgress = SRF_TRANSACTION_GetInProgressByCTRLNo(reader["CtrlNo"].ToString().ToUpper());

                    //    if (inProgress.Count > 0)
                    //    {
                    //        entities.StatAll = "DELIVERY IN-PROGRESS";
                    //    }
                    //    else
                    //    {
                    //        entities.StatAll = "APPROVED";
                    //    }



                    //}


                    //if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurImpexStat == "2")
                    //{
                    //    entities.StatAll = "DISAPPROVED";
                    //}
                    //if (entities.PurImpexStat == "3")
                    //{
                    //    entities.StatAll = "CANCELED";
                    //}


                    if (!string.IsNullOrEmpty(entities.Warehouse_TotalActualQuantity) && !string.IsNullOrEmpty(entities.TotalQuantity))
                    {
                        if (entities.Warehouse_TotalActualQuantity == entities.TotalQuantity)
                        {
                            if (int.Parse(LOACount) <= 0)
                            {
                                entities.StatAll = "CLOSED";
                            }
                            else
                            {
                                entities.StatAll = "DELIVERED";
                            }
                        }
                        else
                        {
                            if (entities.ReqManagerStat == "0" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                            {
                                entities.StatAll = "FOR PROD.MNGR. APPROVAL";
                            }
                            if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                            {
                                entities.StatAll = "FOR PUR.INCHARGE APPROVAL";
                            }
                            if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "0")
                            {
                                entities.StatAll = "FOR PUR.IMPEX PROCESSING";
                            }

                            if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1")
                            {

                                string stat = string.Empty;

                                List<Entities_SRF_RequestEntry> inProgress = new List<Entities_SRF_RequestEntry>();
                                inProgress = SRF_TRANSACTION_GetInProgressByCTRLNo(reader["CtrlNo"].ToString().ToUpper());

                                if (inProgress.Count > 0)
                                {
                                    entities.StatAll = "DELIVERY IN-PROGRESS";
                                }
                                else
                                {
                                    entities.StatAll = "APPROVED";
                                }



                            }

                            if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurImpexStat == "2")
                            {
                                entities.StatAll = "DISAPPROVED";
                            }
                            if (entities.PurImpexStat == "3")
                            {
                                entities.StatAll = "CANCELED";
                            }

                            if (entities.Warehouse_TotalActualQuantity != "0" && (long.Parse(entities.TotalQuantity) > long.Parse(entities.Warehouse_TotalActualQuantity)))
                            {
                                entities.StatAll = "DELIVERED WITH PENDING ITEMS";
                            }

                        }
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_AllRequest_Reports_New2(string ctrlno)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT DETAILS.CTRLNo, DETAILS.ItemName, DETAILS.ItemSpecification, DETAILS.TotalQuantity, UOM.[Description] AS UnitOfMeasure, " +
                                  "(SELECT Name FROM MT_Supplier_Head WITH (NOLOCK) WHERE RefId = (SELECT TOP 1 Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAILS.CTRLNo)) AS SupplierName, " +
                                  "(SELECT TOP 1 TransactionDate FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAILS.CTRLNo) AS RequestDate, " +
                                  "(SELECT Description FROM MT_Category WITH (NOLOCK) WHERE RefId = (SELECT TOP 1 Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAILS.CTRLNo)) AS CategoryDesc, " +
                                  "(SELECT FullName FROM Login_Credentials WITH (NOLOCK) WHERE RefId = (SELECT TOP 1 Pur_Incharge FROM SRF_TRANSACTION_Status WITH (NOLOCK) WHERE CTRLNo = DETAILS.CTRLNo)) AS BuyerName, " +
                                  "(SELECT TOP 1 FullName FROM Login_Credentials WITH (NOLOCK) WHERE RefId = (SELECT TOP 1 Req_Incharge FROM SRF_TRANSACTION_Status WITH (NOLOCK) WHERE CTRLNo = DETAILS.CTRLNo)) AS Requester, " +

                                  "(SELECT SUM(CONVERT(BIGINT, ActualQuantity)) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE DetailsRefId = DETAILS.RefId) AS DeliveredQty, " +
                                  "(SELECT TOP 1 CASE WHEN ISDATE(PullOutServiceDate) <= 0 THEN NULL ELSE CONVERT(DATE,PullOutServiceDate) END FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAILS.CTRLNo AND PullOutServiceDate IS NOT NULL) AS PullOutDate, " +
                                  "(SELECT TOP 1 CONVERT(DATE, AddedDate) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE DetailsRefId = DETAILS.RefId ORDER BY CONVERT(DATE, AddedDate) DESC) AS DeliveredDate, " +
                                  "(SELECT TOP 1 LOA8105 FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE DetailsRefId = DETAILS.RefId) AS LOA8105, " +
                                  "(SELECT TOP 1 LOA8106 FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAILS.CTRLNo) AS LOA8106, " +
                                  "(SELECT TOP 1 REPLACE(REPLACE(LOA8105ProcessDate,'.','/'),'-','/') FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE DetailsRefId = DETAILS.RefId) AS LOA8105ProcessDate,  " +
                                  "(SELECT TOP 1 Name FROM SRF_MT_PurposeOfPullOut WITH (NOLOCK) WHERE RefId = (SELECT PurposeOfPullOut FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAILS.CTRLNo)) AS PurposeOfPullOut, " +
                                  "STAT.STATReq_Manager, STAT.STATReq_Incharge, STAT.STATPur_DeptManager, STAT.STATPur_Incharge, STAT.STATPur_Manager, " +
                                  "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = DETAILS.CTRLNo AND LOA8105 IS NULL) AS LOACount " +

                                  "FROM SRF_TRANSACTION_Request_Details DETAILS WITH (NOLOCK) " +
                                  "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON DETAILS.CTRLNo = STAT.CTRLNo " +
                                  "INNER JOIN MT_UnitOfMeasure UOM WITH (NOLOCK) ON DETAILS.UnitOfMeasure = UOM.RefId " +
                                  "WHERE DETAILS.CTRLNo IN (" + ctrlno + ") " +
                                  "ORDER BY RequestDate DESC";


                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    string LOACount = reader["LOACount"] != DBNull.Value ? reader["LOACount"].ToString() : "0";
                    entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.TotalQuantity = reader["TotalQuantity"] != DBNull.Value ? reader["TotalQuantity"].ToString() : "0";
                    entities.Warehouse_TotalActualQuantity = reader["DeliveredQty"] != DBNull.Value ? reader["DeliveredQty"].ToString() : "0";
                    entities.UnitOfMeasure = reader["UnitOfMeasure"] != DBNull.Value ? reader["UnitOfMeasure"].ToString() : string.Empty;
                    entities.ItemName = reader["ItemName"] != DBNull.Value ? reader["ItemName"].ToString() : string.Empty;
                    entities.Specification = reader["ItemSpecification"] != DBNull.Value ? reader["ItemSpecification"].ToString() : string.Empty;
                    entities.SupplierName = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : string.Empty;
                    entities.TransactionDate = reader["RequestDate"] != DBNull.Value ? reader["RequestDate"].ToString() : string.Empty;
                    entities.CategoryDescription = reader["CategoryDesc"] != DBNull.Value ? reader["CategoryDesc"].ToString() : string.Empty;
                    entities.Report_BuyerName = reader["BuyerName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["BuyerName"].ToString().Replace(" ", "+"), true) : string.Empty;
                    entities.Requester = reader["Requester"] != DBNull.Value ? CryptorEngine.Decrypt(reader["Requester"].ToString().Replace(" ", "+"), true) : string.Empty;

                    entities.Warehouse_RemainingQuantity = (long.Parse(entities.TotalQuantity) - long.Parse(entities.Warehouse_TotalActualQuantity)).ToString();
                    entities.PullOutServiceDate = reader["PullOutDate"] != DBNull.Value ? reader["PullOutDate"].ToString() : string.Empty;
                    entities.Warehouse_DeliveredDate = reader["DeliveredDate"] != DBNull.Value ? reader["DeliveredDate"].ToString() : string.Empty;
                    entities.Warehouse_8105 = reader["LOA8105"] != DBNull.Value ? reader["LOA8105"].ToString() : string.Empty;
                    entities.Warehouse_LOA8105ProcessDate = reader["LOA8105ProcessDate"] != DBNull.Value ? reader["LOA8105ProcessDate"].ToString() : string.Empty;
                    entities.Loa8106 = reader["LOA8106"] != DBNull.Value ? reader["LOA8106"].ToString() : string.Empty;
                    entities.PurposeOfPullOut = reader["PurposeOfPullOut"] != DBNull.Value ? reader["PurposeOfPullOut"].ToString() : string.Empty;


                    entities.ReqInchargeStat = reader["STATReq_Incharge"] != DBNull.Value ? reader["STATReq_Incharge"].ToString() : "0";
                    entities.ReqManagerStat = reader["STATReq_Manager"] != DBNull.Value ? reader["STATReq_Manager"].ToString() : "0";
                    entities.PurInchargeStat = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
                    entities.PurImpexStat = reader["STATPur_Manager"] != DBNull.Value ? reader["STATPur_Manager"].ToString() : "0";



                    //if (entities.ReqManagerStat == "0" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    //{
                    //    entities.StatAll = "FOR PROD.MNGR. APPROVAL";
                    //}
                    //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    //{
                    //    entities.StatAll = "FOR PUR.INCHARGE APPROVAL";
                    //}
                    //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "0")
                    //{
                    //    entities.StatAll = "FOR PUR.IMPEX PROCESSING";
                    //}


                    //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1")
                    //{

                    //    string stat = string.Empty;

                    //    List<Entities_SRF_RequestEntry> inProgress = new List<Entities_SRF_RequestEntry>();
                    //    inProgress = SRF_TRANSACTION_GetInProgressByCTRLNo(reader["CtrlNo"].ToString().ToUpper());

                    //    if (inProgress.Count > 0)
                    //    {
                    //        entities.StatAll = "DELIVERY IN-PROGRESS";
                    //    }
                    //    else
                    //    {
                    //        entities.StatAll = "APPROVED";
                    //    }



                    //}


                    //if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurImpexStat == "2")
                    //{
                    //    entities.StatAll = "DISAPPROVED";
                    //}
                    //if (entities.PurImpexStat == "3")
                    //{
                    //    entities.StatAll = "CANCELED";
                    //}


                    if (!string.IsNullOrEmpty(entities.Warehouse_TotalActualQuantity) && !string.IsNullOrEmpty(entities.TotalQuantity))
                    {
                        if (entities.Warehouse_TotalActualQuantity == entities.TotalQuantity)
                        {
                            if (int.Parse(LOACount) <= 0)
                            {
                                entities.StatAll = "CLOSED";
                            }
                            else
                            {
                                entities.StatAll = "DELIVERED";
                            }
                        }
                        else
                        {
                            if (entities.ReqManagerStat == "0" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                            {
                                entities.StatAll = "FOR PROD.MNGR. APPROVAL";
                            }
                            if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                            {
                                entities.StatAll = "FOR PUR.INCHARGE APPROVAL";
                            }
                            if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "0")
                            {
                                entities.StatAll = "FOR PUR.IMPEX PROCESSING";
                            }

                            if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1")
                            {

                                string stat = string.Empty;

                                List<Entities_SRF_RequestEntry> inProgress = new List<Entities_SRF_RequestEntry>();
                                inProgress = SRF_TRANSACTION_GetInProgressByCTRLNo(reader["CtrlNo"].ToString().ToUpper());

                                if (inProgress.Count > 0)
                                {
                                    entities.StatAll = "DELIVERY IN-PROGRESS";
                                }
                                else
                                {
                                    entities.StatAll = "APPROVED";
                                }



                            }

                            if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurImpexStat == "2")
                            {
                                entities.StatAll = "DISAPPROVED";
                            }
                            if (entities.PurImpexStat == "3")
                            {
                                entities.StatAll = "CANCELED";
                            }

                            if (entities.Warehouse_TotalActualQuantity != "0" && (long.Parse(entities.TotalQuantity) > long.Parse(entities.Warehouse_TotalActualQuantity)))
                            {
                                entities.StatAll = "DELIVERED WITH PENDING ITEMS";
                            }

                        }
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestStatus_ByDateRange_All(Entities_SRF_RequestEntry entity)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_TRANSACTION_RequestStatus_ByDateRange_All";
                cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
                cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
                cmd.Parameters.Add(Factory.CreateParameter("@category", entity.Category = entity.Category.Length <= 0 ? string.Empty : entity.Category));
                cmd.Parameters.Add(Factory.CreateParameter("@selectall", entity.SelectAll = entity.SelectAll.Length <= 0 ? string.Empty : entity.SelectAll));
                cmd.Parameters.Add(Factory.CreateParameter("@isImpex", entity.IsImpex = entity.IsImpex.Length <= 0 ? string.Empty : entity.IsImpex));

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.RefId = reader["RefId"].ToString();
                    entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.ReqIncharge = reader["Req_Incharge"].ToString();
                    entities.ReqManager = reader["Req_Manager"].ToString();
                    entities.PurIncharge = reader["Pur_Incharge"].ToString();
                    entities.PurImpex = reader["Pur_Manager"].ToString();
                    entities.ReqInchargeDOA = reader["DOAReq_Incharge"].ToString();
                    entities.ReqManagerDOA = reader["DOAReq_Manager"].ToString();
                    entities.PurInchargeDOA = reader["DOAPur_Incharge"].ToString();
                    entities.PurImpexDOA = reader["DOAPur_Manager"].ToString();
                    entities.ReqInchargeStat = reader["STATReq_Incharge"].ToString();
                    entities.ReqManagerStat = reader["STATReq_Manager"].ToString();
                    entities.PurInchargeStat = reader["STATPur_Incharge"].ToString();
                    entities.PurImpexStat = reader["STATPur_Manager"].ToString();
                    entities.PurDeptManager = reader["Pur_DeptManager"].ToString();
                    entities.PurDeptManagerDOA = reader["DOAPur_DeptManager"].ToString();
                    entities.PurDeptManagerStat = reader["STATPur_DeptManager"].ToString();
                    entities.StatRemarks = reader["StatRemarks"].ToString();
                    entities.Category = reader["Category"].ToString();
                    entities.Supplier = reader["Name"].ToString();
                    entities.Requester = CryptorEngine.Decrypt(reader["FullName"].ToString(), true).ToUpper();

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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like_All(Entities_SRF_RequestEntry entity)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_TRANSACTION_RequestStatus_ByDateRange_And_ControlNo_Like_All";
                cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
                cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
                cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
                cmd.Parameters.Add(Factory.CreateParameter("@category", entity.Category = entity.Category.Length <= 0 ? string.Empty : entity.Category));
                cmd.Parameters.Add(Factory.CreateParameter("@selectall", entity.SelectAll = entity.SelectAll.Length <= 0 ? string.Empty : entity.SelectAll));
                cmd.Parameters.Add(Factory.CreateParameter("@isImpex", entity.IsImpex = entity.IsImpex.Length <= 0 ? string.Empty : entity.IsImpex));

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.RefId = reader["RefId"].ToString();
                    entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.ReqIncharge = reader["Req_Incharge"].ToString();
                    entities.ReqManager = reader["Req_Manager"].ToString();
                    entities.PurIncharge = reader["Pur_Incharge"].ToString();
                    entities.PurImpex = reader["Pur_Manager"].ToString();
                    entities.ReqInchargeDOA = reader["DOAReq_Incharge"].ToString();
                    entities.ReqManagerDOA = reader["DOAReq_Manager"].ToString();
                    entities.PurInchargeDOA = reader["DOAPur_Incharge"].ToString();
                    entities.PurImpexDOA = reader["DOAPur_Manager"].ToString();
                    entities.ReqInchargeStat = reader["STATReq_Incharge"].ToString();
                    entities.ReqManagerStat = reader["STATReq_Manager"].ToString();
                    entities.PurInchargeStat = reader["STATPur_Incharge"].ToString();
                    entities.PurImpexStat = reader["STATPur_Manager"].ToString();
                    entities.StatRemarks = reader["StatRemarks"].ToString();
                    entities.Category = reader["Category"].ToString();
                    entities.Supplier = reader["Name"].ToString();
                    entities.Requester = CryptorEngine.Decrypt(reader["FullName"].ToString(), true).ToUpper();

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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_ForApproval(Entities_SRF_RequestEntry entity)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_TRANSACTION_ForApproval";
                //cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
                //cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.RefId = reader["RefId"].ToString();
                    entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.ReqIncharge = reader["Req_Incharge"] != DBNull.Value ? reader["Req_Incharge"].ToString() : string.Empty;
                    entities.ReqManager = reader["Req_Manager"] != DBNull.Value ? reader["Req_Manager"].ToString() : string.Empty;
                    entities.PurIncharge = reader["Pur_Incharge"] != DBNull.Value ? reader["Pur_Incharge"].ToString() : string.Empty;
                    entities.PurImpex = reader["Pur_Manager"] != DBNull.Value ? reader["Pur_Manager"].ToString() : string.Empty;
                    entities.ReqInchargeDOA = reader["DOAReq_Incharge"] != DBNull.Value ? reader["DOAReq_Incharge"].ToString() : string.Empty;
                    entities.ReqManagerDOA = reader["DOAReq_Manager"] != DBNull.Value ? reader["DOAReq_Manager"].ToString() : string.Empty;
                    entities.PurInchargeDOA = reader["DOAPur_Incharge"] != DBNull.Value ? reader["DOAPur_Incharge"].ToString() : string.Empty;
                    entities.PurImpexDOA = reader["DOAPur_Manager"] != DBNull.Value ? reader["DOAPur_Manager"].ToString() : string.Empty;
                    entities.ReqInchargeStat = reader["STATReq_Incharge"] != DBNull.Value ? reader["STATReq_Incharge"].ToString() : "0";
                    entities.ReqManagerStat = reader["STATReq_Manager"] != DBNull.Value ? reader["STATReq_Manager"].ToString() : "0";
                    entities.PurInchargeStat = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
                    entities.PurImpexStat = reader["STATPur_Manager"] != DBNull.Value ? reader["STATPur_Manager"].ToString() : "0";
                    entities.PurDeptManager = reader["Pur_DeptManager"].ToString();
                    entities.PurDeptManagerDOA = reader["DOAPur_DeptManager"] != DBNull.Value ? reader["DOAPur_DeptManager"].ToString() : string.Empty;
                    entities.PurDeptManagerStat = reader["STATPur_DeptManager"] != DBNull.Value ? reader["STATPur_DeptManager"].ToString() : "0";
                    entities.Remarks = reader["Remarks"] != DBNull.Value ? reader["Remarks"].ToString() : string.Empty;
                    entities.Department = reader["Department"].ToString();
                    entities.Division = reader["Division"].ToString();
                    entities.Section = reader["Section"].ToString();
                    entities.Category = reader["Category"].ToString();
                    entities.CategoryDescription = reader["CategoryName"].ToString();
                    entities.Supplier = reader["Name"].ToString();
                    entities.SupplierEmail = reader["EmailAddress"].ToString();
                    entities.Requester = CryptorEngine.Decrypt(reader["FullName"].ToString(), true).ToUpper();

                    entities.GatePassNo = reader["GatePassNo"] != DBNull.Value ? reader["GatePassNo"].ToString() : string.Empty;
                    entities.Warehouse_PezaNonPeza = reader["PezaNonPeza"] != DBNull.Value ? reader["PezaNonPeza"].ToString() : string.Empty;

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

        public string SRF_TRANSACTION_IsApprovedByProdManager(string ctrlno)
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
                cmd.CommandText = "SELECT * FROM SRF_TRANSACTION_Status WHERE (STATReq_Manager = 0 OR STATReq_Manager IS NULL) AND CTRLNo = '" + ctrlno + "'";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    retVal = reader["CtrlNo"].ToString().ToUpper();
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

        public string SRF_TRANSACTION_IsApprovedByPURIncharge(string ctrlno)
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
                cmd.CommandText = "SELECT REQUEST.TotalQuantity FROM SRF_TRANSACTION_Status STAT " +
                                    "INNER JOIN SRF_TRANSACTION_Request REQUEST ON STAT.CTRLNo = REQUEST.CTRLNo " +
                                    "WHERE (STAT.STATPur_Incharge = 0 OR STAT.STATPur_Incharge IS NULL) " +
                                    "AND STAT.CTRLNo = '" + ctrlno + "'";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    retVal = reader["TotalQuantity"].ToString().ToUpper();
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

        public int SRF_TRANSACTION_ApprovedRequestorIncharge(Entities_SRF_RequestEntry entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_TRANSACTION_ApprovedRequestorIncharge";

            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
            cmd.Parameters.Add(Factory.CreateParameter("@approver", entity.ReqIncharge = entity.ReqIncharge.Length <= 0 ? string.Empty : entity.ReqIncharge));
            cmd.Parameters.Add(Factory.CreateParameter("@status", entity.ReqInchargeStat = entity.ReqInchargeStat.Length <= 0 ? string.Empty : entity.ReqInchargeStat));
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

        public int SRF_TRANSACTION_ApprovedRequestorManager(Entities_SRF_RequestEntry entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_TRANSACTION_ApprovedRequestorManager";

            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
            cmd.Parameters.Add(Factory.CreateParameter("@approver", entity.ReqManager = entity.ReqManager.Length <= 0 ? string.Empty : entity.ReqManager));
            cmd.Parameters.Add(Factory.CreateParameter("@status", entity.ReqManagerStat = entity.ReqManagerStat.Length <= 0 ? string.Empty : entity.ReqManagerStat));
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

        public int SRF_TRANSACTION_ApprovedPurchasingIncharge(Entities_SRF_RequestEntry entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_TRANSACTION_ApprovedPurchasingIncharge";

            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
            cmd.Parameters.Add(Factory.CreateParameter("@approver", entity.PurIncharge = entity.PurIncharge.Length <= 0 ? string.Empty : entity.PurIncharge));
            cmd.Parameters.Add(Factory.CreateParameter("@status", entity.PurInchargeStat = entity.PurInchargeStat.Length <= 0 ? string.Empty : entity.PurInchargeStat));
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

        public int SRF_TRANSACTION_ApprovedPurchasingManager(Entities_SRF_RequestEntry entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_TRANSACTION_ApprovedPurchasingManager";

            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
            cmd.Parameters.Add(Factory.CreateParameter("@approver", entity.PurImpex = entity.PurImpex.Length <= 0 ? string.Empty : entity.PurImpex));
            cmd.Parameters.Add(Factory.CreateParameter("@status", entity.PurImpexStat = entity.PurImpexStat.Length <= 0 ? string.Empty : entity.PurImpexStat));
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

        public int SRF_TRANSACTION_ApprovedPurchasingDeptManager(Entities_SRF_RequestEntry entity)
        {
            int result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SRF_TRANSACTION_ApprovedPurchasingDeptManager";

            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));
            cmd.Parameters.Add(Factory.CreateParameter("@approver", entity.PurDeptManager = entity.PurDeptManager.Length <= 0 ? string.Empty : entity.PurDeptManager));
            cmd.Parameters.Add(Factory.CreateParameter("@status", entity.PurDeptManagerStat = entity.PurDeptManagerStat.Length <= 0 ? string.Empty : entity.PurDeptManagerStat));
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



        public List<Entities_SRF_LOA> SRF_TRANSACTION_LOA_DISTRIBUTION(string from, string to)
        {
            List<Entities_SRF_LOA> list = new List<Entities_SRF_LOA>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_TRANSACTION_LOA_DISTRIBUTION";

                cmd.Parameters.Add(Factory.CreateParameter("@from", from));
                cmd.Parameters.Add(Factory.CreateParameter("@to", to));

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_LOA entities = new Entities_SRF_LOA();

                    entities.LoaNo = reader["LOANo"].ToString();
                    entities.LoaPriceValue = reader["LoaPriceValue"].ToString();
                    entities.ItemName = reader["ItemName"].ToString();
                    entities.TotalQuantity = reader["TotalQuantity"].ToString();
                    entities.PriceValue = reader["PriceValue"].ToString();
                    entities.TransactionDate = reader["TransactionDate"].ToString();

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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_AllRequest_FreeForm(Entities_SRF_RequestEntry entity)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_TRANSACTION_AllRequest_FreeForm";
                cmd.Parameters.Add(Factory.CreateParameter("@search", entity.SearchItem = entity.SearchItem.Length <= 0 ? string.Empty : entity.SearchItem));

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    string WarehouseTotalActualQty = reader["WarehouseTotalActualQty"] != DBNull.Value ? reader["WarehouseTotalActualQty"].ToString() : string.Empty;
                    string WarehouseTotalQty = reader["WarehouseTotalQty"] != DBNull.Value ? reader["WarehouseTotalQty"].ToString() : string.Empty;
                    string LOACount = reader["LOACount"] != DBNull.Value ? reader["LOACount"].ToString() : string.Empty;


                    entities.RefId = reader["RefId"].ToString();
                    entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.ReqIncharge = reader["Req_Incharge"] != DBNull.Value ? reader["Req_Incharge"].ToString() : string.Empty;
                    entities.ReqManager = reader["Req_Manager"] != DBNull.Value ? reader["Req_Manager"].ToString() : string.Empty;
                    entities.PurIncharge = reader["Pur_Incharge"] != DBNull.Value ? reader["Pur_Incharge"].ToString() : string.Empty;
                    entities.PurImpex = reader["Pur_Manager"] != DBNull.Value ? reader["Pur_Manager"].ToString() : string.Empty;
                    entities.ReqInchargeDOA = reader["DOAReq_Incharge"] != DBNull.Value ? reader["DOAReq_Incharge"].ToString() : string.Empty;
                    entities.ReqManagerDOA = reader["DOAReq_Manager"] != DBNull.Value ? reader["DOAReq_Manager"].ToString() : string.Empty;
                    entities.PurInchargeDOA = reader["DOAPur_Incharge"] != DBNull.Value ? reader["DOAPur_Incharge"].ToString() : string.Empty;
                    entities.PurImpexDOA = reader["DOAPur_Manager"] != DBNull.Value ? reader["DOAPur_Manager"].ToString() : string.Empty;
                    entities.ReqInchargeStat = reader["STATReq_Incharge"] != DBNull.Value ? reader["STATReq_Incharge"].ToString() : "0";
                    entities.ReqManagerStat = reader["STATReq_Manager"] != DBNull.Value ? reader["STATReq_Manager"].ToString() : "0";
                    entities.PurInchargeStat = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
                    entities.PurImpexStat = reader["STATPur_Manager"] != DBNull.Value ? reader["STATPur_Manager"].ToString() : "0";
                    entities.StatRemarks = reader["Remarks"] != DBNull.Value ? reader["Remarks"].ToString() : string.Empty;
                    entities.RefPRPO = reader["RefPRPO"] != DBNull.Value ? reader["RefPRPO"].ToString() : string.Empty;
                    entities.SalesInvoice = reader["SalesInvoice"] != DBNull.Value ? reader["SalesInvoice"].ToString() : string.Empty;
                    entities.BrandMachineName = reader["BrandMachineName"] != DBNull.Value ? reader["BrandMachineName"].ToString() : string.Empty;
                    entities.ItemName = reader["ItemName"] != DBNull.Value ? reader["ItemName"].ToString() : string.Empty;
                    entities.Specification = reader["ItemSpecification"] != DBNull.Value ? reader["ItemSpecification"].ToString() : string.Empty;

                    if (entities.ReqManagerStat == "0" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PROD.MNGR. APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.INCHARGE APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "0")
                    {
                        //entities.StatAll = "FOR PUR.IMPEX APPROVAL";
                        entities.StatAll = "FOR PUR.IMPEX PROCESSING";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1")
                    {
                        
                        string stat = string.Empty;

                        List<Entities_SRF_RequestEntry> inProgress = new List<Entities_SRF_RequestEntry>();
                        inProgress = SRF_TRANSACTION_GetInProgressByCTRLNo(reader["CtrlNo"].ToString().ToUpper());

                        if (inProgress.Count > 0)
                        {
                            stat += "<table><tr>DELIVERY IN-PROGRESS</tr>";

                            foreach (Entities_SRF_RequestEntry eProgress in inProgress)
                            {
                                stat += "<tr><td>" + eProgress.Warehouse_ItemName + "</td></tr>";
                            }

                            stat += "</table>";
                            entities.StatAll = stat;
                        }
                        else
                        {
                            entities.StatAll = "APPROVED";
                        }

                        

                    }
                    if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurImpexStat == "2")
                    {
                        entities.StatAll = "DISAPPROVED";
                    }
                    if (entities.PurImpexStat == "3")
                    {
                        entities.StatAll = "CANCELED";
                    }

                    if (!string.IsNullOrEmpty(WarehouseTotalActualQty) && !string.IsNullOrEmpty(WarehouseTotalQty))
                    {
                        if (WarehouseTotalActualQty == WarehouseTotalQty)
                        {
                            if (int.Parse(LOACount) <= 0)
                            {
                                entities.StatAll = "CLOSED";
                            }
                        }
                        else
                        {
                            entities.StatAll = "DELIVERED WITH PENDING ITEMS";
                        }
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

        //public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_ReceivingEntry(string search)
        //{
        //    List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

        //    DbConnection conn = Factory.CreateConnection();
        //    DbCommand cmd = Factory.CreateCommand();
        //    DbDataReader reader = null;
        //    string query = string.Empty;

        //    try
        //    {
        //        conn.Open();
        //        cmd.Connection = conn;
        //        cmd.CommandType = CommandType.Text;

        //        // SET THE DATE WHEN THE IMPLEMENTATION START (CONVERT(DATE, STAT.DOAPur_Manager) >= '10/8/2022')
        //        if (string.IsNullOrEmpty(search))
        //        {
        //            query = "SELECT TOP 500 HEAD.RefId, HEAD.CTRLNo, HEAD.LOA8106, HEAD.Category, CAT.Description AS CategoryName, " +
        //                    "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = HEAD.Requester) AS RequesterName, " +
        //                    "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
        //                    "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS PezaNonPeza, " +
        //                    "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS Warehouse1, " +
        //                    "(SELECT SUBSTRING((SELECT DISTINCT(LOA8105) + ',' AS 'data()' FROM SRF_TRANSACTION_Warehouse_Actual_Delivery where SRFNumber = HEAD.CTRLNo FOR XML PATH('')), 1 , 9999)) AS LOA8105, " +
        //                    "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(TotalQuantity) = 1 THEN CAST(TotalQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Request_Details WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS WarehouseTotalQty, " +
        //                    "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(ActualQuantity) = 1 THEN CAST(ActualQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS WarehouseTotalActualQty, " +
        //                    "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS LOACount,  " +
        //                    "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND (WithDocuments = '0' OR WithDocuments IS NULL)) AS LOACount2,  " +
        //                    "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND LOA8105 IS NOT NULL) AS ActualDeliveryCount, " +
        //                    "STAT.* " +
        //                    "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
        //                    "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
        //                    "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
        //                    "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '11/14/2022' ";
        //                    //"AND HEAD.Category NOT IN ('" + ConfigurationManager.AppSettings["SRF_PO_FIXED_CATEGORY"].ToString() + "') AND LEN(HEAD.LOA8106) > 0";
        //        }
        //        else
        //        {
        //            query = "SELECT TOP 500 HEAD.RefId, HEAD.CTRLNo, HEAD.LOA8106, HEAD.Category, CAT.Description AS CategoryName, " +
        //                    "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = HEAD.Requester) AS RequesterName, " +
        //                    "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
        //                    "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS PezaNonPeza, " +
        //                    "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS Warehouse1, " +
        //                    "(SELECT SUBSTRING((SELECT DISTINCT(LOA8105) + ',' AS 'data()' FROM SRF_TRANSACTION_Warehouse_Actual_Delivery where SRFNumber = HEAD.CTRLNo FOR XML PATH('')), 1 , 9999)) AS LOA8105, " +
        //                    "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(TotalQuantity) = 1 THEN CAST(TotalQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Request_Details WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS WarehouseTotalQty, " +
        //                    "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(ActualQuantity) = 1 THEN CAST(ActualQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS WarehouseTotalActualQty, " +
        //                    "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS LOACount,  " +
        //                    "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND (WithDocuments = '0' OR WithDocuments IS NULL)) AS LOACount2,  " +
        //                    "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND LOA8105 IS NOT NULL) AS ActualDeliveryCount, " +
        //                    "STAT.* " +
        //                    "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
        //                    "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
        //                    "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
        //                    "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '11/14/2022' " +
        //                    "AND (HEAD.CTRLNo LIKE '%" + search + "%' OR HEAD.LOA8106 LIKE '%" + search + "%') ";
        //                    //"AND HEAD.Category NOT IN ('" + ConfigurationManager.AppSettings["SRF_PO_FIXED_CATEGORY"].ToString() + "') AND LEN(HEAD.LOA8106) > 0";
        //        }

        //        cmd.CommandText = query;

        //        reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

        //            string Warehouse1 = reader["Warehouse1"] != DBNull.Value ? reader["Warehouse1"].ToString() : string.Empty;
        //            string WarehouseTotalQty = reader["WarehouseTotalQty"] != DBNull.Value ? reader["WarehouseTotalQty"].ToString() : string.Empty;
        //            string WarehouseTotalActualQty = reader["WarehouseTotalActualQty"] != DBNull.Value ? reader["WarehouseTotalActualQty"].ToString() : string.Empty;
        //            string LOACount = reader["LOACount"] != DBNull.Value ? reader["LOACount"].ToString() : string.Empty;
        //            string ActualDeliveryCount = reader["ActualDeliveryCount"] != DBNull.Value ? reader["ActualDeliveryCount"].ToString() : string.Empty;

        //            entities.RefId = reader["RefId"].ToString();
        //            entities.Category = reader["Category"].ToString();
        //            entities.CategoryDescription = reader["CategoryName"].ToString();
        //            entities.Warehouse_CtrlNo = reader["CtrlNo"].ToString().ToUpper();
        //            entities.Warehouse_LOA8106 = reader["LOA8106"] != DBNull.Value ? reader["LOA8106"].ToString() : string.Empty;
        //            entities.Warehouse_8105 = reader["LOA8106"] != DBNull.Value ? reader["LOA8105"].ToString() : string.Empty;
        //            entities.Warehouse_PezaNonPeza = reader["PezaNonPeza"] != DBNull.Value ? reader["PezaNonPeza"].ToString() : string.Empty;
        //            entities.Warehouse_RequesterName = CryptorEngine.Decrypt(reader["RequesterName"].ToString(), true).ToUpper();
        //            entities.Warehouse_SupplierName = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : string.Empty;
        //            entities.Warehouse_LoaCount2 = reader["LOACount2"] != DBNull.Value ? reader["LOACount2"].ToString() : "0";

        //            entities.ReqInchargeStat = reader["STATReq_Incharge"] != DBNull.Value ? reader["STATReq_Incharge"].ToString() : "0";
        //            entities.ReqManagerStat = reader["STATReq_Manager"] != DBNull.Value ? reader["STATReq_Manager"].ToString() : "0";
        //            entities.PurInchargeStat = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
        //            entities.PurImpexStat = reader["STATPur_Manager"] != DBNull.Value ? reader["STATPur_Manager"].ToString() : "0";

        //            if (entities.ReqManagerStat == "0" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
        //            {
        //                entities.StatAll = "FOR PROD.MNGR. APPROVAL";
        //            }
        //            if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
        //            {
        //                entities.StatAll = "FOR PUR.INCHARGE APPROVAL";
        //            }
        //            if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "0")
        //            {
        //                entities.StatAll = "FOR PUR.IMPEX PROCESSING";
        //            }
        //            if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1")
        //            {
        //                entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
        //            }
        //            if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && !string.IsNullOrEmpty(Warehouse1))
        //            {
        //                //entities.StatAll = "DELIVERY IN-PROGRESS";

        //                string stat = string.Empty;

        //                stat += "<table><tr>DELIVERY IN-PROGRESS</tr>";

        //                List<Entities_SRF_RequestEntry> inProgress = new List<Entities_SRF_RequestEntry>();
        //                inProgress = SRF_TRANSACTION_GetInProgressByCTRLNo(reader["CtrlNo"].ToString().ToUpper());

        //                if (inProgress != null)
        //                {
        //                    if (inProgress.Count > 0)
        //                    {
        //                        foreach (Entities_SRF_RequestEntry eProgress in inProgress)
        //                        {
        //                            stat += "<tr><td>[" + eProgress.RefId + "] " + eProgress.Warehouse_ItemName + "</td></tr>";
        //                        }
        //                    }
        //                }

        //                stat += "</table>";

        //                entities.StatAll = stat;
        //            }
        //            if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && (!string.IsNullOrEmpty(WarehouseTotalQty) && !string.IsNullOrEmpty(WarehouseTotalActualQty)))
        //            {
        //                if (WarehouseTotalQty == WarehouseTotalActualQty)
        //                {
        //                    entities.StatAll = "DELIVERED";
        //                }
        //                else
        //                {
        //                    string stat2 = string.Empty;
                            

        //                    List<Entities_SRF_RequestEntry> inProgress2 = new List<Entities_SRF_RequestEntry>();
        //                    inProgress2 = SRF_TRANSACTION_GetInProgressByCTRLNo(reader["CtrlNo"].ToString().ToUpper());

        //                    if (inProgress2.Count > 0)
        //                    {
        //                        stat2 += "<table><tr>DELIVERY IN-PROGRESS</tr>";

        //                        foreach (Entities_SRF_RequestEntry eProgress2 in inProgress2)
        //                        {
        //                            stat2 += "<tr><td>[" + eProgress2.RefId + "] " + eProgress2.Warehouse_ItemName + "</td></tr>";
        //                        }

        //                        stat2 += "</table>";
        //                        entities.StatAll = stat2;
        //                    }
        //                    else
        //                    {
        //                        entities.StatAll = "DELIVERED WITH PENDING ITEMS";
        //                    }

        //                }
        //            }
        //            if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurImpexStat == "2")
        //            {
        //                entities.StatAll = "DISAPPROVED";
        //            }
        //            if (entities.PurImpexStat == "3")
        //            {
        //                entities.StatAll = "CANCELED";
        //            }

        //            //if (!string.IsNullOrEmpty(WarehouseTotalQty))
        //            //{

        //            //    if (int.Parse(LOACount) <= 0)
        //            //    {
        //            //        //DO NOTHING
        //            //    }
        //            //    else
        //            //    {
        //            //        list.Add(entities);
        //            //    }

        //            //}

        //            //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && (!string.IsNullOrEmpty(WarehouseTotalQty) && !string.IsNullOrEmpty(WarehouseTotalActualQty)))
        //            //{
        //            //    if (int.Parse(LOACount) > 0)
        //            //    {
        //            //        list.Add(entities);
        //            //    }


        //            //}

        //            //list.Add(entities);

        //            //if ((int.Parse(LOACount) > 0 && int.Parse(ActualDeliveryCount) <= 0) || (int.Parse(LOACount) <= 0 && int.Parse(ActualDeliveryCount) <= 0))
        //            //{
        //            //    list.Add(entities);
        //            //    if (LOACount == ActualDeliveryCount)
        //            //    {
        //            //        //DO NOTHING
        //            //    }
        //            //    else
        //            //    {
        //            //        list.Add(entities);
        //            //    }
        //            //}


        //            if ((WarehouseTotalQty == WarehouseTotalActualQty) && (LOACount == ActualDeliveryCount))
        //            {
        //                // DO NOTHING
        //            }
        //            else
        //            {
        //                list.Add(entities);
        //            }
                    
        //        }

        //        reader.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    finally
        //    {
        //        cmd.Dispose();
        //        cmd = null;

        //        conn.Dispose();
        //        conn.Close();
        //        conn = null;

        //        if (reader != null)
        //        {
        //            reader = null;
        //        }
        //    }

        //    return list;

        //}


        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_ReceivingEntry(string search)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            string query = string.Empty;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                // SET THE DATE WHEN THE IMPLEMENTATION START (CONVERT(DATE, STAT.DOAPur_Manager) >= '10/8/2022')
                if (string.IsNullOrEmpty(search))
                {
                    query = "SELECT TOP 500 HEAD.RefId, HEAD.CTRLNo, HEAD.LOA8106, HEAD.Category, CAT.Description AS CategoryName, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = HEAD.Requester) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS PezaNonPeza, " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS Warehouse1, " +
                            "(SELECT SUBSTRING((SELECT DISTINCT(LOA8105) + ',' AS 'data()' FROM SRF_TRANSACTION_Warehouse_Actual_Delivery where SRFNumber = HEAD.CTRLNo FOR XML PATH('')), 1 , 9999)) AS LOA8105, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(TotalQuantity) = 1 THEN CAST(TotalQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Request_Details WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS WarehouseTotalQty, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(ActualQuantity) = 1 THEN CAST(ActualQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS WarehouseTotalActualQty, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS LOACount,  " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND (WithDocuments = '0' OR WithDocuments IS NULL)) AS LOACount2,  " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND LOA8105 IS NOT NULL) AS ActualDeliveryCount, " +
                            "STAT.* " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' ";
                            //"AND HEAD.Category NOT IN ('" + ConfigurationManager.AppSettings["SRF_PO_FIXED_CATEGORY"].ToString() + "') AND LEN(HEAD.LOA8106) > 0";
                }
                else
                {
                    query = "SELECT TOP 500 HEAD.RefId, HEAD.CTRLNo, HEAD.LOA8106, HEAD.Category, CAT.Description AS CategoryName, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = HEAD.Requester) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS PezaNonPeza, " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS Warehouse1, " +
                            "(SELECT SUBSTRING((SELECT DISTINCT(LOA8105) + ',' AS 'data()' FROM SRF_TRANSACTION_Warehouse_Actual_Delivery where SRFNumber = HEAD.CTRLNo FOR XML PATH('')), 1 , 9999)) AS LOA8105, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(TotalQuantity) = 1 THEN CAST(TotalQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Request_Details WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS WarehouseTotalQty, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(ActualQuantity) = 1 THEN CAST(ActualQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS WarehouseTotalActualQty, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS LOACount,  " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND (WithDocuments = '0' OR WithDocuments IS NULL)) AS LOACount2,  " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND LOA8105 IS NOT NULL) AS ActualDeliveryCount, " +
                            "STAT.* " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.CTRLNo LIKE '%" + search + "%' OR HEAD.LOA8106 LIKE '%" + search + "%') ";
                            //"AND HEAD.Category NOT IN ('" + ConfigurationManager.AppSettings["SRF_PO_FIXED_CATEGORY"].ToString() + "') AND LEN(HEAD.LOA8106) > 0";
                }

                cmd.CommandText = query;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    string Warehouse1 = reader["Warehouse1"] != DBNull.Value ? reader["Warehouse1"].ToString() : string.Empty;
                    string WarehouseTotalQty = reader["WarehouseTotalQty"] != DBNull.Value ? reader["WarehouseTotalQty"].ToString() : string.Empty;
                    string WarehouseTotalActualQty = reader["WarehouseTotalActualQty"] != DBNull.Value ? reader["WarehouseTotalActualQty"].ToString() : string.Empty;
                    string LOACount = reader["LOACount"] != DBNull.Value ? reader["LOACount"].ToString() : string.Empty;
                    string ActualDeliveryCount = reader["ActualDeliveryCount"] != DBNull.Value ? reader["ActualDeliveryCount"].ToString() : string.Empty;

                    entities.RefId = reader["RefId"].ToString();
                    entities.Category = reader["Category"].ToString();
                    entities.CategoryDescription = reader["CategoryName"].ToString();
                    entities.Warehouse_CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.Warehouse_LOA8106 = reader["LOA8106"] != DBNull.Value ? reader["LOA8106"].ToString() : string.Empty;
                    entities.Warehouse_8105 = reader["LOA8106"] != DBNull.Value ? reader["LOA8105"].ToString() : string.Empty;
                    entities.Warehouse_PezaNonPeza = reader["PezaNonPeza"] != DBNull.Value ? reader["PezaNonPeza"].ToString() : string.Empty;
                    entities.Warehouse_RequesterName = CryptorEngine.Decrypt(reader["RequesterName"].ToString(), true).ToUpper();
                    entities.Warehouse_SupplierName = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : string.Empty;
                    entities.Warehouse_LoaCount2 = reader["LOACount2"] != DBNull.Value ? reader["LOACount2"].ToString() : "0";

                    entities.ReqInchargeStat = reader["STATReq_Incharge"] != DBNull.Value ? reader["STATReq_Incharge"].ToString() : "0";
                    entities.ReqManagerStat = reader["STATReq_Manager"] != DBNull.Value ? reader["STATReq_Manager"].ToString() : "0";
                    entities.PurInchargeStat = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
                    entities.PurImpexStat = reader["STATPur_Manager"] != DBNull.Value ? reader["STATPur_Manager"].ToString() : "0";

                    if (entities.ReqManagerStat == "0" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PROD.MNGR. APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.INCHARGE APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.IMPEX PROCESSING";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1")
                    {
                        entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && !string.IsNullOrEmpty(Warehouse1))
                    {
                        //entities.StatAll = "DELIVERY IN-PROGRESS";

                        string stat = string.Empty;

                        stat += "<table><tr>DELIVERY IN-PROGRESS</tr>";

                        List<Entities_SRF_RequestEntry> inProgress = new List<Entities_SRF_RequestEntry>();
                        inProgress = SRF_TRANSACTION_GetInProgressByCTRLNo(reader["CtrlNo"].ToString().ToUpper());

                        if (inProgress != null)
                        {
                            if (inProgress.Count > 0)
                            {
                                foreach (Entities_SRF_RequestEntry eProgress in inProgress)
                                {
                                    stat += "<tr><td>[" + eProgress.RefId + "] " + eProgress.Warehouse_ItemName + "</td></tr>";
                                }
                            }
                        }

                        stat += "</table>";

                        entities.StatAll = stat;
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && (!string.IsNullOrEmpty(WarehouseTotalQty) && !string.IsNullOrEmpty(WarehouseTotalActualQty)))
                    {
                        if (WarehouseTotalQty == WarehouseTotalActualQty)
                        {
                            entities.StatAll = "DELIVERED";
                        }
                        else
                        {
                            string stat2 = string.Empty;
                            

                            List<Entities_SRF_RequestEntry> inProgress2 = new List<Entities_SRF_RequestEntry>();
                            inProgress2 = SRF_TRANSACTION_GetInProgressByCTRLNo(reader["CtrlNo"].ToString().ToUpper());

                            if (inProgress2.Count > 0)
                            {
                                stat2 += "<table><tr>DELIVERY IN-PROGRESS</tr>";

                                foreach (Entities_SRF_RequestEntry eProgress2 in inProgress2)
                                {
                                    stat2 += "<tr><td>[" + eProgress2.RefId + "] " + eProgress2.Warehouse_ItemName + "</td></tr>";
                                }

                                stat2 += "</table>";
                                entities.StatAll = stat2;
                            }
                            else
                            {
                                entities.StatAll = "DELIVERED WITH PENDING ITEMS";
                            }

                        }
                    }
                    if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurImpexStat == "2")
                    {
                        entities.StatAll = "DISAPPROVED";
                    }
                    if (entities.PurImpexStat == "3")
                    {
                        entities.StatAll = "CANCELED";
                    }

                    //if (!string.IsNullOrEmpty(WarehouseTotalQty))
                    //{

                    //    if (int.Parse(LOACount) <= 0)
                    //    {
                    //        //DO NOTHING
                    //    }
                    //    else
                    //    {
                    //        list.Add(entities);
                    //    }

                    //}

                    //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && (!string.IsNullOrEmpty(WarehouseTotalQty) && !string.IsNullOrEmpty(WarehouseTotalActualQty)))
                    //{
                    //    if (int.Parse(LOACount) > 0)
                    //    {
                    //        list.Add(entities);
                    //    }


                    //}

                    //list.Add(entities);

                    //if ((int.Parse(LOACount) > 0 && int.Parse(ActualDeliveryCount) <= 0) || (int.Parse(LOACount) <= 0 && int.Parse(ActualDeliveryCount) <= 0))
                    //{
                    //    list.Add(entities);
                    //    if (LOACount == ActualDeliveryCount)
                    //    {
                    //        //DO NOTHING
                    //    }
                    //    else
                    //    {
                    //        list.Add(entities);
                    //    }
                    //}

                    if ((WarehouseTotalQty == WarehouseTotalActualQty) && (LOACount == ActualDeliveryCount))
                    {
                        list.Add(entities);
                        entities.StatAll = "CLOSED";
                    }
                    else
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_ReceivingEntryTotalOverallQuantity(string from, string to)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            string query = string.Empty;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;


                query = "SELECT SUM(CONVERT(BIGINT,REPLACE(totalquantity,',',''))) AS OverallTotalQty " +
                        "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                        "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " + 
                        "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " + 
                        "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                        "AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' AND ISDATE(HEAD.PullOutServiceDate) > 0";


                cmd.CommandText = query;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.Warehouse_OverallTotalQty = reader["OverallTotalQty"].ToString();
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange(string search, string from, string to)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            string query = string.Empty;
            string old8106 = string.Empty;
            string oldCtrlNo = string.Empty;
            int counter8106 = 0;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                // SET THE DATE WHEN THE IMPLEMENTATION START (CONVERT(DATE, STAT.DOAPur_Manager) >= '10/8/2022')
                if (string.IsNullOrEmpty(search))
                {
                    query = "SELECT TOP 50000 HEAD.RefId, HEAD.LOANo, HEAD.CTRLNo, HEAD.LOA8106, HEAD.Category, CAT.Description AS CategoryName,HEAD.PullOutServiceDate, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = HEAD.Requester) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS PezaNonPeza, " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS Warehouse1, " +
                            "(SELECT SUBSTRING((SELECT DISTINCT(LOA8105) + ',' AS 'data()' FROM SRF_TRANSACTION_Warehouse_Actual_Delivery where SRFNumber = HEAD.CTRLNo FOR XML PATH('')), 1 , 9999)) AS LOA8105, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(TotalQuantity) = 1 THEN CAST(TotalQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Request_Details WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS WarehouseTotalQty, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(ActualQuantity) = 1 THEN CAST(ActualQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS WarehouseTotalActualQty, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS LOACount,  " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND (WithDocuments = '0' OR WithDocuments IS NULL)) AS LOACount2,  " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND LOA8105 IS NOT NULL) AS ActualDeliveryCount, " +
                            "(SELECT COUNT(*) FROM SRF_TRANSACTION_Request_Details WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo AND RefId NOT IN (SELECT DetailsRefId FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo)) AS StillWithNoDelivery, " +
                            "STAT.*, " +
                            "(SELECT TOP 1 IsDisabled FROM SRF_MT_LOA WITH (NOLOCK) WHERE RefId = HEAD.LOANo) AS LOADisabled " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                            "AND HEAD.Supplier NOT IN (442,445,565,451,12) " +
                            "AND (HEAD.Category NOT IN (1012) AND HEAD.Supplier NOT LIKE '%MECHATECH%') " +
                            "AND ISNUMERIC(SUBSTRING(HEAD.LOA8106,0,2)) = 1 " +
                            "AND CONVERT(DATE, (CASE WHEN ISDATE(HEAD.PullOutServiceDate) > 0 THEN FORMAT(CAST(HEAD.PullOutServiceDate AS DATE), 'MM/dd/yyyy') END)) >= '" + from + "' AND CONVERT(DATE, (CASE WHEN ISDATE(HEAD.PullOutServiceDate) > 0 THEN FORMAT(CAST(HEAD.PullOutServiceDate AS DATE), 'MM/dd/yyyy') END)) <= '" + to + "' AND ISDATE(HEAD.PullOutServiceDate) > 0 ORDER BY CONVERT(DATE ,HEAD.PullOutServiceDate), HEAD.LOA8106 DESC";
                            //"AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' AND ISDATE(HEAD.PullOutServiceDate) > 0 ORDER BY CONVERT(DATE ,HEAD.PullOutServiceDate), HEAD.LOA8106 DESC";
                    //"AND HEAD.Category NOT IN ('" + ConfigurationManager.AppSettings["SRF_PO_FIXED_CATEGORY"].ToString() + "') AND LEN(HEAD.LOA8106) > 0";
                }
                else
                {
                    query = "SELECT TOP 50000 HEAD.RefId, HEAD.LOANo, HEAD.CTRLNo, HEAD.LOA8106, HEAD.Category, CAT.Description AS CategoryName,HEAD.PullOutServiceDate, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = HEAD.Requester) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS PezaNonPeza, " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS Warehouse1, " +
                            "(SELECT SUBSTRING((SELECT DISTINCT(LOA8105) + ',' AS 'data()' FROM SRF_TRANSACTION_Warehouse_Actual_Delivery where SRFNumber = HEAD.CTRLNo FOR XML PATH('')), 1 , 9999)) AS LOA8105, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(TotalQuantity) = 1 THEN CAST(TotalQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Request_Details WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS WarehouseTotalQty, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(ActualQuantity) = 1 THEN CAST(ActualQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS WarehouseTotalActualQty, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS LOACount,  " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND (WithDocuments = '0' OR WithDocuments IS NULL)) AS LOACount2,  " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND LOA8105 IS NOT NULL) AS ActualDeliveryCount, " +
                            "(SELECT COUNT(*) FROM SRF_TRANSACTION_Request_Details WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo AND RefId NOT IN (SELECT DetailsRefId FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo)) AS StillWithNoDelivery, " +
                            "STAT.*, " +
                            "(SELECT TOP 1 IsDisabled FROM SRF_MT_LOA WITH (NOLOCK) WHERE RefId = HEAD.LOANo) AS LOADisabled " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.CTRLNo LIKE '%" + search + "%' OR HEAD.LOA8106 LIKE '%" + search + "%') " +
                            "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) ";
                            //"AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' AND ISDATE(HEAD.PullOutServiceDate) > 0 ORDER BY CONVERT(DATE ,HEAD.PullOutServiceDate), HEAD.LOA8106 DESC";
                            //"AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' AND ISDATE(HEAD.PullOutServiceDate) > 0 ORDER BY CONVERT(DATE ,HEAD.PullOutServiceDate) DESC";
                    //"AND HEAD.Category NOT IN ('" + ConfigurationManager.AppSettings["SRF_PO_FIXED_CATEGORY"].ToString() + "') AND LEN(HEAD.LOA8106) > 0";
                }

                cmd.CommandText = query;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    string Warehouse1 = reader["Warehouse1"] != DBNull.Value ? reader["Warehouse1"].ToString() : string.Empty;
                    string WarehouseTotalQty = reader["WarehouseTotalQty"] != DBNull.Value ? reader["WarehouseTotalQty"].ToString() : string.Empty;
                    string WarehouseTotalActualQty = reader["WarehouseTotalActualQty"] != DBNull.Value ? reader["WarehouseTotalActualQty"].ToString() : string.Empty;
                    string LOACount = reader["LOACount"] != DBNull.Value ? reader["LOACount"].ToString() : string.Empty;
                    string ActualDeliveryCount = reader["ActualDeliveryCount"] != DBNull.Value ? reader["ActualDeliveryCount"].ToString() : string.Empty;
                    string LOADisabled = reader["LOADisabled"] != DBNull.Value ? reader["LOADisabled"].ToString() : string.Empty;

                    entities.RefId = reader["RefId"].ToString();
                    entities.Category = reader["Category"].ToString();
                    entities.CategoryDescription = reader["CategoryName"].ToString();
                    entities.Warehouse_CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.Warehouse_LOANo = reader["LOANo"].ToString().ToUpper();
                    entities.Warehouse_LOA8106 = reader["LOA8106"] != DBNull.Value ? reader["LOA8106"].ToString() : string.Empty;
                    entities.Warehouse_8105 = reader["LOA8106"] != DBNull.Value ? reader["LOA8105"].ToString() : string.Empty;
                    entities.Warehouse_PezaNonPeza = reader["PezaNonPeza"] != DBNull.Value ? reader["PezaNonPeza"].ToString() : string.Empty;
                    entities.Warehouse_RequesterName = CryptorEngine.Decrypt(reader["RequesterName"].ToString(), true).ToUpper();
                    entities.Warehouse_SupplierName = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : string.Empty;
                    entities.Warehouse_LoaCount2 = reader["LOACount2"] != DBNull.Value ? reader["LOACount2"].ToString() : "0";
                    entities.Warehouse_PullOutServiceDate = reader["PullOutServiceDate"] != DBNull.Value ? reader["PullOutServiceDate"].ToString() : string.Empty;

                    entities.ReqInchargeStat = reader["STATReq_Incharge"] != DBNull.Value ? reader["STATReq_Incharge"].ToString() : "0";
                    entities.ReqManagerStat = reader["STATReq_Manager"] != DBNull.Value ? reader["STATReq_Manager"].ToString() : "0";
                    entities.PurInchargeStat = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
                    entities.PurImpexStat = reader["STATPur_Manager"] != DBNull.Value ? reader["STATPur_Manager"].ToString() : "0";

                    entities.StillWithNoDelivery = reader["StillWithNoDelivery"] != DBNull.Value ? reader["StillWithNoDelivery"].ToString() : "0";

                    if (old8106 != entities.Warehouse_LOA8106)
                    {
                        counter8106++;
                    }

                    entities.Warehouse_OverallTotalQty = WarehouseTotalQty;
                    entities.Warehouse_TotalActualQuantity = reader["WarehouseTotalActualQty"] != DBNull.Value ? reader["WarehouseTotalActualQty"].ToString() : "0";


                    if (entities.ReqManagerStat == "0" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PROD.MNGR. APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.INCHARGE APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.IMPEX PROCESSING";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1")
                    {
                        entities.Old8106 = counter8106.ToString();
                        entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && !string.IsNullOrEmpty(Warehouse1))
                    {
                        //entities.StatAll = "DELIVERY IN-PROGRESS";

                        string stat = string.Empty;

                        stat += "<table><tr>DELIVERY IN-PROGRESS</tr>";

                        List<Entities_SRF_RequestEntry> inProgress = new List<Entities_SRF_RequestEntry>();
                        inProgress = SRF_TRANSACTION_GetInProgressByCTRLNo(reader["CtrlNo"].ToString().ToUpper());

                        if (inProgress != null)
                        {
                            if (inProgress.Count > 0)
                            {
                                foreach (Entities_SRF_RequestEntry eProgress in inProgress)
                                {
                                    stat += "<tr><td>[" + eProgress.RefId + "] " + eProgress.Warehouse_ItemName + "</td></tr>";
                                }
                            }
                        }

                        stat += "</table>";

                        entities.StatAll = stat;
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && (!string.IsNullOrEmpty(WarehouseTotalQty) && !string.IsNullOrEmpty(WarehouseTotalActualQty)))
                    {
                        if (WarehouseTotalQty == WarehouseTotalActualQty)
                        {
                            entities.Old8106 = counter8106.ToString();
                            entities.StatAll = "DELIVERED";
                        }
                        else
                        {
                            string stat2 = string.Empty;


                            List<Entities_SRF_RequestEntry> inProgress2 = new List<Entities_SRF_RequestEntry>();
                            inProgress2 = SRF_TRANSACTION_GetInProgressByCTRLNo(reader["CtrlNo"].ToString().ToUpper());

                            if (inProgress2.Count > 0)
                            {
                                stat2 += "<table><tr>DELIVERY IN-PROGRESS</tr>";

                                foreach (Entities_SRF_RequestEntry eProgress2 in inProgress2)
                                {
                                    stat2 += "<tr><td>[" + eProgress2.RefId + "] " + eProgress2.Warehouse_ItemName + "</td></tr>";
                                }

                                stat2 += "</table>";
                                entities.StatAll = stat2;
                            }
                            else
                            {
                                entities.Old8106 = counter8106.ToString();
                                entities.StatAll = "DELIVERED WITH PENDING ITEMS";
                                
                            }

                        }
                    }                    


                    if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurImpexStat == "2")
                    {
                        entities.StatAll = "DISAPPROVED";
                    }
                    if (entities.PurImpexStat == "3")
                    {
                        entities.StatAll = "CANCELED";
                    }

                    

                    if ((WarehouseTotalQty == WarehouseTotalActualQty) && (LOACount == ActualDeliveryCount))
                    {                        
                        if (LOADisabled == "0")
                        {
                            entities.Old8106 = counter8106.ToString();
                            entities.StatAll = "CLOSED";
                            list.Add(entities);
                        }
                        
                    }
                    else
                    {
                        if (LOADisabled == "0" || LOADisabled == "" || string.IsNullOrEmpty(LOADisabled))
                        {
                            //if (int.Parse(entities.StillWithNoDelivery) > 0 && oldCtrlNo != entities.Warehouse_CtrlNo)
                            //{
                            //    entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                            //}

                            list.Add(entities);                            

                        }
                    }

                    



                    //int stillWW = 0;
                    //List<Entities_SRF_RequestEntry> stillWithWaiting = new List<Entities_SRF_RequestEntry>();
                    //stillWithWaiting = SRF_TRANSACTION_Warehouse_Reporting_FromDashboard_2(search, reader["CtrlNo"].ToString().ToUpper());

                    //if (stillWithWaiting != null)
                    //{
                    //    if (stillWithWaiting.Count > 0)
                    //    {
                    //        foreach (Entities_SRF_RequestEntry eStillWithWaiting in stillWithWaiting)
                    //        {
                    //            if (long.Parse(eStillWithWaiting.Warehouse_TotalActualQuantity) <= 0)
                    //            {
                    //                stillWW++;
                    //            }
                    //        }
                    //    }
                    //}

                    //if (stillWW > 0)
                    //{
                    //    entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                    //    list.Add(entities);
                    //}


                    old8106 = entities.Warehouse_LOA8106;
                    oldCtrlNo = entities.Warehouse_CtrlNo;

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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_Reporting_FromDashboard_Summarize(string search, string from, string to)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            string query = string.Empty;
            string old8106 = string.Empty;
            string oldCtrlNo = string.Empty;
            int counter8106 = 0;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                // SET THE DATE WHEN THE IMPLEMENTATION START (CONVERT(DATE, STAT.DOAPur_Manager) >= '10/8/2022')
                query = "SELECT TOP 50000 HEAD.RefId, HEAD.LOANo, HEAD.CTRLNo, HEAD.LOA8106, HEAD.Category, CAT.Description AS CategoryName,HEAD.PullOutServiceDate, " +
                        "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = HEAD.Requester) AS RequesterName, " +
                        "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
                        "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS PezaNonPeza, " +
                        "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS Warehouse1, " +
                        "(SELECT SUBSTRING((SELECT DISTINCT(LOA8105) + ',' AS 'data()' FROM SRF_TRANSACTION_Warehouse_Actual_Delivery where SRFNumber = HEAD.CTRLNo FOR XML PATH('')), 1 , 9999)) AS LOA8105, " +
                        "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(TotalQuantity) = 1 THEN CAST(TotalQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Request_Details WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS WarehouseTotalQty, " +
                        "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(ActualQuantity) = 1 THEN CAST(ActualQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS WarehouseTotalActualQty, " +
                        "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS LOACount,  " +
                        "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND (WithDocuments = '0' OR WithDocuments IS NULL)) AS LOACount2,  " +
                        "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND LOA8105 IS NOT NULL) AS ActualDeliveryCount, " +
                        "(SELECT COUNT(*) FROM SRF_TRANSACTION_Request_Details WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo AND RefId NOT IN (SELECT DetailsRefId FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo)) AS StillWithNoDelivery, " +
                        "(SELECT SUBSTRING((SELECT DISTINCT(AddedDate) + ',' AS 'data()' FROM SRF_TRANSACTION_Warehouse_Actual_Delivery where SRFNumber = HEAD.CTRLNo FOR XML PATH('')), 1 , 9999)) AS DeliveredDate, " +
                        "STAT.*, " +
                        "(SELECT TOP 1 IsDisabled FROM SRF_MT_LOA WITH (NOLOCK) WHERE RefId = HEAD.LOANo) AS LOADisabled " +
                        "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                        "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                        "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                        "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                        "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                        "AND HEAD.Supplier NOT IN (442,445,565,451,12) " +
                        "AND (HEAD.Category NOT IN (1012) AND HEAD.Supplier NOT LIKE '%MECHATECH%') " +
                        "AND ISNUMERIC(SUBSTRING(HEAD.LOA8106,0,2)) = 1 " +
                        "AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' AND ISDATE(HEAD.PullOutServiceDate) > 0 " + 
                        "AND HEAD.CTRLNo IN (" + search + ") " +
                        "ORDER BY CONVERT(DATE ,HEAD.PullOutServiceDate), HEAD.LOA8106 DESC";

                cmd.CommandText = query;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    string Warehouse1 = reader["Warehouse1"] != DBNull.Value ? reader["Warehouse1"].ToString() : string.Empty;
                    string WarehouseTotalQty = reader["WarehouseTotalQty"] != DBNull.Value ? reader["WarehouseTotalQty"].ToString() : string.Empty;
                    string WarehouseTotalActualQty = reader["WarehouseTotalActualQty"] != DBNull.Value ? reader["WarehouseTotalActualQty"].ToString() : string.Empty;
                    string LOACount = reader["LOACount"] != DBNull.Value ? reader["LOACount"].ToString() : string.Empty;
                    string ActualDeliveryCount = reader["ActualDeliveryCount"] != DBNull.Value ? reader["ActualDeliveryCount"].ToString() : string.Empty;
                    string LOADisabled = reader["LOADisabled"] != DBNull.Value ? reader["LOADisabled"].ToString() : string.Empty;

                    entities.RefId = reader["RefId"].ToString();
                    entities.Category = reader["Category"].ToString();
                    entities.CategoryDescription = reader["CategoryName"].ToString();
                    entities.Warehouse_CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.Warehouse_LOANo = reader["LOANo"].ToString().ToUpper();
                    entities.Warehouse_LOA8106 = reader["LOA8106"] != DBNull.Value ? reader["LOA8106"].ToString() : string.Empty;
                    entities.Warehouse_8105 = reader["LOA8106"] != DBNull.Value ? reader["LOA8105"].ToString() : string.Empty;
                    entities.Warehouse_DeliveredDate = reader["DeliveredDate"] != DBNull.Value ? reader["DeliveredDate"].ToString() : string.Empty;
                    entities.Warehouse_PezaNonPeza = reader["PezaNonPeza"] != DBNull.Value ? reader["PezaNonPeza"].ToString() : string.Empty;
                    entities.Warehouse_RequesterName = CryptorEngine.Decrypt(reader["RequesterName"].ToString(), true).ToUpper();
                    entities.Warehouse_SupplierName = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : string.Empty;
                    entities.Warehouse_LoaCount2 = reader["LOACount2"] != DBNull.Value ? reader["LOACount2"].ToString() : "0";
                    entities.Warehouse_PullOutServiceDate = reader["PullOutServiceDate"] != DBNull.Value ? reader["PullOutServiceDate"].ToString() : string.Empty;

                    entities.ReqInchargeStat = reader["STATReq_Incharge"] != DBNull.Value ? reader["STATReq_Incharge"].ToString() : "0";
                    entities.ReqManagerStat = reader["STATReq_Manager"] != DBNull.Value ? reader["STATReq_Manager"].ToString() : "0";
                    entities.PurInchargeStat = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
                    entities.PurImpexStat = reader["STATPur_Manager"] != DBNull.Value ? reader["STATPur_Manager"].ToString() : "0";

                    entities.StillWithNoDelivery = reader["StillWithNoDelivery"] != DBNull.Value ? reader["StillWithNoDelivery"].ToString() : "0";

                    if (old8106 != entities.Warehouse_LOA8106)
                    {
                        counter8106++;
                    }

                    entities.Warehouse_OverallTotalQty = WarehouseTotalQty;
                    entities.Warehouse_TotalActualQuantity = reader["WarehouseTotalActualQty"] != DBNull.Value ? reader["WarehouseTotalActualQty"].ToString() : "0";


                    if (entities.ReqManagerStat == "0" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PROD.MNGR. APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.INCHARGE APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.IMPEX PROCESSING";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1")
                    {
                        entities.Old8106 = counter8106.ToString();
                        entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && !string.IsNullOrEmpty(Warehouse1))
                    {
                        //entities.StatAll = "DELIVERY IN-PROGRESS";

                        string stat = string.Empty;

                        stat += "<table><tr>DELIVERY IN-PROGRESS</tr>";

                        List<Entities_SRF_RequestEntry> inProgress = new List<Entities_SRF_RequestEntry>();
                        inProgress = SRF_TRANSACTION_GetInProgressByCTRLNo(reader["CtrlNo"].ToString().ToUpper());

                        if (inProgress != null)
                        {
                            if (inProgress.Count > 0)
                            {
                                foreach (Entities_SRF_RequestEntry eProgress in inProgress)
                                {
                                    stat += "<tr><td>[" + eProgress.RefId + "] " + eProgress.Warehouse_ItemName + "</td></tr>";
                                }
                            }
                        }

                        stat += "</table>";

                        entities.StatAll = stat;
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && (!string.IsNullOrEmpty(WarehouseTotalQty) && !string.IsNullOrEmpty(WarehouseTotalActualQty)))
                    {
                        if (WarehouseTotalQty == WarehouseTotalActualQty)
                        {
                            entities.Old8106 = counter8106.ToString();
                            entities.StatAll = "DELIVERED";
                        }
                        else
                        {
                            string stat2 = string.Empty;


                            List<Entities_SRF_RequestEntry> inProgress2 = new List<Entities_SRF_RequestEntry>();
                            inProgress2 = SRF_TRANSACTION_GetInProgressByCTRLNo(reader["CtrlNo"].ToString().ToUpper());

                            if (inProgress2.Count > 0)
                            {
                                stat2 += "<table><tr>DELIVERY IN-PROGRESS</tr>";

                                foreach (Entities_SRF_RequestEntry eProgress2 in inProgress2)
                                {
                                    stat2 += "<tr><td>[" + eProgress2.RefId + "] " + eProgress2.Warehouse_ItemName + "</td></tr>";
                                }

                                stat2 += "</table>";
                                entities.StatAll = stat2;
                            }
                            else
                            {
                                entities.Old8106 = counter8106.ToString();
                                entities.StatAll = "DELIVERED WITH PENDING ITEMS";

                            }

                        }
                    }


                    if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurImpexStat == "2")
                    {
                        entities.StatAll = "DISAPPROVED";
                    }
                    if (entities.PurImpexStat == "3")
                    {
                        entities.StatAll = "CANCELED";
                    }



                    if ((WarehouseTotalQty == WarehouseTotalActualQty) && (LOACount == ActualDeliveryCount))
                    {
                        if (LOADisabled == "0")
                        {
                            entities.Old8106 = counter8106.ToString();
                            entities.StatAll = "CLOSED";
                            list.Add(entities);
                        }

                    }
                    else
                    {
                        if (LOADisabled == "0" || LOADisabled == "" || string.IsNullOrEmpty(LOADisabled))
                        {

                            list.Add(entities);

                        }
                    }



                    old8106 = entities.Warehouse_LOA8106;
                    oldCtrlNo = entities.Warehouse_CtrlNo;

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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_ReceivingEntryWithDateRange_DashboardCount(string from, string to)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            string query = string.Empty;

            long dashboardPeza_ApprovedWaiting_8106_Count = 0;
            long dashboardPeza_ApprovedWaiting_Pullout_Count = 0;
            long dashboardPeza_ApprovedWaiting_Remaining_Count = 0;

            long dashboardPeza_DeliveredPending_8106_Count = 0;
            long dashboardPeza_DeliveredPending_Pullout_Count = 0;
            long dashboardPeza_DeliveredPending_Remaining_Count = 0;

            long dashboardPeza_Delivered_8106_Count = 0;
            long dashboardPeza_Delivered_Pullout_Count = 0;
            long dashboardPeza_Delivered_Remaining_Count = 0;

            string old8106 = string.Empty;
            int counter8106 = 0;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                // SET THE DATE WHEN THE IMPLEMENTATION START (CONVERT(DATE, STAT.DOAPur_Manager) >= '10/8/2022')
                //query = "SELECT T.* FROM ( " +
                //        "SELECT TOP 50000 HEAD.RefId, HEAD.CTRLNo, HEAD.LOA8106, HEAD.Category, CAT.Description AS CategoryName,HEAD.PullOutServiceDate, " +
                //        "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = HEAD.Requester) AS RequesterName, " +
                //        "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
                //        "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS PezaNonPeza, " +
                //        "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS Warehouse1, " +
                //        //"(SELECT SUBSTRING((SELECT DISTINCT(LOA8105) + ',' AS 'data()' FROM SRF_TRANSACTION_Warehouse_Actual_Delivery where SRFNumber = HEAD.CTRLNo FOR XML PATH('')), 1 , 9999)) AS LOA8105, " +
                //        "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(TotalQuantity) = 1 THEN CAST(TotalQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Request_Details WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS WarehouseTotalQty, " +
                //        "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(ActualQuantity) = 1 THEN CAST(ActualQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS WarehouseTotalActualQty, " +
                //        "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS LOACount,  " +
                //        "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND (WithDocuments = '0' OR WithDocuments IS NULL)) AS LOACount2,  " +
                //        "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND LOA8105 IS NOT NULL) AS ActualDeliveryCount, " +
                //        "STAT.Req_Incharge, STAT.DOAReq_Incharge, STAT.STATReq_Incharge, STAT.Req_Manager, STAT.DOAReq_Manager, STAT.STATReq_Manager, STAT.Pur_Incharge, " +
                //        "STAT.DOAPur_Incharge, STAT.STATPur_Incharge, STAT.Pur_Manager, STAT.DOAPur_Manager, STAT.STATPur_Manager, STAT.Remarks, STAT.Pur_DeptManager, STAT.STATPur_DeptManager, STAT.DOAPur_DeptManager " +
                //        "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                //        "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                //        "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                //        "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                //        "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                //        "AND ISDATE(HEAD.PullOutServiceDate) > 0 " +
                //        ") AS T WHERE CONVERT(DATE,T.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE,T.DOAPur_Manager) <= '" + to + "' ORDER BY T.PezaNonPeza, T.LOA8106 ASC";
                //        //") AS T WHERE CONVERT(DATE,T.PullOutServiceDate) >= '" + from + "' AND CONVERT(DATE,T.PullOutServiceDate) <= '" + to + "' ORDER BY T.PezaNonPeza, T.LOA8106 ASC";
                //        //STAT.DOAPur_Manager


                query = "SELECT TOP 50000 HEAD.RefId, HEAD.CTRLNo, HEAD.LOA8106, HEAD.Category, CAT.Description AS CategoryName,HEAD.PullOutServiceDate, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = HEAD.Requester) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS PezaNonPeza, " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS Warehouse1, " +
                            //"(SELECT SUBSTRING((SELECT DISTINCT(LOA8105) + ',' AS 'data()' FROM SRF_TRANSACTION_Warehouse_Actual_Delivery where SRFNumber = HEAD.CTRLNo FOR XML PATH('')), 1 , 9999)) AS LOA8105, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(TotalQuantity) = 1 THEN CAST(TotalQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Request_Details WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS WarehouseTotalQty, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(ActualQuantity) = 1 THEN CAST(ActualQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS WarehouseTotalActualQty, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS LOACount,  " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND (WithDocuments = '0' OR WithDocuments IS NULL)) AS LOACount2,  " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND LOA8105 IS NOT NULL) AS ActualDeliveryCount, " +
                            "STAT.* " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                            //"AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' AND ISDATE(HEAD.PullOutServiceDate) > 0 ORDER BY CONVERT(DATE ,HEAD.PullOutServiceDate), HEAD.LOA8106 DESC";
                            "AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' AND ISDATE(HEAD.PullOutServiceDate) > 0 ORDER BY CONVERT(DATE ,STAT.DOAPur_Manager), HEAD.LOA8106 DESC";

                cmd.CommandText = query;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    string Warehouse1 = reader["Warehouse1"] != DBNull.Value ? reader["Warehouse1"].ToString() : string.Empty;
                    string WarehouseTotalQty = reader["WarehouseTotalQty"] != DBNull.Value ? reader["WarehouseTotalQty"].ToString() : "0";
                    string WarehouseTotalActualQty = reader["WarehouseTotalActualQty"] != DBNull.Value ? reader["WarehouseTotalActualQty"].ToString() : "0";
                    string LOACount = reader["LOACount"] != DBNull.Value ? reader["LOACount"].ToString() : string.Empty;
                    string ActualDeliveryCount = reader["ActualDeliveryCount"] != DBNull.Value ? reader["ActualDeliveryCount"].ToString() : string.Empty;

                    entities.RefId = reader["RefId"].ToString();
                    entities.Category = reader["Category"].ToString();
                    entities.CategoryDescription = reader["CategoryName"].ToString();
                    entities.Warehouse_CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.Warehouse_LOA8106 = reader["LOA8106"] != DBNull.Value ? reader["LOA8106"].ToString() : string.Empty;
                    //entities.Warehouse_8105 = reader["LOA8105"] != DBNull.Value ? reader["LOA8105"].ToString() : string.Empty;
                    entities.Warehouse_PezaNonPeza = reader["PezaNonPeza"] != DBNull.Value ? reader["PezaNonPeza"].ToString() : string.Empty;
                    entities.Warehouse_RequesterName = CryptorEngine.Decrypt(reader["RequesterName"].ToString(), true).ToUpper();
                    entities.Warehouse_SupplierName = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : string.Empty;
                    entities.Warehouse_LoaCount2 = reader["LOACount2"] != DBNull.Value ? reader["LOACount2"].ToString() : "0";
                    entities.Warehouse_PullOutServiceDate = reader["PullOutServiceDate"] != DBNull.Value ? reader["PullOutServiceDate"].ToString() : string.Empty;

                    entities.Warehouse_TotalQuantity = WarehouseTotalQty;
                    entities.Warehouse_TotalActualQuantity = WarehouseTotalActualQty;

                    entities.ReqInchargeStat = reader["STATReq_Incharge"] != DBNull.Value ? reader["STATReq_Incharge"].ToString() : "0";
                    entities.ReqManagerStat = reader["STATReq_Manager"] != DBNull.Value ? reader["STATReq_Manager"].ToString() : "0";
                    entities.PurInchargeStat = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
                    entities.PurImpexStat = reader["STATPur_Manager"] != DBNull.Value ? reader["STATPur_Manager"].ToString() : "0";


                    if (old8106 != entities.Warehouse_LOA8106)
                    {
                        counter8106++;
                    }


                    if (entities.ReqManagerStat == "0" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PROD.MNGR. APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.INCHARGE APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.IMPEX PROCESSING";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1")
                    {
                        entities.StatAll = "APPROVED / WAITING FOR DELIVERY";

                        dashboardPeza_ApprovedWaiting_8106_Count++;
                        dashboardPeza_ApprovedWaiting_Pullout_Count += long.Parse(WarehouseTotalQty.Trim());
                        dashboardPeza_ApprovedWaiting_Remaining_Count += long.Parse(WarehouseTotalActualQty.Trim());
                        entities.Old8106 = counter8106.ToString();
                        
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && !string.IsNullOrEmpty(Warehouse1))
                    {
                        //entities.StatAll = "DELIVERY IN-PROGRESS";

                        string stat = string.Empty;

                        stat += "<table><tr>DELIVERY IN-PROGRESS</tr>";

                        List<Entities_SRF_RequestEntry> inProgress = new List<Entities_SRF_RequestEntry>();
                        inProgress = SRF_TRANSACTION_GetInProgressByCTRLNo(reader["CtrlNo"].ToString().ToUpper());

                        if (inProgress != null)
                        {
                            if (inProgress.Count > 0)
                            {
                                foreach (Entities_SRF_RequestEntry eProgress in inProgress)
                                {
                                    stat += "<tr><td>[" + eProgress.RefId + "] " + eProgress.Warehouse_ItemName + "</td></tr>";
                                }
                            }
                        }

                        stat += "</table>";

                        entities.StatAll = stat;
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && (!string.IsNullOrEmpty(WarehouseTotalQty) && !string.IsNullOrEmpty(WarehouseTotalActualQty)))
                    {
                        if (WarehouseTotalQty == WarehouseTotalActualQty)
                        {
                            entities.StatAll = "DELIVERED";

                            dashboardPeza_Delivered_8106_Count++;
                            dashboardPeza_Delivered_Pullout_Count += long.Parse(WarehouseTotalQty.Trim());
                            dashboardPeza_Delivered_Remaining_Count += long.Parse(WarehouseTotalActualQty.Trim());
                            entities.Old8106 = counter8106.ToString();
                        }
                        else
                        {
                            string stat2 = string.Empty;

                            if (WarehouseTotalQty != "0" && WarehouseTotalActualQty != "0")
                            {

                                List<Entities_SRF_RequestEntry> inProgress2 = new List<Entities_SRF_RequestEntry>();
                                inProgress2 = SRF_TRANSACTION_GetInProgressByCTRLNo(reader["CtrlNo"].ToString().ToUpper());

                                if (inProgress2.Count > 0)
                                {
                                    stat2 += "<table><tr>DELIVERY IN-PROGRESS</tr>";

                                    foreach (Entities_SRF_RequestEntry eProgress2 in inProgress2)
                                    {
                                        stat2 += "<tr><td>[" + eProgress2.RefId + "] " + eProgress2.Warehouse_ItemName + "</td></tr>";
                                    }

                                    stat2 += "</table>";
                                    entities.StatAll = stat2;
                                }
                                else
                                {
                                    entities.StatAll = "DELIVERED WITH PENDING ITEMS";

                                    dashboardPeza_DeliveredPending_8106_Count++;
                                    dashboardPeza_DeliveredPending_Pullout_Count += long.Parse(WarehouseTotalQty.Trim());
                                    dashboardPeza_DeliveredPending_Remaining_Count += long.Parse(WarehouseTotalActualQty.Trim());
                                    entities.Old8106 = counter8106.ToString();

                                }

                            }

                        }
                    }
                    if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurImpexStat == "2")
                    {
                        entities.StatAll = "DISAPPROVED";
                    }
                    if (entities.PurImpexStat == "3")
                    {
                        entities.StatAll = "CANCELED";
                    }



                    if ((WarehouseTotalQty == WarehouseTotalActualQty) && (LOACount == ActualDeliveryCount))
                    {
                        entities.StatAll = "CLOSED";
                        entities.Old8106 = counter8106.ToString();
                        //list.Add(entities);
                        
                    }
                    //else
                    //{
                    //    list.Add(entities);
                    //}

                    list.Add(entities);


                    old8106 = entities.Warehouse_LOA8106;

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




        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_8105_Entry(string search)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            string query = string.Empty;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                // SET THE DATE WHEN THE IMPLEMENTATION START (CONVERT(DATE, STAT.DOAPur_Manager) >= '10/8/2022')
                //if (string.IsNullOrEmpty(search))
                //{
                //    query = "SELECT TOP 500 HEAD.RefId, HEAD.CTRLNo, HEAD.LOA8106, HEAD.Category, " +
                //            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = HEAD.Requester) AS RequesterName, " +
                //            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
                //            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS Warehouse1, " +
                //            "(SELECT SUM(CONVERT(BIGINT,TotalQuantity)) FROM SRF_TRANSACTION_Request_Details WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS WarehouseTotalQty, " +
                //            "(SELECT SUM(CONVERT(BIGINT,ActualQuantity)) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS WarehouseTotalActualQty, " +
                //            "(SELECT SUM(CONVERT(BIGINT,quantity)) FROM SRF_TRANSACTION_8105 WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS TotalQty8105, " +
                //            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND LOA8105 IS NULL) AS LOACount, " +
                //            "STAT.* " +
                //            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                //            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                //            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '10/8/2022' ";
                //}
                //else
                //{
                //    query = "SELECT TOP 500 HEAD.RefId, HEAD.CTRLNo, HEAD.LOA8106, HEAD.Category, " +
                //            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = HEAD.Requester) AS RequesterName, " +
                //            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
                //            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS Warehouse1, " +
                //            "(SELECT SUM(CONVERT(BIGINT,TotalQuantity)) FROM SRF_TRANSACTION_Request_Details WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS WarehouseTotalQty, " +
                //            "(SELECT SUM(CONVERT(BIGINT,ActualQuantity)) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS WarehouseTotalActualQty, " +
                //            "(SELECT SUM(CONVERT(BIGINT,quantity)) FROM SRF_TRANSACTION_8105 WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS TotalQty8105, " +
                //            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND LOA8105 IS NULL) AS LOACount, " +
                //            "STAT.* " +
                //            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                //            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                //            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '10/8/2022' " +
                //            "AND (HEAD.CTRLNo LIKE '%" + search + "%' OR HEAD.LOA8106 LIKE '%" + search + "%')";
                //}

                if (string.IsNullOrEmpty(search))
                {
                    query = "SELECT TOP 500 ACTUAL.SRFNumber, DETAILS.RefId, DETAILS.TotalQuantity, ACTUAL.ActualQuantity, ACTUAL.LOA8105, DETAILS.ItemName, " +
                            "(SELECT TOP 1 LOA8106 FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS LOA8106, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = (SELECT Requester FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS SupplierName, " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS Warehouse1, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = ACTUAL.SRFNumber AND LOA8105 IS NULL) AS LOACount, " +
                            "(SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS Category, " +
                            "ACTUAL.AddedDate AS DeliveredDate, STAT.* " +
                            "FROM SRF_TRANSACTION_Warehouse_Actual_Delivery ACTUAL WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Request_Details DETAILS WITH (NOLOCK) ON ACTUAL.DetailsRefId = DETAILS.RefId " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON ACTUAL.SRFNumber = STAT.CTRLNo " +
                            "WHERE ACTUAL.SRFNumber IN (SELECT CTRLNo FROM SRF_TRANSACTION_Status WITH (NOLOCK) WHERE STATPur_Manager = 1 AND CONVERT(DATE, DOAPur_Manager) >= '12/19/2022') " +
                            "ORDER BY ACTUAL.SRFNumber, DETAILS.ItemName";
                }
                else
                {
                    query = "SELECT TOP 500 ACTUAL.SRFNumber, DETAILS.RefId, DETAILS.TotalQuantity, ACTUAL.ActualQuantity, ACTUAL.LOA8105, DETAILS.ItemName, " +
                            "(SELECT TOP 1 LOA8106 FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS LOA8106, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = (SELECT Requester FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS SupplierName, " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS Warehouse1, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = ACTUAL.SRFNumber AND LOA8105 IS NULL) AS LOACount, " +
                            "(SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS Category, " +
                            "ACTUAL.AddedDate AS DeliveredDate, STAT.* " +
                            "FROM SRF_TRANSACTION_Warehouse_Actual_Delivery ACTUAL WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Request_Details DETAILS WITH (NOLOCK) ON ACTUAL.DetailsRefId = DETAILS.RefId " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON ACTUAL.SRFNumber = STAT.CTRLNo " +
                            "WHERE ACTUAL.SRFNumber IN (SELECT CTRLNo FROM SRF_TRANSACTION_Status WITH (NOLOCK) WHERE STATPur_Manager = 1 AND CONVERT(DATE, DOAPur_Manager) >= '12/19/2022') " +
                            "AND (ACTUAL.SRFNumber LIKE '%" + search + "%' OR LOA8106 LIKE '%" + search + "%')" +
                            "ORDER BY ACTUAL.SRFNumber, DETAILS.ItemName";
                }

                cmd.CommandText = query;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    string Warehouse1 = reader["Warehouse1"] != DBNull.Value ? reader["Warehouse1"].ToString() : string.Empty;
                    string WarehouseTotalQty = reader["TotalQuantity"] != DBNull.Value ? reader["TotalQuantity"].ToString() : "0";
                    string WarehouseTotalActualQty = reader["ActualQuantity"] != DBNull.Value ? reader["ActualQuantity"].ToString() : "0";
                    //string TotalQty8105 = reader["TotalQty8105"] != DBNull.Value ? reader["TotalQty8105"].ToString() : string.Empty;
                    string LOACount = reader["LOACount"] != DBNull.Value ? reader["LOACount"].ToString() : string.Empty;

                    entities.RefId = reader["RefId"].ToString();
                    entities.Warehouse_CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.Warehouse_LOA8106 = reader["LOA8106"] != DBNull.Value ? reader["LOA8106"].ToString() : string.Empty;
                    entities.Warehouse_8105 = reader["LOA8105"] != DBNull.Value ? reader["LOA8105"].ToString() : string.Empty;
                    entities.Warehouse_ItemName = reader["ItemName"].ToString();
                    entities.Warehouse_DeliveredDate = reader["DeliveredDate"] != DBNull.Value ? reader["DeliveredDate"].ToString() : string.Empty;
                    entities.Warehouse_RequesterName = CryptorEngine.Decrypt(reader["RequesterName"].ToString(), true).ToUpper();
                    entities.Warehouse_SupplierName = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : string.Empty;
                    entities.Category = reader["Category"].ToString();
                    entities.Warehouse_TotalQuantity = WarehouseTotalQty;
                    entities.Warehouse_TotalActualQuantity = WarehouseTotalActualQty;
                    //entities.Warehouse_RemainingQuantity = (int.Parse(WarehouseTotalQty) - int.Parse(WarehouseTotalActualQty)).ToString();

                    entities.ReqInchargeStat = reader["STATReq_Incharge"] != DBNull.Value ? reader["STATReq_Incharge"].ToString() : "0";
                    entities.ReqManagerStat = reader["STATReq_Manager"] != DBNull.Value ? reader["STATReq_Manager"].ToString() : "0";
                    entities.PurInchargeStat = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
                    entities.PurImpexStat = reader["STATPur_Manager"] != DBNull.Value ? reader["STATPur_Manager"].ToString() : "0";

                    if (entities.ReqManagerStat == "0" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PROD.MNGR. APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.INCHARGE APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.IMPEX PROCESSING";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1")
                    {
                        entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && !string.IsNullOrEmpty(Warehouse1))
                    {
                        entities.StatAll = "DELIVERY IN-PROGRESS";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && (!string.IsNullOrEmpty(WarehouseTotalQty) && !string.IsNullOrEmpty(WarehouseTotalActualQty)))
                    {
                        if (WarehouseTotalQty == WarehouseTotalActualQty)
                        {
                            entities.StatAll = "DELIVERED";
                        }
                        else
                        {
                            entities.StatAll = "DELIVERED WITH PENDING ITEMS";
                        }
                    }
                    if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurImpexStat == "2")
                    {
                        entities.StatAll = "DISAPPROVED";
                    }
                    if (entities.PurImpexStat == "3")
                    {
                        entities.StatAll = "CANCELED";
                    }


                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && (!string.IsNullOrEmpty(WarehouseTotalQty) && !string.IsNullOrEmpty(WarehouseTotalActualQty)))
                    {
                        if (int.Parse(LOACount) > 0)
                        {
                            list.Add(entities);
                        }
                        
                        
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_Reporting(string search, string from, string to)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            string query = string.Empty;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                if (string.IsNullOrEmpty(search))
                {
                    query = "SELECT ACTUAL.SRFNumber, ACTUAL.RefId AS ActualID, DETAILS.RefId AS DetailsRefId, " +
                            "CAST(CASE WHEN ISNUMERIC(DETAILS.TotalQuantity) = 1 THEN CAST(DETAILS.TotalQuantity AS MONEY) ELSE 0 END AS BIGINT) AS TotalQuantity, " +
							"CAST(CASE WHEN ISNUMERIC(ACTUAL.ActualQuantity) = 1 THEN CAST(ACTUAL.ActualQuantity AS MONEY) ELSE 0 END AS BIGINT) AS ActualQuantity, " +
                            "ACTUAL.LOA8105, DETAILS.ItemName, ACTUAL.LOA8105ProcessDate, " +
                            "(SELECT LOANo FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOA_Number_From_HEAD, " +
                            "(SELECT MaturityDate FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOA_MaturityDate_From_HEAD, " +
                            "(SELECT LOASuretyBond FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS LOASuretyBond_From_HEAD, " +
                            "(SELECT (CASE WHEN ISDATE(pulloutservicedate) = 1 THEN CONVERT(DATE,PullOutServiceDate) END) FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS PullOutServiceDate_From_HEAD, " +
                            "(SELECT GatePassNo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS GatePassNo_From_HEAD, " +
                            "(SELECT LOAPriceValue FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOAPriceValue_From_HEAD, " +
                            "(SELECT SUM(CONVERT(BIGINT,ActualQuantity)) FROM  SRF_TRANSACTION_Warehouse_Actual_Delivery WHERE SRFNumber = ACTUAL.SRFNumber AND DetailsRefId = DETAILS.RefId) AS ActualQuantity2, " +
                            "(SELECT TOP 1 LOA8106 FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS LOA8106, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = (SELECT Requester FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS PezaNonPeza,  " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS Warehouse1, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = ACTUAL.SRFNumber AND LOA8105 IS NULL) AS LOACount, " +
                            "(SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS Category, " +
                            "(SELECT Description from MT_Category WITH (NOLOCK) WHERE RefId = (SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS CategoryName, " +
                            "ACTUAL.AddedDate AS DeliveredDate, STAT.*, " +
                            "(SELECT TOP 1 IsDisabled FROM SRF_MT_LOA WITH (NOLOCK) WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOADisabled " +
                            "FROM SRF_TRANSACTION_Warehouse_Actual_Delivery ACTUAL WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Request_Details DETAILS WITH (NOLOCK) ON ACTUAL.DetailsRefId = DETAILS.RefId " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON ACTUAL.SRFNumber = STAT.CTRLNo " +
                            "WHERE ACTUAL.SRFNumber IN (SELECT CTRLNo FROM SRF_TRANSACTION_Status WITH (NOLOCK) WHERE STATPur_Manager = 1 AND CONVERT(DATE, DOAPur_Manager) >= '12/19/2022') " +
                            "AND CONVERT(DATE, DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,DOAPur_Manager) <= '" + to + "' " +

                            "AND ACTUAL.SRFNumber IN ( " +
                            "SELECT HEAD.CTRLNo " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                            "AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' " +
                                                ") " +

                            "UNION " +

                            "SELECT DETAIL.CTRLNo AS SRFNumber, '0' AS ActualID, DETAIL.RefId AS DetailsRefId, CAST(CASE WHEN ISNUMERIC(DETAIL.TotalQuantity) = 1 THEN CAST(DETAIL.TotalQuantity AS MONEY) ELSE 0 END AS BIGINT) AS TotalQuantity, '0' AS ActualQuantity, '' AS LOA8105, DETAIL.ItemName, '' AS LOA8105ProcessDate, " +
                            "(SELECT LOANo FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOA_Number_From_HEAD, " +
                            "(SELECT MaturityDate FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOA_MaturityDate_From_HEAD, " +
                            "(SELECT LOASuretyBond FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS LOASuretyBond_From_HEAD, " +
                            "(SELECT (CASE WHEN ISDATE(pulloutservicedate) = 1 THEN CONVERT(DATE,PullOutServiceDate) END) FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS PullOutServiceDate_From_HEAD, " +
                            "(SELECT GatePassNo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS GatePassNo_From_HEAD, " +
                            "(SELECT LOAPriceValue FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOAPriceValue_From_HEAD, " +
                            "(SELECT SUM(CONVERT(BIGINT,ActualQuantity)) FROM  SRF_TRANSACTION_Warehouse_Actual_Delivery WHERE SRFNumber = DETAIL.CTRLNo AND DetailsRefId = DETAIL.RefId) AS ActualQuantity2, " +
                            "(SELECT TOP 1 LOA8106 FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS LOA8106, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = (SELECT Requester FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS PezaNonPeza, " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS Warehouse1, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = DETAIL.CTRLNo AND LOA8105 IS NULL) AS LOACount, " +
                            "(SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS Category, " +
                            "(SELECT Description from MT_Category WITH (NOLOCK) WHERE RefId = (SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS CategoryName, " +
                            "'' AS DeliveredDate, STAT.*, " +
                            "(SELECT TOP 1 IsDisabled FROM SRF_MT_LOA WITH (NOLOCK) WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOADisabled " +
                            "FROM SRF_TRANSACTION_Request_Details DETAIL WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON DETAIL.CTRLNo = STAT.CTRLNo " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND DETAIL.RefId NOT IN (SELECT DetailsRefId FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK)) " +

                            "AND DETAIL.CTRLNo IN ( " +
                            "SELECT HEAD.CTRLNo " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                            "AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' " +
                                                ") " +


                            "ORDER BY SupplierName, ACTUAL.SRFNumber, DETAILS.ItemName, ACTUAL.RefId ASC  ";

                }
                else
                {
                    query = "SELECT ACTUAL.SRFNumber, ACTUAL.RefId AS ActualID, DETAILS.RefId AS DetailsRefId, DETAILS.TotalQuantity, ACTUAL.ActualQuantity, ACTUAL.LOA8105, DETAILS.ItemName, ACTUAL.LOA8105ProcessDate, " +
                            "(SELECT LOANo FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOA_Number_From_HEAD, " +
                            "(SELECT MaturityDate FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOA_MaturityDate_From_HEAD, " +
                            "(SELECT LOASuretyBond FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS LOASuretyBond_From_HEAD, " +
                            "(SELECT PullOutServiceDate FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS PullOutServiceDate_From_HEAD, " +
                            "(SELECT GatePassNo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS GatePassNo_From_HEAD, " + 
                            "(SELECT LOAPriceValue FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOAPriceValue_From_HEAD, " +
                            "(SELECT SUM(CONVERT(BIGINT,ActualQuantity)) FROM  SRF_TRANSACTION_Warehouse_Actual_Delivery WHERE SRFNumber = ACTUAL.SRFNumber AND DetailsRefId = DETAILS.RefId) AS ActualQuantity2, " +
                            "(SELECT TOP 1 LOA8106 FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS LOA8106, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = (SELECT Requester FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS PezaNonPeza,  " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS Warehouse1, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = ACTUAL.SRFNumber AND LOA8105 IS NULL) AS LOACount, " +
                            "(SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS Category, " +
                            "(SELECT Description from MT_Category WITH (NOLOCK) WHERE RefId = (SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS CategoryName, " +
                            "(CASE WHEN LEN(ACTUAL.AddedDate) > 0 THEN (CASE WHEN ISDATE(ACTUAL.AddedDate) = 1 THEN CONVERT(DATE,ACTUAL.AddedDate) END) END) AS DeliveredDate, STAT.* " +
                            "FROM SRF_TRANSACTION_Warehouse_Actual_Delivery ACTUAL WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Request_Details DETAILS WITH (NOLOCK) ON ACTUAL.DetailsRefId = DETAILS.RefId " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON ACTUAL.SRFNumber = STAT.CTRLNo " +
                            "WHERE ACTUAL.SRFNumber IN (SELECT CTRLNo FROM SRF_TRANSACTION_Status WITH (NOLOCK) WHERE STATPur_Manager = 1 AND CONVERT(DATE, DOAPur_Manager) >= '12/19/2022') " +
                            "AND CONVERT(DATE, DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,DOAPur_Manager) <= '" + to + "' " +
                            "AND (ACTUAL.SRFNumber LIKE '%" + search + "%' OR LOA8106 LIKE '%" + search + "%')" +

                            "AND ACTUAL.SRFNumber IN ( " +
                            "SELECT HEAD.CTRLNo " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                            "AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' " +
                                                ") " +

                            "UNION " +

                            "SELECT DETAIL.CTRLNo AS SRFNumber, '0' AS ActualID, DETAIL.RefId AS DetailsRefId, DETAIL.TotalQuantity, '0' AS ActualQuantity, '' AS LOA8105, DETAIL.ItemName, '' AS LOA8105ProcessDate, " +
                            "(SELECT LOANo FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOA_Number_From_HEAD, " +
                            "(SELECT MaturityDate FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOA_MaturityDate_From_HEAD, " +
                            "(SELECT LOASuretyBond FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS LOASuretyBond_From_HEAD, " +
                            "(SELECT PullOutServiceDate FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS PullOutServiceDate_From_HEAD, " +
                            "(SELECT GatePassNo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS GatePassNo_From_HEAD, " +
                            "(SELECT LOAPriceValue FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOAPriceValue_From_HEAD, " +
                            "(SELECT SUM(CONVERT(BIGINT,ActualQuantity)) FROM  SRF_TRANSACTION_Warehouse_Actual_Delivery WHERE SRFNumber = DETAIL.CTRLNo AND DetailsRefId = DETAIL.RefId) AS ActualQuantity2, " +
                            "(SELECT TOP 1 LOA8106 FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS LOA8106, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = (SELECT Requester FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS PezaNonPeza, " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS Warehouse1, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = DETAIL.CTRLNo AND LOA8105 IS NULL) AS LOACount, " +
                            "(SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS Category, " +
                            "(SELECT Description from MT_Category WITH (NOLOCK) WHERE RefId = (SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS CategoryName, " +
                            "'' AS DeliveredDate, STAT.* " +
                            "FROM SRF_TRANSACTION_Request_Details DETAIL WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON DETAIL.CTRLNo = STAT.CTRLNo " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND DETAIL.RefId NOT IN (SELECT DetailsRefId FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK)) " +

                            "AND DETAIL.CTRLNo IN ( " +
                            "SELECT HEAD.CTRLNo " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                            "AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' " +
                                                 ") " +

                            "ORDER BY SupplierName, ACTUAL.SRFNumber, DETAILS.ItemName, ACTUAL.RefId ASC  ";


                }

                cmd.CommandText = query;

                reader = cmd.ExecuteReader();

                string previousActualQuantity = "0";
                string previousRefId = string.Empty;

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    string Warehouse1 = reader["Warehouse1"] != DBNull.Value ? reader["Warehouse1"].ToString() : string.Empty;
                    string WarehouseTotalQty = reader["TotalQuantity"] != DBNull.Value ? reader["TotalQuantity"].ToString() : "0";
                    string WarehouseTotalActualQty = reader["ActualQuantity"] != DBNull.Value ? reader["ActualQuantity"].ToString() : "0";
                    string WarehouseTotalActualQty2 = reader["ActualQuantity2"] != DBNull.Value ? reader["ActualQuantity2"].ToString() : "0";
                    //string TotalQty8105 = reader["TotalQty8105"] != DBNull.Value ? reader["TotalQty8105"].ToString() : string.Empty;
                    string LOACount = reader["LOACount"] != DBNull.Value ? reader["LOACount"].ToString() : string.Empty;
                    string LOADisabled = reader["LOADisabled"] != DBNull.Value ? reader["LOADisabled"].ToString() : string.Empty;

                    entities.RefId = reader["DetailsRefId"].ToString();
                    entities.CategoryDescription = reader["CategoryName"].ToString();
                    entities.Warehouse_CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.Warehouse_LOA8106 = reader["LOA8106"] != DBNull.Value ? reader["LOA8106"].ToString() : string.Empty;
                    entities.Warehouse_8105 = reader["LOA8105"] != DBNull.Value ? reader["LOA8105"].ToString() : string.Empty;
                    entities.Warehouse_LOA8105ProcessDate = reader["LOA8105ProcessDate"] != DBNull.Value ? reader["LOA8105ProcessDate"].ToString() : string.Empty;
                    entities.Warehouse_ItemName = reader["ItemName"] != DBNull.Value ? reader["ItemName"].ToString() : string.Empty;
                    entities.Warehouse_DeliveredDate = reader["DeliveredDate"] != DBNull.Value ? reader["DeliveredDate"].ToString().Replace("1900-01-01","") : string.Empty;
                    entities.Warehouse_RequesterName = reader["RequesterName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["RequesterName"].ToString(), true).ToUpper() : string.Empty;
                    entities.Warehouse_SupplierName = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : string.Empty;
                    entities.Category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : string.Empty;
                    entities.Warehouse_TotalQuantity = WarehouseTotalQty;
                    entities.Warehouse_TotalActualQuantity = WarehouseTotalActualQty;

                    //-----------------------------------------------------------------------------------------------------------------------------------
                    if (reader["DetailsRefId"].ToString() == previousRefId)
                    {
                        entities.Warehouse_RemainingQuantity = (int.Parse(WarehouseTotalQty) - (int.Parse(previousActualQuantity) + int.Parse(WarehouseTotalActualQty))).ToString();                        
                    }
                    else
                    {
                        entities.Warehouse_RemainingQuantity = (int.Parse(WarehouseTotalQty) - int.Parse(WarehouseTotalActualQty)).ToString();
                        previousActualQuantity = "0";
                    }
                    
                    previousRefId = reader["DetailsRefId"].ToString();
                    previousActualQuantity = (int.Parse(previousActualQuantity) + int.Parse(WarehouseTotalActualQty)).ToString();
                    //-----------------------------------------------------------------------------------------------------------------------------------

                    entities.LOA_Number_From_HEAD = reader["LOA_Number_From_HEAD"] != DBNull.Value ? reader["LOA_Number_From_HEAD"].ToString() : string.Empty;
                    entities.LOA_MaturityDate_From_HEAD = reader["LOA_MaturityDate_From_HEAD"] != DBNull.Value ? reader["LOA_MaturityDate_From_HEAD"].ToString() : string.Empty;
                    entities.LOASuretyBond_From_HEAD = reader["LOASuretyBond_From_HEAD"] != DBNull.Value ? reader["LOASuretyBond_From_HEAD"].ToString() : string.Empty;
                    entities.PullOutServiceDate_From_HEAD = reader["PullOutServiceDate_From_HEAD"] != DBNull.Value ? reader["PullOutServiceDate_From_HEAD"].ToString().Replace("12:00:00 AM","") : string.Empty;
                    entities.LOAPriceValue_From_HEAD = reader["LOAPriceValue_From_HEAD"] != DBNull.Value ? reader["LOAPriceValue_From_HEAD"].ToString() : "0";
                    entities.GatePassNo_From_HEAD = reader["GatePassNo_From_HEAD"] != DBNull.Value ? reader["GatePassNo_From_HEAD"].ToString() : string.Empty;

                    entities.Warehouse_PezaNonPeza = reader["PezaNonPeza"] != DBNull.Value ? reader["PezaNonPeza"].ToString() : string.Empty;

                    entities.ReqInchargeStat = reader["STATReq_Incharge"] != DBNull.Value ? reader["STATReq_Incharge"].ToString() : "0";
                    entities.ReqManagerStat = reader["STATReq_Manager"] != DBNull.Value ? reader["STATReq_Manager"].ToString() : "0";
                    entities.PurInchargeStat = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
                    entities.PurImpexStat = reader["STATPur_Manager"] != DBNull.Value ? reader["STATPur_Manager"].ToString() : "0";


                    if (entities.ReqManagerStat == "0" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PROD.MNGR. APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.INCHARGE APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.IMPEX PROCESSING";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1")
                    {
                        entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && !string.IsNullOrEmpty(Warehouse1))
                    {
                        entities.StatAll = "DELIVERY IN-PROGRESS";
                    }

                    if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurImpexStat == "2")
                    {
                        entities.StatAll = "DISAPPROVED";
                    }
                    if (entities.PurImpexStat == "3")
                    {
                        entities.StatAll = "CANCELED";
                    }


                    if ((WarehouseTotalQty == WarehouseTotalActualQty) && (int.Parse(entities.Warehouse_RemainingQuantity) <= 0))
                    {
                        if (LOADisabled == "0")
                        {
                            entities.StatAll = "CLOSED";

                        }

                    }


                    //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && (!string.IsNullOrEmpty(WarehouseTotalQty) && !string.IsNullOrEmpty(WarehouseTotalActualQty)))
                    //{
                    //    if (WarehouseTotalQty == WarehouseTotalActualQty)
                    //    {
                    //        entities.StatAll = "DELIVERED";
                    //    }
                    //    else
                    //    {
                    //        //if (WarehouseTotalActualQty == "0")
                    //        //{
                    //        //    entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                    //        //}
                    //        //else
                    //        //{
                    //        //    entities.StatAll = "DELIVERED WITH PENDING ITEMS";
                    //        //}

                    //        List<Entities_SRF_RequestEntry> inProgress2 = new List<Entities_SRF_RequestEntry>();
                    //        inProgress2 = SRF_TRANSACTION_GetInProgressByCTRLNo(reader["CtrlNo"].ToString().ToUpper());

                    //        if (inProgress2.Count > 0)
                    //        {
                    //            entities.StatAll = "DELIVERY IN-PROGRESS";
                    //        }
                    //        else
                    //        {
                    //            entities.StatAll = "DELIVERED WITH PENDING ITEMS";
                    //        }


                    //    }

                        
                    //}

                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && (entities.PurImpexStat == "1" || entities.PurImpexStat == "3"))
                    {
                        if (WarehouseTotalActualQty == "0")
                        {
                            entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                        }
                        else
                        {
                            if (int.Parse(entities.Warehouse_RemainingQuantity) > 0)
                            {
                                entities.StatAll = "DELIVERED WITH PENDING ITEMS";
                            }
                            else
                            {
                                entities.StatAll = "CLOSED";
                            }
                        }

                        if (WarehouseTotalQty == WarehouseTotalActualQty)
                        {
                            entities.StatAll = "DELIVERED";
                        }                        

                    }


                    if (LOADisabled == "0")
                    {
                        list.Add(entities);
                    }

                    
                    

                    //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && (!string.IsNullOrEmpty(WarehouseTotalQty) && !string.IsNullOrEmpty(WarehouseTotalActualQty)))
                    //{
                    //    if (int.Parse(LOACount) > 0)
                    //    {
                    //        if (LOADisabled == "0")
                    //        {
                    //            list.Add(entities);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (WarehouseTotalActualQty == "0")
                    //        {
                    //            if (LOADisabled == "0")
                    //            {
                    //                list.Add(entities);
                    //                entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                    //            }
                                
                    //        }
                    //        else
                    //        {
                    //            if (int.Parse(entities.Warehouse_RemainingQuantity) > 0)
                    //            {
                    //                if (LOADisabled == "0")
                    //                {
                    //                    list.Add(entities);
                    //                    entities.StatAll = "DELIVERED WITH PENDING ITEMS";
                    //                }
                                    
                    //            }
                    //            else
                    //            {
                    //                if (LOADisabled == "0")
                    //                {
                    //                    list.Add(entities);
                    //                    entities.StatAll = "CLOSED";
                    //                }
                                    
                    //            }
                    //        }

                    //    }



                    //}

                    //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1")
                    //{
                    //    if (WarehouseTotalActualQty == "0")
                    //    {
                    //        entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                    //    }
                    //    else
                    //    {
                    //        if (int.Parse(entities.Warehouse_RemainingQuantity) > 0)
                    //        {
                    //            entities.StatAll = "DELIVERED WITH PENDING ITEMS";
                    //        }
                    //        else
                    //        {
                    //            entities.StatAll = "CLOSED";
                    //        }
                    //    }

                    //    list.Add(entities);

                    //}

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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_Reporting_FromDashboard(string search, string from, string to, string ctrlnoList)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            string query = string.Empty;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                if (string.IsNullOrEmpty(search))
                {
                    query = "SELECT ACTUAL.SRFNumber, ACTUAL.RefId AS ActualID, DETAILS.RefId AS DetailsRefId, " +
                            "CAST(CASE WHEN ISNUMERIC(DETAILS.TotalQuantity) = 1 THEN CAST(DETAILS.TotalQuantity AS MONEY) ELSE 0 END AS BIGINT) AS TotalQuantity, " +
                            "CAST(CASE WHEN ISNUMERIC(ACTUAL.ActualQuantity) = 1 THEN CAST(ACTUAL.ActualQuantity AS MONEY) ELSE 0 END AS BIGINT) AS ActualQuantity, " +
                            "ACTUAL.LOA8105, DETAILS.ItemName, ACTUAL.LOA8105ProcessDate, " +
                            "(SELECT LOANo FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOA_Number_From_HEAD, " +
                            "(SELECT MaturityDate FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOA_MaturityDate_From_HEAD, " +
                            "(SELECT LOASuretyBond FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS LOASuretyBond_From_HEAD, " +
                            "(SELECT (CASE WHEN ISDATE(pulloutservicedate) = 1 THEN CONVERT(DATE,PullOutServiceDate) END) FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS PullOutServiceDate_From_HEAD, " +
                            "(SELECT GatePassNo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS GatePassNo_From_HEAD, " +
                            "(SELECT LOAPriceValue FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOAPriceValue_From_HEAD, " +
                            "(SELECT SUM(CONVERT(BIGINT,ActualQuantity)) FROM  SRF_TRANSACTION_Warehouse_Actual_Delivery WHERE SRFNumber = ACTUAL.SRFNumber AND DetailsRefId = DETAILS.RefId) AS ActualQuantity2, " +
                            "(SELECT TOP 1 LOA8106 FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS LOA8106, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = (SELECT Requester FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS PezaNonPeza,  " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS Warehouse1, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = ACTUAL.SRFNumber AND LOA8105 IS NULL) AS LOACount, " +
                            "(SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS Category, " +
                            "(SELECT Description from MT_Category WITH (NOLOCK) WHERE RefId = (SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS CategoryName, " +
                            "ACTUAL.AddedDate AS DeliveredDate, STAT.*, " +
                            "(SELECT TOP 1 IsDisabled FROM SRF_MT_LOA WITH (NOLOCK) WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOADisabled " +
                            "FROM SRF_TRANSACTION_Warehouse_Actual_Delivery ACTUAL WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Request_Details DETAILS WITH (NOLOCK) ON ACTUAL.DetailsRefId = DETAILS.RefId " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON ACTUAL.SRFNumber = STAT.CTRLNo " +
                            "WHERE ACTUAL.SRFNumber IN (SELECT CTRLNo FROM SRF_TRANSACTION_Status WITH (NOLOCK) WHERE STATPur_Manager = 1 AND CONVERT(DATE, DOAPur_Manager) >= '12/19/2022') " +
                            "AND CONVERT(DATE, DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,DOAPur_Manager) <= '" + to + "' " +

                            "AND ACTUAL.SRFNumber IN ( " +
                            "SELECT HEAD.CTRLNo " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                            "AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' AND HEAD.CTRLNo IN (" + ctrlnoList + ") " +
                                                ") " +

                            "UNION " +

                            "SELECT DETAIL.CTRLNo AS SRFNumber, '0' AS ActualID, DETAIL.RefId AS DetailsRefId, CAST(CASE WHEN ISNUMERIC(DETAIL.TotalQuantity) = 1 THEN CAST(DETAIL.TotalQuantity AS MONEY) ELSE 0 END AS BIGINT) AS TotalQuantity, '0' AS ActualQuantity, '' AS LOA8105, DETAIL.ItemName, '' AS LOA8105ProcessDate, " +
                            "(SELECT LOANo FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOA_Number_From_HEAD, " +
                            "(SELECT MaturityDate FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOA_MaturityDate_From_HEAD, " +
                            "(SELECT LOASuretyBond FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS LOASuretyBond_From_HEAD, " +
                            "(SELECT (CASE WHEN ISDATE(pulloutservicedate) = 1 THEN CONVERT(DATE,PullOutServiceDate) END) FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS PullOutServiceDate_From_HEAD, " +
                            "(SELECT GatePassNo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS GatePassNo_From_HEAD, " +
                            "(SELECT LOAPriceValue FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOAPriceValue_From_HEAD, " +
                            "(SELECT SUM(CONVERT(BIGINT,ActualQuantity)) FROM  SRF_TRANSACTION_Warehouse_Actual_Delivery WHERE SRFNumber = DETAIL.CTRLNo AND DetailsRefId = DETAIL.RefId) AS ActualQuantity2, " +
                            "(SELECT TOP 1 LOA8106 FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS LOA8106, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = (SELECT Requester FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS PezaNonPeza, " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS Warehouse1, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = DETAIL.CTRLNo AND LOA8105 IS NULL) AS LOACount, " +
                            "(SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS Category, " +
                            "(SELECT Description from MT_Category WITH (NOLOCK) WHERE RefId = (SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS CategoryName, " +
                            "'' AS DeliveredDate, STAT.*, " +
                            "(SELECT TOP 1 IsDisabled FROM SRF_MT_LOA WITH (NOLOCK) WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOADisabled " +
                            "FROM SRF_TRANSACTION_Request_Details DETAIL WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON DETAIL.CTRLNo = STAT.CTRLNo " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND DETAIL.RefId NOT IN (SELECT DetailsRefId FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK)) " +

                            "AND DETAIL.CTRLNo IN ( " +
                            "SELECT HEAD.CTRLNo " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                            "AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' AND HEAD.CTRLNo IN (" + ctrlnoList + ") " +
                                                ") " +


                            "ORDER BY SupplierName, ACTUAL.SRFNumber, DETAILS.ItemName, ACTUAL.RefId ASC  ";

                }
                else
                {
                    query = "SELECT ACTUAL.SRFNumber, ACTUAL.RefId AS ActualID, DETAILS.RefId AS DetailsRefId, DETAILS.TotalQuantity, ACTUAL.ActualQuantity, ACTUAL.LOA8105, DETAILS.ItemName, ACTUAL.LOA8105ProcessDate, " +
                            "(SELECT LOANo FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOA_Number_From_HEAD, " +
                            "(SELECT MaturityDate FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOA_MaturityDate_From_HEAD, " +
                            "(SELECT LOASuretyBond FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS LOASuretyBond_From_HEAD, " +
                            "(SELECT PullOutServiceDate FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS PullOutServiceDate_From_HEAD, " +
                            "(SELECT GatePassNo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS GatePassNo_From_HEAD, " +
                            "(SELECT LOAPriceValue FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOAPriceValue_From_HEAD, " +
                            "(SELECT SUM(CONVERT(BIGINT,ActualQuantity)) FROM  SRF_TRANSACTION_Warehouse_Actual_Delivery WHERE SRFNumber = ACTUAL.SRFNumber AND DetailsRefId = DETAILS.RefId) AS ActualQuantity2, " +
                            "(SELECT TOP 1 LOA8106 FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS LOA8106, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = (SELECT Requester FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS PezaNonPeza,  " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS Warehouse1, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = ACTUAL.SRFNumber AND LOA8105 IS NULL) AS LOACount, " +
                            "(SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS Category, " +
                            "(SELECT Description from MT_Category WITH (NOLOCK) WHERE RefId = (SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS CategoryName, " +
                            "(CASE WHEN LEN(ACTUAL.AddedDate) > 0 THEN (CASE WHEN ISDATE(ACTUAL.AddedDate) = 1 THEN CONVERT(DATE,ACTUAL.AddedDate) END) END) AS DeliveredDate, STAT.* " +
                            "FROM SRF_TRANSACTION_Warehouse_Actual_Delivery ACTUAL WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Request_Details DETAILS WITH (NOLOCK) ON ACTUAL.DetailsRefId = DETAILS.RefId " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON ACTUAL.SRFNumber = STAT.CTRLNo " +
                            "WHERE ACTUAL.SRFNumber IN (SELECT CTRLNo FROM SRF_TRANSACTION_Status WITH (NOLOCK) WHERE STATPur_Manager = 1 AND CONVERT(DATE, DOAPur_Manager) >= '12/19/2022') " +
                            "AND CONVERT(DATE, DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,DOAPur_Manager) <= '" + to + "' " +
                            "AND (ACTUAL.SRFNumber LIKE '%" + search + "%' OR LOA8106 LIKE '%" + search + "%')" +

                            "AND ACTUAL.SRFNumber IN ( " +
                            "SELECT HEAD.CTRLNo " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                            "AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' AND HEAD.CTRLNo IN (" + ctrlnoList + ") " +
                                                ") " +

                            "UNION " +

                            "SELECT DETAIL.CTRLNo AS SRFNumber, '0' AS ActualID, DETAIL.RefId AS DetailsRefId, DETAIL.TotalQuantity, '0' AS ActualQuantity, '' AS LOA8105, DETAIL.ItemName, '' AS LOA8105ProcessDate, " +
                            "(SELECT LOANo FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOA_Number_From_HEAD, " +
                            "(SELECT MaturityDate FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOA_MaturityDate_From_HEAD, " +
                            "(SELECT LOASuretyBond FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS LOASuretyBond_From_HEAD, " +
                            "(SELECT PullOutServiceDate FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS PullOutServiceDate_From_HEAD, " +
                            "(SELECT GatePassNo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS GatePassNo_From_HEAD, " +
                            "(SELECT LOAPriceValue FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOAPriceValue_From_HEAD, " +
                            "(SELECT SUM(CONVERT(BIGINT,ActualQuantity)) FROM  SRF_TRANSACTION_Warehouse_Actual_Delivery WHERE SRFNumber = DETAIL.CTRLNo AND DetailsRefId = DETAIL.RefId) AS ActualQuantity2, " +
                            "(SELECT TOP 1 LOA8106 FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS LOA8106, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = (SELECT Requester FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS PezaNonPeza, " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS Warehouse1, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = DETAIL.CTRLNo AND LOA8105 IS NULL) AS LOACount, " +
                            "(SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS Category, " +
                            "(SELECT Description from MT_Category WITH (NOLOCK) WHERE RefId = (SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS CategoryName, " +
                            "'' AS DeliveredDate, STAT.* " +
                            "FROM SRF_TRANSACTION_Request_Details DETAIL WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON DETAIL.CTRLNo = STAT.CTRLNo " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND DETAIL.RefId NOT IN (SELECT DetailsRefId FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK)) " +

                            "AND DETAIL.CTRLNo IN ( " +
                            "SELECT HEAD.CTRLNo " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                            "AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' AND HEAD.CTRLNo IN (" + ctrlnoList + ") " +
                                                 ") " +

                            "ORDER BY SupplierName, ACTUAL.SRFNumber, DETAILS.ItemName, ACTUAL.RefId ASC  ";


                }

                cmd.CommandText = query;

                reader = cmd.ExecuteReader();

                string previousActualQuantity = "0";
                string previousRefId = string.Empty;

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    string Warehouse1 = reader["Warehouse1"] != DBNull.Value ? reader["Warehouse1"].ToString() : string.Empty;
                    string WarehouseTotalQty = reader["TotalQuantity"] != DBNull.Value ? reader["TotalQuantity"].ToString() : "0";
                    string WarehouseTotalActualQty = reader["ActualQuantity"] != DBNull.Value ? reader["ActualQuantity"].ToString() : "0";
                    string WarehouseTotalActualQty2 = reader["ActualQuantity2"] != DBNull.Value ? reader["ActualQuantity2"].ToString() : "0";
                    //string TotalQty8105 = reader["TotalQty8105"] != DBNull.Value ? reader["TotalQty8105"].ToString() : string.Empty;
                    string LOACount = reader["LOACount"] != DBNull.Value ? reader["LOACount"].ToString() : string.Empty;
                    //string LOADisabled = reader["LOADisabled"] != DBNull.Value ? reader["LOADisabled"].ToString() : string.Empty;
                    string LOADisabled = reader["LOADisabled"] != DBNull.Value ? reader["LOADisabled"].ToString() : "0";

                    entities.RefId = reader["DetailsRefId"].ToString();
                    entities.CategoryDescription = reader["CategoryName"].ToString();
                    entities.Warehouse_CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.Warehouse_LOA8106 = reader["LOA8106"] != DBNull.Value ? reader["LOA8106"].ToString() : string.Empty;
                    entities.Warehouse_8105 = reader["LOA8105"] != DBNull.Value ? reader["LOA8105"].ToString() : string.Empty;
                    entities.Warehouse_LOA8105ProcessDate = reader["LOA8105ProcessDate"] != DBNull.Value ? reader["LOA8105ProcessDate"].ToString() : string.Empty;
                    entities.Warehouse_ItemName = reader["ItemName"] != DBNull.Value ? reader["ItemName"].ToString() : string.Empty;
                    entities.Warehouse_DeliveredDate = reader["DeliveredDate"] != DBNull.Value ? reader["DeliveredDate"].ToString().Replace("1900-01-01", "") : string.Empty;
                    entities.Warehouse_RequesterName = reader["RequesterName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["RequesterName"].ToString(), true).ToUpper() : string.Empty;
                    entities.Warehouse_SupplierName = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : string.Empty;
                    entities.Category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : string.Empty;
                    entities.Warehouse_TotalQuantity = WarehouseTotalQty;
                    entities.Warehouse_TotalActualQuantity = WarehouseTotalActualQty;

                    //-----------------------------------------------------------------------------------------------------------------------------------
                    if (reader["DetailsRefId"].ToString() == previousRefId)
                    {
                        entities.Warehouse_RemainingQuantity = (int.Parse(WarehouseTotalQty) - (int.Parse(previousActualQuantity) + int.Parse(WarehouseTotalActualQty))).ToString();
                    }
                    else
                    {
                        entities.Warehouse_RemainingQuantity = (int.Parse(WarehouseTotalQty) - int.Parse(WarehouseTotalActualQty)).ToString();
                        previousActualQuantity = "0";
                    }

                    previousRefId = reader["DetailsRefId"].ToString();
                    previousActualQuantity = (int.Parse(previousActualQuantity) + int.Parse(WarehouseTotalActualQty)).ToString();
                    //-----------------------------------------------------------------------------------------------------------------------------------

                    entities.LOA_Number_From_HEAD = reader["LOA_Number_From_HEAD"] != DBNull.Value ? reader["LOA_Number_From_HEAD"].ToString() : string.Empty;
                    entities.LOA_MaturityDate_From_HEAD = reader["LOA_MaturityDate_From_HEAD"] != DBNull.Value ? reader["LOA_MaturityDate_From_HEAD"].ToString() : string.Empty;
                    entities.LOASuretyBond_From_HEAD = reader["LOASuretyBond_From_HEAD"] != DBNull.Value ? reader["LOASuretyBond_From_HEAD"].ToString() : string.Empty;
                    entities.PullOutServiceDate_From_HEAD = reader["PullOutServiceDate_From_HEAD"] != DBNull.Value ? reader["PullOutServiceDate_From_HEAD"].ToString().Replace("12:00:00 AM", "") : string.Empty;
                    entities.LOAPriceValue_From_HEAD = reader["LOAPriceValue_From_HEAD"] != DBNull.Value ? reader["LOAPriceValue_From_HEAD"].ToString() : "0";
                    entities.GatePassNo_From_HEAD = reader["GatePassNo_From_HEAD"] != DBNull.Value ? reader["GatePassNo_From_HEAD"].ToString() : string.Empty;

                    entities.Warehouse_PezaNonPeza = reader["PezaNonPeza"] != DBNull.Value ? reader["PezaNonPeza"].ToString() : string.Empty;

                    entities.ReqInchargeStat = reader["STATReq_Incharge"] != DBNull.Value ? reader["STATReq_Incharge"].ToString() : "0";
                    entities.ReqManagerStat = reader["STATReq_Manager"] != DBNull.Value ? reader["STATReq_Manager"].ToString() : "0";
                    entities.PurInchargeStat = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
                    entities.PurImpexStat = reader["STATPur_Manager"] != DBNull.Value ? reader["STATPur_Manager"].ToString() : "0";


                    if (entities.ReqManagerStat == "0" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PROD.MNGR. APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.INCHARGE APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.IMPEX PROCESSING";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1")
                    {
                        entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && !string.IsNullOrEmpty(Warehouse1))
                    {
                        entities.StatAll = "DELIVERY IN-PROGRESS";
                    }

                    if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurImpexStat == "2")
                    {
                        entities.StatAll = "DISAPPROVED";
                    }
                    if (entities.PurImpexStat == "3")
                    {
                        entities.StatAll = "CANCELED";
                    }


                    if ((WarehouseTotalQty == WarehouseTotalActualQty) && (int.Parse(entities.Warehouse_RemainingQuantity) <= 0))
                    {
                        if (LOADisabled == "0")
                        {
                            entities.StatAll = "CLOSED";

                        }

                    }


                    //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && (!string.IsNullOrEmpty(WarehouseTotalQty) && !string.IsNullOrEmpty(WarehouseTotalActualQty)))
                    //{
                    //    if (WarehouseTotalQty == WarehouseTotalActualQty)
                    //    {
                    //        entities.StatAll = "DELIVERED";
                    //    }
                    //    else
                    //    {
                    //        //if (WarehouseTotalActualQty == "0")
                    //        //{
                    //        //    entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                    //        //}
                    //        //else
                    //        //{
                    //        //    entities.StatAll = "DELIVERED WITH PENDING ITEMS";
                    //        //}

                    //        List<Entities_SRF_RequestEntry> inProgress2 = new List<Entities_SRF_RequestEntry>();
                    //        inProgress2 = SRF_TRANSACTION_GetInProgressByCTRLNo(reader["CtrlNo"].ToString().ToUpper());

                    //        if (inProgress2.Count > 0)
                    //        {
                    //            entities.StatAll = "DELIVERY IN-PROGRESS";
                    //        }
                    //        else
                    //        {
                    //            entities.StatAll = "DELIVERED WITH PENDING ITEMS";
                    //        }


                    //    }


                    //}

                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && (entities.PurImpexStat == "1" || entities.PurImpexStat == "3"))
                    {
                        if (WarehouseTotalActualQty == "0")
                        {
                            entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                        }
                        else
                        {
                            if (int.Parse(entities.Warehouse_RemainingQuantity) > 0)
                            {
                                entities.StatAll = "DELIVERED WITH PENDING ITEMS";
                            }
                            else
                            {
                                entities.StatAll = "CLOSED";
                            }
                        }

                        if (WarehouseTotalQty == WarehouseTotalActualQty)
                        {
                            entities.StatAll = "DELIVERED";
                        }

                    }




                    if (LOADisabled == "0")
                    {
                        list.Add(entities);
                    }




                    //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && (!string.IsNullOrEmpty(WarehouseTotalQty) && !string.IsNullOrEmpty(WarehouseTotalActualQty)))
                    //{
                    //    if (int.Parse(LOACount) > 0)
                    //    {
                    //        if (LOADisabled == "0")
                    //        {
                    //            list.Add(entities);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (WarehouseTotalActualQty == "0")
                    //        {
                    //            if (LOADisabled == "0")
                    //            {
                    //                list.Add(entities);
                    //                entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                    //            }

                    //        }
                    //        else
                    //        {
                    //            if (int.Parse(entities.Warehouse_RemainingQuantity) > 0)
                    //            {
                    //                if (LOADisabled == "0")
                    //                {
                    //                    list.Add(entities);
                    //                    entities.StatAll = "DELIVERED WITH PENDING ITEMS";
                    //                }

                    //            }
                    //            else
                    //            {
                    //                if (LOADisabled == "0")
                    //                {
                    //                    list.Add(entities);
                    //                    entities.StatAll = "CLOSED";
                    //                }

                    //            }
                    //        }

                    //    }



                    //}

                    //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1")
                    //{
                    //    if (WarehouseTotalActualQty == "0")
                    //    {
                    //        entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                    //    }
                    //    else
                    //    {
                    //        if (int.Parse(entities.Warehouse_RemainingQuantity) > 0)
                    //        {
                    //            entities.StatAll = "DELIVERED WITH PENDING ITEMS";
                    //        }
                    //        else
                    //        {
                    //            entities.StatAll = "CLOSED";
                    //        }
                    //    }

                    //    list.Add(entities);

                    //}

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


        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_Reporting_FromDashboard_2(string search, string ctrlnoList)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            string query = string.Empty;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                if (string.IsNullOrEmpty(search))
                {
                    query = "SELECT ACTUAL.SRFNumber, ACTUAL.RefId AS ActualID, DETAILS.RefId AS DetailsRefId, " +
                            "CAST(CASE WHEN ISNUMERIC(DETAILS.TotalQuantity) = 1 THEN CAST(DETAILS.TotalQuantity AS MONEY) ELSE 0 END AS BIGINT) AS TotalQuantity, " +
                            "CAST(CASE WHEN ISNUMERIC(ACTUAL.ActualQuantity) = 1 THEN CAST(ACTUAL.ActualQuantity AS MONEY) ELSE 0 END AS BIGINT) AS ActualQuantity, " +
                            "ACTUAL.LOA8105, DETAILS.ItemName, ACTUAL.LOA8105ProcessDate, " +
                            "(SELECT LOANo FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOA_Number_From_HEAD, " +
                            "(SELECT MaturityDate FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOA_MaturityDate_From_HEAD, " +
                            "(SELECT LOASuretyBond FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS LOASuretyBond_From_HEAD, " +
                            "(SELECT (CASE WHEN ISDATE(pulloutservicedate) = 1 THEN CONVERT(DATE,PullOutServiceDate) END) FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS PullOutServiceDate_From_HEAD, " +
                            "(SELECT GatePassNo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS GatePassNo_From_HEAD, " +
                            "(SELECT LOAPriceValue FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOAPriceValue_From_HEAD, " +
                            "(SELECT SUM(CONVERT(BIGINT,ActualQuantity)) FROM  SRF_TRANSACTION_Warehouse_Actual_Delivery WHERE SRFNumber = ACTUAL.SRFNumber AND DetailsRefId = DETAILS.RefId) AS ActualQuantity2, " +
                            "(SELECT TOP 1 LOA8106 FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS LOA8106, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = (SELECT Requester FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS PezaNonPeza,  " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS Warehouse1, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = ACTUAL.SRFNumber AND LOA8105 IS NULL) AS LOACount, " +
                            "(SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS Category, " +
                            "(SELECT Description from MT_Category WITH (NOLOCK) WHERE RefId = (SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS CategoryName, " +
                            "ACTUAL.AddedDate AS DeliveredDate, STAT.*, " +
                            "(SELECT TOP 1 IsDisabled FROM SRF_MT_LOA WITH (NOLOCK) WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOADisabled " +
                            "FROM SRF_TRANSACTION_Warehouse_Actual_Delivery ACTUAL WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Request_Details DETAILS WITH (NOLOCK) ON ACTUAL.DetailsRefId = DETAILS.RefId " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON ACTUAL.SRFNumber = STAT.CTRLNo " +
                            "WHERE ACTUAL.SRFNumber IN (SELECT CTRLNo FROM SRF_TRANSACTION_Status WITH (NOLOCK) WHERE STATPur_Manager = 1 AND CONVERT(DATE, DOAPur_Manager) >= '12/19/2022') " +

                            "AND ACTUAL.SRFNumber IN ( " +
                            "SELECT HEAD.CTRLNo " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                            "AND HEAD.CTRLNo IN ('" + ctrlnoList + "') " +
                                                ") " +

                            "UNION " +

                            "SELECT DETAIL.CTRLNo AS SRFNumber, '0' AS ActualID, DETAIL.RefId AS DetailsRefId, CAST(CASE WHEN ISNUMERIC(DETAIL.TotalQuantity) = 1 THEN CAST(DETAIL.TotalQuantity AS MONEY) ELSE 0 END AS BIGINT) AS TotalQuantity, '0' AS ActualQuantity, '' AS LOA8105, DETAIL.ItemName, '' AS LOA8105ProcessDate, " +
                            "(SELECT LOANo FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOA_Number_From_HEAD, " +
                            "(SELECT MaturityDate FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOA_MaturityDate_From_HEAD, " +
                            "(SELECT LOASuretyBond FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS LOASuretyBond_From_HEAD, " +
                            "(SELECT (CASE WHEN ISDATE(pulloutservicedate) = 1 THEN CONVERT(DATE,PullOutServiceDate) END) FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS PullOutServiceDate_From_HEAD, " +
                            "(SELECT GatePassNo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS GatePassNo_From_HEAD, " +
                            "(SELECT LOAPriceValue FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOAPriceValue_From_HEAD, " +
                            "(SELECT SUM(CONVERT(BIGINT,ActualQuantity)) FROM  SRF_TRANSACTION_Warehouse_Actual_Delivery WHERE SRFNumber = DETAIL.CTRLNo AND DetailsRefId = DETAIL.RefId) AS ActualQuantity2, " +
                            "(SELECT TOP 1 LOA8106 FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS LOA8106, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = (SELECT Requester FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS PezaNonPeza, " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS Warehouse1, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = DETAIL.CTRLNo AND LOA8105 IS NULL) AS LOACount, " +
                            "(SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS Category, " +
                            "(SELECT Description from MT_Category WITH (NOLOCK) WHERE RefId = (SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS CategoryName, " +
                            "'' AS DeliveredDate, STAT.*, " +
                            "(SELECT TOP 1 IsDisabled FROM SRF_MT_LOA WITH (NOLOCK) WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOADisabled " +
                            "FROM SRF_TRANSACTION_Request_Details DETAIL WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON DETAIL.CTRLNo = STAT.CTRLNo " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND DETAIL.RefId NOT IN (SELECT DetailsRefId FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK)) " +

                            "AND DETAIL.CTRLNo IN ( " +
                            "SELECT HEAD.CTRLNo " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                            "AND HEAD.CTRLNo IN ('" + ctrlnoList + "') " +
                                                ") " +


                            "ORDER BY SupplierName, ACTUAL.SRFNumber, DETAILS.ItemName, ACTUAL.RefId ASC  ";

                }
                else
                {
                    query = "SELECT ACTUAL.SRFNumber, ACTUAL.RefId AS ActualID, DETAILS.RefId AS DetailsRefId, DETAILS.TotalQuantity, ACTUAL.ActualQuantity, ACTUAL.LOA8105, DETAILS.ItemName, ACTUAL.LOA8105ProcessDate, " +
                            "(SELECT LOANo FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOA_Number_From_HEAD, " +
                            "(SELECT MaturityDate FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOA_MaturityDate_From_HEAD, " +
                            "(SELECT LOASuretyBond FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS LOASuretyBond_From_HEAD, " +
                            "(SELECT PullOutServiceDate FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS PullOutServiceDate_From_HEAD, " +
                            "(SELECT GatePassNo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS GatePassNo_From_HEAD, " +
                            "(SELECT LOAPriceValue FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS LOAPriceValue_From_HEAD, " +
                            "(SELECT SUM(CONVERT(BIGINT,ActualQuantity)) FROM  SRF_TRANSACTION_Warehouse_Actual_Delivery WHERE SRFNumber = ACTUAL.SRFNumber AND DetailsRefId = DETAILS.RefId) AS ActualQuantity2, " +
                            "(SELECT TOP 1 LOA8106 FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS LOA8106, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = (SELECT Requester FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS PezaNonPeza,  " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS Warehouse1, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = ACTUAL.SRFNumber AND LOA8105 IS NULL) AS LOACount, " +
                            "(SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS Category, " +
                            "(SELECT Description from MT_Category WITH (NOLOCK) WHERE RefId = (SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS CategoryName, " +
                            "(CASE WHEN LEN(ACTUAL.AddedDate) > 0 THEN (CASE WHEN ISDATE(ACTUAL.AddedDate) = 1 THEN CONVERT(DATE,ACTUAL.AddedDate) END) END) AS DeliveredDate, STAT.* " +
                            "FROM SRF_TRANSACTION_Warehouse_Actual_Delivery ACTUAL WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Request_Details DETAILS WITH (NOLOCK) ON ACTUAL.DetailsRefId = DETAILS.RefId " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON ACTUAL.SRFNumber = STAT.CTRLNo " +
                            "WHERE ACTUAL.SRFNumber IN (SELECT CTRLNo FROM SRF_TRANSACTION_Status WITH (NOLOCK) WHERE STATPur_Manager = 1 AND CONVERT(DATE, DOAPur_Manager) >= '12/19/2022') " +
                            "AND (ACTUAL.SRFNumber LIKE '%" + search + "%' OR LOA8106 LIKE '%" + search + "%')" +

                            "AND ACTUAL.SRFNumber IN ( " +
                            "SELECT HEAD.CTRLNo " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                            "AND HEAD.CTRLNo IN ('" + ctrlnoList + "') " +
                                                ") " +

                            "UNION " +

                            "SELECT DETAIL.CTRLNo AS SRFNumber, '0' AS ActualID, DETAIL.RefId AS DetailsRefId, DETAIL.TotalQuantity, '0' AS ActualQuantity, '' AS LOA8105, DETAIL.ItemName, '' AS LOA8105ProcessDate, " +
                            "(SELECT LOANo FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOA_Number_From_HEAD, " +
                            "(SELECT MaturityDate FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOA_MaturityDate_From_HEAD, " +
                            "(SELECT LOASuretyBond FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS LOASuretyBond_From_HEAD, " +
                            "(SELECT PullOutServiceDate FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS PullOutServiceDate_From_HEAD, " +
                            "(SELECT GatePassNo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS GatePassNo_From_HEAD, " +
                            "(SELECT LOAPriceValue FROM SRF_MT_LOA WHERE RefId = (SELECT LOANo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS LOAPriceValue_From_HEAD, " +
                            "(SELECT SUM(CONVERT(BIGINT,ActualQuantity)) FROM  SRF_TRANSACTION_Warehouse_Actual_Delivery WHERE SRFNumber = DETAIL.CTRLNo AND DetailsRefId = DETAIL.RefId) AS ActualQuantity2, " +
                            "(SELECT TOP 1 LOA8106 FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS LOA8106, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = (SELECT Requester FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS PezaNonPeza, " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS Warehouse1, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = DETAIL.CTRLNo AND LOA8105 IS NULL) AS LOACount, " +
                            "(SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS Category, " +
                            "(SELECT Description from MT_Category WITH (NOLOCK) WHERE RefId = (SELECT Category FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS CategoryName, " +
                            "'' AS DeliveredDate, STAT.* " +
                            "FROM SRF_TRANSACTION_Request_Details DETAIL WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON DETAIL.CTRLNo = STAT.CTRLNo " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND DETAIL.RefId NOT IN (SELECT DetailsRefId FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK)) " +

                            "AND DETAIL.CTRLNo IN ( " +
                            "SELECT HEAD.CTRLNo " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                            "AND HEAD.CTRLNo IN ('" + ctrlnoList + "') " +
                                                 ") " +

                            "ORDER BY SupplierName, ACTUAL.SRFNumber, DETAILS.ItemName, ACTUAL.RefId ASC  ";


                }

                cmd.CommandText = query;

                reader = cmd.ExecuteReader();

                string previousActualQuantity = "0";
                string previousRefId = string.Empty;

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    string Warehouse1 = reader["Warehouse1"] != DBNull.Value ? reader["Warehouse1"].ToString() : string.Empty;
                    string WarehouseTotalQty = reader["TotalQuantity"] != DBNull.Value ? reader["TotalQuantity"].ToString() : "0";
                    string WarehouseTotalActualQty = reader["ActualQuantity"] != DBNull.Value ? reader["ActualQuantity"].ToString() : "0";
                    string WarehouseTotalActualQty2 = reader["ActualQuantity2"] != DBNull.Value ? reader["ActualQuantity2"].ToString() : "0";
                    //string TotalQty8105 = reader["TotalQty8105"] != DBNull.Value ? reader["TotalQty8105"].ToString() : string.Empty;
                    string LOACount = reader["LOACount"] != DBNull.Value ? reader["LOACount"].ToString() : string.Empty;
                    //string LOADisabled = reader["LOADisabled"] != DBNull.Value ? reader["LOADisabled"].ToString() : string.Empty;
                    string LOADisabled = reader["LOADisabled"] != DBNull.Value ? reader["LOADisabled"].ToString() : "0";

                    entities.RefId = reader["DetailsRefId"].ToString();
                    entities.CategoryDescription = reader["CategoryName"].ToString();
                    entities.Warehouse_CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.Warehouse_LOA8106 = reader["LOA8106"] != DBNull.Value ? reader["LOA8106"].ToString() : string.Empty;
                    entities.Warehouse_8105 = reader["LOA8105"] != DBNull.Value ? reader["LOA8105"].ToString() : string.Empty;
                    entities.Warehouse_LOA8105ProcessDate = reader["LOA8105ProcessDate"] != DBNull.Value ? reader["LOA8105ProcessDate"].ToString() : string.Empty;
                    entities.Warehouse_ItemName = reader["ItemName"] != DBNull.Value ? reader["ItemName"].ToString() : string.Empty;
                    entities.Warehouse_DeliveredDate = reader["DeliveredDate"] != DBNull.Value ? reader["DeliveredDate"].ToString().Replace("1900-01-01", "") : string.Empty;
                    entities.Warehouse_RequesterName = reader["RequesterName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["RequesterName"].ToString(), true).ToUpper() : string.Empty;
                    entities.Warehouse_SupplierName = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : string.Empty;
                    entities.Category = reader["Category"] != DBNull.Value ? reader["Category"].ToString() : string.Empty;
                    entities.Warehouse_TotalQuantity = WarehouseTotalQty;
                    entities.Warehouse_TotalActualQuantity = WarehouseTotalActualQty;

                    //-----------------------------------------------------------------------------------------------------------------------------------
                    if (reader["DetailsRefId"].ToString() == previousRefId)
                    {
                        entities.Warehouse_RemainingQuantity = (int.Parse(WarehouseTotalQty) - (int.Parse(previousActualQuantity) + int.Parse(WarehouseTotalActualQty))).ToString();
                    }
                    else
                    {
                        entities.Warehouse_RemainingQuantity = (int.Parse(WarehouseTotalQty) - int.Parse(WarehouseTotalActualQty)).ToString();
                        previousActualQuantity = "0";
                    }

                    previousRefId = reader["DetailsRefId"].ToString();
                    previousActualQuantity = (int.Parse(previousActualQuantity) + int.Parse(WarehouseTotalActualQty)).ToString();
                    //-----------------------------------------------------------------------------------------------------------------------------------

                    entities.LOA_Number_From_HEAD = reader["LOA_Number_From_HEAD"] != DBNull.Value ? reader["LOA_Number_From_HEAD"].ToString() : string.Empty;
                    entities.LOA_MaturityDate_From_HEAD = reader["LOA_MaturityDate_From_HEAD"] != DBNull.Value ? reader["LOA_MaturityDate_From_HEAD"].ToString() : string.Empty;
                    entities.LOASuretyBond_From_HEAD = reader["LOASuretyBond_From_HEAD"] != DBNull.Value ? reader["LOASuretyBond_From_HEAD"].ToString() : string.Empty;
                    entities.PullOutServiceDate_From_HEAD = reader["PullOutServiceDate_From_HEAD"] != DBNull.Value ? reader["PullOutServiceDate_From_HEAD"].ToString().Replace("12:00:00 AM", "") : string.Empty;
                    entities.LOAPriceValue_From_HEAD = reader["LOAPriceValue_From_HEAD"] != DBNull.Value ? reader["LOAPriceValue_From_HEAD"].ToString() : "0";
                    entities.GatePassNo_From_HEAD = reader["GatePassNo_From_HEAD"] != DBNull.Value ? reader["GatePassNo_From_HEAD"].ToString() : string.Empty;

                    entities.Warehouse_PezaNonPeza = reader["PezaNonPeza"] != DBNull.Value ? reader["PezaNonPeza"].ToString() : string.Empty;

                    entities.ReqInchargeStat = reader["STATReq_Incharge"] != DBNull.Value ? reader["STATReq_Incharge"].ToString() : "0";
                    entities.ReqManagerStat = reader["STATReq_Manager"] != DBNull.Value ? reader["STATReq_Manager"].ToString() : "0";
                    entities.PurInchargeStat = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
                    entities.PurImpexStat = reader["STATPur_Manager"] != DBNull.Value ? reader["STATPur_Manager"].ToString() : "0";


                    if (entities.ReqManagerStat == "0" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PROD.MNGR. APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.INCHARGE APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.IMPEX PROCESSING";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1")
                    {
                        entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && !string.IsNullOrEmpty(Warehouse1))
                    {
                        entities.StatAll = "DELIVERY IN-PROGRESS";
                    }

                    if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurImpexStat == "2")
                    {
                        entities.StatAll = "DISAPPROVED";
                    }
                    if (entities.PurImpexStat == "3")
                    {
                        entities.StatAll = "CANCELED";
                    }


                    if ((WarehouseTotalQty == WarehouseTotalActualQty) && (int.Parse(entities.Warehouse_RemainingQuantity) <= 0))
                    {
                        if (LOADisabled == "0")
                        {
                            entities.StatAll = "CLOSED";

                        }

                    }


                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && (entities.PurImpexStat == "1" || entities.PurImpexStat == "3"))
                    {
                        if (WarehouseTotalActualQty == "0")
                        {
                            entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                        }
                        else
                        {
                            if (int.Parse(entities.Warehouse_RemainingQuantity) > 0)
                            {
                                entities.StatAll = "DELIVERED WITH PENDING ITEMS";
                            }
                            else
                            {
                                entities.StatAll = "CLOSED";
                            }
                        }

                        if (WarehouseTotalQty == WarehouseTotalActualQty)
                        {
                            entities.StatAll = "DELIVERED";
                        }

                    }




                    if (LOADisabled == "0")
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


        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_Reporting_Distinct_SupplierName(string search, string from, string to)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            string query = string.Empty;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                if (string.IsNullOrEmpty(search))
                {
                    query = "SELECT DISTINCT (SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS PezaNonPeza, " +
                            "(SELECT GatePassNo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS GatePassNo_From_HEAD " +
                            "FROM SRF_TRANSACTION_Warehouse_Actual_Delivery ACTUAL WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Request_Details DETAILS WITH (NOLOCK) ON ACTUAL.DetailsRefId = DETAILS.RefId " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON ACTUAL.SRFNumber = STAT.CTRLNo " +
                            "WHERE ACTUAL.SRFNumber IN (SELECT CTRLNo FROM SRF_TRANSACTION_Status WITH (NOLOCK) WHERE STATPur_Manager = 1 AND CONVERT(DATE, DOAPur_Manager) >= '12/19/2022') " +
                            "AND CONVERT(DATE, DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,DOAPur_Manager) <= '" + to + "' " +

                            "AND ACTUAL.SRFNumber IN ( " +
                            "SELECT HEAD.CTRLNo " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                            "AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' " +
                            ") " +

                            "UNION " +

                            "SELECT (SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS PezaNonPeza, " +
                            "(SELECT GatePassNo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS GatePassNo_From_HEAD " +
                            "FROM SRF_TRANSACTION_Request_Details DETAIL WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON DETAIL.CTRLNo = STAT.CTRLNo " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND DETAIL.RefId NOT IN (SELECT DetailsRefId FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK)) " +

                            "AND DETAIL.CTRLNo IN ( " +
                            "SELECT HEAD.CTRLNo " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                            "AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' " +
                            ") ";


                }
                else
                {
                    query = "SELECT DISTINCT (SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber)) AS PezaNonPeza, " +
                            "(SELECT GatePassNo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = ACTUAL.SRFNumber) AS GatePassNo_From_HEAD " +
                            "FROM SRF_TRANSACTION_Warehouse_Actual_Delivery ACTUAL WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Request_Details DETAILS WITH (NOLOCK) ON ACTUAL.DetailsRefId = DETAILS.RefId " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON ACTUAL.SRFNumber = STAT.CTRLNo " +
                            "WHERE ACTUAL.SRFNumber IN (SELECT CTRLNo FROM SRF_TRANSACTION_Status WITH (NOLOCK) WHERE STATPur_Manager = 1 AND CONVERT(DATE, DOAPur_Manager) >= '12/19/2022') " +
                            "AND CONVERT(DATE, DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,DOAPur_Manager) <= '" + to + "' " +
                            "AND (ACTUAL.SRFNumber LIKE '%" + search + "%' OR LOA8106 LIKE '%" + search + "%')" +

                            "AND ACTUAL.SRFNumber IN ( " +
                            "SELECT HEAD.CTRLNo " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                            "AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' " +
                            ") " +

                            "UNION " +

                            "SELECT (SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = (SELECT Supplier FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo)) AS PezaNonPeza, " +
                            "(SELECT GatePassNo FROM SRF_TRANSACTION_Request WITH (NOLOCK) WHERE CTRLNo = DETAIL.CTRLNo) AS GatePassNo_From_HEAD " +
                            "FROM SRF_TRANSACTION_Request_Details DETAIL WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON DETAIL.CTRLNo = STAT.CTRLNo " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND DETAIL.RefId NOT IN (SELECT DetailsRefId FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK)) " +

                            "AND DETAIL.CTRLNo IN ( " +
                            "SELECT HEAD.CTRLNo " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.GatePassNo = '' OR HEAD.GatePassNo IS NULL) " +
                            "AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' " +
                            ") ";

                }

                cmd.CommandText = query;

                reader = cmd.ExecuteReader();

                string previousActualQuantity = "0";
                string previousRefId = string.Empty;

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.Warehouse_SupplierName = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : string.Empty;
                    entities.GatePassNo_From_HEAD = reader["GatePassNo_From_HEAD"] != DBNull.Value ? reader["GatePassNo_From_HEAD"].ToString() : string.Empty;
                    entities.Warehouse_PezaNonPeza = reader["PezaNonPeza"] != DBNull.Value ? reader["PezaNonPeza"].ToString() : string.Empty;

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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_8105_Entry2(string search)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            string query = string.Empty;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                // SET THE DATE WHEN THE IMPLEMENTATION START (CONVERT(DATE, STAT.DOAPur_Manager) >= '10/8/2022')
                if (string.IsNullOrEmpty(search))
                {
                    query = "SELECT TOP 500 HEAD.RefId, HEAD.CTRLNo, HEAD.LOA8106, HEAD.Category, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = HEAD.Requester) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS PezaNonPeza, " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS Warehouse1, " +
                            "(SELECT Description FROM MT_Category WITH (NOLOCK) WHERE RefId = HEAD.Category) AS CategoryDescription, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(TotalQuantity) = 1 THEN CAST(TotalQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Request_Details WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS WarehouseTotalQty, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(ActualQuantity) = 1 THEN CAST(ActualQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS WarehouseTotalActualQty, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(Quantity) = 1 THEN Quantity ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_8105 WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS TotalQty8105, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND LOA8105 IS NULL AND WithDocuments = '1') AS LOACount, " +
                            "STAT.* " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' ";
                            //"AND HEAD.Category NOT IN ('" + ConfigurationManager.AppSettings["SRF_PO_FIXED_CATEGORY"].ToString() + "')";
                }
                else
                {
                    query = "SELECT TOP 500 HEAD.RefId, HEAD.CTRLNo, HEAD.LOA8106, HEAD.Category, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = HEAD.Requester) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS PezaNonPeza, " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS Warehouse1, " +
                            "(SELECT Description FROM MT_Category WITH (NOLOCK) WHERE RefId = HEAD.Category) AS CategoryDescription, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(TotalQuantity) = 1 THEN CAST(TotalQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Request_Details WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS WarehouseTotalQty, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(ActualQuantity) = 1 THEN CAST(ActualQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS WarehouseTotalActualQty, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(Quantity) = 1 THEN Quantity ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_8105 WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS TotalQty8105, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND LOA8105 IS NULL AND WithDocuments = '1') AS LOACount, " +
                            "STAT.* " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' " +
                            "AND (HEAD.CTRLNo LIKE '%" + search + "%' OR HEAD.LOA8106 LIKE '%" + search + "%') ";
                            //"AND HEAD.Category NOT IN ('" + ConfigurationManager.AppSettings["SRF_PO_FIXED_CATEGORY"].ToString() + "')";
                }
                

                cmd.CommandText = query;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    string Warehouse1 = reader["Warehouse1"] != DBNull.Value ? reader["Warehouse1"].ToString() : string.Empty;
                    string WarehouseTotalQty = reader["WarehouseTotalQty"] != DBNull.Value ? reader["WarehouseTotalQty"].ToString() : string.Empty;
                    string WarehouseTotalActualQty = reader["WarehouseTotalActualQty"] != DBNull.Value ? reader["WarehouseTotalActualQty"].ToString() : string.Empty;
                    //string TotalQty8105 = reader["TotalQty8105"] != DBNull.Value ? reader["TotalQty8105"].ToString() : string.Empty;
                    string LOACount = reader["LOACount"] != DBNull.Value ? reader["LOACount"].ToString() : string.Empty;

                    entities.RefId = reader["RefId"].ToString();
                    entities.Warehouse_CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.Warehouse_LOA8106 = reader["LOA8106"] != DBNull.Value ? reader["LOA8106"].ToString() : string.Empty;
                    entities.Warehouse_PezaNonPeza = reader["PezaNonPeza"] != DBNull.Value ? reader["PezaNonPeza"].ToString() : string.Empty;
                    //entities.Warehouse_8105 = reader["LOA8105"] != DBNull.Value ? reader["LOA8105"].ToString() : string.Empty;
                    //entities.Warehouse_ItemName = reader["ItemName"].ToString();
                    //entities.Warehouse_DeliveredDate = reader["DeliveredDate"] != DBNull.Value ? reader["DeliveredDate"].ToString() : string.Empty;
                    entities.Warehouse_RequesterName = CryptorEngine.Decrypt(reader["RequesterName"].ToString(), true).ToUpper();
                    entities.Warehouse_SupplierName = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : string.Empty;
                    entities.Category = reader["Category"].ToString();
                    entities.CategoryDescription = reader["CategoryDescription"].ToString();
                    entities.Warehouse_TotalQuantity = WarehouseTotalQty;
                    entities.Warehouse_TotalActualQuantity = WarehouseTotalActualQty;

                    int orderedQty = int.Parse(entities.Warehouse_TotalQuantity);
                    int actualQty = int.Parse(!string.IsNullOrEmpty(entities.Warehouse_TotalActualQuantity) ? entities.Warehouse_TotalActualQuantity : "0");
                    int remainingQty = orderedQty - actualQty;


                    entities.Warehouse_RemainingQuantity = remainingQty.ToString();


                    entities.ReqInchargeStat = reader["STATReq_Incharge"] != DBNull.Value ? reader["STATReq_Incharge"].ToString() : "0";
                    entities.ReqManagerStat = reader["STATReq_Manager"] != DBNull.Value ? reader["STATReq_Manager"].ToString() : "0";
                    entities.PurInchargeStat = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
                    entities.PurImpexStat = reader["STATPur_Manager"] != DBNull.Value ? reader["STATPur_Manager"].ToString() : "0";

                    if (entities.ReqManagerStat == "0" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PROD.MNGR. APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.INCHARGE APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.IMPEX PROCESSING";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1")
                    {
                        entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && !string.IsNullOrEmpty(Warehouse1))
                    {
                        entities.StatAll = "DELIVERY IN-PROGRESS";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && (!string.IsNullOrEmpty(WarehouseTotalQty) && !string.IsNullOrEmpty(WarehouseTotalActualQty)))
                    {
                        if (WarehouseTotalQty == WarehouseTotalActualQty)
                        {
                            entities.StatAll = "DELIVERED";
                        }
                        else
                        {
                            entities.StatAll = "DELIVERED WITH PENDING ITEMS";
                        }
                    }
                    if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurImpexStat == "2")
                    {
                        entities.StatAll = "DISAPPROVED";
                    }
                    if (entities.PurImpexStat == "3")
                    {
                        entities.StatAll = "CANCELED";
                    }


                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && (!string.IsNullOrEmpty(WarehouseTotalQty) && !string.IsNullOrEmpty(WarehouseTotalActualQty)))
                    {
                        if (int.Parse(LOACount) > 0)
                        {
                            list.Add(entities);
                        }


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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_8105_Entry2WithDateRange(string search, string from, string to, string isChecked)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            string query = string.Empty;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                // SET THE DATE WHEN THE IMPLEMENTATION START (CONVERT(DATE, STAT.DOAPur_Manager) >= '10/8/2022')
                if (string.IsNullOrEmpty(search))
                {
                    query = "SELECT HEAD.RefId, HEAD.CTRLNo, HEAD.LOA8106, HEAD.Category, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = HEAD.Requester) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS PezaNonPeza, " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS Warehouse1, " +
                            "(SELECT Description FROM MT_Category WITH (NOLOCK) WHERE RefId = HEAD.Category) AS CategoryDescription, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(TotalQuantity) = 1 THEN CAST(TotalQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Request_Details WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS WarehouseTotalQty, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(ActualQuantity) = 1 THEN CAST(ActualQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS WarehouseTotalActualQty, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(Quantity) = 1 THEN Quantity ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_8105 WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS TotalQty8105, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND LOA8105 IS NULL AND WithDocuments = '1') AS LOACount, " +
                            "STAT.* " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' AND HEAD.Document8105 = '0' " +
                            "AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' ";
                    //"AND HEAD.Category NOT IN ('" + ConfigurationManager.AppSettings["SRF_PO_FIXED_CATEGORY"].ToString() + "')";
                }
                else
                {
                    query = "SELECT HEAD.RefId, HEAD.CTRLNo, HEAD.LOA8106, HEAD.Category, " +
                            "(SELECT Fullname FROM Login_Credentials LC WITH (NOLOCK) WHERE RefId = HEAD.Requester) AS RequesterName, " +
                            "(SELECT Name FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
                            "(SELECT Peza FROM MT_Supplier_Head SUPPLIER WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS PezaNonPeza, " +
                            "(SELECT TOP 1 CTRLNo FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS Warehouse1, " +
                            "(SELECT Description FROM MT_Category WITH (NOLOCK) WHERE RefId = HEAD.Category) AS CategoryDescription, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(TotalQuantity) = 1 THEN CAST(TotalQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Request_Details WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS WarehouseTotalQty, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(ActualQuantity) = 1 THEN CAST(ActualQuantity AS MONEY) ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo) AS WarehouseTotalActualQty, " +
                            "(SELECT SUM(CONVERT(BIGINT, CAST(CASE WHEN ISNUMERIC(Quantity) = 1 THEN Quantity ELSE 0 END AS BIGINT))) FROM SRF_TRANSACTION_8105 WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS TotalQty8105, " +
                            "(SELECT COUNT(SRFNumber) FROM SRF_TRANSACTION_Warehouse_Actual_Delivery WITH (NOLOCK) WHERE SRFNumber = HEAD.CTRLNo AND LOA8105 IS NULL AND WithDocuments = '1') AS LOACount, " +
                            "STAT.* " +
                            "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                            "WHERE STAT.STATPur_Manager = 1 AND CONVERT(DATE, STAT.DOAPur_Manager) >= '12/19/2022' AND HEAD.Document8105 = '0' " +
                            "AND (HEAD.CTRLNo LIKE '%" + search + "%' OR HEAD.LOA8106 LIKE '%" + search + "%') " +
                            "AND CONVERT(DATE, STAT.DOAPur_Manager) >= '" + from + "' AND CONVERT(DATE ,STAT.DOAPur_Manager) <= '" + to + "' ";
                    //"AND HEAD.Category NOT IN ('" + ConfigurationManager.AppSettings["SRF_PO_FIXED_CATEGORY"].ToString() + "')";
                }


                cmd.CommandText = query;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    string Warehouse1 = reader["Warehouse1"] != DBNull.Value ? reader["Warehouse1"].ToString() : string.Empty;
                    string WarehouseTotalQty = reader["WarehouseTotalQty"] != DBNull.Value ? reader["WarehouseTotalQty"].ToString() : string.Empty;
                    string WarehouseTotalActualQty = reader["WarehouseTotalActualQty"] != DBNull.Value ? reader["WarehouseTotalActualQty"].ToString() : string.Empty;
                    //string TotalQty8105 = reader["TotalQty8105"] != DBNull.Value ? reader["TotalQty8105"].ToString() : string.Empty;
                    string LOACount = reader["LOACount"] != DBNull.Value ? reader["LOACount"].ToString() : string.Empty;

                    entities.RefId = reader["RefId"].ToString();
                    entities.Warehouse_CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                    entities.Warehouse_LOA8106 = reader["LOA8106"] != DBNull.Value ? reader["LOA8106"].ToString() : string.Empty;
                    entities.Warehouse_PezaNonPeza = reader["PezaNonPeza"] != DBNull.Value ? reader["PezaNonPeza"].ToString() : string.Empty;
                    //entities.Warehouse_8105 = reader["LOA8105"] != DBNull.Value ? reader["LOA8105"].ToString() : string.Empty;
                    //entities.Warehouse_ItemName = reader["ItemName"].ToString();
                    //entities.Warehouse_DeliveredDate = reader["DeliveredDate"] != DBNull.Value ? reader["DeliveredDate"].ToString() : string.Empty;
                    entities.Warehouse_RequesterName = CryptorEngine.Decrypt(reader["RequesterName"].ToString(), true).ToUpper();
                    entities.Warehouse_SupplierName = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : string.Empty;
                    entities.Category = reader["Category"].ToString();
                    entities.CategoryDescription = reader["CategoryDescription"].ToString();
                    entities.Warehouse_TotalQuantity = WarehouseTotalQty;
                    entities.Warehouse_TotalActualQuantity = WarehouseTotalActualQty;

                    int orderedQty = int.Parse(entities.Warehouse_TotalQuantity);
                    int actualQty = int.Parse(!string.IsNullOrEmpty(entities.Warehouse_TotalActualQuantity) ? entities.Warehouse_TotalActualQuantity : "0");
                    int remainingQty = orderedQty - actualQty;


                    entities.Warehouse_RemainingQuantity = remainingQty.ToString();


                    entities.ReqInchargeStat = reader["STATReq_Incharge"] != DBNull.Value ? reader["STATReq_Incharge"].ToString() : "0";
                    entities.ReqManagerStat = reader["STATReq_Manager"] != DBNull.Value ? reader["STATReq_Manager"].ToString() : "0";
                    entities.PurInchargeStat = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
                    entities.PurImpexStat = reader["STATPur_Manager"] != DBNull.Value ? reader["STATPur_Manager"].ToString() : "0";

                    if (entities.ReqManagerStat == "0" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PROD.MNGR. APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.INCHARGE APPROVAL";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "FOR PUR.IMPEX PROCESSING";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1")
                    {
                        entities.StatAll = "APPROVED / WAITING FOR DELIVERY";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && !string.IsNullOrEmpty(Warehouse1))
                    {
                        entities.StatAll = "DELIVERY IN-PROGRESS";
                    }
                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && (!string.IsNullOrEmpty(WarehouseTotalQty) && !string.IsNullOrEmpty(WarehouseTotalActualQty)))
                    {
                        if (WarehouseTotalQty == WarehouseTotalActualQty)
                        {
                            entities.StatAll = "DELIVERED";
                        }
                        else
                        {
                            entities.StatAll = "DELIVERED WITH PENDING ITEMS";
                        }
                    }
                    if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurImpexStat == "2")
                    {
                        entities.StatAll = "DISAPPROVED";
                    }
                    if (entities.PurImpexStat == "3")
                    {
                        entities.StatAll = "CANCELED";
                    }

                    if (WarehouseTotalQty == WarehouseTotalActualQty && int.Parse(LOACount) <= 0)
                    {
                        entities.StatAll = "CLOSED";
                    }


                    if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurImpexStat == "1" && (!string.IsNullOrEmpty(WarehouseTotalQty) && !string.IsNullOrEmpty(WarehouseTotalActualQty)))
                    {
                        if (int.Parse(LOACount) > 0)
                        {
                            if (WarehouseTotalQty == WarehouseTotalActualQty)
                            {
                                //DO NOTHING
                            }
                            else
                            {
                                list.Add(entities);
                            }
                        }

                        if (isChecked.ToLower() == "yes")
                        {
                            if (WarehouseTotalQty == WarehouseTotalActualQty)
                            {
                                list.Add(entities);
                            }
                        }


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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Warehouse_GetAttachment(string ctrlno)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;
            string query = string.Empty;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                query = "SELECT * FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo ='" + ctrlno + "' AND TransType = '2'";

                cmd.CommandText = query;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.Warehouse_Attachment = reader["Attachment"] != DBNull.Value ? reader["Attachment"].ToString() : string.Empty;

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

        public int SRF_TRANSACTION_SQLTransaction(string query)
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


        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Reporting_ByDateRange(Entities_SRF_RequestEntry entity)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_TRANSACTION_Reporting_ByDateRange";
                cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
                cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
                cmd.CommandTimeout = 500;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

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


        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Reporting_ByDateRange_ByDivision(Entities_SRF_RequestEntry entity)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_TRANSACTION_Reporting_ByDateRange_ByDivision";
                cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
                cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
                cmd.CommandTimeout = 500;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Reporting_ByDateRange_ByAll(Entities_SRF_RequestEntry entity)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SRF_TRANSACTION_Reporting_ByDateRange_ByAll";
                cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
                cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
                cmd.CommandTimeout = 500;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_Reporting_ByDateRange_Details(Entities_SRF_RequestEntry entity)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT DETAILS.CTRLNo, HEAD.Category, CAT.Description AS CategoryName, DETAILS.RefPRPO, DETAILS.SalesInvoice, DETAILS.BrandMachineName, HEAD.TransactionDate, " +
                                    "DETAILS.ItemName, DETAILS.ItemSpecification, STAT.*, LC.FullName " +
                                    "FROM SRF_TRANSACTION_Request_Details DETAILS WITH (NOLOCK) " +
                                    "LEFT JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON DETAILS.CTRLNo = STAT.CTRLNo " +
                                    "LEFT JOIN SRF_TRANSACTION_Request HEAD WITH (NOLOCK) ON DETAILS.CTRLNo = HEAD.CTRLNo " +
                                    "LEFT JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                                    "LEFT JOIN Login_Credentials LC ON HEAD.Requester = LC.RefId " +
                                    "WHERE CONVERT(DATE, HEAD.TransactionDate) >= '" + entity.DrFrom + "' AND CONVERT(DATE, HEAD.TransactionDate) <= '" + entity.DrTo + "' " +
                                    "ORDER BY HEAD.RefId DESC";
                cmd.CommandTimeout = 500;


                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.CtrlNo = reader["CTRLNo"].ToString();
                    entities.TransactionDate = reader["TransactionDate"].ToString();
                    entities.Category = reader["CategoryName"].ToString();
                    entities.RefPRPO =  reader["RefPRPO"] != DBNull.Value ? reader["RefPRPO"].ToString() : string.Empty;
                    entities.SalesInvoice = reader["SalesInvoice"] != DBNull.Value ? reader["SalesInvoice"].ToString() : string.Empty;
                    entities.BrandMachineName = reader["BrandMachineName"] != DBNull.Value ? reader["BrandMachineName"].ToString() : string.Empty;
                    entities.ItemName = reader["ItemName"] != DBNull.Value ? reader["ItemName"].ToString() : string.Empty;
                    entities.Specification = reader["ItemSpecification"] != DBNull.Value ? reader["ItemSpecification"].ToString() : string.Empty;
                    entities.Requester = CryptorEngine.Decrypt(reader["FullName"].ToString().Replace(" ", "+"), true);
                    entities.ReqInchargeDOA = reader["DOAReq_Incharge"].ToString();

                    entities.PurInchargeStat = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString() : "0";
                    entities.PurImpexStat = reader["STATPur_Manager"] != DBNull.Value ? reader["STATPur_Manager"].ToString() : "0";


                    if (entities.PurImpexStat == "1")
                    {
                        entities.StatAll = "APPROVED";
                    }
                    if (string.IsNullOrEmpty(entities.PurImpexStat) || entities.PurImpexStat == "0")
                    {
                        entities.StatAll = "PENDING APPROVAL";
                    }
                    if (entities.PurImpexStat == "2" || entities.PurInchargeStat == "2")
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

        public List<Entities_SRF_RequestEntry> SRF_TRANSACTION_IsPullOutRequestExistingInSRF(Entities_SRF_RequestEntry entity)
        {
            List<Entities_SRF_RequestEntry> list = new List<Entities_SRF_RequestEntry>();

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();
            DbDataReader reader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT TOP 1 HEAD.CTRLNo, HEAD.ProblemEncountered, HEAD.TransactionDate, HEAD.TotalQuantity, HEAD.Remarks " +
                                    "FROM SRF_TRANSACTION_Request HEAD WITH (NOLOCK) " +
                                    "INNER JOIN SRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                                    "WHERE (STAT.STATPur_Incharge IS NULL OR STAT.STATPur_Incharge = 0) " +
                                    "AND STAT.STATReq_Manager = 1 " +
                                    "AND HEAD.ProblemEncountered = '" + entity.ProblemEncountered + "' " +
                                    "AND CONVERT(DATE, HEAD.TransactionDate) = CONVERT(VARCHAR, GETDATE(), 1) " +
                                    "ORDER BY HEAD.TransactionDate DESC";


                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entities_SRF_RequestEntry entities = new Entities_SRF_RequestEntry();

                    entities.CtrlNo = reader["CTRLNo"].ToString();
                    entities.TotalQuantity = reader["TotalQuantity"].ToString();
                    entities.Remarks = reader["Remarks"].ToString();
                    entities.ProblemEncountered = reader["ProblemEncountered"].ToString();

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


        public Int32 SRF_TRANSACTION_WAREHOUSE_CheckIfMoreThanOneDelivery(string ctrlno)
        {
            Int32 result = 0;

            DbConnection conn = Factory.CreateConnection();
            DbCommand cmd = Factory.CreateCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT COUNT(CTRLNo) FROM SRF_TRANSACTION_Warehouse WITH (NOLOCK) WHERE CTRLNo = '" + ctrlno + "'";

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



        #endregion

    }

