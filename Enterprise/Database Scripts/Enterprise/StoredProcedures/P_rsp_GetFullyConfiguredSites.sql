USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetFullyConfiguredSites]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetFullyConfiguredSites]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--      
-- Description: Fetches the fully configured sites for cross ticketing    
--      
-- Inputs:  SiteCode     
-- Outputs: Site Code, IsCashableRedeemable, IsPromoRedeemable, HostSiteURL          
--      
-- =======================================================================    
    
-- Object:  StoredProcedure [dbo].[rsp_GetFullyConfiguredSites]      
--  Script Date: 03/20/2012 19:19:28   
--  Created By: Lekha       
---------------------------------------------------------------------------    
CREATE PROCEDURE [dbo].[rsp_GetFullyConfiguredSites]  
@SITECODE VARCHAR(50)  
AS  
BEGIN  
  
SELECT  
SI.Site_Code AS ClientSiteCode,
ISNULL(SIA.IsCashableRedeemable,0) AS IsCashableRedeemable,
ISNULL(SIA.IsPromoRedeemable,0) AS IsPromoRedeemable,
(SELECT
SI.TicketingURL
FROM Site SI
WHERE SI.Site_Code=@SITECODE) AS HostSiteURL
FROM Site SI
LEFT OUTER JOIN SiteAlliance SIA ON SI.Site_Code=SIA.ClientSiteCode AND SIA.HostSiteCode=@SITECODE
WHERE (SI.SiteStatus='New'
    OR SI.SiteStatus='FULLYCONFIGURED'
    OR SI.SiteStatus='PARTIALLYCONFIGURED' )
   AND SI.Site_Code <> @SITECODE



END

GO

