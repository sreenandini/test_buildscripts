USE [Enterprise]
GO


IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'AFT_ENABLED')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'AFT_ENABLED', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'AFT_ENABLED'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Auto_Enable_Disable_Machines')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Auto_Enable_Disable_Machines', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'Auto_Enable_Disable_Machines'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Batch_Conf_Screen')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Batch_Conf_Screen', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'Batch_Conf_Screen'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CardNumberFormat')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CardNumberFormat', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CardNumberFormat'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'ConnexusPrintHandpayTimeDiff ')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'ConnexusPrintHandpayTimeDiff ', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'ConnexusPrintHandpayTimeDiff '

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'DailyRetry')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'DailyRetry', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'DailyRetry'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'DailyTry')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'DailyTry', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'DailyTry'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'DropRetry')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'DropRetry', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'DropRetry'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'EnableGMUMachine')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'EnableGMUMachine', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'EnableGMUMachine'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'EnableRedeemPrintCDO')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'EnableRedeemPrintCDO', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'EnableRedeemPrintCDO'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'GAMING_DAY_START_HOUR')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'GAMING_DAY_START_HOUR', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'GAMING_DAY_START_HOUR'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'HOURLY_STATS_CONS_DATA_NOOFDAYS')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'HOURLY_STATS_CONS_DATA_NOOFDAYS', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'HOURLY_STATS_CONS_DATA_NOOFDAYS'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'HourlyRetry')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'HourlyRetry', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'HourlyRetry'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'HourlyTry')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'HourlyTry', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'HourlyTry'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'InnerCardNumberFormat')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'InnerCardNumberFormat', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'InnerCardNumberFormat'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'NumberOfCharToTrim')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'NumberOfCharToTrim', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'NumberOfCharToTrim'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'PartialDetailsExport')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'PartialDetailsExport', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'PartialDetailsExport'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Polling_Timer')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Polling_Timer', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'Polling_Timer'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'TICKET_CREATE_TIMEOUT')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'TICKET_CREATE_TIMEOUT', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'TICKET_CREATE_TIMEOUT'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'EnableLaundering')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'EnableLaundering', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'EnableLaundering'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'TicketValidate_EnableVoucher')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'TicketValidate_EnableVoucher', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'TicketValidate_EnableVoucher'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'VoucherValidate_EnableRefillReceipt')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'VoucherValidate_EnableRefillReceipt', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'VoucherValidate_EnableRefillReceipt'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'TicketValidate_EnableRefundReceipt')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'TicketValidate_EnableRefundReceipt', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'TicketValidate_EnableRefundReceipt'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'TICKETVALIDATE_REQUIRES_HEADCASHIER_SIG')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'TICKETVALIDATE_REQUIRES_HEADCASHIER_SIG', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'TICKETVALIDATE_REQUIRES_HEADCASHIER_SIG'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'TICKETVALIDATE_REQUIRES_MANAGER_SIG')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'TICKETVALIDATE_REQUIRES_MANAGER_SIG', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'TICKETVALIDATE_REQUIRES_MANAGER_SIG'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'TicketValidate_EnableHandpayReceipt')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'TicketValidate_EnableHandpayReceipt', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'TicketValidate_EnableHandpayReceipt'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'TicketValidate_EnableIssueReceipt')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'TicketValidate_EnableIssueReceipt', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'TicketValidate_EnableIssueReceipt'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'TicketValidate_EnableShortpayReceipt')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'TicketValidate_EnableShortpayReceipt', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'TicketValidate_EnableShortpayReceipt'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'BusinessDayAdjustment')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'BusinessDayAdjustment', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'BusinessDayAdjustment'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'TICKET_DB_NAME')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'TICKET_DB_NAME', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'TICKET_DB_NAME'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'REGION')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'REGION', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'REGION'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'AddCashToExistingBreakdown')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'AddCashToExistingBreakdown', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'AddCashToExistingBreakdown'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'HandpayManualEntry')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'HandpayManualEntry', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'HandpayManualEntry'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'NoSerialNumberDisplayed')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'NoSerialNumberDisplayed', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'NoSerialNumberDisplayed'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'NoAltSerialNumberDisplayed')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'NoAltSerialNumberDisplayed', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'NoAltSerialNumberDisplayed'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'DailyAutoReadTime')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'DailyAutoReadTime', 'DB', 'Time Format should be ''00:00''(24 hrs)', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y',
    SettingsMaster_Description = 'Time Format should be ''00:00''(24 hrs)'
    WHERE  SettingsMaster_Name = 'DailyAutoReadTime'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'SAS_UpdateTicketDetails')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'SAS_UpdateTicketDetails', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'SAS_UpdateTicketDetails'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'PROMO_TICKET_CODE')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'PROMO_TICKET_CODE', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'PROMO_TICKET_CODE'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'SGVI_Batch_Net_Value')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'SGVI_Batch_Net_Value', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'SGVI_Batch_Net_Value'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'SGVI_Enabled')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'SGVI_Enabled', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'SGVI_Enabled'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'DB_VERSION')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'DB_VERSION', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'DB_VERSION'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Auto_Declare_Monies')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Auto_Declare_Monies', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'Auto_Declare_Monies'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'External_Connection')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'External_Connection', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'External_Connection'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'DOWNTIME_MINS_ALLOWED_FOR_ExTERNAL_CONNECTION')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'DOWNTIME_MINS_ALLOWED_FOR_ExTERNAL_CONNECTION', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'DOWNTIME_MINS_ALLOWED_FOR_ExTERNAL_CONNECTION'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'TIME_SINCE_LAST_EXTERNAL_CONNECTION')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'TIME_SINCE_LAST_EXTERNAL_CONNECTION', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'TIME_SINCE_LAST_EXTERNAL_CONNECTION'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'PT_GATEWAY_IP')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'PT_GATEWAY_IP', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'PT_GATEWAY_IP'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'PT_GATEWAY_PORT_NO')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'PT_GATEWAY_PORT_NO', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'PT_GATEWAY_PORT_NO'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'PT_GATEWAY_MSG_RESP_TIMEOUT')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'PT_GATEWAY_MSG_RESP_TIMEOUT', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'PT_GATEWAY_MSG_RESP_TIMEOUT'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Auto_Close_Door_Event')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Auto_Close_Door_Event', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'Auto_Close_Door_Event'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Auto_Close_Power_Event')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Auto_Close_Power_Event', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'Auto_Close_Power_Event'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Auto_Close_Comms_Event')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Auto_Close_Comms_Event', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'Auto_Close_Comms_Event'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Auto_Close_Fault_Event')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Auto_Close_Fault_Event', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'Auto_Close_Fault_Event'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'USE_DIRECTED_MESSAGING')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'USE_DIRECTED_MESSAGING', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'USE_DIRECTED_MESSAGING'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'EPIGatewayConnstrNA')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'EPIGatewayConnstrNA', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'EPIGatewayConnstrNA'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'HideLayoutOnDeclarationTab')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'HideLayoutOnDeclarationTab', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'HideLayoutOnDeclarationTab'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'UseJetscanNoteCounter')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'UseJetscanNoteCounter', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'UseJetscanNoteCounter'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'LastFailedEventsSentTime')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'LastFailedEventsSentTime', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'LastFailedEventsSentTime'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Allow_Offline_Redeem')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Allow_Offline_Redeem', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'Allow_Offline_Redeem'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'TicketValidation_FillScreen')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'TicketValidation_FillScreen', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'TicketValidation_FillScreen'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'VoidTransactions')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'VoidTransactions', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'VoidTransactions'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CD_NOT_USE_HOPPERS')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CD_NOT_USE_HOPPERS', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CD_NOT_USE_HOPPERS'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CD_NOT_ISSUE_VOUCHER')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CD_NOT_ISSUE_VOUCHER', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'CD_NOT_ISSUE_VOUCHER'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CD_TITO_NOT_IN_USE')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CD_TITO_NOT_IN_USE', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CD_TITO_NOT_IN_USE'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CD_SP_NOT_IN_USE')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CD_SP_NOT_IN_USE', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CD_SP_NOT_IN_USE'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'USE_ON_SCREEN_KEYBOARD')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'USE_ON_SCREEN_KEYBOARD', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'USE_ON_SCREEN_KEYBOARD'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'VATRate')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'VATRate', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'VATRate'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CDM_SHOW_COIN_HOPPER')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CDM_SHOW_COIN_HOPPER', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CDM_SHOW_COIN_HOPPER'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'SETTING_NAMES_HIDDEN')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'SETTING_NAMES_HIDDEN', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'SETTING_NAMES_HIDDEN'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'EVENTTYPELIST')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'EVENTTYPELIST', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'EVENTTYPELIST'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Allow_POP_Update')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Allow_POP_Update', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'Allow_POP_Update'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Confirm_Redeem_Message')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Confirm_Redeem_Message', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'Confirm_Redeem_Message'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'VerificationTimes')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'VerificationTimes', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'VerificationTimes'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'HandpayPayoutCustomer_Min')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'HandpayPayoutCustomer_Min', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'HandpayPayoutCustomer_Min'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'HandpayPayoutCustomer_Max')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'HandpayPayoutCustomer_Max', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'HandpayPayoutCustomer_Max'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'HandpayPayoutCustomer_BankAccNo')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'HandpayPayoutCustomer_BankAccNo', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'HandpayPayoutCustomer_BankAccNo'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'SlotLifeToDate')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'SlotLifeToDate', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'SlotLifeToDate'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'RedeemTicketCustomer_Min')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'RedeemTicketCustomer_Min', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'RedeemTicketCustomer_Min'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'RedeemTicketCustomer_Max')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'RedeemTicketCustomer_Max', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'RedeemTicketCustomer_Max'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'RedeemTicketCustomer_BankAcctNo')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'RedeemTicketCustomer_BankAcctNo', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'RedeemTicketCustomer_BankAcctNo'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CheckExchangeServerConnectivity')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CheckExchangeServerConnectivity', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CheckExchangeServerConnectivity'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'ExchangeServerConnectivityCheckInterval')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'ExchangeServerConnectivityCheckInterval', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'ExchangeServerConnectivityCheckInterval'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'AFTTransactionsAllowed')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'AFTTransactionsAllowed', 'AFT', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'AFTTransactionsAllowed'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'AllowCashableDeposits')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'AllowCashableDeposits', 'AFT', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'AllowCashableDeposits'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Allow Non-Cashable Deposits')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Allow Non-Cashable Deposits', 'AFT', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'Allow Non-Cashable Deposits'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'AllowRedeemOffers')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'AllowRedeemOffers', 'AFT', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'AllowRedeemOffers'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'AllowPointsWithdrawal')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'AllowPointsWithdrawal', 'AFT', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'AllowPointsWithdrawal'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'AllowCashWithdrawal')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'AllowCashWithdrawal', 'AFT', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'AllowCashWithdrawal'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'AllowPartialTransfers')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'AllowPartialTransfers', 'AFT', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'AllowPartialTransfers'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'AutoDepositNon-CashableCreditsonCardOut')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'AutoDepositNon-CashableCreditsonCardOut', 'AFT', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'AutoDepositNon-CashableCreditsonCardOut'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'AutoDepositCashableCreditsonCardOut')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'AutoDepositCashableCreditsonCardOut', 'AFT', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'AutoDepositCashableCreditsonCardOut'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'AllowOffers')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'AllowOffers', 'AFT', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'AllowOffers'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'EFTTimeoutValue')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'EFTTimeoutValue', 'AFT', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'EFTTimeoutValue'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Option1WithdrawalAmount')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Option1WithdrawalAmount', 'AFT', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'Option1WithdrawalAmount'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Option2WithdrawalAmount')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Option2WithdrawalAmount', 'AFT', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'Option2WithdrawalAmount'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Option3WithdrawalAmount')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Option3WithdrawalAmount', 'AFT', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'Option3WithdrawalAmount'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Option4WithdrawalAmount')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Option4WithdrawalAmount', 'AFT', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'Option4WithdrawalAmount'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Option5WithdrawalAmount')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Option5WithdrawalAmount', 'AFT', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'Option5WithdrawalAmount'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'MaxDeposit Amount')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'MaxDeposit Amount', 'AFT', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'MaxDeposit Amount'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'MaxWithDraw Amount')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'MaxWithDraw Amount', 'AFT', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'MaxWithDraw Amount'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'IssueTicketMaxValue')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'IssueTicketMaxValue', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'IssueTicketMaxValue'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Issue_Ticket_Encrypt_BarCode')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Issue_Ticket_Encrypt_BarCode', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'Issue_Ticket_Encrypt_BarCode'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Ithaca950')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Ithaca950', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'Ithaca950'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'PrinterPort')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'PrinterPort', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'PrinterPort'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Login_Expiry_No_of_Days')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Login_Expiry_No_of_Days', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'Login_Expiry_No_of_Days'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Login_Max_No_Of_Attempts')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Login_Max_No_Of_Attempts', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'Login_Max_No_Of_Attempts'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'MaxHandPayAuthRequired')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'MaxHandPayAuthRequired', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'MaxHandPayAuthRequired'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'ManualEntryTicketValidation')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'ManualEntryTicketValidation', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'ManualEntryTicketValidation'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'RedeemExpiredTicket')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'RedeemExpiredTicket', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'RedeemExpiredTicket'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Instant_Periodic_Interval')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Instant_Periodic_Interval', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'Instant_Periodic_Interval'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'ClearEventsOnFinalDrop')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'ClearEventsOnFinalDrop', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'ClearEventsOnFinalDrop'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Voucher_Printer_Name')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Voucher_Printer_Name', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'Voucher_Printer_Name'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CMPAPP_UNAME')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CMPAPP_UNAME', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CMPAPP_UNAME'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CMPAPP_PWD')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CMPAPP_PWD', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CMPAPP_PWD'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CMP_KIOSKURL')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CMP_KIOSKURL', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CMP_KIOSKURL'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'AUTOLOGOFF_TIMEOUT')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'AUTOLOGOFF_TIMEOUT', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'AUTOLOGOFF_TIMEOUT'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Auto_Close_General_Event')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Auto_Close_General_Event', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'Auto_Close_General_Event'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'DISABLE_MACHINE_ON_DROP')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'DISABLE_MACHINE_ON_DROP', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'DISABLE_MACHINE_ON_DROP'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'LiveMeter')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'LiveMeter', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'LiveMeter'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'TicketDeclarationMethod')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'TicketDeclarationMethod', 'DB', 'Drop_Auto_Ticket_Declaration', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'TicketDeclarationMethod'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'TIMEZONENAME')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'TIMEZONENAME', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'TIMEZONENAME'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'BillVoucherCounterCOMPort')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'BillVoucherCounterCOMPort', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'BillVoucherCounterCOMPort'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'IsAFTIncludedInCalculation')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'IsAFTIncludedInCalculation', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'IsAFTIncludedInCalculation'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'TicketValidate_EnableProgressivePayoutReceipt')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'TicketValidate_EnableProgressivePayoutReceipt', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'TicketValidate_EnableProgressivePayoutReceipt'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'IsAftEnabledForSite')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'IsAftEnabledForSite', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'IsAftEnabledForSite'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'NoWaitForDisableMachine')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'NoWaitForDisableMachine', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'NoWaitForDisableMachine'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'DisableOnExchangeTimeNotInSync')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'DisableOnExchangeTimeNotInSync', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'DisableOnExchangeTimeNotInSync'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'SyncDateTimeEnabled')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'SyncDateTimeEnabled', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'SyncDateTimeEnabled'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'TICKET_EXPIRE')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'TICKET_EXPIRE', 'DB', 'Enter the number of days less an year and should be integer', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y',
    SettingsMaster_Description = 'Enter the number of days less an year and should be integer'
    WHERE  SettingsMaster_Name = 'TICKET_EXPIRE'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'W2GMessage')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'W2GMessage', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'W2GMessage'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'W2GWinAmount')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'W2GWinAmount', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'W2GWinAmount'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'COPYRIGTINFO')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'COPYRIGTINFO', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'COPYRIGTINFO'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'PRODUCTVERSION')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'PRODUCTVERSION', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'PRODUCTVERSION'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'PRODUCTDESC')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'PRODUCTDESC', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'PRODUCTDESC'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'COMPANYNAME')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'COMPANYNAME', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'COMPANYNAME'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'PRODUCTNAME')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'PRODUCTNAME', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'PRODUCTNAME'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'HandPayBeepEnabled')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'HandPayBeepEnabled', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'HandPayBeepEnabled'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'System_Parameter_Stock_Code_Prefix')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'System_Parameter_Stock_Code_Prefix', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'System_Parameter_Stock_Code_Prefix'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CheckForStockPrefix')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CheckForStockPrefix', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'CheckForStockPrefix'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Handpay_Wav_Path')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Handpay_Wav_Path', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'Handpay_Wav_Path'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CLIENT')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CLIENT', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'CLIENT'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'SHOWHANDPAYCODE')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'SHOWHANDPAYCODE', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'SHOWHANDPAYCODE'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'PokerGamePrefix')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'PokerGamePrefix', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'PokerGamePrefix'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'IsKioskRequired')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'IsKioskRequired', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'IsKioskRequired'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CAGE_ALLOWCASHIERLOCONTKTS')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CAGE_ALLOWCASHIERLOCONTKTS', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CAGE_ALLOWCASHIERLOCONTKTS'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CAGE_ALLOWPRINTTKTOVERRIDE')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CAGE_ALLOWPRINTTKTOVERRIDE', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CAGE_ALLOWPRINTTKTOVERRIDE'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CAGE_TKTPRINTERENABLED')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CAGE_TKTPRINTERENABLED', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CAGE_TKTPRINTERENABLED'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CAGE_MINTKTPRINTAMTFOREMP')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CAGE_MINTKTPRINTAMTFOREMP', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CAGE_MINTKTPRINTAMTFOREMP'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CAGE_MAXTKTREDEMPTIONAMTFOREMP')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CAGE_MAXTKTREDEMPTIONAMTFOREMP', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CAGE_MAXTKTREDEMPTIONAMTFOREMP'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CAGE_MAXTKTREDEMPTIONAMT')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CAGE_MAXTKTREDEMPTIONAMT', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CAGE_MAXTKTREDEMPTIONAMT'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CAGE_MAXNOOFTKTPRINTLIMIT')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CAGE_MAXNOOFTKTPRINTLIMIT', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CAGE_MAXNOOFTKTPRINTLIMIT'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CAGE_MAXDAILYCASHIERGENTKTAMT')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CAGE_MAXDAILYCASHIERGENTKTAMT', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CAGE_MAXDAILYCASHIERGENTKTAMT'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CAGE_MAXTKTPRINTAMTFOREMP')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CAGE_MAXTKTPRINTAMTFOREMP', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CAGE_MAXTKTPRINTAMTFOREMP'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CAGE_ENABLED')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CAGE_ENABLED', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'CAGE_ENABLED'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'REDEEM NA')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'REDEEM NA', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'REDEEM NA'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'IsMachineBasedDropDeclaration')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'IsMachineBasedDropDeclaration', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'IsMachineBasedDropDeclaration'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'HANDLE_EXCEPTIONTICKETS')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'HANDLE_EXCEPTIONTICKETS', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'HANDLE_EXCEPTIONTICKETS'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'HANDLE_EXCEPTIONTICKETS_COUNTER')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'HANDLE_EXCEPTIONTICKETS_COUNTER', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'HANDLE_EXCEPTIONTICKETS_COUNTER'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CashDispenserEnabled')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CashDispenserEnabled', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'CashDispenserEnabled'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'AutoCashDispenseRequired')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'AutoCashDispenseRequired', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'AutoCashDispenseRequired'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CashDispenserDenominations')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CashDispenserDenominations', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'CashDispenserDenominations'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'SendPT10FromClient')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'SendPT10FromClient', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'SendPT10FromClient'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'TicketValidate_EnableCustomerReceipt')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'TicketValidate_EnableCustomerReceipt', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'TicketValidate_EnableCustomerReceipt'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'SendAcktoGateway')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'SendAcktoGateway', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'SendAcktoGateway'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'IsShortPayEnabled')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'IsShortPayEnabled', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'IsShortPayEnabled'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Declaration_ShowoutValues')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Declaration_ShowoutValues', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'Declaration_ShowoutValues'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CentralizedDeclaration')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CentralizedDeclaration', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'CentralizedDeclaration'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'StackerLevelAlert')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'StackerLevelAlert', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'StackerLevelAlert'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CashDispenserType')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CashDispenserType', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'CashDispenserType'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'ExportVaultEvents')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'ExportVaultEvents', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'ExportVaultEvents'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'DropAlert')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'DropAlert', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'DropAlert'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'DeclarationAlert')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'DeclarationAlert', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'DeclarationAlert'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'ECash_Points')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'ECash_Points', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'ECash_Points'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'IsSingleCardEmployee')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'IsSingleCardEmployee', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'IsSingleCardEmployee'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'MaxNoOfCardsForEmployee')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'MaxNoOfCardsForEmployee', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'MaxNoOfCardsForEmployee'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'AutoDropEnabled')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'AutoDropEnabled', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'AutoDropEnabled'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'ForceManualDrop')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'ForceManualDrop', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'ForceManualDrop'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'StackerFeature')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'StackerFeature', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'StackerFeature'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'StackerLevelTracking')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'StackerLevelTracking', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'StackerLevelTracking'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'IsEmployeeCardTrackingEnabled')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'IsEmployeeCardTrackingEnabled', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'IsEmployeeCardTrackingEnabled'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'AddShortpayInVoucherOut')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'AddShortpayInVoucherOut', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'AddShortpayInVoucherOut'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'LiquidationType')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'LiquidationType', 'DB', 'Speifies the type of Liquidation (Collection or Read).', 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'LiquidationType'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'ExpenseShare')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'ExpenseShare', 'DB', 'Enable or Disable Expense Share in Liquidation.', 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'ExpenseShare'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'WriteOffShare')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'WriteOffShare', 'DB', 'Enable or Disable WriteOff in Liquidation.', 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'WriteOffShare'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'LiquidationProfitShare')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'LiquidationProfitShare', 'DB', 'Enable or Disable Liquidation Profit Share functionality.', 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'LiquidationProfitShare'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CentralizedReadLiquidation')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CentralizedReadLiquidation', 'DB', 'Enable or Disable Centralized ReadLiquidation for Profit Share.', 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CentralizedReadLiquidation'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'PROCESS_HANDPAY_ON_DROP')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'PROCESS_HANDPAY_ON_DROP', 'DB', 'Manual or Auto process of handpay on drop functionality.', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'PROCESS_HANDPAY_ON_DROP'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'IsRouteEnable')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'IsRouteEnable', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'IsRouteEnable'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'NotesCounter_AutoStart')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'NotesCounter_AutoStart', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'NotesCounter_AutoStart'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Dec_AutoMarkActiveTicketsAsPD')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Dec_AutoMarkActiveTicketsAsPD', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'Dec_AutoMarkActiveTicketsAsPD'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Dec_AutoMarkExceptionTicketAsPD')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Dec_AutoMarkExceptionTicketAsPD', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'Dec_AutoMarkExceptionTicketAsPD'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Are200and500BillsRequired')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Are200and500BillsRequired', 'DB', NULL, 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'Are200and500BillsRequired'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'HourlyBasedOnCalendarDay')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'HourlyBasedOnCalendarDay', 'DB', 'Speifies whether the Hourly screen will be displayed based upon either gaming day or calendar day.', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'HourlyBasedOnCalendarDay'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'ServiceNames')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'ServiceNames', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'ServiceNames'
    
--PART COLLECTION
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'IsPartCollectionEnabled')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'IsPartCollectionEnabled', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'IsPartCollectionEnabled'

--Vault Alert
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'VaultAlert')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'VaultAlert', 'DB', 'Specified whether Vault Alert need to sent STM', 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N',
		   SettingsMaster_Description = 'Specified whether Vault Alert need to sent STM'
    WHERE  SettingsMaster_Name = 'VaultAlert'


--Route Manager
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'IsRouteEnabled')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'IsRouteEnabled', 'DB', 'Specifies whether Route Manager is Enabled', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y',
		   SettingsMaster_Description = 'Specifies whether Route Manager is Enabled'
    WHERE  SettingsMaster_Name = 'IsRouteEnabled'
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'Allow_Machine_Removal')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'Allow_Machine_Removal', 'DB', 'Allow Machine Removal', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y',
		   SettingsMaster_Description = 'Allow Machine Removal'
    WHERE  SettingsMaster_Name = 'Allow_Machine_Removal'


    
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'VoidExpiredTicket')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'VoidExpiredTicket', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'VoidExpiredTicket'
    
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'IsMachineBasedAutoDrop')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'IsMachineBasedAutoDrop', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'IsMachineBasedAutoDrop'
    
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'ProcessW2GAmount')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'ProcessW2GAmount', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'ProcessW2GAmount'
    
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'VoidVouchers')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'VoidVouchers', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'VoidVouchers'



IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'WeeklyReport')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'WeeklyReport', 'DB', NULL, 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'WeeklyReport'


IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'ServiceNotRunningInterval')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'ServiceNotRunningInterval', 'DB', 'Give value in seconds', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'ServiceNotRunningInterval'
    
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'ValidateGMUInSite')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'ValidateGMUInSite', 'DB', 'Validate GMU In Site while enrolling ', 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'ValidateGMUInSite'
 
  
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'DefaultGMUValue')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'DefaultGMUValue', 'DB', 'Default GMU Value is 16 ', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'DefaultGMUValue'
    
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'ShortPayAuthorizationRequired')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'ShortPayAuthorizationRequired', 'DB', 'ShortPay Authorization Limit', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'ShortPayAuthorizationRequired'

GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'ShortPayAuthorizationLimit')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'ShortPayAuthorizationLimit', 'DB', 'ShortPay Authorization Limit', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'ShortPayAuthorizationLimit'
    
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'SlotNumberIsAsset')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'SlotNumberIsAsset', 'DB', 'Slot Number is asset or bar position', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'SlotNumberIsAsset'
    
 IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'StandIsPosition')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'StandIsPosition', 'DB', 'Stand is position or zone', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'StandIsPosition'
    
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'IsFinalDropRequiredForRemoval')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'IsFinalDropRequiredForRemoval', 'DB', 'true:remove-reinstate btn will be disable', 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'IsFinalDropRequiredForRemoval'
    
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'EnableCounterInManualCashEntry')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'EnableCounterInManualCashEntry', 'DB', 'Enable counter in manual CashEntry screen', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
     WHERE  SettingsMaster_Name = 'EnableCounterInManualCashEntry'

GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'SHOW_NAME_IN_RECEPIT_SIGNATURE')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'SHOW_NAME_IN_RECEPIT_SIGNATURE', 'DB', 'Show Name in Receipt Signature', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'SHOW_NAME_IN_RECEPIT_SIGNATURE'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'MonthToDateEnabled')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'MonthToDateEnabled', 'DB', 'Visible Month To Date in Reports', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'MonthToDateEnabled'    
    
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'EnableCashdeskReconciliation')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'EnableCashdeskReconciliation', 'DB', 'Visible Cashdesk Reconciliation Report Button', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'EnableCashdeskReconciliation'
    
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'EnableCashdeskMovement')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'EnableCashdeskMovement', 'DB', 'Visible Cashdesk Movement Report Button', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'EnableCashdeskMovement'

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'EnableSystemBalancing')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'EnableSystemBalancing', 'DB', 'Visible System Balancing Report Button', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'EnableSystemBalancing'  
GO    

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'IncludeVoucherClaimedInSlotInCDMReport')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'IncludeVoucherClaimedInSlotInCDMReport', 'DB', 'Include Voucher claimed in slot in Cash Desk Movement Report', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'IncludeVoucherClaimedInSlotInCDMReport'
GO
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'IsPromotionalTicketEnabled')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'IsPromotionalTicketEnabled', 'DB', 'Enable or disable Promotional Tickets from BMC level', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'IsPromotionalTicketEnabled'
GO
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'IsTISEnabled')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'IsTISEnabled', 'DB', 'Enable or disable TIS Promotional Tickets from BMC level', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'IsTISEnabled'
GO
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'IsGameCappingEnabled')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'IsGameCappingEnabled', 'DB', 'Enabled/Disable GameCapping', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'IsGameCappingEnabled'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'PreCommitmentEnabled')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'PreCommitmentEnabled', 'DB', 'Specifies whether Pre Commitment enabled or not', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'PreCommitmentEnabled'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'PreCommitmentRatingBasis')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'PreCommitmentRatingBasis', 'DB', 'Specifies whether Pre Commitment rating interval based on Time or HandlePulls', 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'PreCommitmentRatingBasis'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'IsExtendedPlayer')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'IsExtendedPlayer', 'DB', 'Specifies whether extended player or not. It should match with CMP Extended player config.', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'IsExtendedPlayer'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'BreakPeriodInterval')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'BreakPeriodInterval', 'DB', 'Specifies Break Period(In Seconds) to player on Pre Commitment Enabled. ', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'BreakPeriodInterval'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'BreakPeriodMessage')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'BreakPeriodMessage', 'DB', 'Specifies the Break Period message on Break Period interval in iView. ', 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'BreakPeriodMessage'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'BreakPeriodFadeOutTime')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'BreakPeriodFadeOutTime', 'DB', 'Specifies the Break Period fade out time in seconds. ', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'BreakPeriodFadeOutTime'
GO


IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'MaximumPromotionalTicketsCount')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'MaximumPromotionalTicketsCount', 'DB', 'Maximum PromotionalTickets Count. Please Enter Integer Value', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y' , SettingsMaster_Description='Maximum PromotionalTickets Count. Please Enter Integer Value'
    WHERE  SettingsMaster_Name = 'MaximumPromotionalTicketsCount'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'MaximumPromotionalTicketAmount')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'MaximumPromotionalTicketAmount', 'DB', 'Maximum Amount of each Promotional Ticket', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'MaximumPromotionalTicketAmount'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'DefaultPromotionalTicketExpireDays')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'DefaultPromotionalTicketExpireDays', 'DB', 'Promotional Ticket expiry days from current date.', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'DefaultPromotionalTicketExpireDays'
GO
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'TISCommunicationMode')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'TISCommunicationMode', 'DB', 'TIS Communication Mode(BOTH,WEBSERVICE OR SOCKET) .', 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'TISCommunicationMode'
	
	
GO
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'AllowTISRedemptionInCDO')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'AllowTISRedemptionInCDO', 'DB', 'Allow TIS Redemption from CDO.', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'AllowTISRedemptionInCDO'
GO
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'AllowTISVoidInCDO')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'AllowTISVoidInCDO', 'DB', 'Allow TIS Void from CDO.', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'AllowTISVoidInCDO'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CentralizedVaultDeclaration')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CentralizedVaultDeclaration', 'DB', 'Enable Centralized Vault Declaration.', 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CentralizedVaultDeclaration'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'VaultStandardFillAmount')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'VaultStandardFillAmount', 'DB', 'Standard Vault Fill Amount.', 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'VaultStandardFillAmount'

GO
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'DisplayGameNameInFloorView')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'DisplayGameNameInFloorView', 'DB', 'Display Game name in Floor View', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'DisplayGameNameInFloorView'

GO
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'IsBillCounterAmountEditable')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'IsBillCounterAmountEditable', 'DB', 'Bill Counter Amount field is Editable', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'IsBillCounterAmountEditable'
GO

GO
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'ReceiveTimestampsFromTISInUTC')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'ReceiveTimestampsFromTISInUTC', 'DB', 'Receive Timestamps From TIS In UTC', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'ReceiveTimestampsFromTISInUTC'
GO

GO
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'SendTimestampsToTISInUTC')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'SendTimestampsToTISInUTC', 'DB', 'Send Timestamps To TIS In UTC', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'SendTimestampsToTISInUTC'
GO
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'VaultAlertSource')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'VaultAlertSource', 'DB', 'Vault Alert Source', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'VaultAlertSource'
GO
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'AllowedVaultEventsToSTM')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'AllowedVaultEventsToSTM', 'DB', 'Allowed Vault Events To STM ', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'AllowedVaultEventsToSTM'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'CMPMode')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'CMPMode', 'DB', 'Set CMP Mode communication mode is Socket or WebService ', 'N'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'N'
    WHERE  SettingsMaster_Name = 'CMPMode'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'ShowVaultPrintMessage')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'ShowVaultPrintMessage', 'DB', 'Show Print Messages in Vault Screen', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'ShowVaultPrintMessage'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'ShowVaultConfirmMessage')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'ShowVaultConfirmMessage', 'DB', 'Show Confirmation Messages in Vault Screen', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'ShowVaultConfirmMessage'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'ShowVaultSuccessMessage')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'ShowVaultSuccessMessage', 'DB', 'Show Success Messages in Vault Screen', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'ShowVaultSuccessMessage'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster]WHERE SettingsMaster_Name = 'AutoFillDeclaredAmount')
    INSERT [SettingsMaster] ( SettingsMaster_Name, SettingsMaster_Type, SettingsMaster_Description, SettingsMaster_IsEnabled )
    SELECT 'AutoFillDeclaredAmount', 'DB', 'Fill declared amount in vault screen', 'Y'
ELSE
    UPDATE [SettingsMaster]
    SET    SettingsMaster_IsEnabled = 'Y'
    WHERE  SettingsMaster_Name = 'AutoFillDeclaredAmount'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'IsCommonCDODeclarationEnabled')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'IsCommonCDODeclarationEnabled','DB','Enable Common CDO Declaration','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'IsCommonCDODeclarationEnabled'
GO
	
IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'EmployeeCardSTMAlertEventType')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'EmployeeCardSTMAlertEventType','DB','STM Alter should be send for event type specified','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'EmployeeCardSTMAlertEventType'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'EmployeeCardSTMAlertCardIN_OUT')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'EmployeeCardSTMAlertCardIN_OUT','DB','STM Alter should be send for card in and out.','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'EmployeeCardSTMAlertCardIN_OUT'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'IsSiteLicensingEnabled')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'IsSiteLicensingEnabled','DB','Enable Site Licensing','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'IsSiteLicensingEnabled'
GO


IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'AllowManualKeyboard')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'AllowManualKeyboard','DB','Allow to Enter amount value by keyboard','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'AllowManualKeyboard'
GO	


IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'ManualCashEntryEnableZero')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'ManualCashEntryEnableZero','DB','Enable/disable default Zero','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'ManualCashEntryEnableZero'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'ShowSystemCalendar')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'ShowSystemCalendar','DB','ShowSystemCalendar','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'ShowSystemCalendar'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'EBSLastMessageId_Recv')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'EBSLastMessageId_Recv','DB','EBSLastMessageId_Recv','N'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'N'
	WHERE SettingsMaster_Name = 'EBSLastMessageId_Recv'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'EBSLastMessageId_Send')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'EBSLastMessageId_Send','DB','EBSLastMessageId_Send','N'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'N'
	WHERE SettingsMaster_Name = 'EBSLastMessageId_Send'
GO

-- For Cashier History

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'CDMShowAllActiveAsLiable')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'CDMShowAllActiveAsLiable','DB','CDMShowAllActiveAsLiable','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'CDMShowAllActiveAsLiable'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'CDMIgnoreDeviceForGeneral')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'CDMIgnoreDeviceForGeneral','DB','CDMIgnoreDeviceForGeneral','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'CDMIgnoreDeviceForGeneral'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'PrintHeaderFormat')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'PrintHeaderFormat','DB','Specify Column from Site Table (ex :Site_Address_1;Site_Address_2;Site_PostCode;Name)','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'PrintHeaderFormat'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name= 'IsMultipleVoucherRedemptionEnabled')
	INSERT [SettingsMaster] (SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description ,SettingsMaster_IsEnabled)
	SELECT 'IsMultipleVoucherRedemptionEnabled','DB','IsMultipleVoucherRedemptionEnabled','Y'
	
ELSE
	
	UPDATE [SettingsMaster] 
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'IsMultipleVoucherRedemptionEnabled'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name= 'TicketAnomaliesEnabled')
	INSERT [SettingsMaster] (SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description ,SettingsMaster_IsEnabled)
	SELECT 'TicketAnomaliesEnabled','DB','TicketAnomaliesEnabled','Y'
	
ELSE
	
	UPDATE [SettingsMaster] 
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'TicketAnomaliesEnabled'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'AutoCalcDecTicketsOnExport')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'AutoCalcDecTicketsOnExport','DB','Auto Calculate Declared Ticket on Export','N'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'N'
	WHERE SettingsMaster_Name = 'AutoCalcDecTicketsOnExport'
GO

GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'Hourly_DefaultItem')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'Hourly_DefaultItem','DB','HOURLY SCRREN DEFAULT SELECTION','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'Hourly_DefaultItem'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'OddRowColor')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'OddRowColor','DB','ODD ROW DEFAULT COLOR OF REPORTS','N'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'N'
	WHERE SettingsMaster_Name = 'OddRowColor'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'EvenRowColor')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'EvenRowColor','DB','EVEN ROW DEFAULT COLOR OF REPORTS','N'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'N'
	WHERE SettingsMaster_Name = 'EvenRowColor'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'ReportDateTimeFormat')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'ReportDateTimeFormat','DB','DATETIME FORMAT OF REPORTS','N'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'N'
	WHERE SettingsMaster_Name = 'ReportDateTimeFormat'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'ReportDateFormat')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'ReportDateFormat','DB','DATE FORMAT OF REPORTS','N'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'N'
	WHERE SettingsMaster_Name = 'ReportDateFormat'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'ReportPrintDateTimeFormat')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'ReportPrintDateTimeFormat','DB','PRINT DATETIME FORMAT OF REPORTS','N'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'N'
	WHERE SettingsMaster_Name = 'ReportPrintDateTimeFormat'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'ReportDataDateAloneFormat')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'ReportDataDateAloneFormat','DB','DATE ALONE FORMAT OF REPORTS','N'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'N'
	WHERE SettingsMaster_Name = 'ReportDataDateAloneFormat'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'ReportDataDateNTimeFormat')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'ReportDataDateNTimeFormat','DB','DATETIME FORMAT OF DATA OF REPORTS','N'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'N'
	WHERE SettingsMaster_Name = 'ReportDataDateNTimeFormat'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'IsAlertEnabled')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'IsAlertEnabled','DB','Is Alert Enabled','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'IsAlertEnabled'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'IsEmailAlertEnabled')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'IsEmailAlertEnabled','DB','Is Email Alert Enabled','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'IsEmailAlertEnabled'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'IncludeRareBills')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'IncludeRareBills','DB','Include Rare Bills 100p,10000p,20000p,50000p','N'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'N'
	WHERE SettingsMaster_Name = 'IncludeRareBills'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'SendMailFromExchange')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'SendMailFromExchange','DB','Send Email Alert from Exchange','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'SendMailFromExchange'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'IsSiteProfileEnabled')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'IsSiteProfileEnabled','DB','Site profile enabled','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'IsSiteProfileEnabled'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'IsEmailAlertEnabled')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'IsEmailAlertEnabled','DB','Email Alert enabled','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'IsEmailAlertEnabled'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'IsAutoCalendarEnabled')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'IsAutoCalendarEnabled','DB','Auto Calendar enabled','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'IsAutoCalendarEnabled'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'AllowMultipleDrops')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'AllowMultipleDrops','DB','Allow Multiple Drops','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'AllowMultipleDrops'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'ClearHandpayTilt')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'ClearHandpayTilt','DB','Clear handpay tilt when processed','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'ClearHandpayTilt'
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'AddShortpayCommentstoDefault')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'AddShortpayCommentstoDefault','DB','Add new shortpay comments to default list','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'AddShortpayCommentstoDefault'
GO


IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'ShowBatchWinLossOnDeclaration')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'ShowBatchWinLossOnDeclaration','DB','Show batch Win/Loss report on declaration complete and history screen','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'ShowBatchWinLossOnDeclaration'
GO


IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'ShowCollectionReport')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'ShowCollectionReport','DB','Show collection report on declaration complete and history  complete','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'ShowCollectionReport'
GO



IF NOT EXISTS(SELECT 1 FROM [SettingsMaster] WHERE SettingsMaster_Name = 'ShowVarianceReport')
	INSERT [SettingsMaster](SettingsMaster_Name,SettingsMaster_Type,SettingsMaster_Description,SettingsMaster_IsEnabled)
	SELECT 'ShowVarianceReport','DB','Show VarianceReport report on declaration complete and history  complete','Y'
ELSE
	UPDATE [SettingsMaster]
	SET SettingsMaster_IsEnabled = 'Y'
	WHERE SettingsMaster_Name = 'ShowVarianceReport'
GO

