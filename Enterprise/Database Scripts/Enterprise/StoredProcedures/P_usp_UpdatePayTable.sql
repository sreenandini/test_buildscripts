USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdatePayTable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdatePayTable]
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
-- Description: Insert/fetch the Pay Table for the given Pay table description
-- Inputs:      See inputs 
--
-- Outputs:         
--                  
-- =======================================================================
-- 
-- Revision History
--
-- Sudarsan S		26/10/2009		Created
-- Yoganandh P		06/01/2011		Modified - Included HQ Paytable ID
-- Yoganandh P		11/01/2011		Modified - Considered MaxBet also, to uniquely identify a Paytable 
-- Yoganandh P		18/01/2011		Modified - Included Site Id 
------------------------------------------------------------------------------------------------------
CREATE PROCEDURE dbo.usp_UpdatePayTable
	@doc			XML
AS

BEGIN
	
	DECLARE @Pay_ID	INT
	DECLARE @Game_ID	INT
	DECLARE @idoc	INT
	DECLARE @Game_Name	VARCHAR(500)
	DECLARE @Payout		FLOAT 
	DECLARE @MaxBet		FLOAT 
	DECLARE @PT_Description	VARCHAR(500)
	DECLARE @HQ_Pay_ID	INT
	DECLARE @Site_Code	VARCHAR(50)
	DECLARE @Site_ID	INT

	EXEC sp_xml_preparedocument @idoc OUTPUT, @doc

	SELECT 
			@Site_Code = Site_Code,
			@HQ_Pay_ID = Paytable_ID,
			@Game_Name = Game_Name,
			@Payout = Payout,
			@PT_Description = PT_Description,
		    @MaxBet = MaxBet,
		    @Game_ID=HQGameID  
			 FROM OPENXML(@idoc, './Site', 2) WITH
				(
					Site_Code		VARCHAR(50)		'./SiteCode',
					Paytable_ID		INT				'./PAYTABLEDETAILS/Game/PayTable/Paytable_ID',
					Game_Name		VARCHAR(500)	'./PAYTABLEDETAILS/Game/Game_Name',
					Payout			FLOAT			'./PAYTABLEDETAILS/Game/PayTable/Payout',
					PT_Description	VARCHAR(500)	'./PAYTABLEDETAILS/Game/PayTable/PT_Description',
					MaxBet			FLOAT			'./PAYTABLEDETAILS/Game/PayTable/MaxBet',
					HQGameID		INT				'./PAYTABLEDETAILS/Game/HQGameID' 
				)

	EXEC sp_xml_removedocument @idoc
		
	SELECT @Pay_ID = Paytable_ID FROM dbo.PayTable WHERE PT_Description = @PT_Description 
														AND Payout = @Payout
														AND Game_ID = @Game_ID
														AND MaxBet = @MaxBet

	IF @Pay_ID IS NULL
	BEGIN
		INSERT INTO dbo.PayTable(Game_ID, Payout, PT_Description, MaxBet, TheoreticalPayout, HQ_Paytable_ID, Site_ID) 
		VALUES(@Game_ID, @Payout, @PT_Description, @MaxBet, @Payout, @HQ_Pay_ID, 0)
		
		INSERT INTO MeterAnalysis.dbo.PayTable(Game_ID, Payout, PT_Description, MaxBet, TheoreticalPayout, HQ_Paytable_ID, Site_ID) 
		VALUES(@Game_ID, @Payout, @PT_Description, @MaxBet, @Payout, @HQ_Pay_ID, 0)

		SELECT @Pay_ID	= SCOPE_IDENTITY()

	END

	RETURN @Pay_ID

END


GO

