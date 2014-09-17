USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetMachineTypesForServiceCall]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetMachineTypesForServiceCall]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetMachineTypesForServiceCall  
(@IsNewCall BIT)  
AS  
BEGIN  
IF (@IsNewCall = 1)   
	 select distinct Machine_Type_Code,Machine_Type_Description,Machine_Type.Machine_Type_ID   
	 from Machine  
	 JOIN Installation   
	  ON Installation.Machine_ID = Machine.Machine_ID  
	 LEFT JOIN Machine_Class   
	  ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID  
	 LEFT JOIN Machine_Type   
	  ON Machine_Class.Machine_Type_ID = Machine_Type.Machine_Type_ID  
	 WHERE  Installation.Installation_End_Date IS NULL  ORDER BY Machine_Type_Code
ELSE  
	  SELECT Machine_Type_ID, Machine_Type_Code,Machine_Type_Description from Machine_Type   ORDER BY Machine_Type_Code
END  