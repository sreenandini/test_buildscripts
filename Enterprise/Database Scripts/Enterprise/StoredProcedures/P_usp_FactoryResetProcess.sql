USE [Enterprise]
GO
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'usp_FactoryResetProcess'
   )
    DROP PROCEDURE dbo.usp_FactoryResetProcess
GO

CREATE PROCEDURE dbo.usp_FactoryResetProcess
 @Site_Code VARCHAR(50),
 @Doc VARCHAR(MAX),
 @IsSuccess INT OUTPUT
AS
--/*****************************************************************************************************
--DESCRIPTION    :	Invokes all the necceasary SPs for reset process. 
--					Reset Modes :	1 - Master Reset
--									2 - Reset Initial Setting
--									3 - Delete Accounting Details
--CREATED DATE   : 
--MODULE		 : Used in Data Import Services for FACTORYRESET import_type
--Example		 : =============================================
--				   EXECUTE dbo.usp_FactoryResetProcess '1002',@Doc,@IsSuccess OUTPUT
--				   GO
--				   =============================================
--CHANGE HISTORY :
--------------------------------------------------------------------------------------------------------
--AUTHOR						MODIFIED DATE		DESCRIPTON
--------------------------------------------------------------------------------------------------------

--*****************************************************************************************************/
BEGIN
	SET NOCOUNT ON
	DECLARE @CurrentModeID  INT
	DECLARE @Return   INT	
	DECLARE @Mode_ID  INT	
	DECLARE @idoc     INT
	DECLARE @SiteID   INT
	
	SET @CurrentModeID = 3
	SET @Return = 0
	SET @IsSuccess = 0
	SET @Mode_ID = 0
	SET @SiteID = 0
	
	BEGIN TRY
		SET @IsSuccess = 0      
		
		IF ISNULL(@Doc, '') = ''
		    RETURN 0      
		
		SET @Doc = '<?xml version="1.0" encoding="ISO-8859-1"?>' + @Doc      
		
		EXEC sp_xml_preparedocument @idoc OUTPUT,
		     @Doc
		
		--Extract the XML for factory reset mode id
		SELECT @Mode_ID = Mode_ID
		FROM   OPENXML(@idoc, './/FactoryReset/FactoryResetDetails', 1) WITH 
		       (Mode_ID INT './FR_ID')
				
		IF @Mode_ID = 0
		BEGIN
			RETURN 0
		END			
		
		--Update the status of corresponind site that reset process is initiated
		UPDATE [Site] SET FactoryResetStatus = 'Initiated' WHERE Site_Code = @Site_Code
		
		--Populate common factory reset details
		EXEC usp_FactoryResetDetails 0, @Site_Code, @Return OUTPUT
		
		IF @Return = 0
		BEGIN
			--Update the status of corresponind site that reset process is going
			UPDATE [Site] SET FactoryResetStatus = 'InProgress' WHERE Site_Code = @Site_Code
			
			--Loops from mode 3 to 1
			--Example when reset mode is Master Reset mode id is 1 and @CurrentModeID is 3.
			--CurrentModeID Will decrease one by one and reset operation will be carried
		    WHILE (@CurrentModeID >= @Mode_ID)
		    BEGIN
		        --Reset Table based on CurrentModeID
		        EXEC usp_ResetTable @CurrentModeID, @Site_Code, @Return OUTPUT		        		        
		        
		        IF @Return <> 0
		        BEGIN
		            SET @IsSuccess = 2
		            BREAK
		        END
		        
		        SET @CurrentModeID = @CurrentModeID - 1
		    END 
		    
		    --Clear Populated common factory reset details
		    SET @Return = 0
		    EXEC usp_FactoryResetDetails 1, @Site_Code, @Return OUTPUT
		    
		    --Update the reset status of site when all reset modes executed
		    IF @IsSuccess = 0 AND @Return = 0
		    BEGIN
		    	UPDATE [Site] SET FactoryResetStatus = 'Completed', @SiteID = Site_ID WHERE Site_Code = @Site_Code		    					
		    END
			ELSE
			BEGIN
				UPDATE [Site] SET FactoryResetStatus = 'Failed' WHERE Site_Code = @Site_Code	
			END
			
			--Puts an entry in export history to initimate site controller about reset status
			INSERT INTO Export_History (EH_Date,EH_Reference1,EH_Type,EH_Site_Code)
			VALUES(GETDATE(),@SiteID,'FACTORYRESET_STATUS',@Site_Code)
			
		END
		ELSE
		BEGIN
			--Error in populating common details
		    SET @IsSuccess = 1
		END
	END TRY
	
	BEGIN CATCH
		--If any error occurs sets the error number
		SELECT @Return = ERROR_NUMBER()
	END CATCH
	
	--If any error in loop marks it and returns to services.
	IF @Return <> 0 AND @IsSuccess = 0
	BEGIN
	    SET @IsSuccess = 3
	END
END
GO