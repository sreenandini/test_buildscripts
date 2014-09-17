using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Contracts.DTO.Monitor;

namespace BMC.ExComms.Server.Handlers
{
    public interface IFFMsgTransmitter
        : IDisposable
    {
        bool ProcessMessage(FFMsg_G2H message, IList<MonitorEntity_MsgTgt> monitorTargets);
        bool ProcessMessage(FFMsg_H2G message);
    }

    public class FFMsgTransmitter
        : DisposableObject, IFFMsgTransmitter
    {
        #region Single Thread Helper (MonitorProcessor)

        private readonly static SingletonThreadHelper<IExCommsMonitorProcessor> _monitorProcessorHelper = null;
        internal readonly ExCommsServerImpl _serverInstance = null;

        static FFMsgTransmitter()
        {
            _monitorProcessorHelper = new SingletonThreadHelper<IExCommsMonitorProcessor>(
                new Lazy<IExCommsMonitorProcessor>(() => new ExCommsMonitorProcessor(ExCommsServerImpl.Current)));
        }

        public FFMsgTransmitter()
        {
            _serverInstance = ExCommsServerImpl.Current;
        }

        internal IExCommsMonitorProcessor MonitorProcessor
        {
            get { return _monitorProcessorHelper.Current; }
        }

        #endregion

        public bool ProcessMessage(FFMsg_G2H message, IList<MonitorEntity_MsgTgt> monitorTargets)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessMessage(G2H)"))
            {
                bool result = default(bool);

                try
                {
                    result = this.OnProcessMessageInternal(message, monitorTargets);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public bool ProcessMessage(FFMsg_H2G message)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessMessage(G2H)"))
            {
                bool result = default(bool);

                try
                {
                    result = this.OnProcessMessageInternal(message);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        protected virtual bool OnProcessMessageInternal(FFMsg_G2H message, IList<MonitorEntity_MsgTgt> monitorTargets)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "OnProcessMessageInternal(G2H)"))
            {
                bool result = default(bool);

                try
                {
                    if (monitorTargets == null || monitorTargets.Count == 0) return true;

                    // convert the monitor message from freeform message
                    MonMsg_G2H monMsg = MonitorEntityFactory.CreateEntity(message, monitorTargets);
                    if (monMsg == null)
                    {
                        method.Info("Unable to convert the monitor message from freeform message.");
                        return false;
                    }

                    // post the monitor message into monitor processor
                    if (!this.MonitorProcessor.ProcessG2HMessage(monMsg))
                    {
                        method.Info("Unable to post the message to monitor processor.");
                        return false;
                    }

                    result = true;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        protected virtual bool OnProcessMessageInternal(FFMsg_H2G message)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "OnProcessMessageInternal(H2G)"))
            {
                bool result = default(bool);

                try
                {
                    if (message == null)
                    {
                        method.Info("Freeform message (H2G) was null.");
                        return false;
                    }

                    result = _serverInstance.PostMessageToTransceiver(message);
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
