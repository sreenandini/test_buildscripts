
namespace BMC.CashDeskOperator
{
    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using System.Reflection;
    using System.ComponentModel;


    [DatabaseAttribute(Name = "Exchange")]
    public class CrossTicketingSettingDetails : DataContext
    {
        public CrossTicketingSettingDetails(string connectionString) :
            base(connectionString)
        {
        }


        [Function(Name = "dbo.rsp_GetCrossTicketingSettings")]
        public ISingleResult<GetCrossTicketingSettingsResult> GetCrossTicketingSettings([Parameter(Name = "SiteCode", DbType = "VarChar(50)")] string siteCode)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteCode);
            return ((ISingleResult<GetCrossTicketingSettingsResult>)(result.ReturnValue));
        }

        public partial class GetCrossTicketingSettingsResult
        {

            private string _HostSiteCode;

            private string _IsCashableRedeemable;

            private string _IsPromoRedeemable;

            private string _HostSiteURL;

            private string _IsCrossTicketingEnabled;

            public GetCrossTicketingSettingsResult()
            {
            }

            [Column(Storage = "_HostSiteCode", DbType = "VarChar(50)")]
            public string HostSiteCode
            {
                get
                {
                    return this._HostSiteCode;
                }
                set
                {
                    if ((this._HostSiteCode != value))
                    {
                        this._HostSiteCode = value;
                    }
                }
            }

            [Column(Storage = "_IsCashableRedeemable", DbType = "VarChar(5)")]
            public string IsCashableRedeemable
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

            [Column(Storage = "_IsPromoRedeemable", DbType = "VarChar(5)")]
            public string IsPromoRedeemable
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

            [Column(Storage = "_HostSiteURL", DbType = "VarChar(2000) NOT NULL", CanBeNull = false)]
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

            [Column(Storage = "_IsCrossTicketingEnabled", DbType = "VarChar(5)")]
            public string IsCrossTicketingEnabled
            {
                get
                {
                    return this._IsCrossTicketingEnabled;
                }
                set
                {
                    if ((this._IsCrossTicketingEnabled != value))
                    {
                        this._IsCrossTicketingEnabled = value;
                    }
                }
            }
        }
       
    }


}

