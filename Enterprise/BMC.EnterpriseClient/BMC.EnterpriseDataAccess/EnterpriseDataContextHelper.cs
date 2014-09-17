using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.Utilities;
using System.Transactions;
using System.Data.SqlClient;

namespace BMC.EnterpriseDataAccess
{
    public class EnterpriseDataContextHelper
    {
        public static EnterpriseDataContext GetDataContext()
        {
            EnterpriseDataContext context = new EnterpriseDataContext(ConnectionString);
            return context;
        }

        public static string ConnectionString
        {
            get
            {
                return DatabaseHelper.GetConnectionString();
            }
        }
    }
}
