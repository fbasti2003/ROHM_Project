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

public class Entities_Common_TransactionLogs
{
    public Entities_Common_TransactionLogs()
    {
    }

    private string transactionLogs;
    private string transactionDateFrom;
    private string transactionDateTo;
    private string transactionDate;



    public string TransactionLogs
    {
        get { return transactionLogs; }
        set { transactionLogs = value; }
    }

    public string TransactionDateFrom
    {
        get { return transactionDateFrom; }
        set { transactionDateFrom = value; }
    }

    public string TransactionDateTo
    {
        get { return transactionDateTo; }
        set { transactionDateTo = value; }
    }

    public string TransactionDate
    {
        get { return transactionDate; }
        set { transactionDate = value; }
    }




}
