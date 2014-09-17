using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using System.Reflection;
using BMC.Common.LogManagement;

namespace BMC.Common.Utilities
{
    public class XMLUtilities
    {
        private string m_strRootName = string.Empty;
        private string m_strRootValue = string.Empty;
        private XmlDocument m_objXMLDoc = new XmlDocument();
        
        public XMLUtilities()
        {

        }

        public XMLUtilities(string strRootNode)
        {
            m_strRootName = strRootNode;
        }

        public void CreateXMLDocument(string strRootName)
        {

            try
            {
                m_objXMLDoc.Load(m_strRootName);
            }
            catch (Exception)
            {
            }
        }

        public string CreateLoggingXML(LoggingUtilities objLoggingUtilities)
        {
            if (m_strRootName == string.Empty)
                m_strRootName = "LogDetails";
            try
            {
                m_objXMLDoc.LoadXml(FormRootString());
                m_objXMLDoc.ChildNodes[0].AppendChild(AddNode("ApplicationName", objLoggingUtilities.ApplicationName, ""));
                m_objXMLDoc.ChildNodes[0].AppendChild(AddNode("NameSpace", objLoggingUtilities.NameSpace, ""));
                m_objXMLDoc.ChildNodes[0].AppendChild(AddNode("LogDateTime", objLoggingUtilities.LogDateTime, ""));
                m_objXMLDoc.ChildNodes[0].AppendChild(AddNode("StackTrace", objLoggingUtilities.StackTrace, ""));
                m_objXMLDoc.ChildNodes[0].AppendChild(AddNode("InnerException", objLoggingUtilities.InnerException, ""));
                m_objXMLDoc.ChildNodes[0].AppendChild(AddNode("Message", objLoggingUtilities.Message, ""));
            }
            catch (Exception)
            {

            }

            return m_objXMLDoc.OuterXml;
        }

        public string CreateXML(Hashtable objList)
        {
            if (m_strRootName == string.Empty)
                m_strRootName = "Unknown";
            try
            {
                m_objXMLDoc.LoadXml(FormRootString());
                foreach (DictionaryEntry objDE in objList)
                {
                    m_objXMLDoc.ChildNodes[0].AppendChild(AddNode(objDE.Key.ToString(), objDE.Value.ToString(), ""));
                }
  
            }
            catch (Exception)
            {

            }

            return m_objXMLDoc.OuterXml;
        }

        public string CreateXML(Object obj)
        {
            PropertyInfo[] objArrPublicProperties = obj.GetType().GetProperties();
            if (m_strRootName == string.Empty)
                m_strRootName = "Unknown";
            try
            {
                m_objXMLDoc.LoadXml(FormRootString());
                foreach (PropertyInfo objPropertyInfo in objArrPublicProperties)
                {
                    m_objXMLDoc.ChildNodes[0].AppendChild(AddNode(objPropertyInfo.Name, objPropertyInfo.GetValue(obj,null).ToString(), ""));
                }
            }
            catch (Exception)
            {

            }

            return m_objXMLDoc.OuterXml;

        }

        private XmlNode AddNode(string strNodeName,string strNodeValue,string strNameSpaceURI)
        {
            XmlNode objXMLNode = null;

            try
            {
                objXMLNode = m_objXMLDoc.CreateNode(XmlNodeType.Element, strNodeName, strNameSpaceURI);
                objXMLNode.InnerText = strNodeValue;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.ToString(), LogManager.enumLogLevel.Error);
            }

            return objXMLNode;
        }

        public string AddNode(string strXML, string strNodeName, string strNodeValue, string strNameSpaceURI)
        {
            XmlNode objXMLNode = null;

            try
            {
                m_objXMLDoc.LoadXml(strXML);
                objXMLNode = m_objXMLDoc.CreateNode(XmlNodeType.Element, strNodeName, strNameSpaceURI);
                objXMLNode.InnerText = strNodeValue;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.ToString(), LogManager.enumLogLevel.Error);
            }

            return m_objXMLDoc.OuterXml ;
        }

        public string[] ParseXMLReturnStringArray(string strXML)
        {

            ArrayList objArrList = new ArrayList();
           
            try
            {
                m_objXMLDoc.LoadXml(strXML);
                foreach (XmlNode objNode in m_objXMLDoc.ChildNodes[0].ChildNodes)
                {
                    objArrList.Add(objNode.InnerText);
                }
            }
            catch (Exception)
            {

            }

            return (string[])objArrList.ToArray(typeof(string));
         }

            
        public string GetValueforNode(string strXML,string strNodeName)
        {
            string strNodeValue = string.Empty;
            try
            {
                m_objXMLDoc.LoadXml(strXML);
                strNodeValue = m_objXMLDoc.ChildNodes[0].SelectSingleNode(strNodeName).InnerXml;
               
            }
            catch(Exception)
            {
            }

            return strNodeValue;
        }

        public string RootName
        {
            get { return m_strRootName; }
            set { m_strRootName = value; }
        }

        public string RootValue
        {
            get { return m_strRootValue; }
            set { m_strRootValue = value; }
        }

        private string FormRootString()
        {
            string strRootString = "<" + RootName + ">" + RootValue + "</" + RootName + ">";
            return strRootString;
        }
    }
}
