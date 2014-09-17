USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteVoucherDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteVoucherDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================
-- rsp_GetSiteVoucherDetails
-- -----------------------------------------------------------------
--
-- returns voucher details.
-- 
-- -----------------------------------------------------------------    
-- Revision History       
--       
-- 09/09/09 Renjish Created     
-- 02/12/09 Vineetha M	Modified Removed check for claimed tickets from Where clause
-- 04/12/09 Vineetha M	Modified Removed icollectioncompleted 
-- 27/04/10	Sudarsan S	IsNonCashable
-- =================================================================  

CREATE PROCEDURE dbo.rsp_GetSiteVoucherDetails
@iSiteID INT,
@NoofDays INT

AS

DECLARE @xml XML

SET @xml = (SELECT 
 
	iVoucherID,
	iSessionID,
	iPayorID,
	iDeviceID,
	iPayDeviceID,
	iPlayerID,
	iVoucherSeq,
	bJackpot,
	dtPrinted,
	iAmount,
	strVoucherStatus,
	bOnlineValidate,
	dtCreation,
	dtPaid,
	dtExpire,
	dtOlValDate,
	iValidationAtt,
	iSeconds,
	iVDayID,
	strBarCode,
	iWithheldAmt,
	iPaidDayID,
	iPltID,
	iPort,
	dtBusDay,
	iPrinterID,
	iPaidPltID,
	iPrizeLevel,
	iVoucherType,
	iVoucherReprintID,
	iSiteID,
	iPaySiteID,
	iCollectionCompleted,
	iRedeemCollectionCompleted,
	strHashed,
	Ticket_Type

FROM dbo.Voucher WITH(NOLOCK) WHERE iSiteID = @iSiteID 
--AND ( iCollectionCompleted = 0)
FOR XML PATH ('Voucher'), ELEMENTS, ROOT('VoucherRoot'))

SELECT @xml 


GO

