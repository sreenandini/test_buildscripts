USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCalendars]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCalendars]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
-- =============================================    
--Purpose: To get active calendars from Enterprise
-- Vineetha Mathew 23/11/09	- To get active calendars for the site
-- =============================================    
*/
CREATE PROCEDURE [dbo].[rsp_GetCalendars]    
 @Site_Code Varchar (50)     
AS    
BEGIN    
----get the site id from site table.  
DECLARE @Site_Id int
    SELECT @Site_Id = Site_Id FROM dbo.Site WHERE Site_Code = @Site_Code  

	IF (@Site_Id IS NOT NULL)
	BEGIN
		SELECT C.Calendar_ID,S.* FROM dbo.Calendar C 
			INNER JOIN dbo.Sub_Company_Calendar SCC ON C.Calendar_ID = SCC.Calendar_ID 
			INNER JOIN dbo.Site S ON SCC.Sub_Company_ID = S.Sub_Company_ID 
		WHERE SCC.Sub_Company_Calendar_Active = 1 AND S.Site_ID = @Site_Id
	END    
END


GO

