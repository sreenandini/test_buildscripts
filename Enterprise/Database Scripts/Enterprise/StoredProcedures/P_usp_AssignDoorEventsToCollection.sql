USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_AssignDoorEventsToCollection]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_AssignDoorEventsToCollection]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_AssignDoorEventsToCollection]

  @Collection_No   INT,
  @Installation_No INT
  
AS 

  SET DATEFORMAT DMY

  -- constants
  --
  DECLARE @EVENT_DOOR            INT

  SELECT @EVENT_DOOR      = 3
         
  UPDATE Door_Event 
     SET Collection_ID = @Collection_No
   WHERE COALESCE( Collection_ID,0) = 0
     AND Installation_ID = @Installation_No
     
/**   
   WHERE Door_Event_No IN ( SELECT Door_Event.Door_Event_No
  
                              FROM Door_Event
  
                              JOIN Event 
                                ON Door_Event.Door_Event_No = Event.Event_Detail
                               
                             WHERE Event.Event_Type = @EVENT_DOOR  
                               AND Event.Installation_No = @Installation_No
                               AND Collection_No = 0
                           )
**/
              
-- return error if any
--
RETURN @@Error

GO

