USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_AC_GetAutoCalendarProfiles]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_AC_GetAutoCalendarProfiles]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_AC_GetAutoCalendarProfiles
AS
BEGIN
	SELECT AC.ACP_ID AS AutoCalendarProfile_ID,
	       AC.ACP_Name AS AutoCalendarProfile_Name
	FROM   AutoCalendarProfile AC WITH(NOLOCK)
	WHERE  AC.ACP_Status = 1
	AND ACP_DeletedDate IS NULL
	ORDER BY
	       AC.ACP_Name
END
GO