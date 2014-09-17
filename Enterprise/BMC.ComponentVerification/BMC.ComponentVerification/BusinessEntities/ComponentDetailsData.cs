using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.ComponentVerification.BusinessEntities
{
    public partial class AllComponentDetailsData
    {
        private string _CCT_Name;
		
		private int _CCD_ID;
		
		private string _CCD_Name;

        private string _CCD_Model_Name;

        public AllComponentDetailsData()
		{
		}
		
		[Column(Storage="_CCT_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string CCT_Name
		{
			get
			{
				return this._CCT_Name;
			}
			set
			{
				if ((this._CCT_Name != value))
				{
					this._CCT_Name = value;
				}
			}
		}
		
		[Column(Storage="_CCD_ID", DbType="Int NOT NULL")]
		public int CCD_ID
		{
			get
			{
				return this._CCD_ID;
			}
			set
			{
				if ((this._CCD_ID != value))
				{
					this._CCD_ID = value;
				}
			}
		}
		
		[Column(Storage="_CCD_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string CCD_Name
		{
			get
			{
				return this._CCD_Name;
			}
			set
			{
				if ((this._CCD_Name != value))
				{
					this._CCD_Name = value;
				}
			}
		}

        [Column(Storage = "_CCD_Model_Name", DbType = "VarChar(50)")]
        public string CCD_Model_Name
		{
			get
			{
                return this._CCD_Model_Name;
			}
			set
			{
                if ((this._CCD_Model_Name != value))
				{
                    this._CCD_Model_Name = value;
				}
			}
		}

    }

    public partial class ComponentDetailsData
    {
        private string _ccd_name;

        private string _ccd_model_name;

        private string _cct_name;

        private string _cat_name;

        private string _ccd_seed_value;
		
		private string _ccd_hash_value;
		
		public ComponentDetailsData()
		{
		}
		
		[Column(Storage="_ccd_name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ccd_name
		{
			get
			{
				return this._ccd_name;
			}
			set
			{
				if ((this._ccd_name != value))
				{
					this._ccd_name = value;
				}
			}
		}

        [Column(Storage = "_ccd_model_name", DbType = "VarChar(50)")]
        public string CCD_Model_Name
		{
			get
			{
                return this._ccd_model_name;
			}
			set
			{
                if ((this._ccd_model_name != value))
				{
                    this._ccd_model_name = value;
				}
			}
		}

        [Column(Storage = "_cct_name", DbType = "VarChar(30) NOT NULL", CanBeNull = false)]
        public string cct_name
		{
			get
			{
                return this._cct_name;
			}
			set
			{
                if ((this._cct_name != value))
				{
                    this._cct_name = value;
				}
			}
		}

        [Column(Storage = "_cat_name", DbType = "VarChar(30) NOT NULL", CanBeNull = false)]
        public string cat_name
		{
			get
			{
                return this._cat_name;
			}
			set
			{
                if ((this._cat_name != value))
				{
                    this._cat_name = value;
				}
			}
		}
		
		[Column(Storage="_ccd_seed_value", DbType="VarChar(30) NOT NULL", CanBeNull=false)]
		public string ccd_seed_value
		{
			get
			{
				return this._ccd_seed_value;
			}
			set
			{
				if ((this._ccd_seed_value != value))
				{
					this._ccd_seed_value = value;
				}
			}
		}
		
		[Column(Storage="_ccd_hash_value", DbType="VarChar(30) NOT NULL", CanBeNull=false)]
		public string ccd_hash_value
		{
			get
			{
				return this._ccd_hash_value;
			}
			set
			{
				if ((this._ccd_hash_value != value))
				{
					this._ccd_hash_value = value;
				}
			}
		}
	}

    
}
