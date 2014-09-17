USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteAllRoleAccessRoleLnkinXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteAllRoleAccessRoleLnkinXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--        
-- Description: Gets the role Access link in XML to be imported to Exchange      
--        
-- Inputs:     Site ID
-- Outputs:    Role Access Link XML
--      
--        
-- =======================================================================        
--         
-- Revision History        
--         
-- Vineetha Mathew 06/11/09   Created  
-- Vineetha Mathew 16/11/09   Modified  To fix the issue when XML failed to convert fully to string    
---------------------------------------------------------------------------   
Create Procedure rsp_GetSiteAllRoleAccessRoleLnkinXML
AS		


DECLARE @XMLData XML  
DECLARE @XMLData1 XML 
  
  BEGIN      
   SET @XMLData=( SELECT     
     SecurityRoleID    
     ,RoleAccessID    
     FROM RoleAccessRole_lnk    
   
    FOR XML PATH('RoleAccessLnk'),ROOT('RoleAccessLnks'))  
  
set @XMLData1 = (select * from [role ]  

for xml path('Role'), Root('Roles'))  
  
select '<UserRoles>' + convert(varchar(max),@XMLData) + convert(varchar(max),@XMLData1) + '</UserRoles>'  
 END

GO

