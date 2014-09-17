USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMachineNamesOnMachineType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetMachineNamesOnMachineType]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[GetMachineNamesOnMachineType](@mtype AS INT)
AS
BEGIN
	SELECT DISTINCT 
	       Machine_Name AS Machine_Name
	FROM   Machine_Class WITH(NOLOCK)
	WHERE  Machine_Type_ID = @mtype
	ORDER BY
	       Machine_Name
END

GO

