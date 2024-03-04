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

public class Entities_Common_MTSupplier
{
    public Entities_Common_MTSupplier()
    {
    }

    private string name;
    private string category;
    private string headRefId;
    private string emailAddress;
    private int refId;
    private string addedBy;
    private string updatedBy;
    private string addedDate;
    private string updatedDate;
    private string isDisabled;
    private string disabledBy;
    private string allowedCategory;
    private string attachmentFile;
    private string address;

    public string Address
    {
        get { return address; }
        set { address = value; }
    }

    public string AttachmentFile
    {
        get { return attachmentFile; }
        set { attachmentFile = value; }
    }

    public string AllowedCategory
    {
        get { return allowedCategory; }
        set { allowedCategory = value; }
    }

    public string HeadRefId
    {
        get { return headRefId; }
        set { headRefId = value; }
    }

    public string DisabledBy
    {
        get { return disabledBy; }
        set { disabledBy = value; }
    }

    public string IsDisabled
    {
        get { return isDisabled; }
        set { isDisabled = value; }
    }



    public string EmailAddress
    {
        get { return emailAddress; }
        set { emailAddress = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Category
    {
        get { return category; }
        set { category = value; }
    }

    public int RefId
    {
        get { return refId; }
        set { refId = value; }
    }

    public string AddedBy
    {
        get { return addedBy; }
        set { addedBy = value; }
    }

    public string UpdatedBy
    {
        get { return updatedBy; }
        set { updatedBy = value; }
    }

    public string AddedDate
    {
        get { return addedDate; }
        set { addedDate = value; }
    }

    public string UpdatedDate
    {
        get { return updatedDate; }
        set { updatedDate = value; }
    }


}
