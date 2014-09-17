USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SL_GetRuleDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SL_GetRuleDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_SL_ValidateLicenseDates
-- -----------------------------------------------------------------    
--    
-- Verify dates for avoiding duplication in site licenses      
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 30/03/12 Venkatesan Haridass Created          
-- 
-- ================================================================= 
CREATE PROCEDURE [dbo].[rsp_SL_GetRuleDetails]
AS
BEGIN

	SET NOCOUNT ON

	SELECT RuleID, RuleName FROM dbo.SL_Rules ORDER BY RuleName ASC

END

GO

