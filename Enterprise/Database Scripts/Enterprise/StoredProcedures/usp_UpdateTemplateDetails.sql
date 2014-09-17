USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateTemplateDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateTemplateDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_UpdateTemplateDetails
	@TemplateName VARCHAR(50),
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
	@AssetDisplayName VARCHAR(8)
	
AS
BEGIN
	
	SET DATEFORMAT dmy
	
     UPDATE AssetCreationTemplate  
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
            TemplateMachine_Class_ID = @Machine_Class_ID,  
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
            AssetDisplayName=@AssetDisplayName            
     WHERE  TemplateName = @TemplateName         
    
 
END  


GO

