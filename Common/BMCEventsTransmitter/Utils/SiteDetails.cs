using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EventsTransmitter.DataAccess;
using System.Collections;

namespace BMC.EventsTransmitter.Utils
{
    public class SiteDetails:IRelaucher
    {
        static DateTime dtUpdated= DateTime.Now; 
        private string _Region=string.Empty;
        static SiteDetails _SiteDetails=null ;
        static object _Lock = new object();
        static DataAdapter _DataAdapter = null;

        public string Company { get; set; }
        public string Sub_Company { get; set; }
        public string Region { get; set; }
        public string Area { get; set; }
        public string District { get; set; }

        private SiteDetails()
        {
        }

        public static SiteDetails GetInstance()
        {
            lock (_Lock)
            {
                if (_SiteDetails == null || DateTime.Now.Subtract(dtUpdated).TotalMinutes > Settings.RefreshSiteDetailsInMinutes)
                {
                    _SiteDetails = new SiteDetails();
                    _DataAdapter = new DataAdapter();
                     Hashtable _hsSiteDetails = _DataAdapter.GetSiteDetails();
                    _SiteDetails.Area = _hsSiteDetails["Area"].ToString();
                    _SiteDetails.Company = _hsSiteDetails["Company"].ToString();
                    _SiteDetails.District = _hsSiteDetails["District"].ToString();
                    _SiteDetails.Region = _hsSiteDetails["Region"].ToString();
                    _SiteDetails.Sub_Company = _hsSiteDetails["Sub_Company"].ToString();
                    Relauncher.GetInstance().RegisterForUpdate(_SiteDetails);
                    dtUpdated = DateTime.Now;
                }
                return _SiteDetails;
            }
        }

        #region IRelaucher Members

        public void RefreshApp()
        {
            lock (_Lock)
            {
                _SiteDetails = null;
            }
        }

        #endregion
    }
}
