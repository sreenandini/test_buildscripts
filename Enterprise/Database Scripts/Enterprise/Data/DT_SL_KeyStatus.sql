/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
 ************************************************************/
USE [Enterprise]
GO


IF NOT EXISTS(SELECT 1 FROM [SL_KeyStatus]WHERE KeyText = 'Created')
    INSERT [SL_KeyStatus] ( KeyText )
    SELECT 'Created'

IF NOT EXISTS(SELECT 1 FROM [SL_KeyStatus]WHERE KeyText = 'Active')
    INSERT [SL_KeyStatus] ( KeyText )
    SELECT 'Active'

IF NOT EXISTS(SELECT 1 FROM [SL_KeyStatus]WHERE KeyText = 'Expired')
    INSERT [SL_KeyStatus] ( KeyText )
    SELECT 'Expired'

IF NOT EXISTS(SELECT 1 FROM [SL_KeyStatus]WHERE KeyText = 'Cancelled')
    INSERT [SL_KeyStatus] ( KeyText )
    SELECT 'Cancelled'

GO