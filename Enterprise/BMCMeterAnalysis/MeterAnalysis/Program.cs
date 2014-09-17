using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BMC.Common.Interfaces;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.Common.Security;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace MeterAnalysis
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
        static void Main(string[] args)
        {
            LogManager.WriteLog("Entering Meter Analysis EXE", LogManager.enumLogLevel.Debug);
            BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Enterprise;
            LogManager.WriteLog("BMCRegistryHelper.InstallationType is :" + BMCRegistryHelper.InstallationType, LogManager.enumLogLevel.Debug);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //args = new string[]{"1"};


            /* To Add the Resource file into memory of executing application */
            #region Resource File
            string executingAssemblyFilePath =Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            string resourceFilePath = System.IO.Path.Combine(executingAssemblyFilePath, "BMC.Resources.dll");
            Assembly resourceAssembly = Assembly.LoadFile(resourceFilePath);
            BMC.Common.ResourceExtensions.RegisterResource("BMC.Resources.MessageResources", (int)AppResourceTypes.MessageResource, resourceAssembly);
            BMC.Common.ResourceExtensions.RegisterResource("BMC.Resources.TextResources", resourceAssembly);
            #endregion
          
            ((IAppInvokeEntryPoint)new Program()).DisplayEntryForm(args, (f) => { Application.Run(f); });
           
        }

        void IAppInvokeEntryPoint.DisplayEntryForm(string[] args, Action<Form> doWork)
        {
            if (args.Length == 1)
            {
                //doWork(new frmMeterAnalysis(Convert.ToInt32("0" + (args[0]))));
                doWork(new frmMeterAnalysis(Convert.ToInt32("0" + CryptographyHelper.Decrypt(args[0]))));
            }
            else
            {
                LogManager.WriteLog("Meter Analysis EXE, Improper parameters passed", LogManager.enumLogLevel.Debug);
            }
        }
    }
}