USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSiteTree]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSiteTree]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetSiteTree]          
AS            
begin            
  
Select Company_ID,Company_Name from Company    
  
Select c.Company_ID,sc.Sub_Company_ID,sc.Sub_Company_name from Company c   
join Sub_Company sc    
on c.Company_ID = sc.Company_ID    
  
Select s.Sub_Company_ID ,s.site_ID,s.Site_Name,  
s.connectionString   
from Site s   
inner join Sub_Company sc on sc.sub_Company_ID = s.Sub_Company_ID    
inner join Company C on C.Company_ID =  sc.Company_ID  
order by s.Sub_Company_ID   
    
end  
GO

