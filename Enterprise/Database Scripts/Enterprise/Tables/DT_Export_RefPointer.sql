/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 03/05/2014 7:51:44 PM
 ************************************************************/

USE [Enterprise]
GO

IF NOT EXISTS (
       SELECT 1
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[Export_RefPointer]')
              AND TYPE IN (N'U')
   )
BEGIN
    SET ANSI_NULLS ON
    SET QUOTED_IDENTIFIER ON
    CREATE TABLE [dbo].[Export_RefPointer]
    (
    	RefPointerID      INT IDENTITY(1, 1) NOT NULL,
    	RefPointerType    VARCHAR(50) NOT NULL,
    	RefPointerLastID  INT NOT NULL
    )
    ON [PRIMARY]
    ALTER TABLE [dbo].[Export_RefPointer] ADD CONSTRAINT 
    [PK_Export_RefPointer] PRIMARY KEY CLUSTERED(RefPointerID ASC)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) 
    ON [PRIMARY]
END
GO
