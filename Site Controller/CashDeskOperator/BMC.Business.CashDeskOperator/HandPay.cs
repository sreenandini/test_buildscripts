using System;
using System.Data;
using System.Collections.Generic;
using BMC.Common.ExceptionManagement;
using BMC.DBInterface.CashDeskOperator;
using BMC.Transport.CashDeskOperatorEntity;
using System.Data.Linq;
using System.Linq;
using BMC.Transport;

namespace BMC.Business.CashDeskOperator
{
    public partial class HandPay : MachineManagerLazyInitializer
    {
        HandpayDataAccess handpayDataAccess = new HandpayDataAccess();
        CommonDataAccess commonDataAccess = new CommonDataAccess();
        LinqDataAccessDataContext linqDB = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);

        /// <summary>
        /// Retrieves the handpay entries yet to be processed.
        /// </summary>
        /// <returns>success or failure</returns>
        public DataTable GetUnprocessedHandPays()
        {
            DataTable objHandPays = null;

            try
            {
                objHandPays = handpayDataAccess.GetUnProcessedHandPays();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objHandPays;
        }



        /// <summary>
        /// Retrieves the active bar positions
        /// </summary>
        /// <returns>success or failure</returns>
        public List<BarPositions> GetBarPositions()
        {
            List<BarPositions> lstPositions = null;

            try
            {
                lstPositions = handpayDataAccess.getBarPositions();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstPositions;
        }


        public DataTable GetUnprocessedHandPays(int InstallationNo)
        {
            DataTable objHandPays = new DataTable();

            try
            {
                objHandPays = handpayDataAccess.GetUnProcessedHandPays(InstallationNo);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objHandPays;
        }

        public IEnumerable<FillTreasuryList> GetHandpays(string BarPos)
        {
            return linqDB.FillTreasuryList(BarPos);
        }

        /// <summary>
        /// saves the manual handpay entries to DB
        /// </summary>
        /// <returns>success or failure</returns>        
        public int ProcessManualHandpay(Treasury Treasury)
        {
            return handpayDataAccess.SaveTreasuryDetails(Treasury);
        }
        //
        public int ProcessHandPayOnDenomChange(string sBarpos, int userID)
        {
            return linqDB.UpdateProcessHandpayOnDenomChange(sBarpos, userID);
        }
        //
        public int ProcessHandpay(Treasury Treasury, int TE_ID)
        {
            //return handpayDataAccess.SaveTreasuryDetails(Treasury);
            int? Treasury_No = 0;
            int result;
            int IsManualAttendantPay = TE_ID == 0 ? 1 : 0;
            int? IsExport = 0;
            try
            {

                if (Settings.IsGloryCDEnabled && Settings.CashDispenserEnabled)
                {
                    IsExport = 1;
                }
            }
            catch (Exception Ex)
            {
                Exception exc = new Exception("Config IsGloryCDEnabled not found");
                ExceptionManager.Publish(exc);
            }
            ///Param IsExport: if 1-default; 0-Can't Export to Enterprise*/
            try
            {
                result = linqDB.InsertTreasury(Treasury.InstallationNumber, 0, Treasury.UserID, Treasury.TreasuryType,
                    Treasury.TreasuryReason, Convert.ToDecimal(Treasury.TreasuryAmount), false, "0", 0, Treasury.UserID, Treasury.TreasuryTemp, 0,
                    Treasury.ActualTreasuryDate, Treasury.CustomerID, Treasury.AuthorizedUser_No, Treasury.Authorized_Date, IsManualAttendantPay, ref Treasury_No, IsExport);

                if (Treasury_No.Value > 0)
                {
                    if (TE_ID > 0)
                        result = linqDB.UpdateFinalStatusTicketException(TE_ID);

                    return Treasury_No.Value;
                }
                else
                    return -1;

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return -1;
            }
        }

        //<summary>
        //Gets the list of machines for all installations
        //</summary>
        //<returns>success or failure</returns>        
        //public DataTable FillMachines()
        //{
        //    DataTable dtMachines = new DataTable();
        //    try
        //    {
        //        dtMachines = handpayDataAccess.GetInstallationList();
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);                
        //    }
        //    return dtMachines;
        //}

        //<summary>
        //Save the expired or void treasury entries
        //</summary>
        //<returns>success or failure</returns>        
        public bool SaveVoidorExpiredTreasury(VoidOrExpiredTreasury ExpiredTreasury)
        {
            bool bStatus = false;
            int iSaveStatus = handpayDataAccess.UpdateVoidorExpiredTreasury(ExpiredTreasury);

            if (iSaveStatus == -1 || iSaveStatus > 0)
                bStatus = false;

            return bStatus;
        }

        //<summary>
        //Gets the list of ticket exceptions
        //</summary>
        //<returns>success or failure</returns>        
        public DataTable GetTicketExceptions(string TicketNo)
        {
            DataTable dtExceptions = new DataTable();
            try
            {
                dtExceptions = handpayDataAccess.GetTicketingExceptionTable(TicketNo);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return dtExceptions;
        }

        //<summary>
        //Update final status in Ticket Exception table
        //</summary>
        //<returns>success or failure</returns>       
        public bool UpdateFinalStatusTicketException(string TicketNo)
        {
            try
            {
                return handpayDataAccess.UpdateTicketException_FinalStatus(TicketNo);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }


        //<summary>
        //Void the treasury transactions.
        //</summary>
        //<returns>success or failure</returns>        
        public int VoidTreasuryNegativeTrans(VoidTranCreate NegativeTransaction)
        {
            int? OutVal = 0;
            int result;
            try
            {
                result = linqDB.VoidCreate(int.Parse(NegativeTransaction.TreasuryID),NegativeTransaction.Date, 
                    int.Parse(NegativeTransaction.UserNo), ref OutVal);
                return OutVal.Value;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return -99;
            }
        }

        public int GetUserID(int? SecurityUserID)
        {
            int? OutVal = 0;
            int Result;
            try
            {
                Result = linqDB.rsp_GetUserNoBySecurityUserID(SecurityUserID, ref OutVal);
               
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return OutVal.Value;
        }


        public List<AssetNumberResult> GetAssetNumber(int installation_No)
        {
            ISingleResult<AssetNumberResult> result = linqDB.GetAssetNumber(installation_No);


            return result.ToList<AssetNumberResult>();
        }

        public string GetEPIDetails(int installation_No)
        {
            string result = linqDB.GetEPIDetails(installation_No);
            return result;
        }
        public int Clearhandpay(int InstallationNo)
        {
            int result = 0;
            try
            {
                result = this.GetMachineManager().ClearHandPayTilt(InstallationNo);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                this.ReleaseMachineManager();
            }
            return result;
        }

        public bool CheckIfHandpayProcessed(FillTreasuryList fillTreasuryList)
        {
            int? Result = 0;
            linqDB.CheckIfHandpayProcessed(fillTreasuryList.TreasuryDate, fillTreasuryList.Amount, fillTreasuryList.Installation_No,
               fillTreasuryList.HP_Type, fillTreasuryList.TE_ID, ref Result);

            return Convert.ToBoolean(Result);
        }


        public DateTime? GetTreasuryDateTime(int Treasury_ID)
        {
            try
            {
                return handpayDataAccess.GetTreasuryDateTime(Treasury_ID);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }

        }

        #region GCD
        public bool RollbackProcessHandPay(int Ticket_ExceptionID, int Treasury_No)
        {
            try
            {
                return handpayDataAccess.RollbackProcessHandPay(Ticket_ExceptionID, Treasury_No);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }


        public bool ExportHandPay(int Treasury_No)
        {
            return handpayDataAccess.ExportHandPay(Treasury_No);
        }


        //For getting Denom Value
        public List<DenomValueResult> GetDenomValue(string Stock_No)
        {
            ISingleResult<DenomValueResult> result = linqDB.GetDenomValue(Stock_No);


            return result.ToList<DenomValueResult>();
        }
        #endregion
    }
}
