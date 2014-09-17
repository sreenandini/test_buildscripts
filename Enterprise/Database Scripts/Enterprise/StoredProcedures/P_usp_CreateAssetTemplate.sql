USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_CreateAssetTemplate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_CreateAssetTemplate]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[usp_CreateAssetTemplate]

 
  @AssetNumber          varchar(50),
  @TemplateName				VARCHAR(50)
AS
BEGIN
INSERT INTO [TemplateMachine_Class]
 SELECT 
	[Machine_Type_ID],
	[Manufacturer_ID],
	machine_Class.[Depreciation_Policy_ID],
	machine_class.[Depreciation_Policy_Use_Default],
	[Machine_Class_Category],
	[Machine_Class_Model_Code],
	[Machine_BACTA_Code],
	[Machine_Name],
	[Machine_Hopper_Type],
	[Machine_Max_No_Of_Discs],
	[Machine_Disc_Type],
	[Label_ID],
	[Machine_Picture_Label],
	[Machine_CD_Numbering],
	[Machine_Track_Numbering],
	[Machine_Class_Counter_Cash_In_Units],
	[Machine_Class_Counter_Cash_Out_Units],
	[Machine_Class_Counter_Tokens_In_Units],
	[Machine_Class_Counter_Tokens_Out_Units],
	[Machine_Class_Counter_Refill_Units],
	[Machine_Class_Counter_Jackpot_Units],
	[Machine_Class_Counter_Prize_Units],
	[Machine_Class_Counter_Tournament_Play_Units],
	[Machine_Class_Counter_JukeBox_Play_Units],
	[Machine_Class_Prize_Machine],
	[Machine_Class_Max_Float_Level],
	[Machine_Class_Default_Coin_Mech],
	[Machine_Class_Default_Note_Acceptor],
	[Machine_Class_Test_Machine],
	[Machine_Class_Delisted],
	[Machine_Class_Features],
	[Machine_Class_Tolerance_Cash_In],
	[Machine_Class_Tolerance_Cash_Out],
	[Machine_Class_Tolerance_Tokens_In],
	[Machine_Class_Tolerance_Tokens_Out],
	[Machine_Class_Tolerance_Refills],
	[Machine_Class_Tolerance_Tournament],
	[Machine_Class_Tolerance_Jukebox],
	[Machine_Class_Tolerance_Prize],
	[Machine_Class_Float_200],
	[Machine_Class_Float_100],
	[Machine_Class_Float_50],
	[Machine_Class_Float_20],
	[Machine_Class_Float_10],
	[Machine_Class_Float_5],
	[Machine_Class_Float_2],
	[Machine_Class_Config_Div_Level_1],
	[Machine_Class_Config_Div_Level_2],
	[Machine_Class_Config_Div_Level_3],
	[Machine_Class_Config_Div_Level_4],
	[Machine_Class_Config_Single_Comm],
	[Machine_Class_Config_Machine_Mode],
	[Machine_Class_Config_Machine_Version],
	[Machine_Class_Config_Attract_Mode_Text],
	[Machine_Class_Config_Bank_Options],
	[Machine_Class_Config_Max_Credit_Limit],
	[Machine_Class_Config_Max_Bank_Value],
	[Machine_Class_Price_Of_Play],
	[Machine_Class_Jackpot],
	[Machine_Class_Percent_Cash_Payout],
	[Machine_Class_Percent_Token_Payout],
	[Machine_Class_Speaker_Volume],
	[Machine_Class_SP_Features],
	[Machine_Class_Release_Date],
	[Machine_Class_Category_ID],
	[Machine_Class_Is_Ticket],
	[Machine_Class_RecreateCancelledCredits],
	[Machine_Class_JackpotAddedToCancelledCredits],
	[Machine_Class_AddTrueCoinInToDrop],
	[Machine_Class_UseCancelledCreditsAsTicketsPrinted],
	[Machine_Class_RecreateTicketsInsertedfromDrop],
	[Meter_Rollover],
	Machine_Occupancy_Hour,
	[Validation_Length]
  from Machine_Class  INNER JOIN MACHINE m ON m.Machine_Class_ID = machine_class.Machine_Class_ID
   WHERE Machine_Stock_No = @AssetNumber 
  AND RTRIM(LTRIM(ISNULL(Machine_End_Date,''))) = ''

DECLARE @Machine_Class_ID INT
SET @Machine_Class_ID = SCOPE_IDENTITY()

IF @Machine_Class_ID <= 0
RETURN

INSERT INTO AssetCreationTemplate
SELECT @TemplateName, @Machine_Class_ID, m.Operator_ID, m.Terms_Profile_ID, m.Depreciation_Policy_ID,
       m.Depreciation_Policy_Use_Default, m.Machine_Number_Of_Discs,
       m.Machine_Stock_No, m.Machine_Counter_Cash_In_Units,
       m.Machine_Counter_Cash_Out_Units, m.Machine_Counter_Tokens_In_Units,
       m.Machine_Counter_Tokens_Out_Units, m.Machine_Counter_Refill_Units,
       m.Machine_Counter_Jackpot_Units,
       m.Machine_Counter_Prize_Units, m.Machine_Counter_JukeBox_Play_Units,
       m.Machine_Counter_Tournament_Play_Units, m.Machine_Test,
       m.Machine_Status_Flag, m.Machine_Status, m.Machine_Start_Date,
       m.Machine_End_Date, m.Machine_Resale_Value, m.Machine_Sales_Invoice_Number,
       m.Machine_Type_Of_Sale, m.Machine_Sold_To, m.Machine_PROM_Version,
       m.Machine_Original_Purchase_Price, m.Machine_Sale_Price,
       m.Machine_Purchase_Invoice_Number, m.Depot_ID,
       m.Machine_AMEDIS_Variant_Code, m.Machine_Previous_Machine_ID,
       m.Machine_Manufacturers_Serial_No, m.Machine_Purchased_From,
       m.Machine_Depreciation_Start_Date, m.Machine_Last_PAT_Date,
       m.Machine_PAT_Required, m.Machine_Alternative_Serial_Numbers, m.Staff_ID,
       m.Machine_Due_In_Stock, m.Machine_Due_In_Stock_Date, m.Machine_Memo,
       m.Machine_Extra_Details, m.Staff_ID_Entered, m.Staff_ID_Deleted,
       m.Machine_Date_Entered, m.Machine_Date_Deleted,
       m.Machine_Float_200p_Capacity, m.Machine_Float_Maximum_Capacity,
       m.Machine_Float_100p_Capacity, m.Machine_Float_50p_Capacity,
       m.Machine_Float_20p_Capacity, m.Machine_Float_10p_Capacity,
       m.Machine_Float_5p_Capacity, m.Machine_Float_2p_Capacity,
       m.Machine_Site_Planned_Movement_ID, m.Machine_Depot_Planned_Movement_ID,
       m.Machine_Category_ID, m.Machine_MAC_Address, m.IsMultiGame,
       m.Machine_Connection_Type, m.Machine_New_Install,
       m.Machine_MAC_Address_Prev, m.Machine_Termination_Reason,
       m.Machine_Termination_Comments, m.Machine_Termination_Username,
       m.Machine_ModelTypeID, m.Machine_Status_Flag_Prev, m.Machine_CIV,Machine_CIV_Prev,
       m.Machine_CIV_Change_Reason, m.IsAFTEnabled, m.Machine_Transit_Site_Code,
       m.IsTITOEnabled, m.IsNonCashVoucherEnabled, m.ActAssetNo, m.GMUNo,
       m.ActSerialNo, m.EnrolmentFlag, m.CMPGameType, m.Machine_Uses_Meters,
       m.Stacker_ID, m.GetGameDetails, m.Base_Denom, m.Percentage_Payout,
       m.IsDefaultAssetDetail,m.IsGameCappingEnabled,
       m.AssetDisplayName, Machine_Occupancy_Hour
  FROM [Machine] m WHERE Machine_Stock_No = @AssetNumber
  AND RTRIM(LTRIM(ISNULL(Machine_End_Date,''))) = ''

END
GO

