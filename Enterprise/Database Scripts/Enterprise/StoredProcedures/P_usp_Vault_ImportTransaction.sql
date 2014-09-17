USE Enterprise
GO 
--Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'usp_Vault_ImportTransaction'
   )
    DROP PROCEDURE dbo.usp_Vault_ImportTransaction
GO


CREATE PROCEDURE dbo.usp_Vault_ImportTransaction
	@xml XML = NULL ,
	@IsSuccess INT OUTPUT
AS
	/*****************************************************************************************************        
DESCRIPTION : PROC Description          
CREATED DATE: 10-July-2013        
MODULE  : Insert exchange drop xml into enterprise          
CHANGE HISTORY :        
----------------------------------------------------------------------------------------------------        
AUTHOR     DESCRIPTON          MODIFIED DATE        
----------------------------------------------------------------------------------------------------        

*****************************************************************************************************/        

BEGIN
	SET NOCOUNT ON         
	
	DECLARE @Transaction_StandardFillID  INT 
	DECLARE @docHandle                   INT  
	DECLARE @Vault_ID                    INT  
	SET @IsSuccess = 0                
	EXEC sp_xml_preparedocument @docHandle OUTPUT,
	     @xml 
	
	-- Insert exchange drop xml into enterprise   
	
	SELECT @Transaction_StandardFillID = tvtt.[Type_ID]
	FROM   tVault_Transaction_Type tvtt
	WHERE  tvtt.Type_Description = 'STANDARDFILL' 
	--Insert exchange drop xml into enterprise   
	
	DECLARE @temp                 TABLE 
	        (
	            [Site_Transaction_ID] [bigint],
	            [Device_ID] [int] NULL,
	            [TransactionsAmount] [decimal](18, 2),
	            [TotalAmountOnTransaction] [decimal](18, 2),
	            [VaultTotalAmountOnTransaction] DECIMAL(18, 2),
	            [CurrentBalance] [decimal](18, 2),
	            [Vault_Balance] [DECIMAL](18, 2),
	            [CreatedDate] [datetime],
	            [CreateUser] [int],
	            [Type] [INT],
	            [Reason_ID] [INT],
	            Site_Drop_ref [BIGINT],
	            site_id [INT]
	        ) 
	
	DECLARE @CassetteTransaction  TABLE 
	        (
	            Site_Transaction_ID BIGINT,
	            Cassette_ID INT,
	            Denom FLOAT,
	            FillAmount DECIMAL(18, 2),
	            AmountOnFill DECIMAL(18, 2),
	            CurrentBalance DECIMAL(18, 2),
	            dtCreated DATETIME,
	            dtUpdated DATETIME
	        )
	
	--1.Parse the input xml  
	INSERT INTO @temp
	  (
	    --Drop_ID -- AUTO GENERATED   
	    Site_Transaction_ID,
	    [Device_ID],
	    [TransactionsAmount],
	    [TotalAmountOnTransaction],
	    [VaultTotalAmountOnTransaction],
	    [CurrentBalance],
	    [Vault_Balance],
	    [CreatedDate],
	    [CreateUser],
	    [Type],
	    [Reason_ID],
	    Site_Drop_ref,
	    site_id
	  )
	SELECT Site_Transaction_ID,
	       [Device_ID],
	       [TransactionsAmount],
	       [TotalAmountOnTransaction],
	       [VaultTotalAmountOnFill],
	       [CurrentBalance],
	       [Vault_Balance],
	       [CreatedDate],
	       [CreateUser],
	       [Type],
	       [Reason_ID],
	       Site_Drop_ref,
	       s.site_id
	FROM   OPENXML(@dochandle, 'Vault_Transaction', 2) 
	       WITH 
	       (
	           Site_Transaction_ID BIGINT 'Transaction_Id',
	           [Device_ID] INT 'HQ_Vault_ID',
	           [TransactionsAmount] DECIMAL(18, 2) 
	           'TransactionsAmount',
	           [TotalAmountOnTransaction] DECIMAL(18, 2)
	           'TotalAmountOnTransaction',
	           [VaultTotalAmountOnFill] DECIMAL(18, 2)
	           'VaultTotalAmountOnFill',
	           [CurrentBalance] DECIMAL(18, 2) 'CurrentBalance',
	           [Vault_Balance] DECIMAL(18, 2) 'Vault_Balance',
	           [CreatedDate] DATETIME 'CreatedDate',
	           [CreateUser] INT 'CreateUser',
	           [Type] INT 'TYPE',
	           [Reason_ID] INT 'Reason_ID',
	           Site_Drop_ref BIGINT 'Drop_ID',
	           Site_Code VARCHAR(10) 'Site_Code'
	       ) temp
	       INNER JOIN [Site] s WITH (NOLOCK)
	            ON  s.Site_Code = temp.Site_Code  
	
	SELECT @Vault_ID = t.[Device_ID] FROM @temp t
	
	INSERT INTO @CassetteTransaction
	SELECT Transaction_ID,
	       Cassette_ID,
	       Denom,
	       FillAmount,
	       AmountOnFill,
	       CurrentBalance,
	       dtCreated,
	       dtUpdated
	FROM   OPENXML(@dochandle, 'Vault_Transaction/Cassettes/Cassette', 2) 
	       WITH 
	       (
	           Transaction_ID BIGINT '@Transaction_ID',
	           Cassette_ID INT '@Cassette_ID',
	           Denom FLOAT '@Denom',
	           FillAmount DECIMAL(18, 2) '@FillAmount',
	           AmountOnFill DECIMAL(18, 2) '@AmountOnFill',
	           CurrentBalance DECIMAL(18, 2) '@CurrentBalance',
	           dtCreated DATETIME '@dtCreated',
	           dtUpdated DATETIME '@dtUpdated'
	       ) temp
	
	
	
	EXEC sp_xml_removedocument @dochandle
	
	--TO check if the record has been already inported
	DECLARE @tempTransaction_ID BIGINT
	
	
	--Use IDX_tvault_Transactions_Site_Transaction_ID_site_id Index
	SELECT @tempTransaction_ID = Transactions_ID
	FROM   tvault_transactions TT WITH(NOLOCK)
	       INNER JOIN @temp t
	            ON  tt.Site_Transaction_ID = t.Site_Transaction_ID
	            AND tt.site_id = t.site_id
	
	BEGIN TRAN 	
	
	IF @tempTransaction_ID IS NULL -- INSER A NEW RECORD
	BEGIN

	    INSERT INTO tVault_Transactions
	      (
	        Site_Transaction_ID,
	        Device_ID,
	        TransactionAmount,
	        TotalAmountOnTransaction,
	        [VaultTotalAmountOnTransaction],
	        CurrentBalance,
	        Vault_Balance,
	        CreatedDate,
	        CreateUser,
	        TYPE,
	        Reason_ID,
	        Site_Drop_ref,
	        site_id
	      )
	    SELECT Site_Transaction_ID,
	           [Device_ID],
	           [TransactionsAmount],
	           [TotalAmountOnTransaction],
	           [VaultTotalAmountOnTransaction],
	           [CurrentBalance],
	           [Vault_Balance],
	           [CreatedDate],
	           [CreateUser],
	           [Type],
	           [Reason_ID],
	           Site_Drop_ref,
	           site_id
	    FROM   @temp
	    
	    IF @@Error <> 0
	        GOTO ERR
	    
	    SET @tempTransaction_ID = SCOPE_IDENTITY()
	    
	    
	    
	    INSERT INTO tVault_CassetteTransactions
	      (
	        Transaction_ID,
	        Site_Transaction_ID,
	        Cassette_ID,
	        Denom,
	        FillAmount,
	        AmountOnFill,
	        CurrentBalance,
	        dtCreated,
	        dtUpdated
	      )
	    SELECT @tempTransaction_ID,
	           Site_Transaction_ID,
	           Cassette_ID,
	           Denom,
	           FillAmount,
	           AmountOnFill,
	           CurrentBalance,
	           dtCreated,
	           dtUpdated
	    FROM   @CassetteTransaction
	    
	    IF @@Error <> 0
	        GOTO ERR
	END
	ELSE
	BEGIN
		
	    UPDATE vt
	    SET    vt.Site_Transaction_ID = t.Site_Transaction_ID,
	           vt.Device_ID = t.Device_ID,
	           vt.TransactionAmount = t.TransactionsAmount,
	           vt.TotalAmountOnTransaction = t.TotalAmountOnTransaction,
	           vt.[VaultTotalAmountOnTransaction] = t.[VaultTotalAmountOnTransaction],
	           vt.CurrentBalance = t.CurrentBalance,
	           vt.Vault_Balance = t.Vault_Balance,
	           vt.CreatedDate = t.CreatedDate,
	           vt.CreateUser = t.CreateUser,
	           vt.TYPE = t.TYPE,
	           vt.Reason_ID = t.Reason_ID,
	           vt.Site_Drop_ref = t.Site_Drop_ref
	    FROM   tVault_Transactions vt,
	           @temp t
	    WHERE  vt.Transactions_ID = @tempTransaction_ID
	    
	    IF @@Error <> 0
	        GOTO ERR
	    
	    
	    UPDATE tc
	    SET    tc.Site_Transaction_ID = temp.Site_Transaction_ID,
	           tc.Cassette_ID = temp.Cassette_ID,
	           tc.Denom = temp.Denom,
	           tc.FillAmount = temp.FillAmount,
	           tc.AmountOnFill = temp.AmountOnFill,
	           tc.CurrentBalance = temp.CurrentBalance,
	           tc.dtCreated = temp.dtCreated,
	           tc.dtUpdated = temp.dtUpdated
	    FROM   tVault_CassetteTransactions tc
	           INNER JOIN @CassetteTransaction temp
	                ON  tc.Cassette_ID = temp.Cassette_ID AND  tc.Transaction_ID =@tempTransaction_ID
	    
	    IF @@Error <> 0
	        GOTO ERR
	END      
	
	
	UPDATE tc
	SET    tc.denom = temp.denom
	FROM   tvault_cassettes tc
	       INNER JOIN @CassetteTransaction temp
	            ON  tc.Cassette_ID = temp.Cassette_ID
	WHERE  tc.Denom <> temp.Denom
	IF @@Error <> 0
	    GOTO ERR
	
	--FOR STANDARD FILL 
	IF EXISTS (SELECT '' FROM @temp WHERE [TYPE]=@Transaction_StandardFillID)
	BEGIN
	    IF EXISTS(
	           SELECT ''
	           FROM   tVault_Devices td
	           WHERE  td.IsWebServiceEnabled = 1
	                  AND td.Vault_ID = @Vault_ID
	       )
	    BEGIN
	        UPDATE tc
	        SET    tc.StandardFillAmount = temp.FillAmount
	        FROM   tvault_cassettes tc
	               INNER JOIN @CassetteTransaction temp
	                    ON  tc.Cassette_ID = temp.Cassette_ID
	               inner join tVault_CassetteTypes tt
	               on tc.Type= tt.CassetteType_ID    
	        WHERE  tc.StandardFillAmount <> temp.FillAmount
					AND tt.CassetteType_Name<>'Rejection'
	        
	        DECLARE @VaultCassetteStandardFillAmount  DECIMAL(18, 2)  
	        DECLARE @VaultHopperStandardFillAmount    DECIMAL(18, 2)
	        
	       
	        SELECT @VaultCassetteStandardFillAmount = SUM(StandardFillAmount)
	        FROM   tvault_cassettes TC
			inner join tVault_CassetteTypes tt
	               on tc.Type= tt.CassetteType_ID 
	        WHERE  vault_id = @Vault_ID
	               AND ISActive = 1
	               AND tt.CassetteType_Name='Cassette'
	                
	        
	        SELECT @VaultHopperStandardFillAmount = SUM(StandardFillAmount)
	        FROM   tvault_cassettes TC
	        inner join tVault_CassetteTypes tt
	               on tc.Type= tt.CassetteType_ID 
	        WHERE  vault_id = @Vault_ID
	               AND ISActive = 1
	                AND tt.CassetteType_Name='Hopper'
	        
	        UPDATE tvault_Devices
     		SET    StandardFillAmount = ISNULL(@VaultCassetteStandardFillAmount, 0.00) + ISNULL(@VaultHopperStandardFillAmount, 0.00)  
	        WHERE  vault_id = @Vault_ID
      
	    END
	END	
	IF @@Error <> 0
	    GOTO ERR
	
	
	SET @IsSuccess = 0 
	COMMIT TRAN 
	RETURN 
	ERR:
	SET @IsSuccess = -1 
	ROLLBACK TRAN
END
GO