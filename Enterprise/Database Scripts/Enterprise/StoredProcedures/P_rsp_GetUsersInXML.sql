USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetUsersInXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetUsersInXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--        
-- Description: Gets the User in XML to be imported to Exchange      
--        
-- Inputs:     UserID
-- Outputs:    User XML
--      
--      rsp_GetUsersInXML 1  
-- =======================================================================        
--         
-- Revision History        
--         
-- Madhu		14/09/09	Created   
-- kiruba		08/03/2010	Modified -Three columns added in user table    
-- Yoganandh	07/05/2010	Modified - To fetch FirstName and Last Name from Staff table into the XML
--------------------------------------------------------------------------- 
CREATE PROC rsp_GetUsersInXML(@UserID INT)          
AS  

DECLARE @XMLData xml

SET @XMLData = (
        SELECT tU.SecurityUserID,
               tU.WindowsUserName,
               tU.UserName,
               tU.Password,
               tU.LanguageID,
               tU.ChangePassword,
               tS.Staff_First_Name AS FirstName,
               tS.Staff_Last_Name AS LastName,
               ISNULL(tLanguageCulture.CultureInfo, 'en-US') AS CultureInfo,
               ISNULL(tCurrencyCulture.CultureInfo, 'en-US') AS CurrencyCulture,
               ISNULL(tDateCulture.CultureInfo, 'en-US') AS DateCulture,
               ISNULL(tu.CreatedDate, GETDATE()) AS CreatedDate,
               ISNULL(tu.PasswordChangeDate, GETDATE()) AS PasswordChangeDate,
               ISNULL(tu.isReset, 1) AS isReset,
               ISNULL(tu.isLocked, 0) AS isLocked,
               COALESCE(tS.Staff_Terminated, 0) AS IsUserTerminated
        FROM   [User] tU
               LEFT JOIN userlanguages tLanguageCulture
                    ON  tU.languageid = tLanguageCulture.languageid
               LEFT JOIN userlanguages tCurrencyCulture
                    ON  tU.CurrencyCulture = tCurrencyCulture.languageid
               LEFT JOIN userlanguages tDateCulture
                    ON  tU.DateCulture = tDateCulture.languageid
               LEFT JOIN staff tS
                    ON  tU.SecurityUserID = tS.UserTableID
        WHERE  tU.SecurityUserID = @userID 
               FOR XML PATH('User'), ROOT('Users')
    )
  
SELECT  @XMLData
       FOR XML PATH(''),
       TYPE,
       ROOT('UserDetails')      

GO

