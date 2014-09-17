USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Insert_Collection_Batch_Advance]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Insert_Collection_Batch_Advance]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--      
-- Description: It will Insert Advance to retailer value to Collection_Batch_Advance column of  Collection_Batch      
--      
--      
-- =======================================================================      
       
CREATE PROCEDURE [dbo].[usp_Insert_Collection_Batch_Advance]      
       
    @BatchNo      Int,      
    @Collection_Batch_Advance_Value Float      
      
AS       
      
Begin      
      
 Update Batch Set Collection_Batch_Advance  = @Collection_Batch_Advance_Value    
 Where Batch_Id=@BatchNo      
      
End 

GO

