using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Business.CashDeskOperator;
using BMC.CashDeskOperator.BusinessObjects;

namespace BMC.CashDeskOperator
{
    public class LiquidationBusinessObject :ILiquidationDetails
    {
        #region Private Variables

        Liquidation liquid = new Liquidation();

        #endregion

       #region Constructor

        private LiquidationBusinessObject() { }

        #endregion

       #region Public Static Function
        public static ILiquidationDetails CreateInstance()
        {
            return new LiquidationBusinessObject();
        }
        #endregion

       

        #region ILiquidationDetails Members

        List<BMC.Transport.LiquidationSummary> ILiquidationDetails.GetLiquidationSummaryDetails(int BatchNo)
        {
            return liquid.GetLiquidationSummaryDetails(BatchNo);
        }

        #endregion

        #region ILiquidationDetails Members


        public void UpdateBatchAdvance(int BatchNo, decimal AdvanceRetailer)
        {
            liquid.UpdateBatchAdvance(BatchNo, AdvanceRetailer);
        }

        #endregion

        #region ILiquidationDetails Members


        public void CalculateRetailerNegative(int BatchNo)
        {
            liquid.CalculateRetailerNegative(BatchNo);
        }

        #endregion

        #region ILiquidationDetails Members


        public string GetSetting(string SettingName)
        {
            return liquid.GetSetting(SettingName);
        }

        #endregion
    }
}
