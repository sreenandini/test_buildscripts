using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Business.CashDeskOperator;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Common.LogManagement;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class CMachineMaintenance
    {
        #region Public Varibale

        CMachineMaintenanceDataAccess objMachineMaintenanceDataAccess;
        MachineManagerInterface objMachineManagerInterface;

        #endregion

        #region Constructor

        public CMachineMaintenance() {

            objMachineMaintenanceDataAccess = new CMachineMaintenanceDataAccess(CommonUtilities.ExchangeConnectionString);
            objMachineManagerInterface = new MachineManagerInterface();
        }


        #endregion

        #region Public Functions

        public List<CMaintenanceSession> GetMaintainSessionForInstallation(int InstallationNo)
        {
            return objMachineMaintenanceDataAccess.GetMaintainSessionForInstallation(InstallationNo).ToList();
        }

        public List<GetEventsForMaintainSessionResult> GetEventsForMaintainSession(int SessionID, int InstallationNo)
        {
            return objMachineMaintenanceDataAccess.GetEventsForMaintainSession(SessionID,InstallationNo).ToList();
        }

        public int ManageMaintenance(int InstallationNo, int EventID, int UserID)
        {
            return objMachineMaintenanceDataAccess.ManageMaintenance(InstallationNo, EventID, UserID);
        }

        public int CheckMachineMaintenance(int InstallationNo)
        {
            int? Return = -1;
            return objMachineMaintenanceDataAccess.CheckMachineMaintenance(InstallationNo, ref Return);
            
        }

        public void CloseMaintenance(int InstallationNo, int UserID, CMaintenanceReasonCategory[] objMaintenanceReasonCategory)
        {
            int? SessionID = 0;
            int? Site_Id = 0;
            int? Return = 0;
            objMachineMaintenanceDataAccess.CloseMaintenance(InstallationNo, UserID, ref SessionID, ref Site_Id);
            foreach (var obj in objMaintenanceReasonCategory)
            {
                objMachineMaintenanceDataAccess.InsertMaintenanceReasonCategory(SessionID, obj.CategoryID, obj.ReasonID, obj.Comments, Site_Id,ref Return);
            }
        }
        public bool IsOpenMaintenanceSession(int InstallationNo)
        {
            List<CMaintenanceSession> objMaintenanceSession = objMachineMaintenanceDataAccess.GetMaintainSessionForInstallation(InstallationNo).ToList();
            foreach (var obj in objMaintenanceSession)
            {
                if (obj.IsSessionOpen.Value)
                {
                    return true;
                }
            }
            return false;
        }

        public List<CSlotPortStatus> GetSlotPortStatusForInstallation(int InstallationNo)
        {
            return objMachineMaintenanceDataAccess.GetSlotPortStatusForInstallation(InstallationNo).ToList();
        }

        public bool ManageSlotPorts(int datapak, string message)
        {
            objMachineManagerInterface.ManageSlotPorts(datapak, message);
            return true;
        }

        public int UpdateSlotPortStatus(int datapak, bool auxSerialPort, bool gatSerialPort, bool slotLinePort)
        {
            return objMachineMaintenanceDataAccess.UpdateSlotPortStatus(datapak, auxSerialPort, gatSerialPort, slotLinePort);
        }

        #endregion
        
    }
}
