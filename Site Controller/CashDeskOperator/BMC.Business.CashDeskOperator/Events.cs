using System;
using System.Data;
using System.Xml;
using BMC.DBInterface.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Business.CashDeskOperator.WebServices;

namespace BMC.Business.CashDeskOperator
{
    public class Events
    {
        EventsDataAccess eventsDataAccess = new EventsDataAccess();

        /// <summary>
        /// Returns the datatable with the list of Current Calls.
        /// </summary>
        /// <returns>Datatable</returns>      
        public DataTable GetEventDetails(DateTime startDate, DateTime endDatetime, string strBarPos,
                                        int showCleared, string strEventType, int iPageSize,int LegalEvent)
        {
            try
            {
                switch (strEventType)
                {
                    case "Device Error":
                        return eventsDataAccess.GetEventsDetails(startDate, endDatetime, strBarPos, showCleared, "DeviceError", iPageSize, LegalEvent);
                    case "Game GMU Request":
                        return eventsDataAccess.GetEventsDetails(startDate, endDatetime, strBarPos, showCleared, "GameGMURequest", iPageSize, LegalEvent);
                    case "Printer":
                        return eventsDataAccess.GetEventsDetails(startDate, endDatetime, strBarPos, showCleared, "Printer", iPageSize, LegalEvent);
                    default:
                        return eventsDataAccess.GetEventsDetails(startDate, endDatetime, strBarPos, showCleared, strEventType, iPageSize, LegalEvent);

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        public bool CheckForUnclearedEvents()
        {
            bool unclearedEvents = false;
            try
            {
                LogManager.WriteLog("Inside UnclearedEvents", LogManager.enumLogLevel.Info);
                return unclearedEvents = eventsDataAccess.CheckForUnclearedEvents();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return unclearedEvents;
            }
        }

        /// <summary>
        /// Returns the datatable with the list of Current Calls.
        /// </summary>
        /// <returns>Datatable</returns>      
        public string FillEventType()
        {
            return eventsDataAccess.FillEventType();
        }

        public bool UpdateEventDetails(string clearType, string eventType, int eventNo, int installationNo)
        {
            LogManager.WriteLog("Inside Method", LogManager.enumLogLevel.Info);
            return eventsDataAccess.UpdateEventDetails(clearType, eventType, eventNo, installationNo);
        }
    }
}
