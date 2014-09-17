using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Security;
using System.Security.Principal;
using System.Security.Permissions;
using System.Reflection;
using BMC.Common.Interfaces;
using BMC.Common.ConfigurationManagement;

namespace BMC.Common.ExceptionManagement
{
    public static class ExceptionManager
    {

        #region static variables

        private static IExceptionPublisher m_IObjExceptionPublisher = null;
        private static IExceptionPublisher m_IObjInternalExceptionPublisher = null;

        const string EXCEPTIONMANAGEMENT_CONFIG_SECTION = "exceptionManagement";

        #endregion

        #region public methods

        public static void Publish(Exception exception)
        {
            ExceptionManager.Publish(exception, null);
        }

        public static void Publish(string fileName, Exception exception)
        {
            Publish(fileName, exception, null);
        }

        /// <summary>
        /// This method will call publish method of corresponding adapters based on config
        /// This needs further enhancements after discussion
        /// </summary>

        public static void Publish(Exception exception, NameValueCollection additionalInfo)
        {
            try
            {

                // Publish the exception based on Configuration Settings and then may be loop thru the config and log for all of them.

                //commenting below line as of now -let log decide where to log the exceptions.
                //if (ConfigManager.Read(EXCEPTIONMANAGEMENT_CONFIG_SECTION) != null) 
                {
                    //Need to do get values from config here
                    if (m_IObjExceptionPublisher == null)
                        m_IObjExceptionPublisher = GetExceptionObject("APPLICATION");

                    m_IObjExceptionPublisher.Publish(exception, additionalInfo, null);
                }


            }
            catch (Exception exp)
            {
                // Publish the exception thrown within the ExceptionManager.
                //We should be adding additional stuff here so that it will be logged to event viewer
                PublishInternalException(exp, null);
            }
        }


        public static void Publish(string fileName, Exception exception, NameValueCollection additionalInfo)
        {
            try
            {

                // Publish the exception based on Configuration Settings and then may be loop thru the config and log for all of them.

                //commenting below line as of now -let log decide where to log the exceptions.
                //if (ConfigManager.Read(EXCEPTIONMANAGEMENT_CONFIG_SECTION) != null) 
                {
                    //Need to do get values from config here
                    if (m_IObjExceptionPublisher == null)
                        m_IObjExceptionPublisher = GetExceptionObject("APPLICATION");

                    m_IObjExceptionPublisher.Publish(fileName, exception, additionalInfo, null);
                }


            }
            catch (Exception exp)
            {
                // Publish the exception thrown within the ExceptionManager.
                //We should be adding additional stuff here so that it will be logged to event viewer
                PublishInternalException(exp, null);
            }
        }

        /// <summary>
        /// Method to publish internal exception - to be published to eventviewer
        /// This has been coded as seperate method so that we can have futher enhancements.
        /// </summary>

        internal static void PublishInternalException(Exception exception, NameValueCollection additionalInfo)
        {

            //m_IObjExceptionPublisher.Publish(exception, additionalInfo, null);
            m_IObjInternalExceptionPublisher = GetExceptionObject("INTERNAL");
            m_IObjInternalExceptionPublisher.Publish(exception, additionalInfo, null);
        }

        /// <summary>
        /// This method will get the Interface corresponding to the ad`apter object.We just have one adapter for now.
        /// This has been coded as seperate method so that we can have futher enhancements.
        /// </summary>
        /// 

        public static IExceptionPublisher GetExceptionObject(string sExceptionType)
        {
            if (sExceptionType == "APPLICATION")
                return (IExceptionPublisher)(new ExceptionAdapter());
            else //Internal exception which will go to event log
                return (IExceptionPublisher)(new ExceptionAdapter(sExceptionType,"ExceptionManagement"));
        }


        #endregion

    }
}
