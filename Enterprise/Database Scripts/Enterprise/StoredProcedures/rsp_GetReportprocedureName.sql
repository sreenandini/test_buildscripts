USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetReportprocedureName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetReportprocedureName]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

----------------------------------------------------------------------------------------------------    
---      
--- Description: to fetch audit details  
---       
--- Revision History      
---     exec rsp_GetReportprocedureName  'AUDITTRAIL' 
--- Anuradha    26 May 2014    Created    

----------------------------------------------------------------------------------------------------    
CREATE PROCEDURE  rsp_GetReportprocedureName    
@ReportArg VARCHAR(200),
@ProcedureName VARCHAR(200) output
AS

SELECT @ProcedureName = ms_procedureused  FROM reportsmenu WITH (NOLOCK)
 WHERE ReportArgName = @ReportArg
 
 GO