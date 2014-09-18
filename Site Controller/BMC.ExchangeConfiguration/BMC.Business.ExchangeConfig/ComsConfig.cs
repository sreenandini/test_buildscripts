using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml.Linq;

namespace BMC.Business.ExchangeConfig
{
    public class ComsConfig
    {
        /// <summary>
        /// Write a Com Config file
        /// #Sample#
        /// <?xml version="1.0" encoding="utf-8"?>
        /// <configuration>
        ///     <client>
        ///         <AppSetting>
        ///             <ConfigLocation type="CSTRING">C:\Program Files\Bally Technologies\Exchange Client\CommClient.config</ConfigLocation>
        ///             <BindIPAddress type="CSTRING">10.2.108.10</BindIPAddress>
        ///             <Default_Server_Ip type="CSTRING">10.2.108.10</Default_Server_Ip>
        ///         </AppSetting>
        ///     <common>
        ///         <ServerIPPort type="DWORD">6810</ServerIPPort>
        ///         <BlockingCallTimeOut type="DWORD">10000</BlockingCallTimeOut>
        ///      </common>
        ///     <Log>
        ///         <ClientLog type="DWORD">1</ClientLog>
        ///         <ExchangeDir type="CSTRING">C:\CashmasterExchange</ExchangeDir>
        ///     </Log>
        ///     </client>
        /// </configuration>
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="sFullPath"></param>
        /// <param name="DefaultServerIP"></param>
        /// <param name="BindServerIP"></param>
        public static bool SaveClientConfig(string RootDirectory, string CommConfigFileName, string DefaultServerIP, string BindServerIP, List<string> ReplicateLocations)
        {
            string PROC = "SaveConfig";
            bool bStatus = true;
            string _sCashmasterExchange = "";
            string CommConfigFileLocation = RootDirectory + Path.DirectorySeparatorChar + CommConfigFileName;
            bool _bCashmasterConfig = Convert.ToBoolean(ConfigurationManager.AppSettings["CreateCashmasterFolderInsideLogs"].ToString() ?? "false");
            if (_bCashmasterConfig)
            {
                _sCashmasterExchange = BMCRegistryHelper.GetRegKeyValue(string.Empty, "DefaultLogDir", "C:\\Logs") + Path.DirectorySeparatorChar + "CashmasterExchange";
            }
            else
            {
                _sCashmasterExchange = (ConfigurationManager.AppSettings["CashmasterExchangeLogDrive"] ?? "c:") + Path.DirectorySeparatorChar + "CashmasterExchange";
            }
            try
            {
                //Frames xml elements with default values
                XElement xConfig = new XElement("configuration",
                                        new XElement("client",
                                            new XElement("AppSetting",
                                                new XElement("ConfigLocation", new XAttribute("type", "CSTRING"), CommConfigFileLocation),
                                                new XElement("BindIPAddress", new XAttribute("type", "CSTRING"), BindServerIP),
                                                new XElement("Default_Server_Ip", new XAttribute("type", "CSTRING"), DefaultServerIP)
                                                ),
                                            new XElement("common",
                                                new XElement("ServerIPPort", new XAttribute("type", "DWORD"), 6810),
                                                new XElement("BlockingCallTimeOut", new XAttribute("type", "DWORD"), 10000)
                                                ),
                                            new XElement("Log",
                                                new XElement("ClientLog", new XAttribute("type", "DWORD"), 1),
                                                new XElement("ExchangeDir", new XAttribute("type", "CSTRING"), _sCashmasterExchange),
                                                new XElement("SocketLog", new XAttribute("type", "DWORD"), 1)
                                                )
                                            )
                                        );

                //Writes to a new file
                if (!WriteConfig(xConfig, CommConfigFileLocation, CommConfigFileName, RootDirectory, ReplicateLocations))
                {
                    bStatus = false;
                }
                //xConfig.Save(CommConfigFileLocation);
                //if (ReplicateLocations != null)
                //{
                //    CopyConfigFiles(CommConfigFileName, CommConfigFileLocation, RootDirectory, ReplicateLocations);
                //}
            }
            catch (Exception ex)
            {
                bStatus = false;
                LogManager.WriteLog(PROC + ": " + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }

            return bStatus;
        }

        public static bool SaveServerConfig(string RootDirectory, string CommConfigFileName, string DefaultServerIP, string BindServerIP, string dhcpServerIP, int EnableDhcp, string multicastip, string interfaceip, int EncryptEnable, int RSAEnable, int DisMachineWhenRemove, List<string> ReplicateLocations)
        {
            string PROC = "SaveConfig";
            bool bStatus = true;
            string _sCashmasterExchange = "";
            string CommConfigFileLocation = RootDirectory + Path.DirectorySeparatorChar + CommConfigFileName;
            bool _bCashmasterConfig = Convert.ToBoolean(ConfigurationManager.AppSettings["CreateCashmasterFolderInsideLogs"].ToString() ?? "false");
            if (_bCashmasterConfig)
            {
                _sCashmasterExchange = BMCRegistryHelper.GetRegKeyValue(string.Empty, "DefaultLogDir", "C:\\Logs") + Path.DirectorySeparatorChar + "CashmasterExchange";
            }
            else
            {
                _sCashmasterExchange = (ConfigurationManager.AppSettings["CashmasterExchangeLogDrive"] ?? "c:") + Path.DirectorySeparatorChar + "CashmasterExchange";
            }

            try
            {
                //Frames xml elements with default values
                XElement xConfig = new XElement("configuration",
                                        new XElement("client",
                                            new XElement("AppSetting",
                                            new XElement("ConfigLocation", new XAttribute("type", "CSTRING"), CommConfigFileLocation),
                                            new XElement("BindIPAddress", new XAttribute("type", "CSTRING"), BindServerIP),
                                            new XElement("Default_Server_Ip", new XAttribute("type", "CSTRING"), DefaultServerIP)
                                        ),
                                        new XElement("common",
                                            new XElement("ServerIPPort", new XAttribute("type", "DWORD"), 6810),
                                            new XElement("BlockingCallTimeOut", new XAttribute("type", "DWORD"), 10000)
                                        ),
                                        new XElement("Log",
                                            new XElement("ClientLog", new XAttribute("type", "DWORD"), 1),
                                            new XElement("ExchangeDir", new XAttribute("type", "CSTRING"), _sCashmasterExchange),
                                            new XElement("SocketLog", new XAttribute("type", "DWORD"), 1)
                                        )),
                                        new XElement("server",
                                            new XElement("AppSetting",
                                                new XElement("dhcp",
                                                    new XElement("ServerIP", new XAttribute("type", "CSTRING"), dhcpServerIP),
                                                    new XElement("NetMask", new XAttribute("type", "CSTRING"), "255.255.0.0"),
                                                    new XElement("GatewayIP", new XAttribute("type", "CSTRING"), "10.0.0.1"),
                                                    new XElement("DNSIP", new XAttribute("type", "CSTRING"), "10.0.0.1"),
                                                    new XElement("LeaseTime", new XAttribute("type", "DWORD"), 6850)
                                                    ),
                                                new XElement("exchange",
                                                    new XElement("EnableDhcp", new XAttribute("type", "DWORD"), EnableDhcp),
                                                    new XElement("multicastip", new XAttribute("type", "CSTRING"), multicastip),
                                                    new XElement("interface", new XAttribute("type", "CSTRING"), interfaceip),
                                                    new XElement("DisMachineWhenRemove", new XAttribute("type", "DWORD"), DisMachineWhenRemove),
                                                    new XElement("EncryptEnable", new XAttribute("type", "DWORD"), EncryptEnable),
                                                    new XElement("RSAEnable", new XAttribute("type", "DWORD"), RSAEnable)
                                                    )
                                            )
                                        ));

                //Writes to a new file
                if (!WriteConfig(xConfig, CommConfigFileLocation, CommConfigFileName, RootDirectory, ReplicateLocations))
                {
                    bStatus = false;
                }
            }
            catch (Exception ex)
            {
                bStatus = false;
                LogManager.WriteLog(PROC + ": " + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }

            return bStatus;
        }

        /// <summary>
        /// Writes a new commconfig file
        /// </summary>
        /// <param name="xmlConfig"></param>
        /// <param name="CommConfigFileLocation"></param>
        /// <param name="ConfigFile"></param>
        /// <param name="Root"></param>
        /// <param name="DirectoryList"></param>
        /// <returns></returns>
        private static bool WriteConfig(XElement xmlConfig, string CommConfigFileLocation, string ConfigFile, string Root, List<string> DirectoryList)
        {
            string PROC = "WriteConfig";

            try
            {
                xmlConfig.Save(CommConfigFileLocation);

                if (DirectoryList != null)
                {
                    CopyConfigFiles(ConfigFile, CommConfigFileLocation, Root, DirectoryList);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(PROC + ": " + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        /// <summary>
        /// Copy CommConfig file to other service folders
        /// </summary>
        /// <param name="ConfigFile"></param>
        /// <param name="strSourceFileFullPath"></param>
        /// <param name="Root"></param>
        /// <param name="DirectoryList"></param>
        private static void CopyConfigFiles(string ConfigFile, string strSourceFileFullPath, string Root, List<string> DirectoryList)
        {
            string PROC = "CopyConfigFiles";

            try
            {
                if (File.Exists(strSourceFileFullPath))
                {
                    string FullLocation = string.Empty;

                    foreach (string Folder in DirectoryList)
                    {
                        if (Folder.Trim() == "")
                            continue;

                        FullLocation = Root + Path.DirectorySeparatorChar + Folder.Trim();

                        if (Directory.Exists(FullLocation))
                        {
                            try
                            {
                                File.Copy(strSourceFileFullPath, FullLocation + Path.DirectorySeparatorChar + ConfigFile, true);
                            }
                            catch (Exception ex)
                            {
                                LogManager.WriteLog(PROC + ": " + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
