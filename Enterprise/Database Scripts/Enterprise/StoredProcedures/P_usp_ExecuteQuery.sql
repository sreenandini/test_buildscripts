USE Enterprise
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
--DESCRIPTION    : Executes input DML query and returns negative result if any error
--CREATED DATE   : 
--MODULE		 : Called in usp_ResetTable for factory reset
--Example		 : =============================================
--				   EXECUTE dbo.usp_ExecuteQuery DynamicQry
--				   GO
--				 =============================================
--CHANGE HISTORY :
--------------------------------------------------------------------------------------------------------
--AUTHOR						MODIFIED DATE		DESCRIPTON
--------------------------------------------------------------------------------------------------------
                                                              
--*****************************************************************************************************/
BEGIN
	SET NOCOUNT ON
	
	DECLARE @Message INT
	
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
