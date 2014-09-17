USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetUserLanguagesDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetUserLanguagesDetails]
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
  Kalaiyarasan.P              25-May-2012         Created               This SP is used to get UserLanguages details     
                                                                             
*/    
    
CREATE PROCEDURE rsp_GetUserLanguagesDetails
AS
BEGIN
	SELECT LanguageID,
	       LanguageName,
	       CultureInfo
	FROM   UserLanguages
END

GO

