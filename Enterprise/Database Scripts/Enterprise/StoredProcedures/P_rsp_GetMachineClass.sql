USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMachineClass]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMachineClass]
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
        Kishore S             20-jun-2014             Created          This SP is used to Read  Machine_class table based on the search criteria value;                                                                      based on Staff_ID.    
*/    
CREATE PROCEDURE [rsp_GetMachineClass]
@SearchCriteria varchar(50) 
AS
SET NOCOUNT ON
SELECT Machine_Type.Machine_Type_Code, 
Machine_Class.Machine_Name, 
Machine_Class.Machine_BACTA_Code,
Machine_Class.Machine_Class_ID 
 FROM Machine_Class INNER JOIN 
 Machine_Type ON Machine_Class.Machine_Type_ID = Machine_Type.Machine_Type_ID 
 WHERE Machine_Class.Machine_Name LIKE '%'+@SearchCriteria+'%'
 OR Machine_Class.Machine_Class_Model_Code LIKE '%'+@SearchCriteria+'%'
  OR Machine_Class.Machine_BACTA_Code LIKE '%'+@SearchCriteria+'%' 
 ORDER BY Machine_Type.Machine_Type_Code ASC, Machine_Class.Machine_Name ASC
      
GO
