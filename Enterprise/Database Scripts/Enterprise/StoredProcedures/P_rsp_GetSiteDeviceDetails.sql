USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteDeviceDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteDeviceDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================
-- rsp_GetSiteDeviceDetails
-- -----------------------------------------------------------------
--
-- returns all device details.
-- 
-- -----------------------------------------------------------------    
-- Revision History       
--       
-- 09/09/09 Renjish Created      
-- =================================================================  

CREATE PROCEDURE dbo.rsp_GetSiteDeviceDetails
@iSiteID INT

AS

SELECT iDeviceID,
iSiteID,
strDeviceType,
dtCreation,
dtDeactivate,
strIPAddress,
strDeviceName,
strSerial,
strProgramName,
strProgramVersion,
bDebug,
bEnabled,
bDESEncrypt,
dtLastResponse,
bAutoCreate,
strRSMIPAddress,
iProtocolVersion,
bPersistConnection,
strPassword,
iDevNum,
iHeartBeatInterval,
iRSMRespond,
bUseXML,
strManufacturerID,
bRSMShowXML,
bRSMSendMail,
bRSMLogScreen,
bRSMLogFile,
bRSMLogPrinter,
iPort,
strLocation,
bSendEODNotify FROM dbo.Device WITH(NOLOCK) WHERE Site_Code = @iSiteID 
--FOR XML PATH ('Device'), ELEMENTS, ROOT('Devices') 


GO

