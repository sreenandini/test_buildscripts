using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_Status_EmployeeCardIn")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_Status_EmployeeCardIn
        : MonTgt_G2H_Status_CardBase
    {
        public MonTgt_G2H_Status_EmployeeCardIn()
        {
        }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_Status_EmployeeCardOut")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_Status_EmployeeCardOut
        : MonTgt_G2H_Status_CardBase
    {
        public MonTgt_G2H_Status_EmployeeCardOut()
        {
        }
    }
}
