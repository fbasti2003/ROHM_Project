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

public class Factory
{
    public Factory()
    {
    }

    private static DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SqlClient");

    public static DbConnection CreateConnection()
    {
        DbConnection conn = fact.CreateConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_SOFRA"].ConnectionString;
        return conn;
    }
    public static DbConnection CreateConnection_Data_Link()
    {
        DbConnection conn = fact.CreateConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["REPI_Data_Link"].ConnectionString;
        return conn;
    }
    public static DbConnection CreateConnection_FROM_UPSVGAD2_DB_Instance()
    {
        DbConnection conn = fact.CreateConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["UPSVGAD2_DB_Instance"].ConnectionString;
        return conn;
    }
    public static DbCommand CreateCommand()
    {
        DbCommand cmd = fact.CreateCommand();
        return cmd;

    }
    public static DbDataAdapter CreateAdapter()
    {
        DbDataAdapter da = fact.CreateDataAdapter();
        return da;
    }

    public static DbParameter CreateParameter(string parameterName, object value)
    {
        DbParameter param = fact.CreateParameter();
        param.ParameterName = parameterName;
        param.Value = value;
        return param;
    }

}
