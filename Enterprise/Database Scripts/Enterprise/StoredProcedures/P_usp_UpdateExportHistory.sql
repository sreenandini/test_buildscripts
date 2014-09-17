USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateExportHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateExportHistory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

      
/*******************************************************************                  
-- ===================================================================================================================================              
-- EXEC [dbo].[usp_UpdateExportHistory] '1175,1176,1177', 'BATCH', '1'              
-- -----------------------------------------------------------------------------------------------------------------------------------              
--              
-- Update the Export History table after exporting the data from exchange to enterprise and vice versa              
--               
select * from export_history where eh_type like 'HOURLY%'              
-- -----------------------------------------------------------------------------------------------------------------------------------              
-- Revision History               
--               
-- 26/05/2008 Sudarsan S Created              
-- 06/06/2008 NaveenChander removed Old Logic of updating records through Reference and Updating directly through EH_ID
-- ===================================================================================================================================              
*/              
              
CREATE PROCEDURE [dbo].[usp_UpdateExportHistory]              
-- @EH_Reference VARCHAR(4000),              
-- @EH_Type VARCHAR(30),              
 @EH_ID Varchar(4000),          
 @EH_Status VARCHAR(100)              
AS              
              
BEGIN              
--declare @EH_ID varchar(4000)            
--              
--DECLARE @Query VARCHAR(5000)              
--DECLARE @Ref VARCHAR(4000)              
-- IF @EH_TYPE='SITEEVENT'            
--  begin            
-- SET @EH_ID=@EH_Reference            
-- SET @Query = 'UPDATE dbo.Export_History SET EH_Status = ' + '''' + @EH_Status + '''' + ', EH_Export_Date = GETDATE()                
--   WHERE  EH_ID IN (' + '''' + REPLACE(@EH_ID, ',', ''',''') + '''' + ')'              
--  end            
--else            
--begin            
-- SET @Query = 'UPDATE dbo.Export_History SET EH_Status = ' + '''' + @EH_Status + '''' + ', EH_Export_Date = GETDATE()                
-- WHERE EH_Type = ' + '''' + @EH_Type + '''' + ' AND EH_Reference1 IN (' + '''' + REPLACE(@EH_Reference, ',', ''',''') + '''' + ')'              
--  end            
--            
--EXEC (@Query)              
          
UPDATE dbo.Export_History SET EH_Status =  @EH_Status, EH_Export_Date = GETDATE()   WHERE EH_ID in ( @EH_ID  ) AND EH_Status <> '100'    
              
END 

GO

