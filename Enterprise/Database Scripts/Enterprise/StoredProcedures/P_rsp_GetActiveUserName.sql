USE [Enterprise]
GO

IF EXISTS (
       SELECT TOP 1 1
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetActiveUserName]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetActiveUserName]
GO

CREATE PROCEDURE rsp_GetActiveUserName
AS
BEGIN
	SELECT U.UserName,
	       U.SecurityUserID
	FROM   [user] U WITH(NOLOCK)
	       INNER JOIN STAFF S WITH(NOLOCK)
	            ON  S.UserTableID = U.SecurityUserID
	WHERE  S.Staff_Terminated = 0
	ORDER BY
	       U.UserName
END





