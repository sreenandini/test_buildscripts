USE [Enterprise]
GO
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_getFactoryResetStatus'
   )
    DROP PROCEDURE dbo.rsp_getFactoryResetStatus
GO

CREATE PROCEDURE dbo.rsp_getFactoryResetStatus
  @SiteID INT
AS
--/*****************************************************************************************************
--DESCRIPTION    :	Creates a XML data reporting the reset status of a site based in Site_ID
--CREATED DATE   : 
--MODULE		 : Used in Data Export service for FACTORYRESET_STATUS type
--Example		 : =============================================
--				   EXECUTE dbo.rsp_getFactoryResetStatus 1
--				   GO
--				   =============================================
--CHANGE HISTORY :
--------------------------------------------------------------------------------------------------------
--AUTHOR						MODIFIED DATE		DESCRIPTON
--------------------------------------------------------------------------------------------------------

--*****************************************************************************************************/
BEGIN
	SET NOCOUNT ON
	
	DECLARE @FRStatusXML AS VARCHAR(MAX)  
	
	--Forms a XML '<FactoryReset><FactoryResetStatus><Site_Code>1002</Site_Code><FactoryResetStatus>Completed</FactoryResetStatus></FactoryResetStatus></FactoryReset>'
	SET @FRStatusXML = (
	        SELECT Site_Code,
	               FactoryResetStatus
	        FROM   [Site]
	        WHERE  Site_ID = @SiteID
	               FOR XML PATH('FactoryResetStatus') ,ELEMENTS,ROOT('FactoryReset')
	    )
	
	SELECT @FRStatusXML
END
GO