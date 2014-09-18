using System;
using System.Data;
using BMC.Business.CashDeskOperator.WebServices;
using BMC.DBInterface.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Transport;
using BMC.Common.LogManagement;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Transactions;

namespace BMC.Business.CashDeskOperator
{
    public class Enrollment : MachineManagerLazyInitializer
    {
        EnrollmentDataAccess enrollmentDataAccess = new EnrollmentDataAccess();
        //MachineManagerInterface machineManagerInterface = new MachineManagerInterface();
        /// <summary>
        /// Get Asset Details from enterprise
        /// </summary>
        /// <param name="AssetNo"></param>
        /// <returns></returns>
        public DataTable GetAssetDetails(string AssetNo, string TransitSiteCode)
        {
            DataTable AssetDetails = new DataTable();
            try
            {
                Proxy WebService = new Proxy(Settings.SiteCode,ConnectionStringHelper.ExchangeConnectionString);
                AssetDetails = WebService.GetAssetDetails(AssetNo, TransitSiteCode);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return AssetDetails;
            }

            return AssetDetails;
        }
        public PositionDetails GetPositionDetails(string Position)
        {
            PositionDetails positionDetails = new PositionDetails();
            DataTable dtPosition = enrollmentDataAccess.GetPositionDetails(Position);
            if (dtPosition != null)
            {
                positionDetails.Position = Position;
                positionDetails.InstallationNo = (Int32)dtPosition.Rows[0]["InstallationNo"];
                positionDetails.BarPosNo = dtPosition.Rows[0]["BarPosNo"].ToString();
                positionDetails.AssetNo = dtPosition.Rows[0]["Stock_No"].ToString();
                positionDetails.GMUNo = dtPosition.Rows[0]["DatapakNo"].ToString();
                positionDetails.SerialNo = dtPosition.Rows[0]["SerialNo"].ToString();
                positionDetails.AltSerialNo = dtPosition.Rows[0]["AltSerialNo"].ToString();
                positionDetails.MachineType = dtPosition.Rows[0]["MachineTypeCode"].ToString();
                positionDetails.Manufacturer = dtPosition.Rows[0]["Manufacturer_Name"].ToString();
                positionDetails.GameCode = dtPosition.Rows[0]["GameCode"].ToString();
                positionDetails.GameCategory = dtPosition.Rows[0]["GameCategory"].ToString();
                positionDetails.ActAssetNo = dtPosition.Rows[0]["ActAssetNo"].ToString();
                positionDetails.GMUNo = dtPosition.Rows[0]["GMUNo"].ToString();
                positionDetails.ActSerialNo = dtPosition.Rows[0]["ActSerialNo"].ToString();
                positionDetails.EnrolmentFlag = int.Parse(dtPosition.Rows[0]["EnrolmentFlag"].ToString());
                positionDetails.OperatorName = dtPosition.Rows[0]["OperatorName"].ToString();
                positionDetails.IsDefaultAssetDetail = Convert.ToBoolean(dtPosition.Rows[0]["IsDefaultAssetDetail"]);
                positionDetails.BaseDenom = int.Parse(dtPosition.Rows[0]["Base_Denom"].ToString());
                positionDetails.PercentagePayout = Convert.ToSingle(dtPosition.Rows[0]["Percentage_Payout"].ToString());
            }
            return positionDetails;
        }

        # region Install machine
        public EnrollmentErrorCodes InstallMachine(PositionDetails PosDetails, out int installationNo)
        {
            int HQInstallationNo;
            installationNo = 0;

            try
            {
                //2.START transaction & Call the SP & //3.Get the new Installation_No from SP output variable
                PosDetails.InstallationNo = enrollmentDataAccess.CreateInstallation(PosDetails);
                installationNo = PosDetails.InstallationNo;
                if (PosDetails.InstallationNo < 1)
                {
                    //1
                    enrollmentDataAccess.RollbackTransaction(0);
                    return EnrollmentErrorCodes.DatabaseError;
                }
                LogManager.WriteLog("InstallMachine:Created Installation in database with Installation no: " + PosDetails.InstallationNo.ToString(), LogManager.enumLogLevel.Info);


                //4.Add to Polling List
                //if (!AddToPollingList(PosDetails))
                //{
                //    //This function callls rollback multiple times 
                //    //2
                //    enrollmentDataAccess.RollbackTransaction(PosDetails.InstallationNo);
                //    return EnrollmentErrorCodes.AddToPollingListFailure;
                //}
                int nPollingList =AddToPollingList(PosDetails);

                if (nPollingList == -1)
                {
                    enrollmentDataAccess.RollbackTransaction(PosDetails.InstallationNo);
                    return EnrollmentErrorCodes.AddToPollingListFailure;
                }
                if (nPollingList == -2)
                {
                    enrollmentDataAccess.RollbackTransaction(PosDetails.InstallationNo);
                    return EnrollmentErrorCodes.ExchangeHostServiceNotRunning;
                }


                LogManager.WriteLog("InstallMachine:Added to Polling list ...datapak no: " + PosDetails.InstallationNo.ToString(), LogManager.enumLogLevel.Info);

                //MachineManagerInterface machineManagerInterface = new MachineManagerInterface();
                //if (!machineManagerInterface.UpdateOptionFileParameter(PosDetails.InstallationNo))
                //{
                //    enrollmentDataAccess.RollbackTransaction(PosDetails.InstallationNo);
                //    return EnrollmentErrorCodes.UpdateToOptionFileParameterFailure;
                //}
                //LogManager.WriteLog("InstallMachine:Updated Option File Parameter ...datapak no: " + PosDetails.InstallationNo.ToString(), LogManager.enumLogLevel.Info);

                //5.If Success send the details to Enterprise else rollback
                EnrollmentErrorCodes WebServiceReturnValue = SendInstallationToEnterprise(PosDetails, out HQInstallationNo);
                if (WebServiceReturnValue != EnrollmentErrorCodes.Success || HQInstallationNo <= 0)
                {
                    LogManager.WriteLog("InstallMachine:Unable to Create Installation in Enterprise: " + PosDetails.InstallationNo.ToString(), LogManager.enumLogLevel.Info);
                    //3
                    RemoveMachineFromPollingList(PosDetails.InstallationNo, 0);
                    enrollmentDataAccess.RollbackTransaction(PosDetails.InstallationNo);
                    return WebServiceReturnValue;

                }
                LogManager.WriteLog("InstallMachine: Installation Created in Enterprise successfully with HQ ID: " + HQInstallationNo.ToString(), LogManager.enumLogLevel.Info);

                LogManager.WriteLog("InstallMachine: Before commiting the transaction", LogManager.enumLogLevel.Info);
                //6.If Success from enterprise then COMMIT else Rollback.. If any other errors display
                enrollmentDataAccess.CommitTransaction();
                LogManager.WriteLog("InstallMachine: Transaction Committed", LogManager.enumLogLevel.Info);

                // Updates the UpdateGMUSiteCodeStatus
                LogManager.WriteLog("InstallMachine: UpdateGMUSiteCodeStatus with Status 1 (START)", LogManager.enumLogLevel.Info);
                enrollmentDataAccess.UpdateGMUSiteCodeStatus(PosDetails.InstallationNo, 1);
                LogManager.WriteLog("InstallMachine: UpdateGMUSiteCodeStatus with Status 1 (END)", LogManager.enumLogLevel.Info);

                //7. update the HQ Id in Installation table
                enrollmentDataAccess.UpdateHQID(PosDetails.InstallationNo, HQInstallationNo);
                LogManager.WriteLog("InstallMachine: Updated HQID", LogManager.enumLogLevel.Info);

                return EnrollmentErrorCodes.Success;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return EnrollmentErrorCodes.DatabaseError;
            }


        }

        /// <summary>
        /// Send New Installation to Enterprise
        /// </summary>
        /// <param name="PosDetails"></param>
        /// <param name="HQInstallatioNo"></param>
        /// <returns></returns>
        private EnrollmentErrorCodes SendInstallationToEnterprise(PositionDetails PosDetails, out int HQInstallatioNo)
        {
            try
            {
                HQInstallatioNo = 0;
                //Get Old installation XML to close in enetrprise.
                string InstallationXML = enrollmentDataAccess.GetInstallationXML(PosDetails.InstallationNo);
                if (InstallationXML.Length == 0)
                {
                    LogManager.WriteLog("SendInstallationToEnterprise Installation XML From SP is empty. May be some problem in generating the XML from DB", LogManager.enumLogLevel.Info);
                    return EnrollmentErrorCodes.DatabaseError;
                }
                Proxy WebService = new Proxy(Settings.SiteCode,ConnectionStringHelper.ExchangeConnectionString);
                
                LogManager.WriteLog("InstallMachine:SendInstallationToEnterprise Calling Webmethod", LogManager.enumLogLevel.Info);
                int ReturnValue = WebService.CreateInstallation(InstallationXML);
                switch (ReturnValue)
                {

                    case -4:
                        return EnrollmentErrorCodes.EnterpriseAssetInUse;
                       

                    case -3:
                        return EnrollmentErrorCodes.EnterpriseAssetNotAvailable;
                        
                    case -90:          //general sql error
                        return EnrollmentErrorCodes.EnterpriseDatabaseError;
                        

                    case -99:          //general sql error
                        return EnrollmentErrorCodes.EnterpriseDatabaseError;
                        

                    default:
                        //STORE HQ ID IN Installation table
                        HQInstallatioNo = ReturnValue;
                        if(ReturnValue > 0)
                            return EnrollmentErrorCodes.Success;
                        else
                            return EnrollmentErrorCodes.EnterpriseDatabaseError;
                       

                }
            }
            catch (Exception ex)
            {
                HQInstallatioNo = 0;
                ExceptionManager.Publish(ex);
                return EnrollmentErrorCodes.EnterpriseWebServiceCommunicationFailure;
            }
        }

        /// <summary>
        /// Adds the Installation to Polling List
        /// </summary>
        /// <param name="PosDetails"></param>
        /// <returns>Installation Number</returns>
        private int AddToPollingList(PositionDetails PosDetails)
        {
            try
            {
                if (PosDetails.InstallationNo > 0)//For new installation
                {
                    int Bar_Pos_Port = enrollmentDataAccess.GetBarPosPort(PosDetails.InstallationNo);
                    int nResult = this.GetMachineManager().AddUDPToListWithoutWait(PosDetails.InstallationNo, Bar_Pos_Port);
                    LogManager.WriteLog("Return Value from AddUDPToList:" + nResult.ToString(), LogManager.enumLogLevel.Info);
                    return nResult;
                    #region commented
                    //int Polling_ID, PollType;
                    //Polling_ID = 0;
                    //PollType = 7;



                    //Parameters parameter = new Parameters() { InstallationNo = PosDetails.InstallationNo, BarPositionNo = Bar_Pos_Port };

                    //Thread t = new Thread(CallAddToUPD);
                    //t.Start(parameter);

                    //Application.DoEvents();
                    //Thread.Sleep(TimeSpan.FromSeconds(15));
                    //Application.DoEvents();

                    //bSuccess = MachineManagerInterface.AckMessage;

                    //LogManager.WriteLog("AddToPollingList: ADD UDP TO LIST for Installation No:" + PosDetails.InstallationNo.ToString() + " Success value: " + bSuccess, LogManager.enumLogLevel.Info);
                    //return bSuccess; 
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception thrown in AddUDPToList", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return -1;
            }
            finally
            {
                this.ReleaseMachineManager();
            }
          return -1;
        }

        public class Parameters
        {
            public int InstallationNo;
            public int BarPositionNo;
        }


        public void CallAddToUPD(object obj)
        {
            try
            {
               // machineManagerInterface.AddUDPToListWithoutWait(((Parameters)obj).InstallationNo, ((Parameters)obj).BarPositionNo);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        public void CallRemoveUPD(object obj)
        {
            try
            {
               // machineManagerInterface.RemoveUDPFromListWithoutWait(((Parameters)obj).InstallationNo);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }



        #endregion

        # region Remove Machine Operations

        public EnrollmentErrorCodes RemoveMachine(int InstallationNo, int MachineStatusFlag, string SiteCode ,int nDisMachine)
        {
            #region Removal functionality (InTransaction)
            using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                bool rollback = true;
                Exception exThrowed = null;
                LogManager.WriteLog("nDisMachine: " + nDisMachine.ToString(), LogManager.enumLogLevel.Info);

                try
                {
                    // STEP 1 : Remove UDP from poling list     
                    int nResult = RemoveMachineFromPollingList(InstallationNo, nDisMachine);
                    if (nResult == -1)
                    {
                        return EnrollmentErrorCodes.RemoveFromPollingListFailure;
                    }
                    else if (nResult == -2)
                    {
                        return EnrollmentErrorCodes.ExchangeHostServiceNotRunning;
                    }
                    else
                    {
                        LogManager.WriteLog("Remove UDP Successful", LogManager.enumLogLevel.Info);
                    }

                    // STEP 2 : Close the installation & 3. Clear Floor-Financials records & 4. Close the machine   

                    if (!CloseInstallationInDB(InstallationNo, MachineStatusFlag, SiteCode))
                    {
                        return EnrollmentErrorCodes.DatabaseError;
                    }
                    else
                    {
                        LogManager.WriteLog("Close Installation DB Succcesful", LogManager.enumLogLevel.Info);
                    }
                    
                    // Success
                    rollback = false;
                }
                catch (Exception ex)
                {
                    exThrowed = ex;
                    ExceptionManager.Publish(ex);
                    return EnrollmentErrorCodes.RemoveFromPollingListFailure;
                }
                finally
                {
                    if (rollback)
                    {
                        if (exThrowed != null)
                        {
                            Transaction.Current.Rollback(exThrowed);
                        }
                        else
                        {
                            Transaction.Current.Rollback();
                        }
                    }
                    else
                    {
                        transScope.Complete();
                    }
                }
            }
            #endregion

            #region Enterprise export and machine status updation (OutOfTransaction)
            try
            {
                // STEP 3 : Send the details to Enterprise
                if (!SendRemoveMachineDetailsToEnterprise(InstallationNo, Security.SecurityHelper.CurrentUser.UserName.ToString()))
                {
                    return EnrollmentErrorCodes.EnterpriseWebServiceCommunicationFailure;
                }

                //STEP 4 : Update Machine Status For Closed Installation
                if (!UpdateMachineStatusForClosedInstallation(InstallationNo))
                {
                    return EnrollmentErrorCodes.DatabaseError;
                }
                else
                {
                    LogManager.WriteLog("Update Machine Status For Closed Installation Succcesful", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return EnrollmentErrorCodes.RemoveFromPollingListFailure;
            }

            #endregion

            return EnrollmentErrorCodes.Success;
        }
        private int  RemoveMachineFromPollingList(int InstallationNo,int nDisMachine)
        {
            try
            {
                return this.GetMachineManager().RemoveUDPFromListWithoutWait(InstallationNo, nDisMachine);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception thrown in RemoveMachineFromPollingList", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return -1;
            }
            finally
            {
                this.ReleaseMachineManager();
            }

            #region MyRegion
            //Parameters parameter = new Parameters() { InstallationNo = InstallationNo };

            //Thread t = new Thread(CallRemoveUPD);
            //t.Start(parameter);

            //Application.DoEvents();
            //Thread.Sleep(TimeSpan.FromSeconds(15));
            //Application.DoEvents();

            //return MachineManagerInterface.AckMessage;

            ////MachineManager objMachineManager = null;
            //MachineManagerInterface machineManagerInterface = new MachineManagerInterface();
            //try
            //{

            //    return machineManagerInterface.RemoveUDPFromList(InstallationNo);
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.Publish(ex);
            //    return false;
            //}
            //finally
            //{
            //    machineManagerInterface.Dispose();
            //} 
            #endregion
        }
        private bool CloseInstallationInDB(int InstallationNo, int MachineStatusFlag, string MachineTransitSiteCode)
        {
            try
            {
                return (enrollmentDataAccess.CloseInstallation(InstallationNo, MachineStatusFlag, MachineTransitSiteCode));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }
        private bool UpdateMachineStatusForClosedInstallation(int InstallationNo)
        {
            try
            {
                return (enrollmentDataAccess.UpdateMachineStatusForClosedInstallation(InstallationNo));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }
        private bool SendRemoveMachineDetailsToEnterprise(int InstallationNo, string UserName)
        {
            try
            {
                //Get Old installation XML to close in enetrprise.
                string InstallationXML = GetClosedInstallationXML(InstallationNo);
                Proxy WebService = new Proxy(Settings.SiteCode,ConnectionStringHelper.ExchangeConnectionString);
                if (WebService.CloseInstallation(InstallationXML) == 100) //success.
                {
                    return true;
                }
                else //FAILURE CALLING WEB SERVICE.
                {
                    //export via fifo (export_history)
                    enrollmentDataAccess.ExportRemoveInstallation(InstallationNo);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                try
                {
                    //export via fifo (export_history) TRY TO FOLLOW FIFO IF ANY EXCEPTION OCCURS
                    enrollmentDataAccess.ExportRemoveInstallation(InstallationNo);
                    return true;

                }
                catch (Exception ex1)
                {
                    ExceptionManager.Publish(ex1);
                    return false;
                }

            }
        }
        private string GetClosedInstallationXML(int InstallationNo)
        {
            return enrollmentDataAccess.GetClosedInstallationXML(InstallationNo);
        }

        public bool SetMachineMaintenanceState(int installationNo)
        {
            return enrollmentDataAccess.SetMachineMaintenanceState(installationNo);
        }

        public bool SetMachinePreviousState(int installationNo)
        {
            return enrollmentDataAccess.SetMachinePreviousState(installationNo);
        }

        public int ExecuteHourlyVTP(int InstallationNumber, DateTime dtTheDate, int iTheHour, bool isRead)
        {
            return enrollmentDataAccess.ExecuteHourlyVTP(InstallationNumber, dtTheDate, iTheHour, isRead);
        }

        public bool UpdateHourlyStatsGamingday(string Installations)
        {
            return enrollmentDataAccess.UpdateHourlyStatsGamingday(Installations);
        }

        public DataTable GetActiveSiteDetails()
        {  
            Proxy _WebProxy = new Proxy(Settings.SiteCode);
            return _WebProxy.GetActiveSiteDetails();                     
        }

        public DataTable GetInTransitAsset()
        {
            Proxy _WebProxy = new Proxy(Settings.SiteCode);
            return _WebProxy.GetInTransitAssetforSite(Settings.SiteCode);
        }

        #endregion

        public IEnumerable<PositionCurrentStatusResult> GetPositionCurrentStatus(bool allPosition, bool vLTAAMS, bool vLTVerification, bool gameAAMS, bool gameVerification, bool gameEnableAAMS, bool BADAAMSEnableDisable, bool BMCEnterpriseStatus)
        {
            LinqDataAccessDataContext linq = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);

            try
            {
                return linq.GetPositionCurrentStatus(allPosition, vLTAAMS, vLTVerification, gameAAMS, gameVerification, gameEnableAAMS, BADAAMSEnableDisable, BMCEnterpriseStatus);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return null;
            }
        }

        public int GetInstallationNoForRemoval(string strSerialNo)
        {
            return enrollmentDataAccess.GetInstallationNo(strSerialNo);
        }

        public void InsertIntoExportHistory(int InstallationNumber)
        {
            enrollmentDataAccess.InsertIntoExportHistory(InstallationNumber);
        }

    }

}