using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Transport;
using BMC.DBInterface.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using System.Data;

namespace BMC.Business.CashDeskOperator
{
    public class Liquidation
    {
        LiquidationDataAccess liquidationDataAccess = new LiquidationDataAccess();

        public List<LiquidationSummary> GetLiquidationSummaryDetails(int BatchNo)
        {
            return liquidationDataAccess.GetLiquidationSummaryDetails(BatchNo);
        }

        public void UpdateBatchAdvance(int BatchNo, decimal AdvanceRetailer)
        {
            liquidationDataAccess.UpdateBatchAdvance(BatchNo, AdvanceRetailer);
        }
        public void CalculateRetailerNegative(int BatchNo)
        {
            liquidationDataAccess.CalculateRetailerNegative(BatchNo);
        }

        public string GetSetting(string settingname)
        {
            string Setting_Value = string.Empty;
            Setting_Value = liquidationDataAccess.GetSetting(settingname);

            return Setting_Value;
        }

        public DataTable GetBatchNoForLiquidation()
        {
            try
            {
                return liquidationDataAccess.GetBatchNoForLiquidation();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }


    }
}
