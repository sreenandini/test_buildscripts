USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_AC_UpdateAlertProcessHistory]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_AC_UpdateAlertProcessHistory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_AC_UpdateAlertProcessHistory(
    @APH_AEH_ID         INT,
    @APH_Site_Code      VARCHAR(50),
    @APH_Type           VARCHAR(50),
    @APH_Message        XML,
    @APE_Received_Date  DATETIME
)
AS
BEGIN
	DECLARE @Setting_Value VARCHAR(100)
	EXEC [rsp_GetSetting] 0,
	     'IsAlertEnabled',
	     'TRUE',
	     @Setting_Value OUTPUT   
	
	DECLARE @IsAlertEnabled BIT    
	SET @IsAlertEnabled = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
	                           WHEN 'TRUE' THEN 1
	                           ELSE 0
	                      END  
	
	IF (@IsAlertEnabled = 1)
	BEGIN
	    INSERT INTO AlertProcessHistory
	      (
	        APH_AEH_ID,
	        APH_Site_Code,
	        APH_Type,
	        APH_Message,
	        APE_Received_Date
	      )
	    VALUES
	      (
	        @APH_AEH_ID,
	        @APH_Site_Code,
	        @APH_Type,
	        @APH_Message,
	        @APE_Received_Date
	      )
	END
END
GO