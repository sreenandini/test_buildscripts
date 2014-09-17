using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.ComponentVerification.BusinessEntities
{
    public partial class MachineCompDetails
    {
        private int _Component_ID;

        private string _Component_Name;

        public MachineCompDetails()
        {
        }

        [Column(Storage = "_Component_ID", DbType = "Int NOT NULL")]
        public int Component_ID
        {
            get
            {
                return this._Component_ID;
            }
            set
            {
                if ((this._Component_ID != value))
                {
                    this._Component_ID = value;
                }
            }
        }

        [Column(Storage = "_Component_Name", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Component_Name
        {
            get
            {
                return this._Component_Name;
            }
            set
            {
                if ((this._Component_Name != value))
                {
                    this._Component_Name = value;
                }
            }
        }
    }

}
