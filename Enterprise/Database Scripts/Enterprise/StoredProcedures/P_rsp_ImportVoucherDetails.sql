USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ImportVoucherDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ImportVoucherDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.rsp_ImportVoucherDetails    
@doc VARCHAR(MAX),    
@IsSuccess INT OUTPUT    
AS    
    
DECLARE @iRowCount INT    
DECLARE @idoc INT    
DECLARE @error INT      
DECLARE @iVoucherID INT      
DECLARE @iSiteID INT      
--DECLARE @dtPrinted DATETIME    
DECLARE @sBarCode CHAR(32)    
    
--variables for error handling     
SET @IsSuccess = -1     
SET @error = 0    
    
IF ISNULL(@doc,'') = ''    
BEGIN    
 SET @IsSuccess = 0     
 RETURN @error     
END    
    
--Declare a table variable to hold the data.    
DECLARE @Voucher TABLE(    
iVoucherID INT,    
iSessionID INT,    
iPayorID INT,    
iDeviceID INT,    
iPayDeviceID INT,    
iPlayerID INT,    
iVoucherSeq INT,    
bJackpot BIT,    
dtPrinted DATETIME,    
iAmount INT,    
strVoucherStatus CHAR(3),    
bOnlineValidate BIT,    
dtCreation DATETIME,    
dtPaid DATETIME,    
dtExpire DATETIME,    
dtOlValDate DATETIME,    
iValidationAtt INT,    
iSeconds INT,    
iVDayID INT,    
strBarCode CHAR(32),    
iWithheldAmt INT,    
iPaidDayID INT,    
iPltID INT,    
iPort INT,    
dtBusDay DATETIME,    
iPrinterID INT,    
iPaidPltID INT,    
iPrizeLevel INT,    
iVoucherType INT,    
iVoucherReprintID INT,    
iSiteID INT,    
iPaySiteID INT,    
iCollectionCompleted INT,    
iRedeemCollectionCompleted INT,    
strHashed VARBINARY(200),    
AuthorizedUser_No INT,    
Authorized_Date DATETIME,    
Ticket_Type INT,    
[ErrCode] [int] NULL,      
[ErrDeviceID] [int] NULL,      
[ErrTime] [datetime] NULL,
[dtVoid]  [datetime] NULL,
[VoucherIssuedUser] INT,
[VoucherRedeemedUser] INT,
[iVoucherVoidUser] INT
)    
--add the encoding version as we need to process special characters like pound symbol     
--SET @doc='<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc      
    
--Create an internal representation of the XML document.    
EXEC sp_xml_preparedocument @idoc OUTPUT, @doc      
    
--Set row count to 0.    
SET @iRowCount = 0    
    
--Insert the XML data to the table varible.    
INSERT INTO @Voucher    
SELECT * FROM OPENXML (@idoc, '/Voucher/Ticket',2)             
WITH Voucher      
    
--Get the row count value.    
SELECT @iRowCount = COUNT(iVoucherID) FROM @Voucher    
    
--Assign the Voucher ID, Site ID & Bar Code to the respective variables.      
SELECT  @iVoucherID = iVoucherID,    
  @iSiteID = iSiteID,    
--  @dtPrinted = dtPrinted    
  @sBarCode = LTRIM(RTRIM(strBarCode))    
FROM OPENXML (@idoc, '/Voucher',2)        
WITH (iVoucherID int './Ticket/iVoucherID',     
iSiteID int './Ticket/iSiteID',     
--dtPrinted DATETIME './Ticket/dtPrinted'    
strBarCode CHAR(32) './Ticket/strBarCode')     
    
--Check for row count value.    
IF @iRowCount > 0    
BEGIN    
 IF EXISTS(SELECT iVoucherID FROM Voucher WHERE iVoucherID = @iVoucherID AND iSiteID = @iSiteID AND LTRIM(RTRIM(strBarCode)) = @sBarCode)    
  BEGIN    
  --Update Code Start.     
  UPDATE V    
  SET         
  iSessionID = tmpV.iSessionID,    
  iPayorID = tmpV.iPayorID,    
  iDeviceID = tmpV.iDeviceID,    
  iPayDeviceID = tmpV.iPayDeviceID,    
  iPlayerID = tmpV.iPlayerID,    
  iVoucherSeq = tmpV.iVoucherSeq,    
  bJackpot = tmpV.bJackpot,    
  iAmount = tmpV.iAmount,    
  strVoucherStatus = tmpV.strVoucherStatus,    
  bOnlineValidate = tmpV.bOnlineValidate,    
  dtCreation = tmpV.dtCreation,    
  dtPaid = tmpV.dtPaid,    
  dtExpire = tmpV.dtExpire,     
  dtOlValDate = tmpV.dtOlValDate,    
  iValidationAtt = tmpV.iValidationAtt,     
  iSeconds = tmpV.iSeconds,    
  iVDayID = tmpV.iVDayID,    
  iWithheldAmt = tmpV.iWithheldAmt,    
  iPaidDayID = tmpV.iPaidDayID,    
  iPltID = tmpV.iPltID,    
  iPort = tmpV.iPort,    
  dtBusDay = tmpV.dtBusDay,    
  iPrinterID = tmpV.iPrinterID,    
  iPaidPltID = tmpV.iPaidPltID,    
  iPrizeLevel = tmpV.iPrizeLevel,    
  iVoucherType = tmpV.iVoucherType,    
  iVoucherReprintID = tmpV.iVoucherReprintID,    
  iPaySiteID = tmpV.iPaySiteID,    
  iCollectionCompleted = tmpV.iCollectionCompleted,    
  iRedeemCollectionCompleted = tmpV.iRedeemCollectionCompleted,    
  strHashed = tmpV.strHashed,      
  AuthorizedUser_No = tmpV.AuthorizedUser_No,    
  Authorized_Date = tmpV.Authorized_Date,    
  Ticket_Type = tmpV.Ticket_Type    
  ,[ErrCode]  = tmpV.[ErrCode]      
  ,[ErrDeviceID] = tmpV.[ErrDeviceID]      
  ,[ErrTime]   = tmpV.[ErrTime]
  ,[dtVoid] = tmpV.[dtVoid]
  ,[VoucherIssuedUser] = tmpV.[VoucherIssuedUser]
  ,[VoucherRedeemedUser] = tmpV.[VoucherRedeemedUser]
  ,[iVoucherVoidUser] = tmpV.[iVoucherVoidUser]
  FROM @Voucher tmpV    
  INNER JOIN Voucher V     
  ON V.iVoucherID = tmpV.iVoucherID     
  AND V.iSiteID = tmpV.iSiteID     
  AND LTRIM(RTRIM(V.strBarCode)) = LTRIM(RTRIM(tmpV.strBarCode))    
  --Update Code End.    
  END    
 ELSE    
  BEGIN    
  --Insert Code Start.    
  INSERT INTO Voucher    
  SELECT * FROM @Voucher       
  --Insert Code End.    
  END     
END    
    
--Removes the internal representation of the XML document.    
EXEC sp_xml_removedocument @idoc    
    
--Check for any errors during the insert process.    
SET @error = @@ERROR    
IF @error <> 0     
GOTO Err_Handler     
     
--Return success/failure      
Err_Handler:      
IF @error = 0      
SET @IsSuccess = 0     
--Success     
ELSE    
SET @IsSuccess = @error     
--Error      
RETURN @error

GO

