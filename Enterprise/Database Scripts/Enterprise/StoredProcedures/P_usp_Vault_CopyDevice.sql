USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'usp_Vault_CopyDevice'
   )
    DROP PROCEDURE dbo.usp_Vault_CopyDevice
GO

CREATE PROCEDURE dbo.usp_Vault_CopyDevice
	@Name VARCHAR(150),
	@Serial_NO VARCHAR(30),
	@User_ID INT,
	@Module_ID INT,
	@Module_Name VARCHAR(150),
	@Screen_Name VARCHAR(150),
	@SrcVault_ID INT
AS
BEGIN
	DECLARE @Vault_ID             INT    
	DECLARE @Active               BIT   
	DECLARE @Site_ID              INT     
	DECLARE @Alert_Level          INT     
	DECLARE @Manufacturer_ID      INT    
	DECLARE @Type_Prefix          VARCHAR(10)    
	DECLARE @Capacity             DECIMAL(18, 2)    
	DECLARE @NoofCoinHopper       INT    
	DECLARE @NoofCassettes        INT    
	DECLARE @IsWebServiceEnabled  BIT    
	DECLARE @NGA_Type             VARCHAR(50) = 'VAULT'    
	DECLARE @Description          VARCHAR(200) = NULL    
	DECLARE @StandardFillAmount   DECIMAL(18, 2)    
	DECLARE @vaultdevice          TABLE(Vault_ID INT)  
	DECLARE @AutoAdjustEnabled	  BIT
	DECLARE @FillRejection		  BIT
	
	SELECT @Active = 0,
	       @Site_ID = 0,
	       @Alert_Level = Alert_Level,
	       @Manufacturer_ID = Manufacturer_ID,
	       @Type_Prefix = Type_Prefix,
	       @Capacity = Capacity,
	       @NoofCoinHopper = NoofCoinHopper,
	       @NoofCassettes = NoofCassettes,
	       @IsWebServiceEnabled = IsWebServiceEnabled,
	       @NGA_Type = 'VAULT',
	       @Description = t.DESCRIPTION,
	       @StandardFillAmount = StandardFillAmount,
	       @AutoAdjustEnabled = AutoAdjustEnabled,
	       @FillRejection = FillRejection
	FROM   tvault_devices td
	       INNER JOIN tNGADevices t
	            ON  td.NGADevice_ID = t.NGADevice_ID
	WHERE  vault_id = @SrcVault_ID   
	
	INSERT INTO @vaultdevice
	EXEC usp_Vault_UpdateDevice 
	     0,
	     @Name,
	     @Serial_NO,
	     @Active,
	     @Site_ID,
	     @Alert_Level,
	     @User_ID,
	     @Module_ID,
	     @Module_Name,
	     @Screen_Name,
	     @Manufacturer_ID,
	     @Type_Prefix,
	     @Capacity,
	     @NoofCoinHopper,
	     @NoofCassettes,
	     @IsWebServiceEnabled,
	     @NGA_Type,
	     @Description,
	     @StandardFillAmount,
	     @AutoAdjustEnabled,
		 @FillRejection  
	
	SELECT @Vault_ID = Vault_ID
	FROM   @vaultdevice  
	
	IF @Vault_ID <= 0
	    RETURN   
	
	INSERT INTO tVault_Cassettes
	  (
	    Vault_ID,
	    Cassette_Name,
	    TYPE,
	    Denom,
	    IsActive,
	    AlertLevel,
	    StandardFillAmount,
	    MinFillAmount,
	    MaxFillAmount,
	    DESCRIPTION,
	    Created_Date,
	    Modified_Date
	  )
	SELECT @Vault_ID,
	       Cassette_Name,
	       TYPE,
	       Denom,
	       IsActive,
	       AlertLevel,
	       StandardFillAmount,
	       MinFillAmount,
	       MaxFillAmount,
	       DESCRIPTION,
	       GETDATE(),
	       GETDATE()
	FROM   tVault_Cassettes tc
	WHERE  tc.Vault_ID = @SrcVault_ID  
	
	DECLARE @CassetteCount    INT     
	DECLARE @HopperCount      INT     
	DECLARE @CassetteAmount   DECIMAL(18, 2)    
	DECLARE @HopperMaxAmount  DECIMAL(18, 2)     
	
	
	SELECT @CassetteAmount = SUM(MaxFillAmount),
	       @CassetteCount = COUNT(Cassette_ID)
	FROM   tvault_cassettes TC
	WHERE  vault_id = @Vault_ID
	       AND ISActive = 1
	       AND [Type] = 1 -- Cassette    
	
	SELECT @HopperMaxAmount = SUM(MaxFillAmount),
	       @HopperCount = COUNT(Cassette_ID)
	FROM   tvault_cassettes TC
	WHERE  vault_id = @Vault_ID
	       AND ISActive = 1
	       AND [Type] = 2 -- Hopper    
	
	--Update number of hopper/Cassete and count     
	UPDATE tvault_Devices
	SET    NoofCassettes = @CassetteCount,
	       NoofCoinHopper = @HopperCount,
	       Capacity = (
	           ISNULL(@CassetteAmount, 0.00) + ISNULL(@HopperMaxAmount, 0.00)
	       )
	WHERE  vault_id = @Vault_ID
END
GO