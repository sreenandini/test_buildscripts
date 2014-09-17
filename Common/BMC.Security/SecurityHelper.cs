using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;
using System.Windows;
using BMC.Security.DataContext;
using BMC.Security.Entity;
using BMC.Security.Enums;
using System.Windows.Controls;
using BMC.Security.Interfaces;
using BMC.Security.Manager;
using Control = System.Windows.Controls.Control;
using System.Data.Linq;

namespace BMC.Security
{
    public class SecurityHelper
    {
        #region private Fields

        private static SecurityContext _securityContext;
        private static SecurityHelper _securityHelper;
        private static List<IUser> _users;
        private static string _connectionString;
        private static IUser _currentUser;
        private static bool _isWebApplication;
        private static bool PreviousLockStatus=false;
        #endregion

        #region Attached Property

        public static bool GetCheckSecurity(DependencyObject obj)
        {
            return (bool)obj.GetValue(CheckSecurityProperty);
        }

        public static void SetCheckSecurity(DependencyObject obj, bool value)
        {
            obj.SetValue(CheckSecurityProperty, value);
        }
        public static readonly DependencyProperty CheckSecurityProperty =
            DependencyProperty.RegisterAttached("CheckSecurity", typeof(bool), typeof(Window), new UIPropertyMetadata(false, ApplySecurityOnWpf));



        public static string GetControlName(DependencyObject obj)
        {
            return (string)obj.GetValue(ControlNameProperty);
        }

        public static void SetControlName(DependencyObject obj, string value)
        {
            obj.SetValue(ControlNameProperty, value);
        }

        public static readonly DependencyProperty ControlNameProperty =
            DependencyProperty.RegisterAttached("ControlName", typeof(string), typeof(Window), new UIPropertyMetadata("Not Found", CheckNameCallBack));

        private static void CheckNameCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Window)d).Loaded += SecurityHelperLoaded;
        }

        static void SecurityHelperLoaded(object sender, RoutedEventArgs e)
        {
            foreach (ControlClassName item in TraverseForWpfControlName(sender as Control, sender.ToString()))
                System.Windows.Forms.MessageBox.Show(item.PropertyName);

            ((Window)sender).Loaded -= SecurityHelperLoaded;
        }


        private static IEnumerable<ControlClassName> TraverseForWpfControlName(object root, string parentName)
        {
            foreach (var child in LogicalTreeHelper.GetChildren((DependencyObject)root))
            {
                if ((child is DependencyObject))
                    if ((child as Control) == null || (child as Control).Name.Length == 0)
                    {
                        yield return new ControlClassName { Control = (child as Control), PropertyName = parentName };
                        foreach (var item in TraverseForWpfControlName(child, parentName))
                            yield return item;
                    }
                    else
                    {
                        yield return new ControlClassName { Control = (child as Control), PropertyName = parentName };
                        foreach (var item in TraverseForWpfControlName(child, parentName + "." + ((Control)child).Name))
                            yield return item;
                    }
            }

        }

        public class ControlClassName
        {
            public Control Control { get; set; }
            public string PropertyName { get; set; }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Initializes the <see cref="SecurityHelper"/> class.
        /// </summary>
        internal SecurityHelper(string connectionString)
        {
#pragma warning disable 612,618
            _securityContext = new SecurityContext(connectionString);
#pragma warning restore 612,618
        }

        #endregion

        #region Public Methods

        public static IUser CurrentUser
        {
            get
            {
                if (!_isWebApplication)
                    return _currentUser;
                else
                    throw new InvalidFunctionCall("Current user cannot be accessed for Web Application.");
            }
            set { _currentUser = value; }
        }

        /// <summary>
        /// Gets the security context.
        /// </summary>
        /// <value>The security context.</value>         
        internal static SecurityContext SecurityContext
        {
            get
            {
                _securityContext = _securityContext ??
#pragma warning disable 612,618
 new SecurityContext(ConfigurationSettings.AppSettings["ConnectionString"]);
#pragma warning restore 612,618
                return _securityContext;
            }
        }

        public static void CreateInstance(string connectionString, bool isWebApplication)
        {
            _isWebApplication = isWebApplication;
            _connectionString = connectionString;
            _securityHelper = _securityHelper ??
                                   new SecurityHelper(_connectionString);
        }

        public static string GetCultureInfo(int userID)
        {
            return _securityContext.GetCultureInfo(userID);
        }

        public static string GetCurrencyCultureInfo(int userID)
        {
            return _securityContext.GetCurrencyCultureInfo(userID);
        }

        public static string GetDateCultureInfo(int userID)
        {
            return _securityContext.GetDateCultureInfo(userID);
        }

        public static string GetSetting(string settingName, string defaultValue)
        {
            return _securityContext.GetSettingValue(settingName, defaultValue);
        }


        /// <summary>
        /// Applies the security.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="user">The user.</param> 
        public static void ApplySecurityOnWinForms(System.Windows.Forms.Control control, IUser user)
        {
            foreach (var tempControl in TraverseWindowsControls(control))
            {
                var securedObjectName = string.Format("{0}.{1}", control.GetType(), tempControl.Name);
                var securityInfo = SecurityContext.AuthorizationInfos.FirstOrDefault(tempInfo => tempInfo.ObjectName == securedObjectName);
                if ((securityInfo != null))
                {
                    System.Windows.Forms.MethodInvoker methodInvoker = () => SetControl(tempControl, securityInfo.RightType);
                    control.Invoke(methodInvoker);
                }
            }
        }

        /// <summary>
        /// Logins the specified windows user name.
        /// </summary>
        /// <param name="windowsUserName">Name of the windows user.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static bool Login(string windowsUserName, out IUser user)
        {
            SecurityContext.LoadData(windowsUserName);
            user = SecurityContext.User;
            if (!_isWebApplication)
                CurrentUser = SecurityContext.User;

            return (user != null);
        }

        /// <summary>
        /// Logins the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static LoginResults Login(string userName, string password, out IUser user)
        {
            var securityUser = new UserManager(_connectionString).GetUserProfileByName(userName);
            if (securityUser == null)
            {
                user = null;
                return LoginResults.InvalidUser;
            }

            if (securityUser.IsUserTerminated ?? false)
            {
                user = null;
                return LoginResults.IsUserTerminated;
            }

            if (securityUser.isLocked)
            {
                user = null;
                return LoginResults.MaxAttemptsExceeded;
            }
            else if (!securityUser.isLocked && PreviousLockStatus == true)
            {
                if (_users != null) _users.Clear();
                PreviousLockStatus = false;
            }            

            SecurityContext.LoadData(userName, password);
            user = SecurityContext.User;

            CurrentUser = !_isWebApplication ? SecurityContext.User : null;

            if (user != null)
            {
                //_users = null;

                if (user.isLocked)
                    return LoginResults.MaxAttemptsExceeded;

                if (user.isReset)
                    return LoginResults.LoginReset;
                string sUserName = user.UserName;

                if (_users != null)
                {
                    var others = from otherusers in _users
                                 where otherusers.UserName != sUserName
                                 select otherusers;

                    if (others.Count() > 0)
                    {
                        _users = new List<IUser>();

                        foreach (var item in others)
                        {
                            _users.Add(item);
                        }
                    }

                }

                if ((DateTime.Now - user.PasswordChangeDate).Days > SecurityContext.GetExpiryDays())
                {
                    return LoginResults.PasswordExpired; 
                }
                else
                {
                    if (_users != null) _users.Clear();
                    return LoginResults.LoginSuccesful;
                }
            }



            if (_users == null)
                _users = new List<IUser>();

            _users.Add(new User(userName, password));

            var k = from passwordDictonary in _users
                    where passwordDictonary.UserName == userName
                    select passwordDictonary;


            if (k.Count() >= SecurityContext.GetNumberOfAttempts())
            {
#pragma warning disable 612,618
                var userManager = new UserManager(_connectionString);
#pragma warning restore 612,618

                var currentuser = userManager.GetUserProfileByName(userName);
                if (currentuser != null)
                {
                    userManager.LockUser(currentuser);
                    PreviousLockStatus = true;
                    return LoginResults.MaxAttemptsExceeded;
                }
            }

            return LoginResults.LoginFailed;
        }



        public static bool HasAccess(string objectName)
        {
            if (SecurityContext.AuthorizationInfos == null) return false;
            var securityInfo = SecurityContext.AuthorizationInfos.FirstOrDefault(tempInfo => tempInfo.ObjectName == objectName);

            //bool bHasAccess = false;
            //if (objectName == "CashdeskOperator.MainScreen.cs.AFTSettings")
            //{
            //    bHasAccess = !SecurityContext.IsRegulatoryEnabled() ? SecurityContext.IsAFTEnabledForSite() ? true : false : false;
            //    return bHasAccess;
            //}
            if (securityInfo == null) return false;
            switch (securityInfo.RightType)
            {
                case RightType.Enable:
                    return true;
                case RightType.Disable:
                    return false;
                case RightType.Visible:
                    return true;
                case RightType.Hidden:
                    return false;
                default:
                    return true;
            }
        }

        public static bool HasAccess(IUser objUser, string objectName)
        {
            var authorizationInfo = SecurityContext.GetAuthorizationInfo(objUser.UserName, objUser.Password);
            if (authorizationInfo == null)
                return false;
            var securityInfo = authorizationInfo.FirstOrDefault(tempInfo => tempInfo.ObjectName == objectName);
            if (securityInfo == null)
                return false;
            switch (securityInfo.RightType)
            {
                case RightType.Enable:
                    return true;
                case RightType.Disable:
                    return false;
                case RightType.Visible:
                    return true;
                case RightType.Hidden:
                    return false;
                default:
                    return true;
            }
        }

        public static void SetControl(UIElement control, string controlname)
        {
            if (SecurityContext.AuthorizationInfos == null) return;
            var securityInfo = SecurityContext.AuthorizationInfos.FirstOrDefault(tempInfo => tempInfo.ObjectName == controlname);
            if (securityInfo == null) return;
            switch (securityInfo.RightType)
            {
                case RightType.Enable:
                    control.Visibility = Visibility.Visible;
                    control.IsEnabled = true;
                    break;
                case RightType.Disable:
                    control.Visibility = Visibility.Visible;
                    control.IsEnabled = false;
                    break;
                case RightType.Visible:
                    control.Visibility = Visibility.Visible;
                    break;
                case RightType.Hidden:
                    control.Visibility = Visibility.Hidden;
                    break;
                default:
                    control.IsEnabled = true;
                    control.Visibility = Visibility.Visible;
                    break;
            }
        }
        #endregion

        #region Private Methods

        private static void ApplySecurityOnWpf(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window)
                ((Window)d).Loaded += ClassLoaded;
            if (d is UserControl)
                ((UserControl)d).Loaded += UserControlLoaded;
        }

        static void UserControlLoaded(object sender, RoutedEventArgs e)
        {
            TraverseWpfControl(sender as Control, sender.ToString());
            ((UserControl)sender).Loaded -= UserControlLoaded;
        }

        private static void ClassLoaded(object sender, RoutedEventArgs e)
        {
            TraverseWpfControl(sender as Control, sender.ToString());
            ((Window)sender).Loaded -= ClassLoaded;
        }

        private static void TraverseWpfControl(object root, string parentName)
        {
            var control = root as Control;

            if (control != null)
                SetControl(control, parentName);

            if (!(root is DependencyObject)) return;

            foreach (var child in LogicalTreeHelper.GetChildren((DependencyObject)root))
            {
                if ((child as Control) == null || (child as Control).Name.Length == 0)
                    TraverseWpfControl(child, parentName);
                else
                    TraverseWpfControl(child, parentName + "." + ((Control)child).Name);
            }
        }

        private static IEnumerable<System.Windows.Forms.Control> TraverseWindowsControls(System.Windows.Forms.Control control)
        {
            foreach (System.Windows.Forms.Control tempControl in control.Controls)
            {
                yield return tempControl;
                foreach (System.Windows.Forms.Control childControl in TraverseWindowsControls(tempControl))
                    yield return childControl;
            }
        }

        /// <summary>
        /// Sets the control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="rightType">Type of the right.</param>
        private static void SetControl(System.Windows.Forms.Control control, RightType rightType)
        {
            switch (rightType)
            {
                case RightType.Enable:
                    control.Visible = true;
                    control.Enabled = true;
                    break;
                case RightType.Disable:
                    control.Visible = true;
                    control.Enabled = false;
                    break;
                case RightType.Visible:
                    control.Visible = true;
                    break;
                case RightType.Hidden:
                    control.Visible = false;
                    break;
                default:
                    control.Enabled = true;
                    control.Visible = true;
                    break;
            }
        }

        #endregion

        [Serializable]
        public class InvalidPasswordException : ApplicationException
        {
            public InvalidPasswordException(string message) : base(message) { }

            public InvalidPasswordException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }

        [Serializable]
        public class InvalidFunctionCall : ApplicationException
        {
            public InvalidFunctionCall(string message) : base(message) { }

            public InvalidFunctionCall(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }

        [Flags]
        public enum LoginResults
        {
            LoginSuccesful,
            LoginFailed,
            PasswordExpired,
            MaxAttemptsExceeded,
            LoginReset,
            InvalidUser,
            IsUserTerminated
        }


    }
}
