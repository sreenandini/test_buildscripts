USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetMachineClassID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetMachineClassID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
* Revision History  
*   
* <Name of the owner>         DateCreated       Type(Created/Modified)  Description  
  Kalaiyarasan.P              17-Sep-2012         Created               This SP is used to get temporary machine class id and delete machine class if exists
  --Exec  usp_GetMachineClassID 2                                                                    
*/  
  
CREATE PROCEDURE usp_GetMachineClassID	
	@Machine_Class_ID INT OUTPUT,
	@IsDelete BIT	
AS
BEGIN
	DECLARE @AutoGenerateModelCode  AS BIT,
	        @ModelCodePrefix        AS VARCHAR(10),
	        @ModelCodeLength        AS INT
	
	SELECT @AutoGenerateModelCode = System_Parameter_Auto_Generate_Model_Codes,
	       @ModelCodePrefix = System_Parameter_Model_Code_Prefix,
	       @ModelCodeLength = System_Parameter_Model_Code_Number_Length
	FROM   System_Parameters
	
	IF (ISNULL(@IsDelete, 0)=1)
	BEGIN
	    DELETE 
	    FROM   Machine_Class
	    WHERE  Machine_Class_ID= @Machine_Class_ID
	END
	ELSE IF (ISNULL(@AutoGenerateModelCode, 0) <> 0)
	BEGIN
	    INSERT INTO Machine_Class
	      (
	        Machine_Class_Model_Code
	      )
	    VALUES
	      (
	        'temp'
	      ) 
	      INSERT INTO MeterAnalysis.dbo.Machine_Class
	      (
	        Machine_Class_Model_Code,
	        Machine_Class_Category_ID     
	      )
	    VALUES
	      (
	        'temp',
	        0
	      ) 
	    SELECT @Machine_Class_ID = SCOPE_IDENTITY()
	    
	    UPDATE Machine_Class
	    SET    Machine_Class_Model_Code = @ModelCodePrefix + RIGHT(
	               '0000000' + CONVERT(VARCHAR(25), @Machine_Class_ID),
	               @ModelCodeLength
	    )
	    WHERE Machine_Class_ID= @Machine_Class_ID
	    
	    UPDATE MeterAnalysis.dbo.Machine_Class
	    SET    Machine_Class_Model_Code = @ModelCodePrefix + RIGHT(
	               '0000000' + CONVERT(VARCHAR(25), @Machine_Class_ID),
	               @ModelCodeLength
	    )
	    WHERE Machine_Class_ID= @Machine_Class_ID
	END
	
END

GO

