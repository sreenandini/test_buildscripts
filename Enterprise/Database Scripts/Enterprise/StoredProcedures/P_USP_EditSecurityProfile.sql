USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_EditSecurityProfile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_EditSecurityProfile]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------                           
--                          
-- Description: Updates the SecurityProfile Table with the Values.  If a matching  
--              Info Exists the Table is updated Else a New Record is Inserted              
--                          
-- Inputs:      @CustomerAccessID  
--              @SecurityProfileType_ID  
--              @SecurityProfileType_Value  
--              @AllowUser  
--              @Description  
--                          
-- Outputs:     Value IS EITHER INSERTED OR UPDATED  
--                          
-- RETURN:      NONE                    
--                          
-- =======================================================================                          
--                           
-- Revision History                          
--                           
-- NaveenChander     19/09/2008     Created    
---------------------------------------------------------------------------                    
  
CREATE PROCEDURE USP_EditSecurityProfile(  
    @CustomerAccessID INT,  
    @SecurityProfileType_ID INT,  
    @SecurityProfileType_Value VARCHAR(100),  
    @AllowUser BIT,  
    @Description VARCHAR(255)) AS   
BEGIN  
  
    IF EXISTS ( SELECT 1 FROM SecurityProfile WHERE Customer_Access_ID = @CustomerAccessID AND SecurityProfileType_ID = @SecurityProfileType_ID AND SecurityProfileType_Value = @SecurityProfileType_Value )  
    BEGIN  
        UPDATE SecurityProfile SET AllowUser = @AllowUser WHERE Customer_Access_ID = @CustomerAccessID AND SecurityProfileType_ID = @SecurityProfileType_ID AND SecurityProfileType_Value = @SecurityProfileType_Value  
    END  
    ELSE  
    BEGIN  
        INSERT INTO SecurityProfile   
                (Customer_Access_ID  
                ,SecurityProfileType_ID  
                ,SecurityProfileType_Value  
                ,AllowUser  
                ,Description)  
            VALUES   
                (@CustomerAccessID  
                ,@SecurityProfileType_ID  
                ,@SecurityProfileType_Value  
                ,@AllowUser  
                ,@Description)  
    END  
  
END
GO

