USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_AssignPowerEventsToCollection]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_AssignPowerEventsToCollection]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_AssignPowerEventsToCollection]

  @Collection_No   INT,
  @Installation_No INT
  
AS 

  SET DATEFORMAT DMY

  -- constants
  --
  DECLARE @EVENT_POWER           INT

  SELECT @EVENT_POWER     = 5
         
  UPDATE Power_Event 
     SET Collection_Id = @Collection_No
   WHERE COALESCE(COllection_Id,0) = 0
     AND Installation_Id = @Installation_No

/**
   WHERE Power_Event_No IN ( SELECT Power_Event.Power_Event_No
  
                               FROM Power_Event
  
                               JOIN Event 
                                 ON Power_Event.Power_Event_No = Event.Event_Detail
                               
                              WHERE Event.Event_Type = @EVENT_POWER  
                                AND Event.Installation_No = @Installation_No
                                AND Collection_No = 0
                           )
**/
              
-- return error if any
--
RETURN @@Error

GO

