USE ENTERPRISE
GO

IF EXISTS (
       SELECT *
       FROM   DBO.SYSOBJECTS
       WHERE  id = OBJECT_ID(N'[dbo].[rsp_GetAllUsers]')
              AND OBJECTPROPERTY(id, N'IsProcedure') = 1
   )
BEGIN
    DROP PROCEDURE [dbo].[rsp_GetAllUsers]
END
GO

CREATE PROCEDURE rsp_GetAllUsers 
      @SiteID INT
AS
BEGIN
      SELECT DISTINCT (tStaff.Staff_First_Name+','+tStaff.Staff_Last_Name) AS UserName,
             u.SecurityUserID,
             (CASE WHEN ISNULL(ul.SiteID, 0) <> 0 THEN 1 ELSE 0 END) AS IsAssigned,
             ISNULL(ul.SiteID, 0) IsAssignedInitial
      FROM   [user] u WITH(NOLOCK)
			 INNER JOIN dbo.[Staff] tStaff  
                         ON  u.SecurityUserID = tStaff.UserTableID 
             LEFT OUTER JOIN UserSite_lnk ul
                  ON  u.SecurityUserID = ul.SecurityUserID
                  AND ul.Siteid = @SiteID
      ORDER BY 1 ASC
END
GO
