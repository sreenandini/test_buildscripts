USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetUnverifiedComponentsData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetUnverifiedComponentsData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------
-------------------------------------------------------------------------- 
---
--- Description: Get  the unverified Comp Data.
---
--- Inputs:      see inputs
---
--- Outputs:     
--- 
--- =======================================================================
--- 
--- Revision History
--- 
--- Senthil  04/06/10     Created 
--------------------------------------------------------------------------- 
CREATE PROCEDURE dbo.rsp_GetUnverifiedComponentsData
AS
BEGIN

	SELECT TOP 50 CVD_CCD_ID As ComponentID, CVD_Machine_Serial_No As MachineSerialNo,
	CVD_ID AS VerificationID
	FROM CV_Verification_Details WHERE CVD_Request_Status <> 0
END
GO

