USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertDepreciationDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertDepreciationDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_InsertDepreciationDetails(
    @Depreciation_Policy_ID                  AS INT,
    @Depreciation_Policy_Details_Period      AS INT,
    @Depreciation_Policy_Details_Duration    AS INT,
    @Depreciation_Policy_Details_Percentage  AS INT,
    @Depreciation_Policy_Details_ID AS INT OUTPUT
)
AS
BEGIN
	INSERT INTO Depreciation_Policy_Details
	  (
	    Depreciation_Policy_ID,
	    Depreciation_Policy_Details_Period,
	    Depreciation_Policy_Details_Duration,
	    Depreciation_Policy_Details_Percentage
	  )
	VALUES
	  (
	    @Depreciation_Policy_ID,
	    @Depreciation_Policy_Details_Period,
	    @Depreciation_Policy_Details_Duration,
	    @Depreciation_Policy_Details_Percentage
	  )SELECT @Depreciation_Policy_Details_ID=SCOPE_IDENTITY()
END

GO

