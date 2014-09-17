IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE NAME = 'rsp_GetUsersInXML1211' AND TYPE = 'P')
DROP PROCEDURE rsp_GetUsersInXML1211
GO
--------------------------------------------------------------------------         
--        
-- Description: Gets the User in XML to be imported to Exchange      
--        
-- Inputs:     UserID
-- Outputs:    User XML
--      
--        
-- =======================================================================        
--         
-- Revision History        
--         
-- Madhu		14/09/09	Created   
-- kiruba		08/03/2010	Modified -Three columns added in user table    
-- Yoganandh	07/05/2010	Modified - To fetch FirstName and Last Name from Staff table into the XML
--------------------------------------------------------------------------- 
CREATE PROC rsp_GetUsersInXML1211
(          
 @UserID int          
)          
as          
Select         
 tU.SecurityUserID,    
 tU.WindowsUserName,    
 tU.UserName,    
 tU.Password,    
 tU.LanguageID,    
 tU.ChangePassword,   
 tS.Staff_First_Name AS FirstName,
 tS.Staff_Last_Name AS LastName,   
 isnull(tLanguageCulture.CultureInfo, 'en-US') as CultureInfo,        
 isnull(tCurrencyCulture.CultureInfo, 'en-US') AS CurrencyCulture,        
 isnull(tDateCulture.CultureInfo, 'en-US') AS DateCulture,
 isnull(tu.CreatedDate,getdate()) as CreatedDate,
 isnull(tu.PasswordChangeDate,getdate()) as PasswordChangeDate,
 isnull(tu.isReset,1) as isReset,     
 isnull(tu.isLocked,0) as isLocked	  
from [User] tU      
left join userlanguages tLanguageCulture on tU.languageid = tLanguageCulture.languageid          
left join userlanguages tCurrencyCulture on tU.CurrencyCulture = tCurrencyCulture.languageid       
left join userlanguages tDateCulture on tU.DateCulture = tDateCulture.languageid       
left join staff tS ON tU.SecurityUserID = tS.UserTableID
where tU.SecurityUserID = @userID          
for XML path('User'), Root('Users') 
GO