/************************************************************
 * 
 * XML format and exports to exchange.
 * EXEC rsp_GetGameCappingParametersinXML 1002
 * 
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT TOP 1 1
       FROM   sys.objects
       WHERE  [name] = 'rsp_GetEmpEventsorModesinXML'
              AND [type] = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_GetEmpEventsorModesinXML
END
GO

CREATE PROCEDURE rsp_GetEmpEventsorModesinXML(@EH_ID VARCHAR(30))
AS
	SET NOCOUNT ON	
	
	SELECT EH_Details
	FROM   EmployeeExportHistory WITH(NOLOCK)
	WHERE  EH_ID = @EH_ID
GO






