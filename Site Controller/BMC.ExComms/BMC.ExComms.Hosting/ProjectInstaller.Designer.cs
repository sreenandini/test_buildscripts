namespace BMC.ExComms.Hosting
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bmcProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.siCommsService = new System.ServiceProcess.ServiceInstaller();
            this.siMonitorService = new System.ServiceProcess.ServiceInstaller();
            this.siAllService = new System.ServiceProcess.ServiceInstaller();
            this.siMonitorServiceRouter = new System.ServiceProcess.ServiceInstaller();
            this.siMonitorServiceProcessor = new System.ServiceProcess.ServiceInstaller();
            // 
            // bmcProcessInstaller
            // 
            this.bmcProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.bmcProcessInstaller.Password = null;
            this.bmcProcessInstaller.Username = null;
            // 
            // siCommsService
            // 
            this.siCommsService.Description = "BMC ExComms Service (Communication Server)";
            this.siCommsService.DisplayName = "BMC ExComms Service (Communication Server)";
            this.siCommsService.ServiceName = "bmc_excomms_svc_comms";
            this.siCommsService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // siMonitorService
            // 
            this.siMonitorService.Description = "BMC ExComms Service (Monitor Server)";
            this.siMonitorService.DisplayName = "BMC ExComms Service (Monitor Server)";
            this.siMonitorService.ServiceName = "bmc_excomms_svc_mon";
            // 
            // siAllService
            // 
            this.siAllService.Description = "BMC ExComms Service";
            this.siAllService.DisplayName = "BMC ExComms Service";
            this.siAllService.ServiceName = "bmc_excomms_svc";
            this.siAllService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // siMonitorServiceRouter
            // 
            this.siMonitorServiceRouter.Description = "BMC ExComms Service (Monitor Server Router)";
            this.siMonitorServiceRouter.DisplayName = "BMC ExComms Service (Monitor Server Router)";
            this.siMonitorServiceRouter.ServiceName = "bmc_excomms_svc_mon_route";
            // 
            // siMonitorServiceProcessor
            // 
            this.siMonitorServiceProcessor.Description = "BMC ExComms Service (Monitor Server Processor)";
            this.siMonitorServiceProcessor.DisplayName = "BMC ExComms Service (Monitor Server Processor)";
            this.siMonitorServiceProcessor.ServiceName = "bmc_excomms_svc_mon_proc";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.bmcProcessInstaller,
            this.siCommsService,
            this.siMonitorService,
            this.siAllService,
            this.siMonitorServiceRouter,
            this.siMonitorServiceProcessor});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller bmcProcessInstaller;
        private System.ServiceProcess.ServiceInstaller siCommsService;
        private System.ServiceProcess.ServiceInstaller siMonitorService;
        private System.ServiceProcess.ServiceInstaller siAllService;
        private System.ServiceProcess.ServiceInstaller siMonitorServiceRouter;
        private System.ServiceProcess.ServiceInstaller siMonitorServiceProcessor;
    }
}