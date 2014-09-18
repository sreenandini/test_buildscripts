using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.DBInterface.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using System.Data.Linq;
using BMC.Transport;

namespace BMC.Business.CashDeskOperator
{
    public class GamePlayDetails : MachineManagerLazyInitializer
    {
        public IEnumerable<SessionGamePlayDetails> GetSessionGamePlayDetails()
        {
            LinqDataAccessDataContext linq = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);

            try
            {
                return linq.GetSessionGamePlayDetails();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return null;
            }
        }

        public IEnumerable<ActiveSessionInstallations> GetInstallationForActiveSession()
        {
            LinqDataAccessDataContext linq = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);

            try
            {
                return linq.GetInstallationForActiveSession();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return null;
            }
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
    }
}
