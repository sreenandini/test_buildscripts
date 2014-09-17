USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'usp_Vault_UpdateCassetteDetails'
   )
    DROP PROCEDURE dbo.usp_Vault_UpdateCassetteDetails
GO

CREATE PROCEDURE dbo.usp_Vault_UpdateCassetteDetails
	@Cassette_ID INT,
	@Vault_ID INT,
	@Cassette_Name VARCHAR(150),
	@Type INT,
	@Denom FLOAT,
	@IsActive BIT,
	@AlertLevel INT,
	@StandardFillAmount DECIMAL(18, 2),
	@MaxFillAmount DECIMAL(18, 2),
	@Description VARCHAR(150), 
	 --Audit details   
	 /*------------------------*/
	@User_ID INT,
	@Module_ID INT,
	@Module_Name VARCHAR(150),
	@Screen_Name VARCHAR(150) 
	 /*------------------------*/
AS
	/*****************************************************************************************************  
DESCRIPTION : PROC Description    
CREATED DATE: 26-aug-2012  
MODULE  : PROC used in vault creation screen , Create or update cassettes   
CHANGE HISTORY :  
ERROR CODE : 
-1 -- Unkonwn error 
-2 -- Duplicate name 
-3 -- Web service Disabled 
-4 -- Max Cassette Level has been reached
------------------------------------------------------------------------------------------------------  
AUTHOR     DESCRIPTON          MODIFIED DATE  
------------------------------------------------------------------------------------------------------  


*****************************************************************************************************/  
BEGIN
	--Get Vault Name
	DECLARE @VaultName VARCHAR(150)
	SELECT @VaultName = NAME
	FROM   tVault_Devices td
	WHERE  td.Vault_ID = @Vault_ID
	
	--AUDIT DETAILS      
	
	DECLARE @CurrentCassetteCount INT  
	IF (@Type = '1')
	BEGIN
	    SET @CurrentCassetteCount = (
	            SELECT s.Setting_Value
	            FROM   Setting s
	            WHERE  s.Setting_Name = 'MaxNoOfVaultCassettes'
	        )
	END
	ELSE
	BEGIN
	    SET @CurrentCassetteCount = (
	            SELECT s.Setting_Value
	            FROM   Setting s
	            WHERE  s.Setting_Name = 'MaxNoOfVaultHoppers'
	        )
	END  
	
	DECLARE @bCassetteLimitReached  BIT   
	DECLARE @iCassetteLimitReached  INT 
	
	IF @Cassette_ID = 0
	BEGIN
	    IF (@Type = '1')
	    BEGIN
	        SELECT @iCassetteLimitReached = COUNT('N')
	        FROM   tvault_cassettes
	        WHERE  Vault_ID = @Vault_ID
	               AND [Type] = '1'
	    END
	    ELSE
	    BEGIN
	        SELECT @iCassetteLimitReached = COUNT('N')
	        FROM   tvault_cassettes
	        WHERE  Vault_ID = @Vault_ID
	               AND [Type] = '2'
	    END
	    IF @iCassetteLimitReached >= @CurrentCassetteCount
	        SET @bCassetteLimitReached = 1
	END
	
	
	IF ISNULL(@bCassetteLimitReached, 0) = 0
	BEGIN
	    DECLARE @WebserviceEnabled BIT   
	    
	    SELECT @WebserviceEnabled = IsWebServiceEnabled
	    FROM   tVault_Devices
	    WHERE  vault_id = @Vault_ID  
	    
	    IF @WebserviceEnabled = 0
	    BEGIN
	        RETURN -3
	    END    
	    
	    DECLARE @Operation VARCHAR(100)    
	    
	    IF EXISTS (
	           SELECT ''
	           FROM   tVault_Cassettes
	           WHERE  Cassette_Name = @Cassette_Name
	                  AND cassette_id <> @Cassette_ID
	                  AND vault_id = @Vault_ID
	       )
	    BEGIN
	        RETURN -2
	    END 
	    
	    BEGIN TRAN     
	    
	    DECLARE @tempAudit     TABLE (
	                old_Cassette_Name VARCHAR(150),
	                old_Denom FLOAT,
	                old_IsActive BIT,
	                old_AlertLevel INT,
	                old_StandardFillAmount DECIMAL(18, 2),
	                old_MaxFillAmount BIGINT,
	                old_Description VARCHAR(150),
	                new_Cassette_Name VARCHAR(150),
	                new_Denom FLOAT,
	                new_IsActive BIT,
	                new_AlertLevel INT,
	                new_StandardFillAmount DECIMAL(18, 2),
	                new_MaxFillAmount BIGINT,
	                new_Description VARCHAR(150)
	            )     
	    
	    
	    DECLARE @CurrDatetime  DATETIME    
	    SET @CurrDatetime = GETDATE()    
	    
	    
	    IF @Cassette_ID = 0
	    BEGIN
	        --New cassette     
	        INSERT INTO tvault_cassettes
	          (
	            Vault_ID,
	            Cassette_Name,
	            TYPE,
	            Denom,
	            IsActive,
	            AlertLevel,
	            StandardFillAmount,
	            MaxFillAmount,
	            DESCRIPTION,
	            Created_Date,
	            Modified_Date
	          )
	        VALUES
	          (
	            @Vault_ID,
	            @Cassette_Name,
	            @Type,
	            ROUND(@Denom, 2),
	            @IsActive,
	            @AlertLevel,
	            @StandardFillAmount,
	            @MaxFillAmount,
	            @Description,
	            @CurrDatetime,
	            @CurrDatetime
	          )    
	        
	        IF @@ERROR <> 0
	            GOTO err    
	        
	        SET @Operation = 'ADD'        
	        SET @Cassette_ID = SCOPE_IDENTITY()
	    END
	    ELSE
	        --Update cassette
	    BEGIN
	        UPDATE tvault_cassettes
	        SET    Cassette_Name = @Cassette_Name,
	               Denom = ROUND(@Denom, 2),
	               IsActive = @IsActive,
	               AlertLevel = @AlertLevel,
	               StandardFillAmount = @StandardFillAmount,
	               MaxFillAmount = @MaxFillAmount,
	               DESCRIPTION = @DESCRIPTION,
	               Modified_Date = @CurrDatetime 
	               OUTPUT 
	               DELETED.Cassette_Name,
	               DELETED.Denom,
	               DELETED.IsActive,
	               DELETED.AlertLevel,
	               DELETED.StandardFillAmount,
	               DELETED.MaxFillAmount,
	               DELETED.Description,
	               INSERTED.Cassette_Name,
	               INSERTED.Denom,
	               INSERTED.IsActive,
	               INSERTED.AlertLevel,
	               INSERTED.StandardFillAmount,
	               INSERTED.MaxFillAmount,
	               INSERTED.Description 
	               INTO @tempAudit
	        WHERE  Cassette_ID = @Cassette_ID
	               AND Vault_ID = @Vault_ID  
	        
	        SET @Operation = 'MODIFY'        
	        
	        IF @@ERROR <> 0
	            GOTO err
	    END     
	    
	    DECLARE @CassetteCount                    INT   
	    DECLARE @HopperCount                      INT   
	    DECLARE @CassetteAmount                   DECIMAL(18, 2)  
	    DECLARE @HopperMaxAmount                  DECIMAL(18, 2)   
	    
	    DECLARE @VaultCassetteStandardFillAmount  DECIMAL(18, 2)  
	    DECLARE @VaultHopperStandardFillAmount    DECIMAL(18, 2)  
	    
	    
	    SELECT @CassetteAmount = SUM(MaxFillAmount),
	           @CassetteCount = COUNT(Cassette_ID),
	           @VaultCassetteStandardFillAmount = SUM(StandardFillAmount)
	    FROM   tvault_cassettes TC
	    INNER JOIN tVault_CassetteTypes tt   WITH (NOLOCK)
	    ON tc.Type=tt.CassetteType_ID
	    WHERE  vault_id = @Vault_ID AND tt.CassetteType_Name='Cassette'
	           AND ISActive = 1
	           --AND [Type] = 1 -- Cassette  
	    
	    SELECT @HopperMaxAmount = SUM(MaxFillAmount),
	           @HopperCount = COUNT(Cassette_ID),
	           @VaultHopperStandardFillAmount = SUM(StandardFillAmount)
	    FROM   tvault_cassettes TC
	   INNER JOIN tVault_CassetteTypes tt   WITH (NOLOCK)
	    ON tc.Type=tt.CassetteType_ID
	    WHERE  vault_id = @Vault_ID AND tt.CassetteType_Name='Hopper'
	           AND ISActive = 1
	           --AND [Type] = 2 -- Hopper  
	    
	    
	    --Update number of hopper/Cassete and count   
	    UPDATE tvault_Devices
	    SET    NoofCassettes = @CassetteCount,
	           NoofCoinHopper = @HopperCount,
	           Capacity = (
	               ISNULL(@CassetteAmount, 0.00) + ISNULL(@HopperMaxAmount, 0.00)
	           ),
	           StandardFillAmount = ISNULL(@VaultCassetteStandardFillAmount, 0.00) 
	           + ISNULL(@VaultHopperStandardFillAmount, 0.00)
	    WHERE  vault_id = @vault_id   
	    
	    DECLARE @Site_Code VARCHAR(20)  
	    
	    SELECT @Site_Code = s.site_code
	    FROM   tVault_Devices td WITH (NOLOCK)
	           INNER JOIN tNGADevices D WITH (NOLOCK)
	                ON  td.NGADevice_ID = d.NGADevice_ID
	           INNER JOIN tNGAInstallations I WITH (NOLOCK)
	                ON  d.NGADevice_ID = i.NGADevice_ID
	           INNER JOIN [SITE] s WITH (NOLOCK)
	                ON  s.site_id = i.site_id
	    WHERE  td.Vault_id = @vault_id
	           AND i.End_Date IS NULL 
	    
	    
	    --Changes from exporting to site     
	    IF @Site_Code IS NOT NULL
	    BEGIN
	        INSERT INTO Export_History
	          (
	            -- EH_ID -- this column value is auto-generated,    
	            EH_Date,
	            EH_Reference1,
	            EH_Type,
	            EH_Status,
	            EH_Export_Date,
	            EH_Site_Code
	          )
	        VALUES
	          (
	            GETDATE(),
	            @Vault_ID,
	            'VAULTDEVICE',
	            NULL,
	            NULL,
	            @Site_Code
	          )    
	        
	        IF @@ERROR <> 0
	            GOTO err
	    END 
	    
	    /*--------------------AUDIT-------------------------------------------*/    
	    
	    
	    DECLARE @staff_id    INT        
	    DECLARE @staff_Name  VARCHAR(100)         
	    SET @staff_id = @User_ID -- CHANGED FROM USER ID TO STAFFID FROM CLIENT       
	    
	    SELECT @staff_Name = ISNULL(Staff_First_Name, '')
	    FROM   Staff
	    WHERE  Staff_ID = @User_ID    
	    
	    DECLARE @Audit_DESC    VARCHAR(MAX)         
	    
	    DECLARE @Cassettetype  VARCHAR(30)    
	    SELECT @Cassettetype =CassetteType_Name FROM tVault_CassetteTypes WHERE CassetteType_ID =@Type
	    
	        
	    IF @Operation = 'ADD'
	    BEGIN
	        SET @Audit_DESC = 
	             ' Cassettetype =' + @Cassettetype 
	            + ' Denom=' + CAST(@Denom AS VARCHAR(30)) 
	            + ' IsActive=' + CAST(@IsActive AS VARCHAR(30)) 
	            + ' AlertLevel=' + CAST(@AlertLevel AS VARCHAR(30)) 
	            + ' StandardFillAmount=' + CAST(@StandardFillAmount AS VARCHAR(30)) 
	            + ' MaxFillAmountAS=' + CAST(@MaxFillAmount AS VARCHAR(30)) 
	            + ' Description=' + @description
	    END
	    ELSE
	    BEGIN
	        SELECT @Audit_DESC = (
	                   CASE 
	                        WHEN old_Cassette_Name <> new_Cassette_Name THEN 
	                             'Updated [Cassette_Name] from ' +
	                             old_Cassette_Name + ' TO  ' +
	                             new_Cassette_Name
	                        ELSE ''
	                   END
	               ) + ' ' + (
	                   CASE 
	                        WHEN old_Denom <> new_Denom THEN 
	                             ' Updated [Denom]  from ' + CAST(old_Denom AS VARCHAR(20)) 
	                             + ' TO  ' + CAST(new_Denom AS VARCHAR(20))
	                        ELSE ''
	                   END
	               ) 
	               + ' ' + (
	                   CASE 
	                        WHEN old_IsActive <> new_IsActive THEN 
	                             ' Updated [IsActive]  from ' + CAST(old_IsActive AS VARCHAR(20)) 
	                             + ' TO  ' + CAST(New_IsActive AS VARCHAR(20))
	                        ELSE ''
	                   END
	               ) 
	               + ' ' + (
	                   CASE 
	                        WHEN old_AlertLevel <> new_AlertLevel THEN 
	                             ' Updated [AlertLevel]  from ' + CAST(old_AlertLevel AS VARCHAR(20)) 
	                             + ' TO  ' + CAST(New_AlertLevel AS VARCHAR(20))
	                        ELSE ''
	                   END
	               ) 
	               + ' ' + (
	                   CASE 
	                        WHEN old_StandardFillAmount <>
	                             new_StandardFillAmount THEN 
	                             ' Updated [StandardFillAmount]  from ' + CAST(old_StandardFillAmount AS VARCHAR(20)) 
	                             + ' TO  ' + CAST(New_StandardFillAmount AS VARCHAR(20))
	                        ELSE ''
	                   END
	               ) +
	               
	               ' ' + (
	                   CASE 
	                        WHEN old_MaxFillAmount <> new_MaxFillAmount THEN 
	                             ' Updated [MaxFillAmount]  from ' + CAST(old_MaxFillAmount AS VARCHAR(20)) 
	                             + ' TO  ' + CAST(New_MaxFillAmount AS VARCHAR(20))
	                        ELSE ''
	                   END
	               ) + (
	                   CASE 
	                        WHEN old_Description <> new_Description THEN 
	                             ' Updated [Description] from ' +
	                             old_Description + ' TO  ' +
	                             new_Description
	                        ELSE ''
	                   END
	               )
	        FROM   @tempAudit
	    END    
	    
	    IF (@Audit_DESC <> '')
	    BEGIN
	        SET @Audit_DESC = '[Cassette_ID] =' + CAST(@Cassette_ID AS VARCHAR(10)) 
	            + ' Cassette_Name=' + @Cassette_Name +
	            ' [Vault_ID_ID] =' + CAST(@Vault_ID AS VARCHAR(10)) +
	            ' [Vault_Name] =' + CAST(@VaultName AS VARCHAR(150)) +
	            ' ' + LTRIM(@Audit_DESC)        
	        
	        EXEC [usp_InsertAuditData] 
	             @staff_id,
	             @staff_Name,
	             @Module_ID,
	             @Module_Name,
	             @Screen_Name,
	             '',
	             '',
	             '',
	             '',
	             @Audit_DESC,
	             @Operation      
	        
	        IF @@ERROR <> 0
	            GOTO err
	    END
	END
	ELSE
	BEGIN
	    RETURN -4
	END 
	/*END--------------------AUDIT-------------------------------------------*/ 
	
	--Error Handling    
	COMMIT TRAN 
	RETURN 
	
	err: 
	ROLLBACK TRAN 
	RETURN -1
END
GO