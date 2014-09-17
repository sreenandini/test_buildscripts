USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckVLTInstallationApproved]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckVLTInstallationApproved]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_CheckVLTInstallationApproved  
-- -----------------------------------------------------------------  
--  
-- Check the VLT installation approval.  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 10/03/10 Renjish Created        
-- =================================================================   
  
CREATE PROCEDURE dbo.rsp_CheckVLTInstallationApproved
@Game_ID INT  
AS  
  
SELECT 1 FROM dbo.Installation_Game_Info IGI
INNER JOIN dbo.Installation I ON IGI.Installation_No = I.Installation_ID
INNER JOIN dbo.Machine M ON I.Machine_ID = M.Machine_ID
INNER JOIN dbo.BMC_AAMS_Details BAD ON M.Machine_Manufacturers_Serial_No = BAD.BAD_Asset_Serial_No
WHERE IGI.HQ_IGI_ID = @Game_ID AND BAD.BAD_AAMS_Entity_Type = 3 AND BAD.BAD_AAMS_Status = 1
	

GO

