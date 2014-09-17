USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_AC_CheckAndCreateCalendar]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_AC_CheckAndCreateCalendar]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_AC_CheckAndCreateCalendar
AS
	SET NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
		DECLARE @NewCalendarID INT
		
		/*************************************************************
		* Making a join with the relative tables to check whether the 
		* calendar has reached the limit of calendar create before 
		* days. And insert the details into a temp table #Tempfinal.
		* The Temp Final table holds the details of the calendar
		* details along with the date difference and subcompanies and
		* sites associated with the calendar.
		* ********************************************************/
		SELECT ACPI.ACPI_ID AS ACPI_ID,
		       C.Calendar_ID AS Calendar_ID,
		       ACP1.ACP_ID AS ACP_ID,
		       ACP1.ACP_IsCalendarBasedOnDays AS IsDaysBased,
		       ACP1.ACP_NewCalendarDayID AS CalendarStartDay,
		       ACP1.ACP_SetNewCalendarActive AS SetNewCalendarActive,
		       ACPI.ACPI_AlertStatus AS ACPI_AlertStatus,
		       SC.Sub_Company_ID AS Sub_Company_ID,
		       SC.Sub_Company_Name AS Sub_Company_Name,
		       DATEDIFF(
		           DAY,
		           CONVERT(DATETIME, DATEADD(DAY, 1, GETDATE()), 103),
		           CONVERT(DATETIME, C.Calendar_Year_End_Date, 103)
		       ) AS CalDateDiff,
		       ACP1.ACP_CreateBeforeDays AS ACP_CreateBeforeDays,
		       S.Site_Code AS Site_Code,
		       S.Site_ID AS Site_ID
		       INTO #Tempfinal
		FROM   AutoCalendarProfile ACP1 WITH(NOLOCK)
		       INNER JOIN AutoCalendarProfileItems ACPI WITH(NOLOCK)
		            ON  ACPI.ACPI_ACP_ID = ACP1.ACP_ID
		            AND ACPI.ACPI_UnAssignedDate IS NULL
		            AND ACPI.ACPI_AlertStatus = 1
		            AND ACPI.ACPI_IsCalendarExportPending = 0
		            AND ACP1.ACP_IsEnabled = 1
		            AND ACP1.ACP_Status = 1
		       INNER JOIN Sub_Company sc WITH(NOLOCK)
		            ON  SC.Sub_Company_ID = ACPI.ACPI_Sub_Company_ID
		       INNER JOIN Sub_Company_Calendar scc WITH(NOLOCK)
		            ON  SC.Sub_Company_ID = SCC.Sub_Company_ID
		            AND SCC.Sub_Company_Calendar_Active = 1
		       INNER JOIN Calendar C WITH(NOLOCK)
		            ON  C.Calendar_ID = SCC.Calendar_ID
		       INNER JOIN [SITE] S WITH(NOLOCK)
		            ON  S.Sub_Company_ID = SC.Sub_Company_ID
		
		--SELECT * FROM   #tmpDetails
		
		/*************************************************
		* This table is created to get the sites,the 
		* calender is created. to send STM alert, 
		**************************************************/
		CREATE TABLE #tempSiteTable
		(
			irow      INT IDENTITY(1, 1),
			SiteCode  VARCHAR(10)
		)
		
		DECLARE @SubCompanyID             INT
		DECLARE @iCount                   INT = 1
		DECLARE @siteCode                 VARCHAR(10)
		DECLARE @SiteName                 VARCHAR(50)
		DECLARE @iSiteCount               INT = 0
		DECLARE @CalendarID               INT
		DECLARE @IsDaysBased              BIT
		DECLARE @CalendarStartDay         INT
		DECLARE @ACP_ID                   INT
		DECLARE @SetNewCalendarActive     BIT
		DECLARE @SubCompanyName           VARCHAR(50)
		DECLARE @NotificationDescription  VARCHAR(2000)
		DECLARE @SiteXml                  XML
		DECLARE @StatusMsg                VARCHAR(2000)
		DECLARE @DateTime                 DATETIME
		DECLARE @ACPI_ID                  INT
		DECLARE @NewCalendarName          VARCHAR(50)
		
		DECLARE @NextSubCompanyID         CURSOR             
		SET @NextSubCompanyID =  CURSOR FOR 
		SELECT DISTINCT Sub_Company_ID
		FROM   #Tempfinal t
		WHERE  t.CalDateDiff <= t.ACP_CreateBeforeDays
		
		OPEN @NextSubCompanyID
		FETCH NEXT
		FROM @NextSubCompanyID INTO @SubCompanyID
		WHILE @@FETCH_STATUS = 0
		BEGIN
		    SET @NewCalendarID = 0
		    SET @iCount = 1
		    
		    SELECT TOP 1 @IsDaysBased = IsDaysBased,
		           @CalendarStartDay = CalendarStartDay,
		           @ACP_ID = ACP_ID,
		           @SubCompanyName = Sub_Company_Name,
		           @SetNewCalendarActive = SetNewCalendarActive,
		           @CalendarID = Calendar_ID,
		           @ACPI_ID = ACPI_ID
		    FROM   #Tempfinal
		    WHERE  Sub_Company_ID = @SubCompanyID
		    
		    EXEC usp_C_CreateCalendar @CalendarID,
		         @IsDaysBased,
		         @CalendarStartDay,
		         @ACP_ID,
		         @NewCalendarID OUTPUT,
		         @NewCalendarName OUTPUT
		    
		    IF (@NewCalendarID <> 0)
		    BEGIN
		        /*Only if the SetNewCalendarActive is true, the new calendar will be set to Active,*/
		        IF (@SetNewCalendarActive = 1)
		        BEGIN
		            --This will make the active calendars to inactive
		            EXEC Usp_UpdateSubComapnyCalendar @SubCompanyID
		            
		            --This will make the calendar active
		            EXEC Usp_InsertNewSubCompanyCalendar @SubCompanyID,
		                 @NewCalendarID,
		                 @SetNewCalendarActive
		            
		            INSERT INTO dbo.Export_History
		              (
		                EH_Date,
		                EH_Reference1,
		                EH_Type,
		                EH_Site_Code
		              )
		            SELECT GETDATE(),
		                   Site_ID,
		                   'S-CALENDAR',
		                   Site_Code
		            FROM   #Tempfinal
		            WHERE  Sub_Company_ID = @SubCompanyID
		            
		            --As calendar is set to active and exported to site, resetting the values
		            UPDATE AutoCalendarProfileItems
		            SET    ACPI_AlertStatus = 0,
		                   ACPI_LastAlertSentDate = NULL,
		                   ACPI_LastRecurrenceDate = NULL,
		                   ACPI_IsCalendarExportPending = 0
		            WHERE  ACPI_ID = @ACPI_ID
		            
		            UPDATE AutoCalendarDetails
		            SET    ACD_CalendarActivatedDate = GETDATE()
		            WHERE  ACD_Previous_Calendar_ID = @CalendarID
		                   AND ACD_New_Calendar_ID = @NewCalendarID
		                   AND ACD_CalendarName = @NewCalendarName
		        END
		        ELSE
		        BEGIN
		            UPDATE AutoCalendarProfileItems
		            SET    ACPI_IsCalendarExportPending = 1
		            WHERE  ACPI_ID = @ACPI_ID
		        END
		        
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
		                              'Created a new calendar ' + @NewCalendarName 
		                              + ' for SubCompany "' + @SubCompanyName +'"' AS AlertMessage,
		                              'NA' AS [User]
		                              FOR XML PATH('Alert')
		                   )
		            
		            SELECT @SiteName = Site_Name
		            FROM   [SITE]
		            WHERE  Site_Code = @SiteCode
		            
		            IF (@SetNewCalendarActive = 1)
		            BEGIN
		                SET @StatusMsg = 'Calendar ' + @NewCalendarName + 
		                    ' creation completed and assigned to site - Completion Alert'
		                
		                SET @NotificationDescription = 'New Calendar ' + @NewCalendarName 
		                    + ' is created and assigned to site : ' + CAST(@SiteCode AS VARCHAR(10)) 
		                    + ' Site :' + @SiteName
		            END
		            ELSE
		            BEGIN
		                SET @StatusMsg = 'Calendar  ' + @NewCalendarName + 
		                    ' creation completed and not assigned to site - Completion Alert'
		                
		                SET @NotificationDescription = 'New Calendar ' + @NewCalendarName 
		                    + ' is created and not assigned to site : ' + CAST(@SiteCode AS VARCHAR(10)) 
		                    + ' Site :' + @SiteName
		            END
		            
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
		    
		    FETCH NEXT
		    FROM @NextSubCompanyID INTO @SubCompanyID
		END
		CLOSE @NextSubCompanyID
		DEALLOCATE @NextSubCompanyID
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
GO