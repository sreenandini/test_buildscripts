USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertSiteActions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertSiteActions]
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
CREATE PROCEDURE [dbo].[usp_InsertSiteActions]    
	@SiteId INT,	
	@TransactionFlagID INT,	
	@UserID INT
AS  

declare @Event varchar(50)
declare @SiteXML xml

SELECT @SiteXML = (SELECT * FROM dbo.Site WHERE Site_ID = @SiteId 
FOR XML AUTO, Elements)

set @event=(SELECT TRANSACTIONFLAGNAME FROM TRANSACTIONFLAG WHERE TRANSACTIONFLAGID=@TransactionFlagID)

INSERT INTO [Site_History]
           ([SiteID]
           ,[TransactionFlagName]
           ,[UserID]
           ,[PreviousState]
           ,[CurrentState]
           ,[CreatedDate]
           ,[Comments])
     VALUES
           	(@SiteId
           ,@event
           ,@UserID
           ,@SiteXML
           ,null
           ,getdate()
           ,'')


GO

