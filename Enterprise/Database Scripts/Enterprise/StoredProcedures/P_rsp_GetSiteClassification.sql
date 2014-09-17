USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteClassification]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteClassification]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[rsp_GetSiteClassification] 

AS
BEGIN

SELECT 
	Site_Classification_ID, 
	Site_Classification_Name 
FROM Site_Classification 
ORDER BY Site_Classification_Name

END


GO

