USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTotalDoorEvents]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetTotalDoorEvents]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------   
--  
-- Description: Retrieves the total door events
--  
--  
-- ====================================================================  
-- Revision History  
--   
-- Gnanasekar Babu   27/09/10    Created   
---------------------------------------------------------------------------   
  
Create function GetTotalDoorEvents
(
	@collection_ID int
)
RETURNS INT
AS  
BEGIN  
	DECLARE @Return INT
	SELECT @Return = COUNT(*) FROM Door_Event 
	--INNER JOIN Datapak_Fault ON Datapak_Fault.Datapak_Fault_Supplementary_Code = Door_Event.Door_Event_Type 
	WHERE --Datapak_Fault.Datapak_Fault_Code = 20 AND 
	Collection_ID= @collection_ID
  
	RETURN ISNULL(@Return,0)
END


GO

