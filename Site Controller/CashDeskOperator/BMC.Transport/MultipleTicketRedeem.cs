using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BMC.Transport
{
    public class ReedemTicketInfo : INotifyPropertyChanged
    {
        private string _Barcode;
        private string _Status;
        private decimal _Amount;
        private bool _IsVoucherRedeemed;
        private int _TicketStatus;
        private bool _IsRemoveVoucherEnable;

        public string Barcode
        {
            get 
            {
                return this._Barcode;
 
            }
            set
            {
                if( (this._Barcode != value))
                {
                    this._Barcode = value;
                    if (null != PropertyChanged) PropertyChanged(this, new PropertyChangedEventArgs("Barcode"));
                }
            }

        }

        public string Status
        {
            get 
            { 
                return this._Status; 
            }
            set 
            { 
                if ((this._Status != value))
                { 
                    this._Status = value;
                    if (null != PropertyChanged) PropertyChanged(this, new PropertyChangedEventArgs("Status"));
                } 
            }
        
        }

        public decimal Amount
        {
            get
            {
                return this._Amount;

            }
            set
            {
                if ((this._Amount != value))
                {
                    this._Amount = value;
                    if (null != PropertyChanged) PropertyChanged(this, new PropertyChangedEventArgs("Amount"));
                }
            
            }
        
        }


        public int TicketStatus
        {
            get 
            {
                return _TicketStatus;
            }
            set
            {
        
                _TicketStatus = value;
                if (null != PropertyChanged) PropertyChanged(this, new PropertyChangedEventArgs("TicketStatus"));
            }

        }


        public bool IsVoucherRedeemed
        {
            get
            {
                return this._IsVoucherRedeemed;
            }

            set 
            
            {
                if ((this._IsVoucherRedeemed != value))
                {
                    this._IsVoucherRedeemed = value;
                    if (null != PropertyChanged) PropertyChanged(this, new PropertyChangedEventArgs("IsVoucherRedeemed"));
                }
            }
        }

        public bool IsRemoveVoucherEnable
        {
            get
            {
                return this._IsRemoveVoucherEnable;
            }

            set
            {
                if ((this._IsRemoveVoucherEnable != value))
                {
                    this._IsRemoveVoucherEnable = value;
                    if (null != PropertyChanged) PropertyChanged(this, new PropertyChangedEventArgs("IsRemoveVoucherEnable"));
                }
            }
        }
        public BMC.Transport.CashDeskOperatorEntity.RTOnlineTicketDetail oRTOnlineTicketDetail { get; set; }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    public partial class rsp_ValidateVoucherForMultipleVoucherRedemptionResult
    {

        private int _iStatus;

        private decimal _Amount;

        private System.Nullable<System.DateTime> _PrintDate;

        private int _VoucherID;

        public rsp_ValidateVoucherForMultipleVoucherRedemptionResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iStatus", DbType = "Int NOT NULL")]
        public int iStatus
        {
            get
            {
                return this._iStatus;
            }
            set
            {
                if ((this._iStatus != value))
                {
                    this._iStatus = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Amount", DbType = "Decimal(18,2) NOT NULL")]
        public decimal Amount
        {
            get
            {
                return this._Amount;
            }
            set
            {
                if ((this._Amount != value))
                {
                    this._Amount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PrintDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> PrintDate
        {
            get
            {
                return this._PrintDate;
            }
            set
            {
                if ((this._PrintDate != value))
                {
                    this._PrintDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_VoucherID", DbType = "Int NOT NULL")]
        public int VoucherID
        {
            get
            {
                return this._VoucherID;
            }
            set
            {
                if ((this._VoucherID != value))
                {
                    this._VoucherID = value;
                }
            }
        }
    }
}
