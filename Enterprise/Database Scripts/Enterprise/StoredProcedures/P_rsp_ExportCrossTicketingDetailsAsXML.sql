USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportCrossTicketingDetailsAsXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportCrossTicketingDetailsAsXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--      
-- Description: Exports the Cross Ticketing Details as XML    
--      
-- Inputs:  Site Code     
-- Outputs: Client Site Code, Host Site Code, Host Site URL, IsCashableRedeemable, IsPromoRedeemable, Last Updated, IsCrossTicketingEnabled
--      
-- =======================================================================    
    
-- Object:  StoredProcedure [dbo].[rsp_ExportCrossTicketingDetailsAsXML]      
--  Script Date: 03/29/2012 12:10:28   
--  Created By: Lekha       
---------------------------------------------------------------------------  
CREATE PROCEDURE [dbo].[rsp_ExportCrossTicketingDetailsAsXML]      
 @Site_Code VARCHAR(50)   
AS     
BEGIN   
 DECLARE @Siteid INT
 DECLARE @XMLData XML  
  
 SELECT @Siteid=Site_ID FROM Site WHERE Site_Code=@Site_Code  
   
  IF @Siteid>0   
   BEGIN      
       
    SET @XMLData=(SELECT   
       SIA.ClientSiteCode AS ClientSiteCode,
	   SIA.HostSiteCode AS HostSiteCode,
	   SI.Site_Name AS HostSiteName,
	   ISNULL(SIA.IsCashableRedeemable,0) AS IsCashableRedeemable,
	   ISNULL(SIA.IsPromoRedeemable,0) AS IsPromoRedeemable,
	   ISNULL(SI.TicketingURL,'') AS HostSiteURL,
	   LastUpdated AS LastUpdated,
	   ISNULL(SI.IsCrossTicketingEnabled,0) AS IsCrossTicketingEnabled        
      FROM Site SI   
       INNER JOIN SiteAlliance SIA ON SI.Site_Code = SIA.HostSiteCode           
     FOR XML PATH('Site') ,TYPE, ELEMENTS, ROOT('Sites'))      
       
     SELECT @XMLData  
    END  
  ELSE  
   PRINT 'No Such site id'  
END 

GO

