USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_DeleteProfitShare]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_DeleteProfitShare]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_DeleteProfitShare(@ProfitShareGroupId INT, @ProfitShareId INT, @Status INT OUTPUT)
AS
BEGIN
	UPDATE ProfitShare
	SET    SysDelete = 1
	WHERE  ProfitShareId = @ProfitShareId
	
	SET @Status = 0
	IF @@ROWCOUNT > 0
	    SET @Status = 1
	
	INSERT INTO dbo.Export_History
	  (
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Site_Code
	  )
	SELECT GETDATE(),
	       @ProfitShareId,
	       'PROFITSHARE',
	       Site_Code
	FROM   SITE WITH(NOLOCK)
	WHERE  Site_Enabled = 1
	       AND sitestatus = 'FULLYCONFIGURED'
	
	UPDATE dbo.ProfitSharegroup
	SET    ProfitSharePercentage = COALESCE((
	           SELECT SUM(PS.ProfitSharePercentage)
	           FROM   dbo.ProfitSharegroup PSG WITH(NOLOCK)
	                  INNER JOIN dbo.ProfitShare PS WITH(NOLOCK)
	                       ON  PSG.ProfitShareGroupId = PS.ProfitShareGroupId
	           WHERE  PSG.ProfitShareGroupId = @ProfitShareGroupId
	                  AND PS.SysDelete = 0
	       ),0)
	WHERE  ProfitShareGroupId = @ProfitShareGroupId
END

GO

