USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertCompDetailsfromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertCompDetailsfromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ===================================================================================================================================   
------------------------------ usp_InsertCompDetailsfromXML ---------------------------------------------------------------------  
-- -----------------------------------------------------------------------------------------------------------------------------------   
-- Revision History  --    
-- 29/05/2010   Renjish  Created   
-- ==================================================================================================================================     

CREATE PROCEDURE dbo.usp_InsertCompDetailsfromXML      
@doc VARCHAR(MAX),     
@IsSuccess INT OUTPUT AS     
  
-- idoc is the document handle of the internal representation of an XML document which is created by calling sp_xml_preparedocument.   
DECLARE @idoc INT    
-- internal variables   
DECLARE @error  INT    
--variables for error handling   
set @IsSuccess=-1   
set @error = 0  
  
--Table Variable to hold the data temporarily.  
DECLARE @CV_Component_Details TABLE
(
	CCD_Name VARCHAR(50),
	CCD_Serial_No VARCHAR(20),
	CCD_CCT_Code INT,
	CCD_CAT_Code INT,
	CCD_Seed_Value VARCHAR(50),
	CCD_Hash_Value VARCHAR(150)
)

DECLARE @CV_Component_Supported_Algorithm TABLE
(	
	CVCSA_CCD_ID INT,
	CVCSA_CAT_Code INT
)
  
--Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @idoc OUTPUT, @doc    
  
--Insert into temp tables.   
INSERT INTO @CV_Component_Details
SELECT CCD_Name,
CCD_Serial_No,
CCD_CCT_Code,
CCD_CAT_Code,
CCD_Seed_Value,
CCD_Hash_Value 
FROM OPENXML (@idoc, '/COMPONENT_DETAILS/COMPONENT',2)      
WITH (CCD_Name VARCHAR(50) './CCD_Name',
CCD_Serial_No VARCHAR(20) './CCD_Serial_No',
CCD_CCT_Code INT './CCD_CCT_Code',
CCD_CAT_Code INT './CCD_CAT_Code',
CCD_Seed_Value VARCHAR(50) './CCD_Seed_Value',
CCD_Hash_Value VARCHAR(150) './CCD_Hash_Value') 

--Check for any errors during the insert process.  
SET @error = @@ERROR      
IF @error <> 0   
GOTO Err_Handler  

INSERT INTO @CV_Component_Supported_Algorithm
SELECT CVCSA_CCD_ID,
	CVCSA_CAT_Code
FROM OPENXML (@idoc, '/COMPONENT_DETAILS/COMPONENT',2)      
WITH (CVCSA_CCD_ID INT './CVCSA_CCD_ID',
CVCSA_CAT_Code INT './CVCSA_CAT_Code'
) 

--Check for any errors during the insert process.  
SET @error = @@ERROR      
IF @error <> 0   
GOTO Err_Handler  
 
--Insert/Update the data to the actual tables.
UPDATE CCD
SET CCD.CCD_CAT_Code = tmpCCD.CCD_CAT_Code
FROM @CV_Component_Details tmpCCD
INNER JOIN dbo.CV_Component_Details CCD 
ON CCD.CCD_Name = tmpCCD.CCD_Name COLLATE Database_Default
AND CCD.CCD_CCT_Code = tmpCCD.CCD_CCT_Code

INSERT INTO CV_Component_Details  
SELECT tmpCCD.* FROM @CV_Component_Details tmpCCD  
LEFT JOIN dbo.CV_Component_Details CCD   
ON CCD.CCD_Name = tmpCCD.CCD_Name COLLATE Database_Default  
AND CCD.CCD_CCT_Code = tmpCCD.CCD_CCT_Code  
WHERE CCD.CCD_Name IS NULL AND tmpCCD.CCD_Name = (SELECT CCD_Name 
FROM @CV_Component_Details GROUP BY CCD_Name
HAVING COUNT(CCD_Name) < 2)

INSERT INTO CV_Component_Details  
SELECT TOP 1 tmpCCD.* FROM @CV_Component_Details tmpCCD  
LEFT JOIN dbo.CV_Component_Details CCD   
ON CCD.CCD_Name = tmpCCD.CCD_Name COLLATE Database_Default  
AND CCD.CCD_CCT_Code = tmpCCD.CCD_CCT_Code  
WHERE CCD.CCD_Name IS NULL AND tmpCCD.CCD_Name = (SELECT CCD_Name 
FROM @CV_Component_Details GROUP BY CCD_Name
HAVING COUNT(CCD_Name) > 1)

--Check for any errors during the insert process.  
SET @error = @@ERROR      
IF @error <> 0   
GOTO Err_Handler   

--Get Enterprise CCD ID
DECLARE @HQ_CCD_ID INT

SELECT @HQ_CCD_ID = CCD_ID FROM dbo.CV_Component_Details CCD
INNER JOIN @CV_Component_Details tmpCCD 
ON CCD.CCD_Name = tmpCCD.CCD_Name COLLATE Database_Default AND CCD.CCD_CCT_Code = tmpCCD.CCD_CCT_Code

IF ISNULL(@HQ_CCD_ID,0) <> 0
UPDATE @CV_Component_Supported_Algorithm
SET CVCSA_CCD_ID = @HQ_CCD_ID

--Clear the existing Alg data for the Component.
DELETE FROM CV_Component_Supported_Algorithm WHERE CVCSA_CCD_ID = @HQ_CCD_ID

--Insert the Data.
INSERT INTO CV_Component_Supported_Algorithm
SELECT * FROM @CV_Component_Supported_Algorithm

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

