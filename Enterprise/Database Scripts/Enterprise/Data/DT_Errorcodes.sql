/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
 ************************************************************/
USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [Errorcodes]WHERE Error_Code = '5')
    INSERT [Errorcodes] ( Error_Type, Error_Code, Error_Description )
    SELECT 'REDEEM', 5, 'Game Confirm Time Out'
ELSE
    UPDATE [Errorcodes]
    SET    Error_Type = 'REDEEM', Error_Description = 'Game Confirm Time Out'
    WHERE  Error_Code = '5'

IF NOT EXISTS(SELECT 1 FROM [Errorcodes]WHERE Error_Code = '6')
    INSERT [Errorcodes] ( Error_Type, Error_Code, Error_Description )
    SELECT 'REDEEM', 6, 'Voucher Redeem Amount CRC Fail'
ELSE
    UPDATE [Errorcodes]
    SET    Error_Type = 'REDEEM', Error_Description = 'Voucher Redeem Amount CRC Fail'
    WHERE  Error_Code = '6'

IF NOT EXISTS(SELECT 1 FROM [Errorcodes]WHERE Error_Code = '36')
    INSERT [Errorcodes] ( Error_Type, Error_Code, Error_Description )
    SELECT 'REDEEM', 36, 'Game Voucher Reject'
ELSE
    UPDATE [Errorcodes]
    SET    Error_Type = 'REDEEM', Error_Description = 'Game Voucher Reject'
    WHERE  Error_Code = '36'

IF NOT EXISTS(SELECT 1 FROM [Errorcodes]WHERE Error_Code = '255')
    INSERT [Errorcodes] ( Error_Type, Error_Code, Error_Description )
    SELECT 'REDEEM', 255, 'Voucher In Process'
ELSE
    UPDATE [Errorcodes]
    SET    Error_Type = 'REDEEM', Error_Description = 'Voucher In Process'
    WHERE  Error_Code = '255'

IF NOT EXISTS(SELECT 1 FROM [Errorcodes]WHERE Error_Code = '42')
    INSERT [Errorcodes] ( Error_Type, Error_Code, Error_Description )
    SELECT 'REDEEM', 42, 'Waiting for Game Response'
ELSE
    UPDATE [Errorcodes]
    SET    Error_Type = 'REDEEM', Error_Description = 'Waiting for Game Response'
    WHERE  Error_Code = '42'

IF NOT EXISTS(SELECT 1 FROM [Errorcodes]WHERE Error_Code = '2')
    INSERT [Errorcodes] ( Error_Type, Error_Code, Error_Description )
    SELECT 'REDEEM', 2, 'System Communication Time Out'
ELSE
    UPDATE [Errorcodes]
    SET    Error_Type = 'REDEEM', Error_Description = 'System Communication Time Out'
    WHERE  Error_Code = '2'

IF NOT EXISTS(SELECT 1 FROM [Errorcodes]WHERE Error_Code = '39')
    INSERT [Errorcodes] ( Error_Type, Error_Code, Error_Description )
    SELECT 'REDEEM', 39, 'System Complete No Ack'
ELSE
    UPDATE [Errorcodes]
    SET    Error_Type = 'REDEEM', Error_Description = 'System Complete No Ack'
    WHERE  Error_Code = '39'

GO