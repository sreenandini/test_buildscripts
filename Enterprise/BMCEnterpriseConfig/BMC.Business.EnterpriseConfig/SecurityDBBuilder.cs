using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using BMC.Common.ConfigurationManagement;
using BMC.Common.LogManagement;
using BMC.DataAccess;
using Microsoft.Win32;

namespace BMC.Business.EnterpriseConfig
{
    public static class SecurityDBBuilder
    {
        #region "Declarations"
        private static SqlConnection objsqlConn = null;
        private static string strConnect = "";
        private static string strKey = string.Empty;
        private static bool boolUseHex = true;
        //private static RegistryKey RegKey;
        private static string strSQLConnect = "";
        #endregion

        /// <summary>
        ///  This function establishes the connection to the database.
        /// </summary>
        /// <param></param>
        /// <param></param>
        public static SqlConnection GetConnection()
        {
            try
            {
                //Getting Connection String from the Configuration File
                string str_TypeOfConnection = ConfigurationManager.AppSettings["ConnectionFlag"];
                if (str_TypeOfConnection.ToUpper() == "TRUE")
                {//Check whether the Flag is true
                    if (objsqlConn == null)
                    {
                        strConnect = ConfigManager.Read("ConnectionString");
                        objsqlConn = new SqlConnection(strConnect);

                    }
                }
                else
                {
                    //if (objsqlConn == null)
                    //{
                    //    ////Get the connection string 
                    //    RegKey = Registry.LocalMachine.OpenSubKey("Software\\Honeyframe\\Cashmaster");
                    //    strSQLConnect = RegKey.GetValue("SQLConnect").ToString();
                    //    //When the connection string is already there before changes (without encryption), then DO NOT decrypt
                    //    if (!strSQLConnect.ToUpper().Contains("SERVER"))
                    //    {
                    //        BGSGeneral.cConstants objBGSConstants = new BGSGeneral.cConstants();
                    //        BGSEncryptDecrypt.clsBlowFish objDecrypt = new BGSEncryptDecrypt.clsBlowFish();
                    //        strKey = objBGSConstants.ENCRYPTIONKEY;

                    //        strSQLConnect = objDecrypt.DecryptString(ref strSQLConnect, ref strKey, ref boolUseHex);
                    //        objsqlConn = new SqlConnection(strSQLConnect);
                    //    }

                    //    RegKey.Close();
                    //}
                    if (objsqlConn == null)
                    {
                        ////Get the connection string 
                        //RegKey = Registry.LocalMachine.OpenSubKey("Software\\Honeyframe\\Cashmaster");
                        //strSQLConnect = RegKey.GetValue("SQLConnect").ToString();
                        //When the connection string is already there before changes (without encryption), then DO NOT decrypt
                        strSQLConnect = BMC.Common.Utilities.DatabaseHelper.GetConnectionString();
                        objsqlConn = new SqlConnection(strSQLConnect);

                        //RegKey.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                //  MessageBox.Show("Error while retrieving ConnectionString" + ex.Message, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LogManager.WriteLog("Error while retrieving ConnectionString:" + ex.Message.ToString(), LogManager.enumLogLevel.Error);
                BMC.Common.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error while retrieving ConnectionString" + ex.Message, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LogManager.WriteLog("Error while retrieving ConnectionString:" + ex.Message.ToString(), LogManager.enumLogLevel.Error);
                BMC.Common.ExceptionManagement.ExceptionManager.Publish(ex);
            }

            return objsqlConn;
        }


        /// <summary>
        ///  This function gets the parameters collection for retrieving User Details
        /// </summary>
        /// <param name="meterObject">MeterHistoryTranport</param>
        public static SqlParameter[] GetUserSpParameters(SecurityProperty meterObject)
        {
            SqlParameter[] sp_parames = null;
            if (meterObject != null)
            {
                //Call the SP with MHIDs to convert
                sp_parames = new SqlParameter[2];
                sp_parames[0] = new SqlParameter(SecuirtyConstants.strStaffUName, meterObject.UserName);
                sp_parames[1] = new SqlParameter(SecuirtyConstants.strPwd, meterObject.Password);
            }
            return sp_parames;
        }


        /// <summary>
        ///  This function retrives the user details
        /// </summary>
        /// /// <param name="baseObject">MeterHistoryTransport</param>
        public static DataSet UserDetails(SecurityProperty baseObject)
        {
            //Check for Active Installation           
            DataSet objdsUser = null;
            string strspName = "rsp_GetUserDetails";
            try
            {
                objdsUser = SqlHelper.ExecuteDataset(GetConnection(), CommandType.StoredProcedure, strspName, GetUserSpParameters(baseObject));
            }
            catch (Exception ex)
            {
                objdsUser = null;
                //MessageBox.Show("Error while retrieving User Details", strCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LogManager.WriteLog("Error while retrieving User Details" + ex.Message, LogManager.enumLogLevel.Error);
                BMC.Common.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return objdsUser;
        }
    }

    public static class SecuirtyConstants
    {
        public static string strUserID = "@USERID";
        public static string strPwd = "@PWD";
        public static string strUserLevel = "@USERLEVEL";
        public static string strUserAddress = "@UADDRESS";
        public static string strUserPhone = "@UPHONE";
        public static string strStaffUName = "@UName";
        public static string strUserEndDate = "@UENDDATE";
    }
}
