USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetInUseMachinesDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetInUseMachinesDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetInUseMachinesDetails]  
AS  
BEGIN  
 DECLARE  @MachineStatusFlag BIT 
 SET @MachineStatusFlag=1
 
 
 SELECT  DISTINCT M.Machine_Stock_No,
 CASE
 WHEN (M.Machine_Status_Flag=1 ) THEN  @MachineStatusFlag
 ELSE
 0
 END 
 AS Machinestatus
 FROM Machine M WHERE  M.Machine_Status_Flag=1 
 ORDER BY Machine_Stock_No ASC
END 




