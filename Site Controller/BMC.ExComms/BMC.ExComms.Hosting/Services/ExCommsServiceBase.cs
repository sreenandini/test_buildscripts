using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using BMC.CoreLib.Services;

namespace BMC.ExComms.Hosting.Services
{
    public class ExCommsServiceBase : ServiceBase
    {
        private readonly IServiceHost _factory = null;

        public ExCommsServiceBase(IServiceHost factory, string serviceName)
        {
            _factory = factory;
            this.ServiceName = serviceName;
        }

        protected override void OnStart(string[] args)
        {
            _factory.Start();
        }

        protected override void OnStop()
        {
            _factory.Stop();
        }
    }
}
