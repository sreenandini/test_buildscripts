USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ImportDeviceDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ImportDeviceDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================
-- rsp_ImportDeviceDetails
-- -----------------------------------------------------------------
--
-- Imports the device details to device table.
-- 
-- -----------------------------------------------------------------    
-- Revision History       
--       
-- 09/09/09 Renjish Created      
-- =================================================================  

CREATE PROCEDURE dbo.rsp_ImportDeviceDetails
@doc VARCHAR(MAX),
@IsSuccess INT OUTPUT
AS

DECLARE @iRowCount INT
DECLARE @idoc INT
DECLARE @error INT  
DECLARE @iDeviceID INT  
DECLARE @iSiteID INT  
DECLARE @Site_Code VARCHAR(50)

--variables for error handling 
SET @IsSuccess = -1 
SET @error = 0

--Declare a table variable to hold the data.
DECLARE @Device TABLE(
[iDeviceID] INT,
[iSiteID] INT,
[strDeviceType] CHAR(6),
[dtCreation] DATETIME,
[dtDeactivate] DATETIME,
[strIPAddress] VARCHAR(15),
[strDeviceName] VARCHAR(10),
[strSerial] VARCHAR(50),
[strProgramName] VARCHAR(10),
[strProgramVersion] VARCHAR(16),
[bDebug] BIT,
[bEnabled] BIT,
[bDESEncrypt] BIT,
[dtLastResponse] DATETIME,
[bAutoCreate] BIT,
[strRSMIPAddress] VARCHAR(15),
[iProtocolVersion] INT,
[bPersistConnection] BIT,
[strPassword] VARCHAR(16),
[iDevNum] INT,
[iHeartBeatInterval] INT,
[iRSMRespond] INT,
[bUseXML] BIT,
[strManufacturerID] CHAR(8),
[bRSMShowXML] BIT,
[bRSMSendMail] BIT,
[bRSMLogScreen] BIT,
[bRSMLogFile] BIT,
[bRSMLogPrinter] BIT,
[iPort] INT,
[strLocation] VARCHAR(32),
[bSendEODNotify] BIT,
[Site_Code]	VARCHAR(50)
)

--add the encoding version as we need to process special characters like pound symbol 
--SET @doc='<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc  

--Create an internal representation of the XML document.
EXEC sp_xml_preparedocument @idoc OUTPUT, @doc  

--Set row count to 0.
SET @iRowCount = 0

--Insert the XML data to the table varible.
INSERT INTO @Device
SELECT * FROM OPENXML (@idoc, '/Devices/Device',2)         
WITH Device  

--Get the row count value.
SELECT @iRowCount = COUNT(iDeviceID) FROM @Device

--Assign the Voucher ID, Site ID & Bar Code to the respective variables.  
SELECT  @iDeviceID = iDeviceID,
		@iSiteID = iSiteID,
		@Site_Code = Site_Code
FROM OPENXML (@idoc, '/Devices',2)    
WITH (iDeviceID int './Device/iDeviceID', 
iSiteID int './Device/iSiteID',
Site_Code	VARCHAR(50)	'./Device/Site_Code') 

--Check for row count value.
IF @iRowCount > 0
BEGIN
	IF EXISTS(SELECT iDeviceID FROM Device WHERE iDeviceID = @iDeviceID AND Site_Code = @Site_Code)--iSiteID	= @iSiteID)
	BEGIN
		--Update Code Start.
		UPDATE D
		SET 
		strDeviceType = tmpD.strDeviceType,
		dtCreation = tmpD.dtCreation,
		dtDeactivate = tmpD.dtDeactivate,
		strIPAddress = tmpD.strIPAddress,
		strDeviceName = tmpD.strDeviceName,
		strSerial = tmpD.strSerial,
		strProgramName = tmpD.strProgramName,
		strProgramVersion = tmpD.strProgramVersion,
		bDebug = tmpD.bDebug,
		bEnabled = tmpD.bEnabled,
		bDESEncrypt = tmpD.bDESEncrypt,
		dtLastResponse = tmpD.dtLastResponse,
		bAutoCreate = tmpD.bAutoCreate,
		strRSMIPAddress = tmpD.strRSMIPAddress,
		iProtocolVersion = tmpD.iProtocolVersion,
		bPersistConnection = tmpD.bPersistConnection,
		strPassword = tmpD.strPassword,
		iDevNum = tmpD.iDevNum,
		iHeartBeatInterval = tmpD.iHeartBeatInterval,
		iRSMRespond = tmpD.iRSMRespond,
		bUseXML = tmpD.bUseXML,
		strManufacturerID = tmpD.strManufacturerID,
		bRSMShowXML = tmpD.bRSMShowXML,
		bRSMSendMail = tmpD.bRSMSendMail,
		bRSMLogScreen = tmpD.bRSMLogScreen,
		bRSMLogFile = tmpD.bRSMLogFile,
		bRSMLogPrinter = tmpD.bRSMLogPrinter,
		iPort = tmpD.iPort,
		strLocation = tmpD.strLocation,
		bSendEODNotify = tmpD.bSendEODNotify	
		FROM @Device tmpD
		INNER JOIN Device D 
		ON D.iDeviceID = tmpD.iDeviceID 
		AND D.Site_Code = tmpD.Site_Code --D.iSiteID = tmpD.iSiteID 
		--Update Code End.
	END
	ELSE	
	BEGIN
		--Insert Code Start.
		INSERT INTO Device
		SELECT * FROM @Device   
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

