USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateModelType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateModelType]
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
  Kalaiyarasan.P              18-Sep-2012         Created               This SP is used to Update Model Type details
  --Exec  usp_UpdateModelType                                                                   
*/  
  
CREATE PROCEDURE usp_UpdateModelType
	@MT_Model_Name VARCHAR(20),
	@MT_Model_Desc VARCHAR(50),
	@IsNGA BIT,
	@MT_ID INT OUTPUT
AS
BEGIN
	IF NOT EXISTS(
	       SELECT 1
	       FROM   Model_Type
	       WHERE  (MT_Model_Name = ISNULL(@MT_Model_Name, '') AND (MT_IsNGA = @IsNGA))
	   )
	BEGIN
	    INSERT INTO Model_Type
	      (
	        MT_Model_Name,
	        MT_Model_Desc,
	        MT_IsNGA
	      )
	    VALUES
	      (
	        @MT_Model_Name,
	        @MT_Model_Desc,
	        @IsNGA
	      )
	    SELECT @MT_ID = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
	    UPDATE Model_Type
	    SET    MT_Model_Desc = @MT_Model_Desc,
	           MT_IsNGA = @IsNGA
	    WHERE  (@MT_ID<>0 AND MT_ID = @MT_ID) OR (MT_Model_Name = @MT_Model_Name AND MT_IsNGA = @IsNGA)
	END
END

GO

