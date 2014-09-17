USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetActiveSiteDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetActiveSiteDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetActiveSiteDetails
-- -----------------------------------------------------------------    
--    
-- Get Active Site Details
--     
-- -----------------------------------------------------------------        
-- Revision History           
--   
-- Yoganandh.P			12/10/2010			Created
-- Venkatesan Haridass  07/05/12            Modified          
-- =================================================================
  

CREATE PROCEDURE [dbo].rsp_GetActiveSiteDetails  
AS  
BEGIN 
SET NOCOUNT ON 
 SELECT   
  Site_ID, Site_Code, Site_Name, SC_ExchangeConnectionSting, SC_TicketConnectionSting
 FROM   
  [dbo].[Site] LEFT OUTER JOIN [dbo].[SiteConnections] ON Site_Code = SC_SiteCode
 WHERE  
  Site_Closed = 0  
END 

GO

