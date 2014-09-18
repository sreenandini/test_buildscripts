namespace BMC.HourlyDailyReadJobs
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
            this.BGSHourlyReadProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.BGSHourlyReadInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // BGSHourlyReadProcessInstaller
            // 
            this.BGSHourlyReadProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.BGSHourlyReadProcessInstaller.Password = null;
            this.BGSHourlyReadProcessInstaller.Username = null;
            // 
            // BGSHourlyReadInstaller
            // 
            this.BGSHourlyReadInstaller.DisplayName = "BGS Hourly Read Service";
            this.BGSHourlyReadInstaller.ServiceName = "HourlyDailyService";
            this.BGSHourlyReadInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.BGSHourlyReadProcessInstaller,
            this.BGSHourlyReadInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller BGSHourlyReadProcessInstaller;
        private System.ServiceProcess.ServiceInstaller BGSHourlyReadInstaller;
    }
}