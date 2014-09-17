namespace BMC.MonitoringService
{
    #region Namespaces

    using System;
    using System.Configuration;

    #endregion Namespaces

    #region Static Class
    
    public static class StaticMembers
    {
        #region Static Variables

        private static int _exchangeMonitorInterval     =   0;
        private static int _enterpriseMonitorInterval   =   0;
        private static int _newSiteMonitorInterval      =   0;        
        private static int _exchangeProxyTimeOut        =   0;
        private static int _exchangeProxyMaxRetries     =   0;
        private static int _siteCommsFaultSource        =   0;
        private static int _siteCommsFailureFaultType   =   0;
        private static int _siteCommsResumedFaultType   =   0;
        
        #endregion Static Variables

        #region Static Properties

        public static int ExchangeMonitorInterval
        {
            get { return StaticMembers._exchangeMonitorInterval; }
            set { StaticMembers._exchangeMonitorInterval = value; }
        }

        public static int EnterpriseMonitorInterval
        {
            get { return StaticMembers._enterpriseMonitorInterval; }
            set { StaticMembers._enterpriseMonitorInterval = value; }
        }

        public static int NewSiteMonitorInterval
        {
            get { return StaticMembers._newSiteMonitorInterval; }
            set { StaticMembers._newSiteMonitorInterval = value; }
        }

        public static int ExchangeProxyTimeOut
        {
            get { return StaticMembers._exchangeProxyTimeOut; }
            set { StaticMembers._exchangeProxyTimeOut = value; }
        }

        public static int ExchangeProxyMaxRetries
        {
            get { return StaticMembers._exchangeProxyMaxRetries; }
            set { StaticMembers._exchangeProxyMaxRetries = value; }
        }

        public static int SiteCommsFaultSource
        {
            get { return StaticMembers._siteCommsFaultSource; }
            set { StaticMembers._siteCommsFaultSource = value; }
        }

        public static int SiteCommsFailureFaultType
        {
            get { return StaticMembers._siteCommsFailureFaultType; }
            set { StaticMembers._siteCommsFailureFaultType = value; }
        }

        public static int SiteCommsResumedFaultType
        {
            get { return StaticMembers._siteCommsResumedFaultType; }
            set { StaticMembers._siteCommsResumedFaultType = value; }
        }

        #endregion Static Properties

        #region Static Constructor

        static StaticMembers()
        {
            ExchangeMonitorInterval     =   Convert.ToInt32(ConfigurationManager.AppSettings.Get("ExchangeMonitorInterval")) * 1000;
            EnterpriseMonitorInterval   =   Convert.ToInt32(ConfigurationManager.AppSettings.Get("EnterpriseMonitorInterval")) * 1000;
            NewSiteMonitorInterval      =   Convert.ToInt32(ConfigurationManager.AppSettings.Get("NewSiteMonitorInterval")) * 60 * 1000;
            ExchangeProxyTimeOut        =   Convert.ToInt32(ConfigurationManager.AppSettings.Get("ExchangeProxyTimeOut")) * 1000;
            ExchangeProxyMaxRetries     =   Convert.ToInt32(ConfigurationManager.AppSettings.Get("ExchangeProxyMaxRetries"));
            SiteCommsFaultSource        =   Convert.ToInt32(ConfigurationManager.AppSettings.Get("SiteCommsFaultSource"));
            SiteCommsFailureFaultType   =   Convert.ToInt32(ConfigurationManager.AppSettings.Get("SiteCommsFailureFaultType"));
            SiteCommsResumedFaultType   =   Convert.ToInt32(ConfigurationManager.AppSettings.Get("SiteCommsResumedFaultType"));
        }

        #endregion Static Constructor
    }

    #endregion Static Class
}
