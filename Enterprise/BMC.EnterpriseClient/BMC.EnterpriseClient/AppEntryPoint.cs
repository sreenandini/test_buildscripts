using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using System.Windows.Forms;
using BMC.EnterpriseClient.Helpers;
using BMC.EnterpriseClient.Views;
using System.Diagnostics;
using BMC.Common.ExceptionManagement;
using System.Runtime.InteropServices;
using System.Threading;
using System.Globalization;
using BMC.Common.ConfigurationManagement;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;

namespace BMC.EnterpriseClient
{
    public class AppEntryPoint : EntryPoint
    {
        private ViewInfoCollection _viewInfos = null;

        public AppEntryPoint(string[] args)
            : base(args)
        {
            Log.AddAppFileLoggingSystem();
            CultureInfo ci = new CultureInfo(ConfigManager.Read("GetDefaultCultureForRegion"));
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            _viewInfos = new ViewInfoCollection();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Current = this;
        }

        public static new AppEntryPoint Current { get; set; }

        public string UserName { get; set; }

        public int UserId { get; set; }

        public int StaffId { get; set; }

        public bool CustomerAccessViewAllDepot { get; set; }

        public bool CustomerAccessViewAllCompanies { get; set; }

        protected override void ExecuteWithCommandLine()
        {
#if EXE_MODULE
            Form frmView = null;
            if (_args != null && _args.Length > 0)
            {
                if (_args.Length > 1)
                {
                    this.UserName = _args[1];
                }

                if (_args.Length > 2)
                {
                    AdminBusiness _business = new AdminBusiness();
                    UserEntity _user = null;

                    this.UserId = TypeSystem.GetValueInt(_args[2]);
                    _business.GetUserDetailsByUserID(this.UserId, ref _user);
                    if (_user == null)
                    {
                        Win32Extensions.ShowInfoMessageBox("User doesn't exist.");
                        return;
                    }
                    AppGlobals.Current.LoggedinUser = _user;
                }

                if (_args.Length > 3)
                {
                    List<string> lstParams = new List<string>();
                    for (int i=3; i< _args.Length; i++)
                    {
                        lstParams.Add(_args[i]);
            }
                    AppGlobals.Current.Params = lstParams;
                }

                string viewName = _args[0].ToLower();
                if (_viewInfos.ContainsKey(viewName))
                {
                    ViewFormInfo viewInfo = _viewInfos[viewName];
                    if (ProcessMutexHelper.FindInstance(viewInfo.UniqueId))
                    {
                        frmView = viewInfo.CreateForm();
                    }
                    else
                    {
                        return;
                    }
                }
            }

            if (frmView == null)
            {
                Win32Extensions.ShowInfoMessageBox("Invalid arguments given.");
            }
            else
            {
                Application.Run(frmView);
            }
#else
            this.ExecuteWithoutCommandLine();
#endif

            this.CleanupProcesses();
        }

        public bool IsSplashCanceled { get; set; }

        protected override void ExecuteWithoutCommandLine()
        {
#if EXE_MODULE
            Win32Extensions.ShowInfoMessageBox("Kindly open the application from Enterprise Client.");
#else

            if (AppGlobals.Current.Config.Splash)
            {
                Application.Run(new AboutForm(AboutFormTypes.Splash));
            }
            if (this.IsSplashCanceled) return;
            Application.Run(new MainForm());
#endif
        }

        #region External Processes
        
        private object _processLock = new object();
        private IDictionary<string, KeyValuePair<string, AppSubTaskLink>> _processes = new SortedDictionary<string, KeyValuePair<string, AppSubTaskLink>>();        

        internal void CleanupProcesses()
        {
            try
            {
                if (_processes.Count == 0) return;

                IEnumerable<AppSubTaskLink> processes = (from p in _processes.Values
                                                         select p.Value).ToArray();
                foreach (AppSubTaskLink process in processes)
                {
                    process.CloseExternalProcess();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void StartProcess(object ownerItem, Func<AppGlobals, bool> accessName, string exePath, string arguments)
        {
            this.StartProcess(ownerItem, accessName, exePath, arguments, true);
        }

        public void StartProcess(object ownerItem, Func<AppGlobals, bool> accessName, string exePath, string arguments, bool singleInstance)
        {
            try
            {
                AppSubTaskLink link = null;
                lock (_processLock)
                {
                    link = (from p in _processes
                            where p.Value.Value.Activator.ExternalFilePath.IgnoreCaseCompare(exePath)
                            select p.Value.Value).FirstOrDefault();
                }
                if (link == null)
                {
                    link = new AppSubTaskLink(ownerItem, accessName, exePath, arguments);
                    link.ExternalProcessOpened += new ExternalProcessOpenedHandler(link_ExternalProcessOpened);
                    link.ExternalProcessClosed += new ExternalProcessClosedHandler(link_ExternalProcessClosed);
                }
                link.ActiveExternalProcess();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Win32Extensions.ShowErrorMessageBox(ex.Message);
            }
        }

        void link_ExternalProcessOpened(AppSubTaskLink link)
        {
            lock (_processLock)
            {
                string key = link.Activator.UniqueKey;
                if (!_processes.ContainsKey(key))
                {
                    _processes.Add(key, new KeyValuePair<string, AppSubTaskLink>(key, link));
                }
                if (link.Activator.IsNonMdiClient)
                {
                    link.ShowChildForm(link.Activator);
                }
            }
        }

        void link_ExternalProcessClosed(AppSubTaskLink link)
        {
            lock (_processLock)
            {
                if (_processes.ContainsKey(link.Activator.UniqueKey))
                {
                    link.ExternalProcessOpened -= (link_ExternalProcessOpened);
                    link.ExternalProcessClosed -= (link_ExternalProcessClosed);
                    _processes.Remove(link.Activator.UniqueKey);
                }
            }
        }

        #endregion
    }
}
