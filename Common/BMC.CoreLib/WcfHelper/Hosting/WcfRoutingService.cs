using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Configuration;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.WcfHelper.Hosting
{
    [ServiceContract(Namespace = "BMC.CoreLib.WcfHelper.Hosting",
        Name = "WcfRoutingService")]
    public interface IWcfRoutingService
    {
        [OperationContract(Action = "*", ReplyAction = "*")]
        Message ProcessMessaage(Message request);
    }

    public interface IWcfRouteTargetFinder : IDisposable
    {
        EndpointAddress GetAddress(Message request);
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ValidateMustUnderstand = false)]
    public abstract class WcfRoutingService : DisposableObject, IWcfRoutingService
    {
        static IChannelFactory<IRequestChannel> _factory = null;

        static WcfRoutingService()
        {
            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            _factory = binding.BuildChannelFactory<IRequestChannel>();
            _factory.Open();
        }

        #region IRouteWebServer Members

        public abstract IWcfRouteTargetFinder TargetFinder { get; }

        public Message ProcessMessaage(Message request)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ProcessMessage");
            Message result = default(Message);
            IRequestChannel channel = null;

            try
            {
                channel = _factory.CreateChannel(this.TargetFinder.GetAddress(request));
                channel.Open();
                result = channel.Request(request);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                if (channel != null && channel.State == CommunicationState.Opened)
                {
                    channel.Close();
                }
            }

            return result;
        }

        #endregion
    }
}
