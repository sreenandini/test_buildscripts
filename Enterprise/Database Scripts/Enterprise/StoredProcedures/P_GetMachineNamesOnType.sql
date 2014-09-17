USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMachineNamesOnType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetMachineNamesOnType]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[GetMachineNamesOnType](@mtype AS INT)
AS
BEGIN
	SELECT 
		Machine_Class_ID as ItemID, 
		Machine_Name as Item FROM Machine_Class 
	WHERE Machine_Type_ID =@mtype 
	ORDER BY Machine_Name
END

GO

