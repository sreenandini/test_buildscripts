using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;

namespace BMC.Transport.CashDeskOperatorEntity
{

    public class FillTreasuryList
    {
		
		private int _TE_ID;
		
		private int _Installation_No;
		
		private System.Nullable<int> _Datapak_No;
		
		private string _Pos;
		
		private string _Machine;
		
        private string _Asset;
		private System.Nullable<System.DateTime> _TreasuryDate;
		
		private System.Nullable<double> _Amount;
		
		private string _HP_Type;
		
		private System.Nullable<bool> _HP_Uncleared;

        public FillTreasuryList()
		{
		}
		
		[Column(Storage="_TE_ID", DbType="Int NOT NULL")]
		public int TE_ID
		{
			get
			{
				return this._TE_ID;
			}
			set
			{
				if ((this._TE_ID != value))
				{
					this._TE_ID = value;
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
		
		[Column(Storage="_Datapak_No", DbType="Int")]
		public System.Nullable<int> Datapak_No
		{
			get
			{
				return this._Datapak_No;
			}
			set
			{
				if ((this._Datapak_No != value))
				{
					this._Datapak_No = value;
				}
			}
		}
		
		[Column(Storage="_Pos", DbType="VarChar(50)")]
		public string Pos
		{
			get
			{
				return this._Pos;
			}
			set
			{
				if ((this._Pos != value))
				{
					this._Pos = value;
				}
			}
		}
		
		[Column(Storage="_Machine", DbType="VarChar(50)")]
		public string Machine
		{
			get
			{
				return this._Machine;
			}
			set
			{
				if ((this._Machine != value))
				{
					this._Machine = value;
				}
			}
		}
		
        [Column(Storage = "_Asset", DbType = "VarChar(50)")]
        public string Asset
        {
            get
            {
                return this._Asset;
            }
            set
            {
                if ((this._Asset != value))
                {
                    this._Asset = value;
                }
            }
        }
		[Column(Storage="_TreasuryDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> TreasuryDate
		{
			get
			{
				return this._TreasuryDate;
			}
			set
			{
				if ((this._TreasuryDate != value))
				{
					this._TreasuryDate = value;
				}
			}
		}
		
		[Column(Storage="_Amount", DbType="Float")]
		public System.Nullable<double> Amount
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
		
		[Column(Storage="_HP_Type", DbType="VarChar(50)")]
		public string HP_Type
		{
			get
			{
				return this._HP_Type;
			}
			set
			{
				if ((this._HP_Type != value))
				{
					this._HP_Type = value;
				}
			}
		}
		
		[Column(Storage="_HP_Uncleared", DbType="Bit")]
		public System.Nullable<bool> HP_Uncleared
		{
			get
			{
				return this._HP_Uncleared;
			}
			set
			{
				if ((this._HP_Uncleared != value))
				{
					this._HP_Uncleared = value ?? false;
				}
			}
		}
	}

    public class AssetNumberResult
    {

        private string _Stock_No;

        private string _Bar_Pos_Name;

        private string _GameName;

        private float _PercPayout;

        private int _Denom;

        private string _ZoneName;

        private string _ManufacturerName;

        private string _GameType;

        public AssetNumberResult()
        {
        }

        [Column(Storage = "_Stock_No", DbType = "VarChar(50)")]
        public string Stock_No
        {
            get
            {
                return this._Stock_No;
            }
            set
            {
                if ((this._Stock_No != value))
                {
                    this._Stock_No = value;
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

        [Column(Storage = "_GameName", DbType = "VarChar(50)")]
        public string GameName
        {
            get
            {
                return this._GameName;
            }
            set
            {
                if ((this._GameName != value))
                {
                    this._GameName = value;
                }
            }
        }


        [Column(Storage = "_PercPayout", DbType = "real")]
        public float PercPayout
        {
            get
            {
                return this._PercPayout;
            }
            set
            {
                if ((this._PercPayout != value))
                {
                    this._PercPayout = value;
                }
            }
        }

        [Column(Storage = "_Denom", DbType = "int")]
        public int Denom
        {
            get
            {
                return this._Denom;
            }
            set
            {
                if ((this._Denom != value))
                {
                    this._Denom = value;
                }
            }
        }

        [Column(Storage = "_ZoneName", DbType = "VarChar(50)")]
        public string ZoneName
        {
            get
            {
                return this._ZoneName;
            }
            set
            {
                if ((this._ZoneName != value))
                {
                    this._ZoneName = value;
                }
            }
        }

        [Column(Storage = "_ManufacturerName", DbType = "VarChar(50)")]
        public string ManufacturerName
        {
            get
            {
                return this._ManufacturerName;
            }
            set
            {
                if ((this._ManufacturerName != value))
                {
                    this._ManufacturerName = value;
                }
            }
        }

        [Column(Storage = "_GameType", DbType = "VarChar(50)")]
        public string GameType
        {
            get
            {
                return this._GameType;
            }
            set
            {
                if ((this._GameType != value))
                {
                    this._GameType = value;
                }
            }
        }
    }

    public class BarPositions:IEquatable<BarPositions>
    {
        private int _Bar_Pos_No;

        private string _Bar_Pos_Name;

        private int _Installation_No;

        [Column(Storage = "_Bar_Pos_No", DbType = "Int not NULL")]
        public int Bar_Pos_No
        {
            get
            {
                return this._Bar_Pos_No;
            }
            set
            {
                if ((this._Bar_Pos_No != value))
                {
                    this._Bar_Pos_No = value;
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

        [Column(Storage = "_Installation_No", DbType = "Int not NULL")]
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


        #region IEquatable<BarPositions> Members

        public bool Equals(BarPositions other)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return Bar_Pos_Name.Equals(other.Bar_Pos_Name) && Installation_No.Equals(other.Installation_No);
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null.
            int hashProductName = Bar_Pos_Name == null ? 0 : Bar_Pos_Name.GetHashCode();

            //Get hash code for the Code field.
            int hashProductCode = Bar_Pos_No.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductName ^ hashProductCode;
        }

        #endregion
    }

    public class DenomValueResult
    {

        private decimal _Denom;


        public DenomValueResult()
        {
        }

        [Column(Storage = "_Denom", DbType = "Decimal")]
        public Decimal Denom
        {
            get
            {
                return this._Denom;
            }
            set
            {
                if ((this._Denom != value))
                {
                    this._Denom = value;
                }
            }
        }
  
    }


}