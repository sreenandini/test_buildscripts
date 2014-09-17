USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertException]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertException]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------   
--  
-- Description: create an exception record  
--  
-- Inputs:      See inputs  
--  
-- Outputs:         0 - All Ok  
--              OTHER - SQL Error  
--  
-- =======================================================================  
--   
-- Revision History  
--   
-- C.Taylor   12/09/04   Created   
--   
---------------------------------------------------------------------------   
CREATE PROCEDURE [dbo].[usp_InsertException]  
  
    @Installation_ID INT,  
    @Exception_Type  SMALLINT,  
    @Details         VARCHAR(1000),  
    @Reference       varchar(100) = NULL,  
    @User            varchar(50) = NULL  
  
AS   
  
    SET DATEFORMAT DMY  
  
    DECLARE @Exception_Date DATETIME,   
            @Save_Date      varchar(20),  
            @Save_Time      varchar(8)  
  
      
    SELECT @Exception_Date = GETDATE()  
  
    INSERT INTO Exception (   
                            Exception_Date,  
                            Exception_Time,  
                            Exception_Installation_No,  
                            Exception_Type,   
                            Exception_Details,  
                            Exception_Reference,  
                            Exception_User  
                          )  
                   VALUES  
                          (  
                            CONVERT ( VARCHAR(12), @exception_date, 106 ),    -- date dd/mmm/yyy  
                            CONVERT ( VARCHAR(5), @exception_date, 108 ),     -- time hh:nn  
                            @Installation_ID,  
                            @Exception_Type,  
                            @Details,  
                            @Reference,  
                            @User  
                          )  
  
-- return error (if any)  
--  
RETURN @@ERROR  

GO

