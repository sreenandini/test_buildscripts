USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertGameTitle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertGameTitle]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_InsertGameTitle]
	@Game_Category_ID INT,
	@Game_Title VARCHAR(100),
	@Manufacturer_ID INT,
	@IsMultiGame BIT
AS

/* ================================================================================= 
 * Purpose	:	To insert Game title details
 * Author	:	Dinesh Rathinavel
 * Created  :	22/11/2012
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 22/11/2012  	Dinesh Rathinavel    Initial Version
 * ================================================================================= 
*/

BEGIN

		SET NOCOUNT ON
		
		DECLARE @Game_Title_Id INT
      
		INSERT INTO Game_Title 
		(
			Game_Category_ID,
			Game_Title,
			Manufacturer_ID,
			IsMultiGame			
		)
		VALUES
		(
			@Game_Category_ID,
			@Game_Title,
			@Manufacturer_ID,
			@IsMultiGame			
		)
		
		INSERT INTO MeterAnalysis.dbo.Game_Title 
		(
			Game_Category_ID,
			Game_Title,
			Manufacturer_ID,
			IsMultiGame				
		)
		VALUES
		(
			@Game_Category_ID,
			@Game_Title,
			@Manufacturer_ID,
			@IsMultiGame	
		)
		
		SET @Game_Title_Id = SCOPE_IDENTITY()
		
		IF @Game_Title_Id IS NOT NULL
			EXEC usp_InsertExportHistory @Game_Title_Id,'GAMETITLE','ALL'
		
		
END


GO

