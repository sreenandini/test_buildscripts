USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'usp_Vault_ImportTransactionEvent'
   )
    DROP PROCEDURE dbo.usp_Vault_ImportTransactionEvent
GO

CREATE PROCEDURE dbo.usp_Vault_ImportTransactionEvent
	@Xml VARCHAR(MAX),
	@IsSuccess INT OUTPUT
AS
	/*****************************************************************************************************    
DESCRIPTION : PROC Description      
CREATED DATE: PROC CreateDate    
MODULE  : PROC used in Modules     
CHANGE HISTORY :    
------------------------------------------------------------------------------------------------------    
AUTHOR     DESCRIPTON          MODIFIED DATE    
------------------------------------------------------------------------------------------------------    

*****************************************************************************************************/    


BEGIN
	DECLARE @docHandle INT    
	SET @IsSuccess = 0             
	EXEC sp_xml_preparedocument @docHandle OUTPUT,
	     @Xml       
	
	
	DECLARE @tempEvent TABLE 
	        (
	            Site_Event_ID BIGINT,
	            Amount DECIMAL(18, 2),
	            TransactionEvent_Type INT,
	            Transaction_Ref BIGINT,
	            CreatedDate DATETIME,
	            Site_Drop_ref BIGINT,
	            Event_Detail VARCHAR(100),
	            Printed_Asset VARCHAR(100),
	            Print_Date DATETIME,
	            Redeem_Date DATETIME,
	            Expired_Date DATETIME,
	            Site_Id INT,
	            Vault_ID INT
	        )    
	
	INSERT INTO @tempEvent
	  (
	    Site_Event_ID,
	    Amount,
	    TransactionEvent_Type,
	    Transaction_Ref,
	    CreatedDate,
	    Site_Drop_ref,
	    Event_Detail,
	    Printed_Asset,
	    Print_Date,
	    Redeem_Date,
	    Expired_Date,
	    Site_Id,
	    Vault_ID
	  )
	SELECT temp.TransactionEvent_ID,
	       temp.Amount,
	       temp.TransactionEvent_Type,
	       temp.Transaction_Ref,
	       temp.CreatedDate,
	       temp.Drop_ID,
	       temp.Event_Detail,
	       temp.Printed_Asset,
	       temp.Print_Date,
	       temp.Redeem_Date,
	       temp.Expired_Date,
	       s.Site_ID,
	       isnull(Vault_ID,
	       (
				SELECT TOP 1 vault_id
				FROM   tVault_Devices tx   WITH(NOLOCK)
					   INNER JOIN tNGADevices td   WITH(NOLOCK)
							ON  tx.NGADevice_ID = td.NGADevice_ID
					   INNER JOIN tNGAInstallations tn   WITH(NOLOCK)
						ON  tn.NGADevice_ID=td.ngaDevice_ID 
				WHERE tn.Site_ID =s.Site_ID
				ORDER BY tn.Start_Date DESC  ))
		  FROM   OPENXML(@dochandle, 'TransactionEvent', 2) 
	       WITH 
	       (
	           TransactionEvent_ID BIGINT 'TransactionEvent_ID',
	           Amount DECIMAL(18, 2) 'Amount',
	           TransactionEvent_Type INT 'TransactionEvent_Type',
	           Transaction_Ref BIGINT 'Transaction_Ref',
	           CreatedDate DATETIME 'CreatedDate',
	           Drop_ID BIGINT 'Drop_ID',
	           Event_Detail VARCHAR(100) 'Event_Detail',
	           Printed_Asset VARCHAR(100) 'Printed_Asset',
	           Print_Date DATETIME 'Print_Date',
	           Redeem_Date DATETIME 'Redeem_Date',
	           Expired_Date DATETIME 'Expired_Date',
	           SiteCode VARCHAR(10) 'SiteCode',
	           Vault_ID INT 'Vault_ID'
	       ) temp
	       INNER JOIN dbo.[Site] s
	            ON  s.site_code = temp.SiteCode 
	
	--tVault_TransactionEvents    
	EXEC sp_xml_removedocument @dochandle    
	
	INSERT INTO tVault_Transactionevents
	  (
	    Site_Event_ID,
	    Amount,
	    TransactionEvent_Type,
	    Transaction_Ref,
	    CreatedDate,
	    Site_Drop_ref,
	    Event_Detail,
	    Printed_Asset,
	    Print_Date,
	    Redeem_Date,
	    Expired_Date,
	    Site_Id,
	    Vault_ID
	  )
	SELECT Site_Event_ID,
	       Amount,
	       TransactionEvent_Type,
	       Transaction_Ref,
	       CreatedDate,
	       Site_Drop_ref,
	       Event_Detail,
	       Printed_Asset,
	       Print_Date,
	       Redeem_Date,
	       Expired_Date,
	       Site_Id,
	       Vault_ID
	FROM   @tempEvent    
	
	IF @@Error <> 0
	BEGIN
	    SET @IsSuccess = -1 -- failed while updating the records in the VaultBalance table
	END
END
GO
