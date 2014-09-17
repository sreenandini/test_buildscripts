USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SL_GetLicenseDateandKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SL_GetLicenseDateandKey]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- [rsp_SL_GetLicenseKey]    
-- -----------------------------------------------------------------    
--    
-- To get License expiry date and License Key
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 20/10/2012 Venkatesan Haridass Created
--    
-- =================================================================  

CREATE PROCEDURE [dbo].[rsp_SL_GetLicenseDateandKey]
	@LicenseInfoId INT,
	@ExpiryDate VARCHAR(100) OUTPUT,
	@LicenseKey VARCHAR(100) OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT @LicenseKey = LI.LicenseKey,
	@ExpiryDate = CONVERT(VARCHAR, LI.[ExpireDate], 120)
	FROM 
	[dbo].[SL_LicenseInfo] LI 
	WHERE LI.[LicenseInfoID] = @LicenseInfoId

END

GO

