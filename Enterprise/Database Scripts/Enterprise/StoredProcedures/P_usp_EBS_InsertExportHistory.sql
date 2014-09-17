/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 03/12/2014 4:19:20 PM
 ************************************************************/

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_EBS_InsertExportHistory]    Script Date: 03/04/2014 20:25:54 ******/
IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_EBS_InsertExportHistory]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_EBS_InsertExportHistory]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_EBS_InsertExportHistory]    Script Date: 03/04/2014 20:25:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- -----------------------------------------------------------------------------------------------------------------------------------            
-- Revision History             
--             
-- 26/05/2008 Rajkumar	Created   
-- 08/03/2013 Venkatesh H added IsEBSEnabled Setting Check         
-- ===================================================================================================================================            
  /*
  DECLARE @EH_ID INT
  EXEC [usp_EBS_InsertExportHistory] 'SITE','TESTING', @EH_ID OUTPUT
  UPDATE SETTING SET SETTING_VALUE = 'FALSE' WHERE SETTING_NAME = 'IsEBSEnabled'
  SELECT * FROM EBS_Export_History
  
  EXEC [usp_EBS_InsertExportHistory] 'SITE','TESTING', @EH_ID OUTPUT
  SELECT COUNT(*) FROM EBS_Export_History
  UPDATE SETTING SET SETTING_VALUE = 'TRUE' WHERE SETTING_NAME = 'IsEBSEnabled'
  
  EXEC [usp_EBS_InsertExportHistory] 'SITE','TESTING', @EH_ID OUTPUT
  SELECT COUNT(*) FROM EBS_Export_History
  
  */          
CREATE PROCEDURE [dbo].[usp_EBS_InsertExportHistory](
    @EH_Type      VARCHAR(20),
    @EH_Value     XML,
    @EH_ID        INT = NULL OUTPUT,
    @EH_SiteCode  VARCHAR(50) = 0,
    @RefTableID   INT = NULL,
    @IsDelete     BIT = 0
)
AS
BEGIN
	SET @EH_ID = 0
	DECLARE @Setting_Value VARCHAR(100)
	EXECUTE [Enterprise].[dbo].[rsp_GetSetting] 0, 'IsEBSEnabled', 'False',@Setting_Value  
	OUTPUT
	
	IF UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, '')))) = 'FALSE'
	BEGIN
		 RETURN
	END
	
	-- If the delete operation is performed, then log it
	IF (@IsDelete = 1)
	BEGIN
	    INSERT INTO dbo.EBS_ObjectDeletion_History
	      (
	      	[EBS_RefType],
	        [EBS_RefID],
	        [EBS_RefDateTime],
	        [EBS_RefValue]
	      )
	    VALUES
	      (
	      	@EH_Type,
	        @RefTableID,
	        GETDATE(),
	        @EH_Value
	      )
	END
	
	IF @EH_Value IS NOT NULL
	  BEGIN
	    INSERT INTO dbo.EBS_Export_History
	      (
	        EH_Date,
	        EH_Type,
	        EH_Value,
	        EH_Status,
	        EH_SiteCode,
	        EH_IsDeleted
	      )
	    VALUES
	      (
	        GETDATE(),
	        @EH_Type,
	        @EH_Value,
	        0,
	        @EH_SiteCode,
	        @IsDelete
	      )
	    SET @EH_ID = SCOPE_IDENTITY()
	END
	
END
GO


