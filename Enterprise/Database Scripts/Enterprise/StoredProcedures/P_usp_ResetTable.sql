USE [Enterprise]
GO
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'usp_ResetTable'
   )
    DROP PROCEDURE dbo.usp_ResetTable
GO

CREATE PROCEDURE dbo.usp_ResetTable
	@Mode_ID INT,
	@Site_Code VARCHAR(50),
	@Status INT OUTPUT
AS
--/*****************************************************************************************************
--DESCRIPTION    :	Flush all records of tables list in [Factory_Reset_List] table based on mode_id.
--					@Mode_ID:	1 - Master Reset
--								2 - Reset Initial Setting
--								3 - Delete Accounting Details
--CREATED DATE   : 
--MODULE		 : Called in usp_FactoryResetProcess for Fatory Reset
--Example		 : =============================================
--				   EXECUTE dbo.usp_ResetTable 3,'1002',@Status OUTPUT
--				   GO
--				   =============================================
--CHANGE HISTORY :
--------------------------------------------------------------------------------------------------------
--AUTHOR						MODIFIED DATE		DESCRIPTON
--------------------------------------------------------------------------------------------------------

--*****************************************************************************************************/
BEGIN
	SET NOCOUNT ON
	
	DECLARE @DBName         VARCHAR(50)
	DECLARE @TableName      VARCHAR(200)
	DECLARE @UniqueColumn   VARCHAR(200)
	DECLARE @RefTableName   VARCHAR(200)	
	DECLARE @RefColumn      VARCHAR(200)
	DECLARE @Qry            VARCHAR  (MAX)				 
	
	--Create a cursor for looping all records returned from udf_GetTableOrder
	DECLARE curTableDetail           CURSOR  
	FOR
	    SELECT FRL_DB,
	           FRL_Table,
	           FRL_UniqueColumn,
	           FRL_RefTable,
	           FRL_RefColumn         
	    FROM   udf_GetTableOrder(@Mode_ID)
		
	OPEN curTableDetail
	
	FETCH NEXT FROM curTableDetail INTO @DBName,@TableName,@UniqueColumn,@RefTableName,
	@RefColumn                                                                                               
	
	WHILE (@@FETCH_STATUS = 0)
	BEGIN
	    --Frames cleanup query for cleanups table
	    SET @Qry = 
	        'IF EXISTS ( SELECT 1 FROM   sys.objects WHERE  OBJECT_ID = OBJECT_ID(N''' + @TableName + ''') AND TYPE IN (N''U'')   )'
	    
	    --If reset mode is reset account information, only account infromation from import history table must be removed based on @Site_Code
	    IF @Mode_ID = 3 AND UPPER(@TableName) = 'IMPORT_HISTORY'
	    BEGIN
	        SET @Qry = @Qry + ' DELETE FROM [' + @TableName + '] WHERE IH_Type IN (''HOURLY'',''FUND'',''METER_HISTORY'',''DAILY'',''TREASURY'',''COLLECTIONDETAILS'',''METER_HISTORY'',''BATCHEXPCOMPLETE'',''VAULTBALANCE'',''LIQUIDATIONDETAILS'',''LIQUIDATIONSHAREDETAILS'') AND ' + @UniqueColumn +' IN (SELECT ' + @RefColumn + ' FROM Enterprise..[' + @RefTableName+ '] WHERE  Site_Code = ''' + @Site_Code + ''')'
	    END
	    
	    --If reset mode is not reset account information, remove all import history table must be removed based on @Site_Code
	    ELSE IF UPPER(@TableName) = 'IMPORT_HISTORY'
	    BEGIN
	    	SET @Qry = @Qry + ' DELETE FROM [' + @TableName + '] WHERE IH_Type <> ''FACTORYRESET'' AND ' + @UniqueColumn +' IN (SELECT ' + @RefColumn + ' FROM Enterprise..[' + @RefTableName+ '] WHERE  Site_Code = ''' + @Site_Code + ''')'
	    END
	    
	    --In COLLECTION_TICKET remove based on CT_Inserted_Installation_ID and CT_Printed_Installation_ID
	    ELSE IF UPPER(@TableName) = 'COLLECTION_TICKET'
	    BEGIN
	        SET @Qry = @Qry + ' DELETE FROM [' + @TableName + '] WHERE CT_Printed_Installation_ID IN (SELECT ' + @RefColumn + ' FROM [' + @RefTableName + '] WHERE  Site_Code = ''' + @Site_Code + ''') OR CT_Inserted_Installation_ID IN (SELECT ' + @RefColumn + ' FROM Enterprise..[' + @RefTableName + '] WHERE  Site_Code = ''' + @Site_Code + ''')'
	    END
	    --To un enroll vault device
	    ELSE IF UPPER(@TableName) = 'tVault_Devices'
	    BEGIN
	        SET @Qry = @Qry + ' UPDATE [' + @TableName + '] SET [Active] = 0, Site_ID = 0 WHERE Site_ID IN (SELECT ' + @RefColumn + ' FROM [' + @RefTableName + '] WHERE  Site_Code = ''' + @Site_Code + ''')'
	    END
	    --To clear vault CassetteDetails event
		ELSE IF UPPER(@TableName) = 'CassetteDetails'
		BEGIN  
	        SET @Qry = @Qry + ' DELETE FROM CassetteDetails WHERE VaultEventid IN (SELECT VaultEventid FROM VaultEvents WHERE SiteID IN (SELECT SiteID FROM Factory_Reset_Details WHERE Site_Code = ''' + @Site_Code + '''))'  
		END  
	    --Default remove records from table based on @Site_Code
	    ELSE
	    BEGIN
	        SET @Qry = @Qry + ' DELETE FROM [' + @TableName + '] WHERE ' + @UniqueColumn + ' IN (SELECT ' + @RefColumn + ' FROM Enterprise..[' + @RefTableName + '] WHERE  Site_Code = ''' + @Site_Code + ''')'
	    END
	    
	    --Sends the coined query to usp_ExecuteQuery based on DB
	    BEGIN TRY
	    	IF UPPER(@DBName) = 'ENTERPRISE'
	    	BEGIN	    	    
	    	    EXEC @Status = usp_ExecuteQuery @Qry
	    	END
	    	ELSE IF UPPER(@DBName) = 'AUDIT'
	    	BEGIN
	    	    EXEC @Status = Audit..usp_ExecuteQuery @Qry
	    	END
	    	ELSE
	    	BEGIN
	    	    EXEC @Status = MeterAnalysis..usp_ExecuteQuery @Qry
	    	END
	    END TRY
	    BEGIN CATCH
	    	SET @Status = -1
	    END CATCH
	    
	    IF @Status <> 0
	    BEGIN
			--If any error occured in deletion process exits from sp with @Status as -1
	        GOTO lblLast
	    END
	    
	    FETCH NEXT FROM curTableDetail INTO @DBName,@TableName,@UniqueColumn,@RefTableName,
	    @RefColumn
	END
	
	--Reset Machine to In Use State. Run only when Reset to Initial mode is selected
	IF @Mode_ID = 2
	BEGIN	           
	    UPDATE Mac
	    SET    Machine_Status_Flag = 0
	    FROM   [Machine] Mac
	           INNER JOIN Factory_Reset_Details FRD
	                ON  FRD.Machine_ID = Mac.Machine_ID
	    WHERE  ISNULL(Machine_End_Date,'') = ''
	           AND ISNULL(Machine_MAC_Address,'') = ''
	           AND FRD.Site_Code = @Site_Code
	END
	
	--Update the site staus back to FULLYCONFIGURED for Account and setting reset modes
	IF @Mode_ID = 3 OR @Mode_ID = 2
	BEGIN
		UPDATE [Site] SET SiteStatus = 'FULLYCONFIGURED' WHERE Site_Code = @Site_Code
	END
	
	--Reset Site information. Run only when Master Reset is selected
	IF @Mode_ID = 1
	BEGIN
		--To mark NGA as in-stock from in-use
		UPDATE Mac
	    SET    Machine_Status_Flag = 0
	    FROM   [Machine] Mac
	           INNER JOIN [Site] St ON St.NGA_Machine_ID = Mac.Machine_ID
	    WHERE  St.Site_Code = @Site_Code
		
	    UPDATE [Site]
	    SET    Site_Status = NULL,
	           Last_Updated_Time = NULL,
	           NGA_Machine_ID = NULL,
	           Site_Setting_Profile_ID = NULL,
	           SiteStatus = 'PARTIALLYCONFIGURED',	            
	           IsTITOEnabled = 0,
	           IsNonCashVoucherEnabled = 0,
	           IsCrossTicketingEnabled = 0,
	           TicketingURL = NULL
	    WHERE  Site_Code = @Site_Code	           
	END
	
	lblLast:
	
	CLOSE curTableDetail
	DEALLOCATE curTableDetail
END
GO