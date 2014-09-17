USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ImportSiteConfigFromExchange]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ImportSiteConfigFromExchange]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- usp_ImportSiteConfigFromExchange
-- -----------------------------------------------------------------    
--    
-- Import Site configuration from the xml
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 07/05/12 Venkatesan Haridass Created          
-- =================================================================
  

CREATE PROCEDURE [dbo].usp_ImportSiteConfigFromExchange(@XML xml)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @SiteCode VARCHAR(10)
	DECLARE @ExchangeConnectionSting NVARCHAR(4000)
	DECLARE @TicketConnectionSting NVARCHAR(4000)
	
	SELECT @SiteCode = @XML.value('(/SITECONFIG/siteCode)[1]','VARCHAR(10)'),
	 @ExchangeConnectionSting = @XML.value('(/SITECONFIG/ExchangeConnection)[1]','NVARCHAR(4000)'),
	 @TicketConnectionSting = @XML.value('(/SITECONFIG/TicketConnection)[1]','NVARCHAR(4000)')

	IF EXISTS (SELECT 1 FROM [SiteConnections] WHERE SC_SiteCode = @SiteCode)
	BEGIN
		UPDATE [SiteConnections] SET 
									 [SC_ExchangeConnectionSting] = @ExchangeConnectionSting,
									 [SC_TicketConnectionSting] = @TicketConnectionSting
		WHERE [SC_SiteCode] = @SiteCode
		IF @@ERROR <> 0
					RETURN 1
				ELSE
					RETURN 0
	END
	ELSE
	BEGIN
		INSERT INTO [SiteConnections] VALUES (@SiteCode, @ExchangeConnectionSting, @TicketConnectionSting)
		IF @@ERROR <> 0
					RETURN 1
				ELSE
					RETURN 0
	END

END

GO

