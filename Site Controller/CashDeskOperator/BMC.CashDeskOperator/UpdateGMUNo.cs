using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.ExceptionManagement;
using BMC.Transport;
using BMC.Business.CashDeskOperator;

namespace BMC.CashDeskOperator
{
    public class UpdateGMUNo
    {
        private static UpdateGMUNo _UpdateGMUNo = null;

        public static UpdateGMUNo CreateInstance()
        {
            if (_UpdateGMUNo == null)
            {
                _UpdateGMUNo = new UpdateGMUNo();
            }
            return _UpdateGMUNo;
        }

        public List<AGSdetails> GetAGSDetails()
        {
            return UpdateGMUNoBiz.CreateInstance().GetAGSDetails(); 
           
        }

        public bool CheckAGSCombination(string ActAssetNo, string NewGMUNo, string ActSerialNo)
        {
            return UpdateGMUNoBiz.CreateInstance().CheckAGSCombination(ActAssetNo, NewGMUNo, ActSerialNo);
        }

        public string ExportAGSDetails(int MachineID, string New_GMUNo)
        {
            return UpdateGMUNoBiz.CreateInstance().ExportAGSDetails(MachineID, New_GMUNo);
        }

        public bool UpdateGMUDetails(int MachineID, string New_GMUNo, int Installation_No)
        {
            return UpdateGMUNoBiz.CreateInstance().UpdateGMUDetails(MachineID, New_GMUNo, Installation_No);
        }

        public bool UpdateGMUDownEvent(int Installation_No)
        {
            return UpdateGMUNoBiz.CreateInstance().UpdateGMUDownEvent(Installation_No);
        }

        public bool RemoveMachineFromPollingList(int InstallationNo,int DisableMachine)
        {
            return UpdateGMUNoBiz.CreateInstance().RemoveMachineFromPollingList(InstallationNo, DisableMachine);
        }

        public bool AddToPollingList(int InstallationNo, int barPositionPortNo)
        {
            return UpdateGMUNoBiz.CreateInstance().AddToPollingList(InstallationNo, barPositionPortNo);
        }

    }
}
