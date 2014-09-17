USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetRoleAccessRoleLnkinXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetRoleAccessRoleLnkinXML]
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
-- Inputs:     RoleID
-- Outputs:    Role Access Link XML
--      
--        
-- =======================================================================        
--         
-- Revision History        
--         
-- Madhu 14/09/09   Created       
---------------------------------------------------------------------------   
Create Proc rsp_GetRoleAccessRoleLnkinXML
(
@RoleID int
)
as
BEGIN
DECLARE @DOCOUT XML 
 DECLARE @DOCOUT1 XML 
IF Exists(SELECT 1 FROM RoleAccessRole_lnk where SecurityRoleID = @RoleID)  
BEGIN  
SET @DOCOUT=( SELECT * FROM RoleAccessRole_lnk   
where SecurityRoleID = @RoleID  
for xml path('RoleAccessLnk'),Root('RoleAccessLnks'))  

set @DOCOUT1 = (select * from [role ]
where SecurityRoleID = @RoleID 
for xml path('Role'), Root('Roles'))

select '<UserRoles>' + convert(varchar(max),@DOCOUT1) + convert(varchar(max),@DOCOUT) + '</UserRoles>'

END  
ELSE  
 BEGIN -- IF all the rights are removed from Exchange admin screen  
 SET @DOCOUT=( SELECT SecurityRoleID = @RoleID,  
      RoleAccessID = 0  
  for xml path('RoleAccessLnk'),Root('RoleAccessLnks'))   


set @DOCOUT1 = (select * from [role ]
where SecurityRoleID = @RoleID 
for xml path('Role'), Root('Roles'))

select '<UserRoles>' + convert(varchar(max),@DOCOUT1) + convert(varchar(max),@DOCOUT) + '</UserRoles>'

 END  
END    

GO

