USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteOnRegion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteOnRegion]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetSiteOnRegion]
	@RegionId INT = 0
AS  
BEGIN
	SELECT
		S.Site_ID,
		S.Site_Name
	FROM Site S
	WHERE 
	(@RegionId=0) OR (@RegionId<>0 AND S.Sub_Company_Region_Id=@RegionId)    
	ORDER BY S.Site_Name    		

END

GO

