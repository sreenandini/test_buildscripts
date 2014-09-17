USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetRegion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetRegion]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetRegion] 
AS
BEGIN
	SELECT 
		SC.Sub_Company_Region_ID, SC.Sub_Company_Region_Name 
	FROM
		Sub_Company_Region SC
	ORDER BY SC.Sub_Company_Region_Name	
		
END

GO

