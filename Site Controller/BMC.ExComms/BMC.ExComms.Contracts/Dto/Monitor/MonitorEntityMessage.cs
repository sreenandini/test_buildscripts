using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    public interface IMonitorEntity_Msg
        : IMonitorEntity, ICommsEntity
    {
        List<MonitorEntity_MsgTgt> Targets { get; }

        MonitorEntity_MsgTgt PrimaryTarget { get; set; }

        string FaultSourceTypeKey { get; }

        T GetTarget<T>()
           where T : IMonitorEntity_MsgTgt;
        void GetTarget<T>(Action<T> doWork)
           where T : IMonitorEntity_MsgTgt;
        IEnumerable<T> GetTargets<T>()
           where T : MonitorEntity_MsgTgt;

        List<Type> ResponseTypes { get; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonitorEntity_Msg")]
    [ExCommsMessageKnownType]
    public class MonitorEntity_Msg
        : MonitorEntity, IMonitorEntity_Msg
    {
        private List<MonitorEntity_MsgTgt> _targets = null;
        private string _ipAddressString = string.Empty;
        private IPAddress _ipAddress = null;
        private Lazy<List<Type>> _responseTypes = null;

        public MonitorEntity_Msg()
        {
            _targets = new List<MonitorEntity_MsgTgt>();
            _ipAddressString = IPAddress.None.ToString();
            _ipAddress = IPAddress.None;
            _responseTypes = new Lazy<List<Type>>();
        }

        public List<Type> ResponseTypes
        {
            get { return _responseTypes.Value; }
        }

        [DataMember]
        public string IpAddress
        {
            get { return _ipAddressString; }
            set
            {
                _ipAddress = value.ToIPAddress();
                _ipAddressString = _ipAddress.ToString();
            }
        }

        public IPAddress IpAddress2
        {
            get { return _ipAddress; }
        }

        [DataMember]
        public string HostIpAddress { get; set; }

        [DataMember]
        public string SiteCode { get; set; }

        [DataMember]
        public DateTime FaultDate { get; set; }

        [DataMember]
        public string Asset { get; set; }

        [DataMember]
        public string CardNumber { get; set; }

        public string BarPositionNo { get; set; }

        [DataMember]
        public List<MonitorEntity_MsgTgt> Targets
        {
            get { return _targets; }
            set { _targets = value; }
        }

        public MonitorEntity_MsgTgt PrimaryTarget { get; set; }

        public virtual void AddTarget(MonitorEntity_MsgTgt target)
        {
            _targets.Add(target);
        }

        public int EntityUniqueKeyInt
        {
            get { return 0; }
        }

        public string EntityUniqueKeyString
        {
            get { return this.FaultSourceTypeKey; }
        }

        public virtual int EntityPrimaryKeyId
        {
            get { return 0; }
        }

        public ICommsUniqueEntity EntityPrimaryTarget { get; set; }

        protected bool GetBaseTargets<T>(Action<IEnumerable<T>> doWork)
            where T : IMonitorEntity_MsgTgt
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ToStringDetail"))
            {
                string typeOfT = typeof(T).FullName;

                try
                {
                    var items = (from t in this.Targets
                                 where t is T
                                 select t).OfType<T>();
                    doWork(items);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return true;
            }
        }

        protected bool GetBaseTarget<T>(Action<T> doWork)
            where T : IMonitorEntity_MsgTgt
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetBaseTarget<T>"))
            {
                string typeOfT = typeof(T).FullName;

                try
                {
                    var item = (from t in this.Targets
                                where t is T
                                select t).OfType<T>().FirstOrDefault();
                    if (item != null)
                    {
                        doWork(item);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return true;
            }
        }

        public T GetTarget<T>()
            where T : IMonitorEntity_MsgTgt
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetTarget<T>"))
            {
                T result = default(T);

                try
                {
                    this.GetBaseTarget<T>((e) =>
                    {
                        result = e;
                    });
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public void GetTarget<T>(Action<T> doWork)
            where T : IMonitorEntity_MsgTgt
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetTarget<T>"))
            {
                try
                {
                    this.GetBaseTarget<T>(doWork);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public IEnumerable<T> GetTargets<T>()
            where T : MonitorEntity_MsgTgt
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetTargets<T>"))
            {
                IEnumerable<T> result = default(IEnumerable<T>);

                try
                {
                    this.GetBaseTargets<T>((e) =>
                    {
                        result = e.ToList();
                    });
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
