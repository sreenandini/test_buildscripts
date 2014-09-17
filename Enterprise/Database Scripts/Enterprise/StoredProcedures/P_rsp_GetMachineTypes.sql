USE [Enterprise]
GO

IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetMachineTypes]')
			AND TYPE IN (
				N'P'
				,N'PC'
				)
		)
	DROP PROCEDURE [dbo].[rsp_GetMachineTypes]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetMachineTypes]    Script Date: 06/30/2014 20:13:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_GetMachineTypes]
AS
SELECT Machine_Type_ID
	,Machine_Type_Code
FROM Machine_Type
ORDER BY Machine_Type_Code
GO


