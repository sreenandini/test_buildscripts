USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ImportAsset]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ImportAsset]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_ImportAsset(@Stock_No VARCHAR(50), 
								 @Machine_Name VARCHAR(50) , 
								 @Machine_Type_Code VARCHAR(50), 
								 @ManufacturerId INT,
								 @Validation_Length INT,
								 @Machine_Class_Occupancy_Games_Per_Hour INT,
								 @Operator_ID INT,
								 @Depot_ID INT,
								 @Depreciation_Policy_Name VARCHAR(50), 
								 @Depreciation_Policy_Use_Default VARCHAR(1),
								 @Machine_Purchase_Invoice_Number VARCHAR(50),
								 @Machine_Manufacturers_Serial_No VARCHAR(50),
								 @Machine_Alternative_Serial_Numbers VARCHAR(50),
								 @Machine_Purchased_From VARCHAR(50),
								 @Machine_Memo VARCHAR(2000),
								 @Machine_MAC_Address VARCHAR(50),
								 @IsMultiGame VARCHAR(5),
								 @MT_Model_Name VARCHAR(20),
								 @IsAFTEnabled VARCHAR(5),
								 @IsTITOEnabled VARCHAR(5),
								 @IsNonCashVoucherEnabled VARCHAR(5),
								 @ActAssetNo VARCHAR(50),
								 @GMUNo VARCHAR(50),
							     @ActSerialNo VARCHAR(50),
								 @CMPGamePrefix VARCHAR(5),
								 @CMPGameType VARCHAR(50),
								 @StackerID int,
								 @Base_Denom INT,
								 @Percentage_Payout REAL,
								 @IsDefaultAssetDetail BIT,
								 @GetGameDetails BIT,
								 @IsGameCappingEnabled BIT,
								 @AssetDisplayName VARCHAR(8),
								 @MachineTypeOfSale VARCHAR(50),								 
								 @MachineSoldTo VARCHAR(50),
								 @UserID INT,
								 @UserName VARCHAR(50),
								 @StaffRepresentative VARCHAR(50)
								 ) 
AS
BEGIN
	
	IF NOT EXISTS(SELECT 1 FROM Machine WITH(NOLOCK) WHERE Machine_Stock_No = @Stock_No)
	BEGIN
	
		EXEC usp_CreateAsset
								 @Stock_No ,
								 @Machine_Name , 
								 @Machine_Type_Code ,
								 @ManufacturerId, 
								 @Validation_Length,							
								 @Machine_Class_Occupancy_Games_Per_Hour, 
							     @Operator_ID,
							     @Depot_ID,
							     @Depreciation_Policy_Name,
							     @Depreciation_Policy_Use_Default,
							     @Machine_Purchase_Invoice_Number,
							     @Machine_Manufacturers_Serial_No,
							     @Machine_Alternative_Serial_Numbers,
							     @Machine_Purchased_From,
							     @Machine_Memo,
							     @Machine_MAC_Address, 
							     @IsMultiGame,
							     @MT_Model_Name,
							     @IsAFTEnabled,
							     @IsTITOEnabled,
							     @IsNonCashVoucherEnabled,
							     @ActAssetNo, 
							     @GMUNo,
							     @ActSerialNo, 
							     @CMPGamePrefix, 
							     @CMPGameType,
							     @StackerID,
							     @Base_Denom,
							     @Percentage_Payout, 
							     @IsDefaultAssetDetail,
							     @GetGameDetails,
							     @IsGameCappingEnabled,
							     @AssetDisplayName,
							     @MachineTypeOfSale,
							     @MachineSoldTo ,
							     @UserID,
							     @UserName,
							     @StaffRepresentative 
	END
	ELSE
	BEGIN
		EXEC usp_ModifyAsset @Stock_No ,
		                     @Machine_Name , 
		                     @Machine_Type_Code , 
		                     @ManufacturerId,  
		                     @Validation_Length,
							 @Machine_Class_Occupancy_Games_Per_Hour, 
							 @Operator_ID,
							 @Depot_ID, 
							 @Depreciation_Policy_Name, 
							 @Depreciation_Policy_Use_Default, 
							 @Machine_Purchase_Invoice_Number,
							 @Machine_Manufacturers_Serial_No, 
							 @Machine_Alternative_Serial_Numbers, 
							 @Machine_Purchased_From,
							 @Machine_Memo, 
							 @Machine_MAC_Address, 
							 @IsMultiGame, 
							 @MT_Model_Name,
							 @IsAFTEnabled,
							 @IsTITOEnabled,
							 @IsNonCashVoucherEnabled, 
							 @ActAssetNo, 
							 @GMUNo,
							 @ActSerialNo, 
							 @CMPGamePrefix,  
							 @CMPGameType,
							 @StackerID,
							 @Base_Denom, 
							 @Percentage_Payout, 
							 @IsDefaultAssetDetail,
							 @GetGameDetails,
							 @IsGameCappingEnabled,
							 @AssetDisplayName,
							 @MachineTypeOfSale,							 
						     @MachineSoldTo ,
						     @UserID,
							 @UserName,
							 @StaffRepresentative 
	END
	
END

GO

