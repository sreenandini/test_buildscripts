USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateSiteActions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateSiteActions]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================  
-- Log any action performed on the site screen.
-- =======================================================================  
-- Revision History
-- Vineetha Mathew 01/12/09  Created
---------------------------------------------------------------------------  
CREATE PROCEDURE [dbo].[usp_UpdateSiteActions]    
	@SiteHistoryId INT,
	@TransactionFlagID INT
AS  

DECLARE @SiteID INT
DECLARE @SiteXML XML
DECLARE @Event VARCHAR(50)

SELECT @SiteID = SiteID FROM Site_History WHERE SiteHistoryID = @SiteHistoryId

SET @Event=(SELECT TRANSACTIONFLAGNAME FROM TRANSACTIONFLAG WHERE TRANSACTIONFLAGID=@TransactionFlagID)

SELECT @SiteXML = (SELECT * FROM dbo.Site WHERE Site_ID = @SiteID 
FOR XML AUTO, Elements)

UPDATE dbo.Site_History
SET CurrentState = @SiteXML,
TransactionFlagName = @Event
WHERE SiteHistoryID = @SiteHistoryId


GO

