using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BMC.Common.LogManagement;
using BMC.EnterpriseReportsTransport;
using System.Data;
using BMC.EnterpriseReportsBusiness;
using BMC.Common.ExceptionManagement;
using BMC.Security.Interfaces;
using BMC.Common.Security;
using BMC.Common.Utilities;
using BMC.Common.ConfigurationManagement;
using System.Globalization;
using BMC.Common.Interfaces;
using BMC.Reports;
using System.Reflection;
using System.IO;
namespace BMC.EnterpriseReportsUI
{
    [Serializable]
    class Program : IAppInvokeEntryPoint
    {
        static ReportsBusiness oReportBusiness = new ReportsBusiness();
        //static ListReportparameters lstParams = new ListReportparameters();
        static IEnumerable<GetAllReportsToRolesAccess> reports = null;
        static string RoleName = string.Empty;
        static int i = 0;
        static int RoleID = 0;
        public static bool bExtApp = false;
        static string sxml;
        static string ProcedureName = string.Empty;
        public static int UserID;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            LogManager.WriteLog("Entering Reports EXE", LogManager.enumLogLevel.Debug);
            BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Enterprise;
            LogManager.WriteLog("BMCRegistryHelper.InstallationTypes is :" + BMCRegistryHelper.ActiveInstallationType, LogManager.enumLogLevel.Debug);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ((IAppInvokeEntryPoint)new Program()).DisplayEntryForm(Environment.GetCommandLineArgs(), (f) => { Application.Run(f); });
            
        }

        
        public void DisplayEntryForm(string[] args, Action<Form> doWork)
        {
            try
            {
                #region Resource File
                string executingAssemblyFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string resourceFilePath = System.IO.Path.Combine(executingAssemblyFilePath, "BMC.Resources.dll");
                Assembly resourceAssembly = Assembly.LoadFile(resourceFilePath);
                BMC.Common.ResourceExtensions.RegisterResource("BMC.Resources.MessageResources", 1, resourceAssembly);
                BMC.Common.ResourceExtensions.RegisterResource("BMC.Resources.TextResources", resourceAssembly);
                #endregion


                //string[] args = Environment.GetCommandLineArgs();
                LogManager.WriteLog("Hello Welcome to enterprise Reports", LogManager.enumLogLevel.Debug);
                //string[] args = { "exe", @"\ROLE=Super User", @"\REPORTNAME=AUDITTRAIL", @"\FROMDATE=2011-03-20 00:00:00", @"\TODATE=2011-03-20 23:59:59", @"\MODULEID=503", @"\LOCAL=TRUE", @"\ROWS=100", @"\MODULENAME=General" };
                //string[] args = { "exe", "0" };
                //string[] args = { "exe", "Super User" ,"2"};
                //args = new String[]{ "exe", "lhW0nWGk6w5JNybUmYVTpQ==[DELIM]P5hKyDzlN3c=" };
                LogManager.WriteLog("args length:" + args.Length.ToString(), LogManager.enumLogLevel.Info);

                if (args.Length > 0)
                {
                    i = args.GetUpperBound(0);
                    LogManager.WriteLog("Upper bound:" + args.GetUpperBound(0).ToString() + "args length:" + args.Length.ToString(), LogManager.enumLogLevel.Debug);

                    if (i == 1)
                    {

                        LogManager.WriteLog(args[1].ToString().Trim(), LogManager.enumLogLevel.Info);
                        string[] tempStr = args[1].ToString().Trim().Split(new string[] { "[DELIM]" }, StringSplitOptions.None);
                        LogManager.WriteLog(tempStr[0] + "   " + tempStr[1], LogManager.enumLogLevel.Info);

                        LogManager.WriteLog(CryptographyHelper.Decrypt(tempStr[1]), LogManager.enumLogLevel.Info);
                        UserID = Convert.ToInt32(CryptographyHelper.Decrypt(tempStr[1]));

                        LogManager.WriteLog(CryptographyHelper.Decrypt(tempStr[0]), LogManager.enumLogLevel.Info);
                        doWork(new ReportsMain(CryptographyHelper.Decrypt(tempStr[0].ToString()), ""));
                        GetSiteCulture();
                    }
                    else if (i == 2)
                    {
                        LogManager.WriteLog(CryptographyHelper.Decrypt(args[1].ToString().Trim()) + ":" + CryptographyHelper.Decrypt(args[2].ToString().Trim()), LogManager.enumLogLevel.Info);
                        doWork(new ReportsMain(CryptographyHelper.Decrypt(args[1].ToString()), CryptographyHelper.Decrypt(args[2].ToString())));
                    }
                   
                    else
                    {
                        MessageBox.Show("Sorry! Could not find a group.", Constants.MessageBoxHeader);
                        LogManager.WriteLog(CryptographyHelper.Decrypt(args[0].ToString()), LogManager.enumLogLevel.Info);
                    }
                }
                else
                {
                    MessageBox.Show("Sorry! Could not find a group.", Constants.MessageBoxHeader);
                    LogManager.WriteLog(CryptographyHelper.Decrypt(args.Length.ToString()), LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            
        }

        private static bool Hasrights(int RoleID)
        {
            bool ret = false;
            try
            {
                reports = oReportBusiness.GetAllReportsToRolesAccess(RoleID);

                foreach (GetAllReportsToRolesAccess rra in reports)
                {
                    if (rra.Level > 0)
                    {
                        if (rra.ReportArgName == Constants.AuditTrail)
                        {
                            ret = true;
                            break;
                        }
                        else
                            ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return ret;
        }

        private static void GetSiteCulture()
        {
            try
            {
                ReportsBusiness oReportsBusiness = new ReportsBusiness(); ;

                SiteCultureInfo siteCultureInfo = oReportsBusiness.GetSiteCulture(UserID);


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
    }
}
