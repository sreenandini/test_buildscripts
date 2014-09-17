USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetStaffList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetStaffList]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[GetStaffList]
as
SELECT
    Staff."Staff_First_Name", Staff."Staff_Last_Name", Staff."Staff_Personel_No",
    Depot."Depot_Name"
FROM
    { oj "Staff" Staff LEFT OUTER JOIN "Depot" Depot ON
        Staff."Depot_ID" = Depot."Depot_ID"}
ORDER BY
    Staff."Staff_Last_Name" ASC
GO

