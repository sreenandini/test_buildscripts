﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.BusinessClasses.Interfaces
{
    public interface ISTMExportDetails
    {
        bool Start();
        void Stop();
    }

    public interface IJob
    {
        void Init();

        void DoWork();

        void UnInit();

        bool CheckSiteStatus();

        string GetSettingDetail(string strSetting);

    }
}
