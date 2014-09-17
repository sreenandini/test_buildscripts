/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 06/05/13 4:25:59 PM
 ************************************************************/

-- =============================================
-- Create basic stored procedure template
-- =============================================

-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_GetVSStaffDetails'
   )
    DROP PROCEDURE dbo.rsp_GetVSStaffDetails
GO

CREATE PROCEDURE dbo.rsp_GetVSStaffDetails
AS
BEGIN
	SELECT Staff_ID,
	       Staff_First_Name + ' ' + Staff_Last_Name AS Staff_Name
	FROM   Staff
	WHERE  Staff_IsARepresentative = 1
END
GO
