USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_MAPReportToRoleAccess]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_MAPReportToRoleAccess]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================    
-- OUTPUT --Mapping Role to Report          
-- Revision History    
-- Vineetha Mathew 22/03/2010  Created    
-- =======================================================================   
CREATE PROCEDURE usp_MAPReportToRoleAccess (
   @RoleID INT,
   @ReportID INT)
AS

BEGIN

DECLARE @Val AS INT
SET @Val=0
 
 IF NOT EXISTS (SELECT 1 FROM ReportsMenuAccess WHERE SecurityRoleID = @RoleID AND ReportID = @ReportID)  
 BEGIN 
	INSERT INTO ReportsMenuAccess (SecurityRoleID, ReportID) VALUES (@RoleID, @ReportID)  
	SELECT @Val=ReportMenuId FROM ReportsMenu WHERE isnull(ReportId,'') =isnull(@ReportID,'')
	UPDATE ReportsMenu SET SecurityRoleID=@RoleID,ReportStatus=1 WHERE ReportID=@ReportID
	If @Val <>0 
	BEGIN
		IF NOT EXISTS (SELECT @RoleId, isnull(ReportID,'') FROM ReportsMenuAccess 
				WHERE isnull(ReportId,'') =isnull(@Val,''))  
		BEGIN
			INSERT INTO ReportsMenuAccess (SecurityRoleID, ReportID) 
			SELECT @RoleId ,@Val			  
		END
	END  
 END  
END 



GO

