USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckAutoModelCodeExists]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckAutoModelCodeExists]
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
  Kalaiyarasan.P              27-NOV-2012         Created               This SP is used to get Auto_Generate_Model_Codes from System_Parameters 
  --Exec  rsp_CheckAutoModelCodeExists 7                                                                    
*/  
  
CREATE PROCEDURE rsp_CheckAutoModelCodeExists

AS
BEGIN
	SELECT System_Parameter_Auto_Generate_Model_Codes
	FROM   System_Parameters
	WHERE  ISNULL(System_Parameter_Auto_Generate_Model_Codes, 0) = 1
END

GO

