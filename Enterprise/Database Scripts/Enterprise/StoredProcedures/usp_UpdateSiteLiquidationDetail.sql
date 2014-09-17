USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateSiteLiquidationDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateSiteLiquidationDetail]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- usp_UpdateSiteLiquidationDetail      
-- -----------------------------------------------------------------      
--   
-- To update site liquidation id
--   
-- -----------------------------------------------------------------  
-- Revision History             
--   
-- 23/12/2013 Dinesh Rathinavel Created  
--   
-- =================================================================  
  
  
CREATE PROCEDURE [dbo].[usp_UpdateSiteLiquidationDetail]  
	@HQLiquidationID INT,  
	@LiquidationID INT
AS    
BEGIN

	UPDATE LiquidationDetails
		SET HQ_ID = @LiquidationID
	WHERE LiquidationId = @HQLiquidationID

END
GO
