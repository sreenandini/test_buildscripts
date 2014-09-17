USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetProfitShareGroupList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetProfitShareGroupList]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetProfitShareGroupList]
AS
BEGIN
	SELECT 
		ProfitShareGroupID,
		ProfitShareGroupName,
		CAST(ISNULL((SELECT B.ProfitSharePercentage
			FROM  (SELECT ProfitSharePercentage, ShareHolderId FROM ProfitShare PS WITH(NOLOCK)      
					WHERE  PS.ProfitShareGroupID = PSG.ProfitShareGroupID AND PS.SysDelete = 0)B  
			WHERE B.ShareHolderId = 3), 0) AS FLOAT) AS ProfitSharePercentage 
	FROM ProfitShareGroup PSG WITH(NOLOCK)
	WHERE PSG.SysDelete = 0
	ORDER BY ProfitShareGroupName
END

GO

