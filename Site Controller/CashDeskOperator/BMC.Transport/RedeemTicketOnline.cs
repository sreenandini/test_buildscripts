using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace BMC.Transport.CashDeskOperatorEntity
{
    ///Redeem Ticket Online    
    [Serializable]
    public class RTOnlineTicketDetail
    {
        public double TicketValue { get; set; }
        public string TicketString { get; set; }
        public string RedeemedMachine { get; set; }
        public string RedeemedDevice { get; set; }
        public DateTime RedeemedDate { get; set; }
        public decimal RedeemedAmount { get; set; }
        public string TicketStatus { get; set; }
        public int TicketStatusCode { get; set; }
        public string TicketWarning { get; set; }
        public int InstallationNumber { get; set; }
        public bool EnableTickerPrintDetails { get; set; }
        public bool ValidTicket { get; set; }
        public bool ShowOfflineTicketScreen { get; set; }
        public Int64 CustomerId { get; set; }
        public int AuthorizedUser_No { get; set; }
        public DateTime Authorized_Date { get; set; }

        public string PrintedDevice { get; set; }
        public DateTime PrintedDate { get; set; }
        public int iVoucherid  { get; set; }
        public string VoucherXMLData { get; set; }

        public string ClientSiteCode { get; set; }
        public string HostSiteCode { get; set; }

        public string RedeemedUser { get; set; }
 public string TicketErrorMessage { get; set; }
        public int CardRequired { get; set; }
        public string PlayerCardNumber { get; set; }
        public int TicketType { get; set; }
        public int CurrentTicketStatus { get; set; }
    }


    public class ReedeemTicketDetailsComms
    {
        public string Barcode { get; set; }
        public string DeviceId { get; set; }
        public int retAmount { get; set; }
        public int retResult { get; set; }
        public string  retBarcode { get; set; }
        public int retTicketType { get; set; }
        public int iErrorCode { get; set; }

        public string HostSiteCode { get; set; }
        public string ClientSiteCode { get; set; }

        public int iVoucherid { get; set; }
        public string VoucherXMLData { get; set; }

    }

    public class RTOnlineReceiptDetail
    {
        public string TicketString { get; set; }
        public double TickerAmount { get; set; }
        public double Payout { get; set; }
        public string PrintDevice { get; set; }
        public DateTime DatePrinted { get; set; }
        public string DeviceName { get; set; }
        public string MachineClassName { get; set; }
        public string BarPositionName { get; set; }
        public string DeviceBarPosition { get; set; }
        public string OfflineRedeemAsset { get; set; }
        public string OfflineRedeemBarPosition { get; set; }
    }

    public class RTOnlineWageredDropDetail
    {
        public double WageredAmount { get; set; }
        public double DropAmount { get; set; }
        public string TicketString { get; set; }
    }

    public enum CrossTicketingErrorCodes
    {
        CrossTicketingDisabled,
        ValidSite,
    }


    public partial class ValidateSiteCode
	{
		
		private string _SiteCode;
		
		private string _URL;

        public ValidateSiteCode()
		{
		}
		
		[Column(Storage="_SiteCode", DbType="VarChar(4) NOT NULL", CanBeNull=false)]
		public string SiteCode
		{
			get
			{
				return this._SiteCode;
			}
			set
			{
				if ((this._SiteCode != value))
				{
					this._SiteCode = value;
				}
			}
		}
		
		[Column(Storage="_URL", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string URL
		{
			get
			{
				return this._URL;
			}
			set
			{
				if ((this._URL != value))
				{
					this._URL = value;
				}
			}
		}
	}

    public partial class CrossTicketingEnabledResult
    {

        private System.Nullable<bool> _isCrossTicketingEnabled;

        public CrossTicketingEnabledResult()
        {
        }

        [Column(Storage = "_isCrossTicketingEnabled", DbType = "Bit")]
        public System.Nullable<bool> isCrossTicketingEnabled
        {
            get
            {
                return this._isCrossTicketingEnabled;
            }
            set
            {
                if ((this._isCrossTicketingEnabled != value))
                {
                    this._isCrossTicketingEnabled = value;
                }
            }
        }
    }
}
