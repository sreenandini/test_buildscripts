USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDepreciationPolicyDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDepreciationPolicyDetails]
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
  Kalaiyarasan.P              10-Dec-2012         Created               This SP is used to get depreciation and depreciation_policy details
																		
  --Exec  rsp_GetDepreciationPolicyDetails  3                                                                
*/  
CREATE PROCEDURE rsp_GetDepreciationPolicyDetails(@Depreciation_Policy_ID AS INT=NULL)
AS
BEGIN
	SELECT dp.Depreciation_Policy_ID,
		   dpd.Depreciation_Policy_Details_ID,
	       dp.Depreciation_Policy_Description,
	       dp.Depreciation_Policy_Residual_Value,
	       dpd.Depreciation_Policy_Details_Period,
	       dpd.Depreciation_Policy_Details_Duration,
	       dpd.Depreciation_Policy_Details_Percentage
	FROM   Depreciation_Policy dp WITH (NOLOCK)
	       INNER JOIN Depreciation_Policy_Details dpd WITH (NOLOCK) 
	            ON  dpd.Depreciation_Policy_ID = dp.Depreciation_Policy_ID
	WHERE  dp.Depreciation_Policy_ID = COALESCE(@Depreciation_Policy_ID,dp.Depreciation_Policy_ID)
END

GO

