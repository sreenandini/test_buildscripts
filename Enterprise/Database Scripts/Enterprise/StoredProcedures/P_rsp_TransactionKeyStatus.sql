USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_TransactionKeyStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_TransactionKeyStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================    
-- OUTPUT           To verify the authorization code and site code sent from Exchange with enterprise  
-- =======================================================================    
--exec rsp_TransactionKeyStatus 1
-- Revision History    
-- Vineetha Mathew 12/05/2010 
---------------------------------------------------------------------------    
  
CREATE PROCEDURE rsp_TransactionKeyStatus

(@Siteid int)

AS  
	BEGIN
		SELECT 
			TK.TransactionKeyId,
			TK.TransactionKey,
			TF.TransactionFlagName, 
			TK.CreatedDate,
			TK.ExpiryDate,
			S.Staff_First_Name,
			S.Staff_Last_Name ,			
			CASE 
				 WHEN TK.Void=1 THEN 'Void'				
				 WHEN TK.ExpiryDate <= GETDATE() THEN 'Closed'				
				 ELSE 'Open' END AS Status
		FROM 
			Transactionkeys TK
			JOIN Transactionflag TF ON TK.TransactionFlagID=TF.TransactionFlagID
			JOIN [STAFF] S ON S.Staff_ID=TK.UserID
			JOIN SITE ON SITE.Site_ID=TK.SiteID
		WHERE TK.SiteID=@Siteid
		ORDER BY TK.TransactionKeyID DESC
	END
GO

