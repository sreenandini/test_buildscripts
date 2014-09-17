using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Configuration;
using BMC.CoreLib.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.EBSComms.Contracts.Configuration
{
    public interface IEBSCommServerConfigStore : IEBSConfigStore
    {
        bool LogRawMessages { get; set; }

        int EBSCommServerHttpPort { get; set; }
    }

    public static class EBSCommServerConfigStoreFactory
    {
        private static object _instanceLock = new object();
        private static IEBSCommServerConfigStore _instance = null;

        public static IEBSCommServerConfigStore Store
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new EBSCommServerConfigStore();
                        }
                    }
                }
                return _instance;

            }
        }
    }
}
