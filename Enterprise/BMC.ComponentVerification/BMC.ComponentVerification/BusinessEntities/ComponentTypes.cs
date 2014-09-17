using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.ComponentVerification.BusinessEntities
{
    public partial class ComponentTypesData
    {

        private int _CCT_Code;

        private string _CCT_Name;

        public ComponentTypesData()
        {
        }

        [Column(Storage = "_CCT_Code", DbType = "Int NOT NULL")]
        public int CCT_Code
        {
            get
            {
                return this._CCT_Code;
            }
            set
            {
                if ((this._CCT_Code != value))
                {
                    this._CCT_Code = value;
                }
            }
        }

        [Column(Storage = "_CCT_Name", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
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
    }
}
