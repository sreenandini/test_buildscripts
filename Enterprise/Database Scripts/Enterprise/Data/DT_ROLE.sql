USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [ROLE]WHERE RoleName = 'Super User')
    INSERT [ROLE] ( RoleName, RoleDescription )
    SELECT 'Super User', 'Super User'
ELSE
    UPDATE [ROLE]
    SET    RoleDescription = 'Super User'
    WHERE  RoleName = 'Super User'
    
GO