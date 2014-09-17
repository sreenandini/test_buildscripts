USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertMGMDInstallation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertMGMDInstallation]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------------------- 
---
--- Description: Insert a record into the MGMD_Installation table
---
--- Inputs:      see inputs
---
--- Outputs:     (0)   - no error .. 
---              OTHER - SQL error 
--- 
--- =======================================================================
--- 
--- Revision History
--- 
--- Sudarsan  26/10/09	Created
--------------------------------------------------------------------------- 
CREATE PROCEDURE dbo.usp_InsertMGMDInstallation
	@doc	XML,
	@Install	INT OUTPUT
AS

BEGIN
SET NOCOUNT ON	
DECLARE @idoc	INT
DECLARE @Installation_ID	INT
DECLARE @Game_Name	VARCHAR(500)
DECLARE @PayTable	VARCHAR(500)
DECLARE @Denom		INT
DECLARE @DenomID	INT
DECLARE @PayTableID	INT
DECLARE @Game_ID	INT
DECLARE @MG_Date	DATETIME

DECLARE @Payout float
	EXEC sp_xml_preparedocument @idoc OUTPUT, @doc

	SELECT @Installation_ID = HQ_Installation_No,
			@Game_Name = Game_Name,
			@payTable = PayTable,
			@Denom = Denom,
   			@MG_Date = MG_Date,
   			@Game_ID=MG_HQ_Game_ID,
   			@Payout= Payout  
		 FROM OPENXML(@idoc, './MULTIGAMEINSTALL/MGMD_Installation', 2) WITH 
		(
			HQ_Installation_No	INT		'./HQ_Installation_No',
			Game_Name	VARCHAR(500)	'./MG_Game_Name',
			PayTable	VARCHAR(500)	'./PT_Description',
			Denom	INT	'./Denom_Value',
   			MG_Date DATETIME 'MGMD_DateTime',
   			MG_HQ_Game_ID INT './MG_HQ_Game_ID',
   			Payout float './Payout'   
		)
	
	SELECT @PayTableID = Paytable_ID FROM dbo.PayTable 
	WHERE PT_Description = @payTable AND Payout=@Payout AND Game_ID=@Game_ID 
	ORDER BY Paytable_ID DESC

	SELECT @Install = MGMD_ID FROM dbo.MGMD_Installation WHERE MGMD_Installation_ID = @Installation_ID
			AND MGMD_Game_ID = @Game_ID AND MGMD_Denom_Value = @Denom AND MGMD_Paytable_ID = @PayTableID

	IF @Install IS NULL
	BEGIN
		INSERT INTO dbo.MGMD_Installation
		SELECT @Installation_ID, @Game_ID, @PayTableID, @Denom, @MG_Date
		
		INSERT INTO MeterAnalysis.dbo.MGMD_Installation
		SELECT @Installation_ID, @Game_ID, @PayTableID, @Denom, @MG_Date

		SELECT @Install = SCOPE_IDENTITY()
	END

END


GO

