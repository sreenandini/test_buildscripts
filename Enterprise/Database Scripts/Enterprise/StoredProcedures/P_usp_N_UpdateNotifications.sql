USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_N_UpdateNotifications]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_N_UpdateNotifications]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_N_UpdateNotifications]
	@NotificationIds VARCHAR(MAX),
	@AcknowledgedUserID INT
AS
BEGIN
	UPDATE Notifications
	SET    [IsAcknowledged] = 1,
	       [AcknowledgedUserID] = @AcknowledgedUserID,
	       [AcknowledgedDate] = GETDATE()
	FROM   notifications
	WHERE  [Id] IN (SELECT IntItem
	                FROM   dbo.Fn_GetIntTableFromStringList(@NotificationIds))
END
GO