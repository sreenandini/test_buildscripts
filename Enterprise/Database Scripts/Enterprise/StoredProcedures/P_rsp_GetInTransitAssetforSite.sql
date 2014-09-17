USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetInTransitAssetforSite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetInTransitAssetforSite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-----------------------------------------------------------------------------------------------------        
--                
-- Description: Get In Transit Assets
-- Inputs:                  
--                
-- Outputs:     Result Set - In Transit Assets
--                                  
-- =======================================================================                
--                 
-- Revision History                
--                
-- Yoganandh P		14/10/2010		Created
------------------------------------------------------------------------------------------------------
CREATE PROCEDURE rsp_GetInTransitAssetforSite
(	
	@Site_Code Varchar(50)
)
AS
BEGIN
	SELECT 
		Machine_Stock_No as 'AssetNo'
	FROM 
		Machine 
	WHERE 
		Machine_Transit_Site_Code = @Site_Code
	AND 
		Machine_Status_Flag = 18
END


GO

