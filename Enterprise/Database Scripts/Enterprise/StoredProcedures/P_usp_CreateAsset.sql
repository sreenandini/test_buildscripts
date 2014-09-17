USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_CreateAsset]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_CreateAsset]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_CreateAsset(
		 @Stock_No VARCHAR(50),   
         @Machine_Name VARCHAR(50) ,   
         @Machine_Type_Code VARCHAR(50),   
         @ManufacturerId INT,  
         @Validation_Length INT,  
         @Machine_Class_Occupancy_Games_Per_Hour INT,  
         @Operator_ID INT,
         @Depot_ID INT,
         @Depreciation_Policy_Name VARCHAR(50),   
         @Depreciation_Policy_Use_Default VARCHAR(5),  
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
         @StackerID INT,
         @Base_Denom INT,
		 @Percentage_Payout REAL,
		 @IsDefaultAssetDetail BIT,		
		 @GetGameDetails BIT,
		 @IsGameCappingEnabled BIT,
		 @AssetDisplayName VARCHAR(8)
		 ,@MachineTypeOfSale VARCHAR(50),
		 @MachineSoldTo VARCHAR(50),
		 @UserID INT,
		 @UserName VARCHAR(50),
		 @StaffRepresentative VARCHAR(50)		 
         )   
AS  
BEGIN  
  
 DECLARE @Machine_ID INT  
 DECLARE @Machine_Class_ID INT  
 DECLARE @Machine_Type_ID INT  
 --DECLARE @Manufacturer_ID INT  
 --DECLARE @Operator_ID INT  
 --DECLARE @Depot_ID INT  
 DECLARE @Depreciation_Policy_ID INT  
 DECLARE @MT_ID INT  
 --DECLARE @Stacker_Id INT  
 DECLARE @GetDate DATETIME   
 DECLARE @AuditDesc VARCHAR(500)  
 DECLARE @AuditId VARCHAR(500)  
 DECLARE @StaffId INT
 DECLARE @AuditUserID INT
   
 SET @GetDate = GETDATE()  
 SET @Machine_Name = RTRIM(LTRIM(@Machine_Name))  
 SET @Machine_Type_Code = RTRIM(LTRIM(@Machine_Type_Code))  
 --SET @Manufacturer_Name = RTRIM(LTRIM(@Manufacturer_Name))  
 SET @Stock_No = RTRIM(LTRIM(@Stock_No))  
 SET @MT_Model_Name = RTRIM(LTRIM(@MT_Model_Name))  
 --SET @Stacker_Name = RTRIM(LTRIM(@Stacker_Name))  
 SET @StaffId=(SELECT TOP 1 Staff_ID FROM Staff WHERE Staff_First_Name+','+Staff_Last_Name = @StaffRepresentative)
 SELECT @AuditUserID =  @UserID
 

 IF @IsDefaultAssetDetail ='False' 
 BEGIN
 SET @Base_Denom=1
 SET @Percentage_Payout=0
 END    
    

  
 --SELECT @Manufacturer_ID =  ISNULL(Manufacturer_ID,0) FROM Manufacturer WITH(NOLOCK) WHERE RTRIM(LTRIM(Manufacturer_Name)) = @Manufacturer_Name  
   
 SELECT @Machine_Type_ID =  ISNULL(Machine_Type_ID,0) FROM Machine_Type WITH(NOLOCK) WHERE RTRIM(LTRIM(Machine_Type_Code)) = @Machine_Type_Code  
   
 SELECT @Machine_Class_ID = ISNULL(Machine_Class_ID,0) FROM Machine_Class WITH(NOLOCK) WHERE RTRIM(LTRIM(Machine_Name)) = @Machine_Name  
 AND Machine_Type_ID = @Machine_Type_ID AND Manufacturer_ID = @ManufacturerId  
   
 
 SELECT @Depreciation_Policy_ID = ISNULL(Depreciation_Policy_ID,0) FROM Depreciation_Policy WITH(NOLOCK) WHERE RTRIM(LTRIM(Depreciation_Policy_Description)) = @Depreciation_Policy_Name  
  
 SELECT @MT_ID = ISNULL(MT_ID,0) FROM Model_Type WITH(NOLOCK) WHERE RTRIM(LTRIM(MT_Model_Name)) = @MT_Model_Name  
  
 --SELECT @Stacker_Id = ISNULL(Stacker_Id,0) FROM Stacker WITH(NOLOCK) WHERE RTRIM(LTRIM(StackerName)) = @Stacker_Name  
 

  
 IF ISNULL(@Machine_Class_ID,0) = 0  
 BEGIN  
  INSERT INTO Machine_Class  
  (  
  Machine_Name,  
  Machine_Type_ID,  
  Machine_Class_Category_ID,  
  Machine_Class_SP_Features,  
  Machine_Class_Model_Code,  
  Depreciation_Policy_ID,  
  Depreciation_Policy_Use_Default,  
  Manufacturer_ID,  
  Machine_Class_Counter_Cash_In_Units,  
  Machine_Class_Counter_Cash_Out_Units,  
  Machine_Class_Counter_Tokens_In_Units,  
  Machine_Class_Counter_Tokens_Out_Units,  
  Machine_Class_Config_Machine_Version,  
  Machine_Class_Config_Attract_Mode_Text,  
  Machine_Class_UseCancelledCreditsAsTicketsPrinted,  
  Machine_Class_RecreateTicketsInsertedfromDrop,  
  Meter_Rollover,  
  Machine_Class_Test_Machine,  
  Validation_Length  
  )  
  SELECT   
  @Machine_Name,  
  @Machine_Type_ID,  
  @Machine_Type_ID,  
  1,  
  @Machine_Name,  
  0,  
  1,  
  @ManufacturerId,  
  0,  
  0,  
  0,  
  0,  
  0,  
  0,  
  0,  
  0,  
  99999999,  
  0,  
  @Validation_Length  
    
  INSERT INTO MeterAnalysis.dbo.Machine_Class  
  (  
  Machine_Name,  
  Machine_Type_ID,  
  Machine_Class_Category_ID,  
  Machine_Class_SP_Features,  
  Machine_Class_Model_Code,  
  Depreciation_Policy_ID,  
  Depreciation_Policy_Use_Default,  
  Manufacturer_ID,  
  Machine_Class_Counter_Cash_In_Units,  
  Machine_Class_Counter_Cash_Out_Units,  
  Machine_Class_Counter_Tokens_In_Units,  
  Machine_Class_Counter_Tokens_Out_Units,  
  Machine_Class_Config_Machine_Version,  
  Machine_Class_Config_Attract_Mode_Text,  
  Machine_Class_UseCancelledCreditsAsTicketsPrinted,  
  Machine_Class_RecreateTicketsInsertedfromDrop,  
  Meter_Rollover,  
  Machine_Class_Test_Machine,  
  Validation_Length  
  )  
  SELECT   
  @Machine_Name,  
  @Machine_Type_ID,  
  @Machine_Type_ID,  
  1,  
  @Machine_Name,  
  0,  
  1,  
  @ManufacturerId,  
  0,  
  0,  
  0,  
  0,  
  0,  
  0,  
  0,  
  0,  
  99999999,  
  0,  
  @Validation_Length  
  SET @Machine_Class_ID = SCOPE_IDENTITY()  
   

  INSERT INTO dbo.Export_History(EH_Date,EH_Reference1,EH_Type, EH_Site_Code)   
  SELECT @GetDate,@Machine_Class_ID,'MODEL', Site_Code FROM dbo.Site WITH(NOLOCK)  
  
 END  
  
 INSERT INTO [Machine]  
 (  
    Machine_Class_ID,  
    Operator_ID,  
    Terms_Profile_ID,  
    Depreciation_Policy_ID,  
    Depreciation_Policy_Use_Default,  
    Machine_Stock_No,  
	Machine_Status_Flag,  
	Machine_Status,  
	Machine_Start_Date,  
	Machine_End_Date,  
    Machine_Purchase_Invoice_Number,  
	Depot_ID,  
	Machine_Manufacturers_Serial_No,  
	Machine_Purchased_From,  
	Machine_Depreciation_Start_Date,  
	Machine_Alternative_Serial_Numbers,  
	Staff_ID,  
	Machine_Memo,   
	Staff_ID_Entered,  
	Machine_Date_Entered,  
	Machine_Category_ID,  
	Machine_MAC_Address,  
	IsMultiGame,  
	Machine_New_Install,  
	Machine_ModelTypeID,  
	IsAFTEnabled,   
	IsTITOEnabled,  
	IsNonCashVoucherEnabled,  
	ActAssetNo,  
	GMUNo,  
	ActSerialNo,  
	EnrolmentFlag,  
	CMPGameType,  
	Stacker_ID,
	Machine_Original_Purchase_Price,
	Base_Denom,
	Percentage_Payout,
	IsDefaultAssetDetail,
	GetGameDetails,
    IsGameCappingEnabled,
    AssetDisplayName 
	,Machine_Type_Of_Sale,
	Machine_Sold_To,
	Machine_Occupancy_Hour
	
 )  
 VALUES  
 (  
 @Machine_Class_ID,  
 @Operator_ID,  
 0,  
 @Depreciation_Policy_ID,  
 CASE WHEN (@Depreciation_Policy_Use_Default = 'True') THEN 1 ELSE 0 END,  
 @Stock_No,  
 0,  
 'Usable Stock',  
 @GetDate,  
 '',  
 @Machine_Purchase_Invoice_Number,  
 @Depot_ID,  
 @Machine_Manufacturers_Serial_No,  
 @Machine_Purchased_From,  
 @GetDate,  
 @Machine_Alternative_Serial_Numbers,  
 @StaffId,  
 @Machine_Memo,  
 @UserID,  
 @GetDate,  
 @Machine_Type_ID,  
 @Machine_MAC_Address,  
 CASE WHEN (@IsMultiGame = 'True') THEN 1 ELSE 0 END,  
 1,  
 @MT_ID,  
 CASE WHEN (@IsAFTEnabled = 'True') THEN 1 ELSE 0 END,  
 CASE WHEN (@IsTITOEnabled = 'True') THEN 1 ELSE 0 END,  
 CASE WHEN (@IsNonCashVoucherEnabled = 'True') THEN 1 ELSE 0 END,  
 @ActAssetNo,  
 @GMUNo,  
 @ActSerialNo,  
 0,  
@CMPGamePrefix,  
@StackerID,
0,
@Base_Denom,
@Percentage_Payout,
@IsDefaultAssetDetail,
@GetGameDetails,
@IsGameCappingEnabled,
@AssetDisplayName 
,@MachineTypeOfSale,
@MachineSoldTo,
@Machine_Class_Occupancy_Games_Per_Hour

 )  
  
  INSERT INTO MeterAnalysis.dbo.[Machine]  
 (  
    Machine_Class_ID,  
    Operator_ID,  
    Terms_Profile_ID,  
    Depreciation_Policy_ID,  
    Depreciation_Policy_Use_Default,  
    Machine_Stock_No,  
	Machine_Status_Flag,  
	Machine_Status,  
	Machine_Start_Date,  
	Machine_End_Date,  
    Machine_Purchase_Invoice_Number,  
	Depot_ID,  
	Machine_Manufacturers_Serial_No,  
	Machine_Purchased_From,  
	Machine_Depreciation_Start_Date,  
	Machine_Alternative_Serial_Numbers,  
	Staff_ID,  
	Machine_Memo,   
	Staff_ID_Entered,  
	Machine_Date_Entered,  
	Machine_Category_ID,  
	Machine_MAC_Address,  
	IsMultiGame,  
	Machine_New_Install,  
	Machine_ModelTypeID,  
	IsAFTEnabled,   
	IsTITOEnabled,  
	IsNonCashVoucherEnabled,  
	ActAssetNo,  
	GMUNo,  
	ActSerialNo,  
	EnrolmentFlag,  
	CMPGameType,  
	Stacker_ID,
	Machine_Original_Purchase_Price,
	Base_Denom,
	Percentage_Payout,
	IsDefaultAssetDetail,
	GetGameDetails,
    IsGameCappingEnabled,
    AssetDisplayName 
	,Machine_Type_Of_Sale,
	Machine_Sold_To,
	Machine_Occupancy_Hour
	
 )  
 VALUES  
 (  
 @Machine_Class_ID,  
 @Operator_ID,  
 0,  
 @Depreciation_Policy_ID,  
 CASE WHEN (@Depreciation_Policy_Use_Default = 'True') THEN 1 ELSE 0 END,  
 @Stock_No,  
 0,  
 'Usable Stock',  
 @GetDate,  
 '',  
 @Machine_Purchase_Invoice_Number,  
 @Depot_ID,  
 @Machine_Manufacturers_Serial_No,  
 @Machine_Purchased_From,  
 @GetDate,  
 @Machine_Alternative_Serial_Numbers,  
 @StaffId,  
 @Machine_Memo,  
 @UserID,  
 @GetDate,  
 @Machine_Type_ID,  
 @Machine_MAC_Address,  
 CASE WHEN (@IsMultiGame = 'True') THEN 1 ELSE 0 END,  
 1,  
 @MT_ID,  
 CASE WHEN (@IsAFTEnabled = 'True') THEN 1 ELSE 0 END,  
 CASE WHEN (@IsTITOEnabled = 'True') THEN 1 ELSE 0 END,  
 CASE WHEN (@IsNonCashVoucherEnabled = 'True') THEN 1 ELSE 0 END,  
 @ActAssetNo,  
 @GMUNo,  
 @ActSerialNo,  
 0,  
@CMPGamePrefix,  
@StackerID,
0,
@Base_Denom,
@Percentage_Payout,
@IsDefaultAssetDetail,
@GetGameDetails,
@IsGameCappingEnabled,
@AssetDisplayName 
,@MachineTypeOfSale,
@MachineSoldTo,
@Machine_Class_Occupancy_Games_Per_Hour

 )  
 SET @Machine_ID = SCOPE_IDENTITY()  
  
 EXEC esp_InsertGameTypes @CMPGameType , @CMPGamePrefix  
  
 SET @AuditDesc = 'Record [' + @Stock_No + '] added to Purchase Machine'  
 
 EXEC usp_CreateAuditHistory @GetDate,@AuditUserID ,@UserName, 552,'Import/Export Asset File', 'Import Asset File',@AuditDesc,'','Stock No', '',@Stock_No,'ADD'  
 
END  

GO

