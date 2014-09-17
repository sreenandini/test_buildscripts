using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.ExceptionManagement;
using System.Windows.Forms;
using System.Threading;

namespace BMC.MeterAdjustmentTool.Helpers
{
    public static class DBServerCollection
    {
        private static IDictionary<string, string> _servers = null;
        private static object _lockServers = new object();
        private static ManualResetEvent _mreServers = new ManualResetEvent(false);

        static DBServerCollection() { }

        public static void InitializeServers()
        {
            Extensions.CreateThreadAndStart(new System.Threading.ThreadStart(InitializeServersInternal));
        }

        private static void InitializeServersInternal()
        {
            if (_servers == null)
            {
                lock (_lockServers)
                {
                    if (_servers == null)
                    {
                        _servers = new SortedDictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
                        System.ComponentModel.TypeConverter.StandardValuesCollection
                            stdValues = new SqlDataSourceConverter().GetStandardValues(null);
                        if (stdValues != null)
                        {
                            foreach (string stdValue in stdValues)
                            {
                                AddServer(stdValue);
                            }
                        }
                    }

                    _mreServers.Set();
                }
            }
        }

        public static AutoCompleteStringCollection GetServers()
        {
            _mreServers.WaitOne();
            AutoCompleteStringCollection result = new AutoCompleteStringCollection();

            try
            {
                foreach (string value in _servers.Values)
                {
                    result.Add(value);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }

        public static void AddServer(string server)
        {            
            if (_servers != null)
            {
                if (!_servers.ContainsKey(server))
                {
                    _servers.Add(server, server);
                }
            }
        }
    }
}
