using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BMC.Common.ExceptionManagement;
using BMC.Common.Interfaces;
using BMC.Common.LogManagement;
using Microsoft.Win32;
using BMC.Common.Utilities;

namespace BMC.Common.ConfigurationManagement
{
    public class RegistryConfigurationAdapter : IConfigurationAdapter
    {
        String strRegistryString;
        public static RegistryConfigurationAdapter Instance;
        //Constructor
        public RegistryConfigurationAdapter()
        { }
        public RegistryConfigurationAdapter(string strRegistryString)
        {
            ///Intialize stuff
            this.strRegistryString = strRegistryString;
        }

        /// <summary>
        /// Static method which returns the object of DBConfigurationAdapter to provide Singleton object
        /// </summary>
        /// <param name="strConnectionString">Database connection string</param>
        /// <returns></returns>
        public static RegistryConfigurationAdapter GetRegistryConfigurationAdapterObject(string strRegistryString)
        {
            //No object created till now. Create new one and send it to caller
            if (Instance == null)
            {
                Instance = new RegistryConfigurationAdapter(strRegistryString);
            }
            //Returning objec without cretaing new one again
            return Instance;
        }

        /// <summary>
        /// Reads from Configuration setting
        /// </summary>
        /// <param name="strKey">Key to get the value from Config</param>
        public string Read(string strKey)
        {
            return BMCRegistryHelper.GetRegKeyValueByInstallationType(strKey, string.Empty);
        }

        /// <summary>
        /// Reads from Configuration setting
        /// </summary>
        /// <param name="strKey">Key to get the value from Config</param>
        /// <param name="defaultValue"></param>
        public static string Read(string strKey, string defaultValue)
        {
            return BMCRegistryHelper.GetRegKeyValueByInstallationType(strKey, defaultValue);
        }

        /// <summary>
        /// Write into the Configuration Setting
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <param name="strValue">Value</param>
        /// <returns>Success code</returns>
        public bool Write(string strKey, string strValue)
        {
            return false;
        }

        /// <summary>
        ///Modifies teh Key in configuration 
        /// </summary>
        /// <param name="strKey">Key to modify</param>
        /// <param name="strValue">New Value</param>
        /// <returns></returns>
        bool IConfigurationAdapter.Modify(string strKey, string strValue)
        {
            return false;
        }

        /// <summary>
        /// Reads Connection String details from Configuration 
        /// </summary>
        /// <param name="strKey">Key to get the value from Config</param>
        /// <returns>Returns the Connection String as string </returns>
        string IConfigurationAdapter.ReadConnectionString(string strKey)
        {
            RegistryKey key = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(ConfigManager.Read(Constants.CONSTANT_REGISTRYPATH), true);
            string connectionString = key.GetValue(strKey).ToString();
            key.Close();
            return connectionString;
        }

        /// <summary>
        /// Writes Connection String details to Configuration 
        /// </summary>
        /// <param name="strKey">Key to get the value from Config</param>
        /// <returns>Writes the Connection String to configuration </returns>
        bool IConfigurationAdapter.WriteConnectionString(string strKey, string strValue)
        { return false; }


        /// <summary>
        /// Reads Section details from Configuration file
        /// </summary>
        /// <param name="strKey">Key to get the value from Config</param>
        /// <returns>Returns the Section </returns>
        string IConfigurationAdapter.ReadSection(string strKey) { return string.Empty; }

        /// <summary>
        /// Free, if any occupide resources
        /// </summary>
        /// <returns>Success Code</returns>
        public bool UnInit()
        {
            return false;
        }

    }
}
