using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;

namespace BMC.CoreLib.WcfHelper.Bindings
{
    public static class ServiceBindingMapper
    {
        public static Binding CreateBinding(string bindingName)
        {
            Binding result = null;

            try
            {
                if (string.Compare(bindingName, typeof(WSHttpBinding).FullName, true) == 0)
                {
                    result = new WSHttpBinding();
                }
                else if (string.Compare(bindingName, typeof(WS2007HttpBinding).FullName, true) == 0)
                {
                    result = new WS2007HttpBinding();
                }
                else if (string.Compare(bindingName, typeof(BasicHttpBinding).FullName, true) == 0)
                {
                    result = new BasicHttpBinding();
                }
                else if (string.Compare(bindingName, typeof(WSDualHttpBinding).FullName, true) == 0)
                {
                    result = new WSDualHttpBinding();
                }
                else if (string.Compare(bindingName, typeof(WS2007FederationHttpBinding).FullName, true) == 0)
                {
                    result = new WS2007FederationHttpBinding();
                }
                else if (string.Compare(bindingName, typeof(WSFederationHttpBinding).FullName, true) == 0)
                {
                    result = new WSFederationHttpBinding();
                }
                else if (string.Compare(bindingName, typeof(NetNamedPipeBinding).FullName, true) == 0)
                {
                    result = new NetNamedPipeBinding();
                }
                else if (string.Compare(bindingName, typeof(NetMsmqBinding).FullName, true) == 0)
                {
                    result = new NetMsmqBinding();
                }
                else if (string.Compare(bindingName, typeof(MsmqIntegrationBinding).FullName, true) == 0)
                {
                    result = new MsmqIntegrationBinding();
                }
                else if (string.Compare(bindingName, typeof(NetTcpBinding).FullName, true) == 0)
                {
                    result = new NetTcpBinding();
                }
                else if (string.Compare(bindingName, typeof(NetPeerTcpBinding).FullName, true) == 0)
                {
                    result = new NetPeerTcpBinding();
                }
            }
            catch
            {
                result = new BasicHttpBinding(BasicHttpSecurityMode.None);
            }

            return result;
        }

        public static Binding CreateBindingFromConfig(string bindingName)
        {
            return CreateBinding(ServiceBindings.GetActualBindingName(bindingName));
        }
    }
}
