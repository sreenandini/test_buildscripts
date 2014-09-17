USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_N_InsertIntoNotifications]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_N_InsertIntoNotifications]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_N_InsertIntoNotifications]
	@Items VARCHAR(200),
	@notifications VARCHAR(5000)
AS
BEGIN
	INSERT INTO Notifications
	  (
	    [Type],
	    [Notifications],
	    [NotifiedDate],
	    [IsAcknowledged]
	  )
	VALUES
	  (
	    @Items,
	    @notifications,
	    GETDATE(),
	    0
	  )
END
GO