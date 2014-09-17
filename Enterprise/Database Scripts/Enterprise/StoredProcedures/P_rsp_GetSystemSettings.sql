USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSystemSettings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSystemSettings]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--Exec rsp_GetSystemSettings
CREATE PROCEDURE rsp_GetSystemSettings
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Setting_Value VARCHAR(100)
	EXEC [rsp_GetSetting] 0,
	     'IsSiteLicensingEnabled',
	     'false',
	     @Setting_Value OUTPUT
	
	DECLARE @IsSiteLicensingEnabled BIT
	SET @IsSiteLicensingEnabled = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
	                                   WHEN 'TRUE' THEN 1
	                                   ELSE 0
	                              END
	                              
	 EXEC [rsp_GetSetting] 0,
	     'ImportExport_AssetFile',
	     'false',
	     @Setting_Value OUTPUT
	
	DECLARE @ImportExportAssetFile BIT
	SET @ImportExportAssetFile = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
	                                   WHEN 'TRUE' THEN 1
	                                   ELSE 0
	                              END
	
	EXEC [rsp_GetSetting] 0,
	     'IsEnrolmentFlag',
	     'false',
	     @Setting_Value OUTPUT
	
	DECLARE @IsEnrolmentFlag BIT
	SET @IsEnrolmentFlag = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
	                                   WHEN 'TRUE' THEN 1
	                                   ELSE 0
	                              END

   	EXEC [rsp_GetSetting] 0,
	     'IsEnrolmentComplete',
	     'false',
	     @Setting_Value OUTPUT
	
	DECLARE @IsEnrolmentComplete BIT
	SET @IsEnrolmentComplete = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
	                                   WHEN 'TRUE' THEN 1
	                                   ELSE 0
	                              END	                              
	                              
	EXEC [rsp_GetSetting] 0,
	     'IsPowerPromoReportsRequired',
	     'false',
	     @Setting_Value OUTPUT
	
	DECLARE @IsPowerPromoReportsRequired BIT
	SET @IsPowerPromoReportsRequired = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
	                                        WHEN 'TRUE' THEN 1
	                                        ELSE 0
	                                   END
	
	EXEC [rsp_GetSetting] 0,
	     'CentralizedDeclaration',
	     'false',
	     @Setting_Value OUTPUT
	
	DECLARE @CentralizedDeclaration BIT
	SET @CentralizedDeclaration = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
	                                   WHEN 'TRUE' THEN 1
	                                   ELSE 0
	                              END
	
	EXEC [rsp_GetSetting] 0,
	     'IsEmployeecardTrackingEnabled',
	     'false',
	     @Setting_Value OUTPUT
	
	DECLARE @IsEmployeecardTrackingEnabled BIT
	SET @IsEmployeecardTrackingEnabled = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
	                                          WHEN 'TRUE' THEN 1
	                                          ELSE 0
	                                     END
	
	EXEC [rsp_GetSetting] 0,
	     'AllowOfflineDeclaration',
	     'false',
	     @Setting_Value OUTPUT
	
	DECLARE @AllowOfflineDeclaration BIT
	SET @AllowOfflineDeclaration = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
	                                    WHEN 'TRUE' THEN 1
	                                    ELSE 0
	                               END
	
	EXEC [rsp_GetSetting] 0,
	     'AddShortpayInVoucherOut',
	     'false',
	     @Setting_Value OUTPUT
	
	DECLARE @AddShortpayInVoucherOut BIT
	SET @AddShortpayInVoucherOut = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
	                                    WHEN 'TRUE' THEN 1
	                                    ELSE 0
	                               END
	
	EXEC [rsp_GetSetting] 0,
	     'SystemSettingsDisplayTabVisible',
	     'false',
	     @Setting_Value OUTPUT
	
	DECLARE @SystemSettingsDisplayTabVisible BIT
	SET @SystemSettingsDisplayTabVisible = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
	                                            WHEN 'TRUE' THEN 1
	                                            ELSE 0
	                                       END
	
	
	EXEC [rsp_GetSetting] 0,
	     'SystemSettingsServiceTabVisible',
	     'false',
	     @Setting_Value OUTPUT
	
	DECLARE @SystemSettingsServiceTabVisible BIT
	SET @SystemSettingsServiceTabVisible = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
	                                            WHEN 'TRUE' THEN 1
	                                            ELSE 0
	                                       END
	
	 EXEC [rsp_GetSetting] 0,
	     'ValidateAGSForGMU',
	     'false',
	     @Setting_Value OUTPUT
	
	DECLARE @ValidateAGSForGMU BIT
	SET @ValidateAGSForGMU = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
	                                    WHEN 'TRUE' THEN 1
	                                    ELSE 0
	                               END
    	EXEC [rsp_GetSetting] 0,
    	     'VIEWPARTIALLYCONFIGUREDSITES',
    	     'false',
    	     @Setting_Value OUTPUT
    	
    	DECLARE @VIEWPARTIALLYCONFIGUREDSITES BIT
    	SET @VIEWPARTIALLYCONFIGUREDSITES = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
    	                              WHEN 'TRUE' THEN 1
    	                              ELSE 0
    	                         END  
    	
    	EXEC [rsp_GetSetting] 0,
    	     'IsGameCappingEnabled',
    	     'false',
    	     @Setting_Value OUTPUT
        DECLARE @IsGameCappingEnabled BIT
    	SET @IsGameCappingEnabled = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
    	                              WHEN 'TRUE' THEN 1
    	                              ELSE 0
    	                         END    	
	EXEC [rsp_GetSetting] 0,
    	     'IsSuppressZoneEnabled',
    	     'false',
    	     @Setting_Value OUTPUT
    	
    	DECLARE @IsSuppressZoneEnabled BIT
    	SET @IsSuppressZoneEnabled = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
    	                              WHEN 'TRUE' THEN 1
    	                              ELSE 0
    	                         END  
    	                         
	EXEC [rsp_GetSetting] 0,  
      'IsSingleCardEmployee',  
      'false',  
      @Setting_Value OUTPUT  
   
	DECLARE @IsSingleCardEmployee BIT  
	SET @IsSingleCardEmployee = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))  
									WHEN 'TRUE'
									THEN 1
									ELSE 0  
								END

	 EXEC [rsp_GetSetting] 0,    
	'AllowEnableDisableBarPosition',    
	'TRUE',    
	 @Setting_Value OUTPUT   

	 DECLARE @AllowEnableDisableBarPosition BIT    
	 SET @AllowEnableDisableBarPosition = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))    
         WHEN 'TRUE'  
	         THEN 1  
        	 ELSE 0    
         END  
         
	 EXEC [rsp_GetSetting] 0,
	      'IsAlertEnabled',
	      'TRUE',
	      @Setting_Value OUTPUT   
	 
	 DECLARE @IsAlertEnabled BIT    
	 SET @IsAlertEnabled = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
	                            WHEN 'TRUE' THEN 1
	                            ELSE 0
	                       END  
         
         EXEC [rsp_GetSetting] 0,
              'IsEmailAlertEnabled',
              'TRUE',
              @Setting_Value OUTPUT   
         
         DECLARE @IsEmailAlertEnabled BIT    
         SET @IsEmailAlertEnabled = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
                                         WHEN 'TRUE' THEN 1
                                         ELSE 0
                                    END
                                    
          EXEC [rsp_GetSetting] 0,
              'IsAutoCalendarEnabled',
              'TRUE',
              @Setting_Value OUTPUT   
         
         DECLARE @IsAutoCalendarEnabled BIT    
         SET @IsAutoCalendarEnabled = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
                                         WHEN 'TRUE' THEN 1
                                         ELSE 0
                                      END
                                      
          EXEC [rsp_GetSetting] 0,
              'SendMailFromEnterprise',
              'TRUE',
              @Setting_Value OUTPUT   
         
       
         DECLARE @SendMailFromEnterprise BIT    
         SET @SendMailFromEnterprise = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
                                         WHEN 'TRUE' THEN 1
                                         ELSE 0
                                       END
                                       
			EXEC [rsp_GetSetting] 0,
              'CancelPendingMails',
              'TRUE',
              @Setting_Value OUTPUT   
                     
		DECLARE @CancelPendingMails BIT    
         SET @CancelPendingMails = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
                                         WHEN 'TRUE' THEN 1
                                         ELSE 0
                                       END
                                       
		
                            
                                        
          EXEC [rsp_GetSetting] 0,
              'AllowMultipleDrops',
              'TRUE',
              @Setting_Value OUTPUT                       
         DECLARE @AllowMultipleDrops BIT    
         SET @AllowMultipleDrops = CASE UPPER(LTRIM(RTRIM(ISNULL(@Setting_Value, ''))))
                                         WHEN 'TRUE' THEN 1
                                         ELSE 0
                                    END
    	          
    	                         
	SELECT ISNULL(SP.System_Parameter_Display_Bar, '') BarPosition,
	       ISNULL(SP.System_Parameter_Display_Company, '') Company,
	       ISNULL(SP.System_Parameter_Display_Site, '') [Site],
	       ISNULL(SP.System_Parameter_Display_Sub, '') SubCompany,
	       ISNULL(SP.System_Parameter_Display_Zone, '') Zone,
	       SP.System_Parameter_Auto_Generate_Model_Codes AutoGenerateModelCode,
	       SP.System_Parameter_Model_Code_Prefix ModelCodePrefix,
	       SP.System_Parameter_Model_Code_Number_Length ModelCodeMinLength,
	       SP.System_Parameter_Auto_Generate_Stock_Codes AutoGenerateStockCode,
	       SP.System_Parameter_Stock_Code_Prefix StockCodePrefix,
	       SP.System_Parameter_Stock_Code_Number_Length StockCodeMinLength,
	       SP.System_Parameter_Stock_Allow_Bulk_Purchase AllowStockBulkPurchase,
	       SP.System_Parameter_Force_Site_Reps_On_Stock ForceSiteRepsOnStock,
	       SP.System_Parameter_Service_Handheld ServiceHandheld,
	       ISNULL(SP.System_Parameter_Server_Name, '') ServerName,
	       ISNULL(MACHINE.Machine_ID, 0) Machine_ID,
	       ISNULL(MACHINE.Machine_Class_ID, 0) Machine_Class_ID,
	       ISNULL(Machine_Class.Machine_Type_ID, 0) Machine_Type_ID,
	       SP.System_Parameter_Region_Culture RegionCulture,
	       @IsSiteLicensingEnabled IsSiteLicensingEnabled,
	       @ImportExportAssetFile	ImportExport_AssetFile,
	       @IsEnrolmentFlag  IsEnrolmentFlag,
	       @IsEnrolmentComplete IsEnrolmentComplete,
	       @IsPowerPromoReportsRequired IsPowerPromoReportsRequired,
	       @CentralizedDeclaration CentralizedDeclaration,
	       @IsEmployeecardTrackingEnabled IsEmployeecardTrackingEnabled,
	       @AllowOfflineDeclaration AllowOfflineDeclaration,
	       @AddShortpayInVoucherOut AddShortpayInVoucherOut,
	       @SystemSettingsDisplayTabVisible SystemSettingsDisplayTabVisible,
	       @SystemSettingsServiceTabVisible SystemSettingsServiceTabVisible,
	       @ValidateAGSForGMU ValidateAGSForGMU,
		   @VIEWPARTIALLYCONFIGUREDSITES VIEWPARTIALLYCONFIGUREDSITES,
		   @IsGameCappingEnabled IsGameCappingEnabled,
		   @IsSuppressZoneEnabled IsSuppressZoneEnabled,
		   @IsSingleCardEmployee IsSingleCardEmployee,
		   @AllowEnableDisableBarPosition AllowEnableDisableBarPosition,
		   @IsAlertEnabled AS IsAlertEnabled,
		   @IsEmailAlertEnabled AS IsEmailAlertEnabled,
		   @IsAutoCalendarEnabled AS IsAutoCalendarEnabled,
		   @SendMailFromEnterprise AS SendMailFromEnterprise,
		   @CancelPendingMails AS CancelPendingMails,
		   @AllowMultipleDrops AS AllowMultipleDrops
	FROM   (
	           (
	               System_Parameters SP LEFT JOIN MACHINE ON SP.System_Parameter_Unallocated_Model 
	               = MACHINE.Machine_ID
	           ) LEFT JOIN Machine_Class ON MACHINE.Machine_Class_ID =
	           Machine_Class.Machine_Class_ID
	       )
	
	SELECT Machine_Type_ID,
	       Machine_Type_Code
	FROM   Machine_Type
	ORDER BY
	       Machine_Type_Code
	
	SELECT CultureInfo
	FROM   userlanguages ORDER BY CultureInfo ASC
	
	
	
	
	
	SET NOCOUNT OFF
END

GO

