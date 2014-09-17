using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Handlers
{
    public interface IFreeformHandler : IDisposable, ICloneable
    {
        bool ProcessMessage(FFMsg_G2H message);
        bool ProcessMessage(FFMsg_H2G message);
    }

    internal abstract class FreeformHandlerBase 
        : DisposableObject, IFreeformHandler
    {
        protected readonly ExCommsServerImpl _serverInstance = null;

        #region Single Thread Helper (MonitorProcessor)

        private readonly static SingletonThreadHelper<IExCommsMonitorProcessor> _monitorProcessorHelper = null;

        static FreeformHandlerBase()
        {
            _monitorProcessorHelper = new SingletonThreadHelper<IExCommsMonitorProcessor>(
                new Lazy<IExCommsMonitorProcessor>(() => new ExCommsMonitorProcessor(ExCommsServerImpl.Current)));
        }

        internal IExCommsMonitorProcessor MonitorProcessor
        {
            get { return _monitorProcessorHelper.Current; }
        }

        #endregion

        protected FreeformHandlerBase()
        {
            _serverInstance = ExCommsServerImpl.Current;
        }

        public bool ProcessMessage(FFMsg_G2H message)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessMessage(G2H)"))
            {
                bool result = default(bool);

                try
                {
                    result = this.ProcessMessageInternal(method, message);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        protected abstract bool ProcessMessageInternal(ILogMethod method, FFMsg_G2H message);

        public bool ProcessMessage(FFMsg_H2G message)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessMessage(H2G)"))
            {
                bool result = default(bool);

                try
                {
                    result = this.ProcessMessageInternal(method, message);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        protected abstract bool ProcessMessageInternal(ILogMethod method, FFMsg_H2G message);

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    [Serializable]
    public class FreeformHandlerAttribute : Attribute
    {
        public FreeformHandlerAttribute() { }

        public FreeformHandlerAttribute(int sessionId)
        {
            this.SessionId = sessionId;
        }

        public Type HandlerType { get; private set; }

        public int SessionId { get; private set; }
    }
}
