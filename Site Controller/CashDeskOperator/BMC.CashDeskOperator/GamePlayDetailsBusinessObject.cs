using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CashDeskOperator.BusinessObjects;
using System.Data.Linq;
using BMC.Business.CashDeskOperator;
using BMC.Transport;

namespace BMC.CashDeskOperator
{
    public class GamePlayDetailsBusinessObject : ISessionGamePlayDetails
    {
        #region Private Members
        GamePlayDetails _gamePlayDetails = new GamePlayDetails();
        #endregion        

        #region public Static Function
        public static ISessionGamePlayDetails CreateInstance()
        {
            return new GamePlayDetailsBusinessObject();
        }
        #endregion

        #region ISessionGamePlayDetails Members
        public IEnumerable<SessionGamePlayDetails> GetSessionGamePlayDetails()
        {
            return _gamePlayDetails.GetSessionGamePlayDetails();
        }

        public IEnumerable<ActiveSessionInstallations> GetInstallationForActiveSession()
        {
            return _gamePlayDetails.GetInstallationForActiveSession();
        }

        public void GetCurrentMeters(int InstallationNo)
        {
            _gamePlayDetails.GetCurrentMeters(InstallationNo);
        }
        #endregion
    }
}
