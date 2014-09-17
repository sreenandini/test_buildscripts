USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSites]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSites]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].rsp_GetSites
(
	@company INT = 0,  
	@subcompany INT = 0,
	@region INT = 0,
	@area INT=0,
	@Userid INT=null
)
AS
-- =======================================================================    
-- OUTPUT --Get Company details -- exec rsp_GetSites 2         
-- Revision History    
-- Vineetha Mathew	22/03/2010  Created   
-- Yoganandh P		21/07/2010	Modified to fetch Sites for the Sub Company selected or for all Sub Companies under selected Company
--EXEC rsp_GetSites 2,0,0,0,
-- ======================================================================= 
BEGIN

 SELECT DISTINCT  B.Site_ID,B.Site_Code,B.Site_Name,B.Region   
 FROM SecurityProfile A   
 INNER JOIN Site  B ON A.SecurityProfileType_Value = B.Site_ID AND AllowUser = 1 AND A.SecurityProfileType_ID = 2  
 INNER JOIN  Staff_Customer_Access C ON A.Customer_Access_ID =C.Customer_Access_ID  
 INNER JOIN STAFF D ON D.staff_id=C.staff_id  WHERE USERTABLEID=@Userid  
 AND ((@subcompany <> 0 AND B.Sub_Company_ID = @subcompany)  
 OR (@subcompany = 0 AND B.Sub_Company_ID IN (Select F.Sub_Company_ID FROM Sub_Company F WHERE F.Company_ID = @company)))  
 ORDER BY  Site_Name  

END  


GO

