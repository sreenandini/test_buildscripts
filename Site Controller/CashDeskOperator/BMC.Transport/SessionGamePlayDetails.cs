using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.Transport
{      

    public partial class SessionGamePlayDetails
    {
	
	private string _Position;
		
		private string _Zone;
		
		private string _Asset_No;
		
		private string _Game_Title;
		
		private string _GMUNo;
		
		private string _Maufacturer;
		
		private string _Category;
		
		private System.Nullable<int> _Base_Denom;
		
		private System.Nullable<int> _CoinValue;
		
		private System.Nullable<float> _PayoutPer;
		
		private string _BillValStatus;
		
		private System.Nullable<int> _GameCapping;
		
		private System.Nullable<bool> _Event_Status;
		
		private System.Nullable<int> _Play_Status;
		
		private System.Nullable<int> _Drop_Status;
		
		private double _Total_BuyIn;
		
		private string _Total_BuyIn_Color;
		
		private double _Win_Loss;
		
		private string _Win_Loss_Color;
		
		private int _Time_Played;
		
		private string _Time_Played_Color;

        public bool IsTotalRow { get; set; }
        public SessionGamePlayDetails()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Position", DbType="VarChar(50)")]
		public string Position
		{
			get
			{
				return this._Position;
			}
			set
			{
				if ((this._Position != value))
				{
					this._Position = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Zone", DbType="VarChar(50)")]
		public string Zone
		{
			get
			{
				return this._Zone;
			}
			set
			{
				if ((this._Zone != value))
				{
					this._Zone = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Asset_No", DbType="VarChar(50)")]
		public string Asset_No
		{
			get
			{
				return this._Asset_No;
			}
			set
			{
				if ((this._Asset_No != value))
				{
					this._Asset_No = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Game_Title", DbType="VarChar(50)")]
		public string Game_Title
		{
			get
			{
				return this._Game_Title;
			}
			set
			{
				if ((this._Game_Title != value))
				{
					this._Game_Title = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GMUNo", DbType="VarChar(50)")]
		public string GMUNo
		{
			get
			{
				return this._GMUNo;
			}
			set
			{
				if ((this._GMUNo != value))
				{
					this._GMUNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Maufacturer", DbType="VarChar(50)")]
		public string Maufacturer
		{
			get
			{
				return this._Maufacturer;
			}
			set
			{
				if ((this._Maufacturer != value))
				{
					this._Maufacturer = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Category", DbType="VarChar(50)")]
		public string Category
		{
			get
			{
				return this._Category;
			}
			set
			{
				if ((this._Category != value))
				{
					this._Category = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Base_Denom", DbType="Int")]
		public System.Nullable<int> Base_Denom
		{
			get
			{
				return this._Base_Denom;
			}
			set
			{
				if ((this._Base_Denom != value))
				{
					this._Base_Denom = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CoinValue", DbType="Int")]
		public System.Nullable<int> CoinValue
		{
			get
			{
				return this._CoinValue;
			}
			set
			{
				if ((this._CoinValue != value))
				{
					this._CoinValue = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PayoutPer", DbType="Real")]
		public System.Nullable<float> PayoutPer
		{
			get
			{
				return this._PayoutPer;
			}
			set
			{
				if ((this._PayoutPer != value))
				{
					this._PayoutPer = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BillValStatus", DbType="VarChar(50)")]
		public string BillValStatus
		{
			get
			{
				return this._BillValStatus;
			}
			set
			{
				if ((this._BillValStatus != value))
				{
					this._BillValStatus = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GameCapping", DbType="Int")]
		public System.Nullable<int> GameCapping
		{
			get
			{
				return this._GameCapping;
			}
			set
			{
				if ((this._GameCapping != value))
				{
					this._GameCapping = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Event_Status", DbType="Bit")]
		public System.Nullable<bool> Event_Status
		{
			get
			{
				return this._Event_Status;
			}
			set
			{
				if ((this._Event_Status != value))
				{
					this._Event_Status = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Play_Status", DbType="Int")]
		public System.Nullable<int> Play_Status
		{
			get
			{
				return this._Play_Status;
			}
			set
			{
				if ((this._Play_Status != value))
				{
					this._Play_Status = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Drop_Status", DbType="Int")]
		public System.Nullable<int> Drop_Status
		{
			get
			{
				return this._Drop_Status;
			}
			set
			{
				if ((this._Drop_Status != value))
				{
					this._Drop_Status = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Total_BuyIn", DbType="Float NOT NULL")]
		public double Total_BuyIn
		{
			get
			{
				return this._Total_BuyIn;
			}
			set
			{
				if ((this._Total_BuyIn != value))
				{
					this._Total_BuyIn = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Total_BuyIn_Color", DbType="VarChar(50)")]
		public string Total_BuyIn_Color
		{
			get
			{
				return this._Total_BuyIn_Color;
			}
			set
			{
				if ((this._Total_BuyIn_Color != value))
				{
					this._Total_BuyIn_Color = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Win_Loss", DbType="Float NOT NULL")]
		public double Win_Loss
		{
			get
			{
				return this._Win_Loss;
			}
			set
			{
				if ((this._Win_Loss != value))
				{
					this._Win_Loss = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Win_Loss_Color", DbType="VarChar(50)")]
		public string Win_Loss_Color
		{
			get
			{
				return this._Win_Loss_Color;
			}
			set
			{
				if ((this._Win_Loss_Color != value))
				{
					this._Win_Loss_Color = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Time_Played", DbType="Int NOT NULL")]
		public int Time_Played
		{
			get
			{
				return this._Time_Played;
			}
			set
			{
				if ((this._Time_Played != value))
				{
					this._Time_Played = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Time_Played_Color", DbType="VarChar(50)")]
		public string Time_Played_Color
		{
			get
			{
				return this._Time_Played_Color;
			}
			set
			{
				if ((this._Time_Played_Color != value))
				{
					this._Time_Played_Color = value;
				}
			}
		}
    }
}
