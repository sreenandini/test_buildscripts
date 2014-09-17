using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace BMC.CoreLib.Services
{
    internal static class AppNotifyServiceHelper
    {
        internal static NetTcpBinding CreateBinding()
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
    }
}
