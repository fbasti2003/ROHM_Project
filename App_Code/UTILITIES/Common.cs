using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data;
using System.Reflection;
using System.Net.Mail;
using System.Xml;
using System.ComponentModel;
using OpenPop.Mime;
using System.Globalization;
using System.Net;

public class Common
{
    public string Concatinate(string input, int value)
    {
        return input.Substring(0, value);
    }

    public double GetBusinessDays(DateTime startD, DateTime endD)
    {
        double calcBusinessDays =
            1 + ((endD - startD).TotalDays * 5 -
            (startD.DayOfWeek - endD.DayOfWeek) * 2) / 7;

        if (endD.DayOfWeek == DayOfWeek.Saturday) calcBusinessDays--;
        if (startD.DayOfWeek == DayOfWeek.Sunday) calcBusinessDays--;

        return calcBusinessDays;
    }

    public string controlNoZeroPrefix(string count)
    {
        string ret = string.Empty;

        switch (count)
        {
            case "1":
                ret = "000";
                break;
            case "2":
                ret = "00";
                break;
            case "3":
                ret = "0";
                break;
        }

        return ret;
    }

    public string setStatus(string input)
    {
        string ret = string.Empty;

        switch (input)
        {
            case "0":
                ret = "PENDING";
                break;
            case "1":
                ret = "APPROVED";
                break;
            case "2":
                ret = "DISAPPROVED";
                break;
        }

        return ret;

    }

    public string setStatusColor(string input)
    {
        string ret = string.Empty;

        switch (input)
        {
            case "PENDING":
                ret = "#F44336";
                break;
            case "APPROVED":
                ret = "#00C851";
                break;
            case "DISAPPROVED":
                ret = "#ffbb33";
                break;
        }

        return ret;

    }

    public bool IsNullOrWhiteSpace(string value)
    {
        if (value != null)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
    {
        Double result;
        return Double.TryParse(val, NumberStyle,
            System.Globalization.CultureInfo.CurrentCulture, out result);
    }


    public void createXMLNodeForRequestDetails(string RefId, string Description,
                                               string Specs, string Maker, string Quantity,
                                               string UOM, string Remarks, XmlWriter writer,
                                               string currencyCollections, string aging)
    {
        writer.WriteStartElement("REQUEST");
        writer.WriteStartElement("RefId");
        writer.WriteString(RefId);
        writer.WriteEndElement();
        writer.WriteStartElement("Description");
        writer.WriteString(Description);
        writer.WriteEndElement();
        writer.WriteStartElement("Specs");
        writer.WriteString(Specs);
        writer.WriteEndElement();
        writer.WriteStartElement("Maker");
        writer.WriteString(Maker);
        writer.WriteEndElement();
        writer.WriteStartElement("Quantity");
        writer.WriteString(Quantity);
        writer.WriteEndElement();
        writer.WriteStartElement("UOM");
        writer.WriteString(UOM);
        writer.WriteEndElement();
        writer.WriteStartElement("Remarks");
        writer.WriteString(Remarks);
        writer.WriteEndElement();
        writer.WriteStartElement("PricePerUnit");
        writer.WriteString(string.Empty);
        writer.WriteEndElement();
        writer.WriteStartElement("LeadTime");
        writer.WriteString(string.Empty);
        writer.WriteEndElement();
        writer.WriteStartElement("SupplierRemarks");
        writer.WriteString(string.Empty);
        writer.WriteEndElement();
        writer.WriteStartElement("Currency");
        writer.WriteString(string.Empty);
        writer.WriteEndElement();
        writer.WriteStartElement("CurrencyCollections");
        writer.WriteString(currencyCollections);
        writer.WriteEndElement();
        writer.WriteStartElement("Aging");
        writer.WriteString(aging);
        writer.WriteEndElement();
        writer.WriteEndElement();
    }

    public string sendEmailToSuppliers(string emailTo, string emailFrom, string emailSubject, string emailBody, string emailAttachment, string supplierName, string supplierIDUnderscoreRFQNo)
    {
        string ret = string.Empty;

        MailMessage msg = new MailMessage();
        msg.To.Add(new MailAddress(emailTo, supplierName));
        msg.From = new MailAddress(emailFrom, "ROHM-RFQ");
        msg.Subject = emailSubject;
        msg.Body = emailBody;
        
        foreach (string str in emailAttachment.Split(','))
        {
            if (str.Length > 0)
            {
                msg.Attachments.Add(new Attachment(str));
            }
        }

        msg.IsBodyHtml = true;

        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        SmtpClient client = new SmtpClient();
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email-username"], ConfigurationManager.AppSettings["email-password"]);
        client.Port = 587; // You can use Port 25 if 587 is blocked 
        client.Host = "smtp.office365.com";
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.EnableSsl = true;
        
        //msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.OnSuccess;

        try
        {
            client.Send(msg);

            foreach (Attachment attachment in msg.Attachments)
            {
                attachment.Dispose();
            }

            msg.Attachments.Dispose();
            msg = null;

            ret = "success";
            //System.Threading.Thread.Sleep(5000);

            //ret = getDeliveryReceipt_ByRFQNo_And_SupplierID(supplierIDUnderscoreRFQNo);
            //msg.Dispose();

            //ret = msg.DeliveryNotificationOptions.ToString();
            //client.SendAsync(msg, msg);
            //client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);  
            

        }
        catch (SmtpException ex)
        {
            ret = ex.Message.ToString();
            //ret = msg.DeliveryNotificationOptions.ToString();
        }

        return ret;

    }

    public string sendEmailToSuppliersForSRFWarehouse(string emailTo, string emailFrom, string emailSubject, string emailBody, string emailAttachment, string supplierName, string supplierIDUnderscoreRFQNo)
    {
        string ret = string.Empty;

        MailMessage msg = new MailMessage();
        msg.To.Add(new MailAddress(emailTo, supplierName));
        msg.CC.Add(new MailAddress(ConfigurationManager.AppSettings["SRF_IMPEX_EMAIL_CC"]));
        msg.From = new MailAddress(emailFrom, "REPI SERVICE REPAIR NOTIFICATION");
        msg.Subject = emailSubject;
        msg.Body = emailBody;

        foreach (string str in emailAttachment.Split(','))
        {
            if (str.Length > 0)
            {
                msg.Attachments.Add(new Attachment(str));
            }
        }

        msg.IsBodyHtml = true;

        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        SmtpClient client = new SmtpClient();
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email-username"], ConfigurationManager.AppSettings["email-password"]);
        client.Port = 587; // You can use Port 25 if 587 is blocked 
        client.Host = "smtp.office365.com";
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.EnableSsl = true;

        //msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.OnSuccess;

        try
        {
            client.Send(msg);

            foreach (Attachment attachment in msg.Attachments)
            {
                attachment.Dispose();
            }

            msg.Attachments.Dispose();
            msg = null;

            ret = "success";
            //System.Threading.Thread.Sleep(5000);

            //ret = getDeliveryReceipt_ByRFQNo_And_SupplierID(supplierIDUnderscoreRFQNo);
            //msg.Dispose();

            //ret = msg.DeliveryNotificationOptions.ToString();
            //client.SendAsync(msg, msg);
            //client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);  


        }
        catch (SmtpException ex)
        {
            ret = ex.Message.ToString();
            //ret = msg.DeliveryNotificationOptions.ToString();
        }

        return ret;

    }    

    public string sendEmailTo_CRF_DRF_URF_Approvers(string emailTo, string emailFrom, string emailSubject, string emailBody)
    {
        string ret = string.Empty;

        MailMessage msg = new MailMessage();
        msg.To.Add(new MailAddress(emailTo, "PUR_SOFRA_Notifications"));
        msg.From = new MailAddress(emailFrom, "PUR_PORTAL");
        msg.Subject = emailSubject;
        msg.Body = emailBody;

        msg.IsBodyHtml = true;

        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        SmtpClient client = new SmtpClient();
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email-username"], ConfigurationManager.AppSettings["email-password"]);
        client.Port = 587; // You can use Port 25 if 587 is blocked 
        client.Host = "smtp.office365.com";
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.EnableSsl = true;


        try
        {
            client.Send(msg);

            ret = "success"; 

        }
        catch (SmtpException ex)
        {
            ret = ex.Message.ToString();
        }

        return ret;

    }

    public string sendEmailToSuppliersURF(string emailTo, string emailFrom, string emailSubject, string emailBody, string emailAttachment, string supplierName, string supplierIDUnderscoreRFQNo)
    {
        string ret = string.Empty;

        MailMessage msg = new MailMessage();
        msg.To.Add(new MailAddress(emailTo, supplierName));
        msg.From = new MailAddress(emailFrom, "ROHM-URF");
        msg.Subject = emailSubject;
        msg.Body = emailBody;

        foreach (string str in emailAttachment.Split(','))
        {
            if (str.Length > 0)
            {
                msg.Attachments.Add(new Attachment(str));
            }
        }

        msg.IsBodyHtml = true;

        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        SmtpClient client = new SmtpClient();
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email-username"], ConfigurationManager.AppSettings["email-password"]);
        client.Port = 587; // You can use Port 25 if 587 is blocked 
        client.Host = "smtp.office365.com";
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.EnableSsl = true;

        //msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.OnSuccess;

        try
        {
            client.Send(msg);

            foreach (Attachment attachment in msg.Attachments)
            {
                attachment.Dispose();
            }

            msg.Attachments.Dispose();
            msg = null;

            ret = "success";
            //System.Threading.Thread.Sleep(5000);

            //ret = getDeliveryReceipt_ByRFQNo_And_SupplierID(supplierIDUnderscoreRFQNo);
            //msg.Dispose();

            //ret = msg.DeliveryNotificationOptions.ToString();
            //client.SendAsync(msg, msg);
            //client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);  

        }
        catch (SmtpException ex)
        {
            ret = ex.Message.ToString();
            //ret = msg.DeliveryNotificationOptions.ToString();
        }

        return ret;

    }

    public string sendEmailToSuppliersDRF(string emailTo, string emailFrom, string emailSubject, string emailBody, string emailAttachment, string supplierName, string supplierIDUnderscoreRFQNo)
    {
        string ret = string.Empty;

        MailMessage msg = new MailMessage();
        msg.To.Add(new MailAddress(emailTo, supplierName));
        msg.From = new MailAddress(emailFrom, "ROHM-DRF");
        msg.Subject = emailSubject;
        msg.Body = emailBody;

        foreach (string str in emailAttachment.Split(','))
        {
            if (str.Length > 0)
            {
                msg.Attachments.Add(new Attachment(str));
            }
        }

        msg.IsBodyHtml = true;

        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        SmtpClient client = new SmtpClient();
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email-username"], ConfigurationManager.AppSettings["email-password"]);
        client.Port = 587; // You can use Port 25 if 587 is blocked 
        client.Host = "smtp.office365.com";
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.EnableSsl = true;

        //msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.OnSuccess;

        try
        {
            client.Send(msg);

            foreach (Attachment attachment in msg.Attachments)
            {
                attachment.Dispose();
            }

            msg.Attachments.Dispose();
            msg = null;

            ret = "success";
            //System.Threading.Thread.Sleep(5000);

            //ret = getDeliveryReceipt_ByRFQNo_And_SupplierID(supplierIDUnderscoreRFQNo);
            //msg.Dispose();

            //ret = msg.DeliveryNotificationOptions.ToString();
            //client.SendAsync(msg, msg);
            //client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);  

        }
        catch (SmtpException ex)
        {
            ret = ex.Message.ToString();
            //ret = msg.DeliveryNotificationOptions.ToString();
        }

        return ret;

    }

    public string sendEmailToSuppliersCRF(string emailTo, string emailFrom, string emailSubject, string emailBody, string emailAttachment, string supplierName, string supplierIDUnderscoreRFQNo)
    {
        string ret = string.Empty;

        MailMessage msg = new MailMessage();
        msg.To.Add(new MailAddress(emailTo, supplierName));
        msg.From = new MailAddress(emailFrom, "ROHM-CRF");
        msg.Subject = emailSubject;
        msg.Body = emailBody;

        foreach (string str in emailAttachment.Split(','))
        {
            if (str.Length > 0)
            {
                msg.Attachments.Add(new Attachment(str));
            }
        }

        msg.IsBodyHtml = true;

        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        SmtpClient client = new SmtpClient();
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email-username"], ConfigurationManager.AppSettings["email-password"]);
        client.Port = 587; // You can use Port 25 if 587 is blocked 
        client.Host = "smtp.office365.com";
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.EnableSsl = true;

        //msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.OnSuccess;

        try
        {
            client.Send(msg);

            foreach (Attachment attachment in msg.Attachments)
            {
                attachment.Dispose();
            }

            msg.Attachments.Dispose();
            msg = null;

            ret = "success";
            //System.Threading.Thread.Sleep(5000);

            //ret = getDeliveryReceipt_ByRFQNo_And_SupplierID(supplierIDUnderscoreRFQNo);
            //msg.Dispose();

            //ret = msg.DeliveryNotificationOptions.ToString();
            //client.SendAsync(msg, msg);
            //client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);  

        }
        catch (SmtpException ex)
        {
            ret = ex.Message.ToString();
            //ret = msg.DeliveryNotificationOptions.ToString();
        }

        return ret;

    }

    public string sendEmailToRequester(string emailTo, string emailFrom, string emailSubject, string emailBody)
    {
        string ret = string.Empty;

        MailMessage msg = new MailMessage();
        msg.To.Add(new MailAddress(emailTo, "REQUESTER"));
        msg.From = new MailAddress(emailFrom, "ROHM-RFQ");
        msg.Subject = emailSubject;
        msg.Body = emailBody;

        msg.IsBodyHtml = true;

        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        SmtpClient client = new SmtpClient();
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email-username"], ConfigurationManager.AppSettings["email-password"]);
        client.Port = 587; // You can use Port 25 if 587 is blocked 
        client.Host = "smtp.office365.com";
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.EnableSsl = true;

        //msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.OnSuccess;

        try
        {
            client.Send(msg);

            ret = "success";
            //System.Threading.Thread.Sleep(5000);

            //ret = getDeliveryReceipt_ByRFQNo_And_SupplierID(supplierIDUnderscoreRFQNo);
            //msg.Dispose();

            //ret = msg.DeliveryNotificationOptions.ToString();
            //client.SendAsync(msg, msg);
            //client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);  

        }
        catch (SmtpException ex)
        {
            ret = ex.Message.ToString();
            //ret = msg.DeliveryNotificationOptions.ToString();
        }

        return ret;

    }

    public string sendEmailToSRF_PullOut(string emailTo, string emailFrom, string emailSubject, string emailBody, string emailAttachment, string supplierName, string supplierIDUnderscoreRFQNo)
    {
        string ret = string.Empty;

        MailMessage msg = new MailMessage();
        msg.To.Add(new MailAddress(emailTo, supplierName));
        msg.CC.Add(new MailAddress(ConfigurationManager.AppSettings["MamAndhieMapanoo_EmailAddress"]));
        msg.From = new MailAddress(emailFrom, "ROHM-URF");
        msg.Subject = emailSubject;
        msg.Body = emailBody;

        foreach (string str in emailAttachment.Split(','))
        {
            if (str.Length > 0)
            {
                msg.Attachments.Add(new Attachment(str));
            }
        }

        msg.IsBodyHtml = true;

        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        SmtpClient client = new SmtpClient();
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email-username"], ConfigurationManager.AppSettings["email-password"]);
        client.Port = 587; // You can use Port 25 if 587 is blocked 
        client.Host = "smtp.office365.com";
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.EnableSsl = true;

        try
        {
            client.Send(msg);

            foreach (Attachment attachment in msg.Attachments)
            {
                attachment.Dispose();
            }

            msg.Attachments.Dispose();
            msg = null;

            ret = "success";           

        }
        catch (SmtpException ex)
        {
            ret = ex.Message.ToString();
        }

        return ret;

    }

    public string getDeliveryReceipt_ByRFQNo_And_SupplierID(string supplierIDUnderscoreRFQNo)
    {
        string ret = string.Empty;

        try
        {
            OpenPop.Pop3.Pop3Client PopClient = new OpenPop.Pop3.Pop3Client();
            PopClient.Connect("smtp.office365.com", 995, true);
            PopClient.Authenticate(ConfigurationManager.AppSettings["email-username"], ConfigurationManager.AppSettings["email-password"], OpenPop.Pop3.AuthenticationMethod.UsernameAndPassword);

            if (PopClient.Connected)
            {
                int msgCount = PopClient.GetMessageCount();

                for (int x = 1; x <= msgCount; x++)
                {
                    var Messages = new List<OpenPop.Mime.Message>(msgCount);
                    OpenPop.Mime.Message getMessage = PopClient.GetMessage(x);
                    Messages.Add(getMessage);

                    foreach (OpenPop.Mime.Message msg in Messages)
                    {
                        MessagePart body = msg.FindFirstHtmlVersion();
                        string bodyPart = string.Empty;
                        bodyPart = string.IsNullOrEmpty(body.GetBodyAsText().ToString()) ? "FAILED TO SEND" : body.GetBodyAsText().ToUpper().Trim();

                        if (msg.Headers.Subject.ToUpper().Trim().Contains("DELIVERED"))
                        {
                            if (msg.Headers.Subject.ToUpper().Trim().Contains(supplierIDUnderscoreRFQNo.ToUpper()))
                            {
                                ret += "success";
                            }
                        }
                        if (msg.Headers.Subject.ToUpper().Trim().Contains("UNDELIVERABLE"))
                        {
                            if (msg.Headers.Subject.ToUpper().Trim().Contains(supplierIDUnderscoreRFQNo.ToUpper()))
                            {
                                ret += "ERROR : " + bodyPart.Substring(0, 62).ToString();
                            }
                        }
                        
                    }

                }

                PopClient.Dispose();
            }


        }
        catch (Exception ex)
        {
            ret = "Response failed from server. Unable to detect email status.";
        }

        return ret;
    }


    public bool isUserAllowed(string loginId, string transaction)
    {
        bool isAllowed = false;
        BLL_Common BLL = new BLL_Common();

        List<Entities_Common_SystemUsers> list = new List<Entities_Common_SystemUsers>();
        list = BLL.Common_checkIfUserHasTransactionsByUserId(loginId);

        if (list.Count > 0)
        {
            foreach (Entities_Common_SystemUsers entity in list)
            {
                if (transaction.Trim().Equals(entity.TransactionType.Trim()))
                {
                    isAllowed = true;
                }
            }
        }

        return isAllowed;
    }

    public string formatNumber(decimal valueIn, int decimalPlaces)
    {
        return string.Format("{0:n" + decimalPlaces.ToString() + "}", valueIn);
    }


    public string sendEmailTo_ERFO_RecommendedContractor(string emailTo, string emailFrom, string emailSubject, string emailBody, string emailAttachment, string supplierName, string supplierIDUnderscoreRFQNo)
    {
        string ret = string.Empty;

        MailMessage msg = new MailMessage();

        foreach (string str in emailTo.Split(','))
        {
            if (str.Length > 0)
            {
                msg.To.Add(new MailAddress(str, supplierName));
            }
        }
        
        msg.From = new MailAddress(emailFrom, "ROHM-ERFO");
        msg.Subject = emailSubject;
        msg.Body = emailBody;

        foreach (string str in emailAttachment.Split(','))
        {
            if (str.Length > 0)
            {
                msg.Attachments.Add(new Attachment(str));
            }
        }

        msg.IsBodyHtml = true;

        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        SmtpClient client = new SmtpClient();
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email-username"], ConfigurationManager.AppSettings["email-password"]);
        client.Port = 587; // You can use Port 25 if 587 is blocked 
        client.Host = "smtp.office365.com";
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.EnableSsl = true;

        try
        {
            client.Send(msg);

            foreach (Attachment attachment in msg.Attachments)
            {
                attachment.Dispose();
            }

            msg.Attachments.Dispose();
            msg = null;

            ret = "success";
            

        }
        catch (SmtpException ex)
        {
            ret = ex.Message.ToString();
            //ret = msg.DeliveryNotificationOptions.ToString();
        }

        return ret;

    }



}
