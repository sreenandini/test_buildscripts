using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;
using BMC.CoreLib.Diagnostics;
using System.Data.Linq;
using System.Xml.Linq;
using System.IO;
using BMC.Common.ExceptionManagement;

namespace BMC.EnterpriseBusiness.Business
{
    public class ViewSitesBusiness : DisposableObject
    {
        public ViewSitesBusiness() { }

        public VSSiteDetailsEntity GetSiteDetails(int siteID)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetHourlyStatisticsTypes");
            VSSiteDetailsEntity result = new VSSiteDetailsEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetVSSiteDetailsResult> dbResults = db.rsp_GetVSSiteDetails(siteID);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetVSSiteDetailsResult dbResult in dbResults)
                        {
                            result.Add(new VSSiteDetailEntity()
                            {
                                Sub_Company_ID = dbResult.Sub_Company_ID,
                                Site_Address_1 = dbResult.Site_Address_1,
                                Site_Address_2 = dbResult.Site_Address_2,
                                Site_Address_3 = dbResult.Site_Address_3,
                                Site_Address_4 = dbResult.Site_Address_4,
                                Site_Address_5 = dbResult.Site_Address_5,
                                Site_Code = dbResult.Site_Code,
                                Site_Name = dbResult.Site_Name,
                                Company_Name = dbResult.Company_Name,
                                Sub_Company_Name = dbResult.Sub_Company_Name,
                                Site_Manager = dbResult.Site_Manager,
                                Site_Postcode = dbResult.Site_Postcode,
                                Site_Reference = dbResult.Site_Reference,
                                Site_Phone_No = dbResult.Site_Phone_No,
                                Sub_Company_Area_Name = dbResult.Sub_Company_Area_Name,
                                Sub_Company_Region_Name = dbResult.Sub_Company_Region_Name,
                                Sub_Company_District_Name = dbResult.Sub_Company_District_Name,
                                Site_Trade_Type = dbResult.Site_Trade_Type,
                                Site_Memo = dbResult.Site_Memo,
                                Sec_Brewery_Name = dbResult.Sec_Brewery_Name,
                                Staff_Last_Name = dbResult.Staff_Last_Name,
                                Staff_First_Name = dbResult.Staff_First_Name,
                                Site_Start_Date = dbResult.Site_Start_Date,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSSiteTreesEntity GetSiteTree(ViewSiteSearchEntity search)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetHourlyStatisticsTypes");
            VSSiteTreesEntity result = new VSSiteTreesEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetVSSiteTreeResult> dbResults = db.rsp_GetVSSiteTree(search.UserId,
                        search.ActiveSitesOnly, search.SearchTextFormatted,
                        search.HasSupplier, search.HasAddress, search.HasSiteRef,
                        (search.HasBarPositionId ? search.BarPositionId : null),
                        search.CompanyID, search.Sub_CompanyID,
                        search.Sub_Company_Region_ID, search.Sub_Company_Area_ID,
                        search.Sub_Company_District_ID,
                        search.Operator_ID, search.Depot_ID,
                        search.Machine_Type_ID, search.Manufacturer_ID,
                        search.SiteRepId, search.ExcludeVacant.SafeValue(),
                        search.ModelSearch, search.PayoutPercentageFrom, search.PayoutPercentageTo);
                    //ISingleResult<rsp_GetVSSiteTreeResult> dbResults = db.rsp_GetVSSiteTree(search.UserId);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetVSSiteTreeResult dbResult in dbResults)
                        {
                            result.Add(new VSSiteTreeEntity()
                            {
                                Company_ID = dbResult.Company_ID,
                                Company_Name = dbResult.Company_Name,
                                Sub_Company_ID = dbResult.Sub_Company_ID,
                                Sub_Company_Name = dbResult.Sub_Company_Name,
                                Site_ID = dbResult.Site_ID,
                                Site_Name = dbResult.Site_Name,
                                Site_Code = dbResult.Site_Code,
                                Site_Address_1 = dbResult.Site_Address_1,
                                Site_Address_2 = dbResult.Site_Address_2,
                                Site_Address_3 = dbResult.Site_Address_3,
                                Site_Status_Id = dbResult.Site_Status_Id,
                                Site_Inactive_Date = dbResult.Site_Inactive_Date,
                                WebURL = dbResult.WebURL,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSInstallationsEntity GetInstallations(int siteID, int? barPositionId)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetInstallations");
            VSInstallationsEntity result = new VSInstallationsEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetCurrentInstallationsResult> dbResults = db.rsp_GetCurrentInstallations(siteID, barPositionId);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetCurrentInstallationsResult dbResult in dbResults)
                        {
                            try
                            {
                                result.Add(new VSInstallationEntity()
                                {
                                    Bar_Position_ID = dbResult.Bar_Position_ID,
                                    Bar_Position = dbResult.Bar_Position,
                                    Bar_Position_Name = dbResult.Bar_Position_Name,
                                    Bar_Position_End_Date = dbResult.Bar_Position_End_Date,
                                    Bar_Pos_Type_Code = dbResult.Bar_Pos_Type_Code,
                                    Bar_Position_Supplier_Position_Code = dbResult.Bar_Position_Supplier_Position_Code,
                                    Bar_Position_Supplier_Site_Code = dbResult.Bar_Position_Supplier_Site_Code,
                                    Bar_Position_Use_Terms = dbResult.Bar_Position_Use_Terms,
                                    Bar_Position_Net_Target = dbResult.Bar_Position_Net_Target,
                                    Bar_Position_Price_Per_Play = dbResult.Bar_Position_Price_Per_Play,
                                    Bar_Position_Category = dbResult.Bar_Position_Category,
                                    Bar_Position_Company_Target = dbResult.Bar_Position_Company_Target,
                                    Bar_Position_Collection_Day = dbResult.Bar_Position_Collection_Day,
                                    Bar_Position_Company_Position_Code = dbResult.Bar_Position_Company_Position_Code,
                                    Bar_Position_Jackpot = dbResult.Bar_Position_Jackpot,
                                    Bar_Position_Percentage_Payout = dbResult.Bar_Position_Percentage_Payout,
                                    Bar_Position_Machine_Enabled = dbResult.Bar_Position_Machine_Enabled,
                                    Bar_Position_Note_Acceptor_Enabled = dbResult.Bar_Position_Note_Acceptor_Enabled,
                                    Zone_Name = dbResult.Zone_Name,
                                    installation_token_value = dbResult.installation_token_value,
                                    Installation_ID = dbResult.Installation_ID,
                                    Installation_Start_Date = dbResult.Installation_Start_Date,
                                    Installation_End_Date = dbResult.Installation_End_Date,
                                    Installation_Change_Flag = dbResult.Installation_Change_Flag,
                                    Installation_BACTA_Code_Override = dbResult.Installation_BACTA_Code_Override,
                                    Installation_Price_Per_Play = dbResult.Installation_Price_Per_Play,
                                    Installation_Jackpot_Value = dbResult.Installation_Jackpot_Value,
                                    Installation_Percentage_Payout = dbResult.Installation_Percentage_Payout,
                                    Datapak_ID = dbResult.Datapak_ID,
                                    Installation_RDC_Datapak_Version = dbResult.Installation_RDC_Datapak_Version,
                                    Installation_RDC_Datapak_Type = dbResult.Installation_RDC_Datapak_Type,
                                    Machine_ID = dbResult.Machine_ID,
                                    Machine_Name = dbResult.Machine_Name,
                                    Machine_Stock_No = dbResult.Machine_Stock_No,
                                    Machine_Test = dbResult.Machine_Test,
                                    Machine_Manufacturers_Serial_No = dbResult.Machine_Manufacturers_Serial_No,
                                    Machine_Alternative_Serial_Numbers = dbResult.Machine_Alternative_Serial_Numbers,
                                    Machine_BACTA_Code = dbResult.Machine_BACTA_Code,
                                    Machine_Due_In_Stock = dbResult.Machine_Due_In_Stock,
                                    Machine_Due_In_Stock_Date = dbResult.Machine_Due_In_Stock_Date,
                                    Machine_Class_Model_Code = dbResult.Machine_Class_Model_Code,
                                    Depreciation_Policy_ID = dbResult.Depreciation_Policy_ID,
                                    Machine_Type_Code = dbResult.Machine_Type_Code,
                                    Operator_Name = dbResult.Operator_Name,
                                    Depot_Name = dbResult.Depot_Name,
                                    Depot_ID = dbResult.Depot_ID,
                                    Installation_RDC_Machine_Code = dbResult.Installation_RDC_Machine_Code,
                                    Installation_RDC_Secondary_Machine_Code = dbResult.Installation_RDC_Secondary_Machine_Code,
                                    Standard_Opening_Hours_ID = dbResult.Standard_Opening_Hours_ID,
                                    Standard_Opening_Hours_Description = dbResult.Standard_Opening_Hours_Description,
                                    Installation_Datapak_SecondaryMachineCode_Version = dbResult.Installation_Datapak_SecondaryMachineCode_Version,
                                    Installation_Datapak_SecondaryMachineCode_CashOrToken = dbResult.Installation_Datapak_SecondaryMachineCode_CashOrToken,
                                    Installation_Datapak_SecondaryMachineCode_PercentagePayout = dbResult.Installation_Datapak_SecondaryMachineCode_PercentagePayout,
                                    Installation_Datapak_SecondaryMachineCode_Type = dbResult.Installation_Datapak_SecondaryMachineCode_Type,
                                    Installation_Datapak_SecondaryMachineCode_PriceOfPlay = dbResult.Installation_Datapak_SecondaryMachineCode_PriceOfPlay,
                                    Installation_Datapak_SecondaryMachineCode_CoinSystem = dbResult.Installation_Datapak_SecondaryMachineCode_CoinSystem,
                                    Installation_Datapak_SecondaryMachineCode_Dataport = dbResult.Installation_Datapak_SecondaryMachineCode_Dataport,
                                    Installation_Datapak_SecondaryMachineCode_Jackpot = dbResult.Installation_Datapak_SecondaryMachineCode_Jackpot,
                                    Installation_Datapak_FirmwareVersion = dbResult.Installation_Datapak_FirmwareVersion,
                                    Installation_Datapak_FirmwareRevision = dbResult.Installation_Datapak_FirmwareRevision,
                                    Installation_Status = dbResult.Installation_Status,
                                    Manufacturer_Name = dbResult.Manufacturer_Name,
                                    GMUNo = dbResult.GMUNo,
                                    IsMultiGame = dbResult.IsMultiGame.GetValueOrDefault()
                                });
                            }
                            catch (Exception ex)
                            {
                                Log.Exception(PROC, ex);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSDropPositionsEntity GetDropPositions(int site, int bar_position, int records, int weeks, int periods)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetHourlyStatisticsTypes");
            VSDropPositionsEntity result = new VSDropPositionsEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetSiteViewerCollsResult> dbResults = db.rsp_GetSiteViewerColls(records, weeks, periods, bar_position, site);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetSiteViewerCollsResult dbResult in dbResults)
                        {
                            result.Add(new VSDropPositionEntity()
                            {
                                Collection_Source = dbResult.Collection_Source,
                                Batch_Received_All = dbResult.Batch_Received_All,
                                Batch_ID = dbResult.Batch_ID,
                                Period_End_ID = dbResult.Period_End_ID,
                                Collection_Terms_Invalid = dbResult.Collection_Terms_Invalid,
                                COLLECTION_REPLACEMENT = dbResult.COLLECTION_REPLACEMENT,
                                Collection_Terms_Invalid1 = dbResult.Collection_Terms_Invalid1,
                                Collection_Terms_Invalid_Ignore = dbResult.Collection_Terms_Invalid_Ignore,
                                Collection_Processed_Through_Terms = dbResult.Collection_Processed_Through_Terms,
                                EDI_Import_Log_ID = dbResult.EDI_Import_Log_ID,
                                Collection_ID = dbResult.Collection_ID,
                                Collection_Date = dbResult.Collection_Date,
                                Machine_Name = dbResult.Machine_Name,
                                GameName = dbResult.GameName,
                                Operator_Name = dbResult.Operator_Name,
                                Depot_ID = dbResult.Depot_ID,
                                Depot_Name = dbResult.Depot_Name,
                                Collection_Days = dbResult.Collection_Days,
                                Cash_Take = dbResult.Cash_Take,
                                CashCollected = dbResult.CashCollected,
                                PercentageIn = dbResult.PercentageIn,
                                PercentageOut = dbResult.PercentageOut,
                                Collection_declared_Notes = dbResult.Collection_declared_Notes,
                                Refunds = dbResult.Refunds,
                                Net_Coin = dbResult.Net_Coin,
                                Collection_Ticket_Balance = dbResult.Collection_Ticket_Balance,
                                DecHandpay = dbResult.DecHandpay,
                                Shortpay = dbResult.Shortpay,
                                Declared_Notes = dbResult.Declared_Notes,
                                Note_Var = dbResult.Note_Var,
                                Coin_Var = dbResult.Coin_Var,
                                DecTicketBalance = dbResult.DecTicketBalance,
                                Net_Coin1 = dbResult.Net_Coin1,
                                DecHandpay1 = dbResult.DecHandpay1,
                                RDC_Notes = dbResult.RDC_Notes,
                                RDCHandpay = dbResult.RDCHandpay,
                                Ticket_Var = dbResult.Ticket_Var,
                                Handpay_Var = dbResult.Handpay_Var,
                                Take_Var = dbResult.Take_Var,
                                DecTicketBalance1 = dbResult.DecTicketBalance1,
                                RDC_Coins = dbResult.RDC_Coins,
                                Void = dbResult.Void,
                                RDC_Ticket_Balance = dbResult.RDC_Ticket_Balance,
                                WinLossVar = dbResult.WinLossVar,
                                Collection_Handpay_Var = dbResult.Collection_Handpay_Var,
                                RDC_Take = dbResult.RDC_Take,
                                Refills = dbResult.Refills,
                                Collection_Supplier_Share = dbResult.Collection_Supplier_Share,
                                Collection_Company_Share = dbResult.Collection_Company_Share,
                                Collection_Location_Share = dbResult.Collection_Location_Share,
                                Collection_Other_Share = dbResult.Collection_Other_Share,
                                Collection_AMLD = dbResult.Collection_AMLD,
                                Remarks = dbResult.Remarks,
                                Period_Id = dbResult.Period_Id,
                                Period_End_ID1 = dbResult.Period_End_ID1,
                                Lag = dbResult.Lag,
                                TargetVariance = dbResult.TargetVariance,
                                RDCIn = dbResult.RDCIn,
                                RDCOut = dbResult.RDCOut,
                                RDCCash = dbResult.RDCCash,
                                RDCVar = dbResult.RDCVar,
                                EftIn = dbResult.EftIn,
                                DecEftIn = dbResult.DecEftIn,
                                EftOut = dbResult.EftOut,
                                DecEftOut = dbResult.DecEftOut,
                                Secondary_Brewery_Name = dbResult.Secondary_Brewery_Name,
                                Secondary_Sub_Company_Period_End_ID = dbResult.Secondary_Sub_Company_Period_End_ID,
                                RDC_Coins_Out = dbResult.RDC_Coins_Out,
                                Week_End_ID = dbResult.Week_End_ID,
                                VTP = dbResult.VTP,
                                Sub_Company_Name = dbResult.Sub_Company_Name,
                                Company_Name = dbResult.Company_Name,
                                MeterCashIn = dbResult.MeterCashIn,
                                MeterCashOut = dbResult.MeterCashOut,
                                Handle = dbResult.Handle,
                                PIndex = dbResult.PIndex,
                                PacePrev9Days = dbResult.PacePrev9Days,
                                PacePrev9Cash = dbResult.PacePrev9Cash,
                                PaceDays = dbResult.PaceDays,
                                PaceCash = dbResult.PaceCash,
                                Collection_GPT = dbResult.Collection_GPT,
                                Collection_FOBT_Stakes = dbResult.Collection_FOBT_Stakes,
                                Collection_FOBT_Payout = dbResult.Collection_FOBT_Payout,
                                Collection_Transactions = dbResult.Collection_Transactions,
                                Machine_Start_Date = dbResult.Machine_Start_Date,                                
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSDropBatchesEntity GetDropBatches(int site, int? weeks, int? periods)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetHourlyStatisticsTypes");
            VSDropBatchesEntity result = new VSDropBatchesEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetSiteViewerCollsBatchResult> dbResults = db.rsp_GetSiteViewerCollsBatch(weeks, periods, site);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetSiteViewerCollsBatchResult dbResult in dbResults)
                        {
                            result.Add(new VSDropBatchEntity()
                            {
                                BatchDate = dbResult.BatchDate,
                                Batch_Date_performed = dbResult.Batch_Date_performed,
                                Batch_Time_performed = dbResult.Batch_Time_performed,
                                Batch_Name = dbResult.Batch_Name,
                                Batch_ID = dbResult.Batch_ID,
                                BatchRef = dbResult.BatchRef,
                                BatchCount = dbResult.BatchCount,
                                CashCollected = dbResult.CashCollected,
                                Notes = dbResult.Notes,
                                DecTicketBalance = dbResult.DecTicketBalance,
                                Coins = dbResult.Coins,
                                TicktesIn = dbResult.TicktesIn,
                                TicktesOut = dbResult.TicktesOut,
                                Net_Coin = dbResult.Net_Coin,
                                Refills = dbResult.Refills,
                                Refunds = dbResult.Refunds,
                                Handpay = dbResult.Handpay,
                                Shortpay = dbResult.Shortpay,
                                NotesVar = dbResult.NotesVar,
                                CoinVar = dbResult.CoinVar,
                                TicketVar = dbResult.TicketVar,
                                HandpayVar = dbResult.HandpayVar,
                                TakeVar = dbResult.TakeVar,
                                RDCRefill = dbResult.RDCRefill,
                                RDCVar = dbResult.RDCVar,
                                MeterCash = dbResult.MeterCash,
                                MeterRefill = dbResult.MeterRefill,
                                MeterVar = dbResult.MeterVar,
                                RDC_Notes = dbResult.RDC_Notes,
                                BatchAdj = dbResult.BatchAdj,
                                DecHandpay = dbResult.DecHandpay,
                                RDCHandpay = dbResult.RDCHandpay,
                                RDC_Tickets_In = dbResult.RDC_Tickets_In,
                                RDC_Tickets_Out = dbResult.RDC_Tickets_Out,
                                MeterHandpay = dbResult.MeterHandpay,
                                Ticket = dbResult.Ticket,
                                RDC_Take = dbResult.RDC_Take,
                                Cash_Take = dbResult.Cash_Take,
                                WinLossVar = dbResult.WinLossVar,
                                RDC_Coins = dbResult.RDC_Coins,
                                HopperChange = dbResult.HopperChange,
                                RDC_Coins_Out = dbResult.RDC_Coins_Out,
                                Void = dbResult.Void,
                                Expired = dbResult.Expired,
                                Progressive_Value_Declared = dbResult.Progressive_Value_Declared,
                                Progressive_Value_Variance = dbResult.Progressive_Value_Variance,
                                EftIn = dbResult.EftIn,
                                DecEftIn = dbResult.DecEftIn,
                                EftOut = dbResult.EftOut,
                                DecEftOut = dbResult.DecEftOut,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSDropBatches2Entity GetDropBatches(int site, int? batchId)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetHourlyStatisticsTypes");
            VSDropBatches2Entity result = new VSDropBatches2Entity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetSiteViewerCollsBatchDataResult> dbResults = db.rsp_GetSiteViewerCollsBatchData(batchId, site);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetSiteViewerCollsBatchDataResult dbResult in dbResults)
                        {
                            result.Add(new VSDropBatch2Entity()
                            {
                                BatchDate = dbResult.BatchDate,
                                Batch_Date_performed = dbResult.Batch_Date_performed,
                                Batch_Time_performed = dbResult.Batch_Time_performed,
                                Batch_Name = dbResult.Batch_Name,
                                Batch_ID = dbResult.Batch_ID,
                                BatchRef = dbResult.BatchRef,
                                BatchCount = dbResult.BatchCount,
                                CashCollected = dbResult.CashCollected,
                                Notes = dbResult.Notes,
                                DecTicketBalance = dbResult.DecTicketBalance,
                                Coins = dbResult.Coins,
                                TicktesIn = dbResult.TicktesIn,
                                TicktesOut = dbResult.TicktesOut,
                                Net_Coin = dbResult.Net_Coin,
                                Refills = dbResult.Refills,
                                Refunds = dbResult.Refunds,
                                Handpay = dbResult.Handpay,
                                Shortpay = dbResult.Shortpay,
                                NotesVar = dbResult.NotesVar,
                                CoinVar = dbResult.CoinVar,
                                TicketVar = dbResult.TicketVar,
                                HandpayVar = dbResult.HandpayVar,
                                Take_Var = dbResult.Take_Var,
                                RDCRefill = dbResult.RDCRefill,
                                MeterCash = dbResult.MeterCash,
                                MeterRefill = dbResult.MeterRefill,
                                MeterVar = dbResult.MeterVar,
                                RDC_Notes = dbResult.RDC_Notes,
                                BatchAdj = dbResult.BatchAdj,
                                DecHandpay = dbResult.DecHandpay,
                                RDCHandpay = dbResult.RDCHandpay,
                                RDC_Tickets_In = dbResult.RDC_Tickets_In,
                                RDC_Tickets_Out = dbResult.RDC_Tickets_Out,
                                MeterHandpay = dbResult.MeterHandpay,
                                Ticket = dbResult.Ticket,
                                RDC_Take = dbResult.RDC_Take,
                                RDC_Coins = dbResult.RDC_Coins,
                                Hopperchange = dbResult.Hopperchange,
                                RDC_Coins_Out = dbResult.RDC_Coins_Out,
                                Void = dbResult.Void,
                                Expired = dbResult.Expired,
                                Progressive_Value_Declared = dbResult.Progressive_Value_Declared,
                                Progressive_Value_Variance = dbResult.Progressive_Value_Variance,
                                EftIn = dbResult.EftIn,
                                EftOut = dbResult.EftOut,
                                Cash_Take = dbResult.Cash_Take,
                                WinLossVar = dbResult.WinLossVar,
                                DecEftIn = dbResult.DecEftIn,
                                DecEftOut = dbResult.DecEftOut,
                                PromoCashableIn=dbResult.PromoCashableAmount,
                                PromoNonCashableIn=dbResult.PromoNonCashableAmount,

                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSDropWeeksEntity GetDropWeeks(int site, int weeks)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetHourlyStatisticsTypes");
            VSDropWeeksEntity result = new VSDropWeeksEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetSiteViewerCollsBatchWeekResult> dbResults = db.rsp_GetSiteViewerCollsBatchWeek(weeks, site);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetSiteViewerCollsBatchWeekResult dbResult in dbResults)
                        {
                            result.Add(new VSDropWeekEntity()
                            {
                                Week_ID = dbResult.Week_ID,
                                WeekNumber = dbResult.WeekNumber,
                                StartDate = dbResult.StartDate,
                                EndDate = dbResult.EndDate,
                                WeekCount = dbResult.WeekCount,
                                CashCollected = dbResult.CashCollected,
                                CashCollected1 = dbResult.CashCollected1,
                                Notes = dbResult.Notes,
                                Coins = dbResult.Coins,
                                TicktesIn = dbResult.TicktesIn,
                                TicktesOut = dbResult.TicktesOut,
                                Refills = dbResult.Refills,
                                Refunds = dbResult.Refunds,
                                AttendantPay = dbResult.AttendantPay,
                                Shortpay = dbResult.Shortpay,
                                NotesVar = dbResult.NotesVar,
                                CoinVar = dbResult.CoinVar,
                                VoucherVar = dbResult.VoucherVar,
                                HandpayVar = dbResult.HandpayVar,
                                TakeVar = dbResult.TakeVar,
                                RDCRefill = dbResult.RDCRefill,
                                MeterCash = dbResult.MeterCash,
                                MeterRefill = dbResult.MeterRefill,
                                MeterVar = dbResult.MeterVar,
                                RDC_Notes = dbResult.RDC_Notes,
                                BatchAdj = dbResult.BatchAdj,
                                DecHandpay = dbResult.DecHandpay,
                                RDCHandpay = dbResult.RDCHandpay,
                                RDC_Tickets_In = dbResult.RDC_Tickets_In,
                                RDC_Tickets_Out = dbResult.RDC_Tickets_Out,
                                MeterHandpay = dbResult.MeterHandpay,
                                Voucher = dbResult.Voucher,
                                RDC_Take = dbResult.RDC_Take,
                                Cash_Take = dbResult.Cash_Take,
                                RDC_Coins = dbResult.RDC_Coins,
                                Hopperchange = dbResult.Hopperchange,
                                RDC_Coins_Out = dbResult.RDC_Coins_Out,
                                Void = dbResult.Void,
                                Expired = dbResult.Expired,
                                WinLossVar = dbResult.WinLossVar,
                                DecEftIn = dbResult.DecEftIn,
                                DecEftOut = dbResult.DecEftOut,
                                Progressive_Value_Declared = dbResult.Progressive_Value_Declared,
                                Progressive_Value_Variance = dbResult.Progressive_Value_Variance,
                                EftIn = dbResult.EftIn,
                                EftOut = dbResult.EftOut,
                                PromoCashableIn=dbResult.PromoCashableIn,
                                PromoNonCashableIn=dbResult.PromoNonCashableIn,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSCompaniesEntity GetCompanies(int SecurityID)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetCompanies");
            VSCompaniesEntity result = new VSCompaniesEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetCompanyDetailsResult> dbResults = db.rsp_GetCompanyDetails(SecurityID);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetCompanyDetailsResult dbResult in dbResults)
                        {
                            result.Add(new VSCompanyEntity()
                            {
                                Company_ID = dbResult.Company_ID,
                                Company_Name = dbResult.Company_Name,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSSubCompaniesEntity GetSubCompanies(int companyId,int SecurityUserID)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetSubCompanies");
            VSSubCompaniesEntity result = new VSSubCompaniesEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetSubCompanyDetailsResult> dbResults = db.rsp_GetSubCompanyDetails(companyId,SecurityUserID );
                    if (dbResults != null)
                    {
                        foreach (rsp_GetSubCompanyDetailsResult dbResult in dbResults)
                        {
                            result.Add(new VSSubCompanyEntity()
                            {
                                Sub_Company_ID = dbResult.Sub_Company_ID,
                                Sub_Company_Name = dbResult.Sub_Company_Name,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSOperatorsEntity GetOperators()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetCompanies");
            VSOperatorsEntity result = new VSOperatorsEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetOperatorDetailsResult> dbResults = db.GetOperatorDetails(null);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetOperatorDetailsResult dbResult in dbResults)
                        {
                            result.Add(new VSOperatorEntity()
                            {
                                Operator_ID = dbResult.Operator_ID,
                                Operator_Name = dbResult.Operator_Name,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSSubCompanyRegionsEntity GetSubCompanyRegions(int? subCompanyId, int? companyId)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetSubCompanies");
            VSSubCompanyRegionsEntity result = new VSSubCompanyRegionsEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetSubCompanyRegionsForFilterResult> dbResults = db.rsp_GetSubCompanyRegionsForFilter(subCompanyId, companyId);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetSubCompanyRegionsForFilterResult dbResult in dbResults)
                        {
                            result.Add(new VSSubCompanyRegionEntity()
                            {
                                Sub_Company_Region_ID = dbResult.Sub_Company_Region_ID,
                                Sub_Company_Region_Name = dbResult.Sub_Company_Region_Name,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSSubCompanyAreasEntity GetSubCompanyAreas(int? regionId, int companyId, int subCompanyId)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetSubCompanies");
            VSSubCompanyAreasEntity result = new VSSubCompanyAreasEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetAreaDetailsResult> dbResults = db.rsp_GetAreaDetails(regionId, companyId, subCompanyId);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetAreaDetailsResult dbResult in dbResults)
                        {
                            result.Add(new VSSubCompanyAreaEntity()
                            {
                                Sub_Company_Area_ID = dbResult.Sub_Company_Area_ID,
                                Sub_Company_Area_Name = dbResult.Sub_Company_Area_Name,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSSubCompanyDistrictsEntity GetSubCompanyDistricts(int? regionId, int? areaId, int subCompanyId, int companyId)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetSubCompanies");
            VSSubCompanyDistrictsEntity result = new VSSubCompanyDistrictsEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetDistrictDetailsResult> dbResults = db.rsp_GetDistrictDetails(regionId, areaId, subCompanyId, companyId);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetDistrictDetailsResult dbResult in dbResults)
                        {
                            result.Add(new VSSubCompanyDistrictEntity()
                            {
                                Sub_Company_District_ID = dbResult.Sub_Company_District_ID,
                                Sub_Company_District_Name = dbResult.Sub_Company_District_Name,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSStaffsEntity GetStaffs()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetCompanies");
            VSStaffsEntity result = new VSStaffsEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetVSStaffDetailsResult> dbResults = db.rsp_GetVSStaffDetails();
                    if (dbResults != null)
                    {
                        foreach (rsp_GetVSStaffDetailsResult dbResult in dbResults)
                        {
                            result.Add(new VSStaffEntity()
                            {
                                Staff_ID = dbResult.Staff_ID,
                                Staff_Name = dbResult.Staff_Name,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSMachineTypesEntity GetMachineTypes()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetCompanies");
            VSMachineTypesEntity result = new VSMachineTypesEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetMachineTypeDetailsResult> dbResults = db.GetMachineTypeDetails(null);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetMachineTypeDetailsResult dbResult in dbResults)
                        {
                            result.Add(new VSMachineTypeEntity()
                            {
                                Machine_Type_ID = dbResult.Machine_Type_ID,
                                Machine_Type_Code = dbResult.Machine_Type_Code,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSDepotsEntity GetDepots(int? operatorId)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetSubCompanies");
            VSDepotsEntity result = new VSDepotsEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetDepoDetailsResult> dbResults = db.rsp_GetDepoDetails(operatorId);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetDepoDetailsResult dbResult in dbResults)
                        {
                            result.Add(new VSDepotEntity()
                            {
                                Depot_ID = dbResult.Depot_ID,
                                Depot_Name = dbResult.Depot_Name,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSManufacturersEntity GetManufacturers()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetCompanies");
            VSManufacturersEntity result = new VSManufacturersEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetManufacturerResult> dbResults = db.GetManufacturer(null);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetManufacturerResult dbResult in dbResults)
                        {
                            result.Add(new VSManufacturerEntity()
                            {
                                Manufacturer_ID = dbResult.Manufacturer_ID,
                                Manufacturer_Name = dbResult.Manufacturer_Name,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        /// <summary>
        /// Gets the asset game report.
        /// </summary>
        /// <param name="barPosition_ID">The bar position_ ID.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public VSAssetGameReportsEntity GetAssetGameReport(int barPosition_ID,int Site_Id, DateTime startDate, DateTime endDate)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetAssetGameReport");
            VSAssetGameReportsEntity result = new VSAssetGameReportsEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_AssetGameReportResult> dbResults = db.GetAssetGameReport(barPosition_ID,Site_Id, startDate, endDate);
                    if (dbResults != null)
                    {
                        foreach (rsp_AssetGameReportResult dbResult in dbResults)
                        {
                            result.Add(new VSAssetGameReportEntity()
                            {
                                Bar_Position_ID = dbResult.Bar_Position_ID,
                                Machine_Stock_No = dbResult.Machine_Stock_No,
                                MG_Game_Manufacturer_Name = dbResult.MG_Game_Manufacturer_Name,
                                Game_Name = dbResult.Game_Name,
                                MG_Alias_Game_Name = dbResult.MG_Alias_Game_Name,
                                Handle = dbResult.Handle,
                                NetWin = dbResult.NetWin,
                                DailyWin = dbResult.DailyWin,
                                AvgBet = dbResult.AvgBet,
                                Played = dbResult.Played,
                                TotalBet = dbResult.TotalBet,
                                TotalWon = dbResult.TotalWon,
                                Theo = dbResult.Theo,
                                Actual = dbResult.Actual,
                                Variance = dbResult.Variance,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSLastBatchIdsEntity GetLastBatchIds(int site, int? weeks, int? periods)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetLastBatchIds");
            VSLastBatchIdsEntity result = new VSLastBatchIdsEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetLastBatchIdResult> dbResults = db.rsp_GetLastBatchId(site, weeks, periods);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetLastBatchIdResult dbResult in dbResults)
                        {
                            result.Add(new VSLastBatchIdEntity()
                            {
                                Batch_ID = dbResult.Batch_ID,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSLastWeekIdsEntity GetLastWeekIds(int site, int? weeks)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetLastWeekIds");
            VSLastWeekIdsEntity result = new VSLastWeekIdsEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetLastWeekIdResult> dbResults = db.rsp_GetLastWeekId(site, weeks);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetLastWeekIdResult dbResult in dbResults)
                        {
                            result.Add(new VSLastWeekIdEntity()
                            {
                                Calendar_Week_ID = dbResult.Calendar_Week_ID,
                                Week_Start_Date = dbResult.Week_Start_Date,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSMultiGameLibrariesEntity GetMultiGameLibraries(int installationID)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetMultiGameLibraries");
            VSMultiGameLibrariesEntity result = new VSMultiGameLibrariesEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_MultiGameLibraryResult> dbResults = db.rsp_MultiGameLibrary(installationID);
                    if (dbResults != null)
                    {
                        foreach (rsp_MultiGameLibraryResult dbResult in dbResults)
                        {
                            result.Add(new VSMultiGameLibraryEntity()
                            {
                                Game_Category_Name = dbResult.Game_Category_Name,
                                Manufacturer = dbResult.Manufacturer,
                                MG_Alias_Game_Name = dbResult.MG_Alias_Game_Name,
                                MG_Group_ID = dbResult.MG_Group_ID,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public VSPaytableForGamesEntity GetPaytableForGames(int installationID, int groupId)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetPaytableForGames");
            VSPaytableForGamesEntity result = new VSPaytableForGamesEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetPaytableForGameResult> dbResults = db.rsp_GetPaytableForGame(groupId, installationID);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetPaytableForGameResult dbResult in dbResults)
                        {
                            result.Add(new VSPaytableForGameEntity()
                            {
                                MaxBet = dbResult.MaxBet,
                                Payout = dbResult.Payout,
                                PaytableID = dbResult.PaytableID,
                                TheoreticalPayout = dbResult.TheoreticalPayout,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public bool DeleteCollectionBatch(int batchID)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetPaytableForGames");
            bool result = false;

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    result = (db.usp_VSDeleteCollectionBatch(batchID) == 0);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
        public void UpdateRemarksCollection(int collection_id, string Remarktext)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "UpdateRemarksCollection");
            try
            {

                EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext();
                db.GetRemarkscollection(collection_id, Remarktext);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public bool GetDepreciationDetailsFromMachineID(Int32 MachineID, ref double NBV, ref double DepreciationPerWeek,
            ref double PurchasePrice, DateTime? DateTo)
        {
            try
            {
                PurchasePrice = 0;
                DepreciationPerWeek = 0;
                NBV = 0;

                DepreciationPolicyEntity entity = new DepreciationPolicyEntity();
                ISingleResult<rsp_GetDepreciationPolicyInfoResult> results = null;
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    results = db.GetDepreciationPolicyInfo(MachineID);
                    foreach (var result in results)
                    {
                        entity.Depreciation_Policy_Details_Duration = result.Depreciation_Policy_Details_Duration;
                        entity.Depreciation_Policy_Details_ID = result.Depreciation_Policy_Details_ID;
                        entity.Depreciation_Policy_Details_Percentage = result.Depreciation_Policy_Details_Percentage;
                        entity.Depreciation_Policy_ID = result.Depreciation_Policy_ID;
                        entity.Depreciation_Policy_Residual_Value = result.Depreciation_Policy_Residual_Value;
                        entity.Machine_Class_ID = result.Machine_Class_ID;
                        entity.Machine_Depreciation_Start_Date = result.Machine_Depreciation_Start_Date;
                        entity.Machine_End_Date = result.Machine_End_Date;
                        entity.Machine_Original_Purchase_Price = result.Machine_Original_Purchase_Price;
                        break;
                    }
                }

                if (entity == null) return false;
                if ((DateTo == null && string.IsNullOrEmpty(entity.Machine_End_Date)))
                {
                    DateTo = DateTime.Now.Date;
                }
                else if (DateTo == null)
                {
                    DateTime temp;
                    if ((DateTime.TryParse(entity.Machine_End_Date, out temp)))
                        DateTo = temp;
                    else
                        DateTo = DateTime.Now.Date;
                }
                DateTime startDate;
                if ((DateTime.TryParse(entity.Machine_Depreciation_Start_Date, out startDate) && DateTo != null && entity.Depreciation_Policy_Details_ID != null))
                {
                    double Purchase_Price = Convert.ToDouble(entity.Machine_Original_Purchase_Price);
                    double Residual_Value = Convert.ToDouble(entity.Depreciation_Policy_Residual_Value);
                    double DaysLeft = 0;
                    double YearsLeft = 0;
                    double Depreciation_Perc = 0;
                    DaysLeft = ((DateTime)DateTo).Subtract(startDate).TotalDays;
                    if ((DaysLeft > 1 && Residual_Value < Purchase_Price))
                    {
                        YearsLeft = DaysLeft / 365;
                        bool isbreak = false;
                        while (YearsLeft > (entity.Depreciation_Policy_Details_Duration / 12))
                        {
                            Depreciation_Perc = Depreciation_Perc + Convert.ToDouble(entity.Depreciation_Policy_Details_Percentage);
                            YearsLeft = YearsLeft - (Convert.ToDouble(entity.Depreciation_Policy_Details_Duration) / 12);
                            if (YearsLeft <= (Convert.ToDouble(entity.Depreciation_Policy_Details_Duration) / 12))
                            {
                                Depreciation_Perc = Depreciation_Perc + (Convert.ToDouble(entity.Depreciation_Policy_Details_Percentage) * (YearsLeft / (Convert.ToDouble(entity.Depreciation_Policy_Details_Duration) / 12)));
                                isbreak = true;
                            }
                        }


                        PurchasePrice = Purchase_Price;
                        NBV = Purchase_Price - ((Depreciation_Perc / 100) * (Purchase_Price - Residual_Value));

                        if (isbreak)
                            DepreciationPerWeek = 0.0;
                        else
                            DepreciationPerWeek = ((Purchase_Price - Residual_Value) * (Convert.ToDouble(entity.Depreciation_Policy_Details_Percentage) / 100.0)) * 7 / (Convert.ToDouble(entity.Depreciation_Policy_Details_Duration) * 30.4);
                    }
                    else
                    {
                        PurchasePrice = Convert.ToDouble(entity.Machine_Original_Purchase_Price);
                        NBV = Convert.ToDouble(entity.Machine_Original_Purchase_Price);
                        DepreciationPerWeek = 0;
                    }
                }
                else
                {
                    PurchasePrice = 0;
                    DepreciationPerWeek = 0;
                    NBV = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return true;
        }
    }
}
