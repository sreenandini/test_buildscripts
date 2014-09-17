using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;

namespace BMC.EnterpriseDataAccess.CashierTransations
{
    public static class DBCommon
    {
        #region "Check Positions to display"
        /// <summary>
        /// Method to Check Positions to display.
        /// </summary>
        /// <param name="Bar Position Name"></param>
        /// <returns>success or failure</returns>
        /// Method Revision History
        /// Author:    Anuradha
        /// Created:   30 April 2009
        /// 
        public static bool CheckPositionToDisplay(string szPositionName, List<string> PositionsToDisplay)
        {
            bool isCheckPositionDisplay = false;
            try
            {
                //Check for each position
                foreach (string sPosition in PositionsToDisplay)
                {
                    if (sPosition.Equals(szPositionName) || sPosition.ToString() == "--Any--")
                    {
                        isCheckPositionDisplay = true;
                        return isCheckPositionDisplay;
                    }
                }
                isCheckPositionDisplay = false;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                isCheckPositionDisplay = true;
            }
            return isCheckPositionDisplay;
        }

        public static bool IsMachineATicketWorkstation(string device,string SITE)
        {
            try
            {
                CashDeskManagerDataAccess cdmAccess = new CashDeskManagerDataAccess();
                Dictionary<string, string> TicketWorkStations = cdmAccess.LoadTicketWorkstations(SITE);
                if (!string.IsNullOrEmpty(TicketWorkStations[device]))
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
                return false;
            }

        }
        #endregion "Check Positions to display"
    }
}
