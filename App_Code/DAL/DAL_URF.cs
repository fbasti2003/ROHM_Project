using System;
using System.Data;
using System.Configuration;
using System.Linq;
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


public class DAL_URF
{
    public DAL_URF()
    {
    }

    #region REASON

    public List<Entities_URF_Reason> URF_MT_Reason_GetAll()
    {
        List<Entities_URF_Reason> list = new List<Entities_URF_Reason>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "URF_MT_Reason_GetAll";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_Reason entities = new Entities_URF_Reason();

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

    public int URF_MT_Reason_IsDisabled(Entities_URF_Reason entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "URF_MT_Reason_IsDisabled";

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

    public int URF_MT_Reason_Append(Entities_URF_Reason entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "URF_MT_Reason_Append";

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

    public int URF_MT_Reason_Insert(Entities_URF_Reason entity)
    {
        int result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "URF_MT_Reason_Insert";

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

    public List<Entities_URF_Reason> URF_MT_Reason_GetByName(string name)
    {
        List<Entities_URF_Reason> list = new List<Entities_URF_Reason>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "URF_MT_Reason_GetByName";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_Reason entities = new Entities_URF_Reason();

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

    public List<Entities_URF_Reason> URF_MT_Reason_GetByName_Like(string name)
    {
        List<Entities_URF_Reason> list = new List<Entities_URF_Reason>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "URF_MT_Reason_GetByName_Like";

            cmd.Parameters.Add(Factory.CreateParameter("@name", name));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_Reason entities = new Entities_URF_Reason();

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


    #region Request Entry

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_RequestEntry_Fill_All_DropdownList()
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "URF_TRANSACTION_RequestEntry_Fill_All_DropdownList";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entity = new Entities_URF_RequestEntry();

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

    public int URF_TRANSACTION_RequestEntry_Insert_SQLTransaction(string query)
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

    public Int32 URF_TRANSACTION_CountRequestHead(string year)
    {
        Int32 result = 0;

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "URF_TRANSACTION_CountRequestHead";

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

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_Monitoring_ByDateRange(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "URF_TRANSACTION_Monitoring_ByDateRange";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.Parameters.Add(Factory.CreateParameter("@requester", entity.RhRequester = entity.RhRequester.Length <= 0 ? string.Empty : entity.RhRequester));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

                string prodSecManager = reader["STATProdSecManager"].ToString();
                string prodDeptManager = reader["STATProdDeptManager"].ToString();
                string prodDivManager = reader["STATProdDivManager"].ToString();
                string prodHQManager = reader["STATProdHQManager"].ToString();
                string purchasingBuyer = reader["STATPurchasingBuyer"].ToString();
                string purchasingManager = reader["STATPurchasingManager"].ToString();
                string statClosed = reader["STATClosed"] != DBNull.Value ? reader["STATClosed"].ToString() : string.Empty;

                entities.RhCtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.RhCategory = reader["Category"].ToString().ToUpper();
                entities.RhCategoryName = reader["CategoryName"].ToString();
                entities.RhTransactionDate = reader["TransactionDate"].ToString();
                entities.RhRequester = CryptorEngine.Decrypt(reader["FullName"].ToString(), true);
                entities.RhSendReceived = reader["SendReceived"] != DBNull.Value ? reader["SendReceived"].ToString() : string.Empty;
                entities.StatClosed = reader["STATClosed"] != DBNull.Value ? reader["STATClosed"].ToString() : string.Empty;
                entities.StatRemarks = reader["Remarks"].ToString().ToUpper();
                entities.EmptyPO = reader["EmptyPO"].ToString().ToUpper();
                //if (prodSecManager == "0")
                //{
                //    entities.StatAll = "FOR SECTION MNGR APPROVAL";
                //    entities.CssColorCode = "#f44336";
                //}
                //if (prodSecManager == "1" && prodDeptManager == "0")
                //{
                //    entities.StatAll = "FOR DEPARTMENT MNGR APPROVAL";
                //    entities.CssColorCode = "#9C27B0";
                //}
                //if (prodSecManager == "1" && prodDeptManager == "1" && prodDivManager == "0")
                //{
                //    entities.StatAll = "FOR DIVISION MNGR APPROVAL";
                //    entities.CssColorCode = "#673AB7";
                //}
                //if (prodSecManager == "1" && prodDeptManager == "1" && prodDivManager == "1" && prodHQManager == "0")
                //{
                //    entities.StatAll = "FOR HQ MNGR APPROVAL";
                //    entities.CssColorCode = "#009688";
                //}
                //if (prodSecManager == "1" && prodDeptManager == "1" && prodDivManager == "1" && prodHQManager == "1" && purchasingBuyer == "0")
                //{
                //    entities.StatAll = "FOR PUR BUYER APPROVAL";
                //    entities.CssColorCode = "#8BC34A";
                //}
                //if (prodSecManager == "1" && prodDeptManager == "1" && prodDivManager == "1" && prodHQManager == "1" && purchasingBuyer == "1" && purchasingManager == "0")
                //{
                //    entities.StatAll = "FOR PUR MANAGER APPROVAL";
                //    entities.CssColorCode = "#CDDC39";
                //}
                if (prodSecManager == "0")
                {
                    entities.StatAll = "FOR SEC. MNGR APPROVAL";
                    entities.CssColorCode = "#f44336";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && prodDeptManager == "0")
                {
                    entities.StatAll = "FOR DEPT. MNGR APPROVAL";
                    entities.CssColorCode = "#9C27B0";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && prodDivManager == "0")
                {
                    entities.StatAll = "FOR DIV. MNGR APPROVAL";
                    entities.CssColorCode = "#673AB7";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && (prodDivManager == "1" || prodDivManager == "-1") && prodHQManager == "0")
                {
                    entities.StatAll = "FOR HQ MNGR APPROVAL";
                    entities.CssColorCode = "#009688";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && (prodDivManager == "1" || prodDivManager == "-1") && (prodHQManager == "1" || prodHQManager == "-1") && purchasingBuyer == "0")
                {
                    entities.StatAll = "FOR PUR BUYER APPROVAL";
                    entities.CssColorCode = "#8BC34A";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && (prodDivManager == "1" || prodDivManager == "-1") && (prodHQManager == "1" || prodHQManager == "-1") && purchasingBuyer == "1" && purchasingManager == "0")
                {
                    entities.StatAll = "FOR PUR MANAGER APPROVAL";
                    entities.CssColorCode = "#CDDC39";
                }

                if ((prodSecManager == "1" || prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && (prodDivManager == "1" || prodDivManager == "-1") && (prodHQManager == "1" || prodHQManager == "-1") && purchasingBuyer == "1" && purchasingManager == "1")
                {
                    entities.StatAll = "FOR SENDING";
                    entities.CssColorCode = "#8BC34A";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && (prodDivManager == "1" || prodDivManager == "-1") && (prodHQManager == "1" || prodHQManager == "-1") && purchasingBuyer == "1" && purchasingManager == "1" && entities.RhSendReceived == "SEND")
                {
                    entities.StatAll = "FOR RESEND";
                    entities.CssColorCode = "#673AB7";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && (prodDivManager == "1" || prodDivManager == "-1") && (prodHQManager == "1" || prodHQManager == "-1") && purchasingBuyer == "1" && purchasingManager == "1" && entities.RhSendReceived == "RECEIVED")
                {
                    entities.StatAll = "SUPPLIER RESPONDED";
                    entities.CssColorCode = "#673AB7";
                }
                if (prodSecManager == "2" || prodDeptManager == "2" || prodDivManager == "2" || prodHQManager == "2" || purchasingBuyer == "2" || purchasingManager == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
                }
                if (statClosed == "1")
                {
                    entities.StatAll = "CLOSED";
                    entities.CssColorCode = "#673AB7";
                }
                if (statClosed == "2")
                {
                    entities.StatAll = "RE-OPENED / WAITING";
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

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_GetRequestDetailsByCTRLNo(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "URF_TRANSACTION_GetRequestDetailsByCTRLNo";
            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.RdCtrlNo = entity.RdCtrlNo.Length <= 0 ? string.Empty : entity.RdCtrlNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

                string doaProdSecManager = reader["DOAProdSecManager"] != DBNull.Value ? reader["DOAProdSecManager"].ToString() : string.Empty;
                string doaProdDeptManager = reader["DOAProdDeptManager"] != DBNull.Value ? reader["DOAProdDeptManager"].ToString() : string.Empty;
                string doaProdDivManager = reader["DOAProdDivManager"] != DBNull.Value ? reader["DOAProdDivManager"].ToString() : string.Empty;
                string doaProdHQManager = reader["DOAProdHQManager"] != DBNull.Value ? reader["DOAProdHQManager"].ToString() : string.Empty;
                string doaPurchasingBuyer = reader["DOAPurchasingBuyer"] != DBNull.Value ? reader["DOAPurchasingBuyer"].ToString() : string.Empty;
                string doaPurchasingManager = reader["DOAPurchasingManager"] != DBNull.Value ? reader["DOAPurchasingManager"].ToString() : string.Empty;

                entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true);
                entities.RhTransactionDate = reader["TransactionDate"].ToString();
                entities.LcDepartment = reader["Department"].ToString();
                entities.LcDivision = reader["Division"].ToString();
                entities.RdRefId = reader["RefId"].ToString();
                entities.RdCtrlNo = reader["CTRLNo"].ToString().ToUpper();
                entities.RdPONO = reader["PONO"].ToString().ToUpper();
                entities.RdPRNO = reader["PRNO"].ToString().ToUpper();
                entities.RdItemName = reader["ItemName"].ToString().ToUpper();
                entities.RdSpecs = reader["Specs"].ToString().ToUpper();
                entities.RdQuantity = reader["Quantity"].ToString().ToUpper();
                entities.RdUnitOfMeasure = reader["UnitOfMeasure"].ToString().ToUpper();
                entities.RdUOMDesc = reader["UOMDesc"].ToString().ToUpper();
                entities.RdDeliveryConfirmationDate = reader["DeliveryConfirmationDate"].ToString().ToUpper();
                entities.RdRequestedDeliveryDate = reader["RequestedDeliveryDate"].ToString().ToUpper();
                //entities.RdReplyDeliveryDate = reader["ReplyDeliveryDate"].ToString().ToUpper();
                if (reader["RepDelDate"] != DBNull.Value || !string.IsNullOrEmpty(reader["RepDelDate"].ToString()))
                {
                    entities.RdReplyDeliveryDate = reader["ReplyDeliveryDate"] != DBNull.Value ? reader["ReplyDeliveryDate"].ToString() : string.Empty;
                    entities.FontColor = "Black";
                }
                else
                {
                    if (string.IsNullOrEmpty(reader["PONO"].ToString()) || reader["PONO"] == DBNull.Value)
                    {
                        entities.RdReplyDeliveryDate = "<p style='color:Blue;'>NO PO YET</p>";
                        entities.FontColor = "Blue";
                    }

                    if (!string.IsNullOrEmpty(reader["PONO"].ToString()) && !string.IsNullOrEmpty(reader["PRNO"].ToString()))
                    {
                        List<Entities_URF_RequestEntry> entitiesPO = new List<Entities_URF_RequestEntry>();
                        entitiesPO = vewRFQDelivery("SELECT * FROM [ROPROS].[dbo].[vewRFQDelivery] WITH (NOLOCK) WHERE PONO = '" + reader["PONO"].ToString() + "' AND PRNO = '" + reader["PRNO"].ToString() + "'", string.Empty);

                        if (entitiesPO.Count <= 0)
                        {
                            //entities.RdReplyDeliveryDate = "<p style='color:Red;'>DELIVERED</p>";
                            entities.RdReplyDeliveryDate = "<p style='color:Red;'>CLOSED</p>";
                            entities.FontColor = "Red";
                        }
                    }

                }
                entities.StatRemarks = reader["Remarks"] != DBNull.Value ? reader["Remarks"].ToString() : string.Empty;
                entities.StatClosed = reader["STATClosed"] != DBNull.Value ? reader["STATClosed"].ToString() : string.Empty;
                entities.StatReOpenRemarks = reader["ReOpenRemarks"] != DBNull.Value ? reader["ReOpenRemarks"].ToString() : string.Empty;

                entities.StatProdSecManager = reader["ProdSecManagerName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["ProdSecManagerName"].ToString(), true).ToString() + "<br/>" + doaProdSecManager : "PENDING APPROVAL";
                entities.StatProdDeptManager = reader["ProdDeptManagerName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["ProdDeptManagerName"].ToString(), true).ToString() + "<br/>" + doaProdDeptManager : "PENDING APPROVAL";
                entities.StatProdDivManager = reader["ProdDivManagerName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["ProdDivManagerName"].ToString(), true).ToString() + "<br/>" + doaProdDivManager : "PENDING APPROVAL";
                entities.StatProdHQManager = reader["ProdHQManagerName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["ProdHQManagerName"].ToString(), true).ToString() + "<br/>" + doaProdHQManager : "PENDING APPROVAL";
                entities.StatPurchasingBuyer = reader["PurchasingBuyerName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["PurchasingBuyerName"].ToString(), true).ToString() + "<br/>" + doaPurchasingBuyer : "PENDING APPROVAL";
                entities.StatPurchasingManager = reader["PurchasingManagerName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["PurchasingManagerName"].ToString(), true).ToString() + "<br/>" + doaPurchasingManager : "PENDING APPROVAL";

                entities.RhSupplier = reader["SupplierName"].ToString().ToUpper();
                entities.RhReason = reader["Reason"] != DBNull.Value ? reader["Reason"].ToString().ToUpper() : reader["OtherReason"].ToString().ToUpper();
                entities.RhOtherReason = reader["OtherReason"] != DBNull.Value ? reader["OtherReason"].ToString().ToUpper() : string.Empty;
                entities.RhType = reader["Type"] != DBNull.Value ? reader["Type"].ToString().ToUpper() : string.Empty;
                entities.RhAttention = reader["Attention"] != DBNull.Value ? reader["Attention"].ToString().ToUpper() : string.Empty;
                entities.RhAttachment1 = reader["Attachment1"] != DBNull.Value ? reader["Attachment1"].ToString() : string.Empty;
                entities.RhAttachment2 = reader["Attachment2"] != DBNull.Value ? reader["Attachment2"].ToString() : string.Empty;
                entities.RhSupplierEmail = reader["SupplierEmail"].ToString();
                entities.RhSupplierId = reader["SupplierId"].ToString();
                entities.RhStockLifeAttachment = reader["StockLifeAttachment"] != DBNull.Value ? reader["StockLifeAttachment"].ToString() : string.Empty;

                entities.StatSTATProdSecManager = reader["STATProdSecManager"].ToString();
                entities.StatSTATProdDeptManager = reader["STATProdDeptManager"].ToString();
                entities.StatSTATProdDivManager = reader["STATProdDivManager"].ToString();
                entities.StatSTATProdHQManager = reader["STATProdHQManager"].ToString();
                entities.StatSTATPurchasingBuyer = reader["STATPurchasingBuyer"].ToString();
                entities.StatSTATPurchasingManager = reader["STATPurchasingManager"].ToString();

                entities.BuyerSend_SendBy = reader["BuyerSend_SendBy"] != DBNull.Value ? reader["BuyerSend_SendBy"].ToString().ToUpper() : string.Empty;
                entities.BuyerSend_SendReceivedDate = reader["BuyerSend_SendReceivedDate"] != DBNull.Value ? reader["BuyerSend_SendReceivedDate"].ToString().ToUpper() : string.Empty;
                entities.SupplierResponse_Date = reader["SupplierResponse_Date"] != DBNull.Value ? reader["SupplierResponse_Date"].ToString().ToUpper() : string.Empty;

                entities.DisapprovalCause = reader["DisapprovalCause"] != DBNull.Value ? reader["DisapprovalCause"].ToString() : string.Empty;

                entities.SendDates = reader["SendDates"] != DBNull.Value ? reader["SendDates"].ToString() : string.Empty;
                entities.PostingRemarks = reader["PostingRemarks"] != DBNull.Value ? reader["PostingRemarks"].ToString() : string.Empty;

                entities.RhAttachment1 = reader["Attachment1"] != DBNull.Value ? reader["Attachment1"].ToString() : string.Empty;
                entities.RhAttachment2 = reader["Attachment2"] != DBNull.Value ? reader["Attachment2"].ToString() : string.Empty;

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

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_GetRequestDetailsByCTRLNo_ExportToExcel(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "URF_TRANSACTION_GetRequestDetailsByCTRLNo";
            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.RdCtrlNo = entity.RhCtrlNo.Length <= 0 ? string.Empty : entity.RhCtrlNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

                string doaProdSecManager = reader["DOAProdSecManager"] != DBNull.Value ? reader["DOAProdSecManager"].ToString() : string.Empty;
                string doaProdDeptManager = reader["DOAProdDeptManager"] != DBNull.Value ? reader["DOAProdDeptManager"].ToString() : string.Empty;
                string doaProdDivManager = reader["DOAProdDivManager"] != DBNull.Value ? reader["DOAProdDivManager"].ToString() : string.Empty;
                string doaProdHQManager = reader["DOAProdHQManager"] != DBNull.Value ? reader["DOAProdHQManager"].ToString() : string.Empty;
                string doaPurchasingBuyer = reader["DOAPurchasingBuyer"] != DBNull.Value ? reader["DOAPurchasingBuyer"].ToString() : string.Empty;
                string doaPurchasingManager = reader["DOAPurchasingManager"] != DBNull.Value ? reader["DOAPurchasingManager"].ToString() : string.Empty;

                entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true);
                entities.RhTransactionDate = reader["TransactionDate"].ToString();
                entities.LcDepartment = reader["Department"].ToString();
                entities.LcDivision = reader["Division"].ToString();
                entities.RdRefId = reader["RefId"].ToString();
                entities.RdCtrlNo = reader["CTRLNo"].ToString().ToUpper();
                entities.RdPONO = reader["PONO"].ToString().ToUpper();
                entities.RdPRNO = reader["PRNO"].ToString().ToUpper();
                entities.RdItemName = reader["ItemName"].ToString().ToUpper();
                entities.RdSpecs = reader["Specs"].ToString().ToUpper();
                entities.RdQuantity = reader["Quantity"].ToString().ToUpper();
                entities.RdUnitOfMeasure = reader["UnitOfMeasure"].ToString().ToUpper();
                entities.RdUOMDesc = reader["UOMDesc"].ToString().ToUpper();
                entities.RdDeliveryConfirmationDate = reader["DeliveryConfirmationDate"].ToString().ToUpper();
                entities.RdRequestedDeliveryDate = reader["RequestedDeliveryDate"].ToString().ToUpper();
                //entities.RdReplyDeliveryDate = reader["ReplyDeliveryDate"].ToString().ToUpper();
                //if (reader["RepDelDate"] != DBNull.Value || !string.IsNullOrEmpty(reader["RepDelDate"].ToString()))
                //{
                //    entities.RdReplyDeliveryDate = reader["ReplyDeliveryDate"] != DBNull.Value ? reader["ReplyDeliveryDate"].ToString() : string.Empty;
                //    entities.FontColor = "Black";
                //}
                //else
                //{
                //    entities.RdReplyDeliveryDate = "<p style='color:red;'>CLOSED</p>";
                //    entities.FontColor = "Red";
                //}

                if (reader["RepDelDate"] != DBNull.Value || !string.IsNullOrEmpty(reader["RepDelDate"].ToString()))
                {
                    entities.RdReplyDeliveryDate = reader["ReplyDeliveryDate"] != DBNull.Value ? reader["ReplyDeliveryDate"].ToString() : string.Empty;
                    entities.FontColor = "Black";
                }
                else
                {
                    if (string.IsNullOrEmpty(reader["PONO"].ToString()) || reader["PONO"] == DBNull.Value)
                    {
                        entities.RdReplyDeliveryDate = "<p style='color:blue;'>NO PO YET</p>";
                        entities.FontColor = "Blue";
                    }

                    if (!string.IsNullOrEmpty(reader["PONO"].ToString()) && !string.IsNullOrEmpty(reader["PRNO"].ToString()))
                    {
                        List<Entities_URF_RequestEntry> entitiesPO = new List<Entities_URF_RequestEntry>();
                        entitiesPO = vewRFQDelivery("SELECT * FROM [ROPROS].[dbo].[vewRFQDelivery] WITH (NOLOCK) WHERE PONO = '" + reader["PONO"].ToString() + "' AND PRNO = '" + reader["PRNO"].ToString() + "'", string.Empty);

                        if (entitiesPO.Count <= 0)
                        {
                            //entities.RdReplyDeliveryDate = "<p style='color:Red;'>DELIVERED</p>";
                            entities.RdReplyDeliveryDate = "<p style='color:Red;'>CLOSED</p>";
                            entities.FontColor = "Red";
                        }
                    }

                }

                entities.StatRemarks = reader["Remarks"] != DBNull.Value ? reader["Remarks"].ToString() : string.Empty;
                entities.StatClosed = reader["STATClosed"] != DBNull.Value ? reader["STATClosed"].ToString() : string.Empty;
                entities.StatReOpenRemarks = reader["ReOpenRemarks"] != DBNull.Value ? reader["ReOpenRemarks"].ToString() : string.Empty;

                entities.StatProdSecManager = reader["ProdSecManagerName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["ProdSecManagerName"].ToString(), true).ToString() + "<br/>" + doaProdSecManager : "PENDING APPROVAL";
                entities.StatProdDeptManager = reader["ProdDeptManagerName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["ProdDeptManagerName"].ToString(), true).ToString() + "<br/>" + doaProdDeptManager : "PENDING APPROVAL";
                entities.StatProdDivManager = reader["ProdDivManagerName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["ProdDivManagerName"].ToString(), true).ToString() + "<br/>" + doaProdDivManager : "PENDING APPROVAL";
                entities.StatProdHQManager = reader["ProdHQManagerName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["ProdHQManagerName"].ToString(), true).ToString() + "<br/>" + doaProdHQManager : "PENDING APPROVAL";
                entities.StatPurchasingBuyer = reader["PurchasingBuyerName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["PurchasingBuyerName"].ToString(), true).ToString() + "<br/>" + doaPurchasingBuyer : "PENDING APPROVAL";
                entities.StatPurchasingManager = reader["PurchasingManagerName"] != DBNull.Value ? CryptorEngine.Decrypt(reader["PurchasingManagerName"].ToString(), true).ToString() + "<br/>" + doaPurchasingManager : "PENDING APPROVAL";

                entities.RhSupplier = reader["SupplierName"].ToString().ToUpper();
                entities.RhReason = reader["Reason"] != DBNull.Value ? reader["Reason"].ToString().ToUpper() : reader["OtherReason"].ToString().ToUpper();
                entities.RhOtherReason = reader["OtherReason"] != DBNull.Value ? reader["OtherReason"].ToString().ToUpper() : string.Empty;
                entities.RhType = reader["Type"] != DBNull.Value ? reader["Type"].ToString().ToUpper() : string.Empty;
                entities.RhAttention = reader["Attention"] != DBNull.Value ? reader["Attention"].ToString().ToUpper() : string.Empty;
                entities.RhAttachment1 = reader["Attachment1"] != DBNull.Value ? reader["Attachment1"].ToString() : string.Empty;
                entities.RhAttachment2 = reader["Attachment2"] != DBNull.Value ? reader["Attachment2"].ToString() : string.Empty;
                entities.RhSupplierEmail = reader["SupplierEmail"].ToString();
                entities.RhSupplierId = reader["SupplierId"].ToString();
                entities.RhStockLifeAttachment = reader["StockLifeAttachment"] != DBNull.Value ? reader["StockLifeAttachment"].ToString() : string.Empty;

                entities.StatSTATProdSecManager = reader["STATProdSecManager"].ToString();
                entities.StatSTATProdDeptManager = reader["STATProdDeptManager"].ToString();
                entities.StatSTATProdDivManager = reader["STATProdDivManager"].ToString();
                entities.StatSTATProdHQManager = reader["STATProdHQManager"].ToString();
                entities.StatSTATPurchasingBuyer = reader["STATPurchasingBuyer"].ToString();
                entities.StatSTATPurchasingManager = reader["STATPurchasingManager"].ToString();

                entities.BuyerSend_SendBy = reader["BuyerSend_SendBy"] != DBNull.Value ? reader["BuyerSend_SendBy"].ToString().ToUpper() : string.Empty;
                entities.BuyerSend_SendReceivedDate = reader["BuyerSend_SendReceivedDate"] != DBNull.Value ? reader["BuyerSend_SendReceivedDate"].ToString().ToUpper() : string.Empty;
                entities.SupplierResponse_Date = reader["SupplierResponse_Date"] != DBNull.Value ? reader["SupplierResponse_Date"].ToString().ToUpper() : string.Empty;

                entities.DisapprovalCause = reader["DisapprovalCause"] != DBNull.Value ? reader["DisapprovalCause"].ToString() : string.Empty;

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

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_GetRequestDetailsByCTRLNo2(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        int rowNum = 1;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "URF_TRANSACTION_GetRequestDetailsByCTRLNo2";
            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.RdCtrlNo = entity.RdCtrlNo.Length <= 0 ? string.Empty : entity.RdCtrlNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

                entities.RowNumber = rowNum.ToString();
                entities.RdRefId = reader["RefId"].ToString();
                entities.RdCtrlNo = reader["CTRLNo"].ToString();
                entities.RdPONO = reader["PONO"] != DBNull.Value ? reader["PONO"].ToString() : string.Empty;
                entities.RdPRNO = reader["PRNO"] != DBNull.Value ? reader["PRNO"].ToString() : string.Empty;
                entities.RdItemName = reader["ItemName"] != DBNull.Value ? reader["ItemName"].ToString() : string.Empty;
                entities.RdSpecs = reader["Specs"] != DBNull.Value ? reader["Specs"].ToString() : string.Empty;
                entities.RdQuantity = reader["Quantity"] != DBNull.Value ? reader["Quantity"].ToString() : string.Empty;
                entities.RdUnitOfMeasure = reader["UnitOfMeasure"] != DBNull.Value ? reader["UnitOfMeasure"].ToString() : string.Empty;
                entities.RdDeliveryConfirmationDate = reader["DeliveryConfirmationDate"] != DBNull.Value ? reader["DeliveryConfirmationDate"].ToString() : string.Empty;
                entities.RdRequestedDeliveryDate = reader["RequestedDeliveryDate"] != DBNull.Value ? reader["RequestedDeliveryDate"].ToString() : string.Empty;
                //if (reader["RepDelDate"] != DBNull.Value || !string.IsNullOrEmpty(reader["RepDelDate"].ToString()))
                //{
                //    entities.RdReplyDeliveryDate = reader["ReplyDeliveryDate"] != DBNull.Value ? reader["ReplyDeliveryDate"].ToString() : string.Empty;
                //}
                //else
                //{
                //    entities.RdReplyDeliveryDate = "CLOSED";
                //}

                if (reader["RepDelDate"] != DBNull.Value || !string.IsNullOrEmpty(reader["RepDelDate"].ToString()))
                {
                    entities.RdReplyDeliveryDate = reader["ReplyDeliveryDate"] != DBNull.Value ? reader["ReplyDeliveryDate"].ToString() : string.Empty;
                    //entities.FontColor = "Black";
                }
                else
                {
                    if (string.IsNullOrEmpty(reader["PONO"].ToString()) || reader["PONO"] == DBNull.Value)
                    {
                        entities.RdReplyDeliveryDate = "NO PO YET";
                    }

                    if (!string.IsNullOrEmpty(reader["PONO"].ToString()) && !string.IsNullOrEmpty(reader["PRNO"].ToString()))
                    {
                        List<Entities_URF_RequestEntry> entitiesPO = new List<Entities_URF_RequestEntry>();
                        entitiesPO = vewRFQDelivery("SELECT * FROM [ROPROS].[dbo].[vewRFQDelivery] WITH (NOLOCK) WHERE PONO = '" + reader["PONO"].ToString() + "' AND PRNO = '" + reader["PRNO"].ToString() + "'", string.Empty);

                        if (entitiesPO.Count <= 0)
                        {
                            //entities.RdReplyDeliveryDate = "DELIVERED";
                            entities.RdReplyDeliveryDate = "CLOSED";
                        }
                    }

                }
             
                list.Add(entities);
                rowNum++;
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_GetRequestDetailsByCTRLNo3(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;
        int rowNum = 1;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "URF_TRANSACTION_GetRequestDetailsByCTRLNo3";
            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.RdCtrlNo = entity.RdCtrlNo.Length <= 0 ? string.Empty : entity.RdCtrlNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

                entities.RowNumber = rowNum.ToString();
                entities.RdRefId = reader["RefId"].ToString();
                entities.RdCtrlNo = reader["CTRLNo"].ToString();
                entities.RdPONO = reader["PONO"] != DBNull.Value ? reader["PONO"].ToString() : string.Empty;
                entities.RdPRNO = reader["PRNO"] != DBNull.Value ? reader["PRNO"].ToString() : string.Empty;
                entities.RdItemName = reader["ItemName"] != DBNull.Value ? reader["ItemName"].ToString() : string.Empty;
                entities.RdSpecs = reader["Specs"] != DBNull.Value ? reader["Specs"].ToString() : string.Empty;
                entities.RdQuantity = reader["Quantity"] != DBNull.Value ? reader["Quantity"].ToString() : string.Empty;
                entities.RdUnitOfMeasure = reader["UOM"] != DBNull.Value ? reader["UOM"].ToString() : string.Empty;
                entities.RdDeliveryConfirmationDate = reader["DeliveryConfirmationDate"] != DBNull.Value ? reader["DeliveryConfirmationDate"].ToString() : string.Empty;
                entities.RdRequestedDeliveryDate = reader["RequestedDeliveryDate"] != DBNull.Value ? reader["RequestedDeliveryDate"].ToString() : string.Empty;
                entities.RdReplyDeliveryDate = reader["ReplyDeliveryDate"] != DBNull.Value ? reader["ReplyDeliveryDate"].ToString() : string.Empty;

                list.Add(entities);
                rowNum++;
            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_GetSupplierAttachmentByCTRLNo(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "URF_TRANSACTION_GetSupplierAttachmentByCTRLNo";
            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.RhCtrlNo = entity.RhCtrlNo.Length <= 0 ? string.Empty : entity.RhCtrlNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

                entities.RhCtrlNo = reader["CTRLNo"].ToString();
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

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_GetExistingRequestByPONumber(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "URF_TRANSACTION_GetExistingRequestByPONumber";
            cmd.Parameters.Add(Factory.CreateParameter("@pono", entity.RdPONO = entity.RdPONO.Length <= 0 ? string.Empty : entity.RdPONO));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

                entities.RdPONO = reader["PONO"].ToString();
                entities.RdCtrlNo = reader["CTRLNo"].ToString();

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

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_GetExistingRequestByPONumber_ApprovedRequest(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "URF_TRANSACTION_GetExistingRequestByPONumber_ApprovedRequest";
            cmd.Parameters.Add(Factory.CreateParameter("@pono", entity.RdPONO = entity.RdPONO.Length <= 0 ? string.Empty : entity.RdPONO));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

                entities.RdPONO = reader["PONO"].ToString();
                entities.RdCtrlNo = reader["CTRLNo"].ToString();
                entities.StatSTATPurchasingBuyer = reader["STATPurchasingBuyer"].ToString();
                entities.StatSTATPurchasingManager = reader["STATPurchasingManager"].ToString();
                entities.StatSTATProdHQManager = reader["STATProdHQManager"].ToString();
                entities.StatSTATProdDivManager = reader["STATProdDivManager"].ToString();
                entities.StatSTATProdDeptManager = reader["STATProdDeptManager"].ToString();
                entities.StatSTATProdSecManager = reader["STATProdSecManager"].ToString();

                
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

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_GetRequestHeadByCTRLNo(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "URF_TRANSACTION_GetRequestHeadByCTRLNo";
            cmd.Parameters.Add(Factory.CreateParameter("@ctrlno", entity.RhCtrlNo = entity.RhCtrlNo.Length <= 0 ? string.Empty : entity.RhCtrlNo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

                entities.RhCtrlNo = reader["CTRLNo"].ToString();
                entities.RhCategory = reader["Category"].ToString();
                entities.RhSupplier = reader["Supplier"].ToString();
                entities.RhType = reader["Type"].ToString();
                entities.RhAttention = reader["Attention"].ToString();
                entities.RhReason = reader["Reason"].ToString();
                entities.RhOtherReason = reader["OtherReason"].ToString();
                entities.RhSupplierComments = reader["SupplierComments"].ToString();
                entities.RhPurchasingRemarks = reader["PurchasingRemarks"].ToString();
                entities.RhTransactionDate = reader["TransactionDate"].ToString();
                entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true);
                entities.RhAttachment1 = reader["Attachment1"].ToString();
                entities.RhAttachment2 = reader["Attachment2"].ToString();
                entities.RhTransactionDate = reader["TransactionDate"].ToString();
                entities.RepiStock = (reader["RepiStock"] != DBNull.Value ? reader["RepiStock"].ToString() : string.Empty);
                entities.DailyUsage = (reader["DailyUsage"] != DBNull.Value ? reader["DailyUsage"].ToString() : string.Empty);
                entities.StockLife = (reader["StockLife"] != DBNull.Value ? reader["StockLife"].ToString() : string.Empty);
                entities.RhRemarks = (reader["Remarks"] != DBNull.Value ? reader["Remarks"].ToString() : string.Empty);
                
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

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_GetHirarchyAccess(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText =
                            "SELECT LC.Refid, LC.Username, LC.FullName, LC.Department, UA.[Transaction] FROM Login_Credentials LC WITH (NOLOCK) " +
                            "INNER JOIN UserAccess UA WITH (NOLOCK) ON LC.RefId = UA.LoginId " +
                            "WHERE UA.[Transaction] IN ('401','402','403','404') AND LC.Department = '" + entity.LcDepartment + "'";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();
                entity.HirarchyDepartment = reader["Department"].ToString();
                entity.HirarchyAccess = reader["Transaction"].ToString();

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

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_Approval_DateRange(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText =
                            "SELECT	HEAD.CTRLNo, HEAD.TransactionDate, HEAD.Category, CATEGORY.Description AS CategoryName, LC.FullName AS Requester, LC.Department, LC.Division, LC.HQ, SUPPLIER.Name AS SupplierName, " +
                            "LC.EmailAddress AS RequesterEmail, " +
                            "STATUS.ProdSecManager, STATUS.DOAProdSecManager, STATUS.STATProdSecManager, " +
                            "STATUS.ProdDeptManager, STATUS.DOAProdDeptManager, STATUS.STATProdDeptManager, " +
                            "STATUS.ProdDivManager, STATUS.DOAProdDivManager, STATUS.STATProdDivManager, " +
                            "STATUS.ProdHQManager, STATUS.DOAProdHQManager, STATUS.STATProdHQManager, " +
                            "STATUS.PurchasingBuyer, STATUS.DOAPurchasingBuyer, STATUS.STATPurchasingBuyer, " +
                            "STATUS.PurchasingManager, STATUS.DOAPurchasingManager, STATUS.STATPurchasingManager, " +
                            "(SELECT COUNT(PONO) FROM URF_TRANSACTION_RequestDetails WITH (NOLOCK) WHERE CTRLNo = HEAD.CTRLNo AND (PONO IS NULL OR LEN(PONO) <= 0)) AS EmptyPO, " +
                            "(SELECT TOP 1 TransactionType FROM URF_TRANSACTION_SendReceived WHERE CTRLNo = HEAD.CTRLNo ORDER BY SendReceivedDate DESC) AS SendReceived, " +
                            "( " +
	                            "SELECT TOP 1 UA.LoginId FROM Login_Credentials LC2 WITH (NOLOCK) " +
	                            "INNER JOIN UserAccess UA WITH (NOLOCK) ON LC2.RefId = UA.LoginId " +
	                            "WHERE UA.[Transaction] = 401 AND LC2.Section = LC.Section " +
	                            "AND UA.LoginId = LC2.RefId " +
                            ") AS SecManagerApprover, " +
                            "( " +
	                            "SELECT TOP 1 UA.LoginId FROM Login_Credentials LC2 WITH (NOLOCK) " +
	                            "INNER JOIN UserAccess UA WITH (NOLOCK) ON LC2.RefId = UA.LoginId " +
	                            "WHERE UA.[Transaction] = 402 AND LC2.Department = LC.Department " +
	                            "AND UA.LoginId = LC2.RefId " +
                            ") AS DeptManagerApprover, " +
                            "( " +
	                            "SELECT TOP 1 UA.LoginId FROM Login_Credentials LC2 WITH (NOLOCK) " +
	                            "INNER JOIN UserAccess UA WITH (NOLOCK) ON LC2.RefId = UA.LoginId " +
	                            "WHERE UA.[Transaction] = 403 AND LC2.Division = LC.Division " +
	                            "AND UA.LoginId = LC2.RefId " +
                            ") AS DivManagerApprover, " +
                            "( " +
	                            "SELECT TOP 1 UA.LoginId FROM Login_Credentials LC2 WITH (NOLOCK) " +
	                            "INNER JOIN UserAccess UA WITH (NOLOCK) ON LC2.RefId = UA.LoginId " +
	                            "WHERE UA.[Transaction] = 404 AND LC2.HQ = LC.HQ " +
	                            "AND UA.LoginId = LC2.RefId " +
                            ") AS HqManagerApprover " +
                            "FROM URF_TRANSACTION_RequestHead HEAD " +
                            "INNER JOIN URF_TRANSACTION_RequestStatus STATUS ON HEAD.CTRLNo = STATUS.CTRLNo " +
                            "INNER JOIN MT_Category CATEGORY ON HEAD.Category = CATEGORY.RefId " +
                            "INNER JOIN Login_Credentials LC ON HEAD.Requester = LC.RefId " +
                            "INNER JOIN MT_Supplier_Head SUPPLIER WITH (NOLOCK) ON HEAD.Supplier = SUPPLIER.RefId " +
                            "WHERE " + entity.ProdArrayApprovalField +
                            "  AND CONVERT(DATE, HEAD.TransactionDate) >= '" + entity.DrFrom + "' AND CONVERT(DATE, HEAD.TransactionDate) <= '" + entity.DrTo +
                            "' AND STATUS.STATClosed IS NULL OR STATUS.STATClosed = '0' " +
                            " ORDER BY HEAD.TransactionDate DESC ";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

                string prodSecManager = reader["STATProdSecManager"].ToString();
                string prodDeptManager = reader["STATProdDeptManager"].ToString();
                string prodDivManager = reader["STATProdDivManager"].ToString();
                string prodHQManager = reader["STATProdHQManager"].ToString();
                string purchasingBuyer = reader["STATPurchasingBuyer"].ToString();
                string purchasingManager = reader["STATPurchasingManager"].ToString();

                entities.RhCtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.RhCategory = reader["Category"].ToString().ToUpper();
                entities.RhCategoryName = reader["CategoryName"].ToString();
                entities.RhTransactionDate = reader["TransactionDate"].ToString();
                entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true);
                entities.LcDepartment = reader["Department"].ToString();
                entities.LcDivision = reader["Division"].ToString();
                entities.LcHQ = reader["HQ"].ToString();
                entities.RhSupplier = reader["SupplierName"].ToString();

                entities.StatSTATProdSecManager = reader["STATProdSecManager"].ToString();
                entities.StatSTATProdDeptManager = reader["STATProdDeptManager"].ToString();
                entities.StatSTATProdDivManager = reader["STATProdDivManager"].ToString();
                entities.StatSTATProdHQManager = reader["STATProdHQManager"].ToString();
                entities.StatPurchasingBuyer = reader["STATPurchasingBuyer"].ToString();
                entities.StatPurchasingManager = reader["STATPurchasingManager"].ToString();
                entities.RhSendReceived = reader["SendReceived"] != DBNull.Value ? reader["SendReceived"].ToString() : string.Empty;

                entities.EmptyPO = reader["EmptyPO"].ToString();

                entities.RhRequesterEmail = reader["RequesterEmail"] != DBNull.Value ? reader["RequesterEmail"].ToString() : string.Empty;

                if (prodSecManager == "0")
                {
                    entities.StatAll = "FOR SEC. MNGR APPROVAL";
                    entities.CssColorCode = "#f44336";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && prodDeptManager == "0")
                {
                    entities.StatAll = "FOR DEPT. MNGR APPROVAL";
                    entities.CssColorCode = "#9C27B0";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && prodDivManager == "0")
                {
                    entities.StatAll = "FOR DIV. MNGR APPROVAL";
                    entities.CssColorCode = "#673AB7";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && (prodDivManager == "1" || prodDivManager == "-1") && prodHQManager == "0")
                {
                    entities.StatAll = "FOR HQ MNGR APPROVAL";
                    entities.CssColorCode = "#009688";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && (prodDivManager == "1" || prodDivManager == "-1") && (prodHQManager == "1" || prodHQManager == "-1") && purchasingBuyer == "0")
                {
                    entities.StatAll = "FOR PUR BUYER APPROVAL";
                    entities.CssColorCode = "#8BC34A";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && (prodDivManager == "1" || prodDivManager == "-1") && (prodHQManager == "1" || prodHQManager == "-1") && purchasingBuyer == "1" && purchasingManager == "0")
                {
                    entities.StatAll = "FOR PUR MANAGER APPROVAL";
                    //entities.CssColorCode = "#CDDC39";
                    entities.CssColorCode = "Black";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && (prodDivManager == "1" || prodDivManager == "-1") && (prodHQManager == "1" || prodHQManager == "-1") && purchasingBuyer == "1" && purchasingManager == "1")
                {
                    entities.StatAll = "FOR SENDING";
                    entities.CssColorCode = "#8BC34A";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && (prodDivManager == "1" || prodDivManager == "-1") && (prodHQManager == "1" || prodHQManager == "-1") && purchasingBuyer == "1" && purchasingManager == "1" && entities.RhSendReceived == "RECEIVED")
                {
                    entities.StatAll = "SUPPLIER RESPONDED";
                    entities.CssColorCode = "#673AB7";
                }
                if (prodSecManager == "2" || prodDeptManager == "2" || prodDivManager == "2" || prodHQManager == "2" || purchasingBuyer == "2" || purchasingManager == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
                }

                ////START - ADDED 05/15/2023
                ////-----------------------------------------------------------------------------------------------------------------------------------------------
                //int stillExistingInPurchaseInquiry = 0;
                //int hasEmptyPO = 0;

                //List<Entities_URF_RequestEntry> listNotInPurchaseInquiry = new List<Entities_URF_RequestEntry>();
                //listNotInPurchaseInquiry = viewNotInPurchaseInquiry(entities.RhCtrlNo);

                //if (listNotInPurchaseInquiry.Count > 0)
                //{
                //    foreach (Entities_URF_RequestEntry eListNotInPurchaseInquiry in listNotInPurchaseInquiry)
                //    {
                //        if (!string.IsNullOrEmpty(eListNotInPurchaseInquiry.RdPONO) && !string.IsNullOrEmpty(eListNotInPurchaseInquiry.RdPRNO))
                //        {

                //            List<Entities_URF_RequestEntry> listNotInPurchaseInquiry2 = new List<Entities_URF_RequestEntry>();
                //            listNotInPurchaseInquiry2 = vewRFQDelivery("SELECT * FROM [ROPROS].[dbo].[vewRFQDelivery] WITH (NOLOCK) WHERE PONO = '" + eListNotInPurchaseInquiry.RdPONO + "' AND PRNO = '" + eListNotInPurchaseInquiry.RdPRNO + "'", string.Empty);

                //            if (listNotInPurchaseInquiry2.Count > 0)
                //            {
                //                stillExistingInPurchaseInquiry++;
                //            }
                //        }
                //        else
                //        {
                //            hasEmptyPO++;
                //        }

                //    }
                //}


                //if (stillExistingInPurchaseInquiry > 0 || hasEmptyPO > 0)
                //{
                //    list.Add(entities);
                //}

                ////-----------------------------------------------------------------------------------------------------------------------------------------------
                ////END - ADDED 05/15/2023

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

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_Receiving_DateRange(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 500;
            cmd.CommandText =
                            "SELECT	HEAD.CTRLNo, HEAD.BuyerRemarks, HEAD.TransactionDate, HEAD.Category, CATEGORY.Description AS CategoryName, LC.FullName AS Requester, LC.Department, " +
                            "STATUS.ProdSecManager, STATUS.DOAProdSecManager, STATUS.STATProdSecManager, " +
                            "STATUS.ProdDeptManager, STATUS.DOAProdDeptManager, STATUS.STATProdDeptManager, " +
                            "STATUS.ProdDivManager, STATUS.DOAProdDivManager, STATUS.STATProdDivManager, " +
                            "STATUS.ProdHQManager, STATUS.DOAProdHQManager, STATUS.STATProdHQManager, " +
                            "STATUS.PurchasingBuyer, STATUS.DOAPurchasingBuyer, STATUS.STATPurchasingBuyer, " +
                            "(SELECT Name FROM URF_MT_Reason WITH (NOLOCK) WHERE RefId = HEAD.Reason) AS ReasonName, HEAD.OtherReason, " +
                            "STATUS.PurchasingManager, STATUS.DOAPurchasingManager, STATUS.STATPurchasingManager, SUPPLIER.Name AS SupplierName, " +
                            "(SELECT TOP 1 TransactionType FROM URF_TRANSACTION_SendReceived WHERE CTRLNo = HEAD.CTRLNo ORDER BY SendReceivedDate DESC) AS SendReceived, " +
                            "STATUS.StatClosed, STATUS.ReOpenRemarks " +
                            "FROM URF_TRANSACTION_RequestHead HEAD " +
                            "INNER JOIN URF_TRANSACTION_RequestStatus STATUS ON HEAD.CTRLNo = STATUS.CTRLNo " +
                            "INNER JOIN MT_Category CATEGORY ON HEAD.Category = CATEGORY.RefId " +
                            "INNER JOIN MT_Supplier_Head SUPPLIER ON HEAD.Supplier = SUPPLIER.RefId " + 
                            "INNER JOIN Login_Credentials LC ON HEAD.Requester = LC.RefId " +
                            "WHERE STATUS.STATPurchasingManager = 1 AND (STATUS.STATClosed IS NULL OR STATUS.STATClosed = 2)" +
                            "  AND CONVERT(DATE, HEAD.TransactionDate) >= '" + entity.DrFrom + "' AND CONVERT(DATE, HEAD.TransactionDate) <= '" + entity.DrTo +
                            "' ORDER BY HEAD.TransactionDate DESC ";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

                string prodSecManager = reader["STATProdSecManager"].ToString();
                string prodDeptManager = reader["STATProdDeptManager"].ToString();
                string prodDivManager = reader["STATProdDivManager"].ToString();
                string prodHQManager = reader["STATProdHQManager"].ToString();
                string purchasingBuyer = reader["STATPurchasingBuyer"].ToString();
                string purchasingManager = reader["STATPurchasingManager"].ToString();
                string statClosed = reader["STATClosed"] != DBNull.Value ? reader["STATClosed"].ToString() : string.Empty;

                entities.RhCtrlNo = reader["CtrlNo"].ToString().ToUpper();
                entities.RhCategory = reader["Category"].ToString().ToUpper();
                entities.RhCategoryName = reader["CategoryName"].ToString();
                entities.RhTransactionDate = reader["TransactionDate"].ToString();
                entities.RhRequester = CryptorEngine.Decrypt(reader["Requester"].ToString(), true);
                entities.LcDepartment = reader["Department"].ToString();
                entities.RhSendReceived = reader["SendReceived"] != DBNull.Value ? reader["SendReceived"].ToString() : string.Empty;
                
                entities.StatSTATProdSecManager = reader["STATProdSecManager"].ToString();
                entities.StatSTATProdDeptManager = reader["STATProdDeptManager"].ToString();
                entities.StatSTATProdDivManager = reader["STATProdDivManager"].ToString();
                entities.StatSTATProdHQManager = reader["STATProdHQManager"].ToString();
                entities.StatClosed = reader["STATClosed"] != DBNull.Value ? reader["STATClosed"].ToString() : string.Empty;
                entities.StatReOpenRemarks = reader["ReOpenRemarks"] != DBNull.Value ? reader["ReOpenRemarks"].ToString() : string.Empty;

                entities.RhSupplierName = reader["SupplierName"].ToString();
                entities.RhReasonName = string.IsNullOrEmpty(reader["ReasonName"].ToString()) ? reader["OtherReason"].ToString() : reader["ReasonName"].ToString();

                if (prodSecManager == "0")
                {
                    entities.StatAll = "FOR SEC. MNGR APPROVAL";
                    entities.CssColorCode = "#f44336";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && prodDeptManager == "0")
                {
                    entities.StatAll = "FOR DEPT. MNGR APPROVAL";
                    entities.CssColorCode = "#9C27B0";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && prodDivManager == "0")
                {
                    entities.StatAll = "FOR DIV. MNGR APPROVAL";
                    entities.CssColorCode = "#673AB7";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && (prodDivManager == "1" || prodDivManager == "-1") && prodHQManager == "0")
                {
                    entities.StatAll = "FOR HQ MNGR APPROVAL";
                    entities.CssColorCode = "#009688";
                }
                //if (prodSecManager == "1" && prodDeptManager == "1" && prodDivManager == "1" && prodHQManager == "1" && purchasingBuyer == "0" && string.IsNullOrEmpty(entities.RhSendReceived))
                //{
                //    entities.StatAll = "FOR SENDING";
                //    entities.CssColorCode = "#8BC34A";
                //}
                if ((prodSecManager == "1" || prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && (prodDivManager == "1" || prodDivManager == "-1") && (prodHQManager == "1" || prodHQManager == "-1") && purchasingBuyer == "1" && purchasingManager == "1" && entities.RhSendReceived == "SEND")
                {
                    entities.StatAll = "FOR RESEND";
                    entities.CssColorCode = "#673AB7";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && (prodDivManager == "1" || prodDivManager == "-1") && (prodHQManager == "1" || prodHQManager == "-1") && purchasingBuyer == "0" && entities.RhSendReceived == "RECEIVED")
                {
                    entities.StatAll = "SUPPLIER RESPONDED";
                    entities.CssColorCode = "#009688";
                }
                if ((prodSecManager == "1" ||prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && (prodDivManager == "1" || prodDivManager == "-1") && (prodHQManager == "1" || prodHQManager == "-1") && purchasingBuyer == "1" && purchasingManager == "0")
                {
                    entities.StatAll = "FOR PUR MANAGER APPROVAL";
                    entities.CssColorCode = "#CDDC39";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && (prodDivManager == "1" || prodDivManager == "-1") && (prodHQManager == "1" || prodHQManager == "-1") && purchasingBuyer == "1" && purchasingManager == "1" && string.IsNullOrEmpty(entities.RhSendReceived))
                {
                    entities.StatAll = "FOR SENDING";
                    entities.CssColorCode = "#8BC34A";
                }
                if ((prodSecManager == "1" || prodSecManager == "-1") && (prodDeptManager == "1" || prodDeptManager == "-1") && (prodDivManager == "1" || prodDivManager == "-1") && (prodHQManager == "1" || prodHQManager == "-1") && purchasingBuyer == "1" && purchasingManager == "1" && entities.RhSendReceived == "RECEIVED")
                {
                    entities.StatAll = "SUPPLIER RESPONDED";
                    entities.CssColorCode = "#009688";
                }
                if (prodSecManager == "2" || prodDeptManager == "2" || prodDivManager == "2" || prodHQManager == "2" || purchasingBuyer == "2" || purchasingManager == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                    entities.CssColorCode = "#ffbb33";
                }
                //if (purchasingManager == "1")
                //{
                //    entities.StatAll = "APPROVED";
                //    entities.CssColorCode = "#00C851";
                //}
                if (statClosed == "2")
                {
                    entities.StatAll = "RE-OPEN";
                    entities.CssColorCode = "#9C27B0";
                }

                entities.BuyerRemarks = reader["BuyerRemarks"] != DBNull.Value ? reader["BuyerRemarks"].ToString() : string.Empty;

                //-----------------------------------------------------------------------------------------------------------------------------------------------
                int stillExistingInPurchaseInquiry = 0;
                int hasEmptyPO = 0;

                List<Entities_URF_RequestEntry> listNotInPurchaseInquiry = new List<Entities_URF_RequestEntry>();
                listNotInPurchaseInquiry = viewNotInPurchaseInquiry(entities.RhCtrlNo);

                if (listNotInPurchaseInquiry.Count > 0)
                {
                    foreach (Entities_URF_RequestEntry eListNotInPurchaseInquiry in listNotInPurchaseInquiry)
                    {
                        if (!string.IsNullOrEmpty(eListNotInPurchaseInquiry.RdPONO) && !string.IsNullOrEmpty(eListNotInPurchaseInquiry.RdPRNO))
                        {
                            //Common cmn = new Common();

                            //if (cmn.isNumeric(eListNotInPurchaseInquiry.RdPONO, System.Globalization.NumberStyles.Number) && cmn.isNumeric(eListNotInPurchaseInquiry.RdPRNO, System.Globalization.NumberStyles.Number))
                            //{
                            //    List<Entities_URF_RequestEntry> listNotInPurchaseInquiry2 = new List<Entities_URF_RequestEntry>();
                            //    //listNotInPurchaseInquiry2 = viewNotInPurchaseInquiry2(eListNotInPurchaseInquiry.RdPONO, eListNotInPurchaseInquiry.RdPRNO);
                            //    listNotInPurchaseInquiry2 = vewRFQDelivery("SELECT * FROM [ROPROS].[dbo].[vewRFQDelivery] WITH (NOLOCK) WHERE PONO = '" + eListNotInPurchaseInquiry.RdPONO + "' AND PRNO = '" + eListNotInPurchaseInquiry.RdPRNO + "'", string.Empty);

                            //    if (listNotInPurchaseInquiry2.Count > 0)
                            //    {
                            //        stillExistingInPurchaseInquiry++;
                            //    }
                            //}
                            List<Entities_URF_RequestEntry> listNotInPurchaseInquiry2 = new List<Entities_URF_RequestEntry>();
                            //listNotInPurchaseInquiry2 = viewNotInPurchaseInquiry2(eListNotInPurchaseInquiry.RdPONO, eListNotInPurchaseInquiry.RdPRNO);
                            listNotInPurchaseInquiry2 = vewRFQDelivery("SELECT * FROM [ROPROS].[dbo].[vewRFQDelivery] WITH (NOLOCK) WHERE PONO = '" + eListNotInPurchaseInquiry.RdPONO + "' AND PRNO = '" + eListNotInPurchaseInquiry.RdPRNO + "'", string.Empty);

                            if (listNotInPurchaseInquiry2.Count > 0)
                            {
                                stillExistingInPurchaseInquiry++;
                            }
                        }
                        else
                        {
                            hasEmptyPO++;
                        }

                    }
                }

                if (stillExistingInPurchaseInquiry <= 0)
                {
                    //UPDATE REQUEST TO AUTO-CLOSE
                    string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                    string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                    string query1 = string.Empty;

                    query1 = "UPDATE URF_TRANSACTION_RequestStatus SET STATClosed = '1', PostingRemarks = 'SYSTEM AUTO-CLOSED' WHERE CTRLNo ='" + reader["CtrlNo"].ToString().ToUpper() + "' ";

                    URF_TRANSACTION_SQLTransaction(queryBeginPart + query1 + queryEndPart);
                }
                else
                {
                    //BACK TO OPEN
                    string queryBeginPart2 = "BEGIN TRY BEGIN TRANSACTION ";
                    string queryEndPart2 = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                    string query12 = string.Empty;

                    query12 = "UPDATE URF_TRANSACTION_RequestStatus SET STATClosed = NULL, PostingRemarks = NULL WHERE CTRLNo ='" + reader["CtrlNo"].ToString().ToUpper() + "' ";

                    URF_TRANSACTION_SQLTransaction(queryBeginPart2 + query12 + queryEndPart2);
                }



                if (stillExistingInPurchaseInquiry > 0 || hasEmptyPO > 0)
                {
                    list.Add(entities);
                }

                //-----------------------------------------------------------------------------------------------------------------------------------------------

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

    public int URF_TRANSACTION_SQLTransaction(string query)
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

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_AllRequest(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "URF_TRANSACTION_AllRequest";
            cmd.Parameters.Add(Factory.CreateParameter("@criteria", entity.Criteria = entity.Criteria.Length <= 0 ? string.Empty : entity.Criteria));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

                entities.RdCtrlNo = reader["CTRLNo"].ToString();
                entities.RdPONO = reader["PONO"] != DBNull.Value ? reader["PONO"].ToString() : string.Empty;
                entities.RdPRNO = reader["PRNO"] != DBNull.Value ? reader["PRNO"].ToString() : string.Empty;
                entities.RdItemName = reader["ItemName"] != DBNull.Value ? reader["ItemName"].ToString() : string.Empty;
                entities.RdSpecs = reader["Specs"] != DBNull.Value ? reader["Specs"].ToString() : string.Empty;
                entities.RdQuantity = reader["Quantity"] != DBNull.Value ? reader["Quantity"].ToString() : string.Empty;
                entities.RdUnitOfMeasure = reader["UnitOfMeasure"] != DBNull.Value ? reader["UnitOfMeasure"].ToString() : string.Empty;
                entities.RdDeliveryConfirmationDate = reader["DeliveryConfirmationDate"] != DBNull.Value ? reader["DeliveryConfirmationDate"].ToString() : string.Empty;
                entities.RdRequestedDeliveryDate = reader["RequestedDeliveryDate"] != DBNull.Value ? reader["RequestedDeliveryDate"].ToString() : string.Empty;
                entities.RdReplyDeliveryDate = reader["ReplyDeliveryDate"] != DBNull.Value ? reader["ReplyDeliveryDate"].ToString() : string.Empty;
                entities.RhCategoryName = reader["CategoryName"].ToString();
                entities.RhDivision = reader["Division"].ToString();

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

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_AllRequest_New(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "URF_TRANSACTION_AllRequest";
            cmd.CommandTimeout = 500;
            cmd.Parameters.Add(Factory.CreateParameter("@criteria", entity.Criteria = entity.Criteria.Length <= 0 ? string.Empty : entity.Criteria));
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

                entities.RdCtrlNo = reader["CTRLNo"].ToString();
                entities.RdPONO = reader["PONO"] != DBNull.Value ? reader["PONO"].ToString() : string.Empty;
                entities.RdPRNO = reader["PRNO"] != DBNull.Value ? reader["PRNO"].ToString() : string.Empty;
                entities.RdItemName = reader["ItemName"] != DBNull.Value ? reader["ItemName"].ToString() : string.Empty;
                entities.RdSpecs = reader["Specs"] != DBNull.Value ? reader["Specs"].ToString() : string.Empty;
                entities.RdQuantity = reader["Quantity"] != DBNull.Value ? reader["Quantity"].ToString() : string.Empty;
                entities.RdUnitOfMeasure = reader["UnitOfMeasure"] != DBNull.Value ? reader["UnitOfMeasure"].ToString() : string.Empty;
                entities.RdDeliveryConfirmationDate = reader["DeliveryConfirmationDate"] != DBNull.Value ? reader["DeliveryConfirmationDate"].ToString() : string.Empty;
                entities.RdRequestedDeliveryDate = reader["RequestedDeliveryDate"] != DBNull.Value ? reader["RequestedDeliveryDate"].ToString() : string.Empty;
                entities.RdReplyDeliveryDate = reader["ReplyDeliveryDate"] != DBNull.Value ? reader["ReplyDeliveryDate"].ToString() : string.Empty;
                entities.RhCategoryName = reader["CategoryName"].ToString();
                entities.RhDivision = reader["Division"].ToString();
                entities.StatSTATProdSecManager = reader["STATProdSecManager"].ToString();
                entities.StatSTATProdDeptManager = reader["STATProdDeptManager"].ToString();
                entities.StatSTATProdDivManager = reader["STATProdDivManager"].ToString();
                entities.StatSTATProdHQManager = reader["STATProdHQManager"].ToString();
                entities.StatSTATPurchasingBuyer = reader["STATPurchasingBuyer"].ToString();
                entities.StatSTATPurchasingManager = reader["STATPurchasingManager"].ToString();
                entities.StatClosed = reader["STATClosed"] != DBNull.Value ? reader["STATClosed"].ToString() : string.Empty;

                entities.StatSend = reader["StatSend"] != DBNull.Value ? reader["StatSend"].ToString() : string.Empty;
                entities.StatReceived = reader["StatReceived"] != DBNull.Value ? reader["StatReceived"].ToString() : string.Empty;
                


                if (entities.StatSTATProdSecManager == "0")
                {
                    entities.StatAll = "FOR PROD. SECTION MNGR. APPROVAL";
                }
                if ((entities.StatSTATProdSecManager == "1" || entities.StatSTATProdSecManager == "-1") && entities.StatSTATProdDeptManager == "0")
                {
                    entities.StatAll = "FOR PROD. DEPARTMENT MNGR. APPROVAL";
                }
                if ((entities.StatSTATProdSecManager == "1" || entities.StatSTATProdSecManager == "-1") && (entities.StatSTATProdDeptManager == "1" || entities.StatSTATProdDeptManager == "-1") && entities.StatSTATProdDivManager == "0")
                {
                    entities.StatAll = "FOR PROD. DIVISION MNGR. APPROVAL";
                }
                if ((entities.StatSTATProdSecManager == "1" || entities.StatSTATProdSecManager == "-1") && (entities.StatSTATProdDeptManager == "1" || entities.StatSTATProdDeptManager == "-1") && (entities.StatSTATProdDivManager == "1" || entities.StatSTATProdDivManager == "-1") && entities.StatSTATProdHQManager == "0")
                {
                    entities.StatAll = "FOR PROD. HQ. MNGR. APPROVAL";
                }
                if ((entities.StatSTATProdSecManager == "1" || entities.StatSTATProdSecManager == "-1") && (entities.StatSTATProdDeptManager == "1" || entities.StatSTATProdDeptManager == "-1") && (entities.StatSTATProdDivManager == "1" || entities.StatSTATProdDivManager == "-1") && (entities.StatSTATProdHQManager == "1" || entities.StatSTATProdHQManager == "-1") && entities.StatSTATPurchasingBuyer == "0")
                {
                    entities.StatAll = "FOR SCD BUYER APPROVAL";
                }
                if ((entities.StatSTATProdSecManager == "1" || entities.StatSTATProdSecManager == "-1") && (entities.StatSTATProdDeptManager == "1" || entities.StatSTATProdDeptManager == "-1") && (entities.StatSTATProdDivManager == "1" || entities.StatSTATProdDivManager == "-1") && (entities.StatSTATProdHQManager == "1" || entities.StatSTATProdHQManager == "-1") && entities.StatSTATPurchasingBuyer == "1" && entities.StatSTATPurchasingManager == "0")
                {
                    entities.StatAll = "FOR SCD INCHARGE APPROVAL";
                }
                if ((entities.StatSTATProdSecManager == "1" || entities.StatSTATProdSecManager == "-1") && (entities.StatSTATProdDeptManager == "1" || entities.StatSTATProdDeptManager == "-1") && (entities.StatSTATProdDivManager == "1" || entities.StatSTATProdDivManager == "-1") && (entities.StatSTATProdHQManager == "1" || entities.StatSTATProdHQManager == "-1") && entities.StatSTATPurchasingBuyer == "1" && entities.StatSTATPurchasingManager == "1")
                {
                    entities.StatAll = "FOR SCD INCHARGE APPROVAL";
                }
                if ((entities.StatSTATProdSecManager == "1" || entities.StatSTATProdSecManager == "-1") && (entities.StatSTATProdDeptManager == "1" || entities.StatSTATProdDeptManager == "-1") && (entities.StatSTATProdDivManager == "1" || entities.StatSTATProdDivManager == "-1") && (entities.StatSTATProdHQManager == "1" || entities.StatSTATProdHQManager == "-1") && entities.StatSTATPurchasingBuyer == "1" && entities.StatSTATPurchasingManager == "1" && string.IsNullOrEmpty(entities.StatSend))
                {
                    entities.StatAll = "FOR SENDING";
                }
                if ((entities.StatSTATProdSecManager == "1" || entities.StatSTATProdSecManager == "-1") && (entities.StatSTATProdDeptManager == "1" || entities.StatSTATProdDeptManager == "-1") && (entities.StatSTATProdDivManager == "1" || entities.StatSTATProdDivManager == "-1") && (entities.StatSTATProdHQManager == "1" || entities.StatSTATProdHQManager == "-1") && entities.StatSTATPurchasingBuyer == "1" && entities.StatSTATPurchasingManager == "1" && entities.StatSend == "SEND" && string.IsNullOrEmpty(entities.StatReceived))
                {
                    entities.StatAll = "FOR RESEND / WAITING FOR RESPONSE";
                }
                if ((entities.StatSTATProdSecManager == "1" || entities.StatSTATProdSecManager == "-1") && (entities.StatSTATProdDeptManager == "1" || entities.StatSTATProdDeptManager == "-1") && (entities.StatSTATProdDivManager == "1" || entities.StatSTATProdDivManager == "-1") && (entities.StatSTATProdHQManager == "1" || entities.StatSTATProdHQManager == "-1") && entities.StatSTATPurchasingBuyer == "1" && entities.StatSTATPurchasingManager == "1" && entities.StatSend == "SEND" && entities.StatReceived == "RECEIVED")
                {
                    entities.StatAll = "SUPPLIER RESPONDED";
                }
                if (entities.StatSTATProdSecManager == "2" || entities.StatSTATProdDeptManager == "2" || entities.StatSTATProdDivManager == "2" || entities.StatSTATProdHQManager == "2" || entities.StatSTATPurchasingBuyer == "2" || entities.StatSTATPurchasingManager == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                }
                if (entities.StatClosed == "1")
                {
                    entities.StatAll = "CLOSED";
                }
                if (entities.StatClosed == "2")
                {
                    entities.StatAll = "RE-OPEN";
                }
                //if (entities.StatClosed == "2")
                //{
                //    entities.StatAll = "CLOSED";
                //}


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

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_AllRequest_Reporting(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "URF_TRANSACTION_AllRequest_Reporting";
            cmd.CommandTimeout = 500;
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

                entities.RhTransactionDate = reader["TransactionDate"].ToString();
                entities.RdCtrlNo = reader["CTRLNo"].ToString();
                entities.RdPONO = reader["PONO"] != DBNull.Value ? reader["PONO"].ToString() : string.Empty;
                entities.RdPRNO = reader["PRNO"] != DBNull.Value ? reader["PRNO"].ToString() : string.Empty;
                entities.RdItemName = reader["ItemName"] != DBNull.Value ? reader["ItemName"].ToString() : string.Empty;
                entities.RdSpecs = reader["Specs"] != DBNull.Value ? reader["Specs"].ToString() : string.Empty;
                entities.RdQuantity = reader["Quantity"] != DBNull.Value ? reader["Quantity"].ToString() : string.Empty;
                entities.RdUnitOfMeasure = reader["UnitOfMeasure"] != DBNull.Value ? reader["UnitOfMeasure"].ToString() : string.Empty;
                entities.RdDeliveryConfirmationDate = reader["DeliveryConfirmationDate"] != DBNull.Value ? reader["DeliveryConfirmationDate"].ToString() : string.Empty;
                entities.RdRequestedDeliveryDate = reader["RequestedDeliveryDate"] != DBNull.Value ? reader["RequestedDeliveryDate"].ToString() : string.Empty;
                entities.RdReplyDeliveryDate = reader["ReplyDeliveryDate"] != DBNull.Value ? reader["ReplyDeliveryDate"].ToString() : string.Empty;
                entities.RhCategoryName = reader["CategoryName"].ToString();
                entities.RhDivision = reader["Division"].ToString();
                entities.StatSTATProdSecManager = reader["STATProdSecManager"].ToString();
                entities.StatSTATProdDeptManager = reader["STATProdDeptManager"].ToString();
                entities.StatSTATProdDivManager = reader["STATProdDivManager"].ToString();
                entities.StatSTATProdHQManager = reader["STATProdHQManager"].ToString();
                entities.StatSTATPurchasingBuyer = reader["STATPurchasingBuyer"].ToString();
                entities.StatSTATPurchasingManager = reader["STATPurchasingManager"].ToString();
                entities.StatClosed = reader["STATClosed"] != DBNull.Value ? reader["STATClosed"].ToString() : string.Empty;

                entities.StatSend = reader["StatSend"] != DBNull.Value ? reader["StatSend"].ToString() : string.Empty;
                entities.StatReceived = reader["StatReceived"] != DBNull.Value ? reader["StatReceived"].ToString() : string.Empty;
                entities.Report_BuyerName = reader["BuyerName"] != null ? CryptorEngine.Decrypt(reader["BuyerName"].ToString().Replace(" ", "+"), true) : string.Empty;



                if (entities.StatSTATProdSecManager == "0")
                {
                    entities.StatAll = "FOR PROD. SECTION MNGR. APPROVAL";
                }
                if ((entities.StatSTATProdSecManager == "1" || entities.StatSTATProdSecManager == "-1") && entities.StatSTATProdDeptManager == "0")
                {
                    entities.StatAll = "FOR PROD. DEPARTMENT MNGR. APPROVAL";
                }
                if ((entities.StatSTATProdSecManager == "1" || entities.StatSTATProdSecManager == "-1") && (entities.StatSTATProdDeptManager == "1" || entities.StatSTATProdDeptManager == "-1") && entities.StatSTATProdDivManager == "0")
                {
                    entities.StatAll = "FOR PROD. DIVISION MNGR. APPROVAL";
                }
                if ((entities.StatSTATProdSecManager == "1" || entities.StatSTATProdSecManager == "-1") && (entities.StatSTATProdDeptManager == "1" || entities.StatSTATProdDeptManager == "-1") && (entities.StatSTATProdDivManager == "1" || entities.StatSTATProdDivManager == "-1") && entities.StatSTATProdHQManager == "0")
                {
                    entities.StatAll = "FOR PROD. HQ. MNGR. APPROVAL";
                }
                if ((entities.StatSTATProdSecManager == "1" || entities.StatSTATProdSecManager == "-1") && (entities.StatSTATProdDeptManager == "1" || entities.StatSTATProdDeptManager == "-1") && (entities.StatSTATProdDivManager == "1" || entities.StatSTATProdDivManager == "-1") && (entities.StatSTATProdHQManager == "1" || entities.StatSTATProdHQManager == "-1") && entities.StatSTATPurchasingBuyer == "0")
                {
                    entities.StatAll = "FOR SCD BUYER APPROVAL";
                }
                if ((entities.StatSTATProdSecManager == "1" || entities.StatSTATProdSecManager == "-1") && (entities.StatSTATProdDeptManager == "1" || entities.StatSTATProdDeptManager == "-1") && (entities.StatSTATProdDivManager == "1" || entities.StatSTATProdDivManager == "-1") && (entities.StatSTATProdHQManager == "1" || entities.StatSTATProdHQManager == "-1") && entities.StatSTATPurchasingBuyer == "1" && entities.StatSTATPurchasingManager == "0")
                {
                    entities.StatAll = "FOR SCD INCHARGE APPROVAL";
                }
                if ((entities.StatSTATProdSecManager == "1" || entities.StatSTATProdSecManager == "-1") && (entities.StatSTATProdDeptManager == "1" || entities.StatSTATProdDeptManager == "-1") && (entities.StatSTATProdDivManager == "1" || entities.StatSTATProdDivManager == "-1") && (entities.StatSTATProdHQManager == "1" || entities.StatSTATProdHQManager == "-1") && entities.StatSTATPurchasingBuyer == "1" && entities.StatSTATPurchasingManager == "1")
                {
                    entities.StatAll = "FOR SCD INCHARGE APPROVAL";
                }
                if ((entities.StatSTATProdSecManager == "1" || entities.StatSTATProdSecManager == "-1") && (entities.StatSTATProdDeptManager == "1" || entities.StatSTATProdDeptManager == "-1") && (entities.StatSTATProdDivManager == "1" || entities.StatSTATProdDivManager == "-1") && (entities.StatSTATProdHQManager == "1" || entities.StatSTATProdHQManager == "-1") && entities.StatSTATPurchasingBuyer == "1" && entities.StatSTATPurchasingManager == "1" && string.IsNullOrEmpty(entities.StatSend))
                {
                    entities.StatAll = "FOR SENDING";
                }
                if ((entities.StatSTATProdSecManager == "1" || entities.StatSTATProdSecManager == "-1") && (entities.StatSTATProdDeptManager == "1" || entities.StatSTATProdDeptManager == "-1") && (entities.StatSTATProdDivManager == "1" || entities.StatSTATProdDivManager == "-1") && (entities.StatSTATProdHQManager == "1" || entities.StatSTATProdHQManager == "-1") && entities.StatSTATPurchasingBuyer == "1" && entities.StatSTATPurchasingManager == "1" && entities.StatSend == "SEND" && string.IsNullOrEmpty(entities.StatReceived))
                {
                    entities.StatAll = "FOR RESEND / WAITING FOR RESPONSE";
                }
                if ((entities.StatSTATProdSecManager == "1" || entities.StatSTATProdSecManager == "-1") && (entities.StatSTATProdDeptManager == "1" || entities.StatSTATProdDeptManager == "-1") && (entities.StatSTATProdDivManager == "1" || entities.StatSTATProdDivManager == "-1") && (entities.StatSTATProdHQManager == "1" || entities.StatSTATProdHQManager == "-1") && entities.StatSTATPurchasingBuyer == "1" && entities.StatSTATPurchasingManager == "1" && entities.StatSend == "SEND" && entities.StatReceived == "RECEIVED")
                {
                    entities.StatAll = "SUPPLIER RESPONDED";
                }
                if (entities.StatSTATProdSecManager == "2" || entities.StatSTATProdDeptManager == "2" || entities.StatSTATProdDivManager == "2" || entities.StatSTATProdHQManager == "2" || entities.StatSTATPurchasingBuyer == "2" || entities.StatSTATPurchasingManager == "2")
                {
                    entities.StatAll = "DISAPPROVED";
                }
                if (entities.StatClosed == "1")
                {
                    entities.StatAll = "CLOSED";
                }
                if (entities.StatClosed == "2")
                {
                    entities.StatAll = "RE-OPEN";
                }
                //if (entities.StatClosed == "2")
                //{
                //    entities.StatAll = "CLOSED";
                //}


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

    public List<Entities_URF_RequestEntry> viewNotInPurchaseInquiry(string CTRLNo)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT DETAILS.PONO, DETAILS.PRNO FROM URF_TRANSACTION_RequestDetails DETAILS WITH (NOLOCK) WHERE DETAILS.CTRLNo = '" + CTRLNo + "'";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

                entities.RdPONO = reader["PONO"] != DBNull.Value ? reader["PONO"].ToString() : string.Empty;
                entities.RdPRNO = reader["PRNO"] != DBNull.Value ? reader["PRNO"].ToString() : string.Empty;

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

    public List<Entities_URF_RequestEntry> viewNotInPurchaseInquiry2(string PONO, string PRNO)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection_Data_Link();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT VW.PONO, VW.PRNO FROM [ROPROS].[dbo].[vewRFQDelivery] VW WITH (NOLOCK) WHERE VW.PONO = '" + PONO + "' AND VW.PRNO = '" + PRNO + "'";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

                entities.RdPONO = reader["PONO"] != DBNull.Value ? reader["PONO"].ToString() : string.Empty;
                entities.RdPRNO = reader["PRNO"] != DBNull.Value ? reader["PRNO"].ToString() : string.Empty;

            }

            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            cmd = null;

            conn.Dispose();
            conn.Close();
            conn = null;

            if (reader != null)
            {
                reader = null;
            }
        }

        return list;

    }

    public List<Entities_URF_RequestEntry> vewRFQDelivery(string query, string dataIndex)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

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
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

                entities.RdPONO = reader["PONO"] != DBNull.Value ? reader["PONO"].ToString() : string.Empty;
                entities.RdPRNO = reader["PRNO"] != DBNull.Value ? reader["PRNO"].ToString() : string.Empty;
                entities.RdItemName = reader["Item"] != DBNull.Value ? reader["Item"].ToString().ToUpper() : string.Empty;
                entities.RdSpecs = reader["Specification"] != DBNull.Value ? reader["Specification"].ToString().ToUpper() : string.Empty;
                entities.RdQuantity = reader["RemainQTY"] != DBNull.Value ? reader["RemainQTY"].ToString() : string.Empty;
                entities.RdUnitOfMeasure = reader["UOM"] != DBNull.Value ? reader["UOM"].ToString() : string.Empty;
                entities.RdDeliveryConfirmationDate = reader["ReplyDLVDate"] != DBNull.Value ? reader["ReplyDLVDate"].ToString() : string.Empty;
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

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_CheckApprover(string userid)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT LC_Main.RefId, " +
                                "( " +
                                    "SELECT TOP 1 '-1' FROM Login_Credentials LC WITH (NOLOCK) " +
                                    "INNER JOIN UserAccess UA WITH (NOLOCK) ON LC.RefId = UA.LoginId " +
                                    "WHERE UA.[Transaction] = 401 AND LC.Section = LC_Main.Section " +
                                    "AND UA.LoginId = LC.RefId " +
                                ") AS SecManagerApprover, " +
                                "( " +
                                    "SELECT TOP 1 '-1' FROM Login_Credentials LC WITH (NOLOCK) " +
                                    "INNER JOIN UserAccess UA WITH (NOLOCK) ON LC.RefId = UA.LoginId " +
                                    "WHERE UA.[Transaction] = 402 AND LC.Department = LC_Main.Department " +
                                    "AND UA.LoginId = LC.RefId " +
                                ") AS DeptManagerApprover, " +
                                "( " +
                                    "SELECT TOP 1 '-1' FROM Login_Credentials LC WITH (NOLOCK) " +
                                    "INNER JOIN UserAccess UA WITH (NOLOCK) ON LC.RefId = UA.LoginId " +
                                    "WHERE UA.[Transaction] = 403 AND LC.Division = LC_Main.Division " +
                                    "AND UA.LoginId = LC.RefId " +
                                ") AS DivManagerApprover, " +
                                "( " +
                                    "SELECT TOP 1 '-1' FROM Login_Credentials LC WITH (NOLOCK) " +
                                    "INNER JOIN UserAccess UA WITH (NOLOCK) ON LC.RefId = UA.LoginId " +
                                    "WHERE UA.[Transaction] = 404 AND LC.HQ = LC_Main.HQ " +
                                    "AND UA.LoginId = LC.RefId " +
                                ") AS HqManagerApprover " +
                                "FROM Login_Credentials LC_Main WITH (NOLOCK) " +
                                "WHERE LC_Main.RefId = '" + userid + "'";

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

                entities.SecManagerApprover = reader["SecManagerApprover"].ToString();
                entities.DeptManagerApprover = reader["DeptManagerApprover"].ToString();
                entities.DivManagerApprover = reader["DivManagerApprover"].ToString();
                entities.HqManagerApprover = reader["HqManagerApprover"].ToString();

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


    public List<Entities_URF_RequestEntry> URF_TRANSACTION_Reporting_ByDateRange(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "URF_TRANSACTION_Reporting_ByDateRange";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.CommandTimeout = 500;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

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


    public List<Entities_URF_RequestEntry> URF_TRANSACTION_Reporting_ByDateRange_ByDivision(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "URF_TRANSACTION_Reporting_ByDateRange_ByDivision";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.CommandTimeout = 500;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

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

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_Reporting_ByDateRange_ByAll(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "URF_TRANSACTION_Reporting_ByDateRange_ByAll";
            cmd.Parameters.Add(Factory.CreateParameter("@from", entity.DrFrom = entity.DrFrom.Length <= 0 ? string.Empty : entity.DrFrom));
            cmd.Parameters.Add(Factory.CreateParameter("@to", entity.DrTo = entity.DrTo.Length <= 0 ? string.Empty : entity.DrTo));
            cmd.CommandTimeout = 500;

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();

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

    public List<Entities_URF_RequestEntry> URF_TRANSACTION_Reporting_ByDateRange_Details(Entities_URF_RequestEntry entity)
    {
        List<Entities_URF_RequestEntry> list = new List<Entities_URF_RequestEntry>();

        DbConnection conn = Factory.CreateConnection();
        DbCommand cmd = Factory.CreateCommand();
        DbDataReader reader = null;

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT DETAILS.RefId, DETAILS.PONO, DETAILS.PRNO, DETAILS.ItemName, DETAILS.Specs, DETAILS.Quantity, DETAILS.UnitOfMeasure, DETAILS.DeliveryConfirmationDate, DETAILS.RequestedDeliveryDate, DETAILS.ReplyDeliveryDate, " +
                                "HEAD.CTRLNo, HEAD.Requester, HEAD.Category, HEAD.Type, HEAD.Supplier, HEAD.Attention, HEAD.Reason, HEAD.OtherReason, HEAD.TransactionDate, HEAD.RepiStock, HEAD.DailyUsage, HEAD.StockLife, " +
                                "STAT.PurchasingBuyer, STAT.STATPurchasingBuyer, STAT.DOAPurchasingBuyer, STAT.PurchasingManager, STAT.STATPurchasingManager, STAT.DOAPurchasingManager, STAT.STATClosed, STAT.STATProdHQManager, " +
                                "STAT.STATProdSecManager, STAT.STATProdDeptManager, STAT.STATProdDivManager, " +
                                "(SELECT TOP 1 TransactionType FROM URF_TRANSACTION_SendReceived WHERE CTRLNo = HEAD.CTRLNo ORDER BY SendReceivedDate DESC) AS SendReceived, " +
                                "LC.FullName AS RequesterName, CAT.Description AS CategoryDescription, " +
                                "(SELECT FullName FROM Login_Credentials WITH (NOLOCK) WHERE RefId = STAT.PurchasingBuyer) AS PurchasingBuyerName, " +
                                "(SELECT FullName FROM Login_Credentials WITH (NOLOCK) WHERE RefId = STAT.PurchasingManager) AS PurchasingManagerName, " +
                                "(SELECT Name FROM MT_Supplier_Head WITH (NOLOCK) WHERE RefId = HEAD.Supplier) AS SupplierName, " +
                                "(SELECT Name FROM URF_MT_Reason WITH (NOLOCK) WHERE RefId = HEAD.Reason) AS ReasonName, " +
                                "(SELECT Description FROM MT_UnitOfMeasure WITH (NOLOCK) WHERE RefId = DETAILS.UnitOfMeasure) AS UOMName " +
                                "FROM URF_TRANSACTION_RequestDetails DETAILS WITH (NOLOCK) " +
                                "LEFT JOIN URF_TRANSACTION_RequestStatus STAT WITH (NOLOCK) ON DETAILS.CTRLNo = STAT.CTRLNo " +
                                "LEFT JOIN URF_TRANSACTION_RequestHead HEAD WITH (NOLOCK) ON DETAILS.CTRLNo = HEAD.CTRLNo " +
                                "LEFT JOIN MT_Category CAT WITH (NOLOCK) ON HEAD.Category = CAT.RefId " +
                                "LEFT JOIN Login_Credentials LC ON HEAD.Requester = LC.RefId " +	
                                "WHERE CONVERT(DATE, HEAD.TransactionDate) >= '" + entity.DrFrom + "' AND CONVERT(DATE, HEAD.TransactionDate) <= '" + entity.DrTo + "' " +
                                "ORDER BY HEAD.RefId DESC";
            cmd.CommandTimeout = 500;


            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Entities_URF_RequestEntry entities = new Entities_URF_RequestEntry();               

                entities.RdCtrlNo = reader["CTRLNo"].ToString();
                entities.RhRequester = CryptorEngine.Decrypt(reader["RequesterName"].ToString().Replace(" ", "+"), true);
                entities.RhCategory = reader["CategoryDescription"].ToString();
                entities.RhType = reader["Type"] != DBNull.Value ? reader["Type"].ToString() : string.Empty;
                entities.RhSupplier = reader["SupplierName"].ToString();
                entities.RhAttention = reader["Attention"] != DBNull.Value ? reader["Attention"].ToString() : string.Empty;
                entities.RhReason = reader["Reason"] != DBNull.Value ? reader["Reason"].ToString() : string.Empty;
                entities.RhOtherReason = reader["OtherReason"] != DBNull.Value ? reader["OtherReason"].ToString() : string.Empty;
                entities.RhTransactionDate = reader["TransactionDate"].ToString();
                entities.RepiStock = reader["RepiStock"] != DBNull.Value ? reader["RepiStock"].ToString() : string.Empty;
                entities.DailyUsage = reader["DailyUsage"] != DBNull.Value ? reader["DailyUsage"].ToString() : string.Empty;
                entities.StockLife = reader["StockLife"] != DBNull.Value ? reader["StockLife"].ToString() : string.Empty;
                entities.RdPONO = reader["PONO"] != DBNull.Value ? reader["PONO"].ToString() : string.Empty;
                entities.RdPRNO = reader["PRNO"] != DBNull.Value ? reader["PRNO"].ToString() : string.Empty;
                entities.RdItemName = reader["ItemName"] != DBNull.Value ? reader["ItemName"].ToString() : string.Empty;
                entities.RdSpecs = reader["Specs"] != DBNull.Value ? reader["Specs"].ToString() : string.Empty;
                entities.RdQuantity = reader["Quantity"] != DBNull.Value ? reader["Quantity"].ToString() : string.Empty;
                entities.RdUnitOfMeasure = reader["UnitOfMeasure"] != DBNull.Value ? reader["UnitOfMeasure"].ToString() : string.Empty;
                entities.RdDeliveryConfirmationDate = reader["DeliveryConfirmationDate"] != DBNull.Value ? reader["DeliveryConfirmationDate"].ToString() : string.Empty;
                entities.RdRequestedDeliveryDate = reader["RequestedDeliveryDate"] != DBNull.Value ? reader["RequestedDeliveryDate"].ToString() : string.Empty;
                entities.RdReplyDeliveryDate = reader["ReplyDeliveryDate"] != DBNull.Value ? reader["ReplyDeliveryDate"].ToString() : string.Empty;
                entities.StatPurchasingBuyer = reader["StatPurchasingBuyer"] != DBNull.Value ? reader["StatPurchasingBuyer"].ToString() : "0";
                entities.StatPurchasingManager = reader["StatPurchasingManager"] != DBNull.Value ? reader["StatPurchasingManager"].ToString() : "0";

                if (entities.StatPurchasingManager == "1")
                {
                    entities.StatAll = "APPROVED";
                }
                if (string.IsNullOrEmpty(entities.StatPurchasingManager) || entities.StatPurchasingManager == "0")
                {
                    entities.StatAll = "PENDING APPROVAL";
                }
                if (entities.StatPurchasingManager == "2" || entities.StatPurchasingBuyer == "2")
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

    

