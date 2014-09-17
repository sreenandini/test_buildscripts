using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows.Forms;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.Security;
using BMC.Common.Utilities;
using BMC.Common.ConfigurationManagement;
using System.Globalization;
using BMC.Common.Interfaces;

namespace BMCCashierTransactions
{
    [Serializable]
    public class Program : IAppInvokeEntryPoint
    {
        public static int nUserID = 1;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string[] args = Environment.GetCommandLineArgs();
            ((IAppInvokeEntryPoint)new Program()).DisplayEntryForm(args, (f) => { Application.Run(f); });
        }

        private static void GetSiteCulture()
        {
            try
            {
                TreasuryTransactions oBusiness = new TreasuryTransactions(); ;

                SiteCultureInfo siteCultureInfo = oBusiness.GetSiteCulture(nUserID);


                if (siteCultureInfo != null)
                {
                    ExtensionMethods.CurrentSiteCulture = siteCultureInfo.RegionCulture;
                    ExtensionMethods.CurrentCurrenyCulture = siteCultureInfo.CurrencyCulture;
                    ExtensionMethods.CurrentDateCulture = siteCultureInfo.DateCulture;

                }
                else
                {
                    ExtensionMethods.CurrentSiteCulture = ConfigManager.Read("GetDefaultCultureForRegion");
                    ExtensionMethods.CurrentCurrenyCulture = ConfigManager.Read("GetDefaultCultureForCurrency");
                    ExtensionMethods.CurrentDateCulture = ConfigManager.Read("GetDefaultCultureForDate");
                }

                Application.CurrentCulture = new CultureInfo(ExtensionMethods.CurrentDateCulture);

            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #region IAppInvokeEntryPoint Members

        public void DisplayEntryForm(string[] args, Action<Form> doWork)
        {
            try
            {
                int i = 0;

                //string[] args = new string[] {"0", "Sf8bkbJhNCM=[DELIM]P5hKyDzlN3c=" }; //<DO NOT REMOVE FOR TESTING PURPOSE>

                LogManager.WriteLog("args length:" + args.Length.ToString(), LogManager.enumLogLevel.Info);
                if (args.Length > 0)
                {
                    i = args.GetUpperBound(0);
                    LogManager.WriteLog("UpperBound=" + i.ToString(), LogManager.enumLogLevel.Info);
                    if (i == 1)
                    {
                        LogManager.WriteLog("Site ID=" + args[1].ToString(), LogManager.enumLogLevel.Info);

                        string[] tempStr = args[1].ToString().Trim().Split(new string[] { "[DELIM]" }, StringSplitOptions.None);
                        LogManager.WriteLog(tempStr[0] + "   " + tempStr[1], LogManager.enumLogLevel.Info);

                        LogManager.WriteLog(CryptographyHelper.Decrypt(tempStr[1]), LogManager.enumLogLevel.Info);
                        nUserID = Convert.ToInt32(CryptographyHelper.Decrypt(tempStr[1]));

                        GetSiteCulture();
                        doWork(new CashDeskManager(Convert.ToInt32(CryptographyHelper.Decrypt(tempStr[0].ToString()))));
                        //Application.Run(new CashDeskManager(1));//<DO NOT REMOVE FOR TESTING PURPOSE>
                    }

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
