USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_DeleteDepreciationPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_DeleteDepreciationPolicy]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
* Revision History  
*   
* <Name of the owner>         DateCreated       Type(Created/Modified)  Description  
  Kalaiyarasan.P              10-Dec-2012         Created               This SP is used to delete depreciation  and depreciation policy details
  --Exec  usp_DeleteDepreciationPolicy 3                                                                   
*/  
  
CREATE PROCEDURE usp_DeleteDepreciationPolicy(@Depreciation_Policy_ID AS INT)
AS
BEGIN
	IF EXISTS(
	       SELECT 1
	       FROM   Depreciation_Policy WITH(NOLOCK)
	       WHERE  Depreciation_Policy_ID = @Depreciation_Policy_ID
	   )
	BEGIN
	    DELETE 
	    FROM   Depreciation_Policy
	    WHERE  Depreciation_Policy_ID = @Depreciation_Policy_ID
	END
	
	IF EXISTS(
	       SELECT 1
	       FROM   Depreciation_Policy_Details WITH(NOLOCK)
	       WHERE  Depreciation_Policy_ID = @Depreciation_Policy_ID
	   )
	BEGIN
	    DELETE 
	    FROM   Depreciation_Policy_Details
	    WHERE  Depreciation_Policy_ID = @Depreciation_Policy_ID
	END
END

GO

