using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BMC.CentralisedSiteSettings.Business;
using BMC.Common.Interfaces;
using BMC.Common.Utilities;
using BMC.CoreLib.Win32;

namespace BMC.CentralisedSiteSettings.Presentation
{
    [Serializable]
    public class Program : IAppInvokeEntryPoint
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BMC.Common.LogManagement.LogManager.WriteLog("Entering Centralised Site Settings EXE", BMC.Common.LogManagement.LogManager.enumLogLevel.Debug);
            BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Enterprise;
            BMC.Common.LogManagement.LogManager.WriteLog("BMCRegistryHelper.InstallationType is :" + BMCRegistryHelper.InstallationType, BMC.Common.LogManagement.LogManager.enumLogLevel.Debug);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            string[] args = Environment.GetCommandLineArgs();
            ((IAppInvokeEntryPoint)new Program()).DisplayEntryForm(args, (f) => { Application.Run(f); });
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogManager.WriteLog("Centralised Site Settings Configuration Failed." + e.ExceptionObject.ToString());
            ShowErrorMessage();
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            LogManager.WriteLog("Centralised Site Settings Configuration Failed." + e.Exception.ToString());
            ShowErrorMessage();    
        }

        static void ShowErrorMessage()
        {
            //MessageBox.Show("\n An error has occured in the BMC Centralised Site Settings Configuration.\nPlease contact the BMC Support Team (EMail - Level2BallyMulticonnect@ballytech.com).", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Error);            
            Win32Extensions.ShowErrorMessageBox(null, BMC.Common.ResourceExtensions.GetResourceTextByKey(null, "Key_SiteSettings_Error"), "Bally MultiConnect - Enterprise");
        }

        #region IAppInvokeEntryPoint Members

        public void DisplayEntryForm(string[] args, Action<Form> doWork)
        {
            //BMC.Common.LogManagement.LogManager.WriteLog("Entering Centralised Site Setting EXE", BMC.Common.LogManagement.LogManager.enumLogLevel.Debug);
            //BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Enterprise;
            //BMC.Common.LogManagement.LogManager.WriteLog("BMCRegistryHelper.InstallationType is :" + BMCRegistryHelper.InstallationType, BMC.Common.LogManagement.LogManager.enumLogLevel.Debug);
            string LoginUsername = args[1];
            string LoginUserID = args[2];

            if (args.Length == 3)
            {
                doWork(new frmSiteSetting(LoginUserID, LoginUsername));
            }            
        }

        #endregion
    }
}