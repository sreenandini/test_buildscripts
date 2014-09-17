USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertBMCLGEExportRecord]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertBMCLGEExportRecord]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- usp_InsertBMCLGEExportRecord  
-- -----------------------------------------------------------------  
--  
-- Insert a new record into LGE_Export_History.    
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 19/11/09 Renjish Created        
-- =================================================================   
  
CREATE PROCEDURE dbo.usp_InsertBMCLGEExportRecord  
@CodeID varchar (50),  
@Type varchar (30),
@Message_ID varchar (20),
@MessReference varchar (50)
AS  
  
DECLARE @Reference varchar(50)

--Need to chnage at some point.
--SELECT @Reference = Machine_Manufacturers_Serial_No FROM Machine WHERE Machine_ID = @CodeID
--SET @Reference = @CodeID

INSERT INTO LGE_Export_History(LGE_EH_Date, LGE_EH_Reference, LGE_EH_Type, LGE_EH_AAMS_Message_ID, LGE_EH_Message_Reference)
VALUES(GETDATE(), @CodeID, @Type, @Message_ID, @MessReference)   


GO

