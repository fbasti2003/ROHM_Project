using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Xml;


namespace REPI_PUR_SOFRA
{
    public partial class RFQ_SendToSuppliers : System.Web.UI.Page
    {

        BLL_RFQ BLL = new BLL_RFQ();
        Common COMMON = new Common();

        public string errorSendingDetails = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["RFQListForSending"] != null)
                {

                    List<string> listRFQNo = (List<string>)Session["RFQListForSending"];

                    foreach (string rfq in listRFQNo)
                    {
                        ddRFQNo.Items.Add(rfq);
                    }

                }                

                LoadDefault();
            }

        }

        private void LoadDefault()
        {
            try
            {                

                List<Entities_RFQ_RequestEntry> listSuppliers = new List<Entities_RFQ_RequestEntry>();
                listSuppliers = BLL.RFQ_TRANSACTION_GetSupplierForSendAndWithResponseByRFQNo(ddRFQNo.SelectedItem.Text.Trim());

                if (listSuppliers != null)
                {
                    if (listSuppliers.Count > 0)
                    {
                        gvSuppliers.DataSource = listSuppliers;
                        gvSuppliers.DataBind();
                    }
                }

                //-------------------------------------------------------------------------------------------------------------------

                List<Entities_RFQ_RequestEntry> listDetails = new List<Entities_RFQ_RequestEntry>();
                listDetails = BLL.RFQ_TRANSACTION_Monitoring_GetDetailsByRFQNo(ddRFQNo.SelectedItem.Text.Trim());

                if (listDetails != null)
                {
                    if (listDetails.Count > 0)
                    {
                        gvData.DataSource = listDetails;
                        gvData.DataBind();
                    }
                }


                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('1 - " + ex.StackTrace.ToString() + "');", true);
            }
        }

        protected void ddRFQNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDefault();
        }

        protected void gvSuppliers_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblSupplierID = (Label)e.Row.FindControl("lblSupplierID");
                    Label lblWithResponse = (Label)e.Row.FindControl("lblWithResponse");
                    Label lblSupplierName = (Label)e.Row.FindControl("lblSupplierName");
                    Label lblResponseCount = (Label)e.Row.FindControl("lblResponseCount");
                    Label lblRegistered = (Label)e.Row.FindControl("lblRegistered");
                    
                    if (!string.IsNullOrEmpty(lblWithResponse.Text))
                    {
                        if (int.Parse(lblWithResponse.Text.Trim()) > 0)
                        {
                            lblSupplierName.Style.Add("background-color", "#4CAF50");
                            lblSupplierName.Style.Add("color", "White");
                        }
                    }

                    if (!string.IsNullOrEmpty(lblResponseCount.Text))
                    {
                        if (int.Parse(lblWithResponse.Text.Trim()) > 0 && int.Parse(lblResponseCount.Text.Trim()) > 0)
                        {
                            lblSupplierName.Style.Add("background-color", "#009688");
                            lblSupplierName.Style.Add("color", "White");
                        }
                    }

                    if (string.IsNullOrEmpty(lblRegistered.Text))
                    {
                        lblRegistered.Text = string.Empty;
                    }
                    else
                    {
                        if (lblRegistered.Text == "1")
                        {
                            lblRegistered.Text = "REGISTERED";
                        }
                        if (lblRegistered.Text == "2")
                        {
                            lblRegistered.Text = "NOT REGISTERED";
                        }
                    }
                    

                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('2 - " + ex.StackTrace.ToString() + "');", true);
            }
        }

        protected void gvSuppliers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                ImageButton ibApproved = row.FindControl("ibApproved") as ImageButton;

                if (e.CommandName == "A_Command")
                {
                    if (ibApproved.ImageUrl == "~/images/A1.png")
                    {
                        ibApproved.ImageUrl = "~/images/A2.png";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SetPositionOnClick();", true);
                    }
                    else
                    {
                        ibApproved.ImageUrl = "~/images/A1.png";
                    }
                }

                //LinkButton lblView = row.FindControl("lblView") as LinkButton;
                //Label lblRFQNo = row.FindControl("lblRFQNo") as Label;
                //Label lblTransDate = row.FindControl("lblTransDate") as Label;
                //Label lblRequester = row.FindControl("lblRequester") as Label;
                //Label lblCategory = row.FindControl("lblCategory") as Label;
                //Label lblStatAll = row.FindControl("lblStatAll") as Label;


                //if (e.CommandName == "lblView_Command")
                //{
                //    string URL = "~/RFQ_Monitoring_Details.aspx?RFQNo_From_Inquiry=" + CryptorEngine.Encrypt(lblRFQNo.Text.Trim(), true);

                //    Session["Requester_From_Inquiry"] = lblRequester.Text;
                //    Session["TransDate_From_Inquiry"] = lblTransDate.Text;
                //    Session["Category_From_Inquiry"] = lblCategory.Text.Trim().ToUpper();
                //    Session["btnPreview_Visibility"] = lblStatAll.Text == "APPROVED" ? "true" : "false";
                //    Session["btnUpdate_Visibility"] = lblStatAll.Text == "FOR PRODUCTION MANAGER APPROVAL" ? "true" : "false";

                //    URL = Page.ResolveClientUrl(URL);
                //    lblView.OnClientClick = "window.open('" + URL + "'); return false;";
                //}

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('3 - " + ex.StackTrace.ToString() + "');", true);
            }
        }

        protected void btnUpdateRemarks_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvData.Rows.Count > 0)
                {
                    string queryPurchasingRemarks = string.Empty;
                    string qBPart = "BEGIN TRY BEGIN TRANSACTION ";
                    string qEPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                    int remarksCounter = 0;
                    string success = string.Empty;

                    for (int i = 0; i < gvData.Rows.Count; i++)
                    {
                        Label lblRefId = (Label)gvData.Rows[i].Cells[0].FindControl("lblRefId");
                        Label lblRemarks = (Label)gvData.Rows[i].Cells[6].FindControl("lblRemarks");
                        TextBox txtPurchasingRemarks = (TextBox)gvData.Rows[i].Cells[7].FindControl("txtPurchasingRemarks");

                        if (!string.IsNullOrEmpty(txtPurchasingRemarks.Text))
                        {
                            queryPurchasingRemarks += "UPDATE Request_Details SET Remarks = '" + lblRemarks.Text + " [" + txtPurchasingRemarks.Text + "]' WHERE RefId = '" + lblRefId.Text.Trim() + "' ";
                            remarksCounter++;
                        }
                    }

                    success = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(qBPart + queryPurchasingRemarks + qEPart).ToString();

                    if (success == remarksCounter.ToString())
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Updating remarks for RFQNo " + ddRFQNo.SelectedItem.Text + " has been successfully updated.');", true);
                    }


                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('" + ex.StackTrace.ToString() + "');", true);
            }
        }

        protected void btnReceiving_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("RFQ_PurchasingReceiving.aspx");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('4 - " + ex.StackTrace.ToString() + "');", true);
                //errorSendingDetails = "ERROR DETAILS : <br/> MESSAGE : " + ex.Message + "<br/> STACKTRACE :" + ex.StackTrace.ToString();
            }
        }

        protected void btnSend2_Click(object sender, EventArgs e)
        {
            try
            {
                int nullRegistered = 0;

                for (int i = 0; i < gvSuppliers.Rows.Count; i++)
                {
                    
                    ImageButton ibApproved = (ImageButton)gvSuppliers.Rows[i].Cells[0].FindControl("ibApproved");
                    Label lblSupplierID = (Label)gvSuppliers.Rows[i].Cells[1].FindControl("lblSupplierID");
                    Label lblSupplierEmail = (Label)gvSuppliers.Rows[i].Cells[1].FindControl("lblSupplierEmail");
                    Label lblSupplierName = (Label)gvSuppliers.Rows[i].Cells[1].FindControl("lblSupplierName");
                    Label lblRegistered = (Label)gvSuppliers.Rows[i].Cells[2].FindControl("lblRegistered");

                    if (ibApproved.ImageUrl == "~/images/A2.png")
                    {
                        if (string.IsNullOrEmpty(lblRegistered.Text))
                        {
                            nullRegistered++;                            
                        }
                    }

                }

                if (nullRegistered > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowRegistered", "showDialog();", true);
                }
                else
                {
                    btnSend_Click(sender, e);
                }

            }
            catch (Exception ex)
            {                
            }
        }
        

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvSuppliers.Rows.Count > 0)
                {
                    
                    List<Entities_RFQ_RequestEntry> listSendStatus = new List<Entities_RFQ_RequestEntry>();                    

                    List<Entities_RFQ_Currency> currencyL = new List<Entities_RFQ_Currency>();
                    string currencyList = string.Empty;

                    currencyL = BLL.RFQ_MT_Currency_GetAll();
                    if (currencyL != null)
                    {
                        if (currencyL.Count > 0)
                        {
                            foreach (Entities_RFQ_Currency currency in currencyL)
                            {
                                if (currency.IsDisabled == "0" || currency.IsDisabled.Length <= 0)
                                {
                                    currencyList += currency.Code + ",";
                                }
                            }
                        }
                    }

                    //-------------------------------------------------------------------------------------------------------------------
                    // SET BUYER INFORMATION

                    List<Entities_RFQ_BuyerInformation> listBuyerInformation = new List<Entities_RFQ_BuyerInformation>();
                    listBuyerInformation = BLL.RFQ_MT_BuyerInformation_GetAll().Where(itm => itm.IsDisabled == "0").ToList();
                    string buyerInformation = string.Empty;

                    if (listBuyerInformation != null)
                    {
                        if (listBuyerInformation.Count > 0)
                        {
                            string tableStart = "<table style='width:100%;'><tr><th align='left'>Purchasing Members</th><th align='left'>Section</th><th align='left'>Personal Email</th><th align='left'>Mobile Number</th></tr>";
                            string tableEnd = "</table>";
                            string information = string.Empty;
                            foreach (Entities_RFQ_BuyerInformation eBI in listBuyerInformation)
                            {
                                information += "<tr><td>" + eBI.Member + "</td><td>" + eBI.Section + "</td><td>" + eBI.Email + "</td><td>" + eBI.Mobile + "</td></tr>";
                            }

                            buyerInformation = tableStart + information + tableEnd;
                        }
                    }

                    //-------------------------------------------------------------------------------------------------------------------

                    foreach (ListItem item in ddRFQNo.Items)
                    {

                        if (System.IO.Directory.Exists(Server.MapPath("~/IO_Request/" + item.Text.Trim().ToString())))
                        {
                            string isRegistered = string.Empty;

                            //// CHECK IF SUPPLIER IS NOT REGISTERED
                            //for (int x = 0; x < gvSuppliers.Rows.Count; x++)
                            //{
                            //    ImageButton ibApproved = (ImageButton)gvSuppliers.Rows[x].Cells[0].FindControl("ibApproved");
                            //    Label lblRegistered = (Label)gvSuppliers.Rows[x].Cells[2].FindControl("lblRegistered");
                            //    Label lblSupplierName = (Label)gvSuppliers.Rows[x].Cells[1].FindControl("lblSupplierName");

                            //    if (ibApproved.ImageUrl == "~/images/A2.png")
                            //    {
                            //        if (lblRegistered.Text == "NOT REGISTERED" || string.IsNullOrEmpty(lblRegistered.Text))
                            //        {
                            //            isRegistered += lblSupplierName.Text + ",";
                            //            lblSupplierName.Style.Add("background-color", "red");
                            //        }
                            //    }
                            //}

                            // Start of for (int i = 0; i < gvSuppliers.Rows.Count; i++)
                            for (int i = 0; i < gvSuppliers.Rows.Count; i++)
                            {
                                ImageButton ibApproved = (ImageButton)gvSuppliers.Rows[i].Cells[0].FindControl("ibApproved");
                                Label lblSupplierID = (Label)gvSuppliers.Rows[i].Cells[1].FindControl("lblSupplierID");
                                Label lblSupplierEmail = (Label)gvSuppliers.Rows[i].Cells[1].FindControl("lblSupplierEmail");
                                Label lblSupplierName = (Label)gvSuppliers.Rows[i].Cells[1].FindControl("lblSupplierName");
                                Label lblRegistered = (Label)gvSuppliers.Rows[i].Cells[2].FindControl("lblRegistered");
                                

                                if (ibApproved.ImageUrl == "~/images/A2.png")
                                {
                                    List<Entities_RFQ_RequestEntry> listRequestDetails = new List<Entities_RFQ_RequestEntry>();
                                    listRequestDetails = BLL.RFQ_TRANSACTION_GetRequestDetailsByRFQNo(item.Text.Trim().ToString());

                                    if (listRequestDetails != null)
                                    {
                                        if (listRequestDetails.Count > 0)
                                        {
                                            string strAttachment = string.Empty;
                                            if (!System.IO.File.Exists(System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + item.Text.Trim().ToString()), lblSupplierID.Text.Trim() + "_" + item.Text.Trim().ToString() + ".xml")))
                                            {
                                                string pathToXML = System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + item.Text.Trim().ToString()), lblSupplierID.Text.Trim() + "_" + item.Text.Trim().ToString() + ".xml");

                                                XmlWriterSettings xmlSetting = new XmlWriterSettings();
                                                xmlSetting.CloseOutput = true;

                                                XmlWriter writer = XmlWriter.Create(pathToXML, xmlSetting);
                                                writer.WriteStartDocument(true);
                                                writer.WriteStartElement("TABLE");

                                                foreach (Entities_RFQ_RequestEntry entity in listRequestDetails)
                                                {
                                                    string remarks = string.Empty;
                                                    if (entity.RdPurchasingRemarks.Length > 0)
                                                    {
                                                        remarks = entity.RdPurchasingRemarks;
                                                    }
                                                    else
                                                    {
                                                        remarks = string.Empty;
                                                    }

                                                    COMMON.createXMLNodeForRequestDetails(entity.RdRefId.ToString(), entity.RdDescription,
                                                                          entity.RdSpecs, entity.RdMaker, entity.RdQuantity.ToString(),
                                                                          entity.RdUOMDesc, remarks, writer, currencyList, DateTime.Now.AddDays(double.Parse(txtAging.Text.Trim())).ToLongDateString());

                                                    if (!string.IsNullOrEmpty(entity.RdAttachment))
                                                    {
                                                        strAttachment += System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + item.Text.Trim().ToString()), entity.RdAttachment) + ",";
                                                    }
                                                }

                                                writer.WriteEndElement();
                                                writer.WriteEndDocument();
                                                writer.Flush();
                                                writer.Close();

                                            }
                                            else
                                            {
                                                foreach (Entities_RFQ_RequestEntry entity in listRequestDetails)
                                                {
                                                    if (!string.IsNullOrEmpty(entity.RdAttachment))
                                                    {
                                                        strAttachment += System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + item.Text.Trim().ToString()), entity.RdAttachment) + ",";
                                                    }
                                                }
                                            }

                                            //=========== Sending Email =================
                                            string path = System.IO.Path.Combine(Server.MapPath("~/IO_Request/" + item.Text.Trim().ToString()), lblSupplierID.Text.Trim() + "_" + item.Text.Trim().ToString() + ".xml");

                                            if (!string.IsNullOrEmpty(strAttachment))
                                            {
                                                path += "," + strAttachment;
                                            }
                                            

                                            string fixedBuyerInfo = buyerInformation;
                                            string userEmail = Session["UserEmail"].ToString();

                                            string emailService = string.Empty;

                                            // CHECK IF NOT SENT TODAY TO ELIMINATE MULTIPLE SENDING TO SUPPLIER
                                            if (string.IsNullOrEmpty(BLL.IsAlreadySentToSupplierToday(item.Text.Trim().ToString(), lblSupplierID.Text.Trim())))
                                            {

                                                emailService = COMMON.sendEmailToSuppliers(lblSupplierEmail.Text.Trim(), ConfigurationManager.AppSettings["email-username"], lblSupplierID.Text.Trim() + "_" + item.Text.Trim().ToString(),
                                                    "Hi <b>" + lblSupplierName.Text + "</b> Good Day!" + "<br /><br /> Kindly check the attached xml file (" + lblSupplierID.Text.Trim() + "_" + item.Text.Trim().ToString() + ".xml) for our quotation request <br /><br />" +
                                                    "<p style='color:red;'>REMINDER!</p><br/><b>" +
                                                    " - FOR ANY CONCERN/PROBLEM FOR THIS PARTICULAR REQUEST, PLEASE EMAIL " + (!string.IsNullOrEmpty(userEmail) ? userEmail : string.Empty).ToString() + "<br/>" +
                                                    " - ALWAYS PUT PRICE VALIDITY PER ITEM IN REMARKS PORTION. <br/>" +
                                                    " - MAKE SURE THAT THE DELIVERY LEAD TIME IS IN WORKING DAYS ONLY. <br/>" +
                                                    " - INPUT ANY CORRECTION OF DETAILS IN REMARKS PORTION. (ITEM NAME, SPECS, MAKER NAME & UNIT OF MEASURE)</b>" +
                                                    "<br /><br /><br />Thank You!<br /><br /><br />" +
                                                    "*** This is an automatically generated email, Please reply accordingly *** <br /> <br />" +
                                                    "You have received a new request quotation from ROHM Electronics Philippines Inc. <br /> <br />" +
                                                    //"Please answer this quotation on or before <b>" + DateTime.Now.AddDays(double.Parse(txtAging.Text.Trim())).ToLongDateString().ToUpper() + "</b><br /> <br />For this RFQ, please contact <b>" + Session["UserFullName"].ToString() + "</b><br /> <br /><br /><br /> For inquries, kindly see below contact details : <br />" + fixedBuyerInfo, path, lblSupplierName.Text, lblSupplierID.Text.Trim() + "_" + item.Text.ToUpper().Trim().ToString());
                                                    "Please answer this quotation on or before <b>" + DateTime.Now.AddDays(double.Parse(txtAging.Text.Trim())).ToLongDateString().ToUpper() + "</b><br /> For this RFQ please contact <b>" + Session["CategoryName"].ToString() + "</b> Section and please CC (<b>" + userEmail.ToString() + "</b>) when replying to this email. <br /><br /> For inquries, kindly see below contact details : <br /><br />" + fixedBuyerInfo, path, lblSupplierName.Text, lblSupplierID.Text.Trim() + "_" + item.Text.ToUpper().Trim().ToString());

                                            }
                                            else
                                            {
                                                emailService = "SENT ALREADY";
                                            }

                                            if (emailService.ToLower().Contains("success"))
                                            {
                                                string queryBeginPart = "BEGIN TRY BEGIN TRANSACTION ";
                                                string query1 = "UPDATE Request_Status SET Purchasing = 1 WHERE RFQNo ='" + item.Text.Trim().ToString() + "' ";
                                                string query2 = "UPDATE Request_Status SET Buyer = 0, PurchasingIncharge = 0, DepartmentManager = 0, DivisionManager = 0 WHERE RFQNo = '" + item.Text.Trim().ToString() + "' ";
                                                string query3 = "UPDATE Request_Status SET Supplier = 3 WHERE RFQNo = '" + item.Text.Trim().ToString() + "' ";
                                                string query4 = "INSERT INTO Request_HistoryOfApproval (RFQNo,TransactionName,ApprovedBy,ApprovedDate,IsActive) VALUES ('" + item.Text.Trim().ToString() + "','" + ConfigurationManager.AppSettings["ApprovedDisapprovedName-Purchasing"].ToString() + "','" + Session["LcRefId"].ToString() + "', GETDATE(),1) ";
                                                string query5 = "INSERT INTO Supplier_Rewarded (RFQNo, SupplierID, SendDate, SendBy, Aging) VALUES ('" + item.Text.Trim().ToString() + "','" + lblSupplierID.Text.Trim() + "', GETDATE(),'" + Session["LcRefId"].ToString() + "','" + txtAging.Text.Trim() + "') ";
                                                string queryEndPart = "COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
                                                string query_Success = BLL.RFQ_TRANSACTION_RequestEntry_Insert_SQLTransaction(queryBeginPart + query1 + query2 + query3 + query4 + query5 + queryEndPart).ToString();

                                                if (query_Success == "5")
                                                {
                                                    Entities_RFQ_RequestEntry entitySendStatus = new Entities_RFQ_RequestEntry();

                                                    entitySendStatus.ResponseRFQNo = item.Text.Trim().ToString();
                                                    entitySendStatus.ResponseSendStatus = "SUCCESSFULLY SENT";
                                                    entitySendStatus.ResponseSupplierName = lblSupplierName.Text;

                                                    listSendStatus.Add(entitySendStatus);
                                                }

                                            }
                                            else
                                            {
                                                Entities_RFQ_RequestEntry entitySendStatus = new Entities_RFQ_RequestEntry();

                                                emailService = string.IsNullOrEmpty(emailService) ? "ERROR SENDING" : emailService;

                                                entitySendStatus.ResponseRFQNo = item.Text.Trim().ToString();
                                                entitySendStatus.ResponseSendStatus = emailService.Length > 70 ? emailService.ToUpper().Substring(0, 70).ToString() + "..." : emailService.ToUpper().ToString();
                                                entitySendStatus.ResponseSupplierName = lblSupplierName.Text;

                                                listSendStatus.Add(entitySendStatus);
                                            }

                                        }
                                    }

                                } // End of if (ibApproved.ImageUrl == "~/images/A2.png")


                            }
                            // End of for (int i = 0; i < gvSuppliers.Rows.Count; i++)

                        } // if (System.IO.Directory.Exists(Server.MapPath("~/IO_Request/" + item.Text.Trim().ToString())))
                    } // End of foreach (ListItem item in ddRFQNo.Items)


                    if (listSendStatus != null)
                    {
                        if (listSendStatus.Count > 0)
                        {
                            gvSentDetails.DataSource = listSendStatus;
                            gvSentDetails.DataBind();
                        }
                    }


                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "SuccessMessage('Sending Process has been completed. Please see below details.');", true);
                }
                
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ErrorMessage('4 - " + ex.StackTrace.ToString() + "');", true);
                errorSendingDetails = "ERROR DETAILS : <br/> MESSAGE : " + ex.Message + "<br/> STACKTRACE :" + ex.StackTrace.ToString();
            }
        }

        


    }
}
