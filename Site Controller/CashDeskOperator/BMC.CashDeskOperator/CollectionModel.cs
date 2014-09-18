using System;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
using System.ComponentModel;
using BMC.Common.Utilities;

namespace BMC.Transport
{
    [DataContract()]
    internal partial class UndeclaredCollectionBatch
    {
		
		private System.Nullable<int> _Zone_No;
		
		private string _Zone_Name;
		
		private string _Bar_Pos_Name;

        private string _AssetNo;
		
		private string _Name;
		
		private System.Nullable<bool> _Machine_Uses_Meters;
		
		private int _Installation_No;

        private System.Nullable<int> _Installation_Token_Value;

        private System.Nullable<System.DateTime> _Date;
		
		private int _Collection_No;
		
		private System.Nullable<int> _Collection_Batch_No;
		
		private System.Nullable<bool> _Collection_Defloat_Collection;
		
		private System.Nullable<int> _CollectionHandHeldMetersReceived;
		
		private System.Nullable<float> _Cash_Collected_100000P;
		
		private System.Nullable<float> _Cash_Collected_50000P;
		
		private System.Nullable<float> _Cash_Collected_20000P;
		
		private System.Nullable<float> _Cash_Collected_10000P;
		
		private System.Nullable<float> _Cash_Collected_5000P;
		
		private System.Nullable<float> _Cash_Collected_2000p;
		
		private System.Nullable<float> _Cash_Collected_1000P;
		
		private System.Nullable<float> _Cash_Collected_500P;
		
		private System.Nullable<float> _Cash_Collected_200P;
		
		private System.Nullable<float> _Cash_Collected_100P;
		
		private System.Nullable<float> _Cash_Collected_50p;
		
		private System.Nullable<float> _Cash_Collected_20p;
		
		private System.Nullable<float> _Cash_Collected_10p;
		
		private System.Nullable<float> _Cash_Collected_5p;
		
		private System.Nullable<float> _Cash_Collected_2p;

        private System.Nullable<float> _Cash_Collected_1p;
		
		private decimal _TicketsIn;
		
		private decimal _TicketsOut;
		
		private decimal _ShortPay;

        private decimal _Handpay;
		
		private float _Refills;
		
		private float _Refunds;
		
		private decimal _HandpayJackpot;

        private decimal _DeclaredHandpay;

        private float _CoinIn;

        private float _CoinOut;

        private decimal _AttendantPay;

        private string _Collection_Batch_Name;

        public UndeclaredCollectionBatch()
		{
		}
		
		[Column(Storage="_Zone_No", DbType="Int")]
		public System.Nullable<int> Zone_No
		{
			get
			{
				return this._Zone_No;
			}
			set
			{
				if ((this._Zone_No != value))
				{
					this._Zone_No = value;
				}
			}
		}
		
		[Column(Storage="_Zone_Name", DbType="VarChar(50)")]
		public string Zone_Name
		{
			get
			{
				return this._Zone_Name;
			}
			set
			{
				if ((this._Zone_Name != value))
				{
					this._Zone_Name = value;
				}
			}
		}
		
		[Column(Storage="_Bar_Pos_Name", DbType="VarChar(50)")]
		public string Bar_Pos_Name
		{
			get
			{
				return this._Bar_Pos_Name;
			}
			set
			{
				if ((this._Bar_Pos_Name != value))
				{
					this._Bar_Pos_Name = value;
				}
			}
		}

        [Column(Storage = "_AssetNo", DbType = "VarChar(50)")]
        public string AssetNo
        {
            get
            {
                return this._AssetNo;
            }
            set
            {
                if ((this._AssetNo != value))
                {
                    this._AssetNo = value;
                }
            }
        }
		
		[Column(Storage="_Name", DbType="VarChar(50)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this._Name = value;
				}
			}
		}
		
		[Column(Storage="_Machine_Uses_Meters", DbType="Bit")]
		public System.Nullable<bool> Machine_Uses_Meters
		{
			get
			{
				return this._Machine_Uses_Meters;
			}
			set
			{
				if ((this._Machine_Uses_Meters != value))
				{
					this._Machine_Uses_Meters = value ?? false;
				}
			}
		}
		
		[Column(Storage="_Installation_No", DbType="Int NOT NULL")]
		public int Installation_No
		{
			get
			{
				return this._Installation_No;
			}
			set
			{
				if ((this._Installation_No != value))
				{
					this._Installation_No = value;
				}
			}
		}

        [Column(Storage = "_Installation_Token_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Token_Value
        {
            get
            {
                return this._Installation_Token_Value;
            }
            set
            {
                if ((this._Installation_Token_Value != value))
                {
                    this._Installation_Token_Value = value;
                }
            }
        }
		
		[Column(Storage="_Date", DbType="DateTime")]
		public System.Nullable<System.DateTime> Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this._Date = value ?? DateTime.Now;
				}
			}
		}
		
		[Column(Storage="_Collection_No", DbType="Int NOT NULL")]
		public int Collection_No
		{
			get
			{
				return this._Collection_No;
			}
			set
			{
				if ((this._Collection_No != value))
				{
					this._Collection_No = value;
				}
			}
		}
		
		[Column(Storage="_Collection_Batch_No", DbType="Int")]
		public System.Nullable<int> Collection_Batch_No
		{
			get
			{
				return this._Collection_Batch_No;
			}
			set
			{
				if ((this._Collection_Batch_No != value))
				{
					this._Collection_Batch_No = value ?? 0;
				}
			}
		}
		
		[Column(Storage="_Collection_Defloat_Collection", DbType="Bit")]
		public System.Nullable<bool> Collection_Defloat_Collection
		{
			get
			{
				return this._Collection_Defloat_Collection;
			}
			set
			{
				if ((this._Collection_Defloat_Collection != value))
				{
					this._Collection_Defloat_Collection = value ?? false;
				}
			}
		}
		
		[Column(Storage="_CollectionHandHeldMetersReceived", DbType="Int")]
		public System.Nullable<int> CollectionHandHeldMetersReceived
		{
			get
			{
				return this._CollectionHandHeldMetersReceived;
			}
			set
			{
				if ((this._CollectionHandHeldMetersReceived != value))
				{
					this._CollectionHandHeldMetersReceived = value ?? 0;
				}
			}
		}
		
		[Column(Storage="_Cash_Collected_100000P", DbType="Real")]
		public System.Nullable<float> Cash_Collected_100000P
		{
			get
			{
				return this._Cash_Collected_100000P;
			}
			set
			{
				if ((this._Cash_Collected_100000P != value))
				{
					this._Cash_Collected_100000P = value ?? 0;
				}
			}
		}
		
		[Column(Storage="_Cash_Collected_50000P", DbType="Real")]
		public System.Nullable<float> Cash_Collected_50000P
		{
			get
			{
				return this._Cash_Collected_50000P;
			}
			set
			{
				if ((this._Cash_Collected_50000P != value))
				{
					this._Cash_Collected_50000P = value ?? 0;
				}
			}
		}
		
		[Column(Storage="_Cash_Collected_20000P", DbType="Real")]
		public System.Nullable<float> Cash_Collected_20000P
		{
			get
			{
				return this._Cash_Collected_20000P;
			}
			set
			{
				if ((this._Cash_Collected_20000P != value))
				{
					this._Cash_Collected_20000P = value ?? 0;
				}
			}
		}
		
		[Column(Storage="_Cash_Collected_10000P", DbType="Real")]
		public System.Nullable<float> Cash_Collected_10000P
		{
			get
			{
				return this._Cash_Collected_10000P;
			}
			set
			{
				if ((this._Cash_Collected_10000P != value))
				{
					this._Cash_Collected_10000P = value ?? 0;
				}
			}
		}
		
		[Column(Storage="_Cash_Collected_5000P", DbType="Real")]
		public System.Nullable<float> Cash_Collected_5000P
		{
			get
			{
				return this._Cash_Collected_5000P;
			}
			set
			{
				if ((this._Cash_Collected_5000P != value))
				{
					this._Cash_Collected_5000P = value ?? 0;
				}
			}
		}
		
		[Column(Storage="_Cash_Collected_2000p", DbType="Real")]
		public System.Nullable<float> Cash_Collected_2000p
		{
			get
			{
				return this._Cash_Collected_2000p;
			}
			set
			{
				if ((this._Cash_Collected_2000p != value))
				{
					this._Cash_Collected_2000p = value ?? 0;
				}
			}
		}
		
		[Column(Storage="_Cash_Collected_1000P", DbType="Real")]
		public System.Nullable<float> Cash_Collected_1000P
		{
			get
			{
				return this._Cash_Collected_1000P;
			}
			set
			{
				if ((this._Cash_Collected_1000P != value))
				{
					this._Cash_Collected_1000P = value ?? 0;
				}
			}
		}
		
		[Column(Storage="_Cash_Collected_500P", DbType="Real")]
		public System.Nullable<float> Cash_Collected_500P
		{
			get
			{
				return this._Cash_Collected_500P;
			}
			set
			{
				if ((this._Cash_Collected_500P != value))
				{
					this._Cash_Collected_500P = value ?? 0;
				}
			}
		}
		
		[Column(Storage="_Cash_Collected_200P", DbType="Real")]
		public System.Nullable<float> Cash_Collected_200P
		{
			get
			{
				return this._Cash_Collected_200P;
			}
			set
			{
				if ((this._Cash_Collected_200P != value))
				{
					this._Cash_Collected_200P = value ?? 0;
				}
			}
		}
		
		[Column(Storage="_Cash_Collected_100P", DbType="Real")]
		public System.Nullable<float> Cash_Collected_100P
		{
			get
			{
				return this._Cash_Collected_100P;
			}
			set
			{
				if ((this._Cash_Collected_100P != value))
				{
					this._Cash_Collected_100P = value ?? 0;
				}
			}
		}
		
		[Column(Storage="_Cash_Collected_50p", DbType="Real")]
		public System.Nullable<float> Cash_Collected_50p
		{
			get
			{
				return this._Cash_Collected_50p;
			}
			set
			{
				if ((this._Cash_Collected_50p != value))
				{
					this._Cash_Collected_50p = value ?? 0;
				}
			}
		}
		
		[Column(Storage="_Cash_Collected_20p", DbType="Real")]
		public System.Nullable<float> Cash_Collected_20p
		{
			get
			{
				return this._Cash_Collected_20p;
			}
			set
			{
				if ((this._Cash_Collected_20p != value))
				{
					this._Cash_Collected_20p = value ?? 0;
				}
			}
		}
		
		[Column(Storage="_Cash_Collected_10p", DbType="Real")]
		public System.Nullable<float> Cash_Collected_10p
		{
			get
			{
				return this._Cash_Collected_10p;
			}
			set
			{
				if ((this._Cash_Collected_10p != value))
				{
					this._Cash_Collected_10p = value ?? 0;
				}
			}
		}
		
		[Column(Storage="_Cash_Collected_5p", DbType="Real")]
		public System.Nullable<float> Cash_Collected_5p
		{
			get
			{
				return this._Cash_Collected_5p;
			}
			set
			{
				if ((this._Cash_Collected_5p != value))
				{
					this._Cash_Collected_5p = value ?? 0;
				}
			}
		}
		
		[Column(Storage="_Cash_Collected_2p", DbType="Real")]
		public System.Nullable<float> Cash_Collected_2p
		{
			get
			{
				return this._Cash_Collected_2p;
			}
			set
			{
				if ((this._Cash_Collected_2p != value))
				{
					this._Cash_Collected_2p = value ?? 0;
				}
			}
		}

        [Column(Storage = "_Cash_Collected_1p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_1p
        {
            get
            {
                return this._Cash_Collected_1p;
            }
            set
            {
                if ((this._Cash_Collected_1p != value))
                {
                    this._Cash_Collected_1p = value ?? 0;
                }
            }
        }
		
		[Column(Storage="_TicketsIn", DbType="Money NOT NULL")]
		public decimal TicketsIn
		{
			get
			{
				return this._TicketsIn;
			}
			set
			{
				if ((this._TicketsIn != value))
				{
					this._TicketsIn = value;
				}
			}
		}
		
		[Column(Storage="_TicketsOut", DbType="Money NOT NULL")]
		public decimal TicketsOut
		{
			get
			{
				return this._TicketsOut;
			}
			set
			{
				if ((this._TicketsOut != value))
				{
					this._TicketsOut = value;
				}
			}
		}

        [Column(Storage = "_ShortPay", DbType = "Decimal NOT NULL")]
        public decimal ShortPay
		{
			get
			{
				return this._ShortPay;
			}
			set
			{
				if ((this._ShortPay != value))
				{
					this._ShortPay = value;
				}
			}
		}

        [Column(Storage = "_Handpay", DbType = "Decimal NOT NULL")]
		public decimal Handpay
		{
			get
			{
				return this._Handpay;
			}
			set
			{
				if ((this._Handpay != value))
				{
					this._Handpay = value;
				}
			}
		}
		
		[Column(Storage="_Refills", DbType="Real NOT NULL")]
		public float Refills
		{
			get
			{
				return this._Refills;
			}
			set
			{
				if ((this._Refills != value))
				{
					this._Refills = value;
				}
			}
		}
		
		[Column(Storage="_Refunds", DbType="Real NOT NULL")]
		public float Refunds
		{
			get
			{
				return this._Refunds;
			}
			set
			{
				if ((this._Refunds != value))
				{
					this._Refunds = value;
				}
			}
		}

        [Column(Storage = "_HandpayJackpot", DbType = "Decimal NOT NULL")]
		public decimal HandpayJackpot
		{
			get
			{
				return this._HandpayJackpot;
			}
			set
			{
				if ((this._HandpayJackpot != value))
				{
					this._HandpayJackpot = value;
				}
			}
		}

        [Column(Storage = "_DeclaredHandpay", DbType = "Decimal NOT NULL")]
		public decimal DeclaredHandpay
		{
			get
			{
				return this._DeclaredHandpay;
			}
			set
			{
				if ((this._DeclaredHandpay != value))
				{
					this._DeclaredHandpay = value;
				}
			}
		}

        [Column(Storage = "_CoinOut", DbType = "Float NOT NULL")]
        public float CoinOut
        {
            get
            {
                return this._CoinOut;
            }
            set
            {
                if ((this._CoinOut != value))
                {
                    this._CoinOut = value;
                }
            }
        }

        [Column(Storage = "_AttendantPay", DbType = "Decimal NOT NULL")]
        public decimal AttendantPay
        {
            get
            {
                return this._AttendantPay;
            }
            set
            {
                if ((this._AttendantPay != value))
                {
                    this._AttendantPay = value;
                }
            }
        }

        [Column(Storage = "_CoinIn", DbType = "Float NOT NULL")]
        public float CoinIn
        {
            get
            {
                return this._CoinIn;
            }
            set
            {
                if ((this._CoinIn != value))
                {
                    this._CoinIn = value;
                }
            }
        }

        [Column(Storage = "_Collection_Batch_Name", DbType = "VarChar(50)")]
        public string Collection_Batch_Name
        {
            get
            {
                return this._Collection_Batch_Name;
            }
            set
            {
                if ((this._Collection_Batch_Name != value))
                {
                    this._Collection_Batch_Name = value;
                }
            }
        }
	}

    public partial class rsp_GetUndeclaredPartCollectionByMachineResult
    {

        private System.Nullable<int> _Zone_No;

        private string _Zone_Name;

        private string _Bar_Pos_Name;

        private string _AssetNo;

        private string _Name;

        private System.Nullable<bool> _Machine_Uses_Meters;

        private int _Installation_No;

        private System.Nullable<int> _Installation_Token_Value;

        private System.Nullable<System.DateTime> _Date;

        private System.Nullable<int> _Collection_No;

        private int _Collection_Batch_No;

        private System.Nullable<bool> _Collection_Defloat_Collection;

        private int _CollectionHandHeldMetersReceived;

        private System.Nullable<float> _Cash_Collected_100000P;

        private System.Nullable<float> _Cash_Collected_50000P;

        private System.Nullable<float> _Cash_Collected_20000P;

        private System.Nullable<float> _Cash_Collected_10000P;

        private System.Nullable<float> _Cash_Collected_5000P;

        private System.Nullable<float> _Cash_Collected_2000p;

        private System.Nullable<float> _Cash_Collected_1000P;

        private System.Nullable<float> _Cash_Collected_500P;

        private System.Nullable<float> _Cash_Collected_200P;

        private System.Nullable<float> _Cash_Collected_100P;

        private System.Nullable<float> _Cash_Collected_50p;

        private System.Nullable<float> _Cash_Collected_20p;

        private System.Nullable<float> _Cash_Collected_10p;

        private System.Nullable<float> _Cash_Collected_5p;

        private System.Nullable<float> _Cash_Collected_2p;

        private System.Nullable<float> _Cash_Collected_1p;

        private decimal _TicketsIn;

        private decimal _TicketsOut;

        private float _ShortPay;

        private float _Handpay;

        private float _Refills;

        private float _Refunds;

        private float _HandpayJackpot;

        private float _DeclaredHandpay;

        public rsp_GetUndeclaredPartCollectionByMachineResult()
        {
        }

        [Column(Storage = "_Zone_No", DbType = "Int")]
        public System.Nullable<int> Zone_No
        {
            get
            {
                return this._Zone_No;
            }
            set
            {
                if ((this._Zone_No != value))
                {
                    this._Zone_No = value;
                }
            }
        }

        [Column(Storage = "_Zone_Name", DbType = "VarChar(50)")]
        public string Zone_Name
        {
            get
            {
                return this._Zone_Name;
            }
            set
            {
                if ((this._Zone_Name != value))
                {
                    this._Zone_Name = value;
                }
            }
        }

        [Column(Storage = "_Bar_Pos_Name", DbType = "VarChar(50)")]
        public string Bar_Pos_Name
        {
            get
            {
                return this._Bar_Pos_Name;
            }
            set
            {
                if ((this._Bar_Pos_Name != value))
                {
                    this._Bar_Pos_Name = value;
                }
            }
        }

        [Column(Storage = "_AssetNo", DbType = "VarChar(50)")]
        public string AssetNo
        {
            get
            {
                return this._AssetNo;
            }
            set
            {
                if ((this._AssetNo != value))
                {
                    this._AssetNo = value;
                }
            }
        }

        [Column(Storage = "_Name", DbType = "VarChar(50)")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this._Name = value;
                }
            }
        }

        [Column(Storage = "_Machine_Uses_Meters", DbType = "Bit")]
        public System.Nullable<bool> Machine_Uses_Meters
        {
            get
            {
                return this._Machine_Uses_Meters;
            }
            set
            {
                if ((this._Machine_Uses_Meters != value))
                {
                    this._Machine_Uses_Meters = value;
                }
            }
        }

        [Column(Storage = "_Installation_No", DbType = "Int NOT NULL")]
        public int Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
                }
            }
        }


        [Column(Storage = "_Installation_Token_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Token_Value
        {
            get
            {
                return this._Installation_Token_Value;
            }
            set
            {
                if ((this._Installation_Token_Value != value))
                {
                    this._Installation_Token_Value = value;
                }
            }
        }
		

        [Column(Storage = "_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Date
        {
            get
            {
                return this._Date;
            }
            set
            {
                if ((this._Date != value))
                {
                    this._Date = value;
                }
            }
        }

        [Column(Storage = "_Collection_No", DbType = "Int")]
        public System.Nullable<int> Collection_No
        {
            get
            {
                return this._Collection_No;
            }
            set
            {
                if ((this._Collection_No != value))
                {
                    this._Collection_No = value ?? 0;
                }
            }
        }

        [Column(Storage = "_Collection_Batch_No", DbType = "Int NOT NULL")]
        public int Collection_Batch_No
        {
            get
            {
                return this._Collection_Batch_No;
            }
            set
            {
                if ((this._Collection_Batch_No != value))
                {
                    this._Collection_Batch_No = value;
                }
            }
        }

        [Column(Storage = "_Collection_Defloat_Collection", DbType = "Bit")]
        public System.Nullable<bool> Collection_Defloat_Collection
        {
            get
            {
                return this._Collection_Defloat_Collection;
            }
            set
            {
                if ((this._Collection_Defloat_Collection != value))
                {
                    this._Collection_Defloat_Collection = value;
                }
            }
        }

        [Column(Storage = "_CollectionHandHeldMetersReceived", DbType = "Int NOT NULL")]
        public int CollectionHandHeldMetersReceived
        {
            get
            {
                return this._CollectionHandHeldMetersReceived;
            }
            set
            {
                if ((this._CollectionHandHeldMetersReceived != value))
                {
                    this._CollectionHandHeldMetersReceived = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_100000P", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_100000P
        {
            get
            {
                return this._Cash_Collected_100000P;
            }
            set
            {
                if ((this._Cash_Collected_100000P != value))
                {
                    this._Cash_Collected_100000P = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_50000P", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_50000P
        {
            get
            {
                return this._Cash_Collected_50000P;
            }
            set
            {
                if ((this._Cash_Collected_50000P != value))
                {
                    this._Cash_Collected_50000P = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_20000P", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_20000P
        {
            get
            {
                return this._Cash_Collected_20000P;
            }
            set
            {
                if ((this._Cash_Collected_20000P != value))
                {
                    this._Cash_Collected_20000P = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_10000P", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_10000P
        {
            get
            {
                return this._Cash_Collected_10000P;
            }
            set
            {
                if ((this._Cash_Collected_10000P != value))
                {
                    this._Cash_Collected_10000P = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_5000P", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_5000P
        {
            get
            {
                return this._Cash_Collected_5000P;
            }
            set
            {
                if ((this._Cash_Collected_5000P != value))
                {
                    this._Cash_Collected_5000P = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_2000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_2000p
        {
            get
            {
                return this._Cash_Collected_2000p;
            }
            set
            {
                if ((this._Cash_Collected_2000p != value))
                {
                    this._Cash_Collected_2000p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_1000P", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_1000P
        {
            get
            {
                return this._Cash_Collected_1000P;
            }
            set
            {
                if ((this._Cash_Collected_1000P != value))
                {
                    this._Cash_Collected_1000P = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_500P", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_500P
        {
            get
            {
                return this._Cash_Collected_500P;
            }
            set
            {
                if ((this._Cash_Collected_500P != value))
                {
                    this._Cash_Collected_500P = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_200P", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_200P
        {
            get
            {
                return this._Cash_Collected_200P;
            }
            set
            {
                if ((this._Cash_Collected_200P != value))
                {
                    this._Cash_Collected_200P = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_100P", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_100P
        {
            get
            {
                return this._Cash_Collected_100P;
            }
            set
            {
                if ((this._Cash_Collected_100P != value))
                {
                    this._Cash_Collected_100P = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_50p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_50p
        {
            get
            {
                return this._Cash_Collected_50p;
            }
            set
            {
                if ((this._Cash_Collected_50p != value))
                {
                    this._Cash_Collected_50p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_20p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_20p
        {
            get
            {
                return this._Cash_Collected_20p;
            }
            set
            {
                if ((this._Cash_Collected_20p != value))
                {
                    this._Cash_Collected_20p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_10p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_10p
        {
            get
            {
                return this._Cash_Collected_10p;
            }
            set
            {
                if ((this._Cash_Collected_10p != value))
                {
                    this._Cash_Collected_10p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_5p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_5p
        {
            get
            {
                return this._Cash_Collected_5p;
            }
            set
            {
                if ((this._Cash_Collected_5p != value))
                {
                    this._Cash_Collected_5p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_2p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_2p
        {
            get
            {
                return this._Cash_Collected_2p;
            }
            set
            {
                if ((this._Cash_Collected_2p != value))
                {
                    this._Cash_Collected_2p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_1p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_1p
        {
            get
            {
                return this._Cash_Collected_1p;
            }
            set
            {
                if ((this._Cash_Collected_1p != value))
                {
                    this._Cash_Collected_1p = value;
                }
            }
        }

        [Column(Storage = "_TicketsIn", DbType = "Money NOT NULL")]
        public decimal TicketsIn
        {
            get
            {
                return this._TicketsIn;
            }
            set
            {
                if ((this._TicketsIn != value))
                {
                    this._TicketsIn = value;
                }
            }
        }

        [Column(Storage = "_TicketsOut", DbType = "Money NOT NULL")]
        public decimal TicketsOut
        {
            get
            {
                return this._TicketsOut;
            }
            set
            {
                if ((this._TicketsOut != value))
                {
                    this._TicketsOut = value;
                }
            }
        }

        [Column(Storage = "_ShortPay", DbType = "Real NOT NULL")]
        public float ShortPay
        {
            get
            {
                return this._ShortPay;
            }
            set
            {
                if ((this._ShortPay != value))
                {
                    this._ShortPay = value;
                }
            }
        }

        [Column(Storage = "_Handpay", DbType = "Real NOT NULL")]
        public float Handpay
        {
            get
            {
                return this._Handpay;
            }
            set
            {
                if ((this._Handpay != value))
                {
                    this._Handpay = value;
                }
            }
        }

        [Column(Storage = "_Refills", DbType = "Real NOT NULL")]
        public float Refills
        {
            get
            {
                return this._Refills;
            }
            set
            {
                if ((this._Refills != value))
                {
                    this._Refills = value;
                }
            }
        }

        [Column(Storage = "_Refunds", DbType = "Real NOT NULL")]
        public float Refunds
        {
            get
            {
                return this._Refunds;
            }
            set
            {
                if ((this._Refunds != value))
                {
                    this._Refunds = value;
                }
            }
        }

        [Column(Storage = "_HandpayJackpot", DbType = "Real NOT NULL")]
        public float HandpayJackpot
        {
            get
            {
                return this._HandpayJackpot;
            }
            set
            {
                if ((this._HandpayJackpot != value))
                {
                    this._HandpayJackpot = value;
                }
            }
        }

        [Column(Storage = "_DeclaredHandpay", DbType = "Real NOT NULL")]
        public float DeclaredHandpay
        {
            get
            {
                return this._DeclaredHandpay;
            }
            set
            {
                if ((this._DeclaredHandpay != value))
                {
                    this._DeclaredHandpay = value;
                }
            }
        }
    }

    [DataContract()]
    public partial class CollectionWeekData
    {
		
		private int _WeekNo;
		
		private int _WeekNumber;
		
		private System.DateTime _WeekStartDate;
		
		private System.DateTime _WeekEndDate;
		
		private int _CollectionCount;
		
		private double _CashTake;
		
		private double _TakeVar;

        private int _BatchNo;

        private double _Eft;

        public CollectionWeekData()
		{
		}
		
		[Column(Storage="_WeekNo", DbType="Int NOT NULL")]
		public int WeekNo
		{
			get
			{
				return this._WeekNo;
			}
			set
			{
				if ((this._WeekNo != value))
				{
					this._WeekNo = value;
				}
			}
		}
		
		[Column(Storage="_WeekNumber", DbType="Int NOT NULL")]
		public int WeekNumber
		{
			get
			{
				return this._WeekNumber;
			}
			set
			{
				if ((this._WeekNumber != value))
				{
					this._WeekNumber = value;
				}
			}
		}
		
		[Column(Storage="_WeekStartDate", DbType="DateTime NOT NULL")]
		public System.DateTime WeekStartDate
		{
			get
			{
				return this._WeekStartDate;
			}
			set
			{
				if ((this._WeekStartDate != value))
				{
					this._WeekStartDate = value;
				}
			}
		}
		
		[Column(Storage="_WeekEndDate", DbType="DateTime NOT NULL")]
		public System.DateTime WeekEndDate
		{
			get
			{
				return this._WeekEndDate;
			}
			set
			{
				if ((this._WeekEndDate != value))
				{
					this._WeekEndDate = value;
				}
			}
		}
		
		[Column(Storage="_CollectionCount", DbType="Int NOT NULL")]
		public int CollectionCount
		{
			get
			{
				return this._CollectionCount;
			}
			set
			{
				if ((this._CollectionCount != value))
				{
					this._CollectionCount = value;
				}
			}
		}

        [Column(Storage = "_BatchNo", DbType = "Int NOT NULL")]
        public int BatchNo
        {
            get
            {
                return this._BatchNo;
            }
            set
            {
                if ((this._BatchNo != value))
                {
                    this._BatchNo = value;
                }
            }
        }
		
		[Column(Storage="_CashTake", DbType="Float NOT NULL")]
		public double CashTake
		{
			get
			{
				return this._CashTake;
			}
			set
			{
				if ((this._CashTake != value))
				{
					this._CashTake = value;
				}
			}
		}
		
		[Column(Storage="_TakeVar", DbType="Float NOT NULL")]
		public double TakeVar
		{
			get
			{
				return this._TakeVar;
			}
			set
			{
				if ((this._TakeVar != value))
				{
					this._TakeVar = value;
				}
			}
		}

        [Column(Storage = "_Eft", DbType = "Float NOT NULL")]
        public double Eft
        {
            get
            {
                return this._Eft;
            }
            set
            {
                if ((this._Eft != value))
                {
                    this._Eft = value;
                }
            }
        }
	}

    [DataContract()]
    public partial class CollectionBatchData
    {

        private int _Collection_Batch_No;

        private string _Collection_Batch_Name;

        private System.Nullable<System.DateTime> _Collection_Batch_Date;

        private System.Nullable<System.DateTime> _Collection_Batch_Date_Performed;

        public CollectionBatchData()
        {
        }

        [Column(Storage = "_Collection_Batch_No", DbType = "Int NOT NULL")]
        public int Collection_Batch_No
        {
            get
            {
                return this._Collection_Batch_No;
            }
            set
            {
                if ((this._Collection_Batch_No != value))
                {
                    this._Collection_Batch_No = value;
                }
            }
        }

        [Column(Storage = "_Collection_Batch_Name", DbType = "VarChar(50)")]
        public string Collection_Batch_Name
        {
            get
            {
                return this._Collection_Batch_Name;
            }
            set
            {
                if ((this._Collection_Batch_Name != value))
                {
                    this._Collection_Batch_Name = value;
                }
            }
        }

        [Column(Storage = "_Collection_Batch_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Collection_Batch_Date
        {
            get
            {
                return this._Collection_Batch_Date;
            }
            set
            {
                if ((this._Collection_Batch_Date != value))
                {
                    this._Collection_Batch_Date = value ?? DateTime.Now;
                }
            }
        }

        [Column(Storage = "_Collection_Batch_Date_Performed", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Collection_Batch_Date_Performed
        {
            get
            {
                return this._Collection_Batch_Date_Performed;
            }
            set
            {
                if ((this._Collection_Batch_Date_Performed != value))
                {
                    this._Collection_Batch_Date_Performed = value ?? DateTime.Now;
                }
            }
        }
    }

    [DataContract()]
    public partial class CollectionMachineData
    {

        private System.Nullable<int> _Installation_No;

        private int _inplay;

        private int _IsHandPayUnProcessed;

        public CollectionMachineData()
        {
        }

        [Column(Storage = "_Installation_No", DbType = "Int")]
        public System.Nullable<int> Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
                }
            }
        }

        [Column(Storage = "_inplay", DbType = "Int NOT NULL")]
        public int inplay
        {
            get
            {
                return this._inplay;
            }
            set
            {
                if ((this._inplay != value))
                {
                    this._inplay = value;
                }
            }
        }

        [Column(Storage = "_IsHandPayUnProcessed", DbType = "Int NOT NULL")]
        public int IsHandPayUnProcessed
        {
            get
            {
                return this._IsHandPayUnProcessed;
            }
            set
            {
                if ((this._IsHandPayUnProcessed != value))
                {
                    this._IsHandPayUnProcessed = value;
                }
            }
        }
    }


    [DataContract()]
    public partial class PartCollectionBatchData
   {
		
		private int _Part_Collection_No;
		
		private System.Nullable<System.DateTime> _Part_Collection_Date;
		
		private System.Nullable<float> _Part_Collection_CashCollected;
		
		private string _Bar_Pos_Name;
		
		private string _Name;

        public PartCollectionBatchData()
		{
		}
		
		[Column(Storage="_Part_Collection_No", DbType="Int NOT NULL")]
		public int Part_Collection_No
		{
			get
			{
				return this._Part_Collection_No;
			}
			set
			{
				if ((this._Part_Collection_No != value))
				{
					this._Part_Collection_No = value;
				}
			}
		}
		
		[Column(Storage="_Part_Collection_Date", DbType="DateTime")]
		public System.Nullable<System.DateTime> Part_Collection_Date
		{
			get
			{
				return this._Part_Collection_Date;
			}
			set
			{
				if ((this._Part_Collection_Date != value))
				{
					this._Part_Collection_Date = value;
				}
			}
		}
		
		[Column(Storage="_Part_Collection_CashCollected", DbType="Real")]
		public System.Nullable<float> Part_Collection_CashCollected
		{
			get
			{
				return this._Part_Collection_CashCollected;
			}
			set
			{
				if ((this._Part_Collection_CashCollected != value))
				{
					this._Part_Collection_CashCollected = value;
				}
			}
		}
		
		[Column(Storage="_Bar_Pos_Name", DbType="VarChar(50)")]
		public string Bar_Pos_Name
		{
			get
			{
				return this._Bar_Pos_Name;
			}
			set
			{
				if ((this._Bar_Pos_Name != value))
				{
					this._Bar_Pos_Name = value;
				}
			}
		}
		
		[Column(Storage="_Name", DbType="VarChar(50)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this._Name = value;
				}
			}
		}
	}

    public partial class rsp_GetDeclaredTicketResult :INotifyPropertyChanged
    {

        private int _ID;

        private System.Nullable<decimal> _Value;

        private string _BarCode;

        public rsp_GetDeclaredTicketResult()
        {
        }

        [Column(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                    PropertyChangedEvent("ID");
                }
            }
        }

        [Column(Storage = "_Value", DbType = "Money")]
        public System.Nullable<decimal> Value
        {
            get
            {
                return this._Value;
            }
            set
            {
                if ((this._Value != value))
                {
                    this._Value = value;
                    PropertyChangedEvent("Value");
                }
            }
        }

        [Column(Storage = "_BarCode", DbType = "VarChar(50)")]
        public string BarCode
        {
            get
            {
                return this._BarCode;
            }
            set
            {
                if ((this._BarCode != value))
                {
                    this._BarCode = value;
                    PropertyChangedEvent("BarCode");
                }
            }
        }

        public string FormatedValue
        {
            get
            {
                return ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol()+ " "+((decimal)Value).GetUniversalCurrencyFormat();
            }

            set
            {
                
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public void PropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public partial class CreditStatus
    {

        private System.Nullable<int> _Installation_No;

        private int _inplay;

        private int _IsHandPayUnProcessed;

        private int _isCardedPlay;

        private int _isGMUUPdate;

        public CreditStatus()
        {
        }

        [Column(Storage = "_Installation_No", DbType = "Int")]
        public System.Nullable<int> Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
                }
            }
        }

        [Column(Storage = "_inplay", DbType = "Int NOT NULL")]
        public int inplay
        {
            get
            {
                return this._inplay;
            }
            set
            {
                if ((this._inplay != value))
                {
                    this._inplay = value;
                }
            }
        }

        [Column(Storage = "_IsHandPayUnProcessed", DbType = "Int NOT NULL")]
        public int IsHandPayUnProcessed
        {
            get
            {
                return this._IsHandPayUnProcessed;
            }
            set
            {
                if ((this._IsHandPayUnProcessed != value))
                {
                    this._IsHandPayUnProcessed = value;
                }
            }
        }

        [Column(Storage = "_isCardedPlay", DbType = "Int NOT NULL")]
        public int isCardedPlay
        {
            get
            {
                return this._isCardedPlay;
            }
            set
            {
                if ((this._isCardedPlay != value))
                {
                    this._isCardedPlay = value;
                }
            }
        }


        [Column(Storage = "_isGMUUPdate", DbType = "Int NOT NULL")]
        public int isGMUUPdate
        {
            get
            {
                return this._isGMUUPdate;
            }
            set
            {
                if ((this._isGMUUPdate != value))
                {
                    this._isGMUUPdate = value;
                }
            }
        }
    }
}
