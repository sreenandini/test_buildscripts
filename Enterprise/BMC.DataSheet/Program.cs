using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BMC.Common.Interfaces;
using System.Reflection;
using System.IO;
using BMC.Common.Security;

namespace CustomReports
{
    public enum AppResourceTypes
    {
        TextResource = 0,
        MessageResource = 1
    }

    [Serializable]
    class Program : IAppInvokeEntryPoint
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
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
            ((IAppInvokeEntryPoint)new Program()).DisplayEntryForm(Environment.GetCommandLineArgs(), (f) => { Application.Run(f); });
        }

        #region IAppInvokeEntryPoint Members

        public void DisplayEntryForm(string[] args, Action<Form> doWork)
        {            
            if (args.Length > 0)
            {
                int LoginUserID = Convert.ToInt32(CryptographyHelper.Decrypt(args[2]));               
                doWork(new MainPage(LoginUserID));
            }
        }

        #endregion
    }
}
