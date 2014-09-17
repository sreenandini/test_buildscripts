USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateInstallationDenomChange]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateInstallationDenomChange]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------- 
--Installation
-- Description: Inserts a new machine class record
--
-- Inputs:      
-- Outputs:     
--
-- =======================================================================
-- 
-- Revision History
-- 
-- Sudarsan S	08-12-2009   Created   
---------------------------------------------------------------------------
CREATE PROCEDURE dbo.usp_UpdateInstallationDenomChange
	@doc	XML,
	@Installation_ID	INT OUTPUT
AS

BEGIN

	DECLARE @idoc	INT
	DECLARE @Bar_ID	INT
	DECLARE @Machine_ID	INT

	CREATE TABLE #TempInst
		(
			Site_Code	VARCHAR(20),
			Bar_Pos_No	INT,
			Bar_Pos_Name	VARCHAR(50),
			HQ_Installation_No	INT,
			Stock_no	VARCHAR(50),
			Start_Date	DATETIME,
			Anticipated_Percentage_Payout	REAL,
			Installation_Price_Of_Play	INT,
			Installation_Jackpot	INT,
			Installation_Token_Value	INT
		)

	SET @Installation_ID = 0

	EXEC SP_XML_PREPAREDOCUMENT @idoc OUTPUT, @doc

	INSERT INTO #TempInst
	SELECT * FROM OPENXML(@idoc, './DenomChange/Installation', 2) WITH #TempInst

	EXEC SP_XML_REMOVEDOCUMENT @idoc

	SELECT @Bar_ID = B.Bar_Position_ID FROM dbo.Bar_Position B
			INNER JOIN dbo.Site S ON B.Site_ID = S.Site_ID
			INNER JOIN #TempInst T ON T.Site_Code = S.Site_Code AND T.Bar_Pos_Name = B.Bar_Position_Name

	SELECT @Machine_ID = M.Machine_ID FROM dbo.Machine M
					INNER JOIN #TempInst T ON M.Machine_Stock_No = T.Stock_No

	IF NOT EXISTS(SELECT 1 FROM dbo.Installation WHERE Bar_Position_ID = @Bar_ID AND Installation_End_Date IS NULL)
	BEGIN

		INSERT INTO dbo.Installation(Bar_Position_ID, Machine_ID, Installation_Start_Date, Installation_Start_Time,
						Installation_Percentage_Payout, Installation_Jackpot_Value, Installation_Price_Per_Play, Installation_Token_Value)
		SELECT @Bar_ID, @Machine_ID, CONVERT(VARCHAR, Start_Date, 106), CONVERT(VARCHAR, Start_Date, 108), 
				Anticipated_Percentage_Payout, Installation_Jackpot, Installation_Price_Of_Play, Installation_Token_Value
			FROM #TempInst

		SET @Installation_ID = SCOPE_IDENTITY()

	END
	ELSE
	BEGIN
		SELECT @Installation_ID = Installation_ID FROM dbo.Installation WHERE Bar_Position_ID = @Bar_ID AND Installation_End_Date IS NULL
	END

END

GO

