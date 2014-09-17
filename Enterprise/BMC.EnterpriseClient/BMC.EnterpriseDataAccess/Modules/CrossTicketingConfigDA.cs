using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.rsp_GetFullyConfiguredSites")]
        public ISingleResult<CrossTicketingSettingResult> GetCrossTicketConfigSites([Parameter(Name = "SITECODE", DbType = "VarChar(50)")] string sITECODE)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sITECODE);
            return ((ISingleResult<CrossTicketingSettingResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.esp_InsertSiteAlliance")]
        public int InsertIntoSiteAlliance([Parameter(Name = "ClientSiteCode", DbType = "VarChar(50)")] string clientSiteCode, [Parameter(Name = "HostSiteCode", DbType = "VarChar(50)")] string hostSiteCode, [Parameter(Name = "HostSiteURL", DbType = "VarChar(2000)")] string hostSiteURL, [Parameter(Name = "IsCashableRedeemable", DbType = "Bit")] System.Nullable<bool> isCashableRedeemable, [Parameter(Name = "IsPromoRedeemable", DbType = "Bit")] System.Nullable<bool> isPromoRedeemable)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), clientSiteCode, hostSiteCode, hostSiteURL, isCashableRedeemable, isPromoRedeemable);
            return ((int)(result.ReturnValue));
        }
    }

    public class CrossTicketingSettingResult
    {

        private string _ClientSiteCode;

        private bool _IsCashableRedeemable;

        private bool _IsPromoRedeemable;

        private string _HostSiteURL;

        public CrossTicketingSettingResult()
        {
        }

        [Column(Storage = "_ClientSiteCode", DbType = "VarChar(50)")]
        public string ClientSiteCode
        {
            get
            {
                return this._ClientSiteCode;
            }
            set
            {
                if ((this._ClientSiteCode != value))
                {
                    this._ClientSiteCode = value;
                }
            }
        }

        [Column(Storage = "_IsCashableRedeemable", DbType = "Bit NOT NULL")]
        public bool IsCashableRedeemable
        {
            get
            {
                return this._IsCashableRedeemable;
            }
            set
            {
                if ((this._IsCashableRedeemable != value))
                {
                    this._IsCashableRedeemable = value;
                }
            }
        }

        [Column(Storage = "_IsPromoRedeemable", DbType = "Bit NOT NULL")]
        public bool IsPromoRedeemable
        {
            get
            {
                return this._IsPromoRedeemable;
            }
            set
            {
                if ((this._IsPromoRedeemable != value))
                {
                    this._IsPromoRedeemable = value;
                }
            }
        }

        [Column(Storage = "_HostSiteURL", DbType = "VarChar(2000)")]
        public string HostSiteURL
        {
            get
            {
                return this._HostSiteURL;
            }
            set
            {
                if ((this._HostSiteURL != value))
                {
                    this._HostSiteURL = value;
                }
            }
        }
    }
}
