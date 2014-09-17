USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTotalFaultEvents]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetTotalFaultEvents]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------   
--  
-- Description: Retrieves the total fault events
--  
--  
-- ====================================================================  
-- Revision History  
--   
-- Gnanasekar Babu   27/09/10    Created   
---------------------------------------------------------------------------   
  
Create function GetTotalFaultEvents
(
	@collection_ID int
)
RETURNS INT
AS  
BEGIN  
	DECLARE @Return INT
	SELECT @Return = COUNT(*) FROM Fault_Event WHERE Fault_Event.Collection_ID= @collection_ID
	RETURN ISNULL(@Return,0)
END


GO

