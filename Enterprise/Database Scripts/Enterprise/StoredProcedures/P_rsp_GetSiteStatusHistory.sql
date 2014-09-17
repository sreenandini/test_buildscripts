USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteStatusHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteStatusHistory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--SP to get Site history
--Created by Madhu	
CREATE PROC rsp_GetSiteStatusHistory(@SiteCode VARCHAR(100))  
AS  
--SELECT TOP 1000 row_number() over (order by Updatetimestamp desc) as RowNumber, *  from SiteStatusHistory   
--where SiteCode = @SiteCode  
--and sitestatus is not null
--order by UpdateTimeStamp desc  
SELECT TOP 1000 
       ROW_NUMBER() OVER(ORDER BY Sno DESC) AS RowNumber,
       SiteCode,
       SiteStatus,
       UpdateTimeStamp
FROM   SiteStatusHistory(NOLOCK)
WHERE  SiteCode = @SiteCode
       AND sitestatus IS NOT NULL

GO

