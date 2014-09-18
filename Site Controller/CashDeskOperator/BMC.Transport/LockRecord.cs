using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.Transport
{
    [DataContract()]
    public class LockRecords
    {

        private string _User_ID;

        private string _Machine;

        private System.Nullable<System.DateTime> _Created;

        public LockRecords()
        {
        }

        [Column(Storage = "_User_ID", DbType = "VarChar(50)")]
        [DataMember(Order = 1)]
        public string User_ID
        {
            get
            {
                return this._User_ID;
            }
            set
            {
                if ((this._User_ID != value))
                {
                    this._User_ID = value;
                }
            }
        }

        [Column(Storage = "_Machine", DbType = "VarChar(50)")]
        [DataMember(Order = 2)]
        public string Machine
        {
            get
            {
                return this._Machine;
            }
            set
            {
                if ((this._Machine != value))
                {
                    this._Machine = value;
                }
            }
        }

        [Column(Storage = "_Created", DbType = "DateTime")]
        [DataMember(Order = 3)]
        public System.Nullable<System.DateTime> Created
        {
            get
            {
                return this._Created;
            }
            set
            {
                if ((this._Created != value))
                {
                    this._Created = value;
                }
            }
        }
    }
}
