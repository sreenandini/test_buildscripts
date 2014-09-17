USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetFIFOStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetFIFOStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
--------------------------------------------------------------------------                   
--                  
-- Description: Gets the current FIFO status from enterprise   
--                  
-- Inputs:      NONE            
--                  
-- Outputs:     Last Record Exported and Unprocessed Record                
--                  
-- RETURN:      NONE            
--                  
-- =======================================================================                  
--                   
-- Revision History                  
--                   
-- Madhu     27/05/2008     Created     
---------------------------------------------------------------------------      
CREATE PROC rsp_GetFIFOStatus  
as  
 
Declare @LastRecord varchar(100)  
Declare @UnProcessed varchar(100)  
  
select @LastRecord  = max(Eh_ID) from Export_history  where isnull(eh_status,'') = '100' 
select @UnProcessed =  count(*)  from export_history where isnull(eh_status,'') <> '100'  
  
select @LastRecord as LastRecord, @UnProcessed as UnProcessed  

GO

