USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetBatchNumbers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetBatchNumbers]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================  
-- OUTPUT    To get all BAtchnumbers in ascending
-- =======================================================================  
-- Revision History  --  Exec  [Rsp_GetSiteAllEvents] 1003,10    
-- poorna 23/02/09  Created
---------------------------------------------------------------------------  
CREATE PROCEDURE  rsp_GetBatchNumbers
@SiteCode varchar(50),
@Xdays int

AS

BEGIN
SET DATEFORMAT dmy
DECLARE @MAXDATE DATETIME
	  Select @MAXDATE = max(cast(Batch_Date as datetime)) From Batch 
	  Where SUBSTRING(Batch_ref,1,charindex(',',Batch_ref)-1) = @SiteCode
	
      SELECT Batch_ID FROM Batch WHERE CAST(Batch_Date AS DATETIME) >=  @MAXDATE - @Xdays
	  AND SUBSTRING(Batch_ref,1,charindex(',',Batch_ref)-1) = @SiteCode

END


GO

