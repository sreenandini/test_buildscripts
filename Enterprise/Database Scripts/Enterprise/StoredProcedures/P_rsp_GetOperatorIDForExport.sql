USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetOperatorIDForExport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetOperatorIDForExport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetOperatorIDForExport(@Operator_ID INT)
AS
BEGIN
SELECT Site_ID,
       Site_Name
FROM   dbo.Site S
       INNER JOIN dbo.Depot D
            ON  S.Depot_ID = D.Depot_ID
       INNER JOIN dbo.Operator O
            ON  D.Supplier_ID = O.Operator_ID
WHERE  O.Operator_ID = @Operator_ID
END


GO

