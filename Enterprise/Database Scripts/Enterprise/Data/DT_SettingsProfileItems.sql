/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/2012 6:45:03 PM
 ************************************************************/

USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AFT_ENABLED'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'TRUE' FROM SettingsMaster WHERE SettingsMaster_Name = 'AFT_ENABLED'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'TRUE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AFT_ENABLED')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Enable_Disable_Machines'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Enable_Disable_Machines'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Enable_Disable_Machines')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Batch_Conf_Screen'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '' FROM SettingsMaster WHERE SettingsMaster_Name = 'Batch_Conf_Screen'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = ''
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Batch_Conf_Screen')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CardNumberFormat'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, ' ;([A-Z,a-z,0-9])*\?' FROM SettingsMaster WHERE SettingsMaster_Name = 'CardNumberFormat'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = ' ;([A-Z,a-z,0-9])*\?'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CardNumberFormat')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ConnexusPrintHandpayTimeDiff'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '10' FROM SettingsMaster WHERE SettingsMaster_Name = 'ConnexusPrintHandpayTimeDiff '
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '10'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ConnexusPrintHandpayTimeDiff ')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DailyRetry'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '3' FROM SettingsMaster WHERE SettingsMaster_Name = 'DailyRetry'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '3'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DailyRetry')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DailyTry'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '3' FROM SettingsMaster WHERE SettingsMaster_Name = 'DailyTry'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '3'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DailyTry')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DropRetry'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '' FROM SettingsMaster WHERE SettingsMaster_Name = 'DropRetry'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = ''
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DropRetry')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableGMUMachine'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableGMUMachine'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableGMUMachine')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableRedeemPrintCDO'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableRedeemPrintCDO'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableRedeemPrintCDO')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'GAMING_DAY_START_HOUR'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '6' FROM SettingsMaster WHERE SettingsMaster_Name = 'GAMING_DAY_START_HOUR'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '6'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'GAMING_DAY_START_HOUR')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HOURLY_STATS_CONS_DATA_NOOFDAYS'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '90' FROM SettingsMaster WHERE SettingsMaster_Name = 'HOURLY_STATS_CONS_DATA_NOOFDAYS'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '90'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HOURLY_STATS_CONS_DATA_NOOFDAYS')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HourlyRetry'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '3' FROM SettingsMaster WHERE SettingsMaster_Name = 'HourlyRetry'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '3'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HourlyRetry')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HourlyTry'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '3' FROM SettingsMaster WHERE SettingsMaster_Name = 'HourlyTry'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '3'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HourlyTry')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'InnerCardNumberFormat'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '([A-Z,a-z,0-9])*' FROM SettingsMaster WHERE SettingsMaster_Name = 'InnerCardNumberFormat'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '([A-Z,a-z,0-9])*'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'InnerCardNumberFormat')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'NumberOfCharToTrim'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '4' FROM SettingsMaster WHERE SettingsMaster_Name = 'NumberOfCharToTrim'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '4'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'NumberOfCharToTrim')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PartialDetailsExport'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '' FROM SettingsMaster WHERE SettingsMaster_Name = 'PartialDetailsExport'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = ''
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PartialDetailsExport')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Polling_Timer'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '2' FROM SettingsMaster WHERE SettingsMaster_Name = 'Polling_Timer'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '2'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Polling_Timer')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TICKET_CREATE_TIMEOUT'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '2' FROM SettingsMaster WHERE SettingsMaster_Name = 'TICKET_CREATE_TIMEOUT'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '2'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TICKET_CREATE_TIMEOUT')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableLaundering'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableLaundering'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableLaundering')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableVoucher'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableVoucher'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableVoucher')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'VoucherValidate_EnableRefillReceipt'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'VoucherValidate_EnableRefillReceipt'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'VoucherValidate_EnableRefillReceipt')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableRefundReceipt'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableRefundReceipt'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableRefundReceipt')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TICKETVALIDATE_REQUIRES_HEADCASHIER_SIG'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '10000' FROM SettingsMaster WHERE SettingsMaster_Name = 'TICKETVALIDATE_REQUIRES_HEADCASHIER_SIG'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '10000'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TICKETVALIDATE_REQUIRES_HEADCASHIER_SIG')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TICKETVALIDATE_REQUIRES_MANAGER_SIG'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '20000' FROM SettingsMaster WHERE SettingsMaster_Name = 'TICKETVALIDATE_REQUIRES_MANAGER_SIG'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '20000'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TICKETVALIDATE_REQUIRES_MANAGER_SIG')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableHandpayReceipt'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'TRUE' FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableHandpayReceipt'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'TRUE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableHandpayReceipt')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableIssueReceipt'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'TRUE' FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableIssueReceipt'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'TRUE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableIssueReceipt')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableShortpayReceipt'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'TRUE' FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableShortpayReceipt'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'TRUE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableShortpayReceipt')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'BusinessDayAdjustment'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '-1' FROM SettingsMaster WHERE SettingsMaster_Name = 'BusinessDayAdjustment'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '-1'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'BusinessDayAdjustment')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TICKET_DB_NAME'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'Ticketing' FROM SettingsMaster WHERE SettingsMaster_Name = 'TICKET_DB_NAME'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'Ticketing'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TICKET_DB_NAME')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'REGION'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'UK' FROM SettingsMaster WHERE SettingsMaster_Name = 'REGION'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'UK'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'REGION')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AddCashToExistingBreakdown'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'AddCashToExistingBreakdown'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AddCashToExistingBreakdown')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HandpayManualEntry'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'HandpayManualEntry'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HandpayManualEntry')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'NoSerialNumberDisplayed'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'NoSerialNumberDisplayed'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'NoSerialNumberDisplayed')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'NoAltSerialNumberDisplayed'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'NoAltSerialNumberDisplayed'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'NoAltSerialNumberDisplayed')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DailyAutoReadTime'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '6:30' FROM SettingsMaster WHERE SettingsMaster_Name = 'DailyAutoReadTime'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '6:30'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DailyAutoReadTime')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SAS_UpdateTicketDetails'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'SAS_UpdateTicketDetails'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SAS_UpdateTicketDetails')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PROMO_TICKET_CODE'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '' FROM SettingsMaster WHERE SettingsMaster_Name = 'PROMO_TICKET_CODE'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = ''
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PROMO_TICKET_CODE')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SGVI_Batch_Net_Value'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '0.22' FROM SettingsMaster WHERE SettingsMaster_Name = 'SGVI_Batch_Net_Value'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '0.22'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SGVI_Batch_Net_Value')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SGVI_Enabled'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'SGVI_Enabled'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SGVI_Enabled')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DB_VERSION'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '12.5' FROM SettingsMaster WHERE SettingsMaster_Name = 'DB_VERSION'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '12.5'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DB_VERSION')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Declare_Monies'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Declare_Monies'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Declare_Monies')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'External_Connection'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'External_Connection'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'External_Connection')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DOWNTIME_MINS_ALLOWED_FOR_ExTERNAL_CONNECTION'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '10' FROM SettingsMaster WHERE SettingsMaster_Name = 'DOWNTIME_MINS_ALLOWED_FOR_ExTERNAL_CONNECTION'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '10'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DOWNTIME_MINS_ALLOWED_FOR_ExTERNAL_CONNECTION')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TIME_SINCE_LAST_EXTERNAL_CONNECTION'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'Jun  8 2010 11:57AM' FROM SettingsMaster WHERE SettingsMaster_Name = 'TIME_SINCE_LAST_EXTERNAL_CONNECTION'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'Jun  8 2010 11:57AM'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TIME_SINCE_LAST_EXTERNAL_CONNECTION')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PT_GATEWAY_IP'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '127.0.0.1' FROM SettingsMaster WHERE SettingsMaster_Name = 'PT_GATEWAY_IP'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '127.0.0.1'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PT_GATEWAY_IP')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PT_GATEWAY_PORT_NO'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '3334' FROM SettingsMaster WHERE SettingsMaster_Name = 'PT_GATEWAY_PORT_NO'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '3334'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PT_GATEWAY_PORT_NO')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PT_GATEWAY_MSG_RESP_TIMEOUT'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '10' FROM SettingsMaster WHERE SettingsMaster_Name = 'PT_GATEWAY_MSG_RESP_TIMEOUT'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '10'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PT_GATEWAY_MSG_RESP_TIMEOUT')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Close_Door_Event'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Close_Door_Event'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Close_Door_Event')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Close_Power_Event'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Close_Power_Event'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Close_Power_Event')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Close_Comms_Event'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Close_Comms_Event'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Close_Comms_Event')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Close_Fault_Event'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Close_Fault_Event'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Close_Fault_Event')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'USE_DIRECTED_MESSAGING'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'USE_DIRECTED_MESSAGING'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'USE_DIRECTED_MESSAGING')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EPIGatewayConnstrNA'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '' FROM SettingsMaster WHERE SettingsMaster_Name = 'EPIGatewayConnstrNA'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = ''
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EPIGatewayConnstrNA')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HideLayoutOnDeclarationTab'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'HideLayoutOnDeclarationTab'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HideLayoutOnDeclarationTab')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'UseJetscanNoteCounter'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'UseJetscanNoteCounter'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'UseJetscanNoteCounter')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'LastFailedEventsSentTime'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '' FROM SettingsMaster WHERE SettingsMaster_Name = 'LastFailedEventsSentTime'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = ''
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'LastFailedEventsSentTime')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Allow_Offline_Redeem'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'Allow_Offline_Redeem'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Allow_Offline_Redeem')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidation_FillScreen'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'TRUE' FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidation_FillScreen'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'TRUE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidation_FillScreen')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'VoidTransactions'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'VoidTransactions'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'VoidTransactions')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CD_NOT_USE_HOPPERS'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'CD_NOT_USE_HOPPERS'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CD_NOT_USE_HOPPERS')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CD_NOT_ISSUE_VOUCHER'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'CD_NOT_ISSUE_VOUCHER'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CD_NOT_ISSUE_VOUCHER')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CD_TITO_NOT_IN_USE'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'CD_TITO_NOT_IN_USE'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CD_TITO_NOT_IN_USE')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CD_SP_NOT_IN_USE'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'CD_SP_NOT_IN_USE'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CD_SP_NOT_IN_USE')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'USE_ON_SCREEN_KEYBOARD'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'USE_ON_SCREEN_KEYBOARD'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'USE_ON_SCREEN_KEYBOARD')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'VATRate'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '' FROM SettingsMaster WHERE SettingsMaster_Name = 'VATRate'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = ''
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'VATRate')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CDM_SHOW_COIN_HOPPER'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'FALSE' FROM SettingsMaster WHERE SettingsMaster_Name = 'CDM_SHOW_COIN_HOPPER'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'FALSE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CDM_SHOW_COIN_HOPPER')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SETTING_NAMES_HIDDEN'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'SETTING_NAMES_HIDDEN,Ticketing.Connection,EPIGatewayConnstr' FROM SettingsMaster WHERE SettingsMaster_Name = 'SETTING_NAMES_HIDDEN'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'SETTING_NAMES_HIDDEN,Ticketing.Connection,EPIGatewayConnstr'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SETTING_NAMES_HIDDEN')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EVENTTYPELIST'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'Comms,Device Error,Door,Error,Fault,Game GMU Request,General,Power,Tilt,Printer,Information,Card' FROM SettingsMaster WHERE SettingsMaster_Name = 'EVENTTYPELIST'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'Comms,Device Error,Door,Error,Fault,Game GMU Request,General,Power,Tilt,Printer,Information,Card'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EVENTTYPELIST')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Allow_POP_Update'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'Allow_POP_Update'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Allow_POP_Update')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Confirm_Redeem_Message'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'Confirm_Redeem_Message'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Confirm_Redeem_Message')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'VerificationTimes'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '10,18' FROM SettingsMaster WHERE SettingsMaster_Name = 'VerificationTimes'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '10,18'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'VerificationTimes')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HandpayPayoutCustomer_Min'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '1000' FROM SettingsMaster WHERE SettingsMaster_Name = 'HandpayPayoutCustomer_Min'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '1000'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HandpayPayoutCustomer_Min')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HandpayPayoutCustomer_Max'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '4999' FROM SettingsMaster WHERE SettingsMaster_Name = 'HandpayPayoutCustomer_Max'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '4999'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HandpayPayoutCustomer_Max')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HandpayPayoutCustomer_BankAccNo'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '5000' FROM SettingsMaster WHERE SettingsMaster_Name = 'HandpayPayoutCustomer_BankAccNo'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '5000'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HandpayPayoutCustomer_BankAccNo')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SlotLifeToDate'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'SlotLifeToDate'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SlotLifeToDate')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'RedeemTicketCustomer_Min'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '1000' FROM SettingsMaster WHERE SettingsMaster_Name = 'RedeemTicketCustomer_Min'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '1000'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'RedeemTicketCustomer_Min')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'RedeemTicketCustomer_Max'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '4999' FROM SettingsMaster WHERE SettingsMaster_Name = 'RedeemTicketCustomer_Max'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '4999'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'RedeemTicketCustomer_Max')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'RedeemTicketCustomer_BankAcctNo'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '5000' FROM SettingsMaster WHERE SettingsMaster_Name = 'RedeemTicketCustomer_BankAcctNo'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '5000'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'RedeemTicketCustomer_BankAcctNo')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CheckExchangeServerConnectivity'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'CheckExchangeServerConnectivity'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CheckExchangeServerConnectivity')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ExchangeServerConnectivityCheckInterval'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '5' FROM SettingsMaster WHERE SettingsMaster_Name = 'ExchangeServerConnectivityCheckInterval'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '5'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ExchangeServerConnectivityCheckInterval')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AFTTransactionsAllowed'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'AFTTransactionsAllowed'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AFTTransactionsAllowed')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowCashableDeposits'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowCashableDeposits'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowCashableDeposits')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Allow Non-Cashable Deposits'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'Allow Non-Cashable Deposits'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Allow Non-Cashable Deposits')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowRedeemOffers'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowRedeemOffers'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowRedeemOffers')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowPointsWithdrawal'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowPointsWithdrawal'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowPointsWithdrawal')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowCashWithdrawal'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowCashWithdrawal'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowCashWithdrawal')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowPartialTransfers'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowPartialTransfers'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowPartialTransfers')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AutoDepositNon-CashableCreditsonCardOut'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'AutoDepositNon-CashableCreditsonCardOut'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AutoDepositNon-CashableCreditsonCardOut')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AutoDepositCashableCreditsonCardOut'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'AutoDepositCashableCreditsonCardOut'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AutoDepositCashableCreditsonCardOut')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowOffers'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowOffers'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowOffers')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EFTTimeoutValue'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '100' FROM SettingsMaster WHERE SettingsMaster_Name = 'EFTTimeoutValue'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '100'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EFTTimeoutValue')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Option1WithdrawalAmount'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '150' FROM SettingsMaster WHERE SettingsMaster_Name = 'Option1WithdrawalAmount'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '150'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Option1WithdrawalAmount')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Option2WithdrawalAmount'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '250' FROM SettingsMaster WHERE SettingsMaster_Name = 'Option2WithdrawalAmount'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '250'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Option2WithdrawalAmount')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Option3WithdrawalAmount'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '350' FROM SettingsMaster WHERE SettingsMaster_Name = 'Option3WithdrawalAmount'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '350'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Option3WithdrawalAmount')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Option4WithdrawalAmount'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '450' FROM SettingsMaster WHERE SettingsMaster_Name = 'Option4WithdrawalAmount'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '450'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Option4WithdrawalAmount')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Option5WithdrawalAmount'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '550' FROM SettingsMaster WHERE SettingsMaster_Name = 'Option5WithdrawalAmount'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '550'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Option5WithdrawalAmount')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'MaxDeposit Amount'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '5000' FROM SettingsMaster WHERE SettingsMaster_Name = 'MaxDeposit Amount'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '5000'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'MaxDeposit Amount')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'MaxWithDraw Amount'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '5000' FROM SettingsMaster WHERE SettingsMaster_Name = 'MaxWithDraw Amount'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '5000'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'MaxWithDraw Amount')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IssueTicketMaxValue'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '5000' FROM SettingsMaster WHERE SettingsMaster_Name = 'IssueTicketMaxValue'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '5000'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IssueTicketMaxValue')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Issue_Ticket_Encrypt_BarCode'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'Issue_Ticket_Encrypt_BarCode'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Issue_Ticket_Encrypt_BarCode')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Ithaca950'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'Ithaca950'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Ithaca950')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PrinterPort'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'COM1' FROM SettingsMaster WHERE SettingsMaster_Name = 'PrinterPort'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'COM1'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PrinterPort')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Login_Expiry_No_of_Days'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '60' FROM SettingsMaster WHERE SettingsMaster_Name = 'Login_Expiry_No_of_Days'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '60'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Login_Expiry_No_of_Days')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Login_Max_No_Of_Attempts'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '3' FROM SettingsMaster WHERE SettingsMaster_Name = 'Login_Max_No_Of_Attempts'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '3'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Login_Max_No_Of_Attempts')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'MaxHandPayAuthRequired'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'MaxHandPayAuthRequired'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'MaxHandPayAuthRequired')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ManualEntryTicketValidation'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'ManualEntryTicketValidation'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ManualEntryTicketValidation')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'RedeemExpiredTicket'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'RedeemExpiredTicket'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'RedeemExpiredTicket')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Instant_Periodic_Interval'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '20' FROM SettingsMaster WHERE SettingsMaster_Name = 'Instant_Periodic_Interval'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '20'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Instant_Periodic_Interval')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ClearEventsOnFinalDrop'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'MANUAL' FROM SettingsMaster WHERE SettingsMaster_Name = 'ClearEventsOnFinalDrop'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'MANUAL'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ClearEventsOnFinalDrop')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Voucher_Printer_Name'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'ITHACA950' FROM SettingsMaster WHERE SettingsMaster_Name = 'Voucher_Printer_Name'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'ITHACA950'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Voucher_Printer_Name')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CMPAPP_UNAME'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'casino' FROM SettingsMaster WHERE SettingsMaster_Name = 'CMPAPP_UNAME'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'casino'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CMPAPP_UNAME')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CMPAPP_PWD'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'nostaw' FROM SettingsMaster WHERE SettingsMaster_Name = 'CMPAPP_PWD'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'nostaw'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CMPAPP_PWD')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CMP_KIOSKURL'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'http://localhost/KioskWebSetup/KiokService.asmx' FROM SettingsMaster WHERE SettingsMaster_Name = 'CMP_KIOSKURL'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'http://localhost/KioskWebSetup/KiokService.asmx'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CMP_KIOSKURL')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AUTOLOGOFF_TIMEOUT'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '6000000' FROM SettingsMaster WHERE SettingsMaster_Name = 'AUTOLOGOFF_TIMEOUT'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '6000000'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AUTOLOGOFF_TIMEOUT')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Close_General_Event'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Close_General_Event'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Auto_Close_General_Event')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DISABLE_MACHINE_ON_DROP'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'DISABLE_MACHINE_ON_DROP'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DISABLE_MACHINE_ON_DROP')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'LiveMeter'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'LiveMeter'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'LiveMeter')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketDeclarationMethod'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'AUTO' FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketDeclarationMethod'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'AUTO'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketDeclarationMethod')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TIMEZONENAME'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'GMT_Standard_Time' FROM SettingsMaster WHERE SettingsMaster_Name = 'TIMEZONENAME'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'GMT_Standard_Time'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TIMEZONENAME')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'BillVoucherCounterCOMPort'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'COM1' FROM SettingsMaster WHERE SettingsMaster_Name = 'BillVoucherCounterCOMPort'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'COM1'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'BillVoucherCounterCOMPort')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsAFTIncludedInCalculation'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsAFTIncludedInCalculation'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsAFTIncludedInCalculation')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableProgressivePayoutReceipt'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'TRUE' FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableProgressivePayoutReceipt'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'TRUE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableProgressivePayoutReceipt')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsAftEnabledForSite'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsAftEnabledForSite'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsAftEnabledForSite')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'NoWaitForDisableMachine'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'NoWaitForDisableMachine'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'NoWaitForDisableMachine')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DisableOnExchangeTimeNotInSync'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'DisableOnExchangeTimeNotInSync'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DisableOnExchangeTimeNotInSync')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SyncDateTimeEnabled'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'SyncDateTimeEnabled'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SyncDateTimeEnabled')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TICKET_EXPIRE'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '10' FROM SettingsMaster WHERE SettingsMaster_Name = 'TICKET_EXPIRE'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '10'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TICKET_EXPIRE')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'W2GMessage'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'W2GMessage'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'W2GMessage')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'W2GWinAmount'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '1200' FROM SettingsMaster WHERE SettingsMaster_Name = 'W2GWinAmount'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '1200'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'W2GWinAmount')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'COPYRIGTINFO'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '© 2014 Bally Technologies Inc. All Rights Reserved' FROM SettingsMaster WHERE SettingsMaster_Name = 'COPYRIGTINFO'
 ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '© 2014 Bally Technologies Inc. All Rights Reserved'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'COPYRIGTINFO')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PRODUCTVERSION'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '12.5' FROM SettingsMaster WHERE SettingsMaster_Name = 'PRODUCTVERSION'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '12.5'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PRODUCTVERSION')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PRODUCTDESC'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '' FROM SettingsMaster WHERE SettingsMaster_Name = 'PRODUCTDESC'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = ''
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PRODUCTDESC')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'COMPANYNAME'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'Bally Technologies Inc' FROM SettingsMaster WHERE SettingsMaster_Name = 'COMPANYNAME'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'Bally Technologies Inc'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'COMPANYNAME')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PRODUCTNAME'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'Bally MultiConnect' FROM SettingsMaster WHERE SettingsMaster_Name = 'PRODUCTNAME'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'Bally MultiConnect'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PRODUCTNAME')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HandPayBeepEnabled'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'Y' FROM SettingsMaster WHERE SettingsMaster_Name = 'HandPayBeepEnabled'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'Y'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HandPayBeepEnabled')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'System_Parameter_Stock_Code_Prefix'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'LC' FROM SettingsMaster WHERE SettingsMaster_Name = 'System_Parameter_Stock_Code_Prefix'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'LC'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'System_Parameter_Stock_Code_Prefix')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CheckForStockPrefix'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'CheckForStockPrefix'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CheckForStockPrefix')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Handpay_Wav_Path'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'Media\HandPay.wav' FROM SettingsMaster WHERE SettingsMaster_Name = 'Handpay_Wav_Path'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'Media\HandPay.wav'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Handpay_Wav_Path')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CLIENT'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'WINCHELLS' FROM SettingsMaster WHERE SettingsMaster_Name = 'CLIENT'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'WINCHELLS'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CLIENT')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SHOWHANDPAYCODE'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'TRUE' FROM SettingsMaster WHERE SettingsMaster_Name = 'SHOWHANDPAYCODE'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'TRUE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SHOWHANDPAYCODE')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PokerGamePrefix'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'P,PK' FROM SettingsMaster WHERE SettingsMaster_Name = 'PokerGamePrefix'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'P,PK'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PokerGamePrefix')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsKioskRequired'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsKioskRequired'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsKioskRequired')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_ALLOWCASHIERLOCONTKTS'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_ALLOWCASHIERLOCONTKTS'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_ALLOWCASHIERLOCONTKTS')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_ALLOWPRINTTKTOVERRIDE'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_ALLOWPRINTTKTOVERRIDE'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_ALLOWPRINTTKTOVERRIDE')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_TKTPRINTERENABLED'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_TKTPRINTERENABLED'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_TKTPRINTERENABLED')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_MINTKTPRINTAMTFOREMP'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '1' FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_MINTKTPRINTAMTFOREMP'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '1'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_MINTKTPRINTAMTFOREMP')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_MAXTKTREDEMPTIONAMTFOREMP'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '100000' FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_MAXTKTREDEMPTIONAMTFOREMP'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '100000'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_MAXTKTREDEMPTIONAMTFOREMP')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_MAXTKTREDEMPTIONAMT'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '1000000' FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_MAXTKTREDEMPTIONAMT'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '1000000'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_MAXTKTREDEMPTIONAMT')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_MAXNOOFTKTPRINTLIMIT'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '10000' FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_MAXNOOFTKTPRINTLIMIT'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '10000'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_MAXNOOFTKTPRINTLIMIT')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_MAXDAILYCASHIERGENTKTAMT'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '1000000' FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_MAXDAILYCASHIERGENTKTAMT'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '1000000'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_MAXDAILYCASHIERGENTKTAMT')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_MAXTKTPRINTAMTFOREMP'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '100000' FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_MAXTKTPRINTAMTFOREMP'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '100000'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_MAXTKTPRINTAMTFOREMP')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_ENABLED'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_ENABLED'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CAGE_ENABLED')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'REDEEM NA'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'REDEEM NA'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'REDEEM NA')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsMachineBasedDropDeclaration'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsMachineBasedDropDeclaration'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsMachineBasedDropDeclaration')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HANDLE_EXCEPTIONTICKETS'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'TRUE' FROM SettingsMaster WHERE SettingsMaster_Name = 'HANDLE_EXCEPTIONTICKETS'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'TRUE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HANDLE_EXCEPTIONTICKETS')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HANDLE_EXCEPTIONTICKETS_COUNTER'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'TRUE' FROM SettingsMaster WHERE SettingsMaster_Name = 'HANDLE_EXCEPTIONTICKETS_COUNTER'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'TRUE'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HANDLE_EXCEPTIONTICKETS_COUNTER')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CashDispenserEnabled'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'CashDispenserEnabled'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CashDispenserEnabled')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AutoCashDispenseRequired'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'AutoCashDispenseRequired'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AutoCashDispenseRequired')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CashDispenserDenominations'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '100,50' FROM SettingsMaster WHERE SettingsMaster_Name = 'CashDispenserDenominations'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '100,50'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CashDispenserDenominations')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SendPT10FromClient'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'SendPT10FromClient'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SendPT10FromClient')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableCustomerReceipt'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableCustomerReceipt'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketValidate_EnableCustomerReceipt')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SendAcktoGateway'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'SendAcktoGateway'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SendAcktoGateway')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsShortPayEnabled'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsShortPayEnabled'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsShortPayEnabled')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Declaration_ShowoutValues'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'Declaration_ShowoutValues'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Declaration_ShowoutValues')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CentralizedDeclaration'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'CentralizedDeclaration'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CentralizedDeclaration')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'StackerLevelAlert'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'StackerLevelAlert'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'StackerLevelAlert')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CashDispenserType'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'Default' FROM SettingsMaster WHERE SettingsMaster_Name = 'CashDispenserType'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'Default'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CashDispenserType')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ExportVaultEvents'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'ExportVaultEvents'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ExportVaultEvents')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DropAlert'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'DropAlert'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DropAlert')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DeclarationAlert'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'DeclarationAlert'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DeclarationAlert')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ECash_Points'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'Cashable' FROM SettingsMaster WHERE SettingsMaster_Name = 'ECash_Points'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'Cashable'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ECash_Points')


IF EXISTS (SELECT 1 FROM   [SettingsProfileItems] WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM   SettingsMaster WHERE  SettingsMaster_Name = 'IsSingleCardEmployee'))
    DELETE [SettingsProfileItems] WHERE  SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM   SettingsMaster WHERE  SettingsMaster_Name = 'IsSingleCardEmployee')
	DELETE SettingsMaster WHERE  SettingsMaster_Name = 'IsSingleCardEmployee' 

IF EXISTS (SELECT 1 FROM   [SettingsProfileItems] WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE  SettingsMaster_Name = 'MaxNoOfCardsForEmployee'))
    DELETE [SettingsProfileItems] WHERE  SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM   SettingsMaster WHERE  SettingsMaster_Name = 'MaxNoOfCardsForEmployee')
    DELETE SettingsMaster WHERE  SettingsMaster_Name = 'MaxNoOfCardsForEmployee' 

IF EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CMPMode'))          
	DELETE [SettingsProfileItems] WHERE  SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM   SettingsMaster WHERE  SettingsMaster_Name = 'CMPMode')
	DELETE SettingsMaster WHERE  SettingsMaster_Name = 'CMPMode' 

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AutoDropEnabled'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'AutoDropEnabled'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AutoDropEnabled')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ForceManualDrop'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'ForceManualDrop'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ForceManualDrop')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'StackerFeature'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'StackerFeature'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'StackerFeature')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'StackerLevelTracking'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'StackerLevelTracking'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'StackerLevelTracking')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsEmployeeCardTrackingEnabled'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsEmployeeCardTrackingEnabled'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsEmployeeCardTrackingEnabled')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AddShortpayInVoucherOut'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'AddShortpayInVoucherOut'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AddShortpayInVoucherOut')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'LiquidationType'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'Collection' FROM SettingsMaster WHERE SettingsMaster_Name = 'LiquidationType'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'Collection'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'LiquidationType')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ExpenseShare'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'ExpenseShare'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ExpenseShare')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'WriteOffShare'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'WriteOffShare'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'WriteOffShare')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ServiceNames'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'BMCExchangeImportExport,BMCExchangeHost,BMC.MSMQ,BMCHourlyReadService,ExchangeTicketExportService,BMCGuardianService,BMCNetworkService,BMC PCIntegrationService,BMC TISService,BMC Utility Service' FROM SettingsMaster WHERE SettingsMaster_Name = 'ServiceNames'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'BMCExchangeImportExport,BMCExchangeHost,BMC.MSMQ,BMCHourlyReadService,ExchangeTicketExportService,BMCGuardianService,BMCNetworkService,BMC PCIntegrationService,BMC TISService,BMC Utility Service'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ServiceNames')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'LiquidationProfitShare'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'LiquidationProfitShare'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'LiquidationProfitShare')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CentralizedReadLiquidation'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'CentralizedReadLiquidation'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CentralizedReadLiquidation')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'NotesCounter_AutoStart'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'NotesCounter_AutoStart'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'NotesCounter_AutoStart')
    
    
    IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Dec_AutoMarkActiveTicketsAsPD'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'Dec_AutoMarkActiveTicketsAsPD'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Dec_AutoMarkActiveTicketsAsPD')


IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Dec_AutoMarkExceptionTicketAsPD'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'Dec_AutoMarkExceptionTicketAsPD'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Dec_AutoMarkExceptionTicketAsPD')


IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Are200and500BillsRequired'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'Are200and500BillsRequired'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Are200and500BillsRequired')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HourlyBasedOnCalendarDay'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'HourlyBasedOnCalendarDay'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'HourlyBasedOnCalendarDay')

--PART COLLECTION

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsPartCollectionEnabled'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsPartCollectionEnabled'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'VaultAlert')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'VaultAlert'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'VaultAlert'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'VaultAlert')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsRouteEnabled'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsRouteEnabled'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsRouteEnabled')

--Allow_Machine_Removal
IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Allow_Machine_Removal'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'Allow_Machine_Removal'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Allow_Machine_Removal')


--Void Ticket Expire
IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'VoidExpiredTicket'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'VoidExpiredTicket'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'VoidExpiredTicket')


IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsMachineBasedAutoDrop'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsMachineBasedAutoDrop'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsMachineBasedAutoDrop')
    
IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ProcessW2GAmount'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'ProcessW2GAmount'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ProcessW2GAmount')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'VoidVouchers'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'VoidVouchers'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'VoidVouchers')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'WeeklyReport'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'WeeklyReport'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'WeeklyReport')
    
IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ServiceNotRunningInterval'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '2' FROM SettingsMaster WHERE SettingsMaster_Name = 'ServiceNotRunningInterval'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '2'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ServiceNotRunningInterval')
    
IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ValidateGMUInSite'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'ValidateGMUInSite'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ValidateGMUInSite')
    
IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DefaultGMUValue'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '16' FROM SettingsMaster WHERE SettingsMaster_Name = 'DefaultGMUValue'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '16'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DefaultGMUValue')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ShortPayAuthorizationRequired'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'ShortPayAuthorizationRequired'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ShortPayAuthorizationRequired')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ShortPayAuthorizationLimit'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '10' FROM SettingsMaster WHERE SettingsMaster_Name = 'ShortPayAuthorizationLimit'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '10'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ShortPayAuthorizationLimit')
    
    
IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SlotNumberIsAsset'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'SlotNumberIsAsset'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SlotNumberIsAsset')
    
    
IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'StandIsPosition'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'StandIsPosition'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'StandIsPosition')
    

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsFinalDropRequiredForRemoval'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsFinalDropRequiredForRemoval'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsFinalDropRequiredForRemoval')
    
    
GO


IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableCounterInManualCashEntry'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableCounterInManualCashEntry'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableCounterInManualCashEntry')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SHOW_NAME_IN_RECEPIT_SIGNATURE'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'SHOW_NAME_IN_RECEPIT_SIGNATURE'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SHOW_NAME_IN_RECEPIT_SIGNATURE')
GO
IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IncludeVoucherClaimedInSlotInCDMReport'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'IncludeVoucherClaimedInSlotInCDMReport'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IncludeVoucherClaimedInSlotInCDMReport')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsGameCappingEnabled'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsGameCappingEnabled'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsGameCappingEnabled')
GO
IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsPromotionalTicketEnabled'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsPromotionalTicketEnabled'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsPromotionalTicketEnabled')
GO
IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsTISEnabled'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsTISEnabled'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsTISEnabled')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PreCommitmentEnabled'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'PreCommitmentEnabled'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PreCommitmentEnabled')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PreCommitmentRatingBasis'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'Time' FROM SettingsMaster WHERE SettingsMaster_Name = 'PreCommitmentRatingBasis'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'Time'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PreCommitmentRatingBasis')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsExtendedPlayer'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsExtendedPlayer'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsExtendedPlayer')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'BreakPeriodInterval'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '900' FROM SettingsMaster WHERE SettingsMaster_Name = 'BreakPeriodInterval'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '900'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'BreakPeriodMessage')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'BreakPeriodMessage'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'Have a Break!!!' FROM SettingsMaster WHERE SettingsMaster_Name = 'BreakPeriodMessage'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'Have a Break!!!'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'BreakPeriodMessage')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'BreakPeriodFadeOutTime'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '20' FROM SettingsMaster WHERE SettingsMaster_Name = 'BreakPeriodFadeOutTime'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '20'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'BreakPeriodFadeOutTime')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'MaximumPromotionalTicketsCount'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '0' FROM SettingsMaster WHERE SettingsMaster_Name = 'MaximumPromotionalTicketsCount'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '0'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'MaximumPromotionalTicketsCount')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'MaximumPromotionalTicketAmount'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '0' FROM SettingsMaster WHERE SettingsMaster_Name = 'MaximumPromotionalTicketAmount'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '0'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'MaximumPromotionalTicketAmount')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DefaultPromotionalTicketExpireDays'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '0' FROM SettingsMaster WHERE SettingsMaster_Name = 'DefaultPromotionalTicketExpireDays'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '0'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DefaultPromotionalTicketExpireDays')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'MonthToDateEnabled'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'MonthToDateEnabled'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'MonthToDateEnabled')
    
IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableCashdeskReconciliation'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableCashdeskReconciliation'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableCashdeskReconciliation')

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableCashdeskMovement'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableCashdeskMovement'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableCashdeskMovement')
    
IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableSystemBalancing'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableSystemBalancing'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'False'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EnableSystemBalancing')    
GO
IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TISCommunicationMode'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'BOTH' FROM SettingsMaster WHERE SettingsMaster_Name = 'TISCommunicationMode'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'BOTH'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TISCommunicationMode')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowTISRedemptionInCDO'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowTISRedemptionInCDO'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowTISRedemptionInCDO')
GO
IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowTISVoidInCDO'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowTISVoidInCDO'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowTISVoidInCDO')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CentralizedVaultDeclaration'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'CentralizedVaultDeclaration'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CentralizedVaultDeclaration')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'VaultStandardFillAmount'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '1000' FROM SettingsMaster WHERE SettingsMaster_Name = 'VaultStandardFillAmount'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '1000'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'VaultStandardFillAmount')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DisplayGameNameInFloorView'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'DisplayGameNameInFloorView'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'DisplayGameNameInFloorView')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsBillCounterAmountEditable'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsBillCounterAmountEditable'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsBillCounterAmountEditable')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ReceiveTimestampsFromTISInUTC'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'ReceiveTimestampsFromTISInUTC'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ReceiveTimestampsFromTISInUTC')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SendTimestampsToTISInUTC'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'SendTimestampsToTISInUTC'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SendTimestampsToTISInUTC')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'VaultAlertSource'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'BALLY' FROM SettingsMaster WHERE SettingsMaster_Name = 'VaultAlertSource'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'BALLY'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'VaultAlertSource')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowedVaultEventsToSTM'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, '1000' FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowedVaultEventsToSTM'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = '1000'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowedVaultEventsToSTM')
GO


--IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CMPMode'))
--    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
--    SELECT 1, SettingsMaster_ID, 'WebService' FROM SettingsMaster WHERE SettingsMaster_Name = 'CMPMode'
    
--ELSE
--    UPDATE [SettingsProfileItems]
--    SET    SettingsProfileItems_SettingsMaster_Values = 'WebService'
--    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CMPMode')
--GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowVaultPrintMessage'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowVaultPrintMessage'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowVaultPrintMessage')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowVaultConfirmMessage'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowVaultConfirmMessage'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowVaultConfirmMessage')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowVaultSuccessMessage'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowVaultSuccessMessage'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowVaultSuccessMessage')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AutoFillDeclaredAmount'))
    INSERT [SettingsProfileItems] ( SettingsProfileItems_SettingsProfile_ID, SettingsProfileItems_SettingsMaster_ID, SettingsProfileItems_SettingsMaster_Values )
    SELECT 1, SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'AutoFillDeclaredAmount'
ELSE
    UPDATE [SettingsProfileItems]
    SET    SettingsProfileItems_SettingsMaster_Values = 'True'
    WHERE  SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AutoFillDeclaredAmount')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsCommonCDODeclarationEnabled'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsCommonCDODeclarationEnabled'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'False'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsCommonCDODeclarationEnabled')
GO	

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EmployeeCardSTMAlertEventType'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'Fault,Comms,Door,Power' FROM SettingsMaster WHERE SettingsMaster_Name = 'EmployeeCardSTMAlertEventType'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'Fault,Comms,Door,Power'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EmployeeCardSTMAlertEventType')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EmployeeCardSTMAlertCardIN_OUT'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'EmployeeCardSTMAlertCardIN_OUT'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'True'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EmployeeCardSTMAlertCardIN_OUT')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsSiteLicensingEnabled'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsSiteLicensingEnabled'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'True'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsSiteLicensingEnabled')
GO	


IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowManualKeyboard'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowManualKeyboard'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'True'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowManualKeyboard')
GO	

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ManualCashEntryEnableZero'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'ManualCashEntryEnableZero'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'False'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ManualCashEntryEnableZero')
GO	

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowSystemCalendar'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowSystemCalendar'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'False'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowSystemCalendar')
GO		

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EBSLastMessageId_Recv'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, '0' FROM SettingsMaster WHERE SettingsMaster_Name = 'EBSLastMessageId_Recv'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = '0'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EBSLastMessageId_Recv')
GO	

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EBSLastMessageId_Send'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, '0' FROM SettingsMaster WHERE SettingsMaster_Name = 'EBSLastMessageId_Send'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = '0'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EBSLastMessageId_Send')
GO

--For Cashier History

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CDMShowAllActiveAsLiable'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'CDMShowAllActiveAsLiable'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'False'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CDMShowAllActiveAsLiable')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CDMIgnoreDeviceForGeneral'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'CDMIgnoreDeviceForGeneral'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'True'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'CDMIgnoreDeviceForGeneral')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PrintHeaderFormat'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'Site_Address_1;Site_Address_2;Site_PostCode' FROM SettingsMaster WHERE SettingsMaster_Name = 'PrintHeaderFormat'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'Site_Address_1;Site_Address_2;Site_PostCode'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'PrintHeaderFormat')
GO	

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsMultipleVoucherRedemptionEnabled'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID,'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsMultipleVoucherRedemptionEnabled'
	
ELSE
	
	UPDATE SettingsProfileItems
	SET SettingsProfileItems_SettingsMaster_Values = 'True'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID =(SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsMultipleVoucherRedemptionEnabled')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketAnomaliesEnabled'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID,'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketAnomaliesEnabled'
	
ELSE
	
	UPDATE SettingsProfileItems
	SET SettingsProfileItems_SettingsMaster_Values = 'False'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID =(SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'TicketAnomaliesEnabled')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AutoCalcDecTicketsOnExport'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'AutoCalcDecTicketsOnExport'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'False'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AutoCalcDecTicketsOnExport')
GO

GO
IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Hourly_DefaultItem'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'DROP' FROM SettingsMaster WHERE SettingsMaster_Name = 'Hourly_DefaultItem'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'DROP'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'Hourly_DefaultItem')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'OddRowColor'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, '#bbbbff' FROM SettingsMaster WHERE SettingsMaster_Name = 'OddRowColor'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = '#bbbbff'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'OddRowColor')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EvenRowColor'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'Transperant' FROM SettingsMaster WHERE SettingsMaster_Name = 'EvenRowColor'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'Transperant'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'EvenRowColor')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ReportDateTimeFormat'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'dd-MMM-yyyy HH:mm:ss' FROM SettingsMaster WHERE SettingsMaster_Name = 'ReportDateTimeFormat'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'dd-MMM-yyyy HH:mm:ss'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ReportDateTimeFormat')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ReportDateFormat'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'dd-MMM-yyyy' FROM SettingsMaster WHERE SettingsMaster_Name = 'ReportDateFormat'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'dd-MMM-yyyy'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ReportDateFormat')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ReportPrintDateTimeFormat'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'dd-MMM-yyyy HH:mm:ss' FROM SettingsMaster WHERE SettingsMaster_Name = 'ReportPrintDateTimeFormat'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'dd-MMM-yyyy HH:mm:ss'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ReportPrintDateTimeFormat')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ReportDataDateAloneFormat'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'dd-MMM-yyyy' FROM SettingsMaster WHERE SettingsMaster_Name = 'ReportDataDateAloneFormat'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'dd-MMM-yyyy'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ReportDataDateAloneFormat')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ReportDataDateNTimeFormat'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'dd-MMM-yyyy HH:mm:ss' FROM SettingsMaster WHERE SettingsMaster_Name = 'ReportDataDateNTimeFormat'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'dd-MMM-yyyy HH:mm:ss'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ReportDataDateNTimeFormat')
GO


IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = 
(SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsAlertEnabled'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsAlertEnabled'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'True'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = 
	(SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsAlertEnabled')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND 
SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsEmailAlertEnabled'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsEmailAlertEnabled'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'True'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = 
	(SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsEmailAlertEnabled')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IncludeRareBills'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'IncludeRareBills'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'False'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IncludeRareBills')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND 
SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SendMailFromExchange'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'SendMailFromExchange'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'False'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = 
	(SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'SendMailFromExchange')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND 
SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsSiteProfileEnabled'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsSiteProfileEnabled'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'True'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = 
	(SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsSiteProfileEnabled')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND 
SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsEmailAlertEnabled'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsEmailAlertEnabled'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'False'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = 
	(SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsEmailAlertEnabled')
GO


IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND 
SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsAutoCalendarEnabled'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'IsAutoCalendarEnabled'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'False'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = 
	(SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'IsAutoCalendarEnabled')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND 
SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowMultipleDrops'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowMultipleDrops'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'False'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = 
	(SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AllowMultipleDrops')
GO


IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ClearHandpayTilt'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'ClearHandpayTilt'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'False'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ClearHandpayTilt')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AddShortpayCommentstoDefault'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'False' FROM SettingsMaster WHERE SettingsMaster_Name = 'AddShortpayCommentstoDefault'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'False'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AddShortpayCommentstoDefault')
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowBatchWinLossOnDeclaration'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowBatchWinLossOnDeclaration'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'True'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowBatchWinLossOnDeclaration')
GO


IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowCollectionReport'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowCollectionReport'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'True'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowCollectionReport')
GO


IF NOT EXISTS(SELECT 1 FROM [SettingsProfileItems] WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowVarianceReport'))
	INSERT [SettingsProfileItems] (SettingsProfileItems_SettingsProfile_ID,SettingsProfileItems_SettingsMaster_ID,SettingsProfileItems_SettingsMaster_Values)
	SELECT 1,SettingsMaster_ID, 'True' FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowVarianceReport'
ELSE
	UPDATE [SettingsProfileItems]
	SET SettingsProfileItems_SettingsMaster_Values = 'True'
	WHERE SettingsProfileItems_SettingsProfile_ID = '1' AND SettingsProfileItems_SettingsMaster_ID = (SELECT TOP 1 SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'ShowVarianceReport')
GO

-----For Including New Profile Items to all the profiles
INSERT INTO SettingsProfileItems
SELECT T.SettingsProfileItems_SettingsProfile_ID,
       T.SettingsProfileItems_SettingsMaster_ID,
       T.SettingsProfileItems_SettingsMaster_Values
FROM   (
           SELECT DISTINCT spi1.SettingsProfileItems_SettingsProfile_ID,
                  spi.SettingsProfileItems_SettingsMaster_ID,
                  spi.SettingsProfileItems_SettingsMaster_Values
           FROM   SettingsProfileItems spi WITH (NOLOCK),
                  SettingsProfileItems spi1 WITH (NOLOCK)
           WHERE  spi.SettingsProfileItems_SettingsProfile_ID = 1
                  AND spi1.SettingsProfileItems_SettingsProfile_ID <> 1
                  AND spi.SettingsProfileItems_SettingsMaster_ID <> spi1.SettingsProfileItems_SettingsMaster_ID
       ) AS T
       LEFT JOIN SettingsProfileItems spi2 WITH (NOLOCK)
            ON  T.SettingsProfileItems_SettingsProfile_ID = spi2.SettingsProfileItems_SettingsProfile_ID
            AND T.SettingsProfileItems_SettingsMaster_ID = spi2.SettingsProfileItems_SettingsMaster_ID
WHERE  spi2.SettingsProfileItems_ID IS NULL

GO

