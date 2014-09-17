 USE [Enterprise]
 GO
 
 IF EXISTS (
        SELECT *
        FROM   sys.objects
        WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_SelectZones]')
               AND TYPE IN (N'P', N'PC')
    )
     DROP PROCEDURE [dbo].[rsp_SelectZones]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_SelectZones  
 @SiteID INT  
AS  
BEGIN  
   
 -- Deleting Unassociated zones   
 IF @SiteID = 0  
 BEGIN  
  DELETE FROM Zone WHERE Site_ID = 0  
 END  
 SELECT DISTINCT   
        z.Zone_ID,  
        z.Zone_Name ,  
        (  
            CASE   
                 WHEN ISNULL(I.Installation_ID, 0) <> 0 THEN 1  
                 ELSE 0  
            END  
        ) AS AssignedZones,  
        ISNULL(z.PromotionEnabled, 0) AS PromotionEnabled,
        Standard_Opening_Hours_ID  
    INTO #Temp1      
 FROM   Zone z  
        INNER JOIN Bar_Position BP  
             ON  BP.Zone_ID = Z.Zone_Id  
        INNER JOIN installation I  
             ON  I.Bar_Position_Id = BP.Bar_Position_Id  
 WHERE  z.Site_ID = @SiteID AND Len(ISNULL(Zone_name,''))>0  
 
 Select Zone_ID,  
        Zone_Name ,
        AssignedZones,
        PromotionEnabled,
        Standard_Opening_Hours_ID
        from #Temp1
UNION

 SELECT z.Zone_ID,  
        z.Zone_Name ,
        0 AS AssignedZones,
        PromotionEnabled,
        Standard_Opening_Hours_ID
         FROM Zone z
         WHERE z.Site_ID = @SiteID 
         AND 
         Len(ISNULL(Zone_name,''))>0 
         AND Zone_ID NOT IN (SELECT Zone_ID FROM #Temp1)
  
END   
GO


