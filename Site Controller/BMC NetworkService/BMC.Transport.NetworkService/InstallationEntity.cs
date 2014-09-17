using System;
using System.Collections.Generic;
using System.Text;

namespace BMC.Transport.NetworkService
{
    public class InstallationEntity
    {
        private int _iInstallationNo = 0;
        private int _BarPositionNo = 0;
        private int _DatapakNo = 0;
        private string _BarPositionName = string.Empty;
        private string _BarPositionMachineEnabledCental = string.Empty;
        private string _SiteCode = string.Empty;
        private string _BarPositionMachineEnabled = string.Empty;
        private bool _BarPositioninEnterpise = false;
        private bool _BarPostionMachineStatusinEnterprise = false;

        public int InstallationNumber
        {
            get
            {
                return _iInstallationNo;
            }
            set
            {
                _iInstallationNo = value;
            }
        }

        public int BarPositionNo
        {
            get
            {
                return _BarPositionNo;
            }
            set
            {
                _BarPositionNo = value;
            }
        }
        public int DataPakNumber
        {
            get
            {
                return _DatapakNo;
            }
            set
            {
                _DatapakNo = value;
            }
        }

        public string BarPositionName
        {
            get
            {
                return _BarPositionName;
            }
            set
            {
                _BarPositionName = value;
            }
        }

        public string BarPositionMachineEnabled
        {
            get
            {
                return _BarPositionMachineEnabled;
            }
            set
            {
                _BarPositionMachineEnabled = value;
            }
        }

        public string BarPositionMachineEnabledCentral
        {
            get
            {
                return _BarPositionMachineEnabledCental;
            }
            set
            {
                _BarPositionMachineEnabledCental = value;
            }
        }


        public string SiteCode
        {
            get
            {
                return _SiteCode;
            }
            set
            {
                _SiteCode = value;
            }
        }

        public bool BarPositionMachineEnabledinEnterprise
        {
            get
            {
                return _BarPositioninEnterpise;
            }
            set
            {
                _BarPositioninEnterpise = value;
            }
        }

        public bool BarPositionMachineStatusinEnterprise
        {
            get
            {
                return _BarPostionMachineStatusinEnterprise;
            }
            set
            {
                _BarPostionMachineStatusinEnterprise = value;
            }
        }
    }

    public class GameEntity
    {
        private int _InstallationNo = 0;
        private string  _GamePosition = string.Empty ;
        private int _GameVerificationResult = 0;

        public int InstallationNumber
        {
            get
            {
                return _InstallationNo;
            }
            set
            {
                _InstallationNo = value;
            }
        }

        public string GamePosition
        {
            get
            {
                return _GamePosition;
            }
            set
            {
                _GamePosition = value;
            }
        }

        public int GameVerificationResult
        {
            get
            {
                return _GameVerificationResult;
            }
            set
            {
                _GameVerificationResult = value;
            }
        }
    }

    public static class NetworkServiceSettings
    {
        public static int RequestWaitTime;
        public static int DBHitWaitTime;
    }
}
