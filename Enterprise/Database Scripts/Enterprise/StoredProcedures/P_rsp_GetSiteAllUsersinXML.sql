USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteAllUsersinXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteAllUsersinXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/************************************************************
 * Code formatted by SoftTree SQL Assistant � v4.8.29
 * Time: 09/11/12 18:17:09
 ************************************************************/

-- =======================================================================    
-- OUTPUT    To get all users based on site id  
-- =======================================================================    
-- Revision History  --  Exec  [rsp_GetSiteAllUsersinXML] 1212    
-- Vineetha Mathew 05/11/09  Created  
-- Vineetha Mathew 16/11/09   Modified  To fix the issue when XML failed to convert fully to string   
-- Vineetha Mathew 16/11/09   Modified  added left joins with language,currency & date  
-- Yoganandh P    02/08/10   Modified  included first name and last name  
---------------------------------------------------------------------------    
CREATE PROCEDURE [dbo].[rsp_GetSiteAllUsersinXML]  
 @Site_Code VARCHAR(50)  
AS  
BEGIN  
 DECLARE @Siteid         INT      
 DECLARE @XMLData        XML     
 DECLARE @EMPErrorCodes  XML  

 SELECT @Siteid = site_id  
 FROM   SITE  
 WHERE  site_code = @Site_Code      

 IF @Siteid > 0  
 BEGIN  
     SET @XMLData = (  
             SELECT U.SecurityUserID,  
                    U.WindowsUserName,  
                    U.UserName,  
                    U.Password,  
                    U.LanguageID,  
                    U.ChangePassword,  
                    ISNULL(tLanguageCulture.CultureInfo, 'en-US') AS   
                    CultureInfo,  
                    ISNULL(tCurrencyCulture.CultureInfo, 'en-US') AS   
                    CurrencyCulture,  
                    ISNULL(tDateCulture.CultureInfo, 'en-US') AS DateCulture,  
                    ISNULL(U.CreatedDate, GETDATE()) AS CreatedDate,  
                    ISNULL(U.PasswordChangeDate, GETDATE()) AS   
                    PasswordChangeDate,  
                    ISNULL(U.isReset, '1') AS isReset,  
                    ISNULL(U.isLocked, '1') AS isLocked,  
                    tStaff.Staff_First_Name AS FirstName,  
                    tStaff.Staff_Last_Name AS LastName,  
                    U.EmpCardNumber,  
                    U.IsSingleCardEmployee AS IsSingleCardUser                      
             FROM   [User] U                      
                    INNER JOIN Staff tStaff  
                         ON  U.SecurityUserID = tStaff.UserTableID  
                    LEFT JOIN userlanguages tLanguageCulture  
                         ON  U.languageid = tLanguageCulture.languageid  
                    LEFT JOIN userlanguages tCurrencyCulture  
                         ON  U.CurrencyCulture = tCurrencyCulture.languageid  
                    LEFT JOIN userlanguages tDateCulture  
                         ON  U.DateCulture = tDateCulture.languageid  
                    INNER JOIN Usersite_lnk UL  
                         ON  U.SecurityUserID = UL.SecurityUserID  
            WHERE  UL.Siteid = @Siteid   
                    FOR XML PATH('User') ,TYPE, ROOT('Users')  
         )          

	 DECLARE @EmpXML AS XML        
  
  
	 SET @EmpXML = (        
         SELECT tblempdet.EmpID,        
                tblempdet.EmployeeName,        
                tblempdet.EmployeeCardNumber,        
                tblempdet.CardType,        
                tblempdet.IsActive,                        
                tblempdet.UserID AS UserID,  
                ISNULL(tblempdet.CreatedOn, DATEADD(DAY, -1, GETDATE())) AS         
                CreatedOn,        
                ISNULL(tblempdet.LastModifedDateTime, GETDATE()) AS         
                LastModifiedDateTime,        
                tect.EmpCardType AS EmpCardType,        
                isnull(tblempdet.IsMasterCard,'False') as IsMastercard,        
				isnull(tblempdet.EmployeeFlags,'') as EmployeeFlags ,
				tblempdet.CardLevel AS CardLevel
         FROM   [User]u        
                INNER JOIN tblEmployeeCardDetails tblEmpDet        
                     ON  u.SecurityUserID = tblempdet.UserID    
                INNER JOIN tblEmployeeCardType tect        
                     ON  tect.EmpCardTypeID = tblempdet.CardType
                INNER JOIN Usersite_lnk UL    
                         ON  U.SecurityUserID = UL.SecurityUserID      
                     WHERE UL.Siteid = @Siteid
                     FOR XML PATH('EMP'),        
                TYPE,        
                ROOT('EMPDetails')        
     )
     
     SET @EMPErrorCodes = (  
             SELECT ErrorCode,  
                    ErrorType  
             FROM   tblEmployeeCardErrorCodes   
                    FOR XML PATH('ErrCode'),  
                    TYPE,  
                    ROOT('EmpErrorCodes')  
         )
     
     DECLARE @Modes XML
         
     SET @Modes = (        
         SELECT GMUMode,        
                GMUModedescription,        
                ltrim(rtrim(permission))        
         FROM tblGMUModes tg FOR XML PATH('Mode'),        
                TYPE,        
                ROOT('GMUModes')        
	 )  

     SELECT @XMLData,
			@EmpXML,
			@Modes,  
            @EMPErrorCodes   
            FOR XML PATH(''),  
            TYPE,  
            ROOT('UserDetails')  
 END  
 ELSE  
     PRINT 'No Such site id '  
END  
GO

