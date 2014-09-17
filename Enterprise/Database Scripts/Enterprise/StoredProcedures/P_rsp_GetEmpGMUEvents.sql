 /************************************************************
 * Reterives Employee Event Details
 * Time: 04/09/2013 16:09:42
 * Author: Aishwarrya V S
 ************************************************************/
USE [Enterprise]
IF EXISTS (
       SELECT TOP 1 1
       FROM   sys.objects o
       WHERE  o.[name] = 'rsp_GetEmpGMUEvents'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_GetEmpGMUEvents
END

GO

CREATE PROCEDURE rsp_GetEmpGMUEvents(@RoleID INT)
AS
	SET NOCOUNT ON
	
	SELECT EG.EmpGMUEventId,
	       EG.GMUEventId,
	       GE.GMUEventGroupID    	       
	FROM   EmpGMUEvents EG WITH(NOLOCK)	   
	       INNER JOIN GMUEVENTS GE WITH(NOLOCK)
	            ON  GE.GMUEventID = EG.GMUEventId
	       INNER JOIN [ROLE] R WITH(NOLOCK)
	            ON  R.SecurityRoleID = EG.RoleID
	WHERE  R.SecurityRoleID = @RoleID
GO

