USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetEmployeeUsersinXML]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetEmployeeUsersinXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


    
-- =======================================================================        
-- OUTPUT    To get all users based on site id      
-- =======================================================================        
-- Revision History  --  Exec  [rsp_GetEmployeeUsersinXML] 1,1212
-- Vineetha Mathew 05/11/09  Created      
-- Vineetha Mathew 16/11/09   Modified  To fix the issue when XML failed to convert fully to string       
-- Vineetha Mathew 16/11/09   Modified  added left joins with language,currency & date      
-- Yoganandh P    02/08/10   Modified  included first name and last name      
---------------------------------------------------------------------------        
CREATE PROCEDURE [dbo].[rsp_GetEmployeeUsersinXML]
(@UserID VARCHAR(50), @SiteCode VARCHAR(50) = NULL)
AS
BEGIN
	DECLARE @UsersXML       XML      
	DECLARE @EMPErrorCodes  XML      
	DECLARE @IsTerminated   BIT
	
	SELECT @IsTerminated = Staff_Terminated
	FROM   Staff
	WHERE  UserTableID = @UserID
	
	--To un-map card from terminated user
	IF @IsTerminated = 1
	BEGIN
	    UPDATE [USER]
	    SET    EmpCardNumber = NULL,
	           IsActiveCard = 0
	    WHERE  SecurityUserID = @UserID
	    
	    UPDATE tblEmployeeCardDetails
	    SET    UserID = NULL
	    WHERE  UserID = @UserID
	END
	
	SET @usersxml = (
	        SELECT DISTINCT U.SecurityUserID,
	               U.WindowsUserName,
	               U.UserName,
	               U.Password,
	               U.LanguageID,
	               U.ChangePassword,
	               ISNULL(tLanguageCulture.CultureInfo, 'en-US') AS CultureInfo,
	               ISNULL(tCurrencyCulture.CultureInfo, 'en-US') AS 
	               CurrencyCulture,
	               ISNULL(tDateCulture.CultureInfo, 'en-US') AS DateCulture,
	               ISNULL(U.CreatedDate, GETDATE()) AS CreatedDate,
	               ISNULL(U.PasswordChangeDate, GETDATE()) AS PasswordChangeDate,
	               ISNULL(U.isReset, '1') AS isReset,
	               ISNULL(U.isLocked, '1') AS isLocked,
	               COALESCE(tStaff.Staff_Terminated, 0) AS IsUserTerminated,
	               tStaff.Staff_First_Name AS FirstName,
	               tStaff.Staff_Last_Name AS LastName,
	               ISNULL(U.EmpCardNumber, '') AS EmpCardNumber,
	               ISNULL(U.IsSingleCardEmployee, 0) AS IsSingleCardUser
	        FROM   [User] U
	               INNER JOIN Staff tStaff
	                    ON  U.SecurityUserID = tStaff.UserTableID
	               INNER  JOIN userlanguages tLanguageCulture
	                    ON  U.languageid = tLanguageCulture.languageid
	               INNER JOIN userlanguages tCurrencyCulture
	                    ON  U.CurrencyCulture = tCurrencyCulture.languageid
	               INNER JOIN userlanguages tDateCulture
	                    ON  U.DateCulture = tDateCulture.languageid
	               INNER JOIN Usersite_lnk UL
	                    ON  U.SecurityUserID = UL.SecurityUserID
	               INNER JOIN SITE S
	                    ON  UL.SiteID = S.Site_ID
	        WHERE  U.SecurityUserId = @UserID
	               AND S.Site_Code = @SiteCode 
	                   FOR XML PATH('User') ,TYPE, ROOT('Users')
	    )      
	
	
	DECLARE @EmpXML AS XML      
	
	
	SET @EmpXML = (
	        SELECT tblempdet.EmpID,
	               tblempdet.EmployeeName,
	               tblempdet.EmployeeCardNumber,
	               R.EmpGMUModeGroup AS CardType,
	               tblempdet.IsActive,
	               CASE 
	                    WHEN @IsTerminated = 1 THEN NULL
	                    ELSE tblempdet.UserID
	               END AS UserID,
	               ISNULL(tblempdet.CreatedOn, DATEADD(DAY, -1, GETDATE())) AS 
	               CreatedOn,
	               ISNULL(tblempdet.LastModifedDateTime, GETDATE()) AS 
	               LastModifiedDateTime,	               
	               ISNULL(tblempdet.IsMasterCard, 'False') AS IsMastercard,
	               R.CardLevel AS CardLevel,
	               ISNULL(tblempdet.EmployeeFlags, '') AS EmployeeFlags
	        FROM   [User]u
	               INNER JOIN tblEmployeeCardDetails tblEmpDet WITH(NOLOCK)
	                    ON  u.SecurityUserID = tblempdet.UserID
	               INNER JOIN UserRole_lnk url1
	                    ON  url1.SecurityUserID = u.SecurityUserID
	               INNER JOIN [role] r
	                    ON  r.SecurityRoleID = url1.SecurityRoleID
	               LEFT JOIN GMUMODEGROUP GM WITH(NOLOCK)
	                    ON  GM.GMUModeGroupID = R.EmpGMUModeGroup
	        WHERE  u.SecurityUserID = @UserID
	               FOR XML PATH('EMP'), 
	               TYPE, 
	               ROOT('EMPDetails')
	    )     
	
	SET @EMPErrorCodes = (
	        SELECT ErrorCode,
	               ErrorType,
	               @EMPErrorCodes
	        FROM   tblEmployeeCardErrorCodes 
	               FOR XML PATH('ErrCode'),
	               TYPE,
	               ROOT('EmpErrorCodes')
	    )      
	
	DECLARE @Modes XML       
	
	SET @Modes = (
	        SELECT GMUMode,
	               GMUModedescription,
	               CASE 
	                    WHEN GMUModeGroupID = 1 THEN 'R'
	                    WHEN GMUModeGroupID = 2 THEN 'W'
	                    ELSE 'RW'
	               END AS permission
	        FROM   GMUModes g WITH (NOLOCK) FOR XML PATH('Mode'),
	               TYPE,
	               ROOT('GMUModes')
	    )       
	
	SELECT @UsersXML,
	       @EmpXML,
	       @Modes,
	       @EMPErrorCodes FOR XML PATH(''),
	       TYPE,
	       ROOT('UserDetails')
END
GO


