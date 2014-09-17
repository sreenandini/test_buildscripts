USE Enterprise
GO
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_IsMultiCardAssociatedToUser'
   )
    DROP PROCEDURE dbo.rsp_IsMultiCardAssociatedToUser
GO

CREATE PROCEDURE dbo.rsp_IsMultiCardAssociatedToUser
AS
--/*****************************************************************************************************
--DESCRIPTION    : Check whether user is associated to more card when switching to sigle card mode
--CREATED DATE   : 
--MODULE		 : Setting - Other Tab
--Example		 : 
--CHANGE HISTORY :
--------------------------------------------------------------------------------------------------------
--AUTHOR						MODIFIED DATE		DESCRIPTON
--------------------------------------------------------------------------------------------------------

--*****************************************************************************************************/
BEGIN
	SET NOCOUNT ON
	
	DECLARE @IsMultiCardAssociatedToUser BIT
	
	SET @IsMultiCardAssociatedToUser = 0
	
	SELECT TOP 1 @IsMultiCardAssociatedToUser = CASE 
	                  WHEN UserID > 0 THEN 'True'
	                  ELSE 'False'
	             END
	FROM   tblEmployeeCardDetails
	GROUP BY
	       UserID
	HAVING COUNT(UserID) > 1
	
	SELECT @IsMultiCardAssociatedToUser IsMultiCardAssociatedToUser
END
GO