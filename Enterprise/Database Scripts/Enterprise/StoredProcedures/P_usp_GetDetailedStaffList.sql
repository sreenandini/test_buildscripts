USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetDetailedStaffList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetDetailedStaffList]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_GetDetailedStaffList]
AS
SELECT
    Staff."Staff_First_Name", Staff."Staff_Last_Name", Staff."Staff_IsACollector", Staff."Staff_IsAnEngineer", Staff."Staff_Remote_Inbox", Staff."Staff_Remote_Outbox", Staff."Staff_Username", Staff."Staff_GPS_Location", Staff."Staff_Terminated",
    Depot."Depot_Name",
    User_Group."User_Group_Name"
FROM
    { oj ("Staff" Staff INNER JOIN "User_Group" User_Group ON
        Staff."User_Group_ID" = User_Group."User_Group_ID")
     LEFT OUTER JOIN "Depot" Depot ON
        Staff."Depot_ID" = Depot."Depot_ID"}
ORDER BY
    Staff."Staff_Last_Name" ASC
GO

