USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_EBS_UpdateZoneDetails]    Script Date: 03/12/2014 13:02:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EBS_UpdateZoneDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EBS_UpdateZoneDetails]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_EBS_UpdateZoneDetails]    Script Date: 03/12/2014 13:02:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
[usp_EBS_UpdateZoneDetails] @ZoneId = 1, @IsDelete = 1
Select * from Zone
Select * from EBS_Export_History
*/
CREATE PROCEDURE [dbo].[usp_EBS_UpdateZoneDetails](@ZoneId INT, @IsDelete BIT = 0)  
AS  
BEGIN  
SET NOCOUNT ON
 DECLARE @Zone  TABLE (  
             ZoneID INT ,  
             ZoneName VARCHAR(50), 
             ZoneValue  VARCHAR(50),   
             IsActive BIT 
         )  
   
 DECLARE @Value    XML   
 DECLARE @SiteCode  VARCHAR(50)
	
	SELECT @SiteCode = Site_Code
	FROM   dbo.[Site] s WITH(NOLOCK)
	WHERE  S.Site_ID  IN (Select Site_ID FROM Zone WHERE Zone_ID = @ZoneId)
	
	
 INSERT INTO @Zone  
 EXEC [dbo].[rsp_EBS_GetZoneDetails] @SiteCode = @SiteCode, @ZoneID = @ZoneId  
   
 SELECT @Value = (  
            SELECT 
            ZoneID,
            ZoneName,
            ZoneValue,
            (CASE WHEN @IsDelete = 1 THEN 0 ELSE IsActive END) AS IsActive
            FROM  @Zone G  
                   FOR XML PATH('Zone'),  
                   TYPE,  
                   ELEMENTS,  
                   ROOT('Zones')  
        )  
   
 IF @Value IS NOT NULL
 BEGIN  
 EXEC [dbo].[usp_EBS_InsertExportHistory] @EH_Type = 'Zone',  
   @EH_Value = @Value, @EH_SiteCode = @SiteCode, @RefTableID = @ZoneId,
@IsDelete = @IsDelete   
 END
   SET NOCOUNT OFF
END  

GO

