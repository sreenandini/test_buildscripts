using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// GMU to Host Freeform for Game Information
    /// </summary>
    [CommsEntity(Response = typeof(FFTgt_H2G_GIM_GameIDInfo))]
    public class FFTgt_G2H_GIM_GameIDInfo
        : FFTgt_B2B_GIM_Data, IFreeformEntity_MsgTgt_Primary
    {
        public override int EntityId
        {
            get { return (int) FF_AppId_GIM_SubTargets.GameIDInfo; }
        }

        public string GMUNumber { get; set; }
        public string AssetNumber { get; set; }
        public string ManufacturerID { get; set; }
        public string SerialNumber { get; set; }
        public string MACAddress { get; set; }
        public string SASVersion { get; set; }
        public string GMUVersion { get; set; }
    }

    public class FFTgt_H2G_GIM_GameIDInfo
        : FFTgt_B2B_GIM_Data, IFreeformEntity_MsgTgt_Primary
    {
        public FFTgt_H2G_GIM_GameIDInfo()
        {
            this.DisplayMessage = "Success!";
        }

        public override int EntityId
        {
            get { return (int) FF_AppId_GIM_SubTargets.GameIDInfo; }
        }

        public override int EntityUniqueKeyInt
        {
            get
            {
                return (int)FreeformUniqueKeys.GIM_GameIDIInfo_Response;
            }
        }

        public string DisplayMessage { get; set; }

        public bool EnableNetworkMessaging { get; set; }

        public IPAddress SourceAddress { get; set; }        

        public int AssetNumberInt { get; set; }

        public string PokerGamePrefix { get; set; }
    }
}
