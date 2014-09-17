USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_InsertComponentDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_InsertComponentDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================    
-- OUTPUT --Insert Component types -- exec rsp_InsertComponentDetails     
-- Revision History    
-- M Senthil 28/05/2010  Created    
-- ======================================================================= 

CREATE PROCEDURE rsp_InsertComponentDetails    
(@CompName VARCHAR(50),    
@ModelName VARCHAR(50),    
@CompType int,    
@AlgoType int,    
@Seed VARCHAR(150),    
@Hash VARCHAR(150),  
@IsSuccess INT OUTPUT)    
AS  

SET @IsSuccess = -1  
 
IF NOT EXISTS(SELECT CCD_Name FROM CV_Component_Details WHERE CCD_Name = @CompName AND CCD_CCT_Code = @CompType)  
BEGIN  
	INSERT CV_Component_Details(CCD_Name, CCD_Model_Name, CCD_CCT_Code, CCD_CAT_Code, CCD_Seed_Value, CCD_Hash_Value)     
	Values(@CompName, @ModelName, @CompType, @AlgoType, @Seed, @Hash)  

	SET @Issuccess = (SELECT CCD_ID FROM CV_Component_Details WHERE CCD_Name = @CompName AND CCD_CCT_Code = @CompType)  
END  


GO

