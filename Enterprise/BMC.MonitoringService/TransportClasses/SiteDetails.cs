namespace BMC.MonitoringService
{
    #region Namespaces

    using System.Data.Linq.Mapping;

    #endregion Namespaces

    #region Public Class

    public class SiteDetails
    {
        #region Private Variables

        private int _Site_ID;

        private string _Site_Code;

        private string _Site_Name;

        private string _WebURL;

        #endregion Private Variables

        #region Constructor

        public SiteDetails()
        {

        }

        #endregion Constructor

        #region Public Properties

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

        [Column(Storage = "_Site_Name", DbType = "VarChar(50)")]
        public string Site_Name
        {
            get
            {
                return this._Site_Name;
            }
            set
            {
                if ((this._Site_Name != value))
                {
                    this._Site_Name = value;
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

        #endregion Public Properties
    }

    #endregion Public Class
}
