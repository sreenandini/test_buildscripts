using System;
using System.Collections.Generic;
using System.Text;
using BMC.Common.Interfaces;

namespace BMC.Common.ConfigurationManagement
{
    class DBConfigurationAdapter : IConfigurationAdapter
    {
        public static DBConfigurationAdapter Instance;
        //Constructor
        public DBConfigurationAdapter() { }
        public DBConfigurationAdapter(string strConnectionString)
        {
            ///Intialize stuff
        }

        /// <summary>
        /// Static method which returns the object of DBConfigurationAdapter to provide Singleton object
        /// </summary>
        /// <param name="strConnectionString">Database connection string</param>
        /// <returns></returns>
        public static DBConfigurationAdapter GetDBConfigurationAdapterObject(string strConnectionString)
        {
            //No object created till now. Create new one and send it to caller
            if (Instance == null)
            {
                Instance = new DBConfigurationAdapter(strConnectionString);
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
            return "Feature not available!!!";
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
        /// Modifies the Key value in configuration
        /// </summary>
        /// <param name="strKey">Key to modify</param>
        /// <param name="strValue">New Value</param>
        /// <returns></returns>
        bool IConfigurationAdapter.Modify(string strKey, string strValue)
        {
            return false;
        }

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
