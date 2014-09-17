using BMC.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseDataAccess
{
    public class MeterAnalysisDataContextHelper
    {
        public static MeterAnalysisDataContext GetDataContext()
        {
            MeterAnalysisDataContext context = new MeterAnalysisDataContext(ConnectionString);
            return context;
        }

        public static string ConnectionString
        {
            get
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.ConnectionString = DatabaseHelper.GetConnectionString();
                builder.InitialCatalog = "MeterAnalysis";
                return builder.ConnectionString;
            }
        }
    }
}
