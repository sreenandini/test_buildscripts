using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Reflection;
using BMC.Common.Interfaces;
using BMC.Common.ConfigurationManagement;
using BMC.Common.LogManagement;

namespace BMC.Common.ExceptionManagement
{
    public sealed class ExceptionAdapter : BaseApplicationException, IExceptionPublisher
    {

        #region constructors

        public ExceptionAdapter()
        {
        }

        public ExceptionAdapter(string logName, string applicationName)
        {
            this.m_slogName = logName;
            this.m_sApplicationName = applicationName;
        }

        #endregion

        #region member variables

        // Member variable declarations
        private string m_slogName = string.Empty;
        private string m_sApplicationName = string.Empty;
        private const string TEXT_SEPARATOR = "*********************************************";

        #endregion

        #region public methods

        /// <summary>
        /// Method used to publish exception information and additional information.
        /// </summary>
        public void Publish(Exception exception, NameValueCollection additionalInfo, NameValueCollection configSettings)
        {
            // Load Config values if they are provided.
            if (configSettings != null)
            {
                if (configSettings["applicationName"] != null && configSettings["applicationName"].Length > 0) m_sApplicationName = configSettings["applicationName"];
                if (configSettings["logName"] != null && configSettings["logName"].Length > 0) m_slogName = configSettings["logName"];
            }

            if (additionalInfo == null)
                additionalInfo = base.AdditionalInformation;


            // Create StringBuilder to maintain publishing information.
            StringBuilder strInfo = new StringBuilder();


            // Record the contents of the AdditionalInfo collection.
            if (additionalInfo != null)
            {
                // Record General information.
                strInfo.AppendFormat("{0}General Information {0}{1}{0}Additional Info:", Environment.NewLine, TEXT_SEPARATOR);

                foreach (string i in additionalInfo)
                {
                    strInfo.AppendFormat("{0}{1}: {2}", Environment.NewLine, i, additionalInfo.Get(i));
                }
            }

            if (exception == null)
            {
                strInfo.AppendFormat("{0}{0}No Exception object has been provided.{0}", Environment.NewLine);
            }
            else
            {
                #region Loop through each exception class in the chain of exception objects
                // Loop through each exception class in the chain of exception objects.
                Exception currentException = exception;	// Temp variable to hold InnerException object during the loop.
                int intExceptionCount = 1;				// Count variable to track the number of exceptions in the chain.
                do
                {
                    // Write title information for the exception object.
                    strInfo.AppendFormat("{0}{0}{1}) Exception Information{0}{2}", Environment.NewLine, intExceptionCount.ToString(), TEXT_SEPARATOR);
                    strInfo.AppendFormat("{0}Exception Type: {1}", Environment.NewLine, currentException.GetType().FullName);

                    #region Loop through the public properties of the exception object and record their value

                    // Loop through the public properties of the exception object and record their value.
                    PropertyInfo[] aryPublicProperties = currentException.GetType().GetProperties();
                    NameValueCollection currentAdditionalInfo;
                    foreach (PropertyInfo p in aryPublicProperties)
                    {
                        // Do not log information for the InnerException or StackTrace. This information is 
                        // captured later in the process.
                        if (p.Name != "InnerException" && p.Name != "StackTrace")
                        {
                            if (p.GetValue(currentException, null) == null)
                            {
                                strInfo.AppendFormat("{0}{1}: NULL", Environment.NewLine, p.Name);
                            }
                            else
                            {
                                // Loop through the collection of AdditionalInformation if the exception type is a BaseApplicationException.
                                if (p.Name == "AdditionalInformation" && currentException is BaseApplicationException)
                                {
                                    // Verify the collection is not null.
                                    if (p.GetValue(currentException, null) != null)
                                    {
                                        // Cast the collection into a local variable.
                                        currentAdditionalInfo = (NameValueCollection)p.GetValue(currentException, null);

                                        // Check if the collection contains values.
                                        if (currentAdditionalInfo.Count > 0)
                                        {
                                            strInfo.AppendFormat("{0}AdditionalInformation:", Environment.NewLine);

                                            // Loop through the collection adding the information to the string builder.
                                            for (int i = 0; i < currentAdditionalInfo.Count; i++)
                                            {
                                                strInfo.AppendFormat("{0}{1}: {2}", Environment.NewLine, currentAdditionalInfo.GetKey(i), currentAdditionalInfo[i]);
                                            }
                                        }
                                    }
                                }
                                // Otherwise just write the ToString() value of the property.
                                else
                                {
                                    strInfo.AppendFormat("{0}{1}: {2}", Environment.NewLine, p.Name, p.GetValue(currentException, null));
                                }
                            }
                        }
                    }
                    #endregion

                    // Record the StackTrace with separate label.
                    if (currentException.StackTrace != null)
                    {
                        strInfo.AppendFormat("{0}{0}StackTrace Information{0}{1}", Environment.NewLine, TEXT_SEPARATOR);
                        strInfo.AppendFormat("{0}{1}", Environment.NewLine, currentException.StackTrace);
                    }


                    // Reset the temp exception object and iterate the counter.
                    currentException = currentException.InnerException;
                    intExceptionCount++;
                } while (currentException != null);

                #endregion
            }

            // Write the entry to the log based on config 
            if (m_slogName == "INTERNAL")
                LogManager.WriteLog(strInfo.ToString() + "#" + "Logging", LogManager.enumLogLevel.Error, "EVENTLOG");
            else
                LogManager.WriteLog(strInfo.ToString(), LogManager.enumLogLevel.Error, string.Empty);

        }


        #endregion


        #region IExceptionPublisher Members


        public void Publish(string fileName, Exception exception, NameValueCollection additionalInfo, NameValueCollection configSettings)
        {
            // Load Config values if they are provided.
            if (configSettings != null)
            {
                if (configSettings["applicationName"] != null && configSettings["applicationName"].Length > 0) m_sApplicationName = configSettings["applicationName"];
                if (configSettings["logName"] != null && configSettings["logName"].Length > 0) m_slogName = configSettings["logName"];
            }

            if (additionalInfo == null)
                additionalInfo = base.AdditionalInformation;


            // Create StringBuilder to maintain publishing information.
            StringBuilder strInfo = new StringBuilder();


            // Record the contents of the AdditionalInfo collection.
            if (additionalInfo != null)
            {
                // Record General information.
                strInfo.AppendFormat("{0}General Information {0}{1}{0}Additional Info:", Environment.NewLine, TEXT_SEPARATOR);

                foreach (string i in additionalInfo)
                {
                    strInfo.AppendFormat("{0}{1}: {2}", Environment.NewLine, i, additionalInfo.Get(i));
                }
            }

            if (exception == null)
            {
                strInfo.AppendFormat("{0}{0}No Exception object has been provided.{0}", Environment.NewLine);
            }
            else
            {
                #region Loop through each exception class in the chain of exception objects
                // Loop through each exception class in the chain of exception objects.
                Exception currentException = exception;	// Temp variable to hold InnerException object during the loop.
                int intExceptionCount = 1;				// Count variable to track the number of exceptions in the chain.
                do
                {
                    // Write title information for the exception object.
                    strInfo.AppendFormat("{0}{0}{1}) Exception Information{0}{2}", Environment.NewLine, intExceptionCount.ToString(), TEXT_SEPARATOR);
                    strInfo.AppendFormat("{0}Exception Type: {1}", Environment.NewLine, currentException.GetType().FullName);

                    #region Loop through the public properties of the exception object and record their value

                    // Loop through the public properties of the exception object and record their value.
                    PropertyInfo[] aryPublicProperties = currentException.GetType().GetProperties();
                    NameValueCollection currentAdditionalInfo;
                    foreach (PropertyInfo p in aryPublicProperties)
                    {
                        // Do not log information for the InnerException or StackTrace. This information is 
                        // captured later in the process.
                        if (p.Name != "InnerException" && p.Name != "StackTrace")
                        {
                            if (p.GetValue(currentException, null) == null)
                            {
                                strInfo.AppendFormat("{0}{1}: NULL", Environment.NewLine, p.Name);
                            }
                            else
                            {
                                // Loop through the collection of AdditionalInformation if the exception type is a BaseApplicationException.
                                if (p.Name == "AdditionalInformation" && currentException is BaseApplicationException)
                                {
                                    // Verify the collection is not null.
                                    if (p.GetValue(currentException, null) != null)
                                    {
                                        // Cast the collection into a local variable.
                                        currentAdditionalInfo = (NameValueCollection)p.GetValue(currentException, null);

                                        // Check if the collection contains values.
                                        if (currentAdditionalInfo.Count > 0)
                                        {
                                            strInfo.AppendFormat("{0}AdditionalInformation:", Environment.NewLine);

                                            // Loop through the collection adding the information to the string builder.
                                            for (int i = 0; i < currentAdditionalInfo.Count; i++)
                                            {
                                                strInfo.AppendFormat("{0}{1}: {2}", Environment.NewLine, currentAdditionalInfo.GetKey(i), currentAdditionalInfo[i]);
                                            }
                                        }
                                    }
                                }
                                // Otherwise just write the ToString() value of the property.
                                else
                                {
                                    strInfo.AppendFormat("{0}{1}: {2}", Environment.NewLine, p.Name, p.GetValue(currentException, null));
                                }
                            }
                        }
                    }
                    #endregion

                    // Record the StackTrace with separate label.
                    if (currentException.StackTrace != null)
                    {
                        strInfo.AppendFormat("{0}{0}StackTrace Information{0}{1}", Environment.NewLine, TEXT_SEPARATOR);
                        strInfo.AppendFormat("{0}{1}", Environment.NewLine, currentException.StackTrace);
                    }


                    // Reset the temp exception object and iterate the counter.
                    currentException = currentException.InnerException;
                    intExceptionCount++;
                } while (currentException != null);

                #endregion
            }

            // Write the entry to the log based on config 
            if (m_slogName == "INTERNAL")
                LogManager.WriteLog(strInfo.ToString() + "#" + "Logging", LogManager.enumLogLevel.Error, "EVENTLOG");
            else
                LogManager.WriteLog(fileName, strInfo.ToString(), LogManager.enumLogLevel.Error);

        }

        #endregion
    }
}
