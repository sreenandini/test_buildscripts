USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteAFTTransactionsInXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteAFTTransactionsInXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*  
 * this stored procedure is to export the AFTTransactions details to the corresponding Exchange  
 * Change History:     
 * Anil		   17 Mar 2011  Created 
*/  
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  

CREATE PROCEDURE [dbo].[rsp_GetSiteAFTTransactionsInXML]
	@Site_Code VARCHAR(50) 
AS  
  
BEGIN  	
	DECLARE @siteid INT
	DECLARE @xml XML

	SELECT @Siteid = site_id FROM SITE WHERE site_code = @Site_Code
	
		IF @Siteid > 0	
			BEGIN				
				SET @xml = 	(SELECT 
							AFT_Transactions.*
							FROM AFT_Transactions
							WHERE SiteCode=@Site_Code						
							FOR XML PATH ('AFTTransaction'), ELEMENTS, ROOT('AFTTransactions'))

				SELECT @xml 
			 END
END


GO

