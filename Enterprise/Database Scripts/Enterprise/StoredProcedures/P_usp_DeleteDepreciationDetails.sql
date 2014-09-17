USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_DeleteDepreciationDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_DeleteDepreciationDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_DeleteDepreciationDetails(@Depreciation_Policy_Details_ID AS INT)
AS
BEGIN
	IF EXISTS(
	       SELECT 1
	       FROM   Depreciation_Policy_Details dpd WITH (NOLOCK)
	       WHERE  Depreciation_Policy_Details_ID = @Depreciation_Policy_Details_ID
	   )
	BEGIN
	    DELETE 
	    FROM   Depreciation_Policy_Details
	    WHERE  Depreciation_Policy_Details_ID = @Depreciation_Policy_Details_ID
	END
END

GO

