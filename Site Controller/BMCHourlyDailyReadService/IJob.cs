using System;
using  System.Diagnostics;
namespace BMC.HourlyDailyReadJobs
{
    public interface IJob
    {
        void Init();        
       
        void DoWork();        
      
        void UnInit();

        bool CheckSiteStatus();

        string GetSettingDetail(string strSetting);
       
    }
}
