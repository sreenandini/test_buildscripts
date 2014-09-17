using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.MonitoringService
{
     #region Namespaces

    using System.Data.Linq.Mapping;
using System.Xml.Linq;

    #endregion Namespaces

    public class SpecificSiteDetails
    {
        public int Site_ID{get;set;}

        public string Site_Code{get;set;}

        public string WebURL{get;set;}

        public System.Nullable<bool> IsCertificateRequired{get;set;}

        public string CertificateIssuer{get;set;}

        public string ReadTime{get;set;}

        public bool isSTMAlertSend { get; set; }
    }

    public class ReadList
    {
        public int Site_ID { get; set; }

        public string Site_Code { get; set; }

        public string ReadTime { get; set; }

        public bool IsProcessed { get; set; }
    }


    public partial class GetSpecificSiteDetails
    {

    private int _Site_ID;

    private string _Site_Code;

    private string _WebURL;

    private System.Nullable<bool> _IsCertificateRequired;

    private string _CertificateIssuer;

    private string _ReadTime;

    public GetSpecificSiteDetails()
    {
    }

    [Column(Storage = "_Site_ID", DbType = "Int NOT NULL")]
    public int Site_ID
    {
        get
        {
            return this._Site_ID;
        }
        set
        {
            if ((this._Site_ID != value))
            {
                this._Site_ID = value;
            }
        }
    }

    [Column(Storage = "_Site_Code", DbType = "VarChar(50)")]
    public string Site_Code
    {
        get
        {
            return this._Site_Code;
        }
        set
        {
            if ((this._Site_Code != value))
            {
                this._Site_Code = value;
            }
        }
    }

    [Column(Storage = "_WebURL", DbType = "VarChar(2000)")]
    public string WebURL
    {
        get
        {
            return this._WebURL;
        }
        set
        {
            if ((this._WebURL != value))
            {
                this._WebURL = value;
            }
        }
    }

    [Column(Storage = "_IsCertificateRequired", DbType = "Bit")]
    public System.Nullable<bool> IsCertificateRequired
    {
        get
        {
            return this._IsCertificateRequired;
        }
        set
        {
            if ((this._IsCertificateRequired != value))
            {
                this._IsCertificateRequired = value;
            }
        }
    }

    [Column(Storage = "_CertificateIssuer", DbType = "VarChar(50)")]
    public string CertificateIssuer
    {
        get
        {
            return this._CertificateIssuer;
        }
        set
        {
            if ((this._CertificateIssuer != value))
            {
                this._CertificateIssuer = value;
            }
        }
    }

    [Column(Storage = "_ReadTime", DbType = "VarChar(500)")]
    public string ReadTime
    {
        get
        {
            return this._ReadTime;
        }
        set
        {
            if ((this._ReadTime != value))
            {
                this._ReadTime = value;
            }
        }
    }
}

    
    public partial class SiteStatusEntity
    {

        public int Site_ID;

        public System.Xml.Linq.XElement Site_Status;
    }

    public partial class GetSiteStatusByIDResult
    {

        private int _Site_ID;

        private System.Xml.Linq.XElement _Site_Status;

        public GetSiteStatusByIDResult()
        {
        }

        [Column(Storage = "_Site_ID", DbType = "Int NOT NULL")]
        public int Site_ID
        {
            get
            {
                return this._Site_ID;
            }
            set
            {
                if ((this._Site_ID != value))
                {
                    this._Site_ID = value;
                }
            }
        }

        [Column(Storage = "_Site_Status", DbType = "Xml")]
        public System.Xml.Linq.XElement Site_Status
        {
            get
            {
                return this._Site_Status;
            }
            set
            {
                if ((this._Site_Status != value))
                {
                    this._Site_Status = value;
                }
            }
        }
    }
}
