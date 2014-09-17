USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ConvertMachine]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ConvertMachine]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------------------- 
--
-- Description: converts a machine from one to another
--    
--    Steps
--    1. Create machine based on previous, setting model type against new machine
--    2. Create installation based on previous, setting machine_id
--    3. expire the old machine
--    4. expire the old installation
--
-- Inputs:      See inputs 
--
-- Outputs:     NONE
--
-- Return:          0 - All Ok
--                  1 - installation_No does not exist in installation table.
--              OTHER - SQL Error
--
-- =======================================================================
-- 
-- Revision History
-- 
-- C.Taylor     07/05/2007     Created
-- C.Taylor     12/06/2007     Updated dates to dd mmm yyyy
--------------------------------------------------------------------------- 
CREATE PROCEDURE [dbo].[usp_ConvertMachine]

  @Machine_ID         INT,
  @User_ID            INT,
  @Machine_Class_ID   INT,
  @ConvertDate        datetime


AS

  SET DATEFORMAT DMY

  DECLARE @STOCK_CONVERTED   int,
          @NewMachineID      int,
          @ModelName         varchar(200),
          @NewInstallationID int,
          @ddmmmyyyy         varchar(12),
          @hhnnss            varchar(8),
          @Installation_ID   int
  
  SET @ddmmmyyyy = convert ( varchar(12), @ConvertDate, 106 )  -- dd mmm yyyy
  SET @hhnnss   = convert ( varchar(8), @ConvertDate, 108 ) -- hh:nn:ss
  SET @STOCK_CONVERTED = 7

  print @ddmmmyyyy

  -- get model name
  SELECT @ModelName = Machine_Name
    FROM Machine_Class
   WHERE Machine_Class_ID = @Machine_Class_ID
  
  SELECT @Installation_ID = Installation_ID
    FROM Installation
   WHERE Machine_ID = @Machine_ID

  -- create new machine ( based on old machine )
  INSERT INTO Machine 
  (
    [Machine_Class_ID],
    [Operator_ID],
    [Terms_Profile_ID],
    [Depreciation_Policy_ID],
    [Depreciation_Policy_Use_Default],  
    [Machine_Number_Of_Discs],
    [Machine_Stock_No],
    [Machine_Counter_Cash_In_Units],
    [Machine_Counter_Cash_Out_Units],
    [Machine_Counter_Tokens_In_Units],
    [Machine_Counter_Tokens_Out_Units],
    [Machine_Counter_Refill_Units],
    [Machine_Counter_Jackpot_Units],
    [Machine_Counter_Prize_Units],
    [Machine_Counter_Tournament_Play_Units],
    [Machine_Counter_JukeBox_Play_Units],
    [Machine_Test],
    [Machine_Status_Flag],
    [Machine_Status],
    [Machine_Start_Date],  
    [Machine_End_Date],
    [Machine_Resale_Value],
    [Machine_Sales_Invoice_Number],
    [Machine_Sold_To],
    [Machine_Type_Of_Sale],
    [Machine_PROM_Version],
    [Machine_Original_Purchase_Price],
    [Machine_Sale_Price],
    [Machine_Purchase_Invoice_Number],
    [Depot_ID],
    [Machine_AMEDIS_Variant_Code],
    [Machine_Previous_Machine_ID],
    [Machine_Manufacturers_Serial_No],
    [Machine_Purchased_From],
    [Machine_Depreciation_Start_Date],
    [Machine_Last_PAT_Date],
    [Machine_PAT_Required],
    [Machine_Alternative_Serial_Numbers],
    [Staff_ID],
    [Machine_Due_In_Stock],
    [Machine_Due_In_Stock_Date],
    [Machine_Memo],
    [Machine_Extra_Details],
    [Staff_ID_Entered],
    [Staff_ID_Deleted],
    [Machine_Date_Entered],
    [Machine_Date_Deleted],
    [Machine_Float_Maximum_Capacity],
    [Machine_Float_200p_Capacity],
    [Machine_Float_100p_Capacity],
    [Machine_Float_50p_Capacity],
    [Machine_Float_20p_Capacity],
    [Machine_Float_10p_Capacity],
    [Machine_Float_5p_Capacity],
    [Machine_Float_2p_Capacity],
    [Machine_Site_Planned_Movement_ID],
    [Machine_Depot_Planned_Movement_ID],
    [Machine_Category_ID]
  )

  SELECT @Machine_Class_ID,     -- [Machine_Class_ID]
         [Operator_ID],
         [Terms_Profile_ID],
         [Depreciation_Policy_ID],
         [Depreciation_Policy_Use_Default],
         [Machine_Number_Of_Discs],
         [Machine_Stock_No],
         [Machine_Counter_Cash_In_Units],
         [Machine_Counter_Cash_Out_Units],
         [Machine_Counter_Tokens_In_Units],
         [Machine_Counter_Tokens_Out_Units],
         [Machine_Counter_Refill_Units],
         [Machine_Counter_Jackpot_Units],
         [Machine_Counter_Prize_Units],
         [Machine_Counter_Tournament_Play_Units],
         [Machine_Counter_JukeBox_Play_Units],
         [Machine_Test],
         [Machine_Status_Flag],
         [Machine_Status],
         @ddmmmyyyy,       -- [Machine_Start_Date]
         NULL,             -- [Machine_End_Date]
         [Machine_Resale_Value],
         [Machine_Sales_Invoice_Number],
         [Machine_Sold_To],
         [Machine_Type_Of_Sale],
         [Machine_PROM_Version],
         [Machine_Original_Purchase_Price],
         [Machine_Sale_Price],
         [Machine_Purchase_Invoice_Number],
         [Depot_ID],
         [Machine_AMEDIS_Variant_Code],
		 @Machine_ID, -- [Machine_Previous_Machine_ID]
--         CASE WHEN ( COALESCE( [Machine_Previous_Machine_ID], 0 ) > 0 ) THEN [Machine_Previous_Machine_ID]
--              ELSE @Machine_ID
--              END,        -- [Machine_Previous_Machine_ID]
         [Machine_Manufacturers_Serial_No],
         [Machine_Purchased_From],
         [Machine_Depreciation_Start_Date],
         [Machine_Last_PAT_Date],
         [Machine_PAT_Required],
         [Machine_Alternative_Serial_Numbers],
         [Staff_ID],
         [Machine_Due_In_Stock],
         [Machine_Due_In_Stock_Date],
         [Machine_Memo],
         [Machine_Extra_Details],
         [Staff_ID_Entered],
         [Staff_ID_Deleted],
         [Machine_Date_Entered],
         [Machine_Date_Deleted],
         [Machine_Float_Maximum_Capacity],
         [Machine_Float_200p_Capacity],
         [Machine_Float_100p_Capacity],
         [Machine_Float_50p_Capacity],
         [Machine_Float_20p_Capacity],
         [Machine_Float_10p_Capacity],
         [Machine_Float_5p_Capacity], 
         [Machine_Float_2p_Capacity],
         [Machine_Site_Planned_Movement_ID],
         [Machine_Depot_Planned_Movement_ID],
         [Machine_Category_ID] 

    FROM Machine
   WHERE Machine_ID = @Machine_ID

  print cast(@@ROWCOUNT as varchar(10))

  SET @NewMachineID =SCOPE_IDENTITY() --IDENT_CURRENT('Machine')

  print cast ( @NewMachineID as varchar(10)) 

  -- create and installation
  --
  INSERT INTO Installation
  ( 
     [Bar_Position_ID],
     [Machine_ID],
     [Datapak_ID],
     [Depot_ID],
     [CD_Program_ID],
     [Duty_ID],
     [Installation_Previous_Installation],
     [Installation_Reference],
     [Installation_Start_Date],
     [Installation_Start_Time],
     [Installation_End_Date],
     [Installation_End_Time],
     [Installation_Percentage_Payout],
     [Installation_Initial_Coins_In],
     [Installation_Initial_Coins_Out],
     [Installation_Initial_Tokens_In],
     [Installation_Initial_Tokens_Out],
     [Installation_Initial_Refill_Meter],
     [Installation_Initial_Jackpot_Meter],
     [Installation_Initial_Prize_Meter],
     [Installation_Initial_Tournament_Play_Meter],
     [Installation_Initial_JukeBox_Play_Meter],
     [Installation_Final_Coins_In],
     [Installation_Final_Coins_Out],
     [Installation_Final_Tokens_In],
     [Installation_Final_Tokens_Out],
     [Installation_Final_Refill_Meter],
     [Installation_Final_Jackpot_Meter],
     [Installation_Final_Prize_Meter],
     [Installation_Final_Tournament_Play_Meter],
     [Installation_Final_JukeBox_Play_Meter],
     [Installation_Counter_Cash_In_Units],
     [Installation_Counter_Cash_Out_Units],
     [Installation_Counter_Tokens_In_Units],
     [Installation_Counter_Tokens_Out_Units],
     [Installation_Counter_Refill_Units],
     [Installation_Counter_Jackpot_Units],
     [Installation_Counter_Prize_Units],
     [Installation_Counter_Tournament_Play_Units],
     [Installation_Counter_JukeBox_Play_Units],
     [Installation_Jackpot_Value],
     [Installation_Price_Per_Play],
     [Installation_Accessory_1_Used],
     [Installation_Accessory_1_Name],
     [Installation_Accessory_1_Reference],
     [Installation_Accessory_1_Has_Meters],
     [Installation_Accessory_1_Initial_Coins_In_Meter],
     [Installation_Accessory_1_Initial_Coins_Out_Meter],
     [Installation_Accessory_2_Used],
     [Installation_Accessory_2_Name],
     [Installation_Accessory_2_Reference],
     [Installation_Accessory_2_Has_Meters],
     [Installation_Accessory_2_Initial_Coins_In_Meter],
     [Installation_Accessory_2_Initial_Coins_Out_Meter],
     [Installation_Accessory_3_Used],
     [Installation_Accessory_3_Name],
     [Installation_Accessory_3_Reference],
     [Installation_Accessory_3_Has_Meters],
     [Installation_Accessory_3_Initial_Coins_In_Meter],
     [Installation_Accessory_3_Initial_Coins_Out_Meter],
     [Installation_Accessory_4_Used],
     [Installation_Accessory_4_Name],
     [Installation_Accessory_4_Reference],
     [Installation_Accessory_4_Has_Meters],
     [Installation_Accessory_4_Initial_Coins_In_Meter],
     [Installation_Accessory_4_Initial_Coins_Out_Meter],
     [Installation_Accessory_5_Used],
     [Installation_Accessory_5_Name],
     [Installation_Accessory_5_Reference],
     [Installation_Accessory_5_Has_Meters],
     [Installation_Accessory_5_Initial_Coins_In_Meter],
     [Installation_Accessory_5_Initial_Coins_Out_Meter],
     [Installation_Change_Flag],
     [Installation_Change_Flag_Date_Entered],
     [Installation_Change_Flag_Staff_ID],
     [Installation_Change_Flag_Comments],
     [Installation_Amedis_Export_In_Status],
     [Installation_Amedis_Export_In_Log_ID],
     [Installation_McMullens_Export_In_Status],
     [Installation_McMullens_Export_In_Log_ID],
     [Installation_LeisureData_Export_In_Status],
     [Installation_LeisureData_Export_In_Log_ID],
     [Installation_Honeyframe_Export_In_Status],
     [Installation_Honeyframe_Export_In_Log_ID],
     [Installation_Amedis_Export_Out_Status],   
     [Installation_Amedis_Export_Out_Log_ID],
     [Installation_McMullens_Export_Out_Status],
     [Installation_McMullens_Export_Out_Log_ID],
     [Installation_LeisureData_Export_Out_Status],
     [Installation_LeisureData_Export_Out_Log_ID],
     [Installation_Honeyframe_Export_Out_Status],
     [Installation_Honeyframe_Export_Out_Log_ID],
     [Installation_Audit_Install],
     [Installation_Audit_Withdrawl],
     [Installation_Amedis_Import_Log_ID],
     [Installation_Amedis_Import_Log_Withdrawl_ID],
     [Installation_BACTA_Code_Override],
     [Installation_Initial_SC_Coins_In],
     [Installation_Initial_SC_Coins_Out],
     [Installation_Initial_Coins_Drop],
     [Installation_Initial_ExternalCredit],
     [Installation_Initial_GamesBet],
     [Installation_Initial_GamesWon],
     [Installation_Initial_Notes],
     [Installation_Initial_Handpay],
     [Installation_RDC_Machine_Code],
     [Installation_RDC_Secondary_Machine_Code],
     [Installation_RDC_Datapak_Type],
     [Installation_RDC_Datapak_Version],
     [Installation_Token_Value],  
     [Installation_Initial_Change],
     [Installation_Initial_VTP]
  )

  SELECT [Bar_Position_ID],
         @NewMachineID,     -- [Machine_ID]
         [Datapak_ID],
         [Depot_ID],
         [CD_Program_ID],
         [Duty_ID],
         [Installation_Previous_Installation],
         [Installation_Reference],
         @ddmmmyyyy,     -- [Installation_Start_Date]
         @hhnnss,        -- [Installation_Start_Time]
         NULL,           -- [Installation_End_Date]
         NULL,           -- [Installation_End_Time]
         [Installation_Percentage_Payout],
         [Installation_Initial_Coins_In],
         [Installation_Initial_Coins_Out],
         [Installation_Initial_Tokens_In],
         [Installation_Initial_Tokens_Out],
         [Installation_Initial_Refill_Meter],
         [Installation_Initial_Jackpot_Meter],
         [Installation_Initial_Prize_Meter],
         [Installation_Initial_Tournament_Play_Meter],
         [Installation_Initial_JukeBox_Play_Meter],
         [Installation_Final_Coins_In],
         [Installation_Final_Coins_Out],
         [Installation_Final_Tokens_In],
         [Installation_Final_Tokens_Out],
         [Installation_Final_Refill_Meter],
         [Installation_Final_Jackpot_Meter],
         [Installation_Final_Prize_Meter],
         [Installation_Final_Tournament_Play_Meter],
         [Installation_Final_JukeBox_Play_Meter],
         [Installation_Counter_Cash_In_Units],
         [Installation_Counter_Cash_Out_Units],
         [Installation_Counter_Tokens_In_Units],
         [Installation_Counter_Tokens_Out_Units],
         [Installation_Counter_Refill_Units],
         [Installation_Counter_Jackpot_Units],
         [Installation_Counter_Prize_Units],
         [Installation_Counter_Tournament_Play_Units],
         [Installation_Counter_JukeBox_Play_Units],
         [Installation_Jackpot_Value],
         [Installation_Price_Per_Play],
         [Installation_Accessory_1_Used],
         [Installation_Accessory_1_Name],
         [Installation_Accessory_1_Reference],
         [Installation_Accessory_1_Has_Meters],
         [Installation_Accessory_1_Initial_Coins_In_Meter],
         [Installation_Accessory_1_Initial_Coins_Out_Meter],
         [Installation_Accessory_2_Used],
         [Installation_Accessory_2_Name],
         [Installation_Accessory_2_Reference],
         [Installation_Accessory_2_Has_Meters],
         [Installation_Accessory_2_Initial_Coins_In_Meter],
         [Installation_Accessory_2_Initial_Coins_Out_Meter],
         [Installation_Accessory_3_Used],
         [Installation_Accessory_3_Name],
         [Installation_Accessory_3_Reference],
         [Installation_Accessory_3_Has_Meters],
         [Installation_Accessory_3_Initial_Coins_In_Meter],
         [Installation_Accessory_3_Initial_Coins_Out_Meter],
         [Installation_Accessory_4_Used],
         [Installation_Accessory_4_Name],
         [Installation_Accessory_4_Reference],
         [Installation_Accessory_4_Has_Meters],
         [Installation_Accessory_4_Initial_Coins_In_Meter],
         [Installation_Accessory_4_Initial_Coins_Out_Meter],
         [Installation_Accessory_5_Used],
         [Installation_Accessory_5_Name],
         [Installation_Accessory_5_Reference],
         [Installation_Accessory_5_Has_Meters],
         [Installation_Accessory_5_Initial_Coins_In_Meter],
         [Installation_Accessory_5_Initial_Coins_Out_Meter],
         [Installation_Change_Flag] [bit],
         [Installation_Change_Flag_Date_Entered],
         [Installation_Change_Flag_Staff_ID],
         [Installation_Change_Flag_Comments],
         [Installation_Amedis_Export_In_Status],
         [Installation_Amedis_Export_In_Log_ID],
         [Installation_McMullens_Export_In_Status],
         [Installation_McMullens_Export_In_Log_ID],
         [Installation_LeisureData_Export_In_Status],
         [Installation_LeisureData_Export_In_Log_ID],
         [Installation_Honeyframe_Export_In_Status],   
         [Installation_Honeyframe_Export_In_Log_ID],
         [Installation_Amedis_Export_Out_Status],   
         [Installation_Amedis_Export_Out_Log_ID],
         [Installation_McMullens_Export_Out_Status],
         [Installation_McMullens_Export_Out_Log_ID],  
         [Installation_LeisureData_Export_Out_Status],
         [Installation_LeisureData_Export_Out_Log_ID],  
         [Installation_Honeyframe_Export_Out_Status],
         [Installation_Honeyframe_Export_Out_Log_ID],
         [Installation_Audit_Install],
         [Installation_Audit_Withdrawl],
         [Installation_Amedis_Import_Log_ID],
         [Installation_Amedis_Import_Log_Withdrawl_ID],
         [Installation_BACTA_Code_Override],
         0,       -- [Installation_Initial_SC_Coins_In]
         0,       -- [Installation_Initial_SC_Coins_Out]
         0,         -- [Installation_Initial_Coins_Drop]
         0,         -- [Installation_Initial_ExternalCredit]
         0,         -- [Installation_Initial_GamesBet]
         0,         -- [Installation_Initial_GamesWon]
         0,         -- [Installation_Initial_Notes]
         0,         -- [Installation_Initial_Handpay]
         [Installation_RDC_Machine_Code],
         [Installation_RDC_Secondary_Machine_Code],
         [Installation_RDC_Datapak_Type],
         [Installation_RDC_Datapak_Version],
         [Installation_Token_Value],  
         [Installation_Initial_Change],
         0        -- [Installation_Initial_VTP]
  
    FROM Installation

   WHERE Installation_ID = @Installation_ID
/*****************************************************************************************************
* (CR# 191254 : EBS Communication Service) - MODIFIED BY Venkatesh.H  (START)
*****************************************************************************************************/


   
   EXEC [dbo].[usp_EBS_UpdateMachineDetails] @NewMachineID

  print cast(@@ROWCOUNT as varchar(10))

  SET @NewInstallationID = IDENT_CURRENT('Installation')

  print cast ( @NewInstallationID  as varchar(10)) 

  -- expire the old machine
  --
UPDATE Machine
     SET Machine_end_date = @ddmmmyyyy,
         Machine_Status_Flag = @STOCK_CONVERTED,
         Staff_ID_Deleted = @User_ID,
         Machine_Date_Deleted = getdate(),
         Machine_Type_Of_Sale = LEFT ( 'Converted to: ' + @ModelName, 50 )
   WHERE machine_ID = @Machine_ID

  print cast(@@ROWCOUNT as varchar(10))

  -- expire old installation, 
  --
  UPDATE Installation 
     SET Installation_End_Date = @ddmmmyyyy, 
         Installation_End_Time = @hhnnss  
   WHERE Installation_ID = @Installation_ID

  print cast(@@ROWCOUNT as varchar(10))
EXEC [dbo].[usp_EBS_UpdateMachineDetails] @Machine_ID
/*****************************************************************************************************
* (CR# 191254 : EBS Communication Service) - MODIFIED BY Venkatesh.H  (END)
*****************************************************************************************************/

-- return error
RETURN @@ERROR




GO

