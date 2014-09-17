USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_CheckSiteTicketingURL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_CheckSiteTicketingURL]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--      
-- Description: To check whether the user has entered the ticketing URL during Ticketing Config    
--      
-- Inputs:  SiteCode     
-- Outputs: TicketingURL         
--      
-- =======================================================================    
    
-- Object:  StoredProcedure [dbo].[[usp_CheckSiteTicketingURL]]      
--  Script Date: 03/20/2012 19:19:28   
--  Created By: Lekha       
---------------------------------------------------------------------------    
CREATE PROCEDURE [dbo].[usp_CheckSiteTicketingURL]  
@SITECODE VARCHAR(50)  
AS  
BEGIN  
  
SELECT  
SI.TicketingURL AS TicketingURL
FROM Site SI
WHERE SI.Site_Code =@SITECODE AND (SI.TicketingURL <> NULL OR SI.TicketingURL<>'')

END

GO

