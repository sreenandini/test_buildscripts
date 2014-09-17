USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteStatusHistoryByTimeStamp]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteStatusHistoryByTimeStamp]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE   PROC rsp_GetSiteStatusHistoryByTimeStamp
@SiteCode VARCHAR(100),
@TimeStamp DATETIME
AS      
BEGIN
	SELECT *
	FROM   SiteStatusHistory WITH (NOLOCK)
	WHERE  SiteCode = @SiteCode
	       AND sitestatus IS NOT NULL
	       AND UpdateTimeStamp = @TimeStamp
END   

GO

