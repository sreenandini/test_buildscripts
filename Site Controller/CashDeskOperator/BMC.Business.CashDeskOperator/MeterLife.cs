using System.Data;
using BMC.DBInterface.CashDeskOperator;
using System.Windows.Controls;
using BMC.Common.ExceptionManagement;
using System.Collections.Generic;
using System;
using System.Threading;

namespace BMC.Business.CashDeskOperator
{
    public class MeterLife : MachineManagerLazyInitializer
    {
        EnrollmentDataAccess enrollmentDataAccess = new EnrollmentDataAccess();
        PrintUtility objPrint = new PrintUtility();
        public DataSet GetMeterLife(int InstallationNumber)
        {
            return enrollmentDataAccess.GetMeterLife(InstallationNumber);
        }

        public DataSet GetCurrentDayMeters(int InstallationNumber)
        {
            return enrollmentDataAccess.GetCurrentDayMeters(InstallationNumber);
        }

        public void GetCurrentMeters(int InstallationNo)
        {
            try
            {
                this.GetMachineManager().MeterForcePeriodic(InstallationNo);
            }
            finally
            {
                this.ReleaseMachineManager();
            }
        }
        public void GetCurrentMeters(IList<int> installations, Action<int, int, int> action)
        {
            try
            {
                MachineManagerInterface machineManager = this.GetMachineManager();
                int i = 1;
                int count = installations.Count;
                foreach (int installationNo in installations)
                {
                    try
                    {
                        if (action != null)
                        {
                            try
                            {
                                action(installationNo, i++, count);
                            }
                            catch { }
                        }
                        machineManager.MeterForcePeriodicForSIandSC(installationNo);
                    }
                    catch (System.Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                    Thread.Sleep(10);
                }
            }
            finally
            {
                this.ReleaseMachineManager();
            }
        }
        public void PrintCurrentMeters(ListView lvView,int InstallationNo)
        {

            var AssetName = string.Empty;
            var PositionName = string.Empty;
            var SerialNo = string.Empty;
            var details = enrollmentDataAccess.GetAssetDetailsForPosition(InstallationNo);
            while (details.Read())
            {
                AssetName = details["Asset_Name"].ToString();
                PositionName = details["Pos_Name"].ToString();
                SerialNo = details["Serial_No"].ToString();
            }
            objPrint.PrintMeterFunction(lvView, BMC.Transport.Settings.SiteName, AssetName,PositionName,SerialNo);
        }

        public void GetAssetDetails(int iInstallationNo, ref string SiteCode, ref string AssetNumber, ref string PosNumber)
        {
            enrollmentDataAccess.GetAssetDetails(iInstallationNo,ref SiteCode,ref AssetNumber,ref PosNumber );
        }

        
    }
}
