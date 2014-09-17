USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckModelCodeExist]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckModelCodeExist]
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
  Kalaiyarasan.P              01-NOV-2012         Created               This SP is used to Check  Machine  & Machine_Class 
																		is already exists or not
--Exec  rsp_CheckModelCodeExist  'PARLOUR SYSTEM',4
*/  
CREATE PROCEDURE rsp_CheckModelCodeExist
	@Machine_Class_ID INT,
	@Machine_Class_Model_Code VARCHAR(50),
	@Machine_Name VARCHAR(50),
	@ModelCodeExist BIT OUTPUT,
	@MachineNameExist BIT OUTPUT
AS
BEGIN
	SET @ModelCodeExist = 0
	SET @MachineNameExist = 0
	
	IF EXISTS(
	       SELECT Machine_Class_ID
	       FROM   Machine_Class WITH(NOLOCK)
	       WHERE  ISNULL(Machine_Class_Model_Code, '') = @Machine_Class_Model_Code
	              AND Machine_Class_ID <> @Machine_Class_ID
	   )
	BEGIN
	    SET @ModelCodeExist = 1
	END
	
	IF EXISTS(
	       SELECT Machine_Class_ID
	       FROM   Machine_Class WITH(NOLOCK)
	       WHERE  ISNULL(Machine_Name, '') = @Machine_Name
	              AND Machine_Class_ID <> @Machine_Class_ID
	   )
	BEGIN
	    SET @MachineNameExist = 1
	END
END


GO

