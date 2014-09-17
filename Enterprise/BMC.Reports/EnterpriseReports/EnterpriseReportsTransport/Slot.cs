using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.EnterpriseReportsTransport
{
    public partial class Slot
    {

        private string _Machine_Stock_No;

        public Slot()
        {
        }

        [Column(Storage = "_Machine_Stock_No", DbType = "VarChar(50)")]
        public string Machine_Stock_No
        {
            get
            {
                return this._Machine_Stock_No;
            }
            set
            {
                if ((this._Machine_Stock_No != value))
                {
                    this._Machine_Stock_No = value;
                }
            }
        }
    }

    public class SProcResult
    {

        private string _RESULT;

        [Column(Storage = "_RESULT", DbType = "VarChar(8) NOT NULL", CanBeNull = false)]
        public string Result
        {
            get
            {
                return _RESULT;
            }
            set
            {
                if ((_RESULT != value))
                {
                    _RESULT = value;
                }
            }
        }
    }
}
