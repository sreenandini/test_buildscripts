USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetListofSuppliers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetListofSuppliers]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[GetListofSuppliers]
as
SELECT Operator_ID as ItemID, Operator_Name as Item FROM Operator

GO

