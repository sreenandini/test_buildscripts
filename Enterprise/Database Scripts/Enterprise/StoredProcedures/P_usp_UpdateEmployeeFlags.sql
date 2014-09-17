USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateEmployeeFlags]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateEmployeeFlags]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
* ********************************************************************************************************
* Revision History
* ********************************************************************************************************
* Update the Employee flags for an Employee card 
*  
* Anuradha			Created			03 Dec 2012
*/

CREATE PROCEDURE usp_UpdateEmployeeFlags(@EmpcardNumber VARCHAR(20))
AS 
BEGIN
	SET NOCOUNT ON
	
--	UPDATE tblEmployeeCardDetails
--	SET    EmployeeFlags = 
--	WHERE  EmployeeCardNumber = @EmpcardNumber
END


GO

