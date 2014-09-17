USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'usp_Vault_UpdateDevice'
   )
    DROP PROCEDURE dbo.usp_Vault_UpdateDevice
GO

CREATE PROCEDURE dbo.usp_Vault_UpdateDevice
	@Vault_ID INT,
	@Name VARCHAR(150),
	@Serial_NO VARCHAR(30),
	@Active BIT,
	@Site_ID INT ,
	@Alert_Level INT ,
	@User_ID INT,
	@Module_ID INT,
	@Module_Name VARCHAR(150),
	@Screen_Name VARCHAR(150),
	@Manufacturer_ID INT,
	@Type_Prefix VARCHAR(10),
	@Capacity DECIMAL(18, 2),
	@NoofCoinHopper INT,
	@NoofCassettes INT,
	@IsWebServiceEnabled BIT,
	@NGA_Type VARCHAR(50) ='VAULT',
	@Description VARCHAR(200)=NULL,
	@StandardFillAmount DECIMAL(18,2),
	@AutoAdjustEnabled BIT,
	@FillRejection BIT
AS
	/*****************************************************************************************************
DESCRIPTION	: PROC Description  
CREATED DATE: PROC CreateDate
MODULE		: PROC used in Modules	
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR					DESCRIPTON										MODIFIED DATE
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/

BEGIN
	--AUDIT DETAILS  
	DECLARE @NGA_Audit TABLE
		(
			  New_Description VARCHAR(200),
	          Old_Description VARCHAR(200)
		)
		
	DECLARE @Audit           TABLE(
	            New_Name VARCHAR(500),
	            Old_Name VARCHAR(100),
	            New_Serial VARCHAR(100),
	            Old_Serial VARCHAR(100),
	            new_ACTIVE BIT,
	            ACTIVE BIT,
	            new_Alert_Level INT,
	            Alert_Level INT,
	            New_Manufacturer_ID INT,
	            Old_Manufacturer_ID INT,
	            New_Type_Prefix VARCHAR(10),
	            Old_Type_Prefix VARCHAR(10),
	            New_Type_Capacity DECIMAL(18, 2),
	            Old_Type_Capacity DECIMAL(18, 2),
	            New_NoofCoinHopper INT,
	            Old_NoofCoinHopper INT,
	            New_NoofCassettes INT,
	            Old_NoofCassettes INT,
	            New_IsWebServiceEnabled INT,
	            Old_IsWebServiceEnabled INT,
	            New_AutoAdjustEnabled BIT,
	            OLD_AutoAdjustEnabled BIT,
	            New_FillRejection BIT,
				Old_FillRejection BIT
	        )  
	
	DECLARE @Operation       VARCHAR(100) 
	
	
	
	-- CHECK FOR DUPLICATES   
	DECLARE @temp_Name       VARCHAR(150)  
	DECLARE @temp_Serial_NO  VARCHAR(30)  
	
	SELECT @temp_Name = [Name]
	FROM   dbo.tVault_Devices WITH (NOLOCK)
	WHERE  Vault_ID <> @Vault_ID
	       AND [Name] = @Name   
	
	IF (@temp_Name IS NOT NULL)
	BEGIN
	    SELECT -10 AS Vault_ID 
	    RETURN
	END  
	
	--SELECT @temp_Serial_NO = Serial_NO
	--FROM   dbo.tVault_Devices WITH (NOLOCK)
	--WHERE  Vault_ID <> @Vault_ID
	--       AND Serial_NO = @Serial_NO  
	
	--IF (@temp_Serial_NO IS NOT NULL)
	--BEGIN
	--    SELECT -11 AS Vault_ID 
	--    RETURN
	--END 
	
	
	BEGIN TRAN 
	
	
	--INSERT OR UPDATE VAULT BASED ON VAULT ID   
	IF @Vault_ID = 0
	BEGIN
		
		DECLARE @NGAType_ID INT 
		DECLARE @NGA_ID INT
		
		SELECT @NGAType_ID=[Type_ID] 
		FROM tNGATypes WITH (NOLOCK) 
		WHERE Name=@NGA_Type
		
		INSERT INTO tNGADevices
		(
			[Name],
			Device_Type,
			[Description]
		)
		VALUES
		(
			@Name,
			@NGAType_ID,
			@Description
		)
		 IF @@ERROR <> 0
	        GOTO err
	    
		SET @NGA_ID=SCOPE_IDENTITY()
				
	    INSERT INTO dbo.tVault_Devices
	      (
	        [Name],
	        Serial_NO,
	        ACTIVE,
	        Site_ID,
	        Alert_Level,
	        Created_Date,
	        Manufacturer_ID,
	        Type_Prefix,
	        Capacity,
	        NoofCoinHopper,
	        NoofCassettes,
	        IsWebServiceEnabled,
	        Ngadevice_Id,
	        StandardFillAmount,
	        AutoAdjustEnabled ,
			FillRejection 
	      )
	    VALUES
	      (
	        @Name,
	        @Serial_NO,
	        @Active,
	        @Site_ID,
	        @Alert_Level,
	        GETDATE(),
	        @Manufacturer_ID,
	        @Type_Prefix,
	        @Capacity,
	        @NoofCoinHopper,
	        @NoofCassettes,
	        @IsWebServiceEnabled,
	        @NGA_ID,
	        @StandardFillAmount,
	        @AutoAdjustEnabled,
	        @FillRejection
	      )  
	    IF @@ERROR <> 0
	        GOTO err
	    
	    SET @Operation = 'ADD'  
	    SET @Vault_ID = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		
		UPDATE d
		SET
			[Name] = @Name,
			[DESCRIPTION]=@Description
			OUTPUT INSERTED.[DESCRIPTION],
			DELETED.[DESCRIPTION]
			INTO @NGA_Audit 
		from tNGADevices D 
		INNER JOIN tVault_Devices td with (nolock)
		on 	td.NGADevice_ID= d.NGADevice_ID
		WHERE td.Vault_id=@Vault_ID 	
		
	    UPDATE dbo.tVault_Devices
	    SET    [Name] = @Name,
	           Serial_NO = @Serial_NO,
	           [Active] = @Active,
	           Alert_Level = @Alert_Level,
	           Manufacturer_ID = @Manufacturer_ID,
	           Type_Prefix = @Type_Prefix,
	           Capacity = @Capacity,
	           NoofCoinHopper = @NoofCoinHopper,
	           NoofCassettes = @NoofCassettes,
	           IsWebServiceEnabled=@IsWebServiceEnabled,
	           StandardFillAmount = @StandardFillAmount,
	           AutoAdjustEnabled=@AutoAdjustEnabled,
			   FillRejection=@FillRejection
	           OUTPUT INSERTED.[Name],
	           DELETED.[Name],
	           INSERTED.Serial_NO,
	           DELETED.Serial_NO,
	           INSERTED.[Active],
	           DELETED.[Active],
	           INSERTED.Alert_Level,
	           DELETED.Alert_Level,
	           INSERTED.Manufacturer_ID ,
	           DELETED.Manufacturer_ID,
	           INSERTED.Type_prefix ,
	           DELETED.Type_prefix,
	           INSERTED.Capacity,
	           DELETED.Capacity,
	           INSERTED.NoofCoinHopper,
	           DELETED.NoofCoinHopper,
	           INSERTED.NoofCassettes,
	           DELETED.NoofCassettes,
	           INSERTED.IsWebServiceEnabled,
	           DELETED.IsWebServiceEnabled,
	           INSERTED.AutoAdjustEnabled ,
	           DELETED.AutoAdjustEnabled ,
			   INSERTED.FillRejection,
			   DELETED.FillRejection    
	           INTO @Audit
	    WHERE  Vault_ID = @Vault_ID
	    
	    IF @@ERROR <> 0
	        GOTO err
	    
	    SET @Operation = 'MODIFY'
	END  
	
	IF @IsWebServiceEnabled = 0 
	BEGIN
		UPDATE tvault_cassettes 
		SET IsActive= 0 
		WHERE vault_id=@vault_id 
	END 
	ELSE
	BEGIN 
		
		
		--Recalculate hopper/cassette numbers
		DECLARE @CassetteCount int 
		DECLARE @HopperCount int 
		DECLARE @CassetteAmount DECIMAL(18,2)
		DECLARE @HopperMaxAmount DECIMAL(18,2) 
		DECLARE @TotalStandardFillAmount DECIMAL(18,2)
		DECLARE @CassetteStandaradFillAmount DECIMAL(18,2)
		DECLARE @HopperStandaradFillAmount DECIMAL(18,2)
		
		SELECT @CassetteAmount = SUM(MaxFillAmount),@CassetteCount = COUNT(Cassette_ID),@CassetteStandaradFillAmount = SUM(TC.StandardFillAmount)
		FROM   tvault_cassettes TC
		 INNER JOIN tVault_CassetteTypes tt   WITH (NOLOCK)
	    ON tc.Type=tt.CassetteType_ID
		WHERE  vault_id = @Vault_ID  AND tt.CassetteType_Name='Cassette'
		       AND ISActive = 1
		       --AND [Type] = 1 -- Cassette
		
		SELECT @HopperMaxAmount = SUM(MaxFillAmount),@HopperCount = COUNT(Cassette_ID),@HopperStandaradFillAmount = SUM(TC.StandardFillAmount)
		FROM   tvault_cassettes TC
		 INNER JOIN tVault_CassetteTypes tt   WITH (NOLOCK)
	    ON tc.Type=tt.CassetteType_ID AND tt.CassetteType_Name='Hopper'
		WHERE  vault_id = @Vault_ID
		       AND ISActive = 1
		       --AND [Type] = 2 -- Hopper			
		
		SET @TotalStandardFillAmount= (ISNULL(@CassetteStandaradFillAmount,0.00) + ISNULL(@HopperStandaradFillAmount,0.00))
		
		--Update number of hopper/Cassete and count 
		Update tvault_Devices 
		SET StandardFillAmount=ISNULL(@TotalStandardFillAmount,0.00),
			NoofCassettes = ISNULL(@CassetteCount,0),
			NoofCoinHopper=ISNULL(@HopperCount,0),
			Capacity= (ISNULL(@CassetteAmount,0.00) + ISNULL(@HopperMaxAmount,0.00))
		WHERE vault_id=@vault_id 	
	
	END 
	
	DECLARE @Site_Code VARCHAR(20)
	
	SELECT  @Site_Code=s.site_code   
	FROM tVault_Devices td WITH (NOLOCK)    
	INNER JOIN tNGADevices D WITH (NOLOCK)    
		ON  td.NGADevice_ID= d.NGADevice_ID    
	INNER JOIN tNGAInstallations I WITH (NOLOCK)    
		ON d.NGADevice_ID= i.NGADevice_ID  
	INNER JOIN Site s WITH (NOLOCK)    
		ON s.site_id = i.site_id  
	WHERE td.Vault_id=@vault_id   
	AND  i.End_Date is null     
    
  
   --Changes from exporting to site   
	IF @Site_Code IS NOT NULL   
	BEGIN  
	
	--Export Manufacturer to site before exporting the vault
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
			@Manufacturer_ID,
			'MANUFACTURER_DETAILS',
			NULL,
			NULL,
			@Site_Code
		  )  
		  IF @@ERROR <> 0
			GOTO err
		
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
		
	DECLARE @staff_id    INT  
	DECLARE @staff_Name  VARCHAR(100)   
	SET @staff_id= @User_ID -- CHANGED FROM USER ID TO STAFFID FROM CLIENT 
	
	SELECT  @staff_Name = ISNULL(Staff_First_Name, '')
	FROM   Staff
	WHERE  Staff_ID = @User_ID  
	

	DECLARE @Audit_DESC VARCHAR(MAX)   
	
	IF @Operation = 'ADD'
	BEGIN
	    SET @Audit_DESC = 'NAME=' + @Name 
	        + ' ,Serial_No = ' + @Serial_NO 
	        + ' , Active =' + CAST(@Active AS VARCHAR(10)) 
	        + ' ,Site_ID= ' + CAST(@Site_ID AS VARCHAR(50)) 
	        + ', Alert_Level =' + CAST(@Alert_Level AS VARCHAR(10))  
	        + ', Type =' + @Type_Prefix
	        + ', Manufacturer_ID =' + CAST(@Manufacturer_ID AS VARCHAR(50))
	        + ', Type_Prefix =' + CAST(@Type_Prefix AS VARCHAR(10)) 
	        + ', Capacity =' + CAST(@Capacity AS VARCHAR(50)) 
			+ ', NoofCoinHopper =' + CAST(@NoofCoinHopper AS VARCHAR(50))
			+ ', NoofCassettes =' + CAST(@NoofCassettes AS VARCHAR(50))
			+ ', IsWebServiceEnabled=' + CAST(@IsWebServiceEnabled AS VARCHAR(10))
			+ ', Description=' + CAST(@Description AS VARCHAR(50))
			+ ', StandaradFillAmount=' + CAST(@StandardFillAmount AS VARCHAR(50))
			+ ', AutoAdjustEnabled =' + CAST(@AutoAdjustEnabled AS VARCHAR(50))
			+ ', FillRejection = ' + CAST(@FillRejection AS VARCHAR(50))
	            
	    EXEC [usp_InsertAuditData] 
	         @staff_id,
	         @staff_Name,
	         @Module_ID,
	         @Module_Name,
	         @Screen_Name,
	         '',
	         '',
	         '',
	         'TRUE',
	         @Audit_DESC,
	         @Operation
	    
	    IF @@ERROR <> 0
	        GOTO err
	END  
	
	IF @Operation = 'MODIFY'
	BEGIN
	    SELECT @Audit_DESC = (
	               CASE 
	                    WHEN New_Name <> Old_Name THEN 'Updated [Name] from ' +
	                         Old_Name + ' TO  ' +
	                         New_Name
	                    ELSE ''
	               END
	           ) + ' ' + (
	               CASE 
	                    WHEN New_Serial <> Old_Serial THEN 
	                         ' Updated [Serial NO]  from ' + Old_Serial 
	                         + ' TO  ' + New_Serial
	                    ELSE ''
	               END
	           ) + ' ' + + (
	               CASE 
	                    WHEN new_ACTIVE <> ACTIVE THEN ' Updated [Status] from ' 
	                         +
	                         CAST(ACTIVE AS VARCHAR(10)) 
	                         + ' TO  ' + CAST(new_ACTIVE AS VARCHAR(10))
	                    ELSE ''
	               END
	           ) + ' ' + (
	               CASE 
	                    WHEN new_Alert_Level <> Alert_Level THEN 
	                         ' Updated Alert_Level from ' +
	                         CAST(Alert_Level AS VARCHAR(10)) 
	                         + ' TO  ' + CAST(new_Alert_Level AS VARCHAR(10))
	                    ELSE ''
	               END
	           ) + ' ' + (
	               CASE 
	                    WHEN new_Manufacturer_ID <> old_Manufacturer_ID THEN 
	                         ' Updated Manufacturer from ' +
	                         CAST(old_Manufacturer_ID AS VARCHAR(10)) 
	                         + ' TO  ' + CAST(new_Manufacturer_ID AS VARCHAR(10))
	                    ELSE ''
	               END
	           )
	           + (
	               CASE 
	                    WHEN New_Type_Prefix <> Old_Type_Prefix THEN 
	                         ' Updated Type from ' +
	                         CAST(ISNULL(Old_Type_Prefix, '') AS VARCHAR(10)) 
	                         + ' TO  ' + CAST(New_Type_Prefix AS VARCHAR(10))
	                    ELSE ''
	               END
	           )
	           
	           + (
	               CASE 
	                    WHEN New_Type_Capacity <> Old_Type_Capacity THEN 
	                         ' Updated Capacity from ' +
	                         CAST(ISNULL(Old_Type_Capacity, '') AS VARCHAR(20)) 
	                         + ' TO  ' + CAST(New_Type_Capacity AS VARCHAR(20))
	                    ELSE ''
	               END
	           )
	             + (
	               CASE 
	                    WHEN New_NoofCoinHopper <> Old_NoofCoinHopper THEN 
	                         ' Updated NoofCoinHopper from ' +
	                         CAST(ISNULL(Old_NoofCoinHopper, '') AS VARCHAR(20)) 
	                         + ' TO  ' + CAST(New_NoofCoinHopper AS VARCHAR(20))
	                    ELSE ''
	               END
	             )
	               + (
	               CASE 
	                    WHEN New_NoofCassettes <> Old_NoofCassettes THEN 
	                         ' Updated New_NoofCassettes from ' +
	                         CAST(ISNULL(Old_NoofCassettes, '') AS VARCHAR(20)) 
	                         + ' TO  ' + CAST(New_NoofCassettes AS VARCHAR(20))
	                    ELSE ''
	               END
	               )
	               + (
	               CASE 
	                    WHEN New_IsWebServiceEnabled <> Old_IsWebServiceEnabled THEN 
	                         ' Updated IsWebServiceEnabled from ' +
	                         CAST(ISNULL(Old_IsWebServiceEnabled, '') AS VARCHAR(20)) 
	                         + ' TO  ' + CAST(New_IsWebServiceEnabled AS VARCHAR(20))
	                    ELSE ''
	               END
	               )
	                 + (
	               CASE 
	                    WHEN New_AutoAdjustEnabled <> OLD_AutoAdjustEnabled THEN 
	                         ' Updated AutoAdjustEnabled from ' +
	                         CAST(ISNULL(Old_AutoAdjustEnabled, '') AS VARCHAR(20)) 
	                         + ' TO  ' + CAST(New_AutoAdjustEnabled AS VARCHAR(20))
	                    ELSE ''
	               END
	               )
	                 + (
	               CASE 
	                    WHEN New_FillRejection <> Old_FillRejection THEN 
	                         ' Updated FillRejection from ' +
	                         CAST(ISNULL(Old_FillRejection, '') AS VARCHAR(20)) 
	                         + ' TO  ' + CAST(New_FillRejection AS VARCHAR(20))
	                    ELSE ''
	               END
	               )
	    FROM   @Audit   
	    
	    SELECT 
	    @Audit_DESC= ISnull(@Audit_DESC,'') + 
	     CASE 
	                    WHEN New_Description <> Old_Description THEN 
	                         ' Updated Description from ' +
	                         CAST(ISNULL(Old_Description, '') AS VARCHAR(20)) 
	                         + ' TO  ' + CAST(New_Description AS VARCHAR(20))
	                    ELSE ''
	     END
	    FROM 
	    @NGA_Audit 
			
	    
	    IF (@Audit_DESC <> '')
	    BEGIN
	        SET @Audit_DESC = '[VAULT_ID] =' + CAST(@Vault_ID AS VARCHAR(10)) +   ' [NAME]=' + @Name +
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
	
	SELECT @Vault_ID AS Vault_ID
	
	COMMIT TRAN
	RETURN 
	
	err: 
	ROLLBACK TRAN
END
GO

