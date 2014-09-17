USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetRoleInXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetRoleInXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------         
--        
-- Description: Gets the role in XML to be imported to Exchange      
--        
-- Inputs:     RoleID
-- Outputs:    Role XML
--      
--        
-- =======================================================================        
--         
-- Revision History        
--         
-- Madhu 14/09/09   Created       
---------------------------------------------------------------------------   
create proc rsp_GetRoleInXML
(
	@RoleID int
)
as
select * from [role ]
where SecurityRoleID = @RoleID 
for xml path('Role'), Root('Roles')

GO

