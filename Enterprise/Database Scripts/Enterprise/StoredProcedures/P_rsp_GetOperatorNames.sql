USE [Enterprise]
GO

IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetOperatorNames]')
			AND TYPE IN (
				N'P'
				,N'PC'
				)
		)
	DROP PROCEDURE [dbo].[rsp_GetOperatorNames]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetOperatorNames]    Script Date: 06/30/2014 20:13:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_GetOperatorNames]
AS
SELECT Operator_ID
	,Operator_Name
FROM Operator
ORDER BY Operator_Name
GO


