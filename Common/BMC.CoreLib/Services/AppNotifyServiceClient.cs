using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.Services
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    internal partial class AppNotifyServiceClient :
        System.ServiceModel.DuplexClientBase<IAppNotifyService>, IAppNotifyService
    {
        internal AppNotifyServiceClient(System.ServiceModel.InstanceContext callbackInstance, string remoteAddress) :
            this(callbackInstance, AppNotifyServiceHelper.CreateBinding(), new EndpointAddress(remoteAddress)) { }

        private AppNotifyServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(callbackInstance, binding, remoteAddress) { }

        public bool SkipLogException { get; set; }

        public void Subscribe()
        {
            ModuleProc PROC = new ModuleProc("Subscribe", "Subscribe");

            try
            {
                base.Channel.Subscribe();
            }
            catch (Exception ex)
            {
                if (!this.SkipLogException)
                {
                    Log.Exception(PROC, ex);
                }
            }
        }

        public void Unsubscribe()
        {
            ModuleProc PROC = new ModuleProc("Subscribe", "Subscribe");

            try
            {
                base.Channel.Unsubscribe();
            }
            catch (Exception ex)
            {
                if (!this.SkipLogException)
                {
                    Log.Exception(PROC, ex);
                }
            }
        }
    }
}
