using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;

namespace BMCTicketingConfig
{
    public class GetSitesResult
    {

        private string _ClientSiteCode;

        private bool _IsCashableRedeemable;

        private bool _IsPromoRedeemable;

        private string _HostSiteURL;

        public GetSitesResult()
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
