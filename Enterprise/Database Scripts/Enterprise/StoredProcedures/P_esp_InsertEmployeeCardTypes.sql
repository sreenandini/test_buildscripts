USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[esp_InsertEmployeeCardTypes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[esp_InsertEmployeeCardTypes]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
* **********************************************************************************************************
* Revision History
* 
* Anuradha		Created		07/06/2012		
* 
* Store the Employee Card Types		
* 
* **********************************************************************************************************
*/
CREATE PROCEDURE esp_InsertEmployeeCardTypes(@CardType VARCHAR(20), @Result INT = 0 OUTPUT)  
AS  
 SET NOCOUNT ON  
   
 IF NOT EXISTS (  
        SELECT 1  
        FROM   tblEmployeeCardType tect  
        WHERE  tect.EmpCardType = @CardType  
    )  
 BEGIN  
     INSERT INTO tblEmployeeCardType  
       (  
         -- EmpCardTypeID -- this column value is auto-generated,  
         EmpCardType  
       )  
     VALUES  
       (  
         @CardType  
       )  
     SET @Result = 1  
 END  
 ELSE  
 BEGIN  
     SET @Result = 0  
 END  
   
 RETURN @Result  

GO

