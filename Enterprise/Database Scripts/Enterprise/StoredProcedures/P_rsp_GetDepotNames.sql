USE [Enterprise]
GO

IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetDepotNames]')
			AND TYPE IN (
				N'P'
				,N'PC'
				)
		)
	DROP PROCEDURE [dbo].[rsp_GetDepotNames]
GO





/****** Object:  StoredProcedure [dbo].[rsp_GetDepotNames]    Script Date: 06/30/2014 20:13:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rsp_GetDepotNames](
    @Supplier_Id         INT
) as 
SELECT Depot_ID, Depot_Name FROM Depot WHERE Supplier_ID = @Supplier_Id
GO
