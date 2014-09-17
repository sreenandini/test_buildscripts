USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMachineAssetDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMachineAssetDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.rsp_GetMachineAssetDetails
	@Machine_ID INT = NULL
AS
BEGIN
	SELECT Machine_ID,
	       Machine_Stock_No,
	       Machine_MAC_Address,
	       Machine_Class_Model_Code,
	       Machine_Name,
	       Machine_Manufacturers_Serial_No,
	       Machine_Alternative_Serial_Numbers
	FROM   MACHINE
	       INNER JOIN Machine_Class
	            ON  MACHINE.Machine_Class_ID = Machine_Class.Machine_Class_ID
	WHERE  Machine_ID = COALESCE(@Machine_ID, Machine_ID)
END

GO

