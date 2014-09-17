using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace BMC.CoreLib.WcfHelper.Helpers
{
    public static class ContractBindingsHelper
    {
        public static readonly NetTcpBinding TcpBinding = CreateTcpBinding();

        static ContractBindingsHelper() { }

        public static NetTcpBinding CreateTcpBinding()
        {
            NetTcpBinding binding = new NetTcpBinding()
            {
                CloseTimeout = TimeSpan.FromMinutes(10),
                OpenTimeout = TimeSpan.FromMinutes(10),
                ReceiveTimeout = TimeSpan.FromMinutes(10),
                SendTimeout = TimeSpan.FromMinutes(10),
            };
            binding.Security.Mode = SecurityMode.None;
            return binding;
        }

        public static BasicHttpBinding CreateBasicHttpBinding()
        {
            BasicHttpBinding binding = new BasicHttpBinding()
            {
                CloseTimeout = TimeSpan.FromMinutes(10),
                OpenTimeout = TimeSpan.FromMinutes(10),
                ReceiveTimeout = TimeSpan.FromMinutes(10),
                SendTimeout = TimeSpan.FromMinutes(10),
            };
            binding.Security.Mode = BasicHttpSecurityMode.None;
            return binding;
        }

        public static WSDualHttpBinding CreateDualHttpBinding()
        {
            WSDualHttpBinding binding = new WSDualHttpBinding()
            {
                CloseTimeout = TimeSpan.FromMinutes(10),
                OpenTimeout = TimeSpan.FromMinutes(10),
                ReceiveTimeout = TimeSpan.FromMinutes(10),
                SendTimeout = TimeSpan.FromMinutes(10),
            };
            return binding;
        }

        public static WSHttpBinding CreateWSHttpBinding()
        {
            WSHttpBinding binding = new WSHttpBinding()
            {
                CloseTimeout = TimeSpan.FromMinutes(10),
                OpenTimeout = TimeSpan.FromMinutes(10),
                ReceiveTimeout = TimeSpan.FromMinutes(10),
                SendTimeout = TimeSpan.FromMinutes(10),
            };
            binding.Security.Mode = SecurityMode.None;
            return binding;
        }

        public static WebHttpBinding CreateWebHttpBinding()
        {
            WebHttpBinding binding = new WebHttpBinding()
            {
                CloseTimeout = TimeSpan.FromMinutes(10),
                OpenTimeout = TimeSpan.FromMinutes(10),
                ReceiveTimeout = TimeSpan.FromMinutes(10),
                SendTimeout = TimeSpan.FromMinutes(10),
            };
            binding.Security.Mode = WebHttpSecurityMode.None;
            return binding;
        }
    }
}
