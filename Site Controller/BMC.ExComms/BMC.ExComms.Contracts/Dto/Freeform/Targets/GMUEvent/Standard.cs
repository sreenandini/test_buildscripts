using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_G2H_GMUEvent_StdData
        : FFTgt_B2B_GMUEventData_Primary
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUEvent_DataSetIds.Standard;
            }
        }
        
        public FF_AppId_GMUEvent_XCodes ExceptionCode { get; set; }
        public FF_AppId_GMUEvent_JackpotIDs JackpotID { get; set; }
        public string EmployeeCardID { get; set; }
        public short LastBet { get; set; }
        public byte DoorStatus { get; set; }
        public byte OptionByte { get; set; }
        public double JackpotAmount { get; set; }
        public double JackpotAmount2 { get; set; }
        public string PlayerCard { get; set; }
        public short BonusPoints { get; set; }
        public byte LastBill { get; set; }
        public string SMICode { get; set; }
        public int GameDenomination { get; set; }
        public string CasinoID { get; set; }
        public short BonusCountdown { get; set; }
        public short HopperCount { get; set; }

        public override string EntityKey
        {
            get { return ((int)this.ExceptionCode).ToString(); }
        }

        public override string TypeDescriptionKey
        {
            get
            {
                return base.TypeDescriptionKey + " (" + this.ExceptionCode.ToString() + ")";
            }
        }
    }
}
