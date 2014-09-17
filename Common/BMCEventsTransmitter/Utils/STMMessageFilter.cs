using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using BMC.EventsTransmitter.DataAccess;
using BMC.EventsTransmitter.Utils;

namespace BMC.EventsTransmitter
{
    internal class STMMessageFilter : IRelaucher
    {
        Log Logger = Log.GetInstance(); 
        static Hashtable oFilteredEvents;
        static DataAdapter _DataAdapter;
        static STMMessageFilter _STMMessageFilter;
        private STMMessageFilter()
        {
                if (oFilteredEvents == null)
                {
                   
                    Logger.Debug("STMMessageFilter: Loading....");
                    _DataAdapter = new DataAdapter();
                    oFilteredEvents = _DataAdapter.GetExcludedEvents();
                    Logger.Debug("STMMessageFilter: Loading Complete.");
                }
        }
        public static STMMessageFilter GetInstance()
        {
            if (_STMMessageFilter == null)
            {
                _STMMessageFilter = new STMMessageFilter();
                Relauncher.GetInstance().RegisterForUpdate(_STMMessageFilter);
            }
            return _STMMessageFilter;
        }
        public bool IsFiltered(string MessageNumber)
        {
            try
            { 
                if(oFilteredEvents.Count==0) return false;
                return !(oFilteredEvents.ContainsKey(MessageNumber));
            }
            catch (Exception Ex)
            {
                Logger.Error("STMMessageFilter", "IsFiltered", Ex);
                return false;
            }
        }
        public void RefreshApp()
        {
            Logger.Debug("STMMessageFilter", "RefreshApp", "Refreshing Filters");
            oFilteredEvents = null;
            oFilteredEvents = _DataAdapter.GetExcludedEvents(); // this line of code Can be removed -- automatic refres on null value 
        }
    }
}