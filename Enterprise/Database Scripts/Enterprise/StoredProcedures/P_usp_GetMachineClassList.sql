USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetMachineClassList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetMachineClassList]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-------------------------------------------------------------------------- 
--
-- Description: SP for Getting Machine Class List
--
-- Inputs:      Type ID 
--
--
-- =======================================================================
-- 
-- Revision History
-- 
-- Madhu A    13/05/08     Created
--------------------------------------------------------------------------- 

CREATE PROCEDURE [dbo].[usp_GetMachineClassList]
(
	@TypeID int 
)
AS
--------------------------------------------------
--- If Type ID is passed get related records 
--- else get all recrods
--------------------------------------------------
IF  @TypeID = 0
BEGIN

SELECT	Machine_Class_ID, 
		Machine_Name 
FROM Machine_Class 


END

ELSE
BEGIN

SELECT	Machine_Class_ID, 
		Machine_Name 
FROM Machine_Class 
WHERE Machine_Type_ID = @TypeID 
order by Machine_Name

END


GO

