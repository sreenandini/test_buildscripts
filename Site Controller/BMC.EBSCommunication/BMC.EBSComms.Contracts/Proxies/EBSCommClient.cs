using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.WcfHelper.Helpers;
using BMC.EBSComms.Contracts.Dto.SMS2EBS;
using BMC.EBSComms.Contracts.Interfaces.SMS2EBS;

namespace BMC.EBSComms.Contracts.Proxies
{
    internal partial class EBSCommClient_13_0 :
        System.ServiceModel.ClientBase<IEBSCommClient_13_0>, IEBSCommClient_13_0, IEBSCommClient
    {
        internal EBSCommClient_13_0(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
            Log.Info("EBSCommClient_13_0.ctor() called.");
        }

        public Messages.SMS2EBS.S2SMessagePostOperationSoapOut S2SMessagePostOperation(Messages.SMS2EBS.S2SMessagePostOperationSoapIn request)
        {
            return base.Channel.S2SMessagePostOperation(request);
        }

        public string S2SMessagePostOperation(string request)
        {
            ModuleProc PROC = new ModuleProc("EBSCommClient_13_0", "S2SMessagePostOperation");
            string result = default(string);

            try
            {
                Contracts.Messages.SMS2EBS.S2SMessagePostOperationSoapIn requestMsg = new Contracts.Messages.SMS2EBS.S2SMessagePostOperationSoapIn()
                {
                    Request = new Contracts.Dto.SMS2EBS.RequestType_13_0()
                    {
                        request = request
                    }
                };

                BMC.EBSComms.Contracts.Messages.SMS2EBS.S2SMessagePostOperationSoapOut response = this.S2SMessagePostOperation(requestMsg);
                if (response != null &&
                    response.Response != null &&
                    !response.Response.response.IsEmpty())
                {
                    result = response.Response.response;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }

    internal partial class EBSCommClient_13_1 :
        System.ServiceModel.ClientBase<IEBSCommClient_13_1>, IEBSCommClient_13_1, IEBSCommClient
    {
        internal EBSCommClient_13_1(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
            Log.Info("EBSCommClient_13_1.ctor() called.");
        }

        public Messages.SMS2EBS.S2SMessagePostOperationResponse S2SMessagePostOperation(Messages.SMS2EBS.S2SMessagePostOperationRequest request)
        {
            return base.Channel.S2SMessagePostOperation(request);
        }

        public string S2SMessagePostOperation(string request)
        {
            ModuleProc PROC = new ModuleProc("EBSCommClient_13_1", "S2SMessagePostOperation");
            string result = default(string);

            try
            {
                Contracts.Messages.SMS2EBS.S2SMessagePostOperationRequest requestMsg = new Contracts.Messages.SMS2EBS.S2SMessagePostOperationRequest()
                {
                    Request = new Contracts.Dto.SMS2EBS.RequestType_13_1()
                         {
                             Request = request
                         }
                };

                BMC.EBSComms.Contracts.Messages.SMS2EBS.S2SMessagePostOperationResponse response = this.S2SMessagePostOperation(requestMsg);
                if (response != null &&
                    response.Response != null &&
                    !response.Response.Response.IsEmpty())
                {
                    result = response.Response.Response;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }

    public static class EBSCommClientFactory
    {
        public static Binding CreateBinding(bool isVersion_13_0)
        {
            if (isVersion_13_0)
                return ContractBindingsHelper.CreateWSHttpBinding();
            else
                return ContractBindingsHelper.CreateBasicHttpBinding();
        }

        public static IEBSCommClient CreateClient(bool isVersion_13_0, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress)
        {
            if (isVersion_13_0)
                return new EBSCommClient_13_0(binding, remoteAddress);
            else
                return new EBSCommClient_13_1(binding, remoteAddress);
        }
    }
}
