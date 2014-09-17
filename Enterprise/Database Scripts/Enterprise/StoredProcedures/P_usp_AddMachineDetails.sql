USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_AddMachineDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_AddMachineDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*  
 * Revision History  
 *   
 * <Name of the owner>         DateCreated       Type(Created/Modified)  Description  
   Kalaiyarasan.P              27-SEP-2012         Created               This SP is used to Add/Modify Machine details   
                                                                         based on Machine_ID. 
Exec  usp_AddMachineDetails  111
*/  
CREATE PROCEDURE usp_AddMachineDetails
	@Machine_ID INT,
	@ActAssetNo VARCHAR(50),
	@ActSerialNo VARCHAR(50),
	@CMPGameType CHAR(1),
	@GameType VARCHAR(50),
	@Depot_ID INT,
	@Depreciation_Policy_ID INT,
	@Depreciation_Policy_Use_Default BIT,
	@EnrolmentFlag INT,
	@GMUNo VARCHAR(50),
	@IsAFTEnabled BIT,
	@IsMultiGame BIT,
	@IsNonCashVoucherEnabled INT,
	@IsTITOEnabled INT,	
	@Machine_Alternative_Serial_Numbers VARCHAR(50),
	@Machine_Category_ID INT,
	@Machine_Class_ID INT,
	@Machine_Date_Entered VARCHAR(30),
	@Machine_Depreciation_Start_Date VARCHAR(30),
	@Machine_End_Date VARCHAR(30),
	@Machine_MAC_Address VARCHAR(17),
	@Machine_MAC_Address_Prev VARCHAR(17),
	@Machine_Manufacturers_Serial_No VARCHAR(50),
	@Machine_Memo NTEXT,
	@Machine_ModelTypeID INT,
	@Machine_New_Install INT,
	@Machine_Original_Purchase_Price MONEY,
	@Machine_Purchase_Invoice_Number VARCHAR(50),
	@Machine_Purchased_From VARCHAR(50),
	@Machine_Start_Date VARCHAR(30),
	@Machine_Status VARCHAR(50),
	@Machine_Status_Flag INT,
	@Machine_Stock_No VARCHAR(50),
    @IsDefaultAssetDetail BIT,
  	@Base_Denom INT,
 	@Percentage_Payout REAL,
	@Operator_ID INT,
	@Stacker_Id INT,
	@Staff_ID INT,
	@Staff_ID_Entered INT,
	@Terms_Profile_ID INT,
	@GetGameDetails BIT,
	@IsGameCappingEnabled BIT,
	@AssetDisplayName VARCHAR(8),
	@OccupancyHour INT
	
AS
BEGIN
	SET DATEFORMAT dmy
	
	IF NOT EXISTS(
	       SELECT 1
	       FROM   MACHINE
	       WHERE  Machine_ID = @Machine_ID
	   )
	BEGIN
	    INSERT INTO MACHINE
	      (
	        ActAssetNo,
	        ActSerialNo,
	        CMPGameType,
	        Depot_ID,
	        Depreciation_Policy_ID,
	        Depreciation_Policy_Use_Default,
	        EnrolmentFlag,
	        GMUNo,
	        IsAFTEnabled,
	        IsMultiGame,
	        IsNonCashVoucherEnabled,
	        IsTITOEnabled,
	        Machine_Alternative_Serial_Numbers,
	        Machine_Category_ID,
	        Machine_Class_ID,
	        Machine_Date_Entered,
	        Machine_Depreciation_Start_Date,
	        Machine_End_Date,
	        Machine_MAC_Address,
	        Machine_MAC_Address_Prev,
	        Machine_Manufacturers_Serial_No,
	        Machine_Memo,
	        Machine_ModelTypeID,
	        Machine_New_Install,
	        Machine_Original_Purchase_Price,
	        Machine_Purchase_Invoice_Number,
	        Machine_Purchased_From,
	        Machine_Start_Date,
	        Machine_Status,
	        Machine_Status_Flag,
	        Machine_Stock_No,
		    IsDefaultAssetDetail,
         	Base_Denom,
         	Percentage_Payout,   
	        Operator_ID,
	        Stacker_Id,
	        Staff_ID,
	        Staff_ID_Entered,
	        Terms_Profile_ID,
	        GetGameDetails,
	        IsGameCappingEnabled,
	        AssetDisplayName,
	        Machine_Occupancy_Hour
	      )
	    VALUES
	      (
	        @ActAssetNo,
	        @ActSerialNo,
	        @CMPGameType,
	        @Depot_ID,
	        @Depreciation_Policy_ID,
	        @Depreciation_Policy_Use_Default,
	        @EnrolmentFlag,
	        @GMUNo,
	        @IsAFTEnabled,
	        @IsMultiGame,
	        @IsNonCashVoucherEnabled,
	        @IsTITOEnabled,
	        @Machine_Alternative_Serial_Numbers,
	        @Machine_Category_ID,
	        @Machine_Class_ID,
	        @Machine_Date_Entered,
	        @Machine_Depreciation_Start_Date,
	        @Machine_End_Date,
	        @Machine_MAC_Address,
	        @Machine_MAC_Address_Prev,
	        @Machine_Manufacturers_Serial_No,
	        @Machine_Memo,
	        @Machine_ModelTypeID,
	        @Machine_New_Install,
	        @Machine_Original_Purchase_Price,
	        @Machine_Purchase_Invoice_Number,
	        @Machine_Purchased_From,
	        @Machine_Start_Date,
	        @Machine_Status,
	        @Machine_Status_Flag,
	        @Machine_Stock_No,
			@IsDefaultAssetDetail,
			@Base_Denom,
			@Percentage_Payout,  
			@Operator_ID,  
			@Stacker_Id,  
			@Staff_ID,  
			@Staff_ID_Entered,  
			@Terms_Profile_ID,
			@GetGameDetails ,
			@IsGameCappingEnabled,
			@AssetDisplayName,
			@OccupancyHour			
       )   
       
          INSERT INTO MeterAnalysis.dbo.MACHINE
	      (
	        ActAssetNo,
	        ActSerialNo,
	        CMPGameType,
	        Depot_ID,
	        Depreciation_Policy_ID,
	        Depreciation_Policy_Use_Default,
	        EnrolmentFlag,
	        GMUNo,
	        IsAFTEnabled,
	        IsMultiGame,
	        IsNonCashVoucherEnabled,
	        IsTITOEnabled,
	        Machine_Alternative_Serial_Numbers,
	        Machine_Category_ID,
	        Machine_Class_ID,
	        Machine_Date_Entered,
	        Machine_Depreciation_Start_Date,
	        Machine_End_Date,
	        Machine_MAC_Address,
	        Machine_MAC_Address_Prev,
	        Machine_Manufacturers_Serial_No,
	        Machine_Memo,
	        Machine_ModelTypeID,
	        Machine_New_Install,
	        Machine_Original_Purchase_Price,
	        Machine_Purchase_Invoice_Number,
	        Machine_Purchased_From,
	        Machine_Start_Date,
	        Machine_Status,
	        Machine_Status_Flag,
	        Machine_Stock_No,
		    IsDefaultAssetDetail,
         	Base_Denom,
         	Percentage_Payout,   
	        Operator_ID,
	        Stacker_Id,
	        Staff_ID,
	        Staff_ID_Entered,
	        Terms_Profile_ID,
	        GetGameDetails,
	        IsGameCappingEnabled,
	        AssetDisplayName,
	        Machine_Occupancy_Hour
	      )
	    VALUES
	      (
	        @ActAssetNo,
	        @ActSerialNo,
	        @CMPGameType,
	        @Depot_ID,
	        @Depreciation_Policy_ID,
	        @Depreciation_Policy_Use_Default,
	        @EnrolmentFlag,
	        @GMUNo,
	        @IsAFTEnabled,
	        @IsMultiGame,
	        @IsNonCashVoucherEnabled,
	        @IsTITOEnabled,
	        @Machine_Alternative_Serial_Numbers,
	        @Machine_Category_ID,
	        @Machine_Class_ID,
	        @Machine_Date_Entered,
	        @Machine_Depreciation_Start_Date,
	        @Machine_End_Date,
	        @Machine_MAC_Address,
	        @Machine_MAC_Address_Prev,
	        @Machine_Manufacturers_Serial_No,
	        @Machine_Memo,
	        @Machine_ModelTypeID,
	        @Machine_New_Install,
	        @Machine_Original_Purchase_Price,
	        @Machine_Purchase_Invoice_Number,
	        @Machine_Purchased_From,
	        @Machine_Start_Date,
	        @Machine_Status,
	        @Machine_Status_Flag,
	        @Machine_Stock_No,
			@IsDefaultAssetDetail,
			@Base_Denom,
			@Percentage_Payout,  
			@Operator_ID,  
			@Stacker_Id,  
			@Staff_ID,  
			@Staff_ID_Entered,  
			@Terms_Profile_ID,
			@GetGameDetails ,
			@IsGameCappingEnabled,
			@AssetDisplayName,
			@OccupancyHour			
       ) 
     SELECT @Machine_ID = SCOPE_IDENTITY()     
     EXEC [dbo].[usp_EBS_UpdateMachineDetails] @Machine_ID 
 END  
 ELSE  
 BEGIN  
/*****************************************************************************************************
* (CR# 191254 : EBS Communication Service) - MODIFIED BY Venkatesh.H  (START)
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
     SET    ActAssetNo = @ActAssetNo,  
            ActSerialNo = @ActSerialNo,  
            CMPGameType = @CMPGameType,  
            Depot_ID = @Depot_ID,  
            Depreciation_Policy_ID = @Depreciation_Policy_ID,  
            Depreciation_Policy_Use_Default = @Depreciation_Policy_Use_Default,  
            EnrolmentFlag = @EnrolmentFlag,  
            GMUNo = @GMUNo,  
            IsMultiGame= @IsMultiGame,  
            IsAFTEnabled = @IsAFTEnabled,  
            IsNonCashVoucherEnabled = @IsNonCashVoucherEnabled,  
            Machine_Alternative_Serial_Numbers = @Machine_Alternative_Serial_Numbers,  
            Machine_Category_ID = @Machine_Category_ID,  
            Machine_Class_ID = @Machine_Class_ID,  
            Machine_Date_Entered = @Machine_Date_Entered,  
            Machine_Depreciation_Start_Date = @Machine_Depreciation_Start_Date,  
            Machine_End_Date = @Machine_End_Date,  
            Machine_MAC_Address = ISNULL(@Machine_MAC_Address, Machine_MAC_Address),  
            Machine_MAC_Address_Prev = ISNULL(@Machine_MAC_Address_Prev, Machine_MAC_Address_Prev),  
            Machine_Manufacturers_Serial_No = @Machine_Manufacturers_Serial_No,  
            Machine_Memo = @Machine_Memo,  
            Machine_ModelTypeID = @Machine_ModelTypeID,  
            Machine_Original_Purchase_Price = @Machine_Original_Purchase_Price,  
            Machine_Purchase_Invoice_Number = @Machine_Purchase_Invoice_Number,  
            Machine_Purchased_From = @Machine_Purchased_From,  
            Machine_Start_Date = CASE   
                                      WHEN Machine_Previous_Machine_ID = 0 THEN   
                                           @Machine_Start_Date  
                                      ELSE Machine_Start_Date  
                                 END,  
            Machine_Status = @Machine_Status,  
            Machine_Status_Flag = @Machine_Status_Flag,  
            Machine_Stock_No = @Machine_Stock_No,
            IsDefaultAssetDetail = @IsDefaultAssetDetail,
            Base_Denom = @Base_Denom,
			Percentage_Payout = @Percentage_Payout,
            Operator_ID = @Operator_ID,  
            Stacker_Id = @Stacker_Id,  
            Staff_ID = @Staff_ID,  
            Terms_Profile_ID = @Terms_Profile_ID,  
            IsTITOEnabled = @IsTITOEnabled,  
            GetGameDetails= @GetGameDetails, 
            Staff_ID_Entered = CASE   
                                    WHEN Staff_ID_Entered = 0 THEN @Staff_ID_Entered  
                                    ELSE Staff_ID_Entered  
                               END  ,
            IsGameCappingEnabled=@IsGameCappingEnabled,
            AssetDisplayName=@AssetDisplayName ,
            Machine_Occupancy_Hour = @OccupancyHour  
            OUTPUT INSERTED.Machine_ID,
                                DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
                                DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
                                DELETED.CMPGameType, INSERTED.CMPGameType, 
                                DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
            INTO @_Modified
        
     WHERE  Machine_ID = @Machine_ID  
     
     UPDATE MeterAnalysis.dbo.MACHINE  
     SET    ActAssetNo = @ActAssetNo,  
            ActSerialNo = @ActSerialNo,  
            CMPGameType = @CMPGameType,  
            Depot_ID = @Depot_ID,  
            Depreciation_Policy_ID = @Depreciation_Policy_ID,  
            Depreciation_Policy_Use_Default = @Depreciation_Policy_Use_Default,  
            EnrolmentFlag = @EnrolmentFlag,  
            GMUNo = @GMUNo,  
            IsMultiGame= @IsMultiGame,  
            IsAFTEnabled = @IsAFTEnabled,  
            IsNonCashVoucherEnabled = @IsNonCashVoucherEnabled,  
            Machine_Alternative_Serial_Numbers = @Machine_Alternative_Serial_Numbers,  
            Machine_Category_ID = @Machine_Category_ID,  
            Machine_Class_ID = @Machine_Class_ID,  
            Machine_Date_Entered = @Machine_Date_Entered,  
            Machine_Depreciation_Start_Date = @Machine_Depreciation_Start_Date,  
            Machine_End_Date = @Machine_End_Date,  
            Machine_MAC_Address = ISNULL(@Machine_MAC_Address, Machine_MAC_Address),  
            Machine_MAC_Address_Prev = ISNULL(@Machine_MAC_Address_Prev, Machine_MAC_Address_Prev),  
            Machine_Manufacturers_Serial_No = @Machine_Manufacturers_Serial_No,  
            Machine_Memo = @Machine_Memo,  
            Machine_ModelTypeID = @Machine_ModelTypeID,  
            Machine_Original_Purchase_Price = @Machine_Original_Purchase_Price,  
            Machine_Purchase_Invoice_Number = @Machine_Purchase_Invoice_Number,  
            Machine_Purchased_From = @Machine_Purchased_From,  
            Machine_Start_Date = CASE   
                                      WHEN Machine_Previous_Machine_ID = 0 THEN   
                                           @Machine_Start_Date  
                                      ELSE Machine_Start_Date  
                                 END,  
            Machine_Status = @Machine_Status,  
            Machine_Status_Flag = @Machine_Status_Flag,  
            Machine_Stock_No = @Machine_Stock_No,
            IsDefaultAssetDetail = @IsDefaultAssetDetail,
            Base_Denom = @Base_Denom,
			Percentage_Payout = @Percentage_Payout,
            Operator_ID = @Operator_ID,  
            Stacker_Id = @Stacker_Id,  
            Staff_ID = @Staff_ID,  
            Terms_Profile_ID = @Terms_Profile_ID,  
            IsTITOEnabled = @IsTITOEnabled,  
            GetGameDetails= @GetGameDetails, 
            Staff_ID_Entered = CASE   
                                    WHEN Staff_ID_Entered = 0 THEN @Staff_ID_Entered  
                                    ELSE Staff_ID_Entered  
                               END  ,
            IsGameCappingEnabled=@IsGameCappingEnabled,
            AssetDisplayName=@AssetDisplayName ,
            Machine_Occupancy_Hour = @OccupancyHour  
            OUTPUT INSERTED.Machine_ID,
                                DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
                                DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
                                DELETED.CMPGameType, INSERTED.CMPGameType, 
                                DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
            INTO @_Modified
        
     WHERE  Machine_ID = @Machine_ID  
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
* (CR# 191254 : EBS Communication Service) - MODIFIED BY Venkatesh.H  (END)
*****************************************************************************************************/
    
 END      
 IF @Machine_ID > 0  
 BEGIN  
     EXEC esp_InsertGameTypes  @GameType,@CMPGameType  
 END  
END  


GO

