using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BMC.Common.Security;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.Interfaces;
using System.Reflection;
using System.IO;

namespace BMCTicketingConfig
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
            try
            {
                #region Resource File
                string executingAssemblyFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string resourceFilePath = System.IO.Path.Combine(executingAssemblyFilePath, "BMC.Resources.dll");
                Assembly resourceAssembly = Assembly.LoadFile(resourceFilePath);
                BMC.Common.ResourceExtensions.RegisterResource("BMC.Resources.MessageResources", (int)AppResourceTypes.MessageResource, resourceAssembly);
                BMC.Common.ResourceExtensions.RegisterResource("BMC.Resources.TextResources", resourceAssembly);
                #endregion

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                string[] args = Environment.GetCommandLineArgs();
                ((IAppInvokeEntryPoint)new Program()).DisplayEntryForm(args, (f) => { Application.Run(f); });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #region IAppInvokeEntryPoint Members

        public void DisplayEntryForm(string[] args, Action<Form> doWork)
        {
            //args = new string[] { "","zVt991p2aKw=" };
            try
            {
                LogManager.WriteLog("args length:" + args.Length.ToString(), LogManager.enumLogLevel.Info);
                if (args.Length > 0)
                {
                    doWork(new TicketingConfig(CryptographyHelper.Decrypt(args[1].ToString())));
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
