USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_N_GetNotificationsCount]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_N_GetNotificationsCount]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_N_GetNotificationsCount]
AS
BEGIN
	DECLARE @ResultCount INT = 0
	SELECT @ResultCount = COUNT(ID)
	FROM   Notifications WITH(NOLOCK)
	WHERE  IsAcknowledged = 0
	
	RETURN @ResultCount
END
GO