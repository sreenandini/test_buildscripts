/*****************************************************************
 * Saves Card Level Settings in GameCappingCardLevelSettings Table
 * Time: 12/09/2013 11:35:09
 * exec esp_InsertCardLevelSettingsForGameCap 2,1,'1,1,1,1,1',
 ****************************************************************/

USE[Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects o
       WHERE  o.[name] = 'esp_InsertCardLevelSettingsForGameCap'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE esp_InsertCardLevelSettingsForGameCap
END

GO

CREATE PROCEDURE esp_InsertCardLevelSettingsForGameCap(
    @MaxNoofmachinestoCap  INT,
    @CardLevel             INT,
    @MintsToCap            VARCHAR(30),
    @Site                  VARCHAR(20),
    @Staff_ID              INT,
    @Module_ID             INT,
    @Module_Name           VARCHAR(150),
    @Screen_Name           VARCHAR(150)
)
AS
BEGIN
	DECLARE @Audit           TABLE (
	            N_MaxNoofMachinestoCap INT,
	            O_MaxNoofMachinestoCap INT,
	            N_MintsToCap VARCHAR(30),
	            O_MintsToCap VARCHAR(30)
	        )
	
	DECLARE @Staff_Name  VARCHAR(100)
	DECLARE @Audit_DESC VARCHAR(MAX)	
	
	SELECT @Staff_Name = ISNULL(Staff_First_Name, '')
	FROM   Staff
	WHERE  UserTableID = @Staff_ID
	
	DECLARE @SiteCode VARCHAR(20)
	
	SELECT @SiteCode = site_code
	FROM   SITE
	WHERE  site_id = @Site
	
	IF NOT EXISTS (
	       SELECT 1
	       FROM   GameCappingCardLevelSettings gccls
	       WHERE  gccls.CardLevel = @CardLevel
	              AND gccls.Site = @SiteCode
	   )
	BEGIN
	    INSERT INTO GameCappingCardLevelSettings
	      (
	        -- SettingID -- this column value is auto-generated,  
	        CardLevel,
	        MaxNoofMachinestoCap,
	        MintsToCap,
	        SITE
	      )
	    VALUES
	      (
	        @CardLevel,
	        @MaxNoofMachinestoCap,
	        @MintsToCap,
	        @SiteCode
	      )
	   	    
	    
	    SET @Audit_DESC = 'SiteCode = ' + @SiteCode
			+ ' ,CardLevel = ' + CAST(@CardLevel AS VARCHAR(10)) 
	        + ' ,MaxNoofMachinestoCap = ' + CAST(@MaxNoofMachinestoCap AS VARCHAR(10))
	        + ' ,MintsToCap =' + @MintsToCap
	    
	    EXEC [usp_InsertAuditData] 
	         @Staff_ID,
	         @Staff_Name,
	         @Module_ID,
	         @Module_Name,
	         @Screen_Name,
	         '',
	         '',
	         '',
	         'TRUE',
	         @Audit_DESC,
	         'ADD'
	END
	ELSE
	BEGIN
	    UPDATE GameCappingCardLevelSettings
	    SET    -- SettingID = ? -- this column value is auto-generated,  
	           MaxNoofMachinestoCap = @MaxNoofmachinestoCap,
	           MintsToCap = @MintsToCap
	           OUTPUT INSERTED.MaxNoofMachinestoCap,
	           DELETED.MaxNoofMachinestoCap,
	           INSERTED.MintsToCap,
	           DELETED.MintsToCap 
	           INTO @Audit
	    WHERE  CardLevel = @CardLevel
	           AND SITE = @SiteCode
	    
	    SELECT @Audit_DESC = (
	               CASE 
	                    WHEN O_MaxNoofMachinestoCap <> N_MaxNoofMachinestoCap THEN 
	                         'Updated [MaxNoofMachinestoCap] from ' +
	                         CAST(O_MaxNoofMachinestoCap AS VARCHAR(10)) + ' TO  ' +
	                         CAST(N_MaxNoofMachinestoCap AS VARCHAR(10))
	                    ELSE ''
	               END
	           ) + ' ' + (
	               CASE 
	                    WHEN O_MintsToCap <> N_MintsToCap THEN 
	                         ' Updated [MintsToCap]  from ' + O_MintsToCap 
	                         + ' TO  ' + N_MintsToCap
	                    ELSE ''
	               END
	           )
	    FROM   @Audit
	    
	    IF (@Audit_DESC <> '')
	    BEGIN
	    	SET @Audit_DESC = 'Site : ' + @SiteCode + ' ' + @Audit_DESC
	    	
	        EXEC [usp_InsertAuditData] 
	             @Staff_ID,
	             @Staff_Name,
	             @Module_ID,
	             @Module_Name,
	             @Screen_Name,
	             '',
	             '',
	             '',
	             '',
	             @Audit_DESC,
	             'MODIFY'
	    END
	END
END
GO

