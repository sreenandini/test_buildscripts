using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace BMC.CoreLib.WcfHelper.Behaviors
{
    public class WcfCustomBehavior : WcfCustomBehaviorBase
    {
        public WcfCustomBehavior() { }

        protected override System.ServiceModel.Description.IEndpointBehavior[] CreateEndpointBehaviors(System.ServiceModel.Description.ServiceEndpoint endpoint)
        {
            return new IEndpointBehavior[] { this };
        }

        protected override System.ServiceModel.Dispatcher.IDispatchMessageInspector[] CreateDispatchMessageInspectors(System.ServiceModel.Dispatcher.DispatchRuntime runtime)
        {
            return new IDispatchMessageInspector[] { this };
        }

        protected override IErrorHandler[] CreateErrorHandlers(ChannelDispatcher dispatcher)
        {
            return new IErrorHandler[] { this };
        }

        protected override void OnContractServiceOperation(DispatchOperation operation)
        {
            base.OnContractServiceOperation(operation);
        }
    }
}
