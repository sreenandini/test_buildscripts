using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.EBSComms.Contracts.Configuration;
using BMC.CoreLib;
using BMC.CoreLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMC.EBSComms.Hosting.Properties;

namespace BMC.EBSComms.Hosting
{
    public class Program : ServiceEntryPoint
    {
        private IEBSCommServerConfigStore _config = null;

        public Program(string[] args)
            : base(new Guid("E1FF7413-B3A8-46F8-AFDB-B9062FC56FEB"), args)
        {
            _config = EBSCommServerConfigStoreFactory.Store;
            Log.AddFileLoggingSystem(_config.LogPath);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            #region ServiceIdentifier
            LogManager.WriteLog("Entering Data Export To Site EXE", LogManager.enumLogLevel.Debug);
            BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Enterprise;
            LogManager.WriteLog("BMCRegistryHelper.InstallationType is :" + BMCRegistryHelper.InstallationType, LogManager.enumLogLevel.Debug);
            #endregion

            using (ServiceEntryPoint ep = new Program(args))
            {
                ep.Run();
            }
        }

        public override void Run()
        {
            this.RunInternal("BMC.EBSCommunication.Service", "BMC EBS Communication Service", typeof(Program).Assembly.Location);
        }

        protected override ServiceBase[] GetServices()
        {
            return new ServiceBase[] 
            {
                new EBSCommServerWinService(_executorService)
            };
        }

        protected override IServiceHost CreateServiceHost()
        {
            return EBSCommServerHostFactoryFactory.CreateHost(_executorService);
        }

        protected override void CustomizeForm(Form form)
        {
            base.CustomizeForm(form);
            form.Icon = Resources.BMC_Icon;
            form.Text = "BMC EBS Communication Service (Debug)";
        }
    }
}
