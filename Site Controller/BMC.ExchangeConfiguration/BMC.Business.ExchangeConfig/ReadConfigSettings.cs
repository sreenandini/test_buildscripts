using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;

namespace BMC.Business.ExchangeConfig
{
     class ReadConfigSettings<T>
    {
        private static Configuration config;       

        /// <summary>
        /// Open the Config file for the section        
        /// <Author>Vineetha Mathew</Author>
        /// <DateCreated>Date Created 08-Dec-2008</DateCreated>
        /// <param ></param>
        /// <returns>Configuration</returns>
        /// </summary>      
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        ///Vineetha Mathew      08-12-2008      Created
        public static Configuration Config
        {

            get
            {
                if (config == null)
                {
                    config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);                    
                    ConfigurationManager.RefreshSection("appSettings");
                }
                return config;
            }
        }
        /// <summary>
        /// To get all the sections from a particular section in the config file        
        /// <Author>Vineetha Mathew</Author>
        /// <DateCreated>Date Created 08-Dec-2008</DateCreated>
        /// <param name="sectionname">string</param>
        /// <returns>sections list</returns>
        /// </summary>      
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        ///Vineetha Mathew      08-12-2008      Created
        public static T GetSection<T>(string sectioname) where T : ConfigurationSection
        {
            T sectionOptions = null;
            try
            {
                sectionOptions = Config.GetSection(sectioname) as T;

            }
            catch (Exception ex)
            {
                ex.Message.ToString();

            }
            return sectionOptions;
        }

        /// <summary>
        /// To get all the keys and values from a section in the config file
        /// <Author>Vineetha Mathew</Author>
        /// <DateCreated>Date Created 08-Dec-2008</DateCreated>
        /// <param name="sectionname">string</param>
        /// <returns>dictionary</returns>
        /// </summary>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        ///Vineetha Mathew      08-12-2008      Created

        public static Dictionary<string,string> GetKeys(string sectionname) 
        {          
            XmlDocument objXml=new XmlDocument() ;
            XmlNode objXmlnode =null;           
            XmlNodeList objXmlNodelist=null;
            Dictionary<string, string> objDictionary = new Dictionary<string, string>();                      
            
            try
            {

              
                //oXml.Load(sPath);
                //objXml.Load(System.Reflection.Assembly.GetExecutingAssembly().Location + ".config");
                if (System.IO.File.Exists(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile) == true)
                {
                    objXml.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                }
                else
                {
                    return null;
                }

                objXmlnode = objXml.DocumentElement.SelectSingleNode("//" + sectionname);

                if (objXmlnode == null)
                {
                    return null;
                }
                else
                {
                    if (objXmlnode.HasChildNodes == true)
                    {
                        objXmlNodelist = objXmlnode.SelectNodes("//" + sectionname);
                    }
                    else
                    {                        
                        return null;
                    }
                    

                    if (objXmlNodelist != null)
                        {
                            foreach (XmlNode oXnode in objXmlNodelist)
                            {
                                
                                if (oXnode != null)
                                {
                                    foreach (XmlNode oXchildnode in oXnode.ChildNodes)
                                    {
                                        if (oXchildnode.Attributes != null)
                                        {
                                            objDictionary.Add(oXchildnode.Attributes.GetNamedItem("key").InnerText, oXchildnode.Attributes.GetNamedItem("value").InnerText);
                                        }
                                    }

                                }
                            }
                          
                            
                        }
                }
                
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetKeys" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
           return objDictionary;
          
        }
    }

    class OptionsConfigHandler : AppSettingsReader
    {
        string skey;
        string svalue;
       // [ConfigurationProperty("ServicesList", IsRequired = true, IsKey = true, DefaultValue = "")]
        public string Skey
        {

            get { return (string)skey; }
           // set { base["ServicesList"] = value; }
            set { skey=value; }
        }

       // [ConfigurationProperty("Port", IsRequired = true, IsKey = false, DefaultValue = "4433")]
        public string SValue
        {
            get { return (string)svalue; }
            set { svalue = value; }
        }
    }

}
 