USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteCulture]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteCulture]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-----------------------------------------------------------------------------------------------------        
--                
-- Description: Get Site Culture
-- Inputs:                  
--                
-- Outputs:     Result Set - UserName & Culture Information for User
--                                  
-- =======================================================================                
--                 
-- Revision History                
--                
-- Yoganandh P		10/08/2010		Created
-- Yoganandh P		12/08/2010		Modified - Gets UserId as Input and fetches culture information for user
------------------------------------------------------------------------------------------------------    
CREATE PROCEDURE rsp_GetSiteCulture
	@UserId		Int
AS
BEGIN
	SELECT 
		tU.UserName,
        tL.CultureInfo 'LanguageCulture', 
		tC.CultureInfo 'CurrencyCulture', 
		tD.CultureInfo 'DateCulture',
		(SELECT System_Parameter_Region_Culture from System_Parameters) 'RegionCulture'
	FROM 
		[User] tU
	INNER JOIN 
		UserLanguages tL ON tU.LanguageID = tL.LanguageID
	INNER JOIN 
		UserLanguages tC ON tU.CurrencyCulture = tC.LanguageID
	INNER JOIN 
		UserLanguages tD ON tU.DateCulture = tD.LanguageID
	WHERE 
		tU.SecurityUserId = @UserId
END

GO

