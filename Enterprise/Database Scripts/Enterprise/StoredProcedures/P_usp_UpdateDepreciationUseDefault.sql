USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateDepreciationUseDefault]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateDepreciationUseDefault]
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
  Kalaiyarasan.P              10-Dec-2012         Created               This SP is used to update depreciation_policy to Machine and Machine Class
																		
  --Exec  usp_UpdateDepreciationUseDefault                                                                   
*/  
  
CREATE PROCEDURE usp_UpdateDepreciationUseDefault
	@Depreciation_Policy_Use_Default
AS
	BIT
	AS
BEGIN
	UPDATE Machine_Class
	SET    Machine_Class.Depreciation_Policy_ID = Machine_Type.Depreciation_Policy_ID
	FROM   Machine_Type
	       INNER JOIN Machine_Class
	            ON  Machine_Type.Machine_Type_ID = Machine_Class.Machine_Type_ID
	WHERE  Machine_Class.Depreciation_Policy_Use_Default = @Depreciation_Policy_Use_Default
	
	UPDATE MACHINE
	SET    MACHINE.Depreciation_Policy_ID = Machine_Class.Depreciation_Policy_ID
	FROM   Machine_Class
	       INNER JOIN MACHINE
	            ON  Machine_Class.Machine_Class_ID = MACHINE.Machine_Class_ID
	WHERE  MACHINE.Depreciation_Policy_Use_Default = @Depreciation_Policy_Use_Default
END


GO

