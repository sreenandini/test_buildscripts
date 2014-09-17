USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCalendarDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCalendarDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetCalendarDetails  
@SiteId INT  
AS  
BEGIN  
SELECT   
 C.Calendar_ID,   
 S.*   
FROM dbo.Calendar C   
INNER JOIN dbo.Sub_Company_Calendar SCC   
ON C.Calendar_ID = SCC.Calendar_ID INNER JOIN dbo.Site S   
ON SCC.Sub_Company_ID = S.Sub_Company_ID   
WHERE   
 SCC.Sub_Company_Calendar_Active = 1 AND S.Site_ID = @SiteId   
END  
  
  
--exec rsp_GetCalendarDetails 1

GO

