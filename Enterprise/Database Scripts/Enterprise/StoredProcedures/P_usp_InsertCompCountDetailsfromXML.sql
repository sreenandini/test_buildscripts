USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertCompCountDetailsfromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertCompCountDetailsfromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ===================================================================================================================================   
------------------------------ usp_InsertCompCountDetailsfromXML ---------------------------------------------------------------------  
-- -----------------------------------------------------------------------------------------------------------------------------------   
-- Revision History  --    
-- 14/06/2010   Renjish  Created   
-- ==================================================================================================================================     

CREATE PROCEDURE dbo.usp_InsertCompCountDetailsfromXML      
@doc VARCHAR(MAX),     
@IsSuccess INT OUTPUT AS     
  
-- idoc is the document handle of the internal representation of an XML document which 
--is created by calling sp_xml_preparedocument.   
DECLARE @idoc INT    

-- internal variables   
DECLARE @error  INT 
DECLARE @InstallationID INT
DECLARE @MachineSerialNo VARCHAR(50)
   
--variables for error handling   
set @IsSuccess=-1   
set @error = 0  
  
--Table Variable to hold the data temporarily.  
DECLARE @CV_Component_Request_Status TABLE
(
	CCRS_Machine_Serial_No VARCHAR(50),
	CCRS_Count_Request_Status INT,
	CCRS_Component_Request_Status INT,
	CCRS_Component_Count INT,
	CCRS_Component_Count_Received INT,
	CCRS_Comments VARCHAR(50),
	CCRS_DateTime DATETIME
)

--Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @idoc OUTPUT, @doc    
  
--Insert into temp tables.    
INSERT INTO @CV_Component_Request_Status
SELECT
CCRS_Machine_Serial_No,
CCRS_Count_Request_Status,
CCRS_Component_Request_Status,
CCRS_Component_Count,
CCRS_Component_Count_Received,
CCRS_Comments,
CCRS_DateTime
FROM OPENXML (@idoc, '/COMPONENT_COUNTS/COMPONENT_COUNT',2)      
WITH (CCRS_Machine_Serial_No VARCHAR(50) './CCRS_Machine_Serial_No',
CCRS_Count_Request_Status INT './CCRS_Count_Request_Status',
CCRS_Component_Request_Status INT './CCRS_Component_Request_Status',
CCRS_Component_Count INT './CCRS_Component_Count',
CCRS_Component_Count_Received INT './CCRS_Component_Count_Received',
CCRS_Comments VARCHAR(50) './CCRS_Comments',
CCRS_DateTime DATETIME './CCRS_DateTime'
) 

--Check for any errors during the insert process.  
SET @error = @@ERROR      
IF @error <> 0   
GOTO Err_Handler   

--Update the CCD ID from the Enterprise Table.
UPDATE CS
SET CS.CCRS_Count_Request_Status = tmpCS.CCRS_Count_Request_Status,
CS.CCRS_Component_Request_Status = tmpCS.CCRS_Component_Request_Status,
CS.CCRS_Component_Count = tmpCS.CCRS_Component_Count,
CS.CCRS_Component_Count_Received = tmpCS.CCRS_Component_Count_Received,
CS.CCRS_Comments = tmpCS.CCRS_Comments,
CS.CCRS_DateTime = tmpCS.CCRS_DateTime
FROM dbo.CV_Component_Request_Status CS INNER JOIN @CV_Component_Request_Status tmpCS 
ON CS.CCRS_Machine_Serial_No = tmpCS.CCRS_Machine_Serial_No

--Insert the data to the actual tables.  
INSERT INTO dbo.CV_Component_Request_Status(CCRS_Machine_Serial_No, CCRS_Count_Request_Status,
CCRS_Component_Request_Status, CCRS_Component_Count, CCRS_Component_Count_Received, CCRS_Comments, CCRS_DateTime)
SELECT tmpCS.CCRS_Machine_Serial_No, tmpCS.CCRS_Count_Request_Status, tmpCS.CCRS_Component_Request_Status, 
tmpCS.CCRS_Component_Count, tmpCS.CCRS_Component_Count_Received, tmpCS.CCRS_Comments, tmpCS.CCRS_DateTime
FROM @CV_Component_Request_Status tmpCS
LEFT JOIN dbo.CV_Component_Request_Status CS 
ON tmpCS.CCRS_Machine_Serial_No = CS.CCRS_Machine_Serial_No COLLATE Database_Default
WHERE CS.CCRS_Machine_Serial_No IS NULL

--Check for any errors during the insert process.  
SET @error = @@ERROR  
IF @error <> 0   
GOTO Err_Handler   
 
--Removes the internal representation of the XML document.  
EXEC sp_xml_removedocument @idoc   
   
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

