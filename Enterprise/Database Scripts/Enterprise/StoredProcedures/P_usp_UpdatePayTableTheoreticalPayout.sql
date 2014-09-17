USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdatePayTableTheoreticalPayout]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdatePayTableTheoreticalPayout]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- usp_UpdatePayTableTheoreticalPayout
-- -----------------------------------------------------------------
-- 
-- To update pay table TheoreticalPayout w.r.t the paytable id.
-- 
-- -----------------------------------------------------------------
-- Revision History
-- 
-- 11/08/2012 Dinesh Rathinavel Created
-- 
-- =================================================================

CREATE PROCEDURE [dbo].[usp_UpdatePayTableTheoreticalPayout]
	@Paytable_ID INT,
	@TheoreticalPayout FLOAT
AS      
BEGIN   
  
	SET NOCOUNT ON
	
	UPDATE [PayTable] 
		SET TheoreticalPayout = @TheoreticalPayout
	WHERE Paytable_ID = @Paytable_ID
	
	UPDATE MeterAnalysis.dbo.[PayTable] 
		SET TheoreticalPayout = @TheoreticalPayout
	WHERE Paytable_ID = @Paytable_ID
END

GO

