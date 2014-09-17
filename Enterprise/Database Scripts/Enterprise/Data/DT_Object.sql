/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 08/04/13 3:54:23 PM
 ************************************************************/

USE [Enterprise]
GO

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.MainScreen'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      ) 
    SELECT 'Main Menu',
           'BMC.Presentation.MainScreen',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Main Menu',
           ObjectName = 'BMC.Presentation.MainScreen',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.MainScreen'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Floor.View'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Floor View from Main Screen',
           'CashdeskOperator.MainScreen.cs.Floor.View',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Floor View from Main Screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.Floor.View',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Floor.View'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'CashdeskOperator.MainScreen.cs.Floor.View.UnLockPosition'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Unlock Position Check in Floor View',
           'CashdeskOperator.MainScreen.cs.Floor.View.UnLockPosition',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Unlock Position Check in Floor View',
           ObjectName = 
           'CashdeskOperator.MainScreen.cs.Floor.View.UnLockPosition',
           ObjectType = 1
    WHERE  ObjectName = 
           'CashdeskOperator.MainScreen.cs.Floor.View.UnLockPosition'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.PosDetails'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Access to Position Details Screen',
           'BMC.Presentation.PosDetails',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Access to Position Details Screen',
           ObjectName = 'BMC.Presentation.PosDetails',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.PosDetails'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.InstallMachine'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Install Machine from Position Details Screen',
           'CashdeskOperator.FloorView.cs.InstallMachine',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Install Machine from Position Details Screen',
           ObjectName = 'CashdeskOperator.FloorView.cs.InstallMachine',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.InstallMachine'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.RemoveMachine'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Remove Machine from Position Details Screen',
           'CashdeskOperator.FloorView.cs.RemoveMachine',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Remove Machine from Position Details Screen',
           ObjectName = 'CashdeskOperator.FloorView.cs.RemoveMachine',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.RemoveMachine'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.ReinstateMachine'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Reinstate Machine from Position Details Screen',
           'CashdeskOperator.FloorView.cs.ReinstateMachine',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Reinstate Machine from Position Details Screen',
           ObjectName = 'CashdeskOperator.FloorView.cs.ReinstateMachine',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.ReinstateMachine'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.SyncTicketExpire'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Sync Ticket Expire with GMU Position Details Screen',
           'CashdeskOperator.FloorView.cs.SyncTicketExpire',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Sync Ticket Expire with GMU Position Details Screen',
           ObjectName = 'CashdeskOperator.FloorView.cs.SyncTicketExpire',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.SyncTicketExpire'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.Handpay'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Handpay from FloorView Screen',
           'CashdeskOperator.FloorView.cs.Handpay',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Handpay from FloorView Screen',
           ObjectName = 'CashdeskOperator.FloorView.cs.Handpay',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.Handpay'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CViewHandpay.btnManual'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Manual AttendantPay button on AttendantPay Screen',
           'BMC.Presentation.CViewHandpay.btnManual',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Manual AttendantPay button on AttendantPay Screen',
           ObjectName = 'BMC.Presentation.CViewHandpay.btnManual',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CViewHandpay.btnManual'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CViewHandpay.btnVoid'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Void Button on AttendantPay Screen',
           'BMC.Presentation.CViewHandpay.btnVoid',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Void Button on AttendantPay Screen',
           ObjectName = 'BMC.Presentation.CViewHandpay.btnVoid',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CViewHandpay.btnVoid'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.Authorize.cs.MaxHandpay'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Maximum Handpay right to user',
           'CashdeskOperator.Authorize.cs.MaxHandpay',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Maximum Handpay right to user',
           ObjectName = 'CashdeskOperator.Authorize.cs.MaxHandpay',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.Authorize.cs.MaxHandpay'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.PlayerClub'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Player Club from Position Details Screen',
           'CashdeskOperator.FloorView.cs.PlayerClub',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Player Club from Position Details Screen',
           ObjectName = 'CashdeskOperator.FloorView.cs.PlayerClub',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.PlayerClub'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.FieldService'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Field Service from Position Details Screen',
           'CashdeskOperator.FloorView.cs.FieldService',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Field Service from Position Details Screen',
           ObjectName = 'CashdeskOperator.FloorView.cs.FieldService',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.FieldService'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CFieldService.btnRequestCall'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Request Call button from Field Service Screen',
           'BMC.Presentation.CFieldService.btnRequestCall',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Request Call button from Field Service Screen',
           ObjectName = 'BMC.Presentation.CFieldService.btnRequestCall',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CFieldService.btnRequestCall'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CFieldService.btnClear'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Clear Call button from Field Service Screen',
           'BMC.Presentation.CFieldService.btnClear',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Clear Call button from Field Service Screen',
           ObjectName = 'BMC.Presentation.CFieldService.btnClear',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CFieldService.btnClear'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CFieldService.btnReview'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Review Notes from Field Service Screen',
           'BMC.Presentation.CFieldService.btnReview',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Review Notes from Field Service Screen',
           ObjectName = 'BMC.Presentation.CFieldService.btnReview',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CFieldService.btnReview'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CFieldService.btnReview.AddNote'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Add Note in Review Notes from Field Service Screen',
           'BMC.Presentation.CFieldService.btnReview.AddNote',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Add Note in Review Notes from Field Service Screen',
           ObjectName = 'BMC.Presentation.CFieldService.btnReview.AddNote',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CFieldService.btnReview.AddNote'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CFieldService.btnEscalate'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Escalate Call button from Field Service Screen',
           'BMC.Presentation.CFieldService.btnEscalate',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Escalate Call button from Field Service Screen',
           ObjectName = 'BMC.Presentation.CFieldService.btnEscalate',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CFieldService.btnEscalate'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.Events'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Events from Position Details screen',
           'CashdeskOperator.FloorView.cs.Events',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Events from Position Details screen',
           ObjectName = 'CashdeskOperator.FloorView.cs.Events',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.Events'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.Events.ClearEvents'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Clear Events in Events from Position Details screen',
           'CashdeskOperator.FloorView.cs.Events.ClearEvents',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Clear Events in Events from Position Details screen',
           ObjectName = 'CashdeskOperator.FloorView.cs.Events.ClearEvents',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.Events.ClearEvents'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.MachineMeters'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Machine Meters from Position Details Screen',
           'CashdeskOperator.FloorView.cs.MachineMeters',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Machine Meters from Position Details Screen',
           ObjectName = 'CashdeskOperator.FloorView.cs.MachineMeters',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.MachineMeters'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.CurrentMeters'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Current Meters from Position Details Screen',
           'CashdeskOperator.FloorView.cs.CurrentMeters',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Current Meters from Position Details Screen',
           ObjectName = 'CashdeskOperator.FloorView.cs.CurrentMeters',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.FloorView.cs.CurrentMeters'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.Authorize.cs.MachineMaintenance'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Machine Maintenance from Position Details Screen',
           'CashdeskOperator.Authorize.cs.MachineMaintenance',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Machine Maintenance from Position Details Screen',
           ObjectName = 'CashdeskOperator.Authorize.cs.MachineMaintenance',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.Authorize.cs.MachineMaintenance'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.Authorize.cs.OverrideEvents'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Override events in Machine Maintenance from Position Details Screen',
           'CashdeskOperator.Authorize.cs.OverrideEvents',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 
           'Override events in Machine Maintenance from Position Details Screen',
           ObjectName = 'CashdeskOperator.Authorize.cs.OverrideEvents',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.Authorize.cs.OverrideEvents'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.POS.Views.InstallationDetails'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Installation Details from Main Screen',
           'BMC.Presentation.POS.Views.InstallationDetails',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Installation Details from Main Screen',
           ObjectName = 'BMC.Presentation.POS.Views.InstallationDetails',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.POS.Views.InstallationDetails'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Tickets'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Vouchers from Main Screen',
           'CashdeskOperator.MainScreen.cs.Tickets',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Vouchers from Main Screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.Tickets',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Tickets'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CPrintTicket'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Print Voucher',
           'BMC.Presentation.CPrintTicket',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Print Voucher',
           ObjectName = 'BMC.Presentation.CPrintTicket',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CPrintTicket'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CRedeemTicket'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Redeem Voucher',
           'BMC.Presentation.CRedeemTicket',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Redeem Voucher',
           ObjectName = 'BMC.Presentation.CRedeemTicket',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CRedeemTicket'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.Authorize.cs.ReedemExpiredTicket'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Redeem Expired Voucher',
           'CashdeskOperator.Authorize.cs.ReedemExpiredTicket',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Redeem Expired Voucher',
           ObjectName = 'CashdeskOperator.Authorize.cs.ReedemExpiredTicket',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.Authorize.cs.ReedemExpiredTicket'
    
IF NOT EXISTS( 
	SELECT 1 
	FROM Object 
	WHERE ObjectName = 'BMC.Presentation.MultipleVoucher'
)
INSERT OBJECT 
(
	DESCRIPTION,
	ObjectName,
	ObjectType
)
	SELECT 'Multiple voucher redemption from Main Screen',
			'BMC.Presentation.MultipleVoucher',
			1
ELSE
	UPDATE OBJECT 
	SET DESCRIPTION = 'Multiple voucher redemption from Main Screen',
		ObjectName = 'BMC.Presentation.MultipleVoucher',	
		ObjectType = 1
	WHERE ObjectName = 'BMC.Presentation.MultipleVoucher'
	


IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.Authorize.cs.MultipleRedeemExpiredTicket'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Multiple Redeem Expired Voucher',
           'CashdeskOperator.Authorize.cs.MultipleRedeemExpiredTicket',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Multiple Redeem Expired Voucher',
           ObjectName = 'CashdeskOperator.Authorize.cs.MultipleRedeemExpiredTicket',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.Authorize.cs.MultipleRedeemExpiredTicket'	
	

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Void'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Void Transactions from Main Screen',
           'CashdeskOperator.MainScreen.cs.Void',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Void Transactions from Main Screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.Void',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Void'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.Void.btnVOID'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Void Button in Void Transactions from Main Screen',
           'BMC.Presentation.Void.btnVOID',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Void Button in Void Transactions from Main Screen',
           ObjectName = 'BMC.Presentation.Void.btnVOID',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.Void.btnVOID'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Reports'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Reports from Main Screen',
           'CashdeskOperator.MainScreen.cs.Reports',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Reports from Main Screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.Reports',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Reports'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Hourly'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Hourly from Main Screen',
           'CashdeskOperator.MainScreen.cs.Hourly',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Hourly from Main Screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.Hourly',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Hourly'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Player.Club'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Player Club from Main Screen',
           'CashdeskOperator.MainScreen.cs.Player.Club',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Player Club from Main Screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.Player.Club',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Player.Club'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Current.Calls'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Current Calls from Main Screen',
           'CashdeskOperator.MainScreen.cs.Current.Calls',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Current Calls from Main Screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.Current.Calls',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Current.Calls'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Machine Drop'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'M/C Drop from Main Screen',
           'CashdeskOperator.MainScreen.cs.Machine Drop',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'M/C Drop from Main Screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.Machine Drop',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Machine Drop'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CPerformDrop'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Perform Drop',
           'BMC.Presentation.CPerformDrop',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Perform Drop',
           ObjectName = 'BMC.Presentation.CPerformDrop',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CPerformDrop'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CDeclaration'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Perform Declaration',
           'BMC.Presentation.CDeclaration',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Perform Declaration',
           ObjectName = 'BMC.Presentation.CDeclaration',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CDeclaration'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.BatchBreakdown.btnExport'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Export Batch',
           'BMC.Presentation.BatchBreakdown.btnExport',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Export Batch',
           ObjectName = 'BMC.Presentation.BatchBreakdown.btnExport',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.BatchBreakdown.btnExport'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.CashDeskMananger'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'CashDeskManager from Main Screen',
           'CashdeskOperator.MainScreen.cs.CashDeskMananger',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'CashDeskManager from Main Screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.CashDeskMananger',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.CashDeskMananger'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'BMC.Presentation.CashDeskManager.UserControls.CashDeskManagerAllDetails.lvViewAll.TicketValue'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'View Ticket Details',
           'BMC.Presentation.CashDeskManager.UserControls.CashDeskManagerAllDetails.lvViewAll.TicketValue',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'View Ticket Details',
           ObjectName = 
           'BMC.Presentation.CashDeskManager.UserControls.CashDeskManagerAllDetails.lvViewAll.TicketValue',
           ObjectType = 1
    WHERE  ObjectName = 
           'BMC.Presentation.CashDeskManager.UserControls.CashDeskManagerAllDetails.lvViewAll.TicketValue'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Audit'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Audit from Main screen',
           'CashdeskOperator.MainScreen.cs.Audit',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Audit from Main screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.Audit',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Audit'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'CustomReports Tab in MainScreen',
           'CashdeskOperator.MainScreen.cs.CustomReports',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'CustomReports Tab in MainScreen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'CashdeskOperator.MainScreen.cs.CustomReports.ExpenseDetailReport'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'ExpenseDetailReport in CustomReports Tab',
           'CashdeskOperator.MainScreen.cs.CustomReports.ExpenseDetailReport',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'ExpenseDetailReport in CustomReports Tab',
           ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.ExpenseDetailReport',
           ObjectType = 1
    WHERE  ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.ExpenseDetailReport'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'CashdeskOperator.MainScreen.cs.CustomReports.ExpiredVoucherCouponReport'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'ExpiredVoucherCouponReport in CustomReports Tab',
           'CashdeskOperator.MainScreen.cs.CustomReports.ExpiredVoucherCouponReport',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'ExpiredVoucherCouponReport in CustomReports Tab',
           ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.ExpiredVoucherCouponReport',
           ObjectType = 1
    WHERE  ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.ExpiredVoucherCouponReport'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'CashdeskOperator.MainScreen.cs.CustomReports.JackpotSlipSummaryReport'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'JackpotSlipSummaryReport in CustomReports Tab',
           'CashdeskOperator.MainScreen.cs.CustomReports.JackpotSlipSummaryReport',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'JackpotSlipSummaryReport in CustomReports Tab',
           ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.JackpotSlipSummaryReport',
           ObjectType = 1
    WHERE  ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.JackpotSlipSummaryReport'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'CashdeskOperator.MainScreen.cs.CustomReports.MeterListReport'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'MeterListReport in CustomReports Tab',
           'CashdeskOperator.MainScreen.cs.CustomReports.MeterListReport',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'MeterListReport in CustomReports Tab',
           ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.MeterListReport',
           ObjectType = 1
    WHERE  ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.MeterListReport'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'CashdeskOperator.MainScreen.cs.CustomReports.RedeemedTicketByDeviceReport'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'RedeemedTicketByDeviceReport in CustomReports Tab',
           'CashdeskOperator.MainScreen.cs.CustomReports.RedeemedTicketByDeviceReport',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'RedeemedTicketByDeviceReport in CustomReports Tab',
           ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.RedeemedTicketByDeviceReport',
           ObjectType = 1
    WHERE  ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.RedeemedTicketByDeviceReport'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'CashdeskOperator.MainScreen.cs.CustomReports.TicketIssuedReport'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'TicketIssuedReport in CustomReports Tab',
           'CashdeskOperator.MainScreen.cs.CustomReports.TicketIssuedReport',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'TicketIssuedReport in CustomReports Tab',
           ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.TicketIssuedReport',
           ObjectType = 1
    WHERE  ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.TicketIssuedReport'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'CashdeskOperator.MainScreen.cs.CustomReports.VoucherCouponLiabilityReport'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'VoucherCouponLiabilityReport in CustomReports Tab in MainScreen',
           'CashdeskOperator.MainScreen.cs.CustomReports.VoucherCouponLiabilityReport',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 
           'VoucherCouponLiabilityReport in CustomReports Tab in MainScreen',
           ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.VoucherCouponLiabilityReport',
           ObjectType = 1
    WHERE  ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.VoucherCouponLiabilityReport'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'CashdeskOperator.MainScreen.cs.CustomReports.ExceptionVoucherDetails'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Exception Voucher Details Report',
           'CashdeskOperator.MainScreen.cs.CustomReports.ExceptionVoucherDetails',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Exception Voucher Details Report',
           ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.ExceptionVoucherDetails',
           ObjectType = 1
    WHERE  ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.ExceptionVoucherDetails'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'CashdeskOperator.MainScreen.cs.CustomReports.CrossPropertyLiabilityTransferSummaryReport'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Cross Property Liability Transfer Summary Report',
           'CashdeskOperator.MainScreen.cs.CustomReports.CrossPropertyLiabilityTransferSummaryReport',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Cross Property Liability Transfer Summary Report',
           ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.CrossPropertyLiabilityTransferSummaryReport',
           ObjectType = 1
    WHERE  ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.CrossPropertyLiabilityTransferSummaryReport'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'CashdeskOperator.MainScreen.cs.CustomReports.CrossPropertyLiabilityTransferDetailsReport'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Cross Property Liability Transfer Details Report',
           'CashdeskOperator.MainScreen.cs.CustomReports.CrossPropertyLiabilityTransferDetailsReport',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Cross Property Liability Transfer Details Report',
           ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.CrossPropertyLiabilityTransferDetailsReport',
           ObjectType = 1
    WHERE  ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.CrossPropertyLiabilityTransferDetailsReport'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'CashdeskOperator.MainScreen.cs.CustomReports.CrossPropertyTicketAnalysisReport'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Cross Property Ticket Analysis Report',
           'CashdeskOperator.MainScreen.cs.CustomReports.CrossPropertyTicketAnalysisReport',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Cross Property Ticket Analysis Report',
           ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.CrossPropertyTicketAnalysisReport',
           ObjectType = 1
    WHERE  ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.CrossPropertyTicketAnalysisReport'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'CashdeskOperator.MainScreen.cs.CustomReports.StackerLevelDetailsReport'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Stacker Level Details Report',
           'CashdeskOperator.MainScreen.cs.CustomReports.StackerLevelDetailsReport',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Stacker Level Details Report',
           ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.StackerLevelDetailsReport',
           ObjectType = 1
    WHERE  ObjectName = 
           'CashdeskOperator.MainScreen.cs.CustomReports.StackerLevelDetailsReport'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Site Settings'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Site Settings from Main Screen',
           'CashdeskOperator.MainScreen.cs.Site Settings',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Site Settings from Main Screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.Site Settings',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Site Settings'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'CashdeskOperator.MainScreen.cs.Site Settings.btnSystemConfigParameters'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Site Settings from Main Screen',
           'CashdeskOperator.MainScreen.cs.Site Settings.btnSystemConfigParameters',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Site Settings from Main Screen',
           ObjectName = 
           'CashdeskOperator.MainScreen.cs.Site Settings.btnSystemConfigParameters',
           ObjectType = 1
    WHERE  ObjectName = 
           'CashdeskOperator.MainScreen.cs.Site Settings.btnSystemConfigParameters'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Factory'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'FactoryReset from Main Screen',
           'CashdeskOperator.MainScreen.cs.Factory',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'FactoryReset from Main Screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.Factory',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Factory'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.AFTSettings'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'AFTSettings from Main screen',
           'CashdeskOperator.MainScreen.cs.AFTSettings',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'AFTSettings from Main screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.AFTSettings',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.AFTSettings'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.ExchangeConfig.Login'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'BMC Exchange Configuration',
           'BMC.ExchangeConfig.Login',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'BMC Exchange Configuration',
           ObjectName = 'BMC.ExchangeConfig.Login',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.ExchangeConfig.Login'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.AFTEnableDisable'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'AFTEnableDisable from Main screen',
           'CashdeskOperator.MainScreen.cs.AFTEnableDisable',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'AFTEnableDisable from Main screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.AFTEnableDisable',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.AFTEnableDisable'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.CoinDispenser'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Coin Dispenser from Main screen',
           'CashdeskOperator.MainScreen.cs.CoinDispenser',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Coin Dispenser from Main screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.CoinDispenser',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.CoinDispenser'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CPpTicket'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Exception vouchers from Main Screen',
           'BMC.Presentation.CPpTicket',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Exception vouchers from Main Screen',
           ObjectName = 'BMC.Presentation.CPpTicket',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CPpTicket'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'BMC.Presentation.CashDeskManager.UserControls.CashDeskManager.DisplayTransactions'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Display Transactions',
           'BMC.Presentation.CashDeskManager.UserControls.CashDeskManager.DisplayTransactions',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Display Transactions',
           ObjectName = 
           'BMC.Presentation.CashDeskManager.UserControls.CashDeskManager.DisplayTransactions',
           ObjectType = 1
    WHERE  ObjectName = 
           'BMC.Presentation.CashDeskManager.UserControls.CashDeskManager.DisplayTransactions'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CrossTicketingSettings'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'CrossTicketingSettings',
           'BMC.Presentation.CrossTicketingSettings',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'CrossTicketingSettings',
           ObjectName = 'BMC.Presentation.CrossTicketingSettings',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CrossTicketingSettings'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Shortpay'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Short pay from Main Screen',
           'CashdeskOperator.MainScreen.cs.Shortpay',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Short pay from Main Screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.Shortpay',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Shortpay'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CommonCDOforDeclaration'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Common CDO for Declaration',
           'BMC.Presentation.CommonCDOforDeclaration',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Common CDO for Declaration',
           ObjectName = 'BMC.Presentation.CommonCDOforDeclaration',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CommonCDOforDeclaration'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'BMC.Presentation.CashDeskManager.UserControls.CashDeskManager.AccessOtherUsers'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Access Other Users',
           'BMC.Presentation.CashDeskManager.UserControls.CashDeskManager.AccessOtherUsers',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Access Other Users',
           ObjectName = 
           'BMC.Presentation.CashDeskManager.UserControls.CashDeskManager.AccessOtherUsers',
           ObjectType = 1
    WHERE  ObjectName = 
           'BMC.Presentation.CashDeskManager.UserControls.CashDeskManager.AccessOtherUsers'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.VoucherConfig'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Voucher Config from Main screen',
           'CashdeskOperator.MainScreen.cs.VoucherConfig',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Voucher Config from Main screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.VoucherConfig',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.VoucherConfig'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.AttendantPay'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Attendant pays from Main screen',
           'CashdeskOperator.MainScreen.cs.AttendantPay',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Attendant pays from Main screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.AttendantPay',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.AttendantPay'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.ManualAttendantPay'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Attendant pays from Main screen',
           'CashdeskOperator.MainScreen.cs.ManualAttendantPay',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Attendant pays from Main screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.ManualAttendantPay',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.ManualAttendantPay'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CAttendantpay.btnVoid'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Void Button on AttendantPay Screen',
           'BMC.Presentation.CAttendantpay.btnVoid',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Void Button on AttendantPay Screen',
           ObjectName = 'BMC.Presentation.CAttendantpay.btnVoid',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CAttendantpay.btnVoid'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CFillVault'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Vault Screen',
           'BMC.Presentation.CFillVault',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Vault Screen',
           ObjectName = 'BMC.Presentation.CFillVault',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CFillVault'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CFillVault.btnVaultFill'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Fill Button in Vault Screen ',
           'BMC.Presentation.CFillVault.btnVaultFill',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Fill Button in Vault Screen ',
           ObjectName = 'BMC.Presentation.CFillVault.btnVaultFill',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CFillVault.btnVaultFill'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CFillVault.btnVaultBleed'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Bleed Button in Vault Screen',
           'BMC.Presentation.CFillVault.btnVaultBleed',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Bleed Button in Vault Screen',
           ObjectName = 'BMC.Presentation.CFillVault.btnVaultBleed',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CFillVault.btnVaultBleed'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CFillVault.btnVaultAdjustment'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Adjustment Button in Vault Screen',
           'BMC.Presentation.CFillVault.btnVaultAdjustment',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Adjustment Button in Vault Screen',
           ObjectName = 'BMC.Presentation.CFillVault.btnVaultAdjustment',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CFillVault.btnVaultAdjustment'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CFillVault.btnVaultDrop'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Drop Button in Vault Screen',
           'BMC.Presentation.CFillVault.btnVaultDrop',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Drop Button in Vault Screen',
           ObjectName = 'BMC.Presentation.CFillVault.btnVaultDrop',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CFillVault.btnVaultDrop'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CFillVault.btnVaultDeclaration'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Declaration Button in Vault Screen',
           'BMC.Presentation.CFillVault.btnVaultDeclaration',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Declaration Button in Vault Screen',
           ObjectName = 'BMC.Presentation.CFillVault.btnVaultDeclaration',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CFillVault.btnVaultDeclaration'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CViewHandpay.btnProcess'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Process Button on AttendantPay Screen',
           'BMC.Presentation.CViewHandpay.btnProcess',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Process Button on AttendantPay Screen',
           ObjectName = 'BMC.Presentation.CViewHandpay.btnProcess',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CViewHandpay.btnProcess'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.CVoidTicket'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Void Vouchers',
           'CashdeskOperator.MainScreen.cs.CVoidTicket',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Void Vouchers',
           ObjectName = 'CashdeskOperator.MainScreen.cs.CVoidTicket',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.CVoidTicket'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.LicenseActivation'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Activate License',
           'CashdeskOperator.MainScreen.cs.LicenseActivation',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Activate License',
           ObjectName = 'CashdeskOperator.MainScreen.cs.LicenseActivation',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.LicenseActivation'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Gmuupdatebin'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Gmuupdatebin from Main Screen',
           'CashdeskOperator.MainScreen.cs.Gmuupdatebin',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Gmuupdatebin from Main Screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.Gmuupdatebin',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Gmuupdatebin'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'CashdeskOperator.MainScreen.cs.ReadBasedLiquidationMain'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'ReadBasedLiquidation from Main Screen',
           'CashdeskOperator.MainScreen.cs.ReadBasedLiquidationMain',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'ReadBasedLiquidation from Main Screen',
           ObjectName = 
           'CashdeskOperator.MainScreen.cs.ReadBasedLiquidationMain',
           ObjectType = 1
    WHERE  ObjectName = 
           'CashdeskOperator.MainScreen.cs.ReadBasedLiquidationMain'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'BMC.Presentation.ReadBasedLiquidationMain.ReadBasedLiquidation'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'ReadBasedLiquidation from Read Liquidation Tab',
           'BMC.Presentation.ReadBasedLiquidationMain.ReadBasedLiquidation',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'ReadBasedLiquidation from Read Liquidation Tab',
           ObjectName = 
           'BMC.Presentation.ReadBasedLiquidationMain.ReadBasedLiquidation',
           ObjectType = 1
    WHERE  ObjectName = 
           'BMC.Presentation.ReadBasedLiquidationMain.ReadBasedLiquidation'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'BMC.Presentation.ReadBasedLiquidationMain.ReportLiquidationReport'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'ReadLiquidation Report from ReadBasedLiquidation',
           'BMC.Presentation.ReadBasedLiquidationMain.ReportLiquidationReport',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'ReadLiquidation Report from ReadBasedLiquidation',
           ObjectName = 
           'BMC.Presentation.ReadBasedLiquidationMain.ReportLiquidationReport',
           ObjectType = 1
    WHERE  ObjectName = 
           'BMC.Presentation.ReadBasedLiquidationMain.ReportLiquidationReport'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.SiteInterrogation'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Site Interrogation from Main Screen',
           'CashdeskOperator.MainScreen.cs.SiteInterrogation',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Site Interrogation from Main Screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.SiteInterrogation',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.SiteInterrogation'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CBatchLiquidation'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Liquidation',
           'BMC.Presentation.CBatchLiquidation',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Liquidation',
           ObjectName = 'BMC.Presentation.CBatchLiquidation',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CBatchLiquidation'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Unlock'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Data Unlock',
           'CashdeskOperator.MainScreen.cs.Unlock',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Data Unlock',
           ObjectName = 'CashdeskOperator.MainScreen.cs.Unlock',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Unlock'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.SyncEmpCard'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'SyncEmpCard',
           'CashdeskOperator.MainScreen.cs.SyncEmpCard',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'SyncEmpCard',
           ObjectName = 'CashdeskOperator.MainScreen.cs.SyncEmpCard',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.SyncEmpCard'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.ExportDetails'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Export Details',
           'CashdeskOperator.MainScreen.cs.ExportDetails',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Export Details',
           ObjectName = 'CashdeskOperator.MainScreen.cs.ExportDetails',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.ExportDetails'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.SpotCheck'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Spot Check',
           'CashdeskOperator.MainScreen.cs.SpotCheck',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Spot Check',
           ObjectName = 'CashdeskOperator.MainScreen.cs.SpotCheck',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.SpotCheck'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CPerformDrop.PartCollection'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Part Collection',
           'BMC.Presentation.CPerformDrop.PartCollection',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Part Collection',
           ObjectName = 'BMC.Presentation.CPerformDrop.PartCollection',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CPerformDrop.PartCollection'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CPerformDrop.PartCollectionDrop'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Part-Collection Drop',
           'BMC.Presentation.CPerformDrop.PartCollectionDrop',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Part-Collection Drop',
           ObjectName = 'BMC.Presentation.CPerformDrop.PartCollectionDrop',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CPerformDrop.PartCollectionDrop'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 
              'BMC.Presentation.CDeclaration.PartCollectionDeclaration'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Part-Collection Declaration',
           'BMC.Presentation.CDeclaration.PartCollectionDeclaration',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Part-Collection Declaration',
           ObjectName = 
           'BMC.Presentation.CDeclaration.PartCollectionDeclaration',
           ObjectType = 1
    WHERE  ObjectName = 
           'BMC.Presentation.CDeclaration.PartCollectionDeclaration'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.UpdateGMUNo'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Update GMU Number',
           'CashdeskOperator.MainScreen.cs.UpdateGMUNo',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Update GMU Number',
           ObjectName = 'CashdeskOperator.MainScreen.cs.UpdateGMUNo',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.UpdateGMUNo'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CShortPay.ShortPayApprover'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'ShortPay Immediate Approver',
           'BMC.Presentation.CShortPay.ShortPayApprover',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'ShortPay Immediate Approver',
           ObjectName = 'BMC.Presentation.CShortPay.ShortPayApprover',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CShortPay.ShortPayApprover'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.Promotions'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Promotions',
           'BMC.Presentation.Promotions',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Promotions',
           ObjectName = 'BMC.Presentation.Promotions',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.Promotions'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.Promotions.Print'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Print Promotional Voucher',
           'BMC.Presentation.Promotions.Print',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Print Promotional Voucher',
           ObjectName = 'BMC.Presentation.Promotions.Print',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.Promotions.Print'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.Promotions.Void'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Void Promotional Voucher',
           'BMC.Presentation.Promotions.Void',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Void Promotional Voucher',
           ObjectName = 'BMC.Presentation.Promotions.Void',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.Promotions.Void'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.Promotions.History'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Promotional History',
           'BMC.Presentation.Promotions.History',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Promotional History',
           ObjectName = 'BMC.Presentation.Promotions.History',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.Promotions.History'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.Promotions.TIS'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'TIS Promotional Details',
           'BMC.Presentation.Promotions.TIS',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'TIS Promotional Details',
           ObjectName = 'BMC.Presentation.Promotions.TIS',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.Promotions.TIS'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.GameCapping'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Game Capping',
           'CashdeskOperator.MainScreen.cs.GameCapping',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Game Capping',
           ObjectName = 'CashdeskOperator.MainScreen.cs.GameCapping',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.GameCapping'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CNGAEnroll'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'NGA Enrollment Screen',
           'BMC.Presentation.CNGAEnroll',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'NGA Enrollment Screen',
           ObjectName = 'BMC.Presentation.CNGAEnroll',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CNGAEnroll'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Grid.View'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Grid View from Main Screen',
           'CashdeskOperator.MainScreen.cs.Grid.View',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Grid View from Main Screen',
           ObjectName = 'CashdeskOperator.MainScreen.cs.Grid.View',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.Grid.View'

IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CGMUPING'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'GMU Ping Screen',
           'BMC.Presentation.CGMUPING',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'GMU Ping Screen',
           ObjectName = 'BMC.Presentation.CGMUPING',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CGMUPING'
    
    
    IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'BMC.Presentation.CProfitShare.ProfitShareApprover'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'ProfitShare Immediate Approver',
           'BMC.Presentation.CProfitShare.ProfitShareApprover',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'ProfitShare Immediate Approver',
           ObjectName = 'BMC.Presentation.CProfitShare.ProfitShareApprover',
           ObjectType = 1
    WHERE  ObjectName = 'BMC.Presentation.CProfitShare.ProfitShareApprover'	
    
    IF NOT EXISTS(
           SELECT 1
           FROM   OBJECT
           WHERE  ObjectName = 'BMC.CPositionDetailsPlayerClub.PlayerClubBonus'
       )
        INSERT OBJECT
          (
            DESCRIPTION,
            ObjectName,
            ObjectType
          )
        SELECT 'PlayerClub Bonus Screen',
               'BMC.CPositionDetailsPlayerClub.PlayerClubBonus',
               1
    ELSE
        UPDATE OBJECT
        SET    DESCRIPTION = 'PlayerClub Bonus Screen',
               ObjectName = 'BMC.CPositionDetailsPlayerClub.PlayerClubBonus',
               ObjectType = 1
        WHERE  ObjectName = 'BMC.CPositionDetailsPlayerClub.PlayerClubBonus'
        
IF NOT EXISTS(
       SELECT 1
       FROM   OBJECT
       WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports.AccountingWinLossReport'
   )
    INSERT OBJECT
      (
        DESCRIPTION,
        ObjectName,
        ObjectType
      )
    SELECT 'Accounting Win/Loss Report',
           'CashdeskOperator.MainScreen.cs.CustomReports.AccountingWinLossReport',
           1
ELSE
    UPDATE OBJECT
    SET    DESCRIPTION = 'Accounting Win/Loss Report',
           ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports.AccountingWinLossReport',
           ObjectType = 1
    WHERE  ObjectName = 'CashdeskOperator.MainScreen.cs.CustomReports.AccountingWinLossReport'
    
GO
        	