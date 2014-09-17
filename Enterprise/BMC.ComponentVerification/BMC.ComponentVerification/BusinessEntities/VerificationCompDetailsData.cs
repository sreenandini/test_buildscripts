using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.ComponentVerification.BusinessEntities
{
    public partial class VerificationCompDetailsData
	{		
		private System.Nullable<int> _Installation_No;		
		private string _Serial_No;		
		private string _Component_Type;		
		private string _Component_Name;		
		private string _Verification_Type;		
		private System.Nullable<System.DateTime> _Verification_Time;		
		private string _Verification_Status;
        private string _Site_Name;

        public VerificationCompDetailsData()
		{
		}

        [Column(Name = "[Site Name]", Storage = "_Site_Name", DbType = "VarChar(50)", CanBeNull = false)]
        public string Site_Name
        {
            get
            {
                return this._Site_Name;
            }
            set
            {
                if ((this._Site_Name != value))
                {
                    this._Site_Name = value;
                }
            }
        }

        [Column(Name = "[Serial No]", Storage = "_Serial_No", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Machine_Serial_No
        {
            get
            {
                return this._Serial_No;
            }
            set
            {
                if ((this._Serial_No != value))
                {
                    this._Serial_No = value;
                }
            }
        }

		[Column(Name="[Installation No]", Storage="_Installation_No", DbType="Int")]
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
		
		
		
		[Column(Name="[Component Type]", Storage="_Component_Type", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Component_Type
		{
			get
			{
				return this._Component_Type;
			}
			set
			{
				if ((this._Component_Type != value))
				{
					this._Component_Type = value;
				}
			}
		}
		
		[Column(Name="[Component Name]", Storage="_Component_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Component_Name
		{
			get
			{
				return this._Component_Name;
			}
			set
			{
				if ((this._Component_Name != value))
				{
					this._Component_Name = value;
				}
			}
		}
		
		[Column(Name="[Verification Type]", Storage="_Verification_Type", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string Verification_Type
		{
			get
			{
				return this._Verification_Type;
			}
			set
			{
				if ((this._Verification_Type != value))
				{
					this._Verification_Type = value;
				}
			}
		}

        [Column(Name = "[Verification Status]", Storage = "_Verification_Status", DbType = "VarChar(6) NOT NULL", CanBeNull = false)]
        public string Verification_Status
        {
            get
            {
                return this._Verification_Status;
            }
            set
            {
                if ((this._Verification_Status != value))
                {
                    this._Verification_Status = value;
                }
            }
        }


		[Column(Name="[Verification Time]", Storage="_Verification_Time", DbType="DateTime")]
		public System.Nullable<System.DateTime> Verification_Time
		{
			get
			{
				return this._Verification_Time;
			}
			set
			{
				if ((this._Verification_Time != value))
				{
					this._Verification_Time = value;
				}
			}
		}
		
	
     
	}
}
