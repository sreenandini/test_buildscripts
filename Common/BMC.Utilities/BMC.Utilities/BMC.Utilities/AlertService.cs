using BMC.BusinessClasses;
using BMC.BusinessClasses.BusinessLogic;
using BMC.BusinessClasses.Interfaces;
using BMC.Common.ConfigurationManagement;
using BMC.Common.LogManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BMC.Utilities
{
    public partial class AlertService : ServiceBase
    {
        System.Timers.Timer timer;
        System.Timers.Timer _ACtimer;
        private IBMCUtilities _bmcUtility;
        public AlertService()
        {
            InitializeComponent();
            ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            _bmcUtility = UtitlityObjectFactory.GetEnterpriseFactoryObject(true);
            timer = new System.Timers.Timer(Convert.ToInt32(ConfigManager.Read("ProcessInterval")) * 1000);
            _ACtimer = new System.Timers.Timer(Convert.ToInt32(ConfigManager.Read("ACProcessInterval")) * 1000);
            timer.Elapsed += timer_Elapsed;
            _ACtimer.Elapsed += _ACtimer_Elapsed;
        }

        void _ACtimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Convert.ToBoolean(AlertEngine.GetSetting("IsAutoCalendarEnabled")))
                _bmcUtility.ProcessAutoCalendar();
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Convert.ToBoolean(AlertEngine.GetSetting("IsEmailAlertEnabled")))
                _bmcUtility.ProcessEmailAlertData();

        }

        protected override void OnStart(string[] args)
        {
            timer.Start();
            _ACtimer.Start();
        }

        protected override void OnStop()
        {
            _bmcUtility.Stop();
            timer.Stop();
            _ACtimer.Stop();
         
        }
    }
}
