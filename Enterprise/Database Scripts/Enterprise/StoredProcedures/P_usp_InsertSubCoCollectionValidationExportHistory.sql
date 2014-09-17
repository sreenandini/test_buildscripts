USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertSubCoCollectionValidationExportHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertSubCoCollectionValidationExportHistory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------------------- 
--
-- Description: Inserts records of type - 'GETCOLLBYDATE' in the Export_History table.
--
-- Inputs:      See inputs
--
-- =======================================================================
-- 
-- Revision History
-- 
-- Renjish N   24/06/08   Created 
-- 
--------------------------------------------------------------------------- 
CREATE PROCEDURE [dbo].[usp_InsertSubCoCollectionValidationExportHistory]    
 -- Add the parameters for the stored procedure here    
 @Reference1 Varchar(50),
 @Site_Code Varchar(30) 
     
AS    
BEGIN    
  
  INSERT INTO 
	dbo.Export_History(EH_Date,EH_Reference1,EH_Type, EH_Site_Code)    
  VALUES (GETDATE(),@Reference1,'GETCOLLBYDATE', @Site_Code)     
    
END   
    

GO

