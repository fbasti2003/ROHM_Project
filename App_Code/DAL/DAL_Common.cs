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

public class DAL_Common
{
    public DAL_Common()
    {
    }

    public List<Entities_Common_SystemUsers> getFormList()
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Common_getFormList";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_SystemUsers entities = new Entities_Common_SystemUsers();

                entities.FormList_RefId = reader["RefId"].ToString();
                entities.FormList_AccessValue = reader["AccessValue"].ToString();
                entities.FormList_FormName = reader["FormName"].ToString();
                entities.FormList_FormType = reader["FormType"].ToString();
                entities.FormList_IsDefault = reader["IsDefault"].ToString();
                entities.FormList_OrderDisplay = reader["OrderDisplay"].ToString();
                if (reader["isAllowed"] == DBNull.Value)
                {
                    entities.FormList_IsAllowed = "0";
                }
                else
                {
                    entities.FormList_IsAllowed = reader["isAllowed"].ToString();
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

    public List<Entities_Common_ForApproval> Common_GetForApprovalByCategoryId(string category, string position, string department, string division, string hq, string isDivision, string isHQ)
    {
        List<Entities_Common_ForApproval> list = new List<Entities_Common_ForApproval>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        Common COMMON = new Common();

        int countForURF = 0;
        int countForERFO = 0;
        int countForERFO_Confirmed = 0;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Common_GetForApprovalByCategoryId";
            cmd.Parameters.Add(Factory.CreateParameter("@category", category = string.IsNullOrEmpty(category) ? "0" : category ));
            cmd.Parameters.Add(Factory.CreateParameter("@position", position = string.IsNullOrEmpty(position) ? string.Empty : position));
            cmd.Parameters.Add(Factory.CreateParameter("@department", department = string.IsNullOrEmpty(department) ? string.Empty : department));
            cmd.Parameters.Add(Factory.CreateParameter("@division", division = string.IsNullOrEmpty(division) ? string.Empty : division));
            cmd.Parameters.Add(Factory.CreateParameter("@hq", hq = string.IsNullOrEmpty(hq) ? string.Empty : hq));
            cmd.Parameters.Add(Factory.CreateParameter("@isDivision", isDivision = string.IsNullOrEmpty(isDivision) ? string.Empty : isDivision));
            cmd.Parameters.Add(Factory.CreateParameter("@isHQ", isHQ = string.IsNullOrEmpty(isHQ) ? string.Empty : isHQ));
            cmd.CommandTimeout = 500;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_ForApproval entities = new Entities_Common_ForApproval();

                entities.TransactionName = reader["Name"].ToString();
                entities.ForApprovalCount = COMMON.formatNumber(decimal.Parse(reader["RequestCount"].ToString()), 0);

                //// URGENT REQUEST ====================================================================
                //if (reader["Name"].ToString() == "URGENT REQUEST FORM (DEPT. MANAGER)")
                //{
                //    countForURF = countForURF + int.Parse(entities.ForApprovalCount);
                //    entities.TransactionName = string.Empty;
                //    entities.ForApprovalCount = COMMON.formatNumber(decimal.Parse(countForURF.ToString()), 0);
                //}

                //if (reader["Name"].ToString() == "URGENT REQUEST FORM (DIVISION MANAGER)")
                //{
                //    countForURF = countForURF + int.Parse(entities.ForApprovalCount);
                //    entities.TransactionName = string.Empty;
                //    entities.ForApprovalCount = COMMON.formatNumber(decimal.Parse(countForURF.ToString()), 0);
                //}

                //if (reader["Name"].ToString() == "URGENT REQUEST FORM (HQ. MANAGER)")
                //{
                //    countForURF = countForURF + int.Parse(entities.ForApprovalCount);
                //    entities.TransactionName = string.Empty;
                //    entities.ForApprovalCount = COMMON.formatNumber(decimal.Parse(countForURF.ToString()), 0);
                //}

                //if (reader["Name"].ToString() == "URGENT REQUEST FORM (SEC. MANAGER)")
                //{
                //    countForURF = countForURF + int.Parse(entities.ForApprovalCount);
                //    entities.TransactionName = "URGENT REQUEST FORM";
                //    entities.ForApprovalCount = COMMON.formatNumber(decimal.Parse(countForURF.ToString()), 0);
                //}
                ////=====================================================================================


                //// EQUIPMENT REQUEST ====================================================================
                //if (reader["Name"].ToString() == "EQUIPMENT REQUEST FOR OPERATION (DEPT. MANAGER)")
                //{
                //    countForERFO = countForERFO + int.Parse(entities.ForApprovalCount);
                //    entities.TransactionName = string.Empty;
                //    entities.ForApprovalCount = COMMON.formatNumber(decimal.Parse(countForERFO.ToString()), 0);
                //}

                //if (reader["Name"].ToString() == "EQUIPMENT REQUEST FOR OPERATION (DIV. MANAGER)")
                //{
                //    countForERFO = countForERFO + int.Parse(entities.ForApprovalCount);
                //    entities.TransactionName = "EQUIPMENT REQUEST FOR OPERATION";
                //    entities.ForApprovalCount = COMMON.formatNumber(decimal.Parse(countForERFO.ToString()), 0);
                //}

                //if (reader["Name"].ToString() == "EQUIPMENT REQUEST FOR OPERATION (INCHARGE-CONFIRMED)")
                //{
                //    entities.TransactionName = "EQUIPMENT REQUEST FOR OPERATION";
                //}

                //if (reader["Name"].ToString() == "EQUIPMENT REQUEST FOR OPERATION (REQUESTER-CONFIRMED)")
                //{
                //    entities.TransactionName = "EQUIPMENT REQUEST FOR OPERATION";
                //}

                //if (reader["Name"].ToString() == "EQUIPMENT REQUEST FOR OPERATION (INCHARGE)")
                //{
                //    entities.TransactionName = "EQUIPMENT REQUEST FOR OPERATION";
                //}

                ////=====================================================================================




                //if (int.Parse(entities.ForApprovalCount) > 0 && !string.IsNullOrEmpty(entities.TransactionName))
                //{
                //    list.Add(entities);
                //}

                if (int.Parse(entities.ForApprovalCount) > 0)
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

    public List<Entities_Common_ForApproval> Common_GetForApprovals()
    {
        List<Entities_Common_ForApproval> list = new List<Entities_Common_ForApproval>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        Common COMMON = new Common();

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Common_GetForApprovals";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_ForApproval entities = new Entities_Common_ForApproval();

                entities.Approval_RefId = reader["RefId"].ToString();
                entities.Approval_Fullname = reader["Fullname"].ToString();
                entities.Approval_EmailAddress = reader["EmailAddress"].ToString();
                entities.Approval_Section = reader["Section"].ToString();
                entities.Approval_Department = reader["Department"].ToString();
                entities.Approval_Division= reader["Division"].ToString();
                entities.Approval_HQ = reader["HQ"].ToString();
                entities.Approval_CRF_ForApproval = reader["CRF_ForApproval"].ToString();
                entities.Approval_DRF_ForApproval = reader["DRF_ForApproval"].ToString();
                entities.Approval_URF_ForApproval_ProdSecManager = reader["URF_ForApproval_ProdSecManager"].ToString();
                entities.Approval_URF_ForApproval_ProdDeptManager = reader["URF_ForApproval_ProdDeptManager"].ToString();
                entities.Approval_URF_ForApproval_ProdDivManager = reader["URF_ForApproval_ProdDivManager"].ToString();
                entities.Approval_URF_ForApproval_ProdHQManager = reader["URF_ForApproval_ProdHQManager"].ToString();


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

    public List<Entities_Common_ForApproval> Common_GetForApprovals2(string category)
    {
        List<Entities_Common_ForApproval> list = new List<Entities_Common_ForApproval>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        Common COMMON = new Common();

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Common_GetForApprovals2";
            cmd.Parameters.Add(Factory.CreateParameter("@category", category = string.IsNullOrEmpty(category) ? string.Empty : category));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_ForApproval entities = new Entities_Common_ForApproval();

                entities.Approval_RefId = reader["RefId"].ToString();
                entities.Approval_Fullname = reader["Fullname"].ToString();
                entities.Approval_EmailAddress = reader["EmailAddress"].ToString();
                entities.Approval_Section = reader["Section"].ToString();
                entities.Approval_Department = reader["Department"].ToString();
                entities.Approval_CRF_ForApproval = reader["CRF_ForApproval"].ToString();
                entities.Approval_DRF_ForApproval = reader["DRF_ForApproval"].ToString();
                entities.Approval_URF_ForApproval_ProdSecManager = reader["URF_ForApproval_ProdSecManager"].ToString();
                entities.Approval_URF_ForApproval_ProdDeptManager = reader["URF_ForApproval_ProdDeptManager"].ToString();
                entities.Approval_URF_ForApproval_ProdDivManager = reader["URF_ForApproval_ProdDivManager"].ToString();
                entities.Approval_URF_ForApproval_ProdHQManager = reader["URF_ForApproval_ProdHQManager"].ToString();
                entities.Approval_URF_ForApproval_Buyer = reader["URF_ForApproval_Buyer"].ToString();
                entities.Approval_URF_ForApproval_PurchasingManager = reader["URF_ForApproval_PurchasingManager"].ToString();

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

    public List<Entities_Common_ForApproval> Common_GetThisWeekRequestStatus()
    {
        List<Entities_Common_ForApproval> list = new List<Entities_Common_ForApproval>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        Common COMMON = new Common();

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Common_GetThisWeekRequestStatus";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_ForApproval entities = new Entities_Common_ForApproval();

                entities.Num = reader["Num"].ToString();
                entities.TransactionName = reader["Name"].ToString();
                entities.ThisWeek_Request = COMMON.formatNumber(decimal.Parse(reader["Request"].ToString()), 0);
                entities.ThisWeek_Pending = COMMON.formatNumber(decimal.Parse(reader["Pending"].ToString()), 0);
                entities.ThisWeek_Approved = COMMON.formatNumber(decimal.Parse(reader["Approved"].ToString()), 0);
                entities.ThiwWeek_Disapproved = COMMON.formatNumber(decimal.Parse(reader["Disapproved"].ToString()), 0);
                entities.Percentage_Request = (((decimal.Parse(reader["Request"].ToString()) / 503) * 100) + 20).ToString() + "%";
                entities.Percentage_Pending = (((decimal.Parse(reader["Pending"].ToString()) / 503) * 100) + 20).ToString() + "%";
                entities.Percentage_Approved = (((decimal.Parse(reader["Approved"].ToString()) / 503) * 100) + 20).ToString() + "%";
                entities.Percentage_Disapproved = (((decimal.Parse(reader["Disapproved"].ToString()) / 503) * 100) + 20).ToString() + "%";


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

    public List<Entities_Common_ForApproval> Common_RFQ_GetOtherBuyerItems()
    {
        List<Entities_Common_ForApproval> list = new List<Entities_Common_ForApproval>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        Common COMMON = new Common();

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT * FROM " +
            //                    "( " +
            //                    "SELECT DISTINCT 'FOR_SENDING' AS Name, COUNT(DISTINCT(HEAD.RFQNo)) AS RequestCount, HEAD.Category, " +
            //                    "(SELECT UPPER(Description) FROM MT_Category WITH (NOLOCK) WHERE RefId = HEAD.Category) AS CategoryName " +
            //                    "FROM Request_Head HEAD WITH (NOLOCK) " +
            //                    "INNER JOIN Request_Details DETAILS WITH (NOLOCK) ON HEAD.RFQNo = DETAILS.RFQNo " +
            //                    "INNER JOIN MT_Category CATEGORY WITH (NOLOCK) ON HEAD.Category = CATEGORY.RefId " +
            //                    "INNER JOIN vew_RequestHistOfApproval RHOA with (nolock) ON HEAD.RFQNo  = RHOA.RFQNo " +
            //                    "INNER JOIN Request_Status [STATUS] WITH (NOLOCK) ON HEAD.RFQNo = [STATUS].RFQNo " +
            //                    "INNER JOIN Login_Credentials LC WITH (NOLOCK) ON HEAD.Requester = LC.RefId " +
            //                    "LEFT JOIN vew_SupplierResponseCount SRC WITH (NOLOCK) ON HEAD.RFQNo = SRC.RFQNo " +
            //                    "WHERE [STATUS].ProdManager = '1' AND [STATUS].Purchasing = '0' " +
            //                    "AND (SELECT COUNT(RefId) FROM MT_Category WITH (NOLOCK) WHERE RefId = HEAD.Category AND (isDisabled IS NULL OR isDisabled = '0')) > 0 " +
            //                    "GROUP BY HEAD.Category " +

            //                    "UNION " +

            //                    "SELECT 'FOR_APPROVAL' AS Name, COUNT(HEAD.RFQNo) AS RequestCount, HEAD.Category, " +
            //                    "(SELECT UPPER(Description) FROM MT_Category WITH (NOLOCK) WHERE RefId = HEAD.Category) AS CategoryName " +
            //                    "FROM Request_Status STAT WITH (NOLOCK) " +
            //                    "INNER JOIN Request_Head HEAD ON STAT.RFQNo = HEAD.RFQNo " +
            //                    "WHERE STAT.ProdManager = '1' AND STAT.Purchasing = '1' AND STAT.Buyer = '0' " +
            //                    "AND (SELECT COUNT(RFQNo) FROM Supplier_Response WITH (NOLOCK) WHERE RFQNo = HEAD.RFQNo) > 0 " +
            //                    "AND (SELECT COUNT(RefId) FROM MT_Category WITH (NOLOCK) WHERE RefId = HEAD.Category AND (isDisabled IS NULL OR isDisabled = '0')) > 0 " +
            //                    "GROUP BY HEAD.Category " +
            //                    ") t " +
            //                    "PIVOT (SUM(RequestCount) FOR Name IN ([FOR_SENDING],[FOR_APPROVAL])) AS PIVOT_TABLE ";

            cmd.CommandText = "SELECT * FROM " +
                            "( " +
                            "SELECT DISTINCT 'FOR_SENDING' AS Name, COUNT(DISTINCT(HEAD.RFQNo)) AS RequestCount, HEAD.Category, " +
                            "(SELECT UPPER(Description) FROM MT_Category WITH (NOLOCK) WHERE RefId = HEAD.Category) AS CategoryName " +
                            "FROM Request_Head HEAD WITH (NOLOCK) " +
                            "INNER JOIN Request_Details DETAILS WITH (NOLOCK) ON HEAD.RFQNo = DETAILS.RFQNo " +
                            "INNER JOIN MT_Category CATEGORY WITH (NOLOCK) ON HEAD.Category = CATEGORY.RefId " +
                            "INNER JOIN vew_RequestHistOfApproval RHOA with (nolock) ON HEAD.RFQNo  = RHOA.RFQNo " +
                            "INNER JOIN Request_Status [STATUS] WITH (NOLOCK) ON HEAD.RFQNo = [STATUS].RFQNo " +
                            "INNER JOIN Login_Credentials LC WITH (NOLOCK) ON HEAD.Requester = LC.RefId " +
                            "LEFT JOIN vew_SupplierResponseCount SRC WITH (NOLOCK) ON HEAD.RFQNo = SRC.RFQNo " +
                            "WHERE [STATUS].ProdManager = '1' AND [STATUS].Purchasing = '0' " +
                            "AND (SELECT COUNT(RefId) FROM MT_Category WITH (NOLOCK) WHERE RefId = HEAD.Category AND (isDisabled IS NULL OR isDisabled = '0')) > 0 " +
                            "GROUP BY HEAD.Category " +

                            "UNION " +

                            "SELECT 'FOR_APPROVAL' AS Name, COUNT(HEAD.RFQNo) AS RequestCount, HEAD.Category, " +
                            "(SELECT UPPER(Description) FROM MT_Category WITH (NOLOCK) WHERE RefId = HEAD.Category) AS CategoryName " +
                            "FROM Request_Status STAT WITH (NOLOCK) " +
                            "INNER JOIN Request_Head HEAD ON STAT.RFQNo = HEAD.RFQNo " +
                            "WHERE STAT.ProdManager = '1' AND STAT.Purchasing = '1' AND STAT.Buyer = '0' " +
                            "AND (SELECT COUNT(RFQNo) FROM Supplier_Response WITH (NOLOCK) WHERE RFQNo = HEAD.RFQNo) > 0 " +
                            "AND (SELECT COUNT(RefId) FROM MT_Category WITH (NOLOCK) WHERE RefId = HEAD.Category AND (isDisabled IS NULL OR isDisabled = '0')) > 0 " +
                            "GROUP BY HEAD.Category " +

                            "UNION " +

                            "SELECT 'CRF_SENDING' AS Name, COUNT(STAT.CTRLNo) AS RequestCount, REQUEST.Category, " +
                            "(SELECT UPPER(Description) FROM MT_Category WITH (NOLOCK) WHERE RefId = REQUEST.Category) AS CategoryName " +
                            "FROM CRF_TRANSACTION_Status STAT WITH (NOLOCK) " +
                            "INNER JOIN CRF_TRANSACTION_RequestHead REQUEST WITH (NOLOCK) ON STAT.CTRLNo = REQUEST.CTRLNo " +
                            "WHERE STAT.STATReq_Manager = '1' AND STAT.STATPur_Incharge = '1' AND STAT.STATPur_Manager = '1' " +
                            "AND STAT.STATBuyerSend = '0' " +
                            "AND (REQUEST.ForSending IS NULL OR REQUEST.ForSending IN ('0','1')) " +
                            "AND (STAT.Posted = '0' OR STAT.Posted IS NULL) " +
                            "AND (SELECT COUNT(RefId) FROM MT_Category WITH (NOLOCK) WHERE RefId = REQUEST.Category AND (isDisabled IS NULL OR isDisabled = '0')) > 0 " +
                            "GROUP BY REQUEST.Category " +

                            "UNION " +

                            "SELECT 'CRF_APPROVAL' AS Name, COUNT(STAT.CTRLNo) AS RequestCount, REQUEST.Category, " +
                            "(SELECT UPPER(Description) FROM MT_Category WITH (NOLOCK) WHERE RefId = REQUEST.Category) AS CategoryName " +
                            "FROM CRF_TRANSACTION_Status STAT WITH (NOLOCK) " +
                            "INNER JOIN CRF_TRANSACTION_RequestHead REQUEST WITH (NOLOCK) ON STAT.CTRLNo = REQUEST.CTRLNo " +
                            "WHERE STAT.STATReq_Manager = '1' AND STAT.STATPur_Incharge = '0' AND STAT.STATPur_Manager = '0' " +
                            "AND (SELECT COUNT(RefId) FROM MT_Category WITH (NOLOCK) WHERE RefId = REQUEST.Category AND (isDisabled IS NULL OR isDisabled = '0')) > 0 " +
                            "GROUP BY REQUEST.Category " +

                            "UNION " +

                            "SELECT 'DRF_SENDING' AS Name, COUNT(STAT.CTRLNo) AS RequestCount, REQUEST.Category, " +
                            "(SELECT UPPER(Description) FROM MT_Category WITH (NOLOCK) WHERE RefId = REQUEST.Category) AS CategoryName " +
                            "FROM DRF_TRANSACTION_Status STAT WITH (NOLOCK) " +
                            "INNER JOIN DRF_TRANSACTION_Request REQUEST WITH (NOLOCK) ON STAT.CTRLNo = REQUEST.CTRLNo " +
                            "WHERE STAT.STATReq_Manager = '1' AND STAT.STATPur_Incharge = '1' AND STAT.STATPur_Manager = '1' " +
                            "AND (STAT.STATBuyerSend IS NULL OR STAT.STATBuyerSend = '0') " +
                            "AND (STAT.Posted = '0' OR STAT.Posted IS NULL) " +
                            "AND (SELECT COUNT(RefId) FROM MT_Category WITH (NOLOCK) WHERE RefId = REQUEST.Category AND (isDisabled IS NULL OR isDisabled = '0')) > 0 " +
                            "GROUP BY REQUEST.Category " +

                            "UNION " +

                            "SELECT 'DRF_APPROVAL' AS Name, COUNT(STAT.CTRLNo) AS RequestCount, REQUEST.Category, " +
                            "(SELECT UPPER(Description) FROM MT_Category WITH (NOLOCK) WHERE RefId = REQUEST.Category) AS CategoryName " +
                            "FROM DRF_TRANSACTION_Status STAT WITH (NOLOCK) " +
                            "INNER JOIN DRF_TRANSACTION_Request REQUEST WITH (NOLOCK) ON STAT.CTRLNo = REQUEST.CTRLNo " +
                            "WHERE STAT.STATReq_Manager = '1' AND STAT.STATPur_Incharge = '0' AND STAT.STATPur_Manager = '0' " +
                            "AND (SELECT COUNT(RefId) FROM MT_Category WITH (NOLOCK) WHERE RefId = REQUEST.Category AND (isDisabled IS NULL OR isDisabled = '0')) > 0 " +
                            "GROUP BY REQUEST.Category " +

                            "UNION " +

                            "SELECT 'URF_SENDING' AS Name, COUNT(STAT.CTRLNo) AS RequestCount, REQUEST.Category, " +
                            "(SELECT UPPER(Description) FROM MT_Category WITH (NOLOCK) WHERE RefId = REQUEST.Category) AS CategoryName " +
                            "FROM URF_TRANSACTION_RequestStatus STAT WITH (NOLOCK) " +
                            "INNER JOIN URF_TRANSACTION_RequestHead REQUEST WITH (NOLOCK) ON STAT.CTRLNo = REQUEST.CTRLNo " +
                            "WHERE (STAT.STATProdHQManager= '1' OR STAT.STATProdHQManager= '-1') " +
                            "AND (STAT.STATPurchasingBuyer= '1' OR STAT.STATPurchasingBuyer = '-1') " +
                            "AND (STAT.STATPurchasingManager = '1' OR STAT.STATPurchasingManager = '-1') " +
                            "AND (STAT.STATClosed IS NULL OR STATClosed = '0') " +
                            "AND (SELECT TOP 1 TransactionType FROM URF_TRANSACTION_SendReceived WHERE CTRLNo = STAT.CTRLNo ORDER BY SendReceivedDate DESC) IS NULL " +
                            "AND (SELECT COUNT(RefId) FROM MT_Category WITH (NOLOCK) WHERE RefId = REQUEST.Category AND (isDisabled IS NULL OR isDisabled = '0')) > 0 " +
                            "GROUP BY REQUEST.Category " +

                            "UNION " +

                            "SELECT 'URF_APPROVAL' AS Name, COUNT(STAT.CTRLNo) AS RequestCount, REQUEST.Category, " +
                            "(SELECT UPPER(Description) FROM MT_Category WITH (NOLOCK) WHERE RefId = REQUEST.Category) AS CategoryName " +
                            "FROM URF_TRANSACTION_RequestStatus STAT WITH (NOLOCK) " +
                            "INNER JOIN URF_TRANSACTION_RequestHead REQUEST WITH (NOLOCK) ON STAT.CTRLNo = REQUEST.CTRLNo " +
                            "WHERE (STAT.STATProdHQManager= '1' OR STAT.STATProdHQManager= '-1') " +
                            "AND (STAT.STATProdDivManager = '1' OR STAT.STATProdDivManager= '-1') " +
                            "AND STAT.STATPurchasingBuyer = '0' " +
                            "AND (SELECT COUNT(RefId) FROM MT_Category WITH (NOLOCK) WHERE RefId = REQUEST.Category AND (isDisabled IS NULL OR isDisabled = '0')) > 0 " +
                            "GROUP BY REQUEST.Category " +

                            "UNION " +

                            "SELECT 'SRF_APPROVAL' AS Name, COUNT(STAT.CTRLNo) AS RequestCount, REQUEST.Category, " +
                            "(SELECT UPPER(Description) FROM MT_Category WITH (NOLOCK) WHERE RefId = REQUEST.Category) AS CategoryName " +
                            "FROM SRF_TRANSACTION_Status STAT WITH (NOLOCK) " +
                            "INNER JOIN SRF_TRANSACTION_Request REQUEST WITH (NOLOCK) ON STAT.CTRLNo = REQUEST.CTRLNo " +
                            "WHERE STAT.STATReq_Incharge = '1' AND STAT.STATReq_Manager = '1' AND STAT.STATPur_Incharge = '0' " +
                            "AND (SELECT COUNT(RefId) FROM MT_Category WITH (NOLOCK) WHERE RefId = REQUEST.Category AND (isDisabled IS NULL OR isDisabled = '0')) > 0 " +
                            "GROUP BY REQUEST.Category " +

                            "UNION " +

                            "SELECT 'PROFORMA_APPROVAL' AS Name, COUNT(STAT.CTRLNo) AS RequestCount, REQUEST.Category, " +
                            "(SELECT UPPER(Description) FROM MT_Category WITH (NOLOCK) WHERE RefId = REQUEST.Category) AS CategoryName " +
                            "FROM PIPL_TRANSACTION_RequestStatus STAT WITH (NOLOCK) " +
                            "INNER JOIN PIPL_TRANSACTION_RequestHead REQUEST WITH (NOLOCK) ON STAT.CTRLNo = REQUEST.CTRLNo " +
                            "WHERE STAT.STATManager = '1' AND STAT.STATPCManager IN ('1','3') AND STAT.STATAccounting IN ('1','3') AND STAT.STATIncharge = '0' " +
                            "AND (SELECT COUNT(RefId) FROM MT_Category WITH (NOLOCK) WHERE RefId = REQUEST.Category AND (isDisabled IS NULL OR isDisabled = '0')) > 0 " +
                            "GROUP BY REQUEST.Category " +

                            "UNION " +

                            "SELECT 'ERFO_APPROVAL' AS Name, COUNT(STAT.CTRLNo) AS RequestCount, REQUEST.Category, " +
                            "(SELECT UPPER(Description) FROM MT_Category WITH (NOLOCK) WHERE RefId = REQUEST.Category) AS CategoryName " +
                            "FROM ERFO_TRANSACTION_Status STAT WITH (NOLOCK) " +
                            "INNER JOIN ERFO_TRANSACTION_Request REQUEST WITH (NOLOCK) ON STAT.CTRLNo = REQUEST.CTRLNo " +
                            "INNER JOIN Login_Credentials LC WITH (NOLOCK) ON REQUEST.Requester = LC.RefId " +
                            "WHERE STAT.Req_Received = 1 AND (STAT.Pur_Received = 0 OR STAT.Pur_Received IS NULL) " +
                            "AND (SELECT COUNT(RefId) FROM MT_Category WITH (NOLOCK) WHERE RefId = REQUEST.Category AND (isDisabled IS NULL OR isDisabled = '0')) > 0 " +
                            "GROUP BY REQUEST.Category " +

                            ") t " +
                            "PIVOT (SUM(RequestCount) FOR Name IN ([FOR_SENDING],[FOR_APPROVAL],[CRF_SENDING],[CRF_APPROVAL],[DRF_SENDING],[DRF_APPROVAL],[URF_SENDING],[URF_APPROVAL],[SRF_APPROVAL],[PROFORMA_APPROVAL],[ERFO_APPROVAL])) AS PIVOT_TABLE ";


            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_ForApproval entities = new Entities_Common_ForApproval();

                entities.OtherBuyer_Category = reader["Category"].ToString();
                entities.OtherBuyer_CategoryName = reader["CategoryName"].ToString();
                entities.OtherBuyer_ForSending = reader["FOR_SENDING"] != DBNull.Value ? reader["FOR_SENDING"].ToString() : string.Empty;
                entities.OtherBuyer_ForApproval = reader["FOR_APPROVAL"] != DBNull.Value ? reader["FOR_APPROVAL"].ToString() : string.Empty;
                entities.OtherBuyer_CRFSending = reader["CRF_SENDING"] != DBNull.Value ? reader["CRF_SENDING"].ToString() : string.Empty;
                entities.OtherBuyer_CRFApproval = reader["CRF_APPROVAL"] != DBNull.Value ? reader["CRF_APPROVAL"].ToString() : string.Empty;
                entities.OtherBuyer_DRFSending = reader["DRF_SENDING"] != DBNull.Value ? reader["DRF_SENDING"].ToString() : string.Empty;
                entities.OtherBuyer_DRFApproval = reader["DRF_APPROVAL"] != DBNull.Value ? reader["DRF_APPROVAL"].ToString() : string.Empty;
                entities.OtherBuyer_URFSending = reader["URF_SENDING"] != DBNull.Value ? reader["URF_SENDING"].ToString() : string.Empty;
                entities.OtherBuyer_URFApproval = reader["URF_APPROVAL"] != DBNull.Value ? reader["URF_APPROVAL"].ToString() : string.Empty;
                entities.OtherBuyer_SRFApproval = reader["SRF_APPROVAL"] != DBNull.Value ? reader["SRF_APPROVAL"].ToString() : string.Empty;
                entities.OtherBuyer_PROFORMAApproval = reader["PROFORMA_APPROVAL"] != DBNull.Value ? reader["PROFORMA_APPROVAL"].ToString() : string.Empty;
                entities.OtherBuyer_ERFOApproval = reader["ERFO_APPROVAL"] != DBNull.Value ? reader["ERFO_APPROVAL"].ToString() : string.Empty;


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


    public List<Entities_Common_SystemUsers> getFormListByRefId(string refid)
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Common_getFormList_ByRefId";
            cmd.Parameters.Add(Factory.CreateParameter("@refid", refid));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_SystemUsers entities = new Entities_Common_SystemUsers();

                entities.FormList_RefId = reader["RefId"].ToString();
                entities.FormList_AccessValue = reader["AccessValue"].ToString();
                entities.FormList_FormName = reader["FormName"].ToString();
                entities.FormList_FormType = reader["FormType"].ToString();
                entities.FormList_IsDefault = reader["IsDefault"].ToString();
                entities.FormList_OrderDisplay = reader["OrderDisplay"].ToString();
                if (reader["isAllowed"] == DBNull.Value)
                {
                    entities.FormList_IsAllowed = "0";
                }
                else
                {
                    entities.FormList_IsAllowed = reader["isAllowed"].ToString();
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

    public List<Entities_Common_SystemUsers> getPossible_RFQ_Approver_ByTransactionAndDepartment(string department, string transaction)
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Common_getPossible_RFQ_Approver_ByTransactionAndDepartment";
            cmd.Parameters.Add(Factory.CreateParameter("@department", department));
            cmd.Parameters.Add(Factory.CreateParameter("@transaction", transaction));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_SystemUsers entities = new Entities_Common_SystemUsers();

                entities.Department = int.Parse(reader["Department"].ToString());
                entities.FullName = CryptorEngine.Decrypt(reader["FullName"].ToString(), true);


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

    public List<Entities_Common_SystemUsers> getLoginCredentials(string username, string password)
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Common_getLoginCredentials";

            cmd.Parameters.Add(Factory.CreateParameter("@username", CryptorEngine.Encrypt(username, true)));
            cmd.Parameters.Add(Factory.CreateParameter("@password", CryptorEngine.Encrypt(password, true)));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_SystemUsers entities = new Entities_Common_SystemUsers();

                entities.LcRefId = long.Parse(reader["RefId"].ToString());
                entities.Username = reader["Username"].ToString();
                entities.Password = reader["Password"].ToString();
                entities.FullName = reader["FullName"].ToString();
                entities.IsDisabled = reader["isDisabled"].ToString();
                entities.DivisionCode = reader["DivisionCode"].ToString();
                entities.Division = int.Parse(reader["Division"].ToString());
                entities.Department = int.Parse(reader["Department"].ToString());
                entities.Category = reader["Category"].ToString();
                entities.CategoryString = reader["CategoryName"].ToString();

                entities.SectionName = reader["SectionName"].ToString();
                entities.DepartmentName = reader["DepartmentName"].ToString();
                entities.DivisionName = reader["DivisionName"].ToString();
                entities.PcName = reader["PcName"].ToString();
                entities.HqName = reader["HqName"].ToString();
                entities.LocalNumber = reader["LocalNumber"].ToString();
                entities.PC = int.Parse(reader["PC"].ToString());
                entities.HQ = int.Parse(reader["HQ"].ToString());
                entities.EmailAddress = reader["EmailAddress"].ToString();


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

    public List<Entities_Common_SystemUsers> getLoginCredentialsByRefId(string refid)
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Common_getLoginCredentials_ByRefId";

            cmd.Parameters.Add(Factory.CreateParameter("@refid", refid));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_SystemUsers entities = new Entities_Common_SystemUsers();

                entities.LcRefId = long.Parse(reader["RefId"].ToString());
                entities.Username = reader["Username"].ToString();
                entities.Password = reader["Password"].ToString();
                entities.FullName = reader["FullName"].ToString();
                entities.IsDisabled = reader["isDisabled"].ToString();
                entities.SectionCode = reader["SectionCode"].ToString();
                entities.DepartmentCode = reader["DepartmentCode"].ToString();
                entities.PcCode = reader["PcCode"].ToString();
                entities.HqCode = reader["HqCode"].ToString();
                entities.DivisionCode = reader["DivisionCode"].ToString();
                entities.Division = int.Parse(reader["Division"].ToString());
                entities.Department = int.Parse(reader["Department"].ToString());
                entities.Category = reader["Category"].ToString();
                entities.Section = int.Parse(reader["Section"].ToString());
                entities.PC = int.Parse(reader["PC"].ToString());
                entities.HQ = int.Parse(reader["HQ"].ToString());
                entities.SectionName = reader["SectionName"].ToString();
                entities.DepartmentName = reader["DepartmentName"].ToString();
                entities.DivisionName = reader["DivisionName"].ToString();
                entities.PcName = reader["PcName"].ToString();
                entities.HqName = reader["HqName"].ToString();
                entities.LocalNumber = reader["LocalNumber"].ToString();
                entities.EmailAddress = reader["EmailAddress"].ToString();

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

    public List<Entities_Common_SystemUsers> getLoginCredentials_ByUserName(string username)
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Common_getLoginCredentials_ByUserName";

            cmd.Parameters.Add(Factory.CreateParameter("@username", username));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_SystemUsers entities = new Entities_Common_SystemUsers();

                entities.LcRefId = long.Parse(reader["RefId"].ToString());
                entities.Username = reader["Username"].ToString();
                entities.Password = reader["Password"].ToString();
                entities.FullName = reader["FullName"].ToString();
                entities.IsDisabled = reader["isDisabled"].ToString();
                entities.DivisionCode = reader["DivisionCode"].ToString();
                entities.Division = int.Parse(reader["Division"].ToString());
                entities.Department = int.Parse(reader["Department"].ToString());
                entities.Category = reader["Category"].ToString();
                entities.Section = int.Parse(reader["Section"].ToString());
                entities.PC = int.Parse(reader["PC"].ToString());
                entities.HQ = int.Parse(reader["HQ"].ToString());
                entities.SectionName = reader["SectionName"].ToString();
                entities.DepartmentName = reader["DepartmentName"].ToString();
                entities.DivisionName = reader["DivisionName"].ToString();
                entities.PcName = reader["PcName"].ToString();
                entities.HqName = reader["HqName"].ToString();
                entities.LocalNumber = reader["LocalNumber"].ToString();

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

    public List<Entities_Common_SystemUsers> getLoginCredentials_All()
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Common_getLoginCredentials_All";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_SystemUsers entities = new Entities_Common_SystemUsers();

                entities.LcRefId = long.Parse(reader["RefId"].ToString());
                entities.Username = CryptorEngine.Decrypt(reader["UserName"].ToString(), true).ToUpper();
                entities.Password = reader["Password"].ToString();
                entities.Password2 = CryptorEngine.Decrypt(reader["Password"].ToString(), true);
                entities.FullName = CryptorEngine.Decrypt(reader["FullName"].ToString(), true).ToUpper();
                entities.IsDisabled = reader["isDisabled"].ToString();
                entities.DivisionCode = reader["DivisionCode"].ToString();
                entities.Division = int.Parse(reader["Division"].ToString());
                entities.Department = int.Parse(reader["Department"].ToString());
                entities.Category = reader["Category"].ToString();

                entities.SectionName = reader["SectionName"].ToString();
                entities.DepartmentName = reader["DepartmentName"].ToString();
                entities.DivisionName = reader["DivisionName"].ToString();
                entities.PcName = reader["PcName"].ToString();
                entities.HqName = reader["HqName"].ToString();


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

    public List<Entities_Common_SystemUsers> getLoginCredentials_All_Export()
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT LC.Category, LC.RefId as RefId, LC.Username as Username, LC.FullName as FullName, LC.LocalNumber, LC.EmailAddress, " +
                              "LC2.Username as LC2AddedBy, LC.AddedDate as AddedDate, LC.UpdatedBy as UpdatedBy, " +
                              "LC.UpdatedDate as UpdatedDate, MTS.Description as Section, MTD.Description as Department, MTPC.Description as PC, MTHQ.Description as HQ, " +
                              "MTDI.Description as Division, LC.IsDisabled," +
                              "CASE WHEN LC.Category = '0' THEN 'Empty Value' ELSE (SELECT [Description] FROM MT_Category WITH (NOLOCK) WHERE RefId = LC.Category) END AS CategoryString, " +
                              "(SELECT LoginId FROM UserAccess WITH (NOLOCK) WHERE LoginId = LC.RefId AND [Transaction] = '401') AS URF_PROD_SECTION_MANAGER, " +
                              "(SELECT LoginId FROM UserAccess WITH (NOLOCK) WHERE LoginId = LC.RefId AND [Transaction] = '402') AS URF_PROD_DEPARTMENT_MANAGER, " +
                              "(SELECT LoginId FROM UserAccess WITH (NOLOCK) WHERE LoginId = LC.RefId AND [Transaction] = '403') AS URF_PROD_DIVISION_MANAGER, " +
                              "(SELECT LoginId FROM UserAccess WITH (NOLOCK) WHERE LoginId = LC.RefId AND [Transaction] = '404') AS URF_PROD_HQ_MANAGER, " +
                              "(SELECT LoginId FROM UserAccess WITH (NOLOCK) WHERE LoginId = LC.RefId AND [Transaction] = '15') AS SCD_INCHARGE, " +
                              "(SELECT LoginId FROM UserAccess WITH (NOLOCK) WHERE LoginId = LC.RefId AND [Transaction] = '16') AS SCD_DEPARTMENT_MANAGER, " +
                              "(SELECT LoginId FROM UserAccess WITH (NOLOCK) WHERE LoginId = LC.RefId AND [Transaction] = '17') AS SCD_DIVISION_MANAGER, " +
                              "(SELECT LoginId FROM UserAccess WITH (NOLOCK) WHERE LoginId = LC.RefId AND [Transaction] = '101') AS PROFORMA_PC_MANAGER, " +
                              "(SELECT LoginId FROM UserAccess WITH (NOLOCK) WHERE LoginId = LC.RefId AND [Transaction] = '8') AS RFQ_CRF_DRF_APPROVER " +
                              "FROM Login_Credentials LC WITH (NOLOCK) " +
                              "INNER JOIN Login_Credentials LC2 WITH (NOLOCK) ON LC.AddedBy = LC2.RefId " +
                              "INNER JOIN MT_Section MTS WITH (NOLOCK) ON LC.Section = MTS.RefId " +
                              "INNER JOIN MT_Department MTD WITH (NOLOCK) ON LC.Department = MTD.RefId " +
                              "LEFT JOIN MT_PC MTPC WITH (NOLOCK) ON LC.PC = MTPC.RefId " +
                              "LEFT JOIN MT_HQ MTHQ WITH (NOLOCK) ON LC.HQ = MTHQ.RefId " +
                              "INNER JOIN MT_Division MTDI WITH (NOLOCK) ON LC.Division = MTDI.RefId WHERE LC.isDisabled = '0' ORDER BY LC.Username ASC";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_SystemUsers entities = new Entities_Common_SystemUsers();

                entities.RefId = reader["RefId"].ToString();
                entities.Username = CryptorEngine.Decrypt(reader["Username"].ToString(), true);
                entities.FullName = CryptorEngine.Decrypt(reader["FullName"].ToString(), true).ToUpper();
                entities.SectionString = reader["Section"].ToString().ToUpper();
                entities.DepartmentString = reader["Department"].ToString().ToUpper();
                entities.DivisionString = reader["Division"].ToString().ToUpper();
                entities.AddedByString = CryptorEngine.Decrypt(reader["LC2AddedBy"].ToString(), true).ToUpper();
                entities.AddedDate = reader["AddedDate"].ToString();
                entities.IsDisabled = reader["IsDisabled"].ToString();
                entities.Category = reader["Category"].ToString().ToUpper();
                entities.CategoryString = reader["CategoryString"].ToString().ToUpper();
                entities.LocalNumber = reader["LocalNumber"].ToString();
                entities.PcString = reader["PC"].ToString();
                entities.HqString = reader["HQ"].ToString();
                entities.EmailAddress = reader["EmailAddress"].ToString();
                entities.Urf_Prod_SectionManager = reader["URF_PROD_SECTION_MANAGER"] != DBNull.Value ? "YES" : string.Empty;
                entities.Urf_Prod_DepartmentManager = reader["URF_PROD_DEPARTMENT_MANAGER"] != DBNull.Value ? "YES" : string.Empty;
                entities.Urf_Prod_DivisionManager = reader["URF_PROD_DIVISION_MANAGER"] != DBNull.Value ? "YES" : string.Empty;
                entities.Urf_Prod_HQManager = reader["URF_PROD_HQ_MANAGER"] != DBNull.Value ? "YES" : string.Empty;
                entities.Scd_Incharge = reader["SCD_INCHARGE"] != DBNull.Value ? "YES" : string.Empty;
                entities.Scd_DepartmentManager = reader["SCD_DEPARTMENT_MANAGER"] != DBNull.Value ? "YES" : string.Empty;
                entities.Scd_DivisionManager = reader["SCD_DIVISION_MANAGER"] != DBNull.Value ? "YES" : string.Empty;
                entities.Proforma_PCManager = reader["PROFORMA_PC_MANAGER"] != DBNull.Value ? "YES" : string.Empty;
                entities.Rfq_Prod_Approver = reader["RFQ_CRF_DRF_APPROVER"] != DBNull.Value ? "YES" : string.Empty;
                entities.Crf_Prod_Approver = reader["RFQ_CRF_DRF_APPROVER"] != DBNull.Value ? "YES" : string.Empty;
                entities.Drf_Prod_Approver = reader["RFQ_CRF_DRF_APPROVER"] != DBNull.Value ? "YES" : string.Empty;

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

    public List<Entities_Common_SystemUsers> Common_checkIfUserAccessExistByUserId(string loginid, string transaction)
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Common_checkIfUserAccessExistByUserId";

            cmd.Parameters.Add(Factory.CreateParameter("@loginid", loginid));
            cmd.Parameters.Add(Factory.CreateParameter("@trans", transaction));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_SystemUsers entities = new Entities_Common_SystemUsers();

                entities.LcRefId = long.Parse(reader["LoginId"].ToString());

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

    public List<Entities_Common_SystemUsers> Common_UserAccess_Fill_All_DropdownList()
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Common_UserAccess_Fill_All_DropdownList";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_SystemUsers entities = new Entities_Common_SystemUsers();

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

    public int Common_ChangePassword_ByRefId(string password, string refid)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "UPDATE Login_Credentials SET Password ='" + password + "' WHERE RefId = '" + refid + "'";

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
    public int Common_UpdateLoginCredentials_ByRefId(Entities_Common_SystemUsers entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Common_UpdateLoginCredentials_ByRefId";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.LcRefId));
        cmd.Parameters.Add(Factory.CreateParameter("@username", entity.Username));
        cmd.Parameters.Add(Factory.CreateParameter("@fullname", entity.FullName));
        cmd.Parameters.Add(Factory.CreateParameter("@section", entity.Section));
        cmd.Parameters.Add(Factory.CreateParameter("@department", entity.Department));
        cmd.Parameters.Add(Factory.CreateParameter("@division", entity.Division));
        cmd.Parameters.Add(Factory.CreateParameter("@pc", entity.PC));
        cmd.Parameters.Add(Factory.CreateParameter("@hq", entity.HQ));
        cmd.Parameters.Add(Factory.CreateParameter("@category", entity.Category));
        cmd.Parameters.Add(Factory.CreateParameter("@localnumber", entity.LocalNumber));
        cmd.Parameters.Add(Factory.CreateParameter("@updatedby", entity.UpdatedBy));
        cmd.Parameters.Add(Factory.CreateParameter("@emailaddress", entity.EmailAddress));

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

    public int Common_InserUserAccess_ByLoginId(Entities_Common_SystemUsers entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Common_InserUserAccess_ByLoginId";

        cmd.Parameters.Add(Factory.CreateParameter("@loginid", entity.LcRefId));
        cmd.Parameters.Add(Factory.CreateParameter("@transaction", entity.TransactionType));
        cmd.Parameters.Add(Factory.CreateParameter("@allowed", entity.Allowed));
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

    public int Common_DeleteUserAccess_ByLoginId(Entities_Common_SystemUsers entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Common_DeleteUserAccess_ByLoginId";

        cmd.Parameters.Add(Factory.CreateParameter("@loginid", entity.LcRefId));

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

    public List<Entities_Common_SystemUsers> Common_checkIfUserHasTransactionsByUserId(string loginid)
    {
        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Common_checkIfUserHasTransactionsByUserId";

            cmd.Parameters.Add(Factory.CreateParameter("@loginid", loginid));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_SystemUsers entities = new Entities_Common_SystemUsers();

                entities.LcRefId = long.Parse(reader["LoginId"].ToString());
                entities.TransactionType = reader["Transaction"].ToString();

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

    public int Common_DisabledLoginCredentials_ByRefId(Entities_Common_SystemUsers entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Common_DisabledLoginCredentials_ByRefId";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.LcRefId));

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

    public int Common_EnabledLoginCredentials_ByRefId(Entities_Common_SystemUsers entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Common_EnabledLoginCredentials_ByRefId";

        cmd.Parameters.Add(Factory.CreateParameter("@refid", entity.LcRefId));

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

    public int Common_InsertLoginCredentials(Entities_Common_SystemUsers entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Common_InsertLoginCredentials";

        cmd.Parameters.Add(Factory.CreateParameter("@username", entity.Username));
        cmd.Parameters.Add(Factory.CreateParameter("@fullname", entity.FullName));
        cmd.Parameters.Add(Factory.CreateParameter("@section", entity.Section));
        cmd.Parameters.Add(Factory.CreateParameter("@department", entity.Department));
        cmd.Parameters.Add(Factory.CreateParameter("@division", entity.Division));
        cmd.Parameters.Add(Factory.CreateParameter("@pc", entity.PC));
        cmd.Parameters.Add(Factory.CreateParameter("@hq", entity.HQ));
        cmd.Parameters.Add(Factory.CreateParameter("@category", entity.Category));
        cmd.Parameters.Add(Factory.CreateParameter("@localnumber", entity.LocalNumber));
        cmd.Parameters.Add(Factory.CreateParameter("@addedby", entity.AddedBy));
        cmd.Parameters.Add(Factory.CreateParameter("@emailaddress", entity.EmailAddress));

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

    public int Common_ResetPassword(Entities_Common_SystemUsers entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "UPDATE Login_Credentials SET Password = 'L/oLNK6q4s4=' WHERE RefId = '" + entity.LcRefId + "'";

       
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


    public List<Entities_Common_MTSupplier> Common_getSupplier_ByRefId(string refid)
    {
        List<Entities_Common_MTSupplier> list = new List<Entities_Common_MTSupplier>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Common_getSupplier_ByRefId";
            cmd.Parameters.Add(Factory.CreateParameter("@refid", refid));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_MTSupplier entities = new Entities_Common_MTSupplier();

                entities.Name = reader["Name"].ToString();
                entities.Address = reader["Address"].ToString();

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


    public List<Entities_Common_IntegrationFromOtherDatabase> Common_getBusinessUnit()
    {
        List<Entities_Common_IntegrationFromOtherDatabase> list = new List<Entities_Common_IntegrationFromOtherDatabase>();

        DbConnection conn = Factory.CreateConnection_FROM_UPSVGAD2_DB_Instance();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT BusinessUnit AS RefId, BusinessUnit AS [Description] FROM vewBusinessUnit WITH (NOLOCK) WHERE Delete_Flg = 0";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_IntegrationFromOtherDatabase entities = new Entities_Common_IntegrationFromOtherDatabase();

                entities.Refid = reader["Refid"].ToString();
                entities.Description = reader["Description"].ToString();

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

    public List<Entities_Common_IntegrationFromOtherDatabase> Common_getAccountCode()
    {
        List<Entities_Common_IntegrationFromOtherDatabase> list = new List<Entities_Common_IntegrationFromOtherDatabase>();

        DbConnection conn = Factory.CreateConnection_FROM_UPSVGAD2_DB_Instance();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT AccountCode AS RefId, AccountCode AS [Description] FROM vewAccountCode WITH (NOLOCK) WHERE Delete_Flg = 0";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_IntegrationFromOtherDatabase entities = new Entities_Common_IntegrationFromOtherDatabase();

                entities.Refid = reader["Refid"].ToString();
                entities.Description = reader["Description"].ToString();

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

    public List<Entities_Common_TransactionLogs> RFQ_TRANSACTION_TransactionLogs_ByDateRange(Entities_Common_TransactionLogs entity)
    {
        List<Entities_Common_TransactionLogs> list = new List<Entities_Common_TransactionLogs>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RFQ_TRANSACTION_TransactionLogs_ByDateRange";

            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.TransactionDateFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.TransactionDateTo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_TransactionLogs entities = new Entities_Common_TransactionLogs();

                entities.TransactionDate = reader["TransactionDate"].ToString();
                entities.TransactionLogs = reader["TransactionLog"].ToString();


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

    public string GetBuyerEmailAddressByHandledCategory(string category)
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
            cmd.CommandText = "SELECT TOP 1 EmailAddress FROM Login_Credentials WITH (NOLOCK) WHERE Category = '" + category + "' AND isDisabled = '0'";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                retVal = reader["EmailAddress"].ToString();
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
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


    #region SYSTEM INFORMATION

    public List<Entities_Common_SystemInformation> Common_SystemInformation_GetAll()
    {
        List<Entities_Common_SystemInformation> list = new List<Entities_Common_SystemInformation>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Common_SystemInformation_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_SystemInformation entities = new Entities_Common_SystemInformation();

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

    public int Common_SystemInformation_IsDisabled(Entities_Common_SystemInformation entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Common_SystemInformation_IsDisabled";

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

    public int Common_SystemInformation_Append(Entities_Common_SystemInformation entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Common_SystemInformation_Append";

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

    public int Common_SystemInformation_Insert(Entities_Common_SystemInformation entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Common_SystemInformation_Insert";

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

    public List<Entities_Common_SystemInformation> Common_SystemInformation_GetByName(string name)
    {
        List<Entities_Common_SystemInformation> list = new List<Entities_Common_SystemInformation>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Common_SystemInformation_GetByName";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_SystemInformation entities = new Entities_Common_SystemInformation();

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

    public List<Entities_Common_SystemInformation> Common_SystemInformation_GetByName_Like(string name)
    {
        List<Entities_Common_SystemInformation> list = new List<Entities_Common_SystemInformation>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Common_SystemInformation_GetByName_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_Common_SystemInformation entities = new Entities_Common_SystemInformation();

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
