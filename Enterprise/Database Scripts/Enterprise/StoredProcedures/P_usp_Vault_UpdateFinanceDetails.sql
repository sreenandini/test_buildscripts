USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'usp_Vault_UpdateFinanceDetails'
   )
    DROP PROCEDURE dbo.usp_Vault_UpdateFinanceDetails
GO

CREATE PROCEDURE dbo.usp_Vault_UpdateFinanceDetails
	@Vault_ID INT,
	@PurchasePrice DECIMAL(18, 2) = NULL,
	@PurchaseInvoice VARCHAR(50) = NULL,
	@PurchaseDate DATETIME = NULL,
	@depreciationDate DATETIME = NULL,
	@SoldPrice DECIMAL(18, 2) = NULL,
	@SoldInvoice VARCHAR(50) = NULL,
	@SoldDate DATETIME = NULL,
	 --Audit details 
	 /*------------------------*/
	@User_ID INT,
	@Module_ID INT,
	@Module_Name VARCHAR(150),
	@Screen_Name VARCHAR(150)
	 /*------------------------*/
AS
	/*****************************************************************************************************
DESCRIPTION	: PROC Description  
CREATED DATE: 26-aug-2012
MODULE		: PROC used in vault creation screen 
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR					DESCRIPTON										MODIFIED DATE
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/
BEGIN
BEGIN TRAN
	--AUDIT DETAILS  
	DECLARE @Audit       TABLE
	        (
	            --old Values
	            old_PurchasePrice DECIMAL(18, 2),
	            old_PurchaseInvoice VARCHAR(50),
	            old_PurchaseDate DATETIME,
	            old_depreciationDate DATETIME,
	            old_SoldPrice DECIMAL(18, 2),
	            old_SoldInvoice VARCHAR(50),
	            old_SoldDate DATETIME,
	            --new Values
	            new_PurchasePrice DECIMAL(18, 2),
	            new_PurchaseInvoice VARCHAR(50),
	            new_PurchaseDate DATETIME,
	            new_depreciationDate DATETIME,
	            new_SoldPrice DECIMAL(18, 2),
	            new_SoldInvoice VARCHAR(50),
	            new_SoldDate DATETIME
	        )
	
	DECLARE @staff_id    INT  
	DECLARE @staff_Name  VARCHAR(100) 
	DECLARE @Vault_Name VARCHAR(200)
	
	
	--UPDATE VAULT DETAILS 
	UPDATE tVault_Devices
	SET    PurchasePrice = @PurchasePrice,
	       PurchaseInvoice = @PurchaseInvoice,
	       PurchaseDate = @PurchaseDate,
	       depreciationDate = @depreciationDate,
	       SoldPrice = @SoldPrice,
	       SoldInvoice = @SoldInvoice,
	       SoldDate = @SoldDate
		   OUTPUT DELETED.PurchasePrice,
				DELETED.PurchaseInvoice,
				DELETED.PurchaseDate,
				DELETED.depreciationDate,
				DELETED.SoldPrice,
				DELETED.SoldInvoice,
			    DELETED.SoldDate ,		     
			   INSERTED.PurchasePrice,	           
			   INSERTED.PurchaseInvoice,	          
			   INSERTED.PurchaseDate,	           
			   INSERTED.depreciationDate,	          
			   INSERTED.SoldPrice,	           
			   INSERTED.SoldInvoice ,	           
			   INSERTED.SoldDate 	          	           
	           INTO @Audit
	WHERE  Vault_ID = @Vault_ID
	
	IF @@ERROR <> 0
	    GOTO err
	
	
	--AUDIT ANY CHANGES------------------------------------------------------------------------------------ 
	SET @staff_id = @User_ID -- CHANGED FROM USER ID TO STAFFID FROM CLIENT 
	
	SELECT  @Vault_Name = Name from dbo.tVault_Devices WHERE Vault_ID =  @Vault_ID 
	
	SELECT @staff_Name = ISNULL(Staff_First_Name, '')
	FROM   Staff
	WHERE  Staff_ID = @User_ID  
	
	
	DECLARE @Audit_DESC VARCHAR(MAX) 
	
	SELECT @Audit_DESC = 
	CAST((
	           CASE 
	                WHEN ISNULL(new_PurchasePrice, 0) <> ISNULL(old_PurchasePrice, 0) THEN 
	                     'Updated [PurchasePrice] from ' +
	                     cast(ISNULL(old_PurchasePrice, 0) as varchar(50)) + ' TO  ' +
	                     cast(new_PurchasePrice AS varchar(50))
	                ELSE ''
	           END
	       )AS VARCHAR(300))
	        + ' ' + CAST((
	           CASE 
	                WHEN ISNULL(New_PurchaseInvoice, '') <> ISNULL(old_PurchaseInvoice, '') THEN 
	                     ' Updated [PurchaseInvoice]  from ' + cast(ISNULL(old_PurchaseInvoice, '') AS varchar(50))
	                     + ' TO  ' + cast(New_PurchaseInvoice AS varchar(50))
	                ELSE ''
	           END
	       )AS VARCHAR(300)) + ' ' + CAST((
	           CASE 
	                WHEN ISNULL(New_PurchaseDate, '') <> ISNULL(old_PurchaseDate, '') THEN 
	                     ' Updated [PurchaseDate]  from ' + cast(ISNULL(old_PurchaseDate, '')AS varchar(50))
	                     + ' TO  ' + cast(new_PurchaseDate AS varchar(50))
	                ELSE ''
	           END
	       )AS VARCHAR(300)) + ' ' + CAST((
	           CASE 
	                WHEN ISNULL(new_depreciationDate, '') <> ISNULL(old_depreciationDate, '') THEN 
	                     ' Updated [DepreciationDate]  from ' + cast(ISNULL(old_depreciationDate, '')AS varchar(50))
	                     + ' TO  ' + CAST( new_depreciationDate AS varchar(50))
	                ELSE ''
	           END
	       )AS VARCHAR(300)) + ' ' + CAST((
	           CASE 
	                WHEN ISNULL(new_SoldPrice, 0) <> ISNULL(old_SoldPrice, 0) THEN 
	                     ' Updated [SoldPrice]  from ' + cast(ISNULL(old_SoldPrice, 0) AS varchar(50))
	                     + ' TO  ' + cast(new_SoldPrice AS varchar(50))
	                ELSE ''
	           END
	       )AS VARCHAR(300)) + ' ' + CAST((
	           CASE 
	                WHEN ISNULL(new_SoldInvoice, '') <> ISNULL(old_SoldInvoice, '') THEN 
	                     ' Updated [SoldInvoice]  from ' + cast(ISNULL(old_SoldInvoice, '') AS varchar(50))
	                     + ' TO  ' + cast(new_SoldInvoice AS varchar(50))
	                ELSE ''
	           END
	       )AS VARCHAR(300))
	       + ' ' + CAST((
	           CASE 
	                WHEN ISNULL(new_SoldDate, '') <> ISNULL(old_SoldDate, '') THEN 
	                     ' Updated [SoldInvoice]  from ' + cast(ISNULL(old_SoldDate, '') AS varchar(50))
	                     + ' TO  ' + cast(new_SoldDate AS varchar(50))
	                ELSE ''
	           END
	       )AS VARCHAR(300))
	FROM   @Audit   
	
	
	
	IF (@Audit_DESC <> '')
	BEGIN
	    SET @Audit_DESC ='[VAULT_ID] =' + CAST(@Vault_ID AS VARCHAR(10))  +' ' +  ' [VAULT_NAME] =' + @Vault_Name 
	        +
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
	         'MODIFY'
	    
	    IF @@ERROR <> 0
	        GOTO err
	END
	
	SELECT @Vault_ID AS Vault_ID
	
	COMMIT TRAN
	RETURN 
	
	err: 
	ROLLBACK TRAN
END
GO

