
USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateSystemSettings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateSystemSettings]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE usp_UpdateSystemSettings(
    @BarPosition                      VARCHAR(50),
    @Company                          VARCHAR(50),
    @SubCompany                       VARCHAR(50),
    @Site                             VARCHAR(50),
    @Zone                             VARCHAR(50),
    @AutoGenerateModelCode            BIT,
    @ModelCodePrefix                  VARCHAR(10),
    @ModelCodeMinLength               INT,
    @AutoGenerateStockCode            BIT,
    @StockCodePrefix                  VARCHAR(10),
    @StockCodeMinLength               INT,
    @AllowStockBulkPurchase           BIT,
    @ForceSiteRepsOnStock             BIT,
    @ServiceHandheld                  BIT,
    @ServerName                       VARCHAR(50),
    @RegionCulture                    VARCHAR(50),
    @IsSiteLicensingEnabled           VARCHAR(10),
    @ImportExportAssetFile			  VARCHAR(10),
    @IsEnrolmentFlag				  VARCHAR(10),
    @IsEnrolmentComplete			  VARCHAR(10),
    @IsPowerPromoReportsRequired      VARCHAR(10),
    @CentralizedDeclaration           VARCHAR(10),
    @IsEmployeecardTrackingEnabled    VARCHAR(10),
    @AllowOfflineDeclaration          VARCHAR(10),
    @AddShortpayInVoucherOut          VARCHAR(10),
    @SystemSettingsDisplayTabVisible  VARCHAR(10),
    @SystemSettingsServiceTabVisible  VARCHAR(10),
    @ValidateAGSForGMU                VARCHAR(10),
    @VIEWPARTIALLYCONFIGUREDSITES	  VARCHAR(10),
    @IsGameCappingEnabled			  VARCHAR(10),
    @IsSuppressZoneEnabled			  VARCHAR(10),
    @IsSingleCardEmployee			  VARCHAR(10),
    @AllowEnableDisableBarPosition 	  VARCHAR(10),
    @IsAlertEnabled					  VARCHAR(10),
    @IsEmailAlertEnabled			  VARCHAR(10),
    @IsAutoCalendarEnabled			  VARCHAR(10),
    @SendMailFromEnterprise			  VARCHAR(10)='false',
    @CancelPendingMails			  VARCHAR(10),
    @AllowMultipleDrops			  VARCHAR(10)
)
AS
BEGIN
	SET NOCOUNT ON
	BEGIN TRANSACTION
	
	IF EXISTS (
	       SELECT TOP 1 1
	       FROM   System_Parameters
	   )
	BEGIN
	    UPDATE System_Parameters
	    SET    System_Parameter_Display_Bar = @BarPosition,
	           System_Parameter_Display_Company = @Company,
	           System_Parameter_Display_Site = @Site,
	           System_Parameter_Display_Sub = @SubCompany,
	           System_Parameter_Display_Zone = @Zone,
	           System_Parameter_Auto_Generate_Model_Codes = @AutoGenerateModelCode,
	           System_Parameter_Model_Code_Prefix = @ModelCodePrefix,
	           System_Parameter_Model_Code_Number_Length = @ModelCodeMinLength,
	           System_Parameter_Auto_Generate_Stock_Codes = @AutoGenerateStockCode,
	           System_Parameter_Stock_Code_Prefix = @StockCodePrefix,
	           System_Parameter_Stock_Code_Number_Length = @StockCodeMinLength,
	           System_Parameter_Stock_Allow_Bulk_Purchase = @AllowStockBulkPurchase,
	           System_Parameter_Force_Site_Reps_On_Stock = @ForceSiteRepsOnStock,
	           System_Parameter_Service_Handheld = @ServiceHandheld,
	           System_Parameter_Server_Name = @ServerName,
	           System_Parameter_Region_Culture = @RegionCulture
	END
	ELSE
	BEGIN
	    INSERT INTO System_Parameters
	      (
	        System_Parameter_Display_Bar,
	        System_Parameter_Display_Company,
	        System_Parameter_Display_Site,
	        System_Parameter_Display_Sub,
	        System_Parameter_Display_Zone,
	        System_Parameter_Auto_Generate_Model_Codes,
	        System_Parameter_Model_Code_Prefix,
	        System_Parameter_Model_Code_Number_Length,
	        System_Parameter_Auto_Generate_Stock_Codes,
	        System_Parameter_Stock_Code_Prefix,
	        System_Parameter_Stock_Code_Number_Length,
	        System_Parameter_Stock_Allow_Bulk_Purchase,
	        System_Parameter_Force_Site_Reps_On_Stock,
	        System_Parameter_Service_Handheld,
	        System_Parameter_Server_Name,
	        System_Parameter_Region_Culture
	      )
	    VALUES
	      (
	        @BarPosition,
	        @Company,
	        @Site,
	        @SubCompany,
	        @Zone,
	        @AutoGenerateModelCode,
	        @ModelCodePrefix,
	        @ModelCodeMinLength,
	        @AutoGenerateStockCode,
	        @StockCodePrefix,
	        @StockCodeMinLength,
	        @AllowStockBulkPurchase,
	        @ForceSiteRepsOnStock,
	        @ServiceHandheld,
	        @ServerName,
	        @RegionCulture
	      )
	END	       
	IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END
	EXEC usp_InsertOrUpdateSetting 'BMC_Reports_Language',
	     @RegionCulture
	
	IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END
	
	IF NOT EXISTS(
	       SELECT 1
	       FROM   Setting s
	       WHERE  s.Setting_Name = 'IsSiteLicensingEnabled'
	              AND s.Setting_Value = @IsSiteLicensingEnabled
	   )
	BEGIN
	    EXEC usp_SL_ExportLicensingEnabled @IsSiteLicensingEnabled
	    IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END
	END 
	
	EXEC usp_InsertOrUpdateSetting 'ImportExport_AssetFile', 
	     @ImportExportAssetFile
	IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END
	
	EXEC usp_InsertOrUpdateSetting 'IsEnrolmentFlag', 
	     @IsEnrolmentFlag
	IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END
	
	EXEC usp_InsertOrUpdateSetting 'IsEnrolmentComplete', 
	     @IsEnrolmentComplete
	IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END
	
	EXEC usp_InsertOrUpdateSetting 'IsPowerPromoReportsRequired',
	     @IsPowerPromoReportsRequired
	
	IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END
	EXEC usp_InsertOrUpdateSetting 'CentralizedDeclaration',
	     @CentralizedDeclaration
	IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END
	EXEC usp_InsertOrUpdateSetting 'IsEmployeecardTrackingEnabled',
	     @IsEmployeecardTrackingEnabled
	IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END
	EXEC usp_InsertOrUpdateSetting 'AllowOfflineDeclaration',
	     @AllowOfflineDeclaration
	IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END
	EXEC usp_InsertOrUpdateSetting 'AddShortpayInVoucherOut',
	     @AddShortpayInVoucherOut
	
	IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END
	EXEC usp_InsertOrUpdateSetting 'SystemSettingsDisplayTabVisible',
	     @SystemSettingsDisplayTabVisible
	IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END
	EXEC usp_InsertOrUpdateSetting 'SystemSettingsServiceTabVisible',
	     @SystemSettingsServiceTabVisible
	
	IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END
	
	EXEC usp_UpdateSettingProfileItem 'AddShortpayInVoucherOut',
	     @AddShortpayInVoucherOut
	
	IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END
	
	
	EXEC usp_InsertOrUpdateSetting 'ValidateAGSForGMU',
	     @ValidateAGSForGMU
	
	IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END
	
	EXEC usp_InsertOrUpdateSetting 'VIEWPARTIALLYCONFIGUREDSITES',
	     @VIEWPARTIALLYCONFIGUREDSITES
	     
	IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END
	
	EXEC usp_InsertOrUpdateSetting 'IsGameCappingEnabled',
	     @IsGameCappingEnabled
	
	IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END
	
	EXEC usp_InsertOrUpdateSetting 'IsSuppressZoneEnabled',
	     @IsSuppressZoneEnabled
	
	IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END
	
	EXEC usp_InsertOrUpdateSetting 'IsSingleCardEmployee',
	     @IsSingleCardEmployee
	
	IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END

	EXEC usp_InsertOrUpdateSetting 'AllowEnableDisableBarPosition',  
   	   @AllowEnableDisableBarPosition  
   
	IF @@ERROR <> 0  
	BEGIN  
	    ROLLBACK TRANSACTION  
	    RETURN  
	END
	
	EXEC usp_InsertOrUpdateSetting 'IsAlertEnabled',  
   	   @IsAlertEnabled  
   
	IF @@ERROR <> 0  
	BEGIN  
	    ROLLBACK TRANSACTION  
	    RETURN  
	END
	
	EXEC usp_InsertOrUpdateSetting 'IsEmailAlertEnabled',  
   	   @IsEmailAlertEnabled  
   
	IF @@ERROR <> 0  
	BEGIN  
	    ROLLBACK TRANSACTION  
	    RETURN  
	END
	
	EXEC usp_InsertOrUpdateSetting 'IsAutoCalendarEnabled',  
   	   @IsAutoCalendarEnabled  
   
	IF @@ERROR <> 0  
	BEGIN  
	    ROLLBACK TRANSACTION  
	    RETURN  
	END
	
	EXEC usp_InsertOrUpdateSetting 'SendMailFromEnterprise',  
   	   @SendMailFromEnterprise  
   
	IF @@ERROR <> 0  
	BEGIN  
	    ROLLBACK TRANSACTION  
	    RETURN  
	END
	
	EXEC usp_InsertOrUpdateSetting 'CancelPendingMails',  
   	   @CancelPendingMails  
   
	IF @@ERROR <> 0  
	BEGIN  
	    ROLLBACK TRANSACTION  
	    RETURN  
	END
	
	
	EXEC usp_InsertOrUpdateSetting 'AllowMultipleDrops',  
   	   @AllowMultipleDrops  
   
	IF @@ERROR <> 0  
	BEGIN  
	    ROLLBACK TRANSACTION  
	    RETURN  
	END
	
	INSERT INTO Export_History
	  (
	    -- EH_ID -- this column value is auto-generated,
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Site_Code
	  )
	SELECT GETDATE(),
	       Site_ID,
	       'SITESETTINGS',
	       Site_Code
	FROM   [SITE]
	WHERE  Site_Closed = 0
	
	IF @@ERROR <> 0
	BEGIN
	    ROLLBACK TRANSACTION
	    RETURN
	END
	
	COMMIT TRANSACTION
	
	
	SET NOCOUNT OFF
END
GO

