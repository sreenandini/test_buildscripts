using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Configuration;
using BMC.Common.Interfaces;



namespace BMC.Common
{
    class AppConfigurationAdapter : IConfigurationAdapter
    {

        #region  Member Variables

        /// <summary>
        /// Variables needed to implement custom configuration file operations
        /// </summary>
        ExeConfigurationFileMap FileMap;
        Configuration Config;
        //Single ton object
        public static AppConfigurationAdapter Instance;

        #endregion Member Variables

        #region Constructors

        //Constructors
        //Default constructor
        public AppConfigurationAdapter()
        {
            FileMap = null;
            Config = null;
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="ConfigFileName">Custom configuration file name</param>
        public AppConfigurationAdapter(String ConfigFileName)
        {
            //Instantiate the variables
            FileMap = new ExeConfigurationFileMap();
            FileMap.ExeConfigFilename = ConfigFileName;
            Config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Static method which returns the object of AppConfigurationAdapter to provide Singleton object
        /// </summary>
        /// <param name="strConfigFileName">Configuration File name</param>
        /// <returns></returns>
        public static AppConfigurationAdapter GetAppConfigurationAdapterObject(string strConfigFileName)
        {
            //No object created till now. Create new one and send it to caller
            if (Instance == null)
            {
                if (strConfigFileName == string.Empty)
                    Instance = new AppConfigurationAdapter();
                else
                    Instance = new AppConfigurationAdapter(strConfigFileName);
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
            
            try
            {
                //Get from Custom Configuration file
                if (FileMap == null)
                {
                    // Open App.Config of executable
                    
                    Value = ConfigurationManager.AppSettings[strKey].ToString();
                }
                //Get from App.config
                else
                {
                    
                    Value = Config.AppSettings.Settings[strKey].Value;
                    
                }
            }
            catch (Exception e)
            {

                //Log the Error Message
                //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                throw e;

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
            try
            {
                //app.config write
                if (Config != null)
                {
                    Config.AppSettings.Settings.Add(strKey, strValue);
                    //Save the configuration file.
                    Config.Save(ConfigurationSaveMode.Modified);
                    //Force a reload of the changed section.
                    ConfigurationManager.RefreshSection("appSettings");
                }
                //Custom config write
                else if (Config == null)
                {
                    // Open App.Config of executable
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    // Add an Application Setting.
                    config.AppSettings.Settings.Add(strKey, strValue);
                    // Save the configuration file.
                    config.Save(ConfigurationSaveMode.Modified);
                    // Force a reload of a changed section.
                    ConfigurationManager.RefreshSection("appSettings");
                }

            }
            catch (Exception e)
            {
                // Implement Exception HAndling mechnaism here
                //Log the error Message
                throw new Exception(e.Message);
            }

            return true;
        }

        /// <summary>
        /// Modifies the key values in configuration
        /// </summary>
        /// <param name="strKey">Key to modify</param>
        /// <param name="strValue">New Value</param>
        /// <returns></returns>
        bool IConfigurationAdapter.Modify(string strKey, string strValue)
        {
            try
            {
                //app.config write
                if (Config != null)
                {
                    Config.AppSettings.Settings[strKey].Value = strValue;
                    //Save the configuration file.
                    Config.Save(ConfigurationSaveMode.Modified);
                    //Force a reload of the changed section.
                    ConfigurationManager.RefreshSection("appSettings");
                }
                //Custom config write
                else if (Config == null)
                {
                    // Open App.Config of executable
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    // Add an Application Setting.
                    config.AppSettings.Settings[strKey].Value = strValue;
                    // Save the configuration file.
                    config.Save(ConfigurationSaveMode.Modified);
                    // Force a reload of a changed section.
                    ConfigurationManager.RefreshSection("appSettings");
                }

            }
            catch (Exception e)
            {
                // Implement Exception HAndling mechnaism here
                //Log the error Message
                throw new Exception(e.Message);
            }

            return true;

        }

        /// <summary>
        /// Reads Connection String details from Configuration 
        /// </summary>
        /// <param name="strKey">Key to get the value from Config</param>
        /// <returns>Returns the Connection String as string </returns>
        string IConfigurationAdapter.ReadConnectionString(string strKey)
        {
            string Value;
            Value = string.Empty;

            try
            {
                //Get from Custom Configuration file
                if (FileMap == null)
                {
                    // Open App.Config of executable

                   
                    Value = ConfigurationManager.ConnectionStrings[strKey].ToString();
                }
                //Get from App.config
                else
                {

                    
                    Value = Config.ConnectionStrings.ConnectionStrings[strKey].ToString();

                }
            }
            catch (Exception e)
            {

                //Log the Error Message
                //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                throw e;

            }
            return Value;

        }

        /// <summary>
        /// Writes Connection String details to Configuration 
        /// </summary>
        /// <param name="strKey">Key to get the value from Config</param>
        /// <returns>Writes the Connection String to configuration </returns>
        bool IConfigurationAdapter.WriteConnectionString(string strName, string strConnectionString)
        {
            ConnectionStringSettings objConnStrSettings;
            objConnStrSettings = new ConnectionStringSettings(strName, strConnectionString);
            ConfigurationSection objSection;
            try
            {
                //Custom
                if (Config != null)
                {                    
                    Config.ConnectionStrings.ConnectionStrings.Add(objConnStrSettings);
                    //Save the configuration file.
                    Config.Save(ConfigurationSaveMode.Modified);
                    //Force a reload of the changed section.
                    ConfigurationManager.RefreshSection("connectionStrings");

                    //Encryption
                    objSection = Config.GetSection("connectionStrings");
                    objSection.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
                    objSection.SectionInformation.ForceSave = true;
                    Config.Save(ConfigurationSaveMode.Full);
                    
                }
                //app.config write
                else if (Config == null)
                {
                    // Open  config of executable
                    Configuration objconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    // Add an Application Setting.
                    objconfig.ConnectionStrings.ConnectionStrings.Add(objConnStrSettings);
                    // Save the configuration file.
                    objconfig.Save(ConfigurationSaveMode.Modified);
                    // Force a reload of a changed section.
                    ConfigurationManager.RefreshSection("connectionStrings");

                    //Encryption
                    objSection = objconfig.GetSection("connectionStrings");
                    objSection.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
                    objSection.SectionInformation.ForceSave = true;
                    objconfig.Save(ConfigurationSaveMode.Full);
                }
              

            }
            catch (Exception e)
            {
                // Implement Exception HAndling mechnaism here
                //Log the error Message
                throw new Exception(e.Message);
            }

            return true;

        }

        /// <summary>
        /// Reads Section details from Configuration file
        /// </summary>
        /// <param name="strKey">Key to get the value from Config</param>
        /// <returns>Returns the Section </returns>
        string IConfigurationAdapter.ReadSection(string strKey) { return string.Empty; }


        bool IConfigurationAdapter.UnInit()
        {
            // throw new Exception("The method or operation is not implemented.");
            return true;
        }

        #endregion
    }
}
