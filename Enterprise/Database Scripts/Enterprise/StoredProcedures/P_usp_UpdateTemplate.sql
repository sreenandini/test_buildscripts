/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 3/10/2014 10:33:00 AM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_UpdateTemplate]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_UpdateTemplate]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.usp_UpdateTemplate
	@bMode BIT ,
	@StockNumber VARCHAR(50),
	@TemplateName VARCHAR(50)
AS
	/*****************************************************************************************************
DESCRIPTION : Modify or remove existing  template  
CREATED DATE: 
MODULE            : Template Edit/Delete Module 
Value for bMode
1 - Edit
0 - Delete      
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR                              DESCRIPTON                                                        MODIFIED DATE
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/

BEGIN
	SET NOCOUNT ON 
	DECLARE @Machine_Class_ID INT
	SELECT @Machine_Class_ID = [TemplateMachine_Class_ID]
	FROM   AssetCreationTemplate
	WHERE  TemplateName = @TemplateName
	
	IF @bMode = 1
	BEGIN
	    UPDATE AssetCreationTemplate
	    SET    [Operator_ID] = M.[Operator_ID],
	           [Terms_Profile_ID] = M.[Terms_Profile_ID],
	           [Depreciation_Policy_ID] = M.[Depreciation_Policy_ID],
	           [Depreciation_Policy_Use_Default] = M.[Depreciation_Policy_Use_Default],
	           [Machine_Number_Of_Discs] = M.[Machine_Number_Of_Discs],
	           [Machine_Stock_No] = M.[Machine_Stock_No],
	           [Machine_Counter_Cash_In_Units] = M.[Machine_Counter_Cash_In_Units],
	           [Machine_Counter_Cash_Out_Units] = M.[Machine_Counter_Cash_Out_Units],
	           [Machine_Counter_Tokens_In_Units] = M.[Machine_Counter_Tokens_In_Units],
	           [Machine_Counter_Tokens_Out_Units] = M.[Machine_Counter_Tokens_Out_Units],
	           [Machine_Counter_Refill_Units] = M.[Machine_Counter_Refill_Units],
	           [Machine_Counter_Jackpot_Units] = M.[Machine_Counter_Jackpot_Units],
	           [Machine_Counter_Prize_Units] = M.[Machine_Counter_Prize_Units],
	           [Machine_Counter_Tournament_Play_Units] = M.[Machine_Counter_Tournament_Play_Units],
	           [Machine_Counter_JukeBox_Play_Units] = M.[Machine_Counter_JukeBox_Play_Units],
	           [Machine_Test] = M.[Machine_Test],
	           [Machine_Status_Flag] = M.[Machine_Status_Flag],
	           [Machine_Status] = M.[Machine_Status],
	           [Machine_Start_Date] = M.[Machine_Start_Date],
	           [Machine_End_Date] = M.[Machine_End_Date],
	           [Machine_Resale_Value] = M.[Machine_Resale_Value],
	           [Machine_Sales_Invoice_Number] = M.[Machine_Sales_Invoice_Number],
	           [Machine_Sold_To] = M.[Machine_Sold_To],
	           [Machine_Type_Of_Sale] = M.[Machine_Type_Of_Sale],
	           [Machine_PROM_Version] = M.[Machine_PROM_Version],
	           [Machine_Original_Purchase_Price] = M.[Machine_Original_Purchase_Price],
	           [Machine_Sale_Price] = M.[Machine_Sale_Price],
	           [Machine_Purchase_Invoice_Number] = M.[Machine_Purchase_Invoice_Number],
	           [Depot_ID] = M.[Depot_ID],
	           [Machine_AMEDIS_Variant_Code] = M.[Machine_AMEDIS_Variant_Code],
	           [Machine_Previous_Machine_ID] = M.[Machine_Previous_Machine_ID],
	           [Machine_Manufacturers_Serial_No] = M.[Machine_Manufacturers_Serial_No],
	           [Machine_Purchased_From] = M.[Machine_Purchased_From],
	           [Machine_Depreciation_Start_Date] = M.[Machine_Depreciation_Start_Date],
	           [Machine_Last_PAT_Date] = M.[Machine_Last_PAT_Date],
	           [Machine_PAT_Required] = M.[Machine_PAT_Required],
	           [Machine_Alternative_Serial_Numbers] = M.[Machine_Alternative_Serial_Numbers],
	           [Staff_ID] = M.[Staff_ID],
	           [Machine_Due_In_Stock] = M.[Machine_Due_In_Stock],
	           [Machine_Due_In_Stock_Date] = M.[Machine_Due_In_Stock_Date],
	           [Machine_Memo] = M.[Machine_Memo],
	           [Machine_Extra_Details] = M.[Machine_Extra_Details],
	           [Staff_ID_Entered] = M.[Staff_ID_Entered],
	           [Staff_ID_Deleted] = M.[Staff_ID_Deleted],
	           [Machine_Date_Entered] = M.[Machine_Date_Entered],
	           [Machine_Date_Deleted] = M.[Machine_Date_Deleted],
	           [Machine_Float_Maximum_Capacity] = M.[Machine_Float_Maximum_Capacity],
	           [Machine_Float_200p_Capacity] = M.[Machine_Float_200p_Capacity],
	           [Machine_Float_100p_Capacity] = M.[Machine_Float_100p_Capacity],
	           [Machine_Float_50p_Capacity] = M.[Machine_Float_50p_Capacity],
	           [Machine_Float_20p_Capacity] = M.[Machine_Float_20p_Capacity],
	           [Machine_Float_10p_Capacity] = M.[Machine_Float_10p_Capacity],
	           [Machine_Float_5p_Capacity] = M.[Machine_Float_5p_Capacity],
	           [Machine_Float_2p_Capacity] = M.[Machine_Float_2p_Capacity],
	           [Machine_Site_Planned_Movement_ID] = M.[Machine_Site_Planned_Movement_ID],
	           [Machine_Depot_Planned_Movement_ID] = M.[Machine_Depot_Planned_Movement_ID],
	           [Machine_Category_ID] = M.[Machine_Category_ID],
	           [Machine_MAC_Address] = M.[Machine_MAC_Address],
	           [IsMultiGame] = M.[IsMultiGame],
	           [Machine_Connection_Type] = M.[Machine_Connection_Type],
	           [Machine_New_Install] = M.[Machine_New_Install],
	           [Machine_MAC_Address_Prev] = M.[Machine_MAC_Address_Prev],
	           [Machine_Termination_Reason] = M.[Machine_Termination_Reason],
	           [Machine_Termination_Comments] = M.[Machine_Termination_Comments],
	           [Machine_Termination_Username] = M.[Machine_Termination_Username],
	           [Machine_ModelTypeID] = M.[Machine_ModelTypeID],
	           [Machine_Status_Flag_Prev] = M.[Machine_Status_Flag_Prev],
	           [Machine_CIV] = M.[Machine_CIV],
	           [Machine_CIV_Prev] = M.[Machine_CIV_Prev],
	           [Machine_CIV_Change_Reason] = M.[Machine_CIV_Change_Reason],
	           [IsAFTEnabled] = M.[IsAFTEnabled],
	           [Machine_Transit_Site_Code] = M.[Machine_Transit_Site_Code],
	           [IsTITOEnabled] = M.[IsTITOEnabled],
	           [IsNonCashVoucherEnabled] = M.[IsNonCashVoucherEnabled],
	           [ActAssetNo] = M.[ActAssetNo],
	           [GMUNo] = M.[GMUNo],
	           [ActSerialNo] = M.[ActSerialNo],
	           [EnrolmentFlag] = M.[EnrolmentFlag],
	           [CMPGameType] = M.[CMPGameType],
	           [Machine_Uses_Meters] = M.[Machine_Uses_Meters],
	           [Stacker_ID] = M.[Stacker_ID],
	           [GetGameDetails] = M.[GetGameDetails],
	           [Base_Denom] = M.[Base_Denom],
	           [Percentage_Payout] = M.[Percentage_Payout],
	           [IsDefaultAssetDetail] = M.[IsDefaultAssetDetail],
	           [IsGameCappingEnabled] = M.IsGameCappingEnabled,
	           [Machine_Occupancy_Hour] = M.[Machine_Occupancy_Hour]
	    FROM   [Machine] M
	    WHERE  AssetCreationTemplate.TemplateName = @TemplateName
	           AND M.Machine_Stock_No = @StockNumber
	    
	    
	    UPDATE TemplateMachine_Class
	    SET    [Machine_Type_ID] = MC.[Machine_Type_ID],
	           [Manufacturer_ID] = MC.[Manufacturer_ID],
	           [Depreciation_Policy_ID] = MC.[Depreciation_Policy_ID],
	           [Depreciation_Policy_Use_Default] = MC.[Depreciation_Policy_Use_Default],
	           [Machine_Class_Category] = MC.[Machine_Class_Category],
	           [Machine_Class_Model_Code] = MC.[Machine_Class_Model_Code],
	           [Machine_BACTA_Code] = MC.[Machine_BACTA_Code],
	           [Machine_Name] = MC.[Machine_Name],
	           [Machine_Hopper_Type] = MC.[Machine_Hopper_Type],
	           [Machine_Max_No_Of_Discs] = MC.[Machine_Max_No_Of_Discs],
	           [Machine_Disc_Type] = MC.[Machine_Disc_Type],
	           [Label_ID] = MC.[Label_ID],
	           [Machine_Picture_Label] = MC.[Machine_Picture_Label],
	           [Machine_CD_Numbering] = MC.[Machine_CD_Numbering],
	           [Machine_Track_Numbering] = MC.[Machine_Track_Numbering],
	           [Machine_Class_Counter_Cash_In_Units] = MC.[Machine_Class_Counter_Cash_In_Units],
	           [Machine_Class_Counter_Cash_Out_Units] = MC.[Machine_Class_Counter_Cash_Out_Units],
	           [Machine_Class_Counter_Tokens_In_Units] = MC.[Machine_Class_Counter_Tokens_In_Units],
	           [Machine_Class_Counter_Tokens_Out_Units] = MC.[Machine_Class_Counter_Tokens_Out_Units],
	           [Machine_Class_Counter_Refill_Units] = MC.[Machine_Class_Counter_Refill_Units],
	           [Machine_Class_Counter_Jackpot_Units] = MC.[Machine_Class_Counter_Jackpot_Units],
	           [Machine_Class_Counter_Prize_Units] = MC.[Machine_Class_Counter_Prize_Units],
	           [Machine_Class_Counter_Tournament_Play_Units] = MC.[Machine_Class_Counter_Tournament_Play_Units],
	           [Machine_Class_Counter_JukeBox_Play_Units] = MC.[Machine_Class_Counter_JukeBox_Play_Units],
	           [Machine_Class_Prize_Machine] = MC.[Machine_Class_Prize_Machine],
	           [Machine_Class_Max_Float_Level] = MC.[Machine_Class_Max_Float_Level],
	           [Machine_Class_Default_Coin_Mech] = MC.[Machine_Class_Default_Coin_Mech],
	           [Machine_Class_Default_Note_Acceptor] = MC.[Machine_Class_Default_Note_Acceptor],
	           [Machine_Class_Test_Machine] = MC.[Machine_Class_Test_Machine],
	           [Machine_Class_Delisted] = MC.[Machine_Class_Delisted],
	           [Machine_Class_Features] = MC.[Machine_Class_Features],
	           [Machine_Class_Tolerance_Cash_In] = MC.[Machine_Class_Tolerance_Cash_In],
	           [Machine_Class_Tolerance_Cash_Out] = MC.[Machine_Class_Tolerance_Cash_Out],
	           [Machine_Class_Tolerance_Tokens_In] = MC.[Machine_Class_Tolerance_Tokens_In],
	           [Machine_Class_Tolerance_Tokens_Out] = MC.[Machine_Class_Tolerance_Tokens_Out],
	           [Machine_Class_Tolerance_Refills] = MC.[Machine_Class_Tolerance_Refills],
	           [Machine_Class_Tolerance_Tournament] = MC.[Machine_Class_Tolerance_Tournament],
	           [Machine_Class_Tolerance_Jukebox] = MC.[Machine_Class_Tolerance_Jukebox],
	           [Machine_Class_Tolerance_Prize] = MC.[Machine_Class_Tolerance_Prize],
	           [Machine_Class_Float_200] = MC.[Machine_Class_Float_200],
	           [Machine_Class_Float_100] = MC.[Machine_Class_Float_100],
	           [Machine_Class_Float_50] = MC.[Machine_Class_Float_50],
	           [Machine_Class_Float_20] = MC.[Machine_Class_Float_20],
	           [Machine_Class_Float_10] = MC.[Machine_Class_Float_10],
	           [Machine_Class_Float_5] = MC.[Machine_Class_Float_5],
	           [Machine_Class_Float_2] = MC.[Machine_Class_Float_2],
	           [Machine_Class_Config_Div_Level_1] = MC.[Machine_Class_Config_Div_Level_1],
	           [Machine_Class_Config_Div_Level_2] = MC.[Machine_Class_Config_Div_Level_2],
	           [Machine_Class_Config_Div_Level_3] = MC.[Machine_Class_Config_Div_Level_3],
	           [Machine_Class_Config_Div_Level_4] = MC.[Machine_Class_Config_Div_Level_4],
	           [Machine_Class_Config_Single_Comm] = MC.[Machine_Class_Config_Single_Comm],
	           [Machine_Class_Config_Machine_Mode] = MC.[Machine_Class_Config_Machine_Mode],
	           [Machine_Class_Config_Machine_Version] = MC.[Machine_Class_Config_Machine_Version],
	           [Machine_Class_Config_Attract_Mode_Text] = MC.[Machine_Class_Config_Attract_Mode_Text],
	           [Machine_Class_Config_Bank_Options] = MC.[Machine_Class_Config_Bank_Options],
	           [Machine_Class_Config_Max_Credit_Limit] = MC.[Machine_Class_Config_Max_Credit_Limit],
	           [Machine_Class_Config_Max_Bank_Value] = MC.[Machine_Class_Config_Max_Bank_Value],
	           [Machine_Class_Price_Of_Play] = MC.[Machine_Class_Price_Of_Play],
	           [Machine_Class_Jackpot] = MC.[Machine_Class_Jackpot],
	           [Machine_Class_Percent_Cash_Payout] = MC.[Machine_Class_Percent_Cash_Payout],
	           [Machine_Class_Percent_Token_Payout] = MC.[Machine_Class_Percent_Token_Payout],
	           [Machine_Class_Speaker_Volume] = MC.[Machine_Class_Speaker_Volume],
	           [Machine_Class_SP_Features] = MC.[Machine_Class_SP_Features],
	           [Machine_Class_Release_Date] = MC.[Machine_Class_Release_Date],
	           [Machine_Class_Category_ID] = MC.[Machine_Class_Category_ID],
	           [Machine_Class_Is_Ticket] = MC.[Machine_Class_Is_Ticket],
	           [Machine_Class_RecreateCancelledCredits] = MC.[Machine_Class_RecreateCancelledCredits],
	           [Machine_Class_JackpotAddedToCancelledCredits] = MC.[Machine_Class_JackpotAddedToCancelledCredits],
	           [Machine_Class_AddTrueCoinInToDrop] = MC.[Machine_Class_AddTrueCoinInToDrop],
	           [Machine_Class_UseCancelledCreditsAsTicketsPrinted] = MC.[Machine_Class_UseCancelledCreditsAsTicketsPrinted],
	           [Machine_Class_RecreateTicketsInsertedfromDrop] = MC.[Machine_Class_RecreateTicketsInsertedfromDrop],
	           [Meter_Rollover] = MC.[Meter_Rollover],
	           --[Machine_Class_Occupancy_Games_Per_Hour] = M.[Machine_Occupancy_Hour],
	           [Validation_Length] = MC.[Validation_Length]
	    FROM   [Machine_Class] MC
	   
	    INNER  JOIN  machine M on M.machine_class_id = Mc.Machine_class_Id 
	                                   WHERE  m.Machine_Stock_No = @StockNumber
	           AND TemplateMachine_Class.TemplateMachine_Class_ID = @Machine_Class_ID
	END
	ELSE
	BEGIN
	    DELETE 
	    FROM   AssetCreationTemplate
	    WHERE  TemplateName = @TemplateName
	    
	    DELETE 
	    FROM   TemplateMachine_Class
	    WHERE  TemplateMachine_Class_ID = @Machine_Class_ID
	END
END
GO



