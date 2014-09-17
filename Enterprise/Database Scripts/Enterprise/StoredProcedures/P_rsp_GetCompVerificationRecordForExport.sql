USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCompVerificationRecordForExport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCompVerificationRecordForExport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================  
-- rsp_GetCompVerificationRecordForExport  
-- -----------------------------------------------------------------  
--  
-- Get the Component Verification Details to export to Enterprise.    
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 14/06/10 Renjish Created       
-- =================================================================  

CREATE PROCEDURE dbo.rsp_GetCompVerificationRecordForExport
@SerialNo VARCHAR(30),
@CompTypeID INT,
@VerificationType INT
AS

DECLARE @OutXML XML

SET @OutXML = (SELECT @SerialNo AS Machine_Serial_No, @CompTypeID AS CVT_CODE,
@VerificationType AS Verification_Type
FOR XML PATH ('VERIFICATION_DETAIL') ,ELEMENTS XSINIL,ROOT('VERIFICATION_DETAILS'))

SELECT @OutXML As 'XMLData'


GO

