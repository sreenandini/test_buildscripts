USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetServiceNotesByJobNo]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetServiceNotesByJobNo]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetServiceNotesByJobNo(@Service_Allocated_Job_No INT)
AS
BEGIN
	SELECT stf.Staff_First_Name + ' ' + stf.Staff_Last_Name AS Staff_Name,
	       sn.Service_Notes_Notes,
	       sn.Service_Notes_Date,
	       sn.Service_Notes_ID
	FROM   Service_Notes sn
	       LEFT JOIN Staff stf
	            ON  sn.Staff_ID = stf.Staff_ID
	WHERE  sn.Service_ID = @Service_Allocated_Job_No
	ORDER BY
	       sn.Service_Notes_ID
END
GO
