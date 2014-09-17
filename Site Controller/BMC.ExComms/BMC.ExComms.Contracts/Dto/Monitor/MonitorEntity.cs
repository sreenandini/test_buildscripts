using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    public interface IMonitorEntity
        : IDisposable, ICloneable, IExecutorKey
    {
        int InstallationNo { get; set; }

        int FaultSource { get; set; }

        int FaultType { get; set; }

        object Extra { get; set; }

        object ExtraAttribute { get; set; }

        string FaultSourceTypeKey { get; }

        string TypeFaultSourceTypeKey { get; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonitorEntity")]
    [ExCommsMessageKnownType]
    public class MonitorEntity : DisposableObject, IMonitorEntity
    {
        public MonitorEntity() { }

        [DataMember]
        public int InstallationNo { get; set; }

        [DataMember]
        public int FaultSource { get; set; }

        [DataMember]
        public int FaultType { get; set; }

        public object Extra { get; set; }

        public object ExtraAttribute { get; set; }

        public T GetExtra<T>()
        {
            if (typeof(T).IsAssignableFrom(this.Extra.GetType()))
                return (T)this.Extra;
            return default(T);
        }

        public string FaultSourceTypeKey
        {
            get { return string.Format("{0:D}_{1:D}", this.FaultSource, this.FaultType); }
        }

        public string TypeFaultSourceTypeKey
        {
            get { return this.GetType().Name + " (" + this.FaultSourceTypeKey + ")"; }
        }

        public virtual string UniqueKey
        {
            get
            {
                return this.InstallationNo.ToString();
            }
        }

        public override string ToString()
        {
            return this.GetType().Name + " (" + this.FaultSourceTypeKey + ")";
        }
    }
}
