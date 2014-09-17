USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SL_GetAssociatedSites]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SL_GetAssociatedSites]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- -----------------------------------------------------------------    
--    
-- Get Assosiated site names for perticular Rule     
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 12/10/2012 Venkatesan Haridass Modified to add License Key   
-- =================================================================  

CREATE PROCEDURE [dbo].[rsp_SL_GetAssociatedSites]
(@ruleID INT,
@UserId INT = 0 )
AS  
BEGIN
	SET NOCOUNT ON

	SELECT DISTINCT s.Site_Name
FROM   dbo.[Site] s
       INNER JOIN SL_LicenseInfo sli
            ON  s.Site_ID = sli.Site_ID
       INNER JOIN SecurityProfile SP
	            ON  SP.SecurityProfileType_Value = S.Site_ID
	            AND AllowUser = 1
	            AND SecurityProfileType_ID = 2
	       INNER JOIN STAFF_CUSTOMER_ACCESS SCA
	            ON  SP.Customer_Access_ID = SCA. Customer_Access_ID
	            AND (@UserId = 0 OR SCA.Staff_ID = @UserId)

WHERE  (sli.KeyStatusID = 1 OR sli.KeyStatusID = 2)
       AND sli.RuleID = @ruleID
END

GO

