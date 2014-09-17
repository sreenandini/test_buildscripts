using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseBusiness.Entities;

namespace BMC.EnterpriseBusiness.Business
{
    public partial class OperatorEntity
    {

        private int _Operator_ID;
		
		private string _Operator_Name;
		
		 private IList<DepotEntity> _depots;

        private DepotEntity _selectedDepot = null;

        public DepotEntity SelectedDepot
        {
            get { return _selectedDepot; }
            set { _selectedDepot = value; }
        }

        public IList<DepotEntity> Depots
        {
            get { return _depots; }
            set
            {
                if ((this._depots != value))
                {
                    this._depots = value;
                }
            }
        }


		private System.Nullable<int> _Calendar_ID;

		private string _Operator_Address;
		
		private string _Operator_PostCode;
		
		private string _Operator_Depot_Phone;
		
		private string _Operator_Fax;
		
		private string _Operator_EMail;
		
		private string _Operator_Contact;
		
		private string _Operator_Invoice_Address;
		
		private string _Operator_Invoice_Postcode;
		
		private string _Operator_Invoice_Name;
		
		private string _Operator_Start_Date;
		
		private string _Operator_End_Date;
		
		private string _Operator_AMEDIS_Code;
		
		private string _Operator_Logo_Reference;
		
		private string _Operator_Account_Name;
		
		private string _Operator_Sort_Code;
		
		private string _Operator_Account_No;

        public OperatorEntity()
		{
            _depots = new List<DepotEntity>();
		}
		
		public int Operator_ID
		{
			get
			{
				return this._Operator_ID;
			}
			set
			{
				if ((this._Operator_ID != value))
				{
					this._Operator_ID = value;
				}
			}
		}
		
		public string Operator_Name
		{
			get
			{
				return this._Operator_Name;
			}
			set
			{
				if ((this._Operator_Name != value))
				{
					this._Operator_Name = value;
				}
			}
		}
	
		public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            {
                if (sb.Length > 0) sb.Append(", ");
                sb.Append("ID : " + _Operator_ID);
            }
            if (!string.IsNullOrEmpty(_Operator_Name))
            {
                if (sb.Length > 0) sb.Append(", ");
                sb.Append("Name : " + _Operator_Name);
            }
            return sb.ToString();
        }
		
		public System.Nullable<int> Calendar_ID
		{
			get
			{
				return this._Calendar_ID;
			}
			set
			{
				if ((this._Calendar_ID != value))
				{
					this._Calendar_ID = value;
				}
			}
		}

		public string Operator_Address
		{
			get
			{
				return this._Operator_Address;
			}
			set
			{
				if ((this._Operator_Address != value))
				{
					this._Operator_Address = value;
				}
			}
		}
		
		public string Operator_PostCode
		{
			get
			{
				return this._Operator_PostCode;
			}
			set
			{
				if ((this._Operator_PostCode != value))
				{
					this._Operator_PostCode = value;
				}
			}
		}
		
		public string Operator_Depot_Phone
		{
			get
			{
				return this._Operator_Depot_Phone;
			}
			set
			{
				if ((this._Operator_Depot_Phone != value))
				{
					this._Operator_Depot_Phone = value;
				}
			}
		}
		
		public string Operator_Fax
		{
			get
			{
				return this._Operator_Fax;
			}
			set
			{
				if ((this._Operator_Fax != value))
				{
					this._Operator_Fax = value;
				}
			}
		}
		
		public string Operator_EMail
		{
			get
			{
				return this._Operator_EMail;
			}
			set
			{
				if ((this._Operator_EMail != value))
				{
					this._Operator_EMail = value;
				}
			}
		}
		
		public string Operator_Contact
		{
			get
			{
				return this._Operator_Contact;
			}
			set
			{
				if ((this._Operator_Contact != value))
				{
					this._Operator_Contact = value;
				}
			}
		}
		
		public string Operator_Invoice_Address
		{
			get
			{
				return this._Operator_Invoice_Address;
			}
			set
			{
				if ((this._Operator_Invoice_Address != value))
				{
					this._Operator_Invoice_Address = value;
				}
			}
		}
		
		public string Operator_Invoice_Postcode
		{
			get
			{
				return this._Operator_Invoice_Postcode;
			}
			set
			{
				if ((this._Operator_Invoice_Postcode != value))
				{
					this._Operator_Invoice_Postcode = value;
				}
			}
		}
		
		public string Operator_Invoice_Name
		{
			get
			{
				return this._Operator_Invoice_Name;
			}
			set
			{
				if ((this._Operator_Invoice_Name != value))
				{
					this._Operator_Invoice_Name = value;
				}
			}
		}
		
		public string Operator_Start_Date
		{
			get
			{
				return this._Operator_Start_Date;
			}
			set
			{
				if ((this._Operator_Start_Date != value))
				{
					this._Operator_Start_Date = value;
				}
			}
		}
		
		public string Operator_End_Date
		{
			get
			{
				return this._Operator_End_Date;
			}
			set
			{
				if ((this._Operator_End_Date != value))
				{
					this._Operator_End_Date = value;
				}
			}
		}
		
		public string Operator_AMEDIS_Code
		{
			get
			{
				return this._Operator_AMEDIS_Code;
			}
			set
			{
				if ((this._Operator_AMEDIS_Code != value))
				{
					this._Operator_AMEDIS_Code = value;
				}
			}
		}
		
		public string Operator_Logo_Reference
		{
			get
			{
				return this._Operator_Logo_Reference;
			}
			set
			{
				if ((this._Operator_Logo_Reference != value))
				{
					this._Operator_Logo_Reference = value;
				}
			}
		}
		
		public string Operator_Account_Name
		{
			get
			{
				return this._Operator_Account_Name;
			}
			set
			{
				if ((this._Operator_Account_Name != value))
				{
					this._Operator_Account_Name = value;
				}
			}
		}
		
		public string Operator_Sort_Code
		{
			get
			{
				return this._Operator_Sort_Code;
			}
			set
			{
				if ((this._Operator_Sort_Code != value))
				{
					this._Operator_Sort_Code = value;
				}
			}
		}
		
		public string Operator_Account_No
		{
			get
			{
				return this._Operator_Account_No;
			}
			set
			{
				if ((this._Operator_Account_No != value))
				{
					this._Operator_Account_No = value;
				}
			}
		}
    }
}