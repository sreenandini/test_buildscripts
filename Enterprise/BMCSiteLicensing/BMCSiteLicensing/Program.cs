using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.Common.Interfaces;

namespace BMCSiteLicensing
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
            LogManager.WriteLog("Entering Site Licensing EXE", LogManager.enumLogLevel.Debug);
            BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Enterprise;


            LogManager.WriteLog("BMCRegistryHelper.InstallationType is :" + BMCRegistryHelper.InstallationType, LogManager.enumLogLevel.Debug);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string[] args = Environment.GetCommandLineArgs();
            ((IAppInvokeEntryPoint)new Program()).DisplayEntryForm(args, (f) => { Application.Run(f); });
        }
        //catch (Exception ex)
        //{
        //    ExceptionManager.Publish(ex);
        //}

        //if (args.Length == 3)
        //{
        //    string LoginUsername = args[1];
        //    string LoginUserID = args[2];
        //    Application.Run(new frmSiteLicensing(LoginUserID, LoginUsername));
        //}
        //else
        //{
        //    LogManager.WriteLog("Site Licensing application invoked with improper parameters", LogManager.enumLogLevel.Error);
        //}
        //  }
        #region IAppInvokeEntryPoint Members

        public void DisplayEntryForm(string[] args, Action<Form> doWork)
        {
            try
            {
                LogManager.WriteLog("args length:" + args.Length.ToString(), LogManager.enumLogLevel.Info);
                if (args.Length >= 4)
                {
                    doWork(new frmSiteLicensing(args[1], args[2], args[3]));
                }
                else
                {
                    LogManager.WriteLog("Site Licensing application invoked with improper parameters", LogManager.enumLogLevel.Error);
                }
            }

            catch (Exception ex)
            {
                BMC.Common.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }

        #endregion

    }

}
