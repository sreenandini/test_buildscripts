USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_AC_CheckAndUpdateNewCalendar]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_AC_CheckAndUpdateNewCalendar]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_AC_CheckAndUpdateNewCalendar(@MinDays INT)
AS
	SET NOCOUNT ON
	BEGIN TRAN
	BEGIN
		SELECT scc.Sub_Company_ID AS Sub_Company_ID,
		       C.Calendar_Year_End_Date AS Calendar_Year_End_Date,
		       DATEDIFF(
		           DAY,
		           GETDATE(),
		           CONVERT(DATETIME, C.Calendar_Year_End_Date, 103)
		       ) AS CalDateDiff,
		       C.Calendar_ID,
		       ACD.ACD_New_Calendar_ID AS ACD_New_Calendar_ID,
		       ACPI.ACPI_ID AS ACPI_ID,
		       ACD_CalendarName AS NewCalendarName
		       INTO #tempFinal
		FROM   AutoCalendarProfileItems ACPI
		       INNER JOIN Sub_Company_Calendar scc
		            ON  scc.Sub_Company_ID = ACPI.ACPI_Sub_Company_ID
		       INNER JOIN Calendar c
		            ON  c.Calendar_ID = scc.Calendar_ID
		       LEFT OUTER JOIN AutoCalendarDetails ACD
		            ON  acd.ACD_New_Calendar_ID = c.Calendar_ID
		WHERE  ACPI.ACPI_IsCalendarExportPending = 1
		       AND ACPI.ACPI_UnAssignedDate IS NULL
		       AND ACD.ACD_CalendarActivatedDate IS NULL
		
		CREATE TABLE #tempSiteTable
		(
			irow      INT IDENTITY(1, 1),
			SiteCode  VARCHAR(10)
		)
		
		DECLARE @CalendarID               INT
		DECLARE @NewCalendarID            INT
		DECLARE @DateTime                 DATETIME
		DECLARE @ACPI_ID                  INT
		DECLARE @NotificationDescription  VARCHAR(2000)
		DECLARE @iCount                   INT = 1
		DECLARE @iSiteCount               INT = 0
		DECLARE @siteCode                 VARCHAR(10)
		DECLARE @SiteName                 VARCHAR(50)
		DECLARE @NewCalendarName          VARCHAR(50)
		DECLARE @SiteXml                  XML 
		DECLARE @StatusMsg                VARCHAR(2000)
		
		DECLARE @SubCompanyID             INT
		DECLARE @NextSubCompanyID         CURSOR                                          
		SET @NextSubCompanyID =  CURSOR FOR 
		SELECT DISTINCT Sub_Company_ID
		FROM   #Tempfinal t
		WHERE  t.CalDateDiff <= @MinDays
		
		OPEN @NextSubCompanyID
		FETCH NEXT
		FROM @NextSubCompanyID INTO @SubCompanyID
		WHILE @@FETCH_STATUS = 0
		BEGIN
		    FETCH NEXT
		    FROM @NextSubCompanyID INTO @SubCompanyID
		    
		    SET @iCount = 1
		    
		    SELECT @ACPI_ID = ACPI_ID,
		           @CalendarID = Calendar_ID,
		           @NewCalendarID = ACD_New_Calendar_ID,
		           @NewCalendarName = NewCalendarName
		    FROM   #tempFinal
		    WHERE  Sub_Company_ID = @SubCompanyID
		    
		    TRUNCATE TABLE #tempSiteTable
		    SET @DateTime = GETDATE()
		    INSERT INTO #tempSiteTable
		      (
		        SiteCode
		      )
		    SELECT Site_Code
		    FROM   #Tempfinal
		    WHERE  Sub_Company_ID = @SubCompanyID
		    GROUP BY
		           Site_Code
		    
		    EXEC Usp_UpdateSubComapnyCalendar @SubCompanyID
		    
		    --This will make the calendar active
		    EXEC Usp_InsertNewSubCompanyCalendar @SubCompanyID,
		         @NewCalendarID,
		         1
		    
		    INSERT INTO dbo.Export_History
		      (
		        EH_Date,
		        EH_Reference1,
		        EH_Type,
		        EH_Site_Code
		      )
		    SELECT GETDATE(),
		           @NewCalendarID,
		           'S-CALENDAR',
		           Site_Code
		    FROM   #Tempfinal
		    WHERE  Sub_Company_ID = @SubCompanyID
		    
		    --As calendar is set to active and exported to site, resetting the values
		    UPDATE AutoCalendarProfileItems
		    SET    ACPI_AlertStatus = 0,
		           ACPI_LastAlertSentDate = NULL,
		           ACPI_LastRecurrenceDate = NULL,
		           ACPI_IsCalendarExportPending = 1
		    WHERE  ACPI_ID = @ACPI_ID
		    
		    --updates ACD_CalendarActivatedDate to the date of calendar set to active
		    UPDATE AutoCalendarDetails
		    SET    ACD_CalendarActivatedDate = GETDATE()
		    WHERE  ACD_Previous_Calendar_ID = @CalendarID
		           AND ACD_New_Calendar_ID = @NewCalendarID
		           AND ACD_CalendarName = @NewCalendarName    
		    
		    SELECT @iSiteCount = COUNT(irow)
		    FROM   #tempSiteTable
		    
		    WHILE (@iSiteCount >= @iCount)
		    BEGIN
		        SELECT @siteCode = SiteCode
		        FROM   #tempSiteTable
		        WHERE  irow = @iCount
		        
		        SELECT @SiteXml = (
		                   SELECT 'AutoCalendar' AS AlertType,
		                          @SiteCode AS Sitecode,
		                          'Created a new calendar ' + @NewCalendarName +
		                          ' for SubCompany' AS AlertMessage,
		                          'NA' AS [User]
		                          FOR XML PATH('Alert')
		               )
		        
		        SELECT @SiteName = Site_Name
		        FROM   [SITE]
		        WHERE  Site_Code = @SiteCode
		        
		        SET @StatusMsg = 'Calendar ' + @NewCalendarName +
		            ' creation completed and assigned to site - Completion Alert'
		        
		        SET @NotificationDescription = 'New Calendar ' + @NewCalendarName 
		            + ' is created and assigned to site : ' + CAST(@SiteCode AS VARCHAR(10)) 
		            + ' Site :' + @SiteName
		        
		        EXEC usp_AC_SendAutoCalendarAlertToSTM @siteCode,
		             @StatusMsg		             
		        
		        EXEC usp_N_InsertIntoNotifications 'Calendar',
		             @NotificationDescription
		        
		        EXEC usp_AC_UpdateAlertProcessHistory 0,
		             @SiteCode,
		             'AutoCalendar',
		             @SiteXml,
		             @DateTime
		        
		        SET @iCount = @iCount + 1
		    END
		END
		CLOSE @NextSubCompanyID
		DEALLOCATE @NextSubCompanyID
	END
	IF (@@ERROR <> 0)
	BEGIN
	    ROLLBACK TRAN
	END
	ELSE
	BEGIN
	    COMMIT TRAN
	END
GO