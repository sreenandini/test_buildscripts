USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetRegion_Site]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetRegion_Site]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/***
rsp_GetRegion_Site		Kirubakar S		08 MAY 2010
***/
CREATE PROCEDURE rsp_GetRegion_Site 
@Site int
AS
BEGIN
SELECT REGION 
FROM Site 
WHERE
	Site_ID=@Site

END

GO

