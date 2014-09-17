USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMachineNames]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetMachineNames]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetMachineNames] as 
SELECT Machine_Class_ID as ItemID, Machine_Name as Item
FROM Machine_Class ORDER BY Machine_Name

GO

