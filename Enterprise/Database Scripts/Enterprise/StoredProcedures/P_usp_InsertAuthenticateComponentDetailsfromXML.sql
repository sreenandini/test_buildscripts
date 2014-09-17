USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertAuthenticateComponentDetailsfromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertAuthenticateComponentDetailsfromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ===================================================================================================================================   
------------------------------ usp_InsertAuthenticateComponentDetailsfromXML ---------------------------------------------------------------------  
-- -----------------------------------------------------------------------------------------------------------------------------------   
-- Revision History  --    
-- 05/07/2010   Renjish  Created   
-- ==================================================================================================================================     

CREATE PROCEDURE dbo.usp_InsertAuthenticateComponentDetailsfromXML      
@SerialNo VARCHAR(30),     
@IsSuccess INT OUTPUT AS     
  
-- internal variables   
DECLARE @error  INT 
DECLARE @InstallationID INT
DECLARE @MachineSerialNo VARCHAR(50)
   
--variables for error handling   
set @IsSuccess=-1   
set @error = 0  
  
UPDATE BMC_AAMS_Details
SET BAD_Verification_Status = 0,
BAD_Updated_Date = GETDATE(),
BAD_Comments = 'Component Verification Triggered. Hence Resetting Verification Status.'
WHERE BAD_Asset_Serial_No = @SerialNo AND BAD_Verification_Status <> 0

--Check for any errors during the insert process.  
SET @error = @@ERROR  
IF @error <> 0   
GOTO Err_Handler 

UPDATE CV_Machine_Component_Details
SET CVMCD_Status = 0
WHERE CVMCD_Machine_Serial_No = @SerialNo

--Check for any errors during the insert process.  
SET @error = @@ERROR  
IF @error <> 0   
GOTO Err_Handler 

UPDATE IGI
SET Game_Verification = 0
FROM dbo.Installation_Game_Info IGI 
INNER JOIN dbo.Installation I ON IGI.Installation_No = I.Installation_ID
INNER JOIN dbo.Machine M ON I.Machine_ID = M.Machine_ID
WHERE M.Machine_Manufacturers_Serial_No = @SerialNo AND
I.Installation_End_Date IS NULL AND IGI.IsAvailable = 1 AND
IGI.Game_Verification <> 0

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

