using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_B2B_GameCapping
        : FFTgt_B2B
    {
        public FFTgt_B2B_GameCappingData CappingData
        {
            get { return this.GetPrimaryTarget<FFTgt_B2B_GameCappingData>(); }
            set { this.SetPrimaryTarget<FFTgt_B2B_GameCappingData>(value); }
        }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.GameCapping;
            }
        }
    }

    /// <summary>
    /// GMU to Host GMU Event Data Information
    /// </summary>
    public class FFTgt_B2B_GameCappingData : FFTgt_B2B { }

    public class FFTgt_H2G_GameCapping
       : FFTgt_B2B_GameCappingData
    {
        public bool IsEnabled { get; set; }
        public bool IsMaxCappingExceeded { get; set; }
        public bool IsSelfCapping { get; set; }
        public bool IsAllowed { get; set; }

        public byte Option1 { get; set; }
        public byte Option2 { get; set; }
        public byte Option3 { get; set; }
        public byte Option4 { get; set; }
        public byte Option5 { get; set; }

        public bool AutoRelease { get; set; }
        public bool IsPinRequired { get; set; }
        public byte MinutesToExpire { get; set; }
    }

    public abstract class FFTgt_G2H_GameCapping_Info
       : FFTgt_G2H
    {
        public string PlayerCardNumber { get; set; }
    }

    public class FFTgt_G2H_GameCapping_StartEnd
       : FFTgt_G2H_GameCapping_Info
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GameCapping_G2H_RequestTypes.StartEnd;
            }
        }

        public bool IsCapStart { get; set; }
        public string EmployeeCardNumber { get; set; }
        public string PlayerPIN { get; set; }
    }

    public class FFTgt_G2H_GameCapping_TimeLeft
       : FFTgt_G2H_GameCapping_Info
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GameCapping_G2H_RequestTypes.TimerExpire;
            }
        }

        public byte NumberOfMinutesToExpire { get; set; }
    }
}
