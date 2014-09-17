USE [Enterprise]
GO
/*
M.Durga - Apr 29,2013- Modified for getting Role Name
*/
IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetUserDetailsByUserID]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetUserDetailsByUserID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


/*
 EXEC dbo.rsp_GetUserDetailsByUserID 1
 */

-- create the procedure
CREATE PROCEDURE dbo.rsp_GetUserDetailsByUserID
(@UserID INT)
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT U.[SecurityUserID],
	       U.[WindowsUserName],
	       U.[UserName],
	       'C' AS PASSWORD,
	       U.[LanguageID],
	       U.[CurrencyCulture],
	       U.[DateCulture],
	       U.[ChangePassword],
	       U.[CreatedDate],
	       U.[PasswordChangeDate],
	       U.[isReset],
	       U. [isLocked],
	       R.RoleName,
	       R.SecurityRoleID
	FROM   [dbo].[USER] U WITH(NOLOCK)
	       INNER JOIN [dbo].[Staff] S WITH(NOLOCK)
	            ON  S.[UserTableID] = U.[SecurityUserID]
	       LEFT OUTER JOIN UserRole_lnk Usrlnk WITH(NOLOCK)
	            ON  U.[SecurityUserID] = Usrlnk.SecurityUserID
	       LEFT OUTER JOIN [ROLE] R WITH(NOLOCK)
	            ON  R.SecurityRoleID = usrlnk.SecurityRoleID
	WHERE  S.[Staff_ID] = @UserID 
	
	-- END
	SET NOCOUNT OFF
END
GO

