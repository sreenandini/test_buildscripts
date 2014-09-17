USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertEnterpriseEvents]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertEnterpriseEvents]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- usp_InsertEnterpriseEvents  
-- -----------------------------------------------------------------  
--  
-- Insert Enterprise Events - Site Comms Failure, Site Comms Resumed
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- Yoganandh.P		29/06/2010		Created
-- =================================================================   
CREATE PROCEDURE usp_InsertEnterpriseEvents    
(    
	@EventSiteId  Int,    
	@EventFaultSource Int,    
	@EventFaultType  Int     
)    
AS    
BEGIN    
	INSERT INTO Enterprise_Events(Evt_Site_ID, Evt_Datetime, Evt_Fault_Source, Evt_Fault_Type)    
	VALUES(@EventSiteId, GetDate(), @EventFaultSource, @EventFaultType)    
END

GO

