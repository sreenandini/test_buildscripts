using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.Reflection;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;

namespace BMC.Common.Utilities
{
    public class LoggingUtilities
    {
        private string   m_strApplicationName = string.Empty;
        private string   m_strNameSpace = string.Empty;
        private string   m_strMessage = string.Empty;       
        private string   m_strStackTrace = string.Empty;
        private string   m_strInnerException = string.Empty;
        
        private DateTime m_dLogDateTime; 

        public LoggingUtilities(Exception Ex)
        {
            m_strMessage = Ex.Message;
            m_strStackTrace = Ex.StackTrace;
            m_strInnerException = Ex.InnerException.Message;
            m_strNameSpace = Ex.Source;
            m_dLogDateTime = DateTime.Now;
            //Logging shouldnt fail just because we dont have app name in config
            try
            {
                //Calling assembly for a webservice is from a library in the unmanaged IIS process. And so the GetEntryAssembly would return null.
                if (Assembly.GetEntryAssembly() != null)
                {
                    string[] arrAssemblyInfo = Assembly.GetEntryAssembly().FullName.Split(',');
                    //arrAssemblyInfo contains , separated info with version information etc, the first of which is the app name
                    m_strApplicationName = arrAssemblyInfo[0];
                }
                else
                {
                    m_strApplicationName = ConfigManager.Read("ApplicationName");
                }

            }
            catch (Exception)
            {
                //do not log exception here as we can only log into eventviewer at this point. logging for each object call makes event log full
                m_strApplicationName = "Unknown";
            }
        }

        public LoggingUtilities()
        {
            try
            {
                //Logging shouldnt fail just because we dont have app name in config
                try
                {
                    //Calling assembly for a webservice is from a library in the unmanaged IIS process. And so the GetEntryAssembly would return null.
                    if (Assembly.GetEntryAssembly() != null)
                    {
                        string[] arrAssemblyInfo = Assembly.GetEntryAssembly().FullName.Split(',');
                        //arrAssemblyInfo contains , separated info with version information etc, the first of which is the app name
                        m_strApplicationName = arrAssemblyInfo[0];
                    }
                    else
                    {
                        m_strApplicationName = ConfigManager.Read("ApplicationName");
                    }

                }
                catch (Exception)
                {
                    //do not log exception here as we can only log into eventviewer at this point. logging for each object call makes event log full
                    m_strApplicationName = "Unknown";
                }
                m_strStackTrace = GetLoggingMethod();
                m_dLogDateTime = DateTime.Now; 
            }
            catch (Exception ex)
            {
                m_strApplicationName = "Unknown";
                ExceptionManager.PublishInternalException(ex, null);
            }
        }

        public LoggingUtilities(string strLogMessage)
        {
            try
            {
                //Logging shouldnt fail just because we dont have app name in config
                try
                {
                    //Calling assembly for a webservice is from a library in the unmanaged IIS process. And so the GetEntryAssembly would return null.
                    if (Assembly.GetEntryAssembly() != null)
                    {
                        string[] arrAssemblyInfo = Assembly.GetEntryAssembly().FullName.Split(',');
                        //arrAssemblyInfo contains , separated info with version information etc, the first of which is the app name
                        m_strApplicationName = arrAssemblyInfo[0];
                    }
                    else
                    {
                        m_strApplicationName = ConfigManager.Read("ApplicationName");
                    }

                }
                catch (Exception)
                {
                    //do not log exception here as we can only log into eventviewer at this point. logging for each object call makes event log full
                    m_strApplicationName = "Unknown";
                }
                m_strMessage = strLogMessage;
                m_dLogDateTime = DateTime.Now;
                m_strStackTrace = GetLoggingMethod(); //m_strStackTrace will have method name for normal logging.
                m_strInnerException = "NA";
            }
            catch (Exception ex)
            {
                ExceptionManager.PublishInternalException(ex, null);
            }
        }

        public LoggingUtilities(string strLogMessage, string strLogNameSpace)
        {
            m_strMessage   = strLogMessage;
            m_strNameSpace = strLogNameSpace;
            m_dLogDateTime = DateTime.Now; 
        }


        public string ApplicationName
        {
            get { return m_strApplicationName; }
        }

        public string NameSpace
        {
            get { return m_strNameSpace; }
            set { m_strNameSpace = value; }
        }

        
        public string Message
        {
            get { return m_strMessage; }
            set { m_strMessage = value; }
        }

        public string LogDateTime
        {
            get { return m_dLogDateTime.ToString(); }
        }       

        public string StackTrace
        {
            get { return m_strStackTrace; }
        }

        public string InnerException
        {
            get { return m_strInnerException; }
        }


        public string ReturnXMLForLogging()
        {
            XmlDocument objLoggingUtilities   = new XmlDocument();
            
            try
            {
                //Elements for log details xml
                XmlNode objNodeApplication = objLoggingUtilities.CreateNode(XmlNodeType.Element, "Application", "");
                XmlNode objNodeNameSpace = objLoggingUtilities.CreateNode(XmlNodeType.Element, "AppNameSpace", "");
                XmlNode objNodeMessage = objLoggingUtilities.CreateNode(XmlNodeType.Element, "Message", "");
                XmlNode objNodeInnerException = objLoggingUtilities.CreateNode(XmlNodeType.Element, "InnerException", "");
                XmlNode objStackTrace = objLoggingUtilities.CreateNode(XmlNodeType.Element, "StackTrace", "");

                //Form an XML using the elements
                //objLoggingUtilities.DocumentElement
                objLoggingUtilities.ChildNodes[0].AppendChild(objNodeApplication);
                objLoggingUtilities.ChildNodes[0].AppendChild(objNodeNameSpace);
                objLoggingUtilities.ChildNodes[0].AppendChild(objNodeMessage);
                objLoggingUtilities.ChildNodes[0].AppendChild(objNodeInnerException);
                objLoggingUtilities.ChildNodes[0].AppendChild(objStackTrace);
            }
            catch (Exception ex)
            {
                ExceptionManager.PublishInternalException(ex,null);
            }

            return objLoggingUtilities.OuterXml;


        }

        public string ReturnFormattedTextForLogging()
        {
            int iSeparatorWidth = 3;
            StringBuilder objSBLogMessage = new StringBuilder();
            try
            {
                
                objSBLogMessage.Append(m_strApplicationName.ToString().PadRight(20, ' '));
                objSBLogMessage.Append(" ".PadRight(iSeparatorWidth));
                objSBLogMessage.Append(m_dLogDateTime.ToString().PadRight(20, ' '));
                objSBLogMessage.Append(" ".PadRight(iSeparatorWidth));
                objSBLogMessage.Append(m_strNameSpace.PadRight(15, ' '));
                objSBLogMessage.Append(" ".PadRight(iSeparatorWidth));
                objSBLogMessage.Append(m_strStackTrace.PadRight(50, ' '));
                objSBLogMessage.Append(" ".PadRight(iSeparatorWidth));
                objSBLogMessage.Append(m_strMessage.PadRight(50, ' '));
                objSBLogMessage.Append(" ".PadRight(iSeparatorWidth));
                objSBLogMessage.Append(m_strInnerException.PadRight(50, ' '));
            }
            catch (Exception ex)
            {
                ExceptionManager.PublishInternalException(ex, null);
            }

            return objSBLogMessage.ToString();
                 
        }

        private string GetLoggingMethod()
        {
            string strLoggingRequestMethod = string.Empty;
            try
            {
                StackTrace objStackTrace = new StackTrace();
                
                //Frame index is 4 as follows . Logging request method -> LogManger.WriteLog -> callee method -> this method.
                StackFrame objStackFrame = objStackTrace.GetFrame(4);
                MethodBase objMethodBase = objStackFrame.GetMethod();
                strLoggingRequestMethod = objMethodBase.Name;
            }
            catch (Exception ex)
            {
                ExceptionManager.PublishInternalException(ex, null);
            }

            return strLoggingRequestMethod;
            
        }

        private Hashtable GetArrayListForFormingXML()
        {
            Hashtable objLogTable = new Hashtable();
            try
            {
            objLogTable.Add("ApplicationName", ApplicationName);
            objLogTable.Add("NameSpace", NameSpace);
            objLogTable.Add("LogDateTime", LogDateTime);
            objLogTable.Add("Message", Message);
            objLogTable.Add("StackTrace",StackTrace);
            objLogTable.Add("InnerException",InnerException);
            }
            catch (Exception ex)
            {
                ExceptionManager.PublishInternalException(ex, null);
            }

            return objLogTable;

        }
    }
}
