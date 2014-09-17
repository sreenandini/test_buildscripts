USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_IsDepotExists]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_IsDepotExists]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION fn_IsDepotExists
(  
 @DepotName   VARCHAR(2000),  
 @DepotID     INT,  
 @OperatorID  INT  
)  
RETURNS BIT  
AS  
BEGIN
	  DECLARE @Status BIT
	  SET @Status =0
      IF EXISTS(  
            SELECT 1  
            FROM   DEPOT WITH(NOLOCK)  
            WHERE  RTRIM(LTRIM(depot_name)) = RTRIM(LTRIM(@DepotName))  
                   AND Depot_ID <> @DepotID  
                   AND Supplier_ID = @OperatorID  
        )  
     BEGIN  
         SET @Status =1  
     END  
     ELSE  
     BEGIN  
         SET @Status =0  
     END  
   RETURN @Status
END  

GO

