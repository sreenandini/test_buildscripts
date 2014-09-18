using BMC.DBInterface.CashDeskOperator;
using BMC.Transport.CashDeskOperatorEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BMC.Business.CashDeskOperator
{
    public class PlayerClubBiz
    {
        /// <summary>
        /// Initially will get the EpiDetails using installation.
        /// With the account details from the epidetails, will get the Player details
        /// </summary>
        /// <param name="InstallationNo"></param>
        /// <returns></returns>
        public DataTable RetrievePlayerDetails_CMP(int InstallationNo)
        {
            AnalysisDataAccess analysisBusinessObject = new AnalysisDataAccess();
            DataTable dtPlayerInfo = null;
            {
                dtPlayerInfo = analysisBusinessObject.GetPlayerDetails(InstallationNo);
            }
            return dtPlayerInfo;
        }
    }
}
