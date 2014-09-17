USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertProfitShareDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertProfitShareDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Author:		Aishwarrya v s
-- Create date: 8th October 2012
-- Description:	Insert ShareHolder Details
-- =============================================


CREATE PROCEDURE usp_InsertProfitShareDetails
	@ProfitSharePercentage FLOAT,
	@ProfitShareDescription VARCHAR(255),
	@ShareHolderId INT
AS
BEGIN
	DECLARE @ProfitShareGroupId  INT	
	DECLARE @ProfitShareId       INT
	
	
	SELECT @ProfitShareGroupId = P.ProfitShareGroupId
	FROM   ShareHolders S WITH(NOLOCK)
	       INNER JOIN ProfitShareGroup P WITH(NOLOCK)
	            ON  s.ShareHolderId = P.ProfitShareGroupId
	
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
	
	SET @ProfitShareId = CAST(SCOPE_IDENTITY() AS INT)
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

GO

