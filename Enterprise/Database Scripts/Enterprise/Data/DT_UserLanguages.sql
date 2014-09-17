/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
 ************************************************************/
USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [UserLanguages]WHERE LanguageName = 'English (US)')
    INSERT [UserLanguages] ( LanguageName, CultureInfo )
    SELECT 'English (US)', 'en-US'
ELSE
    UPDATE [UserLanguages]
    SET    CultureInfo = 'en-US'
    WHERE  LanguageName = 'English (US)'

IF NOT EXISTS(SELECT 1 FROM [UserLanguages]WHERE LanguageName = 'English (UK)')
    INSERT [UserLanguages] ( LanguageName, CultureInfo )
    SELECT 'English (UK)', 'en-GB'
ELSE
    UPDATE [UserLanguages]
    SET    CultureInfo = 'en-GB'
    WHERE  LanguageName = 'English (UK)'

IF NOT EXISTS(SELECT 1 FROM [UserLanguages]WHERE LanguageName = 'Italian')
    INSERT [UserLanguages] ( LanguageName, CultureInfo )
    SELECT 'Italian', 'it-IT'
ELSE
    UPDATE [UserLanguages]
    SET    CultureInfo = 'it-IT'
    WHERE  LanguageName = 'Italian'

IF NOT EXISTS(SELECT 1 FROM [UserLanguages]WHERE LanguageName = 'Spanish(Argentina)')
    INSERT [UserLanguages] ( LanguageName, CultureInfo )
    SELECT 'Spanish(Argentina)', 'es-ar'
ELSE
    UPDATE [UserLanguages]
    SET    CultureInfo = 'es-ar'
    WHERE  LanguageName = 'Spanish(Argentina)'

GO