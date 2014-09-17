using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExComms.Contracts.Hosting
{
    public enum ExCommsHostingServerType
    {
        CommunicationServer = 0,
        MonitorServer = 1,
    }

    [Flags]
    public enum ExCommsHostingModuleType
    {
        CommunicationServer = 2,
        MonitorServer4CommsServer = 4,
        MonitorServer4MonProcessor = 8,
    }

    public class ExCommsHostingModuleTypeHelper
        : DisposableObject, IEquatable<ExCommsHostingModuleTypeHelper>
    {
        private ExCommsHostingModuleType _moduleType = ExCommsHostingModuleType.CommunicationServer;
        private bool _hasCommServer = false;
        private bool _hasMonitorServer4CommsServer = false;
        private bool _hasMonitorServer4MonProcessor = false;

        #region Single Thread Helper (Current)

        private readonly static SingletonHelper<ExCommsHostingModuleTypeHelper> _currentHelper = new SingletonHelper<ExCommsHostingModuleTypeHelper>(
                                    new Lazy<ExCommsHostingModuleTypeHelper>(() => new ExCommsHostingModuleTypeHelper()));

        public static ExCommsHostingModuleTypeHelper Current
        {
            get { return _currentHelper.Current; }
        }

        #endregion

        private ExCommsHostingModuleTypeHelper()
        {
        }

        public ExCommsHostingModuleTypeHelper(ExCommsHostingModuleType moduleType)
        {
            this.ModuleType = moduleType;
        }

        public void SetAll()
        {
            this.ModuleType = ExCommsHostingModuleType.CommunicationServer |
                                ExCommsHostingModuleType.MonitorServer4CommsServer |
                                ExCommsHostingModuleType.MonitorServer4MonProcessor;
        }

        public ExCommsHostingModuleType ModuleType
        {
            get { return _moduleType; }
            set
            {
                _moduleType = value;
                _hasCommServer = ((value & ExCommsHostingModuleType.CommunicationServer) == ExCommsHostingModuleType.CommunicationServer);
                _hasMonitorServer4CommsServer = ((value & ExCommsHostingModuleType.MonitorServer4CommsServer) == ExCommsHostingModuleType.MonitorServer4CommsServer);
                _hasMonitorServer4MonProcessor = ((value & ExCommsHostingModuleType.MonitorServer4MonProcessor) == ExCommsHostingModuleType.MonitorServer4MonProcessor);
            }
        }

        public bool HasCommServer
        {
            get { return _hasCommServer; }
        }

        public bool HasMonitorServer4CommsServer
        {
            get { return _hasMonitorServer4CommsServer; }
        }

        public bool HasMonitorServer4MonProcessor
        {
            get { return _hasMonitorServer4MonProcessor; }
        }

        public bool Equals(ExCommsHostingModuleTypeHelper other)
        {
            return ((this.HasCommServer == other.HasCommServer) ||
                    ((this.HasMonitorServer4CommsServer == other.HasMonitorServer4CommsServer) ||
                    (this.HasMonitorServer4MonProcessor == other.HasMonitorServer4MonProcessor)));
        }
    }
}
