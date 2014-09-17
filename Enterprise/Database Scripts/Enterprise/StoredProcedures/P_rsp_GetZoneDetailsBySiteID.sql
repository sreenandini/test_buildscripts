USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetZoneDetailsBySiteID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetZoneDetailsBySiteID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetZoneDetailsBySiteID
@SiteID INT
AS
	/*****************************************************************************************************
DESCRIPTION : To display Zone Name  
CREATED DATE: 30.1.2013
MODULE      : BarPosition      
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR						MODIFIED DATE		DESCRIPTON                                                        
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/

BEGIN
SELECT Zone_ID, Zone_Name, Site_ID, Standard_Opening_Hours_ID FROM [Zone] WHERE Site_ID = @SiteID  AND LEN(ZONE_NAME)>0 
ORDER BY Zone_Name
END

GO

