using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Reflection;
using BMC.Transport;
using BMC.Business.CashDeskOperator;
using System.Collections.ObjectModel;


namespace BMC.CashDeskOperator
{


    /// <summary>
    /// Get GMU details in list
    /// </summary>
    public class GMUsettings
    {

        private UpdateGMUsettingBiz GMUBiz = new UpdateGMUsettingBiz();
        private static GMUsettings _oGMUsettings;

        private GMUsettings() { }

        public static GMUsettings CreateInstance()
        {
            if (_oGMUsettings == null)
            {
                _oGMUsettings = new GMUsettings();

            }

            return _oGMUsettings;

        }

        public List<GetGMUPosDetailsResult> GetUpdateGMISetting(string IPList)
        {
            return GMUBiz.GetUpdateGMISetting(IPList);
        }

        public void GetUpdateGMISetting(string IPList, ObservableCollection<GetGMUPosDetailsResult> result,
            Func<GetGMUPosDetailsResult, bool> doWork, Action afterWork)
        {
            GMUBiz.GetUpdateGMISetting(IPList, result, doWork, afterWork);
        }
    }
}
