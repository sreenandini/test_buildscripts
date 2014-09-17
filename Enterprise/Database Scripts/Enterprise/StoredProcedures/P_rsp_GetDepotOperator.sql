USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDepotOperator]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDepotOperator]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetDepotOperator
AS
BEGIN
	SET NOCOUNT ON
	SELECT Operator_ID,
	       Operator_Name
	FROM   Operator WITH(NOLOCK)
	order by Operator_Name
END

GO

