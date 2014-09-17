using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.ComponentVerification.BusinessEntities
{
    public partial class MachineDetailsData
    {
        private string _Machine_Manufacturers_Serial_No;

        public MachineDetailsData()
		{
		}
		
		[Column(Storage="_Machine_Manufacturers_Serial_No", DbType="VarChar(50)")]
		public string Machine_Manufacturers_Serial_No
		{
			get
			{
				return this._Machine_Manufacturers_Serial_No;
			}
			set
			{
				if ((this._Machine_Manufacturers_Serial_No != value))
				{
					this._Machine_Manufacturers_Serial_No = value;
				}
			}
		}
    }
}
