/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 22/07/13 6:38:54 PM
 ************************************************************/

USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA       = N'dbo'
              AND SPECIFIC_NAME     = N'usp_Vault_ImportDrop'
   )
    DROP PROCEDURE dbo.usp_Vault_ImportDrop
GO


CREATE PROCEDURE dbo.usp_Vault_ImportDrop
	@xml XML = NULL,
	@IsSuccess INT OUTPUT
AS
	/*****************************************************************************************************      
DESCRIPTION : PROC Description        
CREATED DATE: 10-July-2013      
MODULE  : Insert exchange drop xml into enterprise        
CHANGE HISTORY :      
------------------------------------------------------------------------------------------------------      
AUTHOR     DESCRIPTON          MODIFIED DATE      
------------------------------------------------------------------------------------------------------      

*****************************************************************************************************/      

BEGIN
	SET NOCOUNT ON 
	
	DECLARE @NGAInstallation_no INT 
	
	BEGIN TRAN 
	DECLARE @IsFinalDrop BIT    
	DECLARE @docHandle INT   
	SET @IsSuccess = 0  
	
	DECLARE @ISWebserviceEnabled BIT -- If this value was not sent from exchange 
	
	EXEC sp_xml_preparedocument @docHandle OUTPUT,
	     @xml 
	
	-- Insert exchange drop xml into enterprise      
	
	
	DECLARE @temp_CassetteDrops TABLE 
	        (
	            Cassette_ID INT,
	            Denom FLOAT,
	            MeterBalance DECIMAL(18, 2),
	            VaultBalance DECIMAL(18, 2),
	            DeclaredBalance DECIMAL(18, 2),
	            AuditBalance DECIMAL(18, 2),
	            FillAmount DECIMAL(18, 2),
	            BleedAmount DECIMAL(18, 2),
	            AdjustmentAmount DECIMAL(18, 2),
	            dtCreated DATETIME,
	            dtUpdated DATETIME,
	            AudtiDate DATETIME
	        )		
	
	DECLARE @temp_Drop TABLE 
	        (
	            [Device_ID] [int] NOT NULL,
	            [OpeningBalance] [decimal](15, 2) NULL,
	            [FillAmount] [decimal](15, 2) NULL,
	            [BleedAmount] [decimal](15, 2) NULL,
	            [AdjustmentAmount] [decimal](15, 2) NULL,
	            [Meter_Out] [decimal](15, 2) NULL,
	            [Vault_Out] [decimal](15, 2) NULL,
	            [Meter_Balance] [decimal](15, 2) NULL,
	            [Vault_Balance] [decimal](15, 2) NULL,
	            [Declared_Balance] [decimal](15, 2) NULL,
	            [IsDropComplete] [bit] NULL,
	            [IsDeclared] [bit] NULL,
	            [IsFrozen] [bit] NULL,
	            [CreatedDate] [datetime] NULL,
	            [CreateUser] [int] NULL,
	            [DropCompleteDate] [datetime] NULL,
	            [DropCompleteUser] [int] NULL,
	            [ModifiedDate] [datetime] NULL,
	            [ModifiedUser] [int] NULL,
	            [FrozenDate] [datetime] NULL,
	            [FrozeUser] [int] NULL,
	            [AuditDate] [datetime] NULL,
	            [AuditUser] [int] NULL,
	            [Site_Drop_Ref] [bigint] NULL,
	            [Site_ID] [int] NULL,
	            AuditNote VARCHAR(500),
	            Meter_jackpot [decimal](15, 2) NULL,
	            Meter_Handpay [decimal](15, 2) NULL,
	            Meter_Voucher [decimal](15, 2) NULL,
	            IsVaultWebServiceEnabled BIT,
	            IsFinalDrop BIT,
	            Installation_No INT
	        ) 
	--1.Parse the input xml      
	INSERT INTO @temp_Drop
	  (
	    --Drop_ID -- AUTO GENERATED       
	    Device_ID,
	    OpeningBalance,
	    FillAmount,
	    BleedAmount,
	    AdjustmentAmount,
	    Meter_Out,
	    Vault_Out,
	    Meter_Balance,
	    Vault_Balance,
	    Declared_Balance,
	    IsDropComplete,
	    IsDeclared,
	    IsFrozen,
	    CreatedDate,
	    CreateUser,
	    DropCompleteDate,
	    DropCompleteUser,
	    ModifiedDate,
	    ModifiedUser,
	    FrozenDate,
	    FrozeUser,
	    AuditDate,
	    AuditUser,
	    Site_Drop_Ref,
	    Site_ID,
	    AuditNote,
	    Meter_jackpot,
	    Meter_Handpay,
	    Meter_Voucher,
	    IsVaultWebServiceEnabled,
	    IsFinalDrop,
	    Installation_No
	  )
	SELECT temp.HQ_Vault_ID,
	       temp.OpeningBalance,
	       temp.FillAmount,
	       temp.BleedAmount,
	       temp.AdjustmentAmount,
	       temp.Meter_Out,
	       temp.Vault_Out,
	       temp.Meter_Balance,
	       temp.Vault_Balance,
	       temp.Declared_Balance,
	       temp.IsDropComplete,
	       temp.IsDeclared,
	       temp.IsFrozen,
	       temp.CreatedDate,
	       temp.CreateUser,
	       temp.DropCompleteDate,
	       temp.DropCompleteUser,
	       temp.ModifiedDate,
	       temp.ModifiedUser,
	       temp.FrozenDate,
	       temp.FrozeUser,
	       temp.AuditDate,
	       temp.AuditUser,
	       temp.Site_Drop_Ref,
	       s.Site_ID,
	       temp.AuditNote,
	       temp.Meter_jackpot,
	       temp.Meter_Handpay,
	       temp.Meter_Voucher,
	       temp.IsVaultWebServiceEnabled,
		   ISNULL(temp.IsFinalDrop,0),
		   ISNULL(temp.Installation_No ,(SELECT TOP 1 @NGAInstallation_no
										FROM tNGAInstallations t
										INNER JOIN tVault_Devices  td
										ON t.NGADevice_ID=td.NGADevice_ID
									WHERE td.Vault_ID= vault_id AND t.End_Date IS NULL))
	FROM   (
	           SELECT HQ_Vault_ID,
	                  OpeningBalance,
	                  FillAmount,
	                  BleedAmount,
	                  AdjustmentAmount,
	                  Meter_Out,
	                  Vault_Out,
	                  Meter_Balance,
	                  Vault_Balance,
	                  Declared_Balance,
	                  IsDropComplete,
	                  IsDeclared,
	                  IsFrozen,
	                  CreatedDate,
	                  CreateUser,
	                  DropCompleteDate,
	                  DropCompleteUser,
	                  ModifiedDate,
	                  ModifiedUser,
	                  FrozenDate,
	                  FrozeUser,
	                  AuditDate,
	                  AuditUser,
	                  Site_Drop_Ref,
	                  Site_Code,
	                  AuditNote,
	                  Meter_jackpot,
	                  Meter_Handpay,
	                  Meter_Voucher,
	                  IsVaultWebServiceEnabled,
	                  IsFinalDrop,
	                  Installation_No
	           FROM   OPENXML(@dochandle, 'DROP', 2) 
	                  WITH 
	                  (
	                      Site_Drop_Ref BIGINT 'Drop_ID',
	                      OpeningBalance DECIMAL(15, 2) 'OpeningBalance',
	                      FillAmount DECIMAL(15, 2) 'FillAmount',
	                      BleedAmount DECIMAL(15, 2) 'BleedAmount',
	                      AdjustmentAmount DECIMAL(15, 2) 'AdjustmentAmount',
	                      Meter_Out DECIMAL(15, 2) 'Meter_Out',
	                      Vault_Out DECIMAL(15, 2) 'Vault_Out',
	                      Meter_Balance DECIMAL(15, 2) 'Meter_Balance',
	                      Vault_Balance DECIMAL(15, 2) 'Vault_Balance',
	                      Declared_Balance DECIMAL(15, 2) 'Declared_Balance',
	                      IsDropComplete BIT 'IsDropComplete',
	                      IsDeclared BIT 'IsDeclared',
	                      IsFrozen BIT 'IsFrozen',
	                      CreatedDate DATETIME 'CreatedDate',
	                      CreateUser INT 'CreateUser',
	                      DropCompleteDate DATETIME 'DropCompleteDate',
	                      DropCompleteUser INT 'DropCompleteUser',
	                      ModifiedDate DATETIME 'ModifiedDate',
	                      ModifiedUser INT 'ModifiedUser',
	                      FrozenDate DATETIME 'FrozenDate',
	                      FrozeUser INT 'FrozeUser',
	                      AuditDate DATETIME 'AuditDate',
	                      AuditUser INT 'AuditUser',
	                      HQ_Vault_ID INT 'HQ_Vault_ID',
	                      Site_Code VARCHAR(10) 'Site_Code',
	                      AuditNote VARCHAR(500) 'AuditNote',
	                      Meter_jackpot DECIMAL(15, 2) 'Meter_jackpot',
	                      Meter_Handpay DECIMAL(15, 2) 'Meter_Handpay',
	                      Meter_Voucher DECIMAL(15, 2) 'Meter_Voucher',
	                      IsVaultWebServiceEnabled BIT 
	                      'IsVaultWebServiceEnabled',
	                      IsFinalDrop BIT 'IsFinalDrop',
	                      Installation_No INT 'Installation_No'
	                  )
	       )temp
	       INNER JOIN SITE s WITH (NOLOCK)
	            ON  s.Site_Code = temp.Site_Code      
	
	SELECT @IsWebserviceEnabled = IsWebServiceEnabled
	FROM   tVault_Devices td WITH(NOLOCK)
	       INNER JOIN @temp_Drop d
	            ON  td.Vault_ID = d.Device_ID
	
	INSERT INTO @temp_CassetteDrops
	  (
	    Cassette_ID,
	    Denom,
	    MeterBalance,
	    VaultBalance,
	    DeclaredBalance,
	    AuditBalance,
	    FillAmount,
	    BleedAmount,
	    AdjustmentAmount,
	    dtCreated,
	    dtUpdated,
	    AudtiDate
	  )
	SELECT *
	FROM   OPENXML(@dochandle, 'DROP/Cassettes/Cassette', 2) 
	       WITH 
	       (
	           Cassette_ID INT '@HQ_Cassette_ID',
	           Denom FLOAT '@Denom',
	           MeterBalance DECIMAL(18, 2) '@MeterBalance',
	           VaultBalance DECIMAL(18, 2) '@VaultBalance',
	           DeclaredBalance DECIMAL(18, 2) '@DeclaredBalance',
	           AuditBalance DECIMAL(18, 2) '@AuditBalance',
	           FillAmount DECIMAL(18, 2) '@FillAmount',
	           BleedAmount DECIMAL(18, 2) '@BleedAmount',
	           AdjustmentAmount DECIMAL(18, 2) '@AdjustmentAmount',
	           dtCreated DATETIME '@dtCreated',
	           dtUpdated DATETIME '@dtUpdated',
	           AudtiDate DATETIME '@AudtiDate'
	       )temp    
	
	
	
	
	EXEC sp_xml_removedocument @dochandle      
	
	DECLARE @tempDropID BIGINT 
	--2. Check if the drop record has been already inserted      
	SELECT @tempDropID = d.Drop_ID
	FROM   tvault_drops d WITH(NOLOCK)
	       INNER JOIN @temp_Drop t
	            ON  d.Site_ID = t.Site_ID
	            AND d.site_drop_ref = t.site_drop_ref 
	
	--3. Insert if new record      
	IF @tempDropID IS NULL
	BEGIN
	    INSERT INTO tvault_drops
	      (
	        Device_ID,
	        OpeningBalance,
	        FillAmount,
	        BleedAmount,
	        AdjustmentAmount,
	        Meter_Out,
	        Vault_Out,
	        Meter_Balance,
	        Vault_Balance,
	        Declared_Balance,
	        IsDropComplete,
	        IsDeclared,
	        IsFrozen,
	        CreatedDate,
	        CreateUser,
	        DropCompleteDate,
	        DropCompleteUser,
	        ModifiedDate,
	        ModifiedUser,
	        FrozenDate,
	        FrozeUser,
	        AuditDate,
	        AuditUser,
	        Site_Drop_Ref,
	        Site_ID,
	        AuditNote,
	        Meter_jackpot,
	        Meter_Handpay,
	        Meter_Voucher,
	        IsVaultWebServiceEnabled,
	        IsFinalDrop,
	        Installation_No
	      )
	    SELECT Device_ID,
	           OpeningBalance,
	           FillAmount,
	           BleedAmount,
	           AdjustmentAmount,
	           Meter_Out,
	           Vault_Out,
	           Meter_Balance,
	           Vault_Balance,
	           Declared_Balance,
	           IsDropComplete,
	           IsDeclared,
	           IsFrozen,
	           CreatedDate,
	           CreateUser,
	           DropCompleteDate,
	           DropCompleteUser,
	           ModifiedDate,
	           ModifiedUser,
	           FrozenDate,
	           FrozeUser,
	           AuditDate,
	           AuditUser,
	           Site_Drop_Ref,
	           Site_ID,
	           AuditNote,
	           Meter_jackpot,
	           Meter_Handpay,
	           Meter_Voucher,
	           COALESCE(IsVaultWebServiceEnabled, @IsWebserviceEnabled, 0),
	           ISNULL(IsFinalDrop,0),
			   ISNULL(Installation_No ,(SELECT TOP 1 @NGAInstallation_no
										FROM tNGAInstallations t
										INNER JOIN tVault_Devices  td
										ON t.NGADevice_ID=td.NGADevice_ID
									WHERE td.Vault_ID= vault_id AND t.End_Date IS NULL))
	    FROM   @temp_Drop    
	    
	    IF @@Error <> 0
	    BEGIN
	        SET @IsSuccess = -1 -- failed while updating the records in the VaultBalance table    
	        GOTO ERR_HANDLE
	    END      
	    
	    SET @tempDropID = SCOPE_IDENTITY()
	    INSERT INTO tVault_CassetteDrops
	      (
	        Drop_ID,
	        Cassette_ID,
	        Denom,
	        MeterBalance,
	        VaultBalance,
	        DeclaredBalance,
	        AuditBalance,
	        FillAmount,
	        BleedAmount,
	        AdjustmentAmount,
	        dtCreated,
	        dtUpdated,
	        AudtiDate
	      )
	    SELECT @tempDropID,
	           Cassette_ID,
	           Denom,
	           MeterBalance,
	           VaultBalance,
	           DeclaredBalance,
	           AuditBalance,
	           FillAmount,
	           BleedAmount,
	           AdjustmentAmount,
	           dtCreated,
	           dtUpdated,
	           AudtiDate
	    FROM   @temp_CassetteDrops
	    
	    IF @@Error <> 0
	    BEGIN
	        SET @IsSuccess = -1 -- failed while updating the records in the VaultBalance table    
	        GOTO ERR_HANDLE
	    END   
	    
	    SELECT @IsFinalDrop = IsFinalDrop
	    FROM   @temp_Drop
	    
	    IF (ISNULL(@IsFinalDrop, 0) = 1)
	    BEGIN
	        UPDATE TN
	        SET    TN.End_Date = temp.DropCompleteDate,
	               TN.End_User = temp.DropCompleteUser
	        FROM   @temp_Drop temp
	               INNER JOIN tVault_Devices tvd
	                    ON  tvd.Vault_ID = temp.Device_ID
	               INNER JOIN tNGAInstallations TN
	                    ON  TN.NGADevice_ID = tvd.NGADevice_ID
	        WHERE  tn.End_Date IS NULL
	        
	      UPDATE  tvd
	      SET   tvd.Active = 0,
				site_id = 0
	      FROM   @temp_Drop temp
	      INNER JOIN tVault_Devices tvd
	      ON  tvd.Vault_ID = temp.Device_ID
			   
	    
	    END
	END
	ELSE
	BEGIN
	    --4. Update if already exists      
	    UPDATE td
	    SET    td.Device_ID = temp.Device_ID,
	           td.OpeningBalance = temp.OpeningBalance,
	           td.FillAmount = temp.FillAmount,
	           td.BleedAmount = temp.BleedAmount,
	           td.AdjustmentAmount = temp.AdjustmentAmount,
	           td.Meter_Out = temp.Meter_Out,
	           td.Vault_Out = temp.Vault_Out,
	           td.Meter_Balance = temp.Meter_Balance,
	           td.Vault_Balance = temp.Vault_Balance,
	           td.Declared_Balance = temp.Declared_Balance,
	           td.IsDropComplete = temp.IsDropComplete,
	           td.IsDeclared = temp.IsDeclared,
	           td.IsFrozen = temp.IsFrozen,
	           td.CreatedDate = temp.CreatedDate,
	           td.CreateUser = temp.CreateUser,
	           td.DropCompleteDate = temp.DropCompleteDate,
	           td.DropCompleteUser = temp.DropCompleteUser,
	           td.ModifiedDate = temp.ModifiedDate,
	           td.ModifiedUser = temp.ModifiedUser,
	           td.FrozenDate = temp.FrozenDate,
	           td.FrozeUser = temp.FrozeUser,
	           td.AuditDate = temp.AuditDate,
	           td.AuditUser = temp.AuditUser,
	           td.Site_Drop_Ref = temp.Site_Drop_Ref,
	           td.Site_ID = temp.Site_ID,
	           td.AuditNote = temp.AuditNote,
	           td.Meter_jackpot = temp.Meter_jackpot,
	           td.Meter_Handpay = temp.Meter_Handpay,
	           td.Meter_Voucher = temp.Meter_Voucher,
	           td.IsVaultWebServiceEnabled = COALESCE(temp.IsVaultWebServiceEnabled, @IsWebserviceEnabled, 0),
	           td.IsFinalDrop = temp.IsFinalDrop,
	           td.Installation_No=temp.Installation_No
	    FROM   tvault_drops td
	           INNER JOIN @temp_Drop temp
	                ON  td. Site_Drop_Ref = temp. Site_Drop_Ref
	    WHERE  td.drop_id = @tempDropID    
	    
	    IF @@Error <> 0
	    BEGIN
	        SET @IsSuccess = -1 -- failed while updating the records in the VaultBalance table    
	        GOTO ERR_HANDLE
	    END  
	    
	    UPDATE TC
	    SET    TC. Denom = td.Denom,
	           TC. MeterBalance = td.MeterBalance,
	           TC. VaultBalance = td.VaultBalance,
	           TC. DeclaredBalance = td.DeclaredBalance,
	           TC. AuditBalance = td.AuditBalance,
	           TC. FillAmount = td.FillAmount,
	           TC. BleedAmount = td.BleedAmount,
	           TC. AdjustmentAmount = td. AdjustmentAmount,
	           TC. dtCreated = td.dtCreated,
	           TC. dtUpdated = td.dtUpdated,
	           TC. AudtiDate = td.AudtiDate
	    FROM   tVault_CassetteDrops TC
	           INNER JOIN @temp_CassetteDrops td
	                ON  TC.Cassette_ID = td.Cassette_ID
	    WHERE  TC.Drop_ID = @tempDropID
	    
	    IF @@Error <> 0
	    BEGIN
	        SET @IsSuccess = -1 -- failed while updating the records in the VaultBalance table    
	        GOTO ERR_HANDLE
	    END
	    
	    SELECT @IsFinalDrop = IsFinalDrop
	    FROM   @temp_Drop
	    
	    IF (ISNULL(@IsFinalDrop, 0) = 1)
	    BEGIN
	        UPDATE TN
	        SET    TN.End_Date = temp.DropCompleteDate,
	               TN.End_User = temp.DropCompleteUser
	        FROM   @temp_Drop temp
	               INNER JOIN tNGAInstallations TN
	                    ON  TN.Installation_No = temp.Installation_No
	        
	        IF @@Error <> 0
	        BEGIN
	            SET @IsSuccess = -1 -- failed while updating the records in the VaultBalance table    
	            GOTO ERR_HANDLE
	        END
	        
	        UPDATE tvd
	        SET    tvd.Active = 0,
	               site_id = 0
	        FROM   @temp_Drop temp
	               INNER JOIN tVault_Devices tvd
	                    ON  tvd.Vault_ID = temp.Device_ID
	        
	        IF @@Error <> 0
	        BEGIN
	            SET @IsSuccess = -1 -- failed while updating the records in the VaultBalance table    
	            GOTO ERR_HANDLE
	        END
	    END
	END 
	
	COMMIT TRAN 
	RETURN 
	
	--ERR HANDLE 
	ERR_HANDLE:
	SET @IsSuccess = -1 -- failed while updating the records in the VaultBalance table    
	ROLLBACK TRAN
END
GO
