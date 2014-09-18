using System;
using System.Collections.Generic;
using System.Text;
using BMC.DBInterface.ExchangeConfig;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;

namespace BMC.Business.ExchangeConfig
{
    public static class Credentials
    {
        /// <summary>
        /// Retrieve the server details from connection string
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns>Dictionary with server details</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha        09-Dec-2008        Intial Version 
        /// 
        public static Dictionary<string, string> RetrieveServerDetails(string ConnectionString)
        {
            string strServerName = string.Empty;
            Dictionary<string, string> ReturnDetails = new Dictionary<string, string>();
            string[] strConnection=null;
            try
            {

                if (ConnectionString != string.Empty)
                {
                    strConnection = ConnectionString.Split(';');

                    foreach (string sConn in strConnection)
                    {
                        if (sConn.ToUpper().Contains("SERVER"))
                        {
                            if (sConn.ToUpper().Contains("\\"))
                            {
                                string[] arrServer = sConn.Split('\\');

                                ReturnDetails.Add("SERVER", arrServer[0].Substring(arrServer[0].IndexOf("=") + 1));
                                ReturnDetails.Add("INSTANCE", arrServer[1]);
                            }
                            else
                            {
                                ReturnDetails.Add("SERVER", sConn.Substring(sConn.IndexOf("=") + 1));
                            }
                        }
                        if (sConn.ToUpper().Contains("UID"))
                        {
                            ReturnDetails.Add("UID", sConn.Substring(sConn.IndexOf("=") + 1));
                        }
                        if (sConn.ToUpper().Contains("PWD"))
                        {
                            ReturnDetails.Add("PASSWORD", sConn.Substring(sConn.IndexOf("=") + 1));
                        }
                        if (sConn.ToUpper().Contains("DATABASE") | sConn.ToUpper().Contains("INITIAL CATALOG"))
                        {
                            ReturnDetails.Add("DATABASE", sConn.Substring(sConn.IndexOf("=") + 1));
                        }
                        if (sConn.ToUpper().Contains("TIMEOUT") | sConn.ToUpper().Contains("CONNECTION TIMEOUT"))
                        {
                            ReturnDetails.Add("TIMEOUT", sConn.Substring(sConn.IndexOf("=") + 1));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("RetrieveServerDetails" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }

            return ReturnDetails;
        }

        /// <summary>
        /// Make the connection string with the credentials
        /// </summary>
        /// <param name="Credentials"></param>
        /// <returns>Connection string</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha        10-Dec-2008        Intial Version 
        /// 

        public static string MakeConnectionString(Dictionary<string, string> Credentials)
        {
            string strConnectionstring = string.Empty;
            try
            {
                if (Credentials != null)
                {
                    foreach (KeyValuePair<string, string> objKeyValue in Credentials)
                    {
                        strConnectionstring += objKeyValue.Key + "=" + objKeyValue.Value + ";";
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("MakeConnectionString" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return strConnectionstring;
        }


        /// <summary>
        /// Test the connection with the DB.
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns>success or failure</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha        10-Dec-2008        Intial Version 
        /// 

        public static bool TestConnectionDB(string ConnectionString)
        {

            return DBBuilder.TestConnectionDB(ConnectionString);
        }


        /// <summary>
        /// Check if the user credentials are valid
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns>success or failure</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha         05-Jan-2009        Intial Version 
        /// 
        public static bool Checkuser(string strUser,string strPass)
        {
            bool bResult = false;
            SecurityAuthenticate objSecurity = null;

            try
            {   
                objSecurity = new SecurityAuthenticate();
                SecurityProperty objProperty = new SecurityProperty();
                objProperty.UserName = strUser;
                objProperty.Password = strPass;

                bResult = objSecurity.ValidateUser(objProperty);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Checkuser" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return bResult;
        }
    }
}
