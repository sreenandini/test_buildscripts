USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SL_GetKeyStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SL_GetKeyStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetSL_KeyStatus 
-- -----------------------------------------------------------------    
--    
-- To get keystatus code and keystatus name     
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 30/03/2012 Dinesh Rathinavel Created
--    
-- =================================================================  

CREATE PROCEDURE [dbo].[rsp_SL_GetKeyStatus]
AS  
BEGIN
	SET NOCOUNT ON

	SELECT 
			[KeyStatusID],
			[KeyText]
	FROM [dbo].[SL_KeyStatus]

END

GO

