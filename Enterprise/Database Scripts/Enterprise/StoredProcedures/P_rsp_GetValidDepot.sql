USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetValidDepot]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetValidDepot]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE rsp_GetValidDepot
AS
BEGIN
SELECT  Operator_Name,Depot_Name from Operator 
INNER JOIN Depot ON Operator.Operator_ID = Depot.Supplier_ID
order by Operator_Name,Depot_Name
END 

