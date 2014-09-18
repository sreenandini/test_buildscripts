using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Common.ExceptionManagement;
using BMC.DataAccess;
using System.Data.SqlClient;

namespace BMC.DBInterface.CashDeskOperator
{
    public class AFTSettingsDataAccess
    {
        public DataTable  GetDenoms()
        {
         return SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure,
                    "rsp_GetAFTDenom").Tables[0]  ;
        }

        public List<Transport.AFTSetting> GetAFTSettingsDetails(int iDenom)
        {
            List<Transport.AFTSetting> lstAFTSettings = new List<Transport.AFTSetting>();
            Transport.AFTSetting tspAFTSettings=null;
            List<string> SettingNames = new List<string>(){"AFTTransactionsAllowed",
                            "AllowCashableDeposits",
                            "AllowNonCashableDeposits",
                            "AllowRedeemOffers",
                            "AllowPointsWithdrawal",
                            "AllowCashWithdrawal",
                            "AllowPartialTransfers",
                            "AutoDepositNonCashableCreditsonCardOut",
                            "AutoDepositCashableCreditsonCardOut",
                            "AllowOffers"};


            DataTable dtAFTSettings = new DataTable();
            try
            {
                SqlParameter[] param=new SqlParameter[1]  ;
               param[0]=new SqlParameter("@Denom", iDenom);
                dtAFTSettings = SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure,
                    "rsp_UIGetAFTSettings", param).Tables[0];


                foreach (DataRow row in dtAFTSettings.Rows)
                {
                    tspAFTSettings = new BMC.Transport.AFTSetting();
                    tspAFTSettings.Name = row["SettingName"].ToString();
                    tspAFTSettings.Value = row["SettingValue"].ToString();
                    try
                    {
                        if (row["SettingValue"] != null)
                        {
                            if (SettingNames.Contains(row["SettingName"].ToString()))
                            {
                                tspAFTSettings.IsCheckBox = row["SettingValue"].ToString() == "0" ? true : 
                                    row["SettingValue"].ToString() == "1" ? true : false;
                            }
                            else
                            {
                                tspAFTSettings.IsCheckBox = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tspAFTSettings.IsCheckBox = false;
                    }
                    if (SettingNames.Contains(row["SettingName"].ToString()))
                    {
                        tspAFTSettings.IsActive = row["SettingValue"].ToString() == "1" ? true : row["SettingValue"].ToString() == "0" ? false : false;
                    }
                    else
                    {
                        tspAFTSettings.IsActive = false;
                    }
                    lstAFTSettings.Add(tspAFTSettings);
                }
                return lstAFTSettings;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return lstAFTSettings;
            }
        }

        public bool SaveAFTSettings(List<Transport.AFTSetting> lstAFTSettings)
        {
            bool bResult = false;
            int Executed = 0;
            SqlParameter[] parames= null;
            try
            {

               
                    parames = new SqlParameter[]{ DataBaseServiceHandler.AddParameter<bool>("AllowEFTTransactions", DbType.Boolean, lstAFTSettings[0].Name.ToUpper() == "AFTTRANSACTIONSALLOWED".Trim() ? Convert.ToBoolean(Convert.ToInt32(lstAFTSettings[0].Value)) : Convert.ToBoolean("false")),
                     DataBaseServiceHandler.AddParameter<bool>("ALLOWCASHABLEDEPOSITS", DbType.Boolean, lstAFTSettings[1].Name.ToUpper() == "ALLOWCASHABLEDEPOSITS".Trim() ? Convert.ToBoolean(Convert.ToInt32(lstAFTSettings[1].Value)) : Convert.ToBoolean("false")),
                     DataBaseServiceHandler.AddParameter<bool>("ALLOWNONCASHABLEDEPOSITS", DbType.Boolean, lstAFTSettings[2].Name.ToUpper() == "ALLOW NON-CASHABLE DEPOSITS".Trim() ? Convert.ToBoolean(Convert.ToInt32(lstAFTSettings[2].Value)) : Convert.ToBoolean("false")),
                     DataBaseServiceHandler.AddParameter<bool>("ALLOWREDEEMOFFERS", DbType.Boolean, lstAFTSettings[3].Name.ToUpper() == "ALLOWREDEEMOFFERS".Trim() ? Convert.ToBoolean(Convert.ToInt32(lstAFTSettings[3].Value)) : Convert.ToBoolean("false")),
                     DataBaseServiceHandler.AddParameter<bool>("ALLOWPOINTSWITHDRAWAL", DbType.Boolean, lstAFTSettings[4].Name.ToUpper() == "ALLOWPOINTSWITHDRAWAL".Trim() ? Convert.ToBoolean(Convert.ToInt32(lstAFTSettings[4].Value)) : Convert.ToBoolean("false")),
                     DataBaseServiceHandler.AddParameter<bool>("ALLOWCASHWITHDRAWAL", DbType.Boolean, lstAFTSettings[5].Name.ToUpper() == "ALLOWCASHWITHDRAWAL".Trim() ? Convert.ToBoolean(Convert.ToInt32(lstAFTSettings[5].Value)) : Convert.ToBoolean("false")),
                     DataBaseServiceHandler.AddParameter<bool>("ALLOWPARTIALTRANSFER", DbType.Boolean, lstAFTSettings[6].Name.ToUpper() == "ALLOWPARTIALTRANSFERS".Trim() ? Convert.ToBoolean(Convert.ToInt32(lstAFTSettings[6].Value)) : Convert.ToBoolean("false")),
                     DataBaseServiceHandler.AddParameter<bool>("ALLOWAUTODEPOSITNONCASHABLECREDIT", DbType.Boolean, lstAFTSettings[7].Name.ToUpper() == "AUTODEPOSITNON-CASHABLECREDITSONCARDOUT".Trim() ? Convert.ToBoolean(Convert.ToInt32(lstAFTSettings[7].Value)) : Convert.ToBoolean("false")),
                     DataBaseServiceHandler.AddParameter<bool>("ALLOWAUTODEPOSITCASHABLECREDIT", DbType.Boolean, lstAFTSettings[8].Name.ToUpper() == "AUTODEPOSITCASHABLECREDITSONCARDOUT".Trim() ? Convert.ToBoolean(Convert.ToInt32(lstAFTSettings[8].Value)) : Convert.ToBoolean("false")),
                     DataBaseServiceHandler.AddParameter<bool>("ALLOWOFFERS", DbType.Boolean, lstAFTSettings[9].Name.ToUpper() == "ALLOWOFFERS".Trim() ? Convert.ToBoolean(Convert.ToInt32(lstAFTSettings[9].Value)) : Convert.ToBoolean("false")),
                     DataBaseServiceHandler.AddParameter<int>("EFTTIMEOUT", DbType.Int32, lstAFTSettings[10].Name.ToUpper() == "EFTTIMEOUTVALUE".Trim() ? Convert.ToInt32(lstAFTSettings[10].Value) : Convert.ToInt32("0")),
                     DataBaseServiceHandler.AddParameter<double>("OPTION1WITHDRAWALAMT", DbType.Double, lstAFTSettings[11].Name.ToUpper() == "OPTION1WITHDRAWALAMOUNT".Trim() ? Convert.ToDouble(lstAFTSettings[11].Value) : Convert.ToDouble("0")),
                     DataBaseServiceHandler.AddParameter<double>("OPTION2WITHDRAWALAMT", DbType.Double, lstAFTSettings[12].Name.ToUpper() == "OPTION2WITHDRAWALAMOUNT".Trim() ? Convert.ToDouble(lstAFTSettings[12].Value) : Convert.ToDouble("0")),
                     DataBaseServiceHandler.AddParameter<double>("OPTION3WITHDRAWALAMT", DbType.Double, lstAFTSettings[13].Name.ToUpper() == "OPTION3WITHDRAWALAMOUNT".Trim() ? Convert.ToDouble(lstAFTSettings[13].Value) : Convert.ToDouble("0")),
                     DataBaseServiceHandler.AddParameter<double>("OPTION4WITHDRAWALAMT", DbType.Double, lstAFTSettings[14].Name.ToUpper() == "OPTION4WITHDRAWALAMOUNT".Trim() ? Convert.ToDouble(lstAFTSettings[14].Value) : Convert.ToDouble("0")),
                     DataBaseServiceHandler.AddParameter<double>("OPTION5WITHDRAWALAMT", DbType.Double, lstAFTSettings[15].Name.ToUpper() == "OPTION5WITHDRAWALAMOUNT".Trim() ? Convert.ToDouble(lstAFTSettings[15].Value) : Convert.ToDouble("0"))};
               
                    Executed = DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, 
                        "usp_UpdateAFTSettings", parames);
                       
               
               
                if (Executed> 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return bResult;
            }
        }
    }
}
