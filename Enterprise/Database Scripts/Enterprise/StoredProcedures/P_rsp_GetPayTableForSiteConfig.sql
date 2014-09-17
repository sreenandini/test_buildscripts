USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetPayTableForSiteConfig]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetPayTableForSiteConfig]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------                           
--                          
-- Description: Get the data from Game Pay table and convert that into XML
--                          
-- Inputs:      NONE                    
--                          
-- Outputs:     Game Pay Table XML Data
--                          

-- =======================================================================                          
--                           
-- Revision History                          
--                           
-- GBabu     22/11/2010     Created    
-- A.Vinod Kumar	07/01/11 Site based paytable
---------------------------------------------------------------------------                    

CREATE PROCEDURE [dbo].[rsp_GetPayTableForSiteConfig]
(	
	@Site_Id INT
)
AS
BEGIN
DECLARE @XMLData XML
	SET @XMLData = (
	SELECT PT.Game_ID, 
		GM.MG_HQ_Game_ID AS HQ_Game_ID,
		PT.Payout, 
		PT.PT_Description, 
		PT.MaxBet, 
		PT.TheoreticalPayout,
		PT.HQ_Paytable_ID
	FROM PAYTABLE PT
		INNER JOIN [dbo].[udf_GetGameLibrary_BySite](@Site_id) GM ON GM.MG_Game_ID = PT.Game_ID
	ORDER BY GM.MG_HQ_Game_ID
	FOR XML PATH('PAYTABLE'), ELEMENTS XSINIL, ROOT('PAYTABLES'))
SELECT CONVERT(VARCHAR(MAX),@XMLData)   
END


GO

