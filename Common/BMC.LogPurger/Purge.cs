using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using Microsoft.Win32;
using System.Linq;

namespace BMC.PurgeUtilities
{
    /// <summary>
    /// last modified on 23/12/2009
    /// </summary>
    public class Purge
    {

        public void CleanLogs()
        {
            List<FileInfo> files=null ;
            List<FileInfo> xmlListClass = new List<FileInfo>();
            XmlDocument xdoc = new XmlDocument();
            try
            {
                try
                {
                    xdoc.Load("Purge.xml");
                }
                catch (Exception ex2)
                {
                    LogManager.WriteLog("Invalid XML. XML Cannot be loaded", LogManager.enumLogLevel.Info);
                    ExceptionManager.Publish(ex2);
                    xdoc.LoadXml(GetDefaultValue());
                }

                XmlNode xNode = xdoc.DocumentElement;
                XmlNodeList xList = xNode.SelectNodes(@"/Logs/Log");

                foreach (XmlNode subxNode in xList)
                {

                    string path = subxNode.SelectSingleNode("path").InnerText;
                    DirectoryInfo di = new DirectoryInfo(subxNode.SelectSingleNode("path").InnerText);
                    if (di.Exists)
                    {
                        foreach (XmlNode supersubxNode in xNode.SelectSingleNode(@"/Logs/Log[contains(path, '" + path + "')]/Files"))
                        {
                            string Extension = supersubxNode.Attributes["Extenstions"].Value;
                            string Age = supersubxNode.Attributes["Age"].Value;

                            files = (from file in new DirectoryInfo(subxNode.SelectSingleNode("path").InnerText).GetFiles(supersubxNode.InnerText + "*" + Extension, SearchOption.TopDirectoryOnly)
                                     let modifiedDate = file.LastWriteTime.Date
                                     where (modifiedDate <= DateTime.Now.AddDays(int.Parse(Age) * -1))
                                     select file).ToList<FileInfo>();
                            foreach (FileInfo item in files)
                            {
                                try
                                {
                                    item.Delete();
                                    LogManager.WriteLog(item.Name + " deleted.", LogManager.enumLogLevel.Info);
                                }
                                catch (Exception ex)
                                {
                                    ExceptionManager.Publish(ex);
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            LogManager.WriteLog("********************************************************", LogManager.enumLogLevel.Info);
            LogManager.WriteLog(xmlListClass.Count.ToString() + " files found for deletion.", LogManager.enumLogLevel.Info);
          
            System.Threading.Thread.Sleep(1000);
        }
        //private string getValueFromSetting(string sConfigValue, string sDefaultValue)
        //{
        //    string sSettingValue = string.Empty;

        //    try
        //    {
        //        DataBaseServiceHandler.ConnectionString = GetConnectionString();
        //        System.Data.SqlClient.SqlParameter ReturnValue = DataBaseServiceHandler.AddParameter<string>("Setting_Value", System.Data.DbType.String, "", System.Data.ParameterDirection.Output);
        //        ReturnValue.Size = 50000;

        //        DataBaseServiceHandler.ExecuteScalar<string>(QueryType.Procedure, "rsp_GetSetting",
        //            DataBaseServiceHandler.AddParameter<string>("Setting_Name", System.Data.DbType.String, sConfigValue),
        //            DataBaseServiceHandler.AddParameter<string>("Setting_Default", System.Data.DbType.String, sDefaultValue),
        //            ReturnValue);
        //        return ReturnValue.Value.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //        LogManager.WriteLog("Unable to Connect to DB. Returning Default HardCoded Value from Database", LogManager.enumLogLevel.Error);
        //        return sDefaultValue;
        //    }
        //}

        //private string GetConnectionString()
        //{
        //    string sKey = string.Empty;
        //    bool bUseHex = true;
        //    RegistryKey RegKey;
        //    string SQLConnect = "";
        //    ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
        //    try
        //    {
        //        RegKey = Registry.LocalMachine.OpenSubKey(ConfigManager.Read("RegistryPath"));
        //        SQLConnect = RegKey.GetValue("SQLConnect").ToString();

        //        if (!SQLConnect.ToUpper().Contains("SERVER"))
        //        {

        //            BGSGeneral.cConstants objBGSConstants = new BGSGeneral.cConstants();
        //            BGSEncryptDecrypt.clsBlowFish objDecrypt = new BGSEncryptDecrypt.clsBlowFish();
        //            sKey = objBGSConstants.ENCRYPTIONKEY;
        //            SQLConnect = objDecrypt.DecryptString(ref SQLConnect, ref sKey, ref bUseHex);

        //        }
        //        RegKey.Close();
        //    }

        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("Error reading registry:" + ex.Message.ToString(), LogManager.enumLogLevel.Error);
        //    }

        //    return SQLConnect;
        //}

        private static string GetDefaultValue()
        {
            string DefaultString = @"<Logs>";
            DefaultString = DefaultString + @"<Log>";
            DefaultString = DefaultString + @"<path>C:\Logs</path>";
            DefaultString = DefaultString + @"<Files>";
			DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">CashdeskOperator</File>";
			DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">BGSExchangeMonitor</File>";
			DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".log"">BGSExchangeMonitor</File>";
			DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">DialupServiceLog</File>";
			DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">GuardianLog</File>";
			DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">BGSWSAdmin</File>";
			DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">LogPurger</File>";
			DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">BMCHostService</File>";
			DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">BMCNetworkService</File>";
			DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">ExchangeDialupServiceLog</File>";
			DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">HourlyDailyReadJobs</File>";
			DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">TicketExportService</File>";
			DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">EnterpriseImportExportService</File>";
			DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">ReportService</File>";
			DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">BASWindowsService</File>";
			DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">BMCBASImportWebService</File>";
			DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">BMCBASExportImportService</File>";
			DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">BAS</File>";
            DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">LFMWebService</File>";
            DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">EnterpriseExportService</File>";
            DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".txt"">EnterpriseImportService</File>";
            DefaultString = DefaultString + @"</Files>";
            DefaultString = DefaultString + @"</Log>";
            DefaultString = DefaultString + @"<Log>";
            DefaultString = DefaultString + @"<path>C:\CashmasterExchange</path>";
            DefaultString = DefaultString + @"<Files>";
            DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".old"">Ethernet_Com</File>";
            DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".old"">SesManLog</File>";
            DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".old"">DhcpServerLog</File>";
            DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".old"">ComExchangeServer</File>";
            DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".old"">ComExchangeClient</File>";
            DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".old"">EventLog_</File>";
            DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".old"">FreeFormLog_</File>";
            DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".old"">GIM_MC300</File>";
            DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".old"">GmuLoginLog_</File>";
            DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".old"">TitoLog_</File>";
            DefaultString = DefaultString + @"<File Age =""30"" Extenstions="".old"">PosLog_</File>";
            DefaultString = DefaultString + @"</Files>";
            DefaultString = DefaultString + @"</Log>";
            DefaultString = DefaultString + @"</Logs>";
            return DefaultString;

        }
    }
}
