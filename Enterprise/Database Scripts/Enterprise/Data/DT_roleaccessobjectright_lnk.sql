/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 21/12/12 7:36:53 PM
 ************************************************************/

USE Enterprise
GO

TRUNCATE TABLE roleaccessobjectright_lnk
GO

INSERT INTO roleaccessobjectright_lnk
SELECT RoleAccessID, 0, 1 FROM RoleAccess 

GO

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.MainScreen')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'Exchange')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.Floor.View')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'FloorView')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.Floor.View.UnLockPosition')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'UnlockPositions')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.PosDetails')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'PositionDetails')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.FloorView.cs.InstallMachine')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'MachineInstallation')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.FloorView.cs.RemoveMachine')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'MachineRemoval')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.FloorView.cs.ReinstateMachine')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ReinstateMachine')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.FloorView.cs.SyncTicketExpire')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'TicketExpire')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.FloorView.cs.Handpay')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'AttendantPay')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CViewHandpay.btnManual')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ManualAttendantPay')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CViewHandpay.btnVoid')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'VOIDManualAttendantPay')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.Authorize.cs.MaxHandpay')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'MaxHandpay')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CViewHandpay.btnProcess')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'PROCESSManualAttendantPay')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.FloorView.cs.PlayerClub')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'PDPlayerClub')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.FloorView.cs.FieldService')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'FieldService')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CFieldService.btnRequestCall')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'RequestCall')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CFieldService.btnClear')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ClearCall')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CFieldService.btnReview')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ReviewNotes')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CFieldService.btnReview.AddNote')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'AddNote')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CFieldService.btnEscalate')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'EscalateCall')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.FloorView.cs.Events')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'Events')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.FloorView.cs.Events.ClearEvents')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ClearEvents')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.FloorView.cs.MachineMeters')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'MachineMeters')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.FloorView.cs.CurrentMeters')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'CurrentMeters')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.Authorize.cs.MachineMaintenance')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'MachineMaintenance')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.Authorize.cs.OverrideEvents')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'OverrideEvents')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.POS.Views.InstallationDetails')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'Details')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.Tickets')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'Vouchers')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CPrintTicket')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'PrintVoucher')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CRedeemTicket')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'RedeemVoucher')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.Authorize.cs.ReedemExpiredTicket')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'RedeemExpiredVoucher')

UPDATE roleaccessobjectright_lnk
SET SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE objectName = 'BMC.Presentation.MultipleVoucher')
WHERE RoleAccessID =(SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'MultipleVoucher')



UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.Authorize.cs.MultipleRedeemExpiredTicket')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'MultipleRedeemExpiredVoucher')


UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.Void')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'VoidTransactions')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.Void.btnVOID')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'Void')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.Reports')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'Reports')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.Hourly')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'Hourly')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.Player.Club')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'PlayerClub')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.Current.Calls')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'CurrentCalls')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.Machine Drop')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'MachineDrop')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CPerformDrop')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'PerformDrop')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CDeclaration')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'PerformDeclaration')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.BatchBreakdown.btnExport')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ExportBatch')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.CashDeskMananger')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'CashierTransactions')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CashDeskManager.UserControls.CashDeskManagerAllDetails.lvViewAll.TicketValue')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'DisplayTicketNumber')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.Audit')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'SystemAudit')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'CustomReports')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports.ExpenseDetailReport')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ExpenseDetailReport')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports.ExpiredVoucherCouponReport')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ExpiredVoucherCouponReport')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports.JackpotSlipSummaryReport')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'JackpotSlipSummaryReport')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports.MeterListReport')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'MeterListReport')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports.RedeemedTicketByDeviceReport')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'RedeemedTicketByDeviceReport')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports.TicketIssuedReport')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'TicketIssuedReport')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports.VoucherCouponLiabilityReport')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'VoucherCouponLiabilityReport')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports.ExceptionVoucherDetails')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ExceptionVoucherDetails')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports.CrossPropertyLiabilityTransferSummaryReport')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'CrossPropertyLiabilityTransferSummaryReport')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports.CrossPropertyLiabilityTransferDetailsReport')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'CrossPropertyLiabilityTransferDetailsReport')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports.CrossPropertyTicketAnalysisReport')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'CrossPropertyTicketAnalysisReport')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports.StackerLevelDetailsReport')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'StackerLevelDetailsReport')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.Site Settings')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'SiteSettings')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.Site Settings.btnSystemConfigParameters')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ConfigParameters')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.Factory')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'FactoryReset')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.AFTSettings')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'AFTSettings')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.ExchangeConfig.Login')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'BMCExchangeConfig')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.AFTEnableDisable')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'AFTEnableDisable')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.CoinDispenser')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'CoinDispenser')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CPpTicket')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ExceptionVoucher')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CashDeskManager.UserControls.CashDeskManager.DisplayTransactions')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'DisplayTransactions')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CrossTicketingSettings')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'CrossTicketingSettings')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.Shortpay')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ShortPay')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CommonCDOforDeclaration')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'CommonCDOforDeclaration')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CashDeskManager.UserControls.CashDeskManager.AccessOtherUsers')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'AccessOtherUsers')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.VoucherConfig')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'VoucherConfig')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.AttendantPay')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'AttendantPays')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.ManualAttendantPay')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ManualAttendantPays')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CAttendantpay.btnVoid')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'AttendantPayVoid')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.LicenseActivation')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ActivateLicense')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.Gmuupdatebin')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'Gmuupdatebin')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.ReadBasedLiquidationMain')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ReadBasedLiquidationMain')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.ReadBasedLiquidationMain.ReadBasedLiquidation')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ReadBasedLiquidation')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.ReadBasedLiquidationMain.ReportLiquidationReport')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ReportLiquidationReport')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.SiteInterrogation')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'SiteInterrogation')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CBatchLiquidation')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'Liquidation')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.AttendantPay')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'AttendantPays')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.ManualAttendantPay')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ManualAttendantPays')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.AttendantPay')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'AttendantPays')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.ManualAttendantPay')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ManualAttendantPays')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.Unlock')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'DataUnlock')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.SyncEmpCard')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'SyncEmpCard')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.ExportDetails')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'ExportDetails')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.SpotCheck')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'SpotCheck')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CPerformDrop.PartCollection')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'PartCollection')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CPerformDrop.PartCollectionDrop')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'PartCollectionDrop')


UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CDeclaration.PartCollectionDeclaration')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'PartCollectionDeclaration')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.UpdateGMUNo')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'UpdateGMUNo')
GO

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.CVoidTicket')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'VoidVouchers')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CShortPay.ShortPayApprover')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'AuthorizeShortPay')

--UPDATE roleaccessobjectright_lnk
--SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.CMachineEnableDisable')
--WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'MachineEnableDisable')

--Promotions
UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.Promotions')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'Promotions')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.Promotions.Print')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'PrintPromotionalTicket')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.Promotions.Void')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'VoidPromotionalTicket')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.Promotions.History')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'PromotionalHistory')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.Promotions.TIS')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'TISPromotional')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.GameCapping')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'GameCapping')

GO
---------------------------------------VAULT INTERFACE---------------------------------------
UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CFillVault')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'FillVault')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CFillVault.btnVaultFill')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'VaultFill')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CFillVault.btnVaultBleed')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'VaultBleed')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CFillVault.btnVaultAdjustment')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'VaultAdjustment')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CFillVault.btnVaultDrop')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'VaultDrop')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CFillVault.btnVaultDeclaration')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'VaultDeclaration')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CNGAEnroll')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'NGAEnrollment')
---------------------------------------VAULT INTERFACE---------------------------------------
---------------------------------------GRID VIEW---------------------------------------
UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.Grid.View')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'GridView')
---------------------------------------GRID VIEW---------------------------------------

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CGMUPING')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'GMUPing')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.Presentation.CProfitShare.ProfitShareApprover')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'AuthorizeProfitShare')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'BMC.CPositionDetailsPlayerClub.PlayerClubBonus')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'PlayerClubBonus')

UPDATE roleaccessobjectright_lnk
SET    SecurityObjectID = (SELECT TOP 1 SecurityObjectID FROM OBJECT WHERE ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports.AccountingWinLossReport')
WHERE  RoleAccessID = (SELECT TOP 1 RoleAccessID FROM RoleAccess WHERE RoleAccessName = 'AccountingWinLossReport')

GO
