USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EditSetting]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EditSetting]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE usp_EditSetting  
   
    @Setting_ID          int = 0,  
    @Setting_Name        varchar(100) = NULL,  
    @Setting_Value       varchar(100) = NULL,  
    @Setting_Description varchar(100) = NULL  
  
AS  
  
  SET NOCOUNT ON      -- <<< ADO likes this when using temp tables   
  
  IF NOT EXISTS ( SELECT 1   
                    FROM Setting  
                   WHERE (  
                           @Setting_Name IS NOT NULL   
                           AND   
                           Setting_Name = @Setting_Name   
                         )  
                       OR  
                         (   
                           @Setting_ID <> 0   
                           AND   
                           Setting_ID = @Setting_ID  
                         )  
                )  
  
    INSERT INTO Setting  
          ( Setting_Name, Setting_Value )  
      VALUES  
          ( @Setting_Name, @Setting_Value )    
  
  ELSE  
    UPDATE Setting   
       SET Setting_Value = @Setting_Value  
     WHERE (  
             @Setting_Name IS NOT NULL   
             AND   
             Setting_Name = @Setting_Name   
           )  
        OR  
           (   
             @Setting_ID <> 0   
             AND   
             Setting_ID = @Setting_ID  
           )  
  
-- Return Error (if any)  
--  
RETURN @@ERROR  
  

GO

