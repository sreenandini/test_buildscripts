USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCurrentCredits]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCurrentCredits]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------         
--        
-- Description: To get the live meters at enterprise  
--        
-- Inputs:      @Installation_No INT - Installation No   
--        
-- Return:      Set of meters  
--        
-- =======================================================================        
--         
-- Revision History        
--         
-- NaveenChander     28/09/2010     Created     
---------------------------------------------------------------------------         
    
CREATE PROCEDURE rsp_GetCurrentCredits(@InstallationNo Int) AS
BEGIN
	SELECT InstallationID, CurrentCredit FROM CurrentCreditHistory WITH (NOLOCK) WHERE InstallationID = @InstallationNo
	AND Date = (SELECT Max(Date) FROM CurrentCreditHistory WITH (NOLOCK) WHERE InstallationID = @InstallationNo)
END


GO

