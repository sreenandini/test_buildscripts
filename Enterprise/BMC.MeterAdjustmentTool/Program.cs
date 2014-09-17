using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Resources;
using System.Threading;
using System.Globalization;
using BMC.Common.ExceptionManagement;
using BMC.MeterAdjustmentTool.Helpers;
using BMC.Common.Utilities;
using BMC.Common.LogManagement;
using BMC.Common.Interfaces;
using BMC.Common.Security;
using System.Reflection;
using System.IO;

namespace BMC.MeterAdjustmentTool
{
    public enum AppResourceTypes
    {
        TextResource = 0,
        MessageResource = 1
    }


    [Serializable]
    public class Program : IAppInvokeEntryPoint
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region Resource File
            string executingAssemblyFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string resourceFilePath = System.IO.Path.Combine(executingAssemblyFilePath, "BMC.Resources.dll");
            Assembly resourceAssembly = Assembly.LoadFile(resourceFilePath);
            BMC.Common.ResourceExtensions.RegisterResource("BMC.Resources.MessageResources", (int)AppResourceTypes.MessageResource, resourceAssembly);
            BMC.Common.ResourceExtensions.RegisterResource("BMC.Resources.TextResources", resourceAssembly);

            Extensions.AppTitle = BMC.Common.ResourceExtensions.GetResourceTextByKey(null, 1, "Key_MeterAdjustmentTool");

            #endregion

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string[] args = Environment.GetCommandLineArgs();
            ((IAppInvokeEntryPoint)new Program()).DisplayEntryForm(args, (f) => { Application.Run(f); });
        }

        #region IAppInvokeEntryPoint Members

        public void DisplayEntryForm(string[] args, Action<Form> doWork)
        {
            try
            {
                if (args.Length > 0)
                {
                    string userName = CryptographyHelper.Decrypt(args[1]);
                    int staffId = Convert.ToInt32(CryptographyHelper.Decrypt(args[2]));
                    int securityUserID = Convert.ToInt32(CryptographyHelper.Decrypt(args[3]));
                    LogManager.WriteLog("Entering Meter Adjustment EXE", LogManager.enumLogLevel.Debug);
                    BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Enterprise;
                    LogManager.WriteLog("BMCRegistryHelper.InstallationType is :" + BMCRegistryHelper.InstallationType, LogManager.enumLogLevel.Debug);
                    DBServerCollection.InitializeServers();
                    DBServerCollection.GetServers();

                    doWork(new MainForm(userName, staffId, securityUserID));
                    //Application.Run(new MainForm("admin",1,1));//<DO NOT REMOVE FOR TESTING PURPOSE>
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion
    }
}
