using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.WcfHelper.Hosting;
using BMC.ExComms.Contracts.Interfaces;

namespace BMC.ExComms.Server
{
    internal class ExCommsServerCallbackHelper : WcfCallbackServerBase<IExCommsServerCallback>
    {
        internal ExCommsServerCallbackHelper(IExecutorService executor, bool logClients)
            : base(executor, logClients) { }

        protected override bool StartInternal() { return true; }

        protected override bool StopInternal() { return true; }
    }

    public partial class ExCommServerImpl
    {
        internal ExCommsServerCallbackHelper _callbackRawMessage = null;
        private static ILogger LOGGER_CALLBACKS = Log.AddFileLogger("ExComms_Callbacks");

        private void Init_CallbackHelpers()
        {
            _callbackRawMessage = new ExCommsServerCallbackHelper(this.Executor, _storeComm.LogClients);
        }

        public void Subscribe(Contracts.Dto.Freeform.ExCommsServerCallbackTypes callbackType, Contracts.Dto.SubscribeRequestEntity request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Subscribe"))
            {
                try
                {
                    method.InfoV("Subscribe : {0}", callbackType.ToString());

                    switch (callbackType)
                    {
                        case BMC.ExComms.Contracts.Dto.Freeform.ExCommsServerCallbackTypes.RawMessage:
                            _callbackRawMessage.Subscribe();
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public void Unsubscribe(Contracts.Dto.Freeform.ExCommsServerCallbackTypes callbackType, Contracts.Dto.UnsubscribeRequestEntity request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Unsubscribe"))
            {
                try
                {
                    method.InfoV("Unsubcribe : {0}", callbackType.ToString());

                    switch (callbackType)
                    {
                        case BMC.ExComms.Contracts.Dto.Freeform.ExCommsServerCallbackTypes.RawMessage:
                            _callbackRawMessage.Unsubscribe();
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }
    }
}
