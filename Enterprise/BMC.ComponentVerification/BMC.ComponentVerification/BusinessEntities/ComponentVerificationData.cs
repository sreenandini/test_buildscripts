using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.ComponentVerification.BusinessEntities
{
    public class ComponentVerificationData
    {
        private System.Xml.Linq.XElement _XMLData;

        public ComponentVerificationData()
		{
		}
		
		[Column(Storage="_XMLData", DbType="Xml")]
		public System.Xml.Linq.XElement XMLData
		{
			get
			{
				return this._XMLData;
			}
			set
			{
				if ((this._XMLData != value))
				{
					this._XMLData = value;
				}
			}
		}
    }
}
