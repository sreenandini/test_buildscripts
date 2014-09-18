using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Services;
using BMC.EBSComms.Hosting;

namespace BMC.EBSComms.Hosting
{
    partial class EBSCommServerWinService : ServiceBase
    {
        private IServiceHost _factory = null;

        public EBSCommServerWinService(IExecutorService executorService)
        {
            InitializeComponent();
            _factory = EBSCommServerHostFactoryFactory.CreateHost(executorService);
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
