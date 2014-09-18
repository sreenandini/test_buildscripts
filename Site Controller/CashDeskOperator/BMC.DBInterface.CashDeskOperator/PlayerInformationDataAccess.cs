using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.DataAccess;
using Microsoft.Win32;
using BMC.Common.ConfigurationManagement;
using BMC.Common.Utilities;

namespace BMC.DBInterface.CashDeskOperator
{
    public class PlayerInformationDataAccess
    {
        #region "Private Variables"

        CommonDataAccess commonDataAccess = new CommonDataAccess();

        #endregion

        #region "Public Properties"
        #endregion

        #region "Private Function"
        #endregion

        #region "Public Function"

        /// <summary>
        /// Retrieve the setting value
        /// </summary>
        /// <param name="strConnect"></param>
        /// <returns>dictionary</returns>
        public Dictionary<string, string> GetCMPCredentials(string strConnect)
        {
            Dictionary<string, string> objDicCMPDetails = new Dictionary<string, string>();
            try
            {
                //Get the CMP Kiosk URL
                SqlParameter[] sqlparams = CommonDataAccess.GetSettingParameterDB(DBConstants.CONSTANT_RSP_GETCMPKIOSKURL);
                string strCMPURL = commonDataAccess.ExecuteQuery(strConnect, sqlparams);
                if (!String.IsNullOrEmpty(strCMPURL))
                {
                    objDicCMPDetails.Add("CMPURL", strCMPURL);
                }

                //Get the CMP Application USERNAME
                SqlParameter[] sqlparams1 = CommonDataAccess.GetSettingParameterDB(DBConstants.CONSTANT_RSP_GETCMPAPPUSER);
                string strCMPUSER = commonDataAccess.ExecuteQuery(strConnect, sqlparams1);
                if (!String.IsNullOrEmpty(strCMPUSER))
                {
                    objDicCMPDetails.Add("CMPUSER", strCMPUSER);
                }

                //Get the CMP Application Password
                SqlParameter[] sqlparams2 = CommonDataAccess.GetSettingParameterDB(DBConstants.CONSTANT_RSP_GETCMPAPPPWD);
                string strCMPPWD = commonDataAccess.ExecuteQuery(strConnect, sqlparams2);
                if (!String.IsNullOrEmpty(strCMPPWD))
                {
                    objDicCMPDetails.Add("CMPPWD", strCMPPWD);
                }
            }


            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return objDicCMPDetails;
        }

        public bool CheckAccountNumber(string AccountNumber)
        {
            XmlNode objNode = null;
            XmlDocument objXmlDoc = new XmlDocument();

            bool IsAvailable = false;

            try
            {
                //Load the xml with prize information.
                string XmlPath = System.Windows.Forms.Application.StartupPath + "\\XMLData\\Players.xml";
                if (System.IO.File.Exists(XmlPath))
                {
                    objXmlDoc.Load(XmlPath);
                    XmlNodeList objPlayerList = objXmlDoc.SelectNodes("//PlayerInfo", null);

                    if (objPlayerList.Count > 0)
                    {
                        for (int PlayerCount = 0; PlayerCount < objPlayerList.Count; PlayerCount++)
                        {
                            objNode = objPlayerList[PlayerCount];

                            if (objNode.SelectSingleNode("AccountNumber").InnerText == AccountNumber)
                            {
                                IsAvailable = true;
                                break;
                            }
                            else
                            {
                                IsAvailable = false;
                            }
                        }
                    }
                }
                else
                {
                    LogManager.WriteLog("File not found", LogManager.enumLogLevel.Warning);
                    //return false;
                }
                return IsAvailable;
            }
            catch (Exception ex)
            {
                //LogManager.WriteLog("Check Account Number" + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        /// <summary>
        /// Update the redeemed points.
        /// </summary>
        /// <param name=""></param>
        /// <returns >bool</returns>    
        public bool UpdateUnitCashPointValue(string strPrizeName, int PointstobeRedeemed, float CashValue)
        {
            XmlNode objNode = null;
            XmlNode objNewNode = null;
            XmlNode objOldNode = null;
            bool IsUpdated = false;
            XmlDocument objXmlDoc = new XmlDocument();

            try
            {
                string XmlPath = System.Windows.Forms.Application.StartupPath + "\\XMLData\\PlayerClub.xml";
                if (System.IO.File.Exists(XmlPath))
                {
                    objXmlDoc.Load(XmlPath);
                    XmlNodeList objPlayerList = objXmlDoc.SelectNodes("//PrizeInfo", null);

                    if (objPlayerList.Count > 0)
                    {
                        for (int i = 0; i < objPlayerList.Count; i++)
                        {
                            objNode = objPlayerList[i];

                            if (objNode.SelectSingleNode("PrizeName").InnerText == strPrizeName)
                            {
                                objOldNode = objNode.SelectSingleNode("RedeemPoints");
                                objNewNode = objXmlDoc.CreateElement("RedeemPoints");
                                objNewNode.InnerText = PointstobeRedeemed.ToString();

                                objNode.ReplaceChild(objNewNode, objOldNode);                             
                                objXmlDoc.Save(XmlPath);

                                objOldNode = objNode.SelectSingleNode("AuthAward");
                                objNewNode = objXmlDoc.CreateElement("AuthAward");
                                objNewNode.InnerText = CashValue.ToString();
                                
                                objNode.ReplaceChild(objNewNode, objOldNode);

                                objXmlDoc.Save(XmlPath);
                                IsUpdated = true;
                                break;
                            }
                            else
                            {
                                IsUpdated = false;
                            }
                        }
                    }
                }
                else
                {
                    LogManager.WriteLog("File not found", LogManager.enumLogLevel.Warning);                   
                }
                return IsUpdated;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public bool CheckForPrefixSuffixSetting()
        {
            string bHasSuffix = "";
            string bHasPrefix = "";
            bool bResult = false;

            /*RegistryKey regKeyConnectionString = null;
            try
            {

                regKeyConnectionString = Registry.LocalMachine.OpenSubKey("Software\\Honeyframe\\Cashmaster\\Exchange");
                if (regKeyConnectionString != null)
                {
                    bHasSuffix = regKeyConnectionString.GetValue("HasSuffix").ToString();

                    regKeyConnectionString = Registry.LocalMachine.OpenSubKey("Software\\Honeyframe\\Cashmaster\\Exchange");
                    if (regKeyConnectionString != null)
                    {
                        bHasPrefix = regKeyConnectionString.GetValue("HasPrefix").ToString();

                        regKeyConnectionString.Close();

                        if (bHasPrefix == "1" && bHasSuffix == "1")
                        {
                            bResult = true;
                        }
                        else
                        {
                            bResult = false;
                        }
                    }
                    bResult = false;
                }
                return bResult;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }*/

            try
            {

                bHasSuffix = BMCRegistryHelper.GetRegKeyValue("Cashmaster\\Exchange", "HasSuffix");
                bHasPrefix = BMCRegistryHelper.GetRegKeyValue("Cashmaster\\Exchange", "HasPrefix");
                return ((bHasPrefix == "1") && (bHasSuffix == "1"));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        /// <summary>
        /// To check whether Enable Redeem Receipt printer is enabled from the setting table.
        /// </summary>
        /// <param name=""></param>
        /// <returns >true or false</returns>
        public bool CheckEnableRedeemPrintCDODB()
        {
            string strproc = "CheckEnableRedeemPrintCDODB";
            bool bEnableRedeemPrint = false;
            try
            {
                SqlParameter[] sqlparams = CommonDataAccess.GetSettingParameterDB(DBConstants.CONST_SP_PARAM_ENABLEREDEEMPRINTCDO);
                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, sqlparams);
                if (sqlparams[3].Value != null || sqlparams[3].Value.ToString() != string.Empty)
                {
                    if (Convert.ToString(sqlparams[3].Value).ToUpper() == "TRUE")
                    { bEnableRedeemPrint = true; }
                    else
                    { bEnableRedeemPrint = false; }
                }
                else
                {
                    bEnableRedeemPrint = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(strproc + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                bEnableRedeemPrint = false;
            }
            return bEnableRedeemPrint;
        }

        /// <summary>
        /// To fetch the common card information from the setting table
        /// </summary>
        /// <param name=""></param>
        /// <returns >true or false</returns>
        public string[] GetCardInformation(string cardNumber)
        {
            string[] returnValue = new string[3];

            try
            {
                SqlParameter SettingValueForCardNumberFormat = DataBaseServiceHandler.AddParameter<string>("@Setting_Value", DbType.String, cardNumber, ParameterDirection.InputOutput);
                SettingValueForCardNumberFormat.Size = 20;
                DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetSetting",
                    DataBaseServiceHandler.AddParameter<string>("@Setting_Name", DbType.String, "CardNumberFormat"),
                    DataBaseServiceHandler.AddParameter<string>("@Setting_Default", DbType.String, @";([A-Z,a-z,0-9])*\?"),
                    SettingValueForCardNumberFormat);

                returnValue[0] = SettingValueForCardNumberFormat.Value.ToString();

                SqlParameter SettingValueForInnerCardNumberFormat = DataBaseServiceHandler.AddParameter<string>("@Setting_Value", DbType.String, cardNumber, ParameterDirection.InputOutput);
                SettingValueForInnerCardNumberFormat.Size = 20;
                DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetSetting",
                    DataBaseServiceHandler.AddParameter<string>("@Setting_Name", DbType.String, "InnerCardNumberFormat"),
                    DataBaseServiceHandler.AddParameter<string>("@Setting_Default", DbType.String, @"([A-Z,a-z,0-9])*"),
                    SettingValueForInnerCardNumberFormat);

                returnValue[1] = SettingValueForInnerCardNumberFormat.Value.ToString();

                SqlParameter SettingValueForNumberOfCharToTrim = DataBaseServiceHandler.AddParameter<string>("@Setting_Value", DbType.String, cardNumber, ParameterDirection.InputOutput);
                SettingValueForNumberOfCharToTrim.Size = 20;
                DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetSetting",
                    DataBaseServiceHandler.AddParameter<string>("@Setting_Name", DbType.String, "NumberOfCharToTrim"),
                    DataBaseServiceHandler.AddParameter<string>("@Setting_Default", DbType.String, @"4"),
                    SettingValueForNumberOfCharToTrim);

                returnValue[2] = SettingValueForNumberOfCharToTrim.Value.ToString();
            }

            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return returnValue;
        }

        #endregion

        #region "Public Static Function"
        #endregion

    }
}
