USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetLatestBatchID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetLatestBatchID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================  
-- OUTPUT           To fetch the latest site batch id based on the site code
-- =======================================================================  
-- Revision History  
--   
-- Vineetha Mathew 16/02/09  Created  
---------------------------------------------------------------------------   
CREATE PROCEDURE rsp_GetLatestBatchID 
 
 @SiteCode as varchar(50) 

AS  
SET NOCOUNT ON      

--SELECT right(batch_ref,len(batch_ref)-len(left(batch_ref,5))),batch_id from batch order by 1 desc

SELECT  TOP 1 right(batch_ref,len(batch_ref)-len(left(batch_ref,5))) AS SiteBatchNo,* FROM batch 
WHERE batch_ref LIKE @SiteCode + '%'
order by batch_id desc

--exec rsp_GetLatestBatchID 1003

GO

