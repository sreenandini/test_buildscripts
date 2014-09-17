using System;
using System.Runtime.Serialization;
using System.Reflection;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Collections;
using System.Text;
using System.Security;
using System.Security.Principal;
using System.Security.Permissions;
using System.Collections.Specialized;
using System.Resources;

namespace BMC.Common.ExceptionManagement
{
    public class BaseApplicationException : ApplicationException
    {

        #region Constructors

        public BaseApplicationException()
            : base()
        {
            InitializeEnvironmentInformation();
        }

        public BaseApplicationException(string message)
            : base(message)
        {
            InitializeEnvironmentInformation();
        }

        public BaseApplicationException(string message, Exception inner)
            : base(message, inner)
        {
            InitializeEnvironmentInformation();
        }

        #endregion Constructors

        #region Declare Member Variables

        private string machineName;
        private string appDomainName;
        private string threadIdentity;
        private string windowsIdentity;
        private string applicationname;
        private const string CONFIG_EXCEPTION_LOADING_CONFIGURATION = "Error loading exceptionManagement configuration";
        private const string CONFIG_EXCEPTIONMANAGEMENT_INFOACCESS_EXCEPTION = "Information could not be accessed";
        private const string CONFIG_EXCEPTIONMANAGEMENT_PERMISSION_DENIED = "Permission to access this information has been denied.";
        private const string CONFIG_EXCEPTIONMANAGER_INTERNAL_EXCEPTIONS = "ExceptionManagerInternalException";
        private DateTime createdDateTime = DateTime.Now;

        private static ResourceManager resourceManager = new ResourceManager(typeof(ExceptionManager).Namespace + ".ExceptionManagerText", Assembly.GetAssembly(typeof(ExceptionManager)));

        private NameValueCollection additionalInformation = new NameValueCollection();

        #endregion

        #region Public Properties

        public string MachineName
        {
            get
            {
                return machineName;
            }
        }

        public DateTime CreatedDateTime
        {
            get
            {
                return createdDateTime;
            }
        }

        public string AppDomainName
        {
            get
            {
                return appDomainName;
            }
        }


        public string ThreadIdentityName
        {
            get
            {
                return threadIdentity;
            }
        }

        public string WindowsIdentityName
        {
            get
            {
                return windowsIdentity;
            }
        }

        public NameValueCollection AdditionalInformation
        {
            get
            {
                return additionalInformation;
            }
        }

        public string ApplicationName
        {
            get
            {
                return applicationname;
            }
        }


        #endregion

        #region privatefunctions

        /// <summary>
        /// This function will initialize all the environment variables and will raise appropriate exceptions
        /// incase of any errors
        /// </summary>
        /// 
        private void InitializeEnvironmentInformation()
        {
            try
            {
                machineName = Environment.MachineName;
            }
            catch (SecurityException)
            {
                machineName = CONFIG_EXCEPTIONMANAGEMENT_PERMISSION_DENIED;

            }
            catch
            {
                machineName = CONFIG_EXCEPTIONMANAGEMENT_INFOACCESS_EXCEPTION;
            }

            try
            {
                threadIdentity = Thread.CurrentPrincipal.Identity.Name;
            }
            catch (SecurityException)
            {
                threadIdentity = CONFIG_EXCEPTIONMANAGEMENT_PERMISSION_DENIED;
            }
            catch
            {
                threadIdentity = CONFIG_EXCEPTIONMANAGEMENT_INFOACCESS_EXCEPTION;
            }

            try
            {
                windowsIdentity = WindowsIdentity.GetCurrent().Name;
            }
            catch (SecurityException)
            {
                windowsIdentity = CONFIG_EXCEPTIONMANAGEMENT_PERMISSION_DENIED;
            }
            catch
            {
                windowsIdentity = CONFIG_EXCEPTIONMANAGEMENT_INFOACCESS_EXCEPTION;
            }

            try
            {
                appDomainName = AppDomain.CurrentDomain.FriendlyName;
            }
            catch (SecurityException)
            {
                appDomainName = CONFIG_EXCEPTIONMANAGEMENT_PERMISSION_DENIED;
            }
            catch
            {
                appDomainName = CONFIG_EXCEPTIONMANAGEMENT_INFOACCESS_EXCEPTION;
            }

            try
            {
                applicationname = "ExceptionManagement";
            }
            catch
            {
                appDomainName = CONFIG_EXCEPTIONMANAGEMENT_INFOACCESS_EXCEPTION;
            }

        }
        #endregion privatefunctions
    }
}