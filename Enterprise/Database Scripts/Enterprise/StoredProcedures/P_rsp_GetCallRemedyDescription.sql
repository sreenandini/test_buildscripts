USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetCallRemedyDescription]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetCallRemedyDescription]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetCallRemedyDescription
AS
BEGIN
	SELECT Call_Remedy_ID,
	       Call_Remedy_Description
	FROM   Call_Remedy WITH (NOLOCK)
	WHERE  Call_Remedy_End_Date IS NULL
	ORDER BY
	       Call_Remedy_Description
END
GO
