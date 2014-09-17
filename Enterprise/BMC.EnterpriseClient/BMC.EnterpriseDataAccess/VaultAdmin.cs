using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    partial class  EnterpriseDataContext
    {
        [Function(Name="dbo.rsp_Vault_GetAllSites")]
	    public ISingleResult<rsp_Vault_GetAllSitesResult> Vault_GetAllSites([Parameter(Name="User_id", DbType="Int")] System.Nullable<int> user_id)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), user_id);
			return ((ISingleResult<rsp_Vault_GetAllSitesResult>)(result.ReturnValue));
		}
        [Function(Name = "dbo.usp_Vault_UpdateDevice")]
        public ISingleResult<usp_Vault_UpdateDeviceResult> Vault_UpdateDevice([Parameter(Name = "Vault_ID", DbType = "Int")] System.Nullable<int> vault_ID, [Parameter(Name = "Name", DbType = "VarChar(150)")] string name, [Parameter(Name = "Serial_NO", DbType = "VarChar(30)")] string serial_NO, [Parameter(Name = "Active", DbType = "Bit")] System.Nullable<bool> active, [Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID, [Parameter(Name = "Alert_Level", DbType = "Int")] System.Nullable<int> alert_Level, [Parameter(Name = "User_ID", DbType = "Int")] System.Nullable<int> user_ID, [Parameter(Name = "Module_ID", DbType = "Int")] System.Nullable<int> module_ID, [Parameter(Name = "Module_Name", DbType = "VarChar(150)")] string module_Name, [Parameter(Name = "Screen_Name", DbType = "VarChar(150)")] string screen_Name, [Parameter(Name = "Manufacturer_ID", DbType = "Int")] int Manufacturer_ID, [Parameter(Name = "@Type_Prefix", DbType = "VarChar(150)")]  string Type_Prefix, [Parameter(Name = "@Capacity", DbType = "Decimal(15,2)")]  decimal Capacity)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), vault_ID, name, serial_NO, active, site_ID, alert_Level, user_ID, module_ID, module_Name, screen_Name, Manufacturer_ID, Type_Prefix,Capacity);
            return ((ISingleResult<usp_Vault_UpdateDeviceResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_Vault_GetVaultDetails")]
        public ISingleResult<rsp_Vault_GetVaultDetailsResult> Vault_GetVaultDetails([Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> Site_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), Site_ID);
            return ((ISingleResult<rsp_Vault_GetVaultDetailsResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_Vault_GetAllManufacturers")]
        public ISingleResult<rsp_Vault_GetAllManufacturersResult> rsp_Vault_GetAllManufacturers()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_Vault_GetAllManufacturersResult>)(result.ReturnValue));
        }

	}
	
	public partial class rsp_Vault_GetAllSitesResult
	{
		
		private int _Site_ID;
		
		private string _Site_Name;
		
		public rsp_Vault_GetAllSitesResult()
		{
		}
		
		[Column(Storage="_Site_ID", DbType="Int NOT NULL")]
		public int Site_ID
		{
			get
			{
				return this._Site_ID;
			}
			set
			{
				if ((this._Site_ID != value))
				{
					this._Site_ID = value;
				}
			}
		}
		
		[Column(Storage="_Site_Name", DbType="VarChar(50)")]
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
	}
    public partial class usp_Vault_UpdateDeviceResult
    {

        private System.Nullable<int> _Vault_ID;

        public usp_Vault_UpdateDeviceResult()
        {
        }

        [Column(Storage = "_Vault_ID", DbType = "Int")]
        public System.Nullable<int> Vault_ID
        {
            get
            {
                return this._Vault_ID;
            }
            set
            {
                if ((this._Vault_ID != value))
                {
                    this._Vault_ID = value;
                }
            }
        }
    }
    public partial class rsp_Vault_GetVaultDetailsResult
    {

        private int _Vault_ID;

        private string _NAME;

        private string _Serial_NO;

        private System.Nullable<int> _Site_ID;

        private System.Nullable<int> _Alert_Level;

        private System.Nullable<System.DateTime> _Created_Date;

        private System.Nullable<System.DateTime> _End_Date;

        private bool _Active;

        string _Type_Prefix;

        int _Manufacturer_ID;

        decimal _Capacity;

        public rsp_Vault_GetVaultDetailsResult()
        {
        }
        [Column(Storage = "_Capacity", DbType = "Decimal(15,2)")]
        public decimal Capacity
        {
            get
            {
                return this._Capacity;
            }
            set
            {

                this._Capacity = value;
            }
        }

        [Column(Storage = "_Vault_ID", DbType = "Int NOT NULL")]
        public int Vault_ID
        {
            get
            {
                return this._Vault_ID;
            }
            set
            {
                if ((this._Vault_ID != value))
                {
                    this._Vault_ID = value;
                }
            }
        }

        [Column(Storage = "_NAME", DbType = "VarChar(150)")]
        public string NAME
        {
            get
            {
                return this._NAME;
            }
            set
            {
                if ((this._NAME != value))
                {
                    this._NAME = value;
                }
            }
        }

        [Column(Storage = "_Serial_NO", DbType = "VarChar(30)")]
        public string Serial_NO
        {
            get
            {
                return this._Serial_NO;
            }
            set
            {
                if ((this._Serial_NO != value))
                {
                    this._Serial_NO = value;
                }
            }
        }
        
        [Column(Storage = "_Active", DbType = "Bit")]
        public bool Active
        {
            get
            {
                return this._Active;
            }
            set
            {
                if ((this._Active != value))
                {
                    this._Active = value;
                }
            }
        }

        [Column(Storage = "_Site_ID", DbType = "Int")]
        public System.Nullable<int> Site_ID
        {
            get
            {
                return this._Site_ID;
            }
            set
            {
                if ((this._Site_ID != value))
                {
                    this._Site_ID = value;
                }
            }
        }

        [Column(Storage = "_Alert_Level", DbType = "Int")]
        public System.Nullable<int> Alert_Level
        {
            get
            {
                return this._Alert_Level;
            }
            set
            {
                if ((this._Alert_Level != value))
                {
                    this._Alert_Level = value;
                }
            }
        }

        [Column(Storage = "_Created_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Created_Date
        {
            get
            {
                return this._Created_Date;
            }
            set
            {
                if ((this._Created_Date != value))
                {
                    this._Created_Date = value;
                }
            }
        }

        [Column(Storage = "_End_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> End_Date
        {
            get
            {
                return this._End_Date;
            }
            set
            {
                if ((this._End_Date != value))
                {
                    this._End_Date = value;
                }
            }
        }
        [Column(Storage = "_Manufacturer_ID", DbType = "Int")]
        public int Manufacturer_ID
        {
            get
            {
                return this._Manufacturer_ID;
            }
            set
            {

                this._Manufacturer_ID = value;
            }
        }
        [Column(Storage = "_Type_Prefix", DbType = "VarChar(10)")]
        public string Type_Prefix
        {
            get
            {
                return this._Type_Prefix;
            }
            set
            {

                this._Type_Prefix = value;
            }
        }

    }
    public partial class rsp_Vault_GetAllManufacturersResult
    {

        private int _Manufacturer_ID;

        private string _Manufacturer_Name;

        public rsp_Vault_GetAllManufacturersResult()
        {
        }

        [Column(Storage = "_Manufacturer_ID", DbType = "Int NOT NULL")]
        public int Manufacturer_ID
        {
            get
            {
                return this._Manufacturer_ID;
            }
            set
            {
                if ((this._Manufacturer_ID != value))
                {
                    this._Manufacturer_ID = value;
                }
            }
        }

        [Column(Storage = "_Manufacturer_Name", DbType = "VarChar(50)")]
        public string Manufacturer_Name
        {
            get
            {
                return this._Manufacturer_Name;
            }
            set
            {
                if ((this._Manufacturer_Name != value))
                {
                    this._Manufacturer_Name = value;
                }
            }
        }
    }

}
