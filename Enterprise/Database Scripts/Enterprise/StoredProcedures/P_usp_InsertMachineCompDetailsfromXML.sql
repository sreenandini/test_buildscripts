USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertMachineCompDetailsfromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertMachineCompDetailsfromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ===================================================================================================================================   
------------------------------ usp_InsertMachineCompDetailsfromXML ---------------------------------------------------------------------  
-- -----------------------------------------------------------------------------------------------------------------------------------   
-- Revision History  --    
-- 29/05/2010   Renjish  Created   
-- ==================================================================================================================================     

CREATE PROCEDURE dbo.usp_InsertMachineCompDetailsfromXML      
@doc VARCHAR(MAX),     
@IsSuccess INT OUTPUT AS     
  
-- idoc is the document handle of the internal representation of an XML document which 
--is created by calling sp_xml_preparedocument.   
DECLARE @idoc int    
-- internal variables   
DECLARE @error  int    
--variables for error handling   
set @IsSuccess=-1   
set @error = 0  
  
--Table Variable to hold the data temporarily.  
DECLARE @CV_Machine_Component_Details TABLE
(
	CVMCD_Machine_Serial_No VARCHAR(50),	
	CVMCD_CCT_Code INT,
	CVMCD_CCD_ID INT,
	CVMCD_Status INT,
	CVMCD_Hash_Value_Actual VARCHAR(150),
	CVMCD_Request_Verification INT,
	CVMCD_IsAvailable INT,
	CVMCD_CCT_Code_OS INT,
	CCD_Name VARCHAR(50)
)

--Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @idoc OUTPUT, @doc    
  
--Insert into temp tables.    
INSERT INTO @CV_Machine_Component_Details
SELECT
CVMCD_Machine_Serial_No,	
CVMCD_CCT_Code,
NULL,
CVMCD_Status,
CVMCD_Hash_Value_Actual,
CVMCD_Request_Verification,
CVMCD_IsAvailable,
CVMCD_CCT_Code_OS,
CCD_Name	
FROM OPENXML (@idoc, '/COMPONENT_DETAILS/COMPONENT_DETAIL',2)      
WITH (CVMCD_Machine_Serial_No VARCHAR(50) './CVMCD_Machine_Serial_No',
CVMCD_CCT_Code INT './CVMCD_CCT_Code',
CVMCD_Status INT './CVMCD_Status',
CVMCD_Hash_Value_Actual VARCHAR(150) './CVMCD_Hash_Value_Actual',
CVMCD_Request_Verification INT './CVMCD_Request_Verification',
CVMCD_IsAvailable INT './CVMCD_IsAvailable',
CVMCD_CCT_Code_OS INT './CVMCD_CCT_Code_OS',
CCD_Name VARCHAR(50) './CCD_Name') 

--Update the CCD ID from the Enterprise Table.
UPDATE tmpCMCD
SET tmpCMCD.CVMCD_CCD_ID = CCD.CCD_ID
FROM dbo.CV_Component_Details CCD INNER JOIN @CV_Machine_Component_Details tmpCMCD 
ON CCD.CCD_Name = tmpCMCD.CCD_Name AND CCD.CCD_CCT_Code = tmpCMCD.CVMCD_CCT_Code

--Remove data which does not have CCD_ID.
DELETE FROM @CV_Machine_Component_Details WHERE CVMCD_CCD_ID IS NULL

--Check for any errors during the insert process.  
SET @error = @@ERROR      
IF @error <> 0   
GOTO Err_Handler   
  
--Insert/Update the data to the actual tables.  
UPDATE CMCD
SET CMCD.CVMCD_CCD_ID = tmpCMCD.CVMCD_CCD_ID,
CMCD.CVMCD_Hash_Value_Actual = tmpCMCD.CVMCD_Hash_Value_Actual,
CMCD.CVMCD_Request_Verification = tmpCMCD.CVMCD_Request_Verification,
CMCD.CVMCD_IsAvailable = tmpCMCD.CVMCD_IsAvailable,
CMCD.CVMCD_CCT_Code_OS = tmpCMCD.CVMCD_CCT_Code_OS
--CMCD.CVMCD_Status = tmpCMCD.CVMCD_Status
FROM @CV_Machine_Component_Details tmpCMCD
INNER JOIN dbo.CV_Machine_Component_Details CMCD 
ON tmpCMCD.CVMCD_Machine_Serial_No = CMCD.CVMCD_Machine_Serial_No COLLATE Database_Default
AND tmpCMCD.CVMCD_CCT_Code = CMCD.CVMCD_CCT_Code

INSERT INTO dbo.CV_Machine_Component_Details
SELECT tmpCMCD.CVMCD_Machine_Serial_No,	
	tmpCMCD.CVMCD_CCT_Code,
	tmpCMCD.CVMCD_CCD_ID,
	tmpCMCD.CVMCD_Status,
	tmpCMCD.CVMCD_Hash_Value_Actual,
	tmpCMCD.CVMCD_Request_Verification,
	tmpCMCD.CVMCD_IsAvailable,
	tmpCMCD.CVMCD_CCT_Code_OS	
FROM @CV_Machine_Component_Details tmpCMCD
LEFT JOIN CV_Machine_Component_Details CMCD 
ON tmpCMCD.CVMCD_Machine_Serial_No = CMCD.CVMCD_Machine_Serial_No COLLATE Database_Default
AND tmpCMCD.CVMCD_CCT_Code = CMCD.CVMCD_CCT_Code WHERE CMCD.CVMCD_Machine_Serial_No IS NULL
AND tmpCMCD.CVMCD_CCD_ID IS NOT NULL

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

