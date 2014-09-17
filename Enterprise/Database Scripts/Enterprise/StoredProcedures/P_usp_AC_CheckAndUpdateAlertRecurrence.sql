USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_AC_CheckAndUpdateAlertRecurrence]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_AC_CheckAndUpdateAlertRecurrence]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_AC_CheckAndUpdateAlertRecurrence
AS
	SET NOCOUNT ON
	BEGIN TRAN
	
	/*********************************************************
	* Making a join with the relative tables to check whether 
	* the calendar has reached the limit of Alert before days.
	* And insert the details into a temp table #Tempfinal.
	* The Temp Final table holds the details of the calendar
	* details along with the date difference  and subcompanies 
	* associated with the calendar.
	* ********************************************************/

	SELECT AC.ACP_ID AS ACP_ID,
	       ACPI.ACPI_LastAlertSentDate AS ACPI_LastAlertSentDate,	--this column is used to compare with the CalReccurDateDiff.
	       AC.ACP_AlertRecurrence AS ACP_AlertRecurrence,
	       ACPI.ACPI_LastRecurrenceDate AS ACPI_LastRecurrenceDate,
	       ACPI.ACPI_AlertStatus AS ACPI_AlertStatus,
	       sc.Sub_Company_ID AS Sub_Company_ID,
	       s.Site_Code AS Site_Code,
	       DATEDIFF(DAY,CONVERT(DATETIME,ISNULL(ACPI.ACPI_LastRecurrenceDate, ACPI.ACPI_LastAlertSentDate),103),
	           CONVERT(DATETIME, GETDATE(), 103)) AS CalReccurDateDiff --this column is used to compare with the ACP_LastAlertSentDate.
	       INTO #Tempfinal
	FROM   AutoCalendarProfile AC
	       INNER JOIN AutoCalendarProfileItems ACPI
	            ON  ACPI.ACPI_ACP_ID = AC.ACP_ID
	            AND ACPI.ACPI_UnAssignedDate IS NULL
	            AND ACPI.ACPI_AlertStatus = 1
	            AND ACPI.ACPI_IsCalendarExportPending = 0
	            AND AC.ACP_IsEnabled = 1
	            AND AC.ACP_Status = 1
	       INNER JOIN Sub_Company sc
	            ON  sc.Sub_Company_ID = ACPI.ACPI_Sub_Company_ID
	       INNER JOIN Sub_Company_Calendar scc
	            ON  sc.Sub_Company_ID = scc.Sub_Company_ID
	            AND scc.Sub_Company_Calendar_Active = 1
	       INNER JOIN Calendar c
	            ON  c.Calendar_ID = scc.Calendar_ID
	       INNER JOIN [SITE] s
	            ON  s.Sub_Company_ID = sc.Sub_Company_ID
	            
	--SELECT * FROM   #Tempfinal t
	DECLARE @SiteName VARCHAR(50)
	DECLARE @SiteCode   INT
	DECLARE @getSiteID  CURSOR           
	DECLARE @SiteXml    XML
	DECLARE @DateTime   DATETIME
	DECLARE @NotificationDescription VARCHAR(200)
	SET @getSiteID =   CURSOR FOR 
	SELECT Site_code
	FROM   #Tempfinal t
	WHERE  t.CalReccurDateDiff >= t.ACP_AlertRecurrence
	       AND t.ACPI_AlertStatus = 1
	
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
	                      'Create a new calendar for SubCompany' AS AlertMessage,
	                      'NA' AS [User]
	                      FOR XML PATH('Alert')
	           )
	    
	    --Inserting an entry into the AlertProcessHistory table to send mail alert
	    EXEC usp_AC_UpdateAlertProcessHistory 0,
	         @SiteCode,
	         'AutoCalendar',
	         @SiteXml,
	         @DateTime
	         
	    SELECT @SiteName = Site_Name FROM [SITE] WHERE Site_Code = @SiteCode       
	         
	    EXEC usp_AC_SendAutoCalendarAlertToSTM @SiteCode,'Recurrence Alert'
	         
	    SET @NotificationDescription = 'Recurrence Alert - Calendar is going to expire for the site : ' + CAST(@SiteCode AS VARCHAR(10)) + ' Site Name -' + @SiteName
	         
	    EXEC usp_N_InsertIntoNotifications 'Calendar',@NotificationDescription
	    
	    FETCH NEXT
	    FROM @getSiteID INTO @SiteCode
	END
	
	CLOSE @getSiteID
	DEALLOCATE @getSiteID
	
	/**********************************************************
	* Once the recurrence alert is sent, ACP_LastAlertSentDate 
	* and ACP_LastRecurrenceDate will be updated, with which the 
	* next recurrence will be calculated for the profiles which 
	* are included in the #Tempfinal table
	*********************************************************/
	UPDATE AutoCalendarProfileItems
	SET    ACPI_LastRecurrenceDate = GETDATE(),
	       ACPI_LastAlertSentDate = GETDATE()
	WHERE  ACPI_ACP_ID IN (SELECT DISTINCT ACP_ID
	                       FROM   #Tempfinal tf
	                       WHERE  tf.CalReccurDateDiff >= tf.ACP_AlertRecurrence
	                              AND tf.ACPI_AlertStatus = 1)
	       AND ACPI_Sub_Company_ID IN (SELECT DISTINCT Sub_Company_ID
	                                   FROM   #Tempfinal tf
	                                   WHERE  tf.CalReccurDateDiff >= tf.ACP_AlertRecurrence
	                                          AND tf.ACPI_AlertStatus = 1)
	
	IF (@@ERROR <> 0)
	BEGIN
	    ROLLBACK TRAN
	END
	ELSE
	BEGIN
	    COMMIT TRAN
	END
GO