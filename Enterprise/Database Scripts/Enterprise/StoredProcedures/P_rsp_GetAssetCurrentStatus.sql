USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAssetCurrentStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAssetCurrentStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetAssetCurrentStatus  
-- -----------------------------------------------------------------  
--  
-- Get the Current Status of the VLT.    
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 04/03/09 Renjish Created        
-- =================================================================  

CREATE PROCEDURE dbo.rsp_GetAssetCurrentStatus
@CodeID VARCHAR(12)
AS

DECLARE @VLPoweredUpDate DATETIME
DECLARE @Type INT

SET @VLPoweredUpDate = (SELECT TOP 1 (E.Evt_Datetime) 
FROM dbo.Event E
INNER JOIN dbo.Installation I ON E.Evt_Installation_ID = I.Installation_ID
INNER JOIN dbo.Machine M ON I.Machine_ID = M.Machine_ID
INNER JOIN dbo.BMC_AAMS_Details BAD ON M.Machine_ID = BAD.BAD_Reference_ID
WHERE BAD.BAD_AAMS_Code = @CodeID AND BAD.BAD_AAMS_Entity_Type = 3
AND E.Evt_Fault_Source = 254 AND E.Evt_Fault_Type IN (4,5) 
ORDER BY E.Evt_Datetime DESC)

SET @Type = (SELECT TOP 1 (E.Evt_Fault_Type) 
FROM dbo.Event E
INNER JOIN dbo.Installation I ON E.Evt_Installation_ID = I.Installation_ID
INNER JOIN dbo.Machine M ON I.Machine_ID = M.Machine_ID
INNER JOIN dbo.BMC_AAMS_Details BAD ON M.Machine_ID = BAD.BAD_Reference_ID
WHERE BAD.BAD_AAMS_Code = @CodeID AND BAD.BAD_AAMS_Entity_Type = 3
AND E.Evt_Fault_Source = 254 AND E.Evt_Fault_Type IN (4,5) 
ORDER BY E.Evt_Datetime DESC)

SELECT CASE ISNULL(BP.Bar_Position_Machine_Enabled, 0) 
	WHEN 1 THEN 1 
	ELSE 0 
	END AS VLTEnabled, 
ISNULL(BP.Bar_Position_Machine_Enabled_Date, CAST(I.Installation_Start_Date + ' ' + I.Installation_Start_Time AS DATETIME)) AS VLTEnabledDate, 
CASE @Type
WHEN 4 THEN 0
ELSE 1 END AS VLTPoweredUp, (ISNULL(@VLPoweredUpDate, CAST(I.Installation_Start_Date + ' ' + I.Installation_Start_Time AS DATETIME))) AS VLPoweredUpDate 
FROM dbo.Machine M
INNER JOIN dbo.BMC_AAMS_Details BAD ON M.Machine_ID = BAD.BAD_Reference_ID
INNER JOIN dbo.Installation I ON I.Machine_ID = M.Machine_ID
INNER JOIN dbo.Bar_Position BP ON I.Bar_Position_ID = BP.Bar_Position_ID
WHERE BAD.BAD_AAMS_Code = @CodeID AND BAD.BAD_AAMS_Entity_Type = 3
AND I.Installation_End_Date IS NULL 


GO

