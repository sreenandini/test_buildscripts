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
    public class InstallationListResult
    {

        private string _Bar_Pos_Name;

        private string _Stock_No;

        private string _Name;

        private string _Installation_Reference;

        private int _Installation_No;

        public InstallationListResult()
        {
        }

        [Column(Storage = "_Bar_Pos_Name", DbType = "VarChar(50)")]
        public string Bar_Pos_Name
        {
            get
            {
                return this._Bar_Pos_Name;
            }
            set
            {
                if ((this._Bar_Pos_Name != value))
                {
                    this._Bar_Pos_Name = value;
                }
            }
        }

        [Column(Storage = "_Stock_No", DbType = "VarChar(50)")]
        public string Stock_No
        {
            get
            {
                return this._Stock_No;
            }
            set
            {
                if ((this._Stock_No != value))
                {
                    this._Stock_No = value;
                }
            }
        }

        [Column(Storage = "_Name", DbType = "VarChar(50)")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this._Name = value;
                }
            }
        }

        [Column(Storage = "_Installation_Reference", DbType = "VarChar(50)")]
        public string Installation_Reference
        {
            get
            {
                return this._Installation_Reference;
            }
            set
            {
                if ((this._Installation_Reference != value))
                {
                    this._Installation_Reference = value;
                }
            }
        }

        [Column(Storage = "_Installation_No", DbType = "Int NOT NULL")]
        public int Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
                }
            }
        }
    }
}