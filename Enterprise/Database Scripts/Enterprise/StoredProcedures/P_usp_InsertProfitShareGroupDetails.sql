USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertProfitShareGroupDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertProfitShareGroupDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Author:		Aishwarrya v s
-- Create date: 8th October 2012
-- Description:	Insert ProfitShareGroup Details
-- =============================================
CREATE PROCEDURE usp_InsertProfitShareGroupDetails
	@ProfitShareGroupName VARCHAR(50),
	@ProfitSharePercentage FLOAT,
	@ProfitShareGroupDescription VARCHAR(255)
AS
BEGIN
	DECLARE @ProfitShareGroupId INT	
	
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
	
	SET @ProfitShareGroupId = CAST(SCOPE_IDENTITY() AS INT)
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

GO

