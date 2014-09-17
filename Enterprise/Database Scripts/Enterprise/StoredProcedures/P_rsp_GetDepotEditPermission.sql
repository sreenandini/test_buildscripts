USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDepotEditPermission]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDepotEditPermission]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetDepotEditPermission(@HQ_User_Access_ID AS INT)
AS
BEGIN
	SELECT HQ_Admin_Depot_Edit
	FROM   HQ_User_Access WITH(NOLOCK)
	WHERE  HQ_User_Access_ID = @HQ_User_Access_ID
END

GO

