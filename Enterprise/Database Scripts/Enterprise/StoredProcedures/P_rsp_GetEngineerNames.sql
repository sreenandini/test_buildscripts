USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetEngineerNames]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetEngineerNames]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetEngineerNames(@Staff_ID INT = 0)
AS
BEGIN
	SELECT s.Staff_ID,
	       (s.Staff_First_Name + ' ' + s.Staff_Last_Name) AS Staff_Name
	FROM   Staff s
	WHERE  (
	           (
	               @Staff_ID = 0
	               AND Staff_IsAnEngineer = 'True'
	               AND Staff_Terminated = 'False'
	           )
	           OR (@Staff_ID <> 0 AND s.Staff_ID = @Staff_ID)
	       )
	ORDER BY
	       s.Staff_First_Name,
	       s.Staff_Last_Name
END
GO
