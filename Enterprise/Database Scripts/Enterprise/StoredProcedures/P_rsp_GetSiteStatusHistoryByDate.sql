USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteStatusHistoryByDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteStatusHistoryByDate]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE   PROC rsp_GetSiteStatusHistoryByDate
@SiteCode VARCHAR(100),
@RowNumber INT       
AS      
BEGIN
	SELECT *
	FROM   (
	           SELECT ROW_NUMBER() OVER(ORDER BY Updatetimestamp ASC) AS 
	                  RowNumber,
	                  *
	           FROM   SiteStatusHistory WITH (NOLOCK)
	           WHERE  SiteCode = @SiteCode
	                  AND sitestatus IS NOT NULL
	       ) A
	WHERE  A.RowNumber = @RowNumber
	ORDER BY
	       A.UpdateTimeStamp DESC
END   

GO

