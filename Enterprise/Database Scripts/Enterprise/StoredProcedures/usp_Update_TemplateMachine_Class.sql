/************************************************************
 * Created by: Venkatesan.H  Version:12.4.2
 * Time: 14/05/13 4:41:37 PM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_Update_TemplateMachine_Class]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_Update_TemplateMachine_Class]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_Update_TemplateMachine_Class
	@TemplateName VARCHAR(50),
	@Machine_Name VARCHAR(50),
	@Manufacturer_ID INT,
	@Machine_Type_ID INT OUTPUT,
	@Machine_Class_Category_ID INT,
	@Machine_Class_SP_Features INT,
	@Machine_Class_Model_Code VARCHAR(50),
	@Depreciation_Policy_ID INT,
	@Depreciation_Policy_Use_Default BIT,
	@Machine_Class_Occupancy_Games_Per_Hour INT,
	@Machine_Class_Counter_Cash_In_Units INT,
	@Machine_Class_Counter_Cash_Out_Units INT,
	@Machine_Class_Counter_Tokens_In_Units INT,
	@Machine_Class_Counter_Tokens_Out_Units INT,
	@Machine_Class_Config_Machine_Version VARCHAR(50),
	@Machine_Class_Config_Attract_Mode_Text VARCHAR(50),
	@Machine_Class_UseCancelledCreditsAsTicketsPrinted BIT,
	@Machine_Class_RecreateTicketsInsertedfromDrop BIT,
	@Meter_Rollover INT,
	@Machine_Class_Test_Machine BIT,
	@Validation_Length INT,
	@MachineClassID INT OUTPUT
AS
BEGIN
	UPDATE TemplateMachine_Class
	SET 
		Machine_Name = @Machine_Name,
		Manufacturer_ID = @Manufacturer_ID,
		--Machine_Type_ID = @Machine_Type_ID,
		Machine_Class_Category_ID = @Machine_Class_Category_ID,
		Machine_Class_SP_Features = @Machine_Class_SP_Features,
		Machine_Class_Model_Code = @Machine_Class_Model_Code,
		Depreciation_Policy_ID = @Depreciation_Policy_ID,
		Depreciation_Policy_Use_Default = @Depreciation_Policy_Use_Default,
		--Machine_Class_Occupancy_Games_Per_Hour = @Machine_Class_Occupancy_Games_Per_Hour,
		Machine_Class_Counter_Cash_In_Units = @Machine_Class_Counter_Cash_In_Units,
		Machine_Class_Counter_Cash_Out_Units = @Machine_Class_Counter_Cash_Out_Units,
		Machine_Class_Counter_Tokens_In_Units = @Machine_Class_Counter_Tokens_In_Units,
		Machine_Class_Counter_Tokens_Out_Units = @Machine_Class_Counter_Tokens_Out_Units,
		Machine_Class_Config_Machine_Version = @Machine_Class_Config_Machine_Version,
		Machine_Class_Config_Attract_Mode_Text = @Machine_Class_Config_Attract_Mode_Text,
		Machine_Class_UseCancelledCreditsAsTicketsPrinted = @Machine_Class_UseCancelledCreditsAsTicketsPrinted,
		Machine_Class_RecreateTicketsInsertedfromDrop = @Machine_Class_RecreateTicketsInsertedfromDrop,
		Meter_Rollover = @Meter_Rollover,
		Machine_Class_Test_Machine = @Machine_Class_Test_Machine,
		Validation_Length = @Validation_Length

	Where TemplateMachine_Class_ID in 
	(Select TemplateMachine_Class_ID FROM AssetCreationTemplate WHERE TemplateName =  @TemplateName)
	
	UPDATE AssetCreationTemplate
	SET Machine_Occupancy_Hour = @Machine_Class_Occupancy_Games_Per_Hour
	WHERE TemplateName =  @TemplateName
	
	Select @Machine_Type_ID = MC.Machine_Type_ID,
	@MachineClassID = MC.TemplateMachine_Class_ID
	FROM AssetCreationTemplate M
	INNER JOIN TemplateMachine_Class MC ON  M.TemplateMachine_Class_ID = MC.TemplateMachine_Class_ID 
	 WHERE TemplateName =  @TemplateName
	
END
GO

