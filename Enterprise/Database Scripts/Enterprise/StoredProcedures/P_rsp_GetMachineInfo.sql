USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMachineInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMachineInfo]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
* EXEC rsp_GetMachineInfo 2
*       
*/
CREATE PROCEDURE rsp_GetMachineInfo(@MachineClassID INT)
AS
BEGIN
	SELECT Machine_ID,
	       Machine_Stock_No
	FROM   MACHINE
	WHERE  Machine_Class_ID = @MachineClassID
	ORDER BY
	       Machine_Stock_No
END

GO

