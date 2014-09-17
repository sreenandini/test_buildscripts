using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;

namespace BMC.CoreLib.WcfHelper.Bindings
{
    public static class ServiceBindings
    {
        public const string HTTP = "wsHttp";
        public const string HTTP_2007 = "wsHttp2007";
        public const string BASIC_HTTP = "basicHttp";
        public const string DUAL_HTTP = "wsDualHttp";
        public const string FEDERATION_HTTP_2007 = "wsFedrationHttp2007";
        public const string FEDERATION_HTTP = "wsFedrationHttp";
        public const string NAMED_PIPE = "namedPipe";
        public const string MSMQ = "msmq";
        public const string MSMQ_INTEGRATION = "msmqIntegration";
        public const string TCP = "tcp";
        public const string PEER_TCP = "wshttp";

        private static IDictionary<string, string> BINDING_NAMES = null;

        static ServiceBindings()
        {
            BINDING_NAMES = new SortedDictionary<string, string>() {
                { HTTP, typeof(WSHttpBinding).FullName },
                { HTTP_2007, typeof(WS2007HttpBinding).FullName },
                { BASIC_HTTP, typeof(BasicHttpBinding).FullName },
                { DUAL_HTTP, typeof(WSDualHttpBinding).FullName },
                { FEDERATION_HTTP_2007, typeof(WS2007FederationHttpBinding).FullName },
                { FEDERATION_HTTP,typeof(WSFederationHttpBinding).FullName },
                { NAMED_PIPE, typeof(NetNamedPipeBinding).FullName },
                { MSMQ , typeof(NetMsmqBinding).FullName },
                { MSMQ_INTEGRATION, typeof(MsmqIntegrationBinding).FullName },
                { TCP, typeof(NetTcpBinding).FullName },
                { PEER_TCP, typeof(NetPeerTcpBinding).FullName },
            };
        }

        public static bool HasAny(string bindingName)
        {
            return BINDING_NAMES.ContainsKey(bindingName);
        }

        public static string GetActualBindingName(string bindingName)
        {
            if (BINDING_NAMES.ContainsKey(bindingName)) return BINDING_NAMES[bindingName];
            return BINDING_NAMES[BASIC_HTTP];
        }
    }
}
