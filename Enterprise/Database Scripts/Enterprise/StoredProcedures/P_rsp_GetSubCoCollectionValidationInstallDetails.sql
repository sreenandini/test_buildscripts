USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSubCoCollectionValidationInstallDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSubCoCollectionValidationInstallDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-------------------------------------------------------------------------- 
--
-- Description: create an exception record
--
-- Inputs:      See inputs
--
-- Outputs:     Installation_Start_Date,Installation_End_Date,Machine_Name,Machine_Type_Code 
--
-- =======================================================================
-- 
-- Revision History
-- 
-- Renjish N   24/06/08   Created 
-- 
--------------------------------------------------------------------------- 
CREATE PROCEDURE [dbo].[rsp_GetSubCoCollectionValidationInstallDetails]
   
    @Bar_Position_ID  INT

AS 

SELECT  Installation.Installation_Start_Date, 
		Installation.Installation_End_Date,
		Machine_Class.Machine_Name, 
		Machine_Type.Machine_Type_Code
FROM Bar_Position 
INNER JOIN Installation ON Bar_Position.Bar_Position_ID = Installation.Bar_Position_ID
INNER JOIN Machine ON Installation.Machine_ID = Machine.Machine_ID
INNER JOIN Machine_Class ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID
INNER JOIN Machine_Type ON Machine_Class.Machine_Type_ID = Machine_Type.Machine_Type_ID
WHERE Bar_Position.Bar_Position_ID = @Bar_Position_ID

GO

