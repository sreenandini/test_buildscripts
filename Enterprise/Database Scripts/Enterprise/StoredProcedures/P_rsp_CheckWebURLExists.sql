USE [Enterprise]
GO
/*
DECLARE @Exists INT
set @Exists=0
EXEC rsp_CheckWebURLExists 'http://10.2.108.36/ExchangeWebService/ExchangeWebService.asmx',@Exists output
select @Exists 
*/
IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_CheckWebURLExists]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_CheckWebURLExists]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE rsp_CheckWebURLExists
	@SiteID INT,
	@WebURl VARCHAR(2000),
	@Exists INT OUTPUT
AS
BEGIN
	SET @Exists = 0
	IF EXISTS (
	       SELECT 1
	       FROM   SITE S
	       WHERE  LTRIM(RTRIM(WebURl)) = LTRIM(RTRIM(@WebURl)) AND (@SiteID = 0 OR S.Site_ID <> @SiteID)
	   )
	    SET @Exists = 1
	ELSE
	    SET @Exists = 0
END




