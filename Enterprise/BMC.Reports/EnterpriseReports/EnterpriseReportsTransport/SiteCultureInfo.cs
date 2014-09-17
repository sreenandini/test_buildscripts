using System.Data.Linq.Mapping;

namespace BMC.EnterpriseReportsTransport
{
    public partial class SiteCultureInfo
    {
        private string _UserName;
        private string _LanguageCulture;
        private string _CurrencyCulture;
        private string _DateCulture;
        private string _RegionCulture;

        public SiteCultureInfo()
        {
        }

        //public SiteCultureInfo(string userName, string languageCulture, string currencyCulture, string dateCulture)
        //{
        //    UserName            =   userName;
        //    LanguageCulture     =   languageCulture;
        //    CurrencyCulture     =   currencyCulture;
        //    DateCulture         =   dateCulture;
        //}

        [Column(Storage = "_UserName", DbType = "VarChar(200)")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                if ((this._UserName != value))
                {
                    this._UserName = value;
                }
            }
        }

        [Column(Storage = "_LanguageCulture", DbType = "VarChar(6)")]
        public string LanguageCulture
        {
            get
            {
                return this._LanguageCulture;
            }
            set
            {
                if ((this._LanguageCulture != value))
                {
                    this._LanguageCulture = value;
                }
            }
        }

        [Column(Storage = "_CurrencyCulture", DbType = "VarChar(6)")]
        public string CurrencyCulture
        {
            get
            {
                return this._CurrencyCulture;
            }
            set
            {
                if ((this._CurrencyCulture != value))
                {
                    this._CurrencyCulture = value;
                }
            }
        }

        [Column(Storage = "_DateCulture", DbType = "VarChar(6)")]
        public string DateCulture
        {
            get
            {
                return this._DateCulture;
            }
            set
            {
                if ((this._DateCulture != value))
                {
                    this._DateCulture = value;
                }
            }
        }

        [Column(Storage = "_RegionCulture", DbType = "VarChar(50)")]
        public string RegionCulture
        {
            get
            {
                return this._RegionCulture;
            }
            set
            {
                if ((this._RegionCulture != value))
                {
                    this._RegionCulture = value;
                }
            }
        }
    }
}
