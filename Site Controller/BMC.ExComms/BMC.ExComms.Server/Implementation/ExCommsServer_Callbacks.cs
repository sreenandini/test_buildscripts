using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.WcfHelper.Hosting;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Contracts.Interfaces;
using BMC.ExComms.Server.ExecutionSteps;
using BMC.ExComms.Server.Handlers;

namespace BMC.ExComms.Server
{
    internal class ExCommsServerCallbackHelper
        : WcfCallbackServerBase<IExCommsServerCallback>
    {
        internal ExCommsServerCallbackHelper(IExecutorService executor, bool logClients)
            : base(executor, logClients) { }

        protected override bool StartInternal() { return true; }

        protected override bool StopInternal() { return true; }
    }

    internal partial class ExCommsServerImpl
    {
        internal IDictionary<ExCommsServerCallbackTypes, Lazy<ExCommsServerCallbackHelper>> _callbacks = null;
        private static ILogger LOGGER_CALLBACKS = Log.AddFileLogger("ExComms_Callbacks");

        private ExCommsServerCallbackHelper _cbExecutionStepSubscribed = null;
        private ExCommsServerCallbackHelper _cbExecutionStepChanged = null;
        private readonly ThinEventSlim _teExecutionStepSubscribed = new ThinEventSlim(false);

        private void Init_CallbackHelpers()
        {
            _callbacks = new SortedDictionary<ExCommsServerCallbackTypes, Lazy<ExCommsServerCallbackHelper>>()
            {
                { 
                    ExCommsServerCallbackTypes.RawMessage, 
                        new Lazy<ExCommsServerCallbackHelper>(() => { 
                            return new ExCommsServerCallbackHelper(this.Executor, _storeComm.LogClients); 
                        }) 
                },
                { 
                    ExCommsServerCallbackTypes.ExecutionStepSubscribed, 
                        new Lazy<ExCommsServerCallbackHelper>(() => { 
                            return (_cbExecutionStepSubscribed = new ExCommsServerCallbackHelper(this.Executor, _storeComm.LogClients));
                        }) 
                },
                { 
                    ExCommsServerCallbackTypes.ExecutionStepChanged, 
                        new Lazy<ExCommsServerCallbackHelper>(() => { 
                            return (_cbExecutionStepChanged = new ExCommsServerCallbackHelper(this.Executor, _storeComm.LogClients));
                        }) 
                },
            };
        }

        public void Subscribe(Contracts.DTO.Freeform.ExCommsServerCallbackTypes callbackType, Contracts.DTO.SubscribeRequestEntity request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Subscribe"))
            {
                try
                {
                    method.InfoV("Subscribe : {0}", callbackType.ToString());
                    _callbacks[callbackType].Value.Subscribe();
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public void Unsubscribe(Contracts.DTO.Freeform.ExCommsServerCallbackTypes callbackType, Contracts.DTO.UnsubscribeRequestEntity request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Unsubscribe"))
            {
                try
                {
                    method.InfoV("Unsubcribe : {0}", callbackType.ToString());
                    _callbacks[callbackType].Value.Unsubscribe();
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        internal void OnNotifyExecutionStepSubscribed()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "OnNotifyExecutionStepSubscribed"))
            {
                try
                {
                    if (_cbExecutionStepSubscribed.HasClients)
                    {
                        _cbExecutionStepSubscribed.InvokeCallback((i) =>
                        {
                            i.ExecutionStepSubscribed(FFMsgHandlerFactory.Current.Entities);
                        });
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        internal void OnNotifyExecutionStepChanged(ExCommsExecutionStepEntity entity)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "OnNotifyExecutionStepChanged"))
            {
                try
                {
                    if (_cbExecutionStepChanged.HasClients)
                    {
                        _cbExecutionStepChanged.InvokeCallback((i) =>
                        {
                            i.ExecutionStepChanged(entity);
                        });
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        internal bool HasStepChangedClients
        {
            get
            {
                return _cbExecutionStepChanged != null &&
                        _cbExecutionStepChanged.HasClients;
            }
        }
    }
}
