USE [Enterprise]
GO

IF  EXISTS ( SELECT 1 FROM   sys.objects WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_InsertOldHourlyStatisticsfromXML]')
                      AND TYPE IN (N'P', N'PC') )
DROP PROCEDURE [dbo].[usp_InsertOldHourlyStatisticsfromXML]
GO

USE [Enterprise]
GO

CREATE PROCEDURE [dbo].[usp_InsertOldHourlyStatisticsfromXML]
	@doc VARCHAR(MAX),
	@IsSuccess INT OUTPUT
AS
	SET DATEFORMAT dmy 
	
	-- idoc is the document handle of the internal representation of an 
	-- XML document which is created by calling sp_xml_preparedocument
	DECLARE @idoc            INT 
	
	-- internal variables 
	DECLARE @HSID            AS INT 
	DECLARE @InstallationNo  INT 
	DECLARE @error           INT  
	DECLARE @iRowCount       INT 
	
	--variables for error handling 
	SET @IsSuccess = -1 
	SET @error = 0
	
	--Create an internal representation of the XML document.
	EXEC sp_xml_preparedocument @idoc OUTPUT,@doc 
	
	--Hourly XML Data - Previous Format
	--Copy the xml data to the hash table
	SELECT * INTO #Hourly_Statistics
	FROM   OPENXML(@idoc, '/Hourly_Statistics/Hourly_Statistics', 2) 
	       WITH Hourly_Statistics 
	
	--Assign the HQ_Installation_no as the installationno
	SELECT @InstallationNo = HQ_INSTALLATION_NO
	FROM   OPENXML(@idoc, '/Hourly_Statistics/Hourly_Statistics', 2) 
	       WITH (HQ_INSTALLATION_NO INT './HQ_INSTALLATION_NO') 
	
	
	--Check for any errors during the insert process.
	SET @error = @@ERROR
	
	--Removes the internal representation of the XML document.
	EXEC sp_xml_removedocument @idoc 
	
	IF @error <> 0 
	GOTO Err_Handler 
	
	
	SELECT *
	FROM   #Hourly_Statistics
	--update the installation no with the one in enteprise
	UPDATE #Hourly_Statistics
	SET    HS_Installation_No = @InstallationNo      
	
	SELECT *
	FROM   #Hourly_Statistics
	-- Update Code Start.
	UPDATE HS
	SET    HS_Hour1 = tmpHS.HS_Hour1,
	       HS_Hour2 = tmpHS.HS_Hour2,
	       HS_Hour3 = tmpHS.HS_Hour3,
	       HS_Hour4 = tmpHS.HS_Hour4,
	       HS_Hour5 = tmpHS.HS_Hour5,
	       HS_Hour6 = tmpHS.HS_Hour6,
	       HS_Hour7 = tmpHS.HS_Hour7,
	       HS_Hour8 = tmpHS.HS_Hour8,
	       HS_Hour9 = tmpHS.HS_Hour9,
	       HS_Hour10 = tmpHS.HS_Hour10,
	       HS_Hour11 = tmpHS.HS_Hour11,
	       HS_Hour12 = tmpHS.HS_Hour12,
	       HS_Hour13 = tmpHS.HS_Hour13,
	       HS_Hour14 = tmpHS.HS_Hour14,
	       HS_Hour15 = tmpHS.HS_Hour15,
	       HS_Hour16 = tmpHS.HS_Hour16,
	       HS_Hour17 = tmpHS.HS_Hour17,
	       HS_Hour18 = tmpHS.HS_Hour18,
	       HS_Hour19 = tmpHS.HS_Hour19,
	       HS_Hour20 = tmpHS.HS_Hour20,
	       HS_Hour21 = tmpHS.HS_Hour21,
	       HS_Hour22 = tmpHS.HS_Hour22,
	       HS_Hour23 = tmpHS.HS_Hour23,
	       HS_Hour24 = tmpHS.HS_Hour24,
	       HS_CreditInd = tmpHS.HS_CreditInd
	FROM   #Hourly_Statistics tmpHS
	       INNER JOIN Hourly_Statistics HS
	            ON  HS.HS_Installation_No = tmpHS.HS_Installation_No
	            AND HS.HS_Date = tmpHS.HS_Date
	            AND HS.HS_Type = tmpHS.HS_Type COLLATE DATABASE_DEFAULT
	
	--Check for any errors during the update process.
	SET @error = @@ERROR
	IF @error <> 0 
	GOTO Err_Handler 
	-- Update Code End.
	
	--Insert Code Start.
	INSERT INTO Hourly_Statistics
	SELECT TempHS.HS_Installation_No,
	       TempHS.HS_Date,
	       TempHS.HS_Type,
	       TempHS.HS_MoneyInd,
	       TempHS.HS_Hour1,
	       TempHS.HS_Hour2,
	       TempHS.HS_Hour3,
	       TempHS.HS_Hour4,
	       TempHS.HS_Hour5,
	       TempHS.HS_Hour6,
	       TempHS.HS_Hour7,
	       TempHS.HS_Hour8,
	       TempHS.HS_Hour9,
	       TempHS.HS_Hour10,
	       TempHS.HS_Hour11,
	       TempHS.HS_Hour12,
	       TempHS.HS_Hour13,
	       TempHS.HS_Hour14,
	       TempHS.HS_Hour15,
	       TempHS.HS_Hour16,
	       TempHS.HS_Hour17,
	       TempHS.HS_Hour18,
	       TempHS.HS_Hour19,
	       TempHS.HS_Hour20,
	       TempHS.HS_Hour21,
	       TempHS.HS_Hour22,
	       TempHS.HS_Hour23,
	       TempHS.HS_Hour24,
	       TempHS.HS_CreditInd
	FROM   #Hourly_Statistics TempHS
	       LEFT JOIN Hourly_Statistics HS
	            ON  TempHS.HS_Installation_No = HS.HS_Installation_No
	            AND TempHS.HS_Date = HS.HS_Date
	            AND TempHS.HS_Type = HS.HS_Type COLLATE DATABASE_DEFAULT
	WHERE  HS.HS_Installation_No IS NULL
	       AND HS.HS_Date IS NULL
	       AND HS.HS_Type IS NULL
	--Insert Code End.
	
	--Check for any errors during the insert process.
	SET @error = @@ERROR 
	IF @error <> 0 
	GOTO Err_Handler 
	
	--Return success/failure  
	Err_Handler: 
	IF @error = 0  
	SET @IsSuccess = 0 
	    --Success 
	    ELSE
	
	SET @IsSuccess = @error 
	--Error  
	RETURN @error
GO