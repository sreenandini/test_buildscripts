USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateProfitShareGroupDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateProfitShareGroupDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Author:		Aishwarrya v s
-- Create date: 8th October 2012
-- Description:	Update ProfitShareGroup Details
-- =============================================
CREATE PROCEDURE usp_UpdateProfitShareGroupDetails
	@ProfitShareGroupId INT,
	@ProfitShareGroupName VARCHAR(50),
	@ProfitSharePercentage FLOAT,
	@ProfitShareGroupDescription VARCHAR(255),
	@ProfitShareGroupIdOut INT = 0 OUTPUT
AS
BEGIN
	IF NOT EXISTS(
	       SELECT 1
	       FROM   [dbo].ProfitShareGroup WITH (NOLOCK)
	       WHERE  ProfitShareGroupId = @ProfitShareGroupId
	   )
	BEGIN
	    INSERT INTO ProfitShareGroup
	      (
	        ProfitShareGroupName,
	        ProfitSharePercentage,
	        ProfitShareGroupDescription
	      )
	    VALUES
	      (
	        @ProfitShareGroupName,
	        @ProfitSharePercentage,
	        @ProfitShareGroupDescription
	      )
	    SET @ProfitShareGroupId = SCOPE_IDENTITY()
	    SET @ProfitShareGroupIdOut = @ProfitShareGroupId
	END
	ELSE
	BEGIN
	    UPDATE ProfitShareGroup
	    SET    ProfitShareGroupName = @ProfitShareGroupName,
	           ProfitSharePercentage = @ProfitSharePercentage,
	           ProfitShareGroupDescription = @ProfitShareGroupDescription,
	           DateModified = GETDATE()
	    WHERE  ProfitShareGroupId = @ProfitShareGroupId
	    SET @ProfitShareGroupIdOut = @ProfitShareGroupId
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
	           @ProfitShareGroupId,
	           'PROFITSHAREGROUP',
	           Site_Code
	    FROM   SITE WITH(NOLOCK)
	    WHERE  Site_Enabled = 1
	           AND sitestatus = 'FULLYCONFIGURED'
	END
END

GO

