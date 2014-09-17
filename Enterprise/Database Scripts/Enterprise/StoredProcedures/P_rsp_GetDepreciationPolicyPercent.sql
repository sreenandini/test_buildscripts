USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDepreciationPolicyPercent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDepreciationPolicyPercent]
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
  Kalaiyarasan.P              10-Dec-2012         Created               This SP is used to get depreciation policy percentage for 
																		Check that depreciation will not go over 100%
  --Exec  rsp_GetDepreciationPolicyPercent                                                                   
*/  
  
CREATE PROCEDURE rsp_GetDepreciationPolicyPercent
	@Depreciation_Policy_ID INT,
	@Depreciation_Policy_Details_ID INT
AS
BEGIN
	SELECT SUM(Depreciation_Policy_Details_Percentage) AS TotalDrop
	FROM   Depreciation_Policy_Details
	WHERE  Depreciation_Policy_ID = @Depreciation_Policy_ID
	       AND Depreciation_Policy_Details_ID <> @Depreciation_Policy_Details_ID
END


GO

