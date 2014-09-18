using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Business.CashDeskOperator;

namespace BMC.CashDeskOperator
{
    public class PlayerClubBusinessObject : IPlayerClub
    {

        #region Declarations
        Kiosk objKiosk = new Kiosk();
        #endregion

        public PlayerClubBusinessObject()
        {

        }
        #region IPlayerClub Members

        Dictionary<string, string> IPlayerClub.RetrievePlayerInfoFromEPI(int InstallationNo)
        {
            return objKiosk.RetrievePlayerDetailsFromEPI(InstallationNo);
        }


        #endregion

        #region Public Static Function
        public static IPlayerClub CreateInstance()
        {
            return new PlayerClubBusinessObject();
        }
        #endregion
    }
}
