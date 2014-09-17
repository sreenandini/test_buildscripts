using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Configuration;
using BMC.Common.Interfaces;



namespace BMC.Common
{
    class XMLConfigurationAdapter : IConfigurationAdapter
    {

        #region  Member Variables

        /// <summary>
        /// Variables needed to implement custom configuration file operations
        /// </summary>
        string strFilePath;
        XmlDocument doc;
        
        //Single ton object
        public static XMLConfigurationAdapter Instance;

        #endregion Member Variables

        #region Constructors

        //Constructors
        //Default constructor
        public XMLConfigurationAdapter()
        {
           
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="ConfigFileName">Custom configuration file name</param>
        public XMLConfigurationAdapter(String strXMLFileName)
        {
            doc = new XmlDocument();
            strFilePath = strXMLFileName;
            //Instantiate the variables
            doc.Load(strXMLFileName);
            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Static method which returns the object of XMLConfigurationAdapter to provide Singleton object
        /// </summary>
        /// <param name="strConfigFileName">Configuration File name</param>
        /// <returns></returns>
        public static XMLConfigurationAdapter GetXMLConfigurationAdapterObject(string strXMLFileName)
        {
            //No object created till now. Create new one and send it to caller
            if (Instance == null)
            {
                if (strXMLFileName == string.Empty)
                    Instance = new XMLConfigurationAdapter();
                else
                    Instance = new XMLConfigurationAdapter(strXMLFileName);
            }
            //Returning object without cretaing new one again
            return Instance;
        }

        /// <summary>
        /// Reads from Configuration file
        /// </summary>
        /// <param name="strKey">Key to get the value from Config</param>
        /// <returns>Returns the Value of the key </returns>
        string IConfigurationAdapter.Read(string strKey)
        {
            string Value;
            Value = string.Empty;
            XmlNode objNode;

            objNode = doc.DocumentElement.SelectSingleNode(strKey);
            if (objNode != null)
            {
                
                Value = objNode.InnerText.ToString();                
            }

            return Value;
        }

        /// <summary>
        /// Reads Section details from Configuration file
        /// </summary>
        /// <param name="strKey">Key to get the value from Config</param>
        /// <returns>Returns the Section as XML </returns>
        string IConfigurationAdapter.ReadSection(string strKey)
        {
            string Value;
            Value = string.Empty;
            XmlNode objNode;

            objNode = doc.DocumentElement.SelectSingleNode(strKey);
            if (objNode != null)
            {
                
                Value = objNode.InnerXml.ToString();
            }

            return Value;
           
        }


        /// <summary>
        /// Writes into configuration file
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <param name="strValue">Value</param>
        /// <returns></returns>
        bool IConfigurationAdapter.Write(string strKey, string strValue)
        {
            XmlNode objNode;
            try
            {
               objNode = doc.DocumentElement.SelectSingleNode(strKey);
               if (objNode == null)
               {
                   objNode = doc.CreateElement(strKey);
                   objNode.InnerText = strValue;
                   doc.DocumentElement.AppendChild(objNode);
                   doc.Save(strFilePath);
                   return true;
               }
               else
                   return false;

              

            }
            catch (Exception e)
            {
                // Implement Exception HAndling mechnaism here
                //Log the error Message
                throw new Exception(e.Message);
            }

            
        }

        /// <summary>
        /// Reads Connection String details from Configuration 
        /// </summary>
        /// <param name="strKey">Key to get the value from Config</param>
        /// <returns>Returns the Connection String as string </returns>
        string IConfigurationAdapter.ReadConnectionString(string strKey)
        {
            return "Feature not available";
        }

        /// <summary>
        /// Writes Connection String details to Configuration 
        /// </summary>
        /// <param name="strKey">Key to get the value from Config</param>
        /// <returns>Writes the Connection String to configuration </returns>
        bool IConfigurationAdapter.WriteConnectionString(string strKey, string strValue)
        { return false; }

        /// <summary>
        /// Modifies the key values in configuration
        /// </summary>
        /// <param name="strKey">Key to modify</param>
        /// <param name="strValue">New Value</param>
        /// <returns></returns>
        bool IConfigurationAdapter.Modify(string strKey, string strValue)
        {
            XmlNode objNode;
            try
            {
                objNode = doc.DocumentElement.SelectSingleNode(strKey);
                if (objNode != null)
                {
                    
                    objNode.InnerText = strValue;
                    doc.Save(strFilePath);
                    return true;
                }
                else
                    return false;
               

            }
            catch (Exception e)
            {
                // Implement Exception HAndling mechnaism here
                //Log the error Message
                throw new Exception(e.Message);
            }
            
        }


        bool IConfigurationAdapter.UnInit()
        {
            // throw new Exception("The method or operation is not implemented.");
            return true;
        }

        #endregion
    }
}
