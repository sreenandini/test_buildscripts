USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_AC_CheckAndUpdateAutoCalendar]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_AC_CheckAndUpdateAutoCalendar]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_AC_CheckAndUpdateAutoCalendar
AS
	SET NOCOUNT ON
	BEGIN TRAN
	BEGIN
		/*********************************************************
		* Making a join with the relative tables to check whether 
		* the calendar has reached the limit of Alert before days.
		* And insert the details into a temp table #Tempfinal.
		* The Temp Final table holds the details of the calendar
		* details along with the date difference  and subcompanies 
		* associated with the calendar.
		* ********************************************************/
		
		
		SELECT sc.Sub_Company_ID AS Sub_Company_ID,
		       c.Calendar_ID AS Calendar_ID,
		       c.Calendar_Year_Start_Date AS Calendar_Year_Start_Date,
		       c.Calendar_Year_End_Date AS Calendar_Year_End_Date,
		       DATEDIFF(
		           DAY,
		           CONVERT(DATETIME, GETDATE(), 103),
		           CONVERT(DATETIME, c.Calendar_Year_End_Date, 103)
		       ) AS CalDateDiff,	-- this column is used to check the current day difference.
		       AC.ACP_ID AS ACP_ID,
		       ACPI.ACPI_Sub_Company_ID AS ACPI_Sub_Company_ID,
		       AC.ACP_CreateBeforeDays AS ACP_CreateBeforeDays,
		       AC.ACP_AlertBefore AS ACP_AlertBefore,	-- this column is used to compare with the CalDateDiff.
		       ACPI.ACPI_AlertStatus AS ACPI_AlertStatus,	--this column is used to check whether already an alert is sent.
		       AC.ACP_AlertRecurrence AS ACP_AlertRecurrence,
		       ACPI.ACPI_LastAlertSentDate AS ACPI_LastAlertSentDate
		       INTO #Tempfinal
		FROM   Sub_Company sc WITH (NOLOCK)
		       INNER JOIN Sub_Company_Calendar scc WITH (NOLOCK)
		            ON  scc.Sub_Company_ID = sc.Sub_Company_ID
		       INNER JOIN Calendar c WITH (NOLOCK)
		            ON  c.Calendar_ID = scc.Calendar_ID
		       INNER JOIN AutoCalendarProfileItems ACPI WITH (NOLOCK)
		            ON  ACPI.ACPI_Sub_Company_ID = sc.Sub_Company_ID
		       INNER JOIN AutoCalendarProfile AC WITH (NOLOCK)
		            ON  AC.ACP_ID = ACPI.ACPI_ACP_ID
		WHERE  scc.Sub_Company_Calendar_Active = 1
		       AND ACPI.ACPI_UnAssignedDate IS NULL
		       AND AC.ACP_Status = 1
		       AND ACPI.ACPI_IsCalendarExportPending = 0
		       AND ACPI.ACPI_AlertStatus = 0
		       AND ACPI.ACPI_LastAlertSentDate IS NULL
			   AND AC.ACP_IsEnabled = 1
		
		--SELECT * FROM   #Tempfinal
		
		/********************************************************
		* With the #tempFinal table , initially check whether the
		* CalDateDiff is matching and for the same subcompany and 
		* ACP_AlertStatus = 0(initial alert not sent) 
		********************************************************/
		
		DECLARE @tempSite TABLE (SiteCode INT)
		
		INSERT INTO @tempSite
		SELECT s.Site_Code
		FROM   [SITE] s
		       INNER JOIN #Tempfinal tf
		            ON  tf.Sub_Company_ID = s.Sub_Company_ID
		            AND tf.CalDateDiff <= tf.ACP_AlertBefore
		            AND tf.ACPI_AlertStatus = 0
		            AND tf.ACPI_LastAlertSentDate IS NULL
		
		--SELECT Sitecode FROM   @tempSite
		DECLARE @SiteName                 VARCHAR(50)
		DECLARE @SiteCode                 INT
		DECLARE @getSiteID                CURSOR                  
		DECLARE @SiteXml                  XML
		DECLARE @NotificationDescription  VARCHAR(200)
		DECLARE @DateTime                 DATETIME
		SET @getSiteID =  CURSOR FOR 
		SELECT Sitecode
		FROM   @tempSite
		
		OPEN @getSiteID
		FETCH NEXT
		FROM @getSiteID INTO @SiteCode
		WHILE @@FETCH_STATUS = 0
		BEGIN
		    --Framing an XML to insert into the AlertProcessHistory table
		    SET @DateTime = GETDATE()
		    SELECT @SiteXml = (
		               SELECT 'AutoCalendar' AS AlertType,
		                      @SiteCode AS Sitecode,
		                      'Create a new calendar for SubCompany' AS 
		                      AlertMessage,
		                      'NA' AS [User]
		                      FOR XML PATH('Alert')
		           )
		    
		    --Inserting an entry into the AlertProcessHistory table to send mail alert
		    EXEC usp_AC_UpdateAlertProcessHistory 0,
		         @SiteCode,
		         'AutoCalendar',
		         @SiteXml,
		         @DateTime
		    
		    SELECT @SiteName = Site_Name
		    FROM   SITE
		    WHERE  Site_Code = @SiteCode
		    
		    EXEC usp_AC_SendAutoCalendarAlertToSTM @SiteCode,
		         'Initial Alert'
		    
		    SET @NotificationDescription = 
		        'Initial Alert - Calendar is going to expire for the site : ' + 
		        CAST(@SiteCode AS VARCHAR(10)) + ' SiteName : ' + @SiteName
		    
		    EXEC usp_N_InsertIntoNotifications 'Calendar',
		         @NotificationDescription
		    
		    FETCH NEXT
		    FROM @getSiteID INTO @SiteCode
		END
		CLOSE @getSiteID
		DEALLOCATE @getSiteID
		
		--Activate the alert status
		
		/********************************************************
		* Below we update the columns to maintain a status that an 
		* initial alert has been sent. 
		* ******************************************************/
		
		UPDATE AutoCalendarProfileItems
		SET    ACPI_AlertStatus = 1,
		       ACPI_LastAlertSentDate = GETDATE()
		WHERE  ACPI_ACP_ID IN (SELECT ACPI_ACP_ID
		                       FROM   #Tempfinal)
		       AND ACPI_Sub_Company_ID IN (SELECT DISTINCT Sub_Company_ID
		                                   FROM   #Tempfinal tf
		                                   WHERE  tf.CalDateDiff <= tf.ACP_AlertBefore
		                                          AND tf.ACPI_AlertStatus = 0
		                                          AND tf.ACPI_LastAlertSentDate 
		                                              IS NULL)
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