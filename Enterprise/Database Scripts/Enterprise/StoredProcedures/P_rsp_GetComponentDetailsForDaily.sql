USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetComponentDetailsForDaily]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetComponentDetailsForDaily]
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
--- Senthil  07/06/10     Created 
--------------------------------------------------------------------------- 
CREATE PROCEDURE dbo.rsp_GetComponentDetailsForDaily
AS
BEGIN
	SELECT CVMCD_CCD_ID As ComponentID, CVMCD_Machine_Serial_No As MachineSerialNo
	FROM dbo.CV_Machine_Component_Details
END
GO

