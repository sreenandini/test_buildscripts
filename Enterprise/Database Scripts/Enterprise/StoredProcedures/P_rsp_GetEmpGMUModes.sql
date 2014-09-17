/************************************************************
 * Reterives Employee Mode Details
 * Time: 04/09/2013 16:09:42
 * Author: Aishwarrya V S
 ************************************************************/
USE [Enterprise]
IF EXISTS (
       SELECT TOP 1 1
       FROM   sys.objects o
       WHERE  o.[name] = 'rsp_GetEmpGMUModes'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_GetEmpGMUModes
END

GO

CREATE PROCEDURE rsp_GetEmpGMUModes(@RoleID INT)
AS
	SET NOCOUNT ON
	
	SELECT EM.EmpGMUModeId,
	       EM.GMUModeId,
	       G.GMUMode,
	       G.GMUModeGroupID,
	       R.EmpGMUModeGroup
	FROM   EmpGMUMode EM WITH(NOLOCK)
	       INNER JOIN GMUModes G WITH(NOLOCK)
	            ON  G.GMUModeID = EM.GMUModeId
	       INNER JOIN [ROLE] R WITH(NOLOCK)
	            ON  R.SecurityRoleID = EM.RoleID
	WHERE  R.SecurityRoleID = @RoleID
GO

