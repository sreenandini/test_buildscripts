USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAssetDetailsReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAssetDetailsReport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[rsp_GetAssetDetailsReport]
(
	@Company INT = 0,
	@StockStatus INT = 0
)
AS 
BEGIN

  DECLARE @ProductVersion  varchar(50)        
    SELECT TOP 1 @ProductVersion = 'BMC Version : ' + VersionName        
    FROM VersionHistory         
  ORDER BY VersionDate DESC   

IF @company = 0
		SET @company = NULL
  IF @StockStatus = -1 SET @StockStatus = NULL

SELECT    
			mt.Machine_Type_Code,
            Machine_Stock_No,  
            CASE WHEN ISNULL(Machine_Status_Flag,0) =0 THEN 'IN_STOCK'  
                  WHEN Machine_Status_Flag =1 THEN 'IN_USE'  
                  WHEN Machine_Status_Flag =2 THEN 'UNUSABLE'  
                  WHEN Machine_Status_Flag =3 THEN 'UNDER_REPAIR'  
                  WHEN Machine_Status_Flag =4 THEN 'ON_ORDER'  
                  WHEN Machine_Status_Flag =5 THEN 'DUE_OUT'  
                  WHEN Machine_Status_Flag =6 THEN 'SOLD'  
                  WHEN Machine_Status_Flag =7 THEN 'CONVERTED'  
                  WHEN Machine_Status_Flag =107 THEN 'PLANNEDCONVERSION'  
                  WHEN Machine_Status_Flag =18 THEN 'AAMSPENDINGINSTALLATION'  
                  WHEN Machine_Status_Flag =9 THEN 'AAMSREJECTEDINSTALLATION'  
                  WHEN Machine_Status_Flag =10 THEN 'AAMSPENDINGREMOVAL'  
                  WHEN Machine_Status_Flag =11 THEN 'AAMSREJECTEDREMOVAL'  
                  WHEN Machine_Status_Flag =12 THEN 'AAMSPENDINGTERMINATION'  
                  WHEN Machine_Status_Flag =13 THEN 'TERMINATED'  
                  WHEN Machine_Status_Flag =14 THEN 'AAMSREJECTEDTERMINATION'  
                  WHEN Machine_Status_Flag =18 THEN 'IN_TRANSIT'   
            END AS StockStatus,  
            ActAssetNo,  
            GMUNo,  
            ActSerialNo,  
            CASE WHEN EnrolmentFlag = 1 then 'Asset Number'  
                  WHEN EnrolmentFlag = 2 then 'GMU Number'  
                  ELSE 'Serial Number'  
            END As EnrolmentOn,  
            @ProductVersion as ProductVersion  
      FROM dbo.Machine M
      INNER JOIN dbo.Machine_Class mc ON M.Machine_Class_ID = mc.Machine_Class_ID
      INNER JOIN dbo.Machine_Type mt ON MC.Machine_Type_ID = mt.Machine_Type_ID   
	  WHERE Machine_Status_Flag = ISNUll(@StockStatus,Machine_Status_Flag)
	 
      ORDER BY 
      mt.Machine_Type_Code, 
      M.Machine_Stock_No 

END

GO
