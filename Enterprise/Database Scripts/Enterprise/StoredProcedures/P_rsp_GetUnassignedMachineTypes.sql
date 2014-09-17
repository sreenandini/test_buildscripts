USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetUnassignedMachineTypes]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetUnassignedMachineTypes] 
GO

CREATE PROCEDURE [dbo].[rsp_GetUnassignedMachineTypes] 
(@TermsGroupID INT)
AS
BEGIN
	SET NOCOUNT ON
	SELECT M.Machine_Type_ID,
	       M.Machine_Type_Code
	FROM   dbo.Machine_Type M
	       LEFT JOIN dbo.Terms_Profile T
	            ON  ISNULL(T.Machine_Type_ID, 0) = M.Machine_Type_ID
	            AND T.Terms_Group_ID = @TermsGroupID
	WHERE  T.Machine_Type_ID IS NULL
END
GO
