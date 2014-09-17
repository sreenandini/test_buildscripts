/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 03/06/2014 11:15:50 PM
 ************************************************************/

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_EBS_GetUnprocessedRecords]    Script Date: 03/06/2014 23:15:17 ******/
IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_EBS_GetUnprocessedRecords]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_EBS_GetUnprocessedRecords]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_EBS_GetUnprocessedRecords]    Script Date: 03/06/2014 23:15:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Revision History 
-- 
-- 22/05/2008	Rajkumar	Created
-- ===================================================================================================================================
                 
 
CREATE PROCEDURE [dbo].[rsp_EBS_GetUnprocessedRecords]
AS
BEGIN
	DECLARE @LAST_PROCESSED_EH_ID INT
	SELECT @LAST_PROCESSED_EH_ID = RefPointerLastID
	FROM   Export_RefPointer erp WITH(NOLOCK)
	WHERE  RefPointerType = 'EBS'
	
	SELECT TOP 100 EEH.EH_ID,
	       EEH.EH_Date,
	       EEH.EH_Type,
	       EEH.EH_Value,
	       EEH.EH_Status,
	       EEH.EH_SiteCode,
	       EEH.EH_IsDeleted
	FROM   EBS_Export_History EEH WITH(NOLOCK)
	WHERE  EEH.EH_ID > @LAST_PROCESSED_EH_ID
	ORDER BY
	       EEH.EH_ID ASC
END
GO


