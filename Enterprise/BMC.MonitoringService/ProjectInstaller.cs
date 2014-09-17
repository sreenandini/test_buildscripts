namespace BMC.MonitoringService
{
    #region Namespaces

    using System.ComponentModel;
    using System.Configuration.Install;

    #endregion Namespaces

    #region Class

    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }

    #endregion Class
}
