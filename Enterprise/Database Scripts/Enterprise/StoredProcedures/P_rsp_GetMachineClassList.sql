USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMachineClassList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMachineClassList]
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
  Kalaiyarasan.P              27-NOV-2012         Created               This SP is used to get Machine Class details
  --Exec  rsp_GetMachineClass 7                                                                    
*/  
  
CREATE PROCEDURE rsp_GetMachineClassList
	@Machine_Class_ID INT
AS
BEGIN
	SELECT MC.Machine_Name,
	       MC.Manufacturer_ID,
	       MC.Machine_Class_Model_Code,
	       MC.Machine_Class_DeListed,
	       MC.Machine_Class_Test_Machine,
	       MC.Machine_Class_Category,
	       MC.Depreciation_Policy_ID,
	       MC.Depreciation_Policy_Use_Default,
	       MC.Machine_Class_Release_Date,
	       Manf.Manufacturer_Name,	      
	       MT.Machine_Type_Code,
	       MT.Machine_Type_ID
	FROM   (
	           Machine_Class MC WITH(NOLOCK) LEFT JOIN Manufacturer Manf ON 
	           Manf.Manufacturer_ID = MC.Manufacturer_ID
	       )
	       LEFT JOIN Machine_Type MT  WITH(NOLOCK)
	            ON  MT.Machine_Type_ID = MC.Machine_Type_ID
	WHERE  Machine_Class_ID = @Machine_Class_ID
END

GO

