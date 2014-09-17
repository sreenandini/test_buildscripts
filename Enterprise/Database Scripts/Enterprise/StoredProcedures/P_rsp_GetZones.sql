USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetZones]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetZones]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- OUTPUT --Get Company details -- exec rsp_GetZones 0       
-- Revision History    
-- Vineetha Mathew 22/03/2010  Created    
-- ======================================================================= 
CREATE PROCEDURE [dbo].rsp_GetZones
(@site INT = 0
)
AS
BEGIN

IF @site = 0    SET @site = NULL	

	SELECT Zone_ID,Zone_Name
	FROM Zone 
	WHERE 
	zone_name IS NOT NULL AND  zone_name <> ''
	AND 
	( ( @site IS NULL )OR( @site IS NOT NULL AND Site_ID= @site ))
	ORDER BY Zone_Name

END

GO

