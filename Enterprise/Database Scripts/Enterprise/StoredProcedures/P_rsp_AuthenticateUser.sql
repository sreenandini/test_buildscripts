USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_AuthenticateUser]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[rsp_AuthenticateUser]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- create the procedure
CREATE PROCEDURE [dbo].[rsp_AuthenticateUser]
(
    @UserName         VARCHAR(200),
    @Password         VARCHAR(200),
    @UserID           INT OUTPUT,
    @IsAuthenticated  INT OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON
	-- BEGIN	
	
	DECLARE @FoundPassword       VARCHAR(200)
	DECLARE @Staff_Terminated    BIT
	DECLARE @IsLocked            BIT
	DECLARE @IsReset             BIT
	DECLARE @PasswordChangeDate  DATETIME
	
	SET @Staff_Terminated = 0
	SET @IsLocked = 0
	SET @IsReset = 0
	SET @UserName = ((ISNULL(@UserName, '')))
	SET @Password = ((ISNULL(@Password, '')))
	SET @IsAuthenticated = 0
	
	DECLARE @Login_Expiry_No_of_Days INT
	EXEC dbo.rsp_GetSetting @Setting_Name = 'Login_Expiry_No_of_Days',
	     @Setting_Value = @Login_Expiry_No_of_Days OUTPUT
	
	SET @Login_Expiry_No_of_Days = COALESCE(@Login_Expiry_No_of_Days, 0)
	
	IF ('' = @UserName)
	BEGIN
	    SET @IsAuthenticated = -1
	END
	
	IF ('' = @Password)
	BEGIN
	    SET @IsAuthenticated = -2
	END
	ELSE
	BEGIN
	    SELECT @UserID = US.SecurityUserID,
	           @FoundPassword = US.Password,
	           @Staff_Terminated = ST.Staff_Terminated,
	           @IsLocked = US.isLocked,
	           @IsReset = US.isReset,
	           @PasswordChangeDate = US.PasswordChangeDate
	    FROM   Staff ST WITH(NOLOCK)
	           INNER JOIN User_Group UG WITH(NOLOCK)
	                ON  ST.User_Group_ID = UG.User_Group_ID
	           INNER JOIN HQ_User_Access HUA WITH(NOLOCK)
	                ON  HUA.HQ_User_Access_ID = UG.HQ_User_Access_ID
	           INNER JOIN [user] US WITH(NOLOCK)
	                ON  ST.UserTableID = US.SecurityUserID
	    WHERE  ST.Staff_Username = @UserName
	    
	    SET @PasswordChangeDate = COALESCE(@PasswordChangeDate, GETDATE())
	    
	    IF (ISNULL(@UserID, 0) <> 0)
	    BEGIN
	        IF (@Staff_Terminated = 1)
	            -- Terminated user
	            SET @IsAuthenticated = -5
	        ELSE 
	        IF (@IsLocked = 1)
	            -- User locked
	            SET @IsAuthenticated = -6
	        ELSE
	        BEGIN
	            IF (ISNULL(@FoundPassword, '') <> @Password)
	                -- Password not matched
	                SET @IsAuthenticated = -4
	            ELSE 
	            IF (@IsReset = 1)
	                -- First Login Since Reset
	                SET @IsAuthenticated = -7
	            ELSE 
	            IF (
	                   DATEDIFF(d, @PasswordChangeDate, GETDATE()) > @Login_Expiry_No_of_Days
	               )
	                -- Password expired.
	                SET @IsAuthenticated = -8
	            ELSE
	                SET @IsAuthenticated = 1
	        END
	    END
	    ELSE
	    BEGIN
	        SET @IsAuthenticated = -3
	    END
	END
	
	--IF (@IsAuthenticated <> 1)
	--    SET @UserID = 0
	
	-- END
	SET NOCOUNT OFF
END

GO

