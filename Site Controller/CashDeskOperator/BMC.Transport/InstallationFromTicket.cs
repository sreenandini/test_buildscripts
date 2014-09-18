using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;

namespace BMC.Transport.CashDeskOperatorEntity
{
    public partial class InstallationFromTicket
    {

        private System.Nullable<int> _installation_no;

        private string _strbarcode;

        public InstallationFromTicket()
        {
        }

        [Column(Storage = "_installation_no", DbType = "Int")]
        public System.Nullable<int> installation_no
        {
            get
            {
                return this._installation_no;
            }
            set
            {
                if ((this._installation_no != value))
                {
                    this._installation_no = value;
                }
            }
        }

        [Column(Storage = "_strbarcode", DbType = "Char(32)")]
        public string strbarcode
        {
            get
            {
                return this._strbarcode;
            }
            set
            {
                if ((this._strbarcode != value))
                {
                    this._strbarcode = value;
                }
            }
        }
    }
}