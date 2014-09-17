using System;
using System.Collections.Generic;
using System.Text;
using BMC.DBInterface.EnterpriseConfig;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;

namespace BMC.Business.EnterpriseConfig
{
    public static class Credentials
    {
         
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
                        if (sConn.ToUpper().Contains("DATABASE"))
                        {
                            ReturnDetails.Add("DATABASE", sConn.Substring(sConn.IndexOf("=") + 1));
                        }
                        if (sConn.ToUpper().Contains("CONNECTION TIMEOUT"))
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


        

        public static bool TestConnectionDB(string ConnectionString)
        {

            return DBBuilder.TestConnectionDB(ConnectionString);
        }      
    }
}
