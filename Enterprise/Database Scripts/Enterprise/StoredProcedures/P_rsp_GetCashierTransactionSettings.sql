USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetCashierTransactionSettings]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetCashierTransactionSettings]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-------------------------------------------------------------------------------------------------------------------------
---    
--- Description: Fetches the Settings From Setting table
---        
--- Inputs:         
--- Outputs:     
--- ======================================================================================================================    
---     
--- Revision History    
---     
--- Dinesh R		26/04/2013		Created     
--------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[rsp_GetCashierTransactionSettings]
AS
BEGIN
	SELECT CAST(ISNULL(EnableCashdeskReconciliation, 0) AS BIT) AS EnableCashdeskReconciliation,
	       CAST(ISNULL(EnableCashdeskMovement, 0) AS BIT) AS EnableCashdeskMovement,
	       CAST(ISNULL(EnableSystemBalancing, 0) AS BIT) AS EnableSystemBalancing
	FROM   (
	           SELECT Setting_Name,
	                  Setting_Value
	           FROM   Setting
	       ) AS Source 
	       PIVOT(
	           MAX(Setting_Value)
	           FOR Setting_Name IN (EnableCashdeskReconciliation,EnableCashdeskMovement, EnableSystemBalancing)
	       ) AS Pvt
END
GO