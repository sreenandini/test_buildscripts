USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_AC_GetAutoCalendarProfilesDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_AC_GetAutoCalendarProfilesDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
EXEC rsp_AC_GetAutoCalendarProfilesDetails 4
*/

CREATE PROCEDURE rsp_AC_GetAutoCalendarProfilesDetails
	@AutoCalendarProfile_ID AS INT 
AS
BEGIN
	SELECT AC.ACP_ID AS AutoCalendarProfile_ID,
	       AC.ACP_Name AS AutoCalendarProfile_Name,
	       AC.ACP_IsEnabled AS IsAutoCalendarEnabled,
	       AC.ACP_CreateBeforeDays AS CalendarCreateBeforeDays,
	       AC.ACP_AlertBefore AS CalendarAlertBefore,
	       AC.ACP_AlertRecurrence AS CalendarAlertRecurrence,
	       ACP_IsCalendarBasedOnDays AS IsCalendarBasedOnDays,
	       ACP_NewCalendarDayID AS NewCalendarDayID,
	       ACP_SetNewCalendarActive AS SetNewCalendarActive
	FROM   AutoCalendarProfile ac WITH (NOLOCK)
	WHERE  AC.ACP_ID = @AutoCalendarProfile_ID
	
	SELECT SC.Sub_Company_ID,
	       SC.Sub_Company_Name,
	       ACPI.ACPI_ACP_ID,
	       CASE 
	            WHEN ACPI.ACPI_ACP_ID = @AutoCalendarProfile_ID
	                 AND ACPI.ACPI_UnAssignedDate IS NULL THEN 1 --subcompnay active for that profile
	            WHEN ACPI.ACPI_ACP_ID <> @AutoCalendarProfile_ID 
	                 AND ACPI.ACPI_UnAssignedDate IS NULL THEN 2 --subcompnay active for othr profile
	            ELSE 3
	       END AS Profilestatus
	FROM   Sub_Company SC
	       LEFT JOIN AutoCalendarProfileItems ACPI
	            ON  ACPI.ACPI_Sub_Company_ID = sc.Sub_Company_ID
	            AND ACPI.ACPI_UnAssignedDate IS NULL
	       INNER JOIN Sub_Company_Calendar scc
	            ON  scc.Sub_Company_ID = SC.Sub_Company_ID
	            AND scc.Sub_Company_Calendar_Active = 1
	WHERE  ACPI.ACPI_UnAssignedDate IS NULL
	ORDER BY
	       SC.Sub_Company_Name
END
GO