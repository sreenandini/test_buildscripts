USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateAssetAAMSDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateAssetAAMSDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================  
-- usp_UpdateAssetAAMSDetails  
-- -----------------------------------------------------------------  
--  
-- Updates Asset AAMS Details.  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 18/11/09 Renjish Created        
-- =================================================================   

CREATE PROCEDURE dbo.usp_UpdateAssetAAMSDetails
@CodeID VARCHAR(50),
@Command VARCHAR (12),
@Comments VARCHAR(100)
AS

UPDATE BMC_AAMS_Details
SET BAD_Entity_Floor_Controller_Status = 0,
BAD_Entity_Command = @Command,
BAD_Updated_Date = GETDATE(),
BAD_Comments = @Comments,
BAD_AAMS_EnableDisable = (CASE @Command 
WHEN 'Enabled' THEN 1 WHEN 'Disabled' THEN 2 ELSE 0 END)
WHERE BAD_AAMS_Code = @CodeID
AND BAD_AAMS_Entity_Type = 3


GO

