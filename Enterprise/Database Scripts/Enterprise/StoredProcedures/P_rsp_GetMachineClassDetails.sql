USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMachineClassDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMachineClassDetails]
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
  Kalaiyarasan.P              17-Sep-2012         Created               This SP is used to get Machine Class details
  --Exec  rsp_GetMachineClassDetails 2,null,null                                                                    
*/  
  
CREATE PROCEDURE  rsp_GetMachineClassDetails 
@Machine_Class_ID INT,
@Machine_Name Varchar(50),
@Manufacturer_ID INT,
@Machine_type_Id INT

AS
BEGIN
	SELECT
		MC.Machine_Class_ID,
		MC.Machine_Name,
		MC.Depreciation_Policy_ID,
		MC.Machine_Class_Category_ID,
		MC.Manufacturer_ID,
		M.Manufacturer_Name,
		MC.Validation_Length
	FROM Machine_Class MC WITH(NOLOCK) 
	LEFT JOIN Manufacturer M WITH(NOLOCK)  ON MC.Manufacturer_ID = M.Manufacturer_ID 
	WHERE MC.Machine_Class_ID = ISNULL(@Machine_Class_ID, MC.Machine_Class_ID)
	AND MC.Machine_Name=ISNULL(@Machine_Name,MC.Machine_Name)
	AND MC.Manufacturer_ID=ISNULL(@Manufacturer_ID,MC.Manufacturer_ID) 
	and MC.Machine_Type_ID=ISNULL(@Machine_type_Id,mc.Machine_Type_ID)         

END


GO

