USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_AC_UpdateAutoCalendarProfiles]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_AC_UpdateAutoCalendarProfiles]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_AC_UpdateAutoCalendarProfiles
	@AutoCalendarProfile_ID INT,
	@AutoCalendarProfile_Name VARCHAR(20),
	@IsAutoCalendarEnabled BIT = 0,
	@CalendarCreateBeforeDays INT = 0,
	@CalendarAlertBefore INT = 0,
	@CalendarAlertRecurrence INT = 0,
	@IsCalendarBasedOnDays BIT = 0,
	@NewCalendarDayID INT = 0,
	@SetNewCalendarActive BIT = 0,
	@AssignProfiles VARCHAR(2000) = '',
	@OperationType VARCHAR(20)
AS
	BEGIN TRAN 
	BEGIN
		DECLARE @ProfileID  INT = 0
		DECLARE @SC_List       TABLE(SC_ID INT)
		INSERT INTO @SC_List
		  (
		    SC_ID
		  )(
		       SELECT IntItem
		       FROM   dbo.Fn_GetIntTableFromStringList(@AssignProfiles)
		   ) 
		
		IF (UPPER(@OperationType) = 'DELETE')
		BEGIN
		    UPDATE AutoCalendarProfileItems
		    SET    ACPI_UnAssignedDate = GETDATE()
		    WHERE  ACPI_ACP_ID = @AutoCalendarProfile_ID		   
		    
		    UPDATE AutoCalendarProfile
		    SET    ACP_DeletedDate = GETDATE(),
		           ACP_Status = 0
		    WHERE  ACP_ID = @AutoCalendarProfile_ID
		END
		ELSE
		BEGIN
		    IF NOT EXISTS(
		           SELECT 1
		           FROM   AutoCalendarProfile ac WITH (NOLOCK)
		           WHERE  AC.ACP_ID = @AutoCalendarProfile_ID
		       )
		    BEGIN
		        INSERT INTO AutoCalendarProfile
		          (
		            ACP_Name,
		            ACP_IsEnabled,
		            ACP_CreateBeforeDays,
		            ACP_AlertBefore,
		            ACP_AlertRecurrence,
		            ACP_IsCalendarBasedOnDays,
		            ACP_NewCalendarDayID,
		            ACP_Status,
		            ACP_CreatedDate,
		            ACP_SetNewCalendarActive
		          )
		        VALUES
		          (
		            @AutoCalendarProfile_Name,
		            @IsAutoCalendarEnabled,
		            @CalendarCreateBeforeDays,
		            @CalendarAlertBefore,
		            @CalendarAlertRecurrence,
		            @IsCalendarBasedOnDays,
		            @NewCalendarDayID,
		            1,
		            GETDATE(),
		            @SetNewCalendarActive
		          )
		        
		        SET @ProfileID = SCOPE_IDENTITY()
		    END
		    ELSE
		    BEGIN
		        IF EXISTS(
		               SELECT 1
		               FROM   AutoCalendarProfile acp1
		               WHERE  acp1.ACP_AlertBefore <> @CalendarAlertBefore
		                      AND acp1.ACP_ID = @AutoCalendarProfile_ID
		                      OR acp1.ACP_IsEnabled = 0
		           )
		        BEGIN
		            UPDATE AutoCalendarProfileItems
		            SET    ACPI_LastAlertSentDate = NULL,
		                   ACPI_LastRecurrenceDate = NULL
		            WHERE  ACPI_ACP_ID = @AutoCalendarProfile_ID
		                   AND ACPI_UnAssignedDate IS NULL
		        END
		        
		        
		        --Updating AutoCalendarProfile
		        UPDATE AutoCalendarProfile
		        SET    ACP_Name = @AutoCalendarProfile_Name,
		               ACP_IsEnabled = @IsAutoCalendarEnabled,
		               ACP_CreateBeforeDays = @CalendarCreateBeforeDays,
		               ACP_AlertBefore = @CalendarAlertBefore,
		               ACP_AlertRecurrence = @CalendarAlertRecurrence,
		               ACP_IsCalendarBasedOnDays = @IsCalendarBasedOnDays,
		               ACP_NewCalendarDayID = @NewCalendarDayID,
		               ACP_SetNewCalendarActive = @SetNewCalendarActive
		        WHERE  ACP_ID = @AutoCalendarProfile_ID
		        
		        --Updating AutoCalendarProfileItems
		        UPDATE AutoCalendarProfileItems
		        SET    ACPI_UnAssignedDate = GETDATE()
		        WHERE  ACPI_Sub_Company_ID NOT IN (SELECT SC_ID
		                                           FROM   @SC_List)
		               AND ACPI_ACP_ID = @AutoCalendarProfile_ID
		        
		        UPDATE AutoCalendarProfileItems
		        SET    ACPI_UnAssignedDate = GETDATE()
		        WHERE  ACPI_Sub_Company_ID IN (SELECT SC_ID
		                                       FROM   @SC_List)
		               AND ACPI_ACP_ID <> @AutoCalendarProfile_ID
		        
		        INSERT INTO AutoCalendarProfileItems
		          (
		            ACPI_ACP_ID,
		            ACPI_Sub_Company_ID,
		            ACPI_AssignedDate,
		            ACPI_UnAssignedDate
		          )
		        SELECT @AutoCalendarProfile_ID,
		               SC.SC_ID,
		               GETDATE(),
		               NULL
		        FROM   @SC_List SC
		               LEFT JOIN AutoCalendarProfileItems acpi
		                    ON  acpi.ACPI_Sub_Company_ID = SC.SC_ID
		                    AND ACPI.ACPI_ACP_ID = @AutoCalendarProfile_ID
		                    AND ACPI.ACPI_UnAssignedDate IS NULL
		        WHERE  ACPI.ACPI_ID IS NULL
		        
		        SET @ProfileID = @AutoCalendarProfile_ID
		    END
		END
		
		IF (@@ERROR <> 0)
		BEGIN
		    ROLLBACK TRAN
		    RETURN @ProfileID
		END
		ELSE
		BEGIN
		    COMMIT TRAN
		    RETURN @ProfileID
		END
	END
GO