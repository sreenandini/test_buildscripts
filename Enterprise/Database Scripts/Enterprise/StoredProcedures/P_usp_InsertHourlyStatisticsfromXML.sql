USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertHourlyStatisticsfromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertHourlyStatisticsfromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================================================================================== 
------------------------------ usp_InsertHourlyStatisticsfromXML ---------------------------------------------------------------------
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
-- 26/08/2009   Renjish Modified the root node from /Hourly_Statistics to /Hourly_Statistics/Hourly_Statistics.
-- 11/03/2011   Anil Added new coloumn named HS_CreditInd
-- ===================================================================================================================================   
CREATE PROCEDURE [dbo].[usp_InsertHourlyStatisticsfromXML]    
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
DECLARE @previousVTPID int
DECLARE @currentVTPID int
DECLARE @HS_Date datetime
 
--variables for error handling 
set @IsSuccess=-1 
set @error = 0

--add the encoding version as we need to process special characters like pound symbol 
SET @doc='<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc  

--Create an internal representation of the XML document.
EXEC sp_xml_preparedocument @idoc OUTPUT, @doc  

--Check for the Hourly XML Format.
--Latest Format 11.4.9(ROOT - HourlyStatistics)/Previous Format 11.4.8 & older versions(ROOT - Hourly_Statistics)
--Set row count to 0.
SET @iRowCount = 0

SELECT @iRowCount = COUNT(*) FROM OPENXML (@idoc, '/Hourly_Statistics',2)         
WITH Hourly_Statistics  

PRINT @iRowCount
IF @iRowCount > 0
      --Hourly XML Data - Previous Format.
      BEGIN
      EXEC usp_InsertOldHourlyStatisticsfromXML @doc,@IsSuccess OUTPUT
      EXEC sp_xml_removedocument @idoc
      RETURN @IsSuccess
	END
ELSE
      --Hourly XML Data - Latest Format.
      BEGIN
      --Copy the xml data to the table variable.
      SELECT * into #Hourly_Statistics
      FROM OPENXML (@idoc, '/HourlyStatistics/Hourly_Statistic',2)         
      WITH Hourly_Statistics    

	--Assign the HQ_Installation_no as the installationno  
	SELECT  @InstallationNo = HQ_INSTALLATION_NO ,@HS_Date = HS_Date  
	FROM OPENXML (@idoc, '/HourlyStatistics',2)    
	WITH (
	HQ_INSTALLATION_NO int './Hourly_Statistic/HQ_INSTALLATION_NO',
	HS_Date datetime './Hourly_Statistic/HS_Date'
	) 
	END

--Check for any errors during the insert process.
SET @error = @@ERROR
IF @error <> 0 
GOTO Err_Handler  
--Removes the internal representation of the XML document.
EXEC sp_xml_removedocument @idoc 

SELECT * FROM #Hourly_Statistics
--update the installation no with the one in enteprise
UPDATE #Hourly_Statistics SET HS_Installation_No = @InstallationNo      

-- Get Previous & Current Meter History ID for Updating MH_LinkReference with Hourly_Statistics_ID
SELECT TOP 1 @previousVTPID = MH_ID FROM Meter_history MH WHERE MH.MH_Installation_No = @InstallationNo
AND MH.MH_Process = 'VTP' AND MH.MH_Type = 'P' AND MH_LinkReference IS NULL ORDER BY MH_ID ASC

SELECT TOP 1 @currentVTPID = MH_ID FROM Meter_history WHERE MH_Installation_No = @InstallationNo
AND MH_Process = 'VTP' AND MH_Type = 'C' ORDER BY MH_ID DESC

SELECT * FROM #Hourly_Statistics
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
HS_Hour24  = tmpHS.HS_Hour24,
HS_CreditInd= tmpHS.HS_CreditInd           
FROM #Hourly_Statistics tmpHS
INNER JOIN Hourly_Statistics HS 
ON HS.HS_Installation_No = tmpHS.HS_Installation_No 
AND HS.HS_Date = tmpHS.HS_Date 
AND   HS.HS_Type = tmpHS.HS_Type COLLATE DATABASE_DEFAULT

--Check for any errors during the update process.
SET @error = @@ERROR
IF @error <> 0 
GOTO Err_Handler   
-- Update Code End.
    
--Insert Code Start.
--DECLARE @COUNT INT
--DECLARE @CURRENT INT
--
--SET @CURRENT = 1
--SELECT @COUNT = COUNT(*) FROM @Hourly_Statistics
--
--WHILE (@CURRENT <= @COUNT)
--BEGIN
--END

--Insert Code Start.
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
            FROM #Hourly_Statistics TempHS 
            LEFT JOIN Hourly_Statistics HS 
            ON TempHS.HS_Installation_No = HS.HS_Installation_No
            AND TempHS.HS_Date = HS.HS_Date
            AND TempHS.HS_Type = HS.HS_Type COLLATE DATABASE_DEFAULT
            WHERE HS.HS_Installation_No IS NULL
            AND HS.HS_Date IS NULL
            AND HS.HS_Type IS NULL
--Insert Code End.
   
		SELECT @HSID = HS_ID 
		FROM Hourly_Statistics HS   
		WHERE HS.HS_Installation_No =  @InstallationNo
		AND HS.HS_Date = @HS_Date
		AND HS.HS_Type = 'GAMES_BET' 
		
		-- Update MH_LinkReference with Hourly_Statistics_ID for Previous & Current Meter History ID
		IF(ISNULL(@previousVTPID,0) <> 0 AND ISNULL(@currentVTPID,0) <> 0)
		BEGIN
			UPDATE Meter_History SET MH_LinkReference = @HSID
			WHERE MH_Installation_No = @InstallationNo AND MH_Process = 'VTP' 
			AND MH_ID IN (@previousVTPID, @currentVTPID) AND MH_LinkReference IS NULL
		END

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
return @error 


GO

