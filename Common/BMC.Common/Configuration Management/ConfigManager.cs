using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Xml;
using System.IO;
using BMC.Common.Interfaces;

namespace BMC.Common.ConfigurationManagement
{
    /// <summary>
    /// This class acts as a manager for Configuration Management component
    /// It will create the appropriate class object and return to the client
    /// To avoid ambiguity with ConfigurationManager 
    /// </summary>
    public static class ConfigManager
    {
        # region Member variables and Types

        /// <summary>
        /// Member Variables to set the Intial Settings
        /// </summary>
        public static string ConfigFilePath = string.Empty;
        public static string XMLFilePath = string.Empty;
        public static string ConnectionString = string.Empty;
        public static string RegistryString = string.Empty;

        /// <summary>
        /// Member Variable which contains the configuration setting mode
        /// </summary>
        static ConfigurationMode m_ConfigurationMode;

        /// <summary>
        /// Configuration setting mode XML or Database or registry
        /// </summary>
        public enum ConfigurationMode
        {
            XML = 1,
            Database = 2,
            Registry = 3,
            AppConfig=4

        };

        #endregion

        # region Static Methods

        public static void SetConfigurationMode(ConfigManager.ConfigurationMode iConfigurationMode)
        {
            m_ConfigurationMode = iConfigurationMode;

        }



        /// <summary>
        /// This method will create the required class object 
        /// </summary>
        /// <returns>returns the IConfiguration reference by storing the required class object</returns>
        public static IConfigurationAdapter GetConfigurationObject()
        {
            IConfigurationAdapter objIConfig;
            objIConfig = null;

            if (m_ConfigurationMode == ConfigurationMode.XML)
            {
                objIConfig = XMLConfigurationAdapter.GetXMLConfigurationAdapterObject(XMLFilePath);
            }
            if (m_ConfigurationMode == ConfigurationMode.Database)
            {
                objIConfig = DBConfigurationAdapter.GetDBConfigurationAdapterObject(ConnectionString);
            }
            if (m_ConfigurationMode == ConfigurationMode.Registry)
            {
                objIConfig = RegistryConfigurationAdapter.GetRegistryConfigurationAdapterObject(RegistryString);
            }
            if (m_ConfigurationMode == ConfigurationMode.AppConfig )
            {
                objIConfig = AppConfigurationAdapter.GetAppConfigurationAdapterObject(ConfigFilePath);
            }

            //Load from App.Config is nothing is specified
            if (objIConfig == null)
                objIConfig = AppConfigurationAdapter.GetAppConfigurationAdapterObject(ConfigFilePath);

            return objIConfig;
        }

        /// <summary>
        /// Returns the Value from configuration to the caller
        /// </summary>
        /// <param name="Key">Key in the configuration file</param>
        /// <returns>Value from configuration file</returns>
        public static string Read(string Key)
        {
            //Returns the string according to the Key value
            IConfigurationAdapter IConfig = GetConfigurationObject();
            return IConfig.Read(Key);
        }


        /// <summary>
        /// Writes in Configuration 
        /// </summary>
        /// <param name="Key">Key string to insert</param>
        /// <param name="Value">Value</param>
        /// <returns></returns>
        public static bool Write(string Key, string Value)
        {
            //Write into the configuration file returns Success code
            IConfigurationAdapter IConfig = GetConfigurationObject();
            return IConfig.Write(Key, Value);
        }

        /// <summary>
        /// Modifies Configuration  Setting
        /// </summary>
        /// <param name="Key">Key string to insert</param>
        /// <param name="Value">Value</param>
        /// <returns></returns>
        public static bool Modify(string Key, string Value)
        {
            //Write into the configuration file returns Success code
            IConfigurationAdapter IConfig = GetConfigurationObject();
            return IConfig.Modify (Key, Value);
        }

        /// <summary>
        /// Reads Section details from Configuration file
        /// </summary>
        /// <param name="strKey">Key to get the value from Config</param>
        /// <returns>Returns the Section </returns>
        public static string ReadSection(string strKey) 
        {
            //Returns the string according to the Key value
            IConfigurationAdapter IConfig = GetConfigurationObject();
            return IConfig.ReadSection(strKey); 
            
        }

        /// <summary>
        /// Reads the connection string 
        /// </summary>
        /// <param name="strName">Name</param>
        /// <returns></returns>

        public static string ReadConnectionString(string strName)
        {
            //Returns the string according to the Key value
            IConfigurationAdapter IConfig = GetConfigurationObject();
            return IConfig.ReadConnectionString(strName);
        }

        /// <summary>
        /// Writes in Configuration 
        /// </summary>
        /// <param name="Key">Key string to insert</param>
        /// <param name="Value">Value</param>
        /// <returns></returns>
        public static bool WriteConnectionString(string strName, string strConnectionString)
        {
            //Write into the configuration file returns Success code
            IConfigurationAdapter IConfig = GetConfigurationObject();
            return IConfig.WriteConnectionString(strName, strConnectionString);
        }

        #endregion
    }
}
