USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertCompVerificationDetailsfromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertCompVerificationDetailsfromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ===================================================================================================================================   
------------------------------ usp_InsertCompVerificationDetailsfromXML ---------------------------------------------------------------------  
-- -----------------------------------------------------------------------------------------------------------------------------------   
-- Revision History  --    
-- 14/06/2010   Renjish  Created   
-- ==================================================================================================================================     

CREATE PROCEDURE dbo.usp_InsertCompVerificationDetailsfromXML      
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
DECLARE @CV_Verification_Details TABLE
(
	Site_CVD_ID INT,
	Machine_Serial_No VARCHAR(50),	
	CCD_ID INT,
	Verification_Type INT,
	Site_Code VARCHAR(50),
	CCT_Code INT
)

--Create an internal representation of the XML document.  
EXEC sp_xml_preparedocument @idoc OUTPUT, @doc    
  
--Insert into temp tables.    
INSERT INTO @CV_Verification_Details
SELECT
CVD_ID,
CVD_Machine_Serial_No,
CVD_CCD_ID,
CVD_Verification_Type,
Site_Code,
CCT_Code
FROM OPENXML (@idoc, '/VERIFICATION_DETAILS/VERIFICATION_DETAIL',2)      
WITH (CVD_ID INT './CVD_ID',
CVD_Machine_Serial_No VARCHAR(50) './CVD_Machine_Serial_No',
CVD_CCD_ID INT './CVD_CCD_ID',
CVD_Verification_Type INT './CVD_Verification_Type',
Site_Code VARCHAR(50) './Site_Code',
CCT_Code INT './CCT_Code'
) 

--Check for any errors during the insert process.  
SET @error = @@ERROR      
IF @error <> 0   
GOTO Err_Handler   

--Update the CCD ID from the Enterprise Table.
UPDATE tmpCVD
SET tmpCVD.CCD_ID = CMCD.CVMCD_CCD_ID
FROM dbo.CV_Machine_Component_Details CMCD INNER JOIN @CV_Verification_Details tmpCVD 
ON CMCD.CVMCD_Machine_Serial_No = tmpCVD.Machine_Serial_No 
AND CMCD.CVMCD_CCT_Code = tmpCVD.CCT_Code

SELECT @MachineSerialNo = Machine_Serial_No FROM @CV_Verification_Details
  
SELECT @InstallationID = I.Installation_ID FROM Installation I
INNER JOIN Machine M ON I.Machine_ID = M.Machine_ID
WHERE M.Machine_Manufacturers_Serial_No = @MachineSerialNo 
AND I.Installation_End_Date IS NULL

--Insert the data to the actual tables.  
INSERT INTO dbo.CV_Verification_Details(CVD_Machine_Serial_No, CVD_CCD_ID, CVD_Verification_Type,
CVD_Request_Status, CVD_Verification_Status, CVD_Site_Code, CVD_Site_CVD_ID, CVD_Installation_No)
SELECT tmpCVD.Machine_Serial_No, tmpCVD.CCD_ID, tmpCVD.Verification_Type, 1, 0, 
tmpCVD.Site_Code, tmpCVD.Site_CVD_ID, @InstallationID
FROM @CV_Verification_Details tmpCVD
LEFT JOIN dbo.CV_Verification_Details CVD 
ON tmpCVD.Site_Code = CVD.CVD_Site_Code COLLATE Database_Default
AND tmpCVD.Site_CVD_ID = CVD.CVD_Site_CVD_ID WHERE CVD.CVD_Machine_Serial_No IS NULL

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

