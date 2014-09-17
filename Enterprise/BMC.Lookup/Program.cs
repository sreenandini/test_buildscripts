using System;
using System.Windows.Forms;
using MachineMaintenance;
using BMC.Common.Security;

namespace BMC.Lookup
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static int LoginUserID;
        public static string LoginUserName;
        public static string CatergoryOrReason;

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoginUserID = Convert.ToInt32(CryptographyHelper.Decrypt(args[2]));
            LoginUserName = CryptographyHelper.Decrypt(args[1]);
            CatergoryOrReason = args[0];
            Application.Run(new LookupUI(args[0]));
        }
    }
}
