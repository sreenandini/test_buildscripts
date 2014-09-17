/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 03/05/2014 8:54:14 PM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_ModifyAsset]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_ModifyAsset]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_ModifyAsset(
    @Stock_No                                VARCHAR(50),
    @Machine_Name                            VARCHAR(50),
    @Machine_Type_Code                       VARCHAR(50),
    @ManufacturerId                          INT,
    @Validation_Length                       INT,
    @Machine_Class_Occupancy_Games_Per_Hour  INT,
    @Operator_ID                             INT,
    @Depot_ID                                INT,
    @Depreciation_Policy_Name                VARCHAR(50),
    @Depreciation_Policy_Use_Default         VARCHAR(1),
    @Machine_Purchase_Invoice_Number         VARCHAR(50),
    @Machine_Manufacturers_Serial_No         VARCHAR(50),
    @Machine_Alternative_Serial_Numbers      VARCHAR(50),
    @Machine_Purchased_From                  VARCHAR(50),
    @Machine_Memo                            VARCHAR(2000),
    @Machine_MAC_Address                     VARCHAR(50),
    @IsMultiGame                             VARCHAR(5),
    @MT_Model_Name                           VARCHAR(20),
    @IsAFTEnabled                            VARCHAR(5),
    @IsTITOEnabled                           VARCHAR(5),
    @IsNonCashVoucherEnabled                 VARCHAR(5),
    @ActAssetNo                              VARCHAR(50),
    @GMUNo                                   VARCHAR(50),
    @ActSerialNo                             VARCHAR(50),
    @CMPGamePrefix                           VARCHAR(5),
    @CMPGameType                             VARCHAR(50),
    @StackerID                               INT,
    @Base_Denom                              INT,
    @Percentage_Payout                       REAL,
    @IsDefaultAssetDetail                    BIT,
    @GetGameDetails                          BIT,
    @IsGameCappingEnabled                    BIT,
    @AssetDisplayName                        VARCHAR(8),
    @MachineTypeOfSale                       VARCHAR(50),
    @MachineSoldTo                           VARCHAR(50),
    @UserID                                  INT,
    @UserName                                VARCHAR(50),
    @StaffRepresentative                     VARCHAR(50)
)
AS
BEGIN
	DECLARE @Machine_ID               INT  
	DECLARE @Machine_Class_ID         INT  
	DECLARE @Machine_Type_ID          INT 
	--DECLARE @Manufacturer_ID INT  
	DECLARE @Depreciation_Policy_ID   INT  
	DECLARE @MT_ID                    INT 
	--DECLARE @Stacker_Id INT  
	DECLARE @Machine_status_flag      INT
	DECLARE @GetDate                  DATETIME  
	DECLARE @AuditDesc                VARCHAR(500)  
	DECLARE @AuditId                  VARCHAR(500)  
	DECLARE @AutoGenerateStockNumber  BIT
	DECLARE @StaffId                  INT 
	DECLARE @AuditUserID              INT
	
	SET @GetDate = GETDATE()  
	SET @Machine_Name = RTRIM(LTRIM(@Machine_Name))  
	SET @Machine_Type_Code = RTRIM(LTRIM(@Machine_Type_Code)) 
	--SET @Manufacturer_Name = RTRIM(LTRIM(@Manufacturer_Name))  
	SET @Stock_No = RTRIM(LTRIM(@Stock_No))  
	SET @MT_Model_Name = RTRIM(LTRIM(@MT_Model_Name)) 
	--SET @Stacker_Name = RTRIM(LTRIM(@Stacker_Name))  
	SET @StaffId = (
	        SELECT TOP 1 Staff_ID
	        FROM   Staff
	        WHERE  Staff_First_Name + ',' + Staff_Last_Name = @StaffRepresentative
	    )
	
	SELECT @AuditUserID = @UserID
	
	
	
	IF @IsDefaultAssetDetail = 'False'
	BEGIN
	    SET @Base_Denom = 1
	    SET @Percentage_Payout = 0
	END
	
	
	
	--SELECT @Manufacturer_ID =  ISNULL(Manufacturer_ID,0) FROM Manufacturer WITH(NOLOCK) WHERE RTRIM(LTRIM(Manufacturer_Name)) = @Manufacturer_Name  
	
	SELECT @Machine_Type_ID = ISNULL(Machine_Type_ID, 0)
	FROM   Machine_Type WITH(NOLOCK)
	WHERE  RTRIM(LTRIM(Machine_Type_Code)) = @Machine_Type_Code
	       AND IsNonGamingAssetType = 0
	
	SELECT @Machine_Class_ID = ISNULL(Machine_Class_ID, 0)
	FROM   Machine_Class WITH(NOLOCK)
	WHERE  RTRIM(LTRIM(Machine_Name)) = @Machine_Name
	       AND Machine_Type_ID = @Machine_Type_ID
	       AND Manufacturer_ID = @ManufacturerId 
	
	
	SELECT @Depreciation_Policy_ID = ISNULL(Depreciation_Policy_ID, 0)
	FROM   Depreciation_Policy WITH(NOLOCK)
	WHERE  RTRIM(LTRIM(Depreciation_Policy_Description)) = @Depreciation_Policy_Name  
	
	SELECT @MT_ID = ISNULL(MT_ID, 0)
	FROM   Model_Type WITH(NOLOCK)
	WHERE  RTRIM(LTRIM(MT_Model_Name)) = @MT_Model_Name 
	
	--SELECT @Stacker_Id = ISNULL(Stacker_Id,0) FROM Stacker WITH(NOLOCK) WHERE RTRIM(LTRIM(StackerName)) = @Stacker_Name  
	
	SELECT @Machine_ID = ISNULL(Machine_ID, 0),
	       @Machine_status_flag = Machine_status_flag
	FROM   MACHINE WITH(NOLOCK)
	WHERE  Machine_Stock_No = @Stock_No
	
	SELECT @AutoGenerateStockNumber = System_Parameter_Auto_Generate_Stock_Codes
	FROM   System_Parameters
	
	
	
	
	
	IF ISNULL(@Machine_Class_ID, 0) = 0
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
	        --Machine_Class_Occupancy_Games_Per_Hour,
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
	    SELECT @Machine_Name,
	           @Machine_Type_ID,
	           @Machine_Type_ID,
	           1,
	           @Machine_Name,
	           0,
	           1,
	           --@Machine_Class_Occupancy_Games_Per_Hour,
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
	END
	ELSE
	BEGIN
	    UPDATE Machine_class
	    SET    --Machine_Class_Occupancy_Games_Per_Hour = @Machine_Class_Occupancy_Games_Per_Hour,
	           Validation_Length = @Validation_Length
	    WHERE  machine_class_id = @Machine_Class_ID
	END 
	
	INSERT INTO dbo.Export_History
	  (
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Site_Code
	  )
	SELECT @GetDate,
	       @Machine_Class_ID,
	       'MODEL',
	       Site_Code
	FROM   dbo.Site WITH(NOLOCK)  
	
	
	
	
	IF (@Machine_status_flag <> 13 AND @Machine_status_flag <> 6)
	BEGIN
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
	
	    IF (@Machine_status_flag = 1)
	    BEGIN
	        UPDATE [Machine]
	        SET    @Machine_ID = Machine_ID,
	               Machine_Class_ID = @Machine_Class_ID,
	               --Operator_ID =  @Operator_ID,
	               Terms_Profile_ID = 0,
	               Depreciation_Policy_ID = @Depreciation_Policy_ID,
	               Depreciation_Policy_Use_Default = CASE 
	                                                      WHEN (@Depreciation_Policy_Use_Default = 'True') THEN 
	                                                           1
	                                                      ELSE 0
	                                                 END,
	               Machine_Purchase_Invoice_Number = @Machine_Purchase_Invoice_Number,
	               --Depot_ID = @Depot_ID,	
	               Machine_Purchased_From = @Machine_Purchased_From,
	               Machine_Alternative_Serial_Numbers = @Machine_Alternative_Serial_Numbers,
	               Machine_Memo = @Machine_Memo,
	               Staff_ID_Entered = @UserID,
	               Machine_MAC_Address = @Machine_MAC_Address,
	               --IsMultiGame = CASE WHEN (@IsMultiGame = 'Y') THEN 1 ELSE 0 END,
	               IsAFTEnabled = CASE 
	                                   WHEN (@IsAFTEnabled = 'True') THEN 1
	                                   ELSE 0
	                              END,
	               IsTITOEnabled = CASE 
	                                    WHEN (@IsTITOEnabled = 'True') THEN 1
	                                    ELSE 0
	                               END,
	               IsNonCashVoucherEnabled = CASE 
	                                              WHEN (@IsNonCashVoucherEnabled = 'True') THEN 
	                                                   1
	                                              ELSE 0
	                                         END,
	               CMPGameType = @CMPGamePrefix,
	               Machine_Date_Entered = @GetDate,
	               IsGameCappingEnabled = @IsGameCappingEnabled,
	               AssetDisplayName = @AssetDisplayName,
	               Machine_Occupancy_Hour= @Machine_Class_Occupancy_Games_Per_Hour
	               --Stacker_ID  = @Stacker_ID
	        OUTPUT INSERTED.Machine_ID,
				DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
				DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
				DELETED.CMPGameType, INSERTED.CMPGameType, 
				DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
			INTO @_Modified
	        WHERE  Machine_ID = @Machine_ID
	    END
	    ELSE
	    BEGIN
	        UPDATE [Machine]
	        SET    Operator_ID = @Operator_ID,
	               Depot_ID = @Depot_ID,
	               Machine_Purchased_From = @Machine_Purchased_From,
	               Machine_Class_ID = @Machine_Class_ID,
	               --Setting based
	               Machine_Stock_No = @Stock_No,
	               --CASE WHEN (@AutoGenerateStockNumber = 1) THEN @Stock_No ELSE 
	               ActAssetNo = @ActAssetNo,
	               ActSerialNo = @ActSerialNo,
	               Machine_Manufacturers_Serial_No = @ActSerialNo,
	               GMUNo = @GMUNo,
	               Machine_Alternative_Serial_Numbers = @Machine_Alternative_Serial_Numbers,
	               Machine_MAC_Address = @Machine_MAC_Address,
	               Machine_ModelTypeID = @MT_ID,
	               IsMultiGame = CASE 
	                                  WHEN (@IsMultiGame = 'True') THEN 1
	                                  ELSE 0
	                             END,
	               IsAFTEnabled = CASE 
	                                   WHEN (@IsAFTEnabled = 'True') THEN 1
	                                   ELSE 0
	                              END,
	               IsTITOEnabled = CASE 
	                                    WHEN (@IsTITOEnabled = 'True') THEN 1
	                                    ELSE 0
	                               END,
	               IsNonCashVoucherEnabled = CASE 
	                                              WHEN (@IsNonCashVoucherEnabled = 'True') THEN 
	                                                   1
	                                              ELSE 0
	                                         END,
	               CMPGameType = @CMPGamePrefix,
	               Stacker_ID = @StackerID,
	               IsDefaultAssetDetail = @IsDefaultAssetDetail,
	               Base_Denom = @Base_Denom,
	               Percentage_Payout = @Percentage_Payout,
	               Machine_Memo = @Machine_Memo,
	               Machine_Date_Entered = @GetDate,
	               IsGameCappingEnabled = @IsGameCappingEnabled,
	               AssetDisplayName = @AssetDisplayName,
	               Machine_Occupancy_Hour= @Machine_Class_Occupancy_Games_Per_Hour,
	               GetGameDetails = @GetGameDetails,
	               Staff_ID = @StaffId
	        OUTPUT INSERTED.Machine_ID,
				DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
				DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
				DELETED.CMPGameType, INSERTED.CMPGameType, 
				DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
			INTO @_Modified
	        WHERE  Machine_ID = @Machine_ID
	    END

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
	END
	
	INSERT INTO Export_History
	  (
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Site_Code
	  )
	SELECT GETDATE(),
	       @Machine_ID,
	       'MACHINEUPDATE',
	       Site_Code
	FROM   SITE
	
	INSERT INTO Export_History
	  (
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Site_Code
	  )
	SELECT GETDATE(),
	       Installation_ID,
	       'AFTENABLEDISABLE',
	       s.Site_Code
	FROM   Installation
	       JOIN Bar_Position
	            ON  Installation.Bar_Position_ID = Bar_Position.Bar_Position_ID
	       JOIN MACHINE
	            ON  Installation.Machine_ID = MACHINE.Machine_ID
	       LEFT OUTER JOIN [Site] s
	            ON  S.Site_ID = Bar_Position.Site_ID 
	                --      INNER JOIN [USER] U
	                --ON U.SecurityUserID=@Usertableid
	WHERE  Bar_Position.Bar_Position_End_Date IS NULL
	       AND Installation.Installation_End_Date IS NULL
	
	
	EXEC esp_InsertGameTypes @CMPGameType,
	     @CMPGamePrefix 
	
	
	SET @AuditDesc = 'Record [' + @Stock_No + '] updated to Purchase Machine'   
	EXEC usp_CreateAuditHistory @GetDate,
	     @AuditUserID,
	     @UserName,
	     552,
	     'Import/Export Asset File',
	     'Import Asset File',
	     @AuditDesc,
	     '',
	     'Stock No',
	     '',
	     @Stock_No,
	     'MODIFY'
END
GO



