using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Collections;
using BMC.ExComms.Contracts.Configuration;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Contracts.DTO.Monitor;

namespace BMC.ExComms.Server.Handlers
{
    public enum FFTgtHandlerDeviceTypes
    {
        GMU = 0,
        Simulator = 1,
    }

    public abstract class FFTgtHandlerAttribute
        : Attribute
    {
        public FFTgtHandlerAttribute()
        {
            this.DeviceType = FFTgtHandlerDeviceTypes.GMU;
        }

        public FFTgtHandlerDeviceTypes DeviceType { get; set; }
        public int SessionId { get; set; }
        public Type[] Request { get; set; }
        public Type[] Response { get; set; }
    }

    public abstract class FFTgtHandler_ExternalAttribute
        : FFTgtHandlerAttribute
    {
        public FFTgtHandler_ExternalAttribute()
        {
            this.DeviceType = FFTgtHandlerDeviceTypes.GMU;
        }
    }

    public abstract class FFTgtHandler_InternalAttribute
        : FFTgtHandlerAttribute
    {
        public FFTgtHandler_InternalAttribute()
        {
            this.DeviceType = FFTgtHandlerDeviceTypes.GMU;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class FFTgtHandler_External_GMUAttribute
        : FFTgtHandler_ExternalAttribute
    {
        public FFTgtHandler_External_GMUAttribute()
        {
            this.DeviceType = FFTgtHandlerDeviceTypes.GMU;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class FFTgtHandler_Internal_GMUAttribute
        : FFTgtHandler_InternalAttribute
    {
        public FFTgtHandler_Internal_GMUAttribute()
        {
            this.DeviceType = FFTgtHandlerDeviceTypes.GMU;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class FFTgtHandler_External_SIMAttribute
        : FFTgtHandler_ExternalAttribute
    {
        public FFTgtHandler_External_SIMAttribute()
        {
            this.DeviceType = FFTgtHandlerDeviceTypes.Simulator;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class FFTgtHandler_Internal_SIMAttribute
        : FFTgtHandler_InternalAttribute
    {
        public FFTgtHandler_Internal_SIMAttribute()
        {
            this.DeviceType = FFTgtHandlerDeviceTypes.Simulator;
        }
    }

    public sealed class FFTgtResponses
        : Lazy<FreeformEntityList>
    {
        public FFTgtResponses()
            : base(() =>
            {
                return new FreeformEntityList();
            }) { }
    }

    interface IFFTgtHandler
        : IDisposable
    {
        _FFMsgHandlerFactory MessageHandlerFactory { get; set; }
        bool Execute(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target);
    }

    public sealed class FFTgtExecutionContext
        : DisposableObject
    {
        private Lazy<List<IFreeformEntity>> _freeformTargets = null;
        private Lazy<List<MonitorEntity_MsgTgt>> _monitorTargets = null;
        private Lazy<MonTgt_G2H_Meters> _monitorMeters = null;

        public FFTgtExecutionContext(IFreeformEntity_Msg sourceMessage)
        {
            this.SourceMessage = sourceMessage;
            this.FlowDirection = this.SourceMessage.FlowDirection;
            _freeformTargets = new Lazy<List<IFreeformEntity>>(() =>
            {
                return new List<IFreeformEntity>();
            });
            _monitorTargets = new Lazy<List<MonitorEntity_MsgTgt>>(() =>
            {
                return new List<MonitorEntity_MsgTgt>();
            });
            _monitorMeters = new Lazy<MonTgt_G2H_Meters>(() =>
            {
                return new MonTgt_G2H_Meters();
            });
        }

        public IFreeformEntity_Msg SourceMessage { get; private set; }

        public IFreeformEntity_MsgTgt SourceTarget { get; set; }

        public List<IFreeformEntity> FreeformTargets
        {
            get { return _freeformTargets.Value; }
        }

        public Lazy<List<MonitorEntity_MsgTgt>> MonitorTargets
        {
            get { return _monitorTargets; }
        }

        public Lazy<MonTgt_G2H_Meters> MonitorMeters
        {
            get { return _monitorMeters; }
        }

        public FFTgtHandlerAttribute HandlerAttribute { get; set; }

        public FF_FlowDirection FlowDirection { get; private set; }
    }

    internal abstract class FFTgtHandler
        : DisposableObject, IFFTgtHandler
    {
        private delegate bool _ProcessMessageHandler(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target);
        protected static readonly IExCommsServerConfigStore _configStore = ExCommsServerConfigStoreFactory.Store;

        private readonly IDictionary<string, _ProcessMessageHandler> _processMessagesG2H = null;
        private readonly IDictionary<string, _ProcessMessageHandler> _processMessagesH2G = null;

        public FFTgtHandler()
        {
            _processMessagesG2H = new StringDictionary<_ProcessMessageHandler>()
            {
                { typeof(FFTgtHandler_External_GMUAttribute).Name, this.OnProcessMessageG2HExternal_GMU },
                { typeof(FFTgtHandler_External_SIMAttribute).Name, this.OnProcessMessageG2HExternal_SIM },
                { typeof(FFTgtHandler_Internal_GMUAttribute).Name, this.OnProcessMessageG2HInternal_GMU },
                { typeof(FFTgtHandler_Internal_SIMAttribute).Name, this.OnProcessMessageG2HInternal_SIM },
            };
            _processMessagesH2G = new StringDictionary<_ProcessMessageHandler>()
            {
                { typeof(FFTgtHandler_External_GMUAttribute).Name, this.OnProcessMessageH2GExternal_GMU },
                { typeof(FFTgtHandler_External_SIMAttribute).Name, this.OnProcessMessageH2GExternal_SIM },
                { typeof(FFTgtHandler_Internal_GMUAttribute).Name, this.OnProcessMessageH2GInternal_GMU },
                { typeof(FFTgtHandler_Internal_SIMAttribute).Name, this.OnProcessMessageH2GInternal_SIM },
            };
        }

        public _FFMsgHandlerFactory MessageHandlerFactory { get; set; }

        public bool Execute(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Execute"))
            {
                bool result = default(bool);

                try
                {
                    result = this.OnExecuteInternal(context, target);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        protected virtual bool OnExecuteInternal(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            if (context.HandlerAttribute != null)
            {
                if (context.FlowDirection == FF_FlowDirection.G2H)
                {
                    return _processMessagesG2H[context.HandlerAttribute.GetType().Name](context, target);
                }
                else if (context.FlowDirection == FF_FlowDirection.H2G)
                {
                    return _processMessagesH2G[context.HandlerAttribute.GetType().Name](context, target);
                }
            }
            return false;
        }

        protected virtual bool OnProcessMessageG2HInternal_GMU(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return true;
        }

        protected virtual bool OnProcessMessageG2HExternal_GMU(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            MonitorEntity_MsgTgt monitorTarget = MonitorEntityFactory.CreateTargetEntity(target);
            if (monitorTarget != null)
            {
                context.MonitorTargets.Value.Add(monitorTarget);
            }
            return true;
        }

        protected virtual bool OnProcessMessageH2GInternal_GMU(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return true;
        }

        protected virtual bool OnProcessMessageH2GExternal_GMU(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return true;
        }

        protected virtual bool OnProcessMessageG2HInternal_SIM(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return true;
        }

        protected virtual bool OnProcessMessageG2HExternal_SIM(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return true;
        }

        protected virtual bool OnProcessMessageH2GInternal_SIM(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return true;
        }

        protected virtual bool OnProcessMessageH2GExternal_SIM(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return true;
        }

        public int TransactionId
        {
            get { return FFMsgHandlerFactory.TransactionId; }
        }

        public int NewTransactionId
        {
            get { return FFMsgHandlerFactory.NewTransactionId; }
        }
    }
}
