USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateDepreciationDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateDepreciationDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


  
CREATE PROCEDURE usp_UpdateDepreciationDetails(
    @Depreciation_Policy_Details_ID          AS INT,
    @Depreciation_Policy_Details_Duration    AS INT,
    @Depreciation_Policy_Details_Percentage  AS INT
)
AS
BEGIN
	IF EXISTS(
	       SELECT 1
	       FROM   Depreciation_Policy_Details dpd WITH(NOLOCK)
	       WHERE  Depreciation_Policy_Details_ID = @Depreciation_Policy_Details_ID
	   )
	BEGIN
	    UPDATE Depreciation_Policy_Details
	    SET    Depreciation_Policy_Details_Duration = @Depreciation_Policy_Details_Duration,
	           Depreciation_Policy_Details_Percentage = @Depreciation_Policy_Details_Percentage
	    WHERE  Depreciation_Policy_Details_ID = @Depreciation_Policy_Details_ID
	END
END

GO

