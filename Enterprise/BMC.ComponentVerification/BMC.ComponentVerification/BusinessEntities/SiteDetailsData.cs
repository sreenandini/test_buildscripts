using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.ComponentVerification.BusinessEntities
{
    public partial class SiteDetailsData
    {
        private int _Site_ID;
		
		private string _Site_Name;

        public SiteDetailsData()
		{
		}
		
		[Column(Name="[Site ID]", Storage="_Site_ID", DbType="Int NOT NULL")]
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
		
		[Column(Name="[Site Name]", Storage="_Site_Name", DbType="VarChar(50)")]
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
}
