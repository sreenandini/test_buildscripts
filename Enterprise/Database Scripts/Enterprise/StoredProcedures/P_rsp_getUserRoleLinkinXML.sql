USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_getUserRoleLinkinXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_getUserRoleLinkinXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------           
--          
-- Description: Gets the User role link in XML to be imported to Exchange        
--          
-- Inputs:     UserID
-- Outputs:    Role XML  
--        
--          
-- =======================================================================          
--           
-- Revision History          
--           
-- Madhu 14/09/09   Created         
---------------------------------------------------------------------------     

create proc rsp_getUserRoleLinkinXML
(
	@UserID int
)
as
Select * from userrole_lnk
where securityUserid = @userID
for xml path('UserRole'),Root('UserRoles')

GO

