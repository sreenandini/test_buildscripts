USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RSP_GetSiteTree]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RSP_GetSiteTree]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE RSP_GetSiteTree
AS   
BEGIN  
 SELECT   
  tC.Company_ID,  
  tC.Company_Name,  
  tSC.Sub_Company_ID,  
  tSC.Sub_Company_Name,  
  tS.Site_ID,  
  tS.Site_Code,  
  tS.Site_Name,  
  tS.Region,  
  tS.WebURL  
 FROM Site tS  
 INNER JOIN Sub_Company tSC On tSC.Sub_Company_ID = tS.Sub_Company_ID  
 INNER JOIN Company tC On tC.Company_ID = tSC.Company_ID  
 WHERE ISNULL(Site_End_Date, '') = ''  
 ORDER BY tS.Site_Name ASC
END

GO

