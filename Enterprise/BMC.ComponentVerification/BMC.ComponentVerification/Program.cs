using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BMC.ComponentVerification.UI;
using BMC.Common.Security;
using BMC.Common.LogManagement;

namespace BMC.ComponentVerification
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        ///
        public static int LoginUserID;
        public static string LoginUserName;
        [STAThread]
        static void Main()
        {
           
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                string[] args = Environment.GetCommandLineArgs();
                if (args.Length > 0)
                {
                    
                    
                    LoginUserID = Convert.ToInt32(CryptographyHelper.Decrypt(args[2]));
                    LoginUserName = CryptographyHelper.Decrypt(args[1]);
                    
                    if (args.Length == 3)
                    {
                        Application.Run(new ViewComponents());
                    }
                    else
                        MessageBox.Show("Component Verification failed to load - Invaild Parameters.", "Component Verification", MessageBoxButtons.OK, MessageBoxIcon.Error);                   
                }                
            }
            catch (Exception exMain)
            {
                LogManager.WriteLog("Error - " + exMain.Message, LogManager.enumLogLevel.Info);
                MessageBox.Show("Component Verification failed to load - Exception Occured.", "Component Verification", MessageBoxButtons.OK, MessageBoxIcon.Error);                   
            }
        }
    }
}
