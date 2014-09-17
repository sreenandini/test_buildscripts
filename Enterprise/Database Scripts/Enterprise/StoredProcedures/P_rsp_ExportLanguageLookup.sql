USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportLanguageLookup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportLanguageLookup]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

                             
CREATE PROCEDURE [dbo].[rsp_ExportLanguageLookup]     
@LanguageLookupID INT 
AS                    
BEGIN    
	IF EXISTS (SELECT 1 FROM LanguageLookup Where ID = @LanguageLookupID )
	BEGIN
		SELECT *
		FROM LanguageLookup Where ID = @LanguageLookupID 
		FOR XML AUTO, ELEMENTS, ROOT('LanguageLookups')    
	END
	ELSE
	BEGIN
		SELECT TOP 1 @LanguageLookupID AS ID, 'DELETECODE' AS Code, '' AS Description 
		FROM Setting 
		LanguageLookup FOR XML AUTO, ELEMENTS, ROOT('LanguageLookups')
	END
END


GO

