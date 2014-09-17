using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server.Handlers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class MonitorHandlerMappingAttribute : Attribute
    {
        public MonitorHandlerMappingAttribute(int faultSource, int faultType)
        {
            this.FaultSource = faultSource;
            this.FaultType = faultType;
        }

        public MonitorHandlerMappingAttribute(int faultSource, Type enumType)
            : this(faultSource, -1)
        {
            this.FaultEnumType = enumType;
        }

        public Type FaultEnumType { get; private set; }

        public int FaultSource { get; private set; }

        public int FaultType { get; private set; }
    }
}
