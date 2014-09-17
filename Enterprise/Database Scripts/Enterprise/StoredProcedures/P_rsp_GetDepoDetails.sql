USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDepoDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDepoDetails]
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
CREATE PROCEDURE [dbo].rsp_GetDepoDetails
(@site int=0)
AS
BEGIN

	
IF @site = 0       SET @site = NULL

	SELECT Depot_ID, Depot_Name FROM Depot 
	WHERE ( ( @site IS NULL )OR( @site IS NOT NULL AND Supplier_ID= @site ))
	ORDER BY  Depot_Name
	

END



 
GO

