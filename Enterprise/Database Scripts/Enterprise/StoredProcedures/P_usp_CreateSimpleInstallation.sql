/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 03/05/2014 11:54:50 PM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_CreateSimpleInstallation]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_CreateSimpleInstallation]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_CreateSimpleInstallation]
	@Bar_Position_ID INT,
	@Machine_ID INT,
	@Start_Date VARCHAR(12),
	@Start_time VARCHAR(8),
	@Percent_cash_Payout INT,
	@jackpot_value INT,
	@price_of_play INT,
	@Token_Value INT = 0
AS
	DECLARE @RETURNVALUE INT
	--set @Bar_Position_ID = 19907
	--set @Machine_ID = 3232
	--set @Installation_Start_Date = '18 sep 2007'
	--set @Installation_Start_time = '16:25:00'
	
	INSERT INTO Installation
	  (
	    [Bar_Position_ID],
	    [Machine_ID],
	    [Installation_Start_Date],
	    [Installation_Start_Time],
	    [Installation_End_Date],
	    [Installation_End_Time],
	    [Installation_Percentage_Payout],
	    [Installation_Jackpot_Value],
	    [Installation_Price_Per_Play],
	    [Installation_Token_Value]
	  )
	VALUES
	  (
	    @Bar_Position_ID,
	    @Machine_ID,
	    @Start_Date,
	    @Start_Time,
	    NULL,	-- [Installation_End_Date]
	    NULL,	-- [Installation_End_Time]
	    @Percent_cash_Payout,
	    @jackpot_value,
	    @price_of_play,
	    @Token_Value
	  )
	SET @RETURNVALUE = SCOPE_IDENTITY()
	
	/*****************************************************************************************************
	* (CR# 191254 : EBS Communication Service) - MODIFIED BY A.VINOD KUMAR (START)
	*****************************************************************************************************/

	DECLARE @_Modified TABLE (
		MachineId INT,
		OldFlag INT, NewFlag INT,
		OldGameID INT, NewGameID INT,
		OldCMPGameType varchar(50), NewCMPGameType varchar(50),
		OldStockNo varchar(50), NewStockNo varchar(50),
		FlagChanged AS (CASE WHEN OldFlag = NewFlag THEN 0 ELSE 1 END),
		GameIDChanged AS (CASE WHEN OldGameID = NewGameID THEN 0 ELSE 1 END),	
		CMPGameTypeChanged AS (CASE WHEN OldCMPGameType = NewCMPGameType THEN 0 ELSE 1 END),
		StockNoChanged AS (CASE WHEN OldStockNo = NewStockNo THEN 0 ELSE 1 END)
	)
	
	UPDATE MACHINE
	SET    Machine_Status_Flag = 1
	OUTPUT INSERTED.Machine_ID,
		DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
		DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
		DELETED.CMPGameType, INSERTED.CMPGameType, 
		DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
	INTO @_Modified
	WHERE  Machine_ID = @Machine_ID
	
	UPDATE BMC_AAMS_Details
	SET    BAD_Entity_Command = 'Enabled',
	       BAD_Updated_Date = GETDATE(),
	       BAD_Comments = 'Installation. Enable the machine.'
	WHERE  BAD_Reference_ID = @Machine_ID
	       AND BAD_AAMS_Entity_Type = 3
	
	UPDATE Bar_Position
	SET    Bar_Position_Machine_Enabled = 1,
	       Bar_Position_Note_Acceptor_Enabled = 1
	WHERE  Bar_Position_ID = @Bar_Position_ID
   
	IF EXISTS(
		SELECT 1
		FROM   @_Modified m
		WHERE  m.FlagChanged = 1 OR
				m.GameIDChanged = 1 OR
				m.CMPGameTypeChanged = 1 OR
				m.StockNoChanged = 1
	)
	BEGIN
		EXEC [dbo].[usp_EBS_UpdateMachineDetails] @Machine_ID 
	END

	/*****************************************************************************************************
	* (CR# 191254 : EBS Communication Service) - MODIFIED BY A.VINOD KUMAR (END)
	*****************************************************************************************************/
	
	RETURN @RETURNVALUE
GO

