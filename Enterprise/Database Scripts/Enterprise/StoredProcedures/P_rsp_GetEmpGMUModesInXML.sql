/************************************************************
 * Frames Employee Mode Details In XML
 * Time: 02/07/2014 10:09:42
 * Author: Aishwarrya V S
 ************************************************************/
USE [Enterprise]
IF EXISTS (
       SELECT TOP 1 1
       FROM   sys.objects o
       WHERE  o.[name] = 'rsp_GetEmpGMUModesInXML'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_GetEmpGMUModesInXML
END

GO

CREATE PROCEDURE rsp_GetEmpGMUModesInXML(@EH_ID VARCHAR(30))
AS
BEGIN
	SET NOCOUNT ON	
	
	SELECT EH_Details
	FROM   EmployeeExportHistory WITH(NOLOCK)
	WHERE  EH_ID = @EH_ID
END
            
    