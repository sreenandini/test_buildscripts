USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetCallGroupDescription]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetCallGroupDescription]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetCallGroupDescription
AS
BEGIN
	SELECT Call_Group_ID,
	       Call_Group_Description
	FROM   Call_Group WITH (NOLOCK)
	WHERE  Call_Group_End_Date IS NULL
	ORDER BY
	       Call_Group_Description
END
GO
