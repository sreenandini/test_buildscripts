USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetMachineClassIDForTermsCalculation]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetMachineClassIDForTermsCalculation]
GO

CREATE PROCEDURE [dbo].[rsp_GetMachineClassIDForTermsCalculation] 
(@Installation_ID INT)
AS
BEGIN
	SELECT Machine_Class_ID
	FROM   Installation
	       INNER JOIN MACHINE
	            ON  Installation.Machine_ID = MACHINE.Machine_ID
	WHERE  Installation.Installation_ID = @Installation_ID
END
GO
