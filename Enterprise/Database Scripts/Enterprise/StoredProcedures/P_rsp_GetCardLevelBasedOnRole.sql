USE [Enterprise]
GO

IF EXISTS (
       SELECT TOP 1 1
       FROM   sys.objects
       WHERE  [name] = 'rsp_GetCardLevelBasedOnRole'
              AND [type] = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_GetCardLevelBasedOnRole
END
GO

CREATE PROCEDURE rsp_GetCardLevelBasedOnRole
AS
	SET NOCOUNT ON	
	SELECT r.SecurityRoleID,
	       r.RoleName,
	       r.EmpGMUModeGroup,
	       r.CardLevel
	FROM   [ROLE] r WITH (NOLOCK)
GO



