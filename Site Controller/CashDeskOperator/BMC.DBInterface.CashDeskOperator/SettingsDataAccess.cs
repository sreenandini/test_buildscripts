using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using BMC.DataAccess;
using BMC.Common.ExceptionManagement;
using BMC.Common;

namespace BMC.DBInterface.CashDeskOperator
{
    public class SettingsDataAccess
    {
        public DataTable GetSettingsDetailsList()
        {
            DataSet dsSettingsDetails = new DataSet();
            try
            {
                dsSettingsDetails = SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetSettingsDetails");

                if (dsSettingsDetails.Tables.Count > 0)
                {
                    return dsSettingsDetails.Tables[0];
                }
                else
                {
                    return new DataTable();
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }


        public DataSet GetSettingsDetails()
        {
            DataSet dsSettingsDetails = new DataSet();
            try
            {
                dsSettingsDetails = SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetSettingsDetails");

                if (dsSettingsDetails.Tables.Count > 0)
                {
                    return dsSettingsDetails; ;
                }
                else
                {
                    return new DataSet();
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataSet();
            }
        }



        public string FillSettingsToBeSkipped()
        {
            string strSettings = string.Empty;

            try
            {
                strSettings = CommonDataAccess.GetSettingValue("SETTING_NAMES_HIDDEN");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strSettings = string.Empty;
            }
            return strSettings;
        }
    }
}
