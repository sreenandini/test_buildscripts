using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;
using System.Runtime.Serialization;

namespace BMC.Transport.CashDeskOperatorEntity
{
    [Serializable()] 
    public class FetchOpenServiceCallsResult
    {

        private string _LoggedDate;

        private string _DownTime;

        private string _JobID;

        private string _Call_Status;

        private string _Fault;

        private int _IsTrue;

        private bool _IsHighlighted;

        public FetchOpenServiceCallsResult()
        {
        }

        [Column(Storage = "_LoggedDate", DbType = "VarChar(20)")]
        public string LoggedDate
        {
            get
            {
                return this._LoggedDate;
            }
            set
            {
                if ((this._LoggedDate != value))
                {
                    this._LoggedDate = value;
                }
            }
        }

        [Column(Storage = "_DownTime", DbType = "VarChar(20)")]
        public string DownTime
        {
            get
            {
                return this._DownTime;
            }
            set
            {
                if ((this._DownTime != value))
                {
                    this._DownTime = value;
                }
            }
        }

        [Column(Storage = "_JobID", DbType = "VarChar(61)")]
        public string JobID
        {
            get
            {
                return this._JobID;
            }
            set
            {
                if ((this._JobID != value))
                {
                    this._JobID = value;
                }
            }
        }

        [Column(Storage = "_Call_Status", DbType = "VarChar(10) NOT NULL", CanBeNull = false)]
        public string Call_Status
        {
            get
            {
                return this._Call_Status;
            }
            set
            {
                if ((this._Call_Status != value))
                {
                    this._Call_Status = value;
                }
            }
        }

        [Column(Storage = "_Fault", DbType = "VarChar(101)")]
        public string Fault
        {
            get
            {
                return this._Fault;
            }
            set
            {
                if ((this._Fault != value))
                {
                    this._Fault = value;
                }
            }
        }

        [Column(Storage = "_IsTrue", DbType = "Int NOT NULL")]
        public int IsTrue
        {
            get
            {
                return this._IsTrue;
            }
            set
            {
                if ((this._IsTrue != value))
                {
                    this._IsTrue = value;
                }
            }
        }

        [Column(Storage = "_IsHighlighted", DbType = "Bit NOT NULL")]
        public bool IsHighlighted
        {
            get
            {
                return this._IsHighlighted;
            }
            set
            {
                if ((this._IsHighlighted != value))
                {
                    this._IsHighlighted = value;
                }
            }
        }

       
    }
}