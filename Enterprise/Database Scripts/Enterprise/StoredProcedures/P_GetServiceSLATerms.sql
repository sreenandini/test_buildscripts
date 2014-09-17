USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetServiceSLATerms]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetServiceSLATerms]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[GetServiceSLATerms] 
as
SELECT
    SLA_Contract."SLA_Contract_Description", SLA_Contract."SLA_Contract_Response", SLA_Contract."SLA_Contract_Site_Days", SLA_Contract."SLA_Contract_Site_Calls", SLA_Contract."SLA_Contract_Machine_Days", SLA_Contract."SLA_Contract_Machine_Calls",
    SLA."SLA_Name"
FROM
    { oj "SLA_Contract" SLA_Contract INNER JOIN "SLA" SLA ON
        SLA_Contract."SLA_ID" = SLA."SLA_ID"}
ORDER BY
    SLA."SLA_Name" ASC
GO

