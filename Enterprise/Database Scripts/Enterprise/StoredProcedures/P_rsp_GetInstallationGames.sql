USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetInstallationGames]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetInstallationGames]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*
 *	this stored procedure is to fetch the installation game details
 *
 *	Change History:
 *	
 *	Sudarsan S		16-02-2009		created
*/
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[rsp_GetInstallationGames]
	@InstallationID	INT
AS

BEGIN

	SELECT  IG_ID,
			IG_Game_Position_No, 
			Machine_Name, 
			IG_Denom, 
			IG_Status
	  FROM  dbo.Installation_Game INNER JOIN dbo.Machine_Class ON IG_Game_Title_ID = Machine_Class_ID
	 WHERE  IG_Installation_ID = @InstallationID
	ORDER BY IG_Game_Position_No

END


GO

