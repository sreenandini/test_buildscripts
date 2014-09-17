using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// This message is sent from GMU to the host on Employee Card-In event (0x37).
    /// </summary>
    public class FFTgt_B2B_EmployeeCardInBase
        : FFTgt_B2B { }

    /// <summary>
    /// This message is sent from GMU to the host on Employee Card-In event (0x37).
    /// </summary>
    public class FFTgt_G2H_EmployeeCardIn_RequestBase
        : FFTgt_B2B_EmployeeCardInBase, IFFTgt_G2H { }

    /// <summary>
    /// This message is sent from GMU to the host on Employee Card-In event (0x37).
    /// </summary>
    public class FFTgt_H2G_EmployeeCardIn_ResponseBase
        : FFTgt_B2B_EmployeeCardInBase, IFFTgt_H2G
    {
        public FF_AppId_EmployeeCard_ResponseStatus Status { get; set; }
    }

    public class FFTgt_G2H_EmployeeCardIn_CardIn
        : FFTgt_G2H_EmployeeCardIn_RequestBase
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EmployeeCard_G2H_Actions.EmployeeCardIn;
            }
        }
    }

    public class FFTgt_H2G_EmployeeCardIn_EmpInfo
        : FFTgt_H2G_EmployeeCardIn_ResponseBase
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EmployeeCard_H2G_Actions.EmployeeInformation;
            }
        }

        public string EmpCardNumber { get; set; }
        public short ResponseData { get; set; }
    }

    public class FFTgt_H2G_EmployeeCardIn_MainMenusToDisable
        : FFTgt_H2G_EmployeeCardIn_ResponseBase
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EmployeeCard_H2G_Actions.EmployeeMainMenusToDisable;
            }
        }

        public byte[] MenuNumberArray { get; set; }
    }

    public class FFTgt_H2G_EmployeeCardIn_1stLevelSubMenustoDisable
        : FFTgt_H2G_EmployeeCardIn_ResponseBase
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EmployeeCard_H2G_Actions.Employee1stLevelSubMenustoDisable;
            }
        }

        public byte[] MenuSubMenuNumberArray { get; set; }
    }
}
