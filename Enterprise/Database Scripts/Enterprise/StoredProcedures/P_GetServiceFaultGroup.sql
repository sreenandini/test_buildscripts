USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetServiceFaultGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetServiceFaultGroup]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[GetServiceFaultGroup]
as
SELECT
    Call_Group."Call_Group_Description", Call_Group."Call_Group_Reference", Call_Group."Call_Group_Downtime", Call_Group."Call_Group_End_Date",
    Call_Fault."Call_Fault_Description", Call_Fault."Call_Fault_Reference", Call_Fault."Call_Fault_End_Date"
FROM
    { oj "Call_Group" Call_Group INNER JOIN "Call_Fault" Call_Fault ON
        Call_Group."Call_Group_ID" = Call_Fault."Call_Group_ID"}
WHERE
    Call_Fault."Call_Fault_End_Date" IS NULL
ORDER BY
    Call_Group."Call_Group_Reference" ASC,
    Call_Fault."Call_Fault_Reference" ASC
GO

