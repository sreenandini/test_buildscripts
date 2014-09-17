USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckMachineInUse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckMachineInUse]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
* Revision History  
*   
* <Name of the owner>         DateCreated       Type(Created/Modified)  Description  
  Kalaiyarasan.P              08-NOV-2012         Created               This SP is used to Check  Machine is IN USE
--Exec  rsp_CheckMachineInUse  4
*/  
CREATE PROCEDURE rsp_CheckMachineInUse
	@Machine_ID INT
AS
BEGIN
	SELECT Machine_Status_Flag
	FROM   MACHINE WITH(NOLOCK)
	WHERE  Machine_ID = @Machine_ID
	       AND Machine_Status_Flag = 1
END


GO

