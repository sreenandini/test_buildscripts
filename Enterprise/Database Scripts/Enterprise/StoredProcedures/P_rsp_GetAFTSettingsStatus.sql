USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAFTSettingsStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAFTSettingsStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetAFTSettingsStatus    
(@SiteCode VARCHAR(20))  
as    
BEGIN    
 select count(eh_status) AS AFTStatus  from export_history EH inner join Site S  
 ON  EH.EH_Site_Code = S.Site_Code   
 AND  eh_type ='AFTSettings'  and eh_status <> 100 AND Eh_Site_Code = @SiteCode     
END  

GO

