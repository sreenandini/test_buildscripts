USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_AC_SendAutoCalendarAlertToSTM]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_AC_SendAutoCalendarAlertToSTM]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_AC_SendAutoCalendarAlertToSTM]
	@SiteCode VARCHAR(10),
	@Type VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @VersionName        VARCHAR(20)
	DECLARE @MessageDateTime    DATETIME  
	DECLARE @XML                XML
	DECLARE @Description        VARCHAR(200)
	DECLARE @STM_ExceptionCode  INT    
	
	SELECT @STM_ExceptionCode = sc.ExceptionCode
	FROM   STMAlertsConfig sc
	WHERE  sc.AlertDescription = 'AUTOCALENDAR'
	
	SELECT TOP 1 @VersionName = VersionName
	FROM   VersionHistory
	ORDER BY
	       1 DESC  
	
	IF (@type = 'Initial Alert')
	BEGIN
	    SET @Description = 
	        'AutoCalendar-Initial Alert,Calendar is going to expire'
	END
	ELSE 
	IF (@type = 'Recurrence Alert')
	BEGIN
	    SET @Description = 
	        'AutoCalendar-Recurrence Alert,Calendar is going to expire'
	END
	ELSE
	BEGIN
	    SET @Description = 'AutoCalendar-Calendar Creation,Calendar created'
	END
	
	SELECT @MessageDateTime = GETDATE()
	SET @XML = (
	        SELECT TOP 1 S.Site_Code,
	               S.Site_Name,
	               'BALLY' AS [Source],
	               @STM_ExceptionCode AS ExceptionCode,
	               '000' AS OperatorId,
	               '' AS SubCode,
	               C.Company_Name AS Company,
	               SC.Sub_Company_Name AS SubCompanyName,
	               ISNULL(SCR.Sub_Company_Region_Name, '') AS [Region],
	               ISNULL(SCA.Sub_Company_Area_Name, '') AS [Area],
	               ISNULL(SCD.Sub_Company_District_Name, '') AS [District],
	               @VersionName AS BMCVersion,
	               @Description AS Calendar,
	               @MessageDateTime AS MessageDateTime
	        FROM   [Site] s WITH (NOLOCK)
	               INNER JOIN Sub_Company SC WITH (NOLOCK)
	                    ON  sc.Sub_Company_ID = s.Sub_Company_ID
	               INNER JOIN Company C WITH (NOLOCK)
	                    ON  c.Company_ID = sc.Company_ID
	               LEFT JOIN Sub_Company_Region SCR WITH (NOLOCK)
	                    ON  SCR.SUB_COMPANY_REGION_ID = S.SUB_COMPANY_REGION_ID
	               LEFT JOIN Sub_Company_Area SCA WITH (NOLOCK)
	                    ON  SCA.SUB_COMPANY_AREA_ID = S.SUB_COMPANY_AREA_ID
	               LEFT JOIN Sub_Company_District SCD WITH (NOLOCK)
	                    ON  scd.Sub_Company_District_ID = s.Sub_Company_District_ID
	        WHERE  s.Site_Code = @SiteCode
	               FOR XML PATH('') ,TYPE, ROOT('BMCRequest')
	    )
	
	INSERT INTO dbo.STM_Export_History
	  (
	    [Type],
	    ClientID,
	    Site_Code,
	    [Message],
	    Received_Date
	  )
	VALUES
	  (
	    'AUTOCALENDAR',
	    1,
	    @SiteCode,
	    @XML,
	    GETDATE()
	  )
END
GO