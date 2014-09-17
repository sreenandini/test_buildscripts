using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public class CrossTicketingSetting
    {

        private string _ClientSiteCode;

        private bool _IsCashableRedeemable;

        private bool _IsPromoRedeemable;

        private string _HostSiteURL;

        public CrossTicketingSetting()
        {
        }

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
