Use Enterprise
Go



if exists (Select 1 from sys.objects where name='usp_InsertHourlyStatisticsfromXML_114' and type='P')
Begin
  drop procedure usp_InsertHourlyStatisticsfromXML_114
End

Go
  
    
-- ===================================================================================================================================     
------------------------------ usp_InsertHourlyStatisticsfromXML_114 ---------------------------------------------------------------------    
------------------------------ returns inserts into HourlyStatistics table from XML --    
-- -----------------------------------------------------------------------------------------------------------------------------------     
-- Revision History  --      
-- 11/05/2007   N.Siva  Created     
-- 07/06/2007   N.Siva  sp_xml_removedocument is not called in all flow paths --       If record is already processed, return -1 to the caller     
-- 19/06/07     Poorna  Changes as per standard regional date/time changes     
-- 02/11/07     N.Siva  Added XML encoding  --       collection date,time should be taken from batch table     
-- 22/10/2007   Poorna K. Added handling of RAMRESET/ROLLOVER records     
-- 05/12/2007   Siva  bug fix some MH  are not processed  due to time lag --       check for installation_start_date only if required     
-- 23/05/2008   Vineetha HQ_Installation_No used as installation_no     
-- 17/06/2008   Renjish Modified the sp to update/insert also based on the latest XML Data format for Hourly Statistics.    
-- ===================================================================================================================================       
CREATE PROCEDURE [dbo].[usp_InsertHourlyStatisticsfromXML_114]        
@doc varchar(max),       
@IsSuccess int output AS       
    
SET DATEFORMAT dmy     
    
-- idoc is the document handle of the internal representation of an XML document which is created by calling sp_xml_preparedocument.     
DECLARE @idoc int      
-- internal variables     
DECLARE @HSID as int     
DECLARE @InstallationNo int     
DECLARE @error  int      
DECLARE @iRowCount  int     

BEGIN

--variables for error handling     
set @IsSuccess=-1     
set @error = 0    
    
----Table Variable to hold the data temporarily.    
--DECLARE @Hourly_Statistics TABLE(    
-- HS_ID INT IDENTITY(1,1),    
-- HS_Installation_No INT,    
-- HS_Date DATETIME,    
-- HS_Type VARCHAR(50),    
-- HS_MoneyInd INT,    
-- HS_Hour1 INT,    
-- HS_Hour2 INT,    
-- HS_Hour3 INT,    
-- HS_Hour4 INT,    
-- HS_Hour5 INT,    
-- HS_Hour6 INT,    
-- HS_Hour7 INT,    
-- HS_Hour8 INT,    
-- HS_Hour9 INT,    
-- HS_Hour10 INT,    
-- HS_Hour11 INT,    
-- HS_Hour12 INT,    
-- HS_Hour13 INT,    
-- HS_Hour14 INT,    
-- HS_Hour15 INT,    
-- HS_Hour16 INT,    
-- HS_Hour17 INT,    
-- HS_Hour18 INT,    
-- HS_Hour19 INT,    
-- HS_Hour20 INT,    
-- HS_Hour21 INT,    
-- HS_Hour22 INT,    
-- HS_Hour23 INT,    
-- HS_Hour24 INT,    
-- HS_CreditInd INT    
--)    
    
--add the encoding version as we need to process special characters like pound symbol     
SET @doc='<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc      
    
--Create an internal representation of the XML document.    
EXEC sp_xml_preparedocument @idoc OUTPUT, @doc      
    
--Check for the Hourly XML Format.    
--Latest Format 11.4.9(ROOT - HourlyStatistics)/Previous Format 11.4.8 & older versions(ROOT - Hourly_Statistics)    
--Set row count to 0.    
SET @iRowCount = 0    
    
SELECT @iRowCount = COUNT(1) FROM OPENXML (@idoc, '/HourlyStatistics/Hourly_Statistic',2)             
WITH Hourly_Statistics      
    
PRINT @iRowCount    
IF(@iRowCount > 1 )
BEGIN
  
	SELECT * into #Hourly_Statistic   FROM OPENXML (@idoc, '/HourlyStatistics/Hourly_Statistic',2)             
	WITH Hourly_Statistics

	--set identity_insert @Hourly_Statistics on  
	SELECT TOP 1 * FROM #Hourly_Statistic  
	--Assign the HQ_Installation_no as the installationno      
	SELECT  @InstallationNo = HQ_INSTALLATION_NO      
	FROM OPENXML (@idoc, '/HourlyStatistics',2)      
	WITH (HQ_INSTALLATION_NO int './Hourly_Statistic/HQ_INSTALLATION_NO')     

	--Check for any errors during the insert process.    
	SET @error = @@ERROR    
	IF @error <> 0     
		GOTO Err_Handler  

	--Removes the internal representation of the XML document.    
	EXEC sp_xml_removedocument @idoc     

	--SELECT * FROM @Hourly_Statistics    
	--update the installation no with the one in enteprise    
	UPDATE #Hourly_Statistic SET HS_Installation_No = @InstallationNo       
	
	-- Update Code Start.    
	UPDATE  HS    
	SET         
	HS_Hour1   = tmpHS.HS_Hour1 ,         
	HS_Hour2   = tmpHS.HS_Hour2 ,         
	HS_Hour3   = tmpHS.HS_Hour3 ,         
	HS_Hour4   = tmpHS.HS_Hour4 ,         
	HS_Hour5   = tmpHS.HS_Hour5 ,         
	HS_Hour6   = tmpHS.HS_Hour6 ,         
	HS_Hour7   = tmpHS.HS_Hour7 ,         
	HS_Hour8   = tmpHS.HS_Hour8 ,         
	HS_Hour9   = tmpHS.HS_Hour9 ,         
	HS_Hour10  = tmpHS.HS_Hour10 ,         
	HS_Hour11  = tmpHS.HS_Hour11 ,         
	HS_Hour12  = tmpHS.HS_Hour12 ,         
	HS_Hour13  = tmpHS.HS_Hour13 ,         
	HS_Hour14  = tmpHS.HS_Hour14 ,         
	HS_Hour15  = tmpHS.HS_Hour15 ,         
	HS_Hour16  = tmpHS.HS_Hour16 ,         
	HS_Hour17  = tmpHS.HS_Hour17 ,         
	HS_Hour18  = tmpHS.HS_Hour18 ,         
	HS_Hour19  = tmpHS.HS_Hour19 ,         
	HS_Hour20  = tmpHS.HS_Hour20 ,         
	HS_Hour21  = tmpHS.HS_Hour21 ,         
	HS_Hour22  = tmpHS.HS_Hour22 ,         
	HS_Hour23  =  tmpHS.HS_Hour23 ,         
	HS_Hour24   = tmpHS.HS_Hour24,  
	HS_CreditInd= tmpHS.HS_CreditInd             
	FROM #Hourly_Statistic tmpHS    
	INNER JOIN Hourly_Statistics HS   with (nolock)   
	ON HS.HS_Installation_No = tmpHS.HS_Installation_No     
	AND HS.HS_Date = tmpHS.HS_Date     
	AND HS.HS_Type = tmpHS.HS_Type COLLATE DATABASE_DEFAULT    

	--Check for any errors during the update process.    
	SET @error = @@ERROR    
	IF @error <> 0     
	GOTO Err_Handler       
	-- Update Code End.    
    
	INSERT INTO Hourly_Statistics    
	SELECT     
	TempHS.HS_Installation_No,  
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
	FROM #Hourly_Statistic TempHS  with (nolock)   
	LEFT JOIN Hourly_Statistics HS with (nolock)  
	ON TempHS.HS_Installation_No = HS.HS_Installation_No  
	AND TempHS.HS_Date = HS.HS_Date  
	AND TempHS.HS_Type = HS.HS_Type COLLATE DATABASE_DEFAULT  
	WHERE HS.HS_Installation_No IS NULL  
	AND HS.HS_Date IS NULL  
	AND HS.HS_Type IS NULL  

	--Check for any errors during the insert process.    
	SET @error = @@ERROR        
	IF @error <> 0     
		GOTO Err_Handler       
	--Increment the counter.    
	--Insert Code End.    
END
ELSE
BEGIN
	SELECT * into #Hourly_Statistics   FROM OPENXML (@idoc, '/Hourly_Statistics/Hourly_Statistics',2)             
	WITH Hourly_Statistics       

	--set identity_insert @Hourly_Statistics on  
	SELECT TOP 1 * FROM #Hourly_Statistics  
	--Assign the HQ_Installation_no as the installationno      
	SELECT  @InstallationNo = HQ_INSTALLATION_NO      
	FROM OPENXML (@idoc, '/Hourly_Statistics',2)      
	WITH (HQ_INSTALLATION_NO int './Hourly_Statistics/HQ_INSTALLATION_NO')     
	
	--Check for any errors during the insert process.    
	SET @error = @@ERROR    
	IF @error <> 0     
	GOTO Err_Handler      
	--Removes the internal representation of the XML document.    
	EXEC sp_xml_removedocument @idoc     

	--SELECT * FROM @Hourly_Statistics    
	--update the installation no with the one in enteprise    
	UPDATE #Hourly_Statistics SET HS_Installation_No = @InstallationNo  
	
	--Update Code Start.    
	UPDATE  HS    
	SET         
	HS_Hour1   = tmpHS.HS_Hour1 ,         
	HS_Hour2   = tmpHS.HS_Hour2 ,         
	HS_Hour3   = tmpHS.HS_Hour3 ,         
	HS_Hour4   = tmpHS.HS_Hour4 ,         
	HS_Hour5   = tmpHS.HS_Hour5 ,         
	HS_Hour6   = tmpHS.HS_Hour6 ,         
	HS_Hour7   = tmpHS.HS_Hour7 ,         
	HS_Hour8   = tmpHS.HS_Hour8 ,         
	HS_Hour9   = tmpHS.HS_Hour9 ,         
	HS_Hour10  = tmpHS.HS_Hour10 ,         
	HS_Hour11  = tmpHS.HS_Hour11 ,         
	HS_Hour12  = tmpHS.HS_Hour12 ,         
	HS_Hour13  = tmpHS.HS_Hour13 ,         
	HS_Hour14  = tmpHS.HS_Hour14 ,         
	HS_Hour15  = tmpHS.HS_Hour15 ,         
	HS_Hour16  = tmpHS.HS_Hour16 ,         
	HS_Hour17  = tmpHS.HS_Hour17 ,         
	HS_Hour18  = tmpHS.HS_Hour18 ,         
	HS_Hour19  = tmpHS.HS_Hour19 ,         
	HS_Hour20  = tmpHS.HS_Hour20 ,         
	HS_Hour21  = tmpHS.HS_Hour21 ,         
	HS_Hour22  = tmpHS.HS_Hour22 ,         
	HS_Hour23  =  tmpHS.HS_Hour23 ,         
	HS_Hour24   = tmpHS.HS_Hour24,  
	HS_CreditInd= tmpHS.HS_CreditInd             
	FROM #Hourly_Statistics tmpHS    
	INNER JOIN Hourly_Statistics HS   with (nolock)   
	ON HS.HS_Installation_No = tmpHS.HS_Installation_No     
	AND HS.HS_Date = tmpHS.HS_Date     
	AND HS.HS_Type = tmpHS.HS_Type COLLATE DATABASE_DEFAULT    

	--Check for any errors during the update process.    
	SET @error = @@ERROR    
	IF @error <> 0     
	GOTO Err_Handler       
	-- Update Code End.    
	
	INSERT INTO Hourly_Statistics    
	SELECT     
	TempHS.HS_Installation_No,  
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
	FROM #Hourly_Statistics TempHS  with (nolock)   
	LEFT JOIN Hourly_Statistics HS with (nolock)  
	ON TempHS.HS_Installation_No = HS.HS_Installation_No  
	AND TempHS.HS_Date = HS.HS_Date  
	AND TempHS.HS_Type = HS.HS_Type COLLATE DATABASE_DEFAULT  
	WHERE HS.HS_Installation_No IS NULL  
	AND HS.HS_Date IS NULL  
	AND HS.HS_Type IS NULL  

	--Check for any errors during the insert process.    
	SET @error = @@ERROR        
	IF @error <> 0     
	GOTO Err_Handler       
	--Increment the counter.    
	--Insert Code End.   
END

--Return success/failure      
Err_Handler:      
IF @error = 0      
SET @IsSuccess = 0     
--Success     
ELSE    
SET @IsSuccess = @error     
--Error      
return @error     

END

GO

