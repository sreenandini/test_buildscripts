using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Proxies;

namespace BMC.ExMonitor.Server
{
    internal partial class ExMonitorServerImpl
        : IExMonServer4MonProcessorProxy
    {
        bool IExMonServer4MonProcessorProxy.ProcessH2GMessage(MonMsg_H2G request)
        {
            using (ILogMethod method = Log.LogMethod("IExMonServer4MonProcessorProxy", "ProcessH2GMessage"))
            {
                bool result = default(bool);

                try
                {
                    if (_monProcessorProxy == null)
                    {
                        lock (_monProcessorProxyLock)
                        {
                            if (_monProcessorProxy == null)
                            {
                                _monProcessorProxy = ExMonServer4MonProcessorProxyFactory.Get(this);
                            }
                        }
                    }

                    result = _monProcessorProxy.ProcessH2GMessage(request);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }
    }
}
