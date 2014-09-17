USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteClassifNameonID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteClassifNameonID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[rsp_GetSiteClassifNameonID] 
(
	@SiteClassificationID int
)
AS
BEGIN

SELECT 	
	Site_Classification_Name 
FROM Site_Classification 
WHERE Site_Classification_ID = @SiteClassificationID

END

--exec rsp_GetSiteClassifNameonID 4


GO

