USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckMACInUse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckMACInUse]
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
  Kalaiyarasan.P              08-NOV-2012         Created               This SP is used to Check  Machine MAC Address is IN USE
--Exec  rsp_CheckMACInUse  4
*/  
CREATE PROCEDURE rsp_CheckMACInUse
	@MAC_Address varchar(17), @Machine_ID INT
AS
BEGIN
	SELECT TOP 1 Machine_ID FROM Machine WITH(NOLOCK)	WHERE Machine_MAC_Address=@MAC_Address  
	AND Machine_ID <> @Machine_ID
END


GO

