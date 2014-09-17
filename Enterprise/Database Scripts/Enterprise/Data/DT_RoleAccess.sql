/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 06/04/13 4:10:34 PM
 ************************************************************/

USE Enterprise
GO

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'Exchange'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'Exchange',
           'Main Menu'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'Exchange',
           DESCRIPTION = 'Main Menu'
    WHERE  RoleAccessName = 'Exchange'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'FloorView'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'FloorView',
           'Floor View'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'FloorView',
           DESCRIPTION = 'Floor View'
    WHERE  RoleAccessName = 'FloorView'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'UnlockPositions'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'UnlockPositions',
           'Unlock Positions'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'UnlockPositions',
           DESCRIPTION = 'Unlock Positions'
    WHERE  RoleAccessName = 'UnlockPositions'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'PositionDetails'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'PositionDetails',
           'Position Details'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'PositionDetails',
           DESCRIPTION = 'Position Details'
    WHERE  RoleAccessName = 'PositionDetails'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'MachineInstallation'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'MachineInstallation',
           'Machine Installation'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'MachineInstallation',
           DESCRIPTION = 'Machine Installation'
    WHERE  RoleAccessName = 'MachineInstallation'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'MachineRemoval'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'MachineRemoval',
           'Machine Removal'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'MachineRemoval',
           DESCRIPTION = 'Machine Removal'
    WHERE  RoleAccessName = 'MachineRemoval'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'ReinstateMachine'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'ReinstateMachine',
           'Machine Reinstate'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'ReinstateMachine',
           DESCRIPTION = 'Machine Reinstate'
    WHERE  RoleAccessName = 'ReinstateMachine'

--Only the DESCRIPTION -> Ticket is Changed to Voucher 
IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'TicketExpire'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'TicketExpire',
           'Voucher Expire'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'TicketExpire',
           DESCRIPTION = 'Voucher Expire'
    WHERE  RoleAccessName = 'TicketExpire'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'AttendantPay'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'AttendantPay',
           'AttendantPay'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'AttendantPay',
           DESCRIPTION = 'AttendantPay'
    WHERE  RoleAccessName = 'AttendantPay'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'ManualAttendantPay'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'ManualAttendantPay',
           'Manual AttendantPay'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'ManualAttendantPay',
           DESCRIPTION = 'Manual AttendantPay'
    WHERE  RoleAccessName = 'ManualAttendantPay'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'VOIDManualAttendantPay'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'VOIDManualAttendantPay',
           'Void'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'VOIDManualAttendantPay',
           DESCRIPTION = 'Void'
    WHERE  RoleAccessName = 'VOIDManualAttendantPay'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'MaxHandpay'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'MaxHandpay',
           'MaxAttendantpay'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'MaxHandpay',
           DESCRIPTION = 'MaxAttendantpay'
    WHERE  RoleAccessName = 'MaxHandpay'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'PDPlayerClub'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'PDPlayerClub',
           'Player Club'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'PDPlayerClub',
           DESCRIPTION = 'Player Club'
    WHERE  RoleAccessName = 'PDPlayerClub'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'FieldService'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'FieldService',
           'Field Service'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'FieldService',
           DESCRIPTION = 'Field Service'
    WHERE  RoleAccessName = 'FieldService'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'RequestCall'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'RequestCall',
           'Request Call'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'RequestCall',
           DESCRIPTION = 'Request Call'
    WHERE  RoleAccessName = 'RequestCall'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'ClearCall'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'ClearCall',
           'Clear Call'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'ClearCall',
           DESCRIPTION = 'Clear Call'
    WHERE  RoleAccessName = 'ClearCall'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'ReviewNotes'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'ReviewNotes',
           'Review Notes'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'ReviewNotes',
           DESCRIPTION = 'Review Notes'
    WHERE  RoleAccessName = 'ReviewNotes'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'AddNote'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'AddNote',
           'Add Note'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'AddNote',
           DESCRIPTION = 'Add Note'
    WHERE  RoleAccessName = 'AddNote'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'EscalateCall'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'EscalateCall',
           'Escalate Call'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'EscalateCall',
           DESCRIPTION = 'Escalate Call'
    WHERE  RoleAccessName = 'EscalateCall'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'Events'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'Events',
           'Events'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'Events',
           DESCRIPTION = 'Events'
    WHERE  RoleAccessName = 'Events'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'ClearEvents'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'ClearEvents',
           'Clear Events'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'ClearEvents',
           DESCRIPTION = 'Clear Events'
    WHERE  RoleAccessName = 'ClearEvents'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'MachineMeters'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'MachineMeters',
           'Machine Meters'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'MachineMeters',
           DESCRIPTION = 'Machine Meters'
    WHERE  RoleAccessName = 'MachineMeters'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'CurrentMeters'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'CurrentMeters',
           'Current Meters'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'CurrentMeters',
           DESCRIPTION = 'Current Meters'
    WHERE  RoleAccessName = 'CurrentMeters'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'MachineMaintenance'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'MachineMaintenance',
           'Machine Maintenance'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'MachineMaintenance',
           DESCRIPTION = 'Machine Maintenance'
    WHERE  RoleAccessName = 'MachineMaintenance'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'OverrideEvents'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'OverrideEvents',
           'Override Events'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'OverrideEvents',
           DESCRIPTION = 'Override Events'
    WHERE  RoleAccessName = 'OverrideEvents'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'Details'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'Details',
           'Details'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'Details',
           DESCRIPTION = 'Details'
    WHERE  RoleAccessName = 'Details'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'Vouchers'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'Vouchers',
           'Cashier Transactions'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'Vouchers',
           DESCRIPTION = 'Cashier Transactions'
    WHERE  RoleAccessName = 'Vouchers'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'PrintVoucher'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'PrintVoucher',
           'Print Voucher'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'PrintVoucher',
           DESCRIPTION = 'Print Voucher'
    WHERE  RoleAccessName = 'PrintVoucher'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'RedeemVoucher'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'RedeemVoucher',
           'Redeem Voucher'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'RedeemVoucher',
           DESCRIPTION = 'Redeem Voucher'
    WHERE  RoleAccessName = 'RedeemVoucher'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'RedeemExpiredVoucher'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'RedeemExpiredVoucher',
           'Redeem Expired Voucher'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'RedeemExpiredVoucher',
           DESCRIPTION = 'Redeem Expired Voucher'
    WHERE  RoleAccessName = 'RedeemExpiredVoucher'
    
IF NOT EXISTS(
	SELECT 1 
	FROM RoleAccess 
	WHERE RoleAccessName = 'MultipleVoucher' 
)
 INSERT RoleAccess 
 (
 	RoleAccessName ,
 	Description  	
 )
 SELECT 'MultipleVoucher',
		'Multiple Voucher'
		
ELSE
	UPDATE RoleAccess
    SET    RoleAccessName = 'MultipleVoucher',
           DESCRIPTION = 'Multiple Voucher'
    WHERE  RoleAccessName = 'MultipleVoucher'
    
    
    IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'MultipleRedeemExpiredVoucher'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'MultipleRedeemExpiredVoucher',
           'Multiple Redeem Expired Voucher'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'MultipleRedeemExpiredVoucher',
           DESCRIPTION = 'Multiple Redeem Expired Voucher'
    WHERE  RoleAccessName = 'MultipleRedeemExpiredVoucher'
    
    

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'VoidTransactions'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'VoidTransactions',
           'Void Transactions'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'VoidTransactions',
           DESCRIPTION = 'Void Transactions'
    WHERE  RoleAccessName = 'VoidTransactions'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'Void'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'Void',
           'Void'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'Void',
           DESCRIPTION = 'Void'
    WHERE  RoleAccessName = 'Void'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'Reports'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'Reports',
           'Reports'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'Reports',
           DESCRIPTION = 'Reports'
    WHERE  RoleAccessName = 'Reports'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'Hourly'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'Hourly',
           'Hourly'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'Hourly',
           DESCRIPTION = 'Hourly'
    WHERE  RoleAccessName = 'Hourly'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'PlayerClub'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'PlayerClub',
           'Player Club'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'PlayerClub',
           DESCRIPTION = 'Player Club'
    WHERE  RoleAccessName = 'PlayerClub'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'CurrentCalls'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'CurrentCalls',
           'Current Calls'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'CurrentCalls',
           DESCRIPTION = 'Current Calls'
    WHERE  RoleAccessName = 'CurrentCalls'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'MachineDrop'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'MachineDrop',
           'Machine Drop'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'MachineDrop',
           DESCRIPTION = 'Machine Drop'
    WHERE  RoleAccessName = 'MachineDrop'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'PerformDrop'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'PerformDrop',
           'Perform Drop'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'PerformDrop',
           DESCRIPTION = 'Perform Drop'
    WHERE  RoleAccessName = 'PerformDrop'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'PerformDeclaration'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'PerformDeclaration',
           'Perform Declaration'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'PerformDeclaration',
           DESCRIPTION = 'Perform Declaration'
    WHERE  RoleAccessName = 'PerformDeclaration'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'ExportBatch'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'ExportBatch',
           'Export Batch'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'ExportBatch',
           DESCRIPTION = 'Export Batch'
    WHERE  RoleAccessName = 'ExportBatch'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'CashierTransactions'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'CashierTransactions',
           'Cashier History'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'CashierTransactions',
           DESCRIPTION = 'Cashier History'
    WHERE  RoleAccessName = 'CashierTransactions'
    
--Only the RoleAccess Name Ticket is Changed to Voucher 

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'DisplayTicketNumber'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'DisplayTicketNumber',
           'Display Voucher Number'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'DisplayTicketNumber',
           DESCRIPTION = 'Display Voucher Number'
    WHERE  RoleAccessName = 'DisplayTicketNumber'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'SystemAudit'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'SystemAudit',
           'System Audit'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'SystemAudit',
           DESCRIPTION = 'System Audit'
    WHERE  RoleAccessName = 'SystemAudit'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'CustomReports '
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'CustomReports ',
           'Custom Reports'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'CustomReports ',
           DESCRIPTION = 'Custom Reports'
    WHERE  RoleAccessName = 'CustomReports '

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'ExpenseDetailReport'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'ExpenseDetailReport',
           'Expense Detail Report'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'ExpenseDetailReport',
           DESCRIPTION = 'Expense Detail Report'
    WHERE  RoleAccessName = 'ExpenseDetailReport'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'ExpiredVoucherCouponReport'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'ExpiredVoucherCouponReport',
           'Expired Voucher/Coupon Report'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'ExpiredVoucherCouponReport',
           DESCRIPTION = 'Expired Voucher/Coupon Report'
    WHERE  RoleAccessName = 'ExpiredVoucherCouponReport'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'JackpotSlipSummaryReport'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'JackpotSlipSummaryReport',
           'Jackpot Slip Summary Report'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'JackpotSlipSummaryReport',
           DESCRIPTION = 'Jackpot Slip Summary Report'
    WHERE  RoleAccessName = 'JackpotSlipSummaryReport'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'MeterListReport'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'MeterListReport',
           'Meter List Report'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'MeterListReport',
           DESCRIPTION = 'Meter List Report'
    WHERE  RoleAccessName = 'MeterListReport'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'RedeemedTicketByDeviceReport'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'RedeemedTicketByDeviceReport',
           'Redeemed Voucher by Device Report'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'RedeemedTicketByDeviceReport',
           DESCRIPTION = 'Redeemed Voucher by Device Report'
    WHERE  RoleAccessName = 'RedeemedTicketByDeviceReport'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'TicketIssuedReport'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'TicketIssuedReport',
           'Voucher Listing Report'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'TicketIssuedReport',
           DESCRIPTION = 'Voucher Listing Report'
    WHERE  RoleAccessName = 'TicketIssuedReport'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'VoucherCouponLiabilityReport'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'VoucherCouponLiabilityReport',
           'Voucher/Coupon Liability Report'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'VoucherCouponLiabilityReport',
           DESCRIPTION = 'Voucher/Coupon Liability Report'
    WHERE  RoleAccessName = 'VoucherCouponLiabilityReport'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'ExceptionVoucherDetails'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'ExceptionVoucherDetails',
           'Exception Voucher Details Report'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'ExceptionVoucherDetails',
           DESCRIPTION = 'Exception Voucher Details Report'
    WHERE  RoleAccessName = 'ExceptionVoucherDetails'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'CrossPropertyLiabilityTransferSummaryReport'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'CrossPropertyLiabilityTransferSummaryReport',
           'Cross Property Liability Transfer Summary Report'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'CrossPropertyLiabilityTransferSummaryReport',
           DESCRIPTION = 'Cross Property Liability Transfer Summary Report'
    WHERE  RoleAccessName = 'CrossPropertyLiabilityTransferSummaryReport'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'CrossPropertyLiabilityTransferDetailsReport'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'CrossPropertyLiabilityTransferDetailsReport',
           'Cross Property Liability Transfer Details Report'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'CrossPropertyLiabilityTransferDetailsReport',
           DESCRIPTION = 'Cross Property Liability Transfer Details Report'
    WHERE  RoleAccessName = 'CrossPropertyLiabilityTransferDetailsReport'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'CrossPropertyTicketAnalysisReport'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'CrossPropertyTicketAnalysisReport',
           'Cross Property Ticket Analysis Report'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'CrossPropertyTicketAnalysisReport',
           DESCRIPTION = 'Cross Property Ticket Analysis Report'
    WHERE  RoleAccessName = 'CrossPropertyTicketAnalysisReport'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'StackerLevelDetailsReport'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'StackerLevelDetailsReport',
           'Stacker Level Details Report'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'StackerLevelDetailsReport',
           DESCRIPTION = 'Stacker Level Details Report'
    WHERE  RoleAccessName = 'StackerLevelDetailsReport'
    
IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'AccountingWinLossReport'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'AccountingWinLossReport',
           'Accounting Win/Loss Report'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'AccountingWinLossReport',
           DESCRIPTION = 'Accounting Win/Loss Report'
    WHERE  RoleAccessName = 'AccountingWinLossReport'    

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'SiteSettings'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'SiteSettings',
           'Site Settings'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'SiteSettings',
           DESCRIPTION = 'Site Settings'
    WHERE  RoleAccessName = 'SiteSettings'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'ConfigParameters'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'ConfigParameters',
           'Config Parameters'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'ConfigParameters',
           DESCRIPTION = 'Config Parameters'
    WHERE  RoleAccessName = 'ConfigParameters'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'FactoryReset'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'FactoryReset',
           'Factory Reset'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'FactoryReset',
           DESCRIPTION = 'Factory Reset'
    WHERE  RoleAccessName = 'FactoryReset'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'AFTSettings'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'AFTSettings',
           'AFTSettings'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'AFTSettings',
           DESCRIPTION = 'AFTSettings'
    WHERE  RoleAccessName = 'AFTSettings'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'BMCExchangeConfig'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'BMCExchangeConfig',
           'BMC Exchange Configuration'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'BMCExchangeConfig',
           DESCRIPTION = 'BMC Exchange Configuration'
    WHERE  RoleAccessName = 'BMCExchangeConfig'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'AFTEnableDisable'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'AFTEnableDisable',
           'AFTEnableDisable'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'AFTEnableDisable',
           DESCRIPTION = 'AFTEnableDisable'
    WHERE  RoleAccessName = 'AFTEnableDisable'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'CoinDispenser'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'CoinDispenser',
           'CoinDispenser'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'CoinDispenser',
           DESCRIPTION = 'CoinDispenser'
    WHERE  RoleAccessName = 'CoinDispenser'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'ExceptionVoucher'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'ExceptionVoucher',
           'Exception vouchers'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'ExceptionVoucher',
           DESCRIPTION = 'Exception vouchers'
    WHERE  RoleAccessName = 'ExceptionVoucher'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'DisplayTransactions'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION,
        IsEnabled
      )
    SELECT 'DisplayTransactions',
           'Display Transactions',
           'N'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'DisplayTransactions',
           DESCRIPTION = 'Display Transactions',
           IsEnabled = 'N'
    WHERE  RoleAccessName = 'DisplayTransactions'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'CrossTicketingSettings'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'CrossTicketingSettings',
           'CrossTicketingSettings'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'CrossTicketingSettings',
           DESCRIPTION = 'CrossTicketingSettings'
    WHERE  RoleAccessName = 'CrossTicketingSettings'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'Shortpay'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'Shortpay',
           'Shortpay'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'Shortpay',
           DESCRIPTION = 'Shortpay'
    WHERE  RoleAccessName = 'Shortpay'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'CommonCDOforDeclaration'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'CommonCDOforDeclaration',
           'Common CDO for Declaration'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'CommonCDOforDeclaration',
           DESCRIPTION = 'Common CDO for Declaration'
    WHERE  RoleAccessName = 'CommonCDOforDeclaration'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'AccessOtherUsers'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'AccessOtherUsers',
           'Access Other Users'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'AccessOtherUsers',
           DESCRIPTION = 'Access Other Users'
    WHERE  RoleAccessName = 'AccessOtherUsers'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'VoucherConfig'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'VoucherConfig',
           'Voucher Config'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'VoucherConfig',
           DESCRIPTION = 'Voucher Config'
    WHERE  RoleAccessName = 'VoucherConfig'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'AttendantPays'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'AttendantPays',
           'AttendantPays'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'AttendantPays',
           DESCRIPTION = 'AttendantPays'
    WHERE  RoleAccessName = 'AttendantPays'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'ManualAttendantPays'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'ManualAttendantPays',
           'ManualAttendantPays'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'ManualAttendantPays',
           DESCRIPTION = 'ManualAttendantPays'
    WHERE  RoleAccessName = 'ManualAttendantPays'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'AttendantPayVoid'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'AttendantPayVoid',
           'AttendantPayVoid'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'AttendantPayVoid',
           DESCRIPTION = 'AttendantPayVoid'
    WHERE  RoleAccessName = 'AttendantPayVoid'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'FillVault'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'FillVault',
           'Vault'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'FillVault',
           DESCRIPTION = 'Vault'
    WHERE  RoleAccessName = 'FillVault'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'VaultFill'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'VaultFill',
           'Vault Fill'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'VaultFill',
           DESCRIPTION = 'Vault Fill'
    WHERE  RoleAccessName = 'VaultFill'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'VaultBleed'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'VaultBleed',
           'Vault Bleed'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'VaultBleed',
           DESCRIPTION = 'Vault Bleed'
    WHERE  RoleAccessName = 'VaultBleed'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'VaultAdjustment'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'VaultAdjustment',
           'Vault Adjustment'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'VaultAdjustment',
           DESCRIPTION = 'Vault Adjustment'
    WHERE  RoleAccessName = 'VaultAdjustment'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'VaultDrop'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'VaultDrop',
           'Vault Drop'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'VaultDrop',
           DESCRIPTION = 'Vault Drop'
    WHERE  RoleAccessName = 'VaultDrop'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'VaultDeclaration'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'VaultDeclaration',
           'Vault Declaration'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'VaultDeclaration',
           DESCRIPTION = 'Vault Declaration'
    WHERE  RoleAccessName = 'VaultDeclaration'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'PROCESSManualAttendantPay'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'PROCESSManualAttendantPay',
           'Process'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'PROCESSManualAttendantPay',
           DESCRIPTION = 'Process'
    WHERE  RoleAccessName = 'PROCESSManualAttendantPay'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'VoidVouchers'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'VoidVouchers',
           'VoidVouchers'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'VoidVouchers',
           DESCRIPTION = 'VoidVouchers'
    WHERE  RoleAccessName = 'VoidVouchers'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'Promotions'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'Promotions',
           'Promotions'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'Promotions',
           DESCRIPTION = 'Promotions'
    WHERE  RoleAccessName = 'Promotions'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'PrintPromotionalTicket'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'PrintPromotionalTicket',
           'Print Promotional Voucher'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'PrintPromotionalTicket',
           DESCRIPTION = 'Print Promotional Voucher'
    WHERE  RoleAccessName = 'PrintPromotionalTicket'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'VoidPromotionalTicket'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'VoidPromotionalTicket',
           'Void Promotional Voucher'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'VoidPromotionalTicket',
           DESCRIPTION = 'Void Promotional Voucher'
    WHERE  RoleAccessName = 'VoidPromotionalTicket'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'PromotionalHistory'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'PromotionalHistory',
           'Promotional History'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'PromotionalHistory',
           DESCRIPTION = 'Promotional History'
    WHERE  RoleAccessName = 'PromotionalHistory'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'TISPromotional'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'TISPromotional',
           'TIS Promotional Details'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'TISPromotional',
           DESCRIPTION = 'TIS Promotional Details'
    WHERE  RoleAccessName = 'TISPromotional'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'ActivateLicense'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'ActivateLicense',
           'Activate License'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'ActivateLicense',
           DESCRIPTION = 'Activate License'
    WHERE  RoleAccessName = 'ActivateLicense'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'Gmuupdatebin'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'Gmuupdatebin',
           'Gmuupdatebin'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'Gmuupdatebin',
           DESCRIPTION = 'Gmuupdatebin'
    WHERE  RoleAccessName = 'Gmuupdatebin'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'ReadBasedLiquidationMain'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'ReadBasedLiquidationMain',
           'Read Based Liquidation Main'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'ReadBasedLiquidationMain',
           DESCRIPTION = 'Read Based Liquidation Main'
    WHERE  RoleAccessName = 'ReadBasedLiquidationMain'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'ReadBasedLiquidation'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'ReadBasedLiquidation',
           'Read Based Liquidation'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'ReadBasedLiquidation',
           DESCRIPTION = 'Read Based Liquidation'
    WHERE  RoleAccessName = 'ReadBasedLiquidation'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'ReportLiquidationReport'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'ReportLiquidationReport',
           'Read Liquidation Report'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'ReportLiquidationReport',
           DESCRIPTION = 'Read Liquidation Report'
    WHERE  RoleAccessName = 'ReportLiquidationReport'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'SiteInterrogation'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'SiteInterrogation',
           'SiteInterrogation'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'SiteInterrogation',
           DESCRIPTION = 'SiteInterrogation'
    WHERE  RoleAccessName = 'SiteInterrogation'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'Liquidation'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'Liquidation',
           'Liquidation'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'Liquidation',
           DESCRIPTION = 'Liquidation'
    WHERE  RoleAccessName = 'Liquidation'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'DataUnlock'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'DataUnlock',
           'DataUnlock'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'DataUnlock',
           DESCRIPTION = 'DataUnlock'
    WHERE  RoleAccessName = 'DataUnlock'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'SyncEmpcard'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'SyncEmpcard',
           'SyncEmpcard'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'SyncEmpcard',
           DESCRIPTION = 'SyncEmpcard'
    WHERE  RoleAccessName = 'SyncEmpcard'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'ExportDetails'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'ExportDetails',
           'ExportDetails'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'ExportDetails',
           DESCRIPTION = 'ExportDetails'
    WHERE  RoleAccessName = 'ExportDetails'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'SpotCheck'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'SpotCheck',
           'Spot Check'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'SpotCheck',
           DESCRIPTION = 'Spot Check'
    WHERE  RoleAccessName = 'SpotCheck'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'PartCollection'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'PartCollection',
           'Part Collection'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'PartCollection',
           DESCRIPTION = 'Part Collection'
    WHERE  RoleAccessName = 'PartCollection'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'PartCollectionDrop'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'PartCollectionDrop',
           'Part-Collection Drop'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'PartCollectionDrop',
           DESCRIPTION = 'Part-Collection Drop'
    WHERE  RoleAccessName = 'PartCollectionDrop'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'PartCollectionDeclaration'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'PartCollectionDeclaration',
           'Part-Collection Declaration'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'PartCollectionDeclaration',
           DESCRIPTION = 'Part-Collection Declaration'
    WHERE  RoleAccessName = 'PartCollectionDeclaration'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'UpdateGMUNo'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'UpdateGMUNo',
           'Update GMU No'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'UpdateGMUNo',
           DESCRIPTION = 'Update GMU No'
    WHERE  RoleAccessName = 'UpdateGMUNo'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'AuthorizeShortPay'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'AuthorizeShortPay',
           'Secondary Shortpay Approver'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'AuthorizeShortPay',
           DESCRIPTION = 'Secondary Shortpay Approver'
    WHERE  RoleAccessName = 'AuthorizeShortPay'   


IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'GameCapping'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'GameCapping',
           'Game Capping'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'GameCapping',
           DESCRIPTION = 'Game Capping'
    WHERE  RoleAccessName = 'GameCapping'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'NGAEnrollment'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'NGAEnrollment',
           'NGA Enrollment'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'NGAEnrollment',
           DESCRIPTION = 'NGA Enrollment'
    WHERE  RoleAccessName = 'NGAEnrollment'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'GridView'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'GridView',
           'Grid View'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'GridView',
           DESCRIPTION = 'Grid View'
    WHERE  RoleAccessName = 'GridView'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'GMUPing'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'GMUPing',
           'GMU Ping'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'GMUPing',
           DESCRIPTION = 'GMU Ping'
    WHERE  RoleAccessName = 'GMUPing'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'AuthorizeProfitShare'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'AuthorizeProfitShare',
           'Secondary ProfitShare Approver'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'AuthorizeProfitShare',
           DESCRIPTION = 'Secondary ProfitShare Approver'
    WHERE  RoleAccessName = 'AuthorizeProfitShare'

IF NOT EXISTS(
       SELECT 1
       FROM   RoleAccess
       WHERE  RoleAccessName = 'PlayerClubBonus'
   )
    INSERT RoleAccess
      (
        RoleAccessName,
        DESCRIPTION
      )
    SELECT 'PlayerClubBonus',
           'Show Bonus in Player Club'
ELSE
    UPDATE RoleAccess
    SET    RoleAccessName = 'PlayerClubBonus',
           DESCRIPTION = 'Show Bonus in Player Club'
    WHERE  RoleAccessName = 'PlayerClubBonus'


DECLARE @RoleAccessID INT
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'Exchange'
UPDATE RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('FloorView, Details, Vouchers, VoidTransactions, Reports, Hourly, PlayerClub, CurrentCalls, MachineDrop, CashierTransactions, SystemAudit, CustomReports, SiteSettings, FactoryReset, AFTSettings, AFTEnableDisable, CoinDispenser, CrossTicketingSettings, ShortPay, VoucherConfig, ActivateLicense, Gmuupdatebin, SiteInterrogation,DataUnlock,SyncEmpcard,ExportDetails,SpotCheck,UpdateGMUNo,Promotions,GameCapping,AuthorizeShortPay,FillVault,NGAEnrollment,GridView,GMUPing,AuthorizeProfitShare', ','))

SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'FloorView'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('UnlockPositions, PositionDetails, ExceptionVoucher', ','))

SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'PositionDetails'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('MachineInstallation, MachineRemoval, ReinstateMachine, TicketExpire, AttendantPay, PDPlayerClub, FieldService, Events, MachineMeters, CurrentMeters, MachineMaintenance', ','))

SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'AttendantPay'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('ManualAttendantPay, VOIDManualAttendantPay, MaxHandpay,PROCESSManualAttendantPay', ','))

SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'FieldService'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('RequestCall, ClearCall, ReviewNotes, EscalateCall', ','))

SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'ReviewNotes'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('AddNote', ','))

SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'Events'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('ClearEvents', ','))


SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'MachineMaintenance'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('OverrideEvents', ','))

SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'Vouchers'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('PrintVoucher, VoidVouchers,RedeemVoucher,MultipleVoucher,AttendantPays, ManualAttendantPays, AttendantPayVoid', ','))

SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'RedeemVoucher'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('RedeemExpiredVoucher', ','))
                                 
SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'MultipleVoucher'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('MultipleRedeemExpiredVoucher', ','))

SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'VoidTransactions'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('Void', ','))

SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'MachineDrop'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('PerformDrop, PerformDeclaration, ExportBatch, CommonCDOforDeclaration, Liquidation, PartCollection', ','))

SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'CashierTransactions'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('DisplayTicketNumber, DisplayTransactions, AccessOtherUsers', ','))

SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'SiteSettings'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('ConfigParameters', ','))

SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'CustomReports'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('ExpenseDetailReport, ExpiredVoucherCouponReport, JackpotSlipSummaryReport, MeterListReport, RedeemedTicketByDeviceReport, TicketIssuedReport, VoucherCouponLiabilityReport, ExceptionVoucherDetails, CrossPropertyLiabilityTransferSummaryReport, CrossPropertyLiabilityTransferDetailsReport, CrossPropertyTicketAnalysisReport, StackerLevelDetailsReport, AccountingWinLossReport', ','))


SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'PartCollection'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('PartCollectionDrop, PartCollectionDeclaration', ','))
                                 
SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'ReadBasedLiquidationMain'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('ReadBasedLiquidation, ReportLiquidationReport', ','))                                 

                             
  --Promotions
SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'Promotions'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('PrintPromotionalTicket, VoidPromotionalTicket,PromotionalHistory,TISPromotional', ','))
                                 
SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'PDPlayerClub'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('PlayerClubBonus', ','))

-------------------------------------ShortPay--------------------------------------------------------
SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM dbo.RoleAccess WHERE RoleAccessName = 'Shortpay'
UPDATE dbo.RoleAccess
SET    ParentID = @RoleAccessID
WHERE  RoleAccessName ='AuthorizeShortPay'
                                 
-------------------------------------ShortPay End-----------------------------------------------------

-------------------------------------Vault-------------------------------------
SET @RoleAccessID = 0
SELECT @RoleAccessID = RoleAccessID FROM   dbo.RoleAccess WHERE  RoleAccessName = 'FillVault'
UPDATE dbo.RoleAccess 
SET ParentID = @RoleAccessID
,IsEnabled = 'Y'
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE))
FROM   dbo.[UDF_GetStringTable]('VaultFill,VaultBleed,VaultAdjustment,VaultDrop,VaultDeclaration',','))
GO

UPDATE dbo.RoleAccess 
SET IsEnabled = 'Y'
WHERE  RoleAccessName IN ('FillVault','NGAEnrollment')

-------------------------------------Vault End-------------------------------------

-------------------------------------Cross Ticketing Reports-------------------------------------
UPDATE dbo.RoleAccess 
SET IsEnabled = 'N'
WHERE  RoleAccessName IN ('CrossPropertyLiabilityTransferSummaryReport','CrossPropertyLiabilityTransferDetailsReport','CrossPropertyTicketAnalysisReport')
-------------------------------------Cross Ticketing Reports End-------------------------------------
UPDATE dbo.RoleAccess 
SET IsEnabled = 'N'
WHERE  RoleAccessName IN ('CurrentCalls', 'FieldService')
GO


UPDATE dbo.RoleAccess
SET    IsEnabled = 'N'
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('RequestCall, ClearCall, ReviewNotes, EscalateCall', ','))
GO

UPDATE dbo.RoleAccess
SET    IsEnabled = 'N'
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('AddNote', ','))
GO

-------------------------------- TO ENABLE Service Call Features  Start----------------------------------------------
UPDATE dbo.RoleAccess 
SET IsEnabled = 'Y'
WHERE  RoleAccessName IN ( 'CurrentCalls', 'FieldService')
GO


UPDATE dbo.RoleAccess
SET    IsEnabled = 'Y'
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('RequestCall, ClearCall, ReviewNotes, EscalateCall', ','))
GO

UPDATE dbo.RoleAccess
SET    IsEnabled = 'Y'
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('AddNote', ','))
GO

-------------------------------- TO ENABLE Service Call Features END ----------------------------------------------

-------------------------------- TO DISABLE Service Call Features  Start----------------------------------------------
UPDATE dbo.RoleAccess
SET    IsEnabled = 'N'
WHERE  RoleAccessName IN (SELECT LTRIM(RTRIM(VALUE )) FROM dbo.[UDF_GetStringTable]
                                 ('EscalateCall', ','))
GO
-------------------------------- TO DISABLE Service Call Features  END----------------------------------------------