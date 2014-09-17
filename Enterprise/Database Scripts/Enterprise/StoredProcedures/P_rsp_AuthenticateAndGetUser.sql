USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_AuthenticateAndGetUser]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_AuthenticateAndGetUser]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
/*
M.Durga - Apr 29,2013- Modified for getting Role Name
*/

/*
declare @IsAuthenticated int
 EXEC dbo.rsp_AuthenticateAndGetUser 'test', 'ISMvKXpXpadDiUoOSoAfww==', @IsAuthenticated output
 select @IsAuthenticated
*/

-- create the procedure
CREATE PROCEDURE dbo.rsp_AuthenticateAndGetUser
(
    @UserName         VARCHAR(200),
    @Password         VARCHAR(200),
    @IsAuthenticated  INT OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON
	-- BEGIN
	
	DECLARE @RC                 INT
	DECLARE @UserID             INT 
	
	DECLARE @HQ_User_Access_ID  INT
	DECLARE @User_Group_ID      INT
	DECLARE @SecurityRoleID     INT
	DECLARE @SecurityUserID     INT
	
	-- blank database login
	IF (
	       @UserName = 'friday'
	       AND @Password = 'FuzX/lWq9dap6EDnm9EnQQ=='
	   )
	BEGIN
	    IF NOT EXISTS(
	           SELECT TOP 1 1
	           FROM   dbo.Staff WITH(NOLOCK)
	       )
	    BEGIN
	        -- User Access
	        SELECT TOP 1 @HQ_User_Access_ID = HQ_User_Access_ID
	        FROM   dbo.HQ_User_Access WITH(NOLOCK)
	        
	        IF (ISNULL(@HQ_User_Access_ID, '') = '')
	        BEGIN
	            -- User Access
	            INSERT INTO dbo.HQ_User_Access
	              (
	                HQ_SUPER,
	                HQ_Admin_Users,
	                HQ_Admin_Users_Edit,
	                HQ_Admin_Groups,
	                HQ_Admin_Groups_Edit
	              )
	            VALUES
	              (
	                1,
	                1,
	                1,
	                1,
	                1
	              )
	            SET @HQ_User_Access_ID = SCOPE_IDENTITY()
	        END
	        
	        -- User Group
	        SELECT TOP 1 @User_Group_ID = User_Group_ID
	        FROM   dbo.User_Group WITH(NOLOCK)
	        
	        IF (ISNULL(@User_Group_ID, '') = '')
	        BEGIN
	            -- User Access
	            INSERT INTO dbo.User_Group
	              (
	                User_Group_Name,
	                HQ_User_Access_ID
	              )
	            VALUES
	              (
	                'Super User',
	                @HQ_User_Access_ID
	              )
	            SET @User_Group_ID = SCOPE_IDENTITY()
	        END
	        
	        -- Role
	        SELECT TOP 1 @SecurityRoleID = SecurityRoleID
	        FROM   dbo.[ROLE] WITH(NOLOCK)
	        
	        IF (ISNULL(@SecurityRoleID, '') = '')
	        BEGIN
	            -- User Access
	            INSERT INTO dbo.[Role]
	              (
	                Rolename,
	                RoleDescription
	              )
	            VALUES
	              (
	                'Super User',
	                'Super User'
	              )
	            SET @SecurityRoleID = SCOPE_IDENTITY()
	        END
	        
	        -- User
	        SELECT TOP 1 @SecurityUserID = SecurityUserID
	        FROM   dbo.[User] WITH(NOLOCK)
	        
	        IF (ISNULL(@SecurityUserID, '') = '')
	        BEGIN
	            -- User Access
	            INSERT INTO dbo.[user]
	              (
	                WindowsUserName,
	                Username,
	                [Password],
	                LanguageID,
	                CurrencyCulture,
	                DateCulture,
	                PasswordChangeDate,
	                isReset,
	                isLocked
	              )
	            VALUES
	              (
	                'bmcsysacc',
	                'admin',
	                'ISMvKXpXpadDiUoOSoAfww==',
	                2,
	                2,
	                2,
	                GETDATE(),
	                1,
	                0
	              )
	            SET @SecurityUserID = SCOPE_IDENTITY()
	        END
	        
	        -- user role link
	        IF NOT EXISTS(
	               SELECT 1
	               FROM   dbo.UserRole_lnk ul
	               WHERE  ul.SecurityUserID = @SecurityUserID
	           )
	        BEGIN
	            INSERT INTO dbo.UserRole_lnk
	              (
	                SecurityUserID,
	                SecurityRoleID
	              )
	            VALUES
	              (
	                @SecurityUserID,
	                @SecurityRoleID
	              )
	        END
	        
	        -- Staff
	        INSERT INTO dbo.Staff
	          (
	            Staff_First_Name,
	            Staff_Last_Name,
	            User_Group_ID,
	            Staff_Username,
	            Staff_Password,
	            UserTableID
	          )
	        VALUES
	          (
	            'Admin',
	            'Admin',
	            @User_Group_ID,
	            'admin',
	            '097091104095113',
	            @SecurityUserID
	          )
	        
	        --redirect the new user name and password
	        SET @UserName = 'admin'
	        SET @Password = 'ISMvKXpXpadDiUoOSoAfww=='
	    END
	END
	
	-- Authenticate the user either valid or blank database 
	EXECUTE @RC = [dbo].[rsp_AuthenticateUser] 
	@UserName
	,@Password
	,@UserID OUTPUT
	,@IsAuthenticated OUTPUT 
	
	-- Fetch the user details
	SELECT us.[SecurityUserID],
	       [WindowsUserName],
	       [UserName],
	       'C' AS PASSWORD,
	       [LanguageID],
	       [CurrencyCulture],
	       [DateCulture],
	       [ChangePassword],
	       [CreatedDate],
	       [PasswordChangeDate],
	       [isReset],
	       [isLocked],
	       s.Staff_ID,
	       R.RoleName,
	       R.SecurityRoleID,
	       s.Staff_First_Name AS First_Name, 
	       s.Staff_Last_Name AS Last_Name
	FROM   [dbo].[USER] us WITH(NOLOCK)
	       LEFT JOIN Staff s
	            ON  s.UserTableID = us.SecurityUserID
	       LEFT OUTER JOIN UserRole_lnk Usrlnk WITH(NOLOCK)
	            ON  us.[SecurityUserID] = Usrlnk.SecurityUserID
	       LEFT OUTER JOIN [ROLE] R WITH(NOLOCK)
	            ON  R.SecurityRoleID = usrlnk.SecurityRoleID
	WHERE  us.SecurityUserID = @UserID 
	
	-- END
	SET NOCOUNT OFF
END
GO

