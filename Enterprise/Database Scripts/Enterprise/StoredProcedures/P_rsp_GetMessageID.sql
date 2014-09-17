USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMessageID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMessageID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetAssetDetailsForAAMSExport  
-- -----------------------------------------------------------------  
--  
-- Get the asset details to be exported for AAMS.  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 02/12/09 Poorna Created        
-- =================================================================   
CREATE PROC [dbo].rsp_GetMessageID
@SequenceNumber int output
AS

Update MessageIDs SET ID = CASE WHEN LEN(ID) + 1 < 11 THEN cast(ID as int) + 1 ELSE 1 end

IF @@ROWCOUNT < 1
	INSERT INTO MessageIDs VALUES (0)
SELECT @SequenceNumber = ID FROM MessageIDs


GO

