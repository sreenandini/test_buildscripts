USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[P_rsp_GetDepotNamesForServiceCall]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[P_rsp_GetDepotNamesForServiceCall]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE P_rsp_GetDepotNamesForServiceCall
AS
BEGIN
	SELECT d.Depot_ID,
	       d.Depot_Name
	FROM   Depot d
	ORDER BY
	       d.Depot_Name
END
GO
