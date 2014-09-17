using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BMC.UI.EnterpriseConfig
{
   public static class UIConstants
    {
       
       //Enterprise related
       public const string strSQLConnect = "\\Settings\\SQLConnect";
       public const string strEnterpriseKey = "\\Settings\\EnterpriseKey";
       public const string strLGEConnection = "LGEConnectionDetails";
       public const string StartUpPath = "Software\\Honeyframe";
       public const string DefaultLogDir = "DefaultLogDir"; 
       
       //Captions for messageboxes related
       public const string strBMCConfig = "BMC Enterprise Configuration";
       public const string strEnterpriseDBName = "Enterprise";
       public const string strMeterAnalysisDBName = "MeterAnalysis";
       public const string SQLCommandTimeOut = "SQLCommandTimeout";

    }
}