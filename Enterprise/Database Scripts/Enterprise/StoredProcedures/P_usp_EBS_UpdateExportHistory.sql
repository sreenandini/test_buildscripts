/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 03/05/2014 8:04:40 PM
 ************************************************************/

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_EBS_UpdateExportHistory]    Script Date: 03/04/2014 20:25:54 ******/
IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_EBS_UpdateExportHistory]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_EBS_UpdateExportHistory]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_EBS_UpdateExportHistory]    Script Date: 03/04/2014 20:25:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- -----------------------------------------------------------------------------------------------------------------------------------            
-- Revision History             
--             
-- 26/05/2008 Rajkumar	Created            
-- ===================================================================================================================================            
            
CREATE PROCEDURE [dbo].[usp_EBS_UpdateExportHistory]
	@EH_ID INT,
	@EH_Status INT
AS
BEGIN
	UPDATE dbo.EBS_Export_History
	SET    EH_Status = @EH_Status,
	       EH_Export_Date = GETDATE()
	WHERE  (EH_ID = @EH_ID)
		
	IF (@EH_Status = 100)
	BEGIN
	    EXEC [dbo].[usp_UpdateExportRefPointer] 'EBS',
	         @EH_ID
	END
END
GO


