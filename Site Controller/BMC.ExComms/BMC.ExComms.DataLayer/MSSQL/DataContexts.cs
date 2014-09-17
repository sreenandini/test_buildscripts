using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.Utilities;
using BMC.CoreLib;

namespace BMC.ExComms.DataLayer.MSSQL
{
    public partial class ExCommsDataContext
        : DisposableObject
    {
        private static string _connectionString = string.Empty;

        private static readonly SingletonThreadHelper<ExCommsDataContext> _singletonHelper = new SingletonThreadHelper<ExCommsDataContext>(
                new Lazy<ExCommsDataContext>(() => new ExCommsDataContext()));

        private ExCommsDataContext()
        {
        }

        public static ExCommsDataContext Current
        {
            get { return _singletonHelper.Current; }
        }

        public static string ConnectionString
        {
            get
            {
                if (_connectionString.IsNullOrEmpty())
                    _connectionString = DatabaseHelper.GetConnectionString();

                return _connectionString;
            }
        }

        public ExCommsSQLDataAccess GetDataContext()
        {
            return new ExCommsSQLDataAccess(ConnectionString);
        }
    }

    public partial class ExCommsTicketDataContext
       : DisposableObject
    {
        private static string _connectionString = string.Empty;

        private static readonly SingletonThreadHelper<ExCommsTicketDataContext> _singletonHelper = new SingletonThreadHelper<ExCommsTicketDataContext>(
                new Lazy<ExCommsTicketDataContext>(() => new ExCommsTicketDataContext()));

        private ExCommsTicketDataContext()
        {
        }

        public static ExCommsTicketDataContext Current
        {
            get { return _singletonHelper.Current; }
        }

        public static string ConnectionString
        {
            get
            {
                if (_connectionString.IsNullOrEmpty())
                    _connectionString = DatabaseHelper.GetTicketingConnectionString();

                return _connectionString;
            }
        }

        public ExCommsTicketDataAccess GetDataContext()
        {
            return new ExCommsTicketDataAccess(ConnectionString);
        }
    }
}
