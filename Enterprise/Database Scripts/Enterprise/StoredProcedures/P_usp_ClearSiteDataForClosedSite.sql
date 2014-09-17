USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ClearSiteDataForClosedSite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ClearSiteDataForClosedSite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================  
-- usp_ClearSiteDataForClosedSite  
-- -----------------------------------------------------------------  
--  
-- Clear active installation details for site.  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 19/11/09 Renjish Created        
-- =================================================================   
CREATE PROCEDURE [dbo].usp_ClearSiteDataForClosedSite
(@Site_Id int)    
   
AS    
    
DECLARE @MachineID AS INT

SELECT @MachineID = ISNULL(NGA_Machine_ID,0) FROM Site WHERE Site_ID = @Site_Id

IF @MachineID <> 0 
	BEGIN
	--Remove the NGA Asset for the site.
	UPDATE Site
	SET NGA_Machine_ID = NULL
	WHERE  Site_ID = @Site_Id 

	--Reset the status of the NGA Asset to In USE.
	UPDATE Machine
	SET Machine_Status_Flag = 0 -- In Stock
	WHERE Machine_ID = @MachineID

	UPDATE BMC_AAMS_Details
	SET BAD_AAMS_Status = 0
	WHERE BAD_Reference_ID = @MachineID
	AND BAD_AAMS_Entity_Type = 3

	--Add entries for AAMS.
	EXEC dbo.usp_InsertBMCBASExportRecord @MachineID, 3, 304, NULL

	END



GO

