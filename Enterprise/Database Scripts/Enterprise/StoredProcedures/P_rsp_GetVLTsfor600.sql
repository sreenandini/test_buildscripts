USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetVLTsfor600]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetVLTsfor600]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-------------------------------------------------------------------------- 
--
-- Description: get the list of installations Export Location specific data for AAMS
-- Inputs:      See inputs 
--
-- Outputs:         
--                  
-- =======================================================================
-- 
-- Revision History
--
-- Sudarsan S		03/12/2009		Created
------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[rsp_GetVLTsfor600]
AS
BEGIN

	DECLARE @Start	INT
	DECLARE @Count	INT	
	DECLARE @Machine_ID	INT

	SET @Start = 1

	CREATE TABLE #TempInst
		(
			Sno	INT IDENTITY(1,1),
			Machine_ID	INT
		)

	INSERT INTO #TempInst
	SELECT Machine_ID FROM dbo.Machine WHERE ISNULL(Machine_End_Date, '') = '' OR Machine_Status_Flag = 18

	SELECT @Count = COUNT(*) FROM #TempInst

	WHILE @Start <= @Count
	BEGIN
		SELECT @Machine_ID = Machine_ID FROM #TempInst WHERE Sno = @Start
		EXEC dbo.rsp_ExportVLT600 @Machine_ID, 'YTD'
		EXEC dbo.rsp_ExportVLT600 @Machine_ID, 'MTD'
		EXEC dbo.rsp_ExportVLT600 @Machine_ID, 'PTD'

		SET @Start = @Start + 1

	END

END


GO

