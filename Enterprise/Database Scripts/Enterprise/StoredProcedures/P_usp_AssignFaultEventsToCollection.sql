USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_AssignFaultEventsToCollection]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_AssignFaultEventsToCollection]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_AssignFaultEventsToCollection]

  @Collection_No   INT,
  @Installation_No INT

AS 

  SET DATEFORMAT DMY

  -- constants
  --
  DECLARE @EVENT_FAULT           INT

  SELECT @EVENT_FAULT     = 4
         
  UPDATE Fault_Event 
     SET Collection_ID = @Collection_No
   WHERE COALESCE ( Collection_ID, 0 ) = 0 
     AND Installation_ID = @Installation_No
/**
   WHERE Fault_Event_No IN ( SELECT Fault_Event.Fault_Event_No
  
                               FROM Fault_Event
  
                               JOIN Event 
                                 ON Fault_Event.Fault_Event_No = Event.Event_Detail
                               
                              WHERE Event.Event_Type = @EVENT_FAULT
                                AND Event.Installation_No = @Installation_No
                                AND Collection_No = 0
                           )
**/
              
-- return error if any
--
RETURN @@Error

GO

