USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetGMUModes]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetGMUModes]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetGMUModes
AS
	SELECT GMUModeID,
	       GMUMode,
	       G.GMUMOdeGroupID,
	       GMUModedescription,
	       GG.GMUModeGroupName
	FROM   GMUModes G WITH(NOLOCK) 
	       INNER JOIN GMUModeGroup gg WITH(NOLOCK)
	            ON  G.GMUModeGroupID = GG.GMUModeGroupID
GO
