USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertAuditData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertAuditData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------------------- 
--
-- Description: Insert a record into AUDIT_HISTORY table
--
-- Inputs: @User_ID,@User_Name,@Module_ID,@Module_Name,@Screen_Name,@Slot,@Aud_Field,@Old_Value,@New_Value,@Aud_Desc,@Operation_Type   
-- Outputs:    
--
-- =======================================================================
-- 
-- Revision History
-- 
-- Kirubakar S 	 18/03/2010   Created 		

--------------------------------------------------------------------------- 	
CREATE PROCEDURE [dbo].[usp_InsertAuditData]  
      @User_ID    INT,  
      @User_Name  VARCHAR(50),  
      @Module_ID  INT,  
      @Module_Name      VARCHAR(50),  
   @Screen_Name      VARCHAR(50),  
      @Slot       VARCHAR(50),  
      @Aud_Field  VARCHAR(500),  
      @Old_Value  VARCHAR(500),  
      @New_Value  VARCHAR(500),  
      @Aud_Desc   VARCHAR(500),  
   @Operation_Type VARCHAR(25)  
   
AS  
  
BEGIN  
  
      INSERT INTO dbo.Audit_History  
      SELECT GETDATE(), @User_ID, @User_Name, @Module_ID, @Module_Name,@Screen_Name, @Aud_Desc,  
      @Slot, @Aud_Field, @Old_Value, @New_Value,@Operation_Type   
  
END

GO

