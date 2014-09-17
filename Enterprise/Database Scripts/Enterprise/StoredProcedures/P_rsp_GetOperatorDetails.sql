USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetOperatorDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetOperatorDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetOperatorDetails(@Operator_ID AS INT =NULL)
AS
BEGIN
	SELECT *
	FROM  Operator O WITH(NOLOCK)
	WHERE Operator_ID=COALESCE(@Operator_ID,Operator_ID)
	ORDER BY O.Operator_Name
END

GO

