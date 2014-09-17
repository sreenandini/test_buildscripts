USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetBarPositionMachineStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetBarPositionMachineStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------   
--  
-- Description: Retrieves the bar positions with machine enabled status Based on Site code  
--  
-- Inputs:     Site Code  
-- Outputs:       
--  
-- =======================================================================  
--   
-- Revision History  
--   
--Anuradha  09 Feb 2009  Created.  
---------------------------------------------------------------------------   
  
CREATE PROCEDURE rsp_GetBarPositionMachineStatus  
(@SITECODE AS VARCHAR(10))  
AS  
SELECT   
 isnull(BAR_POSITION_MACHINE_ENABLED,1) as Bar_Position_Machine_Enabled,BP.BAR_POSITION_ID,BP.BAR_POSITION_NAME   
FROM   
 BAR_POSITION BP  
JOIN  
 INSTALLATION I   
ON I.BAR_POSITION_ID=BP.BAR_POSITION_ID  
JOIN   
 SITE S  
ON   
 S.SITE_ID=BP.SITE_ID  
WHERE  
 I.INSTALLATION_END_DATE IS NULL  
  AND   
 S.SITE_CODE = @SITECODE  
  


GO

