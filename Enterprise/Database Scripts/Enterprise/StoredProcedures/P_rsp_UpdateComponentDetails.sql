USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_UpdateComponentDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_UpdateComponentDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================    
-- OUTPUT --update component details -- exec rsp_UpdateComponentDetails     
-- Revision History    
-- M Senthil 31/05/2010  Created    
-- ======================================================================= 

CREATE PROCEDURE rsp_UpdateComponentDetails    
(@CompName VARCHAR(50),    
@ModelName VARCHAR(50),    
@CompType int,    
@AlgoType int,    
@Seed VARCHAR(150),    
@Hash VARCHAR(150),  
@IsSuccess INT OUTPUT)  
AS 
 
SET @IsSuccess = -1  
DECLARE @CompId INT

IF EXISTS(SELECT CCD_ID FROM CV_Component_Details WHERE CCD_Name = @CompName 
AND CCD_CCT_Code = @CompType)  
BEGIN  

SET @IsSuccess =  (SELECT CCD_ID FROM CV_Component_Details WHERE CCD_Name = @CompName 
AND CCD_CCT_Code = @CompType)

UPDATE CV_Component_Details  
SET  
CCD_Model_Name = @ModelName,  
CCD_CCT_Code = @CompType,  
CCD_CAT_Code = @AlgoType,  
CCD_Seed_Value= @Seed,  
CCD_Hash_Value = @Hash  
WHERE CCD_ID = @IsSuccess

END


GO

