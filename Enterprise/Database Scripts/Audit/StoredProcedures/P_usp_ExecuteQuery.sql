USE [Audit]
GO
-- Drop stored procedure if it already exists
IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'usp_ExecuteQuery' 
)
   DROP PROCEDURE dbo.usp_ExecuteQuery
GO

CREATE PROCEDURE dbo.usp_ExecuteQuery	
@Qry VARCHAR(MAX)
AS
--/*****************************************************************************************************
--DESCRIPTION    : To execute dynamic query framed in other SPs
--CREATED DATE   : 
--MODULE		 : Called in Enterprise usp_ResetTable fro factory reset
--Example		 : =============================================
--				   EXECUTE dbo.usp_ExecuteQuery DynamicQry
--				   GO
--				   =============================================
--CHANGE HISTORY :
--------------------------------------------------------------------------------------------------------
--AUTHOR						MODIFIED DATE		DESCRIPTON
--------------------------------------------------------------------------------------------------------
                                                              
--*****************************************************************************************************/
BEGIN
	SET NOCOUNT ON
			
	DECLARE @Message  INT
		
	BEGIN TRY
		EXEC (@Qry)
		SET @Message = 0
	END TRY
	BEGIN CATCH
		SELECT @Message = ERROR_NUMBER()
	END CATCH
	
	RETURN @Message
END
GO
