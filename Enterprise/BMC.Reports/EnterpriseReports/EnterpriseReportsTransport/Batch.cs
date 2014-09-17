using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.EnterpriseReportsTransport
{
    [Table(Name = "dbo.Meter_history")]
    public partial class Batch
    {

        private int _MH_ID;

        private string _MH_Process;

        private System.Nullable<char> _MH_Type;

        private System.Nullable<int> _MH_LinkReference;

        private string _MH_Reference;

        private System.Nullable<int> _MH_Installation_No;

        private System.Nullable<int> _MH_PAYOUT_FLOAT_TOKEN;

        private System.Nullable<int> _MH_PAYOUT_FLOAT_10P;

        private System.Nullable<int> _MH_PAYOUT_FLOAT_20P;

        private System.Nullable<int> _MH_PAYOUT_FLOAT_50P;

        private System.Nullable<int> _MH_PAYOUT_FLOAT_100P;

        private System.Nullable<int> _MH_CASH_IN_2P;

        private System.Nullable<int> _MH_CASH_IN_5P;

        private System.Nullable<int> _MH_CASH_IN_10P;

        private System.Nullable<int> _MH_CASH_IN_20P;

        private System.Nullable<int> _MH_CASH_IN_50P;

        private System.Nullable<int> _MH_CASH_IN_100P;

        private System.Nullable<int> _MH_CASH_IN_200P;

        private System.Nullable<int> _MH_CASH_IN_500P;

        private System.Nullable<int> _MH_CASH_IN_1000P;

        private System.Nullable<int> _MH_CASH_IN_2000P;

        private System.Nullable<int> _MH_CASH_IN_5000P;

        private System.Nullable<int> _MH_CASH_IN_10000P;

        private System.Nullable<int> _MH_CASH_IN_20000P;

        private System.Nullable<int> _MH_CASH_IN_50000P;

        private System.Nullable<int> _MH_CASH_IN_100000P;

        private System.Nullable<int> _MH_TOKEN_IN_5P;

        private System.Nullable<int> _MH_TOKEN_IN_10P;

        private System.Nullable<int> _MH_TOKEN_IN_20P;

        private System.Nullable<int> _MH_TOKEN_IN_50P;

        private System.Nullable<int> _MH_TOKEN_IN_100P;

        private System.Nullable<int> _MH_TOKEN_IN_200P;

        private System.Nullable<int> _MH_TOKEN_IN_500P;

        private System.Nullable<int> _MH_TOKEN_IN_1000P;

        private System.Nullable<int> _MH_CASH_OUT_2P;

        private System.Nullable<int> _MH_CASH_OUT_5P;

        private System.Nullable<int> _MH_CASH_OUT_10P;

        private System.Nullable<int> _MH_CASH_OUT_20P;

        private System.Nullable<int> _MH_CASH_OUT_50P;

        private System.Nullable<int> _MH_CASH_OUT_100P;

        private System.Nullable<int> _MH_CASH_OUT_200P;

        private System.Nullable<int> _MH_CASH_OUT_500P;

        private System.Nullable<int> _MH_CASH_OUT_1000P;

        private System.Nullable<int> _MH_CASH_OUT_2000P;

        private System.Nullable<int> _MH_CASH_OUT_5000P;

        private System.Nullable<int> _MH_CASH_OUT_10000P;

        private System.Nullable<int> _MH_CASH_OUT_20000P;

        private System.Nullable<int> _MH_CASH_OUT_50000P;

        private System.Nullable<int> _MH_CASH_OUT_100000P;

        private System.Nullable<int> _MH_TOKEN_OUT_5P;

        private System.Nullable<int> _MH_TOKEN_OUT_10P;

        private System.Nullable<int> _MH_TOKEN_OUT_20P;

        private System.Nullable<int> _MH_TOKEN_OUT_50P;

        private System.Nullable<int> _MH_TOKEN_OUT_100P;

        private System.Nullable<int> _MH_TOKEN_OUT_200P;

        private System.Nullable<int> _MH_TOKEN_OUT_500P;

        private System.Nullable<int> _MH_TOKEN_OUT_1000P;

        private System.Nullable<int> _MH_CASH_REFILL_5P;

        private System.Nullable<int> _MH_CASH_REFILL_10P;

        private System.Nullable<int> _MH_CASH_REFILL_20P;

        private System.Nullable<int> _MH_CASH_REFILL_50P;

        private System.Nullable<int> _MH_CASH_REFILL_100P;

        private System.Nullable<int> _MH_CASH_REFILL_200P;

        private System.Nullable<int> _MH_CASH_REFILL_500P;

        private System.Nullable<int> _MH_CASH_REFILL_1000P;

        private System.Nullable<int> _MH_CASH_REFILL_2000P;

        private System.Nullable<int> _MH_CASH_REFILL_5000P;

        private System.Nullable<int> _MH_CASH_REFILL_10000P;

        private System.Nullable<int> _MH_CASH_REFILL_20000P;

        private System.Nullable<int> _MH_CASH_REFILL_50000P;

        private System.Nullable<int> _MH_CASH_REFILL_100000P;

        private System.Nullable<int> _MH_TOKEN_REFILL_5P;

        private System.Nullable<int> _MH_TOKEN_REFILL_10P;

        private System.Nullable<int> _MH_TOKEN_REFILL_20P;

        private System.Nullable<int> _MH_TOKEN_REFILL_50P;

        private System.Nullable<int> _MH_TOKEN_REFILL_100P;

        private System.Nullable<int> _MH_TOKEN_REFILL_200P;

        private System.Nullable<int> _MH_TOKEN_REFILL_500P;

        private System.Nullable<int> _MH_TOKEN_REFILL_1000P;

        private System.Nullable<int> _MH_COINS_IN;

        private System.Nullable<int> _MH_COINS_OUT;

        private System.Nullable<int> _MH_COIN_DROP;

        private System.Nullable<int> _MH_HANDPAY;

        private System.Nullable<int> _MH_EXTERNAL_CREDIT;

        private System.Nullable<int> _MH_GAMES_BET;

        private System.Nullable<int> _MH_GAMES_WON;

        private System.Nullable<int> _MH_NOTES;

        private System.Nullable<int> _MH_VTP;

        private System.Nullable<int> _MH_CANCELLED_CREDITS;

        private System.Nullable<int> _MH_GAMES_LOST;

        private System.Nullable<int> _MH_GAMES_SINCE_POWER_UP;

        private System.Nullable<int> _MH_TRUE_COIN_IN;

        private System.Nullable<int> _MH_TRUE_COIN_OUT;

        private System.Nullable<int> _MH_CURRENT_CREDITS;

        private System.Nullable<int> _MH_JACKPOT;

        private System.Nullable<int> _MH_BILL_1;

        private System.Nullable<int> _MH_BILL_2;

        private System.Nullable<int> _MH_BILL_5;

        private System.Nullable<int> _MH_BILL_10;

        private System.Nullable<int> _MH_BILL_20;

        private System.Nullable<int> _MH_BILL_50;

        private System.Nullable<int> _MH_BILL_100;

        private System.Nullable<int> _MH_BILL_250;

        private System.Nullable<int> _MH_BILL_10000;

        private System.Nullable<int> _MH_BILL_20000;

        private System.Nullable<int> _MH_BILL_50000;

        private System.Nullable<int> _MH_BILL_100000;

        private System.Nullable<int> _MH_TICKET_PRINTED_QTY;

        private System.Nullable<int> _MH_TICKET_PRINTED_VALUE;

        private System.Nullable<int> _MH_TICKET_INSERTED_QTY;

        private System.Nullable<int> _MH_TICKET_INSERTED_VALUE;

        private string _MH_Datetime;

        private System.Nullable<int> _MH_progressive_win_value;

        private System.Nullable<int> _MH_progressive_win_Handpay_value;

        private System.Nullable<int> _MH_Mystery_Machine_Paid;

        private System.Nullable<int> _MH_Mystery_Attendant_Paid;

        private System.Nullable<int> _MH_TICKETS_PRINTED_NONCASHABLE_QTY;

        private System.Nullable<int> _MH_TICKETS_PRINTED_NONCASHABLE_VALUE;

        private System.Nullable<int> _MH_TICKETS_INSERTED_NONCASHABLE_QTY;

        private System.Nullable<int> _MH_TICKETS_INSERTED_NONCASHABLE_VALUE;

        private System.Nullable<int> _MH_Promo_Cashable_EFT_IN;

        private System.Nullable<int> _MH_Promo_Cashable_EFT_OUT;

        private System.Nullable<int> _MH_NonCashable_EFT_IN;

        private System.Nullable<int> _MH_NonCashable_EFT_OUT;

        private System.Nullable<int> _MH_Cashable_EFT_IN;

        private System.Nullable<int> _MH_Cashable_EFT_OUT;

        private System.Nullable<int> _MH_BILL_200;

        private System.Nullable<int> _MH_BILL_500;

        public Batch()
        {
        }

        [Column(Storage = "_MH_ID", AutoSync = AutoSync.Always, DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true)]
        public int MH_ID
        {
            get
            {
                return this._MH_ID;
            }
            set
            {
                if ((this._MH_ID != value))
                {
                    this._MH_ID = value;
                }
            }
        }

        [Column(Storage = "_MH_Process", DbType = "VarChar(10)")]
        public string MH_Process
        {
            get
            {
                return this._MH_Process;
            }
            set
            {
                if ((this._MH_Process != value))
                {
                    this._MH_Process = value;
                }
            }
        }

        [Column(Storage = "_MH_Type", DbType = "VarChar(1)")]
        public System.Nullable<char> MH_Type
        {
            get
            {
                return this._MH_Type;
            }
            set
            {
                if ((this._MH_Type != value))
                {
                    this._MH_Type = value;
                }
            }
        }

        [Column(Storage = "_MH_LinkReference", DbType = "Int")]
        public System.Nullable<int> MH_LinkReference
        {
            get
            {
                return this._MH_LinkReference;
            }
            set
            {
                if ((this._MH_LinkReference != value))
                {
                    this._MH_LinkReference = value;
                }
            }
        }

        [Column(Storage = "_MH_Reference", DbType = "VarChar(255)")]
        public string MH_Reference
        {
            get
            {
                return this._MH_Reference;
            }
            set
            {
                if ((this._MH_Reference != value))
                {
                    this._MH_Reference = value;
                }
            }
        }

        [Column(Storage = "_MH_Installation_No", DbType = "Int")]
        public System.Nullable<int> MH_Installation_No
        {
            get
            {
                return this._MH_Installation_No;
            }
            set
            {
                if ((this._MH_Installation_No != value))
                {
                    this._MH_Installation_No = value;
                }
            }
        }

        [Column(Storage = "_MH_PAYOUT_FLOAT_TOKEN", DbType = "Int")]
        public System.Nullable<int> MH_PAYOUT_FLOAT_TOKEN
        {
            get
            {
                return this._MH_PAYOUT_FLOAT_TOKEN;
            }
            set
            {
                if ((this._MH_PAYOUT_FLOAT_TOKEN != value))
                {
                    this._MH_PAYOUT_FLOAT_TOKEN = value;
                }
            }
        }

        [Column(Storage = "_MH_PAYOUT_FLOAT_10P", DbType = "Int")]
        public System.Nullable<int> MH_PAYOUT_FLOAT_10P
        {
            get
            {
                return this._MH_PAYOUT_FLOAT_10P;
            }
            set
            {
                if ((this._MH_PAYOUT_FLOAT_10P != value))
                {
                    this._MH_PAYOUT_FLOAT_10P = value;
                }
            }
        }

        [Column(Storage = "_MH_PAYOUT_FLOAT_20P", DbType = "Int")]
        public System.Nullable<int> MH_PAYOUT_FLOAT_20P
        {
            get
            {
                return this._MH_PAYOUT_FLOAT_20P;
            }
            set
            {
                if ((this._MH_PAYOUT_FLOAT_20P != value))
                {
                    this._MH_PAYOUT_FLOAT_20P = value;
                }
            }
        }

        [Column(Storage = "_MH_PAYOUT_FLOAT_50P", DbType = "Int")]
        public System.Nullable<int> MH_PAYOUT_FLOAT_50P
        {
            get
            {
                return this._MH_PAYOUT_FLOAT_50P;
            }
            set
            {
                if ((this._MH_PAYOUT_FLOAT_50P != value))
                {
                    this._MH_PAYOUT_FLOAT_50P = value;
                }
            }
        }

        [Column(Storage = "_MH_PAYOUT_FLOAT_100P", DbType = "Int")]
        public System.Nullable<int> MH_PAYOUT_FLOAT_100P
        {
            get
            {
                return this._MH_PAYOUT_FLOAT_100P;
            }
            set
            {
                if ((this._MH_PAYOUT_FLOAT_100P != value))
                {
                    this._MH_PAYOUT_FLOAT_100P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_IN_2P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_IN_2P
        {
            get
            {
                return this._MH_CASH_IN_2P;
            }
            set
            {
                if ((this._MH_CASH_IN_2P != value))
                {
                    this._MH_CASH_IN_2P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_IN_5P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_IN_5P
        {
            get
            {
                return this._MH_CASH_IN_5P;
            }
            set
            {
                if ((this._MH_CASH_IN_5P != value))
                {
                    this._MH_CASH_IN_5P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_IN_10P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_IN_10P
        {
            get
            {
                return this._MH_CASH_IN_10P;
            }
            set
            {
                if ((this._MH_CASH_IN_10P != value))
                {
                    this._MH_CASH_IN_10P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_IN_20P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_IN_20P
        {
            get
            {
                return this._MH_CASH_IN_20P;
            }
            set
            {
                if ((this._MH_CASH_IN_20P != value))
                {
                    this._MH_CASH_IN_20P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_IN_50P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_IN_50P
        {
            get
            {
                return this._MH_CASH_IN_50P;
            }
            set
            {
                if ((this._MH_CASH_IN_50P != value))
                {
                    this._MH_CASH_IN_50P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_IN_100P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_IN_100P
        {
            get
            {
                return this._MH_CASH_IN_100P;
            }
            set
            {
                if ((this._MH_CASH_IN_100P != value))
                {
                    this._MH_CASH_IN_100P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_IN_200P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_IN_200P
        {
            get
            {
                return this._MH_CASH_IN_200P;
            }
            set
            {
                if ((this._MH_CASH_IN_200P != value))
                {
                    this._MH_CASH_IN_200P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_IN_500P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_IN_500P
        {
            get
            {
                return this._MH_CASH_IN_500P;
            }
            set
            {
                if ((this._MH_CASH_IN_500P != value))
                {
                    this._MH_CASH_IN_500P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_IN_1000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_IN_1000P
        {
            get
            {
                return this._MH_CASH_IN_1000P;
            }
            set
            {
                if ((this._MH_CASH_IN_1000P != value))
                {
                    this._MH_CASH_IN_1000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_IN_2000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_IN_2000P
        {
            get
            {
                return this._MH_CASH_IN_2000P;
            }
            set
            {
                if ((this._MH_CASH_IN_2000P != value))
                {
                    this._MH_CASH_IN_2000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_IN_5000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_IN_5000P
        {
            get
            {
                return this._MH_CASH_IN_5000P;
            }
            set
            {
                if ((this._MH_CASH_IN_5000P != value))
                {
                    this._MH_CASH_IN_5000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_IN_10000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_IN_10000P
        {
            get
            {
                return this._MH_CASH_IN_10000P;
            }
            set
            {
                if ((this._MH_CASH_IN_10000P != value))
                {
                    this._MH_CASH_IN_10000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_IN_20000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_IN_20000P
        {
            get
            {
                return this._MH_CASH_IN_20000P;
            }
            set
            {
                if ((this._MH_CASH_IN_20000P != value))
                {
                    this._MH_CASH_IN_20000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_IN_50000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_IN_50000P
        {
            get
            {
                return this._MH_CASH_IN_50000P;
            }
            set
            {
                if ((this._MH_CASH_IN_50000P != value))
                {
                    this._MH_CASH_IN_50000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_IN_100000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_IN_100000P
        {
            get
            {
                return this._MH_CASH_IN_100000P;
            }
            set
            {
                if ((this._MH_CASH_IN_100000P != value))
                {
                    this._MH_CASH_IN_100000P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_IN_5P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_IN_5P
        {
            get
            {
                return this._MH_TOKEN_IN_5P;
            }
            set
            {
                if ((this._MH_TOKEN_IN_5P != value))
                {
                    this._MH_TOKEN_IN_5P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_IN_10P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_IN_10P
        {
            get
            {
                return this._MH_TOKEN_IN_10P;
            }
            set
            {
                if ((this._MH_TOKEN_IN_10P != value))
                {
                    this._MH_TOKEN_IN_10P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_IN_20P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_IN_20P
        {
            get
            {
                return this._MH_TOKEN_IN_20P;
            }
            set
            {
                if ((this._MH_TOKEN_IN_20P != value))
                {
                    this._MH_TOKEN_IN_20P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_IN_50P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_IN_50P
        {
            get
            {
                return this._MH_TOKEN_IN_50P;
            }
            set
            {
                if ((this._MH_TOKEN_IN_50P != value))
                {
                    this._MH_TOKEN_IN_50P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_IN_100P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_IN_100P
        {
            get
            {
                return this._MH_TOKEN_IN_100P;
            }
            set
            {
                if ((this._MH_TOKEN_IN_100P != value))
                {
                    this._MH_TOKEN_IN_100P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_IN_200P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_IN_200P
        {
            get
            {
                return this._MH_TOKEN_IN_200P;
            }
            set
            {
                if ((this._MH_TOKEN_IN_200P != value))
                {
                    this._MH_TOKEN_IN_200P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_IN_500P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_IN_500P
        {
            get
            {
                return this._MH_TOKEN_IN_500P;
            }
            set
            {
                if ((this._MH_TOKEN_IN_500P != value))
                {
                    this._MH_TOKEN_IN_500P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_IN_1000P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_IN_1000P
        {
            get
            {
                return this._MH_TOKEN_IN_1000P;
            }
            set
            {
                if ((this._MH_TOKEN_IN_1000P != value))
                {
                    this._MH_TOKEN_IN_1000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_OUT_2P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_OUT_2P
        {
            get
            {
                return this._MH_CASH_OUT_2P;
            }
            set
            {
                if ((this._MH_CASH_OUT_2P != value))
                {
                    this._MH_CASH_OUT_2P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_OUT_5P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_OUT_5P
        {
            get
            {
                return this._MH_CASH_OUT_5P;
            }
            set
            {
                if ((this._MH_CASH_OUT_5P != value))
                {
                    this._MH_CASH_OUT_5P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_OUT_10P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_OUT_10P
        {
            get
            {
                return this._MH_CASH_OUT_10P;
            }
            set
            {
                if ((this._MH_CASH_OUT_10P != value))
                {
                    this._MH_CASH_OUT_10P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_OUT_20P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_OUT_20P
        {
            get
            {
                return this._MH_CASH_OUT_20P;
            }
            set
            {
                if ((this._MH_CASH_OUT_20P != value))
                {
                    this._MH_CASH_OUT_20P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_OUT_50P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_OUT_50P
        {
            get
            {
                return this._MH_CASH_OUT_50P;
            }
            set
            {
                if ((this._MH_CASH_OUT_50P != value))
                {
                    this._MH_CASH_OUT_50P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_OUT_100P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_OUT_100P
        {
            get
            {
                return this._MH_CASH_OUT_100P;
            }
            set
            {
                if ((this._MH_CASH_OUT_100P != value))
                {
                    this._MH_CASH_OUT_100P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_OUT_200P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_OUT_200P
        {
            get
            {
                return this._MH_CASH_OUT_200P;
            }
            set
            {
                if ((this._MH_CASH_OUT_200P != value))
                {
                    this._MH_CASH_OUT_200P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_OUT_500P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_OUT_500P
        {
            get
            {
                return this._MH_CASH_OUT_500P;
            }
            set
            {
                if ((this._MH_CASH_OUT_500P != value))
                {
                    this._MH_CASH_OUT_500P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_OUT_1000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_OUT_1000P
        {
            get
            {
                return this._MH_CASH_OUT_1000P;
            }
            set
            {
                if ((this._MH_CASH_OUT_1000P != value))
                {
                    this._MH_CASH_OUT_1000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_OUT_2000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_OUT_2000P
        {
            get
            {
                return this._MH_CASH_OUT_2000P;
            }
            set
            {
                if ((this._MH_CASH_OUT_2000P != value))
                {
                    this._MH_CASH_OUT_2000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_OUT_5000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_OUT_5000P
        {
            get
            {
                return this._MH_CASH_OUT_5000P;
            }
            set
            {
                if ((this._MH_CASH_OUT_5000P != value))
                {
                    this._MH_CASH_OUT_5000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_OUT_10000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_OUT_10000P
        {
            get
            {
                return this._MH_CASH_OUT_10000P;
            }
            set
            {
                if ((this._MH_CASH_OUT_10000P != value))
                {
                    this._MH_CASH_OUT_10000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_OUT_20000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_OUT_20000P
        {
            get
            {
                return this._MH_CASH_OUT_20000P;
            }
            set
            {
                if ((this._MH_CASH_OUT_20000P != value))
                {
                    this._MH_CASH_OUT_20000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_OUT_50000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_OUT_50000P
        {
            get
            {
                return this._MH_CASH_OUT_50000P;
            }
            set
            {
                if ((this._MH_CASH_OUT_50000P != value))
                {
                    this._MH_CASH_OUT_50000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_OUT_100000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_OUT_100000P
        {
            get
            {
                return this._MH_CASH_OUT_100000P;
            }
            set
            {
                if ((this._MH_CASH_OUT_100000P != value))
                {
                    this._MH_CASH_OUT_100000P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_OUT_5P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_OUT_5P
        {
            get
            {
                return this._MH_TOKEN_OUT_5P;
            }
            set
            {
                if ((this._MH_TOKEN_OUT_5P != value))
                {
                    this._MH_TOKEN_OUT_5P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_OUT_10P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_OUT_10P
        {
            get
            {
                return this._MH_TOKEN_OUT_10P;
            }
            set
            {
                if ((this._MH_TOKEN_OUT_10P != value))
                {
                    this._MH_TOKEN_OUT_10P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_OUT_20P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_OUT_20P
        {
            get
            {
                return this._MH_TOKEN_OUT_20P;
            }
            set
            {
                if ((this._MH_TOKEN_OUT_20P != value))
                {
                    this._MH_TOKEN_OUT_20P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_OUT_50P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_OUT_50P
        {
            get
            {
                return this._MH_TOKEN_OUT_50P;
            }
            set
            {
                if ((this._MH_TOKEN_OUT_50P != value))
                {
                    this._MH_TOKEN_OUT_50P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_OUT_100P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_OUT_100P
        {
            get
            {
                return this._MH_TOKEN_OUT_100P;
            }
            set
            {
                if ((this._MH_TOKEN_OUT_100P != value))
                {
                    this._MH_TOKEN_OUT_100P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_OUT_200P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_OUT_200P
        {
            get
            {
                return this._MH_TOKEN_OUT_200P;
            }
            set
            {
                if ((this._MH_TOKEN_OUT_200P != value))
                {
                    this._MH_TOKEN_OUT_200P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_OUT_500P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_OUT_500P
        {
            get
            {
                return this._MH_TOKEN_OUT_500P;
            }
            set
            {
                if ((this._MH_TOKEN_OUT_500P != value))
                {
                    this._MH_TOKEN_OUT_500P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_OUT_1000P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_OUT_1000P
        {
            get
            {
                return this._MH_TOKEN_OUT_1000P;
            }
            set
            {
                if ((this._MH_TOKEN_OUT_1000P != value))
                {
                    this._MH_TOKEN_OUT_1000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_REFILL_5P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_REFILL_5P
        {
            get
            {
                return this._MH_CASH_REFILL_5P;
            }
            set
            {
                if ((this._MH_CASH_REFILL_5P != value))
                {
                    this._MH_CASH_REFILL_5P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_REFILL_10P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_REFILL_10P
        {
            get
            {
                return this._MH_CASH_REFILL_10P;
            }
            set
            {
                if ((this._MH_CASH_REFILL_10P != value))
                {
                    this._MH_CASH_REFILL_10P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_REFILL_20P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_REFILL_20P
        {
            get
            {
                return this._MH_CASH_REFILL_20P;
            }
            set
            {
                if ((this._MH_CASH_REFILL_20P != value))
                {
                    this._MH_CASH_REFILL_20P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_REFILL_50P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_REFILL_50P
        {
            get
            {
                return this._MH_CASH_REFILL_50P;
            }
            set
            {
                if ((this._MH_CASH_REFILL_50P != value))
                {
                    this._MH_CASH_REFILL_50P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_REFILL_100P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_REFILL_100P
        {
            get
            {
                return this._MH_CASH_REFILL_100P;
            }
            set
            {
                if ((this._MH_CASH_REFILL_100P != value))
                {
                    this._MH_CASH_REFILL_100P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_REFILL_200P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_REFILL_200P
        {
            get
            {
                return this._MH_CASH_REFILL_200P;
            }
            set
            {
                if ((this._MH_CASH_REFILL_200P != value))
                {
                    this._MH_CASH_REFILL_200P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_REFILL_500P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_REFILL_500P
        {
            get
            {
                return this._MH_CASH_REFILL_500P;
            }
            set
            {
                if ((this._MH_CASH_REFILL_500P != value))
                {
                    this._MH_CASH_REFILL_500P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_REFILL_1000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_REFILL_1000P
        {
            get
            {
                return this._MH_CASH_REFILL_1000P;
            }
            set
            {
                if ((this._MH_CASH_REFILL_1000P != value))
                {
                    this._MH_CASH_REFILL_1000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_REFILL_2000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_REFILL_2000P
        {
            get
            {
                return this._MH_CASH_REFILL_2000P;
            }
            set
            {
                if ((this._MH_CASH_REFILL_2000P != value))
                {
                    this._MH_CASH_REFILL_2000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_REFILL_5000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_REFILL_5000P
        {
            get
            {
                return this._MH_CASH_REFILL_5000P;
            }
            set
            {
                if ((this._MH_CASH_REFILL_5000P != value))
                {
                    this._MH_CASH_REFILL_5000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_REFILL_10000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_REFILL_10000P
        {
            get
            {
                return this._MH_CASH_REFILL_10000P;
            }
            set
            {
                if ((this._MH_CASH_REFILL_10000P != value))
                {
                    this._MH_CASH_REFILL_10000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_REFILL_20000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_REFILL_20000P
        {
            get
            {
                return this._MH_CASH_REFILL_20000P;
            }
            set
            {
                if ((this._MH_CASH_REFILL_20000P != value))
                {
                    this._MH_CASH_REFILL_20000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_REFILL_50000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_REFILL_50000P
        {
            get
            {
                return this._MH_CASH_REFILL_50000P;
            }
            set
            {
                if ((this._MH_CASH_REFILL_50000P != value))
                {
                    this._MH_CASH_REFILL_50000P = value;
                }
            }
        }

        [Column(Storage = "_MH_CASH_REFILL_100000P", DbType = "Int")]
        public System.Nullable<int> MH_CASH_REFILL_100000P
        {
            get
            {
                return this._MH_CASH_REFILL_100000P;
            }
            set
            {
                if ((this._MH_CASH_REFILL_100000P != value))
                {
                    this._MH_CASH_REFILL_100000P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_REFILL_5P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_REFILL_5P
        {
            get
            {
                return this._MH_TOKEN_REFILL_5P;
            }
            set
            {
                if ((this._MH_TOKEN_REFILL_5P != value))
                {
                    this._MH_TOKEN_REFILL_5P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_REFILL_10P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_REFILL_10P
        {
            get
            {
                return this._MH_TOKEN_REFILL_10P;
            }
            set
            {
                if ((this._MH_TOKEN_REFILL_10P != value))
                {
                    this._MH_TOKEN_REFILL_10P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_REFILL_20P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_REFILL_20P
        {
            get
            {
                return this._MH_TOKEN_REFILL_20P;
            }
            set
            {
                if ((this._MH_TOKEN_REFILL_20P != value))
                {
                    this._MH_TOKEN_REFILL_20P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_REFILL_50P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_REFILL_50P
        {
            get
            {
                return this._MH_TOKEN_REFILL_50P;
            }
            set
            {
                if ((this._MH_TOKEN_REFILL_50P != value))
                {
                    this._MH_TOKEN_REFILL_50P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_REFILL_100P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_REFILL_100P
        {
            get
            {
                return this._MH_TOKEN_REFILL_100P;
            }
            set
            {
                if ((this._MH_TOKEN_REFILL_100P != value))
                {
                    this._MH_TOKEN_REFILL_100P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_REFILL_200P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_REFILL_200P
        {
            get
            {
                return this._MH_TOKEN_REFILL_200P;
            }
            set
            {
                if ((this._MH_TOKEN_REFILL_200P != value))
                {
                    this._MH_TOKEN_REFILL_200P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_REFILL_500P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_REFILL_500P
        {
            get
            {
                return this._MH_TOKEN_REFILL_500P;
            }
            set
            {
                if ((this._MH_TOKEN_REFILL_500P != value))
                {
                    this._MH_TOKEN_REFILL_500P = value;
                }
            }
        }

        [Column(Storage = "_MH_TOKEN_REFILL_1000P", DbType = "Int")]
        public System.Nullable<int> MH_TOKEN_REFILL_1000P
        {
            get
            {
                return this._MH_TOKEN_REFILL_1000P;
            }
            set
            {
                if ((this._MH_TOKEN_REFILL_1000P != value))
                {
                    this._MH_TOKEN_REFILL_1000P = value;
                }
            }
        }

        [Column(Storage = "_MH_COINS_IN", DbType = "Int")]
        public System.Nullable<int> MH_COINS_IN
        {
            get
            {
                return this._MH_COINS_IN;
            }
            set
            {
                if ((this._MH_COINS_IN != value))
                {
                    this._MH_COINS_IN = value;
                }
            }
        }

        [Column(Storage = "_MH_COINS_OUT", DbType = "Int")]
        public System.Nullable<int> MH_COINS_OUT
        {
            get
            {
                return this._MH_COINS_OUT;
            }
            set
            {
                if ((this._MH_COINS_OUT != value))
                {
                    this._MH_COINS_OUT = value;
                }
            }
        }

        [Column(Storage = "_MH_COIN_DROP", DbType = "Int")]
        public System.Nullable<int> MH_COIN_DROP
        {
            get
            {
                return this._MH_COIN_DROP;
            }
            set
            {
                if ((this._MH_COIN_DROP != value))
                {
                    this._MH_COIN_DROP = value;
                }
            }
        }

        [Column(Storage = "_MH_HANDPAY", DbType = "Int")]
        public System.Nullable<int> MH_HANDPAY
        {
            get
            {
                return this._MH_HANDPAY;
            }
            set
            {
                if ((this._MH_HANDPAY != value))
                {
                    this._MH_HANDPAY = value;
                }
            }
        }

        [Column(Storage = "_MH_EXTERNAL_CREDIT", DbType = "Int")]
        public System.Nullable<int> MH_EXTERNAL_CREDIT
        {
            get
            {
                return this._MH_EXTERNAL_CREDIT;
            }
            set
            {
                if ((this._MH_EXTERNAL_CREDIT != value))
                {
                    this._MH_EXTERNAL_CREDIT = value;
                }
            }
        }

        [Column(Storage = "_MH_GAMES_BET", DbType = "Int")]
        public System.Nullable<int> MH_GAMES_BET
        {
            get
            {
                return this._MH_GAMES_BET;
            }
            set
            {
                if ((this._MH_GAMES_BET != value))
                {
                    this._MH_GAMES_BET = value;
                }
            }
        }

        [Column(Storage = "_MH_GAMES_WON", DbType = "Int")]
        public System.Nullable<int> MH_GAMES_WON
        {
            get
            {
                return this._MH_GAMES_WON;
            }
            set
            {
                if ((this._MH_GAMES_WON != value))
                {
                    this._MH_GAMES_WON = value;
                }
            }
        }

        [Column(Storage = "_MH_NOTES", DbType = "Int")]
        public System.Nullable<int> MH_NOTES
        {
            get
            {
                return this._MH_NOTES;
            }
            set
            {
                if ((this._MH_NOTES != value))
                {
                    this._MH_NOTES = value;
                }
            }
        }

        [Column(Storage = "_MH_VTP", DbType = "Int")]
        public System.Nullable<int> MH_VTP
        {
            get
            {
                return this._MH_VTP;
            }
            set
            {
                if ((this._MH_VTP != value))
                {
                    this._MH_VTP = value;
                }
            }
        }

        [Column(Storage = "_MH_CANCELLED_CREDITS", DbType = "Int")]
        public System.Nullable<int> MH_CANCELLED_CREDITS
        {
            get
            {
                return this._MH_CANCELLED_CREDITS;
            }
            set
            {
                if ((this._MH_CANCELLED_CREDITS != value))
                {
                    this._MH_CANCELLED_CREDITS = value;
                }
            }
        }

        [Column(Storage = "_MH_GAMES_LOST", DbType = "Int")]
        public System.Nullable<int> MH_GAMES_LOST
        {
            get
            {
                return this._MH_GAMES_LOST;
            }
            set
            {
                if ((this._MH_GAMES_LOST != value))
                {
                    this._MH_GAMES_LOST = value;
                }
            }
        }

        [Column(Storage = "_MH_GAMES_SINCE_POWER_UP", DbType = "Int")]
        public System.Nullable<int> MH_GAMES_SINCE_POWER_UP
        {
            get
            {
                return this._MH_GAMES_SINCE_POWER_UP;
            }
            set
            {
                if ((this._MH_GAMES_SINCE_POWER_UP != value))
                {
                    this._MH_GAMES_SINCE_POWER_UP = value;
                }
            }
        }

        [Column(Storage = "_MH_TRUE_COIN_IN", DbType = "Int")]
        public System.Nullable<int> MH_TRUE_COIN_IN
        {
            get
            {
                return this._MH_TRUE_COIN_IN;
            }
            set
            {
                if ((this._MH_TRUE_COIN_IN != value))
                {
                    this._MH_TRUE_COIN_IN = value;
                }
            }
        }

        [Column(Storage = "_MH_TRUE_COIN_OUT", DbType = "Int")]
        public System.Nullable<int> MH_TRUE_COIN_OUT
        {
            get
            {
                return this._MH_TRUE_COIN_OUT;
            }
            set
            {
                if ((this._MH_TRUE_COIN_OUT != value))
                {
                    this._MH_TRUE_COIN_OUT = value;
                }
            }
        }

        [Column(Storage = "_MH_CURRENT_CREDITS", DbType = "Int")]
        public System.Nullable<int> MH_CURRENT_CREDITS
        {
            get
            {
                return this._MH_CURRENT_CREDITS;
            }
            set
            {
                if ((this._MH_CURRENT_CREDITS != value))
                {
                    this._MH_CURRENT_CREDITS = value;
                }
            }
        }

        [Column(Storage = "_MH_JACKPOT", DbType = "Int")]
        public System.Nullable<int> MH_JACKPOT
        {
            get
            {
                return this._MH_JACKPOT;
            }
            set
            {
                if ((this._MH_JACKPOT != value))
                {
                    this._MH_JACKPOT = value;
                }
            }
        }

        [Column(Storage = "_MH_BILL_1", DbType = "Int")]
        public System.Nullable<int> MH_BILL_1
        {
            get
            {
                return this._MH_BILL_1;
            }
            set
            {
                if ((this._MH_BILL_1 != value))
                {
                    this._MH_BILL_1 = value;
                }
            }
        }

        [Column(Storage = "_MH_BILL_2", DbType = "Int")]
        public System.Nullable<int> MH_BILL_2
        {
            get
            {
                return this._MH_BILL_2;
            }
            set
            {
                if ((this._MH_BILL_2 != value))
                {
                    this._MH_BILL_2 = value;
                }
            }
        }

        [Column(Storage = "_MH_BILL_5", DbType = "Int")]
        public System.Nullable<int> MH_BILL_5
        {
            get
            {
                return this._MH_BILL_5;
            }
            set
            {
                if ((this._MH_BILL_5 != value))
                {
                    this._MH_BILL_5 = value;
                }
            }
        }

        [Column(Storage = "_MH_BILL_10", DbType = "Int")]
        public System.Nullable<int> MH_BILL_10
        {
            get
            {
                return this._MH_BILL_10;
            }
            set
            {
                if ((this._MH_BILL_10 != value))
                {
                    this._MH_BILL_10 = value;
                }
            }
        }

        [Column(Storage = "_MH_BILL_20", DbType = "Int")]
        public System.Nullable<int> MH_BILL_20
        {
            get
            {
                return this._MH_BILL_20;
            }
            set
            {
                if ((this._MH_BILL_20 != value))
                {
                    this._MH_BILL_20 = value;
                }
            }
        }

        [Column(Storage = "_MH_BILL_50", DbType = "Int")]
        public System.Nullable<int> MH_BILL_50
        {
            get
            {
                return this._MH_BILL_50;
            }
            set
            {
                if ((this._MH_BILL_50 != value))
                {
                    this._MH_BILL_50 = value;
                }
            }
        }

        [Column(Storage = "_MH_BILL_100", DbType = "Int")]
        public System.Nullable<int> MH_BILL_100
        {
            get
            {
                return this._MH_BILL_100;
            }
            set
            {
                if ((this._MH_BILL_100 != value))
                {
                    this._MH_BILL_100 = value;
                }
            }
        }

        [Column(Storage = "_MH_BILL_250", DbType = "Int")]
        public System.Nullable<int> MH_BILL_250
        {
            get
            {
                return this._MH_BILL_250;
            }
            set
            {
                if ((this._MH_BILL_250 != value))
                {
                    this._MH_BILL_250 = value;
                }
            }
        }

        [Column(Storage = "_MH_BILL_10000", DbType = "Int")]
        public System.Nullable<int> MH_BILL_10000
        {
            get
            {
                return this._MH_BILL_10000;
            }
            set
            {
                if ((this._MH_BILL_10000 != value))
                {
                    this._MH_BILL_10000 = value;
                }
            }
        }

        [Column(Storage = "_MH_BILL_20000", DbType = "Int")]
        public System.Nullable<int> MH_BILL_20000
        {
            get
            {
                return this._MH_BILL_20000;
            }
            set
            {
                if ((this._MH_BILL_20000 != value))
                {
                    this._MH_BILL_20000 = value;
                }
            }
        }

        [Column(Storage = "_MH_BILL_50000", DbType = "Int")]
        public System.Nullable<int> MH_BILL_50000
        {
            get
            {
                return this._MH_BILL_50000;
            }
            set
            {
                if ((this._MH_BILL_50000 != value))
                {
                    this._MH_BILL_50000 = value;
                }
            }
        }

        [Column(Storage = "_MH_BILL_100000", DbType = "Int")]
        public System.Nullable<int> MH_BILL_100000
        {
            get
            {
                return this._MH_BILL_100000;
            }
            set
            {
                if ((this._MH_BILL_100000 != value))
                {
                    this._MH_BILL_100000 = value;
                }
            }
        }

        [Column(Storage = "_MH_TICKET_PRINTED_QTY", DbType = "Int")]
        public System.Nullable<int> MH_TICKET_PRINTED_QTY
        {
            get
            {
                return this._MH_TICKET_PRINTED_QTY;
            }
            set
            {
                if ((this._MH_TICKET_PRINTED_QTY != value))
                {
                    this._MH_TICKET_PRINTED_QTY = value;
                }
            }
        }

        [Column(Storage = "_MH_TICKET_PRINTED_VALUE", DbType = "Int")]
        public System.Nullable<int> MH_TICKET_PRINTED_VALUE
        {
            get
            {
                return this._MH_TICKET_PRINTED_VALUE;
            }
            set
            {
                if ((this._MH_TICKET_PRINTED_VALUE != value))
                {
                    this._MH_TICKET_PRINTED_VALUE = value;
                }
            }
        }

        [Column(Storage = "_MH_TICKET_INSERTED_QTY", DbType = "Int")]
        public System.Nullable<int> MH_TICKET_INSERTED_QTY
        {
            get
            {
                return this._MH_TICKET_INSERTED_QTY;
            }
            set
            {
                if ((this._MH_TICKET_INSERTED_QTY != value))
                {
                    this._MH_TICKET_INSERTED_QTY = value;
                }
            }
        }

        [Column(Storage = "_MH_TICKET_INSERTED_VALUE", DbType = "Int")]
        public System.Nullable<int> MH_TICKET_INSERTED_VALUE
        {
            get
            {
                return this._MH_TICKET_INSERTED_VALUE;
            }
            set
            {
                if ((this._MH_TICKET_INSERTED_VALUE != value))
                {
                    this._MH_TICKET_INSERTED_VALUE = value;
                }
            }
        }

        [Column(Storage = "_MH_Datetime", DbType = "VarChar(30)")]
        public string MH_Datetime
        {
            get
            {
                return this._MH_Datetime;
            }
            set
            {
                if ((this._MH_Datetime != value))
                {
                    this._MH_Datetime = value;
                }
            }
        }

        [Column(Storage = "_MH_progressive_win_value", DbType = "Int")]
        public System.Nullable<int> MH_progressive_win_value
        {
            get
            {
                return this._MH_progressive_win_value;
            }
            set
            {
                if ((this._MH_progressive_win_value != value))
                {
                    this._MH_progressive_win_value = value;
                }
            }
        }

        [Column(Storage = "_MH_progressive_win_Handpay_value", DbType = "Int")]
        public System.Nullable<int> MH_progressive_win_Handpay_value
        {
            get
            {
                return this._MH_progressive_win_Handpay_value;
            }
            set
            {
                if ((this._MH_progressive_win_Handpay_value != value))
                {
                    this._MH_progressive_win_Handpay_value = value;
                }
            }
        }

        [Column(Storage = "_MH_Mystery_Machine_Paid", DbType = "Int")]
        public System.Nullable<int> MH_Mystery_Machine_Paid
        {
            get
            {
                return this._MH_Mystery_Machine_Paid;
            }
            set
            {
                if ((this._MH_Mystery_Machine_Paid != value))
                {
                    this._MH_Mystery_Machine_Paid = value;
                }
            }
        }

        [Column(Storage = "_MH_Mystery_Attendant_Paid", DbType = "Int")]
        public System.Nullable<int> MH_Mystery_Attendant_Paid
        {
            get
            {
                return this._MH_Mystery_Attendant_Paid;
            }
            set
            {
                if ((this._MH_Mystery_Attendant_Paid != value))
                {
                    this._MH_Mystery_Attendant_Paid = value;
                }
            }
        }

        [Column(Storage = "_MH_TICKETS_PRINTED_NONCASHABLE_QTY", DbType = "Int")]
        public System.Nullable<int> MH_TICKETS_PRINTED_NONCASHABLE_QTY
        {
            get
            {
                return this._MH_TICKETS_PRINTED_NONCASHABLE_QTY;
            }
            set
            {
                if ((this._MH_TICKETS_PRINTED_NONCASHABLE_QTY != value))
                {
                    this._MH_TICKETS_PRINTED_NONCASHABLE_QTY = value;
                }
            }
        }

        [Column(Storage = "_MH_TICKETS_PRINTED_NONCASHABLE_VALUE", DbType = "Int")]
        public System.Nullable<int> MH_TICKETS_PRINTED_NONCASHABLE_VALUE
        {
            get
            {
                return this._MH_TICKETS_PRINTED_NONCASHABLE_VALUE;
            }
            set
            {
                if ((this._MH_TICKETS_PRINTED_NONCASHABLE_VALUE != value))
                {
                    this._MH_TICKETS_PRINTED_NONCASHABLE_VALUE = value;
                }
            }
        }

        [Column(Storage = "_MH_TICKETS_INSERTED_NONCASHABLE_QTY", DbType = "Int")]
        public System.Nullable<int> MH_TICKETS_INSERTED_NONCASHABLE_QTY
        {
            get
            {
                return this._MH_TICKETS_INSERTED_NONCASHABLE_QTY;
            }
            set
            {
                if ((this._MH_TICKETS_INSERTED_NONCASHABLE_QTY != value))
                {
                    this._MH_TICKETS_INSERTED_NONCASHABLE_QTY = value;
                }
            }
        }

        [Column(Storage = "_MH_TICKETS_INSERTED_NONCASHABLE_VALUE", DbType = "Int")]
        public System.Nullable<int> MH_TICKETS_INSERTED_NONCASHABLE_VALUE
        {
            get
            {
                return this._MH_TICKETS_INSERTED_NONCASHABLE_VALUE;
            }
            set
            {
                if ((this._MH_TICKETS_INSERTED_NONCASHABLE_VALUE != value))
                {
                    this._MH_TICKETS_INSERTED_NONCASHABLE_VALUE = value;
                }
            }
        }

        [Column(Storage = "_MH_Promo_Cashable_EFT_IN", DbType = "Int")]
        public System.Nullable<int> MH_Promo_Cashable_EFT_IN
        {
            get
            {
                return this._MH_Promo_Cashable_EFT_IN;
            }
            set
            {
                if ((this._MH_Promo_Cashable_EFT_IN != value))
                {
                    this._MH_Promo_Cashable_EFT_IN = value;
                }
            }
        }

        [Column(Storage = "_MH_Promo_Cashable_EFT_OUT", DbType = "Int")]
        public System.Nullable<int> MH_Promo_Cashable_EFT_OUT
        {
            get
            {
                return this._MH_Promo_Cashable_EFT_OUT;
            }
            set
            {
                if ((this._MH_Promo_Cashable_EFT_OUT != value))
                {
                    this._MH_Promo_Cashable_EFT_OUT = value;
                }
            }
        }

        [Column(Storage = "_MH_NonCashable_EFT_IN", DbType = "Int")]
        public System.Nullable<int> MH_NonCashable_EFT_IN
        {
            get
            {
                return this._MH_NonCashable_EFT_IN;
            }
            set
            {
                if ((this._MH_NonCashable_EFT_IN != value))
                {
                    this._MH_NonCashable_EFT_IN = value;
                }
            }
        }

        [Column(Storage = "_MH_NonCashable_EFT_OUT", DbType = "Int")]
        public System.Nullable<int> MH_NonCashable_EFT_OUT
        {
            get
            {
                return this._MH_NonCashable_EFT_OUT;
            }
            set
            {
                if ((this._MH_NonCashable_EFT_OUT != value))
                {
                    this._MH_NonCashable_EFT_OUT = value;
                }
            }
        }

        [Column(Storage = "_MH_Cashable_EFT_IN", DbType = "Int")]
        public System.Nullable<int> MH_Cashable_EFT_IN
        {
            get
            {
                return this._MH_Cashable_EFT_IN;
            }
            set
            {
                if ((this._MH_Cashable_EFT_IN != value))
                {
                    this._MH_Cashable_EFT_IN = value;
                }
            }
        }

        [Column(Storage = "_MH_Cashable_EFT_OUT", DbType = "Int")]
        public System.Nullable<int> MH_Cashable_EFT_OUT
        {
            get
            {
                return this._MH_Cashable_EFT_OUT;
            }
            set
            {
                if ((this._MH_Cashable_EFT_OUT != value))
                {
                    this._MH_Cashable_EFT_OUT = value;
                }
            }
        }

        [Column(Storage = "_MH_BILL_200", DbType = "Int")]
        public System.Nullable<int> MH_BILL_200
        {
            get
            {
                return this._MH_BILL_200;
            }
            set
            {
                if ((this._MH_BILL_200 != value))
                {
                    this._MH_BILL_200 = value;
                }
            }
        }

        [Column(Storage = "_MH_BILL_500", DbType = "Int")]
        public System.Nullable<int> MH_BILL_500
        {
            get
            {
                return this._MH_BILL_500;
            }
            set
            {
                if ((this._MH_BILL_500 != value))
                {
                    this._MH_BILL_500 = value;
                }
            }
        }
    }
}
