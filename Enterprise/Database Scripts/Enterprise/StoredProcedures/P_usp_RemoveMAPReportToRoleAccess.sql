USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_RemoveMAPReportToRoleAccess]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_RemoveMAPReportToRoleAccess]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================    
-- OUTPUT --Remove Mapping Role to Report          
-- Revision History    
-- Vineetha Mathew 22/03/2010  Created    
-- ======================================================================= 
CREATE PROCEDURE [dbo].[usp_RemoveMAPReportToRoleAccess] (@RoleID INT, @ReportID INT)
AS
BEGIN
	IF EXISTS (SELECT * FROM ReportsMenuAccess WHERE SecurityRoleID = @RoleID AND ReportID = @ReportID)
	BEGIN	
		DELETE FROM ReportsMenuAccess WHERE  SecurityRoleID = @RoleID AND ReportID = @ReportID
		UPDATE ReportsMenu SET SecurityRoleID=0,ReportStatus=0 WHERE ReportID=@ReportID  
	END
END

GO

