USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_DeleteBaseDenom]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_DeleteBaseDenom]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* Revision History
* Insert the Denom Types. 
* Venkatesan H Created to Store all possible Denomination in seperate table
*/
/*
Select * from BaseDenom
Select * from EBS_Export_History order by 1 desc
 
*/


CREATE PROCEDURE usp_DeleteBaseDenom
(
@Name VARCHAR(10)
)
AS

BEGIN
	
	DECLARE @Description VARCHAR(255),
@SysDelete BIT

SELECT @Description =  [Description] FROM BaseDenom WHERE Name = @Name

	EXEC usp_InsertUpdateBaseDenom @Name = @Name, @Description = @Description, @SysDelete =1
END	
GO

