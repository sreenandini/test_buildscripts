
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;

namespace BMC.Transport
{
    public  class EditSetting
    {

        private string _Setting_Value;

        public EditSetting()
        {
        }

        [Column(Storage = "_Setting_Value", DbType = "VarChar(8000)")]
        public string Setting_Value
        {
            get
            {
                return this._Setting_Value;
            }
            set
            {
                if ((this._Setting_Value != value))
                {
                    this._Setting_Value = value;
                }
            }
        }
    }
}
