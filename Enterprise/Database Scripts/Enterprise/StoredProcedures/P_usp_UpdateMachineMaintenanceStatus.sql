USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateMachineMaintenanceStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateMachineMaintenanceStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-----------------------------------------------------------------------------------------------------      
--      
-- Description: Update Machine Maintenance Status  
-- Inputs:      See Inputs       
--      
-- Outputs:               
--                        
-- =======================================================================      
--       
-- Revision History      
--      
-- Yoganandh P  04/03/2010  Created      
------------------------------------------------------------------------------------------------------   
CREATE PROCEDURE usp_UpdateMachineMaintenanceStatus  
(    
 @doc XML  
)   
AS  
BEGIN  
  
DECLARE @MachineSerialNo Varchar(50)  
DECLARE @MachineStatusFlag Int  
DECLARE @docHandle Int  
  
EXEC sp_xml_preparedocument @docHandle OUTPUT, @doc  
  
SELECT   
 @MachineSerialNo = MachineManufacturersSerialNo,  
 @MachineStatusFlag = MachineStatusFlag  
FROM OPENXML  
 (@docHandle, './MachineDetails', 2)  
WITH  
(  
 MachineManufacturersSerialNo varchar(50) './MachineManufacturersSerialNo',  
 MachineStatusFlag int './MachineStatusFlag'  
)  
  
EXEC sp_xml_removedocument @dochandle  
  
UPDATE   
 Machine   
Set  
 Machine_Status_Flag = @MachineStatusFlag  
WHERE  
 Machine_Manufacturers_Serial_No = @MachineSerialNo  
  
END  

GO

