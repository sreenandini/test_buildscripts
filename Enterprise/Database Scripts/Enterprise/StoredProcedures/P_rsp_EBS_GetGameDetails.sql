USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_EBS_GetGameDetails]    Script Date: 03/03/2014 18:38:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_EBS_GetGameDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_EBS_GetGameDetails]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_EBS_GetGameDetails]    Script Date: 03/03/2014 18:38:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* 
[rsp_EBS_GetGameDetails] NULL
 
*/

CREATE PROCEDURE [dbo].[rsp_EBS_GetGameDetails]
(
	@SiteCode VARCHAR(50) = NULL,
	@GamePrefix CHAR = NULL
)
AS
BEGIN
	SELECT DISTINCT CAST(GamePrefix AS VARCHAR(1)) AS GameID,
	GameTypeCode AS GameName,
	CAST (1 AS BIT) AS IsActive
	FROM tblCMPGameTypes CGT
	--INNER JOIN MACHINE M ON M.CMPGameType = CGT.GamePrefix
	--INNER JOIN Installation I ON I.Machine_ID = M.Machine_ID
	--INNER JOIN Bar_Position BP ON  BP.Bar_Position_Id = I.Bar_Position_Id
	--INNER JOIN [Site] S ON  BP.Site_Id = S.Site_Id
	WHERE 
	(GamePrefix = COALESCE(@GamePrefix, GamePrefix)) 
	--AND (S.Site_Code = COALESCE(@SiteCode, S.Site_Code))
	AND (CAST(ISNULL(GamePrefix, '') AS VARBINARY(1)) <> 0x00 )
	ORDER BY  GameID, GameName 
END

GO


