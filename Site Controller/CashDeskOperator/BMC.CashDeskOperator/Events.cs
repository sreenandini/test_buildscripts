using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Business.CashDeskOperator;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Common.LogManagement;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class EventsBusinessObject : IEventDetails
    {
        #region Private Variables

        Events events = new Events();

        #endregion

        #region Constructor

        private EventsBusinessObject() { }

        #endregion

        #region Public Functions

        public DataTable GetEventDetails(DateTime startDate, DateTime endDatetime, string strBarPos,
                                        int showCleared, string strEventType, int iPageSize, int LegalEvent)
        {
            return events.GetEventDetails(startDate, endDatetime, strBarPos, showCleared, strEventType, iPageSize, LegalEvent);
        }

        public bool CheckForUnclearedEvents()
        {
            return events.CheckForUnclearedEvents();
        }

        public string FillEventType()
        {
            return events.FillEventType();
        }

        public bool UpdateEventDetails(string clearType, string eventType, int eventNo, int installationNo)
        {
            LogManager.WriteLog("Inside Method", LogManager.enumLogLevel.Info);
            return events.UpdateEventDetails(clearType, eventType, eventNo, installationNo);
        }
        #endregion

        #region Public Static Function
        public static IEventDetails CreateInstance()
        {
            return new EventsBusinessObject();
        }
        #endregion
    }
}
