USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetActiveSiteDetailsforuser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetActiveSiteDetailsforuser]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetActiveSiteDetailsforuser  
-- -----------------------------------------------------------------      
--      
-- Get Active Site Details with user authentication 
--       
-- -----------------------------------------------------------------          
-- Revision History             
--      
-- Venkatesan Haridass  11/05/12            Created   
-- Venkatesan Haridass  13/06/12            Modified to get fully configured sites alone     
-- =================================================================  
    
 CREATE PROCEDURE [dbo].rsp_GetActiveSiteDetailsforuser
@SecurityUserID int    
AS    
BEGIN   
SET NOCOUNT ON   
 SELECT     
  Site_ID, Site_Code, Site_Name, SC_ExchangeConnectionSting, SC_TicketConnectionSting  
 FROM     
  [dbo].[Site] 
  LEFT OUTER JOIN [dbo].[SiteConnections] ON Site_Code = SC_SiteCode  
  INNER JOIN UserSite_lnk USL ON USL.SiteID=[Site].Site_ID
 WHERE    
  SecurityUserID=@SecurityUserID AND
  Site_Closed = 0 AND
  SiteStatus = 'FULLYCONFIGURED'
END   

GO

