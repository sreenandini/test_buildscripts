USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAllServices]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAllServices]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetAllServices  
-- -----------------------------------------------------------------  
--  
-- Get Enterprise Services
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- Yoganandh.P		01/07/2010		Created
-- =================================================================   
CREATE PROCEDURE rsp_GetAllServices
AS
BEGIN
	SELECT 
		Setting_Name, Setting_Value
	FROM 
		Setting
	WHERE
		Setting_Name = 'WindowsServices'
END

GO

