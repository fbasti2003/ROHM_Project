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

public class DAL_CRF
{
    public DAL_CRF()
    {
    }

    #region REASON

    public List<Entities_CRF_Reason> CRF_MT_Reason_GetAll()
    {
        List<Entities_CRF_Reason> list = new List<Entities_CRF_Reason>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRF_MT_Reason_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_Reason entities = new Entities_CRF_Reason();

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

    public int CRF_MT_Reason_IsDisabled(Entities_CRF_Reason entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "CRF_MT_Reason_IsDisabled";

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

    public int CRF_MT_Reason_Append(Entities_CRF_Reason entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "CRF_MT_Reason_Append";

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

    public int CRF_MT_Reason_Insert(Entities_CRF_Reason entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "CRF_MT_Reason_Insert";

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

    public List<Entities_CRF_Reason> CRF_MT_Reason_GetByName(string name)
    {
        List<Entities_CRF_Reason> list = new List<Entities_CRF_Reason>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRF_MT_Reason_GetByName";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_Reason entities = new Entities_CRF_Reason();

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

    public List<Entities_CRF_Reason> CRF_MT_Reason_GetByName_Like(string name)
    {
        List<Entities_CRF_Reason> list = new List<Entities_CRF_Reason>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRF_MT_Reason_GetByName_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_Reason entities = new Entities_CRF_Reason();

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

    #region CRF TRANSACTION ENTRY

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_RequestEntry_Fill_All_DropdownList()
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRF_TRANSACTION_RequestEntry_Fill_All_DropdownList";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

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

    public Int32 CRF_TRANSACTION_CountRequestHead(string year)
    {
        Int32 result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "CRF_TRANSACTION_CountRequestHead";

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

    public string CRF_TRANSACTION_Request_Insert(Entities_CRF_RequestEntry entity)
    {
        string result = string.Empty;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "CRF_TRANSACTION_Request_Insert";

        cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo));
        cmd.Parameters.Add(Factory.CreateParameter("@supplier", entity.Supplier));
        cmd.Parameters.Add(Factory.CreateParameter("@attention", entity.Attention));
        cmd.Parameters.Add(Factory.CreateParameter("@podate", entity.PoDate));
        cmd.Parameters.Add(Factory.CreateParameter("@prno", entity.PrNO));
        cmd.Parameters.Add(Factory.CreateParameter("@pono", entity.PoNO));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
        cmd.Parameters.Add(Factory.CreateParameter("@typedrawingno", entity.TypeDrawingNo));
        cmd.Parameters.Add(Factory.CreateParameter("@orderquantity", entity.OrderQuantity));
        cmd.Parameters.Add(Factory.CreateParameter("@reason", entity.Reason));
        cmd.Parameters.Add(Factory.CreateParameter("@category", entity.Category));
        cmd.Parameters.Add(Factory.CreateParameter("@attachment1", entity.Attachment1));
        cmd.Parameters.Add(Factory.CreateParameter("@attachment2", entity.Attachment2));
        cmd.Parameters.Add(Factory.CreateParameter("@attachment3", entity.Attachment3));
        cmd.Parameters.Add(Factory.CreateParameter("@attachment4", entity.Attachment4));
        cmd.Parameters.Add(Factory.CreateParameter("@attachment5", entity.Attachment5));
        cmd.Parameters.Add(Factory.CreateParameter("@attachment6", entity.Attachment6));
        cmd.Parameters.Add(Factory.CreateParameter("@attachment7", entity.Attachment7));
        cmd.Parameters.Add(Factory.CreateParameter("@attachment8", entity.Attachment8));
        cmd.Parameters.Add(Factory.CreateParameter("@requester", entity.Requester));
        cmd.Parameters.Add(Factory.CreateParameter("@di_supplier", entity.DateInformedSupplier));
        cmd.Parameters.Add(Factory.CreateParameter("@di_requestor", entity.DateInformedRequestor));

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

    public int CRF_TRANSACTION_Request_Append(Entities_CRF_RequestEntry entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "CRF_TRANSACTION_Request_Append";

        cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo));
        cmd.Parameters.Add(Factory.CreateParameter("@supplier", entity.Supplier));
        cmd.Parameters.Add(Factory.CreateParameter("@attention", entity.Attention));
        cmd.Parameters.Add(Factory.CreateParameter("@podate", entity.PoDate));
        cmd.Parameters.Add(Factory.CreateParameter("@prno", entity.PrNO));
        cmd.Parameters.Add(Factory.CreateParameter("@pono", entity.PoNO));
        cmd.Parameters.Add(Factory.CreateParameter("@description", entity.Description));
        cmd.Parameters.Add(Factory.CreateParameter("@typedrawingno", entity.TypeDrawingNo));
        cmd.Parameters.Add(Factory.CreateParameter("@orderquantity", entity.OrderQuantity));
        cmd.Parameters.Add(Factory.CreateParameter("@reason", entity.Reason));
        cmd.Parameters.Add(Factory.CreateParameter("@category", entity.Category));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.UpdatedBy));
        cmd.Parameters.Add(Factory.CreateParameter("@di_supplier", entity.DateInformedSupplier));
        cmd.Parameters.Add(Factory.CreateParameter("@di_requestor", entity.DateInformedRequestor));

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

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_RequestStatus_ByDateRange(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRF_TRANSACTION_RequestStatus_ByDateRange";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.CrFrom = entity.CrFrom.Length <= 0 ? string.Empty : entity.CrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.CrTo = entity.CrTo.Length <= 0 ? string.Empty : entity.CrTo));
            cmd.Parameters.Add(Factory.CreateParameter("@requester", entity.Requester = entity.Requester.Length <= 0 ? string.Empty : entity.Requester));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

                entities.RefId = reader["RefId"].ToString();
                entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.TransactionDate = reader["TransactionDate"].ToString().ToUpper();
                entities.RequesterS = reader["Requester"].ToString();
                entities.ReqManager = reader["Req_Manager"].ToString();
                entities.PurIncharge = reader["PurIncharge"].ToString();
                entities.PurManager = reader["Pur_Manager"].ToString();
                entities.RequesterSDOA = reader["DOARequester"].ToString();
                entities.ReqManagerDOA = reader["DOAReq_Manager"].ToString();
                entities.PurInchargeDOA = reader["DOAPur_Incharge"].ToString();
                entities.PurManagerDOA = reader["DOAPur_Manager"].ToString();
                entities.RequesterSStat = reader["STATRequester"].ToString();
                entities.ReqManagerStat = reader["STATReq_Manager"].ToString();
                entities.PurInchargeStat = reader["STATPur_Incharge"].ToString();
                entities.PurManagerStat = reader["STATPur_Manager"].ToString();
                entities.BuyerSend = reader["BuyerSend"].ToString();
                entities.BuyerSendDOA = reader["DOABuyerSend"].ToString();
                entities.BuyerSendStat = reader["STATBuyerSend"].ToString();
                entities.StatRemarks = reader["StatRemarks"].ToString();
                entities.SupplierResponded = reader["SupplierResponded"].ToString();
                entities.CategoryId = reader["Category"].ToString();
                entities.Category = reader["CategoryName"].ToString();
                entities.Posted = reader["Posted"].ToString();
                entities.StatRemarks = reader["StatRemarks"].ToString();

                if (entities.ReqManagerStat == "0")
                {
                    entities.StatAll = "FOR PROD. MNGR APPROVAL";
                    entities.CssColorCode = "#f44336";
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0")
                {
                    entities.StatAll = "FOR SUPPLY CHAIN BUYER APPROVAL";
                    entities.CssColorCode = "#8BC34A";
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "0")
                {
                    entities.StatAll = "FOR SUPPLY CHAIN INCHARGE APPROVAL";
                    entities.CssColorCode = "#CDDC39";
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "1" && entities.BuyerSendStat == "0")
                {
                    entities.StatAll = "FOR SENDING";
                    entities.CssColorCode = "#8BC34A";
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "1" && entities.BuyerSendStat == "1")
                {
                    entities.StatAll = "FOR RESEND";
                    entities.CssColorCode = "#673AB7";
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "1" && entities.BuyerSendStat == "1" && entities.SupplierResponded == "1")
                {
                    entities.StatAll = "SUPPLIER RESPONDED";
                    entities.CssColorCode = "#009688";
                }

                if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurManagerStat == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
                }

                if (entities.Posted == "1")
                {
                    entities.StatAll = "CLOSED";
                    entities.CssColorCode = "#9C27B0";
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

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_Reporting_ByDateRange(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRF_TRANSACTION_Reporting_ByDateRange";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.CrFrom = entity.CrFrom.Length <= 0 ? string.Empty : entity.CrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.CrTo = entity.CrTo.Length <= 0 ? string.Empty : entity.CrTo));
            cmd.CommandTimeout = 500;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

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

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_Reporting_ByDateRange_ByDivision(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRF_TRANSACTION_Reporting_ByDateRange_ByDivision";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.CrFrom = entity.CrFrom.Length <= 0 ? string.Empty : entity.CrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.CrTo = entity.CrTo.Length <= 0 ? string.Empty : entity.CrTo));
            cmd.CommandTimeout = 500;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

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

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_Reporting_ByDateRange_ByAll(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRF_TRANSACTION_Reporting_ByDateRange_ByAll";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.CrFrom = entity.CrFrom.Length <= 0 ? string.Empty : entity.CrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.CrTo = entity.CrTo.Length <= 0 ? string.Empty : entity.CrTo));
            cmd.CommandTimeout = 500;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

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

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_Reporting_ByDateRange_Details(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT HEAD.CTRLNo, SUP.Name AS Supplier, HEAD.Attention, DETAILS.PODate, DETAILS.PRNo, DETAILS.PONo, DETAILS.Description, DETAILS.Reason, " +
                                "DETAILS.TypeDrawingNo, DETAILS.OrderQuantity, HEAD.Category, HEAD.TransactionDate, " +
                                "HEAD.Requester, CAT.Description AS CategoryName, LC.FullName AS RequesterName, " +
                                "STAT.Req_Manager, STAT.DOAReq_Manager, STAT.STATReq_Manager, STAT.PurIncharge, STAT.DOAPur_Incharge, STAT.STATPur_Incharge, " +
                                "STAT.Pur_Manager, STAT.DOAPur_Manager, STAT.STATPur_Manager, STAT.BuyerSend, STAT.DOABuyerSend, " +
                                "STAT.SupplierResponded, STAT.SupplierResponseDate, STAT.Remarks, STAT.Posted, STAT.PostedBy, STAT.PostedDate, STAT.PostingRemarks, " +
                                "(SELECT TOP 1 TransactionType FROM CRF_TRANSACTION_SendReceived WHERE CTRLNo = HEAD.CTRLNo ORDER BY SendReceivedDate DESC) AS SendReceived, " +
                                "(SELECT TOP 1 FullName FROM Login_Credentials WITH (NOLOCK) WHERE RefId = STAT.Req_Manager) AS Req_ManagerName, " +
                                "(SELECT TOP 1 FullName FROM Login_Credentials WITH (NOLOCK) WHERE RefId = STAT.PurIncharge) AS Pur_InchargeName, " +
                                "(SELECT TOP 1 FullName FROM Login_Credentials WITH (NOLOCK) WHERE RefId = STAT.Pur_Manager) AS Pur_ManagerName, " +
                                "(SELECT TOP 1 FullName FROM Login_Credentials WITH (NOLOCK) WHERE RefId = STAT.PostedBy) AS PostedByName, " +
                                "(SELECT TOP 1 FullName FROM Login_Credentials WITH (NOLOCK) WHERE RefId = STAT.BuyerSend) AS BuyerSendName, " +
                                "(SELECT TOP 1 Name FROM MT_Supplier_Head WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
                                "(SELECT TOP 1 ConfirmedBy FROM CRF_TRANSACTION_SupplierResponse WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS ConfirmedBy, " +
                                "(SELECT TOP 1 DateConfirmed FROM CRF_TRANSACTION_SupplierResponse WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS DateConfirmed, " +
                                "(SELECT TOP 1 Notes FROM CRF_TRANSACTION_SupplierResponse WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo) AS Notes " +
                                "FROM CRF_TRANSACTION_RequestHead HEAD WITH (NOLOCK) " +
                                "INNER JOIN CRF_TRANSACTION_Status STAT WITH (NOLOCK) ON HEAD.CTRLNo = STAT.CTRLNo " +
                                "INNER JOIN CRF_TRANSACTION_RequestDetails DETAILS WITH (NOLOCK) ON HEAD.CTRLNo = DETAILS.CTRLNo " +
                                "INNER JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                                "INNER JOIN Login_Credentials LC ON HEAD.Requester = LC.RefId " +
                                "INNER JOIN MT_Supplier_Head SUP ON HEAD.Supplier = SUP.RefId " +
                                "WHERE CONVERT(DATE, HEAD.TransactionDate) >= '" + entity.CrFrom + "' AND CONVERT(DATE, HEAD.TransactionDate) <= '" + entity.CrTo + "' " +
                                "ORDER BY HEAD.RefId DESC";
            cmd.CommandTimeout = 500;


            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

                entities.CtrlNo = reader["CTRLNo"].ToString();
                entities.Requester = CryptorEngine.Decrypt(reader["RequesterName"].ToString().Replace(" ", "+"), true);
                entities.Supplier = reader["Supplier"].ToString();
                entities.Category = reader["CategoryName"].ToString();
                entities.Attention = reader["Attention"] != DBNull.Value ? reader["Attention"].ToString() : string.Empty;
                entities.PoDate = reader["PoDate"] != DBNull.Value ? reader["PoDate"].ToString() : string.Empty;
                entities.PoNO = reader["PoNO"] != DBNull.Value ? reader["PoNO"].ToString() : string.Empty;
                entities.PrNO = reader["PrNO"] != DBNull.Value ? reader["PrNO"].ToString() : string.Empty;
                entities.Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty;
                entities.TypeDrawingNo = reader["TypeDrawingNo"] != DBNull.Value ? reader["TypeDrawingNo"].ToString() : string.Empty;
                entities.OrderQuantity = reader["OrderQuantity"] != DBNull.Value ? reader["OrderQuantity"].ToString() : string.Empty;
                entities.Reason = reader["Reason"] != DBNull.Value ? reader["Reason"].ToString() : string.Empty;
                entities.TransactionDate = reader["TransactionDate"] != DBNull.Value ? reader["TransactionDate"].ToString() : string.Empty;
                entities.SupplierResponseDate = reader["SupplierResponseDate"] != DBNull.Value ? reader["SupplierResponseDate"].ToString() : string.Empty;

                entities.PurInchargeStat = reader["STATPur_Incharge"] != DBNull.Value ? reader["STATPur_Incharge"].ToString().ToUpper().Trim().Replace(",", string.Empty) : "0";
                entities.PurManagerStat = reader["STATPur_Manager"] != DBNull.Value ? reader["STATPur_Manager"].ToString().ToUpper().Trim().Replace(",", string.Empty) : "0";
                entities.PostedBy = reader["PostedByName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["PostedByName"].ToString().Replace(" ", "+"), true) : "0";

                if (entities.PurManagerStat == "1")
                {
                    entities.StatAll = "APPROVED";
                }
                if (string.IsNullOrEmpty(entities.PurManagerStat) || entities.PurManagerStat == "0")
                {
                    entities.StatAll = "PENDING APPROVAL";
                }
                if (entities.PurManagerStat == "2" || entities.PurInchargeStat == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                }
                if (entities.PostedBy != "0")
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

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_RequestStatus_ByDateRange_All(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRF_TRANSACTION_RequestStatus_ByDateRange_All";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.CrFrom = entity.CrFrom.Length <= 0 ? string.Empty : entity.CrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.CrTo = entity.CrTo.Length <= 0 ? string.Empty : entity.CrTo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

                entities.RefId = reader["RefId"].ToString();
                entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.TransactionDate = reader["TransactionDate"].ToString().ToUpper();
                entities.RequesterS = reader["Requester"].ToString();
                entities.ReqManager = reader["Req_Manager"].ToString();
                entities.PurIncharge = reader["PurIncharge"].ToString();
                entities.PurManager = reader["Pur_Manager"].ToString();
                entities.RequesterSDOA = reader["DOARequester"].ToString();
                entities.ReqManagerDOA = reader["DOAReq_Manager"].ToString();
                entities.PurInchargeDOA = reader["DOAPur_Incharge"].ToString();
                entities.PurManagerDOA = reader["DOAPur_Manager"].ToString();
                entities.RequesterSStat = reader["STATRequester"].ToString();
                entities.ReqManagerStat = reader["STATReq_Manager"].ToString();
                entities.PurInchargeStat = reader["STATPur_Incharge"].ToString();
                entities.PurManagerStat = reader["STATPur_Manager"].ToString();
                entities.BuyerSend = reader["BuyerSend"].ToString();
                entities.BuyerSendDOA = reader["DOABuyerSend"].ToString();
                entities.BuyerSendStat = reader["STATBuyerSend"].ToString();
                entities.StatRemarks = reader["StatRemarks"].ToString();
                entities.SupplierResponded = reader["SupplierResponded"].ToString();
                entities.CategoryId = reader["Category"].ToString();
                entities.Category = reader["CategoryName"].ToString();
                entities.Posted = reader["Posted"].ToString();
                entities.StatRemarks = reader["StatRemarks"].ToString();
                entities.SearchCriteria = reader["SearchCriteria"].ToString().ToLower();

                if (entities.ReqManagerStat == "0")
                {
                    entities.StatAll = "FOR PROD. MNGR APPROVAL";
                    entities.CssColorCode = "#f44336";
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0")
                {
                    entities.StatAll = "FOR SUPPLY CHAIN BUYER APPROVAL";
                    entities.CssColorCode = "#8BC34A";
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "0")
                {
                    entities.StatAll = "FOR SUPPLY CHAIN INCHARGE APPROVAL";
                    entities.CssColorCode = "#CDDC39";
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "1" && entities.BuyerSendStat == "0")
                {
                    entities.StatAll = "FOR SENDING";
                    entities.CssColorCode = "#8BC34A";
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "1" && entities.BuyerSendStat == "1")
                {
                    entities.StatAll = "FOR RESEND";
                    entities.CssColorCode = "#673AB7";
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "1" && entities.BuyerSendStat == "1" && entities.SupplierResponded == "1")
                {
                    entities.StatAll = "SUPPLIER RESPONDED";
                    entities.CssColorCode = "#009688";
                }

                if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurManagerStat == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
                }

                if (entities.Posted == "1")
                {
                    entities.StatAll = "CLOSED";
                    entities.CssColorCode = "#9C27B0";
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


    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_GetRequestByCTRLNo(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRF_TRANSACTION_GetRequestByCTRLNo";
            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));


            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

                entities.RefId = reader["RefId"].ToString();
                entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.Supplier = reader["Supplier"].ToString();
                entities.Attention = reader["Attention"].ToString();
                entities.CategoryId = reader["Category"].ToString();
                entities.Category = reader["CategoryName"].ToString();
                entities.TransactionDate = reader["TransactionDate"].ToString();
                entities.RequesterEmail = reader["EmailAddress"].ToString();
                entities.RequesterLocalNumber = reader["LocalNumber"].ToString();
                entities.Requester = CryptorEngine.Decrypt(reader["RequesterFullName"].ToString(), true);
                entities.ReqManagerStat = reader["STATReq_Manager"].ToString();
                entities.RequesterSStat = reader["STATRequester"].ToString();

                entities.RequesterS = reader["PreparedBy"] != DBNull.Value ? CryptorEngine.Decrypt(reader["PreparedBy"].ToString(), true) : string.Empty;
                entities.ReqManager = reader["NotedBy"] != DBNull.Value ? CryptorEngine.Decrypt(reader["NotedBy"].ToString(), true) : string.Empty;
                entities.PurIncharge = reader["InchargeBy"] != DBNull.Value ? CryptorEngine.Decrypt(reader["InchargeBy"].ToString(), true) : string.Empty;
                entities.PurManager = reader["ManagerBy"] != DBNull.Value ? CryptorEngine.Decrypt(reader["ManagerBy"].ToString(), true) : string.Empty;

                entities.RequesterSDOA = reader["DOARequester"].ToString();
                entities.ReqManagerDOA = reader["DOAReq_Manager"].ToString();
                entities.PurInchargeDOA = reader["DOAPur_Incharge"].ToString();
                entities.PurManagerDOA = reader["DOAPur_Manager"].ToString();
                entities.BuyerSendDOA = reader["DOABuyerSend"].ToString();
                entities.SupplierResponseDate = reader["SupplierResponseDate"].ToString();

                entities.ReqManagerStat = reader["STATReq_Manager"].ToString();
                entities.PurInchargeStat = reader["STATPur_Incharge"].ToString();
                entities.PurManagerStat = reader["STATPur_Manager"].ToString();
                entities.SupplierResponded = reader["SupplierResponded"].ToString();
                entities.BuyerSendStat = reader["STATBuyerSend"].ToString();
                entities.Posted = reader["Posted"].ToString();
                entities.PostedBy = reader["PostedBy"].ToString();
                entities.PostedDate = reader["PostedDate"].ToString();
                entities.PostingRemarks = reader["PostingRemarks"].ToString();
                entities.SupplierName = reader["SupplierName"].ToString();


                entities.ResponseConfirmedBy = reader["ConfirmedBy"] != DBNull.Value ? reader["ConfirmedBy"].ToString() : string.Empty;
                entities.ResponseDateConfirmed = reader["DateConfirmed"] != DBNull.Value ? reader["DateConfirmed"].ToString() : string.Empty;
                entities.ResponseNotes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : string.Empty;
                entities.ResponseDateReceived = reader["DateReceived"] != DBNull.Value ? reader["DateReceived"].ToString() : string.Empty;

                //-------------------------------------------------------------
                if (reader["STATReq_Manager"].ToString() == "1")
                {
                    entities.ReqManagerStatColor = "#8BC34A";
                    entities.ReqManagerStatDisplay = entities.ReqManager + " (" + entities.ReqManagerDOA + ") - <b>APPROVED</b>";
                }
                else if (reader["STATReq_Manager"].ToString() == "2")
                {
                    entities.ReqManagerStatColor = "#ffbb33";
                    entities.ReqManagerStatDisplay = entities.ReqManager + " (" + entities.ReqManagerDOA + ") - <b>DISAPPROVED</b>";
                }
                else
                {
                    entities.ReqManagerStatColor = "#f44336";
                    entities.ReqManagerStatDisplay = "PENDING APPROVAL";
                }
                //-------------------------------------------------------------
                if (reader["STATPur_Incharge"].ToString() == "1")
                {
                    entities.PurInchargeStatColor = "#8BC34A";
                    entities.BuyerStatDisplay = entities.PurIncharge + " (" + entities.PurInchargeDOA + ") - <b>APPROVED</b>";
                }
                else if (reader["STATPur_Incharge"].ToString() == "2")
                {
                    entities.PurInchargeStatColor = "#ffbb33";
                    entities.BuyerStatDisplay = entities.PurIncharge + " (" + entities.PurInchargeDOA + ") - <b>DISAPPROVED</b>";
                }
                else
                {
                    entities.PurInchargeStatColor = "#f44336";
                    entities.BuyerStatDisplay = "PENDING APPROVAL";
                }
                //-------------------------------------------------------------
                if (reader["STATPur_Manager"].ToString() == "1")
                {
                    entities.PurManagerStatColor = "#8BC34A";
                    entities.ScInchargeStatDisplay = entities.PurManager + " (" + entities.PurManagerDOA + ") - <b>APPROVED</b>";
                }
                else if (reader["STATPur_Manager"].ToString() == "2")
                {
                    entities.PurManagerStatColor = "#ffbb33";
                    entities.ScInchargeStatDisplay = entities.PurManager + " (" + entities.PurManagerDOA + ") - <b>DISAPPROVED</b>";
                }
                else
                {
                    entities.PurManagerStatColor = "#f44336";
                    entities.ScInchargeStatDisplay = "PENDING APPROVAL";
                }
                //-------------------------------------------------------------

                //if (reader["STATReq_Manager"].ToString() == "1" && reader["STATPur_Incharge"].ToString() == "1" && reader["STATPur_Manager"].ToString() == "1")
                //{
                //    entities.SupplierStatDisplay = "FOR SENDING";
                //    entities.SupplierStatColor = "#673AB7";
                //}
                //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && reader["STATPur_Manager"].ToString() == "1" && reader["STATBuyerSend"].ToString() == "1")
                //{
                //    entities.SupplierStatDisplay = "FOR RESEND";
                //    entities.SupplierStatColor = "#8BC34A";
                //}
                //if (reader["STATReq_Manager"].ToString() == "1" && reader["STATPur_Incharge"].ToString() == "1" && reader["STATPur_Manager"].ToString() == "1" && reader["STATBuyerSend"].ToString() == "1" && reader["SupplierResponded"].ToString() == "1")
                //{
                //    entities.SupplierStatDisplay = "SUPPLIER RESPONDED";
                //    entities.SupplierStatColor = "#009688";
                //}
                


                entities.StatRemarks = reader["Remarks"].ToString();

                entities.SendDates = reader["SendDates"] != DBNull.Value ? reader["SendDates"].ToString() : string.Empty;

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

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_GetRequestDetailsByCTRLNo(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        int row_num = 1;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRF_TRANSACTION_GetRequestDetailsByCTRLNo";
            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.RdCtrlNo = entity.RdCtrlNo.Length <= 0 ? string.Empty : entity.RdCtrlNo));


            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

                entities.RowNumber = row_num++.ToString();
                entities.RdRefId = reader["DetailsRefId"].ToString();
                entities.RdCtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.RdPODate = reader["PoDate"].ToString().Replace("12:00:00 am", string.Empty);
                entities.RdPRNO = reader["PRNO"].ToString();
                entities.RdPONO = reader["PONO"].ToString();
                entities.RdItemName = reader["Description"].ToString();
                entities.RdSpecs = reader["TypeDrawingNo"].ToString();
                entities.RdQuantity = reader["OrderQuantity"].ToString();
                entities.RdReason = reader["Reason"].ToString();
                entities.RdUnitOfMeasure = reader["UnitOfMeasure"].ToString();
                entities.RdReasonName = reader["ReasonName"].ToString();
                entities.RdUOMDesc = reader["UOMDesc"].ToString();
                entities.RdAttachment = reader["Attachment1"].ToString();
                entities.RdSupplierAttachment = reader["SupplierAttachment"].ToString();
                entities.DivisionName = reader["DivisionName"].ToString();
                entities.DepartmentName = reader["DepartmentName"].ToString();
                entities.Attention = reader["Attention"].ToString();

                entities.RdDateInformedSupplier = reader["DateInformedSupplier"] != DBNull.Value ? reader["DateInformedSupplier"].ToString() : string.Empty;
                entities.RdDateInformedRequester = reader["DateInformedRequester"] != DBNull.Value ? reader["DateInformedRequester"].ToString() : string.Empty;

                entities.ResponseConfirmedBy = reader["ConfirmedBy"] != DBNull.Value ? reader["ConfirmedBy"].ToString() : string.Empty;
                entities.ResponseDateConfirmed = reader["DateConfirmed"] != DBNull.Value ? reader["DateConfirmed"].ToString() : string.Empty;
                entities.ResponseNotes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : string.Empty;
                entities.ResponseDateReceived = reader["DateReceived"] != DBNull.Value ? reader["DateReceived"].ToString() : string.Empty;
                entities.ResponseResponseDetailsId = reader["ResponseDetailsId"] != DBNull.Value ? reader["ResponseDetailsId"].ToString() : string.Empty;

                entities.RequesterS = reader["PreparedBy"] != DBNull.Value ? CryptorEngine.Decrypt(reader["PreparedBy"].ToString(), true) : string.Empty;
                entities.ReqManager = reader["NotedBy"] != DBNull.Value ? CryptorEngine.Decrypt(reader["NotedBy"].ToString(), true) : string.Empty;
                entities.PurIncharge = reader["InchargeBy"] != DBNull.Value ? CryptorEngine.Decrypt(reader["InchargeBy"].ToString(), true) : string.Empty;
                entities.PurManager = reader["ManagerBy"] != DBNull.Value ? CryptorEngine.Decrypt(reader["ManagerBy"].ToString(), true) : string.Empty;

                entities.RequesterSDOA = reader["DOARequester"].ToString();
                entities.ReqManagerDOA = reader["DOAReq_Manager"].ToString();
                entities.PurInchargeDOA = reader["DOAPur_Incharge"].ToString();
                entities.PurManagerDOA = reader["DOAPur_Manager"].ToString();
                entities.BuyerSendDOA = reader["DOABuyerSend"].ToString();
                entities.SupplierResponseDate = reader["SupplierResponseDate"].ToString();
                entities.SupplierName = reader["SupplierName"].ToString();
                entities.StatRemarks = reader["Remarks"].ToString();

                entities.Attachment1 = reader["Attachment1"] != DBNull.Value ? reader["Attachment1"].ToString() : string.Empty;
                entities.PostedDate = reader["PostedDate"].ToString();


                //-------------------------------------------------------------
                if (reader["STATReq_Manager"].ToString() == "1")
                {
                    entities.ReqManagerStatColor = "#8BC34A";
                    entities.ReqManagerStatDisplay = entities.ReqManager + " (" + entities.ReqManagerDOA + ") - <b>APPROVED</b>";
                }
                else if (reader["STATReq_Manager"].ToString() == "2")
                {
                    entities.ReqManagerStatColor = "#ffbb33";
                    entities.ReqManagerStatDisplay = entities.ReqManager + " (" + entities.ReqManagerDOA + ") - <b>DISAPPROVED</b>";
                }
                else
                {
                    entities.ReqManagerStatColor = "#f44336";
                    entities.ReqManagerStatDisplay = "PENDING APPROVAL";
                }
                //-------------------------------------------------------------
                if (reader["STATPur_Incharge"].ToString() == "1")
                {
                    entities.PurInchargeStatColor = "#8BC34A";
                    entities.BuyerStatDisplay = entities.PurIncharge + " (" + entities.PurInchargeDOA + ") - <b>APPROVED</b>";
                }
                else if (reader["STATPur_Incharge"].ToString() == "2")
                {
                    entities.PurInchargeStatColor = "#ffbb33";
                    entities.BuyerStatDisplay = entities.PurIncharge + " (" + entities.PurInchargeDOA + ") - <b>DISAPPROVED</b>";
                }
                else
                {
                    entities.PurInchargeStatColor = "#f44336";
                    entities.BuyerStatDisplay = "PENDING APPROVAL";
                }
                //-------------------------------------------------------------
                if (reader["STATPur_Manager"].ToString() == "1")
                {
                    entities.PurManagerStatColor = "#8BC34A";
                    entities.ScInchargeStatDisplay = entities.PurManager + " (" + entities.PurManagerDOA + ") - <b>APPROVED</b>";
                }
                else if (reader["STATPur_Manager"].ToString() == "2")
                {
                    entities.PurManagerStatColor = "#ffbb33";
                    entities.ScInchargeStatDisplay = entities.PurManager + " (" + entities.PurManagerDOA + ") - <b>DISAPPROVED</b>";
                }
                else
                {
                    entities.PurManagerStatColor = "#f44336";
                    entities.ScInchargeStatDisplay = "PENDING APPROVAL";
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

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_RequestStatus_ByDateRange_All_New(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRF_TRANSACTION_RequestStatus_ByDateRange_All_New";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.CrFrom = entity.CrFrom.Length <= 0 ? string.Empty : entity.CrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.CrTo = entity.CrTo.Length <= 0 ? string.Empty : entity.CrTo));


            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

                entities.TransactionDate = reader["TransactionDate"].ToString();
                entities.RdRefId = reader["RefId"].ToString();
                entities.RdCtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.RdPODate = reader["PoDate"].ToString().Replace("12:00:00 am", string.Empty);
                entities.RdPRNO = reader["PRNO"].ToString();
                entities.RdPONO = reader["PONO"].ToString();
                entities.RdItemName = reader["Description"].ToString();
                entities.RdSpecs = reader["TypeDrawingNo"].ToString();
                entities.RdQuantity = reader["OrderQuantity"].ToString();
                entities.RdReason = reader["Reason"].ToString();
                entities.RdUnitOfMeasure = reader["UnitOfMeasure"].ToString();
                entities.RdReasonName = reader["ReasonName"].ToString();
                entities.RdUOMDesc = reader["UOMDesc"].ToString();
                entities.RdAttachment = reader["Attachment1"].ToString();
                entities.RdSupplierAttachment = reader["SupplierAttachment"].ToString();

                entities.RdDateInformedSupplier = reader["DateInformedSupplier"] != DBNull.Value ? reader["DateInformedSupplier"].ToString() : string.Empty;
                entities.RdDateInformedRequester = reader["DateInformedRequester"] != DBNull.Value ? reader["DateInformedRequester"].ToString() : string.Empty;

                entities.ResponseConfirmedBy = reader["ConfirmedBy"] != DBNull.Value ? reader["ConfirmedBy"].ToString() : string.Empty;
                entities.ResponseDateConfirmed = reader["DateConfirmed"] != DBNull.Value ? reader["DateConfirmed"].ToString() : string.Empty;
                entities.ResponseNotes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : string.Empty;
                entities.ResponseDateReceived = reader["DateReceived"] != DBNull.Value ? reader["DateReceived"].ToString() : string.Empty;
                entities.ResponseResponseDetailsId = reader["ResponseDetailsId"] != DBNull.Value ? reader["ResponseDetailsId"].ToString() : string.Empty;

                entities.Attention = reader["Attention"].ToString();
                entities.Supplier = reader["Supplier"].ToString();
                entities.Requester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true);
                entities.Category = reader["Category"].ToString();


                entities.ReqManagerStat = reader["STATReq_Manager"].ToString();
                entities.PurInchargeStat = reader["STATPur_Incharge"].ToString();
                entities.PurManagerStat = reader["STATPur_Manager"].ToString();
                entities.BuyerSend = reader["BuyerSend"].ToString();
                entities.BuyerSendStat = reader["STATBuyerSend"].ToString();
                entities.Posted = reader["Posted"].ToString();
                entities.PostedDate = reader["PostedDate"].ToString();
                entities.SearchCriteria = reader["SearchCriteria"].ToString().ToLower();
                entities.SupplierResponded = reader["SupplierResponded"].ToString();
                entities.StatRemarks = reader["Remarks"].ToString();
                entities.Division = reader["Division"].ToString();
                entities.PostingRemarks = reader["PostingRemarks"].ToString();

                entities.Report_BuyerName = reader["BuyerName"] != null ? CryptorEngine.Decrypt(reader["BuyerName"].ToString().Replace(" ", "+"), true) : string.Empty;


                if (entities.ReqManagerStat == "0")
                {
                    entities.StatAll = "FOR PROD. MNGR APPROVAL";
                    entities.CssColorCode = "#f44336";
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0")
                {
                    entities.StatAll = "FOR SUPPLY CHAIN BUYER APPROVAL";
                    entities.CssColorCode = "#28B463";
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "0")
                {
                    entities.StatAll = "FOR SUPPLY CHAIN INCHARGE APPROVAL";
                    entities.CssColorCode = "#5499C7";
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "1" && entities.BuyerSendStat == "0")
                {
                    entities.StatAll = "FOR SENDING";
                    entities.CssColorCode = "#73C6B6";
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "1" && entities.BuyerSendStat == "1")
                {
                    entities.StatAll = "FOR RESEND";
                    entities.CssColorCode = "#673AB7";
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "1" && entities.BuyerSendStat == "1" && entities.SupplierResponded == "1")
                {
                    entities.StatAll = "SUPPLIER RESPONDED";
                    entities.CssColorCode = "#009688";
                }

                if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurManagerStat == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
                }

                if (entities.Posted == "1")
                {
                    entities.StatAll = "CLOSED";
                    entities.CssColorCode = "#9C27B0";
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

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_AllRequest(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRF_TRANSACTION_AllRequest";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.CrFrom = entity.CrFrom.Length <= 0 ? string.Empty : entity.CrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.CrTo = entity.CrTo.Length <= 0 ? string.Empty : entity.CrTo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

                entities.RefId = reader["RefId"].ToString();
                entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.Supplier = reader["SupplierName"].ToString();
                entities.Requester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true);
                entities.PrNO = reader["PRNO"].ToString();
                entities.PoNO = reader["PONO"].ToString();
                entities.PoDate = reader["PoDate"].ToString();
                entities.Category = reader["Category"].ToString();
                entities.Description = reader["Description"].ToString();
                entities.TypeDrawingNo = reader["TypeDrawingNo"].ToString();
                entities.OrderQuantity = reader["OrderQuantity"].ToString();
                entities.ReasonName = reader["ReasonName"].ToString();
                entities.Reason = reader["Reason"].ToString();
                entities.Attention = reader["Attention"].ToString();

                entities.ReqManagerStat = reader["ReqManager_Stat"].ToString();
                entities.PurInchargeStat = reader["PurIncharge_Stat"].ToString();
                entities.PurManagerStat = reader["PurManager_Stat"].ToString();
                entities.Posted = reader["Posted"].ToString();

                entities.SendDates = reader["SendDates"] != DBNull.Value ? reader["SendDates"].ToString() : string.Empty;

                //-------------------------------------------------------------
                if (reader["ReqManager_Stat"].ToString() == "1")
                {
                    entities.ReqManagerStatColor = "#D5FBC5";
                }
                else if (reader["ReqManager_Stat"].ToString() == "2")
                {
                    entities.ReqManagerStatColor = "#FCBBC1";
                }
                else
                {
                    entities.ReqManagerStatColor = "white";
                }
                //-------------------------------------------------------------
                if (reader["PurIncharge_Stat"].ToString() == "1")
                {
                    entities.PurInchargeStatColor = "#D5FBC5";
                }
                else if (reader["PurIncharge_Stat"].ToString() == "2")
                {
                    entities.PurInchargeStatColor = "#FCBBC1";
                }
                else
                {
                    entities.PurInchargeStatColor = "white";
                }
                //-------------------------------------------------------------
                if (reader["PurManager_Stat"].ToString() == "1")
                {
                    entities.PurManagerStatColor = "#D5FBC5";
                }
                else if (reader["PurManager_Stat"].ToString() == "2")
                {
                    entities.PurManagerStatColor = "#FCBBC1";
                }
                else
                {
                    entities.PurManagerStatColor = "white";
                }
                //-------------------------------------------------------------

                entities.SupplierResponded = reader["SupplierResponded"].ToString();
                entities.BuyerSendStat = reader["STATBuyerSend"].ToString();

                entities.ResponseConfirmedBy = reader["ResponseConfirmedBy"].ToString();
                entities.ResponseDateConfirmed = reader["ResponseDateConfirmed"].ToString();
                entities.ResponseNotes = reader["ResponseNotes"].ToString();

                entities.ReqManager = !string.IsNullOrEmpty(reader["NAME_REQ_MANAGER"].ToString()) ? CryptorEngine.Decrypt(reader["NAME_REQ_MANAGER"].ToString(), true) : string.Empty;
                entities.PurIncharge = !string.IsNullOrEmpty(reader["NAME_INCHARGE"].ToString()) ? CryptorEngine.Decrypt(reader["NAME_INCHARGE"].ToString(), true) : string.Empty;
                entities.PurManager = !string.IsNullOrEmpty(reader["NAME_PUR_MANAGER"].ToString()) ? CryptorEngine.Decrypt(reader["NAME_PUR_MANAGER"].ToString(), true) : string.Empty;

                entities.RequesterSDOA = !string.IsNullOrEmpty(reader["DOARequester"].ToString()) ? DateTime.Parse(reader["DOARequester"].ToString()).ToShortDateString() + " " + DateTime.Parse(reader["DOARequester"].ToString()).ToLongTimeString() : string.Empty;
                entities.ReqManagerDOA = !string.IsNullOrEmpty(reader["DOAReq_Manager"].ToString()) ? reader["DOAReq_Manager"].ToString() : string.Empty;
                entities.PurInchargeDOA = !string.IsNullOrEmpty(reader["DOAPur_Incharge"].ToString()) ? reader["DOAPur_Incharge"].ToString() : string.Empty;
                entities.PurManagerDOA = !string.IsNullOrEmpty(reader["DOAPur_Manager"].ToString()) ? reader["DOAPur_Manager"].ToString() : string.Empty;

                entities.DepartmentName = reader["DepartmentName"].ToString();
                entities.DivisionName = reader["DivisionName"].ToString();

                entities.DateInformedSupplier = reader["DateInformedSupplier"] != DBNull.Value ? reader["DateInformedSupplier"].ToString() : string.Empty;
                entities.DateInformedRequestor = reader["DateInformedRequester"] != DBNull.Value ? reader["DateInformedRequester"].ToString() : string.Empty;

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

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_AllRequest_Like(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRF_TRANSACTION_AllRequest_Like";
            cmd.Parameters.Add(Factory.CreateParameter("@searchcritearia", entity.SearchCriteria = entity.SearchCriteria.Length <= 0 ? string.Empty : entity.SearchCriteria));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

                entities.RefId = reader["RefId"].ToString();
                entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.Supplier = reader["SupplierName"].ToString();
                entities.Requester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true);
                entities.PrNO = reader["PRNO"].ToString();
                entities.PoNO = reader["PONO"].ToString();
                entities.PoDate = reader["PoDate"].ToString();
                entities.Category = reader["Category"].ToString();
                entities.Description = reader["Description"].ToString();
                entities.TypeDrawingNo = reader["TypeDrawingNo"].ToString();
                entities.OrderQuantity = reader["OrderQuantity"].ToString();
                entities.Reason = reader["Reason"].ToString();
                entities.ReasonName = reader["ReasonName"].ToString();
                entities.Attention = reader["Attention"].ToString();

                entities.ReqManagerStat = reader["ReqManager_Stat"].ToString();
                entities.PurInchargeStat = reader["PurIncharge_Stat"].ToString();
                entities.PurManagerStat = reader["PurManager_Stat"].ToString();
                entities.Posted = reader["Posted"].ToString();

                entities.SendDates = reader["SendDates"] != DBNull.Value ? reader["SendDates"].ToString() : string.Empty;

                //-------------------------------------------------------------
                if (reader["ReqManager_Stat"].ToString() == "1")
                {
                    entities.ReqManagerStatColor = "#D5FBC5";
                }
                else if (reader["ReqManager_Stat"].ToString() == "2")
                {
                    entities.ReqManagerStatColor = "#FCBBC1";
                }
                else
                {
                    entities.ReqManagerStatColor = "white";
                }
                //-------------------------------------------------------------
                if (reader["PurIncharge_Stat"].ToString() == "1")
                {
                    entities.PurInchargeStatColor = "#D5FBC5";
                }
                else if (reader["PurIncharge_Stat"].ToString() == "2")
                {
                    entities.PurInchargeStatColor = "#FCBBC1";
                }
                else
                {
                    entities.PurInchargeStatColor = "white";
                }
                //-------------------------------------------------------------
                if (reader["PurManager_Stat"].ToString() == "1")
                {
                    entities.PurManagerStatColor = "#D5FBC5";
                }
                else if (reader["PurManager_Stat"].ToString() == "2")
                {
                    entities.PurManagerStatColor = "#FCBBC1";
                }
                else
                {
                    entities.PurManagerStatColor = "white";
                }
                //-------------------------------------------------------------


                entities.SupplierResponded = reader["SupplierResponded"].ToString();
                entities.BuyerSendStat = reader["STATBuyerSend"].ToString();

                entities.ResponseConfirmedBy = reader["ResponseConfirmedBy"].ToString();
                entities.ResponseDateConfirmed = reader["ResponseDateConfirmed"].ToString();
                entities.ResponseNotes = reader["ResponseNotes"].ToString();

                entities.ReqManager = !string.IsNullOrEmpty(reader["NAME_REQ_MANAGER"].ToString()) ? CryptorEngine.Decrypt(reader["NAME_REQ_MANAGER"].ToString(), true) : string.Empty;
                entities.PurIncharge = !string.IsNullOrEmpty(reader["NAME_INCHARGE"].ToString()) ? CryptorEngine.Decrypt(reader["NAME_INCHARGE"].ToString(), true) : string.Empty;
                entities.PurManager = !string.IsNullOrEmpty(reader["NAME_PUR_MANAGER"].ToString()) ? CryptorEngine.Decrypt(reader["NAME_PUR_MANAGER"].ToString(), true) : string.Empty;

                entities.RequesterSDOA = !string.IsNullOrEmpty(reader["DOARequester"].ToString()) ? reader["DOARequester"].ToString() : string.Empty;
                entities.ReqManagerDOA = !string.IsNullOrEmpty(reader["DOAReq_Manager"].ToString()) ? reader["DOAReq_Manager"].ToString() : string.Empty;
                entities.PurInchargeDOA = !string.IsNullOrEmpty(reader["DOAPur_Incharge"].ToString()) ? reader["DOAPur_Incharge"].ToString() : string.Empty;
                entities.PurManagerDOA = !string.IsNullOrEmpty(reader["DOAPur_Manager"].ToString()) ? reader["DOAPur_Manager"].ToString() : string.Empty;

                entities.DepartmentName = reader["DepartmentName"].ToString();
                entities.DivisionName = reader["DivisionName"].ToString();

                entities.DateInformedSupplier = reader["DateInformedSupplier"] != DBNull.Value ? reader["DateInformedSupplier"].ToString() : string.Empty;
                entities.DateInformedRequestor = reader["DateInformedRequester"] != DBNull.Value ? reader["DateInformedRequester"].ToString() : string.Empty;

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

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_AllRequest_ByCTRLNo(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRF_TRANSACTION_AllRequest_ByCTRLNo";
            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

                entities.RefId = reader["RefId"].ToString();
                entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.Attention = reader["Attention"].ToString().ToUpper();
                entities.Supplier = reader["SupplierName"].ToString();
                entities.Requester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true);
                entities.PrNO = reader["PRNO"].ToString();
                entities.PoNO = reader["PONO"].ToString();
                entities.PoDate = reader["PoDate"].ToString();
                entities.Category = reader["Category"].ToString();
                entities.Description = reader["Description"].ToString();
                entities.TypeDrawingNo = reader["TypeDrawingNo"].ToString();
                entities.OrderQuantity = reader["OrderQuantity"].ToString();
                entities.Reason = reader["Reason"].ToString();
                entities.ReasonName = reader["ReasonName"].ToString();
                entities.ResponseConfirmedBy = reader["ConfirmedBy"].ToString();
                entities.ResponseDateConfirmed = reader["DateConfirmed"].ToString();
                entities.ResponseNotes = reader["Notes"].ToString();

                entities.ReqManagerStat = reader["ReqManager_Stat"].ToString();
                entities.PurInchargeStat = reader["PurIncharge_Stat"].ToString();
                entities.PurManagerStat = reader["PurManager_Stat"].ToString();
                entities.SupplierResponded = reader["SupplierResponded"].ToString();

                entities.RequesterS = CryptorEngine.Decrypt(reader["PreparedBy"].ToString(), true);
                entities.ReqManager = CryptorEngine.Decrypt(reader["NotedBy"].ToString(), true);
                entities.PurIncharge = CryptorEngine.Decrypt(reader["InchargeBy"].ToString(), true);
                entities.PurManager = CryptorEngine.Decrypt(reader["ManagerBy"].ToString(), true);

                entities.DateInformedSupplier = reader["DateInformedSupplier"] != DBNull.Value ? reader["DateInformedSupplier"].ToString() : string.Empty;
                entities.DateInformedRequestor = reader["DateInformedRequester"] != DBNull.Value ? reader["DateInformedRequester"].ToString() : string.Empty;


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

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_AllRequest_ByCTRLNo2(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRF_TRANSACTION_AllRequest_ByCTRLNo2";
            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

                entities.RdRefId = reader["RefId"].ToString();
                entities.RdCtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.RdPODate = reader["PoDate"].ToString().Replace("12:00:00 am", string.Empty);
                entities.RdPRNO = reader["PRNO"].ToString();
                entities.RdPONO = reader["PONO"].ToString();
                entities.RdItemName = reader["Description"].ToString();
                entities.RdSpecs = reader["TypeDrawingNo"].ToString();
                entities.RdQuantity = reader["OrderQuantity"].ToString();
                entities.RdReason = reader["Reason"].ToString();
                entities.RdUnitOfMeasure = reader["UnitOfMeasure"].ToString();
                entities.RdReasonName = reader["ReasonName"].ToString();
                entities.RdUOMDesc = reader["UOMDesc"].ToString();
                entities.RdAttachment = reader["Attachment1"].ToString();
                entities.ResponseConfirmedBy = reader["ConfirmedBy"].ToString();
                entities.ResponseDateConfirmed = reader["DateConfirmed"].ToString();
                entities.ResponseNotes = reader["Notes"].ToString();   

                entities.RdDateInformedSupplier = reader["DateInformedSupplier"] != DBNull.Value ? reader["DateInformedSupplier"].ToString() : string.Empty;
                entities.RdDateInformedRequester = reader["DateInformedRequester"] != DBNull.Value ? reader["DateInformedRequester"].ToString() : string.Empty;

                entities.Requester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true);
                entities.Attention = reader["Attention"].ToString();
                entities.SupplierName = reader["Supplier"].ToString();
                entities.Category = reader["CategoryName"].ToString();

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

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_Approval_DateRange(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRF_TRANSACTION_Approval_DateRange";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.CrFrom = entity.CrFrom.Length <= 0 ? string.Empty : entity.CrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.CrTo = entity.CrTo.Length <= 0 ? string.Empty : entity.CrTo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

                entities.RefId = reader["RefId"].ToString();
                entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.Attention = reader["Attention"].ToString().ToUpper();
                entities.Supplier = reader["SupplierName"].ToString();
                entities.Requester = CryptorEngine.Decrypt(reader["RequesterName"].ToString(), true);
                entities.SupplierEmail = reader["SupplierEmail"].ToString();
                entities.PrNO = reader["PRNO"].ToString();
                entities.PoNO = reader["PONO"].ToString();
                entities.PoDate = reader["PoDate"].ToString();
                entities.Category = reader["CategoryName"].ToString();
                entities.CategoryId = reader["Category"].ToString();
                entities.Description = reader["Description"].ToString();
                entities.TypeDrawingNo = reader["TypeDrawingNo"].ToString();
                entities.OrderQuantity = reader["OrderQuantity"].ToString();
                entities.Reason = reader["Reason"].ToString();
                entities.ReasonName = reader["ReasonName"].ToString();
                entities.ReasonName = reader["ReasonName"].ToString();
                entities.LcDepartment = reader["Department"].ToString();

                entities.ReqManagerStat = reader["STATReq_Manager"].ToString();
                entities.PurInchargeStat = reader["STATPur_Incharge"].ToString();
                entities.PurManagerStat = reader["STATPur_Manager"].ToString();
                entities.SupplierResponded = reader["SupplierResponded"].ToString();
                entities.SupplierResponseDate = reader["SupplierResponseDate"].ToString();
                entities.BuyerSendStat = reader["STATBuyerSend"].ToString();
                entities.Posted = reader["Posted"].ToString();
                entities.PostedBy = reader["PostedBy"].ToString();
                entities.PostedDate = reader["PostedDate"].ToString();

                entities.Attachment1 = reader["Attachment1"].ToString();
                entities.Attachment2 = reader["Attachment2"].ToString();
                entities.Attachment3 = reader["Attachment3"].ToString();
                entities.Attachment4 = reader["Attachment4"].ToString();

                entities.RequesterS = reader["PreparedBy"].ToString().Length > 0 ? CryptorEngine.Decrypt(reader["PreparedBy"].ToString(), true) : string.Empty;
                entities.ReqManager = reader["NotedBy"].ToString().Length > 0 ? CryptorEngine.Decrypt(reader["NotedBy"].ToString(), true) : string.Empty;
                entities.PurIncharge = reader["InchargeBy"].ToString().Length > 0 ? CryptorEngine.Decrypt(reader["InchargeBy"].ToString(), true) : string.Empty;
                entities.PurManager = reader["ManagerBy"].ToString().Length > 0 ? CryptorEngine.Decrypt(reader["ManagerBy"].ToString(), true) : string.Empty;

                entities.RequesterSDOA = !string.IsNullOrEmpty(reader["DOARequester"].ToString()) ? DateTime.Parse(reader["DOARequester"].ToString()).ToShortDateString() + " " + DateTime.Parse(reader["DOARequester"].ToString()).ToLongTimeString() : string.Empty;
                entities.ReqManagerDOA = !string.IsNullOrEmpty(reader["DOAReq_Manager"].ToString()) ? reader["DOAReq_Manager"].ToString() : string.Empty;
                entities.PurInchargeDOA = !string.IsNullOrEmpty(reader["DOAPur_Incharge"].ToString()) ? reader["DOAPur_Incharge"].ToString() : string.Empty;
                entities.PurManagerDOA = !string.IsNullOrEmpty(reader["DOAPur_Manager"].ToString()) ? reader["DOAPur_Manager"].ToString() : string.Empty;

                entities.DepartmentName = reader["DepartmentName"].ToString();
                entities.DivisionName = reader["DivisionName"].ToString();

                entities.ResponseConfirmedBy = reader["ResponseConfirmedBy"].ToString();
                entities.ResponseDateConfirmed = reader["ResponseDateConfirmed"].ToString();
                entities.ResponseNotes = reader["ResponseNotes"].ToString();

                entities.DateInformedSupplier = reader["DateInformedSupplier"] != DBNull.Value ? reader["DateInformedSupplier"].ToString() : string.Empty;
                entities.DateInformedRequestor = reader["DateInformedRequester"] != DBNull.Value ? reader["DateInformedRequester"].ToString() : string.Empty;

                if (entities.ReqManagerStat == "0")
                {
                    entities.StatAll = "FOR PROD. MNGR APPROVAL";
                    entities.CssColorCode = "#f44336";
                }
                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0")
                {
                    entities.StatAll = "FOR BUYER APPROVAL";
                    entities.CssColorCode = "#9C27B0";
                }
                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "0")
                {
                    entities.StatAll = "FOR PUR. MNGR APPROVAL";
                    entities.CssColorCode = "#673AB7";
                }
                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "1")
                {
                    entities.StatAll = "FOR SENDING";
                    entities.CssColorCode = "#009688";
                }
                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "1" && entities.BuyerSendStat == "1")
                {
                    entities.StatAll = "FOR RESEND";
                    entities.CssColorCode = "#8BC34A";
                }
                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "1" && entities.BuyerSendStat == "1" && entities.SupplierResponded == "1")
                {
                    entities.StatAll = "SUPPLIER RESPONDED";
                    entities.CssColorCode = "#673AB7";
                }
                if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurManagerStat == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
                }
                if (entities.Posted == "1")
                {
                    entities.StatAll = "APPROVED";
                    entities.CssColorCode = "#00C851";
                }


                //-------------------------------------------------------------
                if (reader["ReqManager_Stat"].ToString() == "1")
                {
                    entities.ReqManagerStatColor = "#D5FBC5";
                }
                else if (reader["ReqManager_Stat"].ToString() == "2")
                {
                    entities.ReqManagerStatColor = "#FCBBC1";
                }
                else
                {
                    entities.ReqManagerStatColor = "white";
                }
                //-------------------------------------------------------------
                if (reader["PurIncharge_Stat"].ToString() == "1")
                {
                    entities.PurInchargeStatColor = "#D5FBC5";
                }
                else if (reader["PurIncharge_Stat"].ToString() == "2")
                {
                    entities.PurInchargeStatColor = "#FCBBC1";
                }
                else
                {
                    entities.PurInchargeStatColor = "white";
                }
                //-------------------------------------------------------------
                if (reader["PurManager_Stat"].ToString() == "1")
                {
                    entities.PurManagerStatColor = "#D5FBC5";
                }
                else if (reader["PurManager_Stat"].ToString() == "2")
                {
                    entities.PurManagerStatColor = "#FCBBC1";
                }
                else
                {
                    entities.PurManagerStatColor = "white";
                }
                //-------------------------------------------------------------


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

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_Approval_DateRange_New(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        string CTRLNO = string.Empty;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRF_TRANSACTION_Approval_DateRange_New";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.CrFrom = entity.CrFrom.Length <= 0 ? string.Empty : entity.CrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.CrTo = entity.CrTo.Length <= 0 ? string.Empty : entity.CrTo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

                entities.RefId = reader["RefId"].ToString();
                entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.TransactionDate = reader["TransactionDate"].ToString().ToUpper();
                entities.RequesterS = reader["Requester"].ToString();
                entities.ReqManager = reader["Req_Manager"].ToString();
                entities.PurIncharge = reader["PurIncharge"].ToString();
                entities.PurManager = reader["Pur_Manager"].ToString();
                entities.RequesterSDOA = reader["DOARequester"].ToString();
                entities.ReqManagerDOA = reader["DOAReq_Manager"].ToString();
                entities.PurInchargeDOA = reader["DOAPur_Incharge"].ToString();
                entities.PurManagerDOA = reader["DOAPur_Manager"].ToString();
                entities.RequesterSStat = reader["STATRequester"].ToString();
                entities.ReqManagerStat = reader["STATReq_Manager"].ToString();
                entities.PurInchargeStat = reader["STATPur_Incharge"].ToString();
                entities.PurManagerStat = reader["STATPur_Manager"].ToString();
                entities.BuyerSend = reader["BuyerSend"].ToString();
                entities.BuyerSendDOA = reader["DOABuyerSend"].ToString();
                entities.BuyerSendStat = reader["STATBuyerSend"].ToString();
                entities.StatRemarks = reader["StatRemarks"].ToString();
                entities.SupplierResponded = reader["SupplierResponded"].ToString();
                entities.CategoryId = reader["Category"].ToString();
                entities.Category = reader["CategoryName"].ToString();
                entities.Posted = reader["Posted"].ToString();
                entities.StatRemarks = reader["StatRemarks"].ToString();
                entities.LcDepartment = reader["Department"].ToString();
                entities.LcDivision = reader["Division"].ToString();
                entities.Requester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true);
                entities.Supplier = reader["Supplier"].ToString();
                entities.SupplierEmail = reader["SupplierEmail"].ToString();
                entities.Attention = reader["Attention"].ToString().ToUpper();
                entities.SupplierAttachment = reader["SupplierAttachment"].ToString();
                entities.BuyerRemarks = reader["BuyerRemarks"].ToString();

                entities.ForSending = reader["ForSending"] != DBNull.Value ? reader["ForSending"].ToString() : "0";

                if (entities.ReqManagerStat == "0")
                {
                    entities.StatAll = "FOR PROD. MNGR APPROVAL";
                    entities.CssColorCode = "#f44336";
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0")
                {
                    entities.StatAll = "FOR SUPPLY CHAIN BUYER APPROVAL";
                    entities.CssColorCode = "#9C27B0";
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "0")
                {
                    entities.StatAll = "FOR SUPPLY CHAIN INCHARGE APPROVAL";
                    entities.CssColorCode = "#673AB7";
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "1" && entities.BuyerSendStat == "0")
                {
                    if (entities.ForSending == "1" || entities.ForSending == "0")
                    {
                        entities.StatAll = "FOR SENDING";
                        entities.CssColorCode = "#8BC34A";
                    }
                    if (entities.ForSending == "2")
                    {
                        entities.StatAll = "NOT FOR SENDING";
                        entities.CssColorCode = "#E91E63";
                    }
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "1" && entities.BuyerSendStat == "1")
                {
                    entities.StatAll = "FOR RESEND";
                    entities.CssColorCode = "#673AB7";
                }

                if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "1" && entities.BuyerSendStat == "1" && entities.SupplierResponded == "1")
                {
                    entities.StatAll = "SUPPLIER RESPONDED";
                    entities.CssColorCode = "#009688";
                }

                if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurManagerStat == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
                }

                if (entities.Posted == "1")
                {
                    entities.StatAll = "CLOSED";
                    entities.CssColorCode = "#9C27B0";
                }

                //entities.RefId = reader["RefId"].ToString();
                //entities.CtrlNo = reader["CtrlNo"].ToString().ToUpper();
                //entities.Attention = reader["Attention"].ToString().ToUpper();
                //entities.Supplier = reader["SupplierName"].ToString();
                //entities.Requester = CryptorEngine.Decrypt(reader["RequesterName"].ToString(), true);
                //entities.SupplierEmail = reader["SupplierEmail"].ToString();
                //entities.PrNO = reader["PRNO"].ToString();
                //entities.PoNO = reader["PONO"].ToString();
                //entities.PoDate = reader["PoDate"].ToString();
                //entities.Category = reader["CategoryName"].ToString();
                //entities.CategoryId = reader["Category"].ToString();
                //entities.Description = reader["Description"].ToString();
                //entities.TypeDrawingNo = reader["TypeDrawingNo"].ToString();
                //entities.OrderQuantity = reader["OrderQuantity"].ToString();
                //entities.Reason = reader["Reason"].ToString();
                //entities.ReasonName = reader["ReasonName"].ToString();
                //entities.ReasonName = reader["ReasonName"].ToString();
                //entities.LcDepartment = reader["Department"].ToString();

                //entities.ReqManagerStat = reader["STATReq_Manager"].ToString();
                //entities.PurInchargeStat = reader["STATPur_Incharge"].ToString();
                //entities.PurManagerStat = reader["STATPur_Manager"].ToString();
                //entities.SupplierResponded = reader["SupplierResponded"].ToString();
                //entities.SupplierResponseDate = reader["SupplierResponseDate"].ToString();
                //entities.BuyerSendStat = reader["STATBuyerSend"].ToString();
                //entities.Posted = reader["Posted"].ToString();
                //entities.PostedBy = reader["PostedBy"].ToString();
                //entities.PostedDate = reader["PostedDate"].ToString();

                //entities.Attachment1 = reader["Attachment1"].ToString();
                //entities.Attachment2 = reader["Attachment2"].ToString();
                //entities.Attachment3 = reader["Attachment3"].ToString();
                //entities.Attachment4 = reader["Attachment4"].ToString();

                //entities.RequesterS = reader["PreparedBy"].ToString().Length > 0 ? CryptorEngine.Decrypt(reader["PreparedBy"].ToString(), true) : string.Empty;
                //entities.ReqManager = reader["NotedBy"].ToString().Length > 0 ? CryptorEngine.Decrypt(reader["NotedBy"].ToString(), true) : string.Empty;
                //entities.PurIncharge = reader["InchargeBy"].ToString().Length > 0 ? CryptorEngine.Decrypt(reader["InchargeBy"].ToString(), true) : string.Empty;
                //entities.PurManager = reader["ManagerBy"].ToString().Length > 0 ? CryptorEngine.Decrypt(reader["ManagerBy"].ToString(), true) : string.Empty;

                //entities.RequesterSDOA = !string.IsNullOrEmpty(reader["DOARequester"].ToString()) ? DateTime.Parse(reader["DOARequester"].ToString()).ToShortDateString() + " " + DateTime.Parse(reader["DOARequester"].ToString()).ToLongTimeString() : string.Empty;
                //entities.ReqManagerDOA = !string.IsNullOrEmpty(reader["DOAReq_Manager"].ToString()) ? reader["DOAReq_Manager"].ToString() : string.Empty;
                //entities.PurInchargeDOA = !string.IsNullOrEmpty(reader["DOAPur_Incharge"].ToString()) ? reader["DOAPur_Incharge"].ToString() : string.Empty;
                //entities.PurManagerDOA = !string.IsNullOrEmpty(reader["DOAPur_Manager"].ToString()) ? reader["DOAPur_Manager"].ToString() : string.Empty;

                //entities.DepartmentName = reader["DepartmentName"].ToString();
                //entities.DivisionName = reader["DivisionName"].ToString();

                //entities.ResponseConfirmedBy = reader["ResponseConfirmedBy"].ToString();
                //entities.ResponseDateConfirmed = reader["ResponseDateConfirmed"].ToString();
                //entities.ResponseNotes = reader["ResponseNotes"].ToString();

                //entities.DateInformedSupplier = reader["DateInformedSupplier"] != DBNull.Value ? reader["DateInformedSupplier"].ToString() : string.Empty;
                //entities.DateInformedRequestor = reader["DateInformedRequester"] != DBNull.Value ? reader["DateInformedRequester"].ToString() : string.Empty;

                //if (entities.ReqManagerStat == "0")
                //{
                //    entities.StatAll = "FOR PROD. MNGR APPROVAL";
                //    entities.CssColorCode = "#f44336";
                //}
                //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "0")
                //{
                //    entities.StatAll = "FOR BUYER APPROVAL";
                //    entities.CssColorCode = "#9C27B0";
                //}
                //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "0")
                //{
                //    entities.StatAll = "FOR PUR. MNGR APPROVAL";
                //    entities.CssColorCode = "#673AB7";
                //}
                //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "1")
                //{
                //    entities.StatAll = "FOR SENDING";
                //    entities.CssColorCode = "#009688";
                //}
                //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "1" && entities.BuyerSendStat == "1")
                //{
                //    entities.StatAll = "FOR RESEND";
                //    entities.CssColorCode = "#8BC34A";
                //}
                //if (entities.ReqManagerStat == "1" && entities.PurInchargeStat == "1" && entities.PurManagerStat == "1" && entities.BuyerSendStat == "1" && entities.SupplierResponded == "1")
                //{
                //    entities.StatAll = "SUPPLIER RESPONDED";
                //    entities.CssColorCode = "#673AB7";
                //}
                //if (entities.ReqManagerStat == "2" || entities.PurInchargeStat == "2" || entities.PurManagerStat == "2")
                //{
                //    entities.StatAll = "DISAPPROVED";
                //    entities.CssColorCode = "#ffbb33";
                //}
                //if (entities.Posted == "1")
                //{
                //    entities.StatAll = "APPROVED";
                //    entities.CssColorCode = "#00C851";
                //}


                ////-------------------------------------------------------------
                //if (reader["ReqManager_Stat"].ToString() == "1")
                //{
                //    entities.ReqManagerStatColor = "#D5FBC5";
                //}
                //else if (reader["ReqManager_Stat"].ToString() == "2")
                //{
                //    entities.ReqManagerStatColor = "#FCBBC1";
                //}
                //else
                //{
                //    entities.ReqManagerStatColor = "white";
                //}
                ////-------------------------------------------------------------
                //if (reader["PurIncharge_Stat"].ToString() == "1")
                //{
                //    entities.PurInchargeStatColor = "#D5FBC5";
                //}
                //else if (reader["PurIncharge_Stat"].ToString() == "2")
                //{
                //    entities.PurInchargeStatColor = "#FCBBC1";
                //}
                //else
                //{
                //    entities.PurInchargeStatColor = "white";
                //}
                ////-------------------------------------------------------------
                //if (reader["PurManager_Stat"].ToString() == "1")
                //{
                //    entities.PurManagerStatColor = "#D5FBC5";
                //}
                //else if (reader["PurManager_Stat"].ToString() == "2")
                //{
                //    entities.PurManagerStatColor = "#FCBBC1";
                //}
                //else
                //{
                //    entities.PurManagerStatColor = "white";
                //}
                ////-------------------------------------------------------------

                if (CTRLNO.ToUpper().Trim() != reader["CtrlNo"].ToString().ToUpper().Trim())
                {
                    list.Add(entities);
                    CTRLNO = reader["CtrlNo"].ToString().ToUpper().Trim();
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

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_DataLinkToPR_ByPONO(string pono)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TOP 7 * FROM [ROPROS].[dbo].[vewRFQDelivery] WITH (NOLOCK) WHERE PONO LIKE '%" + pono + "%'";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

                entities.PoNO = reader["PONO"].ToString();
                entities.PrNO = reader["PRNO"].ToString();
                entities.Description = reader["ITEM"].ToString();
                entities.TypeDrawingNo = reader["SPECIFICATION"].ToString();
                entities.OrderQuantity = reader["REMAINQTY"].ToString();
                entities.UnitOfMeasure = reader["UOM"].ToString();
                entities.PoDate = reader["PoDate"].ToString();

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

    public List<Entities_CRF_RequestEntry> vewRFQDelivery(string query, string dataIndex)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection_Data_Link();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

                entities.RdPONO = reader["PONO"] != DBNull.Value ? reader["PONO"].ToString() : string.Empty;
                entities.RdPRNO = reader["PRNO"] != DBNull.Value ? reader["PRNO"].ToString() : string.Empty;
                entities.RdItemName = reader["Item"] != DBNull.Value ? reader["Item"].ToString().ToUpper() : string.Empty;
                entities.RdSpecs = reader["Specification"] != DBNull.Value ? reader["Specification"].ToString().ToUpper() : string.Empty;
                entities.RdQuantity = reader["RemainQTY"] != DBNull.Value ? reader["RemainQTY"].ToString() : string.Empty;
                entities.RdUnitOfMeasure = reader["UOM"] != DBNull.Value ? reader["UOM"].ToString() : string.Empty;
                //entities.RdDeliveryConfirmationDate = reader["ReplyDLVDate"] != DBNull.Value ? reader["ReplyDLVDate"].ToString() : string.Empty;
                entities.RdPODate = reader["PODate"] != DBNull.Value ? reader["PODate"].ToString() : string.Empty;
                entities.DataIndex = dataIndex;

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

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_DataLinkToPR_ByPRNO(string prno)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TOP 7 * FROM [ROPROS].[dbo].[vewRFQDelivery] WITH (NOLOCK) WHERE PRNO LIKE '%" + prno + "%'";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

                entities.PoNO = reader["PONO"].ToString();
                entities.PrNO = reader["PRNO"].ToString();
                entities.Description = reader["ITEM"].ToString();
                entities.TypeDrawingNo = reader["SPECIFICATION"].ToString();
                entities.OrderQuantity = reader["REMAINQTY"].ToString();
                entities.UnitOfMeasure = reader["UOM"].ToString();
                entities.PoDate = reader["PoDate"].ToString();

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

    public int CRF_TRANSACTION_SQLTransaction(string query)
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

    public List<Entities_CRF_RequestEntry> CRF_TRANSACTION_GetSupplierAttachmentByCTRLNo(Entities_CRF_RequestEntry entity)
    {
        List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRF_TRANSACTION_GetSupplierAttachmentByCTRLNo";
            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.CtrlNo = entity.CtrlNo.Length <= 0 ? string.Empty : entity.CtrlNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_CRF_RequestEntry entities = new Entities_CRF_RequestEntry();

                entities.CtrlNo = reader["CTRLNo"].ToString();
                entities.SupplierAttachment = reader["AttachmentFile"].ToString();

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
