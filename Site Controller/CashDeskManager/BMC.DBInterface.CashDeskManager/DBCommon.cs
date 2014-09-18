using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common;
using BMC.Common.LogManagement;

namespace BMC.DBInterface.CashDeskManager
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
        public static bool CheckPositionToDisplay(string szPositionName,List<string> PositionsToDisplay)
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

                LogManager.WriteLog(ex.Message,LogManager.enumLogLevel.Error);
                isCheckPositionDisplay = true;
            }
            return isCheckPositionDisplay;
        }

        public static bool IsMachineATicketWorkstation(string device)
        {
            try
            {
                Dictionary<string, string> TicketWorkStations = DBBuilder.LoadTicketWorkstations();
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
                return false;
            }
           
        }
        #endregion "Check Positions to display"
    }
}
