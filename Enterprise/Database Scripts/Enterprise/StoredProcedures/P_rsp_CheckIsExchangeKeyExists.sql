USE Enterprise
GO
-- Drop stored procedure if it already exists
IF EXISTS (
              SELECT *
              FROM   INFORMATION_SCHEMA.ROUTINES
              WHERE  SPECIFIC_SCHEMA = N'dbo'
                     AND SPECIFIC_NAME = N'rsp_CheckIsExchangeKeyExists'
          )
    DROP PROCEDURE dbo.rsp_CheckIsExchangeKeyExists
GO

CREATE PROCEDURE dbo.rsp_CheckIsExchangeKeyExists
	@Site_ID INT
AS
--/*****************************************************************************************************
--DESCRIPTION    : Checks whether exchange key is present for the I/P site
--CREATED DATE   :
--MODULE		 : Admin->Sites->Comms
--Example		 : rsp_CheckIsExchangeKeyExists 1
--CHANGE HISTORY :
--------------------------------------------------------------------------------------------------------
--AUTHOR						MODIFIED DATE		DESCRIPTON
--------------------------------------------------------------------------------------------------------

--*****************************************************************************************************/
BEGIN
	SET NOCOUNT ON
	DECLARE @IsExchangeKeyAvailable BIT
	
	SET @IsExchangeKeyAvailable = 0
	
	IF EXISTS(
	    SELECT 1
	    FROM   [Site]
	    WHERE  Site_ID = @Site_ID
	           AND ExchangeKey IS NOT NULL
	)
	BEGIN
		SET @IsExchangeKeyAvailable = 1
	END
	
	SELECT @IsExchangeKeyAvailable AS 'IsExchangeKeyAvailable'
END
GO