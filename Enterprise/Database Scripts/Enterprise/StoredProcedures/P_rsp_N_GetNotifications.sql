USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_N_GetNotifications]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_N_GetNotifications]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_N_GetNotifications]
AS
BEGIN
	SELECT [ID] AS NotificationID,
	       [Type] AS NotificationItem,
	       [Notifications] AS Notifications,
	       [NotifiedDate] AS NotifiedDate
	FROM   Notifications WITH(NOLOCK)
	WHERE  [IsAcknowledged] = 0
	ORDER BY
	       [ID] DESC
END
GO