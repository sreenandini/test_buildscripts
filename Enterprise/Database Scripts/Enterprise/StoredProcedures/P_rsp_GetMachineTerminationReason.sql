USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMachineTerminationReason]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMachineTerminationReason]
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
  Kalaiyarasan.P              21-NOV-2012         Created               This SP is used to get Machine Termination Reason details   
                                                              
--Exec  rsp_GetMachineTerminationReason  
*/  
CREATE PROCEDURE rsp_GetMachineTerminationReason
AS
BEGIN
	SELECT MTRT_ID,
	       MTRT_Description,
	       MTRT_Display_Order
	FROM   Machine_Termination_Reason_Types
	ORDER BY
	       MTRT_Display_Order ASC
END


GO

