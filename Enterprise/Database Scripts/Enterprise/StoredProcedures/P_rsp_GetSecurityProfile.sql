USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSecurityProfile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSecurityProfile]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


--------------------------------------------------------------------------                         
--                        
-- Description: Get Security Info for the user
--                        
-- Inputs:      @CustomerAccessID
--              @SecurityProfileType_ID
--                        
-- Outputs:     NONE
--                        
-- RETURN:      Result Set of Security Info                  
--                        
-- =======================================================================                        
--                         
-- Revision History                        
--                         
-- NaveenChander     19/09/2008     Created  
---------------------------------------------------------------------------                  

CREATE PROCEDURE rsp_GetSecurityProfile(
    @CustomerAccessID INT,
    @SecurityProfileType_ID INT)
AS 
BEGIN

    SELECT 
        Customer_Access_ID
        ,SecurityProfileType_ID
        ,SecurityProfileType_Value
        ,AllowUser
        ,Description 
    FROM 
        SecurityProfile
    WHERE
        @CustomerAccessID = Customer_Access_ID
        AND @SecurityProfileType_ID = SecurityProfileType_ID
END


GO

