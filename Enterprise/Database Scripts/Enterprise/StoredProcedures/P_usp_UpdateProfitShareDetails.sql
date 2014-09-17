USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateProfitShareDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateProfitShareDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Author:		Aishwarrya v s
-- Create date: 8th October 2012
-- Description:	Update ProfitShare Details
-- =============================================
CREATE PROCEDURE usp_UpdateProfitShareDetails
	@ShareHolderId INT,
	@ProfitShareGroupId INT,
	@ProfitShareId INT,
	@ProfitSharePercentage FLOAT,
	@ProfitShareDescription VARCHAR(255)
AS
BEGIN
	IF NOT EXISTS(
	       SELECT 1
	       FROM   [dbo].[ProfitShare] WITH (NOLOCK)
	       WHERE  ProfitShareId = @ProfitShareId
	   )
	BEGIN
	    INSERT INTO ProfitShare
	      (
	        ShareHolderId,
	        ProfitShareGroupId,
	        ProfitSharePercentage,
	        ProfitShareDescription
	      )
	    VALUES
	      (
	        @ShareHolderId,
	        @ProfitShareGroupId,
	        @ProfitSharePercentage,
	        @ProfitShareDescription
	      )
	    SET @ProfitShareId = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
	    UPDATE ProfitShare
	    SET    ShareHolderId = @ShareHolderId,
	           ProfitShareGroupId = @ProfitShareGroupId,
	           ProfitSharePercentage = @ProfitSharePercentage,
	           ProfitShareDescription = @ProfitShareDescription,
	           DateModified = GETDATE()
	    WHERE  ProfitShareId = @ProfitShareId
	END
	
	IF @@ROWCOUNT > 0
	BEGIN
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
	END
	
	UPDATE dbo.ProfitSharegroup
	SET    ProfitSharePercentage = (
	           SELECT SUM(PS.ProfitSharePercentage)
	           FROM   dbo.ProfitSharegroup PSG WITH(NOLOCK)
	                  INNER JOIN dbo.ProfitShare PS WITH(NOLOCK)
	                       ON  PSG.ProfitShareGroupId = PS.ProfitShareGroupId
	           WHERE  PSG.ProfitShareGroupId = @ProfitShareGroupId
	                  AND PS.SysDelete = 0
	       )
	WHERE  ProfitShareGroupId = @ProfitShareGroupId
END

GO

