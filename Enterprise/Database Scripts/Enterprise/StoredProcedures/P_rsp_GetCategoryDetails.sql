USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCategoryDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCategoryDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================    
-- OUTPUT --Get Company details -- exec rsp_GetCompanyDetails 2         
-- Revision History    
-- Vineetha Mathew 22/03/2010  Created    
-- ======================================================================= 
CREATE PROCEDURE [dbo].rsp_GetCategoryDetails

AS
BEGIN

	

	SELECT DISTINCT Machine_type_id, Machine_type_code
	FROM Machine_type 
	where IsNonGamingAssetType = 0 ORDER BY Machine_Type_Code
	

END



 
GO

