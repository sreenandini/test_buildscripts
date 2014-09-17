using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.ComponentVerification.BusinessEntities
{
    public partial class AlgorithmTypesData
	{
		
		private int _CAT_Code;
		
		private string _CAT_Name;

        public AlgorithmTypesData()
		{
		}
		
		[Column(Storage="_CAT_Code", DbType="Int NOT NULL")]
		public int CAT_Code
		{
			get
			{
				return this._CAT_Code;
			}
			set
			{
				if ((this._CAT_Code != value))
				{
					this._CAT_Code = value;
				}
			}
		}
		
		[Column(Storage="_CAT_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string CAT_Name
		{
			get
			{
				return this._CAT_Name;
			}
			set
			{
				if ((this._CAT_Name != value))
				{
					this._CAT_Name = value;
				}
			}
		}
	}
}
