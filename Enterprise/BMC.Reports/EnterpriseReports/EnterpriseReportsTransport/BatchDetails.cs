using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.EnterpriseReportsTransport
{
    public partial class BatchDetails
    {

        private int _Batch_ID;

        private string _Batch_ref;

        private string _Batch_Date;

       
        [Column(Storage = "_Batch_ID", DbType = "Int NOT NULL")]
        public int Batch_ID
        {
            get
            {
                return this._Batch_ID;
            }
            set
            {
                if ((this._Batch_ID != value))
                {
                    this._Batch_ID = value;
                }
            }
        }

        [Column(Storage = "_Batch_ref", DbType = "VarChar(50)")]
        public string Batch_ref
        {
            get
            {
                return this._Batch_ref;
            }
            set
            {
                if ((this._Batch_ref != value))
                {
                    this._Batch_ref = value;
                }
            }
        }

        [Column(Storage = "_Batch_Date", DbType = "VarChar(30)")]
        public string Batch_Date
        {
            get
            {
                return this._Batch_Date;
            }
            set
            {
                if ((this._Batch_Date != value))
                {
                    this._Batch_Date = value;
                }
            }
        }
    }
}
