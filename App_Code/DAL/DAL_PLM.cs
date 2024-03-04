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

public class DAL_PLM
{
    public DAL_PLM()
    {
    }


#region PERMIT CERTIFICATES

public List<Entities_PLM_PermitCertificates> PLM_MT_PermitCertificates_GetAll()
{
    List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();
    DbDataReader reader = null;

    try
    {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PLM_MT_PermitCertificates_GetAll";

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Entities_PLM_PermitCertificates entities = new Entities_PLM_PermitCertificates();

            entities.RefId = reader["RefId"].ToString();
            entities.PermitName = reader["PermitName"].ToString();
            entities.ChemicalContent = reader["ChemicalContent"].ToString();
            entities.ItemName = reader["ItemName"].ToString();
            entities.Specification = reader["Specification"].ToString();
            entities.GovernmentAgency = reader["GA_Name"].ToString();
            entities.Frequency = reader["Frequency"].ToString();
            entities.SpecifiedRequirements = reader["SpecifiedRequirements"].ToString();
            entities.LeadTime = reader["LeadTime"].ToString();
            entities.Safety = reader["Safety"].ToString();
            entities.ProcessorName = reader["ProcessorName"].ToString();
            entities.Supplier = reader["Supplier"].ToString();
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

public List<Entities_PLM_PermitCertificates> PLM_MT_PermitCertificates_GetByRefId(string refId)
{
    List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();
    DbDataReader reader = null;

    try
    {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PLM_MT_PermitCertificates_GetByRefId";
        cmd.Parameters.Add(Factory.CreateParameter("@refid", refId = string.IsNullOrEmpty(refId) ? "0" : refId));

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Entities_PLM_PermitCertificates entities = new Entities_PLM_PermitCertificates();

            entities.RefId = reader["RefId"].ToString();
            entities.PermitName = reader["PermitName"].ToString();
            entities.ChemicalContent = reader["ChemicalContent"].ToString();
            entities.ItemName = reader["ItemName"].ToString();
            entities.Specification = reader["Specification"].ToString();
            entities.GovernmentAgency = reader["GovernmentAgency"].ToString();
            entities.Frequency = reader["Frequency"].ToString();
            entities.SpecifiedRequirements = reader["SpecifiedRequirements"].ToString();
            entities.LeadTime = reader["LeadTime"].ToString();
            entities.Safety = reader["Safety"].ToString();
            entities.ProcessorName = reader["ProcessorName"].ToString();
            entities.Supplier = reader["Supplier"].ToString();
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

public int PLM_MT_PermitCertificates_Insert(Entities_PLM_PermitCertificates entity)
{
    int result = 0;

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();

    cmd.CommandType = CommandType.StoredProcedure;
    cmd.CommandText = "PLM_MT_PermitCertificates_Insert";

    cmd.Parameters.Add(Factory.CreateParameter("@permitname", entity.PermitName));
    cmd.Parameters.Add(Factory.CreateParameter("@chemicalcontent", entity.ChemicalContent));
    cmd.Parameters.Add(Factory.CreateParameter("@itemname", entity.ItemName));
    cmd.Parameters.Add(Factory.CreateParameter("@specification", entity.Specification));
    cmd.Parameters.Add(Factory.CreateParameter("@governmentagency", entity.GovernmentAgency));
    cmd.Parameters.Add(Factory.CreateParameter("@frequency", entity.Frequency));
    cmd.Parameters.Add(Factory.CreateParameter("@specifiedrequirements", entity.SpecifiedRequirements));
    cmd.Parameters.Add(Factory.CreateParameter("@leadtime", entity.LeadTime));
    //cmd.Parameters.Add(Factory.CreateParameter("@safety", entity.Safety));
    //cmd.Parameters.Add(Factory.CreateParameter("@processorname", entity.ProcessorName));
    //cmd.Parameters.Add(Factory.CreateParameter("@supplier", entity.Supplier));
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

public int PLM_MT_PermitCertificates_Append(Entities_PLM_PermitCertificates entity)
{
    int result = 0;

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();

    cmd.CommandType = CommandType.StoredProcedure;
    cmd.CommandText = "PLM_MT_PermitCertificates_Append";

    cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
    cmd.Parameters.Add(Factory.CreateParameter("@permitname", entity.PermitName));
    cmd.Parameters.Add(Factory.CreateParameter("@chemicalcontent", entity.ChemicalContent));
    cmd.Parameters.Add(Factory.CreateParameter("@itemname", entity.ItemName));
    cmd.Parameters.Add(Factory.CreateParameter("@specification", entity.Specification));
    cmd.Parameters.Add(Factory.CreateParameter("@governmentagency", entity.GovernmentAgency));
    cmd.Parameters.Add(Factory.CreateParameter("@frequency", entity.Frequency));
    cmd.Parameters.Add(Factory.CreateParameter("@specifiedrequirements", entity.SpecifiedRequirements));
    cmd.Parameters.Add(Factory.CreateParameter("@leadtime", entity.LeadTime));
    //cmd.Parameters.Add(Factory.CreateParameter("@safety", entity.Safety));
    //cmd.Parameters.Add(Factory.CreateParameter("@processorname", entity.ProcessorName));
    //cmd.Parameters.Add(Factory.CreateParameter("@supplier", entity.Supplier));
    cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.AddedBy));

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

public int PLM_MT_PermitCertificates_DisableByRefId(Entities_PLM_PermitCertificates entity)
{
    int result = 0;

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();

    cmd.CommandType = CommandType.StoredProcedure;
    cmd.CommandText = "PLM_MT_PermitCertificates_DisableByRefId";

    cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
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

#endregion

#region Supplier

public List<Entities_PLM_PermitCertificates> PLM_MT_Supplier_GetAll()
{
    List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();
    DbDataReader reader = null;

    try
    {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PLM_MT_Supplier_GetAll";

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Entities_PLM_PermitCertificates entities = new Entities_PLM_PermitCertificates();

            entities.RefId = reader["RefId"].ToString();
            entities.SupplierCode = reader["Code"].ToString();
            entities.SupplierName = reader["Description"].ToString();
            entities.SupplierEmailAddress = reader["EmailAddress"].ToString();
            entities.AddedBy = reader["AddedBy"].ToString();
            entities.TakeAction = (!string.IsNullOrEmpty(reader["TakeAction"].ToString()) && reader["TakeAction"].ToString() == "1") ? "true" : "false";
            entities.Expired = (!string.IsNullOrEmpty(reader["Expired"].ToString()) && reader["Expired"].ToString() == "1") ? "true" : "false";
            entities.Renewed = (!string.IsNullOrEmpty(reader["Renewed"].ToString()) && reader["Renewed"].ToString() == "1") ? "true" : "false";
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

public int PLM_MT_Supplier_IsDisabled(Entities_PLM_PermitCertificates entity)
{
    int result = 0;

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();

    cmd.CommandType = CommandType.StoredProcedure;
    cmd.CommandText = "PLM_MT_Supplier_IsDisabled";

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

public int PLM_MT_Supplier_Append(Entities_PLM_PermitCertificates entity)
{
    int result = 0;

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();

    cmd.CommandType = CommandType.StoredProcedure;
    cmd.CommandText = "PLM_MT_Supplier_Append";

    cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
    cmd.Parameters.Add(Factory.CreateParameter("@code", entity.SupplierCode));
    cmd.Parameters.Add(Factory.CreateParameter("@description", entity.SupplierName));
    cmd.Parameters.Add(Factory.CreateParameter("@email", entity.SupplierEmailAddress));
    cmd.Parameters.Add(Factory.CreateParameter("@takeaction", entity.TakeAction));
    cmd.Parameters.Add(Factory.CreateParameter("@expired", entity.Expired));
    cmd.Parameters.Add(Factory.CreateParameter("@renewed", entity.Renewed));
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

public int PLM_MT_Supplier_Insert(Entities_PLM_PermitCertificates entity)
{
    int result = 0;

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();

    cmd.CommandType = CommandType.StoredProcedure;
    cmd.CommandText = "PLM_MT_Supplier_Insert";

    cmd.Parameters.Add(Factory.CreateParameter("@code", entity.SupplierCode));
    cmd.Parameters.Add(Factory.CreateParameter("@description", entity.SupplierName));
    cmd.Parameters.Add(Factory.CreateParameter("@email", entity.SupplierEmailAddress));
    cmd.Parameters.Add(Factory.CreateParameter("@takeaction", entity.TakeAction));
    cmd.Parameters.Add(Factory.CreateParameter("@expired", entity.Expired));
    cmd.Parameters.Add(Factory.CreateParameter("@renewed", entity.Renewed));
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

public List<Entities_PLM_PermitCertificates> PLM_MT_Supplier_GetBySupplierName(string SupplierName)
{
    List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();
    DbDataReader reader = null;

    try
    {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PLM_MT_Supplier_GetBySupplierName";

        cmd.Parameters.Add(Factory.CreateParameter("@SupplierName", SupplierName));

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Entities_PLM_PermitCertificates entities = new Entities_PLM_PermitCertificates();

            entities.RefId = reader["RefId"].ToString();
            entities.SupplierCode = reader["code"].ToString();
            entities.SupplierName = reader["description"].ToString();
            entities.SupplierEmailAddress = reader["EmailAddress"].ToString();
            entities.TakeAction = (!string.IsNullOrEmpty(reader["TakeAction"].ToString()) || reader["TakeAction"].ToString() == "1") ? "true" : "false";
            entities.Expired = (!string.IsNullOrEmpty(reader["Expired"].ToString()) || reader["Expired"].ToString() == "1") ? "true" : "false";
            entities.Renewed = (!string.IsNullOrEmpty(reader["Renewed"].ToString()) || reader["Renewed"].ToString() == "1") ? "true" : "false";
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

public List<Entities_PLM_PermitCertificates> PLM_MT_Supplier_GetBySupplierName_Like(string SupplierName)
{
    List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();
    DbDataReader reader = null;

    try
    {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PLM_MT_Supplier_GetBySupplierName_Like";

        cmd.Parameters.Add(Factory.CreateParameter("@SupplierName", SupplierName));

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Entities_PLM_PermitCertificates entities = new Entities_PLM_PermitCertificates();

            entities.RefId = reader["RefId"].ToString();
            entities.SupplierCode = reader["SupplierCode"].ToString();
            entities.SupplierName = reader["SupplierName"].ToString();
            entities.SupplierEmailAddress = reader["EmailAddress"].ToString();
            entities.TakeAction = (!string.IsNullOrEmpty(reader["TakeAction"].ToString()) || reader["TakeAction"].ToString() == "1") ? "true" : "false";
            entities.Expired = (!string.IsNullOrEmpty(reader["Expired"].ToString()) || reader["Expired"].ToString() == "1") ? "true" : "false";
            entities.Renewed = (!string.IsNullOrEmpty(reader["Renewed"].ToString()) || reader["Renewed"].ToString() == "1") ? "true" : "false";
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

#region Transactions

public List<Entities_PLM_PermitCertificates> PLM_TRANSACTION_Request_GetAll()
{
    List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();
    DbDataReader reader = null;

    try
    {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PLM_TRANSACTION_Request_GetAll";

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Entities_PLM_PermitCertificates entities = new Entities_PLM_PermitCertificates();

            entities.RefId = reader["RefId"].ToString();
            entities.PermitName = reader["PermitName"].ToString();
            entities.PermitId = reader["PermitId"].ToString();
            entities.ChemicalContent = reader["ChemicalContent"].ToString();
            entities.ItemName = reader["ItemName"].ToString();
            entities.Specification = reader["Specification"].ToString();
            entities.GovernmentAgency = reader["GovernmentAgency"].ToString();
            entities.Frequency = reader["Frequency"].ToString();
            entities.SpecifiedRequirements = reader["SpecifiedRequirements"].ToString();
            entities.LeadTime = reader["LeadTime"].ToString();
            entities.Safety = reader["Safety"].ToString();
            entities.ProcessorName = reader["ProcessorName"].ToString();
            entities.Supplier = reader["Supplier"].ToString();
            //entities.SupplierName = reader["SupplierName"].ToString();
            //entities.Status = reader["Status"].ToString();
            entities.IssuedDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(reader["IssuedDate"].ToString()));
            entities.ExpirationDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(reader["ExpirationDate"].ToString()));
            //entities.IssuedDate = reader["IssuedDate"].ToString();
            //entities.ExpirationDate = reader["ExpirationDate"].ToString();
            entities.AddedBy = reader["AddedBy"].ToString();
            entities.IsDisabled = reader["IsDisabled"].ToString();
            entities.Attachment1 = reader["Attachment1"].ToString();
            entities.Attachment2 = reader["Attachment2"].ToString();
            entities.Attachment3 = reader["Attachment3"].ToString();

            Common COMMON = new Common();
            string dDate2 = reader["ExpirationDate"].ToString();
            DateTime dSecond = DateTime.Parse(dDate2);
            double daysLeft = COMMON.GetBusinessDays(DateTime.Now, dSecond);
            int daysLeftInt = (int)daysLeft;
            int leadTime = int.Parse(reader["LeadTime"].ToString());

            
            if (!string.IsNullOrEmpty(reader["HistoryExpirationDate"].ToString()))
            {
                entities.Status = "RENEWED";
                entities.ColorCode = "Blue";
            }
            else
            {
                if (daysLeftInt <= leadTime)
                {
                    entities.Status = "TAKE ACTION";
                    entities.ColorCode = "Green";
                }
                else
                {
                    entities.Status = "NEW";
                    entities.ColorCode = "Black";
                }
            }

            if (daysLeft <= 0)
            {
                entities.Status = "EXPIRED";
                entities.ColorCode = "Maroon";
            }



            entities.DaysLeft = daysLeftInt.ToString();

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

public List<Entities_PLM_PermitCertificates> PLM_TRANSACTION_Request_GetByRefid(string refid)
{
    List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();
    DbDataReader reader = null;

    try
    {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PLM_TRANSACTION_Request_GetByRefid";
        cmd.Parameters.Add(Factory.CreateParameter("@refid", refid));

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Entities_PLM_PermitCertificates entities = new Entities_PLM_PermitCertificates();

            entities.RefId = reader["RefId"].ToString();
            entities.PermitName = reader["PermitName"].ToString();
            entities.PermitId = reader["PermitId"].ToString();
            entities.ChemicalContent = reader["ChemicalContent"].ToString();
            entities.ItemName = reader["ItemName"].ToString();
            entities.Specification = reader["Specification"].ToString();
            entities.GovernmentAgency = reader["GovernmentAgency"].ToString();
            entities.Frequency = reader["Frequency"].ToString();
            entities.SpecifiedRequirements = reader["SpecifiedRequirements"].ToString();
            entities.LeadTime = reader["LeadTime"].ToString();
            entities.Safety = reader["Safety"].ToString();
            entities.ProcessorName = reader["ProcessorName"].ToString();
            entities.Supplier = reader["Supplier"].ToString();
            //entities.SupplierName = reader["SupplierName"].ToString();
            //entities.Status = reader["Status"].ToString();
            entities.IssuedDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(reader["IssuedDate"].ToString()));
            entities.ExpirationDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(reader["ExpirationDate"].ToString()));
            entities.AddedBy = reader["AddedBy"].ToString();
            entities.IsDisabled = reader["IsDisabled"].ToString();
            entities.Attachment1 = reader["Attachment1"].ToString();
            entities.Attachment2 = reader["Attachment2"].ToString();
            entities.Attachment3 = reader["Attachment3"].ToString();

            Common COMMON = new Common();
            string dDate2 = reader["ExpirationDate"].ToString();
            DateTime dSecond = DateTime.Parse(dDate2);
            double daysLeft = COMMON.GetBusinessDays(DateTime.Now, dSecond);
            int daysLeftInt = (int)daysLeft;
            int leadTime = int.Parse(reader["LeadTime"].ToString());

            if (!string.IsNullOrEmpty(reader["HistoryExpirationDate"].ToString()))
            {
                entities.Status = "RENEWED";
                entities.ColorCode = "Blue";
            }
            else
            {
                if (daysLeftInt <= leadTime)
                {
                    entities.Status = "TAKE ACTION";
                    entities.ColorCode = "Green";
                }
                else
                {
                    entities.Status = "NEW";
                    entities.ColorCode = "Black";
                }
            }

            if (daysLeft <= 0)
            {
                entities.Status = "EXPIRED";
                entities.ColorCode = "Maroon";
            }

            entities.DaysLeft = daysLeftInt.ToString();

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

public List<Entities_PLM_PermitCertificates> PLM_TRANSACTION_Request_History_GetAll()
{
    List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();
    DbDataReader reader = null;

    try
    {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PLM_TRANSACTION_Request_History_GetAll";

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Entities_PLM_PermitCertificates entities = new Entities_PLM_PermitCertificates();

            entities.RefId = reader["RefId"].ToString();
            entities.PermitName = reader["PermitName"].ToString();
            entities.ChemicalContent = reader["ChemicalContent"].ToString();
            entities.ItemName = reader["ItemName"].ToString();
            entities.Specification = reader["Specification"].ToString();
            entities.GovernmentAgency = reader["GovernmentAgency"].ToString();
            entities.Frequency = reader["Frequency"].ToString();
            entities.SpecifiedRequirements = reader["SpecifiedRequirements"].ToString();
            entities.LeadTime = reader["LeadTime"].ToString();
            entities.Safety = reader["Safety"].ToString();
            entities.ProcessorName = reader["ProcessorName"].ToString();
            entities.Supplier = reader["Supplier"].ToString();
            //entities.SupplierName = reader["SupplierName"].ToString();
            //entities.Status = reader["Status"].ToString();
            entities.IssuedDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(reader["IssuedDate"].ToString()));
            entities.ExpirationDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(reader["ExpirationDate"].ToString()));
            entities.AddedBy = reader["AddedBy"].ToString();
            entities.IsDisabled = reader["IsDisabled"].ToString();

            Common COMMON = new Common();
            string dDate2 = reader["ExpirationDate"].ToString();
            DateTime dSecond = DateTime.Parse(dDate2);
            double daysLeft = COMMON.GetBusinessDays(DateTime.Now, dSecond);
            int daysLeftInt = (int)daysLeft;
            int leadTime = int.Parse(reader["LeadTime"].ToString());

            if (!string.IsNullOrEmpty(reader["HistoryExpirationDate"].ToString()))
            {
                entities.Status = "RENEWED";
                entities.ColorCode = "Blue";
            }
            else
            {
                if (daysLeftInt <= leadTime)
                {
                    entities.Status = "TAKE ACTION";
                    entities.ColorCode = "Green";
                }
                else
                {
                    entities.Status = "NEW";
                    entities.ColorCode = "Black";
                }
            }

            if (daysLeft <= 0)
            {
                entities.Status = "EXPIRED";
                entities.ColorCode = "Maroon";
            }

            entities.DaysLeft = daysLeftInt.ToString();

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

public List<Entities_PLM_PermitCertificates> PLM_TRANSACTION_Request_History_GetByRefId(string refid)
{
    List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();
    DbDataReader reader = null;

    try
    {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PLM_TRANSACTION_Request_History_GetByRefId";
        cmd.Parameters.Add(Factory.CreateParameter("@refid", refid));

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Entities_PLM_PermitCertificates entities = new Entities_PLM_PermitCertificates();

            entities.RefId = reader["RefId"].ToString();
            entities.PermitName = reader["PermitName"].ToString();
            entities.ChemicalContent = reader["ChemicalContent"].ToString();
            entities.ItemName = reader["ItemName"].ToString();
            entities.Specification = reader["Specification"].ToString();
            entities.GovernmentAgency = reader["GovernmentAgency"].ToString();
            entities.Frequency = reader["Frequency"].ToString();
            entities.SpecifiedRequirements = reader["SpecifiedRequirements"].ToString();
            entities.LeadTime = reader["LeadTime"].ToString();
            entities.Safety = reader["Safety"].ToString();
            entities.ProcessorName = reader["ProcessorName"].ToString();
            entities.Supplier = reader["Supplier"].ToString();
            //entities.SupplierName = reader["SupplierName"].ToString();
            //entities.Status = reader["Status"].ToString();
            entities.IssuedDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(reader["IssuedDate"].ToString()));
            entities.ExpirationDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(reader["ExpirationDate"].ToString()));
            entities.AddedBy = CryptorEngine.Decrypt(reader["AddedByName"].ToString(), true).ToUpper(); 
            entities.AddedDate = reader["AddedDate"].ToString();
            entities.IsDisabled = reader["IsDisabled"].ToString();
            

            Common COMMON = new Common();
            string dDate2 = reader["ExpirationDate"].ToString();
            DateTime dSecond = DateTime.Parse(dDate2);
            double daysLeft = COMMON.GetBusinessDays(DateTime.Now, dSecond);
            int daysLeftInt = (int)daysLeft;
            int leadTime = int.Parse(reader["LeadTime"].ToString());

            if (!string.IsNullOrEmpty(reader["HistoryExpirationDate"].ToString()))
            {
                entities.Status = "RENEWED";
                entities.ColorCode = "Blue";
            }
            else
            {
                if (daysLeftInt <= leadTime)
                {
                    entities.Status = "TAKE ACTION";
                    entities.ColorCode = "Green";
                }
                else
                {
                    entities.Status = "NEW";
                    entities.ColorCode = "Black";
                }
            }

            if (daysLeft <= 0)
            {
                entities.Status = "EXPIRED";
                entities.ColorCode = "Maroon";
            }

            entities.DaysLeft = daysLeftInt.ToString();

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

public List<Entities_PLM_PermitCertificates> PLM_TRANSACTION_Request_GetAll_Like(string item)
{
    List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();
    DbDataReader reader = null;

    try
    {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PLM_TRANSACTION_Request_GetAll_Like";
        cmd.Parameters.Add(Factory.CreateParameter("@criteria", item));

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Entities_PLM_PermitCertificates entities = new Entities_PLM_PermitCertificates();

            entities.RefId = reader["RefId"].ToString();
            entities.PermitName = reader["PermitName"].ToString();
            entities.ChemicalContent = reader["ChemicalContent"].ToString();
            entities.ItemName = reader["ItemName"].ToString();
            entities.Specification = reader["Specification"].ToString();
            entities.GovernmentAgency = reader["GovernmentAgency"].ToString();
            entities.Frequency = reader["Frequency"].ToString();
            entities.SpecifiedRequirements = reader["SpecifiedRequirements"].ToString();
            entities.LeadTime = reader["LeadTime"].ToString();
            entities.Safety = reader["Safety"].ToString();
            entities.ProcessorName = reader["ProcessorName"].ToString();
            entities.Supplier = reader["Supplier"].ToString();
            //entities.SupplierName = reader["SupplierName"].ToString();
            //entities.Status = reader["Status"].ToString();
            entities.IssuedDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(reader["IssuedDate"].ToString()));
            entities.ExpirationDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(reader["ExpirationDate"].ToString()));
            entities.AddedBy = CryptorEngine.Decrypt(reader["AddedByName"].ToString(), true).ToUpper();
            entities.AddedDate = reader["AddedDate"].ToString();
            entities.IsDisabled = reader["IsDisabled"].ToString();


            Common COMMON = new Common();
            string dDate2 = reader["ExpirationDate"].ToString();
            DateTime dSecond = DateTime.Parse(dDate2);
            double daysLeft = COMMON.GetBusinessDays(DateTime.Now, dSecond);
            int daysLeftInt = (int)daysLeft;
            int leadTime = int.Parse(reader["LeadTime"].ToString());

            if (!string.IsNullOrEmpty(reader["HistoryExpirationDate"].ToString()))
            {
                entities.Status = "RENEWED";
                entities.ColorCode = "Blue";
            }
            else
            {
                if (daysLeftInt <= leadTime)
                {
                    entities.Status = "TAKE ACTION";
                    entities.ColorCode = "Green";
                }
                else
                {
                    entities.Status = "NEW";
                    entities.ColorCode = "Black";
                }
            }

            if (daysLeft <= 0)
            {
                entities.Status = "EXPIRED";
                entities.ColorCode = "Maroon";
            }

            entities.DaysLeft = daysLeftInt.ToString();

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

public List<Entities_PLM_PermitCertificates> PLM_TRANSACTION_RequestEntry_Fill_All_DropdownList()
{
    List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();
    DbDataReader reader = null;

    try
    {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PLM_TRANSACTION_RequestEntry_Fill_All_DropdownList";

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Entities_PLM_PermitCertificates entities = new Entities_PLM_PermitCertificates();

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


public int PLM_TRANSACTION_Request_Insert(Entities_PLM_PermitCertificates entity)
{
    int result = 0;

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();

    cmd.CommandType = CommandType.StoredProcedure;
    cmd.CommandText = "PLM_TRANSACTION_Request_Insert";

    cmd.Parameters.Add(Factory.CreateParameter("@permitname", entity.PermitName));
    cmd.Parameters.Add(Factory.CreateParameter("@chemicalcontent", entity.ChemicalContent));
    cmd.Parameters.Add(Factory.CreateParameter("@itemname", entity.ItemName));
    cmd.Parameters.Add(Factory.CreateParameter("@specification", entity.Specification));
    cmd.Parameters.Add(Factory.CreateParameter("@governmentagency", entity.GovernmentAgency));
    cmd.Parameters.Add(Factory.CreateParameter("@frequency", entity.Frequency));
    cmd.Parameters.Add(Factory.CreateParameter("@specifiedrequirements", entity.SpecifiedRequirements));
    cmd.Parameters.Add(Factory.CreateParameter("@leadtime", entity.LeadTime));
    cmd.Parameters.Add(Factory.CreateParameter("@safety", entity.Safety));
    cmd.Parameters.Add(Factory.CreateParameter("@processorname", entity.ProcessorName));
    cmd.Parameters.Add(Factory.CreateParameter("@supplier", entity.Supplier));
    cmd.Parameters.Add(Factory.CreateParameter("@status", entity.Status));
    cmd.Parameters.Add(Factory.CreateParameter("@issueddate", entity.IssuedDate));
    cmd.Parameters.Add(Factory.CreateParameter("@expirationdate", entity.ExpirationDate));
    cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.AddedBy));
    cmd.Parameters.Add(Factory.CreateParameter("@attachment1", entity.Attachment1));
    cmd.Parameters.Add(Factory.CreateParameter("@attachment2", entity.Attachment2));
    cmd.Parameters.Add(Factory.CreateParameter("@attachment3", entity.Attachment3));

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

public int PLM_TRANSACTION_Request_Append(Entities_PLM_PermitCertificates entity)
{
    int result = 0;

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();

    cmd.CommandType = CommandType.StoredProcedure;
    cmd.CommandText = "PLM_TRANSACTION_Request_Append";

    cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.RefId));
    cmd.Parameters.Add(Factory.CreateParameter("@permitname", entity.PermitName));
    cmd.Parameters.Add(Factory.CreateParameter("@chemicalcontent", entity.ChemicalContent));
    cmd.Parameters.Add(Factory.CreateParameter("@itemname", entity.ItemName));
    cmd.Parameters.Add(Factory.CreateParameter("@specification", entity.Specification));
    cmd.Parameters.Add(Factory.CreateParameter("@governmentagency", entity.GovernmentAgency));
    cmd.Parameters.Add(Factory.CreateParameter("@frequency", entity.Frequency));
    cmd.Parameters.Add(Factory.CreateParameter("@specifiedrequirements", entity.SpecifiedRequirements));
    cmd.Parameters.Add(Factory.CreateParameter("@leadtime", entity.LeadTime));
    cmd.Parameters.Add(Factory.CreateParameter("@safety", entity.Safety));
    cmd.Parameters.Add(Factory.CreateParameter("@processorname", entity.ProcessorName));
    cmd.Parameters.Add(Factory.CreateParameter("@supplier", entity.Supplier));
    cmd.Parameters.Add(Factory.CreateParameter("@status", entity.Status));
    cmd.Parameters.Add(Factory.CreateParameter("@issueddate", entity.IssuedDate));
    cmd.Parameters.Add(Factory.CreateParameter("@expirationdate", entity.ExpirationDate));
    cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.UpdatedBy));
    cmd.Parameters.Add(Factory.CreateParameter("@attachment1", entity.Attachment1));
    cmd.Parameters.Add(Factory.CreateParameter("@attachment2", entity.Attachment2));
    cmd.Parameters.Add(Factory.CreateParameter("@attachment3", entity.Attachment3));

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

public int PLM_TRANSACTION_SQLTransaction(string query)
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

public List<Entities_PLM_PermitCertificates> PLM_TRANSACTION_NotificationReceiver_GetByRefIdAndSupplierIdAndIssuedDateAndExpiration(Entities_PLM_PermitCertificates entity)
{
    List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();
    DbDataReader reader = null;

    try
    {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PLM_TRANSACTION_NotificationReceiver_GetByRefIdAndSupplierIdAndIssuedDateAndExpiration";
        cmd.Parameters.Add(Factory.CreateParameter("@permitrefid", entity.PermitId));
        //cmd.Parameters.Add(Factory.CreateParameter("@suppliername", entity.SupplierName));
        cmd.Parameters.Add(Factory.CreateParameter("@issueddate", entity.IssuedDate));
        cmd.Parameters.Add(Factory.CreateParameter("@expirationdate", entity.ExpirationDate));

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Entities_PLM_PermitCertificates entities = new Entities_PLM_PermitCertificates();

            entities.RefId = reader["PermitRefId"].ToString();
            entities.SupplierName = reader["SupplierDesc"].ToString();
            entities.SupplierCode = reader["SupplierName"].ToString();
            entities.SupplierEmailAddress = reader["SupplierEmailAddress"].ToString();
            entities.NotifiedAlready = reader["NotifiedAlready"] != DBNull.Value ? "SUCCESSFULLY SENT" : "FOR SENDING";

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


public List<Entities_PLM_PermitCertificates> PLM_TRANSACTION_NotificationReceiver_GetByRefId(string refid)
{
    List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();
    DbDataReader reader = null;

    try
    {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PLM_TRANSACTION_NotificationReceiver_GetByRefId";
        cmd.Parameters.Add(Factory.CreateParameter("@refid", refid));

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Entities_PLM_PermitCertificates entities = new Entities_PLM_PermitCertificates();

            entities.RefId = reader["PermitRefId"].ToString();
            entities.SupplierName = reader["SupplierDesc"].ToString();
            entities.SupplierCode = reader["SupplierName"].ToString();
            entities.SupplierEmailAddress = reader["SupplierEmailAddress"].ToString();
            entities.NotifiedAlready = reader["NotifiedAlready"] != DBNull.Value ? "SUCCESSFULLY SENT" : "FOR SENDING";

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

public List<Entities_PLM_PermitCertificates> PLM_TRANSACTION_NotificationReceiver_GetByRefId_AndSupplierCode(string refid, string code)
{
    List<Entities_PLM_PermitCertificates> list = new List<Entities_PLM_PermitCertificates>();

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();
    DbDataReader reader = null;

    try
    {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PLM_TRANSACTION_NotificationReceiver_GetByRefId_AndSupplierCode";
        cmd.Parameters.Add(Factory.CreateParameter("@refid", refid));
        cmd.Parameters.Add(Factory.CreateParameter("@suppliercode", code));

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Entities_PLM_PermitCertificates entities = new Entities_PLM_PermitCertificates();

            entities.RefId = reader["RefId"].ToString();
            entities.SupplierName = reader["Description"].ToString();
            entities.SupplierCode = reader["Code"].ToString();
            entities.SupplierEmailAddress = reader["EmailAddress"].ToString();
            entities.SupplierSelected = reader["Selected"].ToString();
            entities.TakeAction = reader["TakeAction"].ToString();
            entities.Expired = reader["Expired"].ToString();
            entities.Renewed = reader["Renewed"].ToString();

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


#region GOVERNMENT AGENCIES

public List<Entities_PLM_GovernmentAgencies> PLM_MT_GovernmentAgencies_GetAll()
{
    List<Entities_PLM_GovernmentAgencies> list = new List<Entities_PLM_GovernmentAgencies>();

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();
    DbDataReader reader = null;

    try
    {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PLM_MT_GovernmentAgencies_GetAll";

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Entities_PLM_GovernmentAgencies entities = new Entities_PLM_GovernmentAgencies();

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

public int PLM_MT_GovernmentAgencies_IsDisabled(Entities_PLM_GovernmentAgencies entity)
{
    int result = 0;

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();

    cmd.CommandType = CommandType.StoredProcedure;
    cmd.CommandText = "PLM_MT_GovernmentAgencies_IsDisabled";

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

public int PLM_MT_GovernmentAgencies_Append(Entities_PLM_GovernmentAgencies entity)
{
    int result = 0;

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();

    cmd.CommandType = CommandType.StoredProcedure;
    cmd.CommandText = "PLM_MT_GovernmentAgencies_Append";

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

public int PLM_MT_GovernmentAgencies_Insert(Entities_PLM_GovernmentAgencies entity)
{
    int result = 0;

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();

    cmd.CommandType = CommandType.StoredProcedure;
    cmd.CommandText = "PLM_MT_GovernmentAgencies_Insert";

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

public List<Entities_PLM_GovernmentAgencies> PLM_MT_GovernmentAgencies_GetByName(string name)
{
    List<Entities_PLM_GovernmentAgencies> list = new List<Entities_PLM_GovernmentAgencies>();

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();
    DbDataReader reader = null;

    try
    {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PLM_MT_GovernmentAgencies_GetByName";

        cmd.Parameters.Add(Factory.CreateParameter("@name", name));

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Entities_PLM_GovernmentAgencies entities = new Entities_PLM_GovernmentAgencies();

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

public List<Entities_PLM_GovernmentAgencies> PLM_MT_GovernmentAgencies_GetByName_Like(string name)
{
    List<Entities_PLM_GovernmentAgencies> list = new List<Entities_PLM_GovernmentAgencies>();

    DbConnection conn = Factory.CreateConnection();
    DbCommand cmd = Factory.CreateCommand();
    DbDataReader reader = null;

    try
    {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "PLM_MT_GovernmentAgencies_GetByName_Like";

        cmd.Parameters.Add(Factory.CreateParameter("@name", name));

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Entities_PLM_GovernmentAgencies entities = new Entities_PLM_GovernmentAgencies();

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

}
