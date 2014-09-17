using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseClient.Helpers;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using System.Threading;
using BMC.Common.ConfigurationManagement;
using BMC.CoreLib.Configuration;
using BMC.CoreLib.Diagnostics;

namespace BMC.EnterpriseClient
{
    public class AppGlobals : DisposableObject
    {
        private static readonly AppGlobals _current = new AppGlobals();
        private IDictionary<string, UserAccessEntity> _userAccesses = null;
        private AdminBusiness _businessAdmin = null;
        private UserEntity _loggedinUser = null;
        private List<string> _params = null;

        private AppSubTaskLinks _subTasks = null;
        private AppConfigStore _config = null;

        private AppGlobals()
        {
            _businessAdmin = new AdminBusiness();
            _userAccesses = new SortedDictionary<string, UserAccessEntity>(StringComparer.InvariantCultureIgnoreCase);
            _subTasks = new AppSubTaskLinks();
            _config = new AppConfigStore();            
            bool isc = _config.IsValidSQLConnect();
            this.InitConfigValues();
        }

        public static AppGlobals Current
        {
            get { return _current; }
        }

        public AdminBusiness BusinessAdmin
        {
            get { return _businessAdmin; }
        }

        public AppSubTaskLinks SubTasks
        {
            get { return _subTasks; }
        }

        public AppConfigStore Config
        {
            get { return _config; }
        }

        public MainForm ActiveForm { get; set; }
 
        public IDictionary<string, UserAccessEntity> UserAccesses
        {
            get { return _userAccesses; }
        }

        public UserEntity LoggedinUser
        {
            get { return _loggedinUser; }
            set
            {
                _loggedinUser = value;
                if (value == null)
                {
                    _userAccesses.Clear();
                    CommonBiz.iUserId = 0;
                    CommonBiz.strUsername = string.Empty;
                }
                else
                {
                    _businessAdmin.GetUserAccesses(value.SecurityUserID, ref _userAccesses);
                    AppEntryPoint.Current.UserId = value.SecurityUserID;
                    AppEntryPoint.Current.StaffId = value.StaffID;
                    AppEntryPoint.Current.UserName = value.UserName;
                    CommonBiz.iUserId = AppEntryPoint.Current.UserId;
                    CommonBiz.strUsername = AppEntryPoint.Current.UserName;

                    GetCustomerAccessViewAllCompaniesResult AccessCompanyAll = new GetCustomerAccessViewAllCompaniesResult();
                    AccessCompanyAll = _businessAdmin.CheckCustomerAccessViewAllCompanies(AppEntryPoint.Current.StaffId);
                    if (AccessCompanyAll != null)
                        AppEntryPoint.Current.CustomerAccessViewAllCompanies = Convert.ToBoolean(AccessCompanyAll.Customer_Access_View_All_Companies);

                    GetCustomerAccessViewAllDepotsResult AccessDepotAll = new GetCustomerAccessViewAllDepotsResult();
                    AccessDepotAll = _businessAdmin.CheckCustomerAccessDepotAll(AppEntryPoint.Current.StaffId);
                    if(AccessDepotAll != null)
                        AppEntryPoint.Current.CustomerAccessViewAllDepot = Convert.ToBoolean(AccessDepotAll.Customer_Access_View_All_Depots);           
                }
            }
        }

        public List<string> Params
        {
            get
            {
                return _params;
            }
            set
            {
                _params = value;
            }
        }

        public void Signout()
        {
            _businessAdmin.Signout(_loggedinUser);
            this.LoggedinUser = null;
            this.CleanupTasks();
            AppEntryPoint.Current.CleanupProcesses();
        }

        public bool HasUserAccess(string userAccess)
        {
            if (this.UserAccesses.ContainsKey(userAccess))
                return this.UserAccesses[userAccess].Value;
            else
                return false;
        }

        //public event TaskAddedHandler AfterTaskAdded = null;

        public AppSubTaskLink AddSubTask(object source, Func<AppGlobals, bool> accessName, Func<object, Form> createForm)
        {
            AppSubTaskLink task = new AppSubTaskLink(source, accessName, createForm);
            if (!_subTasks.ContainsKey(source))
            {
                _subTasks.Add(source, task);
            }
            else
            {
                task = _subTasks[source];
            }
            return task;
        }

        public AppSubTaskLink AddSubTask(object source, AppSubTaskLink task)
        {
            if (!_subTasks.ContainsKey(source))
            {
                _subTasks.Add(source, task);
            }
            else
            {
                task = _subTasks[source];
            }
            return task;
        }

        public AppSubTaskLink AddSubTask(object source, Func<AppGlobals, bool> accessName, string filePath, string arguments)
        {
            return this.AddSubTask(source, new AppSubTaskLink(source, accessName, filePath, arguments));
        }

        public AppSubTaskLink AddSubTask(object source, Func<AppGlobals, bool> accessName, string filePath, Func<string[]> argumentsFunc)
        {
            return this.AddSubTask(source, new AppSubTaskLink(source, accessName, filePath, argumentsFunc));
        }

        public AppSubTaskLink AddSubTask(object source, Func<AppGlobals, bool> accessName, string filePath, Func<string[]> argumentsFunc, bool ignoreSpace)
        {
            return this.AddSubTask(source, new AppSubTaskLink(source, accessName, filePath, argumentsFunc, ignoreSpace));
        }

        public AppSubTaskLink AddSubTask(object source, Func<AppGlobals, bool> accessName, Action customAction)
        {
            return this.AddSubTask(source, new AppSubTaskLink(source, accessName, customAction));
        }

        public AppSubTaskLink GetSubTask(object source)
        {
            if (_subTasks.ContainsKey(source))
            {
                return _subTasks[source];
            }
            return null;
        }

        public event EventHandler CleanupCompleted = null;

        private void OnCleanupCompleted()
        {
            if (this.CleanupCompleted != null)
            {
                this.CleanupCompleted(this, EventArgs.Empty);
            }
        }

        public void CleanupTasks()
        {
            try
            {
                foreach (AppSubTaskLink link in _subTasks.Values)
                {
                    try
                    {
                        link.Close(true);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                this.OnCleanupCompleted();
        }
        }

        public bool IsAnyPendingInlineForms
        {
            get
            {
                ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "IsAnyPendingInlineForms");
                bool result = default(bool);

                try
                {
                    var count = (from f in _subTasks.Values
                                 where f.LinkType == ToolLinkType.InlineForm
                                        && f.Instance != null
                                        && f.Instance is IBMCExtendedForm
                                 select f.Instance).Count();
                    result = (count > 0);                                 
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }

                return result;
            }
        }

        public void PerformUserAccess()
        {
            try
            {
                foreach (AppSubTaskLink link in _subTasks.Values)
                {
                    try
                    {
                        ToolStripButton button = link.SourceItem as ToolStripButton;
                        button.Visible = true;

                        if (link.AccessName != null)
                        {
                            button.Visible = link.AccessName(AppGlobals.Current);
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            {
                this.OnCleanupCompleted();
            }
        }

        private void InitConfigValues()
        {
            this.NETWORK_CHECK_TIMEOUT = TypeSystem.GetValueInt(this.ReadFromConfig("NETWORK_CHECK_TIMEOUT", "5000"));
            this.DB_CHECK_TIMEOUT = TypeSystem.GetValueInt(this.ReadFromConfig("DB_CHECK_TIMEOUT", "5000"));
            this.ShowHourlyCalendarDay = TypeSystem.GetValueBoolSimple(this.ReadFromConfig("SHOWHOURLYCALENDARDAY", "false"));
        }

        public int NETWORK_CHECK_TIMEOUT { get; private set; }

        public int DB_CHECK_TIMEOUT { get; private set; }

        public bool ShowHourlyCalendarDay { get; set; }

        private string ReadFromConfig(string settingName, string defaultValue)
        {
            try
            {
                return ConfigManager.Read(settingName);
            }
            catch { return defaultValue; }
        }

        public int UserId { get; set; }

        public bool SuperUser { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public string UserPasswordHash
        {
            get
            {
                return AdminBusiness.CreateHash(this.UserPassword);
            }
        }
    }
}
