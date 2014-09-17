USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSubCompanyDefault]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSubCompanyDefault]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetSubCompanyDefault]
 @SiteID INT=0
 AS  
BEGIN  
 SET NOCOUNT ON  
 SELECT 
       Terms_Group.Terms_Group_ID,
       Terms_Group.Terms_Group_Name 
 FROM (Sub_Company INNER JOIN Site ON Sub_Company.Sub_Company_ID = Site.Sub_Company_ID)
  LEFT JOIN Terms_Group 
  ON Sub_Company.Terms_Group_ID = Terms_Group.Terms_Group_ID 
  WHERE Site.Site_ID = @SiteID
  
  SELECT 
        Access_Key.Access_Key_ID, Access_Key.Access_Key_Name 
  FROM (Sub_Company INNER JOIN Site ON Sub_Company.Sub_Company_ID = Site.Sub_Company_ID)
  LEFT JOIN Access_Key ON Sub_Company.Access_Key_ID = Access_Key.Access_Key_ID 
  WHERE Site.Site_ID =@SiteID
  
  SELECT 
        Staff.Staff_ID, Staff.Staff_Last_Name, Staff.Staff_First_Name
   FROM (Sub_Company INNER JOIN Site ON Sub_Company.Sub_Company_ID = Site.Sub_Company_ID) 
   LEFT JOIN 
   Staff ON Sub_Company.Staff_ID = Staff.Staff_ID 
   WHERE Site.Site_ID = @SiteID
   
   
   END


GO

