USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateSiteLiquidationShareDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateSiteLiquidationShareDetail]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- usp_UpdateSiteLiquidationShareDetail      
-- -----------------------------------------------------------------      
--   
-- To update site liquidation share id
--   
-- -----------------------------------------------------------------  
-- Revision History             
--   
-- 23/12/2013 Dinesh Rathinavel Created  
--   
-- =================================================================  
  
  
CREATE PROCEDURE [dbo].[usp_UpdateSiteLiquidationShareDetail]  
	@HQLiquidationShareID INT,  
	@LiquidationShareID INT
AS    
BEGIN

	UPDATE LiquidationShareDetails
		SET HQ_ID = @LiquidationShareID
	WHERE LiquidationShareId = @HQLiquidationShareID

END
GO
