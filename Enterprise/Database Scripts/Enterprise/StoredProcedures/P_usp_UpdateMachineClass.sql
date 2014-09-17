USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateMachineClass]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateMachineClass]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.usp_UpdateMachineClass
	@Mac_Class_Ids VARCHAR(MAX),
	@Game_Tile_Id INT,
	@Remove INT
AS
	/*****************************************************************************************************
DESCRIPTION : PROC Description  
CREATED DATE: 30 - 11 - 2012
MODULE      : PROC used in Modules      
CHANGE HISTORY :
EXAMPLE		: usp_UpdateMachineClass 18,13,1
			  usp_UpdateMachineClass 18,13,0
			  usp_UpdateMachineClass 11,10,1
------------------------------------------------------------------------------------------------------
AUTHOR                              DESCRIPTON                                                        MODIFIED DATE
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/
BEGIN
	DECLARE @Manufacturer_Id       INT
	DECLARE @Game_Title            VARCHAR(100)
	DECLARE @New_Machine_Class_No  INT
	DECLARE @Old_Machine_Class_No  INT
	DECLARE @Machine_Class_Nos     VARCHAR(8000)
	DECLARE @Upd_Game_Title        VARCHAR(100)
	
	SELECT @Game_Title = LTRIM(RTRIM(Game_Title))
	FROM   Game_Title WITH(NOLOCK)
	WHERE  Game_Title_ID = @Game_Tile_Id
	
	DECLARE @tMachineDetail TABLE (MC_ID INT, MT_ID INT)
 
	INSERT @tMachineDetail
	SELECT SUBSTRING([str], 0, CHARINDEX('-', [str], 0)) MC_ID,
	       SUBSTRING([str], CHARINDEX('-', [str], 0) + 1, LEN([str])) MT_ID
	FROM   dbo.iter_charlist_to_tbl(@Mac_Class_Ids, ',')
                     
	DECLARE Manufacture_Id CURSOR LOCAL 
	FOR
	    SELECT DISTINCT Manufacturer_ID
	    FROM   Machine_Class MC WITH(NOLOCK)
	    INNER JOIN @tMachineDetail TEMP
                 ON  MC.Machine_Class_ID = TEMP.MC_ID
                 AND MC.Machine_Type_ID = TEMP.MT_ID	    
	
	OPEN Manufacture_Id
	
	FETCH NEXT FROM Manufacture_Id 
	INTO @Manufacturer_Id
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
	    SET @New_Machine_Class_No = 0
	    SET @Old_Machine_Class_No = 0
	    
	    SET @Upd_Game_Title = @Game_Title
	    
     SELECT @New_Machine_Class_No = ISNULL(MC.Machine_Class_ID, 0)    
     FROM   Machine_Class MC WITH(NOLOCK)  
          INNER JOIN @tMachineDetail TEMP    
                 ON MC.Machine_Type_ID = TEMP.MT_ID    
     WHERE  LTRIM(RTRIM(MC.Machine_Name)) = @Game_Title    
            AND MC.Manufacturer_ID = @Manufacturer_Id  
 
       
     SELECT @Old_Machine_Class_No = ISNULL(MC.Machine_Class_ID, 0)    
     FROM   Machine_Class MC WITH(NOLOCK)    
     INNER JOIN @tMachineDetail TEMP    
                 ON  MC.Machine_Type_ID = TEMP.MT_ID
     WHERE  REPLACE(LTRIM(RTRIM(MC.Machine_Name)), ' ', '') =  'UnassignedGameTitle'    
            AND MC.Manufacturer_ID = @Manufacturer_Id   
	    
	    IF (@Remove = 1)
	    BEGIN
	        SET @New_Machine_Class_No = @New_Machine_Class_No + @Old_Machine_Class_No
	        SET @Old_Machine_Class_No = @New_Machine_Class_No - @Old_Machine_Class_No
	        SET @New_Machine_Class_No = @New_Machine_Class_No - @Old_Machine_Class_No
	        SET @Upd_Game_Title= 'Unassigned GameTitle'
	    END
	    
	    IF (@New_Machine_Class_No > 0)
	    BEGIN
	        UPDATE MACHINE
	        SET    Machine_Class_ID = @New_Machine_Class_No
	        WHERE  Machine_Class_ID = @Old_Machine_Class_No
	        
	        UPDATE MeterAnalysis.dbo.MACHINE
	        SET    Machine_Class_ID = @New_Machine_Class_No
	        WHERE  Machine_Class_ID = @Old_Machine_Class_No
	        
	    END
	    ELSE
	    BEGIN
	        INSERT INTO Machine_Class
	        (
	        Machine_Type_ID,
Manufacturer_ID,
Depreciation_Policy_ID,
Depreciation_Policy_Use_Default,
Machine_Class_Category,
Machine_Class_Model_Code,
Machine_BACTA_Code,
Machine_Name,
Machine_Hopper_Type,
Machine_Max_No_Of_Discs,
Machine_Disc_Type,
Label_ID,
Machine_Picture_Label,
Machine_CD_Numbering,
Machine_Track_Numbering,
Machine_Class_Counter_Cash_In_Units,
Machine_Class_Counter_Cash_Out_Units,
Machine_Class_Counter_Tokens_In_Units,
Machine_Class_Counter_Tokens_Out_Units,
Machine_Class_Counter_Refill_Units,
Machine_Class_Counter_Jackpot_Units,
Machine_Class_Counter_Prize_Units,
Machine_Class_Counter_Tournament_Play_Units,
Machine_Class_Counter_JukeBox_Play_Units,
Machine_Class_Prize_Machine,
Machine_Class_Max_Float_Level,
Machine_Class_Default_Coin_Mech,
Machine_Class_Default_Note_Acceptor,
Machine_Class_Test_Machine,
Machine_Class_Delisted,
Machine_Class_Features,
Machine_Class_Tolerance_Cash_In,
Machine_Class_Tolerance_Cash_Out,
Machine_Class_Tolerance_Tokens_In,
Machine_Class_Tolerance_Tokens_Out,
Machine_Class_Tolerance_Refills,
Machine_Class_Tolerance_Tournament,
Machine_Class_Tolerance_Jukebox,
Machine_Class_Tolerance_Prize,
Machine_Class_Float_200,
Machine_Class_Float_100,
Machine_Class_Float_50,
Machine_Class_Float_20,
Machine_Class_Float_10,
Machine_Class_Float_5,
Machine_Class_Float_2,
Machine_Class_Config_Div_Level_1,
Machine_Class_Config_Div_Level_2,
Machine_Class_Config_Div_Level_3,
Machine_Class_Config_Div_Level_4,
Machine_Class_Config_Single_Comm,
Machine_Class_Config_Machine_Mode,
Machine_Class_Config_Machine_Version,
Machine_Class_Config_Attract_Mode_Text,
Machine_Class_Config_Bank_Options,
Machine_Class_Config_Max_Credit_Limit,
Machine_Class_Config_Max_Bank_Value,
Machine_Class_Price_Of_Play,
Machine_Class_Jackpot,
Machine_Class_Percent_Cash_Payout,
Machine_Class_Percent_Token_Payout,
Machine_Class_Speaker_Volume,
Machine_Class_SP_Features,
Machine_Class_Release_Date,
Machine_Class_Category_ID,
Machine_Class_Is_Ticket,
Machine_Class_RecreateCancelledCredits,
Machine_Class_JackpotAddedToCancelledCredits,
Machine_Class_AddTrueCoinInToDrop,
Machine_Class_UseCancelledCreditsAsTicketsPrinted,
Machine_Class_RecreateTicketsInsertedfromDrop,
Meter_Rollover,
Validation_Length

	        )
	        SELECT Machine_Type_ID,
	               Manufacturer_ID,
	               Depreciation_Policy_ID,
	               Depreciation_Policy_Use_Default,
	               Machine_Class_Category,
	               @Upd_Game_Title,
	               Machine_BACTA_Code,
	               @Upd_Game_Title,
	               Machine_Hopper_Type,
	               Machine_Max_No_Of_Discs,
	               Machine_Disc_Type,
	               Label_ID,
	               Machine_Picture_Label,
	               Machine_CD_Numbering,
	               Machine_Track_Numbering,
	               Machine_Class_Counter_Cash_In_Units,
	               Machine_Class_Counter_Cash_Out_Units,
	               Machine_Class_Counter_Tokens_In_Units,
	               Machine_Class_Counter_Tokens_Out_Units,
	               Machine_Class_Counter_Refill_Units,
	               Machine_Class_Counter_Jackpot_Units,
	               Machine_Class_Counter_Prize_Units,
	               Machine_Class_Counter_Tournament_Play_Units,
	               Machine_Class_Counter_JukeBox_Play_Units,
	               Machine_Class_Prize_Machine,
	               Machine_Class_Max_Float_Level,
	               Machine_Class_Default_Coin_Mech,
	               Machine_Class_Default_Note_Acceptor,
	               Machine_Class_Test_Machine,
	               Machine_Class_Delisted,
	               Machine_Class_Features,
	               Machine_Class_Tolerance_Cash_In,
	               Machine_Class_Tolerance_Cash_Out,
	               Machine_Class_Tolerance_Tokens_In,
	               Machine_Class_Tolerance_Tokens_Out,
	               Machine_Class_Tolerance_Refills,
	               Machine_Class_Tolerance_Tournament,
	               Machine_Class_Tolerance_Jukebox,
	               Machine_Class_Tolerance_Prize,
	               Machine_Class_Float_200,
	               Machine_Class_Float_100,
	               Machine_Class_Float_50,
	               Machine_Class_Float_20,
	               Machine_Class_Float_10,
	               Machine_Class_Float_5,
	               Machine_Class_Float_2,
	               Machine_Class_Config_Div_Level_1,
	               Machine_Class_Config_Div_Level_2,
	               Machine_Class_Config_Div_Level_3,
	               Machine_Class_Config_Div_Level_4,
	               Machine_Class_Config_Single_Comm,
	               Machine_Class_Config_Machine_Mode,
	               Machine_Class_Config_Machine_Version,
	               Machine_Class_Config_Attract_Mode_Text,
	               Machine_Class_Config_Bank_Options,
	               Machine_Class_Config_Max_Credit_Limit,
	               Machine_Class_Config_Max_Bank_Value,
	               Machine_Class_Price_Of_Play,
	               Machine_Class_Jackpot,
	               Machine_Class_Percent_Cash_Payout,
	               Machine_Class_Percent_Token_Payout,
	               Machine_Class_Speaker_Volume,
	               Machine_Class_SP_Features,
	               Machine_Class_Release_Date,
	               Machine_Class_Category_ID,
	               Machine_Class_Is_Ticket,
	               Machine_Class_RecreateCancelledCredits,
	               Machine_Class_JackpotAddedToCancelledCredits,
	               Machine_Class_AddTrueCoinInToDrop,
	               Machine_Class_UseCancelledCreditsAsTicketsPrinted,
	               Machine_Class_RecreateTicketsInsertedfromDrop,
	               Meter_Rollover,
	             --  Machine_Class_Occupancy_Games_Per_Hour,
	               Validation_Length
	        FROM   Machine_Class mc
	        WHERE  mc.Machine_Class_ID = @Old_Machine_Class_No
	       
	        INSERT INTO MeterAnalysis.dbo.Machine_Class
	        (
	        Machine_Type_ID,
Manufacturer_ID,
Depreciation_Policy_ID,
Depreciation_Policy_Use_Default,
Machine_Class_Category,
Machine_Class_Model_Code,
Machine_BACTA_Code,
Machine_Name,
Machine_Hopper_Type,
Machine_Max_No_Of_Discs,
Machine_Disc_Type,
Label_ID,
Machine_Picture_Label,
Machine_CD_Numbering,
Machine_Track_Numbering,
Machine_Class_Counter_Cash_In_Units,
Machine_Class_Counter_Cash_Out_Units,
Machine_Class_Counter_Tokens_In_Units,
Machine_Class_Counter_Tokens_Out_Units,
Machine_Class_Counter_Refill_Units,
Machine_Class_Counter_Jackpot_Units,
Machine_Class_Counter_Prize_Units,
Machine_Class_Counter_Tournament_Play_Units,
Machine_Class_Counter_JukeBox_Play_Units,
Machine_Class_Prize_Machine,
Machine_Class_Max_Float_Level,
Machine_Class_Default_Coin_Mech,
Machine_Class_Default_Note_Acceptor,
Machine_Class_Test_Machine,
Machine_Class_Delisted,
Machine_Class_Features,
Machine_Class_Tolerance_Cash_In,
Machine_Class_Tolerance_Cash_Out,
Machine_Class_Tolerance_Tokens_In,
Machine_Class_Tolerance_Tokens_Out,
Machine_Class_Tolerance_Refills,
Machine_Class_Tolerance_Tournament,
Machine_Class_Tolerance_Jukebox,
Machine_Class_Tolerance_Prize,
Machine_Class_Float_200,
Machine_Class_Float_100,
Machine_Class_Float_50,
Machine_Class_Float_20,
Machine_Class_Float_10,
Machine_Class_Float_5,
Machine_Class_Float_2,
Machine_Class_Config_Div_Level_1,
Machine_Class_Config_Div_Level_2,
Machine_Class_Config_Div_Level_3,
Machine_Class_Config_Div_Level_4,
Machine_Class_Config_Single_Comm,
Machine_Class_Config_Machine_Mode,
Machine_Class_Config_Machine_Version,
Machine_Class_Config_Attract_Mode_Text,
Machine_Class_Config_Bank_Options,
Machine_Class_Config_Max_Credit_Limit,
Machine_Class_Config_Max_Bank_Value,
Machine_Class_Price_Of_Play,
Machine_Class_Jackpot,
Machine_Class_Percent_Cash_Payout,
Machine_Class_Percent_Token_Payout,
Machine_Class_Speaker_Volume,
Machine_Class_SP_Features,
Machine_Class_Release_Date,
Machine_Class_Category_ID,
Machine_Class_Is_Ticket,
Machine_Class_RecreateCancelledCredits,
Machine_Class_JackpotAddedToCancelledCredits,
Machine_Class_AddTrueCoinInToDrop,
Machine_Class_UseCancelledCreditsAsTicketsPrinted,
Machine_Class_RecreateTicketsInsertedfromDrop,
Meter_Rollover,
Validation_Length

	        )
	        SELECT Machine_Type_ID,
	               Manufacturer_ID,
	               Depreciation_Policy_ID,
	               Depreciation_Policy_Use_Default,
	               Machine_Class_Category,
	               @Upd_Game_Title,
	               Machine_BACTA_Code,
	               @Upd_Game_Title,
	               Machine_Hopper_Type,
	               Machine_Max_No_Of_Discs,
	               Machine_Disc_Type,
	               Label_ID,
	               Machine_Picture_Label,
	               Machine_CD_Numbering,
	               Machine_Track_Numbering,
	               Machine_Class_Counter_Cash_In_Units,
	               Machine_Class_Counter_Cash_Out_Units,
	               Machine_Class_Counter_Tokens_In_Units,
	               Machine_Class_Counter_Tokens_Out_Units,
	               Machine_Class_Counter_Refill_Units,
	               Machine_Class_Counter_Jackpot_Units,
	               Machine_Class_Counter_Prize_Units,
	               Machine_Class_Counter_Tournament_Play_Units,
	               Machine_Class_Counter_JukeBox_Play_Units,
	               Machine_Class_Prize_Machine,
	               Machine_Class_Max_Float_Level,
	               Machine_Class_Default_Coin_Mech,
	               Machine_Class_Default_Note_Acceptor,
	               Machine_Class_Test_Machine,
	               Machine_Class_Delisted,
	               Machine_Class_Features,
	               Machine_Class_Tolerance_Cash_In,
	               Machine_Class_Tolerance_Cash_Out,
	               Machine_Class_Tolerance_Tokens_In,
	               Machine_Class_Tolerance_Tokens_Out,
	               Machine_Class_Tolerance_Refills,
	               Machine_Class_Tolerance_Tournament,
	               Machine_Class_Tolerance_Jukebox,
	               Machine_Class_Tolerance_Prize,
	               Machine_Class_Float_200,
	               Machine_Class_Float_100,
	               Machine_Class_Float_50,
	               Machine_Class_Float_20,
	               Machine_Class_Float_10,
	               Machine_Class_Float_5,
	               Machine_Class_Float_2,
	               Machine_Class_Config_Div_Level_1,
	               Machine_Class_Config_Div_Level_2,
	               Machine_Class_Config_Div_Level_3,
	               Machine_Class_Config_Div_Level_4,
	               Machine_Class_Config_Single_Comm,
	               Machine_Class_Config_Machine_Mode,
	               Machine_Class_Config_Machine_Version,
	               Machine_Class_Config_Attract_Mode_Text,
	               Machine_Class_Config_Bank_Options,
	               Machine_Class_Config_Max_Credit_Limit,
	               Machine_Class_Config_Max_Bank_Value,
	               Machine_Class_Price_Of_Play,
	               Machine_Class_Jackpot,
	               Machine_Class_Percent_Cash_Payout,
	               Machine_Class_Percent_Token_Payout,
	               Machine_Class_Speaker_Volume,
	               Machine_Class_SP_Features,
	               Machine_Class_Release_Date,
	               Machine_Class_Category_ID,
	               Machine_Class_Is_Ticket,
	               Machine_Class_RecreateCancelledCredits,
	               Machine_Class_JackpotAddedToCancelledCredits,
	               Machine_Class_AddTrueCoinInToDrop,
	               Machine_Class_UseCancelledCreditsAsTicketsPrinted,
	               Machine_Class_RecreateTicketsInsertedfromDrop,
	               Meter_Rollover,
	             --  Machine_Class_Occupancy_Games_Per_Hour,
	               Validation_Length
	        FROM   Machine_Class mc
	        WHERE  mc.Machine_Class_ID = @Old_Machine_Class_No
	         
	        
	        
	        SELECT @New_Machine_Class_No = SCOPE_IDENTITY()
	        
	        EXEC usp_InsertExportHistory @New_Machine_Class_No,
	             'MODEL',
	             'ALL'
	        
	        UPDATE MACHINE
	        SET    Machine_Class_ID = @New_Machine_Class_No
	        WHERE  Machine_Class_ID = @Old_Machine_Class_No
	        
	        UPDATE MeterAnalysis.dbo.MACHINE
	        SET    Machine_Class_ID = @New_Machine_Class_No
	        WHERE  Machine_Class_ID = @Old_Machine_Class_No
	    END
	    
	    INSERT INTO Export_History
	      (
	        EH_Date,
	        EH_Reference1,
	        EH_Type,
	        EH_Site_Code
	      )
	    SELECT GETDATE(),
	           Machine_ID,
	           'MACHINEUPDATE',
	           Site_Code
	    FROM   MACHINE
	           CROSS APPLY SITE
	    WHERE  MACHINE.Machine_Class_ID = @New_Machine_Class_No
	    
	    FETCH NEXT FROM Manufacture_Id 
	    INTO @Manufacturer_Id
	END
	CLOSE Manufacture_Id
	DEALLOCATE Manufacture_Id
END

GO

