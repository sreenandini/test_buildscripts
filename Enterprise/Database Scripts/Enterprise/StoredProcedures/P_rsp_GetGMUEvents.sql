USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetGMUEvents]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetGMUEvents]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetGMUEvents
AS
	SELECT GMUEventID,
	       GMUEventName,
	       GMUEventGroupID,
	       Event_Fault_Source,
	       Event_Fault_Type	       
	FROM   GMUEvents WITH(NOLOCK)
GO



