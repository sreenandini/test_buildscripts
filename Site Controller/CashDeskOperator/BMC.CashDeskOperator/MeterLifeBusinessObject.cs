using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Business.CashDeskOperator;
using System.Windows.Controls;
namespace BMC.CashDeskOperator.BusinessObjects
{
    public class MeterLifeBusinessObject:IMeterLife
    {
        #region private Variables
        private MeterLife meterLife = new MeterLife();
        
        #endregion

        #region Constructor
        private MeterLifeBusinessObject() { }
        #endregion

        #region Public Function

        public DataSet GetMeterLife(int InstallationNumber)
        {
            return meterLife.GetMeterLife(InstallationNumber);
        }
        #endregion

        #region Public Static Function
        public static IMeterLife CreateInstance()
        {
            return new MeterLifeBusinessObject();
        }
        #endregion

        #region IMeterLife Members

        public void GetCurrentMeters(int InstallationNo)
        {
            meterLife.GetCurrentMeters(InstallationNo);
        }

        public DataSet GetCurrentDayMeters(int InstallationNumber)
        {
            return meterLife.GetCurrentDayMeters(InstallationNumber);
        }

        public void GetCurrentMeters(IList<int> Installations, Action<int, int, int> action)
        {
            meterLife.GetCurrentMeters(Installations, action);
        }

        public void PrintCurrentMeters(ListView lvView,int InstallationNo)
        {
            meterLife.PrintCurrentMeters(lvView, InstallationNo);
        }

        public void GetAssetDetails(int iInstallationNo, ref string SiteCode, ref string AssetNumber, ref string PosNumber)
        {
            meterLife.GetAssetDetails(iInstallationNo, ref SiteCode, ref AssetNumber, ref PosNumber);
        }
        #endregion

    }
}
