using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;

namespace BMC.MeterAdjustmentTool.Helpers
{
    public sealed class SqlDataSourceConverter : StringConverter
    {

        private StandardValuesCollection _standardValues;

        // converter classes should have public ctor
        public SqlDataSourceConverter()
        {
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            StandardValuesCollection dataSourceNames = _standardValues;
            if (null == _standardValues)
            {
                // Get the sources rowset for the SQLOLEDB enumerator                
                DataTable table = SqlClientFactory.Instance.CreateDataSourceEnumerator().GetDataSources();
                DataColumn serverName = table.Columns["ServerName"];
                DataColumn instanceName = table.Columns["InstanceName"];
                DataRowCollection rows = table.Rows;

                string[] serverNames = new string[rows.Count];
                for (int i = 0; i < serverNames.Length; ++i)
                {
                    string server = rows[i][serverName] as string;
                    string instance = rows[i][instanceName] as string;
                    if ((null == instance) || (0 == instance.Length) || ("MSSQLSERVER" == instance))
                    {
                        serverNames[i] = server;
                    }
                    else
                    {
                        serverNames[i] = server + @"\" + instance;
                    }
                }
                Array.Sort<string>(serverNames);

                // Create the standard values collection that contains the sources 
                dataSourceNames = new StandardValuesCollection(serverNames);
                _standardValues = dataSourceNames;
            }
            return dataSourceNames;
        }
    }
}
