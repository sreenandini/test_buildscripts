USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMachineType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetMachineType]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[GetMachineType] as 
SELECT Machine_Type.Machine_Type_ID as ItemID, Machine_Type.Machine_Type_Code Item FROM 
Machine_Type ORDER BY Machine_Type_Code

GO

