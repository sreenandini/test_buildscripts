using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Interfaces;
using BMC.ExComms.Contracts.Proxies;
using BMC.ExMonitor.Server.Handlers;

namespace BMC.ExMonitor.Server
{
    internal partial class ExMonitorServerImpl
        : IExMonServer4MonProcessorProxy
    {
        #region Single Thread Helper (MonitorProcessorProxy)

        private SingletonThreadHelper<IExMonServer4MonProcessor> _monProcessorProxyHelper = null;

        public IExMonServer4MonProcessor MonitorProcessorProxy
        {
            get { return _monProcessorProxyHelper.Current; }
        }

        #endregion

        private void InitMonitorProcessorProxyHelper()
        {
            _monProcessorProxyHelper = new SingletonThreadHelper<IExMonServer4MonProcessor>(
                                            new Lazy<IExMonServer4MonProcessor>(() =>
                                                ExMonServer4MonProcessorProxyFactory.Get(this)));
        }

        bool IExMonServer4MonProcessorProxy.ProcessH2GMessage(MonMsg_H2G request)
        {
            using (ILogMethod method = Log.LogMethod("IExMonServer4MonProcessorProxy", "ProcessH2GMessage"))
            {
                bool result = default(bool);

                try
                {
                    result = this.MonitorProcessorProxy.ProcessH2GMessage(request);
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
