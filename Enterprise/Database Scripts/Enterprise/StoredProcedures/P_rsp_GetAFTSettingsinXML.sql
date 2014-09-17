USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAFTSettingsinXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAFTSettingsinXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetAFTSettingsinXML    
(@SiteCode AS VARCHAR(20),    
 @Denom int)    
AS    
    
BEGIN    
    
if  exists (select '1' from aftsetting a  inner join Site S On a.SiteCode = S.Site_ID WHERE S.site_code = @SiteCode and a.Denom = @Denom)    
 begin    
  SELECT a.*,'ADDUPDATE' TRAN_TYPE  FROM AFTSetting a  inner join Site S On a.SiteCode = S.Site_ID WHERE S.site_code = @SiteCode and a.Denom = @Denom  
     FOR XML PATH('Setting'), root('AFTSettings')    
 end    
else    
 begin    
select * into #aftsettingtemp from aftsetting where 1= 2    --CASE: WHEN A DENOM IS DELETED . 
        insert into #AFTsettingtemp values    
  (     
   @Denom,    
   @SiteCode,    
   0,    
   0,    
   0,    
   0,    
   0,    
   0,    
   0,    
   0,    
   0,    
   0,    
   0,    
   0,    
   0,    
   0,    
   0,    
   0,    
   0,    
  0)    
    
 select *,'DELETE' TRAN_TYPE from  #AFTsettingtemp FOR XML PATH('Setting'), root('AFTSettings')    
 drop table #AFTsettingtemp    
end    
END    

GO

