using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using BMC.Common.Utilities;
using BMC.CoreLib;
using BMC.CoreLib.Services;
using BMC.ExComms.Contracts.Configuration;
using BMC.ExComms.Hosting.Properties;
using BMC.ExComms.Hosting.Services;

namespace BMC.ExComms.Hosting
{
    public class Program : ServiceEntryPoint
    {
        private IExCommsServiceInfo _serviceInfo = null;

        public Program(string[] args)
            : base(args)
        {
            BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Exchange;
            Log.AddFileLoggingSystem(ExchangeConfigStoreFactory.Store.LogPath);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ServiceEntryPoint ep = new Program(args))
            {
                ep.Run();
            }
        }

        public override void Run()
        {
            _serviceInfo = ExCommsServicesFactrory.Run(_executorService, _args);
            _mutexGuid = _serviceInfo.ServiceGuid;
            this.RunInternal(_serviceInfo.ServiceName, _serviceInfo.DisplayName, typeof(Program).Assembly.Location);
        }

        protected override ServiceBase[] GetServices()
        {
            return new ServiceBase[] 
            {
                _serviceInfo.CreateService()
            };
        }

        protected override IServiceHost CreateServiceHost()
        {
            return _serviceInfo.Factory;
        }

        protected override void CustomizeForm(Form form)
        {
            base.CustomizeForm(form);
            form.Icon = Resources.BMC_Icon;
            form.Text = _serviceInfo.Description + @" (Debug)";
        }
    }
}
