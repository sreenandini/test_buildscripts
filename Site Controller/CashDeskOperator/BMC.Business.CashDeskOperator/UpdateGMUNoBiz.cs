using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Transport;
using BMC.DBInterface.CashDeskOperator;
using System.Data.Linq;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;

namespace BMC.Business.CashDeskOperator
{
    public class UpdateGMUNoBiz : MachineManagerLazyInitializer
    {
        private UpdateGMUNoDataAccess _UpdateGMUNo = new UpdateGMUNoDataAccess(CommonDataAccess.ExchangeConnectionString);
        private static UpdateGMUNoBiz _UpdateGMUNoBiz = null;

        public static UpdateGMUNoBiz CreateInstance()
        {
            if (_UpdateGMUNoBiz == null)
            {
                _UpdateGMUNoBiz = new UpdateGMUNoBiz();
            }
            return _UpdateGMUNoBiz;
        }


        public List<AGSdetails> GetAGSDetails()
        {
            List<AGSdetails> lstAGSDetails = null;
            ISingleResult<GetAGSdetailsResult> result = null;
            try
            {
                result = _UpdateGMUNo.GetAGSdetails();

                lstAGSDetails = (from obj in result
                                 select new AGSdetails()
                                 {
                                     AssetNo = obj.AssetNo,
                                     BarPositionNo = obj.BarPositionNo,
                                     ActualAssetNo = obj.ActualAssetNo,
                                     GMUNo = obj.GMUNo,
                                     Installation_No = obj.Installation_No,
                                     SerialNo = obj.SerialNo,
                                     MachineID= obj.MachineID.Value,
                                     EditSave = "Edit",
                                     IsEnabled = false,
                                     IsFilterEnabled=false
                                 }).ToList();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return lstAGSDetails;
        }

        public string ExportAGSDetails(int MachineID, string New_GMUNo)
        {
            string strXml = "";
            try
            {
                List<rsp_ExportAGSDetailsResult> lst_exp = _UpdateGMUNo.ExportAGSDetails(MachineID, New_GMUNo).ToList<rsp_ExportAGSDetailsResult>();
                if (lst_exp != null && lst_exp.Count > 0)
                {
                    strXml = lst_exp[0].XmlString;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return strXml;
        }

        public bool UpdateGMUDetails(int MachineID, string New_GMUNo, int Installation_No)
        {
            bool retVal = false;
            try
            {
                retVal = (_UpdateGMUNo.UpdateGMUDetails(MachineID, New_GMUNo, Installation_No) >= 0);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return retVal;
        }


        public bool UpdateGMUDownEvent(int Installation_No)
        {
            bool retVal = false;
            try
            {
                retVal = (_UpdateGMUNo.UpdateGMUDownEvent(Installation_No) >= 0);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return retVal;
        }

        public bool CheckAGSCombination(string ActAssetNo, string NewGMUNo, string ActSerialNo)
        {
            bool retVal = false;
            try
            {
                retVal = (_UpdateGMUNo.FN_CheckAGSCombination(ActAssetNo, NewGMUNo, ActSerialNo) ?? false);

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return retVal;

        }

        public bool RemoveMachineFromPollingList(int InstallationNo,int DisableMachine)
        {
            bool retVal = false;
            try
            {
                int Result = this.GetMachineManager().RemoveUDPFromListWithoutWait(InstallationNo, DisableMachine);
                int[] errorcode = new int[2] { -1, -2 };
                if (Array.IndexOf(errorcode, Result) == -1)
                {
                    retVal = true;
                }
                LogManager.WriteLog("Return Value from RemoveMachineFromPollingList:" + Result.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception thrown in RemoveMachineFromPollingList", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);

            }
            finally
            {
                this.ReleaseMachineManager();
            }
            return retVal;

        }


        public bool AddToPollingList(int InstallationNo, int barPositionPortNo)
        {
            bool retVal = false;
            try
            {
                int Result = this.GetMachineManager().AddUDPToListWithoutWait(InstallationNo, barPositionPortNo);
                int[] errorcode = new int[2] { -1, -2 };
                if (Array.IndexOf(errorcode, Result) == -1)
                {
                    retVal = true;
                }
                LogManager.WriteLog("Return Value from AddUDPToList:" + Result.ToString(), LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception thrown in AddUDPToList", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);

            }
            finally
            {
                this.ReleaseMachineManager();
            }
            return retVal;
        }
    }
}
