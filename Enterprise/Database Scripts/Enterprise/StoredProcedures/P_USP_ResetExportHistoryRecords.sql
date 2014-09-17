USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_ResetExportHistoryRecords]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_ResetExportHistoryRecords]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


------------------------------------------------------------------------------------------------------------      
--                                
-- Description:                         
--                                
-- =========================================================================================================      
--                                 
-- Revision History                                
--                                 
-- Siva              04/08/2008     reset the records which are in-progress - when the service is stopped        
-------------------------------------------------------------------------------------------------------------      
CREATE PROCEDURE [dbo].[USP_ResetExportHistoryRecords]                          
AS                          
      
Update export_history set eh_status = null where coalesce(eh_status,'')='1'      

GO

