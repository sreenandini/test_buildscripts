USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetSiteAlliance]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetSiteAlliance]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_GetSiteAlliance] (@Site_Id int)  
--With Encryption  
As    
Begin    
 Select     
 SIA.ClientSiteCode AS ClientSiteCode,
 SIA.HostSiteCode AS HostSiteCode,
 ISNULL(SI.TicketingURL,'') AS HostSiteURL,
 ISNULL(SIA.IsCashableRedeemable,0) AS IsCashableRedeemable,
 ISNULL(SIA.IsPromoRedeemable,0) AS IsPromoRedeemable,
 LastUpdated AS LastUpdated,
 ISNULL(SI.IsCrossTicketingEnabled,0) AS IsCrossTicketingEnabled,
 SI.Site_Name AS HostSiteName
	           
      FROM Site SI   
       INNER JOIN SiteAlliance SIA ON SI.Site_Code = SIA.HostSiteCode 
 
where SI.Site_Code=@Site_Id    
  FOR XML PATH('SiteAlliance'),ROOT('SiteAlliances'), TYPE, Elements XSINIL  
End    

GO

