using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.ComponentVerification.BusinessEntities
{
    public partial class VerificationTypesData
    {
        private int _CVT_Code;
		
		private string _CVT_Name;

        public VerificationTypesData()
		{
		}

        [Column(Storage = "_CVT_Code", DbType = "Int NOT NULL")]
		public int CVT_Code
		{
			get
			{
                return this._CVT_Code;
			}
			set
			{
                if ((this._CVT_Code != value))
				{
                    this._CVT_Code = value;
				}
			}
		}
		
		[Column(Storage="_CVT_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string CVT_Name
		{
			get
			{
				return this._CVT_Name;
			}
			set
			{
				if ((this._CVT_Name != value))
				{
					this._CVT_Name = value;
				}
			}
		}
    }
}
