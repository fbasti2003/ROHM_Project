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
using System.IO;
using System.Collections.Generic;
using System.Net;


namespace REPI_PUR_SOFRA
{
    public partial class TestPage : System.Web.UI.Page
    {
        BLL_Common BLLCommon = new BLL_Common();
        Common COMMON = new Common();
        BLL_RFQ RFQ = new BLL_RFQ();
        BLL_CRF bll_crf = new BLL_CRF();


        protected void Page_Load(object sender, EventArgs e)
        {
            //string path = Server.MapPath("~/URF_Request/URF_MCR19090004/URF_MCR19090004.csv");


            //var contents = File.ReadAllText(path).Split('\n');
            //var csv = from line in contents
            //          select line.Split(',').ToArray();

            //int headerRows = 1;
            //foreach (var row in csv.Skip(headerRows)
            //    .TakeWhile(r => r.Length > 1 && r.Last().Trim().Length > 0))
            //{
            //    String zerothColumnValue = row[0]; // leftmost column
            //    var firstColumnValue = row[9];

            //    Response.Write(zerothColumnValue + " - " + firstColumnValue + "<br />");
            //}

            //Response.Write(Session["CategoryAccess"].ToString() + "-" + Session["Username"].ToString() + "-" + Session["Department"].ToString() + "-" + Session["Division"].ToString() + "-" + Session["HQ"].ToString());

            //List<Entities_Common_ForApproval> listForApproval = new List<Entities_Common_ForApproval>();
            //listForApproval = BLLCommon.Common_GetForApprovalByCategoryId(Session["CategoryAccess"].ToString(), Session["Username"].ToString(), Session["Department"].ToString(), Session["Division"].ToString(), Session["HQ"].ToString(), string.Empty, string.Empty);

            //if (listForApproval != null)
            //{
            //    if (listForApproval.Count > 0)
            //    {
            //        gvForApproval.DataSource = listForApproval;
            //        gvForApproval.DataBind();

            //    }
            //}

            //List<Entities_RFQ_RequestEntry> listDetails = new List<Entities_RFQ_RequestEntry>();
            //listDetails = RFQ.RFQ_TRANSACTION_GetRequestDetailsByRFQNoWithUnitPrice_ByRFQNo("FAC18118821");

            //if (listDetails != null)
            //{
            //    if (listDetails.Count > 0)
            //    {
            //        foreach (Entities_RFQ_RequestEntry entity in listDetails)
            //        {
            //            Response.Write(entity.RdRfqNo + " - " + entity.RdDescription);
            //        }
            //    }
            //}

            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            //string json = (new WebClient()).DownloadString("https://raw.githubusercontent.com/aspsnippets/test/master/Customers.json");
            //Response.Write(json);   
        }

        protected void btnShowPassword_Click(object sender, EventArgs e)
        {


            //string eTour = string.Empty;
            //bool eTour_Scam = false;

            //if (!eTour_Scam)
            //{
            //    eTour = "SABI SA INYO LEGIT EH";
            //} 
            //else 
            //{
            //    eTour = "UKINAM SCAM NGARUD!";
            //}

            txtUsername.Text = CryptorEngine.Decrypt(txtUsername.Text, true);



            //List<Entities_CRF_RequestEntry> list = new List<Entities_CRF_RequestEntry>();
            //list = bll_crf.
            //Response.Write(CryptorEngine.Decrypt(txtUsername.Text, true));


            //List<Entities_RFQ_RequestEntry> listDetails = new List<Entities_RFQ_RequestEntry>();
            //listDetails = RFQ.RFQ_TRANSACTION_GetRequestDetailsByRFQNoWithUnitPrice_ByRFQNo(txtUsername.Text);

            //if (listDetails != null)
            //{
            //    if (listDetails.Count > 0)
            //    {
            //        foreach (Entities_RFQ_RequestEntry entity in listDetails)
            //        {
            //            Response.Write(entity.RdRfqNo + " - " + entity.RdDescription);
            //        }
            //    }
            //}


//            Response.Write(CryptorEngine.Decrypt("87aj6PSK9kdn2PgM94ONI4vaCw5juZsl", true) + "<br/>");
//            Response.Write(CryptorEngine.Decrypt("iYjaDx+IB1q8bQH3QZsfbQ==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("dyjZk+YxK/B2zVSBwpzs0paoWl5sEbbU", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("PExlfpi6HCeL2gsOY7mbJQ==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("J+h+PqRQPLd+q3gUSL871IvaCw5juZsl", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("tNc46HGTBGIBIKTA2gmm2g==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("xYUuf/uGl4m79rebm6m66w==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("rRkdZ4VRWHu8KM20KgaoAA==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("HMsrjVohFuPl7Oit1evJHw==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("wLFTWm6Dd2p0nLqnHfgfWA==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("b/DYH3JN9ha6P0/KsntUKg==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("iLFmWXfGzgvw08y8pHGGQ9LSdhcceB+q", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("EBEbuxM37izwujLQy6MhmQ==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("ZnneZtntIm2RwXaAeeKJyw==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("7h+2h76NC29z733iQOQj4A==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("4niKY09CaFBuapkGqnm2/g==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("YWK4Y9VN+jFBoAukRYmS9g==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("wnzMeO2j0vIu/ouw7PxSPg==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("POeCebqjxRSXrC3VldgIbQ==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("f0900si7aOTLNnt6IrUZS8eJqnlSfI3m", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("Fz+hlEMEUN9qJ7Fkd9EDtw==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("mWxWdWstBams3TLQ54Kazg==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("DZH8wYMmRof+ljyE58gtOQ==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("XRvh85Eo1NNnt5awKlsNfA==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("rRkdZ4VRWHsdKrgaducEgg==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("Qa9og2klDVS+5Cpf7M2T+A==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("B+zpx0KnBBDYGZHfOpWrCQ==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("F9b44mMfIHfQDIzkZ95yE5aoWl5sEbbU", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("gZ1cBywIZb86fDqDTk3WbA==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("aKvEabwb/Y3H+xPTGq7o3g==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("l9jiLe3Q6m1sPXhGeE3+dA==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("RwA3SThh8ZjAdA3TsY6mZI/uCYjtyYJi", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("75yuUS2zOscBD2IhLCi0lw==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("vUkrW5kadWZaxM+PQHek5Q==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("UMr2/xRtocZQ/G6IYANQgA==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("mKR2fOuoi0fl2GcXwv7MWQ==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("EZajsrJlPU5nmiI6p5/9pA==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("/B+7FgoK+pYkTdrXzdsdgDEdRkN4SvD5", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("S6Gg6NNyobi2vLrWWKKoUw==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("WxlA+Yb9iF1FgwiQXwZpKw==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("EQqeSU5INIxUJOe8vjweyD/HVwPI3hYq", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("VrZzp9HMCrqgEjT6myb7c4vaCw5juZsl", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("yfDGm+35kGBRJXrCc9LWkg==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("f2BDfdoCznRXfznObPaoJg==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("xJaaozB1wZJQbdu3WEuss6mhXkz+/1ng", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("G6FP2kVF/uaU6TKc3djdAw==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("87aj6PSK9kdn2PgM94ONI4vaCw5juZsl", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("iDS5WwaJf4pytpdiDcWp+A==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("3i9Z/O9B8/ML9kiLooYmMQ==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("P85CD4CNg0H8UrDXt/5FiQ==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("fbnHQRF39q/RoWNkoyOh0MCeM48VOZNC", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("M9x54un5BnnOv52Ty7B+yHb5bZWfLV+S", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("tr+2ECYur3w+Um56SgXycg==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("oIJIUexDfcBy1TCGFiroGQ==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("8TpghkyILb7pFQBNh+p5zA==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("PlKYNmOx4+Cuak3IoEVCXA==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("GbNeHH/ITbmMCSGYgWx7MyT40GXKMLuR", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("lCYtlgpeP/7XHcQAHVkduovaCw5juZsl", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("Pe2MJRK5pjxvRHSUPDlBq/ovmRa/atz8", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("0TYeAQ8tA/cYebU2HzWVWw==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("xgPgFHL2C+FAkTBhQ0YKuVD2qvWbnC/c", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("2Bi0Rh0GnxuoUZR+kn1ElJVGGy1uwp9z", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("YV2V1XQH7vy3Uwe5XBce8A==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("ieiz5rRlf12k7OgyF6Alfg==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("EQ5ikv5IDit5IQ6KdmeC8g==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("tOSHipx1mXcn22SPLpCw0g==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("oF1EFOTa+6+ANy1qRhHt1Q==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("9r8/MkYVrBKpG/EuMS+2MQ==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("VTiqF3+FluroNODBUrHBNlvrYjtBayCe", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("9XDJEWqadn510mVikQNWALKGPyrNhh9l", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("bZc9GIwewB0vZEmbG4l1xQ==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("CXXvJGAxQrhAADvT9ug4QA==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("h0ogwn0ZkERe7YAXGUExZQ==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("gOf4a3dWnFN81trnte3AFA==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("kD4Q6kJnsm0pnMIJcml5og==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("sqNgRUrj0H5g3ZVQ7HQR2g==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("KuuRtxhKjCrUIHpXN9L9J4vaCw5juZsl", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("zQI8gviHzms=", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("6IB4KqvAEejYMQBmVGFTbTRz3X3uo02p", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("UMr2/xRtocapxKH/Jh5rz2fE1ZUDEfbh", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("347q7LmfzLZpwyZ0jhRpXA==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("3xwkLNv5ITTnew6WWafJbA==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("kXzelsWclBbpisyEKVMQew==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("oIJIUexDfcCe3WX+JDMD4Q==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("QV5thEoAiJgIwV6qptMtVYvaCw5juZsl", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("Wl14z2PaRQYs9xnsqYvMPA==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("6tAzW1KotSyL2gsOY7mbJQ==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("5OOjwT9GwbzRKynQjcZtOQ==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("M9x54un5BnnOv52Ty7B+yHb5bZWfLV+S", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("iNzs1ofXINQ=", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("PsnnW+7WEVf/jpQWCCJRIg==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("AYmbeNWgu8VyHPTnW5gcaQ==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("MFdv4v8NF4tHGp57u+xE4IvaCw5juZsl", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("9/Pr+0Fh0fdLyp5XCoXJ2g==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("OwGvpIAeWn6VraBD1yG/uJSMJA9QwaGK", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("tr+2ECYur3yN3x2I8F76ort9TIPXHlQp", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("68xq+2GXoZx0cD0DX0biCIwjJ84DJKTD", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("bjZ7ow4pAfJGHlEjWjXHAXY4Dqr4Xx8yi9oLDmO5myU=", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("oaZ4xkijJcNQP+Q6Zu9e4w==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("FmXUz6APC9660nTNokTBfg==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("a4finR7a3WyQVXbRz8fjgo/uCYjtyYJi", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("ekQ34HyBskW7uaRdVc/G5YvaCw5juZsl", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("3/M2CptI4KLLvJDqRabvhg==", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("cySIbKmcRwSLSMZ8jOBYKWJUVYKNAI8L", true) + "<br/>");
//Response.Write(CryptorEngine.Decrypt("4wJ6DtLWTTFkeGeJE+uA6/h2qP2D3hzc", true) + "<br/>");

        }

    }
}
