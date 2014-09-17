/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 6/27/2013 11:18:11 PM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetSiteSettingsInXML]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetSiteSettingsInXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- exec rsp_GetSiteSettingsInXML 1
CREATE PROCEDURE rsp_GetSiteSettingsInXML(
    @Site_ID       INT,
    @SettingName   VARCHAR(50) = NULL,
    @SettingValue  VARCHAR(50) = NULL OUTPUT
)
AS
BEGIN
	DECLARE @XMLData1    XML    
	DECLARE @XMLData2    XML    
	DECLARE @XMLData3    XML  
	 DECLARE @XMLData4    XML          
	DECLARE @AFTEnabled  BIT  
	
	
	DECLARE @ProfileID   VARCHAR(20)
	SELECT @ProfileID = Site_Setting_Profile_ID
	FROM   SITE
	WHERE  site_id = @Site_ID
	
	
	SELECT AFS.* INTO #temptable
	FROM   aftsettings AFS
	       INNER JOIN SITE S
	            ON  AFS.SiteID = S.Site_ID
	            AND ISNULL(S.AFT_Settings_Enabled, 0) = 1
	WHERE  S.Site_ID = @Site_ID  
	
	SELECT ROW_NUMBER() OVER(ORDER BY Setting_Name) AS Setting_ID,
	       Setting_Name,
	       Setting_Value INTO #temp
	FROM   dbo.#temptable 
	       UNPIVOT(
	           Setting_Value FOR Setting_Name IN ([AFTTransactionsAllowed], 
	                                             [AllowNonCashableDeposits], 
	                                             [AllowCashableDeposits], 
	                                             [AllowCashWithdrawal], 
	                                             [AllowOffers], 
	                                             [AllowPartialTransfers], 
	                                             [AllowPointsWithdrawal], 
	                                             [AllowRedeemOffers], 
	                                             [AutoDepositCashableCreditsonCardOut], 
	                                             [AutoDepositNonCashableCreditsonCardOut], 
	                                             [EFTTimeoutValue], 
	                                             [MaxDepositAmount], 
	                                             [MaxWithDrawAmount], 
	                                             [Option1WithdrawalAmount], 
	                                             [Option2WithdrawalAmount], 
	                                             [Option3WithdrawalAmount], 
	                                             [Option4WithdrawalAmount], 
	                                             [Option5WithdrawalAmount], 
	                                             [SiteID])
	       ) AS UN
	
	SET @XMLData1 = (
	        SELECT *
	        FROM   (
	                   SELECT SettingsMaster_Name COLLATE DATABASE_DEFAULT AS 
	                          Setting_Name,
	                          SettingsProfileItems_SettingsMaster_Values COLLATE 
	                          DATABASE_DEFAULT AS Setting_Value
	                   FROM   SettingsMaster SM
	                          INNER JOIN SettingsProfileItems SPI
	                               ON  SM.SettingsMaster_ID = SPI.SettingsProfileItems_SettingsMaster_ID
	                          INNER JOIN SettingsProfile SP
	                               ON  SPI.SettingsProfileItems_SettingsProfile_ID = 
	                                   SP.SettingsProfile_ID
	                   WHERE  SP.SettingsProfile_ID = (
	                              SELECT Site_Setting_Profile_ID
	                              FROM   [Site]
	                              WHERE  Site_ID = @Site_ID
	                          )
	                          AND SM.SettingsMaster_Type = 'DB'
	                          AND SM.SettingsMaster_Name NOT IN ('PT_GATEWAY_IP', 
	                                                            'PT_GATEWAY_PORT_NO', 
	                                                            'PT_GATEWAY_MSG_RESP_TIMEOUT', 
	                                                            'CMPAPP_UNAME', 
	                                                            'CMPAPP_PWD', 
	                                                            'CMP_KIOSKURL', 
	                                                            'AGSValue',
																'CMPMode' )
	                   UNION ALL
	                   SELECT Setting_Name COLLATE DATABASE_DEFAULT AS 
	                          Setting_Name,
	                          Setting_Value COLLATE DATABASE_DEFAULT AS 
	                          Setting_Value
	                   FROM   dbo.Setting
	                   WHERE  Setting_Name IN ('AGSValue')
	               ) A
	        ORDER BY
	               A.Setting_Name
	               FOR XML PATH('Setting'),
	               ROOT('DB')
	    )
	
	SET @XMLData2 = (
	        SELECT SettingsMaster_Name AS Setting_Name,
	               SettingsProfileItems_SettingsMaster_Values AS Setting_Value
	        FROM   SettingsMaster SM
	               INNER JOIN SettingsProfileItems SPI
	                    ON  SM.SettingsMaster_ID = SPI.SettingsProfileItems_SettingsMaster_ID
	               INNER JOIN SettingsProfile SP
	                    ON  SPI.SettingsProfileItems_SettingsProfile_ID = SP.SettingsProfile_ID
	        WHERE  SP.SettingsProfile_ID = (
	                   SELECT Site_Setting_Profile_ID
	                   FROM   [Site]
	                   WHERE  Site_ID = @Site_ID
	               )
	               AND SM.SettingsMaster_Type = 'XML'
	        ORDER BY
	               SettingsMaster_Name 
	               FOR XML PATH('Setting'),
	               ROOT('XML')
	    )    
	
	
	SET @XMLData3 = (
	        SELECT AFS.Setting_Name,
	               AFS.Setting_Value
	        FROM   #temp AFS
	               INNER JOIN SettingsProfile SP
	                    ON  SP.SettingsProfile_ID = @ProfileID
	        ORDER BY
	               AFS.Setting_name 
	               FOR XML PATH('Setting'),
	               ROOT('AFT')
	    ) 
	
	       
	SET @XMLData4 = (    
         SELECT SP.SettingsProfile_Description AS ProfileName    
         FROM   SettingsProfile SP    
                     where  SP.SettingsProfile_ID = @ProfileID    
                           
                FOR XML PATH('Profile'),    
                ROOT('SiteProfile')    
     )     
     
	DROP TABLE #temptable
	DROP TABLE #temp
	
	--SELECT '<Settings>'+convert(varchar(max),@xmldata1)+ '</Settings>'
	--SELECT '<Settings>'+convert(varchar(max),@xmldata1)+convert(varchar(max),@xmldata2)+'</Settings>'
	--Remove later when XML is enabled    
	IF (@xmldata3 IS NOT NULL)    
 BEGIN    
     SELECT '<Settings>' + CONVERT(VARCHAR(MAX), @xmldata1) + CONVERT(VARCHAR(MAX), @xmldata3)  + CONVERT(VARCHAR(MAX),@XMLData4) +     
            + '</Settings>'    
 END    
 ELSE    
 BEGIN    
     SELECT '<Settings>' + CONVERT(VARCHAR(MAX), @xmldata1) + CONVERT(VARCHAR(MAX),@XMLData4) +  '</Settings>'    
 END 
	
	SELECT @SettingValue = SettingsProfileItems_SettingsMaster_Values
	FROM   SettingsMaster SM
	       INNER JOIN SettingsProfileItems SPI
	            ON  SM.SettingsMaster_ID = SPI.SettingsProfileItems_SettingsMaster_ID
	       INNER JOIN SettingsProfile SP
	            ON  SPI.SettingsProfileItems_SettingsProfile_ID = SP.SettingsProfile_ID
	WHERE  SP.SettingsProfile_ID = (
	           SELECT Site_Setting_Profile_ID
	           FROM   [Site]
	           WHERE  Site_ID = @Site_ID
	       )
	       AND SettingsMaster_Name = @SettingName
END
GO

