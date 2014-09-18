using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Business.CashDeskOperator;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Transport;
using System.Collections.Generic;

using System.Net;
namespace BMC.CashDeskOperator.BusinessObjects
{
    public class EnrollmentBusinessObject: IEnrollment
    {
        #region Private Variables
        
        Enrollment enrollment = new Enrollment();
        
        #endregion

        #region Constructor
        
        private EnrollmentBusinessObject() { }
        
        #endregion

        #region Public Functions

        public DataTable GetAssetDetails(string AssetNo, string TransitSiteCode)
        {
            return enrollment.GetAssetDetails(AssetNo, TransitSiteCode);
        }

        public int GetInstallationNo(string SerialNo)
        {
            return enrollment.GetInstallationNoForRemoval(SerialNo);
        }
        public PositionDetails GetPositionDetails(string Position)
        {
            return enrollment.GetPositionDetails(Position);
        }

        public EnrollmentErrorCodes RemoveMachine(int InstallationNo, int MachineStatusFlag, string SiteCode,int nDisMachine)
        {
            return enrollment.RemoveMachine(InstallationNo, MachineStatusFlag, SiteCode,nDisMachine);
        }

        public EnrollmentErrorCodes InstallMachine(PositionDetails PosDetails, int userid, out int installationNo)
        {
            LockHandler Lock = new LockHandler();
            EnrollmentErrorCodes ReturnValue;
            installationNo = 0;

            int SPResult = Lock.InsertLockRecord(userid, Dns.GetHostName(), "MACHINEADMIN", "POS", PosDetails.Position);
            switch (SPResult)
            {
                case 0: ReturnValue = enrollment.InstallMachine(PosDetails, out installationNo);

                    break;
                case 1:
                    ReturnValue = EnrollmentErrorCodes.LockExists;
                    break;
                case 2:
                    ReturnValue = EnrollmentErrorCodes.LockError;
                    break;
                default:
                    ReturnValue = EnrollmentErrorCodes.DatabaseError;
                    break;

            }
            Lock.DeleteLockRecord(userid, Dns.GetHostName(), "MACHINEADMIN", "POS", PosDetails.Position);
            return ReturnValue;

            
        }

        public bool SetMachineMaintenanceState(int installationNo)
        {
            return enrollment.SetMachineMaintenanceState(installationNo);
        }

        public bool SetMachinePreviousState(int installationNo)
        {
            return enrollment.SetMachinePreviousState(installationNo);
        }

        public IEnumerable<PositionCurrentStatusResult> GetPositionCurrentStatus(bool allPosition, bool vLTAAMS, bool vLTVerification, bool gameAAMS, bool gameVerification, bool gameEnableAAMS, bool BADAAMSEnableDisable, bool BMCEnterpriseStatus)
        {
            return enrollment.GetPositionCurrentStatus(allPosition, vLTAAMS, vLTVerification, gameAAMS, gameVerification, gameEnableAAMS, BADAAMSEnableDisable, BMCEnterpriseStatus);
        }

        public int ExecuteHourlyVTP(int InstallationNumber, DateTime dtTheDate, int iTheHour, bool isRead)
        {
            return enrollment.ExecuteHourlyVTP(InstallationNumber, dtTheDate, iTheHour, isRead);
        }

        public bool UpdateHourlyStatsGamingday(string Installations)
        {
            return enrollment.UpdateHourlyStatsGamingday(Installations);
        }

        public DataTable GetActiveSiteDetails()
        {
            return enrollment.GetActiveSiteDetails();
        }

        public DataTable GetInTransitAsset()
        {
            return enrollment.GetInTransitAsset();
        }

        public void InsertIntoExportHistory(int InstallationNumber)
        {
            enrollment.InsertIntoExportHistory(InstallationNumber);
        }

        #endregion

        #region public Static Function

        public static IEnrollment CreateInstance()
        {
            return new EnrollmentBusinessObject();
        }
        #endregion



    }
}
