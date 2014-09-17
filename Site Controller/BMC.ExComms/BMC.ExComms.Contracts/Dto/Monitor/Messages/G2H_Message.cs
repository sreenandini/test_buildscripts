using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    public interface IMonMsg_G2H
        : IMonitorEntity_Msg
    {
        MonTgt_G2H_Meters Meters { get; }

        MonTgt_G2H_GameInfo GameInfo { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonMsg_G2H")]
    [ExCommsMessageKnownType]
    public class MonMsg_G2H
        : MonitorEntity_Msg, IMonTgt_G2H
    {
        //private readonly Lazy<MonTgt_G2H_Meters> _meters = null;
        private MonTgt_G2H_Meters _meters = null;
        private bool _hasMeters = false;

        public MonMsg_G2H()
        {
            //_meters = new Lazy<MonTgt_G2H_Meters>(() =>
            //{
            //    _hasMeters = true;
            //    MonTgt_G2H_Meters meters = new MonTgt_G2H_Meters();
            //    this.Targets.Add(meters);
            //    return meters;
            //});
        }

        public MonTgt_G2H_Meters Meters
        {
            get { return _meters; }
            set { _meters = value; }
        }

        public long GetMeterValue(MonTgt_G2H_Meter_MeterType meterType)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetMeterValue"))
            {
                long result = default(long);

                try
                {
                    result = _meters.Meters.GetMeterValue(meterType);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public bool HasMeters
        {
            get { return _hasMeters; }
        }

        public MonTgt_G2H_GameInfo GameInfo { get; set; }

        public override string ToString()
        {
            return "MonMsg_G2H_" + this.FaultSourceTypeKey;
        }
    }
}
