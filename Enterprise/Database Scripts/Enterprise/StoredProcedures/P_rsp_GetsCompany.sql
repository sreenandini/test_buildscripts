USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetsCompany]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetsCompany]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetsCompany] 
( @company    INT = 0  
) 
AS 
BEGIN 

IF @company = 0    SET @company = NULL  

        SELECT Sub_Company_ID,Sub_Company_Name 
        FROM Sub_Company 
        WHERE ( ( @company IS NULL )OR( @company IS NOT NULL AND Company_ID= @company )) 
        ORDER BY Sub_Company_Name 
END 

GO

