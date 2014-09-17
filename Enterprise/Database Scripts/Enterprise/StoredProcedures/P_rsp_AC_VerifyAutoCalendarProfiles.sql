USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_AC_VerifyAutoCalendarProfiles]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_AC_VerifyAutoCalendarProfiles]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_AC_VerifyAutoCalendarProfiles
	@AutoCalendarProfile_Name AS VARCHAR(20),
	@AutoCalendarProfile_ID INT
	AS
BEGIN
	SET NOCOUNT ON
	IF EXISTS (
	       SELECT 1
	       FROM   AutoCalendarProfile AC
	       WHERE  RTRIM(LTRIM(AC.ACP_Name)) = RTRIM(LTRIM(@AutoCalendarProfile_Name))
	              AND AC.ACP_ID <> @AutoCalendarProfile_ID
	              AND AC.ACP_DeletedDate IS NULL
	   )
	BEGIN
	    RETURN 1
	END
	ELSE
	BEGIN
	    RETURN 0
	END
END
GO