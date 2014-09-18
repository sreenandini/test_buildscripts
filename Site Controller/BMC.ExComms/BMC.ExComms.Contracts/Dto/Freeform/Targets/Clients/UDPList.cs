using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_H2G_Client_AddUDPToList
        : FFTgt_B2B_ClientData, IFFTgt_H2G
    {
        public string ServerIP { get; set; }

        public int Port { get; set; }

        public long PollingID { get; set; }

        public long Type { get; set; }

        public int InstallationNo { get; set; }
    }

    public class FFTgt_H2G_Client_AddUDPsToList
       : FFTgt_B2B_ClientData, IFFTgt_H2G
    {
        public List<int> InstallationNos { get; set; }
    }

    public class FFTgt_H2G_Client_RemoveUDPFromList
        : FFTgt_B2B_ClientData, IFFTgt_H2G
    {
        public int InstallationNo { get; set; }
    }
}
