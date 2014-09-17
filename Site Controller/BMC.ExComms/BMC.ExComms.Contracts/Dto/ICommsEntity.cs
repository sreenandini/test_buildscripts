using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using BMC.CoreLib.Concurrent;

namespace BMC.ExComms.Contracts.DTO
{
    public interface ICommsUniqueEntity
    {
        int EntityUniqueKeyInt { get; }

        string EntityUniqueKeyString { get; }

        int EntityPrimaryKeyId { get; }

        ICommsUniqueEntity EntityPrimaryTarget { get; set; }
    }

    public interface ICommsEntity
        : IDisposable, 
        IExecutorKeyFreeThread,
        ICommsUniqueEntity
    {
        string IpAddress { get; set; }

        IPAddress IpAddress2 { get; }

        string HostIpAddress { get; set; }

        int InstallationNo { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    [Serializable]
    public class CommsEntityAttribute : Attribute
    {
        public CommsEntityAttribute() { }

        public Type Request { get; set; }

        public Type Response { get; set; }
    }
}
