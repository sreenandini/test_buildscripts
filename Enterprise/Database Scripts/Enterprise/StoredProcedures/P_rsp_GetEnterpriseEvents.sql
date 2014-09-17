USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetEnterpriseEvents]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetEnterpriseEvents]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetEnterpriseEvents  
-- -----------------------------------------------------------------  
--  
-- Get Enterprise Events
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- Yoganandh.P		01/07/2010		Created
-- =================================================================    
CREATE PROCEDURE rsp_GetEnterpriseEvents    
(    
 @startDate		datetime,      
 @endDate		datetime,      
 @siteID		int     
)    
AS    
BEGIN    
SET DATEFORMAT DMY
If @siteID=0 
	Set @siteID = Null

SELECT 
	Enterprise_Events.Evt_Site_ID, Site.Site_Name, Enterprise_Events.Evt_Datetime,    
	COALESCE(call_group.Call_Group_Reference,'Undefined') AS Description_of_event,
	COALESCE(Call_Fault_Description,'Undefined') AS Details_of_the_event     
FROM 
	Enterprise_Events    
INNER JOIN 
	Site ON Site.Site_Id = Enterprise_Events.Evt_Site_ID    
INNER JOIN 
	datapak_fault ON datapak_fault.datapak_fault_code=Enterprise_Events.evt_fault_source     
AND 
	datapak_fault.datapak_fault_supplementary_code=Enterprise_Events.evt_fault_type      
LEFT JOIN 
	Call_Fault ON  Datapak_Fault.Call_Fault_ID = Call_Fault.Call_Fault_ID
LEFT JOIN 
	Call_Group ON  Call_Group.Call_Group_ID = Call_Fault.Call_Group_ID        
WHERE 
	Enterprise_Events.Evt_DateTime >= @startDate      
AND 
	Enterprise_Events.Evt_DateTime <= @EndDate
AND 
	(
		(@siteID IS NULL)         
OR      
		(@siteID IS NOT NULL AND site.site_ID= @siteID)     
	)
ORDER BY 
	Enterprise_Events.Evt_DateTime DESC
END    

GO

